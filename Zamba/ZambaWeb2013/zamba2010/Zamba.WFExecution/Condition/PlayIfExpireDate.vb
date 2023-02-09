Imports zamba.Core

Public Class PlayIfExpireDate
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfExpireDate) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFRB As New WFRulesBusiness
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
        If (myrule.ChildRulesIds Is Nothing OrElse myrule.ChildRulesIds.Count = 0) Then
            myrule.ChildRulesIds = WFRB.GetChildRulesIds(myrule.ID)
        End If

        If myrule.ChildRulesIds.Count = 2 Then
            For Each childruleId As Int64 In myrule.ChildRulesIds
                Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                R.ParentRule = myrule
                R.IsAsync = myrule.IsAsync
                If R.GetType().ToString().Contains("IfBranch") Then
                    Return results
                End If
            Next
        End If

        Dim TR As TaskResult
        Dim SL As New System.Collections.Generic.List(Of Core.ITaskResult)
        Select Case myrule.Comparacion
            Case Zamba.Core.Comparaciones.No_Vencio
                For Each TR In results
                    If TR.IsExpired = False Then
                        SL.Add(TR)
                    End If
                Next
            Case Zamba.Core.Comparaciones.Vencio
                For Each TR In results
                    If TR.IsExpired = True Then
                        SL.Add(TR)
                    End If
                Next
            Case Comparaciones.Vence_En
                For Each TR In results
                    If TR.ExpireDate <= Date.Now.AddHours(myrule.CantidadHoras) Then
                        SL.Add(TR)
                    End If
                Next
            Case Comparaciones.Vencido_Por
                For Each TR In results
                    If TR.ExpireDate <= Date.Now.AddHours(myrule.CantidadHoras * -1.0) Then
                        SL.Add(TR)
                    End If
                Next
        End Select
        Return SL
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfExpireDate, ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim TR As TaskResult
        Dim SL As New System.Collections.Generic.List(Of Core.ITaskResult)
        Select Case myrule.Comparacion
            Case Zamba.Core.Comparaciones.No_Vencio
                For Each TR In results
                    If (TR.IsExpired = False) = ifType Then
                        SL.Add(TR)
                    End If
                Next
            Case Zamba.Core.Comparaciones.Vencio
                For Each TR In results
                    If (TR.IsExpired = True) = ifType Then
                        SL.Add(TR)
                    End If
                Next
            Case Comparaciones.Vence_En
                For Each TR In results
                    ' Saco la diferencia de horas, Dif va a dar positivo.
                    Dim Dif As Int32 = (DateDiff(DateInterval.Hour, Date.Now, TR.ExpireDate))

                    If (Dif < myrule.CantidadHoras And Dif >= 0) = ifType Then
                        SL.Add(TR)
                    End If
                Next
            Case Comparaciones.Vencido_Por
                For Each TR In results
                    ' Saco la diferencia de horas, Dif va a dar negativo.
                    Dim Dif As Int32 = (DateDiff(DateInterval.Hour, Date.Now, TR.ExpireDate))

                    If (Dif < -myrule.CantidadHoras AndAlso Dif < 0) = ifType Then
                        SL.Add(TR)
                    End If
                Next
        End Select
        Return SL
    End Function
End Class


'Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfExpireDate) As System.Collections.Generic.List(Of Core.ITaskResult)
'    Dim TR As TaskResult
'    Dim SL As New System.Collections.Generic.List(Of Core.ITaskResult)
'    Dim dHoras As Double = myrule.CantidadHoras
'    Dim Comp As Comparaciones = myrule.Comparacion

'    For Each TR In results
'        For Each task As TaskResult In TR.WfStep.Tasks
'            If task.IsExpired = True Then
'                TR.WfStep.Tasks.Add(task)
'            End If
'        Next
'    Next

'    Try
'        Select Case Comp
'            Case Comparaciones.No_Vencio
'                For Each TR In results
'                    Try
'                        If Not Vencio(TR, DateTime.Now) Then
'                            SL.Add(TR)
'                        End If
'                    Catch ex As Exception
'                       zamba.core.zclass.raiseerror(ex)
'                    End Try
'                Next
'            Case Comparaciones.Vence_En
'                Dim Fecha As DateTime
'                Fecha = DateTime.Now.AddHours(dHoras)
'                For Each TR In results
'                    Try
'                        If Vencio(TR, Fecha) Then
'                            SL.Add(TR)
'                        End If
'                    Catch ex As Exception
'                       zamba.core.zclass.raiseerror(ex)
'                    End Try
'                Next
'            Case Comparaciones.Vencido_Por
'                Dim Fecha As DateTime
'                Fecha = DateTime.Now.AddHours(dHoras * -1.0)
'                For Each TR In results
'                    Try
'                        If Vencio(TR, Fecha) Then
'                            SL.Add(TR)
'                        End If
'                    Catch ex As Exception
'                       zamba.core.zclass.raiseerror(ex)
'                    End Try
'                Next
'            Case Comparaciones.Vencio
'                For Each TR In results
'                    Try
'                        If Vencio(TR, DateTime.Now) Then
'                            SL.Add(TR)
'                        End If
'                    Catch ex As Exception
'                       zamba.core.zclass.raiseerror(ex)
'                    End Try
'                Next
'        End Select
'    Catch ex As Exception
'        Zamba.Core.ZClass.RaiseError(ex)
'    End Try
'    Return SL
'End Function