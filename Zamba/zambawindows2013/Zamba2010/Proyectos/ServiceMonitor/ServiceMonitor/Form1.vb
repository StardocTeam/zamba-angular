Public Class Inicio

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            'todo iniciar el timer
            If IO.Directory.Exists(TextBox1.Text) Then
                Trace.WriteLine("Directorio inicio " & TextBox1.Text)
                If IO.Directory.Exists(TextBox2.Text) = False Then
                    IO.Directory.CreateDirectory(TextBox2.Text)
                End If
                Trace.WriteLine("Directorio temporal " & TextBox2.Text)
                'If IO.Directory.Exists(TextBox1.Text.Remove(0, TextBox1.Text.LastIndexOf("\") + 1) & "\temp") Then
                '    bolTemp = True
                'End If
                Trace.WriteLine("Iniciando")
                Me.Timer = New Threading.Timer(TCB, state, 1000, 1800000)
                Label3.Text = "Programa iniciado"
                Me.Button1.Enabled = False
            Else
                MsgBox("El directorio de inicio no existe")
            End If
        Catch ex As Exception
            Trace.WriteLine(ex.ToString())
        End Try
    End Sub

    'Dim bolTemp As Boolean
    Private WithEvents Timer As Threading.Timer
    Dim TCB As New Threading.TimerCallback(AddressOf tick)
    Dim state As Object

    Private Sub tick(ByVal STATE As Object)
        Try
            Trace.WriteLine("Tick")
            If Not IsNothing(TextBox1) Then
                If Not IsNothing(TextBox2) Then
                    If String.IsNullOrEmpty(TextBox1.Text) = False Then
                        If String.IsNullOrEmpty(TextBox2.Text) = False Then
                            Dim cant As Int64 = 0
                            cant = IO.Directory.GetFiles(TextBox1.Text).Length
                            Trace.WriteLine("Cantidad archivos" & cant)
                            If cant <= 0 Then
                                Exit Sub
                            End If
                            Trace.WriteLine("Esperando 60 segundos")
                            Threading.Thread.Sleep(60000)
                            Dim cant2 As Int64 = IO.Directory.GetFiles(TextBox1.Text).Length
                            Trace.WriteLine("Cantidad archivos" & cant2)
                            If cant <> cant2 Then
                                Exit Sub
                            End If

                            'Todo matar el servicio
                            Trace.WriteLine("Deteniendo servicio")
                            Dim Proc() As System.Diagnostics.Process
                            Proc = System.Diagnostics.Process.GetProcessesByName("ZMonitoreoServiceV2")
                            For Each mc As System.Diagnostics.Process In Proc
                                Trace.WriteLine("Servicio detenido")
                                mc.Kill()
                            Next

                            'Todo copiar archivos a carpeta temporal
                            Trace.WriteLine("Copiando archivos a la carpeta temporal")
                            For Each f As String In IO.Directory.GetFiles(TextBox1.Text)
                                Try
                                    IO.File.Move(f, TextBox2.Text & "\" & f.Remove(0, f.LastIndexOf("\") + 1))
                                Catch ex As Exception
                                    Trace.WriteLine("Archivo ya existente" & f)
                                    Trace.WriteLine(ex.ToString())
                                End Try
                            Next

                            'If bolTemp = True Then
                            '    For Each f As String In IO.Directory.GetFiles(TextBox1.Text.Remove(0, TextBox1.Text.LastIndexOf("\") + 1) & "\temp")
                            '        IO.File.Move(f, TextBox2.Text & "\" & f.Remove(0, f.LastIndexOf("\") + 1))
                            '    Next
                            'End If

                            'todo esperar a q baje el servicio
                            Trace.WriteLine("Esperando 30 segundos")
                            Threading.Thread.Sleep(30000)

                            'todo iniciar el servicio
                            Trace.WriteLine("Iniciando servicio")
                            Shell("net start " & Chr(34) & "Zamba Servicio de Monitoreo V2" & Chr(34))

                            'todo esperar a q baje el servicio
                            Trace.WriteLine("Esperando 60 segundos")
                            Threading.Thread.Sleep(60000)

                            'todo copiar archivos al directorio
                            Trace.WriteLine("Copiando archivos para procesar")
                            For Each f As String In IO.Directory.GetFiles(TextBox2.Text)
                                Try
                                    IO.File.Move(f, TextBox1.Text & "\" & f.Remove(0, f.LastIndexOf("\") + 1))
                                Catch ex As Exception
                                    Trace.WriteLine("Archivo ya existente" & f)
                                    Trace.WriteLine(ex.ToString())
                                End Try
                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Threading.SynchronizationLockException
            Trace.WriteLine(ex.ToString())
        Catch ex As Threading.ThreadStateException
            Trace.WriteLine(ex.ToString())
        Catch ex As Threading.ThreadInterruptedException
            Trace.WriteLine(ex.ToString())
        Catch ex As Threading.ThreadAbortException
            Trace.WriteLine(ex.ToString())
        Catch ex As Exception
            Trace.WriteLine(ex.ToString())
        End Try
    End Sub

    Private Sub Inicio_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Trace.Listeners.Add(New TextWriterTraceListener(System.Windows.Forms.Application.StartupPath & "\Trace ServiceMonitor " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".txt"))
        Trace.AutoFlush = True
    End Sub
End Class
