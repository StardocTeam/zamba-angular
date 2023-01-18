
Public Class PlayDoIndexFile
    Private _myRule As IDoIndexFile
    Private resultado As String

    Sub New(ByVal rule As IDoIndexFile)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim RB As New Results_Business
        Try

            For Each r As Core.TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Reconociendo variables")
                Dim _docTypeId As String = _myRule.DocTypeId
                _docTypeId = TextoInteligente.ReconocerCodigo(_myRule.DocTypeId, r)
                _docTypeId = WFRuleParent.ReconocerVariables(_docTypeId).TrimEnd

                Dim _docId As String = _myRule.DocId
                _docId = TextoInteligente.ReconocerCodigo(_myRule.DocId, r)
                _docId = WFRuleParent.ReconocerVariables(_docId).TrimEnd

                Dim _documentPath As String
                If (UserPreferences.getValueForMachine("IndexContent", UPSections.Indexer, False) = True) Then
                    _documentPath = _myRule.DocumentPath
                    _documentPath = TextoInteligente.ReconocerCodigo(_myRule.DocumentPath, r)
                    _documentPath = WFRuleParent.ReconocerVariables(_documentPath).TrimEnd
                End If

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Indexando Documento e indices")

                resultado = RB.IndexFile(_documentPath, _docId, _docTypeId, 0).ToString

                If VariablesInterReglas.ContainsKey(_myRule.VarName) = False Then
                    'VERIFICAR SI LA VARIABLE DEBE SER GLOBAL ONO
                    VariablesInterReglas.Add(_myRule.VarName, resultado, False)
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Variable Creada")
                Else
                    VariablesInterReglas.Item(_myRule.VarName) = resultado

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Variable Guardada")
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
