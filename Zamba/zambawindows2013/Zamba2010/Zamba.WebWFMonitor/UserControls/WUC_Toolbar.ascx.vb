
Partial Class WUC_Toolbar
    Inherits System.Web.UI.UserControl

    Public Event Asignar()
    Public Event Desasignar()
    Public Event Quitar()
    Public Event Distribuir()
    Public Event RenovarVencimiento()
    Public Event Historial()

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        RaiseEvent Desasignar()
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        RaiseEvent Quitar()
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        RaiseEvent Distribuir()
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        RaiseEvent RenovarVencimiento()
    End Sub

    Protected Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
        RaiseEvent Historial()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        RaiseEvent Asignar()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Page.RegisterRequiresRaiseEvent(Button1)
        'Button1.Attributes.Add("onclick", "javascript:url();")
    End Sub
End Class
