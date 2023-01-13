
Partial Class PrintGridview
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack AndAlso Not IsNothing(Session("PrintGridviewColumns")) AndAlso Not IsNothing(Session("PrintGridViewDataSource")) Then

            For Each dcf As DataControlField In CType(Session("PrintGridviewColumns"), DataControlFieldCollection)
                gvTareas.Columns.Add(dcf)
            Next

            gvTareas.DataSource = Session("PrintGridViewDataSource")
            gvTareas.DataBind()

            Dim sb As StringBuilder = New StringBuilder
            sb.Append("<script language=javascript>")
            sb.Append("window.print();")
            sb.Append("</script>")
            ClientScript.RegisterStartupScript(Me.GetType, "print", sb.ToString)
            Session("PrintGridviewColumns") = Nothing
            Session("PrintGridViewDataSource") = Nothing

        End If
    End Sub
End Class
