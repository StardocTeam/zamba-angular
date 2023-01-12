Imports System.Windows.Forms

Public Class PlayDoRefreshTask
    Private _myRule As IDORefreshTask
    Private cancelChildRulesExecution As Boolean = False

    Public Sub New(ByVal rule As IDORefreshTask)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return PlayWeb(results, New Hashtable, _myRule)

    End Function

    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable, ByVal myRule As IDORefreshTask) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim taskIDToRefresh As Long
        Dim docIDToRefresh As Long
        If myRule.RefreshActual Then
            taskIDToRefresh = results(0).TaskId
            'docIDToRefresh = results(0).ID
        Else
            Dim VarInterReglas As New VariablesInterReglas()

            Dim strTemp = Zamba.Core.TextoInteligente.ReconocerCodigo(myRule.TaskId, results(0))
            strTemp = VarInterReglas.ReconocerVariablesValuesSoloTexto(strTemp)

            Dim intTaskIDTemp As Long
            If Long.TryParse(strTemp, intTaskIDTemp) Then
                'Dim task As ITaskResult = WF.WF.WFTaskBusiness.GetTask(intTaskIDTemp)
                'taskIDToRefresh = task.TaskId
                taskIDToRefresh = intTaskIDTemp
                'docIDToRefresh = task.ID
            Else
                Throw New Exception("No se ha podido reconocer el TaskID")
            End If
        End If

        Params.Add("TaskIDToRefresh", taskIDToRefresh)
        'Params.Add("DocIDToRefresh", docIDToRefresh)
    End Function

    Public Function PlayWebSecondExecution(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable, ByVal myRule As IDORefreshTask) As System.Collections.Generic.List(Of Core.ITaskResult)
        Params.Clear()
        Return results
    End Function
End Class
