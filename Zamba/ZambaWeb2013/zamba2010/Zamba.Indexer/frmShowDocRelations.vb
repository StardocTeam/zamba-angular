Imports Zamba.Core
Imports Zamba.AppBlock

Public Class frmShowDocRelations
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        loadRelations()
    End Sub

    Private Sub loadRelations()
        Dim ds As DataSet = Results_Business.GetDocRelations
        For Each r As DataRow In ds.Tables(0).Rows
            DataGridView1.Rows.Add(r.Item("RELATIONID"), r.Item("NAME"))
        Next
    End Sub

    Public ReadOnly Property SelectedRelationId() As Int32
        Get
            If DataGridView1.SelectedRows.Count > 0 Then
                Return Int32.Parse(DataGridView1.SelectedRows(0).Cells(0).Value.ToString)
            Else
                Return -1
            End If
        End Get
    End Property

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub BtnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAccept.Click
        If SelectedRelationId = -1 Then
            MessageBox.Show("Seleccione una relación", "Zamba")
            Exit Sub
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class