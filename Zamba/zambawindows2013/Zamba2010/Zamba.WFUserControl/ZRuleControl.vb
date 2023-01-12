Imports Zamba.AdminControls
'Imports Zamba.ZTC

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Controls
''' Class	 : Controls.ZRuleControl
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase de la que deben heredar todos los controles de reglas de condicion
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Martin]	30/05/2006	Created
''' 	[Marcelo]	14/08/2007	Modified
'''     [Gaston]	10/04/2008	Modified
''' </history>
''' -----------------------------------------------------------------------------
Public Class ZRuleControl
    Inherits ZControl
    Implements IZRuleControl

    Friend WithEvents tbAsignation As System.Windows.Forms.TabPage
    Friend WithEvents tbHelp As System.Windows.Forms.TabPage
    Friend WithEvents btnEstadoValDef As ZButton
    Friend WithEvents splHelp As System.Windows.Forms.SplitContainer
    Friend WithEvents lblHelp As ZLabel
    Friend WithEvents txtHelp As TextBox
    Friend WithEvents BtnSaveHelp As ZButton

    Public WithEvents tbState As System.Windows.Forms.TabPage
    Public WithEvents tbHabilitation As System.Windows.Forms.TabPage
    Public WithEvents tbConfiguration As System.Windows.Forms.TabPage
    Public WithEvents tbAlerts As System.Windows.Forms.TabPage
    Public WithEvents tbRule As System.Windows.Forms.TabPage
    Public WithEvents tbctrMain As System.Windows.Forms.TabControl
    Friend WithEvents lblSeleccionarEstado As ZLabel
    Friend WithEvents btnSeleccionar As ZButton
    Friend WithEvents cmbEtapa As ComboBox
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents lstStates As ListBox
    Friend WithEvents tbCases As System.Windows.Forms.TabPage
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents chkRemote As System.Windows.Forms.CheckBox
    Friend WithEvents chkExecuteRuleWhenResult As System.Windows.Forms.CheckBox
    Friend WithEvents chkRefreshRule As System.Windows.Forms.CheckBox
    Friend WithEvents lblMessage As ZLabel
    Friend WithEvents btnSavePreferences As ZButton
    Friend WithEvents Label2 As ZLabel
    Friend WithEvents cmbCategoria As ComboBox
    Friend WithEvents btnGoToRule As ZButton
    Friend WithEvents chkDisableChilds As System.Windows.Forms.CheckBox
    Friend WithEvents cmbRules As ComboBox
    Friend WithEvents chkExecuteRuleInCaseOfError As System.Windows.Forms.CheckBox
    Friend WithEvents txtMessageToShowInCaseOfError As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents chkAsynchronous As System.Windows.Forms.CheckBox
    Friend WithEvents chkSaveUpdateInHistory As System.Windows.Forms.CheckBox
    Friend WithEvents txtComment As Zamba.AppBlock.TextoInteligenteTextBox
    Friend WithEvents lblComment As ZLabel
    Friend WithEvents chkSaveUpdate As System.Windows.Forms.CheckBox
    Friend WithEvents chkCleanRule As System.Windows.Forms.CheckBox
    Friend WithEvents chkCloseTask As System.Windows.Forms.CheckBox
    Friend WithEvents chkThrowExceptionIfCancel As System.Windows.Forms.CheckBox
    Friend WithEvents chkContinueWithError As System.Windows.Forms.CheckBox
    Dim MyRuleSelectionOption As RuleSectionOptions

    Private Event OpenMissedRule(ByVal workflowId As Int64, ByVal ruleId As Int64)
    Private Delegate Sub ChangeCursorDelegate(ByVal cur As Cursor)

    Private Sub ChangeCursor(ByVal cur As Cursor)
        Try
            Cursor = cur
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


#Region " Código generado por el Diseñador de Windows Forms "

    Private Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub


    Public ReadOnly Property MyRule() As IRule Implements IZRuleControl.MyRule 'IWFRuleParent Implements IZRuleControl.MyRule
        Get
            Return Rule
        End Get
    End Property

    Public Shadows ReadOnly Property MyState() As IDoChangeState
        Get
            Return DirectCast(Rule, IDoChangeState)
        End Get
    End Property

    Public Event GoToErrorRule(ByVal Sender As Object, ByVal e As EventArgs) Implements Core.IZRuleControl.GoToErrorRule


    Public ReadOnly Property GoToErrorRuleComboBox As ComboBox Implements Core.IZRuleControl.GoToErrorRuleComboBox
        Get
            Return cmbRules
        End Get
    End Property

    Public Property HasBeenModified As Boolean Implements IZRuleControl.HasBeenModified


    Protected Rule As IRule

    Public Sub New(ByRef _rule As IRule)
        MyBase.New()
        InitializeComponent()

        LoadCommonControls(_rule)

    End Sub

    Sub New(ByRef _rule As IRule, ByRef _wfPanelCircuit As IWFPanelCircuit)
        MyBase.New()
        InitializeComponent()

        LoadCommonControls(_rule)

        If _wfPanelCircuit IsNot Nothing Then
            RemoveHandler OpenMissedRule, AddressOf _wfPanelCircuit.OpenMissedRule
            AddHandler OpenMissedRule, AddressOf _wfPanelCircuit.OpenMissedRule
        End If
    End Sub

    Private Sub LoadCommonControls(ByRef _rule As IRule)
        Rule = _rule

        Dim UCHabilitation As New UCHabilitation(Rule.ID, Rule.WFStepId)
        UCHabilitation.Dock = DockStyle.Fill
        tbHabilitation.Controls.Add(UCHabilitation)
    End Sub

    Public Event UpdateMaskName(ByRef rule As IRule) Implements IZRuleControl.UpdateMaskName
    Public Event UpdateRuleIcon(ByVal rule As IRule) Implements IZRuleControl.UpdateRuleIcon

    Protected Sub RaiseUpdateMaskName()
        WFRulesBusiness.UpdateRuleNameByID(Rule.ID, Rule.Name)
        RaiseEvent UpdateMaskName(Rule)
    End Sub

    Protected Sub InitializeComponent()
        Me.tbState = New System.Windows.Forms.TabPage()
        Me.btnEstadoValDef = New Zamba.AppBlock.ZButton()
        Me.lstStates = New System.Windows.Forms.ListBox()
        Me.lblSeleccionarEstado = New Zamba.AppBlock.ZLabel()
        Me.btnSeleccionar = New Zamba.AppBlock.ZButton()
        Me.cmbEtapa = New System.Windows.Forms.ComboBox()
        Me.Label1 = New Zamba.AppBlock.ZLabel()
        Me.tbHabilitation = New System.Windows.Forms.TabPage()
        Me.tbConfiguration = New System.Windows.Forms.TabPage()
        Me.chkEnabled = New System.Windows.Forms.CheckBox()
        Me.chkRemote = New System.Windows.Forms.CheckBox()
        Me.chkExecuteRuleWhenResult = New System.Windows.Forms.CheckBox()
        Me.chkRefreshRule = New System.Windows.Forms.CheckBox()
        Me.lblMessage = New Zamba.AppBlock.ZLabel()
        Me.btnSavePreferences = New Zamba.AppBlock.ZButton()
        Me.Label2 = New Zamba.AppBlock.ZLabel()
        Me.cmbCategoria = New System.Windows.Forms.ComboBox()
        Me.btnGoToRule = New Zamba.AppBlock.ZButton()
        Me.chkDisableChilds = New System.Windows.Forms.CheckBox()
        Me.cmbRules = New System.Windows.Forms.ComboBox()
        Me.chkExecuteRuleInCaseOfError = New System.Windows.Forms.CheckBox()
        Me.txtMessageToShowInCaseOfError = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.Label4 = New Zamba.AppBlock.ZLabel()
        Me.chkAsynchronous = New System.Windows.Forms.CheckBox()
        Me.chkSaveUpdateInHistory = New System.Windows.Forms.CheckBox()
        Me.txtComment = New Zamba.AppBlock.TextoInteligenteTextBox()
        Me.lblComment = New Zamba.AppBlock.ZLabel()
        Me.chkSaveUpdate = New System.Windows.Forms.CheckBox()
        Me.chkCleanRule = New System.Windows.Forms.CheckBox()
        Me.chkCloseTask = New System.Windows.Forms.CheckBox()
        Me.chkThrowExceptionIfCancel = New System.Windows.Forms.CheckBox()
        Me.chkContinueWithError = New System.Windows.Forms.CheckBox()
        Me.tbAlerts = New System.Windows.Forms.TabPage()
        Me.tbRule = New System.Windows.Forms.TabPage()
        Me.tbctrMain = New System.Windows.Forms.TabControl()
        Me.tbAsignation = New System.Windows.Forms.TabPage()
        Me.tbHelp = New System.Windows.Forms.TabPage()
        Me.splHelp = New System.Windows.Forms.SplitContainer()
        Me.BtnSaveHelp = New Zamba.AppBlock.ZButton()
        Me.lblHelp = New Zamba.AppBlock.ZLabel()
        Me.txtHelp = New System.Windows.Forms.TextBox()
        Me.tbCases = New System.Windows.Forms.TabPage()
        Me.tbState.SuspendLayout()
        Me.tbConfiguration.SuspendLayout()
        Me.tbctrMain.SuspendLayout()
        Me.tbHelp.SuspendLayout()
        CType(Me.splHelp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splHelp.Panel2.SuspendLayout()
        Me.splHelp.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbState
        '
        Me.tbState.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.tbState.Controls.Add(Me.btnEstadoValDef)
        Me.tbState.Controls.Add(Me.lstStates)
        Me.tbState.Controls.Add(Me.lblSeleccionarEstado)
        Me.tbState.Controls.Add(Me.btnSeleccionar)
        Me.tbState.Controls.Add(Me.cmbEtapa)
        Me.tbState.Controls.Add(Me.Label1)
        Me.tbState.Location = New System.Drawing.Point(4, 25)
        Me.tbState.Name = "tbState"
        Me.tbState.Size = New System.Drawing.Size(616, 629)
        Me.tbState.TabIndex = 4
        Me.tbState.Tag = Zamba.Core.RuleSectionOptions.Estado
        Me.tbState.Text = "Estado"
        Me.tbState.UseVisualStyleBackColor = True
        '
        'btnEstadoValDef
        '
        Me.btnEstadoValDef.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnEstadoValDef.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEstadoValDef.ForeColor = System.Drawing.Color.White
        Me.btnEstadoValDef.Location = New System.Drawing.Point(70, 277)
        Me.btnEstadoValDef.Name = "btnEstadoValDef"
        Me.btnEstadoValDef.Size = New System.Drawing.Size(112, 23)
        Me.btnEstadoValDef.TabIndex = 17
        Me.btnEstadoValDef.Text = "Valores por defecto"
        Me.btnEstadoValDef.UseVisualStyleBackColor = False
        '
        'lstStates
        '
        Me.lstStates.FormattingEnabled = True
        Me.lstStates.ItemHeight = 16
        Me.lstStates.Location = New System.Drawing.Point(40, 87)
        Me.lstStates.Name = "lstStates"
        Me.lstStates.Size = New System.Drawing.Size(289, 164)
        Me.lstStates.TabIndex = 16
        '
        'lblSeleccionarEstado
        '
        Me.lblSeleccionarEstado.BackColor = System.Drawing.Color.Transparent
        Me.lblSeleccionarEstado.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeleccionarEstado.FontSize = 9.75!
        Me.lblSeleccionarEstado.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblSeleccionarEstado.Location = New System.Drawing.Point(37, 68)
        Me.lblSeleccionarEstado.Name = "lblSeleccionarEstado"
        Me.lblSeleccionarEstado.Size = New System.Drawing.Size(120, 16)
        Me.lblSeleccionarEstado.TabIndex = 11
        Me.lblSeleccionarEstado.Text = "Nuevo estado"
        Me.lblSeleccionarEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSeleccionar
        '
        Me.btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnSeleccionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSeleccionar.ForeColor = System.Drawing.Color.White
        Me.btnSeleccionar.Location = New System.Drawing.Point(247, 277)
        Me.btnSeleccionar.Name = "btnSeleccionar"
        Me.btnSeleccionar.Size = New System.Drawing.Size(60, 23)
        Me.btnSeleccionar.TabIndex = 13
        Me.btnSeleccionar.Text = "Aceptar"
        Me.btnSeleccionar.UseVisualStyleBackColor = False
        '
        'cmbEtapa
        '
        Me.cmbEtapa.FormattingEnabled = True
        Me.cmbEtapa.Location = New System.Drawing.Point(133, 33)
        Me.cmbEtapa.Name = "cmbEtapa"
        Me.cmbEtapa.Size = New System.Drawing.Size(196, 24)
        Me.cmbEtapa.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.FontSize = 9.75!
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(72, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 16)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Etapa:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbHabilitation
        '
        Me.tbHabilitation.AutoScroll = True
        Me.tbHabilitation.BackColor = System.Drawing.Color.White
        Me.tbHabilitation.Location = New System.Drawing.Point(4, 25)
        Me.tbHabilitation.Name = "tbHabilitation"
        Me.tbHabilitation.Size = New System.Drawing.Size(616, 629)
        Me.tbHabilitation.TabIndex = 3
        Me.tbHabilitation.Tag = Zamba.Core.RuleSectionOptions.Habilitacion
        Me.tbHabilitation.Text = "Habilitación"
        '
        'tbConfiguration
        '
        Me.tbConfiguration.AutoScroll = True
        Me.tbConfiguration.BackColor = System.Drawing.Color.White
        Me.tbConfiguration.Controls.Add(Me.chkEnabled)
        Me.tbConfiguration.Controls.Add(Me.chkRemote)
        Me.tbConfiguration.Controls.Add(Me.chkExecuteRuleWhenResult)
        Me.tbConfiguration.Controls.Add(Me.chkRefreshRule)
        Me.tbConfiguration.Controls.Add(Me.lblMessage)
        Me.tbConfiguration.Controls.Add(Me.btnSavePreferences)
        Me.tbConfiguration.Controls.Add(Me.Label2)
        Me.tbConfiguration.Controls.Add(Me.cmbCategoria)
        Me.tbConfiguration.Controls.Add(Me.btnGoToRule)
        Me.tbConfiguration.Controls.Add(Me.chkDisableChilds)
        Me.tbConfiguration.Controls.Add(Me.cmbRules)
        Me.tbConfiguration.Controls.Add(Me.chkExecuteRuleInCaseOfError)
        Me.tbConfiguration.Controls.Add(Me.txtMessageToShowInCaseOfError)
        Me.tbConfiguration.Controls.Add(Me.Label4)
        Me.tbConfiguration.Controls.Add(Me.chkAsynchronous)
        Me.tbConfiguration.Controls.Add(Me.chkSaveUpdateInHistory)
        Me.tbConfiguration.Controls.Add(Me.txtComment)
        Me.tbConfiguration.Controls.Add(Me.lblComment)
        Me.tbConfiguration.Controls.Add(Me.chkSaveUpdate)
        Me.tbConfiguration.Controls.Add(Me.chkCleanRule)
        Me.tbConfiguration.Controls.Add(Me.chkCloseTask)
        Me.tbConfiguration.Controls.Add(Me.chkThrowExceptionIfCancel)
        Me.tbConfiguration.Controls.Add(Me.chkContinueWithError)
        Me.tbConfiguration.Location = New System.Drawing.Point(4, 25)
        Me.tbConfiguration.Name = "tbConfiguration"
        Me.tbConfiguration.Size = New System.Drawing.Size(616, 629)
        Me.tbConfiguration.TabIndex = 2
        Me.tbConfiguration.Tag = Zamba.Core.RuleSectionOptions.Configuracion
        Me.tbConfiguration.Text = "Configuración"
        '
        'chkEnabled
        '
        Me.chkEnabled.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Location = New System.Drawing.Point(18, 19)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(117, 20)
        Me.chkEnabled.TabIndex = 44
        Me.chkEnabled.Text = "Habilitar regla"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'chkRemote
        '
        Me.chkRemote.AutoSize = True
        Me.chkRemote.Enabled = False
        Me.chkRemote.Location = New System.Drawing.Point(18, 291)
        Me.chkRemote.Name = "chkRemote"
        Me.chkRemote.Size = New System.Drawing.Size(177, 20)
        Me.chkRemote.TabIndex = 45
        Me.chkRemote.Text = "Ejecutar Remotamente"
        Me.chkRemote.UseVisualStyleBackColor = True
        '
        'chkExecuteRuleWhenResult
        '
        Me.chkExecuteRuleWhenResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkExecuteRuleWhenResult.AutoSize = True
        Me.chkExecuteRuleWhenResult.Location = New System.Drawing.Point(18, 42)
        Me.chkExecuteRuleWhenResult.Name = "chkExecuteRuleWhenResult"
        Me.chkExecuteRuleWhenResult.Size = New System.Drawing.Size(298, 20)
        Me.chkExecuteRuleWhenResult.TabIndex = 42
        Me.chkExecuteRuleWhenResult.Text = "Ejecutar Regla cuando exista Documento"
        Me.chkExecuteRuleWhenResult.UseVisualStyleBackColor = True
        '
        'chkRefreshRule
        '
        Me.chkRefreshRule.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkRefreshRule.AutoSize = True
        Me.chkRefreshRule.Location = New System.Drawing.Point(18, 65)
        Me.chkRefreshRule.Name = "chkRefreshRule"
        Me.chkRefreshRule.Size = New System.Drawing.Size(352, 20)
        Me.chkRefreshRule.TabIndex = 43
        Me.chkRefreshRule.Text = "Actualizar tarea al finalizar la ejecucion de reglas"
        Me.chkRefreshRule.UseVisualStyleBackColor = True
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.FontSize = 9.75!
        Me.lblMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblMessage.Location = New System.Drawing.Point(18, 444)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblMessage.Size = New System.Drawing.Size(358, 13)
        Me.lblMessage.TabIndex = 64
        Me.lblMessage.Text = "lblMessage"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblMessage.Visible = False
        '
        'btnSavePreferences
        '
        Me.btnSavePreferences.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnSavePreferences.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSavePreferences.ForeColor = System.Drawing.Color.White
        Me.btnSavePreferences.Location = New System.Drawing.Point(382, 430)
        Me.btnSavePreferences.Name = "btnSavePreferences"
        Me.btnSavePreferences.Size = New System.Drawing.Size(100, 27)
        Me.btnSavePreferences.TabIndex = 63
        Me.btnSavePreferences.Text = "Guardar"
        Me.btnSavePreferences.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.FontSize = 9.75!
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(15, 407)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 16)
        Me.Label2.TabIndex = 62
        Me.Label2.Text = "Categoria"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbCategoria
        '
        Me.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategoria.FormattingEnabled = True
        Me.cmbCategoria.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        Me.cmbCategoria.Location = New System.Drawing.Point(101, 399)
        Me.cmbCategoria.Name = "cmbCategoria"
        Me.cmbCategoria.Size = New System.Drawing.Size(56, 24)
        Me.cmbCategoria.TabIndex = 61
        '
        'btnGoToRule
        '
        Me.btnGoToRule.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnGoToRule.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoToRule.ForeColor = System.Drawing.Color.White
        Me.btnGoToRule.Location = New System.Drawing.Point(346, 191)
        Me.btnGoToRule.Name = "btnGoToRule"
        Me.btnGoToRule.Size = New System.Drawing.Size(136, 23)
        Me.btnGoToRule.TabIndex = 60
        Me.btnGoToRule.Text = "Ir a la regla de destino"
        Me.btnGoToRule.UseVisualStyleBackColor = True
        '
        'chkDisableChilds
        '
        Me.chkDisableChilds.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkDisableChilds.AutoSize = True
        Me.chkDisableChilds.Enabled = False
        Me.chkDisableChilds.Location = New System.Drawing.Point(167, 19)
        Me.chkDisableChilds.Name = "chkDisableChilds"
        Me.chkDisableChilds.Size = New System.Drawing.Size(182, 20)
        Me.chkDisableChilds.TabIndex = 59
        Me.chkDisableChilds.Text = "Deshabilitar reglas hijas"
        Me.chkDisableChilds.UseVisualStyleBackColor = True
        '
        'cmbRules
        '
        Me.cmbRules.Enabled = False
        Me.cmbRules.FormattingEnabled = True
        Me.cmbRules.Location = New System.Drawing.Point(39, 218)
        Me.cmbRules.Name = "cmbRules"
        Me.cmbRules.Size = New System.Drawing.Size(443, 24)
        Me.cmbRules.TabIndex = 58
        '
        'chkExecuteRuleInCaseOfError
        '
        Me.chkExecuteRuleInCaseOfError.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkExecuteRuleInCaseOfError.AutoSize = True
        Me.chkExecuteRuleInCaseOfError.Location = New System.Drawing.Point(18, 197)
        Me.chkExecuteRuleInCaseOfError.Name = "chkExecuteRuleInCaseOfError"
        Me.chkExecuteRuleInCaseOfError.Size = New System.Drawing.Size(319, 20)
        Me.chkExecuteRuleInCaseOfError.TabIndex = 57
        Me.chkExecuteRuleInCaseOfError.Text = "Ejecutar la siguiente regla en caso de error:"
        Me.chkExecuteRuleInCaseOfError.UseVisualStyleBackColor = True
        '
        'txtMessageToShowInCaseOfError
        '
        Me.txtMessageToShowInCaseOfError.Location = New System.Drawing.Point(39, 147)
        Me.txtMessageToShowInCaseOfError.Name = "txtMessageToShowInCaseOfError"
        Me.txtMessageToShowInCaseOfError.Size = New System.Drawing.Size(443, 38)
        Me.txtMessageToShowInCaseOfError.TabIndex = 56
        Me.txtMessageToShowInCaseOfError.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.FontSize = 9.75!
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(36, 131)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(271, 16)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "Mostrar este mensaje en caso de error:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkAsynchronous
        '
        Me.chkAsynchronous.AutoSize = True
        Me.chkAsynchronous.Location = New System.Drawing.Point(216, 291)
        Me.chkAsynchronous.Name = "chkAsynchronous"
        Me.chkAsynchronous.Size = New System.Drawing.Size(203, 20)
        Me.chkAsynchronous.TabIndex = 54
        Me.chkAsynchronous.Text = "Ejecutar Asincronicamente"
        Me.chkAsynchronous.UseVisualStyleBackColor = True
        '
        'chkSaveUpdateInHistory
        '
        Me.chkSaveUpdateInHistory.AutoSize = True
        Me.chkSaveUpdateInHistory.Enabled = False
        Me.chkSaveUpdateInHistory.Location = New System.Drawing.Point(216, 314)
        Me.chkSaveUpdateInHistory.Name = "chkSaveUpdateInHistory"
        Me.chkSaveUpdateInHistory.Size = New System.Drawing.Size(212, 20)
        Me.chkSaveUpdateInHistory.TabIndex = 53
        Me.chkSaveUpdateInHistory.Text = "Agregar Novedad al historial"
        Me.chkSaveUpdateInHistory.UseVisualStyleBackColor = True
        '
        'txtComment
        '
        Me.txtComment.Enabled = False
        Me.txtComment.Location = New System.Drawing.Point(39, 350)
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(443, 40)
        Me.txtComment.TabIndex = 52
        Me.txtComment.Text = ""
        '
        'lblComment
        '
        Me.lblComment.AutoSize = True
        Me.lblComment.BackColor = System.Drawing.Color.Transparent
        Me.lblComment.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComment.FontSize = 9.75!
        Me.lblComment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblComment.Location = New System.Drawing.Point(36, 334)
        Me.lblComment.Name = "lblComment"
        Me.lblComment.Size = New System.Drawing.Size(186, 16)
        Me.lblComment.TabIndex = 51
        Me.lblComment.Text = "Comentario de la novedad:"
        Me.lblComment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkSaveUpdate
        '
        Me.chkSaveUpdate.AutoSize = True
        Me.chkSaveUpdate.Location = New System.Drawing.Point(18, 314)
        Me.chkSaveUpdate.Name = "chkSaveUpdate"
        Me.chkSaveUpdate.Size = New System.Drawing.Size(205, 20)
        Me.chkSaveUpdate.TabIndex = 50
        Me.chkSaveUpdate.Text = "Marcar Novedad al finalizar"
        Me.chkSaveUpdate.UseVisualStyleBackColor = True
        '
        'chkCleanRule
        '
        Me.chkCleanRule.AutoSize = True
        Me.chkCleanRule.Location = New System.Drawing.Point(18, 268)
        Me.chkCleanRule.Name = "chkCleanRule"
        Me.chkCleanRule.Size = New System.Drawing.Size(206, 20)
        Me.chkCleanRule.TabIndex = 49
        Me.chkCleanRule.Text = "Limpiar variables al finalizar"
        Me.chkCleanRule.UseVisualStyleBackColor = True
        '
        'chkCloseTask
        '
        Me.chkCloseTask.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkCloseTask.AutoSize = True
        Me.chkCloseTask.Location = New System.Drawing.Point(18, 245)
        Me.chkCloseTask.Name = "chkCloseTask"
        Me.chkCloseTask.Size = New System.Drawing.Size(262, 20)
        Me.chkCloseTask.TabIndex = 48
        Me.chkCloseTask.Text = "Cerrar tarea al finalizar la ejecucion"
        Me.chkCloseTask.UseVisualStyleBackColor = True
        '
        'chkThrowExceptionIfCancel
        '
        Me.chkThrowExceptionIfCancel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkThrowExceptionIfCancel.AutoSize = True
        Me.chkThrowExceptionIfCancel.Location = New System.Drawing.Point(18, 88)
        Me.chkThrowExceptionIfCancel.Name = "chkThrowExceptionIfCancel"
        Me.chkThrowExceptionIfCancel.Size = New System.Drawing.Size(353, 20)
        Me.chkThrowExceptionIfCancel.TabIndex = 47
        Me.chkThrowExceptionIfCancel.Text = "Al cancelar la regla ejecutar accion de excepcion"
        Me.chkThrowExceptionIfCancel.UseVisualStyleBackColor = True
        '
        'chkContinueWithError
        '
        Me.chkContinueWithError.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkContinueWithError.AutoSize = True
        Me.chkContinueWithError.Location = New System.Drawing.Point(18, 111)
        Me.chkContinueWithError.Name = "chkContinueWithError"
        Me.chkContinueWithError.Size = New System.Drawing.Size(413, 20)
        Me.chkContinueWithError.TabIndex = 46
        Me.chkContinueWithError.Text = "Continuar la ejecución de la tarea si la regla provoca error"
        Me.chkContinueWithError.UseVisualStyleBackColor = True
        '
        'tbAlerts
        '
        Me.tbAlerts.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.tbAlerts.Location = New System.Drawing.Point(4, 25)
        Me.tbAlerts.Name = "tbAlerts"
        Me.tbAlerts.Padding = New System.Windows.Forms.Padding(3)
        Me.tbAlerts.Size = New System.Drawing.Size(616, 629)
        Me.tbAlerts.TabIndex = 1
        Me.tbAlerts.Tag = Zamba.Core.RuleSectionOptions.Alerta
        Me.tbAlerts.Text = "Alertas"
        Me.tbAlerts.UseVisualStyleBackColor = True
        '
        'tbRule
        '
        Me.tbRule.AutoScroll = True
        Me.tbRule.BackColor = System.Drawing.Color.White
        Me.tbRule.Location = New System.Drawing.Point(4, 25)
        Me.tbRule.Name = "tbRule"
        Me.tbRule.Padding = New System.Windows.Forms.Padding(3)
        Me.tbRule.Size = New System.Drawing.Size(616, 629)
        Me.tbRule.TabIndex = 0
        Me.tbRule.Tag = Zamba.Core.RuleSectionOptions.Regla
        Me.tbRule.Text = "Regla"
        '
        'tbctrMain
        '
        Me.tbctrMain.Controls.Add(Me.tbRule)
        Me.tbctrMain.Controls.Add(Me.tbAlerts)
        Me.tbctrMain.Controls.Add(Me.tbConfiguration)
        Me.tbctrMain.Controls.Add(Me.tbHabilitation)
        Me.tbctrMain.Controls.Add(Me.tbState)
        Me.tbctrMain.Controls.Add(Me.tbAsignation)
        Me.tbctrMain.Controls.Add(Me.tbHelp)
        Me.tbctrMain.Controls.Add(Me.tbCases)
        Me.tbctrMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbctrMain.Location = New System.Drawing.Point(0, 0)
        Me.tbctrMain.Name = "tbctrMain"
        Me.tbctrMain.SelectedIndex = 0
        Me.tbctrMain.Size = New System.Drawing.Size(624, 658)
        Me.tbctrMain.TabIndex = 0
        '
        'tbAsignation
        '
        Me.tbAsignation.Location = New System.Drawing.Point(4, 25)
        Me.tbAsignation.Name = "tbAsignation"
        Me.tbAsignation.Size = New System.Drawing.Size(616, 629)
        Me.tbAsignation.TabIndex = 5
        Me.tbAsignation.Tag = Zamba.Core.RuleSectionOptions.Asignacion
        Me.tbAsignation.Text = "Asignación"
        Me.tbAsignation.UseVisualStyleBackColor = True
        '
        'tbHelp
        '
        Me.tbHelp.Controls.Add(Me.splHelp)
        Me.tbHelp.Location = New System.Drawing.Point(4, 25)
        Me.tbHelp.Name = "tbHelp"
        Me.tbHelp.Size = New System.Drawing.Size(616, 629)
        Me.tbHelp.TabIndex = 6
        Me.tbHelp.Tag = Zamba.Core.RuleSectionOptions.Ayuda
        Me.tbHelp.Text = "Ayuda"
        Me.tbHelp.UseVisualStyleBackColor = True
        '
        'splHelp
        '
        Me.splHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.splHelp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splHelp.Location = New System.Drawing.Point(0, 0)
        Me.splHelp.Name = "splHelp"
        Me.splHelp.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splHelp.Panel2
        '
        Me.splHelp.Panel2.Controls.Add(Me.BtnSaveHelp)
        Me.splHelp.Panel2.Controls.Add(Me.lblHelp)
        Me.splHelp.Panel2.Controls.Add(Me.txtHelp)
        Me.splHelp.Size = New System.Drawing.Size(616, 629)
        Me.splHelp.SplitterDistance = 354
        Me.splHelp.TabIndex = 0
        '
        'BtnSaveHelp
        '
        Me.BtnSaveHelp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSaveHelp.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnSaveHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSaveHelp.ForeColor = System.Drawing.Color.White
        Me.BtnSaveHelp.Location = New System.Drawing.Point(531, 194)
        Me.BtnSaveHelp.Name = "BtnSaveHelp"
        Me.BtnSaveHelp.Size = New System.Drawing.Size(70, 27)
        Me.BtnSaveHelp.TabIndex = 38
        Me.BtnSaveHelp.Text = "Guardar"
        Me.BtnSaveHelp.UseVisualStyleBackColor = False
        '
        'lblHelp
        '
        Me.lblHelp.AutoSize = True
        Me.lblHelp.BackColor = System.Drawing.Color.Transparent
        Me.lblHelp.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHelp.FontSize = 9.75!
        Me.lblHelp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.lblHelp.Location = New System.Drawing.Point(13, 14)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(163, 16)
        Me.lblHelp.TabIndex = 37
        Me.lblHelp.Text = "Descripcion de la regla:"
        Me.lblHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtHelp
        '
        Me.txtHelp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtHelp.Location = New System.Drawing.Point(16, 30)
        Me.txtHelp.Multiline = True
        Me.txtHelp.Name = "txtHelp"
        Me.txtHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtHelp.Size = New System.Drawing.Size(585, 158)
        Me.txtHelp.TabIndex = 36
        '
        'tbCases
        '
        Me.tbCases.Location = New System.Drawing.Point(4, 25)
        Me.tbCases.Name = "tbCases"
        Me.tbCases.Padding = New System.Windows.Forms.Padding(3)
        Me.tbCases.Size = New System.Drawing.Size(616, 629)
        Me.tbCases.TabIndex = 7
        Me.tbCases.Tag = Zamba.Core.RuleSectionOptions.CasosDePrueba
        Me.tbCases.Text = "Casos de Prueba"
        Me.tbCases.UseVisualStyleBackColor = True
        '
        'ZRuleControl
        '
        Me.AutoScroll = True
        Me.Controls.Add(Me.tbctrMain)
        Me.Name = "ZRuleControl"
        Me.Size = New System.Drawing.Size(624, 658)
        Me.tbState.ResumeLayout(False)
        Me.tbConfiguration.ResumeLayout(False)
        Me.tbConfiguration.PerformLayout()
        Me.tbctrMain.ResumeLayout(False)
        Me.tbHelp.ResumeLayout(False)
        Me.splHelp.Panel2.ResumeLayout(False)
        Me.splHelp.Panel2.PerformLayout()
        CType(Me.splHelp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splHelp.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Atributos"

    ' Variable utilizada para contener una instancia de UCMail (un UserControl)
    Private ucMail As UCMail2
    ' Variable utilizada para contener una instancia de UCAsign (un UserControl)
    Private ucAsign As UCAsign
    ' Variable utilizada para contener una instancia de UCHelp (un UserControl)
    Private UcHelp As UCHelp
    ' Variable utilizada para contener una instancia de UCTestCase (un UserControl)
    'Private UCTestCase As UCTestCase = Nothing
    ' Variable que actua como bandera, utilizada para no ir al evento Selected_IndexChanges mientras se ejecuta el método loadStages, ya que en
    ' cierta parte del método se llama en forma involuntaria a ese evento, lo que daría un error en tiempo de ejecución
    Private bool As Boolean = False
    Private bool2 As Boolean = False

    ' Variable utilizada para guardar el id de la etapa que se recupera de la base de datos (solapa Estado)
    Private idSelectStage As Integer = -1
    ' Variable utilizada para guardar el id del estado seleccionado que se recupera de la base de datos (solapa Estado)
    Private idSelectState As Integer = -1

    ' Variable utilizada para almacenar el tipo de radioButton que estaba seleccionado cuando se guardo. Así, cuando se recupera de la base de datos
    ' se obtiene el tipo de radioButton. Si no, (si no hay nada en la base de datos) value es cero
    Private value As RulePreferences

    'Variables utilizadas en la solapa de configuracion de la regla

#End Region


    ''' <summary>
    ''' Carga el contenido de la solapa seleccionada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbctrMain_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles tbctrMain.SelectedIndexChanged
        Try
            'Se obtiene el tipo de solapa seleccionada
            MyRuleSelectionOption = DirectCast(tbctrMain.SelectedTab.Tag, RuleSectionOptions)

            Select Case MyRuleSelectionOption
                Case RuleSectionOptions.Alerta
                    tabAlert()
                Case RuleSectionOptions.Asignacion
                    tabAsignation()
                Case RuleSectionOptions.Ayuda
                    tabHelp()
                Case RuleSectionOptions.CasosDePrueba
                    LoadTestCaseTab()
                Case RuleSectionOptions.Estado
                    tabState()
                Case RuleSectionOptions.Configuracion
                    tabConfiguration()
            End Select
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

#Region "Eventos de la solapa Configuracion"

    ''' <summary>
    ''' Carga las preferencias de la regla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub tabConfiguration()
        Try
            RemoveConfigurationHandlers()
            If Rule.RuleClass = "IfBranch" Then
                chkEnabled.Checked = True
                chkDisableChilds.Checked = False
                chkEnabled.Enabled = False
                chkDisableChilds.Enabled = False
            Else
                chkEnabled.Checked = Rule.Enable
                '                Me.chkEnabled.Checked = WFRulesBusiness.GetRuleEstado(Me.Rule.ID, False)
                If Not chkEnabled.Checked Then
                    chkDisableChilds.Enabled = True
                End If
                If Not Rule.DisableChildRules Is Nothing Then
                    chkDisableChilds.Checked = Rule.DisableChildRules
                End If
            End If
            AddConfigurationHandlers()

            chkExecuteRuleWhenResult.Checked = Rule.ExecuteWhenResult
            If Not Rule.RefreshRule Is Nothing Then
                chkRefreshRule.Checked = Rule.RefreshRule
            End If
            If Not Rule.CloseTask Is Nothing Then
                chkCloseTask.Checked = Rule.CloseTask
            End If
            If Not Rule.CleanRule Is Nothing Then
                chkCleanRule.Checked = Rule.CleanRule
            End If
            If Not Rule.SaveUpdate Is Nothing Then
                chkSaveUpdate.Checked = Rule.SaveUpdate
            End If
            If Not String.IsNullOrEmpty(Rule.UpdateComment) Then
                txtComment.Text = Rule.UpdateComment
            End If
            If Not Rule.SaveUpdateInHistory Is Nothing Then
                chkSaveUpdateInHistory.Checked = Rule.SaveUpdateInHistory
            End If
            If Not Rule.Asynchronous Is Nothing Then
                chkAsynchronous.Checked = Rule.Asynchronous
            End If
            If Not Rule.ExecuteRuleInCaseOfError Is Nothing Then
                chkExecuteRuleInCaseOfError.Checked = Rule.ExecuteRuleInCaseOfError
                cmbRules.Enabled = chkExecuteRuleInCaseOfError.Checked
            End If
            If Not Rule.ContinueWithError Is Nothing Then
                chkContinueWithError.Checked = Rule.ContinueWithError
            End If
            If Not Rule.ThrowExceptionIfCancel Is Nothing Then
                chkThrowExceptionIfCancel.Checked = Rule.ThrowExceptionIfCancel
            End If
            If Not Rule.Category Is Nothing Then
                cmbCategoria.SelectedIndex = Rule.Category - 1
            Else
                cmbCategoria.SelectedIndex = 0
            End If

            txtMessageToShowInCaseOfError.Text = Rule.MessageToShowInCaseOfError
            lblMessage.Text = String.Empty
            FillRules()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Quita los handlers especificos del panel de configuracion.
    ''' </summary>
    ''' <remarks>Este método fue agregado por un problema que se perdian los handlers de los eventos</remarks>
    Private Sub RemoveConfigurationHandlers()
        RemoveHandler chkEnabled.CheckedChanged, AddressOf chkEnabled_CheckedChanged
        RemoveHandler chkSaveUpdate.CheckedChanged, AddressOf chkEnabled_CheckedChanged
        RemoveHandler chkContinueWithError.CheckedChanged, AddressOf chkContinueWithError_CheckedChanged
        RemoveHandler chkExecuteRuleInCaseOfError.CheckedChanged, AddressOf chkExecuteRuleInCaseOfError_CheckedChanged
        RemoveHandler btnSavePreferences.Click, AddressOf btnSavePreferences_Click
        RemoveHandler btnGoToRule.Click, AddressOf btnGoToRule_Click
    End Sub

    ''' <summary>
    ''' Agrega los handlers especificos del panel de configuracion.
    ''' </summary>
    ''' <remarks>Este método fue agregado por un problema que se perdian los handlers de los eventos</remarks>
    Private Sub AddConfigurationHandlers()
        AddHandler chkEnabled.CheckedChanged, AddressOf chkEnabled_CheckedChanged
        AddHandler chkSaveUpdate.CheckedChanged, AddressOf chkSaveUpdate_CheckedChanged
        AddHandler chkContinueWithError.CheckedChanged, AddressOf chkContinueWithError_CheckedChanged
        AddHandler chkExecuteRuleInCaseOfError.CheckedChanged, AddressOf chkExecuteRuleInCaseOfError_CheckedChanged
        AddHandler btnSavePreferences.Click, AddressOf btnSavePreferences_Click
        AddHandler btnGoToRule.Click, AddressOf btnGoToRule_Click
    End Sub

    ''' <summary>
    ''' Guarda las preferencias de la regla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSavePreferences_Click(sender As System.Object, e As EventArgs)
        Try
            If Rule.ExecuteWhenResult <> chkExecuteRuleWhenResult.Checked Then
                If chkExecuteRuleWhenResult.Checked Then
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.ConfigurationExecuteWhenResult, 1)
                Else
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.ConfigurationExecuteWhenResult, 0)
                End If
                Rule.ExecuteWhenResult = chkExecuteRuleWhenResult.Checked
            End If

            If Rule.RefreshRule <> chkRefreshRule.Checked Then
                If chkRefreshRule.Checked Then
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.RefreshRule, 1)
                Else
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.RefreshRule, 0)
                End If
                Rule.RefreshRule = chkRefreshRule.Checked
                RaiseEvent UpdateRuleIcon(Rule)
            End If

            If Rule.SaveUpdate <> chkSaveUpdate.Checked Then
                If chkSaveUpdate.Checked Then
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.SaveUpdate, 1)
                Else
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.SaveUpdate, 0)
                End If
                Rule.SaveUpdate = chkSaveUpdate.Checked
            End If

            If Rule.UpdateComment <> txtComment.Text Then
                Rule.UpdateComment = txtComment.Text
                WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.Comment, 0, Rule.UpdateComment)
            End If

            If Rule.SaveUpdateInHistory <> chkSaveUpdateInHistory.Checked Then
                If chkSaveUpdateInHistory.Checked Then
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.SaveUpdateInHistory, 1)
                Else
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.SaveUpdateInHistory, 0)
                End If
                Rule.SaveUpdateInHistory = chkSaveUpdateInHistory.Checked
            End If

            If Rule.Enable <> chkEnabled.Checked Then
                Rule.Enable = chkEnabled.Checked
                WFRulesBusiness.SetRuleEstado(Rule.ID, chkEnabled.Checked)
                chkDisableChilds.Enabled = Not chkEnabled.Checked
                If chkEnabled.Checked Then
                    chkDisableChilds.Checked = False
                End If
                RaiseEvent UpdateRuleIcon(Rule)
            End If

            If Rule.DisableChildRules <> chkDisableChilds.Checked Then
                If chkDisableChilds.Checked Then
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.DisableChildRules, 1)
                Else
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.DisableChildRules, 0)
                End If
                Rule.DisableChildRules = chkDisableChilds.Checked
                RaiseEvent UpdateRuleIcon(Rule)
            End If

            If Rule.ContinueWithError <> chkContinueWithError.Checked Then
                If chkContinueWithError.Checked Then
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.ContinueWithError, 1)
                Else
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.ContinueWithError, 0)
                End If
                Rule.ContinueWithError = chkContinueWithError.Checked

                If chkContinueWithError.Checked Then
                    chkExecuteRuleInCaseOfError.Checked = False
                    Rule.ExecuteRuleInCaseOfError = False
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.ExecuteRuleInCaseOfError, False)
                End If
            End If

            If Rule.CloseTask <> chkCloseTask.Checked Then
                If chkCloseTask.Checked Then
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.CloseTask, 1)
                Else
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.CloseTask, 0)
                End If
                Rule.CloseTask = chkCloseTask.Checked
            End If

            If Rule.CleanRule <> chkCleanRule.Checked Then
                If chkCleanRule.Checked Then
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.CleanRule, 1)
                Else
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.CleanRule, 0)
                End If
                Rule.CleanRule = chkCleanRule.Checked
            End If

            If Rule.Asynchronous <> chkAsynchronous.Checked Then
                If chkAsynchronous.Checked Then
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.Asynchronous, 1)
                Else
                    WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.Asynchronous, 0)
                End If
                Rule.Asynchronous = chkAsynchronous.Checked
            End If

            If Rule.MessageToShowInCaseOfError <> txtMessageToShowInCaseOfError.Text Then
                Rule.MessageToShowInCaseOfError = txtMessageToShowInCaseOfError.Text
                WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.MessageToShowInCaseOfError, 0, Rule.MessageToShowInCaseOfError)
            End If

            If Rule.ExecuteRuleInCaseOfError <> chkExecuteRuleInCaseOfError.Checked Then
                Rule.ExecuteRuleInCaseOfError = chkExecuteRuleInCaseOfError.Checked
                WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.ExecuteRuleInCaseOfError, Rule.ExecuteRuleInCaseOfError)
            End If

            If cmbRules.Enabled AndAlso Rule.ExecuteRuleInCaseOfError AndAlso Rule.RuleIdToExecuteAfterError <> CInt(cmbRules.SelectedValue) Then
                Rule.RuleIdToExecuteAfterError = CInt(cmbRules.SelectedValue)
                WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.RuleIdToExecuteAfterError, Rule.RuleIdToExecuteAfterError)
            End If

            If Rule.ThrowExceptionIfCancel <> chkThrowExceptionIfCancel.Checked Then
                Rule.ThrowExceptionIfCancel = chkThrowExceptionIfCancel.Checked
                WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.ThrowExceptionIfCancel, Rule.ThrowExceptionIfCancel)
            End If

            If Rule.Category <> CShort(cmbCategoria.Text) Then
                Rule.Category = CShort(cmbCategoria.Text)
                WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.RuleCategory, Rule.Category)
            End If

            lblMessage.Text = "Preferencias guardadas correctamente"
            lblMessage.ForeColor = Color.FromArgb(76, 76, 76)
            lblMessage.Visible = True
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Ha ocurrido un error al guardar las preferencias de la regla", "Zamba Administrador", MessageBoxButtons.OK, MessageBoxIcon.Error)
            lblMessage.Text = "Ha ocurrido un error al guardar las preferencias"
            lblMessage.ForeColor = Color.DarkRed
            lblMessage.Visible = True
        End Try
    End Sub

    ''' <summary>
    ''' Evento disparado al modificar la opcion de agregar comentarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkSaveUpdate_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        txtComment.Enabled = chkSaveUpdate.Checked
        chkSaveUpdateInHistory.Enabled = chkSaveUpdate.Checked
    End Sub

    ''' <summary>
    ''' Evento disparado al modificar la opcion de habilitar regla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        chkDisableChilds.Enabled = Not chkEnabled.Checked
        If chkEnabled.Checked Then
            chkDisableChilds.Checked = False
        End If
    End Sub

    ''' <summary>
    ''' Evento disparado al modificar la opcion de continuar con errores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkContinueWithError_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        txtMessageToShowInCaseOfError.Enabled = Not chkContinueWithError.Checked
        chkExecuteRuleInCaseOfError.Enabled = Not chkContinueWithError.Checked
        cmbRules.Enabled = Not chkContinueWithError.Checked AndAlso chkExecuteRuleInCaseOfError.Checked

        If chkExecuteRuleInCaseOfError.Checked AndAlso cmbRules.Items.Count = 0 Then
            FillRules()
        End If
    End Sub

    ''' <summary>
    ''' Evento disparado al modificar la opcion de ejecutar una regla en caso de error
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkExecuteRuleInCaseOfError_CheckedChanged(sender As System.Object, e As EventArgs)
        If chkExecuteRuleInCaseOfError.Checked AndAlso cmbRules.Items.Count = 0 Then
            FillRules()
        End If
        cmbRules.Enabled = chkExecuteRuleInCaseOfError.Checked
        btnGoToRule.Enabled = chkExecuteRuleInCaseOfError.Checked
    End Sub

    ''' <summary>
    ''' Evento disparado al presionar el boton de Ir a regla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGoToRule_Click(ByVal sender As System.Object, ByVal e As EventArgs)
        If Not IsNothing(cmbRules.SelectedValue) Then
            Dim wfbe As New WFBusinessExt
            Try
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.WaitCursor})

                Dim ruleId As Int64 = Int64.Parse(cmbRules.SelectedValue)
                RaiseEvent OpenMissedRule(wfbe.GetWorkflowIdByRule(ruleId), ruleId)

            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                wfbe = Nothing
                Invoke(New ChangeCursorDelegate(AddressOf ChangeCursor), New Object() {Cursors.Default})
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Completa el combo de reglas solamente si se encuentra habilitada la opcion de ejecutar reglas en caso de error.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub FillRules()
        Try
            'If chkExecuteRuleInCaseOfError.Checked = True Then
            Dim dt As DataTable = WFRulesBusiness.GetDoExecuteRules().Tables(0)
            cmbRules.DataSource = dt
            cmbRules.DisplayMember = dt.Columns(0).ColumnName '  "NAME"
            cmbRules.ValueMember = dt.Columns(1).ColumnName    '"ID"
            If Not Rule.RuleIdToExecuteAfterError Is Nothing Then
                cmbRules.SelectedValue = Rule.RuleIdToExecuteAfterError.Value
            End If
            'End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#End Region

#Region "Eventos de la solapa Estado"

    ''' <summary>
    ''' Evento que se ejecuta cuando se selecciona un elemento del comboBox Etapa ubicado dentro de la solapa Estado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]    Aprox. 10/04/2008	Created
    ''' </history>
    Private Sub cmbEtapa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles cmbEtapa.SelectedIndexChanged

        If (bool = False) Then
            loadStates(cmbEtapa.SelectedValue)
        End If

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se presiona el botón Seleccionar ubicado dentro de la solapa Estado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]    Aprox. 15/04/2008	Created
    ''' </history>
    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnSeleccionar.Click

        Try

            If (lstStates.SelectedItems.Count = 1) Then

                ' Se guarda el id de la etapa selecionada (en el combobox) en la base de datos
                ' Parámetros: Número de Regla, Tipo de Solapa, Identifica a una Etapa, Id de la Etapa seleccionada 
                WFBusiness.SetRulesPreferences(MyRule.ID, RuleSectionOptions.Estado, RulePreferences.StateTypeStage, cmbEtapa.SelectedValue)

                ' Se guarda el id del estado selecionado (en el combobox) en la base de datos
                ' Parámetros: Número de Regla, Tipo de Solapa, Identifica a un Estado, Id del Estado seleccionado
                WFBusiness.SetRulesPreferences(MyRule.ID, RuleSectionOptions.Estado, RulePreferences.StateTypeState, lstStates.SelectedItems(0).Tag)

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

#Region "Métodos de la solapa Alerta"

    ''' <summary>
    ''' Método que se ejecuta tras la selección de la solapa Alerta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	27/05/2008	Created
    ''' </history>
    Private Sub tabAlert()

        ' Si el userControl ucMail es nulo, entonces se crea y se lo coloca adentro del panel Alertas
        If (ucMail Is Nothing) Then
            ucMail = New UCMail2(MyRule.WFStepId, MyRule.ID)
            ucMail.Dock = DockStyle.Fill
            tbAlerts.Controls.Add(ucMail)
        End If

    End Sub

#End Region

#Region "Métodos de la solapa Estado"

    ''' <summary>
    ''' Método que se ejecuta tras la selección de la solapa Estado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	27/05/2008	Created
    ''' </history>
    Private Sub tabState()
        ' Se recupera la etapa seleccionada (cuando se guardo), de lo contrario se seleccionara la primera etapa por defecto
        idSelectStage = Integer.Parse(WFRulesBusiness.GetRuleOption(MyRule.ID, RuleSectionOptions.Estado, RulePreferences.StateTypeStage, 0, False, MyRule.WFStepId, WFRulesBusiness.ValueTypes.Normal))
        '        recoverStageFromDataBase(RuleSectionOptions.Estado, RulePreferences.StateTypeStage)
        ' Se recupera el estado seleccionado (cuando se guardo), de lo contrario se seleccionara el primer estado por defecto
        idSelectState = Integer.Parse(WFRulesBusiness.GetRuleOption(MyRule.ID, RuleSectionOptions.Estado, RulePreferences.StateTypeState, 0, False, MyRule.WFStepId, WFRulesBusiness.ValueTypes.Normal))
        '       recoverElementsDisableOrSelectedFromDataBase(RuleSectionOptions.Estado, RulePreferences.StateTypeState, Nothing)
        ' Se recupera la etapa actual y los estados de esa etapa
        recoverAndLoadStageAndStates()
    End Sub


    ''' <summary>
    ''' Método que carga las etapas en el combobox Etapa de la solapa Estado
    ''' </summary>
    ''' <param name="i_dsSteps"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	18/04/2008	Created
    ''' </history>
    Private Sub loadStagesSinceTabState(ByRef i_dsSteps As DsSteps)

        ' Se coloca la bandera bool en True porque cuando el cmbEtapa reciba en su propiedad DataSource a i_dsSteps.WFSteps, en vez de continuar
        ' con el código (lo que hay debajo del DataSource), va a saltar directamente al evento cmbEtapa_SelectedIndexChanged. Allí, la bandera está 
        ' puesta en False para que no se vaya al loadStates, sino va a dar error. Después del evento, va a continuar con lo que hay debajo del DataSource
        bool = True
        cmbEtapa.SuspendLayout()

        cmbEtapa.DataSource = i_dsSteps.WFSteps
        cmbEtapa.ValueMember = "Step_Id"
        cmbEtapa.DisplayMember = "Name"

        cmbEtapa.ResumeLayout()
        bool = False

    End Sub

    ''' <summary>
    ''' Méotodo que carga los estados de la etapa en la lista de la solapa Estado
    ''' </summary>
    ''' <param name="Wfstep"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	18/04/2008	Created
    ''' </history>
    Private Sub loadStatesSinceTabState(ByRef Wfstep As WFStep)
        lstStates.Items.Clear()

        For Each estado As WFStepState In Wfstep.States
            lstStates.Items.Add(New StateItem(estado))
        Next

        lstStates.DisplayMember = "Text"
        checkIfStateExists()
    End Sub

    ''' <summary>
    ''' Método utilizado para ver si ya existe una etapa en una base de datos. Si existe, entonces el elemento del combobox  cmbEtapa que tenga el
    ''' id de etapa queda seleccionado por defecto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]14/04/2008	Created
    ''' </history>
    Private Sub checkIfStageExists()

        If (tbctrMain.SelectedTab.Text = "Estado") Then

            ' Si el id de etapa existe (en la base de datos)
            If (idSelectStage <> -1) Then
                checkStage(idSelectStage)
            End If

        End If

    End Sub

    ''' <summary>
    ''' Método utilizado para seleccionar por defecto la etapa (recuperada de la base de datos) en el combobox correspondiente 
    ''' </summary>
    ''' <param name="idStageSelect: Id de la etapa  "></param>
    ''' <param name="cmbCombo: ComboBox correspondiente a la solapa seleccionada (Estado o Habilitación)"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]    17/04/2008	Created
    ''' </history>
    Private Sub checkStage(ByVal idStageSelect As Integer)

        For i As Byte = 0 To cmbEtapa.Items.Count - 1

            Dim stage As DataRowView = cmbEtapa.Items(i)

            If (stage.Item(0) = idStageSelect) Then
                cmbEtapa.SelectedValue = stage.Item(0)
                Exit For
            End If

        Next

    End Sub

    ''' <summary>
    ''' Método utilizado para ver si ya existe un estado en una base de datos. Si existe, entonces el elemento del listbox (lstStates) que tenga el
    ''' id de estado queda seleccionado por defecto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	14/04/2008	Created
    ''' </history>
    Private Sub checkIfStateExists()

        ' Si el id de estado existe (en la base de datos)
        If (idSelectState <> -1) Then

            ' Se recorre el listbox lstStates para buscar el estado que corresponde a idStateSelect y dejarlo por default seleccionado
            For i As Byte = 0 To lstStates.Items.Count - 1

                Dim state As StateItem = lstStates.Items(i)

                If (state.Tag = idSelectState) Then
                    lstStates.SelectedIndex = i
                    Exit For
                End If

            Next

        End If

    End Sub

#End Region

#Region "Métodos de la solapa Asignación"

    ''' <summary>
    ''' Método que se ejecuta tras la selección de la solapa Alerta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	02/07/2008	Created
    ''' </history>
    Private Sub tabAsignation()

        ' Si el userControl ucAsign es nulo, entonces se crea y se lo coloca adentro del panel Asignación
        If (ucAsign Is Nothing) Then
            ucAsign = New UCAsign(MyRule)
            ucAsign.Dock = DockStyle.Fill
            tbAsignation.Controls.Add(ucAsign)
            tbAsignation.Controls(0).Refresh()
        Else
            tbAsignation.Controls(0).Refresh()
        End If

    End Sub

#End Region

#Region "Métodos de la solapa Ayuda"

    ''' <summary>
    ''' Método que se obtiene la ayuda de la regla
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Martin]	01/04/2009	Created
    '''     [Marcelo]	02/09/2009	Modified
    ''' </history>
    Private Sub tabHelp()
        If (UcHelp Is Nothing) Then
            UcHelp = New UCHelp(MyRule)
            UcHelp.Dock = DockStyle.Fill
            splHelp.Panel1.Controls.Add(UcHelp)
            splHelp.Panel1.Controls(0).Refresh()
        Else
            splHelp.Panel1.Controls(0).Refresh()
        End If
        txtHelp.Text = Rule.Description
    End Sub
    Private Sub BtnSaveHelp_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnSaveHelp.Click
        WFBusiness.SetRulesPreferences(Rule.ID, RuleSectionOptions.Configuracion, RulePreferences.RuleHelp, 0, txtHelp.Text)

        Rule.Description = txtHelp.Text
    End Sub
#End Region

#Region "Métodos de la solapa Casos de Prueba"
    ''' <summary>
    ''' Método que carga los casos de prueba de la regla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTestCaseTab()
        'If UCTestCase Is Nothing Then
        '    UCTestCase = New UCTestCase(ObjectTypes.WFRules, MyRule.ID)
        '    UCTestCase.Dock = DockStyle.Fill
        '    tbCases.Controls.Add(UCTestCase)
        'End If
        If tbCases.Controls.Count = 0 Then
            Dim btnOpenTestCases As New Button()
            btnOpenTestCases.Text = "Abrir casos de prueba"
            btnOpenTestCases.Width = 150
            btnOpenTestCases.Height = 30
            btnOpenTestCases.Left = 50
            btnOpenTestCases.Top = 50
            AddHandler btnOpenTestCases.Click, AddressOf btnOpenTestCases_Click
            tbCases.Controls.Add(btnOpenTestCases)
        End If

        tbCases.Controls(0).Refresh()
    End Sub

    Private Sub btnOpenTestCases_Click(sender As Object, e As EventArgs)
        Try
            Dim ztcApp As String = ZOptBusiness.GetValue("ZTCApplication")
            Dim ztcPath As String = System.Windows.Forms.Application.StartupPath & "\" & ztcApp
            Dim params As String = ObjectTypes.WFRules & " " & MyRule.ID.ToString & " " & Membership.MembershipHelper.CurrentUser.Name & " " & Membership.MembershipHelper.CurrentUser.Password
            ZTrace.WriteLineIf(ZTrace.IsInfo, "ZTC: " & ztcPath)
            If IO.File.Exists(ztcPath) Then
                System.Diagnostics.Process.Start(ztcPath, params)
                Dim form As WaitForm = New WaitForm
                form.Show()
            Else
                MessageBox.Show("No se ha encontrado el módulo de casos de prueba", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Error al abrir el módulo de casos de prueba", "Zamba Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "Métodos utilizados por la solapa Habilitación y la solapa Estado"


    ''' <summary>
    ''' Método utilizado para cargar las etapas y los estados (de la solapa Habilitación o Estado)
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	15/04/2008	Created
    ''' </history>
    Private Sub recoverAndLoadStageAndStates()
        If (cmbEtapa.DataSource Is Nothing) Then
            loadStages()
            If (cmbEtapa.Items.Count > 0) Then
                loadStates(cmbEtapa.SelectedValue)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método utilizado para cargar las etapas en el combobox correspondiente (de la solapa Habilitación o Estado)
    ''' </summary>
    ''' <param name="p_iStepId"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	15/04/2008	Modified
    ''' </history>
    Private Sub loadStages()
        Dim i_dsSteps As DsSteps

        Try
            Dim workId As Int64 = WFBusiness.GetWorkflowIdByStepId(MyRule.WFStepId)

            If workId > 0 Then
                i_dsSteps = WFStepBusiness.GetDsSteps(workId)

                If Not i_dsSteps Is Nothing Then
                    loadStagesSinceTabState(i_dsSteps)
                    checkIfStageExists()
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método utilizado para cargar los estados en el listbox correspondiente (de la solapa Habilitación o Estado)
    ''' </summary>
    ''' <param name="p_iStepId"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Gaston]	15/04/2008	Modified
    ''' </history>
    Private Sub loadStates(ByVal p_iStepId As Int32)
        Dim Wfstep As WFStep

        Try
            Wfstep = WFStepBusiness.GetStepById(p_iStepId)

            If (Not Wfstep Is Nothing) Then
                loadStatesSinceTabState(Wfstep)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

    Private Sub btnValoresPorDef_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEstadoValDef.Click
        Try
            WFBusiness.DeleteRulesPreferencesSinObjectId(MyRule.ID, MyRuleSelectionOption)
            lstStates.SelectedItem = Nothing
            tbctrMain_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class