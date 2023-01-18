Imports Zamba.Core
<Ipreprocess.PreProcessName("Validar Mails"), Ipreprocess.PreProcessHelp("Este Preproceso verifica que cada linea del maestro de mails tenga la misma cantidad de campos")> _
Public Class ippMailCheckPipes
    Implements Ipreprocess

#Region "XML"
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return ""
    End Function
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub
#End Region
    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Este Preproceso verifica que cada linea del maestro de mails tenga la misma cantidad de campos"
    End Function
#Region "Eventos"
    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage
#End Region
    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        For Each f As String In Files
            processFile(f)
        Next
        Return Files
    End Function
    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile
        Try
            checkMailsPipes(File)
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            RaiseEvent PreprocessMessage("Error en el preproceso CheckMailPipes " & ex.ToString)
        End Try
        Return String.Empty
    End Function
    Private Shared Sub checkMailsPipes(ByVal file As String)
        Dim sr As New IO.StreamReader(file)
        Dim Dir As String = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName

        Dim sw As New IO.StreamWriter(Dir & "\tempfile")
        While sr.Peek <> -1
            Dim STR As String = sr.ReadLine
            Dim campos As Integer = 14 - STR.Split("|").Length
            If campos > 0 Then
                Dim i As Integer
                For i = 0 To campos - 1
                    STR = STR & "|"
                Next
            End If
            sw.WriteLine(STR)
        End While
        sw.Close()
        sr.Close()
        IO.File.Copy(Dir & "\tempfile", file, True)
    End Sub


End Class
