Imports Zamba.Filters
Imports Zamba.Core.Enumerators
Imports Zamba.Data
Imports System.Collections.Generic
Imports System.Text
Imports Zamba.Servers

Namespace WF.WF
    Public Class WFTaskBusiness
        Implements IWFTaskBusiness

#Region "Get"

        Public Shared Function GetTasksNamesByTaskIds(ByVal taskIds As List(Of Int64)) As DataTable

            Dim dsDocs As DataTable = Zamba.Data.WFTasksFactory.GetTasksNamesByTaskIds(taskIds)

            Return dsDocs

        End Function


        ''' <summary>
        ''' Este metodo se usa para instanciar un taskresult completo para la ejecucion de las tareas en el cliente de Zamba
        ''' </summary>
        ''' <param name="r"></param>
        ''' <param name="s"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function NewResult(ByVal r As DataRow, ByVal s As WFStep, Optional ByVal inThread As Boolean = False) As TaskResult
            Try
                Dim Doctype As DocType = DocTypesBusiness.GetDocType(CInt(r.Item(GridColumns.DOC_TYPE_ID_COLUMNNAME)), True)
                Dim Task_State_ID As Int32
                If Not IsNothing(Doctype) Then
                    Task_State_ID = CInt(r.Item(GridColumns.TASK_STATE_ID_COLUMNNAME))

                    Dim ResultName As String = Nothing

                    If Not IsDBNull(r.Item(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME)) Then
                        ResultName = r.Item(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME)
                    End If

                    If IsNothing(ResultName) Then
                        ResultName = Doctype.Name
                    End If
                    Dim Result As TaskResult
                    If Server.isOracle Then
                        Result = New TaskResult(s.ID, CLng(r.Item(GridColumns.TASK_ID_COLUMNNAME)), CLng(r.Item(GridColumns.DOC_ID_COLUMNNAME)), Doctype, ResultName, CInt(r.Item(GridColumns.ICONID_COLUMNNAME)), CInt(r.Item("C_Exclusive")), DirectCast(Task_State_ID, Zamba.Core.TaskStates), Doctype.Indexs, s.InitialState)
                    Else
                        Result = New TaskResult(s.ID, CLng(r.Item(GridColumns.TASK_ID_COLUMNNAME)), CLng(r.Item(GridColumns.DOC_ID_COLUMNNAME)), Doctype, ResultName, CInt(r.Item(GridColumns.ICONID_COLUMNNAME)), CInt(r.Item("Exclusive")), DirectCast(Task_State_ID, Zamba.Core.TaskStates), Doctype.Indexs, s.InitialState)
                    End If

                    If Not IsDBNull(r(GridColumns.CHECKIN_COLUMNNAME)) Then
                        Result.CheckIn = r.Item(GridColumns.CHECKIN_COLUMNNAME)
                    End If

                    If Not IsDBNull(r(GridColumns.WORK_ID_COLUMNNAME)) Then
                        Result.WorkId = r.Item(GridColumns.WORK_ID_COLUMNNAME)
                    End If

                    If Not IsDBNull(r(GridColumns.EXPIREDATE_COLUMNNAME)) Then
                        Result.ExpireDate = r.Item(GridColumns.EXPIREDATE_COLUMNNAME)
                    End If

                    If Not IsDBNull(r(GridColumns.DATE_ASIGNED_BY_COLUMNNAME)) Then
                        Result.AsignedDate = r.Item(GridColumns.DATE_ASIGNED_BY_COLUMNNAME)
                    End If

                    If Not IsDBNull(r(GridColumns.VOL_ID_COLUMNNAME)) Then
                        Result.Disk_Group_Id = r.Item(GridColumns.VOL_ID_COLUMNNAME)
                    End If

                    If Not IsDBNull(r(GridColumns.DISK_VOL_PATH_COLUMNNAME)) Then
                        Result.DISK_VOL_PATH = r.Item(GridColumns.DISK_VOL_PATH_COLUMNNAME)
                    End If

                    If Not IsDBNull(r(GridColumns.DOC_FILE_COLUMNNAME)) Then
                        Result.Doc_File = r.Item(GridColumns.DOC_FILE_COLUMNNAME)
                    End If

                    If Not IsDBNull(r(GridColumns.OFFSET_COLUMNNAME)) Then
                        Result.OffSet = r.Item(GridColumns.OFFSET_COLUMNNAME)
                    End If

                    Result.AsignedToId = CType(r.Item(GridColumns.USER_ASIGNED_COLUMNNAME), Int64)
                    Result.AsignedById = CType(r.Item(GridColumns.USER_ASIGNED_BY_COLUMNNAME), Int64)

                    Try
                        Dim WFStepStateId As Int32 = Int32.Parse(r.Item(GridColumns.DO_STATE_ID_COLUMNNAME).ToString())
                        If Result.StepId = 0 Then
                            Result.StepId = s.ID
                        End If

                        Result.State = WFStepStatesComponent.getStateFromList(WFStepStateId, s.States)
                        If s.States.Contains(Result.State) = False Then
                            Result.State = s.InitialState
                            WFTasksFactory.UpdateState(Result.TaskId, Result.StepId, Result.StateId)
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
                                    'Si el atributo es de tipo Sustitucion
                                    If DirectCast(Result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustitución _
                                        Or DirectCast(Result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        'Se carga la descripcion de Atributo
                                        DirectCast(Result.Indexs(i), Index).dataDescription = AutoSubstitutionBusiness.getDescription(DirectCast(Result.Indexs(i), Index).Data, DirectCast(Result.Indexs(i), Index).ID, False, DirectCast(Result.Indexs(i), Index).Type)
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
            End Try
        End Function

        ''' <summary>
        ''' Este metodo se usa para instanciar un taskresult parcial para la grilla del cliente
        ''' </summary>
        ''' <param name="r"></param>
        ''' <param name="s"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>


        'todo: este tipo de funciones se deben poner o en core o en tools
        ''' <summary>
        ''' Método que sirve para castear el tipo de un Atributo a Type
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
                Dim Wfstep As IWFStep = WFStepBusiness.GetStepById(stepId)
                For Each CurrentRow As DataRow In DtTasks.Rows
                    'se cambia el metodo Builtask por el metodo NewResult, que es el que se viene utilizando en el cliente de zamba windows
                    Dim task As TaskResult = NewResult(CurrentRow, Wfstep)
                    Tasks.Add(task)
                Next
            End If

            Return Tasks
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
            Try
                Dim DTList As ArrayList = WFStepBusiness.GetDocTypesAssociatedToWFbyStepId(stepId)
                Dim DtTasks As New DataTable
                Dim tmpTasks As DataTable
                Dim fc As New FiltersComponent
                For Each docid As Int64 In DTList
                    tmpTasks = GetTasksByStepandDocTypeId(stepId, docid, WithRights, CurrentUserId, fc, LastDocId, PageSize, SearchType.GridResults, String.Empty, Nothing)
                    If tmpTasks IsNot Nothing Then
                        DtTasks.Merge(tmpTasks)
                    End If
                Next
                tmpTasks = Nothing
                Return DtTasks
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ''' <summary>
        ''' Devuelve las tareas de la entidad y etapa que se le pasan por parametro
        ''' </summary>
        ''' <param name="stepId">Id de la etapa</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''        [Ezequiel] 14/09/09 - Created.
        '''        [Javier]   06/10/10 - Modified.  Se agrega la obtención de las restricciones sobre los doctypeid.
        ''' </history>
        Public Function GetTasksByStepandDocTypeId(ByVal stepId As Int64,
                                                   ByVal docTypeId As Int64,
                                                   ByVal WithGridRights As Boolean,
                                                   ByVal CurrentUserID As Int64,
                                                   ByRef FC As FiltersComponent,
                                                   ByVal LastPage As Int64,
                                                   ByVal PageSize As Int32,
                                                   ByVal searchType As SearchType,
                                                   ByVal Order As String,
                                                   Optional ByVal wfstateID As Int64 = 0) As DataTable
            Dim orderType As String
            Dim checkInColumnIsShortDate As Boolean
            Dim filters As String
            Dim filterElements As Generic.List(Of IFilterElem)
            Dim sbColumnCondition As StringBuilder = Nothing
            Dim sbDateDeclaration As StringBuilder = Nothing
            Dim indexs As List(Of IIndex) = Nothing
            Dim indexsAux As List(Of IIndex) = Nothing
            Dim autoSustIndex As List(Of IIndex) = Nothing

            Try
                If searchType <> SearchType.WFStepCount Then
                    If FC IsNot Nothing Then
                        Dim strTable As String = Results_Factory.MakeTable(docTypeId, Results_Factory.TableType.Full)
                        Dim isCaseSensitive As Boolean = UserPreferences.getValue("CaseSensitive", Sections.UserPreferences, True)

                        filterElements = FC.GetLastUsedFilters(docTypeId, CurrentUserID, FilterTypes.Task)
                        filters = FC.GetFiltersString(filterElements, False, isCaseSensitive)
                    End If
                End If

                '(pablo) ZUserConfig Option - Converts the DateTime type column into a Date Column
                If CBool(UserPreferences.getValue("CheckInColumnShortDateFormat", Sections.UserPreferences, "True")) Then
                    checkInColumnIsShortDate = True
                End If

                'Obtencion del string de restricciones
                sbColumnCondition = New System.Text.StringBuilder
                sbDateDeclaration = New System.Text.StringBuilder
                Dim wftsbs As New WFTaskBusiness
                wftsbs.CompleteRestrictionString(sbColumnCondition, sbDateDeclaration, docTypeId, CurrentUserID, True)
                wftsbs = Nothing


                Dim VerAsignadosAOtros As Boolean = RightsBusiness.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosAOtros, stepId)
                Dim VerAsignadosANadie As Boolean = RightsBusiness.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosANadie, stepId)

                Dim wftf As New WFTasksFactory

                'Si la columna por la que se va a ordenar la grilla es un indice obtengo el tipo de dato.
                If Not String.IsNullOrEmpty(Order) Then
                    Dim col As String = GridColumns.GetColumnByOrderString(Order)
                    If IsIndex(col) Then
                        Dim currentIndex As IIndex = IndexsBusiness.GetIndex(IndexsBusiness.GetIndexIdByName(col))
                        Select Case currentIndex.Type
                            Case 1, 2, 3
                                If currentIndex.DropDown = IndexAdditionalType.AutoSustitución OrElse currentIndex.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                    orderType = "A"
                                Else
                                    orderType = "N"
                                End If
                            Case 4, 5
                                orderType = "D"
                            Case Else
                                orderType = "A"
                        End Select
                    End If
                End If

                indexs = New List(Of IIndex)
                Dim dt As DataTable
                '[Ezequiel] Se valida por este bolean ya que en el servicio no se precisan validar permisos de atributos pero en el cliente si.
                If WithGridRights Then

                    If searchType <> SearchType.WFStepCount Then
                        '[Ezequiel] Lista que guarda los atributos a incluir en cada tarea.
                        indexsAux = New List(Of IIndex)
                        '[Ezequiel] Lista que guarda los atributos de autosustitucion
                        autoSustIndex = New List(Of IIndex)

                        '[Ezequiel] Valido si la opcion del userconfig esta activa para ver atributos en grilla.
                        If CBool(UserPreferences.getValue("ShowIndexsOnGrid", Sections.UserPreferences, "True")) Then
                            indexs.AddRange(DocTypesBusiness.GetDocType(docTypeId, True).Indexs)
                            '[Ezequiel] valido el permiso de ver atributos en grilla de tareas.
                            For Each indice As IIndex In indexs
                                If UserBusiness.Rights.GetIndexRightValue(docTypeId, indice.ID, CurrentUserID, RightsType.TaskGridIndexView) Then
                                    indexsAux.Add(indice)
                                    If indice.DropDown = IndexAdditionalType.AutoSustitución _
                                        Or indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then autoSustIndex.Add(indice)
                                End If
                            Next
                        End If

                        dt = wftf.GetTasksByStepandDocTypeId(stepId,
                                                             docTypeId,
                                                             indexsAux,
                                                             WithGridRights,
                                                             filters,
                                                             sbColumnCondition.ToString(),
                                                             sbDateDeclaration.ToString(),
                                                             LastPage,
                                                             PageSize,
                                                             checkInColumnIsShortDate,
                                                             autoSustIndex,
                                                             searchType,
                                                             VerAsignadosAOtros,
                                                             VerAsignadosANadie,
                                                             False,
                                                             wfstateID,
                                                             Order,
                                                             Membership.MembershipHelper.CurrentUser,
                                                             orderType)

                    End If


                    If searchType = SearchType.WFStepCount OrElse Not IsNothing(dt) Then
                        If LastPage = 0 Then
                            Dim countSearchType As SearchType
                            If searchType = SearchType.GridResults Then countSearchType = SearchType.GridResultsCount Else countSearchType = searchType
                            Dim tempCount As DataTable = wftf.GetTasksByStepandDocTypeId(stepId,
                                                        docTypeId,
                                                        indexsAux,
                                                        WithGridRights,
                                                        filters,
                                                        sbColumnCondition.ToString(),
                                                        sbDateDeclaration.ToString(),
                                                        LastPage,
                                                        PageSize,
                                                        checkInColumnIsShortDate,
                                                        autoSustIndex,
                                                        countSearchType,
                                                        VerAsignadosAOtros,
                                                        VerAsignadosANadie,
                                                        False,
                                                        wfstateID,
                                                        Order,
                                                        Membership.MembershipHelper.CurrentUser,
                                                        orderType)

                            If Not IsNothing(tempCount) AndAlso tempCount.Rows.Count > 0 Then
                                If dt Is Nothing Then dt = tempCount
                                dt.MinimumCapacity = tempCount.Rows(0)(0)
                            End If
                        End If
                    End If


                Else
                    If searchType <> SearchType.WFStepCount Then
                        indexs.AddRange(DocTypesBusiness.GetDocType(docTypeId, True).Indexs)

                        dt = wftf.GetTasksByStepandDocTypeId(stepId,
                                                             docTypeId,
                                                             indexs,
                                                             WithGridRights,
                                                             filters,
                                                             sbColumnCondition.ToString(),
                                                             sbDateDeclaration.ToString(),
                                                             LastPage,
                                                             PageSize,
                                                             checkInColumnIsShortDate,
                                                             Nothing,
                                                             searchType,
                                                             VerAsignadosAOtros,
                                                             VerAsignadosANadie,
                                                             False,
                                                             wfstateID,
                                                             Order,
                                                             Membership.MembershipHelper.CurrentUser,
                                                             orderType)
                    End If

                    If searchType = SearchType.WFStepCount OrElse Not IsNothing(dt) Then
                        If Server.isOracle AndAlso LastPage = 0 Then
                            Dim countSearchType As SearchType
                            If searchType = SearchType.GridResults Then countSearchType = SearchType.GridResultsCount Else countSearchType = searchType

                            Dim tempCount As DataTable = wftf.GetTasksByStepandDocTypeId(stepId,
                                                        docTypeId,
                                                        indexs,
                                                        WithGridRights,
                                                        filters,
                                                        sbColumnCondition.ToString(),
                                                        sbDateDeclaration.ToString(),
                                                        LastPage,
                                                        PageSize,
                                                        checkInColumnIsShortDate,
                                                        Nothing,
                                                        countSearchType,
                                                        VerAsignadosAOtros,
                                                        VerAsignadosANadie,
                                                        False,
                                                        wfstateID,
                                                        Order,
                                                        Membership.MembershipHelper.CurrentUser,
                                                        orderType)

                            If Not IsNothing(tempCount) AndAlso tempCount.Rows.Count > 0 Then
                                dt.MinimumCapacity = tempCount.Rows(0)(0)
                            End If
                        End If
                    End If
                End If
                wftf = Nothing

                Return dt

                Catch ex As System.Threading.ThreadAbortException
                Return nothing
            Catch ex As Exception

                ZClass.raiseerror(ex)
                Return Nothing

            Finally
                filterElements = Nothing 'no hacer clear, ya que afecta por referencia
                If sbColumnCondition IsNot Nothing Then
                    sbColumnCondition.Clear()
                    sbColumnCondition = Nothing
                End If
                If sbDateDeclaration IsNot Nothing Then
                    sbDateDeclaration.Clear()
                    sbDateDeclaration = Nothing
                End If
                If indexsAux IsNot Nothing Then
                    For i As Int32 = 0 To indexsAux.Count - 1
                        indexsAux(i).Dispose()
                        indexsAux(i) = Nothing
                    Next
                    indexsAux.Clear()
                    indexsAux = Nothing
                End If
                If autoSustIndex IsNot Nothing Then
                    For i As Int32 = 0 To autoSustIndex.Count - 1
                        autoSustIndex(i).Dispose()
                        autoSustIndex(i) = Nothing
                    Next
                    autoSustIndex.Clear()
                    autoSustIndex = Nothing
                End If
                If indexs IsNot Nothing Then
                    For i As Int32 = 0 To indexs.Count - 1
                        indexs(i).Dispose()
                        indexs(i) = Nothing
                    Next
                    indexs.Clear()
                    indexs = Nothing
                End If
            End Try
        End Function

        Public Function GetTasksByWFandDocTypeId(workflowid As Int64, ByVal stepId As Int64, ByVal DocTypeId As Int64, ByVal WithGridRights As Boolean, ByVal CurrentUserID As Int64, ByRef FC As FiltersComponent, ByVal LastDocId As Int64, ByVal PageSize As Int32, ByVal searchType As SearchType, Optional ByVal wfstateID As Int64 = 0) As DataTable
            Dim CheckInColumnIsShortDate As Boolean
            Dim FilterString As String
            Dim Filters As Generic.List(Of IFilterElem)
            Dim ColumCondstring As StringBuilder
            Dim dateDeclarationString As StringBuilder
            Dim indexs As List(Of IIndex)
            Dim indexsaux As List(Of IIndex)
            Dim autosustindex As List(Of IIndex)

            Try
                If (Not FC Is Nothing) Then
                    Dim strTable As String = Results_Factory.MakeTable(DocTypeId, Results_Factory.TableType.Full)
                    Dim isCaseSensitive As Boolean = UserPreferences.getValue("CaseSensitive", Sections.UserPreferences, True)
                    'Dim gc As New GridColumns()

                    Filters = FC.GetLastUsedFilters(DocTypeId, CurrentUserID, FilterTypes.Task)
                    FilterString = FC.GetFiltersString(Filters, False, isCaseSensitive)

                    FilterString = FilterString.Replace("strTable", strTable)

                End If

                '(pablo) ZUserConfig Option - Converts the DateTime type column into a Date Column
                If CBool(UserPreferences.getValue("CheckInColumnShortDateFormat", Sections.UserPreferences, "True")) Then
                    CheckInColumnIsShortDate = True
                End If

                'Obtencion del string de restricciones
                ColumCondstring = New System.Text.StringBuilder
                dateDeclarationString = New System.Text.StringBuilder
                Dim wftsbs As New WFTaskBusiness
                wftsbs.CompleteRestrictionString(ColumCondstring, dateDeclarationString, DocTypeId, CurrentUserID, True)
                wftsbs = Nothing

                indexs = New List(Of IIndex)
                Dim dt As DataTable
                '[Ezequiel] Se valida por este bolean ya que en el servicio no se precisan validar permisos de atributos pero en el cliente si.
                If WithGridRights Then
                    '[Ezequiel] Lista que guarda los atributos a incluir en cada tarea.
                    indexsaux = New List(Of IIndex)
                    '[Ezequiel] Lista que guarda los atributos de autosustitucion
                    autosustindex = New List(Of IIndex)

                    '[Ezequiel] Valido si la opcion del userconfig esta activa para ver atributos en grilla.
                    If CBool(UserPreferences.getValue("ShowIndexsOnGrid", Sections.UserPreferences, "True")) Then
                        indexs.AddRange(DocTypesBusiness.GetDocType(DocTypeId, True).Indexs)
                        '[Ezequiel] valido el permiso de ver atributos en grilla de tareas.
                        For Each indice As IIndex In indexs
                            If UserBusiness.Rights.GetIndexRightValue(DocTypeId, indice.ID, CurrentUserID, RightsType.TaskGridIndexView) Then
                                indexsaux.Add(indice)
                                If indice.DropDown = IndexAdditionalType.AutoSustitución _
                                    Or indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then autosustindex.Add(indice)
                            End If
                        Next
                    End If
                    Dim VerAsignadosAOtros As Boolean = RightsBusiness.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosAOtros, stepId)
                    Dim VerAsignadosANadie As Boolean = RightsBusiness.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosANadie, stepId)
                    Dim wftf As New WFTasksFactory
                    dt = wftf.GetTasksByWFandDocTypeId(workflowid, DocTypeId, indexsaux, WithGridRights, FilterString, ColumCondstring.ToString(), dateDeclarationString.ToString(), LastDocId, PageSize, CheckInColumnIsShortDate, autosustindex, searchType, VerAsignadosAOtros, VerAsignadosANadie, False, wfstateID, Membership.MembershipHelper.CurrentUser)
                    wftf = Nothing
                Else
                    indexs.AddRange(DocTypesBusiness.GetDocType(DocTypeId, True).Indexs)
                    Dim VerAsignadosAOtros As Boolean = RightsBusiness.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosAOtros, stepId)
                    Dim VerAsignadosANadie As Boolean = RightsBusiness.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosANadie, stepId)
                    Dim wftf As New WFTasksFactory
                    dt = wftf.GetTasksByWFandDocTypeId(workflowid, DocTypeId, indexs, WithGridRights, FilterString, ColumCondstring.ToString(), dateDeclarationString.ToString(), LastDocId, PageSize, CheckInColumnIsShortDate, Nothing, searchType, VerAsignadosAOtros, VerAsignadosANadie, False, wfstateID, Membership.MembershipHelper.CurrentUser)
                    wftf = Nothing
                End If
                Return dt
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                CheckInColumnIsShortDate = Nothing
                FilterString = Nothing
                If Not IsNothing(Filters) Then
                    'ESTE DISPOSE ESTA MAL YA QUE AFECTA POR REFERENCIA LOS FILTROS
                    'For i As Int32 = 0 To Filters.Count - 1
                    '    Filters(i) = Nothing
                    'Next
                    'Filters.Clear()
                    Filters = Nothing
                End If
                ColumCondstring = Nothing
                dateDeclarationString = Nothing

                'TODO: VERIFICAR EN ESTOS 3 CASOS SI AFECTA PLICARLE EL DISPOSE Y CLEAR
                'EN EL CASO DE LOS FILTROS LO HACE Y AFECTA POR REFERENCIA LOS FILTROS
                If Not IsNothing(indexsaux) Then
                    For i As Int32 = 0 To indexsaux.Count - 1
                        indexsaux(i).Dispose()
                        indexsaux(i) = Nothing
                    Next
                    indexsaux.Clear()
                    indexsaux = Nothing
                End If
                If Not IsNothing(autosustindex) Then
                    For i As Int32 = 0 To autosustindex.Count - 1
                        autosustindex(i).Dispose()
                        autosustindex(i) = Nothing
                    Next
                    autosustindex.Clear()
                    autosustindex = Nothing
                End If
                If Not IsNothing(indexs) Then
                    For i As Int32 = 0 To indexs.Count - 1
                        indexs(i).Dispose()
                        indexs(i) = Nothing
                    Next
                    indexs.Clear()
                    indexs = Nothing
                End If
            End Try
        End Function

        ''' <summary>
        ''' Devuelve las tareas de la entidad y etapa que se le pasan por parametro
        ''' </summary>
        ''' <param name="stepId">Id de la etapa</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''        [Ezequiel] 14/09/09 - Created.
        '''        [Javier]   06/10/10 - Modified.  Se agrega la obtención de las restricciones sobre los doctypeid.
        ''' </history>
        Public Shared Function GetStepsTasksCountByStepsandDocTypeId(ByVal stepIds As List(Of Int64), ByVal DocTypeId As Int64) As DataTable
            Dim ColumCondstring As StringBuilder
            Dim dateDeclarationString As StringBuilder
            Dim indexs As List(Of IIndex)

            Try
                'Obtencion del string de restricciones
                ColumCondstring = New System.Text.StringBuilder
                dateDeclarationString = New System.Text.StringBuilder

                Dim wftsbs As New WFTaskBusiness
                wftsbs.CompleteRestrictionString(ColumCondstring, dateDeclarationString, DocTypeId, Zamba.Membership.MembershipHelper.CurrentUser.ID, True)
                wftsbs = Nothing


                Dim dt As DataTable
                indexs.AddRange(DocTypesBusiness.GetDocType(DocTypeId, True).Indexs)

                Dim flagAsRead As Boolean = RightsBusiness.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.FlagAsRead, DocTypeId)
                dt = WFTasksFactory.GetStepsTasksCountByStepsandDocTypeId(DocTypeId, stepIds, ColumCondstring.ToString(), dateDeclarationString.ToString(), flagAsRead)

                Return dt
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                ColumCondstring = Nothing
                dateDeclarationString = Nothing

            End Try
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
        Private Sub CompleteRestrictionString(ByRef ColumCondstring As StringBuilder, ByRef dateDeclarationString As StringBuilder, ByVal DocTypeId As Int64, ByVal CurrentUserID As Int64, ByVal useCache As Boolean)
            Dim Valuestring As New System.Text.StringBuilder
            Dim indRestriction As List(Of IIndex)

            indRestriction = GetDocTypeUserRestrictions(DocTypeId, CurrentUserID, useCache)

            Dim First As Boolean = True
            Dim FlagCase As Boolean = Boolean.Parse(UserPreferences.getValue("CaseSensitive", Sections.UserPreferences, "True"))
            If indRestriction IsNot Nothing Then
                Dim i As Integer
                For i = 0 To indRestriction.Count - 1
                    CreateTasksRestrictionsWhere(Valuestring, indRestriction, i, FlagCase, ColumCondstring, First, dateDeclarationString)
                Next
            End If
        End Sub

        Public Shared Function GetDocTypeUserRestrictions(DocTypeId As Long, CurrentUserID As Long, useCache As Boolean) As List(Of IIndex)
            Dim indRestriction As List(Of IIndex)

            Try


                SyncLock (Cache.DocTypesAndIndexs.hsRestrictionsStrings)
                    'SI se usa cache y no se deben recargar las retricciones
                    If useCache Then
                        If Not Cache.DocTypesAndIndexs.hsRestrictionsStrings.Contains(CurrentUserID & "-" & DocTypeId) Then
                            indRestriction = RestrictionsMapper_Factory.getRestrictionIndexs(CurrentUserID, DocTypeId)
                            Cache.DocTypesAndIndexs.hsRestrictionsStrings.Add(CurrentUserID & "-" & DocTypeId, indRestriction)
                        Else
                            indRestriction = Cache.DocTypesAndIndexs.hsRestrictionsStrings(CurrentUserID & "-" & DocTypeId)
                        End If
                    Else
                        indRestriction = RestrictionsMapper_Factory.getRestrictionIndexs(CurrentUserID, DocTypeId)
                    End If
                End SyncLock
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Return indRestriction
        End Function

        Public Shared Function CountFilesInIP_Task(ByVal conf_Id As Decimal) As Integer
            Return WFTasksFactory.CountFilesInIP_Task(conf_Id)
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
        Public Shared Function GetTaskByTaskIdAndDocTypeId(ByVal taskId As Int64, ByVal DocTypeId As Int64, ByVal PageSize As Int32) As ITaskResult
            Dim WFSTepId As Int64 = WFStepBusiness.GetStepIdByTaskId(taskId)
            Dim taskbs As New WFTaskBusiness()
            Return taskbs.GetTaskByTaskIdAndDocTypeIdAndStepId(taskId, DocTypeId, WFSTepId, PageSize)
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
            Dim dt As DataTable
            Dim indexs As List(Of IIndex)
            Dim ColumCondstring As System.Text.StringBuilder
            Dim dateDeclarationString As System.Text.StringBuilder

            Try
                Dim Task As ITaskResult = Nothing

                indexs = New List(Of IIndex)
                'Obtencion del string de restricciones
                ColumCondstring = New System.Text.StringBuilder
                dateDeclarationString = New System.Text.StringBuilder
                CompleteRestrictionString(ColumCondstring, dateDeclarationString, DocTypeId, Membership.MembershipHelper.CurrentUser.ID, True)

                indexs.AddRange(DocTypesBusiness.GetDocType(DocTypeId, True).Indexs)


                Dim flagAsRead As Boolean = RightsBusiness.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.FlagAsRead, DocTypeId)

                Dim wftf As New WFTasksFactory
                dt = wftf.GetTaskByTaskIdAndDocTypeId(taskId, WFStepId, DocTypeId, indexs, False, String.Empty, ColumCondstring.ToString, dateDeclarationString.ToString, flagAsRead)
                wftf = Nothing
                If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                    Task = NewResult(dt.Rows(0), WFStepBusiness.GetStepById(WFStepId))
                End If
                Return Task
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                If Not IsNothing(indexs) Then
                    indexs.Clear()
                    indexs = Nothing
                End If

                If Not IsNothing(dt) Then
                    dt.Dispose()
                    dt = Nothing
                End If
                ColumCondstring = Nothing
                dateDeclarationString = Nothing
            End Try
        End Function

        Public Function GetStepIDByTaskId(ByVal taskId As Int64) As Int64 Implements IWFTaskBusiness.GetStepIDByTaskId
            Return WFStepBusiness.GetStepIdByTaskId(taskId)
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
        ''' </history>
        Public Shared Function GetTasksByTaskIdsAndDocTypeIdAndStepId(ByVal taskIds As List(Of Int64),
                                                                      ByVal docTypeId As Int64,
                                                                      ByVal wfStepId As Int64,
                                                                      Optional ByVal useRestrictions As Boolean = True) As List(Of ITaskResult)
            Dim tasks As New List(Of ITaskResult)
            Dim indexs As New List(Of IIndex)
            Dim dt As DataTable
            Dim sbColumnCondition As New System.Text.StringBuilder
            Dim sbDateDeclaration As New System.Text.StringBuilder

            If useRestrictions Then
                Dim wftsbs As New WFTaskBusiness
                wftsbs.CompleteRestrictionString(sbColumnCondition, sbDateDeclaration, docTypeId, Membership.MembershipHelper.CurrentUser.ID, True)
                wftsbs = Nothing
            End If

            indexs.AddRange(DocTypesBusiness.GetDocType(docTypeId, True).Indexs)

            Dim flagAsRead As Boolean = RightsBusiness.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.FlagAsRead, docTypeId)
            Dim wftf As New WFTasksFactory
            dt = wftf.GetTasksByTasksIdAndDocTypeId(taskIds, wfStepId, docTypeId, indexs, False, String.Empty, sbColumnCondition.ToString, sbDateDeclaration.ToString, flagAsRead)
            wftf = Nothing

            sbColumnCondition.Clear()
            sbColumnCondition = Nothing
            sbDateDeclaration.Clear()
            sbDateDeclaration = Nothing

            If dt IsNot Nothing Then
                Dim _step As WFStep = WFStepBusiness.GetStepById(wfStepId)
                For i As Int16 = 0 To dt.Rows.Count - 1
                    tasks.Add(NewResult(dt.Rows(i), _step))
                Next
                dt.Dispose()
                dt = Nothing
            End If

            indexs.Clear()
            indexs = Nothing

            Return tasks
        End Function

        ''' <summary>
        ''' Método que sirve para obtener un task en base a un stepId
        ''' </summary>
        ''' <param name="docId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        '''' <history>   
        ''''     Gaston     20/10/2008  Modified   Si el taskId no existe se retorna nothing
        ''''     Javier     06/12/2010  Modified    GetTaskByTaskIdAndDocTypeIdAndStepId is now a instance method
        '''' </history>
        Public Shared Function GetTaskByDocIdAndStepIdAAndDocTypeId(ByVal docId As Int64, ByVal stepId As Int64, ByVal DocTypeId As Int64, ByVal PageSize As Int32) As ITaskResult

            Dim TaskId As Int64 = WFTasksFactory.GetTaskIdByDocIdAndStepId(docId, stepId)
            Dim taskbs As New WFTaskBusiness()

            If (TaskId <> -1) Then
                Return taskbs.GetTaskByTaskIdAndDocTypeIdAndStepId(TaskId, DocTypeId, stepId, PageSize)
            Else
                Return (Nothing)
            End If

        End Function

        ''' <summary>
        ''' Método que sirve para obtener un task en base a un WorkId
        ''' </summary>
        ''' <param name="docId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>   
        '''     [Gaston]    20/10/2008  Modified    Si el taskId no existe se retorna nothing
        '''     [Marcelo]   30/11/2009  Modified    Si el documento esta en un solo wf lo devuelve, sino filtra por workid
        '''     [Javier]    06/12/2010  Modified    GetTaskByTaskIdAndDocTypeIdAndStepId is now a instance method
        ''' </history>
        Public Shared Function GetTaskByDocIdAndWorkFlowId(ByVal docId As Int64, ByVal PageSize As Int32, Optional ByVal WorkId As Int64 = 0) As ITaskResult
            Dim ds As DataSet = WFTasksFactory.GetTaskIdsStepsIdsDocTypesIdsByDocId(docId)
            Dim taskbs As New WFTaskBusiness()

            If Not IsNothing(ds) AndAlso ds.Tables.Count = 1 AndAlso ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows.Count = 1 OrElse WorkId = 0 Then
                    Return taskbs.GetTaskByTaskIdAndDocTypeIdAndStepId(ds.Tables(0).Rows(0)("task_id"), ds.Tables(0).Rows(0)("doc_type_id"), ds.Tables(0).Rows(0)("step_id"), PageSize)
                Else
                    ds.Tables(0).Select("Work_id=" + WorkId)
                    If ds.Tables(0).Rows.Count > 0 Then
                        Return taskbs.GetTaskByTaskIdAndDocTypeIdAndStepId(ds.Tables(0).Rows(0)("task_id"), ds.Tables(0).Rows(0)("doc_type_id"), ds.Tables(0).Rows(0)("step_id"), PageSize)
                    Else
                        Return Nothing
                    End If
                End If
            End If
        End Function

        ''' <summary>
        ''' [Sebastian] 14-10-09 CREATED obtiene las tareas asociadas al docid
        ''' </summary>
        ''' <param name="docId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetAllTasksByDocId(ByVal docId As Int64, ByVal doctypeid As Int64, ByVal PageSize As Int32) As Generic.List(Of ITaskResult)
            Dim TaskId As DataSet = WFTasksFactory.GetTaskIdsByDocId(docId)
            Dim AssociatedTask As New Generic.List(Of ITaskResult)


            For Each id As DataRow In TaskId.Tables(0).Rows
                AssociatedTask.Add(WFTaskBusiness.GetTaskByTaskIdAndDocTypeId(id("task_id"), doctypeid, PageSize))

            Next

            Return AssociatedTask
        End Function

        ''' <summary>
        ''' Método que sirve para obtener un task en base a un docId
        ''' </summary>
        ''' <param name="docId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetTaskIDsByDocId(ByVal docId As Int64) As List(Of Int64)
            Dim dsTaskIds As DataSet
            Dim lstTaskIds As New List(Of Int64)

            Try
                dsTaskIds = WFTasksFactory.GetTaskIdsByDocId(docId)
                If Not IsNothing(dsTaskIds) Then
                    If dsTaskIds.Tables.Count > 0 Then
                        For Each dr As DataRow In dsTaskIds.Tables(0).Rows
                            lstTaskIds.Add(Int64.Parse(dr.Item("Task_ID").ToString()))
                        Next
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                If Not IsNothing(dsTaskIds) Then
                    dsTaskIds.Dispose()
                    dsTaskIds = Nothing
                End If
            End Try

            Return lstTaskIds
        End Function

        ''' <summary>
        ''' Método que obtiene las etapas y IDs de tarea de un documento
        ''' </summary>
        ''' <param name="docId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetTaskAndStepIDsByDocId(ByVal docId As Int64) As Dictionary(Of Int64, Int64)
            Dim dsTaskIds As DataSet
            Dim lstTaskIds As New Dictionary(Of Int64, Int64)

            Try
                dsTaskIds = WFTasksFactory.GetTaskAndStepIdsByDocId(docId)
                If Not IsNothing(dsTaskIds) Then
                    If dsTaskIds.Tables.Count > 0 Then
                        For Each dr As DataRow In dsTaskIds.Tables(0).Rows
                            lstTaskIds.Add(Int64.Parse(dr.Item("Task_ID").ToString()), Int64.Parse(dr.Item("step_ID").ToString()))
                        Next
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                If Not IsNothing(dsTaskIds) Then
                    dsTaskIds.Dispose()
                    dsTaskIds = Nothing
                End If
            End Try

            Return lstTaskIds
        End Function
#End Region

#Region "OpenDocument"
        Public Shared Sub OpenDocument(ByRef Result As TaskResult)
            If IsNothing(Result.FullPath) Then
                Throw New Exception("No se encontró la documentación en Zamba Software")
            End If
            Dim fi As New IO.FileInfo(Result.FullPath)
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
        Public Shared Sub AddResultsToWorkFLow(ByVal Results As ArrayList, ByVal WF As IWorkFlow, Optional ByVal LogInAction As Boolean = True, Optional ByVal OpenTaskAfterInsert As Boolean = True)
            Dim TasksResults As New System.Collections.Generic.List(Of Core.ITaskResult)

            For Each Result As Result In Results
                ' valido que no exista el result en el workflow
                If WFBusiness.ValidateDocIdInWF(Result.ID, WF.ID) = False Then
                    If IsNothing(WF.InitialStep) = False Then
                        Dim t As New TaskResult(WF.InitialStep.ID, CoreData.GetNewID(IdTypes.Tasks), Result.ID, Result.DocType,
                                                Result.Name, Result.IconId, 0, Zamba.Core.TaskStates.Desasignada,
                                                Result.Indexs, Result.DISK_VOL_PATH, Result.Parent.ID, Result.OffSet, Result.Doc_File,
                                                Result.Disk_Group_Id, WF.InitialStep.InitialState)

                        t.WorkId = WF.ID
                        'If Not WF.InitialStep.InitialState Is Nothing Then ChangeState(t, WF.InitialStep.InitialState)
                        t.CheckIn = Now

                        TasksResults.Add(t)
                        'Logs the  user action
                        If LogInAction = True Then
                            WFTaskBusiness.LogCheckIn(t)
                        End If
                        UserBusiness.Rights.SaveAction(t.ID, ObjectTypes.WFTask, RightsType.insert, "Se inserto la tarea: " & t.ID _
                                                                                                    & " en el WF: " & WF.Name)
                    Else
                        Throw New Exception("No hay definida una etapa inicial en el Workflow: " & WF.Name)

                    End If
                Else
                    Exit Sub
                    'Throw New Exception("El documento ya esta ingresado en el Workflow: " & WF.Name)
                End If
            Next
            ' valido que haya un result al menos para meter en el workflow
            If TasksResults.Count > 0 Then
                WFTasksFactory.InsertTasks(TasksResults, WF.ID, WF.Name, WF.InitialStep.ID, WF.InitialStep.InitialState.ID, Nothing)

                'TODO WF: ESTO DEBE SER GENERICO
                WFStepBusiness.FillSteps(WF, True)
                'WFRulesBusiness.FillRules(WF, True)

                'todo wf: esto se deberia levantar un evento y un controlador atraparlo y ver las reglas
                'todo wf: verificar que el filtrado no sea de la coleccion real de la etapa
                For Each S As WFStep In WF.Steps.Values
                    If Not IsNothing(WF.InitialStep) Then
                        If WF.InitialStep.ID = S.ID Then


                            Dim DVDSRules As New DataView(S.DsRules.WFRules)
                            DVDSRules.RowFilter = "ParentType = 12"
                            For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                                Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), S.ID, True)
                                TasksResults = Rule.ExecuteRule(TasksResults, New WFTaskBusiness(), Nothing)
                            Next
                            DVDSRules.Dispose()
                            DVDSRules = Nothing

                        End If
                    End If
                Next

                'todo wf: esto se deberia levantar un evento y un controlador atraparlo y ver las reglas
                For Each S As WFStep In WF.Steps.Values
                    If Not IsNothing(WF.InitialStep) Then
                        If WF.InitialStep.ID = S.ID Then

                            Dim DVDSRules As New DataView(S.DsRules.WFRules)
                            DVDSRules.RowFilter = "ParentType = 6"
                            For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                                Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), S.ID, True)
                                Rule.ExecuteRule(TasksResults, New WFTaskBusiness(), Nothing)
                            Next

                            '   Dim DVDSRules As New DataView(WFStep.DsRules.WFRules)
                            DVDSRules.RowFilter = "ParentType = 38"
                            For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                                Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), S.ID, True)
                                Rule.ExecuteRule(TasksResults, New WFTaskBusiness(), Nothing)
                            Next

                        End If
                    End If
                Next

                'Realiza el refresh
                If OpenTaskAfterInsert Then
                    RaiseEvent AddedTask(TasksResults, OpenTaskAfterInsert)
                End If
            End If
        End Sub

        ''' <summary>
        ''' </summary>
        ''' <param name="Results"></param>
        ''' <param name="WF"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [Sebastian 12-05-09]    COMENTED    Agrega un result al wf que esta relacionado o se pretende agregar.
        ''' [Sebastian 12-05-09] se agrego try-catch
        ''' [Tomas] - 13/05/2009 - Modified - Se remueve el try-catch que Sebastian habia puesto ya que generabla conflictos con la interfaz gráfica.
        ''' </history>
        Public Shared Function AddResultsToWorkFLow(ByVal Results As ArrayList, ByVal WFId As Int64, ByRef tr As Transaction) As ArrayList
            Dim TasksResults As New System.Collections.Generic.List(Of Core.ITaskResult)
            Dim values As New ArrayList(2)

            Dim WFInitialStepId As Int64 = WFBusiness.GetInitialStepOfWF(WFId)
            Dim WfInitialState As WFStepState = WFStepStatesBusiness.GetInitialState(WFInitialStepId)

            For Each Result As Result In Results
                ' valido que no exista el result en el workflow
                If WFBusiness.ValidateDocIdInWF(Result.ID, WFId) = False Then
                    If WFInitialStepId > 0 Then
                        If WfInitialState IsNot Nothing Then
                            Dim t As New TaskResult(WFInitialStepId, CoreData.GetNewID(IdTypes.Tasks), Result.ID, Result.DocType, Result.Name, Result.IconId, 0, Zamba.Core.TaskStates.Desasignada, Result.Indexs, WfInitialState)
                            t.WorkId = WFId
                            t.CheckIn = Now
                            t.Name = Result.Name
                            t.Disk_Group_Id = Result.Disk_Group_Id
                            t.DISK_VOL_PATH = Result.DISK_VOL_PATH
                            t.Parent.ID = Result.Parent.ID
                            t.OffSet = Result.OffSet
                            t.Doc_File = Result.Doc_File

                            TasksResults.Add(t)
                            WFTaskBusiness.LogCheckIn(t, tr)
                        Else
                            Throw New Exception("No hay definido un estado inicial en la etapa inicial del Workflow")
                        End If
                    Else
                        Throw New Exception("No hay definida una etapa inicial en el Workflow")
                    End If
                Else
                    Exit Function
                End If
            Next

            ' valido que haya un result al menos para meter en el workflow
            If TasksResults.Count > 0 Then
                Dim wfName As String = WFBusiness.GetWorkflowNameByWFId(WFId)
                WFTasksFactory.InsertTasks(TasksResults, WFId, wfName, WFInitialStepId, WfInitialState.ID, tr)
                wfName = Nothing
            End If

            values.Add(WFInitialStepId)
            values.Add(TasksResults)

            Return values
        End Function


        Public Shared Sub ExecuteCheckInRulesFromInsert(ByVal InsertedWFIdsAndInitialStepsAndTasksResults As ArrayList, ByVal OpenTaskAfterInsert As Boolean)
            If Not IsNothing(InsertedWFIdsAndInitialStepsAndTasksResults) AndAlso InsertedWFIdsAndInitialStepsAndTasksResults.Count = 2 Then
                Dim WFRB As New WFRulesBusiness()
                Dim wfRules As IWFRuleParent()
                Dim TasksResults As Generic.List(Of ITaskResult) = InsertedWFIdsAndInitialStepsAndTasksResults(1)

                wfRules = WFRulesBusiness.GetInstanceRules(CLng(InsertedWFIdsAndInitialStepsAndTasksResults(0)), TypesofRules.ValidacionEntrada, False)
                If wfRules IsNot Nothing Then
                    For Each r As WFRuleParent In wfRules
                        TasksResults = WFRB.ExecutePrimaryRule(r, TasksResults, Nothing)
                    Next
                End If


                wfRules = WFRulesBusiness.GetInstanceRules(CLng(InsertedWFIdsAndInitialStepsAndTasksResults(0)), TypesofRules.Entrada, False)
                If wfRules IsNot Nothing Then
                    For Each r As WFRuleParent In wfRules
                        TasksResults = WFRB.ExecutePrimaryRule(r, TasksResults, Nothing)
                    Next
                End If

                'If OpenTaskAfterInsert Then
                'Realiza el refresh
                If RightsBusiness.GetUserRights(ObjectTypes.ModuleWorkFlow, RightsType.Use) = True Then
                    RaiseEvent AddedTask(TasksResults, OpenTaskAfterInsert)
                End If

                wfRules = WFRulesBusiness.GetInstanceRules(CLng(InsertedWFIdsAndInitialStepsAndTasksResults(0)), TypesofRules.Insertar, True)
                If wfRules IsNot Nothing Then
                    For Each r As WFRuleParent In wfRules
                        r.ExecuteRule(TasksResults, New WFTaskBusiness(), Nothing)
                    Next
                End If

                WFRB = Nothing
                wfRules = Nothing
            End If
        End Sub

        Public Shared Sub AddResultsToWorkFLow(ByVal TResults As ArrayList, ByVal WF As WorkFlow, ByVal p_WStep As WFStep, Optional ByVal OpenTaskAfterInsert As Boolean = True)
            Dim TasksResults As New System.Collections.Generic.List(Of Core.ITaskResult)
            For Each Result As Core.TempTaskResult In TResults
                Dim t As New TaskResult(p_WStep.ID, CoreData.GetNewID(IdTypes.Tasks), Result.Result.ID, Result.Result.DocType, Result.Result.Name, Result.Result.IconId, 0, DirectCast(CInt(Result.TaskState), Zamba.Core.TaskStates), Result.Result.Indexs, p_WStep.InitialState, Result.AsignedToId)
                t.WorkId = WF.ID
                t.CheckIn = Now
                TasksResults.Add(t)
                WFTaskBusiness.LogCheckIn(t)
            Next

            WFTasksFactory.InsertTasks(TasksResults, WF.ID, WF.Name, p_WStep.ID, p_WStep.InitialState.ID, Nothing)
            'TODO WF: ESTO DEBE SER GENERICO
            WFStepBusiness.FillSteps(WF, True)
            'WFRulesBusiness.FillRules(WF, True)

            'todo wf: esto se deberia levantar un evento y un controlador atraparlo y ver las reglas
            'todo wf: verificar que el filtrado no sea de la coleccion real de la etapa
            For Each S As WFStep In WF.Steps.Values
                If WF.InitialStep.ID = S.ID Then

                    Dim DVDSRules As New DataView(S.DsRules.WFRules)
                    DVDSRules.RowFilter = "ParentType = 12"
                    For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                        Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), S.ID, True)
                        TasksResults = Rule.ExecuteRule(TasksResults, New WFTaskBusiness(), Nothing)
                    Next
                    DVDSRules.Dispose()
                    DVDSRules = Nothing

                End If
            Next

            'todo wf: esto se deberia levantar un evento y un controlador atraparlo y ver las reglas
            For Each S As WFStep In WF.Steps.Values
                If WF.InitialStep.ID = S.ID Then
                    Dim DVDSRules As New DataView(S.DsRules.WFRules)
                    DVDSRules.RowFilter = "ParentType = 6"
                    For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                        Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), S.ID, True)
                        Rule.ExecuteRule(TasksResults, New WFTaskBusiness(), Nothing)
                    Next
                    DVDSRules.Dispose()
                    DVDSRules = Nothing

                End If
            Next


            'If OpenTaskAfterInsert Then
            'Realiza el refresh
            RaiseEvent AddedTask(TasksResults, OpenTaskAfterInsert)
            'End If

        End Sub
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
        Public Shared Function AddNewResultsToWorkFLow(ByVal TResults As ArrayList, ByVal WFId As Int64, Optional ByVal OpenTaskAfterInsert As Boolean = True) As System.Collections.Generic.List(Of Core.ITaskResult)
            Dim TasksResults As New System.Collections.Generic.List(Of Core.ITaskResult)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtengo el WF")
            Dim wf As WorkFlow = WFBusiness.GetWorkFlow(WFId)
            Dim currentStep As WFStep = Nothing

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Recorro los newres")
            For Each newRes As NewResult In TResults
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtengo la etapa inicial")
                currentStep = WFStepBusiness.GetStepById(wf.InitialStepIdTEMP)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Contruyo el ITaskResult")
                Dim t As New TaskResult(currentStep.ID, CoreData.GetNewID(IdTypes.Tasks), newRes.ID, newRes.DocType, newRes.Name, newRes.IconId, 0, Zamba.Core.TaskStates.Desasignada, newRes.Indexs, currentStep.InitialState)
                t.CheckIn = Now
                t.File = newRes.File
                TasksResults.Add(t)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Inserto la tarea en el WF")

                t.WorkId = wf.ID
                If Not IsNothing(currentStep.InitialState) AndAlso Not currentStep.InitialState Is Nothing Then
                    t.State = WFStepStatesBusiness.GetInitialState(wf.InitialStepIdTEMP)
                End If
                WFTasksFactory.InsertTask(t, wf.ID, wf.Name, t.StepId, t.StateId, Nothing, Membership.MembershipHelper.CurrentUser)
                WFTaskBusiness.LogCheckIn(t, Nothing)
            Next

            Dim wfRules As IWFRuleParent()
            Dim WFRB As New WFRulesBusiness

            wfRules = WFRulesBusiness.GetInstanceRules(currentStep.ID, TypesofRules.ValidacionEntrada, False)
            If Not IsNothing(wfRules) Then
                For Each r As IWFRuleParent In wfRules
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecuto regla de validacion de entrada con id " & r.ID)
                    Dim list As List(Of ITaskResult) = WFRB.ExecutePrimaryRule(r, TasksResults, Nothing)
                    list = Nothing
                Next
            End If

            wfRules = WFRulesBusiness.GetInstanceRules(currentStep.ID, TypesofRules.Entrada, False)
            If Not IsNothing(wfRules) Then
                For Each r As IWFRuleParent In wfRules
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecuto regla de entrada con id " & r.ID)
                    Dim list As List(Of ITaskResult) = WFRB.ExecutePrimaryRule(r, TasksResults, Nothing)
                    list = Nothing
                Next
            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Retorno las tareas insertadas")

            'If OpenTaskAfterInsert Then
            'Realiza el refresh
            RaiseEvent AddedTask(TasksResults, OpenTaskAfterInsert)
            'End If

            Return TasksResults
        End Function

        '''Inserta una coleccion de newResult en el WF
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="result"></param>
        ''' <returns></returns>
        ''' <history>
        ''' [pablo] 27/01/2011 Created
        '''                           
        ''' </history>
        ''' <remarks></remarks>
        Public Shared Sub OpenTask(ByVal result As Zamba.Core.ITaskResult, ByVal params As Hashtable)
            Try
                If params.Item("UseCurrentTask") Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Abriendo la tarea - TaskId = " & result.ID.ToString)
                    RaiseEvent ShowTask(result, True)
                Else

                    If params.Item("TaskID") <> String.Empty Then
                        'abro la tarea con un TaskId configurado
                        Dim taskId As Int64 = Int64.Parse(params.Item("TaskID").ToString)

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Abriendo la tarea - TaskId = " & taskId.ToString)
                        If result.DocTypeId = 0 Then
                            Dim ds As DataSet = WFTasksFactory.GetTaskByTaskId(taskId)
                            If Not IsNothing(ds) AndAlso ds.Tables(0).Rows.Count > 0 Then
                                result.DocTypeId = ds.Tables(0).Rows(0)("doc_type_id")
                            Else
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "NO se ha encontrado la tarea - TaskId = " & taskId.ToString)
                            End If
                        End If

                        result = GetTaskByTaskIdAndDocTypeId(taskId, result.DocTypeId, 0)
                        RaiseEvent ShowTask(result, True)
                    ElseIf params.Item("DocIDs") <> String.Empty Then
                        'abro la tarea con un DocId configurado
                        'TODO: si existen varios doy a elegir
                        Dim ds As DataSet = WFTasksFactory.GetTaskByDocId(params.Item("DocIDs"))
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Abriendo la tarea - DocId = " & params.Item("DocIDs").ToString)
                        If Not IsNothing(ds) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Dim t As Int32
                            Dim Taskresults As New System.Collections.Generic.List(Of Zamba.Core.ITaskResult)
                            For t = 0 To ds.Tables.Count
                                Taskresults.Add(GetTaskByTaskIdAndDocTypeId(ds.Tables(0).Rows(t)("task_id"), ds.Tables(0).Rows(t)("doc_type_id"), 0))
                                t = t + 1
                            Next
                            ZambaCore.HandleRuleModule(ResultActions.MostrarResult, Taskresults, params)
                        Else
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "NO se ha encontrado la tarea - DocId = " & params.Item("DocID").ToString)
                        End If
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Private Shared Function GetEntrada(ByVal Rule As IWFRuleParent) As Boolean

            If (Rule.ParentType = TypesofRules.Entrada) Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Shared Function GetValidacionEntrada(ByVal Rule As IWFRuleParent) As Boolean

            If (Rule.ParentType = TypesofRules.ValidacionEntrada) Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shared Event LastInsertedTask(ByVal Task As ITaskResult)

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
        Public Shared Sub AddResultsToWorkFLowSinceRuleADDTOWF(ByVal Results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal wfId As Integer, ByVal p_WStep As WFStep, ByVal initialStateId As Int32, ByVal OpenTaskAfterInsert As Boolean)
            Dim TasksResults As New System.Collections.Generic.List(Of Core.ITaskResult)
            Dim wf As WorkFlow = WFBusiness.GetWorkFlow(wfId)

            For Each Result As Result In Results

                ' Si el result no existe en el workflow
                If (WFBusiness.ValidateDocIdInWF(Result.ID, wfId) = False) Then

                    Dim t As New TaskResult(p_WStep.ID, CoreData.GetNewID(IdTypes.Tasks), Result.ID, Result.DocType, Result.Name, Result.IconId, 0, Zamba.Core.TaskStates.Desasignada, Result.Indexs, p_WStep.InitialState)
                    Dim tID As Int64 = t.TaskId
                    t.CheckIn = Now
                    t.WorkId = wfId

                    ' Se registra en el historial (ingreso etapa a workflow)
                    LogCheckIn(t)
                    ' Se inserta la tarea en el workflow
                    WFTasksFactory.InsertTask(t, wfId, wf.Name, p_WStep.ID, initialStateId, Nothing, Membership.MembershipHelper.CurrentUser)

                    t.Dispose()
                    t = Nothing
                    t = GetTask(tID, useRestrictions:=False)
                    TasksResults.Add(t)


                Else
                    Throw New Exception("El result " & Result.Name & " ya se encuentra en el workflow " & wf.Name)
                End If
            Next

            If (TasksResults.Count > 0) Then

                WFStepBusiness.FillSteps(wf, True)
                'WFRulesBusiness.FillRules(wf, True)
                'WFTaskBusiness.FillUserTask(wf)

                For Each S As WFStep In wf.Steps.Values
                    If wf.InitialStep.ID = S.ID Then

                        Dim DVDSRules As New DataView(S.DsRules.WFRules)
                        DVDSRules.RowFilter = "ParentType = 12"
                        For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                            Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), S.ID, True)
                            TasksResults = Rule.ExecuteRule(TasksResults, New WFTaskBusiness(), Nothing)
                        Next
                        DVDSRules.Dispose()
                        DVDSRules = Nothing

                    End If
                Next

                For Each S As WFStep In wf.Steps.Values
                    If wf.InitialStep.ID = S.ID Then

                        Dim DVDSRules As New DataView(S.DsRules.WFRules)
                        DVDSRules.RowFilter = "ParentType = 6"
                        For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                            Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), S.ID, True)
                            Rule.ExecuteRule(TasksResults, New WFTaskBusiness(), Nothing)
                        Next
                        DVDSRules.Dispose()
                        DVDSRules = Nothing

                    End If
                Next
                RaiseEvent AddedTask(TasksResults, OpenTaskAfterInsert)
            End If
        End Sub
#End Region

#Region "Indexs & DocType"
        Friend Shared Sub FillIndexsAndDocType(ByRef Result As TaskResult)
            Try
                FillDocType(Result)
                Result.Indexs = ZCore.FilterIndex(CInt(Result.DocType.ID))
                'Results_Factory.FillIndexData(Result)
                Results_Business.CompleteDocument(DirectCast(Result, Result))
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Carga los atributos para un result
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <param name="inThread"></param>
        ''' <remarks></remarks>
        Friend Shared Sub FillIndexs(ByRef Result As TaskResult, Optional ByVal inThread As Boolean = False)
            Try
                'FillDocType(Result)
                Result.Indexs = ZCore.FilterIndex(CInt(Result.DocType.ID))
                'Results_Factory.FillIndexData(Result)
                Results_Business.CompleteDocument(DirectCast(Result, Result), inThread)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub
        ''' <summary>
        ''' Carga los atributos para un result
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <remarks></remarks>
        Friend Shared Sub CompleteDocument(ByRef Result As TaskResult, ByVal dr As DataRow)
            Try
                Result.Indexs = ZCore.FilterIndex(CInt(Result.DocTypeId))
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
                            'Si el atributo es de tipo Sustitucion
                            If DirectCast(Result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustitución _
                                Or DirectCast(Result.Indexs(i), Index).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                'Se carga la descripcion de Atributo
                                DirectCast(Result.Indexs(i), Index).dataDescription = AutoSubstitutionBusiness.getDescription(DirectCast(Result.Indexs(i), Index).Data, DirectCast(Result.Indexs(i), Index).ID, False, DirectCast(Result.Indexs(i), Index).Type)
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
            End Try
        End Sub
        Private Shared Sub FillDocType(ByRef Result As TaskResult)
            Try
                Static DTs As Hashtable = DTs
                If IsNothing(DTs) Then DTs = New Hashtable
                If DTs.Contains(CLng(Result.DocType.ID)) = False Then
                    Result.Parent = DocTypesBusiness.GetDocType(Result.DocType.ID, True)
                    DTs.Add(Result.Parent.ID, Result.Parent)
                Else
                    Result.Parent = DirectCast(DTs(Long.Parse(Result.DocType.ID.ToString())), IZambaCore)
                    If IsNothing(Result.Parent) Then
                        Result.Parent = DirectCast(DTs(Result.DocType.ID), IZambaCore)
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        'Public Shared Function GetIndexTypeByName(ByVal indexname As String) As Int16
        '    Return WFTasksFactory.GetIndexTypeByName(indexname)
        'End Function
#End Region

#Region "Rights"
        Friend Shared Function HaveRightToView(ByRef wfstep As WFStep, ByVal AsignedToId As Int64, ByVal CurrentUserId As Int64) As Boolean
            Try
                If AsignedToId = 0 Then
                    'Asignado a nadie
                    If Not UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosANadie, CInt(wfstep.ID)) Then
                        Return False
                    End If
                ElseIf AsignedToId = CurrentUserId Then
                    'Asignado a uno
                    If Not UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, Zamba.Core.RightsType.Use, CInt(wfstep.ID)) Then
                        Return False
                    End If
                Else
                    'Asignado a otro
                    If Not UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosAOtros, CInt(wfstep.ID)) Then
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
        Public Shared Event AddedTask(ByVal Results As Generic.List(Of ITaskResult), ByVal OpenTaskAfterInsert As Boolean)
        Public Shared Event ShowTask(ByVal result As Zamba.Core.ITaskResult, ByVal OpenTaskAfterInsert As Boolean)

        'Private Shared Sub RefreshAdd(ByVal r As DsDocuments.DocumentsRow, ByVal wf As WorkFlow)
        '    RaiseEvent AddedTask(WFBusiness.GetNewResult(r, DirectCast(wf.Steps(CLng(r.step_Id)), WFStep)))
        'End Sub
#End Region

#Region "Remove"
        Public Shared Event RemovedTask(ByRef Result As TaskResult)

        Public Shared Sub Remove(ByVal taskId As Int64, ByVal deleteDocument As Boolean, ByVal docTypeId As Int64, ByVal fullpath As String, ByVal CurrentUserId As Int64)
            Try
                If (deleteDocument) Then
                    Results_Factory.Delete(taskId, docTypeId, fullpath, deleteDocument)
                End If

                WFTasksFactory.Delete(taskId)
                UserBusiness.Rights.SaveAction(CurrentUserId, ObjectTypes.WFTask, RightsType.Delete, "Borrar Tarea")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub



        '''' <summary>
        ''''     Remove a task  
        '''' </summary>
        '''' <param name="taskID"></param>
        '''' <param name="deleteDocument"></param>
        '''' <param name="wfstep"></param>
        '''' <param name="CurrentUserId"></param>
        '''' <param name="DocTypeId"></param>
        '''' <history>
        ''''                         Modified    Se modifica este metodo para que se le envie el wfstep al metodo como parametro, 
        ''''                                             para poder quitar por referencia las tareas de ese step
        ''''     Javier  06/12/2010  Modified    GetTaskByTaskIdAndDocTypeIdAndStepId is now a instance method
        '''' </history>
        'Public Shared Sub Remove(ByVal taskID As Int64, ByVal deleteDocument As Boolean, ByRef wfstep As IWFStep, ByVal CurrentUserId As Int64, ByVal DocTypeId As Int64)
        '    Try
        '        Dim taskbs As New WFTaskBusiness()

        '        Dim result As ITaskResult = taskbs.GetTaskByTaskIdAndDocTypeIdAndStepId(taskID, DocTypeId, wfstep.ID, 0)
        '        WFTaskBusiness.LogCheckOut(taskID)
        '        If Not IsNothing(wfstep) Then
        '            wfstep.TasksCount = wfstep.TasksCount - 1
        '        End If

        '        WFTasksFactory.Delete(result.TaskId)

        '        If deleteDocument Then
        '            Results_Factory.Delete(result)
        '        End If

        '        RaiseEvent RemovedTask(result)
        '        UserBusiness.Rights.SaveAction(CurrentUserId, ObjectTypes.WFTask, RightsType.Delete, "Se borro tarea: " & result.Name)
        '    Catch ex As Exception
        '        ZClass.raiseerror(ex)
        '    End Try
        'End Sub

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
        Public Shared Sub Remove(ByRef result As ITaskResult, ByVal deleteDocument As Boolean, ByVal CurrentUserId As Int64, ByVal DeleteFile As Boolean)
            Try
                If deleteDocument Then
                    Results_Factory.Delete(result, DeleteFile)
                End If
                Dim stepname As String = WFStepBusiness.GetStepNameById(result.StepId)
                Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(result.WorkId)

                WFTasksFactory.LogAction(result.TaskId, result.Name, result.DocTypeId, result.DocType.Name, result.StepId, result.State.ID, result.WorkId, "Egreso", Membership.MembershipHelper.CurrentUser.Name, stepname, workflowname)

                WFTasksFactory.Delete(result)
                RaiseEvent RemovedTask(result)
                RaiseEvent Distributed(result)

                result.WorkId = 0
                result.StepId = 0

                '[Sebastian 04-06-2009] se comento este método porque estaba realzando un remove dos veces
                'RaiseEvent refreshSteps(result.WfStep.WorkId, result.StepId, Nothing)

                UserBusiness.Rights.SaveAction(CurrentUserId, ObjectTypes.WFTask, RightsType.Delete, "Borrar Tarea")
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#End Region

#Region "Distribute"
        Public Shared Event Distributed(ByVal Result As TaskResult)
        Public Shared Event refreshSteps(ByVal oldStepID As Long, ByVal newStepID As Long)

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
        Public Shared Sub Distribute(ByRef Results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal newWFStepId As Int64, ByVal CurrentUserId As Int64)
            Dim Resultsaux As List(Of ITaskResult)
            Dim WFRB As WFRulesBusiness
            Dim stateChange As Boolean
            Dim newWFStep As WFStep
            Dim oldStepID, newStepID As Long

            oldStepID = Results(0).StepId

            Try
                newWFStep = WFStepBusiness.GetStepById(newWFStepId)
                newStepID = newWFStep.ID

                WFRB = New WFRulesBusiness()
                For Each Result As ITaskResult In Results
                    Dim wfstep As IWFStep = WFStepBusiness.GetStepById(Result.StepId, False)
                    Resultsaux = New List(Of ITaskResult)
                    Resultsaux.Add(Result)

                    'Ejecuto reglas de validacion de salida
                    'If Not wfstep.Rules Is Nothing Then
                    '    For Each r As WFRuleParent In wfstep.Rules
                    '        If r.ParentType = TypesofRules.ValidacionSalida Then
                    '            Resultsaux = WFRB.ExecutePrimaryRule(r, Resultsaux, Nothing)
                    '        End If
                    '    Next
                    'End If

                    Dim DVDSRules As New DataView(wfstep.DSRules.WFRules)
                    DVDSRules.RowFilter = "ParentType = 13"
                    For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                        Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), wfstep.ID, True)
                        Resultsaux = WFRB.ExecutePrimaryRule(Rule, Resultsaux, Nothing)
                    Next

                    If Resultsaux.Contains(Result) Then
                        'Ejecuto reglas de salida
                        'If Not wfstep.Rules Is Nothing Then
                        '    For Each r As WFRuleParent In wfstep.Rules
                        '        If r.ParentType = TypesofRules.Salida Then
                        '            Resultsaux = WFRB.ExecutePrimaryRule(r, Resultsaux, Nothing)
                        '        End If
                        '    Next
                        'End If

                        'Dim DVDSRules As New DataView(wfstep.DSRules.WFRules)
                        DVDSRules.RowFilter = "ParentType = 7"
                        For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                            Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), wfstep.ID, True)
                            Resultsaux = WFRB.ExecutePrimaryRule(Rule, Resultsaux, Nothing)
                        Next


                        'Derivo
                        Result.StepId = newWFStep.ID

                        'Si se intenta distribuir al mismo usuario que antes, no se loguea la accion
                        'If lastDistributedUser = 
                        If Result.AsignedById <> Result.AsignedToId Then
                            WFTaskBusiness.LogAsignedTask(Result)
                        End If

                        'LA NUEVA ETAPA VINO SIN REGLAS ASI QUE SE LAS AGREGO
                        '--------------------------------------------------
                        'If IsNothing(newWFStep.Rules) OrElse newWFStep.Rules.Count = 0 Then
                        '    WFRulesBusiness.FillRules(newWFStep, True)
                        'End If

                        'Ejecuto reglas de validacion de entrada
                        'If Not IsNothing(newWFStep.Rules) Then
                        '    For Each r As WFRuleParent In newWFStep.Rules
                        '        If r.ParentType = TypesofRules.ValidacionEntrada Then
                        '            Resultsaux = WFRB.ExecutePrimaryRule(r, Resultsaux, Nothing)
                        '        End If
                        '    Next
                        'End If
                        DVDSRules = New DataView(newWFStep.DsRules.WFRules)
                        DVDSRules.RowFilter = "ParentType = 12"
                        For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                            Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), newWFStep.ID, True)
                            Resultsaux = WFRB.ExecutePrimaryRule(Rule, Resultsaux, Nothing)
                        Next

                        If Resultsaux.Contains(Result) Then
                            'Estado de Tarea
                            Result.TaskState = Zamba.Core.TaskStates.Desasignada

                            'Estado Verifica si es estado original de la etapa anterior existe en la nueva etapa, si existe lo mantiene
                            stateChange = False
                            Try
                                For Each State As WFStepState In newWFStep.States
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

                            'Asignacion
                            If Result.AsignedToId <> 0 AndAlso WFStepBusiness.IsUserIdAsignedToStep(newWFStep.ID, Result.AsignedToId) = False Then
                                Result.AsignedToId = 0
                                Result.TaskState = TaskStates.Desasignada
                                WFTasksFactory.UpdateTaskState(Result)
                                WFTasksFactory.UpdateAssign(Result)
                            End If

                            If Result.AsignedById = 0 Then
                                Result.AsignedById = CurrentUserId
                            End If

                            Result.AsignedDate = Nothing
                            Result.ExpireDate = Nothing
                            RaiseEvent AsignedAndExpireDate(Result)

                            'Vencimiento
                            'RaiseEvent ChangedExpireDate(Result)

                            'Checkin
                            Result.CheckIn = Now

                            'actualiza base de datsos
                            WFTasksFactory.UpdateDistribuir(Result)

                            'Log Checkin
                            WFTaskBusiness.LogCheckIn(Result)

                            'Ejecuto reglas de entrada
                            'If Not IsNothing(newWFStep.Rules) Then
                            '    For Each r As WFRuleParent In newWFStep.Rules
                            '        If r.ParentType = TypesofRules.Entrada Then
                            '            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecuto las reglas de ENTRADA")
                            '            Resultsaux = WFRB.ExecutePrimaryRule(r, Resultsaux, Nothing)
                            '        End If
                            '    Next
                            'End If
                            ' Dim DVDSRules As New DataView(newwfstep.DSRules.WFRules)
                            DVDSRules.RowFilter = "ParentType = 6"
                            For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                                Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), newWFStep.ID, True)
                                Resultsaux = WFRB.ExecutePrimaryRule(Rule, Resultsaux, Nothing)
                            Next

                            'ML: SetLastUpdate comenta esta llamada, porque lo que hace es recorrer cada fila de la grilla de tareas
                            'Para quitar las tarea de la grilla, al final del metodo, se llama al refresh de todo el WF, por ende 
                            'ya se quita y se agrega en la nueva etapa
                            'Quito la tarea de la grilla y actualizo la visualizacion
                            RaiseEvent Distributed(Result)

                            If Not IsNothing(Resultsaux) Then
                                Resultsaux = Nothing
                            End If
                        End If
                    End If

                Next
                'Refresco el arbol de workflow
                RaiseEvent refreshSteps(oldStepID, newStepID)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                WFRB = Nothing
                stateChange = Nothing
                If Not IsNothing(newWFStep) Then
                    newWFStep.Dispose()
                    newWFStep = Nothing
                End If
            End Try
        End Sub

        '''' <summary>
        '''' Actualiza el estado de todas las tareas que tenga el usuario en ejecucion y las pasa a Asignadas
        '''' </summary>
        '''' <param name="UserID"></param>
        '''' <remarks></remarks>
        Public Shared Sub UpdateAllUserAsignedTasksState(ByVal UserID As Long)
            Try
                WFTasksFactory.UpdateAllUserAsignedTasksState(UserID)
            Catch ex As Exception
                ZClass.raiseerror(ex)
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
        'Public Shared Sub refreshStepsAfterDistribute(ByRef wfId As Long, ByRef oldStepId As Long, ByRef newStepId As Long, ByRef distributedTasks As Integer)
        '    RaiseEvent refreshSteps(wfId, oldStepId, newStepId, distributedTasks)
        'End Sub

        Private Shared Sub RefreshDistribute(ByVal t As TaskResult, ByVal r As DsDocuments.DocumentsRow)
            Try
                If t.StepId <> CLng(r.step_Id) Then
                    Dim wf As IWorkFlow
                    wf = WFBusiness.GetWorkFlow(t.WorkId)

                    RaiseEvent Distributed(t)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#End Region

#Region "Assign"

        Public Shared Event AsignedAndExpireDate(ByRef Result As TaskResult)
        Public Shared Sub Asign(ByRef result As ITaskResult, ByVal asignedToId As Int64, ByVal asignedById As Int64, ByVal logAction As Boolean, ByVal RefreshUI As Boolean)
            Try
                result.AsignedToId = asignedToId
                result.AsignedById = asignedById
                result.AsignedDate = Now
                If (result.AsignedToId = 0) Then
                    result.TaskState = TaskStates.Desasignada
                Else
                    result.TaskState = TaskStates.Asignada
                End If


                WFTasksFactory.UpdateAssign(result.TaskId, result.AsignedToId, result.AsignedById, result.AsignedDate, result.TaskState)

                If logAction Then
                    Dim asignedToName As String = UserGroupBusiness.GetUserorGroupNamebyId(result.AsignedToId)
                    If String.IsNullOrEmpty(asignedToName) Then
                        asignedToName = "Ninguno"
                    End If
                    Dim stepname As String = WFStepBusiness.GetStepNameById(result.StepId)
                    Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(result.WorkId)

                    WFTasksFactory.LogAsignedTask(result.TaskId, result.Name, result.DocType.ID, result.DocType.Name, result.StepId, result.State.Name, asignedToName, result.WorkId, Membership.MembershipHelper.CurrentUser.Name, stepname, workflowname)
                    asignedToName = Nothing
                End If

                If RefreshUI Then RaiseEvent AsignedAndExpireDate(result)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub


        Public Shared Sub Asign(ByRef result As ITaskResult, ByVal asignedToID As Int64, ByVal asignedByID As Int64, ByVal asignedToName As String)
            Try
                result.AsignedToId = asignedToID
                result.AsignedById = asignedByID
                result.AsignedDate = Now

                If asignedToID <> 0 Then
                    If result.TaskState = TaskStates.Ejecucion Then
                        Dim users As Generic.List(Of Long) = UserGroupBusiness.GetUsersIds(asignedByID, True)

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

                Dim stepname As String = WFStepBusiness.GetStepNameById(result.StepId)
                Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(result.WorkId)
                WFTasksFactory.UpdateAssign(result.TaskId, result.AsignedToId, result.AsignedById, result.AsignedDate, result.TaskState)
                WFTasksFactory.LogAsignedTask(result.TaskId, result.Name, result.DocType.ID, result.DocType.Name, result.StepId, result.State.Name, asignedToName, result.WorkId, Membership.MembershipHelper.CurrentUser.Name, stepname, workflowname)

                RaiseEvent AsignedAndExpireDate(result)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub


        Public Shared Sub Asign(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int64, ByVal docTypeName As String, ByVal stepId As Int32, ByVal stateId As Int32, ByVal userName As String, ByVal workflowId As Int32, ByVal asignedToUserId As Int64, ByVal asignedByUserId As Int64, ByVal asignedDate As Date)
            Dim stepname As String = WFStepBusiness.GetStepNameById(stepId)
            Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(workflowId)
            WFTasksFactory.UpdateAssign(taskID, asignedToUserId, asignedByUserId, asignedDate, TaskStates.Asignada)
            WFTasksFactory.LogAsignedTask(taskID, taskName, docTypeId, docTypeName, stepId, stateId, userName, workflowId, Membership.MembershipHelper.CurrentUser.Name, stepname, workflowname)
        End Sub


#End Region

#Region "UnAssign"
        Public Shared Sub UnAssign(ByRef Result As ITaskResult, ByVal AsignedBy As IUser)
            UnAssign(Result, AsignedBy.ID)
        End Sub
        Public Shared Sub UnAssign(ByRef Result As ITaskResult, ByVal asignedByUserId As Int64)
            Try
                Result.AsignedToId = 0
                Result.AsignedById = asignedByUserId
                Result.AsignedDate = Date.Now()
                Result.CheckIn = Nothing
                Result.TaskState = Zamba.Core.TaskStates.Desasignada

                Dim stepname As String = WFStepBusiness.GetStepNameById(Result.StepId)
                Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(Result.WorkId)
                Dim EntityName As String = DocTypesBusiness.GetDocTypeName(Result.DocTypeId, True)
                WFTasksFactory.UpdateAssign(Result)
                WFTasksFactory.LogAsignedTask(Result.TaskId, Result.Name, Result.DocTypeId, EntityName, Result.StepId, Result.State.Name, Result.AsignedToId, Result.WorkId, Membership.MembershipHelper.CurrentUser.Name, stepname, workflowname)

                RaiseEvent AsignedAndExpireDate(Result)
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
        Public Shared Sub Iniciar(ByRef Result As ITaskResult)
            Try
                Result.TaskState = Zamba.Core.TaskStates.Ejecucion
                WFTasksFactory.UpdateTaskState(Result)
                WFTaskBusiness.LogStart(Result)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Public Shared Sub Finalizar(ByRef TaskResult As ITaskResult, ByVal CurrentUserId As Int64)
            Try
                Asign(TaskResult, TaskResult.AsignedToId, CurrentUserId, False, False)
                WFTasksFactory.UpdateTaskState(TaskResult)
                WFTaskBusiness.LogFinish(TaskResult)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Public Shared Sub Derivar(ByVal Task As ITaskResult, ByVal stepId As Int64, ByVal asignedToId As Int64, ByVal asignedToName As String, ByVal asignedBy As Int64, ByVal asignedDate As Date, ByVal selecCarp As Boolean, ByVal CurrentUserId As Int64)
            'Dim CurrentStep As IWFStep = Nothing
            Try
                'CurrentStep = WFStepBusiness.GetStepById(stepId)

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

                Task.AsignedDate = asignedDate
                Task.TaskState = TaskStates.Asignada

                WFTaskBusiness.Asign(Task, asignedToId, asignedBy, asignedDate)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

#End Region

#Region "Form"
        'Public Shared Sub ChangeForm(ByRef Result As TaskResult)
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
        Private Shared Sub ExecutedSetState(ByVal TaskResult As ITaskResult)
            Dim Results As New System.Collections.Generic.List(Of Core.ITaskResult)
            Results.Add(TaskResult)

            Dim wfstep As IWFStep = WFStepBusiness.GetStepById(TaskResult.StepId, False)

            Dim DVDSRules As New DataView(wfstep.DSRules.WFRules)
            DVDSRules.RowFilter = "Type = 35"
            For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), wfstep.ID, True)
                Dim WFRB As New WFRulesBusiness
                WFRB.ExecutePrimaryRule(Rule, Results, Nothing)
                WFRB = Nothing
            Next


            Results.Clear()
            Results = Nothing
        End Sub


        ''' <summary>
        ''' Cambia el estado de la etapa
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <param name="State"></param>
        ''' <remarks></remarks>
        Public Shared Sub ChangeState(ByRef Result As ITaskResult, ByVal State As IWFStepState)
            Dim wfstep As IWFStep = WFStepBusiness.GetStepById(Result.StepId, False)
            If wfstep.States.Contains(State) Then
                Result.State = State
            Else
                If wfstep.States.Count > 0 Then Result.State = DirectCast(wfstep.States(0), WFStepState)
            End If
            WFTasksFactory.UpdateState(Result.TaskId, Result.StepId, State.ID)
            WFTaskBusiness.LogChangeStepState(Result)

            ExecutedSetState(Result)


        End Sub

#End Region

#Region "ChangeExpireDate"

        'ML: Metodo obsoleto, el asigned llama al expired date
        'Public Shared Event ChangedExpireDate(ByRef Result As TaskResult)

        Public Shared Sub ChangeExpireDate(ByRef Result As TaskResult, ByVal NuevaFecha As DateTime)
            Try
                Result.ExpireDate = NuevaFecha
                WFTasksFactory.UpdateExpiredDate(Result.TaskId, Result.ExpireDate)
                Dim stepname As String = WFStepBusiness.GetStepNameById(Result.StepId)
                Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(Result.WorkId)
                Dim EntityName As String = DocTypesBusiness.GetDocTypeName(Result.DocTypeId, True)
                WFTasksFactory.LogChangeExpireDate(Result.TaskId, Result.Name, Result.DocTypeId, EntityName, Result.StepId, Result.StateId, Result.WorkId, Membership.MembershipHelper.CurrentUser.Name, stepname, workflowname)
                RaiseEvent AsignedAndExpireDate(Result)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        Friend Shared Sub ChangeExpireDate(ByVal taskId As Int64, ByVal expireDate As Date)
            WFTasksFactory.UpdateExpiredDate(taskId, expireDate)


        End Sub

#End Region



#Region "CheckIn"
        Private Shared Sub RefreshCheckIn(ByVal t As TaskResult, ByVal r As DsDocuments.DocumentsRow)
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
        Private Shared Sub RefreshAsignedBy(ByVal t As TaskResult, ByVal r As DsDocuments.DocumentsRow)
            Try
                If t.AsignedById <> 0 Then
                    If t.AsignedById <> (r.User_Asigned_By) Then
                        If WFBusiness.GetAllUsers.Contains(Convert.ToInt64(r.User_Asigned_By)) Then
                            't.AsignedBy = WFBusiness.GetAllUsers(Convert.ToInt64(r.User_Asigned_By))
                            t.AsignedById = CInt(r.User_Asigned_By)
                        Else
                            t.AsignedById = DirectCast(WFBusiness.GetAllUsers().GetByIndex(0), User).ID
                        End If
                    End If
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End Sub
#End Region

#Region "AsignedDate"
        Private Shared Sub RefreshAsignedDate(ByVal t As TaskResult, ByVal r As DsDocuments.DocumentsRow)
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
        Public Shared Sub LoadMonitor(ByVal WF As WorkFlow, ByVal TreeView As TreeView)
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
        '                    Dim tasks As Hashtable = WFTaskBusiness.GetHashTableTasksByStep(s)
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

        Private Shared Sub UpdateNodesTasksCountExtracted(ByVal n As TreeNode)
            DirectCast(n, MonitorStepNode).UpdateNodeText()
        End Sub

        Public Shared Sub UpdateNodesTasksCount(ByVal WFNode As WFNode)
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

        Public Shared Sub LogUserAction(ByVal taskID As Int64, ByVal taskName As String, ByVal docTypeId As Int64, ByVal docTypeName As String, ByVal stepId As Int32, ByVal state As String, ByVal workflowId As Int32, ByVal accionDeUsuario As String)
            Dim stepname As String = WFStepBusiness.GetStepNameById(stepId)
            Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(workflowId)
            WFTasksFactory.LogUserAction(taskID, taskName, docTypeId, docTypeName, stepId, state, workflowId, accionDeUsuario, Membership.MembershipHelper.CurrentUser.Name, stepname, workflowname)
        End Sub

        Public Shared Sub LogOtherActions(ByVal taskId As Int64, ByVal taskName As String, ByVal docTypeId As Int64, ByVal docTypeName As String, ByVal stepId As Int32, ByVal state As String, ByVal workflowId As Int32, ByVal comment As String)
            Dim stepname As String = WFStepBusiness.GetStepNameById(stepId)
            Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(workflowId)
            WFTasksFactory.LogOtherActions(taskId, taskName, docTypeId, docTypeName, stepId, state, workflowId, comment, Membership.MembershipHelper.CurrentUser.Name, stepname, workflowname)
        End Sub

        ''' <summary>
        ''' Loguea la accion especificada para la tarea
        ''' </summary>
        ''' <param name="taskID">ID de la tarea</param>
        ''' <param name="action">Accion a loguear</param>
        ''' <remarks></remarks>
        Public Shared Sub LogAction(ByVal taskID As Int64, ByVal action As String)
        End Sub

        Public Shared Sub LogCheckIn(ByVal TaskResult As TaskResult, Optional ByRef t As Transaction = Nothing)
            '[Tomás] - 21/05/2009 - Modified - Se valida si el WF tiene etpa inicial ya que generaba exception. 
            If TaskResult.StepId <> 0 Then
                Dim stepname As String = WFStepBusiness.GetStepNameById(TaskResult.StepId)
                Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(TaskResult.WorkId)
                WFTasksFactory.LogCheckInOut(TaskResult.TaskId, TaskResult.Name, TaskResult.DocTypeId, TaskResult.DocType.Name, TaskResult.StepId, TaskResult.State.Name, TaskResult.WorkId, TaskResult.CheckIn, stepname, workflowname, t, Membership.MembershipHelper.CurrentUser.Name, "Ingreso de Tarea")
            Else
                Throw New Exception("No hay definida una etapa inicial en el Workflow: " & TaskResult.WorkId)
            End If
        End Sub




        Public Shared Sub LogStart(ByVal TaskResult As TaskResult)
            Dim stepname As String = WFStepBusiness.GetStepNameById(TaskResult.StepId)
            Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(TaskResult.WorkId)
            Dim EntityName As String = DocTypesBusiness.GetDocTypeName(TaskResult.DocTypeId, True)
            WFTasksFactory.LogStartTask(TaskResult.TaskId, TaskResult.Name, TaskResult.DocTypeId, EntityName, TaskResult.StepId, TaskResult.State.Name, TaskResult.WorkId, Membership.MembershipHelper.CurrentUser.Name, stepname, workflowname)
        End Sub

        Public Shared Sub LogFinish(ByVal TaskResult As TaskResult)
            Dim stepname As String = WFStepBusiness.GetStepNameById(TaskResult.StepId)
            Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(TaskResult.WorkId)
            Dim EntityName As String = DocTypesBusiness.GetDocTypeName(TaskResult.DocTypeId, True)
            WFTasksFactory.LogFinishTask(TaskResult.TaskId, TaskResult.Name, TaskResult.DocTypeId, EntityName, TaskResult.StepId, TaskResult.State.Name, TaskResult.WorkId, Membership.MembershipHelper.CurrentUser.Name, stepname, workflowname)
        End Sub


        Public Shared Sub LogChangeStepState(ByVal TaskResult As TaskResult)
            Dim stepname As String = WFStepBusiness.GetStepNameById(TaskResult.StepId)
            Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(TaskResult.WorkId)
            Dim EntityName As String = DocTypesBusiness.GetDocTypeName(TaskResult.DocTypeId, True)
            WFTasksFactory.LogCheckInOut(TaskResult.TaskId, TaskResult.Name, TaskResult.DocTypeId, EntityName, TaskResult.StepId, TaskResult.State.Name, TaskResult.WorkId, Now, stepname, workflowname, Nothing, Membership.MembershipHelper.CurrentUser.Name, "Cambio Estado : " & TaskResult.State.Name)
        End Sub

        Public Shared Sub LogChangeStepState(ByVal TaskResult As TaskResult, ByRef transact As Transaction)
            Dim stepname As String = WFStepBusiness.GetStepNameById(TaskResult.StepId)
            Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(TaskResult.WorkId)
            Dim EntityName As String = DocTypesBusiness.GetDocTypeName(TaskResult.DocTypeId, True)
            WFTasksFactory.LogCheckInOut(TaskResult.TaskId, TaskResult.Name, TaskResult.DocTypeId, EntityName, TaskResult.StepId, TaskResult.State.Name, TaskResult.WorkId, Now, stepname, workflowname, transact, Membership.MembershipHelper.CurrentUser.Name, "Cambio Estado : " & TaskResult.State.Name)
        End Sub

        Public Shared Sub LogReject(ByVal TaskResult As TaskResult)
            Dim stepname As String = WFStepBusiness.GetStepNameById(TaskResult.StepId)
            Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(TaskResult.WorkId)
            WFTasksFactory.LogRejectTask(TaskResult.TaskId, TaskResult.Name, TaskResult.DocTypeId, TaskResult.DocType.Name, TaskResult.StepId, TaskResult.State.Name, TaskResult.WorkId, Membership.MembershipHelper.CurrentUser.Name, stepname, workflowname)
        End Sub


        Public Shared Sub LogView(ByVal TaskResult As TaskResult)
            Dim stepname As String = WFStepBusiness.GetStepNameById(TaskResult.StepId)
            Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(TaskResult.WorkId)
            WFTasksFactory.LogViewTask(TaskResult.TaskId, TaskResult.Name, TaskResult.DocTypeId, TaskResult.DocType.Name, TaskResult.StepId, TaskResult.State.Name, TaskResult.WorkId, Membership.MembershipHelper.CurrentUser.Name, stepname, workflowname)
        End Sub



        Public Shared Sub LogAsignedTask(ByVal TaskResult As TaskResult)
            Dim stepname As String = WFStepBusiness.GetStepNameById(TaskResult.StepId)
            Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(TaskResult.WorkId)
            WFTasksFactory.LogAsignedTask(TaskResult.TaskId, TaskResult.Name, TaskResult.DocTypeId, TaskResult.DocType.Name, TaskResult.StepId, TaskResult.State.Name, TaskResult.AsignedToId, TaskResult.WorkId, Membership.MembershipHelper.CurrentUser.Name, stepname, workflowname)
        End Sub

#End Region

#Region "Save"
        Public Shared Sub SaveIntoIP_Task(ByVal alFiles As ArrayList, ByVal ZipOrigen As String, ByVal conf_Id As Decimal)
            If alFiles.Count > 0 Then
                Try
                    Dim i As Integer
                    For i = 0 To alFiles.Count - 1
                        WFTasksFactory.SaveIntoIP_Task(alFiles(i).ToString, CoreBusiness.GetNewID(IdTypes.IPTASKID), ZipOrigen, conf_Id)
                    Next
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                    'log.WriteEvent(Environment.UserName, Now, "Save Zip Files", exc.Message, Me.conf_Id, Environment.MachineName)
                End Try
            End If
        End Sub

        Public Shared Sub SaveIntoIP_Task(ByVal alFiles As ArrayList, ByVal conf_Id As Decimal)
            If alFiles.Count > 0 Then
                Try
                    Dim i As Integer
                    For i = 0 To alFiles.Count - 1
                        WFTasksFactory.SaveIntoIP_Task(alFiles(i), CoreBusiness.GetNewID(IdTypes.IPTASKID), conf_Id)
                    Next
                Catch ex As Exception
                    Zamba.Core.ZClass.raiseerror(ex)
                End Try
            End If
        End Sub
#End Region

#Region "WebParts"

        Public Shared Function GetTasksToExpireGroupByStep(ByVal workflowid As Int64, ByVal FromHours As Int32, ByVal ToHours As Int32) As DataSet
            Return WFTasksFactory.GetTasksToExpireGroupByStep(workflowid, FromHours, ToHours)
        End Function

        Public Shared Function GetTasksToExpireGroupByUser(ByVal workflowid As Int64, ByVal FromHours As Int32, ByVal ToHours As Int32) As DataSet
            Return WFTasksFactory.GetTasksToExpireGroupByUser(workflowid, FromHours, ToHours)
        End Function

        Public Shared Function GetExpiredTasksGroupByUser(ByVal workflowid As Int64) As DataSet
            Return WFTasksFactory.GetExpiredTasksGroupByUser(workflowid)
        End Function

        Public Shared Function GetExpiredTasksGroupByStep(ByVal workflowid As Int64) As DataSet
            Return WFTasksFactory.GetExpiredTasksGroupByStep(workflowid)
        End Function


        Public Shared Function GetTasksBalanceGroupByWorkflow(ByVal workflowid As Int64) As DataSet
            Return WFTasksFactory.GetTasksBalanceGroupByWorkflow(workflowid)
        End Function

        Public Shared Function GetTasksBalanceGroupByStep(ByVal stepid As Int64) As DataSet
            Return WFTasksFactory.GetTasksBalanceGroupByStep(stepid)
        End Function

        Public Shared Function GetAsignedTasksCountsGroupByUser(ByVal workflowid As Int64) As DataSet
            Return WFTasksFactory.GetAsignedTasksCountsGroupByUser(workflowid)
        End Function

        Public Shared Function GetAsignedTasksCountsGroupByStep(ByVal workflowid As Int64) As DataSet
            Return WFTasksFactory.GetAsignedTasksCountsGroupByStep(workflowid)
        End Function

        Public Shared Function GetTaskConsumedMinutesByWorkflowGroupByUsers(ByVal workflowid As Int64) As DataSet
            Return WFTasksFactory.GetTaskConsumedMinutesByWorkflowGroupByUsers(workflowid)
        End Function

        Public Shared Function GetTaskConsumedMinutesByStepGroupByUsers(ByVal stepid As Int64) As DataSet
            Return WFTasksFactory.GetTaskConsumedMinutesByStepGroupByUsers(stepid)
        End Function

        Public Shared Function GetTasksAverageTimeInSteps(ByVal workflowid As Int64) As Hashtable
            Return WFTasksFactory.GetTasksAverageTimeInSteps(workflowid)
        End Function

        Public Shared Function GetTasksAverageTimeByStep(ByVal stepid As Int64) As Hashtable
            Return WFTasksFactory.GetTasksAverageTimeByStep(stepid)
        End Function

#End Region


        Public Shared Function GetTaskHistory(ByVal taskId As Int64, ByVal doctypeId As Long, Optional ByVal fc As IFiltersComponent = Nothing) As DataSet

            Dim Filters As String
            If fc IsNot Nothing Then
                Dim isCaseSensitive As Boolean = UserPreferences.getValue("CaseSensitive", Sections.UserPreferences, True)
                Dim currentUserID = Membership.MembershipHelper.CurrentUser.ID

                Dim filterElements = fc.GetLastUsedFilters(doctypeId, currentUserID, FilterTypes.History)
                Filters = CType(fc, FiltersComponent).GetFiltersString(filterElements, False, isCaseSensitive)
            End If

            Return WFTasksFactory.GetTaskHistoryByResultId(taskId, Filters)
        End Function
        Public Shared Function GetOnlyIndexsHistory(ByVal doc_Id As Int64) As DataSet
            Dim ds As DataSet
            ds = WFTasksFactory.GetOnlyIndexsHistory(doc_Id)
            'Agrego las columnas faltantes
            ds.Tables(0).Columns.Add("Herramienta", Type.GetType("System.String"))
            ds.Tables(0).Columns.Item("Herramienta").SetOrdinal(1)
            ds.Tables(0).Columns.Add("Accion", Type.GetType("System.String"))
            ds.Tables(0).Columns.Item("Accion").SetOrdinal(2)
            For Each row As DataRow In ds.Tables(0).Rows
                row.Item("Herramienta") = "Documentos"
                row.Item("Accion") = "Edición"
            Next
            Return ds
        End Function

        ''' <summary>
        ''' Devuelve si la tarea fue modificada en el servidor o no desde la fecha pasada como parametro
        ''' </summary>
        ''' <param name="TaskId">Id de la tarea</param>
        ''' <param name="modifiedDate">Fecha de la anterior modificacion</param>
        ''' <history>Marcelo created 26/02/2009</history>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetIFModifiedTaskHistoryByResultId(ByVal TaskId As Int64, ByVal modifiedDate As DateTime) As Boolean
            Dim lastModified As DateTime = WFTasksFactory.GetLastModifiedTaskHistoryByResultId(TaskId)

            If DateTime.Compare(lastModified, modifiedDate) > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shared Function GetTaskAprobementsHistory(ByVal TaskId As Int64) As DataSet
            Return WFTasksFactory.GetTaskAprobementsHistoryByTaskId(TaskId)

        End Function

        ''' <summary>
        ''' Método que sirve para obtener un dataset que tendrá el historial de los task id seleccionados (RequestAction)
        ''' </summary>
        ''' <param name="tasksIds"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history> 
        '''     [Gaston] 22/07/2008 Created
        ''' </history>
        Public Shared Function getTasksRequestActionHistory(ByRef taskId As Int64) As DataSet
            Return (WFTasksFactory.getTasksRequestActionHistory(taskId))
        End Function

        ''' <summary>
        ''' Método que sirve para obtener un dataset con todas las tareas en las que se encuentra el documento
        ''' </summary>
        ''' <param name="docId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetTasksByTask(ByVal result As ITaskResult) As DataSet
            Return WFTasksFactory.GetTasksByTask(result)
        End Function


#Region "ExecuteEventRules"
        Public Sub ExecuteEventRules(ByVal TaskResult As ITaskResult, ByVal withcache As Boolean, ByVal ruleType As TypesofRules)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando reglas de atributos")
            Dim Results As System.Collections.Generic.List(Of Core.ITaskResult)
            Dim DVDSRules As DataView

            Try
                Dim wfstep As IWFStep = WFStepBusiness.GetStepById(TaskResult.StepId, False)

                'If wfstep.Rules.Count = 0 Then
                '    WFRulesBusiness.FillRules(wfstep, withcache)
                'End If

                'For Each Rule As WFRuleParent In wfstep.Rules
                '    If Rule.RuleType = ruleType Then
                '        If IsNothing(Results) Then
                '            Results = New System.Collections.Generic.List(Of Core.ITaskResult)
                '            Results.Add(TaskResult)
                '        End If

                '        Dim WFRB As New WFRulesBusiness
                '        Dim list As List(Of ITaskResult) = WFRB.ExecutePrimaryRule(Rule, Results, Nothing)
                '        list = Nothing
                '        WFRB = Nothing
                '    End If
                'Next

                DVDSRules = New DataView(wfstep.DSRules.WFRules)
                DVDSRules.RowFilter = "Type = " & ruleType
                For Each RuleRow As DataRow In DVDSRules.ToTable().Rows ' wfstep.Rules
                    If IsNothing(Results) Then
                        Results = New System.Collections.Generic.List(Of Core.ITaskResult)
                        Results.Add(TaskResult)
                    End If

                    Dim WFRB As New WFRulesBusiness
                    Dim Rule As IWFRuleParent = WFRulesBusiness.GetInstanceRuleById(RuleRow("Id"), wfstep.ID, True)
                    WFRB.ExecutePrimaryRule(Rule, Results, Nothing)
                    WFRB = Nothing
                Next


            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                Dim i As Int32
                If Not IsNothing(Results) Then
                    For i = 0 To Results.Count - 1
                        If Not IsNothing(Results(i)) Then
                            Results(i) = Nothing
                        End If
                    Next
                    Results.Clear()
                    Results = Nothing
                End If
                If Not IsNothing(DVDSRules) Then
                    DVDSRules.Dispose()
                    DVDSRules = Nothing
                End If
            End Try
        End Sub
#End Region

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
        Private Shared Sub CreateTasksRestrictionsWhere(ByRef sbValue As StringBuilder, ByVal Indexs As List(Of IIndex), ByRef i As Int64, ByRef FlagCase As Boolean, ByRef ColumCondstring As StringBuilder, ByRef First As Boolean, ByRef dateDeclarationString As StringBuilder)
            Dim mainValue As Object = Nothing
            Dim tempValue As Object = Nothing
            Dim dateString As New StringBuilder

            sbValue.Remove(0, sbValue.Length)
            If (String.IsNullOrEmpty(Indexs(i).dataDescription)) Then
                sbValue.Append(Indexs(i).Data)
            Else
                sbValue.Append(Indexs(i).dataDescription)
            End If

            Dim indexColName As String = Indexs(i).Column

            If sbValue.Length <> 0 OrElse Indexs(i).[Operator].ToLower = "es nulo" Then
                If Indexs(i).[Operator] <> "SQL" Then
                    If sbValue.ToString.Split(";").Length > 1 Then
                        Select Case Indexs(i).Type
                            Case IndexDataType.Alfanumerico
                                FlagCase = True
                                mainValue = "'" & LCase(sbValue.Replace(";", "';'").ToString) & ""
                            Case IndexDataType.Alfanumerico_Largo
                                FlagCase = True
                                mainValue = "'" & LCase(sbValue.Replace(";", "';'").ToString) & ""
                        End Select
                    Else
                        Select Case Indexs(i).Type
                            Case IndexDataType.Numerico, IndexDataType.Numerico_Largo
                                If sbValue.Length <> 0 Then
                                    If Indexs(i).DropDown = IndexAdditionalType.AutoSustitución _
                                         Or Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        mainValue = "'" & sbValue.ToString & ""
                                    Else
                                        'Valuestring1 = Int64.Parse(valueString.ToString)
                                        mainValue = sbValue.ToString()
                                    End If
                                End If
                            Case IndexDataType.Numerico_Decimales, IndexDataType.Moneda
                                If sbValue.Length <> 0 Then
                                    If System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString = "," Then sbValue = sbValue.Replace(".", ",")
                                    mainValue = CDec(sbValue.ToString)
                                    mainValue = mainValue.ToString.Replace(",", ".")
                                End If
                            Case IndexDataType.Si_No
                                If sbValue.Length <> 0 Then
                                    mainValue = Int64.Parse(sbValue.ToString)
                                End If
                            Case IndexDataType.Fecha
                                If Not String.IsNullOrEmpty(sbValue.ToString.Trim) Then
                                    If Server.isSQLServer Then
                                        'Se optimiza para sql server
                                        mainValue = "@fecdesde" & Indexs(i).Column
                                        dateString.Append("declare " & mainValue & " datetime ")
                                        dateString.Append("set " & mainValue & " = " & Server.Con.ConvertDate(sbValue.ToString) & " ")
                                        dateDeclarationString.Append(dateString)
                                    Else
                                        mainValue = Server.Con.ConvertDate(sbValue.ToString)
                                    End If
                                End If
                            Case IndexDataType.Fecha_Hora
                                If Not String.IsNullOrEmpty(sbValue.ToString.Trim) Then
                                    If Server.isSQLServer Then
                                        'Se optimiza para sql server
                                        mainValue = "@fecdesde" & Indexs(i).Column
                                        dateString.Append("declare " & mainValue & " datetime ")
                                        dateString.Append("set " & mainValue & " = " & Server.Con.ConvertDateTime(sbValue.ToString) & " ")
                                        dateDeclarationString.Append(dateString)
                                    Else
                                        mainValue = Server.Con.ConvertDateTime(sbValue.ToString)
                                    End If
                                End If
                            Case IndexDataType.Alfanumerico, IndexDataType.Alfanumerico_Largo
                                If FlagCase Then
                                    mainValue = "'" & LCase(sbValue.ToString) & ""
                                Else
                                    mainValue = "'" & sbValue.ToString & ""
                                End If
                        End Select
                    End If
                Else
                    mainValue = Indexs(i).Data
                End If

                Dim Op As String = mainValue.ToString
                If Op.Contains("''") Then Op = Op.Replace("''", "'")
                mainValue = Op
                Op = String.Empty

                Dim separator As String = " AND"
                If ColumCondstring.Length > 0 Then
                    ColumCondstring.Append(separator)
                End If

                Select Case Indexs(i).[Operator]
                    Case "="
                        Op = "="
                        If FlagCase = True AndAlso (Indexs(i).Type = IndexDataType.Alfanumerico OrElse Indexs(i).Type = IndexDataType.Alfanumerico_Largo) Then
                            ColumCondstring.Append(" (lower(" & indexColName & ") " & Op & " " & mainValue & ")")
                        Else
                            ColumCondstring.Append(" (" & indexColName & " " & Op & " " & mainValue & ")")
                        End If

                    Case ">"
                        Op = ">"
                        ColumCondstring.Append(" (" & indexColName & " " & Op & " " & mainValue & ")")

                    Case "<"
                        Op = "<"
                        ColumCondstring.Append(" (" & indexColName & " " & Op & " " & mainValue & ")")
                    Case "Es nulo"
                        Op = "is null"
                        ColumCondstring.Append(" (" & indexColName & " " & Op & " " & ")")

                    Case ">="
                        Op = ">="
                        ColumCondstring.Append(" (" & indexColName & " " & Op & " " & mainValue & ")")

                    Case "<="
                        Op = "<="
                        ColumCondstring.Append(" (" & indexColName & " " & Op & " " & mainValue & ")")
                    Case "<>"
                        Op = "<>"
                        ColumCondstring.Append(" (" & indexColName & " " & Op & " " & mainValue & " or " & indexColName & " is null)")
                    Case "Entre"
                        Dim Data2Added As Boolean
                        Try
                            'cambio las a como indice a I ya que todos los atributos vienen con dato algunos vacio otros no.
                            Select Case Indexs(i).Type
                                Case 1, 2
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        tempValue = Int64.Parse(Indexs(i).Data2)
                                        Data2Added = True
                                    End If
                                Case 3
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        tempValue = Decimal.Parse(Indexs(i).Data2)
                                        Data2Added = True
                                    End If
                                Case 4
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        If Not String.IsNullOrEmpty(sbValue.ToString.Trim) Then
                                            If Server.isSQLServer Then
                                                'Se optimiza para sql server
                                                tempValue = "@fechasta" & indexColName
                                                dateString.Remove(0, dateString.Length)
                                                dateString.Append("declare " & tempValue & " datetime ")
                                                dateString.Append("set " & tempValue & " = " & Server.Con.ConvertDate(Indexs(i).Data2) & " ")
                                                dateDeclarationString.Append(dateString)
                                            Else
                                                tempValue = Server.Con.ConvertDate(Indexs(i).Data2)
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
                                                tempValue = "@fechasta" & indexColName
                                                dateString.Remove(0, dateString.Length)
                                                dateString.Append("declare " & tempValue & " datetime ")
                                                dateString.Append("set " & tempValue & " = " & Server.Con.ConvertDateTime(Indexs(i).Data2) & " ")
                                                dateDeclarationString.Append(dateString)
                                            Else
                                                tempValue = Server.Con.ConvertDateTime(Indexs(i).Data2)
                                            End If
                                        End If
                                        Data2Added = True
                                    End If
                                Case 6
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        tempValue = CDec(Indexs(i).Data2)
                                        Data2Added = True
                                    End If
                                Case 7, 8
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        FlagCase = True
                                        tempValue = "'" & LCase(DirectCast(Indexs(i).Data2, String)) & ""
                                        Data2Added = True
                                    End If
                            End Select
                        Catch ex As Exception
                            Throw New Exception("Ocurrio un error al convertir al tipo de Dato: Dato: " & sbValue.ToString & ", Tipo Dato: " & Indexs(i).Type & " " & ex.ToString)

                        End Try

                        If Data2Added = True Then
                            If Server.isSQLServer And (Indexs(i).Type = IndexDataType.Fecha OrElse Indexs(i).Type = IndexDataType.Fecha_Hora) Then

                                ColumCondstring.Append(" (" & indexColName & " BETWEEN " & mainValue & " and " & tempValue & ")")
                            Else
                                ColumCondstring.Append(" (" & indexColName & " >= " & mainValue & " and " & indexColName & " <= " & tempValue & ")")
                            End If
                            Data2Added = False
                        End If
                    Case "Contiene"
                        If FlagCase = True Then
                            ColumCondstring.Append(" (lower(" & indexColName & ") Like '%" & Replace(Trim(mainValue), "'", "") & "%')")
                        Else
                            ColumCondstring.Append(" (" & indexColName & " Like '%" & Replace(Trim(mainValue), "'", "") & "%')")
                        End If
                    Case "Empieza"
                        If FlagCase = True Then
                            ColumCondstring.Append(" (lower(" & indexColName & ") Like '" & Replace(Trim(mainValue), "'", "") & "%')")
                        Else
                            ColumCondstring.Append(" (" & indexColName & " Like '" & Replace(Trim(mainValue), "'", "") & "%')")
                        End If
                    Case "Termina"
                        If FlagCase = True Then
                            ColumCondstring.Append(" (lower(" & indexColName & ") Like '%" & Replace(Trim(mainValue), "'", "") & "')")
                        Else
                            ColumCondstring.Append(" (" & indexColName & " Like '%" & Replace(Trim(mainValue), "'", "") & "')")
                        End If

                    Case "Alguno"
                        Op = "LIKE"
                        mainValue = mainValue.Replace(";", ",")
                        mainValue = mainValue.Replace("  ", " ")
                        mainValue = mainValue.Replace(" ", ",")
                        Dim SomeValues As Array = DirectCast(mainValue, String).Split(",")
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
                    Case "Distinto"
                        Op = "NOT LIKE"
                        mainValue = mainValue.Replace(";", ",")
                        mainValue = mainValue.Replace("  ", " ")
                        mainValue = mainValue.Replace(" ", ",")
                        Dim SomeValues As Array = DirectCast(mainValue, String).Split(",")
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
                    Case "Dentro"
                        If FlagCase = True Then
                            Select Case Indexs(i).Type
                                Case IndexDataType.Numerico, IndexDataType.Numerico_Largo, IndexDataType.Moneda
                                    If Indexs(i).DropDown = IndexAdditionalType.AutoSustitución _
                                        Or Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        ColumCondstring.Append(" (lower(" & indexColName & ") in (" & mainValue & "'))")
                                    Else
                                        ColumCondstring.Append(" (lower(" & indexColName & ") in (" & mainValue & "))")
                                    End If
                                Case Else
                                    ColumCondstring.Append(" (lower(" & indexColName & ") in (" & mainValue & "'))")
                            End Select

                        Else
                            ColumCondstring.Append(" (" & indexColName & " in (" & mainValue & "))")
                        End If

                    Case "SQL"
                        ColumCondstring.Append(" (" & indexColName & " in (" & mainValue & "))")
                End Select
                Op = Nothing
                separator = Nothing
            End If

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
        ''' </history>
        Public Shared Function GetTaskByDocId(ByVal docId As Int64, Optional ByVal WorkId As Int64 = 0) As ITaskResult
            Dim ds As DataSet = WFTasksFactory.GetTaskIdByDocId(docId)

            If Not IsNothing(ds) And ds.Tables.Count = 1 And ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows.Count = 1 Or WorkId = 0 Then
                    Return WFTaskBusiness.GetTask(ds.Tables(0).Rows(0)(0))
                Else
                    ds.Tables(0).Select("Work_id=" + WorkId)
                    If ds.Tables(0).Rows.Count > 0 Then
                        Return WFTaskBusiness.GetTask(ds.Tables(0).Rows(0)(0))
                    Else
                        Return Nothing
                    End If
                End If
            End If

        End Function



        ''' <summary>
        ''' Obtiene una tarea en particular
        ''' </summary>
        ''' <param name="taskId">Id de la tarea</param>
        ''' <param name="WfStep">Etapa de la tarea (opcional)</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetTask(ByVal taskId As Int64, Optional ByVal WfStep As IWFStep = Nothing, Optional ByVal useRestrictions As Boolean = True) As ITaskResult

            Dim Task As ITaskResult = Nothing
            Dim ds As DataSet = WFTasksFactory.GetTaskByTaskId(taskId)

            If Not IsNothing(ds) And ds.Tables.Count = 1 And ds.Tables(0).Rows.Count > 0 Then
                Dim tasksids As New List(Of Int64)
                tasksids.Add(taskId)
                Dim Tasks As List(Of ITaskResult)
                Tasks = WFTaskBusiness.GetTasksByTaskIdsAndDocTypeIdAndStepId(tasksids, ds.Tables(0).Rows(0)("DOC_TYPE_ID"), ds.Tables(0).Rows(0)("step_id"), useRestrictions)
                If Not IsNothing(Tasks) AndAlso Tasks.Count > 0 Then
                    Task = Tasks(0)
                End If
            End If
            Return Task
        End Function

        ''' <summary>
        ''' Guarda la ultima modificacion
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <param name="comment"></param>
        ''' <remarks></remarks>
        Public Function SetLastUpdate(ByVal Result As ITaskResult, ByVal comment As String, ByVal saveHistory As Boolean) As Int64 Implements IWFTaskBusiness.SetLastUpdate
            Dim newsID As Int64 = CoreData.GetNewID(IdTypes.News)
            comment = TextoInteligente.ReconocerCodigo(comment, Result)

            Dim _Newsfactory As New Newsfactory()
            _Newsfactory.SaveNews(newsID, Result.ID, Result.DocTypeId, comment)
            _Newsfactory = Nothing

            If saveHistory = True Then
                Dim stepname As String = WFStepBusiness.GetStepNameById(Result.StepId)
                Dim workflowname As String = WFBusiness.GetWorkflowNameByWFId(Result.WorkId)
                Dim EntityName As String = DocTypesBusiness.GetDocTypeName(Result.DocTypeId, True)
                WFTasksFactory.LogAction(Result.TaskId, Result.Name, Result.DocTypeId, EntityName, Result.StepId, Result.State.Name, Result.WorkId, comment, Membership.MembershipHelper.CurrentUser.Name, stepname, workflowname)
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
        Public Function GetTaskCount(ByVal stepId As Int64, ByVal WithGridRights As Boolean, ByVal CurrentUserID As Int64) As Int32
            Dim DTList As ArrayList = WFStepBusiness.GetDocTypesAssociatedToWFbyStepId(stepId)
            Dim taskCount As Int32 = 0

            For Each docid As Int64 In DTList
                taskCount += Int32.Parse(GetTasksByStepandDocTypeId(stepId, docid, WithGridRights, CurrentUserID, Nothing, Int32.MaxValue, 0, SearchType.WFStepCount, String.Empty, Nothing).Rows(0)(0).ToString)
            Next

            Return taskCount
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="myrule"></param>
        ''' <param name="r"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ValidateVar(ByVal r As ITaskResult, ByVal TxtVar As String, ByVal Operador As String, ByVal Value As String) As Boolean

            Dim zValue, zvar, rulevalue As String
            Dim ds As DataSet
            Dim taskResult As New Generic.List(Of Core.ITaskResult)()

            zValue = String.Empty
            zvar = String.Empty

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando Variable")
            If TxtVar.IndexOf("zvar(", StringComparison.CurrentCultureIgnoreCase) <> -1 Then
                zvar = TxtVar.Replace("zvar(", String.Empty)
                zvar = zvar.Replace(")", String.Empty).Trim()
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
                        zValue = String.Empty
                    End If
                ElseIf (VariablesInterReglas.Item(zvar)) Is Nothing Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable Vacia")
                    zValue = String.Empty
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
            rulevalue = WFRuleParent.ReconocerVariablesValuesSoloTexto(Value)

            rulevalue = TextoInteligente.ReconocerCodigo(rulevalue, r)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Variable a comparar Obtenida: " & rulevalue)

            If String.IsNullOrEmpty(rulevalue) Then
                rulevalue = Value
            End If
            ' para mantener la funcionalidad vieja ingresamos False como parametro
            Return ToolsBusiness.ValidateComp(zValue, rulevalue, Operador, False)
        End Function

        ''' <summary>
        ''' Obtiene un listado de ids de tareas que se encuentran en una etapa deseada.
        ''' Los ids se encuentran ordenados de menor a mayor.
        ''' </summary>
        ''' <param name="stepId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetTaskIdsByStepId(stepId As Int64) As List(Of Int64)
            Dim dt As DataTable = WFTasksFactory.GetTaskIdsByStepId(stepId)
            Dim taskIds As New List(Of Int64)

            For Each r As DataRow In dt.Rows
                taskIds.Add(CLng(r(0)))
            Next

            Return taskIds
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
                If Server.isOracle Then
                    currentLockedUser = Zamba.Servers.Server.Con.ExecuteScalar(CommandType.Text, String.Format("select (apellido || ',' || nombres) as Usuario from usrtable where id = (select c_exclusive from wfdocument where task_id = {0})", taskId))
                    Return False
                Else
                    currentLockedUser = Zamba.Servers.Server.Con.ExecuteScalar(CommandType.Text, String.Format("select (apellido + ',' + nombres) as Usuario from usrtable where id = (select exclusive from wfdocument where task_id = {0})", taskId))
                    Return False
                End If

            End If
        End Function

        Shared Function LockTasks(TasksIds As List(Of Int64)) As Boolean

            Dim AllLocked As Boolean = True
            Try

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

                Return AllLocked
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return False
            End Try

        End Function

        Public Shared Function UnLockTask(TaskId As Int64) As Boolean
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

        ''' <summary>
        ''' Verifica si una columna es un indice o una columna de zamba
        ''' </summary>
        ''' <param name="Column"></param>
        ''' <returns></returns>
        Private Function IsIndex(Column As String) As Boolean
            If IndexsBusiness.GetIndexIdByName(Column) > 0 Then
                Return True
            End If
            Return False
        End Function

    End Class

End Namespace
