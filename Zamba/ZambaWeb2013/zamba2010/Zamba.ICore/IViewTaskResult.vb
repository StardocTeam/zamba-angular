Public Interface IViewTaskResult
    Property Id() As Int64
    Property ExpireDate() As Date
    Property State() As String
    Property TaskState() As Zamba.Core.TaskStates
    Property IsExpired() As Boolean
    Property Name() As String
    Property Selected() As Boolean
    Property WfStepName() As String
    Property ImageURL() As String
End Interface



