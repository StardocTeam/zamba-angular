Imports Zamba.Core
Imports Zamba.Services
Imports System.Data
Imports System.Web.Configuration
Imports Zamba.AppBlock
Imports Zamba.Membership
Imports Zamba

Partial Class _Default

    Inherits System.Web.UI.Page

    Private _hideColumns As ArrayList
    Private _timeOut As Integer
    Private m_resultsPagingId As Int16
    Private m_pageSize As Int16
    Private Root As TreeNode
    Private uConfig As New SUserPreferences()

    Public Property PageSize() As Int16
        Get
            If Session("PageSize") Is Nothing Then
                m_pageSize = 1
            Else
                m_pageSize = Int16.Parse(Session("PageSize").ToString())
            End If
            Return m_pageSize
        End Get
        Set(value As Int16)
            m_pageSize = value
        End Set
    End Property
    Public Property ResultsPagingId() As Int16
        Get
            If Session("ResultsPagingId") Is Nothing Then
                m_resultsPagingId = 0
            Else
                m_resultsPagingId = Int16.Parse(Session("ResultsPagingId").ToString())
            End If
            Return m_resultsPagingId
        End Get
        Set(value As Int16)
            m_resultsPagingId = value
        End Set
    End Property

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.PreInit

        Page.Theme = Session("CurrentTheme")

        _timeOut = Server.ScriptTimeout
        Server.ScriptTimeout = 3600

        Dim user As IUser = Session("User")

        If user Is Nothing Then
            FormsAuthentication.RedirectToLoginPage()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'En base a userconfig que por defecto estará en false se mostrará la pestaña de Novedades
        Dim sUserPreferences As New Zamba.Services.SUserPreferences()
        Dim UserBusiness As New Zamba.Core.UserBusiness()
        Dim ZOptBusines As New SZOptBusiness()

        If Not Request.QueryString("mode") Is Nothing AndAlso Request.QueryString("mode") = "ajax" Then
            'Saving the variables in session. Variables are posted by ajax.

            If Not Request.Params("SelectedsDocTypesIds") Is Nothing Then
                Dim SelectedsDocTypesIds As New List(Of Int64)
                For Each Id As String In Request.Params("SelectedsDocTypesIds").Split(",")
                    If IsNumeric(Id) Then SelectedsDocTypesIds.Add(Int64.Parse(Id))
                Next
                Session("SelectedsDocTypesIds") = SelectedsDocTypesIds
                ShowIndexs(SelectedsDocTypesIds, WebModuleMode.Search)
                Me.UpdatePanel2.Update()
            End If
        End If


        Try
            Dim sFeedView As Boolean = Boolean.Parse(sUserPreferences.getValue("ViewNewsTabs", Zamba.Core.Sections.UserPreferences, "False"))
            viewsNews.Value = sFeedView.ToString()
            viewInsert.Value = UserBusiness.Rights.GetUserRights(ObjectTypes.InsertWeb, RightsType.View)

            Dim PageTitle As String = ZOptBusines.GetValue("WebViewTitle")
            If String.IsNullOrEmpty(PageTitle) Then
                Me.Title = "Zamba"
            Else
                Me.Title = PageTitle + " - Zamba"
            End If

            If User Is Nothing = False Then
                'Actualiza el timemout
                Dim rights As New SRights()
                Dim type As Int32 = 0
                If MembershipHelper.CurrentUser.WFLic Then
                    type = 1
                End If
                If MembershipHelper.CurrentUser.ConnectionId > 0 Then
                    rights.UpdateOrInsertActionTime(MembershipHelper.CurrentUser.ID, MembershipHelper.CurrentUser.Name, MembershipHelper.CurrentUser.puesto, MembershipHelper.CurrentUser.ConnectionId, Int32.Parse(sUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type)
                Else
                    Response.Redirect("~/Views/Security/LogIn.aspx")
                End If

                AddHandler Arbol.SelectedNodeChanged, AddressOf SelectedNodeChanged
                AddHandler Arbol.WFTreeRefreshed, AddressOf RefreshTaskGrid
                RemoveHandler Arbol.WFTreeIsEmpty, AddressOf WfTreeIsEmpty
                AddHandler Arbol.WFTreeIsEmpty, AddressOf WfTreeIsEmpty
                Arbol.FillWF()
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "TaskCount", "$(document).ready(function () { LoadStepsCounts(); });", True)

                'search
                DocTypesIndexs.ShowHideIndexs.Visible = False
                Dim searchInTasks As String = ZOptBusines.GetValue("TaskSearch")
                If Not [String].IsNullOrEmpty(searchInTasks) Then
                    chkSearchByTask.Visible = [Boolean].Parse(searchInTasks)
                End If
                chkSearchByTask.Checked = [Boolean].Parse(UserPreferences.getValue("SearchInTaks", Sections.Search, True))


                'Solapa Novedades
                If sFeedView Then
                    'Obtiene permiso para ver los feeds
                    sFeedView = New SRights().GetUserRights(ObjectTypes.Feeds, RightsType.View, -1)
                    hdnFView.Value = sFeedView.ToString

                    If sFeedView Then
                        'Obtiene variables de configuracion
                        Dim sFeedRefreshInterval As String = ZOptBusines.GetValue("FeedRefreshInterval")

                        If String.IsNullOrEmpty(sFeedRefreshInterval) Then
                            hdnFRefresh.Value = "5000"
                        Else
                            hdnFRefresh.Value = sFeedRefreshInterval
                        End If

                        Dim sFeedLinesCount As String = ZOptBusines.GetValue("FeedLinesCount")

                        If String.IsNullOrEmpty(sFeedLinesCount) Then
                            hdnFLinesCount.Value = "6"
                        Else
                            hdnFLinesCount.Value = sFeedLinesCount
                        End If
                    End If
                End If
            End If

            If UserPreferences.getValue("OpenRecentTask", Sections.WorkFlow, False) Then
                Dim zopt As New ZOptBusiness
                Dim weblink As String = zopt.GetValue("WebViewPath")
                zopt = Nothing

                If Not String.IsNullOrEmpty(weblink) Then
                    Dim lTasks As DataTable = Zamba.Core.WF.WF.WFTaskBusiness.GetUserOpenedTasks(MembershipHelper.CurrentUser.ID)

                    If lTasks.Rows.Count > 0 Then
                        Dim Script As String = "$(document).ready(function(){"
                        For Each task As DataRow In lTasks.Rows
                            Dim url As String = weblink & "/views/WF/TaskSelector.ashx?taskid=" & task("Task_ID") & "&docid=" & task("Doc_ID") & "&DocTypeId=" & task("doc_type_id").ToString()
                            Script += "AddDocTaskToOpenList(" & task("Task_ID") & ", " & task("Doc_ID") & ", " & task("doc_type_id").ToString() & ", false, '" & task("Name") & "', '" & url & "', " & MembershipHelper.CurrentUser.ID & ");"
                        Next
                        Script += "OpenPendingTabs();});"
                        Page.ClientScript.RegisterStartupScript(Me.Page.GetType, "OpenTask", Script, True)
                    End If

                    lTasks.Dispose()
                    lTasks = Nothing
                End If
            End If

            'Instancio un controller 
            Dim dynamicBtnController As New DynamicButtonController()
            'Pido la vista
            Dim dynBtnView As DynamicButtonPartialViewBase = dynamicBtnController.GetViewHeaderButtons(User)
            'La agrego
            pnlWordSearch.Controls.Add(dynBtnView)

        Catch ex As Exception
            ZClass.raiseerror(ex)
        Finally
            sUserPreferences = Nothing
            ZOptBusines = Nothing
            UserBusiness = Nothing
        End Try
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim zopt As New ZOptBusiness
        Dim weblink As String = zopt.GetValue("WebViewPath")
        zopt = Nothing

        If Not String.IsNullOrEmpty(weblink) Then

            If Page.Request.QueryString.Count > 0 AndAlso Not String.IsNullOrEmpty(Page.Request.QueryString("docid")) Then

                Dim docid As String = Page.Request.QueryString("docid")
                Dim _STask As New STasks
                Dim _task As ITaskResult = _STask.GetTaskByDocId(Int64.Parse(docid))
                _STask = Nothing

                If _task IsNot Nothing Then

                    Dim script As String = "parent.CreateTaskIframe('" & weblink & "/views/WF/TaskSelector.ashx?doctype=" & _task.DocTypeId & "&docid=" & _task.ID & "&taskid=" & _task.TaskId & "&wfstepid=" & _task.StepId & "'," & _task.TaskId & ",'" & _task.Name & "');"
                    Page.ClientScript.RegisterStartupScript(Me.Page.GetType, "OpenTaskLink", script, True)
                    _task = Nothing

                Else

                    Dim script As String = "$(document).ready(function(){toastr.error('El documento es inexistente o no tiene permiso para acceder al mismo');});"
                    Page.ClientScript.RegisterStartupScript(Me.Page.GetType, "ErrorMessage", script, True)

                End If

            End If

        End If
    End Sub

    Protected Sub Page_Unload(sender As Object, e As EventArgs)
        If _timeOut > 0 Then
            Server.ScriptTimeout = _timeOut
        End If
    End Sub

    Private Sub SelectedNodeChanged(ByVal WFId As Int32, ByVal StepId As Int32, ByVal DocTypeId As Int32)
        If Page.IsPostBack Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "TaskCount", "$(document).ready(function () { LoadStepsCounts(); });", True)
        End If

        Dim zoptb As New ZOptBusiness()
        Dim CurrentTheme As String = zoptb.GetValue("CurrentTheme")
        zoptb = Nothing

        If CurrentTheme = "AysaDiseno" Then
            TaskGrid.ClearCurrentFilters(StepId)
            TaskGrid.SetFilters(StepId)
        End If

        TaskGrid.LoadTasks(WFId, StepId, DocTypeId, Arbol.WFTreeView.SelectedNode)
    End Sub

    ''' <summary>
    ''' Handler para una vez finalizado el refresco de wf, refrescar la grilla
    ''' </summary>
    ''' <param name="StepId"></param>
    ''' <remarks></remarks>
    Private Sub RefreshTaskGrid(ByVal StepId As Int32)
        Dim zoptb As New ZOptBusiness()
        Dim CurrentTheme As String = zoptb.GetValue("CurrentTheme")
        zoptb = Nothing

        If CurrentTheme = "AysaDiseno" Then
            TaskGrid.ClearCurrentFilters(StepId)
            TaskGrid.SetFilters(StepId)
        End If

        TaskGrid.RebindGrid()
    End Sub



    ''' <summary>
    ''' Metodo que se utiliza para atrapar el evento que el arbol esta vacio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub WfTreeIsEmpty()
        UpdTaskGrid.Visible = False
        lblNoWFVisible.Visible = True
    End Sub



    ''' <summary>
    ''' Genera los nodos hijos de un id dado, en base a la tabla de secciones.
    ''' </summary>
    ''' <param name="ParentID"></param>
    ''' <param name="TableToBuild"></param>
    ''' <returns></returns>
    Private Function LoadChildTree(ParentID As Integer, TableToBuild As Data_Group_Doc.Doc_Type_GroupDataTable) As TreeNodeCollection
        Try
            'Obtenemos la tabla con los archivos hijos.
            Dim childsArchives As Data_Group_Doc.Doc_Type_GroupDataTable = GetChildArchives(ParentID, TableToBuild)

            If childsArchives IsNot Nothing Then
                Dim treeNodes As New TreeNodeCollection()
                Dim max As Integer = childsArchives.Count
                Dim node As TreeNode
                Dim childNodes As TreeNodeCollection
                Dim maxChilds As Integer

                'Recorremos los hijos
                For i As Integer = 0 To max - 1
                    'Creamos el nodo
                    node = New TreeNode(childsArchives(i).Doc_Type_Group_Name, childsArchives(i).Doc_Type_Group_ID.ToString())

                    'Buscamos si tiene hijos y los cargamos como nodos
                    childNodes = LoadChildTree(CInt(childsArchives(i).Doc_Type_Group_ID), TableToBuild)

                    If childNodes IsNot Nothing Then
                        maxChilds = childNodes.Count

                        For j As Integer = 0 To maxChilds - 1
                            node.ChildNodes.Add(childNodes(j))
                        Next
                    End If

                    'Sumamos el nodo a la lista a devolver
                    treeNodes.Add(node)
                Next

                Return treeNodes
            End If

            Return Nothing
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' En base a la tabla tipada de archivos, obtiene los hijos desde un ID de archivo.
    ''' </summary>
    ''' <param name="ParentID"></param>
    ''' <param name="tableToFind"></param>
    ''' <returns></returns>
    Private Function GetChildArchives(ParentID As Integer, ByRef tableToFind As Data_Group_Doc.Doc_Type_GroupDataTable) As Data_Group_Doc.Doc_Type_GroupDataTable
        Try
            Dim dtToReturn As New Data_Group_Doc.Doc_Type_GroupDataTable()
            Dim max As Integer = tableToFind.Count
            'Data_Group_Doc.Doc_Type_GroupRow row;

            For i As Integer = 0 To max - 1
                If tableToFind(i).Parent_Id = ParentID Then
                    dtToReturn.AddDoc_Type_GroupRow(tableToFind(i).Doc_Type_Group_ID, If((tableToFind(i).IsDoc_Type_Group_NameNull()), String.Empty, tableToFind(i).Doc_Type_Group_Name), If((tableToFind(i).IsIconNull()), -1, tableToFind(i).Icon), If((tableToFind(i).IsParent_IdNull()), -1, tableToFind(i).Parent_Id), If((tableToFind(i).IsObject_Type_IdNull()), -1, tableToFind(i).Object_Type_Id), tableToFind(i).User_Id,
                        If((tableToFind(i).IsRights_TypeNull()), -1, tableToFind(i).Rights_Type))
                End If
            Next

            Return dtToReturn
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
            Return Nothing
        End Try
    End Function

    Private Function GetLastNode(node As TreeNode) As TreeNode
        If node.ChildNodes.Count = 0 Then
            Return node
        Else
            Return GetLastNode(node.ChildNodes(0))
        End If
    End Function

    ''' <summary>
    ''' Busca recursivamente por los archivos y tipos de documento el último seleccionado.
    ''' Al encontrarlo lo selecciona y detiene la búsqueda.
    ''' </summary>
    ''' <param name="nodes">Nodos a iterar la búsqueda</param>
    ''' <param name="SectionIdtoSelect">Id del entidad a seleccionar</param>
    ''' <param name="stop">true: para detener la búsqueda</param>
    Private Sub SelectSectionNode(nodes As TreeNodeCollection, SectionIdtoSelect As String, [stop] As Boolean)
        For i As Integer = 0 To nodes.Count - 1
            If Not [stop] Then
                If [String].Compare(nodes(i).Value, SectionIdtoSelect) = 0 Then
                    nodes(i).[Select]()
                    [stop] = True
                    Exit For
                Else
                    If nodes(i).ChildNodes.Count > 0 Then
                        SelectSectionNode(nodes(i).ChildNodes, SectionIdtoSelect, [stop])
                    End If
                End If
            End If
        Next
    End Sub



    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        NewIndexSearch()
    End Sub

    Protected Sub btnClearIndexs_Click(sender As Object, e As EventArgs)
        If Session("SelectedsDocTypesIds").Count > 0 Then
            'Se crean de vuelta los atributos
            ShowIndexs(Session("SelectedsDocTypesIds"), WebModuleMode.Search)

            'Se Actualizan los valores de los controles del UpdatePanel
            UpdatePanel2.Update()

        End If
        TxtTextSearch.Text = [String].Empty

        ' Si se encuentra visible el mensaje "No se encontraron resultados"
        If NoResults.Visible Then
            ' Entonces se oculta
            NoResults.Visible = False
        End If
    End Sub



    Private Sub ShowIndexs(DocTypesIds As List(Of Int64), Mode As WebModuleMode)
        If DocTypesIds Is Nothing OrElse DocTypesIds.Count = 0 Then
            Dim ind As Index() = New Index(-1) {}
            DocTypesIndexs.Clear()
            lblErrorIndex.Text = "No hay elementos seleccionados para realizar la busqueda"
            lblErrorIndex.Visible = True
            Return
        End If

        Dim Rights As New SRights()
        Try

            Dim indexList = New List(Of IIndex)()

            'Si seleccionó algún documento
            If DocTypesIds.Count > 0 Then
                Dim indexs As IEnumerable(Of Zamba.Core.Index) = GetindexSchemaNew(DocTypesIds)

                Dim viewSpecifiedIndex As Boolean = True
                Dim docTypesIds64 = New List(Of Int64)()

                For Each id As Int32 In DocTypesIds
                    docTypesIds64.Add(id)

                    'Si se hace una busqueda combinada, si algun doctype tiene permiso para no filtrar indices
                    'Bastaria para aplicar ese permiso a todos
                    Dim permisosFiltrarIndices As Boolean = Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, id)

                    If permisosFiltrarIndices = False Then
                        viewSpecifiedIndex = False
                    End If
                Next

                If viewSpecifiedIndex Then
                    Dim user As IUser = Session("User")
                    Dim iri As Hashtable = Rights.GetIndexsRights(docTypesIds64, user.ID, True)

                    For Each currentIndex As Zamba.Core.Index In indexs
                        If DirectCast(iri(currentIndex.ID), Zamba.Core.IndexsRightsInfo).Search Then
                            indexList.Add(currentIndex)
                        End If
                    Next
                Else
                    indexList.AddRange(indexs)
                End If

                'If Not DocTypesControl.GotSelectedIndexs() Then
                '    indexList = New List(Of IIndex)()
                '    Session("SelectedsDocTypesIds") = New List(Of Int64)()
                'End If

                DocTypesIndexs.DtId = DocTypesIds(0)
            End If

            DocTypesIndexs.ShowIndexs(indexList, Mode)
            lblErrorIndex.Visible = False
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub

    Private Function GetindexSchemaNew(docTypesIds As List(Of Int64)) As IEnumerable(Of Zamba.Core.Index)

        Dim indexs As List(Of IIndex)

        'IndexsBusiness IndexBusinessObj = new IndexsBusiness(user);

        Dim ar = New ArrayList()
        ar.AddRange(docTypesIds)
        indexs = ZCore.GetInstance().FilterSearchIndex(ar)

        Dim clonedIndexs = New Zamba.Core.Index(indexs.Count - 1) {}
        Dim contador As Int32 = 0

        For Each ind As Zamba.Core.Index In indexs
            Dim newIndex = ind
            clonedIndexs(contador) = newIndex
            contador += 1
        Next

        Return clonedIndexs
    End Function

    Private Sub NewIndexSearch()
        lblMessage.Visible = False

        Dim DocType As New sDocType()
        Dim Result As New SResult()

        Try
            'Indices con todos los filtros cargados
            Dim indexs As List(Of IIndex) = DocTypesIndexs.CurrentIndexs
            If indexs.Count() = 0 OrElse Session("SelectedsDocTypesIds") Is Nothing OrElse Session("SelectedsDocTypesIds").Count = 0 Then
                txtMensajes.Value = "No hay elementos seleccionados para realizar la busqueda"
                txtMensajes.Visible = True
                Return
            End If

            'Entidades como objetos
            Dim DocTypes As List(Of IDocType) = DocType.GetDocTypes(Session("SelectedsDocTypesIds"), False)

            'Agregada la posibilidad que genere la consulta dependiendo de la forma de busqueda
            Dim consulta As String()
            Dim user As IUser = Session("User")

            'Se arma la consulta a realizar
            consulta = Result.webMakeSearch(DocTypes, indexs, user)

            'Se guarda la opción que selecciono el usuario
            Dim sUP As New SUserPreferences()
            sUP.setValue("SearchInTaks", chkSearchByTask.Checked.ToString(), Sections.Search)

            If consulta.Length > 0 Then
                Session("CurrentQryResults") = consulta
                Session("IsNewSearch") = True

                'Búsqueda de Texto
                If [String].Compare(TxtTextSearch.Text, [String].Empty) <> 0 Then
                    Session("SearchTextValue") = TxtTextSearch.Text
                End If

                MakeSearch()

                Dim script As String = "<script type=""text/javascript""> $(document).ready(function(){ViewResults();});</script>"
                ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "ViewResults", script, False)

            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub


    Protected Function IsNumeric(DATA As Object) As [Boolean]
        Try
            Dim a As Integer = Integer.Parse(DATA.ToString())
            Return True
        Catch generatedExceptionName As Exception
            Return False
        End Try
    End Function

    Private Sub GenerateResultsGrid(dt As DataTable)
        lblMsg.Visible = True
        lblMsg.Font.Size = 10
        lblMsg.Text = "Resultados encontrados: " & Convert.ToString(dt.Rows.Count)
        FormatGridview()
        generateGridColumns(dt)
        BindGrid(dt)
    End Sub

    Private Function getResults() As DataTable
        Dim textToSearch As String = Nothing
        Dim docTypesSelected As List(Of Int64) = Nothing
        Dim qrys As String() = Nothing

        Try
            'Se agrega búsqueda por todos los índices, que es prioritaria al resto.
            textToSearch = DirectCast(Session("SearchTextValue"), String)
            docTypesSelected = DirectCast(Session("SelectedsDocTypesIds"), List(Of Int64))


            If Not String.IsNullOrEmpty(textToSearch) AndAlso docTypesSelected IsNot Nothing Then
                Dim Search As New Zamba.Core.Searchs.Search()
                Search.TextSearchInAllIndexs = textToSearch
                Search.blnSearchInAllDocsType = False
                Search.CaseSensitive = False
                Search.RaiseResults = False
                Return New SResult().RunWebTextSearch(Search, docTypesSelected)
            Else
                If Session("CurrentQryResults") IsNot Nothing Then
                    qrys = DirectCast(Session("CurrentQryResults"), String())
                    Return New SResult().webRunSearch(qrys)
                Else
                    Return Nothing
                End If
            End If
        Catch ex As Exception
            ZException.Log(ex)
            Return Nothing
        Finally
            textToSearch = Nothing
            docTypesSelected = Nothing
            qrys = Nothing
        End Try
    End Function

    Private Shared Function GetPageSize() As Int16
        Return Int16.Parse(WebConfigurationManager.AppSettings("PageSize"))
    End Function

    Private Sub FormatGridview()
        Dim Result As New SResult()

        Try
            Dim pageId As Int16 = Me.ResultsPagingId
            Dim pageSize As Int16 = Me.PageSize

            grdResultados.AutoGenerateColumns = False
            grdResultados.AllowPaging = True
            grdResultados.AllowSorting = False
            grdResultados.PageSize = pageSize
            grdResultados.PageIndex = pageId
            grdResultados.ShowFooter = True
            grdResultados.Attributes.Add("style", "table-layout:fixed")

            Dim a As [String]() = {"DOC_TYPE_ID", "DOC_ID"}

            grdResultados.Columns.Clear()

            Dim colver As New HyperLinkField()

            colver.ShowHeader = True
            colver.HeaderText = "Ver"
            colver.Target = "_blank"
            colver.Text = "Ver"
            colver.DataTextFormatString = "<img src=""../Tools/icono.aspx?id={0}"" border=0/>"
            colver.DataTextField = "ICON_ID"


            colver.ItemStyle.HorizontalAlign = HorizontalAlign.Center

            colver.DataNavigateUrlFields = a

            '13/07/11: se le suma la opción de buscar por tarea.
            'Se construye la url según necesidad.
            colver.DataNavigateUrlFormatString = If((chkSearchByTask.Checked), "../WF/TaskSelector.ashx?docid={1}&doctype={0}", "../search/DocViewer.aspx?docid={1}&doctype={0}")

            grdResultados.Columns.Add(colver)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub

    Private Sub formatValues(_dt As DataTable)
        Dim [date] As DateTime

        If _dt.Rows.Count > 0 Then
            For col As Integer = 0 To grdResultados.Columns.Count - 1
                Dim colname As String = grdResultados.Columns(col).HeaderText

                If GetVisibility(colname.ToLower()) = False Then
                    grdResultados.Columns(col).Visible = False
                Else
                    If _dt.Columns.Contains(colname) Then
                        If _dt.Columns(colname).DataType = Type.[GetType]("System.DateTime") Then
                            For row As Integer = 0 To grdResultados.Rows.Count - 1
                                If String.IsNullOrEmpty(grdResultados.Rows(row).Cells(col).Text) Then
                                    Continue For
                                End If

                                Dim value As String = grdResultados.Rows(row).Cells(col).Text

                                If DateTime.TryParse(value, [date]) Then
                                    grdResultados.Rows(row).Cells(col).Text = [date].ToShortDateString()
                                End If
                            Next
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Private Function GetVisibility(columnName As String) As Boolean
        Dim aux As Long
        Dim UserId As Long = Long.Parse(Session("UserId").ToString())
        Dim rights As New SRights()

        If _hideColumns Is Nothing Then
            _hideColumns = New ArrayList()
        End If

        If _hideColumns.Count = 0 Then
            _hideColumns.Add("doc_id")
            _hideColumns.Add("doc_type_id")
            _hideColumns.Add("icon_id")
            _hideColumns.Add("rnum")

            Dim SUserPreferences As New SUserPreferences()

            If [Boolean].Parse(SUserPreferences.getValue("ShowGridColumnNombreOriginal", Zamba.Core.Sections.UserPreferences, "False")) = False Then
                _hideColumns.Add("nombre original")
            End If
        End If

        If _hideColumns.Contains(columnName.ToLower()) Then
            Return False
        End If

        If columnName.StartsWith("I") AndAlso Int64.TryParse(columnName.Remove(0, 1), aux) Then
            Return False
        End If

        Return True
    End Function

    Private Sub generateGridColumns(_dt As DataTable)
        Try
            If _dt IsNot Nothing AndAlso _dt.Columns.Count > 0 Then
                If _dt.Columns.Contains("Nombre del Documento") Then
                    _dt.Columns("Nombre del Documento").SetOrdinal(0)
                End If
                If _dt.Columns.Contains("Entidad") Then
                    _dt.Columns("Entidad").SetOrdinal(2)
                End If
                If _dt.Columns.Contains("Fecha Creacion") Then
                    _dt.Columns("Fecha Creacion").SetOrdinal(_dt.Columns.Count - 1)
                End If
                If _dt.Columns.Contains("Fecha Modificacion") Then
                    _dt.Columns("Fecha Modificacion").SetOrdinal(_dt.Columns.Count - 1)
                End If
                If _dt.Columns.Contains("Nombre Original") Then
                    _dt.Columns("Nombre Original").SetOrdinal(_dt.Columns.Count - 1)
                End If

                _dt.AcceptChanges()

                For Each c As DataColumn In _dt.Columns
                    Dim f As New BoundField()

                    f.DataField = c.Caption
                    f.ShowHeader = True
                    f.HeaderText = c.Caption
                    f.SortExpression = c.Caption + " ASC"

                    grdResultados.Columns.Add(f)
                Next
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub

    Private Sub BindGrid(dt As DataTable)
        Try
            grdResultados.DataSource = dt
            grdResultados.DataBind()
            formatValues(dt)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub

    Protected Sub grdResultados_OnPageIndexChanging(sender As Object, e As EventArgs)
        Dim pageId As Int32 = DirectCast(e, GridViewPageEventArgs).NewPageIndex
        Session("ResultsPagingId") = pageId
        grdResultados.PageIndex = pageId

        Dim dt As DataTable = getResults()
        GenerateResultsGrid(dt)
        Me.UpdGrid.Update()
    End Sub

    Private Sub MakeSearch()

        _hideColumns = New ArrayList()

        Session("PageSize") = GetPageSize()
        Dim user As IUser = Session("User")

        Dim dt As DataTable = getResults()

        'Verifica que existan resultados
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            'Si los resultados de la búsqueda son 1 y es una busqueda en tareas abrira la tarea.
            If chkSearchByTask.Checked AndAlso dt.Rows.Count = 1 Then
                Dim id As Long = Long.Parse(dt.Rows(0)("DOC_ID").ToString())
                Dim STasks As New STasks()
                Dim taskData As ZCoreView = STasks.GetTaskIdAndNameByDocId(id)

                If taskData IsNot Nothing Then
                    Session("CurrentQryResults") = Nothing
                    grdResultados.Columns.Clear()
                    grdResultados.DataSource = New DataTable()
                    grdResultados.DataBind()

                    Dim urlTask As String = ("../WF/TaskSelector.ashx?docid=" & id.ToString() & "&doctype=") + dt.Rows(0)("DOC_TYPE_ID").ToString()
                    Dim script As String = "$(document).ready(function(){{ OpenDocTask2({0},{1},{2},{3},'{4}','{5}',{6}); }});"
                    ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "OpenTaskScript", String.Format(script, taskData.ID, 0, -1, "false", taskData.Name, _
                        urlTask, user.ID), True)

                    urlTask = Nothing
                    script = Nothing
                Else
                    GenerateResultsGrid(dt)
                End If

                taskData = Nothing
                STasks = Nothing
            Else
                GenerateResultsGrid(dt)
            End If

            lblMsg.Visible = False
            lblMsg.Text = String.Empty
        Else
            lblMsg.Visible = True
            lblMsg.ForeColor = System.Drawing.Color.Red
            lblMsg.Text = "No se encontraron resultados"
        End If

        Me.UpdGrid.Update()

        If Session("User") IsNot Nothing Then
            Dim rights As New SRights()
            Dim type As Int32 = 0
            If user.WFLic Then
                type = 1
            End If
            If user.ConnectionId > 0 Then
                Dim sUserPreferences As New Zamba.Services.SUserPreferences()
                rights.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(sUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type)
            Else
                Response.Redirect("~/Views/Security/LogIn.aspx")
            End If
            rights = Nothing
        End If
    End Sub
End Class
