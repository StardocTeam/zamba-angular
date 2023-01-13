Public Interface IAgent

    Property Id() As Int32
    Property Client() As String
    Property WebServerUrl() As String
    Property Database() As String
    Property DbUser() As String
    Property DbPassword() As String
    Property AttachedServiceId() As Int32
    Property WebServiceUrl() As String
    Property AgentExportMode() As AgentExportMode
    Property SendErrorsByMail() As Boolean

End Interface