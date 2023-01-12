Imports Zamba.Core

Public Class ZopenWaitForm

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub ZopenWaitForm_Activated(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Activated
        Application.DoEvents()
        Update()
    End Sub


    Private Sub ZopenWaitForm_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        Application.DoEvents()
        Update()
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Public Sub SetDataSource(dtTasks As DataTable)
        Try
            Panel1.Visible = False
            Panel2.Visible = True
            Panel3.Visible = True
            Panel3.Height = 500
            Height = 600
            RadListView1.DisplayMember = "Tarea"
            RadListView1.DataSource = dtTasks
            Application.DoEvents()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class