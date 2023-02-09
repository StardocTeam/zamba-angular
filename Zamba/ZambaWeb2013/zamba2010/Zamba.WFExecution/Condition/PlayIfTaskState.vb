Imports Zamba.Core
Public Class PlayIfTaskState
    Private states As String
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfTaskState) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFRB As New WFRulesBusiness

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
        If (myrule.ChildRulesIds Is Nothing OrElse myrule.ChildRulesIds.Count = 0) Then
            myrule.ChildRulesIds = WFRB.GetChildRulesIds(myrule.ID)
        End If

        If myrule.ChildRulesIds.Count = 2 Then
            For Each childruleId As Int64 In myrule.ChildRulesIds
                Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                R.ParentRule = myrule
                R.IsAsync = myrule.IsAsync
                If R.GetType().ToString().Contains("IfBranch") Then
                    Return results
                End If
            Next
        End If

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        states = myrule.States
        Dim Comp As Comparators = myrule.Comp
        For Each r As TaskResult In results
            For Each s As String In Me.getStates()
                Select Case Comp
                    Case Comparators.Equal
                        If Not r.TaskState.ToString.Equals(s) AndAlso s <> "" Then
                            NewList.Add(r)
                            Exit For
                        End If
                    Case Comparators.Different
                        If r.TaskState.ToString.Equals(s) AndAlso s <> "" Then
                            NewList.Add(r)
                            Exit For
                        End If
                End Select
            Next
        Next
        Return NewList
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfTaskState, ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        states = myrule.States
        Dim Comp As Comparators = myrule.Comp
        For Each r As TaskResult In results
            For Each s As String In Me.getStates()
                Select Case Comp
                    Case Comparators.Equal
                        If (Not r.TaskState.ToString.Equals(s) AndAlso s <> "") = ifType Then
                            NewList.Add(r)
                            Exit For
                        End If
                    Case Comparators.Different
                        If (r.TaskState.ToString.Equals(s) AndAlso s <> "") = ifType Then
                            NewList.Add(r)
                            Exit For
                        End If
                End Select
            Next
        Next
        Return NewList
    End Function
    Private Const SEPARADOR_INDICE As String = ","
    Private Function getStates() As String()
        Dim _state As String() = Split(states, SEPARADOR_INDICE)
        Dim i As Int16 = 0
        For Each _s As String In _state
            Select Case _s
                Case "0"
                    _state(i) = "Desasignada"
                Case "1"
                    _state(i) = "Asignada"
                Case "2"
                    _state(i) = "Ejecucion"
                    'Case "3"
                    '    _state(i) = "Finalizada"
                    'Case "4"
                    '    _state(i) = "Derivada"
                    'Case "5"
                    '    _state(i) = "Rechazada"
                Case Else
                    _state(i) = "Deasignada"
            End Select
            i = i + 1
        Next
        Return _state
    End Function
    Enum Comparators
        Equal = 0
        Different = 1
    End Enum
End Class
