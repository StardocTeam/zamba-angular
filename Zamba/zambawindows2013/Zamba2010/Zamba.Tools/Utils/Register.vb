Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.IO

Public Class Register

    Private Const EXE_CLIENTE As String = "Cliente.exe"

    ''' <summary>
    ''' Registra Zamba
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CrearRegZamba() As Boolean
      

            Dim pathLocal As String = Application.StartupPath
            Dim pathCliente As String = Path.Combine(pathLocal, EXE_CLIENTE)
            Dim pathLocalParent As String = String.Empty
            Dim pathParentCliente As String = String.Empty

            Dim dinfo As DirectoryInfo

            dinfo = Directory.GetParent(pathLocal)
            pathLocalParent = dinfo.FullName

            pathParentCliente = Path.Combine(pathLocalParent, EXE_CLIENTE)

            Dim val As String = "URL:Zamba Protocol"
            Dim key As String = "Zamba"
            Dim reg As Microsoft.Win32.RegistryKey = Registry.ClassesRoot.CreateSubKey(key)
            reg.SetValue("", val)
            reg.SetValue("URL Protocol", "")

            key = "Zamba\\DefaultIcon"
            reg = Registry.ClassesRoot.CreateSubKey(key)
            reg.SetValue("", pathCliente.Replace("\\", "\"))

            key = "Zamba\\Shell\\Open\\Command"
            reg = Registry.ClassesRoot.CreateSubKey(key)
            reg.SetValue("", pathCliente.Replace("\\", "\") & " %1")

            Return True
       
    End Function



    Public Function regReportsBuilder() As Boolean


        Dim pathLocal As String = Application.StartupPath
        Dim pathReports As String = Path.Combine(pathLocal, EXE_CLIENTE)
        Dim pathLocalParent As String = String.Empty
        Dim pathParenReports As String = String.Empty

        Dim dinfo As DirectoryInfo

        dinfo = Directory.GetParent(pathLocal)
        pathLocalParent = dinfo.FullName

        pathParenReports = Path.Combine(pathLocalParent, EXE_CLIENTE)

        Dim val As String = "URL:Zamba Protocol"
        Dim key As String = "Reports"
        Dim reg As Microsoft.Win32.RegistryKey = Registry.ClassesRoot.CreateSubKey(key)
        reg.SetValue("", val)
        reg.SetValue("URL Protocol", "")

        key = "Reports\\DefaultIcon"
        reg = Registry.ClassesRoot.CreateSubKey(key)
        reg.SetValue("", pathReports.Replace("\\", "\"))

        key = "Reports\\Shell\\Open\\Command"
        reg = Registry.ClassesRoot.CreateSubKey(key)
        reg.SetValue("", pathReports.Replace("\\", "\") & " %1")

        Return True

    End Function
End Class
