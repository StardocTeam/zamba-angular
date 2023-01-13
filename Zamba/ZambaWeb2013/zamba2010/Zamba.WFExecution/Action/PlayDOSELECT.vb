Imports System.Text
Imports Zamba.Core

Public Class PlayDOSELECT
    Private _myRule As IDOSELECT
    Private strselect As String
    Private strline As String
    Private resultado As Object
    Private nombreVar As String
    Private Ds As DataSet
    Private params As List(Of Object)
    Private tempResultado As Object

    Sub New(ByVal rule As IDOSELECT)
        _myRule = rule
        Ds = New DataSet()
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Cantidad de results a ejecutar: " & results.Count)
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Tipo de ejecucion: " & _myRule.ExecuteType)

            'Si no hay tareas, la ejecuto una sola vez, sino, una vez por cada tarea
            If results.Count = 0 Then
                executeQuery(Nothing)
            Else
                For Each r As TaskResult In results
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                    executeQuery(r)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Consulta ejecutada con éxito.")
                Next
            End If

        Finally
            strselect = Nothing
            strline = Nothing
            resultado = Nothing
            nombreVar = Nothing
            Ds = Nothing
            tempResultado = Nothing
            If Not IsNothing(params) Then
                For i As Integer = 0 To params.Count - 1
                    params(i) = Nothing
                Next
                params.Clear()
                params = Nothing
            End If
        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean



    End Function


    Function DiscoverParams() As List(Of String)

        Return WFRuleParent.ReconocerZvar(_myRule.SQL)

    End Function

    ''' <summary>
    ''' Metodo que se encarga de la ejecucion de la llamada a la base de datos
    ''' </summary>
    ''' <param name="myRule"></param>
    ''' <param name="result"></param>
    ''' <remarks></remarks>
    Private Sub executeQuery(ByVal result As TaskResult)
        Me.strselect = Me._myRule.SQL
        Me.strselect = Me.strselect.Replace("&lt;", "<")
        Me.strselect = Me.strselect.Replace("&gt;", ">")
        Me.strselect = Me.strselect.Replace("&lt", "<")
        Me.strselect = Me.strselect.Replace("&gt", ">")
        Me.strselect = Me.strselect.Replace(Chr(13), "")
        Me.strselect = Me.strselect.Replace(Chr(10), "")

        Dim VarInterReglas As New VariablesInterReglas()
        Me.strselect = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me.strselect)
        Me.strselect = VarInterReglas.ReconocerVariables(Me.strselect)
        VarInterReglas = Nothing

        strselect = strselect.Replace(">>,", ">>§,").Replace(">><<,", ">>§<<")

        If IsNothing(result) AndAlso strselect.ToLower.Contains("<<tarea>>") Then
        Else
            If strselect.Contains("§") Then
                Dim newstrselect As New StringBuilder
                For Each CurrentWord As String In strselect.Split("§")
                    newstrselect.Append(TextoInteligente.ReconocerCodigo(CurrentWord, result).Trim)
                Next
                strselect = newstrselect.ToString
            Else
                strselect = TextoInteligente.ReconocerCodigo(strselect, result)
            End If
        End If

        If (strselect.ToUpper().Contains("EXEC")) Then
            While strselect.Contains(",,")
                strselect = strselect.Replace(",,", ",null,")
            End While

        End If

        While Not String.IsNullOrEmpty(strselect)
            strline = strselect.Split("§")(0)
            strselect = strselect.Replace(strline, String.Empty)

            If strselect.Contains(System.Environment.NewLine) Then
                strselect = strselect.Replace(System.Environment.NewLine, "")
            End If

            If strselect.StartsWith("§") Then
                strselect = strselect.Remove(0, 1)
            End If


            ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia: " & strline)

            If _myRule.ExecuteType = "ESCALAR" Then
                ExecuteScalar(strline, result, strselect)
            ElseIf _myRule.ExecuteType = "DATASET" Then
                ExecuteDataset(strline)
            Else 'NONQUERY
                ExecuteNonQuery(strline)
            End If
        End While
    End Sub

    ''' <summary>
    ''' Ejecuta una consulta q no devuelve valores
    ''' </summary>
    ''' <param name="myRule"></param>
    ''' <param name="strselect"></param>
    ''' <remarks></remarks>
    Private Sub ExecuteNonQuery(ByVal strquery As String)

        If strquery.ToLower.Trim.StartsWith("exec ") AndAlso Servers.Server.isOracle Then

            'Este metodo solo debe devolver un array con los valores de los parametros.
            params = ServersBusiness.GetStoreParamsValues(strquery)

            If String.IsNullOrEmpty(_myRule.Dbname) Then
                ServersBusiness.BuildExecuteNonQuery(params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object()), DirectCast(params(3), Object()))
            Else
                Select Case _myRule.Servertype
                    Case 0 'MSSQLServer7Up
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.MSSQLServer7Up, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 1 'MSSQLServer
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.MSSQLServer, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 2 'Oracle
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.Oracle, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 3 'Oracle9
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.Oracle9, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 4 'SyBase
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.SyBase, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 5 'OracleClient
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.OracleClient, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 6 'ODBC
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.ODBC, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 7 'MSSLEXPRESS
                        ServersBusiness.BuildExecuteScalar(DBTYPES.MSSQLExpress, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 8 'OracleDirect
                        ServersBusiness.BuildExecuteScalar(DBTYPES.OracleDirect, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 10 'OracleManaged
                        ServersBusiness.BuildExecuteScalar(DBTYPES.OracleManaged, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 11 'OracleODP
                        ServersBusiness.BuildExecuteScalar(DBTYPES.OracleODP, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)

                End Select
            End If
        Else
            If String.IsNullOrEmpty(_myRule.Dbname) Then
                ServersBusiness.BuildExecuteNonQuery(CommandType.Text, strquery)
            Else
                Select Case _myRule.Servertype
                    Case 0 'MSSQLServer7Up
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.MSSQLServer7Up, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 1 'MSSQLServer
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.MSSQLServer, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 2 'Oracle
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.Oracle, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 3 'Oracle9
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.Oracle9, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 4 'SyBase
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.SyBase, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 5 'OracleClient
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.OracleClient, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 6 'ODBC
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.ODBC, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 7 'MSSLEXPRESS
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.MSSQLExpress, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 8 'OracleDirect
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.OracleDirect, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 10 'OracleManaged
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.OracleManaged, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 11 'OracleODP
                        ServersBusiness.BuildExecuteNonQuery(DBTYPES.OracleODP, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                End Select
            End If
        End If
    End Sub

    Private Sub ExecuteDataset(ByVal strquery As String)
        Ds = Nothing
        If strquery.ToLower.Trim.StartsWith("exec ") AndAlso Servers.Server.isOracle Then

            'Este metodo solo debe devolver un array con los valores de los parametros.
            params = ServersBusiness.GetStoreParamsValues(strquery)

            If String.IsNullOrEmpty(_myRule.Dbname) Then
                Ds = ServersBusiness.BuildExecuteDataSet(params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object()), DirectCast(params(3), Object()))
            Else
                Select Case _myRule.Servertype
                    Case 0 'MSSQLServer7Up
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.MSSQLServer7Up, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object()), DirectCast(params(3), Object()))
                    Case 1 'MSSQLServer
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.MSSQLServer, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object()), DirectCast(params(3), Object()))
                    Case 2 'Oracle
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.Oracle, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object()), DirectCast(params(3), Object()))
                    Case 3 'Oracle9
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.Oracle9, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object()), DirectCast(params(3), Object()))
                    Case 4 'SyBase
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.SyBase, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object()), DirectCast(params(3), Object()))
                    Case 5 'OracleClient
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.OracleClient, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object()), DirectCast(params(3), Object()))
                    Case 6 'ODBC
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.ODBC, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object()), DirectCast(params(3), Object()))
                    Case 7 'MSSLEXPRESS
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.MSSQLExpress, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object()), DirectCast(params(3), Object()))
                    Case 8 'OracleDirect
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.OracleDirect, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object()), DirectCast(params(3), Object()))
                    Case 10 'OracleManaged
                        'por aca va a ir la llamada
                        'Le esta esta pasando esos parametros en paramas(1) y el nombre del store en params(0) 
                        ' el paramsnames y paramsvalues que antes lo traia el otro metodo, no vienen mas. solo tenemos el array con 2 elementos 0 y 1
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.OracleManaged, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object()), DirectCast(params(3), Object()))
                    Case 11 'OracleODP
                        Ds = ServersBusiness.BuildExecuteScalar(DBTYPES.OracleODP, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object()), DirectCast(params(3), Object()))

                End Select
            End If

            If Not IsNothing(params) Then
                Dim i As Int32 = 1
                For Each pvalue As Object In params(3)
                    If Not IsDBNull(pvalue) Then
                        If Not VariablesInterReglas.ContainsKey(_myRule.HashTable & ".Param" & i) Then
                            VariablesInterReglas.Add(_myRule.HashTable & ".Param" & i, pvalue.ToString)
                        Else
                            VariablesInterReglas.Item(_myRule.HashTable & ".Param" & i) = pvalue.ToString
                        End If
                    End If
                    i = i + 1
                Next
            End If
        Else
            If String.IsNullOrEmpty(_myRule.Dbname) Then
                Ds = ServersBusiness.BuildExecuteDataSet(CommandType.Text, strquery)
            Else
                Select Case _myRule.Servertype
                    Case 0 'MSSQLServer7Up
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.MSSQLServer7Up, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 1 'MSSQLServer
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.MSSQLServer, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 2 'Oracle
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.Oracle, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 3 'Oracle9
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.Oracle9, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 4 'SyBase
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.SyBase, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 5 'OracleClient
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.OracleClient, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 6 'ODBC
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.ODBC, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 7 'MSSLEXPRESS
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.MSSQLExpress, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 8 'OracleDirect
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.OracleDirect, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 10 'OracleManaged
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.OracleManaged, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 11 'OracleODP
                        Ds = ServersBusiness.BuildExecuteDataSet(DBTYPES.OracleODP, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)

                End Select
            End If
        End If

        If Ds IsNot Nothing AndAlso Not Ds.Tables.Count > 0 Then
            Ds.Tables.Add(New DataTable)
        End If

        If Ds IsNot Nothing Then
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Resultado Dataset Count: " & Ds.Tables(0).Rows.Count)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de filas obtenidas: " & Ds.Tables(0).Rows.Count)
        End If

        If Not IsNothing(Ds) Then
            If Not VariablesInterReglas.ContainsKey(_myRule.HashTable & ".Count") Then
                VariablesInterReglas.Add(_myRule.HashTable & ".Count", Ds.Tables(0).Rows.Count)
            Else
                VariablesInterReglas.Item(_myRule.HashTable & ".Count") = Ds.Tables(0).Rows.Count
            End If
        End If



        If Not VariablesInterReglas.ContainsKey(_myRule.HashTable) Then
            VariablesInterReglas.Add(_myRule.HashTable, Ds)
        Else
            VariablesInterReglas.Item(_myRule.HashTable) = Ds
        End If

        WFRuleParent.GeneratarVariablesDesdeDS(Ds)

    End Sub

    ''' <summary>
    ''' Ejecuta una consulta q devuelve un valor
    ''' </summary>
    ''' <param name="myRule"></param>
    ''' <param name="strselect"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] Modified    05/10/2009  Se modificó la forma en que asignaba el resultado de la consulta.
    '''                                     Antes si el resultado era DBNull arrojaba exception. Ahora si es
    '''                                     DBNull le asigna a Me.resultado un Stirng.Empty para simular ese 
    '''                                     valor vacío de la base de datos.
    ''' </history>
    Private Sub ExecuteScalar(ByVal strquery As String, ByVal r As TaskResult, ByRef fullSelect As String)

        resultado = String.Empty
        nombreVar = String.Empty

        If strquery.StartsWith("@") Then
            nombreVar = strquery.Split("=")(0).Trim()
            strquery = strquery.Remove(0, strquery.Split("=")(0).Length() + 1).Trim()
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Consulta: " & strquery)
        End If

        If strquery.ToLower.Trim.StartsWith("exec ") AndAlso Servers.Server.isOracle Then

            params = ServersBusiness.GetStoreParamsValues(strquery)

            If String.IsNullOrEmpty(_myRule.Dbname) Then
                tempResultado = ServersBusiness.BuildExecuteScalar(params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object()), DirectCast(params(3), Object()))
            Else
                Select Case _myRule.Servertype
                    Case 0 'MSSQLServer7Up
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.MSSQLServer7Up, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 1 'MSSQLServer
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.MSSQLServer, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 2 'Oracle
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.Oracle, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 3 'Oracle9
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.Oracle9, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 4 'SyBase
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.SyBase, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 5 'OracleClient
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.OracleClient, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 6 'ODBC
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.ODBC, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 7 'MSSLEXPRESS
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.MSSQLExpress, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 8 'OracleDirect
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.OracleDirect, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 10 'OracleManaged
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.OracleManaged, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 11 'OracleODP
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.OracleODP, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))

                End Select
            End If
        Else
            If String.IsNullOrEmpty(_myRule.Dbname) Then
                tempResultado = ServersBusiness.BuildExecuteScalar(CommandType.Text, strquery)
            Else
                Select Case _myRule.Servertype
                    Case 0 'MSSQLServer7Up
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.MSSQLServer7Up, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 1 'MSSQLServer
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.MSSQLServer, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 2 'Oracle
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.Oracle, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 3 'Oracle9
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.Oracle9, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 4 'SyBase
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.SyBase, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 5 'OracleClient
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.OracleClient, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 6 'ODBC
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.ODBC, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 7 'MSSLEXPRESS
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.MSSQLExpress, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 8 'OracleDirect
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.OracleDirect, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 10 'OracleManaged
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.OracleManaged, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                    Case 11 'OracleODP
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTYPES.OracleODP, _myRule.Dbname, _myRule.Dbpassword, _myRule.Dbuser, _myRule.Servidor, CommandType.Text, strquery)
                End Select
            End If
        End If

        Dim isByteArray As Boolean = False
        If TypeOf (tempResultado) Is DBNull Then
            'Para el caso de DBNull. En caso de que esto también falle se lanzaría la exception.
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Resultado es DBNull")
            resultado = String.Empty
        ElseIf TypeOf (tempResultado) Is Byte() Then
            'TODO: Ver de agregar una opcion para convertirlo a base 64 en caso de ser necesario
            'Me.resultado = Convert.ToBase64String(tempResultado)
            resultado = tempResultado
            isByteArray = True
        Else
            resultado = tempResultado
        End If
        tempResultado = Nothing

        If Not isByteArray Then TextoInteligente.AsignItemFromSmartText(_myRule.HashTable, r, resultado)

        'No se imprime el array de bytes porque en caso de ser un archivo pesado, estaría escribiendo todo el archivo en el trace.
        'Esto bajaría la performance e incrementaría el espacio en disco considerablemente.
        If isByteArray Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado Escalar: Un array de bytes")
        Else
            If Not IsNothing(Me.resultado) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Reemplazando Escalar: " & Me.resultado.ToString)
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado vacio")
            End If
        End If

        If Not String.IsNullOrEmpty(Me.nombreVar) AndAlso Not isByteArray Then
            'No se imprime el array de bytes porque en caso de ser un archivo pesado, estaría escribiendo todo el archivo en el trace.
            'Esto bajaría la performance e incrementaría el espacio en disco considerablemente.
            If isByteArray Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Reemplazando variable: " & Me.nombreVar & " valor: Un array de bytes")
                fullSelect = fullSelect.Replace(nombreVar, resultado)
            Else
                If Not IsNothing(Me.resultado) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Reemplazando variable: " & Me.nombreVar & " valor: " & Me.resultado.ToString)
                    fullSelect = fullSelect.Replace(nombreVar, resultado)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado vacio")
                End If
            End If

        Else
            If Not IsNothing(Me.resultado) Then
                _myRule.HashTable = _myRule.HashTable.Trim()
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando en variable: " & _myRule.HashTable)

                If VariablesInterReglas.ContainsKey(_myRule.HashTable) = False Then
                    VariablesInterReglas.Add(_myRule.HashTable, resultado)
                Else
                    VariablesInterReglas.Item(_myRule.HashTable) = resultado
                End If
            Else
                _myRule.HashTable = _myRule.HashTable.Trim()
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardando en variable: " & _myRule.HashTable)

                If VariablesInterReglas.ContainsKey(_myRule.HashTable) = False Then
                    VariablesInterReglas.Add(_myRule.HashTable, String.Empty)
                Else
                    VariablesInterReglas.Item(_myRule.HashTable) = String.Empty
                End If

            End If
        End If
    End Sub
End Class


