Public Interface IGrid
    Inherits IFilter, IOrder, IExporter

    Sub ShowTaskOfDT()

    Property LastPage() As Int32

    Property PageSize() As Int32

    Property SaveSearch As Boolean
    Sub AddGroupByComponent(v As String)

    Property FontSizeChanged As Boolean

End Interface
