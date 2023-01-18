Public Interface IIfDocAsocExist
    Inherits IRule
    Property TipoDeDocumento() As Int32
    Property Existencia() As Boolean
    Property Comparator() As Comparators
    Property Condiciones() As String
    Property IndexId() As Long
    Property OperatorAND() As Boolean
End Interface