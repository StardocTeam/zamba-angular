Imports Zamba.Tools
Imports Zamba.Membership
Imports System.Text


Public Class ZException
  Inherits System.Exception

    Public Shared Event LogToDB(ByVal ex As Exception)
    Private Shared logExceptions As Boolean = True

    Public Shared Function BuildLog(ByVal ex As Exception, ByVal callStack As String) As String
        Dim sb As New Text.StringBuilder

        Try
            sb.AppendLine(vbCrLf & "==========================================[ERROR]==========================================")
            sb.AppendLine(ex.ToString)
            sb.AppendLine(vbCrLf)
            sb.AppendLine("Versión de Zamba: " & Application.ProductVersion)
            sb.AppendLine("Aplicacion: " & Application.ExecutablePath)
            sb.AppendLine("===========================================================================================" & vbCrLf)
        Catch ex2 As Exception
        End Try

        Return sb.ToString()
    End Function

    Public Shared Sub Log(ByVal Ex As System.Exception)
        If logExceptions Then
            logExceptions = False

            Try
                If (Ex.Message.Contains("ha demorado")) Then
                    WriteFile(Ex, "PerformanceExceptions")
                    RaiseEvent LogToDB(Ex)
                ElseIf (Ex.Message.Contains("Zip exception.Can't locate end of central directory record")) Then
                    WriteFile(Ex, "Indexer")
                    RaiseEvent LogToDB(Ex)
                ElseIf (Ex.Message.Contains("URI no válido")) Then
                    WriteFile(Ex, "Workflow")
                    RaiseEvent LogToDB(Ex)
                ElseIf (Ex.Message.Contains("resolver la matriz estan vacios") OrElse ex.Message.Contains("Se finaliza la ejecución de la tarea debido a un error en la regla: stop del mensaje")) Then
                ElseIf (Ex.Message.Contains("The directive 'control' is unknown")) Then
                ElseIf (Ex.Message.Contains("A network-related") OrElse Ex.Message.Contains("A transport-level error has occurred when receiving results from the server")) Then
                    WriteFile(Ex, "DB Connection Error")
                ElseIf (Ex.Message.Contains("Err 7002")) Then
                    WriteFile(Ex, "IdExpirado")
                    RaiseEvent LogToDB(Ex)
                Else
                    WriteFile(Ex)
                    RaiseEvent LogToDB(Ex)
                End If
            Catch exc As Exception
                Dim varExv = exc
            Finally
                logExceptions = True
            End Try


        End If
    End Sub

    Private Shared Sub WriteFile(ByVal ex As System.Exception, ByVal Optional SubFolder As String = "")
        If ex IsNot Nothing Then
            Dim dsEx As DsExcep = Nothing
            Dim dir As IO.DirectoryInfo
            Dim file As IO.FileInfo
            Dim fileName, winUser, machine As String

            Try
                winUser = Environment.UserName
                machine = Environment.MachineName

                Dim sb As New StringBuilder()

                If ex.Data.Keys.Count > 0 Then
                    For index = 0 To ex.Data.Keys.Count - 1
                        sb.AppendLine(ex.Data.Keys(index) + ":" + ex.Data.Values(index).ToString)
                        sb.AppendLine(Environment.NewLine)
                        sb.AppendLine(Environment.NewLine)
                    Next
                End If


                dsEx = New DsExcep
                dsEx.Excep.AddExcepRow(Now.ToString, winUser, ex.Message, ex.ToString, sb.ToString, String.Empty, String.Empty, winUser, machine, Application.ProductVersion)
                dsEx.AcceptChanges()

                Try
                    If SubFolder.Length > 0 Then
                        dir = New IO.DirectoryInfo(MembershipHelper.AppTempPath & "\Exceptions\" & DateTime.Now.ToString("yyyy-MM-dd") & "\" & SubFolder & "\Exceptions")
                    Else
                        dir = New IO.DirectoryInfo(MembershipHelper.AppTempPath & "\Exceptions\" & DateTime.Now.ToString("yyyy-MM-dd") & "\Exceptions")
                    End If

                    If dir.Exists = False Then
                        dir.Create()
                    End If
                Catch
                    Try
                        dir = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & "\Exceptions")
                        If dir.Exists = False Then
                            dir.Create()
                        End If
                    Catch
                        'Si falla, lo mas probable es que no existan permisos en la carpeta de instalación de Zamba
                    End Try
                End Try

                fileName = dir.FullName & GetExceptionFileName(SubFolder)
                file = New IO.FileInfo(fileName)
                dsEx.WriteXml(file.FullName, XmlWriteMode.IgnoreSchema)

            Catch ex1 As Exception

            Finally
                file = Nothing
                dir = Nothing
                If dsEx IsNot Nothing Then
                    dsEx.Dispose()
                    dsEx = Nothing
                End If
            End Try
        End If
    End Sub



    Private Shared Function GetExceptionScreenFileName() As Object
        Return "\Excep " & Environment.MachineName & " " & Application.ExecutablePath.Substring(Application.ExecutablePath.LastIndexOf("\") + 1) & " " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".png"
    End Function

    Private Shared Function GetExceptionFileName(SubFolder As String) As String
        If (MembershipHelper.CurrentUser Is Nothing) Then
            Return "\Excep " & SubFolder & " " & Environment.MachineName & " " & Application.ExecutablePath.Substring(Application.ExecutablePath.LastIndexOf("\") + 1) & " " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".txt"
        Else
            Return "\Excep " & SubFolder & " " & MembershipHelper.CurrentUser.Name & " " & Environment.MachineName & " " & Application.ExecutablePath.Substring(Application.ExecutablePath.LastIndexOf("\") + 1) & " " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".txt"
        End If

    End Function

    ''' <summary>
    ''' Loguea la exception no atrapada por Zamba en el trace y/o en un log de exception.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Shared Sub ThreadExceptionHandler(ByVal sender As Object, ByVal e As EventArgs)
        Dim stack As New System.Diagnostics.StackTrace()
        Dim strErr As String = "=== Excepcion no controlada ===" & vbCrLf & stack.ToString

        Try
            Trace.WriteLine(strErr)
        Catch ex As Exception
        End Try

        Log(New Exception(strErr))
    End Sub

    Public Shared Sub CleanExceptions()
        Try
            CleanExceptions(New IO.DirectoryInfo(Membership.MembershipHelper.AppTempPath & "\Exceptions"))
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub CleanExceptions(dir As IO.DirectoryInfo)
        Try
            If dir.Exists = False Then
                dir.Create()
            End If
            Dim archivos() As IO.FileInfo = Dir.GetFiles

            For Each fi As IO.FileInfo In Dir.GetFiles
                If fi.CreationTime.Month < Now.Month Then
                    Try
                        fi.Delete()
                    Catch ex As Exception
                    End Try
                End If
            Next
            For Each di As IO.DirectoryInfo In dir.GetDirectories
                CleanExceptions(di)
            Next
        Catch ex As Exception
        End Try
    End Sub
End Class
