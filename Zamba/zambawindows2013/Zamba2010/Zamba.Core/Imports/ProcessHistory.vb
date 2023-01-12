
'Imports zamba.imports


Public Class ProcessHistory
    Implements IProcessHistory

#Region " Atributos "
    Private _historyIndex As Int32
    Private _histories As New DataSet
    Private _errorList As New ArrayList
    Private _logList As New ArrayList()
#End Region

#Region " Propiedades "
    Public Property Id() As Int32 Implements IProcessHistory.Id
        Get
            Return Convert.ToInt32(Histories.Tables(0)(HistoryIndex)("ID"))
        End Get
        Set(ByVal Value As Int32)
            Histories.Tables(0)(HistoryIndex)("ID") = Value
        End Set
    End Property
    Public Property Process_ID() As Int32 Implements IProcessHistory.Process_ID
        Get
            Return Convert.ToInt32(Histories.Tables(0)(HistoryIndex)("Process_ID"))
        End Get
        Set(ByVal Value As Int32)
            Histories.Tables(0)(HistoryIndex)("Process_ID") = Value
        End Set
    End Property
    Public Property Process_Date() As DateTime Implements IProcessHistory.Process_Date
        Get
            Return Histories.Tables(0)(HistoryIndex)("Process_Date")
        End Get
        Set(ByVal Value As DateTime)
            Histories.Tables(0)(HistoryIndex)("Process_Date") = Value
        End Set
    End Property
    Public Property User_Id() As Int32 Implements IProcessHistory.User_Id
        Get
            Return Convert.ToInt32((Histories.Tables(0)(HistoryIndex)("User_Id")))
        End Get
        Set(ByVal Value As Int32)
            Histories.Tables(0)(HistoryIndex)("User_Id") = Value
        End Set
    End Property
    Public Property LogList() As ArrayList Implements IProcessHistory.LogList
        Get
            Return _logList
        End Get
        Set(ByVal value As ArrayList)
            _logList = value
        End Set
    End Property
    Public Property Hash() As String Implements IProcessHistory.Hash
        Get
            Return Histories.Tables(0)(HistoryIndex)("Hash")
        End Get
        Set(ByVal Value As String)
            Histories.Tables(0)(HistoryIndex)("Hash") = Value
        End Set
    End Property
    Public Property TotalFiles() As Int64 Implements IProcessHistory.TotalFiles
        Get
            Return Convert.ToInt32(Histories.Tables(0)(HistoryIndex)("TotalFiles"))
        End Get
        Set(ByVal Value As Int64)
            Histories.Tables(0)(HistoryIndex)("TotalFiles") = Value
        End Set
    End Property
    Public Property ProcesedFiles() As Int64 Implements IProcessHistory.ProcesedFiles
        Get
            Return Convert.ToInt64(Histories.Tables(0)(HistoryIndex)("ProcessedFiles"))
        End Get
        Set(ByVal Value As Int64)
            Histories.Tables(0)(HistoryIndex)("ProcessedFiles") = Value
        End Set
    End Property
    Public Property ErrorFiles() As Int64 Implements IProcessHistory.ErrorFiles
        Get
            Return Convert.ToInt64(Histories.Tables(0)(HistoryIndex)("ErrorFiles"))
        End Get
        Set(ByVal Value As Int64)
            Histories.Tables(0)(HistoryIndex)("ErrorFiles") = Value
        End Set
    End Property
    Public Property SkipedFiles() As Int64 Implements IProcessHistory.SkipedFiles
        Get
            Return Convert.ToInt64(Histories.Tables(0)(HistoryIndex)("SkipedFiles"))
        End Get
        Set(ByVal Value As Int64)
            Histories.Tables(0)(HistoryIndex)("SkipedFiles") = Value
        End Set
    End Property
    Public Property Result() As Results Implements IProcessHistory.Result
        Get
            Return CType(Histories.Tables(0)(HistoryIndex)("Result_Id"), Results)
        End Get
        Set(ByVal Value As Results)
            Histories.Tables(0)(HistoryIndex)("Result_Id") = (Value)
        End Set
    End Property
    Public Property Path() As String Implements IProcessHistory.Path
        Get
            Return Histories.Tables(0)(HistoryIndex)("Path")
        End Get
        Set(ByVal Value As String)
            Histories.Tables(0)(HistoryIndex)("Path") = Value
        End Set
    End Property
    Public Property TEMPFILE() As String Implements IProcessHistory.TEMPFILE
        Get
            Return Histories.Tables(0)(HistoryIndex)("TEMPFILE")
        End Get
        Set(ByVal Value As String)
            Histories.Tables(0)(HistoryIndex)("TEMPFILE") = Value.Trim
        End Set
    End Property
    Public Property ERRORFILE() As String Implements IProcessHistory.ERRORFILE
        Get
            Return Histories.Tables(0)(HistoryIndex)("ERRORFILE")
        End Get
        Set(ByVal Value As String)
            Histories.Tables(0)(HistoryIndex)("ERRORFILE") = Value.Trim
        End Set
    End Property
    Public Property LOGFILE() As String Implements IProcessHistory.LOGFILE
        Get
            Return Histories.Tables(0)(HistoryIndex)("LOGFILE")
        End Get
        Set(ByVal Value As String)
            Histories.Tables(0)(HistoryIndex)("LOGFILE") = Value.Trim
        End Set
    End Property
    Public Property ErrorList() As ArrayList Implements IProcessHistory.ErrorList
        Get
            Return _errorList
        End Get
        Set(ByVal value As ArrayList)
            _errorList = value
        End Set
    End Property
    Public Property Histories() As DataSet Implements IProcessHistory.Histories
        Get
            Return _histories
        End Get
        Set(ByVal value As DataSet)
            _histories = value
        End Set
    End Property
    Public Property HistoryIndex() As Int32 Implements IProcessHistory.HistoryIndex
        Get
            Return _historyIndex
        End Get
        Set(ByVal value As Int32)
            _historyIndex = value
        End Set
    End Property
#End Region

#Region " Constructores "
    Public Sub New()
        MyBase.new()
    End Sub
#End Region

End Class