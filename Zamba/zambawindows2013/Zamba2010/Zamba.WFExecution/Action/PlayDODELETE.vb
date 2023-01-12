Public Class PlayDODELETE

    Private myRule As IDoDelete
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Dim Borrado As Borrados = myRule.TipoBorrado
            For Each t As Core.ITaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Eliminando la tarea " & t.Name)
                Select Case Borrado
                    Case Borrados.Tarea
                        'Elimino
                        WFBusiness.RemoveTask(t, False, Membership.MembershipHelper.CurrentUser.ID, False)
                    Case Borrados.Total
                        'Elimino
                        WFBusiness.RemoveTask(t, True, Membership.MembershipHelper.CurrentUser.ID, Not myRule.DeleteFile)
                End Select
                'If myRule.DeleteFile Then , 
                '     'si es virtual y tiene full path y el full path existe entonces borro el documento
                '    If Not t.ISVIRTUAL Then
                '        If Not t.FullPath Is Nothing Then
                '            If IO.File.Exists(t.FullPath) Then
                '                IO.File.Delete(t.FullPath)
                '            End If
                '        End If
                '    End If
                'End If
            Next

        Finally
            '
        End Try
        Return results
    End Function
    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoDelete)
        myRule = rule
    End Sub
End Class
