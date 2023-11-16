Public Interface IFileResult
    Inherits IZambaCore

    ReadOnly Property FullPath() As String
    Function RealFullPath() As String
    Property CreateDate() As Date
    Property EditDate() As Date
    Property OriginalName() As String
End Interface