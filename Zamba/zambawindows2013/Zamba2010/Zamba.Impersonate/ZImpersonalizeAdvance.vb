Imports System
Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports System.Security.Permissions
Imports Microsoft.VisualBasic
Imports Microsoft.Win32.SafeHandles
Imports System.Runtime.ConstrainedExecution
Imports System.Security
Imports System.Collections.Generic
Imports System.ComponentModel

Public Delegate Sub DRequiredImpersonationMethod(ByVal params As Object())

Public Class ZImpersonalizeAdvance

#Region "Attributes"
    Enum LogonType
        LOGON32_LOGON_INTERACTIVE = 2
        LOGON32_LOGON_NETWORK = 3
        LOGON32_LOGON_BATCH = 4
        LOGON32_LOGON_SERVICE = 5
        LOGON32_LOGON_UNLOCK = 7
        LOGON32_LOGON_NETWORK_CLEARTEXT = 8
        LOGON32_LOGON_NEW_CREDENTIALS = 9
    End Enum

    Enum LogonProvider As Int32
        LOGON32_PROVIDER_DEFAULT = 0
        LOGON32_PROVIDER_WINNT35 = 1
        LOGON32_PROVIDER_WINNT40 = 2
        LOGON32_PROVIDER_WINNT50 = 3
    End Enum

    Enum ImpersonationLevel As Integer
        Anonymous = 0
        Identification = 1
        Impersonation = 2
        Delegation = 3
    End Enum

    Private LOGON_PROVIDER As LogonProvider
    Private LOGON_TYPE As LogonType
    Private userName, password, domain As String
    Private delegateMethod As DRequiredImpersonationMethod
    Private params As Object()

    Private impersonationContext As WindowsImpersonationContext
    Private tokenDuplicate As IntPtr
    Private profileInfo As ProfileInfo
#End Region

#Region "Constructors"
    Public Sub New(ByVal userName As String, _
               ByVal password As String, _
               ByVal domain As String, _
               ByVal logonType As LogonType, _
               ByVal delegateMethod As DRequiredImpersonationMethod, _
               Optional ByVal params As Object() = Nothing)
        Me.userName = userName
        Me.password = password
        Me.domain = domain
        Me.LOGON_TYPE = logonType
        Me.delegateMethod = delegateMethod
        Me.params = params
    End Sub
#End Region

#Region "Apis"
    Private Declare Auto Function LogonUser Lib "advapi32.dll" (ByVal lpszUsername As [String], _
    ByVal lpszDomain As [String], ByVal lpszPassword As [String], _
    ByVal dwLogonType As Integer, ByVal dwLogonProvider As Integer, _
    <Out()> ByRef phToken As SafeTokenHandle) As Boolean

    'Public Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Boolean
    Declare Function LogonUserA Lib "advapi32.dll" (ByVal lpszUsername As String, _
                           ByVal lpszDomain As String, _
                           ByVal lpszPassword As String, _
                           ByVal dwLogonType As Integer, _
                           ByVal dwLogonProvider As Integer, _
                           ByRef phToken As IntPtr) As Integer

    Declare Auto Function DuplicateToken Lib "advapi32.dll" ( _
                        ByVal ExistingTokenHandle As IntPtr, _
                        ByVal ImpersonationLevel As Integer, _
                        ByRef DuplicateTokenHandle As IntPtr) As Integer

    Declare Auto Function RevertToSelf Lib "advapi32.dll" () As Long
    Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Long

    <DllImport("userenv.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function LoadUserProfile(hToken As IntPtr, ByRef lpProfileInfo As ProfileInfo) As Boolean
    End Function

    <DllImport("Userenv.dll", CallingConvention:=CallingConvention.Winapi, SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function UnloadUserProfile(hToken As IntPtr, lpProfileInfo As IntPtr) As Boolean
    End Function
#End Region

#Region "Methods"
    ''' <summary>
    ''' Impersona la ejecución de un delegado a un usuario específico
    ''' </summary>
    ''' <remarks></remarks>
    <PermissionSetAttribute(SecurityAction.Demand, Name:="FullTrust")> _
    Public Sub ImpersonateAndExecuteDelegate()
        Dim safeTokenHandle As SafeTokenHandle

        If LOGON_TYPE = LogonType.LOGON32_LOGON_NEW_CREDENTIALS Then
            LOGON_PROVIDER = LogonProvider.LOGON32_PROVIDER_WINNT50
        Else
            LOGON_PROVIDER = LogonProvider.LOGON32_PROVIDER_DEFAULT
        End If

        ' Call LogonUser to obtain a handle to an access token. 
        Dim returnValue As Boolean = LogonUser(userName, domain, password, CInt(LOGON_TYPE), LOGON_PROVIDER, safeTokenHandle)

        If False = returnValue Then
            Dim ret As Integer = Marshal.GetLastWin32Error()
            Trace.WriteLine("LogonUser failed with error code : " & ret.ToString)
            Throw New System.ComponentModel.Win32Exception(ret)
        End If

        Using safeTokenHandle
            Dim success As String
            If returnValue Then success = "Yes" Else success = "No"
            Trace.WriteLine(("Did LogonUser succeed? " + success))
            Trace.WriteLine(("Value of Windows NT token: " + safeTokenHandle.DangerousGetHandle().ToString()))

            ' Check the identity.
            Trace.WriteLine(("Before impersonation: " + WindowsIdentity.GetCurrent().Name))
            Trace.WriteLine(("Impersonation level: " + WindowsIdentity.GetCurrent().ImpersonationLevel.ToString()))

            ' Use the token handle returned by LogonUser. 
            Using impersonatedUser As WindowsImpersonationContext = WindowsIdentity.Impersonate(safeTokenHandle.DangerousGetHandle())
                ' Check the identity.
                Trace.WriteLine(("After impersonation: " + WindowsIdentity.GetCurrent().Name))
                Trace.WriteLine(("Impersonation level: " + WindowsIdentity.GetCurrent().ImpersonationLevel.ToString()))
                delegateMethod(params)
            End Using ' Free the tokens. 
        End Using

        Trace.WriteLine(("After executing delegate: " + WindowsIdentity.GetCurrent().Name))
        Trace.WriteLine(("Impersonation level: " + WindowsIdentity.GetCurrent().ImpersonationLevel.ToString()))
    End Sub 'ImpersonateAndExecuteDelegate 

    Public Function ImpersonateValidUser() As Boolean

        Dim tempWindowsIdentity As WindowsIdentity
        Dim token As IntPtr = IntPtr.Zero
        tokenDuplicate = IntPtr.Zero
        ImpersonateValidUser = False

        Try
            If RevertToSelf() Then
                Trace.WriteLine(("Before impersonation: " + WindowsIdentity.GetCurrent().Name))
                Trace.WriteLine(("Impersonation level: " + WindowsIdentity.GetCurrent().ImpersonationLevel.ToString()))
                If LogonUserA(userName, domain, password, CInt(LOGON_TYPE), LOGON_PROVIDER, token) <> 0 Then
                    If DuplicateToken(token, ImpersonationLevel.Impersonation, tokenDuplicate) <> 0 Then
                        tempWindowsIdentity = New WindowsIdentity(tokenDuplicate)
                        impersonationContext = tempWindowsIdentity.Impersonate()
                        If Not impersonationContext Is Nothing Then
                            Trace.WriteLine(("After impersonation: " + WindowsIdentity.GetCurrent().Name))
                            Trace.WriteLine(("Impersonation level: " + WindowsIdentity.GetCurrent().ImpersonationLevel.ToString()))

                            profileInfo = New ProfileInfo()
                            profileInfo.dwSize = Marshal.SizeOf(profileInfo)
                            profileInfo.lpUserName = userName
                            profileInfo.dwFlags = 1
                            Dim loadSuccess As [Boolean] = LoadUserProfile(tokenDuplicate, profileInfo)

                            If Not loadSuccess Then
                                Trace.WriteLine("LoadUserProfile() failed with error code: " + Marshal.GetLastWin32Error())
                                Throw New Win32Exception(Marshal.GetLastWin32Error())
                            End If

                            If profileInfo.hProfile = IntPtr.Zero Then
                                Trace.WriteLine("LoadUserProfile() failed -  HKCU handle was not loaded. Error code: " + Marshal.GetLastWin32Error())
                                Throw New Win32Exception(Marshal.GetLastWin32Error())
                            End If

                            If Not tokenDuplicate.Equals(IntPtr.Zero) Then
                                CloseHandle(tokenDuplicate)
                            End If
                            If Not token.Equals(IntPtr.Zero) Then
                                CloseHandle(token)
                            End If

                            ImpersonateValidUser = True
                        End If
                    Else
                        Dim ret As Integer = Marshal.GetLastWin32Error()
                        Trace.WriteLine("LogonUser failed with error code : " & ret.ToString)
                        Throw New System.ComponentModel.Win32Exception(ret)
                    End If
                Else
                    Dim ret As Integer = Marshal.GetLastWin32Error()
                    Trace.WriteLine("LogonUser failed with error code : " & ret.ToString)
                    Throw New System.ComponentModel.Win32Exception(ret)
                End If
            End If
        Finally

        End Try

    End Function

    Public Sub undoImpersonation()
        ' Unload user profile
        ' MSDN remarks 
        ' http:'msdn.microsoft.com/en-us/library/bb762282
        ' (VS.85).aspx
        ' Before calling UnloadUserProfile you should ensure 
        ' that all handles to keys that you have opened in the
        ' user's registry hive are closed. If you do not close 
        ' all open registry handles, the user's profile fails
        ' to unload. For more information, 
        ' see Registry Key Security 
        ' and Access Rights and Registry Hives.
        UnloadUserProfile(tokenDuplicate, profileInfo.hProfile)

        ' Undo impersonation
        impersonationContext.Undo()

        Trace.WriteLine(("After undoing impersonation: " + WindowsIdentity.GetCurrent().Name))
        Trace.WriteLine(("Impersonation level: " + WindowsIdentity.GetCurrent().ImpersonationLevel.ToString()))
    End Sub
#End Region

End Class 'ZImpersonalizeAdvance

Public NotInheritable Class SafeTokenHandle
    Inherits SafeHandleZeroOrMinusOneIsInvalid

    Private Sub New()
        MyBase.New(True)

    End Sub 'New 

    Private Declare Auto Function LogonUser Lib "advapi32.dll" (ByVal lpszUsername As [String], _
            ByVal lpszDomain As [String], ByVal lpszPassword As [String], _
            ByVal dwLogonType As Integer, ByVal dwLogonProvider As Integer, _
            ByRef phToken As IntPtr) As Boolean
    <DllImport("kernel32.dll"), ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), SuppressUnmanagedCodeSecurity()> _
    Private Shared Function CloseHandle(ByVal handle As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
        '¿
        'En caso de error existe tambien:
        '   Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Long
        'Uso: 
        '   If Not handle.Equals(IntPtr.Zero) Then
        '       CloseHandle(handle)
        '   End If
        '?
    End Function
    Protected Overrides Function ReleaseHandle() As Boolean
        Return CloseHandle(handle)
    End Function 'ReleaseHandle
End Class 'SafeTokenHandle

<StructLayout(LayoutKind.Sequential)> _
Public Structure ProfileInfo
    '''
    ''' Specifies the size of the structure, in bytes.
    '''
    Public dwSize As Integer

    '''
    ''' This member can be one of the following flags: 
    ''' PI_NOUI or PI_APPLYPOLICY
    '''
    Public dwFlags As Integer

    '''
    ''' Pointer to the name of the user.
    ''' This member is used as the base name of the directory 
    ''' in which to store a new profile.
    '''
    Public lpUserName As String

    '''
    ''' Pointer to the roaming user profile path.
    ''' If the user does not have a roaming profile, this member can be NULL.
    '''
    Public lpProfilePath As String

    '''
    ''' Pointer to the default user profile path. This member can be NULL.
    '''
    Public lpDefaultPath As String

    '''
    ''' Pointer to the name of the validating domain controller, in NetBIOS format.
    ''' If this member is NULL, the Windows NT 4.0-style policy will not be applied.
    '''
    Public lpServerName As String

    '''
    ''' Pointer to the path of the Windows NT 4.0-style policy file. 
    ''' This member can be NULL.
    '''
    Public lpPolicyPath As String

    '''
    ''' Handle to the HKEY_CURRENT_USER registry key.
    '''
    Public hProfile As IntPtr
End Structure
