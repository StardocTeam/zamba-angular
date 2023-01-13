Imports Zamba.Data
Imports System.ServiceModel



Public Class AgentServiceBusiness
    Implements IService

#Region "Atributo y Constructor"
    Dim serviceId As Int64
    Public Sub New(ByVal serviceId As Int64)
        Me.serviceId = serviceId
    End Sub
#End Region

#Region "Implementación de IService"
    Public Sub StartService() Implements IService.StartService
        'Variables temporales y de funcionamiento
        Dim agent As Agent = Nothing
        Dim dsUcmReport As DataSet = Nothing
        Dim dsIlmReport As DataSet = Nothing
        Dim errorReports() As ErrorReport = Nothing
        Dim abe As New AgentBusinessExt
        Dim afe As AgentFactoryExt = Nothing
        Dim erb As New ErrorReportBusiness
        Dim ucmResult As String = String.Empty
        Dim ilmResult As String = String.Empty
        Dim errorResult As String = String.Empty
        Dim binding As BasicHttpBinding = Nothing
        Dim endpoint As EndpointAddress = Nothing
        Dim enabledLicDoc, resultLicDoc As String
        Dim enabledLicWf, resultLicWf As String
        Dim licBusiness As ClsLic

        'Variables de los reportes
        Dim userId, winUser, winPc As String
        Dim cTime, uTime As DateTime
        Dim conId, timeOut As Int64
        Dim type As Integer
        Dim docTypeId As String

        Try
            '--------------------------------------------------------------------------------------------------------------------------------
            'Se crean los servicios y conexiones necesarias
            '--------------------------------------------------------------------------------------------------------------------------------

            'Se obtiene la configuracion del servicio
            agent = abe.GetAgent(serviceId)

            'Verifica la configuración del nombre del cliente donde se ejecuta el reporte
            If String.IsNullOrEmpty(agent.Client) Then
                Throw New ArgumentException("Falta la configuración del nombre del cliente.", "Name")
            End If

            'Instancia de agentFactory con los datos de la conexión
            If agent.AgentExportMode = AgentExportMode.SQL Then
                afe = New AgentFactoryExt(agent)
            Else
                afe = New AgentFactoryExt()
            End If

            '--------------------------------------------------------------------------------------------------------------------------------
            'Se obtienen los datos a exportar
            '--------------------------------------------------------------------------------------------------------------------------------

            'Se obtienen los datos del reporte UCM
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo conexiones.")
            dsUcmReport = afe.GetUsersReport()
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se encontraron " & dsUcmReport.Tables(0).Rows.Count.ToString & " conexiones.")

            'Se obtienen las licencias habilitadas
            licBusiness = New ClsLic()
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo licencias documentales habilitadas.")
            enabledLicDoc = licBusiness.GetLicenseCount(LicenseType.Documental)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo licencias workflow habilitadas.")
            enabledLicWf = licBusiness.GetLicenseCount(LicenseType.Workflow)

            'Se obtiene el reporte ILM
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo datos de exportación.")
            dsIlmReport = afe.GetILM()
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se encontraron " & dsIlmReport.Tables(0).Rows.Count.ToString & " exportaciones.")

            '--------------------------------------------------------------------------------------------------------------------------------
            'Se guarda una copia local del reporte UCM
            '--------------------------------------------------------------------------------------------------------------------------------

            'Se insertan los resultados en la tabla de reporte local para tener respaldo de los datos
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando datos de conexión de manera local.")
            For Each R As DataRow In dsUcmReport.Tables(0).Rows
                userId = R("ID").ToString()
                cTime = DateTime.Parse(R("C_TIME").ToString())
                uTime = DateTime.Parse(R("U_TIME").ToString())
                winUser = R("WINUSER").ToString()
                winPc = R("WINPC").ToString()
                conId = Int64.Parse(R("CON_ID").ToString())
                timeOut = Int64.Parse(R("TIME_OUT").ToString())
                type = Integer.Parse(R("TYPE").ToString())

                afe.AddUcmClientData(userId, cTime, uTime, winUser, winPc, conId, timeOut, type, agent.Client, False)
            Next

            '--------------------------------------------------------------------------------------------------------------------------------
            'Exportación de los datos via web service
            '--------------------------------------------------------------------------------------------------------------------------------
            If agent.AgentExportMode = AgentExportMode.WebService Then
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Estableciendo conexión con el webservice.")
                binding = New BasicHttpBinding()
                endpoint = New EndpointAddress(agent.WebServiceUrl)


                'Instancia y consume el webservice
                Using ASC As New AgentService.AgentServiceClient(binding, endpoint)
                    If dsUcmReport IsNot Nothing AndAlso dsUcmReport.Tables(0).Rows.Count > 0 Then
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando datos de conexión.")
                        ucmResult = ASC.SaveUCMDataSet(dsUcmReport, agent.Client, DateTime.Now, Zamba.Servers.Server.AppConfig.SERVER, Zamba.Servers.Server.AppConfig.DB)
                        'NOTA: Existe un trigger en la tabla UCMClientsSet que elimina los registros de bases de Stardoc u otras de test.
                        'Tener esto en cuenta ya que pueden perder muchas horas de test hasta darse cuenta de esto...
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "El reporte UCM se encuentra vacío.")
                    End If

                    'Se envian las licencias
                    If String.IsNullOrEmpty(enabledLicDoc) Then
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "# No se pudieron obtener las licencias documentales habilitadas.")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando licencias documentales habilitadas.")
                        resultLicDoc = ASC.SaveEnabledLicCount(agent.Client, LicenseType.Documental, enabledLicDoc)
                        abe.CheckLicResult(resultLicDoc)
                    End If
                    If String.IsNullOrEmpty(enabledLicWf) Then
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "# No se pudieron obtener las licencias de workflow habilitadas.")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando licencias workflow habilitadas.")
                        resultLicWf = ASC.SaveEnabledLicCount(agent.Client, LicenseType.Workflow, enabledLicWf)
                        abe.CheckLicResult(resultLicWf)
                    End If

                    'Se obtienen los errores no exportados
                    errorReports = erb.GetReportsToExport()
                    If errorReports.Count > 0 Then
                        'Se envia el reporte de errores
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando reportes de error y performance.")
                        errorResult = ASC.SaveErrorReports(errorReports, agent.Client)
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, errorResult)
                        ZOptBusiness.InsertUpdateValue("LastExportedReportId", errorReports.Max(Function(x) x.Id))
                    End If

                    'Se envia el reporte de ILM
                    If dsIlmReport IsNot Nothing AndAlso dsIlmReport.Tables(0).Rows.Count > 0 Then
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando datos de exportación.")
                        ilmResult = ASC.SaveILMDataSet(dsIlmReport, agent.Client, DateTime.Now, Zamba.Servers.Server.AppConfig.SERVER, Zamba.Servers.Server.AppConfig.DB)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "El reporte ILM se encuentra vacío.")
                    End If
                End Using

                'Verifica si ocurrió un error en UCM o ILM
                abe.CheckWsResult(ucmResult)
                abe.CheckWsResult(ilmResult)
            End If

            '--------------------------------------------------------------------------------------------------------------------------------
            'Exportación de los datos via SQL
            '--------------------------------------------------------------------------------------------------------------------------------
            If agent.AgentExportMode = AgentExportMode.SQL Then
                If dsUcmReport IsNot Nothing AndAlso dsUcmReport.Tables(0).Rows.Count > 0 Then
                    'Se insertan los resultados en la tabla de reporte de Stardoc
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando datos de conexión.")
                    For Each R As DataRow In dsUcmReport.Tables(0).Rows
                        userId = R("ID").ToString()
                        cTime = DateTime.Parse(R("C_TIME").ToString())
                        uTime = DateTime.Parse(R("U_TIME").ToString())
                        winUser = R("WINUSER").ToString()
                        winPc = R("WINPC").ToString()
                        conId = Int64.Parse(R("CON_ID").ToString())
                        timeOut = Int64.Parse(R("TIME_OUT").ToString())
                        type = Integer.Parse(R("TYPE").ToString())

                        afe.AddUcmClientData(userId, cTime, uTime, winUser, winPc, conId, timeOut, type, agent.Client, True)
                    Next
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "UCM guardado exitosamente. Registros insertados: " + dsUcmReport.Tables(0).Rows.Count.ToString())
                Else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "El reporte UCM se encuentra vacío.")
                End If

                'Se envian las licencias
                If String.IsNullOrEmpty(enabledLicDoc) Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "# No se pudieron obtener las licencias documentales habilitadas.")
                Else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando licencias documentales habilitadas.")
                    resultLicDoc = abe.SaveEnabledLicCount(agent, LicenseType.Documental, enabledLicDoc)
                    abe.CheckLicResult(resultLicDoc)
                End If
                If String.IsNullOrEmpty(enabledLicWf) Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "# No se pudieron obtener las licencias de workflow habilitadas.")
                Else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando licencias workflow habilitadas.")
                    resultLicWf = abe.SaveEnabledLicCount(agent, LicenseType.Workflow, enabledLicWf)
                    abe.CheckLicResult(resultLicWf)
                End If

                If errorReports.Count > 0 Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "El envío de reportes de error y performance no se encuentra disponible para conexiones remotas SQL.")
                End If

                If dsIlmReport IsNot Nothing AndAlso dsIlmReport.Tables(0).Rows.Count > 0 Then
                    Dim colFecha As Int32 = dsIlmReport.Tables(0).Columns("Fecha").Ordinal
                    Dim colUserid As Int32 = dsIlmReport.Tables(0).Columns("UserId").Ordinal
                    Dim colDocTypeId As Int32 = dsIlmReport.Tables(0).Columns("Doc_Type_Id").Ordinal
                    Dim colUserName As Int32 = dsIlmReport.Tables(0).Columns("Name").Ordinal

                    'Verifica si se ejecuto exitosamente la conexion via sql
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando datos de exportación.")
                    For Each R As DataRow In dsIlmReport.Tables(0).Rows
                        userId = Int64.Parse(R(colFecha).ToString())
                        cTime = DateTime.Parse(R(colUserid).ToString())
                        docTypeId = Int64.Parse(R(colDocTypeId).ToString())
                        winUser = R(colUserName).ToString() 'en realidad es el usuario de Zamba

                        'Para que sea genérico
                        afe.AddILMClientData(userId, winUser, cTime.Year, cTime.Month,
                                                            cTime.Day, cTime.Month, cTime, agent.Client,
                                                            String.Empty, 0, docTypeId, True)
                    Next
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "ILM guardado exitosamente. Registros insertados: " + dsIlmReport.Tables(0).Rows.Count.ToString())
                Else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "El reporte ILM se encuentra vacío.")
                End If
            End If

            '--------------------------------------------------------------------------------------------------------------------------------
            'Envio de reporte de errores por mail
            '--------------------------------------------------------------------------------------------------------------------------------
            If agent.SendErrorsByMail AndAlso errorReports.Count > 0 Then
                abe.SendErrorsByMail(errorReports, agent.Client)
            End If

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsVerbose, ex.Message)

        Finally
            '--------------------------------------------------------------------------------------------------------------------------------
            'Liberación de recursos utilizados
            '--------------------------------------------------------------------------------------------------------------------------------
            erb = Nothing
            abe = Nothing
            agent = Nothing
            binding = Nothing
            endpoint = Nothing
            licBusiness = Nothing
            errorReports = Nothing
            If afe IsNot Nothing Then
                Try
                    afe.Dispose()
                Catch
                End Try
                afe = Nothing
            End If
            If dsUcmReport IsNot Nothing Then
                dsUcmReport.Dispose()
                dsUcmReport = Nothing
            End If
            If dsIlmReport IsNot Nothing Then
                dsIlmReport.Dispose()
                dsIlmReport = Nothing
            End If
        End Try

        Try
            Dim WFServiceServicesExecute As String = ServiceBusiness.getValue(serviceId, "ServiceServicesExecute")
            Dim ExecuteServices As Boolean
            If Boolean.TryParse(WFServiceServicesExecute, ExecuteServices) AndAlso ExecuteServices Then
                If UpdateBusiness.CheckAndDownloadVersionsAtServer() > 0 Then
                    UpdateBusiness.UpdateUsersWithAutoUpdate()
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try


    End Sub

    Public Sub StopService() Implements IService.StopService
        Try
            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(ServiceTypes.Agent, ObjectTypes.Services, RightsType.Terminar, "Deteniendo servicio agente con ID " & serviceId.ToString)
        Catch
        End Try
    End Sub

#End Region

End Class


Public Class QuequeMailsServiceBusiness
    Implements IService

#Region "Atributo y Constructor"
    Dim serviceId As Int64
    Public Sub New(ByVal serviceId As Int64)
        Me.serviceId = serviceId
    End Sub
#End Region

#Region "Implementación de IService"
    Public Sub StartService() Implements IService.StartService
        Dim QMB As New Zamba.Core.MessagesBusiness
        Try
            QMB.PeekQuequedMail(QMB.QuequedStates.Pending)
            QMB.PeekQuequedMail(QMB.QuequedStates.WithError)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            QMB = Nothing
        End Try
    End Sub

    Public Sub StopService() Implements IService.StopService
        Try
            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(ServiceTypes.QuequeMails, ObjectTypes.Services, RightsType.Terminar, "Deteniendo servicio Mails con ID " & serviceId.ToString)
        Catch
        End Try
    End Sub

#End Region

End Class