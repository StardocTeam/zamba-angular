Imports Zamba.Core.Enumerators

Public Interface IRule
    Inherits ICore

    ReadOnly Property RuleClass() As String
    Property RuleType() As TypesofRules
    Property ParentRule() As IRule
    Property ParentType() As TypesofRules
    Property Version() As Int32
    Property Enable() As Boolean
    Property IfType() As Boolean
    Property WFStepId() As Int64
    Property ChildRules() As Generic.List(Of IRule)
    Property ExecuteWhenResult() As Nullable(Of Boolean)
    Property AlertExecution() As Nullable(Of Boolean)
    Property RefreshRule() As Nullable(Of Boolean)
    Property ContinueWithError() As Nullable(Of Boolean)
    Property CloseTask() As Nullable(Of Boolean)
    Property Description() As String
    Property CleanRule() As Nullable(Of Boolean)
    Property StartTime() As Date
    Property Category() As Nullable(Of Int16)
    Property SaveUpdate() As Nullable(Of Boolean)
    Property UpdateComment() As String
    Property SaveUpdateInHistory() As Nullable(Of Boolean)
    Property Asynchronous() As Nullable(Of Boolean)
    Property MessageToShowInCaseOfError() As String
    Property ExecuteRuleInCaseOfError() As Nullable(Of Boolean)
    Property RuleIdToExecuteAfterError() As Nullable(Of Integer)
    Property ThrowExceptionIfCancel() As Nullable(Of Boolean)
    Property DisableChildRules() As Nullable(Of Boolean)
End Interface