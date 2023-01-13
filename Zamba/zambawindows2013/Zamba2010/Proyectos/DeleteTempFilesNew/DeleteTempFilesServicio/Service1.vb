Imports System.IO
Imports System.Threading
Imports Zamba.AppBlock

Public Class Service1

    Dim Interval As Int32 = 21600000
    Dim ValueDays As Int16
    Dim ValuePath As String
    Private ExceptionPath As String
    Dim txtTrace As TextWriterTraceListener


    Protected Overrides Sub OnStart(ByVal args() As String)
        Try
            If LoadInitalValues() = True Then
                StartTimer(Interval)
            End If
        Catch
        End Try
    End Sub

    Protected Overrides Sub OnStop()
    End Sub

    Public Sub StartTimer(ByVal interval As Int32)
        Dim tmrC As Timer = New Timer(New TimerCallback(AddressOf Execute))
        tmrC.Change(6000, interval)
    End Sub

    Public Sub Execute(ByVal o As Object)
        Try
            If LoadInitalValues() = True Then
                Trace.WriteLine("Buscando Archivos")
                DeleteTemp()
                DeleteExceptions()
            Else
                Trace.WriteLine("No se pudo validar los valores de inicio")
            End If
            CloseTrace()
            GC.Collect()
        Catch
        End Try
    End Sub

    Private Function InitializeTrace() As Boolean
        Try
            If Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\Logs") = False Then
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\Logs")
            End If
            txtTrace = New TextWriterTraceListener(System.Windows.Forms.Application.StartupPath + "\\Logs\\Trace DeleteTempFiles " + System.DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + ".txt")
            Trace.Listeners.Add(txtTrace)
            Trace.AutoFlush = True
            Return True
        Catch
            Return False
        End Try
    End Function
    Private Sub CloseTrace()
        If Not IsNothing(txtTrace) Then
            txtTrace.Close()
            Trace.Listeners.Remove(txtTrace)
            txtTrace.Dispose()
            txtTrace = Nothing
        End If
    End Sub

    Private Function LoadInitalValues() As Boolean
        Dim initialConfigPath As String = System.Windows.Forms.Application.StartupPath & "\DeleteTempFiles.ini"
        Dim iCstream As StreamReader = New StreamReader(initialConfigPath)
        Dim separators As String() = {"="}

        'Se valida que exista el .ini, caso contrario se devuelve false.
        If File.Exists(initialConfigPath) = False Then
            ZException.Log(New Exception("No se encuentra el archivo" & initialConfigPath), False)
            Return False
        End If

        Try
            Dim withTrace As Boolean = Boolean.Parse(iCstream.ReadLine().Split(separators, StringSplitOptions.None)(1))
            If withTrace = True Then
                InitializeTrace()
                Trace.WriteLine(DateTime.Now.ToString())
                Trace.WriteLine("---------------------------------------------------")
                Trace.WriteLine("       Asignacion de valores iniciales             ")
                Trace.WriteLine("")
            End If

            Dim iCsValueDays As String = iCstream.ReadLine().Split(separators, StringSplitOptions.None)(1)
            ValueDays = Int16.Parse(iCsValueDays)
            Trace.WriteLine("Primer parámetro asignado. [Días :: " + iCsValueDays + "]")
            Dim iCsValuePath As String = iCstream.ReadLine().Split(separators, StringSplitOptions.None)(1)
            If Directory.Exists(iCsValuePath) = False Then
                Trace.WriteLine("No se encuentra el directorio en: ")
                Trace.WriteLine(iCsValuePath)
                Return False
            End If

            Trace.WriteLine("Segundo parámetro asignado. [Ruta :: " + iCsValuePath + "]")
            ValuePath = iCsValuePath

            Dim iCsExceptionPath As String = iCstream.ReadLine().Split(separators, StringSplitOptions.None)(1)
            If Directory.Exists(iCsExceptionPath) = False Then
                Trace.WriteLine("No se encuentra el directorio en: ")
                Trace.WriteLine(iCsExceptionPath)
                Return False
            End If

            Trace.WriteLine("Tercer parámetro asignado. [Exception :: " + iCsExceptionPath + "]")
            ExceptionPath = iCsExceptionPath

        Catch ex As Exception
            Trace.WriteLine("Error asignando valores: " + ex.ToString())
            Return False
        Finally
            If Not IsNothing(iCstream) Then
                iCstream.Close()
            End If
        End Try

        Trace.WriteLine("")
        Trace.WriteLine("---------------------------------------------------")
        Trace.WriteLine("")
        Return True
    End Function
    Private Sub DeleteTemp()
        Trace.WriteLine(DateTime.Now.ToString())
        Trace.WriteLine("---------------------------------------------------")
        Trace.WriteLine("                Borrado de archivos                ")
        Trace.WriteLine("")

        Dim tempDirectory As DirectoryInfo = New DirectoryInfo(ValuePath)
        Trace.WriteLine("Buscando archivos en:")
        Trace.WriteLine(tempDirectory.FullName)
        Trace.WriteLine("con fecha menor o igual a: " + System.DateTime.Today.AddDays(ValueDays * -1))

        Dim tmpFList As FileInfo() = tempDirectory.GetFiles()
        Try
            For Each fi As FileInfo In tmpFList
                If fi.CreationTime <= System.DateTime.Today.AddDays(ValueDays * -1) Then
                    Trace.Write("[" + fi.CreationTime.ToString() + "] " + fi.Name + ": ")
                    Try
                        fi.Delete()
                        Trace.WriteLine("Eliminado con éxito.")
                    Catch
                        Try
                            fi.Attributes = FileAttributes.Normal
                            fi.Delete()
                            Trace.WriteLine("Eliminado con éxito.")
                        Catch
                            Try
                                File.Delete(fi.FullName)
                                Trace.WriteLine("Eliminado con éxito.")
                            Catch ex As Exception
                                Trace.WriteLine("Imosible eliminar. Error: " + ex.ToString())
                            End Try
                        End Try
                    End Try
                End If
            Next
        Catch
        Finally
            Try
                For i As Int16 = 0 To tmpFList.Length
                    i = i + 1
                    tmpFList(i) = Nothing
                Next
            Catch
            Finally
                tempDirectory = Nothing
                tmpFList = Nothing
                Trace.WriteLine("")
                Trace.WriteLine("---------------------------------------------------")
                Trace.WriteLine("")
            End Try
        End Try
    End Sub
    Private Sub DeleteExceptions()
        Trace.WriteLine(System.DateTime.Now.ToString())
        Trace.WriteLine("---------------------------------------------------")
        Trace.WriteLine("                Borrado de exceptions              ")
        Trace.WriteLine("")

        Dim ExceptionDirectory As DirectoryInfo = New DirectoryInfo(ExceptionPath)
        Trace.WriteLine("Buscando archivos en:")
        Trace.WriteLine(ExceptionPath)
        Trace.WriteLine("con fecha menor o igual a: " + DateTime.Today.AddDays(ValueDays * -1))

        Dim ExceptionFiles As FileInfo() = ExceptionDirectory.GetFiles()
        Try
            For Each ExceptionFile As FileInfo In ExceptionFiles
                If ExceptionFile.CreationTime <= DateTime.Today.AddDays(ValueDays * -1) Then
                    Trace.Write("[" + ExceptionFile.CreationTime.ToString() + "] " + ExceptionFile.Name + ": ")
                    Try
                        ExceptionFile.Delete()
                        Trace.WriteLine("Eliminado con éxito.")
                    Catch
                        Try
                            ExceptionFile.Attributes = FileAttributes.Normal
                            ExceptionFile.Delete()
                            Trace.WriteLine("Eliminado con éxito.")
                        Catch
                            Try
                                File.Delete(ExceptionFile.FullName)
                                Trace.WriteLine("Eliminado con éxito.")
                            Catch ex As Exception
                                Trace.WriteLine("Imosible eliminar. Error: " + ex.ToString())
                            End Try
                        End Try
                    End Try
                End If
            Next
        Catch
        Finally
            Try
                For i As Int16 = 0 To ExceptionFiles.Length
                    i = i + 1
                    ExceptionFiles(i) = Nothing
                Next
            Catch
            Finally
                ExceptionDirectory = Nothing
                ExceptionFiles = Nothing
                Trace.WriteLine("")
                Trace.WriteLine("---------------------------------------------------")
                Trace.WriteLine("")
            End Try
        End Try
    End Sub
End Class