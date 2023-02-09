Imports Zamba.Core
Public Class PlayIfTaskStepState

    Private _myRule As IIfTaskStepState
    Private Comp As Comparators

    Sub New(ByVal rule As IIfTaskStepState)
        Me._myRule = rule
    End Sub

    Private Const SEPARADOR_INDICE As String = ","
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFRB As New WFRulesBusiness
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
        If (Me._myRule.ChildRulesIds Is Nothing OrElse Me._myRule.ChildRulesIds.Count = 0) Then
            Me._myRule.ChildRulesIds = WFRB.GetChildRulesIds(Me._myRule.ID)
        End If

        If Me._myRule.ChildRulesIds.Count = 2 Then
            For Each childruleId As Int64 In Me._myRule.ChildRulesIds
                Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                R.ParentRule = _myRule
                R.IsAsync = _myRule.IsAsync
                If R.GetType().ToString().Contains("IfBranch") Then
                    Return results
                End If
            Next
        End If

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Me.Comp = Me._myRule.Comp
            ' Se filtran los result x estado y x comparador...
            For Each item As TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & item.Name)
                For Each itemIdEstado As String In Split(Me._myRule.States, SEPARADOR_INDICE)
                    ' Filtra x comparador...
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparando...")
                    Select Case Comp
                        Case Comparators.Equal
                            If IsNothing(item.State) = True AndAlso String.Compare(itemIdEstado, 0) = 0 Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, " Verdadero")
                                NewList.Add(item)
                                Exit For
                            ElseIf IsNothing(item.State) = False AndAlso String.Compare(itemIdEstado, item.State.ID) = 0 Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, " Verdadero")
                                NewList.Add(item)
                                Exit For
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, " Falso")
                            End If
                        Case Comparators.Different
                            If IsNothing(item.State) = False AndAlso String.Compare(itemIdEstado, item.State.ID) <> 0 Then
                                ZTrace.WriteLineIf(ZTrace.IsInfo, " Verdadero")
                                NewList.Add(item)
                                Exit For
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, " Falso")
                            End If
                    End Select
                Next
            Next
        Finally
            Me.Comp = Nothing
        End Try
        Return NewList
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Me.Comp = Me._myRule.Comp
            ' Se filtran los result x estado y x comparador...
            For Each item As TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & item.Name)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Estado de la tarea " & item.State.ID)
                Dim blnValidate As Boolean
                If Not IsNothing(item.State) Then
                    For Each itemIdEstado As String In Split(Me._myRule.States, SEPARADOR_INDICE)
                        ' Filtra x comparador...
                        'If itemIdEstado <> String.Empty Then
                        If Not String.IsNullOrEmpty(itemIdEstado) Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparando...")
                            Select Case Comp
                                Case Comparators.Equal
                                    If String.Compare(itemIdEstado, item.State.ID) = 0 Then
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, " Verdadero. ID comparado:" & itemIdEstado)
                                        blnValidate = True
                                        Exit For
                                    Else
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, " Falso. ID comparado:" & itemIdEstado)
                                    End If
                                Case Comparators.Different
                                    If String.Compare(itemIdEstado, item.State.ID) <> 0 Then
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, " Verdadero. ID comparado:" & itemIdEstado)
                                        blnValidate = True
                                    Else
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, " Falso. ID comparado:" & itemIdEstado)
                                        blnValidate = False
                                        Exit For
                                    End If
                            End Select
                        End If
                    Next
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, " La tarea no tiene estado valido")
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El resultado de la regla es " & blnValidate)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "La ejecucion de la regla es " & ifType)
                If blnValidate = ifType Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se cumple")
                    NewList.Add(item)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se cumple")
                End If
            Next
        Finally
            Me.Comp = Nothing
        End Try
        Return NewList
    End Function
End Class

