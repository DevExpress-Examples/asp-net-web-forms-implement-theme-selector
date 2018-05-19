Option Infer On

Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Web.Configuration
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports DevExpress.Web.Internal
Imports System.Drawing
Imports DevExpress.Web

Public NotInheritable Class Utils

    Private Sub New()
    End Sub


    Friend Const CurrentThemeCookieKeyPrefix As String = "DXCurrentTheme", CurrentBaseColorCookieKeyPrefix As String = "DXCurrentBaseColor", TunableThemeCookieKeyPrefix As String = "DXTunableTheme", CurrentFontCookieKey As String = "DXCurrentFont", DefaultThemeName As String = "Metropolis"

    Public Shared ReadOnly Property TunableThemeCookieKey() As String
        Get
            Return TunableThemeCookieKeyPrefix
        End Get
    End Property
    Public Shared ReadOnly Property DefaultTheme() As String
        Get
            Return DefaultThemeName
        End Get
    End Property
    Public Shared Function GetFontFamiliesDataSource() As List(Of ListEditItem)
        Return CustomFonts.Select(Function(f) New ListEditItem() With { _
            .Text = GetShortFontName(f), _
            .Value = f _
        }).ToList()
    End Function
    Public Shared Function GetDefaultBaseColor(ByVal themeName As String) As String
        Return ThemesModel.GetThemeModel(themeName).BaseColor
    End Function

    Public Shared ReadOnly Property CurrentBaseColorCookieKey() As String
        Get
            Return CurrentBaseColorCookieKeyPrefix
        End Get
    End Property

    Private Shared ReadOnly Property Context() As HttpContext
        Get
            Return HttpContext.Current
        End Get
    End Property
    Public Shared ReadOnly Property CustomBaseColors() As String()
        Get
            Return New String() { GetOrReplaceCurrentThemeDefaultBaseColor(), "#4796CE", "#35B86B", "#CE5B47", "#F7A233", "#9F47CE", "#5C57C9", "#CE4776"}
        End Get
    End Property
    Private Shared ThemeBases As New Dictionary(Of String, String)() From { _
        { "MaterialCompact", "Material" } _
    }
    Public Shared ReadOnly Property CanChangeTheme() As Boolean
        Get
            Return True

        End Get
    End Property
    Private Shared ReadOnly Property Request() As HttpRequest
        Get
            Return Context.Request
        End Get
    End Property
    Public Shared ReadOnly Property CurrentThemeCookieKey() As String
        Get
            Return CurrentThemeCookieKeyPrefix
        End Get
    End Property
    Public Shared ReadOnly Property CurrentTheme() As String
        Get
            If CanChangeTheme AndAlso Request.Cookies(CurrentThemeCookieKey) IsNot Nothing Then
                Return HttpUtility.UrlDecode(Request.Cookies(CurrentThemeCookieKey).Value)
            End If
            Return DefaultTheme
        End Get
    End Property
    Public Shared Function GetOrReplaceCurrentThemeDefaultBaseColor() As String
        If ThemeBases.ContainsKey(CurrentTheme) Then
            Dim themeModel = ThemesModel.GetThemeModel(ThemeBases(CurrentTheme))
            Return themeModel.BaseColor
        End If
        Return CurrentThemeDefaultBaseColor
    End Function
    Private Shared ReadOnly Property CurrentThemeModel() As ThemeModel
        Get
            Return ThemesModel.Current.Groups.SelectMany(Function(g) g.Themes).FirstOrDefault(Function(t) t.Name = CurrentTheme)
        End Get
    End Property
    Public Shared ReadOnly Property CustomFonts() As String()
        Get
            Return New String() { CurrentThemeDefaultFont, CurrentThemeDefaultFontSize & " " & "'Asap', normal", CurrentThemeDefaultFontSize & " " & "'Arima Madurai', normal", CurrentThemeDefaultFontSize & " " & "'Comfortaa', normal" }
        End Get
    End Property
    Public Shared ReadOnly Property CurrentThemeDefaultFont() As String
        Get
            Return CurrentThemeModel.Font
        End Get
    End Property
    Public Shared ReadOnly Property CurrentThemeDefaultFontSize() As String
        Get
            Return CurrentThemeModel.FontSize
        End Get
    End Property
    Public Shared ReadOnly Property CurrentThemeDefaultBaseColor() As String
        Get
            Return CurrentThemeModel.BaseColor
        End Get
    End Property
    Public Shared ReadOnly Property CurrentFont() As String
        Get
            If Request.Cookies(CurrentFontCookieKey) IsNot Nothing Then
                Return HttpUtility.UrlDecode(Request.Cookies(CurrentFontCookieKey).Value)
            End If
            Return CurrentThemeDefaultFont
        End Get
    End Property

    Private Shared ReadOnly Property IsThemeChanged() As Boolean
        Get
            Return CurrentTheme <> TunableTheme
        End Get
    End Property
    Private Shared Property TunableTheme() As String
        Get
            If Request.Cookies(TunableThemeCookieKey) IsNot Nothing Then
                Return HttpUtility.UrlDecode(Request.Cookies(TunableThemeCookieKey).Value)
            End If
            Return CurrentTheme
        End Get
        Set(ByVal value As String)
            Context.Response.Cookies(TunableThemeCookieKey).Value = value
        End Set
    End Property
    Private Shared Sub SetCurrentBaseColorCookie(ByVal value As String)
        If String.IsNullOrWhiteSpace(value) Then
            Request.Cookies.Remove(CurrentBaseColorCookieKey)
        Else
            Request.Cookies(CurrentBaseColorCookieKey).Value = value
        End If
        Context.Response.Cookies(CurrentBaseColorCookieKey).Value = value
    End Sub
    Private Shared Sub SetCurrentFontCookie(ByVal value As String)
        Context.Response.Cookies(CurrentFontCookieKey).Value = value
    End Sub

    Public Shared ReadOnly Property CanApplyThemeParameters() As Boolean
        Get
            Return (Not String.IsNullOrEmpty(CurrentThemeDefaultFont) OrElse Not String.IsNullOrEmpty(CurrentThemeDefaultBaseColor))
        End Get
    End Property
    Public Shared Sub ResolveThemeParametes()
        If Not CanChangeTheme Then
            Return
        End If
        Dim baseColor As String = CurrentBaseColor
        Dim font As String = CurrentFont
        If IsThemeChanged OrElse Not String.IsNullOrEmpty(baseColor) AndAlso Not CustomBaseColors.Contains(baseColor) OrElse baseColor = CurrentThemeDefaultBaseColor Then
            baseColor = String.Empty
            SetCurrentBaseColorCookie(baseColor)
        End If
        If IsThemeChanged OrElse Not String.IsNullOrEmpty(font) AndAlso Not CustomFonts.Contains(font) OrElse font = CurrentThemeDefaultFont Then
            font = String.Empty
            SetCurrentFontCookie(font)
        End If
        TunableTheme = CurrentTheme
        ASPxWebControl.GlobalThemeBaseColor = baseColor
        ASPxWebControl.GlobalThemeFont = font
    End Sub
    Private Shared Function GetShortFontName(ByVal fullName As String) As String
        If String.IsNullOrWhiteSpace(fullName) Then
            Return fullName
        End If
        Dim indexOfFirstSpace As Integer = fullName.IndexOf(" "c)
        Dim indexOfFirstComma As Integer = fullName.IndexOf(","c)
        Dim endPosition As Integer = If(indexOfFirstComma > -1, indexOfFirstComma - 1, fullName.Length - 1)
        Return fullName.Substring(indexOfFirstSpace + 1, endPosition - indexOfFirstSpace).Trim("'"c)
    End Function
    Public Shared ReadOnly Property CanChangeBaseColor() As Boolean
        Get
            Return ThemesModel.Current.Groups.Where(Function(g) g.Themes.Where(Function(t) t.Name = CurrentTheme AndAlso Not String.IsNullOrEmpty(t.BaseColor)).Count() <> 0).Count() > 0
        End Get
    End Property
    Public Shared Function GetColoredSquareUrl(ByVal color As String) As String
        Dim svg = "<svg xmlns='http://www.w3.org/2000/svg'><g><rect height='100%' width='100%' y='0' x='0' fill='" & color & "'/></g></svg>"
        Dim svgEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(svg))
        Return "data:image/svg+xml;base64," & svgEncoded
    End Function

    Public Shared ReadOnly Property CurrentBaseColor() As String
        Get
            If Request.Cookies(CurrentBaseColorCookieKey) IsNot Nothing Then
                Return HttpUtility.UrlDecode(Request.Cookies(CurrentBaseColorCookieKey).Value)
            End If
            Return CurrentThemeDefaultBaseColor
        End Get
    End Property

End Class

