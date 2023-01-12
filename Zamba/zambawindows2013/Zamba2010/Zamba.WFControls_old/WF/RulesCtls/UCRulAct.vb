Public Class UCRulAct
    Inherits ZControl

#Region " Windows Form Designer generated code "

    Private Sub New()
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
    Friend WithEvents Panel4 As Panel
    Friend WithEvents TreeViewContextMenu As ContextMenu
    Friend WithEvents mnuAdd As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDel As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MnuChangeName As System.Windows.Forms.MenuItem
    Friend WithEvents MnuCopy As System.Windows.Forms.MenuItem
    Friend WithEvents MnuCut As System.Windows.Forms.MenuItem
    Friend WithEvents MnuPast As System.Windows.Forms.MenuItem
    <DebuggerStepThrough()> Private Sub InitializeComponent()
        Panel4 = New System.Windows.Forms.Panel
        TreeView1 = New System.Windows.Forms.TreeView
        TreeViewContextMenu = New ContextMenu
        mnuAdd = New System.Windows.Forms.MenuItem
        MenuItem6 = New System.Windows.Forms.MenuItem
        MenuItem4 = New System.Windows.Forms.MenuItem
        MenuItem1 = New System.Windows.Forms.MenuItem
        MnuCopy = New System.Windows.Forms.MenuItem
        MnuCut = New System.Windows.Forms.MenuItem
        MnuPast = New System.Windows.Forms.MenuItem
        mnuDel = New System.Windows.Forms.MenuItem
        MnuChangeName = New System.Windows.Forms.MenuItem
        Panel4.SuspendLayout()
        SuspendLayout()
        '
        'Panel4
        '
        Panel4.AutoScroll = True
        Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Panel4.Controls.Add(TreeView1)
        Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Panel4.Location = New System.Drawing.Point(0, 0)
        Panel4.Name = "Panel4"
        Panel4.Size = New System.Drawing.Size(360, 272)
        Panel4.TabIndex = 55
        '
        'TreeView1
        '
        TreeView1.AllowDrop = True
        TreeView1.BackColor = System.Drawing.Color.White
        TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        TreeView1.ContextMenu = TreeViewContextMenu
        TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        TreeView1.Font = New Font("Tahoma", 9.75!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        TreeView1.HideSelection = False
        TreeView1.ImageIndex = -1
        TreeView1.ItemHeight = 18
        TreeView1.Location = New System.Drawing.Point(0, 0)
        TreeView1.Name = "TreeView1"
        TreeView1.SelectedImageIndex = -1
        TreeView1.Size = New System.Drawing.Size(358, 270)
        TreeView1.TabIndex = 95
        '
        'TreeViewContextMenu
        '
        TreeViewContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {mnuAdd, MenuItem6, MenuItem4, MenuItem1, MnuCopy, MnuCut, MnuPast, mnuDel, MnuChangeName})
        '
        'mnuAdd
        '
        mnuAdd.Index = 0
        mnuAdd.Text = "Agregar Regla"
        '
        'MenuItem6
        '
        MenuItem6.Index = 1
        MenuItem6.Text = "-"
        '
        'MenuItem4
        '
        MenuItem4.Index = 2
        MenuItem4.Text = "Icono"
        '
        'MenuItem1
        '
        MenuItem1.Index = 3
        MenuItem1.Text = "-"
        '
        'MnuCopy
        '
        MnuCopy.Index = 4
        MnuCopy.Text = "Copiar"
        '
        'MnuCut
        '
        MnuCut.Index = 5
        MnuCut.Text = "Cortar"
        '
        'MnuPast
        '
        MnuPast.Index = 6
        MnuPast.Text = "Pegar"
        '
        'mnuDel
        '
        mnuDel.Index = 7
        mnuDel.Text = "Eliminar"
        '
        'MnuChangeName
        '
        MnuChangeName.Index = 8
        MnuChangeName.Text = "Cambiar Nombre"
        '
        'UCRulAct
        '
        BackColor = System.Drawing.Color.White
        Controls.Add(Panel4)
        Name = "UCRulAct"
        Size = New System.Drawing.Size(360, 272)
        Panel4.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

#End Region

    '    Private WF As WorkFlow
    '    Private WfStep As WfStep
    '    Dim Icons As ZIcons = Zamba.controls.Icons.SingletonWFIcons.ZIcons
    '    Public Sub New(ByVal wf As WorkFlow, ByRef WfStep As WfStep)
    '        Me.New()
    '        Me.WF = wf
    '        Me.WfStep = WfStep
    '        FillMyTreeView()
    '    End Sub

    '#Region "Load Rules"
    '    'Dim TopRoot As ZNode
    '    Private Sub FillMyTreeView()
    '        Dim dsRule As DsRules
    '        'Try
    '        '    Me.TreeView1.ImageList = Icons.IconList
    '        '    Me.TreeView1.Nodes.Clear()

    '        '    'Nodo Raiz
    '        '    TopRoot = New ZGroupNode(New ZambaCore)
    '        '    TopRoot.Text = ("Reglas Desatachadas")
    '        '    TreeView1.Nodes.Add(TopRoot)
    '        '    TopRoot.Expand()

    '        '    'Traigo Rules desatachados
    '        '    dsRule = WFBusiness.GetDesatachedRules()
    '        '    ' Rules 
    '        '    'WfStep.Rules = WFBusiness.SetRulesInstances(dsRule, WfStep)
    '        '    'For Each rule As WFRule In WfStep.Rules.Values
    '        '    '    If rule.ParentId = 0 Then
    '        '    '        addRuleNode(rule)
    '        '    '    End If
    '        '    'Next
    '        'Catch ex As Exception
    '        '    zamba.core.zclass.raiseerror(ex)
    '        'End Try
    '    End Sub
    '    Private Sub addRuleNode(byref rule As WFRule)
    '        'Try
    '        '    Dim RootRule As New ZRuleNode(Rule)
    '        '    RootRule.ImageIndex = Icons.IconIndex(Rule.IconId)
    '        '    'RootRule.Text = Rule.UserDescription
    '        '    TopRoot.Nodes.Add(RootRule)
    '        '    AddResultsNodes(WfStep, Rule, RootRule)
    '        'Catch ex As Exception
    '        '    zamba.core.zclass.raiseerror(ex)
    '        'End Try
    '    End Sub
    '    Private Sub AddResultsNodes(byref wfstep As WfStep, byref rule As WFRule, ByVal Root As TreeNode)
    '        ''   Dim RootP As New ZRuleResultNode(New ZambaCore(IdTypes.ResultadoPositivo, IdTypes.ResultadoPositivo.ToString, 0, IdTypes.ResultadoPositivo, Rule))
    '        'RootP.ForeColor = Color.Blue
    '        'Root.Nodes.Add(RootP)

    '        '' Dim RootN As New ZRuleResultNode(New ZambaCore(IdTypes.ResultadoNegativo, IdTypes.ResultadoNegativo.ToString, 0, IdTypes.ResultadoNegativo, Rule))
    '        'RootN.ForeColor = Color.Red
    '        'Root.Nodes.Add(RootN)

    '        'Dim Subrules As ArrayList
    '        'Subrules = searchforsubrules(WfStep, Rule.Id)
    '        'If Subrules.Count <> 0 Then
    '        '    Dim i As Integer
    '        '    For Each subrule As WFRule In Subrules
    '        '        'If subrule.ParentMode = IdTypes.ResultadoPositivo Then
    '        '        '    addRuleNode(WfStep, subrule, RootP)
    '        '        'End If
    '        '        'If subrule.ParentMode = IdTypes.ResultadoNegativo Then
    '        '        '    addRuleNode(WfStep, subrule, RootN)
    '        '        'End If
    '        '    Next
    '        'End If
    '    End Sub
    '    Private Function searchforsubrules(byref wfstep As WfStep, ByVal Id As Integer) As ArrayList
    '        Dim FilteredRules As New ArrayList
    '        Try
    '            'For Each rule As WFRule In WfStep.Rules.Values
    '            '    If rule.ParentId = Id Then
    '            '        FilteredRules.Add(rule)
    '            '    End If
    '            'Next
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(ex)
    '        End Try
    '        Return FilteredRules
    '    End Function
    '    Private Sub addRuleNode(byref wfstep As WfStep, byref rule As WFRule, ByVal root As TreeNode)
    '        Try
    '            'Dim Root1 As New ZRuleNode(Rule)
    '            'Root1.ImageIndex = Icons.IconIndex(Rule.IconId)
    '            ''Root1.Text = Rule.UserDescription
    '            'root.Nodes.Add(Root1)
    '            'AddResultsNodes(wfstep, Rule, Root1)
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(ex)
    '        End Try
    '    End Sub
    '    Private Sub addRuleNode(byref wfstep As WfStep, ByVal PRule As WFRule, ByVal ChildRule As WFRule)
    '        'Try
    '        '    Dim Proot As ZRuleNode = GetRuleNode(PRule.Id, Me.TreeView1.Nodes(0))
    '        '    ' Dim Root1 As New ZRuleNode(New ZambaCore(ChildRule.Id, ChildRule.UserDescription, Icons.IconIndex(ChildRule.IconId), IdTypes.WFVALIDATION, PRule))
    '        '    'Proot.Nodes.Add(Root1)
    '        '    'AddResultsNodes(wfstep, ChildRule, Root1)
    '        'Catch ex As Exception
    '        '    zamba.core.zclass.raiseerror(ex)
    '        'End Try
    '    End Sub
    '    Private Function GetRuleNode(ByVal RuleId As Int32, ByVal Node As TreeNode) As TreeNode
    '        Dim i As Int32
    '        For i = 0 To Node.GetNodeCount(False) - 1
    '            Try
    '                If Split(Node.Nodes(i).Tag, ",")(0) = RuleId Then
    '                    Return Node.Nodes(i)
    '                Else
    '                    Dim RuleNode As TreeNode = GetRuleNode(RuleId, Node.Nodes(i))
    '                    If IsNothing(RuleNode) = False Then
    '                        Return RuleNode
    '                    End If
    '                End If
    '            Catch
    '            End Try
    '        Next
    '        Return Nothing
    '    End Function
    '#End Region

    '    Private Sub TreeViewContextMenu_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeViewContextMenu.Popup
    '        'Try
    '        '    Dim ZNode As ZNode = TreeView1.SelectedNode
    '        '    Select Case ZNode.NodeType
    '        '        Case ZNode.NodeTypes.GroupNode
    '        '            Dim VisibleItems() As Int32 = {6}
    '        '            For Each item As MenuItem In Me.TreeViewContextMenu.MenuItems
    '        '                If Array.IndexOf(VisibleItems, item.Index) >= 0 Then
    '        '                    item.Visible = True
    '        '                Else
    '        '                    item.Visible = False
    '        '                End If
    '        '            Next
    '        '        Case ZNode.NodeTypes.RuleNode
    '        '            Dim InVisibleItems() As Int32 = {0, 1, 6}
    '        '            For Each item As MenuItem In Me.TreeViewContextMenu.MenuItems
    '        '                If Array.IndexOf(InVisibleItems, item.Index) >= 0 Then
    '        '                    item.Visible = False
    '        '                Else
    '        '                    item.Visible = True
    '        '                End If
    '        '            Next
    '        '        Case ZNode.NodeTypes.RuleResultNode
    '        '            Dim VisibleItems() As Int32 = {0, 1, 6}
    '        '            For Each item As MenuItem In Me.TreeViewContextMenu.MenuItems
    '        '                If Array.IndexOf(VisibleItems, item.Index) >= 0 Then
    '        '                    item.Visible = True
    '        '                Else
    '        '                    item.Visible = False
    '        '                End If
    '        '            Next
    '        '    End Select
    '        'Catch ex As Exception
    '        '    zamba.core.zclass.raiseerror(ex)
    '        'End Try
    '    End Sub
    '    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
    '        Try
    '            TreeViewSelect()
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(ex)
    '        End Try
    '    End Sub
    '    Private Sub TreeView1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TreeView1.MouseDown
    '        If e.Button = MouseButtons.Right Then
    '            Try
    '                Dim node As TreeNode = Me.TreeView1.GetNodeAt(e.X, e.Y)

    '                If IsNothing(node) = False Then
    '                    Me.TreeView1.SelectedNode = node
    '                End If
    '            Catch ex As Exception
    '            End Try
    '        End If
    '    End Sub

    '#Region "Region TreeViewSelect()"
    '    Public Event WFStepSelected(byref wfstep As WfStep)
    '    Public Event RuleSelected(byref rule As WFRule)
    '    Public SelectedRule As WFRule
    '    Dim RuleType As IdTypes = IdTypes.Entrada
    '    Dim Mode As IdTypes = IdTypes.ResultadoPositivo
    '    ' Dim ParentNode As ZNode
    '    Dim SelectionType As SelectionTypes = SelectionTypes.Root
    '    Enum SelectionTypes
    '        WFStep
    '        Rule
    '        Positiva
    '        Negativa
    '        Root
    '        Entrada
    '        Salida
    '        Actualizacion
    '        Usuario
    '        Schedule
    '    End Enum
    '    Private Sub TreeViewSelect()
    '    Try
    '        If IsNothing(TreeView1.SelectedNode) = False Then
    '            Dim ZNode As ZNode = DirectCast(TreeView1.SelectedNode, ZNode)
    '            Dim ZC As ZambaCore = ZNode.ZambaCore
    '            'todo wf: falta ver cuando la regla depende de un resultado y entonces debe heredar el tipo
    '            Select Case ZNode.NodeType
    '                Case ZNode.NodeTypes.GroupNode
    '                    Me.SelectionType = SelectionTypes.Root
    '                    Me.ParentNode = TreeView1.SelectedNode
    '                    Me.Mode = IdTypes.ResultadoPositivo
    '                    Me.RuleType = IdTypes.Actualizacion
    '                    RaiseEvent WFStepSelected(Nothing)
    '                    RaiseEvent RuleSelected(Nothing)
    '                Case ZNode.NodeTypes.RuleNode
    '                    Me.SelectionType = SelectionTypes.Rule
    '                    Me.ParentNode = TreeView1.SelectedNode.Parent
    '                    Me.SelectedRule = ZC
    '                    Me.Mode = IdTypes.ResultadoPositivo
    '                    'Me.RuleType = DirectCast(ZC, WFRule).ParentMode
    '                    RaiseEvent RuleSelected(ZC)
    '                    RaiseEvent EditRule(Me.WfStep, Me.SelectedRule)
    '                Case ZNode.NodeTypes.RuleResultNode
    '                    Select Case ZC.Id
    '                        Case IdTypes.ResultadoPositivo
    '                            Me.SelectionType = SelectionTypes.Positiva
    '                            Me.ParentNode = TreeView1.SelectedNode
    '                            '             Me.ParentNode = TreeView1.SelectedNode.Parent
    '                            Me.SelectedRule = ZC.Parent
    '                            Me.Mode = IdTypes.ResultadoPositivo
    '                            'Me.RuleType = DirectCast(ZC.Parent, WFRule).ParentMode
    '                        Case IdTypes.ResultadoNegativo
    '                            Me.SelectionType = SelectionTypes.Negativa
    '                            Me.ParentNode = TreeView1.SelectedNode
    '                            Me.SelectedRule = ZC.Parent
    '                            Me.Mode = IdTypes.ResultadoNegativo
    '                            'Me.RuleType = DirectCast(ZC.Parent, WFRule).ParentMode
    '                    End Select
    '                    RaiseEvent WFStepSelected(ZC.Parent.Parent)
    '                    RaiseEvent RuleSelected(ZC.Parent)
    '                Case Else
    '                    Me.SelectionType = SelectionTypes.Root
    '                    Me.ParentNode = TreeView1.SelectedNode
    '                    Me.SelectedRule = Nothing
    '                    Me.RuleType = IdTypes.Actualizacion
    '                    Me.Mode = IdTypes.Actualizacion
    '                    RaiseEvent WFStepSelected(Nothing)
    '                    RaiseEvent RuleSelected(Nothing)
    '            End Select
    '            'ShowEspecificMenu()
    '        End If
    '    Catch ex As Exception
    '        zamba.core.zclass.raiseerror(ex)
    '        '    End Try
    '        'End Sub
    '#End Region

    '#End Region

    '#Region "AddRules"
    'Private Sub mnuAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAdd.Click
    '    Try
    '        Dim wzadd As New WFAddRule(WfStep, SelectedRule, Me.Mode)
    '        AddHandler wzadd.RuleAdded, AddressOf RuleAdded
    '        wzadd.ShowDialog()
    '    Catch ex As Exception
    '        zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub
    'Public Sub RuleAdded(byref wfstep As WfStep, byref rule As WFRule)
    '    Try
    '        AskForUSerDescription(Rule)
    '        WFBusiness.AddRule(Rule)
    '        'Me.WfStep.Rules.Add(Rule.Id, Rule)
    '        Me.addRuleNode(wfstep, Rule, Me.ParentNode)
    '    Catch ex As Exception
    '        zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub
    'Private Sub AskForUSerDescription(byref rule As WFRule)
    '    Try
    '        Dim Userdescription As String
    '        If IsNothing(Userdescription) OrElse Userdescription = "" Then
    '            Userdescription = InputBox("Ingrese la descripcion de la regla", "Descripcion de usuario", Rule.Description)
    '        Else
    '            Userdescription = InputBox("Ingrese la descripcion de la regla", "Descripcion de usuario", Rule.UserDescription)
    '        End If
    '        If IsNothing(Userdescription) OrElse Userdescription = "" Then
    '            Rule.UserDescription = Rule.Description
    '        Else
    '            Rule.UserDescription = Userdescription
    '        End If
    '    Catch ex As Exception
    '        zamba.core.zclass.raiseerror(ex)
    '    End Try
    'End Sub
    '    Public Sub AttachRule(byref rule As WFRule)
    '        Try
    '            If (SelectionType = SelectionType.Positiva OrElse SelectionType = SelectionType.Negativa OrElse SelectionType = SelectionType.Actualizacion OrElse SelectionType = SelectionType.Entrada OrElse SelectionType = SelectionType.Salida OrElse SelectionType = SelectionType.Schedule OrElse SelectionType = SelectionType.Usuario) Then
    '                'Me.WfStep.Rules.Add(Rule.Id, Rule)
    '                'Me.addRuleNode(Rule, Me.TreeView1.SelectedNode)
    '                If IsNothing(SelectedRule) Then
    '                    WFBusiness.AttachRule(Me.WfStep, Rule.Id, 0, Me.WfStep.Id, Me.WfStep.Parent.Id, Me.Mode, Me.Mode)
    '                Else
    '                    WFBusiness.AttachRule(Me.WfStep, Rule.Id, SelectedRule.Id, SelectedRule.Parent.Id, SelectedRule.Parent.Parent.Id, Mode, Mode)
    '                End If
    '            End If
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(ex)
    '        End Try
    '    End Sub
    '#End Region

    '#Region "DelRules"
    '    Private Sub mnuDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDel.Click
    '        Try
    '            WFBusiness.DelRule(SelectedRule)
    '            Me.TreeView1.SelectedNode.Remove()
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(ex)
    '        End Try
    '    End Sub
    '#End Region

    '#Region "EditRules"
    '    Public Event EditRule(byref wfstep As WfStep, byref wfrule As WFRule)
    '    Public Event ChangeRuleDescription()
    '    Private Sub MnuChangeName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuChangeName.Click
    '        'Try
    '        '    If AskForUSerDescription() Then
    '        '        WFBusiness.UpdateRule(SelectedRule)
    '        '        Me.TreeView1.SelectedNode.Text = Me.SelectedRule.UserDescription
    '        '    End If
    '        'Catch ex As Exception
    '        '    zamba.core.zclass.raiseerror(ex)
    '        'End Try
    '    End Sub
    '    Private Function AskForUSerDescription() As Boolean
    '        Try
    '            'Dim Userdescription As String = InputBox("Ingrese la descripcion de la regla", "Descripcion de usuario", SelectedRule.Description)
    '            'If IsNothing(Userdescription) OrElse Userdescription = "" Then
    '            '    SelectedRule.UserDescription = SelectedRule.Description
    '            '    Return False
    '            'Else
    '            '    SelectedRule.UserDescription = Userdescription
    '            '    Return True
    '            'End If
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(ex)
    '            Return False
    '        End Try
    '    End Function
    '#End Region

    '#Region "Detach"
    '    Public Event Ruleatached(byref wfstep As WfStep, byref rule As WFRule)
    '    Private Sub ShowEspecificMenu()
    '        'Try
    '        '    If Selection = SelectionType.Rule AndAlso SelectedRule.Parent.Id = 0 Then
    '        '        RemoveHandler MenuItem5.Click, AddressOf RuleAttached
    '        '        AddHandler MenuItem5.Click, AddressOf RuleAttached
    '        '    Else
    '        '        RemoveHandler MenuItem5.Click, AddressOf RuleAttached
    '        '    End If
    '        'Catch ex As Exception
    '        '    zamba.core.zclass.raiseerror(ex)
    '        'End Try
    '    End Sub
    '    Private Sub RuleAttached(ByVal sender As Object, ByVal e As EventArgs)
    '        Try
    '            RaiseEvent Ruleatached(WfStep, SelectedRule)
    '            Me.TreeView1.SelectedNode.Remove()
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(ex)
    '        End Try
    '    End Sub
    '    Public Sub AttachRule(ByVal RuleDetachedNode As ZRuleNode)
    '        Try
    '            'Me.WfStep.Rules.Add(RuleDetachedNode.ZambaCore.Id, RuleDetachedNode.ZambaCore)
    '            TopRoot.Nodes.Add(RuleDetachedNode)
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(ex)
    '        End Try
    '    End Sub
    '#End Region

    '#Region "Copy-Paste"
    '    Public Event Copy(byref rule As WFRule)
    '    Private Sub MnuCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCopy.Click
    '        Try
    '            RaiseEvent Copy(SelectedRule)
    '            MnuPast.Enabled = True
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(ex)
    '        End Try
    '    End Sub
    '    Public Event Cut(byref rule As WFRule)
    '    Private Sub MnuCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuCut.Click
    '        Try
    '            'Me.WfStep.Rules.Remove(SelectedRule.Id)
    '            Me.TreeView1.SelectedNode.Remove()
    '            RaiseEvent Cut(SelectedRule)
    '            MnuPast.Enabled = True
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(ex)
    '        End Try
    '    End Sub
    '    Public Event Past(ByVal node As TreeNode)
    '    Private Sub MnuPast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuPast.Click
    '        RaiseEvent Past(Me.TreeView1.SelectedNode)
    '    End Sub
    '    Public Sub PasteRule(ByVal Copy As Boolean, ByVal ClipBoardRule As WFRule)
    '        'Try
    '        '    If Copy = True Then
    '        '        Dim newRule As WFRule = WFBusiness.CloneRule(ClipBoardRule)
    '        '        newRule.Parent = Me.WfStep
    '        '        Select Case SelectionType
    '        '            '    Case SelectionTypes.Root
    '        '            '        newRule.ParentId = 0
    '        '            '        newRule.ParentMode = 0
    '        '            '    Case SelectionType.Positiva
    '        '            '        newRule.ParentMode = IdTypes.ResultadoPositivo
    '        '            '        newRule.ParentId = SelectedRule.Id
    '        '            '    Case SelectionType.Negativa
    '        '            '        newRule.ParentMode = IdTypes.ResultadoNegativo
    '        '            '        newRule.ParentId = SelectedRule.Id
    '        '            'End Select
    '        '        'Me.WfStep.Rules.Add(newRule.Id, newRule)
    '        '            'WFBusiness.AddRule(newRule)
    '        '            ' Me.addRuleNode(WfStep, newRule, Me.ParentNode)
    '        '    Else
    '        '        ClipBoardRule.Parent = Me.WfStep
    '        '        Select Case SelectionType
    '        '                'Case SelectionTypes.Root
    '        '                '    ClipBoardRule.ParentId = 0
    '        '                '    ClipBoardRule.ParentMode = 0
    '        '                'Case SelectionType.Positiva
    '        '                '    ClipBoardRule.ParentMode = IdTypes.ResultadoPositivo
    '        '                '    ClipBoardRule.ParentId = SelectedRule.Id
    '        '                'Case SelectionType.Negativa
    '        '                '    ClipBoardRule.ParentMode = IdTypes.ResultadoNegativo
    '        '                '    ClipBoardRule.ParentId = SelectedRule.Id
    '        '        End Select
    '        '        WFBusiness.UpdateCutedRule(ClipBoardRule)
    '        '        'Me.WfStep.Rules.Add(ClipBoardRule.Id, ClipBoardRule)
    '        '        Me.addRuleNode(WfStep, ClipBoardRule, Me.ParentNode)
    '        '    End If
    '        'Catch ex As Exception
    '        '    zamba.core.zclass.raiseerror(ex)
    '        'End Try
    '    End Sub
    '#End Region

    '#Region "Iconos"
    '    Public Event AsignIcon2Rule()
    '    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click
    '        AsignIcon()
    '    End Sub
    '    Sub AsignIcon()
    '        Try
    '            If SelectionType = SelectionType.Rule Then
    '                RaiseEvent AsignIcon2Rule()
    '            End If
    '        Catch ex As Exception
    '            zamba.core.zclass.raiseerror(ex)
    '        End Try
    '    End Sub
    '#End Region

End Class
