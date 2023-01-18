Imports Zamba.Core
Imports Zamba.Membership

Public Class PlayDODELETE
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDoDelete) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Dim Borrado As Borrados = myRule.TipoBorrado
            Dim WFBusiness As New WFBusiness
            For Each t As Core.ITaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Eliminando la tarea " & t.Name)
                Select Case Borrado
                    Case Borrados.Tarea
                        'Elimino
                        WFBusiness.RemoveTask(t, False, Zamba.Membership.MembershipHelper.CurrentUser.ID, False)
                    Case Borrados.Total
                        'Elimino
                        WFBusiness.RemoveTask(t, True, Zamba.Membership.MembershipHelper.CurrentUser.ID, Not myRule.DeleteFile)
                End Select
            Next
            WFBusiness = Nothing
        Finally

        End Try
        Return results
    End Function

    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable, ByVal myRule As IDoDelete) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Dim Borrado As Borrados = myRule.TipoBorrado
            Dim WFBusiness As New WFBusiness
            For Each t As Core.ITaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Eliminando la tarea " & t.Name)
                Select Case Borrado
                    Case Borrados.Tarea
                        'Elimino
                        WFBusiness.RemoveTask(t, False, Zamba.Membership.MembershipHelper.CurrentUser.ID, False)
                    Case Borrados.Total
                        'Elimino
                        WFBusiness.RemoveTask(t, True, Zamba.Membership.MembershipHelper.CurrentUser.ID, Not myRule.DeleteFile)
                End Select
            Next
            WFBusiness = Nothing
        Finally

        End Try
        Return results
    End Function
End Class
