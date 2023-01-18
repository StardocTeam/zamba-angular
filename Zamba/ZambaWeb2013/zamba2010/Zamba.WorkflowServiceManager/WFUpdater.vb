Imports System.ServiceProcess
Imports Zamba.Core
Imports System.IO

Public Class WFUpdater

    Private Shared wfSMBusiness As WFServiceManagerBusiness

    Public Shared Sub ManageUpdate()
        Try

            wfSMBusiness = New WFServiceManagerBusiness

            If wfSMBusiness.MustUpdate Then

                Trace.WriteLineIf(ZTrace.IsVerbose, "Se actualizara el servicio de WF de la version " & wfSMBusiness.VersionActual & " a la version " & wfSMBusiness.VersionNueva)

                'Si se fuerza el stop se para el servicio sin importar que este ejecutando, sino se espera
                'a que termine de procesar los WF
                If wfSMBusiness.ForzarUpdate Then

                    Trace.WriteLineIf(ZTrace.IsVerbose, "Tipo de actualizacion: FORZADA")

                    If StopService() Then
                        UpdateService()
                    End If

                Else

                    Trace.WriteLineIf(ZTrace.IsVerbose, "Tipo de actualizacion: normal")
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Se le indico al servicio de WF que se detenga cuando termine los procesos")

                    If GetWFService.Status = ServiceControllerStatus.Running Then
                        'Indicarle al servicio que pare la ejecucion
                        If ZOptBusiness.GetValue("SVMGAction") Is Nothing Then
                            ZOptBusiness.Insert("SVMGAction", "2")
                        Else
                            ZOptBusiness.Update("SVMGAction", "2")
                        End If
                    End If

                    If GetWFService.Status = ServiceControllerStatus.Stopped Then
                        'Si el servicio esta parado se puede actualizar
                        UpdateService()
                    End If

                End If

            End If





            If wfSMBusiness.MonMustUpdate Then

                Trace.WriteLineIf(ZTrace.IsVerbose, "Se actualizara el servicio de Monitoreo de la version " & wfSMBusiness.MonVersionActual & " a la version " & wfSMBusiness.MonVersionNueva)

                'Si se fuerza el stop se para el servicio sin importar que este ejecutando, sino se espera
                'a que termine de procesar los WF
                If wfSMBusiness.MonForzarUpdate Then

                    Trace.WriteLineIf(ZTrace.IsVerbose, "Tipo de actualizacion: FORZADA")

                    If StopMonService() Then
                        UpdateMONService()
                    End If

                Else

                    Trace.WriteLineIf(ZTrace.IsVerbose, "Tipo de actualizacion: normal")
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Se le indico al servicio de WF que se detenga cuando termine los procesos")

                    If GetMonitoreoService.Status = ServiceControllerStatus.Running Then
                        'Indicarle al servicio que pare la ejecucion
                        If ZOptBusiness.GetValue("SVMGAction") Is Nothing Then
                            ZOptBusiness.Insert("SVMGAction", "13")
                        Else
                            ZOptBusiness.Update("SVMGAction", "13")
                        End If
                    End If

                    If GetMonitoreoService.Status = ServiceControllerStatus.Stopped Then
                        'Si el servicio esta parado se puede actualizar
                        UpdateMONService()
                    End If

                End If

            End If




            wfSMBusiness = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Shared Sub UpdateService()

        Dim PathBackup As String
        Dim ResUpdate As Boolean = False
        Dim WithBackup As Boolean = True

        Try

            If Not String.IsNullOrEmpty(wfSMBusiness.PathBackup) Then
                PathBackup = wfSMBusiness.PathBackup & "\BKP_SRV\" & Date.Now.ToString("yyyyMMdd") & "_v" & wfSMBusiness.VersionActual
            Else
                'si no se configura la ruta no hay backup
                WithBackup = False
            End If

            Trace.WriteLineIf(ZTrace.IsVerbose, "Comienza la actualizacion...")
            Trace.WriteLineIf(ZTrace.IsVerbose, "Path del servicio nuevo: " & wfSMBusiness.PathNueva)

            If WithBackup Then

                Trace.WriteLineIf(ZTrace.IsVerbose, "Path donde se hara el backup del servicio actual: " & PathBackup)

                'Backup del servicio actual
                If CopyDirectory(wfSMBusiness.PathActual, PathBackup, True) Then

                    Trace.WriteLineIf(ZTrace.IsVerbose, "Copiando archivos del servicio nuevo")

                    'Copiar el servicio nuevo a la carpeta donde esta el actual
                    If CopyDirectory(wfSMBusiness.PathNueva, wfSMBusiness.PathActual, True) Then

                        Trace.WriteLineIf(ZTrace.IsVerbose, "Iniciando el servicio nuevo (" & wfSMBusiness.VersionNueva & ")")

                        If Not StartService() Then

                            Trace.WriteLineIf(ZTrace.IsVerbose, "Recuperando el servicio anterior (" & wfSMBusiness.VersionActual & ")")

                            'Vaciar la carpeta actual para eliminar cualquier archivo que no estuviera en el backup
                            EmptyDirectory(wfSMBusiness.PathActual)

                            'Copiar el backup que se hizo a la carpeta donde esta el actual
                            If Not CopyDirectory(PathBackup, wfSMBusiness.PathActual, True) Then

                                Trace.WriteLineIf(ZTrace.IsVerbose, "No se pudieron recuperar los archivos del servicio anterior (" & wfSMBusiness.VersionActual & ")")

                            Else

                                Trace.WriteLineIf(ZTrace.IsVerbose, "Intentando volver a iniciar el servicio anterior (" & wfSMBusiness.VersionActual & ")")
                                StartService()

                            End If

                        Else
                            'Actualizacion completa
                            ResUpdate = True
                            wfSMBusiness.ServiceUpdated()
                        End If

                    Else
                        Trace.WriteLineIf(ZTrace.IsVerbose, "No se pudo realizar la copia del servicio nuevo")
                    End If

                Else
                    Trace.WriteLineIf(ZTrace.IsVerbose, "No se pudo realizar el backup del servicio actual")
                End If

                If Not ResUpdate Then
                    'Error al actualizar
                    wfSMBusiness.ServiceUpdateFailed()
                End If

            Else

                Trace.WriteLineIf(ZTrace.IsVerbose, "Se realizara la actualizacion sin un backup previo")
                Trace.WriteLineIf(ZTrace.IsVerbose, "Copiando archivos del servicio nuevo")

                'Copiar el servicio nuevo a la carpeta donde esta el actual
                If CopyDirectory(wfSMBusiness.PathNueva, wfSMBusiness.PathActual, True) Then

                    Trace.WriteLineIf(ZTrace.IsVerbose, "Iniciando el servicio nuevo (" & wfSMBusiness.VersionNueva & ")")

                    If StartService() Then
                        'Actualizacion completa
                        ResUpdate = True
                        wfSMBusiness.ServiceUpdated()
                    Else
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Actualizacion fallida.")
                    End If

                Else
                    Trace.WriteLineIf(ZTrace.IsVerbose, "No se pudo realizar la copia del servicio nuevo")
                End If

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Shared Sub UpdateMONService()

        Dim PathBackup As String
        Dim ResUpdate As Boolean = False
        Dim WithBackup As Boolean = True

        Try

            If Not String.IsNullOrEmpty(wfSMBusiness.PathBackup) Then
                PathBackup = wfSMBusiness.MonPathBackup & "\BKP_SRV\" & Date.Now.ToString("yyyyMMdd") & "_v" & wfSMBusiness.MonVersionActual
            Else
                'si no se configura la ruta no hay backup
                WithBackup = False
            End If

            Trace.WriteLineIf(ZTrace.IsVerbose, "Comienza la actualizacion...")
            Trace.WriteLineIf(ZTrace.IsVerbose, "Path del servicio monitoreo nuevo: " & wfSMBusiness.MonPathNueva)

            If WithBackup Then

                Trace.WriteLineIf(ZTrace.IsVerbose, "Path donde se hara el backup del servicio actual: " & PathBackup)

                'Backup del servicio actual
                If CopyDirectory(wfSMBusiness.MonPathActual, PathBackup, True) Then

                    Trace.WriteLineIf(ZTrace.IsVerbose, "Copiando archivos del servicio nuevo")

                    'Copiar el servicio nuevo a la carpeta donde esta el actual
                    If CopyDirectory(wfSMBusiness.MonPathNueva, wfSMBusiness.MonPathActual, True) Then

                        Trace.WriteLineIf(ZTrace.IsVerbose, "Iniciando el servicio  monitoreo nuevo (" & wfSMBusiness.MonVersionNueva & ")")

                        If Not StartMonService() Then

                            Trace.WriteLineIf(ZTrace.IsVerbose, "Recuperando el servicio anterior (" & wfSMBusiness.MonVersionActual & ")")

                            'Vaciar la carpeta actual para eliminar cualquier archivo que no estuviera en el backup
                            EmptyDirectory(wfSMBusiness.MonPathActual)

                            'Copiar el backup que se hizo a la carpeta donde esta el actual
                            If Not CopyDirectory(PathBackup, wfSMBusiness.MonPathActual, True) Then

                                Trace.WriteLineIf(ZTrace.IsVerbose, "No se pudieron recuperar los archivos del servicio anterior (" & wfSMBusiness.MonVersionActual & ")")

                            Else

                                Trace.WriteLineIf(ZTrace.IsVerbose, "Intentando volver a iniciar el servicio anterior (" & wfSMBusiness.MonVersionActual & ")")
                                StartMonService()

                            End If

                        Else
                            'Actualizacion completa
                            ResUpdate = True
                            wfSMBusiness.ServiceMonUpdated()
                        End If

                    Else
                        Trace.WriteLineIf(ZTrace.IsVerbose, "No se pudo realizar la copia del servicio nuevo")
                    End If

                Else
                    Trace.WriteLineIf(ZTrace.IsVerbose, "No se pudo realizar el backup del servicio actual")
                End If

                If Not ResUpdate Then
                    'Error al actualizar
                    wfSMBusiness.ServiceMonUpdateFailed()
                End If

            Else

                Trace.WriteLineIf(ZTrace.IsVerbose, "Se realizara la actualizacion sin un backup previo")
                Trace.WriteLineIf(ZTrace.IsVerbose, "Copiando archivos del servicio nuevo")

                'Copiar el servicio nuevo a la carpeta donde esta el actual
                If CopyDirectory(wfSMBusiness.MonPathNueva, wfSMBusiness.MonPathActual, True) Then

                    Trace.WriteLineIf(ZTrace.IsVerbose, "Iniciando el servicio nuevo (" & wfSMBusiness.MonVersionNueva & ")")

                    If StartMonService() Then
                        'Actualizacion completa
                        ResUpdate = True
                        wfSMBusiness.ServiceMonUpdated()
                    Else
                        Trace.WriteLineIf(ZTrace.IsVerbose, "Actualizacion fallida.")
                    End If

                Else
                    Trace.WriteLineIf(ZTrace.IsVerbose, "No se pudo realizar la copia del servicio nuevo")
                End If

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Private Shared Function StopService()

        Dim _checkServiceStatusCount As Int64 = UserPreferences.getValue("CheckServiceStatusCount", Sections.WorkflowServiceManager, "3")
        Dim _checkServiceStatusInterval As Int64 = UserPreferences.getValue("CheckServiceStatusInterval", Sections.WorkflowServiceManager, "10") * 1000
        Dim i As Integer = 0

        If GetWFService.Status = ServiceControllerStatus.Running Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "Deteniendo el servicio ...")
            GetWFService.Stop()
        End If

        While GetWFService.Status <> ServiceControllerStatus.Stopped And i < _checkServiceStatusCount
            Trace.WriteLineIf(ZTrace.IsVerbose, "El servicio todavia no se detuvo, esperarando " & _checkServiceStatusInterval & " segundos...")
            System.Threading.Thread.Sleep(_checkServiceStatusInterval)
            i = i + 1
        End While

        If GetWFService.Status = ServiceControllerStatus.Stopped Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "Servicio detenido.")
        Else
            Trace.WriteLineIf(ZTrace.IsVerbose, "No se pudo detener el servicio.")
            Return False
        End If

        Return True

    End Function




    Private Shared Function StopMonService()

        Dim _checkServiceStatusCount As Int64 = UserPreferences.getValue("CheckServiceStatusCount", Sections.WorkflowServiceManager, "3")
        Dim _checkServiceStatusInterval As Int64 = UserPreferences.getValue("CheckServiceStatusInterval", Sections.WorkflowServiceManager, "10") * 1000
        Dim i As Integer = 0

        If GetMonitoreoService.Status = ServiceControllerStatus.Running Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "Deteniendo el servicio ...")
            GetMonitoreoService.Stop()
        End If

        While GetMonitoreoService.Status <> ServiceControllerStatus.Stopped And i < _checkServiceStatusCount
            Trace.WriteLineIf(ZTrace.IsVerbose, "El servicio todavia no se detuvo, esperarando " & _checkServiceStatusInterval & " segundos...")
            System.Threading.Thread.Sleep(_checkServiceStatusInterval)
            i = i + 1
        End While

        If GetMonitoreoService.Status = ServiceControllerStatus.Stopped Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "Servicio detenido.")
        Else
            Trace.WriteLineIf(ZTrace.IsVerbose, "No se pudo detener el servicio.")
            Return False
        End If

        Return True

    End Function




    Private Shared Function StartService()

        Dim _checkServiceStatusCount As Int64 = UserPreferences.getValue("CheckServiceStatusCount", Sections.WorkflowServiceManager, "3")
        Dim _checkServiceStatusInterval As Int64 = UserPreferences.getValue("CheckServiceStatusInterval", Sections.WorkflowServiceManager, "10") * 1000
        Dim i As Integer = 0

        If GetWFService.Status = ServiceControllerStatus.Stopped Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "Iniciando el servicio...")
            GetWFService.Start()
        End If

        While GetWFService.Status <> ServiceControllerStatus.Running And i < _checkServiceStatusCount
            Trace.WriteLineIf(ZTrace.IsVerbose, "El servicio todavia no se inicio, esperarando " & _checkServiceStatusInterval & " segundos...")
            System.Threading.Thread.Sleep(_checkServiceStatusInterval)
            i = i + 1
        End While

        If GetWFService.Status = ServiceControllerStatus.Running Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "Se inicio el servicio correctamente.")
        Else
            Trace.WriteLineIf(ZTrace.IsVerbose, "No se pudo iniciar el servicio.")
            Return False
        End If

        Return True

    End Function



    Private Shared Function StartMonService()

        Dim _checkServiceStatusCount As Int64 = UserPreferences.getValue("CheckServiceStatusCount", Sections.WorkflowServiceManager, "3")
        Dim _checkServiceStatusInterval As Int64 = UserPreferences.getValue("CheckServiceStatusInterval", Sections.WorkflowServiceManager, "10") * 1000
        Dim i As Integer = 0

        If GetMonitoreoService.Status = ServiceControllerStatus.Stopped Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "Iniciando el servicio...")
            GetMonitoreoService.Start()
        End If

        While GetMonitoreoService.Status <> ServiceControllerStatus.Running And i < _checkServiceStatusCount
            Trace.WriteLineIf(ZTrace.IsVerbose, "El servicio todavia no se inicio, esperarando " & _checkServiceStatusInterval & " segundos...")
            System.Threading.Thread.Sleep(_checkServiceStatusInterval)
            i = i + 1
        End While

        If GetMonitoreoService.Status = ServiceControllerStatus.Running Then
            Trace.WriteLineIf(ZTrace.IsVerbose, "Se inicio el servicio correctamente.")
        Else
            Trace.WriteLineIf(ZTrace.IsVerbose, "No se pudo iniciar el servicio.")
            Return False
        End If

        Return True

    End Function


    Private Shared Function GetWFService() As ServiceController
        For Each serv As ServiceController In ServiceController.GetServices
            If String.Compare(serv.ServiceName, "Zamba Servicio de Workflow", True) = 0 Then Return serv
        Next
        Return Nothing
    End Function

    Private Shared Function GetThreadPoolService() As ServiceController
        For Each serv As ServiceController In ServiceController.GetServices
            If String.Compare(serv.ServiceName, "Zamba Servicio de ThreadPool", True) = 0 Then Return serv
        Next
        Return Nothing
    End Function

    Private Shared Function GetMonitoreoService() As ServiceController
        For Each serv As ServiceController In ServiceController.GetServices
            If String.Compare(serv.ServiceName, "Zamba Servicio de Monitoreo V2", True) = 0 Then Return serv
        Next
        Return Nothing
    End Function

    Private Shared Function EmptyDirectory(ByVal Path As String) As Boolean

        Dim ret As Boolean = True

        Try

            If Not Path.EndsWith("\") Then Path = Path & "\"

            If Directory.Exists(Path) Then

                For Each fls As String In Directory.GetFiles(Path)
                    Dim flinfo As FileInfo = New FileInfo(fls)
                    flinfo.Delete()
                Next

                For Each drs As String In Directory.GetDirectories(Path)
                    Dim drinfo As DirectoryInfo = New DirectoryInfo(drs)

                    If Not EmptyDirectory(Path) Then
                        ret = False
                    End If
                Next

            Else
                Trace.WriteLineIf(ZTrace.IsVerbose, "La carpeta " & Path & " no existe.")
                ret = False
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            ret = False
        End Try

        Return ret

    End Function

    Private Shared Function CopyDirectory(ByVal SourcePath As String, ByVal DestinationPath As String, ByVal OverwriteExisting As Boolean) As Boolean

        Dim ret As Boolean = True

        Try

            If Not SourcePath.EndsWith("\") Then SourcePath = SourcePath & "\"
            If Not DestinationPath.EndsWith("\") Then DestinationPath = DestinationPath & "\"

            If Directory.Exists(SourcePath) Then

                If Not Directory.Exists(DestinationPath) Then
                    Directory.CreateDirectory(DestinationPath)
                End If

                For Each fls As String In Directory.GetFiles(SourcePath)
                    Dim flinfo As FileInfo = New FileInfo(fls)
                    flinfo.CopyTo(DestinationPath + flinfo.Name, OverwriteExisting)
                Next

                For Each drs As String In Directory.GetDirectories(SourcePath)
                    Dim drinfo As DirectoryInfo = New DirectoryInfo(drs)

                    If Not drinfo.Name.StartsWith("BKP_SRV") Then
                        If Not CopyDirectory(drs, DestinationPath + drinfo.Name, OverwriteExisting) Then
                            ret = False
                        End If
                    End If
                Next

            Else
                Trace.WriteLineIf(ZTrace.IsVerbose, "La carpeta " & SourcePath & " no existe.")
                ret = False
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            ret = False
        End Try

        Return ret

    End Function

End Class