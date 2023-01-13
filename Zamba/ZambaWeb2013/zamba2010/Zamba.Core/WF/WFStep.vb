Imports System.Drawing
Imports System.Collections.Generic
Imports System.Xml.Serialization

<AttributeUsage(AttributeTargets.Class)> <Serializable()> _
Public Class WFStep
    Inherits ZambaCore
    Implements IWFStep


#Region " Atributos "
    Private _workflowId As Int64
    Private _description As String
    Private _location As Point = Nothing
    Private _CreateDate As Date = Date.Now()
    Private _EditDate As Date = Date.Now()
    Private _Help As String = String.Empty
    Private _DocumentsCount As Int32 = 0
    Private _MaxHours As Int32
    Private _MaxDocs As Int32
    Private _StartAtOpenDoc As Boolean
    'Private _WorkFlow As IWorkFlow = Nothing
    Private _States As List(Of IWFStepState) = Nothing
    Private _InitialState As IWFStepState = Nothing
    Private _Users As SortedList = Nothing
    Private _Groups As SortedList = Nothing
    Private _color As String = String.Empty
    Private _Width As Int32
    Private _Height As Int32
    Private _tasks As List(Of ITaskResult) = Nothing
    Private _RuleTareaIniciada As IWFRuleParent = Nothing
    Private _RuleTareaFinalizada As IWFRuleParent = Nothing
    Private _RuleTareaDerivada As IWFRuleParent = Nothing
    Private _RuleTareaRechazada As IWFRuleParent = Nothing
    Private _rules As List(Of IWFRuleParent) = Nothing
    Private _isFull As Boolean
    Private _isLoaded As Boolean
    Private _tasksCount As Int32
    Private _expiredTasksCount As Int32
#End Region

#Region " Propiedades "
    Public Property WorkId() As Int64 Implements IWFStep.WorkId
        Get
            'If _workflowId = 0 Then
            '    If Not IsNothing(WorkFlow) Then
            '        Return WorkFlow.ID
            '    Else
            '        Return 0
            '    End If
            'Else
            Return _workflowId
            'End If
        End Get
        Set(ByVal value As Int64)
            _workflowId = value
        End Set
    End Property
    Public Property Description() As String Implements IWFStep.Description
        Get
            Return _description
        End Get
        Set(ByVal Value As String)
            _description = Value
        End Set
    End Property
    Public Property Location() As Point Implements IWFStep.Location
        Get
            'If IsNothing(_location) Then CallForceLoad(Me)
            If IsNothing(_location) Then _location = New Point()

            Return _location
        End Get
        Set(ByVal Value As Point)
            _location = Value
        End Set
    End Property
    Public Property color() As String Implements IWFStep.color
        Get
            Return _color
        End Get
        Set(ByVal Value As String)
            _color = Value
        End Set
    End Property
    Public Property Width() As Int32 Implements IWFStep.Width
        Get
            Return _Width
        End Get
        Set(ByVal Value As Int32)
            _Width = Value
        End Set
    End Property
    Public Property Height() As Int32 Implements IWFStep.Height
        Get
            Return _Height
        End Get
        Set(ByVal Value As Int32)
            _Height = Value
        End Set
    End Property
    Public Property CreateDate() As Date Implements IWFStep.CreateDate
        Get
            Return _CreateDate
        End Get
        Set(ByVal Value As Date)
            _CreateDate = Value
        End Set
    End Property
    Public Property EditDate() As Date Implements IWFStep.EditDate
        Get
            Return _EditDate
        End Get
        Set(ByVal Value As Date)
            _EditDate = Value
        End Set
    End Property
    Public Property Help() As String Implements IWFStep.Help
        Get
            Return _Help
        End Get
        Set(ByVal Value As String)
            _Help = Value
        End Set
    End Property
    '<PropiedadesType(Propiedades.PropiedadPublica)> _
    '    Public ReadOnly Property TiempoMaximo() As Decimal
    '    Get
    '        Return MaxHours
    '    End Get
    'End Property
    Public Property MaxHours() As Decimal Implements IWFStep.MaxHours
        Get
            Return _MaxHours
        End Get
        Set(ByVal Value As Decimal)
            _MaxHours = Convert.ToInt32(Value)
        End Set
    End Property
    Public Property MaxDocs() As Integer Implements IWFStep.MaxDocs
        Get
            Return _MaxDocs
        End Get
        Set(ByVal Value As Integer)
            _MaxDocs = Value
        End Set
    End Property
    Public Property StartAtOpenDoc() As Boolean Implements IWFStep.StartAtOpenDoc
        Get
            Return _StartAtOpenDoc
        End Get
        Set(ByVal Value As Boolean)
            _StartAtOpenDoc = Value
        End Set
    End Property
    Public Property DocumentsCount() As Int32 Implements IWFStep.DocumentsCount
        Get
            Return _DocumentsCount
        End Get
        Set(ByVal Value As Int32)
            _DocumentsCount = Value
            RaiseEvent CountOfDocumentsChanged()
        End Set
    End Property

    'Public Property WorkFlow() As IWorkFlow Implements IWFStep.WorkFlow
    '    Get
    '        'If IsNothing(_WorkFlow) Then CallForceLoad(Me)
    '        If IsNothing(_WorkFlow) Then _WorkFlow = New WorkFlow()

    '        Return _WorkFlow
    '    End Get
    '    Set(ByVal Value As IWorkFlow)
    '        _WorkFlow = Value
    '    End Set
    'End Property

    Public Property States() As List(Of IWFStepState) Implements IWFStep.States
        Get
            'If IsNothing(_States) Then CallForceLoad(Me)
            If IsNothing(_States) Then _States = New List(Of IWFStepState)()

            Return _States
        End Get
        Set(ByVal Value As List(Of IWFStepState))
            _States = Value
        End Set
    End Property
    Public Property InitialState() As IWFStepState Implements IWFStep.InitialState
        Get
            'If IsNothing(_InitialState) Then CallForceLoad(Me)
            If IsNothing(_InitialState) Then _InitialState = New WFStepState(0)

            Return _InitialState
        End Get
        Set(ByVal Value As IWFStepState)
            _InitialState = Value
        End Set
    End Property
    Public Property RuleTareaIniciada() As IWFRuleParent Implements IWFStep.RuleTareaIniciada
        Get
            'If IsNothing(_RuleTareaIniciada) Then CallForceLoad(Me)
            'If IsNothing(_RuleTareaIniciada) Then _RuleTareaIniciada = New 

            Return _RuleTareaIniciada
        End Get
        Set(ByVal Value As IWFRuleParent)
            _RuleTareaIniciada = Value
        End Set
    End Property
    Public Property RuleTareaFinalizada() As IWFRuleParent Implements IWFStep.RuleTareaFinalizada
        Get
            'If IsNothing(_RuleTareaFinalizada) Then CallForceLoad(Me)
            Return _RuleTareaFinalizada
        End Get
        Set(ByVal Value As IWFRuleParent)
            _RuleTareaFinalizada = Value
        End Set
    End Property
    Public Property RuleTareaDerivada() As IWFRuleParent Implements IWFStep.RuleTareaDerivada
        Get
            'If IsNothing(_RuleTareaDerivada) Then CallForceLoad(Me)
            Return _RuleTareaDerivada
        End Get
        Set(ByVal Value As IWFRuleParent)
            _RuleTareaDerivada = Value
        End Set
    End Property
    Public Property RuleTareaRechazada() As IWFRuleParent Implements IWFStep.RuleTareaRechazada
        Get
            'If IsNothing(_RuleTareaRechazada) Then CallForceLoad(Me)
            Return _RuleTareaRechazada
        End Get
        Set(ByVal Value As IWFRuleParent)
            _RuleTareaRechazada = Value
        End Set
    End Property
    Public Property Rules() As List(Of IWFRuleParent) Implements IWFStep.Rules
        Get
            'If IsNothing(_rules) Then CallForceLoad(Me)
            If IsNothing(_rules) Then _rules = New List(Of IWFRuleParent)()

            Return _rules
        End Get
        Set(ByVal Value As List(Of IWFRuleParent))
            _rules = Value
        End Set
    End Property
    'Public Property ExpiredTasksCount() As Int32 Implements IWFStep.ExpiredTasksCount
    '    Get
    '        Return _expiredTasksCount
    '    End Get
    '    Set(ByVal value As Int32)
    '        _expiredTasksCount = value
    '    End Set
    'End Property
    'Public Property TasksCount() As Int32 Implements IWFStep.TasksCount
    '    Get
    '        Return _tasksCount
    '    End Get
    '    Set(ByVal value As Int32)
    '        _tasksCount = value
    '    End Set
    'End Property
    'Private _RuleInput As New SortedList
    'Public Property RuleInput() As SortedList
    '    Get
    '        Return _RuleInput
    '    End Get
    '    Set(ByVal Value As SortedList)
    '        _RuleInput = Value
    '    End Set
    'End Property
    'Private _RuleOutput As New SortedList
    'Public Property RuleOutput() As SortedList
    '    Get
    '        Return _RuleOutput
    '    End Get
    '    Set(ByVal Value As SortedList)
    '        _RuleOutput = Value
    '    End Set
    'End Property
    'Private _RuleUpdate As New SortedList
    'Public Property RuleUpdate() As SortedList
    '    Get
    '        Return _RuleUpdate
    '    End Get
    '    Set(ByVal Value As SortedList)
    '        _RuleUpdate = Value
    '    End Set
    'End Property
    'Private _RuleUserAction As New SortedList
    'Public Property RuleUserAction() As SortedList
    '    Get
    '        Return _RuleUserAction
    '    End Get
    '    Set(ByVal Value As SortedList)
    '        _RuleUserAction = Value
    '    End Set
    'End Property
    'Private _RuleSchedule As New SortedList
    'Public Property RuleSchedule() As SortedList
    '    Get
    '        Return _RuleSchedule
    '    End Get
    '    Set(ByVal Value As SortedList)
    '        _RuleSchedule = Value
    '    End Set
    'End Property
    <PropiedadesType(Propiedades.PropiedadPublica)> _
Public ReadOnly Property TasksExpired() As ArrayList Implements IWFStep.TasksExpired
    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property
    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property

    Property ExpiredTasksCount() As Int32 Implements IWFStep.ExpiredTasksCount
        Get
            Return _expiredTasksCount
        End Get
        Set(ByVal value As Int32)
            _expiredTasksCount = value
        End Set
    End Property
    Property TasksCount() As Int32 Implements IWFStep.TasksCount
        Get
            Return _tasksCount
        End Get
        Set(ByVal value As Int32)
            _tasksCount = value
        End Set
    End Property
#End Region

#Region " Constructores "
    'Private Sub New(ByVal id As Int32, ByVal name As String, ByVal height As Int32, ByVal width As Int32, ByVal color As String, ByVal startAtOpenDoc As Boolean, ByVal maxDocs As Int32, ByVal maxHours As Int32)
    '    Me.ID = id
    '    Me.Name = name
    '    Me.Height = height
    '    Me.Width = width
    '    Me.color = color
    '    Me.StartAtOpenDoc = startAtOpenDoc
    '    Me.MaxHours = MaxHours
    '    Me.MaxDocs = MaxDocs
    'End Sub

    'Public Sub New(ByVal workflow As IWorkFlow, ByVal id As Int32, ByVal name As String, ByVal help As String, ByVal description As String, ByVal location As Point, ByVal imageIndex As Int32, ByVal maxDocs As Int32, ByVal maxHours As Int32, ByVal startAtOpenDoc As Boolean, ByVal color As String, ByVal width As Int32, ByVal height As Int32)
    '    Me.New(id, name, height, width, color, startAtOpenDoc, maxDocs, maxHours)

    '    'Me.WorkFlow = workflow

    '    Me.Help = help
    '    Me.Description = description
    '    Me.Location = location
    '    Me.IconId = imageIndex
    '    Me.MaxHours = maxHours
    '    Me.MaxDocs = maxDocs
    '    Me.StartAtOpenDoc = startAtOpenDoc

    '    'Me.ImagePath = ImagePath
    'End Sub
    'Public Sub New(ByVal workflow As IWorkFlow, ByVal id As Int32, ByVal name As String, ByVal description As String, ByVal location As Point, ByVal refreshrate As Int32, ByVal maxDocs As Int32, ByVal maxHours As Int32, ByVal startAtOpenDoc As Boolean, ByVal color As String, ByVal width As Int32, ByVal height As Int32)
    '    Me.New(id, name, height, width, color, startAtOpenDoc, maxDocs, maxHours)

    '    'Me.WorkFlow = workflow

    '    Me.Description = description
    '    Me.Location = location
    '    Me.MaxHours = maxHours
    '    Me.MaxDocs = maxDocs

    '    'Me.ImagePath = ImagePath
    'End Sub
    Public Sub New(ByVal workflowId As Int32, ByVal id As Int32, ByVal name As String, ByVal help As String, ByVal description As String, ByVal location As Point, ByVal imageIndex As Int32, ByVal maxDocs As Int32, ByVal maxHours As Int32, ByVal startAtOpenDoc As Boolean, ByVal color As String, ByVal width As Int32, ByVal height As Int32, ByVal myVal As Int32, ByVal myVal2 As Int32)
        ' Me.New(id, name, height, width, color, startAtOpenDoc, maxDocs, maxHours)

        Me.WorkId = workflowId
        Me.ID = id
        Me.Name = name
        Me.Height = height
        Me.Width = width
        Me.color = color
        Me.StartAtOpenDoc = startAtOpenDoc
        Me.MaxHours = maxHours
        Me.MaxDocs = maxDocs

        Me.Help = help
        Me.Description = description
        Me.Location = location
        Me.IconId = imageIndex

    End Sub
    Public Sub New(ByVal stepId As Int32)
        Me.ID = stepId
    End Sub
    Public Sub New()

    End Sub
#End Region

#Region " Eventos "
    Public Event CountOfDocumentsChanged() Implements IWFStep.CountOfDocumentsChanged
    Public Event SendMsg2Client(ByVal Action As String, ByVal DocId As Int64, ByVal DocTypeId As Int64, ByVal StepId As Int64) Implements IWFStep.SendMsg2Client
#End Region

    'Private _ImagePath As String
    'Public Property ImagePath() As String
    '    Get
    '        Return _ImagePath
    '    End Get
    '    Set(ByVal Value As String)
    '        _ImagePath = Value
    '    End Set
    'End Property

    Public Sub raisemsg2Client(ByVal Action As String, ByVal DocId As Int64, ByVal DocTypeId As Int64, ByVal StepId As Int64) Implements IWFStep.raisemsg2Client
        RaiseEvent SendMsg2Client(Action, DocId, DocTypeId, StepId)
    End Sub
    Public Overrides Sub Dispose()

    End Sub
    Public Overrides Sub FullLoad()
    End Sub
    Public Overrides Sub Load()

    End Sub

    '    <Serializable()> _
    '    Public Class State
    '        Inherits ZClass
    '        Implements IWFStepState

    '#Region " Atributos "
    '        Private _id As Int32
    '        Private _name As String
    '        Private _description As String
    '        Private _initial As Boolean
    '#End Region

    '#Region " Propiedades "
    '        Public Property Id() As Int64 Implements IWFStepState.ID
    '            Get
    '                Return _id
    '            End Get
    '            Set(ByVal Value As Int64)
    '                _id = Value
    '            End Set
    '        End Property
    '        <PropiedadesType(Propiedades.PropiedadPublica)> _
    '        Public Property Name() As String Implements IWFStepState.Name
    '            Get
    '                Return _name
    '            End Get
    '            Set(ByVal Value As String)
    '                _name = Value
    '            End Set
    '        End Property
    '        <PropiedadesType(Propiedades.PropiedadPublica)> _
    '        Public Property Description() As String Implements IWFStepState.Description
    '            Get
    '                Return _description
    '            End Get
    '            Set(ByVal Value As String)
    '                _description = Value
    '            End Set
    '        End Property
    '        <PropiedadesType(Propiedades.PropiedadPublica)> _
    '        Public Property Initial() As Boolean Implements IWFStepState.Initial
    '            Get
    '                Return _initial
    '            End Get
    '            Set(ByVal Value As Boolean)
    '                _initial = Value
    '            End Set
    '        End Property
    '#End Region

    '#Region " Constructores "
    '        Public Sub New(ByVal id As Int32)
    '            Me.Id = id
    '            Me.Name = String.Empty
    '            Me.Description = String.Empty
    '        End Sub
    '        Public Sub New(ByVal id As Int32, ByVal name As String, ByVal description As String, ByVal initial As Boolean)
    '            Me.New(id)
    '            Me.Id = id
    '            Me.Name = name
    '            Me.Description = description
    '            Me.Initial = initial
    '        End Sub
    '#End Region

    '        Public Overrides Sub Dispose()
    '        End Sub
    '    End Class

End Class

