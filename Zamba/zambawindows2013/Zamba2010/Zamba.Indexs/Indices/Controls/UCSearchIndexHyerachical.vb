Imports Zamba.Core
Imports System.Collections.Generic
Imports Zamba.Data

Public Class UCSearchIndexHyerachical

    Inherits ZControl

    Dim _myIndex As New Index
    Dim _indexType As IndexAdditionalType

    Public Event IndexUpdated()

    Public Sub New(ByVal Index As Index, ByVal Type As IndexAdditionalType)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _myIndex = Index
        _myIndex.DropDown = IndexsBussinesExt.GetIndexDropDownType(_myIndex.ID)
        _indexType = Type

        EnableAndFillControls()
    End Sub

    Private Sub EnableAndFillControls()

        If (_myIndex.DropDown <> IndexAdditionalType.DropDownJerarquico AndAlso _
            _myIndex.DropDown <> IndexAdditionalType.AutoSustituciónJerarquico) Then
            btnGenerarIndiceJerarquico.Visible = True
            pnlAsociacionIndices.Visible = False
            lblAsociadoCon.Visible = False
            Panel2.Visible = True
        Else
            btnGenerarIndiceJerarquico.Visible = False
            pnlAsociacionIndices.Visible = True

            Dim ParentName As String
            ParentName = IndexsBusiness.getTableListParentName(_myIndex.ID)

            If Not String.IsNullOrEmpty(ParentName) Then
                lblAsociadoCon.Text = "Atributo asociado con " & ParentName
                lblAsociadoCon.Visible = True
                btnEliminarIndiceJerarquico.Visible = True
                GetHierarchicalTable(_myIndex.ID, _myIndex.HierarchicalParentID)
                dgHierarchicalValues.Visible = True
                pnlAsociacionIndices.Visible = True
            Else
                lblAsociadoCon.Visible = False
                Panel2.Visible = True
                pnlAsociacionIndices.Visible = False
            End If

            'SetDataTableNameControl()

        End If

    End Sub

    Public Sub InsertIndicesBusquedaJerarquicos(ByVal ParentID As Integer)
        Try
            Dim indexParentId As Int32 = ParentID
            'Dim indexDataTableName As String = "Hierarchy_I" & cmbIndiceJerarquico.SelectedValue.ToString() _
            '    & "_I" & _myIndex.ID

            'Armar la asociacion de indices
            If IndexsBussinesExt.InsertIndexListJerarquico(_myIndex.ID, indexParentId, False) Then
                _myIndex.HierarchicalParentID = indexParentId
                If _myIndex.DropDown = IndexAdditionalType.AutoSustitución OrElse _myIndex.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                    _indexType = IndexAdditionalType.AutoSustituciónJerarquico
                ElseIf _myIndex.DropDown = IndexAdditionalType.DropDown OrElse _myIndex.DropDown = IndexAdditionalType.DropDownJerarquico Then
                    _indexType = IndexAdditionalType.DropDownJerarquico
                End If

                IndexsBusiness.SetIndexDropDown(_myIndex.ID, _indexType)
                _myIndex.DropDown = _indexType
                UserBusiness.Rights.SaveAction(_myIndex.ID, ObjectTypes.Index, RightsType.Edit, _myIndex.Name)
                EnableAndFillControls()
                RaiseEvent IndexUpdated()

                EnableAndFillControls()
            Else
                MessageBox.Show("No se ha podido establecer la jerarquía, por favor compruebe que los atributos no posean hijos ni padres previos.", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Ocurrio un error al actualizar la tabla de Búsqueda Jerarquica", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    'Private Sub btnAsociateIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If ValidateIndexToAdd() Then
    '            InsertIndicesBusquedaJerarquicos()
    '            EnableAndFillControls()
    '            RaiseEvent IndexUpdated()
    '        Else
    '            MessageBox.Show("Alguno de los datos ingresados no es correcto", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End If
    '    Catch ex As Exception
    '        Zamba.Core.ZClass.raiseerror(ex)
    '    End Try
    'End Sub

    'Private Function ValidateIndexToAdd() As Boolean
    '    If cmbIndiceJerarquico.SelectedValue Is Nothing Then
    '        Return False
    '    End If

    '    Return True
    'End Function

    Private Sub btnGenerarIndiceJerarquico_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnGenerarIndiceJerarquico.Click
        'convertir el item en jerarquico
        If MessageBox.Show("Esta accion convertira al indice en jerarquico, ¿esta seguro?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim frmSeleccion As frmHierarchyAttributeSelector
            frmSeleccion = New frmHierarchyAttributeSelector()
            AddHandler frmSeleccion.ParentSelected, AddressOf InsertIndicesBusquedaJerarquicos
            frmSeleccion.ShowDialog()
        End If
    End Sub


    Private Shared HIERARCHICALTABLENAME As String = "ZHierarchy_I{0}_I{1}"

    ''' <summary>
    ''' Borra el indice como jerarquico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEliminarIndiceJerarquico_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEliminarIndiceJerarquico.Click
        Dim ParentNameId As Int32
        Dim NewType As IndexAdditionalType

        Try
            If MessageBox.Show("¿Desea quitar la jerarquia y sus asociaciones al atributo?", "Zamba", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                If _indexType = IndexAdditionalType.DropDownJerarquico Then
                    NewType = IndexAdditionalType.DropDown
                ElseIf _indexType = IndexAdditionalType.AutoSustituciónJerarquico Then
                    NewType = IndexAdditionalType.AutoSustitución
                End If

                Dim DataTableName As String = String.Format(HIERARCHICALTABLENAME, _myIndex.HierarchicalParentID, _myIndex.ID)

                If Indexs_Factory.ValidateHierarchicalValueTable(DataTableName) Then

                    IndexsBusiness.DeleteIndexListJerarquico(_myIndex.ID, _myIndex.HierarchicalParentID)

                Else


                End If

                IndexsBusiness.SetIndexDropDown(_myIndex.ID, NewType)
                _myIndex.DropDown = NewType

                EnableAndFillControls()
                RaiseEvent IndexUpdated()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Ha ocurrido un error al intentar eliminar el atributo jerarquico", "Atencion")
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub BtnInsert_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnInsert.Click
        Try
            Dim frmInsertItem As New frmInsertItemJerarquico(_myIndex.ID, GetExistingCodes(), _myIndex.HierarchicalParentID)
            AddHandler frmInsertItem.NewItem, AddressOf AddItem
            frmInsertItem.ShowDialog()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Function GetExistingCodes() As List(Of String)
        Dim Codes As New List(Of String)

        For Each Dr As DataGridViewRow In dgHierarchicalValues.Rows
            Codes.Add(Dr.Cells(0).Value.ToString() & "|" & Dr.Cells(2).Value.ToString())
        Next

        Return Codes
    End Function

    Private Sub AddItem(ByVal Indice As Int32, ByVal ChildValue As String, ByVal ParentValue As String)
        IndexsBusiness.AddHierarchyItem(_myIndex.ID, _myIndex.HierarchicalParentID, ChildValue, ParentValue)
        GetHierarchicalTable(_myIndex.ID, _myIndex.HierarchicalParentID)
    End Sub

    Private Sub btEliminar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btEliminar.Click
        Dim SelectedValues As New List(Of String)
        If dgHierarchicalValues.SelectedRows.Count = 1 Then
            SelectedValues.Add(dgHierarchicalValues.SelectedRows(0).Cells(0).Value.ToString() & "|" & dgHierarchicalValues.SelectedRows(0).Cells(2).Value.ToString().ToString())

            IndexsBusiness.DeleteHierarchyValues(SelectedValues, _myIndex.ID, _myIndex.HierarchicalParentID)
            GetHierarchicalTable(_myIndex.ID, _myIndex.HierarchicalParentID)
        Else
            MessageBox.Show("No ha seleccionado ninguna fila para eliminar", "Atencion")
        End If
    End Sub

    ''' <summary>
    ''' Obtiene la tabla de relaciones existentes
    ''' </summary>
    ''' <param name="childID"></param>
    ''' <param name="ParentID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Sub GetHierarchicalTable(ByVal childID As Int64, ByVal ParentID As Int64)
        Dim ibExt As New IndexsBussinesExt()

        Try
            dgHierarchicalValues.DataSource = ibExt.GetHierarchicalTable(_myIndex.ID, _myIndex.HierarchicalParentID)
            If dgHierarchicalValues.Columns.Count > 1 Then
                dgHierarchicalValues.Columns(0).Width = 70
                dgHierarchicalValues.Columns(1).Width = 120
                dgHierarchicalValues.Columns(2).Width = 70
                dgHierarchicalValues.Columns(3).Width = 120
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            ibExt = Nothing
        End Try
    End Sub
End Class
