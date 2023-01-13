Public Class RequestActionTask
    Implements IDisposable

#Region "Atributos"
    Private _isDisposed As Boolean = False
    Private _requestActionId As Int64
    Private _userId As Int64
    Private _stepId As Int64
    Private _taskId As Int64
    Private _ruleId As Int64
    Private _executionDate As DateTime
#End Region

#Region "Propiedades"
    Public Property UserID() As Int64
        Get
            Return _userId
        End Get
        Set(ByVal value As Int64)
            _userId = value
        End Set
    End Property
    Public Property StepId() As Int64
        Get
            Return _stepId
        End Get
        Set(ByVal value As Int64)
            _stepId = value
        End Set
    End Property
    Public Property TaskId() As Int64
        Get
            Return _taskId
        End Get
        Set(ByVal value As Int64)
            _taskId = value
        End Set
    End Property
    Public Property RuleId() As Int64
        Get
            Return _ruleId
        End Get
        Set(ByVal value As Int64)
            _ruleId = value
        End Set
    End Property

    Public Property ExecutionDate() As DateTime
        Get
            Return _executionDate
        End Get
        Set(ByVal value As DateTime)
            _executionDate = value
        End Set
    End Property
    Public ReadOnly Property IsExecuted() As Boolean
        Get
            Dim Executed As Boolean = False

            If Not IsNothing(_executionDate) Then
                Executed = True
            End If

            Return Executed
        End Get
    End Property
#End Region

#Region "Constructores"

    Public Sub New(ByVal taskId As Int64, ByVal stepId As Int64)
        _taskId = taskId
        _stepId = stepId

    End Sub
    Public Sub New(ByVal taskId As Int64, ByVal stepId As Int64, ByVal userId As Int64)
        Me.New(taskId, stepId)
        _userId = userId
    End Sub

    Public Sub New(ByVal taskId As Int64, ByVal stepId As Int64, ByVal userId As Int64, ByVal executedRuleId As Int64, ByVal executionDate As DateTime)
        Me.New(taskId, stepId, userId)
        _ruleId = executedRuleId
        _executionDate = executionDate
    End Sub
#End Region

#Region "Dispose"
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not _isDisposed Then
            If disposing Then

            End If
        End If
        _isDisposed = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
