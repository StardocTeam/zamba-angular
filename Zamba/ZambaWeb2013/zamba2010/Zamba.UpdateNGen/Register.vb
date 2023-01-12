Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.IO

Public Class Register

    Private Const EXE_CLIENTE As String = "Cliente.exe"
    Private Const MSG_Error As String = "El archivo {0} no existe en {1} ni en {2}"

    ''' <summary>
    ''' Registra Zamba en Windows
    ''' </summary>
    ''' <remarks>Se utiliza para registrar el comando ZAMBA</remarks>
    Public Shared Sub RegisterZamba()
        Dim pathLocal As String = Application.StartupPath
        Dim pathCliente As String = Path.Combine(pathLocal, EXE_CLIENTE)
        Dim dinfo As DirectoryInfo = Directory.GetParent(pathLocal)
        Dim pathLocalParent As String = dinfo.FullName
        Dim pathParentCliente As String = Path.Combine(pathLocalParent, EXE_CLIENTE)
        Dim dirCliente As String

        If File.Exists(pathCliente) Then
            dirCliente = pathCliente
        ElseIf File.Exists(pathParentCliente) Then
            dirCliente = pathParentCliente
        Else
            Throw New FileNotFoundException(String.Format(MSG_Error, EXE_CLIENTE, pathCliente, pathParentCliente))
        End If

        Dim reg As Microsoft.Win32.RegistryKey = Registry.ClassesRoot.CreateSubKey("Zamba")
        reg.SetValue(String.Empty, "URL:Zambass Protocol")
        reg.SetValue("URL Protocol", String.Empty)

        reg = Registry.ClassesRoot.CreateSubKey("Zamba\\DefaultIcon")
        reg.SetValue(String.Empty, dirCliente)

        reg = Registry.ClassesRoot.CreateSubKey("Zamba\\Shell\\Open\\Command")
        reg.SetValue(String.Empty, dirCliente & " %1")
    End Sub

    Public Shared Sub RegistrarOcx()
        Dim str As String = "regsvr32 /s " & Chr(34) & Application.StartupPath & "\com\powerweb.ocx" & Chr(34)
        Shell(str, AppWinStyle.Hide, False)

        Dim val As String = Application.StartupPath & "\com"
        Dim key As String = "Software\\Westbrook Technologies\\Fortis\\InstallInfo"
        Dim reg As Microsoft.Win32.RegistryKey = Registry.CurrentUser.CreateSubKey(key)
        reg.SetValue("path", val)
    End Sub

    Public Shared Sub RegistrarWia()
        Dim str As String = "regsvr32 /s " & Chr(34) & Application.StartupPath & "\Wiaaut.dll" & Chr(34)
        Shell(str, AppWinStyle.Hide, False)
    End Sub

End Class