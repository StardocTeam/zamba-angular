Public Interface IDOGenerateTaskResult
    Inherits IRule
    Property docTypeId() As Int64
    Property indices() As String
    Property addCurrentwf() As Boolean
    Property ContinueWithCurrentTasks() As Boolean
    Property FilePath() As String
    Property DontOpenTaskAfterInsert() As Boolean
    Property AutocompleteIndexsInCommon() As Boolean
    Property SpecificWorkflowId As Int64

    Property OpenMode As Int32

End Interface