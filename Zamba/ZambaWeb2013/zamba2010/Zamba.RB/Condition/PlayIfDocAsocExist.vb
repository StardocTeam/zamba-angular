Imports Zamba.Core
Imports Zamba.Data
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
    
                For Each AsocResult As Core.Result In DocAsociatedBusiness.getAsociatedResultsFromResult(R, Int32.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100)))
                    If AsocResult.DocTypeId = myrule.TipoDeDocumento Then
                        Me.HayUnIndiceValido = False
                        Me.TodosLosIndicesValidos = True

                        'todo validar indices
                        For Each Condicion As String In myrule.Condiciones.Split("*")
                            ' Se Toma IdCampoIndice, operador y valor...
                            Me.Objetos = Condicion.Split("|")

                            indexID = Me.Objetos(0)

                            For i As Int64 = 0 To AsocResult.Indexs.Count - 1
                                If indexID = AsocResult.Indexs(i).ID Then
                                    Trace.WriteLineIf(ZTrace.IsInfo, "Se ha encontrado el atributo")
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
                                    Me.IndiceData = String.Empty
                                    If Not IsNothing(AsocResult.Indexs(i).Data) Then Me.IndiceData = AsocResult.Indexs(i).Data

                                    ' Toma el Comparador...
                                    Me.idComparator = CType(Me.Objetos(1), System.Int32)

                                    ' Toma el Valor esperado...
                                    Me.Value = Me.Objetos(2)
                                    'Convierto texto inteligente y ZVar

                                    Me.Value = Zamba.Core.TextoInteligente.ReconocerCodigo(Me.Value, R).Trim()
                                    If Me.Value.ToString.ToLower.Contains("zvar") Then
                                        Me.Value = WFRuleParent.ReconocerVariablesValuesSoloTexto(Me.Value)
                                    End If

                                    'entra por AND
                                    If myrule.OperatorAND Then
                                        If Not IndexsBusiness.CompareIndex(IndexValue, AsocResult.Indexs(i).Type, Me.idComparator, Me.Value) Then
                                            Me.TodosLosIndicesValidos = False
                                            Trace.WriteLineIf(ZTrace.IsInfo, "No Valido!")
                                            Exit For
                                        End If
                                    Else
                                        'Entre por OR
                                        If IndexsBusiness.CompareIndex(IndexValue, AsocResult.Indexs(i).Type, Me.idComparator, Me.Value) Then
                                            Trace.WriteLineIf(ZTrace.IsInfo, "Se cumple la condicion")
                                            Me.HayUnIndiceValido = True
                                            Exit For
                                        Else
                                            Trace.WriteLineIf(ZTrace.IsInfo, "No se cumple la condicion")
                                        End If
                                    End If
                                    Exit For
                                End If
                            Next
                        Next

                        ' Si se cumplen las validaciones de los campos...
                        ' Dependiendo del operador "And" o "Or" agrega el resultItem
                        Trace.WriteLineIf(ZTrace.IsInfo, "OperadorAnd " & myrule.OperatorAND)
                        If myrule.OperatorAND = True Then
                            If TodosLosIndicesValidos Then
                                Trace.WriteLineIf(ZTrace.IsInfo, "El documento asociado ha sido encontrado.")
                                blnAsoc = True
                                Exit For
                            End If
                        Else
                            If Me.HayUnIndiceValido Then
                                Trace.WriteLineIf(ZTrace.IsInfo, "El documento asociado ha sido encontrado.")
                                blnAsoc = True
                                Exit For
                            End If
                        End If
                    End If
                Next

                If myrule.Existencia = True AndAlso blnAsoc = ifType Then
                    S.Add(R)
                ElseIf myrule.Existencia = False AndAlso blnAsoc = ifType Then
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
        Me.myRule = rule
    End Sub
End Class
