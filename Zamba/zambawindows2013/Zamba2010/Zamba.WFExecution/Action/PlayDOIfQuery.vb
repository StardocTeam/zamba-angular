Public Class PlayDOIfQuery

    Private _myRule As IDOIfQuery
    Private value As String
    Private compareValue As String
    Private ifsql As String
    Private sqlaux As String
    Private sqlaux2 As String
    Private strselect As String
    Private strline As String
    Private Ds As DataSet
    Private valueexec As Object

    Public Sub New(ByVal rule As IDOIfQuery)
        _myRule = rule
        Ds = New DataSet
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Realizando consulta...")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando DoIfQuery")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Results count " & results.Count)
            If results.Count = 0 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia If Original: " & _myRule.IFSQL)
                strselect = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.IFSQL)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia If modificada: " & strselect)

                strselect = strselect.Replace("&lt;", "<")
                strselect = strselect.Replace("&gt;", ">")
                strselect = strselect.Replace("&lt", "<")
                strselect = strselect.Replace("&gt", ">")

                strselect = WFRuleParent.ReconocerVariablesValuesSoloTexto(strselect)

                value = ExecuteScalar(strselect)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor obtenido: " & value)

                ZTrace.WriteLineIf(ZTrace.IsInfo, "comparador Original: " & _myRule.IFValue)
                compareValue = String.Empty
                compareValue = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.IFValue)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia Original: " & compareValue)
                ' para mantener la funcionalidad vieja, ingrasamos False como parametro en tmpCaseInsensitive
                If ToolsBusiness.ValidateComp(value, compareValue, _myRule.CompType, False) Then
                    sqlaux = _myRule.SQL
                    sqlaux = sqlaux.Replace("&lt;", "<")
                    sqlaux = sqlaux.Replace("&gt;", ">")
                    sqlaux = sqlaux.Replace("&lt", "<")
                    sqlaux = sqlaux.Replace("&gt", ">")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia Original: " & sqlaux)
                    strselect = WFRuleParent.ReconocerVariablesValuesSoloTexto(sqlaux)
                Else
                    sqlaux2 = _myRule.SQL2
                    sqlaux2 = sqlaux2.Replace("&lt;", "<")
                    sqlaux2 = sqlaux2.Replace("&gt;", ">")
                    sqlaux2 = sqlaux2.Replace("&lt", "<")
                    sqlaux2 = sqlaux2.Replace("&gt", ">")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia Original: " & sqlaux2)
                    strselect = WFRuleParent.ReconocerVariablesValuesSoloTexto(sqlaux2)
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia: " & strselect)

                strselect = strselect.Replace("&lt;", "<")
                strselect = strselect.Replace("&gt;", ">")
                strselect = strselect.Replace("&lt", "<")
                strselect = strselect.Replace("&gt", ">")

                'If strselect <> String.Empty Then
                If Not String.IsNullOrEmpty(strselect) Then
                    While Not String.IsNullOrEmpty(strselect)
                        If strselect.Split(New Char() {"§"}, StringSplitOptions.RemoveEmptyEntries).Length = 0 Then
                            Exit While
                        End If
                        strline = strselect.Split(New Char() {"§"}, StringSplitOptions.RemoveEmptyEntries)(0)
                        strselect = strselect.Replace(strline, "")

                        If strline.ToLower().Contains("update") Or strline.ToLower().Contains("insert") Then
                            ExecuteNonQuery(strline)
                        Else
                            If _myRule.ExecuteType = "ESCALAR" Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia: " & strline)
                                valueexec = ExecuteScalar(strline)
                                ZTrace.WriteLineIf(ZTrace.IsInfo And IsNothing(valueexec) = False, "Resultado obtenido: " & valueexec.ToString())
                                If VariablesInterReglas.ContainsKey(_myRule.HashTable) = False Then
                                    VariablesInterReglas.Add(_myRule.HashTable, valueexec, False)
                                Else
                                    VariablesInterReglas.Item(_myRule.HashTable) = valueexec
                                End If
                            ElseIf _myRule.ExecuteType = "NONQUERY" Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia: " & strline)
                                ExecuteNonQuery(strline)
                            Else 'DATASET
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia: " & strline)
                                ExecuteDataset(strline)
                                ZTrace.WriteLineIf(ZTrace.IsInfo And IsNothing(Ds) = False, "Resultado Dataset Count: " & Ds.Tables(0).Rows.Count)
                                If VariablesInterReglas.ContainsKey(_myRule.HashTable) = False Then
                                    VariablesInterReglas.Add(_myRule.HashTable, Ds, False)
                                Else
                                    VariablesInterReglas.Item(_myRule.HashTable) = Ds
                                End If
                            End If
                        End If
                    End While
                End If
            Else
                For Each r As TaskResult In results
                    ifsql = _myRule.IFSQL
                    ifsql = ifsql.Replace("&lt;", "<")
                    ifsql = ifsql.Replace("&gt;", ">")
                    ifsql = ifsql.Replace("&lt", "<")
                    ifsql = ifsql.Replace("&gt", ">")
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia Original: " & ifsql)
                    strselect = TextoInteligente.ReconocerCodigo(ifsql, r)
                    strselect = WFRuleParent.ReconocerVariablesValuesSoloTexto(strselect)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia: " & strselect)

                    value = ExecuteScalar(strselect)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor obtenido: " & value)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "comparador Original: " & _myRule.IFValue)
                    compareValue = String.Empty
                    compareValue = TextoInteligente.ReconocerCodigo(_myRule.IFValue, r)
                    compareValue = WFRuleParent.ReconocerVariablesValuesSoloTexto(compareValue)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia Original: " & compareValue)
                    ' para mantener la funcionalidad vieja, ingrasamos False como parametro en tmpCaseInsensitive
                    If ToolsBusiness.ValidateComp(value, compareValue, _myRule.CompType, False) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se cumple")
                        sqlaux = _myRule.SQL
                        sqlaux = sqlaux.Replace("&lt;", "<")
                        sqlaux = sqlaux.Replace("&gt;", ">")
                        sqlaux = sqlaux.Replace("&lt", "<")
                        sqlaux = sqlaux.Replace("&gt", ">")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia Original: " & sqlaux)
                        strselect = TextoInteligente.ReconocerCodigo(sqlaux, r)
                        strselect = WFRuleParent.ReconocerVariablesValuesSoloTexto(strselect)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se cumple")
                        sqlaux2 = _myRule.SQL2
                        sqlaux2 = sqlaux2.Replace("&lt;", "<")
                        sqlaux2 = sqlaux2.Replace("&gt;", ">")
                        sqlaux2 = sqlaux2.Replace("&lt", "<")
                        sqlaux2 = sqlaux2.Replace("&gt", ">")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia Original: " & sqlaux2)
                        strselect = TextoInteligente.ReconocerCodigo(sqlaux2, r)
                        strselect = WFRuleParent.ReconocerVariablesValuesSoloTexto(strselect)
                    End If

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia: " & strselect)

                    strselect = strselect.Replace("&lt;", "<")
                    strselect = strselect.Replace("&gt;", ">")
                    strselect = strselect.Replace("&lt", "<")
                    strselect = strselect.Replace("&gt", ">")
                    If strselect <> String.Empty Then

                        While Not String.IsNullOrEmpty(strselect)

                            If strselect.Split(New Char() {"§"}, StringSplitOptions.RemoveEmptyEntries).Length = 0 Then
                                Exit While
                            End If

                            strline = strselect.Split(New Char() {"§"}, StringSplitOptions.RemoveEmptyEntries)(0)
                            strselect = strselect.Replace(strline, "")

                            If strline.ToLower().Contains("update") Or strline.ToLower().Contains("insert") Then
                                ExecuteNonQuery(strline)
                            Else
                                If _myRule.ExecuteType = "ESCALAR" Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia: " & strline)
                                    valueexec = ExecuteScalar(strline)
                                    If valueexec Is Nothing Then valueexec = String.Empty
                                    ZTrace.WriteLineIf(ZTrace.IsInfo() And Not IsNothing(valueexec), "Resultado obtenido: " & valueexec.ToString())
                                    If _myRule.HashTable.Contains("<<") Then
                                        TextoInteligente.AsignItemFromSmartText(_myRule.HashTable, r, valueexec)
                                    Else
                                        If VariablesInterReglas.ContainsKey(_myRule.HashTable) = False Then
                                            VariablesInterReglas.Add(_myRule.HashTable, valueexec, False)
                                        Else
                                            VariablesInterReglas.Item(_myRule.HashTable) = valueexec
                                        End If
                                    End If
                                ElseIf _myRule.ExecuteType = "NONQUERY" Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia: " & strline)
                                    ExecuteNonQuery(strline)
                                Else 'DATASET
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Sentencia: " & strline)
                                    ExecuteDataset(strline)

                                    WFRuleParent.GeneratarVariablesDesdeDS(Ds)

                                    ZTrace.WriteLineIf(ZTrace.IsInfo And IsNothing(Ds) = False, "Resultado Dataset Count: " & Ds.Tables(0).Rows.Count)
                                    If VariablesInterReglas.ContainsKey(_myRule.HashTable) = False Then
                                        VariablesInterReglas.Add(_myRule.HashTable, Ds, False)
                                    Else
                                        VariablesInterReglas.Item(_myRule.HashTable) = Ds
                                    End If
                                End If
                            End If
                        End While
                    End If
                Next
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Consulta procesada con éxito!")
        Finally

            value = Nothing
            compareValue = Nothing
            ifsql = Nothing
            sqlaux = Nothing
            sqlaux2 = Nothing
            strselect = Nothing
            strline = Nothing
            Ds = Nothing
            valueexec = Nothing
        End Try

        Return results
    End Function


    ''' <summary>
    ''' Ejecuta una consulta q no devuelve valores
    ''' </summary>
    ''' <param name="myRule"></param>
    ''' <param name="strselect"></param>
    ''' <remarks></remarks>
    Private Sub ExecuteNonQuery(ByVal strquery As String)
        If strquery.ToLower.Trim.StartsWith("exec ") AndAlso Servers.Server.isOracle Then
            Dim params As List(Of Object) = ServersBusiness.GetStoreParams(strquery)
            ServersBusiness.BuildExecuteNonQuery(params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))

        Else
            ServersBusiness.BuildExecuteNonQuery(CommandType.Text, strquery)
        End If
    End Sub

    Private Sub ExecuteDataset(ByVal strquery As String)
        Ds = Nothing
        If strquery.ToLower.Trim.StartsWith("exec ") AndAlso Servers.Server.isOracle Then
            Dim params As List(Of Object) = ServersBusiness.GetStoreParams(strquery)
            Ds = ServersBusiness.BuildExecuteDataSet(params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
        Else
            Ds = ServersBusiness.BuildExecuteDataSet(CommandType.Text, strquery)
        End If
    End Sub

    Private Function ExecuteScalar(ByVal strquery As String) As Object
        If strquery.ToLower.Trim.StartsWith("exec ") AndAlso Servers.Server.isOracle Then
            Dim params As List(Of Object) = ServersBusiness.GetStoreParams(strquery)
            Return ServersBusiness.BuildExecuteScalar(params(0).ToString, DirectCast(params(1), Object()), DirectCast(params(2), Object), DirectCast(params(3), Object()))
        Else
            Return ServersBusiness.BuildExecuteScalar(CommandType.Text, strquery)
        End If
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
