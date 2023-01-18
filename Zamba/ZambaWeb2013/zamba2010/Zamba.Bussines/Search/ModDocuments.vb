Imports System.IO
Imports Zamba.Filters
Imports Zamba.Servers
Imports System.Data
Imports System.Collections
Imports Zamba.Data
Imports System.Text
Imports System.Collections.Generic
Imports Zamba.Framework.Search.Components
Imports Zamba.Framework.Search.Data
Imports Zamba.Core.DocTypes.DocAsociated
Imports Zamba.Framework
Imports System.Reflection
Imports Microsoft.Office.Core

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
        'Inherits ZClass

        Dim RB As New Results_Business
        Dim UB As New UserBusiness
        Dim MisTareasSinFiltro As Boolean = False


#Region "IndexSearch"
        Public Overloads Function DoSearchAsocWithWF(ByVal CurrentDocType As DocType, ByVal Indexs As List(Of IIndex), ByVal UserId As Int64, ByVal LastPage As Int64, ByVal PageSize As Int32, ByVal result As IResult, ByVal currentUserName As String) As DataTable
            Dim dtResults As New DataTable
            Try
                Dim i As Int32
                dtResults.MinimumCapacity = 0

                Dim MD As New ModDocuments
                Dim dt As DataTable = MD.SearchRowsAsocForm(CurrentDocType, Indexs, UserId, LastPage, PageSize, result, currentUserName)
                MD = Nothing
                If dt IsNot Nothing Then
                    dtResults.MinimumCapacity = dtResults.MinimumCapacity + dt.MinimumCapacity
                    dtResults.Merge(dt)
                End If

                Return dtResults
            Catch ex As Threading.SynchronizationLockException
                ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadAbortException
                ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadInterruptedException
                ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadStateException
                ZClass.raiseerror(ex)
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Return New DataTable()
        End Function

        Public Overloads Function DoSearch(ByRef search As Searchs.Search,
                                           ByVal UserId As Int64,
                                           ByVal LastPage As Int64,
                                           ByVal PageSize As Int32,
                                           ByVal isAssociatedSearch As Boolean,
                                           ByVal UseFilters As Boolean,
                                           ByVal DtoOnly As Boolean, ByRef TotalCount As Int64, onlyImportants As Boolean) As DataTable
            Dim dtResults As New DataTable
            Dim SearchName As String = String.Empty

            Dim SearchsEntities As New List(Of Int64)
            If IsNothing(search.Doctypes) Then
                Return dtResults
            End If
            For Each DocType As IDocType In search.Doctypes
                SearchsEntities.Add(DocType.ID)
            Next

            ' Esto lo pongo porque si viene desde alguna llamada donde no se setea el view rompe mas adelante, como los docs asociados.
            If search.View Is Nothing Then search.View = String.Empty

            If search.View = "MyTasks,MyTeam,MyAllTeam" Then
                MisTareasSinFiltro = True
            End If

            Try
                If Not String.IsNullOrEmpty(search.Textsearch) Then
                    'SOLO  BUSQUEDA EN TODOS LOS INDICES
                    Dim dsResultsId As DataSet = Nothing
                    Try
                        Dim ST As New SearchToolsData(Server.Con)

                        dsResultsId = ST.SearchInAllIndexs(search)
                        If dsResultsId.Tables.Count <> 0 Then
                            For Each r As DataRow In dsResultsId.Tables(0).Rows
                                Dim drResult As DataRow = RB.GetResultRow(CType(r(0), Int64), CType(r(1), Int64))
                                If Not IsNothing(drResult) Then
                                    dtResults.Merge(drResult.Table)
                                End If
                            Next
                        End If
                    Catch ex As Exception
                        SearchName = Now.ToString
                    Finally
                        If dsResultsId IsNot Nothing Then
                            dsResultsId.Dispose()
                            dsResultsId = Nothing
                        End If
                    End Try

                    If Not isAssociatedSearch Then
                        UB.SaveAction(2, ObjectTypes.Documents, RightsType.Buscar, "Se realizó una búsqueda por la palabra: " & search.Textsearch)
                    End If

                Else
                    Dim CurrentDocType As DocType
                    Dim i As Int32
                    dtResults.MinimumCapacity = 0


                    For i = 0 To search.Doctypes.Count - 1
                        CurrentDocType = search.Doctypes(i)
                        If CurrentDocType.Indexs Is Nothing OrElse CurrentDocType.Indexs.Count = 0 Then
                            CurrentDocType.Indexs = ZCore.GetInstance().FilterIndex(CurrentDocType.ID)
                        End If

                        Dim Count As Int64
                        Dim dt As DataTable = SearchRows(CurrentDocType, search.Indexs, UserId, LastPage, PageSize, UseFilters, SearchsEntities, DtoOnly, UserId, search.OrderBy, search.Filters, search, Count, False, onlyImportants)
                        If Not IsNothing(dt) Then
                            Try
                                dtResults.Merge(dt, True, MissingSchemaAction.Add)
                                TotalCount = TotalCount + Count
                                If (search.ResultsCount.ContainsKey(CurrentDocType.ID)) Then
                                    search.ResultsCount(CurrentDocType.ID) = dt.MinimumCapacity
                                Else
                                    search.ResultsCount.Add(CurrentDocType.ID, Count)
                                End If
                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try
                        End If
                        dt = Nothing
                        Try

                            Dim up As New UserPreferences

                            Dim HabilitarFavoritos As Boolean = up.getValue("HabilitarFavoritos", UPSections.UserPreferences, False, search.UserId)
                            If (search.View IsNot Nothing AndAlso search.View.Contains("MyTasks") AndAlso HabilitarFavoritos) Then
                                dt = SearchRows(CurrentDocType, search.Indexs, UserId, LastPage, PageSize, UseFilters, SearchsEntities, DtoOnly, UserId, search.OrderBy, search.Filters, search, Count, True, False)
                                If Not IsNothing(dt) Then
                                    TotalCount = TotalCount + Count
                                    dtResults.Merge(dt, True, MissingSchemaAction.Add)
                                    If (search.ResultsCount.ContainsKey(CurrentDocType.ID)) Then
                                        search.ResultsCount(CurrentDocType.ID) = dt.MinimumCapacity
                                    Else
                                        search.ResultsCount.Add(CurrentDocType.ID, dt.MinimumCapacity)
                                    End If
                                End If
                                dt = Nothing

                            End If
                        Catch ex As Exception
                            ZClass.raiseerror(ex)
                        End Try

                        If Not isAssociatedSearch Then
                            Try
                                Dim indexsstring As String = String.Empty
                                If Not IsNothing(search.Indexs) Then
                                    For Each ind As IIndex In search.Indexs
                                        indexsstring &= ". Se filtro por el índice '" & ind.Name & " (" & ind.ID & ")' con el operador '" & ind.Operator & "' y valor '" & ind.DataTemp & "'"
                                    Next
                                End If
                                UB.SaveAction(2, ObjectTypes.Documents, RightsType.Buscar,
                                                                   "Se realizó búsqueda por el entidad: " _
                                                                   & CurrentDocType.Name & " (" & CurrentDocType.ID & ")" & indexsstring)

                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try
                        End If
                    Next
                End If

                SearchName = Nothing

                If search.OrderBy IsNot Nothing AndAlso (search.OrderBy.ToLower = """name"" asc" OrElse search.OrderBy.ToLower = """name"" desc") Then
                    Dim View As DataView
                    View = dtResults.AsDataView
                    View.Sort = search.OrderBy.Replace("""", "")
                    dtResults = View.ToTable()
                End If
                Return dtResults
            Catch ex As Threading.SynchronizationLockException
                ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadAbortException
                ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadInterruptedException
                ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadStateException
                ZClass.raiseerror(ex)
            End Try
            Return Nothing
        End Function

        Public Overloads Function DoGlobalSearch(ByVal search As Searchs.Search,
                                                 ByVal UserId As Int64,
                                                 ByVal LastPage As Int64,
                                                 ByVal PageSize As Int32,
                                                 ByVal isAssociatedSearch As Boolean,
                                                 ByVal UseFilters As Boolean,
                                                 ByVal DtoOnly As Boolean) As DataTable
            Dim dtResults As New DataTable
            Dim CurrentDocType As DocType
            Dim SearchName As String = String.Empty

            Dim SearchsEntities As New List(Of Int64)

            For Each DocType As IDocType In search.Doctypes
                SearchsEntities.Add(DocType.ID)
            Next

            Try
                If Not String.IsNullOrEmpty(search.Textsearch) Then
                    'SOLO  BUSQUEDA EN TODOS LOS INDICES
                    Try
                        Dim ST As New SearchToolsData(Server.Con)
                        dtResults = ST.SearchGlobalInAllIndexs(search).Tables(0)
                    Catch ex As Exception
                        SearchName = Now.ToString
                    End Try

                    If Not isAssociatedSearch Then
                        UB.SaveAction(2, ObjectTypes.Documents, RightsType.Buscar, "Se realizó una búsqueda por la palabra: " & search.Textsearch)
                    End If

                Else
                    Dim i As Int32
                    dtResults.MinimumCapacity = 0
                    Dim TotalCount As Int64 = 0
                    For i = 0 To search.Doctypes.Count - 1
                        CurrentDocType = search.Doctypes(i)
                        CurrentDocType.Indexs = ZCore.GetInstance().FilterIndex(CurrentDocType.ID)
                        Dim Count As Int64
                        Dim dt As DataTable = SearchRows(CurrentDocType, search.Indexs, UserId, LastPage, PageSize, UseFilters, SearchsEntities, DtoOnly, UserId, search.OrderBy, search.Filters, search, Count, False, False)
                        If Not IsNothing(dt) Then
                            TotalCount = TotalCount + Count
                            dtResults.Merge(dt)
                        End If
                        dt = Nothing

                        If Not isAssociatedSearch Then
                            Try
                                Dim indexsstring As String = String.Empty
                                If Not IsNothing(search.Indexs) Then
                                    For Each ind As IIndex In search.Indexs
                                        indexsstring &= ". Se filtro por el índice '" & ind.Name & " (" & ind.ID & ")' con el operador '" & ind.Operator & "' y valor '" & ind.DataTemp & "'"
                                    Next
                                End If
                                UB.SaveAction(2, ObjectTypes.Documents, RightsType.Buscar,
                                                                                   "Se realizó búsqueda por el entidad: " _
                                                                                   & CurrentDocType.Name & " (" & CurrentDocType.ID & ")" & indexsstring)

                            Catch ex As Exception
                                ZClass.raiseerror(ex)
                            End Try
                        End If
                    Next
                End If

                SearchName = Nothing
                search = Nothing
                Return dtResults
            Catch ex As Threading.SynchronizationLockException
                ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadAbortException
                ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadInterruptedException
                ZClass.raiseerror(ex)
            Catch ex As Threading.ThreadStateException
                ZClass.raiseerror(ex)
            End Try
            Return Nothing
        End Function

        Const ORACLE_PAGINATE_FORMAT As String = "(rn <= {1} and rn >= {0})"
        Private Function GetTaskTable(ByVal strQuery As StringBuilder,
                                      ByVal strPREselect As StringBuilder,
                                      ByVal strPOSTselect As StringBuilder,
                                      ByVal externalOrdenBy As String,
                                      ByVal PageSize As Integer,
                                      ByVal LastPage As Integer,
                                      ByVal search As ISearch,
                                      ByRef count As Long,
                                      ByVal DeclareString As String,
                                      refIndexs As List(Of ReferenceIndex)) As DataTable

            Dim Desde As Int32
            Dim Hasta As Int32
            Desde = (PageSize * LastPage) + 1
            Hasta = Desde + PageSize - 1

            Dim query As String = strQuery.ToString
            Dim Total As Int64
            Dim ds As New DataSet

            query = query.Replace("Exclusive,", "C_Exclusive,")
            query = query.Replace("[", Chr(34))
            query = query.Replace("]", Chr(34))

            'Se genera el query para hacer el count
            Dim strCountQuery As New StringBuilder(DeclareString & "select count(1) ")
            strCountQuery.Append(query.Substring(query.IndexOf("FROM")))

            'No me gusta mucho esto            
            If refIndexs IsNot Nothing AndAlso refIndexs.Count > 0 Then
                strCountQuery = RemoveIndReferencesJoins(refIndexs, strCountQuery)
            End If

            strCountQuery.Replace(") as Q", " ")
            query = query.Replace(") as Q", ")")
            strQuery.Replace(") as Q", String.Empty)

            strQuery.Insert(0, DeclareString & "select " & strPREselect.ToString() & " from (")
            '            strQuery.Insert(0, DeclareString & "select " & strPREselect.ToString() & ",I.* from (")

            If Boolean.Parse(ZOptBusiness.GetValueOrDefault("Usefetch", "false")) Then
                strQuery.Append(String.Format(" offset {0} rows fetch first {1} rows only ", Desde - 1, PageSize))
            End If

            strQuery.Append(") I ")
            strQuery.Append(strPOSTselect.ToString())
            strQuery.Append(" where ")
            strQuery.AppendFormat(ORACLE_PAGINATE_FORMAT, Desde, Hasta)

            strQuery.Append($" order by {externalOrdenBy}")

            If Server.isOracle Then
                strQuery.Replace("[", Chr(34))
                strQuery.Replace("]", Chr(34))
                strCountQuery.Replace("[", Chr(34))
                strCountQuery.Replace("]", Chr(34))
            Else
                strQuery.Replace("NVL(", "IsNull(")
                strQuery.Replace("nvl(", "IsNull(")
                strCountQuery.Replace("NVL(", "IsNull(")
                strCountQuery.Replace("nvl(", "IsNull(")
            End If

            Total = Server.Con.ExecuteScalar(CommandType.Text, strCountQuery.ToString())
            count = Total
            If Total > 0 Then
                ds = Server.Con.ExecuteDataset(CommandType.Text, strQuery.ToString())
                Return ds.Tables(0)
            Else
                Return Nothing
            End If

            If ds.Tables.Count > 0 Then
                If ds.Tables.Count = 2 AndAlso ds.Tables(1).Rows.Count > 0 Then
                    ds.Tables(0).MinimumCapacity = ds.Tables(1).Rows(0)(0)
                End If
                Return ds.Tables(0)
            End If
            Return Nothing
        End Function

        Private Shared Function RemoveIndReferencesJoins(refIndexs As List(Of ReferenceIndex), strCountQuery As StringBuilder) As StringBuilder

            Dim auxStrCountQuery As StringBuilder = strCountQuery
            Try
                'por cada indice referencial, si aparece en el join y no esta en el where o no hay where, lo quito.
                Dim refIndexsQueryHelper As New ReferenceIndexQueryHelper()

                Dim leftIndex As Int32 = auxStrCountQuery.ToString().ToLower.IndexOf("left")
                Dim whereIndex As Int32 = auxStrCountQuery.ToString().ToLower.IndexOf("where")
                Dim joinString As String
                Dim whereString As String

                If whereIndex > 0 Then
                    joinString = auxStrCountQuery.ToString().Substring(leftIndex, whereIndex - leftIndex)
                    whereString = auxStrCountQuery.ToString().Substring(whereIndex)
                Else
                    joinString = auxStrCountQuery.ToString().Substring(leftIndex)
                End If

                Dim whereRefIndex As New List(Of ReferenceIndex)
                For Each refInd As ReferenceIndex In refIndexs
                    If whereString IsNot Nothing AndAlso (whereString.ToLower.Contains(refInd.DBTable.ToLower) OrElse whereString.ToLower.Contains($"slst_s{refInd.IndexId}")) Then
                        whereRefIndex.Add(refInd)
                    End If
                Next

                For Each refInd As ReferenceIndex In refIndexs
                    If whereRefIndex.Find(Function(value As ReferenceIndex) value.DBTable = refInd.DBTable) Is Nothing AndAlso (joinString.ToLower.Contains(refInd.DBTable.ToLower) AndAlso (String.IsNullOrEmpty(whereString)) OrElse (Not whereString.ToLower.Contains(refInd.DBTable.ToLower) OrElse Not whereString.ToLower.Contains($"slst_s{refInd.IndexId}"))) Then
                        auxStrCountQuery = auxStrCountQuery.Replace(refIndexsQueryHelper.GetStringJoinQuery(refInd.IndexId, refInd.DoctypeId, refIndexs), String.Empty)
                    End If
                Next

                Return auxStrCountQuery

            Catch ex As Exception
                ZTrace.WriteLineIf(TraceLevel.Info, "No se pudieron remover los joins de indices referenciales de la consulta del total de registros.")
                Return strCountQuery
            End Try
        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Funcion para realizar una busqueda en Zamba, devuelve un arraylist de objetos results
        ''' </summary>
        ''' <param name="DocGroup">Id del Grupo o Sector de Zamba</param>
        ''' <param name="DocType">objeto Doc_type donde se realizará la busqueda</param>
        ''' <param name="INDEXS">Vector de objetos indices con la propiedad Data y/o Datatemp cargada</param>
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
        Public Overloads Function SearchRowsAsocForm(ByVal DocType As DocType,
                                                     ByVal Indexs As List(Of IIndex),
                                                     ByVal CurrentUserId As Int64,
                                                     ByVal LastPage As Int64,
                                                     ByVal PageSize As Int32,
                                                     ByVal result As IResult,
                                                     ByVal currentUserName As String) As DataTable


            Dim UP As New UserPreferences
            Dim RF As New Results_Factory

            Dim strTableI As String = MakeTable(DocType.ID, TableType.Indexs)
            Dim strTableT As String = MakeTable(DocType.ID, TableType.Document)
            Dim MainJoin As String = String.Format("{0} T inner join {1} I on T.doc_id = I.doc_id", strTableT, strTableI)

            Dim ZOPT As New ZOptBusiness
            Dim FlagCase As Boolean = Boolean.Parse(ZOPT.GetValue("CaseSensitive"))
            Dim i As Integer
            Dim Valuestring As New System.Text.StringBuilder
            Dim ColumCondstring As New System.Text.StringBuilder
            Dim First As Boolean = True
            Dim Orderstring As New System.Text.StringBuilder
            Dim dateDeclarationString As New System.Text.StringBuilder
            Dim RestrictionString As New System.Text.StringBuilder

            Dim SearchsEntities As New List(Of Int64)
            SearchsEntities.Add(DocType.ID)


            Try
                If Cache.DocTypesAndIndexs.hsRestrictionsIndexs.ContainsKey(CurrentUserId & "-" & DocType.ID) = False Then
                    Dim RMF As New RestrictionsMapper_Factory
                    Cache.DocTypesAndIndexs.hsRestrictionsIndexs.Add(CurrentUserId & "-" & DocType.ID, RMF.GetRestrictionIndexs(CurrentUserId, DocType.ID))
                    RMF = Nothing
                End If

                Dim indRestriction As Generic.List(Of IIndex) = Cache.DocTypesAndIndexs.hsRestrictionsIndexs.Item(CurrentUserId & "-" & DocType.ID)

                'Busco los indices referenciales de la entidad.
                Dim refIndexs As List(Of ReferenceIndex) = (New ReferenceIndexBusiness).GetReferenceIndexesByDoctypeId(DocType.ID)

                For i = 0 To Indexs.Count - 1
                    createWhereSearch(DocType.ID, Valuestring, Indexs, i, FlagCase, ColumCondstring, Orderstring, First, dateDeclarationString, String.Empty, SearchsEntities, refIndexs)
                Next

                For i = 0 To indRestriction.Count - 1
                    'createWhereSearch(Valuestring, indRestriction, i, FlagCase, RestrictionString, Orderstring, First, dateDeclarationString, False)
                    createWhereSearch(DocType.ID, Valuestring, indRestriction, i, FlagCase, RestrictionString, Orderstring, First, dateDeclarationString, DocType.ID.ToString, SearchsEntities, refIndexs)
                Next

                Orderstring.Append(" I.doc_id asc ")

                Dim strselect As New StringBuilder
                Dim strPREselect As New StringBuilder
                Dim strPOSTselect As New StringBuilder

                Dim search As ISearch = New Zamba.Core.Searchs.Search()
                search.SearchType = SearchTypes.AsociatedSearch
                search.UserId = CurrentUserId
                search.ParentEntity = result
                search.View = ""
                search.AddDocType(DocType)
                Dim IB As New IndexsBusiness
                Dim DTIndexs As New List(Of IIndex)
                ' If search.SearchType <> SearchTypes.AsignedTasks Then
                Indexs = IB.FilterIndexsByUserRights(DocType.ID, CurrentUserId, DocType.Indexs, RightsType.TaskGridIndexView)
                ' End If
                CreateSelectSearch(DocType, DocType.Indexs, True, Orderstring.ToString(), search, strselect, strPREselect, strPOSTselect, False, ColumCondstring.ToString, refIndexs)


                'Versiones
                If Boolean.Parse(UP.getValue("UseVersion", UPSections.UserPreferences, False, search.UserId)) Then
                    If ColumCondstring.Length > 0 Then
                        ColumCondstring.Remove(ColumCondstring.Length - 1, 1)
                        ColumCondstring.Append(" And Version = 0)")
                    Else
                        ColumCondstring.Append(" Version = 0")
                    End If
                End If

                Dim SubQueryClose As New StringBuilder
                SubQueryClose.Append(") as Q")

                InsertFilterRestrictionConditionString(DocType, ColumCondstring, SubQueryClose, CurrentUserId, strselect, dateDeclarationString, False, RestrictionString)


                Dim arraySustIndex As New ArrayList
                First = True

                i = 0
                Dim dt As DataTable = New DataTable
                strselect.Append(" And I.doc_id <> ")
                strselect.Append(result.ID)
                Dim Count As Int64
                dt = GetTaskTable(strselect, strPREselect, strPOSTselect, Orderstring.ToString, PageSize, LastPage, search, Count, dateDeclarationString.ToString, refIndexs)

                Return dt
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                Valuestring = Nothing
                ColumCondstring = Nothing
                Orderstring = Nothing
                UP = Nothing
                RF = Nothing
            End Try
            Return Nothing
        End Function

        ''' <summary>
        ''' Armado del select que se utiliza para las busquedas
        ''' </summary>
        ''' <param name="strTable">Nombre de la tabla. Ej: doc58</param>
        ''' <param name="Indexs">Indices que se van a traer</param>
        ''' <returns>Stringbuilder con la consulta</returns>
        ''' <history>   Marcelo    Created     20/08/09</history>
        ''' <remarks></remarks>
        'Private Function createSelectSearchAsocForm(DocTypeId As Int64, ByVal Indexs As List(Of IIndex), ByRef strselect As StringBuilder, ByRef strPREselect As StringBuilder, ByRef strPOSTselect As StringBuilder)

        '    Dim strTableI As String = MakeTable(DocTypeId, TableType.Indexs)
        '    Dim strTableT As String = MakeTable(DocTypeId, TableType.Document)
        '    Dim MainJoin As String = String.Format(" inner join {0} T on T.doc_id = I.doc_id", strTableT)


        '    Dim auIndex As New List(Of Int64)
        '    strselect.Append("Select * from (SELECT ")
        '    strselect.Append("T.DOC_ID, ")
        '    strselect.Append("T.DISK_GROUP_ID,PLATTER_ID,VOL_ID,DOC_FILE,OFFSET,")
        '    strselect.Append("T.DOC_TYPE_ID, ")
        '    strselect.Append("T.NAME as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append("Nombre")
        '    strselect.Append(Chr(34))
        '    strselect.Append(",")
        '    strselect.Append("T.ICON_ID,SHARED,ver_Parent_id,RootId,")
        '    If Server.isOracle Then
        '        strselect.Append("get_filename(original_Filename)")
        '    Else
        '        strselect.Append("REVERSE(SUBSTRING(REVERSE(original_Filename), 0, CHARINDEX('\', REVERSE(original_Filename))))")
        '    End If
        '    strselect.Append(" as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append("Original")
        '    strselect.Append(Chr(34))
        '    strselect.Append(",Version, NumeroVersion")
        '    strselect.Append(",disk_Vol_id, DISK_VOL_PATH, doc_type_name as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append("Entidad")
        '    strselect.Append(Chr(34))
        '    strselect.Append(", crdate as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append("Creado")
        '    strselect.Append(Chr(34))
        '    strselect.Append(", lupdate as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append("Modificado")
        '    strselect.Append(Chr(34))

        '    strselect.Append(", User_Asigned as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append("Asignado")
        '    strselect.Append(Chr(34))
        '    strselect.Append(", Task_State_ID as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append(GridColumns.SITUACION_COLUMNNAME)
        '    strselect.Append(Chr(34))
        '    strselect.Append(", Do_State_ID as ")
        '    strselect.Append(Chr(34))
        '    strselect.Append("Estado")
        '    strselect.Append(Chr(34))

        '    For Each _Index As Index In Indexs
        '        strselect.Append(",")
        '        strselect.Append("I.I")
        '        strselect.Append(_Index.ID)
        '        If _Index.DropDown = IndexAdditionalType.AutoSustitución OrElse _Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
        '            strselect.Append(", slst_s" & _Index.ID & ".descripcion")
        '            auIndex.Add(_Index.ID)
        '        End If
        '        strselect.Append(" as ")
        '        strselect.Append(Chr(34))
        '        strselect.Append(_Index.Name)
        '        strselect.Append(Chr(34))
        '    Next
        '    strselect.Append(" FROM ")
        '    strselect.Append(MainJoin)
        '    strselect.Append(" inner join doc_type on doc_type.doc_type_id = ")
        '    strselect.Append("T.doc_type_id left outer join disk_Volume on disk_Vol_id=vol_id ")

        '    If auIndex.Count > 0 Then
        '        For Each indiceID As Int64 In auIndex
        '            strselect.Append(" left join slst_s" & indiceID & " on I.i" & indiceID & " = slst_s" & indiceID & ".codigo ")
        '        Next
        '    End If

        '    'realizo un inner join con la WFdocument para agregar las
        '    'columnas faltantes al realizar busquedas en tareas
        '    strselect.Append("left join wfdocument wd ON ")
        '    strselect.Append("T.DOC_ID = wd.DOC_ID")

        '    auIndex = Nothing
        '    Return strselect
        'End Function


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

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Funcion para realizar una busqueda en Zamba, devuelve un arraylist de objetos results
        ''' </summary>
        ''' <param name="DocGroup">Id del Grupo o Sector de Zamba</param>
        ''' <param name="DocType">objeto Doc_type donde se realizará la busqueda</param>
        ''' <param name="INDEXS">Vector de objetos indices con la propiedad Data y/o Datatemp cargada</param>
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
        Public Overloads Function SearchRows(ByVal DocType As DocType,
                                             ByVal Indexs As List(Of IIndex),
                                             ByVal CurrentUserId As Int64,
                                             ByVal LastPage As Int64,
                                             ByVal PageSize As Int32,
                                             ByVal UseFilters As Boolean,
                                             ByVal SearchsEntities As List(Of Int64),
                                             ByVal DtoOnly As Boolean,
                                             ByVal UserId As Int64,
                                             ByVal OrderBy As String,
                                             ByVal Filters As List(Of ikendoFilter),
                                             ByVal search As ISearch,
                                             ByRef Count As Int64,
                                             ByVal isFavoriteSearch As Boolean, onlyImportants As Boolean) As DataTable


            Dim ColumCondstring As New StringBuilder
            Dim dateDeclarationString As New StringBuilder
            Dim RestrictionString As New StringBuilder
            Dim Valuestring As New StringBuilder

            Dim FlagCase As Boolean = Boolean.Parse(New ZOptBusiness().GetValue("CaseSensitive"))
            Dim First As Boolean = True

            Try

                'Indices de la entidad, filtrados segun permisos del usuario
                Dim dtIndexs As List(Of IIndex) = New IndexsBusiness().FilterIndexsByUserRights(DocType.ID, CurrentUserId, DocType.Indexs, RightsType.TaskGridIndexView)



                Dim externalOrderBy As StringBuilder = GetExternalOrderString(DocType, dtIndexs, OrderBy)
                Dim internalOrderBy As String = GetInternalOrderString(DocType, dtIndexs, OrderBy)

                Dim RmF As New RestrictionsMapper_Factory
                Dim indRestriction As New List(Of IIndex)
                If Cache.DocTypesAndIndexs.hsRestrictionsIndexs.ContainsKey(CurrentUserId & "-" & DocType.ID) = False Then
                    Dim RInd As List(Of IIndex) = RmF.GetRestrictionIndexs(CurrentUserId, DocType.ID)
                    SyncLock Cache.DocTypesAndIndexs.hsRestrictionsIndexs
                        If Cache.DocTypesAndIndexs.hsRestrictionsIndexs.ContainsKey(CurrentUserId & "-" & DocType.ID) = False Then
                            Cache.DocTypesAndIndexs.hsRestrictionsIndexs.Add(CurrentUserId & "-" & DocType.ID, RInd)
                        End If
                    End SyncLock
                End If

                indRestriction = Cache.DocTypesAndIndexs.hsRestrictionsIndexs.Item(CurrentUserId & "-" & DocType.ID)

                If Not String.IsNullOrEmpty(search.Restriction) Then
                    indRestriction.Add(RmF.GetRestrictionIndexs(CurrentUserId, DocType.ID, Int64.Parse(search.Restriction)))
                End If
                RmF = Nothing

                'Busco los indices referenciales de la entidad.
                Dim refIndexs As List(Of ReferenceIndex) = New ReferenceIndexBusiness().GetReferenceIndexesByDoctypeId(DocType.ID)

                'Obtener los filtros cuando solo tiene 1 entidad seleccionada
                Dim filtersWeb As New List(Of IFilterElem)
                If search.Doctypes.Count = 1 Then
                    filtersWeb = New FiltersComponent().GetFiltersWebByView(search.Doctypes.First().ID, CurrentUserId, search.View).Where(Function(f) IsNumeric(f.Filter.Substring(1)) And f.Enabled).Select(Function(f) f).ToList()
                End If
                'Primero el where porque si se ordena por indice lo hace desde aca
                Dim whereQuery As StringBuilder = CreateWhere(DocType, Indexs, Valuestring, FlagCase, ColumCondstring, externalOrderBy, First, dateDeclarationString, SearchsEntities, indRestriction, RestrictionString, UserId, filtersWeb, refIndexs, DocType.Indexs)

                Dim strselect As New StringBuilder
                Dim strPREselect As New StringBuilder
                Dim strPOSTselect As New StringBuilder

                CreateSelectSearch(DocType, dtIndexs, DtoOnly, internalOrderBy, search, strselect, strPREselect, strPOSTselect, isFavoriteSearch, ColumCondstring.ToString, refIndexs)

                'Versiones
                If Boolean.Parse(New UserPreferences().getValue("UseVersion", UPSections.UserPreferences, False, Membership.MembershipHelper.CurrentUser.ID)) Then
                    If ColumCondstring.Length > 0 Then
                        ColumCondstring.Remove(ColumCondstring.Length - 1, 1)
                        ColumCondstring.Append(" AND")
                    End If
                    ColumCondstring.Append(" Version = 0 ")
                End If

                Dim SubQueryClose As New StringBuilder
                InsertFilterRestrictionConditionString(DocType, ColumCondstring, SubQueryClose, CurrentUserId, strselect, dateDeclarationString, UseFilters, RestrictionString)

                First = True

                Dim dt As DataTable = GetTaskTable(strselect, strPREselect, strPOSTselect, externalOrderBy.ToString, PageSize, LastPage, search, Count, dateDeclarationString.ToString, refIndexs)

                Return dt

            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                ColumCondstring = Nothing
                dateDeclarationString = Nothing
                RestrictionString = Nothing
                Valuestring = Nothing
            End Try

            Return Nothing

        End Function

        Private Function GetExternalOrderString(ByVal DocType As IDocType, ByVal dtIndexes As List(Of IIndex), orderstring As String) As StringBuilder

            Dim _order As New StringBuilder()

            If Not String.IsNullOrEmpty(orderstring) Then

                Dim tempOrderStr As String = orderstring

                ' puede venir con comillas simples o dobles (?) 
                Dim _char As Char = """"
                If tempOrderStr.Contains("""") Then
                    _char = """"
                ElseIf tempOrderStr.Contains("'") Then
                    _char = "'"
                End If

                Dim _column As String = tempOrderStr.Substring(0, (tempOrderStr.LastIndexOf(_char) + 1)).Replace(_char, String.Empty)
                Dim _operator As String = tempOrderStr.Substring(tempOrderStr.LastIndexOf(_char) + 1)

                Dim _indexId As Int64 = IndexsBusiness.GetIndexIdByName(_column)

                If _indexId > 0 Then

                    Dim _dtInd = dtIndexes.Where(Function(i) i.ID = _indexId).FirstOrDefault

                    If _dtInd IsNot Nothing Then
                        _order.Append(orderstring)
                    End If

                Else

                    If _column.ToLower.Trim.Equals("i.crdate") Then
                        _column = "I.CREADO"
                    ElseIf _column.ToLower.Trim.Equals("i.lupdate") Then
                        _column = "I.modificado"
                    ElseIf _column.ToLower.Trim.Equals("step id") Then
                        _column = "I.step_id"
                    ElseIf _column.ToLower.Trim.Equals("task id") Then
                        _column = "I.task_id"
                    ElseIf _column.ToLower.Trim.Equals("user asigned") Then
                        _column = "I.User_Asigned"
                    ElseIf _column.ToLower.Trim.Equals("checkin") Then
                        _column = "I.INGRESO"
                    ElseIf _column.ToLower.Trim.Equals("name") Then
                        _column = "NAME"
                    End If

                    If _column.Split(" ").Count > 1 Then
                        _order.Append($" ""{_column}"" {_operator} ")

                    Else
                        _order.Append($" {_column} {_operator} ")
                    End If

                End If

                If Server.isSQLServer Then
                    _order = _order.Replace("""", "'")
                Else
                    _order = _order.Replace("'", """")
                End If

            End If

            If String.IsNullOrEmpty(_order.ToString()) Then
                _order.Append("I.doc_id desc ")
            End If

            Return _order

        End Function

        Private Function GetInternalOrderString(ByVal DocType As IDocType, ByVal dtIndexes As List(Of IIndex), orderstring As String) As String

            Dim tempOrderStr As String = orderstring

            If Not String.IsNullOrEmpty(tempOrderStr) Then

                ' puede venir con comillas simples o dobles (?) 
                Dim _char As Char = """"
                If tempOrderStr.Contains("""") Then
                    _char = """"
                ElseIf tempOrderStr.Contains("'") Then
                    _char = "'"
                End If

                Dim _column As String = tempOrderStr.Substring(0, (tempOrderStr.LastIndexOf(_char) + 1)).Replace(_char, String.Empty)
                Dim _operator As String = tempOrderStr.Substring(tempOrderStr.LastIndexOf(_char) + 1)

                Dim _indexId As Int64 = IndexsBusiness.GetIndexIdByName(_column)

                If _indexId > 0 Then

                    Dim _dtInd = dtIndexes.Where(Function(i) i.ID = _indexId).FirstOrDefault

                    If _dtInd IsNot Nothing Then

                        If _dtInd.DropDown = IndexAdditionalType.AutoSustitución OrElse _dtInd.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                            tempOrderStr = $"slst_s{_dtInd.ID}.descripcion {_operator}"
                        Else
                            tempOrderStr = $"I.I{_dtInd.ID} {_operator}"
                        End If

                    Else
                        tempOrderStr = String.Empty
                    End If

                Else

                    If _column.ToLower.Trim.Equals("doc type name") Then
                        _column = $"'{DocType.Name}'"
                    ElseIf _column.ToLower.Trim.Equals("task id") Then
                        _column = "task_id"
                    ElseIf _column.ToLower.Trim.Equals("step id") OrElse _column.ToLower.Trim.Equals("step") Then
                        _column = "step_id"
                    ElseIf _column.ToLower.Trim.Equals("name") Then
                        _column = If(Server.isOracle, "to_char(wd.Name)", "wd.Name")
                    ElseIf _column.ToLower.Trim.Equals("user asigned") OrElse _column.ToLower.Trim.Equals("assignedto") Then
                        _column = "User_Asigned"
                    ElseIf _column.ToLower.Trim.Equals("ingreso") Then
                        _column = "checkin"
                    ElseIf _column.ToLower.Trim.Equals("original") Then
                        _column = "original_filename"
                    Else
                        _column = GridColumns.GetColumnNameByAliasName(_column).Trim
                    End If

                    If String.IsNullOrEmpty(_column) Then
                        tempOrderStr = String.Empty
                    Else
                        If _column.Split(" ").Count > 1 Then
                            If Server.isOracle Then
                                tempOrderStr = $" ""{_column}"" {_operator} "
                            Else
                                tempOrderStr = $" '{_column}' {_operator} "
                            End If

                        Else
                            tempOrderStr = $" {_column} {_operator} "
                        End If
                    End If

                End If

            End If

            If String.IsNullOrEmpty(tempOrderStr) Then
                tempOrderStr = "I.doc_id desc "
            End If

            Return tempOrderStr

        End Function

        Private Function CreateWhere(ByVal DocType As DocType,
                                     ByVal Indexs As Generic.List(Of IIndex),
                                     ByRef Valuestring As StringBuilder,
                                     ByRef FlagCase As Boolean,
                                     ByRef ColumCondstring As StringBuilder,
                                     ByRef Orderstring As StringBuilder,
                                     ByRef First As Boolean,
                                     ByRef dateDeclarationString As StringBuilder,
                                     ByVal SearchsEntities As List(Of Int64),
                                     ByRef indRestriction As Generic.List(Of IIndex),
                                     ByRef RestrictionString As StringBuilder,
                                     ByVal UserId As Int64,
                                     ByVal Filters As List(Of IFilterElem),
                                     ByVal refIndexs As List(Of ReferenceIndex),
                                     ByVal EntityIndexs As List(Of IIndex)) As StringBuilder

            If Not Indexs Is Nothing Then
                For i As Integer = 0 To Indexs.Count - 1
                    If EntityIndexs.Count = 0 OrElse EntityIndexs.Exists(Function(x) x.ID = Indexs(i).ID) Then
                        createWhereSearch(DocType.ID, Valuestring, Indexs, i, FlagCase, ColumCondstring, Orderstring, First, dateDeclarationString, DocType.ID.ToString, SearchsEntities, refIndexs)
                    End If
                Next
            End If

            If Not indRestriction Is Nothing Then
                For i As Integer = 0 To indRestriction.Count - 1
                    createWhereSearch(DocType.ID, Valuestring, indRestriction, i, FlagCase, RestrictionString, Orderstring, First, dateDeclarationString, DocType.ID.ToString, SearchsEntities, refIndexs)
                Next
            End If


            Dim auxIndexFilterList As New List(Of IIndex)
            If Not Filters Is Nothing Then
                For i As Integer = 0 To Filters.Count - 1
                    Dim indexID As Long = Int64.Parse(Filters(i).Filter.Substring(1))
                    Dim auxIndex As IIndex = New IndexsBusiness().GetIndex(indexID)
                    If Not String.IsNullOrEmpty(Filters(i).Value) Then
                        Dim valueLength = Filters(i).Value.Length
                        If auxIndex.DropDown = IndexAdditionalType.AutoSustitución Or auxIndex.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Or auxIndex.DropDown = IndexAdditionalType.DropDown Or auxIndex.DropDown = IndexAdditionalType.DropDownJerarquico Then
                            auxIndex.dataDescription = Filters(i).Value.Remove(0, 1).Remove(valueLength - 2, 1)
                        End If
                        auxIndex.Data = Filters(i).Value.Remove(0, 1).Remove(valueLength - 2, 1)
                    Else
                        auxIndex.Data = String.Empty
                    End If
                    auxIndex.Operator = Filters(i).Comparator
                    auxIndexFilterList.Add(auxIndex)
                Next
            End If
            If Not auxIndexFilterList Is Nothing Then
                For i As Integer = 0 To auxIndexFilterList.Count - 1
                    createWhereSearch(DocType.ID, Valuestring, auxIndexFilterList, i, FlagCase, ColumCondstring, Orderstring, First, dateDeclarationString, DocType.ID.ToString, SearchsEntities, refIndexs)
                Next
            End If
            Return ColumCondstring
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="DocType"></param>
        ''' <param name="ColumCondstring"></param>
        ''' <param name="Orderstring"></param>
        ''' <param name="CurrentUserId"></param>
        ''' <param name="FC"></param>
        ''' <param name="strselect"></param>
        ''' <param name="dateDeclarationString"></param>
        ''' <param name="UseFilters"></param>
        ''' <param name="IsTask"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' 		                    Created      
        '''     [Javier]    01/10/2010  Modified    Se aplican cambios para las restricciones, las restricciones son aplicadas en 
        '''                                         search_row
        '''     [Javier]    08/10/2010  Modified    Se coloca las restricciones dentro de la primer consulta
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Private Function InsertFilterRestrictionConditionString(ByVal DocType As DocType, ByVal ColumCondstring As StringBuilder, ByVal Orderstring As StringBuilder, ByVal CurrentUserId As Long, ByRef strselect As StringBuilder, ByRef dateDeclarationString As StringBuilder, ByVal UseFilters As Boolean, ByVal RestrictionString As StringBuilder) As String

            If CurrentUserId > 0 Then
                If RestrictionString.Length > 0 Then
                    If (strselect.ToString.Contains("WHERE")) Then
                        strselect.Append(" and ")
                    Else
                        strselect.Append(" WHERE ")
                    End If
                    strselect.Append(RestrictionString.ToString)
                End If

                strselect.Append(" " & Orderstring.ToString)

                Dim FilterString As String = strselect.ToString
                Dim lastWhere As Integer = FilterString.LastIndexOf("where", StringComparison.CurrentCultureIgnoreCase)
                Dim hayWhere As Boolean = lastWhere > -1 And Not FilterString.EndsWith(")Then as Q") And lastWhere > FilterString.LastIndexOf("SQDRead", StringComparison.CurrentCultureIgnoreCase)
                FilterString = String.Empty

                If ColumCondstring.Length > 0 Then
                    If hayWhere Then
                        strselect.Append(" AND ")
                    Else
                        strselect.Append(" WHERE ")
                        hayWhere = True
                    End If
                    strselect.Append(ColumCondstring.ToString)
                End If

                If FilterString.Length > 0 Then
                    If Not hayWhere Then
                        strselect.Append(" WHERE ")
                        hayWhere = True
                    Else
                        strselect.Append(" AND ")
                    End If
                    strselect.Append(FilterString.ToString)
                End If


            End If

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
        Private Sub CreateRow(ByRef ArraySustIndex As ArrayList, ByRef dr As DataRow, ByVal indexs As List(Of IIndex), Optional ByVal IsFolDerSearch As Boolean = False)
            Dim intCounter As Int32 = 0
            Dim ASB As New AutoSubstitutionBusiness
            If IsFolDerSearch Then dr("nombre") = "(Carpeta) - " & dr("nombre")
            For Each indice As IIndex In indexs
                If indice.DropDown = IndexAdditionalType.AutoSustitución OrElse indice.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                    ArraySustIndex.Add(indice)
                    If dr.Table.Columns.Contains(indice.Name) = False Then dr.Table.Columns.Add(indice.Name)

                    If Not IsDBNull(dr.Item("I" & indice.ID)) Then
                        dr.Item(indice.Name) = ASB.getDescription(dr.Item("I" & indice.ID), indice.ID)
                    Else
                        dr.Item(indice.Name) = String.Empty
                    End If
                End If
            Next
            ASB = Nothing
        End Sub

        ''Castea el tipo de un índice a Type
        Private Function GetIndexType(ByVal iType As IndexDataType) As Type
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

        Private Sub createWhereSearch(EntityId As Int64,
                                      ByRef sbValue As StringBuilder,
                                      ByVal Indexs As Generic.List(Of IIndex),
                                      ByRef i As Int64,
                                      ByRef FlagCase As Boolean,
                                      ByRef ColumCondstring As StringBuilder,
                                      ByRef Orderstring As StringBuilder,
                                      ByRef First As Boolean,
                                      ByRef dateDeclarationString As StringBuilder,
                                      ByVal tablePrefix As String,
                                      ByVal SearchsEntities As List(Of Int64),
                                      ByVal refIndexs As List(Of ReferenceIndex))

            Dim mainVal As Object = Nothing
            Dim tempVal As Object = Nothing
            Dim sbDate As New StringBuilder
            Dim indexColName As String

            Dim separator As String = " AND "

            sbValue.Remove(0, sbValue.Length)
            'sbValue.Append(Indexs(i).Data)

            'If String.IsNullOrEmpty(tablePrefix) Then
            '    indexColName = "[" & Indexs(i).Name & "]"
            'Else
            Dim Ind As Zamba.Core.Index = Indexs(i)
            If Server.isOracle Then
                'Ind.Data = Replace(Ind.Data, "&", "' || chr(38) || '")
                'Ind.dataDescription = Replace(Ind.dataDescription, "&", "' || chr(38) || '")
            End If
            Select Case Indexs(i).DropDown
                Case IndexAdditionalType.AutoSustitución, IndexAdditionalType.AutoSustituciónJerarquico
                    If (Ind.[Operator] <> "=" OrElse Ind.Data.Length = 0) OrElse (Ind.[Operator] = "=" And (Ind.Type <> IndexDataType.Numerico AndAlso Ind.Type <> IndexDataType.Numerico_Largo)) Then
                        'En busqueda global web se usa [Data] para guardar valores
                        Dim dd As String = IIf(Ind.dataDescription = String.Empty, Ind.Data, Ind.dataDescription)
                        sbValue.Append(dd)
                        indexColName = "SLST_S" & Ind.ID.ToString & ".DESCRIPCION"
                    Else
                        sbValue.Append(Indexs(i).Data)
                        If refIndexs IsNot Nothing AndAlso refIndexs.Count > 0 AndAlso refIndexs.Any(Function(_i) _i.IndexId = Ind.ID) Then
                            Dim refQueryHelper As New ReferenceIndexQueryHelper
                            indexColName = $"{refQueryHelper.GetRefIndexRealColumn(Ind.ID, refIndexs)}"
                        Else
                            indexColName = "I.I" & Ind.ID.ToString
                        End If
                    End If

                Case Else
                    sbValue.Append(Indexs(i).Data)
                    If refIndexs IsNot Nothing AndAlso refIndexs.Count > 0 AndAlso refIndexs.Any(Function(_i) _i.IndexId = Ind.ID) Then
                        Dim refQueryHelper As New ReferenceIndexQueryHelper
                        indexColName = $"{refQueryHelper.GetRefIndexRealColumn(Ind.ID, refIndexs)}"
                    Else
                        indexColName = "I.I" & Ind.ID.ToString
                    End If
            End Select
            'End If
            If sbValue.Length = 0 And Not String.IsNullOrEmpty(Indexs(i).Data) Then
                sbValue.Append(Indexs(i).Data)
            End If

            If sbValue.Length > 0 OrElse Indexs(i).[Operator].ToLower = "es nulo" OrElse Indexs(i).[Operator].ToLower = "no es nulo" Then
                mainVal = ""
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
                                        Dim code = AutoSubstitutionDataFactory.getCode(Indexs(i).ID, sbValue.ToString)
                                        If String.IsNullOrEmpty(code) Or Indexs(i).Operator.ToLower = "contiene" Or Indexs(i).Operator.ToLower = "no contiene" Or Indexs(i).Operator.ToLower = "<>" Or Indexs(i).Operator.ToLower = "empieza" Or Indexs(i).Operator.ToLower = "termina" Then
                                            If indexColName.ToLower.Contains(".descripcion") Then
                                                mainVal = "'" & sbValue.ToString & "'"
                                            Else
                                                mainVal = sbValue.ToString
                                            End If
                                        Else
                                            mainVal = "'" & code.ToString & "'"
                                        End If
                                    Else
                                        mainVal = Int64.Parse(sbValue.ToString)
                                    End If
                                End If

                            Case IndexDataType.Numerico_Decimales, IndexDataType.Moneda
                                If sbValue.Length <> 0 Then
                                    If System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator = "," Then
                                        sbValue = sbValue.Replace(".", ",")
                                    End If
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
                                        mainVal = "@fecdesde" & Indexs(i).Column & "_" & i.ToString

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
                    mainVal = Indexs(i).Data
                End If

                Dim Op As String = mainVal.ToString
                If Op.Contains("''") Then Op = Op.Replace("''", "'")
                mainVal = Op
                Op = String.Empty
                If MisTareasSinFiltro = False Then

                    If ColumCondstring.Length > 0 Then
                        ColumCondstring.Append(separator)
                    End If

                    Select Case Indexs(i).[Operator].ToLower()
                        Case "="
                            Op = "="
                            If FlagCase AndAlso (Indexs(i).Type = IndexDataType.Alfanumerico OrElse Indexs(i).Type = IndexDataType.Alfanumerico_Largo) AndAlso (IsNumeric(Results_Business.ReplaceChar(mainVal)) = False) Then
                                ColumCondstring.Append(" (lower(" & indexColName & ") = " & LCase(mainVal) & ") ")
                            ElseIf Indexs(i).Type = IndexDataType.Fecha Or Indexs(i).Type = IndexDataType.Fecha_Hora Then
                                ColumCondstring.Append(" ( CAST(" & indexColName & " AS DATE) =  CAST(" & mainVal & " AS DATE)) ")
                            Else

                                ColumCondstring.Append(" (" & indexColName & " = " & mainVal & " ) ")

                            End If

                        Case "<>", "distinto"
                            Op = "<>"
                            If Indexs(i).Type = IndexDataType.Fecha Or Indexs(i).Type = IndexDataType.Fecha_Hora Then
                                If Server.Con.isOracle Then
                                    ColumCondstring.Append(" (" & indexColName & "<>(" & mainVal & ") or " & indexColName & " is null) ")
                                Else
                                    ColumCondstring.Append(String.Format("(CONVERT(DATE,{0} , 102) <> {1} or " & indexColName & " is null) ", indexColName, mainVal))
                                End If
                            Else
                                ColumCondstring.Append(" (" & indexColName & "<>(" & mainVal & ") or " & indexColName & " is null) ")
                            End If

                        Case ">", "<", ">=", "<="
                            Op = Indexs(i).[Operator]

                            If Indexs(i).Type = IndexDataType.Fecha Or Indexs(i).Type = IndexDataType.Fecha_Hora Then
                                If Server.Con.isOracle Then
                                    ColumCondstring.Append(" (" & indexColName & Op & "(" & mainVal & ")) ")
                                Else
                                    ColumCondstring.Append(String.Format("CONVERT(DATE,{0} , 102) " & Op & " {1} ", indexColName, mainVal))
                                End If
                            Else
                                ColumCondstring.Append(" (" & indexColName & Op & "(" & mainVal & ")) ")
                            End If

                        Case "es nulo", "no es nulo"
                            If Indexs(i).[Operator].ToLower() = "es nulo" Then
                                Op = "is null"
                                If Indexs(i).Type = IndexDataType.Alfanumerico Or Indexs(i).Type = IndexDataType.Alfanumerico_Largo Then
                                    ColumCondstring.Append(" (" & indexColName & " is null or " & indexColName & " = '') ")
                                Else
                                    ColumCondstring.Append(" (" & indexColName & " is null) ")
                                End If
                            Else
                                Op = "is not null"
                                If Indexs(i).Type = IndexDataType.Alfanumerico Or Indexs(i).Type = IndexDataType.Alfanumerico_Largo Then
                                    ColumCondstring.Append(" (" & indexColName & " is not null and " & indexColName & " <> '') ")
                                Else
                                    ColumCondstring.Append(" (" & indexColName & " is not null) ")
                                End If
                            End If

                        Case "entre"
                            Dim Data2Added As Boolean
                            Try
                                'cambio las a como indice a I ya que todos los indices vienen con dato algunos vacio otros no.
                                Select Case Indexs(i).Type
                                    Case 1, 2, 3, 6
                                        If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                            If Indexs(i).Type = IndexDataType.Numerico OrElse
                                        Indexs(i).Type = IndexDataType.Numerico_Largo Then
                                                tempVal = Int64.Parse(Indexs(i).Data2)
                                            Else
                                                tempVal = CDec(Indexs(i).Data2)
                                            End If
                                            Data2Added = True
                                        End If
                                    Case 4, 5
                                        If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                                            If Not String.IsNullOrEmpty(sbValue.ToString.Trim) Then
                                                If Server.isSQLServer Then
                                                    tempVal = "@fechasta" & Indexs(i).Column
                                                    sbDate.Remove(0, sbDate.Length)
                                                    sbDate.Append("declare " & tempVal & " datetime ")

                                                    If Indexs(i).Type = IndexDataType.Fecha Then
                                                        sbDate.Append("set " & tempVal & " = " & Server.Con.ConvertDate(Indexs(i).Data2) & " ")
                                                    Else
                                                        sbDate.Append("set " & tempVal & " = " & Server.Con.ConvertDateTime(Indexs(i).Data2) & " ")
                                                    End If

                                                    dateDeclarationString.Append(sbDate)
                                                Else
                                                    If Indexs(i).Type = IndexDataType.Fecha Then
                                                        tempVal = Server.Con.ConvertDate(Indexs(i).Data2)
                                                    Else
                                                        tempVal = Server.Con.ConvertDateTime(Indexs(i).Data2)
                                                    End If
                                                End If
                                            End If
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
                                    ColumCondstring.Append(" (" & indexColName & ">=(" & mainVal & ") and " & indexColName & "<=(" & tempVal & "))")
                                End If
                                Data2Added = False
                            End If

                        Case "contiene"
                            If mainVal.Contains("  ") Then
                                mainVal = mainVal.Replace("  ", " ")
                            End If
                            mainVal = mainVal.Trim.Replace(" ", "%")

                            If FlagCase AndAlso (IsNumeric(Results_Business.ReplaceChar(Replace(Trim(LCase(mainVal)), "'", String.Empty))) = False) Then
                                ColumCondstring.Append(" (lower(" & indexColName & ") Like '%" & Replace(Trim(LCase(mainVal)), "'", String.Empty) & "%')")
                            Else
                                ColumCondstring.Append(" (" & indexColName & " Like '%" & Replace(Trim(mainVal), "'", String.Empty) & "%')")
                            End If

                        Case "empieza"
                            If FlagCase AndAlso (IsNumeric(Results_Business.ReplaceChar(Replace(Trim(LCase(mainVal)), "'", String.Empty))) = False) Then
                                ColumCondstring.Append(" (lower(" & indexColName & ") Like '" & Replace(Trim(LCase(mainVal)), "'", String.Empty) & "%')")
                            Else
                                ColumCondstring.Append(" (" & indexColName & " Like '" & Replace(Trim(mainVal), "'", String.Empty) & "%')")
                            End If

                        Case "termina"
                            If FlagCase AndAlso (IsNumeric(Results_Business.ReplaceChar(Replace(Trim(LCase(mainVal)), "'", String.Empty))) = False) Then
                                ColumCondstring.Append(" (lower(" & indexColName & ") Like '%" & Replace(Trim(LCase(mainVal)), "'", String.Empty) & "')")
                            Else
                                ColumCondstring.Append(" (" & indexColName & " Like '%" & Replace(Trim(mainVal), "'", String.Empty) & "')")
                            End If

                        Case "alguno"
                            Op = "LIKE"
                            mainVal = mainVal.Replace(";", ",")
                            While mainVal.Contains("  ")
                                mainVal.Replace("  ", " ")
                            End While
                            mainVal = mainVal.Trim.Replace(" ", ",")

                            Dim SomeValues As Array = DirectCast(mainVal, String).Split(",")
                            Dim somestring As String = String.Empty
                            For x As Int32 = 0 To SomeValues.Length - 1
                                Dim Val As String = SomeValues(x)
                                If IsNothing(Val) = False AndAlso Not String.IsNullOrEmpty(Val.Trim) Then
                                    If i = 0 AndAlso x = 0 Then
                                        If FlagCase AndAlso (IsNumeric(Results_Business.ReplaceChar(LCase(Val).Replace("'", String.Empty))) = False) Then
                                            somestring = " (lower(" & indexColName & ") " & Op & " ('%" & LCase(Val).Replace("'", String.Empty) & "%')"
                                        Else
                                            somestring = " (" & indexColName & " " & Op & " ('%" & Val.Replace("'", String.Empty) & "%')"
                                        End If
                                    ElseIf x > 0 Then
                                        If String.IsNullOrEmpty(somestring) Then
                                            If FlagCase AndAlso (IsNumeric(Results_Business.ReplaceChar(Replace(LCase(Val), "'", String.Empty).Trim)) = False) Then
                                                somestring &= separator & " lower(" & DirectCast(indexColName, String) & ") " & Op & " ('%" & Replace(LCase(Val), "'", String.Empty).Trim & "%')"
                                            Else
                                                somestring &= separator & " " & DirectCast(indexColName, String) & " " & Op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                                            End If
                                        Else
                                            separator = " or "
                                            If FlagCase AndAlso (IsNumeric(Results_Business.ReplaceChar(Replace(LCase(Val), "'", String.Empty).Trim)) = False) Then
                                                somestring &= separator & " lower(" & indexColName & ") " & Op & " ('%" & Replace(LCase(Val), "'", String.Empty).Trim & "%')"
                                            Else
                                                somestring &= separator & " " & indexColName & " " & Op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                            If Not String.IsNullOrEmpty(somestring) Then ColumCondstring.Append(somestring & ")")
                            SomeValues = Nothing
                            somestring = Nothing

                        Case "no contiene"

                            Op = "NOT LIKE"

                            'Op = "NOT LIKE"
                            mainVal = mainVal.Replace(";", ",")
                            While mainVal.Contains("  ")
                                mainVal = mainVal.Replace("  ", " ")
                            End While
                            mainVal = mainVal.Trim.Replace(" ", ",")

                            Dim SomeValues As Array = DirectCast(mainVal, String).Split(",")
                            Dim somestring As String = String.Empty
                            For x As Int32 = 0 To SomeValues.Length - 1
                                Dim Val As String = SomeValues(x)
                                If IsNothing(Val) = False AndAlso Not String.IsNullOrEmpty(Val.Trim) Then
                                    If x = 0 And i = 0 Then
                                        If FlagCase AndAlso (IsNumeric(Results_Business.ReplaceChar(LCase(Val).Replace("'", String.Empty))) = False) Then
                                            somestring = " (lower(" & indexColName & ") " & Op & " ('%" & LCase(Val).Replace("'", String.Empty) & "%')"
                                        Else
                                            somestring = " (" & indexColName & " " & Op & " ('%" & Val.Replace("'", String.Empty) & "%')"
                                        End If
                                    ElseIf x = 0 Then

                                        If FlagCase AndAlso (IsNumeric(Results_Business.ReplaceChar(LCase(Val).Replace("'", String.Empty))) = False) Then
                                            somestring = " (lower(" & indexColName & ") " & Op & " ('%" & LCase(Val).Replace("'", String.Empty) & "%')"
                                        Else
                                            If Server.isSQLServer Then
                                                somestring = " (" & indexColName & " " & Op & " ('%" & Val.Replace("'", String.Empty) & "%') OR " & indexColName & " is null"
                                            Else
                                                somestring = " (" & indexColName & " " & Op & " ('%" & Val.Replace("'", String.Empty) & "%')"
                                            End If
                                        End If
                                    Else
                                        If String.IsNullOrEmpty(somestring) Then
                                            If FlagCase AndAlso (IsNumeric(Results_Business.ReplaceChar(Replace(LCase(Val), "'", String.Empty).Trim)) = False) Then
                                                somestring &= separator & " lower(" & DirectCast(indexColName, String) & ") " & Op & " ('%" & Replace(LCase(Val), "'", String.Empty).Trim & "%')"
                                            Else
                                                somestring &= separator & " " & DirectCast(indexColName, String) & " " & Op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                                            End If
                                        Else
                                            separator = " or "
                                            If FlagCase AndAlso (IsNumeric(Results_Business.ReplaceChar(Replace(LCase(Val), "'", String.Empty).Trim)) = False) Then
                                                somestring &= separator & " lower(" & indexColName & ") " & Op & " ('%" & Replace(LCase(Val), "'", String.Empty).Trim & "%')"
                                            Else
                                                somestring &= separator & " " & indexColName & " " & Op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                            If Not String.IsNullOrEmpty(somestring) Then ColumCondstring.Append(somestring & ")")
                            SomeValues = Nothing
                            somestring = Nothing

                        Case "dentro"
                            If FlagCase AndAlso (IsNumeric(Results_Business.ReplaceChar(LCase(mainVal))) = False) Then
                                ColumCondstring.Append(" (lower(" & indexColName & ") in (" & LCase(mainVal) & "))")
                            Else
                                ColumCondstring.Append(" (" & indexColName & " in (" & mainVal & "))")
                            End If

                        Case "sql sin atributo"
                            ColumCondstring.Append(" (" & mainVal & ")")
                        Case "sql"
                            ColumCondstring.Append(" (" & indexColName & " in (" & mainVal & "))")

                    End Select

                    Op = Nothing
                    separator = Nothing
                End If
            End If

            If Indexs(i).OrderSort = OrderSorts.ASC Then Orderstring.Append(", " & indexColName & " ASC ")
            If Indexs(i).OrderSort = OrderSorts.DESC Then Orderstring.Append(", " & indexColName & " DESC ")
        End Sub
        Private Sub createKFilterWhereSearch(EntityId As Int64, ByRef sbValue As StringBuilder, ByVal Indexs As Generic.List(Of IIndex), ByRef FlagCase As Boolean, ByRef ColumCondstring As StringBuilder, ByRef Orderstring As StringBuilder, ByRef First As Boolean, ByRef dateDeclarationString As StringBuilder, ByVal tablePrefix As String, ByVal SearchsEntities As List(Of Int64), kFilter As ikendoFilter)

            Dim tempVal As Object = Nothing
            Dim sbDate As New StringBuilder

            If (GridColumns.ZambaColumns.ContainsKey(kFilter.Field)) Then
                kFilter.DataBaseColumn = GridColumns.ZambaColumns(kFilter.Field)
                If kFilter.DataBaseColumn = "NAME" Then
                    kFilter.DataBaseColumn = "T.NAME"
                End If
            End If

            Dim separator As String = " And "

            sbValue.Remove(0, sbValue.Length)

            If ColumCondstring.Length > 0 Then
                ColumCondstring.Append(separator)
            End If

            Dim op As String = " Like "

            Select Case kFilter.Operator
                Case "eq"
                    op = "="
                    If FlagCase AndAlso (IsNumeric(Results_Business.ReplaceChar(kFilter.Value) = False)) Then
                        ColumCondstring.Append(" (lower(" & kFilter.DataBaseColumn & ")='" & kFilter.Value.ToLower() & "') ")
                    Else
                        ColumCondstring.Append(" (" & kFilter.DataBaseColumn & "='" & kFilter.Value & "') ")
                    End If
                Case "neq"
                    op = "<>"
                    ColumCondstring.Append(" (" & kFilter.DataBaseColumn & "<>(" & kFilter.Value & ") or " & kFilter.DataBaseColumn & " is null) ")

                Case "gte", "gt", "lt", "lte"
                    op = kFilter.Operator.Replace("gt", ">").Replace("gte", ">=").Replace("lt", "<").Replace("lte", "<=")
                    ColumCondstring.Append(" (" & kFilter.DataBaseColumn & op & "(" & kFilter.Value & ")) ")

                Case "isnull"
                    op = "is null"
                    ColumCondstring.Append(" (" & kFilter.DataBaseColumn & " is null) ")

                'Case "Entre"
                '    Dim Data2Added As Boolean
                '    Try
                '        'cambio las a como indice a I ya que todos los indices vienen con dato algunos vacio otros no.
                '        Select Case Indexs(i).Type
                '            Case 1, 2, 3, 6
                '                If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                '                    If Indexs(i).Type = IndexDataType.Numerico OrElse
                '                        Indexs(i).Type = IndexDataType.Numerico_Largo Then
                '                        tempVal = Int64.Parse(Indexs(i).Data2)
                '                    Else
                '                        tempVal = CDec(Indexs(i).Data2)
                '                    End If
                '                    Data2Added = True
                '                End If
                '            Case 4, 5
                '                If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                '                    If Not String.IsNullOrEmpty(sbValue.ToString.Trim) Then
                '                        If Server.isSQLServer Then
                '                            tempVal = "@fechasta" & kFilter.DataBaseColumn
                '                            sbDate.Remove(0, sbDate.Length)
                '                            sbDate.Append("declare " & tempVal & " datetime ")

                '                            If Indexs(i).Type = IndexDataType.Fecha Then
                '                                sbDate.Append("set " & tempVal & " = " & Server.Con.ConvertDate(Indexs(i).Data2) & " ")
                '                            Else
                '                                sbDate.Append("set " & tempVal & " = " & Server.Con.ConvertDateTime(Indexs(i).Data2) & " ")
                '                            End If

                '                            dateDeclarationString.Append(sbDate)
                '                        Else
                '                            If Indexs(i).Type = IndexDataType.Fecha Then
                '                                tempVal = Server.Con.ConvertDate(Indexs(i).Data2)
                '                            Else
                '                                tempVal = Server.Con.ConvertDateTime(Indexs(i).Data2)
                '                            End If
                '                        End If
                '                    End If
                '                    Data2Added = True
                '                End If
                '            Case 7, 8
                '                If Not String.IsNullOrEmpty(Indexs(i).Data2) Then
                '                    FlagCase = True
                '                    tempVal = "'" & LCase(DirectCast(Indexs(i).Data2, String)) & "'"
                '                    Data2Added = True
                '                End If
                '        End Select
                '    Catch ex As Exception
                '        Throw New Exception("Ocurrio un error al convertir al tipo de Dato: Dato: " & sbValue.ToString & ", Tipo Dato: " & Indexs(i).Type & " " & ex.ToString)

                '    End Try

                '    If Data2Added = True Then
                '        If Server.isSQLServer And (Indexs(i).Type = IndexDataType.Fecha OrElse Indexs(i).Type = IndexDataType.Fecha_Hora) Then
                '            ColumCondstring.Append(" (" & kFilter.DataBaseColumn & " BETWEEN (" & kFilter.value & ") and (" & tempVal & "))")
                '        Else
                '            ColumCondstring.Append(" (" & kFilter.DataBaseColumn & ">=(" & kFilter.value & ") and " & kFilter.DataBaseColumn & "<=(" & tempVal & "))")
                '        End If
                '        Data2Added = False
                '    End If

                Case "contains"
                    While kFilter.Value.Contains("  ")
                        kFilter.Value.Replace("  ", " ")
                    End While
                    kFilter.Value = kFilter.Value.Trim.Replace(" ", "%")

                    If FlagCase AndAlso (IsNumeric(Results_Business.ReplaceChar(Replace(Trim(kFilter.Value.ToLower()), "'", String.Empty)) = False)) Then
                        ColumCondstring.Append(" (lower(" & kFilter.DataBaseColumn & ") Like '%" & Replace(Trim(kFilter.Value.ToLower()), "'", String.Empty) & "%')")
                    Else
                        ColumCondstring.Append(" (" & kFilter.DataBaseColumn & " Like '%" & Replace(Trim(kFilter.Value), "'", String.Empty) & "%')")
                    End If

                Case "startswith"
                    If FlagCase Then
                        ColumCondstring.Append(" (lower(" & kFilter.DataBaseColumn & ") Like '" & Replace(Trim(kFilter.Value.ToLower()), "'", String.Empty) & "%')")
                    Else
                        ColumCondstring.Append(" (" & kFilter.DataBaseColumn & " Like '" & Replace(Trim(kFilter.Value), "'", String.Empty) & "%')")
                    End If

                Case "endswith"
                    If FlagCase Then
                        ColumCondstring.Append(" (lower(" & kFilter.DataBaseColumn & ") Like '%" & Replace(Trim(kFilter.Value.ToLower()), "'", String.Empty) & "')")
                    Else
                        ColumCondstring.Append(" (" & kFilter.DataBaseColumn & " Like '%" & Replace(Trim(kFilter.Value), "'", String.Empty) & "')")
                    End If

                'Case "Alguno"
                '    op = "LIKE"
                '    kFilter.value = kFilter.value.Replace(";", ",")
                '    While kFilter.value.Contains("  ")
                '        kFilter.value.Replace("  ", " ")
                '    End While
                '    kFilter.value = kFilter.value.Trim.Replace(" ", ",")

                '    Dim SomeValues As Array = DirectCast(kFilter.value, String).Split(",")
                '    Dim somestring As String = String.Empty
                '    For x As Int32 = 0 To SomeValues.Length - 1
                '        Dim Val As String = SomeValues(x)
                '        If IsNothing(Val) = False AndAlso Not String.IsNullOrEmpty(Val.Trim) Then
                '            If i = 0 AndAlso x = 0 Then
                '                If FlagCase Then
                '                    somestring = " (lower(" & kFilter.DataBaseColumn & ") " & op & " ('%" & Val.Replace("'", String.Empty) & "%')"
                '                Else
                '                    somestring = " (" & kFilter.DataBaseColumn & " " & op & " ('%" & Val.Replace("'", String.Empty) & "%')"
                '                End If
                '            ElseIf x > 0 Then
                '                If String.IsNullOrEmpty(somestring) Then
                '                    If FlagCase Then
                '                        somestring &= separator & " lower(" & DirectCast(kFilter.DataBaseColumn, String) & ") " & op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                '                    Else
                '                        somestring &= separator & " " & DirectCast(kFilter.DataBaseColumn, String) & " " & op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                '                    End If
                '                Else
                '                    separator = " or "
                '                    If FlagCase Then
                '                        somestring &= separator & " lower(" & kFilter.DataBaseColumn & ") " & op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                '                    Else
                '                        somestring &= separator & " " & kFilter.DataBaseColumn & " " & op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                '                    End If
                '                End If
                '            End If
                '        End If
                '    Next
                '    If Not String.IsNullOrEmpty(somestring) Then ColumCondstring.Append(somestring & ")")
                '    SomeValues = Nothing
                '    somestring = Nothing

                Case "doesnotcontain"
                    op = "NOT LIKE"
                    kFilter.Value = kFilter.Value.Replace(";", ",")
                    While kFilter.Value.Contains("  ")
                        kFilter.Value = kFilter.Value.Replace("  ", " ")
                    End While
                    kFilter.Value = kFilter.Value.Trim.Replace(" ", ",")

                    Dim SomeValues As Array = DirectCast(kFilter.Value, String).Split(",")
                    Dim somestring As String = String.Empty
                    For x As Int32 = 0 To SomeValues.Length - 1
                        Dim Val As String = SomeValues(x)
                        If IsNothing(Val) = False AndAlso Not String.IsNullOrEmpty(Val.Trim) Then
                            If x = 0 Then
                                If FlagCase Then
                                    somestring = " (lower(" & kFilter.DataBaseColumn & ") " & op & " ('%" & Val.ToLower().Replace("'", String.Empty) & "%')"
                                Else
                                    somestring = " (" & kFilter.DataBaseColumn & " " & op & " ('%" & Val.Replace("'", String.Empty) & "%')"
                                End If
                            ElseIf x = 0 Then
                                If FlagCase Then
                                    somestring = " (lower(" & kFilter.DataBaseColumn & ") " & op & " ('%" & Val.ToLower().Replace("'", String.Empty) & "%')"
                                Else
                                    somestring = " (" & kFilter.DataBaseColumn & " " & op & " ('%" & Val.Replace("'", String.Empty) & "%')"
                                End If
                            Else
                                If String.IsNullOrEmpty(somestring) Then
                                    If FlagCase Then
                                        somestring &= separator & " lower(" & DirectCast(kFilter.DataBaseColumn, String) & ") " & op & " ('%" & Replace(Val.ToLower(), "'", String.Empty).Trim & "%')"
                                    Else
                                        somestring &= separator & " " & DirectCast(kFilter.DataBaseColumn, String) & " " & op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                                    End If
                                Else
                                    separator = " or "
                                    If FlagCase Then
                                        somestring &= separator & " lower(" & kFilter.DataBaseColumn & ") " & op & " ('%" & Replace(Val.ToLower(), "'", String.Empty).Trim & "%')"
                                    Else
                                        somestring &= separator & " " & kFilter.DataBaseColumn & " " & op & " ('%" & Replace(Val, "'", String.Empty).Trim & "%')"
                                    End If
                                End If
                            End If
                        End If
                    Next
                    If Not String.IsNullOrEmpty(somestring) Then ColumCondstring.Append(somestring & ")")
                    SomeValues = Nothing
                    somestring = Nothing

                Case "Dentro"
                    If FlagCase Then
                        ColumCondstring.Append(" (lower(" & kFilter.DataBaseColumn & ") in (" & kFilter.Value.ToLower() & "))")
                    Else
                        ColumCondstring.Append(" (" & kFilter.DataBaseColumn & " in (" & kFilter.Value & "))")
                    End If

                Case "SQL Sin Atributo"
                    ColumCondstring.Append(" (" & kFilter.Value & ")")

                Case "SQL"
                    ColumCondstring.Append(" (" & kFilter.DataBaseColumn & " in (" & kFilter.Value & "))")

            End Select

            op = Nothing
            separator = Nothing


        End Sub

        Public Function IsNullString()
            If Server.isOracle Then
                Return "NVL("
            Else
                Return "IsNUll("
            End If

        End Function

        ''' <summary>
        ''' Armado del select que se utiliza para las busquedas
        ''' </summary>
        ''' <param name="strTable">Nombre de la tabla. Ej: doc58</param>
        ''' <param name="Indexs">Indices que se van a traer</param>
        ''' <returns>Stringbuilder con la consulta</returns>
        ''' <history>   Marcelo    Created     20/08/09</history>
        ''' <remarks></remarks>
        ''' 
        Dim Flagdocid As Boolean
        Private Sub CreateSelectSearch(docType As IDocType,
                                       ByVal Indexs As List(Of IIndex),
                                       ByVal DtoOnly As Boolean,
                                       internalOrderBy As String,
                                       Search As ISearch,
                                       ByRef strselect As StringBuilder,
                                       ByRef strPREselect As StringBuilder,
                                       ByRef strPOSTselect As StringBuilder,
                                       ByVal isFavoriteSearch As Boolean,
                                       ByVal whereStr As String,
                                       Optional refIndexs As List(Of ReferenceIndex) = Nothing, Optional onlyImportants As Boolean = False)

            Dim withnolock = If(Server.isSQLServer, " with(nolock) ", " ")
            Dim strTableT As String = MakeTable(docType.ID, TableType.Document)
            Dim MainJoin As String = String.Format(" inner join {0} T  " & withnolock & " on T.doc_id = I.doc_id", strTableT)
            Dim concatString As String = If(Server.isOracle, "||", "+")

            Dim autoSubstitutionIndexes As New List(Of IIndex)
            If String.IsNullOrEmpty(internalOrderBy) Then
                internalOrderBy = "I.Doc_Id Desc"
            End If

            strselect.Append("SELECT I.DOC_ID, '' as THUMB ")
            strPREselect.Append("I.DOC_ID, I.THUMB,")

            If DtoOnly AndAlso Server.isSQLServer Then
                strPREselect.Append($"(LTRIM(RTRIM(DISK_VOL_PATH)) + '\' + convert(nvarchar,{docType.ID}) + '\' + convert(nvarchar,OFFSET) + '\' + LTRIM(RTRIM(DOC_FILE))) as FullPath")
            ElseIf DtoOnly AndAlso Server.isOracle Then
                strPREselect.Append($"(LTRIM(RTRIM(DISK_VOL_PATH)) || '\' || {docType.ID} || '\' || OFFSET || '\' || LTRIM(RTRIM(DOC_FILE))) as FullPath")
            Else
                strPREselect.Append("T.DISK_GROUP_ID,VOL_ID,RTRIM(LTRIM(DOC_FILE)) AS DOC_FILE,OFFSET")
            End If

            strselect.Append($", {docType.ID} as DOC_TYPE_ID ")
            strPREselect.Append(", I.DOC_TYPE_ID ")

            If Search.SearchType = SearchTypes.AsignedTasks AndAlso Not IsNothing(Search.UserId) Then
                strselect.Append(", LTRIM(RTRIM(WD.NAME)) as NAME")
                strPREselect.Append(", I.NAME")
            Else
                strPREselect.Append(", LTRIM(RTRIM(T.NAME)) as NAME")
                If Not DtoOnly Then
                    strPREselect.Append(" ,PLATTER_ID,SHARED,ver_Parent_id,RootId,disk_Vol_id, LTRIM(RTRIM(DISK_VOL_PATH)) AS DISK_VOL_PATH")
                End If
            End If



            strPREselect.Append(",I.Execution, I.Do_State_ID, Task_Id, I.STEP_ID, I.work_id")

            strPREselect.Append($", {IsNullString()}ws.Name,'')  as STEP, {IsNullString()} wss.Name ,'')  as State, '{docType.Name}' as ENTIDAD ")

            strselect.Append($", {IsNullString()}User_Asigned ,0) as User_Asigned")
            strPREselect.Append(",I.User_Asigned")

            If Search.SearchType = SearchTypes.AsignedTasks AndAlso Not IsNothing(Search.UserId) Then
                If Not isFavoriteSearch Then
                    strPREselect.Append($" ,{IsNullString()}U.NAME, '')  AssignedTo")
                Else
                    strPREselect.Append($" ,{IsNullString()}(select {IsNullString()} favorite, 0) from DocumentLabels DL  " & withnolock & $" where DL.doctypeid = {docType.ID}")
                    strPREselect.Append($" And DL.docid = I.Doc_ID And DL.userid = {Search.UserId}")
                    strPREselect.Append(" And (FAVORITE = 1 Or IMPORTANCE = 1) ),0) IsFavorite")
                End If
                strPREselect.Append($" ,{IsNullString()}ww.Name, '')  Workflow")
            Else
                strPREselect.Append($" ,{IsNullString()}U.NAME, '')  AssignedTo, {IsNullString()}ww.Name, '')  Workflow")
            End If

            strselect.Append($", {IsNullString()}Task_State_ID,0)  as Execution, {IsNullString()}Do_State_ID ,0) as Do_State_ID, ")
            strselect.Append($"{IsNullString()}wd.TASK_ID ,0) as Task_Id, {IsNullString()}wd.STEP_ID,0) as STEP_ID, {IsNullString()}wd.work_id")
            strselect.Append($",0) as work_id")

            If Server.isOracle Then
                strPREselect.Append(",get_filename(LTRIM(RTRIM(original_Filename)))")
            Else
                strPREselect.Append(",REVERSE(SUBSTRING(REVERSE(LTRIM(RTRIM(original_Filename))), 0, CHARINDEX('\', REVERSE(LTRIM(RTRIM(original_Filename))))))")
            End If

            strPREselect.Append(" as ORIGINAL ")

            strPREselect.Append(",T.ICON_ID")

            ' If Search.SearchType = SearchTypes.AsignedTasks AndAlso Not IsNothing(Search.UserId) Then
            strselect.Append(", checkin as INGRESO")
            strPREselect.Append(", I.INGRESO")
            ' Else
            '                strPREselect.Append(", t.ICON_ID")
            '   End If

            strselect.Append(", i.crdate as CREADO, i.lupdate as MODIFICADO")
            strPREselect.Append(",I.CREADO, I.MODIFICADO")


            Dim UseIndexsRights As Boolean = New RightsBusiness().GetUserRights(Search.UserId, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, docType.ID)
            Dim FirstTryError As Boolean = False
            Dim IndexsRights As Hashtable = Nothing

ReloadCache:

            Dim refIndexsQueryHelper As ReferenceIndexQueryHelper
            If refIndexs IsNot Nothing Then
                refIndexsQueryHelper = New ReferenceIndexQueryHelper
            End If

            If UseIndexsRights Then
                IndexsRights = New UserBusiness().GetIndexsRights(docType.ID, Search.UserId)
            End If





            'ML Falta ordenar los Indices por Orden de Admin.
            For Each _Index As Index In Indexs

                If UseIndexsRights Then

                    Dim IR As IndexsRightsInfo = DirectCast(IndexsRights(_Index.ID), IndexsRightsInfo)
                    If IR Is Nothing AndAlso FirstTryError = False Then
                        FirstTryError = True
                        CacheBusiness.ClearRightsCaches(Search.UserId)
                        If UseIndexsRights Then
                            IndexsRights = Nothing
                            IndexsRights = New UserBusiness().GetIndexsRights(docType.ID, Search.UserId)
                        End If
                    End If

                    If IR IsNot Nothing AndAlso IR.GetIndexRightValue(RightsType.TaskGridIndexView) = True Then

                        If Search.SearchType <> SearchTypes.AsociatedSearch OrElse IsAssociatedIndexVisible(Search.ParentEntity.ID, docType.ID, _Index.ID, Search.UserId) Then

                            If _Index.isReference Then
                                strselect.Append($",{refIndexsQueryHelper.GetStringForDocIQuery(_Index.ID, docType.ID, refIndexs)} AS ")

                                If _Index.DropDown = IndexAdditionalType.AutoSustitución OrElse _Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                    autoSubstitutionIndexes.Add(_Index)
                                    strselect.Append($"I{_Index.ID}")
                                    strPREselect.Append($",I.I{_Index.ID}")

                                    strPREselect.Append($", slst_s{_Index.ID}.Descripcion as [{_Index.Name}]")
                                Else
                                    strselect.Append($"[{_Index.Name}]")
                                    strPREselect.Append($",[{_Index.Name}]")

                                End If

                            Else

                                strselect.Append($",I.I{_Index.ID}")

                                If _Index.DropDown = IndexAdditionalType.AutoSustitución OrElse _Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                                    strPREselect.Append($",I.I{_Index.ID}")
                                    strPREselect.Append($", slst_s{_Index.ID}.Descripcion as [{_Index.Name}]")
                                    autoSubstitutionIndexes.Add(_Index)
                                Else
                                    strselect.Append($" as [{_Index.Name}]")
                                    strPREselect.Append($",[{_Index.Name}]")
                                End If

                            End If

                        End If
                    End If

                Else

                    If _Index.isReference Then

                        strselect.Append($",{refIndexsQueryHelper.GetStringForDocIQuery(_Index.ID, docType.ID, refIndexs)} AS ")

                        If _Index.DropDown = IndexAdditionalType.AutoSustitución OrElse _Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                            autoSubstitutionIndexes.Add(_Index)
                            strselect.Append($"I{_Index.ID}")
                            strPREselect.Append($",I.I{_Index.ID}")
                            strPREselect.Append($", slst_s{_Index.ID}.Descripcion as [{_Index.Name}]")
                        Else
                            strselect.Append($"[{_Index.Name}]")
                            strPREselect.Append($",[{_Index.Name}]")
                        End If

                    Else

                        strselect.Append($",I.I{_Index.ID}")

                        If _Index.DropDown = IndexAdditionalType.AutoSustitución OrElse _Index.DropDown = IndexAdditionalType.AutoSustituciónJerarquico Then
                            strPREselect.Append($",I.I{_Index.ID}")
                            strPREselect.Append($", slst_s{_Index.ID}.Descripcion as [{_Index.Name}]")
                            autoSubstitutionIndexes.Add(_Index)
                        End If

                        If _Index.DropDown <> IndexAdditionalType.AutoSustitución AndAlso _Index.DropDown <> IndexAdditionalType.AutoSustituciónJerarquico Then
                            strselect.Append($" as [{_Index.Name}]")
                            strPREselect.Append($", [{_Index.Name}]")
                        End If

                    End If

                End If
            Next





            ' 1 = true = leido / 0 = false = No Leido
            If Server.isOracle Then
                strPREselect.Append(" ,NVL2(dRead.DOCID, 1,0) AS LEIDO")
            Else
                strPREselect.Append(", CASE WHEN dRead.DOCID Is NULL THEN 0 Else 1 End AS LEIDO")
            End If

            'Se agrega la paginacion, ademas el ordenamiento iria acá
            strselect.Append(" ,row_number() over (order by ")

            If internalOrderBy.ToLower.Contains("etapa") Then
                strselect.Append($"ws.Name) rn FROM DOC_I{docType.ID} I " & withnolock)
            Else
                If internalOrderBy.ToLower.Contains("state") Then
                    strselect.Append($"wss.Name) rn FROM DOC_I{docType.ID} I " & withnolock)
                Else
                    strselect.Append($"{CreateInternalOrderBy(internalOrderBy)}) rn FROM DOC_I{docType.ID} I " & withnolock)
                End If

            End If


            If refIndexs IsNot Nothing Then
                Dim joinStr As String
                For Each refInd As ReferenceIndex In refIndexs
                    joinStr = refIndexsQueryHelper.GetStringJoinQuery(refInd.IndexId, docType.ID, refIndexs)
                    If Not strselect.ToString().ToLower().Contains(joinStr.ToLower.Trim) Then
                        strselect.Append($"{joinStr} ")
                    End If
                Next
            End If

            strPOSTselect.Append($"{MainJoin} left outer join doc_type  " & withnolock & " On doc_type.doc_type_id = T.doc_type_id left outer join disk_Volume  " & withnolock & " On disk_Vol_id = vol_id ")

            strselect.Append($" left outer join WFDOCUMENT wd  " & withnolock & $" On I.DOC_ID = wd.DOC_ID and wd.doc_type_id = {docType.ID} ")
            strselect.Append($" left outer join doc_t{docType.ID} T  " & withnolock & " On I.DOC_ID = T.DOC_ID")
            strselect.Append($" left outer join usrtable usr" & withnolock & " On isNull(wd.User_Asigned, 0) = usr.ID")
            strselect.Append($" left outer join wfstepstates wss" & withnolock & " ON isNull(wd.do_state_id, 0) = wss.doc_state_id")

            If internalOrderBy.ToLower.Contains("etapa") Then
                strselect.Append(" left outer join WFSTEP ws  " & withnolock & " On step_id = ws.step_id ")
            End If

            'If internalOrderBy.ToLower.Contains("state") Then
            '    strselect.Append(" left outer join WFSTEPSTATES wss  " & withnolock & " On do_state_id = wss.doc_state_id ")
            'End If


            If autoSubstitutionIndexes.Count > 0 Then
                For Each currentIndex As IIndex In autoSubstitutionIndexes
                    If currentIndex.isReference Then
                        Dim FirstSideStringJoinQuery As String = refIndexsQueryHelper.GetFirstSideStringJoinQuery(currentIndex.ID, docType.ID, refIndexs)
                        'Joins externos para las slst
                        strPOSTselect.Append($" left outer join slst_s{currentIndex.ID}  " & withnolock & $" On i.i{currentIndex.ID} = slst_s{currentIndex.ID}.codigo ")

                        'Si hay filtro u ordenamiento por slst se agrega join interno
                        If internalOrderBy.ToLower.Contains($"slst_s{currentIndex.ID}") OrElse whereStr.ToLower.Contains($"slst_s{currentIndex.ID}") Then
                            strselect.Append($" left outer join slst_s{currentIndex.ID}  " & withnolock & $" On {FirstSideStringJoinQuery} = slst_s{currentIndex.ID}.codigo ")
                        End If

                    Else
                        'Joins externos para las slst
                        strPOSTselect.Append($" left outer join slst_s{currentIndex.ID}  " & withnolock & $" On i.i{currentIndex.ID} = slst_s{currentIndex.ID}.codigo ")

                        'Si hay filtro u ordenamiento por slst se agrega join interno
                        If internalOrderBy.ToLower.Contains($"slst_s{currentIndex.ID}") OrElse whereStr.ToLower.Contains($"slst_s{currentIndex.ID}") Then
                            strselect.Append($" left outer join slst_s{currentIndex.ID}  " & withnolock & $" On i.i{currentIndex.ID} = slst_s{currentIndex.ID}.codigo ")
                        End If

                    End If

                Next
            End If

            strPOSTselect.Append(" left join WFWORKFLOW ww  " & withnolock & " On i.work_id = ww.work_id ")
            strPOSTselect.Append(" left outer join WFSTEP ws  " & withnolock & " On i.step_id = ws.step_id ")
            strPOSTselect.Append(" left outer join WFSTEPSTATES wss  " & withnolock & " On i.do_state_id = wss.doc_state_id ")
            strPOSTselect.Append(" left outer join zuser_or_group u  " & withnolock & " On i.User_asigned = u.id ")
            strPOSTselect.Append($" LEFT JOIN ZDOCREADS dRead  " & withnolock & $" On i.DOC_ID = dRead.DOCID And dRead.userid = {Search.UserId}")

            If Search.SearchType = SearchTypes.AsignedTasks AndAlso Not IsNothing(Search.UserId) Then

                ' strPOSTselect.Append(" LEFT JOIN ZVW_USR_R_GROUPSANDTHEIRINH ZVW  " & withnolock & " On I.USER_ASIGNED = ZVW.GROUPID And I.USER_ASIGNED = Zvw.Usrid")

                Dim usrsAndGroupsStr As StringBuilder = New StringBuilder()

                If (Search.View.Contains("MyTasks") AndAlso Not Search.View.Contains("ViewAllMy")) Then
                    usrsAndGroupsStr.Append(Search.UserId.ToString() & ",")
                Else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Point of posible ERROR 1")
                End If

                If ((Search.View.Contains("MyTeam") OrElse Search.View.Contains("MyAllTeam")) AndAlso Not Search.View.Contains("ViewAllMy")) Then
                    Dim UserBusiness As New UserBusiness
                    Dim _user As IUser = UserBusiness.GetUserById(Search.UserId)
                    For Each group As IUserGroup In _user.Groups
                        usrsAndGroupsStr.Append(group.ID.ToString & ",")
                    Next
                Else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Point of posible ERROR 2")
                End If

                If usrsAndGroupsStr.Length > 0 Then
                    usrsAndGroupsStr.Remove(usrsAndGroupsStr.Length - 1, 1)
                    If (strselect.ToString.Contains("WHERE")) Then
                        strselect.Append(" And ")
                    Else
                        strselect.Append(" WHERE ")
                    End If
                    strselect.Append(" ( ")
                    If (isFavoriteSearch = False) Then
                        strselect.Append(String.Format("  User_Asigned In ({0})  ", usrsAndGroupsStr.ToString()))

                        If Search.View.Contains("MyAllTeam") Then
                            Dim UP As New UserPreferences
                            Dim MyTeams As String = UP.getValue("MyTeams", UPSections.UserPreferences, "", Search.UserId)
                            UP = Nothing
                            If MyTeams IsNot Nothing AndAlso MyTeams <> String.Empty Then
                                strselect.Append(String.Format(" or User_Asigned In (select usrid from usr_r_group  {1} where groupid in ({0})  ", MyTeams, withnolock))
                                strselect.Append("  )")
                            End If
                        End If
                    End If
                    strselect.Append("  )")
                Else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Point of posible ERROR 3")
                End If

                If isFavoriteSearch Then
                    strselect.Append("  exists(select 'x' from DocumentLabels DL  " & withnolock & " where dl.docid = i.doc_id and dl.userid = ")
                    strselect.Append(Search.UserId)
                    strselect.Append(" and dl.doctypeid = ")
                    strselect.Append(docType.ID)
                    strselect.Append(" and ( FAVORITE = 1 or IMPORTANCE = 1 )) ")
                End If

                If onlyImportants Then
                    strselect.Append("  exists(select 'x' from DocumentLabels DL  " & withnolock & " where dl.docid = i.doc_id ")
                    strselect.Append(" and dl.doctypeid = ")
                    strselect.Append(docType.ID)
                    strselect.Append(" and ( IMPORTANCE = 1 )) ")
                End If

            End If

            If Search.View IsNot Nothing AndAlso Search.View.Contains("View=") Then
                Dim View As String = Search.View.Replace("View=", "")
                Dim UP As New UserPreferences
                Dim ViewScript As String = UP.getValue(View, UPSections.Views, "", Search.UserId)
                If ViewScript.Length > 0 Then
                    If (strselect.ToString.Contains("WHERE")) Then
                        strselect.Append(" And ")
                    Else
                        strselect.Append(" WHERE ")
                    End If
                    strselect.Append(ViewScript)
                End If
            End If

            Dim allFilters As New List(Of IFilterElem)
            Dim stepIdFilters As New List(Of IFilterElem)
            If Search.Doctypes.Count = 1 Then
                allFilters = New FiltersComponent().GetFiltersWebByView(Search.Doctypes.First().ID, Zamba.Membership.MembershipHelper.CurrentUser.ID, Search.View)
                If allFilters IsNot Nothing Then
                    stepIdFilters = allFilters.Where(Function(f) f.Filter = "STEPID" And f.Enabled).Select(Function(f) f).ToList()
                End If
            End If

            If Search.View <> "MyProcess" Then
                Search.StepId = 0
            End If

            If stepIdFilters.Count > 0 Then
                Dim filterValue As String = stepIdFilters.First().Value.Substring(1, stepIdFilters.First().Value.Length - 2)
                Search.StepId = Int64.Parse(filterValue)
            End If

            If Search.StepId > 0 Then
                If strselect.ToString.Contains("WHERE") Then
                    strselect.Append(" And ")
                Else
                    strselect.Append(" WHERE ")
                End If
                strselect.Append($"wd.step_Id in ({Search.StepId}) ")

                If Search.View.ToLower <> "search" Then
                    Dim RiB As New RightsBusiness

                    Dim VerAsignadosAOtros As Boolean = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosAOtros, Search.StepId)
                    Dim VerAsignadosANadie As Boolean = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, Zamba.Core.RightsType.VerAsignadosANadie, Search.StepId)

                    AppendWhereRights(strselect, VerAsignadosANadie, VerAsignadosAOtros)
                End If
            End If

            If Search.StepStateId > 0 Then
                If strselect.ToString.Contains("WHERE") Then
                    strselect.Append(" And ")
                Else
                    strselect.Append(" WHERE ")
                End If
                strselect.Append($"wd.do_state_Id in ({Search.StepStateId}) ")
            End If

            Dim userAssignedFilters As New List(Of IFilterElem)
            userAssignedFilters = allFilters.Where(Function(f) (f.Filter.ToLower = "uag.name" Or f.Filter.ToLower = "asignado" Or f.Filter.ToLower = "assignedto")).Select(Function(f) f).ToList()

            If userAssignedFilters.Count > 0 Then
                If userAssignedFilters.Last().Enabled Then
                    Dim filterValue As String = userAssignedFilters.Last().Value.Substring(1, userAssignedFilters.Last().Value.Length - 2)
                    If filterValue <> "-1" Then
                        If strselect.ToString.Contains("WHERE") Then
                            strselect.Append(" And ")
                        Else
                            strselect.Append(" WHERE ")
                        End If
                        If filterValue = "" And userAssignedFilters.Last().Comparator.ToLower = "es nulo" Then
                            strselect.Append($"(usr.NAME = '{filterValue}' or usr.NAME Is null)")

                        ElseIf filterValue = "" And userAssignedFilters.Last().Comparator.ToLower = "no es nulo" Then
                            strselect.Append("(usr.NAME is not null and RTRIM(LTRIM(usr.NAME)) <> '')")
                        Else

                            strselect.Append($"(usr.NAME = '{filterValue}')")
                        End If
                    End If
                End If
            End If
            'Filtros por columnas propias del sistema Zamba
            Dim crdateFiltersList As New List(Of IFilterElem)
            Dim lupdateFilters As New List(Of IFilterElem)
            Dim nameFilters As New List(Of IFilterElem)
            Dim originalFilenameFilters As New List(Of IFilterElem)
            Dim stateFilters As New List(Of IFilterElem)


            crdateFiltersList = allFilters.Where(Function(f) (f.Filter.ToLower = "i.crdate" Or f.Filter.ToLower = "fecha creacion") And
                                                     f.Enabled And
                                                     f.Comparator.ToLower <> "contiene" And
                                                     f.Comparator.ToLower <> "no contiene").
                                                     Select(Function(f) f).ToList()

            If crdateFiltersList.Count > 0 Then

                If strselect.ToString.Contains("WHERE") Then
                    strselect.Append(" And ")
                Else
                    strselect.Append(" WHERE ")
                End If
                For i As Integer = 0 To crdateFiltersList.Count - 1
                    Dim filterValue As String = crdateFiltersList(i).Value.Substring(1, crdateFiltersList(i).Value.Length - 2)
                    If i > 0 Then
                        strselect.Append(" And ")
                    End If
                    Select Case crdateFiltersList(i).Comparator.ToLower()
                        Case "empieza"
                            If Server.Con.isOracle Then
                                strselect.Append(String.Format("To_date(i.crdate,'DD/MM/YYYY') LIKE '{0}%' ", Server.Con.ConvertDate(filterValue)))
                            Else
                                strselect.Append(String.Format("CAST(i.crdate AS DATE) LIKE '{0}%' ", Server.Con.ConvertDate(filterValue)))
                            End If

                        Case "termina"
                            If Server.Con.isOracle Then
                                strselect.Append(String.Format("To_date(i.crdate,'DD/MM/YYYY') LIKE '%{0}' ", Server.Con.ConvertDate(filterValue)))
                            Else
                                strselect.Append(String.Format("CAST(i.crdate AS DATE) LIKE '%{0}' ", Server.Con.ConvertDate(filterValue)))
                            End If

                        Case "distinto"
                            If Server.Con.isOracle Then
                                strselect.Append(String.Format("To_date(i.crdate,'DD/MM/YYYY') [NOT] LIKE '{0}' ", Server.Con.ConvertDate(filterValue)))
                            Else
                                strselect.Append(String.Format("CAST(i.crdate AS DATE) NOT LIKE '{0}' ", Server.Con.ConvertDate(filterValue)))
                            End If
                        Case "contiene"
                            If Server.Con.isOracle Then
                                strselect.Append(String.Format("To_date(i.crdate,'DD/MM/YYYY') LIKE '%{0}%' ", Server.Con.ConvertDate(filterValue)))
                            Else
                                strselect.Append(String.Format("CAST(i.crdate AS DATE) LIKE '%{0}%' ", Server.Con.ConvertDate(filterValue)))
                            End If
                        Case "es nulo"
                            strselect.Append("(i.crdate is null or i.crdate = '')")
                        Case "no es nulo"
                            strselect.Append("(i.crdate is not null and i.crdate <> '')")
                        Case Else
                            If Server.Con.isOracle Then
                                strselect.Append(String.Format("To_date(i.crdate,'DD/MM/YYYY') {0} {1} ", crdateFiltersList(i).Comparator, Server.Con.ConvertDate(filterValue)))
                            Else
                                strselect.Append(String.Format("CAST(i.crdate AS DATE) {0} {1} ", crdateFiltersList(i).Comparator, Server.Con.ConvertDate(filterValue)))
                            End If

                    End Select
                Next
            End If


            lupdateFilters = allFilters.Where(Function(f) f.Filter = "I.lupdate" And
                                                  f.Enabled And
                                                  f.Comparator.ToLower <> "contiene" And
                                                  f.Comparator.ToLower <> "no contiene").
                                                  Select(Function(f) f).ToList()

            If lupdateFilters.Count > 0 Then
                If strselect.ToString.Contains("WHERE") Then
                    strselect.Append(" And ")
                Else
                    strselect.Append(" WHERE ")
                End If
                For i As Integer = 0 To lupdateFilters.Count - 1
                    Dim filterValue As String = lupdateFilters(i).Value.Substring(1, lupdateFilters(i).Value.Length - 2)
                    If i > 0 Then
                        strselect.Append(" And ")
                    End If
                    Select Case lupdateFilters(i).Comparator.ToLower
                        Case "empieza"
                            If Server.Con.isOracle Then
                                strselect.Append(String.Format("To_date(i.lupdate,'DD/MM/YYYY') LIKE '{0}%' ", Server.Con.ConvertDate(filterValue)))
                            Else
                                strselect.Append(String.Format("CAST(i.lupdate AS DATE) LIKE '{0}%' ", Server.Con.ConvertDate(filterValue)))
                            End If
                        Case "termina"
                            If Server.Con.isOracle Then
                                strselect.Append(String.Format("To_date(i.lupdate,'DD/MM/YYYY') LIKE '%{0}' ", Server.Con.ConvertDate(filterValue)))
                            Else
                                strselect.Append(String.Format("CAST(i.lupdate AS DATE) LIKE '%{0}' ", Server.Con.ConvertDate(filterValue)))
                            End If
                        Case "distinto"
                            If Server.Con.isOracle Then
                                strselect.Append(String.Format("To_date(i.lupdate,'DD/MM/YYYY') [NOT] LIKE '{0}' ", Server.Con.ConvertDate(filterValue)))
                            Else
                                strselect.Append(String.Format("CAST(i.lupdate AS DATE) NOT LIKE '{0}' ", Server.Con.ConvertDate(filterValue)))
                            End If
                        Case "contiene"
                            If Server.isOracle Then
                                strselect.Append(String.Format("To_date(i.lupdate,'DD/MM/YYYY') LIKE '%{0}%' ", Server.Con.ConvertDate(filterValue)))
                            Else
                                strselect.Append(String.Format("CAST(i.lupdate AS DATE) LIKE '%{0}%' ", Server.Con.ConvertDate(filterValue)))
                            End If
                        Case "es nulo"
                            strselect.Append("(i.lupdate is null or i.lupdate = '')")
                        Case "no es nulo"
                            strselect.Append("(i.lupdate is not null and i.lupdate <> '') ")
                        Case Else
                            If Server.isOracle Then
                                strselect.Append(String.Format("To_date(i.lupdate,'DD/MM/YYYY') {0} {1} ", lupdateFilters(i).Comparator, Server.Con.ConvertDate(filterValue)))
                            Else
                                strselect.Append(String.Format("CAST(i.lupdate AS DATE) {0} {1} ", lupdateFilters(i).Comparator, Server.Con.ConvertDate(filterValue)))
                            End If

                    End Select
                Next
            End If

            nameFilters = allFilters.Where(Function(f) (f.Filter.ToUpper = "WD.NAME" Or f.Filter.ToUpper = "NOMBRE DEL DOCUMENTO") And f.Enabled).Select(Function(f) f).ToList()

            If nameFilters.Count > 0 Then
                If strselect.ToString.Contains("WHERE") Then
                    strselect.Append(" And ")
                Else
                    strselect.Append(" WHERE ")
                End If
                For i As Integer = 0 To nameFilters.Count - 1
                    Dim filterValue As String = nameFilters(i).Value.Substring(1, nameFilters(i).Value.Length - 2)
                    If i > 0 Then
                        strselect.Append(" And ")
                    End If
                    Select Case nameFilters(i).Comparator.ToLower
                        Case "empieza"
                            strselect.Append(String.Format("wd.NAME LIKE '{0}%' ", filterValue))
                        Case "termina"
                            strselect.Append(String.Format("wd.NAME LIKE '%{0}' ", filterValue))
                        Case "distinto"
                            If Server.Con.isOracle Then
                                strselect.Append(String.Format("wd.NAME [NOT] LIKE '{0}' ", filterValue))
                            Else
                                strselect.Append(String.Format("wd.NAME NOT LIKE '{0}' ", filterValue))
                            End If
                        Case "no contiene"
                            strselect.Append(String.Format("wd.NAME NOT LIKE '%{0}%' ", filterValue))
                        Case "contiene"
                            strselect.Append(String.Format("wd.NAME LIKE '%{0}%' ", filterValue))
                        Case "es nulo"
                            strselect.Append("(wd.NAME is null or RTRIM(LTRIM(wd.NAME)) = '')")
                        Case "no es nulo"
                            strselect.Append("(wd.NAME is not null and RTRIM(LTRIM(wd.NAME)) <> '')")
                        Case Else
                            strselect.Append(String.Format("wd.NAME {0} '{1}' ", nameFilters(i).Comparator, filterValue))
                    End Select
                Next
            End If

            originalFilenameFilters = allFilters.Where(Function(f) (f.Filter.ToUpper = "ORIGINAL_FILENAME" Or f.Filter.ToUpper = "NOMBRE ORIGINAL") And f.Enabled).Select(Function(f) f).ToList()

            If originalFilenameFilters.Count > 0 Then
                If strselect.ToString.Contains("WHERE") Then
                    strselect.Append(" And ")
                Else
                    strselect.Append(" WHERE ")
                End If
                For i As Integer = 0 To originalFilenameFilters.Count - 1
                    Dim filterValue As String = originalFilenameFilters(i).Value.Substring(1, originalFilenameFilters(i).Value.Length - 2)
                    If i > 0 Then
                        strselect.Append(" And ")
                    End If
                    Select Case originalFilenameFilters(i).Comparator.ToLower
                        Case "empieza"
                            strselect.Append(String.Format("T.Original_filename LIKE '{0}%' ", filterValue))
                        Case "termina"
                            strselect.Append(String.Format("T.Original_filename LIKE '%{0}' ", filterValue))
                        Case "distinto"
                            If Server.Con.isOracle Then
                                strselect.Append(String.Format("T.Original_filename [NOT] LIKE '{0}' ", filterValue))
                            Else
                                strselect.Append(String.Format("T.Original_filename NOT LIKE '{0}' ", filterValue))
                            End If
                        Case "no contiene"
                            strselect.Append(String.Format("T.Original_filename NOT LIKE '%{0}%' ", filterValue))
                        Case "contiene"
                            strselect.Append(String.Format("T.Original_filename LIKE '%{0}%' ", filterValue))
                        Case "es nulo"
                            strselect.Append("(T.Original_filename is null or RTRIM(LTRIM(T.Original_filename)) = '')")
                        Case "no es nulo"
                            strselect.Append("(T.Original_filename is not null and RTRIM(LTRIM(T.Original_filename)) <> '')")
                        Case Else
                            strselect.Append(String.Format("T.Original_filename {0} '{1}' ", originalFilenameFilters(i).Comparator, filterValue))
                    End Select
                Next
            End If

            stateFilters = allFilters.Where(Function(f) f.Description.ToUpper = "ESTADO TAREA" And f.Enabled).Select(Function(f) f).ToList()

            If stateFilters.Count > 0 Then
                If strselect.ToString.Contains("WHERE") Then
                    strselect.Append(" And ")
                Else
                    strselect.Append(" WHERE ")
                End If
                For i As Integer = 0 To stateFilters.Count - 1
                    Dim filterValue As String = stateFilters(i).Value.Substring(1, stateFilters(i).Value.Length - 2)
                    If i > 0 Then
                        strselect.Append(" And ")
                    End If
                    Select Case stateFilters(i).Comparator.ToLower
                        Case "empieza"
                            strselect.Append(String.Format("wss.NAME LIKE '{0}%' ", filterValue))
                        Case "termina"
                            strselect.Append(String.Format("wss.NAME LIKE '%{0}' ", filterValue))
                        Case "distinto"
                            If Server.Con.isOracle Then
                                strselect.Append(String.Format("wss.NAME [NOT] LIKE '{0}' ", filterValue))
                            Else
                                strselect.Append(String.Format("wss.NAME NOT LIKE '{0}' ", filterValue))
                            End If
                        Case "no contiene"
                            strselect.Append(String.Format("wss.NAME NOT LIKE '%{0}%' ", filterValue))
                        Case "contiene"
                            strselect.Append(String.Format("wss.NAME LIKE '%{0}%' ", filterValue))
                        Case "es nulo"
                            strselect.Append("(wss.NAME is null or RTRIM(LTRIM(wss.NAME)) = '')")
                        Case "no es nulo"
                            strselect.Append("(wss.NAME is not null and RTRIM(LTRIM(wss.NAME)) <> '')")
                        Case Else
                            strselect.Append(String.Format("wss.NAME {0} '{1}' ", stateFilters(i).Comparator, filterValue))
                    End Select
                Next
            End If

            If Server.isOracle Then
                strselect.Replace("[", Chr(34))
                strselect.Replace("]", Chr(34))
                strPREselect.Replace("[", Chr(34))
                strPREselect.Replace("]", Chr(34))
                strPOSTselect.Replace("[", Chr(34))
                strPOSTselect.Replace("]", Chr(34))
            Else
                strselect.Replace("NVL(", "IsNull(")
                strPREselect.Replace("NVL(", "IsNull(")
                strPOSTselect.Replace("NVL(", "IsNull(")

                strselect.Replace("nvl(", "IsNull(")
                strPREselect.Replace("nvl(", "IsNull(")
                strPOSTselect.Replace("nvl(", "IsNull(")
            End If

            autoSubstitutionIndexes = Nothing

        End Sub
        Private Function CreateInternalOrderBy(expression As String)
            Dim ArrExpression() As String
            ArrExpression = expression.Split(" ")
            Select Case ArrExpression(1).ToLower
                Case "step_id", "do_state_id", "user_asigned"
                    ArrExpression(1) = "wd." + ArrExpression(1)
            End Select
            expression = Join(ArrExpression, " ")
            Return expression
        End Function


        Public Function IsAssociatedIndexVisible(parentEntityId, EntityId, CurrentIndexId, UserId) As Boolean
            Dim IRI As Hashtable = UB.GetAssociatedIndexsRightsCombined(parentEntityId, EntityId, UserId)
            If (IRI.ContainsKey(CurrentIndexId) AndAlso DirectCast(IRI(CurrentIndexId), AssociatedIndexsRightsInfo).GetIndexRightValue(RightsType.AssociateIndexView)) Then
                Return True
            Else
                Return False
            End If
        End Function


        ''' <summary>
        ''' Agrega los where de permisos al select dado
        ''' 
        ''' </summary>
        ''' <param name="strselect"></param>
        ''' <param name="VerAsignadosANadie"></param>
        ''' <param name="VerAsignadosAOtros"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Sub AppendWhereRights(ByRef strselect As StringBuilder, ByVal VerAsignadosANadie As Boolean,
                                             ByVal VerAsignadosAOtros As Boolean)
            If Not VerAsignadosAOtros AndAlso VerAsignadosANadie Then
                If strselect.ToString.Contains("WHERE") Then
                    strselect.Append(" And ")
                Else
                    strselect.Append(" WHERE ")
                End If

                strselect.Append("  (user_asigned = " & RightFactory.CurrentUser.ID & " Or user_asigned = 0")
                For i As Int32 = 0 To RightFactory.CurrentUser.Groups.Count - 1
                    strselect.Append(" Or user_asigned = ")
                    strselect.Append(RightFactory.CurrentUser.Groups(i).ID())
                Next
                strselect.Append(")")
            ElseIf VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
                If strselect.ToString.Contains("WHERE") Then
                    strselect.Append(" And ")
                Else
                    strselect.Append(" WHERE ")
                End If
                strselect.Append(" user_asigned <> 0")
            ElseIf Not VerAsignadosAOtros AndAlso Not VerAsignadosANadie Then
                If strselect.ToString.Contains("WHERE") Then
                    strselect.Append(" And ")
                Else
                    strselect.Append(" WHERE ")
                End If
                strselect.Append("  (user_asigned = " & RightFactory.CurrentUser.ID)
                For i As Int32 = 0 To RightFactory.CurrentUser.Groups.Count - 1
                    strselect.Append(" Or user_asigned = ")
                    strselect.Append(RightFactory.CurrentUser.Groups(i).ID())
                Next
                strselect.Append(")")
            End If

        End Sub
        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Devuelve un Arraylist de objetos Results obtenidos al ejecutar la sentencia SQL
        ''' </summary>
        ''' <param name="SQL">Sentencia SQL a ejecutar</param>
        ''' <returns>Arraylist de objetos Results</returns>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Hernan]	29/05/2006	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Overloads Function SearchRows(ByVal SQL As String) As DataTable
            Dim dr As IDataReader = Nothing
            Dim dt As DataTable = New DataTable
            Dim con As IConnection = Nothing

            Dim UP As New UserPreferences

            Try
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Devuelve los resultados de la base de datos " & Now.ToString)
                Dim CountTop As Int64 = Int64.Parse(UP.getValue("MaxResults", UPSections.UserPreferences, 100, Membership.MembershipHelper.CurrentUser.ID))
                Dim docTypeId As Int64 = 0
                Dim arraySustIndex As ArrayList = Nothing
                Dim first As Boolean = True
                Dim i As Int32 = 0
                Dim Indexs As List(Of IIndex) = Nothing

                con = Server.Con
                dr = con.ExecuteReader(CommandType.Text, SQL)
                While (dr.Read() AndAlso i < CountTop)
                    i = i + 1
                    If docTypeId <> dr.Item("doc_type_Id") Then
                        Indexs = ZCore.FilterIndex(dr.Item("doc_type_Id"))
                    End If

                    CreateRow(arraySustIndex, dr, Indexs)
                End While

            Finally
                If Not IsNothing(dr) Then
                    dr.Close()
                    dr.Dispose()
                    dr = Nothing
                End If
                If Not IsNothing(con) Then
                    con.Close()
                    con.dispose()
                    con = Nothing
                End If
                UP = Nothing
            End Try
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
        Public Shared Function SearchFileText(ByRef Results As ArrayList, ByVal Text2Search As String, ByVal AllFiles As Boolean) As ArrayList
            Try
                Dim i As Int32
                Dim NewResults As New ArrayList
                Dim CountRows As Int64

                For i = 0 To Results.Count - 1
                    Dim Result As Result = Results(i)
                    Try
                        If FileContainsText(Result.FullPath, Text2Search, AllFiles) Then
                            NewResults.Add(Result)
                            CountRows += 1
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Next
                NewResults.Capacity = CountRows

                Return NewResults
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
            Return Nothing
        End Function
        Public Shared Function FileContainsText(ByVal filename As String, ByVal text As String, ByVal SearchAll As Boolean) As Boolean
            Dim Words() As String = Split(text.Trim, " ")
            Return FileContainsText(filename, Words, SearchAll)
        End Function
        Private Shared Function FileContainsText(ByVal filename As String, ByVal words() As String, ByVal SearchAll As Boolean) As Boolean
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

                    If SearchAll Then
                        If wordFind = words.Length Then
                            Return True
                        End If
                    Else
                        Return True
                    End If
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
        Public Shared Function SearchFileText(ByVal Results As DataTable, ByVal Text2Search As String, ByVal AllFiles As Boolean) As DataTable
            Try
                Dim NewResults As DataTable = Results.Clone()

                For Each Result As DataRow In Results.Rows
                    Try
                        If FileContainsText(getFullPath(Result), Text2Search, AllFiles) Then
                            NewResults.Rows.Add(Result.ItemArray)
                        End If
                    Catch ex As Exception
                        ZClass.raiseerror(ex)
                    End Try
                Next

                Return NewResults
            Catch ex As Exception
                ZClass.raiseerror(ex)
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
        Public Function SearchInNotesAndForo(ByRef Results As ArrayList, ByVal SearchString As String) As ArrayList
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
                        End If
                    Next
                Next

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

            Try
                Dim SelectQuery As String = "SELECT DocId, Mensaje, LinkName FROM ZForum WHERE (lower(Mensaje) Like '%" & LCase(SearchString.Trim) & "%') OR (lower(LinkName) Like '%" & LCase(SearchString.Trim) & "%')"
                Dim DsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, SelectQuery)
                Dim DocIdOrdinal As Int32 = DsTemp.Tables(0).Columns("DOCID").Ordinal

                For Each Row As DataRow In DsTemp.Tables(0).Rows
                    For Each CurrentResult As Result In Results
                        If Row.Item(DocIdOrdinal) = CurrentResult.ID Then
                            NewResults.Add(CurrentResult.ID, CurrentResult)
                        End If
                    Next
                Next

            Catch ex As Exception
                ZClass.raiseerror(ex)
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
                ZClass.raiseerror(ex)
            End Try

            Try
                Dim SelectQuery As String = "SELECT DocId, Mensaje, LinkName FROM ZForum WHERE (lower(Mensaje) Like '%" & LCase(SearchString.Trim) & "%') OR (lower(LinkName) Like '%" & LCase(SearchString.Trim) & "%')"
                Dim DsTemp As DataSet = Server.Con.ExecuteDataset(CommandType.Text, SelectQuery)
                Dim DocIdOrdinal As Int32 = DsTemp.Tables(0).Columns("DOCID").Ordinal

                For Each Row As DataRow In DsTemp.Tables(0).Rows
                    For Each CurrentResult As DataRow In Results.Rows
                        If Row.Item(DocIdOrdinal) = CurrentResult("doc_id") Then
                            NewResults.Rows.Add(CurrentResult.ItemArray())
                        End If
                    Next
                Next
            Catch ex As Exception
                ZClass.raiseerror(ex)
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
        Public Function SearchParentVersions(ByRef Result As Result, ByVal DtoOnly As Boolean) As DataTable
            'Si RootId = 0 <==> Este result no es una version nueva de otro result , la consulta traeria a todos los results que tienen rootId = 0 que son todos los results NO versionados.
            If Result.RootDocumentId = 0 Then Return Nothing

            'TODO Falta que lo saque del dscore
            'TODO Falta que se pueda configurar
            Try
                If IsNothing(Result.DocType) Then
                    Result.DocType = New DocType(Result.DocType.ID, Result.DocType.Name, 0)
                End If

                Return SearchVersions(Result.DocType, Result.RootDocumentId, Result.ID, DtoOnly, Nothing)
            Catch ex As Exception
                ZClass.raiseerror(ex)
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
        Public Function SearchVersions(ByVal docType As DocType, ByVal rootDocumentId As Int64, ByVal docId As Int64, ByVal DtoOnly As Boolean, ByVal search As ISearch) As DataTable

            Dim UP As New UserPreferences
            Try

                Dim strselect As New StringBuilder
                Dim strPREselect As New StringBuilder
                Dim strPOSTselect As New StringBuilder
                Dim IB As New IndexsBusiness
                Dim Indexs As New List(Of IIndex)
                If search.SearchType <> SearchTypes.AsignedTasks Then
                    Indexs = IB.FilterIndexsByUserRights(docType.ID, Membership.MembershipHelper.CurrentUser.ID, docType.Indexs, RightsType.TaskGridIndexView)
                End If
                CreateSelectSearch(docType, Indexs, DtoOnly, String.Empty, search, strselect, strPREselect, strPOSTselect, False, String.Empty)

                If rootDocumentId > 0 Then
                    strselect.Append(" WHERE ")
                    strselect.Append("RootId = ")
                    strselect.Append(rootDocumentId.ToString())
                    strselect.Append(" OR ")
                    strselect.Append("t.doc_id = ")
                    strselect.Append(rootDocumentId.ToString())
                Else
                    strselect.Append(" WHERE ")
                    strselect.Append("t.doc_id = ")
                    strselect.Append(docId.ToString())
                End If

                Dim CountTop As Int64 = Int64.Parse(UP.getValue("MaxResults", UPSections.UserPreferences, 100, search.UserId))
                Dim con As IConnection = Nothing
                Dim dr As IDataReader = Nothing
                Dim dt As New DataTable

                Try
                    dr = Server.Con.ExecuteReader(CommandType.Text, strselect.ToString)

                    Dim arraySustIndex As ArrayList = Nothing
                    Dim i As Int32 = 0
                    Dim first As Boolean = True

                    While (dr.Read() AndAlso i < CountTop)
                        i = i + 1

                        CreateRow(arraySustIndex, dr, docType.Indexs)
                    End While

                Finally
                    If Not IsNothing(dr) Then
                        dr.Close()
                        dr.Dispose()
                        dr = Nothing
                    End If
                    If Not IsNothing(con) Then
                        con.Close()
                        con.dispose()
                        con = Nothing
                    End If
                End Try
                Return dt
            Catch ex As Exception
                ZClass.raiseerror(ex)
            Finally
                UP = Nothing
            End Try
            Return Nothing
        End Function
#End Region

    End Class
End Namespace
