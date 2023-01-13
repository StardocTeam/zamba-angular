Public Interface IStepNodeIdAndName
    Property StepId() As Int64
    Sub UpdateTasksCount(ByVal TaskCount As Int64)
    Property TasksCount() As Int64
End Interface

Public Interface ISearchNodeIdAndName
    Property Search As ISearch
    Sub UpdateTasksCount(ByVal TaskCount As Int64)
    Property SearchResults As DataTable
    Property TasksCount() As Int64
End Interface

Public Interface IStepStateNodeIdAndName
    Property StepId() As Int64
    Property StateStepId() As Int64
    Sub UpdateTasksCount(ByVal TaskCount As Int64)
    Property TasksCount() As Int64
End Interface