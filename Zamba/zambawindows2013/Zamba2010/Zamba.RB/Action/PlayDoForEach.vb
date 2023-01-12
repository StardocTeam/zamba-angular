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
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal refreshTask As List(Of Int64)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Me.objValue = WFRuleParent.ObtenerValorVariableObjeto(Me._myRule.Value)

            If Not objValue Is Nothing Then
                'Se pregunta si no es string porque lo detecta como IEnumerable.
                If TypeOf (objValue) Is IEnumerable AndAlso Not (TypeOf (objValue) Is String) Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "Variable reconocida como IEnumerable")
                    Me.values = objValue
                    For Each item As Object In values
                        If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                            VariablesInterReglas.Add(Me._myRule.Name, item, False)
                        Else
                            VariablesInterReglas.Item(Me._myRule.Name) = item
                        End If

                        For Each r As IRule In Me._myRule.ChildRules
                            Me.WFRB = New WFRulesBusiness
                            results = WFRB.ExecuteRule(r.ID, Me._myRule.WFStepId, results, False, refreshTask)
                        Next
                    Next

                Else
                    Me.ObjectType = objValue.GetType
                    Select Case ObjectType.Name.ToLower()

                        Case "dataset"
                            Trace.WriteLineIf(ZTrace.IsInfo, "Variable reconocida como DataSet")
                            Me.dsValues = DirectCast(objValue, DataSet)

                            For Each row As DataRow In dsValues.Tables(0).Rows

                                If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                                    VariablesInterReglas.Add(Me._myRule.Name, row, False)
                                Else
                                    VariablesInterReglas.Item(Me._myRule.Name) = row
                                End If

                                For Each r As IRule In Me._myRule.ChildRules
                                    Me.WFRB = New WFRulesBusiness
                                    results = WFRB.ExecuteRule(r.ID, Me._myRule.WFStepId, results, False, refreshTask)
                                Next
                            Next

                        Case "datatable"
                            Trace.WriteLineIf(ZTrace.IsInfo, "Variable reconocida como DataTable")
                            Me.dtValues = DirectCast(objValue, DataTable)

                            For Each row As DataRow In dtValues.Rows
                                If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                                    VariablesInterReglas.Add(Me._myRule.Name, row, False)
                                Else
                                    VariablesInterReglas.Item(Me._myRule.Name) = row
                                End If

                                For Each r As IRule In Me._myRule.ChildRules
                                    Me.WFRB = New WFRulesBusiness
                                    results = WFRB.ExecuteRule(r.ID, Me._myRule.WFStepId, results, False, refreshTask)
                                Next
                            Next

                        Case "int32"
                            Trace.WriteLineIf(ZTrace.IsInfo, "Variable reconocida como Int32")
                            Dim iValues As Int32 = 0
                            iValues = Int32.Parse(objValue)

                            For count As Int32 = 1 To iValues Step 1

                                If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                                    VariablesInterReglas.Add(Me._myRule.Name, count, False)
                                Else
                                    VariablesInterReglas.Item(Me._myRule.Name) = count
                                End If

                                For Each r As IRule In Me._myRule.ChildRules
                                    Me.WFRB = New WFRulesBusiness
                                    results = WFRB.ExecuteRule(r.ID, Me._myRule.WFStepId, results, False, refreshTask)
                                Next

                            Next

                        Case "int64"
                            Trace.WriteLineIf(ZTrace.IsInfo, "Variable reconocida como Int64")
                            Dim iValues As Int64
                            iValues = Int64.Parse(objValue)

                            For count As Int64 = 1 To iValues Step 1


                                If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                                    VariablesInterReglas.Add(Me._myRule.Name, count, False)
                                Else
                                    VariablesInterReglas.Item(Me._myRule.Name) = count
                                End If

                                For Each r As IRule In Me._myRule.ChildRules
                                    Me.WFRB = New WFRulesBusiness
                                    results = WFRB.ExecuteRule(r.ID, Me._myRule.WFStepId, results, False, refreshTask)
                                Next
                            Next

                        Case "decimal"
                            Trace.WriteLineIf(ZTrace.IsInfo, "Variable reconocida como Decimal")
                            Dim dValues As Decimal
                            dValues = Decimal.Parse(objValue)


                            For count As Int64 = 1 To dValues Step 1

                                If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                                    VariablesInterReglas.Add(Me._myRule.Name, count, False)
                                Else
                                    VariablesInterReglas.Item(Me._myRule.Name) = count
                                End If
                                Me.WFRB = New WFRulesBusiness
                                For Each r As IRule In Me._myRule.ChildRules
                                    results = WFRB.ExecuteRule(r.ID, Me._myRule.WFStepId, results, False, refreshTask)
                                Next
                            Next

                        Case "string"
                            Trace.WriteLineIf(ZTrace.IsInfo, "Variable reconocida como String")
                            Dim value As Int64

                            If Int64.TryParse(objValue.ToString().Trim(), value) Then

                                For count As Int64 = 1 To value Step 1

                                    If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                                        VariablesInterReglas.Add(Me._myRule.Name, count, False)
                                    Else
                                        VariablesInterReglas.Item(Me._myRule.Name) = count
                                    End If

                                    For Each r As IRule In Me._myRule.ChildRules
                                        Me.WFRB = New WFRulesBusiness
                                        results = WFRB.ExecuteRule(r.ID, Me._myRule.WFStepId, results, False, refreshTask)
                                    Next
                                Next


                            End If

                        Case Else
                            Trace.WriteLineIf(ZTrace.IsInfo, "Tipo de dato ")
                            'Case "list`1"
                            '[sebastian 15-04-2009] Look for the correct type of generic list and returns
                            'the result complete
                            '[Ezequiel] - 21/09/09 - Se cambio la llamada al metodo "LookForTypeOfGenericList" porque se unifico utilizando
                            '                        la interfaz ienumerable.
                            'results = LookForTypeOfGenericList(results, objValue)
                    End Select
                End If

                If VariablesInterReglas.ContainsKey(Me._myRule.Name) = True Then
                    VariablesInterReglas.Remove(Me._myRule.Name)
                End If

            Else
                If IsNumeric(Me._myRule.Value) = True Then

                    '[sebastian 15-04-2009] en caso de que se le haya pasado como variable un numero 
                    'se iterar tantas veces como diga ese numero
                    Dim iValues As Int32 = 0
                    iValues = Int32.Parse(Me._myRule.Value)

                    For count As Int32 = 1 To iValues Step 1
                        If VariablesInterReglas.ContainsKey(Me._myRule.Name) = False Then
                            VariablesInterReglas.Add(Me._myRule.Name, count, False)
                        Else
                            VariablesInterReglas.Item(Me._myRule.Name) = count
                        End If

                        For Each r As IRule In Me._myRule.ChildRules
                            Me.WFRB = New WFRulesBusiness
                            results = WFRB.ExecuteRule(r.ID, Me._myRule.WFStepId, results, False, refreshTask)
                        Next
                    Next
                End If
            End If
            Trace.WriteLineIf(ZTrace.IsInfo, "Fin del recorrido.")
        Finally

            Me.objValue = Nothing
            Me.ObjectType = Nothing
            Me.dsValues = Nothing
            Me.WFRB = Nothing
            Me.dtValues = Nothing
            Me.arValues = Nothing
            Me.values = Nothing
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
