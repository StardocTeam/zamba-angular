Imports Zamba.Core.WF.WF
Imports Zamba.Core.DocTypes.DocAsociated
Imports Zamba.Data
Imports Zamba.Core

Public Class PlayDOGetDocAsoc


    Dim WFTB As New WFTaskBusiness

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDOGetDocAsoc) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim S As New System.Collections.Generic.List(Of Core.ITaskResult)
        ' [AlejandroR] 04/01/10 - Created
        ' Se agregan a la coleccion solo los documentos asociados que
        ' hayan sido configurados en la regla

        Dim docTypes As List(Of String)
        Dim RB As New Results_Business
        Try
            docTypes = New List(Of String)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo los documentos asociados...")

            For Each docType As String In myRule.tiposDeDocumento.Split("*")
                If Not String.IsNullOrEmpty(docType) Then
                    docTypes.Add(docType)
                End If
            Next
            Dim UP As New UserPreferences
            Dim DAB As New DocAsociatedBusiness
            For Each r As Core.TaskResult In results
                For Each AsocResult As Core.Result In DAB.getAsociatedResultsFromResult(r, Int32.Parse(UP.getValue("CantidadFilas", UPSections.UserPreferences, 100, Zamba.Membership.MembershipHelper.CurrentUser.ID)), Membership.MembershipHelper.CurrentUser.ID)
                    If docTypes.Contains(AsocResult.DocTypeId) Then
                        Dim task As ITaskResult = WFTB.GetTaskByDocIdAndWorkFlowId(AsocResult.ID, 0)

                        If IsNothing(task) Then
                            task = WFTB.GetTaskByDocIdAndWorkFlowId(AsocResult.ID, 0)
                        End If

                        If Not IsNothing(task) Then
                            S.Add(task)
                        Else
                            S.Add(DirectCast(RB.GetNewTaskResult(AsocResult.ID, AsocResult.DocType), ITaskResult))
                        End If
                    End If
                Next
            Next
            DAB = Nothing

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Documentos obtenidos con éxito!")
            If myRule.ContinuarConResultadoObtenido Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se retornan " + S.Count.ToString() + " documentos asociados")
                Return S
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se continua el resto de las reglas con la tarea original")
                Return results
            End If
        Finally
            docTypes = Nothing
            RB = Nothing
        End Try
    End Function
End Class