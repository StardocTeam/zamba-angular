Public Class Export


    ''' <summary>
    ''' Update mail insert state
    ''' </summary>
    ''' <param name="codigo">Mail Unique Code</param>
    ''' <remarks></remarks>
    'Public Shared Sub SetMailInserted(ByVal codigo As String)
    '    ExportFactory.SetMailInserted(codigo)
    'End Sub

    'Public Shared Sub Insert(ByVal result As IResult)
    '    Dim ZambaPath As String = Results_Business.GetLinkFromResult(result)
    '    Dim ExportId As Int64 = ExportFactory.InsertDocument(result.Name, result.DocType.Name, ZambaPath)

    '    For Each CurrentIndex As Index In result.Indexs
    '        ExportFactory.InsertIndex(ExportId, CurrentIndex.Name, CurrentIndex.Type.ToString(), CurrentIndex.Data)
    '    Next

    'End Sub
    'Public Shared Sub Insert(ByVal result As IResult, ByRef t As Transaction)
    '    Dim ZambaPath As String = Results_Business.GetLinkFromResult(result)
    '    Dim ExportId As Int64 = ExportFactory.InsertDocument(result.Name, result.DocType.Name, ZambaPath, t)

    '    For Each CurrentIndex As Index In result.Indexs
    '        ExportFactory.InsertIndex(ExportId, CurrentIndex.Name, CurrentIndex.Type.ToString(), CurrentIndex.Data, t)
    '    Next

    'End Sub
    'Public Shared Sub Insert(ByVal docId As Int64, ByVal docTypeId As Int64)
    '    Dim CurrentDocType As DocType = DocTypesBusiness.GetDocType(docTypeId, True)

    '    Dim CurrentResult As Result = Results_Business.GetNewResult(docId, CurrentDocType)
    '    Export.Insert(CurrentResult)

    '    CurrentResult.Dispose()
    'End Sub
End Class
