Imports Zamba.Core
Imports ZAMBA.Servers
<Ipreprocess.PreProcessName("Actualización Masiva"), Ipreprocess.PreProcessHelp("Utiliza dos consultas, una de Select y otra de Update para actualizar")> _
Public Class ippActualizacionMasiva
    Implements Ipreprocess

#Region "XML"
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:Implementar
        Return String.Empty
    End Function
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml
    End Sub
#End Region
#Region "HELP"
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Utiliza dos consultas, una de Select y otra de Update para actualizar"
    End Function
#End Region
#Region "Eventos"
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage
#End Region

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim i As Int16
        For i = 0 To Files.Count - 1
            processFile(Files(i), param(i))
        Next
        Return Files
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Try
            Dim parametros() As String = param.Split(",")
            Dim sr As New IO.StreamReader(File)
            Dim linea As String

            Dim claves As New Hashtable()
            Dim columnasDevueltas As New Hashtable()
            Dim tabla As String = String.Empty
            Dim id As Int32

            id = parametros(1)

            DataBaseAccessBusiness.UCUpdate.LoadConsultas(claves, columnasDevueltas, tabla, id)

            ' Dim strselect As New CWizard

            '  Dim update As New CUpdate(parametros(1))


            Dim campos() As String
            '   Dim car As Char
            Dim valores As New ArrayList
            Dim where As New ArrayList
            Dim claveselect As New ArrayList
            Dim i As Int16
            Dim ds As DataSet
            Dim sql As String
            Dim cx As Int32
            While sr.Peek <> -1
                linea = sr.ReadLine
                campos = linea.Split("|"c)
                where.Clear()
                claveselect.Add(campos(CInt(parametros(2))))
                'sql = strselect.MakeSelect(parametros(0), claveselect) se cambió por
                sql = DataBaseAccessBusiness.UCWizard.MakeSelect(parametros(0), claveselect)
                ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
                claveselect.Clear()
                For i = 0 To claves.Count - 1
                    'los primeros x parametros son para los valores
                    cx = i + 3
                    where.Add(campos(CInt(parametros(cx))))
                Next
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    valores.Add(ds.Tables(0).Rows(i).Item(0))
                    'Dim str As String = update.UpdateString(valores, where)
                    'se cambió por:
                    Dim str As String = DataBaseAccessBusiness.UCUpdate.UpdateString(valores, where, claves, columnasDevueltas, tabla, id)
                    Server.Con.ExecuteNonQuery(CommandType.Text, str)
                    valores.Clear()
                    where.Clear()
                Next
            End While
            sr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        Return String.Empty
    End Function

End Class
