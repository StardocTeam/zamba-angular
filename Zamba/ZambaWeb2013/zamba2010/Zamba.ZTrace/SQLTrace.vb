Imports System.Collections.Generic
Imports System.IO
Imports Zamba.Core
''' <summary>
''' Clase que se encarga de escribir los trace de ejecución de los SQL.
''' </summary>
''' <remarks>   Se creo particularmente para generar un trace por SQL 
'''             detallando la ejecución y el recorrido en el workflow del mismo
''' </remarks>
Public Class SQLTrace

    Private Shared hsSQLTraces As New Hashtable
    Private Shared fileName, rName, fecha As String
    Private Shared exceptions As String = Membership.MembershipHelper.AppTempPath & "\Exceptions\" & DateTime.Now.ToString("yyyy-MM-dd")

    Public Shared cacheTraceDirectories As New List(Of String)

    Public Sub New()

    End Sub

    Public Shared Function CheckDirectory(directoryPath As String)
        If Not SQLTrace.cacheTraceDirectories.Contains(directoryPath) Then
            If Directory.Exists(directoryPath) = False Then
                Directory.CreateDirectory(directoryPath)
            End If
            SQLTrace.cacheTraceDirectories.Add(directoryPath)
        End If
    End Function

    Public Sub Write(ByVal text As String)
        Try
            Dim key As String = ZTrace.GetKey()
            'Verifica si existe el trace en el hash
            If Not hsSQLTraces.ContainsKey("SQL" & key) Then

                Dim path As String = exceptions & "\Performance\Trace"

                SQLTrace.CheckDirectory(path)

                Dim ServiceName As String = String.Empty
                Try
                    ServiceName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath.Replace("/", "")
                Catch ex As Exception

                End Try
                fileName = path & "\Trace SQL " & key & " " & Environment.MachineName & " " & ServiceName & " " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".txt"

                'Genera el trace

                SyncLock hsSQLTraces
                    If Not hsSQLTraces.ContainsKey("SQL" & key) Then
                        Try
                            hsSQLTraces.Add("SQL" & key, New System.Diagnostics.TextWriterTraceListener(fileName))
                        Catch ex As Exception
                        End Try
                    End If
                End SyncLock
            End If

            'Obtiene el trace
            Dim trace As System.Diagnostics.TextWriterTraceListener = DirectCast(hsSQLTraces.Item("SQL" & key), System.Diagnostics.TextWriterTraceListener)

            'Escribe sobre el trace
            trace.WriteLine(text)
            'Libera recursos
            trace.Flush()
            trace.Close()
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
