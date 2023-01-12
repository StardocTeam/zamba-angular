Imports Zamba.Servers
Imports Zamba.Core
Imports System.Text

Public Class AutoSubstitutionDataFactory

	' hashtable donde se guardan las tablas
	' se sustitución consultadas para optimizar
	' la busqueda

    Public Const nombreColumnaCodigo As String = "Codigo"
	Public Const nombreColumnaDescripcion As String = "Descripcion"
	Public Const nombreTablaDeSustitucion As String = "SLST_S"

    ' Función: Devuelve la tabla de sistitucion especificada
    '
    '	Parametros:
    '		IndexId:	id de tabla de sustitución
    '
    '	return: tabla de sustitución



    Public Shared Function GetTableWithLimit(ByVal indexid As Integer, ByVal orderByDesc As Boolean, LimiTo As Int64, Value As String) As DataTable

        Dim columnaCodigo As DataColumn = New DataColumn(nombreColumnaCodigo, System.Type.GetType("System.String"))
        Dim columnaDescripcion As DataColumn = New DataColumn(nombreColumnaDescripcion, System.Type.GetType("System.String"))
        Dim dtTabla As DataTable = New DataTable(nombreTablaDeSustitucion & indexid)
        dtTabla.Columns.Add(columnaCodigo)
        dtTabla.Columns.Add(columnaDescripcion)
        Dim consulta As StringBuilder = New StringBuilder()
        ' Se arma la consulta...

        consulta.Append("Select ")
        If Server.isSQLServer Then
            consulta.Append(" TOP " & LimiTo.ToString() & " ")
        End If
        consulta.Append(nombreColumnaCodigo)
        consulta.Append(",")
        consulta.Append("Replace(" & nombreColumnaDescripcion & ", '''', ' ') as Descripcion")
        consulta.Append(" from ")
        consulta.Append(nombreTablaDeSustitucion & indexid)
        If Char.IsNumber(Value) Then
            consulta.Append(" WHERE LOWER(" & nombreColumnaCodigo & ") LIKE '%" + Value.ToLower() + "%'")
        Else
            consulta.Append(" WHERE LOWER(" & nombreColumnaDescripcion & ") LIKE '%" + Value.ToLower() + "%'")
        End If

        If Server.isOracle Then
            consulta.Append(" AND rownum<=" + LimiTo.ToString)
        End If
        If orderByDesc = True Then
            consulta.Append(" ORDER BY ")
            consulta.Append(nombreColumnaDescripcion)
        Else
            consulta.Append(" ORDER BY ")
            consulta.Append(nombreColumnaCodigo)
        End If
        Dim iterador As IDataReader = Nothing
        Dim Con As IConnection = Nothing
        Try
            ' Se ejecuta la consulta..
            Con = Server.Con
            iterador = Con.ExecuteReader(CommandType.Text, consulta.ToString())

            ' Se traduce el resultado de la consulta a una tabla...
            While (iterador.Read())
                Dim fila As DataRow = dtTabla.NewRow

                ' Se toma codigo...
                fila.Item(nombreColumnaCodigo) =
                 iterador.Item(nombreColumnaCodigo)

                ' Se toma descripcion...
                fila.Item(nombreColumnaDescripcion) =
                 iterador.Item(nombreColumnaDescripcion)

                dtTabla.Rows.Add(fila)
            End While

            iterador.Close()

            If dtTabla.Rows.Count > 0 Then
                Return dtTabla
            Else
                Return New DataTable()
            End If

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cae en excepcion: " & ex.ToString)
            Return New DataTable()
        Finally
            If Not IsNothing(iterador) Then
                iterador.Close()
                iterador.Dispose()
                iterador = Nothing
            End If
            If Not IsNothing(Con) Then
                If Con.State = IConnection.ConnectionStates.Executing OrElse Con.State = IConnection.ConnectionStates.Ready Then
                    Con.Close()
                    Con.dispose()
                    Con = Nothing
                End If
            End If
            'GC.Collect()
        End Try
    End Function




    Public Shared Function GetTable(ByVal indexid As Integer, ByVal orderByDesc As Boolean) As DataTable
        Dim columnaCodigo As DataColumn =
        New DataColumn(nombreColumnaCodigo, System.Type.GetType("System.String"))

        Dim columnaDescripcion As DataColumn =
        New DataColumn(nombreColumnaDescripcion, System.Type.GetType("System.String"))
        Dim dtTabla As DataTable = New DataTable(nombreTablaDeSustitucion & indexid)
        dtTabla.Columns.Add(columnaCodigo)
        dtTabla.Columns.Add(columnaDescripcion)
        Dim consulta As StringBuilder = New StringBuilder()
        ' Se arma la consulta...

        consulta.Append("Select ")
        consulta.Append(nombreColumnaCodigo)
        consulta.Append(",")
        consulta.Append("Replace(" & nombreColumnaDescripcion & ", '''', ' ') as Descripcion")
        consulta.Append(" from ")
        consulta.Append(nombreTablaDeSustitucion & indexid)
        If orderByDesc = True Then
            consulta.Append(" ORDER BY ")
            consulta.Append(nombreColumnaDescripcion)
        Else
            consulta.Append(" ORDER BY ")
            consulta.Append(nombreColumnaCodigo)
        End If
        Dim iterador As IDataReader = Nothing
        Dim Con As IConnection = Nothing
        Try
            ' Se ejecuta la consulta..
            Con = Server.Con
            iterador = Con.ExecuteReader(CommandType.Text, consulta.ToString())

            ' Se traduce el resultado de la consulta a una tabla...
            While (iterador.Read())
                Dim fila As DataRow = dtTabla.NewRow

                ' Se toma codigo...
                fila.Item(nombreColumnaCodigo) =
                 iterador.Item(nombreColumnaCodigo)

                ' Se toma descripcion...
                fila.Item(nombreColumnaDescripcion) =
                 iterador.Item(nombreColumnaDescripcion)

                dtTabla.Rows.Add(fila)
            End While

            iterador.Close()

            If dtTabla.Rows.Count > 0 Then
                Return dtTabla
            Else
                Return New DataTable()
            End If

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cae en excepcion: " & ex.ToString)
            Return New DataTable()
        Finally
            If Not IsNothing(iterador) Then
                iterador.Close()
                iterador.Dispose()
                iterador = Nothing
            End If
            If Not IsNothing(Con) Then
                If Con.State = IConnection.ConnectionStates.Executing OrElse Con.State = IConnection.ConnectionStates.Ready Then
                    Con.Close()
                    Con.dispose()
                    Con = Nothing
                End If
            End If
            'GC.Collect()
        End Try
    End Function

    'Public Shared Function GetMax(ByVal IndexId As Int32) As Int32
    '	Dim Max As Int32
    '	Dim sql As String = "Select max(ItemId) from ILST_I" & IndexId
    '	If Not IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, sql)) Then
    '		Max = Server.Con.ExecuteScalar(CommandType.Text, sql)
    '	End If
    '	If Max = 0 Then Max = 1
    '	Max += 1
    '	Return Max
    'End Function

    '''<summary> Método Obsoleto, utilizar InsertIntoIListAsBoolean [Alejandro]</summary>
    Public Shared Sub InsertIntoIList(ByVal IndexId As Int32, ByVal linea As String)
        Dim sql As String
        '        sql = "Insert into ILST_I" & IndexId & "(ITEMID,Item) Values(" & Max & ",'" & linea & "')"
        sql = "Insert into ILST_I" & IndexId & "(Item) Values('" & linea & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub


    'Inserta un ítem en la tabla ILST_I correspondiente y devuelve True, 
    'en caso de que el ítem exista o no se pueda insertar, devuelve False.
    Public Shared Function InsertIntoIListAsBoolean(ByVal IndexId As Int32, ByVal linea As String) As Boolean
        Try

            'Si el Item no existe con ese valor:
            If Not GetItemExist(IndexId, linea) Then

                Dim sqlBuilder As New StringBuilder()

                Dim sqlText As String = String.Empty
                sqlText = "SELECT MAX(ITEMID) FROM ILST_I" & IndexId

                Dim lastId As Int64

                If IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, sqlText)) Then
                    lastId = 0
                Else
                    lastId = Convert.ToInt64(Server.Con.ExecuteScalar(CommandType.Text, sqlText))
                End If

                lastId += 1

                sqlBuilder.Append("INSERT INTO ILST_I")
                sqlBuilder.Append(IndexId.ToString())
                sqlBuilder.Append(" (ITEMID, ITEM) VALUES ('")
                sqlBuilder.Append(lastId.ToString())
                sqlBuilder.Append("', '")
                sqlBuilder.Append(linea)
                sqlBuilder.Append("')")

                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
                Return True

            Else
                Return False
            End If

        Catch ex As Exception

            Return False

        End Try

    End Function


    'Si el ítem existe con ese valor en la tabla ILST_I correspondiente devuelve True,
    'si no existe devuelve False
    Private Shared Function GetItemExist(ByVal _indexId As Int32, ByVal _linea As String) As Boolean

        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT COUNT(1) FROM ILST_I")
        sqlBuilder.Append(_indexId.ToString())
        sqlBuilder.Append(" WHERE ITEM = '")
        sqlBuilder.Append(_linea)
        sqlBuilder.Append("'")

        Dim existentes As Int16

        existentes = Convert.ToInt16(Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString()))

        If existentes > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

  
    ''' <summary>
    ''' Guarda el valor en las listas de sustitucion
    ''' </summary>
    ''' <param name="values"></param>
    ''' <History> Marcelo Created 24/08/2009</History>
    ''' <remarks></remarks>
    Public Shared Function getDescriptionRow(ByVal indexId As Int64, ByVal code As String) As DataRow
        Dim ds As DataSet = Nothing
        Try
            If String.IsNullOrEmpty(code) = False Then
                Dim Tabla As String = "SLST_S" & indexId
                Dim strselect As String
                If Server.isOracle = True Then
                    strselect = "Select Codigo, replace(DESCRIPCION,'''', ' ') as DESCRIPCION from " & Tabla & " Where Codigo = '" & code & "'"
                Else
                    strselect = "Select Codigo, replace(DESCRIPCION, '''', ' ') as DESCRIPCION from " & Tabla & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "") & "  Where Codigo = '" & code & "'"
                End If
                Dim con As IConnection = Server.Con(True, True, True)
                Try
                    ds = con.ExecuteDataset(CommandType.Text, strselect)
                    If Not IsNothing(ds) Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            Return ds.Tables(0).Rows(0)
                        End If
                    End If
                Catch
                    'Intento nuevamente en caso de que la conexion no haya podido ser realizada
                    Try
                        Threading.Thread.Sleep(500)
                        ds = con.ExecuteDataset(CommandType.Text, strselect)
                        If Not IsNothing(ds) Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Return ds.Tables(0).Rows(0)
                            End If
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Finally
                    If Not IsNothing(con) Then
                        con.dispose()
                        con = Nothing
                    End If
                End Try
            End If
            Return Nothing
        Finally
            If Not IsNothing(ds) Then
                ds = Nothing
            End If
        End Try
    End Function

    Public Shared Function getCode(ByVal indexId As Int64, ByVal code As String) As String
        Dim con As IConnection = Server.Con(True, True, True)
        Try
            If String.IsNullOrEmpty(code) = False Then
                Dim Tabla As String = "SLST_S" & indexId
                Dim strselect As String
                If Server.isOracle = True Then
                    strselect = "Select Codigo, replace(DESCRIPCION,'''', ' ') as DESCRIPCION from " & Tabla & " Where Descripcion ='" & code & "'"
                Else
                    strselect = "Select Codigo, replace(DESCRIPCION, '''', ' ') as DESCRIPCION from " & Tabla & " " & If(Zamba.Servers.Server.isSQLServer, " WITH(NOLOCK) ", "") & "  Where Descripcion ='" & code & "'"
                End If

                Dim codeObj = con.ExecuteScalar(CommandType.Text, strselect)
                If Not IsNothing(codeObj) Then
                    Return codeObj.ToString()
                Else
                    Return String.Empty
                End If
            End If
            Return Nothing
        Finally
            If Not IsNothing(con) Then
                con.dispose()
                con = Nothing
            End If
        End Try
    End Function

    Public Shared Sub AddItems(ByVal sustitutionItem As SustitutionItem, ByVal indexId As Integer)
        Dim strBuilder As New StringBuilder

        strBuilder.Append("INSERT INTO SLST_S")
        strBuilder.Append(indexId.ToString())
        strBuilder.Append("(Codigo, Descripcion) VALUES(")
        strBuilder.Append("'" & sustitutionItem.Code.ToString() & "'")
        strBuilder.Append(" , '")
        strBuilder.Append(Replace(sustitutionItem.Description, "'", "''"))
        strBuilder.Append("')")

        Server.Con.ExecuteNonQuery(CommandType.Text, strBuilder.ToString())
    End Sub
    Public Shared Sub UpdateAddedItem(ByVal codigo As String, ByVal descripcion As String, ByVal LastCode As String, ByVal IndexId As String)
        Dim strBuilder As New StringBuilder

        strBuilder.Append("UPDATE SLST_S" & IndexId & " SET ")
        ' strBuilder.Append(indexId.ToString())
        'strBuilder.Append("(Codigo, Descripcion) VALUES(")
        strBuilder.Append("codigo='" & codigo & "'")
        'strBuilder.Append(" , '")
        strBuilder.Append(",descripcion='" & Replace(descripcion, "'", "''") & "'")
        'strBuilder.Append("')")
        strBuilder.Append(" WHERE codigo='" & lastcode & "'")

        Server.Con.ExecuteNonQuery(CommandType.Text, strBuilder.ToString())
    End Sub
    Shared Sub RemoveItem(ByVal code As Integer, ByVal indexId As Integer)
        Dim strBuilder As New StringBuilder()
        strBuilder.Append("DELETE FROM SLST_S")
        strBuilder.Append(indexId.ToString())
        strBuilder.Append(" WHERE Codigo = ")
        strBuilder.Append(code.ToString())

        Server.Con.ExecuteNonQuery(CommandType.Text, strBuilder.ToString())
    End Sub

End Class
