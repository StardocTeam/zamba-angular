Imports Zamba.Data

Public Class DocTypesRDocTypesBusinessExt
    ''' <summary>
    ''' Obtiene todas las relaciones entre entidades, ordenadas y armadas para juntarlas por cada entidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllDocTypesRDocTypes() As DataTable

        Return DocTypesRDocTypesFactoryExt.GetAllDocTypesRDocTypes

    End Function

    ''' <summary>
    ''' Obtiene todos los ids de las entidades que tengan al menos 1 asociacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllDocTypesIDHaveAsociations() As List(Of Long)
        Dim dt As DataTable = DocTypesRDocTypesFactoryExt.GetAllDocTypesIDHaveAsociations()
        Dim list As New List(Of Long)

        If dt IsNot Nothing Then
            For Each row As DataRow In dt.Rows
                list.Add(Long.Parse(row(0).ToString()))
            Next
        End If
        Return list
    End Function

End Class
