Public Class PlayIfExpireDate

    Private myRule As IIfExpireDate
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim TR As TaskResult
        Dim SL As New System.Collections.Generic.List(Of Core.ITaskResult)
        Select Case myRule.Comparacion
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

                    If (Dif < myRule.CantidadHoras And Dif >= 0) = ifType Then
                        SL.Add(TR)
                    End If
                Next
            Case Comparaciones.Vencido_Por
                For Each TR In results
                    ' Saco la diferencia de horas, Dif va a dar negativo.
                    Dim Dif As Int32 = (DateDiff(DateInterval.Hour, Date.Now, TR.ExpireDate))

                    If (Dif < -myRule.CantidadHoras AndAlso Dif < 0) = ifType Then
                        SL.Add(TR)
                    End If
                Next
        End Select
        Return SL
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IIfExpireDate)
        myRule = rule
    End Sub
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