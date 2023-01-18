Public Class FrmTxtInteligente

    Public ReadOnly Property Valor()
        Get
            Return txtTextoInteligente.Text
        End Get
    End Property

    Private Sub btnIngTextoInteligente_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnIngTextoInteligente.Click
        DialogResult = DialogResult.OK
    End Sub

    Private Sub btnCancelTextoInteligenete_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancelTextoInteligenete.Click
        DialogResult = DialogResult.Cancel
    End Sub
End Class