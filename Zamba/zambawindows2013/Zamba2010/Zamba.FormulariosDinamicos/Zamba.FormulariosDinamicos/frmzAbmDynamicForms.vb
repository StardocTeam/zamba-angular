Imports Zamba.Core
'Imports Zamba.Controls

Public Class frmAbmDynamicForms

    'Especifica el accionar del formulario (true:editar//false:agregar)
    Private dsIndexs As DataSet
    Private _state As DynamicFormState
    Private Secciones1 As New List(Of Section)
    Private frmIndexEdition As frmABMZfrmIndexsDesc
    Dim frmSections As frmAbmSections
    Private bnClose As Boolean = True
    Private OrderByStr As String
    Private Const CONS_HELP_MSJ As String = "Permite agregar y ubicar atributos dentro del formulario dinamico, Se puede agregar en distintas filas y distintas columnas, Luego desde la grilla de visualizacion se permite ordenar en su aparicion, por columnas y por secciones"
#Region "ctr"

    Public Sub New(ByRef state As DynamicFormState)
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _state = state
    End Sub

#End Region

    Private Sub LoadData()

        Try

            If Not IsNothing(dsIndexs) Then
                dsIndexs.Clear()
            End If

            dgrvValuesList.Rows.Clear()
            Secciones1.Clear()

            'Cargo los atributos guardados en la tabla anterior
            dsIndexs = IndexsBusiness.GetIndexSchemaAsDataSet(_state.Doctypeid)
            cmbIndex.DataSource = dsIndexs.Tables(0)
            cmbIndex.ValueMember = "Index_Id"
            cmbIndex.DisplayMember = "Index_Name"

            'Cargo las secciones en el cbo
            Dim Secciones As DataSet = FormBusiness.GetAllDynamicFormSections()
            If Not Secciones Is Nothing Then
                cmbSection.DataSource = Secciones.Tables(0)
                cmbSection.ValueMember = "IdSeccion"
                cmbSection.DisplayMember = "nombre"
            End If

            'en modo edicion, se carga los posibles valores del estado
            If ((_state.Edit) Or (_state.IsFormsRenavigated)) Then
                For Each Item As Hashtable In _state.DynamicFormValues
                    AddItem(Item.Item("IndexId"), IndexsBusiness.GetIndexName(Item.Item("IndexId"), False), _
                            Item.Item("SectionId"), FormBusiness.GetDynamicFormSectionName(Item.Item("SectionId")), _
                            Item.Item("NroFila"))
                Next
            End If
            cmbIndex.SelectedIndex = 0
            '[Sebastian 19-05-09] se comento esta linea porque el control no esta cargado aún
            'con ningún valor y esto prentende seleccionar algo que no existe.
            'cmbSection.SelectedIndex = 0

            cmbOrder.SelectedIndex = 0

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Carga el listado de atributos ingresados en el AMB anterior.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history> [sebastian 24-02-2009]Carga el listado de atributos
    '''           [Tomas] 26/02/09      Se cargan los atributos ingresados en el AMB anterior y las secciones.
    '''           [Tomas 10/03/09]      Se cargan los valores del combo desde un dataSet.       
    '''           [Gaston]	30/03/2009	Modified   Dependiendo desde donde se haya invocado este formulario se muestra o no el botón "Atrás"   
    ''' </history>
    Private Sub frmAbmDynamicForms_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load

        Text = "Opciones de visualización - " & _state.FormName

        LoadData()

    End Sub

    ''' <summary>
    ''' Muestra el formulario para agregar secciones. 
    ''' Si se agregan secciones y se presiona Aceptar, se agregan a la base de datos y 
    ''' se refresca el combo mostrando las nuevas secciones agregadas.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Tomas 13/03/09]   Created 
    ''' </history>
    Private Sub btnAddSections_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAddSections.Click

        'Abro el formulario
        frmSections = New frmAbmSections
        RemoveHandler frmSections.SectionEdited, AddressOf RefreshSectionName
        AddHandler frmSections.SectionEdited, AddressOf RefreshSectionName
        frmSections.ShowDialog()

        If (frmSections.DialogResult = DialogResult.OK) Then

            'Cargo las secciones en el cbo
            Dim Secciones As DataSet = FormBusiness.GetAllDynamicFormSections()
            If Not Secciones Is Nothing Then
                cmbSection.DataSource = Secciones.Tables(0)
                cmbSection.ValueMember = "IdSeccion"
                cmbSection.DisplayMember = "nombre"
                cmbSection.Refresh()
            End If
        End If

    End Sub

    Private Sub RefreshSectionName(ByVal sectionid As Int64, ByVal newSectionName As String)

        For Each row As DataGridViewRow In dgrvValuesList.Rows
            If row.Cells("SectionId").Value = sectionid Then
                row.Cells("SectionName").Value = newSectionName
            End If
        Next

    End Sub

    Private Sub AddItem(ByVal indexid As Int64, ByVal indexname As String, ByVal sectionid As Int64, ByVal sectionname As String, ByVal order As String)
        Dim lastindex As Int32 = -1

        For Each row As DataGridViewRow In dgrvValuesList.Rows
            If row.Cells(2).Value = sectionname AndAlso row.Cells(4).Value = order Then
                If row.Index > lastindex Then
                    lastindex = row.Index
                End If
            End If
        Next


        If lastindex = -1 Then
            For Each row As DataGridViewRow In dgrvValuesList.Rows
                If row.Cells(2).Value = sectionname Then
                    If row.Index > lastindex Then
                        lastindex = row.Index
                    End If
                End If
            Next
        End If


        If lastindex <> -1 Then
            dgrvValuesList.Rows.Insert(lastindex + 1, indexname.Trim, indexid, sectionname.Trim, sectionid, order)
        Else
            dgrvValuesList.Rows.Add(indexname.Trim, indexid, sectionname.Trim, sectionid, order)
        End If



        'dgrvValuesList.Rows.Add()

        'dgrvValuesList.Rows(dgrvValuesList.Rows.Count - 1).Cells("IndexId").Value = indexid
        'dgrvValuesList.Rows(dgrvValuesList.Rows.Count - 1).Cells("Index").Value = indexname.Trim
        'dgrvValuesList.Rows(dgrvValuesList.Rows.Count - 1).Cells("SectionId").Value = sectionid
        'dgrvValuesList.Rows(dgrvValuesList.Rows.Count - 1).Cells("SectionName").Value = sectionname.Trim
        'dgrvValuesList.Rows(dgrvValuesList.Rows.Count - 1).Cells("Order").Value = order


        Dim seccion As New Section(sectionid, indexid, order)
        Secciones1.Add(seccion)

        'Remueve el Atributo agregado al dgv del combo

        Dim rowscount As Int64 = dsIndexs.Tables(0).Rows.Count



        If rowscount > 0 Then
            Dim removeindex As Int64 = -1
            For index As Integer = 0 To rowscount - 1
                If dsIndexs.Tables(0).Rows(index).Item("Index_Id") = indexid Then
                    removeindex = index
                    Exit For
                End If

            Next

            If removeindex <> -1 Then
                dsIndexs.Tables(0).Rows.RemoveAt(removeindex)
            End If

        End If


        'quita las posibles selecciones en la grilla
        For Each dr As DataGridViewRow In dgrvValuesList.Rows
            For Each dc As DataGridViewCell In dr.Cells
                dc.Selected = False
            Next
        Next

        dgrvValuesList.Rows(0).Cells(0).Selected = True
    End Sub

    ''' <summary>
    ''' [sebastian 24-02-2009]Agrega los valores que se fueron configurando a la grilla para luego ser grabados en la 
    ''' base de datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Tomas 10/03/09]   Modified   Se remueve la fila seleccionada del dataSet.
    ''' </history>
    Private Sub tnAddToList_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAddToList.Click

        Try

            If cmbIndex.Items.Count <> 0 Then
                'Agrega al DGV
                AddItem(cmbIndex.SelectedValue, cmbIndex.Text, _
                        cmbSection.SelectedValue, cmbSection.Text, _
                        cmbOrder.Text)
            End If

            'guarda valores en el estado
            SaveValuesToState()
            cmbOrder.SelectedIndex = 0

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' [sebastian 24-02-2009]Remueve cualquier valor de la grilla seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Tomas 10/03/09] Modified - Se modifica para que no acepte 2 atributos.
    ''' </history>
    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnRemove.Click

        'se agrego try [sebastian 16-03-2009]
        Try

            'Valido que existan filas seleccionad7as
            If dgrvValuesList.Rows.Count <> 0 Then
                Dim selectedRows As New List(Of DataGridViewRow)

                For Each cell As DataGridViewCell In dgrvValuesList.SelectedCells
                    selectedRows.Add(dgrvValuesList.Rows(cell.RowIndex))
                Next



                'Por cada fila seleccionada del DGV
                For Each dgvr As DataGridViewRow In selectedRows

                    'Agrego una fila al dsIndices con los valores del Atributo removido
                    dsIndexs.Tables(0).Rows.Add()
                    dsIndexs.Tables(0).Rows(dsIndexs.Tables(0).Rows.Count - 1).Item("Index_Id") = _
                        CType(dgvr.Cells("IndexId").Value, Int64)
                    dsIndexs.Tables(0).Rows(dsIndexs.Tables(0).Rows.Count - 1).Item("Index_Name") = _
                        CType(dgvr.Cells("Index").Value, String).Trim

                    Secciones1.RemoveAt(dgvr.Index)

                    'Remuevo el Atributo del DGV
                    dgrvValuesList.Rows.Remove(dgvr)

                Next
                Dim dv As DataView = dsIndexs.Tables(0).DefaultView
                dv.Sort = "Index_Name ASC"
                cmbIndex.DataSource = dv

                'guarda valores en el estado
                SaveValuesToState()

            End If

            cmbOrder.SelectedIndex = 0

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub cmbSection_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmbSection.SelectedIndexChanged

        cmbOrder.SelectedIndex = 0
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón "Cancelar"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	06/04/2009	Created  
    ''' </history>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancel.Click
        closeForms()
        DialogResult = DialogResult.Cancel
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta antes de cerrar el formulario
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	30/03/2009	Created 
    ''' </history>
    Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)

        If (bnClose = True) Then
            closeForms()
        End If

    End Sub

    ''' <summary>
    ''' [sebastian 24-02-2009]Graba los valores que se cargaron en la grilla en la base de datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	30/03/2009	Modified     Si se muestra el formulario siguiente por primera vez entonces se crea la instancia, sino, se
    '''                                          retorna dicha instancia
    ''' </history>
    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAccept.Click

        Try

            If (Secciones1.Count <> 0) Then
                btnAccept.Enabled = False

                SaveValuesToState()

                ' Se oculta el formulario
                Hide()

                If (IsNothing(frmIndexEdition)) Then

                    ' Llamo al formulario que define los atributos
                    frmIndexEdition = New frmABMZfrmIndexsDesc(_state)

                    RemoveHandler frmIndexEdition.showPreviousForm, AddressOf showForm
                    RemoveHandler frmIndexEdition.closeForms_, AddressOf closeForms
                    AddHandler frmIndexEdition.showPreviousForm, AddressOf showForm
                    AddHandler frmIndexEdition.closeForms_, AddressOf closeForms

                    ' Se ejecuta el código del load del formulario
                    frmIndexEdition.ShowDialog()

                Else

                    frmIndexEdition.ShowDialog()
                    ' frmIndexEdition.Show()
                    ' Se comento el Show y se utilizo el ShowDialog debido al siguiente caso:
                    ' 1 - Se presiona el botón "Siguiente" y se pasa al próximo formulario
                    ' 2 - Se presiona el botón "Atrás" para volver al formulario anterior
                    ' 3 - Se vuelve a presionar el botón "Siguiente", pero por alguna razón el siguiente formulario no se muestra en la pantalla, 
                    '     en su lugar aparece en la barra de tareas. Con el ShowDialog este problema se corrige, apareciendo el formulario en la 
                    '     correspondiente pantalla

                End If

                'Llamo al formulario que define los atributos
                'Dim frmIndexEdition As frmABMZfrmIndexsDesc = New frmABMZfrmIndexsDesc(_state)
                'frmIndexEdition.ShowDialog()

            Else
                RemoveHandler btnAddToList.Click, AddressOf RemoveErrorIcon
                AddHandler btnAddToList.Click, AddressOf RemoveErrorIcon
                ErrorProvider1.SetError(btnAddToList, "Debe agregar un Atributo como mínimo.")
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            btnAccept.Enabled = True
        End Try

    End Sub

    Private Sub RemoveErrorIcon(ByVal sender As Object, ByVal e As EventArgs)
        ErrorProvider1.Dispose()
        RemoveHandler btnAddToList.Click, AddressOf RemoveErrorIcon
    End Sub

    Private Sub SaveValuesToState()
        _state.DynamicFormValues.Clear()
        Dim orden As Int32 = 0
        Dim QueryValues As Hashtable
        'Agrego, modifico y elimino los nuevos atributos
        For Each Row As DataGridViewRow In dgrvValuesList.Rows
            orden += 1
            QueryValues = New Hashtable()
            QueryValues.Add("QueryId", _state.Formid.ToString())
            QueryValues.Add("IndexId", Row.Cells("IndexId").Value.ToString.Trim)
            QueryValues.Add("SectionId", Row.Cells("SectionId").Value.ToString.Trim)
            QueryValues.Add("NroFila", Row.Cells("NroFila").Value.ToString.Trim)
            QueryValues.Add("NroOrden", orden)
            _state.DynamicFormValues.Add(QueryValues)
        Next

    End Sub

    ''' <summary>
    ''' Método que muestra el formulario (sólo cuando en el formulario siguiente se presiona el botón "Atrás")
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	30/03/2009	Created 
    ''' </history>
    Private Sub showForm()
        Show()
    End Sub

    ''' <summary>
    ''' Método que cierra los formularios, ya sea el o los formularios siguientes y el formulario actual
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	30/03/2009	Created 
    '''     [Gaston]    03/04/2009  Modified    Se agrego una bandera necesaria para prevenir que se ejecute el código de OnClosing
    ''' </history>
    Private Sub closeForms()

        bnClose = False

        If (Not (IsNothing(frmIndexEdition))) Then
            frmIndexEdition.closeForms(False)
            frmIndexEdition = Nothing
        End If

        DialogResult = DialogResult.OK
        Close()
        Dispose()

    End Sub


    Private Function GetNewDataTable() As DataTable
        Dim dt As New DataTable
        Dim col1 As New DataColumn(dgrvValuesList.Columns(0).Name)
        Dim col2 As New DataColumn(dgrvValuesList.Columns(1).Name)
        Dim col3 As New DataColumn(dgrvValuesList.Columns(2).Name)
        Dim col4 As New DataColumn(dgrvValuesList.Columns(3).Name)
        Dim col5 As New DataColumn(dgrvValuesList.Columns(4).Name)
        dt.Columns.Add(col1)
        dt.Columns.Add(col2)
        dt.Columns.Add(col3)
        dt.Columns.Add(col4)
        dt.Columns.Add(col5)
        Return dt
    End Function
    Private Sub OrderDownBySection(ByRef dt As DataTable)

        Dim firtselectedrowid As Int32 = 99999999
        Dim lastselectedrowid As Int32 = -1

        For Each cell As DataGridViewCell In dgrvValuesList.SelectedCells
            If cell.RowIndex > lastselectedrowid Then
                lastselectedrowid = cell.RowIndex
            End If

            If cell.RowIndex < firtselectedrowid Then
                firtselectedrowid = cell.RowIndex
            End If
        Next



        If lastselectedrowid = dgrvValuesList.Rows.Count - 1 Then
            MessageBox.Show("La seccion ya se encuentra en el nivel inferior", "Zamba")
            Exit Sub
        End If


        Dim firtMoveselectedrowid As Int32 = 99999999
        Dim lastMoveselectedrowid As Int32 = -1
        Dim moveSectionname As String = String.Empty


        For index As Integer = lastselectedrowid + 1 To dgrvValuesList.Rows.Count - 1

            If moveSectionname = String.Empty Then
                moveSectionname = dgrvValuesList.Rows(index).Cells(2).Value
            End If
            If moveSectionname = dgrvValuesList.Rows(index).Cells(2).Value Then

                If index > lastMoveselectedrowid Then
                    lastMoveselectedrowid = index
                End If

                If index < firtMoveselectedrowid Then
                    firtMoveselectedrowid = index
                End If
            End If
        Next




        Dim selecteddgRows As New List(Of DataGridViewRow)
        Dim moverows As Boolean = True
        For Each dgrow As DataGridViewRow In dgrvValuesList.Rows

            If dgrow.Index < firtselectedrowid Then
                dt.Rows.Add(dgrow.Cells(0).Value, dgrow.Cells(1).Value, dgrow.Cells(2).Value, dgrow.Cells(3).Value, dgrow.Cells(4).Value)
            ElseIf dgrow.Index >= firtselectedrowid AndAlso dgrow.Index <= lastselectedrowid Then
                selecteddgRows.Add(dgrow)
            ElseIf dgrow.Index >= firtMoveselectedrowid AndAlso dgrow.Index <= lastMoveselectedrowid Then

                dt.Rows.Add(dgrow.Cells(0).Value, dgrow.Cells(1).Value, dgrow.Cells(2).Value, dgrow.Cells(3).Value, dgrow.Cells(4).Value)

                If dgrow.Index = lastMoveselectedrowid Then
                    For Each moverow As DataGridViewRow In selecteddgRows
                        dt.Rows.Add(moverow.Cells(0).Value, moverow.Cells(1).Value, moverow.Cells(2).Value, moverow.Cells(3).Value, moverow.Cells(4).Value)
                    Next
                End If

            Else
                dt.Rows.Add(dgrow.Cells(0).Value, dgrow.Cells(1).Value, dgrow.Cells(2).Value, dgrow.Cells(3).Value, dgrow.Cells(4).Value)
            End If

        Next
    End Sub
    Private Sub OrderUpBySection(ByRef dt As DataTable)
        Dim firtselectedrowid As Int32 = 99999999
        Dim lastselectedrowid As Int32 = -1

        For Each cell As DataGridViewCell In dgrvValuesList.SelectedCells
            If cell.RowIndex > lastselectedrowid Then
                lastselectedrowid = cell.RowIndex
            End If

            If cell.RowIndex < firtselectedrowid Then
                firtselectedrowid = cell.RowIndex
            End If
        Next




        If firtselectedrowid = 0 Then
            MessageBox.Show("la seccion ya se encuentra en el nivel superior", "Zamba")
            Exit Sub
        End If

        Dim firtMoveselectedrowid As Int32 = 99999999
        Dim lastMoveselectedrowid As Int32 = -1
        Dim moveSectionname As String = String.Empty


        For index As Integer = 0 To firtselectedrowid
            Dim sectionname As String = dgrvValuesList.Rows(index).Cells(2).Value
            Dim selectedsectionname = dgrvValuesList.Rows(firtselectedrowid).Cells(2).Value


            If moveSectionname = String.Empty Then
                moveSectionname = sectionname
            End If
            If moveSectionname = sectionname Then

                If index > lastMoveselectedrowid Then
                    lastMoveselectedrowid = index
                End If

                If index < firtMoveselectedrowid Then
                    firtMoveselectedrowid = index
                End If
            ElseIf sectionname <> selectedsectionname Then
                moveSectionname = sectionname
                firtMoveselectedrowid = 99999999
                lastMoveselectedrowid = -1
                If index > lastMoveselectedrowid Then
                    lastMoveselectedrowid = index
                End If

                If index < firtMoveselectedrowid Then
                    firtMoveselectedrowid = index
                End If
            End If
        Next




        Dim selecteddgRows As New List(Of DataGridViewRow)
        Dim moverows As Boolean = True

        Dim rowstomove As New List(Of DataGridViewRow)
        For index As Integer = firtselectedrowid To lastselectedrowid
            rowstomove.Add(dgrvValuesList.Rows(index))
        Next


        For Each dgrow As DataGridViewRow In dgrvValuesList.Rows

            If dgrow.Index < firtMoveselectedrowid Then
                dt.Rows.Add(dgrow.Cells(0).Value, dgrow.Cells(1).Value, dgrow.Cells(2).Value, dgrow.Cells(3).Value, dgrow.Cells(4).Value)
            ElseIf dgrow.Index >= firtMoveselectedrowid AndAlso dgrow.Index <= firtMoveselectedrowid Then

                If Not IsNothing(rowstomove) Then
                    For Each r As DataGridViewRow In rowstomove
                        dt.Rows.Add(r.Cells(0).Value, r.Cells(1).Value, r.Cells(2).Value, r.Cells(3).Value, r.Cells(4).Value)
                    Next
                    rowstomove = Nothing
                End If


                dt.Rows.Add(dgrow.Cells(0).Value, dgrow.Cells(1).Value, dgrow.Cells(2).Value, dgrow.Cells(3).Value, dgrow.Cells(4).Value)

            ElseIf dgrow.Index >= firtselectedrowid AndAlso dgrow.Index <= lastselectedrowid Then

            Else
                dt.Rows.Add(dgrow.Cells(0).Value, dgrow.Cells(1).Value, dgrow.Cells(2).Value, dgrow.Cells(3).Value, dgrow.Cells(4).Value)
            End If

        Next
    End Sub
    Private Sub OrderUpByFila(ByRef dt As DataTable)

        Dim selectedrowindex As Int32 = dgrvValuesList.SelectedCells(0).RowIndex

        If selectedrowindex = 0 Then
            MessageBox.Show("el atributo ya se encuentra en el nivel superior", "Zamba")
            Exit Sub
        End If

        Dim selectedrow As DataGridViewRow = dgrvValuesList.Rows(selectedrowindex)

        Dim previoussectionname As String = dgrvValuesList.Rows(selectedrowindex - 1).Cells(2).Value
        Dim actualsectionname As String = selectedrow.Cells(2).Value
        Dim previousFilaID As String = dgrvValuesList.Rows(selectedrowindex - 1).Cells(4).Value
        Dim actualfilaid As String = dgrvValuesList.Rows(selectedrowindex).Cells(4).Value


        If previoussectionname = actualsectionname AndAlso previousFilaID = actualfilaid Then
            For Each row As DataGridViewRow In dgrvValuesList.Rows
                If row.Index = selectedrowindex - 1 Then
                    dt.Rows.Add(selectedrow.Cells(0).Value, selectedrow.Cells(1).Value, selectedrow.Cells(2).Value, selectedrow.Cells(3).Value, selectedrow.Cells(4).Value)
                End If

                If row.Index <> selectedrowindex Then
                    dt.Rows.Add(row.Cells(0).Value, row.Cells(1).Value, row.Cells(2).Value, row.Cells(3).Value, row.Cells(4).Value)
                End If
            Next
        Else
            MessageBox.Show("Se ordenara en el ambito de la seccion y fila", "Información")
        End If
    End Sub
    Private Sub btnOrderUp_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnOrderUp.Click
        Try
            Dim dt As DataTable
            dt = GetNewDataTable()

            If OrderByStr.ToUpper = "SECCION" Then
                MessageBox.Show("Ordenará por seccion", "Información")
                OrderUpBySection(dt)
            ElseIf OrderByStr.ToUpper = "FILA" Then
                MessageBox.Show("Ordenará por fila", "Información")
                OrderUpByFila(dt)
            Else
                MessageBox.Show("Seleccion Seccion o Fila para su ordenamiento")
                Exit Sub
            End If




            If dt.Rows.Count > 0 Then
                dgrvValuesList.Rows.Clear()

                'agrega los items a la grilla
                For Each dr As DataRow In dt.Rows
                    AddItem(dr.Item("IndexId"), IndexsBusiness.GetIndexName(dr.Item("IndexId"), False), _
                            dr.Item("SectionId"), FormBusiness.GetDynamicFormSectionName(dr.Item("SectionId")), _
                            dr.Item("NroFila"))
                Next

                'guarda los items en el estado
                SaveValuesToState()
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub OrderDownByFila(ByRef dt As DataTable)
        Dim selectedrowindex As Int32 = dgrvValuesList.SelectedCells(0).RowIndex

        If selectedrowindex = dgrvValuesList.Rows.Count - 1 Then
            MessageBox.Show("el atributo ya se encuentra en el nivel inferior", "Zamba")
            Exit Sub
        End If

        Dim selectedrow As DataGridViewRow = dgrvValuesList.Rows(selectedrowindex)
        Dim nextsectionname As String = dgrvValuesList.Rows(selectedrowindex + 1).Cells(2).Value
        Dim actualsectionname As String = selectedrow.Cells(2).Value
        Dim nextFilaID As String = dgrvValuesList.Rows(selectedrowindex + 1).Cells(4).Value
        Dim actualfilaid As String = dgrvValuesList.Rows(selectedrowindex).Cells(4).Value

        If nextsectionname = actualsectionname AndAlso nextFilaID = actualfilaid Then
            For Each row As DataGridViewRow In dgrvValuesList.Rows
                If row.Index <> selectedrowindex Then
                    If row.Index = selectedrowindex + 2 Then
                        dt.Rows.Add(selectedrow.Cells(0).Value, selectedrow.Cells(1).Value, selectedrow.Cells(2).Value, selectedrow.Cells(3).Value, selectedrow.Cells(4).Value)
                    End If
                    dt.Rows.Add(row.Cells(0).Value, row.Cells(1).Value, row.Cells(2).Value, row.Cells(3).Value, row.Cells(4).Value)

                End If
            Next

            If dgrvValuesList.Rows.Count <> dt.Rows.Count Then
                dt.Rows.Add(selectedrow.Cells(0).Value, selectedrow.Cells(1).Value, selectedrow.Cells(2).Value, selectedrow.Cells(3).Value, selectedrow.Cells(4).Value)
            End If

        Else
            MessageBox.Show("Se ordenara en el ambito de la seccion y fila", "Información")
        End If
    End Sub
    Private Sub btnOrderDown_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnOrderDown.Click
        Try
            Dim dt As DataTable
            dt = GetNewDataTable()


            If OrderByStr.ToUpper = "SECCION" Then
                MessageBox.Show("Ordenará por seccion", "Información")
                OrderDownBySection(dt)
            ElseIf OrderByStr.ToUpper = "FILA" Then
                MessageBox.Show("Ordenará por fila", "Información")
                OrderDownByFila(dt)
            Else
                MessageBox.Show("Seleccion Seccion o Fila para su ordenamiento")
                Exit Sub
            End If

            If dt.Rows.Count > 0 Then
                dgrvValuesList.Rows.Clear()

                'agrega los items a la grilla
                For Each dr As DataRow In dt.Rows
                    AddItem(dr.Item("IndexId"), IndexsBusiness.GetIndexName(dr.Item("IndexId"), False), _
                            dr.Item("SectionId"), FormBusiness.GetDynamicFormSectionName(dr.Item("SectionId")), _
                            dr.Item("NroFila"))
                Next

                'guarda los items en el estado
                SaveValuesToState()

            End If



        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub dgrvValuesList_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgrvValuesList.CellClick
        Try
            Select Case e.ColumnIndex

                Case 2
                    OrderByStr = "Seccion"
                    Dim selectedsection As String = dgrvValuesList.SelectedCells(0).Value
                    For Each row As DataGridViewRow In dgrvValuesList.Rows
                        If row.Cells(2).Value = selectedsection Then
                            row.Cells(2).Selected = True
                        End If
                    Next
                Case 4
                    OrderByStr = "Fila"
                Case Else
                    OrderByStr = String.Empty

            End Select
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Sub frmAbmDynamicForms_HelpButtonClicked(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked
        Help.ShowPopup(btnAddSections, CONS_HELP_MSJ, New Point(Location.X + btnAddSections.Location.X, Location.Y + btnAddSections.Location.Y))
    End Sub
End Class