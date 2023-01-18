Imports System.IO
Imports Zamba.Data
Imports System.Collections.Generic
Imports Zamba.Servers

Public Class Export
    ''' <summary>
    ''' Insert into zExportControl a new registry
    ''' </summary>
    ''' <param name="userId">User ID</param>
    ''' <param name="codigo">Mail Unique Code</param>
    ''' <param name="DocTypeId">DocType ID</param>
    ''' <history>   Marcelo     Created     26/11/2009</history>
    ''' <remarks></remarks>
    Public Shared Sub InsertIncomingMail(ByVal userId As Int64, ByVal codigo As String, ByVal DocTypeId As Int64)
        ExportFactory.InsertIncomingMail(userId, codigo, DocTypeId)
    End Sub

    ''' <summary>
    ''' Update mail insert state
    ''' </summary>
    ''' <param name="codigo">Mail Unique Code</param>
    ''' <remarks></remarks>
    Public Shared Sub SetMailInserted(ByVal codigo As String)
        ExportFactory.SetMailInserted(codigo)
    End Sub

    Public Shared Sub Insert(ByVal result As IResult)
        Dim Rb As New Results_Business
        Dim ZambaPath As String = Rb.GetLinkFromResult(result)
        Dim ExportId As Int64 = ExportFactory.InsertDocument(result.Name, result.DocType.Name, ZambaPath)

        For Each CurrentIndex As Index In result.Indexs
            ExportFactory.InsertIndex(ExportId, CurrentIndex.Name, CurrentIndex.Type.ToString(), CurrentIndex.Data)
        Next
        Rb = Nothing
    End Sub
    Public Shared Sub Insert(ByVal result As IResult, ByRef t As Transaction)
        Dim RB As New Results_Business
        Dim ZambaPath As String = RB.GetLinkFromResult(result)
        Dim ExportId As Int64 = ExportFactory.InsertDocument(result.Name, result.DocType.Name, ZambaPath, t)

        For Each CurrentIndex As Index In result.Indexs
            ExportFactory.InsertIndex(ExportId, CurrentIndex.Name, CurrentIndex.Type.ToString(), CurrentIndex.Data, t)
        Next
        RB = Nothing
    End Sub
    Public Shared Sub Insert(ByVal docId As Int64, ByVal docTypeId As Int64)
        Dim DTB As New DocTypesBusiness
        Dim RB As New Results_Business
        Dim CurrentDocType As DocType = DTB.GetDocType(DocTypeId)

        Dim CurrentResult As Result = RB.GetNewResult(docId, CurrentDocType)
        Export.Insert(CurrentResult)

        CurrentResult.Dispose()
        RB = Nothing
    End Sub
    'Private Shared Sub Insert(ByVal name As String, ByVal docTypeId As Int64, ByVal serializedFile As Byte(), ByVal fileExtension As String, ByVal status As Int32, ByVal information As String, ByVal indexesValues As Dictionary(Of Int64, String))
    '    ExportFactory.Insert(name, docTypeId, serializedFile, fileExtension, status, information, indexesValues)
    'End Sub

End Class
