Imports System.Reflection

Public Class PlayDoDismemberObject

    Private _myRule As IDoDismemberObject
    Private _objectName As String
    Private objetoObtenido As Object
    Private tempprop As Object
    Private indexs As List(Of Int64)
    Private value As Object
    Private FlagDirect As Boolean
    Private prop As PropertyInfo
    Private Props As String()
    Private Index As String()
    Private FirstProp As Object
    Private newprop As Object
    Private nombreIndice As String
    Private i As Int32

    Sub New(ByVal rule As IDoDismemberObject)
        _myRule = rule
    End Sub

    Private Function GetProperty(ByVal PropertyName As String, ByVal ObjectClass As Object) As Object
        Dim newprop As PropertyInfo
        newprop = ObjectClass.GetType().GetProperty(PropertyName)
        Return newprop
    End Function
    Public Function Play(ByVal results As List(Of ITaskResult)) As List(Of Core.ITaskResult)
        Try

            _objectName = _myRule.ObjectName
            If _objectName.ToLower.Contains("zvar") Then
                _objectName = _objectName.Replace("zvar(", String.Empty).Replace(")", String.Empty)
                'Se vuelve a validar por si el usuario escribio mal "zvar"
                If _objectName.ToLower.Contains("zvar") Then
                    _objectName = _objectName.Remove(0, 5).Replace(")", String.Empty)
                End If
            End If

            objetoObtenido = WFRuleParent.ObtenerValorVariableObjeto(_objectName)
            tempprop = Nothing

            value = Nothing
            If Not IsNothing(objetoObtenido) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Procesando objeto " & _objectName & ". Tipo " & objetoObtenido.GetType().ToString())
                For Each CurrentResult As ITaskResult In results
                    indexs = New List(Of Int64)()

                    Try
                        For Each parent As IDoDismemberObject.IZvarVariable In _myRule.Zvars

                            FlagDirect = New Boolean
                            prop = Nothing

                            If (parent.PropertyName.IndexOf(".") <> -1) Then
                                Props = parent.PropertyName.Split(".")
                                Index = Nothing
                                If Props(0).Contains("(") Then
                                    Index = Props(0).Split("(")
                                    Index(1) = Index(1).Replace(")", String.Empty)
                                End If
                                FirstProp = Nothing
                                If Not IsNothing(Index) Then
                                    FirstProp = objetoObtenido.GetType().GetProperty(Index(0))
                                    FirstProp = FirstProp.GetValue(objetoObtenido, Nothing)
                                    If IsNumeric(Index(1)) Then
                                        tempprop = FirstProp(Int32.Parse(Index(1)))
                                    Else
                                        tempprop = FirstProp(Index(1))
                                    End If
                                Else
                                    tempprop = objetoObtenido.GetType().GetProperty(Props(0))
                                    tempprop = tempprop.GetValue(objetoObtenido, Nothing)
                                End If

                                i = New Int32
                                For i = 1 To Props.Length - 1
                                    newprop = New Object
                                    If Props(i).Contains("(") Then
                                        Index = Props(i).Split("(")
                                        Index(1) = Index(1).Replace(")", String.Empty)
                                        newprop = GetProperty(Index(0), tempprop)
                                        newprop = newprop.GetValue(tempprop, Nothing)
                                        If IsNumeric(Index(1)) Then
                                            newprop = newprop(Int32.Parse(Index(1)))
                                        Else
                                            newprop = newprop(Index(1))
                                        End If
                                    Else
                                        newprop = GetProperty(Props(i), tempprop)
                                        newprop = newprop.GetValue(tempprop, Nothing)
                                    End If
                                    tempprop = newprop
                                Next
                                FlagDirect = True
                            Else
                                If parent.PropertyName.Contains("(") Then
                                    Index = parent.PropertyName.Split("(")
                                    Index(1) = Index(1).Replace(")", String.Empty)
                                End If
                                If Not IsNothing(Index) Then
                                    tempprop = objetoObtenido.GetType().GetProperty(Index(0))
                                    tempprop = tempprop.GetValue(objetoObtenido, Nothing)
                                    If IsNumeric(Index(1)) Then
                                        tempprop = tempprop(Int32.Parse(Index(1)))
                                    Else
                                        tempprop = tempprop(Index(1))
                                    End If
                                Else
                                    tempprop = objetoObtenido.GetType().GetProperty(parent.PropertyName)
                                    tempprop = tempprop.GetValue(objetoObtenido, Nothing)
                                End If
                            End If

                            value = tempprop

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Propiedad: " & parent.PropertyName() & " Valor obtenido: " & value.ToString())
                            If parent.ZvarName.Contains("<<") Then
                                nombreIndice = parent.ZvarName.Split("(")(1)
                                nombreIndice = nombreIndice.Split(")")(0)
                                For Each indice As Index In CurrentResult.Indexs
                                    If indice.Name = nombreIndice Then
                                        indice.Data = value
                                        indice.DataTemp = value
                                        indexs.Add(indice.ID)
                                    End If
                                Next
                            Else
                                If VariablesInterReglas.ContainsKey(parent.ZvarName) = False Then
                                    VariablesInterReglas.Add(parent.ZvarName, value, False)
                                Else
                                    VariablesInterReglas.Item(parent.ZvarName) = value
                                End If
                            End If
                        Next
                        'En caso de que el error sea porque la DoShowTable devolvio un solo valor
                    Catch ex As Exception
                        If _myRule.Zvars.Count = 1 AndAlso objetoObtenido.GetType().ToString.Contains("System.Int") AndAlso _myRule.Zvars(0).PropertyName().Contains("ItemArray") Then
                            value = objetoObtenido
                        Else
                            ZClass.raiseerror(ex)
                        End If
                    End Try
                    If indexs.Count > 0 Then
                        Dim rstBuss As New Results_Business()
                        rstBuss.SaveModifiedIndexData(CurrentResult, True, False, indexs)
                        rstBuss = Nothing

                    End If
                Next
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Objetos procesados con éxito!")
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El objeto esta vacio (Nothing)")
            End If
        Finally
            _objectName = Nothing
            objetoObtenido = Nothing
            tempprop = Nothing
            indexs = Nothing
            value = Nothing
            FlagDirect = Nothing
            prop = Nothing
            Props = Nothing
            Index = Nothing
            FirstProp = Nothing
            newprop = Nothing
            nombreIndice = Nothing
            i = Nothing
        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class

'Reconocer valor
'objetoloco = WFRuleParent.ReconocerVariablesObjeto(myrule.textboxdeasrriba)as object	

'Obtener propiedad
'Dim a As Object = objetoloco.GetType().GetProperty(listboxvalor).GetValue()

'Guardar en texto inteligente
'If myRule.HashTable.Contains("<<") Then
'Dim nombreIndice As String = myRule.HashTable.Split("(")(1)
'                    nombreIndice = nombreIndice.Split(")")(0)
'                    For Each indice As Index In r.Indexs
'                        If indice.Name = nombreIndice Then
'                            indice.Data = resultado
'                            indice.DataTemp = resultado
'                        End If
'                    Next
'                End If

'Para guardar en zvar

'If VariablesInterReglas.ContainsKey(myRule.HashTable) = False Then
'                    VariablesInterReglas.Add(myRule.HashTable, resultado)
'                Else
'                    VariablesInterReglas.Item(myRule.HashTable) = resultado
'                End If
