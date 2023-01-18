Imports Zamba.Core

Public Class UCSelectAsoc

    Public DocsSel As ArrayList

    Public Sub New(ByVal docs As ArrayList)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        '[Ezequiel] 11/05/09: Cargo los tipos de documento:
        DocsSel = New ArrayList
        Dim ds As New DataTable
        ds.Columns.Add(New DataColumn("Nombre"))
        ds.Columns.Add(New DataColumn("ID"))
        lstDocTypes.DataSource = ds

        For Each id As Int64 In docs
            Dim dr As DataRow = ds.NewRow
            dr("Nombre") = DocTypesBusiness.GetDocTypeName(id, True)
            dr("ID") = id
            ds.Rows.Add(dr)
        Next
        lstDocTypes.DisplayMember = "Nombre"
        lstDocTypes.ValueMember = "ID"

    End Sub

    Private Sub UCSelectAsoc_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        For Each doc As Object In lstDocTypes.SelectedItems
            DocsSel.Add(doc.row.itemarray(1).ToString)
        Next
    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnok.Click
        Close()
    End Sub
End Class