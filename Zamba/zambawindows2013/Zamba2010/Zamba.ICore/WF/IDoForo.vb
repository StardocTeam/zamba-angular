Public Interface IDoForo
    Inherits IRule
    Property Subject() As String
    Property Body() As String
    Property IdMensaje() As String
    Property Participantes() As String
    Property Automatic() As Boolean
    'ExecuteRule Configuration
    Property ExecuteRuleID() As Int64
    Property BtnName() As String
End Interface
