Public Interface IDoEnableRule
    Inherits IRule
    Property SelectedRulesIDs() As String
    Property OnlyForTask() As Boolean
    Property RuleEstado() As Boolean
    Property RuleName() As String
    Property ViewAllSteps() As Boolean
    Property RuleEjecucion() As Boolean
End Interface
