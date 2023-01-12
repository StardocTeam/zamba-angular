Imports System.Windows.Forms
Imports System.Drawing
Imports Zamba.Core.Enumerators

Public MustInherit Class BaseWFNode
    Inherits TreeNode
    Implements IBaseWFNode

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
    Public Property WorkFlow() As iWorkFlow Implements IInitNode.WorkFlow
        Get
            Return _workFlow
        End Get
        Set(ByVal value As iWorkFlow)
            _workFlow = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New()
        Me.NodeWFType = NodeWFTypes.Inicio
        Me.BackColor = Color.White
        Me.ImageIndex = 18
        Me.SelectedImageIndex = 18
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
    Public Property WorkFlow() As iWorkFlow Implements IWFNode.WorkFlow
        Get
            Return _workFlow
        End Get
        Set(ByVal value As iWorkFlow)
            _workFlow = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByVal WF As IWorkFlow)
        Me.WorkFlow = WF
        Me.NodeWFType = NodeWFTypes.WorkFlow
        Me.BackColor = Color.WhiteSmoke
        Me.Text = WF.Name
        Me.ImageIndex = 26
        Me.SelectedImageIndex = 26
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
        Me.WorkFlowid = WFId
        Me.NodeWFType = NodeWFTypes.WorkFlow
        Me.BackColor = Color.WhiteSmoke
        Me.Text = WFName
        Me.ImageIndex = 35
        Me.SelectedImageIndex = 36
    End Sub
    Public Sub New(ByVal WFId As Int64, ByVal WFName As String, ByVal taskcountparent As Integer)
        Me.WorkFlowId = WFId
        Me.NodeWFType = NodeWFTypes.WorkFlow
        Me.BackColor = Color.WhiteSmoke
        Me.Text = WFName & " (" & taskcountparent & ")"
        Me.ImageIndex = 35
        Me.SelectedImageIndex = 36
    End Sub

    Public Sub UpdateTasksCountparent(ByVal TaskCount As Int64)
        Try
            If not isnothing(me.treeview) andalso Me.TreeView.Disposing = False AndAlso Not IsNothing(Nodes) Then
                Dim index As Int32 = Me.Text.LastIndexOf("(")
                If index <> -1 Then
                    Me.Text = Me.Text.Remove(index, Me.Text.Length - index).TrimEnd & " (" & TaskCount & ")"
                Else
                    Me.Text = Me.Text & " (" & TaskCount & ")"
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
    Private _wFStep As iWFStep

#End Region

#Region " Propiedades "
    Public Property WFStep() As iWFStep Implements IStepNode.WFStep
        Get
            Return _wFStep
        End Get
        Set(ByVal value As iWFStep)
            _wFStep = value
        End Set
    End Property

#End Region

#Region " Constructores "
    Public Sub New(ByRef wfstep As IWFStep)
        Me.WFStep = wfstep
        Me.NodeWFType = NodeWFTypes.Etapa
        Me.BackColor = Color.White
        Me.Text = wfstep.Name & " (" & wfstep.TasksCount & ")"
        Me.ImageIndex = 33
        Me.SelectedImageIndex = 34
    End Sub

#End Region

    Public Sub UpdateTasksCount(ByVal WfStep As IWFStep) Implements IStepNode.UpdateTasksCount
        Try

            If Me.TreeView.Disposing = False AndAlso Not IsNothing(Nodes) Then
                'Me.Text = WFStep.Name & " (" & Me.Nodes.Count & ")"
                Me.Text = WfStep.Name & " (" & WfStep.TasksCount & ")"
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
        Me.WFStepid = wfstepid
        Me.NodeWFType = NodeWFTypes.Etapa
        Me.BackColor = Color.White
        Me.Text = wfstepName & " (" & taskCount & ")"
        Me.ImageIndex = 33
        Me.SelectedImageIndex = 34
        Me.TasksCount = taskCount
    End Sub

    Public Sub New(ByVal wfstepid As Int64, ByVal wfstepName As String)
        Me.WFStepid = wfstepid
        Me.NodeWFType = NodeWFTypes.Etapa
        Me.BackColor = Color.White
        Me.Text = wfstepName
        Me.ImageIndex = 33
        Me.SelectedImageIndex = 34
        Me.TasksCount = 0
    End Sub

#End Region



    Public Sub UpdateTasksCount(ByVal TaskCount As Int64) Implements IStepNodeIdAndName.UpdateTasksCount
        Try
            If Me.TreeView.Disposing = False AndAlso Not IsNothing(Nodes) Then
                Dim index As Int32 = Me.Text.LastIndexOf("(")
                If index <> -1 Then
                    Me.Text = Me.Text.Remove(index, Me.Text.Length - index).TrimEnd & " (" & TaskCount & ")"
                Else
                    Me.Text = Me.Text & " (" & TaskCount & ")"
                End If
                Me.TasksCount = TaskCount
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
    Public Property Result() As iTaskResult Implements ITaskNode.Result
        Get
            Return _result
        End Get
        Set(ByVal value As iTaskResult)
            _result = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByRef Result As ITaskResult)
        Me.Result = Result
        Me.NodeWFType = NodeWFTypes.Tarea
        Me.BackColor = Color.White
        Me.Text = Result.Name
        Me.ImageIndex = Result.IconId
        Me.SelectedImageIndex = Result.IconId
    End Sub

#End Region

End Class

Public Class EditStepNode
    Inherits BaseWFNode
    Implements IEditStepNode
#Region " Atributos "
    Private _wFStep As IWFStep
    Private _inputNode As New RuleTypeNode(TypesofRules.Entrada)
    Private _inputValidationNode As New RuleTypeNode(TypesofRules.ValidacionEntrada)
    Private _outputNode As New RuleTypeNode(TypesofRules.Salida)
    Private _outputValidationNode As New RuleTypeNode(TypesofRules.ValidacionSalida)
    Private _updateNode As New RuleTypeNode(TypesofRules.Actualizacion)
    Private _userActionNode As New RuleTypeNode(TypesofRules.AccionUsuario)
    Private _scheduleNode As New RuleTypeNode(TypesofRules.Planificada)
    Private _rightNode As New RightNode(WFStep)
    Private _floatingNode As New RuleTypeNode(TypesofRules.Floating)
    Private _EventNode As New RuleTypeNode(TypesofRules.Eventos)
#End Region

#Region " Propiedades "
    Public Sub IsInitialStep(ByVal Value As Boolean) Implements IEditStepNode.IsInitialStep
        If Value Then
            Me.NodeFont = New Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold)
        Else
            Me.NodeFont = New Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular)
        End If
    End Sub
    Public Property FloatingNode() As iRuleTypeNode Implements IEditStepNode.FloatingNode
        Get
            Return _floatingNode
        End Get
        Set(ByVal value As iRuleTypeNode)
            _floatingNode = DirectCast(value, RuleTypeNode)
        End Set
    End Property
    Public Property RightNode() As iRightNode Implements IEditStepNode.RightNode
        Get
            Return _rightNode
        End Get
        Set(ByVal value As IRightNode)
            _rightNode = DirectCast(value, RightNode)
        End Set
    End Property
    Public Property ScheduleNode() As iRuleTypeNode Implements IEditStepNode.ScheduleNode
        Get
            Return _scheduleNode
        End Get
        Set(ByVal value As iRuleTypeNode)
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
    Public Property UpdateNode() As iRuleTypeNode Implements IEditStepNode.UpdateNode
        Get
            Return _updateNode
        End Get
        Set(ByVal value As iRuleTypeNode)
            _updateNode = DirectCast(value, RuleTypeNode)
        End Set
    End Property
    Public Property OutputValidationNode() As iRuleTypeNode Implements IEditStepNode.OutputValidationNode
        Get
            Return _outputValidationNode
        End Get
        Set(ByVal value As iRuleTypeNode)
            _outputValidationNode = DirectCast(value, RuleTypeNode)
        End Set
    End Property
    Public Property OutputNode() As iRuleTypeNode Implements IEditStepNode.OutputNode
        Get
            Return _outputNode
        End Get
        Set(ByVal value As iRuleTypeNode)
            _outputNode = DirectCast(value, RuleTypeNode)
        End Set
    End Property
    Public Property InputValidationNode() As iRuleTypeNode Implements IEditStepNode.InputValidationNode
        Get
            Return _inputValidationNode
        End Get
        Set(ByVal value As iRuleTypeNode)
            _inputValidationNode = DirectCast(value, RuleTypeNode)
        End Set
    End Property
    Public Property UserActionNode() As iRuleTypeNode Implements IEditStepNode.UserActionNode
        Get
            Return _userActionNode
        End Get
        Set(ByVal value As iRuleTypeNode)
            _userActionNode = DirectCast(value, RuleTypeNode)
        End Set
    End Property
    Public Property WFStep() As iWFStep Implements IEditStepNode.WFStep
        Get
            Return _wFStep
        End Get
        Set(ByVal value As IWFStep)
            _wFStep = value
        End Set
    End Property
    Public Property InputNode() As iRuleTypeNode Implements IEditStepNode.InputNode
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
        Me.WFStep = wfstep
        Me.NodeWFType = NodeWFTypes.Etapa

        'Valido si es la etapa inicial
        If Me.WFStep Is initialStep Then
            IsInitialStep(True)
        Else
            IsInitialStep(False)
        End If

        Me.Text = wfstep.Name
        Me.ImageIndex = 27
        Me.SelectedImageIndex = 27

        Me.Nodes.Add(_inputNode)
        Me.Nodes.Add(_inputValidationNode)
        Me.Nodes.Add(_outputNode)
        Me.Nodes.Add(_outputValidationNode)
        Me.Nodes.Add(_updateNode)
        Me.Nodes.Add(_userActionNode)
        Me.Nodes.Add(_scheduleNode)
        Me.Nodes.Add(_EventNode)
        Me.Nodes.Add(_rightNode)
        Me.Nodes.Add(_floatingNode)
    End Sub

#End Region

End Class

Public Class RuleTypeNode
    Inherits BaseWFNode
    Implements IRuleTypeNode

#Region " Atributos "
    Private _ruleParentType As TypesofRules
#End Region

#Region " Propiedades "
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
    Public Sub New(ByVal RuleParentType As TypesofRules)
        Me.NodeWFType = NodeWFTypes.TipoDeRegla
        'todo wf: las demas no cambian el nombre?
        Select Case RuleParentType
            Case TypesofRules.Entrada
                Me.Text = "Entrada"
            Case TypesofRules.Salida
                Me.Text = "Salida"
            Case TypesofRules.Actualizacion
                Me.Text = "Actualizacion"
            Case TypesofRules.AccionUsuario
                UpdateUserActionNodeName("Acción de Usuario")
            Case TypesofRules.Planificada
                Me.Text = "Planificada"
            Case TypesofRules.ValidacionEntrada
                Me.Text = "Validacion Entrada"
            Case TypesofRules.ValidacionSalida
                Me.Text = "Validacion Salida"
            Case TypesofRules.Floating
                Me.Text = "Reglas Generales"
            Case TypesofRules.Eventos
                Me.Text = "Eventos de Zamba"
        End Select
        Me.Text = Text
        Me.ImageIndex = 28
        Me.SelectedImageIndex = 28
        Me.RuleParentType = RuleParentType
        Me.NodeFont = New Font(FontFamily.GenericSansSerif, 7, FontStyle.Regular)
    End Sub
#End Region

    Public Sub UpdateUserActionNodeName(ByRef wfrule As IWFRuleParent) Implements IRuleTypeNode.UpdateUserActionNodeName
        If wfrule Is Nothing Then
            Me.Text = "Acción de Usuario"
        Else
            If Me.Text.StartsWith("Acción de Usuario") Then
                Me.Text = "Acción de Usuario" & " - " & wfrule.Name
            End If
        End If
    End Sub

    Public Sub UpdateUserActionNodeName(ByVal Name As String) Implements IRuleTypeNode.UpdateUserActionNodeName
        If String.IsNullOrEmpty(Name) Then
            Me.Text = "Acción de Usuario"
        Else
            Me.Text = Name
        End If
    End Sub
End Class

Public Class RuleNode
    Inherits BaseWFNode
    Implements IRuleNode

#Region " Atributos "
    Private _rule As IWFRuleParent
#End Region

#Region " Propiedades "
    Public Property Rule() As IWFRuleParent Implements IRuleNode.Rule
        Get
            Return _rule
        End Get
        Set(ByVal value As IWFRuleParent)
            _rule = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByRef rule As WFRuleParent)
        Me.NodeWFType = NodeWFTypes.Regla
        Me.Rule = rule
        If rule IsNot Nothing Then
            If Me.Rule.Enable Then
                Me.ImageIndex = 31
                Me.SelectedImageIndex = 31
            Else
                Me.ImageIndex = 37
                Me.SelectedImageIndex = 37
            End If

            Me.Text = Trim(rule.Name)
            Me.ForeColor = Color.Red
            rule.RuleNode = Me
        End If

    End Sub
#End Region

    Public Sub UpdateRuleNodeName(ByRef wfrule As IWFRuleParent) Implements IRuleNode.UpdateRuleNodeName
        If wfrule Is Nothing OrElse String.IsNullOrEmpty(wfrule.Name) Then
            Me.Text = "Regla"
        Else
            Me.Text = wfrule.Name
        End If
    End Sub

End Class

Public Class FloatingNode
    Inherits RuleNode
    Implements IFloatingNode


#Region " Atributos "
    Private _rule As IWFRuleParent
#End Region

#Region " Propiedades "
    Public Overloads Property Rule() As IWFRuleParent Implements IFloatingNode.Rule
        Get
            Return _rule
        End Get
        Set(ByVal value As IWFRuleParent)
            _rule = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByRef rule As IWFRuleParent)
        MyBase.New(DirectCast(rule, WFRuleParent))
        Me.NodeWFType = NodeWFTypes.FloatingRule
        Me.ImageIndex = rule.IconId
        Me.SelectedImageIndex = rule.IconId
        Me.Rule = rule
        Me.Text = Trim(rule.Name)
        Me.ForeColor = Color.Red
        rule.RuleNode = Me
    End Sub
#End Region

    'Todo Andres: A este metodo se le saco el overloads porquwe no implementaba la interfaz - Marcelo
    Public Sub UpdateRuleNodeName(ByRef wfrule As IWFRuleParent) Implements IFloatingNode.UpdateRuleNodeName
        If wfrule Is Nothing OrElse String.IsNullOrEmpty(wfrule.Name) Then
            Me.Text = "Regla"
        Else
            Me.Text = wfrule.Name
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
        Me.WFStep = wfstep
        Me.NodeWFType = NodeWFTypes.Permiso
        Me.ImageIndex = 32
        Me.SelectedImageIndex = 32
        Me.Text = "Permisos"
        Me.NodeFont = New Font(FontFamily.GenericSansSerif, 7, FontStyle.Bold)
    End Sub
#End Region



End Class

#Region "Monitor WF"
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
        Me.WFStep = wfstep
        Me.NodeWFType = NodeWFTypes.Etapa
        UpdateNodeText()
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
        Me.Text = WFStep.Name & " (" & WFStep.TasksCount & ")"
    End Sub


End Class
#End Region


'Public Class RuleActionNode
'    Inherits BaseWFNode
'    Public RuleAction As WFRuleParent
'    Public Sub New(ByVal RuleAction As WFRuleParent)
'        Me.NodeWFType = NodeWFTypes.ReglaAccion
'        Me.RuleAction = RuleAction
'        Me.ImageIndex = 30
'        Me.SelectedImageIndex = 30
'        Me.ForeColor = Color.Black
'        Me.NodeFont = New Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold)
'        Me.Text = Trim(RuleAction.MaskName)
'    End Sub
'End Class