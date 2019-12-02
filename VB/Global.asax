<%@ Application Language="vb" %>

<script RunAt="server">
	Protected Sub Application_PreRequestHandlerExecute(ByVal sender As Object, ByVal e As EventArgs)
		DevExpress.Web.ASPxWebControl.GlobalTheme = Utils.CurrentTheme
		Utils.ResolveThemeParametes()
	End Sub
</script>