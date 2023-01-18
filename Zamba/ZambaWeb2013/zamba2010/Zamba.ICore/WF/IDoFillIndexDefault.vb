Public Interface IDoFillIndex
    Inherits IRule
    Property Index() As IIndex
    Property IndexId() As String
    Property PrimaryValue() As String
    Property SecondaryValue() As String
    Property OverWriteIndex() As String
    Sub ClearRule()
End Interface