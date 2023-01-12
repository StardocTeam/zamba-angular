Public Interface IBlobDocument
    Inherits ICore

    Property Description() As String
    Property BlobFile() As Byte()
    Property UpdateDate() As Date
    Property Updateuser() As Long
    Property Extension() As String
End Interface
