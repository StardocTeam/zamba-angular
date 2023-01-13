Imports System
Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports System.Security.Permissions
Imports Microsoft.VisualBasic
Imports Microsoft.Win32.SafeHandles
Imports System.Runtime.ConstrainedExecution
Imports System.Security

Public Class ZImpersonalize
    Dim context As WindowsImpersonationContext

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
    <DllImport("advapi32.dll", charset:=CharSet.Auto, SetLastError:=True)>
    Shared Function LogonUser(username As String, _
                               domain As String, _
                               password As String, _
                               logonType As LogonType, _
                               logonProvider As LogonProvider, _
                               ByRef userToken As IntPtr) As Boolean
    End Function

    Declare Auto Function DuplicateToken Lib "advapi32.dll" ( _
                        ByVal ExistingTokenHandle As IntPtr, _
                        ByVal ImpersonationLevel As Integer, _
                        ByRef DuplicateTokenHandle As IntPtr) As Integer

    Declare Auto Function RevertToSelf Lib "advapi32.dll" () As Long

    Declare Auto Function CloseHandle Lib "kernel32.dll" (ByVal handle As IntPtr) As Long
#End Region

    <PermissionSetAttribute(SecurityAction.Demand, Name:="FullTrust")> _
    Public Function impersonateValidUser(ByVal userName As String, ByVal domain As String, ByVal password As String) As Boolean
        Dim identity As WindowsIdentity
        Dim token As IntPtr = IntPtr.Zero
        'Dim tokenDuplicate As IntPtr = IntPtr.Zero
        Dim fail As Boolean = False
        Dim success As Boolean

        Trace.Write("RevertToSelf: ")
        If RevertToSelf() Then
            Trace.WriteLine("OK")
            userName = userName.Trim()
            domain = domain.Trim()
            password = password.Trim()

            Trace.Write("LogonUser: ")
            success = LogonUser(userName, domain, password, LogonType.NetworkCleartText, LogonProvider.Default, token)
            Trace.WriteLine(success)

            If success Then
                'If DuplicateToken(token, 2, tokenDuplicate) <> 0 Then
                '    tempWindowsIdentity = New WindowsIdentity(tokenDuplicate)
                '    impersonationContext = tempWindowsIdentity.Impersonate()
                '    If Not impersonationContext Is Nothing Then
                '        Return True
                '    End If
                'Else
                '    fail = True
                'End If

                Trace.WriteLine("Generando nueva identidad.")
                identity = New WindowsIdentity(token)

                Trace.WriteLine("Impersonando.")
                context = identity.Impersonate()

                'CloseHandle(token)

                If context Is Nothing Then
                    Trace.WriteLine("FAIL.")
                    fail = True
                Else
                    Trace.WriteLine("OK.")
                    Return True
                End If
            Else
                fail = True
            End If

            If fail Then
                Dim ret As Integer = Marshal.GetLastWin32Error()
                Throw New System.ComponentModel.Win32Exception(ret)
            End If
        Else
            Trace.WriteLine("FALSE")
        End If

        'If Not tokenDuplicate.Equals(IntPtr.Zero) Then
        '    CloseHandle(tokenDuplicate)
        'End If
        If Not token.Equals(IntPtr.Zero) Then
            CloseHandle(token)
        End If

        Return False
    End Function

    Public Sub undoImpersonation()
        context.Undo()
    End Sub
End Class
