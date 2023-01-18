Imports Zamba.Core.WF.WF
Imports Zamba.Membership

Public Class PlayDODistribuir

    Private _myRule As IDoDistribuir

    Sub New(ByVal rule As IDoDistribuir)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return PlayWeb(results, Nothing)
    End Function

    ''' <summary>
    ''' Método utilizado para distribuir la tarea
    ''' [Sebastian] 17-09-09 MODIFIED logged  in the new task step
    ''' </summary>
    ''' <param name="Results"></param>
    ''' <param name="myRule"></param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Gaston]    01/09/2008  Modified 
    '''     [Ezequiel]  03/11/2009 Se modifico el metodo por tema de performance.
    ''' </history>
    Private Sub Distribuir(ByRef Results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDoDistribuir, ByVal CurrentUserId As Int64)
        Dim WFTB As New WFTaskBusiness
        Dim x As Int32 = Results.Count
        WFTB.Distribute(Results, myRule.NewWFStepId, CurrentUserId)

        If (Results.Count <> x) Then
            Distribuir(Results, myRule, Zamba.Membership.MembershipHelper.CurrentUser.ID)
        End If
        WFTB = Nothing
    End Sub

    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try

            If (results.Count > 0) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de tareas a distribuir: " & results.Count)
                Distribuir(results, Me._myRule, Zamba.Membership.MembershipHelper.CurrentUser.ID)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se han distribuido todas las tareas.")

                If (Params Is Nothing) Then
                    Params = New Hashtable
                End If


                Params.Add("StepID", Me._myRule.NewWFStepId)
                Params.Add("CloseTask", Me._myRule.CloseTask)
                Params.Add("TasksToExecute", results)
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No hay tareas para distribuir.")
            End If
        Finally

        End Try
        Return results
    End Function

End Class
