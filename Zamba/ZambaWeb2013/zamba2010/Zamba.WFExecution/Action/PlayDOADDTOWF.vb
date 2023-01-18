Imports Zamba.Core.WF.WF

Public Class PlayDOADDTOWF

    ''' <summary>
    ''' Play de la regla ADDTOWF (Agregar a Workflow)
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="myrule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	05/08/2008	Modified
    ''' </history>
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IDOADDTOWF) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFB As New WFBusiness
        Try


            For Each doc As Result In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & doc.Name)
            Next

            Dim initialStep As New WFStep
            '[Sebastian 04-06-2009] se corrigio el mail funcionamiento de add to wf, cuando obtenia el wf lo
            'hacia de forma incompleta por eso lanzaba exception.
            'se utilizo un metodo que devuelve el wf como array, pero por o visto solo devuelve uno porque
            'obtiene por id de wf. De todas formas cualquier error evaluar si o puede llegar a ser esto
            'que lo provoca.

            ' Se recuperan ciertos atributos de la etapa inicial del workflow en donde se va a insertar la tarea
            ' (ya que después se utilizan para guardar un historial de los documentos que se insertan en el workflow)
            'Dim wf As IWorkFlow

            Dim WF As IWorkFlow
            WF = WFB.GetWFbyId(myrule.WorkId)

            'wf = WFBusiness.GetFullUserWF(myrule.WorkId)

            ' Se obtiene el id de la etapa inicial
            initialStep.ID = WF.InitialStep.ID
            'initialStep.ID = WFBusiness.GetInitialStepOfWF(myrule.WorkId)

            ' Se obtiene el nombre de la etapa inicial
            initialStep.Name = WF.InitialStep.Name
            'initialStep.Name = WFStepBusiness.GetStepNameById(initialStep.ID)

            ' Se coloca en la etapa inicial el id de workflow al que pertenece
            initialStep.WorkId = WF.ID
            'initialStep.WorkFlow.ID = myrule.WorkId

            ' Se obtiene el nombre del workflow
            'initialStep.WorkFlow.Name = WFBusiness.GetWorkflowNameByWFId(myrule.WorkId)

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Agregando los documentos al Workflow...")

            Dim WFTB As New WFTaskBusiness
            WFTB.AddResultsToWorkFLowSinceRuleADDTOWF(results, myrule.WorkId, initialStep, WFStepStatesBusiness.getInitialState(initialStep.ID), myrule.ID)
            WFTB = Nothing
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de documentos agregados: " & results.Count)
            Dim UB As New UserBusiness
            For Each r As Result In results
                UB.SaveAction(r.ID, Zamba.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, myrule.Name)
            Next
            UB = Nothing
        Finally
            WFB = Nothing
        End Try

        Return results

    End Function

End Class
