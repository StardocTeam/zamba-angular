Imports Zamba.Servers
Imports System.Windows.Forms
Imports System.Text
Imports Zamba.Core

Public Class ReportBuilderFactory
    Public Shared HashIndexAdded As Hashtable
    ''' <summary>
    ''' Insert a new query on the table
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="name"></param>
    ''' <param name="tables"></param>
    ''' <param name="fields"></param>
    ''' <param name="relations"></param>
    ''' <remarks></remarks>
    ''' 
    Public Sub InsertQueryReportGeneral(ByVal id As Int32, ByVal name As String, ByVal conditions As String, ByVal query As String, ByVal group As String)
        Try
            Dim strinsert = "insert into Reporte_General (id,query,name,completar,groupexpression) values (" & id & ",'" & query & "','" & name & "','" & conditions & "','" & group & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Sub InsertQueryBuilder(ByVal id As Int32, ByVal name As String, ByVal tables As String, ByVal fields As String, ByVal relations As String, ByVal conditions As String, ByVal SortExpression As String, ByVal GroupExpression As String)
        Try
            Dim strinsert = "Insert into ReportBuilder (Id, Name, Tables,Fields,Relations,conditions,SortExpression,GroupExpression) values (" & id & ",'" & name & "','" & tables & "','" & fields & "','" & relations & "','" & conditions & "','" & SortExpression & "','" & GroupExpression & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Return the columns names
    ''' </summary>
    ''' <param name="name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetColumns(ByVal name As String) As DataSet
        Dim ds As DataSet
        Try
            If Server.isSQLServer Then
                Dim sql As String = "select column_name from information_schema.columns where table_name = '" & name & "'"
                ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Else
                Dim sql As String = "select column_name from user_tab_cols where table_name ='" & name & "'"
                ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            End If
            Return ds
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Return the last ID
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function GetLastID() As Int32
        Try
            Dim ds As DataSet
            Dim strinsert As String = "Select max(id) from reportbuilder"
            ds = Server.Con.ExecuteDataset(CommandType.Text, strinsert)
            Return Int32.Parse(ds.Tables(0).Rows(0).Item(0).ToString())
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function
    ''' <summary>
    ''' Update a query of the table
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tables"></param>
    ''' <param name="fields"></param>
    ''' <param name="relations"></param>
    ''' <remarks></remarks>
    Public Sub UpdateQueryBuilder(ByVal id As Int32, ByVal tables As String, ByVal fields As String, ByVal relations As String, ByVal conditions As String, ByVal name As String, ByVal SortExpression As String, ByVal GroupExpression As String)
        Try
            Dim S As New StringBuilder

            S.Append("Update ReportBuilder set ")
            S.Append(" Name = '" & name)
            S.Append("',Tables = '" & tables)
            S.Append("',Fields = '" & fields)
            S.Append("',Relations = '" & relations)
            S.Append("',conditions = '" & conditions & "'")
            S.Append(",SortExpression = '" & SortExpression & "'")
            S.Append(",GroupExpression = '" & GroupExpression & "'")
            S.Append(" Where id = " & id)
            Server.Con.ExecuteNonQuery(CommandType.Text, S.ToString())
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Run the query and return a dataset with the data
    ''' </summary>
    ''' <param name="id">Query ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RunQueryBuilder(ByVal id As Int32, ByVal isZamba As Boolean, ByVal askCondition As Boolean, ByVal IsWeb As Boolean) As DataSet

        'Execute the query
        Dim sql As String = GenerateQueryBuilder(id, isZamba, askCondition)
        If Not IsNothing(sql) AndAlso String.Compare(sql, String.Empty) <> 0 Then
            Try
                Return Server.Con.ExecuteDataset(CommandType.Text, sql)
            Catch ex As Exception
                If ex.Message.Contains("RPC_E_DISCONNECTED") Or ex.Message.Contains("Valor de tiempo de espera caducado") Then
                    Threading.Thread.CurrentThread.Sleep(1000)
                    Return Server.Con.ExecuteDataset(CommandType.Text, sql)
                Else
                    Zamba.Core.ZClass.raiseerror(ex)
                End If
            End Try
        Else
            ZTrace.WriteLineIf(ZTrace.IsError, "ERROR: La consulta del Reporte esta vacia.")
            Return Nothing
        End If


    End Function

    ''' <summary>
    ''' [sebastian] 14-04-2009 Modify se agrego la validacion para saber si el valor que se pasa como condicion
    ''' es numerico o no y también se agregaron trace.
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 13/04/09 - Se agrego codigo para sacar duracion de estados 
    ''' [Sebastian] 05/06/2009 se agrego opcion para que si el usuario cancela la consulta no se ejecute
    ''' </history>
    Public Function RunQueryBuilderReporteGeneral(ByVal id As Int32) As DataSet
        ZTrace.WriteLineIf(ZTrace.IsInfo, "dentro de RunQueryBuilderReporteGeneral")
        Try
            '[Ezequiel] 13/04/09 - Valido si el id es del registro de estados
            If id = 99999 Then
                '[Ezequiel] 13/04/09 - Obtengo todos los registros de cambios de estados

                Dim sqlcomp As String = "select query from reporte_general where id=" & id
                sqlcomp = Server.Con.ExecuteScalar(CommandType.Text, sqlcomp)


                '[Ezequiel] 13/04/09 - Creo la estructura del dataset a mostrar
                Dim dssql As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sqlcomp)
                Dim dtaux As New DataTable
                With dtaux
                    .Columns.Add(New DataColumn("ID"))
                    .Columns.Add(New DataColumn("Nombre del documento"))
                    .Columns.Add(New DataColumn("Etapa"))
                    .Columns.Add(New DataColumn("Estado"))
                    .Columns.Add(New DataColumn("Tiempo estimado"))
                End With

                Dim num As Int16 = -1

                '[Ezequiel] 13/04/09 - Obtengo la fecha actual de la base de datos
                Dim timenow As Object
                If Server.ServerType = DBTYPES.MSSQLServer7Up Then
                    timenow = Server.Con.ExecuteScalar(CommandType.Text, "select getdate()")
                Else
                    timenow = Server.Con.ExecuteScalar(CommandType.Text, "select sysdate")
                End If
                For Each dr As DataRow In dssql.Tables(0).Rows
                    Dim draux As DataRow = dtaux.NewRow
                    '[Ezequiel] 13/04/09 - Esta validacion es debido a que si es el ultimo cambio de estado de la tarea la tengo que comprar
                    '                      con el tiempo del servidor.
                    If num = -1 OrElse num <> Int16.Parse(dr("doc_id")) Then
                        If num <> -1 Then

                            Dim numaux As Int16 = dssql.Tables(0).Rows.IndexOf(dr) - 1
                            draux("ID") = dssql.Tables(0).Rows(numaux).Item("doc_id")
                            draux("Nombre del documento") = dssql.Tables(0).Rows(numaux).Item("doc_name")
                            draux("Etapa") = dssql.Tables(0).Rows(numaux).Item("step_name")
                            draux("Estado") = dssql.Tables(0).Rows(numaux).Item("state")
                            Dim ts As TimeSpan = (DirectCast(timenow, System.DateTime) - DirectCast(DirectCast(dssql.Tables(0).Rows(numaux).Item("fecha"), System.Object), System.DateTime))
                            If ts.Days > 0 Then
                                draux("Tiempo estimado") = ts.Days & If(ts.Days = 1, " Dia", " Dias") & " " & ts.Hours.ToString("#00") & ":" & ts.Minutes.ToString("#00") & ":" & ts.Seconds.ToString("#00")
                            Else
                                draux("Tiempo estimado") = ts.Hours.ToString("#00") & ":" & ts.Minutes.ToString("#00") & ":" & ts.Seconds.ToString("#00")
                            End If
                            dtaux.Rows.Add(draux)
                        End If
                        num = dr("doc_id")
                    Else
                        Dim numaux As Int16 = dssql.Tables(0).Rows.IndexOf(dr) - 1
                        draux("ID") = dssql.Tables(0).Rows(numaux).Item("doc_id")
                        draux("Nombre del documento") = dssql.Tables(0).Rows(numaux).Item("doc_name")
                        draux("Etapa") = dssql.Tables(0).Rows(numaux).Item("step_name")
                        draux("Estado") = dssql.Tables(0).Rows(numaux).Item("state")
                        Dim ts As TimeSpan = (DirectCast(DirectCast(dr("fecha"), System.Object), System.DateTime) - DirectCast(DirectCast(dssql.Tables(0).Rows(numaux).Item("fecha"), System.Object), System.DateTime))
                        If ts.Days > 0 Then
                            draux("Tiempo estimado") = ts.Days & If(ts.Days = 1, " Dia", " Dias") & " " & ts.Hours.ToString("#00") & ":" & ts.Minutes.ToString("#00") & ":" & ts.Seconds.ToString("#00")
                        Else
                            draux("Tiempo estimado") = ts.Hours.ToString("#00") & ":" & ts.Minutes.ToString("#00") & ":" & ts.Seconds.ToString("#00")
                        End If
                        dtaux.Rows.Add(draux)
                    End If
                Next

                '[Ezequiel] 13/04/09 - Guardo el tiempo del ultimo registro.
                If dssql.Tables(0).Rows.Count > 0 Then
                    Dim draux2 As DataRow = dtaux.NewRow
                    draux2("ID") = dssql.Tables(0).Rows(dssql.Tables(0).Rows.Count - 1).Item("doc_id")
                    draux2("Nombre del documento") = dssql.Tables(0).Rows(dssql.Tables(0).Rows.Count - 1).Item("doc_name")
                    draux2("Etapa") = dssql.Tables(0).Rows(dssql.Tables(0).Rows.Count - 1).Item("step_name")
                    draux2("Estado") = dssql.Tables(0).Rows(dssql.Tables(0).Rows.Count - 1).Item("state")
                    Dim ts2 As TimeSpan = (DirectCast(timenow, System.DateTime) - DirectCast(DirectCast(dssql.Tables(0).Rows(dssql.Tables(0).Rows.Count - 1).Item("fecha"), System.Object), System.DateTime))
                    If ts2.Days > 0 Then
                        draux2("Tiempo estimado") = ts2.Days & If(ts2.Days = 1, " Dia", " Dias") & " " & ts2.Hours.ToString("#00") & ":" & ts2.Minutes.ToString("#00") & ":" & ts2.Seconds.ToString("#00")
                    Else
                        draux2("Tiempo estimado") = ts2.Hours.ToString("#00") & ":" & ts2.Minutes.ToString("#00") & ":" & ts2.Seconds.ToString("#00")
                    End If
                    dtaux.Rows.Add(draux2)
                End If


                Dim dsaux As New DataSet
                dsaux.Tables.Add(dtaux)
                Return dsaux

            ElseIf id = 99998 Then
                '[Ezequiel] 13/04/09 - Obtengo todos los registros de cambios de estados

                Dim hsrows As New Hashtable
                Dim sqlcomp As String = "select query from reporte_general where id=" & id
                sqlcomp = Server.Con.ExecuteScalar(CommandType.Text, sqlcomp)


                '[Ezequiel] 13/04/09 - Creo la estructura del dataset a mostrar
                Dim dssql As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sqlcomp)

                Dim num As Int16 = -1

                '[Ezequiel] 13/04/09 - Obtengo la fecha actual de la base de datos
                Dim timenow As Object
                If Server.ServerType = DBTYPES.MSSQLServer7Up Then
                    timenow = Server.Con.ExecuteScalar(CommandType.Text, "select getdate()")
                Else
                    timenow = Server.Con.ExecuteScalar(CommandType.Text, "select sysdate")
                End If
                For Each dr As DataRow In dssql.Tables(0).Rows
                    '[Ezequiel] 13/04/09 - Esta validacion es debido a que si es el ultimo cambio de estado de la tarea la tengo que comprar
                    '                      con el tiempo del servidor.
                    If num = -1 OrElse num <> Int16.Parse(dr("doc_id")) Then
                        If num <> -1 Then
                            Dim numaux As Int16 = dssql.Tables(0).Rows.IndexOf(dr) - 1
                            Dim ts As TimeSpan = (DirectCast(timenow, System.DateTime) - DirectCast(DirectCast(dssql.Tables(0).Rows(numaux).Item("fecha"), System.Object), System.DateTime))
                            SumTime(hsrows, dssql.Tables(0).Rows(numaux).Item("stepid"), dssql.Tables(0).Rows(numaux).Item("step_name"), dssql.Tables(0).Rows(numaux).Item("state"), ts, dssql.Tables(0).Rows(numaux).Item("doc_id"))
                        End If
                        num = dr("doc_id")
                    Else
                        Dim numaux As Int16 = dssql.Tables(0).Rows.IndexOf(dr) - 1
                        Dim ts As TimeSpan = (DirectCast(DirectCast(dr("fecha"), System.Object), System.DateTime) - DirectCast(DirectCast(dssql.Tables(0).Rows(numaux).Item("fecha"), System.Object), System.DateTime))
                        SumTime(hsrows, dssql.Tables(0).Rows(numaux).Item("stepid"), dssql.Tables(0).Rows(numaux).Item("step_name"), dssql.Tables(0).Rows(numaux).Item("state"), ts, dssql.Tables(0).Rows(numaux).Item("doc_id"))
                    End If
                Next

                '[Ezequiel] 13/04/09 - Guardo el tiempo del ultimo registro.
                If dssql.Tables(0).Rows.Count > 0 Then
                    Dim ts2 As TimeSpan = (DirectCast(timenow, System.DateTime) - DirectCast(DirectCast(dssql.Tables(0).Rows(dssql.Tables(0).Rows.Count - 1).Item("fecha"), System.Object), System.DateTime))

                    SumTime(hsrows, dssql.Tables(0).Rows(dssql.Tables(0).Rows.Count - 1).Item("stepid"), dssql.Tables(0).Rows(dssql.Tables(0).Rows.Count - 1).Item("step_name"), dssql.Tables(0).Rows(dssql.Tables(0).Rows.Count - 1).Item("state"), ts2, dssql.Tables(0).Rows(dssql.Tables(0).Rows.Count - 1).Item("doc_id"))
                End If

                Dim dtaux As New DataTable
                With dtaux
                    .Columns.Add(New DataColumn("Etapa"))
                    .Columns.Add(New DataColumn("Estado"))
                    .Columns.Add(New DataColumn("Tiempo promedio"))
                End With

                Dim dsaux As New DataSet
                dsaux.Tables.Add(dtaux)

                For Each araux As ArrayList In hsrows.Values
                    Dim draux As DataRow = dtaux.NewRow
                    draux("Etapa") = araux(0)
                    draux("Estado") = araux(1)
                    Dim tmaux As TimeSpan = TimeSpan.FromTicks(DirectCast(araux(2), TimeSpan).Ticks / DirectCast(araux(3), ArrayList).Count)
                    If tmaux.Days > 0 Then
                        draux("Tiempo promedio") = tmaux.Days & If(tmaux.Days = 1, " Dia", " Dias") & " " & tmaux.Hours.ToString("#00") & ":" & tmaux.Minutes.ToString("#00") & ":" & tmaux.Seconds.ToString("#00")
                    Else
                        draux("Tiempo promedio") = tmaux.Hours.ToString("#00") & ":" & tmaux.Minutes.ToString("#00") & ":" & tmaux.Seconds.ToString("#00")
                    End If
                    dtaux.Rows.Add(draux)
                Next

                dsaux.Tables(0).MinimumCapacity = dsaux.Tables(0).Rows.Count

                Return dsaux

            Else

                'Execute the query
                Dim dtreport As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "select query,completar, groupexpression from reporte_general where id=" & id)
                Dim sql As String
                Dim sqlcomp As String
                Dim sqlgroup As String
                If IsDBNull(dtreport.Tables(0).Rows(0)("completar")) = False Then
                    sqlcomp = dtreport.Tables(0).Rows(0)("completar").ToString
                Else
                    sqlcomp = String.Empty
                End If
                If IsDBNull(dtreport.Tables(0).Rows(0)("groupexpression")) = False Then
                    sqlgroup = dtreport.Tables(0).Rows(0)("groupexpression").ToString
                Else
                    sqlgroup = String.Empty
                End If

                sql = dtreport.Tables(0).Rows(0)("query").ToString
                Dim ListofConditions As New List(Of ReportCondition)
                If Not sql.ToLower.Trim.StartsWith("exec ") OrElse Not Server.isOracle Then
                    If Not IsNothing(sqlcomp) AndAlso String.Compare(sqlcomp, String.Empty) <> 0 Then
                        For Each comp As String In sqlcomp.Split("|")
                            'If comp.ToUpper.Contains("GROUP BY") Then
                            '    sql = sql & " " & comp
                            'Else
                            'Dim sqlorder As String = String.Empty
                            'If sql.ToUpper.Contains("ORDER BY") Then
                            ' sqlorder = sql.Substring(sql.ToUpper.IndexOf("ORDER BY"))
                            'sql = sql.Remove(sql.ToUpper.IndexOf("ORDER BY"))
                            'End If
                            Dim campo As String = comp.Split("§")(0)
                            Dim comparator As String = comp.Split("§")(1)
                            '[sebastian 16-04-09] si tengo una descripcion como pregunta para que el usuario
                            'sepa que ingresar, la muestro. Caso contrario se muestra vacio.
                            Dim Question As String = String.Empty

                            '[Sebastian 17-04-09] verifico si la columna tiene una "i" porque si es asi es una
                            'columna de la doc_I, caso contrario sigo la ejecucion normal.

                            If campo.Split(".").Length > 1 AndAlso IsNumeric(campo.Split(".")(1).Replace(Chr(34), String.Empty).Substring(1)) = True AndAlso campo.Split(".")(0).ToLower.Contains("doc") AndAlso (IndexsBusiness.GetIndexDropDownType(Int64.Parse(campo.Split(".")(1).Replace(Chr(34), String.Empty).Substring(1))) = 2 OrElse IndexsBusiness.GetIndexDropDownType(Int64.Parse(campo.Split(".")(1).Replace(Chr(34), String.Empty).Substring(1))) = 4) Then
                                Dim indexValueSearch As New frmIndexValueSearch(campo.Split(".")(1))
                                '[Sebastian 17-04-09] muestro el formuario que contiene la tabla o lista de
                                'sustitucion.
                                indexValueSearch.ShowDialog()

                                If indexValueSearch.DialogResult = DialogResult.OK Then
                                    Dim rc As New ReportCondition
                                    rc.isZamba = False
                                    rc.preg = indexValueSearch.cmbIndexValue.Text
                                    rc.currentIndex = Nothing
                                    ListofConditions.Add(rc)
                                End If

                            Else
                                If comp.Split("§").Length > 2 AndAlso campo.Split(".").Length > 1 Then
                                    Question = comp.Split("§")(2)

                                    Dim rc As New ReportCondition
                                    rc.isZamba = False
                                    rc.preg = Question
                                    rc.tableName = campo.Split(".")(0)
                                    rc.tableIndex = campo.Split(".")(1)
                                    rc.conditions = comparator
                                    rc.currentIndex = Nothing
                                    rc.indexName = rc.tableIndex
                                    ListofConditions.Add(rc)

                                    'Dim ConditionBox As New ConditionBox(ListofConditions)
                                    'ConditionBox.ShowDialog()
                                    'If Not ConditionBox.bolOk Then Return Nothing
                                    'ListofConditions = ConditionBox.ListofConditions

                                Else

                                    Dim rc As New ReportCondition
                                    rc.isZamba = False
                                    rc.preg = comp.Split("§")(0)
                                    rc.currentIndex = Nothing
                                    ListofConditions.Add(rc)
                                    'Dim ConditionBox As New ConditionBox(ListofConditions)
                                    'ConditionBox.ShowDialog()
                                    'If Not ConditionBox.bolOk Then Return Nothing
                                    'ListofConditions = ConditionBox.ListofConditions

                                End If

                            End If


                            'End If
                        Next

                    End If
                    sql = generateQueryForReport(ListofConditions, sql, sqlgroup)
                    ListofConditions.Clear()
                    If sql IsNot Nothing Then

                        GetPregConditions(sql, ListofConditions)

                        Dim myConditionBox As New ConditionBox(ListofConditions)
                        If myConditionBox.bolOk = False Then
                            myConditionBox.ShowDialog()
                        End If
                        If Not myConditionBox.bolOk Then Return Nothing
                        ListofConditions = myConditionBox.ListofConditions

                        dtreport = Server.Con.ExecuteDataset(CommandType.Text, sql)
                    Else
                        Return Nothing
                    End If
                Else

                    'Caso contrario la sentencia a ejecutar es un store

                    'Armo los filtros

                    Dim hscomp As New Hashtable

                    If Not String.IsNullOrEmpty(sqlcomp) Then
                        Dim arcomps As String() = sqlcomp.Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries)

                        For Each comp As String In arcomps

                            Dim Question As String = comp.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)(1)

                            'Dim ListofConditions As New List(Of ReportCondition)
                            Dim rc As New ReportCondition
                            rc.isZamba = False
                            rc.preg = Question
                            rc.currentIndex = Nothing
                            ListofConditions.Add(rc)
                            Dim ConditionBox As New ConditionBox(ListofConditions)
                            If ConditionBox.bolOk = False Then
                                ConditionBox.ShowDialog()
                            End If

                            If Not ConditionBox.bolOk Then Return Nothing
                            ListofConditions = ConditionBox.ListofConditions

                            If ConditionBox.bolOk Then
                                ListofConditions = ConditionBox.ListofConditions
                            ElseIf comp.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries).Length > 2 Then
                                rc.values = comp.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)(2).Trim
                            Else
                                rc.values = String.Empty
                            End If

                            hscomp.Add(comp.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)(0).Trim, rc.values)
                        Next


                    End If


                    'Aca va tu condition
                    '[Ezequiel] - 07/10/09 - Si el servidor es de oracle y la consulta comienza
                    '                       con exec entonces ejecuto un procedimiento     
                    If Server.isOracle Then

                        Dim params As List(Of Object) = ServersBusiness.GetStoreParams(sql, hscomp)
                        dtreport = Server.Con.ExecuteDataset(params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    End If
                End If
                dtreport.Tables(0).MinimumCapacity = dtreport.Tables(0).Rows.Count
                Return dtreport
            End If


        Catch ex As Exception
            MessageBox.Show("Se produjo un error al ejecutar el reporte", "Consulta Finalizada", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    Private Shared Sub GetPregConditions(sql As String, ByRef listCondition As List(Of ReportCondition))

        Dim condition As String
        Dim myAnswer As String

        'Por cada variable {{var}} se agrega a la lista de objects
        Dim divString As String() = sql.ToString.Split(New Char() {"{{", "}}"})

        If Not IsNothing(divString) AndAlso divString.Count > 0 Then
            For Each varQuery As String In divString

                If Not String.IsNullOrEmpty(varQuery.Trim) AndAlso Not varQuery.Contains(New Char() {vbCrLf, vbLf, vbCr}) Then

                    If varQuery.ToLower.Contains("where") AndAlso varQuery.EndsWith("= ") Then

                        condition = varQuery.Substring(varQuery.ToLower.LastIndexOf("="))

                        'myCondition.conditions = varQuery.Substring(varQuery.ToLower.LastIndexOf("="))

                    ElseIf varQuery.ToLower.Contains("and") AndAlso varQuery.EndsWith("= ") Then
                        condition = varQuery.Substring(varQuery.ToLower.LastIndexOf("="))
                        'myCondition.conditions = varQuery.Substring(varQuery.ToLower.LastIndexOf("="))
                    Else
                        myAnswer = varQuery.ToString()
                    End If
                End If

                If Not String.IsNullOrEmpty(condition) AndAlso Not String.IsNullOrEmpty(myAnswer) Then
                    Dim myCondition As New ReportCondition
                    myCondition.isZamba = False
                    myCondition.currentIndex = Nothing
                    myCondition.preg = myAnswer
                    myCondition.conditions = condition
                    listCondition.Add(myCondition)
                    condition = String.Empty
                    myAnswer = String.Empty
                End If
            Next
        End If
    End Sub

    Private Function generateQueryForReport(listofConditions As List(Of ReportCondition), sql As String, sqlgroup As String) As String
        Dim sqlorder = String.Empty
        If sql.ToUpper.Contains("ORDER BY") Then
            sqlorder = sql.Substring(sql.ToUpper.IndexOf("ORDER BY"))
            sql = sql.Remove(sql.ToUpper.IndexOf("ORDER BY"))
        End If

        If listofConditions.Count > 0 Then
            Dim ConditionBox As New ConditionBox(listofConditions)
            If ConditionBox.bolOk = False Then
                ConditionBox.ShowDialog()
            End If

            If Not ConditionBox.bolOk Then Return Nothing
            listofConditions = ConditionBox.ListofConditions
            If Not listofConditions Is Nothing Then
                For Each reportcondition As ReportCondition In listofConditions
                    Dim Valor As String = reportcondition.values
                    If Not String.IsNullOrEmpty(Valor) Then
                        If sql.ToUpper.Contains(" WHERE") OrElse sql.ToUpper.Contains("WHERE ") Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "contiene where...")
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "contenido de valor: " & Valor)
                            If IsNumeric(Valor) = True Then
                                sql = sql & " AND " & reportcondition.tableIndex & reportcondition.conditions & Valor
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "contenido de query: " & sql)
                            ElseIf Valor.Contains("\") Or Valor.Contains("/") AndAlso IsDate(Valor) Then
                                sql = sql & " AND " & reportcondition.tableIndex & reportcondition.conditions & Server.Con.ConvertDate(Valor)
                            Else
                                If reportcondition.conditions.ToUpper.Contains("LIKE") Then
                                    sql = sql & " AND " & reportcondition.tableIndex & reportcondition.conditions & "'%" & Valor & "%'"
                                Else
                                    sql = sql & " AND " & reportcondition.tableIndex & reportcondition.conditions & "'" & Valor & "'"
                                End If

                                ZTrace.WriteLineIf(ZTrace.IsInfo, "contenido de query: " & sql)
                            End If

                        Else
                            If IsNumeric(Valor) = True Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "no contiene where...")
                                sql = sql & " WHERE " & reportcondition.tableIndex & reportcondition.conditions & Valor
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "contenido de query: " & sql)
                            ElseIf Valor.Contains("\") Or Valor.Contains("/") AndAlso IsDate(Valor) Then
                                sql = sql & " WHERE " & reportcondition.tableIndex & reportcondition.conditions & Server.Con.ConvertDate(Valor)
                            Else
                                sql = sql & " WHERE " & reportcondition.tableIndex & reportcondition.conditions & "'" & Valor & "'"
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "contenido de query: " & sql)
                            End If
                        End If
                    End If
                Next
            End If
        End If
        If sql.ToUpper.Contains("GROUP BY") AndAlso sqlgroup <> String.Empty Then
            sql = sql & " " & sqlgroup
        ElseIf sqlgroup <> String.Empty Then
            sql = sql & " GROUP BY " & sqlgroup
        End If
        Return sql & " " & sqlorder
    End Function

    Private Sub SumTime(ByRef hs As Hashtable, ByVal id As Int64, ByRef _step As String, ByVal state As String, ByVal ti As TimeSpan, ByVal taskid As Int64)
        If hs.ContainsKey(id.ToString & state) Then
            DirectCast(hs(id.ToString & state), ArrayList)(2) = DirectCast(DirectCast(hs(id.ToString & state), ArrayList)(2), TimeSpan) + ti
            If Not DirectCast(DirectCast(hs(id.ToString & state), ArrayList)(3), ArrayList).Contains(taskid) Then
                DirectCast(DirectCast(hs(id.ToString & state), ArrayList)(3), ArrayList).Add(taskid)
            End If
        Else
            Dim ar As New ArrayList
            ar.Add(_step)
            ar.Add(state)
            ar.Add(ti)
            Dim araux As New ArrayList
            araux.Add(taskid)
            ar.Add(araux)
            hs.Add(id.ToString & state, ar)
        End If
    End Sub

    Public Function GenerateQueryBuilderReporteGeneral(ByVal id As Int32) As String
        Try
            'Execute the query
            Dim sqlcomp As String = "select completar from reporte_general where id=" & id

            If IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, sqlcomp)) = False Then
                sqlcomp = Server.Con.ExecuteScalar(CommandType.Text, sqlcomp)
            Else
                sqlcomp = String.Empty
            End If


            Dim sql As String = "select query from reporte_general where id=" & id


            If Not IsNothing(sqlcomp) AndAlso String.Compare(sqlcomp, String.Empty) <> 0 Then
                For Each comp As String In sqlcomp.Split("|")

                    If comp.ToUpper.Contains("GROUP BY") Then
                        sql = sql & comp
                    Else
                        Dim campo As String = comp.Split("§")(0)
                        Dim comparator As String = comp.Split("§")(1)

                        Dim valor As String

                        If comp.Split("§").Length > 2 Then
                            valor = InputBox(comp.Split("§")(2), "Ingreso de valor")
                        Else
                            valor = InputBox(campo, "Ingreso de valor")
                        End If

                        If sql.ToUpper.Contains("WHERE") Then
                            sql = sql & " AND " & campo & comparator & "'" & valor & "'"
                        Else
                            sql = sql & " WHERE " & campo & comparator & "'" & valor & "'"
                        End If
                    End If
                Next
            End If


            Return sql

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Generate the query and return a dataset with the data
    ''' </summary>
    ''' <param name="id">Query ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[Ezequiel] Modified - 12/08/09 - Se arreglaron varios bugs para oracle y  validaciones al tener columnas con espacios</history>
    Public Function GenerateQueryBuilder(ByVal id As Int32, ByVal isZamba As Boolean, ByVal askCondition As Boolean) As String

        Dim colnameflag As Boolean = False
        Dim fields As String = GetFields(id)

        For Each Str As String In fields.Split(",")
            If Not String.IsNullOrEmpty(Str) Then
                If Str.Contains(" As ") Then
                    If Str.Split(Chr(34) & " As")(3).Contains(" ") Then
                        colnameflag = True
                        Exit For
                    End If
                Else
                    If Str.Split(",")(0).Replace(Chr(34), String.Empty).Split(".")(1).Contains(" ") Then
                        colnameflag = True
                        Exit For
                    End If
                End If
            End If
        Next

        Dim relations As String = GetRelations(id)
        If Not colnameflag Then
            For Each Str As String In relations.Split(",")
                If Not String.IsNullOrEmpty(Str) AndAlso Str.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries).Length = 7 Then
                    If Str.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(2).Contains(" ") OrElse Str.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(6).Contains(" ") Then
                        colnameflag = True
                        Exit For
                    End If
                End If
            Next
        End If

        Dim cond As String = GetConditions(id)
        If Not colnameflag Then
            For Each Str As String In cond.Split(",")
                If Not String.IsNullOrEmpty(Str) AndAlso Str.Split(Chr(34) & " As")(3).Contains(" ") Then
                    colnameflag = True
                    Exit For
                End If
            Next



        End If

        Dim SortEx As String = GetSortExpression(id)
        If Not colnameflag Then
            For Each Str As String In SortEx.Split("#")
                If Not String.IsNullOrEmpty(Str) AndAlso Str.Split(Chr(34))(3).Contains(" ") Then
                    colnameflag = True
                    Exit For
                End If
            Next
        End If

        Dim sql As New StringBuilder()
        sql.Append("Select ")
        If GetDistinct(id) = True Then
            sql.Append("Distinct ")
        End If

        Dim values As String = fields
        Dim bolFirst As Boolean = True

        While values <> String.Empty
            Dim strItem As String = values.Split(",")(0)
            If bolFirst = False Then sql.Append(",")
            bolFirst = False
            'If contains as then use it, else if it is a doc_type index get the alias
            If strItem.Contains(" As ") Then
                sql.Append(strItem.Split(Chr(34) & " As")(1))
                sql.Append("." & SetColumnName(strItem.Split(Chr(34) & " As")(3), colnameflag))
                sql.Append(" As ")
                sql.Append(Chr(34) & strItem.Split(Chr(34) & " As")(4).Remove(0, 4).Trim & Chr(34))
                'HashIndexAdded.Add(SetColumnName(strItem.Split(Chr(34) & " As")(3), colnameflag).Replace("I", String.Empty), _
                '                    strItem.Split(Chr(34) & " As")(4).Remove(0, 4).Trim)
            Else
                Dim a As String = strItem.Split(",")(0).Replace(Chr(34), String.Empty)

                If (a.Split(".").Count > 1) Then
                    sql.Append(strItem.Split(",")(0).Replace(Chr(34), String.Empty).Split(".")(0))
                    sql.Append("." & SetColumnName(strItem.Split(",")(0).Replace(Chr(34), String.Empty).Split(".")(1), colnameflag))
                    sql.Append(" As ")
                    sql.Append(Chr(34) & getField(strItem.Split(".")(1), isZamba).Value & Chr(34))
                Else
                    sql.Append(strItem.Split(",")(0).Replace(Chr(34), String.Empty).Split(".")(0))
                    sql.Append("." & SetColumnName(strItem.Split(",")(0).Replace(Chr(34), String.Empty).Split(".")(0), colnameflag))
                    sql.Append(" As ")
                    sql.Append(Chr(34) & getField(strItem.Split(".")(0), isZamba).Value & Chr(34))

                End If
            End If

            'The index it's in the query so erase it
            values = values.Remove(0, strItem.Split(",")(0).Length)
            If values.Length > 0 Then
                values = values.Remove(0, 1)
            End If
        End While

        sql.Append(" from ")

        'Make the Join Sentece
        Dim strAux As String = relations
        Dim bolcont As Boolean = False
        If Not IsNothing(strAux) AndAlso strAux <> String.Empty Then
            bolFirst = True
            Dim primarytablename As String = String.Empty
            Dim lstRelations As New List(Of String)
            Dim auxRelation As New StringBuilder()
            Dim strItem As String
            Dim comp As String

            While strAux <> String.Empty
                If auxRelation.Length > 0 Then auxRelation.Remove(0, auxRelation.Length)
                strItem = strAux.Split(",")(0)

                If Not strItem.Contains("<>") Then
                    bolcont = True
                    If bolFirst = True Then
                        primarytablename = strItem.Split(".")(0).Replace(Chr(34), String.Empty)
                        sql.Append(primarytablename & " ")
                    End If
                    bolFirst = False

                    If strItem.Contains("<=") Then
                        comp = "<="
                        auxRelation.Append(" RIGHT JOIN ")
                    ElseIf strItem.Contains(">=") Then
                        comp = ">="
                        auxRelation.Append(" LEFT JOIN ")
                    ElseIf strItem.Contains("=") Then
                        comp = "="
                        auxRelation.Append(" INNER JOIN ")
                    Else
                        comp = "<>"
                        auxRelation.Append(" OUTER JOIN ")
                    End If

                    'If not already exists on the query then make the join sentence else add condition
                    If ExistsRelation(strItem, comp) = False Then
                        auxRelation.Append(IIf(primarytablename = strItem.Split(comp)(1).Replace("=", String.Empty).Split(".")(0).Replace(Chr(34), String.Empty), strItem.Split(comp)(0).Replace("=", String.Empty).Split(".")(0).Replace(Chr(34), String.Empty), strItem.Split(comp)(1).Replace("=", String.Empty).Split(".")(0).Replace(Chr(34), String.Empty)).ToString.Replace(Chr(34), String.Empty))
                        auxRelation.Append(" ON ")
                        If strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries).Length = 7 Then
                            auxRelation.Append(strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(0) & strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(1) & SetColumnName(strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(2), colnameflag) & "=" & strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(4) & strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(5) & SetColumnName(strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(6), colnameflag))
                        Else
                            auxRelation.Append(strItem.Replace(Chr(34), String.Empty).Replace("<>", "=").Replace(">", String.Empty).Replace("<", String.Empty))
                        End If
                    Else
                        auxRelation.Append(" And ")
                        If strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries).Length = 7 Then
                            auxRelation.Append(strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(0) & strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(1) & SetColumnName(strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(2), colnameflag) & "=" & strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(4) & strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(5) & SetColumnName(strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(6), colnameflag))
                        Else
                            auxRelation.Append(strItem.Replace(Chr(34), String.Empty).Replace("<>", "=").Replace(">", String.Empty).Replace("<", String.Empty))
                        End If
                    End If

                    'Se agrega a una lista para luego reordonar los joins
                    lstRelations.Add(auxRelation.ToString)
                End If

                'The index it's in the query so erase it
                strAux = strAux.Remove(0, strAux.Split(",")(0).Length)
                If strAux.Length > 0 Then
                    strAux = strAux.Remove(0, 1)
                End If
            End While

            'Verifica si debe reordenar los joins para que no generen errores al ejecutarlos
            Dim auxRel As New List(Of String)
            Dim i As Int32 = 0
            Dim j As Int32 = 0

            'Reordenamiento de los joins
            While lstRelations.Count > 0
                'Verifica si la tabla padre existe en el join a realizar
                If lstRelations(i).Contains(primarytablename) Then
                    'Agrega el join que utiliza a la tabla padre debajo de la misma
                    auxRel.Add(lstRelations(i))
                    'Quita el join ordenado
                    lstRelations.RemoveAt(i)
                    i = 0
                Else
                    'Verifica si debe continuar buscando por la misma tabla padre
                    If lstRelations.Count > i + 1 Then
                        i += 1
                    Else
                        'Modifica la tabla padre por una de sus hijas
                        'Ahora comenzará el ciclo buscando las hijas de la actual tabla padre
                        primarytablename = auxRel(j)
                        primarytablename = primarytablename.Substring(primarytablename.IndexOf(" JOIN ", StringComparison.CurrentCultureIgnoreCase) + 5)
                        primarytablename = primarytablename.Substring(0, primarytablename.IndexOf(" ON ", StringComparison.CurrentCultureIgnoreCase)).Trim
                        'Sube en uno el contador para que la próxima vez se busque por la siguiente tabla hija
                        j += 1
                        'Comienza nuevamente la busqueda por las tablas sin ordenar
                        i = 0
                    End If
                End If
            End While

            'Guarda los joins ordenados
            For Each strJoin As String In auxRel
                sql.Append(strJoin)
            Next

            primarytablename = Nothing
            lstRelations.Clear()
            lstRelations = Nothing
            auxRelation = Nothing
            strItem = Nothing
            comp = Nothing

        Else
            Dim tablas As String = GetTables(id)
            sql.Append(tablas.Replace("[Distinct]", String.Empty).Replace(Chr(34), String.Empty))
            tablas = Nothing
        End If

        'Make the where sentence
        strAux = GetRelations(id)
        If bolcont = False And strAux <> String.Empty Then
            sql.Append(strAux.Split(Char.Parse(","))(0).Split(".")(0))
        End If

        If Not IsNothing(strAux) AndAlso strAux <> String.Empty Then
            retArray.Clear()
            bolFirst = True
            While strAux <> String.Empty
                Dim strItem As String = strAux.Split(",")(0)
                If Not strItem.Contains("=") Then
                    If bolFirst = True Then
                        sql.Append(" where ")
                    Else
                        sql.Append(" And ")
                    End If
                    bolFirst = False

                    'If not exists already on the query make a not in sentence
                    If ExistsRelation(strItem, "<>") = False Then
                        sql.Append(strItem.Split("<>")(0))
                        sql.Append(" not in (Select ")
                        sql.Append(strItem.Split("<>")(1).Split(".")(1))
                        sql.Append(" from ")
                        Dim tablename As String = strItem.Split("<>")(1).Split(".")(0).Replace(">", String.Empty).Replace(Chr(34), String.Empty)
                        sql.Append(strItem.Split("<>")(1).Split(".")(0).Replace(">", String.Empty) & " Where " & strItem.Split("<>")(1).Split(".")(1) & " is not null")

                        'Get the conditions(where)
                        Dim strAux1 As String = GetConditions(id)
                        If strAux1 <> String.Empty Then
                            While strAux1 <> String.Empty
                                Dim strItem1 As String = strAux1.Split(",")(0)
                                Dim tableIndex1 As String = strItem1.Split(Chr(34) & " As")(1)
                                If tablename = tableIndex1 Then
                                    Dim conditions1 As String = strItem1.Split(".")(1).Split("|")(1)
                                    values = strItem1.Split(".")(1).Split("|")(2)
                                    If values = String.Empty And askCondition = True Then
                                        Dim tableName1 As String = GetTable(strItem1.Split(Chr(34) & " As")(1), isZamba)
                                        Dim indexName1 As String = getField(strItem1.Split(Chr(34) & " As")(3), isZamba).Value
                                        Dim currentIndex As IIndex = IndexsBussinesExt.getIndex(Zamba.Core.IndexsBusiness.GetIndexIdByName(indexName1), True)

                                        'Ask for the value to filter
                                        Dim ListofConditions As New List(Of ReportCondition)
                                        Dim rc As New ReportCondition
                                        rc.isZamba = isZamba
                                        rc.preg = tablename & "." & indexName1 & " " & conditions1
                                        rc.currentIndex = currentIndex
                                        rc.currentIndex.Operator = rc.conditions
                                        rc.tableName = tablename
                                        rc.tableIndex = indexName1
                                        rc.conditions = conditions1
                                        ListofConditions.Add(rc)
                                        Dim ConditionBox As New ConditionBox(ListofConditions)
                                        If ConditionBox.bolOk = False Then
                                            ConditionBox.ShowDialog()
                                        End If

                                        If Not ConditionBox.bolOk Then Return String.Empty
                                        ListofConditions = ConditionBox.ListofConditions

                                        If ConditionBox.bolOk Then
                                            values = ListofConditions(0).values
                                        Else
                                            'If cancel, then stop the query
                                            Return Nothing
                                        End If
                                    End If
                                    'Make the where sentence
                                    If values <> String.Empty Then
                                        If sql.ToString.ToLower().Contains("where") Then
                                            'If it has @ then it's part of an or clausule
                                            If strItem1.EndsWith("@") Then
                                                sql.Append(" or ")
                                                strItem1 = strItem1.Replace("@", String.Empty)
                                                values = values.Replace("@", String.Empty)
                                            Else
                                                sql.Append(" And ")
                                            End If
                                        Else
                                            sql.Append(" where ")
                                        End If
                                        sql.Append(Chr(34) & strItem1.Split(Chr(34) & " As")(3) & Chr(34))
                                        sql.Append(conditions1)

                                        'If the value is date then convert it
                                        If values.Contains("\") Or values.Contains("\") AndAlso IsDate(values) Then
                                            If values <> String.Empty Then
                                                sql.Append(Server.Con.ConvertDate(values))
                                            End If
                                        ElseIf values.ToLower().Contains("getdate") Then
                                            sql.Append(values)
                                        Else
                                            sql.Append("'" & values & "'")
                                        End If
                                    End If
                                End If
                                strAux1 = strAux1.Remove(0, strAux1.Split(",")(0).Length)
                                If strAux1.Length > 0 Then
                                    strAux1 = strAux1.Remove(0, 1)
                                End If
                            End While
                        End If
                    End If
                    sql.Append(")")
                End If
                strAux = strAux.Remove(0, strAux.Split(",")(0).Length)
                If strAux.Length > 0 Then
                    strAux = strAux.Remove(0, 1)
                End If
            End While
        End If

        'Insert the conditionals into the query
        Dim bolOr As Boolean = False
        If cond <> String.Empty Then

            Dim ListofConditions As New List(Of ReportCondition)

            For Each currentcondition As String In cond.Split(",")
                If currentcondition <> String.Empty Then
                    Dim reportcondition As New ReportCondition

                    Dim strItem As String = currentcondition
                    Dim tableIndex As String = strItem.Split(Chr(34) & " As")(1)
                    Dim bolExists As Boolean = False
                    For Each tables As String() In retArray
                        If tables(1).Replace(Chr(34), String.Empty) = tableIndex Then
                            bolExists = True
                        End If
                    Next

                    If bolExists = False Then
                        Dim conditions As String = strItem.Split(".")(1).Split("|")(1)
                        values = strItem.Split(".")(1).Split("|")(2)
                        Dim indexName As String = getField(strItem.Split(Chr(34) & " As")(3), isZamba).Value

                        Dim indexId As Int64 = getField(strItem.Split(Chr(34) & " As")(3), isZamba).Key

                        Dim tableName As String = GetTable(tableIndex.Split(".")(0), isZamba)
                        Dim idOfIndexName As Int64 = Zamba.Core.IndexsBusiness.GetIndexIdByName(indexName)

                        If idOfIndexName > 0 Then
                            Dim currentIndex As IIndex = IndexsBussinesExt.getIndex(idOfIndexName, True)
                            currentIndex.DataTemp = values
                        End If



                        If values = String.Empty AndAlso askCondition = True Then

                            Dim preg As String
                            If strItem.Split("|")(0).Split(New String() {"&Pregunta&"}, StringSplitOptions.RemoveEmptyEntries).Length > 1 Then
                                preg = strItem.Split("|")(0).Split(New String() {"&Pregunta&"}, StringSplitOptions.RemoveEmptyEntries)(1)
                                strItem = strItem.Replace(strItem.Split("|")(0).Split(New String() {"&Pregunta&"}, StringSplitOptions.RemoveEmptyEntries)(1), String.Empty).Replace("&Pregunta&", String.Empty)
                                If strItem.Split("|")(0).Contains(".") Then
                                    indexName = strItem.Split("|")(0).Split(".")(1)
                                    indexId = getField(indexName, isZamba).Key
                                End If
                            Else
                                preg = indexName 'tableName.Trim() & "." & indexName & " " & conditions
                            End If

                            reportcondition.preg = preg
                        End If


                        reportcondition.isZamba = isZamba
                        reportcondition.indexName = indexName
                        reportcondition.indexId = indexId
                        reportcondition.values = values
                        reportcondition.conditions = conditions
                        reportcondition.strItem = strItem
                        reportcondition.tableIndex = tableIndex
                        reportcondition.tableName = tableIndex.Trim


                    End If

                    ListofConditions.Add(reportcondition)
                End If
            Next

            Dim ConditionBox As New ConditionBox(ListofConditions)
            If ConditionBox.bolOk = False Then
                ConditionBox.ShowDialog()
            End If

            If Not ConditionBox.bolOk Then Return String.Empty
            ListofConditions = ConditionBox.ListofConditions

            If ListofConditions IsNot Nothing Then
                For Each reportcondition As ReportCondition In ListofConditions
                    If reportcondition.values <> String.Empty OrElse reportcondition.conditions.Equals(" IS NULL") OrElse reportcondition.conditions.Equals(" IS NOT NULL") Then
                        If sql.ToString.ToLower().Contains("where") Then
                            If reportcondition.strItem.EndsWith("@1") Then
                                sql.Append(" And (")
                                reportcondition.strItem = reportcondition.strItem.Replace("@1", String.Empty)
                                If Not reportcondition.conditions.Equals(" IS NULL") AndAlso reportcondition.conditions.Equals(" IS NOT NULL") Then
                                    reportcondition.values = reportcondition.values.Replace("@1", String.Empty)
                                End If
                            ElseIf reportcondition.strItem.EndsWith("@2") Then
                                sql.Append(" or ")
                                reportcondition.strItem = reportcondition.strItem.Replace("@2", String.Empty)
                                If Not reportcondition.conditions.Equals(" IS NULL") Then
                                    reportcondition.values = reportcondition.values.Replace("@2", String.Empty)
                                End If
                                bolOr = True
                            Else
                                sql.Append(" And ")
                            End If
                        Else
                            sql.Append(" where ")
                        End If
                        sql.Append(reportcondition.tableIndex & "." & SetColumnName(reportcondition.strItem.Split(Chr(34) & " As")(3), colnameflag))

                        If reportcondition.values.Contains("\") Or reportcondition.values.Contains("/") AndAlso IsDate(reportcondition.values) Then
                            sql.Append(reportcondition.conditions)
                            sql.Append(Server.Con.ConvertDate(reportcondition.values))
                        ElseIf reportcondition.values.ToLower().Contains("getdate") Then
                            sql.Append(reportcondition.conditions)
                            sql.Append(reportcondition.values)
                        ElseIf reportcondition.values.Contains("Null") Then
                            If reportcondition.values.ToLower().Contains("not") Then
                                sql.Append(" is Not Null ")
                            Else
                                sql.Append(" is Null ")
                            End If
                        Else
                            sql.Append(" ")
                            sql.Append(reportcondition.conditions)
                            If reportcondition.conditions.ToLower() <> " is null" AndAlso reportcondition.conditions.ToLower() <> " is not null" Then
                                If reportcondition.conditions.ToLower() <> "like" Then
                                    sql.Append(" '" & reportcondition.values & "' ")
                                Else
                                    sql.Append(" '%" & reportcondition.values & "%' ")
                                End If
                            End If
                        End If
                        If bolOr = True Then
                            bolOr = False
                            sql.Append(")")
                        End If
                    End If

                Next
            End If
        End If

        If Not String.IsNullOrEmpty(SortEx) Then
            sql.Append(" ORDER BY ")
            sql.Append(SortEx.Replace(Chr(34), String.Empty))
        End If


        '================================================================================================
        'links al resutado en caso de que exista configuracion
        Dim table_link As DataTable = GetLinkValuesbyReportID(id)
        If Not table_link Is Nothing AndAlso table_link.Rows.Count > 0 Then
            Dim sqlstring, columns, relation, condition As String
            Dim ToStartToFrom As Short
            sqlstring = sql.ToString()

            ToStartToFrom = sqlstring.ToLower().IndexOf("from")
            columns = sqlstring.Remove(ToStartToFrom)
            relation = sqlstring.Remove(0, columns.Length)

            Dim entityType As String = table_link.Rows(0).Item(2).ToString()
            Dim relationalField As String = table_link.Rows(0).Item(1).ToString()
            Dim entity As String = relationalField.Split(".")(0).ToString().ToUpper()
            Dim columnTitle As String = table_link.Rows(0).Item(3).ToString()

            If String.Compare(columnTitle, "") = 0 Then
                columnTitle = "Ver"
            End If

            If String.Compare(entityType, "task") = 0 Then
                'TAREA
                '
                'obtener relaciones y condiciones del query
                ToStartToFrom = relation.ToLower().IndexOf("where")
                If ToStartToFrom > 0 Then
                    condition = relation.Remove(0, ToStartToFrom)
                    relation = relation.Remove(ToStartToFrom)
                End If

                'si la relacion no esta establecida la genero
                If Not relation.ToLower().Contains("inner join wfdocument on") Then
                    relation &= " INNER JOIN wfdocument ON " & entity & ".doc_id = wfdocument.doc_id"
                End If

                If Server.isOracle Then
                    columns &= "," & Chr(39) & "Zamba:\\TASKID=' ||" & " TO_CHAR(wfdocument.task_id) as " & Chr(34) & columnTitle & Chr(34)
                Else
                    columns &= "," & Chr(39) & "Zamba:\\TASKID=' +" & " cast(" & Chr(34) & "wfdocument" & Chr(34) & "." & Chr(34) & "task_id" & Chr(34) & " as nvarchar(200)) as " & Chr(34) & columnTitle & Chr(34)
                End If


                'vuelver a generar la consulta
                sqlstring = columns & " " & relation & " " & condition
            Else
                'DOCUMENTO
                Dim docid As String = relationalField.Split("|")(0).ToString().Trim()
                Dim doctypeid As String = relationalField.Split("|")(1).ToString().Trim()

                If Server.isOracle Then
                    columns &= "," & Chr(39) & "Zamba:\\DT=' ||" & "TO_CHAR(" & doctypeid & ") ||" & " TO_CHAR('&') ||" &
                        "'DOCID=' ||" & "TO_CHAR(" & docid & ") as" & Chr(34) & columnTitle & Chr(34)

                Else
                    columns &= "," & Chr(39) & "Zamba:\\DT=' +" & " cast(" & doctypeid & " as nvarchar(200)) " & "+ '&DOCID=' +" & " cast(" & docid & " as nvarchar(200)) as " & Chr(34) & columnTitle & Chr(34)

                End If
                sqlstring = columns & " " & relation
            End If

            sql = Nothing
            sql = New StringBuilder()
            sql.Append(sqlstring)
        End If

        Return sql.Replace(Chr(34) & Chr(34), Chr(34)).ToString()

    End Function


    ''' <summary>
    ''' Determina si la columna va con comillas o no
    ''' </summary>
    ''' <param name="colname"></param>
    ''' <param name="addquote"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[Ezequiel] - Created - 12/08/2009</history>
    Private Function SetColumnName(ByVal colname As String, ByVal addquote As Boolean) As String
        If addquote Then
            Return Chr(34) & colname & Chr(34)
        Else
            Return colname
        End If
    End Function

    'Gets the alias of the table
    Private Function GetTable(ByVal table As String, ByVal isZamba As Boolean) As String
        Try
            Dim id As Int64
            table = table.Replace(Chr(34), String.Empty)
            If isZamba = True Then
                If table.ToUpper().Contains("DOC_I") OrElse table.ToUpper().StartsWith("DOC") Then
                    If Int64.TryParse(table.ToUpper().Replace("DOC_I", String.Empty).Replace("DOC", String.Empty), id) Then
                        table = FuncionesZamba.GetDocTypeName(id)
                    End If
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return table
    End Function
    'Gets the alias of the field
    Private Function getField(ByVal field As String, ByVal isZamba As Boolean) As KeyValuePair(Of String, String)
        Try
            field = field.Replace(Chr(34), String.Empty)
            If isZamba = True Then
                Dim id As Int64
                If Int64.TryParse(field.Replace("I", String.Empty), id) Then
                    If HashIndexAdded.Contains(id) = False Then
                        Dim name As String = FuncionesZamba.GetIndexName(id)
                        If name <> String.Empty Then
                            If HashIndexAdded.ContainsKey(id) = False Then HashIndexAdded.Add(id, name.Trim())
                            Return New KeyValuePair(Of String, String)(id, name.Trim())
                        End If
                    Else
                        Return New KeyValuePair(Of String, String)(id, HashIndexAdded(id))
                    End If
                Else
                    id = IndexsBusiness.GetIndexIdByName(field)
                    If id > 0 Then
                        If HashIndexAdded.ContainsKey(id) = False Then HashIndexAdded.Add(id, field.Trim())
                        Return New KeyValuePair(Of String, String)(id, field.Trim())
                    Else
                        Return New KeyValuePair(Of String, String)(0, field.Trim())
                    End If
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return New KeyValuePair(Of String, String)(field, field)
    End Function
    Private retArray As New ArrayList()
    ''' <summary>
    ''' Return true if the relation exists on the array
    ''' </summary>
    ''' <param name="item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExistsRelation(ByVal item As String, ByVal comp As String) As Boolean
        Try
            Dim table(1) As String
            table(0) = item.Split(comp)(0).Split(".")(0)
            table(1) = item.Split(comp)(1).Split(".")(0).Replace(">", String.Empty).Replace("=", String.Empty)

            For Each tables As String() In retArray
                If tables(0) = table(0) OrElse tables(0) = table(1) Then
                    If tables(1) = table(0) OrElse tables(1) = table(1) Then
                        Return True
                    End If
                End If
            Next
            retArray.Add(table)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Gets the tables from the specified id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>string with the tables</returns>
    ''' <remarks></remarks>
    Public Function GetTables(ByVal id As Int32) As String
        Dim strselect As String
        'Dim ds As DataSet = New DataSet
        Try
            strselect = "SELECT Tables FROM ReportBuilder Where Id =" & id
            strselect = Server.Con.ExecuteScalar(CommandType.Text, strselect)
            'strselect = Convert.ToString(ds.Tables(0).Rows(0).Item(0)).Trim
            Return strselect.Trim()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return String.Empty
            'Finally
            'ds.Dispose()
            'ds = Nothing
        End Try
    End Function
    Public Function GetDistinct(ByVal id As Int32) As Boolean
        Dim strselect As String
        Try
            strselect = GetTables(id)
            Return strselect.Contains("[Distinct]")
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Gets the name from the specified id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>string with the tables</returns>
    ''' <remarks></remarks>
    Public Function GetName(ByVal id As Int32) As String
        Dim strselect As String
        'Dim ds As DataSet = New DataSet
        Try
            strselect = "SELECT Name FROM ReportBuilder Where Id =" & id
            strselect = Server.Con.ExecuteScalar(CommandType.Text, strselect)
            'strselect = Convert.ToString(ds.Tables(0).Rows(0).Item(0)).Trim
            Return strselect.Trim()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return String.Empty
            'Finally
            'ds.Dispose()
            'ds = Nothing
        End Try
    End Function
    ''' <summary>
    ''' Gets the indexs from the specified id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>string with the indexs</returns>
    ''' <remarks></remarks>
    Public Function GetFields(ByVal id As Int32) As String
        Dim strselect As String
        'Dim ds As DataSet = New DataSet
        Try
            strselect = "SELECT Fields FROM ReportBuilder Where Id =" & id
            strselect = Server.Con.ExecuteScalar(CommandType.Text, strselect)
            'strselect = Convert.ToString(ds.Tables(0).Rows(0).Item(0)).Trim
            Return strselect.Trim()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return String.Empty
            'Finally
            'ds.Dispose()
            'ds = Nothing
        End Try
    End Function
    ''' <summary>
    ''' Gets the conditions from the specified id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>string with the Conditions</returns>
    ''' <remarks></remarks>
    Public Function GetConditions(ByVal id As Int32) As String
        Dim strselect As Object
        'Dim ds As DataSet = New DataSet
        Try
            strselect = "SELECT Conditions FROM ReportBuilder Where Id =" & id
            strselect = Server.Con.ExecuteScalar(CommandType.Text, strselect)
            'strselect = Convert.ToString(ds.Tables(0).Rows(0).Item(0)).Trim
            If Not IsDBNull(strselect) Then
                Return strselect.Trim()
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return String.Empty
            'Finally
            'ds.Dispose()
            'ds = Nothing
        End Try
    End Function
    ''' <summary>
    ''' Gets the Relations from the specified id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>string with the relations</returns>
    ''' <remarks></remarks>
    Public Function GetRelations(ByVal id As Int32) As String
        Dim strselect As Object
        'Dim ds As DataSet = New DataSet
        Try
            strselect = "SELECT Relations FROM ReportBuilder Where Id =" & id & " order by Relations"
            strselect = Server.Con.ExecuteScalar(CommandType.Text, strselect)
            'strselect = Convert.ToString(ds.Tables(0).Rows(0).Item(0)).Trim
            If Not IsDBNull(strselect) Then
                Return strselect.Trim()
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return String.Empty
            'Finally
            'ds.Dispose()
            'ds = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Get all the stored query Id and names
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetQueryIdsAndNames(ByVal ids As List(Of Int64)) As DataSet
        Dim ds As New DataSet
        Try
            Dim strselect As StringBuilder = New StringBuilder()
            'strselect.Append("select ID,NAME from reportbuilder")
            strselect.Append("select ID,NAME from reportbuilder inner join Zvw_USR_Rights_200 on ZVW_USR_RIGHTS_200.Aditional = reportbuilder.id where objectid = ")
            strselect.Append(ObjectTypes.ModuleReports)
            strselect.Append(" and USER_ID in (")
            For Each id As Int64 In ids
                strselect.Append(id)
                strselect.Append(",")
            Next
            strselect.Remove(strselect.Length - 1, 1)
            strselect.Append(") and RIGHT_TYPE= ")
            strselect.Append(RightsType.View)
            strselect.Append(" ORDER BY NAME")
            ds = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString().ToUpper())
            Return ds
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return ds
        End Try
    End Function
    Public Function GetQueryIdsAndNamesReportGeneral(ByVal ids As List(Of Int64)) As DataSet
        Dim ds As New DataSet
        Try
            Dim strselect As StringBuilder = New StringBuilder()
            'strselect.Append("select ID,NAME from reporte_general")
            strselect.Append("select ID,NAME from reporte_general inner join Zvw_USR_Rights_200 on ZVW_USR_RIGHTS_200.Aditional = reporte_general.id where objectid = ")
            strselect.Append(ObjectTypes.ModuleReports)
            strselect.Append(" and USER_ID in (")
            For Each id As Int64 In ids
                strselect.Append(id)
                strselect.Append(",")
            Next
            strselect.Remove(strselect.Length - 1, 1)
            strselect.Append(") and RIGHT_TYPE= ")
            strselect.Append(RightsType.View)
            strselect.Append(" ORDER BY NAME")
            ds = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString().ToUpper())
            Return ds
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return ds
        End Try
    End Function
    ''' <summary>
    ''' Get all the stored query Id and names
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllQueryIdsAndNames() As DataSet
        Dim ds As New DataSet
        Try
            Dim strselect As StringBuilder = New StringBuilder()
            strselect.Append("select ID,NAME from reportbuilder order by name")
            Return Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString().ToUpper())
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return ds
        End Try
    End Function
    Public Function GetAllQueryIdsAndNamesReporteGeneral() As DataSet
        Dim ds As New DataSet
        Try
            Dim strselect As StringBuilder = New StringBuilder()
            strselect.Append("select ID,NAME from reporte_general order by name")
            Return Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString().ToUpper())
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return ds
        End Try
    End Function

    ''' <summary>
    ''' Get a specified general report ID by the report name.
    ''' </summary>
    ''' <param name="Name">General Report Name</param>
    ''' <remarks></remarks>
    ''' <history>
    '''    [Tomas] 06/04/2009 Created
    ''' </history>
    Public Function GetGeneralReportIdByName(ByVal Name As String) As Int32
        Try
            Dim query = "SELECT ID from Reporte_General WHERE Name=" & "'" & Name & "'"
            Return Server.Con.ExecuteScalar(CommandType.Text, query)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Remove a query of the table
    ''' </summary>
    ''' <param name="id"></param>
    Public Sub DeleteQueryBuilder(ByVal id As Int32)
        Try
            Dim strinsert = "Delete from ReportBuilder where (Id = " & id & ")"
            Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene la expresion de Agrupamiento de registros
    ''' </summary>
    ''' <param name="rptid">Id de reporte</param>
    ''' <remarks></remarks>
    Public Function GetGroupExpression(ByVal rptid As Int32) As String
        Try
            If Not IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, "SELECT GroupExpression FROM REPORTBUILDER WHERE Id=" & rptid)) Then
                Return Server.Con.ExecuteScalar(CommandType.Text, "SELECT GroupExpression FROM REPORTBUILDER WHERE Id=" & rptid)
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la expresion de Agrupamiento de los reportes generales
    ''' </summary>
    ''' <param name="rptid">Id de reporte</param>
    ''' <remarks></remarks>
    Public Function GetGeneralGroupExpression(ByVal rptid As Int32) As String
        Try
            If Not IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, "SELECT GroupExpression FROM reporte_general WHERE Id=" & rptid)) Then
                Return Server.Con.ExecuteScalar(CommandType.Text, "SELECT GroupExpression FROM reporte_general WHERE Id=" & rptid)
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la expresion para ordenamiento de registros
    ''' </summary>
    ''' <param name="rptid">Id de reporte</param>
    ''' <remarks></remarks>
    Public Function GetSortExpression(ByVal rptid As Int32) As String
        Try
            If Not IsDBNull(Server.Con.ExecuteScalar(CommandType.Text, "SELECT SORTEXPRESSION FROM REPORTBUILDER WHERE Id=" & rptid)) Then
                Return Server.Con.ExecuteScalar(CommandType.Text, "SELECT SORTEXPRESSION FROM REPORTBUILDER WHERE Id=" & rptid)
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return String.Empty
        End Try
    End Function
    Public Function GetLinks(ByVal id As Int32) As String
        Try

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return String.Empty
        End Try
    End Function


    Public Sub New()
        HashIndexAdded = New Hashtable
    End Sub


    ''' <summary>
    ''' Las columnas utilizadas para el eje Y del grafico
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Function GetYValueColumn(ByVal idquery As Integer) As String
        Try
            If Server.isOracle Then
                '''Dim parNames() As String = {"ReportID"}
                ''Dim parTypes() As Object = {13}
                Dim parValues() As Object = {idquery}

                Dim query = "SELECT YValueColumn FROM ReportBuilder_ChartOption WHERE ReportID = " & idquery
                Return Server.Con.ExecuteScalar(CommandType.Text, query)
            Else
                Return Server.Con.ExecuteScalar("zsp_ChartReport_100_GetYValueColumn", New Object() {idquery})
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Las columnas utilizadas para el eje X del grafico
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Function GetXValueColumn(ByVal idquery As Integer) As String
        Try
            If Server.isOracle Then
                '''Dim parNames() As String = {"ReportID"}
                ''Dim parTypes() As Object = {13}
                Dim parValues() As Object = {idquery}

                Dim query = "SELECT XValueColumn FROM ReportBuilder_ChartOption WHERE ReportID = " & idquery
                Return Server.Con.ExecuteScalar(CommandType.Text, query)
            Else
                Return Server.Con.ExecuteScalar("zsp_ChartReport_100_GetXValueColumn", New Object() {idquery})
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el tipo del grafico
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Function GetChartType(ByVal idquery As Integer) As Integer
        Try
            Dim returnValue As Integer
            If Server.isOracle Then
                '    ''Dim parNames() As String = {"ReportID"}
                '    'Dim parTypes() As Object = {13}
                Dim parValues() As Object = {idquery}

                Dim query = "SELECT ChartType FROM ReportBuilder_ChartOption WHERE ReportID = " & idquery
                Return Server.Con.ExecuteScalar(CommandType.Text, query)

            Else
                returnValue = Server.Con.ExecuteScalar("zsp_ChartReport_100_GetChartType", New Object() {idquery})
            End If
            If returnValue = 0 Then
                Return -1
            Else
                Return returnValue
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return -1
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los campos de configuracion basica para el grafico en reportes
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <param name="XValueColumns">Valor para el eje X</param>
    ''' <param name="YValueColumns">Valor para el eje Y</param>
    ''' <param name="ChartType">Tipo de grafico</param>
    ''' <remarks></remarks>
    Sub UpdateChartOption(ByVal ReportID As Integer, ByVal XValueColumns As String, ByVal YValueColumns As String, ByVal ChartType As Integer)
        Dim dt As Int32
        Try
            If Server.isOracle Then
                ''Dim parNames() As String = {"ChartReport", "XFields", "YFields", "ChartReportType"}
                'Dim parTypes() As Object = {13, 22, 22, 13}
                Dim parValues() As Object = {ReportID, XValueColumns, YValueColumns, ChartType}

                dt = Server.Con.ExecuteScalar(CommandType.Text, "SELECT count(1) FROM ReportBuilder_ChartOption WHERE ReportID =" & ReportID)

                If dt > 0 Then

                    Dim S As New StringBuilder
                    S.Append("UPDATE ReportBuilder_ChartOption SET XValueColumn= '" & XValueColumns)
                    S.Append("',YValueColumn= '" & YValueColumns & "',ChartType= " & ChartType)
                    S.Append(" WHERE ReportID = " & ReportID)

                    Server.Con.ExecuteNonQuery(CommandType.Text, S.ToString())

                Else

                    Dim S As New StringBuilder
                    S.Append("INSERT INTO ReportBuilder_ChartOption(ReportID,XValueColumn,YValueColumn,ChartType) VALUES ( " & ReportID & ",'" & XValueColumns & "','" & YValueColumns & "'," & ChartType & ")")

                    Server.Con.ExecuteNonQuery(CommandType.Text, S.ToString())

                End If
            Else
                Server.Con.ExecuteNonQuery("zsp_ChartReport_100_UpdateChartReport", New Object() {ReportID, XValueColumns, YValueColumns, ChartType})
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Elimina la configuracion de gráficos, con esto se marca como que no posee
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Sub DeleteChartOption(ByVal idquery As Integer)
        Try
            If Server.isOracle Then
                ''Dim parNames() As String = {"ReportID"}
                'Dim parTypes() As Object = {13}
                Dim parValues() As Object = {idquery}

                Dim S As New StringBuilder
                S.Append("DELETE FROM ReportBuilder_ChartOption WHERE ReportID =" & idquery)

                Server.Con.ExecuteNonQuery(CommandType.Text, S.ToString())

            Else
                Server.Con.ExecuteNonQuery("zsp_ChartReport_100_DelteteChartReport", New Object() {idquery})
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Chequea si hay alguna configuracion para graficos.
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Function CheckReportHaveChart(ByVal idquery As Integer) As Boolean
        Dim gettedValue As Integer
        Try
            If Server.isOracle Then
                '    ''Dim parNames() As String = {"ReportID"}
                '    'Dim parTypes() As Object = {13}
                Dim parValues() As Object = {idquery}

                Dim query = "SELECT count(1) FROM ReportBuilder_ChartOption WHERE ReportID =" & idquery
                Return Server.Con.ExecuteScalar(CommandType.Text, query)
            Else
                gettedValue = Server.Con.ExecuteScalar("zsp_ChartReport_100_CheckReportHaveChart", New Object() {idquery})
            End If
            Return gettedValue > 0
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' obtiene los valores de configuracion para la solapa de links en reportbuilder
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Shared Function GetLinkValuesbyReportID(ByVal idquery As Integer) As DataTable
        Dim dt As DataTable
        Try
            If Server.isOracle Then
                'Server.Con.ExecuteNonQuery("SELECT * FROM ReportBuilder_LinkOption WHERE ID = " & idquery)
                dt = Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM ReportBuilder_LinkOption WHERE ID = " & idquery).Tables(0)
            Else
                dt = Server.Con.ExecuteDataset("zsp_ReportBuilderLinkOption_100_Select", New Object() {idquery}).Tables(0)
            End If
            Return dt
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' inserta los valores de configuracion para la solapa de links en reportbuilder
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Shared Sub InsertUpdateLinkValues(ByVal idquery As Integer, ByVal relatinalField As String,
                                      ByVal Entity As String, ByVal Description As String, ByVal position As Short)
        Try
            If Server.isOracle Then
                'Server.Con.ExecuteNonQuery("INSERT INTO reportbuilder_linkOption ", parNames, parTypes, parValues)
                Dim dt As Int32

                dt = Server.Con.ExecuteScalar(CommandType.Text, "SELECT COUNT(1) FROM ReportBuilder_LinkOption WHERE ID =" & idquery)

                If dt > 0 Then


                    Dim S As New StringBuilder
                    S.Append("UPDATE reportbuilder_linkOption SET RelationalField  ='" & relatinalField & "', EntityType ='" & Entity & "', description = '" & Description & "', Position = " & position & "  WHERE ID =" & idquery)
                    Server.Con.ExecuteNonQuery(CommandType.Text, S.ToString())

                Else

                    Dim insertQuery As New StringBuilder
                    insertQuery.Append("INSERT INTO reportbuilder_linkOption (ID,RELATIONALFIELD,ENTITYTYPE,DESCRIPTION,POSITION) values ('" & idquery)
                    insertQuery.Append("','" & relatinalField)
                    insertQuery.Append("','" & Entity)
                    insertQuery.Append("','" & Description)
                    insertQuery.Append("','" & position & "')")
                    Server.Con.ExecuteNonQuery(insertQuery.ToString)

                End If

            Else
                Server.Con.ExecuteNonQuery("zsp_ReportBuilderLinkOption_100_InsertUpdate", New Object() {idquery, relatinalField, Entity, Description, position})
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' elimina los valores de configuracion para la solapa de links en reportbuilder
    ''' </summary>
    ''' <param name="idquery">Id de reporte</param>
    ''' <remarks></remarks>
    Shared Sub DeleteLinkValuesbyReportID(ByVal idquery As Integer)
        Try
            If Server.isOracle Then
                Server.Con.ExecuteNonQuery("DELETE FROM ReportBuilder_LinkOption WHERE ID = " & idquery)
            Else
                Server.Con.ExecuteNonQuery("zsp_ReportBuilderLinkOption_100_Delete", New Object() {idquery})
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

End Class

