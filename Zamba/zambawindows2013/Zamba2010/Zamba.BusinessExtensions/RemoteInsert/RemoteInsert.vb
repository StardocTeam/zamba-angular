Imports Zamba.Data

Public Class RemoteInsert
    Implements IDisposable

    Dim RIF As New RemoteInsertFactory

    Public Function GetDocumentsToInsert(ByVal WorkItem As Int64) As DataTable
        Return RIF.GetDocumentsToInsert(WorkItem)
    End Function

    ''' <summary>
    ''' Devuelve los atributos a insertar
    ''' </summary>
    ''' <param name="temporaryId"></param>
    ''' <history>Marcelo Created 16/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexToRemoteInsert(ByVal temporaryId As Int64) As DataTable
        Return RIF.GetIndexToRemoteInsert(temporaryId)
    End Function

    Public Sub SaveDocumentInserted(ByVal temporaryId As Int64, ByVal resultId As Int64)
        RIF.SaveDocumentStatus(temporaryId, DocumentsToInsertStatus.Inserted, resultId)
    End Sub

    ''' <summary>
    ''' Guarda el estado del documento
    ''' </summary>
    ''' <param name="temporaryId"></param>
    ''' <param name="status"></param>
    ''' <remarks></remarks>
    Public Sub SaveDocumentStatus(ByVal temporaryId As Int64, ByVal status As Int64, ByVal resultId As Int64)
        RIF.SaveDocumentStatus(temporaryId, status, resultId)
    End Sub

    Public Function ReserveInsertsStatus(ByVal WorkItem As Int64) As Int64
        Return RIF.ReserveInsertsStatus(WorkItem)
    End Function

    Public Sub SaveDocumentError(ByVal temporaryId As Int64, ByVal errorMessage As String)
        RIF.SaveDocumentError(temporaryId, errorMessage, DocumentsToInsertStatus.HasErrors)
    End Sub


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
    Public Function Insert(ByVal name As String, ByVal binaryDocument As Byte(), ByVal fileExtension As String, ByVal docTypeId As Int64, ByVal indexs As DataTable, ByVal DontOpenTaskAfterInsertInDoGenerateCoverPage As Boolean) As Int64
        Dim RB As New Results_Business

        Return RB.Insert(name, binaryDocument, fileExtension, docTypeId, indexs, DontOpenTaskAfterInsertInDoGenerateCoverPage, Nothing)
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If RIF IsNot Nothing Then
                    RIF.Dispose()
                    RIF = Nothing
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
Public Class RemoteUpdate
    Implements IDisposable

    Dim RUF As New RemoteUpdateFactory

    ''' <summary>
    ''' Obtiene los atributos claves
    ''' </summary>
    ''' <param name="intTemporaryID">Id del documento temporal</param>
    ''' <history>Marcelo Modified 15/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocumentsToRemoteUpdate(ByVal WorkItem As Int64) As DataTable
        Return RUF.GetDocumentsToRemoteUpdate(WorkItem)
    End Function

    ''' <summary>
    ''' Obtiene los atributos claves
    ''' </summary>
    ''' <param name="intTemporaryID">Id del documento temporal</param>
    ''' <history>Marcelo Created 15/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexToRemoteUpdate(ByVal intTemporaryId As Int64) As DataTable
        Return RUF.GetIndexToRemoteUpdate(intTemporaryId)
    End Function

    ''' <summary>
    ''' Actualiza los valores de los documentos
    ''' </summary>
    ''' <param name="docTypeId">Id de la entidad</param>
    ''' <param name="lstKeys">Listado de atributos clave (id del indice, valor a buscar)</param>
    ''' <param name="lstValues">Listado de atributos a actualizar (id del indice, valor del indice)</param>
    ''' <returns>Listado de los atributos actualizados</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	06/01/2009	Modified    Se agrego una opción en el UserConfig que (según su valor) permite insertar o no el documento si
    '''                                         falla la actualización
    ''' </history>
    Public Function UpdateDocuments(ByVal docTypeId As Int64, ByVal lstIndexKeys As Dictionary(Of Int64, String), ByVal lstIndexValues As Dictionary(Of Int64, String), ByVal TemporaryId As Int64) As List(Of Int64)
        Dim ds As DataSet = Nothing

        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizando")
            ds = RUF.UpdateDocuments(docTypeId, lstIndexKeys, lstIndexValues)

            Dim lstDocIds As List(Of Int64) = New List(Of Int64)

            If Not IsNothing(ds) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Documentos actualizados: " & ds.Tables(0).Rows.Count)
                For Each dr As DataRow In ds.Tables(0).Rows
                    Dim DocId As Int64 = Int64.Parse(dr("doc_id").ToString())
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Id del documento: " & DocId)
                    lstDocIds.Add(DocId)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando Historial")
                    UserBusiness.Rights.SaveAction(docTypeId, ObjectTypes.Index, RightsType.Edit, docTypeId)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Disparando Evento")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§")
                    Results_Business.ResultUpdated(docTypeId, DocId)
                Next
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El listado esta vacio")
            End If

            If lstDocIds.Count > 0 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Salvando Actualizacion: " & TemporaryId)
                RUF.SaveDocumentUpdated(TemporaryId, 1)
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Salvando Estado No Encontrado: " & TemporaryId)
                RUF.SaveDocumentUpdateError(TemporaryId, "No se han encontrado documentos para actualizar", 2)

                If (Boolean.Parse(UserPreferences.getValue("InsertDocumentsIfFailsUpdate", UPSections.Remoting, "True")) = True) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertando Valor: " & TemporaryId)
                    RUF.SaveUpdateonInsert(docTypeId, lstIndexKeys, lstIndexValues)
                End If
            End If

            Return lstDocIds

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error: " & ex.ToString())
            RUF.SaveDocumentUpdateError(TemporaryId, ex.ToString, 2)
            Return Nothing
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function

    Public Function ReserveUpdatesStatus(ByVal WorkItem As Int64) As Int64
        Return RUF.ReserveUpdatesStatus(WorkItem)
    End Function






































    ''' <summary>
    ''' Obtiene los atributos claves
    ''' </summary>
    ''' <param name="intTemporaryID">Id del documento temporal</param>
    ''' <history>Marcelo Created 15/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexKeysToRemoteUpdate(ByVal intTemporaryId As Int64) As DataTable
        Return RUF.GetIndexKeysToRemoteUpdate(intTemporaryId)
    End Function

    Public Sub SaveDocumentRemoteUpdated(ByVal temporaryId As Int64)
        RUF.SaveDocumentUpdated(temporaryId, DocumentsToInsertStatus.Inserted)
    End Sub

    ''' <summary>
    ''' Guarda el estado
    ''' </summary>
    ''' <param name="temporaryId"></param>
    ''' <param name="status"></param>
    ''' <remarks></remarks>
    Public Sub SaveDocumentRemoteStatus(ByVal temporaryId As Int64, ByVal status As Int64)
        RUF.SaveDocumentUpdated(temporaryId, 3)
    End Sub

    Public Sub SaveDocumentRemoteUpdateError(ByVal temporaryId As Int64, ByVal errorMessage As String)
        RUF.SaveDocumentUpdateError(temporaryId, errorMessage, DocumentsToInsertStatus.HasErrors)
    End Sub

    ''' <summary>
    ''' Cambia el estado de todos los threads activos a pendientes.
    ''' </summary>
    ''' <param name="temporaryId"></param>
    ''' <param name="statusId"></param>
    ''' <param name="ResultId"></param>
    ''' <remarks></remarks>
    Public Shared Sub RestartActiveThreads()
        Zamba.Data.RemoteUpdateFactory.RestartActiveThreads()
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls





























































    ' IDisposable
    Protected Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If RUF IsNot Nothing Then
                    RUF.Dispose()
                    RUF = Nothing
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