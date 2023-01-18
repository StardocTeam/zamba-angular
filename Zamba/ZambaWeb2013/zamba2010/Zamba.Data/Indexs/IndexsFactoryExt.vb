Imports Zamba.Servers
Imports Zamba.Core
Imports System.Text

Public Class IndexsFactoryExt
    ''' <summary>
    ''' Obtiene los datos de la tabla
    ''' </summary>
    ''' <param name="IndexTableName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetHierarchicalTable(ByVal IndexTableName As String, ByVal parentTableName As String, ByVal childTableName As String) As DataTable
        Try
            Dim sbSQL As New StringBuilder()

            If Server.isOracle Then
                sbSQL.Append("SELECT ParentValue as ""Valor Padre"",")
                sbSQL.Append(parentTableName)

                If parentTableName.Contains("ilst") Then
                    sbSQL.Append(".item")
                Else
                    sbSQL.Append(".descripcion")
                End If

                sbSQL.Append(" as ""Descripcion Padre"", ChildValue as ""Valor Hijo"", ")
                sbSQL.Append(childTableName)

                If childTableName.Contains("ilst") Then
                    sbSQL.Append(".item")
                Else
                    sbSQL.Append(".descripcion")
                End If

                sbSQL.Append(" as ""Descripcion Hijo"" FROM ")
                sbSQL.Append(IndexTableName)

                sbSQL.Append(" inner join ")
                sbSQL.Append(parentTableName)
                sbSQL.Append(" on ")
                sbSQL.Append(parentTableName)

                If parentTableName.Contains("ilst") Then
                    sbSQL.Append(".item")
                Else
                    sbSQL.Append(".codigo")
                End If

                sbSQL.Append(" = ParentValue")

                sbSQL.Append(" inner join ")
                sbSQL.Append(childTableName)
                sbSQL.Append(" on ")
                sbSQL.Append(childTableName)

                If childTableName.Contains("ilst") Then
                    sbSQL.Append(".item")
                Else
                    sbSQL.Append(".codigo")
                End If

                sbSQL.Append(" = ChildValue")

            Else
                sbSQL.Append("SELECT ParentValue 'Valor Padre',")
                sbSQL.Append(parentTableName)

                If parentTableName.Contains("ilst") Then
                    sbSQL.Append(".item")
                Else
                    sbSQL.Append(".descripcion")
                End If

                sbSQL.Append(" as 'Descripcion Padre', ChildValue 'Valor Hijo', ")
                sbSQL.Append(childTableName)

                If childTableName.Contains("ilst") Then
                    sbSQL.Append(".item")
                Else
                    sbSQL.Append(".descripcion")
                End If

                sbSQL.Append(" as 'Descripcion Hijo' FROM ")
                sbSQL.Append(IndexTableName)

                sbSQL.Append(" inner join ")
                sbSQL.Append(parentTableName)
                sbSQL.Append(" on ")
                sbSQL.Append(parentTableName)

                If parentTableName.Contains("ilst") Then
                    sbSQL.Append(".item")
                Else
                    sbSQL.Append(".codigo")
                End If

                sbSQL.Append(" = ParentValue")

                sbSQL.Append(" inner join ")
                sbSQL.Append(childTableName)
                sbSQL.Append(" on ")
                sbSQL.Append(childTableName)

                If childTableName.Contains("ilst") Then
                    sbSQL.Append(".item")
                Else
                    sbSQL.Append(".codigo")
                End If

                sbSQL.Append(" = ChildValue")
            End If


            Dim dtToReturn As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sbSQL.ToString())

            If Not dtToReturn Is Nothing AndAlso dtToReturn.Tables.Count > 0 _
                AndAlso Not dtToReturn.Tables(0) Is Nothing Then
                Return dtToReturn.Tables(0)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Public Shared Function GetHierarchicalTableByValue(ByVal IndexTableName As String,
                                                       ByVal ParentValue As String) As DataTable
        Try
            Dim sbSQL As New StringBuilder()
            Dim parentColumnDatatype As String = GetColumnType(IndexTableName, "ParentValue")

            If Server.isOracle Then
                sbSQL.Append("SELECT ChildValue as ""Value"" FROM ")
            Else
                sbSQL.Append("SELECT ChildValue as 'Value' FROM ")
            End If


            sbSQL.Append(IndexTableName)
            sbSQL.Append(" WHERE ParentValue = ")

            sbSQL.Append(GetColumnToWhere(parentColumnDatatype, ParentValue))

            Dim dtToReturn As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sbSQL.ToString())

            If Not dtToReturn Is Nothing AndAlso dtToReturn.Tables.Count > 0 _
                AndAlso Not dtToReturn.Tables(0) Is Nothing Then
                Return dtToReturn.Tables(0)
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Public Shared Function ValidateHierarchyValue(ByVal ValueToValidate As String, ByVal IndexTableName As String,
                                                  ByVal ParentValue As String) As Boolean
        Try
            Dim sbSQL As New StringBuilder()
            Dim parentColumnDatatype As String = GetColumnType(IndexTableName, "ParentValue")
            Dim childColumnDatatype As String = GetColumnType(IndexTableName, "ChildValue")

            sbSQL.Append("SELECT COUNT(1) FROM ")
            sbSQL.Append(IndexTableName)
            sbSQL.Append(" WHERE ParentValue = ")

            sbSQL.Append(GetColumnToWhere(parentColumnDatatype, ParentValue))

            sbSQL.Append(" and ChildValue = ")

            sbSQL.Append(GetColumnToWhere(childColumnDatatype, ValueToValidate))

            Return Server.Con.ExecuteScalar(CommandType.Text, sbSQL.ToString) > 0
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Private Shared Function GetColumnType(ByVal TableName As String, ByVal ColumnName As String) As String
        Try
            Dim sbSQL As New StringBuilder()

            If Server.isSQLServer Then

                sbSQL.Append("select data_type 'DataType', character_maximum_length 'MaximumLength', numeric_precision 'Precision', numeric_scale 'Scale'  ")
                sbSQL.Append("from information_schema.columns where table_name = '")

                sbSQL.Append(TableName)
                sbSQL.Append("' and column_name = '")
                sbSQL.Append(ColumnName & "'")

            Else

                sbSQL.Append("select data_type as DataType, data_length as MaximumLength, data_precision as Precision, data_scale as Scale ")
                sbSQL.Append("from user_tab_cols where table_name = '")
                sbSQL.Append(TableName)
                sbSQL.Append("' and column_name = '")
                sbSQL.Append(ColumnName & "'")
            End If

            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sbSQL.ToString())

            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 AndAlso
                Not ds.Tables(0) Is Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then

                Dim RowDataType As DataRow = ds.Tables(0).Rows(0)

                If RowDataType("DataType") = "char" OrElse RowDataType("DataType") = "varchar" _
                    OrElse RowDataType("DataType") = "nchar" OrElse RowDataType("DataType") = "nvarchar" Then
                    Return RowDataType("DataType") & "(" & RowDataType("MaximumLength") & ")"
                Else
                    If RowDataType("DataType") = "numeric" OrElse RowDataType("DataType") = "decimal" Then
                        Return RowDataType("DataType") & "(" & RowDataType("Precision") & "," & RowDataType("Scale") & ")"
                    Else
                        Return "int"
                    End If
                End If

            End If

            Return String.Empty

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un Dataset tipeado con todos los datos de Doc_index,
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    '''     [Javier]    15/10/2012  Created
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

    ''' <summary>
    ''' Obtiene los hijos de un atributo
    ''' </summary>
    ''' <param name="indexID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetIndexChilds(ByVal indexID As Long) As DataTable
        Dim ds As DataSet

        ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT Indice FROM ZIndexHierarchyKey WHERE IndicePadre = " & indexID)

        If ds Is Nothing OrElse ds.Tables.Count = 0 Then
            Return Nothing
        Else
            Return ds.Tables(0)
        End If

        If ds Is Nothing OrElse ds.Tables.Count = 0 Then
            Return Nothing
        Else
            Return ds.Tables(0)
        End If
    End Function

    Public Shared Function GetColumnToWhere(ByVal columnType As String, ByVal columnValue As String) As String
        Dim sb As New StringBuilder

        If columnType.Contains("char") OrElse columnType.Contains("varchar") _
             OrElse columnType.Contains("nchar") OrElse columnType.Contains("nvarchar") Then
            If String.IsNullOrEmpty(columnValue) Then
                sb.Append("''")
            Else
                sb.Append("'")
                sb.Append(columnValue)
                sb.Append("'")
            End If
        ElseIf String.IsNullOrEmpty(columnValue) Then
            sb.Append("0")
        Else
            sb.Append(columnValue)
        End If

        Return sb.ToString()
    End Function

    Public Shared Function getAllindexByDocTypeID(ByVal docTypeID As Long) As DataTable
        Dim query As New StringBuilder
        query.Append("select doc_index.* from doc_index inner join index_r_doc_type on ")
        query.Append("doc_index.index_id = index_r_doc_type.index_id where index_r_doc_type.doc_type_id = ")
        query.Append(docTypeID.ToString)
        Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString).Tables(0)
    End Function

    Public Shared Function GetIlst(ByVal indexid As Long) As DataTable
        Dim query As New StringBuilder
        query.Append("select * from ilst_i")

        query.Append(indexid.ToString)
        Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString).Tables(0)
    End Function

    Public Shared Function GetSlst(ByVal indexid As Long) As DataTable
        Dim query As New StringBuilder
        query.Append("select * from Slst_s")

        query.Append(indexid.ToString)
        Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString).Tables(0)
    End Function

    Public Shared Function GetSchemeIlst(ByVal indexid As Long) As DataTable
        Dim StrSelect As String = "EXEC sp_help 'Ilst_I" + indexid.ToString + "'"
        Return Server.Con.ExecuteDataset(CommandType.Text, StrSelect).Tables(1)
    End Function

    Public Shared Function GetSchemeSlst(ByVal indexid As Long) As DataTable
        Dim StrSelect As String = "EXEC sp_help 'Slst_s" + indexid.ToString + "'"
        Return Server.Con.ExecuteDataset(CommandType.Text, StrSelect).Tables(1)
    End Function

    Shared Sub UpdateMinValue(ByVal doctypeid As Long, ByVal indexid As Long, ByVal minValue As String)
        Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE index_r_doc_type SET minvalue ='" & minValue & "' where doc_type_id = " & doctypeid & " and index_id=" & indexid)
    End Sub

    Shared Sub UpdateMaxValue(ByVal doctypeid As Long, ByVal indexid As Long, ByVal maxValue As String)
        Server.Con.ExecuteNonQuery(CommandType.Text, "UPDATE index_r_doc_type SET maxvalue ='" & maxValue & "' where doc_type_id = " & doctypeid & " and index_id=" & indexid)
    End Sub

    ''' <summary>
    ''' Obtiene un datatable con el id y nombre de los atributos de una entidad
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetIndexIdAndName(Optional docTypeId As Int64 = 0) As DataTable
        Dim strselect As String
        If docTypeId = 0 Then
            strselect = "Select Index_Id, Index_Name from Doc_Index order by 1"
        Else
            strselect = "Select i.Index_Id, i.Index_Name from Doc_Index i inner join index_r_doc_type r on i.index_id=r.index_id where r.doc_type_id=" & docTypeId & " order by i.Index_Name"
        End If

        Return Server.Con.ExecuteDataset(CommandType.Text, strselect).Tables(0)
    End Function

    Public Function GetAsignedEntities(ByVal indexId As Int64) As DataTable
        Dim query As String
        If Server.isOracle Then
            query = "SELECT trim(DT.DOC_TYPE_NAME) || ' (' || DT.DOC_TYPE_ID || ')' AS ENTITY FROM DOC_TYPE DT INNER JOIN INDEX_R_DOC_TYPE IRD ON DT.DOC_TYPE_ID=IRD.DOC_TYPE_ID WHERE IRD.INDEX_ID=" & indexId & " ORDER BY DT.DOC_TYPE_NAME"
        Else
            query = "SELECT RTRIM(DT.DOC_TYPE_NAME) + ' (' + CAST(DT.DOC_TYPE_ID AS VARCHAR) + ')' AS ENTITY FROM DOC_TYPE DT INNER JOIN INDEX_R_DOC_TYPE IRD ON DT.DOC_TYPE_ID=IRD.DOC_TYPE_ID WHERE IRD.INDEX_ID=" & indexId & " ORDER BY DT.DOC_TYPE_NAME"
        End If
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function
End Class
