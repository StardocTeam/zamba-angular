Imports Zamba.Data

Public Class WFStepBusinessExt
    ''' <summary>
    ''' Obtiene los ids de los grupos que tienen permisos sobre una etapa
    ''' </summary>
    ''' <param name="wfStepId">Id de la etapa</param>
    ''' <returns>DataTable con los ids de los grupos de la etapa</returns>
    ''' <remarks></remarks>
    Public Function GetStepGroupIds(ByVal wfStepId As Int64) As DataTable
        Dim wfStepsFactoryExt As New WFStepsFactoryExt
        Return wfStepsFactoryExt.GetStepGroupIds(wfStepId)
    End Function

    Public Function GetAllWFStepActors() As DataTable
        Return New WFStepsFactoryExt().GetAllWStepFActors()
    End Function

    ''' <summary>
    ''' Obtiene las etapas que estas asociadas al proyecto
    ''' </summary>
    ''' <param name="projectId">Id del Proyecto</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStepsByProject(ByVal projectId As Int64) As DataTable
        Dim wfStepsFactoryExt As New WFStepsFactoryExt
        Return wfStepsFactoryExt.GetStepsByProject(projectId)
    End Function
End Class
