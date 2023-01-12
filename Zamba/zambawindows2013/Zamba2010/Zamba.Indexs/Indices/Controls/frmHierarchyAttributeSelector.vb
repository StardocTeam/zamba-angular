Imports Zamba.Core

Public Class frmHierarchyAttributeSelector
    Inherits ZForm
    Implements IDisposable
    Private _indexId As Int32

    Public Event ParentSelected(ByVal ParentIndexId As Integer)

    Public Sub New()
        InitializeComponent()
        EnableAndFillControls()
    End Sub

    Private Sub btnAsociateIndex_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAsociateIndex.Click
        If Not cmbIndiceJerarquico.SelectedValue Is Nothing AndAlso _
            cmbIndiceJerarquico.SelectedValue > 0 Then
            RaiseEvent ParentSelected(cmbIndiceJerarquico.SelectedValue)
            DialogResult = DialogResult.OK
            Close()
        Else
            MessageBox.Show("Por favor seleccione un atributo padre.", "Error", MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub EnableAndFillControls()
        Dim DsIndexDropDown As DataSet
        DsIndexDropDown = IndexsBusiness.GetIndexOfAnyDropDownType()

        cmbIndiceJerarquico.DataSource = DsIndexDropDown.Tables("DOC_INDEX")
        cmbIndiceJerarquico.DisplayMember = "INDEX_NAME"
        cmbIndiceJerarquico.ValueMember = "INDEX_ID"
    End Sub
End Class