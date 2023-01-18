Public Class PlayIfDocumentsCount
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfDOCUMENTSCOUNT) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFRB As New WFRulesBusiness
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
        If (myrule.ChildRulesIds Is Nothing OrElse myrule.ChildRulesIds.Count = 0) Then
            myrule.ChildRulesIds = WFRB.GetChildRulesIds(myrule.ID, myrule.RuleClass, results)
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
        Select Case myrule.Comparacion
                Case Comparadores.Igual
                    For Each TR As TaskResult In results
                    If TR.WfStep.TasksCount = myrule.CantidadTareas Then
                        Return results
                    Else
                        Return Nothing
                        End If
                    Next
                Case Comparadores.Distinto
                    For Each TR As TaskResult In results
                    If TR.WfStep.TasksCount <> myrule.CantidadTareas Then
                        Return results
                    Else
                        Return Nothing
                        End If
                    Next
                Case Comparadores.Mayor
                    For Each TR As TaskResult In results
                    If TR.WfStep.TasksCount > myrule.CantidadTareas Then
                        Return results
                    Else
                        Return Nothing
                        End If
                    Next
                Case Comparadores.Menor
                    For Each TR As TaskResult In results
                    If TR.WfStep.TasksCount < myrule.CantidadTareas Then
                        Return results
                    Else
                        Return Nothing
                        End If
                    Next
                Case Comparadores.IgualMayor
                    For Each TR As TaskResult In results
                    If TR.WfStep.TasksCount >= myrule.CantidadTareas Then
                        Return results
                    Else
                        Return Nothing
                        End If
                    Next
                Case Comparadores.IgualMenor
                    For Each TR As TaskResult In results
                    If TR.WfStep.TasksCount <= myrule.CantidadTareas Then
                        Return results
                    Else
                        Return Nothing
                        End If
                    Next
                Case Else
                    Return Nothing
            End Select
            Return Nothing
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfDOCUMENTSCOUNT, ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Select Case myrule.Comparacion
            Case Comparadores.Igual
                For Each TR As TaskResult In results
                    If (TR.WfStep.TasksCount = myrule.CantidadTareas) = ifType Then
                        Return results
                    Else
                        Return Nothing
                    End If
                Next
            Case Comparadores.Distinto
                For Each TR As TaskResult In results
                    If (TR.WfStep.TasksCount <> myrule.CantidadTareas) = ifType Then
                        Return results
                    Else
                        Return Nothing
                    End If
                Next
            Case Comparadores.Mayor
                For Each TR As TaskResult In results
                    If (TR.WfStep.TasksCount > myrule.CantidadTareas) = ifType Then
                        Return results
                    Else
                        Return Nothing
                    End If
                Next
            Case Comparadores.Menor
                For Each TR As TaskResult In results
                    If (TR.WfStep.TasksCount < myrule.CantidadTareas) = ifType Then
                        Return results
                    Else
                        Return Nothing
                    End If
                Next
            Case Comparadores.IgualMayor
                For Each TR As TaskResult In results
                    If (TR.WfStep.TasksCount >= myrule.CantidadTareas) = ifType Then
                        Return results
                    Else
                        Return Nothing
                    End If
                Next
            Case Comparadores.IgualMenor
                For Each TR As TaskResult In results
                    If (TR.WfStep.TasksCount <= myrule.CantidadTareas) = ifType Then
                        Return results
                    Else
                        Return Nothing
                    End If
                Next
            Case Else
                If ifType = True Then
                    Return Nothing
                Else
                    Return results
                End If
        End Select
        Return Nothing
    End Function
End Class
