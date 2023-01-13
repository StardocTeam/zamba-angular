Imports System.IO

Imports Zamba.Filters
Imports Zamba.Servers
Imports System.Data
Imports System.Collections
Imports Zamba.Data
Imports System.Text
Imports System.Collections.Generic

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
        Public Shared Event TotalCount(ByVal Count As Int64)
        Public Shared Event DeboMaximizar()

        'Public Overloads Shared Function DoSearchAsocWithWF(ByVal CurrentDocType As DocType, ByVal Indexs As List(Of IIndex), ByVal UserId As Int64, ByRef FC As FiltersComponent, ByVal LastPage As Int64, ByVal PageSize As Int32, ByVal docId As Int64) As DataTable
        '    Dim dtResults As New DataTable
        '    Try
        '        Dim i As Int32
        '        dtResults.MinimumCapacity = 0

        '        Dim dt As DataTable = SearchRowsAsocForm(CurrentDocType, Indexs, UserId, FC, LastPage, PageSize, docId, Nothing)
        '        If dt IsNot Nothing Then
        '            dtResults.MinimumCapacity = dtResults.MinimumCapacity + dt.MinimumCapacity
        '            dtResults.Merge(dt)
        '        End If

        '        Return dtResults

        '    Catch ex As Threading.SynchronizationLockException
        '        ZClass.raiseerror(ex)
        '    Catch ex As Threading.ThreadAbortException
        '        ZClass.raiseerror(ex)
        '    Catch ex As Threading.ThreadInterruptedException
        '        ZClass.raiseerror(ex)
        '    Catch ex As Threading.ThreadStateException
        '        ZClass.raiseerror(ex)
        '    Catch ex As Exception
        '        ZClass.raiseerror(ex)
        '    End Try
        '    Return New DataTable()
        'End Function


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
                                                  Optional ByVal filtersChanged As Boolean = False) As DataTable

            Dim dtResults As New DataTable

            Try
                MostrarWaitForm(True, False)
                If IsNothing(FC) Then FC = New FiltersComponent()
                'Dim SearchName As String = String.Empty
                Dim CurrentDocType As DocType
                Dim searchSql As String = String.Empty
                Dim CountSearchSql As String = String.Empty
                search.Name = LastSearchBusiness.GetSearchName(search)

                'If Not String.IsNullOrEmpty(search.Textsearch) Then
                '    'SOLO  BUSQUEDA EN TODOS LOS ATRIBUTOS
                '    dtResults.MinimumCapacity = 0
                '    Dim dsResultsId As DataSet
                '    Try

                '        'Si es la primera vez se crea la query
                '        If search.SQL.Count = 0 Then
                '            'Obtiene ids de los results
                '            dsResultsId = Results_Factory.SearchInAllIndexs(search, searchSql)
                '            search.SQL.Add(searchSql)
                '            search.SQLCount.Add(CountSearchSql)
                '        Else
                '            dsResultsId = Results_Factory.SearchInAllIndexs(search.SQL(0).ToString)
                '        End If

                '        If dsResultsId IsNot Nothing AndAlso dsResultsId.Tables.Count > 0 Then

                '            For Each _docType As DocType In search.Doctypes
                '                For Each row As DataRow In dsResultsId.Tables(0).Rows
                '                    Dim drResult As DataRow = Results_Business.GetResultRow(CType(row(0), Int64), CType(row(1), Int64))
                '                    If Not IsNothing(drResult) Then
                '                        dtResults.MinimumCapacity += drResult.Table.Rows.Count
                '                        dtResults.Merge(drResult.Table)
                '                    End If
                '                Next
                '            Next

                '        End If

                '    Catch ex As Exception
                '        search.Name = Now.ToString
                '        ZClass.raiseerror(ex)
                '    End Try

                '    If Not isAssociatedSearch Then
                '        Try
                '            If SaveSearch AndAlso Not LastSearchBusiness.LastSearchAlreadyExist(search.Name) Then
                '                Using lsb As New LastSearchBusiness
                '                    lsb.Save(search)
                '                End Using
                '            End If
                '        Catch ex As Exception
                '            ZClass.raiseerror(ex)
                '        End Try
                '        If raiseResults AndAlso FilterType <> FilterTypes.Task Then
                '            UserBusiness.Rights.SaveAction(2, ObjectTypes.Documents, RightsType.Buscar, "Se realizó una búsqueda por la palabra: " & search.Textsearch)
                '            RaiseEvent ShowResults(dtResults, search.Name, search, fromCommandLineArgs)
                '        End If
                '    End If

                '    Return dtResults

                'Else
                dtResults.MinimumCapacity = 0
                Dim i As Int32
                Dim dt As DataTable

                'Si es la primera vez o si llega por cambio de ordenamiento o de filtro se crean las querys de nuevo
                If search.SQL.Count = 0 OrElse sortChanged OrElse filtersChanged Then

                    search.SQL.Clear()
                    search.SQLCount.Clear()

                    For i = 0 To search.Doctypes.Count - 1
                        CurrentDocType = search.Doctypes(i)
                        CurrentDocType.Indexs = ZCore.FilterIndex(CurrentDocType.ID)

                        dt = SearchRows(CurrentDocType, search.Indexs, UserId, FC, LastPage, PageSize, UseFilters, FilterType, visibleIndexs, searchSql, CountSearchSql, search.OrderBy,,, search.Textsearch)

                        If Not dt Is Nothing Then
                            dtResults.MinimumCapacity = dtResults.MinimumCapacity + dt.MinimumCapacity
                            dtResults.Merge(dt)
                        End If

                        search.SQL.Add(searchSql)
                        search.SQLCount.Add(CountSearchSql)

                        If Not isAssociatedSearch Then
                            Try
                                Dim indexsstring As String = String.Empty
                                For Each ind As IIndex In search.Indexs
                                    indexsstring &= ". Se filtro por el Atributo '" & ind.Name & " (" & ind.ID & ")' con el operador '" & ind.Operator & "' y valor '" & ind.DataTemp & "'"
                                Next
                                UserBusiness.Rights.SaveAction(2, ObjectTypes.Documents, RightsType.Buscar,
                                                               "Se realizó búsqueda por la entidad: " _
                                                               & CurrentDocType.Name & " (" & CurrentDocType.ID & ")" & indexsstring)
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try
                        End If
                    Next

                    'If search.Textsearch.Length > 0 OrElse search.Textsearch.Length > 0 Then
                    '    If String.IsNullOrEmpty(search.Textsearch) Then
                    '        'BUSQUEDA DE NOTAS
                    '        dtResults = ModDocuments.SearchInNotesAndForo(dtResults, search.Textsearch)
                    '    ElseIf String.IsNullOrEmpty(search.Textsearch) Then
                    '        'BUSQUEDA DE TEXTO
                    '        dtResults = ModDocuments.SearchFileText(dtResults, search.Textsearch)
                    '    Else
                    '        'BUSQUEDA DE NOTAS Y TEXTO
                    '        dtResults = ModDocuments.SearchInNotesAndForo(dtResults, search.Textsearch)
                    '        dtResults = ModDocuments.SearchFileText(dtResults, search.Textsearch)
                    '    End If
                    'End If

                    If Not isAssociatedSearch Then
                        Try
                            If SaveSearch AndAlso Not LastSearchBusiness.LastSearchAlreadyExist(search.Name) Then
                                Using lsb As New LastSearchBusiness
                                    lsb.Save(search)
                                End Using
                            End If
                        Catch ex As Exception

                        End Try

                        If raiseResults AndAlso FilterType <> FilterTypes.Task Then
                            RaiseEvent ShowResults(dtResults, search.Name, search, fromCommandLineArgs)
                        End If
                    End If

                Else

                    For i = 0 To search.SQL.Count - 1
                        dt = GetTaskTable(search.SQL(i), search.OrderBy, PageSize, LastPage, search.SQLCount(i), Nothing)
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
                ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadAbortException
                ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadInterruptedException
                ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadStateException
                ZClass.raiseerror(ex)
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
        Public Shared Function GetTaskTable(query As String, order As String, pageSize As Int32, lastPage As Int32, queryCount As String, orderType As String) As DataTable

            Dim Desde As Int32 = (pageSize * lastPage) + 1
            Dim Hasta As Int32 = Desde + pageSize - 1
            Dim ds As DataSet
            Dim pagingString As New StringBuilder
            Dim totalrows As Int64

            Try
                If Server.isOracle Then
                    query = query.Replace("Exclusive,", "C_Exclusive,")
                    query = query.Replace(") as Q", ")")
                    query = query.Replace("[", "")
                    query = query.Replace("]", "")
                    queryCount = queryCount.Replace("Exclusive,", "C_Exclusive,")
                    queryCount = queryCount.Replace(") as Q", ")")
                    queryCount = queryCount.Replace("[", "")
                    queryCount = queryCount.Replace("]", "")
                End If

                pagingString.Append("select * FROM (")
                pagingString.Append(query)
                If Server.isOracle Then
                    If String.IsNullOrEmpty(order) Then order = "doc_id asc"
                    pagingString.Append(" Order by " & order.Trim())
                End If
                pagingString.Append(") a) x")

                If (Hasta > 0) Then
                    pagingString.Append(" where rnum >= ")
                    pagingString.Append(Desde)
                    pagingString.Append(" AND ")
                    pagingString.Append(" rnum <= ")
                    pagingString.Append(Hasta)
                End If

                ZTrace.WriteLineIf(TraceLevel.Verbose, String.Format("Consulta: {0} Orden: {1} Desde: {2} Hasta: {3}", pagingString.ToString(), order, Desde, Hasta))
                ds = Server.Con.ExecuteDataset(CommandType.Text, pagingString.ToString())

                If Not lastPage > 0 Then
                    If Not String.IsNullOrEmpty(queryCount) Then
                        ZTrace.WriteLineIf(TraceLevel.Verbose, String.Format("Consulta cantidad: {0}", queryCount))
                        totalrows = Server.Con.ExecuteScalar(CommandType.Text, queryCount)
                    Else
                        Dim queryAuxCount As String
                        queryAuxCount = query.Substring(6)
                        queryCount = "select distinct(subQuery.totalRows) from (select count(*)over() as totalRows," + queryAuxCount + ")) subQuery"
                        totalrows = Server.Con.ExecuteScalar(CommandType.Text, queryCount)
                    End If
                    ds.Tables(0).MinimumCapacity = totalrows
                End If

                Return ds.Tables(0)
            Catch ex As Exception
                ZClass.raiseerror(ex)
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


        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Funcion para realizar una busqueda en Zamba, devuelve un arraylist de objetos results
        ''' </summary>
        ''' <param name="DocGroup">Id del Grupo o Sector de Zamba</param>
        ''' <param name="DocType">objeto Doc_type donde se realizará la busqueda</param>
        ''' <param name="INDEXS">Vector de objetos atributos con la propiedad Data y/o Datatemp cargada</param>
        ''' <param name="user">Objeto User que requiere la busqueda para el log de actividades</param>
        ''' <param name="DocTypeCount">Maxima cantidad de resultados posibles, para limitar los resultados</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Hernan]	29/05/2006	Created
        '''     [Tomas]     30/07/2009  Modified    Se modifica la manera en que obtiene el userid de la tabla zi. Ahora se le hace
        '''                                         un inner join para obtenerlos. Se agrega una validacion para que cuando se arme 
        '''                                         la consulta concatene o no el Version=0. Esto depende del valor UseVersion del 
        '''                                         UserConfig (por defecto viene en False).
        '''     [Javier]    01/10/2010  Modified    Se aplican cambios para las restricciones
        '''     [Javier]    12/10/2010  Modified    Se coloca las restricciones dentro del primer where ya que si no había permisos
        '''                                         la consulta fallaba
        ''' </history>
        ''' -----------------------------------------------------------------------------
        'Public Overloads Shared Function SearchRowsAsocForm(ByVal DocType As DocType, ByVal Indexs As Generic.List(Of IIndex), ByVal CurrentUserId As Int64, ByVal FC As FiltersComponent, ByVal LastPage As Int64, ByVal PageSize As Int32, ByVal doc_id As Int64, ByVal ColumnsOrder As Dictionary(Of String, String)) As DataTable
        '    Dim searchSqltotal As String
        '    Dim strTable As String = Results_Factory.MakeTable(DocType.ID, Results_Factory.TableType.Full)
        '    Dim FlagCase As Boolean = Boolean.Parse(UserPreferences.getValue("CaseSensitive", Sections.UserPreferences, True))
        '       Dim first As Boolean = True
        ' Dim i As Integer
        '    Dim Valuestring As New System.Text.StringBuilder
        '    Dim ColumCondstring As New System.Text.StringBuilder
        '    Dim First As Boolean = True
        '    Dim indexOrderString As New System.Text.StringBuilder
        '    Dim dateDeclarationString As New System.Text.StringBuilder
        '    Dim RestrictionString As New System.Text.StringBuilder
        '    Dim strselect As System.Text.StringBuilder

        '    Try
        '        If Cache.DocTypesAndIndexs.hsRestrictionsIndexs.ContainsKey(CurrentUserId & "-" & DocType.ID) = False Then
        '            Cache.DocTypesAndIndexs.hsRestrictionsIndexs.Add(CurrentUserId & "-" & DocType.ID, RestrictionsMapper_Factory.getRestrictionIndexs(CurrentUserId, DocType.ID))
        '        End If

        '        Dim indRestriction As List(Of IIndex) = Cache.DocTypesAndIndexs.hsRestrictionsIndexs.Item(CurrentUserId & "-" & DocType.ID)

        '        For i = 0 To Indexs.Count - 1
        '            CreateWhereSearch(Valuestring, Indexs, i, FlagCase, ColumCondstring, First, dateDeclarationString, True)
        '        Next

        '        For i = 0 To indRestriction.Count - 1
        '            CreateWhereSearch(Valuestring, indRestriction, i, FlagCase, RestrictionString, First, dateDeclarationString, False)
        '        Next

        '        indexOrderString = FillOrderBy(indexOrderString, ColumnsOrder)

        '        'se obtienen los permisos de visualizacion de atributos para la grilla de asociados
        '        Dim visibleIndexs As List(Of Int64) = IndexsBusiness.GetAssociateDocumentIndexsRights(DocType.ID, strTable, CurrentUserId)

        '        Dim OrderBy As String = "doc_id asc"

        '        strselect = createSelectSearch(strTable, DocType.Indexs, visibleIndexs, False, OrderBy)

        '        'Versiones
        '        If Boolean.Parse(UserPreferences.getValue("UseVersion", Sections.UserPreferences, False)) Then
        '            If ColumCondstring.Length > 0 Then
        '                ColumCondstring.Remove(ColumCondstring.Length - 1, 1)
        '                ColumCondstring.Append(" AND Version = 0)")
        '            Else
        '                ColumCondstring.Append(" Version = 0")
        '            End If
        '        End If

        '        If InsertFilterRestrictionConditionString(DocType, ColumCondstring, New StringBuilder(), CurrentUserId, FC, strselect, dateDeclarationString, False, FilterTypes.Asoc, RestrictionString) = False Then
        '            Return New DataTable
        '        End If

        '        'Si tiene doc_id no lo traigo ya que es el mismo documento
        '        If doc_id > 0 Then
        '            If strselect.ToString.Contains(" WHERE ") Then
        '                strselect.Append(" AND ")
        '            Else
        '                strselect.Append(" WHERE ")
        '            End If
        '            strselect.Append(strTable)
        '            strselect.Append(".doc_id<>")
        '            strselect.Append(doc_id)
        '        End If

        '        Dim arraySustIndex As New ArrayList
        '        First = True

        '        i = 0
        '        Dim dt As DataTable = GetTaskTable(strselect.ToString, indexOrderString.ToString, PageSize, LastPage, String.Empty, Nothing)

        '        Return dt
        '    Catch ex As Exception
        '        ZClass.raiseerror(ex)
        '    Finally
        '        strselect = Nothing
        '        Valuestring = Nothing
        '        ColumCondstring = Nothing
        '        indexOrderString = Nothing
        '    End Try
        '    Return Nothing
        'End Function

        ''' <summary>
        ''' Armado del select que se utiliza para las busquedas
        ''' </summary>
        ''' <param name="strTable">Nombre de la tabla. Ej: doc58</param>
        ''' <param name="Indexs">Atributos que se van a traer</param>
        ''' <returns>Stringbuilder con la consulta</returns>
        ''' <history>   Marcelo    Created     20/08/09</history>
        ''' <remarks></remarks>
        'Private Shared Function createSelectSearchAsocForm(ByVal strTable As String, ByVal Indexs As List(Of IIndex), ByVal VisibleIndexs As List(Of Int64)) As StringBuilder
        '    Dim strselect As New System.Text.StringBuilder
        '    Dim auIndex As New List(Of Int64)
        '    strselect.Append("SELECT ")
        '    strselect.Append(strTable)
        '    strselect.Append(".DOC_ID, ")
        '    strselect.Append(strTable)
        '    strselect.Append(".DISK_GROUP_ID,PLATTER_ID,VOL_ID,DOC_FILE,OFFSET,")
        '    strselect.Append(strTable)
        '    strselect.Append(".DOC_TYPE_ID, ")
        '    strselect.Append(strTable)
        '    strselect.Append(".NAME as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME)
        '    strselect.Append(Chr(34))
        '    strselect.Append(",")
        '    strselect.Append(strTable)
        '    strselect.Append(".ICON_ID,SHARED,ver_Parent_id,RootId,")
        '    If Server.isOracle Then
        '        strselect.Append("get_filename(original_Filename)")
        '    Else
        '        strselect.Append("REVERSE(SUBSTRING(REVERSE(original_Filename), 0, CHARINDEX('\', REVERSE(original_Filename))))")
        '    End If
        '    strselect.Append(" as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append(GridColumns.ORIGINAL_FILENAME_COLUMNNAME)
        '    strselect.Append(Chr(34))
        '    strselect.Append(",Version, NumeroVersion as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append(GridColumns.NUMERO_DE_VERSION_COLUMNNAME)
        '    strselect.Append(Chr(34))
        '    strselect.Append(",disk_Vol_id, DISK_VOL_PATH, doc_type_name as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append("Entidad")
        '    strselect.Append(Chr(34))
        '    strselect.Append(", " & strTable & ".crdate as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append(GridColumns.CRDATE_COLUMNNAME)
        '    strselect.Append(Chr(34))
        '    strselect.Append(", " & strTable & ".lupdate as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append(GridColumns.LASTUPDATE_COLUMNNAME)
        '    strselect.Append(Chr(34))

        '    strselect.Append(",wf.NAME as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append(GridColumns.WORKFLOW_COLUMNAME)

        '    strselect.Append(Chr(34))
        '    strselect.Append(", S.NAME as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append(GridColumns.ETAPA_COLUMNAME)

        '    strselect.Append(Chr(34))
        '    strselect.Append(", SS.NAME as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append(GridColumns.ESTADO_TAREA_COLUMNNAME)

        '    strselect.Append(Chr(34))
        '    strselect.Append(", TS.Task_State_NAME as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append(GridColumns.SITUACION_COLUMNNAME)

        '    strselect.Append(Chr(34))
        '    strselect.Append(", U.NAME as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append(GridColumns.USER_ASIGNEDNAME_COLUMNNAME)
        '    strselect.Append(Chr(34))

        '    For Each _Index As Index In Indexs
        '        If IsNothing(VisibleIndexs) OrElse Not VisibleIndexs.Contains(_Index.ID) Then
        '            strselect.Append(",")
        '            strselect.Append(strTable)
        '            strselect.Append(".I")
        '            strselect.Append(_Index.ID)
        '            If _Index.DropDown = IndexAdditionalType.AutoSustitución _
        '                Or _Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
        '                strselect.Append(", slst_s" & _Index.ID & ".descripcion")
        '                auIndex.Add(_Index.ID)
        '            End If
        '            strselect.Append(" as ")
        '            strselect.Append(Chr(34))
        '            strselect.Append(_Index.Name)
        '            strselect.Append(Chr(34))
        '        End If
        '    Next
        '    strselect.Append(" FROM ")
        '    strselect.Append(strTable)
        '    strselect.Append(" inner join doc_type on doc_type.doc_type_id = ")
        '    strselect.Append(strTable)
        '    strselect.Append(".doc_type_id left outer join disk_Volume on disk_Vol_id=vol_id ")

        '    If auIndex.Count > 0 Then
        '        For Each indiceID As Int64 In auIndex
        '            strselect.Append(" left join slst_s" & indiceID & " on " & strTable & ".i" & indiceID & " = slst_s" & indiceID & ".codigo ")
        '        Next
        '    End If

        '    'realizo un inner join con la WFdocument para agregar las
        '    'columnas faltantes al realizar busquedas en tareas
        '    strselect.Append(" left join wfdocument wd ON ")
        '    strselect.Append(strTable)
        '    strselect.Append(".DOC_ID = wd.DOC_ID")

        '    strselect.Append(" left join wfstep S on wd.step_id=S.step_id ")
        '    strselect.Append(" left join wfworkflow wf on wf.work_id=S.work_id ")
        '    strselect.Append(" left join WFStepStates SS on wd.do_state_id=SS.doc_state_id ")
        '    strselect.Append(" left join WFTask_States TS on wd.Task_State_ID=TS.Task_State_ID ")
        '    strselect.Append(" left join usrtable U on wd.User_Asigned=U.id ")

        '    auIndex = Nothing
        '    Return strselect
        'End Function


        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Funcion para realizar una busqueda en Zamba, devuelve un arraylist de objetos results
        ''' </summary>
        ''' <param name="DocGroup">Id del Grupo o Sector de Zamba</param>
        ''' <param name="DocType">objeto Doc_type donde se realizará la busqueda</param>
        ''' <param name="INDEXS">Vector de objetos atributos con la propiedad Data y/o Datatemp cargada</param>
        ''' <param name="user">Objeto User que requiere la busqueda para el log de actividades</param>
        ''' <param name="DocTypeCount">Maxima cantidad de resultados posibles, para limitar los resultados</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Hernan]	29/05/2006	Created
        '''     [Tomas]     30/07/2009  Modified    Se modifica la manera en que obtiene el userid de la tabla zi. Ahora se le hace
        '''                                         un inner join para obtenerlos. Se agrega una validacion para que cuando se arme 
        '''                                         la consulta concatene o no el Version=0. Esto depende del valor UseVersion del 
        '''                                         UserConfig (por defecto viene en False).
        '''     [Javier]    01/10/2010  Modified    Se aplican cambios para las restricciones
        '''     [Javier]    12/10/2010  Modified    Se coloca las restricciones dentro del primer where ya que si no había permisos
        '''                                         la consulta fallaba
        ''' </history>
        ''' -----------------------------------------------------------------------------
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
                                                    ByVal Optional ExcludedParentDocId As Int64 = 0, Optional ByVal NonvisibleIndexs As List(Of Int64) = Nothing, ByVal Optional ContentSearch As String = "") As DataTable

            'Dim searchSqltotal As String
            Dim strTable As String = Results_Factory.MakeTable(DocType.ID, Results_Factory.TableType.Full)
            Dim flagCase As Boolean = Boolean.Parse(UserPreferences.getValue("CaseSensitive", Sections.UserPreferences, True))
            Dim first As Boolean = True
            Dim i As Integer
            Dim indexRestrictions As List(Of IIndex)

            Dim sbSelectTotal As StringBuilder = Nothing
            Dim sbSelect As StringBuilder = Nothing
            Dim sbValue As New StringBuilder
            Dim sbCondition As New StringBuilder
            Dim sbDate As New StringBuilder
            Dim sbRestriction As New StringBuilder
            Dim sbClose As New StringBuilder
            Dim orderType As String

            If String.IsNullOrEmpty(OrderBy) Then
                OrderBy = "doc_id asc"
            End If

            Try
                With Cache.DocTypesAndIndexs.hsRestrictionsIndexs
                    If .ContainsKey(CurrentUserId & "-" & DocType.ID) = False Then
                        .Add(CurrentUserId & "-" & DocType.ID, RestrictionsMapper_Factory.getRestrictionIndexs(CurrentUserId, DocType.ID))
                    End If
                    indexRestrictions = .Item(CurrentUserId & "-" & DocType.ID)
                End With

                For i = 0 To Indexs.Count - 1
                    CreateWhereSearch(sbValue, Indexs, i, flagCase, sbCondition, first, sbDate, False)
                Next
                For i = 0 To indexRestrictions.Count - 1
                    CreateWhereSearch(sbValue, indexRestrictions, i, flagCase, sbRestriction, first, sbDate, True)
                Next

                sbSelectTotal = createSelectSearch(strTable, DocType.Indexs, visibleIndexs, True, OrderBy, NonvisibleIndexs)

                sbSelect = createSelectSearch(strTable, DocType.Indexs, visibleIndexs, False, OrderBy, NonvisibleIndexs)


                If ContentSearch.Length > 0 Then
                    Dim sbContentSearch As New StringBuilder

                    sbContentSearch.Append("inner join (")
                    sbContentSearch.Append(Results_Factory.GetSearchInAllIndexsJoins(ContentSearch, DocType.ID))
                    sbContentSearch.Append(") AI on AI.ResultId = ")
                    sbContentSearch.Append(strTable)
                    sbContentSearch.Append(".DOC_ID")

                    sbSelect.Append(sbContentSearch.ToString())
                    sbSelectTotal.Append(sbContentSearch.ToString())
                End If


                'Versiones
                'If Boolean.Parse(UserPreferences.getValue("UseVersion", Sections.UserPreferences, False)) Then
                If sbCondition.Length > 0 Then
                    sbCondition.Remove(sbCondition.Length - 1, 1)
                    sbCondition.Append(" AND Version = 0)")
                Else
                    sbCondition.Append(" (Version = 0) ")
                End If
                'End If

                'Si tiene doc_id no lo traigo ya que es el mismo documento
                If ExcludedParentDocId > 0 Then
                    If sbCondition.Length > 0 Then
                        sbCondition.Remove(sbCondition.Length - 1, 1)
                        sbCondition.Append(" AND ")
                        sbCondition.Append(strTable)
                        sbCondition.Append(".DOC_ID <> ")
                        sbCondition.Append(ExcludedParentDocId)
                        sbCondition.Append(" ) ")
                    Else
                        sbCondition.Append(" ( ")
                        sbCondition.Append(strTable)
                        sbCondition.Append(".DOC_ID <>")
                        sbCondition.Append(ExcludedParentDocId)
                        sbCondition.Append(" ) ")
                    End If
                End If

                If Not InsertFilterRestrictionConditionString(DocType, sbCondition, sbClose, CurrentUserId, FC, sbSelect, sbDate, UseFilters, filterType, sbRestriction) Then
                    Return New DataTable
                    'Else
                    '    sbSelect = ReplaceTableName(strTable, sbSelect)
                End If

                If Not IsNothing(sbSelectTotal) Then
                    If InsertFilterRestrictionConditionString(DocType, sbCondition, sbClose, CurrentUserId, FC, sbSelectTotal, sbDate, UseFilters, filterType, sbRestriction) = False Then
                        Return New DataTable
                        'Else
                        '    sbSelectTotal = ReplaceTableName(strTable, sbSelectTotal)
                    End If
                End If

                Dim dt As New DataTable
                sbSelect = ReplaceTableName(strTable, sbSelect)
                sbSelectTotal = ReplaceTableName(strTable, sbSelectTotal)
                searchSql = sbSelect.ToString
                countSearchSql = sbSelectTotal.ToString
                dt = GetTaskTable(searchSql, OrderBy, PageSize, LastPage, countSearchSql, Nothing)

                Return dt

            Catch ex As Exception
                ZClass.raiseerror(ex)

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

        Private Shared Function ReplaceTableName(strTable As String, query As StringBuilder) As StringBuilder
            If query.ToString.Contains("strTable") Then
                query = query.Replace("strTable", strTable)
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
                                                                       ByVal Orderstring As StringBuilder,
                                                                       ByVal CurrentUserId As Long,
                                                                       ByVal FC As FiltersComponent,
                                                                       ByRef strselect As StringBuilder,
                                                                       ByRef dateDeclarationString As StringBuilder,
                                                                       ByVal UseFilters As Boolean,
                                                                       ByVal FilterType As FilterTypes,
                                                                       ByVal RestrictionString As StringBuilder) As Boolean
            If CurrentUserId > 0 Then

                Dim FilterString As String = String.Empty
                Dim Filters As New Generic.List(Of IFilterElem)

                If UseFilters Then
                    'Se verifican los permisos de quitar y deshabilitar los filtros por defecto del usuario
                    Dim removeDefaultFilters As Boolean = UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.RemoveDefaultFilters, DocType.ID)
                    Dim disableDefaultFilters As Boolean = UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.DisableDefaultFilters, DocType.ID)

                    'Si es documental o si es tarea y no tiene ninguno de los dos se procede a cargarlos
                    If FilterType <> FilterTypes.Task OrElse
                        (FilterType = FilterTypes.Task AndAlso Not removeDefaultFilters AndAlso Not disableDefaultFilters) Then

                        Filters = FC.GetLastUsedFilters(DocType.ID, CurrentUserId, FilterType)
                        'For Each f As IFilterElem In Filters
                        '    If f.Enabled = True Then ' AndAlso strselect.ToString().ToLower().Contains(f.Filter().ToLower()) = False Then
                        '        Return False
                        '    End If
                        'Next

                        Dim isCaseSensitive As Boolean = UserPreferences.getValue("CaseSensitive", Sections.UserPreferences, True)
                        'Se cargan unicamente los filtros por defecto, los manuales no son mostrados
                        If FilterType = FilterTypes.Task Then
                            FilterString = FC.GetFiltersString(Filters, True, isCaseSensitive)
                        Else
                            FilterString = FC.GetFiltersString(Filters, False, isCaseSensitive)
                        End If
                    End If
                End If

                If RestrictionString.Length > 0 Then
                    If strselect.ToString.ToLower().Contains("where") = False Then strselect.Append(" WHERE ")
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
        ''' <param name="FlagCase"></param>
        ''' <param name="ColumCondstring"></param>
        ''' <param name="First">Bandera que indica si es la 1era vez</param>
        ''' <history>   Marcelo    Created     20/08/09
        '''             Javier     Modified    12/10/10 Corrobora si es filtro usa el nombre de la campo para armar la condicion
        ''' </history>
        ''' <remarks></remarks>
        Public Shared Sub CreateWhereSearch(ByRef valueString As StringBuilder, ByVal Indexs As List(Of IIndex), ByRef i As Int64, ByRef FlagCase As Boolean, ByRef ColumCondstring As StringBuilder, ByRef First As Boolean, ByRef dateDeclarationString As StringBuilder, ByVal IsRestriction As Boolean)
            Dim Valuestring1 As Object = Nothing
            Dim Valuestring3 As Object = Nothing
            Dim dateString As New StringBuilder

            valueString.Remove(0, valueString.Length)
            valueString.Append(Indexs(i).Data)

            Dim IndexColumnName As String
            IndexColumnName = Indexs(i).Column
            Dim IndexName As String
            IndexName = Indexs(i).Name

            If valueString.Length <> 0 OrElse Indexs(i).[Operator].ToLower = "es nulo" Then
                If Indexs(i).[Operator] <> "SQL" Then
                    If valueString.ToString.Split(";").Length > 1 Then
                        If IsRestriction = False Then
                            Select Case Indexs(i).Type
                                Case IndexDataType.Alfanumerico
                                    FlagCase = True
                                    Valuestring1 = "'" & LCase(valueString.Replace(";", "';'").ToString) & "'"
                                Case IndexDataType.Alfanumerico_Largo
                                    FlagCase = True
                                    Valuestring1 = "'" & LCase(valueString.Replace(";", "';'").ToString) & "'"
                            End Select
                        Else
                            Valuestring1 = valueString.ToString()

                        End If

                    Else
                        Select Case Indexs(i).Type
                            Case IndexDataType.Numerico, IndexDataType.Numerico_Largo

                                If valueString.Length <> 0 Then
                                    Valuestring1 = valueString.ToString()
                                Else
                                    Valuestring1 = valueString.ToString
                                End If


                                FlagCase = False
                            Case IndexDataType.Numerico_Decimales, IndexDataType.Moneda
                                If valueString.Length <> 0 Then
                                    If System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString = "," Then valueString = valueString.Replace(".", ",")
                                    Valuestring1 = CDec(valueString.ToString)
                                    Valuestring1 = Valuestring1.ToString.Replace(",", ".")
                                End If
                                FlagCase = False
                            Case IndexDataType.Si_No
                                If valueString.Length <> 0 Then
                                    Valuestring1 = Int64.Parse(valueString.ToString)
                                End If
                            Case IndexDataType.Fecha
                                If Not String.IsNullOrEmpty(valueString.ToString.Trim) Then
                                    'If Server.isSQLServer Then
                                    '    'Se optimiza para sql server
                                    '    Valuestring1 = "@fecdesde" & Indexs(i).Column
                                    '    dateString.Append("declare " & Valuestring1 & " datetime ")
                                    '    dateString.Append("set " & Valuestring1 & " = " & Server.Con.ConvertDate(valueString.ToString) & " ")
                                    '    dateDeclarationString.Append(dateString)
                                    'Else
                                    '    Valuestring1 = Server.Con.ConvertDate(valueString.ToString)
                                    'End If
                                    Valuestring1 = Server.Con.ConvertDate(valueString.ToString)
                                End If
                            Case IndexDataType.Fecha_Hora
                                If Not String.IsNullOrEmpty(valueString.ToString.Trim) Then
                                    'If Server.isSQLServer Then
                                    '    'Se optimiza para sql server
                                    '    Valuestring1 = "@fecdesde" & Indexs(i).Column
                                    '    dateString.Append("declare " & Valuestring1 & " datetime ")
                                    '    dateString.Append("set " & Valuestring1 & " = " & Server.Con.ConvertDateTime(valueString.ToString) & " ")
                                    '    dateDeclarationString.Append(dateString)
                                    'Else
                                    '    Valuestring1 = Server.Con.ConvertDateTime(valueString.ToString)
                                    'End If
                                    Valuestring1 = Server.Con.ConvertDateTime(valueString.ToString)
                                End If
                            Case IndexDataType.Alfanumerico, IndexDataType.Alfanumerico_Largo
                                If IsRestriction = False Then

                                    If FlagCase Then
                                        Valuestring1 = "'" & LCase(valueString.ToString) & "'"

                                    Else
                                        Valuestring1 = "'" & valueString.ToString & "'"
                                    End If
                                Else
                                    Valuestring1 = valueString.ToString()
                                End If

                        End Select
                    End If
                Else
                    Valuestring1 = Indexs(i).Data
                End If

                Dim Op As String = Valuestring1.ToString
                If Op.Contains("''") Then Op = Op.Replace("''", "'")
                If Op.Contains(")'") Then Op = Op.Replace(")'", ")")
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
                        ElseIf Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                            ColumCondstring.Append(" (")
                            If IsNumeric(Trim(Indexs(i).dataDescription)) Then
                                ColumCondstring.Append(IndexColumnName & " = " & Trim(Valuestring1) & ") ")
                            Else
                                ColumCondstring.Append(If(Server.isOracle, "NVL", "ISNULL"))
                                ColumCondstring.Append("(slst_s" & Indexs(i).ID & ".descripcion, '')")
                                ColumCondstring.Append(" = '" & Trim(Indexs(i).dataDescription) & "')")
                            End If
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
                                            'If Server.isSQLServer Then
                                            '    'Se optimiza para sql server
                                            '    Valuestring3 = "@fechasta" & IndexColumnName
                                            '    dateString.Remove(0, dateString.Length)
                                            '    dateString.Append("declare " & Valuestring3 & " datetime ")
                                            '    dateString.Append("set " & Valuestring3 & " = " & Server.Con.ConvertDate(Indexs(i).Data2) & " ")
                                            '    dateDeclarationString.Append(dateString)
                                            'Else
                                            '    Valuestring3 = Server.Con.ConvertDate(Indexs(i).Data2)
                                            'End If
                                            Valuestring3 = Server.Con.ConvertDate(Indexs(i).Data2)
                                        End If
                                        Data2Added = True
                                    End If
                                Case 5
                                    If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                        'Valuestring3 = Server.Con.ConvertDateTime(Indexs(i).Data2)
                                        If Not String.IsNullOrEmpty(valueString.ToString.Trim) Then
                                            'If Server.isSQLServer Then
                                            '    'Se optimiza para sql server
                                            '    Valuestring3 = "@fechasta" & IndexColumnName
                                            '    dateString.Remove(0, dateString.Length)
                                            '    dateString.Append("declare " & Valuestring3 & " datetime ")
                                            '    dateString.Append("set " & Valuestring3 & " = " & Server.Con.ConvertDateTime(Indexs(i).Data2) & " ")
                                            '    dateDeclarationString.Append(dateString)
                                            'Else
                                            '    Valuestring3 = Server.Con.ConvertDateTime(Indexs(i).Data2)
                                            'End If
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

                        While Valuestring1.Contains("  ")
                            Valuestring1 = Valuestring1.Replace("  ", " ")
                        End While
                        Valuestring1 = Valuestring1.Trim.Replace(" ", "%")

                        If (Indexs(i).DropDown = IndexAdditionalType.AutoSustitución OrElse (Indexs(i).DropDown = IndexAdditionalType.AutoSustituciónJerarquico)) Then
                            ColumCondstring.Append(" (")
                            If FlagCase Then ColumCondstring.Append("lower(")
                            ColumCondstring.Append(If(Server.isOracle, "NVL", "ISNULL"))
                            ColumCondstring.Append("(slst_s" & Indexs(i).ID & ".descripcion, '')")
                            If FlagCase Then ColumCondstring.Append(")")
                            ColumCondstring.Append(" Like '%" & Replace(Replace(Replace(Trim(Indexs(i).dataDescription), "'", String.Empty), "  ", " "), " ", "%") & "%')")
                            ColumCondstring.Append(" OR (" & IndexColumnName & " Like '%" & Replace(Replace(Replace(Trim(Valuestring1), "'", String.Empty), "  ", " "), " ", "%") & "%') ")
                        Else
                            If FlagCase = True Then
                                ColumCondstring.Append(" (lower(" & IndexColumnName & ") Like '%" & Replace(Replace(Replace(Trim(Valuestring1), "'", String.Empty), "  ", " "), " ", "%") & "%')")
                            Else
                                ColumCondstring.Append(" (" & IndexColumnName & " Like '%" & Replace(Replace(Replace(Trim(Valuestring1), "'", String.Empty), "  ", " "), " ", "%") & "%')")
                            End If

                        End If

                    Case "Empieza"
                        If FlagCase = True Then
                            ColumCondstring.Append(" (lower(" & IndexColumnName & ") Like '" & Replace(Trim(Valuestring1), "'", String.Empty) & "%')")
                        Else
                            ColumCondstring.Append(" (" & IndexColumnName & " Like '" & Replace(Trim(Valuestring1), "'", String.Empty) & "%')")
                        End If
                    Case "Termina"
                        If FlagCase = True Then
                            ColumCondstring.Append(" (lower(" & IndexColumnName & ") Like '%" & Replace(Trim(Valuestring1), "'", String.Empty) & "')")
                        Else
                            ColumCondstring.Append(" (" & IndexColumnName & " Like '%" & Replace(Trim(Valuestring1), "'", String.Empty) & "')")
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
                                        somestring = " (lower(" & IndexColumnName & ") " & Op & " ('%" & Val.Replace("'", String.Empty) & "%')"
                                    Else
                                        somestring = " (" & IndexColumnName & " " & Op & " ('%" & Val.Replace("'", String.Empty) & "%')"
                                    End If
                                ElseIf x > 0 Then
                                    If String.IsNullOrEmpty(somestring) Then
                                        If FlagCase = True Then
                                            somestring &= separator & " lower(" & DirectCast(IndexColumnName, String) & ") " & Op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                                        Else
                                            somestring &= separator & " " & DirectCast(IndexColumnName, String) & " " & Op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                                        End If
                                    Else
                                        separator = " or "
                                        If FlagCase = True Then
                                            somestring &= separator & " lower(" & IndexColumnName & ") " & Op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                                        Else
                                            somestring &= separator & " " & IndexColumnName & " " & Op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
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
                                        somestring = " (lower(" & IndexColumnName & ") " & Op & " ('%" & Val.Replace("'", String.Empty) & "%')"
                                    Else
                                        somestring = " (" & IndexColumnName & " " & Op & " ('%" & Val.Replace("'", String.Empty) & "%')"
                                    End If
                                ElseIf x = 0 Then
                                    If FlagCase = True Then
                                        somestring = " (lower(" & IndexColumnName & ") " & Op & " ('%" & Val.Replace("'", String.Empty) & "%')"
                                    Else
                                        somestring = " (" & IndexColumnName & " " & Op & " ('%" & Val.Replace("'", String.Empty) & "%')"
                                    End If
                                Else
                                    If String.IsNullOrEmpty(somestring) Then
                                        If FlagCase = True Then
                                            somestring &= separator & " lower(" & DirectCast(IndexColumnName, String) & ") " & Op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                                        Else
                                            somestring &= separator & " " & DirectCast(IndexColumnName, String) & " " & Op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                                        End If
                                    Else
                                        separator = " or "
                                        If FlagCase = True Then
                                            somestring &= separator & " lower(" & IndexColumnName & ") " & Op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                                        Else
                                            somestring &= separator & " " & IndexColumnName & " " & Op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
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
                            ColumCondstring.Append(" (lower(" & IndexColumnName & ") in (" & Valuestring1 & "'))")
                        Else
                            ColumCondstring.Append(" (" & IndexColumnName & " in (" & Valuestring1 & "))")
                        End If

                    Case "SQL"
                        ColumCondstring.Append(" (" & IndexColumnName & " in (" & Valuestring1 & "))")

                End Select
                Op = Nothing
                separator = Nothing
            End If
        End Sub


        ''' <summary>
        ''' Armado del select que se utiliza para las busquedas
        ''' </summary>
        ''' <param name="strTable">Nombre de la tabla. Ej: doc58</param>
        ''' <param name="Indexs">Atributos que se van a traer</param>
        ''' <returns>Stringbuilder con la consulta</returns>
        ''' <history>   Marcelo    Created     20/08/09</history>
        ''' <history> Ivan 13/01/16 - Agregue parametro GetCount para saber si la consulta es por la cantidad de filas.</history>
        ''' <remarks></remarks>
        Private Shared Function createSelectSearch(ByVal strTable As String, ByVal Indexs As List(Of IIndex), ByVal VisibleIndexs As List(Of Int64), ByVal GetCount As Boolean, ByVal order As String, Optional ByVal NonvisibleIndexs As List(Of Int64) = Nothing, Optional ByVal fromSearchVersions As Boolean = False) As StringBuilder
            Dim strselect As New System.Text.StringBuilder
            Dim strselectForRowNum As StringBuilder = New StringBuilder()
            Dim auIndex As New List(Of Int64)

            If GetCount Then

                strselect.Append("Select count(1) ")
                'strselect.Append(" FROM ")
                'strselect.Append(strTable)
                'strselect.Append(" inner join doc_type on doc_type.doc_type_id = ")
                'strselect.Append(strTable)
                'strselect.Append(".doc_type_id ")

                For Each _Index As Index In Indexs
                    If _Index.DropDown = IndexAdditionalType.AutoSustitución _
                          Or _Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                        auIndex.Add(_Index.ID)
                    End If
                Next

            Else

                strselect.Append("SELECT ")
                strselect.Append(strTable)
                strselect.Append(".DOC_ID, ")
                strselect.Append(GridColumns.TASK_ID_COLUMNNAME)
                strselect.Append(", ")
                strselect.Append(strTable)
                strselect.Append(".DOC_TYPE_ID, ")
                strselect.Append("s.step_Id, ")
                strselect.Append("Do_State_ID, ")
                strselect.Append("WF.work_id, ")
                strselect.Append("wd.Task_State_ID, ")
                strselect.Append("user_asigned, ")
                strselect.Append("doc_type.doc_type_name As ")
                strselect.Append(Chr(34))
                strselect.Append(GridColumns.DOC_TYPE_NAME_COLUMNNAME)
                strselect.Append(Chr(34))
                strselect.Append(", ")
                strselect.Append(strTable)
                strselect.Append(".NAME As ")
                strselect.Append(Chr(34))
                strselect.Append(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME)
                strselect.Append(Chr(34))
                strselect.Append(", ")
                strselect.Append(If(Server.isOracle, "NVL(WF.NAME,'')", "ISNULL(WF.NAME,'')"))
                strselect.Append(" As ")
                strselect.Append(Chr(34))
                strselect.Append(GridColumns.WORKFLOW_COLUMNAME)
                strselect.Append(Chr(34))
                strselect.Append(", ")
                strselect.Append(If(Server.isOracle, "NVL(S.NAME,'')", "ISNULL(S.NAME,'')"))
                strselect.Append(" As ")
                strselect.Append(Chr(34))
                strselect.Append(GridColumns.ETAPA_COLUMNAME)
                strselect.Append(Chr(34))
                strselect.Append(", ")
                strselect.Append(If(Server.isOracle, "NVL(SS.NAME,'')", "ISNULL(SS.NAME,'')"))
                strselect.Append(" As ")
                strselect.Append(Chr(34))
                strselect.Append(GridColumns.STATE_COLUMNNAME)
                strselect.Append(Chr(34))
                strselect.Append(", ")
                strselect.Append(If(Server.isOracle, "NVL(TS.Task_State_NAME,'')", "ISNULL(TS.Task_State_NAME,'')"))
                strselect.Append(" As ")
                strselect.Append(Chr(34))
                strselect.Append(GridColumns.SITUACION_COLUMNNAME)
                strselect.Append(Chr(34))
                strselect.Append(", ")
                strselect.Append(If(Server.isOracle, "NVL(U.NAME,'')", "ISNULL(U.NAME,'')"))
                strselect.Append(" As ")
                strselect.Append(Chr(34))
                strselect.Append(GridColumns.ASIGNADO_COLUMNNAME)
                strselect.Append(Chr(34))
                strselect.Append(", ")
                strselect.Append(strTable)
                strselect.Append(".DISK_GROUP_ID, PLATTER_ID, VOL_ID, DOC_FILE, OFFSET, ")
                strselect.Append(strTable)
                strselect.Append(".ICON_ID, SHARED, ver_Parent_id, RootId, ")

                If Server.isOracle Then
                    strselect.Append("get_filename(original_Filename)")
                Else
                    strselect.Append("REVERSE(SUBSTRING(REVERSE(original_Filename), 0, CHARINDEX('\', REVERSE(original_Filename))))")
                End If

                strselect.Append(" as ")
                strselect.Append(Chr(34))
                strselect.Append(GridColumns.ORIGINAL_FILENAME_COLUMNNAME)
                strselect.Append(Chr(34))
                strselect.Append(",Version, NumeroVersion as ")
                strselect.Append(Chr(34))
                strselect.Append(GridColumns.NUMERO_DE_VERSION_COLUMNNAME)
                strselect.Append(Chr(34))
                strselect.Append(",disk_Vol_id, DISK_VOL_PATH")
                strselect.Append(",")
                strselect.Append(strTable)
                strselect.Append(".crdate as ")
                strselect.Append(Chr(34))
                strselect.Append(GridColumns.CRDATE_COLUMNNAME)
                strselect.Append(Chr(34))
                strselect.Append(", ")
                strselect.Append(strTable)
                strselect.Append(".lupdate as ")
                strselect.Append(Chr(34))
                strselect.Append(GridColumns.LASTUPDATE_COLUMNNAME)
                strselect.Append(Chr(34))


                strselectForRowNum.Append("SELECT ")
                strselectForRowNum.Append("DOC_ID, ")
                strselectForRowNum.Append(GridColumns.TASK_ID_COLUMNNAME)
                strselectForRowNum.Append(", ")
                strselectForRowNum.Append("DOC_TYPE_ID, step_Id, Do_State_ID, ")
                strselectForRowNum.Append("work_id, Task_State_ID, user_asigned, ")
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(GridColumns.DOC_TYPE_NAME_COLUMNNAME)
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(", ")
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME)
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(",")
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(GridColumns.WORKFLOW_COLUMNAME)
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(",")
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(GridColumns.ETAPA_COLUMNAME)
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(",")
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(GridColumns.STATE_COLUMNNAME)
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(",")
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(GridColumns.SITUACION_COLUMNNAME)
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(",")
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(GridColumns.ASIGNADO_COLUMNNAME)
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(", ")
                strselectForRowNum.Append("DISK_GROUP_ID, PLATTER_ID, VOL_ID, DOC_FILE, OFFSET, ICON_ID,SHARED, ver_Parent_id, RootId, ")
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(GridColumns.ORIGINAL_FILENAME_COLUMNNAME)
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(",")
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(GridColumns.NUMERO_DE_VERSION_COLUMNNAME)
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(",disk_Vol_id, DISK_VOL_PATH")
                strselectForRowNum.Append(",")
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(GridColumns.CRDATE_COLUMNNAME)
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(", ")
                strselectForRowNum.Append(Chr(34))
                strselectForRowNum.Append(GridColumns.LASTUPDATE_COLUMNNAME)
                strselectForRowNum.Append(Chr(34))


                For Each _Index As Index In Indexs
                    If (IsNothing(VisibleIndexs) OrElse VisibleIndexs.Contains(_Index.ID)) AndAlso (IsNothing(NonvisibleIndexs) OrElse Not NonvisibleIndexs.Contains(_Index.ID)) Then
                        strselect.Append(",")
                        strselectForRowNum.Append(",")
                        strselect.Append(strTable)
                        strselect.Append(".I")
                        strselect.Append(_Index.ID)
                        If _Index.DropDown = IndexAdditionalType.AutoSustitución _
                            Or _Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                            strselectForRowNum.Append("I")
                            strselectForRowNum.Append(_Index.ID)
                            strselectForRowNum.Append(",")
                            strselect.Append(", ")
                            strselect.Append(If(Server.isOracle, "NVL(", "ISNULL("))
                            strselect.Append("slst_s" & _Index.ID & ".descripcion, '')")
                            auIndex.Add(_Index.ID)
                        End If
                        strselect.Append(" as ")
                        strselect.Append(Chr(34))
                        strselect.Append(_Index.Name)
                        strselect.Append(Chr(34))

                        strselectForRowNum.Append(Chr(34))
                        strselectForRowNum.Append(_Index.Name)
                        strselectForRowNum.Append(Chr(34))
                    End If
                Next

                If Not fromSearchVersions Then
                    If Server.isOracle Then
                        strselect.Insert(0, ", ROWNUM as RNUM from (")
                    Else
                        If String.IsNullOrEmpty(order) Then order = "doc_id asc"
                        strselect.Insert(0, ", ROW_NUMBER() OVER (ORDER  BY " & order & ") RNUM from (")
                    End If
                    strselect.Insert(0, strselectForRowNum.ToString())
                End If

            End If

            strselect.Append(" FROM ")
            strselect.Append(strTable)
            strselect.Append(" inner join doc_type on doc_type.doc_type_id = ")
            strselect.Append(strTable)
            strselect.Append(".doc_type_id")

            strselect.Append(" left join disk_Volume on disk_Vol_id=vol_id ")

            If auIndex.Count > 0 Then
                If Server.isOracle AndAlso GetCount Then
                    For Each indiceID As Int64 In auIndex
                        strselect.Append(" left join slst_s" & indiceID & " on NVL(" & strTable & ".i" & indiceID & ", 0) = slst_s" & indiceID & ".codigo ")
                    Next
                Else
                    For Each indiceID As Int64 In auIndex
                        strselect.Append(" left join slst_s" & indiceID & " on " & strTable & ".i" & indiceID & " = slst_s" & indiceID & ".codigo ")
                    Next
                End If
            End If

            'realizo un inner join con la WFdocument para agregar las
            'columnas faltantes al realizar busquedas en tareas
            strselect.Append("left join wfdocument wd ON ")
            strselect.Append(strTable)
            strselect.Append(".DOC_ID = wd.DOC_ID ")
            strselect.Append("left join wfstep S on wd.step_id=S.step_id ")
            strselect.Append("left join wfworkflow wf on wf.work_id=S.work_id ")
            strselect.Append("left join WFStepStates SS on wd.do_state_id=SS.doc_state_id ")
            strselect.Append("left join WFTask_States TS on wd.Task_State_ID=TS.Task_State_ID ")
            strselect.Append("left join usrtable U on wd.User_Asigned=U.id ")

            auIndex = Nothing
            Return strselect
        End Function

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
            Dim pageSize As Int64 = Int64.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100))

            Dim dt As DataTable = DoSearch(search, currentUserId, Nothing, 0, pageSize, False, False, FilterTypes.Document, False, Nothing, False, False)
            RaiseEvent ShowResults(dt, Ls.Name, search, False)

            UserBusiness.Rights.SaveAction(2, ObjectTypes.Documents, Zamba.Core.RightsType.Buscar, "Se realizo busqueda en ultimas busquedas:  " & Ls.Name)

        End Sub

        Public Overloads Shared Function SearchRows(ByVal SQL As String, ByVal SQLCount As String, ByVal lastPage As Int32) As DataTable
            Dim CountTop As Int64 = Int64.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100))
            Dim dt As DataTable = GetTaskTable(SQL, String.Empty, CountTop, lastPage, SQLCount, Nothing)
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
                ZClass.raiseerror(ex)
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
                        Zamba.Core.ZClass.raiseerror(ex)
                    End Try

                Next
                NewResults.Capacity = CountRows

                Return NewResults
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
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
            Catch ex As System.IO.IOException
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
                        Zamba.Core.ZClass.raiseerror(ex)
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
                Zamba.Core.ZClass.raiseerror(ex)
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
                Dim SelectQuery As String = "SELECT Doc_Id FROM Doc_notes where (lower(Note_text) Like '%" & SearchString.Trim.ToLower & "%')"
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
                Zamba.Core.ZClass.raiseerror(ex)
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
                Zamba.Core.ZClass.raiseerror(ex)
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
                Zamba.Core.ZClass.raiseerror(ex)
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
                Zamba.Core.ZClass.raiseerror(ex)
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
                    Result.DocType = New DocType(Result.DocType.ID, Result.DocType.Name, 0)
                End If

                Return SearchVersions(Result.DocType, Result.RootDocumentId, Result.ID)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
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
        Public Shared Function SearchVersions(ByVal docType As DocType, ByVal rootDocumentId As Int64, ByVal docId As Int64) As DataTable
            Try
                Dim TableName As String = "Doc" & docType.ID.ToString()
                Dim Query As New System.Text.StringBuilder()
                Query.Append(createSelectSearch(TableName, docType.Indexs, Nothing, False, String.Empty, Nothing, True))
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

                Dim CountTop As Int64 = Int64.Parse(UserPreferences.getValue("CantidadFilas", Sections.UserPreferences, 100))

                Dim ds As DataSet = Nothing
                Dim dt As DataTable = Nothing
                Dim con As IConnection = Nothing
                Try
                    con = Server.Con
                    ds = con.ExecuteDataset(CommandType.Text, Query.ToString)

                    If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing Then
                        dt = ds.Tables(0)
                        Dim arraySustIndex As New ArrayList
                        Dim i As Int32 = 0
                        Dim first As Boolean = True
                        For Each r As DataRow In dt.Rows
                            CreateRow(arraySustIndex, r, docType.Indexs)
                        Next
                    End If

                Finally
                    If Not IsNothing(con) Then
                        con.Close()
                        con.dispose()
                        con = Nothing
                    End If
                End Try
                Return dt
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
            Return Nothing
        End Function
#End Region
        Public Overloads Shared Function SearchByFolderId(ByVal resultId As Int64, ByVal DocType As DocType, ByVal FolderId As Int64, ByVal user As IUser) As ArrayList
            Dim Dr As IDataReader = Nothing
            Dim Con1 As IConnection = Nothing
            Dim First As Boolean = True
            Dim docTypeId As String = DocType.ID.ToString
            Try

                Dim strselect As New System.Text.StringBuilder
                strselect.Append("SELECT DOC" & docTypeId & ".* FROM DOC" & docTypeId)
                strselect.Append(" where FOLDER_ID = ")
                strselect.Append(FolderId.ToString())
                strselect.Append(" and doc_id <> ")
                strselect.Append(resultId.ToString())

                Con1 = Server.Con(True, False, True)
                Dr = Con1.ExecuteReader(CommandType.Text, strselect.ToString)

                Dim Results As New ArrayList

                strselect.Remove(0, strselect.Length)
                strselect = Nothing

                Dim CountRows As Int32
                Dim FirstTime As Boolean = True
                Dim indexOrder(DocType.Indexs.Count - 1) As Int16
                Dim onumeroversion, ocrdate, olupdate, oname, ofolderid, ODOCID, oiconid, odiskgroupid, odocfile, ooffset, overparentid, oversion, orootid, ooriginalfilename, ouserid As Int16
                Dim firstloop As Boolean = True
                While Dr.Read
                    If firstloop Then
                        ofolderid = Dr.GetOrdinal("FOLDER_ID")
                        ODOCID = Dr.GetOrdinal("DOC_ID")
                        oiconid = Dr.GetOrdinal("ICON_ID")
                        odocfile = Dr.GetOrdinal("DOC_FILE")
                        ooffset = Dr.GetOrdinal("OFFSET")
                        ooriginalfilename = Dr.GetOrdinal("ORIGINAL_FILENAME")
                        orootid = Dr.GetOrdinal("ROOTID")
                        oversion = Dr.GetOrdinal("VERSION")
                        overparentid = Dr.GetOrdinal("VER_PARENT_ID")
                        odiskgroupid = Dr.GetOrdinal("DISK_GROUP_ID")
                        onumeroversion = Dr.GetOrdinal("NUMEROVERSION")
                        ocrdate = Dr.GetOrdinal("CRDATE")
                        olupdate = Dr.GetOrdinal("LUPDATE")
                        ouserid = Dr.GetOrdinal("PLATTER_ID")

                        Try
                            oname = Dr.GetOrdinal("Name")
                        Catch
                            oname = Dr.GetOrdinal("Nombre")
                        End Try
                        firstloop = False
                    End If

                    Try
                        Dim VOLPATH As String
                        Try
                            VOLPATH = ZCore.filterVolumes(Dr.GetInt32(odiskgroupid)).path
                        Catch ex As Exception
                            VOLPATH = String.Empty
                        End Try
                        Dim Original_filename As String
                        Try
                            Original_filename = Dr.GetString(ooriginalfilename)
                        Catch ex As Exception
                            Original_filename = String.Empty
                        End Try
                        'TODO: hacer que en el insert NumeroVersion sea = 0
                        Dim NumeroVersion As Int32
                        Try
                            NumeroVersion = Dr.GetInt32(onumeroversion)
                        Catch
                            NumeroVersion = 0
                        End Try
                        Dim CRDATE, LUPDATE As Date
                        Try
                            If IsDBNull(Dr.GetDateTime(ocrdate)) Then
                                CRDATE = Now
                            Else
                                CRDATE = Dr.GetDateTime(ocrdate)
                            End If
                        Catch ex As Exception
                            CRDATE = Now
                        End Try
                        Try
                            If IsDBNull(Dr.GetDateTime(olupdate)) Then
                                LUPDATE = Now
                            Else
                                LUPDATE = Dr.GetDateTime(olupdate)
                            End If
                        Catch ex As Exception
                            LUPDATE = Now
                        End Try
                        Dim nombre As String
                        Try
                            nombre = Dr.GetString(oname)
                        Catch ex As Exception
                            nombre = DocType.Name
                        End Try

                        Dim DOCFILE As String
                        Try
                            DOCFILE = Dr.GetString(odocfile).Trim
                        Catch ex As Exception
                            DOCFILE = String.Empty
                        End Try
                        Dim Folder_Id As Int64 = CInt(Dr.GetValue(ofolderid))
                        Dim Result As New Result(Dr.GetValue(ODOCID), DocType, nombre, Dr.GetInt32(oiconid), Folder_Id, CountRows, Dr.GetInt32(odiskgroupid), DOCFILE, Dr.GetInt32(ooffset), VOLPATH, DocType.Name, CRDATE, LUPDATE, Dr.GetValue(overparentid), Dr.GetInt32(oversion), Dr.GetValue(orootid), Original_filename, NumeroVersion)
                        Result.Parent = DocType
                        Dim t As Int16
                        If FirstTime Then
                            For t = 0 To DocType.Indexs.Count - 1
                                Try
                                    Dim Ind As New Index(DocType.Indexs(t))
                                    Dim Ordinal As Int16 = Dr.GetOrdinal("I" & Ind.ID)
                                    If IsDBNull(Dr.GetValue(Ordinal)) = False Then
                                        Ind.Data = Dr.GetValue(Ordinal)
                                    End If
                                    Result.Indexs.Add(Ind)
                                    indexOrder.SetValue(Ordinal, t)
                                Catch ex As Exception
                                    ZClass.raiseerror(ex)
                                End Try
                                FirstTime = False
                            Next
                        Else
                            For t = 0 To DocType.Indexs.Count - 1
                                Try
                                    Dim Ind As New Index(DocType.Indexs(t))
                                    Dim ordinal As Int16 = indexOrder(t)
                                    If IsDBNull(Dr.GetValue(ordinal)) = False Then
                                        Ind.Data = Dr.GetValue(ordinal)
                                    End If
                                    Result.Indexs.Add(Ind)
                                Catch ex As Exception
                                    ZClass.raiseerror(ex)
                                End Try
                            Next
                        End If

                        Result.OwnerID = Dr.GetValue(ouserid)

                        Results.Add(Result)
                        CountRows += +1

                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End While


                If IsNothing(Dr) = False Then
                    Try
                        Con1.Command.Cancel()
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    Dr.Close()
                    Dr.Dispose()
                    Dr = Nothing
                    Try
                        Con1.dispose()
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                End If

                Results.Capacity = CountRows
                Return Results
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally

                If IsNothing(Dr) = False Then
                    Try
                        Con1.Command.Cancel()
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                    Dr.Close()
                    Dr.Dispose()
                    Dr = Nothing
                End If
                Try
                    If Not IsNothing(Con1) Then
                        Con1.Close()
                        Con1.dispose()
                        Con1 = Nothing
                    End If
                Catch ex As Exception
                    ZClass.raiseerror(ex)
                End Try
            End Try
            Return Nothing
        End Function
        Public Overrides Sub Dispose()

        End Sub
    End Class
End Namespace
