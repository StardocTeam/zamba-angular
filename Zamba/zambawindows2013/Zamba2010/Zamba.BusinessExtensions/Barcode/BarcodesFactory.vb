'Public Class BarcodesFactory

'''' <summary>
'''' Devuelve una tabla con un informe de caratulas
'''' </summary>
'''' <returns>Informe caratulas</returns>
'   Public Shared Function getInforme() As DataSet

'       Try
'           Dim resultado As DataSet

'           ' se ejecuta el store procedure que devuelve el informe....
'           If Server.ServerType = DBTYPES.Oracle OrElse _
'           Server.ServerType = DBTYPES.Oracle9 OrElse _
'           Server.ServerType = DBTYPES.OracleClient Then
'               '''Dim parNames() As String = {"IPJOBDocTypeId", "io_cursor"}
'               ''Dim parTypes() As Object = {13, 5}
'               'Dim parValues() As Object = {Process.DocType.Id, 2}


'               resultado = Server.Con.ExecuteDataset(CommandType.Text, _
'               "select * from ZVW_ReporteCaratulas order by " & _
'               Chr(34) & "Nro de Caratula" & Chr(34) & " desc, " & _
'               Chr(34) & "Nombre de la entidad" & Chr(34) & " asc, " & _
'               Chr(34) & "Nro de Caja" & Chr(34) & " desc")
'           Else
'               resultado = Server.Con.ExecuteDataset(CommandType.Text, _
'               "select * from ZVW_ReporteCaratulas order by  " & _
'               "[Nro de Caratula] desc," & _
'               "[Nombre de la entidad] asc," & _
'               "[Nro de caja] desc")
'           End If


'           ' Si no hay resultados se devuelve una tabla vacia...
'           If IsNothing(resultado) Or _
'            resultado.Tables.Count = 0 Or _
'            resultado.Tables(0).Rows.Count = 0 Then
'               Return Nothing
'           End If

'           Return resultado

'       Catch ex As Exception
'           zamba.core.zclass.raiseerror(ex)
'           Return Nothing
'       End Try
'   End Function

'   Public Shared Function GetAutoCompleteIndexs(ByVal docTypeId as Int64) As DataTable
'       Dim ds As New DataSet
'       Try
'           If Server.ServerType = DBTYPES.MSSQLServer OrElse Server.ServerType = DBTYPES.MSSQLServer7Up Then
'               Dim parValues() As Object = {doctypeid}
'               ds = Server.Con.ExecuteDataset("GetAutocompleteIndexs", parValues)
'               Return ds.Tables(0)
'           Else
'               'TEST ORACLE
'               Dim sql As String = "Select * from ZBarcodeComplete Where doctypeid=" & doctypeid
'               ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
'               Return ds.Tables(0)
'           End If
'       Catch ex As Exception
'       Finally
'           ds.Dispose()
'       End Try
'   End Function

'   ''' -----------------------------------------------------------------------------
'   ''' <summary>
'   ''' Guarda la configuración en una base de datos
'   ''' </summary>
'   ''' <param name="doctypeid"></param>
'   ''' <param name="indexid"></param>
'   ''' <param name="tabla"></param>
'   ''' <param name="col"></param>
'   ''' <param name="esclave"></param>
'   ''' <remarks>
'   ''' </remarks>
'   ''' <history>
'   ''' 	[Hernan]	26/05/2006	Created
'   ''' </history>
'   ''' -----------------------------------------------------------------------------
'   Public Shared Sub Insertar(ByVal docTypeId as Int64, ByVal indexid As Int32, ByVal tabla As String, ByVal col As String, ByVal esclave As Boolean, ByVal condition As String)
'       Try
'           Dim orden As Int16 = GetLastOrden(doctypeid) + 1
'           'Dim orden As Int16 = Me.GetLastOrden(doctypeid)
'           Dim sql As String
'           If Server.ServerType = DBTYPES.MSSQLServer OrElse Server.ServerType = DBTYPES.MSSQLServer7Up Then
'               col = "[" & col & "]"
'           Else
'               col = ControlChars.Quote & col & ControlChars.Quote
'           End If
'           If esclave Then
'               sql = "Insert into ZBarCodeComplete(Doctypeid,Indexid,Tabla,Columna,Clave,Orden,Conditions) Values(" & doctypeid & "," & indexid & ",'" & tabla & "','" & col & "'," & 1 & "," & orden & ",'" & condition & "')"
'           Else
'               sql = "Insert into ZBarCodeComplete(Doctypeid,Indexid,Tabla,Columna,Clave,Orden,Conditions) Values(" & doctypeid & "," & indexid & ",'" & tabla & "','" & col & "'," & 0 & "," & orden & ",'" & condition & "')"
'           End If
'           esclave = False
'           Server.Con.ExecuteNonQuery(CommandType.Text, sql)
'       Catch ex As Exception
'           esclave = False
'           '  zamba.core.zclass.raiseerror(ex)
'       End Try
'   End Sub

'   Public Shared Function GetLastOrden(ByVal dt As Int32) As Int16
'       If IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, "Select max(orden) from ZBarCodeComplete where Doctypeid=" & dt)) Then
'           Return -1
'       Else
'           Return Server.Con.ExecuteScalar(CommandType.Text, "Select max(orden) from ZBarCodeComplete where Doctypeid=" & dt)
'       End If
'   End Function

'   Public Shared Function getIndexKey(ByVal id As Int32) As Zamba.Core.Index
'       Dim index As New Zamba.Core.Index
'       Try
'           Dim sql As String = "Select IndexId from ZBarCodeComplete Where doctypeid=" & id & " and Clave=1"
'           index.ID = Server.Con.ExecuteScalar(CommandType.Text, sql)
'       Catch ex As Exception
'           zamba.core.zclass.raiseerror(ex)
'       End Try
'       Return index
'   End Function

'   Public Shared Function GetAutoIndexs(ByVal dt As Int32, ByVal IndexId As Int32) As DataSet
'       Dim ds As DataSet
'       Dim sql As String = "Select * from ZBarcodeComplete where DocTypeID=" & dt & " and Indexid=" & IndexId & " and Clave=1"
'       ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
'       If ds.Tables(0).Rows.Count = 0 Then
'           Return Nothing
'       End If
'       Return ds
'   End Function

'   Public Shared Function LoadRemarks(ByVal UserId As Integer) As ArrayList
'       Dim ds As New DataSet
'       Dim oLoadRemarks As New ArrayList
'       Try
'           Dim strSelect As String = "SELECT USERID,REMARK,ORDER FROM zbarcode_remark WHERE USERID =" & UserId & " order by 3"
'           ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
'           For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
'               Dim bcremark As New BarcodeRemark
'               bcremark.UserId = ds.Tables(0).Rows(i).Item(0)
'               bcremark.Remark = ds.Tables(0).Rows(i).Item(1)
'               bcremark.Order = ds.Tables(0).Rows(i).Item(2)
'               oLoadRemarks.Add(bcremark)
'           Next
'       Catch ex As Exception
'           ' zamba.core.zclass.raiseerror(ex)
'       End Try
'       Return oLoadRemarks
'   End Function

'   Public Shared Sub SaveRemark(ByVal UserId As Integer, ByVal remark As String)
'       If remark = "" Then Exit Sub
'       Try
'           'subo el order de todos en 1
'           Dim strUpdate As String = "UPDATE zbarcode_remark SET zbarcode_remark.order = zbarcode_remark.order + 1 where userid=" & UserId
'           Server.Con.ExecuteNonQuery(CommandType.Text, strUpdate)

'           'borro el order que sea igual a 6
'           Dim strDelete As String = "DELETE FROM zbarcode_remark WHERE zbarcode_remark.order => 6 and userid=" & UserId
'           Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)

'           'inserto el nuevo comentario en la posicion 1
'           Dim strInsert As String = "INSERT INTO zbarcode_remark VALUES(" & UserId & ",'" & remark & "',1)"
'           Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)

'       Catch ex As Exception
'           zamba.core.zclass.raiseerror(ex)
'       End Try
'   End Sub



'   ''' -----------------------------------------------------------------------------
'   ''' <summary>
'   ''' Construye y devuelve una consulta SQL.
'   ''' </summary>
'   ''' <param name="doctypeid"></param>
'   ''' <param name="indexvalue"></param>
'   ''' <returns></returns>
'   ''' <remarks>
'   ''' </remarks>
'   ''' <history>
'   ''' 	[Hernan]	26/05/2006	Created
'   ''' 	[Msrcelo]	29/09/2007	MOdified: se comento el si es numerico ejecutar sin comillas, si es numerico y tiene comillas lo ejecuta igual
'   ''' </history>
'   ''' -----------------------------------------------------------------------------
'   Public Shared Function GetSql(ByVal docTypeId as Int64, ByVal indexvalue As Object) As String
'       Dim tabla As String
'       Dim ds As DataSet
'       Dim i As Int32
'       Dim str As String
'       Dim sql As New System.Text.StringBuilder

'       Try
'           sql.Append("Select indexid,tabla,columna from zbarcodecomplete where doctypeid=")
'           sql.Append(doctypeid)
'           sql.Append(" order by orden")
'           ds = Server.Con.ExecuteDataset(CommandType.Text, sql.ToString)
'           sql.Remove(0, sql.Length)
'           sql.Append("Select ")

'           For i = 0 To ds.Tables(0).Rows.Count - 1
'               If i > 0 Then sql.Append(", ")
'               sql.Append(ds.Tables(0).Rows(i).Item(1))
'               sql.Append(".")
'               sql.Append(ds.Tables(0).Rows(i).Item(2))
'           Next

'           sql.Append(" from ")
'           tabla = ds.Tables(0).Rows(0).Item(1)
'           sql.Append(tabla)
'           'Corte de control
'           For i = 0 To ds.Tables(0).Rows.Count - 1
'               If tabla <> ds.Tables(0).Rows(i).Item(1) AndAlso Not IsDBNull(ds.Tables(0).Rows(i).Item(1)) Then
'                   sql.Append(", " & ds.Tables(0).Rows(i).Item(1))
'                   tabla = ds.Tables(0).Rows(i).Item(1)
'               End If
'           Next
'           str = "Select indexid,columna,conditions from zbarcodecomplete where doctypeid=" & doctypeid & " and clave=1 order by orden"
'           ds = Server.Con.ExecuteDataset(CommandType.Text, str)
'           If ds.Tables(0).Rows.Count = 0 Then
'               Throw New Exception("No hay campo clave para buscar")
'           End If
'           sql.Append(" Where " & ds.Tables(0).Rows(0).Item(1) & " " & ds.Tables(0).Rows(0).Item(2) & " ")
'           'If IsNumeric(indexvalue) Then
'           '    sql.Append(indexvalue)
'           'Else
'           If IsDate(indexvalue) Then
'               sql.Append(Server.Con.ConvertDate(indexvalue))
'           Else
'               sql.Append("'" & indexvalue & "'")
'           End If
'           'End If
'           Return sql.ToString
'       Catch ex As Exception
'           Throw ex
'       Finally
'           sql.Remove(0, sql.Length)
'           sql = Nothing
'       End Try
'   End Function

'   Public Shared Function GetSentencia(ByVal docTypeId as Int64, ByVal DataTemp As String) As DataSet
'       Dim sql As String
'       Dim ds As DataSet
'       sql = BarcodesFactory.GetSql(DocTypeId, DataTemp)
'       ZTrace.WriteLineIf(ZTrace.IsInfo,"AUTOCOMPLETAR: " & sql)
'       ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
'       Return ds
'   End Function

'   Public Shared Function GetDsIndexs(ByVal docTypeId as Int64) As DataSet

'       Dim strsql As String = "Select Indexid, orden, Clave from ZBarCodeComplete where Doctypeid=" & DocTypeId & " Order by Orden ASC"  ' and Clave=0
'       'Dim strsql As String = "Select Indexid from ZBarCodeComplete where Doctypeid=" & Me.DocTypeId & " and Clave=0"
'       Dim dsindexs As DataSet = Nothing
'       Try
'           ZTrace.WriteLineIf(ZTrace.IsInfo,"StrSQL: " & strsql)
'           dsindexs = Server.Con.ExecuteDataset(CommandType.Text, strsql)
'       Catch ex As Exception
'           '           zamba.core.zclass.raiseerror(ex)
'           ZTrace.WriteLineIf(ZTrace.IsInfo,"ERROR: No se pudo realizar la consulta SQL: " & strsql)
'       End Try
'       Return dsindexs
'   End Function


'   'Public Shared Function GetReporte() As DataSet


'   '	Return Nothing
'   'End Function














'   Public Overloads Shared Function getZBARCODECOMPLETE() As DataSet
'       'TODO store: SPGetZBarCodeComp
'       'Dim strSelect As String = "select * from zbarcodecomplete where doctypeid=" & cbodoctype.SelectedValue.ToString
'       Dim strSelect As String = "SELECT * FROM ZBARCODECOMPLETE"
'       Return Servers.Server.Con.ExecuteDataset(CommandType.Text, strSelect)
'   End Function
'   Public Overloads Shared Function getZBARCODECOMPLETE(ByVal doctypeid As String) As DataSet
'       'TODO store: SPGetZBarCodeCompByDocT
'       Dim strSelect As String = "select * from zbarcodecomplete where doctypeid=" & doctypeid
'       Return Servers.Server.Con.ExecuteDataset(CommandType.Text, strSelect)
'   End Function
'   Public Overloads Shared Sub deleteZbarcodecomplete(ByVal sp_DocTypeid As String, ByVal index As String)
'       Dim strdelete As String = "delete from zbarcodecomplete where doctypeid=" & sp_DocTypeid & " and Columna='" & index & "'"
'       Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
'   End Sub
'   Public Overloads Shared Sub deleteZbarcodecomplete(ByVal sp_DocTypeid As String)
'       Dim strdelete As String = "delete from zbarcodecomplete where doctypeid=" & sp_DocTypeid
'       Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
'   End Sub
'   Public Overloads Shared Sub deleteZbarcodecomplete()
'       Dim strdelete As String = "DELETE FROM ZBARCODECOMPLETE"
'       Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
'   End Sub
'   Public Shared Sub insertZbarcodecomplete(ByVal psClave As Boolean, ByVal sp_DocTypeid As String, ByVal psIndexid As String, ByVal psTabla As String, ByVal psColumna As String, ByVal newOrden As String)
'       Dim values As String
'       If psClave Then
'           values = "(" & sp_DocTypeid & "," & psIndexid & ",'" & psTabla & "','" & psColumna & "',1," & newOrden & ")"
'       Else
'           values = "(" & sp_DocTypeid & "," & psIndexid & ",'" & psTabla & "','" & psColumna & "',0," & newOrden & ")"
'       End If
'       Servers.Server.Con.ExecuteNonQuery(CommandType.Text, "insert into zbarcodecomplete (doctypeid,indexid,tabla,columna,clave,orden) values " & values)
'   End Sub


'   ''' <summary>
'   ''' Devuelve la sentencia SELECT-FROM de las consultas de Caratulas 
'   ''' </summary>
'   ''' <returns></returns>
'   ''' <remarks></remarks>
'   Private Shared Function QuerySelectCaratulas() As String
'       Dim cadena As New System.Text.StringBuilder(370, 600)
'       cadena.Append(" SELECT usrtable.name as Usuario, zbarcode.id as Caratula,")
'       cadena.Append(" doc_type.doc_type_name as Documento, zbarcode.fecha as Fecha, zbarcode.scanned as Escaneado,")
'       cadena.Append(" zbarcode.box as Caja, zbarcode.batch as Lote, zbarcode.doc_type_id, zbarcode.doc_id")
'       cadena.Append(" FROM zbarcode INNER JOIN usrtable ON zbarcode.userid = usrtable.id  INNER JOIN doc_type ON doc_type.doc_type_id = zbarcode.doc_type_id")

'       Return cadena.ToString()
'   End Function

'   Public Shared Function dsFilterCaratulas(ByVal UserId As Integer) As DataTable
'       Dim cadena As New System.Text.StringBuilder()
'       cadena.Append(QuerySelectCaratulas())
'       cadena.Append(" WHERE zbarcode.userid =")
'       cadena.Append(UserId.ToString())
'       cadena.Append(" ORDER BY zbarcode.fecha")

'       Return Server.Con.ExecuteDataset(CommandType.Text, cadena.ToString()).tables(0)
'   End Function
'   Public Shared Function dsFilterCaratulas(ByVal UserId As Integer, ByVal fecha As DateTime) As DataTable
'       Return dsFilterCaratulas(UserId, fecha, fecha)
'   End Function
'   Public Shared Function dsFilterCaratulas(ByVal UserId As Integer, ByVal fechaInicial As DateTime, ByVal fechaFinal As DateTime) As DataTable
'       Dim cadena As New System.Text.StringBuilder
'       cadena.Append(QuerySelectCaratulas())
'       cadena.Append(" WHERE zbarcode.userid = ")
'       cadena.Append(UserId)
'       cadena.Append(" AND fecha BETWEEN ")
'       cadena.Append(Server.Con.ConvertDate(fechaInicial))
'       cadena.Append(" AND ")
'       cadena.Append(Server.Con.ConvertDate(fechaFinal.AddDays(1.0)))
'       cadena.Append(" ORDER BY zbarcode.fecha")

'       Return Server.Con.ExecuteDataset(CommandType.Text, cadena.ToString()).Tables(0)
'   End Function

'   Public Shared Function dsFilterCaratulas(ByVal fechaInicial As DateTime, ByVal fechaFinal As DateTime) As DataTable
'       Dim cadena As New System.Text.StringBuilder
'       cadena.Append(QuerySelectCaratulas())
'       cadena.Append(" WHERE fecha BETWEEN ")
'       cadena.Append(Server.Con.ConvertDate(fechaInicial))
'       cadena.Append(" AND ")
'       cadena.Append(Server.Con.ConvertDate(fechaFinal.AddDays(1.0)))
'       cadena.Append(" ORDER BY zbarcode.fecha")
'       Return Server.Con.ExecuteDataset(CommandType.Text, cadena.ToString()).Tables(0)
'   End Function

'   Public Shared Function dsAllCaratulas() As DataTable
'       Return Server.Con.ExecuteDataset(CommandType.Text, QuerySelectCaratulas() & " order by zbarcode.id").tables(0)
'   End Function

'   Private Shared Function getStartDate(ByVal p_date As DateTime) As String
'       Dim local_date As DateTime = New DateTime(p_date.Year, p_date.Month, p_date.Day, 0, 0, 0)
'       Return local_date.ToString()
'   End Function

'   Private Shared Function getEndDate(ByVal p_date As DateTime) As String
'       Dim local_date As DateTime = New DateTime(p_date.Year, p_date.Month, p_date.Day, 23, 59, 59)
'       Return local_date.ToString()
'   End Function

'End Class
