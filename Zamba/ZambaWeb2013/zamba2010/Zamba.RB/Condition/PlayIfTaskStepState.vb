Imports Zamba.Core
Public Class PlayIfTaskStepState

    Private _myRule As IIfTaskStepState
    Private Comp As Comparators

    Sub New(ByVal rule As IIfTaskStepState)
        Me._myRule = rule
    End Sub

    Private Const SEPARADOR_INDICE As String = ","
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Me.Comp = Me._myRule.Comp
            ' Se filtran los result x estado y x comparador...
            For Each item As TaskResult In results
                 If String.IsNullOrEmpty(item.State.Name) Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "Estado de la tarea: " & item.State.ID)
                Else
                    Trace.WriteLineIf(ZTrace.IsInfo, "Estado de la tarea: " & item.State.Name)
                End If

                Dim blnValidate As Boolean
                If Not IsNothing(item.State) Then
                    For Each itemIdEstado As String In Split(Me._myRule.States, SEPARADOR_INDICE)
                        ' Filtra x comparador...
                        'If itemIdEstado <> String.Empty Then
                        If Not String.IsNullOrEmpty(itemIdEstado) Then
                            Trace.WriteLineIf(ZTrace.IsInfo, "Comparando estados.")
                            Select Case Comp
                                Case Comparators.Equal
                                    If String.Compare(itemIdEstado, item.State.ID) = 0 Then
                                        Trace.WriteLineIf(ZTrace.IsInfo, " Verdadero. ID comparado:" & itemIdEstado)
                                        blnValidate = True
                                        Exit For
                                    Else
                                        Trace.WriteLineIf(ZTrace.IsInfo, " Falso. ID comparado:" & itemIdEstado)
                                    End If
                                Case Comparators.Different
                                    If String.Compare(itemIdEstado, item.State.ID) <> 0 Then
                                        Trace.WriteLineIf(ZTrace.IsInfo, " Verdadero. ID comparado:" & itemIdEstado)
                                        blnValidate = True
                                    Else
                                        Trace.WriteLineIf(ZTrace.IsInfo, " Falso. ID comparado:" & itemIdEstado)
                                        blnValidate = False
                                        Exit For
                                    End If
                            End Select
                        End If
                    Next
                Else
                    Trace.WriteLineIf(ZTrace.IsInfo, "La tarea no tiene estado valido.")
                End If
                Trace.WriteLineIf(ZTrace.IsInfo, "El resultado de la regla es " & blnValidate)
                If blnValidate = ifType Then
                    Trace.WriteLineIf(ZTrace.IsInfo, "Se cumple")
                    NewList.Add(item)
                Else
                    Trace.WriteLineIf(ZTrace.IsInfo, "No se cumple")
                End If
            Next
        Finally
            Me.Comp = Nothing
        End Try
        Return NewList
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class

