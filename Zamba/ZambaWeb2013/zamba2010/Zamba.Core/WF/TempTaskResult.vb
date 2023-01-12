Public Class TempTaskResult
    Implements ITempTaskResult

#Region " Atributos "
    Private _result As IResult
    Private _asignedToId As Int64
    Private _state As IWFStepState
    Private _taskState As TaskStates
#End Region

#Region " Propiedades "
    Public Property Result() As IResult Implements ITempTaskResult.Result
        Get
            Return _result
        End Get
        Set(ByVal value As IResult)
            _result = value
        End Set
    End Property
    Public Property AsignedToId() As Int64 Implements ITempTaskResult.AsignedToid
        Get
            Return _asignedToId
        End Get
        Set(ByVal value As Int64)
            _asignedToId = value
        End Set
    End Property
    Public Property State() As IWFStepState Implements ITempTaskResult.State
        Get
            Return _state
        End Get
        Set(ByVal value As IWFStepState)
            _state = value
        End Set
    End Property
    Public Property TaskState() As TaskStates Implements ITempTaskResult.TaskState
        Get
            Return _taskState
        End Get
        Set(ByVal value As TaskStates)
            _taskState = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New(ByRef result As Result, ByVal userId As Int64, ByVal state As IWFStepState, ByVal taskState As TaskStates)
        _result = result
        _asignedToId = userId
        _state = state
        _taskState = taskState
    End Sub
#End Region
End Class