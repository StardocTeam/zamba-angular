Public Interface IIfTaskState
    Inherits IRule
    Property States() As String
    Property Comp() As Comparators
    ReadOnly Property SEPARADOR_INDICE() As String
End Interface
