Public Interface IWFTaskBusiness
    Function SetLastUpdate(ByVal Result As ITaskResult, ByVal comment As String, ByVal saveHistory As Boolean) As Int64
    Function GetTaskByTaskIdAndDocTypeIdAndStepId(ByVal taskId As Int64, ByVal DocTypeId As Int64, ByVal WFStepId As Int64, ByVal PageSize As Int32) As ITaskResult
    Function GetStepIDByTaskId(ByVal taskId As Int64) As Int64
End Interface