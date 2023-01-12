Public Interface IIfValidateVar
    Inherits IRule
    Property TxtVar() As String
    Property Operador() As Comparadores
    Property TxtValue() As String
    Property CaseInsensitive() As Boolean
End Interface