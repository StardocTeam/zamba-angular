Imports Zamba.Data
Imports Zamba.Servers

Public NotInheritable Class RemoteInsertExt
    Implements IDisposable

    Dim RIF As New RemoteInsertFactory
    Dim RIFE As New RemoteInsertFactoryExt
    Dim RIB As New Zamba.Core.RemoteInsert

    ''' <summary>
    ''' Obtiene los documentos a insertar de un workitem específico
    ''' </summary>
    ''' <param name="WorkItem">Workitem</param>
    ''' <returns>Listado de documentos a insertar</returns>
    ''' <remarks></remarks>
    Private Function GetDocumentsToInsert(ByVal WorkItem As Int64) As List(Of RemoteInsertEntry)
        Dim dtDocuments As DataTable = RIF.GetDocumentsToInsert(WorkItem)

        If dtDocuments.Rows.Count > 0 Then
            Dim lstDocuments As New List(Of RemoteInsertEntry)
            Dim serializedFile As Object

            For i As Int32 = 0 To dtDocuments.Rows.Count - 1
                serializedFile = dtDocuments.Rows(i)("SerializedFile")
                If serializedFile Is GetType(DBNull) Then
                    serializedFile = Nothing
                End If

                lstDocuments.Add(New RemoteInsertEntry(dtDocuments.Rows(i)("TemporaryID"), _
                                                       dtDocuments.Rows(i)("DocumentName"), _
                                                       dtDocuments.Rows(i)("DocTypeId"), _
                                                       dtDocuments.Rows(i)("FileExtension"), _
                                                       serializedFile, _
                                                       dtDocuments.Rows(i)("TransactionId")))
            Next

            dtDocuments.Dispose()
            dtDocuments = Nothing
            Return lstDocuments
        Else
            dtDocuments.Dispose()
            dtDocuments = Nothing
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Inserta la documentación de un workitem
    ''' </summary>
    ''' <param name="workItem">Workitem</param>
    ''' <returns>Cuantos documentos inserto para el workitem</returns>
    ''' <remarks></remarks>
    Public Function InsertDocuments(workItem As Int64) As Int64
        Dim documentInserted As Int64 = 0
        Dim documents As List(Of RemoteInsertEntry) = Nothing
        Dim dtIndexes As DataTable = Nothing

        Try
            'Obtiene los documentos a insertar ordenados por el id de transaccion y luego por su posicion
            documents = GetDocumentsToInsert(workItem)

            If documents IsNot Nothing Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Documentos a Insertar para Thread Nro " + workItem.ToString() + " son " + documents.Count.ToString())

                Dim t As New Transaction(Server.Con(False))
                'DESCOMENTAR AL SOLUCIONAR EL PROBLEMA DE EJECUCION DE REGLAS DE WF DE ENTRADA CON TRANSACCIONES: 
                'Dim tError As RemoteInsertError = Nothing
                Dim resultId As Int64

                'Se obtienen los documentos propios de esa transacción.
                For Each document As RemoteInsertEntry In documents
                    Try
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Documento a Insertar con TemporaryId Nro " & document.TemporaryId.ToString &
                                          ", Id de la entidad " & document.DocTypeId.ToString & ", Extension del Archivo " & document.FileExtension)

                        'Se obtienen los valores a completar del documento
                        dtIndexes = RIB.GetIndexToRemoteInsert(document.TemporaryId)

                        'Verifica si puede insertar la documentación
                        If ExistsDocType(document.DocTypeId) Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Insertando Documento")

                            With document
                                'DESCOMENTAR AL SOLUCIONAR EL PROBLEMA DE EJECUCION DE REGLAS DE WF DE ENTRADA CON TRANSACCIONES: 
                                'resultId = Results_Business.Insert(.DocumentName, .SerializedFile, .FileExtension, .DocTypeId, dtIndexes, False, t)
                                'SaveDocumentInserted(.TemporaryId, resultId, t)
                                Dim RB As New Results_Business

                                resultId = RB.Insert(.DocumentName, .SerializedFile, .FileExtension, .DocTypeId, dtIndexes, False, Nothing)
                                SaveDocumentInserted(.TemporaryId, resultId, Nothing)
                            End With

                            If resultId > 0 Then documentInserted += 1
                        Else
                            Throw New Exception("No existe el Id de Entidad " & document.DocTypeId.ToString())
                        End If

                    Catch ex As Exception
                        'Guarda el error para procesarlo luego del rollback
                        'DESCOMENTAR AL SOLUCIONAR EL PROBLEMA DE EJECUCION DE REGLAS DE WF DE ENTRADA CON TRANSACCIONES: 
                        'tError = New RemoteInsertError(document.TransactionId, document.TemporaryId, ex.Message)
                        ZClass.raiseerror(ex) 'BORRAR AL SOLUCIONAR EL PROBLEMA DE TRANSACCIONES
                        Exit For
                    End Try
                Next

                'POSIBLE SOLUCION PARA EL PROBLEMA DE LAS TRANSACCIONES:
                '1. DESDE THREADPOOL, PASARLE UN NUEVO PARAMETRO DE TIPO BOOL A LA INSERCION INDICANDO SI SE DEBE
                '   EJECUTAR O NO LAS REGLAS DE WORKFLOW.
                '2. SI NO SE EJECUTA, QUE GUARDE LA FECHA (GETDATE()) EN UNA NUEVA COLUMNA DE WFDOCUMENT.
                '3. EN EL SERVICIO DE WORKFLOW AGREGARLE UNA OPCIÓN PARA OBTENER LAS TAREAS DE WFDOCUMENT QUE TENGAN
                '   ALGUN DATO EN LA NUEVA COLUMNA ORDENADO DE MENOR A MAYOR POR LA FECHA.
                '4. AL OBTENER TAREAS DEBERÁ PROCESARLAS POR LA ENTRADA.
                '5. CON ESTO LOGRAMOS SEPARAR LA FUNCIONALIDAD DE REMOTEINSERT DE WORKFLOW.
                '6. EN EL CLIENTE SE DEBE AGREGAR A LA QUERY DE WFDOCUMENT QUE SOLO OBTENGA LAS QUE NO TENGAN DATOS 
                '   EN LA NUEVA COLUMNA.

                'DESCOMENTAR AL SOLUCIONAR EL PROBLEMA DE EJECUCION DE REGLAS DE WF DE ENTRADA CON TRANSACCIONES: 
                'If t IsNot Nothing Then
                '    'Se aplica rollback o commit dependiendo de si hubo o no error
                '    Try
                '        If tError IsNot Nothing Then
                '            If Not IsNothing(t.Transaction) AndAlso t.Transaction.Connection.State <> ConnectionState.Closed Then
                '                t.Rollback()
                '            End If

                '            'Guarda el error en el documento
                '            SaveDocumentError(tError)
                '        Else
                '            RIFE.SaveTransactionStatus(documents(0).TransactionId, DirectCast(DocumentsToInsertStatus.Inserted, Int32), t)
                '            t.Commit()
                '        End If
                '    Catch ex As Exception
                '        Zamba.Core.ZClass.raiseerror(ex)
                '    Finally
                '        If Not IsNothing(t) Then
                '            If Not IsNothing(t.Con) Then
                '                t.Con.Close()
                '                t.Con.dispose()
                '                t.Con = Nothing
                '            End If
                '            t.Dispose()
                '            t = Nothing
                '        End If
                '        tError = Nothing
                '    End Try
                'End If
            End If

        Catch ex As Exception
            Global.Zamba.Core.ZClass.raiseerror(ex)
        Finally
            If documents IsNot Nothing Then
                For i As Int32 = 0 To documents.Count - 1
                    If documents(i) IsNot Nothing Then documents(i).Dispose()
                Next
                documents.Clear()
                documents = Nothing
            End If
            If dtIndexes IsNot Nothing Then
                dtIndexes.Dispose()
                dtIndexes = Nothing
            End If
        End Try

        Return documentInserted
    End Function

    ''' <summary>
    ''' Verifica si existe una entidad
    ''' </summary>
    ''' <param name="docTypeId">Id de la entidad a verificar</param>
    ''' <returns>True en caso de existir</returns>
    ''' <remarks></remarks>
    Public Function ExistsDocType(docTypeId As Int64) As Boolean
        If Not Cache.DocTypesAndIndexs.dicDocTypeExistance.ContainsKey(docTypeId) Then
            Dim dtbExt As New DocTypeBusinessExt()
            Cache.DocTypesAndIndexs.dicDocTypeExistance.Add(docTypeId, dtbExt.CheckDocTypeExistance(docTypeId))
            dtbExt = Nothing
        End If

        Return Cache.DocTypesAndIndexs.dicDocTypeExistance(docTypeId)
    End Function

    ''' <summary>
    ''' Marca al documento con error y guarda el mensaje del problema
    ''' </summary>
    ''' <param name="remoteInsertError">Error de inserción</param>
    ''' <remarks></remarks>
    Private Sub SaveDocumentError(ByVal remoteInsertError As RemoteInsertError)
        RIFE.SaveDocumentError(remoteInsertError.TemporaryId, remoteInsertError.Message, DocumentsToInsertStatus.HasErrors, remoteInsertError.TransactionId)
    End Sub

    ''' <summary>
    ''' Marca al documento como insertado
    ''' </summary>
    ''' <param name="temporaryId">Id temporal del documento</param>
    ''' <param name="resultId">Id del documento</param>
    ''' <param name="t">Transacción</param>
    ''' <remarks></remarks>
    Private Sub SaveDocumentInserted(ByVal temporaryId As Int64, ByVal resultId As Int64, ByRef t As Transaction)
        RIFE.SaveDocumentStatus(temporaryId, DocumentsToInsertStatus.Inserted, resultId, t)
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                If RIB IsNot Nothing Then
                    RIB.Dispose()
                    RIB = Nothing
                End If
                If RIF IsNot Nothing Then
                    RIF.Dispose()
                    RIF = Nothing
                End If
                If RIFE IsNot Nothing Then
                    RIFE.Dispose()
                    RIFE = Nothing
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

''' <summary>
''' Proporciona ayuda para manejar los errores generados en el proceso de inserción
''' </summary>
''' <remarks></remarks>
Class RemoteInsertError
    Private _transactionId As Int64
    Public Property TransactionId() As Int64
        Get
            Return _transactionId
        End Get
        Set(ByVal value As Int64)
            _transactionId = value
        End Set
    End Property

    Private _temporaryId As Int64
    Public Property TemporaryId() As String
        Get
            Return _temporaryId
        End Get
        Set(ByVal value As String)
            _temporaryId = value
        End Set
    End Property

    Private _message As String
    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property

    Public Sub New(ByVal transactionId As Int64, ByVal temporaryId As Int64, ByVal message As String)
        _transactionId = transactionId
        _temporaryId = temporaryId
        _message = message
    End Sub
End Class
