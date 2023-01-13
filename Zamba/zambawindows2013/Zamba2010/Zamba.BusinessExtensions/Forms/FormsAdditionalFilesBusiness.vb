Imports Zamba.Data

Public Class FormsAdditionalFilesBusiness
    ''' <summary>
    ''' Funcion encargada de retornar el dataset que luego se va a visualizar en la grilla
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAditionalFiles() As DataTable

        Dim dtToReturn As DataTable
        Try
            Dim Factory As FormsAdditionalFilesFactory = New FormsAdditionalFilesFactory()
            Dim dt As DataTable = Factory.GetAditionalFiles()
            If Not dt Is Nothing Then
                dtToReturn = dt
                dt.Dispose()
                dt = Nothing
            End If
            Factory = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return dtToReturn
    End Function

    ''' <summary>
    ''' Agregar los archivos a la base insertandolos en blob
    ''' </summary>
    ''' <param name="pathFileOriginals"></param>
    ''' <param name="path"></param>
    ''' <remarks></remarks>
    Sub AddFiles(ByVal pathFileOriginals() As String, ByVal path As String)
        Try
            Dim Factory As FormsAdditionalFilesFactory = New FormsAdditionalFilesFactory()
            For Each pathFile As String In pathFileOriginals
                Dim fileB As Byte() = FileEncode.Encode(pathFile)
                Dim fileName As String = System.IO.Path.GetFileName(pathFile)
                Factory.AddAditionalFile(fileName, path, fileB)
            Next
            Factory = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Proceso encargado de eliminar un archivo de la base.
    ''' </summary>
    ''' <param name="file"></param>
    ''' <param name="path"></param>
    ''' <remarks></remarks>
    Sub RemoveFile(ByVal file As String, ByVal path As String)
        Try
            Dim Factory As FormsAdditionalFilesFactory = New FormsAdditionalFilesFactory()
            Factory.RemoveAditionalFile(file, path)
            Factory = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Funcion que se encarga de obtener todos los archivos adicionales con su blob correspondiente
    ''' Este lo retorna como una lista de documentos blob
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAllAdditionalFiles() As List(Of BlobDocument)

        Dim lstBlobDocumnent As List(Of BlobDocument) = New List(Of BlobDocument)
        Dim Factory As FormsAdditionalFilesFactory = New FormsAdditionalFilesFactory()
        Dim dt As DataTable = Factory.GetAditionalFilesAndBlobDocuments()
        For Each row As DataRow In dt.Rows
            Dim blobDocument As New BlobDocument
            blobDocument.BlobFile = row("blobFile")
            blobDocument.Description = row("Path")
            blobDocument.Name = row("name")
            blobDocument.UpdateDate = row("updateDate")
            lstBlobDocumnent.Add(blobDocument)
            blobDocument = Nothing
        Next
        Return lstBlobDocumnent
    End Function

End Class
