Public Interface IIfTimePass
    Inherits IRule
    Property Minute() As Int32
    Property Hour() As Int32
    Property Day() As Int32
    Property Week() As Int32
    Property lastExecute() As Date
End Interface
