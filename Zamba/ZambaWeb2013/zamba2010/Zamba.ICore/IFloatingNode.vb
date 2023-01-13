Public Interface IFloatingNode
    Property Rule() As IWFRuleParent
    Sub UpdateRuleNodeName(ByRef wfrule As IWFRuleParent)
End Interface