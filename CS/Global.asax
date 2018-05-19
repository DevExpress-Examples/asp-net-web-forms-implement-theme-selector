<%@ Application Language="C#" %>

<script RunAt="server">
    protected void Application_PreRequestHandlerExecute(object sender, EventArgs e) {
        DevExpress.Web.ASPxWebControl.GlobalTheme = Utils.CurrentTheme;
        Utils.ResolveThemeParametes();
    }
</script>
