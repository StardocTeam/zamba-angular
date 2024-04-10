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

    Public Shared Function AddState(ByVal NewState As WFStepState, ByRef wfstep As WFStep) As String
        Return (WFStepStatesFactory.AddState(NewState, wfstep))
    End Function

    Public Shared Function AddState(ByVal Doc_State_id As Int32, ByVal Description As String, ByVal Name As String, ByVal Initial As Int32, ByVal StepId As Int32) As String
        Return (WFStepStatesFactory.AddState(Doc_State_id, Description, Name, Initial, StepId))
    End Function

    Public Shared Sub UpdateState(ByVal columnName As String, ByVal value As String, ByVal id As Integer)
        WFStepStatesFactory.UpdateState(columnName, value, id)
    End Sub

    Public Shared Sub RemoveState(ByVal State As WFStepState)
        WFStepStatesFactory.RemoveState(State)
    End Sub

    Public Shared Sub SetInitialState(ByVal State As WFStepState, ByRef wfstep As WFStep)
        WFStepStatesFactory.SetInitialState(State, wfstep)
    End Sub

    Public Shared Function getInitialState(ByVal stepId As Int64) As Integer
        If Cache.Workflows.hsStepsinitialState.ContainsKey(stepId) = False Then
            Dim initialState As Integer = WFStepStatesFactory.getInitialState(stepId)
            If Cache.Workflows.hsStepsinitialState.ContainsKey(stepId) = False Then
                Cache.Workflows.hsStepsinitialState.Add(stepId, initialState)
            End If
            Return initialState
        End If
        Return Cache.Workflows.hsStepsinitialState(stepId)
    End Function


End Class