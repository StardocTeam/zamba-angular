Public Interface IIfIndex
    Inherits IRule
    Property Comparator() As Comparators
    Property Condiciones() As String
    Property IndexId() As Long
    'Property Valor() As String
    Property Variable() As String
    'Agregado MNP
    Property OperatorAND() As Boolean
End Interface
