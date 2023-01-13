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

    ''' <summary>
    ''' Crea un archivo de trace por cada SQL recibido y si estaba creado escribe sobre el mismo.
    ''' </summary>
    ''' <param name="SQL">Colección de SQL para escribir en su log</param>
    ''' <param name="text">Texto a escribir en cada log del SQL</param>
    ''' <remarks></remarks>
    Public Shared Sub Write(ByVal text As String)
        Try

            If (Zamba.Core.ZTrace.Level = TraceLevel.Verbose) Then
                'Verifica si existe el trace en el hash
                If Not hsSQLTraces.ContainsKey("SQL") Then

                    'Fecha formateada
                    fecha = DateTime.Now.ToShortDateString.ToString.Replace("/", "-").Trim & " " & _
                                DateTime.Now.ToShortTimeString.ToString.Replace(":", "-").Replace("a.m.", String.Empty).Replace("p.m.", String.Empty).Trim
                    'Nombre del archivo
                    fileName = exceptions & "\SQL " & fecha & ".txt"

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
            ZClass.raiseerror(ex)
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

    'Public Shared Sub CloseTrace(ByVal SQL As Generic.List(Of ITaskSQL))
    '    For Each t As ITaskSQL In SQL
    '        If Not IsNothing(t) AndAlso hsSQLTraces.ContainsKey(t.ID) Then
    '            hsSQLTraces.Item(t.ID).Close()
    '        End If
    '    Next
    'End Sub

End Class
