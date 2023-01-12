Public Class PlayIfIndex
    Private _myRule As IIfIndex
    Private TodosLosIndicesValidos As Boolean
    Private HayUnIndiceValido As Boolean
    Private Objetos() As String
    Private idComparator As Int32
    Private indexID As Int64
    Private Value As String
    Private IndiceData As String
    Private Comparator As String
    Sub New(ByVal rule As IIfIndex)
        _myRule = rule
    End Sub
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    ''' <summary>
    ''' Ejecucion de la regla
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myrule"></param>
    ''' <param name="ifType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)()
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "IF Index " & ifType & " Condicion " & _myRule.Condiciones)
            ' Si no hay condiciones...
            'If Me._myRule.Condiciones = String.Empty Then Return results
            If String.IsNullOrEmpty(_myRule.Condiciones) Then Return results

            ' Se itera cada result...
            For Each resultItem As Result In results
                HayUnIndiceValido = False
                TodosLosIndicesValidos = True

                ' Se itera cada condicion...
                For Each Condicion As String In _myRule.Condiciones.Split("*")
                    ' Se Toma IdCampoIndice, operador y valor...
                    Objetos = Condicion.Split("|")

                    indexID = Objetos(0)

                    For i As Int64 = 0 To resultItem.Indexs.Count - 1
                        If indexID = resultItem.Indexs(i).ID Then
                            '(pablo) - 31032011 - se cambia el valor "1" del indice a "true"
                            'para que posteriormente pueda ser validado por el metodo Compare
                            Dim IndexValue As String = String.Empty

                            If resultItem.Indexs(i).Type = "9" Then
                                IndexValue = resultItem.Indexs(i).Data
                                If resultItem.Indexs(i).Data = "1" Then
                                    IndexValue = "True"
                                Else
                                    IndexValue = "False"
                                End If
                            End If

                            If IndexValue = String.Empty Then
                                IndexValue = resultItem.Indexs(i).Data
                            End If
                            IndiceData = String.Empty
                            If Not IsNothing(resultItem.Indexs(i).Data) Then IndiceData = resultItem.Indexs(i).Data

                            ' Toma el Comparador...
                            idComparator = CType(Objetos(1), System.Int32)

                            ' Toma el Valor esperado...
                            Value = Objetos(2)
                            'Convierto texto inteligente y ZVar
                            Value = TextoInteligente.ReconocerCodigo(Value, resultItem).Trim()
                            If Value.ToString.ToLower.Contains("zvar") Then
                                Value = WFRuleParent.ReconocerVariablesValuesSoloTexto(Value)
                            End If

                            'entra por AND
                            If _myRule.OperatorAND Then
                                If Not IndexsBusiness.CompareIndex(IndexValue, resultItem.Indexs(i).Type, idComparator, Value, False) Then
                                    TodosLosIndicesValidos = False
                                    Exit For
                                End If
                            Else
                                'Entre por OR
                                If IndexsBusiness.CompareIndex(IndexValue, resultItem.Indexs(i).Type, idComparator, Value, False) Then

                                    HayUnIndiceValido = True
                                    Exit For
                                End If
                            End If
                            Exit For
                        End If
                    Next
                Next

                ' Si se cumplen las validaciones de los campos...
                ' Dependiendo del operador "And" o "Or" agrega el resultItem
                If _myRule.OperatorAND = True Then
                    If TodosLosIndicesValidos = ifType Then
                        NewList.Add(resultItem)
                    End If
                Else
                    If HayUnIndiceValido = ifType Then
                        NewList.Add(resultItem)
                    End If
                End If
            Next

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad Result Devueltos " & NewList.Count)
        Finally
            Objetos = Nothing
            idComparator = 0
            Value = Nothing
            IndiceData = Nothing
        End Try
        Return NewList
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class