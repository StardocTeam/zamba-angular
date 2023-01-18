Public Class GetDir

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Shell("explorer.exe " & TextBox1.Text, AppWinStyle.NormalFocus)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Clipboard.SetText(TextBox1.Text)
    End Sub
End Class
