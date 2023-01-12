Imports Microsoft.Win32
Imports System.Reflection
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

    'Private Shared Function WinPlatForm() As String
    '    Return Environment.OSVersion.Platform.ToString
    'End Function
    'Private Shared Function ApplicationTime() As String
    '    Return Environment.TickCount
    'End Function
    'Private Shared Function FisicMemoryAsigned() As String
    '    Return Environment.WorkingSet
    'End Function

    Private Shared Function OfficeVersion() As OfficeVersions
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
                            Return OfficeVersions.Office2004
                    End Select
                End If
            End If

        Next
        Return OfficeVersions.NotInstaled
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
        Office2004 = 8
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
        Select Case OfficeVersion()
            Case OfficeVersions.Office2000, OfficeVersions.OfficeXP
                Return OfficeVersions.Office2000
            Case OfficeVersions.Office95, OfficeVersions.Office98, OfficeVersions.Office97
                Return OfficeVersions.Office97
            Case OfficeVersions.Office2003
                Return OfficeVersions.Office2003
            Case OfficeVersions.Office2004
                Return OfficeVersions.Office2004
        End Select
        Return OfficeVersions.NotInstaled
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
            Dir = New IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software" & dire)
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
        Dim dll As System.Reflection.Assembly = System.Reflection.Assembly.LoadFile(dllName)
        Dim Version As String = dll.GetName().Version.ToString()
        Return Version
    End Function
End Class