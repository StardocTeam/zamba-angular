Public Class ShapesFactory
    Public Shared Function ExistsId(ByVal id As Integer) As Boolean
        Dim strSelect As String = "Select * From ZNetronShapes Where Shape_Id = " + id.ToString + " And Shape_Tipo <> 4"
        Try
            Dim DsId As DataSet = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            If DsId.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch
            Return False
        End Try
    End Function
End Class
