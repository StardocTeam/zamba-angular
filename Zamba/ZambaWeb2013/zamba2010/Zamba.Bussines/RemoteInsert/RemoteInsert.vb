Imports Zamba.Data
Imports System.Collections.Generic
Imports System.IO

Public Class RemoteInsert

    Dim RIF As New RemoteInsertFactory

    Public Function GetDocumentsToInsert(ByVal WorkItem As Int64) As DataTable
        Return RIF.GetDocumentsToInsert(WorkItem)
    End Function

    ''' <summary>
    ''' Devuelve los indices a insertar
    ''' </summary>
    ''' <param name="temporaryId"></param>
    ''' <history>Marcelo Created 16/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexToRemoteInsert(ByVal temporaryId As Int64) As DataTable
        Return RIF.GetIndexToRemoteInsert(temporaryId)
    End Function

    Public Sub SaveDocumentInserted(ByVal temporaryId As Int64, ByVal resultId As Int64)
        RIF.SaveDocumentStatus(temporaryId, DirectCast(DocumentsToInsertStatus.Inserted, Int32), resultId)
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
        RIF.SaveDocumentError(temporaryId, errorMessage, DirectCast(DocumentsToInsertStatus.HasErrors, Int32))
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
        Dim Reg As New Results_Business
        Dim R As Int64 = Reg.Insert(name, binaryDocument, fileExtension, docTypeId, indexs, DontOpenTaskAfterInsertInDoGenerateCoverPage)
        Reg = Nothing
        Return R
    End Function

End Class
Public Class RemoteUpdate
    Dim RUF As New RemoteUpdateFactory

    ''' <summary>
    ''' Obtiene los indices claves
    ''' </summary>
    ''' <param name="intTemporaryID">Id del documento temporal</param>
    ''' <history>Marcelo Modified 15/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDocumentsToRemoteUpdate(ByVal WorkItem As Int64) As DataTable
        Return RUF.GetDocumentsToRemoteUpdate(WorkItem)
    End Function
    ''' <summary>
    ''' Obtiene los indices claves
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
    ''' <param name="docTypeId">Id del entidad</param>
    ''' <param name="lstKeys">Listado de atributos clave (id del indice, valor a buscar)</param>
    ''' <param name="lstValues">Listado de atributos a actualizar (id del indice, valor del indice)</param>
    ''' <returns>Listado de los indices actualizados</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	06/01/2009	Modified    Se agrego una opción en el UserConfig que (según su valor) permite insertar o no el documento si
    '''                                         falla la actualización
    ''' </history>
    Public Function UpdateDocuments(ByVal docTypeId As Int64, ByVal lstIndexKeys As Dictionary(Of Int64, String), ByVal lstIndexValues As Dictionary(Of Int64, String), ByVal TemporaryId As Int64) As List(Of Int64)
        Try
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Actualizando")
            Dim ds As DataSet = RUF.UpdateDocuments(docTypeId, lstIndexKeys, lstIndexValues)

            Dim lstDocIds As List(Of Int64) = New List(Of Int64)

            If Not IsNothing(ds) Then
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Documentos actualizados: " & ds.Tables(0).Rows.Count)
                For Each dr As DataRow In ds.Tables(0).Rows
                    Dim DocId As Int64 = Int64.Parse(dr("doc_id").ToString())
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Id del documento: " & DocId)
                    lstDocIds.Add(DocId)

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Disparando Evento")
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§")
                    Results_Business.ResultUpdated(docTypeId, DocId)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "§§§§§§§§§§§§§§§§§§§§§§§§§§§§§§")
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando Historial")
                    UserBusiness.Rights.SaveAction(docTypeId, Zamba.Core.ObjectTypes.Index, Zamba.Core.RightsType.Edit, docTypeId)
                Next
            Else
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "El listado esta vacio")
            End If

            If lstDocIds.Count > 0 Then
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Salvando Actualizacion: " & TemporaryId)
                RUF.SaveDocumentUpdated(TemporaryId, 1)
            Else
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Salvando Estado No Encontrado: " & TemporaryId)
                RUF.SaveDocumentUpdateError(TemporaryId, "No se han encontrado documentos para actualizar", 2)

                If (Boolean.Parse(UserPreferences.getValue("InsertDocumentsIfFailsUpdate", Sections.Remoting, "True")) = True) Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Insertando Valor: " & TemporaryId)
                    RUF.SaveUpdateonInsert(docTypeId, lstIndexKeys, lstIndexValues)
                End If
            End If

            Return lstDocIds

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error: " & ex.ToString())
            RUF.SaveDocumentUpdateError(TemporaryId, ex.ToString, 2)
            Return Nothing
        End Try
    End Function

    Public Function ReserveUpdatesStatus(ByVal WorkItem As Int64) As Int64
        Return RUF.ReserveUpdatesStatus(WorkItem)
    End Function

    ''' <summary>
    ''' Corre el proceso de actualizacion de los documentos
    ''' </summary>
    ''' <remarks></remarks>
    'Public Sub RunDocumentUpdates()
    '    Dim dt As DataTable
    '    dt = GetDocumentsToRemoteUpdate()

    '    If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
    '        ZTrace.WriteLineIf(ZTrace.IsVerbose,"Cantidad de documentos a actualizar: " & dt.Rows.Count)
    '        For Each CurrentRow As DataRow In dt.Rows
    '            Dim temporaryId As Int64 = Int64.Parse(CurrentRow("TemporaryID").ToString())
    '            ZTrace.WriteLineIf(ZTrace.IsVerbose,"TemporaryId: " & temporaryId)
    '            Dim dtIndex As DataTable = RU.GetIndexKeysToRemoteUpdate(temporaryId)
    '            Dim dcKeyIndex As Dictionary(Of Int64, String) = New Dictionary(Of Int64, String)()
    '            For Each CurrentKeyRow As DataRow In dtIndex.Rows
    '                ZTrace.WriteLineIf(ZTrace.IsVerbose,"Keys")
    '                ZTrace.WriteLineIf(ZTrace.IsVerbose,"IndexId: " & CurrentKeyRow("IndexId").ToString())
    '                ZTrace.WriteLineIf(ZTrace.IsVerbose,"IndexValue: " & CurrentKeyRow("IndexValue").ToString())
    '                dcKeyIndex.Add(Int64.Parse(CurrentKeyRow("IndexId").ToString()), CurrentKeyRow("IndexValue").ToString())
    '            Next

    '            dtIndex = RU.GetIndexToRemoteUpdate(temporaryId)
    '            Dim dcValueIndex As Dictionary(Of Int64, String) = New Dictionary(Of Int64, String)()
    '            For Each CurrentKeyRow As DataRow In dtIndex.Rows
    '                ZTrace.WriteLineIf(ZTrace.IsVerbose,"Values")
    '                ZTrace.WriteLineIf(ZTrace.IsVerbose,"IndexId: " & CurrentKeyRow("IndexId").ToString())
    '                ZTrace.WriteLineIf(ZTrace.IsVerbose,"IndexValue: " & CurrentKeyRow("IndexValue").ToString())
    '                dcValueIndex.Add(Int64.Parse(CurrentKeyRow("IndexId").ToString()), CurrentKeyRow("IndexValue").ToString())
    '            Next

    '            RU.UpdateDocuments(Int64.Parse(CurrentRow("DocTypeId").ToString()), dcKeyIndex, dcValueIndex, temporaryId)

    '        Next
    '    End If
    'End Sub

    ''' <summary>
    ''' Obtiene los indices claves
    ''' </summary>
    ''' <param name="intTemporaryID">Id del documento temporal</param>
    ''' <history>Marcelo Created 15/12/2008</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIndexKeysToRemoteUpdate(ByVal intTemporaryId As Int64) As DataTable
        Return RUF.GetIndexKeysToRemoteUpdate(intTemporaryId)
    End Function

    Public Sub SaveDocumentRemoteUpdated(ByVal temporaryId As Int64)
        RUF.SaveDocumentUpdated(temporaryId, DirectCast(DocumentsToInsertStatus.Inserted, Int32))
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
        RUF.SaveDocumentUpdateError(temporaryId, errorMessage, DirectCast(DocumentsToInsertStatus.HasErrors, Int32))
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

End Class


'Se encarga de obtener los documentos a ingresar en zamba de las tablas temporales 
'Public Shared Sub HandleDocumentsToInsert()
'    Dim Dt As DataTable = Results_Factory.GetDocumentsToInsert

'    If Not IsNothing(Dt) AndAlso Dt.Rows.Count > 0 Then
'        Dim TaskList As New Dictionary(Of NewResult, Byte())

'        Dim DocumentBynary As Byte() = Nothing
'        Dim CurrentName As String = String.Empty
'        Dim CurrentDocTypeId As Int64
'        Dim CurrentIndexs As New Dictionary(Of Int64, String)
'        Dim CurrentId As Int64 = -1
'        Dim TemporaryId As Int64

'        For Each CurrentRow As DataRow In Dt.Rows

'            TemporaryId = Int64.Parse(CurrentRow("TemporaryID").ToString())

'            If (TemporaryId <> CurrentId) Then
'                If (CurrentId <> -1) Then
'                    Try
'                        Insert(CurrentName, DocumentBynary, CurrentDocTypeId, CurrentIndexs)
'                        SaveDocumentInserted(CurrentId)

'                        CurrentIndexs.Clear()
'                    Catch ex As Exception
'                        SaveDocumentError(CurrentId, ex.Message)
'                    End Try
'                End If
'                CurrentId = TemporaryId

'                CurrentDocTypeId = Int64.Parse(CurrentRow("DocTypeId").ToString())
'                CurrentName = CurrentRow("DocumentName").ToString()

'                DocumentBynary = CurrentRow("SerializedFile")
'            End If

'            CurrentIndexs.Add(Int64.Parse(CurrentRow("IndexId").ToString()), CurrentRow("IndexValue").ToString())
'        Next

'        If Not IsNothing(DocumentBynary) Then
'            Array.Clear(DocumentBynary, 0, DocumentBynary.Length)
'            DocumentBynary = Nothing
'        End If

'        If Not IsNothing(CurrentIndexs) Then
'            CurrentIndexs.Clear()
'            CurrentIndexs = Nothing
'        End If

'        'Results_Business.Insert(TaskList)

'    End If
'End Sub

'Inserta los documentos temporales en zamba
'Private Shared Sub Insert(ByVal documents As Dictionary(Of NewResult, Byte()))
'    Dim Indexs As New Dictionary(Of Int64, String)
'    Dim DocumentsIds As New List(Of Int64)(documents.Keys.Count)

'    For Each CurrentNewResult As NewResult In documents.Keys
'        Indexs.Clear()

'        DocumentsIds.Add(CurrentNewResult.ID)

'        For Each myIndex As Index In CurrentNewResult.Indexs
'            If Not Indexs.ContainsKey(myIndex.ID) Then
'                Indexs.Add(myIndex.ID, myIndex.Data)
'            End If
'        Next

'        Insert(CurrentNewResult.Name, documents.Item(CurrentNewResult), CurrentNewResult.DocType.ID, Indexs)
'    Next

'    Results_Factory.RemoveDocumentsToInsert(DocumentsIds)
'End Sub