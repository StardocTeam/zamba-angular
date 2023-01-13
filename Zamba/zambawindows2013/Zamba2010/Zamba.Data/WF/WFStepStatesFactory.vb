Imports Zamba.Core
Imports System.Text

Public Class WFStepStatesFactory
    Inherits ZClass

    Public Overrides Sub Dispose()

    End Sub
#Region "Get"
    'Public Shared Function IsStepStateMemberOfStep(ByVal StepId As Int64, ByVal StepStateId As Int64) As Boolean
    '    If Server.Con.ExecuteScalar("Select count(Step_id) from wfviewdocstepsstates where step_id = " & StepId & " and doc_state_id = " & StepStateId) > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    Public Shared Function GetAllStates(ByVal DsSteps As DsSteps) As DsStepState
        If Not DsSteps.WFSteps.Rows.Count < 1 Then

            Dim strSelect As String = "select * from WFStepStates where"
            For Each r As DataRow In DsSteps.WFSteps.Rows
                strSelect += " step_id=" & r.Item(0).ToString & " or"
            Next
            strSelect = strSelect.Substring(0, strSelect.Length - 2)
            Dim DsStepState As New DsStepState
            Dim dataTemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
            dataTemp.Tables(0).TableName = DsStepState.WFStepStates.TableName
            DsStepState.Merge(dataTemp)
            Return DsStepState
        End If

        Return Nothing
    End Function


    ''' <summary>
    ''' Obtiene los estados de una etapa
    ''' </summary>
    ''' <param name="stepId">ID de la etapa</param>
    ''' <remarks></remarks>
    ''' <history> 
    '''     [Tomas]     19/06/09    Modified    Se modifica el método para trabajar con stored procedures
    ''' </history>

    Public Shared Function GetStepsStatesbyStepList(ByVal StepIdList As String) As DataSet
        Dim Ds As DataSet = Nothing
        Ds = Server.Con.ExecuteDataset(CommandType.Text, String.Format("SELECT * FROM wfstepstates WHERE step_id in ({0})", StepIdList))
        Return Ds
    End Function

    ''' <summary>
    ''' Obtiene los estados de la etapa especificada.
    ''' </summary>
    ''' <param name="WfStepId"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas]     19/06/2009    Modified    Se adapta el método para Oracle
    ''' </history>
    Public Shared Function GetStepStatesByStepId(ByVal WfStepId As Int32) As Generic.List(Of IWFStepState)
        Dim Ds As DataSet = GetStepsStatesbyStepList(WfStepId)
        Dim States As New Generic.List(Of IWFStepState)

        If Not Ds Is Nothing AndAlso Ds.Tables.Count > 0 Then
            'Si la base es Oracle, la columna 'Initial' se llama 'C_Initial', por eso se le modifica el nombre.
            If Server.isOracle Then
                Ds.Tables(0).Columns("C_Initial").ColumnName = "Initial"
            End If

            For Each R As DataRow In Ds.Tables(0).Rows
                'Se hace esta validación para solucionar el problema de los estados de las etapas en oracle
                If TypeOf (R("Description")) Is DBNull Then
                    R("Description") = String.Empty
                End If
                Dim State As New WFStepState(R("Doc_State_ID"), R("Name"), R("Description"), R("Initial"))
                States.Add(State)
            Next
        End If
        Return States
    End Function

    Public Shared Sub UpdateStapeStatesCounts()
        Dim query As String = String.Empty

        If Server.isOracle Then
            query = "UPDATE zobjcount SET zobjcount.UPDATEDATE = SYSDATE, zobjcount.count = " &
                    "NVL((SELECT TaskCount from (SELECT do_state_id as StateId ,nvl(count(1),0) AS TaskCount " &
                    "FROM wfstepstates s INNER JOIN WFDOCUMENT D ON s.doc_state_id = d.do_state_id " &
                    "WHERE do_state_id IN (SELECT objectId from zobjcount where objecttypeid = 104) AND Do_State_ID is not null " &
                    "GROUP BY do_state_id) q " &
                    "WHERE zobjcount.objectid = q.StateId),0) " &
                    "where zobjcount.OBJECTTYPEID = 104"
        Else
            query = "UPDATE zobjcount SET zobjcount.UpdateDate = GETDATE(), zobjcount.count = isnull(a.TaskCount,0) " &
                    "FROM (SELECT do_state_id as StateId, isnull(count(1),0) as TaskCount " &
                    "FROM wfstepstates s INNER join WFDOCUMENT D on s.doc_state_id = d.do_state_id " &
                    "WHERE do_state_id IN (SELECT objectId from zobjcount where objecttypeid = 104) " &
                    "AND Do_State_ID is not null " &
                    "GROUP BY do_state_id) a " &
                    "WHERE zobjcount.objectid = a.StateId AND zobjcount.ObjectTypeId = 104"
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Sub InsertStapeStatesCounts()
        Dim query As String = String.Empty

        If Server.isOracle Then
            query = "INSERT INTO zobjcount " &
                    "SELECT 104, do_state_id as StateId, nvl(count(1),0) as TaskCount, 0, SYSDATE " &
                    "FROM wfstepstates s left join WFDOCUMENT D on s.doc_state_id = d.do_state_id " &
                    "WHERE do_state_id NOT IN (SELECT objectId from zobjcount where objecttypeid = 104) " &
                    "AND Do_State_ID is not null GROUP BY do_state_id"
        Else
            query = "INSERT INTO zobjcount " &
                    "SELECT 104, do_state_id as StateId, isnull(count(1),0) as TaskCount, 0, GETDATE() " &
                    "FROM wfstepstates s left join WFDOCUMENT D on s.doc_state_id = d.do_state_id " &
                    "WHERE do_state_id NOT IN (SELECT objectId from zobjcount where objecttypeid = 104) " &
                    "AND Do_State_ID is not null GROUP BY do_state_id"
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    ''' <summary>
    ''' Método que trae los estados de una etapa
    ''' </summary>
    ''' <param name="wfstep"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/06/2008	Modified    Se modifico el método de tal forma que acepte Oracle y SQL  
    '''     [Tomas]     02/03/09    Modified    Se modificó el método ya que se generaba 2 veces la etiqueta [Ninguno]
    ''' </history>
    Public Shared Sub FillState(ByRef wfstep As WFStep, ByVal ds As DsStepState)

        Try

            Dim rt() As DataRow = ds.Tables(0).Select("Step_Id=" & wfstep.ID)
            'Tomas: Estas 2 lineas fueron comentadas ya que mostraban 2 veces la etiqueta [Ninguno]
            'Dim se As New WFStep.State(0, "[Ninguno]", "[Ninguno]", True)
            'wfstep.States.Add(se.Id, se)
            Dim columnName As String

            If Server.isOracle Then
                columnName = "C_Initial"
            Else
                columnName = "Initial"
            End If

            For Each r As DataRow In rt
                Dim st As WFStepState
                If Not IsDBNull(r("Description")) Then
                    st = New WFStepState(r("Doc_State_ID"), r("Name"), r("Description"), r(columnName))
                Else
                    st = New WFStepState(r("Doc_State_ID"), r("Name"), String.Empty, r(columnName))
                End If

                If Not wfstep.States.Contains(st) Then
                    wfstep.States.Add(st)
                    If st.Initial = True Then
                        wfstep.InitialState = st
                    End If
                End If
            Next
            'If wfstep.InitialState Is Nothing Then
            '    wfstep.InitialState = se
            'End If

        Catch ex As Exception
            raiseerror(ex)
        End Try

    End Sub


    'Public Shared Sub FillState(ByRef wfstep As WFStep, ByVal ds As DsStepState)
    '    Try
    '        Dim rt() As DsStepState.WFStepStatesRow = ds.WFStepStates.Select("Step_Id=" & wfstep.ID)
    '        Dim se As New WFStep.State(0, "[Ninguno]", "[Ninguno]", True)
    '        wfstep.States.Add(se.Id, se)
    '        For Each r As DsStepState.WFStepStatesRow In rt
    '            Dim st As New WFStep.State(r.Doc_State_ID, r.Name, r.Description, r.Initial)
    '            If Not wfstep.States.ContainsKey(st.Id) Then
    '                wfstep.States.Add(st.Id, st)
    '                If st.Initial = 1 Then
    '                    wfstep.InitialState = st
    '                End If
    '            End If
    '        Next
    '        If wfstep.InitialState Is Nothing Then
    '            wfstep.InitialState = se
    '        End If
    '    Catch ex As Exception
    '        Zamba.Core.ZClass.raiseerror(ex)
    '    End Try
    'End Sub

    'Public Shared Sub FillState(ByRef wfstep As WFStep, ByVal ds As DataSet)
    '    Try
    '        Dim rt() As DataRow = ds.Tables(0).Select("Step_Id=" & wfstep.ID)
    '        Dim se As New WFStep.State(0, "[Ninguno]", "[Ninguno]", True)
    '        wfstep.States.Add(se.Id, se)
    '        For Each r As DataRow In rt
    '            Dim st As New WFStep.State(r("Doc_State_Id"), r("Name"), r("Description"), r("C_Initial"))
    '            If Not wfstep.States.ContainsKey(st.Id) Then
    '                wfstep.States.Add(st.Id, st)
    '                If st.Initial = 1 Then
    '                    wfstep.InitialState = st
    '                End If
    '            End If
    '        Next
    '        If wfstep.InitialState Is Nothing Then
    '            wfstep.InitialState = se
    '        End If
    '    Catch ex As Exception
    '        Zamba.Core.ZClass.raiseerror(ex)
    '    End Try
    'End Sub

#End Region

#Region "ABM"

    ''' <summary>
    ''' Método que sirve para agregar un estado de una etapa a la base de datos
    ''' </summary>
    ''' <param name="NewState"></param>
    ''' <param name="wfstep"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/06/2008	Modified    Se agrego un insert especifico para Oracle y otro especifico para SQL (varía sólo una columna)
    '''                                         En Oracle se coloco C_Initial y no Initial porque parar Oracle Initial es una palabra clave
    ''' </history>
    Public Shared Function AddState(ByVal NewState As WFStepState, ByRef wfstep As WFStep) As String

        Dim strInsert As New StringBuilder

        If Server.isOracle Then
            strInsert.Append("INSERT INTO WFSTEPSTATES (Doc_State_id,Description,Step_Id,Name,C_Initial) ")
        Else
            strInsert.Append("INSERT INTO WFSTEPSTATES (Doc_State_id,Description,Step_Id,Name,Initial) ")
        End If

        strInsert.Append("VALUES (" & NewState.ID & ",'" & NewState.Description & "'," & wfstep.ID & ",'" & NewState.Name & "',0)")
        Server.Con.ExecuteNonQuery(CommandType.Text, strInsert.ToString())

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

        Return Nothing

    End Function

    ''' <summary>
    ''' Método que sirve para agregar un estado de una etapa a la base de datos
    ''' </summary>
    ''' <param name="Doc_State_id"></param>
    ''' <param name="Description"></param>
    ''' <param name="Name"></param>
    ''' <param name="Initial"></param>
    ''' <param name="StepId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/06/2008	Modified    Se agrego un insert especifico para Oracle y otro especifico para SQL (varía sólo una columna)
    '''                                         En Oracle se coloco C_Initial y no Initial porque parar Oracle Initial es una palabra clave
    ''' </history>
    Public Shared Function AddState(ByVal Doc_State_id As Int32, ByVal Description As String, ByVal Name As String, ByVal Initial As Int32, ByVal StepId As Int32) As String

        Dim strInsert As New StringBuilder

        If Server.isOracle Then
            strInsert.Append("INSERT INTO WFSTEPSTATES (Doc_State_id,Description,Name,C_Initial,Step_Id) ")
        Else
            strInsert.Append("INSERT INTO WFSTEPSTATES (Doc_State_id,Description,Name,Initial,Step_Id) ")
        End If

        strInsert.Append("VALUES (" & Doc_State_id & ",'" & Description & "','" & Name & "'," & Initial & "," & StepId & ")")
        Server.Con.ExecuteNonQuery(CommandType.Text, strInsert.ToString())

        Return Nothing

    End Function

    ''' <summary>
    ''' Método que actualiza el nombre o descripción del estado
    ''' </summary>
    ''' <param name="columnName"></param>
    ''' <param name="value"></param>
    ''' <param name="id"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	10/06/2008	Modified    
    ''' </history>
    Public Shared Sub UpdateState(ByVal columnName As String, ByVal value As String, ByVal id As Integer)

        Dim sql As String = "Update WFStepStates Set " & columnName & " = '" & value & "' where doc_state_id=" & id
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
    ''' Método que sirve para actualizar el initial de un estado 
    ''' </summary>
    ''' <param name="State"></param>
    ''' <param name="wfstep"></param>
    ''' <remarks></remarks>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	10/06/2008	Modified    Se agrego un insert especifico para Oracle y otro especifico para SQL (varía sólo una columna)
    '''                                         En Oracle se coloco C_Initial y no Initial porque parar Oracle Initial es una palabra clave
    ''' </history>
    Public Shared Sub SetInitialState(ByVal State As WFStepState, ByRef wfstep As WFStep)

        Dim sql As String

        If Server.isOracle Then

            sql = "UPDATE WFStepStates SET C_Initial = 0 where step_id=" & wfstep.ID
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            sql = "UPDATE WFStepStates SET C_Initial = 1 where doc_state_id=" & State.ID
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)

        Else

            sql = "UPDATE WFStepStates SET Initial = 0 where step_id=" & wfstep.ID
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            sql = "UPDATE WFStepStates SET Initial = 1 where doc_state_id=" & State.ID
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)

        End If

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

    ''' <summary>
    ''' Método que sirve para obtener el estado inicial de una determinada etapa
    ''' </summary>
    ''' <param name="stepId">Id de una etapa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	19/02/2009	Created    
    ''' </history>
    Public Shared Function GetInitialStateId(ByVal stepId As Int32) As Int32

        Dim query As String

        If (Server.isSQLServer) Then
            query = "SELECT Doc_State_ID FROM WFStepStates Where Step_Id = " & stepId.ToString() & " AND Initial = 1"
        ElseIf (Server.isOracle) Then
            query = "SELECT Doc_State_ID FROM WFStepStates Where Step_Id = " & stepId.ToString() & " AND C_Initial = 1"
        End If

        Return Server.Con.ExecuteScalar(CommandType.Text, query)

    End Function
    ''' <summary>
    ''' Método que sirve para determinar la etapa inicial de una tarea
    ''' </summary>
    ''' <param name="stepId">Id de una etapa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Pablo]	05/10/2010	Created    
    ''' </history>
    Public Shared Function GetInitialStateExistance(ByVal stepId As Int32) As Integer
        Dim query As String

        If (Server.isSQLServer) Then
            query = "SELECT initial FROM WFStepStates Where Step_Id = " & stepId.ToString() & " AND Initial = 1"
        ElseIf (Server.isOracle) Then
            query = "SELECT c_initial FROM WFStepStates Where Step_Id = " & stepId.ToString() & " AND C_Initial = 1"
        End If

        Return Server.Con.ExecuteScalar(CommandType.Text, query)
    End Function

#End Region

    Public Shared Function GetStateNameByStateId(ByVal StateId As Int32) As String
        Try
            Dim strSelect As String = "select Name from WFStepStates where Doc_State_ID =" & StateId
            Dim Name As String = Server.Con.ExecuteScalar(CommandType.Text, strSelect)
            Return Name
        Catch ex As Exception
            raiseerror(ex)
            Return "NINGUNO"
        End Try
    End Function

End Class