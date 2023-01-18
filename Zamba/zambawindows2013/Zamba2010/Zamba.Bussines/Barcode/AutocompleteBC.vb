Imports zamba.core
'Imports Zamba.Barcode.Business
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Barcode
''' Class	 : Barcode.AutocompleteBC
''' 
''' -----------------------------------------------------------------------------
''' <summary>
'''     
''' </summary>
''' <remarks>
'''     Esta clase se reubico dentro Zamba.Business.
''' </remarks>
''' <history>
''' 	[oscar]	07/06/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
<Serializable()> Public Class AutocompleteBC
    Inherits AutocompleteBCBusiness
    '    Inherits zclass
    '    Implements IDisposable

    '#Region "Eventos"
    '    Public Event Creado()
    '#End Region
    '    'Private Function existe(ByVal docTypeId as Int64, ByVal indexid As Int32) As Boolean
    '    '    Try
    '    '        If Server.ServerType = DBTYPES.MSSQLServer OrElse Server.ServerType = DBTYPES.MSSQLServer7Up Then
    '    '            Dim parValues() = {doctypeid, indexid}
    '    '            If Server.Con.ExecuteScalar("ExisteAutocompleteIndex", parValues) = 0 Then
    '    '                Return False
    '    '            Else
    '    '                Return True
    '    '            End If
    '    '        Else
    '    '            'TEST ORACLE
    '    '            Dim sql As String = "Select count(1) from zbarcodecomplete where doctypeId=" & doctypeid & " and indexid=" & indexid
    '    '            If Server.Con.ExecuteScalar(CommandType.Text, sql) = 0 Then
    '    '                Return False
    '    '            Else
    '    '                Return True
    '    '            End If
    '    '        End If
    '    '    Catch ex As Exception
    '    '    End Try
    '    'End Function
    '    Public Sub Dispose() Implements System.IDisposable.Dispose

    '    End Sub
    '    'Public Shared Function GetAutoCompleteIndexs(ByVal docTypeId as Int64) As DataTable
    '    '    Dim ds As New DataSet
    '    '    Try
    '    '        If Server.ServerType = DBTYPES.MSSQLServer OrElse Server.ServerType = DBTYPES.MSSQLServer7Up Then
    '    '            Dim parValues() = {doctypeid}
    '    '            ds = Server.Con.ExecuteDataset("GetAutocompleteIndexs", parValues)
    '    '            Return ds.Tables(0)
    '    '        Else
    '    '            'TEST ORACLE
    '    '            Dim sql As String = "Select * from ZBarcodeComplete Where doctypeid=" & doctypeid
    '    '            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
    '    '            Return ds.Tables(0)
    '    '        End If
    '    '    Catch ex As Exception
    '    '    Finally
    '    '        ds.Dispose()
    '    '    End Try
    '    'End Function
    '    ''' -----------------------------------------------------------------------------
    '    ''' <summary>
    '    ''' Guarda la configuración en una base de datos
    '    ''' </summary>
    '    ''' <param name="doctypeid"></param>
    '    ''' <param name="indexid"></param>
    '    ''' <param name="tabla"></param>
    '    ''' <param name="col"></param>
    '    ''' <param name="esclave"></param>
    '    ''' <remarks>
    '    ''' </remarks>
    '    ''' <history>
    '    ''' 	[Hernan]	26/05/2006	Created
    '    ''' </history>
    '    ''' -----------------------------------------------------------------------------
    '    'Public Shared Sub Insertar(ByVal docTypeId as Int64, ByVal indexid As Int32, ByVal tabla As String, ByVal col As String, ByVal esclave As Boolean)
    '    '    Try
    '    '        Dim orden As Int16 = GetLastOrden(doctypeid) + 1
    '    '        'Dim orden As Int16 = Me.GetLastOrden(doctypeid)
    '    '        Dim sql As String
    '    '        If Server.ServerType = DBTYPES.MSSQLServer OrElse Server.ServerType = DBTYPES.MSSQLServer7Up Then
    '    '            col = "[" & col & "]"
    '    '        Else
    '    '            col = ControlChars.Quote & col & ControlChars.Quote
    '    '        End If
    '    '        If esclave Then
    '    '            sql = "Insert into ZBarCodeComplete(Doctypeid,Indexid,Tabla,Columna,Clave,Orden) Values(" & doctypeid & "," & indexid & ",'" & tabla & "','" & col & "'," & 1 & "," & orden & ")"
    '    '        Else
    '    '            sql = "Insert into ZBarCodeComplete(Doctypeid,Indexid,Tabla,Columna,Clave,Orden) Values(" & doctypeid & "," & indexid & ",'" & tabla & "','" & col & "'," & 0 & "," & orden & ")"
    '    '        End If
    '    '        esclave = False
    '    '        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    '    '    Catch ex As Exception
    '    '        esclave = False
    '    '       zamba.core.zclass.raiseerror(ex)
    '    '    End Try
    '    'End Sub
    '    'Private Shared Function GetLastOrden(ByVal dt As Int32) As Int16
    '    '    If IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, "Select max(orden) from ZBarCodeComplete where Doctypeid=" & dt)) Then
    '    '        Return -1
    '    '    Else
    '    '        Return Server.Con.ExecuteScalar(CommandType.Text, "Select max(orden) from ZBarCodeComplete where Doctypeid=" & dt)
    '    '    End If
    '    'End Function

    '    Public Function Complete(Byval Result1 As Result, ByVal index As Index) As Result
    '        Dim ds As DataSet
    '        Dim col As Int16

    '        ds = BarcodesFactory.GetSentencia(DocTypeId, index.DataTemp)
    '        'Dim sql As String
    '        'Try
    '        '    sql = BarcodesFactory.GetSql(DocTypeId, index.DataTemp)
    '        '    ZTrace.WriteLineIf(ZTrace.IsInfo,"AUTOCOMPLETAR: " & sql)
    '        '    ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
    '        'Catch ex As Exception
    '        '   zamba.core.zclass.raiseerror(ex)
    '        '    ZTrace.WriteLineIf(ZTrace.IsInfo,"ERROR: No se pudo realizar la consulta SQL: " & sql)
    '        'End Try

    '        'Disparo excepcion para que n0 refresque atributos
    '        If ds.Tables(0).Rows.Count = 0 Then
    '            Throw New Exception
    '        End If

    '        Dim i, j As Int16
    '        Dim dsindexs As DataSet
    '        dsindexs = BarcodesFactory.GetDsIndexs(DocTypeId)

    '        ds.WriteXml(Application.StartupPath & "\ds.xml")
    '        If ds.Tables(0).Rows.Count > 1 Then Me.GetData(ds)
    '        If ds.Tables(0).Rows.Count <> 0 Then  'ultimo agregado
    '            For i = 0 To dsindexs.Tables(0).Rows.Count - 1
    '                For j = 0 To Result1.Indexs.Count - 1
    '                    If dsindexs.Tables(0).Rows(i)("Clave") <> 1 Then
    '                        If Result1.Indexs(j).Id = dsindexs.Tables(0).Rows(i).Item(0) Then
    '                            '  Result1.Indexs(j).Data = ds.Tables(0).Rows(fila).Item(i)
    '                            ' Result1.Indexs(j).Datatemp = ds.Tables(0).Rows(fila).Item(i)
    '                            Try
    '                                col = dsindexs.Tables(0).Rows(i).Item(1)
    '                                ZTrace.WriteLineIf(ZTrace.IsInfo,"Result1.indexs(j).Data=" & ds.Tables(0).Rows(fila)(col))
    '                                Result1.Indexs(j).Data = ds.Tables(0).Rows(fila)(col)
    '                            Catch ex As Exception
    '                               zamba.core.zclass.raiseerror(ex)
    '                                ZTrace.WriteLineIf(ZTrace.IsInfo,"ERROR: Al cargar el atributo: " & Result1.Indexs(j).name & ", de la columna nro: " & col)
    '                                ZTrace.WriteLineIf(ZTrace.IsInfo,"")
    '                                ZTrace.WriteLineIf(ZTrace.IsInfo,ex.ToString)
    '                            End Try
    '                            Try
    '                                Result1.Indexs(j).Datatemp = ds.Tables(0).Rows(fila)(col)
    '                            Catch ex As Exception
    '                               zamba.core.zclass.raiseerror(ex)
    '                                ZTrace.WriteLineIf(ZTrace.IsInfo,"ERROR: Al cargar el atributo (datatemp): " & Result1.Indexs(j).name & " ,de la columna nro: " & col)
    '                            End Try
    '                        End If
    '                        If Result1.Indexs(j).ID = index.Id Then Result1.Indexs(j).Data = index.DataTemp
    '                    End If
    '                Next
    '            Next
    '        End If

    '        Return Result1
    '    End Function

    'Protected Overrides Function GetData(ByVal ds As DataSet) As Int16
    '    Dim grilla As New frmGrilla(ds)
    '    RemoveHandler grilla.IdFila, AddressOf MyBase.getrow
    '    AddHandler grilla.IdFila, AddressOf MyBase.getrow
    '    grilla.ShowDialog()
    'End Function

    '    Private fila As Int16
    '    Private Sub getrow(ByVal id As Int16)
    '        Try
    '            Me.fila = id
    '        Catch ex As Exception
    '           zamba.core.zclass.raiseerror(ex)
    '        End Try
    '    End Sub
    '    '''' -----------------------------------------------------------------------------
    '    '''' <summary>
    '    '''' Construye y devuelve una consulta SQL.
    '    '''' </summary>
    '    '''' <param name="doctypeid"></param>
    '    '''' <param name="indexvalue"></param>
    '    '''' <returns></returns>
    '    '''' <remarks>
    '    '''' </remarks>
    '    '''' <history>
    '    '''' 	[Hernan]	26/05/2006	Created
    '    '''' </history>
    '    '''' -----------------------------------------------------------------------------
    '    'Private Shared Function GetSql(ByVal docTypeId as Int64, ByVal indexvalue As Object) As String
    '    '    Dim tabla As String
    '    '    Dim ds As DataSet
    '    '    Dim i As Int32
    '    '    Dim str As String
    '    '    Dim sql As New System.Text.StringBuilder

    '    '    Try
    '    '        sql.Append("Select indexid,tabla,columna from zbarcodecomplete where doctypeid=")
    '    '        sql.Append(doctypeid)
    '    '        sql.Append(" order by orden")
    '    '        ds = Server.Con.ExecuteDataset(CommandType.Text, sql.ToString)
    '    '        sql.Remove(0, sql.Length)
    '    '        sql.Append("Select ")

    '    '        For i = 0 To ds.Tables(0).Rows.Count - 1
    '    '            If i > 0 Then sql.Append(", ")
    '    '            sql.Append(ds.Tables(0).Rows(i).Item(1))
    '    '            sql.Append(".")
    '    '            sql.Append(ds.Tables(0).Rows(i).Item(2))
    '    '        Next

    '    '        sql.Append(" from ")
    '    '        tabla = ds.Tables(0).Rows(0).Item(1)
    '    '        sql.Append(tabla)
    '    '        'Corte de control
    '    '        For i = 0 To ds.Tables(0).Rows.Count - 1
    '    '            If tabla <> ds.Tables(0).Rows(i).Item(1) AndAlso Not IsDBNull(ds.Tables(0).Rows(i).Item(1)) Then
    '    '                sql.Append(", " & ds.Tables(0).Rows(i).Item(1))
    '    '                tabla = ds.Tables(0).Rows(i).Item(1)
    '    '            End If
    '    '        Next
    '    '        str = "Select indexid,columna from zbarcodecomplete where doctypeid=" & doctypeid & " and clave=1 order by orden"
    '    '        ds = Server.Con.ExecuteDataset(CommandType.Text, str)
    '    '        If ds.Tables(0).Rows.Count = 0 Then
    '    '            Throw New Exception("No hay campo clave para buscar")
    '    '        End If
    '    '        sql.Append(" Where " & ds.Tables(0).Rows(0).Item(1) & "=")
    '    '        If IsNumeric(indexvalue) Then
    '    '            sql.Append(indexvalue)
    '    '        Else
    '    '            If IsDate(indexvalue) Then
    '    '                sql.Append(Server.Con.ConvertDate(indexvalue))
    '    '            Else
    '    '                sql.Append("'" & indexvalue & "'")
    '    '            End If
    '    '        End If
    '    '        Return sql.ToString
    '    '    Catch ex As Exception
    '    '        Throw ex
    '    '    Finally
    '    '        sql.Remove(0, sql.Length)
    '    '        sql = Nothing
    '    '    End Try
    '    'End Function
    '    'Private Shared Function getIndexKey(ByVal id As Int32) As Zamba.Core.Index
    '    '    Try
    '    '        Dim sql As String = "Select IndexId from ZBarCodeComplete Where doctypeid=" & id & " and Clave=1"
    '    '        Dim index As New Zamba.Core.Index
    '    '        index.Id = Server.Con.ExecuteScalar(CommandType.Text, sql)
    '    '        Return index
    '    '    Catch ex As Exception
    '    '       zamba.core.zclass.raiseerror(ex)
    '    '    End Try
    '    'End Function
    '    Private docTypeId as Int64
    Public Sub New()
        'Se instancia para el insertar, en el módulo de administrador
    End Sub
    Public Sub New(ByVal Dtid As Int32)
        'Me.DocTypeId = Dtid
        'Me._Index = New Zamba.Core.Index
        'Me._Index = BarcodesFactory.getIndexKey(Dtid)
        MyBase.New(Dtid)
    End Sub
    '    Dim _Index As Zamba.Core.Index
    '    Public ReadOnly Property Index() As Zamba.Core.Index
    '        Get
    '            Return Me._Index
    '        End Get
    '    End Property

    Public Overrides Sub Dispose()

    End Sub
End Class

Public Class AutoCompleteBarcode_Factory
    Private Shared AC As AutocompleteBC
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un Objeto AutoCompleteBC si hay configurado autocompletar en base al DocTypeID y al Atributo seleccionado
    ''' </summary>
    ''' <param name="DocTypeID"></param>
    ''' <param name="IndexId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Se debe evaluar si el resultado de esto es NOTHING
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetComplete(ByVal docTypeId As Int64, ByVal IndexId As Int32) As AutocompleteBC
        If IsNothing(BarcodesBusiness.GetAutoIndexs(DocTypeID, IndexId)) Then
            Return Nothing
        Else
            AC = New AutocompleteBC(DocTypeID)
            Return AC
        End If
    End Function
    'Private Shared Function GetAutoIndexs(ByVal dt As Int32, ByVal IndexId As Int32) As DataSet
    '    Dim ds As DataSet
    '    Dim sql As String = "Select * from ZBarcodeComplete where DocTypeID=" & dt & " and Indexid=" & IndexId & " and clave=1"
    '    ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
    '    If ds.Tables(0).Rows.Count = 0 Then
    '        Return Nothing
    '    End If
    '    Return ds
    'End Function
End Class
