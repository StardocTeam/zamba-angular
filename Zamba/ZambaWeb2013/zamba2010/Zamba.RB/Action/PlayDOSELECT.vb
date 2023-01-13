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
        Me._myRule = rule
        Me.Ds = New DataSet()
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Trace.WriteLineIf(ZTrace.IsInfo, "Cantidad de results a ejecutar: " & results.Count)

            'Si no hay tareas, la ejecuto una sola vez, sino, una vez por cada tarea
            If results.Count = 0 Then
                executeQuery(Nothing)
            Else
                For i As Int64 = 0 To results.Count - 1
                    executeQuery(results(i))
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

        Return WFRuleParent.ReconocerZvar(Me._myRule.SQL)

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
        Me.strselect = Me.strselect.Replace(Chr(13), String.Empty)
        Me.strselect = Me.strselect.Replace(Chr(10), String.Empty)


        Me.strselect = WFRuleParent.ReconocerVariables(Me.strselect)
        If Not IsNothing(result) Then
            Me.strselect = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.strselect, result)
        End If

        While Not String.IsNullOrEmpty(Me.strselect)
            Me.strline = Me.strselect.Split("§")(0)
            Me.strselect = Me.strselect.Replace(Me.strline, String.Empty)

            If Me.strselect.StartsWith("§") Then
                Me.strselect = Me.strselect.Remove(0, 1)
            End If

            Trace.WriteLineIf(ZTrace.IsInfo, "Sentencia: " & Me.strline)

            If Me._myRule.ExecuteType = "ESCALAR" Then
                ExecuteScalar(Me.strline, result, Me.strselect)
            ElseIf Me._myRule.ExecuteType = "DATASET" Then
                ExecuteDataset(Me.strline)
            Else 'NONQUERY
                ExecuteNonQuery(Me.strline)
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
        If strquery.ToLower.Trim.StartsWith("exec ") AndAlso ServersBusiness.GetServerType = DBTypes.OracleClient Then
            params = ServersBusiness.GetStoreParams(strquery)
            If String.IsNullOrEmpty(Me._myRule.Dbname) Then
                ServersBusiness.BuildExecuteNonQuery(params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
            Else
                Select Case Me._myRule.Servertype
                    Case 0 'MSSQLServer7Up
                        ServersBusiness.BuildExecuteNonQuery(DBTypes.MSSQLServer7Up, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 1 'MSSQLServer
                        ServersBusiness.BuildExecuteNonQuery(DBTypes.MSSQLServer, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 2 'Oracle
                        ServersBusiness.BuildExecuteNonQuery(DBTypes.Oracle, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 3 'Oracle9
                        ServersBusiness.BuildExecuteNonQuery(DBTypes.Oracle9, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 4 'SyBase
                        ServersBusiness.BuildExecuteNonQuery(DBTypes.SyBase, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 5 'OracleClient
                        ServersBusiness.BuildExecuteNonQuery(DBTypes.OracleClient, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 6 'ODBC
                        ServersBusiness.BuildExecuteNonQuery(DBTypes.ODBC, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                End Select
            End If
        Else
            If String.IsNullOrEmpty(Me._myRule.Dbname) Then
                ServersBusiness.BuildExecuteNonQuery(CommandType.Text, strquery)
            Else
                Select Case Me._myRule.Servertype
                    Case 0 'MSSQLServer7Up
                        ServersBusiness.BuildExecuteNonQuery(DBTypes.MSSQLServer7Up, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 1 'MSSQLServer
                        ServersBusiness.BuildExecuteNonQuery(DBTypes.MSSQLServer, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 2 'Oracle
                        ServersBusiness.BuildExecuteNonQuery(DBTypes.Oracle, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 3 'Oracle9
                        ServersBusiness.BuildExecuteNonQuery(DBTypes.Oracle9, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 4 'SyBase
                        ServersBusiness.BuildExecuteNonQuery(DBTypes.SyBase, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 5 'OracleClient
                        ServersBusiness.BuildExecuteNonQuery(DBTypes.OracleClient, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 6 'ODBC
                        ServersBusiness.BuildExecuteNonQuery(DBTypes.ODBC, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                End Select
            End If
        End If
    End Sub

    Private Sub ExecuteDataset(ByVal strquery As String)
        Me.Ds = Nothing

        If strquery.ToLower.Trim.StartsWith("exec ") AndAlso ServersBusiness.GetServerType = DBTypes.OracleClient Then
            params = ServersBusiness.GetStoreParams(strquery)
            If String.IsNullOrEmpty(Me._myRule.Dbname) Then
                Me.Ds = ServersBusiness.BuildExecuteDataSet(params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
            Else
                Select Case Me._myRule.Servertype
                    Case 0 'MSSQLServer7Up
                        Me.Ds = ServersBusiness.BuildExecuteDataSet(DBTypes.MSSQLServer7Up, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 1 'MSSQLServer
                        Me.Ds = ServersBusiness.BuildExecuteDataSet(DBTypes.MSSQLServer, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 2 'Oracle
                        Me.Ds = ServersBusiness.BuildExecuteDataSet(DBTypes.Oracle, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 3 'Oracle9
                        Me.Ds = ServersBusiness.BuildExecuteDataSet(DBTypes.Oracle9, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 4 'SyBase
                        Me.Ds = ServersBusiness.BuildExecuteDataSet(DBTypes.SyBase, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 5 'OracleClient
                        Me.Ds = ServersBusiness.BuildExecuteDataSet(DBTypes.OracleClient, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 6 'ODBC
                        Me.Ds = ServersBusiness.BuildExecuteDataSet(DBTypes.ODBC, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                End Select
            End If
        Else
            If String.IsNullOrEmpty(Me._myRule.Dbname) Then
                Me.Ds = ServersBusiness.BuildExecuteDataSet(CommandType.Text, strquery)
            Else
                Select Case Me._myRule.Servertype
                    Case 0 'MSSQLServer7Up
                        Me.Ds = ServersBusiness.BuildExecuteDataSet(DBTypes.MSSQLServer7Up, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 1 'MSSQLServer
                        Me.Ds = ServersBusiness.BuildExecuteDataSet(DBTypes.MSSQLServer, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 2 'Oracle
                        Me.Ds = ServersBusiness.BuildExecuteDataSet(DBTypes.Oracle, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 3 'Oracle9
                        Me.Ds = ServersBusiness.BuildExecuteDataSet(DBTypes.Oracle9, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 4 'SyBase
                        Me.Ds = ServersBusiness.BuildExecuteDataSet(DBTypes.SyBase, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 5 'OracleClient
                        Me.Ds = ServersBusiness.BuildExecuteDataSet(DBTypes.OracleClient, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 6 'ODBC
                        Me.Ds = ServersBusiness.BuildExecuteDataSet(DBTypes.ODBC, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                End Select
            End If
        End If

        Trace.WriteLineIf((ZTrace.IsInfo) AndAlso (Not IsNothing(Me.Ds)), "Cantidad de filas obtenidas: " & Me.Ds.Tables(0).Rows.Count)

        If Not IsNothing(Me.Ds) AndAlso Not IsNothing(Me.Ds.Tables(0)) Then
            If VariablesInterReglas.ContainsKey(Me._myRule.HashTable & ".Count") = False Then
                VariablesInterReglas.Add(Me._myRule.HashTable & ".Count", Me.Ds.Tables(0).Rows.Count, False)
            Else
                VariablesInterReglas.Item(Me._myRule.HashTable & ".Count") = Me.Ds.Tables(0).Rows.Count
            End If
        End If

        If VariablesInterReglas.ContainsKey(Me._myRule.HashTable) = False Then
            VariablesInterReglas.Add(Me._myRule.HashTable, Me.Ds, False)
        Else
            VariablesInterReglas.Item(Me._myRule.HashTable) = Me.Ds
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
        Me.resultado = String.Empty
        Me.nombreVar = String.Empty

        If strquery.StartsWith("@") Then
            Me.nombreVar = strquery.Split("=")(0).Trim()
            strquery = strquery.Remove(0, strquery.Split("=")(0).Length() + 1).Trim()
        End If

        If strquery.ToLower.Trim.StartsWith("exec ") AndAlso ServersBusiness.GetServerType = DBTypes.OracleClient Then
            params = ServersBusiness.GetStoreParams(strquery)
            If String.IsNullOrEmpty(Me._myRule.Dbname) Then
                tempResultado = ServersBusiness.BuildExecuteScalar(params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
            Else
                Select Case Me._myRule.Servertype
                    Case 0 'MSSQLServer7Up
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTypes.MSSQLServer7Up, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 1 'MSSQLServer
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTypes.MSSQLServer, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 2 'Oracle
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTypes.Oracle, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 3 'Oracle9
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTypes.Oracle9, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 4 'SyBase
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTypes.SyBase, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 5 'OracleClient
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTypes.OracleClient, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
                    Case 6 'ODBC
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTypes.ODBC, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                End Select
            End If
        Else
            If String.IsNullOrEmpty(Me._myRule.Dbname) Then
                tempResultado = ServersBusiness.BuildExecuteScalar(CommandType.Text, strquery)
            Else
                Select Case Me._myRule.Servertype
                    Case 0 'MSSQLServer7Up
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTypes.MSSQLServer7Up, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 1 'MSSQLServer
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTypes.MSSQLServer, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 2 'Oracle
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTypes.Oracle, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 3 'Oracle9
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTypes.Oracle9, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 4 'SyBase
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTypes.SyBase, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 5 'OracleClient
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTypes.OracleClient, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                    Case 6 'ODBC
                        tempResultado = ServersBusiness.BuildExecuteScalar(DBTypes.ODBC, Me._myRule.Dbname, Me._myRule.Dbpassword, Me._myRule.Dbuser, Me._myRule.Servidor, CommandType.Text, strquery)
                End Select
            End If
        End If

        Dim isByteArray As Boolean = False
        If TypeOf (tempResultado) Is DBNull Then
            'Para el caso de DBNull. En caso de que esto también falle se lanzaría la exception.
            Me.resultado = String.Empty
        ElseIf TypeOf (tempResultado) Is Byte() Then
            'TODO: Ver de agregar una opcion para convertirlo a base 64 en caso de ser necesario
            'Me.resultado = Convert.ToBase64String(tempResultado)
            Me.resultado = tempResultado
            isByteArray = True
        Else
            Me.resultado = tempResultado
        End If
        tempResultado = Nothing

        If Not isByteArray Then TextoInteligente.AsignItemFromSmartText(Me._myRule.HashTable, r, Me.resultado)

        'No se imprime el array de bytes porque en caso de ser un archivo pesado, estaría escribiendo todo el archivo en el trace.
        'Esto bajaría la performance e incrementaría el espacio en disco considerablemente.

        If String.IsNullOrEmpty(Me.nombreVar) = False Then
            'No se imprime el array de bytes porque en caso de ser un archivo pesado, estaría escribiendo todo el archivo en el trace.
            'Esto bajaría la performance e incrementaría el espacio en disco considerablemente.
            If isByteArray Then
                Trace.WriteLineIf(ZTrace.IsInfo, "Reemplazando variable: " & Me.nombreVar & " valor: Un array de bytes")
            Else
                If Not IsNothing(Me.resultado) Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "Reemplazando variable: " & Me.nombreVar & " valor: " & Me.resultado.ToString)
                Else
                    Trace.WriteLineIf(ZTrace.IsInfo, "Resultado vacio")
                End If
            End If

            fullSelect = fullSelect.Replace(Me.nombreVar, Me.resultado)
        Else
            Me._myRule.HashTable = Me._myRule.HashTable.Trim()

            If VariablesInterReglas.ContainsKey(Me._myRule.HashTable) = False Then
                VariablesInterReglas.Add(Me._myRule.HashTable, Me.resultado, False)
            Else
                VariablesInterReglas.Item(Me._myRule.HashTable) = Me.resultado
            End If
        End If
    End Sub
End Class
