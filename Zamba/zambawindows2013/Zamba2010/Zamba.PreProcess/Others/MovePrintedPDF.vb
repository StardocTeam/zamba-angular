Imports Zamba.Core
Imports Zamba.Data

<Ipreprocess.PreProcessName("Mover PDF impreso"), Ipreprocess.PreProcessHelp("Mueve los PDF generados por la exportacion de documentos al volumen correspondiente.")> _
Public Class ippMovePrintedPDF
    Inherits ZClass
    Implements Ipreprocess

    Public Event PreprocessError(ByVal Errormsg As String) Implements Core.Ipreprocess.PreprocessError
    Public Event PreprocessMessage(ByVal msg As String) Implements Core.Ipreprocess.PreprocessMessage

    Public Function GetHelp() As String Implements Ipreprocess.GetHelp
        Return "Mueve los PDF generados por la exportacion de documentos al volumen correspondiente."
    End Function

    Public Overrides Sub Dispose()

    End Sub

    Public Function GetXml(Optional ByVal xml As String = Nothing) As String Implements Ipreprocess.GetXml
        Return ""
    End Function

    Public Function process(ByVal Files As System.Collections.ArrayList, Optional ByVal param As System.Collections.ArrayList = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As System.Collections.ArrayList Implements Core.Ipreprocess.process
        Dim I As Int32
        Dim Result As New ArrayList

        For I = 0 To Files.Count - 1
            Result.Add(processFile(Files(I)))
        Next
        Return Result
    End Function

    Public Function processFile(ByVal File As String, Optional ByVal param As String = Nothing, Optional ByVal xml As String = Nothing, Optional ByVal Test As Boolean = False) As String Implements Core.Ipreprocess.processFile
        Dim FI As New IO.FileInfo(File)
        Dim FileName As String
        Dim NewFileName As String
        Dim Folder As String

        Try
            'obtengo solo el nombre del archivo
            FileName = CleanFileName(FI.Name)

            'carpeta en el volumen a donde hay que mover el archivo
            Folder = ExportFactory.getFolderToMovePDF(FileName)

            NewFileName = Folder & FileName

            If Not String.IsNullOrEmpty(Folder) Then
                IO.File.Move(FI.FullName, NewFileName)
                ExportFactory.updatePDFAsMoved(FileName)
            End If

            Return NewFileName
        Catch ex As Exception
            RaiseEvent PreprocessError(ex.ToString)
        End Try

        Return String.Empty
    End Function

    Private Function CleanFileName(ByVal FileName As String) As String
        If FileName.IndexOf("§") > 0 Then
            FileName = FileName.Split("§")(1)
        End If
        Return FileName
    End Function

    Public Sub SetXml(Optional ByVal xml As String = Nothing) Implements Core.Ipreprocess.SetXml

    End Sub
End Class
