Imports Zamba.Data
Imports Zamba.Tools

Public Class ServiceBusiness
    Public Shared Function getValue(ByVal serviceID As Int64, ByVal name As String) As Object
        Return ServiceFactory.getValue(serviceID, name)
    End Function

    Public Shared Sub setValue(ByVal serviceID As Int64, ByVal name As String, ByVal value As String)
        If IsNothing(ServiceFactory.getValue(serviceID, name)) Then
            ServiceFactory.insertValue(serviceID, name, value)
        Else
            ServiceFactory.updateValue(serviceID, name, value)
        End If
    End Sub

    Public Shared Function getIniValue(ByVal name As String, ByVal defaultValue As String) As String
        ZTrace.WriteLineIf(ZTrace.IsError, "Ruta de Service.ini: " & System.Windows.Forms.Application.StartupPath & "\service.ini")
        Return INIClass.ReadIni(System.Windows.Forms.Application.StartupPath & "\service.ini", "Service", name, "0")
    End Function

    Public Shared Function getServiceName(ByVal serviceID As Int64) As String
        Return ServiceFactory.getServiceName(serviceID)
    End Function

    ''' <summary>
    ''' Obtiene todos los servicios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetServices() As List(Of ServiceObj)
        Dim ds As DataSet = ServiceFactory.getServices()
        Dim services As New List(Of ServiceObj)
        Dim isZambaUser As Boolean = UserBusiness.IsZambaUser

        For Each dr As DataRow In ds.Tables(0).Rows
            Dim service As New ServiceObj(dr("serviceid"), dr("name"), dr("type"), dr("description"))

            services.Add(service)

        Next

        Return services
    End Function

    ''' <summary>
    ''' Obtiene todos los servicios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetServiceByID(ByVal ID As Int32) As ServiceObj
        Dim ds As DataSet = ServiceFactory.getServiceByID(ID)
        Dim service As ServiceObj

        For Each dr As DataRow In ds.Tables(0).Rows
            service = New ServiceObj(dr("serviceid"), dr("name"), dr("type"), dr("description"))
        Next

        Return service
    End Function

    ''' <summary>
    ''' Obtiene los servicios del tipo especificado
    ''' </summary>
    ''' <param name="serviceType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetServices(ByVal serviceType As ServiceTypes) As List(Of ServiceObj)
        Dim ds As DataSet = ServiceFactory.getServices(serviceType)
        Dim services As New List(Of ServiceObj)

        For Each dr As DataRow In ds.Tables(0).Rows
            Dim service As New ServiceObj(dr("serviceid"), dr("name"), dr("type"), dr("description"))
            services.Add(service)
        Next

        Return services
    End Function

    Public Shared Sub InsertService(ByVal serviceID As Int64, ByVal type As ServiceTypes, ByVal name As String, ByVal description As String)
        ServiceFactory.insertService(serviceID, type, name, description)
    End Sub

    Public Shared Sub InsertServiceDate(ByVal serviceID As Int64)
        ServiceFactory.insertServiceDate(serviceID)
    End Sub

    ''' <summary>
    ''' Actualiza la hora en que se ejecuto el servicio y la UCM
    ''' </summary>
    ''' <param name="serviceID"></param>
    ''' <param name="serviceType"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateServiceDate(ByVal serviceID As Int64, ByVal serviceType As ServiceTypes)
        'Actualizo la UCM
        Dim timeout As Integer = UserPreferences.getValue("TimeOut", UPSections.UserPreferences, "20")
        Ucm.UpdateOrInsertActionTime(timeout, serviceType)

        'Actualizo la ZServiceDates
        ServiceFactory.updateServiceDate(serviceID)
    End Sub

    Public Shared Sub DeleteService(ByVal serviceID As Int64)
        ServiceFactory.deleteService(serviceID)
    End Sub

    Public Shared Function GetServiceTrace(moduleName As String, lastID As Int64, rows As Int32, order As String) As DataTable
        Return ServiceFactory.GetServiceTraceByModuleNameAndLastID(moduleName, lastID, rows, order)
    End Function

    Public Shared Function GetMachinesNames() As DataTable
        Dim tempDS As DataSet = ServiceFactory.GetMachinesNames()
        If tempDS IsNot Nothing AndAlso tempDS.Tables.Count > 0 Then
            If tempDS.Tables(0).Rows.Count > 0 Then
                Return tempDS.Tables(0)
            End If
        End If
        Return Nothing
    End Function
End Class
