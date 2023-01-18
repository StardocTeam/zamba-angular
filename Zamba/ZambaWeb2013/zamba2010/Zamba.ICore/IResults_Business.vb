Imports System.Collections.Generic
Imports Zamba.Data

Public Interface IResults_Business
    Property FlagGoOn As Boolean
    Sub AdjuntarAWF(Results As ArrayList, WFID As Long)
    Sub AutocompleteIndexsNewDocument(ByRef newResult As INewResult)
    Sub CloneIndexs(r As IResult, DocType As IDocType)
    Sub CompleteDocument(ByRef _Result As IResult, loadIndexsList As Boolean)
    Sub CompleteDocument(ByRef _Result As IResult, dr As DataRow)
    Sub ConvertToPdfFile(ByRef Result As IResult, pdfFolderPath As String, ByRef CantPdfs As Integer)
    Sub copyFile(ByRef result As INewResult)
    Sub CopyFileToTemp(res As IResult, rootPath As String, destPath As String)
    Sub CopySubDirAndFilesBrowser(copyTo As String, copyFrom As String, originalPath As String)
    Sub Delete(ByRef Result As INewResult, Optional delfile As Boolean = True)
    Sub Delete(ByRef Result As INewResult, delfile As Boolean, ByRef t As ITransaction)
    Sub Delete(ByRef Result As IResult, Optional delfile As Boolean = True, Optional saveAction As Boolean = True)
    Sub DeleteFromZI(lngDocID As Long)
    Sub DeleteFromZWFI(wI As Long)
    Sub DeleteFromZWFII(wI As Long)
    Sub DeleteResultFromWorkflows(docid As Long)
    Sub DeleteSearchIndexData(ResultId As Long)
    Sub DeleteTempFiles()
    Sub DeleteWI(wI As Long)
    Sub Fill(ByRef instance As IBaseImageFileResult)
    Sub Fill(ByRef instance As INewResult)
    Sub Fill(ByRef instance As Ipublishable)
    Sub Fill(ByRef instance As IPublishState)
    Sub Fill(ByRef instance As IResult)
    Sub Fill(ByRef instance As ITaskResult)
    Sub Fill(ByRef instance As IZambaCore)
    Sub Fill(ByRef instance As IZBaseCore)
    Sub Fill(ByRef instance As IZBatch)
    Sub getRelatedsResults(idDocSelected As Long, ByRef relatedResultFinal As IResult)
    Sub HistoricDocumentDropzone(taskID As Long, taskName As String, docTypeId As Long, docTypeName As String, stepId As Long, WorkId As Long, ByVal statename As String, stepname As String)
    Sub InheritUsersToNotify(oldResultId As Long, newResultId As Long)
    Sub InsertDocFile(res As IResult, file() As Byte, fileName As String)
    Sub InsertIntoDOCB(res As IResult)
    Sub InsertSearchIndexData(result As IResult)
    Sub InsertSearchIndexData(result As IResult, ByRef Optional t As ITransaction = Nothing)
    Sub LoadFileFromDB(ByRef res As IResult)
    Sub LoadVolume(ByRef result As INewResult)
    Sub MoveTempFiles(DocTypeId As Long)
    Sub RemoveDocTypeWF(DocTypeID As Integer)
    Sub ReplaceDocument(ByRef Result As IResult, NewDocumentFile As String, ComeFromWF As Boolean, t As ITransaction)
    Sub ResultUpdated(DocTypeId As Long, ResultId As Long)
    Sub SaveModifiedIndexData(ByRef result As IResult, reIndexFlag As Boolean, changeEvent As Boolean, OnlySpecifiedIndexsids As List(Of Long), dtModifiedIndex As DataTable)
    Sub SaveVersionComment(ByRef Result As INewResult)
    Sub SetVersionComment(rID As Long, rComment As String)
    Sub UpdateAutoName(ByRef Result As IResult, Optional ChNomC As Boolean = False)
    Sub UpdateDocFile(ByRef Result As IResult, docfile As String)
    Sub UpdateLastResultVersioned(doctypeId As Long, parentid As Long)
    Sub UpdateOriginalName(DocTypeId As Long, DocId As Long, strOriginalName As String)
    Sub UpdateResultsVersionedDataWhenDelete(DocTypeid As Long, parentid As Long, docid As Long, RootDocumentId As Long)
    Function AllReport(userid As Long) As DataSet
    Function AsociateIncomingResult(_InsertID As Long, _DTID As Long, _DocID As Long, _IDate As Date, _IID() As Long, _IValue() As String) As Boolean
    Function BuildMailWaitingDocument(lngWaitID As Long) As String
    Function CloneResult(originalResult As IResult, filename As String, GenerateIds As Boolean, Optional FlagInsertar As Boolean = False) As INewResult
    Function CopyBlobToVolumeWS(docId As Long, docTypeId As Long) As Boolean
    Function CountChildsVersions(DocTypeid As Long, parentid As Long) As Integer
    Function DeletMigracionObservaciones(EntitiId As Long, AtributeId As Long) As Object
    Function ExistsInOtherWFs(ByRef ResultID As Integer) As Boolean
    Function exportarResultPDF(ByRef Result As IResult, sPdf As String) As Boolean
    Function FillIndexData(EntityId As Long, Id As Long, Indexs As List(Of IIndex), Optional inThread As Boolean = False) As List(Of IIndex)
    Function findIn(Indexs As List(Of IIndex), pIndex As IIndex) As IIndex
    Function GetAllEntities() As DataTable
    Function GetAsociatedToEditTable(idReclamo As Long, idParent As Long, docTypeId As Long, associatedId As Long, idParentColumnName As Long, indexs As Dictionary(Of Long, String), formEntityID As Long) As DataTable
    Function getAssociatedBaremos(idReclamo As Long, idBaremoMuerte As Long) As DataTable
    Function GetBlob(docId As Long, docTypeId As Long, userid As Long) As Byte()
    Function GetCalendarRslt(entityId As String, titleAttribute As String, startAttribute As String, endAttribute As String, filterColumn As String, filterValue As String) As DataTable
    Function GetCountOfBaremoIndex(id As Long, indexName As String) As Boolean
    Function GetCreatorUser(docid As Long) As String
    Function getDataFromHerarchicalParent(parentTagValue As Long) As DataTable
    Function getDataFromHerarchicalParent(parentTagValue As Long, indexs As List(Of String), tableId As String, isView As Boolean) As DataTable
    Function GetDocIDsFromZI(docType As Long) As List(Of Long)
    Function GetDocRelations() As DataSet
    Function GetDocTypesZWFI(ruleID As Long) As List(Of Long)
    Function GetDocumentData(ds As DataSet, dt As IDocType, i As Integer) As DataSet
    Function getEntidadObservaciones() As DataTable
    Function GetExtensionId(File As String) As Integer
    Function GetFileIcon(File As String) As Integer
    Function GetFullName(resultId As Long, docTypeId As Long) As String
    Function GetHtmlLinkFromResult(rDocTypeId As Long, rId As Long) As String
    Function GetIcon(Extension As String) As Integer
    Function GetIdsFromABaremoMuerte(baremoDocId As Long) As DataTable
    Function GetIdsToAsociatedParents(docID As Long, asociatedID As Long, formID As Long) As DataTable
    Function GetIndexByAssociateIndex(DocTypeId As Integer, DocId As Integer) As DataTable
    Function GetIndexForEntities(IndexId As Long) As DataTable
    Function GetIndexIDsFromZWFII(wI As Long) As List(Of Long)
    Function GetIndexObservaciones(indexId As Long, entityId As Long, parentResultId As Long, InputObservacion As String, Evaluation As String) As DataTable
    Function GetIndexValueFromDoc_I(docType As Long, docID As Long, indexID As Long) As String
    Function GetIndexValueFromZWFII(wI As Long, indexID As Long) As String
    Function GetInitialStep(WFID As Short) As Integer
    Function GetInsertIDsWhereDocID(lngDocID As Long) As List(Of Long)
    Function GetLinkFromResult(ByRef Result As IResult) As String
    Function GetName(ResultId As Long, DocTypeId As Long) As String
    Function GetNewNewResult(DocType As IDocType, Optional _UserId As Integer = 0, Optional File As String = "") As INewResult
    Function GetNewNewResult(docTypeId As Long) As INewResult
    Function GetNewNewResult(docId As Long, docType As IDocType) As INewResult
    Function GetNewResult(docId As Long, docType As IDocType) As IResult
    Function GetNewResult(docTypeId As Long, File As String) As INewResult
    Function GetNewTaskResult(DocId As Long, DocType As IDocType) As ITaskResult
    Function GetNewVersionID(RootId As Long, doctype As Integer, OriginalDocId As Long) As Integer
    Function GetObservaciones(entityId As Long, parentResultId As Long, AtributeId As Long) As DataTable
    Function GetPahtWhereDocTypeAndID(lngDocType As Long, lngDocID As Long) As String
    Function GetParentVersionId(DocTypeid As Long, docid As Long) As Long
    Function getPermisosInsert() As DataTable
    Function GetRequestAssociatedResult(requestNumber As Long, tableID As Long, formID As Long, indexs As List(Of Long)) As DataTable
    Function GetRequestNumber(tableId As Long, indexId As Long, docId As Long) As Long
    Function GetResult(docId As Long, docTypeId As Long, FullLoad As Boolean) As IResult
    Function GetResultRow(docId As Long, docTypeId As Long) As DataRow
    Function GetResults(DocTypeId As Integer) As DataTable
    Function getResultsAndPageQueryResults(PageId As Short, PageSize As Short, docTypeId As Integer, indexId As Long, genIndex As List(Of ArrayList), UserId As Integer, Optional comparateValue As String = "", Optional comparateDateValue As String = "", Optional Operation As String = "", Optional searchValue As Boolean = True, Optional SortExpression As String = "", Optional SymbolToReplace As String = "", Optional BySimbolReplace As String = "", ByRef Optional resultCount As Integer = 0) As DataTable
    Function getResultsData(docTypeId As Integer, indexId As Integer, genIndex As List(Of ArrayList), UserId As Integer, Optional comparateValue As String = "", Optional searchValue As Boolean = True) As DataSet
    Function GetRuleIDWhereWI(lngWI As Long) As Long
    Function GetSearchResult(DocType As IDocType, Optional _UserId As Integer = 0, Optional File As String = "") As INewResult
    Function GetSelectDoShowTable() As DataTable
    Function GetTaskURL(currentuserid As Integer, docid As Integer, entityid As Integer, useOriginal As Boolean) As String
    Function GetTempFileFromResult(localResult As IResult, GetPreview As Boolean) As String
    Function GetWatingDocumentMails(lngRuleID As Long) As List(Of String)
    Function GetWebDocFileWS(docTypeId As Long, docId As Long, userId As Long) As Byte()
    Function GetWIFromZIWhereRuleID(lngRuleID As Long) As List(Of Long)
    Function GetWIFromZWFI(ruleID As Long) As List(Of Long)
    Function Insert(ByRef newResult As INewResult, move As Boolean, Optional reIndexFlag As Boolean = False, Optional reemplazarFlag As Boolean = False, Optional showQuestions As Boolean = True, Optional isVirtual As Boolean = False, Optional isReplica As Boolean = False, Optional hasName As Boolean = False, Optional throwEx As Boolean = False, Optional RefreshWFAfterInsert As Boolean = True, Optional Userid As Decimal = 0, Optional newId As Long = 0, Optional ExecuteEntryRules As Boolean = True) As InsertResult
    Function Insert(name As String, binaryDocument() As Byte, fileExtension As String, docTypeId As Long, indexs As DataTable, DontOpenTaskAfterInsertInDoGenerateCoverPage As Boolean) As Long
    Function InsertDocFileWS(docId As Long, docTypeId As Long, fileBytes() As Byte, incomingFile As String, userId As Long) As Boolean
    Function InsertDocumentNew(ByRef newResult As INewResult, move As Boolean, Optional ReindexFlag As Boolean = False, Optional Reemplazar As Boolean = False, Optional showQuestions As Boolean = True, Optional IsVirtual As Boolean = False, Optional IsReplica As Boolean = False, Optional hasName As Boolean = False) As InsertResult
    Function InsertIndexObservaciones(entityId As Long, parentResultId As Long, InputObservacion As String, AtributeId As Long, User As Long) As DataTable
    Function InsertIndexObservaciones(entityId As Long, parentResultId As Long, InputObservacion As String, AtributeId As Long, User As Long, Fecha As String) As DataTable
    Function InsertIndexObservaciones2(entityId As Long, parentResultId As Long, InputObservacion As String, AtributeId As Long, User As Long, Fecha As String) As DataTable
    Function InsertMigracionObservaciones(EntitiId As Long, Fecha As String, UsrId As Long, Value As String, docId As Long, AtributeId As Long) As DataTable
    Function InsertMigracionObservaciones2(EntitiId As Long, Fecha As String, UsrId As Long, Value As String, docId As Long, AtributeId As Long) As DataTable
    Function InsertNew(ByRef newResult As INewResult, move As Boolean, Optional reIndexFlag As Boolean = False, Optional reemplazarFlag As Boolean = False, Optional showQuestions As Boolean = True, Optional isVirtual As Boolean = False, Optional isReplica As Boolean = False, Optional hasName As Boolean = False) As InsertResult
    Function InsertNewVersion(OriginalResult As IResult, Comment As String) As IResult
    Function InsertNewVersionNoComment(OriginalResult As IResult) As IResult
    Function InsertNewVersionNoComment(OriginalResult As IResult, newResultPath As String) As IResult
    Function IsDocTypeInWF(DocTypeid As Integer) As Boolean
    Function IsImage(Ext As Extensiones) As Boolean
    Function IsImage(Ext As String) As Boolean
    Function IsRuleWaitForDocumentReady(ruleID As Long) As Boolean
    Function IsRuleWaiting(lngRuleID As Long, lngInsertID As Long) As Boolean
    Function IsRuleWaitingDocument(ruleId As Long) As Boolean
    Function LoadFileFromDB(docId As Long, dopcTypeId As Long) As Byte()
    Function MigracionObservaciones(Entidad As Long) As DataTable
    Function SearchbyIndexs(indexId As Integer, indexType As Integer, dt As IDocType, IndexData As String) As DataSet
    Function SearchIndex(lngIndexID As Long, enmIndexType As IndexDataType, strComparador As String, strValue As String) As Dictionary(Of Long, Long)
    Function SearchIndex(lngIndexID As Long, enmIndexType As IndexDataType, strComparador As String, strValue As String, lngDocTypeID As Long) As DataSet
    Function SearchIndex(lngIndexID As Long, enmIndexType As IndexDataType, strComparador As String, strValue As String, lngDocTypeID As Long, restriction As String) As DataSet
    Function SearchIndexByUserId(indexId As Long, indexType As IndexDataType, comparador As String, value As String, userId As Long) As Dictionary(Of Long, Long)
    Function SearchIndexByUserIdForWebServices(indexId As Long, indexType As IndexDataType, comparador As String, value As String, userId As Long) As Dictionary(Of Long, Long)
    Function SearchIndexForWebService(lngIndexID As Long, enmIndexType As IndexDataType, strComparador As String, strValue As String, lngDocTypeID As Long, restriction As String) As DataSet
    Function setIndexData(indexId As Long, entityId As Long, parentResultId As Long, indexValue As String) As Boolean
    Function UpdateInsert(ByRef Result As INewResult, move As Boolean, Optional ReindexFlag As Boolean = False, Optional Reemplazar As Boolean = False, Optional showQuestions As Boolean = True, Optional IsVirtual As Boolean = False, Optional addToWF As Boolean = True) As InsertResult
    Function ValidateDescriptionInSustIndex(_newresult As INewResult) As Boolean
    Function ValidateIndexData(_newresult As INewResult) As Boolean
    Function ValidateIndexDatabyRights(_result As IResult) As Boolean
    Function ValidateIndexDataFromDoctype(_newresult As INewResult) As Boolean
    Function ValidateIsDocTypeInZI(docType As Long) As Boolean
    Function ValidateNewResult(DocTypeId As Integer, docid As Integer) As Boolean
    Function ValidateWI(ruleID As Long) As Boolean
End Interface
