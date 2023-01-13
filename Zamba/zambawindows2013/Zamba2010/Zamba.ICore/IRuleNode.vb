Public Interface IRuleNode
    Property RuleId As Int64
    Property RuleName As String
    Property RuleClass As String
    Property RuleEnabled As Boolean
    Property RuleType As Zamba.Core.Enumerators.TypesofRules
    Sub UpdateRuleNodeName(ByVal RuleId As Int64, ByVal RuleName As String)
    Property WFStepId As Int64
    Property ParentId As Int64
    Property ParentType As Zamba.Core.Enumerators.TypesofRules
    Property ParentNode As IBaseWFNode
    Property IconId As Int64
End Interface