Imports Zamba.Membership

Namespace Cache
    Public Class Workflows
        'Guarda los parametros de las reglas en un DS
        Public Shared hsRuleParamsDS As New SynchronizedHashtable

        'Guarda las etapas por wf en un DS
        Public Shared hsWFStepsDS As New SynchronizedHashtable
        'Guarda los estados x etapas por wf en un DS
        Public Shared hsWFStepStatesDS As New SynchronizedHashtable

        'Guarda los estados de cada etapa
        Public Shared hsStepsStates As New SynchronizedHashtable
        Public Shared hsStepsinitialState As New SynchronizedHashtable
        'Guarda las opciones de las reglas (Zruleopt)
        'Public Shared hsRulesOpt As New Hashtable

        'Guarda los nombres y ids de las etapas por workflow
        Public Shared hsSteps As New SynchronizedHashtable()

        'Guarda los estados independientemente de la etapa
        Public Shared hsStates As New SynchronizedHashtable

        'Guarda los colores de las etapas
        Public Shared hsStepsColors As New SynchronizedHashtable()
        'Guarda los Workflows
        Public Shared hsWorkflow As New SynchronizedHashtable()
        Public Shared hsStepsNames As New SynchronizedHashtable

        'Guarda el id de etapa (GetWFStepIdbyRuleID)
        Public Shared hsWFStepId As New SynchronizedHashtable()
        Public Shared hsDocTypesAsWFbyStepId As New SynchronizedHashtable()
        Public Shared hsWFAndStepIdsAndNamesAndTaskCount As New SynchronizedHashtable()


        Friend Shared Sub RemoveCurrentInstance()
            hsRuleParamsDS.Clear()
            hsWFStepsDS.Clear()
            hsWFStepStatesDS.Clear()
            hsStepsStates.Clear()
            hsSteps.Clear()
            hsStates.Clear()
            hsStepsColors.Clear()
            hsWorkflow.Clear()
            hsWFStepId.Clear()
            hsDocTypesAsWFbyStepId.Clear()
            hsStepsinitialState.Clear()
            hsStepsNames.Clear()
            hsWFAndStepIdsAndNamesAndTaskCount.Clear()
        End Sub


    End Class
End Namespace