Imports Zamba.OfficeCommon
Imports Microsoft.Office.Interop.Outlook
Imports Microsoft.Win32

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
            'If Process.GetProcessesByName("OUTLOOK").Length > 0 Then
            '    Trace.WriteLine("Trying to catch outlook...")
            '    _APP = CType(GetObject(, "Outlook.Application"), Application)
            '    Trace.WriteLine("Outlook Catched...")
            'Else

            'Se verifica que la instancia y el proceso de outlook se encuentren correctos, caso contrario genera una instancia.
            '_tempProcesses = Process.GetProcessesByName("OUTLOOK")
            'If _app Is Nothing OrElse _tempProcesses.Length = 0 OrElse _procId <> _tempProcesses(0).Id Then

            '    Trace.WriteLine("Shell Outlook")
            '    Shell(GetOutlookPath(), AppWinStyle.NormalFocus, False)
            '    Trace.WriteLine("Trying to catch outlook...")
            '    Dim abierto As Boolean = False
            '    Dim contador As Integer = 0
            '    Do
            '        Try
            '            Trace.WriteLine("Trying to catch outlook, attemp " + Convert.ToString(contador + 1))
            '            _app = CType(GetObject(, "Outlook.Application"), Application)
            '            Trace.WriteLine("Outlook Catched...")
            '            abierto = True
            '        Catch ex As System.Exception
            '            contador = contador + 1
            '            Threading.Thread.Sleep(1500)
            '        End Try
            '    Loop Until (abierto OrElse contador = 10)
            '    If abierto Then
            '        _procId = Process.GetProcessesByName("OUTLOOK")(0).Id
            '    End If
            'End If

            'Return _app

            If Process.GetProcessesByName("OUTLOOK").Length > 0 Then
                Trace.WriteLine("Trying to catch outlook...")
                _app = CType(GetObject(, "Outlook.Application"), Application)
                Trace.WriteLine("Outlook Catched...")
                'outlookNS = app.GetNamespace(OutlookMailParameters.MAPI_NAMESPACE)
                'MAPIFolderSentMail = outlookNS.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderSentMail)
                System.Windows.Forms.Application.DoEvents()
            Else
                Trace.WriteLine("Shell Outlook")
                Shell(GetOutlookPath(), AppWinStyle.NormalFocus, False)
                Trace.WriteLine("Trying to catch outlook...")
                Dim abierto As Boolean = False
                Dim contador As Integer = 0
                Do
                    Try
                        Trace.WriteLine("Trying to catch outlook, attemp " + Convert.ToString(contador + 1))
                        _app = CType(GetObject(, "Outlook.Application"), Application)
                        Trace.WriteLine("Outlook Catched...")
                        abierto = True
                    Catch ex As System.Exception
                        contador = contador + 1
                        Threading.Thread.Sleep(4000)
                    End Try
                Loop Until (abierto = True OrElse contador = 10)
            End If

            Return _app


        End Function


        ''' <summary>
        ''' Obtiene la ruta de Outlook buscando en el registro.
        ''' </summary>
        ''' <returns>Devuelve la ruta de outlook.</returns>
        Public Shared Function GetOutlookPath() As String
            Try
                Dim rk As RegistryKey

                Try
                    rk = Registry.LocalMachine.OpenSubKey(_PREKEY)
                Catch ex As System.Exception
                    Throw New System.Exception("El paquete Microsoft Office no se encuentra instalado.")
                End Try

                Dim names() As String

                Try
                    names = rk.GetSubKeyNames()

                    Dim s As String
                    Dim reg As RegistryKey = Nothing
                    Dim path As String
                    Dim command As String

                    For Each s In names

                        Try
                            reg = Registry.LocalMachine.OpenSubKey(_PREKEY & "\" & s & _POSKEY)
                        Catch ex As System.Exception
                            'ZClass.raiseerror(ex)
                        End Try

                        If reg IsNot Nothing Then
                            path = reg.GetValue(_VALUE, String.Empty)
                            If path <> String.Empty Then

                                path = path.Replace(Chr(34), String.Empty)
                                If path.EndsWith("\") = False Then
                                    path = String.Concat(path, "\")
                                End If
                                Dim finf As New IO.FileInfo(path & _EXEC)

                                If finf.Exists = True Then
                                    command = Chr(34) & path & _EXEC
                                    Return command
                                End If
                            End If
                        End If
                    Next
                    Return String.Empty

                Catch ex As System.Exception
                    Return String.Empty

                End Try

            Catch ex As System.Exception
                Return String.Empty
            End Try
        End Function

#End Region

#Region "Código obsoleto"
        '''' <summary>
        '''' Id del proceso de outlook. Se utiliza para verificar que el proceso abierto 
        '''' de outlook sea el mismo que utiliza la instancia. Lo que pasaba era que si 
        '''' se tenía Zamba y outlook abierto, se cerraba el outlook y se volvía a abrir
        '''' el msg nunca era mostrado a causa de que la instancia app 'pertenecia' al 
        '''' proceso anteriormente cerrado.
        '''' </summary>
        'Private Shared outProcId As Int32 = 0

        '''' <summary>
        '''' Array de procesos temporal para mejorar la performance al solicitar el proceso de outlook.
        '''' </summary>
        'Private Shared tempProcesses As System.Diagnostics.Process()

        'Public Shared Function GetOutlook() As OutlookInterop
        '    'Se verifica que la instancia y el proceso de outlook se encuentren correctos, caso contrario genera una instancia.
        '    tempProcesses = Process.GetProcessesByName("OUTLOOK")
        '    If app Is Nothing OrElse tempProcesses.Length = 0 OrElse outProcId <> tempProcesses(0).Id Then
        '        app = New Application()
        '        outlookNS = app.GetNamespace(OutlookMailParameters.MAPI_NAMESPACE)
        '        MAPIFolderSentMail = outlookNS.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderSentMail)
        '        outProcId = Process.GetProcessesByName("OUTLOOK")(0).Id
        '    End If

        '    Return New OutlookInterop(app, outlookNS, MAPIFolderSentMail)
        'End Function

        'Public Shared Function GetOutlookApp() As Application
        '    'Se verifica que la instancia y el proceso de outlook se encuentren correctos, caso contrario genera una instancia.
        '    tempProcesses = Process.GetProcessesByName("OUTLOOK")
        '    If app Is Nothing OrElse tempProcesses.Length = 0 OrElse outProcId <> tempProcesses(0).Id Then
        '        app = New Application()
        '        outlookNS = app.GetNamespace(OutlookMailParameters.MAPI_NAMESPACE)
        '        MAPIFolderSentMail = outlookNS.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderSentMail)
        '        outProcId = Process.GetProcessesByName("OUTLOOK")(0).Id
        '    End If

        '    Return app
        'End Function
#End Region

    End Class
End Namespace

