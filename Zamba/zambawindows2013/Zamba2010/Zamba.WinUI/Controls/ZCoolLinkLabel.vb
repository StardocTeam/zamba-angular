Public Class ZCoolLinkLabel
    Inherits LinkLabel
    Private Sub ZCoolLinkLabel_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        ForeColor = System.Drawing.Color.LightBlue
    End Sub
    Private Sub ZCoolLinkLabel_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseUp
        ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
    End Sub
End Class
