Imports Zamba.Servers

Public Class WFStepsFactoryExt
    ''' <summary>
    ''' Obtiene los ids de los grupos que tienen permisos sobre una etapa
    ''' </summary>
    ''' <param name="wfStepId">Id de la etapa</param>
    ''' <returns>DataTable con los ids de los grupos de la etapa</returns>
    ''' <remarks></remarks>
    Public Function GetStepGroupIds(ByVal wfStepId As Int64) As DataTable
        Dim query As String = "Select distinct(G.id) from USRGROUP G inner join usr_Rights R on R.groupid=G.id where R.ADITIONAL=" & _
            wfStepId.ToString & " and R.objid=" & ObjectTypes.WFSteps

        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    Public Function GetAllWStepFActors() As DataTable
        Dim ds As DataSet = Server.Con.ExecuteDataset("zsp_workflow_100_GetWFStepActors", Nothing)
        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene las etapas que estas asociadas al proyecto
    ''' </summary>
    ''' <param name="projectId">Id del Proyecto</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStepsByProject(ByVal projectID As Int64) As DataTable
        Return Server.Con.ExecuteDataset(CommandType.Text, String.Format(" SELECT {0} as 'ID Projecto',42 as 'Tipo de objeto',step_ID as 'ID de Etapa',Name       FROM wfstep       WHERE step_ID IN       (SELECT OBJID       FROM PRJ_R_O       WHERE PrjID = {0} and OBJTyp =  42)",projectID)).Tables(0)
    End Function


    ''' <summary>
    ''' Obtiene el número de reglas que se encuentren en la sección Actualización de una etapa.
    ''' </summary>
    ''' <param name="stepId">Is de la etapa a verificar</param>
    ''' <returns>Número de reglas que se encuentren en la sección Actualización de una etapa</returns>
    ''' <remarks></remarks>
    Public Function GetStepServiceRulesCount(ByVal stepId As Int64) As Int64
        If Server.isOracle Then
            Return Server.Con.ExecuteScalar(CommandType.Text, "SELECT COUNT(1) FROM WFRules WHERE step_Id=" & stepId & " AND ParentType=8")
        Else
            Return Server.Con.ExecuteScalar("ZSP_WORKFLOW_100_GetStepServiceRulesCount", New Object() {stepId})
        End If
    End Function
End Class
