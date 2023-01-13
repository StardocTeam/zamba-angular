Imports Zamba.Core
<Ipreprocess.PreProcessName("EliminarVacio"), Ipreprocess.PreProcessHelp("Elimina la palabra VACIO del archivo maestro de polizas. El proceso presupone que el campo que contiene la palabra se encuentra en quinto lugar")> _
Public Class ippEliminarVacio
    Implements Ipreprocess

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return String.Empty
    End Function

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim i As Int16
        For i = 0 To Files.Count - 1
            processFile(Files(i), param(0), xml)
        Next
        Return Files
    End Function

    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub

    Shared Function deleteLetters(ByVal campo As String) As String
        Dim i As Integer = 0
        Dim str As String = ""
        For i = 0 To campo.Length - 1
            If Char.IsNumber(campo.Chars(i)) Then
                str = str & campo.Chars(i)
            Else
                str = String.Empty
            End If
        Next
        Return str
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Dim fi As New System.IO.FileInfo(File)
        If fi.Exists Then
            Dim sr As New System.IO.StreamReader(fi.OpenRead, System.Text.Encoding.Default)
            Dim Dir As String = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName
            Dim sw As New System.IO.StreamWriter(Dir & "\tmp.txt", False, System.Text.Encoding.Default)

            sw.AutoFlush = True

            While sr.Peek <> -1
                Dim str As String = sr.ReadLine
                str = str.Replace("¦", "|").Replace("VACIO;F", "D")
                Dim campos() As String = str.Split("|")

                Dim i As Int16
                Dim strb As New System.Text.StringBuilder

                For i = 0 To campos.Length - 1
                    If i <> 4 Then
                        strb.Append(campos(i))
                    Else
                        strb.Append(deleteLetters(campos(i)))
                    End If
                    If i < campos.Length - 1 Then
                        strb.Append("|")
                    End If
                Next
                sw.WriteLine(strb.ToString)
            End While
            sr.Close()
            sw.Close()
            Dim fio As New System.IO.FileInfo(dir & "\tmp.txt")
            fio.CopyTo(File, True)
            fio.Delete()
        End If
        Return File
    End Function

    Public Shared Function Mails(ByVal Files() As String) As Object
        'TODO:Implementar
        Return Nothing
    End Function
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Elimina la palabra VACIO del archivo maestro de polizas. El proceso presupone que el campo que contiene la palabra se encuentra en quinto lugar"
    End Function

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
End Class
