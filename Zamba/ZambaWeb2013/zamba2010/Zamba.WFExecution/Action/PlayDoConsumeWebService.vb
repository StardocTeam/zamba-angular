Imports Zamba.Core
Imports Zamba.WorkFlow.Business

Public Class PlayDoConsumeWebService

    Private _myRule As IDoConsumeWebService
    Private b As Object

    Sub New(ByVal rule As IDoConsumeWebService)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim VarInterReglas As VariablesInterReglas = Nothing
        Dim dnmWeb As New DynamicWebservice
        Dim i As Int16

        Try
            For Each r As TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name & ", Id " & r.TaskId)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del servicio: " & Me._myRule.Wsdl)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre de método invocado: " & Me._myRule.MethodName)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Valores de los parámetros: ")
                Dim Parameters(Me._myRule.Param.Count - 1) As Object
                'Reconozco zvar y texto inteligente

                i = 0
                For Each value As Object In Me._myRule.Param
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor: " & value.ToString())
                    VarInterReglas = New VariablesInterReglas()
                    value = VarInterReglas.ReconocerVariablesAsObject(value)

                    If value.ToString().Contains("<<") Then
                        If value.ToString().ToUpper().Contains("NOTHING") Then
                            value = "NOTHING"
                        Else
                            value = VarInterReglas.ReconocerVariablesAsObject(value)
                            value = Zamba.Core.TextoInteligente.ReconocerCodigo(value.ToString().Replace("byref", ""), r).Trim()
                        End If
                    Else
                        value = VarInterReglas.ReconocerVariablesAsObject(value)
                    End If

                    VarInterReglas = Nothing
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor decodificado: " & value.ToString())
                    Parameters.SetValue(value, i)
                    i = i + 1
                    ZTrace.WriteLineIf(ZTrace.IsInfo, value)
                Next

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando Servicio...")
                Me.b = Nothing
                dnmWeb = New DynamicWebservice()
                Me.b = dnmWeb.Consume(Me._myRule.Wsdl, Me._myRule.MethodName, Parameters, _myRule.useCredentials)
                dnmWeb = Nothing

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Fin de la ejecución del Servicio.")
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Procesando resultados obtenidos...")
                'Agregado para guardar, ya sea en variable o en texto inteligente, lo que devuelven los 
                'parámetros ByRef - MNP
                i = 0
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando si existen valores para guardar en variables o texto inteligente...")
                For Each value As String In Me._myRule.Param
                    If value.Contains("zvar") = True Then
                        value = value.Replace("zvar(", String.Empty)
                        value = value.Replace(")", String.Empty)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando variable: " & value)
                        VariablesInterReglas.Item(value) = Parameters(i)
                    ElseIf value.ToLower().Contains("byref") = True And value.Contains("<<") Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando texto inteligente: " & value)
                        TextoInteligente.AsignItemFromSmartText(value, r, Parameters(i).ToString)
                    Else
                        If VariablesInterReglas.ContainsKey("Param" & i) = False Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando un nuevo parámetro")
                            VariablesInterReglas.Add("Param" & i, Parameters(i))
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificando un parámetro existente")
                            VariablesInterReglas.Item("Param" & i) = Parameters(i)
                        End If
                    End If
                    i = i + 1
                Next

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando valores en: " & Me._myRule.SaveInValue)
                'Guarda valor en variable
                If Me._myRule.SaveInValue.Contains("<<") = False Then
                    If IsNothing(b) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "El resultado es nulo")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado obtenido: " & b.ToString())
                    End If
                    If VariablesInterReglas.ContainsKey(Me._myRule.SaveInValue) = False Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando una nueva variable...")
                        VariablesInterReglas.Add(Me._myRule.SaveInValue, b)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Variables agregadas con éxito!")
                    Else
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Actualizando variables...")
                        VariablesInterReglas.Item(Me._myRule.SaveInValue) = b
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Variables actualizadas con éxito!")
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
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando valor en texto inteligente...")
                    TextoInteligente.AsignItemFromSmartText(Me._myRule.SaveInValue, results(0), b)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor guardado con éxito!")
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultados procesados con éxito.")
                Parameters = Nothing
            Next
        Finally
            If VarInterReglas IsNot Nothing Then
                VarInterReglas = Nothing
            End If
            Me.b = Nothing
        End Try
        Return results
    End Function
End Class
