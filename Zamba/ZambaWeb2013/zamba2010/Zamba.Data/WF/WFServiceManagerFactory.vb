Public Class WFServiceManagerFactory

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

    Public Shared Function GetServiceName(ByVal serviceid As Int16) As String
        Dim sqlquery As String = "select descripcion from wf_svmg_services where id = " & serviceid
        Return Server.Con.ExecuteScalar(CommandType.Text, sqlquery)
    End Function

    Public Shared Function GetServiceID(ByVal servicename As String) As Int16
        Dim sqlquery As String = "select id from wf_svmg_services where lower(descripcion) = '" & servicename.Trim.ToLower & "'"
        Dim serviceid As String = Server.Con.ExecuteScalar(CommandType.Text, sqlquery)
        If String.IsNullOrEmpty(serviceid) Then
            Return Nothing
        End If
        Return CShort(serviceid)
    End Function

    Public Shared Function GetActiveAction() As DataSet
        Dim sqlquery As String = "select * from wf_svmg_actions where active = 1"
        Return Server.Con.ExecuteDataset(CommandType.Text, sqlquery)
    End Function

    Public Shared Function GetNextAction() As DataSet
        Dim sqlquery As String = "select top 1 * from wf_svmg_actions where active = 0 and action not in (2,3) order by id asc"
        Return Server.Con.ExecuteDataset(CommandType.Text, sqlquery)
    End Function

    Public Shared Sub ActiveAction(ByVal id As Int16)
        Dim sqlquery As String = "update wf_svmg_actions set active = 1 where id = " & id.ToString
        Server.Con.ExecuteNonQuery(CommandType.Text, sqlquery)
    End Sub

    Public Shared Sub DeleteAction(ByVal id As Int16)
        Dim sqlquery As String = "delete from wf_svmg_actions where id = " & id.ToString
        Server.Con.ExecuteNonQuery(CommandType.Text, sqlquery)
    End Sub

    Public Shared Sub InsertAction(ByVal type As Int16, ByVal action As Int16)
        Dim sqlquery As String = "insert into wf_svmg_actions (type,action) values (" & type.ToString & "," & action.ToString & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, sqlquery)
    End Sub

    Public Shared Function GetServiceAction(ByVal type As Int16) As DataSet
        Dim sqlquery As String = "select top 1 * from wf_svmg_actions where active = 0 and action in (2,3) and type = " & type.ToString & " order by id asc"
        Return Server.Con.ExecuteDataset(CommandType.Text, sqlquery)
    End Function

End Class
