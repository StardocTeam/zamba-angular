Imports System.Text

''' <summary>
''' Obtiene los valores del servicio de la BD
''' </summary>
''' <remarks></remarks>
Public Class ServiceFactory
    ''' <summary>
    ''' Obtiene el valor de la BD con el ID del servicio
    ''' </summary>
    ''' <param name="serviceID">Id del servicio</param>
    ''' <param name="name">Nombre de la variable</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getValue(ByVal serviceID As Int64, ByVal name As String) As Object
        If Server.isOracle Then
            Dim sqlBuilder As New StringBuilder
            Try
                sqlBuilder.Append("select value from ZserviceOptions where serviceID=")
                sqlBuilder.Append(serviceID)
                sqlBuilder.Append(" and name='")
                sqlBuilder.Append(name)
                sqlBuilder.Append("'")

                Return Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())
            Finally
                sqlBuilder = Nothing
            End Try
        Else
            Dim parValues() As Object = {serviceID, name}
            Return Server.Con.ExecuteScalar("zsp_100_srv_GetValue", parValues)
        End If
    End Function

    ''' <summary>
    ''' Inserta una nueva variable
    ''' </summary>
    ''' <param name="serviceID">Id del servicio</param>
    ''' <param name="name">Nombre de la variable</param>
    ''' <param name="value">Valor de la variable</param>
    ''' <remarks></remarks>
    Public Shared Sub insertValue(ByVal serviceID As Int64, ByVal name As String, ByVal value As String)
        If Server.isOracle Then
            Dim sqlBuilder As New StringBuilder
            Try
                sqlBuilder.Append("insert Into ZserviceOptions (serviceid,name,value) values (")
                sqlBuilder.Append(serviceID)
                sqlBuilder.Append(",'")
                sqlBuilder.Append(name)
                sqlBuilder.Append("','")
                sqlBuilder.Append(value)
                sqlBuilder.Append("')")

                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            Finally
                sqlBuilder = Nothing
            End Try
        Else
            Dim parValues() As Object = {serviceID, name, value}
            Server.Con.ExecuteNonQuery("zsp_100_srv_InsertValue", parValues)
        End If
    End Sub

    ''' <summary>
    ''' Actualizar una variable
    ''' </summary>
    ''' <param name="serviceID">Id del servicio</param>
    ''' <param name="name">Nombre de la variable</param>
    ''' <param name="value">Valor de la variable</param>
    ''' <remarks></remarks>
    Public Shared Sub updateValue(ByVal serviceID As Int64, ByVal name As String, ByVal value As String)
        If Server.isOracle Then
            Dim sqlBuilder As New StringBuilder
            Try
                sqlBuilder.Append("update ZserviceOptions set value='")
                sqlBuilder.Append(value)
                sqlBuilder.Append("' where serviceID = ")
                sqlBuilder.Append(serviceID)
                sqlBuilder.Append(" and name='")
                sqlBuilder.Append(name)
                sqlBuilder.Append("'")

                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            Finally
                sqlBuilder = Nothing
            End Try
        Else
            Dim parValues() As Object = {serviceID, name, value}
            Server.Con.ExecuteNonQuery("zsp_100_srv_UpdateValue", parValues)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el nombre del servicio
    ''' </summary>
    ''' <param name="serviceID">Id del servicio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getServiceName(ByVal serviceID As Int64) As String
        If Server.isOracle Then
            Dim sqlBuilder As New StringBuilder
            Try
                sqlBuilder.Append("select name from Zservice where serviceID=")
                sqlBuilder.Append(serviceID)

                Return Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())
            Finally
                sqlBuilder = Nothing
            End Try
        Else
            Dim parValues() As Object = {serviceID}
            Return Server.Con.ExecuteScalar("zsp_100_srv_getServiceName", parValues)
        End If
    End Function

    Public Shared Function GetMachinesNames() As DataSet
        Dim query As New StringBuilder
        query.Append("select M_Name as MachineName from estreg")
        Return Server.Con.ExecuteDataset(CommandType.Text, query.ToString())
    End Function

    Public Shared Function GetServiceTraceByModuleNameAndLastID(moduleName As String, lastID As Int64, rows As Int32, order As String) As DataTable

        Dim query As New StringBuilder
        query.Append(String.Format("Select TOP {0} * from ZTrace ", rows))
        query.Append("where ModuleName = '")
        query.Append(moduleName)
        query.Append("' and ID > ")
        query.Append(lastID)
        query.Append(String.Format(" order by ID {0}", order))

        Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString())

        If dstemp IsNot Nothing AndAlso dstemp.Tables.Count > 0 Then
            Return dstemp.Tables(0)
        End If
        Return Nothing

    End Function

    ''' <summary>
    ''' Obtiene los servicios
    ''' </summary>
    ''' <returns>Listado de servicios</returns>
    ''' <remarks></remarks>
    Public Shared Function getServices() As DataSet
        If Server.isOracle Then
            Return Server.Con.ExecuteDataset(CommandType.Text, "select * from Zservice")
        Else
            Dim parValues() As Object = {}
            Return Server.Con.ExecuteDataset("zsp_100_srv_getServices", parValues)
        End If
    End Function

    ''' <summary>
    ''' Obtiene los servicios
    ''' </summary>
    ''' <returns>Listado de servicios</returns>
    ''' <remarks></remarks>
    Public Shared Function getServiceByID(ByVal serviceID As Int32) As DataSet
        If Server.isOracle Then
            Return Server.Con.ExecuteDataset(CommandType.Text, "select * from Zservice where serviceID=" & serviceID)
        Else
            Dim parValues() As Object = {serviceID}
            Return Server.Con.ExecuteDataset("zsp_100_srv_getServiceByID", parValues)
        End If
    End Function

    ''' <summary>
    ''' Obtiene los servicios del tipo especificados
    ''' </summary>
    ''' <returns>Listado de servicios</returns>
    ''' <remarks></remarks>
    Public Shared Function getServices(ByVal serviceType As Int64) As DataSet
        If Server.isOracle Then
            Return Server.Con.ExecuteDataset(CommandType.Text, "select * from Zservice where type=" & serviceType)
        Else
            Dim parValues() As Object = {serviceType}
            Return Server.Con.ExecuteDataset("zsp_100_srv_getServicesByType", parValues)
        End If
    End Function

    ''' <summary>
    ''' Borra el servicio
    ''' </summary>
    ''' <param name="serviceID">ID del servicio</param>
    ''' <remarks></remarks>
    Public Shared Sub deleteService(ByVal serviceID As Int64)

        If Server.isOracle Then


            Dim sqlBuilder As New StringBuilder
            Try
                Dim strdelete As String = String.Format("delete Zserviceoptions where serviceID= {0}", serviceID)
                Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)

                sqlBuilder.Append("delete Zservice where serviceID=")
                sqlBuilder.Append(serviceID)
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            Finally
                sqlBuilder = Nothing
            End Try
        Else
            Dim parValues() As Object = {serviceID}
            Server.Con.ExecuteNonQuery("zsp_100_srv_deleteService", parValues)
        End If
    End Sub

    ''' <summary>
    ''' Agrega el servicio
    ''' </summary>
    ''' <param name="serviceID">ID del servicio</param>
    ''' <remarks></remarks>
    Public Shared Sub insertService(ByVal serviceID As Int64, ByVal type As String, ByVal name As String, ByVal description As String)
        If Server.isOracle Then
            Dim sqlBuilder As New StringBuilder
            Try
                sqlBuilder.Append("insert into Zservice (serviceid,type,name,description) values (")
                sqlBuilder.Append(serviceID)
                sqlBuilder.Append(",")
                sqlBuilder.Append(type)
                sqlBuilder.Append(",'")
                sqlBuilder.Append(name)
                sqlBuilder.Append("','")
                sqlBuilder.Append(description)
                sqlBuilder.Append("')")

                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            Finally
                sqlBuilder = Nothing
            End Try
        Else
            Dim parValues() As Object = {serviceID, name, type, description}
            Server.Con.ExecuteNonQuery("zsp_100_srv_insertService", parValues)
        End If
    End Sub

    ''' <summary>
    ''' Agrega el servicio
    ''' </summary>
    ''' <param name="serviceID">ID del servicio</param>
    ''' <remarks></remarks>
    Public Shared Sub insertServiceDate(ByVal serviceID As Int64)
        If Server.isOracle Then
            Dim sqlBuilder As New StringBuilder
            Try
                sqlBuilder.Append("insert into ZServiceDates (serviceId) values (")
                sqlBuilder.Append(serviceID)
                sqlBuilder.Append(")")

                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            Finally
                sqlBuilder = Nothing
            End Try
        Else
            Dim parValues() As Object = {serviceID}
            Server.Con.ExecuteNonQuery("zsp_100_srv_insertServiceDate", parValues)
        End If
    End Sub

    ''' <summary>
    ''' Agrega el servicio
    ''' </summary>
    ''' <param name="serviceID">ID del servicio</param>
    ''' <remarks></remarks>
    Public Shared Sub updateServiceDate(ByVal serviceID As Int64)
        If Server.isOracle Then
            Dim sqlBuilder As New StringBuilder
            Try
                sqlBuilder.Append("update ZServiceDates set lastRunDate=sysdate where serviceId =")
                sqlBuilder.Append(serviceID)

                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            Finally
                sqlBuilder = Nothing
            End Try
        Else
            Dim parValues() As Object = {serviceID}
            Server.Con.ExecuteNonQuery("zsp_100_srv_updateServiceDate", parValues)
        End If
    End Sub
End Class