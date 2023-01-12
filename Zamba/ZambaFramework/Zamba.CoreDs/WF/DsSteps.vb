Partial Class DsSteps
    Partial Class WFStepsDataTable

        Private Sub WFStepsDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.LocationXColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
