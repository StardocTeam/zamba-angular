Imports Zamba.Core
Imports Zamba.Membership

Public Class PlayDoMessageForo
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDoMessageForo) As System.Collections.Generic.List(Of Core.ITaskResult)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "*** Ejecutando regla " & myRule.Name)
        Try
            Dim FB As New ZForoBusiness
            For Each r As Core.TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                Dim mID As Int64 = FB.SiguienteId(r.ID)
                Dim mParentID As Int32 = 0
                FB.InsertMessage(r.ID, r.DocType.ID, mID, mParentID, myRule.mAsunto, myRule.mBody, Date.Now, Zamba.Membership.MembershipHelper.CurrentUser.ID, 0)
            Next
            FB = Nothing
        Catch ex As Exception
            Debug.WriteLine(ex.Message & ex.StackTrace)
            ZTrace.WriteLineIf(ZTrace.IsError, "Error en la ejecucion de la regla " & myRule.Name)
            ZClass.raiseerror(ex)
        Finally
            ZTrace.WriteLineIf(ZTrace.IsError, "*** Finalizado ejecucion de regla " & myRule.Name)
        End Try

        Return results
    End Function
End Class
