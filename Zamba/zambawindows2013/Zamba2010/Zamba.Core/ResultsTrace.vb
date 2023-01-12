''' <summary>
''' Clase que se encarga de escribir los trace de ejecución de los results.
''' </summary>
''' <remarks>   Se creo particularmente para generar un trace por result 
'''             detallando la ejecución y el recorrido en el workflow del mismo
''' </remarks>
Public Class ResultsTrace

    Private Shared hsResultTraces As New Hashtable
    Private Shared fileName, rName, fecha As String
    Private Shared exceptions As String = ZTrace.GetTempDir("\Exceptions").FullName

    ''' <summary>
    ''' Crea un archivo de trace por cada result recibido y si estaba creado escribe sobre el mismo.
    ''' </summary>
    ''' <param name="results">Colección de results para escribir en su log</param>
    ''' <param name="text">Texto a escribir en cada log del result</param>
    ''' <remarks></remarks>
    Public Shared Sub Write(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal text As String)
        Try

            'Recorre los diferentes results
            For Each r As ITaskResult In results
                If r IsNot Nothing AndAlso Not String.IsNullOrEmpty(exceptions) Then
                    'Verifica si existe el trace en el hash
                    If Not hsResultTraces.ContainsKey(r.ID) Then
                        'Por alguna razón es posible que el name de la tarea no se encuentre completo. Como todavía 
                        'no se determinó en que casos sucede se agrega la validación para evitar errores.
                        If r.Name Is Nothing Then
                            rName = String.Empty
                        Else
                            'Se remueven los caracteres inválidos del nombre del archivo
                            rName = r.Name
                            For Each invalidChar As Char In IO.Path.GetInvalidFileNameChars()
                                rName = rName.Replace(invalidChar, String.Empty)
                            Next
                        End If

                        'Fecha formateada
                        fecha = DateTime.Now.ToShortDateString.ToString.Replace("/", "-").Trim & " " & _
                                    DateTime.Now.ToShortTimeString.ToString.Replace(":", "-").Replace("a.m.", String.Empty).Replace("p.m.", String.Empty).Trim
                        'Nombre del archivo
                        fileName = exceptions & "\Tarea " & r.ID.ToString & " - " & rName & " - " & fecha & ".txt"

                        'Genera el trace
                        hsResultTraces.Add(r.ID, New System.Diagnostics.TextWriterTraceListener(fileName))
                    End If

                    'Obtiene el trace
                    Dim trace As System.Diagnostics.TextWriterTraceListener = DirectCast(hsResultTraces.Item(r.ID), System.Diagnostics.TextWriterTraceListener)

                    'Escribe sobre el trace
                    trace.WriteLine(text)

                    'Libera recursos
                    trace.Flush()
                End If
            Next
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
        For Each t As System.Diagnostics.TextWriterTraceListener In hsResultTraces.Values

            'Cierra y libera los recursos del trace
            t.Close()
            t.Dispose()

        Next

        hsResultTraces.Clear()

    End Sub

    Public Shared Sub CloseTrace(ByVal results As Generic.List(Of ITaskResult))
        For Each t As ITaskResult In results
            If Not IsNothing(t) AndAlso hsResultTraces.ContainsKey(t.ID) Then
                hsResultTraces.Item(t.ID).Close()
            End If
        Next
    End Sub

End Class
