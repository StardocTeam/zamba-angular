Imports Zamba.Core.Enumerators
Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.AdminControls
Imports Zamba.Simulator
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class WFPanelCircuit
    Inherits ZControl
    Implements IWFPanelCircuit
    Implements IIfWFPanelCircuit

#Region " Windows Form Designer generated code "
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        Catch
        End Try
    End Sub



    'Required by the Windows Form Designer
    Private components As ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TreeViewContextMenu As ContextMenu
    Friend WithEvents mnuInicial As System.Windows.Forms.MenuItem
    Friend WithEvents PanelTop As ZLabel
    Friend WithEvents tvwRules As RadTreeView
    Friend WithEvents mnuPegarRegla As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCambiarNombre As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCortarRegla As System.Windows.Forms.MenuItem
    Friend WithEvents mnuEliminar As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAgregarAccionUsuario As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSeleccionar As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCopiarRegla As System.Windows.Forms.MenuItem
    Friend WithEvents mnuExpadirRegla As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCategoriaRegla As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAgregaraProcesoSim As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCrearSimNueva As System.Windows.Forms.MenuItem
    Friend WithEvents panelToolbar As ZPanel
    Friend WithEvents panelToolbarNavigate As ZPanel
    Friend WithEvents tbrWfTools As ZToolBar
    Friend WithEvents btnPrintPreview As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnExportBMP As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuExport As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAddProcess As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLine1 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLine2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLine3 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLine4 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLine5 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLine6 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLine7 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLine8 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLine9 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLine10 As System.Windows.Forms.MenuItem
    Friend WithEvents btnSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnBack As ZButton
    Friend WithEvents btnFoward As ZButton
    Private WithEvents cbLastRulesList As RadDropDownList
    Friend WithEvents btnChecks As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuAgregarRegla As System.Windows.Forms.MenuItem
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        TreeViewContextMenu = New ContextMenu()
        mnuCambiarNombre = New System.Windows.Forms.MenuItem()
        mnuLine6 = New System.Windows.Forms.MenuItem()
        mnuEliminar = New System.Windows.Forms.MenuItem()
        mnuLine7 = New System.Windows.Forms.MenuItem()
        mnuCortarRegla = New System.Windows.Forms.MenuItem()
        mnuCopiarRegla = New System.Windows.Forms.MenuItem()
        mnuPegarRegla = New System.Windows.Forms.MenuItem()
        mnuLine10 = New System.Windows.Forms.MenuItem()
        mnuAgregarRegla = New System.Windows.Forms.MenuItem()
        mnuAgregarAccionUsuario = New System.Windows.Forms.MenuItem()
        mnuAddProcess = New System.Windows.Forms.MenuItem()
        mnuLine2 = New System.Windows.Forms.MenuItem()
        mnuInicial = New System.Windows.Forms.MenuItem()
        mnuLine3 = New System.Windows.Forms.MenuItem()
        mnuSeleccionar = New System.Windows.Forms.MenuItem()
        mnuLine8 = New System.Windows.Forms.MenuItem()
        mnuExpadirRegla = New System.Windows.Forms.MenuItem()
        mnuLine4 = New System.Windows.Forms.MenuItem()
        mnuCategoriaRegla = New System.Windows.Forms.MenuItem()
        mnuLine5 = New System.Windows.Forms.MenuItem()
        mnuAgregaraProcesoSim = New System.Windows.Forms.MenuItem()
        mnuLine1 = New System.Windows.Forms.MenuItem()
        mnuExport = New System.Windows.Forms.MenuItem()
        mnuLine9 = New System.Windows.Forms.MenuItem()
        mnuCrearSimNueva = New System.Windows.Forms.MenuItem()
        PanelTop = New ZLabel()
        tvwRules = New Telerik.WinControls.UI.RadTreeView()
        panelToolbar = New ZPanel()
        tbrWfTools = New Zamba.AppBlock.ZToolBar()
        btnPrint = New System.Windows.Forms.ToolStripButton()
        btnPrintPreview = New System.Windows.Forms.ToolStripButton()
        btnExportBMP = New System.Windows.Forms.ToolStripButton()
        btnSearch = New System.Windows.Forms.ToolStripButton()
        btnChecks = New System.Windows.Forms.ToolStripButton()
        panelToolbarNavigate = New ZPanel()
        btnBack = New ZButton()
        cbLastRulesList = New Telerik.WinControls.UI.RadDropDownList()
        btnFoward = New ZButton()
        CType(tvwRules, ComponentModel.ISupportInitialize).BeginInit()
        panelToolbar.SuspendLayout()
        tbrWfTools.SuspendLayout()
        panelToolbarNavigate.SuspendLayout()
        CType(cbLastRulesList, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'TreeViewContextMenu
        '
        TreeViewContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {mnuCambiarNombre, mnuLine6, mnuEliminar, mnuLine7, mnuCortarRegla, mnuCopiarRegla, mnuPegarRegla, mnuLine10, mnuAgregarRegla, mnuAgregarAccionUsuario, mnuAddProcess, mnuLine2, mnuInicial, mnuLine3, mnuSeleccionar, mnuLine8, mnuExpadirRegla, mnuLine4, mnuCategoriaRegla, mnuLine5, mnuAgregaraProcesoSim, mnuLine1, mnuExport, mnuLine9, mnuCrearSimNueva})
        '
        'mnuCambiarNombre
        '
        mnuCambiarNombre.Index = 0
        mnuCambiarNombre.Text = "Cambiar Nombre"
        '
        'mnuLine6
        '
        mnuLine6.Index = 1
        mnuLine6.Text = "-"
        '
        'mnuEliminar
        '
        mnuEliminar.Index = 2
        mnuEliminar.Text = "Eliminar "
        '
        'mnuLine7
        '
        mnuLine7.Index = 3
        mnuLine7.Text = "-"
        '
        'mnuCortarRegla
        '
        mnuCortarRegla.Index = 4
        mnuCortarRegla.Text = "Cortar Regla"
        '
        'mnuCopiarRegla
        '
        mnuCopiarRegla.Index = 5
        mnuCopiarRegla.Text = "Copiar Regla"
        '
        'mnuPegarRegla
        '
        mnuPegarRegla.Index = 6
        mnuPegarRegla.Text = "Pegar Regla"
        '
        'mnuLine10
        '
        mnuLine10.Index = 7
        mnuLine10.Text = "-"
        '
        'mnuAgregarRegla
        '
        mnuAgregarRegla.Index = 8
        mnuAgregarRegla.Text = "Agregar Regla"
        '
        'mnuAgregarAccionUsuario
        '
        mnuAgregarAccionUsuario.Index = 9
        mnuAgregarAccionUsuario.Text = "Agregar Accion de Usuario"
        '
        'mnuAddProcess
        '
        mnuAddProcess.Index = 10
        mnuAddProcess.Text = "Agregar Proceso"
        '
        'mnuLine2
        '
        mnuLine2.Index = 11
        mnuLine2.Text = "-"
        '
        'mnuInicial
        '
        mnuInicial.Index = 12
        mnuInicial.Text = "Inicial"
        '
        'mnuLine3
        '
        mnuLine3.Index = 13
        mnuLine3.Text = "-"
        '
        'mnuSeleccionar
        '
        mnuSeleccionar.Index = 14
        mnuSeleccionar.Text = "Seleccionar"
        '
        'mnuLine8
        '
        mnuLine8.Index = 15
        mnuLine8.Text = "-"
        '
        'mnuExpadirRegla
        '
        mnuExpadirRegla.Index = 16
        mnuExpadirRegla.Text = "Expandir todo"
        '
        'mnuLine4
        '
        mnuLine4.Index = 17
        mnuLine4.Text = "-"
        '
        'mnuCategoriaRegla
        '
        mnuCategoriaRegla.Index = 18
        mnuCategoriaRegla.Text = "Modificar Categoria"
        '
        'mnuLine5
        '
        mnuLine5.Index = 19
        mnuLine5.Text = "-"
        '
        'mnuAgregaraProcesoSim
        '
        mnuAgregaraProcesoSim.Index = 20
        mnuAgregaraProcesoSim.Text = "Agregar a proceso de Simulacion"
        '
        'mnuLine1
        '
        mnuLine1.Index = 21
        mnuLine1.Text = "-"
        '
        'mnuExport
        '
        mnuExport.Index = 22
        mnuExport.Text = "Exportar Reglas"
        '
        'mnuLine9
        '
        mnuLine9.Index = 23
        mnuLine9.Text = "-"
        '
        'mnuCrearSimNueva
        '
        mnuCrearSimNueva.Index = 24
        mnuCrearSimNueva.Text = "Probar regla Individualmente"
        '
        'PanelTop
        '
        PanelTop.BackColor = System.Drawing.Color.Transparent
        PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        PanelTop.Font = New Font("Verdana", 9.75!)
        PanelTop.FontSize = 9.75!
        PanelTop.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        PanelTop.Location = New System.Drawing.Point(0, 0)
        PanelTop.Name = "PanelTop"
        PanelTop.Size = New System.Drawing.Size(315, 29)
        PanelTop.TabIndex = 97
        PanelTop.Text = " Proceso"
        PanelTop.TextAlign = ContentAlignment.MiddleLeft
        '
        'tvwRules
        '
        tvwRules.AllowArbitraryItemHeight = True
        tvwRules.AllowDrop = True
        tvwRules.BackColor = System.Drawing.Color.White
        tvwRules.ContextMenu = TreeViewContextMenu
        tvwRules.Dock = System.Windows.Forms.DockStyle.Fill
        tvwRules.ExpandAnimation = Telerik.WinControls.UI.ExpandAnimation.None
        tvwRules.Font = New Font("Tahoma", 14.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        tvwRules.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        tvwRules.HideSelection = False
        tvwRules.HotTracking = False
        tvwRules.ItemHeight = 22
        tvwRules.Location = New System.Drawing.Point(0, 87)
        tvwRules.Margin = New System.Windows.Forms.Padding(0)
        tvwRules.Name = "tvwRules"
        tvwRules.PlusMinusAnimationStep = 1.0R
        '
        '
        '
        tvwRules.RootElement.AutoSize = False
        tvwRules.RootElement.ControlBounds = New System.Drawing.Rectangle(0, 87, 150, 250)
        tvwRules.RootElement.UseDefaultDisabledPaint = False
        tvwRules.ShowLines = True
        tvwRules.ShowNodeToolTips = True
        tvwRules.Size = New System.Drawing.Size(315, 385)
        tvwRules.SpacingBetweenNodes = -1
        tvwRules.TabIndex = 98
        tvwRules.ThemeName = "TelerikMetroBlue"
        tvwRules.TreeIndent = 18
        '
        'panelToolbar
        '
        panelToolbar.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        panelToolbar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        panelToolbar.Controls.Add(tbrWfTools)
        panelToolbar.Dock = System.Windows.Forms.DockStyle.Top
        panelToolbar.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        panelToolbar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        panelToolbar.Location = New System.Drawing.Point(0, 58)
        panelToolbar.Name = "panelToolbar"
        panelToolbar.Size = New System.Drawing.Size(315, 29)
        panelToolbar.TabIndex = 99
        '
        'tbrWfTools
        '
        tbrWfTools.Dock = System.Windows.Forms.DockStyle.Fill
        tbrWfTools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {btnPrint, btnPrintPreview, btnExportBMP, btnSearch, btnChecks})
        tbrWfTools.Location = New System.Drawing.Point(0, 0)
        tbrWfTools.Name = "tbrWfTools"
        tbrWfTools.Size = New System.Drawing.Size(313, 27)
        tbrWfTools.TabIndex = 0
        '
        'btnPrint
        '
        btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        btnPrint.Image = Global.Zamba.Controls.My.Resources.Resources.iconPrint
        btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New System.Drawing.Size(23, 24)
        btnPrint.ToolTipText = "Imprime el árbol de workflow completo"
        '
        'btnPrintPreview
        '
        btnPrintPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        btnPrintPreview.Image = Global.Zamba.Controls.My.Resources.Resources.Control_PrintPreviewControl
        btnPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        btnPrintPreview.Name = "btnPrintPreview"
        btnPrintPreview.Size = New System.Drawing.Size(23, 24)
        btnPrintPreview.ToolTipText = "Previsualiza la impresión del árbol de workflow"
        '
        'btnExportBMP
        '
        btnExportBMP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        btnExportBMP.Image = Global.Zamba.Controls.My.Resources.Resources.save_icon
        btnExportBMP.ImageTransparentColor = System.Drawing.Color.Magenta
        btnExportBMP.Name = "btnExportBMP"
        btnExportBMP.Size = New System.Drawing.Size(23, 24)
        btnExportBMP.ToolTipText = "Exporta a una imagen el diseño de workflow"
        '
        'btnSearch
        '
        btnSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        btnSearch.BackColor = System.Drawing.Color.White
        btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        btnSearch.Name = "btnSearch"
        btnSearch.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
        btnSearch.Size = New System.Drawing.Size(46, 24)
        btnSearch.Text = "Buscar"
        btnSearch.ToolTipText = "Búsqueda de reglas"
        '
        'btnChecks
        '
        btnChecks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        btnChecks.Image = Global.Zamba.Controls.My.Resources.Resources.check2
        btnChecks.ImageAlign = ContentAlignment.MiddleLeft
        btnChecks.ImageTransparentColor = System.Drawing.Color.Magenta
        btnChecks.Name = "btnChecks"
        btnChecks.Size = New System.Drawing.Size(23, 24)
        btnChecks.ToolTipText = "Muestra u oculta la opción para tildar procesos, etapas y reglas"
        '
        'panelToolbarNavigate
        '
        panelToolbarNavigate.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        panelToolbarNavigate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        panelToolbarNavigate.Controls.Add(btnBack)
        panelToolbarNavigate.Controls.Add(cbLastRulesList)
        panelToolbarNavigate.Controls.Add(btnFoward)
        panelToolbarNavigate.Dock = System.Windows.Forms.DockStyle.Top
        panelToolbarNavigate.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        panelToolbarNavigate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        panelToolbarNavigate.Location = New System.Drawing.Point(0, 29)
        panelToolbarNavigate.Name = "panelToolbarNavigate"
        panelToolbarNavigate.Size = New System.Drawing.Size(315, 29)
        panelToolbarNavigate.TabIndex = 99
        '
        'btnBack
        '
        btnBack.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnBack.Dock = System.Windows.Forms.DockStyle.Left
        btnBack.FlatStyle = FlatStyle.Flat
        btnBack.ForeColor = System.Drawing.Color.White
        btnBack.Location = New System.Drawing.Point(0, 0)
        btnBack.Name = "btnBack"
        btnBack.Size = New System.Drawing.Size(46, 27)
        btnBack.TabIndex = 0
        btnBack.Text = "<<"
        btnBack.UseVisualStyleBackColor = False
        '
        'cbLastRulesList
        '
        cbLastRulesList.BackColor = System.Drawing.Color.White
        cbLastRulesList.Dock = System.Windows.Forms.DockStyle.Fill
        cbLastRulesList.Location = New System.Drawing.Point(0, 0)
        cbLastRulesList.Name = "cbLastRulesList"
        '
        '
        '
        cbLastRulesList.RootElement.ControlBounds = New System.Drawing.Rectangle(0, 0, 125, 20)
        cbLastRulesList.RootElement.StretchVertically = True
        cbLastRulesList.Size = New System.Drawing.Size(267, 27)
        cbLastRulesList.TabIndex = 1
        '
        'btnFoward
        '
        btnFoward.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(224, Byte), Integer))
        btnFoward.Dock = System.Windows.Forms.DockStyle.Right
        btnFoward.FlatStyle = FlatStyle.Flat
        btnFoward.ForeColor = System.Drawing.Color.White
        btnFoward.Location = New System.Drawing.Point(267, 0)
        btnFoward.Name = "btnFoward"
        btnFoward.Size = New System.Drawing.Size(46, 27)
        btnFoward.TabIndex = 2
        btnFoward.Text = ">>"
        btnFoward.UseVisualStyleBackColor = False
        '
        'WFPanelCircuit
        '
        BackColor = System.Drawing.Color.White
        Controls.Add(tvwRules)
        Controls.Add(panelToolbar)
        Controls.Add(panelToolbarNavigate)
        Controls.Add(PanelTop)
        Name = "WFPanelCircuit"
        Size = New System.Drawing.Size(315, 472)
        CType(tvwRules, ComponentModel.ISupportInitialize).EndInit()
        panelToolbar.ResumeLayout(False)
        panelToolbar.PerformLayout()
        tbrWfTools.ResumeLayout(False)
        tbrWfTools.PerformLayout()
        panelToolbarNavigate.ResumeLayout(False)
        panelToolbarNavigate.PerformLayout()
        CType(cbLastRulesList, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

#End Region

#Region "Atributos y Propiedades"
    Private _WF As Core.WorkFlow
    Private _IL As Zamba.AppBlock.ZIconsList
    Private _treeViewHelper As Zamba.Core.IPrintTreeViewHelper = Nothing
    Public Event SearchRulesInWorkflow(ByVal workflowId As Int64)
    Public Property DShowSearchForm() As [Delegate]

    Public Property WF() As IWorkFlow
        Get
            Return _WF
        End Get
        Set(ByVal value As IWorkFlow)
            _WF = value
        End Set
    End Property

    Public Property Navigating As Boolean
    Public Property RightType As RightsType
#End Region

#Region "Constructor y carga inicial"
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
    End Sub

    Public Sub New(ByVal WF As Core.WorkFlow, ByVal IL As Zamba.AppBlock.ZIconsList, ByVal RightType As RightsType)
        Me.New()
        _IL = IL
        Me.RightType = RightType
        LoadWf(WF, IL)
        cbLastRulesList.DataSource = LastRulesList
        cbLastRulesList.DisplayMember = "Name"
        If RightType = RightsType.View Then
            tvwRules.AllowEdit = False
            tvwRules.ContextMenu.MenuItems.Clear()
        End If
    End Sub

    Private _isTreeViewLoading As Boolean
    Public Property IsTreeViewLoading() As Boolean
        Get
            Return _isTreeViewLoading
        End Get
        Set(ByVal value As Boolean)
            _isTreeViewLoading = value
        End Set
    End Property

    Public Sub LoadWf(ByVal wf As Core.WorkFlow, ByVal IL As Zamba.AppBlock.ZIconsList)
        Try
            IsTreeViewLoading = True
            _WF = wf
            WFRulesBusiness.LoadRules(wf, tvwRules, False)

            PanelTop.Text = _WF.Name & " (" & _WF.ID.ToString & ")"
            AddHandler WFRuleParent.RuleToExecute, AddressOf RuleToExecute
            AddHandler WFRuleParent.RuleExecuted, AddressOf RuleExecuted
            AddHandler WFRuleParent.RuleExecutedError, AddressOf RuleExecutedError
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            IsTreeViewLoading = False
        End Try

    End Sub
#End Region

#Region "IWFPanelCircuit + IIfWFPanelCircuit"
    Public Sub UpdateRuleType() Implements IWFPanelCircuit.UpdateRuleType
        Dim _rulenode As RuleNode = DirectCast(tvwRules.SelectedNode, RuleNode)
        RaiseEvent RuleSelected(_rulenode.RuleId)
    End Sub
    Public Sub UpdateRuleTypeIfdesign() Implements IIfWFPanelCircuit.UpdateRuleTypeIfDesign
        Dim _rulenode As RuleNode = DirectCast(tvwRules.SelectedNode, RuleNode)
        RaiseEvent RuleSelected(_rulenode.RuleId)
    End Sub
#End Region

#Region "WFTransitions"
    Public Sub SelectStepNode(ByRef wfstep As WFStep)
        Try
            For Each n As RadTreeNode In tvwRules.Nodes(0).Nodes
                If TypeOf n Is EditStepNode AndAlso DirectCast(n, EditStepNode).WFStep Is wfstep Then
                    tvwRules.SelectedNode = n
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub AddNewStep(ByRef wfstep As WFStep)
        Try
            Dim wf As IWorkFlow
            wf = WFBusiness.GetWFbyId(wfstep.WorkId)
            Dim node As New EditStepNode(wfstep, wf.InitialStep)
            DirectCast(node.InputNode, RuleTypeNode).Image = Global.Zamba.Controls.My.Resources.Resources.appbar_cabinet_in
            DirectCast(node.InputValidationNode, RuleTypeNode).Image = Global.Zamba.Controls.My.Resources.Resources.appbar_list_check
            DirectCast(node.OutputNode, RuleTypeNode).Image = Global.Zamba.Controls.My.Resources.Resources.appbar_cabinet_out
            DirectCast(node.OutputValidationNode, RuleTypeNode).Image = Global.Zamba.Controls.My.Resources.Resources.appbar_checkmark_pencil
            DirectCast(node.UpdateNode, RuleTypeNode).Image = Global.Zamba.Controls.My.Resources.Resources.appbar_refresh
            DirectCast(node.UserActionNode, RuleTypeNode).Image = Global.Zamba.Controls.My.Resources.Resources.appbar_user
            DirectCast(node.ScheduleNode, RuleTypeNode).Image = Global.Zamba.Controls.My.Resources.Resources.appbar_timer_forward
            DirectCast(node.RightNode, RightNode).Image = Global.Zamba.Controls.My.Resources.Resources.appbar_key
            DirectCast(node.FloatingNode, RuleTypeNode).Image = Global.Zamba.Controls.My.Resources.Resources.appbar_list_gear
            DirectCast(node.EventNode, RuleTypeNode).Image = Global.Zamba.Controls.My.Resources.Resources.appbar_alert

            node.Image = Global.Zamba.Controls.My.Resources.Resources.appbar_text_align_right
            tvwRules.Nodes(0).Nodes.Add(node)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub RemoveStep(ByVal DelWFStep As WFStep)
        Try
            For Each n As RadTreeNode In tvwRules.Nodes(0).Nodes
                If TypeOf n Is EditStepNode AndAlso DirectCast(n, EditStepNode).WFStep Is DelWFStep Then
                    tvwRules.Nodes(0).Nodes.Remove(n)
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub NameStep(ByVal NameWFStep As WFStep)
        Try
            For Each n As RadTreeNode In tvwRules.Nodes(0).Nodes
                If TypeOf n Is EditStepNode AndAlso DirectCast(n, EditStepNode).WFStep Is NameWFStep Then
                    n.Text = NameWFStep.Name
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub AddNewRule(ByRef rulestep As WFStep, ByVal IDRule As Int64)
        Try
            Dim DTStepsRulesOptions As DataTable = WFRulesBusiness.GetRuleOptionsDT(False, rulestep.ID)
            For Each n As RadTreeNode In tvwRules.Nodes(0).Nodes
                If TypeOf n Is EditStepNode AndAlso DirectCast(n, EditStepNode).WFStep Is rulestep Then
                    Dim StepNode As EditStepNode
                    StepNode = DirectCast(n, EditStepNode)
                    For Each r As DsRules.WFRulesRow In StepNode.WFStep.DSRules.WFRules.Rows
                        If r.Id = IDRule Then

                            Dim DVRuleOptions As New DataView(DTStepsRulesOptions)
                            DVRuleOptions.RowFilter = "RuleId = " & r.Id
                            Dim DTRuleOptions As DataTable = DVRuleOptions.ToTable()

                            r.Enable = True
                            Dim DisableChildRules As Boolean
                            Dim RefreshTask As Boolean
                            Dim IconId As Int32 = Icons.YellowBall

                            If DTRuleOptions.Rows.Count > 0 Then
                                For Each o As DataRow In DTRuleOptions.Rows
                                    Select Case o("ObjectId")
                                        Case 0
                                            r.Enable = DirectCast(o("ObjValue"), Decimal)
                                        Case 59
                                            DisableChildRules = Boolean.Parse(o("ObjValue").ToString)
                                        Case 42
                                            RefreshTask = Boolean.Parse(o("ObjValue").ToString)
                                        Case 63
                                            IconId = Boolean.Parse(o("ObjValue").ToString)
                                    End Select
                                Next
                            End If

                            Dim RuleNode As New RuleNode(r, WFRulesBusiness.GetIcon(r.Enable, r._Class, DisableChildRules, RefreshTask, IconId, r.Name))
                            RuleNode.Image = GetImageBasedOnClass(r._Class, r.Name)

                            Select Case r.ParentType
                                Case TypesofRules.Entrada
                                    DirectCast(StepNode.InputNode, RuleTypeNode).Nodes.Add(RuleNode)
                                Case TypesofRules.ValidacionEntrada
                                    DirectCast(StepNode.InputValidationNode, RuleTypeNode).Nodes.Add(RuleNode)
                                Case TypesofRules.Salida
                                    DirectCast(StepNode.OutputNode, RuleTypeNode).Nodes.Add(RuleNode)
                                Case TypesofRules.ValidacionSalida
                                    DirectCast(StepNode.OutputValidationNode, RuleTypeNode).Nodes.Add(RuleNode)
                                Case TypesofRules.Actualizacion
                                    DirectCast(StepNode.UpdateNode, RuleTypeNode).Nodes.Add(RuleNode)
                                Case TypesofRules.Planificada
                                    DirectCast(StepNode.ScheduleNode, RuleTypeNode).Nodes.Add(RuleNode)
                                Case TypesofRules.AccionUsuario
                                    DirectCast(StepNode.UserActionNode, RuleTypeNode).Nodes.Add(RuleNode)
                                Case TypesofRules.Floating
                                    DirectCast(StepNode.FloatingNode, RuleTypeNode).Nodes.Add(RuleNode)
                            End Select
                            Exit For
                        End If
                    Next
                    Exit For
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Function GetImageBasedOnClass(RuleClass As String, rulename As String) As Image
        Dim strtocompare = RuleClass.ToLower & " " & rulename.ToLower
        If strtocompare.Contains("excel") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_page_excel
        ElseIf strtocompare.Contains("word") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_page_word
        ElseIf strtocompare.Contains("explorer") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_browser_wire
        ElseIf strtocompare.Contains("mail") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_email_hardedge
        ElseIf strtocompare.Contains("outlook") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_office_outlook
        ElseIf strtocompare.Contains("pdf") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_page_file_pdf
        ElseIf strtocompare.Contains("asign") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_user_add
        ElseIf strtocompare.Contains("user") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_user_add
        ElseIf strtocompare.Contains("group") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_user_add
        ElseIf strtocompare.Contains("aprobar") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_check
        ElseIf strtocompare.Contains("rechazar") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_thumbs_down
        ElseIf strtocompare.Contains("valida") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_checkmark_pencil_top
        ElseIf strtocompare.Contains("alta") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_new
        ElseIf strtocompare.Contains("eliminar") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_delete
        ElseIf strtocompare.Contains("borrar") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_delete
        ElseIf strtocompare.Contains("quitar") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_delete
        ElseIf strtocompare.Contains("asociar") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_add
        ElseIf strtocompare.Contains("nuevo") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_new
        ElseIf strtocompare.Contains("agregar") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_add
        ElseIf strtocompare.Contains("asociar") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_add
        ElseIf strtocompare.Contains("digitalizar") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_printer_blank
        ElseIf strtocompare.Contains("crear") OrElse strtocompare.Contains("generar") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_new
        ElseIf strtocompare.Contains("consulta") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_chat
        ElseIf strtocompare.Contains("derivar") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_people_arrow_right
        ElseIf strtocompare.Contains("modificar") OrElse strtocompare.Contains("editar") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_edit
        ElseIf strtocompare.Contains("solicitud") OrElse strtocompare.Contains("solicitar") Then
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_chat
        Else
            Return Global.Zamba.Controls.My.Resources.Resources.appbar_list_gear
        End If
    End Function
#End Region

#Region "Context Menu"
    ''' <summary>
    ''' Evento que muestra el menú contextual al hacer un click derecho sobre un elemento del árbol
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TreeViewContextMenu_Popup(ByVal sender As Object, ByVal e As EventArgs) Handles TreeViewContextMenu.Popup
        EditContextMenu()
    End Sub
    Private Sub EditContextMenu()
        Try
            For Each item As MenuItem In TreeViewContextMenu.MenuItems
                item.Visible = False
            Next

            'Todos los nodos tienen que tener la opcion de expandir reglas
            mnuExpadirRegla.Visible = True

            If TypeOf (tvwRules.SelectedNode) Is BaseWFNode Then
                Dim base As BaseWFNode = DirectCast(tvwRules.SelectedNode, BaseWFNode)
                mnuExport.Visible = True

                Select Case base.NodeWFType

                    Case NodeWFTypes.Etapa, NodeWFTypes.Estado
                        EditStepContextMenu()

                    Case NodeWFTypes.TipoDeRegla
                        EditRuleTypeContextMenu(base)

                    Case NodeWFTypes.Regla
                        EditRuleContextMenu(DirectCast(base, RuleNode).RuleClass.ToUpper)

                    Case NodeWFTypes.FloatingRule
                        EditFloatingRuleContextMenu(DirectCast(base, RuleNode).RuleClass.ToUpper)

                End Select
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub EditStepContextMenu()
        Dim sn As EditStepNode = DirectCast(tvwRules.SelectedNode, EditStepNode)

        If sn.WFStep.ID = WF.InitialStep.ID Then
            'Si es inicial
            mnuEliminar.Visible = False
            mnuLine10.Visible = False
        Else
            'Si no es Inicial
            mnuEliminar.Visible = True
            mnuLine10.Visible = True
            mnuInicial.Visible = True
        End If
        mnuAgregarAccionUsuario.Visible = True
    End Sub
    Private Sub EditFloatingRuleContextMenu(ruleClassUpper As String)
        mnuAgregarRegla.Visible = True
        mnuAddProcess.Visible = True
        mnuEliminar.Visible = True
        mnuCopiarRegla.Visible = True
        mnuCortarRegla.Visible = True
        mnuCategoriaRegla.Visible = True

        If ruleClassUpper.StartsWith("IF") AndAlso ruleClassUpper <> "IFBRANCH" Then
            mnuAgregarRegla.Visible = False
            mnuAddProcess.Visible = False

        ElseIf ruleClassUpper = "IFBRANCH" Then
            mnuEliminar.Visible = False
            mnuCopiarRegla.Visible = False
            mnuCortarRegla.Visible = False
        End If

        mnuCambiarNombre.Visible = True

        If ((IsNothing(WFRulesBusiness.CopiedRuleNode)) AndAlso (IsNothing(WFRulesBusiness.CuttedRuleNode))) Then
            mnuPegarRegla.Visible = False
        Else
            mnuPegarRegla.Visible = True
        End If
    End Sub
    Private Sub EditRuleContextMenu(ruleClassUpper As String)
        mnuAgregarRegla.Visible = True
        mnuAddProcess.Visible = True
        mnuEliminar.Visible = True
        mnuCopiarRegla.Visible = True
        mnuCortarRegla.Visible = True
        mnuCategoriaRegla.Visible = True

        If ruleClassUpper.StartsWith("IF") AndAlso ruleClassUpper <> "IFBRANCH" Then
            mnuAgregarRegla.Visible = False
            mnuAddProcess.Visible = False

        ElseIf ruleClassUpper = "IFBRANCH" Then
            mnuEliminar.Visible = False
            mnuCopiarRegla.Visible = False
            mnuCortarRegla.Visible = False
        End If

        mnuCambiarNombre.Visible = True

        If WFRulesBusiness.CopiedRuleNode Is Nothing AndAlso WFRulesBusiness.CuttedRuleNode Is Nothing Then
            mnuPegarRegla.Visible = False
        Else
            mnuPegarRegla.Visible = True
        End If

        Dim tc As Control = DirectCast(Parent.Parent.Parent, SplitContainer).Panel2.Controls(0).Controls(0)
        If tc.GetType() Is GetType(TabControl) Then
            If DirectCast(tc, TabControl).TabPages(1).Controls.Count > 0 AndAlso
                DirectCast(tc, TabControl).TabPages(1).Controls(0).GetType() Is GetType(UcSimulationManager) Then
                mnuAgregaraProcesoSim.Visible = True
                mnuCrearSimNueva.Visible = True
            End If
        End If
        tc = Nothing
    End Sub
    Private Sub EditRuleTypeContextMenu(base As BaseWFNode)
        mnuAgregarRegla.Visible = True
        mnuAddProcess.Visible = True

        'SI ES ACCION DE USUARIO                  
        If DirectCast(base, RuleTypeNode).RuleParentType = TypesofRules.AccionUsuario Then
            Dim haveNodes As Boolean
            For Each n As RadTreeNode In base.Nodes
                haveNodes = True
                Exit For
            Next

            mnuEliminar.Visible = True
            If haveNodes Then
                mnuAgregarRegla.Visible = False
                mnuAddProcess.Visible = False
                mnuCambiarNombre.Visible = True
            Else
                mnuExpadirRegla.Visible = False
            End If
        End If

        If ((IsNothing(WFRulesBusiness.CopiedRuleNode)) AndAlso (IsNothing(WFRulesBusiness.CuttedRuleNode))) Then
            mnuPegarRegla.Visible = False
        Else
            mnuPegarRegla.Visible = True
        End If
    End Sub

    Private Sub mnuEliminar_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuEliminar.Click

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

        Delete()
    End Sub
    Private Sub Delete()
        If tvwRules IsNot Nothing AndAlso tvwRules.SelectedNode IsNot Nothing AndAlso
            TypeOf (tvwRules.SelectedNode) Is BaseWFNode Then

            RemoveHandler tvwRules.SelectedNodeChanged, AddressOf TreeView1_AfterSelect
            Dim node As BaseWFNode

            Try
                node = DirectCast(tvwRules.SelectedNode, BaseWFNode)

                Select Case node.NodeWFType
                    Case NodeWFTypes.Etapa, NodeWFTypes.Regla, NodeWFTypes.FloatingRule, NodeWFTypes.Estado
                        WFBusiness.Remove(node)

                    Case NodeWFTypes.TipoDeRegla
                        If MessageBox.Show("¿Desea eliminar las reglas?", "Edicion de Reglas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            WFBusiness.Remove(DirectCast(tvwRules.SelectedNode, BaseWFNode))
                        End If
                End Select

                TreeViewSelect()
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            Finally
                AddHandler tvwRules.SelectedNodeChanged, AddressOf TreeView1_AfterSelect
                node = Nothing
            End Try
        End If
    End Sub

    Private Sub CopiarRegla_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuCopiarRegla.Click
        CopiarRegla()
    End Sub
    Private Sub CopiarRegla()
        Dim copyChildRules As Boolean = False

        Try
            If tvwRules.SelectedNode.Nodes.Count > 0 AndAlso
                MessageBox.Show("¿Desea copiar toda la cadena de reglas?", "Edición de Reglas",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                copyChildRules = True
            End If

            WFRulesBusiness.Copy(tvwRules.SelectedNode, copyChildRules)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            MessageBox.Show("Error al copiar reglas", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CortarRegla_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuCortarRegla.Click
        CortarRegla()
    End Sub
    Private Sub CortarRegla()
        Try
            WFRulesBusiness.CuttedRuleNode = DirectCast(tvwRules.SelectedNode, RuleNode)
            WFRulesBusiness.CuttedRuleNode.RuleType = TypesofRules.Regla
            WFRulesBusiness.Cut(DirectCast(tvwRules.SelectedNode, RuleNode))

            ' Si el usuario hizo click en copiar regla, y luego en cortar regla entonces el copiar regla se cancela
            WFRulesBusiness.CopiedRuleNode = Nothing

            '(pablo) guardo el log al cortar la regla
            UserBusiness.Rights.SaveAction(WFRulesBusiness.CuttedRuleNode.RuleId, ObjectTypes.WFRules, RightsType.Edit, "Se cortó regla: '" + WFRulesBusiness.CuttedRuleNode.Text + "' en WorkFlow '" + tvwRules.SelectedNode.FullPath.Split("\")(0) + "'")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            MessageBox.Show("Error al cortar reglas", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PegarRegla_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuPegarRegla.Click
        PegarRegla()
    End Sub
    Private Sub PegarRegla()
        Try
            If Not (IsNothing(WFRulesBusiness.CopiedRuleNode)) Then
                Paste(WFRulesBusiness.CopiedRuleNode, True)
            ElseIf Not (IsNothing(WFRulesBusiness.CuttedRuleNode)) Then
                Paste(WFRulesBusiness.CuttedRuleNode, False)
            End If

            'Se verifica que tipo de nodo es antes de castearlo. 
            'Se cuenta el último porque si estan en paralelo siempre será el ultimo ubicado.
            'NOTA: LA ACTUALIZACION NO FUNCIONA EN CASO DE PEGARSE SOBRE UNA ACCION DE USUARIO EXISTENTE
            'O SOBRE EVENTOS YA QUE EL SELECTED NODE EN EL CASO DE PEGARLO SOBRE UNA ACCION DE USUARIO
            'ES LA ACCION ANTERIOR Y NO LA NUEVA Y EN EL CASO DE LOS EVENTOS LE FALTA CONOCER SOBRE
            'QUE EVENTO DEBE BUSCAR LA REGLA QUE ACABA DE PEGARSE.
            If TypeOf tvwRules.SelectedNode.LastNode Is RuleNode Then
                Dim tempRuleNode As RuleNode = tvwRules.SelectedNode

                UserBusiness.Rights.SaveAction(tempRuleNode.RuleId,
                                               ObjectTypes.WFRules,
                                               RightsType.Edit,
                                               "Se pegó regla: '" + tempRuleNode.RuleName +
                                               "' en WorkFlow '" + tvwRules.SelectedNode.FullPath.Split("\")(0) + "'")

                UpdateIconNodes(tempRuleNode, False)
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            MessageBox.Show("Error al pegar reglas", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            WFRulesBusiness.CopiedRuleNode = Nothing
            WFRulesBusiness.CuttedRuleNode = Nothing
        End Try
    End Sub
    Private Sub Paste(ByRef node As RuleNode, ByVal isCopyNode As Boolean)
        'Hay veces que la llamada al método AttachRule tarda en agregar las reglas por eso se cambia el cursor
        Dim cur As Cursor = Cursor
        Cursor = Cursors.WaitCursor

        Dim aux As Boolean = False
        Dim selectedWfNode As BaseWFNode = DirectCast(tvwRules.SelectedNode, BaseWFNode)
        Dim ruleType As TypesofRules = WFRulesBusiness.GetRuleParentType(selectedWfNode)

        If TypeOf selectedWfNode Is RuleTypeNode Then
            If ruleType = TypesofRules.Eventos Then
                Dim forms As New WFUserControl.frmchooseevent
                forms.ShowDialog()
                node.RuleType = DirectCast(forms.Selected, TypesofRules)
                aux = True
            End If
        End If

        If Not aux Then
            ' Si el usuario decidio cortar una regla ubicada adentro de Eventos de Zamba y decide 
            ' pegarla en otro sector, entonces el ruleType de la regla debe volver a Regla
            If Not isCopyNode AndAlso node.RuleType <> TypesofRules.Regla Then
                node.RuleType = TypesofRules.Regla
            End If
        End If

        If Not node Is selectedWfNode Then
            WFRulesBusiness.PASTE(node, selectedWfNode, isCopyNode)
            tvwRules.SelectedNode = node

            Dim category As Int64
            '[German]:Si la regla padre es Accion de Usuario o Eventos su categoria sera 1 de lo contrario 2
            If node.ParentType = TypesofRules.AccionUsuario OrElse node.ParentType = TypesofRules.AbrirDocumento OrElse node.ParentType = TypesofRules.AbrirZamba OrElse node.ParentType = TypesofRules.Entrada OrElse String.Equals(node.RuleClass, "DoDesign") Then
                category = 1
            Else
                category = 2
            End If


            WFFactory.SetRulesPreferences(node.RuleId, RuleSectionOptions.Configuracion, RulePreferences.RuleCategory, category)
        End If

        Cursor = cur
    End Sub

    Private Sub MnuChangeName_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuCambiarNombre.Click
        ChangeName()
    End Sub
    Private Sub ChangeName()
        If tvwRules.SelectedNode IsNot Nothing AndAlso TypeOf (tvwRules.SelectedNode) Is BaseWFNode Then
            Dim selected As BaseWFNode
            Try
                selected = DirectCast(tvwRules.SelectedNode, BaseWFNode)
                Select Case selected.NodeWFType
                    Case NodeWFTypes.Regla
                        RuleNamesHelper.ChangeRuleName(DirectCast(selected, IRuleNode))
                        UserBusiness.Rights.SaveAction(DirectCast(selected, RuleNode).RuleId,
                                                       ObjectTypes.WFRules, RightsType.Edit,
                                                       "Cambio en nombre de regla: '" + selected.Text +
                                                       "' en WorkFlow '" + selected.FullPath.Split("\")(0) + "'")

                    Case NodeWFTypes.TipoDeRegla
                        Dim tempNode As RuleTypeNode = DirectCast(selected, RuleTypeNode)
                        If tempNode.RuleParentType = TypesofRules.AccionUsuario Then
                            RuleNamesHelper.ChangeUserActionName(tempNode)
                            UserBusiness.Rights.SaveAction(DirectCast(selected.FirstNode, RuleNode).RuleId,
                                                           ObjectTypes.WFRules, RightsType.Edit,
                                                           "Cambio en nombre acción de usuario: '" + selected.Text +
                                                           " - " + DirectCast(selected.FirstNode, RuleNode).RuleName +
                                                           "' en WorkFlow '" + selected.FullPath.Split("\")(0) + "'")
                        End If
                        tempNode = Nothing

                End Select
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
                MessageBox.Show("Error al modificar el nombre", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                selected = Nothing
            End Try
        End If
    End Sub

    Private Sub mnuAgregarAccionUsuario_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuAgregarAccionUsuario.Click
        AddUserAction()
    End Sub
    Private Sub AddUserAction()
        Try
            'Se agrega un nodo de Acción de usuario en la etapa seleccionada
            Dim base As BaseWFNode = DirectCast(tvwRules.SelectedNode, BaseWFNode)
            If base.NodeWFType = NodeWFTypes.Etapa OrElse base.NodeWFType = NodeWFTypes.Estado Then
                WFRulesBusiness.AddUserAction(DirectCast(tvwRules.SelectedNode, EditStepNode).WFStep.ID, DirectCast(tvwRules.SelectedNode, EditStepNode))

                'Busca el primer nodo de Acción de usuario que no tenga reglas
                For Each node As RadTreeNode In tvwRules.SelectedNode.Nodes
                    If String.Compare(node.Text, "Acción de Usuario") = 0 And node.Nodes.Count = 0 Then
                        'Selecciono el nodo de Acción de usuario
                        tvwRules.SelectedNode = tvwRules.SelectedNode.Nodes(node.Index)

                        'Agrego la regla al nodo
                        AddRule()

                        'Si se agrego la regla, entonces muestro la opción de cambiarle el nombre a la acción de usuario
                        If node.Nodes.Count > 0 Then
                            RuleNamesHelper.ChangeUserActionName(DirectCast(node, RuleTypeNode))
                        End If

                        '(pablo) guardo el log de la creacion de la accion de usuario
                        Dim tempRuleNode As RuleNode = DirectCast(tvwRules.SelectedNode, RuleTypeNode).LastNode
                        tvwRules.SelectedNode = tvwRules.SelectedNode.Nodes(tempRuleNode.Index)
                        UserBusiness.Rights.SaveAction(tempRuleNode.RuleId,
                                                       ObjectTypes.WFRules,
                                                       RightsType.Create,
                                                       "Se creó acción de usuario: '" + node.Text + " - " + tempRuleNode.RuleName + "' en WorkFlow '" + base.Parent.Text + "'")
                        Exit For
                    End If
                Next
            End If
        Catch ex As InvalidCastException
            Zamba.Core.ZClass.raiseerror(ex)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub MnuInicial_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuInicial.Click
        Try
            WFBusiness.SetInitialStep(DirectCast(tvwRules.SelectedNode, IEditStepNode))
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuAgregarRegla.Click
        AddRule()
    End Sub
    Private Sub AddRule()
        Dim newBaseWFNode As BaseWFNode = DirectCast(tvwRules.SelectedNode, BaseWFNode)
        Dim isAntiUI As Boolean = False
        Dim tmpParentRuleNode As IRuleNode
        Dim tmpWFParentRuleNode As IRuleNode

        Try
            If newBaseWFNode.NodeWFType = NodeWFTypes.Regla Then
                If DirectCast(newBaseWFNode, RuleNode).RuleClass.ToUpper = "DOWAITFORDOCUMENT" Then
                    isAntiUI = True
                ElseIf Not IsNothing(DirectCast(newBaseWFNode, RuleNode).ParentNode) Then
                    tmpParentRuleNode = DirectCast(newBaseWFNode, RuleNode).ParentNode
                    If tmpParentRuleNode.RuleType = TypesofRules.Regla Then
                        tmpWFParentRuleNode = DirectCast(tmpParentRuleNode, RuleNode)

                        While Not IsNothing(tmpWFParentRuleNode)
                            If tmpWFParentRuleNode.RuleClass.ToUpper = "DOWAITFORDOCUMENT" Then
                                isAntiUI = True
                            End If
                            If Not IsNothing(tmpWFParentRuleNode.ParentNode) Then
                                tmpParentRuleNode = tmpWFParentRuleNode.ParentNode
                                tmpWFParentRuleNode = DirectCast(tmpParentRuleNode, IWFRuleParent)
                            Else
                                tmpWFParentRuleNode = Nothing
                            End If
                        End While
                    End If
                End If
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Dim newAddNewRule As AddNewRuleFuturo = Nothing
        Try
            newAddNewRule = New AddNewRuleFuturo(newBaseWFNode, _IL, isAntiUI)
            newAddNewRule.ShowDialog()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            If newAddNewRule IsNot Nothing Then
                newAddNewRule.Dispose()
                newAddNewRule = Nothing
            End If
        End Try
    End Sub

    Private Sub MenuItem7_Click(sender As Object, e As EventArgs) Handles mnuAddProcess.Click
        AddProcess()
    End Sub
    Private Sub AddProcess()
        Dim newBaseWFNode As BaseWFNode = DirectCast(tvwRules.SelectedNode, BaseWFNode)
        Dim isAntiUI As Boolean = False
        Dim tmpParentRule As IRule
        Dim tmpWFParentRule As IWFRuleParent
        Dim StepId As Int64

        Try
            If newBaseWFNode.NodeWFType = NodeWFTypes.Regla Then
                StepId = DirectCast(newBaseWFNode, RuleNode).WFStepId
                If DirectCast(newBaseWFNode, RuleNode).RuleClass.ToUpper = "DOWAITFORDOCUMENT" Then
                    isAntiUI = True
                ElseIf Not IsNothing(DirectCast(newBaseWFNode, RuleNode).ParentNode) Then
                    tmpParentRule = DirectCast(newBaseWFNode, RuleNode).ParentNode
                    If tmpParentRule.RuleType = TypesofRules.Regla Then
                        tmpWFParentRule = DirectCast(tmpParentRule, IWFRuleParent)
                        While Not IsNothing(tmpWFParentRule)
                            If tmpWFParentRule.RuleClass.ToUpper = "DOWAITFORDOCUMENT" Then
                                isAntiUI = True
                            End If
                            If Not IsNothing(tmpWFParentRule.ParentRule) Then
                                tmpParentRule = tmpWFParentRule.ParentRule
                                tmpWFParentRule = DirectCast(tmpParentRule, IWFRuleParent)
                            Else
                                tmpWFParentRule = Nothing
                            End If
                        End While
                    End If
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Dim newAddNewProcess As AddNewProcessFuturo = Nothing
        Try
            newAddNewProcess = New AddNewProcessFuturo(newBaseWFNode, _IL, StepId, isAntiUI)
            newAddNewProcess.ShowDialog()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            If newAddNewProcess IsNot Nothing Then
                newAddNewProcess.Dispose()
                newAddNewProcess = Nothing
            End If
        End Try
    End Sub

    Private Sub mnuExpadirRegla_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuExpadirRegla.Click
        tvwRules.SelectedNode.ExpandAll()
    End Sub

    Private Sub mnuCategoriaRegla_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuCategoriaRegla.Click
        Dim frm As Form = Nothing
        Dim cat As UCCategory = Nothing

        Try
            frm = New Form
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.ShowIcon = False
            frm.Text = tvwRules.SelectedNode.Name
            frm.AutoScroll = True
            frm.AutoSize = True
            frm.AutoSizeMode = AutoSizeMode.GrowOnly

            cat = New UCCategory(DirectCast(tvwRules.SelectedNode, IRuleNode))
            frm.Controls.Add(cat)
            frm.Height = cat.Height + 30
            frm.Width = cat.Width + 25
            frm.MaximizeBox = False

            frm.ShowDialog()
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Error al configurar la categoria", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cat IsNot Nothing Then
                cat.Dispose()
                cat = Nothing
            End If
            If frm IsNot Nothing Then
                frm.Dispose()
                frm = Nothing
            End If
        End Try
    End Sub
#End Region

#Region "TreeView Select"

    Private Sub TreeViewElement_NodeFormatting(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.TreeNodeFormattingEventArgs) Handles tvwRules.NodeFormatting
        Dim Node As IBaseWFNode = DirectCast(args.Node, IBaseWFNode)
        Dim NodeType As NodeWFTypes = Node.NodeWFType
        Dim RuleNodeType As TypesofRules
        If NodeType = NodeWFTypes.TipoDeRegla Then
            RuleNodeType = DirectCast(Node, IRuleTypeNode).RuleParentType
        End If
        Dim NodeHelper As New WFNodeHelper
        Dim currentImage As Image = NodeHelper.GetnodeImage(NodeType, RuleNodeType)

        args.NodeElement.ImageElement.Image = currentImage
        args.NodeElement.ClipDrawing = False
        args.NodeElement.ImageElement.FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentBounds
        args.NodeElement.ContentElement.DisableHTMLRendering = True
        args.NodeElement.ImageElement.StretchHorizontally = False
        args.NodeElement.ImageElement.ImageLayout = ImageLayout.Stretch
        args.NodeElement.ImageElement.MinSize = New Size(20, 20)

        If NodeType = NodeWFTypes.nodoBusqueda Then
            args.NodeElement.ContentElement.ForeColor = Zamba.AppBlock.ZambaUIHelpers.GetTitlesColor
        ElseIf NodeType = NodeWFTypes.nodoInsercion Then
            args.NodeElement.ContentElement.ForeColor = Zamba.AppBlock.ZambaUIHelpers.GetTitlesColor
        Else
            args.NodeElement.ContentElement.ForeColor = Color.Black
        End If

        If args.NodeElement.IsSelected Then
            args.NodeElement.BorderColor = Color.DarkOrange
            args.NodeElement.BorderBoxStyle = Telerik.WinControls.BorderBoxStyle.SingleBorder
            args.NodeElement.BorderGradientStyle = Telerik.WinControls.GradientStyles.Solid
            args.NodeElement.BackColor = Zamba.AppBlock.ZambaUIHelpers.GetTitlesColor
            args.NodeElement.BackColor2 = Zamba.AppBlock.ZambaUIHelpers.GetTitlesColor
            args.NodeElement.BackColor3 = Zamba.AppBlock.ZambaUIHelpers.GetTitlesColor
            args.NodeElement.BackColor4 = Zamba.AppBlock.ZambaUIHelpers.GetTitlesColor
            args.NodeElement.ContentElement.ForeColor = Color.White
        Else
            args.NodeElement.BorderColor = Color.White
            args.NodeElement.BorderBoxStyle = Telerik.WinControls.BorderBoxStyle.FourBorders
            args.NodeElement.BorderGradientStyle = Telerik.WinControls.GradientStyles.Linear
            args.NodeElement.BackColor = Color.White
            args.NodeElement.BackColor2 = Color.White
            args.NodeElement.BackColor3 = Color.White
            args.NodeElement.BackColor4 = Color.White
            args.NodeElement.ContentElement.ForeColor = Color.Black

        End If

        Try

            args.NodeElement.ClipDrawing = False
            Dim element As RadElement = args.NodeElement.Children(0)

            Dim imageElement As LightVisualElement = Nothing

            If element.Name <> "StateImage" Then
                imageElement = New LightVisualElement()
                imageElement.Name = "StateImage"
                imageElement.StretchHorizontally = False
                imageElement.ImageLayout = ImageLayout.Stretch
                imageElement.MinSize = New Size(25, 20)
                imageElement.MaxSize = New Size(25, 0)
                imageElement.DrawBorder = True
                imageElement.DrawFill = True
                imageElement.GradientStyle = GradientStyles.Solid
                imageElement.GradientAngle = 0
                imageElement.NumberOfColors = 2
                imageElement.BorderBoxStyle = BorderBoxStyle.FourBorders

                imageElement.BorderRightWidth = 4
                imageElement.FitToSizeMode = RadFitToSizeMode.FitToParentBounds
                args.NodeElement.Children.Insert(0, imageElement)

                imageElement.BackColor = Color.LightGray
                imageElement.BackColor2 = Color.LightGray
                imageElement.BorderRightColor = Color.FromArgb(180, 184, 191)
                imageElement.BorderRightShadowColor = Color.FromArgb(241, 241, 241)
                imageElement.BorderLeftWidth = 0
                imageElement.BorderTopWidth = 0
                imageElement.BorderBottomWidth = 0
            Else
                imageElement = CType(args.NodeElement.Children(0), LightVisualElement)
            End If

            If args.Node.Checked Then
                CType(args.NodeElement.Children(0), LightVisualElement).Image = My.Resources.appbar_location_circle
            Else
                CType(args.NodeElement.Children(0), LightVisualElement).Image = Nothing
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
    Private Sub TreeView1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvwRules.MouseDown
        If e.Button = MouseButtons.Right Then
            Try
                Dim node As RadTreeNode = tvwRules.GetNodeAt(e.X, e.Y)
                If IsNothing(node) = False Then
                    tvwRules.SelectedNode = node
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        ElseIf e.X < 25 Then
            Try
                Dim node As RadTreeNode = Me.tvwRules.GetNodeAt(e.X, e.Y)
                If Not IsNothing(node) Then
                    If TypeOf (node) Is BaseWFNode AndAlso DirectCast(node, BaseWFNode).NodeWFType = NodeWFTypes.Regla Then
                        If Not node.Checked Then
                            node.Checked = True
                        Else
                            node.Checked = False
                        End If
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If
    End Sub
    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As RadTreeViewEventArgs) Handles tvwRules.SelectedNodeChanged
        TreeViewSelect()
    End Sub
#Region "Eventos"
    Public Event WFSelected(ByVal WF As Core.WorkFlow)
    Public Event StepSelected(ByRef wfstep As WFStep)
    Public Event TypesofRuleselected(ByVal stepid As Int64, ByVal RuleParentType As TypesofRules, ByVal ruleIds As Generic.List(Of Int64))
    Public Event RuleSelected(ByRef ruleid As Int64)
    Public Event RightSelected(ByRef wfstep As WFStep)
    Public Event TreeViewSelected()
    Public Event LoadWokflow(ByVal workflowId As Int64, ByVal ruleId As Int64)
#End Region

    Dim LastRulesList As New List(Of BaseWFNode)
    Private Sub TreeViewSelect()
        Try
            If IsNothing(tvwRules.SelectedNode) = False Then
                RaiseEvent TreeViewSelected()
                If TypeOf (tvwRules.SelectedNode) Is BaseWFNode Then
                    Dim BaseWFNode As BaseWFNode = DirectCast(tvwRules.SelectedNode, BaseWFNode)


                    If Navigating = False Then
                        RemoveHandler cbLastRulesList.SelectedIndexChanged, AddressOf cbLastRulesList_SelectedIndexChanged
                        LastRulesList.Add(BaseWFNode)
                        cbLastRulesList.DataSource = Nothing
                        cbLastRulesList.DataSource = LastRulesList
                        cbLastRulesList.DisplayMember = "Text"
                        cbLastRulesList.SelectedIndex = cbLastRulesList.Items.Count - 1
                        cbLastRulesList.SelectedText = BaseWFNode.Text
                        AddHandler cbLastRulesList.SelectedIndexChanged, AddressOf cbLastRulesList_SelectedIndexChanged
                    End If

                    Select Case BaseWFNode.NodeWFType

                        Case NodeWFTypes.WorkFlow
                            RaiseEvent WFSelected(DirectCast(DirectCast(BaseWFNode, WFNode).WorkFlow, Core.WorkFlow))

                        Case NodeWFTypes.Etapa, NodeWFTypes.Estado
                            RaiseEvent StepSelected(DirectCast(DirectCast(BaseWFNode, EditStepNode).WFStep, WFStep))

                        Case NodeWFTypes.TipoDeRegla

                            Dim ruleIds As New Generic.List(Of Int64)
                            Dim ruleType As TypesofRules = DirectCast(BaseWFNode, RuleTypeNode).RuleParentType

                            If tvwRules.SelectedNode.Nodes.Count > 0 Then
                                'Si el seleccionado es un evento o regla general, no carga las opciones de configuración
                                If ruleType = TypesofRules.Floating OrElse ruleType = TypesofRules.Eventos Then
                                    ruleIds.Add(-1)

                                Else
                                    ruleIds.Add(DirectCast(BaseWFNode.Nodes(0), RuleNode).RuleId)
                                End If

                            Else
                                'Si el seleccionado no tiene reglas no carga las opciones de configuración
                                ruleIds.Add(-1)
                            End If

                            If tvwRules.SelectedNode.Tag IsNot Nothing AndAlso tvwRules.SelectedNode.Tag.ToString.Equals("Breakpoint") Then
                                tvwRules.SelectedNode.Tag = Nothing
                            Else
                                tvwRules.SelectedNode.Tag = "Breakpoint"
                            End If

                            RaiseEvent TypesofRuleselected(DirectCast(BaseWFNode, RuleTypeNode).StepId, ruleType, ruleIds)
                            ruleType = Nothing
                            ruleIds = Nothing


                        Case NodeWFTypes.Regla
                            RaiseEvent RuleSelected(DirectCast(BaseWFNode, RuleNode).RuleId)

                        Case NodeWFTypes.Comienzo
                            RaiseEvent RuleSelected(DirectCast(BaseWFNode, RuleNode).RuleId)

                        Case NodeWFTypes.Permiso
                            RaiseEvent RightSelected(DirectCast(DirectCast(BaseWFNode.Parent, EditStepNode).WFStep, WFStep))
                            '[Sebastian 17-04-09] se agrego para que muestre la configuracion de las reglas
                            'que fueron cortadas

                        Case NodeWFTypes.FloatingRule
                            RaiseEvent RuleSelected(DirectCast(BaseWFNode, RuleNode).RuleId)
                    End Select
                ElseIf tvwRules.SelectedNode.Parent IsNot Nothing AndAlso DirectCast(DirectCast(Me.tvwRules.SelectedNode.Parent, BaseWFNode), RuleTypeNode).RuleParentType = TypesofRules.Eventos Then
                    'Este es el caso cuando se selecciona un tipo de evento. 

                    Dim ruleIds As New Generic.List(Of Int64)

                    Dim stepid As Int64
                    'Un tipo de evento puede tener varias reglas en un mismo nivel.
                    For Each node As BaseWFNode In tvwRules.SelectedNode.Nodes
                        'ruleIds.Add(DirectCast(DirectCast(node, RuleNode).Rule, WFRuleParent).ID)
                        ruleIds.Add(DirectCast(node, RuleNode).RuleId)
                        stepid = DirectCast(node, RuleNode).WFStepId
                    Next

                    If ruleIds.Count > 0 Then
                        RaiseEvent TypesofRuleselected(stepid, TypesofRules.Eventos, ruleIds)
                        ruleIds.Clear()
                    End If

                    ruleIds = Nothing
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "TreeView Tools"
    Private Sub btnPrintPreview_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnPrintPreview.Click
        PrintTree(True)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnPrint.Click
        PrintTree(False)
    End Sub

    Private Sub btnExportBMP_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExportBMP.Click
        ExportTree()
    End Sub

    Private Sub btnChecks_Click(sender As Object, e As EventArgs) Handles btnChecks.Click
        tvwRules.CheckBoxes = Not tvwRules.CheckBoxes
    End Sub

    Private Sub PrintTree(ByVal preview As Boolean)
        Try
            If MessageBox.Show("¿Desea expandir el arbol para poder imprimirlo completo?", "Expandir arbol", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                tvwRules.ExpandAll()
            End If

            LoadTreeViewHelper()

            If preview Then
                _treeViewHelper.PrintPreviewTree(tvwRules, tvwRules.Nodes(0).Text)
            Else
                _treeViewHelper.PrintTree(tvwRules, tvwRules.Nodes(0).Text)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Ha ocurrido un error al querer imprimir el árbol de workflow", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ExportTree()
        Try
            Using sd As New SaveFileDialog
                sd.DefaultExt = ".bmp"
                sd.Filter = "Imagen (*.bmp)|*.bmp"
                sd.CheckPathExists = True
                sd.FileName = tvwRules.Nodes(0).Text & ".bmp"

                If sd.ShowDialog = DialogResult.OK Then
                    If MessageBox.Show("¿Desea expandir el arbol para poder exportarlo completo?", "Expandir arbol", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                        tvwRules.ExpandAll()
                    End If

                    LoadTreeViewHelper()
                    _treeViewHelper.SaveTreeBitmap(tvwRules, sd.FileName)
                End If
            End Using
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Ha ocurrido un error al exportar el árbol de workflow", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MenuItem2_Click(sender As System.Object, e As EventArgs) Handles mnuExport.Click
        Dim newBaseWFNode As BaseWFNode
        Try
            newBaseWFNode = DirectCast(tvwRules.SelectedNode, BaseWFNode)
            WFEditIDE.AddExport(newBaseWFNode)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            MessageBox.Show("Error en la exportación", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            newBaseWFNode = Nothing
        End Try
    End Sub

    Private Sub LoadTreeViewHelper()
        If _treeViewHelper Is Nothing Then
            _treeViewHelper = New PrintTreeView.PrintHelper()
        End If
    End Sub
#End Region

#Region "Simulación"
    Private Sub mnuAgregaraProcesoSim_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuAgregaraProcesoSim.Click
        AddNewSimulationItem(True)
    End Sub
    Private Sub mnuCrearSimNueva_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles mnuCrearSimNueva.Click
        AddNewSimulationItem(False)
    End Sub
    Private Sub AddNewSimulationItem(ByVal addProcess As Boolean)
        Dim simManager As UcSimulationManager
        Dim ruleID As Int64
        Dim ruleName As String
        Dim description As String

        Try
            simManager = GetSimulationManager()
            ruleID = DirectCast(tvwRules.SelectedNode, IRuleNode).RuleId
            ruleName = DirectCast(tvwRules.SelectedNode, IRuleNode).RuleName

            If addProcess Then
                simManager.AddTestCaseToCurrentSim(ruleID, ruleName)
            Else
                description = "Prueba individual para la regla " & ruleName
                simManager.AddNewSimulation(ruleID, ruleName, description)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)

            If addProcess Then
                MessageBox.Show("Error al crear un nuevo proceso", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("Error al crear una nueva simulación", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub
    Private Function GetSimulationManager() As UcSimulationManager
        Return DirectCast(DirectCast(Parent.Parent.Parent, SplitContainer).Panel2.Controls(0).Controls(0), TabControl).TabPages(1).Controls(0)
    End Function
#End Region

#Region "Refresco de íconos de nodos"
    Public Event updateruleicon(ByVal rule As IRule)
    Private Sub UpdateIconNodes(ByVal ruleNode As RuleNode, ByVal disableChilds As Boolean)
        If Not ruleNode.RuleEnabled OrElse disableChilds Then
            ruleNode.ImageIndex = Icons.Disable
            'ruleNode.SelectedImageIndex = Icons.Disable
        ElseIf ruleNode.RuleClass.Contains("Design") Then
            ruleNode.ImageIndex = Icons.Design
            'ruleNode.SelectedImageIndex = Icons.Design
        Else
            ruleNode.ImageIndex = ruleNode.IconId
            'ruleNode.SelectedImageIndex = ruleNode.IconId
        End If
    End Sub

    Private Sub RefreshRulesIcons(ByVal ruleNode As RuleNode)
        If ruleNode.RuleEnabled Then
            tvwRules.SelectedNode.ImageIndex = ruleNode.IconId
            'Me.tvwRules.SelectedNode.SelectedImageIndex = ruleNode.IconId
        Else
            tvwRules.SelectedNode.ImageIndex = Icons.Disable
            ' Me.tvwRules.SelectedNode.SelectedImageIndex = Icons.Disable
        End If

        If WFRulesBusiness.GetRulePreferencesByRuleId(ruleNode.RuleId, ruleNode.WFStepId, RulePreferences.DisableChildRules) Then
            RefreshNodeIcon(tvwRules.SelectedNode.Nodes, Icons.Disable)
        Else
            For Each childRule As RuleNode In ruleNode.Nodes
                RefreshRulesIcons(childRule)
            Next
        End If
    End Sub
    Private Sub RefreshNodeIcon(ByVal Nodes As RadTreeNodeCollection, ByVal ImageIndex As Integer)
        If Not Nodes Is Nothing Then
            For Each node As RadTreeNode In Nodes
                node.ImageIndex = ImageIndex
                'node.SelectedImageIndex = ImageIndex

                If Not node.Nodes Is Nothing Then
                    RefreshNodeIcon(node.Nodes, ImageIndex)
                End If
            Next
        End If
    End Sub
#End Region

#Region "Depurador de reglas"
    Delegate Sub TrackRuleToPlayDelegate(Rule As IWFRuleParent, Tasks As List(Of ITaskResult))
    Private PlayedRules As New Dictionary(Of Int64, Int64)
    Delegate Sub TrackRulePlayedDelegate(Rule As IWFRuleParent, Tasks As List(Of ITaskResult), ByVal WithErrors As Boolean)
    'Public Shared BreakPoints As New List(Of IRuleNode)

    Private Sub TreeView1_AfterCheck(sender As Object, e As RadTreeViewEventArgs) Handles tvwRules.NodeCheckedChanged
        Try
            If Not IsTreeViewLoading Then
                Dim checkednode As RadTreeNode = e.Node
                If TypeOf checkednode Is RuleNode Then
                    Dim ruleNode As RuleNode = DirectCast(checkednode, RuleNode)
                    'Select Case BaseWFNode.NodeWFType
                    '    Case NodeWFTypes.Regla
                    '        If checkednode.Checked Then
                    '            If BreakPoints.Contains(checkednode) = False Then
                    '                BreakPoints.Add(checkednode)
                    '            End If
                    '        Else
                    '            If BreakPoints.Contains(checkednode) Then
                    '                BreakPoints.Remove(checkednode)
                    '            End If
                    '        End If
                    '    Case NodeWFTypes.FloatingRule
                    '        If checkednode.Checked Then
                    '            If BreakPoints.Contains(checkednode) = False Then
                    '                BreakPoints.Add(checkednode)
                    '            End If
                    '        Else
                    '            If BreakPoints.Contains(checkednode) Then
                    '                BreakPoints.Remove(checkednode)
                    '            End If
                    '        End If
                    'End Select

                    'Dim query As String = String.Format("insert into zbreakpoints (ruleid, userid) values ({0}, {1})", ruleNode.RuleId, Membership.MembershipHelper.CurrentUser.ID)

                    Dim params() As Object = Nothing
                    If checkednode.Checked Then
                        WFFactory.SaveBreakPoint(ruleNode.RuleId, Membership.MembershipHelper.CurrentUser.ID, params)
                    Else
                        WFFactory.RemoveBreakPoint(WFFactory.GetBreakpointIDByRuleIdAndUserID(ruleNode.RuleId, Membership.MembershipHelper.CurrentUser.ID))
                    End If
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub TrackRuleToPlay(Rule As IWFRuleParent, Tasks As List(Of ITaskResult))
        Try

            Dim Finded As Boolean

            BuscarReglaPorID(tvwRules.Nodes, Finded, Rule.ID)
            tvwRules.SelectedNode.ImageIndex = 15
            If Finded Then
                Finded = False
                If tvwRules.SelectedNode.Checked Then
                    DirectCast(Parent.Parent.Parent.Parent.Parent.Parent, Zamba.Controls.WFEditIDE).LoadDebuggerData(Rule, Tasks)
                    UcSimulationExecution.pauseWorker = True
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub TrackRulePlayed(Rule As IWFRuleParent, Tasks As List(Of ITaskResult), ByVal WithErrors As Boolean)
        Try
            Dim Finded As Boolean
            Dim currentnode As TreeNode = BuscarReglaPorID(tvwRules.Nodes, Finded, Rule.ID)

            If Finded Then
                If PlayedRules.ContainsKey(Rule.ID) Then
                    PlayedRules(Rule.ID) = PlayedRules(Rule.ID) + 1
                Else
                    PlayedRules.Add(Rule.ID, 1)
                End If

                tvwRules.SelectedNode.Text = Rule.Name & " ( " & PlayedRules(Rule.ID) & " )"
                If WithErrors Then
                    tvwRules.SelectedNode.ImageIndex = 37
                Else
                    tvwRules.SelectedNode.ImageIndex = 22
                End If

            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub ResumePlayRule()
        Try
            UcSimulationExecution.pauseWorker = False
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Private Sub BtnContinue_Click(sender As Object, e As EventArgs)
        Try
            ResumePlayRule()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub RuleToExecute(ByVal Rule As WFRuleParent, ByVal Tasks As List(Of ITaskResult))
        Try
            Dim arr() As Object = {Rule, Tasks}
            Dim D1 As New TrackRuleToPlayDelegate(AddressOf TrackRuleToPlay)
            Invoke(D1, arr)
            If (UcSimulationExecution.pauseWorker) Then
                While (UcSimulationExecution.pauseWorker)
                    System.Threading.Thread.Sleep(1000)
                End While
            End If
        Catch ex2 As Threading.ThreadInterruptedException
        Catch ex2 As Threading.ThreadAbortException
        Catch ex2 As Threading.ThreadStartException
        Catch ex2 As Threading.ThreadStateException
        Catch ex2 As Exception
            ZClass.raiseerror(ex2)
        End Try
    End Sub
    Public Sub RuleExecuted(ByVal Rule As WFRuleParent, ByVal Tasks As List(Of ITaskResult))
        Try
            Dim arr() As Object = {Rule, Tasks, False}
            Dim D1 As New TrackRulePlayedDelegate(AddressOf TrackRulePlayed)
            Invoke(D1, arr)
            If (UcSimulationExecution.pauseWorker) Then
                While (UcSimulationExecution.pauseWorker)
                    System.Threading.Thread.Sleep(1000)
                End While
            End If
        Catch ex2 As Threading.ThreadInterruptedException
        Catch ex2 As Threading.ThreadAbortException
        Catch ex2 As Threading.ThreadStartException
        Catch ex2 As Threading.ThreadStateException
        Catch ex2 As Exception
            ZClass.raiseerror(ex2)
        End Try
    End Sub
    Public Sub RuleExecutedError(ByVal Rule As WFRuleParent, ByVal Tasks As List(Of ITaskResult), ByVal ex As Exception, ByRef ErrorBreakPoint As Boolean)
        Try
            Dim arr() As Object = {Rule, Tasks, True}
            Dim D1 As New TrackRulePlayedDelegate(AddressOf TrackRulePlayed)
            Invoke(D1, arr)
            If (UcSimulationExecution.pauseWorker) Then
                While (UcSimulationExecution.pauseWorker)
                    System.Threading.Thread.Sleep(1000)
                End While
            End If
        Catch ex2 As Threading.ThreadInterruptedException
        Catch ex2 As Threading.ThreadAbortException
        Catch ex2 As Threading.ThreadStartException
        Catch ex2 As Threading.ThreadStateException
        Catch ex2 As Exception
            ZClass.raiseerror(ex2)
        End Try
    End Sub
#End Region

#Region "Busqueda de reglas"
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        DShowSearchForm.DynamicInvoke(New Object() {WF.ID})
    End Sub

    Private Function BuscarReglaPorID(ByRef nodos As RadTreeNodeCollection, ByRef encontrado As Boolean, ByVal searchID As Int64)
        If Not encontrado Then
            For Each nodo As RadTreeNode In nodos
                If TypeOf (nodo) Is RuleNode AndAlso DirectCast(nodo, RuleNode).RuleId = searchID Then
                    nodo.Expand()
                    nodo.EnsureVisible()
                    tvwRules.SelectedNode = nodo
                    tvwRules.SelectedNode.EnsureVisible()
                    encontrado = True
                Else
                    BuscarReglaPorID(nodo.Nodes, encontrado, searchID)
                End If
                If encontrado Then
                    Exit Function
                End If
            Next
        End If
    End Function

    Private Function BuscarReglaPorNombre(ByRef nodos As RadTreeNodeCollection, ByRef encontrado As Boolean, ByVal searchName As String)
        If Not encontrado Then
            For Each nodo As RadTreeNode In nodos
                If TypeOf (nodo) Is RuleNode AndAlso DirectCast(nodo, RuleNode).RuleName.ToLower().Contains(searchName) Then
                    nodo.Expand()
                    nodo.EnsureVisible()
                    tvwRules.SelectedNode = nodo
                    encontrado = True
                Else
                    BuscarReglaPorNombre(nodo.Nodes, encontrado, searchName)
                End If
                If encontrado Then
                    Exit Function
                End If
            Next
        End If
    End Function

    Public Sub OpenRule(ByVal ruleId As Int64)
        If ruleId > 0 Then
            Dim encontrado As Boolean = False
            BuscarReglaPorID(tvwRules.Nodes, encontrado, ruleId)

            If Not encontrado Then
                MessageBox.Show("No se ha encontrado la regla", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Public Sub OpenMissedRule(ByVal workflowId As Int64, ByVal ruleId As Int64) Implements IWFPanelCircuit.OpenMissedRule
        RaiseEvent LoadWokflow(workflowId, ruleId)
    End Sub
#End Region

#Region "Treeview Keyboard Shortcuts"
    Private Sub TreeView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tvwRules.KeyDown
        'Verifica que sea un objeto con opciones
        If TypeOf (tvwRules.SelectedNode) Is BaseWFNode Then

            'La llamada a este método modifica la visibilidad de las acciones disponibles para el atajo accionado.
            'Puede que algunos atajos no puedan ser invocados en determinados momentos o sobre algunos tipos de nodos.
            EditContextMenu()

            If e.Control Then
                Select Case e.KeyCode.ToString
                    Case "C"
                        If mnuCopiarRegla.Visible Then CopiarRegla()
                    Case "X", "Delete"
                        If mnuCortarRegla.Visible Then CortarRegla()
                    Case "V", "Insert"
                        If mnuPegarRegla.Visible Then PegarRegla()
                    Case "N"
                        If mnuAgregarRegla.Visible Then
                            AddRule()
                        ElseIf mnuAgregarAccionUsuario.Visible Then
                            AddUserAction()
                        End If
                End Select
            Else
                Select Case e.KeyCode.ToString
                    Case "Delete"
                        If mnuEliminar.Visible Then Delete()
                    Case "F2"
                        If mnuCambiarNombre.Visible Then ChangeName()
                End Select
            End If

        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If cbLastRulesList.SelectedIndex > 0 Then
                cbLastRulesList.SelectedIndex = cbLastRulesList.SelectedIndex - 1
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub btnFoward_Click(sender As Object, e As EventArgs) Handles btnFoward.Click
        Try
            If cbLastRulesList.SelectedIndex < cbLastRulesList.Items.Count Then
                cbLastRulesList.SelectedIndex = cbLastRulesList.SelectedItem.Index + 1
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Sub cbLastRulesList_SelectedIndexChanged(sender As Object, e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cbLastRulesList.SelectedIndexChanged
        Try
            Navigating = True
            If LastRulesList.Count > 0 AndAlso cbLastRulesList.SelectedIndex <> -1 Then
                Dim Node As BaseWFNode = LastRulesList(cbLastRulesList.SelectedIndex)
                tvwRules.SelectedNode = Node
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Navigating = False
        End Try
    End Sub
#End Region

End Class
