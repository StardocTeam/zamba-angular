
Imports System.Text
Imports Zamba.Servers
''' <summary>
''' Pregunta por un indice y guardar el valor ingresado
''' </summary>
''' <history>Marcelo Modified 02/12/08</history>
''' <remarks></remarks>
Public Class PlayDOApproveMatrix
    'Private m_Name As String

    Private _myRule As IDOApproveMatrix
    Private lstResults As System.Collections.Generic.List(Of Core.ITaskResult)
    Private Valor As String
    Private lstIndexsIds As List(Of Int64)
    Private MatrixEntity As IDocType

    Sub New(ByVal rule As IDOApproveMatrix)
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
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        lstResults = New System.Collections.Generic.List(Of Core.ITaskResult)
        Valor = String.Empty
        lstIndexsIds = New List(Of Int64)(1)

        MatrixEntity = DocTypeBusinessExt.GetDocTypeByID(_myRule.MatrixEntityId)
        MatrixEntity.Indexs = IndexsBusiness.GetIndexsSchemaAsListOfDT(_myRule.MatrixEntityId, True)
        Try
            For Each R As ITaskResult In results
                Dim NotNullTaskIndexs As New List(Of IIndex)
                For Each TI As IIndex In R.Indexs
                    If TI.Data <> String.Empty AndAlso MatrixHasIndex(TI.ID) Then
                        NotNullTaskIndexs.Add(TI)
                    End If
                Next

                Dim query As New StringBuilder
                '      Dim queryOrder As New StringBuilder

                query.Append("select ")
                If Server.isSQLServer Then
                    query.Append(" top (1) ")
                End If
                query.Append(" I")
                query.Append(_myRule.OutputIndex1)
                query.Append(" as salida  from (")

                query.Append("select I")
                query.Append(_myRule.OutputIndex1)
                query.Append(",")

                For Each i As IIndex In NotNullTaskIndexs
                    If i.ID <> _myRule.OutputIndex1 Then
                        query.Append(" I")
                        query.Append(i.ID)
                        query.Append(",")
                    End If
                Next
                query.Remove(query.Length - 1, 1)

                query.Append(" from ")
                query.Append(Results_Business.MakeTable(_myRule.MatrixEntityId, Data.Results_Factory.TableType.Indexs))
                query.Append(" where (")
                '                queryOrder.Append(" order by ")
                For Each i As IIndex In NotNullTaskIndexs
                    If i.ID <> _myRule.OutputIndex1 Then
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
                        '                   queryOrder.Append("I" & i.ID & " desc ")
                        '                  queryOrder.Append(", ")
                    End If
                Next

                query.Remove(query.Length - 4, 4)
                '             queryOrder.Remove(queryOrder.Length - 2, 2)
                '            query.Append(queryOrder.ToString)
                query.Append(" ) ")

                query.Append(" and I")
                query.Append(_myRule.OutputIndex1)
                query.Append(" is not null ")

                query.Append(" UNION ALL ")


                query.Append("select I")
                query.Append(_myRule.OutputIndex1)
                query.Append(",")

                For Each i As IIndex In NotNullTaskIndexs
                    If i.ID <> _myRule.OutputIndex1 Then
                        query.Append(" I")
                        query.Append(i.ID)
                        query.Append(",")
                    End If
                Next
                query.Remove(query.Length - 1, 1)

                query.Append(" from ")
                query.Append(Results_Business.MakeTable(_myRule.MatrixEntityId, Data.Results_Factory.TableType.Indexs))
                query.Append(" where (")
                '                queryOrder.Append(" order by ")
                For Each i As IIndex In NotNullTaskIndexs
                    If i.ID <> _myRule.OutputIndex1 Then
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
                        '                   queryOrder.Append("I" & i.ID & " desc ")
                        '                  queryOrder.Append(", ")
                    End If

                Next

                query.Remove(query.Length - 3, 3)
                '             queryOrder.Remove(queryOrder.Length - 2, 2)
                '            query.Append(queryOrder.ToString)
                query.Append(" ) ")

                query.Append(" and I")
                query.Append(_myRule.OutputIndex1)
                query.Append(" is not null ")

                query.Append(" ) ")

                If Server.isOracle Then
                    query.Append(" q where rownum = 1")
                End If
                ZTrace.WriteLineIf(ZTrace.IsVerbose, query.ToString())
                Dim value As Object = Server.Con.ExecuteScalar(CommandType.Text, query.ToString)

                If value IsNot Nothing Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Valor Obtenido de la Matriz " & value.ToString())
                    If VariablesInterReglas.ContainsKey(_myRule.OutputVariable1) = False Then
                        VariablesInterReglas.Add(_myRule.OutputVariable1, value.ToString, False)
                    Else
                        VariablesInterReglas.Item(_myRule.OutputVariable1) = value.ToString
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
