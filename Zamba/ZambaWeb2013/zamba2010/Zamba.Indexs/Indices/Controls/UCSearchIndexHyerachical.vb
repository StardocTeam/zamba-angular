Imports Zamba.AppBlock
Imports Zamba.Core
Imports System.Windows.Forms
Imports System.Data
Imports System.Collections.Generic

Public Class UCSearchIndexHyerachical

    Inherits ZControl

    Dim _myIndex As New Index
    Dim _indexType As IndexAdditionalType

    Public Event IndexUpdated()

    Public Sub New(ByVal Index As Index, ByVal Type As IndexAdditionalType)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _myIndex = Index
        _myIndex.DropDown = IndexsBusiness.GetIndexDropDownType(_myIndex.ID)
        _indexType = Type

        EnableAndFillControls()
    End Sub

    Private Sub EnableAndFillControls()

        If _myIndex.DropDown <> IndexAdditionalType.DropDownJerarquico AndAlso _myIndex.DropDown <> IndexAdditionalType.AutoSustituciónJerarquico Then
            btnGenerarIndiceJerarquico.Visible = True
            pnlAsociacionIndices.Visible = False
            lblAsociadoCon.Visible = False
        Else
            btnGenerarIndiceJerarquico.Visible = False
            pnlAsociacionIndices.Visible = True

            Dim DsIndexDropDown As DSIndex
            Dim ParentName As String

            DsIndexDropDown = IndexsBusiness.GetIndexOfAnyDropDownType()

            cmbIndiceJerarquico.DataSource = DsIndexDropDown.DOC_INDEX
            cmbIndiceJerarquico.DisplayMember = "INDEX_NAME"
            cmbIndiceJerarquico.ValueMember = "INDEX_ID"

            ParentName = IndexsBusiness.getTableListParentName(_myIndex.ID)

            If Not String.IsNullOrEmpty(ParentName) Then
                lblAsociadoCon.Text = "Indice asociado con " & ParentName
                lblAsociadoCon.Visible = True
                btnAsociateIndex.Visible = False
                btnEliminarIndiceJerarquico.Visible = True
                cmbIndiceJerarquico.SelectedValue = _myIndex.HierarchicalParentID
                cmbIndiceJerarquico.Enabled = False
                dgHierarchicalValues.DataSource = IndexsBusiness.GetHierarchicalTable(_myIndex.ID, _myIndex.HierarchicalParentID)
                dgHierarchicalValues.Visible = True
            Else
                lblAsociadoCon.Visible = False
                btnAsociateIndex.Visible = True
                btnEliminarIndiceJerarquico.Visible = False
                cmbIndiceJerarquico.Enabled = True
            End If

            'SetDataTableNameControl()

        End If

    End Sub

    ''' <summary>
    ''' Setea el datasource del combo del nombre de la tabla a obtener los datos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Javier]	28/09/2011	Created
    ''' </history>
    Private Sub SetDataTableNameControl()
        Dim docTypesDs As DataSet = DocTypesBusiness.GetAllDocTypes()

        cmbDataTableName.DataSource = docTypesDs.Tables(0)
        cmbDataTableName.DisplayMember = "doc_type_name"
        cmbDataTableName.ValueMember = "doc_type_id"

        If Not String.IsNullOrEmpty(_myIndex.HierarchicalDataTableName) Then
            cmbDataTableName.SelectedValue = _myIndex.HierarchicalDataTableName.Remove(_myIndex.HierarchicalDataTableName.IndexOf("DOC_I"), 5)
            cmbDataTableName.Enabled = False
        End If
    End Sub

    Public Sub InsertIndicesBusquedaJerarquicos()

        Try
            Dim indexParentId As Int32 = Int32.Parse(cmbIndiceJerarquico.SelectedValue.ToString())
            'Dim indexDataTableName As String = "Hierarchy_I" & cmbIndiceJerarquico.SelectedValue.ToString() _
            '    & "_I" & _myIndex.ID

            'Armar la asociacion de atributos
            IndexsBusiness.InsertIndexListJerarquico(_myIndex.ID, indexParentId)

            UserBusiness.Rights.SaveAction(_myIndex.ID, Zamba.Core.ObjectTypes.Index, Zamba.Core.RightsType.Edit, _myIndex.Name)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            MessageBox.Show("Ocurrio un error al actualizar la tabla de Búsqueda Jerarquica", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

    End Sub

    Private Sub btnAsociateIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAsociateIndex.Click
        Try
            If ValidateIndexToAdd() Then
                InsertIndicesBusquedaJerarquicos()
                EnableAndFillControls()
                RaiseEvent IndexUpdated()
            Else
                MessageBox.Show("Alguno de los datos ingresados no es correcto", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Function ValidateIndexToAdd() As Boolean
        If cmbIndiceJerarquico.SelectedValue Is Nothing Then
            Return False
        End If

        Return True
    End Function

    Private Sub btnGenerarIndiceJerarquico_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerarIndiceJerarquico.Click
        'convertir el item en jerarquico
        If MessageBox.Show("Esta accion convertira al indice en jerarquico, ¿esta seguro?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            IndexsBusiness.SetIndexDropDown(_myIndex.ID, _indexType)
            _myIndex.DropDown = _indexType
            EnableAndFillControls()
        End If
    End Sub

    Private Sub btnEliminarIndiceJerarquico_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarIndiceJerarquico.Click
        Dim ParentNameId As Int32
        Dim NewType As IndexAdditionalType

        If MessageBox.Show("Esta accion eliminara el indice jerarquico y todas sus asociaciones, ¿esta seguro?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            If _indexType = IndexAdditionalType.DropDownJerarquico Then
                NewType = IndexAdditionalType.DropDown
            ElseIf _indexType = IndexAdditionalType.AutoSustituciónJerarquico Then
                NewType = IndexAdditionalType.AutoSustitución
            End If

            IndexsBusiness.SetIndexDropDown(_myIndex.ID, NewType)
            _myIndex.DropDown = NewType

            IndexsBusiness.DeleteIndexListJerarquico(_myIndex.ID, _myIndex.HierarchicalParentID)

            EnableAndFillControls()
            RaiseEvent IndexUpdated()
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub BtnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnInsert.Click
        Try
            Dim frmInsertItem As frmInsertItemListaSustitucion
            frmInsertItem = New frmInsertItemListaSustitucion(_myIndex.ID, GetExistingCodes(), True)
            AddHandler frmInsertItem.NewItem, AddressOf AddItem
            frmInsertItem.ShowDialog()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Function GetExistingCodes() As Generic.List(Of String)
        Dim Codes As New Generic.List(Of String)

        For i As Integer = 0 To dgHierarchicalValues.VisibleRowCount - 2
            Codes.Add(dgHierarchicalValues.Item(i, 0).ToString & "|" _
                      & dgHierarchicalValues.Item(i, 1).ToString)
        Next

        Return Codes
    End Function

    Private Sub AddItem(ByVal Indice As Int32, ByVal ChildValue As String, ByVal ParentValue As String)
        IndexsBusiness.AddHierarchyItem(_myIndex.ID, _myIndex.HierarchicalParentID, ChildValue, ParentValue)
        dgHierarchicalValues.DataSource = IndexsBusiness.GetHierarchicalTable(_myIndex.ID, _myIndex.HierarchicalParentID)
    End Sub

    Private Sub btEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEliminar.Click
        Dim SelectedValues As New List(Of String)
        For i As Integer = dgHierarchicalValues.VisibleRowCount - 1 To 0 Step -1
            If dgHierarchicalValues.IsSelected(i) Then
                SelectedValues.Add(dgHierarchicalValues.Item(i, 0).ToString & _
                                      "|" & dgHierarchicalValues.Item(i, 1).ToString)
            End If
        Next

        IndexsBusiness.DeleteHierarchyValues(SelectedValues, _myIndex.ID, _myIndex.HierarchicalParentID)
        dgHierarchicalValues.DataSource = IndexsBusiness.GetHierarchicalTable(_myIndex.ID, _myIndex.HierarchicalParentID)
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        Dim frmUpdate As New frmInsertItemListaSustitucion(_myIndex.ID)
        RemoveHandler frmInsertItemListaSustitucion.MidifiedIndex, AddressOf SaveModifiedIndex
        AddHandler frmInsertItemListaSustitucion.MidifiedIndex, AddressOf SaveModifiedIndex
        frmUpdate.txtCodigo.Text = dgHierarchicalValues.Item(dgHierarchicalValues.CurrentRowIndex, 0).ToString
        frmUpdate.txtDescripcion.Text = dgHierarchicalValues.Item(dgHierarchicalValues.CurrentRowIndex, 1).ToString
        frmUpdate.Label1.Text = "Valor padre"
        frmUpdate.Label3.Text = "Valor hijo"
        frmUpdate.cmdAceptar.Visible = False
        frmUpdate.btnModify.Visible = True
        frmUpdate.ShowDialog()
        If frmUpdate.DialogResult = DialogResult.OK Then
            frmUpdate.btnModify.Visible = False
            frmUpdate.Dispose()
        End If
    End Sub

    Private Sub SaveModifiedIndex(ByVal ParentValue As String, ByVal ChildValue As String)
        Dim selectedIndex As Integer
        For i As Integer = dgHierarchicalValues.VisibleRowCount - 1 To 0 Step -1
            If dgHierarchicalValues.IsSelected(i) Then
                selectedIndex = i
            End If
        Next

        IndexsBusiness.ModifyHierarchyValue(_myIndex.ID, _myIndex.HierarchicalParentID, _
                                            dgHierarchicalValues.Item(selectedIndex, 0), _
                                            dgHierarchicalValues.Item(selectedIndex, 1), _
                                            ParentValue, ChildValue)

        dgHierarchicalValues.DataSource = IndexsBusiness.GetHierarchicalTable(_myIndex.ID, _myIndex.HierarchicalParentID)
    End Sub
End Class