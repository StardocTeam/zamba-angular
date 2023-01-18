Imports Zamba.Core
<Ipreprocess.PreProcessName("Remplazar Comas"), Ipreprocess.PreProcessHelp("Remplaza las comas por |")> _
Public Class ippReplaceComas
    Implements Ipreprocess

#Region "HELP"
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return ""
    End Function
#End Region
#Region "XML"
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:Implemetar
        Return String.Empty
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
        For i = 0 To Files.Count - 1
            processFile(Files(i), param(0), xml)
        Next
        Return Files
    End Function
    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Dim campos() As String
        Dim arch As String = String.Empty
        Dim archivos() As String
        Dim i, j As Int16

        Dim sr As New IO.StreamReader(File)
        Dim sw As New IO.StreamWriter(Application.StartupPath & "\temp.txt")
        Dim srb As New System.Text.StringBuilder
        Dim linea As New System.Text.StringBuilder
        Dim fi As New IO.FileInfo(File)
        While sr.Peek <> -1
            linea.Append(sr.ReadLine)
            campos = linea.ToString.Split("|")
            archivos = campos(10).Split(",")
            linea.Remove(0, linea.Length)
            For j = 0 To 9
                linea.Append(campos(i))
                linea.Append("|") 'proceso modificado por stringbuilder
            Next
            If archivos.Length > 1 Then
                For i = 0 To archivos.Length - 1
                    If archivos(i).IndexOf(".") = -1 Then
                        arch = archivos(i)
                    Else
                        linea.Append(arch & ",")
                    End If
                Next
                linea.Replace(linea.ToString, linea.ToString.Substring(0, linea.Length - 1))
                linea.Append("|")
            End If
            For i = 11 To campos.Length - 1
                linea.Append(campos(i))
                linea.Append("|")
            Next
            linea.Replace(linea.ToString, linea.ToString.Substring(0, linea.Length - 1))
            srb.Append(linea)
            sw.WriteLine(srb.ToString)
        End While
        sr.Close()
        sw.Close()
        Dim fio As New System.IO.FileInfo(Application.StartupPath & "\temp.txt")
        fio.CopyTo(fi.FullName, True)
        fio.Delete()
        Return String.Empty
    End Function
End Class
