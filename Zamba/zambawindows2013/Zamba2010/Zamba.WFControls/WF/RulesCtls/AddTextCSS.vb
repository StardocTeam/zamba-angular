Public Class AddTextCSS
    Private Sub txtAddClass_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAddClass.KeyDown
        If e.KeyCode = Keys.Escape Then
            txtAddClass.Text = ""
            Close()
        End If
        'Acepto cambio
        If e.KeyCode = Keys.Enter Then
            DialogResult = DialogResult.OK
            Close()
        End If
    End Sub
End Class