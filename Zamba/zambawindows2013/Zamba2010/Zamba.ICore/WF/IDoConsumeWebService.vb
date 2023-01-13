Public Interface IDoConsumeWebService
    Inherits IRule
    Property Wsdl() As String
    Property MethodName() As String
    Property Param() As ArrayList
    Property ParamTypes() As String
    Property SaveInValue() As String
    Property useCredentials() As Boolean
    Property ParamNT() As ArrayList
End Interface
