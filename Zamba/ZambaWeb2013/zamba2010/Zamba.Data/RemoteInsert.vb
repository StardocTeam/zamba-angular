Imports System.Text
Imports System.Collections.Generic
Imports System.Data
Imports Zamba.Core

Public NotInheritable Class RemoteInsertFactory

    Public Function GetDocumentsToInsert(ByVal WorkItem As Int64) As DataTable
        Dim Query As New StringBuilder()
        Query.Append("SELECT RemoteInsert.TemporaryID, RemoteInsert.DocumentName, RemoteInsert.DocTypeId,  ")
        Query.Append("RemoteInsert.SerializedFile,RemoteInsert.FileExtension, RemoteInsert.Information ")
        Query.Append("FROM RemoteInsert inner join remoteinsert_relations on remoteinsert.doctypeid = remoteinsert_relations.doctypeid WHERE RemoteInsert.Status = ")
        Query.Append(WorkItem.ToString())
        Query.Append(" order by position ")

        Dim DtDocumentsToInsert As DataTable = Nothing
        Using Ds As DataSet = CurrentConnection.ExecuteDataset(CommandType.Text, Query.ToString)
            If Not IsNothing(Ds) AndAlso Ds.Tables.Count = 1 Then
                DtDocumentsToInsert = Ds.Tables(0)
            End If
        End Using

        Return DtDocumentsToInsert
    End Function

    ''' <summary>
    ''' Obtiene los indices
    ''' </summary>
    ''' <param name="intTemporaryID">Id del documento temporal</param>
    ''' <history>Marcelo Created 16/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexToRemoteInsert(ByVal intTemporaryID As Int64) As DataTable
        Dim Query As String = "select IndexId, IndexValue from DocumentsIndexs Where Id = " & intTemporaryID

        Dim Ds As DataSet = CurrentConnection.ExecuteDataset(CommandType.Text, Query)
        If Not IsNothing(Ds) AndAlso Ds.Tables.Count = 1 Then
            Return Ds.Tables(0)
        End If

        Return Nothing
    End Function

    Public Sub RemoveDocumentsToInsert(ByVal ids As List(Of Int64))

        Dim QueryBuilder As New StringBuilder()

        QueryBuilder.Append("DELETE FROM DocumentsIndexs WHERE")

        For Each CurrentId As Int64 In ids
            QueryBuilder.Append(" ID=")
            QueryBuilder.Append(CurrentId.ToString())
            QueryBuilder.Append(" OR")
        Next

        QueryBuilder.Remove(QueryBuilder.Length - 3, 3)

        CurrentConnection.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)

        QueryBuilder.Append("DELETE FROM RemoteInsert WHERE")

        For Each CurrentId As Int64 In ids
            QueryBuilder.Append(" TemporaryId=")
            QueryBuilder.Append(CurrentId.ToString())
            QueryBuilder.Append(" OR")
        Next

        QueryBuilder.Remove(QueryBuilder.Length - 3, 3)

        CurrentConnection.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing
    End Sub

    ''' <summary>
    ''' Guarda el estado del documento y el nuevo Id
    ''' </summary>
    ''' <param name="temporaryId"></param>
    ''' <param name="statusId"></param>
    ''' <param name="ResultId"></param>
    ''' <remarks></remarks>
    Public Sub SaveDocumentStatus(ByVal temporaryId As Int64, ByVal statusId As Int32, ByVal ResultId As Int64)
        Dim Query As New StringBuilder()
        Query.Append("UPDATE RemoteInsert SET Status=")
        Query.Append(statusId.ToString())
        Query.Append(", DocumentId=")
        Query.Append(ResultId.ToString())
        Query.Append(" WHERE TemporaryID=")
        Query.Append(temporaryId.ToString())

        CurrentConnection.ExecuteNonQuery(CommandType.Text, Query.ToString())

        Query.Remove(0, Query.Length)
        Query = Nothing

    End Sub

    Dim CurrentConnection As IConnection = Servers.Server.Con(True, False, True)

    ''' <summary>
    ''' Martin: Metodo que reserva con un workitem mayor a 4, los registros para ser insertados por un thread
    ''' </summary>
    ''' <param name="WorkItem"></param>
    ''' <remarks></remarks>
    Public Function ReserveInsertsStatus(ByVal WorkItem As Int64) As Int64
        Try
            Dim Query1 As New StringBuilder()

            Query1.Append("UPDATE RemoteInsert SET Status = ")
            Query1.Append(WorkItem)
            Query1.Append(" where temporaryid in (select temporaryid from remoteinsert inner join remoteinsert_relations on remoteinsert.doctypeid = remoteinsert_relations.doctypeid inner join documentsindexs on remoteinsert.temporaryid = documentsindexs.id where(remoteinsert_relations.indexid = documentsindexs.indexid) and status = 3  and indexvalue in (select top 1  indexvalue from remoteinsert inner join remoteinsert_relations on remoteinsert.doctypeid = remoteinsert_relations.doctypeid inner join documentsindexs on remoteinsert.temporaryid = documentsindexs.id where(remoteinsert_relations.indexid = documentsindexs.indexid) and status = 3 order by indexvalue))")
            Dim RowsCount1 As Int64 = CurrentConnection.ExecuteNonQuery(CommandType.Text, Query1.ToString())
            Query1.Remove(0, Query1.Length)
            Query1 = Nothing

            If RowsCount1 > 0 Then
                Return RowsCount1
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)

        End Try

    End Function

    Public Sub SaveDocumentError(ByVal temporaryId As Int64, ByVal errorMessage As String, ByVal statusId As Int32)

        errorMessage = errorMessage.Replace("'", String.Empty) ' Remplazo los "'" de la exception porque rompen las consultas SQL

        Dim Query As New StringBuilder()
        Query.Append("UPDATE RemoteInsert SET Status=")
        Query.Append(statusId.ToString())
        Query.Append(", Information='")
        Query.Append(errorMessage)
        Query.Append("' WHERE TemporaryID = ")
        Query.Append(temporaryId.ToString())

        CurrentConnection.ExecuteNonQuery(CommandType.Text, Query.ToString())

        Query.Remove(0, Query.Length)
        Query = Nothing
    End Sub
End Class

Public NotInheritable Class RemoteUpdateFactory
    ''' <summary>
    ''' Obtiene los documentos a ser actualizados
    ''' </summary>
    ''' <history>Marcelo Created 15/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocumentsToRemoteUpdate(ByVal WorkItem As Int64) As DataTable

        Dim Query As String = "SELECT RemoteUpdate.TemporaryID, RemoteUpdate.DocTypeId FROM RemoteUpdate WHERE Status = " & WorkItem.ToString()

        Dim DtDocumentsToUpdate As DataTable = Nothing
        Using Ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Query)
            If Not IsNothing(Ds) AndAlso Ds.Tables.Count = 1 Then
                DtDocumentsToUpdate = Ds.Tables(0)
            End If
        End Using

        Return DtDocumentsToUpdate
    End Function

    ''' <summary>
    ''' Obtiene los indices claves
    ''' </summary>
    ''' <param name="intTemporaryID">Id del documento temporal</param>
    ''' <history>Marcelo Created 15/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexKeysToRemoteUpdate(ByVal intTemporaryID As Int64) As DataTable

        Dim Query As String = "select IndexId, IndexValue from DocumentKeyIndexs Where Id = " & intTemporaryID

        Dim Ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Query)
        If Not IsNothing(Ds) AndAlso Ds.Tables.Count = 1 Then
            Return Ds.Tables(0)
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene los indices claves
    ''' </summary>
    ''' <param name="intTemporaryID">Id del documento temporal</param>
    ''' <history>Marcelo Created 15/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexToRemoteUpdate(ByVal intTemporaryID As Int64) As DataTable
        Dim Query As String = "select IndexId, IndexValue from DocumentUpdateIndexs Where Id = " & intTemporaryID

        Dim Ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Query)
        If Not IsNothing(Ds) AndAlso Ds.Tables.Count = 1 Then
            Return Ds.Tables(0)
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Actualiza los valores de los documentos
    ''' </summary>
    ''' <param name="docTypeId">Id del entidad</param>
    ''' <param name="lstKeys">Listado de atributos clave (id del indice, valor a buscar)</param>
    ''' <param name="lstValues">Listado de atributos a actualizar (id del indice, valor del indice)</param>
    ''' <returns>Dataset de los indices actualizados</returns>
    ''' <remarks></remarks>
    Public Function UpdateDocuments(ByVal docTypeId As Int64, ByVal lstIndexKeys As Dictionary(Of Int64, String), ByVal lstIndexValues As Dictionary(Of Int64, String)) As DataSet
        Dim strbuilder As StringBuilder = New StringBuilder()
        Dim strWhereBuilder As StringBuilder = New StringBuilder()
        Dim blnAux As Boolean = False

        Try
            strWhereBuilder.Append(" where ")

            blnAux = False
            For Each kvpKey As KeyValuePair(Of Int64, String) In lstIndexKeys
                If blnAux = True Then
                    strWhereBuilder.Append(" and ")
                Else
                    blnAux = True
                End If
                strWhereBuilder.Append(" I")
                strWhereBuilder.Append(kvpKey.Key)
                strWhereBuilder.Append("='")
                strWhereBuilder.Append(kvpKey.Value)
                strWhereBuilder.Append("'")
            Next

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo Documentos a actualizar: Select doc_id from doc_i" & docTypeId & strWhereBuilder.ToString())

            Dim dsModifiedIndex As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "Select doc_id from doc_i" & docTypeId & strWhereBuilder.ToString())

            If Not IsNothing(dsModifiedIndex) AndAlso dsModifiedIndex.Tables.Count > 0 Then
                strbuilder.Append("Update doc_i")
                strbuilder.Append(docTypeId)
                strbuilder.Append(" set ")

                blnAux = False
                For Each kvpValue As KeyValuePair(Of Int64, String) In lstIndexValues
                    If blnAux = True Then
                        strbuilder.Append(", ")
                    Else
                        blnAux = True
                    End If
                    strbuilder.Append(" I")
                    strbuilder.Append(kvpValue.Key)
                    strbuilder.Append("='")
                    strbuilder.Append(kvpValue.Value)
                    strbuilder.Append("'")
                Next

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizando Documentos: " & strbuilder.ToString() & strWhereBuilder.ToString())
                Server.Con.ExecuteNonQuery(CommandType.Text, strbuilder.ToString() & strWhereBuilder.ToString())

                Return dsModifiedIndex
            End If
            Return Nothing
        Finally
            strbuilder = Nothing
            strWhereBuilder = Nothing
        End Try
    End Function

    Dim CurrentConnection As IConnection = Servers.Server.Con(True, False, True)


    ''' <summary>
    ''' Martin: Metodo que reserva con un workitem mayor a 4, los registros para ser insertados por un thread
    ''' </summary>
    ''' <param name="WorkItem"></param>
    ''' <remarks></remarks>
    Public Function ReserveUpdatesStatus(ByVal WorkItem As Int64) As Int64
        Dim Query As New StringBuilder()
        Query.Append("UPDATE RemoteUpdate SET Status = ")
        Query.Append(WorkItem)
        Query.Append(" WHERE Status = 3")
        Dim RowsCount As Int64 = CurrentConnection.ExecuteNonQuery(CommandType.Text, Query.ToString())
        Query.Remove(0, Query.Length)
        Query = Nothing

        Return RowsCount
    End Function

    ''' <summary>
    ''' Escribe el estado de la actualizacion
    ''' </summary>
    ''' <param name="temporaryId"></param>
    ''' <param name="statusId"></param>
    ''' <remarks></remarks>
    Public Sub SaveDocumentUpdated(ByVal temporaryId As Int64, ByVal statusId As Int32)
        Dim Query As New StringBuilder()
        Try
            Query.Append("UPDATE RemoteUpdate SET Status=")
            Query.Append(statusId.ToString())
            Query.Append(" WHERE TemporaryID=")
            Query.Append(temporaryId.ToString())

            Server.Con.ExecuteNonQuery(CommandType.Text, Query.ToString())

        Finally
            Query.Remove(0, Query.Length)
            Query = Nothing
        End Try
    End Sub

    Public Sub SaveDocumentUpdateError(ByVal temporaryId As Int64, ByVal errorMessage As String, ByVal statusId As Int32)

        errorMessage = errorMessage.Replace("'", String.Empty) ' Remplazo los "'" de la exception porque rompen las consultas SQL

        Dim Query As New StringBuilder()
        Query.Append("UPDATE RemoteUpdate SET Status=")
        Query.Append(statusId.ToString())
        Query.Append(", Information='")
        Query.Append(errorMessage)
        Query.Append("' WHERE TemporaryID = ")
        Query.Append(temporaryId.ToString())

        Server.Con.ExecuteNonQuery(CommandType.Text, Query.ToString())

        Query.Remove(0, Query.Length)
        Query = Nothing
    End Sub

    ''' <summary>
    ''' Guarda el documento del update en insert
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <param name="lstIndexKeys"></param>
    ''' <param name="lstIndexValues"></param>
    ''' <remarks></remarks>
    Public Sub SaveUpdateonInsert(ByVal docTypeId As Int64, ByVal lstIndexKeys As Dictionary(Of Int64, String), ByVal lstIndexValues As Dictionary(Of Int64, String))
        Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO RemoteInsert (DocumentName, DocTypeId, FileExtension) values (''," & docTypeId & ",'')")

        Dim TemporaryId As Int64 = Server.Con.ExecuteScalar(CommandType.Text, "select max(temporaryId) as 'Id' from remoteinsert")

        For Each kvpKey As KeyValuePair(Of Int64, String) In lstIndexKeys
            Try
                Dim querybuilder As StringBuilder = New StringBuilder()
                querybuilder.Append("INSERT INTO DocumentsIndexs(Id, IndexId, IndexValue) ")
                querybuilder.Append("VALUES (")
                querybuilder.Append(TemporaryId)
                querybuilder.Append(",")
                querybuilder.Append(kvpKey.Key)
                querybuilder.Append(",'")
                querybuilder.Append(kvpKey.Value)
                querybuilder.Append("')")
                Server.Con.ExecuteNonQuery(CommandType.Text, querybuilder.ToString())
                'Si los indices ya existen, me va a tirar error de primary key
            Catch
                Dim querybuilder As StringBuilder = New StringBuilder()
                querybuilder.Append("UPDATE DocumentsIndexs set IndexValue='")
                querybuilder.Append(kvpKey.Value)
                querybuilder.Append("' where Id=")
                querybuilder.Append(TemporaryId)
                querybuilder.Append(" and IndexId=")
                querybuilder.Append(kvpKey.Key)
                Server.Con.ExecuteNonQuery(CommandType.Text, querybuilder.ToString())
            End Try
        Next

        For Each kvpKey As KeyValuePair(Of Int64, String) In lstIndexValues
            Try
                Dim querybuilder As StringBuilder = New StringBuilder()
                querybuilder.Append("INSERT INTO DocumentsIndexs(Id, IndexId, IndexValue) ")
                querybuilder.Append("VALUES (")
                querybuilder.Append(TemporaryId)
                querybuilder.Append(",")
                querybuilder.Append(kvpKey.Key)
                querybuilder.Append(",'")
                querybuilder.Append(kvpKey.Value)
                querybuilder.Append("')")
                Server.Con.ExecuteNonQuery(CommandType.Text, querybuilder.ToString())
                'Si los indices ya existen, me va a tirar error de primary key
            Catch
                Dim querybuilder As StringBuilder = New StringBuilder()
                querybuilder.Append("UPDATE DocumentsIndexs set IndexValue='")
                querybuilder.Append(kvpKey.Value)
                querybuilder.Append("' where Id=")
                querybuilder.Append(TemporaryId)
                querybuilder.Append(" and IndexId=")
                querybuilder.Append(kvpKey.Key)
                Server.Con.ExecuteNonQuery(CommandType.Text, querybuilder.ToString())
            End Try
        Next

        SaveDocumentStatus(TemporaryId, 3, 0)
    End Sub

    ''' <summary>
    ''' Guarda el estado del documento y el nuevo Id
    ''' </summary>
    ''' <param name="temporaryId"></param>
    ''' <param name="statusId"></param>
    ''' <param name="ResultId"></param>
    ''' <remarks></remarks>
    Public Sub SaveDocumentStatus(ByVal temporaryId As Int64, ByVal statusId As Int32, ByVal ResultId As Int64)
        Dim Query As New StringBuilder()
        Query.Append("UPDATE RemoteInsert SET Status=")
        Query.Append(statusId.ToString())
        Query.Append(", DocumentId=")
        Query.Append(ResultId.ToString())
        Query.Append(" WHERE TemporaryID=")
        Query.Append(temporaryId.ToString())

        Server.Con.ExecuteNonQuery(CommandType.Text, Query.ToString())

        Query.Remove(0, Query.Length)
        Query = Nothing

    End Sub

    ''' <summary>
    ''' Cambia el estado de todos los threads activos a pendientes.
    ''' </summary>
    ''' <param name="temporaryId"></param>
    ''' <param name="statusId"></param>
    ''' <param name="ResultId"></param>
    ''' <remarks></remarks>
    Public Shared Sub RestartActiveThreads()
        Server.Con.ExecuteNonQuery(System.Data.CommandType.Text, "update remoteinsert set status = 3 where status > 3")
    End Sub

End Class