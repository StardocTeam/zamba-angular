Imports Zamba.WorkFlow.Business

Public Class PlayDoConsumeWCF

    Private _myRule As IDoConsumeWCF
    Private serviceResult As Object
    Private dynamicWCF As DynamicWCF ' DynamicWebservice

    Sub New(ByVal rule As IDoConsumeWCF)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)

        Try
            For Each r As TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Servicio: " & _myRule.Wsdl & " metodo invocado: " & _myRule.MethodName)
                Dim Parameters(_myRule.Param.Count - 1) As Object

                'Reconozco zvar y texto inteligente
                Dim i As Int16
                For Each value As Object In _myRule.Param
                    'value = WFRuleParent.ReconocerVariablesAsObject(value)
                    'If value.ToString().Contains("<<") Then
                    If value.ToString().ToUpper().Contains("NOTHING") Then
                        value = "NOTHING"
                    Else
                        value = TextoInteligente.ReconocerCodigo(value.ToString().Replace("byref", String.Empty), r).Trim()
                        value = WFRuleParent.ReconocerVariablesAsObject(value)
                    End If
                    'Else
                    'value = WFRuleParent.ReconocerVariablesAsObject(value)
                    'End If
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Parametro Valor decodificado: " & value.ToString())
                    Parameters.SetValue(value, i)
                    i = i + 1
                Next

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando Servicio...")
                dynamicWCF = New WorkFlow.Business.DynamicWCF

                'Seteo timeOut en GetWCFInstance()
                serviceResult = dynamicWCF.ConsumeWCF(_myRule.Wsdl, _myRule.Contract, _myRule.MethodName, Parameters, _myRule.useCredentials)

                dynamicWCF = Nothing

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Procesando resultados obtenidos...")
                'Agregado para guardar, ya sea en variable o en texto inteligente, lo que devuelven los 
                'parámetros ByRef - MNP
                i = 0

                For Each value As String In _myRule.Param
                    If value.Contains("zvar") = True Then
                        value = value.Replace("zvar(", String.Empty)
                        value = value.Replace(")", String.Empty)

                        VariablesInterReglas.Item(value) = Parameters(i)
                    ElseIf value.ToLower().Contains("byref") = True And value.Contains("<<") Then

                        TextoInteligente.AsignItemFromSmartText(value, r, Parameters(i).ToString)
                    Else
                        If VariablesInterReglas.ContainsKey("Param" & i) = False Then

                            VariablesInterReglas.Add("Param" & i, Parameters(i), False)
                        Else

                            VariablesInterReglas.Item("Param" & i) = Parameters(i)
                        End If
                    End If
                    i = i + 1
                Next

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando valores en: " & _myRule.SaveInValue)
                'Guarda valor en variable
                If _myRule.SaveInValue.Contains("<<") = False Then
                    If IsNothing(serviceResult) Then
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "El resultado es nulo")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Resultado obtenido: " & serviceResult.ToString())
                    End If
                    If VariablesInterReglas.ContainsKey(_myRule.SaveInValue) = False Then
                        VariablesInterReglas.Add(_myRule.SaveInValue, serviceResult, False)

                    Else
                        VariablesInterReglas.Item(_myRule.SaveInValue) = serviceResult

                    End If
                Else
                    'se agrego esto para poder reconocer el texto inteligente y asignar a la variable del texto
                    'inteligente el resulta de la regla.[sebastián]
                    TextoInteligente.AsignItemFromSmartText(_myRule.SaveInValue, results(0), serviceResult)
                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultados procesados con éxito.")
                Parameters = Nothing
                i = Nothing
            Next
        Finally
            dynamicWCF = Nothing
            serviceResult = Nothing
        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
