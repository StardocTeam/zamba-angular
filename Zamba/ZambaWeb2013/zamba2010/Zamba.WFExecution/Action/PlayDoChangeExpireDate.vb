Imports Zamba.Core.WF.WF

Public Class PlayDoChangeExpireDate
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDoChangeExpireDate) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFTB As New WFTaskBusiness
        Dim UB As New UserBusiness

        Try
            For Each r As Core.TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                If IsNothing(r.ExpireDate) = False Then
                    Dim NuevaFecha As DateTime = r.ExpireDate

                    Dim Minutos As Int32 = myRule.Value1
                    Dim Horas As Int32 = myRule.Value2
                    Dim Dias As Int32 = myRule.Value3
                    Dim Semanas As Int32 = myRule.Value4
                    Dim Meses As Int32 = myRule.Value5

                    If myRule.Direccion1 = 1 Then
                        Minutos = -Minutos
                    End If
                    If myRule.Direccion2 = 1 Then
                        Horas = -Horas
                    End If
                    If myRule.Direccion3 = 1 Then
                        Dias = -Dias
                    End If
                    If myRule.Direccion4 = 1 Then
                        Semanas = -Semanas
                    End If
                    If myRule.Direccion5 = 1 Then
                        Meses = -Meses
                    End If

                    NuevaFecha = NuevaFecha.AddMinutes(Minutos)
                    NuevaFecha = NuevaFecha.AddHours(Horas)
                    NuevaFecha = NuevaFecha.AddDays(Dias)
                    NuevaFecha = NuevaFecha.AddDays(Semanas * 7)
                    NuevaFecha = NuevaFecha.AddMonths(Meses)

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificando la fecha de expiración...")
                    WFTB.ChangeExpireDate(r, NuevaFecha)
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Fecha modificada con éxito!")
                End If
                Try
                    UB.SaveAction(r.ID, Zamba.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, myRule.Name)
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al guardar la acción de usuario, no obstante, la regla fué ejecutada correctamente " & r.Name & " (Id " & r.TaskId & ")")
                    Throw
                End Try

            Next
        Finally
            UB = Nothing
            WFTB = Nothing
        End Try
        Return results
    End Function
End Class
