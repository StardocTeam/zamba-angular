Public Interface IDOAsk
    Inherits IRule

    Property Variable() As String
    Property Mensaje() As String
    Property ValorPorDefecto() As String
    Property AskOnetime As Boolean
    'Property tamaño() As Integer
End Interface
