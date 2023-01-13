Imports Zamba.Core.WF.WF

Public Class PlayDoChangeState

    Private _myRule As IDoChangeState
    Private Steps As Core.WFStep
    Private state As Core.WFStepState

    Sub New(ByVal rule As IDoChangeState)
        Me._myRule = rule
    End Sub

    ''' <summary>
    ''' Ejecuta la regla de cambio de estado
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <history> [Marcelo] 31/10/2010 Modified Se modifica la llamada  changestate por que no se utiliza transaccion
    '''</history>
    ''' <remarks></remarks>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFTB As New WFTaskBusiness
        Dim UB As New UserBusiness
        Dim NewResults As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFSB As New WFStepBusiness

        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo etapa con el estado...")
            Steps = WFSB.GetStepById(Me._myRule.StepId)
            If Not Steps Is Nothing Then
                If Steps.States.Contains(New WFStepState(Me._myRule.StateId)) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo el estado...")
                    state = WFStepStatesComponent.getStateFromList(Me._myRule.StateId, Steps.States)
                    For Each r As Core.TaskResult In results
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name & ", Id " & r.TaskId)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificando el estado de la tarea...")
                        WFTB.ChangeState(r, state)
                        NewResults.Add(r)
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificación realizada con éxito!")
                        Try
                            UB.SaveAction(r.ID, Zamba.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, Me._myRule.Name)
                        Catch ex As Exception
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al guardar la acción de usuario, no obstante, la regla fué ejecutada correctamente " & r.Name & " (Id " & r.TaskId & ")")
                        End Try
                    Next
                End If
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha encontrado la etapa. El proceso de cambio de estado no será realizado.")
            End If
        Finally

            Me.state = Nothing
            Me.Steps = Nothing
            WFTB = Nothing
            UB = Nothing
            WFSB = Nothing
        End Try
        Return NewResults
    End Function
End Class
