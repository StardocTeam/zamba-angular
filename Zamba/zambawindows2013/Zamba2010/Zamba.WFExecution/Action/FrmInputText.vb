Imports System.Windows.Forms
Partial Class FrmInputText
    Inherits ZForm
    Sub New(ByVal title As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Text = title
    End Sub

    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Accept_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Accept.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles Cancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub
End Class