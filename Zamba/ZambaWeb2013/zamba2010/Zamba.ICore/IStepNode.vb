Public Interface IStepNode
    Property WFStep() As IWFStep
    Sub UpdateTasksCount(ByVal WFStep As IWFStep)
End Interface