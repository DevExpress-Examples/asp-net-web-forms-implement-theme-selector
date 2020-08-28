<%@ Page Title="" Language="vb" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton"></dx:ASPxButton>
</asp:Content>