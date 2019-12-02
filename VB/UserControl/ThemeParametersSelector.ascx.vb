Option Infer On

Imports DevExpress.Web
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Public Class UserControl_ThemeParametersSelector
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Utils.CanChangeBaseColor Then
            BaseColorEditor.DataSource = Utils.CustomBaseColors.Select(Function(c) New With { _
                Key .Value = c, _
                Key .ImageUrl = Utils.GetColoredSquareUrl(c) _
            })
            BaseColorEditor.DataBind()
            Dim currentBaseColor As String = If(String.IsNullOrWhiteSpace(Utils.CurrentBaseColor), Utils.GetDefaultBaseColor(Utils.CurrentTheme), Utils.CurrentBaseColor)
            Dim item = BaseColorEditor.Items.FindByValue(currentBaseColor)
            If item IsNot Nothing Then
                item.Selected = True
            End If
        End If
        FontEditor.DataSource = Utils.GetFontFamiliesDataSource().Select(Function(i) New With { _
            Key .Value = i.Value, _
            Key .Text = i.Text, _
            Key .ImageUrl = "" _
        })
        FontEditor.DataBind()
        FontEditor.SelectedIndex = If(Not String.IsNullOrEmpty(ASPxWebControl.GlobalThemeFont), FontEditor.Items.IndexOfValue(ASPxWebControl.GlobalThemeFont), 0)
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
        InitEditor(BaseColorEditor)
        InitEditor(FontEditor)
    End Sub
    Protected Sub InitEditor(ByVal editor As ASPxComboBox)
        editor.DropDownStyle = DropDownStyle.DropDownList
        editor.ValueField = "Value"
        editor.ImageUrlField = "ImageUrl"
        editor.ShowImageInEditBox = True
        editor.CssClass &= " themes-parameters-editor"
        editor.CaptionSettings.Position = EditorCaptionPosition.Top
        editor.CaptionSettings.ShowColon = False
        editor.CaptionStyle.CssClass = "themes-parameters-caption"
        editor.ListBoxStyle.CssClass &= " themes-parameters-listbox"
        editor.ButtonStyle.CssClass = "themes-parameters-button-edit"
        editor.DropDownButton.Image.SpriteProperties.CssClass = "icon drop-down"
        editor.DropDownButton.Image.Height = 12
        editor.DropDownButton.Image.Width = 12
    End Sub
End Class