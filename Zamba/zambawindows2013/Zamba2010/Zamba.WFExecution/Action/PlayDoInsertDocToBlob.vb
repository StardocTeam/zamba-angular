Public Class PlayDoInsertDocToBlob
    Private _myRule As IDoInsertDocToBlob

    Sub New(ByVal rule As IDoInsertDocToBlob)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de results a ejecutar: " & results.Count)

        Dim executeDoctype As String
        Dim executeDocId As String

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconociendo valores en la consulta")
        executeDoctype = WFRuleParent.ReconocerVariables(_myRule.DocTypeID)
        If Not IsNothing(results(0)) Then
            executeDoctype = TextoInteligente.ReconocerCodigo(executeDoctype, results(0))
        End If
        ZTrace.WriteLineIf(ZTrace.IsInfo, "DocTypeID: " & executeDoctype)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Reconociendo valores en la consulta")
        executeDocId = WFRuleParent.ReconocerVariables(_myRule.DocID)
        If Not IsNothing(results(0)) Then
            executeDocId = TextoInteligente.ReconocerCodigo(executeDocId, results(0))
        End If
        ZTrace.WriteLineIf(ZTrace.IsInfo, "DocID: " & executeDocId)

        If IsNumeric(executeDocId) AndAlso IsNumeric(executeDoctype) Then
            Dim res As IResult = Results_Business.GetResult(executeDocId, executeDoctype)

            If res IsNot Nothing Then
                If String.IsNullOrEmpty(res.FullPath) Then
                    Throw New Exception("El directorio del documento no fue especificado")
                End If

                res.EncodedFile = FileEncode.Encode(res.FullPath)
                If res.EncodedFile IsNot Nothing AndAlso res.EncodedFile.Length > 0 Then
                    Dim resBusiness As New ResultBusinessExt
                    resBusiness.InsertIntoDOCB(res)
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo o no se encontro o estaba vacio")
                End If
            Else
                Throw New Exception("Result no instanciado")
            End If
        Else
            Throw New Exception("El tipo de documento o el id de documento no son numericos. DocID = " & executeDocId & " DocTypeID = " & executeDoctype)
        End If

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
