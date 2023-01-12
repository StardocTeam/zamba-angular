Imports Zamba.Data

Public Class ServiceBusinessExt

    ''' <summary>
    ''' Obtiene la configuracion de un servicio
    ''' </summary>
    ''' <param name="serviceId"></param>
    ''' <returns>Datatable con la configuración de un servicio</returns>
    ''' <remarks></remarks>
    Public Function GetServiceSettings(ByVal serviceId As Int32) As DataTable
        Dim serviceFactoryExt As New ServiceFactoryExt()
        Dim dtServiceSettings As DataTable = serviceFactoryExt.GetServiceSettings(serviceId)
        serviceFactoryExt = Nothing
        Return dtServiceSettings
    End Function

End Class
