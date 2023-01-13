Public Interface IDoForEach
    Inherits IRule
    Property Value() As String
    Property ContinueIterations As Boolean
    Property SplitText As Boolean
    Property SplitChar As String
    Property SplitType As SplitType
End Interface