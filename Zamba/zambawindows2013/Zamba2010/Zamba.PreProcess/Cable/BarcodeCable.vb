Imports Zamba.Core
<Ipreprocess.PreProcessName("Código De Barra"), Ipreprocess.PreProcessHelp("Preproceso para la importación de -TIPOS DE PARTES- por Cablevision. Sin parametros./n Si hay comillas dobles las quita")> _
Public Class ippBarcodeCable
    Implements Ipreprocess

#Region "Metodos Privados"
    Private Shared Function TipoParte(ByRef Str As String) As String
        Return Str.Substring(0, 2)
    End Function
    Private Shared Function NroParte(ByRef Str As String) As String
        Return Str.Substring(2, 6)
    End Function
    Private Shared Function FechaPedido(ByRef Str As String) As String
        Dim fecha, dia, mes, anio As String

        dia = Str.Substring(8, 2)
        mes = Str.Substring(10, 2)
        anio = Str.Substring(12, 2)
        fecha = Nothing
        fecha = dia & "/" & mes & "/" & anio
        Return fecha
    End Function
#End Region
#Region "Metodos Publicos"
    Public Shared Sub Run(ByVal Fi As IO.FileInfo)
        Dim sr As New IO.StreamReader(Fi.OpenRead, System.Text.Encoding.Default)
        Dim sw As IO.StreamWriter = New IO.StreamWriter(Application.StartupPath & "\tmp2.txt")
        Dim str As String
        Dim campos() As String


        While sr.Peek <> -1
            Dim strb As New System.Text.StringBuilder
            str = sr.ReadLine()
            campos = str.Split(",")
            strb.Append(campos(0))
            strb.Append(",")
            strb.Append(TipoParte(campos(1)))
            strb.Append(",")
            strb.Append(NroParte(campos(1)))
            strb.Append(",")
            strb.Append(FechaPedido(campos(1)))
            strb.Append(",")
            strb.Append(campos(1))
            strb.Append(",")
            strb.Append(campos(2))
            strb.Append(",")
            strb.Append(campos(3))
            strb.Append(",")
            strb.Append(campos(4))
            strb.Append(",")
            strb.Append(campos(5))
            strb.Append(",")
            strb.Append(campos(6))
            sw.WriteLine(strb.ToString)
            strb = Nothing
        End While
        sr.Close()
        sw.Close()
        sr = Nothing
        sw = Nothing
        GC.Collect()
        Dim fio As New System.IO.FileInfo(Application.StartupPath & "\tmp2.txt")
        fio.CopyTo(Fi.FullName, True)
        fio.Delete()
    End Sub
#End Region

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Preproceso para la importación de -TIPOS DE PARTES- por Cablevision. Sin parametros. " & Chr(13) & "Si hay comillas dobles las quita"
    End Function

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return ""
    End Function

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage
    Public Shared Sub QuitarComillas(ByVal Fi As IO.FileInfo)
        Dim sr As New IO.StreamReader(Fi.OpenRead, System.Text.Encoding.Default)
        Dim sw As New IO.StreamWriter(Application.StartupPath & "\tmp3.txt", False, System.Text.Encoding.Default)
        Dim str As String
        sw.AutoFlush = True
        If Fi.Exists Then
            Try
                While sr.Peek <> -1
                    str = sr.ReadLine
                    str = str.Replace(Chr(34), "")
                    sw.WriteLine(str)
                End While
                sr.Close()
                sw.Close()
                sr = Nothing
                sw = Nothing
                GC.Collect()
            Catch
            End Try
        End If
        Dim fio As New System.IO.FileInfo(Application.StartupPath & "\tmp3.txt")
        fio.CopyTo(Fi.FullName, True)
        fio.Delete()
    End Sub

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim I As Int32
        Dim Result As New ArrayList

        For I = 0 To Files.Count - 1
            Result.Add(processFile(Files(I)))
        Next
        Return Result
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Dim FI As New IO.FileInfo(File)
        Try
            QuitarComillas(FI)
            Run(FI)

            Return FI.FullName
        Catch ex As Exception
            RaiseEvent PreprocessError(ex.ToString)
        End Try
        Return String.Empty
    End Function

    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub
End Class
