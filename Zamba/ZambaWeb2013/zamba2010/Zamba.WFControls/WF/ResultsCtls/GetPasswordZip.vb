Public Class GetPasswordZip
    Private Sub AceptarPass_Click(sender As Object, e As EventArgs) Handles AceptarPass.Click
        Close()
    End Sub

    Private Sub CancelPass_Click(sender As Object, e As EventArgs) Handles CancelPass.Click
        Close()
        CancelPass.Tag = "Cancelo"
    End Sub
End Class