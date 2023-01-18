Imports Zamba.Core

Public Class PlayIfIndex

    Private _myRule As IIfIndex
    Private TodosLosIndicesValidos As Boolean
    Private HayUnIndiceValido As Boolean
    Private Objetos() As String
    Private indice As Index
    Private idComparator As Int32
    Private Value As String
    Private IndiceData As String

    Sub New(ByVal rule As IIfIndex)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim ruleid As Integer = _myRule.ID

        Dim WFRB As New WFRulesBusiness
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
        If (Me._myRule.ChildRulesIds Is Nothing OrElse Me._myRule.ChildRulesIds.Count = 0) Then
            Me._myRule.ChildRulesIds = WFRB.GetChildRulesIds(Me._myRule.ID, Me._myRule.RuleClass, results)
        End If

        If Me._myRule.ChildRulesIds.Count = 2 Then
            For Each childruleId As Int64 In Me._myRule.ChildRulesIds
                Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                R.ParentRule = _myRule
                R.IsAsync = _myRule.IsAsync
                If R.GetType().ToString().Contains("IfBranch") Then
                    Return results
                End If
            Next
        End If

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)()

        Try
            ' Si no hay condiciones...
            If Me._myRule.Condiciones = String.Empty Then Return results

            ' Se itera cada result...
            For Each resultItem As Result In results

                Me.TodosLosIndicesValidos = True
                Me.HayUnIndiceValido = False

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando atributos del documento: " & resultItem.Name & "...")
                ' Se itera cada condicion...
                For Each Condicion As String In Me._myRule.Condiciones.Split("*")
                    ' Se Toma IdCampoIndice, operador y valor...
                    Me.Objetos = Condicion.Split("|")

                    ' Se Busca el indice para el result actual...
                    Me.indice = resultItem.GetIndexById(CType(Me.Objetos(0), System.Int32))

                    '(pablo) - 31032011 - se cambia el valor "1" del indice a "true"
                    'para que posterirmente pueda ser validado por el metodo Compare
                    Dim IndexValue As String = String.Empty
                    If Not IsNothing(Me.indice) Then
                        If Me.indice.Type = "9" Then
                            IndexValue = Me.indice.Data
                            If Me.indice.Data = "1" Then
                                IndexValue = "True"
                            Else
                                IndexValue = "False"
                            End If
                        End If
                    End If

                    If IndexValue = String.Empty Then
                        IndexValue = Me.indice.Data
                    End If
                    '  si existe el Indice para este result...
                    If Not IsNothing(Me.indice) Then

                        ' Toma el Comparador...
                        Me.idComparator = CType(Me.Objetos(1), System.Int32)

                        ' Toma el Valor esperado...
                        Me.Value = Me.Objetos(2)

                        'entra por AND
                        If Me._myRule.OperatorAND Then

                            If Not IndexsBusiness.CompareIndex(IndexValue, Me.indice.Type, Me.idComparator, Me.Value) Then
                                Me.TodosLosIndicesValidos = False
                                Exit For
                            End If

                        Else
                            'Entre por OR
                            If IndexsBusiness.CompareIndex(IndexValue, Me.indice.Type, Me.idComparator, Me.Value) Then
                                Me.HayUnIndiceValido = True
                                Exit For
                            End If

                        End If

                    End If
                Next

                ' Si se cumplen las validaciones de los campos...
                ' Dependiendo del operador "And" o "Or" agrega el resultItem
                If Me._myRule.OperatorAND = True Then
                    If Me.TodosLosIndicesValidos = True Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Todos los atributos son válidos")
                        NewList.Add(resultItem)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encontraron atributos inválidos")
                    End If
                Else
                    If Me.HayUnIndiceValido = True Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Existe al menos un índice válido")
                        NewList.Add(resultItem)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encontraron atributos válidos")
                    End If
                End If
            Next
        Finally
            Objetos = Nothing
            indice = Nothing
            idComparator = 0
            Value = Nothing
            IndiceData = Nothing
        End Try
        Return NewList
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
            ZTrace.WriteLineIf(ZTrace.IsInfo, "IF Index " & ifType)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Condicion " & Me._myRule.Condiciones)
            ' Si no hay condiciones...
            'If Me._myRule.Condiciones = String.Empty Then Return results
            If String.IsNullOrEmpty(Me._myRule.Condiciones) Then Return results

            ' Se itera cada result...
            For Each resultItem As Result In results
                Me.HayUnIndiceValido = False
                Me.TodosLosIndicesValidos = True

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando atributos del documento: " & resultItem.Name & "...")
                ' Se itera cada condicion...
                For Each Condicion As String In Me._myRule.Condiciones.Split("*")
                    ' Se Toma IdCampoIndice, operador y valor...
                    Me.Objetos = Condicion.Split("|")

                    ' Se Busca el indice para el result actual...
                    Me.indice = resultItem.GetIndexById(CType(Me.Objetos(0), System.Int32))

                    '(pablo) - 31032011 - se cambia el valor "1" del indice a "true"
                    'para que posterirmente pueda ser validado por el metodo Compare
                    Dim IndexValue As String = String.Empty
                    If Not IsNothing(Me.indice) Then
                        If Me.indice.Type = "9" Then
                            IndexValue = Me.indice.Data
                            If Me.indice.Data = "1" Then
                                IndexValue = "True"
                            Else
                                IndexValue = "False"
                            End If
                        End If

                        If IndexValue = String.Empty Then
                            IndexValue = Me.indice.Data
                        End If
                        Me.IndiceData = String.Empty
                        If Not IsNothing(Me.indice.Data) Then Me.IndiceData = Me.indice.Data

                        ' Toma el Comparador...
                        Me.idComparator = CType(Me.Objetos(1), System.Int32)

                        ' Toma el Valor esperado...
                        Me.Value = Me.Objetos(2)
                        'Convierto texto inteligente y ZVar
                        Me.Value = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.Value, resultItem).Trim()
                        If Me.Value.ToString.ToLower.Contains("zvar") Then
                            Dim varInterReglas As New VariablesInterReglas()
                            Me.Value = varInterReglas.ReconocerVariablesValuesSoloTexto(Me.Value)
                            varInterReglas = Nothing
                        End If

                        'entra por AND
                        If Me._myRule.OperatorAND Then
                            If Not IndexsBusiness.CompareIndex(IndexValue, Me.indice.Type, Me.idComparator, Me.Value) Then
                                Me.TodosLosIndicesValidos = False
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No Valido!")
                                Exit For
                            End If
                        Else
                            'Entre por OR
                            If IndexsBusiness.CompareIndex(IndexValue, Me.indice.Type, Me.idComparator, Me.Value) Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se cumple la condicion")
                                Me.HayUnIndiceValido = True
                                Exit For
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se cumple la condicion")
                            End If
                        End If
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El indice es inexistente")
                    End If
                Next

                ' Si se cumplen las validaciones de los campos...
                ' Dependiendo del operador "And" o "Or" agrega el resultItem
                ZTrace.WriteLineIf(ZTrace.IsInfo, "OperadorAnd " & Me._myRule.OperatorAND)
                If Me._myRule.OperatorAND = True Then
                    If Me.TodosLosIndicesValidos = ifType Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Todos los atributos son válidos")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando Result")
                        NewList.Add(resultItem)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encontraron atributos inválidos")
                    End If
                Else
                    If Me.HayUnIndiceValido = ifType Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Existe al menos un índice válido")
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando Result")
                        NewList.Add(resultItem)
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encontraron atributos válidos")
                    End If
                End If
            Next

            ' If Not Me.compare(indice.Data, idComparator, Value) Then
            'hayUnIndiceValido = False
            'Exit For
            'End If
            'End If
            'Next

            ' Si se cumplen las validaciones de los campos...
            'If hayUnIndiceValido = ifType Then
            'NewList.Add(resultItem)
            'End If
            'Next
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad Result Devueltos " & NewList.Count)
        Finally
            Objetos = Nothing
            indice = Nothing
            idComparator = 0
            Value = Nothing
            IndiceData = Nothing
        End Try
        Return NewList
    End Function

    ''Este método se comenta porque sea cual sea el tipo del índice se compara
    ''como tipo String, con lo cual: 19999 < 5 es verdadero.
    ''[Alejandro]
    '' Compara dos valores segun el comparador espesificado
    'Private Function compare(ByVal resultIndexValue As String, _
    ' ByVal comparator As System.Int32, _
    ' ByVal value As String _
    ' ) As Boolean
    '    Select Case comparator
    '        Case Comparators.Equal
    '            If resultIndexValue.Equals(value) Then Return True
    '        Case Comparators.Different
    '            If Not resultIndexValue.Equals(value) Then Return True
    '        Case Comparators.Contents
    '            If resultIndexValue.IndexOf(value) > -1 Then Return True
    '        Case Comparators.Starts
    '            If resultIndexValue.StartsWith(value) Then Return True
    '        Case Comparators.Ends
    '            If resultIndexValue.EndsWith(value) Then Return True
    '        Case Comparators.Upper
    '            If resultIndexValue.CompareTo(value) > 0 Then Return True
    '        Case Comparators.Lower
    '            If resultIndexValue.CompareTo(value) < 0 Then Return True
    '        Case Comparators.EqualLower
    '            If resultIndexValue.CompareTo(value) <= 0 Then Return True
    '        Case Comparators.EqualUpper
    '            If resultIndexValue.CompareTo(value) >= 0 Then Return True
    '        Case Else
    '            Return False
    '    End Select
    '    Return False

    'End Function

End Class
