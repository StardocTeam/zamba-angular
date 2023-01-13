Public Interface IZRuleControl
    ReadOnly Property MyRule() As IRule 'IWFRuleParent
    Event UpdateMaskName(ByRef rule As IRule) 'IWFRuleParent)
    Event UpdateRuleIcon(ByVal rule As IRule, ByVal IsUpdate As Boolean)
End Interface

