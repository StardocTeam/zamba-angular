Imports Zamba.Core.Enumerators

Public Interface IRuleTypeNode
    Inherits IBaseWFNode
    Sub UpdateUserActionNodeName(ByRef wfrule As IWFRuleParent)
    Sub UpdateUserActionNodeName(ByVal Name As String)
    Property RuleParentType() As TypesofRules
    'Property Nodes() As System.Windows.Forms.TreeNode

End Interface

