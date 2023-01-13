Imports Zamba.Core
Imports System.IO

Public Class UpdateClient
    Dim pathDestino As String
    Dim pathOrigen As String
    Dim commandArgs As String
    Dim flagUserCredentials As Boolean
    Dim pathBackToExecute As String
    Dim flagIsZambaInstalled As Boolean = False
    Dim usr As String
    Dim pass As String
    Dim domain As String
    Dim decodePass As String
    'Dim logonType As ZImpersonalizeAdvance.LogonType
    Dim executeClientAfterUpdate As String
    Dim installWithAdminRights As Boolean
    Dim dirOriginal As String = String.Empty
    Dim dicAssembliesError As Dictionary(Of String, String) = Nothing
    Shared key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Shared iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Trace.WriteLine("Cerrando Procesos Activos de Zamba.")

            ClosingProcesses()

            Trace.WriteLine(String.Empty)
            Trace.WriteLine("---------------------------------------------")
            Trace.WriteLine("Comienza proceso de lectura de comandos.")

            Dim commandBuilder As New System.Text.StringBuilder()

            For Each s As String In Environment.GetCommandLineArgs()
                commandBuilder.Append(s)
                commandBuilder.Append(" ")
            Next

            Me.commandArgs = commandBuilder.ToString()
            Trace.WriteLine("Linea de comandos encontrada:")
            Trace.WriteLine(Me.commandArgs)
            'Obtengo los valores 
            Me.pathOrigen = Me.commandArgs.Split(Char.Parse("+"))(1)
            Trace.WriteLine("PathOrigen: " & pathOrigen)
            Me.pathDestino = Me.commandArgs.Split(Char.Parse("+"))(2)
            Trace.WriteLine("PathDestino: " & pathDestino)
            Me.pathBackToExecute = Me.commandArgs.Split(Char.Parse("+"))(3)
            Trace.WriteLine("PathBackToExecute:" & pathBackToExecute)
            Me.flagIsZambaInstalled = Boolean.Parse(Me.commandArgs.Split(Char.Parse("+"))(4).Trim())
            Trace.WriteLine("FlagIsZambaInstalled:" & flagIsZambaInstalled)

            Try
                Me.usr = Me.commandArgs.Split(Char.Parse("+"))(5).Trim()
                Trace.WriteLine("Usr:" & usr)

                Me.pass = Me.commandArgs.Split(Char.Parse("+"))(6).Trim()
                Trace.WriteLine("Pass:" & pass)
                If Not String.IsNullOrEmpty(pass) Then
                    Me.decodePass = Zamba.Tools.Encryption.DecryptString(pass.Replace("§", "+"), key, iv)
                End If

                Me.domain = Me.commandArgs.Split(Char.Parse("+"))(7).Trim()
                Trace.WriteLine("Domain:" & domain)

                Me.executeClientAfterUpdate = Me.commandArgs.Split(Char.Parse("+"))(8)
                Trace.WriteLine("ExecuteClientAfterUpdate: " & executeClientAfterUpdate)

                Boolean.TryParse(Me.commandArgs.Split(Char.Parse("+"))(9).Trim(), Me.installWithAdminRights)
                Trace.WriteLine("InstallZambaWithAdminRights:" & installWithAdminRights)

                'Me.logonType = [Enum].Parse(GetType(ZImpersonalizeAdvance.LogonType), Me.commandArgs.Split(Char.Parse("+"))(10).Trim())
                'Trace.WriteLine("Tipo de LOGON:" & Me.logonType.ToString)

                Trace.WriteLine("Actualizacion con credenciales")
                Me.flagUserCredentials = True
            Catch ex As Exception
                Trace.WriteLine("Actualizacion sin credenciales")
                Me.flagUserCredentials = False
            End Try

            Trace.WriteLine("Entrando en UpdateTheUpdaterWithCredentials ")
            UpdateTheUpdaterWithCredentials()
            Trace.WriteLine("Preparando Parametros para enviar")

            If pathDestino.Contains("\UpToDate\Zamba.UpdateClientNGen.exe") Then
                pathDestino = Application.StartupPath.ToString.Replace("\UpToDate\Zamba.UpdateClientNGen.exe", String.Empty)
            End If

            Dim dll As String = Application.StartupPath & "\Zamba.UpdateNGen.exe "
            Dim params As String = "+" & pathOrigen & "+" & pathDestino & "+" & pathBackToExecute & "+" & flagIsZambaInstalled & "+" & usr & "+" & pass & "+" & domain & "+" & executeClientAfterUpdate '& "+" & logonType
            Trace.WriteLine("Parametros enviados hacia el updateNGen:")
            Trace.WriteLine("PathOrigen:" & pathOrigen)
            Trace.WriteLine("PathDestino:" & pathDestino)
            Trace.WriteLine("PathBackToExecute:" & pathBackToExecute)
            Trace.WriteLine("FlagIsZambaInstaled:" & flagIsZambaInstalled)
            Trace.WriteLine("Usr:" & usr)
            Trace.WriteLine("pass:" & pass)
            Trace.WriteLine("domain:" & domain)
            Trace.WriteLine("ExecuteClientAfterUpdate:" & executeClientAfterUpdate)

            If installWithAdminRights Then
                Trace.WriteLine("Ejecutando UpdateNGen.exe mediante Process.Start")
                Dim p As New Diagnostics.Process()
                Dim processStartInfo As New Diagnostics.ProcessStartInfo
                processStartInfo.FileName = dll
                processStartInfo.Arguments = params
                processStartInfo.Verb = "runas"
                processStartInfo.WindowStyle = ProcessWindowStyle.Normal
                processStartInfo.UseShellExecute = True
                p.Start(processStartInfo)
            Else
                Trace.WriteLine("Ejecutando UpdateNGen.exe mediante Shell")
                Shell(dll & params, AppWinStyle.NormalFocus, False)
            End If
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido un error al actualizar Zamba. Comuníquese con el área de sistemas.", "Error de actualización", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Trace.WriteLine("Ha ocurrido un error al actualizar UpdateNGen.")
            Trace.WriteLine(ex.Message)
        Finally
            'cerramos el updateClientNGen
            Me.Close()
        End Try
    End Sub

    Private Sub CerrarProcesosZamba()

        Try
            Dim proc As System.Diagnostics.Process
            Dim strPathdestino As String = Application.StartupPath.ToString
            'Se obtienen todos los ejecutables de Zamba que posiblemente puedan estar corriendo y se cierran.
            If strPathdestino.Contains("\UpToDate") Then
                strPathdestino = Application.StartupPath.ToString.Replace("\UpToDate", " ")
            End If

            For Each exePath As String In IO.Directory.GetFiles(strPathdestino.Trim, "*.exe")
                If Not exePath.EndsWith("Zamba.UpdateClientNGen.exe") Then
                    For Each proc In System.Diagnostics.Process.GetProcessesByName(IO.Path.GetFileNameWithoutExtension(exePath))

                        Try
                            Trace.WriteLine("Cerrando " & exePath)
                            Trace.WriteLine("Usuario actual con cual se cierra el proceso: " & Environment.UserName)
                            proc.Kill()
                            Trace.WriteLine("Cerrado Correctamente: " & exePath)
                        Catch ex As Exception
                            If Not proc.HasExited Then
                                If proc.WaitForExit(1000) = False Then
                                    Try
                                        Trace.WriteLine("Reintentando cerrar el proceso anterior...")
                                        proc.Kill()
                                    Catch ex2 As Exception
                                        Trace.WriteLine("El proceso no ha podido cerrarse.")
                                    End Try
                                End If
                            End If
                        End Try
                    Next
                End If
            Next
        Catch ex As Exception
            Trace.WriteLine("Ha ocurrido un error al cerrar los procesos de Zamba.")
            Trace.WriteLine(ex.ToString())
        End Try
    End Sub

    Private Sub ClosingProcesses()
        Dim proc As Diagnostics.Process
        Trace.WriteLine("Cerrando procesos")
        ''Si el Cliente esta abierto se esperan 8 segundo para cerrarse , esto le daria el tiempo al cliente para que pueda 
        ''Actualizar la version en la base.
        For Each proc In Diagnostics.Process.GetProcessesByName("Zamba Cliente")
            If Not proc.HasExited Then
                If proc.WaitForExit(8000) = False Then
                    Trace.WriteLine("Zamba Cliente")
                    proc.Kill()
                End If
            End If
        Next

        For Each proc In Diagnostics.Process.GetProcessesByName("Zamba Cliente.exe")
            If Not proc.HasExited Then
                If proc.WaitForExit(8000) = False Then
                    Trace.WriteLine("Zamba Cliente.exe")
                    proc.Kill()
                End If
            End If
        Next

        For Each proc In Diagnostics.Process.GetProcessesByName("Cliente")
            If Not proc.HasExited Then
                If proc.WaitForExit(8000) = False Then
                    Trace.WriteLine("Cliente")
                    proc.Kill()
                End If
            End If
        Next

        For Each proc In Diagnostics.Process.GetProcessesByName("Cliente.exe")
            If Not proc.HasExited Then
                If proc.WaitForExit(8000) = False Then
                    Trace.WriteLine("Cliente.exe")
                    proc.Kill()
                End If
            End If
        Next
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Try
            Dim exceptionsPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software\Exceptions"
            If Not IO.Directory.Exists(exceptionsPath) Then IO.Directory.CreateDirectory(exceptionsPath)
            Trace.Listeners.Add(New TextWriterTraceListener(exceptionsPath & "\Trace UpdateClientNGen " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".txt"))
            Trace.AutoFlush = True
            Trace.WriteLine("Corriendo actualizacion UpdateClientNGen version:" & Application.ProductVersion)
        Catch
        End Try
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub UpdateTheUpdaterWithCredentials()
        Try
            Dim desimpersonalizar As Boolean = False
            If String.IsNullOrEmpty(usr) OrElse String.IsNullOrEmpty(pass) OrElse String.IsNullOrEmpty(domain) Then
                Trace.WriteLine("Credenciales no encontradas, verifique que se hayan seteado usuario, password y dominio")

                UpdateTheUpdater()

            Else
                Trace.WriteLine("Usuario para actualizacion encontrado: " & usr)

                If Not String.IsNullOrEmpty(decodePass) Then
                    Dim zImper As New ZImpersonalize
                    'Dim zImper As New ZImpersonalizeAdvance(usr, decodePass, domain, logonType, AddressOf UpdateTheUpdater)
                    Trace.WriteLine("Se crea zImper")

                    Try
                        Trace.WriteLine("Impersonalizando UpdateTheUpdater")
                        Trace.WriteLine("Usr:" & usr)
                        Trace.WriteLine("domain:" & domain)

                        desimpersonalizar = zImper.impersonateValidUser(usr, domain, decodePass)
                        If desimpersonalizar Then
                            Trace.WriteLine("Updater impersonalizado")
                        Else
                            Trace.WriteLine("No se ha podido impesonalizar")
                        End If

                        UpdateTheUpdater()
                    Catch ex As Exception
                        Trace.WriteLine(ex.ToString)
                    Finally
                        Try
                            If desimpersonalizar Then
                                Trace.WriteLine("Desimpesonalizando UpdateTheUpdater")
                                Trace.WriteLine("Antes de desloguear: " & Environment.UserName)
                                zImper.undoImpersonation()
                                Trace.WriteLine("Despues de desloguear: " & Environment.UserName)
                                Trace.WriteLine("UpdateTheUpdater desimpersonalizado")
                            End If
                        Catch ex As Exception
                            Trace.WriteLine(ex.ToString())
                        End Try
                        zImper = Nothing
                    End Try
                Else
                    Trace.WriteLine("Password no decodeada")
                End If
            End If
        Catch ex As Exception
            Trace.WriteLine(ex.ToString)
        End Try
        Trace.WriteLine("Saliendo de UpdateTheUpdaterWithCredentials ")
    End Sub

    Private Sub UpdateTheUpdater()
        Try
            Dim DestPath As String = pathDestino & "\UpToDate\"
            Dim OrigPath As String = pathOrigen & "\UpToDate\"

            If Not Directory.Exists(OrigPath) Then
                Trace.WriteLine("ERROR: No se ha encontrado el directorio de origen: " & OrigPath)
                Exit Sub
            End If
            If Not Directory.Exists(DestPath) Then
                Trace.WriteLine("No se ha encontrado el directorio de destino: " & OrigPath)
                Trace.WriteLine("Creando directorio")
                Directory.CreateDirectory(DestPath)
            End If

            For Each fileToCopy As String In Directory.GetFiles(OrigPath)
                If Not (fileToCopy.ToLower().Contains("tools") Or fileToCopy.ToLower().Contains("updateclientngen")) Then
                    Trace.WriteLine("Copiando " & fileToCopy)
                    Try
                        File.Copy(fileToCopy, DestPath & Path.GetFileName(fileToCopy), True)
                    Catch ex As Exception
                        Trace.WriteLine(ex.ToString)
                    End Try
                End If
            Next
            Trace.WriteLine("Todos los archivos han sido copiados con exito.")
        Catch ex As Exception
            Trace.WriteLine("Ha ocurrido un error al copiar los archivos.")
            Trace.WriteLine(ex.ToString)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class