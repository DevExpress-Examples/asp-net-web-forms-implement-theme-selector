using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ThemeParametersSelector : System.Web.UI.UserControl {
    protected void Page_Load(object sender, EventArgs e) {
        if(Utils.CanChangeBaseColor) {
            BaseColorEditor.DataSource = Utils.CustomBaseColors.Select(c => new { Value = c, ImageUrl = Utils.GetColoredSquareUrl(c) });
            BaseColorEditor.DataBind();
            string currentBaseColor = string.IsNullOrWhiteSpace(Utils.CurrentBaseColor) ? Utils.GetDefaultBaseColor(Utils.CurrentTheme) : Utils.CurrentBaseColor;
            var item = BaseColorEditor.Items.FindByValue(currentBaseColor);
            if(item != null)
                item.Selected = true;
        }
        FontEditor.DataSource = Utils.GetFontFamiliesDataSource().Select(i => new { Value = i.Value, Text = i.Text, ImageUrl = "" });
        FontEditor.DataBind();
        FontEditor.SelectedIndex = !string.IsNullOrEmpty(ASPxWebControl.GlobalThemeFont) ? FontEditor.Items.IndexOfValue(ASPxWebControl.GlobalThemeFont) : 0;
    }
    protected void Page_Init(object sender, EventArgs e) {
        InitEditor(BaseColorEditor);
        InitEditor(FontEditor);
    }
    protected void InitEditor(ASPxComboBox editor) {
        editor.DropDownStyle = DropDownStyle.DropDownList;
        editor.ValueField = "Value";
        editor.ImageUrlField = "ImageUrl";
        editor.ShowImageInEditBox = true;
        editor.CssClass += " themes-parameters-editor";
        editor.CaptionSettings.Position = EditorCaptionPosition.Top;
        editor.CaptionSettings.ShowColon = false;
        editor.CaptionStyle.CssClass = "themes-parameters-caption";
        editor.ListBoxStyle.CssClass += " themes-parameters-listbox";
        editor.ButtonStyle.CssClass = "themes-parameters-button-edit";
        editor.DropDownButton.Image.SpriteProperties.CssClass = "icon drop-down";
        editor.DropDownButton.Image.Height = 12;
        editor.DropDownButton.Image.Width = 12;
    }
}