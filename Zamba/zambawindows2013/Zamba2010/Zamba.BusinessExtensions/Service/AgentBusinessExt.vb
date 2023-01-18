Imports Zamba.Data
Imports Zamba.Tools
Imports System.Text

Public Class AgentBusinessExt

    ''' <summary>
    ''' Obtiene el id del agente adjunto en caso de existir uno.
    ''' </summary>
    ''' <param name="serviceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAttachedAgentId(ByVal serviceId As Int32) As Int32
        Dim agentFactoryExt As New AgentFactoryExt
        Dim agentId As Int32 = agentFactoryExt.GetAttachedAgentId(serviceId)
        agentFactoryExt = Nothing
        Return agentId
    End Function

    ''' <summary>
    ''' Obtiene un agente configurado
    ''' </summary>
    ''' <param name="agentId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAgent(ByVal agentId As Int32) As Agent
        'Obtiene la configuración del agente
        Dim serviceBusinessExt As New ServiceBusinessExt()
        Dim dtAgentSettings As DataTable = serviceBusinessExt.GetServiceSettings(agentId)
        serviceBusinessExt = Nothing

        'Verifica que exista y se encuentre configurado
        If dtAgentSettings IsNot Nothing Then
            If dtAgentSettings.Rows.Count > 0 Then
                'Se instancia un agente
                Dim agent As New Agent(agentId)

                'Se obtiene la posicion de la columna nombre y valor
                Dim iName As Int32 = dtAgentSettings.Columns("name").Ordinal
                Dim iValue As Int32 = dtAgentSettings.Columns("value").Ordinal

                'Completa los valores del agente
                For Each dr As DataRow In dtAgentSettings.Rows
                    Select Case dr(iName)
                        Case "AgentClient"
                            agent.Client = dr(iValue).ToString
                        Case "AgentWebServerUrl"
                            agent.WebServerUrl = dr(iValue).ToString
                        Case "AgentDatabase"
                            agent.Database = dr(iValue).ToString
                        Case "AgentDbUser"
                            agent.DbUser = dr(iValue).ToString
                        Case "AgentDbPassword"
                            agent.DbPassword = dr(iValue).ToString
                        Case "AgentAttachedServiceId"
                            agent.AttachedServiceId = CInt(dr(iValue))
                        Case "AgentWebServiceUrl"
                            agent.WebServiceUrl = dr(iValue).ToString
                        Case "AgentExportMode"
                            agent.AgentExportMode = DirectCast(CInt(dr(iValue)), AgentExportMode)
                        Case "SendErrorsByMail"
                            agent.SendErrorsByMail = CBool(dr(iValue))
                    End Select
                Next

                If dtAgentSettings IsNot Nothing Then
                    dtAgentSettings.Dispose()
                    dtAgentSettings = Nothing
                End If

                'Desencripta la información de la conexión
                With agent
                    .WebServerUrl = Decrypt(.WebServerUrl)
                    .Database = Decrypt(.Database)
                    .DbUser = Decrypt(.DbUser)
                    .DbPassword = Decrypt(.DbPassword)
                    .WebServiceUrl = Decrypt(.WebServiceUrl)
                End With

                'Retorna el agente configurado
                Return agent

            Else
                If dtAgentSettings IsNot Nothing Then
                    dtAgentSettings.Dispose()
                    dtAgentSettings = Nothing
                End If
                Throw New Exception("El servicio agente no se encuentra configurado.")
            End If
        Else
            Throw New Exception("El servicio agente no existe.")
        End If
    End Function

    ''' <summary>
    ''' Desencripta la información de conexión del agent
    ''' </summary>
    ''' <param name="encryptedString">Propiedad del agent a desencriptar</param>
    ''' <returns>Propiedad del agent desencriptada</returns>
    ''' <remarks></remarks>
    Private Function Decrypt(ByVal encryptedString As String) As String
        If Not String.IsNullOrEmpty(encryptedString) Then
            Return Encryption.DecryptString(encryptedString)
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Guarda la cantidad de licencias habilitadas en un cliente
    ''' </summary>
    ''' <param name="agent">Contiene la informacion de conexión y el nombre del cliente. Este debe ser igual al de la tabla Clients.</param>
    ''' <param name="licenseType">Tipo de licencia. El id del enumerador debe ser igual al de la tabla Licenses.</param>
    ''' <param name="count">Cantidad de licencias. Debe estar desencriptado.</param>
    ''' <remarks></remarks>
    Public Function SaveEnabledLicCount(ByVal agent As IAgent, ByVal licenseType As Int32, ByVal count As String) As String
        Dim afe As New AgentFactoryExt(agent)

        Try
            Dim updated As Integer = afe.SaveEnabledLicCount(agent.Client, licenseType, count)

            'Verifica si pudo realizar modificaciones
            If updated > 0 Then
                Return String.Empty
            Else
                Return "Error al actualizar las licencias habilitadas. Verifique haber configurado correctamente el campo nombre en el servicio. Debe coincidir con los provistos por Stardoc."
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
            Return "Error al guardar las licencias habilitadas. Ex: " + ex.ToString()
        Finally
            afe = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Verifica si ocurrió un error en un web service
    ''' </summary>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    Public Sub CheckWsResult(result As String)
        If result.ToLower().Contains("error") Then
            Throw New Exception(result)
        Else
            ZTrace.WriteLineIf(ZTrace.IsVerbose, result)
        End If
    End Sub

    ''' <summary>
    ''' Verifica si ocurrió un error al guardar las licencias
    ''' </summary>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    Public Sub CheckLicResult(result As String)
        If result.Length > 0 Then
            ZTrace.WriteLineIf(ZTrace.IsVerbose, result)
        End If
    End Sub

    ''' <summary>
    ''' Envia por mail un reporte con todos los errores y problemas de performance 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Sub SendErrorsByMail(ByVal errorReports() As ErrorReport, ByVal client As String)
        If errorReports IsNot Nothing AndAlso errorReports.Count > 0 Then
            Dim sendTo As String = "exceptions@stardoc.com.ar"
            Dim subject As String = client & " - " & errorReports.Count & " nuevos reportes de error y/o performance encontrados en la última hora"
            Dim body As New StringBuilder

            body.Append("<br><b>")
            body.Append(subject)
            body.Append(" (")
            body.Append(Now.ToString)
            body.Append(")")
            body.Append("</b><br><br>")

            body.Append("<table style='width:100%;border:3px solid black;border-collapse:collapse;'>")
            body.Append("<tr style='border:3px solid black;' bgcolor='#EBF5FF'>")
            body.Append("<th style='border:1px solid black;'>Titulo</th>")
            body.Append("<th style='border:1px solid black;'>Descripcion</th>")
            body.Append("<th style='border:1px solid black;'>Version</th>")
            body.Append("<th style='border:1px solid black;'>UserId</th>")
            body.Append("<th style='border:1px solid black;'>Puesto</th>")
            body.Append("<th style='border:1px solid black;'>Windows</th>")
            body.Append("<th style='border:1px solid black;'>Creado</th>")
            body.Append("</tr>")

            For Each report As ErrorReport In errorReports
                body.Append("<tr><td style='border:1px solid black;vertical-align:top;'>")
                body.Append(report.Subject)
                body.Append("</td><td style='border:1px solid black;'>")
                body.Append(report.Description.Replace("<", "&lt;").Replace(">", "&gt;"))
                body.Append("</td><td style='border:1px solid black;vertical-align:top;'>")
                body.Append(report.Version)
                body.Append("</td><td style='border:1px solid black;vertical-align:top;'>")
                body.Append(report.UserId)
                body.Append("</td><td style='border:1px solid black;vertical-align:top;'>")
                body.Append(report.Machine)
                body.Append("</td><td style='border:1px solid black;vertical-align:top;'>")
                body.Append(report.WinUser)
                body.Append("</td><td style='border:1px solid black;vertical-align:top;'>")
                body.Append(report.Created)
                body.Append("</td></tr>")
            Next

            MessagesBusiness.SendMail(sendTo, String.Empty, String.Empty, subject, body.ToString, True)
        Else
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "No se encontraron reportes de error para ser enviados por mail")
        End If
    End Sub

End Class
