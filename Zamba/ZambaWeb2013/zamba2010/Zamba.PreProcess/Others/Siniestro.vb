Imports Zamba.Core
<Ipreprocess.PreProcessName("Siniestro"), Ipreprocess.PreProcessHelp("Elimina los ceros que preceden a los números correspondientes. Este Preproceso supone que los números de caja y lote están en la linea")> _
Public Class ippSiniestro
    Implements Ipreprocess

#Region "Help"
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Elimina los ceros que preceden a los números correspondientes. Este Preproceso supone que los números de caja y lote están en la linea"
    End Function
#End Region
#Region "XML"
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        'TODO:Implementar
        Return String.Empty
    End Function
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub
#End Region
#Region "Eventos"
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
#End Region

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim i As Int32
        For i = 0 To Files.Count - 1
            processFile(Files(i), param(0), xml)
        Next
        Return Files
    End Function
    Shared Sub siniestro(ByVal file As String)
        Try
            Dim i As Integer
            Dim sb As New System.Text.StringBuilder
            sb.Append(file)


            Dim fi As New System.IO.FileInfo(sb.ToString.Trim)
            If fi.Exists Then
                Dim sr As New System.IO.StreamReader(fi.OpenRead)
                Dim sw As New System.IO.StreamWriter(Application.StartupPath & "\sw")
                sw.AutoFlush = True

                While sr.Peek <> -1
                    Dim str As String = sr.ReadLine
                    Dim strb As New System.Text.StringBuilder
                    Dim campos() As String = str.Split("|"c)
                    'Campo de caja y
                    'campo de fecha
                    For i = 0 To 2
                        strb.Append(campos(i))
                        strb.Append("|")
                    Next

                    For i = 3 To 6
                        strb.Append(sacarceros(campos(i)))
                        strb.Append("|")
                    Next
                    For i = 7 To campos.Length - 1
                        strb.Append(campos(i))
                        If i < campos.Length - 1 Then
                            strb.Append("|")
                        End If
                    Next
                    sw.WriteLine(strb.ToString.Trim)
                End While
                sr.Close()
                sw.Close()
                Dim fio As New System.IO.FileInfo(Application.StartupPath & "\sw")
                fio.CopyTo(sb.ToString.Trim, True)
                fio.Delete()
            End If
            sb = Nothing
        Catch
        End Try
    End Sub

    Private Shared Function sacarceros(ByVal str As String) As String
        Dim i As Integer = 0
        While i < str.Length - 1 And str.Chars(i) = "0"c
            i = i + 1
        End While
        If i = str.Length Then
            Return "0"
        Else
            Return str.Substring(i)
        End If
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        'Dim box As PPNumBox
        'box.processFile(File)
        siniestro(File)
        Return String.Empty
    End Function
End Class
