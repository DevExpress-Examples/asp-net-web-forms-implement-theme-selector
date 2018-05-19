Option Infer On

Imports System.Xml.Serialization
Imports System.Collections.Generic
Imports System.IO
Imports System.Web
Imports System.Linq


<XmlRoot("Themes")> _
Public Class ThemesModel
    Private Const ResourceName As String = "DevExpress.Web.Demos.Data.Themes.xml"
    Private Shared _current As ThemesModel
    Private Shared ReadOnly _currentLock As New Object()
    Public Shared ReadOnly Property Current() As ThemesModel
        Get
            SyncLock _currentLock
                If _current Is Nothing Then
                    Using stream As Stream = File.OpenRead(HttpContext.Current.Server.MapPath("~/App_Data/Themes.xml"))
                        Dim serializer As New XmlSerializer(GetType(ThemesModel))
                        _current = DirectCast(serializer.Deserialize(stream), ThemesModel)
                    End Using
                End If
                Return _current
            End SyncLock
        End Get
    End Property
    Private _groups As New List(Of ThemeGroupModel)()
    Public Shared ReadOnly Property MobileThemes() As IEnumerable(Of String)
        Get
            Return Current.Groups.First(Function(g) g.Name = "Mobile").Themes.Select(Function(t) t.Name)
        End Get
    End Property
    Public Shared ReadOnly Property AllThemes() As IEnumerable(Of ThemeModel)
        Get
            Return Current.Groups.SelectMany(Function(g) g.Themes)
        End Get
    End Property
    Public Shared ReadOnly Property NewThemes() As IEnumerable(Of ThemeModel)
        Get
            Return AllThemes.Where(Function(theme) theme.IsNew)
        End Get
    End Property
    Public Function GetAllGroups() As List(Of ThemeGroupModel)
        Dim topThemes = AllThemes.Where(Function(t) t.ShowAsTop).ToList()
        Dim currentTheme = AllThemes.First(Function(t) t.Name = Utils.CurrentTheme)
        If Not topThemes.Contains(currentTheme) Then
            topThemes.Add(currentTheme)
        End If
        Dim firstGroup = New ThemeGroupModel() With { _
            .Name = "FirstGroup", _
            .Themes = topThemes _
        }
        Return Groups.Concat( { firstGroup }).ToList()
    End Function
    <XmlElement("ThemeGroup")> _
    Public ReadOnly Property Groups() As List(Of ThemeGroupModel)
        Get
            Return _groups
        End Get
    End Property
    Public ReadOnly Property LeftGroups() As List(Of ThemeGroupModel)
        Get
            Return ( _
                From g In Groups _
                Where g.Float = "Left" _
                Select g).ToList()
        End Get
    End Property
    Public ReadOnly Property RightGroups() As List(Of ThemeGroupModel)
        Get
            Return ( _
                From g In Groups _
                Where g.Float = "Right" _
                Select g).ToList()
        End Get
    End Property
    Public Shared Function GetThemeModel(ByVal themeName As String) As ThemeModel
        Return Current.Groups.SelectMany(Function(g) g.Themes).FirstOrDefault(Function(t) t.Name = themeName)
    End Function
End Class



