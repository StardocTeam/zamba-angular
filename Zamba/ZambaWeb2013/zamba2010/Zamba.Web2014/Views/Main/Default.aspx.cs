using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Zamba.Core;
using Zamba.Services;
using System.Web.Configuration;
using Zamba.AppBlock;
using Zamba.Membership;
using Zamba;
using System.Web.UI;
using ExtExtenders;
using System.Web.UI.WebControls;
using System.Web.Security;
using Controls.Indexs;

partial class _Default : System.Web.UI.Page
{

    private ArrayList _hideColumns;
    private int _timeOut;
    private Int16 m_resultsPagingId;
    private Int16 m_pageSize;
    private ExtExtenders.TreeNode Root;

    private SUserPreferences uConfig = new SUserPreferences();

    public Int16 PageSize
    {
        get
        {
            if (Session["PageSize"] == null)
            {
                m_pageSize = 1;
            }
            else {
                m_pageSize = Int16.Parse(Session["PageSize"].ToString());
            }
            return m_pageSize;
        }
        set { m_pageSize = value; }
    }
    public Int16 ResultsPagingId
    {
        get
        {
            if (Session["ResultsPagingId"] == null)
            {
                m_resultsPagingId = 0;
            }
            else {
                m_resultsPagingId = Int16.Parse(Session["ResultsPagingId"].ToString());
            }
            return m_resultsPagingId;
        }
        set { m_resultsPagingId = value; }
    }

    public object Information { get; private set; }
    public object UpdatePanel2 { get; private set; }

    protected void Page_Init(object sender, EventArgs e)
    {
        IUser user = (IUser)Session["User"];
        if (Session["CurrentTheme"] == null || user == null) { 
            FormsAuthentication.RedirectToLoginPage();
            return;
        }
        Page.Theme = Session["CurrentTheme"].ToString();

        _timeOut = Server.ScriptTimeout;
        Server.ScriptTimeout = 3600;
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        //En base a userconfig que por defecto estará en false se mostrará la pestaña de Novedades
        Zamba.Services.SUserPreferences sUserPreferences = new Zamba.Services.SUserPreferences();
        Zamba.Core.UserBusiness UserBusiness = new Zamba.Core.UserBusiness();
        SZOptBusiness ZOptBusines = new SZOptBusiness();

        //if ((Request.QueryString["mode"] != null) && Request.QueryString["mode"] == "ajax")
        //{
        //    //Saving the variables in session. Variables are posted by ajax.

        //    if ((Request.Params["SelectedsDocTypesIds"] != null))
        //    {
        //        List<Int64> SelectedsDocTypesIds = new List<Int64>();
        //        foreach (string Id in Request.Params["SelectedsDocTypesIds"].Split(char.Parse(",")))
        //        {
        //            int temp = 0;
        //            if (Int32.TryParse(Id, out temp))
        //                SelectedsDocTypesIds.Add(Int64.Parse(Id));
        //        }
        //        Session["SelectedsDocTypesIds"] = SelectedsDocTypesIds;
        //        ShowIndexs(SelectedsDocTypesIds, WebModuleMode.Search);
        //        //this.UpdatePanel2.Update();
        //    }
        //}


        try
        {
            bool sFeedView = bool.Parse(sUserPreferences.getValue("ViewNewsTabs", Zamba.Core.Sections.UserPreferences, "False"));
            viewsNews.Value = sFeedView.ToString();
            viewInsert.Value = Convert.ToString(UserBusiness.Rights.GetUserRights(ObjectTypes.InsertWeb, RightsType.View));

            string PageTitle = ZOptBusines.GetValue("WebViewTitle");
            if (string.IsNullOrEmpty(PageTitle))
            {
                this.Title = "Zamba";
            }
            else {
                this.Title = PageTitle + " - Zamba";
            }



            if (MembershipHelper.CurrentUser == null == false)
            {
                //Actualiza el timemout
                SRights rights = new SRights();
                Int32 type = 0;
                if (MembershipHelper.CurrentUser.WFLic)
                {
                    type = 1;
                }
                if (MembershipHelper.CurrentUser.ConnectionId > 0)
                {
                    rights.UpdateOrInsertActionTime(MembershipHelper.CurrentUser.ID, MembershipHelper.CurrentUser.Name, MembershipHelper.CurrentUser.puesto, MembershipHelper.CurrentUser.ConnectionId, Int32.Parse(sUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type);
                }
                else {
                    Response.Redirect("~/Views/Security/LogIn.aspx");
                }

                Arbol.SelectedNodeChanged += SelectedNodeChanged;
                Arbol.WFTreeRefreshed += RefreshTaskGrid;
                Arbol.WFTreeIsEmpty -= WfTreeIsEmpty;
                Arbol.WFTreeIsEmpty += WfTreeIsEmpty;
                Arbol.FillWF();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "TaskCount", "$(document).ready(function () { LoadStepsCounts(); });", true);

                //search
                //docTypesIndexs.ShowHideIndexs.Visible = false;
                string searchInTasks = ZOptBusines.GetValue("TaskSearch");
                if (!String.IsNullOrEmpty(searchInTasks))
                {
                    chkSearchByTask.Visible = Boolean.Parse(searchInTasks);
                }
                chkSearchByTask.Checked = Boolean.Parse(UserPreferences.getValue("SearchInTaks", Sections.Search, true));


                //Solapa Novedades
                if (sFeedView)
                {
                    //Obtiene permiso para ver los feeds
                    sFeedView = new SRights().GetUserRights(ObjectTypes.Feeds, RightsType.View, -1);
                    hdnFView.Value = sFeedView.ToString();

                    if (sFeedView)
                    {
                        //Obtiene variables de configuracion
                        string sFeedRefreshInterval = ZOptBusines.GetValue("FeedRefreshInterval");

                        if (string.IsNullOrEmpty(sFeedRefreshInterval))
                        {
                            hdnFRefresh.Value = "5000";
                        }
                        else {
                            hdnFRefresh.Value = sFeedRefreshInterval;
                        }

                        string sFeedLinesCount = ZOptBusines.GetValue("FeedLinesCount");

                        if (string.IsNullOrEmpty(sFeedLinesCount))
                        {
                            hdnFLinesCount.Value = "6";
                        }
                        else {
                            hdnFLinesCount.Value = sFeedLinesCount;
                        }
                    }
                }
            }

            if (Convert.ToBoolean(UserPreferences.getValue("OpenRecentTask", Sections.WorkFlow, false)))
            {
                ZOptBusiness zopt = new ZOptBusiness();
                string weblink = zopt.GetValue("WebViewPath");
                zopt = null;

                if (!string.IsNullOrEmpty(weblink))
                {
                    DataTable lTasks = Zamba.Core.WF.WF.WFTaskBusiness.GetUserOpenedTasks(MembershipHelper.CurrentUser.ID);

                    if (lTasks.Rows.Count > 0)
                    {
                        string Script = "$(document).ready(function(){";
                        foreach (DataRow task in lTasks.Rows)
                        {
                            string url = weblink + "/views/WF/TaskSelector.ashx?taskid=" + task["Task_ID"] + "&docid=" + task["Doc_ID"] + "&DocTypeId=" + task["doc_type_id"].ToString();
                            Script += "AddDocTaskToOpenList(" + task["Task_ID"] + ", " + task["Doc_ID"] + ", " + task["doc_type_id"].ToString() + ", false, '" + task["Name"] + "', '" + url + "', " + MembershipHelper.CurrentUser.ID + ");";
                        }
                        Script += "OpenPendingTabs();});";
                        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "OpenTask", Script, true);
                    }

                    lTasks.Dispose();
                    lTasks = null;
                }
            }

            //Instancio un controller 
            DynamicButtonController dynamicBtnController = new DynamicButtonController();
            //Pido la vista
            //   DynamicButtonPartialViewBase dynBtnView = dynamicBtnController.GetViewHeaderButtons(MembershipHelper.CurrentUser);
            DynamicButtonPartialViewBase dynBtnView = dynamicBtnController.GetViewHomeButtons(MembershipHelper.CurrentUser);
            
            //La agrego
            pnlHomeButtons.Controls.Add(dynBtnView);

        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        finally
        {
            sUserPreferences = null;
            ZOptBusines = null;
            UserBusiness = null;
            string Script = "$(document).ready(function(){ hideLoading();});";
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "CloseLoadingDialog", Script, true);
        }
    }

    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        ZOptBusiness zopt = new ZOptBusiness();
        string weblink = zopt.GetValue("WebViewPath");
        zopt = null;


        if (!string.IsNullOrEmpty(weblink))
        {

            if (Page.Request.QueryString.Count > 0 && !string.IsNullOrEmpty(Page.Request.QueryString["docid"]))
            {
                string docid = Page.Request.QueryString["docid"];
                STasks _STask = new STasks();
                ITaskResult _task = _STask.GetTaskByDocId(Int64.Parse(docid));
                _STask = null;


                if (_task != null)
                {
                    string script = "parent.CreateTaskIframe('" + weblink + "/views/WF/TaskSelector.ashx?doctype=" + _task.DocTypeId + "&docid=" + _task.ID + "&taskid=" + _task.TaskId + "&wfstepid=" + _task.StepId + "'," + _task.TaskId + ",'" + _task.Name + "');";
                    Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "OpenTaskLink", script, true);
                    _task = null;


                }
                else {
                    string script = "$(document).ready(function(){toastr.error('El documento es inexistente o no tiene permiso para acceder al mismo');});";
                    Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "ErrorMessage", script, true);

                }

            }

        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        if (_timeOut > 0)
        {
            Server.ScriptTimeout = _timeOut;
        }
    }

    private void SelectedNodeChanged(Int32 WFId, Int32 StepId, Int32 DocTypeId)
    {
        if (Page.IsPostBack)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "TaskCount", "$(document).ready(function () { LoadStepsCounts(); });", true);
        }

        ZOptBusiness zoptb = new ZOptBusiness();
        string CurrentTheme = zoptb.GetValue("CurrentTheme");
        zoptb = null;

        if (CurrentTheme == "AysaDiseno")
        {
            TaskGrid.ClearCurrentFilters(StepId);
            TaskGrid.SetFilters(StepId);
        }

        TaskGrid.LoadTasks(WFId, StepId, DocTypeId, Arbol.WFTreeView.SelectedNode);
    }

    /// <summary>
    /// Handler para una vez finalizado el refresco de wf, refrescar la grilla
    /// </summary>
    /// <param name="StepId"></param>
    /// <remarks></remarks>
    private void RefreshTaskGrid(Int32 StepId)
    {
        ZOptBusiness zoptb = new ZOptBusiness();
        string CurrentTheme = zoptb.GetValue("CurrentTheme");
        zoptb = null;

        if (CurrentTheme == "AysaDiseno")
        {
            TaskGrid.ClearCurrentFilters(StepId);
            TaskGrid.SetFilters(StepId);
        }

        TaskGrid.RebindGrid();
    }



    /// <summary>
    /// Metodo que se utiliza para atrapar el evento que el arbol esta vacio
    /// </summary>
    /// <remarks></remarks>
    private void WfTreeIsEmpty()
    {
        UpdTaskGrid.Visible = false;
        lblNoWFVisible.Visible = true;
    }



    /// <summary>
    /// Genera los nodos hijos de un id dado, en base a la tabla de secciones.
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="TableToBuild"></param>
    /// <returns></returns>
    private TreeNodeCollection LoadChildTree(int ParentID, Data_Group_Doc.Doc_Type_GroupDataTable TableToBuild)
    {
        try
        {
            //Obtenemos la tabla con los archivos hijos.
            Data_Group_Doc.Doc_Type_GroupDataTable childsArchives = GetChildArchives(ParentID, ref TableToBuild);

            if (childsArchives != null)
            {
                TreeNodeCollection treeNodes = new TreeNodeCollection();
                int max = childsArchives.Count;
                System.Web.UI.WebControls.TreeNode node = default(System.Web.UI.WebControls.TreeNode);
                TreeNodeCollection childNodes = default(TreeNodeCollection);
                int maxChilds = 0;

                //Recorremos los hijos
                for (int i = 0; i <= max - 1; i++)
                {
                    //Creamos el nodo
                    node = new System.Web.UI.WebControls.TreeNode(childsArchives[i].Doc_Type_Group_Name, childsArchives[i].Doc_Type_Group_ID.ToString());

                    //Buscamos si tiene hijos y los cargamos como nodos
                    childNodes = LoadChildTree(Convert.ToInt32(childsArchives[i].Doc_Type_Group_ID), TableToBuild);

                    if (childNodes != null)
                    {
                        maxChilds = childNodes.Count;

                        for (int j = 0; j <= maxChilds - 1; j++)
                        {
                            node.ChildNodes.Add(childNodes[j]);
                        }
                    }

                    //Sumamos el nodo a la lista a devolver
                    treeNodes.Add(node);
                }

                return treeNodes;
            }

            return null;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return null;
        }
    }

    /// <summary>
    /// En base a la tabla tipada de archivos, obtiene los hijos desde un ID de archivo.
    /// </summary>
    /// <param name="ParentID"></param>
    /// <param name="tableToFind"></param>
    /// <returns></returns>
    private Data_Group_Doc.Doc_Type_GroupDataTable GetChildArchives(int ParentID, ref Data_Group_Doc.Doc_Type_GroupDataTable tableToFind)
    {
        try
        {
            Data_Group_Doc.Doc_Type_GroupDataTable dtToReturn = new Data_Group_Doc.Doc_Type_GroupDataTable();
            int max = tableToFind.Count;
            //Data_Group_Doc.Doc_Type_GroupRow row;

            for (int i = 0; i <= max - 1; i++)
            {
                if (tableToFind[i].Parent_Id == ParentID)
                {
                    dtToReturn.AddDoc_Type_GroupRow(tableToFind[i].Doc_Type_Group_ID, (tableToFind[i].IsDoc_Type_Group_NameNull()) ? string.Empty : tableToFind[i].Doc_Type_Group_Name, (tableToFind[i].IsIconNull()) ? -1 : tableToFind[i].Icon, (tableToFind[i].IsParent_IdNull()) ? -1 : tableToFind[i].Parent_Id, (tableToFind[i].IsObject_Type_IdNull()) ? -1 : tableToFind[i].Object_Type_Id, tableToFind[i].User_Id, (tableToFind[i].IsRights_TypeNull()) ? -1 : tableToFind[i].Rights_Type);
                }
            }

            return dtToReturn;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return null;
        }
    }

    private System.Web.UI.WebControls.TreeNode GetLastNode(System.Web.UI.WebControls.TreeNode node)
    {
        if (node.ChildNodes.Count == 0)
        {
            return node;
        }
        else {
            return GetLastNode(node.ChildNodes[0]);
        }
    }

    /// <summary>
    /// Busca recursivamente por los archivos y tipos de documento el último seleccionado.
    /// Al encontrarlo lo selecciona y detiene la búsqueda.
    /// </summary>
    /// <param name="nodes">Nodos a iterar la búsqueda</param>
    /// <param name="SectionIdtoSelect">Id del entidad a seleccionar</param>
    /// <param name="stop">true: para detener la búsqueda</param>
    private void SelectSectionNode(TreeNodeCollection nodes, string SectionIdtoSelect, bool stop)
    {
        for (int i = 0; i <= nodes.Count - 1; i++)
        {
            if (!stop)
            {
                if (String.Compare(nodes[i].Value, SectionIdtoSelect) == 0)
                {
                    nodes[i].Select();
                    stop = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
                else {
                    if (nodes[i].ChildNodes.Count > 0)
                    {
                        SelectSectionNode(nodes[i].ChildNodes, SectionIdtoSelect, stop);
                    }
                }
            }
        }
    }
        
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        NewIndexSearch();
    }

    //protected void btnClearIndexs_Click(object sender, EventArgs e)
    //{
    //    //Se crean de vuelta los atributos
    //    if ((Session != null) && (Session["SelectedsDocTypesIds"] != null) && Session["SelectedsDocTypesIds"].Count > 0)
    //    {
    //        ShowIndexs(Session["SelectedsDocTypesIds"], WebModuleMode.Search);
    //        //Se Actualizan los valores de los controles del UpdatePanel
    //        UpdatePanel2.Update();
    //    }
    //    TxtTextSearch.Text = String.Empty;

    //    // Si se encuentra visible el mensaje "No se encontraron resultados"
    //    if (NoResults.Visible)
    //    {
    //        // Entonces se oculta
    //        NoResults.Visible = false;
    //    }
    //}

    //private void ShowIndexs(List<Int64> DocTypesIds, WebModuleMode Mode)
    //{
    //    if (DocTypesIds == null || DocTypesIds.Count == 0)
    //    {
    //        Index[] ind = new Index[-1 + 1];
    //        docTypesIndexs.Clear();
    //        this.lblErrorIndex.Text = "No hay elementos seleccionados para realizar la busqueda";
    //        lblErrorIndex.Visible = true;
    //        return;
    //    }





    //    SRights Rights = new SRights();

    //    try
    //    {
    //        dynamic indexList = new List<IIndex>();

    //        //Si seleccionó algún documento
    //        if (DocTypesIds.Count > 0)
    //        {
    //            IEnumerable<Zamba.Core.Index> indexs = GetindexSchemaNew(DocTypesIds);

    //            bool viewSpecifiedIndex = true;
    //            dynamic docTypesIds64 = new List<Int64>();

    //            foreach (Int32 id in DocTypesIds)
    //            {
    //                docTypesIds64.Add(id);

    //                //Si se hace una busqueda combinada, si algun doctype tiene permiso para no filtrar indices
    //                //Bastaria para aplicar ese permiso a todos
    //                bool permisosFiltrarIndices = Rights.GetUserRights(Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, id);

    //                if (permisosFiltrarIndices == false)
    //                {
    //                    viewSpecifiedIndex = false;
    //                }
    //            }

    //            if (viewSpecifiedIndex)
    //            {
    //                IUser user = (IUser)Session["User"];
    //                Hashtable iri = Rights.GetIndexsRights(docTypesIds64, user.ID, true);

    //                foreach (Index currentIndex in indexs)
    //                {
    //                    if (((IndexsRightsInfo)iri[currentIndex.ID]).Search)
    //                    {
    //                        indexList.Add(currentIndex);
    //                    }
    //                }
    //            }
    //            else {
    //                indexList.AddRange(indexs);
    //            }

    //            //If Not DocTypesControl.GotSelectedIndexs() Then
    //            //    indexList = New List(Of IIndex)()
    //            //    Session["SelectedsDocTypesIds"] = New List(Of Int64)()
    //            //End If

    //            docTypesIndexs.DtId = DocTypesIds[0];
    //        }

    //        docTypesIndexs.ShowIndexs(indexList, Mode);
    //        lblErrorIndex.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        Zamba.AppBlock.ZException.Log(ex);
    //    }

    //}

    private IEnumerable<Zamba.Core.Index> GetindexSchemaNew(List<Int64> docTypesIds)
    {

        List<IIndex> indexs = default(List<IIndex>);

        //IndexsBusiness IndexBusinessObj = new IndexsBusiness(user);

        dynamic ar = new ArrayList();
        ar.AddRange(docTypesIds);
        indexs = ZCore.GetInstance().FilterSearchIndex(ar);

        dynamic clonedIndexs = new Zamba.Core.Index[indexs.Count];
        Int32 contador = 0;

        foreach (Zamba.Core.Index ind in indexs)
        {
            dynamic newIndex = ind;
            clonedIndexs[contador] = newIndex;
            contador += 1;
        }

        return clonedIndexs;
    }

    private void NewIndexSearch()
    {
        lblMessage.Visible = false;

        sDocType DocType = new sDocType();
        SResult Result = new SResult();

        try
        {
            //Indices con todos los filtros cargados
            List<IIndex> indexs = new List<IIndex>();
            //List<IIndex> indexs = docTypesIndexs.CurrentIndexs;
            if (indexs.Count == 0 || Session["SelectedsDocTypesIds"] == null /*|| Session["SelectedsDocTypesIds"].Count == 0*/)
            {
                txtMensajes.Value = "No hay elementos seleccionados para realizar la busqueda";
                txtMensajes.Visible = true;
                return;
            }

            //Entidades como objetos
            List<IDocType> DocTypes = DocType.GetDocTypes((List < Int64 > )Session["SelectedsDocTypesIds"], false);

            //Agregada la posibilidad que genere la consulta dependiendo de la forma de busqueda
            string[] consulta = null;
            IUser user = (IUser)Session["User"];

            //Se arma la consulta a realizar
            consulta = Result.webMakeSearch(DocTypes, indexs, user);

            //Se guarda la opción que selecciono el usuario
            SUserPreferences sUP = new SUserPreferences();
            sUP.setValue("SearchInTaks", chkSearchByTask.Checked.ToString(), Sections.Search);

            if (consulta.Length > 0)
            {
                Session["CurrentQryResults"] = consulta;
                Session["IsNewSearch"] = true;

                //Búsqueda de Texto
                if (String.Compare(TxtTextSearch.Text, String.Empty) != 0)
                {
                    Session["SearchTextValue"] = TxtTextSearch.Text;
                }

                MakeSearch();

                string script = "<script type=\"text/javascript\"> $(document).ready(function(){ViewResults();});</script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewResults", script, false);

            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }


    protected Boolean IsNumeric(object DATA)
    {
        try
        {
            int a = int.Parse(DATA.ToString());
            return true;
        }
        catch (Exception generatedExceptionName)
        {
            return false;
        }
    }

    private void GenerateResultsGrid(DataTable dt)
    {
        lblMsg.Visible = true;
        lblMsg.Font.Size = 10;
        lblMsg.Text = "Resultados encontrados: " + Convert.ToString(dt.Rows.Count);
        FormatGridview();
        generateGridColumns(dt);
        BindGrid(dt);
    }

    private DataTable getResults()
    {
        string textToSearch = null;
        List<Int64> docTypesSelected = null;
        string[] qrys = null;

        try
        {
            //Se agrega búsqueda por todos los índices, que es prioritaria al resto.
            textToSearch = (string)Session["SearchTextValue"];
            docTypesSelected = (List<Int64>)Session["SelectedsDocTypesIds"];


            if (!string.IsNullOrEmpty(textToSearch) && docTypesSelected != null)
            {
                Zamba.Core.Searchs.Search Search = new Zamba.Core.Searchs.Search();
                Search.TextSearchInAllIndexs = textToSearch;
                Search.blnSearchInAllDocsType = false;
                Search.CaseSensitive = false;
                Search.RaiseResults = false;
                return new SResult().RunWebTextSearch(Search, docTypesSelected);
            }
            else {
                if (Session["CurrentQryResults"] != null)
                {
                    qrys = (string[])Session["CurrentQryResults"];
                    return new SResult().webRunSearch(qrys);
                }
                else {
                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            ZException.Log(ex);
            return null;
        }
        finally
        {
            textToSearch = null;
            docTypesSelected = null;
            qrys = null;
        }
    }

    private static Int16 GetPageSize()
    {
        return short.Parse(WebConfigurationManager.AppSettings["PageSize"]);
    }

    private void FormatGridview()
    {
        SResult Result = new SResult();

        try
        {
            Int16 pageId = this.ResultsPagingId;
            Int16 pageSize = this.PageSize;

            grdResultados.AutoGenerateColumns = false;
            grdResultados.AllowPaging = true;
            grdResultados.AllowSorting = false;
            grdResultados.PageSize = pageSize;
            grdResultados.PageIndex = pageId;
            grdResultados.ShowFooter = true;
            grdResultados.Attributes.Add("style", "table-layout:fixed");

            String[] a = {
                "DOC_TYPE_ID",
                "DOC_ID"
            };

            grdResultados.Columns.Clear();

            HyperLinkField colver = new HyperLinkField();

            colver.ShowHeader = true;
            colver.HeaderText = "Ver";
            colver.Target = "_blank";
            colver.Text = "Ver";
            colver.DataTextFormatString = "<img src=\"../Tools/icono.aspx?id={0}\" border=0/>";
            colver.DataTextField = "ICON_ID";


            colver.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

            colver.DataNavigateUrlFields = a;

            //13/07/11: se le suma la opción de buscar por tarea.
            //Se construye la url según necesidad.
            colver.DataNavigateUrlFormatString = (chkSearchByTask.Checked) ? "../WF/TaskSelector.ashx?docid={1}&doctype={0}" : "../search/DocViewer.aspx?docid={1}&doctype={0}";

            grdResultados.Columns.Add(colver);
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    private void formatValues(DataTable _dt)
    {
        DateTime date = default(DateTime);

        if (_dt.Rows.Count > 0)
        {
            for (int col = 0; col <= grdResultados.Columns.Count - 1; col++)
            {
                string colname = grdResultados.Columns[col].HeaderText;

                if (GetVisibility(colname.ToLower()) == false)
                {
                    grdResultados.Columns[col].Visible = false;
                }
                else {
                    if (_dt.Columns.Contains(colname))
                    {
                        if (_dt.Columns[colname].DataType == Type.GetType("System.DateTime"))
                        {
                            for (int row = 0; row <= grdResultados.Rows.Count - 1; row++)
                            {
                                if (string.IsNullOrEmpty(grdResultados.Rows[row].Cells[col].Text))
                                {
                                    continue;
                                }

                                string value = grdResultados.Rows[row].Cells[col].Text;

                                if (DateTime.TryParse(value, out date))
                                {
                                    grdResultados.Rows[row].Cells[col].Text = date.ToShortDateString();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private bool GetVisibility(string columnName)
    {
        long aux = 0;
        long UserId = long.Parse(Session["UserId"].ToString());
        SRights rights = new SRights();

        if (_hideColumns == null)
        {
            _hideColumns = new ArrayList();
        }

        if (_hideColumns.Count == 0)
        {
            _hideColumns.Add("doc_id");
            _hideColumns.Add("doc_type_id");
            _hideColumns.Add("icon_id");
            _hideColumns.Add("rnum");

            SUserPreferences SUserPreferences = new SUserPreferences();

            if (Boolean.Parse(SUserPreferences.getValue("ShowGridColumnNombreOriginal", Zamba.Core.Sections.UserPreferences, "False")) == false)
            {
                _hideColumns.Add("nombre original");
            }
        }

        if (_hideColumns.Contains(columnName.ToLower()))
        {
            return false;
        }

        if (columnName.StartsWith("I") && Int64.TryParse(columnName.Remove(0, 1), out aux))
        {
            return false;
        }

        return true;
    }

    private void generateGridColumns(DataTable _dt)
    {
        try
        {
            if (_dt != null && _dt.Columns.Count > 0)
            {
                if (_dt.Columns.Contains("Nombre del Documento"))
                {
                    _dt.Columns["Nombre del Documento"].SetOrdinal(0);
                }
                if (_dt.Columns.Contains("Entidad"))
                {
                    _dt.Columns["Entidad"].SetOrdinal(2);
                }
                if (_dt.Columns.Contains("Fecha Creacion"))
                {
                    _dt.Columns["Fecha Creacion"].SetOrdinal(_dt.Columns.Count - 1);
                }
                if (_dt.Columns.Contains("Fecha Modificacion"))
                {
                    _dt.Columns["Fecha Modificacion"].SetOrdinal(_dt.Columns.Count - 1);
                }
                if (_dt.Columns.Contains("Nombre Original"))
                {
                    _dt.Columns["Nombre Original"].SetOrdinal(_dt.Columns.Count - 1);
                }

                _dt.AcceptChanges();

                foreach (DataColumn c in _dt.Columns)
                {
                    BoundField f = new BoundField();

                    f.DataField = c.Caption;
                    f.ShowHeader = true;
                    f.HeaderText = c.Caption;
                    f.SortExpression = c.Caption + " ASC";

                    grdResultados.Columns.Add(f);
                }
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    private void BindGrid(DataTable dt)
    {
        try
        {
            grdResultados.DataSource = dt;
            grdResultados.DataBind();
            formatValues(dt);
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    protected void grdResultados_OnPageIndexChanging(object sender, EventArgs e)
    {
        Int32 pageId = ((GridViewPageEventArgs)e).NewPageIndex;
        Session["ResultsPagingId"] = pageId;
        grdResultados.PageIndex = pageId;

        DataTable dt = getResults();
        GenerateResultsGrid(dt);
        this.UpdGrid.Update();
    }


    private void MakeSearch()
    {
        _hideColumns = new ArrayList();

        Session["PageSize"] = GetPageSize();
        IUser user = (IUser)Session["User"];

        DataTable dt = getResults();

        //Verifica que existan resultados
        if (dt != null && dt.Rows.Count > 0)
        {
            //Si los resultados de la búsqueda son 1 y es una busqueda en tareas abrira la tarea.
            if (chkSearchByTask.Checked && dt.Rows.Count == 1)
            {
                long id = long.Parse(dt.Rows[0]["DOC_ID"].ToString());
                STasks STasks = new STasks();
                ZCoreView taskData = STasks.GetTaskIdAndNameByDocId(id);

                if (taskData != null)
                {
                    Session["CurrentQryResults"] = null;
                    grdResultados.Columns.Clear();
                    grdResultados.DataSource = new DataTable();
                    grdResultados.DataBind();

                    string urlTask = ("../WF/TaskSelector.ashx?docid=" + id.ToString() + "&doctype=") + dt.Rows[0]["DOC_TYPE_ID"].ToString();
                    string script = "$(document).ready(function(){{ OpenDocTask2({0},{1},{2},{3},'{4}','{5}',{6}); }});";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenTaskScript", string.Format(script, taskData.ID, 0, -1, "false", taskData.Name, urlTask, user.ID), true);

                    urlTask = null;
                    script = null;
                }
                else {
                    GenerateResultsGrid(dt);
                }

                taskData = null;
                STasks = null;
            }
            else {
                GenerateResultsGrid(dt);
            }

            lblMsg.Visible = false;
            lblMsg.Text = string.Empty;
        }
        else {
            lblMsg.Visible = true;
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "No se encontraron resultados";
        }

        this.UpdGrid.Update();

        if (Session["User"] != null)
        {
            SRights rights = new SRights();
            Int32 type = 0;
            if (user.WFLic)
            {
                type = 1;
            }
            if (user.ConnectionId > 0)
            {
                Zamba.Services.SUserPreferences sUserPreferences = new Zamba.Services.SUserPreferences();
                rights.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(sUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type);
            }
            else {
                Response.Redirect("~/Views/Security/LogIn.aspx");
            }
            rights = null;
        }
    }
    public _Default()
    {
        PreRender += Page_PreRender;
        Load += Page_Load;
        PreInit += Page_Init;
    }
}
