Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<ToolboxItem(False)> _
Public Class Propuesta
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function ConvertirEnPoliza(ByVal seccion As Int16, _
                                      ByVal propuesta As Int32, _
                                      ByRef poliza As Int32, _
                                      ByRef endoso As Int32, _
                                      ByRef Er As String) _
                                      As Boolean
        poliza = 123456
        'Threading.Thread.Sleep(2000)
        Return True
    End Function

    <WebMethod()> _
    Public Function MarcarPropuestaParaEliminacion(ByVal idSeccion As Int32, _
                                                   ByVal propuesta As Int32, _
                                                   ByVal idUsuario As Int32, _
                                                   ByVal rechazoExcepcion As String, _
                                                   ByRef Er As String) _
                                                   As Boolean
        'Threading.Thread.Sleep(2000)
        Return True
    End Function

    <WebMethod(MessageName:="MarcarPropuestaParaEliminacion2")> _
    Public Function MarcarPropuestaParaEliminacion(ByVal idSeccion As Int32, _
                                                   ByVal propuesta As Int32, _
                                                   ByVal coLegajo As String, _
                                                   ByVal rechazo As String, _
                                                   ByRef Er As String) _
                                                   As Boolean
        'Threading.Thread.Sleep(2000)
        Return True
    End Function

    <WebMethod(MessageName:="ParaAysa")> _
 Public Function ObtenerStringEmpty() As String
        'Threading.Thread.Sleep(2000)
        Return String.Empty
    End Function
End Class