Option Infer On

Imports DevExpress.Web
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Script.Serialization
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Public Class UserControl_ThemeSelector
	Inherits System.Web.UI.UserControl

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		ThemesContainerRepeater.DataBind()
		ShowAllThemesButton.JSProperties("cpThemesListsNames") = ThemesModel.Current.GetAllGroups().Select(Function(g) GetThemesListClientName(g.Name)).ToList()
		ShowAllThemesButton.JSProperties("cpCurrenTheme") = Utils.CurrentTheme
	End Sub
	Protected Sub ThemesList_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
		Dim themesList As ASPxListBox = DirectCast(sender, ASPxListBox)
		Dim item As RepeaterItem = TryCast(themesList.NamingContainer, RepeaterItem)
		If item Is Nothing Then
			Return
		End If
		Dim group As ThemeGroupModel = TryCast(item.DataItem, ThemeGroupModel)
		BindThemesList(themesList, group)
	End Sub
	Private Sub BindThemesList(ByVal themesList As ASPxListBox, ByVal group As ThemeGroupModel)
		Dim isFirstgroup As Boolean = group.Name = "FirstGroup"
		themesList.Caption = group.Title
		If isFirstgroup Then
			themesList.CssClass &= " firstGroup"
		End If
		Dim dataSource = group.Themes.Select(Function(t) New ListEditItem() With {
			.Value = GetThemeTitle(t.Name),
			.Text = t.Title,
			.ImageUrl = Utils.GetColoredSquareUrl(t.PreveiwColor)
		}).ToList()
		themesList.ClientInstanceName = GetThemesListClientName(group.Name)
		themesList.ClientVisible = isFirstgroup
		themesList.Items.Clear()
		themesList.Items.AddRange(dataSource)
	End Sub
	Protected Sub ThemesLists_PreRender(ByVal sender As Object, ByVal e As EventArgs)
		Dim themesList As ASPxListBox = DirectCast(sender, ASPxListBox)
		themesList.UnselectAll()
		Dim selectedItem = themesList.Items.FindByValue(GetThemeTitle(Utils.CurrentTheme))
		If selectedItem IsNot Nothing Then
			selectedItem.Selected = True
		End If
		Dim jsSerializer = New JavaScriptSerializer()
		themesList.JSProperties("cpNewThemes") = themesList.Items.Cast(Of ListEditItem)().Where(Function(item) ThemesModel.NewThemes.Select(Function(t) t.Title).Contains(item.Text)).Select(Function(item) item.Text)
	End Sub
	Private Function GetThemesListClientName(ByVal groupName As String) As String
		Return "themesList" & groupName
	End Function
	Private Function GetThemeTitle(ByVal themeName As String) As String
		Return If(Not String.IsNullOrEmpty(themeName), themeName, "Default")
	End Function
End Class