Imports Zamba.Servers
Imports Zamba.Core
<Ipreprocess.PreProcessName("Aseguradora"), Ipreprocess.PreProcessHelp("Importación de pólizas PDF, con los tres primeros datos, que son Seccion|Poliza|Endoso y debemos completar con el nombre de la cia según el directorio de donde provenga.")> _
Public Class ippAseguradora
    Implements Ipreprocess

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Importación de pólizas PDF, con los tres primeros datos, que son Seccion|Poliza|Endoso y debemos completar con el nombre de la cia según el directorio de donde provenga."
    End Function

#Region "Eventos"
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

#End Region
    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim I As Int32
        RaiseEvent PreprocessMessage("Comienzo del Proproceso para agregar el Codigo de la Compañia")
        For I = 0 To Files.Count - 1
            RaiseEvent PreprocessMessage("Procesando " & Files(I))
            processFile(Files(I), param(0), xml)
        Next
        Return Files
    End Function
    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Dim Archivo As New IO.FileInfo(File)
        Dim Company As String = ""
        Try
            Dim companyFile As New IO.StreamReader(Archivo.DirectoryName & "\company.txt")
            Company = companyFile.ReadLine
            companyFile.Close()
        Catch ex As Exception
            RaiseEvent PreprocessError("No se encuentra el Archivo company.txt, el cual debe contener el nombre de la compañia")
        End Try
        Dim Dir As String = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName

        Dim sw As New IO.StreamWriter(Dir & "\tempo.txt")
        Dim sr As New IO.StreamReader(File)
        Dim linea As New System.Text.StringBuilder
        While sr.Peek <> -1
            linea.Append(sr.ReadLine)
            linea.Append("|")
            linea.Append(GetCompanyCode(Company))
            sw.WriteLine(linea.ToString)
        End While
        sw.Close()
        sr.Close()
        Try
            Dim fi As New IO.FileInfo(Dir & "\tempo.txt")
            fi.CopyTo(Archivo.FullName, True)
            fi.Delete()
        Catch
        End Try
    End Function
    ''' <summary>
    ''' Obtiene el codigo en base al nombre
    ''' </summary>
    ''' <param name="nombre">Nombre de la compañia que se desea obtener el ID</param>
    ''' <returns>Codigo de la compañia, Int32</returns>
    ''' <remarks></remarks>
    Private Shared Function GetCompanyCode(ByVal nombre As String) As Int32
        Try
            'DONE:se agrego el replace para poder sacar los apostrofes de las sonsultas y evitar que se rompan[sebastian]
            Dim sql As String = "Select Codigo from SLST_S45 where replace(Descripcion, '''', ' ')='" & nombre & "'"
            Dim resultado As String = Server.Con.ExecuteScalar(CommandType.Text, sql)
            Return Int32.Parse(resultado)
        Catch
        End Try
    End Function
#Region "XML"
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml
    End Sub
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
    End Function
#End Region
End Class
