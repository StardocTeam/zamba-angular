Imports Zamba.Core.Enumerators

Public Interface IRule
    Inherits ICore

    'Property ID() As Int64
    Property ParentType() As TypesofRules
    Property Version() As Int32
    Property RuleType() As TypesofRules
    ReadOnly Property RuleClass() As String
    Property Enable() As Boolean
    ' Property WFStep() As IWFStep
    Property WFStepId() As Int64
    '    Property ChildRules() As Generic.List(Of IRule)
    Property ChildRulesIds() As Generic.List(Of Int64)
    Property ParentRule() As IRule
    Property ExecuteWhenResult() As Nullable(Of Boolean)
    Property AlertExecution() As Nullable(Of Boolean)
    '[Ezequiel] 30/03/2009 Created
    Property RefreshRule() As Nullable(Of Boolean)
    '[Ezequiel] 05/06/2009 Created
    Property ContinueWithError() As Nullable(Of Boolean)
    '[Ezequiel] 22/06/2009 Created
    Property CloseTask() As Nullable(Of Boolean)
    '[Ezequiel] 02/09/2009 Created
    Property Description() As String
    '[Tomas] 16/09/2009 Created
    Property CleanRule() As Nullable(Of Boolean)
    '[Ezequiel] 18/09/09 - Created
    Property StartTime() As Date
    '[Ezequiel] 21/09/09 - Created
    'ReadOnly Property TraceTime() As System.Diagnostics.TextWriterTraceListener
    '[Marcelo] 13/10/2010 Created.
    Property Category() As Nullable(Of Int16)
    '[Marcelo] 26/11/2010 Created
    Property SaveUpdate() As Nullable(Of Boolean)
    '[Marcelo] 26/11/2010 Created
    Property UpdateComment() As String
    '[Marcelo] 26/11/2010 Created
    Property SaveUpdateInHistory() As Nullable(Of Boolean)
    '[Marcelo] 03/01/2011 Created
    Property Asynchronous() As Nullable(Of Boolean)
    '[AlejandroR] 13/01/2010 Created
    Property MessageToShowInCaseOfError() As String
    '[AlejandroR] 13/01/2010 Created
    Property ExecuteRuleInCaseOfError() As Nullable(Of Boolean)
    '[AlejandroR] 13/01/2010 Created
    Property RuleIdToExecuteAfterError() As Nullable(Of Integer)
    '[AlejandroR] 17/01/2010 Created
    Property ThrowExceptionIfCancel() As Nullable(Of Boolean)
    '[AlejandroR] 28/02/2011 Created
    Property DisableChildRules() As Nullable(Of Boolean)

    Property IsAsync() As Nullable(Of Boolean)
End Interface