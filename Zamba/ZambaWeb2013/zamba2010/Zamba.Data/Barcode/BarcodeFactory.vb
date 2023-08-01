Imports Zamba.Core
Public Class BarcodeFactory
    Public Sub InsertBarCode(ByVal newresultid As Int64, ByVal DocTypeId As Int32, ByVal UserId As Int32, ByVal CaratulaId As Int32)
        If Server.isOracle Then
            Dim parNames() As String = {"idbarcode", "DocTypeId", "UserId", "Doc_Id"}
            ' Dim parTypes() As Object = {10, 10, 10, 10}
            Dim parValues() As Object = {CaratulaId, DocTypeId, UserId, newresultid}
            'Server.Con.ExecuteNonQuery("INSERT_ZBarcode_PKG.Insert_ZBarCode", parValues)
            Server.Con.ExecuteNonQuery("zsp_barcode_100.InsertBarCode", parValues)
        Else
            Dim parameters() As Object = {CaratulaId, DocTypeId, UserId, newresultid}
            'Server.Con.ExecuteNonQuery("Insert_ZBarCode", parameters)
            Server.Con.ExecuteNonQuery("zsp_barcode_100_InsertBarCode", parameters)
        End If
    End Sub
    Public Shared Function GetDocTypeAndDocIdByCaratulaId(ByVal CaratulaId As Int32) As DataSet
        Dim strSelect As String = "Select doc_id,doc_type_id from zbarcode where id=" & CaratulaId
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        Return ds
    End Function
    Public Shared Sub BorroEnZBarcode(ByVal CaratulaId As Int32)
        Dim strDelete As String = "Delete From ZBarcode Where Id=" & CaratulaId
        Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)
    End Sub

    ''' <summary>
    ''' Devuelve una tabla con un informe de caratulas
    ''' </summary>
    ''' <returns>Informe caratulas</returns>
    Public Shared Function getInforme() As DataSet

        Try
            Dim resultado As DataSet

            ' se ejecuta el store procedure que devuelve el informe....
            If Server.isOracle Then
                'Dim parNames() As String = {"IPJOBDocTypeId", "io_cursor"}
                '' Dim parTypes() As Object = {13, 5}
                'Dim parValues() As Object = {Process.DocType.Id, 2}


                resultado = Server.Con.ExecuteDataset(CommandType.Text, _
                "select * from ZVW_ReporteCaratulas order by " & _
                Chr(34) & "Nro de Caratula" & Chr(34) & " desc, " & _
                Chr(34) & "Nombre del Entidad" & Chr(34) & " asc, " & _
                Chr(34) & "Nro de Caja" & Chr(34) & " desc")



                Dim query As New System.Text.StringBuilder
                query.Append("select  zbarcode.ID as  " & Chr(34) & "Nro de Caratula" & Chr(34) & ",")
                query.Append("Doc_Type.Doc_Type_name as " & Chr(34) & "Nombre del Entidad" & Chr(34) & ",")
                query.Append("Count(zbarcode.Doc_Type_Id) as " & Chr(34) & "Nro de Documentos" & Chr(34) & ",")
                query.Append("zbarcode.SCANNED as " & Chr(34) & "Escaneado" & Chr(34) & ",")
                query.Append("zbarcode.Box as " & Chr(34) & "Nro de Caja" & Chr(34) & ",")
                query.Append("zbarcode.batch as " & Chr(34) & "Nro lote" & Chr(34) & ",")
                query.Append("Count(zbarcode.ID) as " & Chr(34) & "Total de Documentos" & Chr(34) & ",")
                query.Append("zbarcode.Doc_Type_Id as " & Chr(34) & "Entidad" & Chr(34) & ",")
                query.Append("zbarcode.Doc_Id as " & Chr(34) & "Nro de Documento" & Chr(34))
                query.Append("from zbarcode inner join Doc_Type  ")
                query.Append("on zbarcode.Doc_Type_id = Doc_Type.Doc_Type_Id")
                query.Append("group by zbarcode.ID,  zbarcode.Doc_Type_Id, zbarcode.BOX, zbarcode.SCANNED, Doc_Type.doc_type_name, zbarcode.Doc_Type_Id,  zbarcode.Doc_Id,  zbarcode.batch ")
                query.Append("order by " & Chr(34) & "Nro de Caratula" & Chr(34) & " desc, " & Chr(34) & "Nombre del Entidad" & Chr(34) & " asc, " & Chr(34) & "Nro de Caja" & Chr(34) & " desc")



            Else
                resultado = Server.Con.ExecuteDataset(CommandType.Text, _
                "select * from ZVW_ReporteCaratulas order by  " & _
                "[Nro de Caratula] desc," & _
                "[Nombre del Entidad] asc," & _
                "[Nro de caja] desc")
            End If


            ' Si no hay resultados se devuelve una tabla vacia...
            If IsNothing(resultado) Or _
             resultado.Tables.Count = 0 Or _
             resultado.Tables(0).Rows.Count = 0 Then
                Return Nothing
            End If

            Return resultado

        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Public Shared Function GetAutoCompleteIndexs(ByVal doctypeid As Int64) As DataTable
        Dim ds As New DataSet
        Try
            If Server.ServerType = DBTypes.MSSQLServer OrElse Server.ServerType = DBTypes.MSSQLServer7Up Then
                Dim parValues() As Object = {doctypeid}
                ds = Server.Con.ExecuteDataset("GetAutocompleteIndexs", parValues)
                Return ds.Tables(0)
            Else
                'TEST ORACLE
                Dim sql As String = "Select * from ZBarcodeComplete Where doctypeid=" & doctypeid
                ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
                Return ds.Tables(0)
            End If
        Catch ex As Exception
        Finally
            ds.Dispose()
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Guarda la configuración en una base de datos
    ''' </summary>
    ''' <param name="doctypeid"></param>
    ''' <param name="indexid"></param>
    ''' <param name="tabla"></param>
    ''' <param name="col"></param>
    ''' <param name="esclave"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    '''     [Gaston]	12/11/2008	Modified    Se obtiene el verdadero orden de la columna
    '''     [Sebastián] 10/12/2008  Modified Se agrego un parametro mas para insertar un where en la consulta
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub Insertar(ByVal doctypeid As Int32, ByVal indexid As Int32, ByVal tabla As String, ByVal col As String, ByVal esclave As Boolean, ByVal indexGroup As Boolean, ByVal FilterByIndex As Boolean, ByVal condition As String, Optional ByVal Where_condition As String = "")

        Try

            'Dim orden As Int16 = GetLastOrden(doctypeid) + 1
            'Dim orden As Int16 = Me.GetLastOrden(doctypeid)
            Dim orden As Int16 = GetPositionOfColumn(tabla, col)

            If Not (IsNothing(orden)) Then

                Dim sql As String
                Dim IdWhereCondition As Int64 = CoreData.GetNewID(IdTypes.FOLDERSID)
                If Server.ServerType = DBTypes.MSSQLServer OrElse Server.ServerType = DBTypes.MSSQLServer7Up Then
                    col = "[" & col & "]"
                Else
                    col = ControlChars.Quote & col & ControlChars.Quote
                End If

                Dim indexGroupString As String = Nothing

                If (indexGroup = True) Then
                    indexGroupString = "1"
                Else
                    indexGroupString = "0"
                End If

                Dim FilterByIndexString As String = Nothing

                If (FilterByIndex = True) Then
                    FilterByIndexString = "1"
                Else
                    FilterByIndexString = "0"
                End If

                If esclave Then
                    sql = "Insert into ZBarCodeComplete(Doctypeid,Indexid,Tabla,Columna,Clave,Orden,Conditions,WhereCondition,IdWhereCondition,IndexGroup,FilterByIndex) Values(" & doctypeid & "," & indexid & ",'" & tabla & "','" & col & "'," & 1 & "," & orden & ",'" & condition & "'" & ",'" & Where_condition & "'," & IdWhereCondition & ", " & indexGroupString & ", " & FilterByIndexString & ")"
                Else
                    sql = "Insert into ZBarCodeComplete(Doctypeid,Indexid,Tabla,Columna,Clave,Orden,Conditions,WhereCondition,IdWhereCondition,IndexGroup,FilterByIndex) Values(" & doctypeid & "," & indexid & ",'" & tabla & "','" & col & "'," & 0 & "," & orden & ",'" & condition & "'" & ",'" & Where_condition & "'," & IdWhereCondition & ", " & indexGroupString & ", " & FilterByIndexString & ")"
                End If

                esclave = False
                Server.Con.ExecuteNonQuery(CommandType.Text, sql)

            End If

        Catch ex As Exception
            esclave = False
            '  zamba.core.zclass.raiseerror(ex)
        End Try

    End Sub

    Public Shared Function GetLastOrden(ByVal dt As Int32) As Int16

        If IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, "Select max(orden) from ZBarCodeComplete where Doctypeid=" & dt)) Then
            Return -1
        Else
            Return Server.Con.ExecuteScalar(CommandType.Text, "Select max(orden) from ZBarCodeComplete where Doctypeid=" & dt)
        End If

    End Function

    ''' <summary>
    ''' Método que sirve para obtener la posición de la columna y devolver el verdadero orden
    ''' </summary>
    ''' <param name="tabla">Nombre de la tabla a la que se hace referencia</param>
    ''' <param name="col">Nombre de columna asociado al índice</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	12/11/2008	Created
    ''' </history>
    Public Shared Function GetPositionOfColumn(ByRef tabla As String, ByRef col As String) As Int16

        Dim ds As DataSet

        If (Server.isSQLServer) Then
            ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT TOP 0 * FROM " & tabla)
        ElseIf (Server.isOracle) Then
            ds = Server.Con.ExecuteDataset(CommandType.Text, "Select * FROM " & tabla & " Where rownum = 0")
        End If

        For posColumn As Integer = 0 To ds.Tables(0).Columns.Count - 1

            ' Si el nombre de la columna es igual a col
            If (ds.Tables(0).Columns(posColumn).ColumnName.ToUpper() = col.ToUpper()) Then
                ds.Dispose()
                Return (posColumn)
            End If

        Next

        ds.Dispose()
        Return (Nothing)

    End Function

    Public Shared Function getIndexKey(ByVal id As Int32) As Zamba.Core.Index
        Dim index As New Zamba.Core.Index
        Try
            Dim sql As String = "Select IndexId from ZBarCodeComplete Where doctypeid=" & id & " and Clave=1"
            index.ID = Server.Con.ExecuteScalar(CommandType.Text, sql)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return index
    End Function

    ''' <summary>
    ''' Método que sirve para obtener los indices clave
    ''' </summary>
    ''' <param name="id">Id del entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    05/11/2008  Created        
    ''' </history>
    Public Shared Function getIndexKeys(ByVal id As Int64) As ArrayList

        Try

            Dim sql As String = "Select IndexId from ZBarCodeComplete Where doctypeid =" & id & " and Clave = 1"
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)

            If (ds.Tables(0).Rows.Count > 0) Then

                Dim indexKeys As New ArrayList

                For Each row As DataRow In ds.Tables(0).Rows
                    Dim index As New Zamba.Core.Index
                    index.ID = row.Item("INDEXID")
                    indexKeys.Add(index)
                Next

                Return (indexKeys)

            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

        Return (Nothing)

    End Function

    Public Shared Function GetAutoIndexs(ByVal dt As Int32, ByVal IndexId As Int32) As DataSet
        Dim ds As DataSet
        Dim sql As String = "Select * from ZBarcodeComplete where DocTypeID = " & dt & " and Indexid = " & IndexId & " and Clave = 1"
        ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
        If ds.Tables(0).Rows.Count = 0 Then
            Return Nothing
        End If
        Return ds
    End Function

    Public Shared Function LoadRemarks(ByVal UserId As Integer) As ArrayList
        Dim ds As New DataSet
        Dim oLoadRemarks As New ArrayList
        Try
            Dim strSelect As String = "SELECT USERID,REMARK,ORDER FROM zbarcode_remark WHERE USERID =" & UserId & " order by 3"
            ds = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim bcremark As New BarcodeRemark
                bcremark.UserId = ds.Tables(0).Rows(i).Item(0)
                bcremark.Remark = ds.Tables(0).Rows(i).Item(1)
                bcremark.Order = ds.Tables(0).Rows(i).Item(2)
                oLoadRemarks.Add(bcremark)
            Next
        Catch ex As Exception
            ' zamba.core.zclass.raiseerror(ex)
        End Try
        Return oLoadRemarks
    End Function

    Public Shared Sub SaveRemark(ByVal UserId As Integer, ByVal remark As String)
        If remark = "" Then Exit Sub
        Try
            'subo el order de todos en 1
            Dim strUpdate As String = "UPDATE zbarcode_remark SET zbarcode_remark.order = zbarcode_remark.order + 1 where userid=" & UserId
            Server.Con.ExecuteNonQuery(CommandType.Text, strUpdate)

            'borro el order que sea igual a 6
            Dim strDelete As String = "DELETE FROM zbarcode_remark WHERE zbarcode_remark.order => 6 and userid=" & UserId
            Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)

            'inserto el nuevo comentario en la posicion 1
            Dim strInsert As String = "INSERT INTO zbarcode_remark VALUES(" & UserId & ",'" & remark & "',1)"
            Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ' -----------------------------------------------------------------------------
    ' <summary>
    ' Construye y devuelve una consulta SQL.
    ' </summary>
    ' <param name="doctypeid"></param>
    ' <param name="indexvalue"></param>
    ' <returns></returns>
    ' <remarks>
    ' </remarks>
    ' <history>
    ' 	[Hernan]	26/05/2006	Created
    ' 	[Msrcelo]	29/09/2007	MOdified: se comento el si es numerico ejecutar sin comillas, si es numerico y tiene comillas lo ejecuta igual
    ' </history>
    ' -----------------------------------------------------------------------------
    Public Shared Function GetSql(ByVal doctypeid As Int32, ByRef indexvalue As Object) As String
        Dim tabla As String
        Dim ds As DataSet
        Dim i As Int32
        Dim str As String
        Dim sql As New System.Text.StringBuilder
        Dim CurrentData As String = String.Empty

        Try
            sql.Append("Select indexid,tabla,columna from zbarcodecomplete where doctypeid=")
            sql.Append(doctypeid)
            sql.Append(" order by orden")
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            sql.Append("Select ")

            For i = 0 To ds.Tables(0).Rows.Count - 1
                If i > 0 Then sql.Append(", ")
                sql.Append(ds.Tables(0).Rows(i).Item(1))
                sql.Append(".")
                sql.Append(ds.Tables(0).Rows(i).Item(2))
            Next

            sql.Append(" from ")
            tabla = ds.Tables(0).Rows(0).Item(1)
            sql.Append(tabla)
            'Corte de control
            For i = 0 To ds.Tables(0).Rows.Count - 1
                If tabla <> ds.Tables(0).Rows(i).Item(1) AndAlso Not IsDBNull(ds.Tables(0).Rows(i).Item(1)) Then
                    sql.Append(", " & ds.Tables(0).Rows(i).Item(1))
                    tabla = ds.Tables(0).Rows(i).Item(1)
                End If
            Next
            str = "Select indexid,columna,conditions from zbarcodecomplete where doctypeid=" & doctypeid & " and clave=1 order by orden"
            ds = Server.Con.ExecuteDataset(CommandType.Text, str)
            If ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No hay campo clave para buscar")
            End If
            sql.Append(" Where " & ds.Tables(0).Rows(0).Item(1) & " " & ds.Tables(0).Rows(0).Item(2) & " ")
            'If IsNumeric(indexvalue) Then
            '    sql.Append(indexvalue)
            'Else
            If IsDate(indexvalue) Then
                sql.Append(Server.Con.ConvertDate(indexvalue))
            Else
                sql.Append("'" & indexvalue & "'")
            End If
            'End If
            Return sql.ToString

        Finally
            sql.Remove(0, sql.Length)
            sql = Nothing
        End Try
    End Function

    Public Shared Function getsentencia(ByVal doctypeid As Int32, ByRef datatemp As String) As DataSet
        Dim sql As String
        Dim ds As DataSet
        sql = Data.BarcodeFactory.GetSql(doctypeid, datatemp)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "autocompletar: " & sql)
        ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Return ds
    End Function

    ''' <summary>
    ''' Método que sirve para ejecutar la sentencia de los atributos clave (se compara si el índice clave del tipo de doc. y de la tabla son iguales)
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <param name="DataTemp"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    05/11/2008  Created    Código original de GetSentencia, pero con segundo parámetro para aceptar una colección de atributos        
    ''' </history>
    Public Shared Function GetSentencia(ByVal DocTypeId As Int32, ByRef DataTemp As ArrayList) As DataSet

        Dim sql As String
        Dim ds As DataSet
        sql = Data.BarcodeFactory.GetSql(DocTypeId, DataTemp)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "AUTOCOMPLETAR: " & sql)
        ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Return (ds)

    End Function

    ''' <summary>
    ''' Método que devuelve una sentencia SQL (donde los indices clave sean iguales)
    ''' </summary>
    ''' <param name="doctypeid"></param>
    ''' <param name="indexValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    05/11/2008  Created    Código original de GetSql, pero con segundo parámetro para aceptar una colección de atributos        
    '''                                         y modificación del método para adaptarse a los atributos
    '''     [Ezequiel]    29/04/2009  Modified: Se arreglaron errores ya que generaba mal la consulta sql 
    '''     [Ezequiel]    05/05/2009  Modified: Se agrego validacion para saber si es un indice de autosustitucion o no.
    ''' </history>
    Public Shared Function GetSql(ByVal doctypeid As Int32, ByRef indexValues As ArrayList) As String

        Dim tabla As String
        Dim ds As DataSet
        Dim i As Int32
        Dim str As String
        Dim sql As New System.Text.StringBuilder

        Try
            sql.Append("Select indexid,tabla,columna from zbarcodecomplete where doctypeid=")
            sql.Append(doctypeid)
            sql.Append(" order by orden")
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)
            sql.Append("Select ")

            For i = 0 To ds.Tables(0).Rows.Count - 1
                If i > 0 Then sql.Append(", ")
                sql.Append(ds.Tables(0).Rows(i).Item(1))
                sql.Append(".")
                sql.Append(ds.Tables(0).Rows(i).Item(2))
            Next

            sql.Append(" from ")
            tabla = ds.Tables(0).Rows(0).Item(1)
            sql.Append(tabla)

            'Corte de control
            For i = 0 To ds.Tables(0).Rows.Count - 1
                If tabla <> ds.Tables(0).Rows(i).Item(1) AndAlso Not IsDBNull(ds.Tables(0).Rows(i).Item(1)) Then
                    sql.Append(", " & ds.Tables(0).Rows(i).Item(1))
                    tabla = ds.Tables(0).Rows(i).Item(1)
                End If
            Next

            str = "Select indexid,columna,conditions,WhereCondition from zbarcodecomplete where doctypeid=" & doctypeid & " and clave=1 order by orden"
            ds = Server.Con.ExecuteDataset(CommandType.Text, str)

            If ds.Tables(0).Rows.Count = 0 Then
                Throw New Exception("No hay campo clave para buscar")
            End If

            Dim ban As Boolean = False

            For Each row As DataRow In ds.Tables(0).Rows

                If (ban = False) Then
                    sql.Append(" Where " & row.Item(1) & " " & row.Item(2) & " ")
                    ban = True
                Else
                    sql.Append(" AND " & row.Item(1) & " " & row.Item(2) & " ")
                End If

                If IsDate(GetIndex(row.Item(0), indexValues).DataTemp) Then
                    sql.Append(Server.Con.ConvertDate(GetIndex(row.Item(0), indexValues).DataTemp))
                Else
                    sql.Append("'" & GetIndex(row.Item(0), indexValues).DataTemp & "'")
                End If
            Next

            '[sebastian] se le agrega la condicion de fitrado seteada en el administrador en el caso de que la tenga
            '[10/12/2008]

            Dim WhereCondition As String = ds.Tables(0).Rows(0).Item(3).ToString.ToLower


            If Not String.IsNullOrEmpty(WhereCondition) Then
                If String.IsNullOrEmpty(sql.ToString) = False And sql.ToString.ToLower.Contains("where") = True Then
                    sql.Append(" and " & WhereCondition)
                Else
                    sql.Append(" where " & WhereCondition)
                End If
            End If


            Return (sql.ToString())


        Finally
            sql.Remove(0, sql.Length)
            sql = Nothing
        End Try

    End Function
    ''' <summary>
    ''' Metodo que devuelve el indice especifico que se encuentra en el arraylist
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="indexlst"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 29/04/09 Created </history>
    Private Shared Function GetIndex(ByVal id As Int64, ByVal indexlst As ArrayList) As Index
        For Each intmp As Index In indexlst
            If id = intmp.ID Then Return intmp
        Next
    End Function
    ''' <summary>
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="indexlst"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 29/04/09 Modified: Se adapto para que devuelva la columna a completar </history>
    Public Shared Function GetDsIndexs(ByVal DocTypeId As Int64, Optional ByVal withcol As Boolean = False) As DataSet
        Dim strsql As String
        If Not withcol Then
            strsql = "Select Indexid, orden, Clave from ZBarCodeComplete where Doctypeid=" & DocTypeId & " Order by Orden ASC"  ' and Clave=0
        Else
            strsql = "Select Indexid, orden, Clave, Columna from ZBarCodeComplete where Doctypeid=" & DocTypeId & " Order by Orden ASC"  ' and Clave=0
        End If
        'Dim strsql As String = "Select Indexid from ZBarCodeComplete where Doctypeid=" & Me.DocTypeId & " and Clave=0"
        Dim dsindexs As DataSet = Nothing
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "StrSQL: " & strsql)
            dsindexs = Server.Con.ExecuteDataset(CommandType.Text, strsql)
        Catch ex As Exception
            '           zamba.core.zclass.raiseerror(ex)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "ERROR: No se pudo realizar la consulta SQL: " & strsql)
        End Try
        Return dsindexs
    End Function


    Public Shared Function HasSpecificIndexFilters(ByVal DocTypeId As Int32) As Boolean

        Dim strsql As String = "Select count(1) from ZbarcodeComplete Where docTypeId = " & DocTypeId & " and FilterByIndex = 1 "

        If Server.Con.ExecuteScalar(CommandType.Text, strsql) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Shared Function getIndexs(ByVal DocTypeId As Int32) As DataSet

        Dim strsql As String = "Select * from ZbarcodeComplete Where docTypeId = " & DocTypeId
        Dim ds As DataSet

        Try
            ds = Server.Con.ExecuteDataset(CommandType.Text, strsql)
        Catch ex As Exception
            Return (Nothing)
        End Try

        Return (ds)

        'Select * from  ZbarcodeComplete where doctypeId = ....

        '2190	996	Poliza	[IdAuto]	1	0
        '2190	124	Poliza	[Color]		0	1

        'Obtengo esto  

        'Recorro filas y armo el select de columnas

        'Select tabla.columna from tabla


        'Ahora 

        'Select * from tabla

    End Function


    Public Shared Function prueba2(ByVal DocTypeId As Int32) As DataSet

        Dim strsql As String = "Select indexId, Columna from ZbarcodeComplete Where docTypeId = " & DocTypeId
        Dim ds As DataSet

        Try
            ds = Server.Con.ExecuteDataset(CommandType.Text, strsql)
        Catch ex As Exception
            Return (Nothing)
        End Try

        Return (ds)

    End Function

    'Public Shared Function GetReporte() As DataSet

    '	Return Nothing
    'End Function
    Public Overloads Shared Function getZBARCODECOMPLETE() As DataSet
        'TODO store: SPGetZBarCodeComp
        'Dim strSelect As String = "select * from zbarcodecomplete where doctypeid=" & cbodoctype.SelectedValue.ToString
        Dim strSelect As String = "SELECT * FROM ZBARCODECOMPLETE"
        Return Servers.Server.Con.ExecuteDataset(CommandType.Text, strSelect)
    End Function

    Public Shared Function getZBARCODECOMPLETEWithDistinctDocType() As DataSet
        Dim strSelect As String = "SELECT distinct * FROM ZBARCODECOMPLETE WHERE clave = 1"
        Return Servers.Server.Con.ExecuteDataset(CommandType.Text, strSelect)
    End Function


    Public Overloads Shared Sub deleteZbarcodecomplete(ByVal sp_DocTypeid As Int64, ByVal indexId As Int64)
        Dim strdelete As String = "delete from zbarcodecomplete where doctypeid=" & sp_DocTypeid & " and IndexId=" & indexId
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    End Sub

    Public Overloads Shared Sub deleteZbarcodecomplete(ByVal sp_DocTypeid As Int64)
        Dim strdelete As String = "delete from zbarcodecomplete where doctypeid=" & sp_DocTypeid
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    End Sub

    Public Overloads Shared Sub deleteZbarcodecomplete()
        Dim strdelete As String = "DELETE FROM ZBARCODECOMPLETE"
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    End Sub

    Public Shared Sub insertZbarcodecomplete(ByVal psClave As Boolean, ByVal sp_DocTypeid As String, ByVal psIndexid As String, ByVal psTabla As String, ByVal psColumna As String, ByVal newOrden As String)
        Dim values As String
        If psClave Then
            values = "(" & sp_DocTypeid & "," & psIndexid & ",'" & psTabla & "','" & psColumna & "',1," & newOrden & ")"
        Else
            values = "(" & sp_DocTypeid & "," & psIndexid & ",'" & psTabla & "','" & psColumna & "',0," & newOrden & ")"
        End If
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, "insert into zbarcodecomplete (doctypeid,indexid,tabla,columna,clave,orden) values " & values)
    End Sub


    ''' <summary>
    ''' Devuelve la sentencia SELECT-FROM de las consultas de Caratulas 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function QuerySelectCaratulas() As String
        Dim cadena As New System.Text.StringBuilder(370, 600)
        cadena.Append(" SELECT usrtable.name as Usuario, zbarcode.id as Caratula,")
        cadena.Append(" doc_type.doc_type_name as Documento, zbarcode.fecha as Fecha, zbarcode.scanned as Escaneado,")
        cadena.Append(" zbarcode.box as Caja, zbarcode.batch as Lote, zbarcode.doc_type_id, zbarcode.doc_id")
        cadena.Append(" FROM zbarcode INNER JOIN usrtable ON zbarcode.userid = usrtable.id  INNER JOIN doc_type ON doc_type.doc_type_id = zbarcode.doc_type_id")

        Return cadena.ToString()
    End Function

    Public Shared Function dsFilterCaratulas(ByVal UserId As Integer) As DataTable
        Dim cadena As New System.Text.StringBuilder()
        cadena.Append(QuerySelectCaratulas())
        cadena.Append(" WHERE zbarcode.userid =")
        cadena.Append(UserId.ToString())
        cadena.Append(" ORDER BY zbarcode.fecha")

        Return Server.Con.ExecuteDataset(CommandType.Text, cadena.ToString()).Tables(0)
    End Function

    Public Shared Function dsFilterCaratulas(ByVal UserId As Integer, ByVal fecha As DateTime) As DataTable
        Return dsFilterCaratulas(UserId, fecha, fecha)
    End Function

    Public Shared Function dsFilterCaratulas(ByVal UserId As Integer, ByVal fechaInicial As DateTime, ByVal fechaFinal As DateTime) As DataTable
        Dim cadena As New System.Text.StringBuilder
        cadena.Append(QuerySelectCaratulas())
        cadena.Append(" WHERE zbarcode.userid = ")
        cadena.Append(UserId)
        cadena.Append(" AND fecha BETWEEN ")
        cadena.Append(Server.Con.ConvertDate(fechaInicial))
        cadena.Append(" AND ")
        cadena.Append(Server.Con.ConvertDate(fechaFinal.AddDays(1.0)))
        cadena.Append(" ORDER BY zbarcode.fecha")

        Return Server.Con.ExecuteDataset(CommandType.Text, cadena.ToString()).Tables(0)
    End Function

    Public Shared Function dsFilterCaratulas(ByVal fechaInicial As DateTime, ByVal fechaFinal As DateTime) As DataTable
        Dim cadena As New System.Text.StringBuilder
        cadena.Append(QuerySelectCaratulas())
        cadena.Append(" WHERE fecha BETWEEN ")
        cadena.Append(Server.Con.ConvertDate(fechaInicial))
        cadena.Append(" AND ")
        cadena.Append(Server.Con.ConvertDate(fechaFinal.AddDays(1.0)))
        cadena.Append(" ORDER BY zbarcode.fecha")
        Return Server.Con.ExecuteDataset(CommandType.Text, cadena.ToString()).Tables(0)
    End Function

    Public Shared Function dsAllCaratulas() As DataTable
        Return Server.Con.ExecuteDataset(CommandType.Text, QuerySelectCaratulas() & " order by zbarcode.id").Tables(0)
    End Function

    Private Shared Function getStartDate(ByVal p_date As DateTime) As String
        Dim local_date As DateTime = New DateTime(p_date.Year, p_date.Month, p_date.Day, 0, 0, 0)
        Return local_date.ToString()
    End Function

    Private Shared Function getEndDate(ByVal p_date As DateTime) As String
        Dim local_date As DateTime = New DateTime(p_date.Year, p_date.Month, p_date.Day, 23, 59, 59)
        Return local_date.ToString()
    End Function

End Class