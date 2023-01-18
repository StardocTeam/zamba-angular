Imports Zamba.Core
<Ipreprocess.PreProcessName("Sacar ceros de la Póliza"), Ipreprocess.PreProcessHelp("Este preproceso saca los ceros a los campos correspondientes, No se ejecuta el preproceso PPBOX")> _
Public Class ippSacaCerosPoliza
    Implements Ipreprocess

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return String.Empty
    End Function

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim i As Int32
        RaiseEvent PreprocessMessage("Comienzo del Preproceso de Pólizas")

        For i = 0 To Files.Count - 1
            RaiseEvent PreprocessMessage("Procesando " & Files(i))
            processFile(Files(i), param(0), xml)
        Next
        Return Files
    End Function

    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub

    Private Shared Function sacarceros(ByVal str As String) As String
        Dim i As Integer = 0
        While i < str.Length - 1 AndAlso str.Chars(i) = "0"c
            i = i + 1
        End While
        If i = str.Length Then
            Return "0"
        Else
            Return str.Substring(i)
        End If
    End Function

    Private Shared Sub Pppolizas(ByVal files As String)
        Try
            Dim sb As New System.Text.StringBuilder
            sb.Append(files)

            Dim fi As New System.IO.FileInfo(sb.ToString.Trim)
            If fi.Exists Then
                Dim sr As New System.IO.StreamReader(fi.OpenRead)
                Dim Dir As String = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName

                Dim sw As New System.IO.StreamWriter(Dir & "\sw")
                sw.AutoFlush = True

                While sr.Peek <> -1
                    Dim i As Integer
                    Dim str As String = sr.ReadLine
                    Dim strb As New System.Text.StringBuilder
                    Dim campos() As String = str.Split("|"c)
                    For i = 0 To 2
                        strb.Append(campos(i))
                        strb.Append("|")
                    Next

                    For i = 3 To 9
                        strb.Append(sacarceros(campos(i)))
                        strb.Append("|")
                    Next
                    For i = 10 To campos.Length - 1
                        strb.Append(campos(i))
                        If i < campos.Length - 1 Then
                            strb.Append("|")
                        End If
                    Next
                    sw.WriteLine(strb.ToString.Trim)
                End While
                sr.Close()
                sw.Close()
                Dim fio As New System.IO.FileInfo(dir & "\sw")
                fio.CopyTo(sb.ToString.Trim, True)
                fio.Delete()

            End If
        Catch ex As Exception
        End Try
    End Sub


    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Try
            Pppolizas(Trim(File))
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            RaiseEvent PreprocessMessage("Error en el preproceso " & ex.ToString)
        End Try
        Return String.Empty
    End Function

    Public Shared Function Mails(ByVal Files() As String) As Object
        Return String.Empty
    End Function
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Este preproceso saca los ceros a los campos correspondientes, No se ejecuta el preproceso PPBOX"
    End Function

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
End Class
