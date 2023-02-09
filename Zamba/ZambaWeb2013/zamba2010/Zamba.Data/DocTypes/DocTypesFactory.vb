Imports ZAMBA.Servers
Imports Zamba.Core
Imports Zamba.Data
Imports System.Text
Imports System.Collections.Generic

Public Class DocTypesFactory

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Esto borra el contenido de las tablas Doc_I, Doc_T, pero falta borrar las imagenes (archivos fisicos)
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub BorrarDocumentos(ByVal DocTypeId As Int64)
        Dim strdelete As New System.Text.StringBuilder
        Try
            If Server.IsOracle Then
                strdelete.Append("Delete from Doc_I")
                strdelete.Append(DocTypeId)
                Server.Con.ExecuteNonQuery(CommandType.Text, strdelete.ToString)
                strdelete.Remove(0, strdelete.Length)
                strdelete.Append("Delete from Doc_T")
                strdelete.Append(DocTypeId)
                Server.Con.ExecuteNonQuery(CommandType.Text, strdelete.ToString)
            Else
                strdelete.Append("Delete from [Doc_I")
                strdelete.Append(DocTypeId)
                strdelete.Append("]")
                Server.Con.ExecuteNonQuery(CommandType.Text, strdelete.ToString)
                strdelete.Remove(0, strdelete.Length)
                strdelete.Append("Delete from [Doc_T")
                strdelete.Append(DocTypeId)
                strdelete.Append("]")
                Server.Con.ExecuteNonQuery(CommandType.Text, strdelete.ToString)
            End If
            strdelete.Remove(0, strdelete.Length)
            strdelete.Append("UPDATE DOC_TYPE set DOCCOUNT=0 Where doc_type_id=")
            strdelete.Append(DocTypeId)
            Server.Con.ExecuteNonQuery(CommandType.Text, strdelete.ToString)
        Finally
            strdelete = Nothing
        End Try
    End Sub


    ''' <summary>
    ''' Obtiene la cantidad de documentos de un tipo de doc
    ''' </summary>
    ''' <param name="DoctypeId">Id del entidad</param>
    ''' <returns>Cantidad de Documentos que tiene el entidad</returns>
    ''' <History>Marcelo Modified 05/07/2009</History>
    ''' <remarks></remarks>
    Public Shared Function GetDocCountFromADocType(ByVal DoctypeId As Int64) As Int32
        Dim Sql As New StringBuilder()

        Sql.Append("SELECT count(1) from")
        Sql.Append(" doc_t" & DoctypeId)

        Return Server.Con.ExecuteScalar(CommandType.Text, Sql.ToString)
    End Function
    Public Shared Function GetDocTypesByUser(ByVal indexId As Int64, ByVal userId As Int64) As DataSet
        Dim sqlBuilder As New StringBuilder()
        sqlBuilder.Append("SELECT DOC_TYPE_ID FROM INDEX_R_DOC_TYPE WHERE INDEX_ID = '")
        sqlBuilder.Append(indexId.ToString())
        sqlBuilder.Append("'")
        Return Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

    End Function

    Public Shared Function GetDocTypeIdByIndexId(ByVal indexID As Int64) As DataSet
        'Dim restricc As String = RestrictionsMapper_Factory.getRestrictionWebStrings(UserId, docTypeId)
        'Return Results_Factory.getResultsData(docTypeId, indexID, genIndex, comparateValue, searchValue, restricc)


        Dim sqlBuilder As New StringBuilder()
        sqlBuilder.Append("SELECT DOC_TYPE_ID FROM INDEX_R_DOC_TYPE WHERE INDEX_ID = '")
        sqlBuilder.Append(indexID.ToString())
        sqlBuilder.Append("'")
        Return Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

    End Function
    Public Shared Function GetDocTypeIdByIndexId(ByVal indexID As Int64, ByVal userid As Int64) As DataSet
        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("Select * from doc_type where Doc_type_id in (")
        QueryBuilder.Append("Select distinct(aditional) from usr_rights where (GROUPID in (")
        QueryBuilder.Append("Select groupid from usr_r_group where (usrid=")
        QueryBuilder.Append(userid.ToString())
        QueryBuilder.Append("or groupId=")
        QueryBuilder.Append(userid.ToString())
        QueryBuilder.Append(") And (objid=2 and rtype=1)) ")
        QueryBuilder.Append("and doc_type_id in (SELECT DOC_TYPE_ID FROM INDEX_R_DOC_TYPE WHERE INDEX_ID =")
        QueryBuilder.Append(indexID.ToString())
        QueryBuilder.Append(")))")

        Return Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
    End Function

    Public Shared Function GetDocTypeIdByIndexId(ByVal indexIDs As Generic.List(Of Int64)) As Generic.List(Of Int64)
        Dim sqlBuilder As New StringBuilder()
        Dim tmpDocTypeID As Int64
        sqlBuilder.Append("SELECT DOC_TYPE_ID FROM INDEX_R_DOC_TYPE WHERE INDEX_ID = '")

        If indexIDs.Count = 1 Then
            sqlBuilder.Append(indexIDs(0).ToString())
            sqlBuilder.Append("'")

        Else

        End If
        tmpDocTypeID = Convert.ToInt64(Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString()))
        'Return tmpDocTypeID
        Return New List(Of Int64)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina la relación entre un Doc_type y un Doc_type_group
    ''' </summary>
    ''' <param name="doctypeid"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub Remove_DocType_FromAll_DocTypesGroup(ByVal doctypeid As Int64)
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append("Delete from doc_Type_r_doc_type_group where doc_type_id=")
            sql.Append(doctypeid)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
        Finally
            sql = Nothing
        End Try
    End Sub

    Public Shared Function CheckTempFiles(ByVal DocTypeId As Int64) As Int32
        Dim strSelect As New System.Text.StringBuilder
        Try
            strSelect.Append("SELECT COUNT(1) FROM DOC_T" & DocTypeId)
            strSelect.Append(",DISK_VOLUME WHERE VOL_ID = DISK_VOLUME.DISK_VOL_ID AND DISK_VOLUME.DISK_VOL_NAME = '")
            strSelect.Append(Environment.MachineName)
            strSelect.Append("' AND DISK_VOLUME.DISK_VOL_ID < 0")
            Return Convert.ToInt32(Server.Con.ExecuteScalar(CommandType.Text, strSelect.ToString))
        Catch ex As Exception
            Throw New Exception("Ocurrio un error al consultar los Documentos en Volúmenes Temporales" & " " & ex.ToString)
        Finally
            strSelect = Nothing
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve True en caso que el doc_type este asignado a algun archivo
    ''' </summary>
    ''' <param name="DocTypeId">ID del Tipo de documento</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function DocTypeIsAsigned(ByVal DocTypeId As Int64) As Boolean

        Dim strSelect As New System.Text.StringBuilder
        Try
            strSelect.Append("SELECT COUNT(1) from Doc_Type_R_Doc_Type_Group where (Doc_Type_Id = ")
            strSelect.Append(DocTypeId)
            strSelect.Append(")")
            Dim qrows As Int32 = Convert.ToInt32(Server.Con.ExecuteScalar(CommandType.Text, strSelect.ToString))
            If qrows <> 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception("Ocurrio un error al consultar la asignacion de este Entidad " & ex.ToString)
        Finally
            strSelect = Nothing
        End Try
    End Function






    Public Shared Function GetDocTypesDsDocType() As DataSet
        Dim Query As String = "SELECT DOC_TYPE_NAME, FILE_FORMAT_ID, DISK_GROUP_ID, THUMBNAILS, ICON_ID, OBJECT_TYPE_ID, AUTONAME, DOCUMENTALID,DOCCOUNT,DOC_TYPE_ID FROM Doc_Type ORDER BY Doc_Type_Name"
        Dim DsTemp As New DataSet
        Try
            DsTemp = Server.Con.ExecuteDataset(CommandType.Text, Query)
            DsTemp.Tables(0).TableName = "DOC_TYPE"
            Return DsTemp
        Finally
            DsTemp.Dispose()
            DsTemp = Nothing
        End Try
    End Function
#Region "Rights"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Define Permisos para doctypes
    ''' </summary>
    ''' <history>
    ''' 	[Diego]	17/03/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SetDocTypeRight(ByVal DoctypeId As Int64, ByVal RightType As Int32, ByVal Value As Int32)
        Dim query As New StringBuilder
        query.Append("select count(1) from zdoctyperights where dtId= " & DoctypeId & " AND RightId =" & RightType)

        If Server.Con.ExecuteScalar(CommandType.Text, query.ToString) = 0 Then
            query.Remove(0, query.Length)
            query.Append("INSERT INTO ZDocTypeRights(dtId, RightId, RValue) VALUES(")
            query.Append(DoctypeId)
            query.Append(",")
            query.Append(RightType)
            query.Append(",")
            query.Append(Value)
            query.Append(")")
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        Else
            query.Remove(0, query.Length)
            query.Append("UPDATE ZDocTypeRights SET dtId=")
            query.Append(DoctypeId)
            query.Append(",RightId=")
            query.Append(RightType)
            query.Append(",RValue =")
            query.Append(Value)
            query.Append(" WHERE dtId =")
            query.Append(DoctypeId)
            query.Append(" AND RightId =")
            query.Append(RightType)
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        End If

    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene Permisos para Entidades
    ''' </summary>
    ''' <history>
    ''' 	[Diego]	17/03/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function LoadDocTypeRightValue(ByVal DoctypeId As Int64, ByVal RightType As DocTypeRights) As Int32
        Dim query As New StringBuilder
        query.Append("SELECT RValue FROM ZDocTypeRights where Dtid = ")
        query.Append(DoctypeId)
        query.Append(" and RightId =")
        query.Append(RightType)
        If Not IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, query.ToString)) Then
            Return Server.Con.ExecuteScalar(CommandType.Text, query.ToString)
        Else
            Return -1
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene Permisos para Entidades
    ''' </summary>
    ''' <history>
    ''' 	[Diego]	17/03/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function LoadDocTypeRightValue(ByVal DoctypeId As Int64, ByVal RightType As DocTypeRights, ByVal t As Transaction) As Int32
        Dim query As New StringBuilder
        query.Append("SELECT RValue FROM ZDocTypeRights where Dtid = ")
        query.Append(DoctypeId)
        query.Append(" and RightId =")
        query.Append(RightType)
        If Not IsDBNull(t.Con.ExecuteScalar(t.Transaction, CommandType.Text, query.ToString)) Then
            Return t.Con.ExecuteScalar(t.Transaction, CommandType.Text, query.ToString)
        Else
            Return -1
        End If
    End Function
#End Region


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Decuelve un Arraylist con con todos los objetos DocType de Zamba
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Marcelo]	13/08/2009	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetDocTypesArrayList() As DataSet
        Dim DSTEMP As DataSet = Nothing
        Dim strselect As New System.Text.StringBuilder
        Try
            strselect.Append("SELECT Doc_Type_Id, Doc_Type_Name, File_Format_ID, Disk_Group_ID, Thumbnails, Icon_Id, Object_Type_Id, AutoName FROM Doc_Type ORDER BY Doc_Type_Name")
            DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
            DSTEMP.Tables(0).TableName = "DOC_TYPE"
            Return DSTEMP
        Finally
            strselect = Nothing
            DSTEMP.Dispose()
            DSTEMP = Nothing
        End Try
    End Function



    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un DOC_TYPE en base al ID
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' 	[Marcelo]	07/08/2009	Modified - Quito llamada a la cache
    ''' </history>
    Public Shared Function GetDocType(ByVal docTypeId As Int64) As IDocType

        Dim QueryBuilder As New StringBuilder()
        Try
            'QueryBuilder.Append("SELECT Doc_Type_Id, Doc_Type_Name, File_Format_ID, Disk_Group_ID, Thumbnails, Icon_Id, Object_Type_Id, AutoName,Doccount,DOCUMENTALID FROM Doc_Type WHERE Doc_type_Id = ")
            'QueryBuilder.Append(docTypeId.ToString())

            Dim DsTemp As DataSet = Nothing
            If Server.isOracle = True Then
                Dim strbuilder As New StringBuilder()
                strbuilder.Append("SELECT Doc_Type_Id, Doc_Type_Name, File_Format_ID, Disk_Group_ID, Thumbnails,")
                strbuilder.Append("Icon_Id, Object_Type_Id, AutoName, Doccount, DOCUMENTALID FROM Doc_Type ")
                strbuilder.Append("WHERE Doc_type_Id = ")
                strbuilder.Append(docTypeId)

                DsTemp = Server.Con.ExecuteDataset(CommandType.Text, strbuilder.ToString())
            Else
                DsTemp = Server.Con.ExecuteDataset("Zsp_docTypes_100_GetDoctypeById", New Object() {docTypeId})
            End If
            If Not IsNothing(DsTemp) AndAlso DsTemp.Tables.Count > 0 AndAlso DsTemp.Tables(0).Rows.Count > 0 Then
                DsTemp.Tables(0).TableName = "DOC_TYPE"


                Dim CurrentDocType As IDocType = New DocType(Convert.ToInt64(DsTemp.Tables("DOC_TYPE")(0)("DOC_TYPE_ID")), DsTemp.Tables("DOC_TYPE")(0)("DOC_TYPE_NAME"), Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("FILE_FORMAT_ID")), Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("DISK_GROUP_ID")), Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("THUMBNAILS")), Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("ICON_ID")), Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("OBJECT_TYPE_ID")), DsTemp.Tables("DOC_TYPE")(0)("AUTONAME"), DsTemp.Tables("DOC_TYPE")(0)("AUTONAME"), 0)
                Return CurrentDocType
            End If
            Return Nothing
        Finally
            QueryBuilder.Remove(0, QueryBuilder.Length)
            QueryBuilder = Nothing
        End Try


        Return Nothing
    End Function


    ''' <summary>
    ''' Obtiene un DOC_TYPE en base al ID
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    Public Function GetDocTypes(ByVal docTypeIds As List(Of Int64), ByVal fullLoad As Boolean) As List(Of IDocType)

        Dim QueryBuilder As New StringBuilder()
        Try
            If fullLoad Then
                QueryBuilder.Append("SELECT Doc_Type_Id, Doc_Type_Name, File_Format_ID, Disk_Group_ID, Thumbnails, Icon_Id, Object_Type_Id, AutoName FROM Doc_Type WHERE Doc_type_Id IN (")
            Else
                QueryBuilder.Append("SELECT Doc_Type_Id, Doc_Type_Name FROM Doc_Type WHERE Doc_type_Id IN (")
            End If

            For Each docTypeId As Int64 In docTypeIds
                QueryBuilder.Append(docTypeId)
                QueryBuilder.Append(",")
            Next
            QueryBuilder = QueryBuilder.Remove(QueryBuilder.Length - 1, 1)
            QueryBuilder.Append(")")

            Using DsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString)
                If Not IsNothing(DsTemp) AndAlso DsTemp.Tables.Count > 0 AndAlso DsTemp.Tables(0).Rows.Count > 0 Then
                    DsTemp.Tables(0).TableName = "DOC_TYPE"


                    Dim docTypes As New List(Of IDocType)
                    For i As Int32 = 0 To DsTemp.Tables(0).Rows.Count - 1
                        If fullLoad Then
                            docTypes.Add(New DocType(Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("DOC_TYPE_ID")), DsTemp.Tables("DOC_TYPE")(0)("DOC_TYPE_NAME"), Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("FILE_FORMAT_ID")), Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("DISK_GROUP_ID")), Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("THUMBNAILS")), Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("ICON_ID")), Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("OBJECT_TYPE_ID")), DsTemp.Tables("DOC_TYPE")(0)("AUTONAME"), DsTemp.Tables("DOC_TYPE")(0)("AUTONAME"), 0))
                        Else
                            docTypes.Add(New DocType(Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("DOC_TYPE_ID")), DsTemp.Tables("DOC_TYPE")(0)("DOC_TYPE_NAME"), 0))
                        End If
                    Next

                    Return docTypes
                End If
                Return Nothing
            End Using
        Finally
            QueryBuilder.Remove(0, QueryBuilder.Length)
            QueryBuilder = Nothing
        End Try

        Return Nothing
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un doctype en base al nombre
    ''' </summary>
    ''' <param name="DocTypeName"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared DocTypesByNames As New Hashtable
    Public Function GetDocType(ByVal DocTypeName As String) As IDocType

        If Not IsNothing(DocTypesByNames) AndAlso DocTypesByNames.ContainsKey(DocTypeName) Then
            Return DocTypesByNames(DocTypeName)
        End If

        Dim QueryBuilder As New StringBuilder()

        Try
            QueryBuilder.Append("SELECT Doc_Type_Id, Doc_Type_Name, File_Format_ID, Disk_Group_ID, Thumbnails, Icon_Id, Object_Type_Id, AutoName FROM Doc_Type WHERE Doc_type_name = '")
            QueryBuilder.Append(DocTypeName)
            QueryBuilder.Append("' ORDER BY Doc_Type_Name")

            Using DsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())

                DsTemp.Tables(0).TableName = "DOC_TYPE"

                Dim CurrentDocType As IDocType = New DocType(Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("DOC_TYPE_ID")), DsTemp.Tables("DOC_TYPE")(0)("DOC_TYPE_NAME"), Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("FILE_FORMAT_ID")), Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("DISK_GROUP_ID")), Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("THUMBNAILS")), Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("ICON_ID")), Convert.ToInt32(DsTemp.Tables("DOC_TYPE")(0)("OBJECT_TYPE_ID")), DsTemp.Tables("DOC_TYPE")(0)("AUTONAME"), DsTemp.Tables("DOC_TYPE")(0)("AUTONAME"), 0)
                DocTypesByNames.Add(DocTypeName, CurrentDocType)
                Return CurrentDocType

            End Using
        Finally
            QueryBuilder = Nothing
        End Try
        Return Nothing
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Dataset Tipeado con todos los datos de los Entidades existentes en Zamba
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetDocTypes() As DataSet
        Dim DSTEMP As DataSet = Nothing
        Dim strselect As New System.Text.StringBuilder
        Try
            strselect.Append("SELECT doc_type.DOC_TYPE_Id, Doc_Type_Name, File_Format_ID, Disk_Group_ID, Thumbnails, Icon_Id, Object_Type_Id, AutoName, DOC_TYPE_GROUP,DOCUMENTALID,DOCCOUNT FROM Doc_Type, doc_type_r_doc_type_group where doc_type.DOC_TYPE_id = doc_type_r_doc_type_group.DOC_TYPE_id ORDER BY Doc_Type_Name")
            DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
            DSTEMP.Tables(0).TableName = "DOC_TYPE"

            Return DSTEMP
        Finally
            strselect = Nothing
            DSTEMP.Dispose()
            DSTEMP = Nothing
        End Try
    End Function
    'Public Shared Function GetDocTypes(ByVal UserId As Int32) As DSDocType
    'Dim DsDocType As New DsDocType

    'if Server.IsOracle then
    'Dim dstemp As DataSet
    'Dim parNames() As String = {"GlobalUserId", "io_cursor"}
    'Dim parTypes() = {13, 13, 5}
    'Dim parValues() = {UserId, 2}
    'dstemp = Server.Con.ExecuteDataset("Z_USERDOCTYPES_PKG.Z_USERDOCTYPES", parValues)
    'dstemp.Tables(0).TableName = "DocTypes"
    'DsDocType.Merge(dstemp)
    'Else
    'Dim dstemp As DataSet
    'Dim parameters() = {UserId}
    'dstemp = Server.Con.ExecuteDataset("Z_USERDOCTYPES", parameters)
    'dstemp.Tables(0).TableName = "DocTypes"
    'DsDocType.Merge(dstemp)
    'End If
    'End Function

    Public Function GetDocTypeId(ByVal docId As Int64) As Int64
        Dim DocTypeId As Int64

        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("select distinct(doc_type_id) from ")
        QueryBuilder.Append("wfdocument where doc_ID = ")
        QueryBuilder.Append(docId.ToString())

        Dim Value As Object = Server.Con.ExecuteScalar(CommandType.Text, QueryBuilder.ToString())


        If Not IsNothing(Value) Then
            Int64.TryParse(Value.ToString(), DocTypeId)
        End If

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing
        Value = Nothing

        Return DocTypeId
    End Function

    Public Function GetDocTypesChilds() As DataTable
        Dim DsTemp As DataSet = New DataSet
        Dim StrSelect As New StringBuilder
        Try

            StrSelect.Append("Select distinct INDEX_ID, DOC_TYPE_ID, ORDEN, MUSTCOMPLETE, SHOWLOTUS, LOADLOTUS, COMPLETE, DEFAULTVALUE, INDEXSEARCH, ISDATAUNIQUE, AUTOCOMPLETE, ISREFERENCED,  NVL(ZH.IndicePadre, -1) As IndicePadre  from ( select distinct INDEX_ID, DOC_TYPE_ID, ORDEN, MUSTCOMPLETE, SHOWLOTUS, LOADLOTUS, COMPLETE, DEFAULTVALUE, INDEXSEARCH, ISDATAUNIQUE, AUTOCOMPLETE, ISREFERENCED From index_r_doc_type DT  Where DOC_TYPE_ID Not In ( Select  aditional From usr_rights Where (groupid in (Select groupid From zvw_usr_r_groupsandtheirinh  Where usrid  In ( ")
            StrSelect.Append(Membership.MembershipHelper.CurrentUser.ID)
            StrSelect.Append(") ) Or groupid =  ")
            StrSelect.Append(Membership.MembershipHelper.CurrentUser.ID)
            StrSelect.Append(") And objid = 2 And rtype = 56) UNION ALL  Select  distinct INDEX_ID, DOC_TYPE_ID, ORDEN, MUSTCOMPLETE, SHOWLOTUS, LOADLOTUS, COMPLETE, DEFAULTVALUE, INDEXSEARCH, ISDATAUNIQUE, AUTOCOMPLETE, ISREFERENCED From index_r_doc_type DT  inner Join ZIR On DT.index_id = ZIR.indexID And DT.DOC_TYPE_ID = ZIR.DOCTYPEID  where (userid In (Select groupid from zvw_usr_r_groupsandtheirinh  where usrid  In ( ")
            StrSelect.Append(Membership.MembershipHelper.CurrentUser.ID)
            StrSelect.Append(") ) Or userid =  ")
            StrSelect.Append(Membership.MembershipHelper.CurrentUser.ID)
            StrSelect.Append(") And (righttype in ( 53,51, 80))  ) dt Left Join ZIndexHierarchyKey ZH On DT.INDEX_ID = ZH.INDICE ")

            'StrSelect.Append("select distinct INDEX_ID, DOC_TYPE_ID, ORDEN, MUSTCOMPLETE, SHOWLOTUS, LOADLOTUS, COMPLETE, DEFAULTVALUE, INDEXSEARCH, ISDATAUNIQUE, AUTOCOMPLETE, ISREFERENCED, ")

            'If Server.isSQLServer Then
            '    StrSelect.Append(" IsNull(ZH.IndicePadre, -1) AS IndicePadre ")
            'Else
            '    StrSelect.Append(" NVL(ZH.IndicePadre, -1) AS IndicePadre ")
            'End If

            'StrSelect.Append(" from index_r_doc_type DT ")
            'StrSelect.Append(" LEFT JOIN ZIndexHierarchyKey ZH ON DT.INDEX_ID = ZH.INDICE ")
            'StrSelect.Append(" where DOC_TYPE_ID not in (select aditional from usr_rights where (groupid in (")

            'StrSelect.Append(Membership.MembershipHelper.CurrentUser.ID)
            'StrSelect.Append(",")
            'For Each g As IUserGroup In Membership.MembershipHelper.CurrentUser.Groups
            '    StrSelect.Append(g.ID)
            '    StrSelect.Append(",")
            'Next
            'StrSelect.Remove(StrSelect.Length - 1, 1)
            'StrSelect.Append(")) and objid = 2 and rtype = 56)")

            If Server.isSQLServer Then
                StrSelect.Replace("NVL", "IsNull")
            End If

            DsTemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect.ToString)

            Dim PK() As DataColumn = New DataColumn() {DsTemp.Tables(0).Columns("doc_type_id"), DsTemp.Tables(0).Columns("index_id")}

            DsTemp.Tables(0).PrimaryKey = PK

            'StrSelect.Remove(0, StrSelect.Length)
            'StrSelect.Append("Select distinct INDEX_ID, DOC_TYPE_ID, ORDEN, MUSTCOMPLETE, SHOWLOTUS, LOADLOTUS, COMPLETE, DEFAULTVALUE, INDEXSEARCH, ISDATAUNIQUE, AUTOCOMPLETE, ISREFERENCED, ")

            'If Server.isSQLServer Then
            '    StrSelect.Append(" IsNull(ZH.IndicePadre, -1) As IndicePadre ")
            'Else
            '    StrSelect.Append(" NVL(ZH.IndicePadre, -1) As IndicePadre ")
            'End If

            'StrSelect.Append(" from index_r_doc_type DT ")
            'StrSelect.Append(" LEFT JOIN ZIndexHierarchyKey ZH On DT.INDEX_ID = ZH.INDICE ")
            'StrSelect.Append(" inner join ZIR On DT.index_id = ZIR.indexID And DT.DOC_TYPE_ID = ZIR.DOCTYPEID where (userid = ")
            'StrSelect.Append(Membership.MembershipHelper.CurrentUser.ID)
            'For Each g As IUserGroup In Membership.MembershipHelper.CurrentUser.Groups
            '    StrSelect.Append(" Or userid=" & g.ID)
            'Next
            'StrSelect.Append(") And (righttype = 53 Or righttype=51 Or righttype = 80) And DOC_TYPE_ID In (Select aditional from usr_rights where (groupid in (")

            'StrSelect.Append(Membership.MembershipHelper.CurrentUser.ID)
            'StrSelect.Append(",")
            'For Each g As IUserGroup In Membership.MembershipHelper.CurrentUser.Groups
            '    StrSelect.Append(g.ID)
            '    StrSelect.Append(",")
            'Next
            'StrSelect.Remove(StrSelect.Length - 1, 1)
            'StrSelect.Append(")) and objid = 2 and rtype = 56)")

            'DsTemp.Merge(Server.Con.ExecuteDataset(CommandType.Text, StrSelect.ToString))

            DsTemp.Tables(0).DefaultView.Sort = "Orden ASC"
            Dim DT As DataTable = DsTemp.Tables(0).DefaultView.ToTable

            Return DT
        Finally
            StrSelect = Nothing
        End Try
    End Function

    Public Function GetIndexText(ByVal DocTypeId As Int64) As Int32
        Dim strselect As New System.Text.StringBuilder
        Try
            strselect.Append("Select DOCUMENTALID FROM Doc_Type Where Doc_Type_Id = ")
            strselect.Append(DocTypeId)
            Return Server.Con.ExecuteScalar(CommandType.Text, strselect.ToString)
        Finally
            strselect = Nothing
        End Try
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Arraylist con los DOC_TYPE_NAMES
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' 	[Marcelo]	05/03/2006	Modified(Add if on the catch)
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetDocTypeNames() As ArrayList
        Static arr As ArrayList = arr
        Dim strselect As New System.Text.StringBuilder
        Dim ds As DataSet = Nothing
        Try
            If arr Is Nothing Then
                arr = New ArrayList
                strselect.Append("Select Doc_Type_Name FROM Doc_Type order by 1")
                ds = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
                For i As Int32 = 0 To ds.Tables(0).Rows.Count - 1
                    arr.Add(Convert.ToString(ds.Tables(0).Rows(i).Item(0)).Trim)
                Next
            End If
            Return arr
        Finally
            strselect = Nothing
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Arraylist con los DOC_TYPE_NAMES_ID
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	28/02/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetDocTypeNamesAndIds() As DataTable
        Dim strselect As New System.Text.StringBuilder
        Dim ds As DataSet = Nothing
        Try
            strselect.Append("Select Doc_Type_Id, Doc_Type_Name FROM Doc_Type order by Doc_Type_Name")
            ds = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
            Return ds.Tables(0)
        Finally
            strselect = Nothing
        End Try
    End Function

#Region "Add"

    ''' <summary>
    ''' Método que sirve para agregar un nuevo entidad a la base de datos
    ''' </summary>
    ''' <param name="DocType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	04/05/2009	Modified    El nuevo entidad se agrega a la colección DocTypes
    ''' </history>
    Public Shared Function AddDocType(ByVal DocType As DocType) As Integer
        Dim DocTypeId As Int64 = CoreData.GetNewID(IdTypes.DOCTYPEID)
        Dim StrInsert As New System.Text.StringBuilder

        Try

            StrInsert.Append("INSERT INTO Doc_Type (DOC_TYPE_ID,Doc_Type_Name,File_Format_Id,Disk_Group_id,thumbnails,Icon_Id, Object_Type_Id, AutoName,DOCUMENTALID,Doccount) Values (")
            StrInsert.Append(DocTypeId)
            StrInsert.Append(",'")
            StrInsert.Append(DocType.Name)
            StrInsert.Append("',")
            StrInsert.Append(DocType.FileFormatId)
            StrInsert.Append(",")
            StrInsert.Append(DocType.DiskGroupId)
            StrInsert.Append(",")
            StrInsert.Append(DocType.Thumbnails)
            StrInsert.Append(",")
            StrInsert.Append(DocType.IconId)
            StrInsert.Append(",")
            StrInsert.Append(DocType.ObjecttypeId)
            StrInsert.Append(",'")
            StrInsert.Append(DocType.AutoNameCode)
            StrInsert.Append("',0,0)")
            Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert.ToString)

        Finally
            StrInsert = Nothing
        End Try

        Dim STRSELECT As New System.Text.StringBuilder
        Dim DSTEMP As DataSet = Nothing

        Try
            STRSELECT.Append("SELECT Doc_Type_Id FROM Doc_Type where (Doc_Type_name = '")
            STRSELECT.Append(Trim(DocType.Name))
            STRSELECT.Append("') ORDER BY Doc_Type_Name")
            DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, STRSELECT.ToString)
            DSTEMP.Tables(0).TableName = "Doc_Type"
            Dim Cr As CreateTables = Server.CreateTables

            Try
                'agrego la tabla del documento
                Cr.AddDocsTables(DocTypeId)
            Catch ex As Exception
                Throw New Exception("Ocurrió un error al crear las tablas. " & ex.ToString, ex)
            End Try

            Return DocTypeId
        Finally
            STRSELECT = Nothing
            DSTEMP.Dispose()
            DSTEMP = Nothing
        End Try
    End Function
#End Region

#Region "Delete"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina un DOC_Type. Recibe como parametro un objeto Doc_type
    ''' </summary>
    ''' <param name="DocType">Objeto doc_type que se desea eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DelDocType(ByVal DocType As DocType)
        Dim StrDelete As New System.Text.StringBuilder
        Try
            StrDelete.Append("DELETE from Doc_Type where (doc_type_id = ")
            StrDelete.Append(DocType.ID)
            StrDelete.Append(")")
            Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete.ToString)
        Finally
            StrDelete = Nothing
        End Try
    End Sub
#End Region

#Region "Update"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza en la BD un Objeto Doc_type
    ''' </summary>
    ''' <param name="DocType"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdateDocType(ByVal DocType As DocType)
        'PACKAGE UPDATE_DOC_TYPE_pkg AS
        'PROCEDURE Update_DocType
        Dim StrUpdate As New System.Text.StringBuilder
        Try
            StrUpdate.Append("UPDATE Doc_Type SET Doc_Type_Name = '")
            StrUpdate.Append(DocType.Name)
            StrUpdate.Append("',File_Format_Id = ")
            StrUpdate.Append(DocType.FileFormatId)
            StrUpdate.Append(",Disk_Group_id = ")
            StrUpdate.Append(DocType.DiskGroupId)
            StrUpdate.Append(",thumbnails = ")
            StrUpdate.Append(DocType.Thumbnails)
            StrUpdate.Append(",Icon_Id = ")
            StrUpdate.Append(DocType.IconId)
            StrUpdate.Append(", Object_Type_Id =")
            StrUpdate.Append(DocType.ObjecttypeId)
            StrUpdate.Append(", AutoName ='")
            StrUpdate.Append(DocType.AutoNameCode)
            StrUpdate.Append("' where (Doc_Type_Id = ")
            StrUpdate.Append(DocType.ID)
            StrUpdate.Append(")")
            Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate.ToString)

            StrUpdate.Remove(0, StrUpdate.Length)
        Finally
            StrUpdate = Nothing
        End Try

    End Sub
#End Region

    Public Shared Function DocTypeIsDuplicated(ByVal DocTypeName As String) As Boolean

        Dim strSelect As New System.Text.StringBuilder
        Try
            strSelect.Append("SELECT COUNT(1) from DOC_TYPE where (Doc_Type_Name = '")
            strSelect.Append(Trim(DocTypeName))
            strSelect.Append("')")
            Dim Qrows As Int32 = Convert.ToInt32(Server.Con.ExecuteScalar(CommandType.Text, strSelect.ToString))
            If Qrows > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception("Ocurrio un error al consultar la duplicidad del Entidad " & ex.ToString)
        Finally
            strSelect = Nothing
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve la cantidad de valores que tiene cargado un indice  
    ''' </summary>
    ''' <param name="Doctypeid">ID del Tipo de documento</param>
    ''' <param name="Indexid">ID del Index</param>
    ''' <returns>Cantidad</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/06/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function IndexCountInDocuments(ByVal Doctypeid As Int64, ByVal Indexid As Int64) As Int64
        Dim sql As System.Text.StringBuilder
        Try
            sql = New System.Text.StringBuilder
            sql.Append("Select count(1) from DOC")
            sql.Append(Doctypeid)
            sql.Append(" where I")
            sql.Append(Indexid)
            sql.Append(" is not null")
            Return Convert.ToInt64(Server.Con.ExecuteScalar(CommandType.Text, sql.ToString))
        Finally
            sql = Nothing
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve la cantidad de documentos, en base al ID
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function DocumentsCount(ByVal DocTypeId As Int64) As Int32
        'MAXI NO SOLUCIONADO
        Dim Sql As New System.Text.StringBuilder
        Sql.Append("Select COUNT(1) from DOC_T")
        Sql.Append(DocTypeId)
        Dim DocCount As Int32 = Convert.ToInt32(Server.Con.ExecuteScalar(CommandType.Text, Sql.ToString))
        Sql = Nothing

        'MAXI 11/11/05
        'Sql = "Update doc_type set Doccount=" & DocCount & " where doc_type_id=" & DocTypeId
        'Server.Con.ExecuteNonQuery(CommandType.Text, Sql)

        If Server.isOracle Then
            Dim parNames() As String = {"DocCount", "DocTypeId"}
            ' Dim parTypes() As Object = {13, 13}
            Dim parValues() As Object = {DocCount, DocTypeId}
            'Server.Con.ExecuteNonQuery("ZDtUpdDoctypes_pkg.ZDtUpdDoccountByDtId", parValues)
            Server.Con.ExecuteNonQuery("zsp_doctypes_100.UpdDocCountById", parValues)
        Else
            Dim parValues() As Object = {DocCount, DocTypeId}
            'Server.Con.ExecuteNonQuery("ZDtUpdDoccountByDtId", parvalues)
            Server.Con.ExecuteNonQuery("zsp_doctypes_100_UpdDocCountById", parValues)
        End If
        Return DocCount
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Convierte un Dataset en un Arraylist, en base a una columna
    ''' </summary>
    ''' <param name="Ds"></param>
    ''' <param name="ColumnId"></param>
    '''     ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function ConvertDatasettoArraylist(ByVal Ds As DataSet, ByVal ColumnId As Int32) As ArrayList
        Dim Array As New ArrayList
        Dim i As Int32
        For i = 0 To Ds.Tables(0).Rows.Count - 1
            Array.Add(CInt(Ds.Tables(0).Rows(i).Item(ColumnId)))
        Next
        Array.Sort()
        Return Array
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea un Entidad en base a otro ya existente.
    ''' </summary>
    ''' <param name="DocIDOrigen">ID del entidad original</param>
    ''' <param name="DocNameDestino">Nombre del nuevo entidad</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Lo genera en el mismo volumen que el original
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function CopyDoc(ByVal DocIDOrigen As Int64, ByVal DocNameDestino As String) As Int32
        Dim DocIDDestino As Int32 = CoreData.GetNewID(IdTypes.DOCTYPEID)
        Dim parvalues() As String = {DocIDOrigen.ToString(), DocIDDestino.ToString(), DocNameDestino}
        Dim parnames() As String = {"DocTypeId", "NewDocTypeId", "NewName"}
        Dim partypes() As String = {"10", "10", "3"}
        Dim DocTable As CreateTables = Server.CreateTables
        Dim Ok As Boolean = False

        Dim IndexsList As New Generic.List(Of IIndex)
        '        Dim ArrayId As New ArrayList
        '       Dim ArrayType As New ArrayList
        '      Dim ArrayLen As New ArrayList

        '    IndexsList = Indexs_Factory.GetIndexs(DocIDOrigen)
        '        ArrayId = ConvertDatasettoArraylist(Indexs_Factory.GetIndexSch(Convert.ToInt32(DocIDOrigen)), 0)
        '       ArrayType = ConvertDatasettoArraylist(GetIndexSchema(Convert.ToInt32(DocIDOrigen)), 2)    ' Index_Type
        '      ArrayLen = ConvertDatasettoArraylist(GetIndexSchema(Convert.ToInt32(DocIDOrigen)), 3)

        If Server.isOracle Then
            'Server.Con.ExecuteNonQuery("Copy_Doc_Type_Pkg.Copy_Doc_Type", parValues)
            Server.Con.ExecuteNonQuery("zsp_doctypes_100.CopyDocType", parvalues)
            DocTable.AddDocsTables(DocIDDestino)
            '      DocTable.AddIndexColumn(DocIDDestino, ArrayId, ArrayType, ArrayLen)
            Ok = True
        Else
            'Server.Con.ExecuteNonQuery("Copy_Doc_Type", parvalues)
            Server.Con.ExecuteNonQuery("zsp_doctypes_100_CopyDocType", parvalues)
            DocTable.AddDocsTables(DocIDDestino)
            '    DocTable.AddIndexColumn(DocIDDestino, ArrayId, ArrayType, ArrayLen)
            Ok = True
        End If

        'Ahora Consigo el ultimo DocTypeID
        If Ok = True Then
            'Dim Documento As New utilities
            Return CoreData.GetNewID(IdTypes.DOCID)
        Else
            Return 0
        End If
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' obtiene el ID de un entidad en base al nombre del mismo
    ''' </summary>
    ''' <param name="DocTypeName">Nombre del Doc_type del cual se desea averiguar el ID</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetDocTypeIdByName(ByVal DocTypeName As String) As Int64
        Dim strselect As New System.Text.StringBuilder
        strselect.Append("select doc_type_id from doc_type where upper(doc_type_name)='")
        strselect.Append(DocTypeName.ToUpper.Trim)
        strselect.Append("'")
        Return Server.Con.ExecuteScalar(CommandType.Text, strselect.ToString)
    End Function

#Region "LinkIndexs"
    Public Shared Function IndexIsLinked(ByVal doctypeid As Int32, ByVal indexid As Int32) As Boolean

        Dim sql As String = "Select count(1) from Index_link where (doctypeid1=" & doctypeid & " and indexid1=" & indexid & ") or (doctypeid2 = " & doctypeid & " And indexid2 = " & indexid & ")"
        Dim i As Int16 = Convert.ToInt16(Server.Con.ExecuteScalar(CommandType.Text, sql))
        'Dim i As Int16

        If i > 0 Then
            Return True
        Else
            Return False
        End If
    End Function



    Public Shared Sub UpdateSomeRowsCascade(ByVal doctypeIdParent As Int32, ByVal indexParentId As Int32, ByVal Value As Object, ByVal whereindexId As Int32, ByVal wherevalue As Object)
        'Actualiza en cascada los indices vinculados, con una condicion Where

        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append("Select DOCTYPE2,INDEX2 from index_link where DOCTYPE1=")
            sql.Append(doctypeIdParent)
            sql.Append(" and INDEX1=")
            sql.Append(indexParentId)
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql.ToString)
            Dim i As Int32
            Dim table As String
            Dim indice As String
            For i = 0 To ds.Tables(0).Rows.Count - 1
                Dim CurrentRow As DataRow = ds.Tables(0).Rows(i)
                table = "doc_I" & CurrentRow(0).ToString()
                indice = "I" & CurrentRow(1).ToString()
                sql.Remove(0, sql.Length)
                If Not IsNumeric(Value) Then
                    sql.Append("Update ")
                    sql.Append(table)
                    sql.Append(" set ")
                    sql.Append(indice)
                    sql.Append("='")
                    sql.Append(Value)
                    sql.Append("'")
                Else
                    sql.Append("Update ")
                    sql.Append(table)
                    sql.Append(" set ")
                    sql.Append(indice)
                    sql.Append("=")
                    sql.Append(Value)
                End If
                If IsNumeric(wherevalue) Then
                    sql.Append(" where I")
                    sql.Append(whereindexId)
                    sql.Append("=")
                    sql.Append(wherevalue)
                Else
                    sql.Append(" where I")
                    sql.Append(whereindexId)
                    sql.Append("='")
                    sql.Append(wherevalue)
                    sql.Append("'")
                End If
                'MAXI 11/11/05
                'Server.Con.ExecuteNonQuery(CommandType.Text, sql)

                If Server.isOracle Then
                    Dim parNames() As String = {"strsql"}
                    ' Dim parTypes() As Object = {7}
                    Dim parValues() As Object = {sql}
                    'Server.Con.ExecuteDataset("ZExecSql_pkg.ZExecSQL", parValues)
                    Server.Con.ExecuteDataset("zsp_generic_100.ExecSqlString", parValues)
                Else
                    Dim parValues() As Object = {sql}
                    'Server.Con.ExecuteNonQuery("ZExecSQL", parvalues)
                    Server.Con.ExecuteNonQuery("zsp_generic_100_ExecSqlString", parValues)
                End If

            Next
            ds.Dispose()
        Finally
            sql = Nothing
        End Try
    End Sub

#End Region


#Region "WF"



    ''' <summary>
    ''' Se obtienen los workflow doc types
    ''' </summary>
    ''' <param name="WfId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/06/2008	Modified    Se agrego la consulta en Oracle
    ''' 	[Sebastian]	22/07/2008	Modified    Se cambio el nombre de la funcion y el valor del retorno
    ''' </history>
    Public Shared Function GetAllWFDocTypes(ByVal WfId As Int64) As DataSet

        Dim DSTEMP As DataSet
        Dim strselect As System.Text.StringBuilder = New StringBuilder()

        If Server.isOracle Then

            strselect.Append("SELECT DOC_TYPE.DOC_TYPE_ID, DOC_TYPE.DOC_TYPE_NAME, WF_DT.WFId ")
            strselect.Append("FROM   DOC_TYPE INNER JOIN WF_DT ")
            strselect.Append("ON DOC_TYPE.DOC_TYPE_ID = WF_DT.DocTypeId Where WF_DT.wfid = " & WfId & " ORDER BY DOC_TYPE.DOC_TYPE_Name")

        Else
            strselect.Append("SELECT * from Zwfviewwfdoctypes where wfid = " & WfId & " ORDER BY Doc_Type_Name")
        End If

        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString())

        Return DSTEMP

    End Function
    Public Shared Function GetDocTypeWfIds(ByVal DocTypeId As Int64) As ArrayList
        Dim strselect As String = "SELECT WfId FROM WF_DT Where DocTypeId = " & DocTypeId
        Dim D As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Dim A As New ArrayList
        For Each R As DataRow In D.Tables(0).Rows
            A.Add(R.Item(0).ToString)
        Next
        Return A
    End Function
    Public Shared Function GetDocTypeWfIds(ByVal DocTypeId As Int64, ByVal t As Transaction) As ArrayList
        Dim strselect As String = "SELECT WfId FROM WF_DT Where DocTypeId = " & DocTypeId
        Dim D As DataSet = t.Con.ExecuteDataset(t.Transaction, CommandType.Text, strselect)
        Dim A As New ArrayList
        For Each R As DataRow In D.Tables(0).Rows
            A.Add(R.Item(0).ToString)
        Next
        Return A
    End Function

    Public Shared Function GetDocTypeWorkFlowByWfId(ByVal WfId As Int64) As DataSet
        Dim strselect As String = "SELECT * FROM WF_DT Where WFId = " & WfId
        Dim D As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Return D
    End Function

    Public Shared Sub AsignDocType2Wf(ByVal WFID As Int64, ByVal DocTypeId As Int64)
        Dim strinsert As String = "INSERT INTO WF_DT (WfId,DocTypeId) VALUES (" & WFID & "," & DocTypeId & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    End Sub
    Public Shared Sub RemoveDocTypefromWf(ByVal WFID As Int64, ByVal DocTypeId As Int64)
        Dim strdelete As String = "DELETE WF_DT WHERE WfId = " & WFID & " and DocTypeId = " & DocTypeId
        Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    End Sub
    Public Shared Sub RemoveAllAsociationsByDT(ByVal DocTypeId As Int64)
        Dim strdelete As String = "DELETE FROM WF_DT WHERE DocTypeId = " & DocTypeId
        Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    End Sub
#End Region

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Checkea para todos los Doc_Types que existan las respectivas tablas
    ''' DOC_I,DOC_T y DOC_D
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub CheckDockTypeTables()
        Dim DocTypes As DataSet = GetDocTypesDsDocType()
        Dim i As Integer
        ' Dim row As DSDOCTYPE.Tables("DOC_TYPE")Row
        For i = 0 To DocTypes.Tables("Doc_Type").Rows.Count - 1
            checkDocTypeRow(DocTypes.Tables("Doc_Type").Rows(i))
        Next
    End Sub

#Region "Eventos"
    Public Shared Event CheckTable(ByVal str As String)
#End Region


    Private Shared Sub checkDocTypeRow(ByVal row As System.Data.DataRow)
        Dim errorFlag As Boolean = False
        RaiseEvent CheckTable("DOC_T" + CInt(row.Item(0)).ToString)
        Dim str As New System.Text.StringBuilder
        Try
            str.Append("Select * from DOC_T")
            str.Append(CInt(row.Item(0)).ToString)
            Try
                Server.Con.ExecuteDataset(CommandType.Text, str.ToString)
            Catch ex As Exception
                errorFlag = True
            End Try
            RaiseEvent CheckTable("DOC_D" + CInt(row.Item(0)).ToString)
            str.Remove(0, str.Length)
            str.Append("Select * from DOC_D")
            str.Append(CInt(row.Item(0)).ToString)
            Try
                Server.Con.ExecuteDataset(CommandType.Text, str.ToString)
            Catch ex As Exception
                errorFlag = True
            End Try
            RaiseEvent CheckTable("DOC" & CInt(row.Item(0)).ToString)
            str.Remove(0, str.Length)
            str.Append("Select * from DOC")
            str.Append(CInt(row.Item(0)).ToString)
            Try
                Server.Con.ExecuteDataset(CommandType.Text, str.ToString)
            Catch ex As Exception
                errorFlag = True
            End Try
            If errorFlag Then
                'if msgbox("Se detecto que la tabla " row.
                DeleteDocTypeTablesTables(CInt(row.Item(0)).ToString)
            End If
        Finally
            str = Nothing
        End Try
    End Sub
    Private Shared Sub DeleteDocTypeTablesTables(ByVal docID As String)
        Server.Con.ExecuteNonQuery(CommandType.Text, "Delete from doc_type where doc_type_id =" & docID)
        Server.Con.ExecuteNonQuery(CommandType.Text, "drop table DOC_D" & docID)
        Server.Con.ExecuteNonQuery(CommandType.Text, "DROP TABLE DOC_I" & docID)
        Server.Con.ExecuteNonQuery(CommandType.Text, "DROP TABLE DOC_T" & docID)
        Server.Con.ExecuteNonQuery(CommandType.Text, "delete from doc_type_R_doc_type_group where doc_type_id=DOC_T" & docID)
        Server.Con.ExecuteNonQuery(CommandType.Text, "delete from index_r_doc_type where doc_type_id=DOC_T" & docID)
    End Sub
#Region "AutoNombre"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve el Autonombre
    ''' </summary>
    ''' <param name="AutoNameCode"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Martin]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Overloads Function GetAutoName(ByVal AutoNameCode As String, ByVal DocTypeName As String, ByVal CreateDate As Date, ByVal EditDate As Date, ByVal Indexs As List(Of IIndex)) As String
        Dim PreAutoNameText As String = String.Empty
        'Dim str As String = String.Empty
        PreAutoNameText &= AutoNameCode.Trim()
        PreAutoNameText = PreAutoNameText.Replace("@DT@", DocTypeName)
        PreAutoNameText = PreAutoNameText.Replace("@CD@", CreateDate.ToString())
        PreAutoNameText = PreAutoNameText.Replace("@ED@", EditDate.ToString())

        Dim i As Integer
        Dim IndexIdLstCount As Int32 = Indexs.Count
        For i = 0 To IndexIdLstCount - 1
            Dim strData As String = String.Empty
            Dim CurrentIndex As Index = Nothing

            CurrentIndex = DirectCast(Indexs(i), Index)
            If CurrentIndex.DropDown = IndexAdditionalType.LineText Then
                If Not IsNothing(CurrentIndex.DataTemp) AndAlso Not IsDBNull(CurrentIndex.DataTemp) AndAlso Not String.IsNullOrEmpty(CurrentIndex.DataTemp.Trim()) Then
                    strData = Trim(CurrentIndex.DataTemp)
                Else
                    strData = Trim(CurrentIndex.Data)
                End If
            Else
                If String.IsNullOrEmpty(CurrentIndex.dataDescriptionTemp) Then
                    If String.IsNullOrEmpty(CurrentIndex.dataDescription) Then
                        strData = CurrentIndex.Data.Trim()
                    Else
                        strData = CurrentIndex.dataDescription.Trim()
                    End If
                Else
                    strData = CurrentIndex.dataDescriptionTemp.Trim()
                End If
            End If
            PreAutoNameText = PreAutoNameText.Replace("@I" & CurrentIndex.ID.ToString() & "@", strData)
        Next
        If PreAutoNameText.Trim = String.Empty Then Return DocTypeName
        Return PreAutoNameText.Trim().Replace("  ", " ").Replace(" - - ", " - ")
    End Function
    Public Shared Function GetAutoNameCode(ByVal AutoNameText As String, ByVal IndexTable As DataTable) As String
        Dim PreAutoNameCode As String
        PreAutoNameCode = AutoNameText.Trim
        PreAutoNameCode = PreAutoNameCode.Replace("@Entidad@", "@DT@")
        PreAutoNameCode = PreAutoNameCode.Replace("@Creado@", "@CD@")
        PreAutoNameCode = PreAutoNameCode.Replace("@Modificado@", "@ED@")

        Dim i As Integer
        Dim IndexTableRowsCount As Int32 = IndexTable.Rows.Count
        For i = 0 To IndexTableRowsCount - 1
            PreAutoNameCode = PreAutoNameCode.Replace("@" & IndexTable.Rows(i)("Index_Name").ToString().Trim() & "@", "@I" & IndexTable.Rows(i).Item("Index_Id").ToString() & "@")
        Next
        Return PreAutoNameCode
    End Function
    Public Shared Function GetAutoNameText(ByVal AutoNameCode As String, ByVal IndexTable As DataTable) As String
        Dim PreAutoNameText As String
        PreAutoNameText = AutoNameCode
        PreAutoNameText = Replace(PreAutoNameText.Trim, "@DT@", "@Entidad@")
        PreAutoNameText = Replace(PreAutoNameText.Trim, "@CD@", "@Creado@")
        PreAutoNameText = Replace(PreAutoNameText.Trim, "@ED@", "@Modificado@")

        Dim i As Integer
        For i = 0 To IndexTable.Rows.Count - 1
            PreAutoNameText = Replace(PreAutoNameText.Trim, "@I" & IndexTable.Rows(i).Item("Index_Id").ToString() & "@", "@" & Trim(IndexTable.Rows(i).Item("Index_Name").ToString()) & "@")
        Next

        Return PreAutoNameText.Trim
    End Function

#End Region

    'Public Shared Function GetIndexSchema(ByVal docTypeId As Int64) As DataSet
    '    Dim dstemp As DataSet = Nothing
    '    if Server.IsOracle then
    '        Dim parNames() As String = {"DocTypeId", "io_cursor"}
    '        ' Dim parTypes() As Object = {13, 5}
    '        Dim parValues() As Object = {docTypeId, 2}
    '        'dstemp = Server.Con.ExecuteDataset("CLSDOC_GENERACIONINDICES_PKG.GeneracionIndices", parValues)
    '        dstemp = Server.Con.ExecuteDataset("zsp_index_100.IndexGeneration", parValues)
    '        dstemp.Tables(0).TableName = "IndexLst"
    '    Else
    '        Dim parameters() As Object = {docTypeId}
    '        'dstemp = Server.Con.ExecuteDataset("GeneracionIndices", parameters)
    '        dstemp = Server.Con.ExecuteDataset("zsp_index_100_IndexGeneration", parameters)
    '        dstemp.Tables(0).TableName = "IndexLst"
    '    End If
    '    Return dstemp
    'End Function

    ''' <summary>
    '''  Obtiene todos los indices de los documentos
    ''' </summary>
    ''' <returns>Dataset</returns>
    ''' <remarks></remarks>
    'Public Shared Function GetAllIndexs() As DataSet
    '    Dim DSTEMP1 As DataSet = Nothing
    '    Dim Ds As New DataSet
    '    Dim strselect As New System.Text.StringBuilder
    '    Try
    '        strselect.Append("SELECT Index_Id, Index_Name, Index_Type, Index_Len, Object_Type_Id FROM Doc_Index ")
    '        strselect.Append(" Order By Index_Name")
    '        DSTEMP1 = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
    '        DSTEMP1.Tables(0).TableName = "Doc_Index"
    '        Ds.Merge(DSTEMP1)
    '        Return Ds
    '    Finally
    '        strselect = Nothing
    '        If Not IsNothing(DSTEMP1) Then
    '            DSTEMP1.Dispose()
    '            DSTEMP1 = Nothing
    '        End If
    '    End Try
    'End Function
    ''' <summary>
    ''' Cambia el orden de un indice asociado en la base de datos
    ''' </summary>
    ''' <param name="docTypeID">El ID del entidad al que pertenecen los indices</param>
    ''' <param name="selectedIndexID">El id del indice seleccionado </param>
    ''' <param name="selectedIndexOrder">El numero de orden del indice seleccionado</param>
    ''' <param name="modifiedIndexId">El id del indice que se va a modificar</param>
    ''' <param name="modifiedIndexOrder">El orden del indice que se va a modificar</param>
    ''' <remarks></remarks>
    Public Shared Sub ChangeIndexOrder(ByVal docTypeID As Int64, ByVal selectedIndexID As Int64, ByVal selectedIndexOrder As Integer, ByVal modifiedIndexId As Int64, ByVal modifiedIndexOrder As Int64)
        If Server.isOracle = False Then
            Dim StrBuilder As New StringBuilder()
            StrBuilder.Append("UPDATE INDEX_R_DOC_TYPE SET ORDEN = ")
            StrBuilder.Append(selectedIndexOrder.ToString())
            StrBuilder.Append(" WHERE doc_Type_id = ")
            StrBuilder.Append(docTypeID.ToString())
            StrBuilder.Append(" AND index_id = ")
            StrBuilder.Append(modifiedIndexId.ToString())

            StrBuilder.Append(" UPDATE INDEX_R_DOC_TYPE SET ORDEN = ")
            StrBuilder.Append(modifiedIndexOrder.ToString())
            StrBuilder.Append(" WHERE doc_Type_id = ")
            StrBuilder.Append(docTypeID.ToString())
            StrBuilder.Append(" AND index_id = ")
            StrBuilder.Append(selectedIndexID.ToString())

            Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString())
        Else
            Dim StrBuilder As New StringBuilder()
            StrBuilder.Append("UPDATE INDEX_R_DOC_TYPE SET ORDEN=")
            StrBuilder.Append(selectedIndexOrder.ToString())
            StrBuilder.Append(" WHERE doc_Type_id=")
            StrBuilder.Append(docTypeID.ToString())
            StrBuilder.Append(" AND index_id=")
            StrBuilder.Append(modifiedIndexId.ToString())

            Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString())

            StrBuilder.Remove(0, StrBuilder.Length)

            StrBuilder.Append("UPDATE INDEX_R_DOC_TYPE SET ORDEN=")
            StrBuilder.Append(modifiedIndexOrder.ToString())
            StrBuilder.Append(" WHERE doc_Type_id=")
            StrBuilder.Append(docTypeID.ToString())
            StrBuilder.Append(" AND index_id=")
            StrBuilder.Append(selectedIndexID.ToString())

            Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString())
        End If
    End Sub





    ''' <summary>
    '''  Obtiene los indices de un entidad
    ''' </summary>
    ''' <param name="DocTypeId">Id de entidad</param>
    ''' <history> Marcelo Modified 03/02/2009
    '''           Marcelo Modified 05/02/2009</history>
    ''' <returns>Dataset</returns>
    ''' <remarks></remarks>
    Public Shared Function getAsignedIndexAndNonAsignedByDocTypeId(ByVal DocTypeId As Int64) As DataSet
        Dim DSTEMP1 As DataSet = Nothing
        Dim Ds As New DataSet
        Dim dstemp As DataSet
        Dim CondNegation As New System.Text.StringBuilder
        Dim strselect As New System.Text.StringBuilder
        Try
            If Server.isOracle Then
                Dim parNames() As String = {"DocTypeId", "io_cursor"}
                ' Dim parTypes() As Object = {OracleType.Number, OracleType.Cursor}
                Dim parValues() As Object = {DocTypeId, 2}
                dstemp = Server.Con.ExecuteDataset("zsp_index_200.GetDocTypeIndexs", parValues)
                dstemp.Tables(0).TableName = "AsignedIndex"
                Ds.Merge(dstemp)
            Else
                Dim parValues() As Object = {DocTypeId}
                'Ds = Server.Con.ExecuteDataset("FrmDocType_LoadIndex", parValues)
                Ds = Server.Con.ExecuteDataset("zsp_docindex_200_LoadIndex", parValues)
                Ds.Tables(0).TableName = "AsignedIndex"
            End If
            Dim qRows As Integer = Ds.Tables("AsignedIndex").Rows.Count - 1
            Dim i As Int32
            For i = 0 To qRows
                If i = 0 Then
                    CondNegation.Append(" where (Index_Id <> ")
                    CondNegation.Append(Ds.Tables("AsignedIndex").Rows(i).Item("Index_Id"))
                    CondNegation.Append(")")
                Else
                    CondNegation.Append(" and (Index_Id <> ")
                    CondNegation.Append(Ds.Tables("AsignedIndex").Rows(i).Item("Index_Id"))
                    CondNegation.Append(")")
                End If
            Next
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo descripcion de indices Indices")
            strselect.Append("SELECT Index_Id, Index_Name, Index_Type, Index_Len, Object_Type_Id, Dropdown FROM doc_index ")
            strselect.Append(CondNegation)
            strselect.Append(" Order By Index_Name")
            DSTEMP1 = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
            DSTEMP1.Tables(0).TableName = "doc_index"
            Ds.Merge(DSTEMP1)
            Return Ds
        Finally
            strselect = Nothing
            CondNegation = Nothing
            If Not IsNothing(DSTEMP1) Then
                DSTEMP1.Dispose()
            End If
            DSTEMP1 = Nothing
        End Try
    End Function
    'Public Shared Function GetIndexsByDocTypeIdAsDataset(ByVal DocTypeId As Int64) As DataSet
    '    Dim DSTEMP1 As DataSet = Nothing
    '    Dim Ds As New DataSet
    '    Dim dstemp As DataSet
    '    Dim CondNegation As New System.Text.StringBuilder
    '    Dim strselect As New System.Text.StringBuilder
    '    Try
    '        if Server.IsOracle then
    '            Dim parNames() As String = {"DocTypeId", "io_cursor"}
    '            ' Dim parTypes() As Object = {13, 5}
    '            Dim parValues() As Object = {DocTypeId, 2}
    '            'dstemp = Server.Con.ExecuteDataset("FRMDOCTYPE_LOADINDEX_PKG.loadIndex", parValues)
    '            dstemp = Server.Con.ExecuteDataset("zsp_index_100.GetDocTypeIndexs", parValues)
    '            dstemp.Tables(0).TableName = "AsignedIndex"
    '            Ds.Merge(dstemp)
    '        Else
    '            'Dim parValues() As Object = {DocType.ID}
    '            'Ds = Server.Con.ExecuteDataset("FrmDocType_LoadIndex", parValues)
    '            'Se modifico porque el paquete pedia un integer en vez de un array - MC
    '            Ds = Server.Con.ExecuteDataset("zsp_docindex_100_LoadIndex", DocTypeId)
    '            Ds.Tables(0).TableName = "AsignedIndex"
    '        End If
    '        Dim qRows As Integer = Ds.Tables("AsignedIndex").Rows.Count - 1
    '        Dim i As Int32
    '        For i = 0 To qRows
    '            If i = 0 Then
    '                CondNegation.Append(" where (Index_Id <> ")
    '                CondNegation.Append(Ds.Tables("AsignedIndex").Rows(i).Item("Index_Id"))
    '                CondNegation.Append(")")
    '            Else
    '                CondNegation.Append(" and (Index_Id <> ")
    '                CondNegation.Append(Ds.Tables("AsignedIndex").Rows(i).Item("Index_Id"))
    '                CondNegation.Append(")")
    '            End If
    '        Next
    '        strselect.Append("SELECT Index_Id, Index_Name, Index_Type, Index_Len, Object_Type_Id FROM Doc_Index ")
    '        strselect.Append(CondNegation)
    '        strselect.Append(" Order By Index_Name")
    '        DSTEMP1 = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)
    '        DSTEMP1.Tables(0).TableName = "Doc_Index"
    '        Ds.Merge(DSTEMP1)
    '        Return Ds
    '    Finally
    '        strselect = Nothing
    '        CondNegation = Nothing
    '        DSTEMP1.Dispose()
    '        DSTEMP1 = Nothing
    '    End Try
    'End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina las Tablas byvaleridas al Doc_type_ID
    ''' </summary>
    ''' <param name="DocType"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------


    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub DeleteTables(ByVal DocType As DocType)
        Server.CreateTables.DeleteTable("doc_i" & DocType.ID)
        Server.CreateTables.DeleteTable("doc_t" & DocType.ID)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina los permisos asignados a un DOCTYPE.
    ''' Debe usarse al eliminar un Doc_type
    ''' </summary>
    ''' <param name="doctype"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub DeleteRights(ByVal doctype As DocType)
        Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE FROM usr_rights WHERE aditional=" & doctype.ID)
    End Sub
    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub RemoveIndex(ByVal Doctype As DocType, ByVal IndexId As Int64)
        Dim strDelete As New System.Text.StringBuilder
        Try
            strDelete.Append("DELETE FROM Index_R_Doc_Type where(Index_Id= ")
            strDelete.Append(IndexId)
            strDelete.Append(") and (Doc_Type_Id = ")
            strDelete.Append(Doctype.ID)
            strDelete.Append(")")
            Server.Con.ExecuteNonQuery(CommandType.Text, strDelete.ToString)
        Finally
            strDelete = Nothing
        End Try
    End Sub
    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub Removecolumn(ByVal doctype As DocType, ByVal indexidarray As ArrayList)
        'quito la columna de la tabla de documentos
        Dim Cr As CreateTables = Server.CreateTables
        Try
            Cr.DelIndexColumn(Convert.ToInt32(doctype.ID), indexidarray)
        Finally
            Cr.Dispose()
            Cr = Nothing
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece un indice como obligatorio para un doc_type
    ''' </summary>
    ''' <param name="DocTypeId">Id del Tipo de documento</param>
    ''' <param name="indexid">Indice que se desea establecer como obligatorio</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SetRequiredIndex(ByVal DocTypeId As Int64, ByVal indexid As Int64)
        'MAXI 11/11/05
        'Dim sql As String
        'Dim i As Int16 = Server.Con.ExecuteScalar(CommandType.Text, "Select Dropdown from doc_Index where index_id=" & indexid)
        Dim i As Int32

        If Server.isOracle Then
            Dim parNames() As String = {"indexid"}
            ' Dim parTypes() As Object = {13}
            Dim parValues() As Object = {indexid}
            'i = Server.Con.ExecuteScalar("ZDocIndGet_pkg.ZDIndGetDdownByInd", parValues)
            i = Int32.Parse(Server.Con.ExecuteScalar("zsp_index_100.GetIndexDropDown", parValues))
        Else
            Dim parValues() As Object = {indexid}
            'i = Server.Con.ExecuteScalar("ZDIndGetDdownByInd", parvalues)
            i = Convert.ToInt32(Server.Con.ExecuteScalar("zsp_index_100_GetIndexDropDown", parValues))
        End If

        If i = 2 Then
            'sql = "Update Index_R_DocType set Mustcomplete=1, ShowLotus=1, LoadLotus=1 where Doc_Type_ID=" & DocTypeId & " and Index_Id=" & indexid
            If Server.isOracle Then
                Dim parNames() As String = {"DocTypeId", "IndexId"}
                ' Dim parTypes() As Object = {13, 13}
                Dim parValues() As Object = {DocTypeId, indexid}
                'Server.Con.ExecuteNonQuery("ZIndRDtUpd_pkg.ZIndRDtUpdByDtIDIndID", parValues)
                Server.Con.ExecuteNonQuery("zsp_index_100.UpdIndexRDoctypeByDtInd", parValues)
            Else
                Dim parValues() As Object = {DocTypeId, indexid}
                'Server.Con.ExecuteNonQuery("ZIndRDtUpdByDtIDIndID", parvalues)
                Server.Con.ExecuteNonQuery("zsp_index_100_UpdIndexRDoctypeByDtInd", parValues)
            End If
        Else
            If Server.isOracle Then
                Dim parNames() As String = {"DocTypeId", "IndexId"}
                ' Dim parTypes() As Object = {13, 13}
                Dim parValues() As Object = {DocTypeId, indexid}
                'Server.Con.ExecuteNonQuery("ZIndRDtUpd_pkg.ZIndRDtUpdByDtIDIndID2", parValues)
                Server.Con.ExecuteNonQuery("zsp_index_100.UpdIndexRDoctypeByDtInd2", parValues)
            Else
                Dim parValues() As Object = {DocTypeId, indexid}
                'Server.Con.ExecuteNonQuery("ZIndRDtUpdByDtIDIndID2", parvalues)
                Server.Con.ExecuteNonQuery("zsp_index_100_UpdIndexRDoctypeByDtInd2", parValues)
            End If
            'sql = "Update Index_R_DocType set Mustcomplete=1, ShowLotus=1 where Doc_Type_ID=" & DocTypeId & " and Index_Id=" & indexid
        End If
        'Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Agrega un Indice a un Entidad. Modifica la tabla DOC_I agregandole a la misma una columna
    ''' </summary>
    ''' <param name="doctype"></param>
    ''' <param name="IndexIdArray"></param>
    ''' <param name="IndexTypeArray"></param>
    ''' <param name="IndexLenArray"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history> 
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Obsolete("Las entidades no corresponden a la capa de datos")>
    Public Shared Sub AddColumn(ByVal doctype As DocType, ByVal IndexIdArray As ArrayList, ByVal IndexTypeArray As ArrayList, ByVal IndexLenArray As ArrayList)
        Dim Cr As CreateTables = Server.CreateTables
        Try
            Cr.AddIndexColumn(Convert.ToInt32(doctype.ID), IndexIdArray, IndexTypeArray, IndexLenArray)
        Finally
            Cr.Dispose()
            Cr = Nothing
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece la relación entre el DOC_TYPE y los Indices
    ''' </summary>
    ''' <param name="indexid">ID del indice que se desea asignar</param>
    ''' <param name="doctypeid">Entidad al que se le desea asignar un Indice</param>
    ''' <param name="order">Orden en el que apareceran listados los indices.</param>
    ''' <param name="Required">Establece si el indice es obligatorio</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub adddoctyperelationindex(ByVal indexid As Int64, ByVal doctypeid As Int64, ByVal order As Int32, ByVal Required As Boolean, ByVal isreferenced As Boolean)
        Dim StrInsert As New System.Text.StringBuilder
        Try
            StrInsert.Append("INSERT INTO Index_R_Doc_Type (Index_Id, Doc_Type_Id, Orden, MustComplete, isreferenced) Values (" & indexid)
            StrInsert.Append(", ")
            StrInsert.Append(doctypeid)
            StrInsert.Append(", ")
            StrInsert.Append(order)
            StrInsert.Append(", ")
            If Required Then
                StrInsert.Append(1)
            Else
                StrInsert.Append(0)
            End If
            StrInsert.Append(", ")
            If isreferenced = True Then
                StrInsert.Append("1")
            Else
                StrInsert.Append("0")
            End If
            StrInsert.Append(")")
            Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert.ToString)
        Finally
            StrInsert = Nothing
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea la vista DOCX
    ''' </summary>
    ''' <param name="DocType"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    '<Obsolete("Las entidades no corresponden a la capa de datos")> _
    'Public Shared Sub CreateView(ByVal DocType As DocType)
    '    Server.CreateTables.CreateView(Convert.ToInt32(DocType.ID))
    'End Sub
    Public Shared Sub AddColumnTextindex(ByVal DocTypeId As Int64, ByVal IndexId As Int64)
        Dim Cr As CreateTables = Server.CreateTables
        Try
            Cr.CreateTextIndex(Convert.ToInt32(DocTypeId), IndexId)
        Finally
            Cr.Dispose()
            Cr = Nothing
        End Try
    End Sub

    Public Shared Function GetDocTypesbyUserRights(ByVal UserId As Int64, ByVal RightType As Zamba.Core.RightsType) As List(Of DocType)
        If Server.isOracle Then
            Dim parValues() As Object = {UserId, CInt(RightType), 2}
            Dim dstemp As DataSet
            ZTrace.WriteLineIf(ZTrace.IsInfo, "ejecutando select GetDocTypesbyUserRights ")
            ZTrace.WriteLineIf(ZTrace.IsInfo, " el user es :  " & UserId)
            ZTrace.WriteLineIf(ZTrace.IsInfo, " el RightType es :  " & CInt(RightType))
            dstemp = Server.Con.ExecuteDataset(CommandType.Text, "Select  distinct dt.* from doc_type dt  inner join doc_type_r_doc_type_group dtrg on dt.doc_type_id = dtrg.doc_type_id inner join doc_type_group dtg on dtg.doc_type_group_id = dtrg.doc_type_group where dt.Doc_type_id in ( Select distinct(aditional) from usr_rights where (groupid = " & UserId & " or groupid in (select inheritedusergroup from group_r_group where usergroup = " & UserId & ") or groupid in (Select groupid from usr_r_group where usrid= " & UserId & ") or groupid in (select inheritedusergroup from group_r_group where usergroup in (Select groupid from usr_r_group where usrid= " & UserId & "))) And (objid=2 and rtype=" & CInt(RightType) & "))  order by doc_type_name")

            Dim DocTypes As New List(Of DocType)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "ejecucion con exito")
            Dim i As Int32
            For i = 0 To dstemp.Tables(0).Rows.Count - 1
                Dim DocType As New DocType(Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("DOC_TYPE_ID")),
                dstemp.Tables(0).Rows(i).Item("DOC_TYPE_NAME").ToString(),
                Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("FILE_FORMAT_ID")),
                Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("DISK_GROUP_ID")),
                Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("THUMBNAILS")),
                Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("ICON_ID")),
                Convert.ToInt32(dstemp.Tables(0).Rows(i).Item("OBJECT_TYPE_ID")),
                dstemp.Tables(0).Rows(i).Item("AUTONAME").ToString(),
                dstemp.Tables(0).Rows(i).Item("AUTONAME").ToString(), 0)
                DocTypes.Add(DocType)
            Next
            Return DocTypes
        Else
            Dim dr As IDataReader
            Dim con As IConnection
            Dim DocTypes As New List(Of DocType)

            Try
                Dim parValues() As Object = {UserId, RightType}
                con = Server.Con

                dr = con.ExecuteReader("zsp_doctypes_400_GetDocTypesByUserRights", parValues)

                While dr.Read
                    Dim DocType As New DocType(Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DOC_TYPE_ID"))),
                    dr.GetValue(dr.GetOrdinal("DOC_TYPE_NAME")).ToString(),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("FILE_FORMAT_ID"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("DISK_GROUP_ID"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("THUMBNAILS"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ICON_ID"))),
                    Convert.ToInt32(dr.GetValue(dr.GetOrdinal("OBJECT_TYPE_ID"))),
                    dr.GetValue(dr.GetOrdinal("AUTONAME")).ToString(),
                    dr.GetValue(dr.GetOrdinal("AUTONAME")).ToString(), 0)
                    DocTypes.Add(DocType)
                End While
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

            Return DocTypes
        End If
    End Function



    ''' <summary>
    ''' Obtiene las propiedades de atributos asociados de la tabla index_r_doc_type 
    ''' </summary>
    ''' <param name="doctypeid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>Diego 18/7/2008 Created
    '''         Marcelo Modified 22/09/2009
    ''' </history>
    Public Shared Function GetIndexsProperties(ByVal doctypeid As Int64) As DataSet
        Dim query As New StringBuilder
        query.Append("SELECT DOC_INDEX.INDEX_ID, DOC_INDEX.INDEX_NAME, ORDEN, MUSTCOMPLETE, ")
        query.Append(" LoadLotus, ShowLotus, Complete, IndexSearch, DefaultValue, IsDataUnique, AutoComplete as Autoincremental, ISREFERENCED ")
        query.Append(" FROM index_r_doc_type ")
        query.Append(" inner join DOC_INDEX ON DOC_INDEX.INDEX_ID = index_r_doc_type.index_id ")
        query.Append(" where doc_type_id = ")
        query.Append(doctypeid)
        query.Append(" ORDER BY index_r_doc_type.ORDEN")
        Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString)


    End Function

    ''' <summary>
    ''' Obtiene las propiedades de atributos asociados de la tabla index_r_doc_type 
    ''' </summary>
    ''' <param name="doctypeid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>Diego 18/7/2008 Created</history>
    Public Shared Function GetIndexsProperties(ByVal doctypeid As Int64, ByVal indexid As Int64) As DataSet
        Dim query As New StringBuilder
        query.Append("SELECT DOC_INDEX.INDEX_ID, DOC_INDEX.INDEX_NAME, ORDEN, MUSTCOMPLETE, ")
        query.Append(" LoadLotus, ShowLotus, Complete, IndexSearch, DefaultValue, IsDataUnique, AutoComplete ")
        query.Append(" FROM index_r_doc_type ")
        query.Append(" inner join DOC_INDEX ON DOC_INDEX.INDEX_ID = index_r_doc_type.index_id ")
        query.Append(" where doc_type_id = ")
        query.Append(doctypeid)
        query.Append(" and doc_index.index_id = ")
        query.Append(indexid)
        Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
    End Function
#Region "Permanent Collections"
#End Region

    Public Shared Function GetDocTypesIdsAndNames(DTIds As String) As DataSet
        Dim sql As String = String.Format("Select doc_type_name,doc_type_id from doc_type where doc_type_id in ({0})", DTIds)
        Return Server.Con.ExecuteDataset(CommandType.Text, sql)
    End Function

    Public Shared Function GetDocTypesIdsAndNames() As DataSet
        Dim parNames() As String = {"io_cursor"}
        ' Dim parTypes() As Object = {5}
        Dim parValues() As Object = {2}
        Dim DSDT As New DataSet
        If Server.isOracle Then
            DSDT = Server.Con.ExecuteDataset("zsp_doctypes_100.GetAllDocTypesIdNames", parValues)
        Else
            DSDT = Server.Con.ExecuteDataset(CommandType.StoredProcedure, "zsp_doctypes_100_GetAllDocTypesIdNames")
        End If
        Return DSDT
    End Function


    Public Shared Function GetAllDocTypes() As DataSet
        'Cargo todos los tipos de documentos
        Dim sql As String = "Select doc_type_name,doc_type_id from doc_type order by doc_type_name"
        Return Server.Con.ExecuteDataset(CommandType.Text, sql)
    End Function



    Public Shared Function GetAllDocType() As DataTable
        Dim Query As String = "SELECT INDEX_R_DOC_TYPE.DOC_TYPE_id as DocTypeId, DOC_TYPE.DOC_TYPE_NAME as DocTypeName , INDEX_R_DOC_TYPE.INDEX_ID as IndexId , DOC_INDEX.INDEX_NAME as IndexName, Doc_Index.Index_Len as  IndexLength, IsReferenced  FROM INDEX_R_DOC_TYPE inner join DOC_TYPE on INDEX_R_DOC_TYPE.DOC_TYPE_id = DOC_TYPE.DOC_TYPE_ID inner join DOC_INDEX ON DOC_INDEX.Index_Id = INDEX_R_DOC_TYPE.INDEX_ID ORDER BY INDEX_R_DOC_TYPE.DOC_TYPE_id"
        Dim tempDs As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Query)

        Query = Nothing

        If Not IsNothing(tempDs) AndAlso tempDs.Tables.Count = 1 Then
            Return tempDs.Tables(0)
        Else
            Return Nothing
        End If

        Return Nothing
    End Function



    ''' <summary>
    ''' Obtiene las propiedades de atributos asociados de la tabla index_r_doc_type 
    ''' </summary>
    ''' <param name="doctypeid"></param>
    ''' <param name="indexid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>Diego 18/7/2008 Created</history>
    Public Shared Function GetIndexRestrictions(ByVal doctypeid As Int64, ByVal indexid As Int64) As DataSet
        Dim query As New StringBuilder
        query.Append("SELECT DOC_TYPE_ID, INDEX_ID, REPLACE(STRING_VALUE,CHR(13) || CHR(10), ' ') STRING_VALUE, RESTRICTION_ID, RESTRICTION_NAME FROM DOC_RESTRICTIONS ")
        query.Append(" WHERE DOC_TYPE_ID = ")
        query.Append(doctypeid)
        query.Append(" AND INDEX_ID = ")
        query.Append(indexid)
        Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
    End Function

    Public Shared Sub ClearHashTables()
        SyncLock (DocTypesByNames)
            DocTypesByNames = New Hashtable
        End SyncLock
    End Sub


End Class
