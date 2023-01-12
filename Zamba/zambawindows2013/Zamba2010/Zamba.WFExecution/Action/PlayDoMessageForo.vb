Imports Zamba.Core

Public Class PlayDoMessageForo
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDoMessageForo) As System.Collections.Generic.List(Of Core.ITaskResult)
        Trace.WriteLine("*** Ejecutando regla " & myRule.Name)
        Try
            For Each r As Core.TaskResult In results
                Trace.WriteLine("Ejecutando la regla para la tarea " & r.Name)
                Dim mID As Int64 = ZForoBusiness.SiguienteId(r.ID)
                Dim mParentID As Int32 = 0
                ZForoBusiness.InsertMessage(r.ID, r.DocType.ID, mID, mParentID, myRule.mAsunto, myRule.mBody, Date.Now, Users.User.CurrentUser.ID, 0)
            Next
        Catch ex As Exception
            Debug.WriteLine(ex.Message & ex.StackTrace)
            Trace.WriteLine("Error en la ejecucion de la regla " & myRule.Name)
            ZClass.raiseerror(ex)
        Finally
            Trace.WriteLine("*** Finalizado ejecucion de regla " & myRule.Name)
        End Try

        Return results
    End Function
End Class
