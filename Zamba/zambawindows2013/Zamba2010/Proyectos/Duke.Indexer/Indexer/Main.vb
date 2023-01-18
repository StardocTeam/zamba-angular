Imports Zamba.Core
Imports Zamba.Servers
Imports Zamba.Data

''' <summary>
''' Zamba.Indexer
''' Se reescribe por completo el indexador de índices para no 
''' depender de la ZI ni de objetos Result.
''' </summary>
''' <remarks></remarks>
Module Main

    Private rowsToGet As Int64
    Private lastDocId As Int64
    Private indexerStart As Int32
    Private indexerEnd As Int32

    Sub Main()
        Dim docTypeIds As DataTable
        Dim docs As DataTable
        Dim docId As Int64
        Dim word As String
        Dim resultId As Int64
        Dim indexId As Int64
        Dim fullDescription As Boolean
        Dim saveLastProcessedDoc As Boolean

        Try
            'Obtiene la cantidad de filas que procesará por tabla. Esto evita que la aplicación se 
            'cuelgue por tener que procesar millones de filas a la vez.
            rowsToGet = CLng(UserPreferences.getValue("RowsToGet", Sections.Indexer, "5000"))
            'Último Id de documento procesado
            lastDocId = CLng(UserPreferences.getValue("LastDocIdProcessed", Sections.Indexer, "0"))
            'Descripción completa del proceso
            fullDescription = Boolean.Parse(UserPreferences.getValue("FullDescription", Sections.Indexer, "False"))
            'Guarda en el userconfig el último docid y docTypeId procesado
            saveLastProcessedDoc = Boolean.Parse(UserPreferences.getValue("SaveLastProcessedDoc", Sections.Indexer, "True"))
            'Tiempo que el indexer podrá ejecutarse
            indexerStart = Int32.Parse(Zamba.Core.UserPreferences.getValue("IndexerStart", Sections.Indexer, "0"))
            indexerEnd = Int32.Parse(Zamba.Core.UserPreferences.getValue("IndexerEnd", Sections.Indexer, "24"))

            ShowMessage()

            'Obtiene los ids de todos los tipos de documentos
            docTypeIds = GetDocTypes()

            'Recorre los tipos de documentos
            For Each _docId In docTypeIds.Rows

                docId = CLng(_docId(0))

                'Guarda el tipo de documento procesado
                If saveLastProcessedDoc Then
                    UserPreferences.setValue("StartFromDocType", docId.ToString, Sections.Indexer)
                End If

                Do
                    'Obtiene los documentos que no se encuentran indexados
                    docs = GetDocuments(docId)

                    'Recorre los documentos
                    For Each doc As DataRow In docs.Rows
                        'Recorre las columnas
                        For i As Int32 = 0 To docs.Columns.Count - 1

                            Try
                                'Si el nombre de la columna es un índice y contiene datos lo agrego a la zsearchvalues
                                If (docs.Columns(i).ColumnName.ToLower.StartsWith("i")) AndAlso _
                                    Not (TypeOf (doc(i)) Is DBNull) AndAlso _
                                    Not (String.IsNullOrEmpty(doc(i).ToString)) Then
                                    'Obtiene los datos necesarios para la inserción
                                    word = doc(i).ToString
                                    resultId = CLng(doc("doc_id"))
                                    indexId = CLng(docs.Columns(i).ColumnName.Remove(0, 1))
                                    'Log
                                    Console.Write(vbCrLf & "Procesando: " & word)
                                    If fullDescription Then
                                        Console.Write(", ResultID=" & resultId.ToString & ", IndexId=" & indexId.ToString & ", DocId=" & docId.ToString & ")")
                                    End If
                                    'Inserción
                                    Results_Factory.InsertSearchIndexData(word, docId, resultId)
                                    'Guarda el último id procesado
                                    If saveLastProcessedDoc Then
                                        UserPreferences.setValue("LastDocIdProcessed", resultId.ToString, Sections.Indexer)
                                    End If
                                End If
                            Catch ex As Exception
                                raiseerror(ex)
                                Console.Write("ERROR")
                            End Try

                            'Verifica si puede o no ejecutarse en la hora de la máquina
                            CheckTime()
                        Next
                    Next

                Loop While docs.Rows.Count = rowsToGet

                lastDocId = 0

            Next

        Catch ex As Exception
            raiseerror(ex)
            Console.WriteLine("ERROR - " & ex.Message)
        Finally
            Console.WriteLine(vbCrLf & "Último tipo de documento procesado: " & docId.ToString)
            Console.WriteLine("Último documento procesado: " & resultId.ToString)
            Console.Write(vbCrLf & "Presione una tecla para salir...")
            Console.ReadLine()
        End Try
    End Sub

    Private Function GetDocTypes() As DataTable
        Dim dt As DataTable
        Dim docTypeToStart As Int32 = CInt(UserPreferences.getValue("StartFromDocType", Sections.Indexer, "0"))
        Dim rowCount As Int32
        Console.Write("Obteniendo tipos de documentos...")
        dt = Server.Con.ExecuteDataset(CommandType.Text, "SELECT DOC_TYPE_ID FROM DOC_TYPE ORDER BY DOC_TYPE_ID").Tables(0)
        Console.WriteLine("OK")

        'Remueve los docTypes que no se procesarán
        rowCount = dt.Rows.Count
        For i As Int32 = 0 To rowCount
            If CInt(dt.Rows(0)(0)) < docTypeToStart Then
                dt.Rows.RemoveAt(0)
            Else
                Exit For
            End If
        Next

        Return dt
    End Function

    Private Function GetDocuments(ByVal docId As Int64) As DataTable
        Dim dt As DataTable
        Console.WriteLine("_______________________________________________" & vbCrLf & "Procesando tipo de documento Nº: " & docId.ToString)

        'Obtiene N cantidad de filas de una tabla DOC_I. Los datos en la tabla se ordenan para poder obtener el último id
        'asi la proxima vez que se haga el select se hará desde ese id en adelante.
        If Server.isSQLServer Then
            dt = Server.Con.ExecuteDataset(CommandType.Text, "SELECT TOP " & rowsToGet.ToString & " * FROM DOC_I" & docId.ToString & _
                                       " WHERE DOC_ID > " & lastDocId.ToString & " ORDER BY DOC_ID").Tables(0)
        Else
            dt = Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM DOC_I" & docId & " WHERE DOC_ID > " & _
                                        lastDocId.ToString & " AND ROWNUM < " & (rowsToGet + 1).ToString & " ORDER BY DOC_ID").Tables(0)
        End If

        'Obtiene el último id procesado
        If dt.Rows.Count > 0 Then
            lastDocId = CLng(dt.Rows(dt.Rows.Count - 1)("DOC_ID"))
        End If
        Return dt
    End Function

    Private Sub ShowMessage()
        Console.Write("********************************************************************************")
        Console.Write("                                 ZAMBA.INDEXER                                  ")
        Console.Write("********************************************************************************")
        Console.WriteLine("NOTA: El proceso puede requerir de una gran cantidad de tiempo dependiendo de")
        Console.WriteLine("      la cantidad de documentos que existan en zamba. Se recomienda configurar")
        Console.WriteLine("      IndexerStart e IndexerEnd en el caso de que por la noche se realizen")
        Console.WriteLine("      backups de la base. También es recomendable tener en True la opción")
        Console.WriteLine("      SaveLastProcessedDoc (ver la descripción debajo para su utilidad).")
        Console.WriteLine(vbCrLf & "Configuración:")
        Console.WriteLine("* RowsToGet: El procesamiento se realiza por bloques de registros para evitar")
        Console.WriteLine("  traer demasiados datos y 'colgar' la base. El bloque procesado será de N")
        Console.WriteLine("  registros. Default = 5000")
        Console.WriteLine("* FullDescription: Descripción detallada del procesamiento. Default = False")
        Console.WriteLine("* LastDocIdProcessed: DocId de una DOC_I para comenzar. Se utiliza en conjunto")
        Console.WriteLine("  con la opción StartFromDocType. Default = 0")
        Console.WriteLine("* StartFromDocType: DocType para comenzar a procesar. El procesamiento es")
        Console.WriteLine("  ascendente. Default = 0")
        Console.WriteLine("* SaveLastProcessedDoc: Guarda el último ID de tipo de documento en la opción")
        Console.WriteLine("  StartFromDocType y el ID del documento en la opción LastDocIdProcessed.")
        Console.WriteLine("  Default = True")
        Console.WriteLine("* IndexerStart: Hora en que el indexer comienza a ejecutarse. Default = 0")
        Console.WriteLine("* IndexerEnd: Hora en que el indexer detiene la ejecución. Default = 24")
        Console.Write(vbCrLf & "Presione una tecla para comenzar...")
        Console.ReadLine()
    End Sub

    Private Sub CheckTime()
        If DateTime.Now.Hour >= indexerStart AndAlso DateTime.Now.Hour <= indexerEnd Then
        Else
            System.Threading.Thread.Sleep(3600000)
            CheckTime()
        End If
    End Sub

End Module