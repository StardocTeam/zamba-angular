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
    ''' 
    Private myRule As IDOADDTOWF
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Try


           

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
            WF = WFBusiness.GetWFbyId(myrule.WorkId)

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

            Trace.WriteLineIf(ZTrace.IsInfo, "Agregando los documentos al Workflow...")
            WFTaskBusiness.AddResultsToWorkFLowSinceRuleADDTOWF(results, myrule.WorkId, initialStep, WFStepStatesBusiness.GetInitialStateId(initialStep.ID), myrule.ID)
            Trace.WriteLineIf(ZTrace.IsInfo, "Cantidad de documentos agregados: " & results.Count)
            For Each r As Result In results
                UserBusiness.Rights.SaveAction(r.ID, Zamba.Core.ObjectTypes.WFTask, Zamba.Core.RightsType.ExecuteRule, myrule.Name)
            Next
        Finally

        End Try

        Return results

    End Function
    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDOADDTOWF)
        Me.myRule = rule
    End Sub
End Class