Imports System.IO
Imports System.Object
Imports Zamba
Imports Zamba.Core

Public Enum BrowserEmulationVersion As Integer
    defaul = 0
    Version7 = 7000
    Version8 = 8000
    Version8Standards = 8888
    Version9 = 9000
    Version9Standards = 9999
    Version10 = 10000
    Version10Standards = 10001
    Version11 = 11000
    Version11Edge = 11001
End Enum

Public Class WBEmulator
    Public InternetExplorerRootKey As String = "Software\Microsoft\Internet Explorer"
    Public BrowserEmulationKey As String = InternetExplorerRootKey + "\Main\FeatureControl\FEATURE_BROWSER_EMULATION"


    Public Function GetInternetExplorerMajorVersion() As Integer
        Dim result As Integer = 0
        Try
            Dim key As Microsoft.Win32.RegistryKey

            key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(InternetExplorerRootKey)

            If Not IsNothing(key) Then

                Dim value As Object

                If IsNothing(value = key.GetValue("svcVersion", vbNull)) Then
                    key.GetValue("Version", vbNull)
                Else
                    Dim Version As String
                    Dim separator As Integer

                    Version = value.ToString()
                    separator = Version.IndexOf(".")
                    If Not (separator = -1) Then Integer.TryParse(Version.Substring(0, separator), result)
                End If
            End If

        Catch ex As Exception
            'ZClass.raiseerror(ex)

        End Try
        Return result
    End Function

    Public Function GetBrowserEmulationVersion() As BrowserEmulationVersion
        Dim result As BrowserEmulationVersion = BrowserEmulationVersion.defaul

        Try
            Dim key As Microsoft.Win32.RegistryKey

            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(BrowserEmulationKey, True)

            If Not IsNothing(key) Then

                Dim programName As String = Path.GetFileName(Environment.GetCommandLineArgs()(0))
                Dim value As Object = key.GetValue(programName, vbNull)

                If Not IsNothing(value) Then result = Convert.ToInt32(value)
            End If

        Catch ex As Exception
            'ZClass.raiseerror(ex)
        End Try
        Return result
    End Function

    Public Function SetBrowserEmulationVersion(ByVal browserEmulationVersion As BrowserEmulationVersion) As Boolean
        Dim result As Boolean = False

        Try
            Dim key As Microsoft.Win32.RegistryKey

            If Not IsNothing(key) Then

                Dim programName As String = Path.GetFileName(Environment.GetCommandLineArgs()(0))

                If browserEmulationVersion <> BrowserEmulationVersion.defaul Then

                    key.SetValue(programName, browserEmulationVersion, Microsoft.Win32.RegistryValueKind.DWord)

                Else
                    key.DeleteValue(programName, False)

                End If

                result = True
            End If
        Catch ex As Exception
            'ZClass.raiseerror(ex)
        End Try
        Return result
    End Function
    Public Function SetBrowserEmulationVersion() As Boolean
        Dim ieVersion As Integer

        Dim emulationCode As BrowserEmulationVersion

        ieVersion = GetInternetExplorerMajorVersion()

        If ieVersion >= 11 Then

            emulationCode = BrowserEmulationVersion.Version11
        Else
            Select Case ieVersion
                Case 10
                    emulationCode = BrowserEmulationVersion.Version10
                Case 9
                    emulationCode = BrowserEmulationVersion.Version9
                Case 8
                    emulationCode = BrowserEmulationVersion.Version8
                Case Else
                    emulationCode = BrowserEmulationVersion.Version7
            End Select

        End If

        Return SetBrowserEmulationVersion(emulationCode)
    End Function

    Public Function IsBrowserEmulationSet() As Boolean

        Return GetBrowserEmulationVersion() <> BrowserEmulationVersion.defaul
    End Function


End Class
