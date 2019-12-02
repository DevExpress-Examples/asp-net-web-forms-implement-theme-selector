Option Infer On

Imports System
Imports System.Xml.Serialization
Imports System.Collections.Generic


Public Class ThemeModelBase
	Private _name As String
	Private _title As String
	<XmlAttribute>
	Public Property Name() As String
		Get
			If _name Is Nothing Then
				Return ""
			End If
			Return _name
		End Get
		Set(ByVal value As String)
			_name = value
		End Set
	End Property
	<XmlAttribute>
	Public Property Title() As String
		Get
			If _title Is Nothing Then
				Return ""
			End If
			Return _title
		End Get
		Set(ByVal value As String)
			_title = value
		End Set
	End Property
End Class


