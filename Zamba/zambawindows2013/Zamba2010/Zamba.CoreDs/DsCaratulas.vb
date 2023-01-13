Public Class DsCaratulas
    Inherits System.Data.DataSet
    'Este Dataset es que se carga en el Modulo de Caratulas mediante busquedas  
	Partial Class ZBarCodeDataTable
        Private Sub ZBarCodeDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = FechaColumn.ColumnName) Then

            End If
        End Sub
    End Class
End Class