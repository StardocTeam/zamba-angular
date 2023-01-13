Imports System
Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports System.Security.Permissions
Imports Microsoft.VisualBasic
Imports Microsoft.Win32.SafeHandles
Imports System.Runtime.ConstrainedExecution
Imports System.Security

Public Class ZImpersonalize
    Dim impersonationContext As WindowsImpersonationContext

    Const LOGON32_LOGON_INTERACTIVE As Integer = 2
    Const LOGON32_PROVIDER_DEFAULT As Integer = 0

#Region "Apis"
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
#End Region

    Public Function impersonateValidUser(ByVal userName As String, _
        ByVal domain As String, ByVal password As String) As Boolean

        Dim tempWindowsIdentity As WindowsIdentity
        Dim token As IntPtr = IntPtr.Zero
        Dim tokenDuplicate As IntPtr = IntPtr.Zero
        impersonateValidUser = False

        Try
            If RevertToSelf() Then

                userName = userName.Trim()
                domain = domain.Trim()
                password = password.Trim()

                If LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                             LOGON32_PROVIDER_DEFAULT, token) <> 0 Then
                    If DuplicateToken(token, 2, tokenDuplicate) <> 0 Then
                        tempWindowsIdentity = New WindowsIdentity(tokenDuplicate)
                        impersonationContext = tempWindowsIdentity.Impersonate()
                        If Not impersonationContext Is Nothing Then
                            impersonateValidUser = True
                        End If
                    Else
                        Dim ret As Integer = Marshal.GetLastWin32Error()
                        Throw New System.ComponentModel.Win32Exception(ret)
                    End If
                Else
                    Dim ret As Integer = Marshal.GetLastWin32Error()
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
        impersonationContext.Undo()
    End Sub
End Class
