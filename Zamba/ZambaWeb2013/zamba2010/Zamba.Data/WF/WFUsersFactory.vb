Imports Zamba.Core
Imports Zamba.Data
Imports Zamba.Servers

Public Class WFUsersFactory
    Public Shared Function GetAsignedUsersCountByStep(ByVal stepid As Int64) As Int32
        Dim UsersCounts As New Int32
        Dim parvalues() As Object = {stepid}
        UsersCounts = Server.Con.ExecuteScalar("sp_GetAsignedUsersCountByStep", parvalues)
        Return UsersCounts
    End Function
    Public Shared Function GetAsignedUsersCountByWorkflow(ByVal workflowid As Int64) As Int32
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteScalar("sp_GetAsignedUsersCountWorkflow", parvalues)
    End Function

    ''' <summary>
    ''' Método que sirve para obtener los usuarios dependiendo el id de la etapa
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/06/2008	Modified    Consulta desde código fuente para Oracle y vista para sql
    ''' </history>
    Public Shared Function GetUsersByStepID(ByVal stepId As Int64) As DataSet

        Dim dsFull As DataSet = Nothing
        Dim query As New System.Text.StringBuilder

        If Server.IsOracle Then

            query.Append("SELECT * FROM ")
            query.Append("(SELECT WFSTEP.STEP_ID,USRTABLE.ID, USRTABLE.NAME ")
            query.Append("FROM USR_RIGHTS INNER JOIN ")
            query.Append("WFStep ON USR_RIGHTS.ADITIONAL = WFStep.step_Id INNER JOIN ")
            query.Append("USR_R_GROUP ON USR_RIGHTS.GROUPID = USR_R_GROUP.GROUPID INNER JOIN ")
            query.Append("USRGROUP ON USR_R_GROUP.GROUPID = USRGROUP.ID AND USR_RIGHTS.GROUPID = USRGROUP.ID INNER JOIN ")
            query.Append("USRTABLE ON USR_R_GROUP.USRID = USRTABLE.ID ")
            query.Append("WHERE (USR_RIGHTS.RTYPE = 19) AND (USR_RIGHTS.OBJID = 42)) ")
            query.Append("WHERE step_id = " & stepId & " AND ID NOT IN ")
            query.Append("(SELECT ID FROM( ")
            query.Append("SELECT WFStep.step_Id, USRTABLE.ID, USRTABLE.NAME ")
            query.Append("FROM USR_RIGHTS INNER JOIN ")
            query.Append("WFStep ON USR_RIGHTS.ADITIONAL = WFStep.step_Id INNER JOIN ")
            query.Append("USRTABLE ON USR_RIGHTS.GROUPID = USRTABLE.ID ")
            query.Append("WHERE (USR_RIGHTS.RTYPE = 19) AND (USR_RIGHTS.OBJID = 42) ")
            query.Append("ORDER BY USR_RIGHTS.ADITIONAL) Where step_id = " & stepId & ")")

        Else

            query.Append("select * from zview_URBG_300 where step_id = " & stepId & " And ID not in (select ID from zview_URBU_300 where step_id = " & stepId & ")")

        End If

        Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString())
        Dim query2 As New System.Text.StringBuilder

        If Server.IsOracle Then

            query2.Append("SELECT * FROM ")
            query2.Append("(SELECT WFStep.step_Id, USRTABLE.ID, USRTABLE.NAME ")
            query2.Append("FROM USR_RIGHTS INNER JOIN ")
            query2.Append("WFStep ON USR_RIGHTS.ADITIONAL = WFStep.step_Id INNER JOIN ")
            query2.Append("USRTABLE ON USR_RIGHTS.GROUPID = USRTABLE.ID ")
            query2.Append("WHERE (USR_RIGHTS.RTYPE = 19) AND (USR_RIGHTS.OBJID = 42) ")
            query2.Append("ORDER BY USR_RIGHTS.ADITIONAL) ")
            query2.Append("Where step_id = " & stepId)

        Else

            query2.Append("select * from zview_URBU_300 where step_id = " & stepId)

        End If

        dsFull = Server.Con.ExecuteDataset(CommandType.Text, query2.ToString())

        If (dstemp.Tables.Count > 0) Then
            dstemp.Tables(0).TableName = "TablaUsuariosPorGrupos"
            dsFull.Tables(0).Merge(dstemp.Tables(0))
        End If

        Return dsFull

    End Function

    Public Shared Function GetDistinctUsersByStepID(ByVal stepId As Int64) As DataSet
        'ML: Se cambio por metodo que se usa en permisos para traer los usuarios que tienen permisos por la etapa, antes habia un metodo que intentaba no duplicar los usuarios por grupos con los usuarios que no son de los grupos
        Dim Strselect As String = "Select distinct usrtable.id,usrtable.name from usrtable,USR_RIGHTS where (aditional = " & stepId & " and usrtable.id=USR_RIGHTS.groupid) order by usrtable.name"
        Dim dsFull As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        Strselect = Nothing
        Return dsFull
    End Function

    Public Shared Function GetGroupsByStepID(ByVal stepId As Int64) As DataSet
        Dim Strselect As String = "Select distinct(id),name from USRGROUP inner join usr_Rights on usr_Rights.groupid = USRGROUP.id where ADITIONAL = " & stepId & "order by name"
        Dim dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Strselect)
        Strselect = Nothing
        Return dstemp
    End Function
End Class