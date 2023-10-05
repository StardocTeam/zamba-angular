Imports Zamba.Filters
Imports Zamba.Core.Enumerators
Imports Zamba.Data
Imports System.Collections.Generic
Imports Zamba.Servers
Imports System.Text
Imports Zamba.Membership
Imports System.IO
Imports System.Linq

Namespace WF.WF
    Public Class WFTaskBusiness
        Implements IWFTaskBusiness
        'Implements DoGenerateTasK
        Dim RF As New Results_Factory
        Dim UB As New UserBusiness
        Dim WTF As New WFTasksFactory
        Dim RiB As New RightsBusiness



#Region "Get"

        Public Function GetTasksNamesByTaskIds(ByVal taskIds As List(Of Int64)) As DataTable

            Dim dsDocs As DataTable = WTF.GetTasksNamesByTaskIds(taskIds)

            Return dsDocs

        End Function
        ''' <summary>
        ''' Este metodo se usa para instanciar un taskresult completo para la ejecucion de las tareas en el cliente de Zamba
        ''' </summary>
        ''' <param name="r"></param>
        ''' <param name="s"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NewResult(ByVal r As DataRow, ByVal WFStepId As Int64, Optional ByVal inThread As Boolean = False) As TaskResult
            Dim ASB As New AutoSubstitutionBusiness
            Dim DTB As New DocTypesBusiness
            Try

                Dim Doctype As DocType = DTB.GetDocType(CInt(r.Item("DOC_TYPE_ID")))


                Dim Task_State_ID As Int32
                If Not IsNothing(Doctype) Then
                    Task_State_ID = CInt(r.Item("Task_State_ID"))

                    Dim ResultName As String = r.Item("Name")

                    If IsNothing(ResultName) Then
                        ResultName = Doctype.Name
                    End If
                    Dim Result As TaskResult
                    If Server.isOracle Then

                        Result = New TaskResult(WFStepId, CLng(r.Item("Task_ID")), CLng(r.Item("Doc_ID")), Doctype, ResultName, CInt(r.Item("IconId")), CInt(r.Item("C_Exclusive")), DirectCast(Task_State_ID, Zamba.Core.TaskStates), Doctype.Indexs, WFStepStatesComponent.GetStepStateById(WFStepStatesBusiness.getInitialState(WFStepId)))
                    Else
                        Result = New TaskResult(WFStepId, CLng(r.Item("Task_ID")), CLng(r.Item("Doc_ID")), Doctype, ResultName, CInt(r.Item("IconId")), CInt(r.Item("Exclusive")), DirectCast(Task_State_ID, Zamba.Core.TaskStates), Doctype.Indexs, WFStepStatesComponent.GetStepStateById(WFStepStatesBusiness.getInitialState(WFStepId)))
                    End If

                    If Not IsDBNull(r("IsImportant")) Then
                        Result.IsImportant = CBool(r.Item("IsImportant"))
                    End If

                    If Not IsDBNull(r("IsFavorite")) Then
                        Result.IsFavorite = CBool(r.Item("IsFavorite"))
                    End If
                    If Not IsDBNull(r("CheckIn")) Then
                        Result.CheckIn = r.Item("CheckIn")
                    End If

                    If Not IsDBNull(r("Work_ID")) Then
                        Result.WorkId = r.Item("Work_ID")
                    End If

                    If Not IsDBNull(r("ExpireDate")) Then
                        Result.ExpireDate = r.Item("ExpireDate")
                    End If

                    If Not IsDBNull(r("Date_Asigned_By")) Then
                        Result.AsignedDate = r.Item("Date_Asigned_By")
                    End If
                    If Not IsDBNull(r("Username_Asigned")) Then
                        Result.Username_Asigned = r.Item("Username_Asigned")
                    End If

                    If Not IsDBNull(r("Disk_group_ID")) Then
                        Result.Disk_Group_Id = r.Item("Disk_group_ID")
                    End If

                    If Not IsDBNull(r("DISK_VOL_PATH")) Then
                        Result.DISK_VOL_PATH = r.Item("DISK_VOL_PATH")
                    End If

                    If Not IsDBNull(r("Doc_File")) Then
                        Result.Doc_File = r.Item("Doc_File")
                    End If

                    If Not IsDBNull(r("OffSet")) Then
                        Result.OffSet = r.Item("OffSet")
                    End If

                    If Not IsDBNull(r.Item("User_Asigned")) Then
                        Result.AsignedToId = CType(r.Item("User_Asigned"), Int64)
                    Else
                        Result.AsignedToId = 0
                    End If

                    If Not IsDBNull(r.Item("User_Asigned_By")) Then
                        Result.AsignedById = CType(r.Item("User_Asigned_By"), Int64)
                    Else
                        Result.AsignedById = 0
                    End If

                    Try
                        Dim WFStepStateId As Int32 = Int32.Parse(r.Item("Do_State_ID").ToString())
                        If Result.StepId = 0 Then
                            Result.StepId = WFStepId
                        End If

                        If (r.Table.Columns.Contains("STATE")) AndAlso Not IsDBNull(r.Item("STATE")) Then
                            Result.StateId = Int64.Parse(r.Item("DO_STATE_ID").ToString)
                            Result.State = WFStepStatesComponent.GetStepStateById(Int64.Parse(r.Item("DO_STATE_ID").ToString))
                        Else
                            Result.StateId = WFStepStateId
                            Result.State = WFStepStatesComponent.GetStepStateById(WFStepStateId)
                        End If

                        If (Result.State Is Nothing) Then
                            Result.State = WFStepStatesComponent.GetStepStateById(WFStepStatesBusiness.getInitialState(Result.StepId))
                            WTF.UpdateState(Result)
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try

                    If IsNothing(Result.Parent) Then
                        FillIndexsAndDocType(Result)
                    Else
                        Dim i As Int16
                        For i = 0 To Result.Indexs.Count - 1
                            Try
                                If Not IsDBNull(r("I" & DirectCast(Result.Indexs(i), Index).ID)) Then
                                    DirectCast(Result.Indexs(i), Index).Data = r("I" & DirectCast(Result.Indexs(i), Index).ID).ToString
                                    DirectCast(Result.Indexs(i), Index).DataTemp = r("I" & DirectCast(Result.Indexs(i), Index).ID).ToString
                                    'Si el indice es de tipo Sustitucion
                                    If DirectCast(Result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustitución OrElse DirectCast(Result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        'Se carga la descripcion de Indice
                                        DirectCast(Result.Indexs(i), Index).dataDescription = ASB.getDescription(DirectCast(Result.Indexs(i), Index).Data, DirectCast(Result.Indexs(i), Index).ID)
                                        DirectCast(Result.Indexs(i), Index).dataDescriptionTemp = DirectCast(Result.Indexs(i), Index).dataDescription
                                    End If
                                Else
                                    DirectCast(Result.Indexs(i), Index).Data = String.Empty
                                    DirectCast(Result.Indexs(i), Index).DataTemp = String.Empty
                                End If
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try
                        Next

                    End If

                    Return Result
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return Nothing
            Finally
                DTB = Nothing
                ASB = Nothing
            End Try
        End Function

        ''' <summary>
        ''' Método que sirve para castear el tipo de un índice a Type
        ''' </summary>
        ''' <param name="iType"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]    21/08/2008  Created    Código tomado de UCFusion2
        ''' </history>
        Public Shared Function GetIndexType(ByVal iType As IndexDataType) As Type
            Dim indexType As Type
            Select Case iType
                Case IndexDataType.Alfanumerico
                    indexType = GetType(String)
                Case IndexDataType.Alfanumerico_Largo
                    indexType = GetType(String)
                Case IndexDataType.Fecha
                    indexType = GetType(Date)
                Case IndexDataType.Fecha_Hora
                    indexType = GetType(DateTime)
                Case IndexDataType.Moneda
                    indexType = GetType(Decimal)
                Case IndexDataType.None
                    indexType = GetType(String)
                Case IndexDataType.Numerico
                    indexType = GetType(Int64)
                Case IndexDataType.Numerico_Decimales
                    indexType = GetType(Decimal)
                Case IndexDataType.Numerico_Largo
                    indexType = GetType(Decimal)
                Case IndexDataType.Si_No
                    indexType = GetType(String)
                Case Else
                    indexType = GetType(String)
            End Select

            Return indexType
        End Function

        ''' <summary>
        ''' Deuvelve las tareas a partir de la etapa
        ''' </summary>
        ''' <param name="stepId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Ezequiel] 14/09/09 - Modified. - Se modifico la manera de obtener las tareas.
        ''' </history>
        Public Function GetTasksByStepId(ByVal stepId As Int64, ByVal WithRights As Boolean, ByVal CurrentUserId As Int64, ByVal LastDocTypeId As Int64, ByVal PageSize As Int32) As List(Of ITaskResult)
            Dim Tasks As New List(Of ITaskResult)

            Dim DtTasks As DataTable = GetTasksByStepIdDataTable(stepId, WithRights, CurrentUserId, LastDocTypeId, PageSize)

            If Not IsNothing(DtTasks) Then
                'Dim WFSB As New WFStepBusiness

                'Dim Wfstep As IWFStep = WFSB.GetStepById(stepId)
                'WFSB = Nothing

                For Each CurrentRow As DataRow In DtTasks.Rows
                    'se cambia el metodo Builtask por el metodo NewResult, que es el que se viene utilizando en el cliente de zamba windows
                    Dim task As TaskResult = NewResult(CurrentRow, stepId)
                    Tasks.Add(task)
                Next
            End If

            Return Tasks
        End Function

        Public Function GetUserOpenedTasks(ByVal usrID As Int64) As DataTable
            Return WTF.GetUserOpenedTasks(usrID)
        End Function

        ''' <summary>
        ''' Deuvelve las tareas a partir de la etapa
        ''' </summary>
        ''' <param name="stepId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Ezequiel] 14/09/09 - Modified. - Se modifico la manera de obtener las tareas.
        ''' </history>
        Public Function GetTasksByStepIdDataTable(ByVal stepId As Int64, ByVal WithRights As Boolean, ByVal CurrentUserId As Int64, ByVal LastDocId As Int64, ByVal PageSize As Int32) As DataTable

            Dim DTList As List(Of Int64) = WFStepBusiness.GetDocTypesAssociatedToWFbyStepId(stepId)
            Dim DtTasks As New DataTable

            Dim count As Long


            For Each docid As Int64 In DTList
                DtTasks.Merge(GetTasksByStepandDocTypeId(stepId, docid, WithRights, CurrentUserId, Nothing, LastDocId, PageSize, count))
            Next



            Return DtTasks
        End Function

        ''' <summary>
        ''' Devuelve las tareas del entidad y etapa que se le pasan por parametro
        ''' </summary>
        ''' <param name="stepId">Id de la etapa</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''        [Ezequiel] 14/09/09 - Created.
        '''        [Javier]   06/10/10 - Modified.  Se agrega la obtención de las restricciones sobre los doctypeid.
        ''' </history>
        Public Function GetTasksByStepandDocTypeId(ByVal stepId As Int64, ByVal DocTypeId As Int64, ByVal WithGridRights As Boolean, ByVal CurrentUserID As Int64,
                                                          ByRef FC As IFiltersComponent, ByVal LastDocId As Int64, ByVal PageSize As Int32,
                                                          ByRef totalCount As Long, Optional ByVal isCount As Boolean = False) As DataTable
            Dim CheckInColumnIsShortDate As Boolean
            Dim FilterString As String

            If (Not FC Is Nothing) Then
                Dim Filters As New Generic.List(Of IFilterElem)
                Filters = FC.GetLastUsedFilters(DocTypeId, CurrentUserID, True)

                If (Not Filters Is Nothing) Then 'Se agrega validacion, si valor esta vacio "()" se elimina filtro
                    For i As Integer = Filters.Count - 1 To 0 Step -1
                        If Filters(i).Value = "()" Then Filters.Remove(Filters(i))
                    Next

                    If Filters.Count Then FilterString = FC.GetFiltersString(Filters)
                End If

            End If
            Dim UP As New UserPreferences

            '(pablo) ZUserConfig Option - Converts the DateTime type column into a Date Column
            If Boolean.Parse(UP.getValue("CheckInColumnShortDateFormat", UPSections.UserPreferences, True, CurrentUserID)) Then
                CheckInColumnIsShortDate = True
            End If

            'Obtencion del string de restricciones
            Dim ColumCondstring As New StringBuilder
            Dim dateDeclarationString As New StringBuilder

            Dim UB As New UserBusiness
            getRestrictionString(ColumCondstring, dateDeclarationString, DocTypeId, CurrentUserID)

            Dim HabilitarFavoritos As Boolean = UP.getValue("HabilitarFavoritos", UPSections.UserPreferences, False, CurrentUserID)


            Dim order As String = UP.getValue("TaskGridOrder", UPSections.UserPreferences, "Desc", CurrentUserID)
            Dim indexs As New List(Of IIndex)
            Dim dt As DataTable
            Dim DTB As New DocTypesBusiness
            '[Ezequiel] Se valida por este bolean ya que en el servicio no se precisan validar permisos de atributos pero en el cliente si.
            If WithGridRights Then
                '[Ezequiel] Lista que guarda los indices a incluir en cada tarea.
                Dim indexsaux As New List(Of IIndex)
                '[Ezequiel] Lista que guarda los indices de autosustitucion
                Dim autosustindex As New List(Of IIndex)
                '[Ezequiel] Valido si la opcion del userconfig esta activa para ver indices en grilla.
                If CBool(UP.getValue("ShowIndexsOnGrid", UPSections.UserPreferences, "True", CurrentUserID)) Then

                    indexs.AddRange(DTB.GetDocType(DocTypeId).Indexs)

                    '[Ezequiel] valido el permiso de ver indices en grilla de tareas.
                    For Each indice As IIndex In indexs
                        If UB.GetIndexRightValue(DocTypeId, indice.ID, CurrentUserID, RightsType.TaskGridIndexView) Then
                            indexsaux.Add(indice)
                            If indice.DropDown = IndexAdditionalType.AutoSustitución OrElse indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                autosustindex.Add(indice)
                            End If
                        End If
                    Next
                End If


                'Busco los indices referenciales de la entidad.
                Dim refIndexs As List(Of ReferenceIndex) = (New ReferenceIndexBusiness).GetReferenceIndexesByDoctypeId(DocTypeId)

                dt = WTF.GetTasksByStepandDocTypeId(stepId, DocTypeId, indexsaux, WithGridRights, FilterString, ColumCondstring.ToString(),
                                                    dateDeclarationString.ToString(), LastDocId, PageSize, CheckInColumnIsShortDate,
                                                    totalCount, order, RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,
                                                    ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosAOtros, stepId),
                                                    RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps,
                                                    Zamba.Core.RightsType.VerAsignadosANadie, stepId), HabilitarFavoritos, autosustindex, refIndexs)
            Else

                'Busco los indices referenciales de la entidad.
                Dim refIndexs As List(Of ReferenceIndex) = (New ReferenceIndexBusiness).GetReferenceIndexesByDoctypeId(DocTypeId)

                indexs.AddRange(DTB.GetDocType(DocTypeId).Indexs)
                dt = WTF.GetTasksByStepandDocTypeId(stepId, DocTypeId, indexs, WithGridRights, FilterString, ColumCondstring.ToString(),
                                                    dateDeclarationString.ToString(), LastDocId, PageSize, CheckInColumnIsShortDate,
                                                    totalCount, order, RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,
                                                    ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosAOtros, stepId),
                                                    RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,
                                                    ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosANadie, stepId),
                                                    HabilitarFavoritos, Nothing, refIndexs)
            End If
            DTB = Nothing
            If isCount = True Then
                Return dt
            Else
                If UP.getValue("TaskGridOrder", UPSections.UserPreferences, "Asc", CurrentUserID).ToLower() = "desc" Then
                    dt.DefaultView.Sort = "doc_id desc"
                    Return dt.DefaultView.ToTable()
                Else
                    Return dt
                End If
            End If
            UP = Nothing

        End Function

        ''' <summary>
        ''' Devuelve un count de las tareas del entidad y etapa que se le pasan por parametro
        ''' </summary>
        ''' <param name="stepId">Id de la etapa</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''        [Javier] 29/12/11 - Created.
        ''' </history>
        Public Function GetTasksCountByStepandDocTypeId(ByVal stepId As Int64, ByVal DocTypeId As Int64, ByVal WithGridRights As Boolean, ByVal CurrentUserID As Int64, ByRef FC As IFiltersComponent) As Long
            Dim ColumCondstring As New StringBuilder
            Dim dateDeclarationString As New StringBuilder
            Dim UserGroupBusiness As New UserGroupBusiness
            Dim UB As New UserBusiness
            getRestrictionString(ColumCondstring, dateDeclarationString, DocTypeId, CurrentUserID)

            UB = Nothing
            Dim VerAsignadosAOtros As Boolean
            Dim VerAsignadosANadie As Boolean

            If CurrentUserID > 0 Then
                VerAsignadosAOtros = RiB.GetUserRights(CurrentUserID, ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosAOtros, stepId)
                VerAsignadosANadie = RiB.GetUserRights(CurrentUserID, ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosANadie, stepId)
            Else
                VerAsignadosAOtros = RiB.GetUserRights(CurrentUserID, ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosAOtros, stepId)
                VerAsignadosANadie = RiB.GetUserRights(CurrentUserID, ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosANadie, stepId)
            End If

            Return WTF.GetTasksCountByStepandDocTypeId(stepId, DocTypeId, WithGridRights, String.Empty, ColumCondstring.ToString(), VerAsignadosAOtros, VerAsignadosANadie, CurrentUserID, UserGroupBusiness.getUserGroups(CurrentUserID))
        End Function

        ''' <summary>
        '''  Obtiene el string de restricciones
        ''' </summary>
        ''' <param name="ColumCondstring"></param>
        ''' <param name="dateDeclarationString"></param>
        ''' <param name="DocTypeId"></param>
        ''' <param name="CurrentUserID"></param>
        ''' <remarks></remarks>
        ''' <history>        
        '''        [Javier]   06/10/10  Created.
        ''' </history>
        Private Sub getRestrictionString(ByRef ColumCondstring As StringBuilder, ByRef dateDeclarationString As StringBuilder, ByVal DocTypeId As Int64, ByVal CurrentUserID As Int64)
            Dim Valuestring As New System.Text.StringBuilder
            Dim indRestriction As Generic.List(Of IIndex)
            Dim ZOPTB As New ZOptBusiness
            Dim dinamycRestriciton As String = ZOPTB.GetValue("UseDinamycRestriccions")

            'SI se usa cache y no se deben recargar las retricciones
            Dim RF As New RestrictionsMapper_Factory
            If String.IsNullOrEmpty(dinamycRestriciton) OrElse Not Boolean.Parse(dinamycRestriciton) Then
                Dim key As String = DocTypeId & "-" & CurrentUserID
                If Cache.RestrictionsStrings.GetInstance().hsRestrictionsStrings.ContainsKey(key) = False Then
                    SyncLock Cache.RestrictionsStrings.GetInstance().hsRestrictionsStrings
                        If Cache.RestrictionsStrings.GetInstance().hsRestrictionsStrings.ContainsKey(key) = False Then
                            Cache.RestrictionsStrings.GetInstance().hsRestrictionsStrings.Add(key, RF.GetRestrictionIndexs(CurrentUserID, DocTypeId))
                            indRestriction = Cache.RestrictionsStrings.GetInstance().hsRestrictionsStrings(key)
                        End If
                    End SyncLock
                ElseIf Cache.RestrictionsStrings.GetInstance().hsRestrictionsStrings(key) Is Nothing Then
                    SyncLock Cache.RestrictionsStrings.GetInstance().hsRestrictionsStrings
                        If Cache.RestrictionsStrings.GetInstance().hsRestrictionsStrings(key) Is Nothing Then
                            Cache.RestrictionsStrings.GetInstance().hsRestrictionsStrings(key) = RF.GetRestrictionIndexs(CurrentUserID, DocTypeId)
                            indRestriction = Cache.RestrictionsStrings.GetInstance().hsRestrictionsStrings(key)
                        End If
                    End SyncLock
                Else
                    indRestriction = Cache.RestrictionsStrings.GetInstance().hsRestrictionsStrings(key)
                End If
            Else
                indRestriction = RF.GetRestrictionIndexs(CurrentUserID, DocTypeId)
            End If
            RF = Nothing

            If Not indRestriction Is Nothing Then
                Dim UP As New UserPreferences
                Dim ZOPT As New ZOptBusiness
                Dim FlagCase As Boolean = Boolean.Parse(ZOPT.GetValue("CaseSensitive"))
                UP = Nothing
                For i As Int32 = 0 To indRestriction.Count - 1
                    CreateTasksRestrictionsWhere(Valuestring, indRestriction, i, FlagCase, ColumCondstring, True, dateDeclarationString)
                Next
            End If
            ZOPTB = Nothing
        End Sub

        Public Function CountFilesInIP_Task(ByVal conf_Id As Decimal) As Integer
            Return WTF.CountFilesInIP_Task(conf_Id)
        End Function

        ''' <summary>
        ''' Get a Task by Taskid and doctype id
        ''' </summary>
        ''' <param name="taskId"></param>
        ''' <param name="DocTypeId"></param>
        ''' <param name="PageSize"></param>
        ''' <returns></returns>
        ''' <history>
        '''     Javier  06/12/2010  Modified    GetTaskByTaskIdAndDocTypeIdAndStepId is now a instance method
        ''' </history>
        Public Function GetTaskByTaskIdAndDocTypeId(ByVal taskId As Int64, ByVal DocTypeId As Int64, ByVal PageSize As Int32) As ITaskResult
            Dim WFSTepId As Int64 = WFStepBusiness.GetStepIdByTaskId(taskId)

            Return GetTaskByTaskIdAndDocTypeIdAndStepId(taskId, DocTypeId, WFSTepId, PageSize)
        End Function

        ''' <summary>
        ''' Obtiene tareas por Taskid, DocType y StepId
        ''' </summary>
        ''' <param name="taskId"></param>
        ''' <param name="DocTypeId"></param>
        ''' <param name="WFStepId"></param>
        ''' <param name="PageSize"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>        
        '''                             Created
        '''         Javier  06/10/10    Modified    Se agregan sentencias para obtener los strings de restricciones sobre docs
        '''         Javier  06/12/2010  Modified    GetTaskByTaskIdAndDocTypeIdAndStepId is now a instance method
        ''' </history>
        Public Function GetTaskByTaskIdAndDocTypeIdAndStepId(ByVal taskId As Int64, ByVal DocTypeId As Int64, ByVal WFStepId As Int64, ByVal PageSize As Int32) As ITaskResult Implements IWFTaskBusiness.GetTaskByTaskIdAndDocTypeIdAndStepId

            Dim Task As ITaskResult = Nothing

            Dim indexs As New List(Of IIndex)
            Dim dt As DataTable

            'Obtencion del string de restricciones
            Dim ColumCondstring As New StringBuilder
            Dim dateDeclarationString As New StringBuilder
            getRestrictionString(ColumCondstring, dateDeclarationString, DocTypeId, Zamba.Membership.MembershipHelper.CurrentUser.ID)

            Dim DTB As New DocTypesBusiness
            indexs.AddRange(DTB.GetDocType(DocTypeId).Indexs)
            DTB = Nothing

            'Busco los indices referenciales de la entidad.
            Dim refIndexs As List(Of ReferenceIndex) = (New ReferenceIndexBusiness).GetReferenceIndexesByDoctypeId(DocTypeId)

            dt = WTF.GetTaskByTaskIdAndDocTypeId(taskId, WFStepId, DocTypeId, indexs, False, String.Empty, ColumCondstring.ToString, dateDeclarationString.ToString, FilterTypes.Task, True, Nothing, refIndexs)

            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                '                Dim WFSB As New WFStepBusiness

                Task = NewResult(dt.Rows(0), WFStepId)
                'WFSB = Nothing
            End If
            Return Task
        End Function

        Public Function GetStepIDByTaskId(ByVal taskId As Int64) As Int64 Implements IWFTaskBusiness.GetStepIDByTaskId
            Return WFStepBusiness.GetStepIdByTaskId(taskId)
        End Function

        Public Function GetStepIdDocTypeIdByTaskId(ByVal taskId As Int64) As DataSet Implements IWFTaskBusiness.GetStepIdDocTypeIdByTaskId
            Dim WF As New WFTasksFactory
            Return WF.GetStepIdDocTypeIdByTaskId(taskId)
            WF = Nothing
        End Function


        ''' <summary>
        '''     Obtiene tareas por Taskid, DocType y StepId
        ''' </summary>
        ''' <param name="taskIds"></param>
        ''' <param name="DocTypeId"></param>
        ''' <param name="WFStepId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>        
        '''                             Created.
        '''        [Javier]   06/10/10  Modified    Se agregan sentencias para obtener los strings de restricciones sobre docs
        '''        [Javier P.]08/07/12 Modified    Se agrega parámetro opcional de usuario para compatibilidad con WS.
        ''' </history>
        Public Function GetTasksByTaskIdsAndDocTypeIdAndStepId(ByVal taskIds As List(Of Int64), ByVal DocTypeId As Int64, ByVal WFStepId As Int64, SearchType As SearchType, Optional ByVal usrID As Int64 = 0) As ITaskResult
            Dim indexs As New List(Of IIndex)
            Dim dt As DataTable

            'Obtencion del string de restricciones
            Dim ColumCondstring As New StringBuilder
            Dim dateDeclarationString As New StringBuilder

            'Si usrID es 0 usar el curren user, sino usar el parametro
            If usrID = 0 Then
                getRestrictionString(ColumCondstring, dateDeclarationString, DocTypeId, MembershipHelper.CurrentUser.ID)
            Else

                Dim UB As New UserBusiness
                getRestrictionString(ColumCondstring, dateDeclarationString, DocTypeId, usrID)
                UB = Nothing
            End If

            Dim IB As New IndexsBusiness
            indexs.AddRange(IB.GetIndexsSchemaAsListOfDT(DocTypeId))
            IB = Nothing

            Dim refIndexs As List(Of ReferenceIndex) = (New ReferenceIndexBusiness).GetReferenceIndexesByDoctypeId(DocTypeId)

            dt = WTF.GetTasksByTasksIdAndDocTypeId(taskIds, DocTypeId, indexs, False, String.Empty, ColumCondstring.ToString, dateDeclarationString.ToString, SearchType, True, refIndexs)

            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                Return NewResult(dt.Rows(0), WFStepId)
            End If
        End Function

        ''' <summary>
        ''' Método que sirve para obtener un task en base a un stepId
        ''' </summary>
        ''' <param name="docId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        Public Function GetTaskByDocIdAndStepIdAAndDocTypeId(ByVal docId As Int64, ByVal stepId As Int64, ByVal DocTypeId As Int64, ByVal PageSize As Int32) As ITaskResult

            Dim ds As DataSet = WTF.GetTaskIdByDocIdAndDocTypeId(docId, DocTypeId)

            If (ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0) Then
                Return GetTaskByTaskIdAndDocTypeIdAndStepId(ds.Tables(0).Rows(0)("tak_id"), DocTypeId, stepId, PageSize)
            Else
                Return (Nothing)
            End If

        End Function

        Public Function GetTaskByDocIdAndWorkFlowId(ByVal docId As Int64, ByVal PageSize As Int32) As ITaskResult
            Dim ds As DataSet = WTF.GetTaskIdsStepsIdsDocTypesIdsByDocId(docId)

            If Not IsNothing(ds) AndAlso ds.Tables.Count = 1 AndAlso ds.Tables(0).Rows.Count > 0 Then

                Return GetTaskByTaskIdAndDocTypeIdAndStepId(ds.Tables(0).Rows(0)("task_id"), ds.Tables(0).Rows(0)("doc_type_id"), ds.Tables(0).Rows(0)("step_id"), PageSize)
            Else

                Return Nothing

            End If
        End Function


        Public Function GetAllTasksByDocId(ByVal docId As Int64, ByVal doctypeid As Int64, ByVal PageSize As Int32) As Generic.List(Of ITaskResult)
            Dim dtTaskIds As DataSet = WTF.GetTaskIdsByDocId(docId)
            Dim AssociatedTask As New Generic.List(Of ITaskResult)

            For Each id As DataRow In dtTaskIds.Tables(0).Rows
                AssociatedTask.Add(GetTaskByTaskIdAndDocTypeId(id("task_id"), doctypeid, PageSize))
            Next

            dtTaskIds.Dispose()
            dtTaskIds = Nothing

            Return AssociatedTask
        End Function

        ''' <summary>
        ''' Método que sirve para obtener un task en base a un docId
        ''' </summary>
        ''' <param name="docId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTaskIDsByDocId(ByVal docId As Int64) As List(Of Int64)
            Dim dsTaskIds As DataSet = WTF.GetTaskIdsByDocId(docId)
            Dim lstTaskIds As List(Of Int64) = New List(Of Int64)

            If Not IsNothing(dsTaskIds) Then
                If dsTaskIds.Tables.Count > 0 Then
                    For Each dr As DataRow In dsTaskIds.Tables(0).Rows
                        lstTaskIds.Add(Int64.Parse(dr.Item("Task_ID").ToString()))
                    Next
                End If
            End If

            dsTaskIds.Dispose()
            dsTaskIds = Nothing

            Return lstTaskIds
        End Function

        ''' <summary>
        ''' Crea las condiciones para el where sobre las restricciones para los documentos y las va agregando a ColumCndstring
        ''' </summary>
        ''' <param name="valueString"></param>
        ''' <param name="Indexs"></param>
        ''' <param name="i"></param>
        ''' <param name="FlagCase"></param>
        ''' <param name="ColumCondstring"></param>
        ''' <param name="First"></param>
        ''' <param name="dateDeclarationString"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''        [Javier] 06/10/10    Created
        '''        [Javier] 12/10/10    Modified    Se reemplaza nombre de columna por nombre de campo en tabla
        ''' </history>
        Private Sub CreateTasksRestrictionsWhere(ByRef sbValue As StringBuilder, ByVal Indexs As Generic.List(Of IIndex), ByRef i As Int64, ByRef FlagCase As Boolean, ByRef ColumCondstring As StringBuilder, ByRef First As Boolean, ByRef dateDeclarationString As StringBuilder)
            Dim mainVal As Object = Nothing
            Dim tempVal As Object = Nothing
            Dim sbDate As New StringBuilder
            Dim indexColName As String = "I." + Indexs(i).Column

            sbValue.Remove(0, sbValue.Length)
            If (String.IsNullOrEmpty(Indexs(i).dataDescription)) Then
                sbValue.Append(Indexs(i).Data)
            Else
                sbValue.Append(Indexs(i).dataDescription)
            End If

            If sbValue.Length <> 0 OrElse Indexs(i).[Operator].ToLower = "es nulo" Then
                If (Indexs(i).[Operator] <> "SQL" AndAlso Not sbValue.ToString.StartsWith("select ", True, Nothing)) AndAlso Indexs(i).[Operator].ToLower() <> "sql sin atributo" Then
                    If sbValue.ToString.Split(";").Length > 1 Then
                        If Indexs(i).Type = IndexDataType.Alfanumerico OrElse Indexs(i).Type = IndexDataType.Alfanumerico_Largo Then
                            FlagCase = True
                            mainVal = "'" & LCase(sbValue.Replace(";", "';'").ToString) & "'"
                        End If
                    Else
                        Select Case Indexs(i).Type
                            Case IndexDataType.Numerico, IndexDataType.Numerico_Largo
                                If sbValue.Length <> 0 Then
                                    If Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        mainVal = "'" & sbValue.ToString & "'"
                                    Else
                                        mainVal = sbValue.ToString()
                                    End If
                                End If
                            Case IndexDataType.Numerico_Decimales, IndexDataType.Moneda
                                If sbValue.Length <> 0 Then
                                    If System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator = "," Then sbValue = sbValue.Replace(".", ",")
                                    mainVal = CDec(sbValue.ToString)
                                    mainVal = mainVal.ToString.Replace(",", ".")
                                End If
                            Case IndexDataType.Si_No
                                If sbValue.Length <> 0 Then
                                    mainVal = Int64.Parse(sbValue.ToString)
                                End If
                            Case IndexDataType.Fecha, IndexDataType.Fecha_Hora
                                If Not String.IsNullOrEmpty(sbValue.ToString.Trim) Then
                                    If Server.isSQLServer Then
                                        mainVal = "@fecdesde" & Indexs(i).Column
                                        sbDate.Append("declare " & mainVal & " datetime ")
                                        If Indexs(i).Type = IndexDataType.Fecha Then
                                            sbDate.Append("set " & mainVal & " = " & Server.Con.ConvertDate(sbValue.ToString) & " ")
                                        Else
                                            sbDate.Append("set " & mainVal & " = " & Server.Con.ConvertDateTime(sbValue.ToString) & " ")
                                        End If
                                        dateDeclarationString.Append(sbDate)
                                    Else
                                        If Indexs(i).Type = IndexDataType.Fecha Then
                                            mainVal = Server.Con.ConvertDate(sbValue.ToString)
                                        Else
                                            mainVal = Server.Con.ConvertDateTime(sbValue.ToString)
                                        End If
                                    End If
                                End If
                            Case IndexDataType.Alfanumerico, IndexDataType.Alfanumerico_Largo
                                If sbValue.ToString().IndexOf("=") <> -1 OrElse sbValue.ToString().IndexOf(" or ") <> -1 OrElse sbValue.ToString().IndexOf(" and ") <> -1 Then
                                    If FlagCase Then
                                        mainVal = LCase(sbValue.ToString)
                                    Else
                                        mainVal = sbValue.ToString
                                    End If
                                Else
                                    If FlagCase Then
                                        mainVal = "'" & LCase(sbValue.ToString) & "'"
                                    Else
                                        mainVal = "'" & sbValue.ToString & "'"
                                    End If
                                End If
                        End Select
                    End If
                Else
                    mainVal = sbValue.ToString
                End If

                Dim Op As String = mainVal.ToString
                If Op.Contains("''") Then Op = Op.Replace("''", "'")
                mainVal = Op
                Op = String.Empty

                Dim separator As String = " AND "
                If ColumCondstring.Length > 0 Then
                    ColumCondstring.Append(separator)
                End If

                Select Case Indexs(i).[Operator].ToLower()
                    Case "="
                        Op = "="
                        If FlagCase = True AndAlso (Indexs(i).Type = IndexDataType.Alfanumerico OrElse Indexs(i).Type = IndexDataType.Alfanumerico_Largo) AndAlso (IsNumeric(Results_Business.ReplaceChar(mainVal)) = False) Then
                            ColumCondstring.Append(" (lower(" & indexColName & ")=" & mainVal & ")")
                        Else
                            ColumCondstring.Append(" (" & indexColName & "=" & mainVal & ")")
                        End If

                    Case ">", "<", ">=", "<="
                        Op = Indexs(i).[Operator]
                        ColumCondstring.Append(" (" & indexColName & " " & Op & " (" & mainVal & ")) ")

                    Case "es nulo"
                        Op = "is null"
                        ColumCondstring.Append(" (" & indexColName & " is null)")

                    Case "<>"
                        Op = "<>"
                        ColumCondstring.Append(" (" & indexColName & " <> (" & mainVal & ") or " & indexColName & " is null)")

                    Case "entre"
                        Dim Data2Added As Boolean
                        Try
                            'cambio las a como indice a I ya que todos los indices vienen con dato algunos vacio otros no.
                            Select Case Indexs(i).Type
                                Case 1, 2
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        tempVal = Int64.Parse(Indexs(i).Data2)
                                        Data2Added = True
                                    End If
                                Case 3
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        tempVal = Decimal.Parse(Indexs(i).Data2)
                                        Data2Added = True
                                    End If
                                Case 4
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        If Not String.IsNullOrEmpty(sbValue.ToString.Trim) Then
                                            If Server.isSQLServer Then
                                                'Se optimiza para sql server
                                                tempVal = "@fechasta" & indexColName
                                                sbDate.Remove(0, sbDate.Length)
                                                sbDate.Append("declare " & tempVal & " datetime ")
                                                sbDate.Append("set " & tempVal & " = " & Server.Con.ConvertDate(Indexs(i).Data2) & " ")
                                                dateDeclarationString.Append(sbDate)
                                            Else
                                                tempVal = Server.Con.ConvertDate(Indexs(i).Data2)
                                            End If
                                        End If
                                        Data2Added = True
                                    End If
                                Case 5
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        'Valuestring3 = Server.Con.ConvertDateTime(Indexs(i).Data2)
                                        If Not String.IsNullOrEmpty(sbValue.ToString.Trim) Then
                                            If Server.isSQLServer Then
                                                'Se optimiza para sql server
                                                tempVal = "@fechasta" & indexColName
                                                sbDate.Remove(0, sbDate.Length)
                                                sbDate.Append("declare " & tempVal & " datetime ")
                                                sbDate.Append("set " & tempVal & " = " & Server.Con.ConvertDateTime(Indexs(i).Data2) & " ")
                                                dateDeclarationString.Append(sbDate)
                                            Else
                                                tempVal = Server.Con.ConvertDateTime(Indexs(i).Data2)
                                            End If
                                        End If
                                        Data2Added = True
                                    End If
                                Case 6
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        tempVal = CDec(Indexs(i).Data2)
                                        Data2Added = True
                                    End If
                                Case 7, 8
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        FlagCase = True
                                        tempVal = "'" & LCase(DirectCast(Indexs(i).Data2, String)) & "'"
                                        Data2Added = True
                                    End If
                            End Select
                        Catch ex As Exception
                            Throw New Exception("Ocurrio un error al convertir al tipo de Dato: Dato: " & sbValue.ToString & ", Tipo Dato: " & Indexs(i).Type & " " & ex.ToString)
                        End Try

                        If Data2Added = True Then
                            If Server.isSQLServer And (Indexs(i).Type = IndexDataType.Fecha OrElse Indexs(i).Type = IndexDataType.Fecha_Hora) Then

                                ColumCondstring.Append(" (" & indexColName & " BETWEEN (" & mainVal & ") and (" & tempVal & "))")
                            Else
                                ColumCondstring.Append(" (" & indexColName & " >= (" & mainVal & ") and " & indexColName & " <= (" & tempVal & "))")
                            End If
                            Data2Added = False
                        End If

                    Case "contiene"
                        If FlagCase = True Then
                            ColumCondstring.Append(" (lower(" & indexColName & ") Like '%" & Replace(Trim(mainVal), "'", "") & "%')")
                        Else
                            ColumCondstring.Append(" (" & indexColName & " Like '%" & Replace(Trim(mainVal), "'", "") & "%')")
                        End If

                    Case "empieza"
                        If FlagCase = True Then
                            ColumCondstring.Append(" (lower(" & indexColName & ") Like '" & Replace(Trim(mainVal), "'", "") & "%')")
                        Else
                            ColumCondstring.Append(" (" & indexColName & " Like '" & Replace(Trim(mainVal), "'", "") & "%')")
                        End If

                    Case "termina"
                        If FlagCase = True Then
                            ColumCondstring.Append(" (lower(" & indexColName & ") Like '%" & Replace(Trim(mainVal), "'", "") & "')")
                        Else
                            ColumCondstring.Append(" (" & indexColName & " Like '%" & Replace(Trim(mainVal), "'", "") & "')")
                        End If

                    Case "alguno"
                        Op = "LIKE"
                        mainVal = mainVal.Replace(";", ",")
                        mainVal = mainVal.Replace("  ", " ")
                        mainVal = mainVal.Replace(" ", ",")
                        Dim SomeValues As Array = DirectCast(mainVal, String).Split(",")
                        Dim x As Int32
                        Dim somestring As String = String.Empty
                        For x = 0 To SomeValues.Length - 1
                            Dim Val As String = SomeValues(x)
                            If IsNothing(Val) = False AndAlso Not String.IsNullOrEmpty(Val.Trim) Then
                                If i = 0 AndAlso x = 0 Then
                                    If FlagCase = True Then
                                        somestring = " (lower(" & indexColName & ") " & Op & " ('%" & Val.Replace("'", "") & "%')"
                                    Else
                                        somestring = " (" & indexColName & " " & Op & " ('%" & Val.Replace("'", "") & "%')"
                                    End If
                                ElseIf x > 0 Then
                                    If String.IsNullOrEmpty(somestring) Then
                                        If FlagCase = True Then
                                            somestring &= separator & " lower(" & DirectCast(indexColName, String) & ") " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        Else
                                            somestring &= separator & " " & DirectCast(indexColName, String) & " " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        End If
                                    Else
                                        separator = " or "
                                        If FlagCase = True Then
                                            somestring &= separator & " lower(" & indexColName & ") " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        Else
                                            somestring &= separator & " " & indexColName & " " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        End If
                                    End If
                                End If
                            End If
                        Next
                        If Not String.IsNullOrEmpty(somestring) Then ColumCondstring.Append(somestring & ")")
                        SomeValues = Nothing
                        somestring = Nothing

                    Case "distinto"
                        Op = "NOT LIKE"
                        mainVal = mainVal.Replace(";", ",")
                        mainVal = mainVal.Replace("  ", " ")
                        mainVal = mainVal.Replace(" ", ",")
                        Dim SomeValues As Array = DirectCast(mainVal, String).Split(",")
                        Dim x As Int32
                        Dim somestring As String = String.Empty
                        For x = 0 To SomeValues.Length - 1
                            Dim Val As String = SomeValues(x)
                            If IsNothing(Val) = False AndAlso Not String.IsNullOrEmpty(Val.Trim) Then
                                If x = 0 And i = 0 Then
                                    If FlagCase = True Then
                                        somestring = " (lower(" & indexColName & ") " & Op & " ('%" & Val.Replace("'", "") & "%')"
                                    Else
                                        somestring = " (" & indexColName & " " & Op & " ('%" & Val.Replace("'", "") & "%')"
                                    End If
                                ElseIf x = 0 Then
                                    If FlagCase = True Then
                                        somestring = " (lower(" & indexColName & ") " & Op & " ('%" & Val.Replace("'", "") & "%')"
                                    Else
                                        somestring = " (" & indexColName & " " & Op & " ('%" & Val.Replace("'", "") & "%')"
                                    End If
                                Else
                                    If String.IsNullOrEmpty(somestring) Then
                                        If FlagCase = True Then
                                            somestring &= separator & " lower(" & DirectCast(indexColName, String) & ") " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        Else
                                            somestring &= separator & " " & DirectCast(indexColName, String) & " " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        End If
                                    Else
                                        separator = " or "
                                        If FlagCase = True Then
                                            somestring &= separator & " lower(" & indexColName & ") " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        Else
                                            somestring &= separator & " " & indexColName & " " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        End If
                                    End If
                                End If
                            End If
                        Next
                        If Not String.IsNullOrEmpty(somestring) Then ColumCondstring.Append(somestring & ")")
                        SomeValues = Nothing
                        somestring = Nothing

                    Case "dentro" 'jhp
                        If FlagCase = True Then
                            Select Case Indexs(i).Type
                                Case IndexDataType.Numerico, IndexDataType.Numerico_Largo, IndexDataType.Moneda
                                    If Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        ColumCondstring.Append(" (lower(" & indexColName & ") in (" & mainVal & "'))")
                                    Else
                                        ColumCondstring.Append(" (lower(" & indexColName & ") in (" & mainVal & "))")
                                    End If
                                Case Else
                                    ColumCondstring.Append(" (lower(" & indexColName & ") in (" & mainVal & "'))")
                            End Select
                        Else
                            Select Case Indexs(i).Type
                                Case IndexDataType.Numerico, IndexDataType.Numerico_Largo, IndexDataType.Moneda
                                    If Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        ColumCondstring.Append(" (" & indexColName & " in (" & mainVal & "')")
                                    Else
                                        ColumCondstring.Append(" (" & indexColName & " in (" & mainVal & ")")
                                    End If
                                Case Else
                                    ColumCondstring.Append(" (" & indexColName & " in (" & mainVal & "')")
                            End Select
                        End If
                    Case "sql sin atributo"
                        ColumCondstring.Append(" (" & mainVal & ")")
                    Case "sql"
                        ColumCondstring.Append(" (" & indexColName & " in (" & mainVal & "))")
                End Select
                Op = Nothing
                separator = Nothing
            End If

        End Sub

        Public Sub SetDocumentRead(currentUserId As Long, docTypeId As Long, docId As Long)
            Try
                WTF.SetDocumentRead(currentUserId, docTypeId, docId)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' Método que sirve para obtener un task en base a un WorkId
        ''' </summary>
        ''' <param name="docId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>   
        '''     [Gaston]    20/10/2008  Modified    Si el taskId no existe se retorna nothing
        '''     [Marcelo]   30/11/2009  Modified    Si el documento esta en un solo wf lo devuelve, sino filtra por workid
        '''     [Javier P.]   07/08/2012  Modified    Se agrega parámetro opcional para poder pasar el user id desde un WS
        ''' </history>
        Public Function GetTaskByDocId(ByVal docId As Int64, ByVal userID As Int64) As ITaskResult
            Dim ds As DataSet = WTF.GetTaskIdsByDocId(docId)

            If Not IsNothing(ds) And ds.Tables.Count = 1 And ds.Tables(0).Rows.Count > 0 Then

                Return GetTaskByTaskIdAndDocTypeIdAndStepId(Int64.Parse(ds.Tables(0).Rows(0)("task_id").ToString()), Int64.Parse(ds.Tables(0).Rows(0)("doc_type_id").ToString()), Int64.Parse(ds.Tables(0).Rows(0)("step_id").ToString()), userID)
            Else
                Return Nothing
            End If

        End Function

        ''' <summary>
        ''' Método que sirve para obtener un task en base al documento y el tipo de documento
        ''' </summary>
        ''' <param name="docId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>   
        '''     [Javier P.]   07/11/2012  Created
        ''' </history>
        Public Function GetTaskByDocIdAndDocTypeId(ByVal docId As Int64, ByVal docTypeId As Int64) As ITaskResult
            Dim ds As DataSet = WTF.GetTaskIdsByDocId(docId)
            If Not IsNothing(ds) And ds.Tables.Count = 1 And ds.Tables(0).Rows.Count > 0 Then

                Return GetTaskByTaskIdAndDocTypeIdAndStepId(ds.Tables(0).Rows(0)("task_id"), ds.Tables(0).Rows(0)("doc_type_id"), ds.Tables(0).Rows(0)("step_id"), 0)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Devuelve un objeto ZCoreView con el id y nombre de la tarea
        ''' </summary>
        ''' <param name="docId">Id del documento a buscar</param>
        ''' <returns>ZCoreView con el id y nombre de la tarea</returns>
        ''' <remarks>Devuelve la primer tarea encontrada por ese DocId</remarks>
        Public Function GetTaskIdAndNameByDocId(ByVal docId As Int64) As ZCoreView
            Dim dtTemp As DataTable = WTF.GetTaskIdAndNameByDocId(docId)
            If dtTemp.Rows.Count = 1 Then
                Return New ZCoreView(CLng(dtTemp.Rows(0)(0)), dtTemp.Rows(0)(1))
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Devuelve un objeto ZCoreView con el id y nombre de la tarea
        ''' </summary>
        ''' <param name="docId">Id del documento a buscar</param>
        ''' <param name="docTypeId">Tipo de documento</param>
        ''' <returns>ZCoreView con el id y nombre de la tarea</returns>
        ''' <remarks>Devuelve la primer tarea encontrada por ese DocId</remarks>
        Public Function GetTaskViewByDocIdAndDocTypeId(ByVal docId As Int64, ByVal docTypeId As Long) As ZCoreView
            Dim dtTemp As DataTable = WTF.GetTaskViewByDocIdAndDocTypeId(docId, docTypeId)
            If dtTemp.Rows.Count = 1 Then
                Return New ZCoreView(CLng(dtTemp.Rows(0)(0)), dtTemp.Rows(0)(1))
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' Obtiene una tarea en particular
        ''' </summary>
        ''' <param name="taskId">Id de la tarea</param>
        ''' <param name="WfStep">Etapa de la tarea (opcional)</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Javier P.]   07/08/2012  Modified    Se agrega parámetro opcional para poder pasar el user id desde un WS
        '''     [Javier P.]   10/10/2012  Modified    Se modifica el metodo para aumentar la performance
        ''' </history>
        Public Function GetTask(ByVal taskId As Int64, ByVal userID As Int64) As ITaskResult
            Dim taskResult As ITaskResult = Nothing
            Dim ds As DataSet = GetStepIdDocTypeIdByTaskId(taskId)
            If (ds.Tables(0).Rows IsNot Nothing) AndAlso (ds.Tables(0).Rows.Count > 0) Then
                Dim taskIDs As New List(Of Long)
                taskIDs.Add(taskId)
                taskResult = GetTasksByTaskIdsAndDocTypeIdAndStepId(taskIDs, Int64.Parse(ds.Tables(0).Rows(0).Item("Doc_type_Id").ToString()), Int64.Parse(ds.Tables(0).Rows(0).Item("Step_Id").ToString()), SearchType.OpenTask, userID)
            End If
            If taskResult Is Nothing Then
                Return Nothing
            Else
                Return taskResult
            End If
        End Function

        ''' <summary>
        ''' Obtiene una slst
        ''' </summary>
        ''' <param id="id">Id de la tarea</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''     [FelipeH.]   17/04/2020  Create
        ''' </history>
        Public Function SelectResult(ByVal id As Int64) As DataTable
            Try
                Dim ASB As New AutoSubstitutionBusiness
                Dim SustTable As DataTable = ASB.GetIndexData(id, False)
                Return SustTable
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Function

        Public Function SelectResultDinamicWithLimit(ByVal id As Int64, ByVal value As String, LimitTo As Int64) As List(Of CodigoDescripcion)
            Try
                Dim ASB As New AutoSubstitutionBusiness
                Dim SustTable As DataTable = ASB.GetIndexDataWithLimit(id, LimitTo, value)

                Dim items As New List(Of CodigoDescripcion)
                items = (From p In SustTable.AsEnumerable()
                         Select New CodigoDescripcion With {.Codigo = p.Field(Of String)("Codigo"),
                                .Descripcion = p.Field(Of String)("Descripcion")}).ToList()

                'Dim filtered = items.Where(Function(x) x.Descripcion.Contains(value)).ToList()



                Return items
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Function




        Public Function SelectResultDinamic(ByVal id As Int64, ByVal value As String) As List(Of CodigoDescripcion)
            Try
                Dim ASB As New AutoSubstitutionBusiness
                Dim SustTable As DataTable = ASB.GetIndexData(id, False)

                Dim items As New List(Of CodigoDescripcion)
                items = (From p In SustTable.AsEnumerable()
                         Select New CodigoDescripcion With {.Codigo = p.Field(Of String)("Codigo"),
                                .Descripcion = p.Field(Of String)("Descripcion")}).Where(Function(x) x.Descripcion.ToLower() Like value.ToLower() + "*").ToList()

                'Dim filtered = items.Where(Function(x) x.Descripcion.Contains(value)).ToList()



                Return items
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Function

        Public Class CodigoDescripcion
            Property Codigo As String
            Property Descripcion As String
        End Class

        Public Function GetDocId(ByVal taskId As Int64) As Int64
            Return WTF.GetDocId(taskId)
        End Function

        Public Function GetDocTypeId(ByVal taskId As Int64) As Int64
            Return WTF.GetDocTypeId(taskId)
        End Function

        Public Function GetResultExtraData(ByVal taskId As Int64) As DataTable
            Return WTF.GetResultExtraData(taskId)
        End Function

        ''' <summary>
        ''' Devuelve la tarea en forma de dataset
        ''' </summary>
        ''' <param name="taskId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTaskDs(ByVal taskId As Int64) As DataSet
            Return WTF.GetTaskByTaskId(taskId)
        End Function

        ''' <summary>
        ''' Método que sirve para obtener un dataset con todas las tareas en las que se encuentra el documento
        ''' </summary>
        ''' <param name="docId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTasksByTask(ByVal result As ITaskResult) As DataSet
            Return WTF.GetTasksByTask(result)
        End Function

        ''' <summary>
        ''' Devuelve el total de tareas que tiene un usuario sin leer.
        ''' </summary>
        ''' <param name="UserID">Id usuario del que se quiere conocer total de tareas sin leer.</param>
        ''' <returns></returns>
        Public Function GetUnreadTasksCountByUserID(ByVal UserID As Int64) As Int64
            Try
                Dim UserBusiness As New UserBusiness
                Dim MyTaskEntities As String = New UserPreferences().getValue("MyTasksEntities", UPSections.UserPreferences, "", UserID)
                Return WTF.GetUnreadTasksCountByUserID(UserBusiness.GetUserById(UserID), MyTaskEntities)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Function


#End Region

#Region "OpenDocument"
        Public Sub OpenDocument(ByRef Result As TaskResult)
            If IsNothing(Result.FullPath) Then
                Throw New Exception("No se encontró la documentación en Zamba Software")
            End If
            Dim fi As New FileInfo(Result.RealFullPath)
            If Not fi.Exists Then
                Throw New Exception("No se encontró el archivo")
            End If
        End Sub
#End Region

#Region "AddtoWF"
        'todo porque hay dos
        ''' <summary>
        ''' </summary>
        ''' <param name="Results"></param>
        ''' <param name="WF"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [Sebastian 12-05-09]    COMENTED    Agrega un result al wf que esta relacionado o se pretende agregar.
        ''' [Sebastian 12-05-09] se agrego try-catch
        ''' [Tomas] - 13/05/2009 - Modified - Se remueve el try-catch que Sebastian habia puesto ya que generabla conflictos con la interfaz gráfica.
        ''' [Marcelo] - 31/08/2010 - Modified - Se quita la llamada a la actualizacion del estado, ya que la misma se hace cuando se inserta
        ''' </history>
        Public Sub AddResultsToWorkFLow(ByVal Results As ArrayList, ByVal WF As IWorkFlow, LogInAction As Boolean, OpenTaskAfterInsert As Boolean, currentUserid As Long, IsAsync As Boolean)

            Dim TasksResults As New List(Of ITaskResult)
            Dim WFB As New WFBusiness

            For Each Result As Result In Results
                ' valido que no exista el result en el workflow
                If WFB.ValidateDocIdInWF(Result.ID, WF.ID, Result.DocTypeId) = False Then
                    If IsNothing(WF.InitialStep) = False Then
                        Dim t As New TaskResult(WF.InitialStep, CoreData.GetNewID(IdTypes.Tasks), Result.ID, Result.DocType,
                                                Result.Name, Result.IconId, 0, TaskStates.Asignada,
                                                Result.Indexs, Result.DISK_VOL_PATH, Result.Parent.ID, Result.OffSet, Result.Doc_File,
                                                Result.Disk_Group_Id, WF.InitialStep.InitialState)

                        t.WorkId = WF.ID
                        t.CheckIn = Now
                        t.WorkId = WF.ID
                        t.CheckIn = Now
                        t.Name = Result.Name
                        t.Disk_Group_Id = Result.Disk_Group_Id
                        t.DISK_VOL_PATH = Result.DISK_VOL_PATH
                        t.Parent.ID = Result.Parent.ID
                        t.OffSet = Result.OffSet
                        t.Doc_File = Result.Doc_File
                        t.AsignedToId = currentUserid
                        t.AsignedById = currentUserid


                        TasksResults.Add(t)

                        'Logs the  user action
                        If LogInAction = True Then
                            LogTask(t, "Ingreso Tarea a Workflow")
                        End If

                        UB.SaveAction(t.ID, ObjectTypes.WFTask, RightsType.insert, "Se inserto la tarea: " & t.ID _
                                                                                                    & " en el WF: " & WF.Name)
                    Else
                        Throw New Exception("No hay definida una etapa inicial en el Workflow: " & WF.Name)
                    End If

                Else
                    Throw New Exception("El documento ya esta ingresado en el Workflow: " & WF.Name)
                End If

            Next
            ' valido que haya un result al menos para meter en el workflow
            If TasksResults.Count > 0 Then

                WTF.InsertTasks(TasksResults, WF, -1, Int32.Parse(New UserPreferences().getValue("TimeOut", UPSections.UserPreferences, "30", currentUserid)))

                Dim WFRulesBusiness As New WFRulesBusiness
                'TODO WF: ESTO DEBE SER GENERICO
                WFStepBusiness.FillSteps(WF)
                WFRulesBusiness.FillRules(WF)

                'todo wf: esto se deberia levantar un evento y un controlador atraparlo y ver las reglas
                'todo wf: verificar que el filtrado no sea de la coleccion real de la etapa
                For Each S As WFStep In WF.Steps.Values
                    If Not IsNothing(WF.InitialStep) Then
                        If WF.InitialStep.ID = S.ID Then
                            For Each r As WFRuleParent In S.Rules
                                If r.ParentType = TypesofRules.ValidacionEntrada Then

                                    TasksResults = r.ExecuteRule(TasksResults, Me, New WFRulesBusiness, IsAsync)

                                End If
                            Next
                        End If
                    End If
                Next

                'todo wf: esto se deberia levantar un evento y un controlador atraparlo y ver las reglas
                For Each S As WFStep In WF.Steps.Values
                    If Not IsNothing(WF.InitialStep) Then
                        If WF.InitialStep.ID = S.ID Then
                            For Each r As WFRuleParent In S.Rules
                                If r.ParentType = TypesofRules.Entrada AndAlso r.Enable Then

                                    r.ExecuteRule(TasksResults, Me, New WFRulesBusiness, IsAsync)

                                End If
                                If r.ParentType = TypesofRules.Insertar Then

                                    r.ExecuteRule(TasksResults, Me, New WFRulesBusiness, IsAsync)

                                End If
                            Next
                        End If
                    End If
                Next

                'Realiza el refresh
                'If OpenTaskAfterInsert Then
                '    RaiseEvent AddedTask(TasksResults, OpenTaskAfterInsert)
                'End If
            End If
        End Sub

        ''' <summary>
        ''' Sobrecarga del metodo que recibe una transaction y se separa la ejecucion de las reglas de entrada.
        ''' </summary>
        ''' <param name="Results"></param>
        ''' <param name="WF"></param>
        ''' <param name="LogInAction"></param>
        ''' <param name="OpenTaskAfterInsert"></param>
        ''' <param name="currentUserid"></param>
        ''' <param name="IsAsync"></param>
        ''' <param name="tran"></param>
        Public Function AddResultsToWorkFLow(ByVal Results As ArrayList, ByVal WF As IWorkFlow, LogInAction As Boolean, OpenTaskAfterInsert As Boolean, currentUserid As Long, IsAsync As Boolean, tran As Transaction) As List(Of ITaskResult)

            Dim TasksResults As New List(Of ITaskResult)
            Dim WFB As New WFBusiness
            Dim timeout = Integer.Parse(New UserPreferences().getValue("TimeOut", UPSections.UserPreferences, "30", currentUserid))

            For Each Result As Result In Results

                ' valido que no exista el result en el workflow
                If WFB.ValidateDocIdInWF(Result.ID, WF.ID, Result.DocTypeId, tran) = False Then

                    If Not IsNothing(WF.InitialStep) Then

                        Dim t As New TaskResult(WF.InitialStep,
                                                CoreData.GetNewID(IdTypes.Tasks),
                                                Result.ID,
                                                Result.DocType,
                                                Result.Name,
                                                Result.IconId,
                                                0,
                                                TaskStates.Asignada,
                                                Result.Indexs,
                                                Result.DISK_VOL_PATH,
                                                Result.Parent.ID,
                                                Result.OffSet,
                                                Result.Doc_File,
                                                Result.Disk_Group_Id,
                                                WF.InitialStep.InitialState)

                        t.WorkId = WF.ID
                        t.CheckIn = Now
                        t.WorkId = WF.ID
                        t.CheckIn = Now
                        t.Name = Result.Name
                        t.Disk_Group_Id = Result.Disk_Group_Id
                        t.DISK_VOL_PATH = Result.DISK_VOL_PATH
                        t.Parent.ID = Result.Parent.ID
                        t.OffSet = Result.OffSet
                        t.Doc_File = Result.Doc_File
                        t.AsignedToId = currentUserid
                        t.AsignedById = currentUserid

                        TasksResults.Add(t)

                        'Logs the  user action
                        If LogInAction Then
                            LogTask(t, "Ingreso Tarea a Workflow")
                        End If

                        WTF.InsertTasks(New List(Of ITaskResult) From {t}, WF, -1, tran, timeout)
                        UB.SaveAction(t.ID, ObjectTypes.WFTask, RightsType.insert, "Se inserto la tarea: " & t.ID & " en el WF: " & WF.Name, currentUserid, tran)
                    Else
                        Throw New Exception("No hay definida una etapa inicial en el Workflow: " & WF.Name)
                    End If

                Else
                    Throw New Exception("El documento ya esta ingresado en el Workflow: " & WF.Name)
                End If

            Next

            ' valido que haya un result al menos para meter en el workflow
            'If TasksResults.Count > 0 Then
            '    WTF.InsertTasks(TasksResults, WF, -1, tran, timeout)
            'End If

            Return TasksResults

        End Function

        Public Sub ExecuteWFRulesFromInsert(WF As IWorkFlow, OpenTaskAfterInsert As Boolean, IsAsync As Boolean, TasksResults As List(Of ITaskResult))

            Dim WFRulesBusiness As New WFRulesBusiness
            WFStepBusiness.FillSteps(WF)
            WFRulesBusiness.FillRules(WF)

            For Each S As WFStep In WF.Steps.Values
                If Not IsNothing(WF.InitialStep) Then
                    If WF.InitialStep.ID = S.ID Then
                        For Each r As WFRuleParent In S.Rules

                            If r.ParentType = TypesofRules.ValidacionEntrada Then
                                TasksResults = r.ExecuteRule(TasksResults, Me, New WFRulesBusiness, IsAsync)
                            End If

                        Next
                    End If
                End If
            Next

            If OpenTaskAfterInsert = False Then
                For Each S As WFStep In WF.Steps.Values
                    If Not IsNothing(WF.InitialStep) Then
                        If WF.InitialStep.ID = S.ID Then
                            For Each r As WFRuleParent In S.Rules

                                If r.ParentType = TypesofRules.Entrada AndAlso r.Enable Then
                                    r.ExecuteRule(TasksResults, Me, New WFRulesBusiness, IsAsync)
                                End If

                                If r.ParentType = TypesofRules.Insertar Then
                                    r.ExecuteRule(TasksResults, Me, New WFRulesBusiness, IsAsync)
                                End If

                            Next
                        End If
                    End If
                Next
            End If

            'If OpenTaskAfterInsert Then
            '    RaiseEvent AddedTask(TasksResults, OpenTaskAfterInsert)
            'End If

        End Sub




        Public Sub executeWF(ByVal wfs As ArrayList)
            executeWF(wfs, True, False)
        End Sub

        Public Sub executeWF(ByVal wfs As ArrayList, ByVal OpenTaskAfterInsert As Boolean, ByVal IsAsync As Boolean)

            If Not IsNothing(wfs) AndAlso wfs.Count = 2 Then

                Dim TasksResults As Generic.List(Of ITaskResult) = wfs(1)
                Dim WF As WorkFlow = wfs(0)

                'todo wf: esto se deberia levantar un evento y un controlador atraparlo y ver las reglas
                'todo wf: verificar que el filtrado no sea de la coleccion real de la etapa
                For Each S As WFStep In WF.Steps.Values
                    If Not IsNothing(WF.InitialStep) Then
                        If WF.InitialStep.ID = S.ID Then
                            For Each r As WFRuleParent In S.Rules
                                If r.ParentType = TypesofRules.ValidacionEntrada Then

                                    TasksResults = r.ExecuteRule(TasksResults, Me, New WFRulesBusiness, IsAsync)

                                End If
                            Next
                        End If
                    End If
                Next

                'todo wf: esto se deberia levantar un evento y un controlador atraparlo y ver las reglas
                For Each S As WFStep In WF.Steps.Values
                    If Not IsNothing(WF.InitialStep) Then
                        If WF.InitialStep.ID = S.ID Then
                            For Each r As WFRuleParent In S.Rules
                                If r.ParentType = TypesofRules.Entrada AndAlso r.Enable Then

                                    r.ExecuteRule(TasksResults, Me, New WFRulesBusiness, IsAsync)

                                End If
                            Next
                        End If
                    End If
                Next

                'If OpenTaskAfterInsert Then
                'Realiza el refresh
                'If RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.ModuleWorkFlow, RightsType.Use) = True Then
                '    RaiseEvent AddedTask(TasksResults, OpenTaskAfterInsert)
                'End If

                For Each S As WFStep In WF.Steps.Values
                    If Not IsNothing(WF.InitialStep) Then
                        If WF.InitialStep.ID = S.ID Then
                            For Each r As WFRuleParent In S.Rules
                                If r.RuleType = TypesofRules.Insertar Then

                                    r.ExecuteRule(TasksResults, Me, New WFRulesBusiness, IsAsync)

                                End If
                            Next
                        End If
                    End If
                Next
            End If
        End Sub

        'Public Sub AddResultsToWorkFLow(ByVal TResults As ArrayList, ByVal WF As WorkFlow, ByVal p_WStep As WFStep, Optional ByVal OpenTaskAfterInsert As Boolean = True)
        '    Dim TasksResults As New System.Collections.Generic.List(Of Core.ITaskResult)
        '    For Each Result As Core.TempTaskResult In TResults
        '        Dim t As New TaskResult(p_WStep, CoreData.GetNewID(IdTypes.Tasks), Result.Result.ID, Result.Result.DocType, Result.Result.Name, Result.Result.IconId, 0, DirectCast(CInt(Result.TaskState), Zamba.Core.TaskStates), Result.Result.Indexs, p_WStep.InitialState, Result.AsignedToId)
        '        t.WorkId = WF.ID
        '        t.CheckIn = Now
        '        TasksResults.Add(t)
        '        LogCheckIn(t)
        '    Next

        '    WTF.InsertTasks(TasksResults, WF, p_WStep.ID, 30)
        '    'TODO WF: ESTO DEBE SER GENERICO
        '    WFStepBusiness.FillSteps(WF)
        '    Dim WFRulesBusiness As New WFRulesBusiness
        '    WFRulesBusiness.FillRules(WF)

        '    'todo wf: esto se deberia levantar un evento y un controlador atraparlo y ver las reglas
        '    'todo wf: verificar que el filtrado no sea de la coleccion real de la etapa
        '    For Each S As WFStep In WF.Steps.Values
        '        If WF.InitialStep.ID = S.ID Then
        '            For Each r As WFRuleParent In S.Rules
        '                If r.ParentType = TypesofRules.ValidacionEntrada Then

        '                    TasksResults = r.ExecuteRule(TasksResults, Me)

        '                End If
        '            Next
        '        End If
        '    Next

        '    'todo wf: esto se deberia levantar un evento y un controlador atraparlo y ver las reglas
        '    For Each S As WFStep In WF.Steps.Values
        '        If WF.InitialStep.ID = S.ID Then
        '            For Each r As WFRuleParent In S.Rules
        '                If r.ParentType = TypesofRules.Entrada Then

        '                    r.ExecuteRule(TasksResults, Me)

        '                End If
        '            Next
        '        End If
        '    Next

        '    'If OpenTaskAfterInsert Then
        '    'Realiza el refresh
        '    RaiseEvent AddedTask(TasksResults, OpenTaskAfterInsert)
        '    'End If

        'End Sub
        '''Inserta una coleccion de newResult en el WF
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="TResults"></param>
        ''' <param name="WFId"></param>
        ''' <returns></returns>
        ''' <history>
        ''' [Ezequiel] 10/09/2009 Modified - Se aplico llamada a cache al metodo ya que consultaba directamente la base de datos
        '''                                  para consultar reglas de entrada y validacion de entrada.
        ''' </history>
        ''' <remarks></remarks>
        Public Function AddNewResultsToWorkFLow(ByVal TResults As ArrayList, ByVal WFId As Int64, Optional ByVal OpenTaskAfterInsert As Boolean = True) As System.Collections.Generic.List(Of Core.ITaskResult)
            Dim TasksResults As New System.Collections.Generic.List(Of Core.ITaskResult)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtengo el WF")
            Dim WF As New WFBusiness
            Dim workflow As WorkFlow = WF.GetWorkFlow(WFId)
            WF = Nothing

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Recorro los newres")
            Dim WFSB As New WFStepBusiness

            For Each newRes As NewResult In TResults
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Contruyo el ITaskResult")
                Dim WorkId As Int64 = workflow.ID
                Dim StateId As Int64 = WFStepStatesBusiness.getInitialState(workflow.InitialStepIdTEMP)
                Dim State As WFStepState = WFStepStatesComponent.GetStepStateById(StateId)
                Dim t As New TaskResult(workflow.InitialStepIdTEMP, CoreData.GetNewID(IdTypes.Tasks), newRes.ID, newRes.DocType, newRes.Name, newRes.IconId, 0, Zamba.Core.TaskStates.Desasignada, newRes.Indexs, State)
                t.CheckIn = Now
                t.File = newRes.File
                TasksResults.Add(t)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Inserto la tarea en el WF")

                t.WorkId = workflow.ID
                t.StateId = WFStepStatesBusiness.getInitialState(workflow.InitialStepIdTEMP)
                t.State = WFStepStatesComponent.GetStepStateById(t.StateId)
                WTF.InsertTask(t, workflow.ID, workflow.InitialStepIdTEMP, t.StateId)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Hago log de checkin")
                'LogCheckIn(t.TaskId, t.CheckIn)
            Next
            WFSB = Nothing

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Retorno las tareas insertadas")

            Return TasksResults
        End Function





        Public Shared Event LastInsertedTask(ByVal Task As ITaskResult, ByVal OpenTaskAfterInsert As Boolean)
        ''' <summary>
        ''' Método que sirve para guardar los results seleccionados al workflow que indica la regla ADDTOWF
        ''' </summary>
        ''' <param name="Results"></param>
        ''' <param name="wfId"></param>
        ''' <param name="wfInitialStepId"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' 	[Gaston]	05/08/2008	Created     Gran parte del código se tomo de algunos AddResultsToWorkFLow
        ''' 	[Gaston]	19/02/2009	Modified    Nuevo parámetro: initialStateId, que indica el estado inicial de la etapa inicial
        '''     [Tomas]     01/04/2009  Modified    Se sube el evento para refrescar los controles (grilla, tree, etc..)
        ''' </history>
        Public Sub AddResultsToWorkFLowSinceRuleADDTOWF(ByVal Results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal wfId As Integer, ByVal p_WStep As WFStep, ByVal initialStateId As Int32, Optional ByVal RuleId As Int64 = -1, Optional ByVal OpenTaskAfterInsert As Boolean = True, Optional ByVal IsAsync As Boolean = False)

            Dim TasksResults As New System.Collections.Generic.List(Of Core.ITaskResult)
            Dim WFB As New WFBusiness
            Dim workflow As WorkFlow = WFB.GetWorkFlow(wfId)


            Dim WFRB As New WFRulesBusiness


            Dim RuleConfig As DataSet = WFRB.GetRuleParamItems(RuleId)
            WFRB = Nothing
            For Each Result As Result In Results

                ' Si el result no existe en el workflow
                If (WFB.ValidateDocIdInWF(Result.ID, wfId, Result.DocTypeId) = False) Then

                    Dim t As New TaskResult(p_WStep.ID, CoreData.GetNewID(IdTypes.Tasks), Result.ID, Result.DocType, Result.Name, Result.IconId, 0, Zamba.Core.TaskStates.Desasignada, Result.Indexs, p_WStep.InitialState)
                    t.CheckIn = Now
                    t.WorkId = wfId
                    TasksResults.Add(t)
                    ' Se registra en el historial (ingreso etapa a workflow)
                    LogTask(t, "Ingreso Tarea")
                    ' Se inserta la tarea en el workflow
                    WTF.InsertTask(t, wfId, p_WStep.ID, initialStateId)

                    For Each row As DataRow In RuleConfig.Tables(1).Rows

                        If RuleId <> -1 AndAlso String.Compare(row("value").ToString.ToLower, "show") = 0 Then
                            RaiseEvent LastInsertedTask(t, False)
                        End If
                    Next

                Else
                    Throw New Exception("El result " & Result.Name & " ya se encuentra en el workflow " & workflow.Name)
                End If

            Next
            WFB = Nothing

            If (TasksResults.Count > 0) Then

                WFStepBusiness.FillSteps(workflow)
                Dim WFRulesBusiness As New WFRulesBusiness
                WFRulesBusiness.FillRules(workflow)
                'FillUserTask(wf)

                For Each S As WFStep In workflow.Steps.Values
                    If workflow.InitialStep.ID = S.ID Then
                        For Each r As WFRuleParent In S.Rules
                            If r.ParentType = TypesofRules.ValidacionEntrada Then

                                TasksResults = r.ExecuteRule(TasksResults, Me, New WFRulesBusiness, IsAsync)

                            End If
                        Next
                    End If
                Next

                For Each S As WFStep In workflow.Steps.Values
                    If workflow.InitialStep.ID = S.ID Then
                        For Each r As WFRuleParent In S.Rules
                            If r.ParentType = TypesofRules.Entrada AndAlso r.Enable Then

                                r.ExecuteRule(TasksResults, Me, New WFRulesBusiness, IsAsync)

                            End If
                        Next
                    End If

                Next

                'If OpenTaskAfterInsert Then
                'Realiza el refresh
                'RaiseEvent AddedTask(TasksResults, OpenTaskAfterInsert)
                'End If

            End If

        End Sub

#End Region

#Region "Indexs & DocType"
        Friend Shared Sub FillIndexsAndDocType(ByRef Result As TaskResult)
            Dim DTB As New DocTypesBusiness
            Dim RB As New Results_Business
            Try
                Result.Parent = DTB.GetDocType(Result.DocType.ID)
                Result.Indexs = ZCore.GetInstance().FilterIndex(CInt(Result.DocType.ID))
                RB.CompleteDocument(DirectCast(Result, Result), True)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                DTB = Nothing
                RB = Nothing
            End Try
        End Sub
        ''' <summary>
        ''' Carga los indices para un result
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <param name="inThread"></param>
        ''' <remarks></remarks>
        Friend Shared Sub FillIndexs(ByRef Result As TaskResult, Optional ByVal inThread As Boolean = False)
            Dim RB As New Results_Business
            Try
                'FillDocType(Result)
                Result.Indexs = ZCore.GetInstance().FilterIndex(CInt(Result.DocType.ID))
                'RF.FillIndexData(Result)
                RB.CompleteDocument(DirectCast(Result, Result), inThread)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                RB = Nothing
            End Try
        End Sub
        ''' <summary>
        ''' Carga los indices para un result
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <remarks></remarks>
        Friend Shared Sub CompleteDocument(ByRef Result As TaskResult, ByVal dr As DataRow)

            Dim ASB As New AutoSubstitutionBusiness
            Try
                Result.Indexs = ZCore.GetInstance().FilterIndex(CInt(Result.DocType.ID))

                Try
                    Result.Disk_Group_Id = dr("DISK_GROUP_ID")
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Try
                    Result.Platter_Id = dr("PLATTER_ID")
                Catch ex As Exception
                End Try
                Try
                    Result.Doc_File = dr("DOC_FILE")
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Try
                    Result.OffSet = dr("OFFSET")
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Try
                    Result.Name = dr("NAME")
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Try
                    Result.IconId = dr("ICON_ID")
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Try
                    If IsDBNull(dr("SHARED")) = False Then
                        If CInt(dr("SHARED")) = 1 Then
                            Result.isShared = True
                        Else
                            Result.isShared = False
                        End If
                    Else
                        Result.isShared = False
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Try
                    Result.ParentVerId = dr("ver_Parent_id")
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Try
                    Result.HasVersion = dr("version")
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Try
                    Result.RootDocumentId = dr("RootId")
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Try
                    Result.OriginalName = dr("original_Filename")
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Try

                    If (IsDBNull(dr("NumeroVersion"))) Then
                        Result.VersionNumber = 0
                    Else
                        Result.VersionNumber = dr("NumeroVersion")
                    End If

                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
                Try
                    If IsDBNull(dr("disk_Vol_id")) = False Then
                        Result.Disk_Group_Id = dr("disk_Vol_id")
                    Else
                        Result.Disk_Group_Id = 0
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Try
                    If IsDBNull(dr("DISK_VOL_PATH")) = False Then
                        Result.DISK_VOL_PATH = dr("DISK_VOL_PATH")
                    Else
                        Result.DISK_VOL_PATH = String.Empty
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
                Try
                    Result.UserID = Result.Platter_Id
                    Result.OwnerID = Result.Platter_Id
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try

                If Not IsDBNull(dr("crdate")) Then
                    If Not IsNothing(dr("crdate")) Then
                        If Not String.IsNullOrEmpty(dr("crdate").ToString()) Then
                            Try
                                If Not IsNothing(Result) Then
                                    Result.CreateDate = DateTime.Parse(dr("crdate").ToString())
                                End If
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try
                        End If
                    End If
                End If

                Dim i As Int32
                For i = 0 To Result.Indexs.Count - 1
                    Try
                        If Not IsDBNull(dr("I" & DirectCast(Result.Indexs(i), Index).ID)) Then
                            DirectCast(Result.Indexs(i), Index).Data = dr("I" & DirectCast(Result.Indexs(i), Index).ID).ToString
                            'Si el indice es de tipo Sustitucion
                            If DirectCast(Result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustitución OrElse DirectCast(Result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                'Se carga la descripcion de Indice
                                DirectCast(Result.Indexs(i), Index).dataDescription = ASB.getDescription(DirectCast(Result.Indexs(i), Index).Data, DirectCast(Result.Indexs(i), Index).ID)
                            End If
                        Else
                            DirectCast(Result.Indexs(i), Index).Data = String.Empty
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Next

            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                ASB = Nothing

            End Try
        End Sub

#End Region

#Region "Rights"
        Friend Function HaveRightToView(ByRef wfstep As WFStep, ByVal AsignedToId As Int64, ByVal CurrentUserId As Int64) As Boolean
            Try
                If AsignedToId = 0 Then
                    'Asignado a nadie
                    If Not RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.VerAsignadosANadie, CInt(wfstep.ID)) Then
                        Return False
                    End If
                ElseIf AsignedToId = CurrentUserId Then
                    'Asignado a uno
                    If Not RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.Use, CInt(wfstep.ID)) Then
                        Return False
                    End If
                Else
                    'Asignado a otro
                    If Not RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.VerAsignadosAOtros, CInt(wfstep.ID)) Then
                        Return False
                    End If
                End If
                Return True
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return False
            End Try
        End Function
#End Region


#Region "Actions"

#Region "Add"
        'Public Shared Event AddedTask(ByVal Results As Generic.List(Of ITaskResult), ByVal OpenTaskAfterInsert As Boolean)
        Public Shared Event ShowTask(ByVal result As Zamba.Core.ITaskResult, ByVal OpenTaskAfterInsert As Boolean)

        'Private Sub RefreshAdd(ByVal r As DsDocuments.DocumentsRow, ByVal wf As WorkFlow)
        '    RaiseEvent AddedTask(WFBusiness.GetNewResult(r, DirectCast(wf.Steps(CLng(r.step_Id)), WFStep)))
        'End Sub
#End Region

#Region "Remove"
        Public Shared Event RemovedTask(ByRef Result As TaskResult)

        Public Sub Remove(ByVal taskId As Int64, ByVal deleteDocument As Boolean, ByVal docTypeId As Int64, ByVal fullpath As String, ByVal CurrentUserId As Int64)
            Try
                If (deleteDocument) Then

                    Dim RF As New Results_Factory
                    RF.Delete(taskId, docTypeId, fullpath)
                    RF = Nothing
                End If

                WTF.Delete(taskId)
                UB.SaveAction(CurrentUserId, ObjectTypes.WFTask, RightsType.Delete, "Borrar Tarea")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub



        ''' <summary>
        '''     Remove a task  
        ''' </summary>
        ''' <param name="taskID"></param>
        ''' <param name="deleteDocument"></param>
        ''' <param name="wfstep"></param>
        ''' <param name="CurrentUserId"></param>
        ''' <param name="DocTypeId"></param>
        ''' <history>
        '''                         Modified    Se modifica este metodo para que se le envie el wfstep al metodo como parametro, 
        '''                                             para poder quitar por referencia las tareas de ese step
        '''     Javier  06/12/2010  Modified    GetTaskByTaskIdAndDocTypeIdAndStepId is now a instance method
        ''' </history>
        Public Sub Remove(ByVal taskID As Int64, ByVal deleteDocument As Boolean, ByRef wfstep As IWFStep, ByVal CurrentUserId As Int64, ByVal DocTypeId As Int64)
            Try

                Dim result As ITaskResult = GetTaskByTaskIdAndDocTypeIdAndStepId(taskID, DocTypeId, wfstep.ID, 0)
                LogTask(result, "se quito la Tarea")
                If Not IsNothing(wfstep) Then
                    wfstep.TasksCount = wfstep.TasksCount - 1
                End If

                WTF.Delete(result.TaskId)

                If deleteDocument Then

                    Dim RF As New Results_Factory
                    RF.Delete(result)
                    RF = Nothing
                End If

                RaiseEvent RemovedTask(result)
                UB.SaveAction(CurrentUserId, ObjectTypes.WFTask, RightsType.Delete, "Se borro tarea: " & result.Name)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        '''     Borra la tarea
        ''' </summary>
        ''' <param name="result"></param>
        ''' <param name="deleteDocument"></param>
        ''' <param name="CurrentUserId"></param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Javier]    05/10/2010  Modified    Se coloca en cero el stepid y workid para no abrir tarea luego.
        ''' </history>
        Public Sub Remove(ByRef result As ITaskResult, ByVal deleteDocument As Boolean, ByVal CurrentUserId As Int64, ByVal DeleteFile As Boolean)


            Try
                If deleteDocument Then
                    RF.Delete(result)
                End If

                LogTask(result, "Se quito la tarea")

                WTF.Delete(result)
                RaiseEvent RemovedTask(result)
                RaiseEvent Distributed(result)

                result.WorkId = 0
                result.StepId = 0

                '[Sebastian 04-06-2009] se comento este método porque estaba realzando un remove dos veces
                'RaiseEvent refreshSteps(result.WfStep.WorkId, result.StepId, Nothing)

                UB.SaveAction(CurrentUserId, ObjectTypes.WFTask, RightsType.Delete, "Borrar Tarea")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                RF = Nothing
            End Try
        End Sub
        'Private Sub RefreshRemove(ByVal KeysToRemove As ArrayList, ByVal s As WFStep)
        '    For i As Int32 = 0 To KeysToRemove.Count - 1
        '        RaiseEvent RemovedTask(DirectCast(s.Tasks(KeysToRemove(i)), TaskResult))
        '        s.Tasks.Remove(KeysToRemove(i))
        '    Next
        'End Sub
#End Region

#Region "Distribute"
        Public Shared Event Distributed(ByRef Result As TaskResult)
        Public Shared Event refreshSteps()

        ''' <summary>
        ''' Método utilizado para distribuir la tarea
        ''' </summary>
        ''' <param name="result"></param>
        ''' <param name="newWFStepId"></param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    01/09/2008  Modified
        '''     [Ezequiel]  03/12/09 - Se modifico el metodo para mas performance.
        ''' </history>
        ''' <summary>
        ''' Método utilizado para distribuir la tarea
        ''' </summary>
        ''' <param name="result"></param>
        ''' <param name="newWFStepId"></param>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston]    01/09/2008  Modified
        '''     [Ezequiel]  03/12/09 - Se modifico el metodo para mas performance.
        ''' </history>
        Public Sub Distribute(ByRef Results As List(Of ITaskResult), ByVal newWFStepId As Int64,
                                      ByVal CurrentUserId As Int64)

            Try

                Dim WFSB As New WFStepBusiness

                For Each Result As ITaskResult In Results
                    Dim Resultsaux As New List(Of ITaskResult)

                    Resultsaux.Add(Result)

                    'Ejecuto reglas de validacion de salida
                    'If Not Result.WfStep.Rules Is Nothing Then
                    '    For Each r As WFRuleParent In Result.WfStep.Rules
                    '        If r.ParentType = TypesofRules.ValidacionSalida Then
                    '            Dim WFRB As New WFRulesBusiness

                    '            Results = WFRB.ExecuteRule(r, Resultsaux)
                    '        End If
                    '    Next
                    'End If

                    If Resultsaux.Contains(Result) Then
                        'Ejecuto reglas de salida
                        'If Not Result.WfStep.Rules Is Nothing Then
                        '    For Each r As WFRuleParent In Result.WfStep.Rules
                        '        If r.ParentType = TypesofRules.Salida Then
                        '            Dim WFRB As New WFRulesBusiness
                        '            WFRB.ExecuteRule(r, Resultsaux)
                        '        End If
                        '    Next
                        'End If

                        'Derivo
                        Dim newWFStep As WFStep = WFSB.GetStepById(newWFStepId)
                        Result.WfStep = newWFStep
                        Result.StepId = newWFStep.ID



                        'LA NUEVA ETAPA VINO SIN REGLAS ASI QUE SE LAS AGREGO
                        '--------------------------------------------------
                        If IsNothing(newWFStep.Rules) OrElse newWFStep.Rules.Count = 0 Then
                            Dim WFRulesBusiness As New WFRulesBusiness
                            WFRulesBusiness.FillRules(newWFStep)
                        End If

                        'Ejecuto reglas de validacion de entrada
                        'If Not IsNothing(newWFStep.Rules) Then
                        '    For Each r As WFRuleParent In newWFStep.Rules
                        '        If r.ParentType = TypesofRules.ValidacionEntrada Then
                        '            Dim WFRB As New WFRulesBusiness
                        '            Resultsaux = WFRB.ExecuteRule(r, Resultsaux)
                        '        End If
                        '    Next
                        'End If

                        If Resultsaux.Contains(Result) Then


                            'Estado Verifica si es estado original de la etapa anterior existe en la nueva etapa, si existe lo mantiene
                            Dim stateChange As Boolean = False
                            Try
                                For Each State As WFStepState In Result.WfStep.States
                                    If Not IsNothing(Result.State) Then
                                        If String.Compare(Result.State.Name.ToUpper, State.Name.ToUpper, True) = 0 Then
                                            Result.State = State
                                            stateChange = True
                                            Exit For
                                        End If
                                    End If
                                Next
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try
                            If Not stateChange Then Result.State = newWFStep.InitialState


                            'Vencimiento
                            Result.ExpireDate = Nothing
                            RaiseEvent ChangedExpireDate(Result)

                            'Checkin
                            Result.CheckIn = Now

                            'actualiza base de datsos
                            WTF.UpdateDistribuir(Result)

                            'Log Checkin
                            LogTask(Result, "La Tarea paso a: " & newWFStep.Name)

                            'Ejecuto reglas de entrada
                            If Not IsNothing(newWFStep.Rules) Then
                                For Each r As WFRuleParent In newWFStep.Rules
                                    If r.ParentType = TypesofRules.Entrada Then
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecuto las reglas de ENTRADA")
                                        Dim WFRB As New WFRulesBusiness
                                        WFRB.ExecuteRule(r, Resultsaux, False)
                                    End If
                                Next
                            End If

                            'Quito la tarea de la grilla y actualizo la visualizacion
                            'RaiseEvent Distributed(Result)
                        End If
                    End If
                Next
                WFSB = Nothing
                'Refresco el arbol de workflow
                RaiseEvent refreshSteps()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally

            End Try
        End Sub


        '''' <summary>
        '''' Método que llama al evento refreshSteps encargado de actualizar los nodos que muestran el nombre de la etapa y la cantidad de tareas
        '''' relacionados con el Distribuir
        '''' </summary>
        '''' <param name="wfId"></param>
        '''' <param name="oldStepId"></param>
        '''' <param name="newStepId"></param>
        '''' <param name="distributedTasks"></param>
        '''' <remarks></remarks>
        '''' <history> 
        ''''     [Gaston]    01/09/2008  Created 
        '''' </history>
        'Public Sub refreshStepsAfterDistribute(ByRef wfId As Long, ByRef oldStepId As Long, ByRef newStepId As Long, ByRef distributedTasks As Integer)
        '    RaiseEvent refreshSteps(wfId, oldStepId, newStepId, distributedTasks)
        'End Sub

        Private Sub RefreshDistribute(ByVal t As TaskResult, ByVal r As DsDocuments.DocumentsRow)
            Dim WFB As New WFBusiness
            Try
                If t.StepId <> CLng(r.step_Id) Then
                    Dim wf As IWorkFlow
                    wf = WFB.GetWorkFlow(t.WorkId)

                    RaiseEvent Distributed(t)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                WFB = Nothing
            End Try
        End Sub

#End Region

#Region "Assign"



        Public Shared Event Asigned(ByRef Result As TaskResult)
        Public Sub Asign(ByRef result As ITaskResult, ByVal asignedToId As Int64, ByVal asignedById As Int64, Optional ByVal logAction As Boolean = True)
            Try
                result.AsignedToId = asignedToId
                result.AsignedById = asignedById
                result.AsignedDate = Now
                If (result.AsignedToId = 0) Then
                    result.TaskState = TaskStates.Desasignada
                Else
                    result.TaskState = TaskStates.Asignada
                End If

                WTF.UpdateAssign(result.TaskId, result.AsignedToId, result.AsignedById, result.AsignedDate, result.TaskState)

                If logAction Then
                    Dim IsGroup As Boolean
                    Dim asignedToName As String = UserGroups.GetUserorGroupNamebyId(result.AsignedToId, IsGroup)
                    If String.IsNullOrEmpty(asignedToName) Then
                        asignedToName = "Ninguno"
                    End If
                    WTF.LogAsignedTask(result.TaskId, result.Name, result.DocType.ID, result.DocType.Name, result.StepId, WFStepBusiness.GetStepNameById(result.StepId), result.State.Name, asignedToName, result.WorkId, New WFBusiness().GetWorkflowNameByWFId(result.WorkId))
                End If
                RaiseEvent Asigned(result)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub


        Public Sub Asign(ByRef result As ITaskResult, ByVal asignedToID As Int64, ByVal asignedByID As Int64, ByVal asignedToName As String)
            Try
                result.AsignedToId = asignedToID
                result.AsignedById = asignedByID
                result.AsignedDate = Now

                If asignedToID <> 0 Then

                    If result.TaskState = Global.Zamba.Core.TaskStates.Ejecucion Then
                        Dim UserGroupBusiness As New UserGroupBusiness
                        Dim users As Generic.List(Of Long) = UserGroupBusiness.GetUsersIds(asignedByID)

                        'Si la tarea esta en ejecucion y se asigna al mismo usuario que ya la tiene
                        'o a un grupo en donde esta el usuario entonces dejarla en ejecucion para que no
                        'deba iniciar la tarea nuevamente
                        If users.Contains(asignedToID) OrElse asignedToID = asignedByID Then
                            result.TaskState = TaskStates.Ejecucion
                        Else
                            result.TaskState = TaskStates.Asignada
                        End If

                    Else
                        result.TaskState = TaskStates.Asignada
                    End If

                Else
                    result.TaskState = TaskStates.Desasignada
                End If

                WTF.UpdateAssign(result.TaskId, result.AsignedToId, result.AsignedById, result.AsignedDate, result.TaskState)
                LogTask(result, "Se asigno tarea a: " & asignedToName)
                RaiseEvent Asigned(result)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub



#End Region

#Region "UnAssign"
        Public Sub UnAssign(ByRef Result As ITaskResult, ByVal AsignedBy As IUser)
            Try
                Result.AsignedToId = 0
                Result.AsignedById = AsignedBy.ID
                Result.AsignedDate = Date.Now()
                Result.CheckIn = Nothing
                Result.TaskState = TaskStates.Desasignada

                WTF.UpdateAssign(Result)

                LogTask(Result, "Se desasigno la Tarea")
                RaiseEvent Asigned(Result)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub



        ''' <summary>
        ''' Actualiza el estado de las tareas del usuario
        ''' </summary>
        ''' <param name="userId"></param>
        ''' <remarks></remarks>
        Public Sub UpdateUserTaskStateToAsign(ByVal userId As Int64)
            Try
                If Server.ConInitialized = True Then
                    WTF.UpdateUserTaskStateToAsign(userId)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub


        Public Sub UpdateConIDTaskStateToAsign(ByVal userId As Int64)
            Try
                If Server.ConInitialized = True Then
                    WTF.UpdateConIDTaskStateToAsign(userId)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Public Sub CloseOpenTasksByConId(ByVal conId As Int64)
            Try
                If Server.ConInitialized = True Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "CloseOpenTasksByConId" & conId.ToString())
                    WTF.CloseOpenTasksByConId(conId)
                    Dim UCMF As New UcmFactory
                    Dim UserId As Int64 = UCMF.GetUserIdByConId(conId)
                    UnLockTasks(UserId)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Public Sub CloseOpenTasksByTaskId(ByVal taskId As Long)
            Try
                If Server.ConInitialized = True Then
                    WTF.CloseOpenTasksByTaskId(taskId)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        Public Sub ReleaseOpenTasksWithOutConnection()
            Try
                If Server.ConInitialized = True Then
                    WTF.ReleaseOpenTasksWithOutConnection()
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        Public Sub RegisterTaskAsOpen(ByVal taskId As Int64, ByVal userId As Int64)
            Try
                If Server.ConInitialized = True Then
                    WTF.RegisterTaskAsOpen(taskId, userId)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#End Region

#Region "Estado de Tarea"
        ''' <summary>
        ''' [Sebastian] 21-09-09 MODIFIED I commented line
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <remarks></remarks>
        Public Sub Iniciar(ByRef Result As ITaskResult)
            Try
                If Not Result.WfStep.RuleTareaIniciada Is Nothing Then
                    Dim Results As New List(Of ITaskResult)
                    Results.Add(Result)
                    Dim WFRB As New WFRulesBusiness
                    WFRB.ExecuteRule(Result.WfStep.RuleTareaIniciada, Results, False)
                End If
                LogTask(Result, "Inicio Tarea")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Public Sub Finalizar(ByRef TaskResult As ITaskResult, ByVal CurrentUserId As Int64)
            Try
                If TaskResult.WfStep.RuleTareaFinalizada IsNot Nothing Then
                    Dim Results As New List(Of ITaskResult)
                    Results.Add(TaskResult)
                    Dim WFRB As New WFRulesBusiness
                    WFRB.ExecuteRule(TaskResult.WfStep.RuleTareaFinalizada, Results, False)
                End If

                TaskResult.AsignedToId = 0
                TaskResult.TaskState = Zamba.Core.TaskStates.Desasignada

                Asign(TaskResult, TaskResult.AsignedToId, CurrentUserId, False)

                LogTask(TaskResult, "Finalizo Tarea")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Public Sub Derivar(ByVal Task As ITaskResult,
                           ByVal stepId As Int64,
                           ByVal asignedToId As Int64,
                           ByVal asignedToName As String,
                           ByVal asignedBy As Int64,
                           ByVal asignedDate As Date,
                           ByVal selecCarp As Boolean,
                           ByVal CurrentUserId As Int64)
            Try
                If (asignedToId = 0 OrElse asignedToId < 0) Then
                    Task.AsignedToId = 0
                Else
                    Task.AsignedToId = asignedToId
                End If

                If (asignedBy = 0 OrElse asignedBy < 0) Then
                    Task.AsignedById = 0
                Else
                    Task.AsignedById = asignedBy
                End If
                Task.TaskState = TaskStates.Asignada
                Task.AsignedDate = asignedDate
                Asign(Task, asignedToId, asignedBy, asignedToName)

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Public Sub UpdateTaskState(ByVal taskId As Int64, ByVal taskStateId As Int64)
            WTF.UpdateTaskState(taskId, taskStateId)
        End Sub
#End Region

#Region "Form"
        'Public Sub ChangeForm(ByRef Result As TaskResult)
        '    Try
        '        RaiseEvent ChangedForm(Result)
        '    Catch ex As Exception
        '        ZClass.raiseerror(ex)
        '    End Try
        'End Sub
        'Public Shared Event ChangedForm(ByRef Result As TaskResult)
#End Region

#Region "State"

        ''' <summary>
        ''' Método que sirve para buscar y ejecutar reglas de tipo Estado
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	01/09/2008	Modified    Llamada al método loadWfStepRules
        ''' </history>
        Private Sub ExecutedSetState(ByVal TaskResult As ITaskResult)
            Dim Results As New System.Collections.Generic.List(Of Core.ITaskResult)
            Results.Add(TaskResult)
            Dim WFSB As New WFStepBusiness

            Dim wfstep As IWFStep = WFSB.GetStepById(TaskResult.StepId)
            WFSB = Nothing

            If wfstep.Rules.Count = 0 Then
                Dim WFRulesBusiness As New WFRulesBusiness
                wfstep.Rules = WFRulesBusiness.GetCompleteHashTableRulesByStep(TaskResult.StepId)
                WFRulesBusiness = Nothing
            End If

            For Each Rule As WFRuleParent In wfstep.Rules
                If Rule.RuleType = TypesofRules.Estado Then
                    Dim WFRB As New WFRulesBusiness
                    Dim list As ITaskResult = WFRB.ExecuteRule(Rule, Results, False)
                    WFRB = Nothing
                    list = Nothing
                End If
            Next
            wfstep.Dispose() 'validar referencia
            wfstep = Nothing
            Results.Clear()
            Results = Nothing
        End Sub


        ''' <summary>
        ''' Cambia el estado de la etapa
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <param name="State"></param>
        ''' <remarks></remarks>
        Public Sub ChangeState(ByRef Result As ITaskResult, ByVal State As IWFStepState)
            Dim WFSB As New WFStepBusiness

            Dim wfstep As IWFStep = WFSB.GetStepById(Result.StepId)
            WFSB = Nothing

            If wfstep.States.Contains(State) Then
                Result.State = State
            Else
                If wfstep.States.Count > 0 Then Result.State = DirectCast(wfstep.States(0), WFStepState)
            End If
            wfstep.Dispose() 'validar referencia
            wfstep = Nothing
            WTF.UpdateState(Result)
            LogTask(Result, "Cambio Estado")

            ExecutedSetState(Result)
        End Sub

#End Region

#Region "ChangeExpireDate"
        Public Event ChangedExpireDate(ByRef Result As TaskResult)
        Public Sub ChangeExpireDate(ByRef Result As TaskResult, ByVal NuevaFecha As DateTime)
            Try
                Result.ExpireDate = NuevaFecha
                WTF.UpdateExpiredDate(Result.TaskId, Result.ExpireDate)
                LogTask(Result, "Cambio Fecha Vencimiento")
                RaiseEvent ChangedExpireDate(Result)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        Friend Sub ChangeExpireDateWithoutLogAction(ByRef Result As TaskResult, ByVal NuevaFecha As DateTime)
            Try
                Result.ExpireDate = NuevaFecha
                WTF.UpdateExpiredDate(Result.TaskId, Result.ExpireDate)
                RaiseEvent ChangedExpireDate(Result)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        Public Sub ChangeExpireDate(ByVal taskId As Int64, ByVal expireDate As Date)
            WTF.UpdateExpiredDate(taskId, expireDate)


        End Sub

        Private Sub RefreshChangeExpireDate(ByVal t As TaskResult, ByVal r As DsDocuments.DocumentsRow)
            Try
                If IsDBNull(r("ExpireDate")) Then
                    If Not t.ExpireDate = #12:00:00 AM# Then
                        t.ExpireDate = Nothing
                        RaiseEvent ChangedExpireDate(t)
                    End If
                Else
                    Dim ExpireDate As Date = CDate(r("ExpireDate"))
                    If t.ExpireDate <> ExpireDate Then
                        t.ExpireDate = ExpireDate
                        RaiseEvent ChangedExpireDate(t)
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#End Region

#Region "CheckIn"
        Private Sub RefreshCheckIn(ByVal t As TaskResult, ByVal r As DsDocuments.DocumentsRow)
            Try
                If IsDBNull(r("CheckIn")) Then
                    If Not t.CheckIn = #12:00:00 AM# Then
                        t.CheckIn = Nothing
                    End If
                Else
                    Dim CheckIn As Date = CDate(r("CheckIn"))
                    If t.CheckIn <> CheckIn Then
                        t.CheckIn = CheckIn
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#End Region

#Region "AsignedBy"
        Private Sub RefreshAsignedBy(ByVal t As TaskResult, ByVal r As DsDocuments.DocumentsRow)
            Dim WFB As New WFBusiness
            Try
                If t.AsignedById <> 0 Then
                    If t.AsignedById <> (r.User_Asigned_By) Then
                        If WFB.GetAllUsers.Contains(Convert.ToInt64(r.User_Asigned_By)) Then

                            t.AsignedById = CInt(r.User_Asigned_By)
                        Else
                            t.AsignedById = DirectCast(WFB.GetAllUsers().GetByIndex(0), User).ID
                        End If
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                WFB = Nothing
            End Try

        End Sub
#End Region

#Region "AsignedDate"
        Private Sub RefreshAsignedDate(ByVal t As TaskResult, ByVal r As DsDocuments.DocumentsRow)
            Try
                If IsDBNull(r("Date_Asigned_By")) Then
                    If Not t.AsignedDate = #12:00:00 AM# Then
                        t.AsignedDate = Nothing
                    End If
                Else
                    Dim DateAsignedBy As Date = CDate(r("Date_Asigned_By"))
                    If t.AsignedDate <> DateAsignedBy Then
                        t.AsignedDate = DateAsignedBy
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#End Region

#End Region

#Region "Load"
        Public Sub LoadMonitor(ByVal WF As WorkFlow, ByVal TreeView As TreeView)
            Try
                TreeView.Nodes.Clear()

                'Nodo Inicial
                Dim Toproot As New WFNode(WF)
                TreeView.Nodes.Add(Toproot)

                'Nodos de Etapas
                For Each s As WFStep In WF.Steps.Values
                    Try
                        Dim StepNode As New MonitorStepNode(DirectCast(s, WFStep))
                        Toproot.Nodes.Add(StepNode)
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Next

                TreeView.ExpandAll()
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        'Friend Shared Sub LoadClient(ByVal WFs As Hashtable, ByVal TreeView As TreeView)
        '    Try
        '        TreeView.Nodes.Clear()

        '        Dim Root As New InitNode
        '        Root.Text = "Inicio"
        '        TreeView.Nodes.Add(Root)

        '        For Each wf As Int64 In WFs.Keys
        '            Dim WFNode As New WFNodeIdandName(wf, WFs(wf))
        '            'Dim wfnode As New TreeNode(WFs(wf))
        '            'wfnode.Tag = wf
        '            Root.Nodes.Add(wfnode)
        '            '  WFNode.Expand()

        '            Dim wfsteps As Hashtable = WFStepBusiness.GetHashTableSteps(wf)
        '            If Not IsNothing(wfsteps) Then
        '                For Each s As Object In wfsteps.Keys
        '                    Dim tasks As Hashtable = GetHashTableTasksByStep(s)
        '                    Dim WFStepNode As New StepNodeIdAndName(s, wfsteps(s), tasks.Count)
        '                    'Dim WFStepNode As New TreeNode(wfsteps(s))
        '                    'WFStepNode.Tag = s.ToString()
        '                    WFNode.Nodes.Add(WFStepNode)
        '                Next
        '            End If
        '        Next

        '        Root.Expand()
        '    Catch ex As Exception
        '        ZClass.raiseerror(ex)
        '    End Try
        'End Sub
#End Region

#Region "Nodes"
        Delegate Sub SetText()

        Private Sub UpdateNodesTasksCountExtracted(ByVal n As TreeNode)
            DirectCast(n, MonitorStepNode).UpdateNodeText()
        End Sub

        Public Sub UpdateNodesTasksCount(ByVal WFNode As WFNode)
            Try
                For Each n As TreeNode In WFNode.Nodes
                    UpdateNodesTasksCountExtracted(n)
                Next
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
#End Region

#Region "Logs"


        Public Sub LogTask(ByVal TaskResult As ITaskResult, ByVal comments As String)

            Dim TypeName = "Sin entidad"
            Dim StepNameById = "Sin etapa"
            Dim StepStateById = "Sin estado"
            Dim WorkflowNameByWFId = "Sin proceso"

            Try
                If (TaskResult.DocTypeId <> 0) Then
                    TypeName = New DocTypesBusiness().GetDocTypeName(TaskResult.DocTypeId)
                End If
                StepNameById = WFStepBusiness.GetStepNameById(TaskResult.StepId)
                StepStateById = New WFStepStatesComponent().GetStepStateById(TaskResult.StateId).Name
                WorkflowNameByWFId = New WFBusiness().GetWorkflowNameByWFId(TaskResult.WorkId)

            Catch ex As Exception

            End Try

            WTF.LogTask(
                TaskResult.TaskId,
                TaskResult.Name,
                TaskResult.DocTypeId,
                TypeName,
                0,
                TaskResult.StepId, StepNameById,
                StepStateById,
                TaskResult.WorkId, WorkflowNameByWFId,
                Membership.MembershipHelper.CurrentUser.Name, comments
                )
        End Sub


#End Region

#Region "Save"
        Public Sub SaveIntoIP_Task(ByVal alFiles As ArrayList, ByVal ZipOrigen As String, ByVal conf_Id As Decimal)
            If alFiles.Count > 0 Then
                Try
                    Dim i As Integer
                    For i = 0 To alFiles.Count - 1
                        WTF.SaveIntoIP_Task(alFiles(i).ToString, CoreBusiness.GetNewID(IdTypes.IPTASKID), ZipOrigen, conf_Id)
                    Next
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                    'log.WriteEvent(Environment.UserName, Now, "Save Zip Files", exc.Message, Me.conf_Id, Environment.MachineName)
                End Try
            End If
        End Sub

        Public Sub SaveIntoIP_Task(ByVal alFiles As ArrayList, ByVal conf_Id As Decimal)
            If alFiles.Count > 0 Then
                Try
                    Dim i As Integer
                    For i = 0 To alFiles.Count - 1
                        WTF.SaveIntoIP_Task(alFiles(i), CoreBusiness.GetNewID(IdTypes.IPTASKID), conf_Id)
                    Next
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End If
        End Sub
#End Region

#Region "WebParts"

        Public Function GetTasksToExpireGroupByStep(ByVal workflowid As Int64, ByVal FromHours As Int32, ByVal ToHours As Int32) As DataSet
            Return WTF.GetTasksToExpireGroupByStep(workflowid, FromHours, ToHours)
        End Function

        Public Function GetTasksToExpireGroupByUser(ByVal workflowid As Int64, ByVal FromHours As Int32, ByVal ToHours As Int32) As DataSet
            Return WTF.GetTasksToExpireGroupByUser(workflowid, FromHours, ToHours)
        End Function

        Public Function GetExpiredTasksGroupByUser(ByVal workflowid As Int64) As DataSet
            Return WTF.GetExpiredTasksGroupByUser(workflowid)
        End Function

        Public Function GetExpiredTasksGroupByStep(ByVal workflowid As Int64) As DataSet
            Return WTF.GetExpiredTasksGroupByStep(workflowid)
        End Function


        Public Function GetTasksBalanceGroupByWorkflow(ByVal workflowid As Int64) As DataSet
            Return WTF.GetTasksBalanceGroupByWorkflow(workflowid)
        End Function

        Public Function GetTasksBalanceGroupByStep(ByVal stepid As Int64) As DataSet
            Return WTF.GetTasksBalanceGroupByStep(stepid)
        End Function

        Public Function GetMyTasks(ByVal userId As Int64) As List(Of TaskDTO)
            Dim newsDataset As DataSet = WTF.GetMyTasks(userId)
            Dim Tasks As New List(Of TaskDTO)
            If newsDataset IsNot Nothing AndAlso newsDataset.Tables(0) IsNot Nothing Then
                For Each row As DataRow In newsDataset.Tables(0).Rows
                    Tasks.Add(New TaskDTO(row("Tarea"), row("Task_id"), row("doc_id"), row("DOC_TYPE_ID"),
                                          row("Ingreso"), row("Etapa"), row("Asignado"), row("Ingreso"), row("Vencimiento")))
                Next
            End If
            Return Tasks
        End Function
        Public Function GetMyTasksCount(userId As Long) As Long
            Return WTF.GetMyTasksCount(userId)
        End Function

        Public Function GetRecentTasks(ByVal userId As Int64) As DataSet
            Return WTF.GetRecentTasks(userId)
        End Function
        Public Function GetRecentTasksCount(userId As Long) As Long
            Return WTF.GetRecentTasksCount(userId)
        End Function
        Public Function GetAsignedTasksCountsGroupByUser(ByVal workflowid As Int64) As DataSet
            Return WTF.GetAsignedTasksCountsGroupByUser(workflowid)
        End Function

        Public Function GetAsignedTasksCountsGroupByStep(ByVal workflowid As Int64) As DataSet
            Return WTF.GetAsignedTasksCountsGroupByStep(workflowid)
        End Function

        Public Function GetTaskConsumedMinutesByWorkflowGroupByUsers(ByVal workflowid As Int64) As DataSet
            Return WTF.GetTaskConsumedMinutesByWorkflowGroupByUsers(workflowid)
        End Function

        Public Function GetTaskConsumedMinutesByStepGroupByUsers(ByVal stepid As Int64) As DataSet
            Return WTF.GetTaskConsumedMinutesByStepGroupByUsers(stepid)
        End Function
        Public Function GetTasksAverageTimeInSteps(ByVal workflowid As Int64) As Hashtable
            Return WTF.GetTasksAverageTimeInSteps(workflowid)
        End Function

        Public Function GetTasksAverageTimeByStep(ByVal stepid As Int64) As Hashtable
            Return WTF.GetTasksAverageTimeByStep(stepid)
        End Function
        Public Function GetTaskHistory(ByVal task_id As Int64) As DataSet
            Return WTF.GetTaskHistoryByResultId(task_id)
        End Function

        Public Function GetOnlyIndexsHistory(ByVal doc_Id As Int64) As DataSet
            Return WTF.GetOnlyIndexsHistory(doc_Id)
        End Function
        ''' <summary>
        ''' Devuelve si la tarea fue modificada en el servidor o no desde la fecha pasada como parametro
        ''' </summary>
        ''' <param name="TaskId">Id de la tarea</param>
        ''' <param name="modifiedDate">Fecha de la anterior modificacion</param>
        ''' <history>Marcelo created 26/02/2009</history>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetIFModifiedTaskHistoryByResultId(ByVal TaskId As Int64, ByVal modifiedDate As DateTime) As Boolean
            Dim lastModified As DateTime = WTF.GetLastModifiedTaskHistoryByResultId(TaskId)

            If DateTime.Compare(lastModified, modifiedDate) > 0 Then
                Return True
            Else
                Return False
            End If
        End Function


        ''' <summary>
        ''' Crea las condiciones para el where sobre las restricciones para los documentos y las va agregando a ColumCndstring
        ''' </summary>
        ''' <param name="valueString"></param>
        ''' <param name="Indexs"></param>
        ''' <param name="i"></param>
        ''' <param name="FlagCase"></param>
        ''' <param name="ColumCondstring"></param>
        ''' <param name="First"></param>
        ''' <param name="dateDeclarationString"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''        [Javier] 06/10/10    Created
        '''        [Javier] 12/10/10    Modified    Se reemplaza nombre de columna por nombre de campo en tabla
        ''' </history>
        Private Sub CreateTasksRestrictionsWhere(ByRef valueString As StringBuilder, ByVal Indexs() As Index, ByRef i As Int64, ByRef FlagCase As Boolean, ByRef ColumCondstring As StringBuilder, ByRef First As Boolean, ByRef dateDeclarationString As StringBuilder)
            Dim Valuestring1 As Object = Nothing
            Dim Valuestring3 As Object = Nothing
            Dim dateString As New StringBuilder


            valueString.Remove(0, valueString.Length)
            If (String.IsNullOrEmpty(Indexs(i).dataDescription)) Then
                valueString.Append(Indexs(i).Data)
            Else
                valueString.Append(Indexs(i).dataDescription)
            End If

            'Dim IndexColumnName As String = FiltersComponent.FormatColumnName(Indexs(i).Name)
            Dim IndexColumnName As String = Indexs(i).Column

            If valueString.Length <> 0 OrElse Indexs(i).[Operator].ToLower = "es nulo" Then
                If valueString.ToString.Split(";").Length > 1 Then
                    Select Case Indexs(i).Type
                        Case IndexDataType.Alfanumerico
                            FlagCase = True
                            Valuestring1 = "'" & LCase(valueString.Replace(";", "';'").ToString) & "'"
                        Case IndexDataType.Alfanumerico_Largo
                            FlagCase = True
                            Valuestring1 = "'" & LCase(valueString.Replace(";", "';'").ToString) & "'"
                    End Select
                Else
                    Select Case Indexs(i).Type
                        Case IndexDataType.Numerico, IndexDataType.Numerico_Largo
                            If valueString.Length <> 0 Then
                                If Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                    Valuestring1 = "'" & valueString.ToString & "'"
                                Else
                                    'Valuestring1 = Int64.Parse(valueString.ToString)
                                    Valuestring1 = valueString.ToString()
                                End If
                            End If
                        Case IndexDataType.Numerico_Decimales, IndexDataType.Moneda
                            If valueString.Length <> 0 Then
                                If System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString = "," Then valueString = valueString.Replace(".", ",")
                                Valuestring1 = CDec(valueString.ToString)
                                Valuestring1 = Valuestring1.ToString.Replace(",", ".")
                            End If
                        Case IndexDataType.Si_No
                            If valueString.Length <> 0 Then
                                Valuestring1 = Int64.Parse(valueString.ToString)
                            End If
                        Case IndexDataType.Fecha
                            If Not String.IsNullOrEmpty(valueString.ToString.Trim) Then
                                If Server.isSQLServer Then
                                    'Se optimiza para sql server
                                    Valuestring1 = "@fecdesde" & Indexs(i).Column
                                    dateString.Append("declare " & Valuestring1 & " datetime ")
                                    dateString.Append("set " & Valuestring1 & " = " & Server.Con.ConvertDate(valueString.ToString) & " ")
                                    dateDeclarationString.Append(dateString)
                                Else
                                    Valuestring1 = Server.Con.ConvertDate(valueString.ToString)
                                End If
                            End If
                        Case IndexDataType.Fecha_Hora
                            If Not String.IsNullOrEmpty(valueString.ToString.Trim) Then
                                If Server.isSQLServer Then
                                    'Se optimiza para sql server
                                    Valuestring1 = "@fecdesde" & Indexs(i).Column
                                    dateString.Append("declare " & Valuestring1 & " datetime ")
                                    dateString.Append("set " & Valuestring1 & " = " & Server.Con.ConvertDateTime(valueString.ToString) & " ")
                                    dateDeclarationString.Append(dateString)
                                Else
                                    Valuestring1 = Server.Con.ConvertDateTime(valueString.ToString)
                                End If
                            End If
                        Case IndexDataType.Alfanumerico, IndexDataType.Alfanumerico_Largo
                            If FlagCase Then
                                Valuestring1 = "'" & LCase(valueString.ToString) & "'"
                            Else
                                Valuestring1 = "'" & valueString.ToString & "'"
                            End If
                    End Select
                End If

                Dim Op As String = Valuestring1.ToString
                If Op.Contains("''") Then Op = Op.Replace("''", "'")
                Valuestring1 = Op
                Op = String.Empty

                Dim separator As String = " AND"
                If ColumCondstring.Length > 0 Then
                    ColumCondstring.Append(separator)
                End If

                Select Case Indexs(i).[Operator]
                    Case "="
                        Op = "="
                        If FlagCase = True AndAlso (Indexs(i).Type = IndexDataType.Alfanumerico OrElse Indexs(i).Type = IndexDataType.Alfanumerico_Largo) Then
                            ColumCondstring.Append(" (lower(" & IndexColumnName & ") " & Op & " " & Valuestring1 & ")")
                        Else
                            ColumCondstring.Append(" (" & IndexColumnName & " " & Op & " " & Valuestring1 & ")")
                        End If

                    Case ">"
                        Op = ">"
                        ColumCondstring.Append(" (" & IndexColumnName & " " & Op & " " & Valuestring1 & ")")

                    Case "<"
                        Op = "<"
                        ColumCondstring.Append(" (" & IndexColumnName & " " & Op & " " & Valuestring1 & ")")
                    Case "Es nulo"
                        Op = "is null"
                        ColumCondstring.Append(" (" & IndexColumnName & " " & Op & " " & ")")

                    Case ">="
                        Op = ">="
                        ColumCondstring.Append(" (" & IndexColumnName & " " & Op & " " & Valuestring1 & ")")

                    Case "<="
                        Op = "<="
                        ColumCondstring.Append(" (" & IndexColumnName & " " & Op & " " & Valuestring1 & ")")
                    Case "<>"
                        Op = "<>"
                        ColumCondstring.Append(" (" & IndexColumnName & " " & Op & " " & Valuestring1 & " or " & IndexColumnName & " is null)")
                    Case "Entre"
                        Dim Data2Added As Boolean
                        Try
                            'cambio las a como indice a I ya que todos los indices vienen con dato algunos vacio otros no.
                            Select Case Indexs(i).Type
                                Case 1, 2
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        Valuestring3 = Int64.Parse(Indexs(i).Data2)
                                        Data2Added = True
                                    End If
                                Case 3
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        Valuestring3 = Decimal.Parse(Indexs(i).Data2)
                                        Data2Added = True
                                    End If
                                Case 4
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        If Not String.IsNullOrEmpty(valueString.ToString.Trim) Then
                                            If Server.isSQLServer Then
                                                'Se optimiza para sql server
                                                Valuestring3 = "@fechasta" & IndexColumnName
                                                dateString.Remove(0, dateString.Length)
                                                dateString.Append("declare " & Valuestring3 & " datetime ")
                                                dateString.Append("set " & Valuestring3 & " = " & Server.Con.ConvertDate(Indexs(i).Data2) & " ")
                                                dateDeclarationString.Append(dateString)
                                            Else
                                                Valuestring3 = Server.Con.ConvertDate(Indexs(i).Data2)
                                            End If
                                        End If
                                        Data2Added = True
                                    End If
                                Case 5
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        'Valuestring3 = Server.Con.ConvertDateTime(Indexs(i).Data2)
                                        If Not String.IsNullOrEmpty(valueString.ToString.Trim) Then
                                            If Server.isSQLServer Then
                                                'Se optimiza para sql server
                                                Valuestring3 = "@fechasta" & IndexColumnName
                                                dateString.Remove(0, dateString.Length)
                                                dateString.Append("declare " & Valuestring3 & " datetime ")
                                                dateString.Append("set " & Valuestring3 & " = " & Server.Con.ConvertDateTime(Indexs(i).Data2) & " ")
                                                dateDeclarationString.Append(dateString)
                                            Else
                                                Valuestring3 = Server.Con.ConvertDateTime(Indexs(i).Data2)
                                            End If
                                        End If
                                        Data2Added = True
                                    End If
                                Case 6
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        Valuestring3 = CDec(Indexs(i).Data2)
                                        Data2Added = True
                                    End If
                                Case 7, 8
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        FlagCase = True
                                        Valuestring3 = "'" & LCase(DirectCast(Indexs(i).Data2, String)) & "'"
                                        Data2Added = True
                                    End If
                            End Select
                        Catch ex As Exception
                            Throw New Exception("Ocurrio un error al convertir al tipo de Dato: Dato: " & valueString.ToString & ", Tipo Dato: " & Indexs(i).Type & " " & ex.ToString)

                        End Try

                        If Data2Added = True Then
                            If Server.isSQLServer And (Indexs(i).Type = IndexDataType.Fecha OrElse Indexs(i).Type = IndexDataType.Fecha_Hora) Then

                                ColumCondstring.Append(" (" & IndexColumnName & " BETWEEN " & Valuestring1 & " and " & Valuestring3 & ")")
                            Else
                                ColumCondstring.Append(" (" & IndexColumnName & " >= " & Valuestring1 & " and " & IndexColumnName & " <= " & Valuestring3 & ")")
                            End If
                            Data2Added = False
                        End If
                    Case "Contiene"
                        If FlagCase = True Then
                            ColumCondstring.Append(" (lower(" & IndexColumnName & ") Like '%" & Replace(Trim(Valuestring1), "'", "") & "%')")
                        Else
                            ColumCondstring.Append(" (" & IndexColumnName & " Like '%" & Replace(Trim(Valuestring1), "'", "") & "%')")
                        End If
                    Case "Empieza"
                        If FlagCase = True Then
                            ColumCondstring.Append(" (lower(" & IndexColumnName & ") Like '" & Replace(Trim(Valuestring1), "'", "") & "%')")
                        Else
                            ColumCondstring.Append(" (" & IndexColumnName & " Like '" & Replace(Trim(Valuestring1), "'", "") & "%')")
                        End If
                    Case "Termina"
                        If FlagCase = True Then
                            ColumCondstring.Append(" (lower(" & IndexColumnName & ") Like '%" & Replace(Trim(Valuestring1), "'", "") & "')")
                        Else
                            ColumCondstring.Append(" (" & IndexColumnName & " Like '%" & Replace(Trim(Valuestring1), "'", "") & "')")
                        End If

                    Case "Alguno"
                        Op = "LIKE"
                        Valuestring1 = Valuestring1.Replace(";", ",")
                        Valuestring1 = Valuestring1.Replace("  ", " ")
                        Valuestring1 = Valuestring1.Replace(" ", ",")
                        Dim SomeValues As Array = DirectCast(Valuestring1, String).Split(",")
                        Dim x As Int32
                        Dim somestring As String = String.Empty
                        For x = 0 To SomeValues.Length - 1
                            Dim Val As String = SomeValues(x)
                            If IsNothing(Val) = False AndAlso Not String.IsNullOrEmpty(Val.Trim) Then
                                If i = 0 AndAlso x = 0 Then
                                    If FlagCase = True Then
                                        somestring = " (lower(" & IndexColumnName & ") " & Op & " ('%" & Val.Replace("'", "") & "%')"
                                    Else
                                        somestring = " (" & IndexColumnName & " " & Op & " ('%" & Val.Replace("'", "") & "%')"
                                    End If
                                ElseIf x > 0 Then
                                    If String.IsNullOrEmpty(somestring) Then
                                        If FlagCase = True Then
                                            somestring &= separator & " lower(" & DirectCast(IndexColumnName, String) & ") " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        Else
                                            somestring &= separator & " " & DirectCast(IndexColumnName, String) & " " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        End If
                                    Else
                                        separator = " or "
                                        If FlagCase = True Then
                                            somestring &= separator & " lower(" & IndexColumnName & ") " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        Else
                                            somestring &= separator & " " & IndexColumnName & " " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        End If
                                    End If
                                End If
                            End If
                        Next
                        If Not String.IsNullOrEmpty(somestring) Then ColumCondstring.Append(somestring & ")")
                        SomeValues = Nothing
                        somestring = Nothing
                    Case "Distinto"
                        Op = "NOT LIKE"
                        Valuestring1 = Valuestring1.Replace(";", ",")
                        Valuestring1 = Valuestring1.Replace("  ", " ")
                        Valuestring1 = Valuestring1.Replace(" ", ",")
                        Dim SomeValues As Array = DirectCast(Valuestring1, String).Split(",")
                        Dim x As Int32
                        Dim somestring As String = String.Empty
                        For x = 0 To SomeValues.Length - 1
                            Dim Val As String = SomeValues(x)
                            If IsNothing(Val) = False AndAlso Not String.IsNullOrEmpty(Val.Trim) Then
                                If x = 0 And i = 0 Then
                                    If FlagCase = True Then
                                        somestring = " (lower(" & IndexColumnName & ") " & Op & " ('%" & Val.Replace("'", "") & "%')"
                                    Else
                                        somestring = " (" & IndexColumnName & " " & Op & " ('%" & Val.Replace("'", "") & "%')"
                                    End If
                                ElseIf x = 0 Then
                                    If FlagCase = True Then
                                        somestring = " (lower(" & IndexColumnName & ") " & Op & " ('%" & Val.Replace("'", "") & "%')"
                                    Else
                                        somestring = " (" & IndexColumnName & " " & Op & " ('%" & Val.Replace("'", "") & "%')"
                                    End If
                                Else
                                    If String.IsNullOrEmpty(somestring) Then
                                        If FlagCase = True Then
                                            somestring &= separator & " lower(" & DirectCast(IndexColumnName, String) & ") " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        Else
                                            somestring &= separator & " " & DirectCast(IndexColumnName, String) & " " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        End If
                                    Else
                                        separator = " or "
                                        If FlagCase = True Then
                                            somestring &= separator & " lower(" & IndexColumnName & ") " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        Else
                                            somestring &= separator & " " & IndexColumnName & " " & Op & " ('%" & Replace(Val, "'", "").Trim & "%')"
                                        End If
                                    End If
                                End If
                            End If
                        Next
                        If Not String.IsNullOrEmpty(somestring) Then ColumCondstring.Append(somestring & ")")
                        SomeValues = Nothing
                        somestring = Nothing
                    Case "Dentro" 'jhp
                        If FlagCase = True Then
                            Select Case Indexs(i).Type
                                Case IndexDataType.Numerico, IndexDataType.Numerico_Largo, IndexDataType.Moneda
                                    If Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        ColumCondstring.Append(" (lower(" & IndexColumnName & ") in (" & Valuestring1 & "'))")
                                    Else
                                        ColumCondstring.Append(" (lower(" & IndexColumnName & ") in (" & Valuestring1 & "))")
                                    End If
                                Case Else
                                    ColumCondstring.Append(" (lower(" & IndexColumnName & ") in (" & Valuestring1 & "'))")
                            End Select
                        Else
                            Select Case Indexs(i).Type
                                Case IndexDataType.Numerico, IndexDataType.Numerico_Largo, IndexDataType.Moneda
                                    If Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        ColumCondstring.Append(" (" & IndexColumnName & ") in (" & Valuestring1 & "')")
                                    Else
                                        ColumCondstring.Append(" (" & IndexColumnName & ") in (" & Valuestring1 & ")")
                                    End If
                                Case Else
                                    ColumCondstring.Append(" (" & IndexColumnName & ") in (" & Valuestring1 & "')")
                            End Select
                        End If
                End Select
                Op = Nothing
                separator = Nothing
            End If

        End Sub

#End Region

#Region "History"




        ''' <summary>
        ''' Método que sirve para obtener un dataset que tendrá el historial de los task id seleccionados (RequestAction)
        ''' </summary>
        ''' <param name="tasksIds"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston] 22/07/2008 Created
        ''' </history>
        Public Function getTasksRequestActionHistory(ByRef taskId As Int64) As DataSet
            Return (WTF.getTasksRequestActionHistory(taskId))
        End Function

#End Region

#Region "ExecuteEventRules"
        Public Sub ExecutedSetIndexsRules(ByVal TaskResult As ITaskResult)
            Dim Results As New System.Collections.Generic.List(Of Core.ITaskResult)
            Dim WFSB As New WFStepBusiness

            Try
                Results.Add(TaskResult)
                Dim wfstep As IWFStep = WFSB.GetStepById(TaskResult.StepId)
                If wfstep.Rules.Count = 0 Then
                    Dim WFRulesBusiness As New WFRulesBusiness
                    WFRulesBusiness.FillRules(wfstep)
                End If
                For Each Rule As WFRuleParent In wfstep.Rules
                    If Rule.RuleType = TypesofRules.Indices Then
                        Dim WFRB As New WFRulesBusiness
                        Dim list As List(Of ITaskResult) = WFRB.ExecuteRule(Rule, Results, False)
                        list = Nothing
                        WFRB = Nothing
                    End If
                Next
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                Dim i As Int32
                For i = 0 To Results.Count - 1
                    If Not IsNothing(Results(i)) Then
                        Results(i).Dispose()
                        Results(i) = Nothing
                    End If
                Next
                Results.Clear()
                Results = Nothing
                WFSB = Nothing
            End Try
        End Sub

#End Region

        ''' <summary>
        ''' Guarda la ultima modificacion 
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <param name="comment"></param>
        ''' <remarks></remarks>
        Public Function SetLastUpdate(ByVal Result As ITaskResult, ByVal comment As String, ByVal saveHistory As Boolean, userId As Int64, details As String) As Int64 Implements IWFTaskBusiness.SetLastUpdate
            Dim newsID As Int64 = CoreData.GetNewID(IdTypes.News)
            comment = TextoInteligente.ReconocerCodigo(comment, Result)
            Dim NB As New NewsBusiness
            NB.SaveNews(newsID, Result.ID, Result.DocTypeId, comment, userId, details)

            If saveHistory = True Then
                LogTask(Result, comment)

            End If
            Return newsID
        End Function

        ''' <summary>
        ''' Obtiene el Count para un step, para un determinado usuario
        ''' </summary>
        ''' <param name="stepId"></param>
        ''' <param name="WithGridRights"></param>
        ''' <param name="CurrentUserID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTaskCount(ByVal stepId As Int64, ByVal WithGridRights As Boolean, ByVal CurrentUserID As Int64) As Int64
            Try
                Dim WFSB As New WFStepBusiness
                Dim dt As DataTable = WFSB.GetDocTypesByWfStepAsDT(stepId, CurrentUserID)

                Return dt.Rows.Cast(Of DataRow)().Aggregate(Of Long)(0, Function(current, r) (current + GetTasksCountByStepandDocTypeId(stepId, r.Item(0), WithGridRights, CurrentUserID, Nothing)))
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Function

        Public Shared Function ValidateVar(ByVal r As ITaskResult, ByVal TxtVar As String, ByVal Operador As String, ByVal Value As String) As Boolean

            Dim zValue, zvar, rulevalue As String
            Dim ds As DataSet
            Dim taskResult As New Generic.List(Of Core.ITaskResult)()

            zValue = String.Empty
            zvar = String.Empty

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando Variable")
            If TxtVar.IndexOf("zvar(") <> -1 Then
                zvar = TxtVar.Replace("zvar(", "")
                zvar = zvar.Replace(")", "").Trim()
            Else
                zvar = TxtVar
            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable original: " & zvar)
            zvar = Zamba.Core.TextoInteligente.ReconocerCodigo(zvar, r)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable pasada por textointeligente: " & zvar)
            zvar = zvar.Trim()
            If VariablesInterReglas.ContainsKey(zvar) Then
                If (TypeOf (VariablesInterReglas.Item(zvar)) Is DataSet) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable DataSet")
                    ds = VariablesInterReglas.Item(zvar)
                    If Not IsNothing(ds) AndAlso Not IsDBNull(ds) AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 AndAlso Not IsDBNull(ds.Tables(0).Rows(0)) Then
                        zValue = ds.Tables(0).Rows(0)(1).ToString()
                    Else
                        'Return 0
                        zValue = ""
                    End If
                ElseIf (VariablesInterReglas.Item(zvar)) Is Nothing Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable Vacia")
                    zValue = ""
                Else
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable de otro tipo")
                    zValue = VariablesInterReglas.Item(zvar)
                End If
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se ha encontrado la variable")
                zValue = zvar
            End If


            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable Obtenida: " & zValue)

            rulevalue = String.Empty
            Dim VarInterReglas As New VariablesInterReglas()
            rulevalue = VarInterReglas.ReconocerVariablesValuesSoloTexto(Value)
            VarInterReglas = Nothing

            rulevalue = TextoInteligente.ReconocerCodigo(rulevalue, r)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable a comparar Obtenida: " & rulevalue)

            If String.IsNullOrEmpty(rulevalue) Then
                rulevalue = Value
            End If

            Return ToolsBusiness.ValidateComp(zValue, rulevalue, Operador, False)
        End Function


        Public Shared Function LockTask(ByVal taskId As Int64, ByRef currentLockedUser As String) As Boolean

            Dim StrBuilder As New StringBuilder
            StrBuilder.Append("Update WFDocument Set ")
            Dim StrWhere As String

            If Server.isOracle Then
                StrBuilder.Append(" c_exclusive = " & Membership.MembershipHelper.CurrentUser.ID)
                StrBuilder.Append(",LastUpdateDate=sysdate")
                StrWhere = " and (c_exclusive = 0 or c_exclusive = " & Membership.MembershipHelper.CurrentUser.ID & ")"
            Else
                StrBuilder.Append(" exclusive = " & Membership.MembershipHelper.CurrentUser.ID)
                StrBuilder.Append(",LastUpdateDate=getdate()")
                StrWhere = " and (exclusive = 0 or exclusive = " & Membership.MembershipHelper.CurrentUser.ID & ")"
            End If

            StrBuilder.Append(" where task_id=" & taskId & StrWhere)

            Dim AffectedRows As Int64 = Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString)
            If AffectedRows > 0 Then
                Return True
            Else
                currentLockedUser = Server.Con.ExecuteScalar(CommandType.Text, String.Format("select (APELLIDO " & If(Server.isOracle, "|| ", "+ ") & "', '" & If(Server.isOracle, "|| ", "+ ") & "NOMBRES) as Usuario from usrtable where id = (select " & If(Server.isOracle, "C_EXCLUSIVE ", "exclusive ") & "from wfdocument where task_id = {0})", taskId))
                Return False
            End If
        End Function

        Function LockTasks(TasksIds As List(Of Int64)) As Boolean
            Dim AllLocked As Boolean = False
            For Each TaskId As Int64 In TasksIds
                Dim currentLockedUser As String
                If Not LockTask(TaskId, currentLockedUser) Then
                    AllLocked = False
                    Exit For
                End If
            Next

            If AllLocked = False Then
                For Each TaskId As Int64 In TasksIds
                    UnLockTask(TaskId)
                Next
            End If

        End Function

        Public Function UnLockTask(TaskId As Int64) As Boolean
            Dim StrBuilder As New StringBuilder
            Dim StrWhere As String
            StrBuilder.Append("Update WFDocument Set ")

            If Server.isOracle Then
                StrBuilder.Append(" c_exclusive = 0")
                StrBuilder.Append(",LastUpdateDate=sysdate")
                StrWhere = " and  c_exclusive = " & Membership.MembershipHelper.CurrentUser.ID
            Else
                StrBuilder.Append(" exclusive = 0")
                StrBuilder.Append(",LastUpdateDate=getdate()")
                StrWhere = " and exclusive = " & Membership.MembershipHelper.CurrentUser.ID
            End If

            StrBuilder.Append(" where task_id=" & TaskId & StrWhere)

            Dim AffectedRows As Int64 = Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString)
            If AffectedRows > 0 Then
                Return True
            Else
                Return False
            End If
        End Function


        Public Shared Function UnLockTasks(UserId As Int64) As Boolean

            Dim StrBuilder As New StringBuilder
            Dim StrWhere As String
            StrBuilder.Append("Update WFDocument Set ")

            If Server.isOracle Then
                StrBuilder.Append(" c_exclusive = 0")
                StrBuilder.Append(",LastUpdateDate=sysdate")
                StrWhere = " where  c_exclusive = " & UserId
            Else
                StrBuilder.Append(" exclusive = 0")
                StrBuilder.Append(",LastUpdateDate=getdate()")
                StrWhere = " where exclusive = " & UserId
            End If

            StrBuilder.Append(StrWhere)

            Dim AffectedRows As Int64 = Server.Con.ExecuteNonQuery(CommandType.Text, StrBuilder.ToString)
            If AffectedRows > 0 Then
                Return True
            Else
                Return False
            End If
        End Function


        Public Function GetDocTypesIdsByDocId(ByVal docId As Int64) As DataSet
            Dim QueryBuilder As New StringBuilder
            Try
                QueryBuilder.Append("select doc_type_id from wfdocument where doc_id = ")
                QueryBuilder.Append(docId.ToString())
                Return Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
            Finally
                QueryBuilder.Remove(0, QueryBuilder.Length)
                QueryBuilder = Nothing
            End Try

        End Function

        Public Shared Function setCExclusive(taskId As Int64, value As Int32) As Int16
            Dim WF As New WFTasksFactory
            Return WF.setCExclusive(taskId, value)
        End Function


    End Class
End Namespace
