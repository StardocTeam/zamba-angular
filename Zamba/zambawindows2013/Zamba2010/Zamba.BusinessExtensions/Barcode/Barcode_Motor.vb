'Imports Zamba.Barcodes
Imports System.Drawing


Public Class Barcode_Motor
    Inherits ZClass

    Public Overrides Sub Dispose()
    End Sub

    '''' -----------------------------------------------------------------------------
    '''' <summary>
    '''' Inserta la caratula en la base de Zamba
    '''' </summary>
    '''' <param name="newresult"></param>
    '''' <param name="DocTypeId"></param>
    '''' <param name="UserId"></param>
    '''' <param name="CaratulaId"></param>
    '''' <returns></returns>
    '''' <remarks>
    '''' </remarks>
    '''' <history>
    '''' 	[Hernan]	26/05/2006	Created
    '''' </history>
    '''' -----------------------------------------------------------------------------
    'Public Shared Function Insert(ByVal newresult As NewResult, ByVal docTypeId as Int64, ByVal UserId As Int32, ByVal CaratulaId As Int32) As Boolean

    '    'Inserto un newresult
    '    Dim docId As Long
    '    Try
    '        Dim insertresult As Results_Factory.InsertResult
    '        insertresult = Results_Factory.insertdocument(newresult, False, False, False, False)
    '        docId = newresult.Id
    '        If insertresult = Results_Factory.InsertResult.NoInsertado Then Return False
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '        Return False
    '    End Try

    '    'inserto en ZBarCode
    '    Try
    '        if Server.IsOracle then
    '            ''Dim parNames() As String = {"idbarcode", "DocTypeId", "UserId", "Doc_Id"}
    '            'Dim parTypes() As Object = {10, 10, 10, 10}
    '            Dim parValues() As Object = {CaratulaId, DocTypeId, UserId, docId}
    '            'Server.Con.ExecuteNonQuery("INSERT_ZBarcode_PKG.Insert_ZBarCode",  parValues)
    '            Server.Con.ExecuteNonQuery("zsp_barcode_100.InsertBarCode",  parValues)
    '        Else
    '            Dim parameters() = {CaratulaId, DocTypeId, UserId, docId}
    '            'Server.Con.ExecuteNonQuery("Insert_ZBarCode", parameters)
    '            Server.Con.ExecuteNonQuery("zsp_barcode_100_InsertBarCode", parameters)
    '        End If
    '    Catch ex As Exception
    '       zamba.core.zclass.raiseerror(ex)
    '        Return False
    '    End Try
    '    Return True
    'End Function
    '''' -----------------------------------------------------------------------------
    '''' <summary>
    '''' Borra una caratula de Zamba
    '''' </summary>
    '''' <param name="CaratulaId"></param>
    '''' <remarks>
    '''' </remarks>
    '''' <history>
    '''' 	[Hernan]	26/05/2006	Created
    '''' </history>
    '''' -----------------------------------------------------------------------------
    'Public Shared Sub Delete(ByVal CaratulaId As Int32)
    '    Dim DocId As New ArrayList
    '    Dim DocTypeId As New ArrayList
    '    Dim result As New ArrayList

    '    'Obtengo DocId y DocTypeId en base al CaratulaId
    '        Dim strSelect As String = "Select doc_id,doc_type_id from zbarcode where id=" & CaratulaId
    '        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
    '        'si tiene replicas pregunto si quiere seguir eliminando
    '        If ds.Tables(0).Rows.Count > 1 Then
    '            If MessageBox.Show("La caratula tiene replicas y se eliminaran las mismas, desea continuar ?      ", "Generador de Codigo de Barras", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
    '                Exit Sub
    '            End If
    '        End If
    '        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
    '        DocId.Add(ds.Tables(0).Rows(i).Item(0))
    '        DocTypeId.Add(ds.Tables(0).Rows(i).Item(1))
    '        DocId.Add(ds.Tables(0).Rows(i).Item(0))
    '            DocTypeId.Add(ds.Tables(0).Rows(i).Item(1))
    '        Next


    '    'obtengo results

    '        For i As Integer = 0 To DocId.Count - 1
    '            Dim result1 As result = Results_Factory.GetNewResult(DocId(i), DocTypeId(i))
    '            result.Add(result1)
    '        Next


    '    'borro el documento

    '        For i As Integer = 0 To result.Count - 1
    '            Results_Factory.Delete(result(i))
    '        Next


    '    'Borro en ZBarcode
    '    Try
    '        Dim strDelete As String = "Delete From ZBarcode Where Id=" & CaratulaId
    '        Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)
    '    Catch
    '    End Try



    'End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Imprime en la caratula un codigo de barras
    ''' </summary>
    ''' <param name="e"></param>
    ''' <param name="Value"></param>
    ''' <param name="x"></param>
    ''' <param name="y"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function Print(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal Value As String, ByVal x As Integer, ByVal y As Integer) As Graphics
        Dim bc As New PrintControl.PrintBarcodes
        bc.BarCode = Value
        bc.HeaderText = ""
        bc.VertAlign = PrintControl.PrintBarcodes.AlignType.Left
        bc.LeftMargin = x
        bc.TopMargin = y
        e = bc.PrintImage(e)
        Return e.Graphics
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Agrega ceros al value del barcode 
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function convertCodeBar(ByVal data As Integer) As String
        Dim s As New System.Text.StringBuilder
        Try
            s.Append(data)
            If s.Length <= 9 Then
                s.Insert(0, "0", 9 - s.Length)
            End If
            Return s.ToString
        Finally
            s = Nothing
        End Try
    End Function
End Class
