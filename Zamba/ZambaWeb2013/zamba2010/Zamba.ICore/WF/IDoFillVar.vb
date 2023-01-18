Public Interface IDoFillVar
    Inherits IRule
    Property variableName() As String
    Property variableValue() As String
    Property useConc() As Boolean
    Property concValue() As String
End Interface