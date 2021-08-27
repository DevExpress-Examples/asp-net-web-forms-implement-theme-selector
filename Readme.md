<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/134106079/17.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T590818)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [ThemeGroupModel.cs](./CS/App_Code/ThemeGroupModel.cs) (VB: [ThemeGroupModel.vb](./VB/App_Code/ThemeGroupModel.vb))
* [ThemeModel.cs](./CS/App_Code/ThemeModel.cs) (VB: [ThemeModel.vb](./VB/App_Code/ThemeModel.vb))
* [ThemeModelBase.cs](./CS/App_Code/ThemeModelBase.cs) (VB: [ThemeModelBase.vb](./VB/App_Code/ThemeModelBase.vb))
* [ThemesModel.cs](./CS/App_Code/ThemesModel.cs) (VB: [ThemesModel.vb](./VB/App_Code/ThemesModel.vb))
* [Utils.cs](./CS/App_Code/Utils.cs) (VB: [Utils.vb](./VB/App_Code/Utils.vb))
* [Default.aspx](./CS/Default.aspx) (VB: [Default.aspx](./VB/Default.aspx))
* [Default.aspx.cs](./CS/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/Default.aspx.vb))
* [Global.asax](./CS/Global.asax) (VB: [Global.asax](./VB/Global.asax))
* [MasterPage.master.cs](./CS/MasterPage.master.cs) (VB: [MasterPage.master.vb](./VB/MasterPage.master.vb))
* [Script.js](./CS/Scripts/Script.js) (VB: [Script.js](./VB/Scripts/Script.js))
* [ThemeParametersSelector.ascx](./CS/UserControl/ThemeParametersSelector.ascx) (VB: [ThemeParametersSelector.ascx](./VB/UserControl/ThemeParametersSelector.ascx))
* [ThemeParametersSelector.ascx.cs](./CS/UserControl/ThemeParametersSelector.ascx.cs) (VB: [ThemeParametersSelector.ascx.vb](./VB/UserControl/ThemeParametersSelector.ascx.vb))
* [ThemeSelector.ascx](./CS/UserControl/ThemeSelector.ascx) (VB: [ThemeSelector.ascx](./VB/UserControl/ThemeSelector.ascx))
* [ThemeSelector.ascx.cs](./CS/UserControl/ThemeSelector.ascx.cs) (VB: [ThemeSelector.ascx.vb](./VB/UserControl/ThemeSelector.ascx.vb))
<!-- default file list end -->
# How to implement a Theme Selector control similar to DevExpress Demo
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/t590818/)**
<!-- run online end -->


<p>The sample provides two web user controls (ThemeSelector, ThemeParametersSelector) that can be used in your project. To use these controls in your solution, execute these steps:</p>
<p>1. Copy the following files, taking into account their location:</p>
<p>   a. An xml file with theme groups and themes: Themes.xml;<br>   b. Classes that are responsible for getting and presenting data from Themes.xml: ThemeGroupModel.cs, ThemeModel.cs, ThemeModelBase.cs, ThemesModel.cs;<br>   c. sprite.svg with images;<br>   d. sprite.css and StyleSheet.css with CSS classes;<br>   e. Script.js with client-side methods;   <br>   f. ThemeSelector.ascx, ThemeSelector.ascx.cs, ThemeParametersSelector.ascx and ThemeParametersSelector.ascx.cs;<br>   g. Utils.cs - a class that is responsible for manipulations with themes.</p>
<p>2. Register the ThemeSelector and ThemeParametersSelector web user controls in your web.config file:</p>


```aspx
<pages>
  <controls>
...
    <add src="~/UserControl/ThemeParametersSelector.ascx" tagName="ThemeParametersSelector" tagPrefix="dx" />
    <add src="~/UserControl/ThemeSelector.ascx" tagName="ThemeSelector" tagPrefix="dx" />
  </controls>
</pages>
```


<p>3. In the sample, a chosen theme is written to a cookie. To apply this theme from the cookie, subscribe to the Application.PreRequestHandlerExecute event in your Global.asax file and handle it in the following manner:</p>


```cs
protected void Application_PreRequestHandlerExecute(object sender, EventArgs e) {
    DevExpress.Web.ASPxWebControl.GlobalTheme = Utils.CurrentTheme;
    Utils.ResolveThemeParametes();
}
```


<p>4. Add the ThemeSelector and ThemeParametersSelector user controls to ASPxPanel of the master page:</p>


```aspx
    <form id="form1" runat="server">
        <header>
            <dx:ASPxPanel runat="server" ClientInstanceName="TopPanel" CssClass="header-panel" FixedPosition="WindowTop" EnableTheming="false">
                <PanelCollection>
                    <dx:PanelContent>
                        <a class="right-button icon cog right-button-toggle-themes-panel" href="javascript:void(0)" onclick="DXDemo.toggleThemeSettingsPanel(); return false;"></a>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </header>
        <div class="main-content-wrapper">
            <section class="top-panel clearfix top-panel-dark">
                <dx:ASPxButton runat="server" Text="Change Theme Settings" CssClass="theme-settings-menu-button adaptive"
                    EnableTheming="false" AutoPostBack="false" ImagePosition="Right" UseSubmitBehavior="false">
                    <Image SpriteProperties-CssClass="icon angle-down theme-settings-menu-button-image" />
                    <FocusRectBorder BorderWidth="0" />
                    <ClientSideEvents Click="DXDemo.toggleThemeSettingsPanel" />
                </dx:ASPxButton>
            </section>
            <dx:ASPxPanel runat="server" ClientInstanceName="ThemeSettingsPanel" CssClass="theme-settings-panel"
                FixedPosition="WindowRight" Collapsible="true" EnableTheming="false" ScrollBars="Auto">
                <SettingsCollapsing AnimationType="Slide" ExpandEffect="PopupToLeft" ExpandButton-Visible="false" />
                <Styles>
                    <ExpandBar Width="0" />
                    <ExpandedPanel CssClass="theme-settings-panel-expanded"></ExpandedPanel>
                </Styles>
                <PanelCollection>
                    <dx:PanelContent>
                        <div class="top-panel top-panel-dark clearfix">
                            <dx:ASPxButton runat="server" Text="Change Theme Settings" CssClass="theme-settings-menu-button"
                                EnableTheming="false" AutoPostBack="false" ImagePosition="Right" HorizontalAlign="Left" UseSubmitBehavior="false">
                                <Image SpriteProperties-CssClass="icon angle-down theme-settings-menu-button-image" />
                                <FocusRectBorder BorderWidth="0" />
                                <ClientSideEvents Click="DXDemo.toggleThemeSettingsPanel" />
                            </dx:ASPxButton>
                        </div>
                        <div class="theme-settings-panel-content">
                            <dx:ThemeSelector ID="ThemeSelector" runat="server" />
                            <% if(Utils.CanApplyThemeParameters) { %>
                            <dx:ThemeParametersSelector ID="ThemeParametersSelector" runat="server" />
                            <% } %>
                        </div>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </div>
        <div style="clear: both;">
            <asp:ContentPlaceHolder ID="ContentPlaceHolder1" runat="server">
            </asp:ContentPlaceHolder>
        </div>
    </form>

```


<p><strong>See also:</strong><br><a href="https://supportcenter.devexpress.com/ticket/details/t504407/how-to-implement-a-theme-selector-control-similar-to-devexpress-demo-old-style">How to implement a Theme Selector control similar to DevExpress Demo (Old Style)</a></p>

<br/>


