Public Class frmFileUpdateOptions
    Sub New(ByVal Filename As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Label1.Text = "La version de " & Filename & " de su pc es mas actualizada que la del servidor, Desea sobreescribir el archivo?"
    End Sub

    Private Sub btnOverwrite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOverwrite.Click
        Me.DialogResult = Windows.Forms.DialogResult.Yes
    End Sub

    Private Sub btnUpdateServerFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateServerFile.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class