
Partial Class Log
    Inherits System.Web.UI.Page

    Protected Sub Login1_Logueado() Handles Login1.LoggedIn
        Response.Redirect("Monitor.aspx")
    End Sub
End Class
