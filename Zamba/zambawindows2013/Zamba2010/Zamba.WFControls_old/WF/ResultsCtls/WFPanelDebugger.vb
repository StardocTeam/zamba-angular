Imports Zamba.Core.Enumerators
Imports Zamba.Core
'Imports Zamba.WFBusiness
Imports Telerik.WinControls.UI
Imports System.Reflection

Public Class WFPanelDebugger
    Inherits ZControl
    Implements IWFPanelCircuit
    Implements IIfWFPanelCircuit

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
    ''' <summary>
    ''' Actualiza para la regla do design el nodo cuando se convierte la regla.
    ''' </summary>
    ''' <history>
    ''' [Sebastian] 21-10-2009 CREATED
    ''' </history>
    ''' <remarks></remarks>
    Public Sub UpdateRuleType() Implements IWFPanelCircuit.UpdateRuleType
        Dim _rulenode As RuleNode = DirectCast(Me.TreeView1.SelectedNode, RuleNode)
        RaiseEvent RuleSelected(_rulenode.RuleId)
    End Sub
    Public Sub UpdateRuleTypeIfdesign() Implements IIfWFPanelCircuit.UpdateRuleTypeIfDesign
        Dim _rulenode As RuleNode = DirectCast(Me.TreeView1.SelectedNode, RuleNode)
        RaiseEvent RuleSelected(_rulenode.RuleId)
    End Sub
    'Form overrides dispose to clean up the component list.
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
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TreeViewContextMenu As ContextMenu
    Private WithEvents TreeView1 As RadTreeView
    Friend WithEvents mnuExpadirRegla As System.Windows.Forms.MenuItem
    Friend WithEvents panelToolbar As System.Windows.Forms.Panel
    Friend WithEvents Toolbar As ZToolBar
    Friend WithEvents btnPrintPreview As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnExportBMP As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlTreeView As ZPanel
    Public WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents pnlDetails As ZPanel
    Friend WithEvents FlowPanel As FlowLayoutPanel
    Friend WithEvents txtRuleId As ToolStripTextBox

    Public ReadOnly Property TreeView() As RadTreeView
        Get
            Return TreeView1
        End Get
    End Property

    Public Sub SetResult(rule As WFRuleParent, stateRule As Boolean)
        Try
            Dim NodeName As String = rule.Name & " (" & rule.ID.ToString() & " - " & rule.RuleClass & ")"
            Dim ActualNodes As List(Of RadTreeNode) = BuscarNodoPorNombre(Me.TreeView1.Nodes, NodeName)
            If ActualNodes IsNot Nothing AndAlso ActualNodes.Count > 0 Then
                Dim nodoActual As RadTreeNode = ActualNodes.LastOrDefault()
                If stateRule Then nodoActual.Checked = True
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Dim countNodes As Integer = -1
    Dim lastRuleInsert As WFRuleParent
    Public Sub AddNewRule(rule As WFRuleParent, stateRule As Boolean, Task As ITaskResult)
        Try
            Dim nodoActual As RadTreeNode
            Dim NewNode As RadTreeNode
            Dim lastNodeInserted As RadTreeNode
            If Not IsNothing(rule) Then

                'If IsNothing(rule.ParentRule) AndAlso IsNothing(lastRuleInsert) Then 'Asumo que es el primer nodo de todos
                '    Dim WFNameNode As RadTreeNode = New RadTreeNode("Proceso: " & WFBusiness.GetWorkflowByStepId(rule.WFStepId).Name & " | Etapa: " & WFStepBusiness.GetStepNameById(rule.WFStepId))
                '    WFNameNode.Font = New Font("Tahoma", 11.0!, FontStyle.Bold, GraphicsUnit.Point, 0)
                '    WFNameNode.CheckType = CheckType.None
                '    WFNameNode.ForeColor = Color.Black
                '    TreeView1.Nodes.Add(WFNameNode)
                'End If

                If IsNothing(rule.ParentRule) Then

                    'Dim infoNode As RadTreeNode = New RadTreeNode("Provocado por: " & rule.ParentType.ToString())
                    Dim infoNode As RadTreeNode = New RadTreeNode(rule.ParentType.ToString())
                    infoNode.Font = New Font("Tahoma", 8.0!, FontStyle.Bold, GraphicsUnit.Point, 0)
                    infoNode.CheckType = CheckType.None

                    If Not IsNothing(lastRuleInsert) AndAlso (lastRuleInsert.RuleClass = "DOExecuteRule" OrElse rule.ParentType = TypesofRules.Entrada) Then

                        Dim ParentNodeName As String = lastRuleInsert.Name & " (" & lastRuleInsert.ID.ToString() & " - " & lastRuleInsert.RuleClass & ")"

                        Dim nodosEncontrados As List(Of RadTreeNode) = BuscarNodoPorNombre(Me.TreeView1.Nodes, ParentNodeName)

                        nodoActual = nodosEncontrados.LastOrDefault()

                        If nodoActual IsNot Nothing Then
                            nodoActual.Nodes.Add(infoNode)
                            NewNode = New RadTreeNode(rule.Name & " (" & rule.ID.ToString() & " - " & rule.RuleClass & ")", SelectedImage(rule.RuleType))
                            NewNode.Tag = New Object() {rule, Task}
                            nodoActual.Nodes.Add(NewNode)
                            lastNodeInserted = NewNode
                        End If

                        'ElseIf Not IsNothing(lastRuleInsert) AndAlso rule.ParentType = TypesofRules.AbrirDocumento Then
                    Else

                        TreeView1.Nodes.Add(infoNode)
                        NewNode = New RadTreeNode(rule.Name & " (" & rule.ID.ToString() & " - " & rule.RuleClass & ")", SelectedImage(rule.RuleType))
                        NewNode.Tag = New Object() {rule, Task}
                        TreeView1.Nodes.Add(NewNode)
                        lastNodeInserted = NewNode

                        countNodes += 1

                    End If

                    TreeView1.ExpandAll()
                    lastRuleInsert = rule
                Else

                    nodoActual = ObtenerNodoPadre(rule)

                    If nodoActual IsNot Nothing Then
                        NewNode = New RadTreeNode(rule.Name & " (" & rule.ID.ToString() & " - " & rule.RuleClass & ")", SelectedImage(rule.RuleType))
                        NewNode.Tag = New Object() {rule, Task}
                        nodoActual.Nodes.Add(NewNode)
                        lastNodeInserted = NewNode
                    End If

                    lastRuleInsert = rule
                    TreeView1.ExpandAll()
                End If
            End If

            If lastNodeInserted IsNot Nothing Then
                Me.TreeView1.SelectedNode = lastNodeInserted
            End If

            If BreakPointsUtil.CheckBreakPointOnRule(rule.ID) AndAlso Not BreakPointsUtil.BreakPointContinue(rule.ID) Then
                lastNodeInserted.Text &= ("  - EN PAUSA")
            End If

            Me.TreeView1.Refresh()

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Function ObtenerNodoPadre(rule As WFRuleParent) As RadTreeNode
        Dim nodoActual As RadTreeNode
        Dim ParentNodeName As String = rule.ParentRule.Name & " (" & rule.ParentRule.ID.ToString() & " - " & rule.ParentRule.RuleClass & ")"
        Dim nodosEncontrados As List(Of RadTreeNode) = BuscarNodoPorNombre(Me.TreeView1.Nodes, ParentNodeName)
        nodoActual = nodosEncontrados.LastOrDefault()
        Return nodoActual
    End Function

    Private Function BuscarNodoPorNombre(ByRef nodos As RadTreeNodeCollection, ByVal searchName As String) As List(Of RadTreeNode)
        Try
            Dim encontrados As New List(Of RadTreeNode)

            For Each nodo As RadTreeNode In nodos
                If nodo.Name = searchName Then
                    encontrados.Add(nodo)
                Else
                    encontrados.AddRange(BuscarNodoPorNombre(nodo.Nodes, searchName))
                End If
            Next
            Return encontrados
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function
    Private Function SelectedImage(ruleType As TypesofRules) As Integer
        'Falta agregar los iconos correctos.
        Select Case ruleType
            Case TypesofRules.AccionUsuario, 10
                Return 48
            Case 1, 2, 3, 4 'Tareas
                Return 33
            Case TypesofRules.Insertar, 6, 7 'Entrada salida
                Return 53

            Case TypesofRules.AbrirDocumento
                Return 53

            Case TypesofRules.AbrirZamba
                Return 18
            Case TypesofRules.GuardarDocumento

                Return 53

            Case Else
                Return 53

                'Actualizacion = 8
                'Planificada = 9
                'Condicion = 11
                'ValidacionEntrada = 12
                'ValidacionSalida = 13
                'Floating = 14
                'Iniciar = 30
                'Terminar = 31
                'Derivar = 33
                'Asignar = 34
                'Estado = 35
                'Indices = 37

        End Select
    End Function

    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(WFPanelDebugger))
        Me.TreeViewContextMenu = New ContextMenu()
        Me.mnuExpadirRegla = New System.Windows.Forms.MenuItem()
        Me.TreeView1 = New Telerik.WinControls.UI.RadTreeView()
        Me.panelToolbar = New System.Windows.Forms.Panel()
        Me.Toolbar = New Zamba.AppBlock.ZToolBar()
        Me.btnPrintPreview = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.btnExportBMP = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.txtRuleId = New System.Windows.Forms.ToolStripTextBox()
        Me.pnlTreeView = New ZPanel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.pnlDetails = New ZPanel()
        Me.FlowPanel = New System.Windows.Forms.FlowLayoutPanel()
        CType(Me.TreeView1, ComponentModel.ISupportInitialize).BeginInit()
        Me.panelToolbar.SuspendLayout()
        Me.Toolbar.SuspendLayout()
        CType(Me.SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.pnlDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'TreeViewContextMenu
        '
        Me.TreeViewContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuExpadirRegla})
        '
        'mnuExpadirRegla
        '
        Me.mnuExpadirRegla.Index = 0
        Me.mnuExpadirRegla.Text = "Expandir todo"
        '
        'TreeView1
        '
        Me.TreeView1.AllowArbitraryItemHeight = True
        Me.TreeView1.AllowDrop = True
        Me.TreeView1.AllowPlusMinusAnimation = True
        Me.TreeView1.BackColor = System.Drawing.Color.White
        Me.TreeView1.CheckBoxes = True
        Me.TreeView1.ContextMenu = Me.TreeViewContextMenu
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.ExpandAnimation = Telerik.WinControls.UI.ExpandAnimation.None
        Me.TreeView1.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Me.TreeView1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.TreeView1.HideSelection = False
        Me.TreeView1.ItemHeight = 22
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Margin = New System.Windows.Forms.Padding(0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.PlusMinusAnimationStep = 1.0R
        '
        '
        '
        Me.TreeView1.RootElement.AutoSize = False
        Me.TreeView1.RootElement.ControlBounds = New System.Drawing.Rectangle(0, 0, 150, 250)
        Me.TreeView1.ShowLines = True
        Me.TreeView1.ShowNodeToolTips = True
        Me.TreeView1.ShowRootLines = False
        Me.TreeView1.Size = New System.Drawing.Size(572, 320)
        Me.TreeView1.SpacingBetweenNodes = -1
        Me.TreeView1.TabIndex = 98
        Me.TreeView1.ThemeName = "TelerikMetroBlue"
        Me.TreeView1.TreeIndent = 18
        '
        'panelToolbar
        '
        Me.panelToolbar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelToolbar.Controls.Add(Me.Toolbar)
        Me.panelToolbar.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelToolbar.Location = New System.Drawing.Point(0, 0)
        Me.panelToolbar.Name = "panelToolbar"
        Me.panelToolbar.Size = New System.Drawing.Size(572, 29)
        Me.panelToolbar.TabIndex = 99
        Me.panelToolbar.Visible = False
        '
        'Toolbar
        '
        Me.Toolbar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Toolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPrintPreview, Me.btnPrint, Me.btnExportBMP, Me.ToolStripLabel1, Me.txtRuleId})
        Me.Toolbar.Location = New System.Drawing.Point(0, 0)
        Me.Toolbar.Name = "Toolbar"
        Me.Toolbar.Size = New System.Drawing.Size(570, 27)
        Me.Toolbar.TabIndex = 0
        Me.Toolbar.Text = "ZToolBar1"
        Me.Toolbar.Visible = False
        '
        'btnPrintPreview
        '
        Me.btnPrintPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnPrintPreview.Image = CType(resources.GetObject("btnPrintPreview.Image"), System.Drawing.Image)
        Me.btnPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrintPreview.Name = "btnPrintPreview"
        Me.btnPrintPreview.Size = New System.Drawing.Size(23, 24)
        Me.btnPrintPreview.Text = "Previsualizar"
        '
        'btnPrint
        '
        Me.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(23, 24)
        Me.btnPrint.Text = "Imprimir"
        '
        'btnExportBMP
        '
        Me.btnExportBMP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnExportBMP.Image = CType(resources.GetObject("btnExportBMP.Image"), System.Drawing.Image)
        Me.btnExportBMP.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnExportBMP.Name = "btnExportBMP"
        Me.btnExportBMP.Size = New System.Drawing.Size(23, 24)
        Me.btnExportBMP.Text = "Exportar a imagen"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(45, 24)
        Me.ToolStripLabel1.Text = "Buscar:"
        '
        'txtRuleId
        '
        Me.txtRuleId.AutoToolTip = True
        Me.txtRuleId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRuleId.Name = "txtRuleId"
        Me.txtRuleId.Size = New System.Drawing.Size(100, 27)
        Me.txtRuleId.ToolTipText = "Ingrese el ID o nombre de la regla a buscar"
        '
        'pnlTreeView
        '
        Me.pnlTreeView.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.pnlTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTreeView.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTreeView.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.pnlTreeView.Location = New System.Drawing.Point(0, 0)
        Me.pnlTreeView.Name = "pnlTreeView"
        Me.pnlTreeView.Size = New System.Drawing.Size(318, 291)
        Me.pnlTreeView.TabIndex = 100
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 29)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.pnlTreeView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlDetails)
        Me.SplitContainer1.Size = New System.Drawing.Size(572, 291)
        Me.SplitContainer1.SplitterDistance = 318
        Me.SplitContainer1.TabIndex = 101
        '
        'pnlDetails
        '
        Me.pnlDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.pnlDetails.Controls.Add(Me.FlowPanel)
        Me.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDetails.Font = New Font("Verdana", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Me.pnlDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(76, Byte), Integer))
        Me.pnlDetails.Location = New System.Drawing.Point(0, 0)
        Me.pnlDetails.Name = "pnlDetails"
        Me.pnlDetails.Size = New System.Drawing.Size(250, 291)
        Me.pnlDetails.TabIndex = 0
        '
        'FlowPanel
        '
        Me.FlowPanel.AutoScroll = True
        Me.FlowPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowPanel.Location = New System.Drawing.Point(0, 0)
        Me.FlowPanel.Name = "FlowPanel"
        Me.FlowPanel.Size = New System.Drawing.Size(250, 291)
        Me.FlowPanel.TabIndex = 0
        '
        'WFPanelDebugger
        '
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.panelToolbar)
        Me.Controls.Add(Me.TreeView1)
        Me.Name = "WFPanelDebugger"
        Me.Size = New System.Drawing.Size(572, 320)
        CType(Me.TreeView1, ComponentModel.ISupportInitialize).EndInit()
        Me.panelToolbar.ResumeLayout(False)
        Me.panelToolbar.PerformLayout()
        Me.Toolbar.ResumeLayout(False)
        Me.Toolbar.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.pnlDetails.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Public Sub ClearDebugPanel()
        If Me.TreeView1 IsNot Nothing AndAlso Me.TreeView1.Nodes.Count > 0 Then
            Me.TreeView1.Nodes.Clear()
            lastRuleInsert = Nothing
        End If
    End Sub

#End Region

    Private _IL As Zamba.AppBlock.ZIconsList
    Private _treeViewHelper As Zamba.Core.IPrintTreeViewHelper = Nothing
    Private WFRule As WFRuleParent

    Public Sub New(ByVal IL As Zamba.AppBlock.ZIconsList)
        Me.New()
        _IL = IL
        Me.TreeView1.ImageList = Me._IL.ZIconList
        Me.pnlTreeView.Controls.Add(Me.TreeView1)
    End Sub

#Region "WFTransitions"
    Public Sub SelectStepNode(ByRef wfstep As WFStep)
        Try
            For Each n As RadTreeNode In Me.TreeView1.Nodes(0).Nodes
                If TypeOf n Is EditStepNode AndAlso DirectCast(n, EditStepNode).WFStep Is wfstep Then
                    Me.TreeView1.SelectedNode = n
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
            Me.TreeView1.Nodes(0).Nodes.Add(New EditStepNode(wfstep, wf.InitialStep))
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub RemoveStep(ByVal DelWFStep As WFStep)
        Try
            For Each n As RadTreeNode In Me.TreeView1.Nodes(0).Nodes
                If TypeOf n Is EditStepNode AndAlso DirectCast(n, EditStepNode).WFStep Is DelWFStep Then
                    Me.TreeView1.Nodes(0).Nodes.Remove(n)
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub NameStep(ByVal NameWFStep As WFStep)
        Try
            For Each n As RadTreeNode In Me.TreeView1.Nodes(0).Nodes
                If TypeOf n Is EditStepNode AndAlso DirectCast(n, EditStepNode).WFStep Is NameWFStep Then
                    n.Text = NameWFStep.Name
                End If
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "Context Menu"

    ''' <summary>
    ''' Evento que muestra el menú contextual al hacer un click derecho sobre un elemento del árbol
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	16/09/2008	Modified    Se agrego el Copiar Regla
    ''' </history>
    Private Sub TreeViewContextMenu_Popup(ByVal sender As Object, ByVal e As EventArgs) Handles TreeViewContextMenu.Popup
        Try
            If TypeOf (Me.TreeView1.SelectedNode) Is BaseWFNode Then

                Dim BaseWFNode As BaseWFNode = DirectCast(TreeView1.SelectedNode, BaseWFNode)

                'PRIMERO PONGO EN VISIBLE FALSE A TODOS LOS MENUES
                For Each item As MenuItem In Me.TreeViewContextMenu.MenuItems
                    item.Visible = False
                Next

                '[AlejandroR] (WI 2592) - 15/01/10
                'Todos los nodos tienen que tener la opcion de expandir reglas
                mnuExpadirRegla.Visible = True

                'LUEGO PONGO EN VISIBLE TRUE LOS QUE ME INTERESAN
                Select Case BaseWFNode.NodeWFType

                    Case NodeWFTypes.Etapa, NodeWFTypes.Estado

                        '--------------------------------- WFStep
                        Dim sn As EditStepNode = DirectCast(Me.TreeView1.SelectedNode, EditStepNode)
                        Dim wf As IWorkFlow
                        wf = WFBusiness.GetWFbyId(sn.WFStep.WorkId)

                    Case NodeWFTypes.TipoDeRegla

                        '---------------------------------- RuleType
                        Dim HaveNodes As Boolean
                        For Each n As RadTreeNode In BaseWFNode.Nodes
                            HaveNodes = True
                            Exit For
                        Next


                        'SI ES ACCION DE USUARIO                  
                        If DirectCast(BaseWFNode, RuleTypeNode).RuleParentType = TypesofRules.AccionUsuario Then
                            If HaveNodes Then
                            Else
                                mnuExpadirRegla.Visible = False
                            End If
                        End If


                    Case NodeWFTypes.Regla

                        '--------------------------------- RuleCondition
                        Dim HaveNodes As Boolean
                        For Each n As RadTreeNode In BaseWFNode.Nodes
                            HaveNodes = True
                            Exit For
                        Next

                        'Si no tiene nodos


                    Case NodeWFTypes.Permiso
                        '--------------------------------- Rights
                        For Each item As MenuItem In Me.TreeViewContextMenu.MenuItems
                            item.Visible = False
                        Next

                    Case NodeWFTypes.FloatingRule
                        Dim HaveNodes As Boolean
                        For Each n As RadTreeNode In BaseWFNode.Nodes
                            HaveNodes = True
                            Exit For
                        Next


                End Select
            Else
                '[Tomas]    23/04/2009
                'Si el nodo no es un BaseWFNode no se muestra el contextmenu
                'Esto se utiliza mas que nada por nodos como el de Eventos que
                'sirven nada mas como etiquetas para ver que reglas se encuentran
                'en el evento correspondiente.
                For Each item As MenuItem In Me.TreeViewContextMenu.MenuItems
                    item.Visible = False
                Next

                mnuExpadirRegla.Visible = True
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region

#Region "TreeView Select"
    Private Sub TreeViewElement_NodeFormatting(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.TreeNodeFormattingEventArgs) Handles TreeView1.NodeFormatting

        'args.NodeElement.ClipDrawing = False
        'args.NodeElement.ImageElement.FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentBounds
        'args.NodeElement.ContentElement.DisableHTMLRendering = True
        'args.NodeElement.ImageElement.StretchHorizontally = False
        'args.NodeElement.ImageElement.ImageLayout = ImageLayout.Stretch
        'args.NodeElement.ImageElement.MinSize = New Size(20, 20)

        'args.NodeElement.BackColor = Color.White
        'args.NodeElement.BackColor2 = Color.White
        'args.NodeElement.BackColor3 = Color.White
        'args.NodeElement.BackColor4 = Color.White

        'args.NodeElement.ContentElement.ResetValue(LightVisualElement.FontProperty, Telerik.WinControls.ValueResetFlags.Local)
        'args.NodeElement.ContentElement.DisableHTMLRendering = True
        'CType(args.NodeElement.Children(0), LightVisualElement).Image = Nothing

    End Sub



    Private Sub TreeView1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeView1.MouseDown
        Try
            Dim node As RadTreeNode = Me.TreeView1.GetNodeAt(e.X, e.Y)
            If IsNothing(node) = False Then
                TreeView1.SelectedNode = node
            End If
            ShowData(node.Tag(0), node.Tag(1))
            'RaiseEvent NodeSelected(node.Tag(0), node.Tag(1))
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub ShowData(rule As IRule, task As Result)
        Try
            Me.FlowPanel.Controls.Clear()
            Me.FlowPanel.Controls.Add(New Label() With {.Text = "REGLA: "})
            For Each prop As PropertyInfo In rule.GetType().GetProperties()
                Try
                    'If EsPublica(prop) Then
                    Me.FlowPanel.Controls.Add(New Label() With {.Text = prop.Name + ": " + If(Not String.IsNullOrEmpty(prop.GetValue(rule)), prop.GetValue(rule).ToString(), String.Empty)})
                    'End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Next

            Me.FlowPanel.Controls.Add(New Label() With {.Text = "TAREA: "})
            For Each prop As PropertyInfo In task.GetType().GetProperties()
                Try
                    Me.FlowPanel.Controls.Add(New Label() With {.Text = prop.Name + ": " + If(Not String.IsNullOrEmpty(prop.GetValue(task)), prop.GetValue(task).ToString(), String.Empty)})
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#Region "Eventos"
    Public Event WFSelected(ByVal WF As Core.WorkFlow)
    Public Event StepSelected(ByRef wfstep As WFStep)
    Public Event TypesofRuleselected(ByVal RuleParentType As TypesofRules, ByVal ruleIds As Generic.List(Of Int64))
    Public Event RuleSelected(ByRef ruleid As Int64)
    Public Event RightSelected(ByRef wfstep As WFStep)
    Public Event NodeSelected(Rule As IRule, Task As ITaskResult)

#End Region
#End Region





    Private Sub btnPrintPreview_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnPrintPreview.Click
        Try
            LoadTreeViewHelper()
            PrintTree(True)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnPrint.Click
        Try
            LoadTreeViewHelper()
            PrintTree(False)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub btnExportBMP_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnExportBMP.Click
        Try
            LoadTreeViewHelper()
            ExportTree()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub PrintTree(ByVal Preview As Boolean)

        Dim Resul As DialogResult

        Resul = MessageBox.Show("¿Desea expandir el arbol para poder imprimirlo completo?", "Expandir arbol", MessageBoxButtons.YesNoCancel)

        Select Case Resul
            Case DialogResult.Yes
                TreeView1.ExpandAll()
            Case DialogResult.Cancel
                Exit Sub
        End Select

        If Preview Then
            _treeViewHelper.PrintPreviewTree(TreeView1, Me.TreeView1.Nodes(0).Text)
        Else
            _treeViewHelper.PrintTree(TreeView1, Me.TreeView1.Nodes(0).Text)
        End If

    End Sub

    Private Sub ExportTree()

        Dim Resul As DialogResult

        Resul = MessageBox.Show("¿Desea expandir el arbol para poder exportarlo completo?", "Expandir arbol", MessageBoxButtons.YesNoCancel)

        Select Case Resul
            Case DialogResult.Yes
                TreeView1.ExpandAll()
            Case DialogResult.Cancel
                Exit Sub
        End Select

        Dim sd As New SaveFileDialog

        sd.DefaultExt = ".bmp"
        sd.Filter = "Imagen (*.bmp)|*.bmp"
        sd.CheckPathExists = True
        sd.FileName = Me.TreeView1.Nodes(0).Text & ".bmp"

        If sd.ShowDialog = DialogResult.OK Then
            _treeViewHelper.SaveTreeBitmap(TreeView1, sd.FileName)
        End If

        sd.Dispose()
        sd = Nothing

    End Sub

    Private Sub LoadTreeViewHelper()
        If _treeViewHelper Is Nothing Then

            'Dim tt As System.Reflection.Assembly
            'Dim t As System.Type
            'Dim args() As Object

            'tt = System.Reflection.Assembly.LoadFile(System.Windows.Forms.Application.StartupPath + "\\Zamba.PrintTreeView.dll")
            't = tt.GetType("Zamba.PrintTreeView.PrintHelper", True, True)

            '_treeViewHelper = DirectCast(Activator.CreateInstance(t, args), Zamba.Core.IPrintTreeViewHelper)
            _treeViewHelper = New Zamba.PrintTreeView.PrintHelper()
        End If

    End Sub

    Private Sub txtRuleId_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRuleId.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim encontrado As Boolean = False
            Dim searchId As Int64
            If Int64.TryParse(txtRuleId.Text, searchId) AndAlso searchId > 0 Then
                BuscarReglaPorID(TreeView1.Nodes, encontrado, searchId)
            Else
                BuscarReglaPorNombre(TreeView1.Nodes, encontrado, txtRuleId.Text.Trim().ToLower())
            End If
            If Not encontrado Then
                MessageBox.Show("No se ha encontrado la regla", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Public Sub ClearTrace(ByVal node As RadTreeNode)
        Try
            If node Is Nothing Then node = Me.TreeView1.Nodes(0)

            For Each childnode As RadTreeNode In node.Nodes
                childnode.BackColor = Color.White
                childnode.Checked = False
                ClearTrace(childnode)
            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub Trace(ByVal Rule As IRule)
        Try

            Dim encontrado As Boolean = False
            Dim RN As RuleNode = BuscarReglaPorID(TreeView1.Nodes, encontrado, Rule.ID)
            encontrado = False
            If Not RN Is Nothing Then
                DirectCast(RN, RadTreeNode).BackColor = Color.LightBlue
                DirectCast(RN, RadTreeNode).Checked = True
            End If
            Application.DoEvents()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Function BuscarReglaPorID(ByRef nodos As RadTreeNodeCollection, ByRef encontrado As Boolean, ByVal searchID As Int64) As RuleNode
        If Not encontrado Then
            For Each nodo As RadTreeNode In nodos
                If TypeOf (nodo) Is RuleNode AndAlso DirectCast(nodo, RuleNode).RuleId = searchID Then
                    nodo.Expand()
                    nodo.EnsureVisible()
                    TreeView1.SelectedNode = nodo
                    encontrado = True
                    Return nodo
                Else
                    Dim ChildNodo As RuleNode = BuscarReglaPorID(nodo.Nodes, encontrado, searchID)
                    If Not ChildNodo Is Nothing Then
                        encontrado = True
                        Return ChildNodo
                    End If
                End If
                If encontrado Then
                    Exit Function
                End If
            Next
        End If
    End Function

    ''' <summary>
    ''' Busca la regla por el nombre
    ''' </summary>
    ''' <param name="nodos"></param>
    ''' <param name="encontrado"></param>
    ''' <param name="searchName"></param>
    ''' <history>   Marcelo Created 19/01/11</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function BuscarReglaPorNombre(ByRef nodos As RadTreeNodeCollection, ByRef encontrado As Boolean, ByVal searchName As String)
        If Not encontrado Then
            For Each nodo As RadTreeNode In nodos
                If TypeOf (nodo) Is RuleNode AndAlso DirectCast(nodo, RuleNode).RuleName.ToLower().Contains(searchName) Then
                    nodo.Expand()
                    nodo.EnsureVisible()
                    TreeView1.SelectedNode = nodo
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

    Public Sub OpenMissedRule(ByVal workflowId As Int64, ByVal ruleId As Int64) Implements IWFPanelCircuit.OpenMissedRule

    End Sub
End Class