Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Autos
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function ObtenerVehiculoPropuestaDesdeSISE(ByVal NroPropuesta As Integer, ByVal NroItem As Integer, ByRef Er As String) As Auto
        'Threading.Thread.Sleep(2000)
        If NroPropuesta.ToString().EndsWith("1") Then
            Return New Auto(11, 22, 34500, 2007, "ABC123", "Chevrolet Corsa", 33)
        ElseIf NroItem = 6 Then
            Er = "Ha ocurrido un error en el servicio, el nro de item es igual a 6"
            Return New Auto(0, 0, 0, 0, String.Empty, String.Empty, 0)
        Else
            Return New Auto(1, 22, 50100, 2011, "MJF555", "Renault", 12)
        End If
    End Function

End Class