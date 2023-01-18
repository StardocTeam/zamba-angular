Imports Zamba.Core.WF.WF
Imports Zamba.Servers
Imports Zamba.Data
Imports System.Collections.Generic
Imports System.Text
Imports System.IO

Imports Zamba.Office
Imports Zamba.Core.Enumerators
Imports Zamba.Framework.Tools

Public Class Results_Business

    Private Shared _fileStream As FileStream = Nothing
    Private Shared _binaryWriter As BinaryWriter = Nothing
    Private Shared dsRelateds As DataSet = Nothing
    Private Shared dsRelations As DataSet = Nothing
    Private Shared _unicodeCategory As String
    ' Evento que se ejecuta después de agregar el result (que se inserto con insertar documento) a las etapas iniciales de los correspondientes 
    ' workflow (sólo si el Entidad está asociado a uno o más workflows)
    Public Shared Event updateWFs()


    Public Shared Function MakeTable(ByVal DocTypeId As Integer, ByVal TableType As Results_Factory.TableType) As String
        Dim tables As String
        tables = Results_Factory.MakeTable(DocTypeId, TableType)
        Return tables
    End Function

#Region "Extensiones"
    'Funciones para manejar archivos de distintas extensiones
    Public Shared Function GetFileIcon(ByVal File As String) As Int32
        If File Is Nothing Then
            Return 30
        Else
            Try
                'Seteo icono al insertar documentos outlook a traves de la Zopt
                If File.Trim.ToLower.EndsWith(".msg") Then
                    Dim MsgIconValue As String = ZOptBusiness.GetValue("UseOutlookIcon")

                    If MsgIconValue Is Nothing Then
                        ZOptBusiness.Insert("UseOutlookIcon", "True")
                    Else
                        If String.Compare(MsgIconValue.Trim.ToLower, "false") = 0 Then
                            File = File.Replace(".msg", ".HTML")
                            Return GetIcon(New FileInfo(File.ToUpper).Extension)
                        End If
                    End If
                End If

                Return GetIcon(New FileInfo(File.ToUpper).Extension)
            Catch ex As Exception
                Return 30
            End Try
        End If

    End Function
    Public Shared Function GetIcon(ByVal Extension As String) As Int32
        Try
            Select Case Extension.ToUpper
                Case ".TIF"
                    Return 1
                Case ".MAG"
                    Return 6
                Case ".ASP"
                    Return 9
                Case ".AVI"
                    Return 15
                Case ".BMP"
                    Return 1
                Case ".CSV"
                    Return 8
                Case ".DOC"
                    Return 2
                Case ".DOCX"
                    Return 2
                Case ".GIF"
                    Return 1
                Case ".HTML"
                    Return 6
                Case ".HTM"
                    Return 6
                Case ".EML"
                    Return 6
                Case ".IMG"
                    Return 1
                Case ".JPG"
                    Return 1
                Case ".JPEG"
                    Return 1
                Case ".MDB"
                    Return 10
                Case ".MID"
                    Return 14
                Case ".MP3"
                    Return 14
                Case ".PDF"
                    Return 4
                Case ".PPS"
                    Return 5
                Case ".PPSX"
                    Return 5
                Case ".PPT"
                    Return 5
                Case ".PPTX"
                    Return 5
                Case ".POT"
                    Return 5
                Case ".POTX"
                    Return 5
                Case ".RTF"
                    Return 2
                Case ".TXT"
                    Return 7
                Case ".PRN"
                    Return 7
                Case ".DOT"
                    Return 2
                Case ".DOTX"
                    Return 2
                Case ".MSG"
                    Return 39
                Case ".XLS"
                    Return 3
                Case ".XLSX"
                    Return 3
                Case ".XLT"
                    Return 3
                Case ".XLTX"
                    Return 3
                Case ".ZIP"
                    Return 16
                Case ".RAR"
                    Return 16
                Case ".LOG"
                    Return 6
                Case ".INI"
                    Return 6
                Case Else
                    Return 17
            End Select
        Catch ex As Exception
            Return 17
        End Try
    End Function
    Public Enum Extensiones As Integer
        HTM = 6
        HTML = 6
        DOC = 2
        DOT = 2
        XLS = 3
        PDF = 4
        TXT = 7
        PRN = 7
        PPT = 5
        PPS = 5
        MP3 = 14
        MDB = 10
        AVI = 15
        MID = 14
        ASP = 9
        RTF = 2
        GIF = 1
        TIF = 1
        JPG = 1
        JPEG = 1
        IMG = 1
        BMP = 1
        CSV = 8
        ZIP = 16
        RAR = 16
    End Enum
    Public Shared Function IsImage(ByVal Ext As Results_Business.Extensiones) As Boolean
        If Ext = Results_Business.Extensiones.TIF OrElse Ext = Results_Business.Extensiones.GIF OrElse Ext = Results_Business.Extensiones.JPG OrElse Ext = Results_Business.Extensiones.JPEG OrElse Ext = Results_Business.Extensiones.BMP OrElse Ext = Results_Business.Extensiones.IMG Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function IsImage(ByVal Ext As String) As Boolean
        Select Case Ext.ToUpper
            Case ".TIF", ".TIFF", ".GIF", ".JPG", ".JPEG", ".IMG", ".BMP"
                Return True
            Case "TIF", "TIFF", "GIF", "JPG", "JPEG", "IMG", "BMP"
                Return True
        End Select
        Return False
    End Function
    Public Shared Function GetExtensionId(ByVal File As String) As Int32
        Dim Fi As New FileInfo(File)
        Dim Ext As String = Fi.Extension
        Return GetIcon(Ext)
    End Function
#End Region

#Region "GetResults"
    Public Shared Function GetDocuments(ByVal DocTypeId As Integer) As DsResults
        Dim Results As New DsResults
        Results = Results_Factory.GetDocuments(DocTypeId)
        Return Results
    End Function

    ''' <summary>
    ''' Creo el result a partir de la fila de la grilla
    ''' </summary>
    ''' <param name="_Result"></param>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Public Shared Sub CompleteDocument(ByRef _Result As Result, ByVal dr As DataRow)
        Try
            _Result.Disk_Group_Id = CInt(dr("DISK_GROUP_ID"))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.Platter_Id = CInt(dr("PLATTER_ID"))
        Catch ex As Exception
        End Try
        Try
            _Result.Doc_File = dr("Doc_File").ToString()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.OffSet = CInt(dr("OFFSET"))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If CInt(dr("SHARED")) = 1 Then
                _Result.isShared = True
            Else
                _Result.isShared = False
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.ParentVerId = CInt(dr("ver_Parent_id"))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.HasVersion = CInt(dr("Version"))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.RootDocumentId = CInt(dr("RootId"))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.OriginalName = dr("Nombre Original").ToString()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.VersionNumber = CInt(dr("Numero de Version"))
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Try
            _Result.Disk_Group_Id = CInt(dr("DISK_GROUP_ID"))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.DISK_VOL_PATH = dr("Disk_vol_path").ToString()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            _Result.UserID = _Result.Platter_Id
            _Result.OwnerID = _Result.Platter_Id
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        If Not IsDBNull(dr("Fecha Creacion")) Then
            If Not IsNothing(dr("Fecha Creacion")) Then
                If Not String.IsNullOrEmpty(dr("Fecha Creacion").ToString()) Then
                    Try
                        If Not IsNothing(_Result) Then
                            _Result.CreateDate = DateTime.Parse(dr("Fecha Creacion").ToString())
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End If
            End If
        End If

        For Each indice As IIndex In _Result.DocType.Indexs
            If indice.DropDown = IndexAdditionalType.AutoSustitución _
                Or indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                If Not IsDBNull(dr("I" & indice.ID)) Then
                    indice.Data = dr("I" & indice.ID).ToString()
                    indice.dataDescription = dr(indice.Name).ToString()
                End If
            Else
                indice.Data = dr(indice.Name).ToString()
            End If
        Next
        _Result.Indexs = _Result.DocType.Indexs
    End Sub

    Public Shared Function SearchIndexByUserId(ByVal indexId As Int64, ByVal indexType As IndexDataType, ByVal comparador As String, ByVal value As String, ByVal userId As Int64) As Dictionary(Of Int64, Int64)

        Dim TmpDocsDictionary As New Dictionary(Of Int64, Int64)()
        Dim TmpDocTypeIDs As Generic.List(Of Int64) = DocTypesBusiness.GetDocTypeIdByIndexId(indexId, userId)
        Dim TmpDS As DataSet
        Dim Restriction As String
        For Each Tmpdoctypeid As Int64 In TmpDocTypeIDs
            TmpDS = New DataSet()

            Restriction = RestrictionsMapper_Factory.GetRestrictionWebStrings(userId, Tmpdoctypeid)
            Try
                TmpDS = SearchIndex(indexId, indexType, comparador, value, Tmpdoctypeid, Restriction)
                If Not IsNothing(TmpDS) AndAlso Not IsNothing(TmpDS.Tables(0)) AndAlso TmpDS.Tables(0).Rows.Count > 0 Then
                    For Each r As DataRow In TmpDS.Tables(0).Rows
                        TmpDocsDictionary.Add(Convert.ToInt64(r("DOC_ID")), Tmpdoctypeid)
                    Next
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        Next
        Return TmpDocsDictionary
    End Function
    Public Shared Function SearchIndexByUserIdForWebServices(ByVal indexId As Int64, ByVal indexType As IndexDataType, ByVal comparador As String, ByVal value As String, ByVal userId As Int64) As Dictionary(Of Int64, Int64)

        Dim TmpDocsDictionary As New Dictionary(Of Int64, Int64)()
        Dim TmpDocTypeIDs As Generic.List(Of Int64) = DocTypesBusiness.GetDocTypeIdByIndexId(indexId, userId)
        Dim TmpDS As DataSet
        Dim Restriction As String
        For Each Tmpdoctypeid As Int64 In TmpDocTypeIDs
            TmpDS = New DataSet()

            Restriction = RestrictionsMapper_Factory.GetRestrictionWebStrings(userId, Tmpdoctypeid)
            Try
                TmpDS = SearchIndexForWebService(indexId, indexType, comparador, value, Tmpdoctypeid, Restriction)
                If Not IsNothing(TmpDS) AndAlso Not IsNothing(TmpDS.Tables(0)) AndAlso TmpDS.Tables(0).Rows.Count > 0 Then
                    For Each r As DataRow In TmpDS.Tables(0).Rows
                        TmpDocsDictionary.Add(Convert.ToInt64(r("DOC_ID")), Tmpdoctypeid)
                    Next
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        Next
        Return TmpDocsDictionary
    End Function

    Public Shared Function SearchIndex(ByVal lngIndexID As Int64, ByVal enmIndexType As IndexDataType, ByVal strComparador As String, ByVal strValue As String) As Dictionary(Of Int64, Int64)
        Dim tmpDocsDictionary As New Dictionary(Of Int64, Int64)()
        Dim tmpDocTypeIDs As Generic.List(Of Int64) = DocTypesBusiness.GetDocTypeIdByIndexId(lngIndexID)
        Dim tmpDS As DataSet

        For Each tmpdoctypeid As Int64 In tmpDocTypeIDs
            tmpDS = New DataSet()
            Try
                tmpDS = SearchIndex(lngIndexID, enmIndexType, strComparador, strValue, tmpdoctypeid)
                If Not IsNothing(tmpDS) AndAlso Not IsNothing(tmpDS.Tables(0)) AndAlso tmpDS.Tables(0).Rows.Count > 0 Then
                    For Each r As DataRow In tmpDS.Tables(0).Rows
                        tmpDocsDictionary.Add(Convert.ToInt64(r("DOC_ID")), tmpdoctypeid)
                    Next
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        Next
        Return tmpDocsDictionary
    End Function

    Public Shared Function SearchIndex(ByVal lngIndexID As Int64, ByVal enmIndexType As IndexDataType, ByVal strComparador As String, ByVal strValue As String, ByVal lngDocTypeID As Int64) As DataSet
        Return Results_Factory.SearchIndex(lngIndexID, enmIndexType, strComparador, strValue, lngDocTypeID)
    End Function

    Public Shared Function SearchIndex(ByVal lngIndexID As Int64, ByVal enmIndexType As IndexDataType, ByVal strComparador As String, ByVal strValue As String, ByVal lngDocTypeID As Int64, ByVal restriction As String) As DataSet
        Return Results_Factory.SearchIndex(lngIndexID, enmIndexType, strComparador, strValue, lngDocTypeID, restriction)
    End Function

    Public Shared Function SearchIndexForWebService(ByVal lngIndexID As Int64, ByVal enmIndexType As IndexDataType, ByVal strComparador As String, ByVal strValue As String, ByVal lngDocTypeID As Int64, ByVal restriction As String) As DataSet
        Return Results_Factory.SearchIndexForWebServices(lngIndexID, enmIndexType, strComparador, strValue, lngDocTypeID, restriction)
    End Function

    Public Shared Function SearchbyIndexs(ByVal indexId As Int32, ByVal indexType As Int32, ByVal dt As DocType, ByVal IndexData As String) As DataSet
        Return Results_Factory.SearchbyIndexs(indexId, indexType, dt, IndexData)
    End Function

    Public Shared Function GetDocumentData(ByVal ds As DataSet, ByVal dt As DocType, ByVal i As Int32) As DataSet
        Return Results_Factory.GetDocumentData(ds, dt, i)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Dataset con los datos de un result a travez de una llamada a la vista
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	28/09/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function getResultsData(ByVal docTypeId As Int64, ByVal indexId As Int64, ByVal genIndex As List(Of ArrayList), ByVal UserId As Int32, Optional ByVal comparateValue As String = "", Optional ByVal searchValue As Boolean = True) As DataSet
        'Traigo las restricciones y armo un string con ellas
        Dim restricc As String = RestrictionsMapper_Factory.GetRestrictionWebStrings(UserId, docTypeId)
        Return Results_Factory.getResultsData(docTypeId, indexId, genIndex, comparateValue, searchValue, restricc)
    End Function


#Region "WebView"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Dataset con los results y Pagina el resultado de la consulta segun criterios 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	19/02/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function getResultsAndPageQueryResults(ByVal PageId As Int16, ByVal PageSize As Int16, ByVal docTypeId As Int64, ByVal indexId As Int64, ByVal genIndex As List(Of ArrayList), ByVal UserId As Int32, Optional ByVal comparateValue As String = "", Optional ByVal comparateDateValue As String = "", Optional ByVal Operation As String = "", Optional ByVal searchValue As Boolean = True, Optional ByVal SortExpression As String = "", Optional ByVal SymbolToReplace As String = "", Optional ByVal BySimbolReplace As String = "", Optional ByRef resultCount As Integer = 0) As DataTable

        'Traigo las restricciones y armo un string con ellas
        Dim restricc As String = RestrictionsMapper_Factory.GetRestrictionWebStrings(UserId, docTypeId)

        Dim strSql As New System.Text.StringBuilder()
        'todo: Agregar la restriccion si existe a la que se esta por crear
        If searchValue = True Then
            If indexId > 0 Then
                If Indexs_Factory.GetIndexDropDownType(Int64.Parse(indexId.ToString)) = IndexAdditionalType.AutoSustitución OrElse Indexs_Factory.GetIndexDropDownType(Int64.Parse(indexId.ToString)) = IndexAdditionalType.AutoSustituciónJerarquico Then
                    strSql.Append(" WHERE SLST_S")
                    strSql.Append(indexId.ToString())
                    strSql.Append(".DESCRIPCION")
                Else
                    strSql.Append(" WHERE I")
                    strSql.Append(indexId.ToString())
                End If

            Else
                'si Indexid = 0 Se toma como el nombre del documento
                strSql.Append(" WHERE name")
            End If
            Dim ResultDate As DateTime
            If Date.TryParse(comparateValue, ResultDate) Then
                If Date.TryParse(comparateDateValue, ResultDate) Then
                    strSql.Append(" >= ")
                    strSql.Append(Server.Con.ConvertDateTime(comparateValue))
                    strSql.Append(" AND I")
                    strSql.Append(indexId.ToString())
                    strSql.Append(" <= ")
                    strSql.Append(Server.Con.ConvertDateTime(comparateDateValue))
                Else
                    strSql.Append(" " & Operation & " ")
                    strSql.Append(Server.Con.ConvertDateTime(comparateValue))
                End If
            Else
                Select Case Operation.ToUpper
                    Case "CONTIENE"
                        strSql.Append(" LIKE '%")
                        strSql.Append(comparateValue)
                        strSql.Append("%'")
                    Case "EMPIEZA"
                        strSql.Append(" LIKE '")
                        strSql.Append(comparateValue)
                        strSql.Append("%'")
                    Case "TERMINA"
                        strSql.Append(" LIKE '%")
                        strSql.Append(comparateValue)
                        strSql.Append("'")
                    Case "IGUAL"
                        strSql.Append(" = '")
                        strSql.Append(comparateValue)
                        strSql.Append("'")
                    Case "ES NULO"
                        strSql.Append(" = '' or ")
                        If indexId > 0 Then
                            strSql.Append("I" & indexId)
                            strSql.Append(" is null ")
                        Else
                            'si Indexid = 0 Se toma como el nombre del documento
                            strSql.Append(" name is null")
                        End If
                    Case "DISTINTO"
                        strSql.Append(" <> '")
                        strSql.Append(comparateValue)
                        strSql.Append("'")
                    Case "ENTRE"
                        strSql.Append(" >= '")
                        strSql.Append(comparateValue)
                        strSql.Append("' and I" & indexId & " <= '")
                        strSql.Append(comparateDateValue)
                        strSql.Append("' ")
                    Case Else
                        strSql.Append(" " & Operation & " '")
                        strSql.Append(comparateValue)
                        strSql.Append("'")
                End Select
            End If
        End If
        If Not String.IsNullOrEmpty(restricc) Then
            strSql.Append("AND " & restricc)
        End If
        If Not String.IsNullOrEmpty(SortExpression) Then
            SortExpression = SortExpression.Trim
            SortExpression = SortExpression.Insert(0, "'")
            SortExpression = SortExpression.Replace("DESC", "' DESC")
            SortExpression = SortExpression.Replace("ASC", "' ASC")

            strSql.Append(" ORDER BY " & SortExpression)
        End If
        Dim RestrictionAndSortExpression As String = strSql.ToString

        Return Results_Factory.getResultsAndPageQueryResults(PageId, PageSize, docTypeId, indexId, genIndex, RestrictionAndSortExpression, SymbolToReplace, BySimbolReplace, resultCount)
        'Return Nothing
    End Function

#End Region
    ''' <summary>
    ''' Obtiene las relaciones de tabla zrelations
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>Diego 31-07-2008 Created</history>
    Public Shared Function GetDocRelations() As DataSet
        Try
            Return Results_Factory.GetDocRelations
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return Nothing
    End Function

#End Region

#Region "Insert & Save Document"

    Public Shared Function GetNewNewResult(ByVal DocType As DocType, Optional ByVal _UserId As Int32 = 0, Optional ByVal File As String = "", Optional ByVal loadVolume As Boolean = True) As NewResult
        Dim newResult As New NewResult(File)
        newResult.UserID = _UserId
        If IsNothing(DocType) = False Then
            newResult.Parent = DocType

            If loadVolume Then
                Try
                    'todo: el loadvolume deberia ser del doctype y no del result
                    Results_Business.LoadVolume(newResult)
                Catch ex As Exception
                    newResult.Ready = False
                End Try
            End If

            LoadIndexs(DirectCast(newResult, NewResult))
        End If
        Return newResult
    End Function

    Public Shared Function GetSearchResult(ByVal DocType As DocType, Optional ByVal _UserId As Int32 = 0, Optional ByVal File As String = "") As NewResult
        Dim newResult As New NewResult(File)
        newResult.DocType = DocType
        newResult.UserID = _UserId
        Try
            LoadVolume(newResult)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        LoadIndexs(DirectCast(newResult, NewResult))
        Return newResult
    End Function

    Public Shared Function CloneResult(ByVal originalResult As Result,
                                       ByVal filename As String,
                                       ByVal GenerateIds As Boolean,
                                       Optional ByVal FlagInsertar As Boolean = False,
                                       Optional ByVal setDiskGroupIdToZero As Boolean = False) As NewResult
        Dim ClonedResult As New NewResult(filename)
        ClonedResult.OriginalName = originalResult.OriginalName
        ClonedResult.AutoName = originalResult.AutoName
        ClonedResult.DISK_VOL_PATH = originalResult.DISK_VOL_PATH

        If setDiskGroupIdToZero Then
            ClonedResult.Disk_Group_Id = 0
        Else
            ClonedResult.Disk_Group_Id = originalResult.Disk_Group_Id
        End If
        If GenerateIds Then
            ClonedResult.ID = CoreData.GetNewID(IdTypes.DOCID)
            ClonedResult.Doc_File = ClonedResult.ID & New FileInfo(filename).Extension
        Else
            ClonedResult.ID = 0
            ClonedResult.Doc_File = New FileInfo(filename).Name
        End If

        ClonedResult.DocType = originalResult.DocType
        ClonedResult.DocumentalId = originalResult.DocumentalId

        ClonedResult.IconId = originalResult.IconId
        ClonedResult.Indexs = originalResult.Indexs
        ClonedResult.Index = originalResult.Index
        ClonedResult.Name = originalResult.Name
        ClonedResult.Object_Type_Id = originalResult.Object_Type_Id
        ClonedResult.OffSet = originalResult.OffSet
        ClonedResult.Platter_Id = originalResult.Platter_Id
        ClonedResult.Thumbnails = originalResult.Thumbnails

        If FlagInsertar Then
            ClonedResult.VersionNumber = 0
            ClonedResult.ParentVerId = 0
            ClonedResult.RootDocumentId = 0
        Else
            ClonedResult.ParentVerId = originalResult.ID
            ClonedResult.VersionNumber = GetNewVersionID(originalResult.RootDocumentId, originalResult.DocType.ID, originalResult.ID)
            If originalResult.RootDocumentId = 0 Then
                ClonedResult.RootDocumentId = originalResult.ID
            Else
                ClonedResult.RootDocumentId = originalResult.RootDocumentId
            End If
        End If

        Return ClonedResult
    End Function
    Public Shared Sub CloneIndexs(ByVal r As Result, ByVal DocType As DocType)
        For Each I As Index In DocType.Indexs
            Dim NewI As New Index(I)
            r.Indexs.Add(I)
        Next
    End Sub
    Public Shared Function CloneIndexs(Indexs As ArrayList) As ArrayList
        Dim ClonedIndexs As New ArrayList
        For Each I As Index In Indexs
            Dim NewI As New Index(I)
            ClonedIndexs.Add(I)
        Next
        Return ClonedIndexs
    End Function
    ''' <summary>
    ''' Inserta una relacion entre dos results
    ''' </summary>
    ''' <param name="parentResultId">id del result padre</param>
    ''' <param name="relationatedResultId">id del result relacionado</param>
    ''' <param name="relationId">id de la relacion</param>
    ''' <remarks></remarks>
    ''' <history>Diego 31-07-2008 Created</history>
    Shared Sub InsertDocumentRelation(ByVal parentResultId As Int64, ByVal relationatedResultId As Int64, ByVal relationid As Int32)
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "insert document relation")
            Results_Factory.InsertDocumentRelation(parentResultId, relationatedResultId, relationid)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Inserta una relacion entre dos results
    ''' </summary>
    ''' <param name="parentResultId">id del result padre</param>
    ''' <param name="relationatedResultId">id del result relacionado</param>
    ''' <param name="relationId">id de la relacion</param>
    ''' <remarks></remarks>
    ''' <history>Diego 31-07-2008 Created</history>
    Shared Sub InsertDocumentRelation(ByVal parentResultId As Int64, ByVal relationatedResultId As Int64, ByVal relationid As Int32, ByRef t As Transaction)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "insert document relation")
        Results_Factory.InsertDocumentRelation(parentResultId, relationatedResultId, relationid, t)

    End Sub

#Region "Agregar o remover DocTypes de Workflow"

    Public Shared Sub AddDocTypeToWF(ByVal docTypeId As Int64, ByVal WfID As Int32)
        Results_Factory.AddDocTypeToWF(docTypeId, WfID)
    End Sub
    Public Shared Sub RemoveDocTypeWF(ByVal docTypeId As Int64)
        Results_Factory.RemoveDocTypeWF(docTypeId)
    End Sub
    Public Shared Function GetInitialStep(ByVal WFID As Int16) As Int32
        Dim initialStep As Int32
        initialStep = Results_Factory.GetInitialStep(WFID)
        Return initialStep
    End Function
    Public Shared Function IsDocTypeInWF(ByVal docTypeId As Int64) As Boolean
        Return Results_Factory.IsDocTypeInWF(docTypeId)
    End Function
#End Region
    Public Shared Event ResultInserted(ByRef Result As IResult, ByVal ParentTaskId As Int64)

#End Region

#Region "Indexs"


    Public Enum indexType As Integer
        index = 0
        unique = 1
        notnull = 2
    End Enum
    Public Shared Sub LoadIndexs(ByRef result As IResult)
        if result.DocType.Indexs.Count = 0 then result.DocType.Indexs = ZCore.FilterIndex(result.DocType.ID)
        Results_Business.CloneIndexs(result, DirectCast(result.DocType, DocType))
        '  result.Indexs = ZCore.FilterIndex(result.DocType.ID)
    End Sub


    Public Shared Sub LoadFileFromDB(ByRef res As IResult)
        res.EncodedFile = Results_Factory.LoadFileFromDB(res.ID, res.DocTypeId)
    End Sub


    ''' <summary>
    ''' Updates the result encoded file
    ''' </summary>
    ''' <param name="res"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateDOCB(ByVal res As Result)
        Results_Factory.UpdateDOCB(res)
    End Sub

    Public Shared Sub CompleteIndexData(ByVal docTypeId As Int64, ByVal docId As Int64, ByRef indexs As ArrayList)
        Results_Factory.CompleteIndexData(docTypeId, docId, indexs)
    End Sub
    ''' <summary>
    ''' Completa los atributos del documento
    ''' </summary>
    ''' <param name="_result"></param>
    ''' <param name="inThread"></param>
    ''' <remarks></remarks>
    Public Shared Sub CompleteIndexData(ByRef _result As Result, Optional ByVal inThread As Boolean = False)
        Dim dr As IDataReader = Nothing
        Dim con As IConnection = Nothing

        Try
            con = Server.Con
            dr = Results_Factory.CompleteIndexDataDr(_result.ID, _result.DocTypeId, _result.Indexs, con)
            If dr.IsClosed = False Then
                While dr.Read
                    Dim i As Int32
                    If Not IsDBNull(dr.GetValue(0)) Then
                        If Not IsNothing(dr.GetValue(0)) Then
                            If dr.GetValue(0).ToString() <> String.Empty Then
                                Try
                                    If Not IsNothing(_result) Then
                                        _result.CreateDate = DateTime.Parse(dr.GetValue(0).ToString())
                                    End If
                                Catch ex As Exception
                                    ZClass.raiseerror(ex)
                                End Try

                            End If
                        End If
                    End If

                    For i = 0 To _result.Indexs.Count - 1
                        Try
                            If Not IsDBNull(dr.GetValue(dr.GetOrdinal("I" & DirectCast(_result.Indexs(i), Index).ID))) Then
                                DirectCast(_result.Indexs(i), Index).Data = dr.GetValue(dr.GetOrdinal("I" & DirectCast(_result.Indexs(i), Index).ID)).ToString
                                'Si el atributo es de tipo Sustitucion
                                If DirectCast(_result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustitución _
                                    Or DirectCast(_result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                    'Se carga la descripcion de Atributo
                                    DirectCast(_result.Indexs(i), Index).dataDescription = AutoSubstitutionBusiness.getDescription(DirectCast(_result.Indexs(i), Index).Data, DirectCast(_result.Indexs(i), Index).ID, inThread, DirectCast(_result.Indexs(i), Index).Type)
                                End If
                            Else
                                DirectCast(_result.Indexs(i), Index).Data = String.Empty
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Next
                End While
            End If
        Finally
            If Not IsNothing(dr) Then
                dr.Close()
                dr.Dispose()
                dr = Nothing
            End If
            If Not IsNothing(con) Then
                con.Close()
                con.dispose()
                con = Nothing
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Completa los atributos de la tarea
    ''' </summary>
    ''' <param name="_result"></param>
    ''' <param name="inThread"></param>
    ''' <remarks></remarks>
    Public Shared Sub FillIndexData(ByRef _result As TaskResult, Optional ByVal inThread As Boolean = False)
        Dim dr As IDataReader = Nothing
        Dim con As IConnection = Nothing
        Try
            If _result.Indexs.Count = 0 Then
                _result.Indexs = ZCore.FilterIndex(_result.DocType.ID)
            End If
            con = Server.Con
            dr = Results_Factory.CompleteIndexDataDr(_result.DocTypeId, _result.ID, _result.Indexs, con)
            If dr.IsClosed = False Then
                While dr.Read
                    Dim i As Int32
                    If Not IsDBNull(dr.GetValue(0)) Then
                        If Not IsNothing(dr.GetValue(0)) Then
                            If dr.GetValue(0).ToString() <> String.Empty Then
                                Try
                                    If Not IsNothing(_result) Then
                                        _result.CreateDate = DateTime.Parse(dr.GetValue(0).ToString())
                                    End If
                                Catch ex As Exception
                                    ZClass.raiseerror(ex)
                                End Try

                            End If
                        End If
                    End If

                    For i = 0 To _result.Indexs.Count - 1
                        Try
                            If Not IsDBNull(dr.GetValue(dr.GetOrdinal("I" & DirectCast(_result.Indexs(i), Index).ID))) Then
                                DirectCast(_result.Indexs(i), Index).Data = dr.GetValue(dr.GetOrdinal("I" & DirectCast(_result.Indexs(i), Index).ID)).ToString
                                'Si el atributo es de tipo Sustitucion
                                If DirectCast(_result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustitución _
                                    Or DirectCast(_result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                    'Se carga la descripcion de Atributo
                                    DirectCast(_result.Indexs(i), Index).dataDescription = AutoSubstitutionBusiness.getDescription(DirectCast(_result.Indexs(i), Index).Data, DirectCast(_result.Indexs(i), Index).ID, inThread, DirectCast(_result.Indexs(i), Index).Type)
                                End If
                            Else
                                DirectCast(_result.Indexs(i), Index).Data = String.Empty
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Next
                End While
            End If
        Finally
            If Not IsNothing(dr) Then
                dr.Close()
                dr.Dispose()
                dr = Nothing
            End If
            If Not IsNothing(con) Then
                con.Close()
                con.dispose()
                con = Nothing
            End If
        End Try
    End Sub

    Public Shared Function ValidateIndexDatabyRights(ByVal _result As Result) As Boolean
        If UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, _result.DocType.ID) Then
            Dim IRI As Hashtable = UserBusiness.Rights.GetIndexsRights(_result.DocType.ID, Membership.MembershipHelper.CurrentUser.ID, True, True)
            For Each ir As IndexsRightsInfo In IRI.Values
                If ir.GetIndexRightValue(RightsType.IndexRequired) = True Then

                    For Each _index As Index In _result.Indexs
                        If _index.ID = ir.Indexid Then
                            If String.IsNullOrEmpty(_index.DataTemp) Then
                                Return False
                            End If
                        End If
                    Next
                End If

            Next
        End If
        Return True
    End Function

    ''' <summary>
    ''' Guarda solamente los datos modificados
    ''' </summary>
    ''' <param name="result"></param>
    ''' <param name="reIndexFlag"></param>
    ''' <param name="changeEvent"></param>
    ''' <param name="OnlySpecifiedIndexsids"></param>
    ''' <remarks></remarks>
    Public Sub SaveModifiedIndexData(ByRef result As IResult, ByVal reIndexFlag As Boolean, ByVal changeEvent As Boolean, Optional ByVal OnlySpecifiedIndexsids As Generic.List(Of Int64) = Nothing)
        Dim taskResult As ITaskResult = Nothing
        Dim WFTaskBusiness As WFTaskBusiness

        Try
            Results_Factory.SaveModifiedIndexData(result, reIndexFlag, OnlySpecifiedIndexsids)
            UpdateAutoName(result)
            Results_Factory.InsertIndexerState(result.DocTypeId, result.ID, 0, Nothing)

            'Si se modifican los atributos y es una tarea se ejecutan las reglas de modificacion de atributos
            If changeEvent = True Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Verifica si el documento es una tarea")
                If result.GetType() Is GetType(TaskResult) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Disparando el evento INDICES de la tarea")
                    WFTaskBusiness = New WFTaskBusiness()
                    WFTaskBusiness.ExecuteEventRules(result, True, TypesofRules.Indices)
                Else
                    taskResult = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(result.ID, 0)

                    If Not IsNothing(taskResult) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Disparando el evento INDICES de la tarea")
                        WFTaskBusiness = New WFTaskBusiness()
                        WFTaskBusiness.ExecuteEventRules(taskResult, True, TypesofRules.Indices)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El evento INDICES no es disparado ya que la tarea no fue encontrada")
                    End If
                End If
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El evento INDICES no es disparado")
            End If
        Finally
            If taskResult IsNot Nothing Then
                taskResult.Dispose()
                taskResult = Nothing
            End If
            WFTaskBusiness = Nothing
        End Try
    End Sub

    Public Shared Sub ResultUpdated(ByVal DocTypeId As Int64, ByVal ResultId As Int64)
        Dim taskResult As ITaskResult = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(ResultId, 0)
        If Not IsNothing(taskResult) Then
            Dim WFTaskBusiness As New WFTaskBusiness
            WFTaskBusiness.ExecuteEventRules(taskResult, True, TypesofRules.Indices)
            WFTaskBusiness = Nothing
            taskResult.Dispose()
            taskResult = Nothing
        End If

    End Sub

    Private Shared Function CheckRequiredIndexs(ByRef result As NewResult) As Boolean

        Dim isOk As Boolean

        For Each I As IIndex In result.Indexs

            If I.Required AndAlso String.IsNullOrEmpty(I.DataTemp.Trim) AndAlso String.IsNullOrEmpty(I.Data.Trim) Then
                Throw New Exception("El Atributo " & I.Name & " es obligatorio")
            End If

            If I.DropDown = IndexAdditionalType.DropDown OrElse I.DropDown = IndexAdditionalType.AutoSustitución OrElse I.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then

                isOk = False

                'si no se permite valores fuera de la lista. 
                If Not IndexsBusiness.CheckIfAllowDataOutOfList(I.ID) Then

                    If I.DropDown = IndexAdditionalType.DropDown OrElse I.DropDown = IndexAdditionalType.DropDownJerarquico Then

                        Dim Data As List(Of String) = IndexsBusiness.GetDropDownList(I.ID)

                        For Each d As String In Data
                            If String.IsNullOrEmpty(I.Data.Trim) OrElse d.Trim.ToLower = I.Data.Trim.ToLower Then
                                'si esta en la lista esta ok! 
                                isOk = True
                                Exit For
                            End If
                        Next

                    Else

                        Dim dt As DataTable = AutoSubstitutionBusiness.GetIndexData(I.ID, False)

                        For Each row As DataRow In dt.Rows

                            If String.IsNullOrEmpty(I.Data.Trim) OrElse row.Item(0).ToString.Trim().ToLower = I.Data.Trim.ToLower Then
                                'si esta en la lista esta ok! 
                                isOk = True
                                Exit For
                            End If

                            If String.IsNullOrEmpty(I.Data.Trim) OrElse row.Item(1).ToString.Trim().ToLower = I.dataDescription.Trim.ToLower Then
                                'si esta en la lista esta ok! 
                                isOk = True
                                Exit For
                            End If

                        Next

                    End If

                    If Not isOk Then
                        Throw New Exception("El Atributo " & I.Name & " no permite valores fuera de la lista")
                    End If

                End If

            End If

        Next

        Return True
    End Function


    Shared Reg As New Results_Business

    Public Shared Function InsertDocument(ByRef newResult As NewResult, ByVal move As Boolean, Optional ByVal ReindexFlag As Boolean = False, Optional ByVal Reemplazar As Boolean = False, Optional ByVal showQuestions As Boolean = True, Optional ByVal IsVirtual As Boolean = False, Optional ByVal IsReplica As Boolean = False, Optional ByVal hasName As Boolean = False, Optional ByVal RefreshWFAfterInsert As Boolean = True, Optional ByVal openTask As Boolean = False, Optional ByVal OpenDocument As Boolean = False, Optional ByVal ParentTaskId As Int64 = 0, Optional ByVal DoAutoComplete As Boolean = True) As InsertResult
        ZTrace.WriteLineIf(ZTrace.IsInfo, "insert document")
        Try
            SyncLock Reg
                If Reg Is Nothing Then
                    Reg = New Results_Business
                End If
                Return Results_Business.Insert(newResult, move, ReindexFlag, Reemplazar, showQuestions, IsVirtual, IsReplica, hasName, False, RefreshWFAfterInsert, OpenDocument, ParentTaskId)

            End SyncLock
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return InsertResult.NoInsertado
        End Try
    End Function

    'TODO: [sebstian] se hizo un over load para poder cargar los atributos de danone correctamente en zamba
    Public Shared Function InsertDocumentNew(ByRef newResult As NewResult, ByVal move As Boolean, Optional ByVal ReindexFlag As Boolean = False, Optional ByVal Reemplazar As Boolean = False, Optional ByVal showQuestions As Boolean = True, Optional ByVal IsVirtual As Boolean = False, Optional ByVal IsReplica As Boolean = False, Optional ByVal hasName As Boolean = False, Optional ByVal OpenDocumentAfterInsert As Boolean = False, Optional ByVal ParentTaskId As Int64 = 0) As InsertResult
        ZTrace.WriteLineIf(ZTrace.IsInfo, "insert documentt new")
        Try
            SyncLock Reg
                If Reg Is Nothing Then
                    Reg = New Results_Business
                End If

                Return Results_Business.InsertNew(newResult, move, ReindexFlag, Reemplazar, showQuestions, IsVirtual, IsReplica, hasName)
            End SyncLock
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return InsertResult.NoInsertado
        End Try
    End Function

    Private Shared Function GetResultName(ByRef result As NewResult) As String
        Try
            Return GetResultName(result.DocType.ID, result.CreateDate, result.EditDate, result.Indexs)
            'Dim AutoName As String = ZCore.GetDocTypeAutoName(result.DocType.ID)
            'Return DocTypesFactory.GetAutoName(AutoName, DocTypesFactory.GetDocTypeName(result.DocType.ID), result.CreateDate, result.EditDate, result.Indexs).Trim()
        Catch ex As Exception
            ZClass.raiseerror(ex)
            If Not String.IsNullOrEmpty(result.DocType.Name) Then
                Return result.DocType.Name
            Else
                Return DocTypesBusiness.GetDocTypeName(result.DocTypeId, True)
            End If
        End Try

    End Function

    Private Shared Function GetResultName(ByVal docTypeId As Int64, ByVal createDate As Date, ByVal editDate As Date, ByVal indexs As List(Of IIndex)) As String
        Dim AutoName As String = ZCore.GetDocTypeAutoName(docTypeId)
        Return DocTypesBusiness.GetAutoName(AutoName, DocTypesBusiness.GetDocTypeName(docTypeId, True), createDate, editDate, indexs).Trim()
    End Function

    ''' <summary>
    ''' Inserta un binario a zamba y devuelve el id
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="binaryDocument"></param>
    ''' <param name="fileExtension"></param>
    ''' <param name="docTypeId"></param>
    ''' <param name="indexs"></param>
    ''' <history>Marcelo modified 29/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal name As String, ByVal binaryDocument As Byte(), ByVal fileExtension As String, ByVal docTypeId As Int64, ByVal indexs As DataTable, ByVal DontOpenTaskAfterInsertInDoGenerateCoverPage As Boolean, ByRef transaction As Transaction) As Int64
        Dim resultId As Int64 = 0
        Dim temporaryPath As String = Nothing
        Dim documentPath As String = Nothing

        Try
            If Not String.IsNullOrEmpty(fileExtension.Trim()) Then
                temporaryPath = Tools.EnvironmentUtil.GetTempDir("\Temp").FullName

                If (Not fileExtension.Contains(".")) Then
                    fileExtension = "." + fileExtension
                End If

                documentPath = temporaryPath + "\" + name + fileExtension

                If Not Directory.Exists(temporaryPath) Then
                    Directory.CreateDirectory(temporaryPath)
                ElseIf File.Exists(documentPath) Then
                    File.Delete(documentPath)
                End If

                _fileStream = New FileStream(documentPath, FileMode.CreateNew)
                _binaryWriter = New BinaryWriter(_fileStream)
                _binaryWriter.Write(binaryDocument)
                _fileStream.Flush()
                _fileStream.Close()
                _binaryWriter.Close()
                _fileStream.Dispose()
                _fileStream = Nothing

                Array.Clear(binaryDocument, 0, binaryDocument.Length)
            Else
                documentPath = String.Empty
            End If

            Using CurrentNewResult As NewResult = GetNewResult(docTypeId, documentPath, False)

                If (Not IsNothing(indexs) AndAlso indexs.Rows.Count > 0) Then
                    Dim ExistsIndexId As Boolean
                    Dim DocTypeName As String

                    For Each CurrentIndexRow As DataRow In indexs.Rows
                        ExistsIndexId = False

                        For Each CurrentIndex As IIndex In CurrentNewResult.Indexs
                            If CurrentIndex.ID = CurrentIndexRow.Item("IndexId") Then
                                CurrentIndex.DataTemp = CurrentIndexRow.Item("IndexValue")
                                CurrentIndex.Data = CurrentIndex.DataTemp
                                ExistsIndexId = True

                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Pregunto si el Atributo es Valido If Not CurrentIndex.isvalid() Then")
                                If Not CurrentIndex.isvalid() Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "El Atributo NO es Valido")
                                    Throw New Exception("El valor '" & CurrentIndex.DataTemp & "' no es valido con el tipo de datos del indice " & CurrentIndex.Name)
                                End If
                            End If
                        Next

                        If Not ExistsIndexId Then
                            DocTypeName = DocTypesBusiness.GetDocTypeName(docTypeId, True)
                            Throw New Exception("La entidad " & DocTypeName & " no contiene un Atributo con ID " & CurrentIndexRow.Item("IndexId").ToString() & " o el usuario no tiene permisos sobre el mismo")
                        End If

                    Next
                End If

                If String.IsNullOrEmpty(fileExtension.Trim()) Then
                    Insert(CurrentNewResult, False, False, False, False, True, False, False, True, True, False, 0, transaction)
                Else
                    Insert(CurrentNewResult, True, False, False, False, False, False, False, True, True, False, 0, transaction)
                End If

                resultId = CurrentNewResult.ID
            End Using

        Finally
            If Not IsNothing(binaryDocument) Then
                Array.Clear(binaryDocument, 0, binaryDocument.Length)
                binaryDocument = Nothing
            End If
            If indexs IsNot Nothing Then
                indexs = Nothing
            End If
            If Not String.IsNullOrEmpty(documentPath) AndAlso File.Exists(documentPath) Then
                File.Delete(documentPath)
            End If

            name = Nothing
            fileExtension = Nothing
            temporaryPath = Nothing
            documentPath = Nothing
        End Try

        Return resultId
    End Function

    ''' <summary>
    ''' [Sebastian 13-05-09] valida que indice este completo si es requerido contra la ZIR
    ''' </summary>
    ''' <param name="_newresult"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ValidateIndexData(ByVal _newresult As NewResult) As Boolean

        Dim IRI As Hashtable = UserBusiness.Rights.GetIndexsRights(_newresult.DocType.ID, Membership.MembershipHelper.CurrentUser.ID, True, True)
        For Each i As Index In _newresult.DocType.Indexs
            If DirectCast(IRI(i.ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexRequired) = True Then
                If String.IsNullOrEmpty(i.Data) = True AndAlso String.IsNullOrEmpty(i.DataTemp) = True Then
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    ''' <summary>
    '''(pablo) valida que que se hayan ingresado valores correctos en los atributos de sustitucion
    ''' </summary>
    ''' <param name="_newresult">Result a insertar</param>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Pablo] Created
    '''     [Tomas] 09/06/2011  Modified    Se agrega la validación de atributos de búsqueda.
    ''' </history>
    Public Shared Function ValidateSlstIlstDescription(ByVal _newresult As NewResult) As Boolean
        'Recorre los atributos del result a insertar
        For Each i As Index In _newresult.Indexs
            'Verifica si tiene algo cargado
            If Not String.IsNullOrEmpty(i.DataTemp) Then
                'Verifica solamente los que sean de sustitución o búsqueda si la descripción existe.
                If i.DropDown = IndexAdditionalType.AutoSustitución Or i.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                    If String.IsNullOrEmpty(AutoSubstitutionBusiness.getDescription(i.DataTemp, i.ID, True, i.Type, True)) Then
                        Return False
                    End If
                ElseIf i.DropDown = IndexAdditionalType.DropDown Then                    
                    If Not DirectCast(Cache.DocTypesAndIndexs.hsIndexsArray(i.ID), IList).Contains(i.DataTemp) Then
                        Return False
                    End If
                End If
            End If
        Next
        Return True
    End Function

    ''' <summary>
    ''' [Sebastian 13-05-09] valida que el atributo en r_doc_type tenga el permiso de obligatorio. Si no lo
    ''' tiene devuelve false
    ''' </summary>
    ''' <param name="_newresult"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ValidateIndexDataFromDoctype(ByVal _newresult As NewResult) As Boolean

        Dim dsIndexproperty As DataSet = DocTypesBusiness.GetIndexsProperties(_newresult.DocType.ID, True)

        For Each i As Index In _newresult.Indexs
            For Each row As DataRow In dsIndexproperty.Tables(0).Rows
                If row("index_id") = i.ID AndAlso row("mustcomplete") = 1 Then
                    If String.IsNullOrEmpty(i.Data) = True AndAlso String.IsNullOrEmpty(i.DataTemp) = True Then
                        Return False
                    End If
                End If
            Next
        Next

        Return True
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="newResult"></param>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 29/04/09 Modified: Se adapto el codigo para que tome mas de una clave. </history>
    Private Shared Sub AutocompleteIndexsNewDocument(ByRef newResult As NewResult)
        Dim AC As AutocompleteBCBusiness = Nothing
        Dim indexTemp As ArrayList

        Try
            'Obtiene el campo IndexKey relacionado con AutoComplete del primer
            'documento. Es es porque deberían ser todos del mismo tipo.
            indexTemp = AutoCompleteBarcode_FactoryBusiness.getIndexKeys(newResult.DocType.ID)
            'Si ocurre un error en este punto, es porque index
            'es Nothing que quiere decir 1 el documento no tiene
            'atributos para autocompletado
            If Not indexTemp Is Nothing AndAlso indexTemp.Count > 0 Then

                AC = AutoCompleteBarcode_FactoryBusiness.GetComplete(Int32.Parse(newResult.DocType.ID), indexTemp(0).ID) 'Obtiene una instancia del Objeto AutoComplete

                If Not IsNothing(AC) Then 'Siempre AC deberia ser una instancia
                    'Actuliza el valor del indice.
                    'Dicho valor es utilizado para el seguimiento del documento dentro
                    'de un WorkFlow.
                    For Each intmp As Index In indexTemp
                        intmp.DataTemp = findIn(newResult.Indexs, intmp).Data
                    Next
                    'Dim res As Result = New Result()
                    'If indextemp.count = 1 Then
                    '    res = AC.Complete(newResult, DirectCast(indexTemp(0), Index))
                    'Else

                    'End If
                    'Persiste los cambios en el documento
                    If Not IsNothing(AC.Complete(DirectCast(newResult, NewResult), indexTemp, Nothing, True)) Then
                        'se cambio por save modified
                        Dim rstBuss As New Results_Business()
                        rstBuss.SaveModifiedIndexData(DirectCast(newResult, NewResult), True, True)
                        rstBuss = Nothing
                    End If
                    AC.Dispose()
                    AC = Nothing
                End If
                indexTemp = Nothing
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo que inserta un nuevo documento en zamba
    ''' </summary>
    ''' <param name="newResult"></param>
    ''' <param name="move"></param>
    ''' <param name="reIndexFlag"></param>
    ''' <param name="reemplazarFlag"></param>
    ''' <param name="showQuestions"></param>
    ''' <param name="isVirtual"></param>
    ''' <param name="isReplica"></param>
    ''' <param name="hasName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	11/09/2008	Modified    Se agrego el evento que permite refrescar el  o los workflows
    '''     [Tomas]     27/08/2009  Modified    Se agrega transacciones
    '''     [Tomas]     06/10/2009  Modified    Se corrige atrapa una exception al intentar eliminar archivos de imagenes y otros.
    ''' </history>
    Public Shared Function Insert(ByRef newResult As NewResult, ByVal move As Boolean, Optional ByVal reIndexFlag As Boolean = False, Optional ByVal reemplazarFlag As Boolean = False, Optional ByVal showQuestions As Boolean = True, Optional ByVal isVirtual As Boolean = False, Optional ByVal isReplica As Boolean = False, Optional ByVal hasName As Boolean = False, Optional ByVal throwEx As Boolean = False, Optional ByVal RefreshWFAfterInsert As Boolean = True, Optional ByVal OpenDocumentAfterInsert As Boolean = False, Optional ByVal ParentTaskId As Int64 = 0, Optional ByRef transaction As Transaction = Nothing, Optional ByVal DoAutocomplete As Boolean = True) As InsertResult
        Dim NewResultStatus As InsertResult = InsertResult.NoInsertado

        Try
            CheckRequiredIndexs(newResult)

            'Autocompleta los atributos si corresponde, Se subio en nivel la ejecucion porque quizas se utilizan valores de los atributos para autonombre
            If DoAutocomplete Then AutocompleteIndexsNewDocument(newResult)
            '[sebastian 29/01/09] Se verifica si tiene el permiso de completar con el id mas alto el atributo
            'y luego se lo agrega.
            'obtengo los permismos de los atributos
            'Dim IRI As Hashtable = UserBusiness.Rights.GetIndexsRights(newResult.DocType.ID, Membership.MembershipHelper.CurrentUser.ID, True, True)

            'Si el atributo es autoincremental le cargo el valor si no lo tiene
            Dim dsIndexsToIncrement As DataSet = DocTypesBusiness.GetIndexsProperties(newResult.DocType.ID, True)
            Dim IncrementedValue As Int64 = 0
            For Each CurrentRow As DataRow In dsIndexsToIncrement.Tables(0).Rows
                If IsDBNull(CurrentRow("autoincremental")) = False AndAlso Int64.Parse(CurrentRow("Autoincremental").ToString) = 1 Then
                    For Each CurrentIndex As Index In newResult.Indexs
                        If String.Compare(CurrentRow("Index_Name").ToString.Trim, CurrentIndex.Name.Trim) = 0 Then
                            If CurrentIndex.Data.Trim() = String.Empty Then
                                IncrementedValue = IndexsBusiness.SelectMaxIndexValue(CurrentIndex.ID, newResult.DocType.ID)
                                CurrentIndex.Data = IncrementedValue.ToString
                                CurrentIndex.DataTemp = IncrementedValue.ToString
                            Else
                                CurrentIndex.DataTemp = CurrentIndex.Data
                            End If
                        End If
                    Next
                End If
            Next

            If newResult.ID = 0 Then newResult.ID = CoreData.GetNewID(IdTypes.DOCID)

            newResult.Name = GetResultName(newResult)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre: " & newResult.Name)

            If isVirtual = False OrElse isReplica OrElse newResult.Volume.Type <> VolumeTypes.DataBase Then 'Intento cargar un volumen para el documento, si no se puede se genera una excepcion
                Results_Business.LoadVolume(newResult)
                newResult.NewFile = VolumesBusiness.VolumePath(newResult.Volume, newResult.DocType.ID) & "\" & newResult.ID & newResult.Extension
                newResult.OffSet = newResult.Volume.offset
                If hasName = False Then
                    Dim f As New FileInfo(newResult.NewFile)
                    newResult.Doc_File = f.Name
                    f = Nothing
                End If
            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "ID del documento a insertar: " & newResult.ID)
            newResult.IconId = Results_Business.GetFileIcon(newResult.File) 'Obtengo el icono

            If isVirtual = False Or isReplica Then 'Copio fisicamente el archivo
                'Si el volumen es de tipo DB lo codifica, caso contrario mueve el archivo fisico al servidor
                If newResult.Volume.Type = VolumeTypes.DataBase Then
                    newResult.EncodedFile = FileEncode.Encode(newResult.File) 'result.NewFile ?
                Else
                    If move Then
                        Try
                            Results_Business.MoveFile(newResult)
                        Catch ex As Exception
                            Results_Business.copyFile(newResult)
                        End Try
                    Else
                        Results_Business.copyFile(newResult)
                    End If
                End If
            End If

            If isVirtual And isReplica = False Then
                newResult.Disk_Group_Id = 0
            Else
                newResult.Disk_Group_Id = newResult.Volume.ID
                newResult.DISK_VOL_PATH = newResult.Volume.path
            End If

            Dim InsertedWFIdsAndInitialStepsAndTasksResults As New ArrayList
            Dim WFIds As ArrayList = DocTypesBusiness.GetDocTypeWfIds(newResult.DocType.ID)
            Dim ResultsArray As New ArrayList(1)

            'Objeto para realizar la inserción en una transaccion
            Dim Transact As Transaction
            If transaction Is Nothing Then
                Transact = New Transaction
            Else
                Transact = transaction
            End If

            Try
                Try
                    RegisterDocumentWithAlfanumericIndex(newResult, reIndexFlag, isVirtual, Transact)
                    NewResultStatus = InsertResult.Insertado
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsError, "Error al guardar la doc i Buscando si es un reemplazo " & ex.ToString)
                    If ex.ToString.ToLower.IndexOf("unique") <> -1 OrElse ex.ToString.ToLower.IndexOf("unica") <> -1 Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "UNIQUE DETECTADO")
                        If reemplazarFlag Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Reemplazo detectado")

                            Results_Business.Delete(newResult, True, Transact)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento Borrado")
                            RegisterDocumentWithAlfanumericIndex(newResult, reIndexFlag, isVirtual, Transact)
                            '                        Results_Business.ReplaceDocument(newResult)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento Reemplazado")
                            NewResultStatus = InsertResult.Remplazado
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se hara reemplazo")
                            If showQuestions Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se haran preguntas")
                                'TODO : MsgBox en Business?
                                Select Case ReplaceMsgBox.Show("Los datos ingresados no son únicos. ¿Desea reemplazar el documento existente?", "Insertar Documento")
                                    Case ReplaceMsgBox.ReplaceMsgBoxResult.yes
                                        Results_Business.Delete(newResult, False, Transact)
                                        Results_Business.ReplaceDocument(newResult, Transact)
                                        NewResultStatus = InsertResult.Remplazado
                                    Case ReplaceMsgBox.ReplaceMsgBoxResult.yesAll
                                        Results_Business.Delete(newResult, False, Transact)
                                        Results_Business.ReplaceDocument(newResult, Transact)
                                        NewResultStatus = InsertResult.RemplazadoTodos
                                    Case ReplaceMsgBox.ReplaceMsgBoxResult.no
                                        Results_Business.Delete(newResult, True, Transact)
                                        NewResultStatus = InsertResult.NoRemplazado
                                    Case ReplaceMsgBox.ReplaceMsgBoxResult.noAll
                                        Results_Business.Delete(newResult, True, Transact)
                                        NewResultStatus = InsertResult.NoRemplazadoTodos
                                End Select
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsError, "Error en No Reemplazo " & ex.ToString())
                                ZClass.raiseerror(ex)
                                Throw ex
                            End If
                        End If
                    Else
                        ZClass.raiseerror(ex)
                        Throw ex
                    End If
                End Try

                If newResult.Volume.ID <> 0 Then
                    Dim volFactory As New VolumesFactoryExt()
                    newResult.DISK_VOL_PATH = volFactory.GetVolumenPathByVolId(newResult.Volume.ID)
                    volFactory = Nothing
                Else
                    newResult.DISK_VOL_PATH = String.Empty
                End If

                If isVirtual = False Then
                    newResult.File = newResult.FullPath
                End If

                'LO ASOCIO AUTOMATICAMENTE A WORKFLOWS
                ResultsArray.Add(newResult)

                For Each wfId As String In WFIds
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Asociando a WF:" & wfId)
                    InsertedWFIdsAndInitialStepsAndTasksResults.Add(WFTaskBusiness.AddResultsToWorkFLow(ResultsArray, wfId, Transact))
                Next

                Zamba.Data.Results_Factory.InsertIndexerState(newResult.DocTypeId, newResult.ID, 0, Transact)


                'Aplicando cambios
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Inserción Finalizada. Realizando Commit...")
                If transaction Is Nothing Then Transact.Commit()
                ZTrace.WriteLineIf(ZTrace.IsWarning, "Inserción finalizada con éxito.")

            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsError, "Inserción finalizada con errores. Removiendo los cambios realizados...")
                If transaction Is Nothing AndAlso Not IsNothing(Transact.Transaction) AndAlso Transact.Transaction.Connection.State <> ConnectionState.Closed Then
                    Transact.Rollback()
                End If
                InsertedWFIdsAndInitialStepsAndTasksResults = Nothing
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cambios removidos con éxito. El estado del sistema ha vuelto a como era antes de la inserción.")
                If throwEx = False Then
                    If ex.Message.Contains("es obligatorio") Then
                        Return InsertResult.ErrorIndicesIncompletos
                    ElseIf ex.Message.Contains("no permite valores fuera de la lista") Then
                        Return InsertResult.ErrorIndicesInvalidos
                    Else
                        ZClass.raiseerror(ex)
                    End If
                    Return InsertResult.NoInsertado
                Else
                    ZClass.raiseerror(ex)
                    Throw ex
                End If
            Finally
                If transaction Is Nothing AndAlso Not IsNothing(Transact) Then
                    If Not IsNothing(Transact.Con) Then
                        Transact.Con.Close()
                        Transact.Con.dispose()
                        Transact.Con = Nothing
                    End If
                    Transact.Dispose()
                    Transact = Nothing
                End If
            End Try

            ' Se actualiza el U_TIME del usuario (un campo que indica el horario de cuando fue la última acción realizada por el usuario) y se 
            ' guarda su acción en la tabla USER_HST. 
            UserBusiness.Rights.SaveAction(newResult.ID, ObjectTypes.Documents, Zamba.Core.RightsType.Create, "Se crea el documento " & newResult.Name & "(" & newResult.ID & ")")
            If (OpenDocumentAfterInsert) Then
                RaiseEvent ResultInserted(DirectCast(newResult, Result), ParentTaskId)
            End If

            'Ejecuto los workflows
            'ML: Este metodo esta muy acoplado al formato del arraylist de tres dimensiones, habria que cambiarlo.
            Try
                If Not IsNothing(InsertedWFIdsAndInitialStepsAndTasksResults) Then
                    For Each A As ArrayList In InsertedWFIdsAndInitialStepsAndTasksResults
                        WFTaskBusiness.ExecuteCheckInRulesFromInsert(A, RefreshWFAfterInsert)
                    Next
                End If
            Catch ex As Exception
                'Si falla la entrada a WF lo inserto igual, pero hago raise del error
                ZClass.raiseerror(ex)
            End Try

            Return NewResultStatus
        Catch ex As Exception
            If throwEx = False Then
                If ex.Message.Contains("es obligatorio") Then
                    Return InsertResult.ErrorIndicesIncompletos
                ElseIf ex.Message.Contains("no permite valores fuera de la lista") Then
                    Return InsertResult.ErrorIndicesInvalidos
                Else
                    ZClass.raiseerror(ex)
                End If
                Return InsertResult.NoInsertado
            Else
                ZClass.raiseerror(ex)
                Throw ex
            End If
        End Try
    End Function
    '[sebastian] se hizo un overload para poder insertar los atributos de danone correctamente
    Public Shared Function InsertNew(ByRef newResult As NewResult, ByVal move As Boolean, Optional ByVal reIndexFlag As Boolean = False, Optional ByVal reemplazarFlag As Boolean = False, Optional ByVal showQuestions As Boolean = True, Optional ByVal isVirtual As Boolean = False, Optional ByVal isReplica As Boolean = False, Optional ByVal hasName As Boolean = False) As InsertResult
        Dim NewResultStatus As InsertResult = InsertResult.NoInsertado

        'Autocompleta los atributos si corresponde, Se subio en nivel la ejecucion porque quizas se utilizan valores de los atributos para autonombre
        AutocompleteIndexsNewDocument(newResult)

        newResult.Name = GetResultName(newResult)

        If isVirtual = False Or isReplica Then 'Intento cargar un volumen para el documento, si no se puede se genera una excepcion
            Results_Business.LoadVolume(newResult)
            newResult.NewFile = VolumesBusiness.VolumePath(newResult.Volume, newResult.DocType.ID) & "\" & newResult.ID & newResult.Extension
            newResult.OffSet = newResult.Volume.offset
            If hasName = False Then
                newResult.Doc_File = New FileInfo(newResult.NewFile).Name
            End If
        End If

        newResult.ID = CoreData.GetNewID(IdTypes.DOCID)
        newResult.IconId = Results_Business.GetFileIcon(newResult.File) 'Obtengo el icono

        If isVirtual = False Or isReplica Then 'Copio fisicamente el archivo
            'Si el volumen es de tipo DB lo codifica, caso contrario mueve el archivo fisico al servidor
            If newResult.Volume.Type = VolumeTypes.DataBase Then
                newResult.EncodedFile = FileEncode.Encode(newResult.FullPath) 'result.NewFile ?
            Else
                If move Then
                    Try
                        Results_Business.MoveFile(newResult)
                    Catch ex As Exception
                        Results_Business.copyFile(newResult)
                    End Try
                Else
                    Results_Business.copyFile(newResult)
                End If
            End If
        End If

        Try
            If isVirtual And isReplica = False Then
                newResult.Disk_Group_Id = 0
            Else
                newResult.Disk_Group_Id = newResult.Volume.ID
                newResult.DISK_VOL_PATH = newResult.Volume.path
            End If

            RegisterDocumentWithAlfanumericIndex(newResult, reIndexFlag, Membership.MembershipHelper.CurrentUser.ID, isVirtual)
            'Llamo al metodo que se fijara si debe realizar una relacion segun la dependencia de atributos
            'CheckForIndexsRelations(newResult)
            NewResultStatus = InsertResult.Insertado
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al guardar la doc i Buscando si es un reemplazo " & ex.ToString)

            If ex.ToString.ToLower.IndexOf("unique") <> -1 OrElse ex.ToString.ToLower.IndexOf("unica") <> -1 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "UNIQUE DETECTADO")
                If reemplazarFlag Then
                    Try
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reemplazo detectado")

                        Results_Business.Delete(newResult, True)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento Borrado")
                        RegisterDocumentWithAlfanumericIndex(newResult, reIndexFlag, Membership.MembershipHelper.CurrentUser.ID, isVirtual)
                        '                        Results_Business.ReplaceDocument(newResult)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento Reemplazado")
                        NewResultStatus = InsertResult.Remplazado
                    Catch exep As Exception
                        NewResultStatus = InsertResult.ErrorReemplazar
                        Throw New Exception("Error al Reemplazar el documento. " & exep.Message)
                    End Try
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se hara reemplazo")
                    If showQuestions Then
                        'TODO : MsgBox en Business?
                        Select Case ReplaceMsgBox.Show("Los datos ingresados no son únicos. ¿Desea reemplazar el documento existente?", "Insertar Documento")
                            Case ReplaceMsgBox.ReplaceMsgBoxResult.yes
                                Try
                                    Results_Business.Delete(newResult, False)
                                    Results_Business.ReplaceDocument(newResult)
                                    NewResultStatus = InsertResult.Remplazado
                                Catch exe As Exception
                                    MessageBox.Show("Ocurrió un error al reemplazar el documento", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    NewResultStatus = InsertResult.ErrorReemplazar
                                    Throw exe
                                End Try
                            Case ReplaceMsgBox.ReplaceMsgBoxResult.yesAll
                                Try
                                    Results_Business.Delete(newResult, False)
                                    Results_Business.ReplaceDocument(newResult)
                                    NewResultStatus = InsertResult.RemplazadoTodos
                                Catch exe As Exception
                                    'TODO : MsgBox en Business?
                                    MessageBox.Show("Ocurrió un error al reemplazar el documento", "ZAMBA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    NewResultStatus = InsertResult.RemplazadoTodos
                                    Throw exe
                                End Try
                            Case ReplaceMsgBox.ReplaceMsgBoxResult.no
                                Try
                                    Results_Business.Delete(newResult, True)
                                    NewResultStatus = InsertResult.NoRemplazado
                                Catch exc As Exception
                                    NewResultStatus = InsertResult.ErrorReemplazar
                                End Try
                            Case ReplaceMsgBox.ReplaceMsgBoxResult.noAll
                                Try
                                    Results_Business.Delete(newResult, True)
                                    NewResultStatus = InsertResult.NoRemplazadoTodos
                                Catch exc As Exception
                                    NewResultStatus = InsertResult.ErrorReemplazar
                                End Try
                        End Select
                    Else
                        Try
                            newResult.ID = 0
                            InsertDocument(newResult, move, reIndexFlag, reemplazarFlag, showQuestions, isVirtual)
                        Catch exc As Exception
                            NewResultStatus = InsertResult.NoInsertado
                            Throw ex
                        End Try
                    End If
                End If
            Else
                NewResultStatus = InsertResult.NoInsertado
                Throw ex
            End If
        End Try

        If move AndAlso NewResultStatus <> InsertResult.NoInsertado Then
            Try
                File.Delete(newResult.File)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If

        newResult.DISK_VOL_PATH = VolumesBusiness.GetVolume(newResult.Volume.ID).path ' Hernan

        If isVirtual = False Then
            newResult.File = newResult.FullPath
        End If

        Try

            UserBusiness.Rights.SaveAction(newResult.ID, ObjectTypes.Documents, Zamba.Core.RightsType.Create, "Se crea el documento " & newResult.Name & "(" & newResult.ID & ")")
            RaiseEvent ResultInserted(DirectCast(newResult, NewResult), 0)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try 'LO ASOCIO AUTOMATICAMENTE A WORKFLOWS
            Dim ResultsArray As New ArrayList(1)
            ResultsArray.Add(newResult)

            Dim WFIds As ArrayList = DocTypesBusiness.GetDocTypeWfIds(newResult.DocType.ID)

            For Each wfId As String In WFIds
                'RaiseEvent AddResult2Wf(ResultsArray, wfId)
                'La anterior línea se cambia por:
                AdjuntarAWF(ResultsArray, wfId)

            Next

            If (WFIds.Count > 0) Then
                RaiseEvent updateWFs()
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Zamba.Data.Results_Factory.InsertIndexerState(newResult.DocTypeId, newResult.ID, Nothing)


        Return NewResultStatus

    End Function
    Public Shared Function findIn(ByVal Indexs As List(Of IIndex), ByVal pIndex As Core.Index) As Core.Index
        For i As Int16 = 0 To Indexs.Count - 1
            If Indexs.Item(i).ID = pIndex.ID Then
                pIndex.Data = Indexs.Item(i).Data
                pIndex.DataTemp = Indexs.Item(i).DataTemp
                If Indexs.Item(i).DropDown = IndexAdditionalType.AutoSustitución _
                    Or Indexs.Item(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                    pIndex.dataDescription = Indexs.Item(i).dataDescription
                    pIndex.dataDescriptionTemp = AutoSubstitutionBusiness.getDescription(Indexs.Item(i).DataTemp, Indexs.Item(i).ID, False, Indexs.Item(i).Type)
                    pIndex.DropDown = Indexs.Item(i).DropDown
                End If
                pIndex.Type = Indexs.Item(i).Type
                Return pIndex
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Método utilizado para actualizar la inserción de un documento
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <param name="move"></param>
    ''' <param name="ReindexFlag"></param>
    ''' <param name="Reemplazar"></param>
    ''' <param name="showQuestions"></param>
    ''' <param name="IsVirtual"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	11/09/2008	Modified    Se agrego el evento que permite refrescar el o los workflows
    ''' </history>
    Public Shared Function UpdateInsert(ByRef Result As NewResult, ByVal move As Boolean, Optional ByVal ReindexFlag As Boolean = False, Optional ByVal Reemplazar As Boolean = False, Optional ByVal showQuestions As Boolean = True, Optional ByVal IsVirtual As Boolean = False, Optional ByVal addToWF As Boolean = True) As InsertResult

        Dim insertresult As InsertResult = InsertResult.NoInsertado

        'Obtengo el auto nombre
        Try
            Dim autonamecode As String = ZCore.GetDocTypeAutoName(Result.DocType.ID)
            If Result.Indexs.Count > 0 Then
                Result.Name = DocTypesBusiness.GetAutoName(autonamecode, DocTypesBusiness.GetDocTypeName(Result.DocType.ID, True), Result.CreateDate, Result.EditDate, Result.Indexs).Trim
            End If
        Catch ex As Exception
            Result.Name = Result.DocType.Name
            ZClass.raiseerror(ex)
        End Try


        'busco lugar en algún volumen

        'Intento cargar un volumen para el documento, si no se puede se genera una excepcion
        If IsVirtual = False Then
            Results_Business.LoadVolume(Result)
            Result.NewFile = VolumesBusiness.VolumePath(Result.Volume, Result.DocType.ID) & "\" & Result.ID & Result.Extension
            Result.OffSet = Result.Volume.offset
            Result.Doc_File = New FileInfo(Result.NewFile).Name
        End If

        'obtengo el último doc_ID
        If Result.ID = 0 Then Result.ID = CoreData.GetNewID(IdTypes.DOCID)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "ID=" & Result.ID)

        'Obtengo el icono
        Result.IconId = Results_Business.GetFileIcon(Result.File)
        'copio fisicamente el archivo
        Try
            If IsVirtual = False Then
                If Not IsNothing(Result.EncodedFile) Then
                    Result.EncodedFile = FileEncode.Encode(Result.FullPath)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento codificado con exito")
                Else
                    If move = False Then
                        Results_Business.copyFile(Result)
                    Else
                        Try
                            Results_Business.MoveFile(Result)
                        Catch ex As Exception
                            Results_Business.copyFile(Result)
                        End Try
                    End If
                End If
            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al copiar el documento")
            ZClass.raiseerror(ex)
        End Try

        'guardo la doc t
        Try
            UpdateRegisterDocument(Result, ReindexFlag, IsVirtual)
            'guardo la doc i
            '            SaveIndexData(Result, ReindexFlag)
            insertresult = InsertResult.Insertado
            If IsVirtual = False Then
                Result.Disk_Group_Id = Result.Volume.ID
            Else
                Result.Disk_Group_Id = 0
            End If

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al guardar la doc i Buscando si es un reemplazo " & ex.ToString)

            If ex.ToString.ToLower.IndexOf("unique") <> -1 OrElse ex.ToString.ToLower.IndexOf("unica") <> -1 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "UNIQUE DETECTADO")
                If Reemplazar = True Then
                    Try
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reemplazo detectado")
                        Results_Business.Delete(Result, False)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento Borrado")
                        Results_Business.ReplaceDocument(Result)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento Reemplazado")
                        insertresult = InsertResult.Remplazado
                    Catch exep As Exception
                        insertresult = InsertResult.ErrorReemplazar
                        Throw New Exception("Error al Reemplazar el documento. " & exep.Message)
                    End Try
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se hara reemplazo")
                    If showQuestions = True Then
                        Select Case ReplaceMsgBox.Show("Los datos ingresados no son únicos. ¿Desea reemplazar el documento existente?", "Insertar Documento")
                            Case ReplaceMsgBox.ReplaceMsgBoxResult.yes
                                Try
                                    Results_Business.Delete(Result, False)
                                    Results_Business.ReplaceDocument(Result)
                                    insertresult = InsertResult.Remplazado
                                Catch exe As Exception
                                    MessageBox.Show("Ocurrió un error al reemplazar el documento", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    insertresult = InsertResult.ErrorReemplazar
                                    Throw exe
                                End Try
                            Case ReplaceMsgBox.ReplaceMsgBoxResult.yesAll
                                Try
                                    Results_Business.Delete(Result, False)
                                    Results_Business.ReplaceDocument(Result)
                                    insertresult = InsertResult.RemplazadoTodos
                                Catch exe As Exception
                                    MessageBox.Show("Ocurrió un error al reemplazar el documento", "ZAMBA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    insertresult = InsertResult.RemplazadoTodos
                                    Throw exe
                                End Try
                            Case ReplaceMsgBox.ReplaceMsgBoxResult.no
                                Try
                                    Results_Business.Delete(Result, True)
                                    insertresult = InsertResult.NoRemplazado
                                Catch exc As Exception
                                    insertresult = InsertResult.ErrorReemplazar
                                End Try
                            Case ReplaceMsgBox.ReplaceMsgBoxResult.noAll
                                Try
                                    Results_Business.Delete(Result, True)
                                    insertresult = InsertResult.NoRemplazadoTodos
                                Catch exc As Exception
                                    insertresult = InsertResult.ErrorReemplazar
                                End Try
                        End Select
                    Else
                        Try
                            Result.ID = 0
                            InsertDocument(Result, move, ReindexFlag, Reemplazar, showQuestions, IsVirtual)
                        Catch exc As Exception
                            insertresult = InsertResult.NoInsertado
                            Throw ex
                        End Try
                    End If
                End If
            Else
                insertresult = InsertResult.NoInsertado
                Throw ex
            End If
        End Try

        If move = True AndAlso insertresult <> InsertResult.NoInsertado Then
            Try
                File.Delete(Result.File)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If
        Result.DISK_VOL_PATH = VolumesBusiness.GetVolume(Result.Volume.ID).path ' Hernan

        If IsVirtual = False Then
            Result.File = Result.FullPath
        End If

        Try
            RaiseEvent ResultInserted(DirectCast(Result, NewResult), 0)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Try
            If addToWF = True Then
                'LO ASOCIO AUTOMATICAMENTE A WORKFLOWS
                Dim A As New ArrayList
                A.Add(Result)

                Dim WFIds As ArrayList = DocTypesBusiness.GetDocTypeWfIds(Result.DocType.ID)

                For Each wfId As String In WFIds
                    AdjuntarAWF(A, wfId)
                Next

                If (WFIds.Count > 0) Then
                    RaiseEvent updateWFs()
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return insertresult
    End Function

    Public Shared Sub AdjuntarAWF(ByVal Results As ArrayList, ByVal WFID As Int64)
        Dim WF As WorkFlow
        WF = WFBusiness.GetWFbyId(WFID)
        WFTaskBusiness.AddResultsToWorkFLow(Results, WF)
    End Sub

    Private Shared Sub RegisterDocumentWithAlfanumericIndex(ByRef result As NewResult, ByVal reIndexFlag As Boolean, ByVal userid As Int64, Optional ByVal isVirtual As Boolean = False)
        'Valido que se hayan cargado todos los atributos obligatorios
        If CheckRequiredIndexs(result) Then
            Results_Factory.RegisterDocumentWithAlfanumericIndex(result, reIndexFlag, isVirtual)

        End If
    End Sub
    Private Shared Sub RegisterDocumentWithAlfanumericIndex(ByRef result As NewResult, ByVal reIndexFlag As Boolean, ByVal isVirtual As Boolean, ByRef Transact As Transaction)
        Results_Factory.RegisterDocumentWithAlfanumericIndex(result, reIndexFlag, isVirtual, Transact, Membership.MembershipHelper.CurrentUser.ID)
    End Sub
    Private Shared Sub UpdateRegisterDocument(ByRef Result As NewResult, ByVal reIndexFlag As Boolean, Optional ByVal isvirtual As Boolean = False)
        If CheckRequiredIndexs(Result) Then
            Results_Factory.UpdateRegisterDocument(Result, reIndexFlag, isvirtual)
        End If

    End Sub
    Public Shared Function GetIndexData(ByVal docTypeId As Int64, ByVal DocId As Int32) As DataSet
        Return Results_Factory.GetIndexData(docTypeId, DocId)
    End Function
    Public Shared Function GetIndexSchema(ByVal docTypeId As Int64) As DataSet
        Return Results_Factory.GetIndexSchema(docTypeId)
    End Function

#End Region

#Region "Waiting Documents"

    'Obtiene el campo DocTypes de la TablaZWFI asociados a ese Id de regla
    Public Shared Function GetDocTypesZWFI(ByVal ruleID As Int64) As Generic.List(Of Int64)

        Dim ds As New DataSet()
        Dim resultsDTID As New Generic.List(Of Int64)

        ds = Results_Factory.GetDocTypesZWFI(ruleID)

        If Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) Then
            If ds.Tables(0).Rows.Count > 0 Then

                For Each row As DataRow In ds.Tables(0).Rows
                    If Not resultsDTID.Contains(Convert.ToInt64(row("DTID"))) Then
                        resultsDTID.Add(Convert.ToInt64(row("DTID")))
                    End If
                Next

            End If
        End If

        Return resultsDTID

    End Function

    'Obtiene los WI asociados al Id de regla pasado por parámetro en la tabla ZWFI
    Public Shared Function GetWIFromZWFI(ByVal ruleID As Int64) As Generic.List(Of Int64)

        Dim ds As New DataSet
        Dim resultsWI As New Generic.List(Of Int64)

        Try

            ds = Results_Factory.GetWIFromZWFI(ruleID)

            If Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In ds.Tables(0).Rows
                    resultsWI.Add(Convert.ToInt64(row("WI")))
                Next
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return resultsWI

    End Function

    'Obtiene el valor de un Atributo de una Tabla DOC_I y de un Id de Documento.
    Public Shared Function GetIndexValueFromDoc_I(ByVal docType As Int64, ByVal docID As Int64, ByVal indexID As Int64) As String
        Return Results_Factory.GetIndexValueFromDoc_I(docType, docID, indexID)
    End Function

    'Obtiene el valor de un Atributo en la tabla ZWFII
    Public Shared Function GetIndexValueFromZWFII(ByVal wI As Int64, ByVal indexID As Int64) As String
        Return Results_Factory.GetIndexValueFromZWFII(wI, indexID)
    End Function

    'Obtiene todos los Id de Documento asociados a un DocType en la tabla ZI
    Public Shared Function GetDocIDsFromZI(ByVal docType As Int64) As Generic.List(Of Int64)

        Dim ds As New DataSet
        Dim resultDocIDs As New Generic.List(Of Int64)

        Try

            ds = Results_Factory.GetZIbyDocType(docType)

            If Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) Then
                If ds.Tables(0).Rows.Count > 0 Then

                    For Each row As DataRow In ds.Tables(0).Rows
                        resultDocIDs.Add(Convert.ToInt64(row("DocID")))
                    Next

                End If
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return resultDocIDs

    End Function


    'Public Shared Function GetInsertIDsFromZI(ByVal docType As Int64) As Generic.List(Of Int64)

    '    Dim ds As New DataSet
    '    Dim resultInsertIDs As New Generic.List(Of Int64)

    '    Try

    '        ds = Results_Factory.GetZIbyDocType(docType)

    '        If Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) Then
    '            If ds.Tables(0).Rows.Count > 0 Then

    '                For Each row As DataRow In ds.Tables(0).Rows
    '                    resultInsertIDs.Add(Convert.ToInt64(row("InsertID")))
    '                Next

    '            End If
    '        End If

    '    Catch ex As Exception
    '       ZClass.raiseerror(ex)
    '    End Try

    '    Return resultInsertIDs

    'End Function

    'Obtiene todos los Id de Atributos de la tabla ZWFII asociados a un WI
    Public Shared Function GetIndexIDsFromZWFII(ByVal wI As Int64) As List(Of Int64)

        Dim ds As New DataSet
        Dim resultIndexIDs As New Generic.List(Of Int64)

        Try

            ds = Results_Factory.GetZWFIIbyWI(wI)

            If Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) Then
                If ds.Tables(0).Rows.Count > 0 Then

                    For Each row As DataRow In ds.Tables(0).Rows
                        resultIndexIDs.Add(Convert.ToInt64(row("IID")))
                    Next

                End If
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return resultIndexIDs

    End Function

    ''' <summary>
    ''' Indexa el documento
    ''' </summary>
    ''' <param name="_result"></param>
    ''' <history>   Marcelo Modified 27/08/2009</history>
    ''' <remarks></remarks>


    'Borra de las tablas ZWFI & ZWFII los registros asociados a un WI
    Public Shared Sub DeleteWI(ByVal wI As Int64)

        DeleteFromZWFI(wI)
        DeleteFromZWFII(wI)

    End Sub

    Public Shared Sub DeleteFromZWFI(ByVal wI As Int64)
        Try
            Results_Factory.DeleteFromZWFI(wI)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub DeleteFromZWFII(ByVal wI As Int64)
        Try
            Results_Factory.DeleteFromZWFII(wI)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub DeleteFromZI(ByVal lngDocID As Int64)
        Try
            Results_Factory.DeleteFromZI(lngDocID)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    'Verifica si el Id de esa regla aparece en la tabla ZWFI
    '(si es TRUE es que está esperando a por documentos)
    Public Shared Function IsRuleWaitingDocument(ByVal ruleId As Int64) As Boolean

        Dim count As Int16

        Try
            count = Results_Factory.VerifyIfWaitingDocuments(ruleId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        If count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Shared Function GetWIFromZIWhereRuleID(ByVal lngRuleID As Int64) As List(Of Int64)
        Return Results_Factory.GetRuleIDsFromZIWhereInsertID(lngRuleID)
    End Function

    'Valida si el DocType pasado por parámetro aparece en la 
    'tabla ZI
    Public Shared Function ValidateIsDocTypeInZI(ByVal docType As Int64) As Boolean
        Try
            Return Results_Factory.ValidateIsDocTypeInZI(docType)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try
    End Function

    'Valida si para todos los WI de la regla hay documentos Insertados que cumplen
    'con los valores de Atributo esperados.
    Public Shared Function ValidateWI(ByVal ruleID As Int64) As Boolean

        Dim ds As New DataSet()
        Dim flagIsValid As Boolean = True

        Try

            ds = Results_Factory.GetWIFromZWFI(ruleID)

            If Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then

                For Each r As DataRow In ds.Tables(0).Rows

                    Dim wI As Int64
                    Dim docType As Int64

                    wI = Convert.ToInt64(r("WI"))
                    docType = Convert.ToInt64(r("DTID"))

                    If Not ValidateWI(wI, docType) Then
                        flagIsValid = False
                    Else
                        SendMailWaitingDocument(ruleID, wI)
                        DeleteWI(wI)
                    End If

                Next

            Else
                flagIsValid = False
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try

        Return flagIsValid

    End Function

    'Valida si para ese WaitID (WI) hay un documento Insertado con los valores
    'de Atributo esperados.
    Private Shared Function ValidateWI(ByVal wI As Int64, ByVal docType As Int64) As Boolean

        Dim docIDs As List(Of Int64)
        Dim indexIDs As List(Of Int64)

        Dim countDeberianSerCorrectos As Int16 = 0
        Dim countSonCorrectos As Int16 = 0

        Dim valueA As String = String.Empty
        Dim valueB As String = String.Empty

        Dim flagIsValid As Boolean = False

        Try

            'Obtengo todos los Id de Documentos que fueron insertados
            'con el tipo pasado por parámetro (docType)
            docIDs = GetDocIDsFromZI(docType)

            'Obtiene todos los atributos de ese WI en la tabla ZWFII
            indexIDs = GetIndexIDsFromZWFII(wI)

            'Después de la comparación Atributo a Atributo, deberían
            'ser correctos todos los atributos (index.Count)
            countDeberianSerCorrectos = indexIDs.Count


            'Para cada documento insertado se valida indice a indice si cumple 
            'con los valores de Atributo esperados
            For Each docID As Int64 In docIDs

                countSonCorrectos = 0

                For Each indexID As Int64 In indexIDs

                    valueA = GetIndexValueFromDoc_I(docType, docID, indexID)
                    valueB = GetIndexValueFromZWFII(wI, indexID)

                    If String.Compare(valueA, valueB) = 0 Then
                        countSonCorrectos += 1
                    End If

                Next

                'Si todas las comparaciones fueron correctas entonces
                'levanta la bandera.
                If countDeberianSerCorrectos = countSonCorrectos AndAlso countDeberianSerCorrectos <> 0 Then
                    flagIsValid = True
                    DeleteFromZI(docID)
                End If

            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try

        Return flagIsValid

    End Function

    'Valida si para ese WaitID (WI) hay un documento Insertado con los valores
    'de Atributo esperados.
    Private Shared Function ValidateWI2(ByVal wI As Int64, ByVal docType As Int64) As List(Of Int64)

        Dim docIDs As New List(Of Int64)
        Dim indexIDs As New List(Of Int64)
        Dim insertIDs As New List(Of Int64)

        Dim countDeberianSerCorrectos As Int16 = 0
        Dim countSonCorrectos As Int16 = 0

        Dim valueA As String = String.Empty
        Dim valueB As String = String.Empty

        Dim resultInsertIDs As New List(Of Int64)

        Try


            'Obtengo todos los Id de Documentos que fueron insertados
            'con el tipo pasado por parámetro (docType)
            docIDs = GetDocIDsFromZI(docType)

            'Obtiene todos los atributos de ese WI en la tabla ZWFII
            indexIDs = GetIndexIDsFromZWFII(wI)

            'Después de la comparación Atributo a Atributo, deberían
            'ser correctos todos los atributos (index.Count)
            countDeberianSerCorrectos = indexIDs.Count


            'Para cada documento insertado se valida indice a indice si cumple 
            'con los valores de Atributo esperados
            For Each docID As Int64 In docIDs

                countSonCorrectos = 0

                For Each indexID As Int64 In indexIDs

                    valueA = GetIndexValueFromDoc_I(docType, docID, indexID)
                    valueB = GetIndexValueFromZWFII(wI, indexID)

                    If String.Compare(valueA, valueB) = 0 Then
                        countSonCorrectos += 1
                    End If

                Next

                'Si todas las comparaciones fueron correctas entonces
                'levanta la bandera.
                If countDeberianSerCorrectos = countSonCorrectos AndAlso countDeberianSerCorrectos <> 0 Then
                    resultInsertIDs.AddRange(GetInsertIDsWhereDocID(docID))
                End If

            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
            resultInsertIDs = New List(Of Int64)
        End Try

        Return resultInsertIDs

    End Function

    Public Shared Function GetInsertIDsWhereDocID(ByVal lngDocID As Int64) As List(Of Int64)
        Dim ds As New DataSet()
        Dim insertIDs As New List(Of Int64)

        ds = Results_Factory.GetZIWhereDocID(lngDocID)

        If Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
            For Each r As DataRow In ds.Tables(0).Rows
                insertIDs.Add(Convert.ToInt64(r("InsertID")))
            Next
        End If

        Return insertIDs

    End Function

    '''<summary></summary>
    '''<history>[Alejandro]</history>
    '''<summary>Valida si la Regla está lista para ejecutarse o tiene algun WaitID en espera</summary>
    '''<history>[Alejandro]</history>
    Public Shared Function IsRuleWaitForDocumentReady(ByVal ruleID As Int64) As Boolean

        Dim ds As New DataSet()
        Dim flagIsValid As Boolean = True
        Dim insertIDs As New List(Of Int64)

        Try

            ds = Results_Factory.GetWIFromZWFI(ruleID)

            If Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then

                For Each r As DataRow In ds.Tables(0).Rows

                    Dim wI As Int64
                    Dim docType As Int64

                    wI = Convert.ToInt64(r("WI"))
                    docType = Convert.ToInt64(r("DTID"))

                    insertIDs = ValidateWI2(wI, docType)

                    If insertIDs.Count = 0 Then
                        flagIsValid = False
                    Else
                        For Each insertID As Int64 In insertIDs
                            If IsRuleWaiting(ruleID, insertID) Then
                                Results_Factory.SetRuleIDNotWaiting(ruleID, insertID)
                                SendMailWaitingDocument(ruleID, wI)
                                DeleteWI(wI)
                            Else
                                flagIsValid = False
                            End If
                        Next
                    End If
                Next

            Else
                flagIsValid = False
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try

        Return flagIsValid

    End Function


    Public Shared Function IsRuleWaiting(ByVal lngRuleID As Int64, ByVal lngInsertID As Int64) As Boolean
        Dim ruleIDs As New List(Of Int64)
        Try
            ruleIDs = Results_Factory.GetRuleIDsFromZIWhereInsertID(lngInsertID)

            If Not ruleIDs.Contains(lngRuleID) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Public Shared Function GetRuleIDWhereWI(ByVal lngWI As Int64) As Int64
        Dim ruleID As Int64 = -1
        Dim ds As New DataSet
        ds = Results_Factory.GetZWFIbyWI(lngWI)

        If Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
            For Each r As DataRow In ds.Tables(0).Rows
                ruleID = Convert.ToInt64(r("RuleID"))
                Return ruleID
            Next
        End If

        Return ruleID

    End Function



    'Esta Funcion se fija que haya algún WAIT ID esperando por ese DocType con esos 
    'IID(atributos) e IValue(Valores de atributos)::
    'en caso TRUE: Updatea la tabla ZWFI y le carga el valor InsertID y devuelve true.
    'en caso FALSE o Excepción: devuelve false.
    Public Shared Function AsociateIncomingResult(ByVal _InsertID As Int64, ByVal _DTID As Int64, ByVal _DocID As Int64, ByVal _IDate As Date, ByVal _IID As Int64(), ByVal _IValue As String()) As Boolean

        Dim _dt As New DataTable
        Dim _waitIDs As Int64() = {0}

        Try

            'Verifica si hay algún WI esperando por ese DocType
            _dt = Results_Factory.SelectWaitingDocTypeInZWFI(_DTID)

            If _dt.Rows.Count > 0 Then
                'Si lo hay valida los atributos
                _waitIDs = Results_Factory.ZWFIIValidation(_dt, _IID, _IValue)
            End If

            If _waitIDs.Length > 0 Then
                'Si los atributos son correctos inserta el InsertID
                Results_Factory.ZWFIUpdateInsertID(_waitIDs, _InsertID)
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return False

    End Function

    Public Shared Function BuildMailWaitingDocument(ByVal lngWaitID As Int64) As String

        Dim miList As List(Of String)

        Dim mensaje As New StringBuilder()

        miList = GetWaitingData(lngWaitID)

        Dim count As Int16

        Try
            If miList.Count <= 0 Then
                mensaje.Append("Un documento que estaba en espera ha ingresado.")
            ElseIf miList.Count = 1 Then
                mensaje.Append("El documento de tipo: '")
                mensaje.Append(miList(0))
                mensaje.Append("' que estaba en espera ha ingresado.")
            Else
                mensaje.Append("El documento de tipo '")
                mensaje.Append(miList(0))
                mensaje.Append("' con valores:")
                mensaje.AppendLine()

                For count = 1 To miList.Count - 1
                    mensaje.Append(miList(count))
                    mensaje.Append(",")
                    mensaje.AppendLine()
                Next

                mensaje.Append("ha ingresado.")
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return ("Un documento que estaba en espera ha ingresado.")
        End Try

        Return mensaje.ToString()

    End Function

    Private Shared Function SendMailWaitingDocument(ByVal lngRuleID As Int64, ByVal lngWaitID As Int64) As Boolean

        Dim listaDeMails As New List(Of String)

        Try
            Dim mensaje As String = BuildMailWaitingDocument(lngWaitID)
            listaDeMails = GetWatingDocumentMails(lngRuleID)
            'MessagesBusiness.SendMail(listaDeMails, "", "", "Documento Entrante", mensaje, False)
            MessagesBusiness.SendMail(listaDeMails.ToString, "", "", "Documento Entrante", mensaje, False, Nothing)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Function

    Public Shared Function GetWatingDocumentMails(ByVal lngRuleID As Int64) As List(Of String)

        Dim ds As New DataSet()
        Dim Correo As ICorreo

        Dim listaMails As New List(Of String)
        Dim userID As Int64
        Dim userIDsList As New List(Of Int64)


        Try
            ds = Results_Factory.GetWatingDocumentMails(lngRuleID)

            If Not IsDBNull(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then

                For Each r As DataRow In ds.Tables(0).Rows
                    If IsDBNull(r("ExtraData")) Then
                        If Not IsDBNull(r("UserId")) Then
                            userID = Convert.ToInt64(r("UserID"))
                            userIDsList.Add(userID)
                        End If
                    Else
                        listaMails.Add(r("ExtraData"))
                    End If
                Next

                For Each uID As Int64 In userIDsList

                    Correo = UserBusiness.Mail.FillUserMailConfig(uID)

                    If Not IsNothing(Correo) Then

                        listaMails.Add(Correo.Mail)

                    End If
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return listaMails

    End Function

    '''<summary>
    '''  Devuelve todos los datos de un ID de espera (WaitID)
    '''</summary>
    Private Shared Function GetWaitingData(ByVal lngWaitID As Int64) As List(Of String)

        Dim dsWaitIDs As New DataSet()
        Dim dsIndexs As New DataSet()

        Dim tempdocTypeId As Int64
        Dim tempDocTypeName As String = String.Empty

        Dim tempIndexID As Int64
        Dim tempIndexName As String = String.Empty
        Dim tempIndex As String = String.Empty
        Dim tempIndexValue As String = String.Empty

        Dim tempWaitId As Int64
        Dim tempWaitIdList As New List(Of String)

        Try
            dsWaitIDs = Results_Factory.GetZWFIbyWI(lngWaitID)

            If Not IsDBNull(dsWaitIDs) AndAlso Not IsNothing(dsWaitIDs.Tables(0)) AndAlso dsWaitIDs.Tables(0).Rows.Count > 0 Then

                For Each r As DataRow In dsWaitIDs.Tables(0).Rows

                    tempdocTypeId = Convert.ToInt32(r("DTID"))
                    tempDocTypeName = DocTypesBusiness.GetDocTypeName(tempdocTypeId, True)

                    tempWaitIdList = New List(Of String)
                    tempWaitId = Convert.ToInt64(r("WI"))

                    tempWaitIdList.Add(tempDocTypeName)

                    dsIndexs = Results_Factory.GetZWFIIbyWI(tempWaitId)

                    If Not IsDBNull(dsIndexs) AndAlso Not IsNothing(dsIndexs.Tables(0)) AndAlso dsIndexs.Tables(0).Rows.Count > 0 Then

                        For Each r2 As DataRow In dsIndexs.Tables(0).Rows

                            tempIndexID = Convert.ToInt64(r2("IID"))
                            tempIndexName = IndexsBusiness.GetIndexName(tempIndexID, True)
                            tempIndexValue = r2("IValue").ToString()

                            tempIndex = tempIndexName + ": " + tempIndexValue

                            tempWaitIdList.Add(tempIndex)

                        Next

                    End If

                Next

            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            tempWaitIdList = New List(Of String)
        End Try

        Return tempWaitIdList

    End Function


#End Region

#Region "CompleteDocument"
    Public Shared Sub aCompleteDocument(ByRef Document As Result)
        Results_Factory.aCompleteDocument(Document)
    End Sub

    Public Shared Sub CompleteDocument(ByRef _Result As Result, Optional ByVal inThread As Boolean = False)
        Dim dr As IDataReader = Nothing
        Dim con As IConnection = Nothing
        Try
            con = Server.Con
            dr = Results_Factory.CompleteDocument(_Result.ID, _Result.DocTypeId, _Result.Indexs, con)

            If dr.IsClosed = False Then
                While dr.Read
                    Try
                        _Result.Disk_Group_Id = dr.Item(1)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    Try
                        _Result.Platter_Id = dr.Item(2)
                    Catch ex As Exception
                    End Try
                    Try
                        If Not IsDBNull(dr.Item(4)) Then _Result.Doc_File = dr.Item(4)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    Try
                        _Result.OffSet = dr.Item(5)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    Try
                        _Result.Name = dr.Item(7)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    Try
                        _Result.IconId = dr.Item(8)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    Try
                        If IsDBNull(dr.Item(9)) = False Then
                            If CInt(dr.Item(9)) = 1 Then
                                _Result.isShared = True
                            Else
                                _Result.isShared = False
                            End If
                        Else
                            _Result.isShared = False
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    Try
                        _Result.ParentVerId = dr.Item(10)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    Try
                        _Result.HasVersion = dr.Item(11)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    Try
                        _Result.RootDocumentId = dr.Item(12)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    Try
                        _Result.OriginalName = dr.Item(13)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    Try

                        If (IsDBNull(dr.Item(14))) Then
                            _Result.VersionNumber = 0
                        Else
                            _Result.VersionNumber = dr.Item(14)
                        End If

                    Catch ex As Exception
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try
                    Try
                        If IsDBNull(dr.Item(15)) = False Then
                            _Result.Disk_Group_Id = dr.Item(15)
                        Else
                            _Result.Disk_Group_Id = 0
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    Try
                        If IsDBNull(dr.Item(16)) = False Then
                            _Result.DISK_VOL_PATH = dr.Item(16)
                        Else
                            _Result.DISK_VOL_PATH = String.Empty
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    Try
                        _Result.UserID = _Result.Platter_Id
                        _Result.OwnerID = _Result.Platter_Id
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    If Not IsDBNull(dr.GetValue(17)) Then
                        If Not IsNothing(dr.GetValue(17)) Then
                            If Not String.IsNullOrEmpty(dr.GetValue(17).ToString()) Then
                                Try
                                    If Not IsNothing(_Result) Then
                                        _Result.CreateDate = DateTime.Parse(dr.GetValue(17).ToString())
                                    End If
                                Catch ex As Exception
                                    ZClass.raiseerror(ex)
                                End Try
                            End If
                        End If
                    End If

                    Dim i As Int16
                    For i = 0 To _Result.Indexs.Count - 1
                        Try
                            If Not IsDBNull(dr.GetValue(dr.GetOrdinal("I" & DirectCast(_Result.Indexs(i), Index).ID))) Then
                                DirectCast(_Result.Indexs(i), Index).Data = dr.GetValue(dr.GetOrdinal("I" & DirectCast(_Result.Indexs(i), Index).ID)).ToString
                                DirectCast(_Result.Indexs(i), Index).DataTemp = dr.GetValue(dr.GetOrdinal("I" & DirectCast(_Result.Indexs(i), Index).ID)).ToString
                                'Si el atributo es de tipo Sustitucion
                                If DirectCast(_Result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustitución _
                                    Or DirectCast(_Result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                    'Se carga la descripcion de Atributo
                                    DirectCast(_Result.Indexs(i), Index).dataDescription = AutoSubstitutionBusiness.getDescription(DirectCast(_Result.Indexs(i), Index).Data, DirectCast(_Result.Indexs(i), Index).ID, inThread, DirectCast(_Result.Indexs(i), Index).Type)
                                    DirectCast(_Result.Indexs(i), Index).dataDescriptionTemp = DirectCast(_Result.Indexs(i), Index).dataDescription
                                End If
                            Else
                                DirectCast(_Result.Indexs(i), Index).Data = String.Empty
                                DirectCast(_Result.Indexs(i), Index).DataTemp = String.Empty
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Next
                End While
            End If
        Finally
            If Not IsNothing(dr) Then
                If dr.IsClosed = False Then
                    dr.Close()
                End If
                dr.Dispose()
                dr = Nothing
            End If
            If Not IsNothing(con) Then
                con.Close()
                con.dispose()
                con = Nothing
            End If
        End Try
    End Sub
    Public Shared Function GetName(ByVal ResultId As Int64, ByVal DocTypeId As Int64) As String
        Dim selstr As String
        selstr = Results_Factory.GetName(ResultId, DocTypeId)
        Return selstr
    End Function
    Public Shared Function GetFullName(ByVal resultId As Int64, ByVal docTypeId As Int64) As String
        Return Results_Factory.GetFullName(resultId, docTypeId)
    End Function
#End Region

#Region "GetNewResult"

    Public Shared Function GetNewResult(ByVal docTypeId As Int64, ByVal File As String, Optional ByVal loadVolume As Boolean = True) As NewResult
        Dim CurrentDocType As DocType = DocTypesBusiness.GetDocType(docTypeId, True)
        Return GetNewNewResult(CurrentDocType, , File, loadVolume)
    End Function
    Public Shared Function GetNewNewResult(ByVal docId As Int64, ByVal docType As DocType) As NewResult
        Dim Result As New NewResult()
        Result.ID = docId
        Result.DocType = docType
        If Not Result.Indexs.Count > 0 Then
            LoadIndexs(DirectCast(Result, Result))
        End If
        CompleteDocument(Result)
        Return Result
    End Function
    Public Shared Function GetNewResult(ByVal docId As Int64, ByVal docType As DocType) As Result
        Dim Result As New Result()
        Result.ID = docId
        Result.DocType = docType
        LoadIndexs(DirectCast(Result, Result))
        CompleteDocument(Result)
        Return Result
    End Function

    ''' <summary>
    ''' Obtiene un result
    ''' </summary>
    ''' <param name="docId"></param>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetResult(ByVal docId As Int64, ByVal docTypeId As Int64) As Result
        Dim Result As New Result()
        Result.ID = docId
        Result.DocType = DocTypesBusiness.GetDocType(docTypeId, True)
        LoadIndexs(DirectCast(Result, Result))
        CompleteDocument(Result)
        Return Result
    End Function

    Public Shared Function GetResults(ByVal DocTypeId As Integer) As DataTable
        Return Results_Factory.GetResults(DocTypeId)
    End Function

    ''' <summary>
    ''' Obtiene los ids de los documentos a actualizar el nombre
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <param name="days"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAutoNameResults(ByVal DocTypeId As Integer, ByVal days As Int32) As DataTable
        Return Results_Factory.GetAutoNameResults(DocTypeId, days)
    End Function

    Public Shared Function ValidateNewResult(ByVal DocTypeId As Integer, ByVal docid As Integer) As Boolean
        Return Results_Factory.ValidateNewResult(DocTypeId, docid)
    End Function

    Public Shared Function GetNewNewResult(ByVal docTypeId As Long) As NewResult
        Dim Result As New NewResult()
        Result.DocType = DocTypesBusiness.GetDocType(docTypeId, True)

        LoadIndexs(DirectCast(Result, NewResult))

        Return Result
    End Function

    ''' <summary>
    ''' Obtiene un datarow con todos los datos del documento
    ''' </summary>
    ''' <param name="docId">Id del documento</param>
    ''' <param name="docTypeId">Id del Entidad</param>
    ''' <returns>Datarow del documento</returns>
    ''' <history> Marcelo modified 20/08/2009 </history>
    ''' <remarks></remarks>
    Public Shared Function GetResultRow(ByVal docId As Int64, ByVal docTypeId As Int64) As DataRow
        Dim atributos As Index() = ZCore.FilterCIndex(docTypeId)
        Dim rowResult As DataRow = Nothing
        Dim restrictionKey As String = Membership.MembershipHelper.CurrentUser.ID & "-" & docTypeId
        Dim restrictionBuilder As New StringBuilder()
        Dim FlagCase As Boolean = Boolean.Parse(UserPreferences.getValue("CaseSensitive", Sections.UserPreferences, True))
        Dim indRestriction As List(Of IIndex)

        'Obtiene las restricciones
        If Cache.DocTypesAndIndexs.hsRestrictionsIndexs.ContainsKey(restrictionKey) = False Then
            Cache.DocTypesAndIndexs.hsRestrictionsIndexs.Add(restrictionKey, RestrictionsMapper_Factory.getRestrictionIndexs(Membership.MembershipHelper.CurrentUser.ID, docTypeId))
        End If
        indRestriction = Cache.DocTypesAndIndexs.hsRestrictionsIndexs.Item(restrictionKey)

        'Contruye la consulta SQL de restriccion
        For i As Int32 = 0 To indRestriction.Count - 1
            Search.ModDocuments.CreateWhereSearch(New StringBuilder, indRestriction, i, FlagCase, restrictionBuilder, True, New StringBuilder, False)
        Next

        rowResult = Results_Factory.CompleteDocument(docId, docTypeId, atributos, False, restrictionBuilder.ToString)
        restrictionBuilder = Nothing
        indRestriction = Nothing

        If rowResult IsNot Nothing Then
            For Each indice As IIndex In atributos
                'Valido si el indice pertenece a la coleccion de columnas antes de agregarlo.
                If (indice.DropDown = IndexAdditionalType.AutoSustitución _
                    OrElse indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico) AndAlso (Not rowResult.Table.Columns.Contains(indice.Name)) Then
                    rowResult.Table.Columns.Add(indice.Name)
                    If Not IsDBNull(rowResult.Item("I" & indice.ID)) Then
                        rowResult.Item(indice.Name) = AutoSubstitutionBusiness.getDescription(rowResult.Item("I" & indice.ID), indice.ID, False, indice.Type)
                        'r.Table.Columns.Remove(r.Table.Columns("I" & indice.ID))
                    Else
                        rowResult.Item(indice.Name) = String.Empty
                    End If
                End If
            Next
        End If
        Return rowResult
    End Function
#End Region

#Region "Tasks"
    Public Shared Function GetNewTaskResult(ByVal DocId As Int64, ByVal DocType As DocType) As TaskResult
        Dim Result As New TaskResult
        Result.ID = DocId
        Result.DocType = DocType
        Dim i As Int16
        For i = 0 To Result.DocType.Indexs.Count - 1
            Result.Indexs.Add(Result.DocType.Indexs(i))
        Next
        CompleteDocument(DirectCast(Result, ITaskResult), False)
        Return Result
    End Function
#End Region

#Region "Volume"
    Public Shared Sub LoadVolume(ByRef result As NewResult)
        result.VolumeListId = VolumesBusiness.GetVolumeListId(result.DocType.ID)
        result.DsVols = VolumesBusiness.GetVolumes(result.VolumeListId)
        result.Volume = VolumesBusiness.LoadVolume(result.DocType.ID, result.DsVols)
    End Sub

#End Region

#Region "Delete"

    ''' <summary>
    ''' Borra un result
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <param name="delfile"></param>
    ''' <history>
    ''' 	[Marcelo]	22/05/2008	Modified
    '''</history>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByRef Result As Result, Optional ByVal delfile As Boolean = True, Optional ByVal saveAction As Boolean = True)
        If Result.Indexs.Count = 0 Then
            Result.Indexs = ZCore.FilterIndex(Result.DocType.ID)
        End If
        Results_Factory.Delete(Result, delfile)
        'added this call to delete the doc from zsearchValues_DT for Duke (sebastian 19-03-2009)
        Results_Business.DeleteSearchIndexData(Result.ID)
        If (saveAction = True) Then
            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(Result.ID, ObjectTypes.Documents, RightsType.Delete, "Se elimino el documento: " & Result.Name & "(" & Result.ID & ")")
        End If
    End Sub

    Public Shared Sub Delete(ByRef Result As NewResult, Optional ByVal delfile As Boolean = True)
        Results_Factory.Delete(Result, delfile)
    End Sub

    Public Shared Sub Delete(ByRef Result As NewResult, ByVal delfile As Boolean, ByRef t As Transaction)
        Results_Factory.Delete(Result, delfile, t)
    End Sub

    '''<summary>
    '''  Borra un result de un workflow
    '''</summary>
    Public Shared Sub DeleteResultFromWorkflows(ByVal docid As Int64)
        Results_Factory.DeleteResultFromWorkflows(docid)
    End Sub
#End Region

#Region "DOCFILE"
    Public Shared Sub updatedocfile(ByRef Result As Result, ByVal docfile As String)
        Results_Factory.updatedocfile(Result, docfile)
    End Sub
    ''' <summary>
    ''' [Sebastian 28-07-2009] Se agrego validacion para la ruta del archivo en caso de que venga vacia
    ''' </summary>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    Public Shared Sub copyFile(ByRef result As NewResult)
        ' Dim IR As InsertResult
        Dim fi As FileInfo = Nothing

        'Puede ya estar tomado. Validarlo

        Try
            'Diego: Para version automatica tiene que usar el path del documento temporal
            fi = New FileInfo(result.File)
            result.OriginalName = result.File

            If fi.Exists Then

                'Si es web
                'Se agrego esta validación porque en este renglón ocurrio una "exception".
                If String.IsNullOrEmpty(result.NewFile) = False Then

                    fi.CopyTo(result.NewFile, True)

                End If

                If String.Compare(fi.Extension, ".html") = 0 Or String.Compare(fi.Extension, ".htm") = 0 Then
                    'fi.CopyTo(result.NewFile, True)
                    'If result.FlagCopyVerify = True Then
                    '    'Dim NewFi As New FileInfo(result.NewFile)
                    '    'If NewFi.Exists Then
                    '    '    TODO hacer algo cuando tengo el ok de copia
                    '    '    result.FlagCopyDone = True
                    '    'Else
                    '    '    result.FlagCopyDone = False
                    '    '    TODO ver que hago si no la copio por algo
                    '    'End If
                    '    'NewFi = Nothing
                    'End If

                    InsertWebDocument(fi, New FileInfo(result.NewFile))
                Else
                    'Sino....
                    Try
                        If fi.Directory.Exists = False Then
                            fi.Directory.Create()
                        End If

                        'fi.CopyTo(result.NewFile, True)
                        'If result.FlagCopyVerify = True Then
                        '    Dim NewFi As New FileInfo(result.NewFile)
                        '    If NewFi.Exists Then
                        '        'TODO hacer algo cuando tengo el ok de copia
                        '        '	Result.FlagCopyDone = True
                        '    Else
                        '        '		Result.FlagCopyDone = False
                        '        'TODO ver que hago si no la copio por algo
                        '    End If
                        '    NewFi = Nothing
                        'End If

                    Catch ex As Exception
                        Throw New ArgumentException("No se tiene acceso al archivo " & result.NewFile & ". Motivo del Error : " & ex.Message)
                    End Try
                End If
            Else
                Throw New Exception("El Archivo origen no existe o no se puede acceder a el, verifique la existencia del mismo: " & fi.FullName)
            End If
        Finally
            fi = Nothing
        End Try
    End Sub
    Private Shared Sub MoveFile(ByRef newResult As NewResult)
        Dim fi As FileInfo = Nothing
        Try
            fi = New FileInfo(newResult.File)
            If fi.Exists Then
                If fi.Extension = ".html" Or fi.Extension = ".htm" Then
                    InsertWebDocument(fi, New FileInfo(newResult.NewFile))
                End If
                fi.MoveTo(newResult.NewFile)
            Else
                Throw New Exception("El Archivo origen no existe o no se puede acceder a el, verifique la existencia del mismo: " & fi.FullName)
            End If
        Finally
            fi = Nothing
        End Try
    End Sub

    Public Shared Sub SetVersionComment(ByVal rID As Int64, ByVal rComment As String)
        Results_Factory.SetVersionComment(rID, rComment)
    End Sub
    Public Shared Sub ReplaceFile(ByRef Result As Result, ByVal NewDocumentFile As String)
        Results_Factory.ReplaceFile(Result, NewDocumentFile)
        Results_Factory.InsertIndexerState(Result.DocTypeId, Result.ID, 0, Nothing)

    End Sub
    Public Shared Sub ReplaceDocument(ByRef Result As Result, ByVal NewDocumentFile As String, ByVal ComeFromWF As Boolean)
        Dim Fi As New IO.FileInfo(Result.FullPath)
        Dim NewFi As New IO.FileInfo(NewDocumentFile)
        Dim NewName As String = Results_Factory.MakefileName()
        Dim NewFullFileName As String = Fi.DirectoryName & "\" & NewName & NewFi.Extension
        NewFi.CopyTo(NewFullFileName, True)
        Result.Doc_File = NewName & NewFi.Extension
        Result.IconId = Results_Business.GetFileIcon(Result.File)
        Results_Factory.ReplaceDocument(Result, Result.Doc_File, ComeFromWF)
        Results_Factory.InsertIndexerState(Result.DocTypeId, Result.ID, 0, Nothing)

    End Sub
    Public Shared Sub ReplacePhysicalDocument(ByVal r As Result, ByVal newfile As String)
        File.Copy(newfile, r.FullPath, True)
    End Sub
    Public Shared Sub ReplaceDocument(ByRef Result As NewResult)
        Results_Factory.ReplaceDocument(Result)
        Results_Factory.InsertIndexerState(Result.DocTypeId, Result.ID, 0, Nothing)

    End Sub
    Public Shared Sub ReplaceDocument(ByRef Result As NewResult, ByRef t As Transaction)
        Results_Factory.ReplaceDocument(Result, t)
        Results_Factory.InsertIndexerState(Result.DocTypeId, Result.DocTypeId, 0, Nothing)

    End Sub

    ''' <summary>
    ''' Estable el nuevo id del icono para el documento
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <param name="docId"></param>
    ''' <param name="newIconID"></param>
    ''' <remarks></remarks>
    Public Shared Sub setIconID(ByVal docTypeId As Int64, ByVal docId As Int64, ByVal newIconID As Int64)
        Results_Factory.setIconId(docTypeId, docId, newIconID)
    End Sub
#End Region

#Region "Versioning"

    Public Shared Function GetParentVersionId(ByVal DocTypeid As Int64, ByVal docid As Int64) As Int64
        Return Results_Factory.GetParentVersionId(DocTypeid, docid)
    End Function

    Public Shared Function CountChildsVersions(ByVal DocTypeid As Int64, ByVal parentid As Int64) As Int32
        Return Results_Factory.CountChildsVersions(DocTypeid, parentid)
    End Function

    Public Shared Function GetNewVersionID(ByVal RootId As Long, ByVal doctype As Int64, ByVal OriginalDocId As Int64) As Int32
        Dim id As Int32
        id = Results_Factory.GetNewVersionID(RootId, doctype, OriginalDocId)
        Return id
    End Function

    Private Shared Sub setParentVersion(ByVal docTypeId As Int64, ByVal docId As Int64)
        Results_Factory.setParentVersion(docTypeId, docId)
    End Sub

    Public Shared Function InsertNewVersion(ByVal OriginalResult As Result, ByVal Comment As String) As Result
        Dim ClonedResult As NewResult = CloneResult(OriginalResult, OriginalResult.FullPath, False)
        ClonedResult.Comment = Comment

        For index As Integer = 0 To ClonedResult.Indexs.Count - 1
            Dim value As Object = ClonedResult.Indexs.Item(index).Data
            ClonedResult.Indexs.Item(index).DataTemp = value
        Next

        InsertDocument(ClonedResult, False, False, False, False, False)


        OriginalResult.HasVersion = 1

        setParentVersion(OriginalResult.DocType.ID, OriginalResult.ID)

        SaveVersionComment(ClonedResult)
        ' las nuevas versiones herendan de sus padres los usuarios a notificar de todos los rubros
        InheritUsersToNotify(OriginalResult.ID, ClonedResult.ID)

        Return ClonedResult

    End Function

    Public Shared Function InsertNewVersionNoComment(ByVal OriginalResult As Result) As Result
        Dim ClonedResult As NewResult = CloneResult(OriginalResult, OriginalResult.FullPath, False)

        'ClonedResult.Comment = Comment

        For index As Integer = 0 To ClonedResult.Indexs.Count - 1
            Dim value As Object = ClonedResult.Indexs.Item(index).Data
            ClonedResult.Indexs.Item(index).DataTemp = value
        Next

        InsertDocument(ClonedResult, False, False, False, False, False)


        OriginalResult.HasVersion = 1

        setParentVersion(OriginalResult.DocType.ID, OriginalResult.ID)

        ' las nuevas versiones herendan de sus padres los usuarios a notificar de todos los rubros
        InheritUsersToNotify(OriginalResult.ID, ClonedResult.ID)

        Return ClonedResult
    End Function

    Public Shared Sub InheritUsersToNotify(ByVal oldResultId As Int64, ByVal newResultId As Int64)
        Dim ds As DataSet = NotifyBusiness.GetAllData(oldResultId)
        If Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows

                'SaveAllData(ByVal doc_id As Int64, ByVal typeid As Int32, ByVal userid As Int64, ByVal extradata As String, ByVal groupid As Int64)
                NotifyBusiness.SaveAllData(newResultId, row.Item(0), row.Item(2), row.Item(3), row.Item(4))
            Next
        End If

    End Sub


    'Public Shared Function InsertNewVersion(ByVal OriginalResult As Result, ByVal Comment As String, ByVal newResultPath As String) As Result
    ' DIEGO: COMENTE ESTA SOBRECARGA PORQUE AL PARECER NO SE UTILIZA
    '    Dim ClonedResult As NewResult = CloneResult(OriginalResult, newResultPath, False)
    '    ClonedResult.Comment = Comment

    '    For index As Integer = 0 To ClonedResult.Indexs.Count - 1
    '        Dim value As Object = ClonedResult.Indexs.Item(index).data
    '        ClonedResult.Indexs.Item(index).dataTemp = value
    '    Next

    '    InsertDocument(ClonedResult, False, False, False, False, False)

    '    OriginalResult.HasVersion = 1

    '    setParentVersion(OriginalResult.DocType.ID, OriginalResult.ID)

    '    SaveVersionComment(ClonedResult)
    '    Return ClonedResult

    'End Function

    Public Shared Function InsertNewVersionNoComment(ByVal OriginalResult As Result, ByVal newResultPath As String) As Result
        Dim ClonedResult As NewResult = CloneResult(OriginalResult, newResultPath, False)
        'ClonedResult.Comment = Comment

        For index As Integer = 0 To ClonedResult.Indexs.Count - 1
            Dim value As Object = ClonedResult.Indexs.Item(index).Data
            ClonedResult.Indexs.Item(index).DataTemp = value
        Next

        InsertDocument(ClonedResult, False, False, False, False, False)

        OriginalResult.HasVersion = 1

        setParentVersion(OriginalResult.DocType.ID, OriginalResult.ID)

        ' las nuevas versiones herendan de sus padres los usuarios a notificar de todos los rubros
        InheritUsersToNotify(OriginalResult.ID, ClonedResult.ID)
        Return ClonedResult

    End Function

    Public Shared Sub SaveVersionComment(ByRef Result As NewResult)
        Results_Factory.SaveVersionComment(Result)
    End Sub

    Public Shared Sub SaveVersionComment(ByVal ResultID As Int64, ByVal ResultComment As String)
        Results_Factory.SaveVersionComment(ResultID, ResultComment)
    End Sub

    Public Shared Function GetVersionComment(ByVal ResultId As Int64) As String
        Return Results_Factory.GetVersionComment(ResultId)
    End Function

    Public Shared Function GetVersionCommentDate(ByVal ResultId As Int64) As String
        Return Results_Factory.GetVersionCommentDate(ResultId)
    End Function

    Public Shared Function PublishableResult(ByVal result As Result) As PublishableResult
        Dim PublishResult As New PublishableResult
        PublishResult.PublishId = CoreBusiness.GetNewID(IdTypes.Publish)
        PublishResult.Publisher = Membership.MembershipHelper.CurrentUser
        PublishResult.DocId = result.ID
        PublishResult.PublishDate = Now.Date
        PublishResult.DocType = result.DocType
        PublishResult.DocType.ID = result.DocType.ID
        PublishResult.Indexs = result.Indexs
        PublishResult.ID = result.ID
        PublishResult.CreateDate = result.CreateDate
        PublishResult.EditDate = result.EditDate
        Return PublishResult
    End Function

    Public Shared Sub SavePublish(ByVal result As Result, ByVal Publishresult As PublishableResult)
        Dim hash As Hashtable
        'look for data
        hash = IndexsBusiness.GetIndexByPublishStates(Publishresult)
        'update indexs
        If hash.Count > 0 Then
            Dim indexsToModify As New Generic.List(Of Int64)
            For Each indice As Index In Publishresult.Indexs
                If hash.ContainsKey(Int32.Parse(indice.ID.ToString())) Then
                    indexsToModify.Add(indice.ID)
                    indice.DataTemp = hash.Item(Int32.Parse(indice.ID.ToString()))
                    indice.Data = hash.Item(Int32.Parse(indice.ID.ToString()))
                End If
            Next
            Dim rstBuss As New Results_Business()
            If indexsToModify.Count > 0 Then
                rstBuss.SaveModifiedIndexData(DirectCast(Publishresult, Result), True, True, indexsToModify)
            Else
                rstBuss.SaveModifiedIndexData(DirectCast(Publishresult, Result), True, True)
            End If
            rstBuss = Nothing
            Results_Factory.SavePublish(Publishresult.PublishId, Publishresult.DocId, Publishresult.Publisher.ID, Publishresult.PublishDate)
        End If
    End Sub

    Public Shared Function GetPublishEvents() As DataSet
        Return Results_Factory.GetPublishEvents
    End Function

    Public Shared Function GetPublishEventsIds(ByVal EventName As String) As Int32
        Return Results_Factory.GetPublishEventsIds(EventName)
    End Function


    Public Shared Function GetPublishableIndexsStates(ByVal idDocType As Int64) As DataSet
        Try
            Return Results_Factory.GetPublishableIndexsStates(idDocType)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="dataid"></param>
    ''' <param name="DocTypeid"></param>
    ''' <param name="Indexid"></param>
    ''' <param name="eventId"></param>
    ''' <param name="DefValue"></param>
    ''' <param name="DocTypeName"></param>
    ''' <remarks></remarks>
    Public Shared Sub SavePublishableIndexsState(ByVal dataid As Int64, ByVal DocTypeid As Int64, ByVal Indexid As Int64, ByVal eventId As Int32, ByVal DefValue As String, ByVal DocTypeName As String)
        Try
            Results_Factory.SavePublishableIndexsState(dataid, DocTypeid, Indexid, eventId, DefValue)

            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(DocTypeid, ObjectTypes.Version, RightsType.EditVersions, "Usuario modifico los eventos para la entidad: " & DocTypeName)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub DeletePublishableIndexsState(ByVal DocTypeid As Int64, ByVal Indexid As Int64, ByVal eventId As Int32, ByVal DefValue As String, ByVal DocTypeName As String)
        Try
            Results_Factory.DeletePublishableIndexsState(DocTypeid, Indexid, DefValue)

            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(DocTypeid, ObjectTypes.Version, RightsType.EditVersions, "Usuario elimino eventos para la entidad: " & DocTypeName)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Shared Function ValidatePublishableIndexsStateExistance(ByVal DocTypeid As Int64, ByVal Indexid As Int64, ByVal eventId As Int32) As Boolean
        Dim count As Int32 = Results_Factory.ValidatePublishableIndexsStateExistance(DocTypeid, Indexid, eventId)
        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de documentos padres de un documento en la estructura de versionado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>Diego</history>
    Public Shared Function GetParentsCountForVersions(ByVal DocId As Int64, ByVal doctypeId As Int64) As Int32
        Return Results_Factory.GetParentsCountForVersions(DocId, doctypeId)
    End Function

#End Region

#Region "AutoName"

    Private Shared Function GetAutoNameCode(ByVal docTypeId As Int64) As String
        Dim strselect As String
        strselect = Results_Factory.GetAutoNameCode(docTypeId)
        Return strselect
    End Function
    Private Shared Sub SaveName(ByRef Result As Result)
        Results_Factory.SaveName(Result)
    End Sub
    ''' <summary>
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Ezequiel]	20/01/2009	Modified
    ''' </history>
    Public Shared Sub UpdateAutoName(ByRef Result As Result, Optional ByVal ChNomC As Boolean = False)
        Dim AutoNameCode As String
        Dim DocTypeName As String

        If Not Result.DocType Is Nothing AndAlso Result.DocType.AutoNameCode.Length = 0 Then
            AutoNameCode = DocTypesBusiness.GetDocType(Result.DocType.ID, True).AutoNameCode
        Else
            AutoNameCode = Result.DocType.AutoNameCode
        End If
        If Not Result.DocType Is Nothing AndAlso Result.DocType.Name.Length = 0 Then
            DocTypeName = DocTypesBusiness.GetDocTypeName(Result.DocType.ID, True)
        Else
            DocTypeName = Result.DocType.Name
        End If

        If Not Result.Indexs Is Nothing AndAlso Result.Indexs.Count > 0 Then
            If Not ChNomC OrElse Result.Name = String.Empty Then
                Result.Name = DocTypesBusiness.GetAutoName(AutoNameCode, DocTypeName, Result.CreateDate, Result.EditDate, Result.Indexs).Trim
                If Result.ID <> 0 Then Results_Factory.SaveName(Result)
            End If
        Else
            ZClass.raiseerror(New Exception("Los atributos no estan cargados para el result, no se ejecuta el autoname" + "  -  " + My.Application.Info.StackTrace.ToString()))
        End If

    End Sub

#End Region

#Region "Link"
    Public Shared Function GetLinkFromResult(ByRef Result As Result) As String
        Dim Link As String
        Link = "Zamba:\\DT=" & Result.DocType.ID & "&DOCID=" & Result.ID

        Return Link
    End Function

    Public Shared Function GetHtmlLinkFromResult(ByVal rDocTypeId As Int64, ByVal rId As Int64, Optional ByVal rName As String = "", Optional ByVal TypeMail As Boolean = False) As String
        Dim link As New StringBuilder

        If Not TypeMail = True Then
            link.Append("<a href=")
        End If
        link.Append(Chr(34))
        link.Append("zamba:\\DT=")
        link.Append(rDocTypeId)
        If TypeMail = True Then
            link.Append("&DOCID=")
        Else
            link.Append("&amp;DOCID=")
        End If
        link.Append(rId)
        link.Append(Chr(34))
        If Not TypeMail = True Then
            link.Append(">")
        End If
        If Not String.IsNullOrEmpty(rName) Then
            link.Append(rName)
        Else
            link.Append("Acceso al Documento")
        End If
        If Not TypeMail = True Then
            link.Append("</a>")
        End If
        Return link.ToString()

    End Function
#End Region

#Region "HTML Documents"
    'Funciones para Mover archivos HTML
    Private Shared Sub InsertWebDocument(ByVal forigen As FileInfo, ByVal fDestino As FileInfo)
        Dim dir As DirectoryInfo = forigen.Directory 'Obtengo el directorio origen donde esta el "html"
        'Obtengo el subdirectorio que tenga el nombredeorigen + "_"
        'Dim dirorigen As DirectoryInfo = dir.GetDirectories(forigen.Name.Split("."c)(0) & "_")(0)
        Dim subdirname As String = forigen.Name.Split(".html")(0)

        Try
            'valida si existe el primer elemento del metodo
            Dim SubDir As DirectoryInfo = dir.GetDirectories(subdirname)(0)


            'Formo el directorio destino con el path destino y el nombre del primer directorio
            Dim dirDest As New DirectoryInfo(fDestino.DirectoryName) '& "\" & SubDir.Name)
            CopySubDirAndFiles(SubDir, dirDest)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
    Private Shared Sub CopySubDirAndFiles(ByVal DirOrigen As DirectoryInfo, ByVal PathDestino As DirectoryInfo, Optional ByVal move As Boolean = False)
        If PathDestino.Exists = False Then
            PathDestino.Create()
        End If

        Dim DirDestino As New DirectoryInfo(PathDestino.FullName & "\" & DirOrigen.Name)

        If DirDestino.Exists = False Then
            DirDestino.Create()
        End If

        Dim archivos As FileInfo() = DirOrigen.GetFiles()
        Dim i As Integer

        For i = 0 To archivos.Length - 1
            archivos(i).CopyTo(DirDestino.FullName & "\" & archivos(i).Name, True)
        Next

        Dim SubDir As DirectoryInfo() = DirOrigen.GetDirectories()

        For i = 0 To SubDir.Length - 1
            CopySubDirAndFiles(SubDir(i), DirDestino, move)
        Next

    End Sub
    ''' <summary>
    ''' Copia los directorio de imagenes para los archivos html
    ''' </summary>
    ''' <param name="DirOrigen"></param>
    ''' <param name="PathDestino"></param>
    ''' <remarks></remarks>
    Public Shared Sub CopySubDirAndFilesBrowser(ByVal copyTo As String, ByVal copyFrom As String, ByVal originalPath As String)
        For Each folder As String In Directory.GetDirectories(copyFrom)
            If folder.Contains("_") Then
                If Directory.Exists(copyTo + folder.Replace(originalPath, "")) = False Then
                    Directory.CreateDirectory(copyTo + folder.Replace(originalPath, ""))
                End If
                For Each f As String In Directory.GetFiles(copyFrom + folder.Replace(originalPath, ""))
                    File.Copy(f, copyTo + f.Replace(originalPath, ""), True)
                Next
                CopySubDirAndFilesBrowser(copyTo, folder, originalPath)
            End If
        Next
    End Sub

#End Region

#Region "Creation Info"
    Public Shared Function GetCreatorUser(ByVal docid As Int64) As String
        'todo estaria bueno incluir esto en las DOC_T****
        Return Results_Factory.GetCreatorUser(docid)
    End Function

    Public Shared Function HasForms(ByVal Result As Result) As Boolean
        If FormBusiness.GetShowAndEditFormsCount(Result.DocType.ID) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function HasFormsExtended(ByVal Result As Result) As Boolean
        If FormBusiness.GetShowAndEditFormsCountExtended(Result.DocType.ID) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region
    '''<summary>
    '''  Actualiza un result Versionado, Actualiza el campo version a "0" y el ParentId que corresponda
    '''</summary>
#Region "Update Result"
    Public Shared Sub UpdateResultsVersionedDataWhenDelete(ByVal DocTypeid As Int64, ByVal parentid As Int64, ByVal docid As Int64, ByVal RootDocumentId As Int64)
        Results_Factory.UpdateResultsVersionedDataWhenDelete(DocTypeid, parentid, docid, RootDocumentId)
    End Sub

    Public Shared Sub UpdateLastResultVersioned(ByVal doctypeId As Int64, ByVal parentid As Int64)

        Results_Factory.UpdateLastResultVersioned(doctypeId, parentid)
    End Sub

    Public Shared Sub UpdateOriginalName(ByVal DocTypeId As Int64, ByVal DocId As Int64, ByVal strOriginalName As String)
        Results_Factory.UpdateOriginalName(DocTypeId, DocId, strOriginalName)

    End Sub

#End Region

    Public Shared Function getDate() As DateTime
        Dim fecha As DateTime = Zamba.Servers.Server.Con.ExecuteScalar(CommandType.Text, "Select getDate()")
        Return fecha
    End Function


    ''' <summary>
    ''' Obtiene la ruta fisica del archivo temporal de blob
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDBTempFile(ByVal Result As Result) As String
        If Result IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.FullPath) AndAlso Result.FullPath.IndexOf("aspx", StringComparison.CurrentCultureIgnoreCase) = -1 Then
            Dim fi As IO.FileInfo = Nothing
            Dim FTemp As IO.FileInfo = Nothing
            Dim dir As IO.DirectoryInfo

            Try
                fi = New IO.FileInfo(Result.FullPath)
                dir = GetTempDir("\OfficeTemp")
                If dir.Exists = False Then dir.Create()

                FTemp = New IO.FileInfo(dir.FullName & "\" & fi.Name)

                If FTemp.Exists = False Then
                    Results_Business.CopyFileToTemp(Result, fi.FullName, FTemp.FullName)
                    FTemp.Attributes = IO.FileAttributes.Normal
                End If
                If FTemp.Exists Then

                    Return FTemp.FullName
                Else
                    Return String.Empty
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            Finally
                'Libera recursos tomados
                If Not IsNothing(fi) Then fi = Nothing
                If Not IsNothing(FTemp) Then FTemp = Nothing
                If Not IsNothing(dir) Then dir = Nothing
            End Try
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Crea el archivo temporal que va a ser visualizado trayendolo desde el servidor
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [AlejandroR] 11/08/2010  Created
    ''' </history>
    Public Shared Function CreateTempFile(ByVal Result As Result) As String
        If Result IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.FullPath) AndAlso Result.FullPath.IndexOf("aspx", StringComparison.CurrentCultureIgnoreCase) = -1 Then
            Dim fi As IO.FileInfo = Nothing
            Dim FTemp As IO.FileInfo = Nothing
            Dim dir As IO.DirectoryInfo

            Try
                fi = New IO.FileInfo(Result.FullPath)
                dir = GetTempDir("\OfficeTemp")
                If dir.Exists = False Then dir.Create()

                If Result.IsExcel OrElse Result.IsWord Then
                    'Esto evita el error de abrir 2 excel con el mismo nombre (abrirlo en resultado y tareas)
                    FTemp = New IO.FileInfo(FileBusiness.GetUniqueFileName(dir.FullName, fi.Name))
                Else
                    FTemp = New IO.FileInfo(dir.FullName & "\" & fi.Name)
                End If

                Try
                    If FTemp.Exists = False Then
                        Results_Business.CopyFileToTemp(Result, fi.FullName, FTemp.FullName)
                    End If
                    FTemp.Attributes = IO.FileAttributes.Normal
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                If Result.FullPath.ToUpper.EndsWith(".HTML") Or Result.FullPath.ToUpper.EndsWith(".HTM") Then
                    Results_Business.CopySubDirAndFilesBrowser(dir.FullName, Result.FullPath.Remove(Result.FullPath.LastIndexOf("\")), Result.FullPath.Remove(Result.FullPath.LastIndexOf("\")))
                End If

                Return FTemp.FullName
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return String.Empty
            Finally
                'Libera recursos tomados
                If Not IsNothing(fi) Then fi = Nothing
                If Not IsNothing(FTemp) Then FTemp = Nothing
                If Not IsNothing(dir) Then dir = Nothing
            End Try
        Else
            Return String.Empty
        End If
    End Function

    Private Shared Function GetTempDir(ByVal dire As String) As IO.DirectoryInfo
        Dim Dir As IO.DirectoryInfo
        Try
            Dir = New IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\Zamba Software" & dire)
            If Dir.Exists = False Then
                Dir.Create()
            End If
        Catch
            Dir = New IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath & dire)
            If Dir.Exists = False Then
                Dir.Create()
            End If
        End Try
        Return Dir
    End Function

    ''' <summary>
    ''' Método genérico para traer un documento del servidor (físico o DB) al temporal del usuario.
    ''' </summary>
    ''' <param name="res">Result</param>
    ''' <param name="rootPath">Ruta de origen</param>
    ''' <param name="destPath">Ruta de destino</param>
    ''' <history>
    '''     [Tomas] 17/03/2011  Created
    ''' </history>
    ''' <remarks>   
    '''     Se encarga de realizar la migración de documentos físicos a 
    '''     DB en caso de que el volumen del documento sea de tipo DB.
    ''' </remarks>
    Public Shared Sub CopyFileToTemp(ByVal res As Result, ByVal rootPath As String, ByVal destPath As String)
        'Verifica si el volumen es de tipo base de datos o si todavia no se inserto el documento
        If (res.Disk_Group_Id <> 0 AndAlso ZCore.filterVolumes(res.Disk_Group_Id).Type = VolumeTypes.DataBase) OrElse File.Exists(res.FullPath) = False Then
            'traer el archivo desde la base
            LoadFileFromDB(res)

            'Verifica si se debe codificar (en caso de ser la primera vez en abrirse)
            If IsNothing(res.EncodedFile) Then
                'Copia del servidor el documento
                File.Copy(rootPath, destPath, True)
                'Lo codifica
                res.EncodedFile = FileEncode.Encode(destPath)
                'Se guarda en la base el archivo
                Results_Factory.InsertIntoDOCB(res, False)
            Else
                'Si el documento ya habia sido codificado, lo decodifica en la ruta de destino.
                FileEncode.Decode(destPath, res.EncodedFile)
            End If

            Dim duplicate As Boolean

            If Not ZOptBusiness.GetValue("DuplicateBlobAndVolumeFiles") Is Nothing Then
                duplicate = ZOptBusiness.GetValue("DuplicateBlobAndVolumeFiles")
            Else
                'Todo: ML cuando se solucione el tema de blob, se debe poner en false para no duplicar los archivos
                ZOptBusiness.InsertUpdateValue("DuplicateBlobAndVolumeFiles", True)
                duplicate = True
            End If

            If duplicate AndAlso File.Exists(res.FullPath) = False Then
                FileEncode.Decode(res.FullPath, res.EncodedFile)
            End If
        Else
            'Si el volumen es en disco rigido simplemente lo copia
            File.Copy(rootPath, destPath, True)
        End If
    End Sub

    Public Const _EXTMSG As String = ".msg"
    Public Const _EXTHTML As String = ".html"
    Public Const _EXTTXT As String = ".txt"



    Public Shared Function GetTempFileFromResult(localResult As IResult, GetPreview As Boolean) As String
        Dim tempPath, strPathLocal, tag, path As String
        Dim fi As FileInfo
        Dim fTemp As FileInfo
        Dim dir As DirectoryInfo

        path = localResult.FullPath

        Try
            'Se obtiene la ruta temporal del archivo a visualizar con el iframe
            fi = New FileInfo(path)
            dir = GetTempDir("\OfficeTemp")
            If Not dir.Exists Then dir.Create()
            fTemp = New FileInfo(dir.FullName & "\" & fi.Name)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        'Si el archivo es msg y tiene una copia en otro formato, se obtiene para visualizarlo
        If GetPreview AndAlso (localResult.IsMsg AndAlso
            (File.Exists(fTemp.FullName.ToLower().Replace(_EXTMSG, _EXTHTML)) OrElse
             File.Exists(fTemp.FullName.ToLower().Replace(_EXTMSG, _EXTTXT)) OrElse
             File.Exists(localResult.FullPath.ToLower().Replace(_EXTMSG, _EXTHTML)) OrElse
             File.Exists(localResult.FullPath.ToLower().Replace(_EXTMSG, _EXTTXT)))) Then

            tempPath = fTemp.FullName.ToLower()

            'Si la copia no se encuentra en los temporales se obtiene del servidor
            If Not File.Exists(tempPath.Replace(_EXTMSG, _EXTHTML)) AndAlso Not File.Exists(tempPath.Replace(_EXTMSG, _EXTTXT)) Then
                Try
                    If File.Exists(localResult.FullPath.ToLower().Replace(_EXTMSG, _EXTHTML)) Then
                        File.Copy(localResult.FullPath.ToLower().Replace(_EXTMSG, _EXTHTML), tempPath.Replace(_EXTMSG, _EXTHTML))
                    ElseIf File.Exists(localResult.FullPath.ToLower().Replace(_EXTMSG, _EXTTXT)) Then
                        File.Copy(localResult.FullPath.ToLower().Replace(_EXTMSG, _EXTTXT), tempPath.Replace(_EXTMSG, _EXTTXT))
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If

            'Obtiene la ruta local para visualizar con el iframe
            If File.Exists(tempPath.Replace(_EXTMSG, _EXTHTML)) Then
                path = tempPath.Replace(_EXTMSG, _EXTHTML)
            ElseIf File.Exists(tempPath.Replace(_EXTMSG, _EXTTXT)) Then
                path = tempPath.Replace(_EXTMSG, _EXTTXT)
            End If

        Else
            'Se hace la copia local para visualizarlo en el iframe
            Try
                If fTemp.Exists = False Then
                    Results_Business.CopyFileToTemp(localResult, fi.FullName, fTemp.FullName)
                End If
                fTemp.Attributes = FileAttributes.Normal
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            path = fTemp.FullName
        End If

        Return path
    End Function
    ''' <summary>
    ''' Valida si este este result ya esta asignado a otro WF
    ''' </summary>
    ''' <param name="resultID">ID del result a validar</param>
    ''' <returns>True si esta asignado, False si no</returns>
    ''' <remarks></remarks>
    Public Shared Function ExistsInOtherWFs(ByRef ResultID As Integer) As Boolean
        Try
            If ResultID = 0 Then Return False
            Dim resultCount As Object = Server.Con.ExecuteScalar(CommandType.Text, "SELECT count(1) FROM wfdocument WHERE doc_id = " & ResultID.ToString())
            Return CInt(resultCount) > 0
            resultCount = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Public Shared Function UpdateMonitorInsert(ByRef Result As NewResult, Optional ByVal Reemplazar As Boolean = False) As InsertResult
        Dim insertresult As InsertResult = InsertResult.NoInsertado

        'Intento cargar un volumen para el documento, si no se puede se genera una excepcion
        Results_Business.LoadVolume(Result)
        Result.NewFile = VolumesBusiness.VolumePath(Result.Volume, Result.DocType.ID) & "\" & Result.ID & Result.Extension
        Result.OffSet = Result.Volume.offset
        Result.Doc_File = New FileInfo(Result.NewFile).Name

        'Obtengo el auto nombre
        Try
            Dim autonamecode As String = ZCore.GetDocTypeAutoName(Result.DocType.ID)
            Result.Name = DocTypesBusiness.GetAutoName(autonamecode, DocTypesBusiness.GetDocTypeName(Result.DocType.ID, True), Result.CreateDate, Result.EditDate, Result.Indexs).Trim
        Catch ex As Exception
            Result.Name = Result.DocType.Name
            ZClass.raiseerror(ex)
        End Try

        'Obtengo el icono
        Result.IconId = Results_Business.GetFileIcon(Result.File)

        'Muevo fisicamente el archivo
        Try
            Results_Business.copyFile(Result)
        Catch ex As Exception
            ZClass.raiseerror(New Exception("No se pudo copiar el Resultado de Caratulas en Monitoreo porque " & ex.ToString))
            Throw ex
        End Try

        'guardo la doc t
        Try
            UpdateRegisterDocument(Result, False, False)
            insertresult = InsertResult.Insertado
            'todo Martin: para que usar una propiedad del result, usar solo la del volumen 
            Result.Disk_Group_Id = Result.Volume.ID
        Catch ex As Exception
            If ex.ToString.ToLower.IndexOf("unique") <> -1 OrElse ex.ToString.ToLower.IndexOf("unica") <> -1 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "UNIQUE DETECTADO")
                If Reemplazar = True Then
                    Try
                        Results_Factory.Delete(Result, False)
                        Results_Factory.ReplaceDocument(Result)
                        insertresult = InsertResult.Remplazado
                    Catch exep As Exception
                        insertresult = InsertResult.ErrorReemplazar
                        Throw New Exception("Error al Reemplazar el documento. " & exep.Message)
                    End Try
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se hara reemplazo")
                    Try
                        Result.ID = 0
                        InsertDocument(Result, True, False, Reemplazar, False, False)
                    Catch exc As Exception
                        insertresult = InsertResult.NoInsertado
                        Throw ex
                    End Try
                End If
            Else
                insertresult = InsertResult.NoInsertado
                Throw ex
            End If
        End Try

        'todo Martin: para que usar una propiedad del result, usar solo la del volumen 
        Result.DISK_VOL_PATH = VolumesBusiness.GetVolume(Result.Volume.ID).path ' Hernan
        Result.File = Result.FullPath

        Try
            RaiseEvent ResultInserted(DirectCast(Result, NewResult), 0)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return insertresult
    End Function

    ''' <summary>
    ''' Método que sirve para obtener los results relacionados
    ''' </summary>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	01/08/2008	Created
    ''' </history>
    Public Shared Sub getRelatedsResults(ByVal idDocSelected As Int64, ByRef relatedResultFinal As Result)

        Try

            ' Si el dataset que contiene las relaciones entre los docs esta vacío entonces traerlo
            If (dsRelateds Is Nothing) Then
                dsRelateds = Results_Factory.GetZDocRelations(dsRelateds)
            End If

            If (dsRelateds.Tables(0).Rows.Count > 0) Then

                ' Si el dataset que contiene los nombres de las relaciones esta vacío entonces traerlo
                If (dsRelations Is Nothing) Then
                    dsRelations = Results_Factory.GetDocRelations()
                End If

                ' Se obtiene el id raíz
                obtainIdRoot(idDocSelected, relatedResultFinal.ID)

                If (relatedResultFinal.ID = 0) Then
                    ' Se guarda el nombre del documento raíz
                    relatedResultFinal.Name = "Relaciones"
                End If

                createInstancesRelatedResult(relatedResultFinal)
            Else
                relatedResultFinal = Nothing
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método que sirve para obtener el id raíz
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="idRoot"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	04/08/2008	Created
    ''' </history>
    Private Shared Sub obtainIdRoot(ByVal id As Int64, ByRef idRoot As Int64)

        If (dsRelateds.Tables(0).Rows.Count > 0) Then

            Dim ban As Boolean = False
            Dim view As New DataView
            view.Table = dsRelateds.Tables(0)
            ' Obtener padre de Doc_Id seleccionado en el cliente
            view.RowFilter = "Doc_Id = " & id

            Dim idloop As Long = 300
            If (view.ToTable.Rows.Count > 0) Then

                While (ban = False)

                    If (view.ToTable.Rows.Count > 0) Then
                        If (idloop = idRoot) Then
                            ban = True
                        Else
                            idRoot = CType(view.ToTable.Rows(0).Item("ParentId"), Integer)
                            view.RowFilter = "Doc_Id = " & idRoot
                            idloop = idRoot
                        End If
                    Else
                        ban = True
                    End If
                End While
            Else
                idRoot = id
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método recursivo que sirve para crear las instancias RelatedResult de cada result que se encuentre en el 
    ''' dataset. A su vez, también se agregan los hijos del RelatedResult que se recibe como parámetro y así 
    ''' sucesivamente
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="relatedR"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	01/08/2008	Created
    '''                 04/08/2008  Modified
    ''' </history>
    Private Shared Sub createInstancesRelatedResult(ByRef relatedR As Result)

        ' Se buscan los hijos del relatedResult
        Dim childrens() As System.Data.DataRow = dsRelateds.Tables(0).Select("ParentId = " & relatedR.ID)
        relatedR.ChildsResults = New List(Of IResult)

        ' Se recorren los hijos del relatedResult
        For Each children As DataRow In childrens

            Dim result As New Result
            ' Se guarda el id del documento
            result.ID = children.Item("Doc_Id")
            ' Se guarda el nombre del documento
            'result.Name = Results_Factory.getNameDoc_Id(result.ID)
            Dim rowRelation() As System.Data.DataRow = dsRelations.Tables(0).Select("RelationId = " & children.Item("RelationId").ToString())
            ' Se guarda el nombre de la relación
            result.AutoName = rowRelation(0).Item("Name")
            createInstancesRelatedResult(result)
            relatedR.ChildsResults.Add(result)

        Next

    End Sub
#Region "Exportar a PDF"


    Public Shared Function exportarResultPDF(ByRef Result As Result, ByVal sPdf As String) As Boolean
        Try
            If Result.IsImage Then

                'TODO: Validar la ruta del archivo, si las carpetas no existen crearlas
                Dim fi As New IO.FileInfo(sPdf)
                If fi.Directory.Exists = False Then fi.Directory.Create()


                Dim sRuta As String
                Dim Doc As New ceTe.DynamicPDF.Document
                sRuta = Result.FullPath()
                'Si la imagen del result es un Tif requiere un tratamiento distinto a otras
                If sRuta.ToUpper.EndsWith(".TIF") OrElse sRuta.ToUpper.EndsWith(".TIFF") Then
                    Dim fTif As New ceTe.DynamicPDF.Imaging.TiffFile(sRuta)
                    Dim i, k As Int32
                    Dim Img As ceTe.DynamicPDF.PageElements.Image
                    k = fTif.Images.Count

                    'TODO HERNAN FIJATE Q ES ESTO
                    'If MessageBox.Show("La imagen contiene " & k & " paginas. ¿Desea exportarlas todas?", "Zamba - Exportación a PDF", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = System.Windows.Forms.DialogResult.No Then


                    'End If
                    For i = 0 To k - 1
                        Img = New ceTe.DynamicPDF.PageElements.Image(fTif.Images(i), 0, 0)
                        Img.SetDpi(72)
                        Dim Pag As New ceTe.DynamicPDF.Page
                        Dim Pd As New ceTe.DynamicPDF.PageDimensions(Img.Width, Img.Height)
                        Pag.Elements.Add(Img)
                        Pag.Dimensions = Pd
                        Pag.Dimensions.SetMargins(0)
                        Doc.Pages.Add(Pag)

                        'Doc.Pages.Add(pag)
                    Next
                Else
                    Dim Img As New ceTe.DynamicPDF.PageElements.Image(sRuta, 0, 0)
                    Img.SetDpi(72)
                    Dim Pag As New ceTe.DynamicPDF.Page
                    Dim Pd As New ceTe.DynamicPDF.PageDimensions(Img.Width, Img.Height)
                    Pag.Elements.Add(Img)
                    Pag.Dimensions = Pd
                    Pag.Dimensions.SetMargins(0)
                    Doc.Pages.Add(Pag)
                End If

                Try
                    Doc.Draw(sPdf)
                    Return True
                Catch
                    Return False
                End Try
            Else
                Return False
            End If
        Catch
            Return False
        End Try

    End Function
     
    Public Shared Function ConvertDocToPdfFile(ByRef Result As Result, ByVal PrinterName As String)

        Dim OfficeTemp As String
        Dim OfficeTempPDF As String
        Dim LocalDocFile As String
        Dim LocalDocFileToPrint As String
        Dim Resul As Boolean

        'hace una copia del archivo copiado en officetemp (por que puede estar tomado)
        OfficeTemp = Zamba.Tools.EnvironmentUtil.GetTempDir("\OfficeTemp").FullName
        OfficeTempPDF = OfficeTemp & "\PDFTemp"

        LocalDocFile = OfficeTemp & "\" & Result.Doc_File
        LocalDocFileToPrint = OfficeTemp & "\PDFTemp\§" & Result.Doc_File

        Try

            If Not Directory.Exists(OfficeTempPDF) Then
                Directory.CreateDirectory(OfficeTempPDF)
            End If

            If Not File.Exists(LocalDocFile) Then
                File.Copy(Result.FullPath, LocalDocFile)
            End If

            File.Copy(LocalDocFile, LocalDocFileToPrint, True)

            Dim word As New WordInterop

            Resul = word.Print(LocalDocFileToPrint, PrinterName)

            Application.DoEvents()

            If Resul Then

                'guardar datos en la tabla de exportacion
                Data.ExportFactory.InsertExportedPDF(Membership.MembershipHelper.CurrentUser.ID, Result.ID, Result.DocTypeId, Result.Doc_File)

            End If

            word = Nothing

            If File.Exists(LocalDocFileToPrint) Then
                File.Delete(LocalDocFileToPrint)
            End If

        Catch ex As Exception

            ZClass.raiseerror(ex)

        End Try

        Return Resul

    End Function

    ''' <summary>
    ''' Converts images to PDF Files
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <param name="pdfFolderPath"></param>
    ''' <param name="CantPdfs"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Tomas] - 27/04/2009 - Modified - Se mueve un bloque de código dentro de un catch que generaba error
    ''' [Tomas] - 19/05/2009 - Modified - Se implementa el método GetUniqueFileName al crear los pdfs.
    ''' </history>
    Public Shared Sub ConvertToPdfFile(ByRef Result As Result, ByVal pdfFolderPath As String, ByRef CantPdfs As Int32)
        If Not Result.IsImage And IsNothing(Result.Picture) Then
            Exit Sub
        End If

        Dim p As ceTe.DynamicPDF.Page
        '   Dim bm As System.Drawing.Bitmap
        Dim pe As ceTe.DynamicPDF.PageElements.Image
        Dim ps As ceTe.DynamicPDF.PageDimensions
        '     Dim i As Int32
        '    Dim iTotal As Int32
        Dim k As Int32
        Dim fTiff As ceTe.DynamicPDF.Imaging.TiffFile = Nothing
        '   Dim imgTiff As TiffImageData
        Dim FlagToJPG As Boolean = False
        Dim ArrayDeTemps As New ArrayList
        Dim aux As New ArrayList
        Dim Img As Drawing.Image = Nothing

        Dim PathFile As String = Result.FullPath

        'TODO: Buscar codigo de PDFCOnvert de Repsol con ultima version.
        Try

            If PathFile.ToUpper.EndsWith("TIF") Then

                Img = Drawing.Image.FromFile(PathFile)

                FlagToJPG = True
                Dim oFDimension As System.Drawing.Imaging.FrameDimension
                Dim iCount As Int32 = 0
                Dim actualFrame As Int32 = 0
                oFDimension = New System.Drawing.Imaging.FrameDimension(Img.FrameDimensionsList(actualFrame))
                iCount = Img.GetFrameCount(oFDimension)

                If iCount = 1 Then

                    'SI NO ES MULTITIFF

                    Dim NumMagicoW As Decimal
                    Dim NumMagicoH As Decimal

                    Dim ScaleXAnt As Int32
                    Dim ScaleYAnt As Int32

                    p = New ceTe.DynamicPDF.Page
                    pe = New ceTe.DynamicPDF.PageElements.Image(CStr(PathFile), 0.0, 0.0, 100)

                    ScaleXAnt = CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleX
                    ScaleYAnt = CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleY

                    CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleX = 1
                    CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleY = 1

                    Dim Img2 As Drawing.Image
                    Img2 = Drawing.Image.FromFile(PathFile)

                    NumMagicoW = Img2.HorizontalResolution / CType(pe, ceTe.DynamicPDF.PageElements.Image).HorizontalDpi
                    NumMagicoH = Img2.VerticalResolution / CType(pe, ceTe.DynamicPDF.PageElements.Image).VerticalDpi

                    CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleX = ScaleXAnt
                    CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleY = ScaleYAnt

                    pe.Width = Single.Parse(Img2.Width / NumMagicoW)
                    pe.Height = Single.Parse(Img2.Height / NumMagicoH)
                    p.Elements.Add(pe)
                    p.Dimensions.Width = Img2.Width / NumMagicoW
                    p.Dimensions.Height = Img2.Height / NumMagicoH
                    p.Dimensions.SetMargins(0, 0, 0, 0)

                    aux.Add(p)

                Else

                    'SI ES MULTITIFF LO HACE PARA CADA PAGINA

                    For j As Int32 = 0 To iCount - 1

                        oFDimension = New System.Drawing.Imaging.FrameDimension(Img.FrameDimensionsList(actualFrame))
                        Img.SelectActiveFrame(oFDimension, j)
                        Dim Path As String = New System.IO.FileInfo(PathFile).Directory.FullName & "\" & New System.IO.FileInfo(PathFile).Name.ToUpper.Replace(".TIF", "") & j & "_TEMPORAL.TIF"
                        Img.Save(Path)
                        ArrayDeTemps.Add(Path)

                        Dim NumMagicoW As Decimal
                        Dim NumMagicoH As Decimal

                        Dim ScaleXAnt As Int32
                        Dim ScaleYAnt As Int32

                        p = New ceTe.DynamicPDF.Page
                        pe = New ceTe.DynamicPDF.PageElements.Image(CStr(Path), 0.0, 0.0, 100)

                        ScaleXAnt = CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleX
                        ScaleYAnt = CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleY

                        CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleX = 1
                        CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleY = 1

                        Dim Img2 As Drawing.Image
                        Img2 = Drawing.Image.FromFile(Path)

                        NumMagicoW = Img2.HorizontalResolution / CType(pe, ceTe.DynamicPDF.PageElements.Image).HorizontalDpi
                        NumMagicoH = Img2.VerticalResolution / CType(pe, ceTe.DynamicPDF.PageElements.Image).VerticalDpi

                        CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleX = ScaleXAnt
                        CType(pe, ceTe.DynamicPDF.PageElements.Image).ScaleY = ScaleYAnt

                        pe.Width = Single.Parse(Img2.Width / NumMagicoW)
                        pe.Height = Single.Parse(Img2.Height / NumMagicoH)
                        p.Elements.Add(pe)
                        p.Dimensions.Width = Img2.Width / NumMagicoW
                        p.Dimensions.Height = Img2.Height / NumMagicoH

                        p.Dimensions.SetMargins(0, 0, 0, 0)

                        aux.Add(p)

                    Next

                End If

            Else

                Try
                    p = New ceTe.DynamicPDF.Page
                    pe = New ceTe.DynamicPDF.PageElements.Image(CStr(PathFile), 0.0, 0.0, 100)
                    Dim Img2 As Drawing.Image
                    Img2 = Drawing.Image.FromFile(PathFile)
                    pe.Width = Single.Parse((Img2.Width / Img2.HorizontalResolution) * 72)
                    pe.Height = Single.Parse((Img2.Height / Img2.VerticalResolution) * 72)
                    p.Elements.Add(pe)
                    p.Dimensions.Width = (Img2.Width / Img2.HorizontalResolution) * 72
                    p.Dimensions.Height = (Img2.Height / Img2.VerticalResolution) * 72
                    p.Dimensions.SetMargins(0, 0, 0, 0)
                    aux.Add(p)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

            End If

        Catch ex As Exception

            Try

                'TIRO ERROR O FORCED = TRUE
                Img = Drawing.Image.FromFile(PathFile)
                Dim Path As String = New System.IO.FileInfo(PathFile).Directory.FullName & "\" & New System.IO.FileInfo(PathFile).Name & "TEMP.TIF"
                ArrayDeTemps.Add(Path)
                If Img.HorizontalResolution = 0 OrElse Img.VerticalResolution = 0 Then
                End If
                Img.Save(Path, Drawing.Imaging.ImageFormat.Tiff)
                fTiff = New ceTe.DynamicPDF.Imaging.TiffFile(CStr(New System.IO.FileInfo(PathFile).Directory.FullName & "\" & New System.IO.FileInfo(PathFile).Name & "TEMP.TIF"))

            Catch ex2 As Exception

                Try
                    GC.Collect()
                    GC.WaitForPendingFinalizers()
                    If Img Is Nothing = False Then Img.Dispose()
                    Img = Nothing
                    Img = Drawing.Bitmap.FromFile(PathFile)
                    Dim Path As String = New System.IO.FileInfo(PathFile).Directory.FullName & "\" & New System.IO.FileInfo(PathFile).Name & "TEMP.TIF"
                    Try
                        If System.IO.File.Exists(Path) Then System.IO.File.Delete(Path)
                    Catch
                    End Try
                    ArrayDeTemps.Add(Path)
                    Img.Save(Path, Drawing.Imaging.ImageFormat.Tiff)
                    fTiff = New ceTe.DynamicPDF.Imaging.TiffFile(CStr(New System.IO.FileInfo(PathFile).Directory.FullName & "\" & New System.IO.FileInfo(PathFile).Name & "TEMP.TIF"))
                Catch exc As Exception
                    ZClass.raiseerror(exc)
                End Try

            Finally
                If FlagToJPG = False Then
                    If Not IsNothing(fTiff.Images) Then
                        For k = 0 To fTiff.Images.Count() - 1
                            Try
                                p = fTiff.Images(k).GetPage
                                pe = New ceTe.DynamicPDF.PageElements.Image(fTiff.Images(k), 0.0, 0.0)
                                ps = New ceTe.DynamicPDF.PageDimensions(pe.Width, pe.Height)
                                ps.SetMargins(0, 0, 0, 0)
                                p.Dimensions = ps
                                aux.Add(p)
                            Catch ex3 As Exception
                                ZClass.raiseerror(ex3)
                            End Try
                        Next
                    End If
                End If
                Img.Dispose()
                pe = Nothing
                ps = Nothing

            End Try
        End Try

        '[Tomas]    27/04/2009  Modified    Se comenta el siguiente código y se lo pone en el finally
        '                                   que se encuentra arriba, ya que la instanciación del objeto
        '                                   fTiff se realizaba unicamente en el catch y si el código no
        '                                   generaba exception no se instanciaba y luego si se generaba 
        '                                   exception al pasar por el código comentado.
        'If FlagToJPG = False Then
        '    If Not IsNothing(fTiff.Images) Then
        '        For k = 0 To fTiff.Images.Count() - 1
        '            Try
        '                p = fTiff.Images(k).GetPage
        '                pe = New ceTe.DynamicPDF.PageElements.Image(fTiff.Images(k), 0.0, 0.0)
        '                ps = New ceTe.DynamicPDF.PageDimensions(pe.Width, pe.Height)
        '                ps.SetMargins(0, 0, 0, 0)
        '                p.Dimensions = ps
        '                aux.Add(p)
        '            Catch ex As Exception
        '                ZClass.raiseerror(ex)
        '            End Try
        '        Next
        '    End If
        'End If
        'Img.Dispose()
        'pe = Nothing
        'ps = Nothing

        Try
            If aux.Count > 0 Then
                Dim Documento As New ceTe.DynamicPDF.Document
                For Each Pag As ceTe.DynamicPDF.Page In aux
                    If Not IsNothing(Pag) Then
                        Documento.Pages.Add(Pag)
                    End If
                Next

                Documento.Draw(FileBusiness.GetUniqueFileName(pdfFolderPath & "\", GetValidFileName(Result.Name), ".pdf"))
                CantPdfs += 1
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

#End Region


    Private Shared Function GetValidFileName(ByVal fileName As String) As String
        For Each invalidChar As Char In Path.InvalidPathChars
            If fileName.Contains(invalidChar) Then
                fileName = fileName.Replace(invalidChar, String.Empty)
            End If
        Next
        Return fileName.Replace(" ", "_").Replace(".", String.Empty).Replace(":", String.Empty).Replace("/", String.Empty).Replace("__", "_")
    End Function

    ''' <summary>
    ''' Insert Index's values on DataBase
    ''' </summary>
    ''' <param name="Word"></param>
    ''' <param name="IndexId"></param>
    ''' <param name="ResultId"></param>
    ''' <param name="DocTypeId"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Ezequiel] 18/03/09 - Created
    ''' </history>
    Public Shared Sub InsertSearchIndexData(ByVal result As IResult, Optional ByRef t As Transaction = Nothing)
        Dim datatoinsert As String
        For Each i As Index In result.Indexs
            If Not String.IsNullOrEmpty(i.Data.Trim) Then
                If i.DropDown <> IndexAdditionalType.AutoSustitución AndAlso i.DropDown <> IndexAdditionalType.AutoSustituciónJerarquico Then
                    For Each d As String In i.Data.Trim.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)
                        datatoinsert += "§" + d.Trim
                    Next
                    If Not String.IsNullOrEmpty(datatoinsert) AndAlso datatoinsert.Trim.Length > 1 Then
                        datatoinsert = ReplaceChar(datatoinsert)
                        datatoinsert = TextTools.ReemplazarAcentos(datatoinsert)
                        datatoinsert = datatoinsert.ToLower()
                        Results_Factory.InsertSearchIndexDataService(datatoinsert, result.DocType.ID, result.ID, i.ID, t)
                    End If
                Else
                    For Each d As String In i.dataDescription.Trim.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)
                        datatoinsert += "§" + d.Trim
                    Next
                    If Not String.IsNullOrEmpty(datatoinsert) AndAlso datatoinsert.Trim.Length > 1 Then
                        datatoinsert = ReplaceChar(datatoinsert)
                        datatoinsert = TextTools.ReemplazarAcentos(datatoinsert)
                        datatoinsert = datatoinsert.ToLower()
                        Results_Factory.InsertSearchIndexDataService(datatoinsert, result.DocType.ID, result.ID, i.ID, t)
                    End If

                End If
            End If
            datatoinsert = String.Empty
        Next
    End Sub



    ''' <summary>
    ''' Delete document's values from zsearchvalues_dt [sebastian 18-03-2009]
    ''' </summary>
    ''' <param name="Word"></param>
    ''' <param name="IndexId"></param>
    ''' <param name="ResultId"></param>
    ''' <param name="DocTypeId"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Tomas 18/03/09]    Created
    ''' </history>
    Public Shared Sub DeleteSearchIndexData(ByVal ResultId As Int64)
        Results_Factory.DeleteSearchIndexData(ResultId)
    End Sub

    Public Function IndexFile(ByVal filePath As String, docId As Integer, docTypeid As Integer) As Integer
        Dim body As String = String.Empty

        Try
            Dim res As IResult = GetResult(docId, docTypeid)
            Dim IsBlob As Boolean


            If filePath.Length > 0 AndAlso Not File.Exists(filePath) Then
                filePath = Results_Business.GetDBTempFile(res)
                If filePath.Length > 0 Then IsBlob = True
            End If

            If filePath.Length > 0 AndAlso File.Exists(filePath) Then
                Dim IndexResult As IndexedState = IndexFile(filePath, docId, docTypeid, body, res, IsBlob)
                Dim thumbGenerate As Boolean = GenerateThumbs(filePath, docId, docTypeid)
                ZTrace.WriteLineIf(ZTrace.IsVerbose, IIf(thumbGenerate, "Generacion thumb exitosa", "Error al generar thumb"))
                res.Dispose()
                res = Nothing
                If IsBlob Then
                    File.Delete(filePath)
                End If
                Return IndexResult
            Else
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "No se encuentra la ruta del archivo: " + filePath)
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "=======================================================================================================================")

                'Indexo el valor de los índices
                InsertSearchIndexData(res, Nothing)

                Return IndexedState.Indexado
            End If


        Catch ex As Exception
            ZClass.raiseerror(ex)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, Date.Now.ToString + " - Ocurrio un error al obtener el texto. " + ex.ToString())
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "=======================================================================================================================")
            Return IndexedState.Erroneo
        Finally
            body = Nothing
        End Try

    End Function

    Private Shared Function GenerateThumbs(filePath As String, docId As Integer, docTypeid As Integer) As Boolean
        Dim processState As Boolean
        If (Not File.Exists(filePath)) Then
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "No se encontro el archivo: " + filePath + " para la generacion de thumbs")
            Return processState
        End If

        Try
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Generando thumb de: " + filePath)
            Dim thumb As String = Thumbs.Base64.ConvertFromPath(filePath)
            If thumb IsNot String.Empty Then
                Dim exist As Boolean = Server.Con.ExecuteScalar(CommandType.Text, String.Format _
                    ("SELECT COUNT(1) FROM ZTHUMB WHERE DOC_TYPE_ID = {0} AND DOC_ID= {1}" _
                     , docTypeid, docId))
                If Not exist Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Insertando registro de thumb: docTypeid " & docTypeid & " docId " & docId)
                    If Server.isOracle Then

                        Dim insertZthumb As New StringBuilder()

                        Dim distThumb1 As String = thumb.Substring(0, 25000)
                        Dim distThumb2 As String = thumb.Substring(25001, 25000)
                        Dim distThumb3 As String = thumb.Substring(50001, thumb.Length - 50001)

                        insertZthumb.Append("DECLARE ")
                        insertZthumb.Append("ThumbClob CLOB;")
                        insertZthumb.Append("BEGIN ")
                        insertZthumb.Append("ThumbClob := ThumbClob || '" & distThumb1 & "';")
                        insertZthumb.Append("ThumbClob := ThumbClob || '" & distThumb2 & "' ;")
                        insertZthumb.Append("ThumbClob := ThumbClob || '" & distThumb3 & "' ;")
                        insertZthumb.Append("INSERT INTO ZTHUMB (DOC_TYPE_ID, DOC_ID,THUMB) VALUES(" & docTypeid & "," & docId & ", ThumbClob );")
                        insertZthumb.Append("End;")

                        Server.Con.ExecuteNonQuery(CommandType.Text, insertZthumb.ToString())

                    Else
                        processState = Server.Con.ExecuteNonQuery(CommandType.Text, String.Format _
                        ("INSERT INTO ZTHUMB (DOC_TYPE_ID, DOC_ID,THUMB) VALUES({0},{1},'{2}')" _
                         , docTypeid, docId, thumb))
                    End If
                Else
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Actualizando registro de thumb: docTypeid " & docTypeid & " docId " & docId)
                    processState = Server.Con.ExecuteNonQuery(CommandType.Text, String.Format _
                        ("UPDATE ZTHUMB SET THUMB = '{0}' WHERE DOC_TYPE_ID = {1} AND DOC_ID = {2}" _
                         , thumb, docTypeid, docId))
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return processState
    End Function

    Private Shared Function IndexFile(filePath As String, docId As Integer, docTypeid As Integer, ByRef body As String, ByRef res As IResult, IsBlob As Boolean) As IndexedState
        Dim extension As String = filePath.Remove(0, filePath.LastIndexOf("."))

        'Obtengo los tipos de documentos que no deben ser indexados
        Dim excludedTypes As String = ZOptBusiness.GetValue("IndexerExcludedTypes")
        If excludedTypes = Nothing Then excludedTypes = String.Empty

        If excludedTypes.Contains(extension) Then
            'No se indexa porque es un tipo excluido
            Return IndexedState.IndexadoNoRequerido
        Else
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "obteniendo texto del archivo:" + filePath)

            If extension.Contains(".doc") OrElse extension.Contains(".docx") OrElse extension.Contains(".dot") Then
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Procesando archivo Word")

                Dim st As New Zamba.FileTools.SpireTools()
                body = st.GetTextFromDoc(filePath)

                st = Nothing

            ElseIf extension.Contains(".xls") OrElse extension.Contains(".xlsx") Then
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Procesando archivo Excel")

                Dim st As New Zamba.FileTools.SpireTools()
                body = st.GetTextFromExcel(filePath)
                st = Nothing

            ElseIf extension.Contains(".pdf") Then
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Procesando archivo PDF")

                Dim ITS As New Zamba.FileTools.ITextSharp()
                body = ITS.GetTextFromPDF(filePath)
                ITS = Nothing

            ElseIf extension.Contains(".msg") Then
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Procesando archivo MSG")

                Dim interopB As New Zamba.FileTools.Interop()
                body = interopB.ExtractTextFromMsgAttach(filePath, body)
                interopB = Nothing

            ElseIf extension.Contains(".ppt") OrElse extension.Contains(".pps") Then
                ZTrace.WriteLineIf(ZTrace.IsVerbose, Date.Now.ToString + "Procesando archivo PPT")

                Dim interopB As New Zamba.FileTools.Interop()
                body = interopB.GetTextFromPPT(filePath)
                interopB = Nothing

            ElseIf extension.Contains(".txt") OrElse extension.Contains(".rtf") Then
                'Si no es Ninguno de los anteriores intento procesarlo de forma binaria
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Procesando archivo de forma binaria")
                Dim errors As Boolean = False
                Dim binaryB As New Zamba.FileTools.Binary()
                body = binaryB.GetFileText(filePath, errors)
                binaryB = Nothing
            Else
                Return IndexedState.MotorIndexadoNoDefinido
            End If

            Results_Factory.DeleteValuesZsearchValue_DT(docId)

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "texto obtenido para indexar:" + body)
            InsertSearchData(body, docId, docTypeid)

            'Indexo el valor de los índices
            InsertSearchIndexData(res, Nothing)

            Return IndexedState.Indexado
        End If
    End Function

    ''' Utilizado por el servicio SearchService, inserta el contenido 
    ''' de un archivo para utilizarlo por el proceso de indexacion
    Public Shared Function InsertSearchData(ByVal body As String, ByVal docID As Int64, ByVal docTypeID As Int64) As Boolean
        Dim data As String = String.Empty
        Try
            data = ReplaceChar(body)
            data = TextTools.ReemplazarAcentos(data)
            data = data.ToLower()

            While data.Contains("§§")
                data = Replace(data, "§§", "§")
            End While

            If body.Length > 0 Then
                Results_Factory.InsertSearchIndexDataService(data, docTypeID, docID, 0, Nothing)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            data = Nothing
        End Try

        Return True
    End Function

    'Reemplaza Espacios, enter y caracteres no alfanumericos por el siguiente caracter: §
    Public Shared Function ReplaceChar(body As String) As String
        Dim data As New StringBuilder
        _unicodeCategory = ZOptBusiness.GetValue("GetUnicodeCategory")

        Dim Letters As CharEnumerator = body.GetEnumerator

        While Letters.MoveNext
            If EvaluateIsNotValidChar(Letters.Current) Then
                If data.Length > 0 AndAlso data.Chars(data.Length - 1) <> "§" Then
                    data.Append("§")
                End If
            Else
                data.Append(Letters.Current)
            End If
        End While
        Return data.ToString
    End Function

    'Evalua si debe o no reemplazarse el caracter por §
    Public Shared Function EvaluateIsNotValidChar(chr As Char) As Boolean

        If Char.IsLetterOrDigit(chr) Then
            Dim currentUnicodeCategory As System.Globalization.UnicodeCategory = Char.GetUnicodeCategory(chr)

            If (_unicodeCategory <> String.Empty) AndAlso _unicodeCategory.Contains(currentUnicodeCategory) Then
                Return False
            ElseIf currentUnicodeCategory.LetterNumber OrElse currentUnicodeCategory.DecimalDigitNumber OrElse currentUnicodeCategory.UppercaseLetter OrElse currentUnicodeCategory.LowercaseLetter Then
                Return False
            Else
                Return True
            End If
        Else

            Select Case Asc(chr)
                'Case 13 'Espacio: Se reemplaza
                '    Return True
                'Case 32 'Enter: Se reemplaza
                '    Return True
                Case 47 '/: No se reemplaza (fecha)
                    Return False
                Case 92 '\: No se reemplaza (fecha)
                    Return False
                Case Else
                    Return True
            End Select

        End If

    End Function

    Public Function GetFullPath(docTypeId As Int64, docId As Int64) As String
        Return Results_Factory.getPathForIdTypeIdDoc(docTypeId, docId)
    End Function



End Class