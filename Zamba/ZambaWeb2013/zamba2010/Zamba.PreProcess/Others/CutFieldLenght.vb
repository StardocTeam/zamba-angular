Imports ZAMBA.core
<Ipreprocess.PreProcessName("Recortar longitud de campos"), Ipreprocess.PreProcessHelp("Sin Parametros, recorta la longitud de los campos Para, CC, BCC y ASUNTO quita el @marsh.com y recorta hasta 1000 caracteres")> _
Public Class ippCutFieldLenght
    Inherits ZClass
    Implements Ipreprocess
    Private Shared Function ReduceFieldLenght(ByVal Field As String, ByVal Len As Int32) As String
        Dim str As String
        If Len < Field.Length Then
            str = Field.Substring(0, Len)
        Else
            str = Field
        End If
        Return str
    End Function
    Private Shared Function DeleteDomain(ByVal Field As String) As String
        Dim str As String
        str = Field.Replace("@marsh.com", "")
        Return str
    End Function
    'Private Sub DeleteSubString(Byval Field As String, ByVal StrDel As String)
    '    Field = Field.Replace(StrDel, "")
    'End Sub
    Private Shared Sub VerificarLongitud(ByVal Fi As IO.FileInfo)
        Try
            If Fi.Exists Then
                Dim sr As New IO.StreamReader(Fi.OpenRead, System.Text.Encoding.Default)
                Dim Dir As String = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName

                Dim sw As New IO.StreamWriter(Dir & "\tmp.txt", False, System.Text.Encoding.Default)
                sw.AutoFlush = True
                While sr.Peek <> -1
                    Dim str As String = sr.ReadLine
                    Dim campos() As String = str.Split("|")

                    Dim i As Int16
                    Dim strb As New System.Text.StringBuilder

                    For i = 0 To campos.Length - 1
                        If i = 4 Or i = 5 Or i = 6 Then 'Campos Para, CC, BCC
                            'Primero quito "@marsh.com" y si supera el largo lo corto a la fuerza
                            strb.Append(ReduceFieldLenght(DeleteDomain(campos(i)), 1000))
                        ElseIf i = 7 Then 'Campo Asunto
                            strb.Append(ReduceFieldLenght(campos(i), 1000))
                        Else
                            strb.Append(campos(i))
                        End If
                        If i < campos.Length - 1 Then
                            strb.Append("|")
                        End If
                    Next
                    sw.WriteLine(strb.ToString)
                End While
                sr.Close()
                sw.Close()
                sr = Nothing
                sw = Nothing
                GC.Collect()
                Dim fio As New System.IO.FileInfo(dir & "\tmp.txt")
                fio.CopyTo(Fi.FullName, True)
                fio.Delete()
            End If
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub

    Function process(ByVal Files As ArrayList, Optional ByVal param As ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As ArrayList Implements Ipreprocess.process
        Dim i As Int32
        Dim Array As New ArrayList
        For i = 0 To Files.Count - 1
            Dim File As New IO.FileInfo(Files(i))
            VerificarLongitud(File)
            Array.Add(Files(i))
        Next
        Return Array
    End Function
#Region "XML"
    Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return ""
    End Function

    Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub

#End Region
    Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Dim Fileinfo As New IO.FileInfo(File)
        VerificarLongitud(Fileinfo)
        processFile = File
        Return File
    End Function
#Region "HELP"
    Public Shared Function HELP() As String
        Return "Verifica el tamaño de cada campo del archivo maestro con la base de datos y lo ajusta al mismo"
    End Function
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Sin Parametros, recorta la longitud de los campos Para, CC, BCC y ASUNTO quita el @marsh.com y recorta hasta 1000 caracteres"
    End Function
#End Region
#Region "Eventos"
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
#End Region
    Public Overrides Sub Dispose()

    End Sub
End Class
