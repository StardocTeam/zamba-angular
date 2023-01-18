Imports Telerik.WinControls.UI
Imports Zamba.Core
Imports Zamba.Data

Public Class GridController

    Public Shared Sub SetColumnsVisibility(Grid As Grid.Grid.GroupGrid)

        'Dim GC As New GridColumns

        For Each Column As GridViewColumn In Grid.NewGrid.Columns

            If GridColumns.ColumnsVisibility.ContainsKey(Column.Name.ToLower()) Then

                Grid.SetColumnVisible(Column.Name, GridColumns.ColumnsVisibility(Column.Name.ToLower()))

            ElseIf Column.Name.StartsWith("I") AndAlso IsNumeric(Column.Name.Remove(0, 1)) Then

                Grid.SetColumnVisible(Column.Name, False)

            End If

        Next

        Grid.SetColumnVisible(GridColumns.ORIGINAL_FILENAME_COLUMNNAME,
                                     Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.TaskResultGridShowOriginalNameColumn)))
        Grid.SetColumnVisible(GridColumns.NUMERO_DE_VERSION_COLUMNNAME,
                                     Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.ResultGridShowVersionNumberColumn)))
        Grid.SetColumnVisible(GridColumns.VERSION_COLUMNNAME,
                                     Boolean.Parse(RightsBusiness.GetUserRights(ObjectTypes.Grids, RightsType.ResultGridShowVersionColumn)))
    End Sub

End Class
