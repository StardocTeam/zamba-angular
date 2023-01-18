Public Class Agent
    Implements IAgent

    Sub New(ByVal agentId As Int32)
        _id = agentId
    End Sub

    Public Property AttachedServiceId As Integer Implements IAgent.AttachedServiceId
    Public Property Database As String Implements IAgent.Database
    Public Property DbPassword As String Implements IAgent.DbPassword
    Public Property DbUser As String Implements IAgent.DbUser
    Public Property Id As Integer Implements IAgent.Id
    Public Property Client As String Implements IAgent.Client
    Public Property WebServerUrl As String Implements IAgent.WebServerUrl
    Public Property WebServiceUrl As String Implements IAgent.WebServiceUrl
    Public Property AgentExportMode As AgentExportMode Implements IAgent.AgentExportMode
    Public Property SendErrorsByMail As Boolean Implements IAgent.SendErrorsByMail

End Class
