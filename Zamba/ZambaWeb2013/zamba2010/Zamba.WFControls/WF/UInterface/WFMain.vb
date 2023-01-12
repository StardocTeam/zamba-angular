Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.AdminControls

Public Class WFMain
    Inherits ZForm

#Region " Código generado por el Diseñador de Windows Forms "

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <history>
    '''     [Marcelo]   01/10/2009  Modified     Rules Load Event
    ''' </history>
    ''' <remarks></remarks>
    Public Sub New()
        MyBase.New()
        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()
        _openMode = OpenModes.Edit
        RemoveHandler ZBaseCore.ForceLoad, AddressOf LazyLoadBusiness.LoadInstance
        AddHandler ZBaseCore.ForceLoad, AddressOf LazyLoadBusiness.LoadInstance
        'Marcelo: this event loads the rules preferences
        RemoveHandler ZBaseCore.loadRulePreference, AddressOf WFRulesBusiness.FillRulePreference
        AddHandler ZBaseCore.loadRulePreference, AddressOf WFRulesBusiness.FillRulePreference
    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents btNuevo As ZButton
    Friend WithEvents lstWorkflows As ListBox
    Friend WithEvents Panel1 As ZPanel
    Friend WithEvents btCambiarNombre As ZButton
    Friend WithEvents btEliminar As ZButton
    Friend WithEvents PanelButtons As System.Windows.Forms.Panel
    Friend WithEvents PanelListBox As System.Windows.Forms.Panel
    Friend WithEvents PanelLeft As System.Windows.Forms.Panel
    Friend WithEvents PanelRight As System.Windows.Forms.Panel
    Friend WithEvents PanelTop As System.Windows.Forms.Panel
    Friend WithEvents PanelBottom As System.Windows.Forms.Panel
    Friend WithEvents btnSalir As ZButton
    Friend WithEvents btEditar As ZButton
    Friend WithEvents lbTituloDescripcion As ZLabel
    Friend WithEvents btnDescripcion As ZButton
    Friend WithEvents txtHelp As TextBox
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents btnGlobalVariables As ZButton
    Friend WithEvents btnGeneralRules As ZButton
    Friend WithEvents lbTituloAyuda As ZLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(WFMain))
        btNuevo = New ZButton()
        lstWorkflows = New ListBox()
        Panel1 = New ZPanel()
        PanelListBox = New System.Windows.Forms.Panel()
        PanelButtons = New System.Windows.Forms.Panel()
        btnDescripcion = New ZButton()
        btCambiarNombre = New ZButton()
        btnGeneralRules = New ZButton()
        btnGlobalVariables = New ZButton()
        txtHelp = New TextBox()
        txtDescription = New TextBox()
        lbTituloAyuda = New ZLabel()
        lbTituloDescripcion = New ZLabel()
        btnSalir = New ZButton()
        btEliminar = New ZButton()
        btEditar = New ZButton()
        PanelLeft = New System.Windows.Forms.Panel()
        PanelRight = New System.Windows.Forms.Panel()
        PanelTop = New System.Windows.Forms.Panel()
        PanelBottom = New System.Windows.Forms.Panel()
        Panel1.SuspendLayout()
        PanelListBox.SuspendLayout()
        PanelButtons.SuspendLayout()
        SuspendLayout()
        '
        'btNuevo
        '
        btNuevo.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btNuevo.FlatStyle = FlatStyle.Flat
        btNuevo.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btNuevo.ForeColor = System.Drawing.Color.White
        btNuevo.Location = New System.Drawing.Point(8, 0)
        btNuevo.Name = "btNuevo"
        btNuevo.Size = New System.Drawing.Size(252, 29)
        btNuevo.TabIndex = 1
        btNuevo.Text = "Nuevo"
        btNuevo.UseVisualStyleBackColor = False
        '
        'lstWorkflows
        '
        lstWorkflows.BackColor = System.Drawing.Color.White
        lstWorkflows.Dock = System.Windows.Forms.DockStyle.Fill
        lstWorkflows.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lstWorkflows.ItemHeight = 16
        lstWorkflows.Location = New System.Drawing.Point(0, 0)
        lstWorkflows.Name = "lstWorkflows"
        lstWorkflows.Size = New System.Drawing.Size(297, 564)
        lstWorkflows.TabIndex = 2
        '
        'Panel1
        '
        Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel1.Controls.Add(PanelListBox)
        Panel1.Controls.Add(PanelButtons)
        Panel1.Controls.Add(PanelLeft)
        Panel1.Controls.Add(PanelRight)
        Panel1.Controls.Add(PanelTop)
        Panel1.Controls.Add(PanelBottom)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Panel1.Location = New System.Drawing.Point(2, 2)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(580, 578)
        Panel1.TabIndex = 3
        '
        'PanelListBox
        '
        PanelListBox.BackColor = System.Drawing.Color.Transparent
        PanelListBox.Controls.Add(lstWorkflows)
        PanelListBox.Dock = System.Windows.Forms.DockStyle.Fill
        PanelListBox.Location = New System.Drawing.Point(7, 6)
        PanelListBox.Name = "PanelListBox"
        PanelListBox.Size = New System.Drawing.Size(297, 564)
        PanelListBox.TabIndex = 12
        '
        'PanelButtons
        '
        PanelButtons.BackColor = System.Drawing.Color.Transparent
        PanelButtons.Controls.Add(btnDescripcion)
        PanelButtons.Controls.Add(btCambiarNombre)
        PanelButtons.Controls.Add(btnGeneralRules)
        PanelButtons.Controls.Add(btnGlobalVariables)
        PanelButtons.Controls.Add(txtHelp)
        PanelButtons.Controls.Add(txtDescription)
        PanelButtons.Controls.Add(lbTituloAyuda)
        PanelButtons.Controls.Add(lbTituloDescripcion)
        PanelButtons.Controls.Add(btnSalir)
        PanelButtons.Controls.Add(btEliminar)
        PanelButtons.Controls.Add(btEditar)
        PanelButtons.Controls.Add(btNuevo)
        PanelButtons.Dock = System.Windows.Forms.DockStyle.Right
        PanelButtons.Location = New System.Drawing.Point(304, 6)
        PanelButtons.Name = "PanelButtons"
        PanelButtons.Size = New System.Drawing.Size(267, 564)
        PanelButtons.TabIndex = 13
        '
        'btnDescripcion
        '
        btnDescripcion.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnDescripcion.FlatStyle = FlatStyle.Flat
        btnDescripcion.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnDescripcion.ForeColor = System.Drawing.Color.White
        btnDescripcion.Location = New System.Drawing.Point(8, 213)
        btnDescripcion.Name = "btnDescripcion"
        btnDescripcion.Size = New System.Drawing.Size(252, 28)
        btnDescripcion.TabIndex = 3
        btnDescripcion.Text = "Modificar"
        btnDescripcion.UseVisualStyleBackColor = False
        '
        'btCambiarNombre
        '
        btCambiarNombre.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btCambiarNombre.FlatStyle = FlatStyle.Flat
        btCambiarNombre.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btCambiarNombre.ForeColor = System.Drawing.Color.White
        btCambiarNombre.Location = New System.Drawing.Point(8, 177)
        btCambiarNombre.Name = "btCambiarNombre"
        btCambiarNombre.Size = New System.Drawing.Size(252, 29)
        btCambiarNombre.TabIndex = 2
        btCambiarNombre.Text = "Plantillas"
        btCambiarNombre.UseVisualStyleBackColor = False
        '
        'btnGeneralRules
        '
        btnGeneralRules.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnGeneralRules.FlatStyle = FlatStyle.Flat
        btnGeneralRules.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnGeneralRules.ForeColor = System.Drawing.Color.White
        btnGeneralRules.Location = New System.Drawing.Point(8, 142)
        btnGeneralRules.Name = "btnGeneralRules"
        btnGeneralRules.Size = New System.Drawing.Size(252, 28)
        btnGeneralRules.TabIndex = 14
        btnGeneralRules.Text = "Reglas Generales"
        btnGeneralRules.UseVisualStyleBackColor = False
        '
        'btnGlobalVariables
        '
        btnGlobalVariables.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnGlobalVariables.FlatStyle = FlatStyle.Flat
        btnGlobalVariables.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnGlobalVariables.ForeColor = System.Drawing.Color.White
        btnGlobalVariables.Location = New System.Drawing.Point(8, 106)
        btnGlobalVariables.Name = "btnGlobalVariables"
        btnGlobalVariables.Size = New System.Drawing.Size(252, 29)
        btnGlobalVariables.TabIndex = 13
        btnGlobalVariables.Text = "Variables Globales"
        btnGlobalVariables.UseVisualStyleBackColor = False
        '
        'txtHelp
        '
        txtHelp.Enabled = False
        txtHelp.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtHelp.Location = New System.Drawing.Point(8, 401)
        txtHelp.Multiline = True
        txtHelp.Name = "txtHelp"
        txtHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        txtHelp.Size = New System.Drawing.Size(252, 114)
        txtHelp.TabIndex = 12
        '
        'txtDescription
        '
        txtDescription.Enabled = False
        txtDescription.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        txtDescription.Location = New System.Drawing.Point(8, 267)
        txtDescription.Multiline = True
        txtDescription.Name = "txtDescription"
        txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        txtDescription.Size = New System.Drawing.Size(252, 115)
        txtDescription.TabIndex = 11
        '
        'lbTituloAyuda
        '
        lbTituloAyuda.AutoSize = True
        lbTituloAyuda.BackColor = System.Drawing.Color.Transparent
        lbTituloAyuda.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lbTituloAyuda.FontSize = 9.0!
        lbTituloAyuda.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lbTituloAyuda.Location = New System.Drawing.Point(8, 385)
        lbTituloAyuda.Name = "lbTituloAyuda"
        lbTituloAyuda.Size = New System.Drawing.Size(41, 14)
        lbTituloAyuda.TabIndex = 10
        lbTituloAyuda.Text = "Ayuda"
        lbTituloAyuda.TextAlign = ContentAlignment.MiddleLeft
        '
        'lbTituloDescripcion
        '
        lbTituloDescripcion.AutoSize = True
        lbTituloDescripcion.BackColor = System.Drawing.Color.Transparent
        lbTituloDescripcion.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        lbTituloDescripcion.FontSize = 9.0!
        lbTituloDescripcion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        lbTituloDescripcion.Location = New System.Drawing.Point(8, 248)
        lbTituloDescripcion.Name = "lbTituloDescripcion"
        lbTituloDescripcion.Size = New System.Drawing.Size(68, 14)
        lbTituloDescripcion.TabIndex = 9
        lbTituloDescripcion.Text = "Descripción"
        lbTituloDescripcion.TextAlign = ContentAlignment.MiddleLeft
        '
        'btnSalir
        '
        btnSalir.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btnSalir.ForeColor = System.Drawing.Color.White
        btnSalir.Location = New System.Drawing.Point(8, 521)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New System.Drawing.Size(252, 43)
        btnSalir.TabIndex = 7
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        '
        'btEliminar
        '
        btEliminar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btEliminar.FlatStyle = FlatStyle.Flat
        btEliminar.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btEliminar.ForeColor = System.Drawing.Color.White
        btEliminar.Location = New System.Drawing.Point(8, 71)
        btEliminar.Name = "btEliminar"
        btEliminar.Size = New System.Drawing.Size(252, 28)
        btEliminar.TabIndex = 3
        btEliminar.Text = "Eliminar"
        btEliminar.UseVisualStyleBackColor = False
        '
        'btEditar
        '
        btEditar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btEditar.FlatStyle = FlatStyle.Flat
        btEditar.Font = New Font("Tahoma", 9.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        btEditar.ForeColor = System.Drawing.Color.White
        btEditar.Location = New System.Drawing.Point(8, 35)
        btEditar.Name = "btEditar"
        btEditar.Size = New System.Drawing.Size(252, 29)
        btEditar.TabIndex = 8
        btEditar.Text = "Diseñar"
        btEditar.UseVisualStyleBackColor = False
        '
        'PanelLeft
        '
        PanelLeft.BackColor = System.Drawing.Color.Transparent
        PanelLeft.Dock = System.Windows.Forms.DockStyle.Left
        PanelLeft.Location = New System.Drawing.Point(0, 6)
        PanelLeft.Name = "PanelLeft"
        PanelLeft.Size = New System.Drawing.Size(7, 564)
        PanelLeft.TabIndex = 11
        '
        'PanelRight
        '
        PanelRight.BackColor = System.Drawing.Color.Transparent
        PanelRight.Dock = System.Windows.Forms.DockStyle.Right
        PanelRight.Location = New System.Drawing.Point(571, 6)
        PanelRight.Name = "PanelRight"
        PanelRight.Size = New System.Drawing.Size(7, 564)
        PanelRight.TabIndex = 10
        '
        'PanelTop
        '
        PanelTop.BackColor = System.Drawing.Color.Transparent
        PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        PanelTop.Location = New System.Drawing.Point(0, 0)
        PanelTop.Name = "PanelTop"
        PanelTop.Size = New System.Drawing.Size(578, 6)
        PanelTop.TabIndex = 9
        '
        'PanelBottom
        '
        PanelBottom.BackColor = System.Drawing.Color.Transparent
        PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        PanelBottom.Location = New System.Drawing.Point(0, 570)
        PanelBottom.Name = "PanelBottom"
        PanelBottom.Size = New System.Drawing.Size(578, 6)
        PanelBottom.TabIndex = 8
        '
        'WFMain
        '
        AutoScaleBaseSize = New System.Drawing.Size(7, 16)
        BackColor = System.Drawing.Color.White
        ClientSize = New System.Drawing.Size(584, 582)
        Controls.Add(Panel1)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Location = New System.Drawing.Point(0, 0)
        MaximizeBox = False
        MinimizeBox = False
        Name = "WFMain"
        Text = ""
        Panel1.ResumeLayout(False)
        PanelListBox.ResumeLayout(False)
        PanelButtons.ResumeLayout(False)
        PanelButtons.PerformLayout()
        ResumeLayout(False)

    End Sub

#End Region

    Private _openMode As OpenModes
    Public Event ShowIde(ByVal workflow As Zamba.Core.WorkFlow, ByVal OpenMode As OpenModes, ByVal RightType As RightsType)
    ' Si el usuario selecciono un workflow, éste va a mostrar sus etapas y reglas si es que tiene. Pero si el usuario va nuevamente a la ventana
    ' de workflows y decide eliminar el workflow que recién abrio, entonces se debería también eliminar el explorador que muestra el workflow
    ' (a no ser que haya un workflow con el mismo nombre, por lo que se compara también por id, idem para changeworkflowName)
    Public Event deleteWorkflow(ByVal workflowIdDelete As Int64, ByVal workflowNameDeleted As String)
    ' Si el usuario selecciono un workflow, éste va a mostrar sus etapas y reglas si es que tiene. Pero si el usuario va nuevamente a la ventana
    ' de workflows y decide cambiar el nombre del workflow que recién abrio, entonces el cambio de nombre también se debería visualizar en el árbol
    ' , es decir, en el explorador que permite explorar las etapas y reglas de un workflow
    Public Event changeWorkflowName(ByVal workflowId As Int64, ByVal workflowActualName As String, ByVal workflowNewName As String)
    'Abre la ventana de reglas generales
    Public Event OpenGeneralRulesWindow()

    Enum OpenModes
        Monitor
        Edit
        View
    End Enum

#Region "Load"

    Private Sub WFMain_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        FillWF()
    End Sub

    ''' <summary>
    ''' Método que sirve para traer un dataset con los workflow existentes, o en caso de que el dataset sea nothing crearlo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     29/05/2008  [Gaston]    Modified
    ''' </history>
    Sub FillWF()

        Try
            DsWf = WFBusiness.GetWFsByUserRights(Membership.MembershipHelper.CurrentUser.ID)

            If (DsWf Is Nothing) Then
                DsWf = New List(Of WorkflowAdminDto)
            End If


            lstWorkflows.DisplayMember = "Name"
            lstWorkflows.ValueMember = "Work_ID"
            lstWorkflows.DataSource = DsWf

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

#Region "Eventos Botones"

    ''' <summary>
    ''' Editar workflow
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' 	[Gaston]	04/06/2008	Modified    Se agrego el evento changeWorkflowName
    ''' </history>
    ''' <remarks></remarks>
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btCambiarNombre.Click
        Dim wftemp As New WFTemplates
        wftemp.ShowDialog()
    End Sub

    ''' <summary>
    ''' Borrar el workflow
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' 	[Gaston]	03/06/2008	Modified    Se agrego el evento deleteWorkflow
    ''' </history>
    ''' <remarks></remarks>
    Private Sub BtnDel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btEliminar.Click

        Try

            If (MessageBox.Show("Esta seguro que desea eliminar este workflow?", "Eliminar workflow", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
                Dim frm As New FrmAskPassword(UserBusiness.Rights.CurrentUser.Password)
                If frm.ShowDialog() <> DialogResult.OK Then
                    If frm.isCancel = True Then
                        frm.Dispose()
                        Exit Sub
                    Else
                        frm.Dispose()
                        MessageBox.Show("Clave incorrecta", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End If
                frm.Dispose()
                Dim name As String = DsWf(lstWorkflows.SelectedIndex).Name
                Dim id As Int64 = DsWf(lstWorkflows.SelectedIndex).Work_ID

                WFFactory.RemoveWorkFlow(id)
                'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
                UserBusiness.Rights.SaveAction(id, ObjectTypes.ModuleWorkFlow, RightsType.Delete, "Se eliminó el workflow: " & name & "(" & id.ToString & ")")
                DsWf.RemoveAt(lstWorkflows.SelectedIndex)

                ' Se ejecuta el evento deleteworkflow que verifica si existe un workflow con nombre idéntico en el explorador. Si es así, entonces 
                ' se elimina el explorador (o mejor dicho la clase WFEditIDE que contiene al explorador). A no ser que haya dos workflows con el 
                ' mismo nombre, en cuyo caso si el id del workflow que se selecciono para eliminar es distinto al que hay en el explorador, entonces
                ' no se eliminara el formulario que contiene al workflow (pero si es igual si)
                RaiseEvent deleteWorkflow(id, name)

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub ListBox1_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lstWorkflows.DoubleClick, btEditar.Click
        OpenWf()


    End Sub


    ''' <summary>
    ''' Abrir el WF para editarlo
    ''' </summary>
    ''' <history>
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' <remarks></remarks>
    Private Sub OpenWf()
        Try
            If lstWorkflows.SelectedIndex <> -1 Then
                Dim WFRow As WorkflowAdminDto = DsWf(lstWorkflows.SelectedIndex)

                WF = WFFactory.GetWf(WFRow)
                RaiseEvent ShowIde(WF, _openMode, WFRow.Right)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Dim WF As Zamba.Core.WorkFlow
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles lstWorkflows.SelectedIndexChanged
        Try
            If lstWorkflows.SelectedIndex <> -1 Then
                Dim WFRow As WorkflowAdminDto = DsWf(lstWorkflows.SelectedIndex)
                WF = WFFactory.GetWf(WFRow)
                txtDescription.Text = WF.Description
                txtHelp.Text = WF.Help
                Text = WF.Name & "(" & WF.ID & ")"
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSalir.Click
        Close()
    End Sub
#End Region

#Region "New Workflow"
    Dim WithEvents zw As Zamba.Controls.ZWizard
    Dim c0 As Control0
    Dim c1 As Control1
    Dim c2 As Control2
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btNuevo.Click
        Try
            c0 = New Control0
            c1 = New Control1
            c2 = New Control2
            Dim c() As UserControl = {c0, c1, c2}
            zw = New ZWizard("Asistente de WorkFlow", c)
            zw.ShowDialog()
            If zw.DialogResult = DialogResult.OK Then
                'Agregar permiso para editar el wf que se acaba de crear
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub FinalizeWizard() Handles zw.FinalizeWizard

        Try

            If (c1.Nombre <> "") Then
                Dim WorkflowAdminDto As New WorkflowAdminDto
                CreateWf(WorkflowAdminDto)
                WF = WFFactory.GetWf(WorkflowAdminDto)
                FillSteps(WF)
                Close()
                OpenWf(WF, RightsType.Edit)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Public DsWf As New List(Of WorkflowAdminDto)

    Public Shared Sub LoadWorkFlows()
        'DsWf = WFBusiness.GetWFs()
    End Sub
    ''' <summary>
    ''' Crea un WF nuevo
    ''' </summary>
    ''' <param name="Row"></param>
    ''' <history>
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' <remarks></remarks>
    Private Sub CreateWf(ByVal Row As WorkflowAdminDto)
        Row.Work_ID = ToolsBusiness.GetNewID(IdTypes.WF)
        Row.WStat_Id = 1
        Row.Name = c1.Nombre
        Row.Description = c1.Descripcion
        Row.Help = c1.Ayuda
        Row.EditDate = Now
        Row.CreateDate = Now
        Row.RefreshRate = 5
        Row.InitialStepId = 0
        DsWf.Add(Row)
        WFFactory.CreateWorkflow(Row)

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(CLng(Row.Work_ID), ObjectTypes.ModuleWorkFlow, RightsType.insert, "Se creó el Workflow " & Row.Name & "(" & Row.Work_ID & ")")

        'Die: le agrego a todos los grupos en lo que se incuye el usuario actual
        'el permiso para editar el wf recien creado
        Dim _userGroupsIds As Generic.List(Of Int64)
        _userGroupsIds = UserBusiness.GetUserGroupsIdsByUserid(Membership.MembershipHelper.CurrentUser.ID)
        If Not IsNothing(_userGroupsIds) Then
            For Each groupid As Int64 In _userGroupsIds
                UserBusiness.Rights.SetRight(groupid, ObjectTypes.ModuleWorkFlow, RightsType.Edit, CLng(Row.Work_ID), True)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Método que sirve para guardar las etapas del nuevo workflow en la base de datos
    ''' </summary>
    ''' <param name="wf"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/10/2008	Modified    Se agrega el estado "[Ninguno]" por defecto  a las etapas del nuevo workflow  
    ''' </history>
    Private Sub FillSteps(ByVal wf As Zamba.Core.WorkFlow)

        Dim steps() As SWfStep = c2.SWFSteps
        Dim x As Int32
        Dim y As Int32

        Try

            For Each s As SWfStep In steps
                Dim wfstepId As Int64 = WFStepsFactory.InsertStep(wf.ID, s.Name, s.Help, s.Description, New Point(x, y), 0, 30, 48, s.Initial)
                If s.Initial = True Then
                    wf.InitialStepIdTEMP = wfstepId
                End If

                If y <= 60 Then
                    y += 20
                Else
                    y = 0
                    x += 20
                End If

                ' Se agrega el estado por defecto "[Ninguno]" a la etapa o etapas, y se establece como inicial
                Dim NewState As New WFStepState(ToolsBusiness.GetNewID(IdTypes.WFSTEPSTATE), s.Name, "Estado por defecto", True)
                WFStepStatesFactory.AddState(CInt(NewState.ID), NewState.Description, NewState.Name, 1, CInt(wfstepId))

            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub OpenWf(ByVal WFRow As Zamba.Core.WorkFlow, RightType As RightsType)
        RaiseEvent ShowIde(WF, _openMode, RightType)
    End Sub

#End Region
    ''' <summary>
    ''' boton que modifica el campo de ayuda y descripcion del WFMain.
    ''' </summary>
    ''' <param name="WFMainDescripcion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[alan]	    09/02/2010	Modified    Se agrega boton "Editar descripcion/ayuda" 
    '''     [Marcelo]   20/04/2012  Modified    Se agrega refresh
    ''' </history>
    Private Sub btnDescripcion_Click_1(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnDescripcion.Click
        If lstWorkflows.SelectedIndex <> -1 Then
            Dim selectedIndex As Int64 = lstWorkflows.SelectedIndex
            Dim WFRow As WorkflowAdminDto = DsWf(lstWorkflows.SelectedIndex)

            Dim frmDescripcion As New WFMainDescripcion(WFRow.Work_ID, WFRow.Description, WFRow.Help)
            frmDescripcion.ShowDialog()

            FillWF()
            lstWorkflows.SelectedIndex = selectedIndex
        End If


        Try
            Dim OldName As String = DsWf(lstWorkflows.SelectedIndex).Name
            Dim NewName As String = InputBox("Ingrese el nombre", "Zamba Software", OldName)
            Dim wfId As Int64 = DsWf(lstWorkflows.SelectedIndex).Work_ID

            If NewName <> OldName AndAlso Not String.IsNullOrEmpty(NewName.Trim) Then

                DsWf(lstWorkflows.SelectedIndex).Name = NewName

                WFFactory.SaveNewName(NewName, CInt(wfId))
                'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
                UserBusiness.Rights.SaveAction(wfId, ObjectTypes.ModuleWorkFlow, RightsType.Edit, "Cambio de nombre del Workflow: '" & OldName & "' a '" & NewName & "'(" & wfId.ToString & ")")
                ' Se ejecuta el evento cambio de nombre que verifica si existe un workflow con nombre idéntico en el explorador (que muestra al
                ' workflow y sus etapas (y reglas si es que ahi)). Si es así, entonces se cambia el nombre del workflow en el explorador. A no
                ' ser que haya dos workflows con el mismo nombre, en cuyo caso si el id del workflow que se selecciono para cambiar el nombre 
                ' es distinto al que hay en el explorador, entonces no se cambiara el nombre del workflow en el explorador (pero si es igual si)
                RaiseEvent changeWorkflowName(wfId, OldName, NewName)

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnGlobalVariables_Click(sender As Object, e As EventArgs) Handles btnGlobalVariables.Click
        Dim frmGlobalVariables As frmGlobalVariables = Nothing

        Try
            frmGlobalVariables = New frmGlobalVariables
            frmGlobalVariables.ShowDialog()
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Ha ocurrido un error al abrir el configurador de variables globales", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If frmGlobalVariables IsNot Nothing Then
                frmGlobalVariables.Dispose()
                frmGlobalVariables = Nothing
            End If
        End Try
    End Sub


    Private Sub btnGeneralRules_Click(sender As Object, e As EventArgs) Handles btnGeneralRules.Click
        RaiseEvent OpenGeneralRulesWindow()
        Close()
    End Sub
End Class