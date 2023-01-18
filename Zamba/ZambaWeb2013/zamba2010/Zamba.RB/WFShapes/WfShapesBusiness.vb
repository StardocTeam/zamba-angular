Imports Zamba.Data
Imports zamba.Core
'Imports Zamba.Icons
'imports Zamba.WFBusiness

Public Class WfShapesBusiness

    '''
    ''''Factory of WfStep
    Public Shared Function GetNewWFSTep(ByVal workflow As IWorkFlow, ByVal id As Int32, ByVal name As String, ByVal description As String, ByVal location As Point, ByVal refreshrate As Int32, ByVal maxDocs As Int32, ByVal maxHours As Int32, ByVal startAtOpenDoc As Boolean, ByVal color As String, ByVal width As Int32, ByVal height As Int32) As IWFStep
        Return New WFStep(workflow.ID, id, name, "", description, location, 0, maxDocs, maxHours, startAtOpenDoc, color, width, height, 0, 0)
    End Function

    ''' <summary>
    ''' Trae los parametros de la regla
    ''' </summary>
    ''' <param name="p_iRuleID">Id de la regla</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
  
    ''' Se comenta este metodo para evaluar su falta de uso, 
    '''llama a otro metodo addrules, que tiene hardcoded la regla dodistribuir.
    ''' Si alguien ve que esto provoca un errror, verlo con Martin.
    '''Se utiliza para agregar una regla dodistribuir entre 2 etapas de wfShapes, si alguien tiene algun problema, verlo con Marcelo
    ''' <summary>
    ''' Agrega una regla dositribuir entre WFStep1 y WFStep 2
    ''' </summary>
    ''' <param name="intTarea"></param>
    ''' <param name="strName">Nombre de la regla</param>
    ''' <param name="WFStep1">Step de inicio</param>
    ''' <param name="WFStep2">Step de destino</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function addRules(ByVal intTarea As Int32, ByVal strName As String, ByVal WFStep1 As IWFStep, ByVal WFStep2 As IWFStep) As Int32
        Try
            'Dim oWFRulesBusiness As New WFRulesBusiness()
            Return WFRulesBusiness.addRules(intTarea, strName, WFStep1, WFStep2)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return 0
    End Function
    ''' <summary>
    ''' Trae un lista con las reglas de un workflow
    ''' </summary>
    ''' <param name="wf">Workflow cuyas reglas hay que buscar</param>
    ''' <returns>lista con las reglas</returns>
    ''' <remarks></remarks>
    Public Shared Function FillTransitions(ByVal wf As IWorkFlow) As ArrayList
        Try
            Dim oWFRulesBusiness As WFRulesBusiness
            Dim listCon As ArrayList

            oWFRulesBusiness = New WFRulesBusiness()
            listCon = oWFRulesBusiness.FillTransitions(wf)
            Return listCon

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return Nothing
    End Function
    Public Shared Function GetIconsPathString(ByVal iconKey As String) As String
        Dim path As String
        Try
            '    path = IconsFactory.GetIconsPathString(iconKey)
            path = IconsBusiness.GetIconsPathString(iconKey)
            Return path
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
            Return ""
        End Try
    End Function
    ''' <summary>
    ''' Actualiza la posicion del step
    ''' </summary>
    ''' <param name="WFStep">Step cuya posicion hay que actualizar</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateStepPosition(ByVal wfstep As IWFStep)
        WFStepBusiness.UpdateStepPosition(wfstep)
    End Sub
    ''' <summary>
    ''' Se le pasa el id de la regla y devuelve el nombre
    ''' </summary>
    ''' <param name="p_iRuleId">Id de la regla</param>
    ''' <returns>Nombre de la regla</returns>
    ''' <remarks></remarks>
    Public Shared Function GetRuleNameById(ByVal p_iRuleId As Int64) As String
        Try
            Dim name As String
            name = WFRulesBusiness.GetRuleName(p_iRuleId)
            Return name
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return ""
        End Try
    End Function

    ''' <summary>
    '''  Borra un step.
    ''' </summary>
    ''' <param name="WFStep">Step a borrar</param>
    ''' <remarks></remarks>
    Public Shared Sub DelStep(ByVal wfstep As IWFStep)
        Try
            WFStepBusiness.DelStep(wfstep)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    '''  Borra una regla.
    ''' </summary>
    ''' <param name="id">Regla a borrar</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteRuleByID(ByVal id As Int32)
        Try
            WFRulesBusiness.DeleteRuleByID(id)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    '''  Actualiza un Step.
    ''' </summary>
    ''' <param name="WFStep">Step a actualizar</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateStep(ByVal wfstep As IWFStep)
        Try
            WFStepBusiness.UpdateStep(wfstep)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Actualiza el tamaño de un step
    ''' </summary>
    ''' <param name="WFStep">Step a actualizar</param>
    Public Shared Sub UpdateStepSize(ByVal wfstep As IWFStep)
        Try
            WFStepBusiness.UpdateStepSize(wfstep)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Actualiza el color de un step
    ''' </summary>
    ''' <param name="color">color que va a tener el Step</param>
    ''' <param name="ID">ID del Step a actualizar</param>
    Public Shared Sub UpdateStepColor(ByVal color As String, ByVal ID As Long)
        Try
            WFStepBusiness.UpdateStepColor(color, ID)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Public Shared Sub SetIconspath(ByVal Type As String, ByVal Path As String)
        Try
            IconsBusiness.SetIconspath(Type, Path)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    Public Shared Sub UpdateIconspath(ByVal Type As String, ByVal Path As String)
        Try
            IconsBusiness.UpdateIconspath(Type, Path)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Actualiza el nombre de la regla
    ''' </summary>
    ''' <param name="Id">Id de la regla</param>
    ''' <param name="Name">Nuevo nombre</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateRuleNameByID(ByVal Id As Int32, ByVal Name As String)
        Try
            WFRulesBusiness.UpdateRuleNameByID(Id, Name)
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Devuelve un nuevo ID
    ''' </summary>
    ''' <param name="IdType">Tipo de objeto para el cual se requiere un nuevo ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetNewID(ByVal idtype As IdTypes) As Integer
        Try
            Dim Id As Int32
            Id = ToolsBusiness.GetNewID(idtype)
            Return Id
        Catch ex As Exception
            zamba.core.zclass.raiseerror(ex)
            Return 0
        End Try
    End Function
    ''' <summary>
    ''' Agrega un step en la base
    ''' </summary>
    ''' <param name="WfStep">Step a agregar</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     dalbarellos 13.04.2009 se cambio la llamada por una que funciona correctamente, si bien es de data, se esta utilizando de esa forma
    ''' </history>
    Public Shared Sub InsertStep(ByVal wfstep As IWFStep, ByVal WFName As String)
        Try
            'WFStepBusiness.InsertStep(wfstep)
            wfstep.ID = WFStepsFactory.InsertStep(wfstep.WorkId, wfstep.Name, wfstep.Help, wfstep.Description, wfstep.Location, 0, 30, 48, wfstep.InitialState.Initial)

            'Guardo log de la insersion
            Dim userId As Integer = Membership.MembershipHelper.CurrentUser.ID
            UserBusiness.Rights.SaveAction(userId, ObjectTypes.WFSteps, RightsType.Create, "Se generó la etapa '" & wfstep.Name & "', con ID=" & wfstep.ID & " para el WorkFlow: " & WFName)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
End Class
