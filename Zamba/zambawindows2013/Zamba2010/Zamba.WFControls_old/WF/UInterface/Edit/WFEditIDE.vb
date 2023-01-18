Imports Zamba.Core.Enumerators
Imports Zamba.Core
Imports Zamba.WorkFlow.Execution.Control
Imports Zamba.WFShapes.Controls
Imports Telerik.WinControls.UI

Public Class WFEditIDE
    Inherits ZForm

#Region " Código generado por el Diseñador de Windows Forms "

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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
    Friend WithEvents PanelBottom As System.Windows.Forms.Panel
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents PanelCircuit As System.Windows.Forms.Panel
    Friend WithEvents PanelMain As ZPanel
    Friend WithEvents TabPageWF As System.Windows.Forms.TabControl
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lstWfDocType As ListBox
    Friend WithEvents Label1 As ZLabel
    Friend WithEvents tabGeneralRules As System.Windows.Forms.TabPage
    Friend WithEvents TabSteps As System.Windows.Forms.TabPage
    Friend WithEvents BtnDownIndex As ZButton
    Friend WithEvents btnUpIndex As ZButton
    Friend WithEvents Label4 As ZLabel
    Friend WithEvents lstEtapas As ListBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents TabDebuggerControl As System.Windows.Forms.TabControl
    Friend WithEvents TabRule As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents VarName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VarValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VarType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabTask As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents AttributeName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AttributeValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AttributeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabVariables As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView3 As System.Windows.Forms.DataGridView
    Friend WithEvents GlobalVarName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GlobalVarValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GlobalVarType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PanelRight As System.Windows.Forms.Panel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        PanelBottom = New System.Windows.Forms.Panel()
        PanelRight = New System.Windows.Forms.Panel()
        TabPage1 = New System.Windows.Forms.TabPage()
        SplitContainer1 = New System.Windows.Forms.SplitContainer()
        PanelCircuit = New System.Windows.Forms.Panel()
        PanelMain = New ZPanel()
        TabPageWF = New System.Windows.Forms.TabControl()
        TabPage2 = New System.Windows.Forms.TabPage()
        Label1 = New ZLabel()
        lstWfDocType = New ListBox()
        tabGeneralRules = New System.Windows.Forms.TabPage()
        TabSteps = New System.Windows.Forms.TabPage()
        Panel1 = New System.Windows.Forms.Panel()
        SplitContainer2 = New System.Windows.Forms.SplitContainer()
        BtnDownIndex = New ZButton()
        btnUpIndex = New ZButton()
        Label4 = New ZLabel()
        lstEtapas = New ListBox()
        TabDebuggerControl = New System.Windows.Forms.TabControl()
        TabRule = New System.Windows.Forms.TabPage()
        DataGridView1 = New System.Windows.Forms.DataGridView()
        VarName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        VarValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        VarType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        TabTask = New System.Windows.Forms.TabPage()
        DataGridView2 = New System.Windows.Forms.DataGridView()
        AttributeName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        AttributeValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        AttributeType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        TabVariables = New System.Windows.Forms.TabPage()
        DataGridView3 = New System.Windows.Forms.DataGridView()
        GlobalVarName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        GlobalVarValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        GlobalVarType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        TabPage1.SuspendLayout()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        TabPageWF.SuspendLayout()
        TabPage2.SuspendLayout()
        TabSteps.SuspendLayout()
        Panel1.SuspendLayout()
        CType(SplitContainer2, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer2.Panel1.SuspendLayout()
        SplitContainer2.SuspendLayout()
        TabDebuggerControl.SuspendLayout()
        TabRule.SuspendLayout()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        TabTask.SuspendLayout()
        CType(DataGridView2, ComponentModel.ISupportInitialize).BeginInit()
        TabVariables.SuspendLayout()
        CType(DataGridView3, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'PanelBottom
        '
        PanelBottom.AutoScroll = True
        PanelBottom.BackColor = System.Drawing.Color.Wheat
        PanelBottom.Location = New System.Drawing.Point(157, 560)
        PanelBottom.Name = "PanelBottom"
        PanelBottom.Size = New System.Drawing.Size(665, 3)
        PanelBottom.TabIndex = 41
        '
        'PanelRight
        '
        PanelRight.AutoScroll = True
        PanelRight.BackColor = System.Drawing.Color.Wheat
        PanelRight.Location = New System.Drawing.Point(822, 101)
        PanelRight.Name = "PanelRight"
        PanelRight.Size = New System.Drawing.Size(3, 462)
        PanelRight.TabIndex = 42
        '
        'TabPage1
        '
        TabPage1.Controls.Add(SplitContainer1)
        TabPage1.Location = New System.Drawing.Point(4, 22)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New System.Windows.Forms.Padding(3)
        TabPage1.Size = New System.Drawing.Size(733, 471)
        TabPage1.TabIndex = 0
        TabPage1.Text = "WorkFlow"
        TabPage1.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        SplitContainer1.Location = New System.Drawing.Point(3, 3)
        SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        SplitContainer1.Panel1.Controls.Add(PanelCircuit)
        '
        'SplitContainer1.Panel2
        '
        SplitContainer1.Panel2.Controls.Add(PanelMain)
        SplitContainer1.Size = New System.Drawing.Size(727, 465)
        SplitContainer1.SplitterDistance = 241
        SplitContainer1.TabIndex = 49
        '
        'PanelCircuit
        '
        PanelCircuit.AutoScroll = True
        PanelCircuit.BackColor = System.Drawing.Color.White
        PanelCircuit.Dock = System.Windows.Forms.DockStyle.Fill
        PanelCircuit.Location = New System.Drawing.Point(0, 0)
        PanelCircuit.Name = "PanelCircuit"
        PanelCircuit.Size = New System.Drawing.Size(241, 465)
        PanelCircuit.TabIndex = 47
        '
        'PanelMain
        '
        PanelMain.AutoScroll = True
        PanelMain.BackColor = System.Drawing.Color.White
        PanelMain.Dock = System.Windows.Forms.DockStyle.Fill
        PanelMain.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        PanelMain.Location = New System.Drawing.Point(0, 0)
        PanelMain.Name = "PanelMain"
        PanelMain.Size = New System.Drawing.Size(482, 465)
        PanelMain.TabIndex = 48
        '
        'TabPageWF
        '
        TabPageWF.Controls.Add(TabPage1)
        TabPageWF.Controls.Add(TabPage2)
        TabPageWF.Controls.Add(tabGeneralRules)
        TabPageWF.Controls.Add(TabSteps)
        TabPageWF.Dock = System.Windows.Forms.DockStyle.Fill
        TabPageWF.Location = New System.Drawing.Point(2, 2)
        TabPageWF.Name = "TabPageWF"
        TabPageWF.SelectedIndex = 0
        TabPageWF.Size = New System.Drawing.Size(741, 497)
        TabPageWF.TabIndex = 47
        '
        'TabPage2
        '
        TabPage2.Controls.Add(Label1)
        TabPage2.Controls.Add(lstWfDocType)
        TabPage2.Location = New System.Drawing.Point(4, 22)
        TabPage2.Name = "TabPage2"
        TabPage2.Size = New System.Drawing.Size(329, 232)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Entidades"
        TabPage2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Font = New Font("Tahoma", 18.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label1.ForeColor = System.Drawing.Color.Blue
        Label1.Location = New System.Drawing.Point(95, 35)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(411, 29)
        Label1.TabIndex = 1
        Label1.Text = "Entidades Asociadas al WorkFlow"
        '
        'lstWfDocType
        '
        lstWfDocType.FormattingEnabled = True
        lstWfDocType.HorizontalScrollbar = True
        lstWfDocType.Location = New System.Drawing.Point(68, 106)
        lstWfDocType.Name = "lstWfDocType"
        lstWfDocType.Size = New System.Drawing.Size(510, 238)
        lstWfDocType.Sorted = True
        lstWfDocType.TabIndex = 0
        '
        'tabGeneralRules
        '
        tabGeneralRules.Location = New System.Drawing.Point(4, 22)
        tabGeneralRules.Name = "tabGeneralRules"
        tabGeneralRules.Padding = New System.Windows.Forms.Padding(3)
        tabGeneralRules.Size = New System.Drawing.Size(329, 232)
        tabGeneralRules.TabIndex = 2
        tabGeneralRules.Text = "Reglas Generales"
        tabGeneralRules.UseVisualStyleBackColor = True
        '
        'TabSteps
        '
        TabSteps.Controls.Add(Panel1)
        TabSteps.Location = New System.Drawing.Point(4, 22)
        TabSteps.Name = "TabSteps"
        TabSteps.Size = New System.Drawing.Size(329, 232)
        TabSteps.TabIndex = 3
        TabSteps.Text = "Orden Etapas"
        TabSteps.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Panel1.Controls.Add(SplitContainer2)
        Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Panel1.Location = New System.Drawing.Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New System.Drawing.Size(329, 232)
        Panel1.TabIndex = 110
        '
        'SplitContainer2
        '
        SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        SplitContainer2.Location = New System.Drawing.Point(0, 0)
        SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        SplitContainer2.Panel1.Controls.Add(BtnDownIndex)
        SplitContainer2.Panel1.Controls.Add(btnUpIndex)
        SplitContainer2.Panel1.Controls.Add(Label4)
        SplitContainer2.Panel1.Controls.Add(lstEtapas)
        SplitContainer2.Size = New System.Drawing.Size(329, 232)
        SplitContainer2.SplitterDistance = 145
        SplitContainer2.TabIndex = 110
        '
        'BtnDownIndex
        '
        BtnDownIndex.BackColor = System.Drawing.Color.White
        BtnDownIndex.Image = Global.Zamba.Controls.My.Resources.Resources.down
        BtnDownIndex.Location = New System.Drawing.Point(266, 72)
        BtnDownIndex.Name = "BtnDownIndex"
        BtnDownIndex.Size = New System.Drawing.Size(20, 24)
        BtnDownIndex.TabIndex = 107
        BtnDownIndex.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        BtnDownIndex.UseVisualStyleBackColor = False
        '
        'btnUpIndex
        '
        btnUpIndex.BackColor = System.Drawing.Color.White
        btnUpIndex.Image = Global.Zamba.Controls.My.Resources.Resources.up
        btnUpIndex.Location = New System.Drawing.Point(266, 45)
        btnUpIndex.Name = "btnUpIndex"
        btnUpIndex.Size = New System.Drawing.Size(20, 21)
        btnUpIndex.TabIndex = 106
        btnUpIndex.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        btnUpIndex.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Label4.BackColor = System.Drawing.Color.Transparent
        Label4.Font = New Font("Verdana", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        Label4.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        Label4.Location = New System.Drawing.Point(3, 16)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(195, 24)
        Label4.TabIndex = 105
        Label4.Text = "Etapas"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        '
        'lstEtapas
        '
        lstEtapas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        lstEtapas.DisplayMember = "NAME"
        lstEtapas.HorizontalScrollbar = True
        lstEtapas.Location = New System.Drawing.Point(6, 43)
        lstEtapas.Name = "lstEtapas"
        lstEtapas.Size = New System.Drawing.Size(254, 340)
        lstEtapas.TabIndex = 104
        '
        'TabDebuggerControl
        '
        TabDebuggerControl.Controls.Add(TabRule)
        TabDebuggerControl.Controls.Add(TabTask)
        TabDebuggerControl.Controls.Add(TabVariables)
        TabDebuggerControl.Dock = System.Windows.Forms.DockStyle.Fill
        TabDebuggerControl.Location = New System.Drawing.Point(2, 333)
        TabDebuggerControl.Name = "TabDebuggerControl"
        TabDebuggerControl.SelectedIndex = 0
        TabDebuggerControl.Size = New System.Drawing.Size(668, 131)
        TabDebuggerControl.TabIndex = 48
        '
        'TabRule
        '
        TabRule.Controls.Add(DataGridView1)
        TabRule.Location = New System.Drawing.Point(4, 22)
        TabRule.Name = "TabRule"
        TabRule.Padding = New System.Windows.Forms.Padding(3)
        TabRule.Size = New System.Drawing.Size(660, 105)
        TabRule.TabIndex = 0
        TabRule.Text = "Regla"
        TabRule.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        DataGridView1.AllowUserToOrderColumns = True
        DataGridView1.BackgroundColor = System.Drawing.Color.White
        DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {VarName, VarValue, VarType})
        DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        DataGridView1.GridColor = System.Drawing.Color.White
        DataGridView1.Location = New System.Drawing.Point(3, 3)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New System.Drawing.Size(654, 99)
        DataGridView1.TabIndex = 0
        '
        'VarName
        '
        VarName.HeaderText = "Parametro"
        VarName.Name = "VarName"
        VarName.ReadOnly = True
        '
        'VarValue
        '
        VarValue.HeaderText = "Valor"
        VarValue.Name = "VarValue"
        VarValue.ReadOnly = True
        '
        'VarType
        '
        VarType.HeaderText = "Tipo"
        VarType.Name = "VarType"
        VarType.ReadOnly = True
        '
        'TabTask
        '
        TabTask.Controls.Add(DataGridView2)
        TabTask.Location = New System.Drawing.Point(4, 22)
        TabTask.Name = "TabTask"
        TabTask.Padding = New System.Windows.Forms.Padding(3)
        TabTask.Size = New System.Drawing.Size(660, 105)
        TabTask.TabIndex = 1
        TabTask.Text = "Tarea"
        TabTask.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        DataGridView2.AllowUserToOrderColumns = True
        DataGridView2.BackgroundColor = System.Drawing.Color.White
        DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {AttributeName, AttributeValue, AttributeType})
        DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        DataGridView2.GridColor = System.Drawing.Color.White
        DataGridView2.Location = New System.Drawing.Point(3, 3)
        DataGridView2.Name = "DataGridView2"
        DataGridView2.Size = New System.Drawing.Size(654, 99)
        DataGridView2.TabIndex = 0
        '
        'AttributeName
        '
        AttributeName.HeaderText = "Atributo"
        AttributeName.Name = "AttributeName"
        AttributeName.ReadOnly = True
        '
        'AttributeValue
        '
        AttributeValue.HeaderText = "Valor"
        AttributeValue.Name = "AttributeValue"
        AttributeValue.ReadOnly = True
        '
        'AttributeType
        '
        AttributeType.HeaderText = "Tipo"
        AttributeType.Name = "AttributeType"
        AttributeType.ReadOnly = True
        '
        'TabVariables
        '
        TabVariables.Controls.Add(DataGridView3)
        TabVariables.Location = New System.Drawing.Point(4, 22)
        TabVariables.Name = "TabVariables"
        TabVariables.Size = New System.Drawing.Size(660, 105)
        TabVariables.TabIndex = 2
        TabVariables.Text = "Variables"
        TabVariables.UseVisualStyleBackColor = True
        '
        'DataGridView3
        '
        DataGridView3.AllowUserToOrderColumns = True
        DataGridView3.BackgroundColor = System.Drawing.Color.White
        DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView3.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {GlobalVarName, GlobalVarValue, GlobalVarType})
        DataGridView3.Dock = System.Windows.Forms.DockStyle.Fill
        DataGridView3.GridColor = System.Drawing.Color.White
        DataGridView3.Location = New System.Drawing.Point(0, 0)
        DataGridView3.Name = "DataGridView3"
        DataGridView3.Size = New System.Drawing.Size(660, 105)
        DataGridView3.TabIndex = 0
        '
        'GlobalVarName
        '
        GlobalVarName.HeaderText = "Variable"
        GlobalVarName.Name = "GlobalVarName"
        GlobalVarName.ReadOnly = True
        '
        'GlobalVarValue
        '
        GlobalVarValue.HeaderText = "Valor"
        GlobalVarValue.Name = "GlobalVarValue"
        GlobalVarValue.ReadOnly = True
        '
        'GlobalVarType
        '
        GlobalVarType.HeaderText = "Tipo"
        GlobalVarType.Name = "GlobalVarType"
        GlobalVarType.ReadOnly = True
        '
        'WFEditIDE
        '
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        ClientSize = New System.Drawing.Size(745, 501)
        Controls.Add(TabPageWF)
        Controls.Add(PanelBottom)
        Controls.Add(PanelRight)
        Name = "WFEditIDE"
        WindowState = System.Windows.Forms.FormWindowState.Maximized
        TabPage1.ResumeLayout(False)
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        TabPageWF.ResumeLayout(False)
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        TabSteps.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        SplitContainer2.Panel1.ResumeLayout(False)
        CType(SplitContainer2, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer2.ResumeLayout(False)
        TabDebuggerControl.ResumeLayout(False)
        TabRule.ResumeLayout(False)
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        TabTask.ResumeLayout(False)
        CType(DataGridView2, ComponentModel.ISupportInitialize).EndInit()
        TabVariables.ResumeLayout(False)
        CType(DataGridView3, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

#End Region

    Public Property DShowSearchForm() As [Delegate]
        Get
            If WFPanelCircuit IsNot Nothing Then
                Return WFPanelCircuit.DShowSearchForm
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As [Delegate])
            If WFPanelCircuit IsNot Nothing Then
                WFPanelCircuit.DShowSearchForm = value
            End If
        End Set
    End Property

    Public Property WfPanel() As WFPanelCircuit
        Get
            Return WFPanelCircuit
        End Get
        Set(ByVal value As WFPanelCircuit)
            WFPanelCircuit = value
        End Set
    End Property


    Dim WF As Core.WorkFlow
    Dim WithEvents WFPanelCircuit As WFPanelCircuit
    Dim WithEvents WFShapeCircuit As Zamba.WFShapes.Controls.MainForm
    Dim WithEvents UcStepCtrl As UCStepCtrl
    Dim WithEvents UCRule As IZRuleControl
    Dim UCRuleContainer As UCRuleContainer
    Dim WFRights As WFRights
    Dim IL As Zamba.AppBlock.ZIconsList
    Dim wfTabControl As TabControl
    Dim dgvreglasInvalidas As DataGridView
    Dim dttable As DataSet
    Dim dttable2 As DataSet

    Public ReadOnly Property WFId() As Integer
        Get
            Return WF.ID
        End Get
    End Property

    Private UCButtonsABM As UCButtonsABM = Nothing
    Public Event LoadWF(ByVal workflowId As Int64, ByVal RightType As RightsType, ByVal ruleId As Int64)
    Dim RightType As RightsType
    Public Sub New(ByVal WF As Core.WorkFlow, ByVal RightType As RightsType, Optional ByVal ruleId As Int64 = 0)
        MyBase.New()
        InitializeComponent()
        Me.RightType = RightType
        CommonNew(WF, ruleId)
    End Sub

    'Public Sub New(ByVal WFId As Integer, ByVal ruleId As Integer)
    '    MyBase.New()
    '    InitializeComponent()

    '    Dim dsWf As Core.DsWF.WFRow = Zamba.Data.WFFactory.GetWfById(WFId).WF(0)
    '    Dim WF As Core.WorkFlow = Zamba.Data.WFFactory.GetWf(dsWf)

    '    CommonNew(WF, ruleId)
    'End Sub

    Private Sub CommonNew(ByVal wf As Core.WorkFlow, ByVal ruleId As Int64)
        IL = New Zamba.AppBlock.ZIconsList
        Me.WF = wf
        WFBusiness.GetFullEditWF(Me.WF)
        LoadEditIDE(ruleId)
        LoadStepOrder()
        Text = Me.WF.Name
    End Sub

#Region "Load"
    Private Sub LoadEditIDE(Optional ByVal RuleId As Int64 = 0)
        Try
            WFPanelCircuit = New WFPanelCircuit(WF, IL, RightType)
            WFPanelCircuit.Dock = DockStyle.Fill
            WindowState = FormWindowState.Maximized
            PanelCircuit.Controls.Add(WFPanelCircuit)
            AddWorkflowTreeHandlers()
            EditWF(WF)

            'Expande el nodo del wf
            WFPanelCircuit.tvwRules.Nodes(0).Expand()
            If Not RuleId.Equals(0) Then
                WFPanelCircuit.OpenRule(RuleId)
            End If

            'listado de documentos asociados a un workflow
            lstWfDocType.DataSource = Zamba.Core.DocTypesBusiness.GetAllWFDocTypesByWFIdOnlyForAdmin(WF.ID)
            lstWfDocType.DisplayMember = "TABLE.DOC_TYPE_NAME"

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub AddWorkflowTreeHandlers()
        RemoveHandler WFPanelCircuit.WFSelected, AddressOf EditWF
        RemoveHandler WFPanelCircuit.StepSelected, AddressOf EditStep
        RemoveHandler WFPanelCircuit.RuleSelected, AddressOf EditRule
        RemoveHandler WFPanelCircuit.LoadWokflow, AddressOf OpenMissedRule
        RemoveHandler WFPanelCircuit.TypesofRuleselected, AddressOf TypesofRuleselected
        RemoveHandler WFPanelCircuit.RightSelected, AddressOf EditRights
        RemoveHandler WFPanelCircuit.updateruleicon, AddressOf UpdateRuleIcon
        AddHandler WFPanelCircuit.WFSelected, AddressOf EditWF
        AddHandler WFPanelCircuit.StepSelected, AddressOf EditStep
        AddHandler WFPanelCircuit.RuleSelected, AddressOf EditRule
        AddHandler WFPanelCircuit.LoadWokflow, AddressOf OpenMissedRule
        AddHandler WFPanelCircuit.TypesofRuleselected, AddressOf TypesofRuleselected
        AddHandler WFPanelCircuit.RightSelected, AddressOf EditRights
        AddHandler WFPanelCircuit.updateruleicon, AddressOf UpdateRuleIcon
    End Sub

    Private Sub WFEditIDE_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Dispose()
    End Sub

    Private Sub WFEditIDE_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        'Ajusto PanelCircuit a 40%
        PanelCircuit.Width = CInt(((40 * Width) / 100))

        dttable = New DataSet
        dttable2 = New DataSet
        dttable.Tables.Add("table1")

        dttable.Tables("table1").Columns.Add("Id Regla")
        dttable.Tables("table1").Columns.Add("Nombre Regla")
        dttable.Tables("table1").Columns.Add("Estado")

        LoadReglasInvalidas()

    End Sub
#End Region

#Region "Rules"
    ''' <summary>
    ''' Carga la edicion de WF
    ''' </summary>
    ''' <history>
    ''' [Sebastian] 21-10-2009 Modified en caso de ser una do design se le pasa 
    '''                                 la interfaz para luego recargar el nodo con la regla convertida
    ''' [Sebastian] 28-10-2009 MODIFIED Se aplico inyeccion de codigo pero para las reglas if en if do design
    ''' </history>
    ''' <param name="rule"></param>
    ''' <remarks></remarks>
    Private Sub EditRule(ByRef ruleid As Int64)
        Try


            Dim Rule As IWFRuleParent = Zamba.Core.WFRulesBusiness.GetInstanceRuleById(ruleid, False)

            If Not TabRuleContainer Is Nothing AndAlso TabRuleContainer.IsDisposed <> True Then
                If TabRuleContainer.Controls.Count > 0 Then
                    If TypeOf TabRuleContainer.Controls(0) Is UCRuleContainer Then
                        Dim UCRule As IZRuleControl = DirectCast(TabRuleContainer.Controls(0), UCRuleContainer).PanelRule.Controls(0)
                        If UCRule.HasBeenModified Then
                            If MessageBox.Show("Esta seguro que quiere salir?", "Edicion de Reglas", MessageBoxButtons.YesNo) <> DialogResult.Yes Then
                                Exit Sub
                            End If
                        End If
                    End If
                End If
                TabRuleContainer.Controls.Clear()
                Dim tt As System.Reflection.Assembly = System.Reflection.Assembly.LoadFile(System.Windows.Forms.Application.StartupPath & "\Zamba.WFUserControl.dll")
                Dim t As System.Type = tt.GetType("Zamba.WFUserControl.UC" & Rule.GetType.Name, True, True)
                Dim args As Object()

                If Not Rule Is Nothing AndAlso Not WFPanelCircuit Is Nothing AndAlso WFPanelCircuit.IsDisposed <> True Then

                    '[kom] Se modifico el constructor de todas las reglas.
                    args = New Object() {Rule, WFPanelCircuit}

                    If Not t Is Nothing Then
                        UCRule = DirectCast(Activator.CreateInstance(t, args), IZRuleControl)

                        If Not UCRule Is Nothing Then

                            RemoveHandler UCRule.UpdateMaskName, AddressOf UpdateMaskName
                            AddHandler UCRule.UpdateMaskName, AddressOf UpdateMaskName

                            RemoveHandler WFPanelCircuit.updateruleicon, AddressOf UpdateRuleIcon
                            AddHandler WFPanelCircuit.updateruleicon, AddressOf UpdateRuleIcon

                            RemoveHandler UCRule.UpdateRuleIcon, AddressOf UpdateRuleIcon
                            AddHandler UCRule.UpdateRuleIcon, AddressOf UpdateRuleIcon

                            UCRuleContainer = New UCRuleContainer(Rule)
                            UCRuleContainer.Dock = DockStyle.Fill
                            UCRuleContainer.AutoScroll = True
                            UCRuleContainer.PanelRule.AutoScroll = True
                            UCRuleContainer.PanelTop.Text = " Id: " & Rule.ID & " - " & Rule.Name & " - (" & Rule.RuleClass.ToString & ")"
                            TabRuleContainer.AutoScroll = True
                            TabRuleContainer.Controls.Add(UCRuleContainer)
                            UCRuleContainer.PanelRule.Controls.Add(DirectCast(UCRule, Control))
                            UCRuleContainer.PanelRule.Controls(0).Dock = DockStyle.Fill
                            UCRuleContainer.PanelRule.AutoScroll = True
                            wfTabControl.SelectedTab = TabRuleContainer
                            If RightType = RightsType.View Then
                                UCRuleContainer.Enabled = False
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As ComponentModel.Win32Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub UpdateRuleIcon(ByVal rule As IRule)
        If rule.Enable Then
            If rule.RefreshRule Then
                WFPanelCircuit.tvwRules.SelectedNode.ImageIndex = Icons.Refresh
                ' WFPanelCircuit.tvwRules.SelectedNode.SelectedImageIndex = Icons.Refresh
            ElseIf rule.RuleClass.Contains("Design") Then
                WFPanelCircuit.tvwRules.SelectedNode.ImageIndex = Icons.Design
                ' WFPanelCircuit.tvwRules.SelectedNode.SelectedImageIndex = Icons.Design
            Else
                WFPanelCircuit.tvwRules.SelectedNode.ImageIndex = Icons.YellowBall
                ' WFPanelCircuit.tvwRules.SelectedNode.SelectedImageIndex = Icons.YellowBall
            End If
        Else
            WFPanelCircuit.tvwRules.SelectedNode.ImageIndex = Icons.Disable
            ' WFPanelCircuit.tvwRules.SelectedNode.SelectedImageIndex = Icons.Disable
        End If

        If rule.DisableChildRules Then
            SetNodeIcon(WFPanelCircuit.tvwRules.SelectedNode.Nodes, rule.DisableChildRules)
        Else
            SetNodeIcon(WFPanelCircuit.tvwRules.SelectedNode.Nodes, rule.DisableChildRules)
        End If
    End Sub
    Private Sub SetNodeIcon(ByVal Nodes As RadTreeNodeCollection, ByVal DisableChildRules As Boolean)
        If Not Nodes Is Nothing Then
            For Each node As RadTreeNode In Nodes
                If DisableChildRules Then
                    node.ImageIndex = Icons.Disable
                    'node.SelectedImageIndex = Icons.Disable
                Else
                    node.ImageIndex = Icons.YellowBall
                    'node.SelectedImageIndex = Icons.YellowBall
                End If

                If Not node.Nodes Is Nothing Then
                    SetNodeIcon(node.Nodes, DisableChildRules)
                End If
            Next
        End If
    End Sub

    Private Sub UpdateMaskName(ByRef rule As IRule) 'IWFRuleParent)
        Try
            Dim NewMask As String = Trim(rule.Name)

            'Si es la primera vez que la asigno la agrego al WFStep
            If WFPanelCircuit.tvwRules.SelectedNode.Text = "Distribuyo" Then
                'Todo Esto no tiene sentido ver si realmente se usa
                'rule.WFStep.Rules.Add(rule)
            End If
            WFPanelCircuit.tvwRules.SelectedNode.Text = NewMask

            'Si es hija de una Accion de Usuario le cambio el nombre
            'Dim WFRule As WFRuleParent
            'WFRule = DirectCast(Me.WFPanelCircuit.TreeView1.SelectedNode, RuleNode).RuleId
            If DirectCast(Me.WFPanelCircuit.tvwRules.SelectedNode, RuleNode).ParentType = TypesofRules.AccionUsuario Then
                Dim userActionName As String = String.Empty

                'Busca en la tabla si existe un nombre de acción de usuario para esa regla
                Try
                    userActionName = WFBusiness.GetUserActionName(DirectCast(WFPanelCircuit.tvwRules.SelectedNode, RuleNode).RuleId, DirectCast(WFPanelCircuit.tvwRules.SelectedNode, RuleNode).WFStepId, DirectCast(WFPanelCircuit.tvwRules.SelectedNode, RuleNode).RuleName, False)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    userActionName = String.Empty
                End Try

                'Si el nombre no existe entonces le asigna el nombre de la regla
                If String.IsNullOrEmpty(userActionName) Then
                    DirectCast(WFPanelCircuit.tvwRules.SelectedNode, RuleNode).PrevVisibleNode.Text = DirectCast(WFPanelCircuit.tvwRules.SelectedNode, RuleNode).RuleName
                Else
                    DirectCast(WFPanelCircuit.tvwRules.SelectedNode, RuleNode).PrevVisibleNode.Text = userActionName
                End If
            End If

            UCRuleContainer.PanelTop.Text = " " & NewMask
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub TypesofRuleselected(ByVal stepid As Int64, ByVal RuleParentType As TypesofRules, ByVal ruleIds As Generic.List(Of Int64))
        Try
            PanelMain.SuspendLayout()
            TabRuleContainer.Controls.Clear()
            Dim ucRuleType As New UCRuleType(stepid, RuleParentType, ruleIds)
            ucRuleType.Dock = DockStyle.Fill
            TabRuleContainer.Controls.Add(ucRuleType)
            wfTabControl.SelectTab(TabRuleContainer)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        PanelMain.ResumeLayout()
    End Sub

    Private Sub OpenMissedRule(ByVal workflowId As Int64, ByVal ruleId As Int64)
        Try
            RaiseEvent LoadWF(workflowId, RightType, ruleId)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        PanelMain.ResumeLayout()
    End Sub

#End Region

#Region "Steps"
    Private Sub EditStep(ByRef wfstep As WFStep)
        Try
            TabRuleContainer.Controls.Clear()
            UcStepCtrl = New UCStepCtrl(wfstep, True)
            UcStepCtrl.Dock = DockStyle.Fill
            TabRuleContainer.Controls.Add(UcStepCtrl)
            RemoveHandler UcStepCtrl.NameChanged, AddressOf NameChanged
            AddHandler UcStepCtrl.NameChanged, AddressOf NameChanged
            wfTabControl.SelectTab(TabRuleContainer)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub NameChanged(ByVal Name As String, ByRef wfstep As WFStep)
        Try
            WFPanelCircuit.tvwRules.SelectedNode.Text = Name
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Rights"
    Private Sub EditRights(ByRef wfstep As WFStep)
        If Not IsDisposed Then
            Try
                TabRuleContainer.Controls.Clear()
                WFRights = New WFRights
                WFRights.Dock = DockStyle.Fill
                TabRuleContainer.Controls.Add(WFRights)
                WFRights.WFStep = wfstep
                wfTabControl.SelectTab(TabRuleContainer)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If
    End Sub
#End Region

#Region "WF"

    Dim TabRuleContainer As New ZTabPage

    Private Sub LoadTabs(ByVal WF As Core.WorkFlow)
        PanelMain.Controls.Clear()
        wfTabControl = New TabControl
        wfTabControl.Dock = DockStyle.Fill

        wfTabControl.TabPages.Add("Diagrama", "Diagrama")


        TabRuleContainer.Text = "Edicion"


        wfTabControl.TabPages.Add(TabRuleContainer)

        wfTabControl.TabPages.Add("Simulaciones", "Simulaciones")

        wfTabControl.TabPages.Add("Reglas Invalidas", "Reglas Invalidas")

        wfTabControl.TabPages.Add("Depuracion", "Depuracion")

        PanelMain.Controls.Add(wfTabControl)

        'El contenido de esta pestaña se carga a pedido
        RemoveHandler wfTabControl.SelectedIndexChanged, AddressOf LoadSimulationTab
        AddHandler wfTabControl.SelectedIndexChanged, AddressOf LoadSimulationTab


        If RightType <> RightsType.View Then
            WFShapeCircuit = New Zamba.WFShapes.Controls.MainForm(WF, True)
            AddDiagramHandlers()
        Else
            WFShapeCircuit = New Zamba.WFShapes.Controls.MainForm(WF, False)
        End If
        WFShapeCircuit.Dock = DockStyle.Fill


        wfTabControl.TabPages("Diagrama").Controls.Add(WFShapeCircuit)
        wfTabControl.TabPages("Depuracion").Controls.Add(TabDebuggerControl)

    End Sub
    Dim Loading As Boolean = True

    Public Sub EditWF(ByVal WF As Core.WorkFlow)
        Try
            If Loading Then
                Loading = False
                LoadTabs(WF)
            End If
            wfTabControl.SelectTab("Diagrama")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub LoadSimulationTab(sender As Object, e As EventArgs)
        'If wfTabControl IsNot Nothing AndAlso wfTabControl.TabPages("Simulaciones") IsNot Nothing AndAlso wfTabControl.SelectedTab Is wfTabControl.TabPages("Simulaciones") AndAlso wfTabControl.TabPages("Simulaciones").Controls.Count = 0 Then
        '    RemoveHandler wfTabControl.SelectedIndexChanged, AddressOf LoadSimulationTab

        '    Dim ucSimMgr As New UcSimulationManager()
        '    ucSimMgr.Dock = DockStyle.Fill
        '    wfTabControl.TabPages("Simulaciones").Controls.Add(ucSimMgr)
        'End If
    End Sub

    Private Sub AddDiagramHandlers()
        RemoveHandler WFShapeCircuit.OnAddStep, AddressOf AddedStep
        RemoveHandler WFShapeCircuit.OnRemoveStep, AddressOf RemoveStep
        RemoveHandler WFShapeCircuit.OnNameStep, AddressOf NameStep
        RemoveHandler WFShapeCircuit.OnAddRule, AddressOf AddedRule
        RemoveHandler WFShapeCircuit.OnRemoveRule, AddressOf RemoveRule
        RemoveHandler WFShapeCircuit.OnNameRule, AddressOf NameRule
        RemoveHandler WFShapeCircuit.OnDesignStep, AddressOf DesignStep
        AddHandler WFShapeCircuit.OnAddStep, AddressOf AddedStep
        AddHandler WFShapeCircuit.OnRemoveStep, AddressOf RemoveStep
        AddHandler WFShapeCircuit.OnNameStep, AddressOf NameStep
        AddHandler WFShapeCircuit.OnAddRule, AddressOf AddedRule
        AddHandler WFShapeCircuit.OnRemoveRule, AddressOf RemoveRule
        AddHandler WFShapeCircuit.OnNameRule, AddressOf NameRule
        AddHandler WFShapeCircuit.OnDesignStep, AddressOf DesignStep
    End Sub


    Private Sub AddedStep(ByVal NewStep As IWFStep)
        Try
            WFPanelCircuit.AddNewStep(DirectCast(NewStep, WFStep))
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub RemoveStep(ByVal DelStep As IWFStep)
        Try
            WFPanelCircuit.RemoveStep(DirectCast(DelStep, WFStep))
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub RemoveRule(ByVal DelRule As IWFRuleParent, ByVal delStep As IWFStep)
        Try
            'Me.WFPanelCircuit.RemoveRule(DelRule)
            PanelCircuit.Controls.Clear()
            LoadEditIDE()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub NameRule(ByVal IdRule As Int64, ByVal name As String)
        Try
            'Dim i As Int32
            For Each s As WFStep In WF.Steps.Values
                For Each r As DsRules.WFRulesRow In s.DsRules.WFRules.Rows
                    If r.Id = IdRule Then
                        r.Name = name
                        Exit For
                    End If
                Next
            Next
            PanelCircuit.Controls.Clear()
            LoadEditIDE()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub NameStep(ByVal NameStep As IWFStep)
        Try
            WFPanelCircuit.NameStep(DirectCast(NameStep, WFStep))
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Crea el xoml a partir del step y lo agrega en un tab
    ''' </summary>
    ''' <param name="ShapeStepID"></param>
    ''' <param name="ShapeStepName"></param>
    ''' <history>Marcelo Modified 28/05/08</history>
    ''' <remarks></remarks>
    Private Sub DesignStep(ByVal ShapeStepID As Int64, ByVal ShapeStepName As String)
        Try
            'Si ya existe lo selecciono
            For Each pag As TabPage In TabPageWF.TabPages
                If String.Compare(ShapeStepName, pag.Text.ToString()) = 0 Then
                    pag.Select()
                    Exit Sub
                End If
            Next

            'Creo el xoml
            Dim xoml As WorkflowDesignerControl = New WorkflowDesignerControl(ShapeStepID, True, True)

            AddHandler xoml.OnCloseWF, AddressOf CloseWF
            AddHandler xoml.OnOpenRegionWF, AddressOf DesignRegionWF

            'Creo el tab y lo agrego
            Dim page As New ZTabPage
            page.Name = "Xoml"
            page.Text = "Etapa " & ShapeStepName
            page.Tag = ShapeStepName
            page.Controls.Add(xoml)
            xoml.Dock = DockStyle.Fill

            TabPageWF.TabPages.Add(page)
            TabPageWF.SelectTab(page)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub DesignRegionWF(ByVal StepId As Int64, ByVal ParentType As Int64)
        Try
            WFBusiness.GetFullEditWF(WF)

            'Creo el xoml
            ' Dim xoml As WorkflowDesignerControl = New WorkflowDesignerControl(StepId, ParentId, True)
            Dim WFStep As IWFStep = WFStepBusiness.GetStepById(StepId, True)
            Dim RuleDesigner As New UCRulesDesigner(WFStep, True, ParentType)
            '  AddHandler xoml.OnCloseWF, AddressOf CloseWF
            ' AddHandler xoml.OnOpenRegionWF, AddressOf DesignRegionWF

            RemoveHandler RuleDesigner.OnCloseControl, AddressOf CloseWF
            AddHandler RuleDesigner.OnCloseControl, AddressOf CloseWF
            'AddHandler RuleDesigner.OnOpenRegionWF, AddressOf DesignRegionWF

            'Creo el tab y lo agrego
            Dim page As New ZTabPage
            page.Name = "RuleDesigner"

            page.Text = WFStepBusiness.GetStepNameById(StepId) & " - " & System.Enum.GetName(GetType(TypesofRules), ParentType)
            page.Tag = StepId & " - " & ParentType
            'page.Controls.Add(xoml)
            'xoml.Dock = DockStyle.Fill
            page.Controls.Add(RuleDesigner)
            RuleDesigner.Dock = DockStyle.Fill

            TabPageWF.TabPages.Add(page)
            TabPageWF.SelectTab(page)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    'Cierra la page seleccionada
    Public Sub CloseWF(ByVal page As TabPage)
        Try
            TabPageWF.TabPages.Remove(page)
            page.Dispose()
            WF = WFBusiness.GetWFbyId(WFId, True)
            CommonNew(WF, 0)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub


    Private Sub AddedRule(ByVal rulestep As IWFStep, ByVal IDRule As Int64)
        Try
            Dim wf As IWorkFlow
            wf = WFBusiness.GetWFbyId(rulestep.WorkId)
            wf.Steps.Remove(rulestep.ID)
            wf.Steps.Add(rulestep.ID, rulestep)

            WFPanelCircuit.AddNewRule(DirectCast(rulestep, WFStep), IDRule)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

    ''' <summary>
    ''' Método que retorna true si el workflow que se selecciono en la ventana de WorkFlows es el mismo que está en el explorador que muestra al 
    ''' workflow (nombre y id) y sus etapas (y reglas si es que ahi).  
    ''' </summary>
    ''' <param name="workflowNameDeleted"></param>
    ''' <param name="workflowId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	04/06/2008	Created
    ''' </history> 
    Public Function deleteExplorerIfWorkflowNameExists(ByVal workflowId As Int64, ByVal workflowNameDeleted As String) As Boolean

        If (WFPanelCircuit IsNot Nothing) Then

            ' Si el nombre y id del workflow que aparece en el explorador es igual al nombre del workflow que se selecciono en la ventana que muestra una 
            ' lista con los WorkFlows (botón WORKFLOW de la barra de herramientas del Administrador)
            If ((DirectCast(WFPanelCircuit.tvwRules.Nodes(0), WFNode).WorkFlow.ID = workflowId) AndAlso (WFPanelCircuit.tvwRules.Nodes(0).Text = workflowNameDeleted)) Then
                ' Se retorna True para que se elimine la clase WFEditIDE (la clase que contiene al explorador)
                Return (True)
            Else
                Return (False)
            End If

        End If

    End Function

    ''' <summary>
    ''' Método que cambia el nombre del workflow del explorador (que muestra al workflow y sus etapas (y reglas si es que ahi)) si es que el nombre
    ''' actual del workflow y el id es el mismo que se selecciono en la ventana de WorkFlows (y para el que se decidio cambiar el nombre) 
    ''' </summary>
    ''' <param name="workflowActualName"></param>
    ''' <param name="workflowNewName"></param>
    ''' <param name="workflowId"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	04/06/2008	Created
    ''' </history>
    Public Sub changeWorkFlowNameInExplorer(ByVal workflowId As Int64, ByVal workflowActualName As String, ByVal workflowNewName As String)
        If (WFPanelCircuit IsNot Nothing) Then

            ' Si el nombre de workflow que aparece en el explorador es igual al nombre del workflow que se selecciono en la ventana que muestra una 
            ' lista con los WorkFlows (botón WORKFLOW de la barra de herramientas del Administrador)
            If ((DirectCast(WFPanelCircuit.tvwRules.Nodes(0), WFNode).WorkFlow.ID = workflowId) AndAlso (WFPanelCircuit.tvwRules.Nodes(0).Text = workflowActualName)) Then
                ' Se cambia el nombre del workflow que aparece en el explorador
                WFPanelCircuit.tvwRules.Nodes(0).Text = workflowNewName
            End If

        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub btnUpIndex_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnUpIndex.Click
        Dim Index As Integer = lstEtapas.SelectedIndex
        Dim Swap As WFStep = lstEtapas.SelectedItem
        If (Index <> -1) AndAlso (Index > 0) Then
            lstEtapas.Items.RemoveAt(Index)
            lstEtapas.Items.Insert(Index - 1, Swap)
            lstEtapas.SelectedItem = Swap
            WFStepBusiness.UpdateStepOrder(DirectCast(lstEtapas.Items(Index), WFStep).ID, Index + 1)
            WFStepBusiness.UpdateStepOrder(Swap.ID, Index)
        End If
    End Sub

    Private Sub BtnDownIndex_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnDownIndex.Click
        Dim Index As Integer = lstEtapas.SelectedIndex    'Index of selected item
        Dim Swap As Object = lstEtapas.SelectedItem       'Selected Item
        If (Index <> -1) AndAlso (Index + 1 < lstEtapas.Items.Count) Then
            lstEtapas.Items.RemoveAt(Index)                   'Remove it
            lstEtapas.Items.Insert(Index + 1, Swap)           'Add it back in one spot up
            lstEtapas.SelectedItem = Swap                     'Keep this item selected
            WFStepBusiness.UpdateStepOrder(DirectCast(lstEtapas.Items(Index), WFStep).ID, Index + 1)
            WFStepBusiness.UpdateStepOrder(Swap.ID, Index + 2)
        End If
    End Sub

    ''' <summary>
    ''' Carga el listado de etapas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStepOrder()
        lstEtapas.Items.Clear()
        If Not IsNothing(WF) Then
            For Each dr As DataRow In WFStepBusiness.GetOrderedWFAndStepIdsAndNames(WF.ID).Rows
                Dim wfstep As New WFStep(dr("step_id"))
                wfstep.Name = dr("name")
                If (String.IsNullOrEmpty(dr("orden").ToString) OrElse dr("orden") = 0) Then
                    WFStepBusiness.UpdateMaxStepOrder(wfstep.ID, WF.ID)
                End If

                lstEtapas.Items.Add(wfstep)
            Next
        End If
        lstEtapas.DisplayMember = "name"
    End Sub

#Region "Model export"

    Private Shared WFP As New WFTemplates

    Public Shared ReadOnly Property WFTemplates() As WFTemplates
        Get
            If WFP Is Nothing Then
                WFP = New WFTemplates
            End If
            Return WFP
        End Get
    End Property

    Public Shared Sub AddExport(baseWFNode As BaseWFNode)

        WFP.TopMost = True
        'WFP.Left
        WFP.Show()

        Select Case baseWFNode.NodeWFType
            Case NodeWFTypes.WorkFlow
                '--------------------------------- WF
                Dim sn As WFNode = DirectCast(baseWFNode, WFNode)
                WFP.AddWorkflow(sn.WorkFlow, True)

            Case NodeWFTypes.Etapa, NodeWFTypes.Estado
                '--------------------------------- WFStep
                Dim sn As EditStepNode = DirectCast(baseWFNode, EditStepNode)
                WFP.AddStep(sn.WFStep, True)

            Case NodeWFTypes.TipoDeRegla

            Case NodeWFTypes.Regla
                '--------------------------------- Rule
                Dim sn As RuleNode = DirectCast(baseWFNode, RuleNode)
                Dim Rule As IWFRuleParent = Core.WFRulesBusiness.GetInstanceRuleById(sn.RuleId, False)
                If DialogResult.Yes = MessageBox.Show("¿Desea Exportar las reglas hijas?", "Atención", MessageBoxButtons.YesNo) Then
                    WFP.AddRule(Rule)
                    WFP.AddRuleToform(sn, True)
                Else
                    WFP.AddRule(Rule, False)
                    WFP.AddRuleToform(sn, False)
                End If

            Case NodeWFTypes.FloatingRule

                Dim sn As RuleNode = DirectCast(baseWFNode, RuleNode)
                Dim Rule As IWFRuleParent = Core.WFRulesBusiness.GetInstanceRuleById(sn.RuleId, False)
                If DialogResult.Yes = MessageBox.Show("¿Desea Exportar las reglas hijas?", "Atención", MessageBoxButtons.YesNo) Then
                    WFP.AddRule(Rule)
                    WFP.AddRuleToform(sn, True)
                Else
                    WFP.AddRule(Rule, False)
                    WFP.AddRuleToform(sn, False)
                End If

        End Select
    End Sub

    Public Sub LoadReglasInvalidas()
        RemoveHandler WFPanelCircuit.tvwRules.SelectedNodeChanged, AddressOf afselect
        AddHandler WFPanelCircuit.tvwRules.SelectedNodeChanged, AddressOf afselect
    End Sub

#End Region


    Public Sub LoadDebuggerData(ByVal Rule As WFRuleParent, ByVal Tasks As List(Of ITaskResult))
        Try

            If Not Rule Is Nothing Then
                For Each param As String In Rule.DiscoverParams
                    param.Remove(param.Length - 1)
                    param = param.Replace("zvar(", "")
                    Dim paramvalue As String
                    Dim paramtype As String
                    If VariablesInterReglas.ContainsKey(param) Then
                        paramvalue = VariablesInterReglas.Item(param)
                        paramtype = paramvalue.GetType().Name
                    End If
                    Dim r As DataGridViewRow
                    r = DataGridView1.RowTemplate.Clone
                    r.CreateCells(DataGridView1)
                    r.Cells(0).Value = param
                    r.Cells(1).Value = paramvalue
                    r.Cells(2).Value = paramtype
                    DataGridView1.Rows.Add(r)
                Next
            End If

            If Not Tasks Is Nothing Then
                For Each index As IIndex In Tasks(0).Indexs
                    Dim r As DataGridViewRow
                    r = DataGridView2.RowTemplate.Clone
                    r.CreateCells(DataGridView2)
                    r.Cells(0).Value = index.Name
                    r.Cells(1).Value = index.Data
                    r.Cells(2).Value = index.Type
                    DataGridView2.Rows.Add(r)
                Next
            End If

            If Not VariablesInterReglas._VariablesInterReglas Is Nothing Then
                For Each paramname As String In VariablesInterReglas._VariablesInterReglas.Keys

                    If VariablesInterReglas.Item(paramname).GetType.Name = "String" Then

                        Dim paramvalue As String
                        Dim paramtype As String

                        paramvalue = VariablesInterReglas.Item(paramname)
                        paramtype = paramvalue.GetType().Name

                        Dim r As DataGridViewRow
                        r = DataGridView3.RowTemplate.Clone
                        r.CreateCells(DataGridView3)
                        r.Cells(0).Value = paramname
                        r.Cells(1).Value = paramvalue
                        r.Cells(2).Value = paramtype
                        DataGridView3.Rows.Add(r)
                    End If
                Next
            End If

            If Not VariablesInterReglas._VariablesGlobales Is Nothing Then
                For Each paramname As String In VariablesInterReglas._VariablesGlobales.Keys

                    If VariablesInterReglas.Item(paramname).GetType.Name = "String" Then

                        Dim paramvalue As String
                        Dim paramtype As String

                        paramvalue = VariablesInterReglas.Item(paramname)
                        paramtype = paramvalue.GetType().Name

                        Dim r As New DataGridViewRow()
                        r = DataGridView3.RowTemplate.Clone
                        r.CreateCells(DataGridView3)
                        r.Cells(0).Value = paramname
                        r.Cells(1).Value = paramvalue
                        r.Cells(2).Value = paramtype
                        DataGridView3.Rows.Add(r)
                    End If
                Next
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub afselect(sender As Object, e As RadTreeViewEventArgs)
        Dim newBaseWFNode As BaseWFNode = Nothing

        Try
            If TypeOf WFPanelCircuit.tvwRules.SelectedNode Is BaseWFNode Then
                newBaseWFNode = DirectCast(WFPanelCircuit.tvwRules.SelectedNode, BaseWFNode)

                If newBaseWFNode IsNot Nothing AndAlso (newBaseWFNode.NodeWFType = NodeWFTypes.Etapa OrElse newBaseWFNode.NodeWFType = NodeWFTypes.Estado) Then
                    Dim sn As EditStepNode = DirectCast(newBaseWFNode, EditStepNode)
                    dttable2 = WFRulesBusiness.GetRulesDSByStepID(sn.WFStep.ID, False)
                    sn = Nothing

                    For Each datrow As DataRow In dttable2.Tables(0).Rows
                        dttable.Tables(0).Rows.Add(New Object() {datrow(0).ToString, datrow(1).ToString, "invalida"})
                    Next

                    dgvreglasInvalidas = New DataGridView
                    dgvreglasInvalidas.Dock = DockStyle.Fill
                    dgvreglasInvalidas.DataSource = dttable.Tables(0)

                    If wfTabControl.TabPages("Reglas Invalidas") IsNot Nothing Then wfTabControl.TabPages("Reglas Invalidas").Controls.Add(dgvreglasInvalidas)
                End If
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            newBaseWFNode = Nothing
        End Try
    End Sub

    Private Sub TabPageWF_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabPageWF.SelectedIndexChanged
        If TabPageWF.SelectedIndex = 2 AndAlso tabGeneralRules.Controls.Count = 0 Then
            Try
                'Reglas Generales
                UCButtonsABM = New UCButtonsABM("   Reglas Generales", ButtonType.Rule, WF.ID)
                UCButtonsABM.Dock = DockStyle.Fill
                tabGeneralRules.Controls.Add(UCButtonsABM)
            Catch ex As Exception
                ZClass.raiseerror(ex)

                If UCButtonsABM Is Nothing Then
                    Dim lblError As Label = New Label()
                    lblError.Text = "Ha ocurrido un error al cargar los botones dinámicos"
                    tabGeneralRules.Controls.Add(lblError)
                Else
                    UCButtonsABM.dgvButtons.Rows.Add(New Object() {"Ha ocurrido un error al cargar los botones dinámicos"})
                End If
            Finally
                RemoveHandler TabPageWF.SelectedIndexChanged, AddressOf TabPageWF_SelectedIndexChanged
            End Try
        End If
    End Sub
End Class
