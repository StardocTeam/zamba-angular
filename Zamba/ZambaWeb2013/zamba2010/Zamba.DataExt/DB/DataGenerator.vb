Imports Zamba.Servers
Imports System.Text
Imports Zamba.Core.DataGeneratorHelper

Public Class DataGenerator
    Dim queryStr As String

    ''' <summary>
    ''' Actualiza un campo de un registro de una tabla
    ''' </summary>
    ''' <param name="table">Tabla destino</param>
    ''' <param name="setColumns">Una columna para actualizar</param>
    ''' <param name="setValues">Un valor para actualizar</param>
    ''' <param name="whereColumnName">Nombre de una ID</param>
    ''' <param name="whereValue">Valor por el cual filtrar</param>
    ''' <remarks></remarks>
    Public Sub UpdateRow(table As String, _
                         setColumn As String, _
                         setValue As String, _
                         whereColumnName As String, _
                         whereValue As String)

        queryStr = "update "
        queryStr &= table
        queryStr &= " set ["
        queryStr &= setColumn
        queryStr &= "]='"
        queryStr &= setValue.Replace("'", "''")
        queryStr &= "' where ["
        queryStr &= whereColumnName
        queryStr &= "]= '"
        queryStr &= whereValue.Replace("'", "''")
        queryStr &= "' and ["
        queryStr &= setColumn
        queryStr &= "] is not null"

        Server.Con.ExecuteScalar(CommandType.Text, queryStr)

    End Sub

    ''' <summary>
    ''' Actualiza uno o varios campos de un mismo registro de una tabla
    ''' </summary>
    ''' <param name="table">Tabla destino</param>
    ''' <param name="setColumns">Una o multiples columnas para actualizar</param>
    ''' <param name="setValues">Uno o múltiples valores a actualizar</param>
    ''' <param name="whereColumnName">Nombre de una ID</param>
    ''' <param name="whereValue">Valor por el cual filtrar</param>
    ''' <remarks>La cantidad de registros en setColumns debe ser igual a la cantidad de registros de setValues.</remarks>
    Public Sub UpdateRow(table As String, _
                         setColumns As List(Of ColumnHelper), _
                         setValues As List(Of String), _
                         whereColumnName As String, _
                         whereValue As String)

        Dim query As New StringBuilder
        query.Append("update ")
        query.Append(table)
        query.Append(" set [")

        For i As Int32 = 0 To setColumns.Count - 1
            query.Append(setColumns(i).ColumnName)
            query.Append("]='")
            query.Append(setValues(i).Replace("'", "''"))
            query.Append("',[")
        Next
        query = query.Remove(query.Length - 2, 2)

        query.Append(" where ")
        query.Append(whereColumnName)
        query.Append(" = '")
        query.Append(whereValue.Replace("'", "''"))
        query.Append("'")

        Server.Con.ExecuteScalar(CommandType.Text, query.ToString)

    End Sub

    ''' <summary>
    ''' Obtiene los ID por los que luego utilizará para actualizar los datos generados
    ''' </summary>
    ''' <param name="table">Tabla destino</param>
    ''' <param name="idColumnName">Nombre de la columna que contiene el campo identificador</param>
    ''' <returns>Lista con los IDs</returns>
    ''' <remarks></remarks>
    Public Function GetIds(table As String, _
                       idColumnName As String) As DataTable
        queryStr = "select " & idColumnName & " from " & table
        Return Server.Con.ExecuteDataset(CommandType.Text, queryStr).Tables(0)

    End Function

    ''' <summary>
    ''' Obtiene una previsualización de la tabla deseada
    ''' </summary>
    ''' <param name="table">Tabla a previsualizar</param>
    ''' <param name="filterTop100">True para obtener los primeros 100 registros</param>
    ''' <param name="attributes"></param>
    ''' <returns>Datatable</returns>
    ''' <remarks></remarks>
    Public Function GetTablePreview(ByVal table As String, _
                                    ByVal filterTop100 As Boolean, _
                                    ByVal attributes As String) As DataTable
        queryStr = "select "
        If filterTop100 Then
            queryStr &= " top 100 "
        End If
        If attributes.Length = 0 Then
            attributes = "*"
        Else
            attributes = "DOC_ID," & attributes
        End If
        queryStr &= attributes & " from [" & table & "]"

        'Se obtienen los datos y se cargan en la grilla
        Return Server.Con.ExecuteDataset(CommandType.Text, queryStr).Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene una tabla con los IDS de las entidades que tienen agregados al menos uno de los atributos pasados por parámetro
    ''' </summary>
    ''' <param name="lstAttributes">Lista con los ids de los atributos</param>
    ''' <returns>DataTable con los ids de las entidades</returns>
    ''' <remarks></remarks>
    Public Function GetLinkedDocI(ByVal lstAttributes As List(Of Int64)) As DataTable
        Dim sbQuery As New StringBuilder
        sbQuery.Append("select distinct('DOC_I' + cast(d.doc_type_id as varchar)) AS 'NAME' ")
        sbQuery.Append("from index_r_doc_type r ")
        sbQuery.Append("inner join doc_type d on d.doc_type_id = r.doc_type_id ")
        sbQuery.Append("where r.index_id in(")
        For i As Int32 = 0 To lstAttributes.Count - 1
            sbQuery.Append(lstAttributes(i))
            sbQuery.Append(",")
        Next
        sbQuery = sbQuery.Remove(sbQuery.Length - 1, 1).Append(")")

        Return Server.Con.ExecuteDataset(CommandType.Text, sbQuery.ToString).Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene una tabla con los IDS de las entidades que tienen agregados al menos uno de los atributos pasados por parámetro
    ''' </summary>
    ''' <param name="lstAttributes">Lista con los ids de los atributos</param>
    ''' <returns>DataTable con los ids de las entidades</returns>
    ''' <remarks></remarks>
    Public Function GetLinkedSlstAndIlst(ByVal lstAttributes As List(Of Int64)) As DataTable
        Dim sbQuery As New StringBuilder
        sbQuery.Append("select INDEX_ID as ID, case dropdown when 1 then 'ILST_I' else 'SLST_S' end + CAST(INDEX_ID as varchar) as NAME ")
        sbQuery.Append("from doc_index where DROPDOWN in (1,2) and index_id in (")
        For i As Int32 = 0 To lstAttributes.Count - 1
            sbQuery.Append(lstAttributes(i))
            sbQuery.Append(",")
        Next
        sbQuery = sbQuery.Remove(sbQuery.Length - 1, 1).Append(")")

        Return Server.Con.ExecuteDataset(CommandType.Text, sbQuery.ToString).Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene solo los atributos agregados a entidades
    ''' </summary>
    ''' <param name="lstAttributes">Lista con los ids de los atributos</param>
    ''' <returns>DataTable con los ids de las entidades</returns>
    ''' <remarks></remarks>
    Public Function GetLinkedDocIAttributes(ByVal docTypeId As String, _
                                            ByVal lstAttributes As List(Of Int64)) As DataTable
        Dim sbQuery As New StringBuilder
        sbQuery.Append("select distinct('I' + cast(r.index_id as varchar)) AS 'NAME' ")
        sbQuery.Append("from index_r_doc_type r ")
        sbQuery.Append("inner join doc_type d on d.doc_type_id = r.doc_type_id ")
        sbQuery.Append("where d.doc_type_id=")
        sbQuery.Append(docTypeId)
        sbQuery.Append(" and r.index_id in(")
        For i As Int32 = 0 To lstAttributes.Count - 1
            sbQuery.Append(lstAttributes(i))
            sbQuery.Append(",")
        Next
        sbQuery = sbQuery.Remove(sbQuery.Length - 1, 1).Append(")")

        Return Server.Con.ExecuteDataset(CommandType.Text, sbQuery.ToString).Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene solo los atributos agregados a entidades
    ''' </summary>
    ''' <param name="lstAttributes">Lista con los ids de los atributos</param>
    ''' <returns>DataTable con los ids de las entidades</returns>
    ''' <remarks></remarks>
    Public Function GetLinkedDocIAttributes() As List(Of Int64)
        queryStr = "select distinct(i.index_id) AS 'index_id' "
        queryStr &= "from doc_index i "
        queryStr &= "inner join index_r_doc_type r on i.index_id=r.index_id "
        queryStr &= "inner join doc_type d on d.doc_type_id = r.doc_type_id"

        Dim dt As DataTable = Server.Con.ExecuteDataset(CommandType.Text, queryStr).Tables(0)
        Dim indexIds As New List(Of Int64)

        For i As Int32 = 0 To dt.Rows.Count - 1
            indexIds.Add(CLng(dt.Rows(i)(0)))
        Next

        dt.Dispose()
        dt = Nothing

        Return indexIds
    End Function

    ''' <summary>
    ''' Obtiene todos los nombres de las tablas de la base de datos excluyendo las tablas de SQL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllTableNames() As DataTable
        Dim query As String = "select name from sysobjects where xtype='U' and category=0 order by name"
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene todas las columnas de una tabla especifica
    ''' </summary>
    ''' <param name="table">Tabla</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllTableColumns(ByVal table As String) As DataTable
        Dim query As String = "select name from syscolumns where id in (select id from sysobjects where name='" & table & "' and xtype='u')"
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

End Class
