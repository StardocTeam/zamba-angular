Public Interface IDoFillIndex
    Inherits IRule
    Property IndexId() As String
    Property PrimaryValue() As String
    Property SecondaryValue() As String
    Property OverWriteIndex() As String
    Sub ClearRule()
End Interface