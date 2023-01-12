Public Interface IWFRuleParent
    Inherits IZambaCore, IRule

    'Property ParentType() As TypesofRules
    'Property RuleType() As TypesofRules
    'Property WFStep() As IWFStep
    'Property ParentRule() As IWFRuleParent
    Property RuleNode() As IRuleNode
    'Property ChildRules() As Generic.List(Of IWFRuleParent)
    'ReadOnly Property RuleClass() As String
    'Property Enable() As Boolean
    Property OldStateEnable() As Boolean
    Property OldStateTrue() As Boolean
    Property IsUI() As Boolean
    'Property Version() As Int32
    'Function Play(ByVal results As System.Collections.SortedList) As System.Collections.SortedList
    Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
    Function PlayWeb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
    'Function ExecuteRule(ByVal results As SortedList) As SortedList
    Function ExecuteRule(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal taskBusiness As IWFTaskBusiness, ByVal ruleBusiness As IWFRuleBusiness, ByVal IsAsync As Boolean) As System.Collections.Generic.List(Of ITaskResult)
    Function ExecuteWebRule(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal taskBusiness As IWFTaskBusiness, ByVal rulebusiness As IWFRuleBusiness, ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable, ByVal IsAsync As Boolean) As System.Collections.Generic.List(Of ITaskResult)
    'ReadOnly Property MaskName() As String
End Interface
