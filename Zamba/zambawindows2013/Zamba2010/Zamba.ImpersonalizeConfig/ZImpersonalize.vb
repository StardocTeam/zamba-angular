Imports System
Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports System.Security.Permissions
Imports Microsoft.VisualBasic
Imports Microsoft.Win32.SafeHandles
Imports System.Runtime.ConstrainedExecution
Imports System.Security
Imports Zamba.Core

Public Class ZImpersonalize

    Dim impersonatedUser As WindowsImpersonationContext

    Public Enum LogonType As Integer
        [Default] = 0
        Interactive = 2
        Network = 3
        Batch = 4
        Service = 5
        Unlock = 7
        NetworkCleartText = 8
        NewCredentials = 9
    End Enum

    Public Enum LogonProvider As Integer
        [Default] = 0
    End Enum

#Region "Apis"
    <DllImport("advapi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Shared Function LogonUser(username As String, _
                               domain As String, _
                               password As String, _
                               logonType As LogonType, _
                               logonProvider As LogonProvider, _
                               ByRef userToken As SafeTokenHandle) As Boolean
    End Function

    Public Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Boolean

    Declare Auto Function DuplicateToken Lib "advapi32.dll" ( _
                        ByVal ExistingTokenHandle As IntPtr, _
                        ByVal ImpersonationLevel As Integer, _
                        ByRef DuplicateTokenHandle As IntPtr) As Integer

    Declare Auto Function RevertToSelf Lib "advapi32.dll" () As Long
#End Region

    <PermissionSetAttribute(SecurityAction.Demand, Name:="FullTrust")> _
    Public Function impersonateValidUser(ByVal userName As String, _
        ByVal domain As String, ByVal password As String) As Boolean


        Dim token As IntPtr = IntPtr.Zero
        Dim tokenDuplicate As IntPtr = IntPtr.Zero
        impersonateValidUser = False

        Dim safeTokenHandle As SafeTokenHandle = Nothing
        Dim tokenHandle As New IntPtr(0)

        Try
            If RevertToSelf() Then

                userName = userName.Trim()
                domain = domain.Trim()
                password = password.Trim()

                Dim returnValue As Boolean = LogonUser(userName, domain, password, LogonType.NetworkCleartText, LogonType.Default, safeTokenHandle)

                If returnValue <> False Then

                    'If DuplicateToken(token, 2, tokenDuplicate) <> 0 Then
                    Using safeTokenHandle
                        Dim success As String
                        If returnValue Then success = "Yes" Else success = "No"
                        ZTrace.WriteLineIf(ZTrace.IsVerbose,"Did LogonUser succeed? " + success)
                        ZTrace.WriteLineIf(ZTrace.IsVerbose,"Value of Windows NT token: " + safeTokenHandle.DangerousGetHandle().ToString())

                        ' Check the identity.
                        ZTrace.WriteLineIf(ZTrace.IsVerbose,"Before impersonation: " + WindowsIdentity.GetCurrent().Name)

                        ' Use the token handle returned by LogonUser. 
                        Using newId As New WindowsIdentity(safeTokenHandle.DangerousGetHandle())
                            Using impersonatedUser As WindowsImpersonationContext = newId.Impersonate()
                                If Not impersonatedUser Is Nothing Then
                                    impersonateValidUser = True
                                End If
                                ' Check the identity.
                                ZTrace.WriteLineIf(ZTrace.IsVerbose,"After impersonation: " + WindowsIdentity.GetCurrent().Name)

                                ' Free the tokens. 
                            End Using
                        End Using
                    End Using
                    'Else
                    '    Dim ret As Integer = Marshal.GetLastWin32Error()
                    '    ZTrace.WriteLineIf(ZTrace.IsError,"LogonUser failed with error code : {0}", ret)
                    '    Throw New System.ComponentModel.Win32Exception(ret)
                    'End If
                Else
                    Dim ret As Integer = Marshal.GetLastWin32Error()
                    ZTrace.WriteLineIf(ZTrace.IsError, "LogonUser failed with error code : {0}" & ret.ToString())
                    Throw New System.ComponentModel.Win32Exception(ret)
                End If
            End If
        Finally
            If Not tokenDuplicate.Equals(IntPtr.Zero) Then
                CloseHandle(tokenDuplicate)
            End If
            If Not token.Equals(IntPtr.Zero) Then
                CloseHandle(token)
            End If
        End Try

    End Function

    Public Sub undoImpersonation()
        impersonatedUser.Undo()
    End Sub
End Class

Public NotInheritable Class SafeTokenHandle
    Inherits SafeHandleZeroOrMinusOneIsInvalid

    Public Enum LogonType As Integer
        [Default] = 0
        Interactive = 2
        Network = 3
        Batch = 4
        Service = 5
        Unlock = 7
        NetworkCleartText = 8
        NewCredentials = 9
    End Enum

    Public Enum LogonProvider As Integer
        [Default] = 0
    End Enum

    Private Sub New()
        MyBase.New(True)

    End Sub 'New 

    <DllImport("advapi32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Shared Function LogonUser(username As String, _
                               domain As String, _
                               password As String, _
                               logonType As LogonType, _
                               logonProvider As LogonProvider, _
                               ByRef userToken As IntPtr) As Boolean
    End Function

    <DllImport("kernel32.dll"), ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), SuppressUnmanagedCodeSecurity()> _
    Private Shared Function CloseHandle(ByVal handle As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean

    End Function
    Protected Overrides Function ReleaseHandle() As Boolean
        Return CloseHandle(handle)

    End Function 'ReleaseHandle
End Class 'SafeTokenHandle
