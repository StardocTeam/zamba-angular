Imports Zamba.Servers
Imports System.Text

Public Class ProjectFactory
#Region "Proyectos"

#Region "Read"
    ''' <summary>
    ''' Obtiene todos los proyectos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllProjects() As DataTable
        Dim query As String = "zsp_100_PRJ_GAPs"
        Dim ds As DataSet = Server.Con.ExecuteDataset(query, Nothing)

        If ds Is Nothing OrElse ds.Tables.Count < 1 OrElse ds.Tables(0).Rows.Count < 1 Then
            Return Nothing
        Else
            Return ds.Tables(0)
        End If
    End Function

    ''' <summary>
    ''' Obtiene un proyecto por ID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetProjectByID(ByVal id As Long) As DataRow
        Dim query As String = "zsp_100_PRJ_GPID"
        Dim ds As DataSet = Server.Con.ExecuteDataset(query, New Object() {id})

        If ds Is Nothing OrElse ds.Tables.Count < 1 OrElse ds.Tables(0).Rows.Count < 1 Then
            Return Nothing
        Else
            Return ds.Tables(0).Rows(0)
        End If
    End Function
#End Region

#Region "CUD"
    ''' <summary>
    ''' Crea un pryecto
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="name"></param>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Public Shared Sub CreateProject(ByVal id As Long, ByVal name As String, ByVal desc As String)
        Dim query As String = "zsp_100_PRJ_CP"
        Server.Con.ExecuteNonQuery(query, New Object() {id, name, desc})
    End Sub

    ''' <summary>
    ''' Edita un proyecto
    ''' </summary>
    ''' <param name="id">ID del proyecto a editar</param>
    ''' <param name="name"></param>
    ''' <param name="desc"></param>
    ''' <remarks></remarks>
    Public Shared Sub EditProject(ByVal id As Long, ByVal name As String, ByVal desc As String)
        Dim query As String = "zsp_100_PRJ_EP"
        Server.Con.ExecuteNonQuery(query, New Object() {id, name, desc})
    End Sub

    ''' <summary>
    ''' Borra un proyecto(Se recomienda antes ejecutar ReasignProjectAsociations para que no tire error de foreign key)
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteProject(ByVal id As Long)
        Dim query As String = "zsp_100_PRJ_DP"
        Server.Con.ExecuteNonQuery(query, New Object() {id})
    End Sub
#End Region

#End Region

#Region "Asociaciones de proyectos"

#Region "Read"

    ''' <summary>
    ''' Obtiene todas las asociaciones de los objetos al proyecto, menos los grupos y usarios
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetProjectAsociations(ByVal id As Long)
        Dim query As String = "zsp_200_PRJ_GPAsocID"
        Dim ds As DataSet = Server.Con.ExecuteDataset(query, id)

        If ds Is Nothing OrElse ds.Tables.Count < 1 OrElse ds.Tables(0).Rows.Count < 1 Then
            Return Nothing
        Else
            Return ds.Tables(0)
        End If
    End Function

    ''' <summary>
    ''' Obtiene el count de asociaciones con el proyecto
    ''' </summary>
    ''' <param name="projectID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetProjectAsociationsCount(ByVal projectID As Long) As Long
        Dim query As String = "zsp_100_PRJ_GPAC"
        Return Server.Con.ExecuteScalar(query, New Object() {projectID})
    End Function


    ''' <summary>
    ''' Obtiene los proyectos asociados a un grupo
    ''' </summary>
    ''' <param name="groupID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetProjectGroupAsociations(ByVal groupID As Long) As DataTable
        Dim ds As DataSet

        If Server.isOracle Then
            Dim strbuilder As New StringBuilder()
            strbuilder.Append("SELECT Prj_ID,Name,Description ")
            strbuilder.Append("FROM PRJ_TBL ")
            strbuilder.Append("WHERE Prj_ID in (SELECT PRJID ")
            strbuilder.Append("FROM PRJ_R_O ")
            strbuilder.Append("WHERE OBJID = ")
            strbuilder.Append(groupID)
            strbuilder.Append(" and OBJTYP = 103 GROUP BY PRJID)")
        Else
            'En caso que la base sea de sql2000
            Try
                ds = Server.Con.ExecuteDataset("zsp_200_PRJ_GPGAs", New Object() {groupID})
            Catch
                ds = Server.Con.ExecuteDataset("zsp_100_PRJ_GPGAs", New Object() {groupID})
            End Try
        End If

        If ds Is Nothing OrElse ds.Tables.Count < 1 OrElse ds.Tables(0).Rows.Count < 1 Then
            Return Nothing
        Else
            Return ds.Tables(0)
        End If
    End Function

    ''' <summary>
    ''' Obtiene los proyectos que no se encuentran asociados a un grupo.
    ''' </summary>
    ''' <param name="groupID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetProjectGroupAvailable(ByVal groupID As Long) As DataTable
        Dim ds As DataSet

        If Server.isOracle Then
            Dim strbuilder As New StringBuilder()
            strbuilder.Append("SELECT Prj_ID,Name,Description FROM PRJ_TBL WHERE Prj_ID not in (SELECT PRJID ")
            strbuilder.Append("FROM PRJ_R_O WHERE (OBJID = ")
            strbuilder.Append(groupID)
            strbuilder.Append(" or OBJID in (SELECT inheritedUsergroup as OBJID FROM zfu_100_getinheritedgroups(")
            strbuilder.Append(groupID)
            strbuilder.Append("))) and OBJTYP = 103 GROUP BY PRJID)")
        Else
            'En caso que la base sea de sql2000
            Try
                ds = Server.Con.ExecuteDataset("zsp_200_PRJ_GPGAv", New Object() {groupID})
            Catch
                ds = Server.Con.ExecuteDataset("zsp_100_PRJ_GPGAv", New Object() {groupID})
            End Try
        End If

        If ds Is Nothing OrElse ds.Tables.Count < 1 OrElse ds.Tables(0).Rows.Count < 1 Then
            Return Nothing
        Else
            Return ds.Tables(0)
        End If
    End Function

    ''' <summary>
    ''' Obtiene todos los poyectos de una coleccion de grupos
    ''' </summary>
    ''' <param name="groupIds">Un string separado por '|' con los group ids(el string tiene que terminar con '|').</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function GetProjectsByUser(ByVal groupIds As String) As DataTable
        Dim ds As DataSet

        If Server.isOracle Then
            Dim mander As Char = "|"

            'No se arma la consulta entera, ya que no se va a utilizar para Oracle
            For Each groupid As String In groupIds.Split(mander)
                Dim strbuilder As New StringBuilder()
                strbuilder.Append("SELECT Prj_ID,Name,Description FROM PRJ_TBL WHERE Prj_ID in (SELECT PRJID FROM PRJ_R_O WHERE (OBJID = ")
                strbuilder.Append(groupid)
                strbuilder.Append(") and OBJTYP = 103 GROUP BY PRJID")

                Dim dsAux As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strbuilder.ToString())

                ds.Tables(0).Merge(dsAux.Tables(0))

                dsAux.Dispose()
            Next
        Else
            'En caso que la base sea de sql2000
            Try
                ds = Server.Con.ExecuteDataset("zsp_200_PRJ_GPByID", New Object() {groupIds})
            Catch
                ds = Server.Con.ExecuteDataset("zsp_100_PRJ_GPByID", New Object() {groupIds})
            End Try
        End If

        If ds Is Nothing OrElse ds.Tables.Count < 1 OrElse ds.Tables(0).Rows.Count < 1 Then
            Return Nothing
        Else
            Return ds.Tables(0)
        End If
    End Function

#End Region

#Region "CUD"

    ''' <summary>
    ''' Realiza el insert a PRJ_R_O 
    ''' </summary>
    ''' <param name="projectID"></param>
    ''' <param name="objType"></param>
    ''' <param name="objID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateProjectAsociation(ByVal projectID As Long, ByVal objType As Long, ByVal objID As Long) As Integer
        If Server.isOracle Then
            Dim strBuilder As New StringBuilder()
            strBuilder.Append("INSERT INTO PRJ_R_O(PRJID,OBJTYP,OBJID) VALUES (")
            strBuilder.Append(projectID)
            strBuilder.Append(",")
            strBuilder.Append(objType)
            strBuilder.Append(",")
            strBuilder.Append(objID)
            strBuilder.Append(")")

            Return Server.Con.ExecuteNonQuery(CommandType.Text, strBuilder.ToString())
        Else
            Dim query As String = "zsp_100_PRJ_CPA"

            Return Server.Con.ExecuteNonQuery(query, New Object() {projectID, objType, objID})
        End If
    End Function

    ''' <summary>
    ''' Realiza el delete a PRJ_R_O 
    ''' </summary>
    ''' <param name="projectID"></param>
    ''' <param name="objType"></param>
    ''' <param name="objID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DeleteProjectAsociation(ByVal projectID As Long, ByVal objType As Long, ByVal objID As Long) As Integer
        If Server.isOracle Then
            Dim strBuilder As New StringBuilder()
            strBuilder.Append("DELETE FROM PRJ_R_O WHERE PRJID = ")
            strBuilder.Append(projectID)
            strBuilder.Append(" and OBJTYP =")
            strBuilder.Append(objType)
            strBuilder.Append(" and OBJID = ")
            strBuilder.Append(objID)

            Return Server.Con.ExecuteNonQuery(CommandType.Text, strBuilder.ToString())
        Else
            Dim query As String = "zsp_100_PRJ_DPA"
            Return Server.Con.ExecuteNonQuery(query, New Object() {projectID, objType, objID})
        End If
    End Function

    ''' <summary>
    ''' Reasigna todos los objetos asignados a un pryecto al proyecto por defecto 0.
    ''' </summary>
    ''' <param name="projectID"></param>
    ''' <remarks></remarks>
    Shared Sub ReasignProjectAsociations(ByVal projectID As Long)
        If Server.isOracle Then
            Dim strBuilder As New StringBuilder()
            strBuilder.Append("UPDATE PRJ_R_O SET PRJID = 0 WHERE PRJID = ")
            strBuilder.Append(projectID)
            strBuilder.Append(" AND OBJID not in (select objid from PRJ_R_O where PRJID = 0)")

            Server.Con.ExecuteNonQuery(CommandType.Text, strBuilder.ToString())
            Server.Con.ExecuteNonQuery(CommandType.Text, "DELETE PRJ_R_O WHERE PRJID = " & projectID)
        Else
            Dim query As String = "zsp_200_PRJ_RPA"
            Server.Con.ExecuteNonQuery(query, New Object() {projectID})
        End If
    End Sub
#End Region

#End Region

End Class
