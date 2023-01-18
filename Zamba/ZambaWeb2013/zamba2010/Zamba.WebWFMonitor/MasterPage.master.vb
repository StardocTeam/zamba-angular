Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Public Event Refrescar_Click(ByVal sender As Object, ByVal e As EventArgs)

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        RaiseEvent Refrescar_Click(sender, e)
    End Sub

    Protected Sub Cronometro1_TimeOut(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cronometro1.TimeOut
        RaiseEvent Refrescar_Click(sender, e)
        Me.Cronometro1.InitialTime = Now
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        Me.Cronometro1.Duracion = New TimeSpan(0, Me.DropDownList1.SelectedValue, 0)
        Me.Cronometro1.InitialTime = Now
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Cronometro1.Duracion = New TimeSpan(0, Me.DropDownList1.SelectedValue, 0)
        Me.Cronometro1.InitialTime = Now
    End Sub
End Class

