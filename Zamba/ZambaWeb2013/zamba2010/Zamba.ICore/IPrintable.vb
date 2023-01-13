Public Interface IPrintable
    Inherits IIndexable

    Property PrintPicture() As IZPicture
    ReadOnly Property IsImage() As Boolean
    Property IsOpen() As Boolean

End Interface