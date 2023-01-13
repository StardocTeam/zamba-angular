Public Interface IDoConsumeRestApi
    Inherits IRule
    Property Url() As String
    Property JsonMessage() As String
    Property ResultVar() As String
    Property Method() As String
End Interface
