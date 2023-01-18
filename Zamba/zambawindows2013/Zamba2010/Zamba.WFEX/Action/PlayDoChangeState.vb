Imports Zamba.Core
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

        Dim NewResults As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Steps = WFStepBusiness.GetStepById(Me._myRule.StepId)
            If Not Steps Is Nothing Then
                If Steps.States.Contains(New WFStepState(Me._myRule.StateId)) Then

                    state = WFStepStatesComponent.getStateFromList(Me._myRule.StateId, Steps.States)
                    For Each r As Core.TaskResult In results
                        WFTaskBusiness.ChangeState(r, state)
                        NewResults.Add(r)
                        Try
                            UserBusiness.Rights.SaveAction(r.ID, Zamba.Core.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, Me._myRule.Name)
                        Catch ex As Exception
                            Trace.WriteLineIf(ZTrace.IsInfo, "Error al guardar la acción de usuario, no obstante, la regla fué ejecutada correctamente " & r.Name & " (Id " & r.TaskId & ")")
                        End Try
                    Next
                    Trace.WriteLineIf(ZTrace.IsInfo, "Modificación realizada con éxito!")
                End If
            Else
                Trace.WriteLineIf(ZTrace.IsInfo, "No se ha encontrado la etapa. El proceso de cambio de estado no será realizado.")
            End If
        Finally

            Me.state = Nothing
            Me.Steps = Nothing
        End Try
        Return NewResults
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
