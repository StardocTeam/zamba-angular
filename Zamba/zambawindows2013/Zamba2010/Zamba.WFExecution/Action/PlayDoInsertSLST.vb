Public Class PlayDoInsertSLST
    Private _myRule As IDoInsertSLST

    Public Sub New(ByVal rule As IDoInsertSLST)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim code, description, indexID As String
        code = _myRule.Code.ToString
        description = _myRule.Description.ToString
        indexID = _myRule.IDSLST.ToString

        Try
            For Each t As Core.TaskResult In results
                code = TextoInteligente.ReconocerCodigo(_myRule.Code, t).Trim
                description = TextoInteligente.ReconocerCodigo(_myRule.Description, t).Trim
                indexID = TextoInteligente.ReconocerCodigo(_myRule.IDSLST, t).Trim

                If code.Contains("zvar") Then
                    code = WFRuleParent.ReconocerVariablesValuesSoloTexto(code)
                End If


                If description.Contains("zvar") Then
                    description = WFRuleParent.ReconocerVariablesValuesSoloTexto(description)
                End If
            
                If indexID.Contains("zvar") Then
                    indexID = WFRuleParent.ReconocerVariablesValuesSoloTexto(indexID)
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Codigo = " + code & " Descripcion = " + description & " Atributo = " + indexID)


                IndexsBusiness.InsertIndexSust(indexID, code.ToString, description.ToString)
            Next
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function

    Function DiscoverParams() As List(Of String)

    End Function
End Class
