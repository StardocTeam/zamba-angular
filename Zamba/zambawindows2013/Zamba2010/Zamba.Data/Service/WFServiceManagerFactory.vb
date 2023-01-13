Imports Zamba.Core

Public Class WFServiceManagerFactory
#Region "Update"
    Public Function getOptions() As DataSet
        Return Server.Con.ExecuteDataset(CommandType.Text, "SELECT * FROM WF_SVMG_OPT")
    End Function

    Public Sub ServiceUpdated()

        Dim SQL As String

        SQL = "UPDATE WF_SVMG_OPT SET "
        SQL &= "    Actualizar = 0, "
        SQL &= "    Actualizado = 1, "
        SQL &= "    Version_Act = Version_UPD, "
        SQL &= "    Fecha_UPD = "

        If Server.isSQLServer Then
            SQL &= " getdate() "
        Else
            SQL &= " sysdate "
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, SQL)

    End Sub

    Public Sub ServiceUpdateFailed()

        Dim SQL As String

        SQL = "UPDATE WF_SVMG_OPT SET "

        If Server.isSQLServer Then
            SQL &= "  Fallas = IsNull(Fallas, 0) + 1 "
        Else
            SQL &= "  Fallas = NVL(Fallas, 0) + 1 "
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, SQL)

    End Sub

    Public Sub ServiceMonUpdated()

        Dim SQL As String

        SQL = "UPDATE WF_SVMG_OPT SET "
        SQL &= "    MON_Actualizar = 0, "
        SQL &= "    MON_Actualizado = 1, "
        SQL &= "    MON_Version_Act = MON_Version_UPD, "
        SQL &= "    MON_Fecha_UPD = "

        If Server.isSQLServer Then
            SQL &= " getdate() "
        Else
            SQL &= " sysdate "
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, SQL)

    End Sub

    Public Sub ServiceMonUpdateFailed()

        Dim SQL As String

        SQL = "UPDATE WF_SVMG_OPT SET "

        If Server.isSQLServer Then
            SQL &= "  Fallas = IsNull(MON_Fallas, 0) + 1 "
        Else
            SQL &= "  Fallas = NVL(MON_Fallas, 0) + 1 "
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, SQL)

    End Sub

    Public Sub MonitoreoUpdated()

        Dim SQL As String

        SQL = "UPDATE WF_SVMG_OPT SET "
        SQL &= "    MON_Actualizar = 0, "
        SQL &= "    MON_Actualizado = 1, "
        SQL &= "    MON_Version_Act = Version_UPD, "
        SQL &= "    MON_Fecha_UPD = "

        If Server.isSQLServer Then
            SQL &= " getdate() "
        Else
            SQL &= " sysdate "
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, SQL)

    End Sub

    Public Sub MonitoreoUpdateFailed()

        Dim SQL As String

        SQL = "UPDATE WF_SVMG_OPT SET "

        If Server.isSQLServer Then
            SQL &= "  MON_Fallas = IsNull(Fallas, 0) + 1 "
        Else
            SQL &= "  MON_Fallas = NVL(Fallas, 0) + 1 "
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, SQL)

    End Sub
#End Region

    ''' <summary>
    ''' Obtiene todas las acciones activas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetActiveAction() As DataSet
        If Server.isOracle = True Then
           Return Server.Con.ExecuteDataset(CommandType.Text, "select * from ZServiceActions where active = 1")
        Else
            Dim parValues() As Object = {}
            Return Server.Con.ExecuteDataset("zsp_100_srv_GetServiceActiveActions", parValues)
        End If
    End Function

    Public Shared Function GetNextAction() As DataSet
        If Server.isOracle = True Then
            Dim sqlquery As String = "select * from ZServiceActions where active = 0 and actionID not in (2,3) and rownum = 1 order by id asc"
            Return Server.Con.ExecuteDataset(CommandType.Text, sqlquery)
        Else
            Dim parValues() As Object = {}
            Return Server.Con.ExecuteDataset("zsp_100_srv_GetServiceNextAction", parValues)
        End If
    End Function

    Public Shared Sub ActiveAction(ByVal id As Int16)
        If Server.isOracle Then
            Dim sqlquery As String = "update ZServiceActions set active = 1 where id = " & id.ToString
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlquery)
        Else
            Dim parValues() As Object = {id}
            Server.Con.ExecuteNonQuery("zsp_100_srv_UpdateServiceActiveAction", parValues)
        End If
    End Sub

    Public Shared Sub DeleteAction(ByVal id As Int16)
        If Server.isOracle Then
            Dim sqlquery As String = "delete from ZServiceActions where id = " & id.ToString
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlquery)
        Else
            Dim parValues() As Object = {id}
            Server.Con.ExecuteNonQuery("zsp_100_srv_UpdateServiceDeleteAction", parValues)
        End If
    End Sub

    ''' <summary>
    ''' Borra todas las acciones pendientes
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub DeleteAllActions()
        Dim sqlquery As String = "delete from ZServiceActions"
        Server.Con.ExecuteNonQuery(CommandType.Text, sqlquery)
    End Sub

    Public Shared Sub InsertAction(ByVal serviceId As Int32, ByVal action As ServiceManagerAction)
        If Server.isOracle Then
            Dim sqlquery As String = "insert into ZServiceActions (id,serviceID,actionID,active) values (ZSERVICEACTIONS_seq.NEXTVAL," & serviceId.ToString & "," & action & ", 0)"
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlquery)
        Else
            Dim parValues() As Object = {serviceId, action}
            Server.Con.ExecuteNonQuery("zsp_100_srv_UpdateServiceInsertAction", parValues)
        End If
    End Sub

    Public Shared Function GetServiceAction(ByVal serviceID As Int32) As DataSet
        If Server.isOracle Then
            Dim sqlquery As String = "select * from ZServiceActions where ROWNUM<=1 AND active = 0 and actionID in (2,3) and serviceid = " & serviceID.ToString
            Return Server.Con.ExecuteDataset(CommandType.Text, sqlquery)
        Else
            Dim parValues() As Object = {serviceID}
            Return Server.Con.ExecuteDataset("zsp_100_srv_GetServiceActions", parValues)
        End If
    End Function

    Public Shared Function GetServicesPendingActions() As DataSet
        If Server.isOracle Then
            Return Server.Con.ExecuteDataset(CommandType.Text, "select ac.ID as " & Chr(34) & "ID Accion" & Chr(34) & ", serv.serviceID as " & Chr(34) & "ID Servicio" & Chr(34) & ", serv.Description, descri.Description as " & Chr(34) & "Accion" & Chr(34) & ", ac.Active as " & Chr(34) & "Activo" & Chr(34) & " from ZServiceActions ac inner join ZService serv on serv.serviceid=ac.serviceid inner join ZServiceActionsDesc descri on ac.actionid=descri.actionid order by ac.id asc")
        Else
            Dim parValues() As Object = {}
            Return Server.Con.ExecuteDataset("zsp_100_srv_GetServicePendingActions", parValues)
        End If
    End Function
End Class