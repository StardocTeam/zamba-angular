Imports Zamba.Core
<Ipreprocess.PreProcessName("Renombrar Archivos"), Ipreprocess.PreProcessHelp("Renombra los archivos, quitando los caracteres inválidos como ser ñ,Ñ, á,é,´,í,ó,ú, €")> _
Public Class ippRenameFiles
    Implements Ipreprocess
#Region "HELP"
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Renombra los archivos, quitando los caracteres inválidos como ser ñ,Ñ, á,é,´,í,ó,ú, €"
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
        RenameFiles(New IO.DirectoryInfo(param))
        Try
            RenameMasterFile(File)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
        Return String.Empty
    End Function
    Private Sub RenameMasterFile(ByVal File As String)
        Try
            Dim params() As Char = {"´", "`", "æ", "ü", "á", "å", "é", "í", "ó", "ú", "ñ", "Ñ", "Á", "É", "Í", "Ó", "Ú", "à", "è", "ì", "ò", "ù", "º", "ÿ", "û", "ö", "ô", "Ö", "Ü", "Ã", "Ê", "È", "À", "Ì", "Ò", "Ù", "'", "<", ">", "î", "ï", "ë", "ê", "ç", "Ç", Chr(34), "º", "'", "ª", "ä", "õ", "Ñ", "#", "Á", "É", "Í", "Ó", "Ú", "~", "³"}
            Dim linea As String

            Dim fi As New System.IO.FileInfo(File)
            If fi.Exists Then
                Dim sr As New System.IO.StreamReader(fi.OpenRead)
                Dim Dir As String = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName

                Dim sw As New System.IO.StreamWriter(Dir & "\tmp.txt", False)
                sw.AutoFlush = True
                While sr.Peek <> -1
                    linea = sr.ReadLine
                    If linea.Split("|")(10).IndexOfAny(params) <> -1 Then
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ç", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ç", "")))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Ç", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("º", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("´", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("`", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Ã", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Á", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("à", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("á", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("å", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("æ", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ñ", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Ñ", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("€", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("É", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Ê", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("È", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("è", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("é", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ë", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ê", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Í", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ì", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("í", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("î", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ï", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ÿ", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Ó", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Ò", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ò", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ó", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ö", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Ü", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Ú", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("û", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ù", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ü", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ú", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Ù", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("'", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("<", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace(">", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("º", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ª", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("ä", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("'", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace(Chr(34), ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace(",", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("õ", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("#", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("~", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Á", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("É", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Í", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Ó", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Ú", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("³", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("Ö", ""))
                        sw.WriteLine(linea)
                    End If
                End While
                sw.Close()
                sr.Close()
                Try

                    Dim fio As New System.IO.FileInfo(Tools.EnvironmentUtil.GetTempDir("\Temp").FullName & "\tmp.txt")
                    fio.CopyTo(File, True)
                    fio.Delete()
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            RaiseEvent PreprocessError(ex.ToString)
            Trace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        End Try
    End Sub
    Private Sub RenameFiles(ByVal carpeta As IO.DirectoryInfo)
        Try
            Dim params() As Char = {"´", "`", "æ", "ü", "á", "å", "é", "í", "ó", "ú", "ñ", "Ñ", "Á", "É", "Í", "Ó", "Ú", "à", "è", "ì", "ò", "ù", "º", "ÿ", "û", "ö", "ô", "Ö", "Ü", "Ã", "Ê", "È", "À", "Ì", "Ò", "Ù", "'", "<", ">", "î", "ï", "ë", "ê", "ç", "Ç", Chr(34), "º", ",", "'", "ª", "ä", "õ", "Ñ", "#", "Á", "É", "Í", "Ó", "Ú", "~", "³"}
            Dim newname As String

            For Each file As IO.FileInfo In carpeta.GetFiles
                If file.Name.IndexOfAny(params) <> -1 Then
                    newname = file.FullName
                    newname = newname.Replace("ç", "")
                    newname = newname.Replace("Ç", "")
                    newname = newname.Replace("º", "")
                    newname = newname.Replace("´", "")
                    newname = newname.Replace("`", "")
                    newname = newname.Replace("Ã", "")
                    newname = newname.Replace("Á", "")
                    newname = newname.Replace("à", "")
                    newname = newname.Replace("á", "")
                    newname = newname.Replace("å", "")
                    newname = newname.Replace("æ", "")
                    newname = newname.Replace("ñ", "")
                    newname = newname.Replace("Ñ", "")
                    newname = newname.Replace("€", "")
                    newname = newname.Replace("É", "")
                    newname = newname.Replace("Ê", "")
                    newname = newname.Replace("È", "")
                    newname = newname.Replace("è", "")
                    newname = newname.Replace("é", "")
                    newname = newname.Replace("ë", "")
                    newname = newname.Replace("ê", "")
                    newname = newname.Replace("Í", "")
                    newname = newname.Replace("ì", "")
                    newname = newname.Replace("í", "")
                    newname = newname.Replace("î", "")
                    newname = newname.Replace("ï", "")
                    newname = newname.Replace("ÿ", "")
                    newname = newname.Replace("Ó", "")
                    newname = newname.Replace("Ò", "")
                    newname = newname.Replace("ò", "")
                    newname = newname.Replace("ó", "")
                    newname = newname.Replace("ö", "")
                    newname = newname.Replace("Ü", "")
                    newname = newname.Replace("Ú", "")
                    newname = newname.Replace("û", "")
                    newname = newname.Replace("ù", "")
                    newname = newname.Replace("ü", "")
                    newname = newname.Replace("ú", "")
                    newname = newname.Replace("Ù", "")
                    newname = newname.Replace("'", "")
                    newname = newname.Replace("<", "")
                    newname = newname.Replace(">", "")
                    newname = newname.Replace("º", "")
                    newname = newname.Replace("ª", "")
                    newname = newname.Replace("ä", "")
                    newname = newname.Replace("'", "")
                    newname = newname.Replace(",", "")
                    newname = newname.Replace(Chr(34), "")
                    newname = newname.Replace(",", "")
                    newname = newname.Replace("õ", "")
                    newname = newname.Replace("#", "")
                    newname = newname.Replace("~", "")
                    newname = newname.Replace("Á", "")
                    newname = newname.Replace("É", "")
                    newname = newname.Replace("Í", "")
                    newname = newname.Replace("Ó", "")
                    newname = newname.Replace("Ú", "")
                    newname = newname.Replace("³", "")
                    newname = newname.Replace("Ö", "")
                    IO.File.Copy(file.FullName, newname, True)
                    IO.File.Delete(file.FullName)
                End If
            Next
        Catch ex As Exception
            RaiseEvent PreprocessError(ex.ToString)
            Trace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
        End Try
    End Sub

End Class
