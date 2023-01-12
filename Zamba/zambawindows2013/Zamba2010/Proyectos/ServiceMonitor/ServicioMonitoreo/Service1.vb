Imports System.IO
Imports System.Threading
Public Class Service1


    ''' <summary>
    ''' Path del directorio temporal levantado del .ini
    ''' </summary>
    ''' <remarks></remarks>
    Private _dirTemp As String

    ''' <summary>
    ''' Path del .ini
    ''' </summary>
    ''' <remarks></remarks>
    Private _dirIni As String
    ''' <summary>
    ''' Hora que se da de baja el servicio monitoreado. Levantado del .ini
    ''' </summary>
    ''' <remarks></remarks>
    Private _stopHour As Int32

    ''' <summary>
    ''' Hora que se da de alta el servicio monitoreado. Levantado del .ini
    ''' </summary>
    ''' <remarks></remarks>
    Private _startHour As Int32
    ''' <summary>
    ''' Cantidad de segundos entre pasada y pasada del servicio. Levantado del .ini
    ''' </summary>
    ''' <remarks></remarks>
    Private _delaySeconds As Int32
    Private WithEvents Timer As Threading.Timer
    Private TCB As New Threading.TimerCallback(AddressOf tick)
    Private state As Object

    Protected Overrides Sub OnStart(ByVal args() As String)
        Try
            Dim initialConfigPath As String = System.Windows.Forms.Application.StartupPath & "\ServiceMonitorConfiguration.ini"
            Dim mander As Char = "="

            If File.Exists(initialConfigPath) Then
                Dim iCstream As StreamReader = New StreamReader(initialConfigPath)

                Try
                    Dim withTrace As Boolean = Boolean.Parse(iCstream.ReadLine().Split(mander)(1))
                    If withTrace = True Then
                        Trace.Listeners.Add(New TextWriterTraceListener(System.Windows.Forms.Application.StartupPath & "\Trace ServiceMonitor " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".log"))
                        Trace.AutoFlush = True
                        Trace.WriteLine(Now())
                        Trace.WriteLine("Configuracion: " & initialConfigPath)
                    End If
                Catch ex As Exception
                    Zamba.AppBlock.ZException.Log(ex, False)
                End Try

                Try
                    _dirIni = iCstream.ReadLine().Split(mander)(1)
                    If IO.Directory.Exists(_dirIni) Then
                        Trace.WriteLine("Directorio inicio: " & _dirIni)
                        _dirTemp = iCstream.ReadLine().Split(mander)(1)
                        If IO.Directory.Exists(_dirTemp) = False Then
                            IO.Directory.CreateDirectory(_dirTemp)
                        End If
                        Trace.WriteLine("Directorio temporal " & _dirTemp)
                        
                    Else
                        Trace.WriteLine("No existe el directorio de inicio " & _dirIni)
                    End If
                Catch ex As Exception
                    Trace.WriteLine(ex.ToString())
                End Try

                Try
                    _stopHour = Int32.Parse(iCstream.ReadLine().Split(mander)(1))
                    Trace.WriteLine("Hora a detener servicio: " & _stopHour)

                    _startHour = Int32.Parse(iCstream.ReadLine().Split(mander)(1))
                    Trace.WriteLine("Hora a iniciar servicio: " & _startHour)

                    _delaySeconds = Int32.Parse(iCstream.ReadLine().Split(mander)(1))
                    Trace.WriteLine("El servicio esta configurado para monitorear cada " & _delaySeconds & " segundos")

                    Trace.WriteLine("Iniciando")
                    Me.Timer = New Threading.Timer(TCB, state, 1000, _delaySeconds * 1000)

                Catch ex As Exception
                    Trace.WriteLine(ex.ToString())
                End Try

            Else
                Throw New Exception("No se encuentra el archivo " & initialConfigPath)
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex, False)
        End Try
    End Sub

    Private Sub tick(ByVal STATE As Object)
        Try
            Trace.WriteLine("Tick - " + DateTime.Now.ToString)

            If String.IsNullOrEmpty(_dirTemp) = False Then
                If String.IsNullOrEmpty(_dirIni) = False Then

                    Dim CurrentTime As DateTime = DateTime.Now
                    Dim HoursSinceMidnight As Int32 = Math.Truncate(CurrentTime.Subtract(DateTime.Today).TotalHours).ToString()

                    Trace.WriteLine(String.Concat("Hora desde la medianoche = ", HoursSinceMidnight))

                    If CurrentTime.Hour = _startHour Then
                        Trace.WriteLine(String.Concat("Iniciando el servicio - ", HoursSinceMidnight))
                        StartService()
                        Exit Sub
                    ElseIf CurrentTime.Hour = _stopHour Then
                        Trace.WriteLine(String.Concat("Deteniendo el servicio - ", HoursSinceMidnight))
                        StopService()
                        Exit Sub
                    End If

                    Dim cant As Int64 = 0
                    cant = IO.Directory.GetFiles(_dirIni).Length
                    Trace.WriteLine("Cantidad archivos: " & cant)
                    Trace.WriteLine(Now())
                    If cant <= 0 Then
                        Exit Sub
                    End If
                    Trace.WriteLine("Esperando 60 segundos")
                    Threading.Thread.Sleep(60000)

                    Dim cant2 As Int64 = IO.Directory.GetFiles(_dirIni).Length
                    Trace.WriteLine("Cantidad archivos: " & cant2)
                    Trace.WriteLine(Now())
                    If cant <> cant2 Then
                        Exit Sub
                    End If

                    StopService()

                    'Todo copiar archivos a carpeta temporal
                    Trace.WriteLine("Copiando archivos a la carpeta temporal")
                    Trace.WriteLine(Now())
                    For Each f As String In IO.Directory.GetFiles(_dirIni)
                        Try
                            IO.File.Move(f, _dirTemp & "\" & f.Remove(0, f.LastIndexOf("\") + 1))
                        Catch ex As Exception
                            Trace.WriteLine("Archivo ya existente" & f)
                            Trace.WriteLine(ex.ToString())
                        End Try
                    Next

                    'todo esperar a q baje el servicio
                    Trace.WriteLine("Esperando 30 segundos")
                    Threading.Thread.Sleep(30000)

                    StartService()

                    'todo esperar a q baje el servicio
                    Trace.WriteLine("Esperando 60 segundos")
                    Threading.Thread.Sleep(60000)

                    'todo copiar archivos al directorio
                    Trace.WriteLine("Copiando archivos para procesar - " + DateTime.Now.ToString())

                    For Each f As String In IO.Directory.GetFiles(_dirTemp)
                        Try
                            IO.File.Move(f, _dirIni & "\" & f.Remove(0, f.LastIndexOf("\") + 1))
                        Catch ex As Exception
                            Trace.WriteLine("Archivo ya existente" & f)
                            Trace.WriteLine(ex.ToString())
                        End Try
                    Next
                Else
                    Trace.WriteLine("Directorio inexistente")
                End If
            Else
                Trace.WriteLine("Directorio temporal inexistente")
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

    Protected Overrides Sub OnStop()
    End Sub

    Private Sub StopService()
        Try
            Dim Proc() As System.Diagnostics.Process
            Proc = System.Diagnostics.Process.GetProcessesByName("ZMonitoreoServiceV2")
            For Each mc As System.Diagnostics.Process In Proc
                Trace.WriteLine("Servicio detenido")
                Trace.WriteLine(Now())
                mc.Kill()
            Next
        Catch ex As Exception
            Trace.WriteLine(ex.ToString())
        End Try
    End Sub

    Private Sub StartService()
        Try
            Shell("net start " & Chr(34) & "Zamba Servicio de Monitoreo V2" & Chr(34))
        Catch ex As Exception
            Trace.WriteLine(ex.ToString())
        End Try
    End Sub
End Class
