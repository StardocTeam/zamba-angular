Public Interface IWFTaskBusiness
    Function SetLastUpdate(ByVal Result As ITaskResult, ByVal comment As String, ByVal saveHistory As Boolean, userId As Int64, details As String) As Int64
    Function GetTaskByTaskIdAndDocTypeIdAndStepId(ByVal taskId As Int64, ByVal DocTypeId As Int64, ByVal WFStepId As Int64, ByVal PageSize As Int32) As ITaskResult
    Function GetStepIDByTaskId(ByVal taskId As Int64) As Int64
    Function GetStepIdDocTypeIdByTaskId(ByVal taskId As Int64) As DataSet
End Interface

Public Interface IWFRuleBusiness
    Function GetChildRulesIds(ByVal RuleId As Int64) As List(Of Int64)
    Function GetWFStepIdbyRuleID(ByVal RuleId As Int64) As Int64

    Function GetInstanceRuleById(ByVal RuleId As Int64) As IWFRuleParent
End Interface
