Public Interface ITempTaskResult
    Property Result() As IResult
    Property AsignedToId() As Int64
    Property State() As IWFStepState
    Property TaskState() As Zamba.Core.TaskStates
End Interface