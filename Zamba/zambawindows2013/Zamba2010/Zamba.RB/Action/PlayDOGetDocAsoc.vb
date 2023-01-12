Imports Zamba.Core.WF.WF
Imports Zamba.Core.DocTypes.DocAsociated
Imports Zamba.Data
Imports Zamba.Core

Public Class PlayDOGetDocAsoc


    Private myRule As IDOGetDocAsoc
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim S As New System.Collections.Generic.List(Of Core.ITaskResult)
        ' [AlejandroR] 04/01/10 - Created
        ' Se agregan a la coleccion solo los documentos asociados que
        ' hayan sido configurados en la regla

        Dim docTypes As List(Of String)

        Try
            docTypes = New List(Of String)

            For Each docType As String In myRule.tiposDeDocumento.Split("*")
                If Not String.IsNullOrEmpty(docType) Then
                    docTypes.Add(docType)
                End If
            Next

            For Each r As Core.TaskResult In results
                Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo los documentos asociados de " & r.Name & " (ID: " & r.ID.ToString & ", DTID: " & r.DocTypeId.ToString & ")")
                For Each AsocResult As Core.Result In DocAsociatedBusiness.getAsociatedResultsFromResult(r, Int32.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100)))
                    If docTypes.Contains(AsocResult.DocTypeId) Then
                        Dim task As ITaskResult = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(AsocResult.ID, 0)

                        If IsNothing(task) Then
                            task = WFTaskBusiness.GetTaskByDocIdAndWorkFlowId(AsocResult.ID, 0)
                        End If

                        If Not IsNothing(task) Then
                            S.Add(task)
                        Else
                            S.Add(DirectCast(Results_Business.GetNewTaskResult(AsocResult.ID, AsocResult.DocType), ITaskResult))
                        End If
                    End If
                Next
            Next

            Trace.WriteLineIf(ZTrace.IsInfo, "Documentos obtenidos con éxito!")
            If myRule.ContinuarConResultadoObtenido Then
                Trace.WriteLineIf(ZTrace.IsInfo, "Se retornan " + S.Count.ToString() + " documentos asociados")
                Return S
            Else
                Trace.WriteLineIf(ZTrace.IsInfo, "Se retorna el result original")
                Return results
            End If
        Finally
            docTypes = Nothing
        End Try
    End Function
    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDOGetDocAsoc)
        Me.myRule = rule
    End Sub
End Class