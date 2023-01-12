Public Class TestSendMail

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim TS As New ILM.LotusLibrary.TestMail
            TS.SendMail(Me.TextBox1.Text, Me.TextBox2.Text, Me.RichTextBox1.Text)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub
End Class
