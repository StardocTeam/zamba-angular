Public Class PlayDoForEach

    Private _myRule As IDoForEach
    Private objValue As Object
    Private ObjectType As Type
    Private dsValues As DataSet
    Private WFRB As New WFRulesBusiness
    Private dtValues As DataTable
    Private arValues As ArrayList
    Private values As IEnumerable

    Sub New(ByVal rule As IDoForEach)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As List(Of Core.ITaskResult), ByVal refreshTask As List(Of Int64)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            objValue = WFRuleParent.ObtenerValorVariableObjeto(_myRule.Value)

            If Not objValue Is Nothing Then
                'Se pregunta si no es string porque lo detecta como IEnumerable.
                If TypeOf (objValue) Is IEnumerable AndAlso Not (TypeOf (objValue) Is String) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Variable: {0} reconocida como IEnumerable", _myRule.Value))
                    values = objValue
                    Dim iterationcount As Int64 = 0

                    For Each item As Object In values
                        iterationcount = iterationcount + 1
                        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("--- Registro numero {0} en variable {1}", iterationcount, _myRule.Name))

                        If VariablesInterReglas.ContainsKey(_myRule.Name) = False Then
                            VariablesInterReglas.Add(_myRule.Name, item, False)
                        Else
                            VariablesInterReglas.Item(_myRule.Name) = item
                        End If

                        For Each r As IRule In _myRule.ChildRules
                            WFRB = New WFRulesBusiness
                            results = WFRB.ExecuteRule(r.ID, _myRule.WFStepId, results, False, refreshTask)
                        Next
                        ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("--- Fin Registro numero {0} en variable {1}", iterationcount, _myRule.Name))

                    Next

                Else
                    ObjectType = objValue.GetType
                    Select Case ObjectType.Name.ToLower()

                        Case "dataset"
                            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Variable: {0} reconocida como DataSet", _myRule.Value))
                            dsValues = DirectCast(objValue, DataSet)

                            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Comienzo de iteracion de {0} registros", dsValues.Tables(0).Rows.Count))

                            Dim iterationcount As Int64 = 0
                            For Each row As DataRow In dsValues.Tables(0).Rows
                                iterationcount = iterationcount + 1
                                ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("--- Registro numero {0} en variable {1}", iterationcount, _myRule.Name))

                                If VariablesInterReglas.ContainsKey(_myRule.Name) = False Then
                                    VariablesInterReglas.Add(_myRule.Name, row, False)
                                Else
                                    VariablesInterReglas.Item(_myRule.Name) = row
                                End If

                                For Each r As IRule In _myRule.ChildRules
                                    WFRB = New WFRulesBusiness
                                    results = WFRB.ExecuteRule(r.ID, _myRule.WFStepId, results, False, refreshTask)
                                Next
                                ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("--- Fin Registro numero {0} en variable {1}", iterationcount, _myRule.Name))

                            Next

                        Case "datatable"
                            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Variable: {0} reconocida como Tabla", _myRule.Value))
                            dtValues = DirectCast(objValue, DataTable)

                            Dim iterationcount As Int64 = 0
                            For Each row As DataRow In dtValues.Rows

                                iterationcount = iterationcount + 1
                                ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("--- Registro numero {0} en variable {1}", iterationcount, _myRule.Name))

                                If VariablesInterReglas.ContainsKey(_myRule.Name) = False Then
                                    VariablesInterReglas.Add(_myRule.Name, row, False)
                                Else
                                    VariablesInterReglas.Item(_myRule.Name) = row
                                End If

                                For Each r As IRule In _myRule.ChildRules
                                    WFRB = New WFRulesBusiness
                                    results = WFRB.ExecuteRule(r.ID, _myRule.WFStepId, results, False, refreshTask)
                                Next
                                ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("--- Fin Registro numero {0} en variable {1}", iterationcount, _myRule.Name))

                            Next

                        Case "int32"
                            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Variable: {0} reconocida como Numerico", _myRule.Value))
                            Dim iValues As Int32 = 0
                            iValues = Int32.Parse(objValue)

                            For count As Int32 = 1 To iValues Step 1

                                If VariablesInterReglas.ContainsKey(_myRule.Name) = False Then
                                    VariablesInterReglas.Add(_myRule.Name, count, False)
                                Else
                                    VariablesInterReglas.Item(_myRule.Name) = count
                                End If

                                For Each r As IRule In _myRule.ChildRules
                                    WFRB = New WFRulesBusiness
                                    results = WFRB.ExecuteRule(r.ID, _myRule.WFStepId, results, False, refreshTask)
                                Next

                            Next

                        Case "int64"
                            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Variable: {0} reconocida como Numero", _myRule.Value))
                            Dim iValues As Int64
                            iValues = Int64.Parse(objValue)

                            For count As Int64 = 1 To iValues Step 1


                                If VariablesInterReglas.ContainsKey(_myRule.Name) = False Then
                                    VariablesInterReglas.Add(_myRule.Name, count, False)
                                Else
                                    VariablesInterReglas.Item(_myRule.Name) = count
                                End If

                                For Each r As IRule In _myRule.ChildRules
                                    WFRB = New WFRulesBusiness
                                    results = WFRB.ExecuteRule(r.ID, _myRule.WFStepId, results, False, refreshTask)
                                Next
                            Next

                        Case "decimal"
                            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Variable: {0} reconocida como Decimal", _myRule.Value))
                            Dim dValues As Decimal
                            dValues = Decimal.Parse(objValue)


                            For count As Int64 = 1 To dValues Step 1

                                If VariablesInterReglas.ContainsKey(_myRule.Name) = False Then
                                    VariablesInterReglas.Add(_myRule.Name, count, False)
                                Else
                                    VariablesInterReglas.Item(_myRule.Name) = count
                                End If
                                WFRB = New WFRulesBusiness
                                For Each r As IRule In _myRule.ChildRules
                                    results = WFRB.ExecuteRule(r.ID, _myRule.WFStepId, results, False, refreshTask)
                                Next
                            Next

                        Case "string"
                            ZTrace.WriteLineIf(ZTrace.IsInfo, String.Format("Variable: {0} reconocida como texto", _myRule.Value))
                            Dim value As Int64

                            If Int64.TryParse(objValue.ToString().Trim(), value) Then

                                For count As Int64 = 1 To value Step 1

                                    If VariablesInterReglas.ContainsKey(_myRule.Name) = False Then
                                        VariablesInterReglas.Add(_myRule.Name, count, False)
                                    Else
                                        VariablesInterReglas.Item(_myRule.Name) = count
                                    End If

                                    For Each r As IRule In _myRule.ChildRules
                                        WFRB = New WFRulesBusiness
                                        results = WFRB.ExecuteRule(r.ID, _myRule.WFStepId, results, False, refreshTask)
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

                If VariablesInterReglas.ContainsKey(_myRule.Name) = True Then
                    VariablesInterReglas.Remove(_myRule.Name)
                End If

            Else
                If IsNumeric(_myRule.Value) = True Then

                    '[sebastian 15-04-2009] en caso de que se le haya pasado como variable un numero 
                    'se iterar tantas veces como diga ese numero
                    Dim iValues As Int32 = 0
                    iValues = Int32.Parse(_myRule.Value)

                    For count As Int32 = 1 To iValues Step 1
                        If VariablesInterReglas.ContainsKey(_myRule.Name) = False Then
                            VariablesInterReglas.Add(_myRule.Name, count, False)
                        Else
                            VariablesInterReglas.Item(_myRule.Name) = count
                        End If

                        For Each r As IRule In _myRule.ChildRules
                            WFRB = New WFRulesBusiness
                            results = WFRB.ExecuteRule(r.ID, _myRule.WFStepId, results, False, refreshTask)
                        Next
                    Next
                End If
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Fin del recorrido.")
        Finally

            objValue = Nothing
            ObjectType = Nothing
            dsValues = Nothing
            WFRB = Nothing
            dtValues = Nothing
            arValues = Nothing
            values = Nothing
        End Try
        Return New List(Of ITaskResult)
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

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
