Imports Zamba.WorkFlow.Business

Public Class PlayDoConsumeWebService

    Private _myRule As IDoConsumeWebService
    Private serviceResult As Object
    Private dynamicWebService As DynamicWebservice

    Sub New(ByVal rule As IDoConsumeWebService)
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
                    value = WFRuleParent.ReconocerVariablesAsObject(value)
                    If value.ToString().Contains("<<") Then
                        If value.ToString().ToUpper().Contains("NOTHING") Then
                            value = "NOTHING"
                        Else
                            value = WFRuleParent.ReconocerVariablesAsObject(value)
                            value = TextoInteligente.ReconocerCodigo(value.ToString().Replace("byref", String.Empty), r).Trim()
                        End If
                    Else
                        value = WFRuleParent.ReconocerVariablesAsObject(value)
                    End If
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Parametro Valor decodificado: " & value.ToString())
                    Parameters.SetValue(value, i)
                    i = i + 1
                Next

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando Servicio...")
                dynamicWebService = New WorkFlow.Business.DynamicWebservice

                Dim sTimeOutValue As String = ZOptBusiness.GetValue("DoConsumeWebServiceTimeOut")

                Dim timeOutValue As Integer
                If Not Integer.TryParse(sTimeOutValue, timeOutValue) Then
                    timeOutValue = 60000
                    ZOptBusiness.Insert("DoConsumeWebServiceTimeOut", "60000")
                End If

                serviceResult = dynamicWebService.Consume(_myRule.Wsdl, _myRule.MethodName, Parameters, _myRule.useCredentials, timeOutValue)
                dynamicWebService = Nothing

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Procesando resultados obtenidos...")
                'Agregado para guardar, ya sea en variable o en texto inteligente, lo que devuelven los 
                'parámetros ByRef - MNP
                i = 0

                For Each value As String In _myRule.Param
                    If value.Contains("zvar") = True Then
                        value = value.Replace("zvar(", String.Empty)
                        value = value.Replace(")", String.Empty)

                        VariablesInterReglas.Item(value) = If(Parameters(i) IsNot Nothing, Parameters(i), String.Empty)

                    ElseIf value.ToLower().Contains("byref") = True And value.Contains("<<") Then

                        TextoInteligente.AsignItemFromSmartText(value, r, If(Parameters(i) IsNot Nothing, Parameters(i).ToString(), String.Empty))
                    Else
                        If VariablesInterReglas.ContainsKey("Param" & i) = False Then

                            VariablesInterReglas.Add("Param" & i, If(Parameters(i) IsNot Nothing, Parameters(i), String.Empty), False)
                        Else

                            VariablesInterReglas.Item("Param" & i) = If(Parameters(i) IsNot Nothing, Parameters(i), String.Empty)
                        End If
                    End If
                    i = i + 1
                Next

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando valores en: " & _myRule.SaveInValue)
                'Guarda valor en variable
                If _myRule.SaveInValue.Contains("<<") = False Then
                    If IsNothing(serviceResult) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El resultado es nulo")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado obtenido: " & serviceResult.ToString())
                    End If
                    If VariablesInterReglas.ContainsKey(_myRule.SaveInValue) = False Then
                        VariablesInterReglas.Add(_myRule.SaveInValue, serviceResult, False)

                    Else
                        VariablesInterReglas.Item(_myRule.SaveInValue) = serviceResult

                    End If
                Else
                    'For Each r As ITaskResult In results
                    '    Dim nombreIndice As String = myRule.SaveInValue.Split("(")(1)
                    '    nombreIndice = nombreIndice.Split(")")(0)
                    '    For Each indice As Index In r.Indexs
                    '        If indice.Name = nombreIndice Then
                    '            indice.Data = b
                    '            indice.DataTemp = b
                    '        End If
                    '    Next
                    '    Newresults.Add(r)
                    'Next
                    'se agrego esto para poder reconocer el texto inteligente y asignar a la variable del texto
                    'inteligente el resulta de la regla.[sebastián]
                    TextoInteligente.AsignItemFromSmartText(_myRule.SaveInValue, results(0), serviceResult)

                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultados procesados con éxito.")
                Parameters = Nothing
                i = Nothing
            Next
        Finally
            dynamicWebService = Nothing
            serviceResult = Nothing
        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
