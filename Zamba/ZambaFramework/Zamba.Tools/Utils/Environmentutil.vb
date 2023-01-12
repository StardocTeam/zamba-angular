Imports Microsoft.Win32
Imports System.Reflection
Imports Zamba.Membership
Imports System.Web

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Tools
''' Class	 : Tools.EnvironmentUtil
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para la obtencion de valores de entorno. Todos metodos estaticos
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	26/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class EnvironmentUtil
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene la version de windows que se esta utilizando en la PC actual
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Shared Function WinVersion() As Windows
        Select Case Environment.OSVersion.Platform
            Case PlatformID.Win32S
                Return Windows.Windows31
            Case PlatformID.Win32Windows
                Select Case Environment.OSVersion.Version.Minor
                    Case 0
                        Return Windows.Windows95
                    Case 10
                        Return Windows.Windows98
                    Case 90
                        Return Windows.WindowsME
                    Case Else
                        Return Windows.otherSO
                End Select
            Case PlatformID.Win32NT
                Select Case Environment.OSVersion.Version.Major
                    Case 3
                        Return Windows.WindowsNT351
                    Case 4
                        Return Windows.WindowsNT40
                    Case 5
                        Select Case Environment.OSVersion.Version.Minor
                            Case 0
                                Return Windows.Windows2000
                            Case 1
                                Return Windows.WindowsXp
                            Case 2
                                Return Windows.Windows2003
                        End Select
                    Case Else
                        Return Windows.otherSO
                End Select
            Case PlatformID.WinCE
                Return Windows.WindowsCE
        End Select
    End Function
    Enum Windows As Integer
        WindowsXp = 0
        Windows2003 = 1
        Windows2000 = 2
        WindowsME = 3
        Windows98 = 4
        WindowsCE = 5
        WindowsNT40 = 6
        WindowsNT351 = 7
        Windows95 = 8
        Windows31 = 9
        otherSO = 10
    End Enum

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene la version de windows que se esta utilizando en la PC actual
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function getWindowsVersion() As Windows
        Return WinVersion()
    End Function

    Public Shared Function GetOfficeVersion() As OfficeVersions
        Try
            Dim Reg As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Office\")

            Dim s As String
            For Each s In Reg.GetSubKeyNames
                Reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Office\" & s & "\Word\InstallRoot\")
                If Not Reg Is Nothing Then
                    If Not String.IsNullOrEmpty(Reg.GetValue("Path", String.Empty).ToString()) Then
                        Select Case s
                            Case "8.0"
                                Return OfficeVersions.Office97
                            Case "9.0"
                                Return OfficeVersions.Office2000
                            Case "10.0"
                                Return OfficeVersions.OfficeXP
                            Case "11.0"
                                Return OfficeVersions.Office2003
                            Case "12.0"
                                Return OfficeVersions.Office2007
                            Case "14.0"
                                Return OfficeVersions.Office2010
                            Case "15.0"
                                Return OfficeVersions.Office2013
                        End Select
                    End If
                End If

            Next
            Return OfficeVersions.NotInstaled
        Catch
            Return OfficeVersions.NotInstaled
        End Try
    End Function

    'Private Shared Function DefaultMail() As MailTypes

    'End Function

    Enum OfficeVersions As Integer
        NotInstaled = 0
        Office95 = 1
        Office97 = 2
        Office98 = 3
        Office2000 = 4
        OfficeXP = 5
        OfficeSystem = 6
        Office2003 = 7
        Office2007 = 8
        Office2010 = 9
        Office2013 = 10
    End Enum

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene la version de Office de la PC donde se esta ejecutando Zamba.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function getOfficePlatform() As OfficeVersions
        Try
            Select Case GetOfficeVersion()
                Case OfficeVersions.Office2000, OfficeVersions.OfficeXP
                    Return OfficeVersions.Office2000
                Case OfficeVersions.Office95, OfficeVersions.Office98, OfficeVersions.Office97
                    Return OfficeVersions.Office97
                Case OfficeVersions.Office2003
                    Return OfficeVersions.Office2003
                Case OfficeVersions.Office2007
                    Return OfficeVersions.Office2007
            End Select
            Return OfficeVersions.NotInstaled
        Catch
            Return OfficeVersions.NotInstaled
        End Try
    End Function

    ''' <summary>
    ''' Gets directory to save data
    ''' </summary>
    ''' <param name="dire"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Ezequiel] 15/05/09 Created.
    ''' </history>
    Public Shared Function GetTempDir(ByVal dire As String) As IO.DirectoryInfo
        Dim Dir As IO.DirectoryInfo
        Try
            Dir = New IO.DirectoryInfo(MembershipHelper.AppTempPath & dire)
            If Dir.Exists = False Then
                Dir.Create()
            End If
        Catch
            Dir = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & dire)
            If Dir.Exists = False Then
                Dir.Create()
            End If
        End Try
        Return Dir
    End Function
    ''' <summary>
    ''' Copy a file into a temporary directory inside appData
    ''' </summary>
    ''' <param name="fileFullPath">nombre de la ruta origen</param>
    ''' <param name="newName">nombre a cambiar del documento. Si no se desea cambiar el nombre, especificar String.Empty o ""</param>
    ''' <returns>devuelve un string con la ruta destino del archivo generado</returns>
    ''' <remarks>asd</remarks>
    ''' <history>
    ''' [pablo] 24/08/2011 Created.
    ''' </history>
    Public Shared Function CopyFilesToTemp(ByVal fileFullPath As String, ByVal newName As String) As String

        If String.IsNullOrEmpty(fileFullPath) Then
            Return String.Empty
        End If

        Const officeTemp As String = "\Zamba Software\OfficeTemp\"
        Dim fileName() As String
        Dim NewFileRoute, OriginalFileName, newFileExtension, filePath As String

        filePath = fileFullPath.Trim()
        fileName = filePath.Split("\")
        OriginalFileName = fileName((fileName.Length - 1))

        'si no existe officeTemp, lo creo
        If IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & officeTemp) Then
            IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & officeTemp)
        End If

        'si el documento debe renombrarse
        If newName <> String.Empty Then
            newFileExtension = IO.Path.GetExtension(fileFullPath)
            NewFileRoute = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & officeTemp & newName & newFileExtension.Trim()

            'Se remueven caracteres que son inválidos para el nombre de un archivo
            For Each invalidChar As Char In IO.Path.GetInvalidFileNameChars
                fileName(0) = fileName(0).Replace(invalidChar, String.Empty)
            Next

            'verifico que no exista un documento con el mismo nombre
            Dim i As Int16 = 0
            Do While IO.File.Exists(NewFileRoute)
                If i = 0 Then
                    NewFileRoute = NewFileRoute.Substring(0, NewFileRoute.LastIndexOf(".")) & "(" & i.ToString() & ")" & newFileExtension
                Else
                    NewFileRoute = NewFileRoute.Substring(0, NewFileRoute.LastIndexOf("(")) & "(" & i.ToString() & ")" & newFileExtension
                End If

                i += 1
            Loop

            'hago la copia del documento renombrado en officeTemp
            IO.File.Copy(fileFullPath, NewFileRoute, True)
            filePath = NewFileRoute
        Else
            'hago la copia del documento en officeTemp
            IO.File.Copy(fileFullPath, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & officeTemp & OriginalFileName)
        End If

        Return filePath
    End Function

    Enum MailTypes As Integer
        NetMail = 1
        OutLookMail = 2
        LotusNotesMail = 3
        OutLookExpress = 4
        Eudora = 5
    End Enum



    Public Shared Function curProtocol(Request As HttpRequest) As String
        Dim s As String
        If Request.ServerVariables("HTTPS") = "on" Then
            s = "s"
        Else
            s = ""
        End If

        Return strLeft(LCase(Request.ServerVariables("SERVER_PROTOCOL")), "/") & s
    End Function

    Public Shared Function curPageURL(Request As HttpRequest) As String
        Dim protocol, port As String

        protocol = curProtocol(Request)

        If Request.ServerVariables("SERVER_PORT") = "80" Then
            port = ""
        Else
            port = ":" & Request.ServerVariables("SERVER_PORT")
        End If

        curPageURL = protocol & "://" & Request.ServerVariables("SERVER_NAME") & port & Request.ServerVariables("SCRIPT_NAME")

    End Function

    Public Shared Function strLeft(str1 As String, str2 As String) As String
        strLeft = Left(str1, InStr(str1, str2) - 1)
    End Function
End Class

Public Class ReflectionLibrary
    Public Shared Function GetVersion(ByVal dllName As String) As String
        Dim dll As System.Reflection.Assembly = System.Reflection.Assembly.LoadFrom(dllName)
        Dim Version As String = dll.GetName().Version.ToString()
        Return Version
    End Function
End Class