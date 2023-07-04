Imports Zamba.Core
Imports Zamba.Servers
Imports System.Text
Imports System.Collections.Generic
Imports System.Data.SqlClient

Public Class WFTasksFactory
    Inherits ZClass

    Const ORACLE_PAGINATE_FORMAT As String = "(rn <= {1} and rn >= {0})"

    Private UcmFactory As New UcmFactory
#Region "Get"

    <Obsolete("Metodo discontinuado", False)>
    Public Function GetUserTasks(ByVal WFs() As WorkFlow, ByVal VerAsignadosAOtros As Boolean, ByVal VerAsignadosANadie As Boolean) As DsDocuments
        'Diego, parece que se hicieron cambios al metodo y no se probaron, estaba funcionando mal asi que volvi
        ' a la forma original el metodo, Este probado y funcionando
        Dim DsDoc As New DsDocuments
        'Dim DsDoc As New DataSet
        Try
            Dim strBuilder As New StringBuilder
            strBuilder.Append("select * from wfdocument where")

            For Each wf As WorkFlow In WFs
                For Each s As WFStep In wf.Steps.Values

                    strBuilder.Append(" (step_id = ")
                    strBuilder.Append(s.ID.ToString())

                    ' ------------------------------------------------------------------------------------------
                    'Dim EjecutarTareasDeOtrosUsuarios as boolean = 
                    ' ------------------------------------------------------------------------------------------


                    If Not VerAsignadosAOtros AndAlso VerAsignadosANadie Then
                        strBuilder.Append(" and (user_asigned = " & RightFactory.CurrentUser.ID & " or user_asigned = 0)")
                    ElseIf VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
                        strBuilder.Append(" and user_asigned <> 0")
                    ElseIf Not VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
                        strBuilder.Append(" and user_asigned = " & RightFactory.CurrentUser.ID)
                    End If

                    strBuilder.Append(") or")
                Next
            Next


            Dim str As String = strBuilder.ToString()
            'borro el ultimo 'or' de mas
            str = str.Substring(0, str.Length - 2)

            'DsDoc = Server.Con.ExecuteDataset(CommandType.Text, str)
            Dim DsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, str)
            DsTemp.Tables(0).TableName = DsDoc.Documents.TableName
            DsDoc.Merge(DsTemp)

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
        Return DsDoc
    End Function

    <Obsolete("Metodo discontinuado", False)>
    Public Function GetTasks(ByVal WF As WorkFlow) As DataSet
        'Dim DsDoc As New DsDocuments
        Dim DsTemp As DataSet

        Dim StrSelect As String = "select * from wfdocument where"
        For Each s As WFStep In WF.Steps.Values
            StrSelect += " step_id = " & s.ID
            StrSelect += " or"
        Next
        'borro el ultimo 'or' de mas
        StrSelect = StrSelect.Substring(0, StrSelect.Length - 2)
        DsTemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        'DsTemp.Tables(0).TableName = DsDoc.Documents.TableName
        'DsDoc.Merge(DsTemp)
        Return DsTemp
    End Function

    Public Function GetDocId(ByVal taskId As Int64) As Int64

        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("select doc_id from wfdocument where task_id = ")
        QueryBuilder.Append(taskId.ToString())

        Dim Value As Object = Server.Con.ExecuteScalar(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing

        If IsNothing(Value) Then
            Return -1
        Else
            Return Convert.ToInt64(Value)
        End If


    End Function
    Public Function GetStepIdDocTypeIdByTaskId(ByVal taskId As Int64) As DataSet

        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("select doc_id, step_id, doc_type_id, work_id from wfdocument where task_id = ")
        QueryBuilder.Append(taskId.ToString())

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing

        Return ds


    End Function




    Public Function GetDocTypeId(ByVal taskId As Int64) As Int64

        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("select doc_type_id from wfdocument where task_id = ")
        QueryBuilder.Append(taskId.ToString())

        Dim Value As Object = Server.Con.ExecuteScalar(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing

        If IsNothing(Value) Then
            Return -1
        Else
            Return Convert.ToInt64(Value)
        End If


    End Function

    Public Function GetResultExtraData(ByVal taskId As Int64) As DataTable

        Dim query As String = "select doc_id,doc_type_id,name from wfdocument where task_id = " & taskId.ToString()
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)

    End Function

    Public Function GetTasksByWfId(ByVal wfId As Int64) As DataSet
        Dim strBuilder As New System.Text.StringBuilder()

        strBuilder.Append("SELECT Doc_ID, DOC_TYPE_ID, step_Id, Do_State_ID, Name, IconId, CheckIn, ")
        strBuilder.Append("User_Asigned, Exclusive, ExpireDate, User_Asigned_By, Date_Asigned_By, Task_ID, Task_State_ID, ")
        strBuilder.Append("Remark, Tag, work_id ")
        strBuilder.Append("FROM WFDocument where step_id in (SELECT step_id FROM wfstep WHERE work_id = ")
        strBuilder.Append(wfId.ToString())
        strBuilder.Append(")")
        'EN ORACLE SE RENOMBRO LA COLUMNA EXCLUSIVE A C_EXCLUSIVE
        Dim query As String = strBuilder.ToString
        If Server.isOracle Then query = query.Replace("Exclusive,", "C_Exclusive,")

        Return Server.Con.ExecuteDataset(CommandType.Text, query)
    End Function

    Public Function GetTasksByWfIdAndStepId(ByVal wfId As Int64, ByVal stepId As Int64) As DataSet

        Dim StrBuilder As New StringBuilder()
        StrBuilder.Append("SELECT Doc_ID, DOC_TYPE_ID,  step_Id, Do_State_ID, Name, IconId, ")
        StrBuilder.Append("CheckIn, User_Asigned, Exclusive, ExpireDate, User_Asigned_By, Date_Asigned_By, ")
        StrBuilder.Append("Task_ID, Task_State_ID, Remark, Tag, work_id from wfdocument ")
        StrBuilder.Append("where step_id = ")
        StrBuilder.Append(stepId.ToString())
        StrBuilder.Append(" AND work_id = ")
        StrBuilder.Append(wfId.ToString())

        'EN ORACLE SE RENOMBRO LA COLUMNA EXCLUSIVE A C_EXCLUSIVE
        Dim query As String = StrBuilder.ToString
        If Server.isOracle Then query = query.Replace("Exclusive,", "C_Exclusive,")
        Return Server.Con.ExecuteDataset(CommandType.Text, query)
    End Function

    Public Function GetTaskByTaskId(ByVal taskId As Int64) As DataSet
        Dim DsTemp As New DataSet
        Try
            Dim StrSelect As String = "select * from wfdocument where Task_ID = " & taskId
            DsTemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return DsTemp
    End Function
    ''' <summary>
    ''' </summary>
    ''' <param name="taskId"></param>
    ''' <history>(pablo) Created</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTaskByDocId(ByVal DocId As Int64) As DataSet
        Dim DsTemp As New DataSet
        Try
            Dim StrSelect As String = "select * from wfdocument where Doc_ID = " & DocId
            DsTemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return DsTemp
    End Function

    Public Function GetTasksNamesByTaskIds(ByVal taskIds As List(Of Int64)) As DataTable
        Dim DsTemp As New DataSet
        Dim QueryBuilder As New StringBuilder

        For Each CurrentId As Int64 In taskIds
            QueryBuilder.Append("Task_id = ")
            QueryBuilder.Append(CurrentId.ToString)
            QueryBuilder.Append(" OR ")
        Next
        QueryBuilder.Remove(QueryBuilder.Length - 4, 4)

        Dim StrSelect As String = "select name from wfdocument " & QueryBuilder.ToString
        DsTemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Return DsTemp.Tables(0)
    End Function
    ''' <summary>
    ''' Devuelve el id del estado de la tarea
    ''' </summary>
    ''' <param name="taskId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTaskStateByTaskId(ByVal taskId As Int64) As Int64
        Dim StrSelect As String = "select do_state_id from wfdocument where Task_ID = " & taskId
        Return Server.Con.ExecuteScalar(CommandType.Text, StrSelect)
    End Function

    ''' <summary>
    ''' Get TaskIds of the Document
    ''' </summary>
    ''' <param name="docId">Document ID</param>
    ''' <returns></returns>
    ''' <history>Marcelo Modified 30/11/09</history>
    ''' <remarks></remarks>

    ''' <summary>
    ''' Get TaskIds of the Document
    ''' </summary>
    ''' <param name="docId">Document ID</param>
    ''' <returns></returns>
    ''' <history>Marcelo Modified 30/11/09</history>
    ''' <remarks></remarks>
    Public Function GetTaskIdsStepsIdsDocTypesIdsByDocId(ByVal docId As Int64) As DataSet
        Dim QueryBuilder As New StringBuilder
        Try
            QueryBuilder.Append("select task_id,step_id,doc_type_id from wfdocument where doc_id = ")
            QueryBuilder.Append(docId.ToString())

            Return Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
        Finally
            QueryBuilder.Remove(0, QueryBuilder.Length)
            QueryBuilder = Nothing
        End Try
    End Function
    Public Function GetTaskIdByDocIdAndStepId(ByVal docId As Int64, ByVal stepId As Int64) As DataSet

        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("select task_id, step_id , doc_id, work_id from wfdocument where doc_id = ")
        QueryBuilder.Append(docId.ToString())
        QueryBuilder.Append(" and ")
        QueryBuilder.Append("step_id = ")
        QueryBuilder.Append(stepId.ToString())


        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing

        Return ds



    End Function

    Public Function GetTaskIdByDocIdAndWorkId(ByVal docId As Int64, ByVal workId As Int64) As Int64

        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("select task_id from wfdocument where doc_id = ")
        QueryBuilder.Append(docId.ToString())
        QueryBuilder.Append(" and ")
        QueryBuilder.Append("work_id = ")
        QueryBuilder.Append(workId.ToString())


        Dim Value As Object = Server.Con.ExecuteScalar(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing

        If IsNothing(Value) Then
            Return -1
        Else
            Return Convert.ToInt64(Value)
        End If



    End Function




    Public Function GetTasksByTask(ByVal result As ITaskResult) As DataSet

        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("select * from wfdocument where doc_id = ")
        QueryBuilder.Append(result.ID.ToString())
        QueryBuilder.Append(" and DOC_TYPE_ID = ")
        QueryBuilder.Append(result.DocTypeId.ToString())
        QueryBuilder.Append(" and Task_ID <> ")
        QueryBuilder.Append(result.TaskId.ToString())

        Dim Value As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder = Nothing

        Return Value
    End Function


    Public Function GetTasks(ByVal taskIds As List(Of Int64)) As DataSet
        If (IsNothing(taskIds) OrElse taskIds.Count = 0) Then
            Return Nothing
        Else
            Dim QueryBuilder As New StringBuilder()
            QueryBuilder.Append("SELECT Doc_ID, DOC_TYPE_ID,  step_Id, Do_State_ID, ")
            QueryBuilder.Append("Name, IconId, CheckIn, User_Asigned, Exclusive, ExpireDate, ")
            QueryBuilder.Append("User_Asigned_By, Date_Asigned_By, Task_ID, Task_State_ID, Remark, ")
            QueryBuilder.Append("Tag, work_id FROM WFDocument WHERE ")

            For Each CurrentId As Int32 In taskIds
                QueryBuilder.Append("Task_id = ")
                QueryBuilder.Append(CurrentId.ToString)
                QueryBuilder.Append(" OR ")
            Next

            QueryBuilder.Remove(QueryBuilder.Length - 4, 4)
            'EN ORACLE SE RENOMBRO LA COLUMNA EXCLUSIVE A C_EXCLUSIVE
            Dim query As String = QueryBuilder.ToString
            If Server.isOracle Then query = query.Replace("Exclusive,", "C_Exclusive,")

            Return Server.Con.ExecuteDataset(CommandType.Text, query)
        End If


    End Function

    Public Function GetTasks(ByVal stepId As Int64) As DataTable
        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("SELECT Doc_ID, DOC_TYPE_ID,")
        QueryBuilder.Append("wfdocument.Name,IconId, CheckIn,User_Asigned,")
        QueryBuilder.Append("Exclusive,ExpireDate, User_Asigned_By,")
        QueryBuilder.Append("Date_Asigned_By,Task_ID, Task_State_ID,")
        QueryBuilder.Append("Remark,Tag,work_id ,")
        QueryBuilder.Append("wfstepstates.Step_Id, wfstepstates.Description, ")
        QueryBuilder.Append("wfstepstates.Name, wfstepstates.Initial")
        QueryBuilder.Append("from wfdocument Inner Join wfstepstates ")
        QueryBuilder.Append("ON wfdocument.do_state_id = wfstepstates.doc_state_id ")
        QueryBuilder.Append("WHERE wfstepstates.step_id = ")
        QueryBuilder.Append(stepId.ToString())

        'EN ORACLE SE RENOMBRO LA COLUMNA EXCLUSIVE A C_EXCLUSIVE
        Dim query As String = QueryBuilder.ToString
        If Server.isOracle Then query = query.Replace("Exclusive,", "C_Exclusive,")

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query)
        If ds.Tables.Count = 1 Then
            Return ds.Tables(0)
        End If

        Return Nothing
    End Function







    Public Function GetTasksByStep(ByVal stepId As Int64) As DataTable

        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("SELECT * from wfdocument Inner Join wfstepstates ")
        QueryBuilder.Append("ON wfdocument.do_state_id =wfstepstates.doc_state_id ")
        QueryBuilder.Append("WHERE wfstepstates.step_id = ")
        QueryBuilder.Append(stepId.ToString())

        'EN ORACLE SE RENOMBRO LA COLUMNA EXCLUSIVE A C_EXCLUSIVE
        Dim query As String = QueryBuilder.ToString
        If Server.isOracle Then query = query.Replace("Exclusive,", "C_Exclusive,")

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query)

        If ds.Tables.Count = 1 Then
            Return ds.Tables(0)
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Devuelve las tareas del entidad y etapa que se le pasan por parametro
    ''' </summary>
    ''' <param name="stepId">Id de la etapa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''        [Ezequiel] 14/09/09  Created.
    '''        [Javier]   06/10/10  Modified.   Se agrega string de restricciones y string de declaracion de variables de fechas
    '''        [Javier]   12/10/10  Modified    Se cambia parametros a la llamada de GetCommonTaskStringBuilder
    ''' </history>
    Public Function GetTasksByStepandDocTypeId(ByVal stepId As Int64,
                                                      ByVal EntityId As Int64,
                                                      ByVal indexs As List(Of IIndex),
                                                      ByVal WithRights As Boolean,
                                                      ByVal FilterString As String,
                                                      ByVal RestrictionString As String,
                                                      ByVal dateDeclarationString As String,
                                                      ByVal LastPage As Int64,
                                                      ByVal PageSize As Int32,
                                                      ByVal CheckInColumnIsShortDate As Boolean,
                                                      ByRef totalCount As Long,
                                                      ByVal order As String,
                                                      ByVal VerAsignadosAOtros As Boolean,
                                                      ByVal VerAsignadosANadie As Boolean,
                                                      ByVal HabilitarFavoritos As Boolean,
                                                      Optional ByVal auIndex As List(Of IIndex) = Nothing,
                                                      Optional ByVal refIndexs As List(Of ReferenceIndex) = Nothing) As DataTable


        Dim strTableI As String = MakeTable(EntityId, TableType.Indexs)
        Dim strTableT As String = MakeTable(EntityId, TableType.Document)
        Dim MainJoin As String = String.Format("{0} T inner join {1} I on T.doc_id = I.doc_id", strTableT, strTableI)

        Dim strselect As StringBuilder = GetCommonTaskStringBuilder(EntityId, WithRights, indexs, auIndex, stepId, RestrictionString, dateDeclarationString, CheckInColumnIsShortDate, FilterTypes.Task, HabilitarFavoritos, refIndexs)

        If WithRights Then
            strselect = AppendWhereRights(strselect, VerAsignadosANadie, VerAsignadosAOtros)
        End If

        If Not String.IsNullOrEmpty(FilterString) AndAlso FilterString.Trim() <> String.Empty Then
            If FilterString.ToLower.Trim.StartsWith("and") = False Then
                strselect.Append(" and ")
            End If
            strselect.Append(FilterString)
        End If

        If dateDeclarationString.Length > 0 Then
            'Si existe declaracion de variables de fecha las inserta al inicio del select
            strselect.Insert(0, dateDeclarationString)
        End If
        ReplaceTableName(strTableT, strTableI, strselect)
        Return GetTaskTable(strselect, "task_id " & order, PageSize, LastPage, totalCount, False)
    End Function

    ''' <summary>
    ''' Devuelve un count de las tareas del entidad y etapa que se le pasan por parametro
    ''' </summary>
    ''' <param name="stepId">Id de la etapa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''        [Javier] 29/12/11  Created.
    ''' </history>
    Public Function GetTasksCountByStepandDocTypeId(ByVal stepId As Int64, ByVal entityid As Int64, ByVal WithRights As Boolean, ByVal FilterString As String, ByVal RestrictionString As String,
                                                                       ByVal VerAsignadosAOtros As Boolean, ByVal VerAsignadosANadie As Boolean, ByVal usrID As Long, ByVal UserGroups As ArrayList) As Long
        Dim params As New List(Of IDbDataParameter)
        Dim isOracle As Boolean = Server.isOracle
        Dim strselect As StringBuilder = GetCommonCountTaskStringBuilder(entityid, WithRights, stepId, RestrictionString)

        If isOracle Then
            params.Add(New OracleParameter("@step_id", stepId))
            params.Add(New OracleParameter("@entityid", entityid))
        Else
            params.Add(New SqlParameter("@step_id", stepId))
            params.Add(New SqlParameter("@entityid", entityid))

        End If

        Dim groups As ArrayList
        Dim userID As Long
        If usrID > 0 Then
            groups = UserGroups
            userID = usrID
        Else
            groups = RightFactory.CurrentUser.Groups
            userID = RightFactory.CurrentUser.ID
        End If

        If WithRights Then
            If Not VerAsignadosAOtros AndAlso VerAsignadosANadie Then
                strselect.Append(" and (user_asigned = @user_id or user_asigned = 0")
                For i As Int32 = 0 To groups.Count - 1
                    strselect.Append(" or user_asigned = @group_id")
                    strselect.Append(i)
                    If isOracle Then
                        params.Add(New OracleParameter("@group_id" & i, groups(i).Id))
                    Else
                        params.Add(New SqlParameter("@group_id" & i, groups(i).Id))
                    End If
                Next
                strselect.Append(")")
            ElseIf VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
                strselect.Append(" and user_asigned <> 0")
            ElseIf Not VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
                strselect.Append(" and (user_asigned = @user_id")
                For i As Int32 = 0 To groups.Count - 1
                    strselect.Append(" or user_asigned = @group_id")
                    strselect.Append(i)

                    If isOracle Then
                        params.Add(New OracleParameter("@group_id" & i, groups(i).Id))
                    Else
                        params.Add(New SqlParameter("@group_id" & i, groups(i).Id))
                    End If
                Next
                strselect.Append(")")
            End If

            If isOracle Then
                params.Add(New OracleParameter("@user_id", userID))
            Else
                params.Add(New SqlParameter("@user_id", userID))
            End If
        End If

        If Not String.IsNullOrEmpty(FilterString) AndAlso FilterString.Trim() <> String.Empty Then
            If FilterString.ToLower.Trim.StartsWith("and") = False Then
                strselect.Append(" and ")
            End If

            strselect.Append(FilterString)
        End If

        If isOracle Then
            Dim strOracle As String = strselect.ToString()
            'todo ver porque no funciona en oracle los params y quitar este foreach
            For Each param As IDbDataParameter In params
                strOracle = strOracle.Replace(param.ParameterName, param.Value)
            Next

            Return Server.Con.ExecuteScalar(CommandType.Text, strOracle)

        Else
            Return Server.Con.ExecuteScalar(CommandType.Text, strselect.ToString(), params.ToArray())
        End If
    End Function

    ''' <summary>
    ''' Devuelve las tareas del entidad y etapa que se le pasan por parametro
    ''' </summary>
    ''' <param name="stepId">Id de la etapa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''        [Ezequiel] 14/09/09 - Created.
    '''        [Javier]   12/10/10  Modified    Se agregan parámetros Restriction y DateDeclaration
    ''' </history>
    'Public  Function GetTasksByTasksIdsAndDocTypeId(ByVal tasksids As List(Of Int64), ByVal stepId As Int64, ByVal docTypeId As Int64, ByVal indexs As List(Of IIndex), ByVal WithGridRights As Boolean, ByVal FilterString As String, ByVal PageSize As Int32, ByVal LastPage As Int64, ByVal RestrictionString As String, ByVal DateDeclarationString As String, Optional ByVal auIndex As List(Of IIndex) = Nothing) As DataTable


    '    Dim whereids As StringBuilder
    '    Dim firsttask As Boolean = True

    '    Dim strselect As StringBuilder = GetCommonTaskStringBuilder(docTypeId, WithGridRights, indexs, auIndex, stepId, RestrictionString, DateDeclarationString, False)

    '    strselect.Append(" and ")
    '    strselect.Append(FilterString)


    '    For Each taskid As Int64 In tasksids
    '        If firsttask Then
    '            whereids.Append(" and (task_id = ")
    '            whereids.Append(taskid.ToString)
    '            firsttask = False
    '        Else
    '            whereids.Append(" or task_id = ")
    '            whereids.Append(taskid.ToString)
    '        End If
    '    Next

    '    whereids.Append(")")
    '    strselect.Append(whereids.ToString())

    '    Return GetTaskTable(strselect, "task_id asc", PageSize, LastPage, False)

    'End Function

    ''' <summary>
    ''' Devuelve las tareas del entidad y etapa que se le pasan por parametro
    ''' </summary>
    ''' <param name="stepId">Id de la etapa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''        [Ezequiel] 14/09/09  Created.
    '''        [Javier]   12/10/10  Modified    Se agregan parámetros Restriction y DateDeclaration
    ''' </history>
    Public Function GetTaskByTaskIdAndDocTypeId(ByVal taskid As Int64, ByVal stepId As Int64, ByVal docTypeId As Int64, ByVal indexs As List(Of IIndex), ByVal WithGridRights As Boolean, ByVal FilterString As String, ByVal RestrictionString As String, ByVal DateDeclarationString As String, FilterType As SearchType, ByVal HabilitarFavortios As Boolean, Optional ByVal auIndex As List(Of IIndex) = Nothing, Optional ByVal refIndexs As List(Of ReferenceIndex) = Nothing) As DataTable

        '[Ezequiel] Obtengo el nombre de la vista del entidad

        Dim strselect As StringBuilder = GetCommonTaskStringBuilder(docTypeId, WithGridRights, indexs, auIndex, stepId, RestrictionString, DateDeclarationString, False, FilterType, HabilitarFavortios, refIndexs)

        Dim firsttask As Boolean = True
        If (String.IsNullOrEmpty(FilterString) = False) Then
            strselect.Append(" and ")
            strselect.Append(FilterString)
        End If
        strselect.Append(" and (task_id = ")
        strselect.Append(taskid.ToString)
        strselect.Append(")")
        Dim count As Long
        Return GetTaskTable(strselect, "task_id asc", 1, 1, count, True)

    End Function

    ''' <summary>
    ''' Devuelve las tareas del entidad y etapa que se le pasan por parametro
    ''' </summary>
    ''' <param name="stepId">Id de la etapa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''        [Marcelo] 16/09/10 - Created.
    '''        [Javier]  12/10/10   Modified    Se agregan parámetros Restriction y DateDeclaration
    ''' </history>
    Public Function GetTasksByTasksIdAndDocTypeId(ByVal taskids As List(Of Int64), ByVal docTypeId As Int64,
                                                  ByVal indexs As List(Of IIndex), ByVal WithGridRights As Boolean,
                                                  ByVal FilterString As String, ByVal RestrictionString As String,
                                                  ByVal DateDeclarationString As String, FilterType As SearchType,
                                                  ByVal HabilitarFavoritos As Boolean,
                                                  refIndexs As List(Of ReferenceIndex),
                                                  Optional ByVal auIndex As List(Of IIndex) = Nothing) As DataTable


        Dim strselect As StringBuilder = GetCommonTaskStringBuilder(docTypeId, WithGridRights, indexs, auIndex, 0, RestrictionString, DateDeclarationString, False, FilterType, HabilitarFavoritos, refIndexs)

        Dim firsttask As Boolean = True

        If String.IsNullOrEmpty(FilterString) = False OrElse String.IsNullOrEmpty(RestrictionString) = False Then
            strselect.Append(FilterString)
            strselect.Append(" and task_id in (")
        Else
            strselect.Append(" where ")
            strselect.Append(" task_id in (")
        End If

        For i As Int16 = 0 To taskids.Count - 1
            strselect.Append(taskids(i).ToString)
            strselect.Append(", ")
        Next
        strselect.Remove(strselect.Length - 2, 2)
        strselect.Append(")")

        Return Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString).Tables(0)

    End Function


    ''' <summary>
    ''' Ejecuta el query dado y obtiene un datatable con tareas.
    ''' </summary>
    ''' <param name="strQuery">Query a ejecutar.</param>
    ''' <param name="Orden"></param>
    ''' <param name="PageSize">Cantidad de resultados a traer</param>
    ''' <param name="LastPage">Página seleccionada</param>
    ''' <param name="ShowBoolTaskDetails">Este parametro es para evitar hacer el paginado cuando hago click en la grilla de tareas para abrir el taskviewer</param>
    ''' <returns>Datatable con las tareas.</returns>
    ''' <remarks></remarks>
    Private Function GetTaskTable(ByVal strQuery As StringBuilder, ByVal Orden As String,
                                         ByVal PageSize As Int32, ByVal LastPage As Int32,
                                         ByRef totalCount As Long,
                                         ByVal ShowBoolTaskDetails As Boolean) As DataTable

        'El desde y hasta sirven para traer una cantidad límite del resultado obtenido del paginado.
        Dim Desde As Int32 = (PageSize * LastPage) + 1
        Dim Hasta As Int32 = Desde + PageSize - 1
        Dim query As String = strQuery.ToString
        Dim Total As Int64
        Dim ds As DataSet

        'se agrego este parametro para que no haga el paginado cuando hago doble click en la grilla de tareas 

        If (ShowBoolTaskDetails) Then
            ds = Server.Con.ExecuteDataset(CommandType.Text, query)
            Return ds.Tables(0)
        Else
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Consulta de busqueda tareas")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Query: " & query.Trim)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Orden: " & Orden.Trim)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Desde: " & Desde)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Hasta: " & Hasta)

            If Server.isOracle Then
                'Se genera el query para hacer el count
                Dim strCountQuery As New StringBuilder("select count(1) ")
                strCountQuery.Append(query.Substring(query.IndexOf("FROM")))

                'Se agrega subconsulta para poder hacer paginacion
                strQuery.Insert(0, "select * from (")
                strQuery.Append(") where ")

                strQuery.AppendFormat(ORACLE_PAGINATE_FORMAT, Desde, Hasta)

                strQuery.Append(" order by ")
                strQuery.Append(Orden)

                ds = Server.Con.ExecuteDataset(CommandType.Text, strQuery.ToString())
                totalCount = Server.Con.ExecuteScalar(CommandType.Text, strCountQuery.ToString())

                Return ds.Tables(0)
            Else
                Dim parameters() As Object = {query, Orden, Desde, Hasta}
                ds = Server.Con.ExecuteDataset("zsp_search_200_search", parameters)

                'Verifica que haya traido resultados.
                If ds.Tables.Count > 0 Then
                    'Si los trajo, deberían existir 2 tablas, una con los resultados de la búsqueda
                    'y la otra con la cantidad total de resultados sin paginar.
                    If ds.Tables.Count = 2 AndAlso ds.Tables(1).Rows.Count > 0 Then
                        'Se asigna el total de resultados sin paginar. Se asigno a dicha propiedad 
                        'porque no se sabía donde guardar ese tipo de valor.
                        ds.Tables(0).MinimumCapacity = ds.Tables(1).Rows(0)(0)

                        'Seteamos el count total en la variable por referencia
                        totalCount = ds.Tables(1).Rows(0)(0)
                    End If

                    Return ds.Tables(0)
                End If
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Consulta realizada correctamente")
        End If

        Return Nothing
    End Function


    Private Function ReplaceTableName(strTableT As String, strTableI As String, query As StringBuilder) As StringBuilder
        If query.ToString.Contains("strTableT") Then
            query = query.Replace("strTableT", strTableT)
        End If
        If query.ToString.Contains("strTableI") Then
            query = query.Replace("strTableI", strTableI)
        End If
        Return query
    End Function


    Private Function GetCommonTaskStringBuilder(ByVal docTypeId As Long, ByVal WithGridRights As Boolean,
                                                ByVal indexs As List(Of IIndex), ByVal auIndex As List(Of IIndex),
                                                ByVal stepId As Long, ByVal RestrictionString As String,
                                                ByVal DateDeclarationString As String, ByVal CheckInColumnIsShortDate As Boolean,
                                                SearchType As SearchType, HabilitarFavoritos As Boolean, ByVal refindexs As List(Of ReferenceIndex)) As StringBuilder


        Dim strTableI As String = MakeTable(docTypeId, TableType.Indexs)
        Dim strTableT As String = MakeTable(docTypeId, TableType.Document)
        Dim MainJoin As String = String.Format("{0} T inner join {1} I on T.doc_id = I.doc_id", strTableT, strTableI)

        Dim strselect As StringBuilder = New StringBuilder()
        Dim whereids As New System.Text.StringBuilder

        Dim f As Int16
        strselect.Append("SELECT wfdocument.doc_Id,wfdocument.doc_type_Id, wfdocument.step_Id, Do_State_ID, IconId,")

        If CheckInColumnIsShortDate AndAlso Not Server.isOracle Then
            strselect.Append("convert(datetime,convert(char(10),CheckIn,101),101) as CheckIn,")
        Else
            strselect.Append(" CheckIn,")
        End If

        strselect.Append(" User_Asigned,usr.NAME as Username_Asigned, ")

        If Server.isOracle Then
            strselect.Append("C_Exclusive,")
        Else
            strselect.Append("Exclusive,")
        End If

        strselect.Append(" ExpireDate, User_Asigned_By, Date_Asigned_By, Task_ID, wfdocument.Task_State_ID, Remark, Tag, work_id, wfstepstates.name as state, uag.name as Asignado")
        strselect.Append(", T.DISK_GROUP_ID, T.PLATTER_ID, T.VOL_ID, T.DOC_FILE, T.OFFSET, T.NAME , T.ICON_ID, T.SHARED, wfdocument.Task_State_ID as Situacion")
        strselect.Append(", T.ver_Parent_id, T.version, T.RootId, T.original_Filename, T.NumeroVersion, disk_Volume.disk_Vol_id, disk_Volume.DISK_VOL_PATH, I.crdate")
        strselect.Append(", DL.importance IsImportant, DLS.favorite IsFavorite")

        Dim refIndexsQueryHelper As ReferenceIndexQueryHelper
        If refindexs IsNot Nothing Then
            refIndexsQueryHelper = New ReferenceIndexQueryHelper
        End If

        If WithGridRights Then

            For f = 0 To indexs.Count - 1
                strselect.Append(",")

                If Not auIndex Is Nothing AndAlso auIndex.Contains(DirectCast(indexs(f), Index)) Then

                    strselect.Append("slst_s" & DirectCast(indexs(f), Index).ID & ".descripcion")

                Else

                    If indexs(f).Type = IndexDataType.Si_No Then

                        If Server.isOracle Then
                            strselect.Append($"I.I{indexs(f).ID}, Case ")
                        Else
                            strselect.Append($" Case I.I{indexs(f).ID}")
                        End If
                        strselect.Append(" when 1 then 'Si' else 'No' end ")

                    Else

                        strselect.Append($"I.I{DirectCast(indexs(f), Index).ID}")

                    End If

                End If

                strselect.Append(" as " & Chr(34) & DirectCast(indexs(f), Index).Name.Trim & Chr(34))

            Next

        Else
            For f = 0 To indexs.Count - 1
                If refindexs IsNot Nothing AndAlso refindexs.Any(Function(_i) _i.IndexId = DirectCast(indexs(f), Index).ID) Then
                    strselect.Append($",{refIndexsQueryHelper.GetStringForDocIQuery(indexs(f).ID, docTypeId, refindexs)} AS I{indexs(f).ID}")
                Else
                    strselect.Append(",I.I" & DirectCast(indexs(f), Index).ID)
                End If
            Next
        End If

        If Server.isOracle Then
            strselect.Append(" ,row_number() over (order by Task_ID) rn")
        End If

        strselect.Append($" FROM wfdocument LEFT JOIN USRTABLE usr on WFDocument.User_Asigned = usr.ID INNER JOIN {MainJoin} On wfdocument.doc_id = T.doc_id")
        strselect.Append(" left outer join disk_Volume On disk_Vol_id = vol_id")
        strselect.Append(" left join wfstepstates On do_state_id = doc_state_id")
        strselect.Append(" left join zuser_or_group uag On wfdocument.user_asigned = uag.id ")

        If refindexs IsNot Nothing AndAlso refindexs.Count > 0 Then
            Dim joinStr As String
            For Each refInd As ReferenceIndex In refindexs
                joinStr = refIndexsQueryHelper.GetStringJoinQuery(refInd.IndexId, docTypeId, refindexs)
                If Not strselect.ToString().ToLower().Contains(joinStr.ToLower.Trim) Then
                    strselect.Append($"{joinStr} ")
                End If
            Next
        End If

        If (HabilitarFavoritos) Then
            strselect.Append(" left join DocumentLabels DL On DL.doctypeid = wfdocument.DOC_TYPE_ID And DL.docid=wfdocument.Doc_ID And DL.userid=" + Membership.MembershipHelper.CurrentUser.ID.ToString() + " ")
            strselect.Append(" left join (Select * from DocumentLabels  where userid=" + Membership.MembershipHelper.CurrentUser.ID.ToString() + ") DLS On DLS.doctypeid = wfdocument.DOC_TYPE_ID And DLS.docid = wfdocument.Doc_ID ")
        End If

        If Not auIndex Is Nothing AndAlso auIndex.Count > 0 Then
            For Each indice As IIndex In auIndex
                strselect.Append(" left join slst_s" & indice.ID & " On I.i" & indice.ID & " = slst_s" & indice.ID & ".codigo ")
            Next
        End If

        If Not String.IsNullOrEmpty(RestrictionString) AndAlso RestrictionString.Trim() <> String.Empty Then
            strselect.Append($" WHERE {RestrictionString}")
            If SearchType <> SearchType.OpenTask Then strselect.Append(" And wfdocument.step_id = " & stepId)
        Else
            If SearchType <> SearchType.OpenTask Then strselect.Append(" WHERE wfdocument.step_id = " & stepId)
        End If

        If DateDeclarationString.Length > 0 Then
            'Si existe declaracion de variables de fecha las inserta al inicio del select
            strselect.Insert(0, DateDeclarationString)
        End If

        Return strselect

    End Function

    Public Function MakeTable(ByVal docTypeId As Integer, ByVal tableType As TableType) As String
        Dim TableName As String = String.Empty

        Select Case tableType
            Case Core.TableType.Full
                TableName = "Doc" & docTypeId.ToString
            Case Core.TableType.Document
                TableName = "Doc_T" & docTypeId.ToString
            Case Core.TableType.Indexs
                TableName = "Doc_I" & docTypeId.ToString
            Case Core.TableType.Blob
                TableName = "Doc_B" & docTypeId.ToString
        End Select

        Return TableName
    End Function

    Private Function GetCommonCountTaskStringBuilder(ByVal docTypeId As Long, ByVal WithGridRights As Boolean,
                                                      ByVal stepId As Long, ByVal RestrictionString As String) As StringBuilder

        Dim strTableI As String = MakeTable(docTypeId, TableType.Indexs)
        Dim strTableT As String = MakeTable(docTypeId, TableType.Document)
        Dim MainJoin As String = String.Format("{0} T " & If(Server.isSQLServer, " WITH (NOLOCK) ", " ") & " On wfdocument.doc_id = T.doc_id  inner join {1} I " & If(Server.isSQLServer, " WITH (NOLOCK) ", " ") & " On T.doc_id = I.doc_id ", strTableT, strTableI)


        Dim strselect As StringBuilder = New StringBuilder()

        Dim whereids As New StringBuilder
        Dim f As Int16

        strselect.Append("Select count(1) As TaskCount FROM wfdocument")
        strselect.Append(If(Server.isSQLServer, " WITH (NOLOCK) ", " "))


        If Not String.IsNullOrEmpty(RestrictionString) AndAlso RestrictionString.Trim() <> String.Empty Then
            'Join para validar que las tareas existan el la doc_i
            strselect.Append(" INNER JOIN " & MainJoin)
            strselect.Append(" WHERE ")
            strselect.Append(RestrictionString)
        End If

        If strselect.ToString.IndexOf("where", StringComparison.CurrentCultureIgnoreCase) = -1 Then
            strselect.Append(" WHERE ")
        Else
            strselect.Append(" And")
        End If

        strselect.Append(" step_id = @step_id and wfdocument.doc_type_id = @entityid")

        Return strselect
    End Function

    Public Function GetIndexDropDownType(ByVal indexId As Int64, ByVal TimeOut As Int32) As Int16
        Dim query As New StringBuilder
        UcmFactory.UpdateUserTime(TimeOut)
        query.Append("Select DROPDOWN from doc_index where index_id =")
        query.Append(indexId)
        Return Server.Con.ExecuteScalar(CommandType.Text, query.ToString)
    End Function

    ''' <summary>
    ''' Agrega los where de permisos al select dado
    ''' </summary>
    ''' <param name="strselect"></param>
    ''' <param name="VerAsignadosANadie"></param>
    ''' <param name="VerAsignadosAOtros"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AppendWhereRights(ByVal strselect As StringBuilder, ByVal VerAsignadosANadie As Boolean,
                                             ByVal VerAsignadosAOtros As Boolean) As StringBuilder
        If Not VerAsignadosAOtros AndAlso VerAsignadosANadie Then
            strselect.Append(" And (user_asigned = " & RightFactory.CurrentUser.ID & " Or user_asigned = 0")
            For i As Int32 = 0 To RightFactory.CurrentUser.Groups.Count - 1
                strselect.Append(" Or user_asigned = ")
                strselect.Append(RightFactory.CurrentUser.Groups(i).ID())
            Next
            strselect.Append(")")
        ElseIf VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
            strselect.Append(" And user_asigned <> 0")
        ElseIf Not VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
            strselect.Append(" And (user_asigned = " & RightFactory.CurrentUser.ID)
            For i As Int32 = 0 To RightFactory.CurrentUser.Groups.Count - 1
                strselect.Append(" Or user_asigned = ")
                strselect.Append(RightFactory.CurrentUser.Groups(i).ID())
            Next
            strselect.Append(")")
        End If

        Return strselect
    End Function

    Public Function GetUnreadTasksCountByUserID(ByVal CurrentUser As IUser, ByVal MyTaskEntities As String) As Int64
        Dim strselect As StringBuilder = New StringBuilder()
        Dim usrsAndGroupsStr As StringBuilder = New StringBuilder()
        usrsAndGroupsStr.Append(CurrentUser.ID)
        For Each group As IUserGroup In CurrentUser.Groups
            usrsAndGroupsStr.Append(" ," & group.ID.ToString)
        Next
        strselect.Append(String.Format("Select count(1) from (Select doc_id,user_asigned FROM WFDOCUMENT " & If(Server.isSQLServer, " WITH (NOLOCK) ", " ") & " where user_asigned In ({0}) {1}) ", usrsAndGroupsStr.ToString(), If(MyTaskEntities.Length > 0, " And doc_type_id In (" & MyTaskEntities & ")", "")))
        strselect.Append(String.Format("wfd left join zdocreads zrd " & If(Server.isSQLServer, " WITH (NOLOCK) ", " ") & " On wfd.doc_id = zrd.docid And zrd.userid = {0} where userid Is null ", CurrentUser.ID.ToString()))
        Return Server.Con.ExecuteScalar(CommandType.Text, strselect.ToString())
    End Function
#End Region

#Region "Insert"
    <Obsolete("Metodo discontinuado", False)>
    Public Sub InsertTasks(ByVal Tasks As List(Of ITaskResult), ByVal WF As WorkFlow, ByVal p_StepId As Long, TimeOut As Int32)
        UcmFactory.UpdateUserTime(TimeOut)
        Try
            Dim strinsert As String = String.Empty
            Dim StepId As Long

            If p_StepId > -1 Then
                StepId = p_StepId
            Else
                StepId = WF.InitialStepIdTEMP
            End If

            For Each t As TaskResult In Tasks
                Try
                    Dim UserAsignedTo As Int32
                    Dim dateasigned As String
                    Dim userAsignedBy As Int32

                    If Not IsNothing(t.AsignedToId) Then
                        UserAsignedTo = t.AsignedToId
                        dateasigned = Server.Con.ConvertDateTime(Now)
                        If Not IsNothing(RightFactory.CurrentUser) Then
                            userAsignedBy = RightFactory.CurrentUser.ID
                        End If
                    Else
                        UserAsignedTo = 0
                        userAsignedBy = 0
                        dateasigned = "null"
                    End If

                    If Not IsNothing(WF.InitialStep) Then
                        If Server.isOracle Then
                            strinsert = "insert into wfdocument(Doc_ID,DOC_TYPE_ID,Folder_ID,step_Id,Do_State_ID,Name,IconId,CheckIn,User_Asigned,C_Exclusive,ExpireDate,User_Asigned_By,Date_Asigned_By,Task_ID,Task_State_ID,work_id) values(" _
                                    & t.ID & "," & t.DocType.ID & ",0," & StepId & "," & WF.InitialStep.InitialState.ID & ",'" & Results_Factory.EncodeQueryString(t.Name) & "'," & t.IconId & "," & Server.Con.ConvertDateTime(t.CheckIn) & "," & UserAsignedTo & ",0," & Server.Con.ConvertDateTime(t.ExpireDate) & "," & userAsignedBy & "," & dateasigned & "," & t.TaskId & "," & CInt(t.TaskState) & "," & WF.ID & ")"
                        Else
                            strinsert = "insert into wfdocument(Doc_ID,DOC_TYPE_ID,Folder_ID,step_Id,Do_State_ID,Name,IconId,CheckIn,User_Asigned,Exclusive,ExpireDate,User_Asigned_By,Date_Asigned_By,Task_ID,Task_State_ID,work_id) values(" _
                                   & t.ID & "," & t.DocType.ID & ",0," & StepId & "," & WF.InitialStep.InitialState.ID & ",'" & Results_Factory.EncodeQueryString(t.Name) & "'," & t.IconId & "," & Server.Con.ConvertDateTime(t.CheckIn) & "," & UserAsignedTo & ",0," & Server.Con.ConvertDateTime(t.ExpireDate) & "," & userAsignedBy & "," & dateasigned & "," & t.TaskId & "," & CInt(t.TaskState) & "," & WF.ID & ")"
                        End If

                        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
                        ActionsFactory.SaveActioninDB(t.ID, ObjectTypes.Documents, RightsType.AgregarDocumento, "Se agrego el documento: " & t.DocType.Name &
                                                      " con id:  " & Convert.ToString(t.ID) & ", en el WF: " & WF.Name, RightFactory.CurrentUser.ID, RightFactory.CurrentUser.ConnectionId, Environment.MachineName)
                    End If



                Catch ex As SqlClient.SqlException
                    Zamba.Core.ZClass.raiseerror(ex)
                    Throw New Exception("Ocurrio un error al insertar el documento " & t.Name & " en el WorkFlow")
                End Try
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Sub SetDocumentRead(currentUserId As Long, docTypeId As Long, docId As Long)
        Dim check As String = String.Format("select count(1) from ZDOCREADS where userid = {0} and docid = {1} and doctypeid = {2}", currentUserId, docId, docTypeId)
        Dim Checkcount As Object = Server.Con.ExecuteScalar(CommandType.Text, check)

        If Checkcount Is Nothing OrElse Int16.Parse(Checkcount.ToString) = 0 Then
            Dim query As String
            If Server.isSQLServer Then
                query = String.Format("Insert into ZDOCREADS (userid, docid, doctypeid, crdate) values ({0}, {1}, {2}, getdate())", currentUserId, docId, docTypeId)
            Else
                query = String.Format("Insert into ZDOCREADS (userid, docid, doctypeid, crdate) values ({0}, {1}, {2}, sysdate)", currentUserId, docId, docTypeId)
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, query)
        End If
    End Sub

    <Obsolete("Metodo discontinuado", False)>
    Public Sub InsertTasks(ByVal Tasks As List(Of ITaskResult), ByVal WF As WorkFlow, ByVal p_StepId As Long, ByRef tr As Transaction, ByVal TimeOut As Int32)
        UcmFactory.UpdateUserTime(TimeOut)
        Dim strinsert As String = String.Empty
        Dim StepId As Long

        If p_StepId > -1 Then
            StepId = p_StepId
        Else
            StepId = WF.InitialStepIdTEMP
        End If

        For Each t As TaskResult In Tasks
            Try
                Dim UserAsignedTo As Int32
                Dim dateasigned As String
                Dim userAsignedBy As Int32

                If Not IsNothing(t.AsignedToId) Then
                    UserAsignedTo = t.AsignedToId
                    dateasigned = Server.Con.ConvertDateTime(Now)
                    If Not IsNothing(RightFactory.CurrentUser) Then
                        userAsignedBy = RightFactory.CurrentUser.ID
                    End If
                Else
                    UserAsignedTo = 0
                    userAsignedBy = 0
                    dateasigned = "null"
                End If

                If Not IsNothing(WF.InitialStep) Then
                    If Server.isOracle Then
                        strinsert = "insert into wfdocument(Doc_ID,DOC_TYPE_ID,Folder_ID,step_Id,Do_State_ID,Name,IconId,CheckIn,User_Asigned,C_Exclusive,ExpireDate,User_Asigned_By,Date_Asigned_By,Task_ID,Task_State_ID,work_id) values(" _
                                & t.ID & "," & t.DocType.ID & ",0," & StepId & "," & WF.InitialStep.InitialState.ID & ",'" & Results_Factory.EncodeQueryString(t.Name) & "'," & t.IconId & "," & Server.Con.ConvertDateTime(t.CheckIn) & "," & UserAsignedTo & ",0," & Server.Con.ConvertDateTime(t.ExpireDate) & "," & userAsignedBy & "," & dateasigned & "," & t.TaskId & "," & CInt(t.TaskState) & "," & WF.ID & ")"
                    Else
                        strinsert = "insert into wfdocument(Doc_ID,DOC_TYPE_ID,Folder_ID,step_Id,Do_State_ID,Name,IconId,CheckIn,User_Asigned,Exclusive,ExpireDate,User_Asigned_By,Date_Asigned_By,Task_ID,Task_State_ID,work_id) values(" _
                               & t.ID & "," & t.DocType.ID & ",0," & StepId & "," & WF.InitialStep.InitialState.ID & ",'" & Results_Factory.EncodeQueryString(t.Name) & "'," & t.IconId & "," & Server.Con.ConvertDateTime(t.CheckIn) & "," & UserAsignedTo & ",0," & Server.Con.ConvertDateTime(t.ExpireDate) & "," & userAsignedBy & "," & dateasigned & "," & t.TaskId & "," & CInt(t.TaskState) & "," & WF.ID & ")"
                    End If

                    tr.Con.ExecuteNonQuery(tr.Transaction, CommandType.Text, strinsert)
                End If
            Catch ex As SqlClient.SqlException
                Zamba.Core.ZClass.raiseerror(ex)
                Throw New Exception("Ocurrio un error al insertar el documento " & t.Name & " en el WorkFlow")
            End Try
        Next

    End Sub
    Public Sub InsertTasks(ByVal Tasks As List(Of ITaskResult), ByVal WFId As Int64, ByVal StepId As Int64)
        Try
            Dim strinsert As String = String.Empty

            For Each t As TaskResult In Tasks
                Try
                    Dim UserAsignedTo As Int32
                    Dim dateasigned As String
                    Dim userAsignedBy As Int32

                    If Not IsNothing(t.AsignedToId) Then
                        UserAsignedTo = t.AsignedToId
                        dateasigned = Server.Con.ConvertDateTime(Now)
                        userAsignedBy = RightFactory.CurrentUser.ID
                    Else
                        UserAsignedTo = 0
                        userAsignedBy = 0
                        dateasigned = "null"
                    End If

                    strinsert = "insert into wfdocument(Doc_ID,DOC_TYPE_ID,Folder_ID,step_Id,Do_State_ID,Name,IconId,CheckIn,User_Asigned,Exclusive,ExpireDate,User_Asigned_By,Date_Asigned_By,Task_ID,Task_State_ID,work_id) values(" _
                               & t.ID & "," & t.DocType.ID & ",0," & StepId & "," & Zamba.Core.TaskStates.Desasignada & ",'" & Results_Factory.EncodeQueryString(t.Name) & "'," & t.IconId & "," & Server.Con.ConvertDateTime(t.CheckIn) & "," & UserAsignedTo & ",0," & Server.Con.ConvertDateTime(t.ExpireDate) & "," & userAsignedBy & "," & dateasigned & "," & t.TaskId & "," & CInt(t.TaskState) & "," & WFId & ")"
                    'EN ORACLE SE RENOMBRO LA COLUMNA EXCLUSIVE A C_EXCLUSIVE
                    If Server.isOracle Then strinsert = strinsert.Replace(",Exclusive,", ",C_Exclusive,")

                    Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
                Catch ex As SqlClient.SqlException
                    Zamba.Core.ZClass.raiseerror(ex)
                    Throw New Exception("Ocurrio un error al insertar el documento " & t.Name & " en el WorkFlow")
                End Try
            Next
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método que inserta una tarea en un workflow
    ''' </summary>
    ''' <param name="T">Tarea</param>
    ''' <param name="WFId">Id de un workflow</param>
    ''' <param name="StepId">Id de una etapa inicial</param>
    ''' <param name="initialStateId">Id del estado inicial de la etapa inicial</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	19/02/2009	Modified     Se agrego un nuevo parámetro: initialStateId, es decir, el estado inicial de la etapa
    ''' </history>
    Public Sub InsertTask(ByVal T As ITaskResult, ByVal WFId As Int64, ByVal StepId As Int64, ByVal initialStateId As Int32)

        Try

            Dim strinsert As String = String.Empty

            Try

                Dim UserAsignedTo As Int32
                Dim dateasigned As String
                Dim userAsignedBy As Int32

                If Not IsNothing(T.AsignedToId) Then
                    UserAsignedTo = T.AsignedToId
                    dateasigned = Server.Con.ConvertDateTime(Now)
                    userAsignedBy = RightFactory.CurrentUser.ID
                Else
                    UserAsignedTo = 0
                    userAsignedBy = 0
                    dateasigned = "null"
                End If

                If (IsNothing(initialStateId)) Then
                    strinsert = "insert into wfdocument(Doc_ID,DOC_TYPE_ID,Folder_ID,step_Id,Do_State_ID,Name,IconId,CheckIn,User_Asigned,Exclusive,ExpireDate,User_Asigned_By,Date_Asigned_By,Task_ID,Task_State_ID,work_id) values(" _
                   & T.ID & "," & T.DocType.ID & ",0," & StepId & "," & Zamba.Core.TaskStates.Desasignada & ",'" & Results_Factory.EncodeQueryString(T.Name) & "'," & T.IconId & "," & Server.Con.ConvertDateTime(T.CheckIn) & "," & UserAsignedTo & ",0," & Server.Con.ConvertDateTime(T.ExpireDate) & "," & userAsignedBy & "," & dateasigned & "," & T.TaskId & "," & CInt(T.TaskState) & "," & WFId & ")"
                Else
                    strinsert = "insert into wfdocument(Doc_ID,DOC_TYPE_ID,Folder_ID,step_Id,Do_State_ID,Name,IconId,CheckIn,User_Asigned,Exclusive,ExpireDate,User_Asigned_By,Date_Asigned_By,Task_ID,Task_State_ID,work_id) values(" _
                    & T.ID & "," & T.DocType.ID & ",0," & StepId & "," & initialStateId & ",'" & Results_Factory.EncodeQueryString(T.Name) & "'," & T.IconId & "," & Server.Con.ConvertDateTime(T.CheckIn) & "," & UserAsignedTo & ",0," & Server.Con.ConvertDateTime(T.ExpireDate) & "," & userAsignedBy & "," & dateasigned & "," & T.TaskId & "," & CInt(T.TaskState) & "," & WFId & ")"
                End If

                'EN ORACLE SE RENOMBRO LA COLUMNA EXCLUSIVE A C_EXCLUSIVE
                If Server.isOracle Then strinsert = strinsert.Replace("Exclusive,", "C_Exclusive,")

                Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)

            Catch ex As SqlClient.SqlException
                If ex.Message.Contains("uk_doc_id") Then
                    'Dim dscounts As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "select 'select max(doc_id) from doc_t' + doc_type_id from doc_type")
                End If
                Zamba.Core.ZClass.raiseerror(ex)
                Throw New Exception("Ocurrio un error al insertar el documento " & T.Name & " en el WorkFlow")
            End Try

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try

    End Sub


#End Region

#Region "Update"
    <Obsolete("Metodo discontinuado", False)>
    Public Event drefresh(ByRef Result As TaskResult)
    ''' <summary>
    ''' Actualiza la tabla WFDocument
    ''' </summary>
    ''' <remarks></remarks>
    <Obsolete("Metodo discontinuado", False)>
    Public Sub UpdateDistribuir(ByRef Result As TaskResult)
        'distribuye un dato solo
        Dim strupdate As New System.Text.StringBuilder
        'ZTrace.WriteLineIf(ZTrace.IsInfo,"Actualizo en la base de datos")
        Try
            strupdate.Append("UPDATE WFDOCUMENT SET STEP_ID=")
            strupdate.Append(Result.StepId)
            strupdate.Append(" ,Task_State_ID = ")
            strupdate.Append(CInt(Result.TaskState))
            strupdate.Append(",do_state_id=")
            strupdate.Append(Result.State.ID)
            strupdate.Append(", USER_ASIGNED = ")
            strupdate.Append(Result.AsignedToId)
            strupdate.Append(",USER_ASIGNED_BY=")
            strupdate.Append(Result.AsignedById)
            strupdate.Append(" ,DATE_ASIGNED_BY=null ,EXPIREDATE=")
            strupdate.Append(Server.Con.ConvertDateTime(Result.ExpireDate))
            strupdate.Append(",CheckIn=")
            strupdate.Append(Server.Con.ConvertDateTime(Result.CheckIn))
            If Server.isOracle Then
                strupdate.Append(" ,c_exclusive = 0")
            Else
                strupdate.Append(" ,exclusive = 0")
            End If
            strupdate.Append(" WHERE Task_ID = ")
            strupdate.Append(Result.TaskId)
            'ZTrace.WriteLineIf(ZTrace.IsInfo,"EJECUTO SENTENCIA")
            'ZTrace.WriteLineIf(ZTrace.IsInfo,strupdate.ToString)
            Server.Con.ExecuteNonQuery(CommandType.Text, strupdate.ToString)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        'If IsNothing(Result.AsignedTo) Then
        '    'SP 3/1/2006
        '    Try
        '        if Server.IsOracle then
        '            Dim ParValues() As Object = {Result.WfStep.Id, Server.Con.ConvertDateTime(Result.ExpireDate), Result.TaskId}
        '            'Dim ParNames() As Object = {"pStepId", "pExpDate", "pTaskId"}
        '            ' Dim parTypes() As Object = {13, 7, 13}
        '            Server.Con.ExecuteNonQuery("ZWFUpdWFTasksFactory_pkg.ZUpdWFDDlgTaskByTaskId", parValues)
        '        Else
        '            Dim ParValues() As Object = {Result.WfStep.Id, Server.Con.ConvertDateTime(Result.ExpireDate), Result.TaskId}
        '            Server.Con.ExecuteNonQuery("ZUpdWFDDlgTaskByTaskId", ParValues)
        '        End If
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'Else
        '    'SP 3/1/2006
        '    Try
        '        if Server.IsOracle then
        '            Dim ParValues() As Object = {Result.WfStep.Id, Result.AsignedTo.Id, Server.Con.ConvertDateTime(Result.ExpireDate), RightFactory.CurrentUser.id, Server.Con.ConvertDateTime(Result.AsignedDate), Result.TaskId}
        '            'Dim ParNames() As Object = {"pStepId", "pAsigned", "pExpDate", "pUserId", "pAsgDate", "pTaskId"}
        '            ' Dim parTypes() As Object = {13, 13, 7, 13, 7, 13}
        '            Server.Con.ExecuteNonQuery("ZWFUpdWFTasksFactory_pkg.ZUpdWFDDlgTaskByTaskId2", parValues)
        '        Else
        '            Dim ParValues() As Object = {Result.WfStep.Id, Result.AsignedTo.Id, Server.Con.ConvertDateTime(Result.ExpireDate), RightFactory.CurrentUser.id, Server.Con.ConvertDateTime(Result.AsignedDate), Result.TaskId}
        '            Server.Con.ExecuteNonQuery("ZUpdWFDDlgTaskByTaskId2", ParValues)
        '        End If
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'End If
        strupdate = Nothing
    End Sub


    <Obsolete("Metodo discontinuado", False)>
    Public Sub UpdateAssign(ByRef Result As TaskResult)
        Dim AsignedToId, AsignedById As Int32

        AsignedToId = Result.AsignedToId

        Dim StrBuilder As New StringBuilder()
        If Result.AsignedById <> 0 Then
            AsignedById = Result.AsignedById
            'strupdate = "UPDATE WFDOCUMENT SET USER_ASIGNED = " & Asignedtoid & " ,USER_ASIGNED_BY = " & asignedbyid & " ,DATE_ASIGNED_BY = " & queryDate & " ,Task_State_ID = " & CInt(Result.TaskState) & " WHERE Task_ID = " & Result.ID
            StrBuilder.Append("UPDATE WFDOCUMENT SET USER_ASIGNED = ")
            StrBuilder.Append(AsignedToId.ToString())
            StrBuilder.Append(" ,USER_ASIGNED_BY = ")
            StrBuilder.Append(AsignedById.ToString())
            StrBuilder.Append(" ,DATE_ASIGNED_BY = " & Zamba.Servers.Server.Con.ConvertDateTime(Result.AsignedDate))
            'StrBuilder.Append(Server.Con.ConvertDateTime(Result.AsignedDate))
            StrBuilder.Append(" ,Task_State_ID = ")
            StrBuilder.Append((CInt(Result.TaskState)).ToString())
            If ((CInt(Result.TaskState)) <> 2) Then
                If Server.isOracle Then
                    StrBuilder.Append(" ,c_exclusive = 0")
                Else
                    StrBuilder.Append(" ,exclusive = 0")
                End If
            End If
            StrBuilder.Append(" WHERE Task_ID = ")
            StrBuilder.Append(Result.TaskId.ToString())
        Else
            'LE SACO EL DATE_ASIGNED_BY PORQUE TIRA ERROR AL ESTAR COMO "00/00/0001"
            StrBuilder.Append("UPDATE WFDOCUMENT SET USER_ASIGNED = ")
            StrBuilder.Append(AsignedToId.ToString())
            StrBuilder.Append(" ,USER_ASIGNED_BY= ")
            StrBuilder.Append(AsignedById.ToString())
            StrBuilder.Append(" ,Task_State_ID = ")
            StrBuilder.Append((CInt(Result.TaskState)).ToString())
            If ((CInt(Result.TaskState)) <> 2) Then
                If Server.isOracle Then
                    StrBuilder.Append(" ,c_exclusive = 0")
                Else
                    StrBuilder.Append(" ,exclusive = 0")
                End If
            End If
            StrBuilder.Append(" WHERE Task_ID = ")
            StrBuilder.Append(Result.TaskId.ToString())
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString())
    End Sub

    Public Sub UpdateAssign(ByVal taskId As Int64, ByVal asignedToUserId As Int64, ByVal asignedByUserId As Int64, ByVal asignedDate As Date, ByVal taskStateId As Int32)
        Dim StrBuilder As New StringBuilder()

        StrBuilder.Append("UPDATE WFDOCUMENT SET USER_ASIGNED = ")
        StrBuilder.Append(asignedToUserId.ToString())
        StrBuilder.Append(" ,USER_ASIGNED_BY = ")
        StrBuilder.Append(asignedByUserId.ToString())
        StrBuilder.Append(" ,DATE_ASIGNED_BY = ")
        StrBuilder.Append(Zamba.Servers.Server.Con.ConvertDateTime(asignedDate))
        StrBuilder.Append(" ,Task_State_ID = ")
        StrBuilder.Append(taskStateId.ToString())
        If (taskStateId <> 2) Then
            If Server.isOracle Then
                StrBuilder.Append(" ,c_exclusive = 0")
            Else
                StrBuilder.Append(" ,exclusive = 0")
            End If
        End If
        StrBuilder.Append(" WHERE Task_ID = ")
        StrBuilder.Append(taskId.ToString())

        Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString())
    End Sub



    Public Sub UpdateTaskState(ByVal taskId As Int64, ByVal taskStateId As Int64)
        Dim StrBuilder As New StringBuilder()
        StrBuilder.Append("Update WFDocument Set Task_State_ID=")
        StrBuilder.Append(taskStateId.ToString())
        If (taskStateId <> 2) Then
            If Server.isOracle Then
                StrBuilder.Append(" ,c_exclusive = 0")
            Else
                StrBuilder.Append(" ,exclusive = 0")
            End If
        End If
        StrBuilder.Append(" where task_id=")
        StrBuilder.Append(taskId.ToString())

        Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString())
    End Sub

    Public Sub UpdateConIDTaskStateToAsign(ByVal conId As Int64)
        Dim StrBuilder As New StringBuilder()
        If Server.isOracle Then
            StrBuilder.Append("Update WFDocument Set Task_State_ID=1, c_exclusive = 0 where user_asigned in (SELECT USER_ID FROM UCM WHERE CON_ID =")
        Else
            StrBuilder.Append("Update WFDocument Set Task_State_ID=1, exclusive = 0 where user_asigned in (SELECT USER_ID FROM UCM WHERE CON_ID =")
        End If
        StrBuilder.Append(conId.ToString())
        StrBuilder.Append(") and Task_State_ID=2")
        Dim query As String = StrBuilder.ToString
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Sub UpdateUserTaskStateToAsign(ByVal userId As Int64)
        Dim StrBuilder As New StringBuilder()
        If Server.isOracle Then
            StrBuilder.Append("Update WFDocument Set Task_State_ID=1, c_exclusive = 0 where user_asigned=")
        Else
            StrBuilder.Append("Update WFDocument Set Task_State_ID=1, exclusive = 0 where user_asigned=")
        End If
        StrBuilder.Append(userId.ToString())
        StrBuilder.Append(" and Task_State_ID=2")

        Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString())
    End Sub

    <Obsolete("Metodo discontinuado", False)>
    Public Sub UpdateTaskState(ByRef Result As TaskResult)
        Dim StrBuilder As New StringBuilder
        StrBuilder.Append("Update WFDocument Set Task_State_ID=")
        StrBuilder.Append((CInt(Result.TaskState)).ToString())
        If ((CInt(Result.TaskState)) <> 2) Then
            If Server.isOracle Then
                StrBuilder.Append(" ,c_exclusive = 0")
            Else
                StrBuilder.Append(" ,exclusive = 0")
            End If
        End If
        StrBuilder.Append(" where task_id=")
        StrBuilder.Append(Result.TaskId.ToString())
        Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString())
    End Sub

    Public Sub UpdateState(ByVal taskId As Int64, ByVal stepId As Int32, ByVal stateId As Int32)
        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("Update WFDocument Set Do_state_id=")
        QueryBuilder.Append(stateId.ToString())
        QueryBuilder.Append(" WHERE task_id = ")
        QueryBuilder.Append(taskId.ToString())
        QueryBuilder.Append(" AND step_id = ")
        QueryBuilder.Append(stepId.ToString())

        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString)
    End Sub
    <Obsolete("Metodo discontinuado", False)>
    Public Sub UpdateState(ByRef Result As TaskResult)

        Dim sql As String = "Update WFDocument Set Do_state_id=" & Result.State.ID & " where task_id=" & Result.TaskId
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)

        'SP 29/12/05
        'Try
        '    Dim ParValues() As Object = {state.Id, result.Id, result.WfStep.Id}
        '    'Dim ParNames() As Object = {"pStateId", "pDocId", "pStepId"}
        '    ' Dim parTypes() As Object = {13, 13, 13}
        '    if Server.IsOracle then
        '        Server.Con.ExecuteNonQuery("ZWFUpdStepsFactory_pkg.ZUpdWFDStateIdByDocStepId", parValues)
        '    Else
        '        Server.Con.ExecuteNonQuery("ZUpdWFDStateIdByDocStepId", ParValues)
        '    End If
        'Catch
        'End Try

    End Sub
    <Obsolete("Metodo discontinuado", False)>
    Public Sub UpdateState(ByRef Result As TaskResult, ByRef t As Transaction)

        Dim sql As String = "Update WFDocument Set Do_state_id=" & Result.State.ID & " where task_id=" & Result.TaskId & " and step_id = " & Result.StepId
        t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, sql)

        'SP 29/12/05
        'Try
        '    Dim ParValues() As Object = {state.Id, result.Id, result.WfStep.Id}
        '    'Dim ParNames() As Object = {"pStateId", "pDocId", "pStepId"}
        '    ' Dim parTypes() As Object = {13, 13, 13}
        '    if Server.IsOracle then
        '        Server.Con.ExecuteNonQuery("ZWFUpdStepsFactory_pkg.ZUpdWFDStateIdByDocStepId", parValues)
        '    Else
        '        Server.Con.ExecuteNonQuery("ZUpdWFDStateIdByDocStepId", ParValues)
        '    End If
        'Catch
        'End Try

    End Sub
    Public Sub UpdateState(ByVal TaskId As Long, ByVal CurrentStateId As Long, ByVal StateID As Long)
        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("Update WFDocument Set Do_State_id = ")
        QueryBuilder.Append(StateID)
        QueryBuilder.Append(" where task_id = ")
        QueryBuilder.Append(TaskId)
        QueryBuilder.Append(" and step_id = ")
        QueryBuilder.Append(CurrentStateId)

        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())
        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing
    End Sub





    Public Sub UpdateExpiredDate(ByVal taskId As Int64, ByVal expireDate As Date)
        Dim QueryBuilder As New StringBuilder()

        QueryBuilder.Append("UPDATE WfDocument SET ExpireDate=")
        QueryBuilder.Append(Server.Con.ConvertDateTime(expireDate))
        QueryBuilder.Append(" WHERE Task_ID = ")
        QueryBuilder.Append(taskId.ToString())

        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())
        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing
    End Sub

    Public Sub UpdateState1(ByVal taskId As Int64, ByVal stateId As Int32)
        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("Update WFDocument Set Do_state_id=")
        QueryBuilder.Append(stateId.ToString())
        QueryBuilder.Append(" WHERE task_id = ")
        QueryBuilder.Append(taskId.ToString())
        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString)
        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing
    End Sub

    Public Sub UpdateState(ByVal taskId As Int64, ByVal stepId As Int64, ByVal stateId As Int32)

        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("Update WFDocument Set Do_state_id = ")
        QueryBuilder.Append(stateId.ToString)
        QueryBuilder.Append(" where task_id = ")
        QueryBuilder.Append(taskId.ToString)
        QueryBuilder.Append(" and step_id = ")
        QueryBuilder.Append(stepId.ToString)

        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing
    End Sub

#End Region

#Region "Tasks & Connections"
    Public Function GetUserOpenedTasks(ByVal userId As Int64) As DataTable
        Dim query As String = "select * from wfdocument where Task_ID in (Select TASKID FROM USR_R_OPENTASK " & If(Server.isSQLServer, " WITH (NOLOCK) ", " ") & " WHERE USERID =" & userId & ")"
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    Public Sub CloseOpenTasksByConId(ByVal conId As Int64)
        Server.Con.ExecuteNonQuery(CommandType.Text, "update wfdocument set task_state_id = 1 WHERE task_state_id = 2 and User_Asigned = (SELECT USER_ID FROM UCM " & If(Server.isSQLServer, " WITH (NOLOCK) ", " ") & " where CON_ID=" & conId & ")")
    End Sub

    Public Sub CloseOpenTasksByTaskId(ByVal taskId As Int64)
        Dim query As String = "DELETE USR_R_OPENTASK WHERE TASKID=" & taskId
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
        query = "update wfdocument set task_state_id = 1 WHERE task_state_id = 2 and task_id = " & taskId & ""
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Sub ReleaseOpenTasksWithOutConnection()
        Dim query As String = "DELETE USR_R_OPENTASK  WHERE TASKID in (select task_id from WFDocument " & If(Server.isSQLServer, " WITH (NOLOCK) ", " ") & "  where task_state_id = 2 and user_asigned not in (select userid from UCM " & If(Server.isSQLServer, " WITH (NOLOCK) ", " ") & "))"
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
        query = "update WFDocument  set task_state_id = 1 where task_state_id = 2 and user_asigned not in (select user_id from UCM " & If(Server.isSQLServer, " WITH (NOLOCK) ", " ") & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Sub RegisterTaskAsOpen(ByVal taskId As Int64, ByVal userId As Int64)
        If Server.isSQLServer Then
            Dim params As Object() = {userId, taskId}
            Server.Con.ExecuteNonQuery("ZSP_WORKFLOW_100_SetOpenTask", params)
            params = Nothing
        Else
            Dim query As String = "delete USR_R_OPENTASK where USERID = " & userId & " AND TASKID = " & taskId
            Server.Con.ExecuteNonQuery(CommandType.Text, query)

            query = "INSERT INTO USR_R_OPENTASK values (" & userId & "," & taskId & ", SYSDATE)"
            Server.Con.ExecuteNonQuery(CommandType.Text, query)
        End If
    End Sub
#End Region

#Region "Delete"
    Public Sub Delete(ByVal taskId As Int64)
        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("DELETE WFDOCUMENT WHERE Task_ID = ")
        QueryBuilder.Append(taskId.ToString())

        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())

    End Sub

    <Obsolete("Metodo discontinuado", False)>
    Public Sub Delete(ByRef Result As TaskResult)
        Dim strdelete As String = "DELETE WFDOCUMENT WHERE Task_ID = " & Result.TaskId
        Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
        'SP 3/1/2006
        'Try
        '    if Server.IsOracle then
        '        Dim ParValues() As Object = {Result.TaskId}
        '        'Dim ParNames() As Object = {"pTaskId"}
        '        ' Dim parTypes() As Object = {13}
        '        Server.Con.ExecuteNonQuery("ZWFDelWFTasksFactory_pkg.ZDelWFDByTaskId", parValues)
        '    Else
        '        Dim ParValues() As Object = {Result.TaskId}
        '        Server.Con.ExecuteNonQuery("ZDelWFDByTaskId", ParValues)
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub
#End Region

#Region "Logs"

    '<Obsolete("Metodo discontinuado", False)>
    'Public Sub LogCheckIn(ByVal task As TaskResult, ByVal wfstep As IWFStep)
    '    Dim State As String = "Ninguno"
    '    If Not IsNothing(task.State) Then
    '        State = task.State.Name
    '    ElseIf Not IsNothing(wfstep) AndAlso Not IsNothing(wfstep.InitialState) Then
    '        State = wfstep.InitialState.Name
    '    End If
    '    LogCheckIn(task.TaskId, task.Name, task.DocType.ID, task.DocType.Name, task.StepId, State, task.WorkId, Date.Now)
    '    State = Nothing
    'End Sub
    '<Obsolete("Metodo discontinuado", False)>
    'Public Sub LogCheckIn(ByVal task As TaskResult, ByVal wfstep As IWFStep, ByRef transact As Transaction)
    '    Dim State As String = "Ninguno"
    '    If Not IsNothing(task.State) Then
    '        State = task.State.Name
    '    ElseIf Not IsNothing(wfstep) AndAlso Not IsNothing(wfstep.InitialState) Then
    '        State = wfstep.InitialState.Name
    '    End If
    '    LogCheckIn(task.TaskId, task.Name, task.DocType.ID, task.DocType.Name, task.StepId, State, task.WorkId, Date.Now, transact)
    '    State = Nothing
    'End Sub

    Private Sub LogCheckIn(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int32, ByVal docTypeName As String, ByVal stepId As Int64, ByVal stepname As String, ByVal statename As String, ByVal workflowId As Int64, ByVal wfname As String, ByVal taskCheckIn As Date)


        If IsDBNull(stepname) AndAlso stepname Is Nothing Then
            stepname = String.Empty
        End If


        If IsDBNull(wfname) AndAlso wfname Is Nothing Then
            wfname = String.Empty
        End If

        Dim QueryBuilder As New StringBuilder()
        If Server.isOracle Then
            Dim parValues() As Object = {taskID, taskName, docTypeId, docTypeName, 0, stepId, statename, RightFactory.CurrentUser.Name, "Ingreso de tarea", Now, workflowId}
            Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO WFStepHst(Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion,  Fecha, WorkflowId, WorkflowName) VALUES (" &
                                  taskID & ",'" & taskName & "'," & docTypeId & ",'" & docTypeName & "',0," & stepId & ",'" & stepname & "','" & statename & "','" & RightFactory.CurrentUser.Name & "','Ingreso',sysdate," & workflowId & ",'" & wfname & "')")
        Else
            Dim parameters() As Object = {taskID, taskName, docTypeId, docTypeName, 0, stepId, statename, RightFactory.CurrentUser.Name, "Ingreso de tarea", Now, workflowId}
            Server.Con.ExecuteNonQuery("zsp_workflow_200_InsertWFStepHst", parameters)
        End If

        LogStepPerformance(taskID, taskName, stepId, RightFactory.CurrentUser.Name, workflowId, taskCheckIn, stepname, wfname, Nothing)

    End Sub
    Private Sub LogCheckIn(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int32, ByVal docTypeName As String,
                                  ByVal stepId As Int64, ByVal stepname As String, ByVal statename As String, ByVal workflowId As Int64, ByVal wfname As String, ByVal taskCheckIn As Date, ByRef t As Transaction)
        Dim QueryBuilder As New StringBuilder()
        If Server.isOracle Then

            If IsDBNull(stepname) AndAlso stepname Is Nothing Then
                stepname = String.Empty
            End If

            If IsDBNull(wfname) AndAlso wfname Is Nothing Then
                wfname = String.Empty
            End If

            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, "INSERT INTO WFStepHst(Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion,  Fecha, WorkflowId, WorkflowName) VALUES (" &
                                  taskID & ",'" & taskName & "'," & docTypeId & ",'" & docTypeName & "',0," & stepId & ",'" & stepname & "','" & statename & "','" & RightFactory.CurrentUser.Name & "','Ingreso',sysdate," & workflowId & ",'" & wfname & "')")
        Else
            Dim parameters() As Object = {taskID, taskName, docTypeId, docTypeName, 0, stepId, statename, RightFactory.CurrentUser.Name, "Ingreso", Now, workflowId}
            t.Con.ExecuteNonQuery(t.Transaction, "zsp_workflow_200_InsertWFStepHst", parameters)
        End If

        LogStepPerformance(taskID, taskName, stepId, RightFactory.CurrentUser.Name, workflowId, taskCheckIn, stepname, wfname, t)

    End Sub

    '[AlejandroR] - Created - 01/03/2010 (WI 4406)
    '[JavierC] - Modified - 26/08/2010 (WI 5396) Se agregan SP
    Private Sub LogStepPerformance(ByVal taskID As Int64, ByVal taskName As String, ByVal stepId As Int64, ByVal userName As String, ByVal workflowId As Int64, ByVal taskCheckIn As Date, ByVal stepname As String, WFName As String, ByRef t As Transaction)

        Dim QueryBuilder As StringBuilder
        Dim TotalMinutes As Int32
        Dim Ds As DataSet

        'Ver si hay una etapa anterior para actualizar la fecha de checkedout
        QueryBuilder = New StringBuilder()

        If Server.isOracle Then
            QueryBuilder.Append("SELECT * FROM (")
            QueryBuilder.Append("   SELECT ")
            QueryBuilder.Append("       StepId, CheckedIn ")
            QueryBuilder.Append("   FROM ")
            QueryBuilder.Append("       WfStepPerformance ")
            QueryBuilder.Append("   WHERE ")
            QueryBuilder.Append("       WorkflowId = " + workflowId.ToString)
            QueryBuilder.Append("       AND DocumentId = " + taskID.ToString)
            QueryBuilder.Append("   ORDER BY ")
            QueryBuilder.Append("       CheckedIn DESC ")
            QueryBuilder.Append(" ) WHERE RowNum = 1 ")
        Else
            QueryBuilder.Append("   SELECT TOP 1 ")
            QueryBuilder.Append("       StepId, CheckedIn ")
            QueryBuilder.Append("   FROM ")
            QueryBuilder.Append("       WfStepPerformance ")
            QueryBuilder.Append("   WHERE ")
            QueryBuilder.Append("       WorkflowId = " + workflowId.ToString)
            QueryBuilder.Append("       AND DocumentId = " + taskID.ToString)
            QueryBuilder.Append("   ORDER BY ")
            QueryBuilder.Append("       CheckedIn DESC ")
        End If

        If t Is Nothing Then
            Ds = Server.Con(False, False, True).ExecuteDataset(CommandType.Text, QueryBuilder.ToString)
        Else
            Ds = t.Con.ExecuteDataset(t.Transaction, CommandType.Text, QueryBuilder.ToString)
        End If

        'Insertar la nueva etapa y su fecha de checkedin
        TotalMinutes = System.Convert.ToInt32(Date.Now.Subtract(taskCheckIn).TotalMinutes)

        'PRIMERO
        If Server.isOracle Then
            'Implementar luego para ORACLE            
        Else
            Dim WF As New WFFactory
            Dim parameters() As Object = {taskID.ToString, taskName, taskCheckIn, Date.Now, stepId.ToString, stepname, TotalMinutes.ToString, workflowId.ToString, WFName, userName}
            WF = Nothing
            If t Is Nothing Then
                Server.Con(False, False, True).ExecuteNonQuery("zsp_workflow_100_InsertWfStepPerformance", parameters)
            Else
                t.Con.ExecuteNonQuery(t.Transaction, "zsp_workflow_100_InsertWfStepPerformance", parameters)
            End If
        End If


        'Si hay una etapa anterior actualizar la fecha de checkedout
        If Ds.Tables.Count > 0 AndAlso Ds.Tables(0).Rows.Count > 0 Then

            TotalMinutes = System.Convert.ToInt32(Date.Now.Subtract(Ds.Tables(0).Rows(0).Item("CheckedIn")).TotalMinutes)

            'SEGUNDO
            If Server.isOracle Then
                'Implementar luego para ORACLE            
            Else
                Dim parameters() As Object = {Date.Now, TotalMinutes.ToString(), workflowId.ToString, taskID.ToString, Ds.Tables(0).Rows(0).Item("StepId").ToString()}
                If t Is Nothing Then
                    Server.Con(False, False, True).ExecuteNonQuery("zsp_workflow_100_UpdCheckedOut_WfStepPerformance", parameters)
                Else
                    t.Con.ExecuteNonQuery(t.Transaction, "zsp_workflow_100_UpdCheckedOut_WfStepPerformance", parameters)
                End If
            End If

        End If

    End Sub


    Public Sub LogTask(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int32, ByVal docTypeName As String, ByVal folderId As Int32, ByVal stepId As Int64, ByVal stepname As String, ByVal stateName As String, ByVal workflowId As Int64, ByVal wfname As String, ByVal CurrentUserName As String, ByVal Action As String)
        If Server.isOracle Then
            If (IsDBNull(stepname) AndAlso stepname Is Nothing) Or String.IsNullOrEmpty(stepname) Then
                stepname = "-"
            End If
            If (IsDBNull(wfname) AndAlso wfname Is Nothing) Or String.IsNullOrEmpty(wfname) Then
                wfname = "-"
            End If
            If (IsDBNull(stateName) AndAlso stateName Is Nothing) Or String.IsNullOrEmpty(stateName) Then
                stateName = "-"
            End If
            Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO WFStepHst(Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion,  Fecha, WorkflowId, WorkflowName) VALUES (" &
                                  taskID & ",'" & taskName & "'," & docTypeId & ",'" & docTypeName & "',0," & stepId & ",'" & stepname & "','" & stateName & "','" & RightFactory.CurrentUser.Name & "','" & Action & "',sysdate," & workflowId & ",'" & wfname & "')")
        Else
            Dim parameters() As Object = {taskID, taskName, docTypeId, docTypeName, 0, stepId, stateName, RightFactory.CurrentUser.Name, Action, Now, workflowId}
            Server.Con.ExecuteNonQuery("zsp_workflow_200_InsertWFStepHst", parameters)
        End If
    End Sub






    Private Sub LogChangeStepState(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int32, ByVal docTypeName As String, ByVal stepId As Int64, ByVal stepname As String, ByVal state As String, ByVal stateName As String, ByVal workflowId As Int64, ByVal wfname As String)
        If Server.isOracle Then

            If IsDBNull(stepname) AndAlso stepname Is Nothing Then
                stepname = String.Empty
            End If


            If IsDBNull(wfname) AndAlso wfname Is Nothing Then
                wfname = String.Empty
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO WFStepHst(Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion,  Fecha, WorkflowId, WorkflowName) VALUES (" &
                                  taskID & ",'" & taskName & "'," & docTypeId & ",'" & docTypeName & "',0," & stepId & ",'" & stepname & "','" & stateName & "','" & RightFactory.CurrentUser.Name & "','Cambio Estado',sysdate," & workflowId & ",'" & wfname & "')")

        Else
            Dim parameters() As Object = {taskID, taskName, docTypeId, docTypeName, 0, stepId, stateName, RightFactory.CurrentUser.Name, "Cambio Estado", Now, workflowId}
            Server.Con.ExecuteNonQuery("zsp_workflow_200_InsertWFStepHst", parameters)
        End If
    End Sub
    Private Sub LogChangeStepState(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int32, ByVal docTypeName As String, ByVal stepId As Int64, ByVal stepname As String, ByVal state As String, ByVal stateName As String, ByVal workflowId As Int64, ByVal wfname As String, ByRef t As Transaction, TimeOut As Int32)
        Dim UCMF As New UcmFactory
        UCMF.UpdateUserTime(TimeOut)
        If Server.isOracle Then

            If IsDBNull(stepname) AndAlso stepname Is Nothing Then
                stepname = String.Empty
            End If


            If IsDBNull(wfname) AndAlso wfname Is Nothing Then
                wfname = String.Empty
            End If

            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, "INSERT INTO WFStepHst(Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion,  Fecha, WorkflowId, WorkflowName) VALUES (" &
                                  taskID & ",'" & taskName & "'," & docTypeId & ",'" & docTypeName & "',0," & stepId & ",'" & stepname & "','" & stateName & "','" & RightFactory.CurrentUser.Name & "','Cambio Estado',sysdate," & workflowId & ",'" & wfname & "')")

        Else
            Dim parameters() As Object = {taskID, taskName, docTypeId, docTypeName, 0, stepId, stateName, RightFactory.CurrentUser.Name, "Cambio Estado", Now, workflowId}
            t.Con.ExecuteNonQuery(t.Transaction, "zsp_workflow_200_InsertWFStepHst", parameters)
        End If
    End Sub

    Public Sub LogStartTask(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int32, ByVal docTypeName As String, ByVal stepId As Int64, ByVal stepname As String, ByVal state As String, ByVal workflowId As Int64, ByVal wfname As String)
        If Server.isOracle Then

            If IsDBNull(stepname) AndAlso stepname Is Nothing Then
                stepname = String.Empty
            End If

            If IsDBNull(wfname) AndAlso wfname Is Nothing Then
                wfname = String.Empty
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO WFStepHst(Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion,  Fecha, WorkflowId, WorkflowName) VALUES (" &
                                  taskID & ",'" & taskName & "'," & docTypeId & ",'" & docTypeName & "',0," & stepId & ",'" & stepname & "','" & state & "','" & RightFactory.CurrentUser.Name & "','Inicio Tarea',sysdate," & workflowId & ",'" & wfname & "')")
        Else
            Dim parameters() As Object = {taskID, taskName, docTypeId, docTypeName, 0, stepId, state, RightFactory.CurrentUser.Name, "Inicio Tarea", Now, workflowId}
            Server.Con.ExecuteNonQuery("zsp_workflow_200_InsertWFStepHst", parameters)
        End If
    End Sub



    Public Sub LogFinishTask(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int32, ByVal docTypeName As String, ByVal stepId As Int64, ByVal stepname As String, ByVal state As String, ByVal workflowId As Int64, ByVal wfname As String)
        If Server.isOracle Then

            If IsDBNull(stepname) AndAlso stepname Is Nothing Then
                stepname = String.Empty
            End If


            If IsDBNull(wfname) AndAlso wfname Is Nothing Then
                wfname = String.Empty
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO WFStepHst(Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion,  Fecha, WorkflowId, WorkflowName) VALUES (" &
                                  taskID & ",'" & taskName & "'," & docTypeId & ",'" & docTypeName & "',0," & stepId & ",'" & stepname & "','" & state & "','" & RightFactory.CurrentUser.Name & "','Finalizo Tarea',sysdate," & workflowId & ",'" & wfname & "')")

        Else
            Dim parameters() As Object = {taskID, taskName, docTypeId, docTypeName, 0, stepId, state, RightFactory.CurrentUser.Name, "Finalizo Tarea", Now, workflowId}
            Server.Con.ExecuteNonQuery("zsp_workflow_200_InsertWFStepHst", parameters)
        End If
    End Sub


    Public Sub LogUserAction(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int32, ByVal docTypeName As String, ByVal stepId As Int64, ByVal stepname As String, ByVal statename As String, ByVal workflowId As Int64, ByVal wfname As String, ByVal accionDeUsuario As String, ByVal TimeOut As Int32)
        '473 = Longitud de: "Ejecuto acción de usuario: ". La longitud de la columna es de 500.
        UcmFactory.UpdateUserTime(TimeOut)

        If accionDeUsuario.Length > 473 Then
            accionDeUsuario = accionDeUsuario.Substring(0, 473)
        End If

        If taskName.Length = 0 Then
            taskName = " "
        End If
        If docTypeName.Length = 0 Then
            docTypeName = " "
        End If
        If statename.Length = 0 Then
            statename = " "
        End If

        If Server.isOracle Then

            If stepId > 0 Then

                If IsDBNull(stepname) OrElse stepname Is Nothing Then
                    stepname = String.Empty
                End If
            End If


            If workflowId > 0 Then

                If IsDBNull(wfname) OrElse wfname Is Nothing Then
                    wfname = String.Empty
                End If
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO WFStepHst(Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion,  Fecha, WorkflowId, WorkflowName) VALUES (" &
                                  taskID & ",'" & taskName & "'," & docTypeId & ",'" & docTypeName & "',0," & stepId & ",'" & stepname & "','" & statename & "','" & RightFactory.CurrentUser.Name & "','Ejecuto acción de usuario: " & accionDeUsuario & "',sysdate," & workflowId & ",'" & wfname & "')")

        Else
            Dim parameters() As Object = {taskID, taskName, docTypeId, docTypeName, 0, stepId, statename, RightFactory.CurrentUser.Name, "Ejecuto acción de usuario: " & accionDeUsuario, Now, workflowId}
            Server.Con.ExecuteNonQuery("zsp_workflow_200_InsertWFStepHst", parameters)
        End If
    End Sub

    ''' <summary>
    ''' Guarda en el historial de la tarea alguna acción especifica por el comentario ingresado
    ''' </summary>
    ''' <param name="taskId"></param>
    ''' <param name="taskName"></param>
    ''' <param name="docTypeId"></param>
    ''' <param name="docTypeName"></param>

    ''' <param name="stepId"></param>
    ''' <param name="state"></param>
    ''' <param name="workflowId"></param>
    ''' <param name="comment">Comentario de la acción en particular</param>
    ''' <remarks>El comentario debe ser igual o menor de 500 caracteres. En caso de ser superado se truncará.</remarks>
    ''' <history>
    '''     Tomas   12/05/2011  Created
    ''' </history>
    Public Sub LogOtherActions(ByVal taskId As Int64, ByVal taskName As String, ByVal docTypeId As Int32, ByVal docTypeName As String, ByVal stepId As Int64, ByVal stepname As String, ByVal statename As String, ByVal workflowId As Int64, ByVal wfname As String, ByVal comment As String)
        If comment.Length > 500 Then
            comment = comment.Substring(0, 500)
        End If

        If docTypeName.Length = 0 Then
            docTypeName = " "
        End If
        If Server.isOracle Then

            If IsDBNull(stepname) AndAlso stepname Is Nothing Then
                stepname = String.Empty
            End If


            If IsDBNull(wfname) AndAlso wfname Is Nothing Then
                wfname = String.Empty
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO WFStepHst(Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion,  Fecha, WorkflowId, WorkflowName) VALUES (" &
                                  taskId & ",'" & taskName & "'," & docTypeId & ",'" & docTypeName & "',0," & stepId & ",'" & stepname & "','" & statename & "','" & RightFactory.CurrentUser.Name & "','" & comment & "',sysdate," & workflowId & ",'" & wfname & "')")

        Else
            Dim parameters() As Object = {taskId, taskName, docTypeId, docTypeName, 0, stepId, statename, RightFactory.CurrentUser.Name, comment, Now, workflowId}
            Server.Con.ExecuteNonQuery("zsp_workflow_200_InsertWFStepHst", parameters)
        End If
    End Sub

    ''' <summary>
    ''' Loguea una accion en el historial de la tarea
    ''' </summary>
    ''' <param name="taskID"></param>
    ''' <param name="taskName"></param>
    ''' <param name="docTypeId"></param>
    ''' <param name="docTypeName"></param>

    ''' <param name="stepId"></param>
    ''' <param name="state"></param>
    ''' <param name="workflowId"></param>
    ''' <param name="comentario"></param>
    ''' <remarks></remarks>


    '<Obsolete("Metodo discontinuado", False)>
    'Public Sub LogViewTask(ByVal task As TaskResult, ByVal user As IUser)
    '    Dim State As String = "Ninguno"
    '    If IsNothing(task.State) = False Then State = task.State.Name
    '    LogViewTask(task.TaskId, task.Name, task.DocType.ID, task.DocType.Name, task.StepId, State, task.WorkId)
    '    State = Nothing
    'End Sub
    Public Sub LogViewTask(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int32, ByVal docTypeName As String, ByVal stepId As Int64, ByVal stepname As String, ByVal statename As String, ByVal workflowId As Int64, ByVal wfname As String)
        If Server.isOracle Then

            If IsDBNull(stepname) AndAlso stepname Is Nothing Then
                stepname = String.Empty
            End If


            If IsDBNull(wfname) AndAlso wfname Is Nothing Then
                wfname = String.Empty
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO WFStepHst(Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion,  Fecha, WorkflowId, WorkflowName) VALUES (" &
                                  taskID & ",'" & taskName & "'," & docTypeId & ",'" & docTypeName & "',0," & stepId & ",'" & stepname & "','" & statename & "','" & RightFactory.CurrentUser.Name & "','Consulto Tarea',sysdate," & workflowId & ",'" & wfname & "')")
        Else
            Dim parameters() As Object = {taskID, taskName, docTypeId, docTypeName, 0, stepId, statename, RightFactory.CurrentUser.Name, "Consulto Tarea", Now, workflowId}
            Server.Con.ExecuteNonQuery("zsp_workflow_200_InsertWFStepHst", parameters)
        End If
    End Sub

    '<Obsolete("Metodo discontinuado", False)>
    'Public Sub LogAsignedTask(ByVal task As TaskResult)
    '    Dim AsignedTo As String = "Ninguno"
    '    Dim State As String = "Ninguno"
    '    If IsNothing(task.State) = False Then State = task.State.Name
    '    Dim IsGroup As Boolean
    '    If task.AsignedToId <> 0 Then AsignedTo = UserGroups.GetUserorGroupNamebyId(task.AsignedToId, IsGroup)

    '    Dim WTF As New WFTasksFactory
    '    WTF.LogAsignedTask(task.TaskId, task.Name, task.DocType.ID, task.DocType.Name, task.StepId, State, AsignedTo, task.WorkId)
    '    WTF = Nothing
    '    AsignedTo = Nothing
    '    State = Nothing
    'End Sub

    Public Sub LogAsignedTask(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int32, ByVal docTypeName As String, ByVal stepId As Int64, ByVal stepname As String, ByVal statename As String, ByVal assignedTo As String, ByVal workflowId As Int64, ByVal wfname As String)

        If Server.isOracle Then

            If IsDBNull(stepname) AndAlso stepname Is Nothing Then
                stepname = String.Empty
            End If


            If IsDBNull(wfname) AndAlso wfname Is Nothing Then
                wfname = String.Empty
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO WFStepHst(Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion,  Fecha, WorkflowId, WorkflowName) VALUES (" &
                                  taskID & ",'" & taskName & "'," & docTypeId & ",'" & docTypeName & "',0," & stepId & ",'" & stepname & "','" & statename & "','" & RightFactory.CurrentUser.Name & "','Asigno Tarea a " & assignedTo & "',sysdate," & workflowId & ",'" & wfname & "')")
        Else
            Dim parameters() As Object = {taskID, taskName, docTypeId, docTypeName, 0, stepId, statename, RightFactory.CurrentUser.Name, "Asigno Tarea a " & assignedTo, Now, workflowId}
            Server.Con.ExecuteNonQuery("zsp_workflow_200_InsertWFStepHst", parameters)
        End If
    End Sub

    '<Obsolete("Metodo discontinuado", False)>
    'Public Sub LogChangeExpireDate(ByVal task As TaskResult)
    '    Dim State As String = "Ninguno"
    '    If IsNothing(task.State) = False Then State = task.State.Name
    '    LogChangeExpireDate(task.TaskId, task.Name, task.DocType.ID, task.DocType.Name, task.StepId, State, task.WorkId)
    '    State = Nothing
    'End Sub

    Public Sub LogChangeExpireDate(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int32, ByVal docTypeName As String, ByVal stepId As Int64, ByVal stepname As String, ByVal statename As String, ByVal workflowId As Int64, ByVal wfname As String)

        If Server.isOracle Then

            If IsDBNull(stepname) AndAlso stepname Is Nothing Then
                stepname = String.Empty
            End If


            If IsDBNull(wfname) AndAlso wfname Is Nothing Then
                wfname = String.Empty
            End If

            Server.Con.ExecuteNonQuery(CommandType.Text, "INSERT INTO WFStepHst(Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion,  Fecha, WorkflowId, WorkflowName) VALUES (" &
                                  taskID & ",'" & taskName & "'," & docTypeId & ",'" & docTypeName & "',0," & stepId & ",'" & stepname & "','" & statename & "','" & RightFactory.CurrentUser.Name & "','Cambio Vencimiento',sysdate," & workflowId & ",'" & wfname & "')")
        Else
            Dim parameters() As Object = {taskID, taskName, docTypeId, docTypeName, 0, stepId, statename, RightFactory.CurrentUser.Name, "Cambio Vencimiento", Now, workflowId}
            Server.Con.ExecuteNonQuery("zsp_workflow_200_InsertWFStepHst", parameters)
        End If
    End Sub

    Public Function GetLogInformation(ByVal taskId As Int64) As TaskLogInformation
        If Server.isOracle = False Then
            Dim Parameters(0) As Object
            Parameters(0) = taskId
            Dim TaskLogInformation As New TaskLogInformation()
            Dim DsLogInformation As DataSet = Server.Con.ExecuteDataset(TaskLogInformation.STORED_PROCEDURE_GET_LOG_INFO, Parameters)
            If Not IsNothing(DsLogInformation) AndAlso DsLogInformation.Tables(0).Rows.Count > 0 Then
                Return BuildLogInformation(DsLogInformation.Tables(0).Rows(0))
            End If
        Else
            Dim query As New StringBuilder
            query.Append("Select WFDocument.Name as " & Chr(34) & "DocumentName" & Chr(34) & ",")
            query.Append(" WFDocument.DOC_TYPE_ID as " & Chr(34) & "DocTypeId" & Chr(34) & ",")
            query.Append(" DOC_TYPE.DOC_TYPE_NAME as " & Chr(34) & "DocTypeName" & Chr(34) & ",")
            query.Append(" WFDocument.Step_Id as " & Chr(34) & "StepId" & Chr(34) & ",  ")
            query.Append(" WFDocument.Task_State_Id as " & Chr(34) & "TaskStateId" & Chr(34) & ",")
            query.Append(" WfTask_States.Task_State_Name as " & Chr(34) & "TaskStateName" & Chr(34) & ",")
            query.Append(" WFDocument.Work_Id as " & Chr(34) & "WorkflowId" & Chr(34) & " ")
            query.Append(" From WFDocument Inner Join DOC_TYPE  ")
            query.Append(" ON WFDocument.DOC_TYPE_ID = DOC_TYPE.DOC_TYPE_ID ")
            query.Append(" Inner Join WfTask_States ")
            query.Append(" ON WfDocument.Task_State_Id = ")
            query.Append(" WfTask_States.Task_State_Id ")
            query.Append("Where ")
            query.Append(" WFDocument.task_Id = ")
            query.Append(taskId)
            Dim DsLogInformation As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
            If Not IsNothing(DsLogInformation) AndAlso DsLogInformation.Tables(0).Rows.Count > 0 Then
                Return BuildLogInformation(DsLogInformation.Tables(0).Rows(0))
            End If
        End If
        Return Nothing
    End Function

    Private Function BuildLogInformation(ByVal dr As DataRow) As TaskLogInformation

        Dim DocTypeID As Int64
        Dim DocTypeName As String = String.Empty
        Dim DocumentName As String = String.Empty
        Dim stepId As Int64
        Dim TaskStateId As Int32
        Dim TaskStateName As String = String.Empty
        Dim WorkflowId As Int64

        If Not IsNothing(dr) Then

            If Not IsNothing(dr("DocumentName")) Then
                DocumentName = dr("DocumentName").ToString
            End If

            If Not IsNothing(dr("DocTypeName")) Then
                DocTypeName = dr("DocTypeName").ToString
            End If

            If Not IsNothing(dr("TaskStateName")) Then
                TaskStateName = dr("TaskStateName").ToString
            End If

            If Not IsNothing(dr("DocTypeId")) Then
                Int64.TryParse(dr("DocTypeId").ToString(), DocTypeID)
            End If


            If Not IsNothing(dr("TaskStateId")) Then
                Int32.TryParse(dr("TaskStateId").ToString(), TaskStateId)
            End If

            If Not IsNothing(dr("stepId")) Then
                Int64.TryParse(dr("stepId").ToString(), stepId)
            End If

            If Not IsNothing(dr("WorkflowId")) Then
                Int64.TryParse(dr("WorkflowId").ToString(), WorkflowId)
            End If

        End If

        Return New TaskLogInformation(DocumentName, DocTypeID, DocTypeName, TaskStateId, TaskStateName, stepId, WorkflowId)
    End Function

    Public Class TaskLogInformation
        Implements IDisposable

#Region "Constantes"
        Public STORED_PROCEDURE_GET_LOG_INFO As String = "zsp_100_GetLogInformation"
#End Region

#Region "Atributos"
        Private _taskName As String = String.Empty
        Private _docTypeID As Int64
        Private _docTypeName As String = String.Empty
        Private _stepId As Int64
        Private _taskStateId As Int32
        Private _taskStateName As String = String.Empty
        Private _workflowId As Int64
#End Region

#Region "Propiedades"
        Public ReadOnly Property TaskName() As String
            Get
                Return _taskName
            End Get
        End Property
        Public ReadOnly Property DocTypeID() As Int64
            Get
                Return _docTypeID
            End Get
        End Property
        Public ReadOnly Property DocTypeName() As String
            Get
                Return _docTypeName
            End Get
        End Property

        Public ReadOnly Property StepId() As Int64
            Get
                Return _stepId
            End Get
        End Property

        Public ReadOnly Property TaskStateId() As Int32
            Get
                Return _taskStateId
            End Get
        End Property
        Public ReadOnly Property TaskStateName() As String
            Get
                Return _taskStateName
            End Get
        End Property
        Public ReadOnly Property WorkflowId() As Int64
            Get
                Return _workflowId
            End Get
        End Property
#End Region

#Region "Constructores"
        Public Sub New()

        End Sub
        Public Sub New(ByVal documentName As String, ByVal docTypeID As Int64, ByVal docTypeName As String, ByVal taskStateId As Int32, ByVal taskStateName As String, ByVal stepId As Int64, ByVal workflowId As Int64)
            _docTypeID = docTypeID
            _docTypeName = docTypeName
            _taskName = documentName

            _stepId = stepId
            _taskStateId = taskStateId
            _taskStateName = taskStateName
            _workflowId = workflowId
        End Sub
#End Region


        Public Sub Dispose() Implements IDisposable.Dispose
            _taskName = Nothing
            _docTypeID = Nothing
            _docTypeName = Nothing
            _stepId = Nothing
            _taskStateId = Nothing
            _taskStateName = Nothing
            _workflowId = Nothing
        End Sub
    End Class

    Public Function CountFilesInIP_Task(ByVal conf_Id As Decimal) As Integer
        Dim strselect As String = "SELECT COUNT(1) FROM IP_Task WHERE Id = " & conf_Id
        Return Server.Con.ExecuteScalar(CommandType.Text, strselect)
    End Function
#End Region


#Region "WebParts"

    Public Function GetTasksAverageTimeInSteps(ByVal workflowid As Int64) As Hashtable
        Dim query As New StringBuilder()
        query.Append("SELECT Doc_id,Stepid,UserName,Accion,Fecha")
        query.Append(" FROM WFStepHst")
        query.Append(" WHERE workflowid = ")
        query.Append(workflowid.ToString)
        query.Append(" AND ( accion = 'Ingreso' OR accion = 'Egreso')")
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

        Dim steps As SortedList = WFStepsFactory.GetStepsByWorkId(workflowid)

        Dim docsIds As New List(Of Int64)
        Dim DocsIngresos As New Generic.List(Of DataRow)
        Dim DocsEgresos As New Generic.List(Of DataRow)

        Dim TiempoIngEgr As New SortedList
        Dim averagetimeByStep As New Hashtable
        Dim StepId As Int64

        For Each wfs As WFStep In steps.Values
            StepId = wfs.ID
            TiempoIngEgr.Clear()
            docsIds.Clear()
            DocsIngresos.Clear()
            DocsEgresos.Clear()
            If Not IsNothing(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                Dim rows As DataRow() = ds.Tables(0).Select("StepId = " & StepId.ToString)
                For Each r As DataRow In rows
                    If docsIds.Contains(Int64.Parse(r.Item(0).ToString)) = False Then
                        docsIds.Add(Int64.Parse(r.Item(0).ToString))
                    End If
                    Select Case r.Item("Accion").ToString
                        Case "Ingreso"
                            DocsIngresos.Add(r)
                        Case "Egreso"
                            DocsEgresos.Add(r)
                        Case Else

                    End Select
                Next
                Dim TiempoIngreso As New DateTime
                Dim TiempoEgreso As New DateTime
                Dim docidEgr As Int64

                For Each DocId As Int64 In docsIds
                    TiempoIngreso = Nothing
                    TiempoEgreso = Nothing
                    For Each r As DataRow In DocsIngresos
                        If DocId = Int64.Parse(r.Item(0).ToString) Then
                            TiempoIngreso = DateTime.Parse(r.Item(4).ToString)
                            For Each row As DataRow In DocsEgresos
                                docidEgr = Int64.Parse(row.Item(0).ToString)
                                If DocId = docidEgr Then
                                    TiempoEgreso = DateTime.Parse(row.Item(4).ToString)
                                    If Not IsNothing(TiempoIngreso) AndAlso Not IsNothing(TiempoEgreso) Then
                                        If TiempoIngEgr.ContainsKey(docidEgr) = False Then
                                            TiempoIngEgr.Add(Int64.Parse(row.Item(0).ToString), System.Convert.ToInt32(TiempoEgreso.Subtract(TiempoIngreso).TotalMinutes))
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    Next
                Next
            End If
            Dim sum As Int64 = 0
            Dim cont As Int32 = 0
            For Each docid As Int64 In TiempoIngEgr.Keys
                cont += 1
                sum += TiempoIngEgr.Item(docid).ToString
            Next

            If sum > 0 Then
                averagetimeByStep.Add(StepId, sum / cont)
            End If
        Next
        Return averagetimeByStep
    End Function

    ''' <summary>
    ''' Método que sirve para obtener el tiempo promedio de una etapa
    ''' </summary>
    ''' <param name="name">Nombre de la consulta</param>
    ''' <param name="tables"></param>
    ''' <param name="fields"></param>
    ''' <param name="relations"></param>
    ''' <param name="conditions"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	26/11/2008	Modified    Se agrego a la consulta el inner join
    ''' </history>
    Public Function GetTasksAverageTimeByStep(ByVal stepid As Int64) As Hashtable

        Dim query As New StringBuilder()
        query.Append("SELECT WFStepHst.Doc_id,Stepid,UserName,Accion,Fecha")
        query.Append(" FROM WFStepHst")
        query.Append(" inner join wfdocument on WFStepHst.Doc_id = wfdocument.Doc_id")
        query.Append(" WHERE Stepid = ")
        query.Append(stepid.ToString)
        query.Append(" AND ( accion = 'Ingreso' OR accion = 'Egreso')")

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

        Dim docsIds As New List(Of Int64)
        Dim DocsIngresos As New Generic.List(Of DataRow)
        Dim DocsEgresos As New Generic.List(Of DataRow)

        Dim TiempoIngEgr As New SortedList
        Dim averagetimeByStep As New Hashtable

        If Not IsNothing(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
            For Each r As DataRow In ds.Tables(0).Rows
                If docsIds.Contains(Int64.Parse(r.Item(0).ToString)) = False Then
                    docsIds.Add(Int64.Parse(r.Item(0).ToString))
                End If
                Select Case r.Item("Accion").ToString
                    Case "Ingreso"
                        DocsIngresos.Add(r)
                    Case "Egreso"
                        DocsEgresos.Add(r)
                    Case Else

                End Select
            Next
            Dim TiempoIngreso As New DateTime
            Dim TiempoEgreso As New DateTime
            Dim docidEgr As Int64

            For Each DocId As Int64 In docsIds
                TiempoIngreso = Nothing
                TiempoEgreso = Nothing
                For Each r As DataRow In DocsIngresos
                    If DocId = Int64.Parse(r.Item(0).ToString) Then
                        TiempoIngreso = DateTime.Parse(r.Item(4).ToString)
                        For Each row As DataRow In DocsEgresos
                            docidEgr = Int64.Parse(row.Item(0).ToString)
                            If DocId = docidEgr Then
                                TiempoEgreso = DateTime.Parse(row.Item(4).ToString)
                                If Not IsNothing(TiempoIngreso) AndAlso Not IsNothing(TiempoEgreso) Then
                                    If TiempoIngEgr.ContainsKey(docidEgr) = False Then
                                        TiempoIngEgr.Add(Int64.Parse(row.Item(0).ToString), System.Convert.ToInt32(TiempoEgreso.Subtract(TiempoIngreso).TotalMinutes))
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                    End If
                Next
            Next
        End If
        Dim sum As Int64
        Dim cont As Int32
        For Each docid As Int64 In TiempoIngEgr.Keys
            cont += 1
            sum += TiempoIngEgr.Item(docid).ToString
        Next

        If sum > 0 Then
            averagetimeByStep.Add(stepid, sum / cont)
        End If

        Return averagetimeByStep

    End Function

    Public Function GetTasksToExpireGroupByStep(ByVal workflowid As Int64, ByVal FromHours As Int32, ByVal ToHours As Int32) As DataSet
        Dim parvalues() As Object = {workflowid, FromHours, ToHours}
        Return Server.Con.ExecuteDataset("sp_GetTaskToExpireGroupByStep", parvalues)
    End Function
    Public Function GetExpiredTasksGroupByUser(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_GetExpiredTasksGroupByUser", parvalues)
    End Function

    Public Function GetExpiredTasksGroupByStep(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_GetExpiredTasksGroupByStep", parvalues)
    End Function

    Public Function GetTasksToExpireGroupByUser(ByVal workflowid As Int64, ByVal FromHours As Int32, ByVal ToHours As Int32) As DataSet
        Dim parvalues() As Object = {workflowid, FromHours, ToHours}
        Return Server.Con.ExecuteDataset("sp_GetTaskToExpireGroupByUser", parvalues)
    End Function

    Public Function GetTasksBalanceGroupByWorkflow(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_GetTasksBalanceByWorkflow", parvalues)
    End Function

    Public Function GetTasksBalanceGroupByStep(ByVal stepid As Int64) As DataSet
        Dim parvalues() As Object = {stepid}
        Return Server.Con.ExecuteDataset("sp_GetTasksBalanceByStep", parvalues)
    End Function

    Public Function GetMyTasks(userId As Int64) As DataSet
        If Server.isSQLServer Then
            Return Server.Con.ExecuteDataset(CommandType.Text, String.Format("select Task_id, Tarea, Etapa, Asignado,Ingreso, Vencimiento, DOC_ID, DOC_TYPE_ID from (select  distinct  Task_Id, WF.Name Tarea, s.name Etapa, isnull(g.name,'') Asignado, wf.checkin Ingreso, wf.expiredate Vencimiento, wf.user_asigned, DOC_ID, DOC_TYPE_ID  from wfdocument wf  with (nolock)  inner join wfstep s  with (nolock) on s.step_id = wf.step_id inner join zuser_or_group g  with (nolock) on g.id = wf.user_asigned where user_asigned = {0} or user_asigned in (select groupid from usr_r_group r  with (nolock) where r.USRID = {0})) q order by ingreso desc", userId))
        Else
            Return Server.Con.ExecuteDataset(CommandType.Text, String.Format("select Task_id, Tarea, Etapa, Asignado,CONVERT(varchar,Ingreso,103) Ingreso, CONVERT(varchar,Vencimiento,103) Vencimiento, DOC_TYPE_ID from (select  distinct  top(200) Task_Id, WF.Name Tarea, s.name Etapa, isnull(g.name,'') Asignado, wf.checkin Ingreso, wf.expiredate Vencimiento, wf.user_asigned, DOC_ID, DOC_TYPE_ID  from wfdocument wf  inner join wfstep s on s.step_id = wf.step_id left join zuser_or_group g on g.id = wf.user_asigned where user_asigned = {0} or user_asigned in (select groupid from usr_r_group r where r.USRID = {0})   order by checkin  desc ) q", userId))
        End If
    End Function
    Public Function GetMyTasksCount(userId As Int64) As Int64
        If Server.isSQLServer Then
            Return Server.Con.ExecuteScalar(CommandType.Text, String.Format("select count(1)   from wfdocument wf  with (nolock)   where user_asigned = {0} or user_asigned in (select groupid from usr_r_group r  with (nolock) where r.USRID = {0})   ", userId))
        Else
            Return Server.Con.ExecuteScalar(CommandType.Text, String.Format("select count(1)   from wfdocument wf                  where user_asigned = {0} or user_asigned in (select groupid from usr_r_group r where r.USRID = {0})  ", userId))
        End If
    End Function

    Public Function GetRecentTasks(userId As Int64) As DataSet
        If Server.isSQLServer Then
            Return Server.Con.ExecuteDataset(CommandType.Text, String.Format("select  wf.Task_id, q.doc_id, wf.DOC_TYPE_ID,wf.name Tarea,Fecha ,s.name Etapa, isnull(g.name,'') Asignado, wf.checkin Ingreso, wf.expiredate Vencimiento from (select  top(20) object_id doc_Id, MAX(ACTION_DATE) Fecha  from USER_HST u  with (nolock)  where USER_ID = {0} and ACTION_TYPE in (1,71) and OBJECT_TYPE_ID = 6  group by object_id order by FECHA desc ) q left join wfdocument wf  with (nolock) on wf.doc_id = q.doc_id left join wfstep s  with (nolock) on s.step_id = wf.step_id left join zuser_or_group g  with (nolock) on g.id = wf.user_asigned", userId))
        Else
            Return Server.Con.ExecuteDataset(CommandType.Text, String.Format("select  wf.Task_id, wf.doc_id, wf.DOC_TYPE_ID,wf.name Tarea,Fecha ,s.name Etapa, isnull(g.name,'') Asignado, wf.checkin Ingreso, wf.expiredate Vencimiento from (select   object_id doc_Id, MAX(ACTION_DATE) Fecha  from USER_HST u    where USER_ID = {0} and ACTION_TYPE in (1,71) and OBJECT_TYPE_ID = 6  group by object_id order by FECHA desc ) q left join wfdocument wf   on wf.doc_id = q.doc_id left join wfstep s   on s.step_id = wf.step_id left join zuser_or_group g   on g.id = wf.user_asigned where rownum < 20", userId))
        End If
    End Function

    Public Function GetRecentTasksCount(userId As Int64) As Int64
        If Server.isSQLServer Then
            Return Server.Con.ExecuteScalar(CommandType.Text, String.Format("select  count(1) from (select  top(20) object_id doc_Id, MAX(ACTION_DATE) Fecha  from USER_HST u  with (nolock)  where USER_ID = {0} and ACTION_TYPE in (1,71) and OBJECT_TYPE_ID = 6  group by object_id order by FECHA desc ) q left join wfdocument wf  with (nolock) on wf.doc_id = q.doc_id left join wfstep s  with (nolock) on s.step_id = wf.step_id left join zuser_or_group g  with (nolock) on g.id = wf.user_asigned", userId))
        Else
            Return Server.Con.ExecuteScalar(CommandType.Text, String.Format("select  count(1) from (select   object_id doc_Id, MAX(ACTION_DATE) Fecha  from USER_HST u    where USER_ID = {0} and ACTION_TYPE in (1,71) and OBJECT_TYPE_ID = 6  group by object_id order by FECHA desc ) q left join wfdocument wf   on wf.doc_id = q.doc_id left join wfstep s   on s.step_id = wf.step_id left join zuser_or_group g   on g.id = wf.user_asigned where rownum < 20", userId))
        End If
    End Function


    Public Function GetAsignedTasksCountsGroupByUser(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_AsignedTasksCountsGroupByUser", parvalues)
    End Function

    Public Function GetAsignedTasksCountsGroupByStep(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_AsignedTasksCountsGroupByStep", parvalues)
    End Function

    Public Function GetTaskConsumedMinutesByWorkflowGroupByUsers(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_Sarasa", parvalues)
    End Function

    Public Function GetTaskConsumedMinutesByStepGroupByUsers(ByVal stepid As Int64) As DataSet
        Dim parvalues() As Object = {stepid}
        Return Server.Con.ExecuteDataset("sp_Sarasa", parvalues)
    End Function

#End Region

    Public Function GetTaskHistoryByResultId(ByVal task_id As Integer) As DataSet
        Dim Query As String = "SELECT  Fecha, Step_Name Etapa, State Estado, UserName Usuario, Accion, Doc_Name Tarea, Doc_Type_Name Entidad FROM WFStepHst WHERE Doc_Id = " & task_id.ToString() & " order by Fecha desc"
        Return Server.Con.ExecuteDataset(CommandType.Text, Query)
    End Function


    Public Function GetOnlyIndexsHistory(ByVal DocID As Integer) As DataSet
        If Server.isOracle Then
            Dim Query As String = "Select  Action_Date As " & Chr(34) & "Fecha" & Chr(34) & ", 'Documentos' as " & Chr(34) & "Herramienta" & Chr(34) & ", 'Edición' as " & Chr(34) & "Accion" & Chr(34) & ", USRTABLE.Name AS " & Chr(34) & "Usuario" & Chr(34) & ", S_Object_ID AS " & Chr(34) & "En" & Chr(34) & " FROM User_Hst left outer join USRTABLE on User_Hst.User_ID = USRTABLE.ID WHERE object_id = " & DocID & " and object_type_id = 6 And action_type = 12 order by Action_Date desc"
            Return Server.Con.ExecuteDataset(CommandType.Text, Query)
        Else
            Dim Query As String = "SELECT  Action_Date AS 'Fecha', 'Documentos' as 'Herramienta', 'Edición' as 'Accion', USRTABLE.Name AS 'Usuario', S_Object_ID AS 'En' FROM User_Hst left outer join USRTABLE on User_Hst.User_ID = USRTABLE.ID WHERE object_id = " & DocID & " and object_type_id = 6 And action_type = 12 order by Action_Date desc"
            Return Server.Con.ExecuteDataset(CommandType.Text, Query)
        End If
    End Function

    ''' <summary>
    ''' Devuelva la ultima fecha de modificacion de la tarea
    ''' </summary>
    ''' <param name="TaskId">Id de la tarea</param>
    ''' <history>Marcelo created 26/02/09</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLastModifiedTaskHistoryByResultId(ByVal TaskId As Integer) As DateTime
        Dim lastModDate As Nullable(Of DateTime)
        If Server.isOracle Then
            Dim Query As String = "SELECT  max(Fecha) AS " & Chr(34) & "Fecha" & Chr(34) & " FROM(WFStepHst) WHERE Doc_Id = " & TaskId.ToString()
            lastModDate = Server.Con.ExecuteScalar(CommandType.Text, Query)
        Else
            Dim Query As String = "SELECT max(Fecha) as Fecha FROM Zvw_WFHistory_200 Where DOC_ID = " & TaskId.ToString()
            lastModDate = Server.Con.ExecuteScalar(CommandType.Text, Query)
        End If
        If lastModDate.HasValue = False Then
            Return New DateTime(1987, 5, 20)
        Else
            Return lastModDate.Value
        End If
    End Function
    ''' <summary>
    ''' Get TaskIds of the Document
    ''' </summary>
    ''' <param name="docId">Document ID</param>
    ''' <returns></returns>
    ''' <history>Marcelo Modified 30/11/09</history>
    ''' <remarks></remarks>
    Public Function GetTaskIdsByDocId(ByVal docId As Int64) As DataSet
        Dim QueryBuilder As New StringBuilder
        Try
            QueryBuilder.Append("select task_id, step_id, doc_type_id, work_id from wfdocument where doc_id = ")
            QueryBuilder.Append(docId.ToString())

            Return Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
        Finally
            QueryBuilder.Remove(0, QueryBuilder.Length)
            QueryBuilder = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Get TaskIds of the Document
    ''' </summary>
    ''' <param name="docId">Document ID</param>
    ''' <returns></returns>
    ''' <history>Marcelo Modified 30/11/09</history>
    ''' <remarks></remarks>
    Public Function GetTaskIdByDocIdAndDocTypeId(ByVal docId As Int64, ByVal docTypeId As Long) As DataSet
        Dim QueryBuilder As New StringBuilder
        Try
            QueryBuilder.Append("select task_id, step_id from wfdocument where doc_id = ")
            QueryBuilder.Append(docId)
            QueryBuilder.Append(" and doc_type_id = ")
            QueryBuilder.Append(docTypeId)

            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())

            Return ds
        Finally
            QueryBuilder.Remove(0, QueryBuilder.Length)
            QueryBuilder = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el id y nombre de la tarea por su docid
    ''' </summary>
    ''' <param name="docId">Document ID</param>
    ''' <returns>Devuelve la primer tarea encontrada por el docid</returns>
    ''' <remarks></remarks>
    Public Function GetTaskIdAndNameByDocId(ByVal docId As Int64) As DataTable
        If Server.isOracle Then
            Dim params As Object() = {docId, 2}
            ' Dim parTypes() As Object = {OracleType.Number, OracleType.Cursor}
            'Dim parNames As String() = {"DOCID", "io_cursor"}

            Return Server.Con.ExecuteDataset("ZSP_WORKFLOW_300.GetTaskIdAndNameByDocId", params).Tables(0)
        Else
            Dim params As Object() = {docId}
            Return Server.Con.ExecuteDataset("ZSP_WORKFLOW_100_GetTaskIdAndNameByDocId", params).Tables(0)
        End If
    End Function

    ''' <summary>
    ''' Obtiene el id y nombre de la tarea por su docid
    ''' </summary>
    ''' <param name="docId">Document ID</param>
    ''' <param name="docTypeId">Tipo de documento</param>
    ''' <returns>Devuelve la primer tarea encontrada por el docid</returns>
    ''' <remarks></remarks>
    Public Function GetTaskViewByDocIdAndDocTypeId(ByVal docId As Int64, ByVal docTypeId As Long) As DataTable
        If Server.isOracle Then
            Dim params As Object() = {docId, docTypeId, 2}
            ' Dim parTypes() As Object = {OracleType.Number, OracleType.Number, OracleType.Cursor}
            'Dim parNames As String() = {"DOCID", "docTypeId", "io_cursor"}

            Return Server.Con.ExecuteDataset("ZSP_WORKFLOW_500.GetTaskViewByDocIdAndDocTypeId", params).Tables(0)
        Else
            Dim params As Object() = {docId, docTypeId}
            Return Server.Con.ExecuteDataset("ZSP_WORKFLOW_100_GetTaskViewByDocIdAndDocTypeId", params).Tables(0)
        End If
    End Function



    ''' <summary>
    ''' Método que sirve para obtener un dataset que tendrá el historial de los task id seleccionados. Sobre los 
    ''' task id seleccionados se debio haber ejecutado la regla RequestAction. Los task id que no ejecutaron la 
    ''' regla no se mostrarán
    ''' </summary>
    ''' <param name="taskIds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>   
    '''     [Gaston]    22/07/2008  Created
    '''                 30/07/2008  Modified
    '''                 26/09/2008  Correción de consulta Oracle
    ''' </history>
    Public Function getTasksRequestActionHistory(ByVal taskId As Int64) As DataSet

        Dim strBuilder As New StringBuilder
        Dim counter As Integer = 0

        ' Si el servidor es SQL
        If (Server.isSQLServer) Then

            strBuilder.Append("SELECT * FROM ZView_RequestAction Where ")
            strBuilder.Append("TaskId = " & taskId)

            ' Sino, si el servidor es Oracle
        Else

            strBuilder.Append("SELECT WFDocument.Name AS Tarea, RequestAction.RequestName AS Solicitud, RequestAction.RequestDate AS " & Chr(34) & "Fecha de solicitud" & Chr(34) & ", ")
            strBuilder.Append("USRTABLE.NAME AS Usuario, RequestActionExecution.ExecutionDate AS " & Chr(34) & "Fecha de realización" & Chr(34) & ", WFRules.Name AS " & Chr(34) & "Acción realizada" & Chr(34) & ", ")
            strBuilder.Append("RequestActionTasks.TaskId ")
            strBuilder.Append("FROM WFRules INNER JOIN ")
            strBuilder.Append("RequestActionExecution ON WFRules.Id = RequestActionExecution.RuleId RIGHT OUTER JOIN ")
            strBuilder.Append("RequestActionTasks INNER JOIN ")
            strBuilder.Append("RequestAction INNER JOIN ")
            strBuilder.Append("RequestActionUsers ON RequestAction.Id = RequestActionUsers.RequestActionId INNER JOIN ")
            strBuilder.Append("USRTABLE ON RequestActionUsers.UserId = USRTABLE.ID ON RequestActionTasks.RequestActionId = RequestAction.Id INNER JOIN ")
            strBuilder.Append("WFDocument ON RequestActionTasks.TaskId = WFDocument.Task_ID ON RequestActionExecution.RequestActionId = RequestAction.Id AND ")
            strBuilder.Append("RequestActionExecution.UserId = RequestActionUsers.UserId ")
            strBuilder.Append("Where RequestActionTasks.taskid = " & taskId)

        End If

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strBuilder.ToString())
        ds.Tables(0).Columns.Remove(ds.Tables(0).Columns("TaskId"))
        Return (ds)

    End Function


    Public Function setCExclusive(ByVal taskId As Int64, ByVal value As Int32) As Int16
        Dim QueryBuilder As String
        Try
            QueryBuilder = String.Format("UPDATE WFDOCUMENT SET C_EXCLUSIVE={0} WHERE TASK_ID = {1}", value, taskId)

            If Server.isOracle = False Then
                QueryBuilder = QueryBuilder.Replace("C_EXCLUSIVE", "EXCLUSIVE")
            End If
            Return Server.Con.ExecuteScalar(CommandType.Text, QueryBuilder)
        Finally
            QueryBuilder = Nothing
        End Try
    End Function


#Region "Save"
    Public Sub SaveIntoIP_Task(ByVal alFiles As String, ByVal IPTASKID As Int64, ByVal ZipOrigen As String, ByVal conf_Id As Decimal)
        Dim strinsert As String = "INSERT INTO IP_Task (Id,Id_Configuracion, File_Path, Zip_Origen) VALUES(" _
        & IPTASKID & "," & conf_Id & ", '" & alFiles & "', '" & ZipOrigen & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    End Sub

    Public Sub SaveIntoIP_Task(ByVal alFiles As String, ByVal IPTASKID As Int64, ByVal conf_Id As Decimal)
        Dim strinsert As String = "INSERT INTO IP_Task (Id,Id_Configuracion, File_Path, Zip_Origen,Bloqueado,Maquina) VALUES(" _
        & IPTASKID & "," & conf_Id & ", '" & alFiles & "', '', 0, '" & Environment.MachineName & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    End Sub
#End Region
    Public Overrides Sub Dispose()

    End Sub

End Class
