Imports Zamba.Core
Imports Zamba.Servers

''' <summary>
''' Realiza una indexación para documentos anteriores a la implementación en Zamba
''' </summary>
''' <remarks></remarks>
''' <history>
'''     dalbarellos 16.04.2009 tfs1619 Created
'''     [Tomas] - 28/04/2009 - Modified - Se optimiza el proceso completo de búsqueda e indexación.
''' </history>
Public Class frmIndexer

    Private indexedDocsCount As Int32 = 0
    Private counter As Int64 = 0
    Private _salir As Boolean = False
    Delegate Sub DSetCResultCount()
    Private th As New Threading.Thread(AddressOf Indexar)
    'Private _dtids As List(Of Int64)
    'Private Const CONS_TABLENAME = "DOC_T"
    'Private Const COL_DOC_ID = "DOC_ID"

    Private Sub btnIndexar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIndexar.Click
        '
        '   PROCESAMIENTO ANTIGUO
        '
        'Dim doctypes As DSDOCTYPE = DocTypesBusiness.GetDocTypes()
        ''setea el estado inicial del form
        'SetInitialState(doctypes)
        'obtiene los ids de los doctypes
        'Dim dtids As List(Of Int64) = GetDoctypesIds(doctypes)
        'doctypes.Dispose()
        'GetResultCount(dtids)
        'Me._dtids = dtids

        '
        '   PROCESAMIENTO NUEVO
        '
        'Obtiene la cantidad de documentos a indexar
        Dim count As Int32 = GetResultCount()

        'Se actualiza el formulario con la cantidad de documentos a indexar.
        btnIndexar.Enabled = False
        lblDocsToIndex.Text = count.ToString
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = count
        ProgressBar1.Step = count

        th.Start()
    End Sub


    Private Sub Indexar()
        Dim result As Result
        Dim ex As Exception
        Dim timeOutError As Boolean = False
        Dim errorCount As Int32 = 0
        Try
            'Obtengo los documentos a indexar
            Dim dsDocs As DataSet = Server.Con.ExecuteDataset(System.Data.CommandType.StoredProcedure, "zsp_GetDocIdAndDTIDtoIndex")

            'Realizo la indexación con los datos obtenidos
            For Each row As DataRow In dsDocs.Tables(0).Rows
                'Do
                Try
                    ''Hay que hacer un sleep porque sino hay exceptions de max pool en base de datos
                    'Threading.Thread.CurrentThread.Sleep(1000)

                    'Obtiene el result
                    result = Results_Business.GetResult(row.Item("DocId"), row.Item("DTID"))
                    'Indexación
                    Results_Business.InsertSearchIndexData(result)

                    If _salir Then
                        Exit For
                    End If

                    indexedDocsCount += 1
                    Me.Invoke(New DSetCResultCount(AddressOf SetCResultCount))
                    ProgressBar1.Invoke(New DSetCResultCount(AddressOf SetCResultCountProgressBar))

                    timeOutError = False
                    errorCount = 0

                Catch ex
                    ''Si despues de 3 veces vuelve a fallar se loguea la exception y continua
                    'If errorCount = 3 Then
                    '    ZClass.raiseerror(ex)
                    '    timeOutError = False
                    '    errorCount = 0
                    'Else
                    '    'Si el error fué de timeout hace una espera de 3 segundos y reintenta.
                    '    If ex.Message.Contains("timeout") Then
                    '        Threading.Thread.Sleep(3000)
                    '        timeOutError = True
                    '        errorCount += 1
                    '    End If
                    'End If

                    ZClass.raiseerror(ex)

                End Try
                'Loop While timeOutError = True

            Next
        Catch ex
            ZClass.raiseerror(ex)
        End Try
        MessageBox.Show("El proceso de indexación ha finalizado con éxito", "Exito en la indexación")
    End Sub

    ''' <summary>
    ''' Obtiene la cantidad de documentos a indexar
    ''' </summary>
    ''' <returns>Cantidad de documentos a indexar (Int32)</returns>
    ''' <remarks></remarks>
    ''' <history>[Tomas] - 28/04/2009 - Created</history>
    Private Function GetResultCount() As Int32
        Dim count As Int32 = 0
        Try
            count = DirectCast(Server.Con.ExecuteScalar(CommandType.StoredProcedure, "zsp_GetDocCountToIndex"), Int32)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return count
    End Function

    Sub SetCResultCountProgressBar()
        ProgressBar1.Value = ProgressBar1.Value + 1
    End Sub

    Sub SetCResultCount()
        Me.lblIndexedDocs.Text = indexedDocsCount
        'Me.counter4 = counter51.ToString()
    End Sub

    Private Sub frmIndexer_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        _salir = True
        th.Join()
    End Sub


#Region "Métodos y eventos antiguos"
    'Private Function GetDoctypesIds(ByVal doctypes As DSDOCTYPE) As List(Of Int64)
    '    Dim dtids As New List(Of Int64)
    '    For Each dr As DSDOCTYPE.DOC_TYPERow In doctypes.DOC_TYPE
    '        dtids.Add(dr.DOC_TYPE_ID)
    '    Next
    '    doctypes = Nothing
    '    Return dtids
    'End Function
    'Private Sub GetResultCount(ByRef dtids As List(Of Int64))
    '    Dim count As Int64 = 0
    '    For Each id As Int64 In dtids
    '        Try
    '            Dim query As String = "Select count(*) from doc_t" & id
    '            count += Server.Con.ExecuteScalar(CommandType.Text, query)
    '        Catch ex As Exception
    '            ZClass.raiseerror(ex)
    '        End Try
    '    Next

    '    lblDocsToIndex.Text = count

    '    ProgressBar1.Minimum = 0
    '    ProgressBar1.Maximum = count
    '    ProgressBar1.Step = count
    'End Sub
    'Private Sub DIndexResults(ByVal State As Object)
    '    Dim dtid As Int64 = State(0)

    '    IndexResults(dtid)
    'End Sub


    'Private Sub IndexResults(ByVal DsResults As DataSet)
    '    'Dim results As DsResults
    '    Dim result As Result
    '    'Dim tablename As String = String.Empty
    '    'tablename = CONS_TABLENAME & dtid

    '    For Each row As DataRow In DsResults.Tables(0).rows
    '        'Obtiene el result
    '        result = Results_Business.GetResult(row.Item(COL_DOC_ID), row.Item(COL_DOCTYPE_ID))

    '        'Indexa
    '        Results_Business.InsertSearchIndexData(result)

    '        Me.Invoke(New DSetCResultCount(AddressOf SetCResultCount))
    '        ProgressBar1.Invoke(New DSetCResultCount(AddressOf SetCResultCountProgressBar))
    '    Next

    '    'obtiene los results del doctype
    '    'results = Results_Business.GetDocuments(dtid)
    '    'counter51 = results.Tables(tablename).Rows.Count
    '    'For Each row As DataRow In results.Tables(tablename).Rows
    '    '    Try
    '    '        'hay que hacer un sleep porque sino hay exceptions de max pool en base de datos
    '    '        Threading.Thread.CurrentThread.Sleep(1000)

    '    '        'Obtiene el result
    '    '        result = Results_Business.GetResult(row.Item(COL_DOC_ID), dtid)

    '    '        'Indexa
    '    '        Results_Business.InsertSearchIndexData(result)
    '    '        counter5 += 1
    '    '        If _salir Then
    '    '            Exit For
    '    '        End If
    '    '    Catch ex As Exception
    '    '        ZClass.raiseerror(ex)
    '    '    End Try
    '    '    Me.Invoke(New DSetCResultCount(AddressOf SetCResultCount))
    '    '    ProgressBar1.Invoke(New DSetCResultCount(AddressOf SetCResultCountProgressBar))
    '    'Next
    'End Sub
    'Private Sub SetInitialState(ByVal doctypes As DSDOCTYPE)
    '    Dim UserId As Int64 = 1917
    '    UserBusiness.Rights.ValidateLogIn(UserId)

    '    btnIndexar.Enabled = False
    'End Sub
#End Region

End Class
