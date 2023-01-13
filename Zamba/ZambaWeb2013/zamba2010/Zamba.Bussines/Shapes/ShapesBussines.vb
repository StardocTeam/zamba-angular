Imports Zamba.Data
Public Class ShapesBusiness
    Public Shared Function ExistsId(ByVal id As Integer) As Boolean
        Return ShapesFactory.ExistsId(id)
    End Function
End Class
