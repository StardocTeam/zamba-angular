Imports Zamba.Servers
Imports System.Data.SqlClient
Imports Zamba.Core

Public Class DocTypesRDocTypesFactoryExt

    ''' <summary>
    ''' Obtiene todas las relaciones entre entidades, ordenadas y armadas para juntarlas por cada entidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllDocTypesRDocTypes() As DataTable
        Return Server.Con.ExecuteDataset("zsp_doctypes_100_getDocTypesAsociations").Tables(0)
    End Function

    ''' <summary>
    ''' Obtiene todos los ids de las entidades que tengan al menos 1 asociacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllDocTypesIDHaveAsociations() As DataTable
        Return Server.Con.ExecuteDataset("zsp_doctypes_100_getEntitiesHaveAsociations").Tables(0)
    End Function
End Class
