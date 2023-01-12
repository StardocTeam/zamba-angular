Imports Zamba.Core.WF.WF
Imports Zamba.Core
Imports Telerik.WinControls.UI

Public Class WFPanelMonitor
    Inherits ZControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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
    Friend WithEvents PanelTop As ZLabel
    Friend WithEvents TreeView1 As RadTreeView
    Friend WithEvents IBUpdate As Button
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(WFPanelMonitor))
        PanelTop = New ZLabel
        TreeView1 = New RadTreeView
        IBUpdate = New Button
        SuspendLayout()
        '
        'PanelTop
        '
        PanelTop.BackColor = System.Drawing.Color.White
        PanelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        PanelTop.Font = New Font("Verdana", 12.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
        PanelTop.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        PanelTop.Location = New System.Drawing.Point(0, 0)
        PanelTop.Name = "PanelTop"
        PanelTop.Size = New System.Drawing.Size(200, 32)
        PanelTop.TabIndex = 97
        PanelTop.Text = " Etapas"
        PanelTop.TextAlign = ContentAlignment.MiddleLeft
        '
        'TreeView1
        '
        TreeView1.AllowDrop = True
        TreeView1.BackColor = System.Drawing.Color.White
        'Me.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        TreeView1.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        TreeView1.ForeColor = System.Drawing.Color.FromArgb(76, 76, 76)
        TreeView1.FullRowSelect = True
        TreeView1.HideSelection = False
        TreeView1.ImageIndex = -1
        '  Me.TreeView1.Indent = 28
        TreeView1.ItemHeight = 24
        TreeView1.Location = New System.Drawing.Point(0, 32)
        TreeView1.Name = "TreeView1"
        ' Me.TreeView1.SelectedImageIndex = -1
        TreeView1.ShowLines = False
        'Me.TreeView1.ShowPlusMinus = False
        TreeView1.ShowRootLines = False
        TreeView1.Size = New System.Drawing.Size(200, 288)
        TreeView1.TabIndex = 98
        TreeView1.AllowDrop = True
        TreeView1.FullRowSelect = True
        TreeView1.HideSelection = False
        TreeView1.HotTracking = True
        TreeView1.Location = New System.Drawing.Point(0, 0)
        TreeView1.Margin = New System.Windows.Forms.Padding(0)
        TreeView1.ShowLines = False
        TreeView1.ShowNodeToolTips = True
        TreeView1.ShowRootLines = False
        TreeView1.ItemHeight = 27
        TreeView1.ExpandAnimation = ExpandAnimation.None
        TreeView1.AllowPlusMinusAnimation = False
        TreeView1.PlusMinusAnimationStep = 1
        TreeView1.TreeViewElement.AutoSizeItems = Telerik.WinControls.Enumerations.ToggleState.On
        TreeView1.TreeViewElement.AllowAlternatingRowColor = Telerik.WinControls.Enumerations.ToggleState.Off
        TreeView1.FullRowSelect = Telerik.WinControls.Enumerations.ToggleState.On
        TreeView1.ShowExpandCollapse = Telerik.WinControls.Enumerations.ToggleState.On
        TreeView1.ShowRootLines = Telerik.WinControls.Enumerations.ToggleState.Off
        TreeView1.ShowLines = Telerik.WinControls.Enumerations.ToggleState.Off
        TreeView1.EnableTheming = True
        TreeView1.ThemeName = "TelerikMetroBlue"

        '
        'IBUpdate
        '
        IBUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        IBUpdate.BackColor = System.Drawing.Color.FromArgb(CType(213, Byte), CType(213, Byte), CType(255, Byte))
        IBUpdate.Location = New System.Drawing.Point(160, 4)
        IBUpdate.Name = "IBUpdate"
        IBUpdate.Size = New System.Drawing.Size(24, 24)
        IBUpdate.TabIndex = 99
        '
        'WFPanelMonitor
        '
        BackColor = System.Drawing.Color.White
        Controls.Add(IBUpdate)
        Controls.Add(TreeView1)
        Controls.Add(PanelTop)
        Name = "WFPanelMonitor"
        Size = New System.Drawing.Size(200, 320)
        ResumeLayout(False)

    End Sub

#End Region

    Private WF As Zamba.Core.WorkFlow
    Public Sub New(ByVal WF As Core.WorkFlow, ByVal IL As Zamba.AppBlock.ZIconsList)
        Me.New()
        LoadWf(WF, IL)
    End Sub
    Public Sub LoadWf(ByVal WF As Core.WorkFlow, ByVal IL As Zamba.AppBlock.ZIconsList)
        Try
            TreeView1.ImageList = IL.ZIconList
            Me.WF = WF
            WFTaskBusiness.LoadMonitor(WF, TreeView1)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

#Region "TreeView Select"

    Private Sub TreeViewElement_NodeFormatting(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.TreeNodeFormattingEventArgs) Handles TreeView1.NodeFormatting
        Dim Node As IBaseWFNode = DirectCast(args.Node, IBaseWFNode)
        Dim NodeType As NodeWFTypes = Node.NodeWFType
        Dim NodeHelper As New WFNodeHelper
        Dim currentImage As Image = NodeHelper.GetnodeImage(NodeType)

        args.NodeElement.ImageElement.Image = currentImage
        args.NodeElement.ClipDrawing = False
        args.NodeElement.ImageElement.FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentBounds
        args.NodeElement.ContentElement.DisableHTMLRendering = True
        args.NodeElement.ImageElement.StretchHorizontally = False
        args.NodeElement.ImageElement.ImageLayout = ImageLayout.Stretch
        args.NodeElement.ImageElement.MinSize = New Size(20, 20)


        args.NodeElement.BackColor = Color.White
        args.NodeElement.BackColor2 = Color.White
        args.NodeElement.BackColor3 = Color.White
        args.NodeElement.BackColor4 = Color.White

    End Sub

    Private Sub TreeView1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeView1.MouseDown
        If e.Button = MouseButtons.Right Then
            Try
                Dim node As RadTreeNode = TreeView1.GetNodeAt(e.X, e.Y)
                If IsNothing(node) = False Then
                    TreeView1.SelectedNode = node
                End If
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End If
    End Sub
    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As RadTreeViewEventArgs) Handles TreeView1.SelectedNodeChanged
        TreeViewSelect()
    End Sub

    Public Event WFSelected(ByVal WF As Core.WorkFlow)
    Public Event StepSelected(ByRef wfstep As WFStep)
    Private Sub TreeViewSelect()
        Try
            If IsNothing(TreeView1.SelectedNode) = False Then
                Dim BaseWFNode As BaseWFNode = DirectCast(TreeView1.SelectedNode, BaseWFNode)
                Select Case BaseWFNode.NodeWFType
                    Case NodeWFTypes.WorkFlow
                        RaiseEvent WFSelected(DirectCast(DirectCast(BaseWFNode, WFNode).WorkFlow, Core.WorkFlow))
                    Case NodeWFTypes.Etapa, NodeWFTypes.Estado
                        RaiseEvent StepSelected(DirectCast(DirectCast(BaseWFNode, MonitorStepNode).WFStep, WFStep))
                End Select
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region

End Class
