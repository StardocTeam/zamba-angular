Public Interface IRemoteInsertEntry
    Property TemporaryId() As Int64
    Property DocumentName() As String
    Property DocTypeId() As Int64
    Property FileExtension() As String
    Property SerializedFile() As Byte()
    Property TransactionId() As Int64
End Interface