Imports Zamba.Core
<Ipreprocess.PreProcessName("Renombrar Archivos"), Ipreprocess.PreProcessHelp("Renombra los archivos, quitando los caracteres inv�lidos como ser �,�, �,�,�,�,�,�, �")> _
Public Class ippRenameFiles
    Implements Ipreprocess
#Region "HELP"
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Renombra los archivos, quitando los caracteres inv�lidos como ser �,�, �,�,�,�,�,�, �"
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
            Dim params() As Char = {"�", "`", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "'", "<", ">", "�", "�", "�", "�", "�", "�", Chr(34), "�", "'", "�", "�", "�", "�", "#", "�", "�", "�", "�", "�", "~", "�"}
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
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", "")))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("`", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("'", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("<", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace(">", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("'", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace(Chr(34), ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace(",", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("#", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("~", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
                        linea = linea.Replace(linea.Split("|")(10), linea.Split("|")(10).Replace("�", ""))
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
            Dim params() As Char = {"�", "`", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "�", "'", "<", ">", "�", "�", "�", "�", "�", "�", Chr(34), "�", ",", "'", "�", "�", "�", "�", "#", "�", "�", "�", "�", "�", "~", "�"}
            Dim newname As String

            For Each file As IO.FileInfo In carpeta.GetFiles
                If file.Name.IndexOfAny(params) <> -1 Then
                    newname = file.FullName
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("`", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("'", "")
                    newname = newname.Replace("<", "")
                    newname = newname.Replace(">", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("'", "")
                    newname = newname.Replace(",", "")
                    newname = newname.Replace(Chr(34), "")
                    newname = newname.Replace(",", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("#", "")
                    newname = newname.Replace("~", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
                    newname = newname.Replace("�", "")
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
