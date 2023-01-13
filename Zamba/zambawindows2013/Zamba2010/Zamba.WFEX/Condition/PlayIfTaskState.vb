Imports Zamba.Core
Public Class PlayIfTaskState
    Private states As String
    Private myRule As IIfTaskState
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
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

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
    Enum Comparators
        Equal = 0
        Different = 1
    End Enum

    Public Sub New(ByVal rule As IIfTaskState)
        Me.myRule = rule
    End Sub
End Class
