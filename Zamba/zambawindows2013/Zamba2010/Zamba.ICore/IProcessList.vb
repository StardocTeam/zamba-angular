Public Interface IProcessList
    Property Id() As Int32
    Property Name() As String
    Property Description() As String
    Property Enabled() As Boolean
    Property DsProcess() As DataSet
End Interface