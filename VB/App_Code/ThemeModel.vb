Option Infer On

Imports System
Imports System.Xml.Serialization
Imports System.Collections.Generic
Imports System.Web.UI
Imports System.Drawing



Public Class ThemeModel
    Inherits ThemeModelBase

    Private _baseColor As String
    Private _fontFamily As String
    Private _fontSize As String
    Private _showAsTop As Boolean
    Private _previewColor As String
    <XmlAttribute> _
    Public Property BaseColor() As String
        Get
            If String.IsNullOrEmpty(_baseColor) Then
                Return ""
            End If
            Return _baseColor
        End Get
        Set(ByVal value As String)
            _baseColor = value.ToUpper()
        End Set
    End Property
    <XmlAttribute> _
    Public Property IsNew() As Boolean
    <XmlAttribute> _
    Public Property FontFamily() As String
        Get
            If String.IsNullOrEmpty(_fontFamily) Then
                Return ""
            End If
            Return _fontFamily
        End Get
        Set(ByVal value As String)
            _fontFamily = value
        End Set
    End Property
    <XmlAttribute> _
    Public Property FontSize() As String
        Get
            If String.IsNullOrEmpty(_fontSize) Then
                Return ""
            End If
            Return _fontSize
        End Get
        Set(ByVal value As String)
            _fontSize = value
        End Set
    End Property
    Public ReadOnly Property Font() As String
        Get
            Dim result = String.Empty
            If Not String.IsNullOrEmpty(FontSize) AndAlso Not String.IsNullOrEmpty(FontFamily) Then
                result = String.Format("{0} {1}", FontSize, FontFamily)
            End If
            Return result
        End Get
    End Property
    <XmlAttribute> _
    Public Property ShowAsTop() As Boolean
        Get
            Return _showAsTop
        End Get
        Set(ByVal value As Boolean)
            _showAsTop = value
        End Set
    End Property
    <XmlAttribute> _
    Public Property PreveiwColor() As String
        Get
            If String.IsNullOrEmpty(_previewColor) Then
                Return ""
            End If
            Return _previewColor
        End Get
        Set(ByVal value As String)
            _previewColor = value.ToUpper()
        End Set
    End Property
End Class


