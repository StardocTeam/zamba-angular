Imports Zamba.Servers

Public Class ServiceFactoryExt

    ''' <summary>
    ''' Obtiene la configuracion de un servicio
    ''' </summary>
    ''' <param name="serviceId"></param>
    ''' <returns>Datatable con la configuración de un servicio</returns>
    ''' <remarks></remarks>
    Public Function GetServiceSettings(ByVal serviceId As Int32) As DataTable
        If Server.isOracle Then
            Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT name, value FROM ZServiceOptions where serviceid=" & serviceId.ToString()).Tables(0)
        Else
            Return Server.Con.ExecuteDataset("zsp_service_100_GetSettings", New Object() {serviceId}).Tables(0)
        End If
    End Function
End Class
