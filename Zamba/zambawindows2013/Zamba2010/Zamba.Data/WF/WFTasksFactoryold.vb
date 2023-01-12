Imports Zamba.Core
Imports System.Text
Imports System.Collections.Generic


Public Class WFTasksFactory
    Inherits ZClass

    Public Shared Property doc_type_name As Object

#Region "Get"

    <Obsolete("Metodo discontinuado", False)>
    Public Shared Function GetUserTasks(ByVal WFs() As WorkFlow, ByVal VerAsignadosAOtros As Boolean, ByVal VerAsignadosANadie As Boolean, ByVal CurrentUser As IUser) As DsDocuments
        'Diego, parece que se hicieron cambios al metodo y no se probaron, estaba funcionando mal asi que volvi
        ' a la forma original el metodo, Este probado y funcionando
        Dim DsDoc As New DsDocuments
        'Dim DsDoc As New DataSet
        Try
            Dim strBuilder As New StringBuilder
            strBuilder.Append("select * from wfdocument WHERE")

            For Each wf As WorkFlow In WFs
                For Each s As WFStep In wf.Steps.Values

                    strBuilder.Append(" (step_id = ")
                    strBuilder.Append(s.ID.ToString())


                    ' ------------------------------------------------------------------------------------------
                    'Dim EjecutarTareasDeOtrosUsuarios as boolean = 
                    ' ------------------------------------------------------------------------------------------


                    If Not VerAsignadosAOtros AndAlso VerAsignadosANadie Then
                        strBuilder.Append(" and (user_asigned = " & CurrentUser.ID & " or user_asigned = 0)")
                    ElseIf VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
                        strBuilder.Append(" and user_asigned <> 0")
                    ElseIf Not VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
                        strBuilder.Append(" and user_asigned = " & CurrentUser.ID)
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
            raiseerror(ex)
        End Try
        Return DsDoc
    End Function

    <Obsolete("Metodo discontinuado", False)>
    Public Shared Function GetTasks(ByVal WF As WorkFlow) As DataSet
        'Dim DsDoc As New DsDocuments
        Dim DsTemp As DataSet

        Dim StrSelect As String = "select * from wfdocument WHERE"
        For Each s As WFStep In WF.Steps.Values
            StrSelect += " step_id = " & s.ID
            StrSelect += " or"
        Next
        'borro el ultimo 'or' de mas
        StrSelect = StrSelect.Substring(0, StrSelect.Length - 2)
        DsTemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        'DsTemp.Tables(0).TableName = DsDoc.Documents.TableName
        'DsDoc.Merge(DSTEMP)
        Return DsTemp
    End Function

    Public Shared Function GetDocId(ByVal taskId As Int64) As Int64

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

    Public Shared Function GetTasksByWfId(ByVal wfId As Int64) As DataSet
        Dim strBuilder As New StringBuilder()

        strBuilder.Append("SELECT Doc_ID, DOC_TYPE_ID,  step_Id, Do_State_ID, Name, IconId, CheckIn, ")
        strBuilder.Append("User_Asigned, Exclusive, ExpireDate, User_Asigned_By, Date_Asigned_By, Task_ID, Task_State_ID, ")
        strBuilder.Append("Remark, Tag, work_id ")
        strBuilder.Append("FROM WFDocument WHERE step_id in (SELECT step_id FROM wfstep WHERE work_id = ")
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
        StrBuilder.Append("WHERE step_id = ")
        StrBuilder.Append(stepId.ToString())
        StrBuilder.Append(" AND work_id = ")
        StrBuilder.Append(wfId.ToString())

        'EN ORACLE SE RENOMBRO LA COLUMNA EXCLUSIVE A C_EXCLUSIVE
        Dim query As String = StrBuilder.ToString
        If Server.isOracle Then query = query.Replace("Exclusive,", "C_Exclusive,")
        Return Server.Con.ExecuteDataset(CommandType.Text, query)
    End Function

    Public Shared Function GetTaskByTaskId(ByVal taskId As Int64) As DataSet
        Dim DsTemp As New DataSet
        Try
            Dim StrSelect As String = "select step_id,doc_type_id from wfdocument WHERE Task_ID = " & taskId
            DsTemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Catch ex As Exception
            raiseerror(ex)
        End Try
        Return DsTemp
    End Function
    ''' <summary>
    ''' </summary>
    ''' <param name="taskId"></param>
    ''' <history>(pablo) Created</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTaskByDocId(ByVal DocId As Int64) As DataSet
        Dim DsTemp As New DataSet
        Try
            Dim StrSelect As String = "select task_id, step_id,doc_type_id from wfdocument WHERE Doc_ID = " & DocId
            DsTemp = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        Catch ex As Exception
            raiseerror(ex)
        End Try
        Return DsTemp
    End Function


    Public Shared Function GetTasksNamesByTaskIds(ByVal taskIds As List(Of Int64)) As DataTable
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
    Public Shared Function GetTaskStateByTaskId(ByVal taskId As Int64) As Int64
        Dim StrSelect As String = "select do_state_id from wfdocument WHERE Task_ID = " & taskId
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
    Public Shared Function GetTaskIdsStepsIdsDocTypesIdsByDocId(ByVal docId As Int64) As DataSet
        Dim QueryBuilder As New StringBuilder
        Try
            QueryBuilder.Append("select task_id,step_id,doc_type_id from wfdocument WHERE doc_id = ")
            QueryBuilder.Append(docId.ToString())

            Return Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
        Finally
            QueryBuilder.Remove(0, QueryBuilder.Length)
            QueryBuilder = Nothing
        End Try
    End Function
    Public Shared Function GetTaskIdByDocIdAndStepId(ByVal docId As Int64, ByVal stepId As Int64) As Int64

        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("select task_id from wfdocument WHERE doc_id = ")
        QueryBuilder.Append(docId.ToString())
        QueryBuilder.Append(" and ")
        QueryBuilder.Append("step_id = ")
        QueryBuilder.Append(stepId.ToString())


        Dim Value As Object = Server.Con.ExecuteScalar(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing

        If IsNothing(Value) Then
            Return -1
        Else
            Return Convert.ToInt64(Value)
        End If



    End Function

    Public Shared Function GetTaskIdByDocIdAndWorkId(ByVal docId As Int64, ByVal workId As Int64) As Int64

        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("select task_id from wfdocument WHERE doc_id=")
        QueryBuilder.Append(docId.ToString())
        QueryBuilder.Append(" and work_id=")
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


    ''' <summary>
    ''' Devuelve todos los taskIds de la tarea (por si estan en varios WF)
    ''' </summary>
    ''' <param name="docId">Id del documento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTaskIdsByDocId(ByVal docId As Int64) As DataSet
        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("select task_id from wfdocument WHERE doc_id = ")
        QueryBuilder.Append(docId.ToString())

        Return Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
    End Function


    ''' <summary>
    ''' Método que obtiene las etapas y IDs de tarea de un documento
    ''' </summary>
    ''' <param name="docId">ID del documento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''' <history>   
    ''''     [Marcelo]    13/10/2011  Created
    '''' </history>
    Public Shared Function GetTaskAndStepIdsByDocId(ByVal docId As Int64) As DataSet
        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("select task_id,step_id from wfdocument WHERE doc_id = ")
        QueryBuilder.Append(docId.ToString())

        Return Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
    End Function

    Public Shared Function GetTasksByTask(ByVal result As ITaskResult) As DataSet

        Dim QueryBuilder As New StringBuilder

        QueryBuilder.Append("select Doc_ID,DOC_TYPE_ID,step_Id,Do_State_ID,Name,IconId,CheckIn,User_Asigned,Exclusive,ExpireDate,User_Asigned_By,Date_Asigned_By,Task_ID,Task_State_ID,Remark,Tag,work_id,LastUpdateDate,Comments from wfdocument WHERE doc_id = ")
        QueryBuilder.Append(result.ID.ToString())
        QueryBuilder.Append(" and DOC_TYPE_ID = ")
        QueryBuilder.Append(result.DocTypeId.ToString())
        QueryBuilder.Append(" and Task_ID <> ")
        QueryBuilder.Append(result.TaskId.ToString())

        Dim Value As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())

        QueryBuilder = Nothing

        Return Value
    End Function


    Public Shared Function GetTasks(ByVal taskIds As List(Of Int64)) As DataSet
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

    Public Shared Function GetTasks(ByVal stepId As Int64) As DataTable
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



    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub CompleteTask(ByRef Result As TaskResult)
        Dim DocTable As String = "DOC_T" & Result.DocTypeId
        Dim StrSelect As String = "SELECT " & DocTable & ".ICON_ID," & DocTable & ".VOL_ID," & DocTable & ".DOC_FILE," & DocTable & ".OFFSET,DISK_VOLUME.DISK_VOL_PATH,original_filename as " & Chr(34) & GridColumns.ORIGINAL_FILENAME_COLUMNNAME & Chr(34) & ",rootid,ver_parent_id,version as " & Chr(34) & GridColumns.VERSION_COLUMNNAME & Chr(34) & ",numeroversion as " & Chr(34) & GridColumns.NUMERO_DE_VERSION_COLUMNNAME & Chr(34) & ",DOC_TYPE.DOC_TYPE_NAME FROM DISK_VOLUME, " & DocTable & ",DOC_TYPE WHERE DISK_VOLUME.DISK_VOL_ID = " & DocTable & ".Disk_Group_Id AND DOC_ID = " & Result.ID & " AND DOC_TYPE.DOC_TYPE_ID= " & Result.DocTypeId
        Dim DsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)

        If Not DsTemp.Tables(0).Rows.Count = 0 Then
            Result.Doc_File = DsTemp.Tables(0).Rows(0).Item("DOC_FILE")
            Result.File = DsTemp.Tables(0).Rows(0).Item("DOC_FILE")
            Result.DISK_VOL_PATH = DsTemp.Tables(0).Rows(0).Item("DISK_VOL_PATH")
            Result.OffSet = DsTemp.Tables(0).Rows(0).Item("OFFSET")

            Result.Disk_Group_Id = DsTemp.Tables(0).Rows(0).Item("VOL_ID")
            Result.OriginalName = DsTemp.Tables(0).Rows(0).Item(GridColumns.ORIGINAL_FILENAME_COLUMNNAME)
            Result.RootDocumentId = DsTemp.Tables(0).Rows(0).Item("ROOTID")
            Result.ParentVerId = DsTemp.Tables(0).Rows(0).Item("VER_PARENT_ID")
            Result.HasVersion = DsTemp.Tables(0).Rows(0).Item(GridColumns.VERSION_COLUMNNAME)
            Result.VersionNumber = DsTemp.Tables(0).Rows(0).Item(GridColumns.NUMERO_DE_VERSION_COLUMNNAME)
            '  Result.DocTypeName = DsTemp.Tables(0).Rows(0).Item("DOC_TYPE_NAME")
        End If
    End Sub

    <Obsolete("Metodo discontinuado", False)>
    Public Shared Function GetTasksBySteps(ByVal stepIds As List(Of Int64)) As DataTable
        If (IsNothing(stepIds) OrElse stepIds.Count = 0) Then
            Return Nothing
        Else
            Dim QueryBuilder As New StringBuilder()
            QueryBuilder.Append("SELECT * FROM wfdocument left Join wfstepstates ")
            QueryBuilder.Append("ON wfdocument.do_state_id = wfstepstates.doc_state_id WHERE ")

            For Each CurrentStep As Int32 In stepIds
                QueryBuilder.Append(" wfdocument.step_id = ")
                QueryBuilder.Append(CurrentStep.ToString)
                QueryBuilder.Append(" OR ")
            Next

            QueryBuilder.Remove(QueryBuilder.Length - 4, 4)

            'EN ORACLE SE RENOMBRO LA COLUMNA EXCLUSIVE A C_EXCLUSIVE
            Dim query As String = QueryBuilder.ToString
            If Server.isOracle Then query = query.Replace("Exclusive,", "C_Exclusive,")

            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query)

            If ds.Tables.Count = 1 Then
                Return ds.Tables(0)
            End If

            Return Nothing
        End If


    End Function

    <Obsolete("Metodo discontinuado", False)>
    Public Shared Function GetTasksByStep(ByVal stepId As Int64) As DataTable

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
    ''' Devuelve las tareas de la entidad y etapa que se le pasan por parametro
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
                                               ByVal docTypeId As Int64,
                                               ByVal indexs As List(Of IIndex),
                                               ByVal WithRights As Boolean,
                                               ByVal FilterString As String,
                                               ByVal RestrictionString As String,
                                               ByVal dateDeclarationString As String,
                                               ByVal LastPage As Int64,
                                               ByVal PageSize As Int32,
                                               ByVal CheckInColumnIsShortDate As Boolean,
                                               ByVal auIndex As List(Of IIndex),
                                               ByVal searchType As SearchType,
                                               ByVal VerAsignadosAOtros As Boolean,
                                               ByVal VerAsignadosANadie As Boolean,
                                               ByVal FlagAsRead As Boolean,
                                               ByVal wfstateID As Int64,
                                               ByVal order As String,
                                               ByVal CurrentUser As IUser,
                                               ByVal orderType As String, ByVal GroupByString As String, ByRef GroupByCount As Hashtable,
                                               ByVal slstFiltersIndexsID As List(Of Long)) As DataTable

        Dim strTableI As String = Results_Factory.MakeTable(docTypeId, Results_Factory.TableType.Indexs)
        Dim strTableT As String = Results_Factory.MakeTable(docTypeId, Results_Factory.TableType.Document)

        Dim MainJoin As String = String.Format("{0} T inner join {1} I on T.doc_id = I.doc_id", strTableT, strTableI)
        Dim dt As DataTable


        'Si hay ordenamiento lo formateo apropiadamente, sino va por task_id.

        If Not String.IsNullOrEmpty(order) Then
            If order.Contains("strTableT") Then order = order.Replace("strTableT", strTableT)
            If order.Contains("strTableI") Then order = order.Replace("strTableI", strTableI)
            order = order.Replace("CHECKIN", """Fecha de ingreso""")
            order = order.Replace("EXPIREDATE", """Vencimiento Tarea""")
            order = order.Replace("USER_ASIGNED_BY", """Usuario asignado por""")
            order = order.Replace("DATE_ASIGNED_BY", """Fecha asignada por""")
            order = order.Replace("NAME", """Tarea""")

        Else
            order = "task_id asc"
        End If

        Dim strPREselect As New StringBuilder
        Dim strselect As New StringBuilder
        Dim strPOSTselect As New StringBuilder

        GetCommonTaskStringBuilder(docTypeId, WithRights, indexs, auIndex, stepId, 0, TaskQueryMode.WFStep, RestrictionString, dateDeclarationString, CheckInColumnIsShortDate, FlagAsRead, wfstateID, searchType, Membership.MembershipHelper.CurrentUser.ID, order, slstFiltersIndexsID, strPREselect, strselect, strPOSTselect)

        '[Ezequiel] Armo el where en base a los permisos
        If WithRights Then

            If Not VerAsignadosAOtros AndAlso VerAsignadosANadie Then
                strselect.Append(" and (user_asigned = " & CurrentUser.ID & " or user_asigned = 0")
                For i As Int32 = 0 To CurrentUser.Groups.Count - 1
                    strselect.Append(" or user_asigned = ")
                    strselect.Append(CurrentUser.Groups(i).ID())
                Next
                strselect.Append(")")
            ElseIf VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
                strselect.Append(" and user_asigned <> 0")
            ElseIf Not VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
                strselect.Append(" and (user_asigned = " & CurrentUser.ID)
                For i As Int32 = 0 To CurrentUser.Groups.Count - 1
                    strselect.Append(" or user_asigned = ")
                    strselect.Append(CurrentUser.Groups(i).ID())
                Next
                strselect.Append(")")
            End If
        End If

        If Not String.IsNullOrEmpty(FilterString) AndAlso FilterString.Trim() <> String.Empty Then

            If FilterString.ToLower.Trim.StartsWith("and") = False Then

                If (strselect.ToString.ToLower.Contains("where")) Then
                    strselect.Append(" and ")
                Else
                    strselect.Append(" where ")
                End If

            End If

            FilterString = FilterString.Replace("strTableT", strTableT)
            FilterString = FilterString.Replace("strTableI", strTableI)


            If Server.isOracle Then
                FilterString = FilterString.Replace("[", "")
                FilterString = FilterString.Replace("]", "")
                FilterString = FilterString.Replace("WFDOCUMENT.CHECKIN", """Fecha de ingreso""")
                FilterString = FilterString.Replace("WFDOCUMENT.EXPIREDATE", """Vencimiento Tarea""")
                FilterString = FilterString.Replace("WFDOCUMENT.USER_ASIGNED_BY", """Usuario asignado por""")
                FilterString = FilterString.Replace("WFDOCUMENT.DATE_ASIGNED_BY", """Fecha asignada por""")
                FilterString = FilterString.Replace("WFDOCUMENT.NAME", """Tarea""")
            End If

            strselect.Append(FilterString)
        End If


        'Verifica si solo debe obtener la cantidad de tareas
        If searchType = searchType.WFStepCount OrElse searchType = searchType.GridResultsCount Then
            Try
                dt = Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString).Tables(0)
            Catch ex As Threading.ThreadAbortException
            Catch ex As NullReferenceException
                raiseerror(New Exception("Error en consulta " & strselect.ToString(), ex))
                'Se atrapa el error y no se procesa ya que se genera cuando se desea abrir el 
                'panel de tareas y rapidamente se presiona multiples veces el boton cerrar.
            Catch ex As Exception
                raiseerror(ex)
            End Try
        Else
            If dateDeclarationString.Length > 0 Then
                'Si existe declaracion de variables de fecha las inserta al inicio del select
                strselect.Insert(0, dateDeclarationString)
            End If

            dt = GetTaskTable(strselect, strPREselect, strPOSTselect, order, PageSize, LastPage, False, searchType.GridResults, orderType, GroupByString, GroupByCount)
        End If


        Return dt

    End Function



    Public Function GetTasksByWFandDocTypeId(workflowid As Int64,
                                             ByVal docid As Int64,
                                             ByVal indexs As List(Of IIndex),
                                             ByVal WithRights As Boolean,
                                             ByVal FilterString As String,
                                             ByVal RestrictionString As String,
                                             ByVal dateDeclarationString As String,
                                             ByVal LastPage As Int64,
                                             ByVal PageSize As Int32,
                                             ByVal CheckInColumnIsShortDate As Boolean,
                                             ByVal auIndex As List(Of IIndex),
                                             ByVal searchType As SearchType,
                                             ByVal VerAsignadosAOtros As Boolean,
                                             ByVal VerAsignadosANadie As Boolean,
                                             ByVal FlagAsRead As Boolean,
                                             ByVal wfstateID As Int64,
                                             ByVal CurrentUser As IUser, ByRef GroupByCount As Hashtable, slstFiltersIndexsID As List(Of Long)) As DataTable

        Dim strPREselect As New StringBuilder
        Dim strselect As New StringBuilder
        Dim strPOSTselect As New StringBuilder

        GetCommonTaskStringBuilder(docid,
                                                                    WithRights,
                                                                    indexs,
                                                                    auIndex,
                                                                    0,
                                                                    workflowid,
                                                                    TaskQueryMode.Workflow,
                                                                    RestrictionString,
                                                                    dateDeclarationString,
                                                                    CheckInColumnIsShortDate,
                                                                    FlagAsRead,
                                                                    wfstateID,
                                                                    searchType,
                                                                    Membership.MembershipHelper.CurrentUser.ID,
                                                                    String.Empty, slstFiltersIndexsID, strPREselect, strselect, strPOSTselect)

        '[Ezequiel] Armo el where en base a los permisos
        If WithRights Then

            If Not VerAsignadosAOtros AndAlso VerAsignadosANadie Then
                strselect.Append(" and (user_asigned = " & CurrentUser.ID & " or user_asigned = 0")
                For i As Int32 = 0 To CurrentUser.Groups.Count - 1
                    strselect.Append(" or user_asigned = ")
                    strselect.Append(CurrentUser.Groups(i).ID())
                Next
                strselect.Append(")")
            ElseIf VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
                strselect.Append(" and user_asigned <> 0")
            ElseIf Not VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
                strselect.Append(" and (user_asigned = " & CurrentUser.ID)
                For i As Int32 = 0 To CurrentUser.Groups.Count - 1
                    strselect.Append(" or user_asigned = ")
                    strselect.Append(CurrentUser.Groups(i).ID())
                Next
                strselect.Append(")")
            End If
        End If


        If Not String.IsNullOrEmpty(FilterString) AndAlso FilterString.Trim() <> String.Empty Then
            If FilterString.ToLower.Trim.StartsWith("and") = False Then
                strselect.Append(" and ")
            End If

            strselect.Append(FilterString)
        End If

        'Verifica si solo debe obtener la cantidad de tareas
        If searchType = searchType.WFStepCount Then
            Try
                Return Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString).Tables(0)
            Catch ex As NullReferenceException
                'Se atrapa el error y no se procesa ya que se genera cuando se desea abrir el 
                'panel de tareas y rapidamente se presiona multiples veces el boton cerrar.
            End Try
        Else
            If dateDeclarationString.Length > 0 Then
                'Si existe declaracion de variables de fecha las inserta al inicio del select
                strselect.Insert(0, dateDeclarationString)
            End If
            Return GetTaskTable(strselect, strPREselect, strPOSTselect, "task_id asc", PageSize, LastPage, False, searchType.GridResults, Nothing, String.Empty, GroupByCount)
        End If

    End Function

    ''' <summary>
    ''' Devuelve las tareas de la entidad y etapa que se le pasan por parametro
    ''' </summary>
    ''' <param name="stepId">Id de la etapa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''        [Ezequiel] 14/09/09  Created.
    '''        [Javier]   06/10/10  Modified.   Se agrega string de restricciones y string de declaracion de variables de fechas
    '''        [Javier]   12/10/10  Modified    Se cambia parametros a la llamada de GetCommonTaskStringBuilder
    ''' </history>
    Public Shared Function GetStepsTasksCountByStepsandDocTypeId(ByVal doctypeid As Int64, ByVal stepIds As List(Of Int64), ByVal RestrictionString As String, ByVal dateDeclarationString As String, ByVal FlagAsRead As Boolean) As DataTable


        Dim strselect As StringBuilder = GetCommonStepsTaskCountStringBuilder(doctypeid, stepIds, RestrictionString, dateDeclarationString, FlagAsRead)


        '        Dim VerAsignadosAOtros As Boolean = RightFactory.GetUserRights(ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosAOtros, stepId)
        '       Dim VerAsignadosANadie As Boolean = RightFactory.GetUserRights(ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosANadie, stepId)

        'If Not VerAsignadosAOtros AndAlso VerAsignadosANadie Then
        '    strselect.Append(" and (user_asigned = " & RightFactory.CurrentUser.ID & " or user_asigned = 0")
        '    For i As Int32 = 0 To RightFactory.CurrentUser.Groups.Count - 1
        '        strselect.Append(" or user_asigned = ")
        '        strselect.Append(RightFactory.CurrentUser.Groups(i).ID())
        '    Next
        '    strselect.Append(")")
        'ElseIf VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
        '    strselect.Append(" and user_asigned <> 0")
        'ElseIf Not VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
        '    strselect.Append(" and (user_asigned = " & RightFactory.CurrentUser.ID)
        '    For i As Int32 = 0 To RightFactory.CurrentUser.Groups.Count - 1
        '        strselect.Append(" or user_asigned = ")
        '        strselect.Append(RightFactory.CurrentUser.Groups(i).ID())
        '    Next
        '    strselect.Append(")")
        'End If


        Return Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString).Tables(0)

    End Function

    ''' <summary>
    ''' Devuelve las tareas de la entidad y etapa que se le pasan por parametro
    ''' </summary>
    ''' <param name="stepId">Id de la etapa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''        [Ezequiel] 14/09/09 - Created.
    '''        [Javier]   12/10/10  Modified    Se agregan parámetros Restriction y DateDeclaration
    ''' </history>
    Public Function GetTasksByTasksIdsAndDocTypeId(ByVal tasksids As List(Of Int64), ByVal stepId As Int64, ByVal docTypeId As Int64, ByVal indexs As List(Of IIndex), ByVal WithGridRights As Boolean, ByVal FilterString As String, ByVal PageSize As Int32, ByVal LastPage As Int64, ByVal RestrictionString As String, ByVal DateDeclarationString As String, ByVal FlagAsRead As Boolean, Optional ByVal auIndex As List(Of IIndex) = Nothing, ByRef Optional GroupByCount As Hashtable = Nothing, Optional slstFiltersIndexsID As List(Of Long) = Nothing) As DataTable


        Dim whereids As StringBuilder
        Dim firsttask As Boolean = True
        Dim strPREselect As New StringBuilder
        Dim strselect As New StringBuilder
        Dim strPOSTselect As New StringBuilder
        GetCommonTaskStringBuilder(docTypeId, WithGridRights, indexs, auIndex, stepId, 0, TaskQueryMode.WFStep, RestrictionString, DateDeclarationString, False, FlagAsRead, 0, SearchType.GridResults, Membership.MembershipHelper.CurrentUser.ID, String.Empty, slstFiltersIndexsID, strPREselect, strselect, strPOSTselect)

        strselect.Append(" and ")
        strselect.Append(FilterString)


        For Each taskid As Int64 In tasksids
            If firsttask Then
                whereids.Append(" and (task_id = ")
                whereids.Append(taskid.ToString)
                firsttask = False
            Else
                whereids.Append(" or task_id = ")
                whereids.Append(taskid.ToString)
            End If
        Next

        whereids.Append(")")
        strselect.Append(whereids.ToString())

        Return GetTaskTable(strselect, strPREselect, strPOSTselect, "task_id asc", PageSize, LastPage, False, SearchType.GridResults, Nothing, String.Empty, GroupByCount)

    End Function

    ''' <summary>
    ''' Devuelve las tareas de la entidad y etapa que se le pasan por parametro
    ''' </summary>
    ''' <param name="stepId">Id de la etapa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''        [Ezequiel] 14/09/09  Created.
    '''        [Javier]   12/10/10  Modified    Se agregan parámetros Restriction y DateDeclaration
    ''' </history>
    Public Function GetTaskByTaskIdAndDocTypeId(ByVal taskid As Int64, ByVal stepId As Int64, ByVal docTypeId As Int64, ByVal indexs As List(Of IIndex), ByVal WithGridRights As Boolean, ByVal FilterString As String, ByVal RestrictionString As String, ByVal DateDeclarationString As String, ByVal FlagAsRead As Boolean, Optional ByVal auIndex As List(Of IIndex) = Nothing, ByRef Optional GroupByCount As Hashtable = Nothing, Optional slstFiltersIndexsID As List(Of Long) = Nothing) As DataTable

        Dim strPREselect As New StringBuilder
        Dim strselect As New StringBuilder
        Dim strPOSTselect As New StringBuilder

        Dim firsttask As Boolean
        Try
            '[Ezequiel] Obtengo el nombre de la vista de la entidad
            GetCommonTaskStringBuilder(docTypeId, WithGridRights, indexs, auIndex, stepId, 0, TaskQueryMode.WFTask, RestrictionString, DateDeclarationString, False, FlagAsRead, 0, SearchType.OpenTask, Membership.MembershipHelper.CurrentUser.ID, String.Empty, slstFiltersIndexsID, strPREselect, strselect, strPOSTselect)

            firsttask = True
            If (String.IsNullOrEmpty(FilterString) = False) Then
                strselect.Append(" and ")
                strselect.Append(FilterString)
            End If
            If strselect.ToString().IndexOf("where", StringComparison.InvariantCultureIgnoreCase) >= 0 Then
                strselect.Append(" and (task_id = ")
            Else
                strselect.Append(" where (task_id = ")
            End If
            strselect.Append(taskid.ToString)
            strselect.Append(")")

            Return GetTaskTable(strselect, strPREselect, strPOSTselect, "task_id asc", 1, 1, True, SearchType.OpenTask, Nothing, String.Empty, GroupByCount)
        Finally
            strselect = Nothing
            firsttask = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Devuelve las tareas de la entidad y etapa que se le pasan por parametro
    ''' </summary>
    ''' <param name="stepId">Id de la etapa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''        [Marcelo] 16/09/10 - Created.
    '''        [Javier]  12/10/10   Modified    Se agregan parámetros Restriction y DateDeclaration
    ''' </history>
    Public Function GetTasksByTasksIdAndDocTypeId(ByVal taskids As List(Of Int64), ByVal stepId As Int64, ByVal docTypeId As Int64, ByVal indexs As List(Of IIndex), ByVal WithGridRights As Boolean, ByVal FilterString As String, ByVal RestrictionString As String, ByVal DateDeclarationString As String, ByVal FlagAsRead As Boolean, Optional ByVal auIndex As List(Of IIndex) = Nothing, Optional slstFiltersIndexsID As List(Of Long) = Nothing) As DataTable

        Dim strPREselect As New StringBuilder
        Dim strselect As New StringBuilder
        Dim strPOSTselect As New StringBuilder

        GetCommonTaskStringBuilder(docTypeId,
                                                                    WithGridRights,
                                                                    indexs,
                                                                    auIndex,
                                                                    stepId,
                                                                    0,
                                                                    TaskQueryMode.WFTask,
                                                                    RestrictionString,
                                                                    DateDeclarationString,
                                                                    False,
                                                                    FlagAsRead,
                                                                    0,
                                                                    SearchType.OpenTask,
                                                                    Membership.MembershipHelper.CurrentUser.ID,
                                                                    String.Empty, slstFiltersIndexsID, strPREselect, strselect, strPOSTselect)


        Try
            Dim firsttask As Boolean = True
            If String.IsNullOrEmpty(FilterString) Then
                If strselect.ToString().ToLower.IndexOf("where") = -1 Then
                    strselect.Append(" where task_id in (")
                Else
                    strselect.Append(" and task_id in (")
                End If
            Else
                If strselect.ToString().ToLower.IndexOf("where") = -1 AndAlso FilterString.IndexOf("where") = -1 Then
                    'ni el select ni el filtro tienen el where
                    strselect.Append(" where ")
                    strselect.Append(FilterString)
                    strselect.Append(" and task_id in (")
                Else
                    If strselect.ToString().ToLower.IndexOf("where") = -1 Then
                        'el filtro tiene el where
                        strselect.Append(FilterString)
                        strselect.Append(" and task_id in (")
                    Else
                        'el select tiene el where
                        strselect.Append(" and ")
                        strselect.Append(FilterString)
                        strselect.Append(" and task_id in (")
                    End If
                End If
            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try

        For i As Int16 = 0 To taskids.Count - 1
            strselect.Append(taskids(i).ToString)
            strselect.Append(", ")
        Next
        strselect.Remove(strselect.Length - 2, 2)
        strselect.Append(")")


        If Server.isOracle Then
            If Not strselect.ToString().ToLower().Contains("c_exclusive") Then
                strselect = strselect.Replace("Exclusive,", "C_Exclusive,")
            End If
        End If

        Return Server.Con.ExecuteDataset(CommandType.Text, strPREselect.ToString & " from ( " & strselect.ToString & " ) wd " & strPOSTselect.ToString).Tables(0)

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
    Private Shared Function GetTaskTable(strQuery As StringBuilder, strPREQuery As StringBuilder, strPOSTQuery As StringBuilder,
                                         Orden As String,
                                         PageSize As Int32,
                                         LastPage As Int32,
                                         ShowBoolTaskDetails As Boolean,
                                         SearchType As SearchType,
                                         orderType As String, GroupByString As String, ByRef GroupByCount As Hashtable) As DataTable

        Dim Desde As Int32 = (PageSize * LastPage) + 1
        Dim Hasta As Int32 = Desde + PageSize - 1
        Dim ds As DataSet
        Dim pagingString As StringBuilder

        Try


            'se agrego este parametro para que no haga el paginado cuando hago doble click en la grilla de tareas 
            If (ShowBoolTaskDetails) Then
                ds = Server.Con.ExecuteDataset(CommandType.Text, strPREQuery.ToString & " from (" & strQuery.ToString & " ) wd " & strPOSTQuery.ToString)
            Else
                pagingString = New StringBuilder

                'If SearchType <> SearchType.GridResults Then
                '    pagingString.Append("select /*+ FIRST_ROWS */ * FROM (")
                'Else
                '    pagingString.Append("select * FROM (")
                'End If

                pagingString.Append(strPREQuery.ToString & " from (" & strQuery.ToString & " ) wd " & strPOSTQuery.ToString)


                If SearchType <> SearchType.OpenTask Then
                    If Server.isOracle Then
                        pagingString.Append(" Order by ")
                        pagingString.Append(Orden.Trim())
                    End If

                    '----------------------------------------grouping iterator------------------------
                    Dim ListofGroups As New Hashtable
                    Dim dt As DataSet
                    Dim dtGroups As New DataTable
                    dtGroups.MinimumCapacity = 0

                    Dim Count As Int64

                    If (GroupByString IsNot Nothing AndAlso GroupByString.Length > 0) Then
                        Dim DsGroups As DataSet

                        Dim GroupByColumns As String = GroupByString.Replace(" ASC", "").Replace(" DESC", "").Replace(Chr(34), "").Trim()
                        GroupByColumns = "[" & GroupByColumns
                        If GroupByColumns.EndsWith(",") Then
                            GroupByColumns = GroupByColumns.Remove(GroupByColumns.Length - 1).Trim()
                        End If

                        If Server.isOracle Then
                            GroupByColumns = GroupByColumns.Replace(",", Chr(34) & "," & Chr(34))
                            GroupByColumns &= Chr(34)
                        Else
                            GroupByColumns = GroupByColumns.Replace(",", "],[")
                            GroupByColumns = GroupByColumns & "]"
                        End If

                        If Server.isOracle Then
                            GroupByColumns = GroupByColumns.Replace("[", Chr(34)).Replace("]", Chr(34))
                        End If

                        Dim GroupQuery As New StringBuilder
                        GroupQuery.Append("select ")
                        GroupQuery.Append(GroupByColumns)
                        GroupQuery.Append(", count(1) as Cantidad from (")
                        GroupQuery.Append(pagingString.ToString())
                        GroupQuery.Append(") wd ")
                        GroupQuery.Append(" group by ")
                        GroupQuery.Append(GroupByColumns)
                        'GroupQuery.Append(" order by ")
                        'GroupQuery.Append(GroupByString)


                        DsGroups = Server.Con.ExecuteDataset(CommandType.Text, GroupQuery.ToString())


                        If DsGroups IsNot Nothing AndAlso DsGroups.Tables.Count > 0 AndAlso DsGroups.Tables(0).Rows.Count > 0 Then

                            Dim ColumnDescriptor As String
                            For Each column As DataColumn In DsGroups.Tables(0).Columns
                                If column.ColumnName.IndexOf("CANTIDAD", StringComparison.CurrentCultureIgnoreCase) = -1 Then
                                    ColumnDescriptor &= column.ColumnName & "-"
                                End If
                            Next
                            ColumnDescriptor = ColumnDescriptor.Remove(ColumnDescriptor.Length - 1, 1)

                            Dim ListOfGroupValues As New Hashtable
                            Dim GroupByCountList As New Hashtable

                            For Each r As DataRow In DsGroups.Tables(0).Rows

                                Dim ColumnValueDescriptor As String = String.Empty
                                For Each column As DataColumn In r.Table.Columns
                                    If column.ColumnName.IndexOf("CANTIDAD", StringComparison.CurrentCultureIgnoreCase) = -1 Then
                                        ColumnValueDescriptor &= r(column.Ordinal).ToString().Trim() & "-"
                                    End If
                                Next
                                ColumnValueDescriptor = ColumnValueDescriptor.Remove(ColumnValueDescriptor.Length - 1, 1)

                                If Not ListOfGroupValues.ContainsKey(ColumnValueDescriptor) Then ListOfGroupValues.Add(ColumnValueDescriptor, r(r.Table.Columns.Count - 1).ToString().Trim())
                                Count += Int64.Parse(r(r.Table.Columns.Count - 1).ToString().Trim())

                            Next
                            ListofGroups.Add(GroupByColumns, ListOfGroupValues)
                            GroupByCountList.Add(GroupByColumns.Replace("[", "").Replace("]", "").Replace(Chr(34), "").Replace(Chr(34), ""), ListOfGroupValues)
                            GroupByCount = GroupByCountList
                        Else
                            ListofGroups.Add("Default", New Hashtable() From {{"NoGroup", "0"}})
                        End If
                    Else
                        ListofGroups.Add("Default", New Hashtable() From {{"NoGroup", "0"}})
                    End If


                    For Each Group As String In ListofGroups.Keys
                        Dim currentgroup As Hashtable = ListofGroups(Group)
                        For Each value As String In currentgroup.Keys
                            Dim rowscount As Int64 = currentgroup(value)
                            Dim cDesde As Int64 = Desde
                            Dim cHasta As Int64 = Hasta
                            If rowscount > 0 Then
                                cDesde = Math.Round(Desde * (rowscount * 100 / Count) / 100)
                                cHasta = Math.Round(cDesde + (rowscount * 100 / Count) * PageSize)
                            End If

                            If dt IsNot Nothing Then
                                dt.Clear()
                                dt.AcceptChanges()
                            End If

                            Dim GroupQueryPagingString As New StringBuilder

                            If Not String.IsNullOrEmpty(Group) AndAlso Group.Trim() <> String.Empty AndAlso Group <> "Default" And GroupByString IsNot Nothing AndAlso GroupByString.Length > 0 Then

                                GroupQueryPagingString.Append(strPREQuery.ToString & " from (" & strQuery.ToString)

                                GroupQueryPagingString.Append(" ) wd " & strPOSTQuery.ToString)

                                GroupQueryPagingString.Append(" ) wd ")

                                If Not strPOSTQuery.ToString().ToLower().Contains("where") Then
                                    GroupQueryPagingString.Append(" WHERE ")
                                Else
                                    GroupQueryPagingString.Append(" And ")
                                End If

                                GroupQueryPagingString.Append(" rnum >= ")
                                GroupQueryPagingString.Append(cDesde)
                                GroupQueryPagingString.Append(" AND rnum <= ")
                                GroupQueryPagingString.Append(cHasta)

                                Dim ColumnIndex As Int32 = 0
                                For Each ColumnDescriptor As String In Group.Split(Char.Parse(","))
                                    Dim GroupValue As String = value.Split(Char.Parse("-"))(ColumnIndex).ToString().Trim()
                                    If GroupValue.Length = 0 Then
                                        GroupQueryPagingString.Append(" (")
                                        GroupQueryPagingString.Append(ColumnDescriptor)
                                        GroupQueryPagingString.Append(" = '")
                                        GroupQueryPagingString.Append(value)
                                        GroupQueryPagingString.Append("' or ")
                                        GroupQueryPagingString.Append(ColumnDescriptor)
                                        GroupQueryPagingString.Append(" is null )")
                                        GroupQueryPagingString.Append("  AND ")
                                    Else
                                        GroupQueryPagingString.Append(ColumnDescriptor)
                                        GroupQueryPagingString.Append(" = '")
                                        GroupQueryPagingString.Append(GroupValue)
                                        GroupQueryPagingString.Append("' AND ")

                                    End If
                                    ColumnIndex += 1
                                Next
                                GroupQueryPagingString.Remove(GroupQueryPagingString.Length - 4, 4)
                                'GroupQueryPagingString.Append(") x ")
                            Else
                                GroupQueryPagingString.Append(strPREQuery.ToString & " from (" & strQuery.ToString)
                                'GroupQueryPagingString.Append(") x ")

                                GroupQueryPagingString.Append(" ) wd " & strPOSTQuery.ToString)

                                If Not strPOSTQuery.ToString().ToLower().Contains("where") Then
                                    GroupQueryPagingString.Append(" WHERE ")
                                Else
                                    GroupQueryPagingString.Append(" And ")
                                End If

                                GroupQueryPagingString.Append(" rnum >= ")
                                GroupQueryPagingString.Append(Desde)
                                If Hasta > 0 Then
                                    GroupQueryPagingString.Append(" AND rnum <= ")
                                    GroupQueryPagingString.Append(Hasta)
                                End If

                                If Server.isOracle Then
                                    GroupQueryPagingString.Append(" Order by ")
                                    GroupQueryPagingString.Append(Orden.Trim())
                                End If

                            End If

                            '---------------------------------------------------------------------------------
                            dt = Server.Con.ExecuteDataset(CommandType.Text, GroupQueryPagingString.ToString())

                            If dt IsNot Nothing AndAlso dt.Tables.Count > 0 Then
                                If dtGroups.Rows.Count = 0 Then
                                    dtGroups = dt.Tables(0).Copy
                                Else
                                    dtGroups.Merge(dt.Tables(0))
                                End If
                                dtGroups.MinimumCapacity += dt.Tables(0).Rows.Count
                            End If
                        Next
                    Next
                    Return dtGroups
                Else
                    ds = Server.Con.ExecuteDataset(CommandType.Text, pagingString.ToString())
                End If
            End If

            If ds.Tables.Count > 0 Then Return ds.Tables(0)

            Return Nothing
        Catch ex As Exception
            Throw ex
        Finally
            Desde = Nothing
            Hasta = Nothing

            If Not IsNothing(ds) Then
                For i As Int32 = 0 To ds.Tables.Count - 1
                    ds.Tables(i).Dispose()
                Next
                ds.Tables.Clear()
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function

    Private Enum TaskQueryMode
        Workflow = 1
        WFStep = 2
        WFStepState = 3
        WFTask = 4

    End Enum
    ''' <summary>
    ''' Genera la consulta común para obtener las tareas. 
    ''' Luego se deberán aplicar los filtros necesarios para obtener lo deseado.
    ''' </summary>
    ''' <param name="docid"></param>
    ''' <param name="WithGridRights"></param>
    ''' <param name="indexs"></param>
    ''' <param name="auIndex"></param>
    ''' <param name="stepId"></param>
    ''' <returns>Consulta prearmada en formato StringBuilder</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''                             Created.
    '''        [Javier]   12/10/10  Modified.   Se agrega string de restricciones y string de declaracion de variables de fecha
    ''' </history>
    Private Sub GetCommonTaskStringBuilder(ByVal docTypeId As Long,
                                            ByVal WithGridRights As Boolean,
                                            ByVal indexs As List(Of IIndex),
                                            ByVal auIndex As List(Of IIndex),
                                            ByVal stepId As Long,
                                            workflowid As Int64,
                                            QueryMode As TaskQueryMode,
                                            ByVal RestrictionString As String,
                                            ByVal DateDeclarationString As String,
                                            ByVal CheckInColumnIsShortDate As Boolean,
                                            ByVal FlagAsRead As Boolean,
                                            ByVal wfstateID As Int64,
                                            ByVal searchType As SearchType,
                                            ByVal CurrentUserId As Int64,
                                            ByVal orderString As String,
                                            ByVal slstFiltersIndexsID As List(Of Long),
                                           ByRef strPREselect As StringBuilder,
                                           ByRef strselect As StringBuilder,
                                           ByRef strPOSTselect As StringBuilder)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Tipo de Busqueda: " & searchType.ToString())
        '[Ezequiel] Obtengo el nombre de la vista de la entidad
        Dim strTableI As String = Results_Factory.MakeTable(docTypeId, Results_Factory.TableType.Indexs)
        Dim strTableT As String = Results_Factory.MakeTable(docTypeId, Results_Factory.TableType.Document)

        Dim strWDSelect As New StringBuilder
        Dim noLock As String
        If Server.isSQLServer Then
            noLock = "With (nolock)"
        Else
            noLock = String.Empty
        End If

        Dim MainJoin As String
        Dim MainPOSTJoin As String
        If (auIndex IsNot Nothing AndAlso auIndex.Count > 0) Then
            MainJoin = String.Format(" from wfdocument wd {1} inner join {0} i  {1} on wd.doc_id = i.doc_id ", strTableI, If(Server.isSQLServer, noLock, String.Empty))
            MainPOSTJoin = String.Format(" inner join {0} t  {1} on wd.doc_id = t.doc_id ", strTableT, If(Server.isSQLServer, noLock, String.Empty))
        Else
            MainJoin = String.Format(" from wfdocument wd {0} ", If(Server.isSQLServer, noLock, String.Empty))
            MainPOSTJoin = String.Format("  inner join {0} i  {2} on wd.doc_id = i.doc_id inner join {1} t  {2} on i.doc_id = t.doc_id ", strTableI, strTableT, If(Server.isSQLServer, noLock, String.Empty))
        End If

        Dim whereids As New StringBuilder
        '[Ezequiel] Agrego a la consulta los atributos que preciso
        Dim f As Int16
        If slstFiltersIndexsID Is Nothing Then
            slstFiltersIndexsID = New List(Of Long)
        End If

        'Si es para cargar la grilla con resultados
        If searchType = SearchType.GridResults OrElse searchType = SearchType.OpenTask Then

            strselect.Append("SELECT ")

            If searchType <> SearchType.GridResults Then strselect.Append("/*+ FIRST_ROWS */ ")

            strselect.Append(" wd.doc_Id,")
            strselect.Append(" wd.doc_type_Id,")
            strselect.Append(" wd.step_Id,")
            strselect.Append(" wd.Do_State_ID, wd.IconId,")
            strselect.Append(" wd.User_Asigned,")
            strselect.Append(" wd.Exclusive,")
            If Server.isOracle Then
                strselect.Replace("Exclusive", "C_Exclusive")
            End If
            strselect.Append(" wd.Task_ID,")
            strselect.Append(" wd.Task_State_ID,")
            strselect.Append(" wd.work_id,")
            strselect.Append(" wd.Tag,")
            strselect.Append(" wd.ExpireDate as " & Chr(34) & GridColumns.EXPIREDATE_COLUMNNAME & Chr(34) & ",")
            strselect.Append(" wd.User_Asigned_By as " & Chr(34) & GridColumns.USER_ASIGNED_BY_COLUMNNAME & Chr(34) & ",")
            strselect.Append(" wd.Date_Asigned_By as " & Chr(34) & GridColumns.DATE_ASIGNED_BY_COLUMNNAME & Chr(34) & ",")
            strselect.Append(" wd.Remark,")

            strPREselect.Append(" SELECT ")

            If Server.isOracle Then
                strselect.Append(" wd.CheckIn")
            Else
                If CheckInColumnIsShortDate Then
                    strselect.Append("convert(datetime, Convert(Char(10), wd.CheckIn, 101), 101)")
                Else
                    strselect.Append(" wd.CheckIn")
                End If
            End If
            strselect.Append(" As " & Chr(34) & GridColumns.CHECKIN_COLUMNNAME & Chr(34) & ",")


            strselect.Append("wd.NAME As " & Chr(34) & GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME & Chr(34))

            'If Server.isOracle Then
            'strPREselect.Replace("Exclusive", "C_Exclusive")
            'End If
            strPREselect.Append(" wd.doc_id,wd.doc_type_id,wd.step_id,wd.do_state_id,wd.task_id,wd.iconid,wd.user_asigned,wd.c_exclusive,wd.task_state_id,wd.work_id,wd.tag,wd.remark")

            strPREselect.Append(",wd." & Chr(34) & "Tarea" & Chr(34))


            strPREselect.Append(If(Server.isOracle, ",NVL(wfstepstates.name, '')", ",IsNull(wfstepstates.name, '')"))
            strPREselect.Append(" As " & Chr(34) & GridColumns.STATE_COLUMNNAME & Chr(34) & ", ")
            strPREselect.Append(If(Server.isOracle, "NVL(wftask_states.task_state_name, '')", "IsNull(wftask_states.task_state_name, '')"))
            strPREselect.Append(" As " & Chr(34) & GridColumns.SITUACION_COLUMNNAME & Chr(34) & ", ")
            strPREselect.Append(If(Server.isOracle, "NVL(uag.name, '')", "IsNull(uag.name, '')"))
            strPREselect.Append(" As " & Chr(34) & GridColumns.ASIGNADO_COLUMNNAME & Chr(34) & ", ")
            strPREselect.Append("T.original_Filename As " & Chr(34) & GridColumns.ORIGINAL_FILENAME_COLUMNNAME & Chr(34))
            strPREselect.Append(" wd.*")


            strPREselect.Append(",wd." & Chr(34) & "Fecha de ingreso" & Chr(34))

            If (auIndex IsNot Nothing AndAlso auIndex.Count > 0) Then

                strPREselect.Append(",wd." & Chr(34) & "Fecha Creacion" & Chr(34))

            End If

            strPREselect.Append(",wd." & Chr(34) & "Vencimiento Tarea" & Chr(34))

            strPREselect.Append(",wd." & Chr(34) & "Usuario asignado por" & Chr(34))

            strPREselect.Append(",wd." & Chr(34) & "Fecha asignada por" & Chr(34))


            Dim CreateDateString As String

                If (auIndex IsNot Nothing AndAlso auIndex.Count > 0) Then
                    If Server.isOracle Then
                        CreateDateString = CreateDateString & ",crdate As " & Chr(34) & GridColumns.CRDATE_COLUMNNAME & Chr(34)
                    Else
                        CreateDateString = CreateDateString & ",convert(datetime,convert(Char(10),crdate,101),101) As " & Chr(34) & GridColumns.CRDATE_COLUMNNAME & Chr(34)
                    End If
                Else
                    If Server.isOracle Then
                        CreateDateString = CreateDateString & ",i.crdate As " & Chr(34) & GridColumns.CRDATE_COLUMNNAME & Chr(34)
                    Else
                        CreateDateString = CreateDateString & ",convert(datetime,convert(Char(10),i.crdate,101),101) As " & Chr(34) & GridColumns.CRDATE_COLUMNNAME & Chr(34)
                    End If
                End If


                If (auIndex IsNot Nothing AndAlso auIndex.Count > 0) Then
                    strselect.Append(CreateDateString)
                Else
                    strPREselect.Append(CreateDateString)
                End If

                strPREselect.Append(",T.DISK_GROUP_ID, T.PLATTER_ID, T.VOL_ID, DOC_FILE, OFFSET, ")
                strPREselect.Append("T.ICON_ID, T.Shared")
                strPREselect.Append(", T.ver_Parent_id, T.version as " & Chr(34) & GridColumns.VERSION_COLUMNNAME & Chr(34) & ", RootId, NumeroVersion as " & Chr(34) & GridColumns.NUMERO_DE_VERSION_COLUMNNAME & Chr(34) & ", disk_Volume.disk_Vol_id, disk_Volume.DISK_VOL_PATH ")


            If Server.isOracle Then

                strPREselect.Append(",wd." & Chr(34) & "RNUM" & Chr(34))
                'strPREselect.Append(",wd.rnum")
            End If

            Dim IndexsString As New StringBuilder
                If WithGridRights Then
                    For f = 0 To indexs.Count - 1

                        If Not auIndex Is Nothing AndAlso auIndex.Contains(DirectCast(indexs(f), Index)) AndAlso DirectCast(indexs(f), Index).isReference = False Then
                            IndexsString.Append(",")
                            IndexsString.Append("I" & DirectCast(indexs(f), Index).ID)

                            'strPREselect.Append(",")
                            'strPREselect.Append(If(Server.isOracle, "NVL(slst_s", " IsNull(slst_s"))
                            'strPREselect.Append(DirectCast(indexs(f), Index).ID & ".descripcion, '')")
                            'strPREselect.Append(" As " & Chr(34) & DirectCast(indexs(f), Index).Name.Trim & Chr(34))

                            strWDSelect.Append(",")
                            strWDSelect.Append("I" & DirectCast(indexs(f), Index).ID)

                            strWDSelect.Append(",")
                            strWDSelect.Append(If(Server.isOracle, "NVL(slst_s", " IsNull(slst_s"))
                            strWDSelect.Append(DirectCast(indexs(f), Index).ID & ".descripcion, '')")
                            strWDSelect.Append(" As " & Chr(34) & DirectCast(indexs(f), Index).Name.Trim & Chr(34))
                        Else
                            IndexsString.Append(",")
                            IndexsString.Append("I" & DirectCast(indexs(f), Index).ID)
                            IndexsString.Append(" As " & Chr(34) & DirectCast(indexs(f), Index).Name.Trim & Chr(34))

                            strWDSelect.Append(",")
                            strWDSelect.Append(Chr(34) & DirectCast(indexs(f), Index).Name.Trim & Chr(34))
                        End If
                    Next
                Else
                    For f = 0 To indexs.Count - 1
                        If indexs(f).isReference = False Then
                            IndexsString.Append(",I" & DirectCast(indexs(f), Index).ID)
                        End If
                    Next
                End If

                strPREselect.Replace("wd.*", strWDSelect.ToString)


                If (auIndex IsNot Nothing AndAlso auIndex.Count > 0) Then
                    strselect.Append(IndexsString.ToString())
                Else
                    strPREselect.Append(IndexsString.ToString())
                End If

                'Verifica si debe obtener la marca de leido
                If FlagAsRead Then
                    If Server.isOracle Then
                        strPREselect.Append(", ZDocReads.crdate As READDATE ")
                    Else
                        strPREselect.Append(", ZDocReads.crdate As READDATE ")
                    End If
                End If


                If QueryMode <> TaskQueryMode.WFTask AndAlso searchType <> SearchType.OpenTask Then



                    'Dim ColumOrderString As String = GridColumns.GetColumnNameByAliasName(GridColumns.GetColumnByOrderString(orderString))
                    'If Server.isOracle Then
                    'strselect.Insert(0, ", ROWNUM as RNUM from (")
                    'Else

                    'strselect.Append(", ROW_NUMBER() OVER (ORDER  BY " & orderString & ") RNUM  ")
                    strselect.Append(", ROW_NUMBER() OVER (ORDER  BY  task_id asc ) RNUM  ")

                    'End If
                    'strselect.Append(strPREselect.ToString())
                End If

            Else
                strselect.Append("Select count(1)  ")
        End If



        'Se agregan las tablas necesarias donde se obtendrán todos los datos
        strselect.Append(MainJoin)
        If searchType = SearchType.GridResultsCount Then
            strselect.Append(MainPOSTJoin)
        End If


        Dim HasRestrictions As Boolean = Not (String.IsNullOrEmpty(RestrictionString) OrElse RestrictionString.Trim() = String.Empty)

        If HasRestrictions OrElse searchType = SearchType.GridResults OrElse searchType = SearchType.OpenTask OrElse searchType = SearchType.GridResultsCount Then
            strPOSTselect.Append(MainPOSTJoin)
        End If

        If searchType = SearchType.GridResults OrElse searchType = SearchType.OpenTask Then
            strPOSTselect.Append(String.Format(" left join disk_Volume {0} On disk_Vol_id = T.vol_id", noLock))
        End If

        If searchType = SearchType.GridResults OrElse searchType = SearchType.OpenTask OrElse searchType = SearchType.GridResultsCount Then

            strPOSTselect.Append(String.Format(" left join wfstepstates {0} On do_state_id = doc_state_id", noLock))
            strPOSTselect.Append(" left join zvw_UserAndGroups uag On user_asigned = uag.id")
            strPOSTselect.Append(String.Format(" left join wftask_states {0} On wftask_states.task_state_id = wd.Task_State_ID", noLock))

            'Verifica si debe obtener la marca de leido
            If FlagAsRead AndAlso searchType <> SearchType.WFStepCount Then
                strPOSTselect.Append(" left join ZDocReads On I.doc_id = ZDocReads.DOCID And ZDocReads.USERID=")
                strPOSTselect.Append(CurrentUserId)
                strPOSTselect.Append(" ")
            End If
        End If

        If HasRestrictions OrElse searchType = SearchType.GridResults OrElse searchType = SearchType.OpenTask OrElse slstFiltersIndexsID.Count > 0 Then 'OrElse searchType = SearchType.GridResultsCount

            'Se agregan las slst donde se obtendran las descripciones de los atributos

            If Not auIndex Is Nothing AndAlso auIndex.Count > 0 Then
                For Each indice As IIndex In auIndex
                    If indice.isReference = False Then
                        If searchType <> SearchType.GridResultsCount OrElse (searchType = SearchType.GridResultsCount AndAlso slstFiltersIndexsID.Count > 0 AndAlso slstFiltersIndexsID.Contains(indice.ID)) AndAlso indice.isReference = False Then
                            strPOSTselect.Append(" left join slst_s")
                            strPOSTselect.Append(indice.ID)
                            strPOSTselect.Append(String.Format(" {0} On ", noLock))
                            strPOSTselect.Append("I")
                            strPOSTselect.Append(indice.ID)
                            strPOSTselect.Append(" = slst_s")
                            strPOSTselect.Append(indice.ID)
                            strPOSTselect.Append(".codigo ")
                        End If
                    End If
                Next
            End If

        End If

        'Agrega las restricciones

        If Not String.IsNullOrEmpty(RestrictionString) AndAlso RestrictionString.Trim() <> String.Empty Then
            If Not strselect.ToString().ToLower().Contains("where") Then
                strselect.Append(" WHERE ")
            End If
            strselect.Append(RestrictionString)
        End If

        If QueryMode = TaskQueryMode.Workflow Then
            If Not strselect.ToString().ToLower().Contains("where") Then
                strselect.Append(" WHERE wd.work_id = " & workflowid)
            Else
                strselect.Append(" And wd.work_id = " & workflowid)
            End If
        Else
            If Not strselect.ToString().ToLower().Contains("where") Then

                If searchType <> SearchType.OpenTask Then strselect.Append(" WHERE wd.step_id = " & stepId)
            Else
                If searchType <> SearchType.OpenTask Then strselect.Append(" And wd.step_id = " & stepId)
            End If

            If wfstateID <> 0 AndAlso searchType <> SearchType.OpenTask Then
                strselect.Append(" And wd.do_state_id=" & wfstateID)
            End If
        End If

        If searchType = SearchType.WFStepCount Then
            strselect.Append(" And wd.doc_type_id=" & docTypeId)
        End If

        If DateDeclarationString.Length > 0 Then
            'Si existe declaracion de variables de fecha las inserta al inicio del select
            strselect.Insert(0, DateDeclarationString)
        End If

    End Sub

    ''' <summary>
    ''' Genera la consulta común para obtener las tareas. 
    ''' Luego se deberán aplicar los filtros necesarios para obtener lo deseado.
    ''' </summary>
    ''' <param name="docid"></param>
    ''' <param name="WithGridRights"></param>
    ''' <param name="indexs"></param>
    ''' <param name="auIndex"></param>
    ''' <param name="stepId"></param>
    ''' <returns>Consulta prearmada en formato StringBuilder</returns>
    ''' <remarks></remarks>
    ''' <history>
    '''                             Created.
    '''        [Javier]   12/10/10  Modified.   Se agrega string de restricciones y string de declaracion de variables de fecha
    ''' </history>
    Private Shared Function GetCommonStepsTaskCountStringBuilder(ByVal docTypeId As Long, ByVal stepIds As List(Of Int64), ByVal RestrictionString As String, ByVal DateDeclarationString As String, ByVal FlagAsRead As Boolean) As StringBuilder

        '[Ezequiel] Obtengo el nombre de la vista de la entidad
        Dim strTableI As String = Results_Factory.MakeTable(docTypeId, Results_Factory.TableType.Indexs)
        Dim strTableT As String = Results_Factory.MakeTable(docTypeId, Results_Factory.TableType.Document)

        Dim MainJoin As String = String.Format("{0} T inner join {1} I On T.doc_id = I.doc_id", strTableT, strTableI)
        Dim strselect As StringBuilder = New StringBuilder()
        Dim whereids As New StringBuilder
        '[Ezequiel] Agrego a la consulta los atributos que preciso
        Dim f As Int16

        strselect.Append("Select count(1) FROM  wfdocument wd")

        'Agrega las restricciones
        If Not String.IsNullOrEmpty(RestrictionString) AndAlso RestrictionString.Trim() <> String.Empty Then
            strselect.Append(" WHERE ")
            strselect.Append(RestrictionString)
        End If

        strselect.Append(" WHERE wd.step_id In (" & stepIds.ToString() & ")")

        Return strselect

    End Function



    ''' <summary>
    ''' Obtiene las tareas de la etapa
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Tomas] 19/06/2009  Modified    Se adapta el método para trabajar con stored procedures
    ''' </history>


    Public Shared Function GetIndexDropDownType(ByVal indexId As Int64) As Int16
        Dim query As New StringBuilder
        query.Append("Select DROPDOWN from doc_index WHERE index_id =")
        query.Append(indexId)
        Return Server.Con.ExecuteScalar(CommandType.Text, query.ToString)
    End Function

    'Public Shared Function GetIndexTypeByName(ByVal indexname As String) As Int16
    '    Dim query As New StringBuilder
    '    query.Append("Select DROPDOWN from doc_index where index_name = '")
    '    query.Append(indexname & "'")
    '    Return Server.Con.ExecuteScalar(CommandType.Text, query.ToString)
    'End Function
#End Region

#Region "Insert"


    ''' <summary>
    ''' Inserta una tarea en el WF
    ''' </summary>
    ''' <param name="Tasks"></param>
    ''' <param name="WFID"></param>
    ''' <param name="InitialStep"></param>
    ''' <param name="tr"></param>
    ''' <remarks></remarks>
    Public Shared Sub InsertTasks(ByVal Tasks As List(Of ITaskResult), ByVal WFID As Int64, ByVal WFName As String, ByVal InitialStepId As Int64, ByVal InitialStateId As Int64, ByRef tr As Transaction)
        For i As Int32 = 0 To Tasks.Count - 1
            InsertTask(Tasks(i), WFID, WFName, InitialStepId, InitialStateId, tr, Membership.MembershipHelper.CurrentUser)
        Next
    End Sub
    Public Shared Sub InsertTask(ByVal t As ITaskResult, ByVal WFID As Int64, ByVal WFName As String, ByVal InitialStepId As Int64, ByVal InitialStateId As Int64, ByRef tr As Transaction, ByVal CurrentUser As IUser)
        Try
            Dim strinsert As String = String.Empty
            Dim UserAsignedTo As Int64
            Dim dateasigned As String
            Dim userAsignedBy As Int64

            If Not IsNothing(t.AsignedToId) Then
                UserAsignedTo = t.AsignedToId
                dateasigned = Server.Con.ConvertDateTime(Now)
                If Not IsNothing(CurrentUser) Then
                    userAsignedBy = CurrentUser.ID
                End If
            Else
                UserAsignedTo = 0
                userAsignedBy = 0
                dateasigned = "null"
            End If


            If Server.isOracle Then
                strinsert = "insert into wfdocument(Doc_ID,DOC_TYPE_ID,Folder_ID,step_Id,Do_State_ID,Name,IconId,CheckIn,User_Asigned,C_Exclusive,ExpireDate,User_Asigned_By,Date_Asigned_By,Task_ID,Task_State_ID,work_id) values(" _
                        & t.ID & "," & t.DocType.ID & ",0," & InitialStepId & "," & InitialStateId & ",'" & t.Name & "'," & t.IconId & "," & Server.Con.ConvertDateTime(t.CheckIn) & "," & UserAsignedTo & ",0," & Server.Con.ConvertDateTime(t.ExpireDate) & "," & userAsignedBy & "," & dateasigned & "," & t.TaskId & "," & CInt(t.TaskState) & "," & WFID & ")"
            Else
                strinsert = "insert into wfdocument(Doc_ID,DOC_TYPE_ID,Folder_ID,step_Id,Do_State_ID,Name,IconId,CheckIn,User_Asigned,Exclusive,ExpireDate,User_Asigned_By,Date_Asigned_By,Task_ID,Task_State_ID,work_id) values(" _
                       & t.ID & "," & t.DocType.ID & ",0," & InitialStepId & "," & InitialStateId & ",'" & t.Name & "'," & t.IconId & "," & Server.Con.ConvertDateTime(t.CheckIn) & "," & UserAsignedTo & ",0," & Server.Con.ConvertDateTime(t.ExpireDate) & "," & userAsignedBy & "," & dateasigned & "," & t.TaskId & "," & CInt(t.TaskState) & "," & WFID & ")"
            End If
            If (tr Is Nothing) Then
                Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
            Else
                tr.Con.ExecuteNonQuery(tr.Transaction, CommandType.Text, strinsert)
            End If

            'ML: Select cree duplicada esta accion porque existe el LOGCHECKIN.
            'ActionsFactory.SaveActioninDB(t.ID, ObjectTypes.Documents, RightsType.AgregarDocumento, "Se agrego el documento: " & t.DocType.Name & _
            '                                  " con id:  " & Convert.ToString(t.ID) & ", en el WF: " & WFName, RightFactory.CurrentUser.ID, RightFactory.CurrentUser.ConnectionId, Environment.MachineName)

        Catch ex As SqlClient.SqlException
            raiseerror(ex)
            Throw New Exception("Ocurrio un error al insertar el documento " & t.Name & " en el WorkFlow")
        End Try

    End Sub
    'ML: Se discontinua el metodo
    ''' <summary>



#End Region

#Region "Update"
    <Obsolete("Metodo discontinuado", False)>
    Public Shared Event drefresh(ByRef Result As TaskResult)
    ''' <summary>
    ''' Actualiza la tabla WFDocument
    ''' </summary>
    ''' <remarks></remarks>
    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub UpdateDistribuir(ByRef Result As TaskResult)
        'distribuye un dato solo
        Dim strupdate As New StringBuilder
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
                strupdate.Append(",LastUpdateDate=sysdate")
            Else
                strupdate.Append(",LastUpdateDate=getdate()")
            End If

            strupdate.Append(" WHERE Task_ID = ")
            strupdate.Append(Result.TaskId)
            'ZTrace.WriteLineIf(ZTrace.IsInfo,"EJECUTO SENTENCIA")
            'ZTrace.WriteLineIf(ZTrace.IsInfo,strupdate.ToString)
            Server.Con.ExecuteNonQuery(CommandType.Text, strupdate.ToString)
        Catch ex As Exception
            raiseerror(ex)
        End Try

        strupdate = Nothing
    End Sub


    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub UpdateAssign(ByRef Result As TaskResult)
        Dim AsignedToId, AsignedById As Int32

        AsignedToId = Result.AsignedToId

        Dim StrBuilder As New StringBuilder()
        If Result.AsignedById <> 0 Then
            AsignedById = Result.AsignedById
            StrBuilder.Append("UPDATE WFDOCUMENT SET USER_ASIGNED = ")
            StrBuilder.Append(AsignedToId.ToString())
            StrBuilder.Append(" ,USER_ASIGNED_BY = ")
            StrBuilder.Append(AsignedById.ToString())
            StrBuilder.Append(" ,DATE_ASIGNED_BY = " & Server.Con.ConvertDateTime(Result.AsignedDate))
            StrBuilder.Append(" ,Task_State_ID = ")
            StrBuilder.Append((CInt(Result.TaskState)).ToString())

            If Server.isOracle Then
                StrBuilder.Append(",LastUpdateDate=sysdate")
            Else
                StrBuilder.Append(",LastUpdateDate=getdate()")
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

            If Server.isOracle Then
                StrBuilder.Append(",LastUpdateDate=sysdate")
            Else
                StrBuilder.Append(",LastUpdateDate=getdate()")
            End If

            StrBuilder.Append(" WHERE Task_ID = ")
            StrBuilder.Append(Result.TaskId.ToString())
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString())
    End Sub

    Public Shared Sub UpdateAssign(ByVal taskId As Int64, ByVal asignedToUserId As Int64, ByVal asignedByUserId As Int64, ByVal asignedDate As Date, ByVal taskStateId As Int32)
        Dim StrBuilder As New StringBuilder()

        StrBuilder.Append("UPDATE WFDOCUMENT SET USER_ASIGNED = ")
        StrBuilder.Append(asignedToUserId.ToString())
        StrBuilder.Append(" ,USER_ASIGNED_BY = ")
        StrBuilder.Append(asignedByUserId.ToString())
        StrBuilder.Append(" ,DATE_ASIGNED_BY = ")
        StrBuilder.Append(Server.Con.ConvertDateTime(asignedDate))
        StrBuilder.Append(" ,Task_State_ID = ")
        StrBuilder.Append(taskStateId.ToString())

        If Server.isOracle Then
            StrBuilder.Append(",LastUpdateDate=sysdate")
        Else
            StrBuilder.Append(",LastUpdateDate=getdate()")
        End If

        StrBuilder.Append(" WHERE Task_ID = ")
        StrBuilder.Append(taskId.ToString())

        Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString())
    End Sub

    Public Shared Sub UpdateAssign(ByVal taskId As Int64, ByVal asignedToUserId As Int64, ByVal asignedByUserId As Int64, ByVal asignedDate As Date, ByVal taskState As TaskStates)

        UpdateAssign(taskId, asignedToUserId, asignedByUserId, asignedDate, DirectCast(taskState, Int32))
    End Sub

    Public Sub UpdateUserTaskStateToAsign(ByVal userId As Int64)
        Dim StrBuilder As New StringBuilder()
        StrBuilder.Append("Update WFDocument Set Task_State_ID=1 where user_asigned=")
        StrBuilder.Append(userId.ToString())
        StrBuilder.Append(" and Task_State_ID=2")

        Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString())
    End Sub

    Public Shared Sub UpdateTaskState(ByVal taskId As Int64, ByVal taskStateId As Int32)
        Dim StrBuilder As New StringBuilder()
        StrBuilder.Append("Update WFDocument Set Task_State_ID=")
        StrBuilder.Append(taskStateId.ToString())

        If Server.isOracle Then
            StrBuilder.Append(",LastUpdateDate=sysdate")
        Else
            StrBuilder.Append(",LastUpdateDate=getdate()")
        End If

        StrBuilder.Append(" WHERE task_id=")
        StrBuilder.Append(taskId.ToString())

        Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString())

    End Sub

    Public Shared Sub UpdateAllUserAsignedTasksState(ByVal UserID As Long)
        Dim StrBuilder As New StringBuilder()
        StrBuilder.Append("UPDATE WFDocument SET Task_State_ID=1")

        If Server.isOracle Then
            StrBuilder.Append(",LastUpdateDate=sysdate")
            StrBuilder.Append(", c_exclusive = 0")
        Else
            StrBuilder.Append(",LastUpdateDate=getdate()")
            StrBuilder.Append(", exclusive = 0")
        End If


        StrBuilder.Append(" WHERE user_asigned=")
        StrBuilder.Append(UserID.ToString())
        StrBuilder.Append(" and task_state_id=2")

        Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString())
    End Sub
    <Obsolete("Metodo discontinuado", False)>
    Public Shared Function UpdateTaskState(ByRef Result As TaskResult) As Boolean
        Dim StrBuilder As New StringBuilder
        StrBuilder.Append("Update WFDocument Set Task_State_ID=" & CInt(Result.TaskState).ToString())

        If Server.isOracle Then
            StrBuilder.Append(",LastUpdateDate=sysdate")
        Else
            StrBuilder.Append(",LastUpdateDate=getdate()")
        End If

        StrBuilder.Append(" WHERE task_id=" & Result.TaskId & " and User_asigned = " & Result.AsignedToId)

        Dim AffectedRows As Int64 = Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString)
        If AffectedRows > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub UpdateConIDTaskStateToAsign(ByVal conId As Int64)
        Dim StrBuilder As New StringBuilder()
        StrBuilder.Append("Update WFDocument Set Task_State_ID=1 where user_asigned in (SELECT USER_ID FROM UCM WHERE CON_ID =")
        StrBuilder.Append(conId.ToString())
        StrBuilder.Append(") and Task_State_ID=2")
        Dim query As String = StrBuilder.ToString
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Sub UpdateState(ByVal taskId As Int64, ByVal stepId As Int64, ByVal stateId As Int64)

        Dim StrBuilder As New StringBuilder

        StrBuilder.Append("Update WFDocument Set Do_state_id=" & stateId)

        If Server.isOracle Then
            StrBuilder.Append(",LastUpdateDate=sysdate")
        Else
            StrBuilder.Append(",LastUpdateDate=getdate()")
        End If

        StrBuilder.Append(" WHERE task_id=" & taskId & " AND step_id=" & stepId)
        Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString)
    End Sub



    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub UpdateState(ByRef Result As TaskResult, ByRef t As Transaction)
        Dim StrBuilder As New StringBuilder

        StrBuilder.Append("Update WFDocument Set Do_state_id=" & Result.State.ID)

        If Server.isOracle Then
            StrBuilder.Append(",LastUpdateDate=sysdate")
        Else
            StrBuilder.Append(",LastUpdateDate=getdate()")
        End If

        StrBuilder.Append(" WHERE task_id=" & Result.TaskId & " and step_id = " & Result.StepId)

        If t Is Nothing Then
            Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString)
        Else
            t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, StrBuilder.ToString)
        End If
    End Sub

    Public Shared Sub UpdateState(ByVal taskId As Int64, ByVal stepId As Int64, ByVal stateId As Int32)

        Dim StrBuilder As New StringBuilder()
        StrBuilder.Append("Update WFDocument Set Do_state_id = ")
        StrBuilder.Append(stateId.ToString)

        If Server.isOracle Then
            StrBuilder.Append(",LastUpdateDate=sysdate")
        Else
            StrBuilder.Append(",LastUpdateDate=getdate()")
        End If

        StrBuilder.Append(" WHERE task_id = ")
        StrBuilder.Append(taskId.ToString)
        StrBuilder.Append(" and step_id = ")
        StrBuilder.Append(stepId.ToString)

        Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString())

        StrBuilder.Remove(0, StrBuilder.Length)
        StrBuilder = Nothing
    End Sub




    Public Shared Sub UpdateExpiredDate(ByVal taskId As Int64, ByVal expireDate As Date)
        Dim QueryBuilder As New StringBuilder()

        QueryBuilder.Append("UPDATE WfDocument SET ExpireDate=")
        QueryBuilder.Append(Server.Con.ConvertDateTime(expireDate))

        If Server.isOracle Then
            QueryBuilder.Append(",LastUpdateDate=sysdate")
        Else
            QueryBuilder.Append(",LastUpdateDate=getdate()")
        End If

        QueryBuilder.Append(" WHERE Task_ID = ")
        QueryBuilder.Append(taskId.ToString())

        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())
    End Sub

#End Region

#Region "Tasks & Connections"
    Public Function GetUserOpenedTasks(ByVal userId As Int64) As DataTable
        Dim query As String = "select * from wfdocument where Task_ID in (Select TASKID FROM USR_R_OPENTASK WHERE USERID =" & userId & ")"
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    Public Sub CloseOpenTasksByConId(ByVal conId As Int64)
        Server.Con.ExecuteNonQuery(CommandType.Text, "update wfdocument set task_state_id = 1 WHERE task_state_id = 2 and User_Asigned = (SELECT USER_ID FROM UCM where CON_ID=" & conId & ")")
    End Sub

    Public Sub CloseOpenTasksByTaskId(ByVal taskId As Int64)
        Dim query As String = "DELETE USR_R_OPENTASK WHERE TASKID=" & taskId
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
        query = "update wfdocument set task_state_id = 1 WHERE task_state_id = 2 and task_id = " & taskId & ""
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Sub ReleaseOpenTasksWithOutConnection()
        Dim query As String = "DELETE USR_R_OPENTASK  WHERE TASKID in (select task_id from WFDocument  where task_state_id = 2 and user_asigned not in (select userid from UCM ))"
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
        query = "update WFDocument  set task_state_id = 1 where task_state_id = 2 and user_asigned not in (select user_id from UCM )"
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Sub RegisterTaskAsOpen(ByVal taskId As Int64, ByVal userId As Int64)
        If Server.isSQLServer Then
            Dim params As Object() = {userId, taskId}
            Server.Con.ExecuteNonQuery("ZSP_WORKFLOW_100_SetOpenTask", params)
            params = Nothing
        Else
            Dim query As String = "INSERT INTO USR_R_OPENTASK values (" & userId & "," & taskId & ", SYSDATE)"
            Server.Con.ExecuteNonQuery(CommandType.Text, query)
        End If
    End Sub
#End Region

#Region "Delete"
    Public Shared Sub Delete(ByVal taskId As Int64)
        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("DELETE WFDOCUMENT WHERE Task_ID = ")
        QueryBuilder.Append(taskId.ToString())

        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString())

    End Sub

    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub Delete(ByRef Result As TaskResult)
        Dim strdelete As String = "DELETE WFDOCUMENT WHERE Task_ID = " & Result.TaskId
        Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)

    End Sub
#End Region

#Region "Logs"


    Public Shared Sub LogCheckInOut(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int64, ByVal docTypeName As String, ByVal stepId As Int32, ByVal statename As String, ByVal workflowId As Int32, ByVal taskCheckIn As Date, ByVal stepname As String, ByVal workflowname As String, ByRef t As Transaction, Currentusername As String, ByVal ActionName As String)
        Try
            Dim insertquery As String

            If taskName.Length > 200 Then
                taskName = taskName.Substring(0, 200)
            End If

            If t Is Nothing Then

                If Server.isOracle Then
                    insertquery = String.Format("INSERT INTO WFSTEPHST (DOC_ID,DOC_NAME,docTypeId,doc_type_name,FOLDER_ID,stepId,STEP_NAME,STATE,USERNAME,ACCION,FECHA,workflowId,workflowname) VALUES({0},'{1}',{2},'{3}',{4},{5},'{6}','{7}','{8}','{9}',{10},{11},'{12}')",
      taskID, taskName, docTypeId, docTypeName, 0, stepId, stepname, statename, Currentusername, ActionName,
      "SYSDATE", workflowId, workflowname)
                Else
                    insertquery = String.Format("INSERT INTO WFSTEPHST (DOC_ID,DOC_NAME,docTypeId,doc_type_name,FOLDER_ID,stepId,STEP_NAME,STATE,USERNAME,ACCION,FECHA,workflowId,workflowname) VALUES({0},'{1}',{2},'{3}',{4},{5},'{6}','{7}','{8}','{9}',{10},{11},'{12}')",
      taskID, taskName, docTypeId, docTypeName, 0, stepId, stepname, statename, Currentusername, ActionName,
      "GETDATE()", workflowId, workflowname)
                End If
                Server.Con.ExecuteNonQuery(CommandType.Text, insertquery)
            Else
                If Server.isOracle Then
                    insertquery = String.Format("INSERT INTO WFSTEPHST (DOC_ID,DOC_NAME,docTypeId,doc_type_name,FOLDER_ID,stepId,STEP_NAME,STATE,USERNAME,ACCION,FECHA,workflowId,workflowname) VALUES({0},'{1}',{2},'{3}',{4},{5},'{6}','{7}','{8}','{9}',{10},{11},'{12}')",
      taskID, taskName, docTypeId, docTypeName, 0, stepId, stepname, statename, Currentusername, ActionName,
      "SYSDATE", workflowId, workflowname)
                Else
                    insertquery = String.Format("INSERT INTO WFSTEPHST (DOC_ID,DOC_NAME,docTypeId,doc_type_name,FOLDER_ID,stepId,STEP_NAME,STATE,USERNAME,ACCION,FECHA,workflowId,workflowname) VALUES({0},'{1}',{2},'{3}',{4},{5},'{6}','{7}','{8}','{9}',{10},{11},'{12}')",
      taskID, taskName, docTypeId, docTypeName, 0, stepId, stepname, statename, Currentusername, ActionName,
      "GETDATE()", workflowId, workflowname)
                End If
                t.Con.ExecuteNonQuery(t.Transaction, CommandType.Text, insertquery)
            End If

            'LogStepPerformance(taskID, taskName, stepId, Currentusername, workflowId, taskCheckIn, stepname, workflowname)

        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub

    'Private Shared Sub LogStepPerformance(ByVal taskID As Int64, ByVal taskName As String, ByVal stepId As Int64, ByVal userName As String, ByVal workflowId As Int64, ByVal taskCheckIn As Date, ByVal stepname As String, ByVal workflowname As String, Optional ByRef t As Transaction = Nothing)

    '    '        Dim QueryBuilder As StringBuilder
    '    '       Dim TotalMinutes As Int32
    '    '      Dim Ds As DataSet

    '    'Ver si hay una etapa anterior para actualizar la fecha de checkedout
    '    'QueryBuilder = New StringBuilder()

    '    'If Server.isOracle Then
    '    '    QueryBuilder.Append("SELECT * FROM (")
    '    '    QueryBuilder.Append("   SELECT ")
    '    '    QueryBuilder.Append("       StepId, CheckedIn ")
    '    '    QueryBuilder.Append("   FROM ")
    '    '    QueryBuilder.Append("       WfStepPerformance ")
    '    '    QueryBuilder.Append("   WHERE ")
    '    '    QueryBuilder.Append("       WorkflowId = " + workflowId.ToString)
    '    '    QueryBuilder.Append("       AND DocumentId = " + taskID.ToString)
    '    '    QueryBuilder.Append("   ORDER BY ")
    '    '    QueryBuilder.Append("       CheckedIn DESC ")
    '    '    QueryBuilder.Append(" ) WHERE RowNum = 1 ")
    '    'Else
    '    '    QueryBuilder.Append("   SELECT TOP 1 ")
    '    '    QueryBuilder.Append("       StepId, CheckedIn ")
    '    '    QueryBuilder.Append("   FROM ")
    '    '    QueryBuilder.Append("       WfStepPerformance with (nolock)")
    '    '    QueryBuilder.Append("   WHERE ")
    '    '    QueryBuilder.Append("       WorkflowId = " + workflowId.ToString)
    '    '    QueryBuilder.Append("       AND DocumentId = " + taskID.ToString)
    '    '    QueryBuilder.Append("   ORDER BY ")
    '    '    QueryBuilder.Append("       CheckedIn DESC ")
    '    'End If

    '    'Ds = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString)

    '    'Insertar la nueva etapa y su fecha de checkedin
    '    ' TotalMinutes = System.Convert.ToInt32(Date.Now.Subtract(taskCheckIn).TotalMinutes)

    '    'PRIMERO
    '    If Server.isOracle Then
    '        Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("INSERT INTO WfStepPerformance (DocumentId, DocumentName, CheckedIn, CheckedOut, StepId, StepName, ConsumedMinutes, WorkflowId, WorkflowName, UserName) VALUES ({0},'{1}',to_date('{2}','DD/MM/YYYY HH24:MI:SS'),to_date('{3}','DD/MM/YYYY HH24:MI:SS'),{4},'{5}',{6},{7},'{8}','{9}')", taskID.ToString, taskName, taskCheckIn.ToString("dd/MM/yyyy HH:mm"), fecha.ToString("dd/MM/yyyy HH:mm"), stepId.ToString, stepname, 0, workflowId.ToString, workflowname, userName))
    '    Else
    '        Dim parameters() As Object = {taskID.ToString, taskName, taskCheckIn, Date.Now, stepId.ToString, stepname, 0, workflowId.ToString, workflowname, userName}
    '        If t Is Nothing Then
    '            Server.Con.ExecuteNonQuery("zsp_workflow_100_InsertWfStepPerformance", parameters)
    '        Else
    '            t.Con.ExecuteNonQuery(t.Transaction, "zsp_workflow_100_InsertWfStepPerformance", parameters)
    '        End If
    '    End If

    '    'Si hay una etapa anterior actualizar la fecha de checkedout
    '    'If Ds.Tables.Count > 0 AndAlso Ds.Tables(0).Rows.Count > 0 Then

    '    '    TotalMinutes = System.Convert.ToInt32(Date.Now.Subtract(Ds.Tables(0).Rows(0).Item("CheckedIn")).TotalMinutes)

    '    '    'SEGUNDO
    '    '    If Server.isOracle Then
    '    '        'Implementar luego para ORACLE            
    '    '    Else
    '    '        Dim parameters() As Object = {Date.Now, TotalMinutes.ToString(), workflowId.ToString, taskID.ToString, Ds.Tables(0).Rows(0).Item("StepId").ToString()}
    '    '        If t Is Nothing Then
    '    '            Server.Con.ExecuteNonQuery("zsp_workflow_100_UpdCheckedOut_WfStepPerformance", parameters)
    '    '        Else
    '    '            t.Con.ExecuteNonQuery(t.Transaction, "zsp_workflow_100_UpdCheckedOut_WfStepPerformance", parameters)
    '    '        End If
    '    '    End If
    '    'End If
    'End Sub





    Public Shared Sub LogStartTask(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int64, ByVal docTypeName As String, ByVal stepId As Int32, ByVal state As String, ByVal workflowId As Int32, CurrentUserName As String, stepname As String, workflowname As String)
        Dim query As String = String.Format("INSERT INTO WFStepHst (Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion, Fecha, WorkflowId, WorkflowName) VALUES ({0}, '{1}', {2}, '{3}', {4}, {5}, '{6}', '{7}', '{8}', '{9}', {10}, {11}, '{12}')", taskID, taskName, docTypeId, docTypeName, 0, stepId, stepname, state, CurrentUserName, "Inicio Tarea", Servers.Server.Con.ConvertDateTime(Now), workflowId, workflowname)
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Sub LogFinishTask(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int64, ByVal docTypeName As String, ByVal stepId As Int64, ByVal state As String, ByVal workflowId As Int64, ByVal CurrentUserName As String, stepname As String, workflowname As String)
        Dim query As String = String.Format("INSERT INTO WFStepHst (Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion, Fecha, WorkflowId, WorkflowName) VALUES ({0}, '{1}', {2}, '{3}', {4}, {5}, '{6}', '{7}', '{8}', '{9}', {10}, {11}, '{12}')", taskID, taskName, docTypeId, docTypeName, 0, stepId, stepname, state, CurrentUserName, "Finalizo Tarea", Servers.Server.Con.ConvertDateTime(Now), workflowId, workflowname)
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Sub LogUserAction(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int64, ByVal docTypeName As String, ByVal stepId As Int64, ByVal state As String, ByVal workflowId As Int64, ByVal accionDeUsuario As String, CurrentUserName As String, stepname As String, workflowname As String)
        If accionDeUsuario.Length > 473 Then
            accionDeUsuario = accionDeUsuario.Substring(0, 473)
        End If
        Dim query As String = String.Format("INSERT INTO WFStepHst (Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion, fecha, WorkflowId, WorkflowName) VALUES ({0}, '{1}', {2}, '{3}', {4}, {5}, '{6}', '{7}', '{8}', '{9}', {10}, {11}, '{12}')", taskID, taskName, docTypeId, docTypeName, 0, stepId, stepname, state, CurrentUserName, "Ejecuto acción de usuario: " & accionDeUsuario, Servers.Server.Con.ConvertDateTime(Now), workflowId, workflowname)
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Sub LogOtherActions(ByVal taskId As Int64, ByVal taskName As String, ByVal docTypeId As Int64, ByVal docTypeName As String, ByVal stepId As Int32, ByVal state As String, ByVal workflowId As Int32, ByVal comment As String, CurrentUserName As String, stepname As String, workflowname As String)
        If comment.Length > 500 Then
            comment = comment.Substring(0, 500)
        End If
        Dim query As String = String.Format("INSERT INTO WFStepHst (Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion, Fecha, WorkflowId, WorkflowName) VALUES ({0}, '{1}', {2}, '{3}', {4}, {5}, '{6}', '{7}', '{8}', '{9}', {10}, {11}, '{12}')", taskId, taskName, docTypeId, docTypeName, 0, stepId, stepname, state, CurrentUserName, comment, Servers.Server.Con.ConvertDateTime(Now), workflowId, workflowname)
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub


    Public Shared Sub LogAction(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int64, ByVal docTypeName As String, ByVal stepId As Int32, ByVal state As String, ByVal workflowId As Int32, ByVal comentario As String, CurrentUserName As String, stepname As String, workflowname As String)
        Dim query As String = String.Format("INSERT INTO WFStepHst (Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion, Fecha, WorkflowId, WorkflowName) VALUES ({0}, '{1}', {2}, '{3}', {4}, {5}, '{6}', '{7}', '{8}', '{9}', {10}, {11}, '{12}')", taskID, taskName, docTypeId, docTypeName, 0, stepId, stepname, state, CurrentUserName, comentario, Servers.Server.Con.ConvertDateTime(Now), workflowId, workflowname)
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Sub LogRejectTask(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int64, ByVal docTypeName As String, ByVal stepId As Int32, ByVal state As String, ByVal workflowId As Int32, CurrentUserName As String, stepname As String, workflowname As String)
        Dim query As String = String.Format("INSERT INTO WFStepHst (Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion, Fecha, WorkflowId, WorkflowName) VALUES ({0}, '{1}', {2}, '{3}', {4}, {5}, '{6}', '{7}', '{8}', '{9}', {10}, {11}, '{12}')", taskID, taskName, docTypeId, docTypeName, 0, stepId, stepname, state, CurrentUserName, "Rechazo Tarea", Servers.Server.Con.ConvertDateTime(Now), workflowId, workflowname)
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Sub LogViewTask(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int64, ByVal docTypeName As String, ByVal stepId As Int32, ByVal state As String, ByVal workflowId As Int32, CurrentUserName As String, stepname As String, workflowname As String)
        Dim query As String = String.Format("INSERT INTO WFStepHst (Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion, Fecha, WorkflowId, WorkflowName) VALUES ({0}, '{1}', {2}, '{3}', {4}, {5}, '{6}', '{7}', '{8}', '{9}', {10}, {11}, '{12}')", taskID, taskName, docTypeId, docTypeName, 0, stepId, stepname, state, CurrentUserName, "Consulto Tarea", Servers.Server.Con.ConvertDateTime(Now), workflowId, workflowname)
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Sub LogAsignedTask(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int64, ByVal docTypeName As String, ByVal stepId As Int32, ByVal state As String, ByVal assignedTo As String, ByVal workflowId As Int32, CurrentUserName As String, stepname As String, workflowname As String)

        Dim query As String = String.Format("INSERT INTO WFStepHst (Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion, Fecha, WorkflowId, WorkflowName) VALUES ({0}, '{1}', {2}, '{3}', {4}, {5}, '{6}', '{7}', '{8}', '{9}', {10}, {11}, '{12}')", taskID, taskName, docTypeId, docTypeName, 0, stepId, stepname, state, CurrentUserName, "Asigno Tarea a " & assignedTo, Servers.Server.Con.ConvertDateTime(Now), workflowId, workflowname)
        Server.Con.ExecuteNonQuery(CommandType.Text, query)
    End Sub

    Public Shared Sub LogChangeExpireDate(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int64, ByVal docTypeName As String, ByVal stepId As Int32, ByVal state As String, ByVal workflowId As Int32, CurrentUserName As String, stepname As String, workflowname As String)
        Dim query As String = String.Format("INSERT INTO WFStepHst (Doc_Id, Doc_Name, DocTypeId, Doc_Type_Name, FOLDER_Id, StepId, Step_Name, State, UserName, Accion, Fecha, WorkflowId, WorkflowName) VALUES ({0}, '{1}', {2}, '{3}', {4}, {5}, '{6}', '{7}', '{8}', '{9}', {10}, {11}, '{12}')", taskID, taskName, docTypeId, docTypeName, 0, stepId, stepname, state, CurrentUserName, "Cambio Vencimiento", Servers.Server.Con.ConvertDateTime(Now), workflowId, workflowname)
        Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("INSERT INTO WFStepHst(	Doc_Id, 	Doc_Name, 	DocTypeId, 	Doc_Type_Name, 	FOLDER_Id, 	StepId, 	Step_Name, 	State, 	UserName, 	Accion, 	Fecha, 	WorkflowId, 	WorkflowName) VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})", taskID, taskName, docTypeId, docTypeName, 0, stepId, stepname, state, CurrentUserName, "Cambio Vencimiento", Now, workflowId, workflowname))
    End Sub

    Private Class TaskLogInformation
        Implements IDisposable

#Region "Constantes"
        Private Shared STORED_PROCEDURE_GET_LOG_INFO As String = "zsp_100_GetLogInformation"
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
        Private Sub New(ByVal documentName As String, ByVal docTypeID As Int64, ByVal docTypeName As String, ByVal taskStateId As Int32, ByVal taskStateName As String, ByVal stepId As Int64, ByVal workflowId As Int64)
            _docTypeID = docTypeID
            _docTypeName = docTypeName
            _taskName = documentName

            _stepId = stepId
            _taskStateId = taskStateId
            _taskStateName = taskStateName
            _workflowId = workflowId
        End Sub
#End Region

        Public Shared Function GetLogInformation(ByVal taskId As Int64) As TaskLogInformation
            If Server.isSQLServer Then
                Dim Parameters(0) As Object
                Parameters(0) = taskId

                Dim DsLogInformation As DataSet = Server.Con.ExecuteDataset(STORED_PROCEDURE_GET_LOG_INFO, Parameters)
                If Not IsNothing(DsLogInformation) AndAlso DsLogInformation.Tables(0).Rows.Count > 0 Then
                    Return BuildLogInformation(DsLogInformation.Tables(0).Rows(0))
                End If
            ElseIf Server.isOracle Then
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
                query.Append("WHERE ")
                query.Append(" WFDocument.task_Id = ")
                query.Append(taskId)

                Dim DsLogInformation As DataSet = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
                If Not IsNothing(DsLogInformation) AndAlso DsLogInformation.Tables(0).Rows.Count > 0 Then
                    Return BuildLogInformation(DsLogInformation.Tables(0).Rows(0))
                End If
            End If
            Return Nothing
        End Function

        Private Shared Function BuildLogInformation(ByVal dr As DataRow) As TaskLogInformation

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

    Public Shared Function CountFilesInIP_Task(ByVal conf_Id As Decimal) As Integer
        Dim strselect As String = "SELECT COUNT(Id) FROM IP_Task WHERE Id = " & conf_Id
        Return Server.Con.ExecuteScalar(CommandType.Text, strselect)
    End Function
#End Region

#Region "AutomaticAddResultsToWf"
    <Obsolete("Metodo discontinuado", False)>
    Public Shared Sub AddResultsToWorkFLowTask(ByVal Results As ArrayList, ByVal WfId As Int32)

        Dim W As ArrayList = WFFactory.GetWFsToAddDocuments(Membership.MembershipHelper.CurrentUser)
        For Each WF As WorkFlow In W
            If WF.ID = WfId Then
                'LO ASOCIO
                Dim TasksResults As New List(Of ITaskResult)
                For Each Result As Result In Results
                    'TODO Verificar
                    Dim t As New TaskResult(WF.InitialStep.ID, CoreData.GetNewID(IdTypes.Tasks), Result.ID, Result.DocType, Result.Name, Result.IconId, 0, Zamba.Core.TaskStates.Desasignada, Result.Indexs, WF.InitialStep.InitialState)
                    t.CheckIn = Now
                    TasksResults.Add(t)
                    Dim wfstep As IWFStep
                    wfstep = WF.Steps.Item(Convert.ToDecimal(t.StepId))
                    WFTasksFactory.LogCheckInOut(t.ID, t.Name, t.DocTypeId, t.DocType.Name, wfstep.ID, t.State.Name, t.WorkId, t.CheckIn, wfstep.Name, WF.Name, Nothing, Membership.MembershipHelper.CurrentUser.Name, "Ingreso de Tarea")
                Next
                WFTasksFactory.InsertTasks(TasksResults, WfId, WF.Name, WF.InitialStep.ID, WF.InitialStep.InitialState.ID, Nothing)
            End If
        Next
    End Sub
#End Region

#Region "WebParts"

    Public Shared Function GetTasksAverageTimeInSteps(ByVal workflowid As Int64) As Hashtable
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
    Public Shared Function GetTasksAverageTimeByStep(ByVal stepid As Int64) As Hashtable

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

    Public Shared Function GetTasksToExpireGroupByStep(ByVal workflowid As Int64, ByVal FromHours As Int32, ByVal ToHours As Int32) As DataSet
        Dim parvalues() As Object = {workflowid, FromHours, ToHours}
        Return Server.Con.ExecuteDataset("sp_GetTaskToExpireGroupByStep", parvalues)
    End Function
    Public Shared Function GetExpiredTasksGroupByUser(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_GetExpiredTasksGroupByUser", parvalues)
    End Function

    Public Shared Function GetExpiredTasksGroupByStep(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_GetExpiredTasksGroupByStep", parvalues)
    End Function

    Public Shared Function GetTasksToExpireGroupByUser(ByVal workflowid As Int64, ByVal FromHours As Int32, ByVal ToHours As Int32) As DataSet
        Dim parvalues() As Object = {workflowid, FromHours, ToHours}
        Return Server.Con.ExecuteDataset("sp_GetTaskToExpireGroupByUser", parvalues)
    End Function

    Public Shared Function GetTasksBalanceGroupByWorkflow(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_GetTasksBalanceByWorkflow", parvalues)
    End Function

    Public Shared Function GetTasksBalanceGroupByStep(ByVal stepid As Int64) As DataSet
        Dim parvalues() As Object = {stepid}
        Return Server.Con.ExecuteDataset("sp_GetTasksBalanceByStep", parvalues)
    End Function

    Public Shared Function GetAsignedTasksCountsGroupByUser(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_AsignedTasksCountsGroupByUser", parvalues)
    End Function

    Public Shared Function GetAsignedTasksCountsGroupByStep(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_AsignedTasksCountsGroupByStep", parvalues)
    End Function

    Public Shared Function GetTaskConsumedMinutesByWorkflowGroupByUsers(ByVal workflowid As Int64) As DataSet
        Dim parvalues() As Object = {workflowid}
        Return Server.Con.ExecuteDataset("sp_Sarasa", parvalues)
    End Function

    Public Shared Function GetTaskConsumedMinutesByStepGroupByUsers(ByVal stepid As Int64) As DataSet
        Dim parvalues() As Object = {stepid}
        Return Server.Con.ExecuteDataset("sp_Sarasa", parvalues)
    End Function

#End Region

    Public Shared Function GetTaskHistoryByResultId(ByVal TaskId As Integer, ByVal filters As String) As DataSet

        'Esta echo solo para Oracle, tengo que hacer lo mismo para SQL y para el otro metodo "GetOnlyIndexsHistory"
        Dim Query As String = String.Empty

        If Server.isOracle Then
            Query = "SELECT Fecha AS Fecha, " &
                    "Step_Name AS Etapa, " &
                    "State AS Estado, " &
                    "UserName AS Usuario, " &
                    "Accion AS Accion, " &
                    "Doc_Name AS Tarea, " &
                    "Doc_Type_Name AS ""Tipo Documento"" " &
                    "FROM (WFStepHst) " &
                    "WHERE Doc_Id = " & TaskId.ToString() &
                    " ORDER BY fecha DESC"

            If Not String.IsNullOrEmpty(filters) Then
                Query = "SELECT Fecha, Etapa, Estado, Usuario, Accion, Tarea, ""Tipo Documento"" FROM (" & Query & ") t WHERE " &
                        filters.Replace("[", "").Replace("]", "")
            End If

        Else
            Query = "SELECT Fecha, Etapa, Estado, Usuario, Accion, Tarea, [Tipo Documento] FROM Zvw_WFHistory_200 WHERE DOC_ID = " &
                    TaskId.ToString() &
                    If(Not String.IsNullOrEmpty(filters), " AND " & filters, "") &
                    " order by fecha desc"
        End If

        Return Server.Con.ExecuteDataset(CommandType.Text, Query)

    End Function
    Public Shared Function GetOnlyIndexsHistory(ByVal DocID As Integer) As DataSet

        Dim Query As String = String.Empty

        If Server.isOracle Then
            Query = "SELECT  Action_Date AS Fecha, S_Object_ID As En FROM User_Hst WHERE object_id = " & DocID & " And object_type_id = 6 And action_type = 12 order by action_id desc"
        Else
            Query = "Select  Action_Date As Fecha, S_Object_ID AS En FROM User_Hst WHERE object_id = " & DocID & " and object_type_id = 6 And action_type = 12 order by action_id desc"
        End If

        Return Server.Con.ExecuteDataset(CommandType.Text, Query)

    End Function

    ''' <summary>
    ''' Devuelva la ultima fecha de modificacion de la tarea
    ''' </summary>
    ''' <param name="TaskId">Id de la tarea</param>
    ''' <history>Marcelo created 26/02/09</history>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetLastModifiedTaskHistoryByResultId(ByVal TaskId As Integer) As DateTime
        Dim lastModDate As Nullable(Of DateTime)
        If Server.isOracle Then
            Dim Query As String = "SELECT  max(Fecha) AS " & Chr(34) & "Fecha" & Chr(34) & " FROM(WFStepHst) WHERE Doc_Id = " & TaskId.ToString()
            lastModDate = Server.Con.ExecuteScalar(CommandType.Text, Query)
        Else
            Dim Query As String = "SELECT max(Fecha) as Fecha FROM Zvw_WFHistory_200 WHERE DOC_ID = " & TaskId.ToString()
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
    Public Shared Function GetTaskIdByDocId(ByVal docId As Int64) As DataSet
        Dim QueryBuilder As New StringBuilder
        Try
            QueryBuilder.Append("select task_id from wfdocument WHERE doc_id = ")
            QueryBuilder.Append(docId.ToString())

            Return Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
        Finally
            QueryBuilder.Remove(0, QueryBuilder.Length)
            QueryBuilder = Nothing
        End Try
    End Function

    Public Shared Function GetTaskAprobementsHistoryByTaskId(ByVal TaskId As Int64) As DataSet
        Dim Query As New StringBuilder
        'Usa la tabla ZAPROB
        If Server.isOracle Then
            Query.Append("SELECT USRTABLE.APELLIDO || ',' || USRTABLE.NOMBRES AS " & Chr(34) & "Usuario" & Chr(34) & " ,")
            Query.Append("ZTaskStatus.TaskStatus as " & Chr(34) & "Estado Actual" & Chr(34) & " FROM ZAPROB ")
            Query.Append("INNER JOIN USRTABLE ON ZAPROB.IDU = USRTABLE.ID ")
            Query.Append("INNER JOIN ZTaskStatus ON ZAPROB.OK = ZTaskStatus.StatusId")
            Query.Append(" WHERE IDT =")
            Query.Append(TaskId.ToString)
        Else
            Query.Append("SELECT USRTABLE.APELLIDO + ',' + USRTABLE.NOMBRES AS 'Usuario' ,")
            Query.Append("ZTaskStatus.TaskStatus as 'Estado Actual' FROM ZAPROB ")
            Query.Append("INNER JOIN USRTABLE ON ZAPROB.IDU = USRTABLE.ID ")
            Query.Append("INNER JOIN ZTaskStatus ON ZAPROB.OK = ZTaskStatus.StatusId")
            Query.Append(" WHERE IDT =")
            Query.Append(TaskId.ToString)
        End If
        Return Server.Con.ExecuteDataset(CommandType.Text, Query.ToString())

    End Function

    '''' <summary>
    '''' Método que sirve para obtener un dataset que tendrá el historial de los task id seleccionados. Sobre los 
    '''' task id seleccionados se debio haber ejecutado la regla RequestAction. Los task id que no ejecutaron la 
    '''' regla no se mostrarán
    '''' </summary>
    '''' <param name="taskIds"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    '''' <history>   
    ''''     [Gaston]    22/07/2008  Created
    ''''                 30/07/2008  Modified
    ''''                 26/09/2008  Correción de consulta Oracle
    '''' </history>


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
    Public Shared Function getTasksRequestActionHistory(ByVal taskId As Int64) As DataSet

        Dim strBuilder As New StringBuilder
        Dim counter As Integer = 0

        ' Si el servidor es SQL
        If (Server.isSQLServer) Then

            strBuilder.Append("SELECT * FROM ZView_RequestAction WHERE ")
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
            strBuilder.Append("WHERE RequestActionTasks.taskid = " & taskId)

        End If

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strBuilder.ToString())
        ds.Tables(0).Columns.Remove(ds.Tables(0).Columns("TaskId"))
        Return (ds)

    End Function

    ''' <summary>
    ''' Obtiene un listado de ids de tareas que se encuentran en una etapa deseada.
    ''' Los ids se encuentran ordenados de menor a mayor.
    ''' </summary>
    ''' <param name="stepId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTaskIdsByStepId(stepId As Int64) As DataTable
        If Server.isSQLServer Then
            Return Server.Con.ExecuteDataset("ZSP_WORKFLOW_100_GetTaskIdsByStepId", New Object() {stepId}).Tables(0)
        Else
            Dim query As String = "SELECT TASK_ID FROM WFDOCUMENT WHERE STEP_ID=" & stepId & " ORDER BY TASK_ID"
            Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
        End If
    End Function

#Region "Save"
    Public Shared Sub SaveIntoIP_Task(ByVal alFiles As String, ByVal IPTASKID As Int64, ByVal ZipOrigen As String, ByVal conf_Id As Decimal)
        Dim strinsert As String = "INSERT INTO IP_Task (Id,Id_Configuracion, File_Path, Zip_Origen) VALUES(" _
        & IPTASKID & "," & conf_Id & ", '" & alFiles & "', '" & ZipOrigen & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    End Sub

    Public Shared Sub SaveIntoIP_Task(ByVal alFiles As String, ByVal IPTASKID As Int64, ByVal conf_Id As Decimal)
        Dim strinsert As String = "INSERT INTO IP_Task (Id,Id_Configuracion, File_Path, Zip_Origen,Bloqueado,Maquina) VALUES(" _
        & IPTASKID & "," & conf_Id & ", '" & alFiles & "', '', 0, '" & Environment.MachineName & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    End Sub
#End Region
    Public Overrides Sub Dispose()

    End Sub

End Class
