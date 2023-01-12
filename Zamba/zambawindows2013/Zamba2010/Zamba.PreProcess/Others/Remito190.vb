Imports Zamba.Core
<Ipreprocess.PreProcessName("Remito190"), Ipreprocess.PreProcessHelp("Forma un campo nuevo posicionado al principio con la union del segundo campo con el primero. No recibe parámetros")> _
Public Class ippRemito190
    Implements Ipreprocess

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return String.Empty
    End Function

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        For Each file As String In Files
            processFile(file)
        Next
        Return Files
    End Function

    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub



    Public Function processFile(ByVal Filename As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Dim file As New IO.FileInfo(Filename)
        If file.Exists Then
            Dim stream As New IO.StreamReader(file.OpenRead)
            Dim Dir As String = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName

            Dim Wstream As New IO.StreamWriter(Dir & "\OUTFILE", False)
            While stream.Peek <> -1
                Dim line As String = stream.ReadLine
                Dim strb As New System.Text.StringBuilder
                Dim campos As String() = line.Split("|")
                strb.Append(campos(1) & campos(0))
                strb.Append("|")
                strb.Append(line)
                Wstream.WriteLine(strb.ToString)
            End While
            stream.Close()
            Wstream.Close()
            IO.File.Delete(Filename)
            IO.File.Move(dir & "\OUTFILE", Filename)
            IO.File.Delete(dir & "\OUTFILE")
        End If
        Return Filename
    End Function
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Forma un campo nuevo posisionado al principio con la union del segundo campo con el primero. No recibe parámetros"
    End Function

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
End Class
