Imports System.IO

Imports Zamba.Filters
Imports Zamba.Servers
Imports Zamba.Data
Imports System.Text
Imports Zamba.Searchs



Namespace Search


    ''' -----------------------------------------------------------------------------
    ''' Project	 : Zamba.Business
    ''' Class	 : Core.ModDocuments
    ''' 
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Clase estatica para realizar busquedas de documentos en Zamba
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class ModDocuments
        Inherits ZClass

#Region "IndexSearch"
        Private Shared strselect1 As String
        Public Shared Property Sections As Object
        Public Shared Event TotalCount(ByVal Count As Int64)
        Public Shared Event DeboMaximizar()


        Public Overloads Shared Function DoSearch(ByVal search As Searchs.Search,
                                                  ByVal UserId As Int64,
                                                  ByRef FC As FiltersComponent,
                                                  ByVal LastPage As Int64,
                                                  ByVal PageSize As Int32,
                                                  Optional ByVal raiseResults As Boolean = True,
                                                  Optional ByVal isAssociatedSearch As Boolean = False,
                                                  Optional ByVal FilterType As FilterTypes = FilterTypes.Document,
                                                  Optional ByVal UseFilters As Boolean = False,
                                                  Optional ByVal visibleIndexs As List(Of Int64) = Nothing,
                                                  Optional ByVal SaveSearch As Boolean = True,
                                                  Optional ByVal fromCommandLineArgs As Boolean = False,
                                                  Optional ByVal sortChanged As Boolean = False,
                                                  Optional ByVal filtersChanged As Boolean = False,
                                                  Optional GroupByString As String = "") As DataTable

            Dim dtResults As New DataTable

            Try
                MostrarWaitForm(True, False)
                If IsNothing(FC) Then FC = New FiltersComponent()
                Dim CurrentDocType As DocType
                Dim searchSql As String = String.Empty
                Dim CountSearchSql As String = String.Empty
                search.Name = LastSearchBusiness.GetSearchName(search)

                dtResults.MinimumCapacity = 0
                Dim i As Int32
                Dim dt As DataTable
                'search.Name = LastSearchBusiness.GetSearchName(search)

                'Si es la primera vez o si llega por cambio de ordenamiento o de filtro se crean las querys de nuevo
                If search.SQL.Count >= 0 OrElse sortChanged OrElse filtersChanged Then

                    search.SQL.Clear()
                    search.SQLCount.Clear()


                    For i = 0 To search.Doctypes.Count - 1
                        CurrentDocType = search.Doctypes(i)
                        If CurrentDocType.Indexs Is Nothing OrElse CurrentDocType.Indexs.Count = 0 Then CurrentDocType.Indexs = ZCore.FilterIndex(CurrentDocType.ID)
                        If visibleIndexs Is Nothing Then

                            Dim UseIndexsRights As Boolean = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, CurrentDocType.ID)
                            If UseIndexsRights Then
                                visibleIndexs = New List(Of Int64)
                                Dim iri As Hashtable = UserBusiness.Rights.GetIndexsRights(CurrentDocType.ID, Membership.MembershipHelper.CurrentUser.ID, True, True)

                                For Each ir As IndexsRightsInfo In iri.Values
                                    If ir.GetIndexRightValue(RightsType.TaskGridIndexView) = True Then
                                        visibleIndexs.Add(ir.Indexid)
                                    End If
                                Next
                                If visibleIndexs.Count = 0 Then
                                    visibleIndexs = Nothing
                                End If
                            End If
                        End If

                        dt = SearchRows(CurrentDocType, search.Indexs, UserId, FC, LastPage, PageSize, UseFilters, FilterType, visibleIndexs, searchSql, CountSearchSql, search.OrderBy,,, search.Textsearch, False, GroupByString, search)

                        If Not dt Is Nothing Then
                            dtResults.MinimumCapacity = dtResults.MinimumCapacity + dt.MinimumCapacity
                            dtResults.Merge(dt)
                        End If

                        search.SQL.Add(searchSql)
                        search.SQLCount.Add(CountSearchSql)

                        If Not isAssociatedSearch Then
                            Try
                                Dim indexsstring As String = String.Empty
                                If search.Indexs IsNot Nothing Then
                                    For Each ind As IIndex In search.Indexs
                                        indexsstring &= ". Se filtro por el Atributo '" & ind.Name & " (" & ind.ID & ")' con el operador '" & ind.Operator & "' y valor '" & ind.DataTemp & "'"
                                    Next
                                End If
                                UserBusiness.Rights.SaveAction(2, ObjectTypes.Documents, RightsType.Buscar,
                                                               "Se realizó búsqueda por la entidad: " _
                                                               & CurrentDocType.Name & " (" & CurrentDocType.ID & ")" & indexsstring)
                            Catch ex As Exception
                                raiseerror(ex)
                            End Try
                        End If
                    Next

                    If Not isAssociatedSearch Then
                        Try
                            If dtResults.Rows.Count > 0 AndAlso SaveSearch AndAlso Not LastSearchBusiness.LastSearchAlreadyExist(search.Name) Then
                                Using lsb As New LastSearchBusiness
                                    lsb.Save(search)
                                End Using
                            End If
                        Catch ex As Exception
                            raiseerror(ex)
                        End Try

                        If raiseResults AndAlso FilterType <> FilterTypes.Task Then
                            RaiseEvent ShowResults(dtResults, search.Name, search, fromCommandLineArgs)
                        End If
                    End If

                Else
                    Dim sbSelectTotal As New StringBuilder
                    Dim sbSelect As New StringBuilder
                    Dim strPRESelect As New StringBuilder
                    Dim strPOSTSelect As New StringBuilder

                    For i = 0 To search.SQL.Count - 1
                        dt = GetTaskTable(search.SQL(i), search.OrderBy, PageSize, LastPage, search.SQLCount(i), String.Empty, search, sbSelect, sbSelectTotal, strPRESelect, strPOSTSelect)
                        If dt IsNot Nothing Then
                            dtResults.MinimumCapacity = dtResults.MinimumCapacity + dt.MinimumCapacity
                            dtResults.Merge(dt)
                        End If
                    Next

                End If

                search = Nothing
                dt = Nothing

                Return dtResults
                'End If
            Catch ex As Threading.SynchronizationLockException
                raiseerror(ex)
            Catch ex As Threading.ThreadAbortException
                raiseerror(ex)
            Catch ex As Threading.ThreadInterruptedException
                raiseerror(ex)
            Catch ex As Threading.ThreadStateException
                raiseerror(ex)
            Catch ex As NullReferenceException
                raiseerror(ex)
                RaiseInfos("Ocurrio un error en la busqueda", "Busqueda Finalizada")
            Catch ex As Exception
                RaiseInfos("No se han encontrado resultados", "Busqueda Finalizada")
            Finally
                MostrarWaitForm(False)
            End Try
            Return Nothing
        End Function

        Public Overloads Shared Sub DoSearch(ByVal DocTypeId As Int64, ByVal DOC_IDS() As String, ByVal fromCommandLineArgs As Boolean)
            Dim dtResults As New DataTable
            Dim continueSearch As Boolean = True
            Try
                MostrarWaitForm(True, False)

                'Si el origen de la llamada es por fuera de Zamba, deben validarse los permisos de visualización sobre la entidad
                If fromCommandLineArgs Then
                    Dim r As New RightsBusiness()
                    continueSearch = r.GetUserRights(ObjectTypes.DocTypes, RightsType.View, DocTypeId)
                End If

                If continueSearch Then
                    For Each doc_id As String In DOC_IDS
                        Dim drResult As DataRow = Results_Business.GetResultRow(doc_id, DocTypeId)
                        If drResult IsNot Nothing Then
                            dtResults.Merge(drResult.Table)
                        End If
                    Next
                End If

                RaiseEvent ShowResults(dtResults, String.Empty, Nothing, fromCommandLineArgs)
                RaiseEvent SearchFinished()
            Catch ex As Threading.SynchronizationLockException
            Catch ex As Threading.ThreadAbortException
            Catch ex As Threading.ThreadInterruptedException
            Catch ex As Threading.ThreadStateException
            Catch ex As Exception
                RaiseInfos("No se han encontrado resultados", "Busqueda Finalizada")
            Finally
                MostrarWaitForm(False)
                dtResults = Nothing
            End Try
        End Sub

        ''' <summary>
        ''' Ejecuta el query dado y obtiene un datatable con tareas.
        ''' </summary>
        ''' <param name="strselect">Query a ejecutar</param>
        ''' <returns>Datatable con tareas.</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTaskTable(SearchSQL As String, order As String, pageSize As Int32, lastPage As Int32, countSearchSql As String, orderType As String, Search As ISearch, ByRef strselect As StringBuilder, ByRef strselectTotal As StringBuilder, ByRef strPREselect As StringBuilder, ByRef strPOSTselect As StringBuilder) As DataTable

            Dim Desde As Int32 = (pageSize * lastPage) + 1
            Dim Hasta As Int32 = Desde + pageSize - 1

            Dim ds As DataSet
            Dim totalrows As Int64
            Dim pagingString As New StringBuilder

            Try
                If Server.isOracle Then
                    strselect.Replace("Exclusive,", "C_Exclusive,")
                    strselect.Replace(") as Q", ")")
                    strselect.Replace("[", "")
                    strselect.Replace("]", "")
                    strselectTotal.Replace("Exclusive,", "C_Exclusive,")
                    strselectTotal.Replace(") as Q", ")")
                    strselectTotal.Replace("[", "")
                    strselectTotal.Replace("]", "")
                End If

                pagingString.Append("select * FROM (")
                pagingString.Append(strPREselect.ToString)
                pagingString.Append(strselect.ToString)

                'If Server.isOracle Then
                '    If (Hasta > 0) Then
                '        If pagingString.ToString.ToLower.Contains("where") Then
                '            pagingString.Append(" and rownum >= ")
                '        Else
                '            pagingString.Append(" where rownum >= ")
                '        End If

                '        pagingString.Append(Desde)
                '        pagingString.Append(" AND ")
                '        pagingString.Append(" rownum <= ")
                '        pagingString.Append(Hasta)
                '    End If
                'End If

                pagingString.Append(") I")
                pagingString.Append(strPOSTselect.ToString)
                pagingString.Append(") x")

                'If Server.isSQLServer Then
                If (Hasta > 0) Then
                    pagingString.Append(" where rnum >= ")
                    pagingString.Append(Desde)
                    pagingString.Append(" AND ")
                    pagingString.Append(" rnum <= ")
                    pagingString.Append(Hasta)
                End If
                'End If

                '  If Server.isOracle Then
                If String.IsNullOrEmpty(order) Then
                    order = "doc_id desc"
                End If
                pagingString.Append(" Order by " & order.Trim())
                'If Not String.IsNullOrEmpty(order) Then 'order = "doc_id asc"
                '    pagingString.Append(" Order by " & order.Trim())
                'End If
                '  End If


                'Consulta de results paginada.
                'ZTrace.WriteLineIf(TraceLevel.Verbose, String.Format("Consulta: {0} Orden: {1} Desde: {2} Hasta: {3}", pagingString.ToString(), order, Desde, Hasta))
                ds = Server.Con.ExecuteDataset(CommandType.Text, pagingString.ToString())
                SearchSQL = pagingString.ToString()

                'Consulta total de filas del resultado.
                If Not lastPage > 0 Then
                    If strselectTotal IsNot Nothing AndAlso strselectTotal.Length > 0 Then
                        'ZTrace.WriteLineIf(TraceLevel.Verbose, String.Format("Consulta cantidad: {0}", strselectTotal))
                        countSearchSql = strselectTotal.ToString & strPOSTselect.ToString
                        totalrows = Server.Con.ExecuteScalar(CommandType.Text, strselectTotal.ToString)
                    Else
                        Dim queryAuxCount As String
                        queryAuxCount = pagingString.ToString().Substring(6)
                        countSearchSql = "select distinct(subQuery.totalRows) from (select count(*)over() as totalRows," + queryAuxCount + ") subQuery"
                        totalrows = Server.Con.ExecuteScalar(CommandType.Text, countSearchSql)
                    End If
                    ds.Tables(0).MinimumCapacity = totalrows
                End If

                Return ds.Tables(0)
            Catch ex As Exception
                raiseerror(ex)
            Finally
                If Not IsNothing(ds) Then
                    ds.Dispose()
                    ds = Nothing
                End If
            End Try
            Return Nothing
        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Metodo que dispara el evento "DeboMaximizar" para maximizar Zamba al  finalizar una busqueda
        ''' </summary>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Hernan]	29/05/2006	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Shared Sub Maximizar()
            RaiseEvent DeboMaximizar()
        End Sub

        Public Overloads Shared Function SearchRows(ByVal DocType As DocType,
                                                    ByVal Indexs As List(Of IIndex),
                                                    ByVal CurrentUserId As Int64,
                                                    ByVal FC As FiltersComponent,
                                                    ByVal LastPage As Int64,
                                                    ByVal PageSize As Int32,
                                                    ByVal UseFilters As Boolean,
                                                    ByVal filterType As FilterTypes,
                                                    ByVal visibleIndexs As List(Of Int64),
                                                    ByRef searchSql As String,
                                                    ByRef countSearchSql As String,
                                                    ByVal OrderBy As String,
                                                    ByVal Optional ExcludedParentDocId As Int64 = 0,
                                                    Optional ByVal NonvisibleIndexs As List(Of Int64) = Nothing,
                                                    ByVal Optional ContentSearch As String = "",
                                                    ByVal Optional GetImportantOnly As Boolean = False,
                                                    Optional ByVal GroupByString As String = "",
                                                    Optional Search As ISearch = Nothing) As DataTable

            Dim strTableI As String = Results_Factory.MakeTable(DocType.ID, Results_Factory.TableType.Indexs)
            Dim strTableT As String = Results_Factory.MakeTable(DocType.ID, Results_Factory.TableType.Document)

            Dim HasToIncludeTaskInfo, HasToIncludeLocationInfo, HasToIncludeVersionInfo As Boolean

            Dim TaskCount, VersionCount As Int64

            Dim HasTask As Object = Server.Con.ExecuteScalar(CommandType.Text, String.Format("select count(1) from wfdocument where doc_type_id = {0}", DocType.ID))

            If HasTask IsNot Nothing AndAlso Not IsDBNull(HasTask) AndAlso Int64.TryParse(HasTask, TaskCount) AndAlso TaskCount > 0 Then
                HasToIncludeTaskInfo = True
            End If

            'Dim HasVersions As Object = Server.Con.ExecuteScalar(CommandType.Text, String.Format("select count(1) from {0} where version not in (0,2)", Results_Factory.MakeTable(DocType.ID, Results_Factory.TableType.Document)))

            'If HasVersions IsNot Nothing AndAlso Not IsDBNull(HasVersions) AndAlso Int64.TryParse(HasVersions, VersionCount) AndAlso VersionCount > 0 Then
            '    HasToIncludeVersionInfo = True
            'End If


            Dim flagCase As Boolean = Boolean.Parse(ZOptBusiness.GetValueOrDefault("CaseSensitive", True))
            Dim first As Boolean = True
            Dim i As Integer
            Dim indexRestrictions As List(Of IIndex)

            Dim sbSelectTotal As New StringBuilder
            Dim sbSelect As New StringBuilder
            Dim strPRESelect As New StringBuilder
            Dim strPOSTSelect As New StringBuilder

            Dim sbValue As New StringBuilder
            Dim sbCondition As New StringBuilder
            Dim sbPostCondition As New StringBuilder

            Dim sbDate As New StringBuilder
            Dim sbRestriction As New StringBuilder
            Dim sbClose As New StringBuilder
            Dim orderType As String
            Dim slstFiltersIndexsIds As List(Of Long)

            'If String.IsNullOrEmpty(OrderBy) Then
            'OrderBy = "doc_id asc"
            'End If

            Try
                With Cache.DocTypesAndIndexs.hsRestrictionsIndexs
                    If .ContainsKey(CurrentUserId & "-" & DocType.ID) = False Then
                        .Add(CurrentUserId & "-" & DocType.ID, RestrictionsMapper_Factory.getRestrictionIndexs(CurrentUserId, DocType.ID, True))
                    End If
                    indexRestrictions = .Item(CurrentUserId & "-" & DocType.ID)
                End With

                slstFiltersIndexsIds = New List(Of Long)

                If Indexs IsNot Nothing AndAlso Indexs.Count > 0 Then
                    For i = 0 To Indexs.Count - 1
                        CreateWhereSearch(sbValue, Indexs, i, flagCase, sbCondition, first, sbDate, False)
                        slstFiltersIndexsIds.Add(Indexs(i).ID)
                    Next
                End If
                If indexRestrictions IsNot Nothing Then
                    For i = 0 To indexRestrictions.Count - 1
                        slstFiltersIndexsIds.Add(indexRestrictions(i).ID)
                        CreateWhereSearch(sbValue, indexRestrictions, i, flagCase, sbRestriction, first, sbDate, True)
                    Next
                End If

                If slstFiltersIndexsIds Is Nothing Then
                    slstFiltersIndexsIds = FC.GetSlstFiltersIndexsIDs(DocType.ID, CurrentUserId, filterType)
                Else
                    Dim ListOfFiltersIndexs As List(Of Long) = FC.GetSlstFiltersIndexsIDs(DocType.ID, CurrentUserId, filterType)
                    If ListOfFiltersIndexs IsNot Nothing Then
                        slstFiltersIndexsIds.AddRange(ListOfFiltersIndexs)
                    End If
                End If


                createSelectSearch(DocType, DocType.Indexs, visibleIndexs, False, OrderBy, NonvisibleIndexs, False, GetImportantOnly, slstFiltersIndexsIds, HasToIncludeTaskInfo, HasToIncludeLocationInfo, HasToIncludeVersionInfo, slstFiltersIndexsIds, Search, sbSelect, strPRESelect, strPOSTSelect)

                createSelectSearch(DocType, DocType.Indexs, visibleIndexs, True, OrderBy, NonvisibleIndexs, False, GetImportantOnly, slstFiltersIndexsIds, HasToIncludeTaskInfo, HasToIncludeLocationInfo, HasToIncludeVersionInfo, slstFiltersIndexsIds, Search, sbSelectTotal, strPRESelect, strPOSTSelect)



                If ContentSearch.Length > 0 Then
                    Dim sbContentSearch As New StringBuilder

                    sbContentSearch.Append(", (")
                    If Search IsNot Nothing AndAlso Search.SearchType = SearchTypes.QuickSearch Then
                        For Each EQ As IEntityEnabledForQuickSearch In Search.EntitiesEnabledForQuickSearch
                            If EQ.EntityId = DocType.ID Then
                                sbContentSearch.Append(Results_Factory.GetSearchInAllIndexsJoins(ContentSearch, DocType.ID, EQ))
                                Exit For
                            End If
                        Next
                    Else
                        sbContentSearch.Append(Results_Factory.GetSearchInAllIndexsJoins(ContentSearch, DocType.ID, Nothing))

                    End If
                    sbContentSearch.Append(") AI WHERE AI.ResultId = ")
                    sbContentSearch.Append("I.DOC_ID")
                    sbSelect.Append(sbContentSearch.ToString())
                    sbSelectTotal.Append(sbContentSearch.ToString())
                End If


                'Versiones
                'If Boolean.Parse(UserPreferences.getValue("UseVersion", Sections.UserPreferences, False)) Then
                '    If HasToIncludeVersionInfo Then
                '        If sbCondition.Length > 0 Then
                '            'sbCondition.Remove(sbCondition.Length - 1, 1)
                '            sbCondition.Append(" AND Version in (0,2) ")
                '        Else
                '            sbCondition.Append(" Version in (0,2) ")
                '        End If
                '    End If
                'End If

                'Si tiene doc_id no lo traigo ya que es el mismo documento
                If ExcludedParentDocId > 0 Then
                    If sbCondition.Length > 0 Then
                        sbCondition.Append(" AND ")
                        sbCondition.Append("T.DOC_ID <> ")
                        sbCondition.Append(ExcludedParentDocId)
                    Else
                        sbCondition.Append("T.DOC_ID <>")
                        sbCondition.Append(ExcludedParentDocId)
                    End If
                End If

                If Not InsertFilterRestrictionConditionString(DocType, sbCondition, sbPostCondition, sbClose, CurrentUserId, FC, sbSelect, sbDate, UseFilters, filterType, sbRestriction) Then
                    Return New DataTable
                    'Else
                    '    sbSelect = ReplaceTableName(strTable, sbSelect)
                End If

                If Not IsNothing(sbSelectTotal) Then
                    If InsertFilterRestrictionConditionString(DocType, sbCondition, sbPostCondition, sbClose, CurrentUserId, FC, sbSelectTotal, sbDate, UseFilters, filterType, sbRestriction) = False Then
                        Return New DataTable
                        'Else
                        '    sbSelectTotal = ReplaceTableName(strTable, sbSelectTotal)
                    End If
                End If

                Dim MainJoin As String = String.Format("{0} T inner join {1} I on T.doc_id = I.doc_id", strTableT, strTableI)

                Dim dt As New DataTable
                sbSelect = ReplaceTableName(strTableT, strTableI, sbSelect)
                sbSelectTotal = ReplaceTableName(strTableT, strTableI, sbSelectTotal)
                dt = GetTaskTable(searchSql, OrderBy, PageSize, LastPage, countSearchSql, String.Empty, Nothing, sbSelect, sbSelectTotal, strPRESelect, strPOSTSelect)

                Return dt

            Catch ex As Exception
                raiseerror(ex)

            Finally
                indexRestrictions = Nothing
                If sbValue IsNot Nothing Then
                    sbValue.Clear()
                    sbValue = Nothing
                End If
                If sbCondition IsNot Nothing Then
                    sbCondition.Clear()
                    sbCondition = Nothing
                End If
                If sbSelect IsNot Nothing Then
                    sbSelect.Clear()
                    sbSelect = Nothing
                End If
                If sbDate IsNot Nothing Then
                    sbDate.Clear()
                    sbDate = Nothing
                End If
                If sbRestriction IsNot Nothing Then
                    sbRestriction.Clear()
                    sbRestriction = Nothing
                End If
                If sbClose IsNot Nothing Then
                    sbClose.Clear()
                    sbClose = Nothing
                End If
            End Try

            Return Nothing
        End Function

        ''' <summary>
        ''' Verifica si una columna es un indice o una columna de zamba
        ''' </summary>
        ''' <param name="Column"></param>
        ''' <returns></returns>
        Private Shared Function IsIndex(Column As String) As Boolean
            If IndexsBusiness.GetIndexIdByName(Column) > 0 Then
                Return True
            End If
            Return False
        End Function

        Private Shared Function ReplaceTableName(strTableT As String, strTableI As String, query As StringBuilder) As StringBuilder
            If query.ToString.Contains("strTableT") Then
                query = query.Replace("strTableT", strTableT)
            End If
            If query.ToString.Contains("strTableI") Then
                query = query.Replace("strTableI", strTableI)
            End If
            Return query
        End Function

        Public Shared Function FillOrderBy(ByVal IndexsOrderString As StringBuilder, ByVal OrderByZambaColumns As Dictionary(Of String, String)) As StringBuilder

            Dim OrderString As New StringBuilder()

            'Agrega ordenamiento por columnas de zamba
            If OrderByZambaColumns IsNot Nothing Then

                Dim enumOrderSortsIndex As Int32 = 0

                For Each column As String In OrderByZambaColumns.Keys

                    enumOrderSortsIndex = OrderByZambaColumns(column)

                    If OrderString.Length > 0 Then
                        OrderString.Append(", ")
                    End If

                    If enumOrderSortsIndex > 0 Then
                        OrderString.Append(column)
                        OrderString.Append(" ")
                        OrderString.Append([Enum].GetName(GetType(OrderSorts), enumOrderSortsIndex))
                    End If
                Next
            End If

            'Agrega ordenamiento por columnas que son indexs
            If Not String.IsNullOrEmpty(IndexsOrderString.ToString()) Then
                If OrderString.Length > 0 Then
                    OrderString.Append(", ")
                End If
                OrderString.Append(IndexsOrderString.ToString())
            End If

            'Si no hay ninguna condicion para ordenar, ordeno por doc_id
            If String.IsNullOrEmpty(OrderString.ToString()) Then
                OrderString.Append(" doc_id asc ")
            End If

            Return OrderString
        End Function



        Private Shared Function InsertFilterRestrictionConditionString(ByVal DocType As DocType,
                                                                       ByVal ColumnCondstring As StringBuilder,
                                                                       ByVal sbPostCondition As StringBuilder,
                                                                       ByVal Orderstring As StringBuilder,
                                                                       ByVal CurrentUserId As Long,
                                                                       ByVal FC As FiltersComponent,
                                                                       ByRef strselect As StringBuilder,
                                                                       ByRef dateDeclarationString As StringBuilder,
                                                                       ByVal UseFilters As Boolean,
                                                                       ByVal FilterType As FilterTypes,
                                                                       ByVal RestrictionString As StringBuilder,
                                                                       ByRef Optional slstIndexsIds As List(Of Long) = Nothing) As Boolean
            If CurrentUserId > 0 Then

                Dim FilterString As String = String.Empty
                Dim Filters As New List(Of IFilterElem)

                If UseFilters Then
                    'Se verifican los permisos de quitar y deshabilitar los filtros por defecto del usuario
                    Dim removeDefaultFilters As Boolean = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.RemoveDefaultFilters, DocType.ID)
                    Dim disableDefaultFilters As Boolean = UserBusiness.Rights.GetUserRights(Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.DisableDefaultFilters, DocType.ID)

                    'Si es documental o si es tarea y no tiene ninguno de los dos se procede a cargarlos
                    If FilterType <> FilterTypes.Task OrElse
                        (FilterType = FilterTypes.Task AndAlso Not removeDefaultFilters AndAlso Not disableDefaultFilters) Then

                        Filters = FC.GetLastUsedFilters(DocType.ID, CurrentUserId, FilterType)
                        'For Each f As IFilterElem In Filters
                        '    If f.Enabled = True Then ' AndAlso strselect.ToString().ToLower().Contains(f.Filter().ToLower()) = False Then
                        '        Return False
                        '    End If
                        'Next

                        Dim isCaseSensitive As Boolean = ZOptBusiness.GetValueOrDefault("CaseSensitive", True)
                        'Se cargan unicamente los filtros por defecto, los manuales no son mostrados
                        If FilterType = FilterTypes.Task Then
                            FilterString = FC.GetFiltersString(Filters, True, isCaseSensitive, slstIndexsIds)
                        Else
                            FilterString = FC.GetFiltersString(Filters, False, isCaseSensitive, slstIndexsIds)
                        End If
                    End If
                End If

                If RestrictionString.Length > 0 Then
                    If strselect.ToString.ToLower().Contains("where") = False OrElse (strselect.ToString.ToLower().Contains("ai.resultid =") AndAlso strselect.ToString.ToLower().EndsWith("doc_id")) Then strselect.Append(" WHERE ")
                    strselect.Append(RestrictionString.ToString)
                End If

                If ColumnCondstring.Length > 0 Then
                    If Not strselect.ToString.ToLower().Contains("where") Then
                        strselect.Append(" WHERE ")
                    Else
                        strselect.Append(" AND ")
                    End If
                    strselect.Append(ColumnCondstring.ToString)
                End If

                If FilterString.Length > 0 Then
                    If Not strselect.ToString.ToLower().Contains("where") Then
                        strselect.Append(" WHERE ")
                    Else
                        strselect.Append(" AND ")
                    End If

                    If Server.isOracle Then
                        FilterString = FilterString.Replace("strTable", "I")
                    End If

                    strselect.Append(FilterString.ToString)
                End If

                If dateDeclarationString.Length > 0 Then
                    'Si existe declaracion de variables de fecha las inserta al inicio del select
                    strselect.Insert(0, dateDeclarationString)
                End If

                strselect.Append(" " & Orderstring.ToString)

            End If

            Return True
        End Function

        ''' <summary>
        ''' Arma la fila
        ''' </summary>
        ''' <param name="First"></param>
        ''' <param name="ArraySustIndex"></param>
        ''' <param name="dr"></param>
        ''' <param name="dt"></param>
        ''' <param name="indexs"></param>
        ''' <remarks></remarks>
        Private Shared Sub CreateRow(ByRef ArraySustIndex As ArrayList, ByRef dr As DataRow, ByVal indexs As List(Of IIndex), Optional ByVal IsFolDerSearch As Boolean = False)
            Dim intCounter As Int32 = 0

            If IsFolDerSearch Then dr("nombre del documento") = "(Carpeta) - " & dr("nombre del documento")
            For Each indice As IIndex In indexs
                If indice.DropDown = IndexAdditionalType.AutoSustitución Or indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                    ArraySustIndex.Add(indice)

                    If Not dr.Table.Columns.Contains(indice.Name) Then
                        dr.Table.Columns.Add(indice.Name)
                    End If

                    If Not IsDBNull(dr.Item("I" & indice.ID)) Then
                        dr.Item(indice.Name) = AutoSubstitutionBusiness.getDescription(dr.Item("I" & indice.ID), indice.ID, False, indice.Type)
                    Else
                        dr.Item(indice.Name) = String.Empty
                    End If
                End If
            Next
        End Sub

        ''Castea el tipo de un Atributo a Type
        Private Shared Function GetIndexType(ByVal iType As IndexDataType) As Type
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
        ''' Armado del where que se utiliza para las busquedas
        ''' </summary>
        ''' <param name="valueString"></param>
        ''' <param name="Indexs"></param>
        ''' <param name="i"></param>
        ''' <param name="ignoreCase"></param>
        ''' <param name="ColumCondstring"></param>
        ''' <param name="First">Bandera que indica si es la 1era vez</param>
        ''' <history>   Marcelo    Created     20/08/09
        '''             Javier     Modified    12/10/10 Corrobora si es filtro usa el nombre de la campo para armar la condicion
        ''' </history>
        ''' <remarks></remarks>
        Public Shared Sub CreateWhereSearch(ByRef valueString As StringBuilder, ByVal Indexs As List(Of IIndex), ByRef i As Int64, ByRef ignoreCase As Boolean, ByRef ColumCondstring As StringBuilder, ByRef First As Boolean, ByRef dateDeclarationString As StringBuilder, ByVal IsRestriction As Boolean)

            Dim Valuestring1 As Object = Nothing
            Dim Valuestring3 As Object = Nothing
            Dim dateString As New StringBuilder
            valueString.Remove(0, valueString.Length)
            valueString.Append(Indexs(i).Data)
            Dim IndexColumnName As String = Indexs(i).Column
            Dim IndexName As String = Indexs(i).Name
            Dim needLower As Boolean = ignoreCase

            If valueString.Length <> 0 OrElse Indexs(i).[Operator].ToLower = "es nulo" Then
                If Indexs(i).[Operator] <> "SQL" Then
                    If valueString.ToString.Split(";").Length > 1 Then
                        If Not IsRestriction Then
                            If Indexs(i).Type = IndexDataType.Alfanumerico OrElse Indexs(i).Type = IndexDataType.Alfanumerico_Largo Then
                                Valuestring1 = "'" & valueString.Replace(";", "';'").ToString() & "'"
                                If (IsNumeric(Results_Business.ReplaceChar(valueString.Replace(";", "';'").ToString(), String.Empty)) = False) Then
                                    needLower = True
                                End If
                            End If
                        Else
                            If Indexs(i).Type = IndexDataType.Alfanumerico OrElse Indexs(i).Type = IndexDataType.Alfanumerico_Largo Then
                                If Not valueString.ToString().StartsWith("'") AndAlso Not valueString.ToString().EndsWith("'") Then
                                    Valuestring1 = "'" & valueString.ToString() & "'"
                                Else
                                    Valuestring1 = valueString.ToString()
                                End If
                            Else
                                Valuestring1 = valueString.ToString()
                            End If
                            needLower = False
                        End If
                    Else
                        Select Case Indexs(i).Type
                            Case IndexDataType.Numerico, IndexDataType.Numerico_Largo

                                Valuestring1 = valueString.ToString()

                                If Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                    If (IsNumeric(Results_Business.ReplaceChar(valueString.ToString(), String.Empty)) = False) Then
                                        needLower = True
                                    End If
                                Else
                                    needLower = False
                                End If

                            Case IndexDataType.Numerico_Decimales, IndexDataType.Moneda
                                If valueString.Length <> 0 Then
                                    If Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString = "," Then
                                        valueString = valueString.Replace(".", ",")
                                    End If
                                    Valuestring1 = CDec(valueString.ToString)
                                    Valuestring1 = Valuestring1.ToString.Replace(",", ".")
                                End If
                                needLower = False
                            Case IndexDataType.Si_No
                                If valueString.Length <> 0 Then
                                    Valuestring1 = Int64.Parse(valueString.ToString)
                                End If
                                needLower = False
                            Case IndexDataType.Fecha
                                If Not String.IsNullOrEmpty(valueString.ToString.Trim) Then
                                    Valuestring1 = Server.Con.ConvertDate(valueString.ToString)
                                End If
                                needLower = False
                            Case IndexDataType.Fecha_Hora
                                If Not String.IsNullOrEmpty(valueString.ToString.Trim) Then
                                    Valuestring1 = Server.Con.ConvertDateTime(valueString.ToString)
                                End If
                                needLower = False
                            Case IndexDataType.Alfanumerico, IndexDataType.Alfanumerico_Largo
                                If Not IsRestriction Then
                                    Valuestring1 = "'" & valueString.ToString & "'"
                                    If (IsNumeric(Results_Business.ReplaceChar(valueString.ToString(), String.Empty)) = False) Then

                                        needLower = True
                                    End If
                                Else
                                    If Not valueString.ToString().StartsWith("'") AndAlso Not valueString.ToString().EndsWith("'") Then
                                        Valuestring1 = "'" & valueString.ToString() & "'"
                                    Else
                                        Valuestring1 = valueString.ToString()
                                    End If
                                    needLower = False
                                End If
                        End Select
                    End If
                Else
                    Valuestring1 = Indexs(i).Data
                    needLower = False
                End If

                Dim Op As String = Valuestring1.ToString
                Op = Op.Replace("''", "'").Replace(")'", ")")
                Valuestring1 = Op
                Op = String.Empty

                Dim separator As String = " AND "
                If ColumCondstring.Length > 0 Then
                    ColumCondstring.Append(separator)
                End If

                Select Case Indexs(i).[Operator]
                    Case "="
                        Op = " = "

                        ColumCondstring.Append(" (")
                        If Trim(Indexs(i).dataDescription).Length = 0 OrElse IsNumeric(Trim(Indexs(i).dataDescription)) OrElse IsNumeric(Trim(Indexs(i).Data)) Then
                            ColumCondstring.Append(IndexColumnName & Op & Trim(Valuestring1))
                            ColumCondstring.Append(" )")
                        Else
                            ColumCondstring.Append(If(ignoreCase AndAlso needLower, "lower(", ""))
                            ColumCondstring.Append(If(Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico,
                                                   "slst_s" & Indexs(i).ID & ".descripcion",
                                                   IndexColumnName))
                            ColumCondstring.Append(If(ignoreCase AndAlso needLower, ")", ""))
                            ColumCondstring.Append(Op)
                            ColumCondstring.Append(If(ignoreCase AndAlso needLower, "lower(", ""))
                            'ColumCondstring.Append("'")
                            ColumCondstring.Append(If(Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico,
                                                   "'" & Trim(Indexs(i).dataDescription) & "'",
                                                   Trim(Valuestring1)))
                            'ColumCondstring.Append("'")
                            ColumCondstring.Append(If(ignoreCase AndAlso needLower, ")", ""))

                            ColumCondstring.Append(" or ")
                            ColumCondstring.Append(IndexColumnName & Op & Trim(Valuestring1))
                            ColumCondstring.Append(")")
                        End If

                        'If ignoreCase AndAlso (Indexs(i).Type = IndexDataType.Alfanumerico OrElse Indexs(i).Type = IndexDataType.Alfanumerico_Largo) Then
                        '    ColumCondstring.Append(" (lower(" & IndexColumnName & ") " & Op & " " & LCase(Valuestring1) & ")")
                        'ElseIf Indexs(i)("DROPDOWN") = IndexAdditionalType.AutoSustitución OrElse Indexs(i)("DROPDOWN") = IndexAdditionalType.AutoSustituciónJerarquico Then
                        '    ColumCondstring.Append(" (")
                        '    If IsNumeric(Trim(Indexs(i).dataDescription)) Then
                        '        ColumCondstring.Append(IndexColumnName & " = " & Trim(Valuestring1) & ") ")
                        '    Else
                        '        ColumCondstring.Append("slst_s" & Indexs(i).ID & ".descripcion")
                        '        ColumCondstring.Append(" = '" & Trim(Indexs(i).dataDescription) & "')")
                        '    End If
                        'Else
                        '    ColumCondstring.Append(" (" & IndexColumnName & " " & Op & " " & Valuestring1 & ")")
                        'End If


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
                            'cambio las a como indice a I ya que todos los atributos vienen con dato algunos vacio otros no.
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
                                            Valuestring3 = Server.Con.ConvertDate(Indexs(i).Data2)
                                        End If
                                        Data2Added = True
                                    End If
                                Case 5
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        If Not String.IsNullOrEmpty(valueString.ToString.Trim) Then
                                            Valuestring3 = Server.Con.ConvertDateTime(Indexs(i).Data2)
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
                                        ignoreCase = True
                                        Valuestring3 = "'" & LCase(Indexs(i).Data2) & "'"
                                        Data2Added = True
                                    End If
                            End Select
                        Catch ex As Exception
                            Throw New Exception("Ocurrio un error al convertir al tipo de Dato: Dato: " & valueString.ToString & ", Tipo Dato: " & Indexs(i).Type & " " & ex.ToString)
                        End Try

                        If Data2Added Then
                            If Server.isSQLServer And (Indexs(i).Type = IndexDataType.Fecha OrElse Indexs(i).Type = IndexDataType.Fecha_Hora) Then
                                ColumCondstring.Append(" (" & IndexColumnName & " BETWEEN " & Valuestring1 & " and " & Valuestring3 & ")")
                            Else
                                ColumCondstring.Append(" (" & IndexColumnName & " >= " & Valuestring1 & " and " & IndexColumnName & " <= " & Valuestring3 & ")")
                            End If
                            Data2Added = False
                        End If
                    Case "Contiene"
                        While Valuestring1.Contains("  ")
                            Valuestring1 = Valuestring1.Replace("  ", " ")
                        End While
                        Valuestring1 = Valuestring1.Trim.Replace(" ", "%")

                        If (Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse (Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico)) Then
                            ColumCondstring.Append(" (")
                            If ignoreCase AndAlso needLower Then ColumCondstring.Append("lower(")
                            ColumCondstring.Append("slst_s" & Indexs(i).ID & ".descripcion")
                            If ignoreCase AndAlso needLower Then ColumCondstring.Append(")")
                            ColumCondstring.Append(" LIKE ")
                            If ignoreCase AndAlso needLower Then ColumCondstring.Append("lower(")
                            ColumCondstring.Append("'%")
                            ColumCondstring.Append(Replace(Replace(Replace(Trim(Indexs(i).dataDescription), "'", String.Empty), "  ", " "), " ", "%"))
                            ColumCondstring.Append("%'")
                            If ignoreCase AndAlso needLower Then ColumCondstring.Append(")")
                            ColumCondstring.Append(" OR (")
                            ColumCondstring.Append(IndexColumnName)
                            ColumCondstring.Append(" Like ")
                            ColumCondstring.Append("'%")
                            ColumCondstring.Append(Replace(Replace(Replace(Trim(Valuestring1), "'", String.Empty), "  ", " "), " ", "%") & "%') ")
                            ColumCondstring.Append(") ")
                        Else
                            ColumCondstring.Append(" (")
                            If ignoreCase AndAlso needLower Then ColumCondstring.Append("lower(")
                            ColumCondstring.Append(IndexColumnName)
                            If ignoreCase AndAlso needLower Then ColumCondstring.Append(")")
                            ColumCondstring.Append(" LIKE ")
                            If ignoreCase AndAlso needLower Then ColumCondstring.Append("lower(")
                            ColumCondstring.Append("'%")
                            ColumCondstring.Append(Replace(Replace(Replace(Trim(Valuestring1), "'", String.Empty), "  ", " "), " ", "%"))
                            ColumCondstring.Append("%'")
                            If ignoreCase AndAlso needLower Then ColumCondstring.Append(")")
                            ColumCondstring.Append(") ")

                            'If ignoreCase Then
                            '    ColumCondstring.Append(" (lower(" & IndexColumnName & ") Like '%" & Replace(Replace(Replace(Trim(Valuestring1), "'", String.Empty), "  ", " "), " ", "%") & "%')")
                            'Else
                            '    ColumCondstring.Append(" (" & IndexColumnName & " Like '%" & Replace(Replace(Replace(Trim(Valuestring1), "'", String.Empty), "  ", " "), " ", "%") & "%')")
                            'End If

                        End If

                    Case "Empieza"

                        If Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                            ColumCondstring.Append(" (")
                            If ignoreCase Then ColumCondstring.Append("lower(")
                            ColumCondstring.Append("slst_s" & Indexs(i).ID & ".descripcion")
                            If ignoreCase Then ColumCondstring.Append(")")
                            ColumCondstring.Append(" Like '" & Replace(Replace(Replace(Trim(Indexs(i).dataDescription), "'", String.Empty), "  ", " "), " ", "%") & "%') OR")
                        End If

                        If ignoreCase AndAlso (IsNumeric(Results_Business.ReplaceChar(Valuestring1, String.Empty)) = False) Then

                            ColumCondstring.Append(" (lower(" & IndexColumnName & ") Like '" & Replace(Trim(Valuestring1), "'", String.Empty) & "%')")
                        Else
                            ColumCondstring.Append(" (" & IndexColumnName & " Like '" & Replace(Trim(Valuestring1), "'", String.Empty) & "%')")
                        End If

                    Case "Termina"

                        If Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                            ColumCondstring.Append(" (")
                            If ignoreCase Then ColumCondstring.Append("lower(")
                            ColumCondstring.Append("slst_s" & Indexs(i).ID & ".descripcion")
                            If ignoreCase Then ColumCondstring.Append(")")
                            ColumCondstring.Append(" Like '%" & Replace(Replace(Replace(Trim(Indexs(i).dataDescription), "'", String.Empty), "  ", " "), " ", "") & "') OR")
                        End If

                        If ignoreCase AndAlso (IsNumeric(Results_Business.ReplaceChar(Valuestring1, String.Empty)) = False) Then
                            ColumCondstring.Append(" (lower(" & IndexColumnName & ") Like '%" & Replace(Trim(Valuestring1), "'", String.Empty) & "')")
                        Else
                            ColumCondstring.Append(" (" & IndexColumnName & " Like '%" & Replace(Trim(Valuestring1), "'", String.Empty) & "')")
                        End If

                    Case "Alguno"
                        Op = " LIKE "
                        Valuestring1 = Valuestring1.Replace(";", ",")
                        Valuestring1 = Valuestring1.Replace("  ", " ")
                        Valuestring1 = Valuestring1.Replace(" ", ",")
                        Dim valuesToFilter As Array = DirectCast(Valuestring1, String).Split(",")
                        Dim valueIndex As Int32
                        Dim currentValueToFilter As String
                        Dim tempWhereString As New StringBuilder()

                        For valueIndex = 0 To valuesToFilter.Length - 1
                            currentValueToFilter = valuesToFilter(valueIndex)
                            If Not IsNothing(currentValueToFilter) AndAlso Not String.IsNullOrEmpty(currentValueToFilter.Trim) Then

                                If Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then

                                    If valueIndex > 0 Then
                                        separator = If(String.IsNullOrEmpty(tempWhereString.ToString), " AND ", " OR ")
                                        tempWhereString.Append(separator)
                                    End If

                                    If (i = 0 AndAlso valueIndex = 0) OrElse valueIndex > 0 Then
                                        If (IsNumeric(Results_Business.ReplaceChar(currentValueToFilter, String.Empty)) = False) Then
                                            tempWhereString.Append(If(ignoreCase, "lower(", ""))
                                            tempWhereString.Append("slst_s" & Indexs(i).ID & ".descripcion")
                                            tempWhereString.Append(If(ignoreCase, ") ", " "))
                                            tempWhereString.Append(Op)
                                            tempWhereString.Append(If(ignoreCase, "lower(", ""))
                                            tempWhereString.Append("'%")
                                            tempWhereString.Append(currentValueToFilter.Replace("'", String.Empty).Trim)
                                            tempWhereString.Append("%'")
                                            tempWhereString.Append(If(ignoreCase, ") ", " "))
                                        Else
                                            tempWhereString.Append("slst_s" & Indexs(i).ID & ".descripcion")
                                            tempWhereString.Append(Op)
                                            tempWhereString.Append("'%")
                                            tempWhereString.Append(currentValueToFilter.Replace("'", String.Empty).Trim)
                                            tempWhereString.Append("%'")
                                        End If

                                        If valueIndex = 0 Then
                                            tempWhereString.Append(" OR ")
                                        End If
                                    End If

                                End If

                                If valueIndex > 0 Then
                                    separator = If(String.IsNullOrEmpty(tempWhereString.ToString), " AND ", " OR ")
                                    tempWhereString.Append(separator)
                                End If

                                If (i = 0 AndAlso valueIndex = 0) OrElse valueIndex > 0 Then
                                    If (IsNumeric(Results_Business.ReplaceChar(currentValueToFilter, String.Empty)) = False) Then
                                        tempWhereString.Append(If(ignoreCase, "lower(", ""))
                                        tempWhereString.Append(IndexColumnName)
                                        tempWhereString.Append(If(ignoreCase, ") ", " "))
                                        tempWhereString.Append(Op)
                                        tempWhereString.Append(If(ignoreCase, "lower(", ""))
                                        tempWhereString.Append(" '%")
                                        tempWhereString.Append(currentValueToFilter.Replace("'", String.Empty).Trim)
                                        tempWhereString.Append("%'")
                                        tempWhereString.Append(If(ignoreCase, ") ", " "))
                                    Else
                                        tempWhereString.Append(IndexColumnName)
                                        tempWhereString.Append(Op)
                                        tempWhereString.Append(" '%")
                                        tempWhereString.Append(currentValueToFilter.Replace("'", String.Empty).Trim)
                                        tempWhereString.Append("%'")
                                    End If

                                End If

                            End If
                        Next
                        If Not String.IsNullOrEmpty(tempWhereString.ToString) Then
                            ColumCondstring.Append("(").Append(tempWhereString.ToString).Append(")")
                        End If
                        valuesToFilter = Nothing
                        tempWhereString = Nothing
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
                            If Not IsNothing(Val) AndAlso Not String.IsNullOrEmpty(Val.Trim) Then
                                If x = 0 Then
                                    If Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        If ignoreCase AndAlso (IsNumeric(Results_Business.ReplaceChar(Indexs(i).dataDescription.Trim().Replace("'", String.Empty), String.Empty)) = False) Then
                                            somestring = " (lower( slst_s" & Indexs(i).ID & ".descripcion) " & Op & " '%" & Indexs(i).dataDescription.Trim().Replace("'", String.Empty) & "%'"
                                            somestring &= " OR lower(" & IndexColumnName & ") " & Op & " '%" & Val.Replace("'", String.Empty) & "%'"
                                        Else
                                            somestring = " (slst_s" & Indexs(i).ID & ".descripcion " & Op & " '%" & Indexs(i).dataDescription.Trim().Replace("'", String.Empty) & "%'"
                                            somestring &= " OR " & IndexColumnName & " " & Op & " '%" & Val.Replace("'", String.Empty) & "%'"
                                        End If

                                    Else
                                        If ignoreCase = True AndAlso (IsNumeric(Results_Business.ReplaceChar(Val.Replace("'", String.Empty), String.Empty)) = False) Then
                                            somestring = " (lower(" & IndexColumnName & ") " & Op & " '%" & Val.Replace("'", String.Empty) & "%'"
                                        Else
                                            somestring = " (" & IndexColumnName & " " & Op & " '%" & Val.Replace("'", String.Empty) & "%'"
                                        End If
                                    End If
                                Else
                                    If String.IsNullOrEmpty(somestring) Then separator = " OR "
                                    If Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                        If ignoreCase AndAlso (IsNumeric(Results_Business.ReplaceChar(Indexs(i).dataDescription, String.Empty)) = False) Then
                                            somestring &= separator & " lower(slst_s" & IndexColumnName & ".descripcion) " & Op & " '%" & Replace(Indexs(i).dataDescription, "'", String.Empty).Trim & "%'"
                                            somestring &= " OR lower(" & IndexColumnName & ") " & Op & " '%" & Replace(Val, "'", String.Empty).Trim & "%'"
                                        Else
                                            somestring &= separator & " slst_s" & IndexColumnName & ".descripcion " & Op & " '%" & Replace(Indexs(i).dataDescription, "'", String.Empty).Trim & "%'"
                                            somestring &= " OR " & IndexColumnName & " " & Op & " '%" & Replace(Val, "'", String.Empty).Trim & "%'"
                                        End If
                                    Else
                                        If ignoreCase = True AndAlso (IsNumeric(Results_Business.ReplaceChar(Replace(Val, "'", String.Empty).Trim, String.Empty)) = False) Then
                                            somestring &= separator & " lower(" & IndexColumnName & ") " & Op & " '%" & Replace(Val, "'", String.Empty).Trim & "%'"
                                        Else
                                            somestring &= separator & " " & IndexColumnName & " " & Op & " '%" & Replace(Val, "'", String.Empty).Trim & "%'"
                                        End If
                                    End If
                                End If

                            End If
                        Next
                        If Not String.IsNullOrEmpty(somestring) Then ColumCondstring.Append(somestring & ")")
                        SomeValues = Nothing
                        somestring = Nothing
                    Case "Dentro"
                        If ignoreCase = True AndAlso (IsNumeric(Results_Business.ReplaceChar(Valuestring1, String.Empty)) = False) Then
                            ColumCondstring.Append(" (lower(" & IndexColumnName & ") in (" & Valuestring1 & "'))")
                        Else
                            ColumCondstring.Append(" (" & IndexColumnName & " in (" & Valuestring1 & "))")
                        End If

                    Case "SQL Sin Atributo"
                        ColumCondstring.Append(" (" & Valuestring1 & ")")
                    Case "SQL"
                        ColumCondstring.Append(" (" & IndexColumnName & " in (" & Valuestring1 & "))")

                End Select
                Op = Nothing
                separator = Nothing
            End If
        End Sub



        Private Shared Sub createSelectSearch(DocType As IDocType,
                                                   ByVal Indexs As List(Of IIndex),
                                                   ByVal VisibleIndexs As List(Of Int64),
                                                   ByVal GetCount As Boolean,
                                                   ByVal order As String,
                                                   ByVal NonvisibleIndexs As List(Of Int64),
                                                   ByVal fromSearchVersions As Boolean, ByVal GetImportantOnly As Boolean, ByVal slstFiltersIndexsIds As List(Of Long), ByVal HasToIncludeTaskInfo As Boolean, ByVal HasToIncludeLocationInfo As Boolean, ByVal HasToIncludeVersionInfo As Boolean, ByVal restrictionIndexs As List(Of Int64), Search As ISearch, ByRef strselect As StringBuilder, ByRef strPREselect As StringBuilder, ByRef strPOSTselect As StringBuilder)

            Dim strTableI As String
            Dim strTableT As String

            If Search.SearchType <> SearchTypes.AsignedTasks Then
                strTableI = Results_Factory.MakeTable(DocType.ID, Results_Factory.TableType.Indexs)
                strTableT = Results_Factory.MakeTable(DocType.ID, Results_Factory.TableType.Document)
            End If

            Dim strNolock As String = " WITH (nolock)"
            Dim auIndex As New List(Of Int64)
            If slstFiltersIndexsIds Is Nothing Then
                slstFiltersIndexsIds = New List(Of Long)
            End If

            If GetCount Then
                strselect.Append("Select count(1) ")

                For Each _Index As Index In Indexs
                    If (_Index.DropDown = IndexAdditionalType.AutoSustitución _
                          Or _Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico) AndAlso (restrictionIndexs Is Nothing OrElse restrictionIndexs.Contains(_Index.ID)) Then
                        auIndex.Add(_Index.ID)
                    End If
                Next
            Else

                strPREselect.Append(" SELECT I.* ")

                'If HasToIncludeTaskInfo Then
                '    strPREselect.Append(", ")
                '    strPREselect.Append(GridColumns.TASK_ID_COLUMNNAME)
                'End If

                strPREselect.Append(", " & DocType.ID)
                strPREselect.Append(" as DOC_TYPE_ID")

                strPREselect.Append(", '" & DocType.Name & "' as ")
                strPREselect.Append(Chr(34))
                strPREselect.Append(GridColumns.DOC_TYPE_NAME_COLUMNNAME)
                strPREselect.Append(Chr(34))

                If HasToIncludeTaskInfo Then
                    'strPREselect.Append(",")
                    'strPREselect.Append(Chr(34))
                    'strPREselect.Append(GridColumns.WORKFLOW_COLUMNAME)
                    'strPREselect.Append(Chr(34))

                    'strPREselect.Append(",")
                    'strPREselect.Append(Chr(34))
                    'strPREselect.Append(GridColumns.ETAPA_COLUMNAME)
                    'strPREselect.Append(Chr(34))
                    'strPREselect.Append(",")
                    'strPREselect.Append(Chr(34))
                    'strPREselect.Append(GridColumns.STATE_COLUMNNAME)
                    'strPREselect.Append(Chr(34))

                    'strPREselect.Append(",")
                    'strPREselect.Append(Chr(34))
                    'strPREselect.Append(GridColumns.SITUACION_COLUMNNAME)
                    'strPREselect.Append(Chr(34))

                    'strPREselect.Append(",")
                    'strPREselect.Append(Chr(34))
                    'strPREselect.Append(GridColumns.ASIGNADO_COLUMNNAME)
                    'strPREselect.Append(Chr(34))
                End If

                If HasToIncludeLocationInfo Then
                    strPREselect.Append(", ")
                    strPREselect.Append("DISK_GROUP_ID")
                End If


                If HasToIncludeLocationInfo Then
                    strPREselect.Append(", VOL_ID, DOC_FILE, OFFSET")
                End If


                If HasToIncludeVersionInfo Then
                    strPREselect.Append(", ver_Parent_id, RootId ")
                End If


                If HasToIncludeVersionInfo Then
                    strPREselect.Append(",")
                    strPREselect.Append(Chr(34))
                    strPREselect.Append(GridColumns.VERSION_COLUMNNAME)
                    strPREselect.Append(Chr(34))
                    strPREselect.Append(",")
                    strPREselect.Append(Chr(34))
                    strPREselect.Append(GridColumns.NUMERO_DE_VERSION_COLUMNNAME)
                    strPREselect.Append(Chr(34))
                End If

                If HasToIncludeLocationInfo Then
                    strPREselect.Append(",disk_Vol_id, DISK_VOL_PATH")
                End If

                'strPREselect.Append(",")
                'strPREselect.Append(Chr(34))
                'strPREselect.Append(GridColumns.CRDATE_COLUMNNAME)
                'strPREselect.Append(Chr(34))
                'strPREselect.Append(", ")
                'strPREselect.Append(Chr(34))
                'strPREselect.Append(GridColumns.LASTUPDATE_COLUMNNAME)
                'strPREselect.Append(Chr(34))


                strselect.Append("SELECT ")

                strselect.Append("I.DOC_ID ")
                If HasToIncludeTaskInfo Then
                    strselect.Append(", ")
                    strselect.Append(GridColumns.TASK_ID_COLUMNNAME)
                End If

                'strPREselect.Append(", ")
                'strPREselect.Append("T.DOC_TYPE_ID ")

                If HasToIncludeTaskInfo Then
                    strselect.Append(",wd.step_Id, ")
                    strselect.Append("Do_State_ID, ")
                    strselect.Append("wd.work_id, ")
                    strselect.Append("wd.Task_State_ID, ")
                    strselect.Append("user_asigned ")
                End If

                strPREselect.Append(", ")
                strPREselect.Append("T.NAME As ")
                strPREselect.Append(Chr(34))
                strPREselect.Append(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME)
                strPREselect.Append(Chr(34))

                If HasToIncludeTaskInfo Then

                    '     strselect.Append(If(Server.isOracle, ",NVL(WF.NAME,'')", ",ISNULL(WF.NAME,'')"))
                    '    strselect.Append(" As ")
                    '   strselect.Append(Chr(34))
                    '  strselect.Append(GridColumns.WORKFLOW_COLUMNAME)
                    ' strselect.Append(Chr(34))
                    strPREselect.Append(", ")
                    strPREselect.Append(If(Server.isOracle, "NVL(S.NAME,'')", "ISNULL(S.NAME,'')"))
                    strPREselect.Append(" As ")
                    strPREselect.Append(Chr(34))
                    strPREselect.Append(GridColumns.ETAPA_COLUMNAME)
                    strPREselect.Append(Chr(34))
                    strPREselect.Append(", ")
                    strPREselect.Append(If(Server.isOracle, "NVL(SS.NAME,'')", "ISNULL(SS.NAME,'')"))
                    strPREselect.Append(" As ")
                    strPREselect.Append(Chr(34))
                    strPREselect.Append(GridColumns.STATE_COLUMNNAME)
                    strPREselect.Append(Chr(34))
                    'strselect.Append(", ")
                    ' strselect.Append(If(Server.isOracle, "NVL(TS.Task_State_NAME,'')", "ISNULL(TS.Task_State_NAME,'')"))
                    ' strselect.Append(" As ")
                    ' strselect.Append(Chr(34))
                    ' strselect.Append(GridColumns.SITUACION_COLUMNNAME)
                    ' strselect.Append(Chr(34))
                    strPREselect.Append(", ")
                    strPREselect.Append(If(Server.isOracle, "NVL(UAG.NAME,'')", "ISNULL(UAG.NAME,'')"))
                    strPREselect.Append(" As ")
                    strPREselect.Append(Chr(34))
                    strPREselect.Append(GridColumns.ASIGNADO_COLUMNNAME)
                    strPREselect.Append(Chr(34))
                End If

                If HasToIncludeLocationInfo Then
                    strPREselect.Append(", ")
                    strPREselect.Append("T.DISK_GROUP_ID, VOL_ID, DOC_FILE, OFFSET ")
                End If


                strPREselect.Append(", ")
                strPREselect.Append("T.ICON_ID,")
                strPREselect.Append("Shared,PLATTER_ID")

                If HasToIncludeVersionInfo Then
                    strPREselect.Append(",ver_Parent_id, RootId ")
                End If

                If Server.isOracle Then
                    strPREselect.Append(",get_filename(original_Filename)")
                Else
                    strPREselect.Append(",REVERSE(SUBSTRING(REVERSE(original_Filename), 0, CHARINDEX('\', REVERSE(original_Filename))))")
                End If
                strPREselect.Append(" as ")
                strPREselect.Append(Chr(34))
                strPREselect.Append(GridColumns.ORIGINAL_FILENAME_COLUMNNAME)
                strPREselect.Append(Chr(34))

                If HasToIncludeVersionInfo Then
                    strPREselect.Append(", Version as ")
                    strPREselect.Append(Chr(34))
                    strPREselect.Append(GridColumns.VERSION_COLUMNNAME)
                    strPREselect.Append(Chr(34))
                    strPREselect.Append(", NumeroVersion as ")
                    strPREselect.Append(Chr(34))
                    strPREselect.Append(GridColumns.NUMERO_DE_VERSION_COLUMNNAME)
                    strPREselect.Append(Chr(34))
                End If

                If HasToIncludeLocationInfo Then
                    strPREselect.Append(",disk_Vol_id, DISK_VOL_PATH")
                End If

                strselect.Append(",")
                strselect.Append("I.crdate as ")
                strselect.Append(Chr(34))
                strselect.Append(GridColumns.CRDATE_COLUMNNAME)
                strselect.Append(Chr(34))
                strselect.Append(", ")
                strselect.Append("I.lupdate as ")
                strselect.Append(Chr(34))
                strselect.Append(GridColumns.LASTUPDATE_COLUMNNAME)
                strselect.Append(Chr(34))



                For Each _Index As Index In Indexs
                    If (((IsNothing(VisibleIndexs) OrElse VisibleIndexs.Contains(_Index.ID)) AndAlso (IsNothing(NonvisibleIndexs) OrElse Not NonvisibleIndexs.Contains(_Index.ID))) OrElse slstFiltersIndexsIds.Contains(_Index.ID)) AndAlso _Index.isReference = False Then
                        strselect.Append(",")
                        strselect.Append("I.I")
                        strselect.Append(_Index.ID)
                        If _Index.DropDown = IndexAdditionalType.AutoSustitución _
                            Or _Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                            'strPREselect.Append("I")
                            'strPREselect.Append(_Index.ID)
                            'strPREselect.Append(",")
                            strPREselect.Append(", ")
                            strPREselect.Append(If(Server.isOracle, "NVL(", "ISNULL("))
                            strPREselect.Append("slst_s" & _Index.ID & ".descripcion, '')")
                            auIndex.Add(_Index.ID)
                            strPREselect.Append(" as ")
                            strPREselect.Append(Chr(34))
                            strPREselect.Append(_Index.Name)
                            strPREselect.Append(Chr(34))
                        Else
                            strselect.Append(" as ")
                            strselect.Append(Chr(34))
                            strselect.Append(_Index.Name)
                            strselect.Append(Chr(34))
                        End If
                    End If
                Next

                If Not fromSearchVersions Then
                    '                    If Not Server.isOracle Then
                    '                   strselect.Insert(0, " from (")
                    '                  strselect.Insert(0, ", ROWNUM as RNUM ")
                    '             Else
                    '                        If String.IsNullOrEmpty(order) Then order = "I.doc_id asc"
                    strselect.Insert(0, " from (")
                    strselect.Insert(0, ", ROW_NUMBER() OVER (ORDER  BY I.DOC_ID desc) RNUM")
                    '                End If
                    ' strselect.Insert(0, strPREselect.ToString())
                End If

            End If

            strselect.Append(String.Format(" FROM DOC_I{0} I {1}", DocType.ID, If(Server.isSQLServer, strNolock, String.Empty)))

            If GetCount = False Then
                Dim MainJoin As String = String.Format(" inner join {0} T {1} on T.doc_id = I.doc_id", strTableT, If(Server.isSQLServer, strNolock, String.Empty))
                strPOSTselect.Append(MainJoin)
            End If

            'If Not GetCount Then
            'strselect.Append(" left outer join doc_type")
            'strselect.Append(If(Server.isSQLServer, strNolock, String.Empty))
            'strselect.Append("  On doc_type.doc_type_id = ")
            'strselect.Append("T.doc_type_id")
            'End If


            If GetImportantOnly Then
                strPOSTselect.Append(" left outer join DocumentLabels dl on dl.docid = ")
                strPOSTselect.Append("T.doc_id and dl.importance=1")
            End If

            If Not GetCount Then
                If HasToIncludeLocationInfo Then
                    strPOSTselect.Append(" left outer join disk_Volume")
                    strPOSTselect.Append(If(Server.isSQLServer, strNolock, String.Empty))
                    strPOSTselect.Append(" On disk_Vol_id = vol_id ")
                End If
            End If

            If auIndex.Count > 0 Then

                If Server.isOracle AndAlso GetCount Then
                    For Each indiceID As Int64 In auIndex
                        If Not strPOSTselect.ToString().Contains("slst_s" & indiceID) Then
                            strPOSTselect.Append(" left outer join slst_s" & indiceID & If(Server.isSQLServer, strNolock, String.Empty) & " On NVL(I.i" & indiceID & ", 0) = slst_s" & indiceID & ".codigo ")
                        End If
                        ' Se agrego para que si viene algun filtro por slst tmb se haga join en la consulta interna por esa tabla
                        If slstFiltersIndexsIds.Contains(indiceID) AndAlso Not strselect.ToString().Contains("slst_s" & indiceID) Then
                            strselect.Append(" left outer join slst_s" & indiceID & If(Server.isSQLServer, strNolock, String.Empty) & " On NVL(I.i" & indiceID & ", 0) = slst_s" & indiceID & ".codigo ")
                        End If
                    Next
                Else
                    For Each indiceID As Int64 In auIndex
                        If Not strPOSTselect.ToString().Contains("slst_s" & indiceID) Then
                            strPOSTselect.Append(" left outer join slst_s" & indiceID & If(Server.isSQLServer, strNolock, String.Empty) & " On (I.i" & indiceID & ") = slst_s" & indiceID & ".codigo ")
                        End If
                        ' Se agrego para que si viene algun filtro por slst tmb se haga join en la consulta interna por esa tabla
                        If slstFiltersIndexsIds.Contains(indiceID) AndAlso Not strselect.ToString().Contains("slst_s" & indiceID) Then
                            strselect.Append(" left outer join slst_s" & indiceID & If(Server.isSQLServer, strNolock, String.Empty) & " On (I.i" & indiceID & ") = slst_s" & indiceID & ".codigo ")
                        End If
                    Next
                End If

            End If

            'Si no esta haciendo el Count realizo un inner join con la WFdocument para agregar las
            'columnas faltantes al realizar busquedas en tareas
            'If Not GetCount Then
            If HasToIncludeTaskInfo AndAlso Not GetCount Then

                strselect.Append(" left outer join wfdocument wd")
                strselect.Append(If(Server.isSQLServer, strNolock, String.Empty))
                strselect.Append(" On ")
                strselect.Append("I.DOC_ID = wd.DOC_ID ")

                strPOSTselect.Append(" left outer join wfstep S")
                strPOSTselect.Append(If(Server.isSQLServer, strNolock, String.Empty))
                strPOSTselect.Append(" On I.step_id = S.step_id ")
                strPOSTselect.Append(" left outer join wfworkflow wf ")
                strPOSTselect.Append(If(Server.isSQLServer, strNolock, String.Empty))
                strPOSTselect.Append(" On wf.work_id = S.work_id")
                strPOSTselect.Append(" left outer join WFStepStates SS ")
                strPOSTselect.Append(If(Server.isSQLServer, strNolock, String.Empty))
                strPOSTselect.Append(" On I.do_state_id = SS.doc_state_id")
                strPOSTselect.Append(" left outer join WFTask_States TS ")
                strPOSTselect.Append(If(Server.isSQLServer, strNolock, String.Empty))
                strPOSTselect.Append(" On I.Task_State_ID = TS.Task_State_ID ")
                strPOSTselect.Append("left outer join zvw_UserAndGroups uag ")
                strPOSTselect.Append(If(Server.isSQLServer, strNolock, String.Empty))
                strPOSTselect.Append("On I.user_asigned = uag.id ")
                'strselect.Append(" left outer join usrtable U ")
                'strselect.Append(If(Server.isSQLServer, strNolock, String.Empty))
                'strselect.Append(" On wd.User_Asigned = U.id ")
                'End If
            End If
            auIndex = Nothing
        End Sub

#End Region

#Region "Eventos"
        'Mostrar los results
        Public Shared Event ShowResults(ByVal Results As DataTable, ByVal SearchName As String, ByVal Search As ISearch, ByVal fromCommandLineArgs As Boolean)
        ' Public Shared Event ShowWFResults(ByVal Results As DataTable)
        Public Shared Event SearchFinished()
#End Region

#Region "Total Count"
        'Public Shared Sub SumTotalCount(ByVal Count As Int64)
        '    'RaiseEvent TotalCounts(Count)
        'End Sub
        Public Shared Event TotalCounts(ByVal TotalDocuments As Int64, ByVal DocTypesCount As Int32)
#End Region

#Region "LastSearchs"

        ''' <summary>
        ''' Recarga las busquedas anteriores
        ''' </summary>
        ''' <param name="Ls"></param>
        ''' <history>   Marcelo 20/08/2009  Modified</history>
        ''' <remarks></remarks>
        Public Shared Sub ReLoad(Ls As Searchs.LastSearch, search As ISearch)

            Dim currentUserId = Membership.MembershipHelper.CurrentUser.ID
            Dim pageSize As Int64 = Int64.Parse(UserPreferences.getValue("CantidadFilas", UPSections.UserPreferences, 100))

            Dim dt As DataTable = DoSearch(search, currentUserId, Nothing, 0, pageSize, False, False, FilterTypes.Document, False, Nothing, False, False)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                RaiseEvent ShowResults(dt, Ls.Name, search, False)
            Else
                RaiseInfo("No hay resultados, para la busqueda realizada", "Busqueda")
            End If

            UserBusiness.Rights.SaveAction(2, ObjectTypes.Documents, RightsType.Buscar, "Se realizo busqueda en ultimas busquedas  " & Ls.Name)

        End Sub

        Public Shared Function DoSearchBySearch(search As ISearch) As DataTable
            Dim currentUserId = Membership.MembershipHelper.CurrentUser.ID
            Dim pageSize As Long = Long.Parse(UserPreferences.getValue("CantidadFilas", UPSections.UserPreferences, 100))
            Dim dt As DataTable = DoSearch(search, currentUserId, Nothing, 0, pageSize, False, False, FilterTypes.Document, False, Nothing, False, False)
            Return dt
        End Function


        Public Overloads Shared Function SearchRows(ByVal SQL As String, ByVal SQLCount As String, ByVal lastPage As Int32) As DataTable
            Dim sbSelectTotal As New StringBuilder
            Dim sbSelect As New StringBuilder
            Dim strPRESelect As New StringBuilder
            Dim strPOSTSelect As New StringBuilder

            Dim CountTop As Int64 = Int64.Parse(UserPreferences.getValue("CantidadFilas", UPSections.UserPreferences, 100))
            Dim dt As DataTable = GetTaskTable(SQL, String.Empty, CountTop, lastPage, SQLCount, String.Empty, Nothing, sbSelect, sbSelectTotal, strPRESelect, strPOSTSelect)
            Return dt
        End Function

#End Region

#Region "DocumentalSearch"

        Private Shared Function FileIsIndexed(ByVal IndexedResults As ArrayList, ByVal Result As Result) As Boolean
            Try

                If Not IsNothing(IndexedResults) Then
                    For Each S As String In IndexedResults
                        If S = Result.Doc_File Then
                            Return True
                        End If
                    Next
                Else
                    Return False
                End If
            Catch ex As Exception
                raiseerror(ex)
            End Try
        End Function
#End Region

#Region "BinarySearch"
        Public Shared Function SearchFileText(ByRef Results As ArrayList, ByVal Text2Search As String) As ArrayList
            Try
                Dim i As Int32
                Dim NewResults As New ArrayList
                Dim CountRows As Int64

                For i = 0 To Results.Count - 1
                    Dim Result As Result = Results(i)
                    Try
                        If FileContainsText(Result.FullPath, Text2Search) Then
                            NewResults.Add(Result)
                            CountRows += 1
                        End If
                    Catch ex As Exception
                        raiseerror(ex)
                    End Try

                Next
                NewResults.Capacity = CountRows

                Return NewResults
            Catch ex As Exception
                raiseerror(ex)
            End Try
            Return Nothing
        End Function
        Public Shared Function FileContainsText(ByVal filename As String, ByVal text As String) As Boolean
            Dim Words() As String = Split(text.Trim, " ")
            Return FileContainsText(filename, Words)
        End Function
        Private Shared Function FileContainsText(ByVal filename As String, ByVal words() As String) As Boolean
            Dim Errors As Boolean
            If isText(filename) = False Then Return False

            ' -------------------------------------------------
            ' TO DO: Crear un diccionario para de alguna forma reconocer una palabra con acento. Por ejemplo: se tiene la palabra programación en el
            ' archivo txt y en alltext programación va aparecer como programaci[]on (en realidad con un cuadrado) por lo tanto, nunca se va a encontrar
            ' la palabra con acento cuando se realize el indexOf
            ' -------------------------------------------------

            Dim alltext As String = GetFileText(filename, Errors)

            Dim wordFind As Int32 = 0

            For Each w As String In words
                If alltext.IndexOf(w.ToLower) <> -1 Then
                    wordFind += 1
                    Return True
                End If
            Next
            Return False
        End Function
        Public Shared Function isText(ByVal filename As String) As Boolean
            If IsNothing(filename) Then
                Return False
            End If

            Dim Fi As New FileInfo(filename.Trim)

            If (String.Compare(Fi.Extension, ".doc", True) = 0) Then Return True
            If (String.Compare(Fi.Extension, ".wri", True) = 0) Then Return True
            If (String.Compare(Fi.Extension, ".pdf", True) = 0) Then Return True
            If (String.Compare(Fi.Extension, ".htm", True) = 0) Then Return True
            If (String.Compare(Fi.Extension, ".html", True) = 0) Then Return True
            If (String.Compare(Fi.Extension, ".xls", True) = 0) Then Return True
            If (String.Compare(Fi.Extension, ".txt", True) = 0) Then Return True
            Return False
        End Function
        Private Shared Function GetFileText(ByVal FileName As String, ByVal Errors As Boolean) As String
            Dim Text As String = String.Empty

            Dim Fi As New FileInfo(FileName)

            Dim Fs As FileStream = Nothing
            Dim Sr As StreamReader = Nothing

            Errors = False

            Try
                Fs = Fi.OpenRead()
                Sr = New StreamReader(Fs)
            Catch ex As IOException
                If IsNothing(Fs) = False Then
                    Fs.Close()
                End If

                If IsNothing(Sr) = False Then
                    Sr.Close()
                End If

                Errors = True
            End Try

            If Errors = False Then
                Text = Sr.ReadToEnd().ToLower()
                Sr.Close()
                Fs.Close()
            End If
            Return Text
        End Function
        ''' <summary>
        ''' Busca el texto especificado en los archivos fìsicos
        ''' </summary>
        ''' <param name="Results"></param>
        ''' <param name="Text2Search"></param>
        ''' <param name="AllFiles"></param>
        ''' <history>Marcelo    modified    19/08/2009</history>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function SearchFileText(ByVal Results As DataTable, ByVal Text2Search As String) As DataTable
            Try
                Dim NewResults As DataTable = Results.Clone()
                'Dim IndexedResults As ArrayList = SearchTextIndexing(Results, Text2Search)

                For Each Result As DataRow In Results.Rows
                    Try
                        If FileContainsText(getFullPath(Result), Text2Search) Then
                            NewResults.Rows.Add(Result.ItemArray)
                        End If
                    Catch ex As Exception
                        raiseerror(ex)
                    End Try
                    'Try
                    'busco los archivos que si tienen la frase por indexado
                    'If FileIsIndexed(IndexedResults, Result) Then
                    'NewResults.Rows.Add(Result.ItemArray())
                    'End If
                    'Catch ex As Exception
                    'Zamba.Core.ZClass.raiseerror(ex)
                    'End Try
                Next

                Return NewResults
            Catch ex As Exception
                raiseerror(ex)
            End Try
            Return Nothing
        End Function

        ''' <summary>
        ''' Obtiene la ruta 
        ''' </summary>
        ''' <param name="result"></param>
        ''' <history>Marcelo    created 19/08/2009</history>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getFullPath(ByVal result As DataRow) As String
            If result.Item("Disk_group_id") = -1 Then
                Return result.Item("doc_file")
            ElseIf result.Item("Disk_group_id") = 0 Then
                Return String.Empty
            Else
                Return result.Item("disk_vol_path") & "\" & result.Item("doc_type_id") & "\" & result.Item("offset") & "\" & result.Item("doc_file")
            End If
        End Function
#End Region

#Region "NotesSearch"
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Funcion para realizar busquedas en Notas y comentarios del Foro
        ''' </summary>
        ''' <param name="Results">Arraylist de objetos Results donde se va a realizar la busqueda </param>
        ''' <param name="Text2Search">Texto que se desea buscar</param>
        ''' <returns>Arraylist de resultados</returns>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Hernan]	29/05/2006	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Shared Function SearchInNotesAndForo(ByRef Results As ArrayList, ByVal SearchString As String) As ArrayList
            Dim NewResults As New Hashtable
            Dim ResultsList As New ArrayList
            'Dim CountRows As Int64

            Try
                Dim SelectQuery As String = "Select Doc_Id FROM Doc_notes where (lower(Note_text) Like '%" & SearchString.Trim.ToLower & "%')"
                Dim DsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, SelectQuery)
                Dim Doc_IdOrdinal As Int32 = DsTemp.Tables(0).Columns("DOC_ID").Ordinal

                For Each Row As DataRow In DsTemp.Tables(0).Rows
                    For Each CurrentResult As Result In Results
                        If Row.Item(Doc_IdOrdinal) = CurrentResult.ID Then
                            NewResults.Add(CurrentResult.ID, CurrentResult)
                            'CountRows += +1
                        End If
                    Next
                Next

            Catch ex As Exception
                raiseerror(ex)
            End Try

            Try
                Dim SelectQuery As StringBuilder
                SelectQuery.Append("SELECT distinct docid, zf.UserId, zf.IdMensaje, zf.ParentId, zf.LinkName, zf.Mensaje, zf.Fecha, ")
                SelectQuery.Append("zfd.doct, ut.name, ut.nombres, ut.apellido, zf.DiasVto FROM zforum zf ")
                SelectQuery.Append("INNER JOIN ZFORUM_R_DOC zfd on zfd.IdMensaje = zf.IdMensaje ")
                SelectQuery.Append("INNER JOIN USRTABLE ut on ut.id = zf.userid ")

                SelectQuery.Append("WHERE zfd.Doct in () and (lower(Mensaje) Like '%" & LCase(SearchString.Trim) & "%') OR (lower(LinkName) Like '%" & LCase(SearchString.Trim) & "%')")

                Dim DsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, SelectQuery.ToString)
                Dim DocIdOrdinal As Int32 = DsTemp.Tables(0).Columns("DOCID").Ordinal

                For Each Row As DataRow In DsTemp.Tables(0).Rows
                    For Each CurrentResult As Result In Results
                        If Row.Item(DocIdOrdinal) = CurrentResult.ID Then
                            NewResults.Add(CurrentResult.ID, CurrentResult)
                            'CountRows += +1
                        End If
                    Next
                Next


            Catch ex As Exception
                raiseerror(ex)
            End Try


            ResultsList.AddRange(NewResults.Values)
            ResultsList.Capacity = NewResults.Count
            Return ResultsList
        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Funcion para realizar busquedas en Notas y comentarios del Foro
        ''' </summary>
        ''' <param name="Results">Arraylist de objetos Results donde se va a realizar la busqueda </param>
        ''' <param name="Text2Search">Texto que se desea buscar</param>
        ''' <returns>Arraylist de resultados</returns>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Hernan]	29/05/2006	Created
        ''' 	[Marcelo]	19/08/2009	Modified
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Shared Function SearchInNotesAndForo(ByVal Results As DataTable, ByVal SearchString As String) As DataTable
            Dim NewResults As DataTable = Results.Clone()
            Try
                Dim SelectQuery As String = "SELECT Doc_Id FROM Doc_notes where (lower(Note_text) LIKE '%" & SearchString.Trim.ToLower & "%')"
                Dim DsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, SelectQuery)
                Dim Doc_IdOrdinal As Int32 = DsTemp.Tables(0).Columns("DOC_ID").Ordinal

                For Each Row As DataRow In DsTemp.Tables(0).Rows
                    For Each CurrentResult As DataRow In Results.Rows
                        If Row.Item(Doc_IdOrdinal) = CurrentResult("doc_ID") Then
                            NewResults.Rows.Add(CurrentResult.ItemArray())
                        End If
                    Next
                Next
            Catch ex As Exception
                raiseerror(ex)
            End Try

            Try
                Dim SelectQuery As StringBuilder
                SelectQuery.Append("SELECT distinct docid, zf.UserId, zf.IdMensaje, zf.ParentId, zf.LinkName, zf.Mensaje, zf.Fecha, ")
                SelectQuery.Append("zfd.doct, ut.name, ut.nombres, ut.apellido, zf.DiasVto FROM zforum zf ")
                SelectQuery.Append("INNER JOIN ZFORUM_R_DOC zfd on zfd.IdMensaje = zf.IdMensaje ")
                SelectQuery.Append("INNER JOIN USRTABLE ut on ut.id = zf.userid ")

                SelectQuery.Append("WHERE zfd.Doct in () and (lower(Mensaje) Like '%" & LCase(SearchString.Trim) & "%') OR (lower(LinkName) Like '%" & LCase(SearchString.Trim) & "%')")

                Dim DsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, SelectQuery.ToString)
                Dim DocIdOrdinal As Int32 = DsTemp.Tables(0).Columns("DOCID").Ordinal

                For Each Row As DataRow In DsTemp.Tables(0).Rows
                    For Each CurrentResult As DataRow In Results.Rows
                        If Row.Item(DocIdOrdinal) = CurrentResult("doc_id") Then
                            NewResults.Rows.Add(CurrentResult.ItemArray())
                        End If
                    Next
                Next
            Catch ex As Exception
                raiseerror(ex)
            End Try

            Return NewResults
        End Function
#End Region

#Region "Versions"
        ''' <summary>
        ''' Muestra las versiones padre del documento
        ''' </summary>
        ''' <param name="Result"></param>
        ''' <returns></returns>
        ''' <history>   Marcelo Modified 20/08/2009</history>
        ''' <remarks></remarks>
        Public Shared Function SearchParentVersions(ByRef Result As Result) As DataTable
            'Si RootId = 0 <==> Este result no es una version nueva de otro result , la consulta traeria a todos los results que tienen rootId = 0 que son todos los results NO versionados.
            If Result.RootDocumentId = 0 Then Return Nothing

            'TODO Falta que lo saque del dscore
            'TODO Falta que se pueda configurar
            Try
                If IsNothing(Result.DocType) Then
                    Result.DocType = New DocType(Result.DocTypeId, Result.DocType.Name, 0)
                End If

                Return SearchVersions(Result.DocType, Result.RootDocumentId, Result.ID, Nothing)
            Catch ex As Exception
                raiseerror(ex)
            End Try
            Return Nothing
        End Function

        ''' <summary>
        ''' Busca las versiones
        ''' </summary>
        ''' <param name="docType"></param>
        ''' <param name="rootDocumentId"></param>
        ''' <param name="docId"></param>
        ''' <returns></returns>
        ''' <history>   Marcelo Modified 20/08/2009</history>
        ''' <remarks></remarks>
        Public Shared Function SearchVersions(ByVal docType As DocType, ByVal rootDocumentId As Int64, ByVal docId As Int64, ByVal search As ISearch) As DataTable
            Try
                Dim TableName As String = "Doc" & docType.ID.ToString()
                Dim Query As New StringBuilder()
                Dim strSelect As New StringBuilder
                Dim strPREselect As New StringBuilder
                Dim strPOSTselect As New StringBuilder
                createSelectSearch(TableName, docType.Indexs, Nothing, False, String.Empty, Nothing, True, False, Nothing, False, False, True, Nothing, search, strSelect, strPREselect, strPOSTselect)

                Query.Append(strSelect)
                If rootDocumentId > 0 Then
                    Query.Append(" WHERE ")
                    Query.Append("RootId = ")
                    Query.Append(rootDocumentId.ToString())
                    Query.Append(" OR ")
                    Query.Append(TableName)
                    Query.Append(".")
                    Query.Append("doc_id = ")
                    Query.Append(rootDocumentId.ToString())
                Else
                    Query.Append(" WHERE ")
                    Query.Append(TableName)
                    Query.Append(".")
                    Query.Append("doc_id = ")
                    Query.Append(docId.ToString())
                End If

                Query.Append("Order by ")
                Query.Append(TableName)
                Query.Append(".CRDATE")

                Dim CountTop As Int64 = Int64.Parse(UserPreferences.getValue("CantidadFilas", UPSections.UserPreferences, 100))

                Dim ds As DataSet = Nothing
                Dim dt As DataTable = Nothing

                ds = Server.Con.ExecuteDataset(CommandType.Text, Query.ToString)

                If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing Then
                    dt = ds.Tables(0)
                    Dim arraySustIndex As New ArrayList
                    Dim i As Int32 = 0
                    Dim first As Boolean = True
                    For Each r As DataRow In dt.Rows
                        CreateRow(arraySustIndex, r, docType.Indexs)
                    Next
                End If


                Return dt
            Catch ex As Exception
                raiseerror(ex)
            End Try
            Return Nothing
        End Function
#End Region

        Public Overrides Sub Dispose()

        End Sub
    End Class
End Namespace
