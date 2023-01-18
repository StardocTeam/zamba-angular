Imports Zamba.Servers

Public Class AgentFactoryExt
    Implements IDisposable

    Private _agent As Core.Agent
    Private _webServerCon As IConnection = Nothing

    Sub New()
    End Sub

    Sub New(agent As Core.Agent)
        ' TODO: Complete member initialization 
        _agent = agent
    End Sub

    ''' <summary>
    ''' Obtiene el id del agente adjunto al servicio.
    ''' </summary>
    ''' <param name="serviceId"></param>
    ''' <returns>Si el valor es -1 es porque no tiene un servicio agente adjunto.</returns>
    ''' <remarks></remarks>
    Public Function GetAttachedAgentId(ByVal serviceId As Int32) As Int32
        Dim result As Object = Nothing
        Dim agentId As Int32

        If Server.isOracle Then
            Dim query As String = "SELECT serviceId FROM ZServiceOptions where name='AgentAttachedServiceId' AND value='" & serviceId.ToString() & "'"
            result = Server.Con.ExecuteScalar(CommandType.Text, query)
        Else
            result = Server.Con.ExecuteScalar("zsp_agent_100_GetAttachedAgentId", New Object() {serviceId})
        End If

        If Int32.TryParse(result, agentId) Then
            Return agentId
        Else
            Return -1
        End If
    End Function

    ''' <summary>
    ''' Ejecuta el reporte de licencias de usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUsersReport() As DataSet
        If Server.isOracle Then
            Dim query As String = "select nombres, apellido, Name,ID,C_TIME,U_TIME,WINUSER,WINPC,CON_ID,TIME_OUT,TYPE from ucm c inner join usrtable u on c.user_id = u.id"
            Return Server.Con.ExecuteDataset(CommandType.Text, query)
        Else
            Return Server.Con.ExecuteDataset("zsp_agent_100_GetUsersData")
        End If
    End Function

    ''' <summary>
    ''' Agrega un registro del reporte de licencias
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <param name="cTime"></param>
    ''' <param name="uTime"></param>
    ''' <param name="winUser"></param>
    ''' <param name="winPc"></param>
    ''' <param name="conId"></param>
    ''' <param name="timeOut"></param>
    ''' <param name="type"></param>
    ''' <param name="client"></param>
    ''' <param name="useWebServerCon"></param>
    ''' <remarks></remarks>
    Public Sub AddUcmClientData(userId As String, _
                                cTime As Date, _
                                uTime As Date, _
                                winUser As String, _
                                winPc As String, _
                                conId As Long, _
                                timeOut As Long, _
                                type As Integer, _
                                client As String, _
                                useWebServerCon As Boolean)
        Dim query As String = BuildUcmInsertQuery(userId, cTime, uTime, winUser, winPc, conId, timeOut, type, client)
        If useWebServerCon Then
            _webServerCon = Server.Con(Server.AppConfig.SERVERTYPE, _agent.WebServerUrl, _agent.Database, _agent.DbUser, _agent.DbPassword, True, True)
            _webServerCon.ExecuteNonQuery(CommandType.Text, query)
        Else
            Server.Con.ExecuteNonQuery(CommandType.Text, query)
        End If
    End Sub

    ''' <summary>
    ''' Construye la consulta de inserción para el reporte
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <param name="cTime"></param>
    ''' <param name="uTime"></param>
    ''' <param name="winUser"></param>
    ''' <param name="winPc"></param>
    ''' <param name="conId"></param>
    ''' <param name="timeOut"></param>
    ''' <param name="type"></param>
    ''' <param name="client"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function BuildUcmInsertQuery(userId As String, _
                            cTime As Date, _
                            uTime As Date, _
                            winUser As String, _
                            winPc As String, _
                            conId As Long, _
                            timeOut As Long, _
                            type As Integer, _
                            client As String)
        Return "INSERT INTO UCMCLIENTSSet (USER_ID,C_TIME,U_TIME,WINUSER,WINPC,CON_ID,TIME_OUT,TYPE,Client,Server,Base,UpdateDate) VALUES (" & _
            userId & "," & _
            Server.Con.ConvertDateTime(cTime.ToString()) & "," & _
            Server.Con.ConvertDateTime(uTime.ToString()) & ",'" & _
            winUser & "','" & _
            winPc & "'," & _
            conId & "," & _
            timeOut & "," & _
            type & ",'" & _
            client & "','" & _
            Server.AppConfig.SERVER & "','" & _
            Server.AppConfig.DB & "'," & _
            Server.Con.ConvertDateTime(DateTime.Now.ToString()) & ")"
    End Function

    ''' <summary>
    ''' Obtiene los reportes ILM
    ''' </summary>
    ''' <returns></returns>
    Public Function GetILM() As DataSet
        If Server.isOracle Then
            Dim query As String
            query = "SELECT ZExportControl.Fecha, ZExportControl.UserId, ZExportControl.Doc_Type_Id, USRTABLE.Name "
            query &= "FROM ZExportControl INNER JOIN USRTABLE ON USRTABLE.ID = ZExportControl.UserId "
            query &= "WHERE to_number(to_char(Fecha, 'yyyy')) = to_number(to_char(sysdate, 'yyyy')) "
            query &= "AND to_number(to_char(Fecha, 'MM')) = to_number(to_char(sysdate, 'MM')) "
            query &= "AND to_number(to_char(Fecha, 'dd')) = to_number(to_char(sysdate, 'dd')) "
            query &= "AND to_number(to_char(Fecha, 'HH24')) = to_number(to_char(sysdate, 'HH24')) - 1 "
            query &= "ORDER BY FECHA"

            Return Server.Con.ExecuteDataset(CommandType.Text, query)
        Else
            Return Server.Con.ExecuteDataset("zsp_agent_200_GetILM")
        End If
    End Function

    ''' <summary>
    ''' Agrega un registro del reporte de ILM
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <param name="winUser"></param>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <param name="day"></param>
    ''' <param name="hour"></param>
    ''' <param name="crdate"></param>
    ''' <param name="client"></param>
    ''' <param name="codigoMail"></param>
    ''' <param name="docId"></param>
    ''' <param name="docTypeId"></param>
    ''' <remarks></remarks>
    Public Sub AddILMClientData(userId As String, _
                            userName As String, _
                            year As String, _
                            month As String, _
                            day As String, _
                            hour As String, _
                            crdate As DateTime, _
                            client As String, _
                            codigoMail As String, _
                            docId As String, _
                            docTypeId As String, _
                            useWebServerCon As Boolean)
        Dim query As String = BuildILMInsertQuery(userId, userName, year, month, day, hour, crdate, client, codigoMail, docId, docTypeId)
        If useWebServerCon Then
            _webServerCon = Server.Con(Server.AppConfig.SERVERTYPE, _agent.WebServerUrl, _agent.Database, _agent.DbUser, _agent.DbPassword, True, True)
            _webServerCon.ExecuteNonQuery(CommandType.Text, query)
        Else
            Server.Con.ExecuteNonQuery(CommandType.Text, query)
        End If
    End Sub

    ''' <summary>
    ''' Construye la consulta de inserción para ILM
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <param name="userName"></param>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <param name="day"></param>
    ''' <param name="hour"></param>
    ''' <param name="crdate"></param>
    ''' <param name="client"></param>
    ''' <param name="codigoMail"></param>
    ''' <param name="docId"></param>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function BuildILMInsertQuery(userId As String, _
                            userName As String, _
                            year As String, _
                            month As String, _
                            day As String, _
                            hour As String, _
                            crdate As DateTime, _
                            client As String, _
                            codigoMail As String, _
                            docId As String, _
                            docTypeId As String)
        Return "INSERT INTO ILMClientSet (UserId,UserName,Year,Month,Day,Hour,Type,UpdateDate,CrDate,Client,Server,Base,CodigoMail,doc_id,DocTypeId) values (" & _
            userId & ",'" & _
            userName & "'," & _
            year & "," & _
            month & "," & _
            day & "," & _
            hour & "," & _
            82 & "," & _
            Server.Con.ConvertDateTime(DateTime.Now.ToString()) & "," & _
            Server.Con.ConvertDateTime(crdate.ToString()) & ",'" & _
            client & "','" & _
            Server.AppConfig.SERVER & "','" & _
            Server.AppConfig.DB & "','" & _
            codigoMail & "'," & _
            docId & "," & _
            docTypeId & ")"
    End Function

    ''' <summary>
    ''' Guarda la cantidad de licencias habilitadas en un cliente
    ''' </summary>
    ''' <param name="client">Cliente. El cliente configurado en el agent debe ser igual al de la tabla Clients.</param>
    ''' <param name="licenseType">Tipo de licencia. El id del enumerador debe ser igual al de la tabla Licenses.</param>
    ''' <param name="count">Cantidad de licencias. Debe estar desencriptado.</param>
    ''' <remarks></remarks>
    Public Function SaveEnabledLicCount(ByVal client As String, ByVal licenseType As Int32, ByVal count As String) As Int32
        _webServerCon = Server.Con(Server.AppConfig.SERVERTYPE, _agent.WebServerUrl, _agent.Database, _agent.DbUser, _agent.DbPassword, True, True)
        Return _webServerCon.ExecuteScalar(CommandType.Text, String.Format(" DECLARE @idClient numeric  SET NOCOUNT ON Select @idClient = ID FROM Clients WHERE LOWER(Name) = LOWER({0}) SET NOCOUNT OFF UPDATE ClientsLicenses SET [Enabled] = {2} WHERE ClientsLicenses.IdClient = @idClient  And ClientsLicenses.IdLicense = {1}", client, licenseType, count))

    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _agent = Nothing
                If _webServerCon IsNot Nothing Then
                    Try
                        _webServerCon.dispose()
                    Catch
                    Finally
                        _webServerCon = Nothing
                    End Try
                End If
            End If
        End If
        disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
