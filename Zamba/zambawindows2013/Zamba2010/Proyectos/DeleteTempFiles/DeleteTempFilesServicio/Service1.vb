Imports System.IO
Imports System.Threading
Imports Zamba.AppBlock

Public Class Service1

    Private _dirTemp As String
    Private _dirExceptions As String
    Private _delayHours As Int32
    Private WithEvents Timer As Timer
    Dim TCB As New TimerCallback(AddressOf Execute)
    'Dim TCB As New TimerCallback(AddressOf tick)
    Dim state As Object

    Protected Overrides Sub OnStart(ByVal args() As String)
        Try
            Dim initialConfigPath As String = System.Windows.Forms.Application.StartupPath & "\DeleteTempFiles.ini"
            If File.Exists(initialConfigPath) Then
                Dim mander As Char = "="
                Dim iCstream As StreamReader = New StreamReader(initialConfigPath)

                Try
                    Dim withTrace As Boolean = Boolean.Parse(iCstream.ReadLine().Split(mander)(1))
                    If withTrace = True Then
                        Trace.Listeners.Add(New TextWriterTraceListener(System.Windows.Forms.Application.StartupPath & "\Trace ServiceMonitor " & Now.ToString("dd-MM-yyyy HH-mm-ss") & ".log"))
                        Trace.AutoFlush = True
                        Trace.WriteLine(Now())
                        Trace.WriteLine("Configuracion " & initialConfigPath)
                    End If
                Catch ex As Exception
                    ZException.Log(ex, False)
                End Try

                'cantidad de dias
                Try
                    _delayHours = Int32.Parse(iCstream.ReadLine().Split(mander)(1))
                    Trace.WriteLine("Cantidad de horas " & _delayHours.ToString())
                Catch ex As Exception
                    ZException.Log(ex, False)
                End Try


                'Directorio temporal
                Try
                    _dirTemp = iCstream.ReadLine().Split(mander)(1)
                    If IO.Directory.Exists(_dirTemp) Then
                        Trace.WriteLine("Directorio temporales: " & _dirTemp)
                        _dirExceptions = iCstream.ReadLine().Split(mander)(1)
                        If Directory.Exists(_dirExceptions) = False Then
                            Directory.CreateDirectory(_dirExceptions)
                        End If

                        Trace.WriteLine("Directorio exceptions: " & _dirExceptions)
                        Trace.WriteLine("Iniciando")
                        Me.Timer = New Timer(TCB, state, 1000, 1800000) 'cantidad de milisegundos por dia
                    Else
                        Trace.WriteLine("No existe el directorio de temporales: " & _dirTemp)
                    End If
                Catch ex As Exception
                    Trace.WriteLine(ex.ToString())
                End Try
            Else
                Throw New Exception("No se encuentra el archivo " & initialConfigPath)
            End If
        Catch ex As Exception
            ZException.Log(ex, False)
        End Try
    End Sub

    Protected Overrides Sub OnStop()
    End Sub

    Public Sub StartTimer(ByVal interval As Int32)
        Dim tmrC As Timer = New Timer(New TimerCallback(AddressOf Execute))
        tmrC.Change(60000, interval)
    End Sub

    Public Sub Execute(ByVal o As Object)
        Try
            Trace.WriteLine("Tick")
            Trace.WriteLine(Now())
            If String.IsNullOrEmpty(_dirTemp) = False Then
                If String.IsNullOrEmpty(_dirExceptions) = False Then

                    'Dim cant As Int64 = Directory.GetFiles(_dirIni).Length

                    'Trace.WriteLine("Cantidad archivos" & cant.ToString())
                    'Trace.WriteLine(Now())

                    'If cant <= 0 Then
                    '    Exit Sub
                    'End If

                    'Trace.WriteLine("Esperando 60 segundos")
                    'Thread.Sleep(60000)

                    'Dim cant2 As Int64 = Directory.GetFiles(_dirIni).Length

                    'Trace.WriteLine("Cantidad archivos" & cant2.ToString())
                    'Trace.WriteLine(Now())


                    'If cant <> cant2 Then
                    '    Exit Sub
                    'End If

                    ''Todo matar el servicio
                    'Trace.WriteLine("Deteniendo servicio")
                    'Trace.WriteLine(Now())
                    'Dim Proc() As System.Diagnostics.Process
                    'Proc = System.Diagnostics.Process.GetProcessesByName("ZMonitoreoServiceV2")
                    'For Each mc As System.Diagnostics.Process In Proc
                    '    Trace.WriteLine("Servicio detenido")
                    '    Trace.WriteLine(Now())
                    '    mc.Kill()
                    'Next

                    ''Todo copiar archivos a carpeta temporal
                    'Trace.WriteLine("Copiando archivos a la carpeta temporal")
                    'Trace.WriteLine(Now())
                    'For Each f As String In Directory.GetFiles(_dirIni)
                    '    Try
                    '        File.Move(f, _dirTemp & "\" & f.Remove(0, f.LastIndexOf("\") + 1))
                    '    Catch ex As Exception
                    '        Trace.WriteLine("Archivo ya existente" & f)
                    '        Trace.WriteLine(ex.ToString())
                    '    End Try
                    'Next

                    ''todo iniciar el servicio
                    'Trace.WriteLine("Iniciando servicio")
                    'Trace.WriteLine(Now())
                    'Shell("net start " & Chr(34) & "Zamba Servicio de Monitoreo V2" & Chr(34))


                    Dim tempDirectory As DirectoryInfo = New DirectoryInfo(_dirTemp)
                    Trace.WriteLine("Buscando archivos en:")
                    Trace.WriteLine(tempDirectory.FullName)

                    Dim FilterDate As DateTime = DateTime.Now.AddHours(_delayHours * -1)

                    Trace.WriteLine("con fecha menor o igual a: " + FilterDate)

                    ''todo copiar archivos al directorio
                    'Trace.WriteLine("Copiando archivos para procesar")
                    'Trace.WriteLine(Now())



                    For Each Fi As FileInfo In New DirectoryInfo(_dirTemp).GetFiles
                        Try

                            Trace.WriteLine("FilterDate - Fi.CreationTime = " + FilterDate.Subtract(Fi.CreationTime).Milliseconds + " milisegundos")
                            If (FilterDate.Subtract(Fi.CreationTime).Milliseconds > 0) Then
                                Fi.Attributes = FileAttributes.Normal
                                Fi.Delete()
                                Trace.WriteLine("Eliminado " + Fi.FullName + " con éxito.")
                            End If
                        Catch
                            Try
                                File.Delete(Fi.FullName)
                                Trace.WriteLine("Eliminado " + Fi.FullName + " con éxito.")
                            Catch ex As Exception
                                Trace.WriteLine("Imposible eliminar. Error: " + ex.ToString())
                            End Try
                        End Try
                    Next

                    For Each Fi As FileInfo In New DirectoryInfo(_dirExceptions).GetFiles
                        Try

                            Trace.WriteLine("FilterDate - Fi.CreationTime = " + FilterDate.Subtract(Fi.CreationTime).Milliseconds + " milisegundos")
                            If (FilterDate.Subtract(Fi.CreationTime).Milliseconds > 0) Then
                                Fi.Attributes = FileAttributes.Normal
                                Fi.Delete()
                                Trace.WriteLine("Eliminado " + Fi.FullName + " con éxito.")
                            End If
                        Catch
                            Try
                                File.Delete(Fi.FullName)
                                Trace.WriteLine("Eliminado " + Fi.FullName + " con éxito.")
                            Catch ex As Exception
                                Trace.WriteLine("Imposible eliminar. Error: " + ex.ToString())
                            End Try
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
End Class
