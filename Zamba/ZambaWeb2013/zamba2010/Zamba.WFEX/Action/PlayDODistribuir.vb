Imports Zamba.Core
Imports Zamba.Core.WF.WF

Public Class PlayDODistribuir
    Private _myRule As IDoDistribuir

    Sub New(ByVal rule As IDoDistribuir)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            If (results.Count > 0) Then

                Distribuir(results, Me._myRule, Membership.MembershipHelper.CurrentUser.ID)
                Trace.WriteLineIf(ZTrace.IsInfo, "Se han distribuido todas las tareas.")
            Else
                Trace.WriteLineIf(ZTrace.IsInfo, "No hay tareas para distribuir.")
            End If
        Finally

        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

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
        Dim x As Int32 = Results.Count
        WFTaskBusiness.Distribute(Results, myRule.NewWFStepId, CurrentUserId)

        If (Results.Count <> x) Then
            Distribuir(Results, myRule, Membership.MembershipHelper.CurrentUser.ID)
        End If
    End Sub


End Class