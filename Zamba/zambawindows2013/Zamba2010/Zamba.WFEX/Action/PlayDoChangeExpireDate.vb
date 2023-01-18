Imports Zamba.Core
Imports Zamba.Core.WF.WF

Public Class PlayDoChangeExpireDate

    Private myRule As IDoChangeExpireDate

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Try
            For Each r As Core.TaskResult In results
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

                    Trace.WriteLineIf(ZTrace.IsInfo, "Modificando la fecha de expiración...")
                    WFTaskBusiness.ChangeExpireDate(r, NuevaFecha)
                    Trace.WriteLineIf(ZTrace.IsInfo, "Fecha modificada con éxito!")
                End If
                Try
                    UserBusiness.Rights.SaveAction(r.ID, Zamba.Core.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, myRule.Name)
                Catch ex As Exception
                    Trace.WriteLineIf(ZTrace.IsInfo, "Error al guardar la acción de usuario, no obstante, la regla fué ejecutada correctamente " & r.Name & " (Id " & r.TaskId & ")")
                    Throw ex
                End Try

            Next
        Finally

        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoChangeExpireDate)
        Me.myRule = rule
    End Sub
End Class
