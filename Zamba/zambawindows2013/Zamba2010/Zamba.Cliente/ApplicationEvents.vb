Imports ZAMBA.Core
Imports System.Text

Namespace My
    ' The follow events are available for MyApplication:
    '
    ' Startup: Raised when the application starts, before
    ' the startup form is created.
    ' Shutdown: Raised after all application forms are closed.
    ' This event is not raised if the application terminals
    ' abnormally.
    ' UnhandledException: Raised if the application encounters
    ' an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-
    ' instance application and the application is already
    ' active.
    ' NetworkAvailabilityChanged: Raised when the network
    ' connection is connected or disconnected.

    Partial Friend Class MyApplication
        'If the aplication was already started enter here
        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            Try
                If e.CommandLine IsNot Nothing Then
                    Dim sbLineCommands As New StringBuilder
                    For Each line As String In e.CommandLine
                        sbLineCommands.Append(line)
                    Next
                    Dim commandLines As String = sbLineCommands.ToString
                    sbLineCommands = Nothing
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Linea de Argumentos: " & commandLines)
                    Zamba.Client.MainForm.line = commandLines

                    'If there are parameters give them to the client
                    If e.CommandLine.Count > 0 Then
                        Zamba.Client.MainForm.FlagInitialize = True
                    Else
                        Zamba.Client.MainForm.FlagInitialize = False
                    End If
                End If

                If Application.MainForm() IsNot Nothing AndAlso Application.MainForm().Visible = False Then
                    Application.MainForm().Show()
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        'the first time the client is started
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            If e.CommandLine IsNot Nothing AndAlso e.CommandLine.Count > 0 Then
                Try
                    Dim sbLineCommands As New StringBuilder
                    For Each line As String In e.CommandLine
                        sbLineCommands.Append(line)
                    Next
                    Dim commandLines As String = sbLineCommands.ToString
                    sbLineCommands = Nothing
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Linea de Argumentos: " & commandLines)
                    If Zamba.Client.MainForm.line Is Nothing Then Zamba.Client.MainForm.line = String.Empty
                    Zamba.Client.MainForm.line &= commandLines

                    'If there are parameters give them to the client
                    If e.CommandLine.Count > 0 Then
                        Zamba.Client.MainForm.FlagInitialize = True
                    Else
                        Zamba.Client.MainForm.FlagInitialize = False
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If
        End Sub
    End Class
End Namespace
