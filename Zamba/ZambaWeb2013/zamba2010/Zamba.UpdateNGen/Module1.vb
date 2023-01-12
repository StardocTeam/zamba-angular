Module Module1

    Dim commandFile As String
    Dim parameter As String
    Dim myDir As String = Application.StartupPath & "\"
    'Const configfile As String = "UpdateConfig.xsd"

    Sub Main()
        Try
            Log.Open("Update.log", "Update Script Log")
            GetParameters()
            Log.WriteLines("Parametros de Entrada=" + commandFile)
        Catch ex As Exception
            Log.WriteLines("Excepcion " + ex.tostring)
        End Try
        Try
            Log.WriteLines("Esperando Que finalice Zamba Cliente")
            CloseProcesses()
        Catch ex As Exception
            Log.WriteLines("Excepcion " + ex.tostring)
        End Try
        Try
            Log.WriteLines("|Ejecutando Proceso de Actualizacion")
            executeScript()
        Catch ex As Exception
            Log.WriteLines("Excepcion " + ex.tostring)
        End Try
        Try
            Log.WriteLines("|Corriendo zamba")
            RunZamba()
        Catch ex As Exception
            Log.WriteLines("Excepcion " + ex.tostring)
        End Try
    End Sub
    Sub GetParameters()
        'Console.WriteLine("Leyendo " + (System.Environment.GetCommandLineArgs.Length - 1).ToString + "Parametros")
        If System.Environment.GetCommandLineArgs.Length > 1 Then
            Try
                commandFile = System.Environment.GetCommandLineArgs(1)
            Catch
                commandFile = String.Empty
                Exit Sub
            End Try
            '   Console.WriteLine(commandFile)
        End If
    End Sub
    Sub CloseProcesses()
        Trace.WriteLine("Verificando Procesos")
        Dim destino As String = myDir.Remove(myDir.LastIndexOf("\"))
        Dim proc As System.Diagnostics.Process

        'Se obtienen todos los ejecutables de Zamba que posiblemente puedan estar corriendo y se cierran.
        For Each exePath As String In IO.Directory.GetFiles(destino, "*.exe")
            If Not exePath.EndsWith("Zamba.UpdateNGen.exe") Then
                For Each proc In System.Diagnostics.Process.GetProcessesByName(IO.Path.GetFileNameWithoutExtension(exePath))
                    Try
                        Trace.WriteLine("Cerrando " & exePath)
                        proc.Kill()
                        Trace.WriteLine(exePath & "ha sido cerrado.")
                    Catch ex As Exception
                        If Not proc.HasExited Then
                            If proc.WaitForExit(1000) = False Then
                                Try
                                    Trace.WriteLine("Reintentando cerrar el proceso anterior...")
                                    proc.Kill()
                                    Trace.WriteLine(exePath & "ha sido cerrado.")
                                Catch ex2 As Exception
                                    Trace.WriteLine("El proceso no ha podido cerrarse.")
                                End Try
                            End If
                        End If
                    End Try
                Next
            End If
        Next
        Trace.WriteLine("Verificacion de procesos finalizada")
    End Sub
    Sub executeScript()
        Try
            Log.WriteLines("Renombrando Archivos failed")
            updateFailedFiles(myDir)
        Catch ex As Exception
            Log.WriteLines("Error Renombrando Archivos failed " & ex.ToString)
        End Try
        If commandFile <> String.Empty Then
            Log.WriteLine("Ejecutando shell " + commandFile)
            Shell(commandFile, AppWinStyle.NormalFocus, True)
        End If
    End Sub
    Sub RunZamba()
        Console.WriteLine("Corriendo Zamba")
        Shell("cliente.exe", AppWinStyle.MaximizedFocus, False)
    End Sub
    Private Sub updateFailedFiles(ByVal Dir As String)
        Log.WriteLine("Renombrando los failed de " + Dir)
        Dim di As New IO.DirectoryInfo(Dir)
        Dim files() As IO.FileInfo = di.GetFiles()

        Dim fi As IO.FileInfo

        For Each fi In files
            If fi.Name.IndexOf("failed") = 0 Then
                Log.WriteLine("Renombrando :" + fi.Name)
                Try
                    fi.CopyTo(fi.Name.Replace("failed", ""), True)
                    Log.WriteLines("Borrando Archivo " + fi.Name)
                    Try
                        fi.Delete()
                    Catch ex As Exception
                        Log.WriteLines("Ocurrio un error al Borrar " + fi.Name + "|" + ex.tostring)
                    End Try
                Catch ex As Exception
                    Log.WriteLine("Ocurrio un error al renombrar " + fi.Name + "|" + ex.tostring)
                End Try

            End If
        Next

        Dim directorys() As IO.DirectoryInfo = di.GetDirectories
        Dim dinfo As IO.DirectoryInfo
        For Each dinfo In directorys
            updateFailedFiles(dinfo.FullName)
        Next
    End Sub
End Module
