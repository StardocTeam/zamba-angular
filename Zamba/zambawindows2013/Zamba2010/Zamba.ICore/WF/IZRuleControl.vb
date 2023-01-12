Public Interface IZRuleControl
    ReadOnly Property MyRule() As IRule
    Event UpdateMaskName(ByRef rule As IRule)
    Event UpdateRuleIcon(ByVal rule As IRule)
    Event GoToErrorRule(ByVal Sender As Object, ByVal e As EventArgs)
    ReadOnly Property GoToErrorRuleComboBox As ComboBox

    Property HasBeenModified As Boolean
End Interface

