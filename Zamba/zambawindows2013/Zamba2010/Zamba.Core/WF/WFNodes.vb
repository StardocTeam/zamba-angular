Imports System.Drawing
Imports Zamba.Core.Enumerators
Imports Telerik.WinControls.UI

Public MustInherit Class BaseWFNode
    Inherits RadTreeNode
    Implements IBaseWFNode

    Private Sub New()

    End Sub

    Public Sub New(Name As String)
        Me.Name = Name
        ItemHeight = 22

    End Sub

#Region " Atributos "
    Private _nodeWFType As NodeWFTypes

#End Region

#Region " Propiedades "
    Public Property NodeWFType() As NodeWFTypes Implements IBaseWFNode.NodeWFType
        Get
            Return _nodeWFType
        End Get
        Set(ByVal value As NodeWFTypes)
            _nodeWFType = value
        End Set
    End Property
#End Region


End Class

Public Class InitNode
    Inherits BaseWFNode
    Implements IInitNode

#Region " Atributos "
    Private _workFlow As IWorkFlow

#End Region

#Region " Propiedades "
    Public Property WorkFlow() As IWorkFlow Implements IInitNode.WorkFlow
        Get
            Return _workFlow
        End Get
        Set(ByVal value As IWorkFlow)
            _workFlow = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New()
        MyBase.New("Inicio")
        NodeWFType = NodeWFTypes.Inicio
        ImageIndex = 18
        'Me.SelectedImageIndex = 53
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
    End Sub
#End Region
End Class

Public Class WFNode
    Inherits BaseWFNode
    Implements IWFNode
#Region " Atributos "
    Private _workFlow As IWorkFlow
#End Region

#Region " Propiedades "
    Public Property WorkFlow() As IWorkFlow Implements IWFNode.WorkFlow
        Get
            Return _workFlow
        End Get
        Set(ByVal value As IWorkFlow)
            _workFlow = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByVal WF As IWorkFlow)
        MyBase.New(WF.Name)

        WorkFlow = WF
        NodeWFType = NodeWFTypes.WorkFlow
        Text = WF.Name
        ImageIndex = 26
        'Me.SelectedImageIndex = 53
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
    End Sub
#End Region
End Class

Public Class WFNodeIdandName
    Inherits BaseWFNode
    Implements IWFNodeIdandName
#Region " Atributos "
    Private _workFlowid As Int64
#End Region

#Region " Propiedades "
    Public Property WorkFlowId() As Int64 Implements IWFNodeIdandName.WorkFlowId
        Get
            Return _workFlowid
        End Get
        Set(ByVal value As Int64)
            _workFlowid = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByVal WFId As Int64, ByVal WFName As String)
        MyBase.New(WFName)

        WorkFlowId = WFId
        NodeWFType = NodeWFTypes.WorkFlow
        Text = WFName
        ImageIndex = 35
        'Me.SelectedImageIndex = 53
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
    End Sub
    Public Sub New(ByVal WFId As Int64, ByVal WFName As String, ByVal taskcountparent As Integer)
        MyBase.New(WFName)

        WorkFlowId = WFId
        NodeWFType = NodeWFTypes.WorkFlow
        BackColor = Color.WhiteSmoke
        Text = WFName & " (" & taskcountparent & ")"
        ImageIndex = 35
        'Me.SelectedImageIndex = 53
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
    End Sub

    Public Sub UpdateTasksCountparent(ByVal TaskCount As Int64)
        Try
            If Not IsNothing(TreeView) AndAlso TreeView.Disposing = False AndAlso Not IsNothing(Nodes) Then
                Dim index As Int32 = Text.LastIndexOf("(")
                If index <> -1 Then
                    Text = Text.Remove(index, Text.Length - index).TrimEnd & " (" & TaskCount & ")"
                Else
                    Text = Text & " (" & TaskCount & ")"
                End If
                'Me.TasksCount = TaskCount
            End If
        Catch ex As ObjectDisposedException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
#End Region
End Class

Public Class StepNode
    Inherits BaseWFNode
    Implements IStepNode

#Region " Atributos "
    Private _wFStep As IWFStep

#End Region

#Region " Propiedades "
    Public Property WFStep() As IWFStep Implements IStepNode.WFStep
        Get
            Return _wFStep
        End Get
        Set(ByVal value As IWFStep)
            _wFStep = value
        End Set
    End Property

#End Region

#Region " Constructores "
    Public Sub New(ByRef wfstep As IWFStep)
        MyBase.New(wfstep.Name)

        Me.WFStep = wfstep
        NodeWFType = NodeWFTypes.Estado
        Text = wfstep.Name & " (" & wfstep.TasksCount & ")"
        ImageIndex = 33
        'Me.SelectedImageIndex = 53
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
    End Sub

#End Region

    Public Sub UpdateTasksCount(ByVal WfStep As IWFStep) Implements IStepNode.UpdateTasksCount
        Try

            If TreeView.Disposing = False AndAlso Not IsNothing(Nodes) Then
                'Me.Text = WFStep.Name & " (" & Me.Nodes.Count & ")"
                Text = WfStep.Name & " (" & WfStep.TasksCount & ")"
            End If
        Catch ex As ObjectDisposedException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class

Public Class StepNodeIdAndName
    Inherits BaseWFNode
    Implements IStepNodeIdAndName

#Region " Atributos "
    Private _wFStepid As Int64
    Private mTaskCount As Int64
#End Region

#Region " Propiedades "

    Public Property WFStepid() As Int64 Implements IStepNodeIdAndName.StepId
        Get
            Return _wFStepid
        End Get
        Set(ByVal value As Int64)
            _wFStepid = value
        End Set
    End Property
    Public Property TasksCount() As Int64 Implements IStepNodeIdAndName.TasksCount
        Get
            Return mTaskCount
        End Get
        Set(ByVal value As Int64)
            mTaskCount = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByVal wfstepid As Int64, ByVal wfstepName As String, ByVal taskCount As Int32)
        MyBase.New(wfstepName)

        Me.WFStepid = wfstepid
        NodeWFType = NodeWFTypes.Etapa
        Text = wfstepName & " (" & taskCount & ")"
        ImageIndex = 33
        'Me.SelectedImageIndex = 53
        TasksCount = taskCount
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
    End Sub

    Public Sub New(ByVal wfstepid As Int64, ByVal wfstepName As String)
        MyBase.New(wfstepName)

        Me.WFStepid = wfstepid
        NodeWFType = NodeWFTypes.Etapa
        Text = wfstepName
        ImageIndex = 33
        'Me.SelectedImageIndex = 53
        TasksCount = 0
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
    End Sub

#End Region



    Public Sub UpdateTasksCount(ByVal TaskCount As Int64) Implements IStepNodeIdAndName.UpdateTasksCount
        Try
            If TreeView.Disposing = False AndAlso Not IsNothing(Nodes) Then
                Dim index As Int32 = Text.LastIndexOf("(")
                If index <> -1 Then
                    Text = Text.Remove(index, Text.Length - index).TrimEnd & " (" & TaskCount & ")"
                Else
                    Text = Text & " (" & TaskCount & ")"
                End If
                TasksCount = TaskCount
            End If
        Catch ex As ObjectDisposedException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class

Public Class SearchNode
    Inherits BaseWFNode
    Implements ISearchNode

#Region " Constructores "
    Public Sub New(ByVal name As String)
        MyBase.New(name)

        NodeWFType = NodeWFTypes.nodoBusqueda
        Me.Name = name
        Text = name
        ImageIndex = 44
        'Me.SelectedImageIndex = 53
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
        ItemHeight = 25

    End Sub
#End Region
End Class

Public Class SearchNodeIdAndName
    Inherits BaseWFNode
    Implements ISearchNodeIdAndName

#Region " Atributos "

    Private mTaskCount As Int64
    Private _searchName As String
    Private _sqlSearch As String
    Private _sqlSearchCount As String
    Private _search As ISearch
    Private _Indexs As Generic.List(Of IIndex)

#End Region

#Region " Propiedades "

    Public Property TasksCount() As Int64 Implements ISearchNodeIdAndName.TasksCount
        Get
            Return mTaskCount
        End Get
        Set(ByVal value As Int64)
            mTaskCount = value
        End Set
    End Property

    Public Property SearchName() As String
        Get
            Return _searchName
        End Get
        Set(ByVal value As String)
            _searchName = value
        End Set
    End Property

    Public Property SqlSearch() As String
        Get
            Return _sqlSearch
        End Get
        Set(ByVal value As String)
            _sqlSearch = value
        End Set
    End Property
    Public Property SqlSearchCount() As String
        Get
            Return _sqlSearchCount
        End Get
        Set(ByVal value As String)
            _sqlSearchCount = value
        End Set
    End Property

    Public Property Search() As ISearch Implements ISearchNodeIdAndName.Search
        Get
            Return _search
        End Get
        Set(ByVal value As ISearch)
            _search = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByVal Search As ISearch, taskCount As Integer)
        MyBase.New(Search.Name)

        Me.Search = Search
        _Indexs = Search.Indexs.ToList
        SearchName = Search.Name
        ToolTipText = Search.Name & " (" & taskCount & ")"
        TasksCount = taskCount
        NodeWFType = NodeWFTypes.Busqueda
        Text = If(Search.Name.Length >= 50, Left(Search.Name, 50) & "...(", Search.Name & " (") & taskCount & ")"
        ImageIndex = 33
        'Me.SelectedImageIndex = 53
        SqlSearch = String.Empty
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
        ItemHeight = 25
    End Sub
#End Region

    'Public Property Search As ISearch Implements ISearchNodeIdAndName.Search
    Private Property SearchResults As DataTable Implements ISearchNodeIdAndName.SearchResults

    Public Sub UpdateTasksCount(ByVal TaskCount As Int64) Implements ISearchNodeIdAndName.UpdateTasksCount
        Try
            If TreeView.Disposing = False AndAlso Not IsNothing(Nodes) Then
                Dim index As Int32 = Text.LastIndexOf("(")
                Text = Text.Substring(0, index) & "(" & TaskCount & ")"
                TasksCount = TaskCount
                ToolTipText = Text
            End If
        Catch ex As ObjectDisposedException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class

Public Class InsertNode
    Inherits BaseWFNode
    Implements IInsertNode

#Region " Constructores "
    Public Sub New(ByVal name As String)
        MyBase.New(name)

        NodeWFType = NodeWFTypes.nodoInsercion
        Me.Name = name
        Text = name
        ImageIndex = 45
        'Me.SelectedImageIndex = 53
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
        ItemHeight = 25
    End Sub
#End Region
End Class

Public Class InsertNodeIdAndName
    Inherits BaseWFNode
    Implements IInsertNodeIdAndName

    Private _result As IResult
    Public Property Result() As IResult
        Get
            Return _result
        End Get

        Set(ByVal value As IResult)
            _result = value
        End Set
    End Property

    Public Sub New(result As IResult)
        MyBase.New(result.Name)

        Me.Result = result
        If Not String.IsNullOrEmpty(result.AutoName) Then
            Text = result.AutoName
        ElseIf Not String.IsNullOrEmpty(result.Name) Then
            Text = result.Name
        Else
            Text = "Documento insertado"
        End If
        NodeWFType = NodeWFTypes.insercion
        ImageIndex = 33
        'Me.SelectedImageIndex = 53
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
        ItemHeight = 25

    End Sub

End Class

Public Class StepStateNodeIdAndName
    Inherits BaseWFNode
    Implements IStepStateNodeIdAndName

#Region " Atributos "
    Private _wFStepid As Int64
    Private _wFStateid As Int64
    Private mTaskCount As Int64
#End Region

#Region " Propiedades "
    Public Property WFStepid() As Int64 Implements IStepStateNodeIdAndName.StepId
        Get
            Return _wFStepid
        End Get
        Set(ByVal value As Int64)
            _wFStepid = value
        End Set
    End Property
    Public Property StateId() As Int64 Implements IStepStateNodeIdAndName.StateStepId
        Get
            Return _wFStateid
        End Get
        Set(ByVal value As Int64)
            _wFStateid = value
        End Set
    End Property
    Public Property TasksCount() As Int64 Implements IStepStateNodeIdAndName.TasksCount
        Get
            Return mTaskCount
        End Get
        Set(ByVal value As Int64)
            mTaskCount = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByVal wfstepid As Int64, ByVal stateID As Int64, ByVal wfstateName As String, ByVal taskCount As Int32)
        MyBase.New(wfstateName)

        Me.WFStepid = wfstepid
        Me.StateId = stateID
        NodeWFType = NodeWFTypes.Estado
        Text = wfstateName & " (" & taskCount & ")"
        ImageIndex = 33
        'Me.SelectedImageIndex = 53
        TasksCount = taskCount
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor

    End Sub

    Public Sub New(ByVal wfstepid As Int64, ByVal stateID As Int64, ByVal wfstateName As String)
        MyBase.New(wfstateName)

        Me.WFStepid = wfstepid
        Me.StateId = stateID
        NodeWFType = NodeWFTypes.Estado
        Text = wfstateName
        ImageIndex = 33
        'Me.SelectedImageIndex = 53
        TasksCount = 0
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
    End Sub

#End Region

    Public Sub UpdateTasksCount(ByVal TaskCount As Int64) Implements IStepStateNodeIdAndName.UpdateTasksCount
        Try
            If TreeView.Disposing = False AndAlso Not IsNothing(Nodes) Then
                Dim index As Int32 = Text.LastIndexOf("(")
                If index <> -1 Then
                    Text = Text.Remove(index, Text.Length - index).TrimEnd & " (" & TaskCount & ")"
                Else
                    Text = Text & " (" & TaskCount & ")"
                End If
                TasksCount = TaskCount
            End If
        Catch ex As ObjectDisposedException
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class

Public Class TaskNode
    Inherits BaseWFNode
    Implements ITaskNode

#Region " Atributos "
    Private _result As ITaskResult
#End Region

#Region " Propiedades "
    Public Property Result() As ITaskResult Implements ITaskNode.Result
        Get
            Return _result
        End Get
        Set(ByVal value As ITaskResult)
            _result = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByRef Result As ITaskResult)
        MyBase.New(Result.Name)

        Me.Result = Result
        NodeWFType = NodeWFTypes.Tarea
        Text = Result.Name
        ImageIndex = Result.IconId
        'Me.SelectedImageIndex = Result.IconId
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
    End Sub

#End Region

End Class

Public Class EditStepNode
    Inherits BaseWFNode
    Implements IEditStepNode
#Region " Atributos "
    Private _wFStep As IWFStep
    Private _inputNode As RuleTypeNode
    Private _inputValidationNode As RuleTypeNode
    Private _outputNode As RuleTypeNode
    Private _outputValidationNode As RuleTypeNode
    Private _updateNode As RuleTypeNode
    Private _userActionNode As RuleTypeNode
    Private _scheduleNode As RuleTypeNode
    Private _rightNode As RightNode
    Private _floatingNode As RuleTypeNode
    Private _EventNode As RuleTypeNode
#End Region

#Region " Propiedades "
    Public Sub IsInitialStep(ByVal Value As Boolean) Implements IEditStepNode.IsInitialStep
        If Value Then
            Font = New Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold)
        Else
            Font = New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular)
        End If
    End Sub
    Public Property FloatingNode() As IRuleTypeNode Implements IEditStepNode.FloatingNode
        Get
            Return _floatingNode
        End Get
        Set(ByVal value As IRuleTypeNode)
            _floatingNode = DirectCast(value, RuleTypeNode)
        End Set
    End Property
    Public Property RightNode() As IRightNode Implements IEditStepNode.RightNode
        Get
            Return _rightNode
        End Get
        Set(ByVal value As IRightNode)
            _rightNode = DirectCast(value, RightNode)
        End Set
    End Property
    Public Property ScheduleNode() As IRuleTypeNode Implements IEditStepNode.ScheduleNode
        Get
            Return _scheduleNode
        End Get
        Set(ByVal value As IRuleTypeNode)
            _scheduleNode = DirectCast(value, RuleTypeNode)
        End Set
    End Property
    Public Property EventNode() As IRuleTypeNode Implements IEditStepNode.EventNode
        Get
            Return _EventNode
        End Get
        Set(ByVal value As IRuleTypeNode)
            _EventNode = DirectCast(value, RuleTypeNode)
        End Set
    End Property
    Public Property UpdateNode() As IRuleTypeNode Implements IEditStepNode.UpdateNode
        Get
            Return _updateNode
        End Get
        Set(ByVal value As IRuleTypeNode)
            _updateNode = DirectCast(value, RuleTypeNode)
        End Set
    End Property
    Public Property OutputValidationNode() As IRuleTypeNode Implements IEditStepNode.OutputValidationNode
        Get
            Return _outputValidationNode
        End Get
        Set(ByVal value As IRuleTypeNode)
            _outputValidationNode = DirectCast(value, RuleTypeNode)
        End Set
    End Property
    Public Property OutputNode() As IRuleTypeNode Implements IEditStepNode.OutputNode
        Get
            Return _outputNode
        End Get
        Set(ByVal value As IRuleTypeNode)
            _outputNode = DirectCast(value, RuleTypeNode)
        End Set
    End Property
    Public Property InputValidationNode() As IRuleTypeNode Implements IEditStepNode.InputValidationNode
        Get
            Return _inputValidationNode
        End Get
        Set(ByVal value As IRuleTypeNode)
            _inputValidationNode = DirectCast(value, RuleTypeNode)
        End Set
    End Property
    Public Property UserActionNode() As IRuleTypeNode Implements IEditStepNode.UserActionNode
        Get
            Return _userActionNode
        End Get
        Set(ByVal value As IRuleTypeNode)
            _userActionNode = DirectCast(value, RuleTypeNode)
        End Set
    End Property
    Public Property WFStep() As IWFStep Implements IEditStepNode.WFStep
        Get
            Return _wFStep
        End Get
        Set(ByVal value As IWFStep)
            _wFStep = value
        End Set
    End Property
    Public Property InputNode() As IRuleTypeNode Implements IEditStepNode.InputNode
        Get
            Return _inputNode
        End Get
        Set(ByVal value As IRuleTypeNode)
            _inputNode = DirectCast(value, RuleTypeNode)
        End Set
    End Property
#End Region

#Region " Constructores "

    Public Sub New(ByRef wfstep As WFStep, ByVal initialStep As IWFStep)
        MyBase.New(wfstep.Name)
        _rightNode = New RightNode(wfstep)
        Me.WFStep = wfstep
        NodeWFType = NodeWFTypes.Etapa
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor

        'Valido si es la etapa inicial
        If Me.WFStep Is initialStep Then
            IsInitialStep(True)
        Else
            IsInitialStep(False)
        End If

        Text = wfstep.Name & " (" & wfstep.ID & ")"
        ImageIndex = 27
        'Me.SelectedImageIndex = 53

        _inputNode = New RuleTypeNode(TypesofRules.Entrada, wfstep.ID)
        _inputValidationNode = New RuleTypeNode(TypesofRules.ValidacionEntrada, wfstep.ID)
        _outputNode = New RuleTypeNode(TypesofRules.Salida, wfstep.ID)
        _outputValidationNode = New RuleTypeNode(TypesofRules.ValidacionSalida, wfstep.ID)
        _updateNode = New RuleTypeNode(TypesofRules.Actualizacion, wfstep.ID)
        _userActionNode = New RuleTypeNode(TypesofRules.AccionUsuario, wfstep.ID)
        _scheduleNode = New RuleTypeNode(TypesofRules.Planificada, wfstep.ID)
        _rightNode = New RightNode(wfstep)
        _floatingNode = New RuleTypeNode(TypesofRules.Floating, wfstep.ID)
        _EventNode = New RuleTypeNode(TypesofRules.Eventos, wfstep.ID)

        Nodes.Add(_inputNode)
        Nodes.Add(_inputValidationNode)
        Nodes.Add(_outputNode)
        Nodes.Add(_outputValidationNode)
        Nodes.Add(_updateNode)
        Nodes.Add(_userActionNode)
        Nodes.Add(_scheduleNode)
        Nodes.Add(_EventNode)
        Nodes.Add(_rightNode)
        Nodes.Add(_floatingNode)
    End Sub

#End Region

End Class

Public Class RuleTypeNode
    Inherits BaseWFNode
    Implements IRuleTypeNode

#Region " Atributos "
    Private _ruleParentType As TypesofRules
    Private _stepId As Int64
#End Region

#Region " Propiedades "
    Public ReadOnly Property StepId() As Int64
        Get
            Return _stepId
        End Get
    End Property
    Public Property RuleParentType() As TypesofRules Implements IRuleTypeNode.RuleParentType
        Get
            Return _ruleParentType
        End Get
        Set(ByVal value As TypesofRules)
            _ruleParentType = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByVal currentRuleParentType As TypesofRules, stepid As Int64)
        MyBase.New("")
        _stepId = stepid
        NodeWFType = NodeWFTypes.TipoDeRegla

        'todo wf: las demas no cambian el nombre?
        Select Case currentRuleParentType
            Case TypesofRules.Entrada
                Text = "Entrada"
            Case TypesofRules.Salida
                Text = "Salida"
            Case TypesofRules.Actualizacion
                Text = "Actualizacion"
            Case TypesofRules.AccionUsuario
                Text = "Acción de Usuario"
            Case TypesofRules.Planificada
                Text = "Planificada"
            Case TypesofRules.ValidacionEntrada
                Text = "Validacion Entrada"
            Case TypesofRules.ValidacionSalida
                Text = "Validacion Salida"
            Case TypesofRules.Floating
                Text = "Reglas Generales"
            Case TypesofRules.Eventos
                Text = "Eventos de Zamba"
            Case Else
                Text = currentRuleParentType.ToString()
        End Select
        Name = Text
        ImageIndex = 28
        'Me.SelectedImageIndex = 53
        RuleParentType = currentRuleParentType
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
    End Sub
#End Region

    Public Sub UpdateUserActionNodeName(ByVal Name As String) Implements IRuleTypeNode.UpdateUserActionNodeName
        If String.IsNullOrEmpty(Name) Then
            Text = "Acción de Usuario"
        Else
            Text = Name
        End If
    End Sub
End Class

Public Class RuleNode
    Inherits BaseWFNode
    Implements IRuleNode, ICloneable

#Region " Atributos "
    Private _ruleid As Int64
    Private _rulename As String
    Private _ruleclass As String
    Private _ruleenabled As Boolean
    Private _ruletype As TypesofRules
    Private _wfStepId As Int64
    Private _childRuleNodes As Generic.List(Of IRuleNode) = Nothing
    Private _parentId As Int64
    Private _parentType As TypesofRules
    Private _parentNode As IBaseWFNode
    Private _iconid As Int64
#End Region

#Region " Propiedades "
    Public Property RuleType() As TypesofRules Implements IRuleNode.RuleType
        Get
            Return _ruletype
        End Get
        Set(ByVal value As TypesofRules)
            _ruletype = value
        End Set
    End Property
    Public Property ParentType() As TypesofRules Implements IRuleNode.ParentType
        Get
            Return _parentType
        End Get
        Set(ByVal value As TypesofRules)
            _parentType = value
        End Set
    End Property
    Public Property ParentNode() As IBaseWFNode Implements IRuleNode.ParentNode
        Get
            Return _parentNode
        End Get
        Set(ByVal value As IBaseWFNode)
            _parentNode = value
        End Set
    End Property
    Public Property RuleId() As Int64 Implements IRuleNode.RuleId
        Get
            Return _ruleid
        End Get
        Set(ByVal value As Int64)
            _ruleid = value
        End Set
    End Property
    Public Property IconId() As Int64 Implements IRuleNode.IconId
        Get
            Return _iconid
        End Get
        Set(ByVal value As Int64)
            _iconid = value
        End Set
    End Property
    Public Property ParentId() As Int64 Implements IRuleNode.ParentId
        Get
            Return _parentId
        End Get
        Set(ByVal value As Int64)
            _parentId = value
        End Set
    End Property
    Public Property WFStepId() As Int64 Implements IRuleNode.WFStepId
        Get
            Return _wfStepId
        End Get
        Set(ByVal value As Int64)
            _wfStepId = value
        End Set
    End Property
    Public Property RuleName() As String Implements IRuleNode.RuleName
        Get
            Return _rulename
        End Get
        Set(ByVal value As String)
            _rulename = value
        End Set
    End Property
    Public Property RuleClass() As String Implements IRuleNode.RuleClass
        Get
            Return _ruleclass
        End Get
        Set(ByVal value As String)
            _ruleclass = value
        End Set
    End Property
    Public Property RuleEnabled() As Boolean Implements IRuleNode.RuleEnabled
        Get
            Return _ruleenabled
        End Get
        Set(ByVal value As Boolean)
            _ruleenabled = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ruleId As Int64,
                   ruleName As String,
                   ruleClass As String,
                   ruleEnable As Boolean,
                   ruleType As TypesofRules,
                   ruleParentId As Int64,
                   ruleParentType As TypesofRules,
                   stepId As Int64,
                   iconId As Int32)
        MyBase.New(ruleName)
        BackColor = Color.White
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
        'Datos de la regla que el nodo representa
        NodeWFType = NodeWFTypes.Regla
        _ruleid = ruleId
        _rulename = ruleName
        _ruleclass = ruleClass
        _ruleenabled = ruleEnable
        _ruletype = ruleType
        _parentId = ruleParentId
        _parentType = ruleParentType
        _wfStepId = stepId
        _iconid = iconId

        ImageIndex = iconId
        'SelectedImageIndex = iconId
        Text = Trim(ruleName) & " (" & ruleId & ")"
        ToolTipText = ruleClass
        ForeColor = Color.Red
    End Sub
    Public Sub New(rule As WFRuleParent, parentId As Int32)
        Me.New(rule.ID,
                rule.Name,
                rule.RuleClass,
                rule.Enable,
                rule.RuleType,
                parentId,
                rule.ParentType,
                rule.WFStepId,
                rule.IconId)
    End Sub
    Public Sub New(rule As DsRules.WFRulesRow, iconId As Int32)
        Me.New(rule.Id,
                rule.Name,
                rule._Class,
                rule.Enable,
                rule.Type,
                rule.ParentId,
                rule.ParentType,
                rule.step_Id,
                iconId)
    End Sub

    Public Overloads Function Clone() As RuleNode
        'NO USAR CLONE DE TREENODE. IMPIDE EL USO DE COPIAR Y PEGAR EN EL ADMINISTRADOR.
        Dim copy As New RuleNode(RuleId, RuleName, RuleClass, RuleEnabled, RuleType, ParentId, ParentType, WFStepId, IconId)

        With copy
            'Miembros de TreeNode
            .BackColor = BackColor
            .Checked = Checked
            .ContextMenu = ContextMenu
            .ForeColor = ForeColor
            .ImageIndex = ImageIndex
            .ImageKey = ImageKey
            .Name = Name
            .Font = Font
            '.SelectedImageIndex = Me.SelectedImageIndex
            '.SelectedImageKey = Me.SelectedImageKey
            '.StateImageIndex = Me.StateImageIndex
            '.StateImageKey = Me.StateImageKey
            .Tag = Tag
            .Text = Text
            .ToolTipText = ToolTipText

            'Miembros de RuleNode
            .IconId = IconId
            .NodeWFType = NodeWFType
            .ParentId = ParentId
            .ParentType = ParentType
            .RuleClass = RuleClass
            .RuleEnabled = RuleEnabled
            .RuleId = RuleId
            .RuleName = RuleName
            .RuleType = RuleType
            .WFStepId = WFStepId
        End With

        Return copy
    End Function

    Public Shared Sub CloneChilds(ByVal baseNode As RuleNode, ByVal copyNode As RuleNode)
        For i As Int32 = 0 To baseNode.Nodes.Count - 1
            copyNode.Nodes.Add(DirectCast(baseNode.Nodes(i), RuleNode).Clone)

            If baseNode.Nodes(i).Nodes.Count > 0 Then
                CloneChilds(baseNode.Nodes(i), copyNode.Nodes(i))
            End If
        Next
    End Sub
#End Region

    Public Sub UpdateRuleNodeName(ByVal RuleId As Int64, ByVal RuleName As String) Implements IRuleNode.UpdateRuleNodeName
        If String.IsNullOrEmpty(RuleName) Then
            Text = "Regla"
        Else
            Text = RuleName & " (" & RuleId & ")"
        End If
    End Sub
End Class

Public Class RightNode
    Inherits BaseWFNode
    Implements IRightNode

#Region " Atributos "
    Private _wFStep As IWFStep
#End Region

#Region " Propiedades "
    Public Property WFStep() As IWFStep Implements IRightNode.WFStep
        Get
            Return _wFStep
        End Get
        Set(ByVal value As IWFStep)
            _wFStep = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByRef wfstep As IWFStep)
        MyBase.New(wfstep.Name)
        Me.WFStep = wfstep
        NodeWFType = NodeWFTypes.Permiso
        ImageIndex = 32
        ''Me.SelectedImageIndex = 53
        Text = "Permisos"
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
    End Sub
#End Region

End Class

Public Class MonitorStepNode
    Inherits BaseWFNode
    Implements IMonitorStepNode

#Region " Atributos "
    Private _wFStep As IWFStep
#End Region

#Region " Propiedades "
    Public Property WFStep() As IWFStep Implements IMonitorStepNode.WFStep
        Get
            Return _wFStep
        End Get
        Set(ByVal value As IWFStep)
            _wFStep = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByRef wfstep As IWFStep)
        MyBase.New(wfstep.Name)

        Me.WFStep = wfstep
        NodeWFType = NodeWFTypes.Etapa
        UpdateNodeText()
        Font = ZambaUIHelpers.GetTreeViewStepNodesFont
        ForeColor = ZambaUIHelpers.GetTreeViewStepNodesFontColor
    End Sub
#End Region

    Public Sub UpdateNodeText() Implements IMonitorStepNode.UpdateNodeText
        Try
            SetText()
        Catch ex As Exception
        End Try
    End Sub
    Delegate Sub DSetText()
    Private Sub SetText()
        Text = WFStep.Name & " (" & WFStep.TasksCount & ")"
    End Sub


End Class
