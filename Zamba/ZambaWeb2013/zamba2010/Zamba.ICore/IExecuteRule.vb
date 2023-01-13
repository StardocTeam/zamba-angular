Public Interface IExecuteRule
    Inherits IRule

    Property ExecuteWhenResult() As Nullable(Of Boolean)
    Property AlertExecution() As Nullable(Of Boolean)
End Interface
