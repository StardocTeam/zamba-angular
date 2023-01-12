Imports Zamba.Data

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : WFStepStatesBusiness
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase que interactua con la clase WFStepStatesFactory para el manejo de 
''' estados de una etapa, ya sea para agregar un estado, obtener el estado inicial
''' de una etapa, etc ...
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Gaston]	19/02/2009	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class WFStepStatesBusiness

    Public Shared Function AddState(ByVal NewState As WFStepState, ByRef wfstep As IWFStep) As String
        Dim strToReturn As String = (WFStepStatesFactory.AddState(NewState, wfstep))

        'Guardo log de la insersion
        Dim userId As Integer = Membership.MembershipHelper.CurrentUser.ID
        UserBusiness.Rights.SaveAction(NewState.ID, ObjectTypes.WFStates, RightsType.Create, "Se generó el estado '" & NewState.Name & "', para la etapa '" & wfstep.Name & "'")

        Return strToReturn
    End Function

    Public Shared Function AddState(ByVal Doc_State_id As Int32, ByVal Description As String, ByVal Name As String, ByVal Initial As Int32, ByVal WFStep As IWFStep) As String
        Dim strToReturn As String = (WFStepStatesFactory.AddState(Doc_State_id, Description, Name, Initial, WFStep.ID))

        'Guardo log de la insersion
        Dim userId As Integer = Membership.MembershipHelper.CurrentUser.ID
        UserBusiness.Rights.SaveAction(Doc_State_id, ObjectTypes.WFStates, RightsType.Create, "Se generó el estado '" & Name & "', para la etapa '" & WFStep.Name & "'")

        Return strToReturn
    End Function

    Public Shared Sub UpdateState(ByVal columnName As String, ByVal value As String, ByVal id As Integer, ByVal StateName As String)
        WFStepStatesFactory.UpdateState(columnName, value, id)

        'Guardo log de la insersion
        Dim userId As Integer = Membership.MembershipHelper.CurrentUser.ID
        UserBusiness.Rights.SaveAction(id, ObjectTypes.WFStates, RightsType.Edit, "Se editó el valor de '" & columnName & "' a '" & value & "' para el estado " & StateName)
    End Sub

    Public Shared Sub RemoveState(ByVal State As WFStepState)
        WFStepStatesFactory.RemoveState(State)

        'Guardo log de baja
        Dim userId As Integer = Membership.MembershipHelper.CurrentUser.ID
        UserBusiness.Rights.SaveAction(State.ID, ObjectTypes.WFStates, RightsType.Delete, "Se ha eliminado el estado '" & State.Name & "'")
    End Sub

    Public Shared Sub SetInitialState(ByVal State As WFStepState, ByRef wfstep As WFStep)
        WFStepStatesFactory.SetInitialState(State, wfstep)
    End Sub

    Public Shared Function GetInitialState(ByVal stepId As Int64) As IWFStepState
        Dim wfSteps As Generic.List(Of IWFStepState) = WFStepStatesComponent.GetStepStatesByStepId(stepId, False)

        For i As Int32 = 0 To wfSteps.Count - 1
            If wfSteps(i).Initial Then
                Return wfSteps(i)
            End If
        Next

        wfSteps = Nothing
        Return Nothing
    End Function

    'Public Shared Function GetInitialState(ByVal stepId As Int64, ByVal t As Transaction) As IWFStepState
    '    Dim wfSteps As Generic.List(Of IWFStepState) = WFStepStatesComponent.GetStepStatesByStepId(stepId, False, t)

    '    For i As Int32 = 0 To wfSteps.Count - 1
    '        If wfSteps(i).Initial Then
    '            Return wfSteps(i)
    '        End If
    '    Next

    '    wfSteps = Nothing
    '    Return Nothing
    'End Function

    Public Shared Function GetInitialStateId(ByVal stepId As Int32) As Integer
        Return (WFStepStatesFactory.GetInitialStateId(stepId))
    End Function
    Public Shared Function GetInitialStateExistance(ByVal stepId As Int32) As Integer
        Return (WFStepStatesFactory.GetInitialStateExistance(stepId))
    End Function


    Public Shared Function GetStepStateById(ByVal StateId As Int32) As IWFStepState
        Dim st As IWFStepState

        SyncLock (Cache.Workflows.hsStepsStates)

            If Cache.Workflows.hsStepsStates.Contains(StateId) = False Then
                st = WFStepStatesComponent.GetStepStateById(StateId)
                If Cache.Workflows.hsStepsStates.Contains(StateId) = False Then Cache.Workflows.hsStepsStates.Add(StateId, st)
            End If

        End SyncLock
        Return st
    End Function

    Public Shared Sub InsertStapeStatesCounts()
        Try
            WFStepStatesFactory.InsertStapeStatesCounts()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Public Shared Sub UpdateStapeStatesCounts()
        Try
            WFStepStatesFactory.UpdateStapeStatesCounts()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub
End Class