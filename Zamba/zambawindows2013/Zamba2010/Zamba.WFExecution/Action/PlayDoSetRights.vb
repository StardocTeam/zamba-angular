Public Class PlayDoSetRights

    Private myRule As IDoSetRights

    Sub New(ByVal rule As IDoSetRights)
        myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal myRule As IDoSetRights) As System.Collections.Generic.List(Of ITaskResult)
        Dim taskResults As New List(Of ITaskResult)
        Dim hshValues As New Hashtable()

        Try
            For i As Int32 = 0 To results.Count - 1
                For Each pair As KeyValuePair(Of RightsType, Boolean) In myRule.Rights
                    'Pone una marca en el viewer como solo lectura
                    taskResults.Add(results(i))

                    Select Case pair.Key
                        Case RightsType.Edit
                            results(i).DocType.IsReadOnly = pair.Value
                            hshValues.Add("value", pair.Value)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificando por regla el permiso de Edición a " & pair.Value)
                            ZClass.HandleRuleModule(ResultActions.SetReadOnly, taskResults, hshValues)

                        Case RightsType.ReIndex
                            results(i).DocType.IsReindex = pair.Value
                            hshValues.Add("value", Not pair.Value)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificando por regla el permiso de Reindex a " & pair.Value)
                            ZClass.HandleRuleModule(ResultActions.SetReindex, taskResults, hshValues)

                        Case RightsType.HideReplaceDocument
                            hshValues.Add("value", Not pair.Value)
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificando por regla la visualización del botón Reemplazar Documento a " & pair.Value)
                            ZClass.HandleRuleModule(ResultActions.HideReplaceDocument, taskResults, hshValues)

                    End Select

                    taskResults.Clear()
                    hshValues.Clear()
                Next
            Next

        Finally
            If taskResults IsNot Nothing Then
                taskResults.Clear() 'ver que esto no rompa results
                taskResults = Nothing
            End If
            If hshValues IsNot Nothing Then
                hshValues.Clear()
                hshValues = Nothing
            End If
        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
