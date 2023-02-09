Imports Zamba.Servers
Imports System.Windows.Forms
Imports System.Text
Imports Zamba.Core
Imports System.Data

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
    Public Sub InsertQueryBuilder(ByVal id As Int32, ByVal name As String, ByVal tables As String, ByVal fields As String, ByVal relations As String, ByVal conditions As String, ByVal SortExpression As String, ByVal GroupExpression As String)
        Try
            Dim strinsert As String = "Insert into ReportBuilder (Id, Name, Tables,Fields,Relations,conditions,SortExpression,GroupExpression) values (" & id & ",'" & name & "','" & tables & "','" & fields & "','" & relations & "','" & conditions & "','" & SortExpression & "','" & GroupExpression & "')"
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
            If Server.ServerType = DBTYPES.MSSQLServer7Up OrElse Server.ServerType = DBTYPES.MSSQLServer Then
                Dim sql As String = "select column_name from information_schema.columns where table_name = '" & name & "'"
                ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
            Else
                Dim sql As String = "Desc " & name
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
            Dim S As New System.Text.StringBuilder

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
    Public Function RunQueryBuilder(ByVal id As Int32, ByVal isZamba As Boolean) As DataSet
        Try
            'Execute the query
            Dim sql As String = GenerateQueryBuilder(id, isZamba)
            If Not IsNothing(sql) AndAlso String.Compare(sql, String.Empty) <> 0 Then
                Return Server.Con.ExecuteDataset(CommandType.Text, sql)
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Run the query with the specified conditions and return a dataset with the data
    ''' </summary>
    ''' <param name="id">Query ID</param>
    ''' <param name="isZamba">Si es o no zamba</param>
    ''' <param name="conditions">Hashtable q contiene indice y valor de la condicion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RunWebQueryBuilder(ByVal id As Int32, ByVal isZamba As Boolean, ByVal conditions As Hashtable) As DataSet
        Try
            'Execute the query
            Dim sql As String = GenerateWebQueryBuilder(id, isZamba, conditions)
            If Not IsNothing(sql) AndAlso String.Compare(sql, String.Empty) <> 0 Then
                Return Server.Con.ExecuteDataset(CommandType.Text, sql)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        End Try
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
    Public Function RunQueryBuilderReporteGeneral(ByVal id As Int32, ByVal vars As Hashtable, Task As ITaskResult) As DataSet
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
                    .Columns.Add(New DataColumn("Nombre"))
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
                            draux("Nombre") = dssql.Tables(0).Rows(numaux).Item("doc_name")
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
                        draux("Nombre") = dssql.Tables(0).Rows(numaux).Item("doc_name")
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
                    draux2("Nombre") = dssql.Tables(0).Rows(dssql.Tables(0).Rows.Count - 1).Item("doc_name")
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
                Dim dtreport As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "select query,completar from reporte_general where id=" & id)
                Dim sql As String
                Dim sqlcomp As String

                If dtreport.Tables(0).Rows.Count > 0 Then
                    If IsDBNull(dtreport.Tables(0).Rows(0)("completar")) = False Then
                        sqlcomp = dtreport.Tables(0).Rows(0)("completar").ToString
                    Else
                        sqlcomp = String.Empty
                    End If

                    sql = dtreport.Tables(0).Rows(0)("query").ToString
                    Dim valor As String = String.Empty

                    If Not sql.ToLower.Trim.StartsWith("exec ") Then
                        If Not IsNothing(sqlcomp) AndAlso String.Compare(sqlcomp, String.Empty) <> 0 Then
                            For Each comp As String In sqlcomp.Split("|")

                                If comp.ToUpper.Contains("GROUP BY") Then
                                    sql = sql & " " & comp
                                Else
                                    Dim sqlorder As String = String.Empty
                                    If sql.ToUpper.Contains(" ORDER BY") Then
                                        sqlorder = sql.Substring(sql.ToUpper.IndexOf("ORDER BY"))
                                        sql = sql.Remove(sql.ToUpper.IndexOf("ORDER BY"))
                                    End If
                                    Dim campo As String = comp.Split("§")(0)
                                    Dim comparator As String = comp.Split("§")(1)
                                    '[sebastian 16-04-09] si tengo una descripcion como pregunta para que el usuario
                                    'sepa que ingresar, la muestro. Caso contrario se muestra vacio.
                                    Dim Question As String = String.Empty

                                    '[Sebastian 17-04-09] verifico si la columna tiene una "i" porque si es asi es una
                                    'columna de la doc_I, caso contrario sigo la ejecucion normal.

                                    If IsNumeric(campo.Split(".")(1).Replace(Chr(34), String.Empty).Substring(1)) = True AndAlso campo.Split(".")(0).ToLower.Contains("doc") AndAlso (IndexsBusiness.GetIndexDropDownType(Int64.Parse(campo.Split(".")(1).Replace(Chr(34), String.Empty).Substring(1))) = 2 OrElse IndexsBusiness.GetIndexDropDownType(Int64.Parse(campo.Split(".")(1).Replace(Chr(34), String.Empty).Substring(1))) = 4) Then
                                        'Dim a As New frmIndexValueSearch(campo.Split(".")(1))
                                        ''[Sebastian 17-04-09] muestro el formuario que contiene la tabla o lista de
                                        ''sustitucion.
                                        'a.ShowDialog()

                                        'If a.DialogResult = DialogResult.OK Then
                                        '    valor = a.cmbIndexValue.Text
                                        'End If

                                    Else

                                        If comp.Split("§").Length > 2 Then
                                            Question = comp.Split("§")(2)
                                            'Dim questionfrm As New ConditionBox(Question)
                                            'questionfrm.ShowDialog()
                                            'If questionfrm.bolOk Then
                                            '    valor = questionfrm.value
                                            'Else
                                            '    MessageBox.Show("Se ha cancelado la consulta", "Zamba Administrador", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            '    Return Nothing
                                            '    Exit Function
                                            'End If
                                        Else
                                            'Dim questionfrm As New ConditionBox(comp.Split("§")(0))
                                            'questionfrm.ShowDialog()
                                            'If questionfrm.bolOk Then
                                            '    valor = questionfrm.value
                                            'Else
                                            '    MessageBox.Show("Se ha cancelado la consulta", "Zamba Administrador", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                            '    Return Nothing
                                            '    Exit Function
                                            'End If
                                        End If

                                    End If

                                    If Not String.IsNullOrEmpty(valor) Then
                                        If sql.ToUpper.Contains(" WHERE") OrElse sql.ToUpper.Contains("WHERE ") Then
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "contiene where...")
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "contenido de valor: " & valor)
                                            If IsNumeric(valor) = True Then
                                                sql = sql & " AND " & campo & comparator & valor
                                                ZTrace.WriteLineIf(ZTrace.IsInfo, "contenido de query: " & sql)
                                            ElseIf valor.Contains("\") Or valor.Contains("/") AndAlso IsDate(valor) Then
                                                sql = sql & " AND " & campo & comparator & Server.Con.ConvertDate(valor)
                                            Else
                                                sql = sql & " AND " & campo & comparator & "'" & valor & "'"
                                                ZTrace.WriteLineIf(ZTrace.IsInfo, "contenido de query: " & sql)
                                            End If

                                        Else
                                            If IsNumeric(valor) = True Then
                                                ZTrace.WriteLineIf(ZTrace.IsInfo, "no contiene where...")
                                                sql = sql & " WHERE " & campo & comparator & valor
                                                ZTrace.WriteLineIf(ZTrace.IsInfo, "contenido de query: " & sql)
                                            ElseIf valor.Contains("\") Or valor.Contains("/") AndAlso IsDate(valor) Then
                                                sql = sql & " WHERE " & campo & comparator & Server.Con.ConvertDate(valor)
                                            Else
                                                sql = sql & " WHERE " & campo & comparator & "'" & valor & "'"
                                                ZTrace.WriteLineIf(ZTrace.IsInfo, "contenido de query: " & sql)
                                            End If
                                        End If
                                    End If
                                    If Not String.IsNullOrEmpty(sqlorder) Then
                                        sql = sql & " " & sqlorder
                                    End If
                                End If
                            Next
                        End If

                        If (vars IsNot Nothing) Then
                            For Each var As String In vars.Keys
                                sql = sql.Replace("zvar(" & var & ")", vars(var))
                                If (var.Contains("zamba.vars")) Then
                                    sql = sql.Replace(var, vars(var))
                                End If
                            Next
                        End If

                        Try
                            sql = sql.Replace("zamba.usuarioactual.id", Membership.MembershipHelper.CurrentUser.ID.ToString())
                            sql = sql.Replace("zamba.usuarioactual.usuario", Membership.MembershipHelper.CurrentUser.Name)
                            sql = sql.Replace("zamba.usuarioactual.nombre", Membership.MembershipHelper.CurrentUser.Nombres)
                            sql = sql.Replace("zamba.usuarioactual.apellido", Membership.MembershipHelper.CurrentUser.Apellidos)
                            sql = sql.Replace("zamba.usuarioactual.mail", Membership.MembershipHelper.CurrentUser.eMail.Mail)
                            sql = sql.Replace("zamba.fechaactual", DateTime.Now.ToShortDateString())
                        Catch ex As Exception

                        End Try

                        Try

                            Dim listaDeLineas As String() = sql.Split("\n")
                            Dim listaDeLineasNew As New List(Of String)

                            For Each lineaActual As String In listaDeLineas

                                If (lineaActual.ToLower().Contains("<<tarea") OrElse lineaActual.ToLower().Contains("<<funciones")) Then
                                    lineaActual = Zamba.Core.TextoInteligente.ReconocerCodigo(lineaActual, Task)
                                    listaDeLineasNew.Add(lineaActual)
                                Else
                                    listaDeLineasNew.Add(lineaActual)
                                End If
                            Next
                            sql = String.Concat(listaDeLineasNew)
                        Catch ex As Exception

                        End Try



                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Consulta Reporte: " & sql)
                        dtreport = Server.Con.ExecuteDataset(CommandType.Text, sql)

                    Else

                        'Caso contrario la sentencia a ejecutar es un store

                        'Armo los filtros

                        Dim hscomp As New Hashtable

                        If Not String.IsNullOrEmpty(sqlcomp) Then
                            Dim arcomps As String() = sqlcomp.Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries)

                            For Each comp As String In arcomps

                                Dim Question As String = comp.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)(1)

                                'Dim questionfrm As New ConditionBox(Question)
                                'questionfrm.ShowDialog()
                                'If questionfrm.bolOk AndAlso Not String.IsNullOrEmpty(questionfrm.value) Then
                                '    valor = questionfrm.value
                                'ElseIf comp.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries).Length > 2 Then
                                '    valor = comp.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)(2).Trim
                                'Else
                                '    valor = String.Empty
                                'End If


                                hscomp.Add(comp.Split(New String() {"§"}, StringSplitOptions.RemoveEmptyEntries)(0).Trim, valor)
                            Next


                        End If






                        Dim params As List(Of Object) = ServersBusiness.GetStoreParams(sql, hscomp)
                        dtreport = Server.Con.ExecuteDataset(params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))

                    End If
                    dtreport.Tables(0).MinimumCapacity = dtreport.Tables(0).Rows.Count
                    Return dtreport
                Else
                    Return Nothing
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return Nothing
        End Try
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
    Public Function GenerateQueryBuilder(ByVal id As Int32, ByVal isZamba As Boolean) As String
        Try
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
                        If Str.Split(",")(0).Replace(Chr(34), "").Split(".")(1).Contains(" ") Then
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

            Dim sql As New System.Text.StringBuilder()
            sql.Append("Select ")
            If GetDistinct(id) = True Then
                sql.Append("Distinct ")
            End If

            Dim values As String = fields
            Dim bolFirst As Boolean = True

            While values <> ""
                Dim strItem As String = values.Split(",")(0)
                If bolFirst = False Then sql.Append(",")
                bolFirst = False
                'If contains as then use it, else if it is a doc_type index get the alias
                If strItem.Contains(" As ") Then
                    sql.Append(strItem.Split(Chr(34) & " As")(1))
                    sql.Append("." & SetColumnName(strItem.Split(Chr(34) & " As")(3), colnameflag))
                    sql.Append(" As ")
                    sql.Append(Chr(34) & strItem.Split(Chr(34) & " As")(4).Remove(0, 4).Trim & Chr(34))
                    'HashIndexAdded.Add(SetColumnName(strItem.Split(Chr(34) & " As")(3), colnameflag).Replace("I", ""), _
                    '                    strItem.Split(Chr(34) & " As")(4).Remove(0, 4).Trim)
                Else
                    sql.Append(strItem.Split(",")(0).Replace(Chr(34), "").Split(".")(0))
                    sql.Append("." & SetColumnName(strItem.Split(",")(0).Replace(Chr(34), "").Split(".")(1), colnameflag))
                    sql.Append(" As ")
                    sql.Append(Chr(34) & getField(strItem.Split(".")(1), isZamba) & Chr(34))
                    'HashIndexAdded.Add(getField(strItem.Split(".")(1), isZamba), getField(strItem.Split(".")(1), isZamba))
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

            If Not IsNothing(strAux) AndAlso strAux <> "" Then
                bolFirst = True
                Dim primarytablename As String = ""
                While strAux <> ""
                    Dim strItem As String = strAux.Split(",")(0)
                    If Not strItem.Contains("<>") Then
                        bolcont = True
                        If bolFirst = True Then
                            primarytablename = strItem.Split(".")(0).Replace(Chr(34), "")
                            sql.Append(primarytablename & " ")
                        End If
                        bolFirst = False
                        'If not already exists on the query then make the join sentence else add condition
                        If ExistsRelation(strItem, "=") = False Then
                            sql.Append(" INNER JOIN ")
                            sql.Append(IIf(primarytablename = strItem.Split("=")(1).Split(".")(0).Replace(Chr(34), ""), strItem.Split("=")(0).Split(".")(0).Replace(Chr(34), ""), strItem.Split("=")(1).Split(".")(0).Replace(Chr(34), "")).ToString.Replace(Chr(34), ""))
                            sql.Append(" ON ")
                            If strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries).Length = 7 Then
                                sql.Append(strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(0) & strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(1) & SetColumnName(strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(2), colnameflag) & strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(3) & strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(4) & strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(5) & SetColumnName(strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(6), colnameflag))
                            Else
                                sql.Append(strItem.Replace(Chr(34), ""))
                            End If
                        Else
                            sql.Append(" And ")
                            If strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries).Length = 7 Then
                                sql.Append(strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(0) & strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(1) & SetColumnName(strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(2), colnameflag) & strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(3) & strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(4) & strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(5) & SetColumnName(strItem.Split(New Char() {Chr(34)}, StringSplitOptions.RemoveEmptyEntries)(6), colnameflag))
                            Else
                                sql.Append(strItem.Replace(Chr(34), ""))
                            End If
                        End If
                    End If

                    'The index it's in the query so erase it
                    strAux = strAux.Remove(0, strAux.Split(",")(0).Length)
                    If strAux.Length > 0 Then
                        strAux = strAux.Remove(0, 1)
                    End If
                End While
            Else
                Dim tablas As String = GetTables(id)
                sql.Append(tablas.Replace("[Distinct]", String.Empty).Replace(Chr(34), ""))
            End If

            'Make the where sentence
            strAux = GetRelations(id)
            If bolcont = False And strAux <> "" Then
                sql.Append(strAux.Split(Char.Parse(","))(0).Split(".")(0))
            End If

            If Not IsNothing(strAux) AndAlso strAux <> "" Then
                retArray.Clear()
                bolFirst = True
                While strAux <> ""
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
                            Dim tablename As String = strItem.Split("<>")(1).Split(".")(0).Replace(">", "").Replace(Chr(34), "")
                            sql.Append(strItem.Split("<>")(1).Split(".")(0).Replace(">", "") & " Where " & strItem.Split("<>")(1).Split(".")(1) & " is not null")

                            'Get the conditions(where)
                            Dim strAux1 As String = GetConditions(id)
                            If strAux1 <> "" Then
                                While strAux1 <> ""
                                    Dim strItem1 As String = strAux1.Split(",")(0)
                                    Dim tableIndex1 As String = strItem1.Split(Chr(34) & " As")(1)
                                    If tablename = tableIndex1 Then
                                        Dim conditions1 As String = strItem1.Split(".")(1).Split("|")(1)
                                        values = strItem1.Split(".")(1).Split("|")(2)
                                        If values = "" Then
                                            Dim tableName1 As String = GetTable(strItem1.Split(Chr(34) & " As")(1), isZamba)
                                            Dim indexName1 As String = getField(strItem1.Split(Chr(34) & " As")(3), isZamba)

                                            'Ask for the value to filter
                                            'Dim question As New ConditionBox(tablename & "." & indexName1 & " " & conditions1)
                                            'question.ShowDialog()
                                            'If question.bolOk Then
                                            '    values = question.value
                                            'Else
                                            '    'If cancel, then stop the query
                                            '    Return Nothing
                                            'End If
                                        End If
                                        'Make the where sentence
                                        If values <> "" Then
                                            If sql.ToString.Contains("where") Then
                                                'If it has @ then it's part of an or clausule
                                                If strItem1.EndsWith("@") Then
                                                    sql.Append(" or ")
                                                    strItem1 = strItem1.Replace("@", "")
                                                    values = values.Replace("@", "")
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
                                                If values <> "" Then
                                                    sql.Append(Server.Con.ConvertDate(values))
                                                End If
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
            Dim value As String = cond
            If value <> "" Then
                While value <> ""
                    Dim strItem As String = value.Split(",")(0)
                    Dim tableIndex As String = strItem.Split(Chr(34) & " As")(1)
                    Dim bolExists As Boolean = False
                    For Each tables As String() In retArray
                        If tables(1).Replace(Chr(34), "") = tableIndex Then
                            bolExists = True
                        End If
                    Next

                    If bolExists = False Then
                        Dim conditions As String = strItem.Split(".")(1).Split("|")(1)
                        values = strItem.Split(".")(1).Split("|")(2)
                        If values = "" Then
                            Dim tableName As String = GetTable(tableIndex.Split(".")(0), isZamba)
                            Dim indexName As String = getField(strItem.Split(Chr(34) & " As")(3), isZamba)
                            Dim preg As String
                            If strItem.Split("|")(0).Split(New String() {"&Pregunta&"}, StringSplitOptions.RemoveEmptyEntries).Length > 1 Then
                                preg = strItem.Split("|")(0).Split(New String() {"&Pregunta&"}, StringSplitOptions.RemoveEmptyEntries)(1)
                                strItem = strItem.Replace(strItem.Split("|")(0).Split(New String() {"&Pregunta&"}, StringSplitOptions.RemoveEmptyEntries)(1), "").Replace("&Pregunta&", "")
                            Else
                                preg = tableName & "." & indexName & " " & conditions
                            End If
                            'Dim question As New ConditionBox(preg)
                            'question.ShowDialog()
                            'If question.bolOk Then
                            '    values = question.value
                            'Else
                            '    Return Nothing
                            'End If
                        End If
                        If values <> "" Then
                            If sql.ToString.Contains("where") Then
                                If strItem.EndsWith("@1") Then
                                    sql.Append(" And (")
                                    strItem = strItem.Replace("@1", "")
                                    values = values.Replace("@1", "")
                                ElseIf strItem.EndsWith("@2") Then
                                    sql.Append(" or ")
                                    strItem = strItem.Replace("@2", "")
                                    values = values.Replace("@2", "")
                                    bolOr = True
                                Else
                                    sql.Append(" And ")
                                End If
                            Else
                                sql.Append(" where ")
                            End If
                            sql.Append(tableIndex & "." & SetColumnName(strItem.Split(Chr(34) & " As")(3), colnameflag))
                            sql.Append(conditions)
                            If values.Contains("\") Or values.Contains("/") AndAlso IsDate(values) Then
                                sql.Append(Server.Con.ConvertDate(values))
                            ElseIf values.Contains("Null") Then
                                sql.Append(" " & values & " ")
                            Else
                                sql.Append("'" & values & "'")
                            End If
                            If bolOr = True Then
                                bolOr = False
                                sql.Append(")")
                            End If
                        End If
                    End If
                    value = value.Remove(0, value.Split(",")(0).Length)
                    If value.Length > 0 Then
                        value = value.Remove(0, 1)
                    End If
                End While
            End If


            ''Agrega el Agrupamiento(Group)
            'value = GetGroupExpression(id)
            'If Not String.IsNullOrEmpty(value) AndAlso Not String.Compare(value.ToUpper, "SIN AGRUPAMIENTO") = 0 Then
            '    sql.Append(" GROUP BY ")
            '    sql.Append(value.Replace(Chr(34), ""))
            '    '    Dim dttable As String = "DOC_I" & DocTypesBusiness.GetDocTypeIdByName(value.Split(Chr(34))(1))
            '    '    Dim indextable As String = "I" & IndexsBusiness.GetIndexId(value.Split(Chr(34))(3))
            '    '    sql.Append(Chr(34) & dttable & Chr(34) & "." & Chr(34) & indextable & Chr(34))
            'End If

            'Agrega el Orden(Sort)
            value = SortEx
            If Not String.IsNullOrEmpty(value) Then
                sql.Append(" ORDER BY ")
                sql.Append(SortEx.Replace(Chr(34), ""))

                'For Each Item As String In value.Split("#")
                '    Dim dttable As String = GetTable(Item.Split(Chr(34))(1), isZamba)
                '    Dim indexName As String = getField(Item.Split(Chr(34))(3), isZamba)
                '    'Dim dttable As String = Chr(34) & "DOC_I" & DocTypesBusiness.GetDocTypeIdByName(Item.Split(Chr(34))(1)) & Chr(34)
                '    'Dim indextable As String = Chr(34) & "I" & IndexsBusiness.GetIndexId(Item.Split(Chr(34))(3)) & Chr(34)
                '    Dim sortExpression As String = dttable & "." & SetColumnName(indexName, colnameflag)
                '    Select Case Item.Split(Chr(34))(4).ToUpper
                '        Case "|ASCENDENTE"
                '            sortExpression += " ASC"
                '        Case "|DESCENDENTE"
                '            sortExpression += " DESC"
                '    End Select
                '    sql.Append(" " & sortExpression & ",")
                'Next
                'sql.Remove(sql.Length - 1, 1)
            End If

            'Save the query in a txt for debugging
            Dim r As New IO.StreamWriter(Membership.MembershipHelper.StartUpPath & "\Query.txt")
            r.WriteLine(sql.ToString)
            r.Close()

            Return sql.ToString()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return ""
        End Try
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



    ''' <summary>
    ''' Generate the query and return a dataset with the data
    ''' </summary>
    ''' <param name="id">Query ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateWebQueryBuilder(ByVal id As Int32, ByVal isZamba As Boolean, ByVal completedConditions As Hashtable) As String
        Try
            Dim sql As New System.Text.StringBuilder()
            sql.Append("Select ")
            If GetDistinct(id) = True Then
                sql.Append("Distinct ")
            End If
            Dim values As String = GetFields(id)
            Dim bolFirst As Boolean = True
            While values <> ""
                Dim strItem As String = values.Split(",")(0)
                If bolFirst = False Then sql.Append(",")
                bolFirst = False
                'If contains as then use it, else if it is a doc_type index get the alias
                If strItem.Contains(" As ") Then
                    sql.Append(Chr(34) & strItem.Split(Chr(34) & " As")(1) & Chr(34))
                    sql.Append("." & Chr(34) & strItem.Split(Chr(34) & " As")(3) & Chr(34))
                    sql.Append(" As ")
                    sql.Append(Chr(34) & strItem.Split(Chr(34) & " As")(4).Remove(0, 4).Trim & Chr(34))
                Else
                    sql.Append(strItem.Split(",")(0))
                    sql.Append(" As ")
                    sql.Append(Chr(34) & getField(strItem.Split(".")(1), isZamba) & Chr(34))
                End If

                'The index it's in the query so erase it
                values = values.Remove(0, strItem.Split(",")(0).Length)
                If values.Length > 0 Then
                    values = values.Remove(0, 1)
                End If
            End While
            sql.Append(" from ")

            'Make the Join Sentece
            Dim strAux As String = GetRelations(id)
            Dim bolcont As Boolean = False
            If Not IsNothing(strAux) AndAlso strAux <> "" Then
                bolFirst = True
                While strAux <> ""
                    Dim strItem As String = strAux.Split(",")(0)
                    If Not strItem.Contains("<>") Then
                        bolcont = True
                        If bolFirst = True Then
                            sql.Append(strItem.Split(".")(0) & " ")
                        End If
                        bolFirst = False
                        'If not already exists on the query then make the join sentence else add condition
                        If ExistsRelation(strItem, "=") = False Then
                            sql.Append(" INNER JOIN ")
                            sql.Append(strItem.Split("=")(1).Split(".")(0))
                            sql.Append(" ON ")
                            sql.Append(strItem)
                        Else
                            sql.Append(" And ")
                            sql.Append(strItem)
                        End If
                    End If

                    'The index it's in the query so erase it
                    strAux = strAux.Remove(0, strAux.Split(",")(0).Length)
                    If strAux.Length > 0 Then
                        strAux = strAux.Remove(0, 1)
                    End If
                End While
            Else
                Dim tablas As String = GetTables(id)
                sql.Append(tablas.Replace("[Distinct]", String.Empty))
            End If

            'Make the where sentence
            strAux = GetRelations(id)
            If bolcont = False And strAux <> "" Then
                sql.Append(strAux.Split(Char.Parse(","))(0).Split(".")(0))
            End If

            If Not IsNothing(strAux) AndAlso strAux <> "" Then
                retArray.Clear()
                bolFirst = True
                While strAux <> ""
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
                            Dim tablename As String = strItem.Split("<>")(1).Split(".")(0).Replace(">", "").Replace(Chr(34), "")
                            sql.Append(strItem.Split("<>")(1).Split(".")(0).Replace(">", "") & " Where " & strItem.Split("<>")(1).Split(".")(1) & " is not null")

                            'Get the conditions(where)
                            Dim strAux1 As String = GetConditions(id)
                            If strAux1 <> "" Then
                                While strAux1 <> ""
                                    Dim strItem1 As String = strAux1.Split(",")(0)
                                    Dim tableIndex1 As String = strItem1.Split(Chr(34) & " As")(1)
                                    If tablename = tableIndex1 Then
                                        Dim conditions1 As String = strItem1.Split(".")(1).Split("|")(1)
                                        values = strItem1.Split(".")(1).Split("|")(2)
                                        If values = "" Then
                                            Dim tableName1 As String = GetTable(strItem1.Split(Chr(34) & " As")(1), isZamba)
                                            Dim indexName1 As String = getField(strItem1.Split(Chr(34) & " As")(3), isZamba)

                                            'Ask for the value to filter
                                            'Dim question As New ConditionBox(tablename & "." & indexName1 & " " & conditions1)
                                            'question.ShowDialog()
                                            'If question.bolOk Then
                                            '    values = question.value
                                            'Else
                                            '    'If cancel, then stop the query
                                            '    Return Nothing
                                            'End If
                                        End If
                                        'Make the where sentence
                                        If values <> "" Then
                                            If sql.ToString.Contains("where") Then
                                                'If it has @ then it's part of an or clausule
                                                If strItem1.EndsWith("@") Then
                                                    sql.Append(" or ")
                                                    strItem1 = strItem1.Replace("@", "")
                                                    values = values.Replace("@", "")
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
                                                If values <> "" Then
                                                    sql.Append(Server.Con.ConvertDate(values))
                                                End If
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
            Dim value As String = GetConditions(id)
            If value <> "" Then
                While value <> ""
                    Dim strItem As String = value.Split(",")(0)
                    Dim tableIndex As String = strItem.Split(Chr(34) & " As")(1)
                    Dim bolExists As Boolean = False
                    For Each tables As String() In retArray
                        If tables(1).Replace(Chr(34), "") = tableIndex Then
                            bolExists = True
                        End If
                    Next

                    If bolExists = False Then
                        Dim conditions As String = strItem.Split(".")(1).Split("|")(1)
                        values = strItem.Split(".")(1).Split("|")(2)
                        If values = "" Then
                            If completedConditions.Contains(strItem.Split(Chr(34) & " As")(3)) Then
                                values = completedConditions(strItem.Split(Chr(34) & " As")(3))
                            End If
                        End If
                        If values <> "" Then
                            If sql.ToString.Contains("where") Then
                                If strItem.EndsWith("@1") Then
                                    sql.Append(" And (")
                                    strItem = strItem.Replace("@1", "")
                                    values = values.Replace("@1", "")
                                ElseIf strItem.EndsWith("@2") Then
                                    sql.Append(" or ")
                                    strItem = strItem.Replace("@2", "")
                                    values = values.Replace("@2", "")
                                    bolOr = True
                                Else
                                    sql.Append(" And ")
                                End If
                            Else
                                sql.Append(" where ")
                            End If
                            sql.Append(Chr(34) & tableIndex & Chr(34) & "." & Chr(34) & strItem.Split(Chr(34) & " As")(3) & Chr(34))
                            sql.Append(conditions)
                            If values.Contains("\") Or values.Contains("/") AndAlso IsDate(values) Then
                                sql.Append(Server.Con.ConvertDate(values))
                            ElseIf values.Contains("Null") Then
                                sql.Append(" " & values & " ")
                            Else
                                sql.Append("'" & values & "'")
                            End If
                            If bolOr = True Then
                                bolOr = False
                                sql.Append(")")
                            End If
                        End If
                    End If
                    value = value.Remove(0, value.Split(",")(0).Length)
                    If value.Length > 0 Then
                        value = value.Remove(0, 1)
                    End If
                End While
            End If




            'Agrega el Orden(Sort)
            value = GetSortExpression(id)
            If Not String.IsNullOrEmpty(value) Then
                sql.Append(" ORDER BY ")
                Dim DTB As New DocTypesBusiness
                For Each Item As String In value.Split("#")
                    Dim dttable As String = Chr(34) & "DOC_I" & DTB.GetDocTypeIdByName(Item.Split(Chr(34))(1)) & Chr(34)
                    Dim indextable As String = Chr(34) & "I" & IndexsBusiness.GetIndexIdByName(Item.Split(Chr(34))(3)) & Chr(34)
                    Dim sortExpression As String = dttable & "." & indextable
                    Select Case Item.Split(Chr(34))(4).ToUpper
                        Case "|ASCENDENTE"
                            sortExpression += " ASC"
                        Case "|DESCENDENTE"
                            sortExpression += " DESC"
                    End Select
                    sql.Append(" " & sortExpression & ",")
                Next
                DTB = Nothing
                sql.Remove(sql.Length - 1, 1)
            End If

            'Save the query in a txt for debugging
            Dim r As New IO.StreamWriter(Membership.MembershipHelper.StartUpPath & "\Query.txt")
            r.WriteLine(sql.ToString)
            r.Close()

            Return sql.ToString()
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return ""
        End Try
    End Function
    'Gets the alias of the table
    Private Function GetTable(ByVal table As String, ByVal isZamba As Boolean) As String
        Dim DTB As New DocTypesBusiness
        Try
            table = table.Replace(Chr(34), "")
            If isZamba = True Then
                If table.Contains("DOC_I") Then
                    If IsNumeric(table.Replace("DOC_I", "")) Then
                        table = DTB.GetDocTypeName(table.Replace("DOC_I", ""))
                    End If
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            DTB = Nothing
        End Try
        Return table
    End Function
    'Gets the alias of the field
    Private Function getField(ByVal field As String, ByVal isZamba As Boolean) As String
        Try
            field = field.Replace(Chr(34), "")
            If isZamba = True Then
                If IsNumeric(field.Replace("I", "")) Then
                    Dim name As String = FuncionesZamba.GetIndexName(field.Replace("I", ""))
                    If name <> "" Then
                        HashIndexAdded.Add(field.Replace("I", ""), name.Trim())
                        Return name.Trim()
                    Else


                    End If
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return field
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
            table(1) = item.Split(comp)(1).Split(".")(0).Replace(">", "")

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
            Return strselect
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
            Return strselect
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
            Return strselect
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
                Return strselect
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
                Return strselect
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
    Public Function GetQueryIdsAndNames(ByVal userid As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim strselect As StringBuilder = New StringBuilder()
            'strselect.Append("select ID,NAME from reportbuilder")
            strselect.Append("select ID,NAME from reportbuilder inner join Zvw_USR_Rights_200 on Zvw_USR_Rights_200.Aditional = reportbuilder.id where objectid = ")
            strselect.Append(ObjectTypes.ModuleReports)
            strselect.Append(" and USER_ID= ")
            strselect.Append(userid)
            strselect.Append(" and RIGHT_TYPE= ")
            strselect.Append(RightsType.View)
            strselect.Append(" ORDER BY NAME")
            ds = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString().ToUpper())
            Return ds
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return ds
        End Try
    End Function
    Public Function GetQueryIdsAndNamesReportGeneral(ByVal userid As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim strselect As StringBuilder = New StringBuilder()
            'strselect.Append("select ID,NAME from reporte_general")
            strselect.Append("select ID,NAME from reporte_general inner join Zvw_USR_Rights_200 on Zvw_USR_Rights_200.Aditional = reporte_general.id where objectid = ")
            strselect.Append(ObjectTypes.ModuleReports)
            strselect.Append(" and USER_ID= ")
            strselect.Append(userid)
            strselect.Append(" and RIGHT_TYPE= ")
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
            strselect.Append("select ID,NAME from reportbuilder")
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


    Public Sub New()
        HashIndexAdded = New Hashtable
    End Sub
End Class
