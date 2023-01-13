Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports Zamba.Servers

Public Class AgentFactoryExt

    ''' <summary>
    ''' Agrega un registro del reporte de licencias
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <param name="cTime"></param>
    ''' <param name="uTime"></param>
    ''' <param name="winUser"></param>
    ''' <param name="winPc"></param>
    ''' <param name="conId"></param>
    ''' <param name="timeOut"></param>
    ''' <param name="type"></param>
    ''' <param name="client"></param>
    ''' <param name="dsDate"></param>
    ''' <param name="serverCon"></param>
    ''' <param name="dataBase"></param>
    Public Sub AddUcmClientData(userId As String, _
                                cTime As String, _
                                uTime As String, _
                                winUser As String, _
                                winPc As String, _
                                conId As String, _
                                timeOut As String, _
                                type As String, _
                                client As String, _
                                dsDate As DateTime, _
                                serverCon As String, _
                                dataBase As String)
        'Solamente se llama para cargar correctamente el app.ini
        Dim query As String = Server.AppConfig.SERVER
        query = "INSERT INTO [UCMCLIENTSSet] ([USER_ID],[C_TIME],[U_TIME],[WINUSER],[WINPC],[CON_ID],[TIME_OUT],[TYPE],[Client],Server,Base,[UpdateDate]) VALUES (" _
            & userId & "," _
            & Server.Con.ConvertDateTime(cTime) & "," _
            & Server.Con.ConvertDateTime(uTime) & ",'" _
            & winUser & "','" _
            & winPc & "'," _
            & conId & "," _
            & timeOut & "," _
            & type & ",'" _
            & client & "','" _
            & serverCon & "','" _
            & dataBase & "'," _
            & Server.Con.ConvertDateTime(dsDate.ToString()) & ")"

        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    ''' <summary>
    ''' Agrega un registro del reporte de ILM
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <param name="userName"></param>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <param name="day"></param>
    ''' <param name="hour"></param>
    ''' <param name="crdate"></param>
    ''' <param name="client"></param>
    ''' <param name="codigoMail"></param>
    ''' <param name="docId"></param>
    ''' <param name="docTypeId"></param>
    ''' <remarks></remarks>
    Public Sub AddILMClientData(userId As String, _
                                userName As String, _
                                year As String, _
                                month As String, _
                                day As String, _
                                hour As String, _
                                crdate As String, _
                                client As String, _
                                codigoMail As String, _
                                docId As String, _
                                docTypeId As String)
        Dim query As String = "INSERT INTO [ILMClientSet] ([UserId],[UserName],[Year],[Month],[Day],[Hour],[Type],[UpdateDate],[CrDate],[Client],[Server],[Base],[CodigoMail],[doc_id],[DocTypeId]) values (" _
                              & userId & ",'" _
                              & userName & "'," _
                              & year & "," _
                              & month & "," _
                              & day & "," _
                              & hour & "," _
                              & 82 & "," _
                              & Server.Con.ConvertDateTime(DateTime.Now.ToString()) & "," _
                              & Server.Con.ConvertDateTime(crdate) & ",'" _
                              & client & "','" _
                              & Server.AppConfig.SERVER & "','" _
                              & Server.AppConfig.DB & "','" _
                              & codigoMail & "'," _
                              & docId & "," _
                              & docTypeId & ")"

        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    ''' <summary>
    ''' Guarda la cantidad de licencias habilitadas en un cliente
    ''' </summary>
    ''' <param name="client">Cliente. El cliente configurado en el agent debe ser igual al de la tabla Clients.</param>
    ''' <param name="licenseType">Tipo de licencia. El id del enumerador debe ser igual al de la tabla Licenses.</param>
    ''' <param name="count">Cantidad de licencias. Debe estar desencriptado.</param>
    ''' <remarks></remarks>
    Public Function SaveEnabledLicCount(ByVal client As String, ByVal licenseType As Int32, ByVal count As String) As Int32
        Return Server.Con.ExecuteNonQuery("ZSP_LICENSE_100_SaveEnabledLicCount", New Object() {client, licenseType, count})
    End Function

    ''' <summary>
    ''' Ejecuta el reporte de licencias de usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>En este caso solo se utiliza para test unitario</remarks>
    Public Function GetUsersReport() As DataSet
        If Server.isOracle Then
            Dim query As String = "select nombres, apellido, Name,ID,C_TIME,U_TIME,WINUSER,WINPC,CON_ID,TIME_OUT,TYPE from ucm c inner join usrtable u on c.user_id = u.id"
            Return Server.Con.ExecuteDataset(CommandType.Text, query)
        Else
            Return Server.Con.ExecuteDataset("zsp_agent_100_GetUsersData")
        End If
    End Function

    ''' <summary>
    ''' Obtiene los reportes ILM
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>En este caso solo se utiliza para test unitario</remarks>
    Public Function GetILM() As DataSet
        If Server.isOracle Then
            Dim query As String
            query = "SELECT [Fecha], [UserId] FROM [ZExportControl]"
            query &= "WHERE to_number(to_char(Fecha, 'yyyy')) = to_number(to_char(sysdate, 'yyyy'))"
            query &= "AND to_number(to_char(Fecha, 'MM')) = to_number(to_char(sysdate, 'MM'))"
            query &= "AND to_number(to_char(Fecha, 'dd')) = to_number(to_char(sysdate, 'dd'))"
            query &= "AND to_number(to_char(Fecha, 'HH24')) = to_number(to_char(sysdate, 'HH24')) - 1"
            query &= "ORDER BY FECHA"

            Return Server.Con.ExecuteDataset(CommandType.Text, query)
        Else
            Return Server.Con.ExecuteDataset("zsp_agent_200_GetILM")
        End If
    End Function

    ''' <summary>
    ''' Método utilizado para devolver la cantidad de pc's actualmente conectadas a Zamba (licencia del WF)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ActiveWfConnections() As Integer
        Dim count As Integer

        If Server.isOracle Then
            count = Server.Con.ExecuteScalar(CommandType.Text, "Select count(1) from UCM Where Type = 1 and winuser <> 'Servicio'")
        Else
            count = Server.Con.ExecuteScalar("ZSP_LICENSE_100_GetWfConnectionsCount")
        End If

        Return (count)
    End Function

    ''' <summary>
    ''' Método utilizado para devolver la cantidad de pc's actualmente conectadas a Zamba (licencia documental)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ActiveDocConections() As Int32
        Dim count As Integer

        If Server.isOracle Then
            count = Server.Con.ExecuteScalar(CommandType.Text, "Select count(1) from UCM Where Type <> 1 and winuser <> 'Servicio'")
        Else
            count = Server.Con.ExecuteScalar("ZSP_LICENSE_100_GetDocConectionsCount")
        End If

        Return (count)
    End Function

    ''' <summary>
    ''' Obtiene un listado con datos de conexión
    ''' </summary>
    ''' <returns>Tabla con datos de las conexiones actuales</returns>
    ''' <remarks></remarks>
    Public Function GetConnectionsReport() As DataTable
        If Server.isOracle Then
            Dim query As String = "SELECT USR.NAME, UCM.WINUSER, UCM.WINPC, UCM.TYPE, UCM.TIME_OUT, UCM.C_TIME, UCM.U_TIME FROM UCM INNER JOIN USRTABLE USR ON USR.ID = UCM.USER_ID ORDER BY USR.NAME"
            Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
        Else
            Return Server.Con.ExecuteDataset("ZSP_LICENSE_100_GetConnectionsReport").Tables(0)
        End If
    End Function
End Class
