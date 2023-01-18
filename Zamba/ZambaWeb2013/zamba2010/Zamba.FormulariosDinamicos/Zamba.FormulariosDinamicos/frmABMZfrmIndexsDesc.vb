Imports Zamba.Core
Imports Zamba.Core.DocTypesBusiness
Imports Zamba.Core.FormBusiness
'Imports Zamba.Controls

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.FormulariosDinamicos
''' Class	 : frmABMZfrmIndexsDesc
''' -----------------------------------------------------------------------------
''' <summary>
''' Formulario dinámico que sirve para configurar el tipo y valor de cada índice
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Gaston]	23/02/2009	Created
''' </history>
''' -----------------------------------------------------------------------------

Public Class frmABMZfrmIndexsDesc

    Dim _state As DynamicFormState
    Dim indexsCollection As New ArrayList
    Dim originalIndexsCollection As New ArrayList
    Dim typesCollection As New ArrayList
    Dim bnClose As Boolean = True
    'contador de ingresos al form, utilizado para determinar si realizo navegaciones en los forms
    Dim FormGetFocusCount As Int32
    ' Variable que contendrá una instancia del formulario de Condiciones
    Private frmAddFrmConditions As frmAbmZfrmDesc
    Private Const CONS_HELP_MSJ As String = "Permite agregar propiedades especificas a los indices dentro de un formulario dinamico, las mismas pueden ser: " + vbCrLf + "Visible (Indica si se visualiza o no)" + vbCrLf + "Solo Lectura (no Permite editar el indice)" + vbCrLf + "Requerido (Indica si su ingreso de datos debe ser obligatorio)" + vbCrLf + "Exceptuable (Intervienen dos indices, y uno de ellos va a ser requerido en el caso de que el otro no tenga datos)"

    ' [Gaston]  06/04/2009  Created  
    ' Evento que cuando se ejecuta hace que se muestre el formulario anterior 
    Public Event showPreviousForm()
    ' Evento que cuando se ejecuta hace que se cierre este formulario y el anterior
    Public Event closeForms_()

    Private Const CONS_VERDADERO As String = "Verdadero"

    ''' <summary>
    ''' Constructor que carga el ID del formulario y establece el modo de accionar del form.
    ''' </summary>
    ''' <param name="state">Estado del formulario</param>
    ''' <remarks></remarks>
    Public Sub New(ByRef state As DynamicFormState)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me._state = state

    End Sub

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta cuando se carga el formulario por primera vez
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmABMZfrmIndexsDesc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Text = "Propiedades en Indices - " & _state.FormName

        Try

            'setea colecciones y controles a estado inicial
            SetCollectionsInitialState()
            dgvData.Rows.Clear()

            ' Se cargan los combobox "Índice" y "Tipo"
            loadIndexs()
            loadTypes()

            If ((_state.Edit) Or (_state.IsFormsRenavigated)) Then
                LoadPreviousData()
                SetComboboxesDefaultValue()
            End If

            bnClose = True

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se selecciona un elemento en el combobox "Índice"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cbIndexs_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbIndexs.SelectedIndexChanged
        LoadIndexData()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se selecciona un elemento en el combobox "Tipo"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cbTypes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTypes.SelectedIndexChanged

        If Not (IsNothing((cbTypes.SelectedItem))) Then

            Dim tempType As TypeElem = cbTypes.SelectedItem

            Me.cbIndexsValue.Enabled = True
            Me.cbIndexsValue.Text = ""

            Select Case tempType.NumberElem

                Case formIndexDescriptionType.required, formIndexDescriptionType.readOnly_, formIndexDescriptionType.noVisible

                    cbIndexsValue.Items.Clear()
                    Me.GroupBox2.Visible = False
                    Me.cbIndexsValue.Visible = False

                Case formIndexDescriptionType.exceptuable

                    Me.GroupBox2.Visible = True
                    Me.cbIndexsValue.Visible = True

                    loadIndexsValue()

                    Dim indexSelected As IndexElem = Me.cbIndexs.SelectedItem

                    If (indexSelected.IndexsExceptuable.Count > 0) Then

                        ' Se eliminan del combobox que hay dentro de valor los índices que ya se encuentran en la grilla y que se seleccionaron 
                        ' para el índice que hay en el combobox "Índice"
                        For Each idIndexExceptuable As Long In indexSelected.IndexsExceptuable

                            For Each indexElem As IndexElem In Me.indexsCollection

                                If (indexElem.IndexId = idIndexExceptuable) Then
                                    Me.cbIndexsValue.Items.Remove(indexElem)
                                    Exit For
                                End If

                            Next

                        Next

                    End If

                    Me.cbIndexsValue.SelectedItem = Me.cbIndexsValue.Items(0)

            End Select

        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón "Agregar"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Try

            If ((Not (IsNothing(Me.cbIndexs.SelectedItem))) AndAlso (Not (IsNothing(Me.cbTypes.SelectedItem)))) Then

                Dim actualIndexElem As IndexElem = Me.cbIndexs.SelectedItem
                Dim actualTempType As TypeElem = Me.cbTypes.SelectedItem

                If (actualTempType.NumberElem = formIndexDescriptionType.exceptuable) Then

                    If (IsNothing(Me.cbIndexsValue.SelectedItem)) Then
                        MessageBox.Show("Se debe seleccionar un índice para asignar al índice de tipo exceptuable", "Zamba Software", MessageBoxButtons.OK)
                        Me.cbIndexsValue.Focus()
                        Exit Sub
                    End If

                End If

                AddItem(actualIndexElem, actualTempType)

                actualIndexElem = Nothing
                actualTempType = Nothing

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón "Eliminar"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	28/04/2009	Modified    Aplicación para índices exceptuables y eliminación del modificar
    '''     [Gaston]	30/04/2009	Modified    Llamada al método "updateComboboxType"
    ''' </history>
    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click

        Try

            'If (Not (IsNothing(Me.dgvData.SelectedRows(0).Cells("eId").Value))) Then

            If (MessageBox.Show("¿Está seguro que desea eliminar el índice?", "Zamba.Software", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then

                ' Si el tipo asignado para el índice es distinto a "exceptuable"
                If (Me.dgvData.SelectedRows(0).Cells("idType").Value <> formIndexDescriptionType.exceptuable) Then

                    For Each indexElem As IndexElem In Me.indexsCollection

                        If (indexElem.IndexId = Me.dgvData.SelectedRows(0).Cells("iId").Value) Then

                            ' Si el índice no estaba en el combobox "Índice" entonces se agrega al propio combobox
                            ' Si la colección del índice de tipo "exceptuable" es igual a la cantidad de índices del form. dinámico -1 
                            If ((indexElem.Types.Count = typesCollection.Count - 1) AndAlso (indexElem.IndexsExceptuable.Count = indexsCollection.Count - 1)) Then
                                cbIndexs.Items.Add(indexElem)
                            End If

                            indexElem.Types.Remove(CShort(Me.dgvData.SelectedRows(0).Cells("idType").Value))
                            Exit For
                        End If

                    Next

                    ' Sino si el tipo asignado para el índice es igual a "exceptuable"
                ElseIf (Me.dgvData.SelectedRows(0).Cells("idType").Value = formIndexDescriptionType.exceptuable) Then

                    For Each indexElem As IndexElem In Me.indexsCollection

                        If (indexElem.IndexId = Me.dgvData.SelectedRows(0).Cells("iId").Value) Then

                            ' Si el índice no estaba en el combobox "Índice" entonces se agrega al propio combobox
                            ' Si la colección del índice de tipo "exceptuable" es igual a la cantidad de índices del form. dinámico -1 
                            If ((indexElem.Types.Count = typesCollection.Count - 1) AndAlso (indexElem.IndexsExceptuable.Count = indexsCollection.Count - 1)) Then
                                cbIndexs.Items.Add(indexElem)
                            End If

                            ' Se remueve el id de índice excp. de la colección de la instancia índice
                            indexElem.IndexsExceptuable.Remove(Me.dgvData.SelectedRows(0).Cells("idValue").Value)

                            Exit For

                        End If

                    Next

                End If

                SetComboboxesDefaultValue()

                ' Si el índice seleccionado en el combobox "Índice" es igual al de la fila seleccionada entonces se actualiza el combobox "Tipo"
                If (Me.cbIndexs.Text = Me.dgvData.SelectedRows(0).Cells("indexName").Value) Then
                    updateComboboxType()
                    Me.cbTypes.SelectedItem = Me.cbTypes.Items(0)
                End If

                ' Se elimina la fila seleccionada de la grilla
                dgvData.Rows.Remove(dgvData.SelectedRows(0))

                'Agrega el item al estado
                AddItemsToFormState()

                If (cbIndexs.Enabled = False) Then
                    Me.cbIndexs.Enabled = True
                    Me.cbTypes.Enabled = True
                    Me.btnAdd.Enabled = True
                End If

                If (dgvData.Rows.Count = 0) Then
                    Me.btnRemove.Enabled = False
                End If

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

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
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        closeForms(True)

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta antes de cerrar el formulario
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	06/04/2009	Created 
    ''' </history>
    Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)

        If (bnClose = True) Then
            RaiseEvent closeForms_()
        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón "Atrás"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	06/04/2009	Created 
    ''' </history>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        _state.IsFormsRenavigated = True

        ' Se oculta el formulario actual
        Me.Hide()
        ' No se ejecuta el código que hay dentro del evento OnClosing
        bnClose = False
        ' Se ejecuta el evento que llama al formulario anterior
        RaiseEvent showPreviousForm()

    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click

        Try

            ' Agrega los items al estado
            AddItemsToFormState()

            ' Se oculta el formulario
            Me.Hide()

            If (IsNothing(frmAddFrmConditions)) Then

                frmAddFrmConditions = New frmAbmZfrmDesc(Me._state)

                RemoveHandler frmAddFrmConditions.showPreviousForm, AddressOf Me.showForm
                RemoveHandler frmAddFrmConditions.closeForms, AddressOf Me.closeForms
                AddHandler frmAddFrmConditions.showPreviousForm, AddressOf Me.showForm
                AddHandler frmAddFrmConditions.closeForms, AddressOf Me.closeForms

                frmAddFrmConditions.ShowDialog()

            Else
                frmAddFrmConditions.ShowDialog()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' setea las colecciones y controles a su estado inicial
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>dalbarellos 01.04.2009 created</history>
    Private Sub SetCollectionsInitialState()
        indexsCollection.Clear()
        originalIndexsCollection.Clear()
        typesCollection.Clear()
        cbIndexs.Items.Clear()
        cbTypes.Items.Clear()
    End Sub

    ''' <summary>
    ''' Método que sirve para guardar los índices del form. dinámico en el combobox que muestra los índices
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	06/05/2009	Modified    Se obtienen los índices seleccionados para el armado del formulario dinámico
    ''' </history>
    Private Sub loadIndexs()

        Try

            Dim temporalIndexs As New ArrayList

            ' Se obtienen los índices que se seleccionaron para el armado del formulario dinámico
            For Each item As Hashtable In _state.DynamicFormValues

                If (item.ContainsKey("IndexId")) Then

                    If Not (temporalIndexs.Contains(item.Item("IndexId"))) Then
                        temporalIndexs.Add(item.Item("IndexId"))
                    End If

                Else
                    Dim ex As New Exception("No existe la clave IndexId")
                    ZClass.raiseerror(ex)
                    Exit For
                End If

            Next

            If (temporalIndexs.Count > 0) Then

                For Each indexId As String In temporalIndexs

                    Dim tempIndex As New IndexElem(Long.Parse(indexId), IndexsBusiness.GetIndexNameById(Long.Parse(indexId)).Trim())
                    Me.cbIndexs.Items.Add(tempIndex)
                    indexsCollection.Add(tempIndex)
                    tempIndex = Nothing

                Next

                Me.cbIndexs.SelectedItem = cbIndexs.Items(0)
                Me.cbIndexs.ValueMember = "IndexName"

                temporalIndexs = Nothing

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Método que sirve para cargar los tipos y guardarlos en el combobox que muestra los tipos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub loadTypes()

        Try

            Dim collection As New ArrayList
            collection.Add("Requerido")
            collection.Add("Sólo lectura")
            collection.Add("Exceptuable")
            collection.Add("No Visible")

            ' Contador que representa el número del tipo en el enumerador
            Dim counter As Integer = 0

            For Each typeName As String In collection

                Dim tempType As New TypeElem(counter, typeName)
                Me.cbTypes.Items.Add(tempType)
                typesCollection.Add(tempType)
                counter = counter + 1
                tempType = Nothing

            Next

            Me.cbTypes.SelectedItem = cbTypes.Items(0)
            Me.cbTypes.ValueMember = "TypeName"

            collection = Nothing
            counter = Nothing

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' carga posibles datos cargados anteriormente, solo para modo edicion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     dalbarellos 01.04.2009 created
    '''     [Gaston]	30/04/2009	Modified    Funcionalidad para identificar el tipo que tiene el índice. En base al tipo se colocará un 
    '''                                         determinado valor en la columna "Valor". Verdadero, o en caso de tipo exceptuable el nombre de 
    '''                                         un índice
    ''' </history>
    Private Sub LoadPreviousData()

        Dim dgvRow As String() = Nothing
        Dim value As String = Nothing

        ' Por cada índice insertado en la tabla y que pertenece al form. dinámico
        For Each item As String() In _state.IndexsPropertiesFormValues

            For Each indexElem As IndexElem In indexsCollection

                If (indexElem.IndexId = item(1)) Then

                    value = ""

                    ' Si el tipo es igual a exceptuable
                    If (item(3) = formIndexDescriptionType.exceptuable) Then
                        ' Se agrega el id de índice a la colección de índices exceptuables del índice
                        indexElem.addIndexExceptuable(Convert.ToInt64(item(6)))
                    Else
                        ' Se agrega el tipo a la colección de tipos del elemento índice del for indicando que cuando se seleccione el índice en
                        ' el combobox índices, no aparezca el tipo en el combobox "tipos" ya que se encuentra en la grilla para este índice
                        indexElem.addType(item(3))
                    End If

                    ' Tipo de restricción
                    Select Case item(3)

                        Case formIndexDescriptionType.required, formIndexDescriptionType.readOnly_, formIndexDescriptionType.noVisible

                            If (item(6).ToString() = "1") Then
                                value = "Verdadero"
                            End If

                        Case formIndexDescriptionType.exceptuable

                            ' Se busca el nombre del índice exceptuable en base al id en la colección IndexsCollection
                            For Each indexElem_2 As IndexElem In indexsCollection

                                If (indexElem_2.IndexId = item(6)) Then
                                    value = indexElem_2.IndexName.Trim()
                                    Exit For
                                End If

                            Next

                    End Select

                    ' Id del form. dinámico, id del índice, nombre del índice, id de tipo, nombre del tipo, id del índice exceptuable o 1,
                    ' Nombre del índice exceptuable o valor "Verdadero"
                    dgvRow = New String() _
                    {_state.Formid, indexElem.IndexId, indexElem.IndexName, item(3), getTypeName(item(3)), item(6), value}

                    If Not (IsNothing(dgvRow)) Then
                        dgvData.Rows.Add(dgvRow)
                        dgvRow = Nothing
                        Exit For
                    End If

                End If

            Next

        Next

        For Each indexElem As IndexElem In indexsCollection

            ' Si la colección del índice de tipo "exceptuable" es igual a la cantidad de índices del form. dinámico -1 
            If ((indexElem.Types.Count = typesCollection.Count - 1) AndAlso (indexElem.IndexsExceptuable.Count = indexsCollection.Count - 1)) Then
                ' Se remueve dicho índice del combobox "Índice" ya que contiene todos los índices (en valor) menos él mismo
                cbIndexs.Items.Remove(indexElem)
            End If

        Next

        If (cbIndexs.Items.Count = 0) Then
            cbIndexs.Enabled = False
            cbTypes.Items.Clear()
            cbTypes.Enabled = False
            btnAdd.Enabled = False
        Else
            ' Se guardan los ìndices que se encuentran en el combobox "índice" para después, cuando se presione el botón "Cancelar" recuperarlos y
            ' colocarlos en el combobox "Índice"
            For Each iElem As IndexElem In Me.cbIndexs.Items
                originalIndexsCollection.Add(iElem)
            Next
        End If

        'Carga los tipos validos para el indice
        LoadIndexData()

        If (dgvData.Rows.Count > 0) Then
            btnRemove.Enabled = True
        Else
            btnRemove.Enabled = False
        End If

    End Sub

    ''' <summary>
    ''' carga los tipos para un indice
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     dalbarellos 06.04.2009 created
    '''     [Gaston]   28/04/2009  Modified    Aplicación para índices exceptuables 
    '''     [Gaston]   30/04/2009  Modified    Llamada al método "updateComboboxType"
    ''' </history>
    Private Sub LoadIndexData()

        If (Not (IsNothing(Me.cbIndexs.SelectedItem))) Then

            updateComboboxType()

            Dim indexSelected As IndexElem = Me.cbIndexs.SelectedItem

            ' Si el índice seleccionado ya se encuentra en la grilla y es de tipo exceptuable
            If (indexSelected.IndexsExceptuable.Count > 0) Then

                loadIndexsValue()

                ' Se eliminan del combobox que hay dentro de valor los índices que ya se encuentran en la grilla y que se seleccionaron para el 
                ' índice que hay en el combobox "Índice"
                For Each idIndexExceptuable As Long In indexSelected.IndexsExceptuable

                    For Each indexElem As IndexElem In Me.indexsCollection

                        If (indexElem.IndexId = idIndexExceptuable) Then
                            Me.cbIndexsValue.Items.Remove(indexElem)
                            Exit For
                        End If

                    Next

                Next

                If (Me.cbTypes.Items.Count > 0) Then
                    Me.cbTypes.SelectedItem = Me.cbTypes.Items(0)
                End If

                Exit Sub

            End If

            cbTypes.SelectedItem = Nothing
            cbTypes.Text = ""
            cbTypes.Enabled = True
            cbIndexsValue.Items.Clear()
            cbIndexsValue.Text = ""
            cbIndexsValue.Enabled = False

            If (Me.cbTypes.Items.Count > 0) Then
                Me.cbTypes.SelectedItem = Me.cbTypes.Items(0)
            End If

        End If

    End Sub

    ''' <summary>
    ''' Método que sirve para guardar todos los índices del form. dinámico en el combobox que hay en valor, menos el seleccionado en el combobox 
    ''' "índice"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub loadIndexsValue()

        Me.cbIndexsValue.Items.Clear()

        Dim actualIndexElem As IndexElem = Me.cbIndexs.SelectedItem

        For Each indexElem As IndexElem In Me.indexsCollection

            If (indexElem.IndexName <> actualIndexElem.IndexName) Then
                Me.cbIndexsValue.Items.Add(indexElem)
            End If

        Next

        Me.cbIndexsValue.ValueMember = "IndexName"
        actualIndexElem = Nothing

    End Sub

    ''' <summary>
    ''' Agrega un item configurado a la grilla
    ''' </summary>
    ''' <param name="actualIndexElem"></param>
    ''' <param name="actualTempType"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	28/04/2009	Modified    Aplicación para índices exceptuables
    '''     [Gaston]    30/04/2009  Modified    Llamada al método "updateControls"
    ''' </history>
    Private Sub AddItem(ByRef actualIndexElem As IndexElem, ByRef actualTempType As TypeElem)

        Dim value As String = Nothing
        ' Para identificar el id de índice colocado en valor cuando se selecciona una fila. Sólo en caso de que el tipo sea exceptuable
        Dim idValue As String = Nothing

        assignValues(value, idValue)

        If (dgvData.Rows.Count = 0) Then
            btnRemove.Enabled = False
        End If

        ' Se crea una fila y se guarda en la grilla
        Dim dgvRow As String() = New String() _
        {_state.Formid, actualIndexElem.IndexId, actualIndexElem.IndexName, actualTempType.NumberElem, actualTempType.TypeName, idValue, value}
        dgvData.Rows.Add(dgvRow)

        ' Si el tipo de elemento seleccionado para el índice es igual a exceptuable
        If (actualTempType.NumberElem = formIndexDescriptionType.exceptuable) Then

            ' Se agrega el id del índice del combobox valor a la colección de índices exceptuables del índice seleccionado en el combobox índices
            actualIndexElem.addIndexExceptuable(CType(Me.cbIndexsValue.SelectedItem, IndexElem).IndexId)

            ' Si ya se asignaron todos los índices para el índice seleccionado entonces desaparece "exceptuable" del combobox "tipos"
            If (actualIndexElem.IndexsExceptuable.Count = Me.indexsCollection.Count - 1) Then
                cbTypes.Items.RemoveAt(cbTypes.SelectedIndex)
                cbIndexsValue.Items.Clear()
                cbIndexsValue.Enabled = False
            Else

                loadIndexsValue()

                ' Se eliminan del combobox que hay dentro de valor los índices que ya se encuentran en la grilla y que se seleccionaron para el 
                ' índice que hay en el combobox "Índice"
                For Each idIndexExceptuable As Long In actualIndexElem.IndexsExceptuable

                    For Each indexElem As IndexElem In Me.indexsCollection

                        If (indexElem.IndexId = idIndexExceptuable) Then
                            Me.cbIndexsValue.Items.Remove(indexElem)
                            Exit For
                        End If

                    Next

                Next

            End If

        Else
            actualIndexElem.addType(actualTempType.NumberElem)
        End If

        ' Si ya se asignaron todos los tipos para el índice y se asignaron todos los valores (todos los índices menos el actual) para el tipo 
        ' "exceptuable"
        If ((actualIndexElem.Types.Count = typesCollection.Count - 1) AndAlso (actualIndexElem.IndexsExceptuable.Count = Me.indexsCollection.Count - 1)) Then
            ' Se elimina el índice del combobox "Índice"
            Me.cbIndexs.Items.Remove(actualIndexElem)
        End If

        ' Si ya se asignaron todos los tipos para el índice  
        If (actualIndexElem.Types.Count = typesCollection.Count) Then
            ' Se elimina el índice del combobox "Índice"
            Me.cbIndexs.Items.Remove(actualIndexElem)
        End If

        'Agrega el item al estado
        AddItemsToFormState()

        actualIndexElem = Nothing
        actualTempType = Nothing

        updateControls()

    End Sub

    ''' <summary>
    ''' Método que sirve para actualizar determinados controles, tras el agregado de elementos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	30/04/2009	Created    Parte del código de este método era de otros métodos
    ''' </history>
    Private Sub updateControls()

        Me.btnRemove.Enabled = True

        If (cbIndexs.Items.Count = 0) Then
            Me.cbIndexs.Text = ""
            Me.cbIndexs.Enabled = False
            Me.cbTypes.Enabled = False
            Me.btnAdd.Enabled = False
        End If

        value = Nothing

        ' Si el combobox "Tipo" no tiene elementos
        If (Me.cbTypes.Items.Count = 0) Then

            If (Me.cbIndexs.Items.Count > 0) Then
                ' Se selecciona el próximo índice que haya en el combobox "Índice"
                Me.cbIndexs.SelectedItem = Me.cbIndexs.Items(0)
            End If

        Else

            If ((Not IsNothing(Me.cbTypes.SelectedItem)) AndAlso (CType(Me.cbTypes.SelectedItem, TypeElem).NumberElem) = formIndexDescriptionType.exceptuable) Then
                Me.cbIndexsValue.SelectedItem = Me.cbIndexsValue.Items(0)
            Else

                If (Not IsNothing(Me.cbTypes.SelectedItem)) Then
                    Me.cbTypes.Items.RemoveAt(cbTypes.SelectedIndex)
                End If

                ' Si el combobox "Tipo" tiene elementos
                If (Me.cbTypes.Items.Count > 0) Then
                    ' Se selecciona el próximo tipo
                    Me.cbTypes.SelectedItem = Me.cbTypes.Items(0)

                    ' De lo contrario, si el combobox "Tipo" no tiene elementos quiere decir que el índice ubicado en el combobox "Índice" ya 
                    ' tiene asignado todos los tipos y por lo tanto ya no se encuentra más en dicho combobox
                Else

                    If (Me.cbIndexs.Items.Count > 0) Then
                        ' Se selecciona el próximo índice que haya en el combobox "Índice"
                        Me.cbIndexs.SelectedItem = Me.cbIndexs.Items(0)
                    End If

                End If

            End If

        End If

    End Sub

    Private Sub SetComboboxesDefaultValue()
        If cbIndexs.Items.Count > 0 Then
            cbIndexs.SelectedIndex = 0
        End If

        If cbTypes.Items.Count > 0 Then
            cbTypes.SelectedIndex = 0
        End If
    End Sub

    ''' <summary>
    ''' Método que sirve para actualizar el combobox "Tipo"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]   30/04/2009  Created   Código original proveniente de "LoadIndexData"
    ''' </history>
    Private Sub updateComboboxType()

        ' Se borran los elementos del combobox Tipo
        Me.cbTypes.Items.Clear()

        Dim iElem As IndexElem = Me.cbIndexs.SelectedItem

        For Each typeElem As TypeElem In typesCollection

            If Not (iElem.Types.Contains(typeElem.NumberElem)) Then

                If (typeElem.NumberElem = formIndexDescriptionType.exceptuable) Then

                    If (iElem.IndexsExceptuable.Count <> Me.indexsCollection.Count - 1) Then
                        cbTypes.Items.Add(typeElem)
                    End If
                Else
                    cbTypes.Items.Add(typeElem)
                End If

            End If

        Next

    End Sub

    ''' <summary>
    ''' Método que sirve para asignar los valores a la grilla
    ''' </summary>
    ''' <param name="value">Valor</param>
    ''' <param name="idValue">Id de valor</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	01/04/2009	Created
    ''' 	[Gaston]	28/04/2009	Modified
    ''' </history>
    Private Sub assignValues(ByRef value As String, ByRef idValue As String)

        ' Si el combobox que hay dentro de valor está habilitado y hay un elemento seleccionado entonces se asigna en valor el id del índice 
        ' seleccionado en dicho combo
        If ((Me.cbIndexsValue.Enabled = True) AndAlso (Not IsNothing(Me.cbIndexsValue.SelectedItem))) Then
            value = CType(Me.cbIndexsValue.SelectedItem, IndexElem).IndexName
            idValue = CType(Me.cbIndexsValue.SelectedItem, IndexElem).IndexId
            ' Sino, si el checkbox está habilitado entonces se asigna un 1 o un 0 dependiendo del estado del checkbox
        Else
            value = CONS_VERDADERO
            idValue = 1
        End If

    End Sub

    Private Function getIndexInstance(ByRef idPos As Short) As IndexElem

        Dim indexE As IndexElem = Nothing

        ' Se obtiene la instancia del índice seleccionado en la fila
        For Each IElem As IndexElem In Me.indexsCollection

            If (IElem.IndexId = Me.dgvData.SelectedRows(0).Cells("iId").Value) Then
                indexE = IElem
                Exit For
            End If

            idPos = idPos + 1

        Next

        Return (indexE)

    End Function

    Private Function getIndexsfromPreviousForm(ByVal formid As Int32) As DataSet
        Dim ds As New DataSet
        ds.Tables.Add()
        ds.Tables(0).Columns.Add("IdIndice")

        Dim IndexsList As Generic.List(Of IIndex) = IndexsBusiness.GetIndexsSchemaAsListOf(_state.Doctypeid, False)
        For Each item As Zamba.Core.Index In IndexsList
            ds.Tables(0).Rows.Add(item.ID)
        Next
        Return ds
    End Function

    ''' <summary>
    ''' Método que sirve para asignar un valor al parámetro que se recibe para más adelante utilizar el contenido de value para guardarlo en la 
    ''' base de datos
    ''' </summary>
    ''' <param name="value">String que tendrá un valor al finalizar el método </param>
    ''' <remarks></remarks>
    Private Sub assignValue(ByRef value As String)

        ' Si el combobox que hay dentro de valor está habilitado y hay un elemento seleccionado entonces se asigna en valor el id del índice 
        ' seleccionado en dicho combo
        If ((Me.cbIndexsValue.Enabled = True) AndAlso (Not IsNothing(Me.cbIndexsValue.SelectedItem))) Then
            value = CType(Me.cbIndexsValue.SelectedItem, IndexElem).IndexId
            ' Sino, si el checkbox está habilitado entonces se asigna un 1 o un 0 dependiendo del estado del checkbox
        End If

    End Sub

    ''' <summary>
    ''' Método que sirve para mostrar información asociada al índice que se selecciona en la grilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub showInformation()

        Try

            Dim indexElem As IndexElem = Nothing

            ' Se obtiene la instancia del índice seleccionado en la fila
            For Each IElem As IndexElem In Me.indexsCollection

                If (IElem.IndexId = Me.dgvData.SelectedRows(0).Cells("iId").Value) Then
                    indexElem = IElem
                    Exit For
                End If

            Next

            If Not (IsNothing(indexElem)) Then

                ' Se remueven los índices que se encuentran en el combobox "índice"
                cbIndexs.Items.Clear()
                ' Se coloca dicha instancia en el combobox "índice"
                cbIndexs.Items.Add(indexElem)
                ' El elemento seleccionado en el combobox "índice" pasa a ser el índice seleccionado en la grilla
                Me.cbIndexs.SelectedItem = indexElem
                Me.cbTypes.SelectedItem = Nothing
                Me.cbTypes.Text = ""

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Agrega un item al estado del form. (o mejor dicho actualiza la colección IndexsPropertiesFormValues)
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     dalbarellos 03.04.2009 created
    '''     [Gaston]	30/04/2009	Modified    Al crear el string se coloca como último parámetro el idvalue, y value como anteúltimo. Se agrego el Try-Catch
    ''' </history>
    Private Sub AddItemsToFormState()

        Try

            _state.IndexsPropertiesFormValues.Clear()

            For Each dr As DataGridViewRow In dgvData.Rows

                Dim idtype As Int64 = dr.Cells("idType").Value
                Dim indexid As Int64 = dr.Cells("iId").Value
                Dim idvalue As String = dr.Cells("idValue").Value
                Dim value As String = dr.Cells("value").Value

                Dim dgvRow As String() = New String() _
                {_state.Formid, indexid, IndexsBusiness.GetIndexName(indexid, False).Trim(), idtype, getTypeName(idtype), value, idvalue}
                _state.IndexsPropertiesFormValues.Add(dgvRow)

            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Function getTypeName(ByVal typeId As Integer) As String

        For Each typeElem As TypeElem In typesCollection

            If (typeElem.NumberElem = typeId) Then
                Return (typeElem.TypeName)
            End If

        Next

        Return (Nothing)

    End Function

    Private Function getIndexName(ByVal indexId As Integer) As String

        For Each indexElem As IndexElem In indexsCollection

            If (indexElem.IndexId = indexId) Then
                Return (indexElem.IndexName)
            End If

        Next

        Return (Nothing)

    End Function

    ''' <summary>
    ''' Método que muestra el formulario (sólo cuando en el formulario siguiente se presiona el botón "Atrás")
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	06/04/2009	Created 
    ''' </history>
    Private Sub showForm()
        Me.Show()
    End Sub

    ''' <summary>
    ''' Método que cierra los formularios, ya sea el o formulario siguiente, el anterior y el formulario actual
    ''' </summary>
    ''' <param name="value">Bandera que diferencia si se presiono el botón "Agregar" o "Condiciones". Necesario para ver que formularios cerrar</param>
    '''                     También para ver si se ejecuta el evento OnClosing para evitar preguntar por _state.Conditions 
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	06/04/2009	Created 
    ''' </history>
    Public Sub closeForms(ByVal value As Boolean)

        ' Cuando se cierra el formulario no se ejecuta el código que hay dentro del evento OnClosing
        Me.bnClose = False

        If (value = True) Then
            RaiseEvent closeForms_()
            Exit Sub
        End If

        If (Not (IsNothing(frmAddFrmConditions))) Then
            frmAddFrmConditions.Close()
            frmAddFrmConditions.Dispose()
            frmAddFrmConditions = Nothing
        End If

        ' Se limpian algunas variables globales
        cleanGlobalVariables()

        Me.Close()
        Me.Dispose()

    End Sub

    ''' <summary>
    ''' Método que sirve para limpiar algunas variables globales. Las variables que no se limpian son necesarias para agregar el form. dinámico y
    ''' eliminar el propio form. ABM
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	03/04/2009	Created 
    ''' </history>
    Private Sub cleanGlobalVariables()

        indexsCollection = Nothing
        originalIndexsCollection = Nothing
        typesCollection = Nothing
        FormGetFocusCount = Nothing

    End Sub

#End Region

#Region "Clases Privadas"

    Private Class IndexElem

        Private mIndexName As String
        Private mIndexId As Long
        Private mIndexsExceptuable As New ArrayList
        Private mTypes As New ArrayList

        Public Sub New(ByVal indexId As Long, ByVal indexName As String)
            mIndexId = indexId
            mIndexName = indexName
        End Sub

        Public ReadOnly Property IndexName() As String
            Get
                Return (mIndexName)
            End Get
        End Property

        Public ReadOnly Property IndexId() As String
            Get
                Return (mIndexId)
            End Get
        End Property

        Public ReadOnly Property IndexsExceptuable() As ArrayList
            Get
                Return (mIndexsExceptuable)
            End Get
        End Property

        Public ReadOnly Property Types() As ArrayList
            Get
                Return (mTypes)
            End Get
        End Property

        Public Sub addIndexExceptuable(ByVal indexExp As Long)
            mIndexsExceptuable.Add(indexExp.ToString)
        End Sub

        Public Sub addType(ByVal numberElem As Short)
            mTypes.Add(numberElem)
        End Sub

    End Class

    Private Class TypeElem

        Private mTypeName As String
        Private mNumberElem As Short

        Public Sub New(ByVal numberElem As Long, ByVal typeName As String)
            mNumberElem = numberElem
            mTypeName = typeName
        End Sub

        Public ReadOnly Property TypeName() As String
            Get
                Return (mTypeName)
            End Get
        End Property

        Public ReadOnly Property NumberElem() As Short
            Get
                Return (mNumberElem)
            End Get
        End Property

    End Class

#End Region

End Class