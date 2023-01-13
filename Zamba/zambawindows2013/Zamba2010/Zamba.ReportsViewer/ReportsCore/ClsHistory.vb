Imports Zamba.Servers
Imports Zamba.AppBlock
'' Imports Zamba.ReportsCore

Public Class ClsHistory

    Public Shared Function HistorialUsuario(ByVal Userid As Int32) As DataSet
        Dim Ds As New DsHistory
        Try
            Dim dstemp As DataSet
            If Server.ServerType = Server.DBTYPES.Oracle9 Or Server.ServerType = Server.DBTYPES.Oracle Or Server.ServerType = Server.DBTYPES.OracleClient Then
                Dim parNames() As String = {"UserId", "io_cursor"}
                Dim parTypes() As Object = {13, 5}
                Dim parValues() As Object = {Userid, 2}
                'dstemp = Server.Con(True).ExecuteDataset("CLSACTIONS_GETUSERACTIONS_PKG.getUserActions", parNames, parTypes, parValues)
                dstemp = Server.Con(True).ExecuteDataset("zsp_users_100.GetUserActions", parNames, parTypes, parValues)
                dstemp.Tables(0).TableName = Ds.Tables(0).TableName
            Else
                Dim parvalues() As Object = {Userid}
                'dstemp = Server.Con(True).ExecuteDataset("ClsActions_GetUserActions", parvalues)
                dstemp = Server.Con(True).ExecuteDataset("zsp_users_100_GetUserAction", parvalues)
                dstemp.Tables(0).TableName = Ds.Tables(0).TableName
            End If
            Ds.Merge(dstemp)
            dstemp.Dispose()
            Try
                Dim row As DsHistory.dsUsersRow = Ds.dsUsers.NewdsUsersRow
                row.Usuario = GetUserName(Userid)
                Ds.dsUsers.Rows.Add(row)
                Ds.AcceptChanges()
            Catch ex As Exception
            End Try
        Catch ex As Exception
            ZException.Log(ex)
        End Try
        Return Ds
    End Function

    Private Shared Function GetUserName(ByVal Id As Int32) As String
        Try
            Dim sql As String = "Select (Apellido + ' ' + Nombres + ' (' + Name + ')') from usrtable where id=" & Id
            Return Server.Con.ExecuteScalar(CommandType.Text, sql)
        Catch ex As Exception
            Return ""
        End Try
        Return ""
    End Function

    Public Shared Function GetDocumentActions(ByVal DocumentId As Integer) As DataSet 'DSActions
        Dim DsActions As New DsActions
        Dim Table As String = "VisHistory"
        If Server.ServerType = Server.DBTYPES.Oracle9 Or Server.ServerType = Server.DBTYPES.Oracle Or Server.ServerType = Server.DBTYPES.OracleClient Then
            Dim dstemp As DataSet
            Dim parNames() As String = {"DocumentId", "io_cursor"}
            Dim parTypes() As Object = {13, 5}
            Dim parValues() As Object = {DocumentId, 2}
            'dstemp = Server.Con(True).ExecuteDataset("GETDOCUMENTACTIONS_PKG.GetDocumentActions", parNames, parTypes, parValues)
            dstemp = Server.Con(True).ExecuteDataset("zsp_doctypes_100.GetDocumentActions", parNames, parTypes, parValues)
            dstemp.Tables(0).TableName = Table
            DsActions.Merge(dstemp)
        Else
            Dim dstemp As DataSet
            Dim parameters() As Object = {DocumentId}
            'dstemp = Server.Con(True).ExecuteDataset("ClsActions_GetDocumentActions", parameters)
            dstemp = Server.Con(True).ExecuteDataset("zsp_users_100_GetDocumentAction", parameters)
            dstemp.Tables(0).TableName = Table
            'DsActions.Merge(dstemp)
            Return dstemp
        End If
        Return DsActions
    End Function

#Region "Codigo de Barras"

#Region "Por fecha"
    ' Dim WithEvents frmDates As frmDates 'Revisar, hay que eliminar el uso de formularios desde clases
    Dim date1 As Date
    Dim date2 As Date
    Public Function ScannedBarcodeByDate() As DataSet
        Dim ds As New DsBarcode
        Dim dsTemp As DataSet
        Dim sql As String

        'Revisar, hay que eliminar el uso de formularios desde clases
        'Try
        '    frmDates = New frmDates
        '    frmDates.StartPosition = FormStartPosition.CenterScreen
        '    frmDates.ShowDialog()
        'Catch ex As Exception
        'End Try

        Try
            sql = "Select usrtable.name as Usuario,zbarcode.id as Caratula,doc_type_name as doc_type,fecha as create_date,scanneddate as scanned_date,batch,Box from zbarcode,doc_type,usrtable where scanned='SI' and doc_type.doc_type_id = zbarcode.doc_type_id and zbarcode.userid = usrtable.id and fecha between " + Server.Con.ConvertDate(date1.ToString()) + " and " + Server.Con.ConvertDate(date2.ToString()) + " order by 4"
            dsTemp = Server.Con.ExecuteDataset(CommandType.Text, sql)
            dsTemp.Tables(0).TableName = ds.Barcode.TableName
            ds.Merge(dsTemp)
            Dim sqlCount As String = "Select count (distinct caratula) from (" & sql & ")"
            Dim c As Int32 = Server.Con.ExecuteScalar(CommandType.Text, sqlCount)
            ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(7) = c
            ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(8) = ds.Tables(0).Rows.Count - c
        Catch ex As Exception
        End Try
        Return ds
    End Function
    'Revisar, hay que eliminar el uso de formularios desde clases
    'Private Sub frmdates_fechas(ByVal Desde As Date, ByVal Hasta As Date) Handles frmDates.Fechas
    '    MyClass.date1 = Desde
    '    MyClass.date2 = Hasta
    'End Sub
#End Region

#Region "Por Lote"
    ' Dim WithEvents frmbatch As frmbatch
    Dim lote() As String
    Dim allBatches As Boolean = False
    Public Function ScannedBarcodeByBatch() As DataSet
        Dim ds As New DsBarcode
        Dim dsTemp As DataSet
        Dim sql As String
        'Revisar, hay que eliminar el uso de formularios desde clases
        'Try
        '    frmbatch = New frmbatch
        '    frmbatch.StartPosition = FormStartPosition.CenterScreen
        '    frmbatch.ShowDialog()
        'Catch ex As Exception
        'End Try
        Try
            If MyClass.allBatches = True Then
                sql = "Select usrtable.name as Usuario,zbarcode.id as Caratula,doc_type_name as doc_type,fecha as create_date,scanneddate as scanned_date,batch,Box from zbarcode,doc_type,usrtable where scanned='SI' and doc_type.doc_type_id = zbarcode.doc_type_id and zbarcode.userid = usrtable.id order by 4"
            Else
                Dim lotebuilder As String = ""
                For i As Int32 = 0 To lote.Length - 1
                    If i = 0 Then
                        lotebuilder = " Batch='" & MyClass.lote(i) & "'"
                    Else
                        lotebuilder += " or Batch='" & MyClass.lote(i) & "'"
                    End If
                Next
                lotebuilder = "(" & lotebuilder & ")"
                sql = "Select usrtable.name as Usuario,zbarcode.id as Caratula,doc_type_name as doc_type,fecha as create_date,scanneddate as scanned_date,batch,Box from zbarcode,doc_type,usrtable where scanned='SI' and doc_type.doc_type_id = zbarcode.doc_type_id and zbarcode.userid = usrtable.id and " & lotebuilder & " order by 4"
            End If
            dsTemp = Server.Con.ExecuteDataset(CommandType.Text, sql)
            dsTemp.Tables(0).TableName = ds.Barcode.TableName
            ds.Merge(dsTemp)
            Dim sqlCount As String = "Select count (distinct caratula) from (" & sql & ")"
            Dim c As Int32 = Server.Con.ExecuteScalar(CommandType.Text, sqlCount)
            ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(7) = c
            ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(8) = ds.Tables(0).Rows.Count - c
        Catch ex As Exception
        End Try
        Return ds
    End Function
    'Revisar, hay que eliminar el uso de formularios desde clases
    'Private Sub frmbatch_batch(ByVal dato() As String, ByVal all As Boolean) Handles frmbatch.Batch
    '    If all = False Then
    '        MyClass.lote = dato
    '    Else
    '        allBatches = True
    '    End If
    'End Sub
#End Region

#Region "Busqueda Avanzada"
    Public Shared Function BarcodeAll(ByVal opt As Int16, ByVal UserId As Int32, ByVal fechaDesdeCreate As Date, ByVal fechaHastaCreate As Date, ByVal fechaDesdeScanned As Date, ByVal fechaHastaScanned As Date) As DataSet
        Dim ds As New DsBarcode
        Dim dsTemp As DataSet
        Dim sql As String
        If UserId = -1 Then
            sql = "Select usrtable.name as Usuario,zbarcode.id as Caratula,doc_type_name as doc_type,fecha as create_date,scanneddate as scanned_date,batch,Box from zbarcode,doc_type,usrtable where doc_type.doc_type_id = zbarcode.doc_type_id and zbarcode.userid = usrtable.id and fecha between " + Server.Con.ConvertDate(fechaDesdeCreate.ToString()) + " and " + Server.Con.ConvertDate(fechaHastaCreate.ToString()) + " and scanneddate between " + Server.Con.ConvertDate(fechaDesdeScanned.ToString()) + " and " + Server.Con.ConvertDate(fechaHastaScanned.ToString()) + "  order by 1,2"
        Else
            sql = "Select usrtable.name as Usuario,zbarcode.id as Caratula,doc_type_name as doc_type,fecha as create_date,scanneddate as scanned_date,batch,Box from zbarcode,doc_type,usrtable where doc_type.doc_type_id = zbarcode.doc_type_id and zbarcode.userid = usrtable.id and fecha between " + Server.Con.ConvertDate(fechaDesdeCreate.ToString()) + " and " + Server.Con.ConvertDate(fechaHastaCreate.ToString()) + " and scanneddate between " + Server.Con.ConvertDate(fechaDesdeScanned.ToString()) + " and " + Server.Con.ConvertDate(fechaHastaScanned.ToString()) + " and usrtable.id=" + UserId + " order by 1,2"
        End If
        Try
            dsTemp = Server.Con.ExecuteDataset(CommandType.Text, sql)
            dsTemp.Tables(0).TableName = ds.Barcode.TableName
            Trace.WriteLine("Hago el merge")
            ds.Merge(dsTemp)
            Dim sqlCount As String = "Select count (distinct caratula) from (" & sql & ")"
            Dim c As Int32 = Server.Con.ExecuteScalar(CommandType.Text, sqlCount)
            ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(7) = c
            ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(8) = ds.Tables(0).Rows.Count - c
            Trace.WriteLine("Count: " & ds.Barcode.Rows.Count)
        Catch ex As Exception
            Trace.WriteLine(ex.ToString)
        End Try
        Return ds
    End Function
    Public Shared Function BarcodeNoDigitalizados(ByVal opt As Int16, ByVal UserId As Int32, ByVal fechaDesdeCreate As Date, ByVal fechaHastaCreate As Date) As DataSet
        Dim ds As New DsBarcode
        Dim dsTemp As DataSet
        Dim sql As String
        If UserId = -1 Then
            sql = "Select usrtable.name as Usuario,zbarcode.id as Caratula,doc_type_name as doc_type,fecha as create_date,scanneddate as scanned_date,batch,Box from zbarcode,doc_type,usrtable where doc_type.doc_type_id = zbarcode.doc_type_id and zbarcode.userid = usrtable.id and fecha between " + Server.Con.ConvertDate(fechaDesdeCreate) + " and " + Server.Con.ConvertDate(fechaHastaCreate.ToString()) + " and scanned='NO' order by 1,2"
        Else
            sql = "Select usrtable.name as Usuario,zbarcode.id as Caratula,doc_type_name as doc_type,fecha as create_date,scanneddate as scanned_date,batch,Box from zbarcode,doc_type,usrtable where doc_type.doc_type_id = zbarcode.doc_type_id and zbarcode.userid = usrtable.id and fecha between " + Server.Con.ConvertDate(fechaDesdeCreate) + " and " + Server.Con.ConvertDate(fechaHastaCreate) + " and scanned='NO' and usrtable.id=" + UserId + " order by 1,2"
        End If
        Try
            dsTemp = Server.Con.ExecuteDataset(CommandType.Text, sql)
            dsTemp.Tables(0).TableName = ds.Barcode.TableName
            ds.Merge(dsTemp)
            Dim sqlCount As String = "Select count (distinct caratula) from (" & sql & ")"
            Dim c As Int32 = Server.Con.ExecuteScalar(CommandType.Text, sqlCount)
            ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(7) = c
            ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(8) = ds.Tables(0).Rows.Count - c
        Catch ex As Exception
        End Try
        Return ds
    End Function
    Public Shared Function BarcodeDigitalizados(ByVal opt As Int16, ByVal UserId As Int32, ByVal fechaDesdeCreate As Date, ByVal fechaHastaCreate As Date, ByVal fechaDesdeScanned As Date, ByVal fechaHastaScanned As Date) As DataSet
        Dim ds As New DsBarcode
        Dim dsTemp As DataSet
        Dim sql As String
        If UserId = -1 Then
            sql = "Select usrtable.name as Usuario,zbarcode.id as Caratula,doc_type_name as doc_type,fecha as create_date,scanneddate as scanned_date,batch,Box from zbarcode,doc_type,usrtable where doc_type.doc_type_id = zbarcode.doc_type_id and zbarcode.userid = usrtable.id and fecha between " + Server.Con.ConvertDate(fechaDesdeCreate) + " and " + Server.Con.ConvertDate(fechaHastaCreate) + " and scanneddate between " + Server.Con.ConvertDate(fechaDesdeScanned) + " and " + Server.Con.ConvertDate(fechaHastaScanned) + " and scanned='SI' order by 1,2"
        Else
            sql = "Select usrtable.name as Usuario,zbarcode.id as Caratula,doc_type_name as doc_type,fecha as create_date,scanneddate as scanned_date,batch,Box from zbarcode,doc_type,usrtable where doc_type.doc_type_id = zbarcode.doc_type_id and zbarcode.userid = usrtable.id and fecha between " + Server.Con.ConvertDate(fechaDesdeCreate) + " and " + Server.Con.ConvertDate(fechaHastaCreate) + " and scanneddate between " + Server.Con.ConvertDate(fechaDesdeScanned) + " and " + Server.Con.ConvertDate(fechaHastaScanned) + " and usrtable.id=" + UserId + " and scanned='SI' order by 1,2"
        End If
        Try
            dsTemp = Server.Con.ExecuteDataset(CommandType.Text, sql)
            dsTemp.Tables(0).TableName = ds.Barcode.TableName
            ds.Merge(dsTemp)
            Dim sqlCount As String = "Select count (distinct caratula) from (" & sql & ")"
            Dim c As Int32 = Server.Con.ExecuteScalar(CommandType.Text, sqlCount)
            ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(7) = c
            ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1).Item(8) = ds.Tables(0).Rows.Count - c
        Catch ex As Exception
        End Try
        Return ds
    End Function
#End Region

#End Region

End Class
