Imports Zamba.Core.Enumerators

Public Interface IRuleTypeNode
    Inherits IBaseWFNode
    Sub UpdateUserActionNodeName(ByVal Name As String)
    Property RuleParentType() As TypesofRules
End Interface

