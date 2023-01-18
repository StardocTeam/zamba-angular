Public Interface ILazyLoad
    ReadOnly Property IsLoaded() As Boolean
    ReadOnly Property IsFull() As Boolean
    Sub Load()
    Sub FullLoad()
End Interface