Imports System.Drawing
Imports System.Collections.Generic

<AttributeUsage(AttributeTargets.Class)> <Serializable()> _
Public Class WFStep
    Inherits ZambaCore
    Implements IWFStep, IDisposable

#Region " Atributos "
    Private _workflowId As Int64
    Private _description As String
    Private _DocumentsCount As Int32 = 0
    Private _MaxHours As Int32
    Private _MaxDocs As Int32
    Private _StartAtOpenDoc As Boolean
    'Private _WorkFlow As IWorkFlow = Nothing
    Private _States As List(Of IWFStepState) = Nothing
    Private _InitialState As IWFStepState = Nothing
    Private _Users As SortedList = Nothing
    Private _Groups As SortedList = Nothing
    Private _dsrules As DsRules = Nothing 'List(Of IWFRuleParent) = Nothing
    Private _isFull As Boolean
    Private _isLoaded As Boolean
    Private _expiredTasksCount As Int32
#End Region

#Region " Propiedades "
    Public Property WorkId() As Int64 Implements IWFStep.WorkId
        Get
            Return _workflowId
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
            If IsNothing(_InitialState) Then _InitialState = New WFStepState(0)
            Return _InitialState
        End Get
        Set(ByVal Value As IWFStepState)
            _InitialState = Value
        End Set
    End Property
    Public Property DsRules() As DsRules Implements IWFStep.DSRules
        Get
            If IsNothing(_dsrules) Then
                _dsrules = New DsRules
                _dsrules.CaseSensitive = False
                _dsrules.WFRuleParamItems.CaseSensitive = False
                _dsrules.WFRules.CaseSensitive = False
            End If

            Return _dsrules
        End Get
        Set(ByVal Value As DsRules)
            _dsrules = Value
            If _dsrules IsNot Nothing Then
                _dsrules.CaseSensitive = False
                _dsrules.WFRuleParamItems.CaseSensitive = False
                _dsrules.WFRules.CaseSensitive = False
            End If
        End Set
    End Property
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

        WorkId = workflowId
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
        IconId = imageIndex

    End Sub
    Public Sub New(ByVal stepId As Int32)
        ID = stepId
    End Sub
    Public Sub New()

    End Sub
#End Region

#Region " Eventos "
    Public Event CountOfDocumentsChanged() Implements IWFStep.CountOfDocumentsChanged
    Public Event SendMsg2Client(ByVal Action As String, ByVal DocId As Int64, ByVal DocTypeId As Int64, ByVal StepId As Int64) Implements IWFStep.SendMsg2Client
#End Region

    Public Sub raisemsg2Client(ByVal Action As String, ByVal DocId As Int64, ByVal DocTypeId As Int64, ByVal StepId As Int64) Implements IWFStep.raisemsg2Client
        RaiseEvent SendMsg2Client(Action, DocId, DocTypeId, StepId)
    End Sub

    Private _disposed As Boolean
    Public Overrides Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)

        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        'Para evitar que se haga dispose 2 veces
        If Not _disposed Then
            If disposing Then
                Dim i As Int16

                'Se comenta porque en algun lado esta pasado por referencia
                'If Not IsNothing(_rules) Then
                '    For i = 0 To _rules.Count - 1
                '        _rules(i).Dispose()
                '        _rules(i) = Nothing
                '    Next
                '    Me._rules.Clear()
                'End If

                'Se comenta porque en algun lado esta pasado por referencia
                'If Not IsNothing(_States) Then
                '    For i = 0 To _States.Count - 1
                '        _States(i).Dispose()
                '        _States(i) = Nothing
                '    Next
                '    Me._States.Clear()
                'End If
            End If

            ' Indicates that the instance has been disposed.
            _disposed = True

            'Se comenta porque en algun lado esta pasado por referencia
            'Me._rules = Nothing
            'Se comenta porque en algun lado esta pasado por referencia
            'Me._States = Nothing
        End If
    End Sub

    Public Overrides Sub FullLoad()
    End Sub
    Public Overrides Sub Load()

    End Sub
End Class