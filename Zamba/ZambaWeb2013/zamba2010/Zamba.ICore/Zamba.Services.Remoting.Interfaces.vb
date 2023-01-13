Imports Zamba.Core

Public Interface IAutocompleteBC
    Function Complete(ByVal Result1 As IResult, ByVal indexs As ArrayList) As Hashtable
    Sub Dispose()
End Interface

Public Interface IAutoCompleteBarcode_Factory
    Function GetComplete(ByVal DocTypeID As Int32, ByVal IndexId As Int32) As IZClass
End Interface

Public Interface IAutoCompleteBarcode_FactoryBusiness
    Function getIndexKeys(ByVal id As Int32) As ArrayList
End Interface

Public Interface IAutocompleteBCBusiness
    Function ExecuteAutoComplete(ByRef docresult As IResult, ByRef ind As IIndex, ByRef frmGrilla As Form) As Hashtable
End Interface

Public Interface IAutoSubstitutionBusiness
    Sub AddItems(ByVal items As List(Of ISustitutionItem), ByVal indexID As Int32)
    Sub RemoveItems(ByVal codeList As List(Of Integer), ByVal indexId As Integer)
    Sub UpdateAddItem(ByVal codigo As String, ByVal descripcion As String, ByVal LastCode As String, ByVal IndexId As String)
    Function GetIndexData(ByVal Indexid As Int64, ByVal Reload As Boolean) As DataTable
    Function getDescription(ByVal Code As String, ByVal IndexId As Int64, ByVal inThread As Boolean) As String
    Function getDescription(ByVal Code As String, ByVal IndexId As Int64) As String
End Interface

Public Interface ICreateTablesBusiness
    Sub InsertIntoSustitucion(ByVal Tabla As String, ByVal Codigo As Integer, ByVal Descripcion As String)
    Sub BorrarSustitucionTable(ByVal IndexId As Integer)
    Sub DropSustitucionTable(ByVal IndexId As Integer)
    Sub BulkInsertSustitucionTable(ByVal FileName As String, ByVal separador As String, ByVal IndexId As Integer)
    Sub ExportSustitucionTable(ByVal file As String, ByVal separador As String, ByVal IndexId As Integer)
    Sub Dispose(ByRef _instance As Object)
End Interface



Public Interface ICoreBusiness
    Function GetNewID(ByVal IdType As IdTypes) As Integer
End Interface

Public Interface IDocTypesBusiness
    Function GetDocTypesbyUserRights(ByVal UserId As Long, ByVal RightType As RightsType) As ArrayList
    Function GetIndexsProperties(ByVal doctypeid As Int64, ByVal withcache As Boolean) As DataSet
    Sub GetEditRights(ByVal DocType As IDocType)
End Interface

Public Interface IIndexsBusiness
    Property LoadedControlsTable() As Hashtable
    Function GetIndexs(ByVal docId As Int64, ByVal DocTypeId As Int64) As List(Of IIndex)
    Function GetDropDownList(ByVal IndexId As Int32) As ArrayList
End Interface

Public Interface IIndexsRightsInfo
    Function GetIndexRightValue(ByVal RightType As RightsType) As Boolean
End Interface

'Public Interface IResults_Business
'    'InsertResult InsertDocument(ref INewResult newResult, bool move, bool ReindexFlag, bool Reemplazar, bool showQuestions, bool IsVirtual, bool IsReplica, bool hasName)
'    Function InsertDocument(ByRef newResult As INewResult, ByVal move As Boolean, ByVal ReindexFlag As Boolean, ByVal Reemplazar As Boolean, ByVal showQuestions As Boolean, ByVal IsVirtual As Boolean, ByVal IsReplica As Boolean, ByVal hasName As Boolean) As InsertResult
'    'INewResult CreateNewResult()
'    Function CreateNewResult() As INewResult
'    'INewResult GetNewNewResult(long docTypeId)
'    Function GetNewNewResult(ByVal docTypeId As Long) As INewResult
'    'InsertResult  Insert(ref INewResult newResult, bool move, bool reIndexFlag, bool reemplazarFlag, bool showQuestions, bool isVirtual, bool isReplica, bool hasName)
'    Function Insert(ByRef newResult As INewResult, ByVal move As Boolean, ByVal ReindexFlag As Boolean, ByVal Reemplazar As Boolean, ByVal showQuestions As Boolean, ByVal IsVirtual As Boolean, ByVal IsReplica As Boolean, ByVal hasName As Boolean) As InsertResult
'    'void UpdateOriginalName(long DocTypeId, long DocId, string strOriginalName)
'    Sub UpdateOriginalName(ByVal DocTypeId As Long, ByVal DocId As Long, ByVal strOriginalName As String)
'    'INewResult CloneResult(IResult originalResult, string filename, bool GenerateIds, bool FlagInsertar)
'    Function CloneResult(ByVal originalResult As IResult, ByVal filename As String, ByVal GenerateIds As Boolean, ByVal FlagInsertar As Boolean) As INewResult
'    Function findIn(ByVal Indexs As ArrayList, ByVal pIndex As IIndex) As IIndex
'    Function ValidateIndexDatabyRights(ByVal _result As IResult) As Boolean
'    Sub SaveModifiedIndexData(ByVal result As IResult, ByVal reIndexFlag As Boolean, ByVal changeEvent As Boolean, ByVal OnlySpecifiedIndexsids As List(Of Int64))
'    Sub UpdateAutoName(ByVal Result As IResult)


'End Interface

Public Interface IUserBusiness

    'IUser GetUserByname(string name)
    Function GetUserByname(ByVal name As String) As IUser
    'string GetFilesPath(long id)
    Function GetFilesPath(ByVal id As Long) As String
    'void SetCurrentUser( IUser _user)
    Sub SetCurrentUser(ByVal _user As IUser)
    'IUser CurrentUser()
    Function CurrentUser() As IUser


End Interface

Public Interface IRights
    Function GetIndexsRights(ByVal DocTypeId As Int64, ByVal GID As Int64, ByVal addOwnerGroup As Boolean, ByVal WithCache As Boolean) As Hashtable
    Function GetUserRights(ByVal User As IUser, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Integer = -1) As Boolean
    Function GetUserRights(ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Int64 = -1) As Boolean
    Sub SaveAction(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As RightsType, ByVal S_Object_ID As String)
    Function DisableOwnerChanges(ByVal user As IUser, ByVal doctype As Int64) As Boolean
End Interface

Public Interface IUserPreferences

    'void setValue( string name , string valor , Zamba.Sections Section )
    Sub setValue(ByVal name As String, ByVal valor As String, ByVal Section As UPSections)
    'string getValue(string name, Zamba.Sections Section, object DefaultValue)
    Function getValue(ByVal name As String, ByVal Section As UPSections, ByVal DefaultValue As Object) As String

End Interface

Public Interface IZCore
    Function FilterIndex(ByVal DocTypeId As Int32) As ArrayList
End Interface


'Public Interface IZTrace

'    'void SetLevel(int level, string zModuleName)
'    Sub SetLevel(ByVal level As Integer, ByVal zModuleName As String)

'End Interface

