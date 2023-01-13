Imports Zamba.Core.WF.WF

Public Class PlayDoGetTask

    Private _myRule As IDoGetTask

    Public Sub New(ByVal rule As IDoGetTask)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim VarInterReglas As New VariablesInterReglas()
        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)

        Dim TaskId As String

        For Each t As Core.TaskResult In results

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Resolviendo Tarea " & _myRule.TaskIdVariable)
            TaskId = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.TaskIdVariable, t)
            TaskId = VarInterReglas.ReconocerVariablesValuesSoloTexto(TaskId)
            Dim TaskIdnumeric As Int64

            If TaskId = String.Empty OrElse (Int64.TryParse(TaskId, TaskIdnumeric) = False) Then
                Throw New Exception(String.Format("Error en Regla: GetTask Id: {0} El Id de Tarea a Obtener esta vacio o no es numerico: Valor de variable: {1} ", _myRule.ID, TaskId))
            End If

            Dim WFTB As New WF.WF.WFTaskBusiness
            Dim task As ITaskResult = WFTB.GetTask(TaskId, Zamba.Membership.MembershipHelper.CurrentUser.ID)
            WFTB = Nothing
            If Not IsNothing(task) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Tarea obtenida " & task.Name & " (" & task.ID & ")")
                NewList.Add(task)
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se puedo obtener la tarea ")
            End If
        Next

        VarInterReglas = Nothing
        Return NewList
    End Function
End Class
