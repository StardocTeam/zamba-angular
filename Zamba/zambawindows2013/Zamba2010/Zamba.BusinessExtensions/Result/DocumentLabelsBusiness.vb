Imports Zamba.Data

Public Class DocumentLabelsBusiness

    Public Shared Sub UpdateImportanceLabel(ByVal result As IResult)
        If result IsNot Nothing Then
            Try
                DocumentLabelsData.UpdateImportantLabels(result.DocTypeId, result.ID, result.IsImportant)
            Catch ex As Exception
                If result IsNot Nothing Then
                    Throw New Exception("Ha ocurrido un error al modificar la marca de importancia del documento " & result.Name, ex)
                Else
                    Throw ex
                End If
            End Try
        End If
    End Sub

    Public Shared Sub UpdateImportantLabels(ByVal docTypeId As Int64, ByVal docId As Int64, ByVal important As Boolean)
        Try
            DocumentLabelsData.UpdateImportantLabels(docTypeId, docId, important)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub UpdateFavoriteLabel(ByVal result As IResult)
        If result IsNot Nothing Then
            Try
                DocumentLabelsData.UpdateFavoriteLabels(result.DocTypeId, result.ID, result.IsFavorite, Membership.MembershipHelper.CurrentUser.ID)
            Catch ex As Exception
                If result IsNot Nothing Then
                    Throw New Exception("Ha ocurrido un error al modificar la marca de favorito del documento " & result.Name, ex)
                Else
                    Throw ex
                End If
            End Try
        End If
    End Sub



    Public Shared Sub FillResultLabels(ByRef result As IResult)
        Try
            'Se obtienen las etiquetas del documento filtradas por el usuario
            Dim dsDocumentLabels As DataSet = DocumentLabelsData.GetDocumentLabelsByUser(Membership.MembershipHelper.CurrentUser.ID, result.ID)

            result.IsImportant = False
            result.IsFavorite = False

            If dsDocumentLabels IsNot Nothing AndAlso dsDocumentLabels.Tables.Count > 0 Then
                For Each r As DataRow In dsDocumentLabels.Tables(0).Rows

                    If r("LABEL").ToString = "IMPORTANCE" AndAlso CInt(r("FLAG")) > 0 Then result.IsImportant = True
                    If r("LABEL").ToString = "FAVORITE" AndAlso CInt(r("FLAG")) > 0 Then result.IsFavorite = True
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

End Class
