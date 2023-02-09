Public Interface IFileResult
    Inherits IZambaCore

    Function RealFullPath() As String
    ReadOnly Property FullPath() As String
    Property CreateDate() As Date
    Property EditDate() As Date
    Property OriginalName() As String
    ReadOnly Property MimeType() As String

End Interface