Imports Zamba.Core
Imports System.Text
Imports Zamba.Servers
Imports Zamba.Indexs
Imports Zamba
'Imports Zamba.Controls

Public Class frmAbmZfrmDesc

    Private _state As DynamicFormState
    Private frmDynamicForm As frmAbmDynamicForms
    Private bnClose As Boolean = True
    Private _display As DisplayindexCtl
    Dim index As Index
    Private Const CONS_HELP_MSJ As String = "Permite agregar condiciones a atributos, las mismas condicionaran el ingreso de datos a los atributos"


    ''' <summary>
    ''' Propiedad que acepta o retorna un elemento de tipo DisplayindexCtl
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	16/04/2009	Created    Propiedad tomada de la clase UCDoFillIndex
    ''' </history>
    Public Property Display() As DisplayindexCtl
        Get
            Return _display
        End Get
        Set(ByVal value As DisplayindexCtl)
            _display = value
        End Set
    End Property

    ' [Gaston]  06/04/2009  Created  
    ' Evento que cuando se ejecuta hace que se muestre el formulario anterior 
    Public Event showPreviousForm()
    ' Evento que cuando se ejecuta hace que se cierre este formulario y el anterior
    Public Event closeForms(ByVal value As Boolean)

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    ''' <summary>
    ''' Constructor de la clase frmAbmZfrmDesc
    ''' </summary>
    ''' <param name="state"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	03/03/2009	Modified  Se agrego el parámetro frmId que indica el id del formulario dinámico (Nota: ya no existe)
    ''' </history>
    Public Sub New(ByRef state As DynamicFormState)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _state = state
        grvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se carga el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	06/04/2009	Modified   Si el formulario se abrio desde con el botón "Condiciones" entonces se oculta el botón "Atrás"
    ''' </history>
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        grvList.Rows.Clear()
        Try
            Text = "Condiciones de Atributos - " & _state.FormName.ToString()

            bnClose = True

            If (Tag = "openFromBtnConditions") Then
                btnBack.Visible = False
                btnCancel.Location = New Point(302, 400)
            End If

            '[sebastian 24-02-2009] carga el listado de entidades
            Dim DocTypeList As DataSet = DocTypesBusiness.GetAllDocTypes

            ' [Tomas] 25/02/09  Carga los atributos al cboIndexs dependiendo de la entidad elegida 
            '                   El código fué adaptado de uno que había escrito Seba pero para listBox
            Dim IndexList As DataSet = IndexsBusiness.GetIndexSchemaAsDataSet(_state.Doctypeid)
            cboIndexs.DataSource = IndexList.Tables(0)
            cboIndexs.ValueMember = "Index_Id"
            cboIndexs.DisplayMember = "Index_name"

            cboOpComparacion.SelectedIndex = 0
            cboOpRelacion.SelectedIndex = 0

            'Carga los posibles valores al form, Modo Edicion
            If ((_state.Edit) Or (_state.IsFormsRenavigated)) Then
                For Each Item As Hashtable In _state.ConditionsFormValues
                    Dim conexion As String = Item.Item("CompOperator").ToString.Split("|")(1)
                    Dim compoperator As String = Item.Item("CompOperator").ToString.Split("|")(0)
                    AddItem(IndexsBusiness.GetIndexName(Item.Item("Index"), False), Item.Item("Index"), _
                            compoperator, Item.Item("Value"), _
                            conexion)
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se selecciona un elemento perteneciente al combobox "Atributos"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	16/04/2009	Created 
    ''' </history>
    Private Sub cboIndexs_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cboIndexs.SelectedIndexChanged

        Try

            If (Not IsNothing(cboIndexs.SelectedItem)) Then

                index = ZCore.GetIndex(Convert.ToInt64(CType(cboIndexs.SelectedItem, DataRowView).Item(0)))

                If (Not IsNothing(index)) Then
                    createControlOfDynamicIndex()
                End If

            End If

            ErrorProvider1.Dispose()

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub AddItem(ByVal _indexname As String, ByVal _indexId As Int64, _
                        ByVal _compoperator As String, ByVal _value As String, _
                        ByVal conexion As String)
        grvList.Rows.Add()
        grvList.Rows(grvList.Rows.Count - 1).Cells("IndexName").Value = _indexname.Trim
        grvList.Rows(grvList.Rows.Count - 1).Cells("IndexId").Value = _indexId
        grvList.Rows(grvList.Rows.Count - 1).Cells("CompOperator").Value = _compoperator
        grvList.Rows(grvList.Rows.Count - 1).Cells("Value").Value = _value
        grvList.Rows(grvList.Rows.Count - 1).Cells("Conexion").Value = conexion
    End Sub

    ''' <summary>
    ''' [sebastian 24-02-2009] Agrega al listado los valores configurados previamente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''    [Tomas] 25/02/09 Se modifica el grvList mostrando nada más los datos necesarios, el resto se obtiene de las variables globales
    '''    [Gaston]	16/04/2009	Modified  Se obtiene el valor del control que se genera dependiendo del Atributo seleccionado. Llamada a createControlOfDynamicIndex()    
    ''' </history>
    Private Sub btnAddToList_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAddToList.Click

        If (IsValidForm()) Then
            AddItem(cboIndexs.Text.TrimEnd, cboIndexs.SelectedValue, cboOpComparacion.Text, Display.Index.DataTemp.Trim(), cboOpRelacion.Text)
            SaveItemsToState()
            createControlOfDynamicIndex()
        End If

    End Sub

    ''' <summary>
    ''' Valida el contenido del formulario antes de agregar una fila al dgv.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Tomas 26/02/09] Valida el contenido del formulario antes de agregar una fila al dgv.
    '''     [Gaston]	16/04/2009	Modified    Adaptación para verificar si el control que se crea en base al Atributo está vacío o no
    ''' </history>
    Private Function IsValidForm() As Boolean

        If (Display.Index.Type <> IndexDataType.Si_No) Then

            If (String.IsNullOrEmpty(Display.Index.DataTemp.Trim())) Then
                ErrorProvider1.SetError(btnAddToList, "Ingrese un valor")
                Display.Focus()
                Return (False)
            End If

        End If

        ErrorProvider1.Dispose()
        Return (True)

    End Function

    ''' <summary>
    ''' Obtiene el máximo ID de la tabla ZFrms.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Tomas] 25/02/09 Función modificada de Seba desde Zamba.Data/Forms/FormFactory.vb</history>
    Public Shared Function GetMaxFormId() As Int64

        Dim query As New StringBuilder()
        Dim value As Int64

        query.Append("SELECT MAX(Id) from ZFrms")

        Try
            If IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, query.ToString)) = True Then
                value = 0
            Else
                Return Server.Con.ExecuteScalar(CommandType.Text, query.ToString)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)

        End Try
    End Function

    ''' <summary>
    ''' [sebastian 24-02-2009] borra de la lista de valores para la configuración.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnRemove.Click
        'Form2.Show()
        If grvList.Rows.Count <> 0 Then
            grvList.Rows.Remove(grvList.CurrentRow)
            SaveItemsToState()
        End If

    End Sub


    ''' <summary>
    ''' [sebastian 24-02-2009] limpia el text box del valor por el cual comparar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClean_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        'txtCompValue.Clear()
        panelValues.Controls.Clear()
    End Sub

    Private Sub SaveItemsToState()
        _state.ConditionsFormValues.Clear()

        Dim QueryValues As New Hashtable()

        For Each Row As DataGridViewRow In grvList.Rows

            QueryValues = New Hashtable
            QueryValues.Add("DocType", _state.Doctypeid)
            QueryValues.Add("Index", Row.Cells("IndexId").Value.ToString.Trim)
            QueryValues.Add("CompOperator", Row.Cells("CompOperator").Value.ToString.Trim + "|" + Row.Cells("conexion").Value.ToString.Trim)
            QueryValues.Add("Conexion", Row.Cells("conexion").Value.ToString.Trim)
            QueryValues.Add("Value", Row.Cells("Value").Value.ToString.Trim)
            QueryValues.Add("FormId", _state.Formid)
            _state.ConditionsFormValues.Add(QueryValues)

        Next
    End Sub
    ''' <summary>
    ''' [sebastian 24-02-2009] Guarda los valores de la lista en la base de datos.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	03/03/2009	Modified  El id del formulario dinámico es el que proviene del constructor
    '''     [Gaston]	30/03/2009	Modified  Si se muestra el formulario siguiente por primera vez entonces se crea la instancia, sino, se
    '''                                       retorna dicha instancia
    '''     [Gaston]	06/04/2009	Modified  Si el formulario se abrio con el botón "Condiciones" entonces se cierra en forma directa
    ''' </history>
    Private Sub btnFinish_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnFinish.Click

        Try

            _state.IsFinish = True
            SaveItemsToState()

            bnClose = False

            If (Tag = "openFromBtnConditions") Then
                Close()
                Dispose()
            Else
                RaiseEvent closeForms(True)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnBack.Click

        _state.IsFormsRenavigated = True

        ' Se oculta el formulario actual
        Hide()
        ' No se ejecuta el código que hay dentro del evento OnClosing
        bnClose = False
        ' Se ejecuta el evento que llama al formulario anterior
        RaiseEvent showPreviousForm()

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

        If (Tag = "openFromBtnConditions") Then
            Close()
            Dispose()
        Else
            ' Cuando se cierra el formulario no se ejecuta el código que hay dentro del evento OnClosing
            bnClose = False
            RaiseEvent closeForms(True)
        End If

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

        If (Tag = "openFromBtnConditions") Then

            If (bnClose = True) Then
                bnClose = False
                Close()
                Dispose()
            End If

        Else

            DialogResult = DialogResult.Abort

            If (bnClose = True) Then
                bnClose = False
                RaiseEvent closeForms(True)
            End If

        End If

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
    ''' Método que sirve para crear un control en base al Atributo seleccionado en el combobox "Atributos"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	16/04/2009	Created    
    ''' </history>
    Private Sub createControlOfDynamicIndex()

        panelValues.Controls.Clear()
        Display = New DisplayindexCtl(index, True)
        panelValues.Controls.Add(Display)

    End Sub

    Private Sub frmAbmZfrmDesc_HelpButtonClicked(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked
        Help.ShowPopup(lblOpRelacion, CONS_HELP_MSJ, New Point(Location.X + lblOpRelacion.Location.X, Location.Y + lblOpRelacion.Location.Y))
    End Sub


End Class