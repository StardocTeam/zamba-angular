Public Class PlayIfValidateVar
    Private _myRule As IIfValidateVar
    Private zValue As String
    Private zvar As String
    Private ds As DataSet
    Private rulevalue As String
    Private dt As DataTable
    Private dstest As DataSet

    Sub New(ByVal rule As IIfValidateVar)
        _myRule = rule
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="myrule"></param>
    ''' <param name="r"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateVar(ByVal r As Result) As Boolean

        zValue = String.Empty
        zvar = String.Empty

        If _myRule.TxtVar.IndexOf("zvar(", StringComparison.CurrentCultureIgnoreCase) <> -1 Then
            zvar = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.TxtVar)
            zvar = TextoInteligente.ReconocerCodigo(zvar, r)
            If (zvar.IndexOf("zvar(", StringComparison.CurrentCultureIgnoreCase) <> -1) Then
                zvar = zvar.Replace("zvar(", String.Empty)
                If (zvar.IndexOf("))", StringComparison.CurrentCultureIgnoreCase) <> -1) Then
                    zvar = zvar.Replace("))", ")").Trim()
                Else
                    zvar = zvar.Replace(")", String.Empty).Trim()
                End If
            End If
        Else
            zvar = _myRule.TxtVar
        End If
        If (zvar <> String.Empty) Then
            zvar = TextoInteligente.ReconocerCodigo(zvar, r)
        End If

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable original: " & zvar & " Variable pasada por textointeligente: " & zvar)
        zvar = zvar.Trim()

        If VariablesInterReglas.ContainsKey(zvar) Then
            If (TypeOf (VariablesInterReglas.Item(zvar)) Is DataSet) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable DataSet")
                ds = VariablesInterReglas.Item(zvar)

                If IsNothing(ds) Then
                    zValue = String.Empty
                ElseIf IsDBNull(ds) Then
                    zValue = String.Empty
                ElseIf ds.Tables.Count <= 0 Then
                    zValue = String.Empty
                ElseIf ds.Tables(0).Rows.Count = 1 AndAlso ds.Tables(0).Columns.Count > 1 Then
                    zValue = ds.Tables(0).Rows(0)(1).ToString()
                ElseIf ds.Tables(0).Rows.Count > 0 AndAlso Not IsDBNull(ds.Tables(0).Rows(0)) Then
                    zValue = "Dataset Con Valores"
                Else
                    zValue = String.Empty
                End If
            ElseIf (VariablesInterReglas.Item(zvar)) Is Nothing OrElse IsDBNull(VariablesInterReglas.Item(zvar)) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable Vacia")
                zValue = String.Empty
            ElseIf (TypeOf (VariablesInterReglas.Item(zvar)) Is DataTable) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable Datatable")
                dt = VariablesInterReglas.Item(zvar)

                If Not IsNothing(dt) AndAlso Not IsDBNull(dt) AndAlso dt.Rows.Count > 0 AndAlso Not IsDBNull(dt.Rows(0)) Then
                    zValue = dt.Rows(0)(0).ToString() '.Tables(0).Rows(0)(1).ToString()
                Else
                    'Return 0
                    zValue = String.Empty
                End If
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable de otro tipo")
                zValue = VariablesInterReglas.Item(zvar)
            End If
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha encontrado la variable")
            zValue = zvar
        End If

        rulevalue = String.Empty
        rulevalue = WFRuleParent.ReconocerVariablesValuesSoloTexto(_myRule.TxtValue)

        rulevalue = TextoInteligente.ReconocerCodigo(rulevalue, r)

        If Not String.IsNullOrEmpty(rulevalue) Then
            rulevalue = rulevalue.Trim()
        End If
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable Obtenida: " & zValue & " Operador: " & _myRule.Operador.ToString() & " Variable a comparar: " & Chr(34) & rulevalue & Chr(34))

        If String.IsNullOrEmpty(rulevalue) Then
            rulevalue = _myRule.TxtValue
        End If
        Return ToolsBusiness.ValidateComp(zValue, rulevalue, _myRule.Operador, _myRule.CaseInsensitive)
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)()
        Try

            For Each R As TaskResult In results
                If ValidateVar(R) = ifType Then
                    If ifType = True Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se cumple la condicion")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se cumple la condicion")
                    End If
                    NewList.Add(R)
                Else
                    If ifType = True Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se cumple la condicion")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Se cumple la condicion")
                    End If
                End If
            Next
        Finally
            zValue = String.Empty
            zvar = String.Empty
            ds = Nothing
        End Try
        Return NewList
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)


        Return WFRuleParent.ReconocerZvar(_myRule.TxtValue)

    End Function
End Class