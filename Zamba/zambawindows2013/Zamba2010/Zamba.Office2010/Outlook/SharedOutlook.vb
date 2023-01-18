Imports Zamba.OfficeCommon
Imports Microsoft.Office.Interop.Outlook
Imports Microsoft.Win32
Imports System.Runtime.InteropServices
Imports Zamba.Core
Imports System.Reflection

Namespace Outlook
    ''' <summary>
    ''' Clase utilizada para instanciar los mails de Outlook desde Zamba.
    ''' Con esta clase se evita estar instanciando continuamente outlook
    ''' por cada MSG a visualizar.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SharedOutlook

#Region "Atributos & Propiedades"

        Private Shared _app As Application = Nothing
        Private Shared _outlookNs As [NameSpace] = Nothing
        Private Shared _mapiFolder As MAPIFolder = Nothing
        Private Shared WithEvents _exp As Explorer
        Private Const _PREKEY As String = "software\microsoft\office"
        Private Const _POSKEY As String = "\Outlook\InstallRoot"
        Private Const _EXEC As String = "outlook.exe"
        Private Const _VALUE As String = "Path"

        ''' <summary>
        ''' Id del proceso de outlook. Se utiliza para verificar que el proceso abierto 
        ''' de outlook sea el mismo que utiliza la instancia. Lo que pasaba era que si 
        ''' se tenía Zamba y outlook abierto, se cerraba el outlook y se volvía a abrir
        ''' el msg nunca era mostrado a causa de que la instancia app 'pertenecia' al 
        ''' proceso anteriormente cerrado.
        ''' </summary>
        Private Shared _procId As Int32 = 0

        ''' <summary>
        ''' Array de procesos temporal para mejorar la performance al solicitar el proceso de outlook.
        ''' </summary>
        Private Shared _tempProcesses As System.Diagnostics.Process()
#End Region

#Region "Métodos"

        ''' <summary>
        ''' Devuelve una instancia de la clase Zamba.Office.Outlook.OutlookInterop
        '''  utilizando una única instancia de Outlook. 
        ''' </summary>
        ''' <returns>Un objeto de la clase OutlookInterop utilizando una única instancia de OUTLOOK.EXE</returns>
        Public Shared Function GetOutlook() As OutlookInterop
            'Dim OutlookMailParameters As Object = Nothing
            _app = GetOutlookApp()
            _outlookNs = _app.GetNamespace(OutlookMailParameters.MAPI_NAMESPACE)
            _mapiFolder = _outlookNs.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderSentMail)
            Return New OutlookInterop(_app, _outlookNs, _mapiFolder)
        End Function

        ''' <summary>
        ''' Obtiene el objeto Outlook.Application de la instancia de Outlook.
        ''' En caso de que el Outlook este cerrado, lo abre y obtiene la instancia.
        ''' </summary>
        ''' <returns>Outlook.Application</returns>
        Public Shared Function GetOutlookApp() As Application
            Dim TryOption As Int16 = 1
            Try
TryNext:
                Select Case TryOption
                    Case 1
                        Dim abierto As Boolean = False
                        Dim contador As Integer = 1

                        If Not IsOutlookRunning() Then
                            ZTrace.WriteLineIf(ZTrace.IsError, "# Proceso no encontrado. Iniciando la búsqueda de outlook.")
                            Dim path As String = GetOutlookPath()
                            ZTrace.WriteLineIf(ZTrace.IsError, "# Se iniciará el proceso " & path)
                            Dim OP As System.Diagnostics.Process = System.Diagnostics.Process.Start(path)
                            Threading.Thread.Sleep(4000)
                        End If

                        Do
                            Try

                                ZTrace.WriteLineIf(ZTrace.IsError, "# Buscando el proceso abierto de outlook.")
                                If IsOutlookRunning() Then
                                    ZTrace.WriteLineIf(ZTrace.IsError, "# Proceso encontrado. Intentando obtener Outlook.Application")
                                    _app = DirectCast(Marshal.GetActiveObject("Outlook.Application"), Application)
                                    ZTrace.WriteLineIf(ZTrace.IsError, "# Outlook.Application obtenido.")
                                    abierto = True
                                End If
                                If Not System.Windows.Forms.Application.MessageLoop Then
                                    System.Windows.Forms.Application.DoEvents()
                                End If
                                contador = contador + 1
                                If abierto = False Then Threading.Thread.Sleep(2000)
                            Catch ex As System.Exception
                                contador = contador + 1
                                Threading.Thread.Sleep(2000)
                            End Try
                        Loop Until (abierto = True OrElse contador > 30)
                    Case 2
                        Dim abierto As Boolean = False
                        Dim contador As Integer = 1
                        If Not IsOutlookRunning() Then
                            ZTrace.WriteLineIf(ZTrace.IsError, "# Proceso no encontrado. Iniciando la búsqueda de outlook.")
                            Dim path As String = GetOutlookPath()
                            ZTrace.WriteLineIf(ZTrace.IsError, "# Se iniciará el proceso " & path)
                            Dim OP As System.Diagnostics.Process = System.Diagnostics.Process.Start(path)
                            Threading.Thread.Sleep(2000)
                        End If

                        Do
                            Try

                                ZTrace.WriteLineIf(ZTrace.IsError, "# Buscando el proceso abierto de outlook.")
                                If IsOutlookRunning() Then
                                    ZTrace.WriteLineIf(ZTrace.IsError, "# Proceso encontrado. Intentando obtener Outlook")
                                    _app = DirectCast(Marshal.GetActiveObject("Outlook"), Application)
                                    ZTrace.WriteLineIf(ZTrace.IsError, "# Outlook obtenido.")
                                    abierto = True
                                End If
                                If Not System.Windows.Forms.Application.MessageLoop Then
                                    System.Windows.Forms.Application.DoEvents()
                                End If
                                contador = contador + 1
                                If abierto = False Then Threading.Thread.Sleep(1000)
                            Catch ex As System.Exception
                                contador = contador + 1
                                Threading.Thread.Sleep(1000)
                            End Try
                        Loop Until (abierto = True OrElse contador > 10)

                    Case 3

                        ZTrace.WriteLineIf(ZTrace.IsError, "# Intentando obtener Outlook.Application, intento Nº " + TryOption.ToString)

                        _app = DirectCast(Activator.CreateInstance(Type.GetTypeFromProgID("Outlook.Application")), Application)

                        ZTrace.WriteLineIf(ZTrace.IsError, "# Outlook.Application obtenido.")
                    Case 4
                        ZTrace.WriteLineIf(ZTrace.IsError, "# Intentando obtener Outlook.Application, intento Nº " + TryOption.ToString)

                        _app = CType(GetObject(, "Outlook.Application"), Application)

                        ZTrace.WriteLineIf(ZTrace.IsError, "# Outlook.Application obtenido.")
                    Case 5
                        _app = New Microsoft.Office.Interop.Outlook.Application()
                        Dim nameSpace1 As Microsoft.Office.Interop.Outlook.NameSpace = _app.GetNamespace("MAPI")
                        nameSpace1.Logon("", "", Missing.Value, Missing.Value)
                        nameSpace1 = Nothing
                    Case Else
                        ZTrace.WriteLineIf(ZTrace.IsError, "# Outlook NO obtenido.")
                        Return Nothing
                End Select
                If _app Is Nothing Then
                    TryOption = TryOption + 1
                    GoTo TryNext
                End If
            Catch ex As System.Exception
                If TryOption < 5 Then
                    ZTrace.WriteLineIf(ZTrace.IsError, String.Format("Error: Outlook NO obtenido, opcion {0}.", TryOption))
                    TryOption = TryOption + 1
                    GoTo TryNext
                End If
                Zamba.Core.ZClass.raiseerror(ex)
                Return Nothing
            End Try
            Return _app
        End Function

        Public Shared Function IsOutlookRunning() As Boolean
            For Each p As Diagnostics.Process In Diagnostics.Process.GetProcesses()
                If p.ProcessName.ToUpper.Contains("OUTLOOK") Then
                    Return True
                End If
            Next
            Return False
        End Function

        ''' <summary>
        ''' Obtiene la ruta de Outlook buscando en el registro.
        ''' </summary>
        ''' <returns>Devuelve la ruta de outlook.</returns>
        Public Shared Function GetOutlookPath() As String
            Dim rk As RegistryKey
            Dim reg As RegistryKey = Nothing
            Dim names() As String
            Dim s As String
            Dim path As String

            Try
                Try
                    rk = Registry.LocalMachine.OpenSubKey(_PREKEY)
                Catch ex As System.Exception
                    ZTrace.WriteLineIf(ZTrace.IsError, ex.Message)
                    Throw New System.Exception("El paquete Microsoft Office no se encuentra instalado.")
                End Try

                Try
                    names = rk.GetSubKeyNames()

                    For Each s In names

                        Try
                            reg = Registry.LocalMachine.OpenSubKey(_PREKEY & "\" & s & _POSKEY)
                            If reg IsNot Nothing Then
                                path = reg.GetValue(_VALUE, String.Empty)
                                If path <> String.Empty Then

                                    path = path.Replace(Chr(34), String.Empty)
                                    If path.EndsWith("\") = False Then
                                        path = String.Concat(path, "\")
                                    End If
                                    path &= _EXEC

                                    If IO.File.Exists(path) Then
                                        Return path
                                    End If
                                End If
                            End If
                        Catch
                        End Try
                    Next

                    'En caso de no encontrar el outlook se intenta abrir mediante outlook.exe
                    Return _EXEC

                Catch ex As System.Exception
                    ZTrace.WriteLineIf(ZTrace.IsError, ex.Message)
                    'En caso de no encontrar el outlook se intenta abrir mediante outlook.exe
                    Return _EXEC
                End Try
            Catch ex As System.Exception
                ZTrace.WriteLineIf(ZTrace.IsError, ex.Message)
                'En caso de no encontrar el outlook se intenta abrir mediante outlook.exe
                Return _EXEC
            Finally
                rk = Nothing
                reg = Nothing
            End Try
        End Function
#End Region

    End Class
End Namespace

