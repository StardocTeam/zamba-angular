Public Class Process_Business
    Public Shared Sub asignprocesslist(ByVal processID As Int32, ByVal ProcessListId As Int32)
        ProcessFactory.asignprocesslist(processID, ProcessListId)
    End Sub

    Public Shared Sub Removeprocesslist(ByVal ProcessId As Object)
        ProcessFactory.Removeprocesslist(ProcessId)
    End Sub
#Region "PostProcess"
    Public Shared Function PostProcess(ByVal Files As ArrayList, ByVal ProcessId As Int32) As ArrayList
        Dim PostProcessEjecutados As New ArrayList
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ruta XML: " & System.Windows.Forms.Application.StartupPath & "\postprocess.xml")
            If IO.File.Exists(System.Windows.Forms.Application.StartupPath & "\postprocess.xml") Then
                Dim ds As New DataSet
                Try
                    ds.ReadXml(System.Windows.Forms.Application.StartupPath & "\postprocess.xml")
                Catch ex As Exception
                    ds = New DataSet
                End Try
                Dim i As Int32
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    Try
                        If CInt(ds.Tables(0).Rows(i).Item(1)) = ProcessId Then
                            For Each s As String In Files
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "PostProceso: " & ds.Tables(0).Rows(i).Item(0) & " " & s)
                                Shell(ds.Tables(0).Rows(i).Item(0) & " " & s, AppWinStyle.NormalFocus, False)
                                PostProcessEjecutados.Add(ds.Tables(0).Rows(i).Item(0))
                            Next
                        End If
                    Catch ex As Exception
                        ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString())
                        ZClass.raiseerror(New Exception("Fallo el postproceso " & ds.Tables(0).Rows(i).Item(0) & " " & ex.ToString))
                    End Try
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return PostProcessEjecutados
        Finally
        End Try
        Return PostProcessEjecutados
    End Function
#End Region
End Class
