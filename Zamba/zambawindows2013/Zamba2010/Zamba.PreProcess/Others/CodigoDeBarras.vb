Imports Zamba.Core

<Ipreprocess.PreProcessName("Código De Barras"), Ipreprocess.PreProcessHelp("Procesa los códigos de barra")> _
Public Class ippCodigoDeBarras
    Implements Ipreprocess

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return ""
    End Function

    Public Event PreprocessError(ByVal Errormsg As String) Implements Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Ipreprocess.PreprocessMessage

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Ipreprocess.process
        Dim i As Int32
        For i = 0 To Files.Count - 1
            processFile(Files(i), param(0), xml)
        Next
        Return Files
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Ipreprocess.processFile

    End Function

#Region "XML"
    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml

    End Function
    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Ipreprocess.SetXml

    End Sub

#End Region

End Class
