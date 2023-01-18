Imports Zamba.Core
Public Interface IIfDates
    Inherits IRule
    Property Comparador() As Comparadores
    Property TipoComparacion() As TipoComparaciones
    Property MiFecha() As DocumentDates
    Property FechaDocumentoComparar() As DocumentDates
    Property FechaAComparar() As DateTime
    Property CantidadDias() As Int32
    Property CantidadHoras() As Int32
End Interface
