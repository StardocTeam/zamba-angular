Public Class PlayIfTaskStepState

    Private _myRule As IIfTaskStepState
    Private Comp As Comparators

    Sub New(ByVal rule As IIfTaskStepState)
        _myRule = rule
    End Sub

    Private Const SEPARADOR_INDICE As String = ","
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Comp = _myRule.Comp
            ' Se filtran los result x estado y x comparador...
            For Each item As TaskResult In results
                Dim stateId As String = item.StateId.ToString()
                Dim stateName As String = item.State.Name
                Dim msgTrace As String = String.Concat(stateId, " - ", stateName)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Estado de la tarea: " & msgTrace)
                Dim blnValidate As Boolean
                If Not IsNothing(item.State) Then
                    For Each itemIdEstado As String In Split(_myRule.States, SEPARADOR_INDICE)
                        ' Filtra x comparador...
                        'If itemIdEstado <> String.Empty Then
                        If Not String.IsNullOrEmpty(itemIdEstado) Then
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Comparando estados.")
                            Select Case Comp
                                Case Comparators.Equal
                                    If String.Compare(itemIdEstado, item.StateId) = 0 Then
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, " Verdadero. ID comparado:" & itemIdEstado)
                                        blnValidate = True
                                        Exit For
                                    Else
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, " Falso. ID comparado:" & itemIdEstado)
                                    End If
                                Case Comparators.Different
                                    If String.Compare(itemIdEstado, item.StateId) <> 0 Then
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
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "La tarea no tiene estado valido.")
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El resultado de la regla es " & blnValidate)
                If blnValidate = ifType Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se cumple")
                    NewList.Add(item)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se cumple")
                End If
            Next
        Finally
            Comp = Nothing
        End Try
        Return NewList
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class

