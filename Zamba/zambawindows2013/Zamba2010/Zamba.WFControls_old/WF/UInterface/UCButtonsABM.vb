Imports Zamba.Core

Public Class UCButtonsABM

    Dim frmButtons As frmGeneralRules
    Dim wfpanelcircuit As WFPanelCircuit
    Dim wfId As Int64

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
    End Sub

    Public Sub New(Optional ByVal title As String = "",
                   Optional ByVal buttonTypeFilter As ButtonType = ButtonType.All,
                   Optional ByVal wfId As Int64 = 0)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeData(title, buttonTypeFilter, wfId)
    End Sub

    Public Sub InitializeData(Optional ByVal title As String = "",
                              Optional ByVal buttonTypeFilter As ButtonType = ButtonType.All,
                              Optional ByVal wfId As Int64 = 0)
        ' Título del control.
        If String.IsNullOrEmpty(title) Then
            PanelTop.Visible = False
        Else
            PanelTop.Visible = True
            PanelTop.Text = title
        End If

        'Tipos de botones
        FillTypes(buttonTypeFilter)

        'Se completa la grilla con los botones
        Me.wfId = wfId
        FillGrid(buttonTypeFilter)
    End Sub

    Private Sub RefreshGrid(Optional ByVal buttonTypeFilter As ButtonType = ButtonType.All)
        dgvButtons.DataSource = Nothing
        dgvButtons.Rows.Clear()
        dgvButtons.Columns.Clear()
        FillGrid(buttonTypeFilter)
    End Sub

    Private Sub FillGrid(ByVal buttonTypeFilter As ButtonType)
        Try
            'Grilla de botones
            dgvButtons.DataSource = GenericButtonBusiness.GetButtons(buttonTypeFilter, wfId)

            'Se edita el nombre de columna y ayuda
            dgvButtons.Columns("ID").ToolTipText = "Identificador del botón a nivel base de datos"
            dgvButtons.Columns("BUTTONID").HeaderText = "Nombre interno"
            dgvButtons.Columns("BUTTONID").ToolTipText = "Identificador del botón a nivel código"
            dgvButtons.Columns("CAPTION").HeaderText = "Leyenda"
            dgvButtons.Columns("CAPTION").ToolTipText = "Es el nombre del botón para el usuario, lo que visualizará"
            dgvButtons.Columns("PLACEDESC").HeaderText = "Ubicación"
            dgvButtons.Columns("PLACEDESC").ToolTipText = "La ubicación del botón dentro de las distintas partes de Zamba"
            dgvButtons.Columns("TYPEDESC").HeaderText = "Tipo"
            dgvButtons.Columns("TYPEDESC").ToolTipText = "La acción del botón estará determinada por el tipo"
            dgvButtons.Columns("NEEDRIGHTS").HeaderText = "Verifica permisos"
            dgvButtons.Columns("NEEDRIGHTS").ToolTipText = "Al estar tildado verificará los permisos del usuario para visualizar el botón o no"
            dgvButtons.Columns("WFNAME").HeaderText = "Workflow"
            dgvButtons.Columns("WFNAME").ToolTipText = "Nombre del Workflow que la regla pertenece (vacío significa que puede mostrarse en todos los Workflows)"
            dgvButtons.Columns("CREATEDATE").HeaderText = "Fecha de creación"
            dgvButtons.Columns("CREATEDATE").ToolTipText = "Fecha en que el botón fué creado"
            dgvButtons.Columns("MODIFIEDDATE").HeaderText = "Fecha de edición"
            dgvButtons.Columns("MODIFIEDDATE").ToolTipText = "Fecha en que el botón fué modificado"
            dgvButtons.Columns("ICONID").HeaderText = "Id ícono"
            dgvButtons.Columns("ICONID").ToolTipText = "Id del ícono asignado al control"
            dgvButtons.Columns("VIEWCLASS").HeaderText = "Clase CSS"
            dgvButtons.Columns("VIEWCLASS").ToolTipText = "Clase que contiene el estilo css del botón (solo para Zamba Web)"
            dgvButtons.Columns("GroupName").HeaderText = "Grupo"
            dgvButtons.Columns("GroupName").ToolTipText = "Nombre del grupo por el cual se agruparan en web"
            dgvButtons.Columns("GroupClass").HeaderText = "Clase css del grupo"
            dgvButtons.Columns("GroupClass").ToolTipText = "Clase para mostrar los grupos en web"
            dgvButtons.Columns("BUTTONORDER").HeaderText = "Orden"
            dgvButtons.Columns("BUTTONORDER").ToolTipText = "Orden del botón con respecto a los otros botones"
            dgvButtons.Columns("PARAMS").HeaderText = "Parámetros"
            dgvButtons.Columns("PARAMS").ToolTipText = "Parámetros opcionales"
        Catch ex As Exception
            ZClass.raiseerror(ex)
            dgvButtons.DataSource = Nothing
            dgvButtons.Rows.Add(New Object() {"Ha ocurrido un error al cargar los botones dinámicos"})
        End Try
    End Sub

    Private Sub FillTypes(ByVal buttonTypeFilter As ButtonType)
        cboFilters.DataSource = GenericButtonBusiness.GetTypes()
        cboFilters.ValueMember = "TYPEID"
        cboFilters.DisplayMember = "TYPEDESC"
        cboFilters.SelectedValue = CInt(buttonTypeFilter)

        If buttonTypeFilter <> ButtonType.All Then
            pnlFilters.Visible = False
        Else
            RemoveHandler cboFilters.SelectedIndexChanged, AddressOf cboFilters_SelectedIndexChanged
            AddHandler cboFilters.SelectedIndexChanged, AddressOf cboFilters_SelectedIndexChanged
        End If
    End Sub

    ''' <summary>
    ''' Agrega un botón dinámico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>/*
    ''' <remarks></remarks>
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try

            'Se genera un nuevo abm de botones de tipo reglas y se muestra
            frmButtons = New frmGeneralRules()
            Dim dt As DataTable = GenericButtonBusiness.GetZbuttonsAndGroups()

            Dim typesOfButtons() As DataRow = dt.Select("Ztype = '1'")


            For Each button As DataRow In typesOfButtons
                frmButtons.lstTypesClass.Items.Add(button.Item("Zname").ToString)
            Next

            typesOfButtons = Nothing
            typesOfButtons = dt.Select("Ztype = '2'")

            For Each button As DataRow In typesOfButtons
                frmButtons.lstTypesGroups.Items.Add(button.Item("Zname").ToString)
            Next


            With frmButtons
                If .ShowDialog() = DialogResult.OK Then
                    'Si la descripcion esta vacia se pone el nombre de la regla
                    If String.IsNullOrEmpty(.txtCaption.Text.Trim) Then
                        .txtCaption.Text = .cboRules.Text.Substring(0, .cboRules.Text.LastIndexOf(" (")).Trim
                    End If

                    'Si se requiere para varios WF se asigna el id a cero
                    Dim workId As Int64
                    If Not .chkForOneWf.Checked Then
                        workId = 0
                    Else
                        If wfId = 0 Then
                            Dim wfbe As New WFBusinessExt
                            workId = wfbe.GetWorkflowIdByRule(.cboRules.SelectedValue)
                        Else
                            workId = wfId
                        End If
                    End If

                    'Por defecto el orden en cero
                    If String.IsNullOrEmpty(.txtOrder.Text.Trim) Then .txtOrder.Text = 0

                    Dim btnClass As String
                    Dim groupClass As String

                    If .lstTypesClass.CheckedItems.Count > 0 Then

                        Dim tableClass() As DataRow = dt.Select("Ztype = '1'")


                        For Each item As String In .lstTypesClass.CheckedItems
                            Dim indice As Integer = 0

                            While tableClass(indice).Item("Zname").ToString <> item
                                indice += 1
                            End While

                            GenericButtonBusiness.InsertZbuttonClassRule(.cboRules.SelectedValue, tableClass(indice).Item("ID"))

                            btnClass = btnClass & " " & item

                        Next


                    End If


                    If .lstTypesGroups.CheckedItems.Count > 0 Then
                        Dim tableGroup() As DataRow = dt.Select("Ztype = '2'")

                        For Each item As String In .lstTypesGroups.CheckedItems


                            Dim indice As Integer = 0

                            While tableGroup(indice).Item("Zname").ToString <> item
                                indice += 1
                            End While

                            GenericButtonBusiness.InsertZbuttonClassRule(.cboRules.SelectedValue, tableGroup(indice).Item("ID"))

                            groupClass = groupClass & " " & item

                        Next

                    End If

                    'Se agrega la regla
                    GenericButtonBusiness.AddRuleButton(.cboRules.SelectedValue,
                                                 CInt(.cboPlace.SelectedValue),
                                                 Integer.Parse(.txtOrder.Text),
                                                 .txtCaption.Text,
                                                 .txtParams.Text,
                                                 .chkNeedRights.Checked,
                                                 workId,
                                                 .cmbSelectIcon.SelectedIndex,
                                                 btnClass,
                                                 .txtGroupName.Text,
                                                 groupClass)

                    RefreshGrid(CInt(cboFilters.SelectedValue))
                End If
            End With
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Actualiza un botón dinámico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEdit.Click
        If dgvButtons.SelectedRows.Count > 0 Then
            Dim id As Int64 = Int64.Parse(dgvButtons.SelectedRows(0).Cells("ID").Value.ToString)

            'Verifica la existencia del botón
            If GenericButtonBusiness.CheckExistance(id) Then
                'Se genera un nuevo abm de botones de tipo reglas, se cargan los valores y se muestra
                frmButtons = New frmGeneralRules(Int64.Parse(dgvButtons.SelectedRows(0).Cells("ButtonID").Value.ToString.Replace("zamba_rule_", String.Empty)))

                Dim dt As DataTable = GenericButtonBusiness.GetZbuttonsAndGroups()


                Dim typesOfButtons() As DataRow = dt.Select("Ztype = '1'")

                For Each button As DataRow In typesOfButtons
                    frmButtons.lstTypesClass.Items.Add(button.Item("Zname").ToString)
                Next

                typesOfButtons = Nothing
                typesOfButtons = dt.Select("Ztype = '2'")

                For Each button As DataRow In typesOfButtons
                    frmButtons.lstTypesGroups.Items.Add(button.Item("Zname").ToString)
                Next

                With frmButtons
                    .cboPlace.Text = dgvButtons.SelectedRows(0).Cells("PlaceDesc").Value.ToString
                    .txtCaption.Text = dgvButtons.SelectedRows(0).Cells("Caption").Value.ToString
                    .txtOrder.Text = dgvButtons.SelectedRows(0).Cells("ButtonOrder").Value.ToString
                    .chkNeedRights.Checked = CBool(dgvButtons.SelectedRows(0).Cells("NeedRights").Value.ToString)
                    .txtParams.Text = dgvButtons.SelectedRows(0).Cells("Params").Value.ToString
                    .chkForOneWf.Checked = Not String.IsNullOrEmpty(dgvButtons.SelectedRows(0).Cells("WFNAME").Value.ToString)
                    Dim ruleID As Int64
                    If Not IsNothing(dgvButtons.SelectedRows(0).Cells("ButtonID").Value) AndAlso dgvButtons.SelectedRows(0).Cells("ButtonID").Value.ToString().Contains("_") Then
                        If Int64.TryParse(dgvButtons.SelectedRows(0).Cells("ButtonID").Value.ToString().Split("_")(2), ruleID) Then
                            .cboRules.SelectedValue = ruleID
                        End If
                    End If

                    If dgvButtons.SelectedRows(0).Cells("ICONID").Value = 0 Then
                        .cmbSelectIcon.SelectedIndex = 0
                    Else
                        .cmbSelectIcon.SelectedIndex = 1
                    End If

                    If Not IsDBNull(dgvButtons.SelectedRows(0).Cells("VIEWCLASS").Value) Then

                        Dim lstBotones As String() = dgvButtons.SelectedRows(0).Cells("VIEWCLASS").Value.Split(" ")
                        Dim indexAux As Integer

                        For Each btnChecked As String In lstBotones

                            If Not String.IsNullOrEmpty(btnChecked) Then

                                For indexAux = 0 To .lstTypesClass.Items.Count - 1

                                    If .lstTypesClass.Items(indexAux).ToString = btnChecked Then
                                        .lstTypesClass.SetItemChecked(indexAux, True)
                                        Exit For
                                    End If
                                Next
                            End If
                            indexAux = 0
                        Next

                    End If

                    If IsDBNull(dgvButtons.SelectedRows(0).Cells("GroupName").Value) Then
                        .txtGroupName.Text = String.Empty
                    Else
                        .txtGroupName.Text = dgvButtons.SelectedRows(0).Cells("GroupName").Value
                    End If

                    If Not IsDBNull(dgvButtons.SelectedRows(0).Cells("GroupClass").Value) Then
                        Dim lstBotones As String() = dgvButtons.SelectedRows(0).Cells("GroupClass").Value.Split(" ")
                        Dim indexAux As Integer

                        For Each btnChecked As String In lstBotones
                            If Not String.IsNullOrEmpty(btnChecked) Then

                                For indexAux = 0 To .lstTypesGroups.Items.Count - 1

                                    If .lstTypesGroups.Items(indexAux).ToString = btnChecked Then
                                        .lstTypesGroups.SetItemChecked(indexAux, True)
                                    End If
                                Next
                            End If
                            indexAux = 0
                        Next


                        .lstTypesGroups.SelectedItem = dgvButtons.SelectedRows(0).Cells("GroupClass").Value
                    End If

                    If .ShowDialog() = DialogResult.OK Then
                        'Si la descripcion esta vacia se pone el nombre de la regla
                        If String.IsNullOrEmpty(.txtCaption.Text.Trim) Then
                            .txtCaption.Text = .cboRules.Text.Substring(0, .cboRules.Text.LastIndexOf(" (")).Trim
                        End If

                        'Si se requiere para varios WF se asigna el id a cero
                        Dim workId As Int64
                        If Not .chkForOneWf.Checked Then
                            workId = 0
                        Else
                            If wfId = 0 Then
                                Dim wfbe As New WFBusinessExt
                                workId = wfbe.GetWorkflowIdByRule(.cboRules.SelectedValue)
                            Else
                                workId = wfId
                            End If
                        End If

                        'Por defecto el orden en cero
                        If String.IsNullOrEmpty(.txtOrder.Text.Trim) Then .txtOrder.Text = 0

                        GenericButtonBusiness.UpdateRuleButton(id,
                                                            .cboRules.SelectedValue,
                                                            DirectCast(CInt(.cboPlace.SelectedValue), ButtonPlace),
                                                            Int32.Parse(.txtOrder.Text),
                                                            .txtCaption.Text,
                                                            .txtParams.Text,
                                                            .chkNeedRights.Checked,
                                                            workId,
                                                            .cmbSelectIcon.SelectedIndex,
                                                            .lstTypesClass.Text,
                                                            .txtGroupName.Text,
                                                            .lstTypesGroups.Text)

                        RefreshGrid(DirectCast(CInt(cboFilters.SelectedValue), ButtonType))
                    End If
                End With
            Else
                MessageBox.Show("El botón que desea editar ha sido eliminado", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                RefreshGrid(DirectCast(cboFilters.SelectedValue, ButtonType))
            End If
        End If
    End Sub

    ''' <summary>
    ''' Elimina un botón dinámico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnDelete.Click
        If dgvButtons.SelectedRows.Count > 0 Then
            Dim id As Int64 = Int64.Parse(dgvButtons.SelectedRows(0).Cells("ID").Value.ToString)

            'Verifica la existencia del botón
            If GenericButtonBusiness.CheckExistance(id) Then
                If MessageBox.Show("Se eliminará el botón " & dgvButtons.SelectedRows(0).Cells("ButtonID").Value.ToString & " ¿Desea continuar?", "Confirmar acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    GenericButtonBusiness.DeleteButton(Int64.Parse(dgvButtons.SelectedRows(0).Cells("ID").Value.ToString))
                    RefreshGrid([Enum].Parse(GetType(ButtonType), StrConv(cboFilters.Text, VbStrConv.ProperCase)))
                    'StrConv(cboFilters.Text, VbStrConv.ProperCase)
                End If
            Else
                MessageBox.Show("El botón que desea eliminar ya ha sido eliminado", String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                RefreshGrid([Enum].Parse(GetType(ButtonType), StrConv(cboFilters.Text, VbStrConv.ProperCase)))
            End If
        End If
    End Sub

    ''' <summary>
    ''' Abre la ventana de edicion sobre la fila que se encuentre seleccionada al hacer doble click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvButtons_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvButtons.CellContentDoubleClick
        btnEdit_Click(Nothing, Nothing)
    End Sub

    ''' <summary>
    ''' Abre la ventana de edicion al presionar enter sobre la grilla.
    ''' Abre la ventana de eliminación al presionar Supr o Backspace.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvButtons_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvButtons.KeyDown
        If e.KeyValue = 13 Then
            btnEdit_Click(Nothing, Nothing)
        ElseIf e.KeyValue = 46 Or e.KeyValue = 8 Then
            btnDelete_Click(Nothing, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Filtra la grilla por el tipo de boton
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboFilters_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        RefreshGrid(DirectCast(CInt(cboFilters.SelectedValue), ButtonType))
    End Sub
End Class
