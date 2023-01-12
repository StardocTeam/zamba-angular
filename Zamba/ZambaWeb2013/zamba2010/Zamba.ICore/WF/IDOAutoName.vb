Public Interface IDoAutoName
    Inherits IRule

    'Property Seleccionid() As Boolean
    Property Seleccion() As String
    Property variabledocid() As String
    Property variabledoctypeid() As String
    Property nombreColumna() As String
    Property updateMultiple() As Boolean
    Property docTypeIds() As String
End Interface
