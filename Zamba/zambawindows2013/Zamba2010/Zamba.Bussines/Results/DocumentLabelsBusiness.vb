Imports Zamba.Core
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

    Public Shared Sub UpdateFavoriteLabels(ByVal docTypeId As Int64, ByVal docId As Int64, ByVal favorite As Boolean, ByVal userId As Int64)
        Try
            DocumentLabelsData.UpdateFavoriteLabels(docTypeId, docId, favorite, userId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub FillResultLabels(ByRef result As IResult)
        Try
            'Se obtienen las etiquetas del documento filtradas por el usuario
            Dim dsDocumentLabels As DataSet = DocumentLabelsData.GetDocumentLabelsByUser(Membership.MembershipHelper.CurrentUser.ID, result.ID)

            'Completa las propiedades del result
            With dsDocumentLabels.Tables(0)
                If CInt(.Rows(0)("FLAG")) = 1 Then result.IsImportant = True
                If CInt(.Rows(1)("FLAG")) = 1 Then result.IsFavorite = True
            End With
        Catch ex As Exception
            If result IsNot Nothing Then
                Throw New Exception("Ha ocurrido un error al obtener las marcas del documento " & result.Name, ex)
            Else
                Throw ex
            End If
        End Try
    End Sub

End Class
