
Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub Login1_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles Login1.Authenticate
        Dim user As Zamba.Core.User
        user = TreeViewBusiness.Validatelogin(Login1.UserName, Login1.Password)
        If IsNothing(user) OrElse user.ID = 0 Then
            Login1.FailureText = "El usuario o clave es incorrecto"
            Login1.FailureAction = LoginFailureAction.Refresh
        Else
            Server.Transfer("Default.aspx", False)
        End If
    End Sub
End Class
