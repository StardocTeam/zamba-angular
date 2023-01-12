Imports Zamba.Core
<Ipreprocess.PreProcessName("Remplazar Separadores"), Ipreprocess.PreProcessHelp("Reemplaza los ; por |")> _
Public Class ippReplaceSeparator
    Implements Ipreprocess

#Region "HELP"
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Reemplaza "";"" por | "
    End Function
#End Region
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
        For i = 0 To Files.Count - 1
            processFile(Files(i), param(0), xml)
        Next
        Return Files
    End Function
    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Try
            '    Dim i As Int32
            Dim fi As New System.IO.FileInfo(File)
            If fi.Exists Then
                Dim sr As New System.IO.StreamReader(fi.OpenRead)
                Dim sw As New System.IO.StreamWriter(Application.StartupPath & "\tempreplace.txt", True)
                sw.AutoFlush = True

                Dim str As String
                While sr.Peek <> -1
                    str = sr.ReadLine
                    Dim oldchar As String = Chr(34) & ";" & Chr(34)
                    str.Replace(oldchar, "|")
                    str.Replace(Chr(34), "")
                    sw.WriteLine(str)
                End While
                sr.Close()
                sw.Close()
                Dim fio As New System.IO.FileInfo(Application.StartupPath & "\sw")
                fio.CopyTo(fi.FullName, True)
                fio.Delete()
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Function


End Class
