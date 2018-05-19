using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ThemeSelector : System.Web.UI.UserControl {
    protected void Page_Load(object sender, EventArgs e) {
        ThemesContainerRepeater.DataBind();
        ShowAllThemesButton.JSProperties["cpThemesListsNames"] = ThemesModel.Current.GetAllGroups().Select(g => GetThemesListClientName(g.Name)).ToList();
        ShowAllThemesButton.JSProperties["cpCurrenTheme"] = Utils.CurrentTheme;
    }
    protected void ThemesList_DataBinding(object sender, EventArgs e) {
        ASPxListBox themesList = (ASPxListBox)sender;
        RepeaterItem item = themesList.NamingContainer as RepeaterItem;
        if(item == null)
            return;
        ThemeGroupModel group = item.DataItem as ThemeGroupModel;
        BindThemesList(themesList, group);
    }
    void BindThemesList(ASPxListBox themesList, ThemeGroupModel group) {
        bool isFirstgroup = group.Name == "FirstGroup";
        themesList.Caption = group.Title;
        if(isFirstgroup)
            themesList.CssClass += " firstGroup";
        var dataSource = group.Themes.Select(t => new ListEditItem() {
            Value = GetThemeTitle(t.Name),
            Text = t.Title,
            ImageUrl = Utils.GetColoredSquareUrl(t.PreveiwColor),
        }).ToList();
        themesList.ClientInstanceName = GetThemesListClientName(group.Name);
        themesList.ClientVisible = isFirstgroup;
        themesList.Items.Clear();
        themesList.Items.AddRange(dataSource);
    }
    protected void ThemesLists_PreRender(object sender, EventArgs e) {
        ASPxListBox themesList = (ASPxListBox)sender;
        themesList.UnselectAll();
        var selectedItem = themesList.Items.FindByValue(GetThemeTitle(Utils.CurrentTheme));
        if(selectedItem != null)
            selectedItem.Selected = true;
        var jsSerializer = new JavaScriptSerializer();
        themesList.JSProperties["cpNewThemes"] = themesList.Items.Cast<ListEditItem>()
            .Where(item => ThemesModel.NewThemes.Select(t => t.Title).Contains(item.Text))
            .Select(item => item.Text);
    }
    string GetThemesListClientName(string groupName) {
        return "themesList" + groupName;
    }
    string GetThemeTitle(string themeName) {
        return !string.IsNullOrEmpty(themeName) ? themeName : "Default";
    }
}