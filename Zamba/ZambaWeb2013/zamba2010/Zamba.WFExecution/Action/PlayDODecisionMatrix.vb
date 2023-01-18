
Imports System.Text
Imports Zamba.Servers
''' <summary>
''' Pregunta por un indice y guardar el valor ingresado
''' </summary>
''' <history>Marcelo Modified 02/12/08</history>
''' <remarks></remarks>
Public Class PlayDODecisionMatrix
    'Private m_Name As String

    Private _myRule As IDODecisionMatrix
    Private lstResults As System.Collections.Generic.List(Of Core.ITaskResult)
    Private Valor As String
    Private lstIndexsIds As List(Of Int64)
    Private MatrixEntity As IDocType

    Sub New(ByVal rule As IDODecisionMatrix)
        _myRule = rule
    End Sub

    ''' <summary>
    ''' Pregunta por un indice y guarda el valor en la tabla
    ''' </summary>
    ''' <param name="results">Resulta a ejecutar</param>
    ''' <param name="myrule">Parametros de la regla</param>
    ''' <history>Marcelo Modified 02/12/08</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Public Function Play(results As List(Of ITaskResult)) As List(Of ITaskResult)

    '    lstResults = New System.Collections.Generic.List(Of Core.ITaskResult)
    '    Valor = String.Empty
    '    lstIndexsIds = New List(Of Int64)(1)
    '    Dim DTB As New DocTypesBusiness
    '    MatrixEntity = DTB.GetDocType(_myRule.EntityId, True)
    '    Dim IB As New IndexsBusiness
    '    MatrixEntity.Indexs = IB.GetIndexsSchemaAsListOfDT(_myRule.EntityId)
    '    Try
    '        For Each R As ITaskResult In results
    '            Dim NotNullTaskIndexs As New List(Of IIndex)
    '            For Each TI As IIndex In R.Indexs
    '                If TI.Data <> String.Empty AndAlso MatrixHasIndex(TI.ID) Then
    '                    NotNullTaskIndexs.Add(TI)
    '                End If
    '            Next

    '            Dim query As New StringBuilder
    '            'Dim queryOcurrence As New StringBuilder

    '            query.Append("select DOC_ID, ")
    '            'If Server.isSQLServer Then
    '            '    query.Append(" top (1) ")
    '            'End If
    '            query.Append(" I")
    '            query.Append(_myRule.OutputIndex)
    '            query.Append(" as salida, ")
    '            query.Append(_myRule.AltOutputIndex)
    '            query.Append(" as salidaAlt ")
    '            query.Append(" from (")

    '            query.Append("select DOC_ID, I")
    '            query.Append(_myRule.OutputIndex)
    '            query.Append(",")
    '            query.Append(_myRule.AltOutputIndex)
    '            query.Append(",")

    '            For Each i As IIndex In NotNullTaskIndexs
    '                If i.ID <> _myRule.OutputIndex AndAlso i.ID <> _myRule.AltOutputIndex Then
    '                    query.Append(" I")
    '                    query.Append(i.ID)
    '                    query.Append(",")
    '                End If
    '            Next
    '            query.Remove(query.Length - 1, 1)

    '            query.Append(" from ")
    '            Dim RB As New Results_Business
    '            query.Append(RB.MakeTable(_myRule.EntityId, TableType.Indexs))
    '            query.Append(" where (")
    '            '                queryOrder.Append(" order by ")
    '            For Each i As IIndex In NotNullTaskIndexs
    '                If i.ID <> _myRule.OutputIndex AndAlso i.ID <> _myRule.AltOutputIndex Then
    '                    query.Append(" I")
    '                    query.Append(i.ID)
    '                    query.Append(" = ")
    '                    If (i.Type = IndexDataType.Alfanumerico OrElse i.Type = IndexDataType.Alfanumerico_Largo) Then
    '                        query.Append("'")
    '                        query.Append(i.Data)
    '                        query.Append("'")
    '                    Else
    '                        query.Append(i.Data)
    '                    End If
    '                    query.Append(" and ")
    '                    '                   queryOrder.Append("I" & i.ID & " desc ")
    '                    '                  queryOrder.Append(", ")
    '                End If
    '            Next

    '            query.Remove(query.Length - 4, 4)
    '            '             queryOrder.Remove(queryOrder.Length - 2, 2)
    '            '            query.Append(queryOrder.ToString)
    '            query.Append(" ) ")

    '            query.Append(" and (I")
    '            query.Append(_myRule.OutputIndex)
    '            query.Append(" is not null ")

    '            query.Append(" or I")
    '            query.Append(_myRule.AltOutputIndex)
    '            query.Append(" is not null )")

    '            query.Append(" UNION ALL ")


    '            query.Append("select doc_id, I")
    '            query.Append(_myRule.OutputIndex)
    '            query.Append(",")
    '            query.Append(_myRule.AltOutputIndex)
    '            query.Append(",")

    '            For Each i As IIndex In NotNullTaskIndexs
    '                If i.ID <> _myRule.OutputIndex AndAlso i.ID <> _myRule.AltOutputIndex Then
    '                    query.Append(" I")
    '                    query.Append(i.ID)
    '                    query.Append(",")
    '                End If
    '            Next
    '            query.Remove(query.Length - 1, 1)

    '            query.Append(" from ")
    '            query.Append(Results_Business.MakeTable(_myRule.EntityId, TableType.Indexs))
    '            query.Append(" where (")
    '            '                queryOrder.Append(" order by ")
    '            For Each i As IIndex In NotNullTaskIndexs
    '                If i.ID <> _myRule.OutputIndex AndAlso i.ID <> _myRule.AltOutputIndex Then
    '                    query.Append(" I")
    '                    query.Append(i.ID)
    '                    query.Append(" = ")
    '                    If (i.Type = IndexDataType.Alfanumerico OrElse i.Type = IndexDataType.Alfanumerico_Largo) Then
    '                        query.Append("'")
    '                        query.Append(i.Data)
    '                        query.Append("'")
    '                    Else
    '                        query.Append(i.Data)
    '                    End If
    '                    query.Append(" or ")
    '                    '                   queryOrder.Append("I" & i.ID & " desc ")
    '                    '                  queryOrder.Append(", ")
    '                End If

    '            Next

    '            query.Remove(query.Length - 3, 3)
    '            '             queryOrder.Remove(queryOrder.Length - 2, 2)
    '            '            query.Append(queryOrder.ToString)
    '            query.Append(" ) ")

    '            query.Append(" and (I")
    '            query.Append(_myRule.OutputIndex)
    '            query.Append(" is not null ")

    '            query.Append(" or I")
    '            query.Append(_myRule.AltOutputIndex)
    '            query.Append(" is not null ) ")

    '            query.Append(" ) ")

    '            'If Server.isOracle Then
    '            '    query.Append(" q where rownum = 1")
    '            'End If
    '            ZTrace.WriteLineIf(ZTrace.IsVerbose, query.ToString())
    '            Dim resultobject As Object = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

    '            If resultobject IsNot Nothing Then
    '                Dim ds As DataSet
    '                ds = resultobject
    '                If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then

    '                    Dim ResultRow As DataRow
    '                    If ds.Tables(0).Rows.Count = 1 Then
    '                        ResultRow = ds.Tables(0).Rows(0)
    '                    Else
    '                        Dim BestOcurrence As New SortedDictionary(Of Int64, Int32)
    '                        For Each Ind As IIndex In NotNullTaskIndexs
    '                            Dim Qselect As String = "I" & Ind.ID & " = "
    '                            If (Ind.Type = IndexDataType.Alfanumerico OrElse Ind.Type = IndexDataType.Alfanumerico_Largo) Then
    '                                Qselect = "'" & Ind.Data & "'"
    '                            Else
    '                                Qselect = Ind.Data
    '                            End If
    '                            Dim rows As DataRow() = ds.Tables(0).Select(Qselect)
    '                            For Each sr As DataRow In rows
    '                                If BestOcurrence.ContainsKey(sr.Item("DOC_ID")) Then
    '                                    BestOcurrence(sr.Item("DOC_ID")) = BestOcurrence(sr.Item("DOC_ID")) + 1
    '                                Else
    '                                    BestOcurrence.Add(sr.Item("DOC_ID"), 1)
    '                                End If
    '                            Next

    '                        Next

    '                        If BestOcurrence.Count > 0 Then
    '                            Dim MaxDocId As Int64
    '                            Dim MaxValue As Int32

    '                            For Each val As KeyValuePair(Of Int64, Int32) In BestOcurrence.AsEnumerable()
    '                                If MaxValue < val.Value Then
    '                                    MaxValue = val.Value
    '                                    MaxDocId = val.Key
    '                                End If
    '                            Next

    '                            Dim rows As DataRow() = ds.Tables(0).Select("doc_id = " & MaxDocId)

    '                            If rows.Count > 0 Then
    '                                ResultRow = rows(0)
    '                            End If
    '                        End If
    '                    End If

    '                    Dim Value As String
    '                    If IsDBNull(ResultRow("I" & _myRule.OutputIndex)) OrElse String.IsNullOrEmpty(ResultRow("I" & _myRule.OutputIndex)) Then
    '                        Value = ResultRow("I" & _myRule.AltOutputIndex)
    '                    Else
    '                        Value = ResultRow("I" & _myRule.OutputIndex)
    '                    End If

    '                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Valor Obtenido de la Matriz " & Value.ToString())
    '                    If VariablesInterReglas.ContainsKey(_myRule.OutputVariable) = False Then
    '                        VariablesInterReglas.Add(_myRule.OutputVariable, Value.ToString)
    '                    Else
    '                        VariablesInterReglas.Item(_myRule.OutputVariable) = Value.ToString
    '                    End If

    '                End If
    '            End If
    '            lstResults.Add(R)
    '        Next

    '    Finally

    '    End Try

    '    Return lstResults
    'End Function

    Public Function Play(results As List(Of ITaskResult)) As List(Of ITaskResult)

        lstResults = New System.Collections.Generic.List(Of Core.ITaskResult)
        Valor = String.Empty
        lstIndexsIds = New List(Of Int64)(1)

        MatrixEntity = (New DocTypesBusiness).GetDocType(_myRule.EntityId)
        MatrixEntity.Indexs = (New IndexsBusiness).GetIndexsSchemaAsListOfDT(_myRule.EntityId)
        Try
            For Each R As ITaskResult In results
                Dim NotNullTaskIndexs As New List(Of IIndex)
                For Each TI As IIndex In R.Indexs
                    If TI.Data <> String.Empty AndAlso MatrixHasIndex(TI.ID) Then
                        NotNullTaskIndexs.Add(TI)
                    End If
                Next

                Dim query As New StringBuilder
                query.Append("select ")
                If Server.isSQLServer Then
                    query.Append(" top (1) ")
                End If
                query.Append(" I")
                query.Append(_myRule.OutputIndex)
                query.Append(" as salida  from (")

                query.Append("select I")
                query.Append(_myRule.OutputIndex)
                query.Append(",")

                For Each i As IIndex In NotNullTaskIndexs
                    If i.ID <> _myRule.OutputIndex Then
                        query.Append(" I")
                        query.Append(i.ID)
                        query.Append(",")
                    End If
                Next
                query.Remove(query.Length - 1, 1)

                query.Append(" from ")
                query.Append(Results_Business.MakeTable(_myRule.EntityId, Data.Results_Factory.TableType.Indexs))
                query.Append(" where (")
                For Each i As IIndex In NotNullTaskIndexs
                    If i.ID <> _myRule.OutputIndex Then
                        query.Append(" I")
                        query.Append(i.ID)
                        query.Append(" = ")
                        If (i.Type = IndexDataType.Alfanumerico OrElse i.Type = IndexDataType.Alfanumerico_Largo) Then
                            query.Append("'")
                            query.Append(i.Data)
                            query.Append("'")
                        Else
                            query.Append(i.Data)
                        End If
                        query.Append(" and ")
                    End If
                Next

                query.Remove(query.Length - 4, 4)
                query.Append(" ) ")
                query.Append(" and I")
                query.Append(_myRule.OutputIndex)
                query.Append(" is not null ")
                query.Append(" UNION ALL ")
                query.Append("select I")
                query.Append(_myRule.OutputIndex)
                query.Append(",")

                For Each i As IIndex In NotNullTaskIndexs
                    If i.ID <> _myRule.OutputIndex Then
                        query.Append(" I")
                        query.Append(i.ID)
                        query.Append(",")
                    End If
                Next
                query.Remove(query.Length - 1, 1)

                query.Append(" from ")
                query.Append(Results_Business.MakeTable(_myRule.EntityId, Data.Results_Factory.TableType.Indexs))
                query.Append(" where (")
                For Each i As IIndex In NotNullTaskIndexs
                    If i.ID <> _myRule.OutputIndex Then
                        query.Append(" I")
                        query.Append(i.ID)
                        query.Append(" = ")
                        If (i.Type = IndexDataType.Alfanumerico OrElse i.Type = IndexDataType.Alfanumerico_Largo) Then
                            query.Append("'")
                            query.Append(i.Data)
                            query.Append("'")
                        Else
                            query.Append(i.Data)
                        End If
                        query.Append(" or ")
                    End If
                Next

                query.Remove(query.Length - 3, 3)
                query.Append(" ) ")
                query.Append(" and I")
                query.Append(_myRule.OutputIndex)
                query.Append(" is not null ")
                query.Append(" ) q")

                If Server.isOracle Then
                    query.Append("  where rownum = 1")
                End If

                ZTrace.WriteLineIf(ZTrace.IsVerbose, query.ToString())
                Dim value As Object = Server.Con.ExecuteScalar(CommandType.Text, query.ToString)

                If value IsNot Nothing Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Valor Obtenido de la Matriz " & value.ToString())
                    If VariablesInterReglas.ContainsKey(_myRule.OutputVariable) = False Then
                        VariablesInterReglas.Add(_myRule.OutputVariable, value.ToString)
                    Else
                        VariablesInterReglas.Item(_myRule.OutputVariable) = value.ToString
                    End If
                End If
                lstResults.Add(R)
            Next
        Finally
        End Try
        Return lstResults
    End Function

    Private Function MatrixHasIndex(ID As Int64) As Boolean
        For Each I As Index In MatrixEntity.Indexs
            If I.ID = ID Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
