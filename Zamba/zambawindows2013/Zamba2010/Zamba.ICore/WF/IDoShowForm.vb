Imports Zamba.Core.Enumerators

Public Interface IDoShowForm
    Inherits IRule
    Property FormID() As Int64
    Property associatedDocDataShow() As Boolean
    Property varDocId() As String
    Property DontShowDialogMaximized() As Boolean
    Property RuleParentType() As TypesofRules
    Property ViewOriginal() As Boolean
    Property ViewAsociatedDocs() As Boolean
    Property RefreshForm() As Boolean
    Property ControlBox() As Boolean
    Property CloseFormWindowAfterRuleExecution() As Boolean
End Interface