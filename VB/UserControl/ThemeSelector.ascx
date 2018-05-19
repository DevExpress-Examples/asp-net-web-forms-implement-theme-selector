<%@ Control Language="vb" AutoEventWireup="true" CodeFile="ThemeSelector.ascx.vb" Inherits="UserControl_ThemeSelector" %>
<dx:ASPxLabel runat="server" Text="Themes" CssClass="themes-caption"></dx:ASPxLabel>
<asp:Repeater runat="server" ID="ThemesContainerRepeater" EnableViewState="false" DataSource="<%#ThemesModel.Current.GetAllGroups()%>">
    <ItemTemplate>
        <dx:ASPxListBox runat="server" CssClass="themes-list" Width="100%" Caption="Themes" CaptionSettings-Position="Top"
            ValueField="Value" TextField="Text" ImageUrlField="ImageUrl" OnDataBinding="ThemesList_DataBinding" EnableCallbackMode="false"
            OnPreRender="ThemesLists_PreRender">
            <CaptionCellStyle CssClass="themes-list-caption-cell" />
            <ItemImage Height="18px" Width="18px" />
            <CaptionSettings ShowColon="false" />
            <CaptionStyle CssClass="themes-list-caption" />
            <ItemStyle CssClass="item" SelectedStyle-CssClass="item-selected" HoverStyle-CssClass="item-hover" />
            <ClientSideEvents SelectedIndexChanged="DXDemo.OnThemesListSelectedIndexChanged" Init="DXDemo.OnThemesListInit" />
        </dx:ASPxListBox>
    </ItemTemplate>
</asp:Repeater>
<dx:ASPxButton runat="server" ID="ShowAllThemesButton" ClientInstanceName="ShowAllThemesButton" Text="Show All Themes" Width="100%"
    AutoPostBack="false" CssClass="show-all-themes-button" HorizontalAlign="Left">
    <FocusRectBorder BorderWidth="0" />
    <Image SpriteProperties-CssClass="icon three-dots"/>
    <ClientSideEvents Click="function(){ DXDemo.OnShowAllThemesClick(); }" />
</dx:ASPxButton>