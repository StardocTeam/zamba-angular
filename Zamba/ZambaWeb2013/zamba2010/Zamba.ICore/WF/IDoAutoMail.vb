Public Interface IDOAutoMail
    Inherits IRule
    Property Automail() As IAutoMail
    Property smtp() As ISMTP_Validada
    Property AddDocument() As Boolean
    Property AddLink() As Boolean
    Property AddIndexs() As Boolean
    Property IndexNames() As List(Of String)
    Property groupMailTo() As Boolean
End Interface
