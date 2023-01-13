Imports Zamba.Core
<Ipreprocess.PreProcessName("Remito"), Ipreprocess.PreProcessHelp("Este es el preproceso de remitos. Separa el número de remito en dos partes")> _
Public Class ippRemito
    Implements Ipreprocess

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Este es el preproceso de remitos. Separa el número de remito en dos partes"
    End Function


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

    Private Shared Sub remito(ByVal file As String, ByVal param As String, ByVal xml As String)
        Dim fi As New System.IO.FileInfo(file)
        Dim index As Integer = 1
        Try
            If fi.Exists Then
                Dim sr As New System.IO.StreamReader(fi.OpenRead)
                Dim Dir As String = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName

                Dim sw As New System.IO.StreamWriter(Dir & "\tmp.txt", False)

                sw.AutoFlush = True



                While sr.Peek <> -1
                    Dim str As String = sr.ReadLine
                    If str <> "" Then
                        Dim strb As New System.Text.StringBuilder
                        Dim strs() As String
                        Dim i As Int32
                        strs = str.Split("|"c)

                        For i = 0 To strs.Length - 1
                            If i <> 2 Then
                                strb.Append(strs(i))
                            Else
                                strb.Append(strs(i))
                                strb.Append("|")
                                strb.Append(strs(i).Substring(0, 3))
                                strb.Append("|")
                                strb.Append(strs(i).Substring(3, strs(i).Length - 3))
                            End If
                            If i <> strs.Length - 1 Then
                                strb.Append("|")
                            End If
                        Next
                        sw.WriteLine(strb.ToString)
                        index += 1
                    End If
                End While
                sr.Close()
                sw.Close()
                Dim fio As New System.IO.FileInfo(Dir & "\tmp.txt")
                fio.CopyTo(file, True)
                fio.Delete()
            End If
        Catch ex As Exception
            Throw (New Exception("En la linea " & index & ". " & ex.ToString, ex))
        End Try
    End Sub

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Try
            remito(File, param, xml)
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
        Return Nothing
    End Function

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
End Class
