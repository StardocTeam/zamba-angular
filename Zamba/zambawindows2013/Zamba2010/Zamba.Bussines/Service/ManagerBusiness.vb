Imports System.ServiceProcess
Imports System.Collections.Generic
Imports Zamba.Core
Imports System.Text

Public Class ManagerBusiness
    Implements IService

    Public Sub stopService() Implements IService.StopService
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Deteniendo Manager")
    End Sub

    Dim firstTimeRunning As Boolean = True
    Dim serviceIDRunning As Int64
    Dim Services As List(Of ServiceObj)
    Dim id As Object
    Dim ServiceID As Object
    Dim actionID As Object
    Dim dsaction As DataSet = WFServiceManagerBusiness.GetActiveAction()

    Public Sub StartService() Implements IService.StartService
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando accion a realizar...")

            Try
                ServiceBusiness.UpdateServiceDate(_id, ServiceTypes.Manager)
            Catch ex As Exception
                'raiseerror(ex)
                ZTrace.WriteLineIf(ZTrace.IsError, ex.Message)
            End Try

            Dim id As Object
            Dim ServiceID As Object
            Dim actionID As Object
            Dim dsaction As DataSet = WFServiceManagerBusiness.GetActiveAction()

            If dsaction.Tables(0).Rows.Count > 0 Then
                id = dsaction.Tables(0).Rows(0)(0)
                ServiceID = dsaction.Tables(0).Rows(0)(1)
                actionID = dsaction.Tables(0).Rows(0)(2)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Accion activa: " & actionID & " para el servicio: " & ServiceID)
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No hay accion activa")
                dsaction = WFServiceManagerBusiness.GetNextAction()
                If dsaction.Tables(0).Rows.Count > 0 Then
                    id = dsaction.Tables(0).Rows(0)(0)
                    ServiceID = dsaction.Tables(0).Rows(0)(1)
                    actionID = dsaction.Tables(0).Rows(0)(2)
                    WFServiceManagerBusiness.ActiveAction(CShort(id))
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Accion activa: " & actionID & " para el servicio: " & ServiceID)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando si hay que levantar algun servicio automatico")
                    Try
                        If IsNothing(Services) Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo servicios")
                            Services = ServiceBusiness.GetServices()
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Servicios obtenidos: " & Services.Count)
                        End If

                        For Each r As ServiceObj In Services

                            If r.ServiceType <> ServiceTypes.Manager Then

                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo el servicio de Windows " & r.ServiceName)
                                Dim service As ServiceController = GetWinService(r.ServiceName)
                                If service IsNot Nothing Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Servicio encontrado.")

                                    Dim forceAutoStart As String = ServiceBusiness.getValue(r.ServiceID, "ForceAutoStart")
                                    If String.IsNullOrEmpty(forceAutoStart) Then
                                        forceAutoStart = "true"
                                    Else
                                        forceAutoStart = forceAutoStart.ToLower()
                                    End If

                                    If String.Compare(forceAutoStart, "true") = 0 Then
                                        If service.Status = ServiceControllerStatus.Stopped Then
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "# Iniciando el servicio...")
                                            service.Start()
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "# Servicio iniciado.")
                                        Else
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "El estado del servicio es " & service.Status.ToString)

                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando actividad del servicio de WorkFlow")

                                            CheckInactiveService(r)

                                            If r.Description <> Nothing Then
                                                If service.Status = ServiceControllerStatus.Running Then
                                                End If
                                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Deteniendo servicio...")
                                                service.Stop()
                                            ElseIf service.Status = ServiceControllerStatus.Stopped Then
                                                WFServiceManagerBusiness.DeleteAction(CShort(id))
                                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El servicio fue detenido correctamente.")

                                            ElseIf service.Status = ServiceControllerStatus.Stopped Then
                                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando el servicio...")
                                                service.Start()
                                                firstTimeRunning = False
                                                serviceIDRunning = ServiceID
                                            ElseIf service.Status = ServiceControllerStatus.Running Then
                                                'Si ya estaba corriendo le doy una vuelta mas en caso de que haya tardado en cerrarse
                                            ElseIf firstTimeRunning = False And ServiceID = serviceIDRunning Then
                                                firstTimeRunning = True
                                                serviceIDRunning = 0
                                                WFServiceManagerBusiness.DeleteAction(CShort(id))
                                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El servicio fue iniciado correctamente.")
                                            Else
                                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El servicio ya estaba corriendo.")
                                                firstTimeRunning = False
                                                serviceIDRunning = ServiceID
                                            End If

                                        End If
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Solo se iniciarán los servicios con estado Detenido.")
                                    End If
                                Else
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Servicio no encontrado. Verifique que el servicio se encuentre instalado.")
                                End If
                            End If
                        Next

                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End If
            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "No hay accion a realizar.")
            Exit Sub
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Exit Sub

        Try
            Dim ServiceDesc As String = ServiceBusiness.getServiceName(CShort(ServiceID))
            If String.IsNullOrEmpty(ServiceDesc) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No existe ningun servicio con id " & ServiceID & " en la tabla de servicios.")
                WFServiceManagerBusiness.DeleteAction(CShort(id))
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del servicio: " & ServiceDesc)
            End If
            If CShort(actionID) > 0 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia del servicio")
                Dim service As ServiceController = GetWinService(ServiceDesc)
                If service Is Nothing Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "El servicio " & service.ServiceName & " no existe.")
                    WFServiceManagerBusiness.DeleteAction(CShort(id))
                    Exit Sub
                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Procesando acción " & actionID & " sobre el servicio " & service.ServiceName & " (" & ServiceID & ")")
                Select Case [Enum].Parse(GetType(ServiceManagerAction), actionID)
                    Case ServiceManagerAction.ForceStopService
                        If service.Status = ServiceControllerStatus.Running Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Deteniendo servicio...")
                            service.Stop()
                        End If
                        If service.Status = ServiceControllerStatus.Stopped Then
                            WFServiceManagerBusiness.DeleteAction(CShort(id))
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "El servicio fue detenido correctamente.")
                        End If
                    Case ServiceManagerAction.StartService
                        If service.Status = ServiceControllerStatus.Stopped Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Iniciando el servicio...")
                            service.Start()
                            firstTimeRunning = False
                            serviceIDRunning = ServiceID
                        ElseIf service.Status = ServiceControllerStatus.Running Then
                            'Si ya estaba corriendo le doy una vuelta mas en caso de que haya tardado en cerrarse
                            If firstTimeRunning = False And ServiceID = serviceIDRunning Then
                                firstTimeRunning = True
                                serviceIDRunning = 0
                                WFServiceManagerBusiness.DeleteAction(CShort(id))
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El servicio fue iniciado correctamente.")
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El servicio ya estaba corriendo.")
                                firstTimeRunning = False
                                serviceIDRunning = ServiceID
                            End If
                        End If
                    Case ServiceManagerAction.ForceResetService
                        If service.Status = ServiceControllerStatus.Running Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Deteniendo el servicio para volver a iniciarlo...")
                            service.Stop()
                        End If
                        If service.Status = ServiceControllerStatus.Stopped Then
                            WFServiceManagerBusiness.InsertAction(ServiceID, ServiceManagerAction.StartService)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Servicio detenido.")
                            WFServiceManagerBusiness.DeleteAction(CShort(id))
                        End If
                    Case ServiceManagerAction.None
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No hay accion a realizar.")
                        WFServiceManagerBusiness.DeleteAction(CShort(id))
                    Case Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Accion invalida.")
                        WFServiceManagerBusiness.DeleteAction(CShort(id))
                End Select
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No hay accion a realizar.")
                WFServiceManagerBusiness.DeleteAction(CShort(id))
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Function getServiceName(ByVal serviceID As Long) As String
        If Servers.Server.isOracle Then
            Dim sqlBuilder As New StringBuilder
            Try
                sqlBuilder.Append("select name from Zservice where serviceID=")
                sqlBuilder.Append(serviceID)

                Return Servers.Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())
            Finally
                sqlBuilder = Nothing
            End Try
        Else
            Dim parValues() As Object = {serviceID}
            Return Servers.Server.Con.ExecuteNonQuery(CommandType.Text, "select lastRunDate from ZserviceDates" & serviceID)
        End If
    End Function
    Private Sub CheckInactiveService(r As ServiceObj)
        lastRundate.text = r.Description


    End Sub

    Private Function Ultimaactualizacion() As Object
        Throw New NotImplementedException()
    End Function

    Private Function GetWinService(ByVal servicedesc As String) As ServiceController
        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Empty)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "# Obteniendo servicios del sistema")
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de servicios obtenidos: " & ServiceController.GetServices.Length)
        For Each serv As ServiceController In ServiceController.GetServices
            'ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando nombre servicio: " & serv.ServiceName.Trim & " en " & serv.MachineName)
            If serv.ServiceName.Trim = servicedesc.Trim Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Servicio encontrado: " & serv.ServiceName)
                Return serv
            End If
        Next
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Servicio " & servicedesc & " no encontrado.")
        Return Nothing
    End Function

    Dim _id As Int64
    Private lastRundate As Object

    Public Sub New(ByVal ID As Int64)
        _id = ID
    End Sub
End Class