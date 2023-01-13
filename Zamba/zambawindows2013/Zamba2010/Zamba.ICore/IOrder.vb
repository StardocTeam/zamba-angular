Public Interface IOrder

    'Sub AddOrderComponent(index As String, direction As String)

    Sub AddOrderComponent(orderString As String)

    Property SortChanged() As Boolean

End Interface
