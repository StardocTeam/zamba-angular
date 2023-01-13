''' <summary>
''' Agrupa una tarea para ejectuar reglas y el id de la regla para comenzar
''' </summary>
''' <remarks></remarks>
Public Class ExecutionRequest
    Implements IExecutionRequest

    Dim _currTask As ITaskResult
    Dim _startRule As Long

    Public Property ExecutionTask As ITaskResult Implements IExecutionRequest.ExecutionTask
        Get
            Return _currTask
        End Get
        Set(ByVal value As ITaskResult)
            _currTask = value
        End Set
    End Property

    Public Property StartRule As Long Implements IExecutionRequest.StartRule
        Get
            Return _startRule
        End Get
        Set(ByVal value As Long)
            _startRule = value
        End Set
    End Property
End Class
