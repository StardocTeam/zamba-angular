Public Interface IProcessHistory
    Property LogList() As ArrayList
    Property Id() As Int32
    Property Process_ID() As Int32
    Property HistoryIndex() As Int32
    Property Process_Date() As DateTime
    Property User_Id() As Int32
    Property Hash() As String
    Property TotalFiles() As Int64
    Property ProcesedFiles() As Int64
    Property ErrorFiles() As Int64
    Property SkipedFiles() As Int64
    Property Result() As Results
    Property Path() As String
    Property TEMPFILE() As String
    Property ERRORFILE() As String
    Property LOGFILE() As String
    Property Histories() As DataSet
    Property ErrorList() As ArrayList
End Interface