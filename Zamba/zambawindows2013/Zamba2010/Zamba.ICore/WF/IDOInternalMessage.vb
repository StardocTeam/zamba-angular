Public Interface IDOInternalMessage
    Inherits IRule
    Property ToStr() As String
    Property CCStr() As String
    Property CCOStr() As String
    Property Msg() As IInternalMessage
    Property OneDocPerMail() As Boolean
End Interface