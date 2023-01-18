Imports Zamba.Core
Imports Zamba.Servers
<Ipreprocess.PreProcessName("Importación Cable"), Ipreprocess.PreProcessHelp("Este proceso genera un archivo de texto con dos campos, rutacompleta y Barcode nuevo. Utilizado solo para la migración de los viejos barcodes")> _
Public Class ippImportacionCable
    Implements Ipreprocess
    Dim ds As New DataSet
#Region "Help"
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Este proceso genera un archivo de texto con dos campos, rutacompleta y Barcode nuevo. Utilizado solo para la migración de los viejos barcodes"
    End Function
#End Region
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:Implentar
        Return String.Empty
    End Function
#Region "Eventos"
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage
#End Region
    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        'TODO:Implementar 
        Return Nothing
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Dim sw As New IO.StreamWriter(Application.StartupPath & "\temporary.txt")
        Try
            CargarDs()
            Dim sr As New IO.StreamReader(File)
            Dim linea As String
            Dim campos() As String
            While sr.Peek <> -1
                linea = sr.ReadLine
                campos = linea.Split(","c)
                sw.WriteLine(campos(1).ToString)
                sw.Write("|")
                sw.Write(GetNewCode(campos(2).ToString))
            End While
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        Finally
            sw.Close()
        End Try
        Return Nothing
    End Function
    Private Sub CargarDs()
        Try
            Dim sql As String = "Select Barcode,Barcode2 from DOC_I2_X"
            ds = Server.Con.ExecuteDataset(Server.Con.ConString, CommandType.Text, sql)
        Catch ex As Exception
        End Try
    End Sub
    Private Function GetNewCode(ByVal Codigo As String) As String
        Try
            Dim row As DataRow = ds.Tables(0).NewRow
            row = ds.Tables(0).Rows.Find(Codigo)
            Return CType(row.Item(1), String)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Return String.Empty
    End Function
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub
End Class
