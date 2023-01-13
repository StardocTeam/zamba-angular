Imports Zamba.Core
<Ipreprocess.PreProcessName("Dos caratulas en un archivo"), Ipreprocess.PreProcessHelp("Se Utiliza para Siniestros con dos caratulas, es decir, datos diferentes que apuntan al mismo archivo. Parametro: el campo donde esta el Nro de Siniestro,Nro de Póliza (base cero)")> _
Public Class ippDosCaratulasUnArchivo
    Implements Ipreprocess

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Se Utiliza para Siniestros con dos caratulas, es decir, datos diferentes que apuntan al mismo archivo. Parametro: el campo donde esta el Nro de Siniestro,Nro de Póliza (base cero)"
    End Function
#Region "XML"
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return ""
    End Function
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub
#End Region
#Region "Eventos"
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage
#End Region
    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim i As Int32
        'param(0) el nro de campo del NRO de Orden

        For i = 0 To Files.Count - 1
            processFile(Files(i), param(0), xml)
        Next
        Return Files
    End Function
    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        ' Dim i As Int32
        Dim parametro() As String = param.Split(",")
        Dim fi As New System.IO.FileInfo(File)
        Dim ds As New DsDosCaratulas
        If fi.Exists Then
            Dim str As String = ""
            Dim campos() As String
            Dim NroPoliza As String = ""
            Dim NroSiniestro As String = ""
            Dim path As String = ""
            Dim sr As New System.IO.StreamReader(fi.OpenRead)
            Dim Dir As String = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName

            Dim sw As New System.IO.StreamWriter(Dir & "\tempreplace.txt", True)
            sw.AutoFlush = True
            While sr.Peek <> -1
                str = sr.ReadLine
                campos = str.Split("|")
                If campos(campos.Length - 1).IndexOf("\") <> -1 Then
                    sw.WriteLine(str)
                    path = campos(campos.Length - 1)
                    NroSiniestro = campos(parametro(0))
                    NroPoliza = campos(parametro(1))
                Else
                    sw.WriteLine(str & path.Trim)
                    Dim row As DsDosCaratulas.dsdosCaratulasRow = ds.dsdosCaratulas.NewdsdosCaratulasRow
                    row.OriginalFileName = path
                    row.NroSiniestro = campos(parametro(0))
                    row.NroPoliza = campos(parametro(1))
                    row.ReemplazarPorSiniestro = NroSiniestro
                    row.ReemplazarPorPoliza = NroPoliza
                    ds.Tables(0).Rows.Add(row)
                End If
            End While
            ds.AcceptChanges()
            ds.WriteXml(".\Caratulas.xml")
            ds.Dispose()
            sr.Close()
            sw.Close()
            Dim fio As New IO.FileInfo(dir & "\tempreplace.txt")
            fio.CopyTo(fi.FullName, True)
            fio.Delete()
        End If
    End Function
End Class
