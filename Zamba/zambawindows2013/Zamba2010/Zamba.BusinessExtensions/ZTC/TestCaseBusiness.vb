Imports Zamba.Data

Public Class TestCaseBusiness
    Public Function GetCases(ByVal objType As Int64, _
                                      ByVal objectId As Int64) As DataTable
        Return ZTCData.GetCases(objType, objectId)
    End Function


    ''' <summary>
    ''' Obtiene los tipos generales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGeneralTypes() As DataTable
        Return ZTCData.GetGeneralTypes()
    End Function

    ''' <summary>
    ''' Obtiene los tipos generales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAsocGeneralTypes(ByVal _projectID As Int64, ByVal Type As Int64) As DataTable
        Return ZTCData.GetAsocGeneralTypes(_projectID, Type)
    End Function
End Class
