Imports System.Collections.Generic
Imports System.Reflection

Public Class PlayDoDismemberObject

    Private Const parAbre As String = "("
    Private Const parCierra As String = ")"
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
        Me._myRule = rule
    End Sub

    Private Function GetProperty(ByVal PropertyName As String, ByVal ObjectClass As Object) As Object
        Dim newprop As PropertyInfo
        newprop = ObjectClass.GetType().GetProperty(PropertyName)
        Return newprop
    End Function
    Public Function Play(ByVal results As List(Of ITaskResult)) As List(Of Core.ITaskResult)
        Dim dtModifiedIndex As DataTable = Nothing
        Dim Results_Business As New Results_Business

        Try

            Me._objectName = Me._myRule.ObjectName
            If Me._objectName.ToLower.Contains("zvar") Then
                Me._objectName = Me._objectName.Replace("zvar(", String.Empty).Replace(parCierra, String.Empty)
                'Se vuelve a validar por si el usuario escribio mal "zvar"
                If Me._objectName.ToLower.Contains("zvar") Then
                    Me._objectName = Me._objectName.Remove(0, 5).Replace(parCierra, String.Empty)
                End If
            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Procesando objeto " & Me._objectName)
            Me.objetoObtenido = WFRuleParent.ObtenerValorVariableObjeto(Me._objectName)
            Me.tempprop = Nothing
            Me.value = Nothing

            If Not IsNothing(objetoObtenido) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El objeto es del tipo: " & objetoObtenido.GetType().ToString())
                Dim setEmptyValue = New Boolean
                If objetoObtenido.GetType().ToString() = "System.Data.DataSet" Then
                    Dim auxObjetoObtenido As DataSet = CType(objetoObtenido, DataSet)
                    If auxObjetoObtenido.Tables(0).Rows.Count() = 0 Then
                        setEmptyValue = True
                    End If
                End If
                For j As Int32 = 0 To results.Count - 1

                    dtModifiedIndex = New DataTable()
                    dtModifiedIndex.Columns.Add("ID", GetType(Int64))
                    dtModifiedIndex.Columns.Add("OldValue", GetType(String))
                    dtModifiedIndex.Columns.Add("NewValue", GetType(String))

                    Me.indexs = New List(Of Int64)()

                    For Each parent As IDoDismemberObject.IZvarVariable In Me._myRule.Zvars
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Propiedad: " & parent.PropertyName())
                        Me.FlagDirect = New Boolean
                        Me.prop = Nothing

                        If (parent.PropertyName.IndexOf(".") <> -1) Then
                            Me.Props = parent.PropertyName.Split(".")
                            Me.Index = Nothing
                            If Props(0).Contains(parAbre) Then
                                Index = Props(0).Split(parAbre)
                                Index(1) = Index(1).Replace(parCierra, String.Empty)
                            End If
                            Me.FirstProp = Nothing
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

                            Me.i = New Int32
                            For i = 1 To Props.Length - 1
                                Me.newprop = New Object
                                If Props(i).Contains(parAbre) Then
                                    Index = Props(i).Split(parAbre)
                                    Index(1) = Index(1).Replace(parCierra, String.Empty)

                                    If setEmptyValue Then
                                        newprop = String.Empty
                                    Else
                                        newprop = GetProperty(Index(0), tempprop)
                                        newprop = newprop.GetValue(tempprop, Nothing)
                                        If IsNumeric(Index(1)) Then
                                            newprop = newprop(Int32.Parse(Index(1)))
                                        Else
                                            newprop = newprop(Index(1))
                                        End If
                                    End If
                                Else
                                    newprop = GetProperty(Props(i), tempprop)
                                    newprop = newprop.GetValue(tempprop, Nothing)
                                End If
                                tempprop = newprop
                            Next
                            FlagDirect = True
                        Else
                            If parent.PropertyName.Contains(parAbre) Then
                                Index = parent.PropertyName.Split(parAbre)
                                Index(1) = Index(1).Replace(parCierra, String.Empty)
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

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Valor obtenido: " & value.ToString())
                        If parent.ZvarName.Contains("<<") AndAlso parent.ZvarName.Contains(">>") Then
                            Me.nombreIndice = parent.ZvarName.Split(parAbre)(1)
                            nombreIndice = nombreIndice.Split(parCierra)(0)
                            For Each indice As Index In results(j).Indexs
                                If indice.Name = nombreIndice Then
                                    Dim row As DataRow = dtModifiedIndex.NewRow()
                                    row("ID") = indice.ID
                                    row("OldValue") = indice.Data
                                    row("NewValue") = value
                                    dtModifiedIndex.Rows.Add(row)

                                    indice.Data = value
                                    indice.DataTemp = value
                                    indexs.Add(indice.ID)
                                End If
                            Next
                        Else
                            If VariablesInterReglas.ContainsKey(parent.ZvarName) = False Then
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Agregando variable..." & parent.ZvarName)
                                VariablesInterReglas.Add(parent.ZvarName, value)
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Modificando el valor de la variable..." & parent.ZvarName)
                                VariablesInterReglas.Item(parent.ZvarName) = value
                            End If
                        End If

                    Next
                    If indexs.Count > 0 Then
                        Results_Business.SaveModifiedIndexData(results(j), True, True, indexs, dtModifiedIndex)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificaciones aplicadas con éxito!")
                    End If
                Next

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Objetos procesados con éxito!")
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se han encontrado objetos.")
            End If
        Finally
            Me._objectName = Nothing
            Me.objetoObtenido = Nothing
            Me.tempprop = Nothing
            Me.indexs = Nothing
            Me.value = Nothing
            Me.FlagDirect = Nothing
            Me.prop = Nothing
            Me.Props = Nothing
            Me.Index = Nothing
            Me.FirstProp = Nothing
            Me.newprop = Nothing
            Me.nombreIndice = Nothing
            Me.i = Nothing
            If dtModifiedIndex IsNot Nothing Then
                dtModifiedIndex.Dispose()
                dtModifiedIndex = Nothing
            End If
        End Try

        Return results
    End Function
End Class

'Reconocer valor
'objetoloco = WFRuleParent.ReconocerVariablesObjeto(myrule.textboxdeasrriba)as object	

'Obtener propiedad
'Dim a As Object = objetoloco.GetType().GetProperty(listboxvalor).GetValue()

'Guardar en texto inteligente
'If myRule.HashTable.Contains("<<") Then
'Dim nombreIndice As String = myRule.HashTable.Split(parAbre)(1)
'                    nombreIndice = nombreIndice.Split(parCierra)(0)
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
