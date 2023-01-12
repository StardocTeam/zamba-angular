Public Interface IDoConsumeWCF
    Inherits IRule
    Property Wsdl() As String
    Property MethodName() As String
    Property Param() As ArrayList
    Property ParamTypes() As String
    Property SaveInValue() As String
    Property useCredentials() As Boolean
    Property Contract() As String
End Interface
