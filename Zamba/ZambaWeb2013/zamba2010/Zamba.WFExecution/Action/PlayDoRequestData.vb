Imports Zamba.Core.WF.WF
Imports Zamba.Core
Public Class PlayDoRequestData

    Private _myRule As IDoRequestData

    Sub New(ByVal rule As IDoRequestData)
        Me._myRule = rule

    End Sub

    ''' <summary>
    ''' play de la regla
    ''' </summary>
    ''' <history>
    ''' 
    ''' </history>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return PlayWeb(results, Nothing)
    End Function

    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            For Each r As TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo indices...")

                Dim indexlist As New List(Of Index)

                For Each Ind As IIndex In r.Indexs
                    If Me._myRule.ArrayIds.Contains(Ind.ID.ToString) Then
                        indexlist.Add(Ind)
                    End If
                Next

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de atributos a mostrar: " & indexlist.Count)
                Params.Add("indexlist", indexlist)
                Params.Add("DocTypeId", Me._myRule.DocTypeId)
                Params.Add("Result", r)

            Next
        Finally

        End Try

        Return results
    End Function

    Public Function PlayWebSecondExecution(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Dim r As TaskResult = Params("Result")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
            Dim indlst As List(Of Index) = Params("indexlist")
            Dim idlist As New List(Of Long)
            For Each ind As Index In indlst
                idlist.Add(ind.ID)
            Next

            Dim Results_Business As New Results_Business()
            Results_Business.SaveModifiedIndexData(DirectCast(r, Zamba.Core.Result), True, False, idlist, Nothing)
            Dim RightsBusiness As New RightsBusiness()
            RightsBusiness.SaveAction(r.ID, Zamba.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, Me._myRule.Name)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificaciones realizadas con éxito.")
            Return results
        Finally
            Params.Clear()
        End Try
    End Function

End Class