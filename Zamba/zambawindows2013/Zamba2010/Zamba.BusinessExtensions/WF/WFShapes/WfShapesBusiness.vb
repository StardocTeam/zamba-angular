Imports Zamba.Data
'Imports Zamba.Icons
'imports Zamba.WFBusiness

Public Class WfShapesBusiness

    ''' <summary>
    ''' Trae los parametros de la regla
    ''' </summary>
    ''' <param name="p_iRuleID">Id de la regla</param>
    ''' <returns></returns>
    ''' <remarks></remarks>



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
            Zamba.Core.ZClass.raiseerror(ex)
            Return ""
        End Try
    End Function
    ''' <summary>
    ''' Actualiza la posicion del step
    ''' </summary>
    ''' <param name="WFStep">Step cuya posicion hay que actualizar</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateStepPosition(ByVal wfstep As IWFStep)
        wfstep.LastModified = DateTime.Now

        WFStepBusiness.UpdateStepPosition(wfstep)
    End Sub

    Public Shared Sub UpdateShapePosition(ByVal ZambaObject As IZambaCore)
        ZambaObject.LastModified = DateTime.Now
        WFStepBusiness.UpdateShapePosition(ZambaObject)
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
    Public Shared Sub DeleteRuleByID(ByVal id As Int64)
        Try
            WFRulesBusiness.DeleteRuleByID(id)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
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
            wfstep.LastModified = DateTime.Now

            WFStepBusiness.UpdateStepSize(wfstep)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Shared Sub UpdateShapeSize(ByVal ZambaObject As IZambaCore)
        Try
            ZambaObject.LastModified = DateTime.Now

            WFStepBusiness.UpdateShapeSize(ZambaObject)
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
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Shared Sub SetIconspath(ByVal Type As String, ByVal Path As String)
        Try
            IconsBusiness.SetIconspath(Type, Path)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    Public Shared Sub UpdateIconspath(ByVal Type As String, ByVal Path As String)
        Try
            IconsBusiness.UpdateIconspath(Type, Path)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub
    ''' <summary>
    ''' Actualiza el nombre de la regla
    ''' </summary>
    ''' <param name="Id">Id de la regla</param>
    ''' <param name="Name">Nuevo nombre</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateRuleNameByID(ByVal Id As Int64, ByVal Name As String)
        Try
            WFRulesBusiness.UpdateRuleNameByID(Id, Name)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
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
            Zamba.Core.ZClass.raiseerror(ex)
            Return 0
        End Try
    End Function

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
