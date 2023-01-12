Imports Zamba.Servers
Imports Zamba.AppBlock
Public Class csCaratulasIngresadas
    ' Private WithEvents frmUsrFecha As New frmUSrFRecha
    Private FechaInicial, FechaFinal As New Date
    Public UsrId As Int32
    Private Const STRING_SELECT As String = "SELECT zbarcode.ID, zbarcode.FECHA,  doc_type_name,description, SCANNED, SCANNEDDATE, DOC_ID, BATCH , boX  FROM ZBARCODE, usrtable, doc_type where zbarcode.userid=usrtable.id and doc_type.doc_type_id=zbarcode.doc_type_id and zbarcode.FECHA BETWEEN "
    Public Function cargarDataSet() As dsCaratulasIngresadas
        Dim Ds As New DataSet
        Dim DsT As New dsCaratulasIngresadas
        Dim sql, sNombreTabla As String
        Dim FechaI As String = ""
        Dim FechaF As String = ""

        'Revisar, hay que eliminar el uso de formularios desde clases
        'Try
        '    frmUsrFecha.ShowDialog()
        'Catch ex As Exception
        '    ZException.Log(ex, False)
        'End Try

        If Server.ServerType = Server.DBTYPES.Oracle9 Or Server.ServerType = Server.DBTYPES.Oracle Or Server.ServerType = Server.DBTYPES.OracleClient Then
            FechaI = Server.Con.ConvertDate(FechaInicial.ToString)
            FechaF = Server.Con.ConvertDate(FechaFinal.ToString)
            Try
                If UsrId > 0 Then
                    sql = STRING_SELECT & FechaI & " AND " & FechaF & " AND zbarcode.userid=" & UsrId.ToString
                Else
                    sql = STRING_SELECT & FechaI & " AND " & FechaF
                End If
                Ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Catch ex As Exception
                ZException.Log(ex, False)
            End Try
        Else
            Try
                sql = STRING_SELECT & FechaI & " AND " & FechaF
                Ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Catch ex As Exception
                ZException.Log(ex, False)
            End Try
        End If
        sNombreTabla = DsT.Tables(0).TableName
        DsT.Tables(0).TableName = Ds.Tables(0).TableName
        DsT.Merge(Ds)
        DsT.Tables(0).TableName = sNombreTabla
        Return DsT
    End Function
    'Revisar, hay que eliminar el uso de formularios desde clases
    'Private Sub tomarUsrFechas(ByVal FechaDesde As Date, ByVal FechaHasta As Date, ByVal UsrId As Int32) Handles frmUsrFecha.Aceptar
    '    FechaInicial = FechaDesde
    '    FechaFinal = FechaHasta
    '    Me.UsrId = UsrId
    'End Sub

End Class
