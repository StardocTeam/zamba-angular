Imports Zamba.Core
Imports Zamba.ZTimers
Imports System.ServiceProcess

Public Class WorkflowServiceManager

    Private _controlMonTimer As ZTimer
    Private _controlUpdTimer As ZTimer
    Private _cbTimer As New Threading.TimerCallback(AddressOf Manage)
    Private _upTimer As New Threading.TimerCallback(AddressOf UpdateService)
    Private _stateMon As Object
    Private _stateUpd As Object

    Protected Overrides Sub OnStart(ByVal args() As String)
        Try
            Dim _checkRate As Int64 = UserPreferences.getValue("CheckRate", Sections.WorkflowServiceManager, "20")

            Me._controlMonTimer = New ZTimer(_cbTimer, _stateMon, 0, _checkRate * 1000, 0, 24)
            Me._controlUpdTimer = New ZTimer(_upTimer, _stateUpd, 0, _checkRate * 1000, 0, 24)

            If CBool(UserPreferences.getValue("WithTrace", Sections.WorkflowServiceManager, "False")) Then
                Dim level As Int32 = CType(UserPreferences.getValue("TraceLevel", Sections.UserPreferences, "1"), Int32)
                ZTrace.SetLevel(level, "Service Manager")
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Private Sub Manage()
        Me._controlMonTimer.Pause()

        Try

            Trace.WriteLineIf(ZTrace.IsVerbose, "Verificando accion a realizar...")

            Dim actionid As Object
            Dim ServiceType As Object
            Dim actionType As Object

            Dim dsaction As DataSet = WFServiceManagerBusiness.GetActiveAction()

            If dsaction.Tables(0).Rows.Count > 0 Then
                actionid = dsaction.Tables(0).Rows(0)(0)
                ServiceType = dsaction.Tables(0).Rows(0)(1)
                actionType = dsaction.Tables(0).Rows(0)(2)
            Else
                dsaction = WFServiceManagerBusiness.GetNextAction()
                If dsaction.Tables(0).Rows.Count > 0 Then
                    actionid = dsaction.Tables(0).Rows(0)(0)
                    ServiceType = dsaction.Tables(0).Rows(0)(1)
                    actionType = dsaction.Tables(0).Rows(0)(2)
                    WFServiceManagerBusiness.ActiveAction(CShort(actionid))
                Else
                    Trace.WriteLineIf(ZTrace.IsVerbose, "No hay accion a realizar.")
                    Exit Sub
                End If
            End If

            Dim ServiceDesc As String = WFServiceManagerBusiness.GetServiceName(CShort(ServiceType))
            If String.IsNullOrEmpty(ServiceDesc) Then
                Trace.WriteLineIf(ZTrace.IsVerbose, "No existe ningun servicio con id " & ServiceType & " en la tabla de servicios.")
                WFServiceManagerBusiness.DeleteAction(CShort(actionid))
            End If
            If CShort(actionType) > 0 Then

                Trace.WriteLineIf(ZTrace.IsVerbose, "Verificando la existencia del servicio: " & ServiceDesc)

                If GetWinService(ServiceDesc) Is Nothing Then
                    Trace.WriteLineIf(ZTrace.IsVerbose, "El servicio no existe.")
                    WFServiceManagerBusiness.DeleteAction(CShort(actionid))
                    Exit Sub
                End If

                Select Case [Enum].Parse(GetType(ServiceManagerAction), actionType)

                    Case ServiceManagerAction.ForceStopService

                        If GetWinService(ServiceDesc).Status = ServiceControllerStatus.Running Then
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Deteniendo servicio...")
                            GetWinService(ServiceDesc).Stop()
                        End If
                        If GetWinService(ServiceDesc).Status = ServiceControllerStatus.Stopped Then
                            WFServiceManagerBusiness.DeleteAction(CShort(actionid))
                            Trace.WriteLineIf(ZTrace.IsVerbose, "El servicio fue detenido correctamente.")
                        End If

                    Case ServiceManagerAction.StartService

                        If GetWinService(ServiceDesc).Status = ServiceControllerStatus.Stopped Then
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Iniciando el servicio...")
                            GetWinService(ServiceDesc).Start()
                        End If
                        If GetWinService(ServiceDesc).Status = ServiceControllerStatus.Running Then
                            WFServiceManagerBusiness.DeleteAction(CShort(actionid))
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Se inicio el servicio correctamente.")
                        End If

                    Case ServiceManagerAction.ForceResetService

                        If GetWinService(ServiceDesc).Status = ServiceControllerStatus.Running Then
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Deteniendo el servicio para volver a iniciarlo...")
                            GetWinService(ServiceDesc).Stop()
                        End If
                        If GetWinService(ServiceDesc).Status = ServiceControllerStatus.Stopped Then
                            WFServiceManagerBusiness.InsertAction(ServiceType, ServiceManagerAction.StartService)
                            Trace.WriteLineIf(ZTrace.IsVerbose, "Servicio detenido.")
                            WFServiceManagerBusiness.DeleteAction(CShort(actionid))
                        End If

                    Case ServiceManagerAction.None
                        Trace.WriteLineIf(ZTrace.IsVerbose, "No hay accion a realizar.")
                        WFServiceManagerBusiness.DeleteAction(CShort(actionid))
                    Case Else
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Accion invalida.")
                        WFServiceManagerBusiness.DeleteAction(CShort(actionid))
                End Select

            Else
                Trace.WriteLineIf(ZTrace.IsVerbose, "No hay accion a realizar.")
                WFServiceManagerBusiness.DeleteAction(CShort(actionid))
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Me._controlMonTimer.Resume()
        End Try
    End Sub

    Private Sub UpdateService()

        Me._controlUpdTimer.Pause()

        Try

            WFUpdater.ManageUpdate()

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Me._controlUpdTimer.Resume()
        End Try

    End Sub



    Private Function GetWinService(ByVal servicedesc As String) As ServiceController
        For Each serv As ServiceController In ServiceController.GetServices
            If String.Compare(serv.ServiceName.Trim, servicedesc.Trim, True) = 0 Then Return serv
        Next
        Return Nothing
    End Function


    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        Me._controlMonTimer.Dispose()
        Me._controlUpdTimer.Dispose()
    End Sub

End Class
