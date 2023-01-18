Imports System.Text
Imports System.Collections.Generic
Imports Zamba.Core

Public NotInheritable Class RemoteInsertFactory
    Implements IDisposable

    Dim CurrentConnection As IConnection = Servers.Server.Con(False)

    ''' <summary>
    ''' Obtiene los documentos a insertar
    ''' </summary>
    ''' <param name="WorkItem"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocumentsToInsert(ByVal WorkItem As Int64) As DataTable
        If Server.isOracle = True Then
            Dim Query As New StringBuilder()
            Query.Append("SELECT RemoteInsert.TemporaryID, RemoteInsert.DocumentName, RemoteInsert.DocTypeId,  ")
            Query.Append("RemoteInsert.SerializedFile,RemoteInsert.FileExtension, RemoteInsert.Information, RemoteInsert.TransactionId ")
            Query.Append("FROM RemoteInsert inner join remoteinsert_relations on remoteinsert.doctypeid = remoteinsert_relations.doctypeid WHERE RemoteInsert.Status = ")
            Query.Append(WorkItem.ToString())
            Query.Append(" order by position ")

            Return CurrentConnection.ExecuteDataset(CommandType.Text, Query.ToString).Tables(0)
        Else
            Return CurrentConnection.ExecuteDataset("ZSP_THREADPOOL_100_REMOTEINSERT_GetDocumentsToInsert", New Object() {WorkItem}).Tables(0)
        End If
    End Function

    ''' <summary>
    ''' Obtiene los atributos
    ''' </summary>
    ''' <param name="intTemporaryID">Id del documento temporal</param>
    ''' <history>Marcelo Created 16/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexToRemoteInsert(ByVal intTemporaryID As Int64) As DataTable
        Dim Ds As DataSet
        If Server.isOracle Then
            Dim Query As String = "select IndexId, IndexValue from DocumentsIndexs Where Id = " & intTemporaryID
            Ds = CurrentConnection.ExecuteDataset(CommandType.Text, Query)
        Else
            Dim parValues() As Object = {intTemporaryID}
            Ds = CurrentConnection.ExecuteDataset("zsp_100_remoteInsert_GetIndexToRemoteInsert", parValues)
            parValues = Nothing
        End If
        If Not IsNothing(Ds) AndAlso Ds.Tables.Count = 1 Then
            Return Ds.Tables(0)
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Quita los documentos de la insercion
    ''' </summary>
    ''' <param name="ids"></param>
    ''' <remarks></remarks>
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
        If Server.isOracle Then
            Dim Query As New StringBuilder()
            Query.Append("UPDATE RemoteInsert SET Status=")
            Query.Append(statusId.ToString())
            Query.Append(", DocumentId=")
            Query.Append(ResultId.ToString())
            Query.Append(", InsertDate=getdate()")
            Query.Append(" WHERE TemporaryID=")
            Query.Append(temporaryId.ToString())

            CurrentConnection.ExecuteNonQuery(CommandType.Text, Query.ToString())

            Query.Remove(0, Query.Length)
            Query = Nothing
        Else
            Dim parValues() As Object = {statusId, ResultId, temporaryId}
            CurrentConnection.ExecuteNonQuery("zsp_100_remoteInsert_SaveDocumentStatus", parValues)
            parValues = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Martin: Metodo que reserva con un workitem mayor a 9, los registros para ser insertados por un thread
    ''' </summary>
    ''' <param name="WorkItem"></param>
    ''' <remarks></remarks>
    Public Function ReserveInsertsStatus(ByVal WorkItem As Int64) As Int64
        Dim Query1 As New StringBuilder()
        Try
            If Server.isOracle Then
                Dim indexValue As String = CurrentConnection.ExecuteScalar(CommandType.Text, "select top 1  indexvalue from remoteinsert inner join remoteinsert_relations on remoteinsert.doctypeid = remoteinsert_relations.doctypeid inner join documentsindexs on remoteinsert.temporaryid = documentsindexs.id where remoteinsert_relations.indexid = documentsindexs.indexid and status = 3 order by registerdate, indexvalue")

                If String.IsNullOrEmpty(indexValue) Then
                    Return 0
                End If

                Query1.Append("UPDATE RemoteInsert SET Status = ")
                Query1.Append(WorkItem)
                Query1.Append(" where temporaryid in (select temporaryid from remoteinsert inner join remoteinsert_relations on remoteinsert.doctypeid = remoteinsert_relations.doctypeid inner join documentsindexs on remoteinsert.temporaryid = documentsindexs.id where remoteinsert_relations.indexid = documentsindexs.indexid and status = 3  and indexvalue ='")
                Query1.Append(indexValue)
                Query1.Append("')")

                Dim RowsCount1 As Int64 = CurrentConnection.ExecuteNonQuery(CommandType.Text, Query1.ToString())
                Query1.Remove(0, Query1.Length)

                Return RowsCount1
            Else
                Dim parValues() As Object = {WorkItem}
                Return CLng(CurrentConnection.ExecuteScalar("ZSP_THREADPOOL_100_RemoteInsert_ReserveStatus", parValues))
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return 0
        Finally
            Query1 = Nothing
        End Try
    End Function

    Public Sub SaveDocumentError(ByVal temporaryId As Int64, ByVal errorMessage As String, ByVal statusId As Int32)
        If Server.isOracle Then
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
        Else
            Dim parValues() As Object = {temporaryId, errorMessage, statusId}
            CurrentConnection.ExecuteNonQuery("zsp_100_remoteInsert_SaveDocumentError", parValues)
            parValues = Nothing
        End If
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If CurrentConnection IsNot Nothing AndAlso CurrentConnection.State = IConnection.ConnectionStates.Ready Then
                    CurrentConnection.Close()
                    CurrentConnection.dispose()
                    CurrentConnection = Nothing
                End If
            End If
        End If

        disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

Public NotInheritable Class RemoteUpdateFactory
    Implements IDisposable

    Dim CurrentConnection As IConnection = Servers.Server.Con(False)

    ''' <summary>
    ''' Obtiene los documentos a ser actualizados
    ''' </summary>
    ''' <history>Marcelo Created 15/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocumentsToRemoteUpdate(ByVal WorkItem As Int64) As DataTable
        Dim DtDocumentsToUpdate As DataTable = Nothing

        If Server.isOracle Then
            Dim Query As String = "SELECT RemoteUpdate.TemporaryID, RemoteUpdate.DocTypeId FROM RemoteUpdate WHERE Status = " & WorkItem.ToString()

            Using Ds As DataSet = CurrentConnection.ExecuteDataset(CommandType.Text, Query)
                If Not IsNothing(Ds) AndAlso Ds.Tables.Count = 1 Then
                    DtDocumentsToUpdate = Ds.Tables(0)
                End If
            End Using
        Else
            Dim parValues() As Object = {WorkItem}

            Using Ds As DataSet = CurrentConnection.ExecuteDataset("zsp_100_remoteUpdate_GetDocumentsToUpdate", parValues)
                If Not IsNothing(Ds) AndAlso Ds.Tables.Count = 1 Then
                    DtDocumentsToUpdate = Ds.Tables(0)
                End If
            End Using
            parValues = Nothing
        End If

        Return DtDocumentsToUpdate
    End Function

    ''' <summary>
    ''' Obtiene los atributos claves
    ''' </summary>
    ''' <param name="intTemporaryID">Id del documento temporal</param>
    ''' <history>Marcelo Created 15/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexKeysToRemoteUpdate(ByVal intTemporaryID As Int64) As DataTable
        Dim Ds As DataSet
        If Server.isOracle Then
            Dim Query As String = "select IndexId, IndexValue from DocumentKeyIndexs Where Id = " & intTemporaryID

            Ds = CurrentConnection.ExecuteDataset(CommandType.Text, Query)
        Else
            Dim parValues() As Object = {intTemporaryID}
            Ds = CurrentConnection.ExecuteDataset("zsp_100_remoteUpdate_GetKeyIndexToRemoteUpdate", parValues)
            parValues = Nothing
        End If

        If Not IsNothing(Ds) AndAlso Ds.Tables.Count = 1 Then
            Return Ds.Tables(0)
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene los atributos claves
    ''' </summary>
    ''' <param name="intTemporaryID">Id del documento temporal</param>
    ''' <history>Marcelo Created 15/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexToRemoteUpdate(ByVal intTemporaryID As Int64) As DataTable
        Dim Ds As DataSet
        If Server.isOracle Then
            Dim Query As String = "select IndexId, IndexValue from DocumentUpdateIndexs Where Id = " & intTemporaryID

            Ds = CurrentConnection.ExecuteDataset(CommandType.Text, Query)
        Else
            Dim parValues() As Object = {intTemporaryID}
            Ds = CurrentConnection.ExecuteDataset("zsp_100_remoteUpdate_GetIndexToRemoteUpdate", parValues)
            parValues = Nothing
        End If

        If Not IsNothing(Ds) AndAlso Ds.Tables.Count = 1 Then
            Return Ds.Tables(0)
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Actualiza los valores de los documentos
    ''' </summary>
    ''' <param name="docTypeId">Id de la entidad</param>
    ''' <param name="lstKeys">Listado de atributos clave (id del indice, valor a buscar)</param>
    ''' <param name="lstValues">Listado de atributos a actualizar (id del indice, valor del indice)</param>
    ''' <returns>Dataset de los atributos actualizados</returns>
    ''' <remarks></remarks>
    Public Function UpdateDocuments(ByVal docTypeId As Int64, ByVal lstIndexKeys As Dictionary(Of Int64, String), ByVal lstIndexValues As Dictionary(Of Int64, String)) As DataSet
        Dim strbuilder As StringBuilder = New StringBuilder()
        Dim strWhereBuilder As StringBuilder = New StringBuilder()
        Dim blnAux As Boolean

        Try
            strWhereBuilder.Append(" where ")

            For Each kvpKey As KeyValuePair(Of Int64, String) In lstIndexKeys
                If blnAux Then
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

            Dim dsModifiedIndex As DataSet = CurrentConnection.ExecuteDataset(CommandType.Text, "Select doc_id from doc_i" & docTypeId & strWhereBuilder.ToString())

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
                CurrentConnection.ExecuteNonQuery(CommandType.Text, strbuilder.ToString() & strWhereBuilder.ToString())

                Return dsModifiedIndex
            End If
            Return Nothing
        Finally
            strbuilder = Nothing
            strWhereBuilder = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Martin: Metodo que reserva con un workitem mayor a 4, los registros para ser insertados por un thread
    ''' </summary>
    ''' <param name="WorkItem"></param>
    ''' <remarks></remarks>
    Public Function ReserveUpdatesStatus(ByVal WorkItem As Int64) As Int64
        Dim RowsCount As Int64
        If Server.isOracle Then
            Dim Query As New StringBuilder()
            Query.Append("UPDATE RemoteUpdate SET Status = ")
            Query.Append(WorkItem)
            Query.Append(" WHERE Status = 3")
            RowsCount = CurrentConnection.ExecuteNonQuery(CommandType.Text, Query.ToString())
            Query.Remove(0, Query.Length)
            Query = Nothing
        Else
            Dim parValues() As Object = {WorkItem}
            RowsCount = CurrentConnection.ExecuteNonQuery("zsp_100_remoteUpdate_ReserveStatus", parValues)
            parValues = Nothing
        End If

        Return RowsCount
    End Function

    ''' <summary>
    ''' Escribe el estado de la actualizacion
    ''' </summary>
    ''' <param name="temporaryId"></param>
    ''' <param name="statusId"></param>
    ''' <remarks></remarks>
    Public Sub SaveDocumentUpdated(ByVal temporaryId As Int64, ByVal statusId As Int32)
        If Server.isOracle Then
            Dim Query As New StringBuilder()
            Try
                Query.Append("UPDATE RemoteUpdate SET Status=")
                Query.Append(statusId.ToString())
                Query.Append(" WHERE TemporaryID=")
                Query.Append(temporaryId.ToString())

                CurrentConnection.ExecuteNonQuery(CommandType.Text, Query.ToString())

            Finally
                Query.Remove(0, Query.Length)
                Query = Nothing
            End Try
        Else
            Dim parValues() As Object = {temporaryId, statusId}
            CurrentConnection.ExecuteNonQuery("zsp_100_remoteUpdate_SaveDocumentUpdated", parValues)
            parValues = Nothing
        End If
    End Sub

    Public Sub SaveDocumentUpdateError(ByVal temporaryId As Int64, ByVal errorMessage As String, ByVal statusId As Int32)
        errorMessage = errorMessage.Replace("'", String.Empty) ' Remplazo los "'" de la exception porque rompen las consultas SQL

        If Server.isOracle Then
            Dim Query As New StringBuilder()
            Query.Append("UPDATE RemoteUpdate SET Status=")
            Query.Append(statusId.ToString())
            Query.Append(", Information='")
            Query.Append(errorMessage)
            Query.Append("' WHERE TemporaryID = ")
            Query.Append(temporaryId.ToString())

            CurrentConnection.ExecuteNonQuery(CommandType.Text, Query.ToString())

            Query.Remove(0, Query.Length)
            Query = Nothing
        Else
            Dim parValues() As Object = {temporaryId, errorMessage, statusId}
            CurrentConnection.ExecuteNonQuery("zsp_100_remoteUpdate_SaveDocumentUpdateError", parValues)
            parValues = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Guarda el documento del update en insert
    ''' </summary>
    ''' <param name="docTypeId"></param>
    ''' <param name="lstIndexKeys"></param>
    ''' <param name="lstIndexValues"></param>
    ''' <remarks></remarks>
    Public Sub SaveUpdateonInsert(ByVal docTypeId As Int64, ByVal lstIndexKeys As Dictionary(Of Int64, String), ByVal lstIndexValues As Dictionary(Of Int64, String))
        Dim tempId As Int64 = CurrentConnection.ExecuteScalar("ZSP_THREADPOOL_100_REMOTEINSERT_InsertAndGetId", New Object() {docTypeId})

        Dim params() As Object = {0, 0, 0}
        For Each kvpKey As KeyValuePair(Of Int64, String) In lstIndexKeys
            params = {tempId, kvpKey.Key, kvpKey.Value}
            CurrentConnection.ExecuteNonQuery("ZSP_THREADPOOL_100_DOCUMENTSINDEXS_InsertOrUpdate", params)
        Next
        For Each kvpKey As KeyValuePair(Of Int64, String) In lstIndexValues
            params = {tempId, kvpKey.Key, kvpKey.Value}
            CurrentConnection.ExecuteNonQuery("ZSP_THREADPOOL_100_DOCUMENTSINDEXS_InsertOrUpdate", params)
        Next
        params = Nothing

        SaveDocumentStatus(tempId, 3, 0)
    End Sub

    ''' <summary>
    ''' Guarda el estado del documento y el nuevo Id
    ''' </summary>
    ''' <param name="temporaryId"></param>
    ''' <param name="statusId"></param>
    ''' <param name="ResultId"></param>
    ''' <remarks></remarks>
    Public Sub SaveDocumentStatus(ByVal temporaryId As Int64, ByVal statusId As Int32, ByVal ResultId As Int64)
        If Server.isOracle Then
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
        Else
            Dim parValues() As Object = {temporaryId, statusId, ResultId}
            CurrentConnection.ExecuteNonQuery("zsp_100_remoteUpdate_SaveDocumentStatus", parValues)
            parValues = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Cambia el estado de todos los threads activos a pendientes.
    ''' </summary>
    ''' <param name="temporaryId"></param>
    ''' <param name="statusId"></param>
    ''' <param name="ResultId"></param>
    ''' <remarks></remarks>
    Public Shared Sub RestartActiveThreads()
        If Server.isOracle Then
            Server.Con.ExecuteNonQuery(System.Data.CommandType.Text, "update remoteinsert set status = 3 where status > 3")
        Else
            Dim parValues() As Object = {}
            Server.Con.ExecuteNonQuery("zsp_100_remote_RestartActiveThreads", parValues)
            parValues = Nothing
        End If
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If CurrentConnection IsNot Nothing AndAlso CurrentConnection.State = IConnection.ConnectionStates.Ready Then
                    CurrentConnection.Close()
                    CurrentConnection.dispose()
                    CurrentConnection = Nothing
                End If
            End If
        End If

        disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class