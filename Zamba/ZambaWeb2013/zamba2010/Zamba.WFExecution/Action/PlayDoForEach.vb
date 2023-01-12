Public Class PlayDoForEach

    Private _myRule As IDoForEach
    Private objValue As Object
    Private ObjectType As Type
    Private dsValues As DataSet
    Private WFRB As New WFRulesBusiness
    Private dtValues As DataTable
    Private arValues As ArrayList
    Private values As IEnumerable
    Private IsAsync As Boolean

    Sub New(ByVal rule As IDoForEach)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Me.objValue = WFRuleParent.ObtenerValorVariableObjeto(Me._myRule.Value)

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Recorriendo objeto...")
            If Not objValue Is Nothing Then
                'Se pregunta si no es string porque lo detecta como IEnumerable.
                If TypeOf (objValue) Is IEnumerable AndAlso Not (TypeOf (objValue) Is String) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable reconocida como IEnumerable")
                    Me.values = objValue
                    For Each item As Object In values
                        If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                            VariablesInterReglas.Add(Me._myRule.Name, item)
                        Else
                            VariablesInterReglas.Item(Me._myRule.Name) = item
                        End If


                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
                        If (Me._myRule.ChildRulesIds Is Nothing OrElse Me._myRule.ChildRulesIds.Count = 0) Then
                            Me._myRule.ChildRulesIds = WFRB.GetChildRulesIds(Me._myRule.ID, Me._myRule.RuleClass, results)
                        End If

                        For Each childruleId As Int64 In Me._myRule.ChildRulesIds
                            Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                            R.ParentRule = _myRule
                            R.IsAsync = _myRule.IsAsync
                            results = WFRB.ExecuteRule(R.ID, results, Me._myRule.IsAsync)
                        Next
                    Next

                Else
                    Me.ObjectType = objValue.GetType
                    Select Case ObjectType.Name.ToLower()

                        Case "dataset"
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable reconocida como DataSet")
                            Me.dsValues = DirectCast(objValue, DataSet)

                            For Each row As DataRow In dsValues.Tables(0).Rows
                                If VariablesInterReglas.ContainsKey(Me._myRule.Name & "break") = False OrElse VariablesInterReglas.Item(Me._myRule.Name & "break") <> "break" Then

                                    If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                                        VariablesInterReglas.Add(Me._myRule.Name, row)
                                    Else
                                        VariablesInterReglas.Item(Me._myRule.Name) = row
                                    End If


                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
                                    If (Me._myRule.ChildRulesIds Is Nothing OrElse Me._myRule.ChildRulesIds.Count = 0) Then
                                        Me._myRule.ChildRulesIds = WFRB.GetChildRulesIds(Me._myRule.ID, Me._myRule.RuleClass, results)
                                    End If

                                    For Each childruleId As Int64 In Me._myRule.ChildRulesIds
                                        Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                                        R.ParentRule = _myRule
                                        R.IsAsync = _myRule.IsAsync
                                        results = WFRB.ExecuteRule(R.ID, results, Me._myRule.IsAsync)
                                    Next
                                Else
                                    VariablesInterReglas.Remove(Me._myRule.Name & "break")
                                    Exit For
                                End If
                            Next

                        Case "datatable"
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable reconocida como DataTable")
                            Me.dtValues = DirectCast(objValue, DataTable)

                            For Each row As DataRow In dtValues.Rows
                                If VariablesInterReglas.ContainsKey(Me._myRule.Name & "break") = False OrElse VariablesInterReglas.Item(Me._myRule.Name & "break") <> "break" Then
                                    If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                                        VariablesInterReglas.Add(Me._myRule.Name, row)
                                    Else
                                        VariablesInterReglas.Item(Me._myRule.Name) = row
                                    End If

                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
                                    If (_myRule.ChildRulesIds Is Nothing OrElse _myRule.ChildRulesIds.Count = 0) Then
                                        _myRule.ChildRulesIds = WFRB.GetChildRulesIds(_myRule.ID, _myRule.RuleClass, results)
                                    End If
                                    For Each childruleId As Int64 In Me._myRule.ChildRulesIds
                                        Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                                        R.ParentRule = _myRule
                                        R.IsAsync = _myRule.IsAsync
                                        results = WFRB.ExecuteRule(R.ID, results, Me._myRule.IsAsync)
                                    Next
                                Else
                                    VariablesInterReglas.Remove(Me._myRule.Name & "break")
                                    Exit For


                                End If

                            Next

                        Case "int32"
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable reconocida como Int32")
                            Dim iValues As Int32 = 0
                            iValues = Int32.Parse(objValue)

                            For count As Int32 = 1 To iValues Step 1

                                If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                                    VariablesInterReglas.Add(Me._myRule.Name, count)
                                Else
                                    VariablesInterReglas.Item(Me._myRule.Name) = count
                                End If


                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
                                If (_myRule.ChildRulesIds Is Nothing OrElse _myRule.ChildRulesIds.Count = 0) Then
                                    _myRule.ChildRulesIds = WFRB.GetChildRulesIds(_myRule.ID, _myRule.RuleClass, results)
                                End If
                                For Each childruleId As Int64 In Me._myRule.ChildRulesIds
                                    Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                                    R.ParentRule = _myRule
                                    R.IsAsync = _myRule.IsAsync
                                    results = WFRB.ExecuteRule(R.ID, results, Me._myRule.IsAsync)
                                Next

                            Next

                        Case "int64"
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable reconocida como Int64")
                            Dim iValues As Int64
                            iValues = Int64.Parse(objValue)

                            For count As Int64 = 1 To iValues Step 1


                                If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                                    VariablesInterReglas.Add(Me._myRule.Name, count)
                                Else
                                    VariablesInterReglas.Item(Me._myRule.Name) = count
                                End If



                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
                                If (_myRule.ChildRulesIds Is Nothing OrElse _myRule.ChildRulesIds.Count = 0) Then
                                    _myRule.ChildRulesIds = WFRB.GetChildRulesIds(_myRule.ID, _myRule.RuleClass, results)
                                End If
                                For Each childruleId As Int64 In Me._myRule.ChildRulesIds
                                    Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                                    R.ParentRule = _myRule
                                    R.IsAsync = _myRule.IsAsync
                                    results = WFRB.ExecuteRule(R.ID, results, Me._myRule.IsAsync)
                                Next
                            Next

                        Case "decimal"
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable reconocida como Decimal")
                            Dim dValues As Decimal
                            dValues = Decimal.Parse(objValue)


                            For count As Int64 = 1 To dValues Step 1

                                If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                                    VariablesInterReglas.Add(Me._myRule.Name, count)
                                Else
                                    VariablesInterReglas.Item(Me._myRule.Name) = count
                                End If


                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
                                If (_myRule.ChildRulesIds Is Nothing OrElse _myRule.ChildRulesIds.Count = 0) Then
                                    _myRule.ChildRulesIds = WFRB.GetChildRulesIds(_myRule.ID, _myRule.RuleClass, results)
                                End If
                                For Each childruleId As Int64 In Me._myRule.ChildRulesIds
                                    Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                                    R.ParentRule = _myRule
                                    R.IsAsync = _myRule.IsAsync
                                    results = WFRB.ExecuteRule(R.ID, results, Me._myRule.IsAsync)
                                Next
                            Next

                        Case "string"
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable reconocida como String")
                            Dim value As Int64

                            If Int64.TryParse(objValue.ToString().Trim(), value) Then

                                For count As Int64 = 1 To value Step 1

                                    If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                                        VariablesInterReglas.Add(Me._myRule.Name, count)
                                    Else
                                        VariablesInterReglas.Item(Me._myRule.Name) = count
                                    End If



                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
                                    If (_myRule.ChildRulesIds Is Nothing OrElse _myRule.ChildRulesIds.Count = 0) Then
                                        _myRule.ChildRulesIds = WFRB.GetChildRulesIds(_myRule.ID, _myRule.RuleClass, results)
                                    End If
                                    For Each childruleId As Int64 In Me._myRule.ChildRulesIds

                                        Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                                        R.ParentRule = _myRule
                                        R.IsAsync = _myRule.IsAsync
                                        results = WFRB.ExecuteRule(R.ID, results, Me._myRule.IsAsync)
                                    Next
                                Next


                            End If

                        Case Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Tipo de dato ")
                            'Case "list`1"
                            '[sebastian 15-04-2009] Look for the correct type of generic list and returns
                            'the result complete
                            '[Ezequiel] - 21/09/09 - Se cambio la llamada al metodo "LookForTypeOfGenericList" porque se unifico utilizando
                            '                        la interfaz ienumerable.
                            'results = LookForTypeOfGenericList(results, objValue)
                    End Select
                End If

                'If VariablesInterReglas.ContainsKey(Me._myRule.Name) = True Then
                '    VariablesInterReglas.Remove(Me._myRule.Name)
                'End If

            Else
                If IsNumeric(Me._myRule.Value) = True Then

                    '[sebastian 15-04-2009] en caso de que se le haya pasado como variable un numero 
                    'se iterar tantas veces como diga ese numero
                    Dim iValues As Int32 = 0
                    iValues = Int32.Parse(Me._myRule.Value)

                    For count As Int32 = 1 To iValues Step 1
                        If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                            VariablesInterReglas.Add(Me._myRule.Name, count)
                        Else
                            VariablesInterReglas.Item(Me._myRule.Name) = count
                        End If



                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
                        If (_myRule.ChildRulesIds Is Nothing OrElse _myRule.ChildRulesIds.Count = 0) Then
                            _myRule.ChildRulesIds = WFRB.GetChildRulesIds(_myRule.ID, _myRule.RuleClass, results)
                        End If
                        For Each childruleId As Int64 In Me._myRule.ChildRulesIds

                            Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                            R.ParentRule = _myRule
                            R.IsAsync = _myRule.IsAsync
                            results = WFRB.ExecuteRule(R.ID, results, Me._myRule.IsAsync)
                        Next
                    Next
                End If
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Fin del recorrido.")
        Finally

            Me.objValue = Nothing
            Me.ObjectType = Nothing
            Me.dsValues = Nothing

            Me.dtValues = Nothing
            Me.arValues = Nothing
            Me.values = Nothing
        End Try
        Return New List(Of ITaskResult)
    End Function


    ''' <summary>
    ''' [sebastian] 15-04-2009 Create assess whether it is a generic list and review it
    ''' </summary>
    ''' <param name="Results"></param>
    ''' <param name="ObjValue"></param>
    ''' <param name="rule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Private Function LookForTypeOfGenericList(ByVal Results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ObjValue As Object) As Generic.List(Of ITaskResult)
    '    Dim ObjFullName As Type = ObjValue.GetType
    '    Select Case ObjFullName.GetGenericArguments(0).Name.ToLower()
    '        Case "int32"
    '            Dim iValues As New Generic.List(Of Int32)
    '            iValues = DirectCast(ObjValue, Generic.List(Of Int32))

    '            For Each item As Int32 In iValues
    '                If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
    '                    VariablesInterReglas.Add(Me._myRule.Name, item)
    '                Else
    '                    VariablesInterReglas.Item(Me._myRule.Name) = item
    '                End If

    '                For Each r As IRule In Me._myRule.ChildRules
    '                    Dim WFRB As New WFRulesBusiness
    '                    Results = WFRB.ExecuteRule(r.ID, Me._myRule.WFStepId, Results)
    '                Next
    '            Next
    '        Case "int64"
    '            Dim Values As New Generic.List(Of Int64)
    '            Values = DirectCast(ObjValue, Generic.List(Of Int64))

    '            For Each item As Int64 In Values
    '                If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
    '                    VariablesInterReglas.Add(Me._myRule.Name, item)
    '                Else
    '                    VariablesInterReglas.Item(Me._myRule.Name) = item
    '                End If

    '                For Each r As IRule In Me._myRule.ChildRules
    '                    Dim WFRB As New WFRulesBusiness
    '                    Results = WFRB.ExecuteRule(r.ID, Me._myRule.WFStepId, Results)
    '                Next
    '            Next
    '        Case "decimal"
    '            Dim Values As New Generic.List(Of Decimal)
    '            Values = DirectCast(ObjValue, Generic.List(Of Decimal))

    '            For Each item As Decimal In Values
    '                If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
    '                    VariablesInterReglas.Add(Me._myRule.Name, item)
    '                Else
    '                    VariablesInterReglas.Item(Me._myRule.Name) = item
    '                End If

    '                For Each r As IRule In Me._myRule.ChildRules
    '                    Dim WFRB As New WFRulesBusiness
    '                    Results = WFRB.ExecuteRule(r.ID, Me._myRule.WFStepId, Results)
    '                Next
    '            Next
    '        Case "string"
    '            Dim Values As New Generic.List(Of String)
    '            Values = DirectCast(ObjValue, Generic.List(Of String))

    '            For Each item As String In Values
    '                If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
    '                    VariablesInterReglas.Add(Me._myRule.Name, item)
    '                Else
    '                    VariablesInterReglas.Item(Me._myRule.Name) = item
    '                End If

    '                For Each r As IRule In Me._myRule.ChildRules
    '                    Dim WFRB As New WFRulesBusiness
    '                    Results = WFRB.ExecuteRule(r.ID, Me._myRule.WFStepId, Results)
    '                Next
    '            Next
    '        Case "object"
    '            Dim Values As New Generic.List(Of Object)
    '            Values = DirectCast(ObjValue, Generic.List(Of Object))

    '            For Each item As Object In Values
    '                If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
    '                    VariablesInterReglas.Add(Me._myRule.Name, item)
    '                Else
    '                    VariablesInterReglas.Item(Me._myRule.Name) = item
    '                End If

    '                For Each r As IRule In Me._myRule.ChildRules
    '                    Dim WFRB As New WFRulesBusiness
    '                    Results = WFRB.ExecuteRule(r.ID, Me._myRule.WFStepId, Results)
    '                Next
    '            Next
    '    End Select
    '    Return Results
    'End Function
End Class
