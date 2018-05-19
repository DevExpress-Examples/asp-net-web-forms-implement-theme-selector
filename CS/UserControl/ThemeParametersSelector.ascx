<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ThemeParametersSelector.ascx.cs" Inherits="UserControl_ThemeParametersSelector" %>

<div class="themes-parameters-container">
<% if(Utils.CanChangeBaseColor) { %>
    <dx:ASPxComboBox runat="server" ID="BaseColorEditor" Caption="Base Color" EnableTheming="false" TextField="Value" DropDownRows="8">
        <ItemImage Height="18px" Width="18px" />
        <ClientSideEvents SelectedIndexChanged="function(s){ DXDemo.SetCurrentBaseColor(s.GetSelectedItem().value); }" />
    </dx:ASPxComboBox>
<% } %>
    <dx:ASPxComboBox runat="server" ID="FontEditor" Caption="Font" TextField="Text" EnableTheming="false" CssClass="font-editor" >
        <Buttons>
            <dx:EditButton Image-SpriteProperties-CssClass="icon font" Position="Left" />
        </Buttons>
        <ListBoxStyle CssClass="font-editor" />
        <ItemImage SpriteProperties-CssClass="icon font" />
        <ClientSideEvents SelectedIndexChanged="function(s){ DXDemo.SetCurrentFont(s.GetSelectedItem().value); }" ButtonClick="DXDemo.iconButtonClick" />
    </dx:ASPxComboBox>
</div>
