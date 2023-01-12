Imports Zamba.Servers
Public Class Volumenes
    Implements IDisposable
    Public Sub Dispose() Implements System.IDisposable.Dispose

    End Sub
    Public Shared Function GetVolumenes() As DataSet
        Dim sql As String
        Dim ds As New dsvols
        Dim dstemp As DataSet
        Dim i As Int32
        sql = "Select * from volumenesview"
        dstemp = Server.Con.ExecuteDataset(CommandType.Text, sql)
        For i = 0 To dstemp.Tables(0).Rows.Count - 1
            Dim row As dsvols.dsvolsRow = ds.dsvols.NewdsvolsRow
            row.Lista = dstemp.Tables(0).Rows(i).Item(0).ToString()
            row.Volumen = dstemp.Tables(0).Rows(i).Item(1).ToString()
            row.Ruta = dstemp.Tables(0).Rows(i).Item(2).ToString()
            row.Archivos = dstemp.Tables(0).Rows(i).Item(3)
            row.MB = dstemp.Tables(0).Rows(i).Item(4)
            row.Libre = dstemp.Tables(0).Rows(i).Item(5)
            If IO.File.Exists(dstemp.Tables(0).Rows(i).Item(2).ToString() & "\volid.txt") = False Then
                row.Observaciones = "s/conexion"
            End If
            ds.dsvols.Rows.Add(row)
        Next
        dstemp.Dispose()
        Return ds
    End Function

End Class
