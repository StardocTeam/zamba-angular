Imports Zamba.Core.DocTypes.DocAsociated

Public Class PlayIfDocAsocExist
    Private Objetos() As String
    Private IndiceData As String
    Private Comparator As String
    Private Value As String
    Private idComparator As Int32
    Private indexID As Int64
    Private TodosLosIndicesValidos As Boolean
    Private HayUnIndiceValido As Boolean
    Private myRule As IIfDocAsocExist
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim S As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            For Each R As TaskResult In results
                Dim blnAsoc As Boolean

                For Each AsocResult As Core.Result In DocAsociatedBusiness.getAsociatedResultsFromResult(R)
                    If AsocResult.DocTypeId = myRule.TipoDeDocumento Then
                        HayUnIndiceValido = False
                        TodosLosIndicesValidos = True

                        'todo validar indices
                        For Each Condicion As String In myRule.Condiciones.Split("*")
                            ' Se Toma IdCampoIndice, operador y valor...
                            Objetos = Condicion.Split("|")

                            indexID = Objetos(0)

                            For i As Int64 = 0 To AsocResult.Indexs.Count - 1
                                If indexID = AsocResult.Indexs(i).ID Then
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado el atributo")
                                    '(pablo) - 31032011 - se cambia el valor "1" del indice a "true"
                                    'para que posteriormente pueda ser validado por el metodo Compare
                                    Dim IndexValue As String = String.Empty

                                    If AsocResult.Indexs(i).Type = "9" Then
                                        IndexValue = AsocResult.Indexs(i).Data
                                        If AsocResult.Indexs(i).Data = "1" Then
                                            IndexValue = "True"
                                        Else
                                            IndexValue = "False"
                                        End If
                                    End If

                                    If IndexValue = String.Empty Then
                                        IndexValue = AsocResult.Indexs(i).Data
                                    End If
                                    IndiceData = String.Empty
                                    If Not IsNothing(AsocResult.Indexs(i).Data) Then IndiceData = AsocResult.Indexs(i).Data

                                    ' Toma el Comparador...
                                    idComparator = CType(Objetos(1), System.Int32)

                                    ' Toma el Valor esperado...
                                    Value = Objetos(2)
                                    'Convierto texto inteligente y ZVar

                                    Value = TextoInteligente.ReconocerCodigo(Value, R).Trim()
                                    If Value.ToString.ToLower.Contains("zvar") Then
                                        Value = WFRuleParent.ReconocerVariablesValuesSoloTexto(Value)
                                    End If

                                    'entra por AND
                                    If myRule.OperatorAND Then
                                        If Not IndexsBusiness.CompareIndex(IndexValue, AsocResult.Indexs(i).Type, idComparator, Value, False) Then
                                            TodosLosIndicesValidos = False
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No Valido!")
                                            Exit For
                                        End If
                                    Else
                                        'Entre por OR
                                        If IndexsBusiness.CompareIndex(IndexValue, AsocResult.Indexs(i).Type, idComparator, Value, False) Then
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se cumple la condicion")
                                            HayUnIndiceValido = True
                                            Exit For
                                        Else
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se cumple la condicion")
                                        End If
                                    End If
                                    Exit For
                                End If
                            Next
                        Next

                        ' Si se cumplen las validaciones de los campos...
                        ' Dependiendo del operador "And" o "Or" agrega el resultItem
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "OperadorAnd " & myRule.OperatorAND)
                        If myRule.OperatorAND = True Then
                            If TodosLosIndicesValidos Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El documento asociado ha sido encontrado.")
                                blnAsoc = True
                                Exit For
                            End If
                        Else
                            If HayUnIndiceValido Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "El documento asociado ha sido encontrado.")
                                blnAsoc = True
                                Exit For
                            End If
                        End If
                    End If
                Next

                If myRule.Existencia = True AndAlso blnAsoc = ifType Then
                    S.Add(R)
                ElseIf myRule.Existencia = False AndAlso blnAsoc = ifType Then
                    S.Add(R)
                End If
            Next

        Finally
            Objetos = Nothing
        End Try

        Return S
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IIfDocAsocExist)
        myRule = rule
    End Sub
End Class
