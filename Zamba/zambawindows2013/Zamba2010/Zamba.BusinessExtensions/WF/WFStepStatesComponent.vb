Imports Zamba.servers
Imports Zamba.Data

Public Class WFStepStatesComponent
    Inherits ZClass

#Region "Get"
    Public Shared Function GetAllStates(ByVal DsSteps As DsSteps) As DsStepState
        Try
            If Not DsSteps.WFSteps.Rows.Count < 1 Then

                Dim strSelect As String = "select * from WFStepStates where"
                For Each r As DataRow In DsSteps.WFSteps.Rows
                    strSelect += " step_id=" & r.Item(0) & " or"
                Next
                strSelect = strSelect.Substring(0, strSelect.Length - 2)
                Dim DsStepState As New DsStepState
                Dim dataTemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
                dataTemp.Tables(0).TableName = DsStepState.WFStepStates.TableName
                DsStepState.Merge(dataTemp)
                Return DsStepState
            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try
        Return Nothing
    End Function
    Public Shared Function GetStateName(ByVal StateId As Int32, ByVal DsStepsStates As DsStepsTask) As String
        Try
            Return DsStepsStates.WFViewDocStepsStates.FindByDoc_State_ID(StateId).Name()
        Catch ex As Exception
            raiseerror(ex)
            Return String.Empty
        End Try
    End Function

    Public Shared Function GetStepsStates(Optional ByVal StepId As Int32 = 0) As DsStepsTask
        Dim strselect As String
        'La vista wfviewdocstepsstates no funciona
        If StepId = 0 Then
            strselect = "Select * from WFStepStates"
        Else
            strselect = "Select * from WFStepStates where step_id = " & StepId
        End If
        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Dim dsstepstask As DsStepsTask = New DsStepsTask
        Dstemp.Tables(0).TableName = dsstepstask.WFViewDocStepsStates.TableName
        dsstepstask.Merge(Dstemp)
        Return dsstepstask
    End Function
    Public Shared Function GetStepStateById(ByVal StateId As Int32) As IWFStepState
        Dim strselect As String
        strselect = "Select * from WFStepStates where doc_state_id = " & StateId

        Dim Dstemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Dim dsstepstask As DsStepsTask = New DsStepsTask
        Dstemp.Tables(0).TableName = dsstepstask.WFViewDocStepsStates.TableName
        dsstepstask.Merge(Dstemp)

        Dim st As WFStepState
        If dsstepstask.WFViewDocStepsStates.Rows.Count > 0 Then
            st = New WFStepState(dsstepstask.WFViewDocStepsStates(0).Doc_State_ID, dsstepstask.WFViewDocStepsStates(0).Name, dsstepstask.WFViewDocStepsStates(0).Description, dsstepstask.WFViewDocStepsStates(0).Initial)
        Else
            st = New WFStepState(0, "Ninguno", "Estado por defecto", False)
        End If
        Dstemp.Dispose()
        dsstepstask.Dispose()
        Dstemp = Nothing
        dsstepstask = Nothing

        Return st
    End Function




    ''' <summary>
    ''' Devuelve los estados de la etapa a partir del id de la etapa
    ''' </summary>
    ''' <param name="WfStepId">Id de la etapa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetStepStatesByStepId(ByVal WfStepId As Int32) As Generic.List(Of IWFStepState)
        Return GetStepStatesByStepId(WfStepId, False)
    End Function

    ''' <summary>
    ''' Devuelve los estados de la etapa a partir del id de la etapa. Permite forzar la recarga
    ''' del estado.
    ''' </summary>
    ''' <param name="WfStepId">Id de la etapa</param>
    ''' <param name="isReload">True para volver a cargar el estado de la base de datos</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetStepStatesByStepId(ByVal WfStepId As Int32, ByVal isReload As Boolean) As Generic.List(Of IWFStepState)
        SyncLock (Cache.Workflows.hsStepsStates)
            If isReload Then
                If Cache.Workflows.hsStepsStates.ContainsKey(WfStepId) Then
                    Cache.Workflows.hsStepsStates.Remove(WfStepId)
                End If
            End If
            If Cache.Workflows.hsStepsStates.ContainsKey(WfStepId) = False Then
                Cache.Workflows.hsStepsStates.Add(WfStepId, WFStepStatesFactory.GetStepStatesByStepId(WfStepId))
            End If
            Return Cache.Workflows.hsStepsStates(WfStepId)
        End SyncLock
    End Function

    ''' <summary>
    ''' Carga en cache todos los estados del listado de etapas.
    ''' </summary>
    ''' <param name="WfStepId">Id de la etapa</param>
    ''' <param name="isReload">True para volver a cargar el estado de la base de datos</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Sub LoadStepStatesByStepIdList(ByVal WfStepIdList As List(Of Int64))
        SyncLock (Cache.Workflows.hsStepsStates)
            Dim StrIDList As String

            For Each stepid As Int64 In WfStepIdList
                If Cache.Workflows.hsStepsStates.ContainsKey(stepid) = False Then
                    StrIDList = StrIDList & "," & stepid
                End If
            Next
            Dim ds As DataSet = WFStepStatesFactory.GetStepsStatesbyStepList(StrIDList.Substring(1))

            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                'Si la base es Oracle, la columna 'Initial' se llama 'C_Initial', por eso se le modifica el nombre.
                If Server.IsOracle Then
                    ds.Tables(0).Columns("C_Initial").ColumnName = "Initial"
                End If

                For Each R As DataRow In ds.Tables(0).Rows
                    'Se hace esta validación para solucionar el problema de los estados de las etapas en oracle
                    If TypeOf (R("Description")) Is DBNull Then
                        R("Description") = String.Empty
                    End If
                    Dim State As New WFStepState(R("Doc_State_ID"), R("Name"), R("Description"), R("Initial"))

                    If Cache.Workflows.hsStepsStates.ContainsKey(CInt(R("Step_Id"))) = False Then
                        Dim statelist As New List(Of IWFStepState)
                        statelist.Add(State)
                        Cache.Workflows.hsStepsStates.Add(CInt(R("Step_Id")), statelist)
                    Else
                        DirectCast(Cache.Workflows.hsStepsStates.Item(CInt(R("Step_Id"))), List(Of IWFStepState)).Add(State)
                    End If
                Next

            End If
        End SyncLock
    End Sub

    'Martin: la etapa deberia tener sus estados o de ultima se debe generar un hash para los estados por etapa
    'Public Shared Function IsStepStateMemberOfStep(ByVal StepId As Int64, ByVal StepStateId As Int64) As Boolean
    '    Dim StepStates As Generic.List(Of IWFStepState) = Cache.Workflows.hsWFStepStates(StepId)

    '    If Server.Con.ExecuteScalar(CommandType.Text, "SELECT COUNT(Step_Id) FROM WFStepStates where step_id = " & StepId & " and doc_state_id = " & StepStateId) > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    Public Shared Sub FillState(ByRef wfstep As WFStep, ByVal ds As DsStepState)
        Try
            Dim rt() As DsStepState.WFStepStatesRow = ds.WFStepStates.Select("step_id=" & wfstep.ID)
            Dim se As New WFStepState(0, "[Ninguno]", "[Ninguno]", 0)
            wfstep.States.Add(se)
            For Each r As DsStepState.WFStepStatesRow In rt
                Dim st As New WFStepState(r.Doc_State_ID, r.Name, r.Description, r.Initial)
                wfstep.States.Add(st)
                If st.Initial = 1 Then
                    wfstep.InitialState = st
                End If
            Next
            If wfstep.InitialState Is Nothing Then
                wfstep.InitialState = se
            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub
#End Region

#Region "ABM"
    Public Shared Function AddState(ByVal NewState As WFStepState, ByRef wfstep As WFStep) As String

        Dim strInsert As String = "INSERT INTO WFSTEPSTATES (Doc_State_id,Description,Step_Id,Name,Initial) VALUES (" & NewState.Id & ",'" & NewState.Description & "'," & wfstep.ID & ",'" & NewState.Name & "',0)"
        Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)

        'SP 5/1/2006
        'Try
        '    if Server.IsOracle then
        '        Dim ParValues() As Object = {row.Doc_State_ID, row.Description, row.Step_Id, row.Name, row.Initial}
        '        ''Dim parNames() As Object = {"pStateID", "pDesc", "pStepId", "pName", "pInitial"}
        '        'Dim parTypes() As Object = {13, 7, 13, 7, 13}
        '        Server.Con.ExecuteNonQuery("ZWFInsStepsFactory_pkg.ZInsWfStepStates",  ParValues)
        '    Else
        '        Dim ParValues() As Object = {row.Doc_State_ID, row.Description, row.Step_Id, row.Name, row.Initial}
        '        Server.Con.ExecuteNonQuery("ZInsWfStepStates", ParValues)
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try

        Return String.Empty
    End Function
    Public Shared Sub UpdateState(ByVal State As WFStepState)

        Dim sql As String = "Update WFStepStates Set name='" & State.Name & "' where doc_state_id=" & State.ID
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)

        'SP 5/1/2006
        'Try
        '    if Server.IsOracle then
        '        Dim ParValues() As Object = {Name, Description, StateId}
        '        ''Dim parNames() As Object = {"pName", "pDesc", "pStateId"}
        '        'Dim parTypes() As Object = {7, 7, 13}
        '        Server.Con.ExecuteNonQuery("ZWFUpdStepsFactory_pkg.ZUpdWFStepStatesByStateId",  ParValues)
        '    Else
        '        Dim ParValues() As Object = {Name, Description, StateId}
        '        Server.Con.ExecuteNonQuery("ZUpdWFStepStatesByStateId", ParValues)
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try

    End Sub
    Public Shared Sub RemoveState(ByVal State As WFStepState)

        Dim sql As String = "Delete from WFStepStates where doc_state_id=" & State.ID
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)

        ''SP 5/1/2006
        'Try
        '    if Server.IsOracle then
        '        Dim ParValues() As Object = {StateId}
        '        ''Dim parNames() As Object = {"pStateId"}
        '        'Dim parTypes() As Object = {13}
        '        Server.Con.ExecuteNonQuery("ZWFDelStepsFactory_pkg.ZDelWFStepStatesByStateId",  ParValues)
        '    Else
        '        Dim ParValues() As Object = {StateId}
        '        Server.Con.ExecuteNonQuery("ZDelWFStepStatesByStateId", ParValues)
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try

    End Sub
    ''' <summary>
    ''' Devuelve el estado de la lista
    ''' </summary>
    ''' <param name="stateID">Id a buscar en la lista</param>
    ''' <param name="states">Listado de estados</param>
    ''' <history>Marcelo created 18/06/2009</history>
    ''' <remarks></remarks>
    Public Shared Function getInitialStateFromList(ByVal states As List(Of IWFStepState)) As IWFStepState
        For Each state As IWFStepState In states
            If state.Initial Then
                Return state
            End If
        Next
        Return New WFStepState(0, "[Ninguno]", "[Ninguno]", 0)
    End Function


    ''' <summary>
    ''' Devuelve el estado de la lista
    ''' </summary>
    ''' <param name="stateID">Id a buscar en la lista</param>
    ''' <param name="states">Listado de estados</param>
    ''' <history>Marcelo created 18/06/2009</history>
    ''' <remarks></remarks>
    Public Shared Function getStateFromList(ByVal stateID As Int64, ByVal states As List(Of IWFStepState)) As IWFStepState
        SyncLock (Cache.Workflows.hsStepsStates)
            If Cache.Workflows.hsStepsStates.Contains(stateID) = False Then
                For Each state As IWFStepState In states
                    If state.ID = stateID Then
                        Cache.Workflows.hsStepsStates.Add(state.ID, state)
                        Return Cache.Workflows.hsStepsStates(state.ID)
                    End If
                Next
                Return New WFStepState(0, "[Ninguno]", "[Ninguno]", 0)
            End If
            Return Cache.Workflows.hsStepsStates(stateID)
        End SyncLock
    End Function
    ''' <summary>
    ''' Devuelve el nombre del estado de la lista
    ''' </summary>
    ''' <param name="stateID">Id a buscar en la lista</param>
    ''' <param name="states">Listado de estados</param>
    ''' <history>Marcelo created 19/06/2009</history>
    ''' <remarks></remarks>
    Public Shared Function getStateNameFromList(ByVal stateID As Int64, ByVal states As List(Of IWFStepState)) As String
        For Each state As IWFStepState In states
            If state.ID = stateID Then
                Return state.Name
            End If
        Next
        Return "[Ninguno]"
    End Function

    Public Shared Sub SetStateInitial(ByVal State As WFStepState, ByRef wfstep As WFStep)
        Dim sql As String

        sql = "UPDATE WFStepStates SET Initial = 0 where step_id=" & wfstep.ID
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        sql = "UPDATE WFStepStates SET Initial = 1 where doc_state_id=" & State.ID
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)

        'SP 5/1/2006
        'Try
        '    if Server.IsOracle then
        '        Dim ParValues() As Object = {0, StepId}
        '        ''Dim parNames() As Object = {"pInitial", "pStepId"}
        '        'Dim parTypes() As Object = {13, 13}
        '        Server.Con.ExecuteNonQuery("ZWFUpdStepsFactory_pkg.ZUpdWFSSInitialByStepId",  ParValues)
        '    Else
        '        Dim ParValues() As Object = {0, StepId}
        '        Server.Con.ExecuteNonQuery("ZUpdWFSSInitialByStepId", ParValues)
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try


        'SP 5/1/2006
        'Try
        '    if Server.IsOracle then
        '        Dim ParValues() As Object = {1, StateId}
        '        ''Dim parNames() As Object = {"pInitial", "pStateId"}
        '        'Dim parTypes() As Object = {13, 13}
        '        Server.Con.ExecuteNonQuery("ZWFUpdStepsFactory_pkg.ZUpdWFSSInitialByStateId",  ParValues)
        '    Else
        '        Dim ParValues() As Object = {1, StateId}
        '        Server.Con.ExecuteNonQuery("ZUpdWFSSInitialByStateId", ParValues)
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub
#End Region
    Public Overrides Sub Dispose()

    End Sub
End Class

