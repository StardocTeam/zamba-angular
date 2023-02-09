''' <summary>
''' Clase que se encarga de escribir los trace de ejecución de los SQL.
''' </summary>
''' <remarks>   Se creo particularmente para generar un trace por SQL 
'''             detallando la ejecución y el recorrido en el workflow del mismo
''' </remarks>
Public Class SQLTrace

    Private Shared hsSQLTraces As New Hashtable
    Private Shared fileName, rName, fecha As String
    Private Shared exceptions As String = ZTrace.GetTempDir("\Exceptions").FullName

  
    Public Shared Sub Write(ByVal text As String)
        Try

            If (Zamba.Core.ZTrace.Level = TraceLevel.Verbose) Then
                'Verifica si existe el trace en el hash
                If Not hsSQLTraces.ContainsKey("SQL") Then

                    Dim path As String = exceptions & "\Performance"

                    If (Not IO.Directory.Exists(path)) Then
                        IO.Directory.CreateDirectory(path)
                    End If
                    Dim ServiceName As String = String.Empty
                    Try
                        ServiceName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath.Replace("/", "")
                    Catch ex As Exception

                    End Try
                    fileName = path & "\SQL " & Environment.MachineName & " " & ServiceName & " " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".txt"

                    'Genera el trace
                    hsSQLTraces.Add("SQL", New System.Diagnostics.TextWriterTraceListener(fileName))
                End If

                'Obtiene el trace
                Dim trace As System.Diagnostics.TextWriterTraceListener = DirectCast(hsSQLTraces.Item("SQL"), System.Diagnostics.TextWriterTraceListener)

                'Escribe sobre el trace
                trace.WriteLine(text)
                'Libera recursos
                trace.Flush()
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Cierra todos los Trace abiertos y libera todos los recursos.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub CloseTraces()

        'Recorre todos los trace
        For Each t As System.Diagnostics.TextWriterTraceListener In hsSQLTraces.Values
            'Cierra y libera los recursos del trace
            t.Close()
            t.Dispose()
        Next
        hsSQLTraces.Clear()

    End Sub

End Class
