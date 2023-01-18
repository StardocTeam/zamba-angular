Imports Zamba.Core
<Ipreprocess.PreProcessName("Procesar archivos FaxView"), Ipreprocess.PreProcessHelp("Realiza el preproceso necesario para procesar los archivos de FaxView. Obtiene el numero de cliente del último campo, No recibe parámetros")> _
Public Class ippFaxView
    Implements Ipreprocess

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Realiza el preproceso necesario para procesar los archivos de FaxView. Obtiene el numero de cliente del último campo, No recibe parámetros"
    End Function

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return ""
    End Function

    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim i As Int32 = 0
        Dim result As New ArrayList


        For Each f As String In Files
            RaiseEvent PreprocessMessage("Comenzó el preproceso FaxView para el archivo " & f)
            result.Add(processFile(f, param(i), xml))
            RaiseEvent PreprocessMessage("Preproceso finalizado correctamente")
            i += 1
        Next
        Return result
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Dim fi As New System.IO.FileInfo(File)
        If fi.Exists Then
            Dim sr As New System.IO.StreamReader(fi.OpenRead, System.Text.Encoding.Default)
            Dim Dir As String = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName

            Dim sw As New System.IO.StreamWriter(Dir & "\sw", False, System.Text.Encoding.Default)

            sw.AutoFlush = True
            'Obtiene el numero de cliente del último campo, No recibe parámetros
            While sr.Peek <> -1
                'Dim i As Integer
                Dim str As String = sr.ReadLine
                Dim strb As New System.Text.StringBuilder
                strb.Append(str.Substring(0, str.LastIndexOf("|")))
                strb.Append("|")
                If str.LastIndexOf("|") < str.Length Then
                    strb.Append(Getnumber(str.Substring(str.LastIndexOf("|") + 1)))
                End If

                '  Dim campos() As String = str.Split("|"c)

                sw.WriteLine(strb.ToString.Trim)
            End While
            sr.Close()
            sw.Close()
            Dim fio As New System.IO.FileInfo(dir & "\sw")
            fio.CopyTo(File, True)
            fio.Delete()

        End If

        Return File
    End Function

    Private Shared Function Getnumber(ByVal str As String) As String
        ' Dim flag As Boolean = True
        Dim result As String = String.Empty
        Dim numflag As Boolean = False
        If str = String.Empty Then
            Return ""
        End If
        Try
            Dim number As Integer = CInt(str)
            Return number.ToString
        Catch ex As Exception
        End Try


        Dim i As Integer = 0

        For i = 0 To str.Length - 1
            'Estoy dentro del numero entre paréntesis
            If numflag Then
                'Mientras sea un digito lo pongo al resultado
                If Char.IsDigit(str.Chars(i)) Then
                    result = result & str.Chars(i)
                Else
                    'si cerró el paréntesis ya tengo el número
                    If str.Chars(i) = ")" Then
                        Return result
                    Else
                        numflag = False
                        result = ""
                    End If
                End If
            Else
                If str.Chars(i) = "(" Then
                    numflag = True
                End If
            End If
        Next
        Return result
    End Function
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
End Class
