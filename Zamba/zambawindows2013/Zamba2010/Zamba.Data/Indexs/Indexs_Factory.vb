Imports Zamba.Core
Imports System.Text
Imports System.Collections.Generic
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Core.Indexs_Factory
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Fabrica de Atributos
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Marcelo]	16/10/2007	Created
''' 	[Marcelo]	07/10/2008	Modified
''' </history>
''' -----------------------------------------------------------------------------
Public Class Indexs_Factory

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Dataset tipeado con todos los datos de Doc_index,
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    '''     [Javier]    28/09/2011  Agregado el nombre de la tabla a levantar los datos
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndex() As DataSet
        Dim strselect As String

        strselect = "SELECT "
        strselect &= "  INDEX_ID, INDEX_NAME, INDEX_TYPE, INDEX_LEN, AUTOFILL, NOINDEX, DROPDOWN, AUTODISPLAY, "
        strselect &= "  INVISIBLE, OBJECT_TYPE_ID "

        If Server.isSQLServer Then
            strselect &= ", IsNull(ZHP.IndicePadre, -1) AS IndicePadre, "
            strselect &= " -1 AS IndiceHijo, "
            strselect &= " IsNull(ZHP.DataValuesTable, '') AS DataTableName "
        Else
            strselect &= ", NVL(ZHP.IndicePadre, -1) AS IndicePadre, "
            strselect &= " -1 AS IndiceHijo, "
            strselect &= " NVL(ZHP.DataValuesTable, '') AS DataTableName "
        End If

        strselect &= " FROM DOC_INDEX DI "
        strselect &= " LEFT JOIN ZIndexHierarchyKey ZHP ON DI.INDEX_ID = ZHP.INDICE "

        strselect &= " GROUP BY INDEX_ID, INDEX_NAME, INDEX_TYPE, INDEX_LEN, AUTOFILL, NOINDEX, DROPDOWN, AUTODISPLAY,INVISIBLE, OBJECT_TYPE_ID,"
        strselect &= " ZHP.IndicePadre, ZHP.DataValuesTable"

        strselect &= " ORDER BY INDEX_NAME "

        Dim DSTEMP As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        DSTEMP.Tables(0).TableName = "DOC_INDEX"
        Return DSTEMP
    End Function

    'Javier: Agregado el nombre de la tabla a levantar los datos
    Public Shared Function GetIndexByDropDownValue(ByVal type As IndexAdditionalType)
        Dim strselect As String

        strselect = "SELECT "
        strselect &= "  INDEX_ID, INDEX_NAME, INDEX_TYPE, INDEX_LEN, AUTOFILL, NOINDEX, DROPDOWN, AUTODISPLAY, "
        strselect &= "  INVISIBLE, OBJECT_TYPE_ID "

        If Server.isSQLServer Then
            strselect &= ", IsNull(ZHP.IndicePadre, -1) AS IndicePadre, "
            strselect &= " -1 AS IndiceHijo, "
            strselect &= " IsNull(ZHP.DataValuesTable, '') AS DataTableName "
        Else
            strselect &= ", NVL(ZHP.IndicePadre, -1) AS IndicePadre, "
            strselect &= " -1 AS IndiceHijo, "
            strselect &= " NVL(ZHP.DataValuesTable, '') AS DataTableName "
        End If

        strselect &= " FROM DOC_INDEX DI "
        strselect &= " LEFT JOIN ZIndexHierarchyKey ZHP ON DI.INDEX_ID = ZHP.INDICE "
        strselect &= " LEFT JOIN ZIndexHierarchyKey ZHC ON DI.INDEX_ID = ZHC.INDICEPADRE "
        strselect &= " WHERE DROPDOWN = " & type & " "
        strselect &= " GROUP BY INDEX_ID, INDEX_NAME, INDEX_TYPE, INDEX_LEN, AUTOFILL, NOINDEX, DROPDOWN, AUTODISPLAY,INVISIBLE, OBJECT_TYPE_ID,"
        strselect &= " ZHP.IndicePadre, ZHP.DataValuesTable"
        strselect &= " ORDER BY INDEX_NAME"

        Dim DSIndex1 As New DataSet
        Dim DSTEMP As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)

        DSTEMP.Tables(0).TableName = DSIndex1.Tables(0).TableName
        DSIndex1.Merge(DSTEMP)

        Return DSIndex1
    End Function
    'Javier: Agregado el nombre de la tabla a levantar los datos
    Public Shared Function GetIndexOfAnyDropDownType()
        Dim strselect As String

        strselect = "SELECT "
        strselect &= " INDEX_ID, INDEX_NAME, INDEX_TYPE, INDEX_LEN, AUTOFILL, NOINDEX, DROPDOWN, AUTODISPLAY, "
        strselect &= " INVISIBLE, OBJECT_TYPE_ID "

        If Server.isSQLServer Then
            strselect &= ", IsNull(ZHP.IndicePadre, -1) AS IndicePadre, "
            strselect &= " -1 AS IndiceHijo, "
            strselect &= " IsNull(ZHP.DataValuesTable, '') AS DataTableName "
        Else
            strselect &= ", NVL(ZHP.IndicePadre, -1) AS IndicePadre, "
            strselect &= " -1 AS IndiceHijo, "
            strselect &= " NVL(ZHP.DataValuesTable, '') AS DataTableName "
        End If

        strselect &= " FROM DOC_INDEX DI "
        strselect &= " LEFT JOIN ZIndexHierarchyKey ZHP ON DI.INDEX_ID = ZHP.INDICE "
        strselect &= " WHERE DROPDOWN IN (" & IndexAdditionalType.DropDown & "," & IndexAdditionalType.DropDownJerarquico & ", " & IndexAdditionalType.AutoSustitución & ", " & IndexAdditionalType.AutoSustituciónJerarquico & ") "
        strselect &= " GROUP BY INDEX_ID, INDEX_NAME, INDEX_TYPE, INDEX_LEN, AUTOFILL, NOINDEX, DROPDOWN, AUTODISPLAY,INVISIBLE, OBJECT_TYPE_ID,"
        strselect &= " ZHP.IndicePadre, ZHP.DataValuesTable"
        strselect &= " ORDER BY INDEX_NAME"

        Dim DSTEMP As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)

        If DSTEMP IsNot Nothing AndAlso DSTEMP.Tables.Count > 0 Then
            DSTEMP.Tables(0).TableName = "DOC_INDEX"
        End If
        Return DSTEMP
    End Function
    'Javier: Agregado el nombre de la tabla a levantar los datos
    Public Shared Function GetIndexById(ByVal id As Int64) As DataSet
        Dim strselect As String

        strselect = "SELECT "
        strselect &= "  INDEX_ID, INDEX_NAME, INDEX_TYPE, INDEX_LEN, AUTOFILL, NOINDEX, DROPDOWN, AUTODISPLAY, "
        strselect &= "  INVISIBLE, OBJECT_TYPE_ID "

        If Server.isSQLServer Then
            strselect &= ", IsNull(ZHP.IndicePadre, -1) AS IndicePadre, "
            strselect &= " -1 AS IndiceHijo, "
            strselect &= " IsNull(ZHP.DataValuesTable, '') AS DataTableName "
        Else
            strselect &= ", NVL(ZHP.IndicePadre, -1) AS IndicePadre, "
            strselect &= " -1 AS IndiceHijo, "
            strselect &= " NVL(ZHP.DataValuesTable, '') AS DataTableName "
        End If

        strselect &= " FROM DOC_INDEX DI "
        strselect &= " LEFT JOIN ZIndexHierarchyKey ZHP ON DI.INDEX_ID = ZHP.INDICE "
        strselect &= " WHERE Index_Id = " & id
        strselect &= " GROUP BY INDEX_ID, INDEX_NAME, INDEX_TYPE, INDEX_LEN, AUTOFILL, NOINDEX, DROPDOWN, AUTODISPLAY,INVISIBLE, OBJECT_TYPE_ID,"
        strselect &= " ZHP.IndicePadre, ZHP.DataValuesTable"
        strselect &= " ORDER BY INDEX_NAME"

        Dim DSTEMP As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)

        DSTEMP.Tables(0).TableName = "DOC_INDEX"

        Return DSTEMP
    End Function
    ''' <summary>
    ''' Obtiene los permisos de visualizacion de atributos para los asociados de la tarea
    ''' </summary>
    ''' <param name="DocTypeParentId">Entidad a la cual esta asociada la entidad</param>
    ''' <param name="DoctypeId">doctypeid asociado a la entidad</param>
    ''' <param name="userID">lista de grupos a cual pertenece el usuario</param>
    ''' <returns>visibleIndexs</returns>
    ''' <history>   Pablo    Created     11/12/13</history>
    ''' <remarks></remarks>
    Public Shared Function GetAssociateDocumentIndexsRights(ByVal DocTypeParentId As Int64, ByVal DoctypeId As Int64, ByVal GIDs As List(Of Int64)) As DataSet
        Dim strbuilder As New StringBuilder()
        strbuilder.Append("SELECT distinct indexid FROM zvir WHERE DocTypeParentId = " & DocTypeParentId & " AND DoctypeId = " & DoctypeId & " AND (")

        For Each GroupID As Integer In GIDs
            strbuilder.Append(" GID = " & GroupID & " OR ")
        Next
        strbuilder.Remove(strbuilder.ToString.Length - 4, 4)
        strbuilder.Append(") AND RightType = 132")

        Return Server.Con.ExecuteDataset(CommandType.Text, strbuilder.ToString)
    End Function


    Public Shared Function GetDocIndexByIndexName(ByVal _IndexName As String) As DataSet
        Dim dsTemp As New DataSet
        Dim strSelect As String = "SELECT INDEX_ID, INDEX_TYPE FROM DOC_INDEX WHERE rtrim(ltrim(INDEX_NAME)) = '" & _IndexName & "' ORDER BY INDEX_NAME"
        dsTemp = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        Return dsTemp
    End Function

    Public Shared Function GetIndexNameById(ByVal Indexid As Int64) As String

        Dim sql As String = "SELECT INDEX_NAME FROM DOC_INDEX WHERE INDEX_ID = " + Indexid.ToString
        Return Server.Con.ExecuteScalar(CommandType.Text, sql)
    End Function

    Public Shared Function GetIndexIdByName(ByVal IndexName As String) As Int64
        Dim sql As String = "SELECT INDEX_ID FROM DOC_INDEX WHERE rtrim(ltrim(INDEX_NAME)) = '" + IndexName + "'"
        Return Server.Con.ExecuteScalar(CommandType.Text, sql)
    End Function

    Public Shared Function GetIndexParentID(ByVal IndexId As Integer) As Integer
        Dim sql As String = "SELECT IndicePadre FROM ZIndexHierarchyKey WHERE Indice = " & IndexId
        Return Server.Con.ExecuteScalar(CommandType.Text, sql)
    End Function

    Public Shared Function GetIndexChildID(ByVal IndexId As Integer) As Integer
        Dim sql As String = "SELECT Indice FROM ZIndexHierarchyKey WHERE IndicePadre = " & IndexId
        Return Server.Con.ExecuteScalar(CommandType.Text, sql)
    End Function
    'Javier: Agregado el nombre de la tabla a levantar los datos
    Public Shared Function GetIndexDataTableName(ByVal IndexId As Integer) As String
        Dim sql As String = "SELECT DataValuesTable FROM ZIndexHierarchyKey WHERE IndicePadre = " & IndexId
        Return Server.Con.ExecuteScalar(CommandType.Text, sql)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Dataset con una lista de los valores que aparecen en un item
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	28/09/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetDistinctIndexValues(ByVal docTypeId As Int64, ByVal indexId As Int32, ByVal strRestricc As String, ByVal indexType As Int32) As DataSet
        Dim strSQL As New StringBuilder()

        strSQL.Append("select distinct(")
        If indexType = 4 Then
            strSQL.Append("CONVERT(varchar, I")
            strSQL.Append(indexId.ToString())
            strSQL.Append(",103)")
        Else
            strSQL.Append("I")
            strSQL.Append(indexId.ToString())
        End If

        strSQL.Append(") As ITEM from doc")
        strSQL.Append(docTypeId.ToString())
        strSQL.Append(" where convert(nvarchar,I")
        strSQL.Append(indexId.ToString())
        strSQL.Append(") <> ''")

        If (String.IsNullOrEmpty(strRestricc) = False) Then
            strSQL.Append(" and ")
            strSQL.Append(strRestricc)
        End If
        Return Server.Con.ExecuteDataset(CommandType.Text, strSQL.ToString())
    End Function
    'Javier: Agregado el nombre de la tabla a levantar los datos
    Public Shared Function GetIndexByIdDataSet(ByVal id As Int32) As DataSet
        Dim strselect As String

        strselect = "SELECT "
        strselect &= "  INDEX_ID, INDEX_NAME, INDEX_TYPE, INDEX_LEN, AUTOFILL, NOINDEX, DROPDOWN, AUTODISPLAY, "
        strselect &= "  INVISIBLE, OBJECT_TYPE_ID "

        If Server.isSQLServer Then
            strselect &= ", IsNull(ZHP.IndicePadre, -1) AS IndicePadre, "
            strselect &= " -1 AS IndiceHijo, "
            strselect &= " IsNull(ZHP.DataValuesTable, '') AS DataTableName "
        Else
            strselect &= ", NVL(ZHP.IndicePadre, -1) AS IndicePadre, "
            strselect &= " -1 AS IndiceHijo, "
            strselect &= " NVL(ZHP.DataValuesTable, '') AS DataTableName "
        End If

        strselect &= " FROM DOC_INDEX DI "
        strselect &= " LEFT JOIN ZIndexHierarchyKey ZHP ON DI.INDEX_ID = ZHP.INDICE "
        strselect &= " LEFT JOIN ZIndexHierarchyKey ZHC ON DI.INDEX_ID = ZHC.INDICEPADRE "
        strselect &= " WHERE DI.Index_Id = " & id & ""

        strselect &= " GROUP BY INDEX_ID, INDEX_NAME, INDEX_TYPE, INDEX_LEN, AUTOFILL, NOINDEX, DROPDOWN, AUTODISPLAY,INVISIBLE, OBJECT_TYPE_ID,"
        strselect &= " ZHP.IndicePadre, ZHP.DataValuesTable"

        strselect &= " ORDER BY INDEX_NAME"

        Return Server.Con.ExecuteDataset(CommandType.Text, strselect)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Dataset tipeado con los datos de Index_R_Doc_type para un Doc_Type especifico
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad</param>
    ''' <returns>DsIndex</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    '''     [Javier]    28/09/2011  Agregado el nombre de la tabla a levantar los datos
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexSchema(ByVal docTypeId As Int64) As DataSet
        Dim strSelect As New StringBuilder()
        Dim Dstemp As New DataSet

        Try
            'strSelect.Append("SELECT Doc_Index.Index_Id, Doc_Index.Index_Name, Doc_Index.Index_Type, Doc_Index.Index_Len, Doc_Index.DropDown, IsReferenced FROM Doc_Index, Index_R_Doc_Type WHERE Doc_Index.Index_Id = Index_R_Doc_Type.Index_Id AND ")
            'strSelect.Append("Index_R_Doc_Type.Doc_Type_Id = " & DocTypeId)
            'strSelect.Append(" ORDER BY Index_R_Doc_Type.Orden")

            strSelect.Append("SELECT ")
            strSelect.Append("  DI.Index_Id, DI.Index_Name, DI.Index_Type, DI.Index_Len, DI.DropDown, IsReferenced, ")

            If Server.isSQLServer Then
                strSelect.Append(" IsNull(ZHP.IndicePadre, -1) AS IndicePadre, ")
                strSelect.Append(" -1 AS IndiceHijo, ")
                strSelect.Append(" IsNull(ZHP.DataValuesTable, '') AS DataTableName ")
            Else
                strSelect.Append(" NVL(ZHP.IndicePadre, -1) AS IndicePadre, ")
                strSelect.Append(" -1 AS IndiceHijo, ")
                strSelect.Append(" NVL(ZHP.DataValuesTable, '') AS DataTableName ")
            End If

            strSelect.Append(" FROM DOC_INDEX DI ")
            strSelect.Append(" LEFT JOIN ZIndexHierarchyKey ZHP ON DI.INDEX_ID = ZHP.INDICE ")
            strSelect.Append(" LEFT JOIN ZIndexHierarchyKey ZHC ON DI.INDEX_ID = ZHC.INDICEPADRE ")
            strSelect.Append(" INNER JOIN Index_R_Doc_Type IRDOC ON DI.Index_Id = IRDOC.Index_Id ")
            strSelect.Append(" WHERE IRDOC.Doc_Type_Id = " & docTypeId.ToString)

            strSelect.Append(" GROUP BY DI.Index_Id, DI.Index_Name, DI.Index_Type, DI.Index_Len, DI.DropDown, IsReferenced, IRDOC.Orden,")
            strSelect.Append(" ZHP.IndicePadre, ZHP.DataValuesTable")

            strSelect.Append(" ORDER BY IRDOC.Orden")

            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, strSelect.ToString)

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            strSelect.Remove(0, strSelect.Length)
            strSelect = Nothing

            Dstemp.Dispose()
            Dstemp = Nothing
        End Try

        Return Dstemp
    End Function
    'Javier: Agregado el nombre de la tabla a levantar los datos
    Public Shared Function GetIndexSchemaAsDataSet(ByVal docTypeId As Int64) As DataSet
        Dim strSelect As New StringBuilder
        Dim Dstemp As DataSet = Nothing

        Try
            'strSelect.Append("SELECT Doc_Index.Index_Id, Doc_Index.Index_Name, Doc_Index.Index_Type, Doc_Index.Index_Len, Doc_Index.DropDown, IsReferenced, Index_R_Doc_Type.Orden  FROM Doc_Index, Index_R_Doc_Type WHERE Doc_Index.Index_Id = Index_R_Doc_Type.Index_Id AND ")
            'strSelect.Append("Index_R_Doc_Type.Doc_Type_Id = ")
            'strSelect.Append(DocTypeId.ToString())
            'strSelect.Append(" ORDER BY Index_R_Doc_Type.Orden")

            strSelect.Append("SELECT DI.Index_Id, DI.Index_Name, DI.Index_Type, DI.Index_Len, DI.DropDown, IsReferenced, IRDOC.Orden, ")

            If Server.isSQLServer Then
                strSelect.Append(" IsNull(ZHP.IndicePadre, -1) AS IndicePadre, ")
                strSelect.Append(" -1 AS IndiceHijo, ")
                strSelect.Append(" IsNull(ZHP.DataValuesTable, '') AS DataTableName ")
            Else
                strSelect.Append(" NVL(ZHP.IndicePadre, -1) AS IndicePadre, ")
                strSelect.Append(" -1 AS IndiceHijo, ")
                strSelect.Append(" NVL(ZHP.DataValuesTable, '') AS DataTableName ")
            End If

            strSelect.Append(" FROM DOC_INDEX DI ")
            strSelect.Append(" LEFT JOIN ZIndexHierarchyKey ZHP ON DI.INDEX_ID = ZHP.INDICE ")
            strSelect.Append(" LEFT JOIN ZIndexHierarchyKey ZHC ON DI.INDEX_ID = ZHC.INDICEPADRE ")
            strSelect.Append(" INNER JOIN Index_R_Doc_Type IRDOC ON DI.Index_Id = IRDOC.Index_Id ")
            strSelect.Append(" WHERE IRDOC.Doc_Type_Id  =" & docTypeId.ToString())
            strSelect.Append(" GROUP BY DI.Index_Id, DI.Index_Name, DI.Index_Type, DI.Index_Len, DI.DropDown, IsReferenced, IRDOC.Orden,")
            strSelect.Append(" ZHP.IndicePadre, ZHP.DataValuesTable")
            strSelect.Append(" ORDER BY IRDOC.Orden")

            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, strSelect.ToString)

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            strSelect.Remove(0, strSelect.Length)
            strSelect = Nothing
        End Try

        Return Dstemp
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Vector de objetos Index en base a un Doc_type_id
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad</param>
    ''' <returns>Coleccion de objetos Index</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Marcelo]	16/10/2007	Modified
    '''     [Javier]    28/09/2011  Agregado el nombre de la tabla a levantar los datos
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexsSchema(ByVal docTypeId As Int64) As DataSet
        Dim strSelect As New StringBuilder()
        Dim Dstemp As DataSet = Nothing

        Try
            'strSelect.Append("SELECT Doc_Index.Index_Id, Doc_Index.Index_Name, Doc_Index.Index_Type, Doc_Index.Index_Len, Doc_Index.DropDown,mustcomplete, IsReferenced  FROM Doc_Index, Index_R_Doc_Type WHERE Doc_Index.Index_Id = Index_R_Doc_Type.Index_Id AND ")
            'strSelect.Append("Index_R_Doc_Type.Doc_Type_Id = ")
            'strSelect.Append(DocTypeId.ToString())
            'strSelect.Append(" ORDER BY Index_R_Doc_Type.Orden")

            strSelect.Append("SELECT DI.Index_Id, DI.Index_Name, DI.Index_Type, DI.Index_Len, DI.DropDown,mustcomplete, IsReferenced, ")

            If Server.isSQLServer Then
                strSelect.Append(" IsNull(ZHP.IndicePadre, -1) AS IndicePadre, ")
                strSelect.Append(" -1 AS IndiceHijo, ")
                strSelect.Append(" IsNull(ZHP.DataValuesTable, '') AS DataTableName ")
            Else
                strSelect.Append(" NVL(ZHP.IndicePadre, -1) AS IndicePadre, ")
                strSelect.Append(" -1 AS IndiceHijo, ")
                strSelect.Append(" NVL(ZHP.DataValuesTable, '') AS DataTableName ")
            End If

            strSelect.Append(" FROM DOC_INDEX DI ")
            strSelect.Append(" LEFT JOIN ZIndexHierarchyKey ZHP ON DI.INDEX_ID = ZHP.INDICE ")

            strSelect.Append(" INNER JOIN Index_R_Doc_Type IRDOC ON DI.Index_Id = IRDOC.Index_Id ")
            strSelect.Append(" WHERE IRDOC.Doc_Type_Id = " & docTypeId.ToString() & " ")
            strSelect.Append(" GROUP BY DI.Index_Id, DI.Index_Name, DI.Index_Type, DI.Index_Len, DI.DropDown, mustcomplete, IsReferenced, IRDOC.Orden,")
            strSelect.Append(" ZHP.IndicePadre, ZHP.DataValuesTable")

            strSelect.Append(" ORDER BY IRDOC.Orden")

            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, strSelect.ToString)

            Return Dstemp
        Finally
            Dstemp.Dispose()
            Dstemp = Nothing
            strSelect = Nothing
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Dataset tipeado con los atributos en comun entre varios DOC_TYPEs
    ''' </summary>
    ''' <param name="DocTypeIds"></param>
    ''' <returns>DsIndex</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	28/06/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexSchema(ByVal DocTypeIds As ArrayList) As DataSet
        Dim DsIndex As DataSet = Nothing
        Dim i As Integer
        Dim WhereString As New StringBuilder
        Dim strSelect As New StringBuilder
        Dim Dstemp As DataSet = Nothing

        Try
            For i = 0 To DocTypeIds.Count - 1
                If i = 0 Then
                    WhereString.Append("(Index_R_Doc_Type.Doc_Type_Id = " & DocTypeIds(i) & ")")
                Else
                    WhereString.Append(" or (Index_R_Doc_Type.Doc_Type_Id = " & DocTypeIds(i) & ")")
                End If
            Next

            If DocTypeIds.Count > 1 Then
                'TODO Falta cambiar por store procedure ya creados previamente para cada docid o dinamicos
                strSelect.Append("SELECT Doc_Index.Index_Id, Doc_Index.Index_Name, Doc_Index.Index_Type, Doc_Index.Index_Len, Doc_Index.DropDown, COUNT(Doc_Index.Index_Name) AS Expr2 ")
                strSelect.Append("FROM Doc_Index,Index_R_Doc_Type WHERE Doc_Index.Index_Id = Index_R_Doc_Type.Index_Id ")
                strSelect.Append(" and (")
                strSelect.Append(WhereString.ToString)
                strSelect.Append(") GROUP BY Doc_Index.Index_Id, Doc_Index.Index_Name, ")
                strSelect.Append("Doc_Index.Index_Type, Doc_Index.Index_Len, Doc_Index.DropDown")
                strSelect.Append(" HAVING (COUNT(Doc_Index.Index_Name) = ")
                strSelect.Append(DocTypeIds.Count)
                strSelect.Append(")")
                'TODO Falta ver si se le puede pasar un array dinamico de parametros para una sola columna
            Else
                strSelect.Append("SELECT Doc_Index.Index_Id, Doc_Index.Index_Name, Doc_Index.Index_Type, Doc_Index.Index_Len, Doc_Index.DropDown, IsReferenced  FROM Doc_Index, Index_R_Doc_Type WHERE Doc_Index.Index_Id = Index_R_Doc_Type.Index_Id AND ")
                strSelect.Append(WhereString)
                strSelect.Append(" ORDER BY Index_R_Doc_Type.Orden")
            End If

            Dstemp = Server.Con.ExecuteDataset(CommandType.Text, strSelect.ToString)
            Dstemp.Tables(0).TableName = DsIndex.Tables(0).TableName
            DsIndex.Merge(Dstemp)


        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            strSelect.Remove(0, strSelect.Length)
            strSelect = Nothing
            WhereString = Nothing
            Dstemp.Dispose()
            Dstemp = Nothing
        End Try

        Return DsIndex
    End Function


    'Public Shared Function GetIndexs(ByVal docTypeId As Int64) As DataTable
    '    Dim DtIndexs As DataTable = Nothing

    '    Dim QueryBuilder As New StringBuilder

    '    QueryBuilder.Append("SELECT DOC_INDEX.INDEX_ID, DOC_INDEX.INDEX_NAME, DOC_INDEX.INDEX_TYPE, DOC_INDEX.INDEX_LEN, ")
    '    QueryBuilder.Append("DOC_INDEX.AUTOFILL, DOC_INDEX.NOINDEX, DOC_INDEX.DROPDOWN, DOC_INDEX.AUTODISPLAY, ")
    '    QueryBuilder.Append("DOC_INDEX.INVISIBLE, DOC_INDEX.OBJECT_TYPE_ID, INDEX_R_DOC_TYPE.ORDEN, ")
    '    QueryBuilder.Append("INDEX_R_DOC_TYPE.MUSTCOMPLETE, IsReferenced  FROM Index_R_Doc_Type INNER JOIN doc_index ")
    '    QueryBuilder.Append("on doc_index.INDEX_ID = index_r_doc_type.index_id WHERE doc_type_id = ")
    '    QueryBuilder.Append(docTypeId.ToString())
    '    QueryBuilder.Append(" ORDER BY INDEX_R_DOC_TYPE.orden")

    '    Dim DsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())

    '    If Not IsNothing(DsTemp) AndAlso DsTemp.Tables.Count = 1 Then
    '        DtIndexs = DsTemp.Tables(0)
    '    End If

    '    QueryBuilder.Remove(0, QueryBuilder.Length)
    '    QueryBuilder = Nothing

    '    DsTemp.Dispose()
    '    DsTemp = Nothing

    '    Return DtIndexs
    'End Function

    Public Shared Function GetIndexValues(ByVal taskId As Int64, ByVal docTypeId As Int64, ByVal indexIds As List(Of Int64)) As Dictionary(Of Int64, String)

        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("SELECT DOC_ID,")
        For Each CurrentIndexId As Int64 In indexIds
            QueryBuilder.Append("I")
            QueryBuilder.Append(CurrentIndexId.ToString())
            QueryBuilder.Append(",")
        Next

        QueryBuilder.Remove(QueryBuilder.Length - 1, 1)

        QueryBuilder.Append(" FROM Doc")
        QueryBuilder.Append(docTypeId.ToString())
        QueryBuilder.Append(" where doc_id = ")
        QueryBuilder.Append(taskId.ToString())

        Dim DsIndexValues As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing

        Dim IndexValues As New Dictionary(Of Int64, String)

        If Not IsNothing(DsIndexValues) AndAlso DsIndexValues.Tables.Count = 1 AndAlso DsIndexValues.Tables(0).Rows.Count = 1 Then
            Dim CurrentRow As DataRow = DsIndexValues.Tables(0).Rows(0)

            For Each CurrentIndexId As Int64 In indexIds
                If Not IsNothing(CurrentRow("I" + CurrentIndexId.ToString())) Then
                    IndexValues.Add(CurrentIndexId, CurrentRow("I" + CurrentIndexId.ToString()).ToString())
                End If

            Next

            CurrentRow = Nothing
        End If

        DsIndexValues.Dispose()

        Return IndexValues
    End Function

    Public Shared Function GetIndexValues(ByVal taskId As Int64, ByVal docTypeId As Int64, ByVal indexs As List(Of IIndex)) As Dictionary(Of Int64, String)

        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("SELECT DOC_ID,")
        For Each CurrentIndex As IIndex In indexs
            QueryBuilder.Append("I")
            QueryBuilder.Append(CurrentIndex.ID.ToString())
            QueryBuilder.Append(",")
        Next

        QueryBuilder.Remove(QueryBuilder.Length - 1, 1)

        QueryBuilder.Append(" FROM Doc")
        QueryBuilder.Append(docTypeId.ToString())
        QueryBuilder.Append(" where doc_id = ")
        QueryBuilder.Append(taskId.ToString())

        Dim DsIndexValues As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing

        Dim IndexValues As New Dictionary(Of Int64, String)

        If Not IsNothing(DsIndexValues) AndAlso DsIndexValues.Tables.Count = 1 AndAlso DsIndexValues.Tables(0).Rows.Count = 1 Then
            Dim CurrentRow As DataRow = DsIndexValues.Tables(0).Rows(0)

            For Each CurrentIndex As IIndex In indexs
                If Not IsNothing(CurrentRow("I" + CurrentIndex.ID.ToString())) Then
                    IndexValues.Add(CurrentIndex.ID, CurrentRow("I" + CurrentIndex.ID.ToString()).ToString())
                End If

            Next

            CurrentRow = Nothing
        End If

        DsIndexValues.Dispose()

        Return IndexValues
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea un indice.
    ''' </summary>
    ''' <param name="Index">Objeto indice que se va a guardar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Marcelo]	16/10/2007	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function AddIndex(ByVal id As Int32, ByVal Name As String, ByVal Type As String,
    ByVal Len As Int32, ByVal AutoFill1 As Int32, ByVal NoIndex1 As Int32,
    ByVal DropDown As String, ByVal AutoDisplay1 As Int32, ByVal Invisible1 As Int32,
    ByVal Object_Type_Id As Int32) As Integer
        Dim sql As New StringBuilder
        Try
            sql.Append("INSERT INTO Doc_Index (Index_Id,Index_Name,Index_Type,Index_Len,Autofill,NoIndex,DropDown, Autodisplay, Invisible, Object_Type_Id) Values (")
            sql.Append(id.ToString())
            sql.Append(",'")
            sql.Append(Name)
            sql.Append("',")
            sql.Append(Type)
            sql.Append(",")
            sql.Append(Len.ToString())
            sql.Append(",")
            sql.Append(AutoFill1.ToString())
            sql.Append(",")
            sql.Append(NoIndex1.ToString())
            sql.Append(",")
            sql.Append(DropDown)
            sql.Append(",")
            sql.Append(AutoDisplay1.ToString())
            sql.Append(",")
            sql.Append(Invisible1.ToString())
            sql.Append(",")
            sql.Append(Object_Type_Id.ToString())
            sql.Append(")")
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            sql.Append("SELECT Index_Id FROM ")
            sql.Append("Doc_Index")
            sql.Append(" where (Index_name = '")
            sql.Append(Name.Trim)
            sql.Append("')")
            Dim IndexId As Integer = Server.Con.ExecuteScalar(CommandType.Text, sql.ToString)

            Return IndexId
        Finally
            sql.Remove(0, sql.Length)
            sql = Nothing
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina un Atributo de Zamba
    ''' </summary>
    ''' <param name="Index">Objeto Atributo que se desea eliminar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DelIndex(ByVal IndexId As Int64)
        Dim StrDelete As String = "DELETE from Doc_Index where (Index_id = " & IndexId.ToString & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, StrDelete)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve los DT que poseen el atributo
    ''' </summary>
    ''' <param name="Index">Objeto Atributo que se desea eliminar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetDocTypesFromIndexRDocType(ByVal IndexId As Int64) As DataSet
        Dim StrQuery As String = "SELECT doc_type_id from Index_R_Doc_Type where (Index_id = " & IndexId.ToString & ")"
        Return Server.Con.ExecuteDataset(CommandType.Text, StrQuery)
    End Function

#Region "Attribute updates: functionality"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza longitud de un Atributo de Zamba
    ''' </summary>
    ''' <param name="Index">Objeto Atributo que se desea eliminar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdateIndex(ByVal IndexId As Int64, ByVal IndexLength As String, ByVal IndexType As String)
        Dim StrQuery As String = Nothing

        If Not IsNothing(IndexType) Then
            StrQuery = "UPDATE Doc_Index SET  index_type = " + IndexType + ", index_len = " + IndexLength + " where (Index_id = " & IndexId.ToString & ")"
        Else
            StrQuery = "UPDATE Doc_Index SET index_len = " + IndexLength + " where (Index_id = " & IndexId.ToString & ")"
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, StrQuery)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza el atributo en las Doc_i y Doc
    ''' </summary>
    ''' <param name="Index">Objeto Atributo que se desea eliminar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[pablo]	26/05/2012	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdateIndexLengthByDocTypeID(ByVal DocTypeId As Int64, ByVal Indx As Index, ByVal Length As Int64)
        Dim StrQuery As String
        Select Case Indx.Type
            Case IndexDataType.Alfanumerico
                If Server.isOracle Then
                    StrQuery = "ALTER TABLE Doc_i" & DocTypeId & " MODIFY  i" & Indx.ID & " varchar(" & Length & ")"
                Else
                    StrQuery = "ALTER TABLE Doc_i" & DocTypeId & " ALTER COLUMN i" & Indx.ID & " varchar(" & Length & ")"
                End If
            Case IndexDataType.Alfanumerico_Largo
                If Server.isOracle Then
                    StrQuery = "ALTER TABLE Doc_i" & DocTypeId & " MODIFY  i" & Indx.ID & " varchar(" & Length & ")"
                Else
                    StrQuery = "ALTER TABLE Doc_i" & DocTypeId & " ALTER COLUMN i" & Indx.ID & " varchar(" & Length & ")"
                End If
        End Select
        If Not String.IsNullOrEmpty(StrQuery) Then
            Server.Con.ExecuteNonQuery(CommandType.Text, StrQuery)
        End If
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza el atributo en las Doc_i y Doc
    ''' </summary>
    ''' <param name="Index">Objeto Atributo que se desea eliminar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub TruncateIndexsLengthByDocTypeID(ByVal DocID As Int64, ByVal DocTypeID As Int64, ByVal IndexID As Int64, ByVal Length As Int32, ByVal IndexType As Int16)
        Dim parValues() As Object = {DocID, DocTypeID, IndexID, Length, IndexType}
        If Server.isOracle Then

            'TODO: Realizar el procedimiento zsp_100_Index_UpdateIndexLength para ORACLE
            'Server.Con.ExecuteNonQuery("zsp_100_Index_UpdateIndexLength", parValues)
        Else
            Try
                Server.Con.ExecuteNonQuery("zsp_100_Index_UpdateIndexLength", parValues)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' actualiza al tipo de dato solicitado
    ''' </summary>
    ''' <param name="DocTypeID">ID tipo de documento</param>
    ''' <param name="IndexID">Id de atributo</param>
    ''' <param name="IndexType">tipo de atributo</param>
    ''' <param name="IndexLength">tipo de atributo</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Pablo]	26/03/2012	Created
    ''' </history>
    Public Shared Function UpdateIndexType(ByVal DocTypeID As Int64, ByVal IndexID As String, ByVal IndexType As String, ByVal IndexLength As String) As Hashtable
        Dim StrQuery As String = Nothing
        Dim DiscardChanges As Boolean = False

        Select Case IndexType
            Case IndexDataType.Alfanumerico
                StrQuery = "ALTER TABLE doc_i" + DocTypeID.ToString() + " ADD tmpColumn VARCHAR (" + IndexLength + ")"
            Case IndexDataType.Alfanumerico_Largo
                StrQuery = "ALTER TABLE doc_i" + DocTypeID.ToString() + " ADD tmpColumn VARCHAR (" + IndexLength + ")"
            Case IndexDataType.Fecha
                StrQuery = "ALTER TABLE doc_i" + DocTypeID.ToString() + " ADD tmpColumn DATETIME"
            Case IndexDataType.Fecha_Hora
                StrQuery = "ALTER TABLE doc_i" + DocTypeID.ToString() + " ADD tmpColumn DATETIME"
            Case IndexDataType.Moneda
                StrQuery = "ALTER TABLE doc_i" + DocTypeID.ToString() + " ADD tmpColumn DECIMAL (18,2)"
            Case IndexDataType.Numerico
                StrQuery = "ALTER TABLE doc_i" + DocTypeID.ToString() + " ADD tmpColumn NUMERIC (7,0)"
            Case IndexDataType.Numerico_Decimales
                StrQuery = "ALTER TABLE doc_i" + DocTypeID.ToString() + " ADD tmpColumn DECIMAL (18,2)"
            Case IndexDataType.Numerico_Largo
                StrQuery = "ALTER TABLE doc_i" + DocTypeID.ToString() + " ADD tmpColumn NUMERIC (18,0)"
        End Select

        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, StrQuery)
        Catch
            DiscardChanges = True
        End Try

        Dim VUnableToCast As New Hashtable
        If DiscardChanges = False Then
            Dim dsDTID As DataTable = Results_Factory.GetResultsFromDoc_I(DocTypeID)


            For Each row As DataRow In dsDTID.Rows

                Select Case IndexType
                    Case "1"
                        If ValidateNumericIndex(VUnableToCast, row.Item(3).ToString(), row.Item("i" + IndexID).ToString()) Then
                            Exec_Index_UpdateIndexType(row.Item(3), DocTypeID, Int64.Parse(IndexID), Int64.Parse(IndexType), 7)
                        End If
                    Case "2"
                        If ValidateNumericIndex(VUnableToCast, row.Item(3).ToString(), row.Item("i" + IndexID).ToString()) Then
                            Exec_Index_UpdateIndexType(row.Item(3), DocTypeID, Int64.Parse(IndexID), Int64.Parse(IndexType), 18)
                        End If
                    Case "3"
                        If ValidateNumericIndex(VUnableToCast, row.Item(3).ToString(), row.Item("i" + IndexID).ToString()) Then
                            Exec_Index_UpdateIndexType(row.Item(3), DocTypeID, Int64.Parse(IndexID), Int64.Parse(IndexType), 18)
                        End If
                    Case "4"
                        If ValidateDateIndex(VUnableToCast, row.Item(3).ToString(), row.Item("i" + IndexID).ToString()) Then
                            Exec_Index_UpdateIndexType(row.Item(3), DocTypeID, Int64.Parse(IndexID), Int64.Parse(IndexType), 0)
                        End If
                    Case "5"
                        If ValidateDateIndex(VUnableToCast, row.Item(3).ToString(), row.Item("i" + IndexID).ToString()) Then
                            Exec_Index_UpdateIndexType(row.Item(3), DocTypeID, Int64.Parse(IndexID), Int64.Parse(IndexType), 0)
                        End If
                    Case "6"
                        If ValidateNumericIndex(VUnableToCast, row.Item(3).ToString(), row.Item("i" + IndexID).ToString()) Then
                            Exec_Index_UpdateIndexType(row.Item(3), DocTypeID, Int64.Parse(IndexID), Int64.Parse(IndexType), 18)
                        End If
                    Case "7"
                        Exec_Index_UpdateIndexType(row.Item(3), DocTypeID, Int64.Parse(IndexID), Int64.Parse(IndexType), IndexLength)
                    Case "8"
                        Exec_Index_UpdateIndexType(row.Item(3), DocTypeID, Int64.Parse(IndexID), Int64.Parse(IndexType), IndexLength)
                End Select
            Next

        Else
            'En caso de que falle el ALTER TABLE
            Return Nothing
        End If
        Return VUnableToCast
    End Function

    ''' <summary>
    ''' Verifica que el tipo de dato pasado como parametro sea de tipo numerico
    ''' </summary>
    ''' <param name="VUnableToCast">ID tipo de documento</param>
    ''' <param name="DocID">ID de documento</param>
    ''' <param name="IndexValue">valor del atributo a verificar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Pablo]	26/03/2012	Created
    ''' </history>
    Public Shared Function ValidateNumericIndex(ByRef VUnableToCast As Hashtable, ByVal DocID As String, ByVal IndexValue As String) As Boolean
        If Not IsDBNull(IndexValue) And String.Compare(IndexValue, "") Then
            If Not IsNumeric(IndexValue) Then
                VUnableToCast.Add(DocID, IndexValue)
                Return False
            Else
                Return True
            End If
        End If
    End Function

    ''' <summary>
    ''' Verifica que el tipo de dato pasado como parametro sea de tipo fecha
    ''' </summary>
    ''' <param name="VUnableToCast">ID tipo de documento</param>
    ''' <param name="DocID">ID de documento</param>
    ''' <param name="IndexValue">valor del atributo a verificar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Pablo]	26/03/2012	Created
    ''' </history>
    Public Shared Function ValidateDateIndex(ByRef VUnableToCast As Hashtable, ByVal DocID As String, ByVal IndexValue As String) As Boolean
        If Not IsDBNull(IndexValue) And String.Compare(IndexValue, "") Then
            If Not IsDate(IndexValue) Then
                VUnableToCast.Add(DocID, IndexValue)
                Return False
            Else
                Return True
            End If
        End If
    End Function

    ''' <summary>
    ''' Ejecuta el  stored procedure para convertir al tipo de dato solicitado
    ''' </summary>
    ''' <param name="docID">ID documento</param>
    ''' <param name="DocTypeID">ID tipo de documento</param>
    ''' <param name="indexId">ID de Indice</param>
    ''' <param name="IndexType">tipo de Indice</param>
    ''' <param name="IndexLength">longitud de Indice</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Pablo]	26/03/2012	Created
    ''' </history>

    Public Shared Sub ExecIndexAlterSlst(ByVal indexId As Int64, ByVal indexType As String, ByVal indexLength As String)

        Dim type As String = Nothing
        Select Case indexType
            Case IndexDataType.Alfanumerico
                type = " VARCHAR(" + indexLength + ") "
            Case IndexDataType.Alfanumerico_Largo
                type = " VARCHAR(" + indexLength + ") "
            Case IndexDataType.Fecha
                type = " DATETIME "
            Case IndexDataType.Fecha_Hora
                type = " DATETIME "
            Case IndexDataType.Moneda
                type = " DECIMAL(18,2) "
            Case IndexDataType.Numerico
                type = " NUMERIC(7,0) "
            Case IndexDataType.Numerico_Decimales
                type = " DECIMAL(18,2) "
            Case IndexDataType.Numerico_Largo
                type = " NUMERIC(18,0) "
        End Select

        Dim parValues() As Object = {Int64.Parse(indexId), type}

        Try
            Server.Con.ExecuteNonQuery("zsp_100_Index_ModifyColumnCodigoSlst", parValues)
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecuto store al dato solicitado")
        End Try



    End Sub

    Public Shared Sub Exec_Index_UpdateIndexType(ByVal docID As Int64, ByVal DocTypeID As Int64, ByVal IndexID As Int64, ByVal IndexType As Int64, ByVal IndexLength As Int64)
        Dim parValues() As Object = {docID, DocTypeID, Int64.Parse(IndexID), Int64.Parse(IndexType), Int64.Parse(IndexLength)}
        If Server.isOracle Then
            'TODO: Realizar el procedimiento zsp_100_Index_UpdateIndexLength para ORACLE
            'Server.Con.ExecuteNonQuery("zsp_100_Index_UpdateIndexLength", parValues)
        Else
            Try
                Server.Con.ExecuteNonQuery("zsp_100_index_UpdateIndexType", parValues)
            Catch ex As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizo tipo de indice")
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Aplica los cambios de conversion de tipo de dato a un atributo
    ''' </summary>
    ''' <param name="DocTypeID">ID tipo de documento</param>
    ''' <param name="IndexID">ID de Indice</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Pablo]	26/03/2012	Created
    ''' </history>
    Public Shared Sub ConfirmUpdateIndexType(ByVal DocTypeID As String, ByVal IndexID As String)
        Try
            Dim StrQuery As String = Nothing
            StrQuery = "ALTER TABLE doc_i" + DocTypeID + " DROP COLUMN i" + IndexID
            Server.Con.ExecuteNonQuery(CommandType.Text, StrQuery)

            StrQuery = Nothing
            StrQuery = "EXEC sp_rename @objname = 'doc_i" + DocTypeID.ToString() + ".tmpColumn' , @newname = 'i" + IndexID + "', @objtype = 'COLUMN'"
            Server.Con.ExecuteNonQuery(CommandType.Text, StrQuery)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Descarta los cambios de conversion de tipo de dato a un atributo
    ''' </summary>
    ''' <param name="DocTypeID"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Pablo]	26/03/2012	Created
    ''' </history>
    Public Shared Sub DiscardUpdateIndexTypeChanges(ByVal DocTypeID As String)
        Dim StrQuery As String = Nothing
        StrQuery = "ALTER TABLE doc_i" + DocTypeID + " DROP COLUMN tmpColumn"
        Server.Con.ExecuteNonQuery(CommandType.Text, StrQuery)
    End Sub
#End Region
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza un indice en Zamba
    ''' </summary>
    ''' <param name="Index">Atributo que se desea guardar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function UpdateIndex(ByVal IndexId As Int64, ByVal Name As String, ByVal AutoDisplay1 As Int32, ByVal NoIndex1 As Int32,
    ByVal DropDown As String, ByVal Invisible1 As Int32) As Int32
        'PACKAGE UPDATE_DOC_INDEX_pkg AS
        'PROCEDURE Update_DocIndex
        Try
            'MAXI 11/11/05
            'Dim sql As String = "Select count(1) from Doc_index where Index_name='" & Index.Name & "' and Index_id <>" & Index.Id
            'Dim cant As Int16 = Server.Con.ExecuteScalar(CommandType.Text, sql)

            Dim cant As Int16
            If Server.isOracle Then
                Dim sql As String = "Select count(1) from Doc_index where Index_name='" & Name & "' and Index_id <>" & IndexId.ToString()
                cant = Server.Con.ExecuteScalar(CommandType.Text, sql)
            Else
                Dim parValues() As Object = {Name, IndexId}
                Try
                    'cant = Server.Con.ExecuteScalar("ZDIndGetCantByNameId", parvalues)
                    cant = Server.Con.ExecuteScalar("zsp_index_100_GetIndexQtyByNameId", parValues)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If

            If cant <= 0 Then
                Dim StrUpdate As String = "UPDATE Doc_Index SET Index_Name = '" & Name & "',AutoFill = " & AutoDisplay1.ToString() & ",NoIndex = " & NoIndex1.ToString() _
                & ",DropDown = " & DropDown & ", AutoDisplay = " & AutoDisplay1.ToString() & ", Invisible = " & Invisible1.ToString() & " where (Index_Id = " & IndexId.ToString() & ")"
                Server.Con.ExecuteNonQuery(CommandType.Text, StrUpdate)
            End If
            Return cant
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return 0
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica si existe un indice con el mismo nombre que se le pasa por parametro
    ''' </summary>
    ''' <param name="IndexName">Nombre del indice que se desea verificar</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function IndexIsDuplicated(ByVal IndexName As String) As Boolean
        Dim table As String = "Doc_Index"
        Dim value As Boolean
        Try
            Dim strSelect As String = "SELECT COUNT(Index_id) from " & table & " where (Index_Name = '" & Trim(IndexName) & "')"
            Dim qrows As Int32 = Server.Con.ExecuteScalar(CommandType.Text, strSelect)
            If qrows > 0 Then
                value = True
            Else
                value = False
            End If
        Catch ex As Exception
            Throw New Exception("Ocurrio un error al consultar la duplicidad del Atributo" & " " & ex.ToString)
        End Try

        Return value
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza el valor de un indice de la lista de seleccion
    ''' </summary>
    ''' <param name="IndexId">ID del Atributo que se desea modificar</param>
    ''' <param name="Item">Valor que se desea modificar</param>
    ''' <param name="NewItem">Nuevo valor que se desea guardar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdateListItem(ByVal IndexId As Integer, ByVal Item As String, ByVal NewItem As String)
        Dim QueryBuilder As New StringBuilder

        Try
            QueryBuilder.Append("UPDATE ILst_I")
            QueryBuilder.Append(IndexId)
            QueryBuilder.Append(" set ITEM = '")
            QueryBuilder.Append(NewItem)
            QueryBuilder.Append("' where (Item = '")
            QueryBuilder.Append(Item)
            QueryBuilder.Append("')")
            Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            QueryBuilder.Remove(0, QueryBuilder.Length)
            QueryBuilder = Nothing
        End Try

    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene el Nombre del Atributo en base a su ID
    ''' </summary>
    ''' <param name="IndexId">Id del indice que se desea conocer el nombre</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexName(ByVal IndexId As Integer) As String
        Dim strselect As New StringBuilder()
        Dim IndexName As String = Nothing
        Try
            strselect.Append("Select Index_Name from Doc_Index where(Index_Id = ")
            strselect.Append(IndexId)
            strselect.Append(")")
            IndexName = Server.Con.ExecuteScalar(CommandType.Text, strselect.ToString)

            If IsNothing(IndexName) Then
                IndexName = String.Empty
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return IndexName
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Arraylist con los nombre de todos los atributos existentes en Zamba
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexNames() As ArrayList
        Static arr As ArrayList = arr

        If arr Is Nothing Then
            arr = New ArrayList
            arr.Add("")
            Dim strselect As String = "Select Index_Name from Doc_Index order by 1"
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
            For i As Int32 = 0 To ds.Tables(0).Rows.Count - 1
                arr.Add(DirectCast(ds.Tables(0).Rows(i).Item(0), String).Trim)
            Next
        End If

        Return arr
    End Function

    Public Shared Function GetIndexIdAndName() As DataTable

        Dim strselect As String = "Select Index_Id, Index_Name from Doc_Index order by 1"
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Return ds.Tables(0)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Datatable con los nombres de todos los atributos existentes en Zamba
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	09/10/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexDsNames() As DataSet
        Dim strselect As String = "Select Index_Name from Doc_Index order by Index_Name"

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)

        ds.DataSetName = "DsIndexName"

        Return ds
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Datatable con los ids y nombres de todos los atributos existentes en Zamba
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	04/05/2011	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexsDsIdsAndNames() As DataSet
        Dim strselect As String = "Select Index_id, Index_Name from Doc_Index order by Index_Name"

        Return Server.Con.ExecuteDataset(CommandType.Text, strselect)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene el ID de un indice en base a su nombre
    ''' </summary>
    ''' <param name="IndexName">Nombre del indice del cual se desea conocer su ID</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexId(ByVal IndexName As String) As Int64
        Dim strselect As String = "Select Index_Id from Doc_Index where ltrim(rtrim(Index_Name)) = '" & IndexName & "'"
        Dim DsIndex As String = Server.Con.ExecuteScalar(CommandType.Text, strselect)
        Dim IndexId As Int64
        If String.IsNullOrEmpty(DsIndex) Then
            IndexId = 0
        Else
            IndexId = Int64.Parse(DsIndex)
        End If
        Return IndexId
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Dataset con todos los valores que aparecen en el atributo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexValues() As DataSet
        Dim strselect As String = "SELECT INDEX_ID, INDEX_NAME, INDEX_TYPE, INDEX_LEN, AUTOFILL, NOINDEX, DROPDOWN, AUTODISPLAY, INVISIBLE, OBJECT_TYPE_ID FROM DOC_INDEX ORDER BY INDEX_NAME"
        Dim DSIndex1 As New DataSet
        Dim DSTEMP As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)

        DSTEMP.Tables(0).TableName = DSIndex1.Tables(0).TableName
        DSIndex1.Merge(DSTEMP)


        strselect = Nothing
        DSTEMP.Dispose()
        DSTEMP = Nothing

        Return DSIndex1
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece el dropdown de un indice
    ''' </summary>
    ''' <param name="IndexId">Id del indice</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SetIndexDropDown(ByVal IndexId As Int32, ByVal dropValue As IndexAdditionalType)
        Dim query As New StringBuilder

        query.Append("Update doc_index set DROPDOWN=")
        query.Append(dropValue)
        query.Append(" where index_id = ")
        query.Append(IndexId)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece a un indice si el dropdown es de una vista o una tabla
    ''' </summary>
    ''' <param name="IndexId">Id del indice</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	07/10/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SetIndexDropDownType(ByVal IndexId As Int32, ByVal dropValue As Int32)
        Dim query As New StringBuilder

        query.Append("Update doc_index set DROPDOWNTYPE=")
        'query.Append("Update doc_index set DROPDOWN=")
        query.Append(dropValue)
        query.Append(" where index_id = ")
        query.Append(IndexId)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna el tipo de indice
    ''' ********  Este metodo es utilizado para Web, no utilizar desde Zamba ********
    ''' </summary>
    ''' <param name="IndexId">Id del indice</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	07/10/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexDropDownType(ByVal IndexId As Int32) As Int64
        Dim query As New StringBuilder

        query.Append("Select DROPDOWN from doc_index")
        query.Append(" where index_id = ")
        query.Append(IndexId)
        Return Server.Con.ExecuteScalar(CommandType.Text, query.ToString)
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece un indice como obligatorio para un Entidad
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad que tendrá el atributo obligatorio</param>
    ''' <param name="IndexId">Id del indice que sera obligatorio</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    '''     [Diego]     22/07/2008 Modified Los storeds/paquetes estaban referenciando a una tabla con el nombre equivocado
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SetIndexRequired(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        Dim query As New StringBuilder
        Dim dropdown As Int16
        If Server.isOracle Then
            Dim parValues() As Object = {IndexId, 2}
            dropdown = Server.Con.ExecuteScalar(CommandType.Text, "Select Dropdown from doc_Index where index_id=" & IndexId)  '"zsp_index_100.GetIndexDropDown",  parValues)
        Else
            Dim parValues() As Object = {IndexId}
            Try
                dropdown = Server.Con.ExecuteScalar("zsp_index_100_GetIndexDropDown", parValues)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If

        If dropdown = 2 Then
            If Server.isOracle Then
                query.Append("Update Index_R_Doc_Type set Mustcomplete=1, ShowLotus=1, LoadLotus = 1 where Doc_Type_ID=")
                query.Append(DocTypeId)
                query.Append("and Index_Id=")
                query.Append(IndexId)
                Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)

            Else
                Dim parValues() As Object = {DocTypeId, IndexId}
                Try
                    Server.Con.ExecuteNonQuery("zsp_index_200_UpdIndexRDoctypeByDtInd", parValues)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If

        Else
            If Server.isOracle Then
                query.Append("Update Index_R_Doc_Type set Mustcomplete=1, ShowLotus=1 where Doc_Type_ID=")
                query.Append(DocTypeId)
                query.Append("and Index_Id=")
                query.Append(IndexId)
                Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
            Else
                Dim parValues() As Object = {DocTypeId, IndexId}
                Try
                    Server.Con.ExecuteNonQuery("zsp_index_200_UpdIndexRDoctypeByDtInd2", parValues)
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If
        End If
        'Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub

    Public Shared Function GetIndexsIdsByDocTypeId(ByVal docTypeId As Int64) As List(Of Int64)

        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("SELECT Doc_Index.Index_Id FROM Doc_Index INNER JOIN Index_R_Doc_Type ")
        QueryBuilder.Append("ON Doc_Index.Index_Id = Index_R_Doc_Type.Index_Id WHERE ")
        QueryBuilder.Append("Index_R_Doc_Type.Doc_Type_Id = ")
        QueryBuilder.Append(docTypeId.ToString())

        Dim DsIndexs As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing

        Dim IndexIds As New List(Of Int64)

        If Not IsNothing(DsIndexs) AndAlso DsIndexs.Tables.Count = 1 Then
            For Each CurrentRow As DataRow In DsIndexs.Tables(0).Rows
                IndexIds.Add(Int64.Parse(CurrentRow(0).ToString))
            Next

        End If

        Return IndexIds
    End Function

    ''' <summary>
    ''' Quita la propiedad REQUERIDO de un indice para un Entidad
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad</param>
    ''' <param name="IndexId">Id del indice</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Diego]	21/07/2008	Created
    ''' </history>
    Public Shared Sub DeleteIndexRequired(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        Dim query As New StringBuilder
        query.Append("Update Index_R_Doc_Type set Mustcomplete= 0 where Doc_Type_ID= ")
        query.Append(DocTypeId)
        query.Append(" and Index_Id=")
        query.Append(IndexId)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    'Public Shared Property IsRequired(ByVal docTypeId as Int64, ByVal IndexId As Int32) As Boolean
    '    Get

    '    End Get
    '    Set(ByVal Value As Boolean)

    '    End Set
    'End Property

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo estatico que permite la visualización de un Atributo, en un entidad para su exportacion
    ''' desde Lotus Notes
    ''' </summary>
    ''' <param name="Doctypeid">Id del DOC_TYPE que se desea mostrar</param>
    ''' <param name="index">Objeto Index que se va a mostrar en Lotus Notes</param>
    ''' <remarks>
    ''' El objeto Atributo debe existir previamente en Zamba.
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Javier]	07/01/2011	Modified    Se corrige query si dropdown distinto de cero loadlotus tambien va en 1
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub ShowInLotusNotes(ByVal Doctypeid As Int64, ByVal indexId As Int64, ByVal dropDown As Int32)
        Dim sql As String
        If dropDown = 0 Then
            sql = "Update index_r_doc_type set ShowLotus=1, loadlotus=0 where Index_id=" & indexId & " and doc_type_id=" & Doctypeid
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)

            '[Comento] DIEGO 21-7-2008 Ver si se usa esta tabla, Cree un workitem al respecto
            'sql = "Insert into Notesindex_r_doc_index(Indexid,cargar) values(" & indexId & ",0)"
            'Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Else
            sql = "Update index_r_doc_type set ShowLotus=1, loadlotus=1 where Index_id=" & indexId & " and doc_type_id=" & Doctypeid
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)

            '[Comento] DIEGO 21-7-2008  Ver si se usa esta tabla, Cree un workitem al respecto
            'sql = "Insert into Notesindex_r_doc_index(Indexid,cargar) values(" & indexId & ",1)"
            'Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        End If
    End Sub

    ''' <summary>
    ''' [sebastian 20-03-2009]Change value in autocomplete column into  index_r_doc_type_Businesss
    ''' </summary>
    ''' <history>Marcelo Modified 22/09/2009</history>
    ''' <param name="Doctypeid"></param>
    ''' <param name="indexId"></param>
    ''' <param name="dropDown"></param>
    ''' <remarks></remarks>
    Public Shared Sub BuildAutoincremental(ByVal Doctypeid As Int64, ByVal indexId As Int64, ByVal dropDown As Int32)
        Dim sql As String
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertando valor autoincremental")
            sql = "Update index_r_doc_type set AutoComplete=1 where Index_id=" & indexId & " and doc_type_id=" & Doctypeid
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)

            sql = "insert into ZIndexAutoincremental values (" & indexId & ", " & Doctypeid & ", 0)"
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Finally
            sql = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Quita la propiedad Mostrar en lotus Notes de un indice para un Entidad
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad</param>
    ''' <param name="IndexId">Id del indice</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Diego]	21/07/2008	Created
    ''' </history>
    Public Shared Sub DeleteShowInLotusNotes(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        Dim sql As String
        sql = "Update index_r_doc_type set ShowLotus=0, loadlotus=0 where Index_id=" & IndexId & " and doc_type_id=" & DocTypeId
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub
    ''' <summary>
    ''' [sebastian 20-03-2009] Set autocomplete column to 0 to delete check from grid
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <param name="IndexId"></param>
    ''' <history>Marcelo Modified 22/09/2009</history>
    ''' <remarks></remarks>
    Public Shared Sub DeleteAutoincremental(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        Dim sql As String
        Try
            sql = "Update index_r_doc_type set AutoComplete=0 where Index_id=" & IndexId & " and doc_type_id=" & DocTypeId
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)

            sql = "delete ZIndexAutoincremental where index_id =" & IndexId & " and doc_type_id= " & DocTypeId
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Finally
            sql = Nothing
        End Try
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Arraylist con la lista de seleccion asociada a un Atributo
    ''' </summary>
    ''' <param name="IndexID">ID del indice que se desea obtener la lista de selección</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Marcelo]	26/07/2008	Modified
    '''     [Gaston]	04/03/2009	Modified    Si hay un error al ejecutar la consulta entonces se retorna Nothing
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetIndexsSearchListILSTasArrayList(ByVal IndexID As Integer) As ArrayList

        Dim DSTableList As DataSet
        Dim Strselect As String = String.Empty
        If Server.isOracle = True Then
            Strselect = "SELECT trim(ITEM) as item FROM ILst_I" & IndexID & " order by ITEM"
        Else
            Strselect = "SELECT ltrim(ITEM) as item FROM ILst_I" & IndexID & " order by ITEM"
        End If

        Try
            DSTableList = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return (Nothing)
        End Try

        Dim a As New ArrayList
        Dim i As Integer
        For i = 0 To DSTableList.Tables(0).Rows.Count - 1
            a.Add(DSTableList.Tables(0).Rows(i).Item(0).ToString())
        Next

        Return a

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Arraylist con la lista de seleccion asociada a un Atributo
    ''' </summary>
    ''' <param name="IndexID">ID del indice que se desea obtener la lista de selección</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Marcelo]	26/07/2008	Modified
    '''     [Gaston]	04/03/2009	Modified    Si hay un error al ejecutar la consulta entonces se retorna Nothing
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function retrieveArraylist(ByVal IndexID As Integer) As List(Of String)

        Dim DSTableList As DataSet
        Dim Strselect As String = String.Empty
        If Server.isOracle = True Then
            Strselect = "SELECT trim(ITEM) as item FROM ILst_I" & IndexID & " order by ITEM"
        Else
            Strselect = "SELECT ltrim(ITEM) as item FROM ILst_I" & IndexID & " order by ITEM"
        End If

        Try
            DSTableList = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Tabla faltante ILst_I" & IndexID)
            Return (Nothing)
        End Try

        Dim a As New List(Of String)
        Dim i As Integer
        For i = 0 To DSTableList.Tables(0).Rows.Count - 1
            a.Add(DSTableList.Tables(0).Rows(i).Item(0).ToString())
        Next

        Return a

    End Function

    Public Shared Function retrieveArraylistHierachical(ByVal DataTableName As String, ByVal IndexID As Integer, ByVal IndexsValues As Hashtable) As ArrayList

        Dim DSTableList As DataSet
        Dim Strselect As String = String.Empty
        Dim StrWhere As String = String.Empty
        Dim Index As Index

        If Server.isOracle = True Then
            Strselect = "SELECT DISTINCT trim(I" & IndexID & ") AS Item FROM " & DataTableName
        Else
            Strselect = "SELECT DISTINCT ltrim(I" & IndexID & ") AS Item FROM " & DataTableName
        End If

        For Each ind As DictionaryEntry In IndexsValues
            Index = DirectCast(ind.Value, Index)
            If Not String.IsNullOrEmpty(Index.DataTemp) Then
                StrWhere &= "I" & Index.ID.ToString & " = '" & Index.DataTemp & "' AND "
            End If
        Next

        If Not String.IsNullOrEmpty(StrWhere) Then
            'eliminar el ultimo and
            If StrWhere.EndsWith(" AND ") Then
                StrWhere = StrWhere.Remove(StrWhere.Length - 5, 5)
            End If
            Strselect &= " WHERE " & StrWhere
        End If

        Try
            DSTableList = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return (Nothing)
        End Try

        Dim a As New ArrayList

        For i As Integer = 0 To DSTableList.Tables(0).Rows.Count - 1
            a.Add(DSTableList.Tables(0).Rows(i).Item(0).ToString())
        Next

        Return a

    End Function

    Public Shared Function GetDropDownListSerarchCode(ByVal IndexId As Int32, ByVal Value As String) As Int32

        Dim Strselect As String = String.Empty

        If Server.isOracle = True Then
            Strselect = "SELECT ITEMID FROM ILst_I" & IndexId & " WHERE trim(ITEM) = '" & Value.Trim() & "'"
        Else
            Strselect = "SELECT ITEMID FROM ILst_I" & IndexId & " WHERE ltrim(rtrim(ITEM)) = '" & Value.Trim() & "'"
        End If

        Return Server.Con.ExecuteScalar(CommandType.Text, Strselect)

    End Function


#Region "Versiones"

    Public Shared Function GetIndexByPublishStates(ByVal DocTypeId As Int64) As DataSet
        Dim query As New StringBuilder
        query.Append("SELECT ZVerConfig.IndexId, ZVerEv.EvValue ")
        query.Append(" FROM ZVerConfig  INNER JOIN ZVerEv ON ZVerConfig.dataid = ZVerEv.dataid ")
        query.Append(" INNER JOIN ZVerEvents ON ZVerEvents.EventId = ZVerEv.EventId ")
        query.Append(" WHERE ZVerConfig.DtId = ")
        query.Append(DocTypeId.ToString)
        query.Append(" and ZVerEvents.Event = 'Publicado'")

        Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

    End Function
#End Region

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un DataTable con la lista de seleccion asociada a un Atributo
    ''' </summary>
    ''' <param name="IndexID">ID del indice que se desea obtener la lista de selección</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function retrievetablelist(ByVal IndexID As Integer) As DataTable
        Dim table As String = "ILst_I" & IndexID.ToString
        Dim strSelect As String = "SELECT Item from " & table
        Dim DSTableList As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        'TODO Falta ver si se le puede pasar a un Store con que tabla se va a manejar

        DSTableList.Tables(0).TableName = table
        Return DSTableList.Tables(table)
    End Function

    Public Shared Function retrievearraytablelist(ByVal IndexID As Integer) As List(Of String)
        Dim Strselect As String = "SELECT ITEM FROM ILst_I" & IndexID
        Dim DSTableList As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)

        Dim a As New List(Of String)
        Dim i As Integer
        For i = 0 To DSTableList.Tables(0).Rows.Count - 1
            a.Add(DSTableList.Tables(0).Rows(i).Item(0))
        Next

        Return a
    End Function

    Public Shared Function getTableList(ByVal IndexId As Int32) As DataSet ' DSTableList
        Dim strSelect As String = "SELECT Item from ILst_I" & IndexId & " order by Item"
        '        Dim DsTableList1 As New DSTableList
        Return Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        ' dstemp.Tables(0).TableName = DsTableList1.Tables(0).TableName
        ' dstemp.Tables(0).Columns(0).ColumnName = DsTableList1.Tables(0).Columns(0).ColumnName
        ' dstemp.Tables(0).Columns(1).ColumnName = DsTableList1.Tables(0).Columns(1).ColumnName
        '       DsTableList1.Merge(DSTEMP)
        'Return dstemp 'DsTableList1
    End Function

    Public Shared Function getTableListHierachical(ByVal IndexId As Int32) As DataSet ' DSTableList
        Dim strSelect As String

        strSelect = "SELECT "
        strSelect &= "  Id, Indice,  "
        strSelect &= "  IndicePadre AS IndicePadreID, RTrim(LTrim(DIP.INDEX_NAME)) AS IndicePadre, "
        strSelect &= "  CodigoPadre AS CodigoPadreID, '' AS ValorPadre, "
        strSelect &= "  Codigo As CodigoId, '' As Valor "
        strSelect &= "FROM "
        strSelect &= "  ZIndexHierarchyKey HK "
        strSelect &= "  INNER JOIN DOC_INDEX DIP ON HK.IndicePadre = DIP.INDEX_ID "
        strSelect &= "WHERE "
        strSelect &= "  Indice = " & IndexId

        Return Server.Con.ExecuteDataset(CommandType.Text, strSelect)
    End Function

    Public Shared Function getTableListParentName(ByVal IndexId As Int32) As String
        Dim strSelect As String

        strSelect = "SELECT "
        strSelect &= "  RTrim(LTrim(DIP.INDEX_NAME)) AS IndicePadre "
        strSelect &= "FROM "
        strSelect &= "  ZIndexHierarchyKey HK "
        strSelect &= "  INNER JOIN DOC_INDEX DIP ON HK.IndicePadre = DIP.INDEX_ID "
        strSelect &= "WHERE "
        strSelect &= "  Indice = " & IndexId

        Return Server.Con.ExecuteScalar(CommandType.Text, strSelect)
    End Function

    Public Shared Function getTableListParentID(ByVal IndexId As Int32) As Int32
        Dim strSelect As String

        strSelect = "SELECT "
        strSelect &= "  IndicePadre "
        strSelect &= "FROM "
        strSelect &= "  ZIndexHierarchyKey HK "
        strSelect &= "  INNER JOIN DOC_INDEX DIP ON HK.IndicePadre = DIP.INDEX_ID "
        strSelect &= "WHERE "
        strSelect &= "  Indice = " & IndexId

        Return Server.Con.ExecuteScalar(CommandType.Text, strSelect)
    End Function

    Public Shared Function getTableListHierachicalValueByIDs(ByVal IndexId As Int32, ByVal Codigo As Int32) As String
        Dim strSelect As String = "SELECT Item from ILst_I" & IndexId & " WHERE ITEMID = " & Codigo
        Return Server.Con.ExecuteScalar(CommandType.Text, strSelect)
    End Function

    Public Shared Function GetTable(ByVal indexid As Int32) As DataSet
        Try
            'Dim strSelect As String = "SELECT Item from ILst_I" & indexid & " order by Item"
            Dim strSelect As String = "SELECT * from ILst_I" & indexid & " order by Item"
            Return Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return Nothing
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Destruye la lista de busqueda asociada a un indice
    ''' </summary>
    ''' <param name="IndexId">Atributo del cual se eliminara la lista</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' 	[Marcelo]	07/10/2008	Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub delindexlist(ByVal IndexId As Int32)
        Dim Cr As CreateTables = Server.CreateTables
        Try
            Cr.DelIndexList(IndexId)
        Catch ex As SqlClient.SqlException
            Try
                If ex.Number = 3705 Then
                    Dim strSelect As String = "Drop view ILst_I" & IndexId
                    Server.Con.ExecuteNonQuery(CommandType.Text, strSelect)
                End If
            Catch ex2 As Exception
                ZClass.raiseerror(ex2)
            End Try
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Cr.Dispose()
            Cr = Nothing
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea una lista de busqueda para un indice
    ''' </summary>
    ''' <param name="IndexId">ID del Atributo para el cual se creara la lista</param>
    ''' <param name="IndexLen">Longitud maxima que representa los caracteres que podra contener como maximo cada palabra</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub addindexlist(ByVal IndexId As Int32, ByVal IndexLen As Int32)
        Dim Cr As CreateTables = Server.CreateTables
        Try
            Cr.AddIndexList(IndexId, IndexLen)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Cr.Dispose()
            Cr = Nothing
        End Try
    End Sub
    ''' <summary>
    ''' Crea una vista para una lista de sustitucion de un indice
    ''' </summary>
    ''' <param name="IndexId">Id del indice</param>
    ''' <param name="tableName">Nombre de la tabla a la q apuntara la vista</param>
    ''' <param name="columnCodName">Nombre de la columna q sera el codigo</param>
    ''' <param name="columnDescName">Nombre de la columna q sera la descripcion</param>
    ''' <history>Marcelo 07/10/08 Created</history>
    ''' <remarks></remarks>
    Public Shared Sub addindexlist(ByVal IndexId As Int32, ByVal tableName As String, ByVal columnCodName As String, ByVal columnDescName As String, ByVal script As String)
        Dim sql As StringBuilder = New StringBuilder()
        Try
            If script.Length = 0 Then
                sql.Append("CREATE VIEW ILST_I")
                sql.Append(IndexId.ToString())
                sql.Append(" AS SELECT ")
                sql.Append(columnCodName)
                sql.Append(" AS ITEMID, ")
                sql.Append(columnDescName)
                sql.Append(" AS ITEM FROM ")
                sql.Append(tableName)

                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString())
            Else
                sql.Append(script)
                Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString())
            End If

        Finally
            sql = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Crea una vista para una tabla de busqueda de un indice
    ''' </summary>
    ''' <param name="IndexId">Id del indice</param>
    ''' <param name="tableName">Nombre de la tabla a la q apuntara la vista</param>
    ''' <param name="columnCodName">Nombre de la columna q sera el codigo</param>
    ''' <param name="columnDescName">Nombre de la columna q sera la descripcion</param>
    ''' <history>Marcelo 07/10/08 Created</history>
    ''' <remarks></remarks>
    Public Shared Sub addindexSust(ByVal IndexId As Int32, ByVal tableName As String, ByVal columnCodName As String, ByVal columnDescName As String, ByVal script As String)
        Dim sql As StringBuilder = New StringBuilder()
        Try
            If script.Length = 0 Then
                sql.Append("CREATE VIEW SLST_S")
                sql.Append(IndexId.ToString())
                sql.Append(" AS SELECT ")
                sql.Append(columnCodName)
                sql.Append(" AS CODIGO, ")
                sql.Append(columnDescName)
                sql.Append(" AS DESCRIPCION FROM ")
                If Server.isSQLServer Then
                    Dim intParams As Int32
                    Dim strParams() As String = tableName.Split(Chr(46))

                    tableName = String.Empty
                    For intParams = 0 To UBound(strParams)
                        tableName += Chr(91) + strParams(intParams) + Chr(93) + Chr(46)
                    Next

                    tableName = tableName.Substring(0, tableName.Length - 1)
                End If
                sql.Append(tableName)
            Else
                sql.Append(script)
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString())
        Finally
            sql = Nothing
        End Try
    End Sub




    Public Shared Sub InsertIndexListJerarquico(ByVal IndexId As Int32, ByVal IndexParentId As Int32, ByVal Codigo As Int32, ByVal CodigoParent As Int32)
        Try
            Dim strSQL As String

            strSQL = "INSERT INTO ZIndexHierarchyKey (Indice, IndicePadre, Codigo, CodigoPadre) VALUES ("
            strSQL &= IndexId & ", " & IndexParentId & ", " & Codigo & ", " & CodigoParent & ")"

            Server.Con.ExecuteNonQuery(CommandType.Text, strSQL)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    'Javier: Agregado el nombre de la tabla a levantar los datos
    Public Shared Sub InsertIndexListJerarquico(ByVal IndexId As Int32, ByVal IndexParentId As Int32, ByVal DataTableName As String)
        Try
            Dim strSQL As String

            strSQL = "INSERT INTO ZIndexHierarchyKey (Indice, IndicePadre, DataValuesTable) VALUES ("
            strSQL &= IndexId & ", " & IndexParentId & ", '" & DataTableName & "')"

            Server.Con.ExecuteNonQuery(CommandType.Text, strSQL)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub AddHierarchyItem(ByVal DataTableName As String,
                                       ByVal ParentValue As String, ByVal ChildValue As String)
        Try
            Dim strSQL As String

            strSQL = "INSERT INTO " & DataTableName & " (ParentValue, ChildValue) VALUES ("

            Dim parentColumnDatatype As String = GetColumnType(DataTableName, "ParentValue")
            Dim childColumnDatatype As String = GetColumnType(DataTableName, "ChildValue")

            If parentColumnDatatype.Contains("char") OrElse parentColumnDatatype.Contains("varchar") _
             OrElse parentColumnDatatype.Contains("nchar") OrElse parentColumnDatatype.Contains("nvarchar") Then
                strSQL &= "'" & ParentValue & "'"
            ElseIf parentColumnDatatype.Contains("numeric") OrElse parentColumnDatatype.Contains("int") OrElse
                parentColumnDatatype.Contains("decimal") Then
                strSQL &= ParentValue
            End If

            strSQL &= ", "

            If childColumnDatatype.Contains("char") OrElse childColumnDatatype.Contains("varchar") _
             OrElse childColumnDatatype.Contains("nchar") OrElse childColumnDatatype.Contains("nvarchar") Then
                strSQL &= "'" & ChildValue & "'"
            ElseIf childColumnDatatype.Contains("numeric") OrElse childColumnDatatype.Contains("int") OrElse
                childColumnDatatype.Contains("decimal") Then
                strSQL &= ChildValue
            End If

            strSQL &= ")"

            Server.Con.ExecuteNonQuery(CommandType.Text, strSQL)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub DeleteHierarchyValues(ByVal ListOfValues As List(Of String), ByVal DataTableName As String)
        Try
            Dim max As Integer = ListOfValues.Count - 1
            Dim sbForWhere As New StringBuilder
            Dim parentColumnDatatype As String = GetColumnType(DataTableName, "ParentValue")
            Dim childColumnDatatype As String = GetColumnType(DataTableName, "ChildValue")

            For i As Integer = 0 To max
                sbForWhere.Append("(")
                sbForWhere.Append("ParentValue = ")

                If parentColumnDatatype.Contains("char") OrElse parentColumnDatatype.Contains("varchar") _
                    OrElse parentColumnDatatype.Contains("nchar") OrElse parentColumnDatatype.Contains("nvarchar") Then
                    sbForWhere.Append("'" & ListOfValues(i).Split("|")(0) & "'")
                Else
                    sbForWhere.Append(ListOfValues(i).Split("|")(0))
                End If

                sbForWhere.Append(" and ChildValue = ")

                If childColumnDatatype.Contains("char") OrElse childColumnDatatype.Contains("varchar") _
                    OrElse childColumnDatatype.Contains("nchar") OrElse childColumnDatatype.Contains("nvarchar") Then
                    sbForWhere.Append("'" & ListOfValues(i).Split("|")(1) & "'")
                Else
                    sbForWhere.Append(ListOfValues(i).Split("|")(1))
                End If


                sbForWhere.Append(") or ")
            Next

            Dim strWhere As String = sbForWhere.ToString.Substring(0, sbForWhere.ToString.LastIndexOf(" or ") + 1)

            Dim sqlQuery As String = "delete " & DataTableName & " where " & strWhere
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlQuery)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub ModifyHierarchyValue(ByVal DataTableName As String, ByVal ParentOldValue As String,
                                            ByVal ChildOldValue As String, ByVal ParentNewValue As String,
                                            ByVal ChildNewValue As String)
        Try
            Dim parentColumnDatatype As String = GetColumnType(DataTableName, "ParentValue")
            Dim childColumnDatatype As String = GetColumnType(DataTableName, "ChildValue")

            Dim sqlQuery As String = "update " & DataTableName & " set ParentValue = "

            If parentColumnDatatype.Contains("char") OrElse parentColumnDatatype.Contains("varchar") _
             OrElse parentColumnDatatype.Contains("nchar") OrElse parentColumnDatatype.Contains("nvarchar") Then
                sqlQuery &= "'" & ParentNewValue & "'"
            Else
                sqlQuery &= ParentNewValue
            End If

            sqlQuery &= ","

            sqlQuery &= " ChildValue = "

            If childColumnDatatype.Contains("char") OrElse childColumnDatatype.Contains("varchar") _
             OrElse childColumnDatatype.Contains("nchar") OrElse childColumnDatatype.Contains("nvarchar") Then
                sqlQuery &= "'" & ChildNewValue & "'"
            Else
                sqlQuery &= ChildNewValue
            End If

            sqlQuery &= " where ParentValue = "

            If parentColumnDatatype.Contains("char") OrElse parentColumnDatatype.Contains("varchar") _
             OrElse parentColumnDatatype.Contains("nchar") OrElse parentColumnDatatype.Contains("nvarchar") Then
                sqlQuery &= "'" & ParentOldValue & "'"
            Else
                sqlQuery &= ParentOldValue
            End If

            sqlQuery &= " and ChildValue = "

            If childColumnDatatype.Contains("char") OrElse childColumnDatatype.Contains("varchar") _
             OrElse childColumnDatatype.Contains("nchar") OrElse childColumnDatatype.Contains("nvarchar") Then
                sqlQuery &= "'" & ChildOldValue & "'"
            Else
                sqlQuery &= ChildOldValue
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, sqlQuery)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Shared Function ValidateHierarchicalValueTable(ByVal DataTableName As String) As Boolean

        Dim sbSQL As String
        sbSQL = (" select count(*) as c from user_tables where table_name = upper('" & DataTableName & "')")
        Dim count As Int16 = Server.Con.ExecuteScalar(CommandType.Text, sbSQL)

        If count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Shared Sub CreateHierarchicalValueTable(ByVal IndexId As Int32, ByVal IndexParentId As Int32,
                                                   ByVal IndexTableName As String, ByVal ParentIndexTableName As String,
                                                   ByVal DataTableName As String)
        Try
            Dim sbSQL As New StringBuilder()
            Dim dataTypeParentColumn As String
            Dim dataTypeChildColumn As String
            Dim random As New Random(Date.Now.Ticks)

            Dim IsParentATable As Boolean
            Dim IsChildATable As Boolean
            Try
                Dim parentObjecType As String
                Dim childObjecType As String

                If Server.isOracle Then

                    parentObjecType = Server.Con.ExecuteScalar(CommandType.Text, "select DATA_TYPE from user_tab_cols where table_name = '" & ParentIndexTableName & "'")
                    If Not String.IsNullOrEmpty(parentObjecType) AndAlso parentObjecType = "Table" Then
                        IsParentATable = True
                    Else
                        IsParentATable = False
                    End If

                    childObjecType = Server.Con.ExecuteScalar(CommandType.Text, "select DATA_TYPE from user_tab_cols where table_name = '" & IndexTableName & "'")
                    If Not String.IsNullOrEmpty(childObjecType) AndAlso childObjecType = "Table" Then
                        IsChildATable = True
                    Else
                        IsChildATable = False
                    End If

                    sbSQL.Append("CREATE TABLE ")
                    sbSQL.Append(DataTableName)
                    sbSQL.Append("( ")

                    If ParentIndexTableName.Contains("SLST") Then
                        sbSQL.Append("  ParentValue " & GetColumnType(ParentIndexTableName, "Codigo") & ",")
                    Else
                        sbSQL.Append("  ParentValue " & GetColumnType(ParentIndexTableName, "Item") & ",")
                    End If

                    If IndexTableName.Contains("SLST") Then
                        sbSQL.Append("  ChildValue " & GetColumnType(IndexTableName, "Codigo") & ",")
                    Else
                        sbSQL.Append("  ChildValue " & GetColumnType(IndexTableName, "Item") & ",")
                    End If

                    sbSQL.Append("  PRIMARY KEY (ParentValue,ChildValue)")

                    If (IsParentATable) Then
                        If ParentIndexTableName.Contains("SLST") Then
                            sbSQL.Append("  ,CONSTRAINT HierarchicalFK_" & random.Next() & " FOREIGN KEY (ParentValue) REFERENCES ")
                            sbSQL.Append(ParentIndexTableName)
                            sbSQL.Append(" (Codigo)")
                        End If
                    End If

                    If (IsChildATable) Then
                        If IndexTableName.Contains("SLST") Then
                            sbSQL.Append("  ,CONSTRAINT HierarchicalFK_" & random.Next() & " FOREIGN KEY (ChildValue) REFERENCES ")
                            sbSQL.Append(IndexTableName)
                            sbSQL.Append(" (Codigo)")
                        End If
                    End If

                    sbSQL.Append(") ")


                    Server.Con.ExecuteNonQuery(CommandType.Text, sbSQL.ToString())

                    If (IsParentATable) Then
                        If Not ParentIndexTableName.Contains("SLST") Then
                            sbSQL = New StringBuilder()
                            Dim parentTriggerName As String = "Delete_" & DataTableName & "_" & ParentIndexTableName
                            sbSQL.Append("if exists (select * from user_tables where name like '")
                            sbSQL.Append(parentTriggerName & "') ")
                            sbSQL.Append("drop trigger ")
                            sbSQL.Append(parentTriggerName)
                            Server.Con.ExecuteNonQuery(CommandType.Text, sbSQL.ToString())
                            sbSQL = New StringBuilder()
                            sbSQL.Append("CREATE TRIGGER ")
                            sbSQL.Append(parentTriggerName)
                            sbSQL.Append(" ON ")
                            sbSQL.Append(ParentIndexTableName)
                            sbSQL.Append(" FOR DELETE AS ")
                            sbSQL.Append("Declare @Item as ")
                            sbSQL.Append(GetColumnType(ParentIndexTableName, "Item"))
                            sbSQL.Append(" IF (SELECT COUNT(1) FROM deleted) > 0 BEGIN ")
                            sbSQL.Append(" SELECT @Item=Item from deleted")
                            sbSQL.Append(" DELETE ")
                            sbSQL.Append(DataTableName)
                            sbSQL.Append(" where ParentValue = @Item")
                            sbSQL.Append(" END")
                            sbSQL.Append(" ")
                            Server.Con.ExecuteNonQuery(CommandType.Text, sbSQL.ToString())
                        End If
                    End If

                    If (IsChildATable) Then
                        If Not ParentIndexTableName.Contains("SLST") Then
                            sbSQL = New StringBuilder()
                            Dim childTriggerName As String = "Delete_" & DataTableName & "_" & IndexTableName
                            sbSQL.Append("if exists (select * from user_tables where name like '")
                            sbSQL.Append(childTriggerName & "') ")
                            sbSQL.Append("drop trigger ")
                            sbSQL.Append(childTriggerName)
                            Server.Con.ExecuteNonQuery(CommandType.Text, sbSQL.ToString())
                            sbSQL = New StringBuilder()
                            sbSQL.Append("CREATE TRIGGER ")
                            sbSQL.Append(childTriggerName)
                            sbSQL.Append(" ON ")
                            sbSQL.Append(IndexTableName)
                            sbSQL.Append(" FOR DELETE AS ")
                            sbSQL.Append("Declare @Item as ")
                            sbSQL.Append(GetColumnType(IndexTableName, "Item"))
                            sbSQL.Append(" IF (SELECT COUNT(1) FROM deleted) > 0 BEGIN ")
                            sbSQL.Append(" SELECT @Item=Item from deleted")
                            sbSQL.Append(" DELETE ")
                            sbSQL.Append(DataTableName)
                            sbSQL.Append(" where ChildValue = @Item")
                            sbSQL.Append(" END")
                            sbSQL.Append(" ")
                            Server.Con.ExecuteNonQuery(CommandType.Text, sbSQL.ToString())
                        End If
                    End If

                Else
                    parentObjecType = Server.Con.ExecuteScalar(CommandType.Text, "select xtype from user_tables where name = '" & ParentIndexTableName & "'")
                    If Not String.IsNullOrEmpty(parentObjecType) AndAlso parentObjecType = "U" Then
                        IsParentATable = True
                    Else
                        IsParentATable = False
                    End If

                    childObjecType = Server.Con.ExecuteScalar(CommandType.Text, "select xtype from user_tables where name = '" & IndexTableName & "'")
                    If Not String.IsNullOrEmpty(childObjecType) AndAlso childObjecType = "U" Then
                        IsChildATable = True
                    Else
                        IsChildATable = False
                    End If


                    sbSQL.Append(DataTableName)
                    sbSQL.Append("') ")

                    sbSQL.Append("CREATE TABLE ")
                    sbSQL.Append(DataTableName)
                    sbSQL.Append("( ")

                    If ParentIndexTableName.Contains("SLST") Then
                        sbSQL.Append("  ParentValue " & GetColumnType(ParentIndexTableName, "Codigo") & ",")
                    Else
                        sbSQL.Append("  ParentValue " & GetColumnType(ParentIndexTableName, "Item") & ",")
                    End If

                    If IndexTableName.Contains("SLST") Then
                        sbSQL.Append("  ChildValue " & GetColumnType(IndexTableName, "Codigo") & ",")
                    Else
                        sbSQL.Append("  ChildValue " & GetColumnType(IndexTableName, "Item") & ",")
                    End If

                    sbSQL.Append("  PRIMARY KEY (ParentValue,ChildValue)")

                    If (IsParentATable) Then
                        If ParentIndexTableName.Contains("SLST") Then
                            sbSQL.Append("  ,CONSTRAINT HierarchicalFK_" & random.Next() & " FOREIGN KEY (ParentValue) REFERENCES ")
                            sbSQL.Append(ParentIndexTableName)
                            sbSQL.Append(" (Codigo)")
                        End If
                    End If

                    If (IsChildATable) Then
                        If IndexTableName.Contains("SLST") Then
                            sbSQL.Append("  ,CONSTRAINT HierarchicalFK_" & random.Next() & " FOREIGN KEY (ChildValue) REFERENCES ")
                            sbSQL.Append(IndexTableName)
                            sbSQL.Append(" (Codigo)")
                        End If
                    End If
                    sbSQL.Append(") ")

                    Server.Con.ExecuteNonQuery(CommandType.Text, sbSQL.ToString())

                    If (IsParentATable) Then
                        If Not ParentIndexTableName.Contains("SLST") Then
                            sbSQL = New StringBuilder()
                            Dim parentTriggerName As String = "Delete_" & DataTableName & "_" & ParentIndexTableName
                            sbSQL.Append("if exists (select * from user_tables where name like '")
                            sbSQL.Append(parentTriggerName & "') ")
                            sbSQL.Append("drop trigger ")
                            sbSQL.Append(parentTriggerName)
                            Server.Con.ExecuteNonQuery(CommandType.Text, sbSQL.ToString())
                            sbSQL = New StringBuilder()
                            sbSQL.Append("CREATE TRIGGER ")
                            sbSQL.Append(parentTriggerName)
                            sbSQL.Append(" ON ")
                            sbSQL.Append(ParentIndexTableName)
                            sbSQL.Append(" FOR DELETE AS ")
                            sbSQL.Append("Declare @Item as ")
                            sbSQL.Append(GetColumnType(ParentIndexTableName, "Item"))
                            sbSQL.Append(" IF (SELECT COUNT(1) FROM deleted) > 0 BEGIN ")
                            sbSQL.Append(" SELECT @Item=Item from deleted")
                            sbSQL.Append(" DELETE ")
                            sbSQL.Append(DataTableName)
                            sbSQL.Append(" where ParentValue = @Item")
                            sbSQL.Append(" END")
                            sbSQL.Append(" ")
                            Server.Con.ExecuteNonQuery(CommandType.Text, sbSQL.ToString())
                        End If
                    End If

                    If (IsChildATable) Then
                        If Not ParentIndexTableName.Contains("SLST") Then
                            sbSQL = New StringBuilder()
                            Dim childTriggerName As String = "Delete_" & DataTableName & "_" & IndexTableName
                            sbSQL.Append("if exists (select * from user_tables where name like '")
                            sbSQL.Append(childTriggerName & "') ")
                            sbSQL.Append("drop trigger ")
                            sbSQL.Append(childTriggerName)
                            Server.Con.ExecuteNonQuery(CommandType.Text, sbSQL.ToString())
                            sbSQL = New StringBuilder()
                            sbSQL.Append("CREATE TRIGGER ")
                            sbSQL.Append(childTriggerName)
                            sbSQL.Append(" ON ")
                            sbSQL.Append(IndexTableName)
                            sbSQL.Append(" FOR DELETE AS ")
                            sbSQL.Append("Declare @Item as ")
                            sbSQL.Append(GetColumnType(IndexTableName, "Item"))
                            sbSQL.Append(" IF (SELECT COUNT(1) FROM deleted) > 0 BEGIN ")
                            sbSQL.Append(" SELECT @Item=Item from deleted")
                            sbSQL.Append(" DELETE ")
                            sbSQL.Append(DataTableName)
                            sbSQL.Append(" where ChildValue = @Item")
                            sbSQL.Append(" END")
                            sbSQL.Append(" ")
                            Server.Con.ExecuteNonQuery(CommandType.Text, sbSQL.ToString())
                        End If
                    End If

                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Sub DeleteIndexListJerarquico(ByVal Id As Int32)
        Try
            Dim strSQL As String
            strSQL = "DELETE FROM ZIndexHierarchyKey WHERE Indice = " & Id
            Server.Con.ExecuteNonQuery(CommandType.Text, strSQL)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Borra la tabla de relaciones del atributo jerarquico
    ''' </summary>
    ''' <param name="IndexTableName"></param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteHierarchicalValueTable(ByVal IndexTableName As String)
        Try
            Dim sbSQL As New StringBuilder()
            sbSQL.Append("Drop table ")
            sbSQL.Append(IndexTableName)

            Server.Con.ExecuteNonQuery(CommandType.Text, sbSQL.ToString())

            sbSQL = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Shared Function GetHierarchicalTable(ByVal IndexTableName As String)
        Try
            Dim sbSQL As New StringBuilder()
            sbSQL.Append("SELECT ParentValue 'Valor padre',ChildValue 'Valor hijo' FROM ")
            sbSQL.Append(IndexTableName)

            Dim dtToReturn As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sbSQL.ToString())

            If Not dtToReturn Is Nothing AndAlso dtToReturn.Tables.Count > 0 _
                AndAlso Not dtToReturn.Tables(0) Is Nothing Then
                Return dtToReturn.Tables(0)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Actualiza un registro para una tabla de tipo SLST
    ''' </summary>
    ''' <param name="IndexID">ID de la tabla SLST</param>
    ''' <param name="columnCodName">Codigo del registro seleccionado</param>
    ''' <param name="ColumnDescName">Descripcion del registro seleccionado</param>
    ''' <history>
    '''     (pablo) 21/02/2011 Created</history>
    ''' <remarks></remarks>
    Public Shared Sub UpdateindexSust(ByVal IndexId As Int32, ByVal columnCodName As String, ByVal columnDescName As String)
        Dim sql As StringBuilder = New StringBuilder()
        Try
            sql.Append("UPDATE SLST_S")
            sql.Append(IndexId.ToString())
            sql.Append(" SET CODIGO = ")
            sql.Append(columnCodName)
            sql.Append(", DESCRIPCION = '")
            sql.Append(columnDescName)
            sql.Append("' WHERE CODIGO = ")
            sql.Append(columnCodName)

            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString())
        Finally
            sql = Nothing
        End Try
    End Sub
    ''' <summary>
    ''' Inserta un registro para una tabla de tipo SLST
    ''' </summary>
    ''' <param name="IndexID">ID de la tabla SLST</param>
    ''' <param name="columnCodName">Codigo del registro seleccionado</param>
    ''' <param name="ColumnDescName">Descripcion del registro seleccionado</param>
    ''' <history>
    '''     (pablo) 21/02/2011 Created
    '''</history>
    ''' <remarks></remarks>
    Public Shared Function InsertSustIndex(ByVal IndexId As Int32, ByVal columnCodName As String, ByVal columnDescName As String)
        Dim sql As StringBuilder = New StringBuilder()
        Dim DSTableList As DataSet
        Try
            sql.Append("INSERT INTO SLST_S")
            sql.Append(IndexId.ToString)
            sql.Append(" (CODIGO,DESCRIPCION) VALUES ('")
            sql.Append(columnCodName)
            sql.Append("','")
            sql.Append(columnDescName)
            sql.Append("')")

            DSTableList = Server.Con.ExecuteDataset(CommandType.Text, sql.ToString())

            Return DSTableList
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return (Nothing)
        Finally
            sql = Nothing
        End Try
    End Function


    Public Shared Sub InsertIndexList(ByVal indexid As Int32, ByVal IndexList As ArrayList)
        Dim Cr As CreateTables = Server.CreateTables
        Try
            Cr.InsertIndexList(indexid, IndexList)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Cr.Dispose()
            Cr = Nothing
        End Try
    End Sub

    Public Shared Function IndexIsAsigned(ByVal index As Index) As Boolean
        Dim strSelect As String = "SELECT COUNT(Index_Id) from Index_R_Doc_Type where (Index_Id = " & index.ID & ")"
        Dim qrows As Int32 = Server.Con.ExecuteScalar(CommandType.Text, strSelect)
        Return qrows <> 0
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Establece un indice como Unico ( no permite repetir un valor)
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad que tendrá el atributo unico</param>
    ''' <param name="IndexId">Id del indice que sera unico</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	13/03/2008	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SetIndexUnique(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        Dim query As New StringBuilder
        query.Append("ALTER TABLE DOC_I")
        query.Append(DocTypeId)
        query.Append(" ADD CONSTRAINT UQ_I")
        query.Append(DocTypeId)
        query.Append("_I")
        query.Append(IndexId)
        query.Append(" UNIQUE (I")
        query.Append(IndexId)
        query.Append(")")

        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        query.Remove(0, query.Length)
        query.Append("UPDATE INDEX_R_DOC_TYPE SET IsDataUnique = 1 ")
        query.Append(" WHERE INDEX_ID = ")
        query.Append(IndexId)
        query.Append(" AND DOC_TYPE_ID= ")
        query.Append(DocTypeId)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    ''' <summary>
    ''' quita la propiedad Unico del indice
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad que tendrá el atributo unico</param>
    ''' <param name="IndexId">Id del indice que sera unico</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	22/07/2008	Created
    ''' </history>
    Public Shared Sub RemoveIndexUnique(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        Dim removed As Boolean = False
        Dim query As New StringBuilder
        If Server.isSQLServer Then
            'SQL
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "exec sp_helpindex DOC_I" & DocTypeId)

            For Each r As DataRow In ds.Tables(0).Rows
                If (r.Item("INDEX_KEYS") = "I" & IndexId) AndAlso (r.Item("INDEX_NAME").ToString.ToUpper.StartsWith("UQ") OrElse r.Item("INDEX_NAME").ToString.ToUpper.StartsWith("UNIQUE")) Then
                    query.Remove(0, query.Length)
                    query.Append("ALTER TABLE DOC_I")
                    query.Append(DocTypeId)
                    query.Append(" DROP CONSTRAINT ")
                    query.Append(r.Item("INDEX_NAME"))
                    Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
                    removed = True
                End If
            Next
        Else
            'ORACLE
            query.Append("SELECT CONSTRAINT_NAME  FROM USER_CONS_COLUMNS WHERE TABLE_NAME = 'DOC_I")
            query.Append(DocTypeId)
            query.Append("' AND COLUMN_NAME = 'I")
            query.Append(IndexId)
            query.Append("'")
            Dim DS As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

            For Each r As DataRow In DS.Tables(0).Rows
                If r.Item("CONSTRAINT_NAME").ToString.ToUpper.StartsWith("UQ") OrElse r.Item("CONSTRAINT_NAME").ToString.ToUpper.StartsWith("UNIQUE") Then
                    query.Remove(0, query.Length)
                    query.Append("ALTER TABLE DOC_I")
                    query.Append(DocTypeId)
                    query.Append(" DROP CONSTRAINT ")
                    query.Append(r.Item("CONSTRAINT_NAME").ToString)
                    Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
                    removed = True
                End If
            Next
        End If
        If removed Then
            'SI SE QUITO EL UNIQUE SE ACTUALIZA LA INDEX_R_DOC_TYPE
            query.Remove(0, query.Length)
            query.Append("UPDATE INDEX_R_DOC_TYPE SET IsDataUnique = 0 ")
            query.Append(" WHERE INDEX_ID = ")
            query.Append(IndexId)
            query.Append(" AND DOC_TYPE_ID= ")
            query.Append(DocTypeId)
            Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        End If
    End Sub


    ''' <summary>
    ''' Establece el valor por defecto a un indice
    ''' </summary>
    ''' <param name="DocTypeId">Id de la entidad que tendrá el atributo unico</param>
    ''' <param name="IndexId">Id del indice que sera unico</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Diego]	02/07/2008	Created
    ''' </history>
    Public Shared Sub SetIndexDefaultValue(ByVal DocTypeId As Int64, ByVal IndexId As Int32, ByVal Value As Object, ByVal datatypeid As Int16)
        Dim query As New StringBuilder
        If Server.isSQLServer Then
            'SQL
            'BORRA UN DEFAULT EN CASO DE QUE EXISTA
            DeleteIndexDefaultValue(DocTypeId, IndexId)
            query.Append("alter table DOC_I")
            query.Append(DocTypeId)
            query.Append(" add default(")
            Select Case datatypeid
                Case 1, 2, 3, 6
                    'NUMERICO, 'NUMERICO LARGO, 'NUMERICO DECIMALES, 'MONEDA
                    query.Append(Value)
                Case 4
                    'FECHA
                    query.Append(Server.Con.ConvertDate(Value))
                Case 5
                    'FECHA HORA
                    query.Append(Server.Con.ConvertDateTime(Value.ToString))
                Case 7, 8
                    'ALFANUMERICO, 'ALFANUMERICO LARGO
                    query.Append("'" & Value & "'")
                Case 9
                    'SI_NO
                    If String.Compare(Value.ToString.ToUpper, "TRUE") = 0 Then query.Append("1")
                    If String.Compare(Value.ToString.ToUpper, "FALSE") = 0 Then query.Append("0")


            End Select
            query.Append(") for I")
            query.Append(IndexId)
        Else
            'ORACLE
            query.Append("alter table DOC_I")
            query.Append(DocTypeId)
            query.Append(" MODIFY I")
            query.Append(IndexId)
            query.Append(" DEFAULT ")
            Select Case datatypeid
                Case 1, 2, 3, 6
                    'NUMERICO, 'NUMERICO LARGO, 'NUMERICO DECIMALES, 'MONEDA
                    query.Append(Value)
                Case 4
                    'FECHA
                    query.Append(Server.Con.ConvertDate(Value))
                Case 5
                    'FECHA HORA
                    query.Append(Server.Con.ConvertDateTime(Value.ToString))
                Case 7, 8
                    'ALFANUMERICO, 'ALFANUMERICO LARGO
                    query.Append("'" & Value & "'")
                Case 9
                    'SI_NO
                    If Value = True Then
                        query.Append("1")
                    Else
                        query.Append("0")
                    End If

            End Select
        End If
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
        'GUARDA UN REGISTRO EN INDEX_R_DOC_TYPE
        query.Remove(0, query.Length)
        query.Append("UPDATE INDEX_R_DOC_TYPE")
        query.Append(" SET  DefaultValue='")
        query.Append(Value)
        query.Append("' WHERE INDEX_ID = ")
        query.Append(IndexId)
        query.Append(" AND DOC_TYPE_ID= ")
        query.Append(DocTypeId)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    ''' <summary>
    ''' Borra el valor por defecto de un indice
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <param name="IndexId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''<history>[Diego]	21/07/2008	[Created]</history>
    Public Shared Sub DeleteIndexDefaultValue(ByVal DocTypeId As Int64, ByVal IndexId As Int32)
        Dim query As New StringBuilder
        If Server.isSQLServer Then
            'OBTIENE LAS CONSTRAINTS DE LA COLUMNA
            query.Append("SELECT name FROM sysobjects WHERE parent_obj in (select id from sysobjects where name like 'DOC_I")
            query.Append(DocTypeId)
            query.Append("')  and name like '%I")
            query.Append(IndexId)
            query.Append("%'")
            Dim cname As String
            'EN CASO DE QUE EXISTA LA ELIMINA
            For Each Item As DataRow In Server.Con.ExecuteDataset(CommandType.Text, query.ToString).Tables(0).Rows
                If Not IsDBNull(Item.Item(0)) Then
                    cname = Item.Item(0).ToString
                    If String.IsNullOrEmpty(cname) = False Then
                        query.Remove(0, query.Length)
                        query.Append("alter table DOC_I")
                        query.Append(DocTypeId)
                        query.Append(" drop constraint ")
                        query.Append(cname)
                        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
                    End If
                End If
            Next
        Else
            query.Append("alter table DOC_I")
            query.Append(DocTypeId)
            query.Append(" MODIFY I")
            query.Append(IndexId)
            query.Append(" DEFAULT ")
            query.Append(" NULL")
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)

        End If

        'GUARDA UN REGISTRO EN INDEX_R_DOC_TYPE
        query.Remove(0, query.Length)
        query.Append("UPDATE INDEX_R_DOC_TYPE")
        query.Append(" SET  DefaultValue= ")
        query.Append(" NULL ")
        query.Append(" WHERE INDEX_ID = ")
        query.Append(IndexId)
        query.Append(" AND DOC_TYPE_ID= ")
        query.Append(DocTypeId)
        Server.Con.ExecuteNonQuery(CommandType.Text, query.ToString)
    End Sub

    Public Shared Sub delindexitems(ByVal IndexId As Int32, ByVal IndexList As ArrayList)
        Dim Cr As CreateTables = Server.CreateTables

        Try
            Cr.DelIndexItems(IndexId, IndexList)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Cr.Dispose()
            Cr = Nothing
        End Try
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo que chequea si un indice de sustitucion o de busqueda permite insertar datos que no se encuentren en la lista de valores
    ''' </summary>
    ''' <param name="IndexId">ID del indice a comprobar</param>
    ''' <history>
    ''' 	[AlejandroR]    14/03/2011  Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function CheckIfAllowDataOutOfList(ByVal IndexID As Long) As Boolean
        If Server.isOracle Then
            Return True
            'Return Server.Con.ExecuteScalar(CommandType.Text, String.Format("SELECT NVL(ALLOWDATAOUTOFLIST, 0) As ALLOWDATAOUTOFLIST FROM DOC_INDEX WHERE INDEX_ID = {0}", IndexID))
        Else
            Return Server.Con.ExecuteScalar(CommandType.Text, String.Format("SELECT IsNull(ALLOWDATAOUTOFLIST, 0) As ALLOWDATAOUTOFLIST FROM DOC_INDEX WHERE INDEX_ID = {0}", IndexID))
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Crea una tabla de sustitucion asociada a un Atributo
    ''' </summary>
    ''' <param name="IndexId">ID del indice al que se le asignará la tabla de sustitucion</param>
    ''' <remarks>
    ''' Tabla de Codigo y descripcion
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub createsustituciontable(ByVal IndexId As Int32, ByVal IndexLen As Int32, ByVal IndexType As IndexDataType)
        Dim Cr As CreateTables = Server.CreateTables
        Try
            Cr.CreateSustitucionTable(IndexId, IndexLen, IndexType)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Cr.Dispose()
            Cr = Nothing
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Destruye la tabla de sustitución asociada a un indice
    ''' </summary>
    ''' <param name="IndexId">Id del Atributo</param>
    ''' <remarks>
    ''' Se elimina la tabla y su contenido
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DeleteSustituciontable(ByVal IndexId As Int32)
        Dim Cr As CreateTables = Server.CreateTables
        Try
            Cr.BorrarSustitucionTable(IndexId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Cr.Dispose()
            Cr = Nothing
        End Try
    End Sub
    ''' <summary>
    ''' Borro la stls_t
    ''' </summary>
    ''' <param name="tabla"></param>
    ''' <history>Marcelo modified 07/10/08</history>
    ''' <remarks></remarks>
    Public Shared Sub DropTable(ByVal tabla As String)
        Dim Cr As CreateTables = Server.CreateTables
        Try
            Cr.DeleteTable(tabla)
        Catch ex As SqlClient.SqlException
            Try
                If ex.Number = 3705 Then
                    Dim strSelect As String = "Drop view " & tabla
                    Server.Con.ExecuteNonQuery(CommandType.Text, strSelect)
                End If
            Catch ex2 As Exception
                ZClass.raiseerror(ex2)
            End Try
        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            Cr.Dispose()
            Cr = Nothing
        End Try
    End Sub

    Public Shared Function IsDuplicated(ByVal IndexName As String, ByVal IndexID As Int32) As Boolean
        Dim cant As Int16
        If Server.isOracle Then
            Dim sql As String = "Select count(1) from Doc_index where Index_name='" & IndexName & "' and Index_id <>" & IndexID
            cant = Server.Con.ExecuteScalar(CommandType.Text, sql)
        Else
            Dim parValues() As Object = {IndexName, IndexID}
            Try
                cant = Server.Con.ExecuteScalar("zsp_index_100_GetIndexQtyByNameId", parValues)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End If
        If cant > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function LoadIndexFindTypeValues(ByVal IndexId As Int64) As DataTable
        Dim query As New StringBuilder()
        query.Append("Select ITEM from ILST_I")
        query.Append(IndexId)
        Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString).Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene si un indice esta declarado como referencial o no
    ''' </summary>
    ''' <param name="docTypeID"></param>
    ''' <param name="indexID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getReferenceStatus(ByVal docTypeID As Int64, ByVal indexID As Int64) As Boolean
        Return Server.Con.ExecuteScalar(CommandType.Text, "select isreferenced from index_r_doc_type where index_id = " & indexID & " and doc_type_id=" & docTypeID)
    End Function


    ''' <summary>
    ''' Obtiene los valores por defecto de los atributos atachados al entidad
    ''' </summary>
    ''' <param name="doctypeid">Id de entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[Diego] created 4-07-2008</history>
    Public Shared Function GetIndexDefaultValues(ByVal doctypeid As Int64, ByVal indexsId As List(Of Int64)) As Dictionary(Of Int64, String)
        Dim str As New StringBuilder
        Dim DefaultValues As New Dictionary(Of Int64, String)
        For Each indexid As Int64 In indexsId
            str.Remove(0, str.Length)
            If Server.isSQLServer Then

                str.Append("select text from syscomments where id = (select cdefault from syscolumns where id = (")
                str.Append("select id from sysobjects where name Like 'DOC_I")
                str.Append(doctypeid)
                str.Append("')")
                str.Append(" and name = 'I")
                str.Append(indexid)
                str.Append("')")
                Dim objectvalue As Object = Nothing
                objectvalue = Server.Con.ExecuteScalar(CommandType.Text, str.ToString())
                Dim _isdbnull As Boolean = False
                If IsDBNull(objectvalue) Or IsNothing(objectvalue) Then _isdbnull = True

                If Not _isdbnull Then
                    Dim cDataDefault As String = objectvalue
                    cDataDefault = cDataDefault.Remove(0, 1)
                    cDataDefault = cDataDefault.Remove(cDataDefault.Length - 1, 1)
                    If cDataDefault.StartsWith("'") AndAlso cDataDefault.Length > 2 Then cDataDefault = cDataDefault.Remove(0, 1)
                    If cDataDefault.EndsWith("'") AndAlso cDataDefault.Length > 1 Then cDataDefault = cDataDefault.Remove(cDataDefault.Length - 1, 1)
                    If cDataDefault.ToUpper.Contains("CONVERT") AndAlso cDataDefault.ToUpper.Contains("DATETIME") Then
                        cDataDefault = Server.Con.ExecuteScalar(CommandType.Text, "SELECT " & cDataDefault.ToString)
                    End If
                    DefaultValues.Add(indexid, cDataDefault)
                Else
                    'DBNULL
                    DefaultValues.Add(indexid, String.Empty)
                End If
            Else
                str.Append("select DATA_DEFAULT from all_tab_columns where COLUMN_NAME = 'I")
                str.Append(indexid)
                str.Append("' and TABLE_NAME='DOC_I")
                str.Append(doctypeid)
                str.Append("' and owner = '")
                str.Append(Server.dbOwner.ToUpper)
                str.Append("'")

                If Not IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, str.ToString())) Then
                    Dim strvalue As String = Server.Con.ExecuteScalar(CommandType.Text, str.ToString())
                    If Not String.IsNullOrEmpty(strvalue) Then
                        If strvalue.ToUpper.StartsWith("TO_DATE") Then
                            strvalue = Server.Con.ExecuteScalar(CommandType.Text, "SELECT " & strvalue & " From DUAL")
                        End If
                        strvalue = strvalue.Replace("'", String.Empty)
                        DefaultValues.Add(indexid, strvalue)
                    Else
                        DefaultValues.Add(indexid, String.Empty)
                    End If
                End If
            End If
        Next
        Return DefaultValues
    End Function

    ''' <summary>
    ''' Método que sirve para verificar si el Atributo es un Atributo referenciado o no
    ''' </summary>
    ''' <param name="indexId"></param>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	18/12/2008	Created
    ''' </history>
    Public Shared Function verifyIfAIndexReferenced(ByVal indexId As Int64, ByVal docTypeId As Long) As Boolean

        Dim query As String = "SELECT IsReferenced FROM INDEX_R_DOC_TYPE Where INDEX_ID = " & indexId & " AND DOC_TYPE_ID = " & docTypeId
        Dim result As Integer = Server.Con.ExecuteScalar(CommandType.Text, query)

        If (result = 0) Then
            Return (False)
        Else
            Return (True)
        End If

    End Function

    ''' <summary>
    ''' sumar uno:
    ''' Devuelve el mayor id de la doc. Luego se usa para insertarlo como valor por defecto en el atributo.
    ''' [sebastian 27/01/2009]
    ''' </summary>
    ''' <history>Marcelo Modified 20/09/2009</history>
    ''' <param name="IndexId"></param>
    ''' <param name="DocTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SelectMaxIndexValue(ByVal IndexId As Int64, ByVal DocTypeId As Int64) As Int64
        Dim IndexValue As Int64 = 0
        Dim DSTEMP As New DataSet

        If Server.isOracle Then
            Dim parValues() As Object = {IndexId, DocTypeId, 2}
            Try
                DSTEMP = Server.Con.ExecuteDataset("zsp_index_incremental_100.GetAndSetLastId", parValues)
                IndexValue = Int64.Parse(DSTEMP.Tables(0).Rows(0).Item(0).ToString())
            Catch ex As Exception
                System.Threading.Thread.Sleep(500)
                DSTEMP = Server.Con.ExecuteDataset("zsp_index_incremental_100.GetAndSetLastId", parValues)
                If DSTEMP.Tables(0).Rows.Count > 0 Then
                    IndexValue = Int64.Parse(DSTEMP.Tables(0).Rows(0).Item(0).ToString())
                Else
                    IndexValue = 0
                End If
            End Try
        Else
            Dim parameters() As Object = {IndexId, DocTypeId}
            Try
                IndexValue = Server.Con.ExecuteScalar("zsp_index_incremental_100_GetAndSetLastId", parameters)
            Catch ex As Exception
                System.Threading.Thread.Sleep(500)
                IndexValue = Server.Con.ExecuteScalar("zsp_index_incremental_100_GetAndSetLastId", parameters)
            End Try
        End If

        'Mantengo la funcionalidad anterior
        If IndexValue = 0 Then
            Dim query As String = "SELECT MAX(I" & IndexId & ") from doc" & DocTypeId
            DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, query)
            If DSTEMP.Tables(0).Rows.Count = 0 Or IsDBNull(DSTEMP.Tables(0).Rows(0).Item(0)) Then
                Return 1
            Else
                IndexValue = Int64.Parse(DSTEMP.Tables(0).Rows(0).Item(0).ToString())
                'obtengo el mayor valor y le sumo uno
                IndexValue = IndexValue + 1
            End If
        End If

        Return IndexValue
    End Function

    ''' <summary>
    ''' Arma los filtros por atributos específicos
    ''' </summary>
    ''' <param name="query">Consulta que obtendrá el resultado del filtrado</param>
    ''' <returns>Valores filtrados separados</returns>
    ''' <history>
    '''     [Tomas] 03/03/2010  Created
    ''' </history>
    Public Shared Function GetIndexFilterText(ByVal query As String) As String
        'Se obtienen los filtros
        Dim dtFilters As DataTable = Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)

        If dtFilters.Rows.Count > 0 Then
            'Se arma el filtro con os valores obtenidos
            Dim filters As New StringBuilder

            'Verifica si la columna es de tipo String.
            If String.Compare(dtFilters.Columns(0).DataType.ToString, "System.String") = 0 Then
                For Each row As DataRow In dtFilters.Rows
                    filters.Append("'")
                    filters.Append(row(0).ToString)
                    filters.Append("',")
                Next
                'Se remueve la última comilla
                filters.Remove(filters.Length - 1, 1)

                dtFilters.Dispose()
                dtFilters = Nothing
                Return filters.ToString

            Else
                For Each row As DataRow In dtFilters.Rows
                    filters.Append(row(0).ToString)
                    filters.Append(",")
                Next
                'Se remueve la última comilla
                filters.Remove(filters.Length - 1, 1)

                dtFilters.Dispose()
                dtFilters = Nothing
                Return filters.ToString
            End If

        Else
            'Si no hay filtros se devuelve vacio
            dtFilters.Dispose()
            dtFilters = Nothing
            Return String.Empty
        End If
    End Function

    Private Shared Function GetColumnType(ByVal TableName As String, ByVal ColumnName As String) As String
        Try
            Dim sbSQL As New StringBuilder()

            sbSQL.Append("select data_type as DataType, DATA_LENGTH as MaximumLength, ")
            If Server.isSQLServer Then
                sbSQL.Append(" ISNULL(DATA_PRECISION,0) as Precision, ISNULL(DATA_SCALE,0) as Scale ")
                sbSQL.Append("from information_schema.columns where table_name = '")
            Else
                sbSQL.Append(" NVL(DATA_PRECISION,0) as Precision, NVL(DATA_SCALE,0) as Scale ")
                sbSQL.Append("from user_tab_cols where table_name = '")
            End If

            sbSQL.Append(TableName)
            sbSQL.Append("' and lower (column_name) = lower ('")
            sbSQL.Append(ColumnName & "')")

            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sbSQL.ToString())

            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 AndAlso
                Not ds.Tables(0) Is Nothing Then

                Dim RowDataType As DataRow = ds.Tables(0).Rows(0)

                If Server.isSQLServer Then
                    If RowDataType("DataType") = "char" OrElse RowDataType("DataType") = "varchar" _
                    OrElse RowDataType("DataType") = "nchar" OrElse RowDataType("DataType") = "nvarchar" Then
                        Return RowDataType("DataType") & "(" & RowDataType("DATA_LENGTH") & ")"
                    End If

                    If RowDataType("DataType") = "numeric" OrElse RowDataType("DataType") = "decimal" Then
                        Return RowDataType("DataType") & "(" & RowDataType("DATA_LENGTH") & "," & RowDataType("Scale") & ")"
                    End If

                    If RowDataType("DataType") = "int" Then
                        Return RowDataType("DataType")
                    End If
                Else
                    If String.Compare(RowDataType("DataType"), "char", False) = 0 OrElse String.Compare(RowDataType("DataType"), "varchar", False) = 0 _
                    OrElse String.Compare(RowDataType("DataType"), "nchar", False) = 0 OrElse String.Compare(RowDataType("DataType") = "varchar2", False) = 0 Then
                        Return RowDataType("DataType") & "(" & RowDataType("MaximumLength") & ")"
                    End If

                    If String.Compare(RowDataType("DataType"), "number", False) = 0 OrElse String.Compare(RowDataType("DataType"), "decimal", False) = 0 Then
                        Return RowDataType("DataType") & "(" & If(RowDataType("Precision") = 0, RowDataType("MaximumLength"), RowDataType("Precision")) & "," & RowDataType("Scale") & ")"
                    End If

                    If String.Compare(RowDataType("DataType"), "int", False) = 0 Then
                        Return RowDataType("DataType")
                    End If

                End If
            End If

                Return String.Empty

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Obtiene toda la tabla con las relaciones padre-hijo de los atributos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetHierarchicalRelations() As DataTable
        Dim ds As DataSet
        ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT IndicePadre, Indice FROM ZIndexHierarchyKey ORDER BY IndicePadre")

        If ds Is Nothing OrElse ds.Tables.Count = 0 Then
            Return Nothing
        Else
            Return ds.Tables(0)
        End If
    End Function
End Class