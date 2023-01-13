Public Interface IDoDecodeFile
    Inherits IRule

    Property binary() As String
    Property path() As String
    Property fname() As String
    Property varpath() As String
    Property textstart() As String
    Property textend() As String
    Property extfile() As String
End Interface
