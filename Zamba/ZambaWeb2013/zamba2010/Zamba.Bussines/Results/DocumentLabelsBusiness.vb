Imports System.Collections.Generic
Imports System.Globalization
Imports Zamba.Core
Imports Zamba.Data

Public Class DocumentLabelsBusiness

    Dim DocumentLabelsData As New DocumentLabelsData
    Public Sub UpdateImportanceLabel(ByVal result As IResult)
        If result IsNot Nothing Then
            Try
                DocumentLabelsData.UpdateImportantLabels(result.DocTypeId, result.ID, result.IsImportant, result.UserId)
            Catch ex As Exception
                If result IsNot Nothing Then
                    Throw New Exception("Ha ocurrido un error al modificar la marca de importancia del documento " & result.Name, ex)
                Else
                    Throw ex
                End If
            End Try
        End If
    End Sub


    'VALIDAR ELIMINACION YA QUE NADIE LO INVOCA
    Public Sub UpdateImportantLabels(ByVal docTypeId As Int64, ByVal docId As Int64, ByVal important As Boolean)
        Try
            DocumentLabelsData.UpdateImportantLabels(docTypeId, docId, important, 0)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub UpdateFavoriteLabel(ByVal result As IResult)
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



    Public Sub FillResultLabels(ByRef result As IResult)
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


    Public Function GetBookmarks(ByVal UserId As Int64) As List(Of TaskDTO)
        Dim newsDataset As DataSet = DocumentLabelsData.GetBookmarks(UserId)
        Dim Tasks As New List(Of TaskDTO)
        If newsDataset IsNot Nothing AndAlso newsDataset.Tables(0) IsNot Nothing Then
            Dim cultureInfo As New CultureInfo("es-AR")
            For Each row As DataRow In newsDataset.Tables(0).Rows
                Dim Task_id As Long
                Dim Etapa As String = String.Empty
                Dim Ingreso As New System.DateTime()
                Dim Vencimiento As New System.DateTime()
                If IsDBNull(row("Task_id")) = False Then
                    Task_id = row("Task_id")
                End If
                If IsDBNull(row("Etapa")) = False Then
                    Etapa = row("Etapa")
                End If
                If IsDBNull(row("Ingreso")) = False Then
                    Ingreso = DateTime.Parse(row("Ingreso"), cultureInfo)
                End If
                If IsDBNull(row("Vencimiento")) = False Then
                    Vencimiento = DateTime.Parse(row("Vencimiento"), cultureInfo)
                End If
                Tasks.Add(New TaskDTO(row("Tarea"), Task_id, row("docid"), row("doctypeid"),
                                      DateTime.Parse(row("Fecha"), cultureInfo), Etapa, row("Asignado"), Ingreso, Vencimiento))
            Next
        End If
        Return Tasks
    End Function

    Public Function GetBookmarksCount(UserId As Long) As Long
        Return DocumentLabelsData.GetBookmarksCount(UserId)
    End Function

    Public Function GetStarsCount(UserId As Long) As Long
        Return DocumentLabelsData.GetStarsCount(UserId)
    End Function

    Public Function GetStars(ByVal UserId As Int64) As List(Of TaskDTO)
        Dim newsDataset As DataSet = DocumentLabelsData.GetStars(UserId)
        Dim Tasks As New List(Of TaskDTO)
        Try

            If newsDataset IsNot Nothing AndAlso newsDataset.Tables(0) IsNot Nothing Then
                Dim cultureInfo As New CultureInfo("es-AR")
                For Each row As DataRow In newsDataset.Tables(0).Rows
                    Dim Task_id As Long
                    Dim Etapa As String = String.Empty
                    Dim Ingreso As New System.DateTime()
                    Dim Vencimiento As New System.DateTime()
                    If IsDBNull(row("Task_id")) = False Then
                        Task_id = row("Task_id")
                    End If
                    If IsDBNull(row("Etapa")) = False Then
                        Etapa = row("Etapa")
                    End If
                    If IsDBNull(row("Ingreso")) = False Then
                        Ingreso = DateTime.Parse(row("Ingreso"), cultureInfo)
                    End If
                    If IsDBNull(row("Vencimiento")) = False Then
                        Vencimiento = DateTime.Parse(row("Vencimiento"), cultureInfo)
                    End If
                    Tasks.Add(New TaskDTO(row("Tarea"), Task_id, row("docid"), row("doctypeid"),
                                          DateTime.Parse(row("Fecha"), cultureInfo), Etapa, row("Asignado"), Ingreso, Vencimiento))
                Next
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return Tasks
    End Function
End Class
