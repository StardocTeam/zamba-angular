Imports System.IO
Imports Zamba.Membership
Imports Zamba.Tools.Capture

Public Class ZException
    Inherits System.Exception

    Public Shared Event LogToDB(ByVal ex As Exception)
    Private Shared logExceptions As Boolean = True
    Public Shared ModuleName As String = "Zamba"

    Public Shared Function BuildLog(ByVal ex As Exception, ByVal callStack As String) As String
        Dim sb As New System.Text.StringBuilder

        Try
            sb.AppendLine(vbCrLf & "==========================================[ERROR]==========================================")
            sb.AppendLine(ex.ToString)
            sb.AppendLine(vbCrLf)
            sb.AppendLine("Versi�n de Zamba: " & Application.ProductVersion)
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
                If (Ex.Message.Contains("La siguiente consulta ha demorado")) Then
                    WriteFile(Ex, "Performance")
                ElseIf (Ex.Message.Contains("Zip exception.Can't locate end of central directory record")) Then
                    WriteFile(Ex, "Indexer")
                ElseIf (Ex.Message.Contains("URI no v�lido")) Then
                    WriteFile(Ex, "Workflow")
                Else
                    WriteFile(Ex)
                    RaiseEvent LogToDB(Ex)
                End If
            Catch
            End Try
            logExceptions = True
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

                dsEx = New DsExcep
                dsEx.Excep.AddExcepRow(Now.ToString, winUser, ex.Message, ex.ToString, String.Empty, String.Empty, String.Empty, winUser, machine, Application.ProductVersion)
                dsEx.AcceptChanges()

                Try
                    If SubFolder.Length > 0 Then
                        dir = New IO.DirectoryInfo(MembershipHelper.AppTempPath & "\Exceptions\" & SubFolder)
                    Else
                        dir = New IO.DirectoryInfo(MembershipHelper.AppTempPath & "\Exceptions")
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
                        'Si falla, lo mas probable es que no existan permisos en la carpeta de instalaci�n de Zamba
                    End Try
                End Try

                fileName = dir.FullName & GetExceptionFileName()
                file = New IO.FileInfo(fileName)
                dsEx.WriteXml(file.FullName, XmlWriteMode.IgnoreSchema)
                '                Dim ScreenFile As String = dir.FullName & GetExceptionScreenFileName()
                '               CaptureScreen(ScreenFile)

            Catch
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

    Private Shared Sub CaptureScreen(ByVal FilePath As String)

        Try
            capture_class.CaptureScreen(FilePath)



        Catch generatedExceptionName As AccessViolationException
        Catch generatedExceptionName As ApplicationException
        Catch generatedExceptionName As ArgumentOutOfRangeException
        Catch generatedExceptionName As IndexOutOfRangeException
        Catch generatedExceptionName As OverflowException
        Catch generatedExceptionName As Exception
        End Try
    End Sub

    Private Shared Function GetExceptionScreenFileName() As Object
        Return "\Excep " & Environment.MachineName & " " & Application.ExecutablePath.Substring(Application.ExecutablePath.LastIndexOf("\") + 1) & " " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".png"
    End Function

    Private Shared Function GetExceptionFileName() As String
        Return "\Excep " & Environment.MachineName & " " & ModuleName & " " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".txt"
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

End Class