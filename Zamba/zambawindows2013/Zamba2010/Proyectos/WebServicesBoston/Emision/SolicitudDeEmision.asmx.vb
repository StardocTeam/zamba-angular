Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class SolicitudDeEmision
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function ObtenerEstadoSolicitudEmisionBpm(ByVal codigoSolicitudEmision As Integer, ByRef solicitudEmision As SolicitudEmision, ByRef err As String) As Boolean
        solicitudEmision = New SolicitudEmision()
        solicitudEmision.Cotizacion = New Cotizacion(123)
        solicitudEmision.NumeroEndoso = 456
        solicitudEmision.NumeroPropuesta = 789
    End Function

End Class