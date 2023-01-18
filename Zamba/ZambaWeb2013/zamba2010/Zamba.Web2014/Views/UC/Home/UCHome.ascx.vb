Imports Zamba.Core

Partial Class Views_UC_Home_UCHome
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserId") IsNot Nothing Then

            'valido permisos de creacion para el entidad industrias
            If RightsBusiness.GetUserRights(ObjectTypes.DocTypes, RightsType.Create, 1027) Then
                btnAltaEntidad.Visible = True
            Else
                btnAltaEntidad.Visible = False
            End If

            'valido permisos de creacion para el entidad Reintegro
            If RightsBusiness.GetUserRights(ObjectTypes.DocTypes, RightsType.Create, 26028) Then
                btnAltaEntidad1.Visible = True
            Else
                btnAltaEntidad1.Visible = False
            End If

            'valido permisos de creacion para el entidad afiliado
            If RightsBusiness.GetUserRights(ObjectTypes.DocTypes, RightsType.Create, 26042) Then
                btnAltaEntidad6.Visible = True
            Else
                btnAltaEntidad6.Visible = False
            End If

            'Instancio un controller 
            Dim dynamicBtnController As New DynamicButtonController()
            'Pido la vista
            Dim dynBtnView As DynamicButtonPartialViewBase = dynamicBtnController.GetViewHomeButtons(DirectCast(Session("User"), IUser))
            'La agrego
            pnl.Controls.Add(dynBtnView)
        End If

    End Sub
End Class
