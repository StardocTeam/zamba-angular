Imports Zamba.Core

Partial Class Logout

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim User As IUser = DirectCast(Session("User"), IUser)
            Dim UcmObj As Ucm = New Ucm()

            'Se remueven las conexiones y licencias
            If Not IsNothing(Session("ComputerNameOrIP")) And Not IsNothing(Session("ConnectionId")) Then
                Dim SRights As New Zamba.Services.SRights()
                Zamba.Core.WF.WF.WFTaskBusiness.CloseOpenTasksByConId(Session("ConnectionId").ToString())
                SRights.RemoveConnectionFromWeb(Int64.Parse(Session("ConnectionId").ToString()))
                SRights = Nothing
            End If

            Session.RemoveAll()
            Session.Abandon()

            FormsAuthentication.SignOut()
            Response.Redirect("~/Views/Security/LogIn.aspx", False)
            'FormsAuthentication.RedirectToLoginPage()

        Catch ex As Exception
            ZTrace.WriteLine(ex.ToString())
            Session.RemoveAll()
            Session.Abandon()
        Finally
            ZTrace.RemoveCurrentInstance()
        End Try
    End Sub
End Class