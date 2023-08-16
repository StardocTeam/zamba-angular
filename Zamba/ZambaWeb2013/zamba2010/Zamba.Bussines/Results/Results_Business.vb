Imports Zamba.Core.WF.WF
Imports Zamba.Servers
Imports Zamba.Data
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports Zamba.Office
Imports Zamba.DataExt.WSResult.Consume
Imports Zamba.Membership
Imports Spire.Email
Imports Newtonsoft.Json

Public Class Results_Business
    Implements IResults_Business

    Private Shared _fileStream As FileStream = Nothing
    Private Shared _binaryWriter As BinaryWriter = Nothing
    Private Shared dsRelateds As DataSet = Nothing
    Private Shared dsRelations As DataSet = Nothing
    ' Evento que se ejecuta después de agregar el result (que se inserto con insertar documento) a las etapas iniciales de los correspondientes 
    ' workflow (sólo si el entidad está asociado a uno o más workflows)
    Public Shared Event updateWFs()

    Dim RF As New Results_Factory
    Dim RB As New RightsBusiness
    Dim UB As New UserBusiness
    Dim RMF As New RestrictionsMapper_Factory
    Dim WFTB As New WFTaskBusiness

    Friend Sub UpdateResultIcon(resultId As Int64, docTypeId As Int64, IconID As Int32)
        RF.UpdateFileIcon(resultId, docTypeId, IconID)
    End Sub

    Public Shared Function MakeTable(ByVal DocTypeId As Integer, ByVal TableType As TableType) As String
        Dim tables As String
        tables = Results_Factory.MakeTable(DocTypeId, TableType)
        Return tables
    End Function

#Region "Extensiones"
    'Funciones para manejar archivos de distintas extensiones
    Public Function GetFileIcon(ByVal File As String) As Int32 Implements IResults_Business.GetFileIcon
        Try
            If File IsNot Nothing Then
                Return GetIcon(New FileInfo(File.ToUpper).Extension)
            Else
                Return 30
            End If

        Catch
            Return 30
        End Try
    End Function
    Public Function GetIcon(ByVal Extension As String) As Int32 Implements IResults_Business.GetIcon
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
                Case ".PNG"
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
                    Return 6
                Case ".EML"
                    Return 6
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

    Public Function IsImage(ByVal Ext As Extensiones) As Boolean Implements IResults_Business.IsImage
        If Ext = Extensiones.TIF OrElse Ext = Extensiones.GIF OrElse Ext = Extensiones.JPG OrElse Ext = Extensiones.JPEG OrElse Ext = Extensiones.BMP OrElse Ext = Extensiones.IMG Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function IsImage(ByVal Ext As String) As Boolean Implements IResults_Business.IsImage
        Select Case Ext.ToUpper
            Case ".TIF", ".TIFF", ".GIF", ".JPG", ".JPEG", ".IMG", ".BMP"
                Return True
            Case "TIF", "TIFF", "GIF", "JPG", "JPEG", "IMG", "BMP"
                Return True
        End Select
        Return False
    End Function
    Public Function GetExtensionId(ByVal File As String) As Int32 Implements IResults_Business.GetExtensionId
        Dim Fi As New FileInfo(File)
        Dim Ext As String = Fi.Extension
        Return GetIcon(Ext)
    End Function
#End Region

#Region "GetResults"
    ''' <summary>
    ''' Creo el result a partir de la fila de la grilla
    ''' </summary>
    ''' <param name="_Result"></param>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Public Sub CompleteDocument(ByRef _Result As IResult, ByVal dr As DataRow) Implements IResults_Business.CompleteDocument
        Try
            If dr.Table.Columns.Contains("DISK_GROUP_ID") AndAlso Not IsDBNull(dr("DISK_GROUP_ID")) Then _Result.Disk_Group_Id = CInt(dr("DISK_GROUP_ID"))
            If dr.Table.Columns.Contains("PLATTER_ID") AndAlso Not IsDBNull(dr("PLATTER_ID")) Then _Result.Platter_Id = CInt(dr("PLATTER_ID"))
            If dr.Table.Columns.Contains("Doc_File") AndAlso Not IsDBNull(dr("Doc_File")) Then _Result.Doc_File = dr("Doc_File").ToString()
            If dr.Table.Columns.Contains("OFFSET") AndAlso Not IsDBNull(dr("OFFSET")) Then _Result.OffSet = CInt(dr("OFFSET"))
            If dr.Table.Columns.Contains("SHARED") AndAlso Not IsDBNull(dr("SHARED")) Then _Result.IsShared = IIf(CInt(dr("SHARED")) = 1, True, False)
            If dr.Table.Columns.Contains("ver_Parent_id") AndAlso Not IsDBNull(dr("ver_Parent_id")) Then _Result.ParentVerId = CInt(dr("ver_Parent_id"))
            If dr.Table.Columns.Contains("Version") AndAlso Not IsDBNull(dr("Version")) Then _Result.HasVersion = CInt(dr("Version"))
            If dr.Table.Columns.Contains("RootId") AndAlso Not IsDBNull(dr("RootId")) Then _Result.RootDocumentId = CInt(dr("RootId"))
            If dr.Table.Columns.Contains("Original") AndAlso Not IsDBNull(dr("Original")) Then _Result.OriginalName = dr("Original").ToString()
            If dr.Table.Columns.Contains("Numero de Version") AndAlso Not IsDBNull(dr("Numero de Version")) Then _Result.VersionNumber = CInt(dr("Numero de Version"))
            If dr.Table.Columns.Contains("Disk_vol_path") AndAlso Not IsDBNull(dr("Disk_vol_path")) Then _Result.DISK_VOL_PATH = dr("Disk_vol_path").ToString()
            _Result.UserId = _Result.Platter_Id
            _Result.OwnerID = _Result.Platter_Id
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        If dr.Table.Columns.Contains(dr("Creado")) Then
            If Not IsNothing(dr("Creado")) Then
                If Not String.IsNullOrEmpty(dr("Creado").ToString()) Then
                    Try
                        If Not IsNothing(_Result) Then
                            _Result.CreateDate = DateTime.Parse(dr("Creado").ToString())
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End If
            End If
        End If

        For Each indice As IIndex In _Result.DocType.Indexs
            If indice.DropDown = IndexAdditionalType.AutoSustitución OrElse indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                If dr.Table.Columns.Contains("I" & indice.ID) AndAlso Not IsDBNull(dr("I" & indice.ID)) Then
                    indice.Data = dr("I" & indice.ID).ToString()
                    If Not IsDBNull(dr(indice.Name)) Then indice.dataDescription = dr(indice.Name).ToString()

                End If
            Else
                If dr.Table.Columns.Contains(indice.Name) AndAlso Not IsDBNull(dr(indice.Name)) Then
                    indice.Data = dr(indice.Name).ToString()
                Else
                    indice.Data = String.Empty
                End If
            End If
        Next
        _Result.Indexs = _Result.DocType.Indexs

    End Sub

    Public Function SearchIndexByUserId(ByVal indexId As Int64, ByVal indexType As IndexDataType, ByVal comparador As String, ByVal value As String, ByVal userId As Int64) As Dictionary(Of Int64, Int64) Implements IResults_Business.SearchIndexByUserId
        Dim TmpDocsDictionary As New Dictionary(Of Int64, Int64)()
        Dim DTB As New DocTypesBusiness
        Dim TmpDocTypeIDs As Generic.List(Of Int64) = DTB.GetDocTypeIdByIndexId(indexId, userId)
        Dim TmpDS As DataSet
        Dim Restriction As String
        Dim UB As New UserBusiness
        Dim RMF As New RestrictionsMapper_Factory
        For Each Tmpdoctypeid As Int64 In TmpDocTypeIDs
            TmpDS = New DataSet()

            Restriction = RMF.GetRestrictionWebStrings(userId, Tmpdoctypeid, UB.GetUserNamebyId(userId))
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

    Public Function SearchIndexByUserIdForWebServices(ByVal indexId As Int64, ByVal indexType As IndexDataType, ByVal comparador As String, ByVal value As String, ByVal userId As Int64) As Dictionary(Of Int64, Int64) Implements IResults_Business.SearchIndexByUserIdForWebServices
        Dim TmpDocsDictionary As New Dictionary(Of Int64, Int64)()
        Dim DTB As New DocTypesBusiness
        Dim TmpDocTypeIDs As Generic.List(Of Int64) = DTB.GetDocTypeIdByIndexId(indexId, userId)
        Dim TmpDS As DataSet
        Dim Restriction As String
        Dim UB As New UserBusiness
        Dim RMF As New RestrictionsMapper_Factory
        For Each Tmpdoctypeid As Int64 In TmpDocTypeIDs
            TmpDS = New DataSet()

            Restriction = RMF.GetRestrictionWebStrings(userId, Tmpdoctypeid, UB.GetUserNamebyId(userId))
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

    Public Function SearchIndex(ByVal lngIndexID As Int64, ByVal enmIndexType As IndexDataType, ByVal strComparador As String, ByVal strValue As String) As Dictionary(Of Int64, Int64) Implements IResults_Business.SearchIndex
        Dim tmpDocsDictionary As New Dictionary(Of Int64, Int64)()
        Dim DTB As New DocTypesBusiness
        Dim tmpDocTypeIDs As Generic.List(Of Int64) = DTB.GetDocTypeIdByIndexId(lngIndexID)
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

    Public Function SearchIndex(ByVal lngIndexID As Int64, ByVal enmIndexType As IndexDataType, ByVal strComparador As String, ByVal strValue As String, ByVal lngDocTypeID As Int64) As DataSet Implements IResults_Business.SearchIndex
        Return Results_Factory.SearchIndex(lngIndexID, enmIndexType, strComparador, strValue, lngDocTypeID)
    End Function

    Public Function SearchIndex(ByVal lngIndexID As Int64, ByVal enmIndexType As IndexDataType, ByVal strComparador As String, ByVal strValue As String, ByVal lngDocTypeID As Int64, ByVal restriction As String) As DataSet Implements IResults_Business.SearchIndex
        Return Results_Factory.SearchIndex(lngIndexID, enmIndexType, strComparador, strValue, lngDocTypeID, restriction)
    End Function

    Public Function SearchIndexForWebService(ByVal lngIndexID As Int64, ByVal enmIndexType As IndexDataType, ByVal strComparador As String, ByVal strValue As String, ByVal lngDocTypeID As Int64, ByVal restriction As String) As DataSet Implements IResults_Business.SearchIndexForWebService
        Return Results_Factory.SearchIndexForWebServices(lngIndexID, enmIndexType, strComparador, strValue, lngDocTypeID, restriction)
    End Function

    Public Function SearchbyIndexs(ByVal indexId As Int32, ByVal indexType As Int32, ByVal dt As IDocType, ByVal IndexData As String) As DataSet Implements IResults_Business.SearchbyIndexs
        Return Results_Factory.SearchbyIndexs(indexId, indexType, dt, IndexData)
    End Function

    Public Function GetDocumentData(ByVal ds As DataSet, ByVal dt As IDocType, ByVal i As Int32) As DataSet Implements IResults_Business.GetDocumentData
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
    Public Function getResultsData(ByVal docTypeId As Int32, ByVal indexId As Int32, ByVal genIndex As List(Of ArrayList), ByVal UserId As Int32, Optional ByVal comparateValue As String = "", Optional ByVal searchValue As Boolean = True) As DataSet Implements IResults_Business.getResultsData
        Dim UB As New UserBusiness
        Dim RMF As New RestrictionsMapper_Factory

        Dim RF As New Results_Factory
        'Traigo las restricciones y armo un string con ellas
        Dim restricc As String = RMF.GetRestrictionWebStrings(UserId, docTypeId, UB.GetUserNamebyId(UserId))



        Return RF.getResultsData(docTypeId, indexId, genIndex, comparateValue, searchValue, restricc)

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
    Public Function getResultsAndPageQueryResults(ByVal PageId As Int16, ByVal PageSize As Int16, ByVal docTypeId As Int32, ByVal indexId As Int64, ByVal genIndex As List(Of ArrayList), ByVal UserId As Int32, Optional ByVal comparateValue As String = "", Optional ByVal comparateDateValue As String = "", Optional ByVal Operation As String = "", Optional ByVal searchValue As Boolean = True, Optional ByVal SortExpression As String = "", Optional ByVal SymbolToReplace As String = "", Optional ByVal BySimbolReplace As String = "", Optional ByRef resultCount As Integer = 0) As DataTable Implements IResults_Business.getResultsAndPageQueryResults
        Dim UB As New UserBusiness
        Dim RMF As New RestrictionsMapper_Factory
        'Traigo las restricciones y armo un string con ellas
        Dim restricc As String = RMF.GetRestrictionWebStrings(UserId, docTypeId, UB.GetUserNamebyId(UserId))



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
    Public Function GetDocRelations() As DataSet Implements IResults_Business.GetDocRelations
        Try
            Return Results_Factory.GetDocRelations
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return Nothing
    End Function

#End Region

#Region "Insert & Save Document"


    Public Function GetNewNewResult(ByVal DocType As IDocType, Optional ByVal _UserId As Int32 = 0, Optional ByVal File As String = "") As INewResult Implements IResults_Business.GetNewNewResult
        Dim newResult As New NewResult(File)
        newResult.UserID = _UserId
        If IsNothing(DocType) = False Then
            newResult.Parent = DocType
            Try
                LoadVolume(newResult)
            Catch ex As Exception
                newResult.Ready = False
            End Try
            newResult.Indexs = New IndexsBusiness().GetIndexsData(newResult.ID, newResult.DocTypeId)
        End If
        Return newResult
    End Function

    Public Function GetSearchResult(ByVal DocType As IDocType, Optional ByVal _UserId As Int32 = 0, Optional ByVal File As String = "") As INewResult Implements IResults_Business.GetSearchResult
        Dim newResult As New NewResult(File)
        newResult.DocType = DocType
        newResult.UserID = _UserId
        Try
            LoadVolume(newResult)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        newResult.Indexs = New IndexsBusiness().GetIndexsData(newResult.ID, newResult.DocTypeId)
        Return newResult
    End Function


    Public Function setIndexData(ByVal indexId As Int64, ByVal entityId As Int64, ByVal parentResultId As Int64,
                                 ByVal indexValue As String) As Boolean Implements IResults_Business.setIndexData
        Dim ResultFactory As New Results_Factory
        Dim isIndexUpdated As Boolean
        '''Dim TableType As String = "Doc_I" & DocTypeId & ""
        Try
            isIndexUpdated = ResultFactory.setIndexData(indexId, entityId, parentResultId, indexValue)

        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        Return isIndexUpdated
    End Function

    Public Function CloneResult(ByVal originalResult As IResult, ByVal filename As String, ByVal GenerateIds As Boolean, Optional ByVal FlagInsertar As Boolean = False) As INewResult Implements IResults_Business.CloneResult
        Dim ClonedResult As New NewResult(filename)
        ClonedResult.OriginalName = originalResult.OriginalName
        ClonedResult.AutoName = originalResult.AutoName

        ClonedResult.Disk_Group_Id = originalResult.Disk_Group_Id
        ClonedResult.DISK_VOL_PATH = originalResult.DISK_VOL_PATH

        If GenerateIds Then
            ClonedResult.ID = CoreData.GetNewID(IdTypes.DOCID)
            ClonedResult.Doc_File = ClonedResult.ID & New FileInfo(filename).Extension
        Else
            ClonedResult.ID = 0
            ClonedResult.Doc_File = New FileInfo(filename).Name
        End If

        ClonedResult.DocType = originalResult.DocType
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
    Public Sub CloneIndexs(ByVal r As IResult, ByVal DocType As IDocType) Implements IResults_Business.CloneIndexs
        For Each I As Index In DocType.Indexs
            Dim NewI As New Index(I)
            r.Indexs.Add(I)
        Next
    End Sub

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
    Shared Sub InsertDocumentRelation(ByVal parentResultId As Int64, ByVal relationatedResultId As Int64, ByVal relationid As Int32, ByRef t As ITransaction)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "insert document relation")
        Results_Factory.InsertDocumentRelation(parentResultId, relationatedResultId, relationid, t)

    End Sub




#Region "Agregar o remover DocTypes de Workflow"

    'Public Sub AddDocTypeToWF(ByVal DocTypeID As Int32, ByVal WfID As Int32)
    '    Results_Factory.AddDocTypeToWF(DocTypeID, WfID)
    'End Sub
    Public Sub RemoveDocTypeWF(ByVal DocTypeID As Int32) Implements IResults_Business.RemoveDocTypeWF
        Results_Factory.RemoveDocTypeWF(DocTypeID)
    End Sub
    Public Function GetInitialStep(ByVal WFID As Int16) As Int32 Implements IResults_Business.GetInitialStep
        Dim initialStep As Int32
        initialStep = Results_Factory.GetInitialStep(WFID)
        Return initialStep
    End Function
    Public Function IsDocTypeInWF(ByVal DocTypeid As Int32) As Boolean Implements IResults_Business.IsDocTypeInWF
        Return Results_Factory.IsDocTypeInWF(DocTypeid)
    End Function
#End Region
    Public Shared Event ResultInserted(ByRef Result As IResult)
    'Enum InsertResult
    '    Insertado
    '    InsertadoNuevoVolumen
    '    InsertadoTempVolumen
    '    NoInsertado
    '    Remplazado
    '    RemplazadoTodos
    '    NoRemplazadoTodos
    '    NoRemplazado
    '    ErrorInsertar
    '    ErrorReemplazar
    '    ErrorCopia
    '    ErrorIndicesIncompletos
    'End Enum

#End Region

#Region "Indexs"


    Public Enum indexType As Integer
        index = 0
        unique = 1
        notnull = 2
    End Enum


    Public Function FillIndexData(ByVal EntityId As Int64, ByVal Id As Int64, ByVal Indexs As List(Of IIndex), Optional ByVal inThread As Boolean = False) As List(Of IIndex) Implements IResults_Business.FillIndexData
        Dim dr As IDataReader = Nothing
        Dim Con As IConnection = Nothing
        Dim ASB As New AutoSubstitutionBusiness
        Try
            If Indexs.Count = 0 Then
                Indexs = ZCore.GetInstance().FilterIndex(EntityId)
            End If

            Dim RF As New Results_Factory
            dr = RF.CompleteIndexData(Id, EntityId, Indexs, Con)

            If dr.IsClosed = False Then
                While dr.Read
                    Dim i As Int32
                    For i = 0 To Indexs.Count - 1
                        Try
                            If Not IsDBNull(dr.GetValue(dr.GetOrdinal("I" & DirectCast(Indexs(i), Index).ID))) Then
                                DirectCast(Indexs(i), Index).Data = dr.GetValue(dr.GetOrdinal("I" & DirectCast(Indexs(i), Index).ID)).ToString
                                'Si el indice es de tipo Sustitucion
                                If DirectCast(Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustitución OrElse DirectCast(Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                    'Se carga la descripcion de Indice
                                    DirectCast(Indexs(i), Index).dataDescription = ASB.getDescription(DirectCast(Indexs(i), Index).Data, DirectCast(Indexs(i), Index).ID)
                                End If
                            Else
                                DirectCast(Indexs(i), Index).Data = String.Empty
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    Next
                End While
            End If
            Return Indexs
        Finally
            If Not IsNothing(dr) Then
                If dr.IsClosed = False Then
                    dr.Close()
                End If
                dr.Dispose()
                dr = Nothing
            End If
            If Not IsNothing(Con) Then
                Con.Close()
                Con.dispose()
                Con = Nothing
            End If
            ASB = Nothing

        End Try
    End Function

    Public Function ValidateIndexDatabyRights(ByVal _result As IResult) As Boolean Implements IResults_Business.ValidateIndexDatabyRights
        If RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, _result.DocType.ID) Then
            Dim IRI As Hashtable = UB.GetIndexsRights(_result.DocType.ID, Zamba.Membership.MembershipHelper.CurrentUser.ID)
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

    'Public Sub SaveIndexData(ByRef result As IResult, ByVal reIndexFlag As Boolean, Optional ByVal changeEvent As Boolean = True, Optional ByVal OnlySpecifiedIndexsids As Generic.List(Of Int64) = Nothing)
    '    If Results_Factory.SaveIndexData(result, reIndexFlag, changeEvent, OnlySpecifiedIndexsids) Then
    '        UpdateAutoName(result)
    '        RaiseEvent ResultIndexsChanged(result)

    '    End If
    'End Sub

    ''' <summary>
    ''' Guarda solamente los datos modificados
    ''' </summary>
    ''' <param name="result"></param>
    ''' <param name="reIndexFlag"></param>
    ''' <param name="changeEvent"></param>
    ''' <param name="OnlySpecifiedIndexsids"></param>
    ''' <remarks></remarks>
    Public Sub SaveModifiedIndexData(ByRef result As IResult, ByVal reIndexFlag As Boolean, ByVal changeEvent As Boolean, ByVal OnlySpecifiedIndexsids As Generic.List(Of Int64), ByVal dtModifiedIndex As DataTable) Implements IResults_Business.SaveModifiedIndexData
        Dim taskResult As ITaskResult
        Dim WFTaskBusiness As New WFTaskBusiness

        Dim RF As New Results_Factory
        Try
            If Not IsNothing(dtModifiedIndex) Then
                Try

                    If VariablesInterReglas.ContainsKey("IndicesModificados") = False Then
                        VariablesInterReglas.Add("IndicesModificados", dtModifiedIndex)
                    Else
                        VariablesInterReglas.Item("IndicesModificados") = dtModifiedIndex
                    End If
                Catch ex As Exception

                End Try
            End If

            RF.SaveModifiedIndexData(result, reIndexFlag, OnlySpecifiedIndexsids)
            UpdateAutoName(result)
            RF.InsertIndexerState(result.DocTypeId, result.ID, 0, Nothing)



        Finally
            If Not IsNothing(taskResult) Then
                taskResult.Dispose()
                taskResult = Nothing
            End If
            WFTaskBusiness = Nothing


        End Try
    End Sub

    Public Sub ResultUpdated(ByVal DocTypeId As Int64, ByVal ResultId As Int64) Implements IResults_Business.ResultUpdated
        Dim WFTB As New WFTaskBusiness
        Dim taskResult As ITaskResult = WFTB.GetTaskByDocIdAndWorkFlowId(ResultId, 0)
        If Not IsNothing(taskResult) Then

            WFTB.ExecutedSetIndexsRules(taskResult)
        End If

    End Sub

    Private Shared Sub SaveIndexText(ByRef result As INewResult, ByVal data As String)
        Results_Factory.SaveIndexText(result, data)
    End Sub

    Private Function CheckRequiredIndexs(ByRef result As INewResult) As Boolean
        Dim isOk As Boolean
        Dim ASB As New AutoSubstitutionBusiness

        For Each I As IIndex In result.Indexs

            If I.Required AndAlso String.IsNullOrEmpty(I.DataTemp.Trim) AndAlso String.IsNullOrEmpty(I.Data.Trim) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El índice " & I.Name & " es obligatorio")
                Throw New Exception("El índice " & I.Name & " es obligatorio")
            End If

            If I.DropDown = IndexAdditionalType.DropDown OrElse I.DropDown = IndexAdditionalType.AutoSustitución OrElse I.DropDown = IndexAdditionalType.AutoSustituciónJerarquico OrElse I.DropDown = IndexAdditionalType.DropDownJerarquico Then
                isOk = False

                If I.DropDown = IndexAdditionalType.DropDown OrElse I.DropDown = IndexAdditionalType.DropDownJerarquico Then
                    If String.IsNullOrEmpty(I.Data.Trim) = False Then

                        Dim Data As List(Of String) = IndexsBusiness.GetDropDownList(I.ID)

                        For Each d As String In Data
                            If String.IsNullOrEmpty(I.Data.Trim) OrElse d.Trim.ToLower = I.Data.Trim.ToLower Then
                                'si esta en la lista esta ok! 
                                isOk = True
                                Exit For
                            End If
                        Next
                    Else
                        isOk = True
                    End If
                Else
                    I.Data = IIf(I.Data = Nothing, "", I.Data)

                    If String.IsNullOrEmpty(I.Data.Trim) = False Then
                        'Dim dt As DataTable = ASB.GetIndexData(I.ID, False)

                        If Not String.IsNullOrEmpty(I.Data.Trim) Then
                            Dim desc = ASB.getDescription(I.Data.Trim.ToLower, I.ID)
                            If String.IsNullOrEmpty(desc) = False Then
                                'si esta en la lista esta ok! 
                                isOk = True
                            Else
                                Dim code = ASB.getCode(I.dataDescription.Trim.ToLower, I.ID)
                                If String.IsNullOrEmpty(desc) = False Then
                                    'si esta en la lista esta ok! 
                                    isOk = True
                                End If
                            End If

                        Else
                            isOk = True
                        End If

                        If Not isOk Then
                            'Elimino el Cache del indice
                            If I.DropDown = IndexAdditionalType.DropDown OrElse I.DropDown = IndexAdditionalType.DropDownJerarquico Then
                                If Cache.DocTypesAndIndexs.hsIndexsArray.ContainsKey(I.ID) Then
                                    Cache.DocTypesAndIndexs.hsIndexsArray.Remove(I.ID)
                                End If
                            ElseIf I.DropDown = IndexAdditionalType.AutoSustitución OrElse I.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                If Cache.DocTypesAndIndexs.hsIndexsDT.ContainsKey(I.ID) Then
                                    Cache.DocTypesAndIndexs.hsIndexsDT.Remove(I.ID)
                                End If
                            End If

                            'si no se permite valores fuera de la lista. 
                            If Not IndexsBusiness.CheckIfAllowDataOutOfList(I.ID) Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El índice " & I.Name & " no permite valores fuera de la lista")
                                Throw New Exception("El índice " & I.Name & " no permite valores fuera de la lista")
                            End If
                        End If
                    Else
                        isOk = True
                    End If
                End If
            End If

        Next
        ASB = Nothing
        Return True
    End Function


    Public Function GetIndexByAssociateIndex(ByVal DocTypeId As Int32, ByVal DocId As Int32) As DataTable Implements IResults_Business.GetIndexByAssociateIndex
        Dim ResultFactory As New Results_Factory
        Dim DataTable As DataTable
        Dim TableType As String = "Doc_I" & DocTypeId & ""
        Try
            DataTable = ResultFactory.GetIndexDataAssociate(DocTypeId, DocId, TableType)

        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        Return DataTable
    End Function

    ''' <summary>
    ''' Entity Observaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Function getEntidadObservaciones() As DataTable Implements IResults_Business.getEntidadObservaciones
        Dim ResultFactory As New Results_Factory
        Dim DataTable As DataTable
        Try
            DataTable = ResultFactory.getEntidadObservaciones()

        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        Return DataTable
    End Function

    ''' <summary>
    ''' Guarda las observaciones
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <param name="parentResultId"></param>
    ''' <param name="AtributeId"></param>
    ''' <remarks></remarks>
    Public Function GetObservaciones(ByVal entityId As Int64, ByVal parentResultId As Int64, ByVal AtributeId As Int64) As DataTable Implements IResults_Business.GetObservaciones
        Dim ResultFactory As New Results_Factory
        Dim DataTable As DataTable
        Try
            DataTable = ResultFactory.GetObservaciones(entityId, parentResultId, AtributeId)

        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        Return DataTable
    End Function

    ''' <summary>
    ''' Migracion las observaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Function DeletMigracionObservaciones(ByVal EntitiId As Int64, ByVal AtributeId As Int64) Implements IResults_Business.DeletMigracionObservaciones
        Dim ResultFactory As New Results_Factory
        Try
            ResultFactory.DeletMigracionObservaciones(EntitiId, AtributeId)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Function

    ''' <summary>
    ''' Migracion las observaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Function InsertMigracionObservaciones(ByVal EntitiId As Int64, ByVal Fecha As String, ByVal UsrId As Int64, ByVal Value As String, ByVal docId As Int64, ByVal AtributeId As Int64) As DataTable Implements IResults_Business.InsertMigracionObservaciones
        Dim ResultFactory As New Results_Factory
        Dim DataTable As DataTable
        Try
            InsertIndexObservaciones(EntitiId, docId, Value, AtributeId, UsrId, Fecha)

        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        Return DataTable
    End Function

    ''' <summary>
    ''' Migracion las observaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Function InsertMigracionObservaciones2(ByVal EntitiId As Int64, ByVal Fecha As String, ByVal UsrId As Int64, ByVal Value As String, ByVal docId As Int64, ByVal AtributeId As Int64) As DataTable Implements IResults_Business.InsertMigracionObservaciones2
        Dim ResultFactory As New Results_Factory
        Dim DataTable As DataTable
        Try
            InsertIndexObservaciones2(EntitiId, docId, Value, AtributeId, UsrId, Fecha)

        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        Return DataTable
    End Function



    ''' <summary>
    ''' Migrar las observaciones
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <remarks></remarks>
    Public Function MigracionObservaciones(ByVal Entidad As Int64) As DataTable Implements IResults_Business.MigracionObservaciones
        Dim ResultFactory As New Results_Factory
        Dim DataTable As DataTable
        Try
            DataTable = ResultFactory.MigracionObservaciones(Entidad)

        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        Return DataTable
    End Function



    ''' <summary>
    ''' Guarda las observaciones
    ''' </summary>
    ''' <param name="indexId"></param>
    ''' <param name="parentResultId"></param>
    ''' <param name="InputObservacion"></param>
    ''' <remarks></remarks>
    Public Function GetIndexObservaciones(ByVal indexId As Int64, ByVal entityId As Int64, ByVal parentResultId As Int64, ByVal InputObservacion As String, ByVal Evaluation As String) As DataTable Implements IResults_Business.GetIndexObservaciones
        Dim ResultFactory As New Results_Factory
        Dim DataTable As DataTable
        '''Dim TableType As String = "Doc_I" & DocTypeId & ""
        Try
            DataTable = ResultFactory.GetIndexObservaciones(indexId, entityId, parentResultId, InputObservacion, Evaluation)

        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        Return DataTable
    End Function

    ''' <summary>
    ''' Guarda las observaciones
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <param name="parentResultId"></param>
    ''' <param name="InputObservacion"></param>
    ''' <param name="AtributeId"></param>
    ''' <param name="User"></param>
    ''' <remarks></remarks>
    Public Function InsertIndexObservaciones(ByVal entityId As Int64, ByVal parentResultId As Int64, ByVal InputObservacion As String, ByVal AtributeId As Int64, ByVal User As Int64) As DataTable Implements IResults_Business.InsertIndexObservaciones
        Dim ResultFactory As New Results_Factory
        Dim DataTable As DataTable
        Try
            Dim IncrementID = CoreData.GetNewID(IdTypes.Observaciones)
            DataTable = ResultFactory.GetIndexObservacionesV2(entityId, parentResultId, InputObservacion, AtributeId, IncrementID, User)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        Return DataTable
    End Function

    ''' <summary>
    ''' Guarda las observaciones
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <param name="parentResultId"></param>
    ''' <param name="InputObservacion"></param>
    ''' <param name="AtributeId"></param>
    ''' <param name="User"></param>
    ''' <remarks></remarks
    Public Function InsertIndexObservaciones(ByVal entityId As Int64, ByVal parentResultId As Int64, ByVal InputObservacion As String, ByVal AtributeId As Int64, ByVal User As Int64, ByVal Fecha As String) As DataTable Implements IResults_Business.InsertIndexObservaciones
        Dim ResultFactory As New Results_Factory
        Dim DataTable As DataTable
        Try
            Dim IncrementID = CoreData.GetNewID(IdTypes.Observaciones)
            DataTable = ResultFactory.GetIndexObservacionesV2(entityId, parentResultId, InputObservacion, AtributeId, IncrementID, User, Fecha)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        Return DataTable
    End Function

    ''' <summary>
    ''' Guarda las observaciones
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <param name="parentResultId"></param>
    ''' <param name="InputObservacion"></param>
    ''' <param name="AtributeId"></param>
    ''' <param name="User"></param>
    ''' <remarks></remarks
    Public Function InsertIndexObservaciones2(ByVal entityId As Int64, ByVal parentResultId As Int64, ByVal InputObservacion As String, ByVal AtributeId As Int64, ByVal User As Int64, ByVal Fecha As String) As DataTable Implements IResults_Business.InsertIndexObservaciones2
        Dim ResultFactory As New Results_Factory
        Dim DataTable As DataTable
        Try
            Dim IncrementID = CoreData.GetNewID(IdTypes.Observaciones)
            DataTable = ResultFactory.GetIndexObservacionesV2Date(entityId, parentResultId, InputObservacion, AtributeId, IncrementID, User, Fecha)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        Return DataTable
    End Function


    ''' <summary>
    ''' Guarda las observaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Function AllReport(ByVal userid As Int64) As DataSet Implements IResults_Business.AllReport
        Dim ResultFactory As New Results_Factory
        Dim Ds As DataSet
        '''Dim TableType As String = "Doc_I" & DocTypeId & ""
        Try
            Ds = ResultFactory.AllReportGeneral(userid)

        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        Return Ds
    End Function


    'TODO: [sebstian] se hizo un over load para poder cargar los indices de danone correctamente en zamba
    Public Function InsertDocumentNew(ByRef newResult As INewResult, ByVal move As Boolean, Optional ByVal ReindexFlag As Boolean = False, Optional ByVal Reemplazar As Boolean = False, Optional ByVal showQuestions As Boolean = True, Optional ByVal IsVirtual As Boolean = False, Optional ByVal IsReplica As Boolean = False, Optional ByVal hasName As Boolean = False) As InsertResult Implements IResults_Business.InsertDocumentNew
        ZTrace.WriteLineIf(ZTrace.IsInfo, "insert documentt new")
        Try
            InsertNew(newResult, move, ReindexFlag, Reemplazar, showQuestions, IsVirtual, IsReplica, hasName)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return InsertResult.NoInsertado
        End Try
    End Function



    Private Function GetResultName(ByRef result As INewResult) As String
        Dim DTB As New DocTypesBusiness
        Try
            Return GetResultName(result.DocType.ID, result.CreateDate, result.EditDate, result.Indexs)
            'Dim AutoName As String = ZCore.GetDocTypeAutoName(result.DocType.ID)
            'Return DocTypesFactory.GetAutoName(AutoName, DocTypesFactory.GetDocTypeName(result.DocType.ID), result.CreateDate, result.EditDate, result.Indexs).Trim()
        Catch ex As Exception
            ZCore.raiseerror(ex)
            If Not String.IsNullOrEmpty(result.DocType.Name) Then
                Return result.DocType.Name
            Else
                Return DTB.GetDocTypeName(result.DocTypeId)
            End If
        End Try
        DTB = Nothing
    End Function

    Private Function GetResultName(ByVal docTypeId As Int64, ByVal createDate As Date, ByVal editDate As Date, ByVal indexs As List(Of IIndex)) As String
        Dim AutoName As String = ZCore.GetInstance().GetDocTypeAutoName(docTypeId)
        Dim DTB As New DocTypesBusiness
        Dim DTF As New DocTypesFactory
        Return DTF.GetAutoName(AutoName, DTB.GetDocTypeName(docTypeId), createDate, editDate, indexs).Trim()
        DTB = Nothing
        DTF = Nothing
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
    Public Function Insert(ByVal name As String, ByVal binaryDocument As Byte(), ByVal fileExtension As String, ByVal docTypeId As Int64, ByVal indexs As DataTable, ByVal DontOpenTaskAfterInsertInDoGenerateCoverPage As Boolean) As Int64 Implements IResults_Business.Insert
        Dim resultId As Int64 = 0
        Dim TemporaryPath As String = Nothing
        Dim DocumentPath As String = Nothing
        Dim DTB As New DocTypesBusiness
        Try
            If Not String.IsNullOrEmpty(fileExtension.Trim()) Then
                TemporaryPath = Zamba.Membership.MembershipHelper.AppTempDir("\Temp").FullName

                If (Not fileExtension.Contains(".")) Then
                    fileExtension = "." + fileExtension
                End If

                DocumentPath = TemporaryPath + "\" + name + fileExtension

                If Not Directory.Exists(TemporaryPath) Then
                    Directory.CreateDirectory(TemporaryPath)
                End If

                If File.Exists(DocumentPath) Then
                    File.Delete(DocumentPath)
                End If

                _fileStream = New FileStream(DocumentPath, FileMode.CreateNew)
                _binaryWriter = New BinaryWriter(_fileStream)
                _binaryWriter.Write(binaryDocument)
                _fileStream.Flush()
                _fileStream.Close()
                _binaryWriter.Close()
                _fileStream.Dispose()
                _fileStream = Nothing


                Array.Clear(binaryDocument, 0, binaryDocument.Length)
            Else
                DocumentPath = String.Empty
            End If
            Using CurrentNewResult As INewResult = GetNewResult(docTypeId, DocumentPath)

                If (Not IsNothing(indexs) AndAlso indexs.Rows.Count > 0) Then

                    Dim ExistsIndexId As Boolean
                    For Each CurrentIndexRow As DataRow In indexs.Rows
                        ExistsIndexId = False

                        For Each CurrentIndex As IIndex In CurrentNewResult.Indexs
                            If CurrentIndex.ID = CurrentIndexRow.Item("IndexId") Then
                                CurrentIndex.DataTemp = CurrentIndexRow.Item("IndexValue")
                                CurrentIndex.Data = CurrentIndex.DataTemp
                                ExistsIndexId = True

                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Pregunto si el Indice es Valido If Not CurrentIndex.isvalid() Then")
                                If Not CurrentIndex.isvalid() Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "El Indice NO es Valido")

                                    Throw New Exception("El valor '" & CurrentIndex.DataTemp & "' no es valido con el tipo de datos del indice " & CurrentIndex.Name)
                                End If
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El Indice es Valido")
                            End If
                        Next

                        If Not ExistsIndexId Then
                            Dim DocTypeName As String = DTB.GetDocTypeName(docTypeId)
                            Throw New Exception("El entidad " & DocTypeName & " no contiene un Indice con ID " & CurrentIndexRow.Item("IndexId").ToString() & " o el usuario no tiene permisos sobre el mismo")
                        End If

                    Next
                End If

                '   indexs.Clear()
                indexs = Nothing

                If String.IsNullOrEmpty(fileExtension.Trim()) Then
                    Insert(CurrentNewResult, False, False, False, False, True, False, False, True, True)
                Else
                    Insert(CurrentNewResult, True, False, False, False, False, False, False, True, True)
                End If
                resultId = CurrentNewResult.ID
            End Using

            If File.Exists(DocumentPath) Then
                File.Delete(DocumentPath)
            End If

            TemporaryPath = Nothing
            DocumentPath = Nothing

            'Catch ex As Exception
            'Zamba.Core.ZClass.raiseerror(ex)
            'Throw ex
        Finally

            If Not IsNothing(binaryDocument) Then
                Array.Clear(binaryDocument, 0, binaryDocument.Length)
                binaryDocument = Nothing
            End If

            If Not IsNothing(indexs) Then
                '       indexs.Clear()
                indexs = Nothing
            End If

            If Not String.IsNullOrEmpty(DocumentPath) AndAlso File.Exists(DocumentPath) Then
                File.Delete(DocumentPath)
            End If

            DTB = Nothing
            name = Nothing
            fileExtension = Nothing
            TemporaryPath = Nothing
            DocumentPath = Nothing
        End Try
        Return resultId
    End Function

    ''' <summary>
    ''' [Sebastian 13-05-09] valida que indice este completo si es requerido contra la ZIR
    ''' </summary>
    ''' <param name="_newresult"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateIndexData(ByVal _newresult As INewResult) As Boolean Implements IResults_Business.ValidateIndexData
        Dim IRI As Hashtable = UB.GetIndexsRights(_newresult.DocType.ID, Zamba.Membership.MembershipHelper.CurrentUser.ID)
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
    '''(pablo) valida que que se hayan ingresado valores correctos en los indices de sustitucion
    ''' </summary>
    ''' <param name="_newresult"></param>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    Public Function ValidateDescriptionInSustIndex(ByVal _newresult As INewResult) As Boolean Implements IResults_Business.ValidateDescriptionInSustIndex
        Dim DTB As New DocTypesBusiness
        Dim dsIndexproperty As DataSet = DTB.GetIndexsProperties(_newresult.DocType.ID)
        DTB = Nothing
        For Each i As Index In _newresult.Indexs
            If (i.DropDown = IndexAdditionalType.AutoSustitución OrElse i.DropDown = IndexAdditionalType.AutoSustituciónJerarquico) And i.dataDescriptionTemp <> String.Empty Then
                Return True
            Else
                Return False
            End If
        Next
        Return True
    End Function
    ''' <summary>
    ''' [Sebastian 13-05-09] valida que el indice en r_doc_type tenga el permiso de obligatorio. Si no lo
    ''' tiene devuelve false
    ''' </summary>
    ''' <param name="_newresult"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateIndexDataFromDoctype(ByVal _newresult As INewResult) As Boolean Implements IResults_Business.ValidateIndexDataFromDoctype
        Dim DTB As New DocTypesBusiness
        Dim dsIndexproperty As DataSet = DTB.GetIndexsProperties(_newresult.DocType.ID)
        DTB = Nothing
        For Each i As Index In _newresult.Indexs
            For Each row As DataRow In dsIndexproperty.Tables(0).Rows
                If row("index_id") = i.ID AndAlso row("mustcomplete") = 1 Then
                    If String.IsNullOrEmpty(i.Data) = True AndAlso String.IsNullOrEmpty(i.DataTemp) = True Then
                        Return False
                    End If
                End If
            Next
        Next

        'Dim IRI As Hashtable = UB.GetIndexsRights(_newresult.DocType.ID, Zamba.Membership.MembershipHelper.CurrentUser.ID, True)
        'For Each i As Index In _newresult.DocType.Indexs
        '    If DirectCast(IRI(i.ID), IndexsRightsInfo).GetIndexRightValue(RightsType.IndexRequired) = True Then
        '        If String.IsNullOrEmpty(i.Data) = True Then
        '            Return False
        '        End If
        '    End If
        'Next

        Return True
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="newResult"></param>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 29/04/09 Modified: Se adapto el codigo para que tome mas de una clave. </history>
    Public Sub AutocompleteIndexsNewDocument(ByRef newResult As INewResult) Implements IResults_Business.AutocompleteIndexsNewDocument
        Dim AC As AutocompleteBCBusiness = Nothing
        Dim indexTemp As ArrayList

        Try
            'Obtiene el campo IndexKey relacionado con AutoComplete del primer
            'documento. Es es porque deberían ser todos del mismo tipo.
            indexTemp = AutoCompleteBarcode_FactoryBusiness.getIndexKeys(newResult.DocType.ID)
            'Si ocurre un error en este punto, es porque index
            'es Nothing que quiere decir 1 el documento no tiene
            'indices para autocompletado
            If Not indexTemp Is Nothing AndAlso indexTemp.Count > 0 Then

                AC = AutoCompleteBarcode_FactoryBusiness.GetComplete(Int32.Parse(newResult.DocType.ID), indexTemp(0).ID) 'Obtiene una instancia del Objeto AutoComplete

                If Not IsNothing(AC) Then 'Siempre AC deberia ser una instancia
                    'Actuliza el valor del indice.
                    'Dicho valor es utilizado para el seguimiento del documento dentro
                    'de un WorkFlow.
                    For Each intmp As Index In indexTemp
                        intmp.DataTemp = findIn(newResult.Indexs, intmp).DataTemp
                    Next
                    'Dim res As IResult = New Result()
                    'If indextemp.count = 1 Then
                    '    res = AC.Complete(newResult, DirectCast(indexTemp(0), Index))
                    'Else

                    'End If
                    'Persiste los cambios en el documento
                    If Not IsNothing(AC.Complete(DirectCast(newResult, NewResult), indexTemp, Nothing)) Then
                        'se cambio por save modified
                        If newResult.ID > 0 Then
                            SaveModifiedIndexData(DirectCast(newResult, NewResult), True, True, Nothing, Nothing)
                        End If
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
    Public Function Insert(ByRef newResult As INewResult, ByVal move As Boolean, Optional ByVal reIndexFlag As Boolean = False, Optional ByVal reemplazarFlag As Boolean = False, Optional ByVal showQuestions As Boolean = True, Optional ByVal isVirtual As Boolean = False, Optional ByVal isReplica As Boolean = False, Optional ByVal hasName As Boolean = False, Optional ByVal throwEx As Boolean = False, Optional ByVal RefreshWFAfterInsert As Boolean = True, Optional Userid As Decimal = Nothing, Optional newId As Int64 = 0, Optional ExecuteEntryRules As Boolean = True) As InsertResult Implements IResults_Business.Insert
        Dim NewResultStatus As InsertResult = InsertResult.NoInsertado
        Dim DTB As New DocTypesBusiness

        Try
            Dim ZOPTB As New ZOptBusiness
            Dim forceBlob As String = ZOPTB.GetValue("ForceBlob")
            ZOPTB = Nothing
            CheckRequiredIndexs(newResult)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ingresando a la insercion de documentos")

            'Autocompleta los indices si corresponde, Se subio en nivel la ejecucion porque quizas se utilizan valores de los indices para autonombre
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Autocompletando")
            AutocompleteIndexsNewDocument(newResult)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se autocompletaron los indices")

            MapMail(newResult)

            '[sebastian 29/01/09] Se verifica si tiene el permiso de completar con el id mas alto el indice
            'y luego se lo agrega.
            'obtengo los permismos de los indices
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo Permisos para entidad: " & newResult.DocType.ID)
            'Dim IRI As Hashtable = UB.GetIndexsRights(newResult.DocType.ID, Zamba.Membership.MembershipHelper.CurrentUser.ID, True, True)
            'Si el indice es autoincremental le cargo el valor si no lo tiene
            Dim dsIndexsToIncrement As DataSet = DTB.GetIndexsProperties(newResult.DocType.ID)

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

            If newId = 0 Then
                newResult.ID = CoreData.GetNewID(IdTypes.DOCID)
            Else
                newResult.ID = newId
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "newResult.ID: " & newResult.ID)

            newResult.Name = GetResultName(newResult)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre: " & newResult.Name)

            If isVirtual = False OrElse isReplica OrElse (isVirtual = False AndAlso Not String.IsNullOrEmpty(forceBlob) AndAlso Boolean.Parse(forceBlob)) Then 'Intento cargar un volumen para el documento, si no se puede se genera una excepcion
                ZTrace.WriteLineIf(ZTrace.IsInfo, "cargando volumen")
                LoadVolume(newResult)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "se cargo el volumen")
                newResult.NewFile = VolumesBusiness.VolumePath(newResult.Volume, newResult.DocType.ID) & "\" & newResult.ID & newResult.Extension
                ZTrace.WriteLineIf(ZTrace.IsInfo, "path del volumen: " & newResult.NewFile)
                newResult.OffSet = newResult.Volume.offset
                If hasName = False Then
                    newResult.Doc_File = Path.GetFileName(newResult.NewFile)
                End If
            End If


            ZTrace.WriteLineIf(ZTrace.IsInfo, "ID: " & newResult.ID)
            If newResult.File IsNot Nothing Then
                newResult.IconId = GetFileIcon(newResult.File) 'Obtengo el icono
            End If
            'Ezequiel: Se comenta funcionalidad ZIP
            'Dim IsZipped As Boolean = False

            If isVirtual = False Or isReplica Then 'Copio fisicamente el archivo

                'Si el volumen es de tipo DB lo codifica o se fuerza a hacerlo, caso contrario mueve el archivo fisico al servidor
                If newResult.Volume.VolumeType = VolumeType.DataBase OrElse (Not String.IsNullOrEmpty(forceBlob) AndAlso Boolean.Parse(forceBlob)) Then

                    newResult.EncodedFile = FileEncode.Encode(newResult.File)



                    'If ChechIfMustZipBlob(newResult.FileName) Then
                    '    newResult.EncodedFile = FileEncode.Zip(newResult.EncodedFile)
                    '    IsZipped = True
                    'End If
                    If IsNothing(newResult.EncodedFile) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento NO encontrado en ruta especificada.")
                    Else

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento codificado con exito")
                    End If


                    Try
                        'Asigna el tamaño del result
                        If Not newResult.ISVIRTUAL And newResult.EncodedFile IsNot Nothing Then
                            newResult.FileLength = Math.Ceiling(newResult.EncodedFile.Length / 1024)
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                Else
                    If move Then
                        Try
                            MoveFile(newResult)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento movido")
                        Catch ex As Exception
                            copyFile(newResult)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento copiado")
                        End Try
                    Else
                        copyFile(newResult)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento copiado")
                    End If

                    Try
                        'Asigna el tamaño del result
                        If File.Exists(newResult.FullPath) Then
                            newResult.FileLength = Math.Ceiling(New IO.FileInfo(newResult.FullPath).Length / 1024)
                        Else
                            'si el fullpath no existe es porque se lo movió al servidor
                            newResult.FileLength = Math.Ceiling(New IO.FileInfo(newResult.NewFile).Length / 1024)
                        End If

                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                End If

            End If

            If isVirtual AndAlso isReplica = False Then
                newResult.Disk_Group_Id = 0
            Else
                newResult.Disk_Group_Id = newResult.Volume.ID
                newResult.DISK_VOL_PATH = newResult.Volume.path
            End If

            'Objeto para realizar la inserción en una transaccion
            Dim Transact As New Transaction
            Dim wf_results As New Dictionary(Of IWorkFlow, List(Of ITaskResult))

            Try
                Try

                    Dim isShared As Boolean = RB.GetUserRights(Userid, Zamba.ObjectTypes.DocTypes, Zamba.Core.RightsType.Share, newResult.DocTypeId)

                    If Userid <> 0 Then
                        RF.RegisterDocumentWithAlfanumericIndex(newResult, reIndexFlag, isVirtual, Transact, Userid, isShared)
                        NewResultStatus = InsertResult.Insertado
                    Else
                        RF.RegisterDocumentWithAlfanumericIndex(newResult, reIndexFlag, isVirtual, Transact, Membership.MembershipHelper.CurrentUser.ID, isShared)
                        NewResultStatus = InsertResult.Insertado
                    End If

                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al guardar la doc i Buscando si es un reemplazo " & ex.ToString)
                    If ex.ToString.ToLower.IndexOf("unique") <> -1 OrElse ex.ToString.ToLower.IndexOf("unica") <> -1 Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "UNIQUE DETECTADO")
                        If reemplazarFlag Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Reemplazo detectado")

                            Delete(newResult, True, Transact)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento Borrado")

                            Dim isShared As Boolean = RB.GetUserRights(Userid, Zamba.ObjectTypes.DocTypes, Zamba.Core.RightsType.Share, newResult.DocTypeId)
                            If Not IsNothing(Userid) Then
                                RF.RegisterDocumentWithAlfanumericIndex(newResult, reIndexFlag, isVirtual, Transact, Userid, isShared)
                            Else
                                RF.RegisterDocumentWithAlfanumericIndex(newResult, reIndexFlag, isVirtual, Transact, Membership.MembershipHelper.CurrentUser.ID, isShared)
                            End If


                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento Reemplazado")
                            NewResultStatus = InsertResult.Remplazado
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se hara reemplazo")
                            If showQuestions Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se haran preguntas")
                                'TODO : MsgBox en Business?
                                Select Case ReplaceMsgBox.Show("Los datos ingresados no son únicos. ¿Desea reemplazar el documento existente?", "Insertar Documento")
                                    Case ReplaceMsgBox.ReplaceMsgBoxResult.yes
                                        Delete(newResult, False, Transact)
                                        ReplaceDocument(newResult, newResult.File, False, Transact)
                                        NewResultStatus = InsertResult.Remplazado
                                    Case ReplaceMsgBox.ReplaceMsgBoxResult.yesAll
                                        Delete(newResult, False, Transact)
                                        ReplaceDocument(newResult, newResult.File, False, Transact)
                                        NewResultStatus = InsertResult.RemplazadoTodos
                                    Case ReplaceMsgBox.ReplaceMsgBoxResult.no
                                        Delete(newResult, True, Transact)
                                        NewResultStatus = InsertResult.NoRemplazado
                                    Case ReplaceMsgBox.ReplaceMsgBoxResult.noAll
                                        Delete(newResult, True, Transact)
                                        NewResultStatus = InsertResult.NoRemplazadoTodos
                                End Select
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en No Reemplazo " & ex.ToString())
                                ZCore.raiseerror(ex)
                                Throw
                            End If
                        End If
                    Else
                        ZCore.raiseerror(ex)
                        Throw
                    End If
                End Try

                If newResult.Volume.ID <> 0 Then
                    newResult.DISK_VOL_PATH = VolumesBusiness.GetVolumeData(newResult.Volume.ID, Transact).path ' Hernan
                Else
                    newResult.DISK_VOL_PATH = String.Empty
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Volumen:" & newResult.DISK_VOL_PATH)
                If isVirtual = False Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ruta:" & newResult.FullPath)
                    newResult.File = newResult.FullPath
                End If

                'LO ASOCIO AUTOMATICAMENTE A WORKFLOWS
                Dim ResultsArray As New ArrayList(1)
                ResultsArray.Add(newResult)
                Dim WFIds As ArrayList = DTB.GetDocTypeWfIds(newResult.DocType.ID, Transact)

                Dim WFTB As New WFTaskBusiness
                Dim WFB As New WFBusiness

                For Each wfId As String In WFIds

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Asociando a WF:" & wfId)

                    Dim WF As WorkFlow = WFB.GetWFbyId(wfId)
                    wf_results.Add(WF, WFTB.AddResultsToWorkFLow(ResultsArray, WF, True, False, Userid, False, Transact))

                Next

                WFB = Nothing

                ' Se actualiza el U_TIME del usuario (un campo que indica el horario de cuando fue la última acción realizada por el usuario) y se 
                ' guarda su acción en la tabla USER_HST. 
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando historial")
                UB.SaveAction(newResult.ID, Zamba.ObjectTypes.Documents, Zamba.Core.RightsType.Create, newResult.Name, 0)

                Try
                    'Guarda el documento para la busqueda por indices especificos
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Guarda el documento para la busqueda por indices especificos")
                    RF.InsertIndexerState(newResult.DocTypeId, newResult.ID, 0, Transact)
                    InsertSearchIndexData(newResult, Nothing)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                'Aplicando cambios
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Inserción Finalizada. Realizando Commit...")
                Transact.Commit()
                Transact.Con.Close()
                Transact.Con.dispose()
                Transact.Con = Nothing
                Transact.Dispose()
                Transact = Nothing
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Commit ejecutado correctamente. Inserción finalizada con éxito.")
                RaiseEvent ResultInserted(DirectCast(newResult, Result))
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Inserción finalizada con errores. Removiendo los cambios realizados...")
                ZCore.raiseerror(ex)
                If Not IsNothing(Transact.Transaction) Then
                    Transact.Rollback()
                End If

                wf_results = Nothing

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cambios removidos con éxito. El estado del sistema ha vuelto a como era antes de la inserción.")
                If throwEx = False Then
                    If ex.Message.Contains("es obligatorio") Then
                        Return InsertResult.ErrorIndicesIncompletos
                    ElseIf ex.Message.Contains("no permite valores fuera de la lista") Then
                        Return InsertResult.ErrorIndicesInvalidos
                    Else
                        Return InsertResult.NoInsertado
                    End If
                Else
                    Throw
                End If
            Finally
                If Not IsNothing(Transact) Then
                    If Not IsNothing(Transact.Con) Then
                        Transact.Con.Close()
                        Transact.Con.dispose()
                        Transact.Con = Nothing
                    End If
                    Transact.Dispose()
                    Transact = Nothing
                End If
            End Try

            Try

                If wf_results IsNot Nothing AndAlso ExecuteEntryRules Then
                    Dim WFTBusiness As New WFTaskBusiness

                    For Each wf_res As KeyValuePair(Of IWorkFlow, List(Of ITaskResult)) In wf_results
                        WFTBusiness.ExecuteWFRulesFromInsert(wf_res.Key, Not ExecuteEntryRules, False, wf_res.Value)
                    Next

                End If

            Catch ex As Exception
                'Si falla la entrada a WF lo inserto igual, pero hago raise del error
                ZClass.raiseerror(ex)
            End Try

            Return NewResultStatus

        Catch ex As Exception
            ZCore.raiseerror(ex)
            If throwEx = False Then
                If ex.Message.Contains("es obligatorio") Then
                    Return InsertResult.ErrorIndicesIncompletos
                ElseIf ex.Message.Contains("no permite valores fuera de la lista") Then
                    Return InsertResult.ErrorIndicesInvalidos
                End If
                Return InsertResult.NoInsertado
            Else
                Throw
            End If
        Finally
            DTB = Nothing
        End Try
    End Function

    Public Sub MapMail(newResult As INewResult)
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "[MapMail]: Validando MSG..." + newResult.FileName)
        If (newResult.FileName.ToLower().EndsWith(".msg") OrElse newResult.FileName.ToLower().EndsWith(".eml")) AndAlso File.Exists(newResult.FullPath) Then
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "[MapMail]: Mapeando archivo MSG..." + newResult.FullPath)

            Dim message As MailMessage = MailMessage.Load(newResult.FullPath)

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "[MapMail]: Obteniendo atributos...")
            Dim Opts As List(Of Zamba.IIndex) =
                newResult.Indexs.Where(Function(x As Zamba.IIndex) x.Name.ToLower() = "code" Or x.Name.ToLower() = "codigo" Or
                    x.Name.ToLower() = "to" Or x.Name.ToLower() = "para" Or
                    x.Name.ToLower() = "cc" Or
                    x.Name.ToLower() = "subject" Or x.Name.ToLower() = "asunto" Or
                    x.Name.ToLower() = "date" Or x.Name.ToLower() = "fecha" Or x.Name.ToLower() = "fechaenviado" Or x.Name.ToLower() = "fecha recibido" Or
                    x.Name.ToLower() = "from" Or x.Name.ToLower() = "enviadopor" Or x.Name.ToLower() = "enviado por").ToList()

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "[MapMail]: Asignando valores de atributos obtenidos:")
            For Each item As Zamba.IIndex In Opts
                Select Case item.Name.ToLower
                    Case "from", "enviadopor", "enviado por"
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "[MapMail]: Asignando 'From'...")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Id del atributo '" + item.Name + "': " + item.ID.ToString())
                        Dim IndexFrom As String = ZOptBusiness.GetValueOrDefault("msgIndexFrom", item.ID)
                        Dim From As IIndex = newResult.GetIndexById(IndexFrom)
                        From.DataTemp = IIf(From IsNot Nothing, GetCollectionMailingList(message.From), "")

                    Case "to", "para"
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "[MapMail]: Asignando 'To'...")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Id del atributo '" + item.Name + "': " + item.ID.ToString())
                        Dim IndexTo As String = ZOptBusiness.GetValueOrDefault("msgIndexTo", item.ID)
                        Dim [To] As IIndex = newResult.GetIndexById(IndexTo)
                        [To].DataTemp = IIf([To] IsNot Nothing, GetCollectionMailingList(message.To), "")

                    Case "cc"
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "[MapMail]: Asignando 'Cc'...")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Id del atributo '" + item.Name + "': " + item.ID.ToString())
                        Dim IndexCc As String = ZOptBusiness.GetValueOrDefault("msgIndexCc", item.ID)
                        Dim Cc As IIndex = newResult.GetIndexById(IndexCc)
                        Cc.DataTemp = IIf(Cc IsNot Nothing, GetCollectionMailingList(message.Cc), "")

                    Case "subject", "asunto"
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "[MapMail]: Asignando 'Subject'...")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Id del atributo '" + item.Name + "': " + item.ID.ToString())
                        Dim IndexSubject As String = ZOptBusiness.GetValueOrDefault("msgIndexSubject", item.ID)
                        Dim Subject As IIndex = newResult.GetIndexById(IndexSubject)
                        Subject.DataTemp = IIf(Subject IsNot Nothing, message.Subject, "")

                    Case "date", "fecha", "fechaenviado", "fecha recibido"
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "[MapMail]: Asignando atributo '" + item.Name + "': " + item.ID.ToString() + " valor: " + message.Date)
                        Dim IndexDate As String = ZOptBusiness.GetValueOrDefault("msgIndexDate", item.ID)
                        Dim [Date] As IIndex = newResult.GetIndexById(IndexDate)
                        [Date].DataTemp = IIf([Date] IsNot Nothing, message.Date, "")

                    'Case "code", "codigo"
                    '    ZTrace.WriteLineIf(ZTrace.IsVerbose, "[MapMail]: Asignando 'Code'...")
                    '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Id del atributo '" + item.Name + "': " + item.ID.ToString())
                    '    Dim IndexCode As String = ZOptBusiness.GetValueOrDefault("msgIndexCode", item.ID)
                    '    Dim Code As IIndex = newResult.GetIndexById(IndexCode)
                    '    Code.DataTemp = IIf(Code IsNot Nothing, message.Id, "")

                    Case "usuario correo"
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "[MapMail]: Asignando 'UserMail'...")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Id del atributo '" + item.Name + "': " + item.ID.ToString())
                        Dim IndexUserMail As String = ZOptBusiness.GetValueOrDefault("msgIndexUserMail", item.ID)
                        Dim UserMail As IIndex = newResult.GetIndexById(IndexUserMail)
                        UserMail.DataTemp = IIf(UserMail IsNot Nothing, message.Id.Substring(0, message.Id.IndexOf("@")), "")

                    Case Else
                End Select
            Next
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "[MapMail]: Mapeado Exitoso.")
        End If
    End Sub

    Private Shared Function GetCollectionMailingList(MailsList As MailAddressCollection) As String
        Dim result As String = ""

        If MailsList.Count = 1 Then
            For Each item As String In MailsList(0).Address.Split(";")
                If (MailsList(0).Address.Split(";").Last() = item) Then
                    result += item
                Else
                    result += item + "; "
                End If
            Next
        ElseIf MailsList.Count > 1 Then
            For Each item As MailAddress In MailsList
                If (MailsList.Last().ToString = item.ToString()) Then
                    result += item.Address
                Else
                    result += item.Address + "; "
                End If
            Next
        End If

        Return result
    End Function

    '[sebastian] se hizo un overload para poder insertar los indices de danone correctamente
    Public Function InsertNew(ByRef newResult As INewResult, ByVal move As Boolean, Optional ByVal reIndexFlag As Boolean = False, Optional ByVal reemplazarFlag As Boolean = False, Optional ByVal showQuestions As Boolean = True, Optional ByVal isVirtual As Boolean = False, Optional ByVal isReplica As Boolean = False, Optional ByVal hasName As Boolean = False) As InsertResult Implements IResults_Business.InsertNew
        Dim RF As New Results_Factory



        Dim NewResultStatus As InsertResult = InsertResult.NoInsertado

        'Autocompleta los indices si corresponde, Se subio en nivel la ejecucion porque quizas se utilizan valores de los indices para autonombre
        AutocompleteIndexsNewDocument(newResult)

        newResult.Name = GetResultName(newResult)

        If isVirtual = False Or isReplica Then 'Intento cargar un volumen para el documento, si no se puede se genera una excepcion
            LoadVolume(newResult)
            newResult.NewFile = VolumesBusiness.VolumePath(newResult.Volume, newResult.DocType.ID) & "\" & newResult.ID & newResult.Extension
            newResult.OffSet = newResult.Volume.offset
            If hasName = False Then
                newResult.Doc_File = New FileInfo(newResult.NewFile).Name
            End If
        End If

        newResult.ID = CoreData.GetNewID(IdTypes.DOCID)
        newResult.IconId = GetFileIcon(newResult.File) 'Obtengo el icono

        'Ezequiel: Se comenta funcionalidad ZIP
        'Dim IsZipped As Boolean = False

        If isVirtual = False Or isReplica Then 'Copio fisicamente el archivo
            If newResult.Volume.VolumeType = VolumeType.DataBase Then

                newResult.EncodedFile = FileEncode.Encode(newResult.RealFullPath)

                'Ezequiel: Se comenta funcionalidad ZIP
                'If ChechIfMustZipBlob(newResult.FileName) Then
                '    newResult.EncodedFile = FileEncode.Zip(newResult.EncodedFile)
                '    IsZipped = True
                'End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento codificado con exito")
            Else
                If move Then
                    Try
                        MoveFile(newResult)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento movido")
                    Catch ex As Exception
                        copyFile(newResult)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento copiado")
                    End Try
                Else
                    copyFile(newResult)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento copiado")
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

            If CheckRequiredIndexs(newResult) Then
                Dim isShared As Boolean = RB.GetUserRights(Membership.MembershipHelper.CurrentUser.ID, Zamba.ObjectTypes.DocTypes, Zamba.Core.RightsType.Share, newResult.DocTypeId)
                Dim ResultFactory As New Results_Factory
                RF.RegisterDocumentWithAlfanumericIndex(newResult, reIndexFlag, Membership.MembershipHelper.CurrentUser.ID, isVirtual, isShared)
                ResultFactory = Nothing
            End If

            'Llamo al metodo que se fijara si debe realizar una relacion segun la dependencia de atributos

            NewResultStatus = InsertResult.Insertado
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al guardar la doc i Buscando si es un reemplazo " & ex.ToString)

            If ex.ToString.ToLower.IndexOf("unique") <> -1 OrElse ex.ToString.ToLower.IndexOf("unica") <> -1 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "UNIQUE DETECTADO")
                If reemplazarFlag Then
                    Try
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reemplazo detectado")

                        Delete(newResult, True)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento Borrado")

                        If CheckRequiredIndexs(newResult) Then
                            Dim isShared As Boolean = RB.GetUserRights(Membership.MembershipHelper.CurrentUser.ID, Zamba.ObjectTypes.DocTypes, Zamba.Core.RightsType.Share, newResult.DocTypeId)
                            Dim ResultFactory As New Results_Factory
                            RF.RegisterDocumentWithAlfanumericIndex(newResult, reIndexFlag, Membership.MembershipHelper.CurrentUser.ID, isVirtual, isShared)
                            ResultFactory = Nothing
                        End If

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
                                    Delete(newResult, False)
                                    ReplaceDocument(newResult, newResult.File, False, Nothing)
                                    NewResultStatus = InsertResult.Remplazado
                                Catch exe As Exception
                                    MessageBox.Show("Ocurrió un error al reemplazar el documento", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    NewResultStatus = InsertResult.ErrorReemplazar
                                    Throw
                                End Try
                            Case ReplaceMsgBox.ReplaceMsgBoxResult.yesAll
                                Try
                                    Delete(newResult, False)
                                    ReplaceDocument(newResult, newResult.File, False, Nothing)
                                    NewResultStatus = InsertResult.RemplazadoTodos
                                Catch exe As Exception
                                    'TODO : MsgBox en Business?
                                    MessageBox.Show("Ocurrió un error al reemplazar el documento", "ZAMBA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    NewResultStatus = InsertResult.RemplazadoTodos
                                    Throw
                                End Try
                            Case ReplaceMsgBox.ReplaceMsgBoxResult.no
                                Try
                                    Delete(newResult, True)
                                    NewResultStatus = InsertResult.NoRemplazado
                                Catch exc As Exception
                                    NewResultStatus = InsertResult.ErrorReemplazar
                                End Try
                            Case ReplaceMsgBox.ReplaceMsgBoxResult.noAll
                                Try
                                    Delete(newResult, True)
                                    NewResultStatus = InsertResult.NoRemplazadoTodos
                                Catch exc As Exception
                                    NewResultStatus = InsertResult.ErrorReemplazar
                                End Try
                        End Select
                    Else
                        Try
                            newResult.ID = 0
                            Return Insert(newResult, move, reIndexFlag, reemplazarFlag, showQuestions, isVirtual, isReplica, hasName, False, True)

                        Catch exc As Exception
                            NewResultStatus = InsertResult.NoInsertado
                            Throw
                        End Try
                    End If
                End If
            Else
                NewResultStatus = InsertResult.NoInsertado
                Throw
            End If
        End Try

        If move AndAlso NewResultStatus <> InsertResult.NoInsertado Then
            Try
                File.Delete(newResult.File)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If

        newResult.DISK_VOL_PATH = VolumesBusiness.GetVolumeData(newResult.Volume.ID).path ' Hernan

        If isVirtual = False Then
            newResult.File = newResult.FullPath
        End If

        Try

            ' Se actualiza el U_TIME del usuario (un campo que indica el horario de cuando fue la última acción realizada por el usuario) y se 
            ' guarda su acción en la tabla USER_HST. En este caso, la acción es cuando el cliente presiona el botón Insertar 
            ' (no el de la barra de herramientas)
            UB.SaveAction(newResult.ID, Zamba.ObjectTypes.Documents, Zamba.Core.RightsType.Create, newResult.Name)
            RaiseEvent ResultInserted(DirectCast(newResult, NewResult))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        'Try 'Si tiene asignado algún WorkFlow lo inserto en la etapa inicial
        '    If Results_Factory.IsDocTypeInWF(newResult.DocType.ID) = True Then
        '        Dim Results(1) As IResult 'todo wf: ver de que no sea por evento
        '        Results.SetValue(newResult, 0)
        '        RaiseEvent AddResult2Wf(Results)
        '    End If
        'Catch ex As Exception
        '    zclass.raiseerror(ex)
        'End Try
        Dim DTB As New DocTypesBusiness
        Try 'LO ASOCIO AUTOMATICAMENTE A WORKFLOWS
            Dim ResultsArray As New ArrayList(1)
            ResultsArray.Add(newResult)

            Dim WFIds As ArrayList = DTB.GetDocTypeWfIds(newResult.DocType.ID)

            For Each wfId As String In WFIds
                'RaiseEvent AddResult2Wf(ResultsArray, wfId)
                'La anterior línea se cambia por:
                AdjuntarAWF(ResultsArray, wfId)
                'ya que se ha eliminado el proyecto Zamba.WFBusines y no se
                'necesita levantar un evento.
                '[Alejandro]
                '       WFTasksFactory.AddResultsToWorkFLowTask(ResultsArray, wfId)
            Next

            If (WFIds.Count > 0) Then
                RaiseEvent updateWFs()
            End If

            UpdateAutoName(newResult)
            RF.InsertIndexerState(newResult.DocTypeId, newResult.ID, 0, Nothing)

        Catch ex As Exception
            ZClass.raiseerror(ex)

        Finally
            DTB = Nothing

        End Try

        Return NewResultStatus

    End Function
    Public Function findIn(ByVal Indexs As List(Of IIndex), ByVal pIndex As IIndex) As IIndex Implements IResults_Business.findIn
        Dim i As Int16 = 0
        Dim ASB As New AutoSubstitutionBusiness
        For i = 0 To Indexs.Count - 1
            If Indexs(i).ID = pIndex.ID Then
                pIndex.Data = Indexs(i).Data
                pIndex.DataTemp = Indexs(i).DataTemp
                If DirectCast(Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustitución OrElse DirectCast(Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                    pIndex.dataDescription = Indexs(i).dataDescription
                    pIndex.dataDescriptionTemp = ASB.getDescription(Indexs(i).DataTemp, Indexs(i).ID)
                    pIndex.DropDown = Indexs(i).DropDown
                End If
                pIndex.Type = Indexs(i).Type
                Return pIndex
            End If
        Next
        ASB = Nothing

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
    Public Function UpdateInsert(ByRef Result As INewResult, ByVal move As Boolean, Optional ByVal ReindexFlag As Boolean = False, Optional ByVal Reemplazar As Boolean = False, Optional ByVal showQuestions As Boolean = True, Optional ByVal IsVirtual As Boolean = False, Optional ByVal addToWF As Boolean = True) As InsertResult Implements IResults_Business.UpdateInsert
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Comenzando InsertDocument")
        Dim insertresult As InsertResult = insertresult.NoInsertado
        Dim DTB As New DocTypesBusiness
        Dim DTF As New DocTypesFactory


        'Obtengo el auto nombre
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Busco el Autoname")
            Dim autonamecode As String = ZCore.GetInstance().GetDocTypeAutoName(Result.DocType.ID)
            Result.Name = DTF.GetAutoName(autonamecode, DTB.GetDocTypeName(Result.DocType.ID), Result.CreateDate, Result.EditDate, Result.Indexs).Trim
        Catch ex As Exception
            Result.Name = Result.DocType.Name
            ZClass.raiseerror(ex)
        End Try



        'busco lugar en algún volumen
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargo los volumenes")

        'Intento cargar un volumen para el documento, si no se puede se genera una excepcion
        If IsVirtual = False Then
            LoadVolume(Result)
            Result.NewFile = VolumesBusiness.VolumePath(Result.Volume, Result.DocType.ID) & "\" & Result.ID & Result.Extension
            Result.OffSet = Result.Volume.offset
            Result.Doc_File = New FileInfo(Result.NewFile).Name
        End If

        'obtengo el último doc_ID
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Busco el id del documento")
        If Result.ID = 0 Then Result.ID = CoreData.GetNewID(IdTypes.DOCID)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "ID=" & Result.ID)

        'Obtengo el icono
        Result.IconId = GetFileIcon(Result.File)
        'copio fisicamente el archivo
        Try
            If IsVirtual = False Then
                If Not IsNothing(Result.EncodedFile) Then
                    Result.EncodedFile = FileEncode.Encode(Result.FullPath)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento codificado con exito")
                Else
                    If move = False Then
                        copyFile(Result)
                    Else
                        Try
                            MoveFile(Result)
                        Catch ex As Exception
                            copyFile(Result)
                        End Try
                    End If
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento copiado")
                End If

            End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al copiar el documento")
            ZClass.raiseerror(ex)
        End Try

        Dim RF As New Results_Factory
        'guardo la doc t
        Try
            If CheckRequiredIndexs(Result) Then
                RF.UpdateRegisterDocument(Result, ReindexFlag, IsVirtual)
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Doct registrada")
            'guardo la doc i
            '            SaveIndexData(Result, ReindexFlag)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "DocI guardada correctamente")
            insertresult = insertresult.Insertado
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
                        Delete(Result, False)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento Borrado")
                        ReplaceDocument(Result, Result.File, False, Nothing)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento Reemplazado")
                        insertresult = insertresult.Remplazado
                    Catch exep As Exception
                        insertresult = insertresult.ErrorReemplazar
                        Throw New Exception("Error al Reemplazar el documento. " & exep.Message)
                    End Try
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se hara reemplazo")
                    If showQuestions = True Then
                        Select Case ReplaceMsgBox.Show("Los datos ingresados no son únicos. ¿Desea reemplazar el documento existente?", "Insertar Documento")
                            Case ReplaceMsgBox.ReplaceMsgBoxResult.yes
                                Try
                                    Delete(Result, False)
                                    ReplaceDocument(Result, Result.File, False, Nothing)
                                    insertresult = insertresult.Remplazado
                                Catch exe As Exception
                                    MessageBox.Show("Ocurrió un error al reemplazar el documento", "Zamba", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    insertresult = insertresult.ErrorReemplazar
                                    Throw
                                End Try
                            Case ReplaceMsgBox.ReplaceMsgBoxResult.yesAll
                                Try
                                    Delete(Result, False)
                                    ReplaceDocument(Result, Result.File, False, Nothing)
                                    insertresult = insertresult.RemplazadoTodos
                                Catch exe As Exception
                                    MessageBox.Show("Ocurrió un error al reemplazar el documento", "ZAMBA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    insertresult = insertresult.RemplazadoTodos
                                    Throw
                                End Try
                            Case ReplaceMsgBox.ReplaceMsgBoxResult.no
                                Try
                                    Delete(Result, True)
                                    insertresult = insertresult.NoRemplazado
                                Catch exc As Exception
                                    insertresult = insertresult.ErrorReemplazar
                                End Try
                            Case ReplaceMsgBox.ReplaceMsgBoxResult.noAll
                                Try
                                    Delete(Result, True)
                                    insertresult = insertresult.NoRemplazadoTodos
                                Catch exc As Exception
                                    insertresult = insertresult.ErrorReemplazar
                                End Try
                        End Select
                    Else
                        Try
                            Result.ID = 0
                            Insert(Result, move, ReindexFlag, Reemplazar, showQuestions, IsVirtual)
                        Catch exc As Exception
                            insertresult = insertresult.NoInsertado
                            Throw
                        End Try
                    End If
                End If
            Else
                insertresult = insertresult.NoInsertado
                Throw
            End If
        Finally

        End Try

        If move = True AndAlso insertresult <> insertresult.NoInsertado Then
            Try
                File.Delete(Result.File)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If
        Result.DISK_VOL_PATH = VolumesBusiness.GetVolumeData(Result.Volume.ID).path ' Hernan

        If IsVirtual = False Then
            Result.File = Result.FullPath
        End If

        Try
            RaiseEvent ResultInserted(DirectCast(Result, NewResult))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Try
            If addToWF = True Then
                'LO ASOCIO AUTOMATICAMENTE A WORKFLOWS
                Dim A As New ArrayList
                A.Add(Result)

                Dim WFIds As ArrayList = DTB.GetDocTypeWfIds(Result.DocType.ID)

                For Each wfId As String In WFIds
                    AdjuntarAWF(A, wfId)
                Next

                If (WFIds.Count > 0) Then
                    RaiseEvent updateWFs()
                End If
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            DTB = Nothing
            DTF = Nothing
        End Try
        Return insertresult
    End Function

    Public Sub AdjuntarAWF(ByVal Results As ArrayList, ByVal WFID As Int64) Implements IResults_Business.AdjuntarAWF
        Dim WFTB As New WFTaskBusiness
        Dim WFB As New WFBusiness
        Dim WF As WorkFlow
        WF = WFB.GetWFbyId(WFID)
        WFTB.AddResultsToWorkFLow(Results, WF, True, True, MembershipHelper.CurrentUser.ID, False)
        WFTB = Nothing
        WFB = Nothing
    End Sub
#End Region

#Region "Waiting Documents"

    'Obtiene el campo DocTypes de la TablaZWFI asociados a ese Id de regla
    Public Function GetDocTypesZWFI(ByVal ruleID As Int64) As Generic.List(Of Int64) Implements IResults_Business.GetDocTypesZWFI
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
    Public Function GetWIFromZWFI(ByVal ruleID As Int64) As Generic.List(Of Int64) Implements IResults_Business.GetWIFromZWFI
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

    'Obtiene el valor de un índice de una Tabla DOC_I y de un Id de Documento.
    Public Function GetIndexValueFromDoc_I(ByVal docType As Int64, ByVal docID As Int64, ByVal indexID As Int64) As String Implements IResults_Business.GetIndexValueFromDoc_I
        Return Results_Factory.GetIndexValueFromDoc_I(docType, docID, indexID)
    End Function

    'Obtiene el valor de un índice en la tabla ZWFII
    Public Function GetIndexValueFromZWFII(ByVal wI As Int64, ByVal indexID As Int64) As String Implements IResults_Business.GetIndexValueFromZWFII
        Return Results_Factory.GetIndexValueFromZWFII(wI, indexID)
    End Function

    'Obtiene todos los Id de Documento asociados a un DocType en la tabla ZI
    Public Function GetDocIDsFromZI(ByVal docType As Int64) As Generic.List(Of Int64) Implements IResults_Business.GetDocIDsFromZI
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


    'Public Function GetInsertIDsFromZI(ByVal docType As Int64) As Generic.List(Of Int64)

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
    '        raiseerror(ex)
    '    End Try

    '    Return resultInsertIDs

    'End Function

    'Obtiene todos los Id de Atributos de la tabla ZWFII asociados a un WI
    Public Function GetIndexIDsFromZWFII(ByVal wI As Int64) As List(Of Int64) Implements IResults_Business.GetIndexIDsFromZWFII
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
    Public Sub DeleteWI(ByVal wI As Int64) Implements IResults_Business.DeleteWI
        DeleteFromZWFI(wI)
        DeleteFromZWFII(wI)

    End Sub

    Public Sub DeleteFromZWFI(ByVal wI As Int64) Implements IResults_Business.DeleteFromZWFI
        Try
            Results_Factory.DeleteFromZWFI(wI)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub DeleteFromZWFII(ByVal wI As Int64) Implements IResults_Business.DeleteFromZWFII
        Try
            Results_Factory.DeleteFromZWFII(wI)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub DeleteFromZI(ByVal lngDocID As Int64) Implements IResults_Business.DeleteFromZI
        Try
            Results_Factory.DeleteFromZI(lngDocID)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub


    'Verifica si el Id de esa regla aparece en la tabla ZWFI
    '(si es TRUE es que está esperando a por documentos)
    Public Function IsRuleWaitingDocument(ByVal ruleId As Int64) As Boolean Implements IResults_Business.IsRuleWaitingDocument
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

    Public Function GetWIFromZIWhereRuleID(ByVal lngRuleID As Int64) As List(Of Int64) Implements IResults_Business.GetWIFromZIWhereRuleID
        Return Results_Factory.GetRuleIDsFromZIWhereInsertID(lngRuleID)
    End Function

    'Valida si el DocType pasado por parámetro aparece en la 
    'tabla ZI
    Public Function ValidateIsDocTypeInZI(ByVal docType As Int64) As Boolean Implements IResults_Business.ValidateIsDocTypeInZI
        Try
            Return Results_Factory.ValidateIsDocTypeInZI(docType)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try
    End Function

    'Valida si para todos los WI de la regla hay documentos Insertados que cumplen
    'con los valores de índice esperados.
    Public Function ValidateWI(ByVal ruleID As Int64) As Boolean Implements IResults_Business.ValidateWI
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
    'de índice esperados.
    Private Function ValidateWI(ByVal wI As Int64, ByVal docType As Int64) As Boolean

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

            'Después de la comparación índice a índice, deberían
            'ser correctos todos los atributos (index.Count)
            countDeberianSerCorrectos = indexIDs.Count


            'Para cada documento insertado se valida indice a indice si cumple 
            'con los valores de índice esperados
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
    'de índice esperados.
    Private Function ValidateWI2(ByVal wI As Int64, ByVal docType As Int64) As List(Of Int64)

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

            'Después de la comparación índice a índice, deberían
            'ser correctos todos los atributos (index.Count)
            countDeberianSerCorrectos = indexIDs.Count


            'Para cada documento insertado se valida indice a indice si cumple 
            'con los valores de índice esperados
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

    Public Function GetInsertIDsWhereDocID(ByVal lngDocID As Int64) As List(Of Int64) Implements IResults_Business.GetInsertIDsWhereDocID
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

    Public Function GetPahtWhereDocTypeAndID(ByVal lngDocType As Int64, ByVal lngDocID As Int64) As String Implements IResults_Business.GetPahtWhereDocTypeAndID
        Return Results_Factory.GetFullNameWhereDocID(lngDocType, lngDocID)
    End Function

    '''<summary></summary>
    '''<history>[Alejandro]</history>
    '''<summary>Valida si la Regla está lista para ejecutarse o tiene algun WaitID en espera</summary>
    '''<history>[Alejandro]</history>
    Public Function IsRuleWaitForDocumentReady(ByVal ruleID As Int64) As Boolean Implements IResults_Business.IsRuleWaitForDocumentReady
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


    Public Function IsRuleWaiting(ByVal lngRuleID As Int64, ByVal lngInsertID As Int64) As Boolean Implements IResults_Business.IsRuleWaiting
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

    Public Function GetRuleIDWhereWI(ByVal lngWI As Int64) As Int64 Implements IResults_Business.GetRuleIDWhereWI
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
    Public Function AsociateIncomingResult(ByVal _InsertID As Int64, ByVal _DTID As Int64, ByVal _DocID As Int64, ByVal _IDate As Date, ByVal _IID As Int64(), ByVal _IValue As String()) As Boolean Implements IResults_Business.AsociateIncomingResult
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

    Public Function BuildMailWaitingDocument(ByVal lngWaitID As Int64) As String Implements IResults_Business.BuildMailWaitingDocument
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

    Private Function SendMailWaitingDocument(ByVal lngRuleID As Int64, ByVal lngWaitID As Int64) As Boolean
        Dim listaDeMails As New List(Of String)
        Dim mail As SendMailConfig = Nothing

        Try
            Dim mensaje As String = BuildMailWaitingDocument(lngWaitID)
            listaDeMails = GetWatingDocumentMails(lngRuleID)

            mail = New SendMailConfig
            mail.MailTo = listaDeMails.ToString
            mail.Subject = "Documento Entrante"
            mail.Body = mensaje
            mail.IsBodyHtml = False

            MessagesBusiness.SendQuickMail(mail)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            If mail IsNot Nothing Then
                mail.Dispose()
                mail = Nothing
            End If
        End Try

    End Function

    Public Function GetWatingDocumentMails(ByVal lngRuleID As Int64) As List(Of String) Implements IResults_Business.GetWatingDocumentMails
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
                    Correo = UB.FillUserMailConfig(uID)
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
    Private Function GetWaitingData(ByVal lngWaitID As Int64) As List(Of String)

        Dim dsWaitIDs As New DataSet()
        Dim dsIndexs As New DataSet()

        Dim tempDocTypeID As Int32
        Dim tempDocTypeName As String = String.Empty

        Dim tempIndexID As Int64
        Dim tempIndexName As String = String.Empty
        Dim tempIndex As String = String.Empty
        Dim tempIndexValue As String = String.Empty

        Dim tempWaitId As Int64
        Dim tempWaitIdList As New List(Of String)
        Dim DTB As New DocTypesBusiness

        Try
            dsWaitIDs = Results_Factory.GetZWFIbyWI(lngWaitID)

            If Not IsDBNull(dsWaitIDs) AndAlso Not IsNothing(dsWaitIDs.Tables(0)) AndAlso dsWaitIDs.Tables(0).Rows.Count > 0 Then

                For Each r As DataRow In dsWaitIDs.Tables(0).Rows

                    tempDocTypeID = Convert.ToInt32(r("DTID"))
                    tempDocTypeName = DTB.GetDocTypeName(tempDocTypeID)

                    tempWaitIdList = New List(Of String)
                    tempWaitId = Convert.ToInt64(r("WI"))

                    tempWaitIdList.Add(tempDocTypeName)

                    dsIndexs = Results_Factory.GetZWFIIbyWI(tempWaitId)

                    If Not IsDBNull(dsIndexs) AndAlso Not IsNothing(dsIndexs.Tables(0)) AndAlso dsIndexs.Tables(0).Rows.Count > 0 Then

                        For Each r2 As DataRow In dsIndexs.Tables(0).Rows

                            tempIndexID = Convert.ToInt64(r2("IID"))
                            tempIndexName = IndexsBusiness.GetIndexName(tempIndexID)
                            tempIndexValue = r2("IValue").ToString()

                            tempIndex = tempIndexName + ": " + tempIndexValue

                            tempWaitIdList.Add(tempIndex)

                        Next

                    End If

                Next

            End If

            DTB = Nothing

        Catch ex As Exception
            ZClass.raiseerror(ex)
            tempWaitIdList = New List(Of String)
        End Try

        Return tempWaitIdList

    End Function


#End Region

#Region "CompleteDocument"


    Public Sub CompleteDocument(ByRef _Result As IResult, ByVal loadIndexsList As Boolean) Implements IResults_Business.CompleteDocument
        Dim dr As IDataReader = Nothing
        Dim con As IConnection

        Dim RF As New Results_Factory
        Dim ASB As New AutoSubstitutionBusiness
        Dim refIndexs As List(Of ReferenceIndex) = (New ReferenceIndexBusiness).GetReferenceIndexesByDoctypeId(_Result.DocTypeId)
        Try
            dr = RF.CompleteDocument(_Result.ID, _Result.DocTypeId, _Result.Indexs, con, refIndexs)

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
                                _Result.IsShared = True
                            Else
                                _Result.IsShared = False
                            End If
                        Else
                            _Result.IsShared = False
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

                        If IsDBNull(dr.Item(13)) Then
                            _Result.OriginalName = String.Empty
                        Else
                            _Result.OriginalName = dr.Item(13)
                        End If
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
                        If IsDBNull(dr.Item(16)) = False Then
                            _Result.DISK_VOL_PATH = dr.Item(16)
                        Else
                            _Result.DISK_VOL_PATH = String.Empty
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    Try
                        _Result.UserId = _Result.Platter_Id
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

                    If Not IsDBNull(dr("IsImportant")) Then
                        _Result.IsImportant = CBool(dr.Item("IsImportant"))
                    End If

                    If Not IsDBNull(dr("IsFavorite")) Then
                        _Result.IsFavorite = CBool(dr.Item("IsFavorite"))
                    End If

                    Dim i As Int16
                    For i = 0 To _Result.Indexs.Count - 1
                        Try
                            If Not IsDBNull(dr.GetValue(dr.GetOrdinal("I" & DirectCast(_Result.Indexs(i), Index).ID))) Then
                                DirectCast(_Result.Indexs(i), Index).Data = dr.GetValue(dr.GetOrdinal("I" & DirectCast(_Result.Indexs(i), Index).ID)).ToString
                                DirectCast(_Result.Indexs(i), Index).DataTemp = dr.GetValue(dr.GetOrdinal("I" & DirectCast(_Result.Indexs(i), Index).ID)).ToString
                                'Si el indice es de tipo Sustitucion
                                If DirectCast(_Result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustitución OrElse DirectCast(_Result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                    'Se carga la descripcion de Indice
                                    If loadIndexsList Then
                                        DirectCast(_Result.Indexs(i), Index).dataDescription = ASB.getDescription(DirectCast(_Result.Indexs(i), Index).Data, DirectCast(_Result.Indexs(i), Index).ID)
                                    End If
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

            ASB = Nothing

        End Try
    End Sub
    Public Function GetName(ByVal ResultId As Int64, ByVal DocTypeId As Int64) As String Implements IResults_Business.GetName
        Dim selstr As String
        selstr = Results_Factory.GetName(ResultId, DocTypeId)
        Return selstr
    End Function
    Public Function GetFullName(ByVal resultId As Int64, ByVal docTypeId As Int64) As String Implements IResults_Business.GetFullName
        Return Results_Factory.GetFullName(resultId, docTypeId)
    End Function
#End Region

#Region "GetNewResult"

    Public Function GetNewResult(ByVal docTypeId As Int64, ByVal File As String) As INewResult Implements IResults_Business.GetNewResult
        Dim DTB As New DocTypesBusiness
        Dim CurrentDocType As IDocType = DTB.GetDocType(docTypeId)

        Dim NR As INewResult = GetNewNewResult(CurrentDocType, , File)

        DTB = Nothing
        Return NR
    End Function
    Public Function GetNewNewResult(ByVal docId As Int64, ByVal docType As IDocType) As INewResult Implements IResults_Business.GetNewNewResult
        Dim Result As New NewResult
        Result.ID = docId
        Result.DocType = docType
        CompleteDocument(DirectCast(Result, NewResult), True)
        Return Result
    End Function
    Public Function GetNewResult(ByVal docId As Int64, ByVal docType As IDocType) As IResult Implements IResults_Business.GetNewResult
        Dim Result As New Result()
        Result.ID = docId
        Result.DocType = docType
        Result.Indexs = New IndexsBusiness().GetIndexsData(Result.ID, Result.DocTypeId)
        CompleteDocument(Result, True)
        Return Result
    End Function

    Public Function GetResult(ByVal docId As Int64, ByVal docTypeId As Int64, ByVal FullLoad As Boolean) As IResult Implements IResults_Business.GetResult
        Dim Result As New Result()
        Dim DTB As New DocTypesBusiness
        Result.ID = docId
        If docTypeId > 0 Then
            Result.DocType = DTB.GetDocType(docTypeId)
            Result.DocTypeId = docTypeId

            If FullLoad Then
                If Not IsNothing(Result.DocType) Then
                    Result.Indexs = New IndexsBusiness().GetIndexsData(Result.ID, Result.DocTypeId)
                End If
            End If
            If docId > 0 Then CompleteDocument(Result, FullLoad)
        End If
        DTB = Nothing
        Return Result
    End Function

    Public Function GetResults(ByVal DocTypeId As Integer) As DataTable Implements IResults_Business.GetResults
        Return Results_Factory.GetResults(DocTypeId)
    End Function

    Public Function getPermisosInsert() As DataTable Implements IResults_Business.getPermisosInsert
        Return Results_Factory.getPermisosInsert()
    End Function

    ''' <summary>
    ''' Consulta DoshowTable
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSelectDoShowTable() As DataTable Implements IResults_Business.GetSelectDoShowTable
        Dim RF As New Results_Factory
        Return RF.GetResultsDoshhowtable()
    End Function

    Public Function GetNewNewResult(ByVal docTypeId As Long) As INewResult Implements IResults_Business.GetNewNewResult
        Dim Result As New NewResult()
        Dim DTB As New DocTypesBusiness
        Result.DocType = DTB.GetDocType(docTypeId)
        Result.DocTypeId = docTypeId
        Result.Indexs = ZCore.GetInstance().FilterIndex(Result.DocTypeId)
        '        LoadIndexs(DirectCast(Result, NewResult))
        DTB = Nothing
        Return Result
    End Function

    ''' <summary>
    ''' Obtiene un datarow con todos los datos del documento
    ''' </summary>
    ''' <param name="docId">Id del documento</param>
    ''' <param name="docTypeId">Id del entidad</param>
    ''' <returns>Datarow del documento</returns>
    ''' <history> Marcelo modified 20/08/2009 </history>
    ''' <remarks></remarks>
    Public Function GetResultRow(ByVal docId As Int64, ByVal docTypeId As Int64) As DataRow Implements IResults_Business.GetResultRow
        Dim RF As New Results_Factory
        Dim ASB As New AutoSubstitutionBusiness
        Try
            Dim indices As Index() = ZCore.GetInstance().FilterCIndex(docTypeId)

            Dim r As DataRow = RF.CompleteDocument(docId, docTypeId, indices)
            If Not IsNothing(r) Then
                For Each indice As IIndex In indices
                    If indice.DropDown = IndexAdditionalType.AutoSustitución OrElse indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then

                        If (Not r.Table.Columns.Contains(indice.Name)) Then

                            r.Table.Columns.Add(indice.Name)

                            If Not IsDBNull(r.Item("I" & indice.ID)) Then
                                r.Item(indice.Name) = ASB.getDescription(r.Item("I" & indice.ID), indice.ID)

                            Else
                                r.Item(indice.Name) = String.Empty
                            End If
                        End If
                    End If
                Next
            End If
            Return r
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        Finally

            ASB = Nothing
        End Try
    End Function
#End Region

#Region "Tasks"
    Public Function GetNewTaskResult(ByVal DocId As Int64, ByVal DocType As IDocType) As ITaskResult Implements IResults_Business.GetNewTaskResult
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
    Public Sub LoadVolume(ByRef result As INewResult) Implements IResults_Business.LoadVolume
        result.VolumeListId = VolumesBusiness.GetVolumeListId(result.DocType.ID)
        result.DsVols = VolumeListsBusiness.GetActiveDiskGroupVolumes(result.VolumeListId)
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
    Public Sub Delete(ByRef Result As IResult, Optional ByVal delfile As Boolean = True, Optional ByVal saveAction As Boolean = True) Implements IResults_Business.Delete
        Dim RF As New Results_Factory
        If Result.Indexs.Count = 0 Then
            Result.Indexs = ZCore.GetInstance().FilterIndex(Result.DocType.ID)
        End If
        RF.Delete(Result, delfile)

        'added this call to delete the doc from zsearchValues_DT for Duke (sebastian 19-03-2009)
        DeleteSearchIndexData(Result.ID)
        If (saveAction = True) Then
            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UB.SaveAction(Result.ID, ObjectTypes.Documents, RightsType.Delete, "Se elimino el documento: " & Result.Name)
        End If
    End Sub

    Public Sub Delete(ByRef Result As INewResult, Optional ByVal delfile As Boolean = True) Implements IResults_Business.Delete
        Dim RF As New Results_Factory
        RF.Delete(Result, delfile)

    End Sub

    Public Sub Delete(ByRef Result As INewResult, ByVal delfile As Boolean, ByRef t As ITransaction) Implements IResults_Business.Delete
        Dim RF As New Results_Factory
        RF.Delete(Result, delfile, t)


    End Sub
    Public Sub RemoveDocument(ByVal docid As Int64, docTypeId As Int64)
        Dim DocTypeName As String = FuncionesZamba.GetDocTypeNameById(docTypeId)
        Results_Factory.RemoveDocument(docid, docTypeId)
        UB.SaveAction(docid.ToString, ObjectTypes.Documents, RightsType.Delete, "Se elimino el documento con id " + docid.ToString + " del tipo " + DocTypeName, MembershipHelper.CurrentUser.ID)
    End Sub
    '''<summary>
    '''  Borra un result de un workflow
    '''</summary>
    Public Sub DeleteResultFromWorkflows(ByVal docid As Int64) Implements IResults_Business.DeleteResultFromWorkflows
        Results_Factory.DeleteResultFromWorkflows(docid)
    End Sub
#End Region

#Region "DOCFILE"
    Public Sub UpdateDocFile(ByRef Result As IResult, ByVal docfile As String) Implements IResults_Business.UpdateDocFile
        If IsNothing(Result.EncodedFile) Then
            Results_Factory.updatedocfile(Result, docfile)
        Else
            Dim encode As Byte() = FileEncode.Encode(docfile)

            'Verifica si hubo un error al actualizar
            If encode IsNot Nothing Then
                Result.EncodedFile = encode
                Results_Factory.UpdateDOCB(Result)
            End If
        End If
    End Sub

    Public Shared Function CreateTempFile(ByVal Result As IResult) As String
        If Result IsNot Nothing AndAlso Not String.IsNullOrEmpty(Result.FullPath) AndAlso Result.FullPath.IndexOf("aspx", StringComparison.CurrentCultureIgnoreCase) = -1 Then
            Dim fi As FileInfo = Nothing
            Dim FTemp As FileInfo = Nothing
            Dim dir As IO.DirectoryInfo

            Try
                fi = New FileInfo(Result.FullPath)
                dir = GetTempDir("\OfficeTemp")
                If dir.Exists = False Then dir.Create()

                If Result.IsExcel OrElse Result.IsWord Then
                    'Esto evita el error de abrir 2 excel con el mismo nombre (abrirlo en resultado y tareas)
                    FTemp = New FileInfo(FileBusiness.GetUniqueFileName(dir.FullName, fi.Name))
                Else
                    FTemp = New FileInfo(dir.FullName & "\" & fi.Name)
                End If

                Try
                    If FTemp.Exists = False Then
                        Dim RB As New Results_Business
                        RB.CopyFileToTemp(Result, fi.FullName, FTemp.FullName)
                        RB = Nothing
                    End If
                    FTemp.Attributes = IO.FileAttributes.Normal
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                If Result.FullPath.ToUpper.EndsWith(".HTML") Or Result.FullPath.ToUpper.EndsWith(".HTM") Then
                    Dim RB As New Results_Business
                    RB.CopySubDirAndFilesBrowser(dir.FullName, Result.FullPath.Remove(Result.FullPath.LastIndexOf("\")), Result.FullPath.Remove(Result.FullPath.LastIndexOf("\")))
                    RB = Nothing
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
    Public Sub CopyFileToTemp(ByVal res As IResult, ByVal rootPath As String, ByVal destPath As String) Implements IResults_Business.CopyFileToTemp
        'Verifica si el volumen es de tipo base de datos
        If res.Disk_Group_Id > 0 AndAlso VolumesBusiness.GetVolumeType(res.Disk_Group_Id) = VolumeType.DataBase Then

            'traer el archivo desde la base
            LoadFileFromDB(res)

            'Verifica si se debe codificar (en caso de ser la primera vez en abrirse)
            If IsNothing(res.EncodedFile) Then
                'Copia del servidor el documento
                File.Copy(rootPath, destPath, True)
                'Lo codifica
                res.EncodedFile = FileEncode.Encode(destPath)
                'Se guarda en la base el archivo
                InsertIntoDOCB(res)
            Else
                'Si el documento ya habia sido codificado, lo decodifica en la ruta de destino.
                FileEncode.Decode(destPath, res.EncodedFile)
            End If

        Else
            'Si el volumen es en disco rigido simplemente lo copia
            File.Copy(rootPath, destPath, True)
        End If

    End Sub

    'Ezequiel: Se comenta funcionalidad ZIP
    'Public Function ChechIfMustZipBlob(ByVal fileName As String) As Boolean

    '    Dim ext As String = zopt.GetValue("BLOB_NOTZIP")

    '    If Not String.IsNullOrEmpty(ext) Then

    '        Dim FI As New FileInfo(fileName)

    '        If (ext.Contains(FI.Extension)) Then
    '            Return False
    '        End If

    '    End If

    '    Return True

    'End Function

    Public Sub InsertIntoDOCB(ByVal res As IResult) Implements IResults_Business.InsertIntoDOCB
        'Ezequiel: Se comenta funcionalidad ZIP
        'Dim isZipped As Integer = 0

        'If ChechIfMustZipBlob(res.File) Then
        '    res.EncodedFile = FileEncode.Zip(res.EncodedFile)
        '    isZipped = 1
        'End If

        Results_Factory.InsertResIntoDOCB(res, False)
    End Sub

    Public Sub LoadFileFromDB(ByRef res As IResult) Implements IResults_Business.LoadFileFromDB
        res.EncodedFile = Results_Factory.LoadFileFromDB(res.ID, res.DocTypeId)

        'Ezequiel: Se comenta la implementacion de ZIP
        'If Results_Factory.GetIfFileIsZipped(res.ID, res.DocTypeId) Then
        '    res.EncodedFile = FileEncode.UnZip(res.EncodedFile)
        'End If
    End Sub
    Public Function LoadFileFromDB(ByVal docId As Int64, ByVal dopcTypeId As Int64) As Byte() Implements IResults_Business.LoadFileFromDB
        Return Results_Factory.LoadFileFromDB(docId, dopcTypeId)
    End Function

    ''' <summary>
    ''' [Sebastian 28-07-2009] Se agrego validacion para la ruta del archivo en caso de que venga vacia
    ''' </summary>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    Public Sub copyFile(ByRef result As INewResult) Implements IResults_Business.copyFile
        Dim fi As FileInfo = Nothing

        Try

            fi = New FileInfo(result.File)
            result.OriginalName = result.File

            If fi.Exists Then
                Dim fn = New FileInfo(result.NewFile)
                If fn.Directory.Exists = False Then
                    fn.Directory.Create()
                End If


                If String.IsNullOrEmpty(result.NewFile) = False Then
                    fi.CopyTo(result.NewFile, True)
                Else
                    Throw New Exception("El Archivo destino no esta esecificado, verifique la existencia del mismo")
                End If

                If String.Compare(fi.Extension, ".html") = 0 Or String.Compare(fi.Extension, ".htm") = 0 Then
                    InsertWebDocument(fi, New FileInfo(result.NewFile))
                End If
            Else
                Throw New Exception("El Archivo origen no existe o no se puede acceder a el, verifique la existencia del mismo: " & fi.FullName)
            End If
        Finally
            fi = Nothing
        End Try
    End Sub
    Private Shared Sub MoveFile(ByRef newResult As INewResult)
        Dim fi As FileInfo = Nothing
        Try
            fi = New FileInfo(newResult.File)
            If fi.Exists Then
                Dim fn = New FileInfo(newResult.NewFile)
                If fn.Directory.Exists = False Then
                    'fn.Directory.Create()
                    Directory.CreateDirectory(fn.Directory.ToString())
                End If

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

    Public Sub SetVersionComment(ByVal rID As Int64, ByVal rComment As String) Implements IResults_Business.SetVersionComment
        Results_Factory.SetVersionComment(rID, rComment)
    End Sub



    Public Sub HistoricDocumentDropzone(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Long, ByVal docTypeName As String, ByVal stepId As Long, ByVal WorkId As Long, ByVal statename As String, stepname As String) Implements IResults_Business.HistoricDocumentDropzone
        Dim RF As New Results_Factory
        Dim WFB As New WFBusiness
        RF.LogDropzoneHistory(taskID, taskName, docTypeId, docTypeName, stepId, WorkId, statename, stepname, WFB.GetWorkflowNameByWFId(WorkId))
        WFB = Nothing
    End Sub

    Public Sub ReplaceDocument(ByRef Result As IResult, ByVal NewDocumentFile As String, ByVal ComeFromWF As Boolean, ByVal t As ITransaction) Implements IResults_Business.ReplaceDocument
        Dim RF As New Results_Factory

        Try
            'Verifica si el volumen es de tipo base de datos o si todavia no se inserto el documento

            Dim RB As New Results_Business

            Dim VolumeListId As Int32 = VolumesBusiness.GetVolumeListId(Result.DocType.ID)
            Dim DsVols As DataSet = VolumeListsBusiness.GetActiveDiskGroupVolumes(VolumeListId)
            Dim Volume As IVolume = VolumesBusiness.LoadVolume(Result.DocType.ID, DsVols)

            If Volume IsNot Nothing AndAlso Volume.VolumeType = VolumeTypes.DataBase Then
                'Se reemplaza el documento en Zamba
                Result.EncodedFile = FileEncode.Encode(NewDocumentFile)
                Result.File = NewDocumentFile
                Result.Doc_File = Result.ID & Path.GetExtension(NewDocumentFile)
                Result.IconId = RB.GetFileIcon(Result.File)
                Result.Disk_Group_Id = Volume.ID
                Dim NewFi As New IO.FileInfo(NewDocumentFile)
                RF.ReplaceDocument(Result, NewFi, Result.Doc_File, ComeFromWF, t)

                If Volume.VolumeType = VolumeTypes.DataBase Then
                    RF.ReplaceDigitalDocument(Result)
                End If

                RF.InsertIndexerState(Result.DocTypeId, Result.ID, 0, t)

                'Se copia al temporal para ser abierto al refrescar la tarea
                Dim dirInfo As DirectoryInfo = Tools.EnvironmentUtil.GetTempDir("\OfficeTemp")
                Dim destPath As String = dirInfo.FullName & "\" & Result.Doc_File
                File.Copy(NewDocumentFile, destPath, True)
                dirInfo = Nothing
                RB = Nothing

            ElseIf Volume IsNot Nothing Then
                Dim NewFi As New IO.FileInfo(NewDocumentFile)
                Result.OffSet = Volume.offset

                Dim NewFullFileName As String = VolumesBusiness.VolumePath(Volume, Result.DocType.ID) & "\" & Result.ID & NewFi.Extension
                Dim NewFullFileNamePDFPreview As String = VolumesBusiness.VolumePath(Volume, Result.DocType.ID) & "\" & Result.ID & NewFi.Extension & ".pdf"

                ''Custom migration for FileNet Format
                If (NewFi.Extension.Contains(".__")) Then
                    NewFullFileName = NewFullFileName.Replace(".__1", New FileInfo(Result.OriginalName).Extension)
                    NewFullFileNamePDFPreview = NewFullFileNamePDFPreview.Replace("__1", New FileInfo(Result.OriginalName).Extension)
                End If

                If File.Exists(NewFullFileNamePDFPreview) Then
                    File.Delete(NewFullFileNamePDFPreview)
                End If

                NewFi.CopyTo(NewFullFileName, True)
                Result.File = NewDocumentFile
                Result.IconId = RB.GetFileIcon(Result.File)
                Result.Doc_File = Path.GetFileName(NewFullFileName)
                Result.Disk_Group_Id = Volume.ID
                Result.DISK_VOL_PATH = Volume.path
                'Se reemplaza el documento y se refresca la tarea completa
                RF.ReplaceDocument(Result, NewFi, Result.Doc_File, ComeFromWF, Nothing)
                RF.InsertIndexerState(Result.DocTypeId, Result.ID, 0, Nothing)

            Else
                ZTrace.WriteLineIf(ZTrace.IsError, "No hay volumen disponible para el reemplazo")
                Throw New Exception("No hay volumen disponible para el reemplazo")
            End If
            Dim UB As New UserBusiness
            UB.SaveAction(Result.ID, ObjectTypes.Documents, RightsType.Edit, Result.Name)
            UB = Nothing
            If File.Exists(Result.FullPath) Then
                File.Delete(Result.FullPath)
                File.Copy(Result.File, Result.FullPath)
            End If
            DeleteDocumentPreview(Result)
        Catch ex As IOException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Public Sub ReplaceDocumentForZeditor(ByRef Result As IResult, ByVal NewDocumentFile As String, ByVal ComeFromWF As Boolean, ByVal t As ITransaction, ByVal ZEditorUrl As String)
        Dim RF As New Results_Factory

        Try
            'Verifica si el volumen es de tipo base de datos o si todavia no se inserto el documento

            Dim RB As New Results_Business

            Dim VolumeListId As Int32 = VolumesBusiness.GetVolumeListId(Result.DocType.ID)
            Dim DsVols As DataSet = VolumeListsBusiness.GetActiveDiskGroupVolumes(VolumeListId)
            Dim Volume As IVolume = VolumesBusiness.LoadVolume(Result.DocType.ID, DsVols)

            If Volume IsNot Nothing AndAlso Volume.VolumeType = VolumeTypes.DataBase Then
                'Se reemplaza el documento en Zamba
                Result.EncodedFile = FileEncode.Encode(NewDocumentFile)
                Result.File = NewDocumentFile
                Result.Doc_File = Result.ID & Path.GetExtension(NewDocumentFile)
                Result.IconId = RB.GetFileIcon(Result.File)
                Result.Disk_Group_Id = Volume.ID
                Dim NewFi As New IO.FileInfo(NewDocumentFile)
                RF.ReplaceDocument(Result, NewFi, Result.Doc_File, ComeFromWF, t)

                If Volume.VolumeType = VolumeTypes.DataBase Then
                    RF.ReplaceDigitalDocument(Result)
                End If

                RF.InsertIndexerState(Result.DocTypeId, Result.ID, 0, t)

                'Se copia al temporal para ser abierto al refrescar la tarea

                'Dim dirInfo As DirectoryInfo = Tools.EnvironmentUtil.GetTempDir("\OfficeTemp")
                Dim dirInfo As String = ZEditorUrl + "\OfficeTemp"
                Dim destPath As String = dirInfo & "\" & Result.Doc_File

                If Not Directory.Exists(dirInfo) Then
                    Directory.CreateDirectory(dirInfo)
                End If

                File.Copy(NewDocumentFile, destPath, True)
                dirInfo = Nothing
                RB = Nothing

            ElseIf Volume IsNot Nothing Then
                Dim NewFi As New IO.FileInfo(NewDocumentFile)
                Result.OffSet = Volume.offset

                Dim NewFullFileName As String = VolumesBusiness.VolumePath(Volume, Result.DocType.ID) & "\" & Result.ID & NewFi.Extension
                Dim NewFullFileNamePDFPreview As String = VolumesBusiness.VolumePath(Volume, Result.DocType.ID) & "\" & Result.ID & NewFi.Extension & ".pdf"

                ''Custom migration for FileNet Format
                If (NewFi.Extension.Contains(".__")) Then
                    NewFullFileName = NewFullFileName.Replace(".__1", New FileInfo(Result.OriginalName).Extension)
                    NewFullFileNamePDFPreview = NewFullFileNamePDFPreview.Replace("__1", New FileInfo(Result.OriginalName).Extension)
                End If

                If File.Exists(NewFullFileNamePDFPreview) Then
                    File.Delete(NewFullFileNamePDFPreview)
                End If

                NewFi.CopyTo(NewFullFileName, True)
                Result.File = NewDocumentFile
                Result.IconId = RB.GetFileIcon(Result.File)
                Result.Doc_File = Path.GetFileName(NewFullFileName)
                Result.Disk_Group_Id = Volume.ID
                Result.DISK_VOL_PATH = Volume.path
                'Se reemplaza el documento y se refresca la tarea completa
                RF.ReplaceDocument(Result, NewFi, Result.Doc_File, ComeFromWF, Nothing)
                RF.InsertIndexerState(Result.DocTypeId, Result.ID, 0, Nothing)

            Else
                ZTrace.WriteLineIf(ZTrace.IsError, "No hay volumen disponible para el reemplazo")
                Throw New Exception("No hay volumen disponible para el reemplazo")
            End If
            Dim UB As New UserBusiness
            UB.SaveAction(Result.ID, ObjectTypes.Documents, RightsType.Edit, Result.Name)
            UB = Nothing
            If File.Exists(Result.FullPath) Then
                File.Delete(Result.FullPath)
                File.Copy(Result.File, Result.FullPath)
            End If
            DeleteDocumentPreview(Result)
        Catch ex As IOException
            ZClass.raiseerror(ex)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub
    Public Sub DeleteDocumentPreview(ByRef Result As IResult)
        If File.Exists(Result.FullPath + ".pdf") Then
            File.Delete(Result.FullPath + ".pdf")
        End If
    End Sub
    ''' <summary>
    ''' Inserta o actualiza un documento, en base al array de bytes y el nombre del archivo nuevo
    ''' </summary>
    ''' <param name="res"></param>
    ''' <param name="file"></param>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Public Sub InsertDocFile(ByVal res As IResult, ByVal file As Byte(), ByVal fileName As String) Implements IResults_Business.InsertDocFile
        'TO-DO: Queda hacer el  en los volumenes si es windows

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertando documento desde formulario docId " & res.ID & " doctypeid " & res.DocTypeId)

        If TypeOf res Is INewResult Then
            DirectCast(res, INewResult).EncodedFile = file
        Else
            res.EncodedFile = file
        End If

        ZTrace.WriteLineIf(ZTrace.IsInfo, "verificando que el documento exista en doc_b, docId " & res.ID)

        'Si el archivo ya esta insertado en blob se actualiza, sino lo inserta
        If Results_Factory.ExistsInDOCB(res.ID, res.DocTypeId) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ya existe un documento en blob previo, actualizando, docId " & res.ID)
            Results_Factory.UpdateDOCB(res)
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "No existe documento en blob previo, insertando, docId " & res.ID)
            Results_Factory.InsertIntoDOCB(res)
        End If

        'Se le completa el original file name si no tenia uno
        res.OriginalName = fileName

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cargando volumenes " & res.ID)
        'Cargo los volumenes para completar la info en la doc_t
        Dim VolumeListId As Integer = VolumesBusiness.GetVolumeListId(res.DocTypeId)
        Dim DsVols As DataSet = VolumeListsBusiness.GetActiveDiskGroupVolumes(VolumeListId)
        Dim vol As IVolume = VolumesBusiness.LoadVolume(res.DocTypeId, DsVols)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizando info en doc_t docId " & res.ID)
        'Actualizo la doc_t
        Results_Factory.ReplaceResultFileInfo(res, vol)
    End Sub

#Region "Interfaz WS"
    ''' <summary>
    ''' Obtiene el archivo blob del documento, llamando al WS
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <param name="docId"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWebDocFileWS(ByVal docTypeId As Long, ByVal docId As Long, ByVal userId As Long) As Byte() Implements IResults_Business.GetWebDocFileWS
        Dim wsFactory As New WSResultsFactory()
        Dim returnVal As Byte() = Nothing
        Try
            returnVal = wsFactory.ConsumeGetWebDocFile(docTypeId, docId, userId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            wsFactory.Dispose()
        End Try

        Return returnVal
    End Function

    ''' <summary>
    ''' Llama al WS para copiar el archivo blob al volumen.
    ''' </summary>
    ''' <param name="docId"></param>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyBlobToVolumeWS(ByVal docId As Long, ByVal docTypeId As Long) As Boolean Implements IResults_Business.CopyBlobToVolumeWS
        Dim wsFactory As New WSResultsFactory()
        Dim returnVal As Boolean = False
        Try
            returnVal = wsFactory.ConsumeCopyBlobToVolume(docId, docTypeId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            wsFactory.Dispose()
        End Try

        Return returnVal
    End Function

    Public Function GetBlob(ByVal docId As Long, ByVal docTypeId As Long, ByVal userid As Long) As Byte() Implements IResults_Business.GetBlob
        Dim wsFactory As New WSResultsFactory()
        Dim returnVal As Byte()
        Try
            returnVal = wsFactory.ConsumeGetWebDocFile(docTypeId, docId, userid)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            wsFactory.Dispose()
        End Try

        Return returnVal
    End Function
    ''' <summary>
    ''' Llama al ws para insertar un archivo blob a un documento zamba
    ''' </summary>
    ''' <param name="docId"></param>
    ''' <param name="docTypeId"></param>
    ''' <param name="fileBytes"></param>
    ''' <param name="incomingFile"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertDocFileWS(ByVal docId As Long, ByVal docTypeId As Long, ByVal fileBytes As Byte(), ByVal incomingFile As String, ByVal userId As Long) As Boolean Implements IResults_Business.InsertDocFileWS
        Dim wsFactory As New WSResultsFactory()
        Dim returnVal As Boolean = False
        Try
            returnVal = wsFactory.ConsumeInsertDocFile(docId, docTypeId, fileBytes, incomingFile, userId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            wsFactory.Dispose()
        End Try

        Return returnVal
    End Function
#End Region
#End Region

#Region "Versioning"

    Public Function GetParentVersionId(ByVal DocTypeid As Int64, ByVal docid As Int64) As Int64 Implements IResults_Business.GetParentVersionId
        Return Results_Factory.GetParentVersionId(DocTypeid, docid)
    End Function

    Public Function CountChildsVersions(ByVal DocTypeid As Int64, ByVal parentid As Int64) As Int32 Implements IResults_Business.CountChildsVersions
        Return Results_Factory.CountChildsVersions(DocTypeid, parentid)
    End Function

    Public Function GetNewVersionID(ByVal RootId As Long, ByVal doctype As Int32, ByVal OriginalDocId As Int64) As Int32 Implements IResults_Business.GetNewVersionID
        Dim id As Int32
        id = Results_Factory.GetNewVersionID(RootId, doctype, OriginalDocId)
        Return id
    End Function

    Private Shared Sub setParentVersion(ByVal docTypeId As Int32, ByVal docId As Int64)
        Results_Factory.setParentVersion(docTypeId, docId)
    End Sub

    Public Function InsertNewVersion(ByVal OriginalResult As IResult, ByVal Comment As String) As IResult Implements IResults_Business.InsertNewVersion
        ZTrace.WriteLineIf(ZTrace.IsInfo, "3")
        Dim ClonedResult As INewResult = CloneResult(OriginalResult, OriginalResult.RealFullPath, False)
        ClonedResult.Comment = Comment

        For index As Integer = 0 To ClonedResult.Indexs.Count - 1
            Dim value As Object = ClonedResult.Indexs.Item(index).Data
            ClonedResult.Indexs.Item(index).DataTemp = value
        Next

        Insert(ClonedResult, False, False, False, False, False)


        OriginalResult.HasVersion = 1

        setParentVersion(OriginalResult.DocType.ID, OriginalResult.ID)

        SaveVersionComment(ClonedResult)
        ' las nuevas versiones herendan de sus padres los usuarios a notificar de todos los rubros
        InheritUsersToNotify(OriginalResult.ID, ClonedResult.ID)

        Return ClonedResult

    End Function

    Public Function InsertNewVersionNoComment(ByVal OriginalResult As IResult) As IResult Implements IResults_Business.InsertNewVersionNoComment
        Dim ClonedResult As INewResult = CloneResult(OriginalResult, OriginalResult.RealFullPath, False)

        'ClonedResult.Comment = Comment

        For index As Integer = 0 To ClonedResult.Indexs.Count - 1
            Dim value As Object = ClonedResult.Indexs.Item(index).Data
            ClonedResult.Indexs.Item(index).DataTemp = value
        Next

        Insert(ClonedResult, False, False, False, False, False)


        OriginalResult.HasVersion = 1

        setParentVersion(OriginalResult.DocType.ID, OriginalResult.ID)

        ' las nuevas versiones herendan de sus padres los usuarios a notificar de todos los rubros
        InheritUsersToNotify(OriginalResult.ID, ClonedResult.ID)

        Return ClonedResult
    End Function

    Public Sub InheritUsersToNotify(ByVal oldResultId As Int64, ByVal newResultId As Int64) Implements IResults_Business.InheritUsersToNotify
        Dim ds As DataSet = NotifyBusiness.GetAllData(oldResultId)
        If Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows

                'SaveAllData(ByVal doc_id As Int64, ByVal typeid As Int32, ByVal userid As Int64, ByVal extradata As String, ByVal groupid As Int64)
                NotifyBusiness.SaveAllData(newResultId, row.Item(0), row.Item(2), row.Item(3), row.Item(4))
            Next
        End If

    End Sub


    'Public Function InsertNewVersion(ByVal OriginalResult As IResult, ByVal Comment As String, ByVal newResultPath As String) As IResult
    ' DIEGO: COMENTE ESTA SOBRECARGA PORQUE AL PARECER NO SE UTILIZA
    '    Dim ClonedResult As INewResult = CloneResult(OriginalResult, newResultPath, False)
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

    Public Function InsertNewVersionNoComment(ByVal OriginalResult As IResult, ByVal newResultPath As String) As IResult Implements IResults_Business.InsertNewVersionNoComment
        ZTrace.WriteLineIf(ZTrace.IsInfo, "5")
        Dim ClonedResult As INewResult = CloneResult(OriginalResult, newResultPath, False)
        'ClonedResult.Comment = Comment

        For index As Integer = 0 To ClonedResult.Indexs.Count - 1
            Dim value As Object = ClonedResult.Indexs.Item(index).Data
            ClonedResult.Indexs.Item(index).DataTemp = value
        Next

        Insert(ClonedResult, False, False, False, False, False)

        OriginalResult.HasVersion = 1

        setParentVersion(OriginalResult.DocType.ID, OriginalResult.ID)

        ' las nuevas versiones herendan de sus padres los usuarios a notificar de todos los rubros
        InheritUsersToNotify(OriginalResult.ID, ClonedResult.ID)
        Return ClonedResult

    End Function

    Public Sub SaveVersionComment(ByRef Result As INewResult) Implements IResults_Business.SaveVersionComment
        Results_Factory.SaveVersionComment(Result)
    End Sub




#End Region

#Region "AutoName"
    ''' <summary>
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Ezequiel]	20/01/2009	Modified
    ''' </history>
    Public Sub UpdateAutoName(ByRef Result As IResult, Optional ByVal ChNomC As Boolean = False) Implements IResults_Business.UpdateAutoName
        Dim AutoNameCode As String
        Dim DocTypeName As String
        Dim DTB As New DocTypesBusiness
        Dim DTF As New DocTypesFactory
        Dim RF As New Results_Factory

        If Not Result.DocType Is Nothing AndAlso Result.DocType.AutoNameCode.Length = 0 Then
            AutoNameCode = DTB.GetDocType(Result.DocType.ID).AutoNameCode
        Else
            AutoNameCode = Result.DocType.AutoNameCode
        End If
        If Not Result.DocType Is Nothing AndAlso Result.DocType.Name.Length = 0 Then
            DocTypeName = DTB.GetDocTypeName(Result.DocType.ID)
        Else
            DocTypeName = Result.DocType.Name
        End If

        If Not Result.Indexs Is Nothing AndAlso Result.Indexs.Count > 0 Then
            If Not ChNomC OrElse Result.Name = String.Empty Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre Anterior:" & Result.Name)
                Result.Name = DTF.GetAutoName(AutoNameCode, DocTypeName, Result.CreateDate, Result.EditDate, Result.Indexs).Trim
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Nuevo Nombre:" & Result.Name)
                If Result.ID <> 0 Then RF.SaveName(Result)
            End If
        Else
            ZClass.raiseerror(New Exception("Los indices no estan cargados para el result, no se ejecuta el autoname" + "  -  " + My.Application.Info.StackTrace.ToString()))
        End If

        DTB = Nothing
        DTF = Nothing

    End Sub

#End Region

#Region "Link"
    Public Function GetLinkFromResult(ByRef Result As IResult) As String Implements IResults_Business.GetLinkFromResult
        Return "Zamba:\\DT=" & Result.DocType.ID & "&DOCID=" & Result.ID
    End Function

    Public Function GetHtmlLinkFromResult(ByVal rDocTypeId As Int64,
                                                 ByVal rId As Int64) As String Implements IResults_Business.GetHtmlLinkFromResult
        Dim link As New StringBuilder

        link.Append("<a href=")
        link.Append(Chr(34))
        link.Append("zamba:\\DT=")
        link.Append(rDocTypeId)
        link.Append("&DOCID=")
        link.Append(rId)
        link.Append(Chr(34))
        link.Append(">")
        link.Append("Acceso al Documento")
        link.Append("</a>")

        Return link.ToString()
    End Function
#End Region

#Region "HTML Documents"
    'Funciones para Mover archivos HTML
    Private Shared Sub InsertWebDocument(ByVal forigen As FileInfo, ByVal fDestino As FileInfo)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "6")
        Dim dir As DirectoryInfo = forigen.Directory 'Obtengo el directorio origen donde esta el "html"
        'Obtengo el subdirectorio que tenga el nombredeorigen + "_"
        'Dim dirorigen As DirectoryInfo = dir.GetDirectories(forigen.Name.Split("."c)(0) & "_")(0)
        Dim subdirname As String = forigen.Name.Split(".html")(0)

        'Verifica si contiene un directorio con el mismo nombre
        If dir.GetDirectories(subdirname).Length > 0 Then
            Try
                'valida si existe el primer elemento del metodo
                Dim SubDir As DirectoryInfo = dir.GetDirectories(subdirname)(0)


                'Formo el directorio destino con el path destino y el nombre del primer directorio
                Dim dirDest As New DirectoryInfo(fDestino.DirectoryName) '& "\" & SubDir.Name)
                CopySubDirAndFiles(SubDir, dirDest)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If

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
    Public Sub CopySubDirAndFilesBrowser(ByVal copyTo As String, ByVal copyFrom As String, ByVal originalPath As String) Implements IResults_Business.CopySubDirAndFilesBrowser
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
    Public Function GetCreatorUser(ByVal docid As Int64) As String Implements IResults_Business.GetCreatorUser
        'todo estaria bueno incluir esto en las DOC_T****
        Return Results_Factory.GetCreatorUser(docid)
    End Function
#End Region

#Region "Update Result"
    Public Sub UpdateResultsVersionedDataWhenDelete(ByVal DocTypeid As Int64, ByVal parentid As Int64, ByVal docid As Int64, ByVal RootDocumentId As Int64) Implements IResults_Business.UpdateResultsVersionedDataWhenDelete
        Results_Factory.UpdateResultsVersionedDataWhenDelete(DocTypeid, parentid, docid, RootDocumentId)
    End Sub

    Public Sub UpdateLastResultVersioned(ByVal doctypeId As Int64, ByVal parentid As Int64) Implements IResults_Business.UpdateLastResultVersioned
        Results_Factory.UpdateLastResultVersioned(doctypeId, parentid)
    End Sub

    Public Sub UpdateOriginalName(ByVal DocTypeId As Int64, ByVal DocId As Int64, ByVal strOriginalName As String) Implements IResults_Business.UpdateOriginalName
        Results_Factory.UpdateOriginalName(DocTypeId, DocId, strOriginalName)
    End Sub
#End Region

    ''' <summary>
    ''' Valida si este este result ya esta asignado a otro WF
    ''' </summary>
    ''' <param name="resultID">ID del result a validar</param>
    ''' <returns>True si esta asignado, False si no</returns>
    ''' <remarks></remarks>
    Public Function ExistsInOtherWFs(ByRef ResultID As Integer, Entityid As Int64) As Boolean Implements IResults_Business.ExistsInOtherWFs
        Try
            If ResultID = 0 Then Return False
            Dim resultCount As Object = Server.Con.ExecuteScalar(CommandType.Text, "SELECT count(1) FROM wfdocument  " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "") & "  WHERE doc_id = " & ResultID.ToString() & " and doc_type_id = " & Entityid)
            Return CInt(resultCount) > 0
            resultCount = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function





    Public Sub Fill(ByRef instance As IResult) Implements IResults_Business.Fill
        Dim IB As New IndexsBusiness

        If IsNothing(instance.ChildsResults) Then

        End If
        If IsNothing(instance.DocType) Then
            ' TODO
        End If
        If IsNothing(instance.Indexs) Then
            instance.Indexs.AddRange(IB.GetIndexsData(instance.ID, instance.DocTypeId))
        End If
        If IsNothing(instance.Picture) Then
            ' TODO
        End If
        If IsNothing(instance.PrintPicture) Then
            ' TODO
        End If

        IB = Nothing
    End Sub
    Public Sub Fill(ByRef instance As ITaskResult) Implements IResults_Business.Fill
        If IsNothing(instance.State) Then

        End If
        If IsNothing(instance.UserRules) Then

        End If
        If IsNothing(instance.WfStep) Then
            Dim WFSB As New WFStepBusiness

            instance.WfStep = WFSB.GetStepById(instance.StepId)
            WFSB = Nothing
        End If

    End Sub
    Public Sub Fill(ByRef instance As INewResult) Implements IResults_Business.Fill
        If IsNothing(instance.DsVols) Then
            instance.DsVols = VolumeListsBusiness.GetActiveDiskGroupVolumes(instance.VolumeListId)
        End If
        If IsNothing(instance.Volume) Then

        End If

    End Sub
    Public Sub Fill(ByRef instance As Ipublishable) Implements IResults_Business.Fill
        If IsNothing(instance.PublishDate) Then

        End If

    End Sub
    Public Sub Fill(ByRef instance As IZBaseCore) Implements IResults_Business.Fill
    End Sub
    Public Sub Fill(ByRef instance As IZambaCore) Implements IResults_Business.Fill
        If IsNothing(instance.Childs) Then

        End If

        'Analizar si el Parent de ZambaCore hay que cargarlo. Que este en Nothing 
        '
        'instance.Parent
    End Sub
    Public Sub Fill(ByRef instance As IBaseImageFileResult) Implements IResults_Business.Fill
    End Sub
    Public Sub Fill(ByRef instance As IZBatch) Implements IResults_Business.Fill
        If IsNothing(instance.CreateDate) Then

        End If
        If IsNothing(instance.Results) Then

        End If
    End Sub
    Public Sub Fill(ByRef instance As IPublishState) Implements IResults_Business.Fill
    End Sub

    ''' <summary>
    ''' Método que sirve para obtener los results relacionados
    ''' </summary>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	01/08/2008	Created
    ''' </history>
    Public Sub getRelatedsResults(ByVal idDocSelected As Int64, ByRef relatedResultFinal As IResult) Implements IResults_Business.getRelatedsResults
        Try

            ' Si el dataset que contiene las relaciones entre los docs esta vacío entonces traerlo
            If (dsRelateds Is Nothing) Then
                Results_Factory.getZDocRelations(dsRelateds)
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
                Else
                    'relatedResultFinal.Name = Results_Factory.getNameDoc_Id(relatedResultFinal.ID)
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
    Private Shared Sub createInstancesRelatedResult(ByRef relatedR As IResult)

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


    'Public Function GetSharedState(ByVal docid As Int64, ByVal doctypeid As Int64) As Int16
    '    If doctypeid = 0 Then Return 0
    '    Return Results_Factory.GetSharedState(docid, doctypeid)
    'End Function
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
    Public Sub InsertSearchIndexData(ByVal result As IResult) Implements IResults_Business.InsertSearchIndexData
        Dim datatoinsert As String
        For Each i As Index In result.Indexs
            If Not String.IsNullOrEmpty(i.Data.Trim) Then
                If i.DropDown = IndexAdditionalType.LineText Then
                    For Each d As String In i.Data.Trim.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)
                        If Not String.IsNullOrEmpty(d.Trim) Then datatoinsert += "§" & d.Trim
                    Next
                Else
                    For Each d As String In i.dataDescription.Trim.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)
                        If Not String.IsNullOrEmpty(d.Trim) Then datatoinsert += "§" & d.Trim
                    Next
                End If
            End If
        Next
        If Not String.IsNullOrEmpty(datatoinsert) AndAlso datatoinsert.Trim.Length > 1 Then Results_Factory.InsertSearchIndexData(datatoinsert.Substring(1), result.DocType.ID, result.ID)
    End Sub


#Region "Exportar a PDF"


    Public Function exportarResultPDF(ByRef Result As IResult, ByVal sPdf As String) As Boolean Implements IResults_Business.exportarResultPDF
        Try
            If Result.IsImage Then

                'TODO: Validar la ruta del archivo, si las carpetas no existen crearlas
                Dim fi As New IO.FileInfo(sPdf)
                If fi.Directory.Exists = False Then fi.Directory.Create()


                Dim Doc As New ceTe.DynamicPDF.Document
                'Si la imagen del result es un Tif requiere un tratamiento distinto a otras
                If Result.FullPath().ToUpper.EndsWith(".TIF") OrElse Result.FullPath().ToUpper.EndsWith(".TIFF") Then
                    Dim fTif As New ceTe.DynamicPDF.Imaging.TiffFile(Result.RealFullPath)
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
                    Dim Img As New ceTe.DynamicPDF.PageElements.Image(Result.RealFullPath, 0, 0)
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
    Public Sub ConvertToPdfFile(ByRef Result As IResult, ByVal pdfFolderPath As String, ByRef CantPdfs As Int32) Implements IResults_Business.ConvertToPdfFile
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

    Private Function GetValidFileName(ByVal fileName As String) As String
        For Each invalidChar As Char In Path.InvalidPathChars
            If fileName.Contains(invalidChar) Then
                fileName = fileName.Replace(invalidChar, "")
            End If
        Next
        Return fileName.Replace(" ", "_").Replace(".", "").Replace(":", "").Replace("/", "").Replace("__", "_")
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




    Public Sub InsertSearchIndexData(ByVal result As IResult, Optional ByRef t As ITransaction = Nothing) Implements IResults_Business.InsertSearchIndexData
        Dim datatoinsert As String
        For Each i As Index In result.Indexs
            If i IsNot Nothing AndAlso i.Data IsNot Nothing AndAlso Not String.IsNullOrEmpty(i.Data.Trim) Then
                Dim RF As New Results_Factory
                If i.DropDown <> IndexAdditionalType.AutoSustitución AndAlso i.DropDown <> IndexAdditionalType.AutoSustituciónJerarquico Then
                    For Each d As String In i.Data.Trim.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)
                        datatoinsert += "§" + d.Trim
                    Next
                    If Not String.IsNullOrEmpty(datatoinsert) AndAlso datatoinsert.Trim.Length > 1 Then
                        datatoinsert = ReplaceChar(datatoinsert)
                        datatoinsert = TextTools.ReemplazarAcentos(datatoinsert)
                        datatoinsert = datatoinsert.ToLower()
                        RF.InsertSearchIndexDataService(datatoinsert, result.DocTypeId, result.ID, i.ID, t)
                    End If
                Else
                    For Each d As String In i.dataDescription.Trim.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)
                        datatoinsert += "§" + d.Trim
                    Next
                    If Not String.IsNullOrEmpty(datatoinsert) AndAlso datatoinsert.Trim.Length > 1 Then
                        datatoinsert = ReplaceChar(datatoinsert)
                        datatoinsert = TextTools.ReemplazarAcentos(datatoinsert)
                        datatoinsert = datatoinsert.ToLower()
                        RF.InsertSearchIndexDataService(datatoinsert, result.DocTypeId, result.ID, i.ID, t)
                    End If

                End If
            End If
            datatoinsert = String.Empty
        Next
    End Sub

    Public Class TextTools
        Public Shared Function ReemplazarAcentos(texto As String) As String
            texto = texto.Replace("á", "a")
            texto = texto.Replace("é", "e")
            texto = texto.Replace("í", "i")
            texto = texto.Replace("ó", "o")
            texto = texto.Replace("ú", "u")
            texto = texto.Replace("Á", "A")
            texto = texto.Replace("É", "E")
            texto = texto.Replace("Í", "I")
            texto = texto.Replace("Ó", "O")
            texto = texto.Replace("Ú", "U")

            Return texto
        End Function
    End Class
    Private Shared _unicodeCategory As String
    Public Shared Function ReplaceChar(body As String) As String
        Dim data As New StringBuilder
        Dim ZOPTB As New ZOptBusiness
        _unicodeCategory = ZOPTB.GetValue("GetUnicodeCategory")
        ZOPTB = Nothing

        Dim Letters As CharEnumerator = body.GetEnumerator

        While Letters.MoveNext
            If EvaluateIsNotValidChar(Letters.Current) Then
                'If data.Length > 0 AndAlso data.Chars(data.Length - 1) <> "§" Then
                'data.Append("§")
                'End If
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
                Case 45 '-: No se reemplaza (numeros compuestos)
                    Return False
                Case 44 ',: No se reemplaza (con coma)
                    Return False
                Case 46 '.: No se reemplaza (con punto)
                    Return False
                Case 64 '@: No se reemplaza (@)
                    Return False
                Case 95 '_: No se reemplaza (guion bajo)
                    Return False

                Case Else
                    Return True

            End Select

        End If

    End Function

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
    Public Sub DeleteSearchIndexData(ByVal ResultId As Int64) Implements IResults_Business.DeleteSearchIndexData
        Results_Factory.DeleteSearchIndexData(ResultId)
    End Sub




    Public Sub DeleteTempFiles() Implements IResults_Business.DeleteTempFiles
        Try
            Dim i As Int32
            Dim Fi As IO.FileInfo
            For i = 0 To Results_Factory.FilesForDelete.Count - 1
                Try
                    Fi = Results_Factory.FilesForDelete(i)
                    Fi.Delete()
                    Results_Factory.FilesForDelete.RemoveAt(i)
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub



    Public Sub MoveTempFiles(ByVal DocTypeId As Int64) Implements IResults_Business.MoveTempFiles
        Throw New NotImplementedException()
        'Try
        '    Dim VolumeId As Int32 = Volumes.RetrieveDiskGroupId(DocTypeId)
        '    Dim Volume As Volume = Volumes.GetVolume(VolumeId)
        '    If Volume.state = 1 Then
        '        'significa que el volumen esta lleno
        '        RaiseEvent VolumeIsFull()
        '        Exit Sub
        '    End If
        'Catch ex As Exception
        '   zamba.core.zclass.raiseerror(ex)
        '    RaiseEvent VolumeIsFull()
        '    Exit Sub
        'End Try

        'Dim Dr As IDataReader

        'Try
        '    RaiseEvent CountingDoc()
        '    Dim Table As String = Documents.MakeTable(DocTypeId, Documents.TableType.Document)
        '    Dim Strselect As String = "SELECT COUNT(DOC_ID) FROM " & Table & " WHERE VOL_ID < 0"

        '    Dim Count As Int64 = Server.Con.ExecuteScalar(CommandType.Text, Strselect)

        '    Strselect = "SELECT DOC_ID,DISK_GROUP_ID,PLATTER_ID,VOL_ID,DOC_FILE,OFFSET,DOC_TYPE_ID FROM " & Table & " WHERE VOL_ID < 0"

        '    Dim Procesed As Int64 = 0

        '    Dim DocCount As Int32 = 0
        '    Dim DocError As Int32 = 0

        '    While Procesed < Count

        '        Dr = Server.Con.ExecuteReader(CommandType.Text, Strselect)
        '        Dim Read As Int16 = 0
        '        While Dr.Read And Read < 11
        '            Procesed += +1
        '            Read += +1
        '            Dim TempVolId As Int32 = 0
        '            Dim TempVolOffset As Int32 = 0
        '            Dim Document As New Result
        '            DocCount += +1
        '            RaiseEvent ReadingDocFile(DocCount)
        '            Document.Id = Dr.GetInt32(0)
        '            Document.DocTypeId = Dr.GetInt32(6)
        '            Documents.GetDocumentData(Document)
        '            RaiseEvent GettingDocumentData(Document.Id)
        '            TempVolId = Dr.GetInt32(3)
        '            TempVolOffset = Dr.GetInt32(5)
        '            Try
        '                MoveDocument(Document, EstationId, TempVolId, TempVolOffset)
        '                RaiseEvent DocumentMigrated(Document)
        '            Catch ex As Exception
        '               zamba.core.zclass.raiseerror(ex)
        '                DocError += +1
        '                RaiseEvent DocumentMigrationError(Document)
        '            End Try

        '            'Verifico la cancelacion
        '            If Me.FlagGoOn = False Then
        '                If MsgBox("Esta seguro que desea cancelar el proceso de migracion?", MsgBoxStyle.YesNo, "Cancelacion Proceso de Migracion") = MsgBoxResult.Yes Then
        '                    Me.Canceled = True
        '                    Exit While
        '                Else
        '                    Me.FlagGoOn = True
        '                    Me.Canceled = False
        '                End If
        '            End If
        '        End While
        '        Try
        '            Server.Con.Command.Cancel()
        '        Catch ex As Exception
        '        End Try
        '        Dr.Close()
        '        'Verifico la cancelacion
        '        If Me.FlagGoOn = False Then
        '            If Canceled = True Then Exit While
        '            If MsgBox("Esta seguro que desea cancelar el proceso de migracion?", MsgBoxStyle.YesNo, "Cancelacion Proceso de Migracion") = MsgBoxResult.Yes Then
        '                Me.Canceled = True
        '                Exit While
        '            Else
        '                Me.FlagGoOn = True
        '                Me.Canceled = False
        '            End If
        '        End If
        '    End While
        '    Me.FlagGoOn = True
        '    RaiseEvent MigrationFinalized(DocCount, DocError)
        'Catch ex As Exception
        '   zamba.core.zclass.raiseerror(ex)
        '    RaiseEvent MigrationError(ex.tostring)
        'Finally
        '    Try
        '        Server.Con.Command.Cancel()
        '    Catch ex As Exception
        '    End Try
        '    Dr.Close()
        'End Try
    End Sub



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo para mover un documento a otro entidad
    ''' </summary>
    ''' <param name="Result">NewResult Original que se va a mover o copiar</param>
    ''' <param name="TempVolId">Volumen Temporal</param>
    ''' <param name="TempVolOffSet"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------

    Private _flaggoon As Boolean = True
    Public Property FlagGoOn() As Boolean Implements IResults_Business.FlagGoOn
        Get
            Return _flaggoon
        End Get
        Set(ByVal Value As Boolean)
            _flaggoon = Value
        End Set
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub



#Region "Indexs & DocType"
    Friend Sub FillIndexsAndDocType(ByRef Result As ITaskResult)
        Try
            FillDocType(Result)
            Result.Indexs = ZCore.GetInstance().FilterIndex(CInt(Result.DocType.ID))
            'Results_Factory.FillIndexData(Result)
            CompleteDocument(DirectCast(Result, Result), True)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Carga los indices para un result
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <param name="inThread"></param>
    ''' <remarks></remarks>
    Friend Sub FillIndexs(ByRef Result As ITaskResult, Optional ByVal inThread As Boolean = False)
        Try
            'FillDocType(Result)
            Result.Indexs = ZCore.GetInstance().FilterIndex(CInt(Result.DocType.ID))
            'Results_Factory.FillIndexData(Result)
            CompleteDocument(DirectCast(Result, Result), inThread)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Carga los indices para un result
    ''' </summary>
    ''' <param name="Result"></param>
    ''' <remarks></remarks>
    Friend Sub CompleteDocument(ByRef Result As ITaskResult, ByVal dr As DataRow)
        Dim ASB As New AutoSubstitutionBusiness
        Try
            Result.Indexs = ZCore.GetInstance().FilterIndex(CInt(Result.DocType.ID))

            Try
                Result.Disk_Group_Id = dr("DISK_GROUP_ID")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                Result.Platter_Id = dr("PLATTER_ID")
            Catch ex As Exception
            End Try
            Try
                Result.Doc_File = dr("DOC_FILE")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                Result.OffSet = dr("OFFSET")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                Result.Name = dr("NAME")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                Result.IconId = dr("ICON_ID")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                If IsDBNull(dr("shared")) = False Then
                    If CInt(dr("shared")) = 1 Then
                        Result.IsShared = True
                    Else
                        Result.IsShared = False
                    End If
                Else
                    Result.IsShared = False
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                Result.ParentVerId = dr("ver_Parent_id")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                Result.HasVersion = dr("version")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                Result.RootDocumentId = dr("RootId")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                Result.OriginalName = dr("original_Filename")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try

                If (IsDBNull(dr("NumeroVersion"))) Then
                    Result.VersionNumber = 0
                Else
                    Result.VersionNumber = dr("NumeroVersion")
                End If

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
            Try
                If IsDBNull(dr("disk_Vol_id")) = False Then
                    Result.Disk_Group_Id = dr("disk_Vol_id")
                Else
                    Result.Disk_Group_Id = 0
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                If IsDBNull(dr("DISK_VOL_PATH")) = False Then
                    Result.DISK_VOL_PATH = dr("DISK_VOL_PATH")
                Else
                    Result.DISK_VOL_PATH = String.Empty
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Try
                Result.UserId = Result.Platter_Id
                Result.OwnerID = Result.Platter_Id
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            If Not IsDBNull(dr("crdate")) Then
                If Not IsNothing(dr("crdate")) Then
                    If Not String.IsNullOrEmpty(dr("crdate").ToString()) Then
                        Try
                            If Not IsNothing(Result) Then
                                Result.CreateDate = DateTime.Parse(dr("crdate").ToString())
                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try
                    End If
                End If
            End If

            Dim i As Int32
            For i = 0 To Result.Indexs.Count - 1
                Try
                    If Not IsDBNull(dr("I" & DirectCast(Result.Indexs(i), Index).ID)) Then
                        DirectCast(Result.Indexs(i), Index).Data = dr("I" & DirectCast(Result.Indexs(i), Index).ID).ToString
                        'Si el indice es de tipo Sustitucion
                        If DirectCast(Result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustitución OrElse DirectCast(Result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                            'Se carga la descripcion de Indice
                            DirectCast(Result.Indexs(i), Index).dataDescription = ASB.getDescription(DirectCast(Result.Indexs(i), Index).Data, DirectCast(Result.Indexs(i), Index).ID)
                        End If
                    Else
                        DirectCast(Result.Indexs(i), Index).Data = String.Empty
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            Next

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            ASB = Nothing

        End Try
    End Sub
    Private Sub FillDocType(ByRef Result As ITaskResult)
        Dim DTB As New DocTypesBusiness
        Try
            Static DTs As Hashtable = DTs
            If IsNothing(DTs) Then DTs = New Hashtable
            If DTs.Contains(CLng(Result.DocType.ID)) = False Then
                Result.Parent = DTB.GetDocType(Result.DocType.ID)
                DTs.Add(Result.Parent.ID, Result.Parent)
            Else
                Result.Parent = DirectCast(DTs(Long.Parse(Result.DocType.ID.ToString())), IZambaCore)
                If IsNothing(Result.Parent) Then
                    Result.Parent = DirectCast(DTs(Result.DocType.ID), IZambaCore)
                End If
            End If

            DTB = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    'Public  Function GetIndexTypeByName(ByVal indexname As String) As Int16
    '    Return WFTasksFactory.GetIndexTypeByName(indexname)
    'End Function
#End Region


    Public Const _EXTMSG As String = ".msg"
    Public Const _EXTHTML As String = ".html"
    Public Const _EXTTXT As String = ".txt"

    Public Function GetTempFileFromResult(localResult As IResult, GetPreview As Boolean) As String Implements IResults_Business.GetTempFileFromResult
        Dim tempPath As String = Nothing
        Dim fTemp As FileInfo = Nothing
        Dim dir As DirectoryInfo = Nothing
        Dim Resultpath As String = Nothing

        Resultpath = localResult.FullPath

        Try
            'Se obtiene la ruta temporal del archivo a visualizar con el iframe
            dir = Zamba.Membership.MembershipHelper.AppTempDir("\OfficeTemp")
            If Not dir.Exists Then
                dir.Create()
            End If
            fTemp = New FileInfo(dir.FullName + "\" + Path.GetFileName(localResult.FullPath))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        'Si el archivo es msg y tiene una copia en otro formato, se obtiene para visualizarlo

        If GetPreview AndAlso (localResult.IsMsg AndAlso (File.Exists(fTemp.FullName.ToLower().Replace(_EXTMSG, _EXTHTML)) OrElse File.Exists(fTemp.FullName.ToLower().Replace(_EXTMSG, _EXTTXT)) OrElse File.Exists(localResult.FullPath.ToLower().Replace(_EXTMSG, _EXTHTML)) OrElse File.Exists(localResult.FullPath.ToLower().Replace(_EXTMSG, _EXTTXT)))) Then
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
                Resultpath = tempPath.Replace(_EXTMSG, _EXTHTML)
            ElseIf File.Exists(tempPath.Replace(_EXTMSG, _EXTTXT)) Then
                Resultpath = tempPath.Replace(_EXTMSG, _EXTTXT)
            End If

        Else
            'Se hace la copia local para visualizarlo en el iframe
            Try
                If fTemp.Exists = False Then
                    CopyFileToTemp(localResult, localResult.RealFullPath, fTemp.FullName)
                End If
                fTemp.Attributes = FileAttributes.Normal
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            Resultpath = fTemp.FullName
        End If

        Return Resultpath
    End Function

    Public Function GetTaskURL(currentuserid As Integer, docid As Integer, entityid As Integer, useOriginal As Boolean) As String Implements IResults_Business.GetTaskURL
        Dim WTB As New WFTaskBusiness()
        Dim localResult As IResult = WTB.GetTaskByDocIdAndDocTypeId(docid, entityid)
        Dim path As [String]

        If Not String.IsNullOrEmpty(localResult.FullPath) AndAlso System.IO.Path.HasExtension(localResult.FullPath) Then
            path = GetTempFileFromResult(localResult, True)
            Return path
        Else
            Return String.Empty
        End If
    End Function



    Public Function ValidateNewResult(ByVal DocTypeId As Integer, ByVal docid As Integer) As Boolean Implements IResults_Business.ValidateNewResult
        Dim RF As New Results_Factory()
        Return RF.ValidateNewResult(DocTypeId, docid)
    End Function

    Public Function GetCountOfBaremoIndex(ByVal id As Long, ByVal indexName As String) As Boolean Implements IResults_Business.GetCountOfBaremoIndex
        Dim RF As New Results_Factory()
        Return RF.GetCountOfBaremoIndex(id, indexName)
    End Function

    Public Function getAssociatedBaremos(ByVal idReclamo As Int64, ByVal idBaremoMuerte As Int64) As DataTable Implements IResults_Business.getAssociatedBaremos
        Dim RF As New Results_Factory()
        Return RF.GetBaremoMuerteReclamantes(idReclamo, idBaremoMuerte)
    End Function



    '' Public Function GetAsociatedToEditTable(ByVal idReclamo As Int64, ByVal idBaremoMuerte As Int64, ByVal docTypeId As Int64, ByVal associatedId As Int64) As DataTable
    Public Function GetAsociatedToEditTable(ByVal idReclamo As Int64,
                                            ByVal idParent As Int64,
                                            ByVal docTypeId As Int64,
                                            ByVal associatedId As Int64,
                                            ByVal idParentColumnName As Int64,
                                            ByVal indexs As Dictionary(Of Int64, String),
                                            ByVal formEntityID As Int64) As DataTable Implements IResults_Business.GetAsociatedToEditTable
        Dim DTB As New DocTypesBusiness
        Dim childAlias As String = DTB.GetDocType(formEntityID).Name
        Dim parentAlias As String = DTB.GetDocType(associatedId).Name



        Dim RF As New Results_Factory()
        Return RF.GetAsociatedToEditTable(idReclamo, idParent, idParentColumnName, docTypeId, associatedId, childAlias, parentAlias, indexs)
    End Function

    Public Function GetIdsFromABaremoMuerte(ByVal baremoDocId As Int64) As DataTable Implements IResults_Business.GetIdsFromABaremoMuerte
        Dim RF As New Results_Factory()
        Return RF.GetIdsFromABaremoMuerte(baremoDocId)
    End Function

    ''Devuelve los id de reclamo y del formulario
    Public Function GetIdsToAsociatedParents(ByVal docID As Int64, ByVal asociatedID As Int64, ByVal formID As Int64) As DataTable Implements IResults_Business.GetIdsToAsociatedParents
        Dim RF As New Results_Factory()
        Dim DTB As New DocTypesBusiness()
        Dim tableAlias As String = DTB.GetDocTypeName(asociatedID)
        Return RF.GetIdsToAsociatedParents(docID, asociatedID, formID, tableAlias)
    End Function

    Public Function GetRequestAssociatedResult(ByVal requestNumber As Int64, ByVal tableID As Int64,
                                               ByVal formID As Int64,
                                               ByVal indexs As List(Of Int64)) As DataTable Implements IResults_Business.GetRequestAssociatedResult
        Dim RF As New Results_Factory()
        Return RF.GetRequestAssociatedResult(requestNumber, tableID, formID, indexs)
    End Function

    Public Function GetRequestNumber(ByVal tableId As Int64, ByVal indexId As Int64, ByVal docId As Int64) As Int64 Implements IResults_Business.GetRequestNumber
        Dim RF As New Results_Factory()
        Return RF.GetRequestNumber(tableId, indexId, docId)
    End Function

    Public Function GetCalendarRslt(ByVal entityId As String, titleAttribute As String, startAttribute As String, endAttribute As String, filterColumn As String, filterValue As String) As DataTable Implements IResults_Business.GetCalendarRslt
        Dim RF As New Results_Factory()
        Return RF.GetCalendar(entityId, titleAttribute, startAttribute, endAttribute, filterColumn, filterValue)
    End Function

    Public Function GetAllEntities() As DataTable Implements IResults_Business.GetAllEntities
        Dim RF As New Results_Factory()
        Return RF.GetAllEntities()
    End Function

    Public Function GetIndexForEntities(IndexId As Int64) As DataTable Implements IResults_Business.GetIndexForEntities
        Dim RF As New Results_Factory()
        Return RF.GetIndexForEntities(IndexId)
    End Function


    Public Function getDataFromHerarchicalParent(parentTagValue As Int64) As DataTable Implements IResults_Business.getDataFromHerarchicalParent
        Dim RF As New Results_Factory()
        Return RF.getDataFromHerarchicalParentData(parentTagValue)
    End Function

    Public Function getInsertAdInfoInZamba(TagValue As String, UserId As Int64, PropertId As Int64, eId As Int64) As Boolean
        Dim RF As New Results_Factory()
        RF.getInsertAdInfoInZamba(TagValue, UserId, PropertId, eId)
        Return True
    End Function

    Public Function getZudt() As DataTable
        Dim RF As New Results_Factory()
        Return RF.getZudt()
    End Function

    Public Function getDataFromHerarchicalParent(parentTagValue As Int64, indexs As List(Of String), tableId As String, isView As Boolean) As DataTable Implements IResults_Business.getDataFromHerarchicalParent
        Dim RF As New Results_Factory()
        Return RF.getHeralchicalTagData(parentTagValue, indexs, tableId, isView)
    End Function


    Public Function saveDoSearchResults(ByVal SearchObject As String, ByVal Mode As String, ByVal UserId As Int64, ByVal expirationDate As DateTime) As Boolean
        Dim result = RF.InsertOrUpdateDoSearchResults(SearchObject, Mode, UserId, expirationDate)
        Return True
    End Function
    Public Function saveLastSearchResults(search As ISearch, ByVal SearchObject As String, ByVal Mode As String, ByVal UserId As Int64, ByVal searchDate As DateTime) As Boolean
        Dim Name As String
        Dim LSB As New LastSearchBusiness
        Name = LSB.GetSearchName(search)
        Dim result = RF.InsertOrUpdateLastSearchResults(SearchObject, Mode, UserId, searchDate, Name)
        Return True
    End Function

    Public Function loadDoSearchResults(ByVal UserId As Int64, ByVal Mode As String) As DataTable
        Dim result = RF.SelectDoSearchResults(UserId, Mode)
        Return result
    End Function

    Public Function loadLastSearchResults(ByVal UserId As Int64) As DataTable
        Dim result = RF.SelectLastSearchResults(UserId)
        Return result
    End Function


    Public Function removeDoSearchResults(userId As Long, Mode As String) As Object
        Dim result = RF.DeleteDoSearchResults(userId, Mode)
        Return result
    End Function


    ''' <summary>
    '''  Get date for token --  Zss table.
    ''' </summary>
    ''' <param name="UserId"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Felipe 29/03/2022]    Created
    ''' </history>dd
    Public Function getValidateActiveSession(ByVal UserId As Int64, ByVal userToken As String) As Boolean

        Dim isValidSession As Boolean = True

        Dim RF As New Results_Factory()
        Dim sessionInfo As DataTable = RF.getUserSessionInfo(UserId)

        If sessionInfo Is Nothing Then

            isValidSession = False

        Else
            'ZTrace.WriteLineIf(ZTrace.IsVerbose, "getValidateActiveSession - DateTime.Now :  " + DateTime.Now)
            'ZTrace.WriteLineIf(ZTrace.IsVerbose, "getValidateActiveSession : TokenExpireDate " + sessionInfo.Rows(0)("TokenExpireDate"))
            'ZTrace.WriteLineIf(ZTrace.IsVerbose, "getValidateActiveSession : Token " + sessionInfo.Rows(0)("Token"))
            'ZTrace.WriteLineIf(ZTrace.IsVerbose, "getValidateActiveSession : userToken " + userToken)

            isValidSession = DateTime.Now <= sessionInfo.Rows(0)("TokenExpireDate") And sessionInfo.Rows(0)("Token").ToString() = userToken

        End If




        Return isValidSession
    End Function


    ''' <summary>
    '''  Get date for token --  Zss table.
    ''' </summary>
    ''' <param name="UserId"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Felipe 29/03/2022]    Created
    ''' </history>dd
    Public Function getUserSessionInfoforToken(ByVal UserId As Int64) As DataTable



        Dim RF As New Results_Factory()
        Dim sessionInfo As DataTable = RF.getUserSessionInfo(UserId)

        Return sessionInfo

    End Function







End Class

