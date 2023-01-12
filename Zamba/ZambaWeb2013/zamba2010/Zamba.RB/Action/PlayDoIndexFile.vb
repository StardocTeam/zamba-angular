
Public Class PlayDoIndexFile
    Private _myRule As IDoIndexFile
    Private resultado As String

    Sub New(ByVal rule As IDoIndexFile)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim RB As New Results_Business
        Try

            For Each r As Core.TaskResult In results
                Trace.WriteLineIf(ZTrace.IsVerbose, "Reconociendo variables")
                Dim _docTypeId As String = Me._myRule.DocTypeId
                _docTypeId = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.DocTypeId, r)
                _docTypeId = WFRuleParent.ReconocerVariables(_docTypeId).TrimEnd

                Dim _docId As String = Me._myRule.DocId
                _docId = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.DocId, r)
                _docId = WFRuleParent.ReconocerVariables(_docId).TrimEnd

                Dim _documentPath As String = Me._myRule.DocumentPath
                _documentPath = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.DocumentPath, r)
                _documentPath = WFRuleParent.ReconocerVariables(_documentPath).TrimEnd

                Trace.WriteLineIf(ZTrace.IsInfo, "Indexando Documento e indices")

                resultado = RB.IndexFile(_documentPath, _docId, _docTypeId).ToString

                If VariablesInterReglas.ContainsKey(Me._myRule.VarName) = False Then
                    'VERIFICAR SI LA VARIABLE DEBE SER GLOBAL ONO
                    VariablesInterReglas.Add(Me._myRule.VarName, Me.resultado, False)
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Variable Creada")
                Else
                    VariablesInterReglas.Item(Me._myRule.VarName) = Me.resultado

                    Trace.WriteLineIf(ZTrace.IsVerbose, "Variable Guardada")
                End If

            Next
        Finally
            RB = Nothing
        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function

    Function DiscoverParams() As List(Of String)

    End Function

End Class
