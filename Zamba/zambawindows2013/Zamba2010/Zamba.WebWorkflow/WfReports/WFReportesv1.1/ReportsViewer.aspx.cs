using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.ReportBuilder.Business;
using System.Drawing;
using System.Collections;
[assembly: System.Security.AllowPartiallyTrustedCallers]
public partial class ReportsViewer 
    : Page
{
    private const string ZOPT_PREFIX = "ReportViewer_";

    private List<String> _checkedNodes = new List<string>();

    #region Eventos

    /// <summary>
    /// Evento que se ejecuta al iniciar la página Web y de esta forma armar los controles dinámicos otra vez
    /// </summary>
    /// <history>
    ///     [Gaston]  24/11/2008  Created
    ///     [Gaston]  27/11/2008  Modified    Se agrego un try-catch
    /// </history>
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //

        try
        {
            InitializeComponent();
            base.OnInit(e);

            ArrayList dynamicReportsId = (ArrayList)Session["dynamicReportsId"];
            ArrayList dynamicReportsName = (ArrayList)Session["dynamicReportsName"];
            ArrayList valuesAndConditions = (ArrayList)Session["valuesAndConditions"];

            if ((dynamicReportsId != null) & (valuesAndConditions != null))
            {
                if (dynamicReportsId.Count == valuesAndConditions.Count)
                {
                    for (int i = 0; i < dynamicReportsId.Count; i++)
                    {
                        bool blnBan = false;

                        foreach (TableRow tr in tblUcs.Rows)
                        {
                            // Si la fila contiene una celda entonces se agrega otra celda
                            if (tr.Cells.Count == 1)
                            {
                                createDynamicReport(Int32.Parse(dynamicReportsId[i].ToString()), dynamicReportsName[i].ToString(), (Hashtable)valuesAndConditions[i], tr);
                                blnBan = true;
                            }
                        }

                        // Si blnBan es igual a true se necesita crear una nueva fila y agregar una celda
                        if (blnBan == false)
                        {
                            TableRow tr = new TableRow();
                            createDynamicReport(Int32.Parse(dynamicReportsId[i].ToString()), dynamicReportsName[i].ToString(), (Hashtable)valuesAndConditions[i], tr);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
    }
    
    protected void Page_Init(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Evento que se ejecuta cuando se carga la página Web
    /// </summary>
    /// <history>
    ///     [Gaston]  27/11/2008  Modified    Se agrego un try-catch
    /// </history>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (null != Session["UserId"])
            {
                if (!Page.IsPostBack)
                {
                    TreeNode B1 = new TreeNode("Seleccionar Todos", "SeleccionarTodos");
                    B1.ShowCheckBox = false;
                    TreeNode C1 = new TreeNode("Balances", "Balances");
                    C1.ShowCheckBox = false;
                    TreeNode D1 = new TreeNode("Distribución de Tareas", "UCTaskBalances");
                    TreeNode D2 = new TreeNode("Tiempos Promedio por Tarea", "UCAverageTimeInSteps");
                    C1.ChildNodes.Add(D1);
                    C1.ChildNodes.Add(D2);
                    B1.ChildNodes.Add(C1);

                    TreeNode C2 = new TreeNode("Vencimiento", "Vencimiento");
                    C2.ShowCheckBox = false;
                    TreeNode D3 = new TreeNode("Vencimiento de Tareas", "UCTaskToExpire");
                    C2.ChildNodes.Add(D3);
                    B1.ChildNodes.Add(C2);

                    TreeNode C3 = new TreeNode("Cantidad", "Cantidad");
                    C3.ShowCheckBox = false;
                    TreeNode D4 = new TreeNode("Asignación de Tareas", "UCAsignedTasksCount");
                    C3.ChildNodes.Add(D4);
                    B1.ChildNodes.Add(C3);

                    DataSet DsQueries = null;
                    if (null != Session["UserId"])
                        DsQueries = ReportBuilderComponent.GetQueryIdsAndNames(Int64.Parse(Session["UserId"].ToString()));
                    else
                        DsQueries = ReportBuilderComponent.GetAllQueryIdsAndNames();

                    TreeNode QueryNode = new TreeNode("Consultas", "Consultas");

                    if (null != DsQueries && DsQueries.Tables.Count > 0)
                    {
                        foreach (DataRow Dr in DsQueries.Tables[0].Rows)
                            QueryNode.ChildNodes.Add(new TreeNode(Dr[1].ToString(), Dr[0].ToString()));
                    }
                    else
                        //Se agrego al nodo hijo el texto en el caso que no haya consultas.
                        QueryNode.ChildNodes.Add(new TreeNode("No hay ocnsultas para mostrar","No hay ocnsultas para mostrar"));

                    B1.ChildNodes.Add(QueryNode);
                    TreeView1.Nodes.Add(B1);

                    ucTaskCount.Visible = false;
                    ucAverageTime.Visible = false;
                    ucTaskBalance.Visible = false;
                    ucTaskExpire.Visible = false;

                    LoadUserState();
                    SaveUserState();
                    LoadGenericReports();
                }
            }
            else
                FormsAuthentication.RedirectToLoginPage();

   

        }
        catch (Exception ex)

        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    /// <summary>
    /// Evento que se ejecuta cuando se presiona sobre el botón "Aplicar selección"
    /// </summary>
    /// <history>
    ///     [Gaston]  25/11/2008  Modified
    /// </history>
    protected void btnImplementSelection_Click(object sender, EventArgs e)
    {
        cleanSessionsViewStateAndOldDynamicReports();
        ShowReports();
    }

    /// <summary>
    /// Evento que se ejecuta al presionar el botón "Aceptar" del PopUp
    /// </summary>
    /// <history>
    ///     [Gaston]  21/11/2008  Created
    ///     [Gaston]  25/11/2008  Modified
    ///     [Gaston]  27/11/2008  Modified    Se agrego un try-catch y se mejoro el código al crear los reportes dinámicos
    /// </history>
    protected void btnAccept_click(object sender, EventArgs e)
    {
        ArrayList indexsIds = (ArrayList)ViewState["indexsId"];
        Hashtable hs = new Hashtable();
        int counter = 0;
        
        try
        {
            foreach (object I in this.ListaIndices.Controls)
            {
                // Se usa posición 5, ya que en esa posición se encuentra la caja de texto
                string value = ((TextBox)((DataListItem)I).Controls[5]).Text;
                hs.Add(indexsIds[counter], value);
                counter = counter + 1;
            }

           TableRow tr = new TableRow();

           if (tblUcs.Rows.Count > 2)
           {
                if (((TableRow)tblUcs.Rows[tblUcs.Rows.Count - 1]).Cells.Count == 1)
                    tr = (TableRow)tblUcs.Rows[tblUcs.Rows.Count - 1];
           }

           createDynamicReport(Int32.Parse(ViewState["dynamicReportActualId"].ToString()), ((Label)this.PopupPanel.Controls[1]).Text, hs, tr);
           saveStateOfDynamicReport(Int32.Parse(ViewState["dynamicReportActualId"].ToString()), ((Label)this.PopupPanel.Controls[1]).Text, hs);
    
           ArrayList DynamicReportsWithConditions = (ArrayList)ViewState["DynamicReportsWithConditions"];

           if (DynamicReportsWithConditions.Count > 0)
                showPopUp(ref DynamicReportsWithConditions);
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
        finally
        {
            hs.Clear();
        }
    }

    /// <summary>
    /// Evento que se ejecuta al presionar el botón "Cancelar" del PopUp
    /// </summary>
    /// <history>
    ///     [Gaston]  25/11/2008  Created
    ///     [Gaston]  27/11/2008  Modified    Se agrego un try-catch
    /// </history>
    protected void btnCancel_click(object sender, EventArgs e)
    {
        try
        {
            placeDynamicReportCheckBoxInFalse();

            ArrayList DynamicReportsWithConditions = (ArrayList)ViewState["DynamicReportsWithConditions"];

            if (DynamicReportsWithConditions.Count > 0)
                showPopUp(ref DynamicReportsWithConditions);
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
      
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        
     
    }


    protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        //Int32 QueryId;
        
        //if (e.Node.Checked && Int32.TryParse(e.Node.Value, out QueryId))
        //    LoadGenericReport(QueryId);

        //upGenericReport.Update();
        foreach (TreeNode A in TreeView1.Nodes)
        {
            foreach (TreeNode C in A.ChildNodes)
            {
                foreach (TreeNode D in C.ChildNodes)
                {
                    switch (D.Value)
                    {
                        case "UCTaskBalances":
                            this.ucTaskBalance.Visible = D.Checked;
                            break;
                        case "UCTaskToExpire":
                            this.ucTaskExpire.Visible = D.Checked;
                            break;
                        case "UCAsignedTasksCount":
                            this.ucTaskCount.Visible = D.Checked;
                            break;
                        case "UCAverageTimeInSteps":
                            this.ucAverageTime.Visible = D.Checked;
                            break;
                    }
                }
            }
        }
        
    }

    #endregion

    #region Métodos

    /// <summary>
    /// Método que sirve para limpiar la información de estado, sesión y los controles que contienen los reportes dinámicos
    /// </summary>
    /// <history>
    ///     [Gaston]  25/11/2008  Created
    /// </history>
    private void cleanSessionsViewStateAndOldDynamicReports()
    {
        // Si el control tiene más de dos filas, entonces se deben borrar los reportes dinamicos actuales
        if(tblUcs.Rows.Count > 2)
        {
            int totalTblUcsControls = tblUcs.Controls.Count;

            for (int i = 2; i < totalTblUcsControls; i++)
                tblUcs.Rows.RemoveAt(1 + 1);
        }
        
        // Información de estado que contiene los reportes que tienen condiciones. Se guarda un ArrayList con id del reporte dinamico, nombre y condiciones
        // Necesario para saber que reportes dinámicos tienen condiciones, asi, despues de presionar "Aceptar" sobre el PopUp se arma el próximo PopUp
        // (en caso de haber seleccionado más de un reporte dinámico con condiciones)
        if (ViewState["DynamicReportsWithConditions"] != null)
            ViewState["DynamicReportsWithConditions"] = null;

        // Información de estado que contiene los id de índices necesarios para completar el hashtable key: id de indice value: valor que el usuario 
        // ingreso para el índice y de esta forma buscar en la base de datos y completar el reporte dinámico
        if (ViewState["indexsId"] != null)
            ViewState["indexsId"] = null;

        // Información de estado que contiene el id actual del reporte dinámico. Necesario para cuando se presiona el botón "Aceptar" e identificar el
        // reporte dinámico
        if (ViewState["dynamicReportActualId"] != null)
            ViewState["dynamicReportActualId"] = null;

        // Sesión que contiene los id's de los reportes dinámicos. Es necesario guardarlos, junto con los nombres porque si el usuario elige un 
        // workflow de algún reporte básico se hace un postBack, y entonces cuando se vuelven a armar los controles dinámicos se deben identificar
        // por su nombre, y su id para volver a buscar en la base de datos
        if (Session["dynamicReportsId"] != null)
            Session["dynamicReportsId"] = null;

        // Sesión que contiene los nombres de los reportes dinámicos
        if (Session["dynamicReportsName"] != null)
            Session["dynamicReportsName"] = null;

        // Sesión que contiene un ArrayList donde cada elemento es un hashtable key: id del indice value: valor que el usuario ingreso para el índice. 
        // Necesario para realizar una busqueda en la base de datos, junto con el id del reporte dinamico al volver al postback y volver a completar el 
        // control dinámico
        if (Session["valuesAndConditions"] != null)
            Session["valuesAndConditions"] = null;
    }

    /// <summary>
    /// Método que sirve para mostrar los reportes seleccionados (tanto básicos como dinámicos)
    /// </summary>
    /// <history>
    ///     [Gaston]  25/11/2008  Modified
    ///     [Gaston]  27/11/2008  Modified      Se agrego un try-catch y se mejoro el código al seleccionar todos los reportes y al crear los reportes dinámicos
    /// </history>
    private void ShowReports()
    {
        ArrayList DynamicReportsWithConditions = new ArrayList();
        bool showAll = false;

        foreach (TreeNode B in this.TreeView1.Nodes)
        {
            if ((B.Value == "SeleccionarTodos") && (B.Checked == true))
                showAll = true;

            if (showAll == true)
            {
                ucTaskCount.Visible = true;
                ucAverageTime.Visible = true;
                ucTaskBalance.Visible = true;
                ucTaskExpire.Visible = true;

                foreach (TreeNode C in B.ChildNodes)
                {
                    foreach (TreeNode D in C.ChildNodes)
                    {
                        D.Checked = true;

                        if ((D.Value != "UCTaskBalances") && (D.Value != "UCTaskToExpire") && (D.Value != "UCAsignedTasksCount") && (D.Value != "UCAverageTimeInSteps"))
                            analysisOfDynamicReports(D, ref DynamicReportsWithConditions);
                    }
                }
            }
            else
            {
                foreach (TreeNode C in B.ChildNodes)
                {
                    foreach (TreeNode D in C.ChildNodes)
                    {
                        switch (D.Value)
                        {
                            case "UCTaskBalances":
                                this.ucTaskBalance.Visible = D.Checked;
                                break;
                            case "UCTaskToExpire":
                                this.ucTaskExpire.Visible = D.Checked;
                                break;
                            case "UCAsignedTasksCount":
                                this.ucTaskCount.Visible = D.Checked;
                                break;
                            case "UCAverageTimeInSteps":
                                this.ucAverageTime.Visible = D.Checked;
                                break;

                            // Es un id de reporte dinamico
                            default:
                                if (D.Checked == true)
                                    analysisOfDynamicReports(D, ref DynamicReportsWithConditions);
                                break;
                        }
                    }
                }
            }
        }

        try
        {
            foreach (TreeNode B in this.TreeView1.Nodes)
            {
                switch (B.Value)
                {
                    case "SeleccionarTodos":
                        B.Checked = false;
                        break;
                }
            }

            if (DynamicReportsWithConditions.Count > 0)
                showPopUp(ref DynamicReportsWithConditions);
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    /// <summary>
    /// Método que sirve para ver si el reporte dinámico tiene o no condiciones. Si no tiene, entonces se crea el reporte dinámico en el momento, sino,
    /// el reporte dinámico se guarda en una colección, necesario para mostrar popUps
    /// </summary>
    /// <history>
    ///     [Gaston]  27/11/2008  Created      
    /// </history>
    private void analysisOfDynamicReports(TreeNode D, ref ArrayList DynamicReportsWithConditions)
    {
        TableRow tr = new TableRow();
        ArrayList conditions = ReportBuilderComponent.GetConditionsToComplete(Int32.Parse(D.Value));
        
        if (tblUcs.Rows.Count > 2)
        {
            if (((TableRow)tblUcs.Rows[tblUcs.Rows.Count - 1]).Cells.Count == 1)
                tr = (TableRow)tblUcs.Rows[tblUcs.Rows.Count - 1];
        }

        if (conditions.Count == 0)
        {
            createDynamicReport(Int32.Parse(D.Value), D.Text, new Hashtable(), tr);
            saveStateOfDynamicReport(Int32.Parse(D.Value), D.Text, new Hashtable());
        }
        else
        {
            conditions.Add(D.Text + " - " + D.Value);
            DynamicReportsWithConditions.Add(conditions);
        }
    }

    /// <summary>
    /// Método que sirve para crear un control que contiene un gridview y de esta forma armar un reporte dinámico 
    /// </summary>
    /// <param name="DynamicReportId"> Id del reporte dinámico</param>
    /// <param name="dynamicReportName"> Nombre del reporte dinámico</param>
    /// <param name="conditions"> Condiciones key: id del indice | value: valor que el usuario ingreso para el índice</param>
    /// <param name="tr">Tablerow o fila que tendrá una celda que a su vez contendrá a un reporte dinámico</param>
    /// <history>
    ///     [Gaston]  21/11/2008  Created
    ///     [Gaston]  25/11/2008  Modified
    ///     [Gaston]  26/11/2008  Modified      Si el dataset es null entonces los datos ingresados no son válidos
    ///     [Gaston]  27/11/2008  Modified      Se agrego un try-catch
    /// </history>
    private void createDynamicReport(int DynamicReportId, string dynamicReportName, Hashtable conditions, TableRow tr)
    {
        DataSet ds = new DataSet();
        TableCell tc = new TableCell();

        try
        {
            ASP.wfreports_wfreportesv1_1_usercontrols_ucdynamicreport_ucdynamicreport_ascx dynamicReport = (ASP.wfreports_wfreportesv1_1_usercontrols_ucdynamicreport_ucdynamicreport_ascx)LoadControl("~/WfReports/WFReportesv1.1/UserControls/UCDynamicReport/UCDynamicReport.ascx");
            ds = ReportBuilderComponent.RunWebQueryBuilder(DynamicReportId, true, conditions);
            
            // Se agrega al PopUp el nombre del reporte dinamico
            dynamicReport.Title = dynamicReportName;

            if (ds != null)
                // Se agrega la tabla al origen de datos del datatable del reporte dinamico
                dynamicReport.Table = ds.Tables[0];
            else
                dynamicReport.Error = "Los datos ingresados no son válidos";

            // El control se agrega adentro de una celda
            tc.Controls.Add(dynamicReport);
            // La celda se agrega a la fila
            tr.Cells.Add(tc);
            // La fila se agrega a la colección de filas del control
            tblUcs.Rows.Add(tr);
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
        finally
        {
            if (ds != null)
            {
                ds.Clear(); 
                ds.Dispose();
            }
            tr.Dispose(); tc.Dispose();
        }
    }


    /// <summary>
    /// Método que sirve para guardar el estado de un reporte dinámico, y de esta forma volver a obtener de la base de datos y armar el control dinámico
    /// cada vez que se haga un postback, por ejemplo: al seleccionar un elemento de un combobox de un determinado reporte básico
    /// </summary>
    /// <param name="DynamicReportId"> Id del reporte dinámico</param>
    /// <param name="dynamicReportName"> Nombre del reporte dinámico</param>
    /// <param name="hs"> Condiciones key: id del indice | value: valor que el usuario ingreso para el índice</param>
    /// <history>
    ///     [Gaston]  25/11/2008  Created
    ///     [Gaston]  27/11/2008  Modified      Se agrego un try-catch
    /// </history>
    private void saveStateOfDynamicReport(int dynamicReportId, string dynamicReportName, Hashtable hs)
    {
        ArrayList dynamicReportsId = new ArrayList();
        ArrayList dynamicReportsName = new ArrayList();
        ArrayList valuesAndConditions = new ArrayList();

        try
        {
            dynamicReportsId = (ArrayList)Session["dynamicReportsId"];
            dynamicReportsName = (ArrayList)Session["dynamicReportsName"];
            valuesAndConditions = (ArrayList)Session["valuesAndConditions"];

            if (dynamicReportsId == null)
                dynamicReportsId = new ArrayList();

            if (dynamicReportsName == null)
                dynamicReportsName = new ArrayList();

            if (valuesAndConditions == null)
                valuesAndConditions = new ArrayList();

            dynamicReportsId.Add(dynamicReportId.ToString());
            dynamicReportsName.Add(dynamicReportName);
            valuesAndConditions.Add(hs);

            Session["dynamicReportsId"] = dynamicReportsId;
            Session["dynamicReportsName"] = dynamicReportsName;
            Session["valuesAndConditions"] = valuesAndConditions;
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
    }

    /// <summary>
    /// Método que sirve para mostrar un PopUp
    /// </summary>
    /// <param name="DynamicReportsWithConditions"> ArrayList que contiene los id y nombre de los reportes dinámicos más sus condiciones</param>
    /// <history>
    ///     [Gaston]  21/11/2008  Created
    ///     [Gaston]  27/11/2008  Modified      Se agrego un try-catch
    /// </history>
    private void showPopUp(ref ArrayList DynamicReportsWithConditions)
    {
        System.Collections.Generic.List<Indexs> lstList = new System.Collections.Generic.List<Indexs>();
        ArrayList indexsIds = new ArrayList();

        try
        {
            foreach (string index in (ArrayList)DynamicReportsWithConditions[0])
            {
                if (index.Contains("|"))
                {
                    // Id del índice
                    string indexId = (index.Substring(index.IndexOf(".") + 3)).Remove((index.Substring(index.IndexOf(".") + 3)).IndexOf("|") - 1);
                    indexsIds.Add("I" + indexId);
                    // Nombre del índice
                    string indexName = Zamba.ReportBuilder.Business.FuncionesZamba.GetIndexName(Convert.ToInt32(indexId));
                    // Operador
                    string condition = (index.Substring(index.IndexOf("|") + 1)).Remove((index.Substring(index.IndexOf("|") + 1)).Length - 1);

                    Indexs I = new Indexs();
                    I.IndexName = indexName.Trim();
                    I.IndexOperator = condition;
                    lstList.Add(I);
                }

                else
                {
                    // El nombre del reporte dinamico se coloca como título para el PopUp
                    this.lblDynamicReportName.Text = index.Remove(index.IndexOf("-") - 1);
                    // El id del reporte dinamico se almacena temporalmente para después buscar en la base de datos cuando el usuario presiona aceptar
                    // en el PopUp, junto con lo que haya ingresado en las cajas de texto
                    string idDynamicReport = index.Substring(index.IndexOf("-") + 2);
                    ViewState["dynamicReportActualId"] = idDynamicReport;
                }
            }

            this.ListaIndices.DataSource = lstList;
            this.ListaIndices.DataBind();

            if (DynamicReportsWithConditions.Count > 0)
                DynamicReportsWithConditions.RemoveAt(0);

            ViewState["indexsId"] = indexsIds;
            ViewState["DynamicReportsWithConditions"] = DynamicReportsWithConditions;

            PopUpUltimate.TargetControlID = "btnImplementSelection";
            PopUpUltimate.Show();
        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
        finally
        {
            lstList.Clear(); 
        }
    }

    /// <summary>
    /// Método que sirve para colocar un checkbox en false. El checkbox corresponde al reporte dinamico que se cancelo (al cancelar el PopUp asociado
    /// a ese reporte dinámico
    /// </summary>
    /// <history>
    ///     [Gaston]  25/11/2008    Created
    /// </history>
    private void placeDynamicReportCheckBoxInFalse()
    {
        foreach (TreeNode B in this.TreeView1.Nodes)
        {
            foreach (TreeNode C in B.ChildNodes)
            {
                foreach (TreeNode D in C.ChildNodes)
                {
                    if ((D.Value != "UCTaskBalances") && (D.Value != "UCTaskToExpire") && (D.Value != "UCAsignedTasksCount") && (D.Value != "UCAverageTimeInSteps"))
                    {
                        if ((D.Value == ViewState["dynamicReportActualId"].ToString()) && (D.Checked == true))
                        {
                            D.Checked = false;
                            return;
                        }
                    }
                }
            }
        }
    }
        
    private void LoadGenericReports()
    {
        Dictionary<String, String> Queries = new Dictionary<string, string>();
        DataSet ds = null;
        if (null != Session["UserId"])
            ds = ReportBuilderComponent.GetQueryIdsAndNames(Int64.Parse(Session["UserId"].ToString()));
        else
            ds = ReportBuilderComponent.GetAllQueryIdsAndNames();
        Dictionary<Int32, String> IdsNames = new Dictionary<Int32, String>();

        foreach (DataRow dr in ds.Tables[0].Rows)
            IdsNames.Add(Int32.Parse(dr[0].ToString()), dr[1].ToString());

        Int32 QueryId;
        String CurrentQuery;
        ds = new DataSet();
        ds.Tables.Add(new DataTable());
        ds.Tables[0].Columns.Add(new DataColumn("QueryName"));
        ds.Tables[0].Columns.Add(new DataColumn("Query"));
        ds.Tables[0].Columns.Add(new DataColumn("GraphicPath"));
        DataSet dstemp;
        foreach (string NodeValue in _checkedNodes)
        {
            if (Int32.TryParse(NodeValue, out QueryId) && IdsNames.ContainsKey(QueryId))
            {
                CurrentQuery = ReportBuilderComponent.GenerateQueryBuilder(QueryId, true);
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][0] = IdsNames[QueryId];
                ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][1] = CurrentQuery;
                //ExcelReportService.ExcelService excelService = new ExcelReportService.ExcelService();
                //string filename;
                //filename = Server.MapPath("~") + "\\" + IdsNames[QueryId] + ".jpg";
                //try
                //{
                    //dstemp = Zamba.Servers.Server.get_Con(false, false, false).ExecuteDataset(CommandType.Text, CurrentQuery);
                    //try
                    //{
                        //excelService.setExcelGraphics(dstemp, "LINEAS", filename, true, false);
                    //}
                    //catch (Exception ex)
                    //{
                //        excelService.setExcelGraphics2(dstemp, "LINEAS", filename, true);
                //    }
                //}
                //catch (Exception ex)
                //{

                //}
                //ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][2] = filename;
            }
        }

        //rpGeneric.DataSource = ds;
        //rpGeneric.DataBind();
    }


    //private Color getColor(string hexa)
    //{
    //    int r, g, b;
    //    r = Convert.ToInt32(hexa.Substring(1, 2), 16);
    //    g = Convert.ToInt32(hexa.Substring(3, 2), 16);
    //    b = Convert.ToInt32(hexa.Substring(5, 2), 16);
    //    return (Color.FromArgb(r, g, b));
    //}

    private void LoadGenericReport(Int32 queryId)
    {
        String Query = ReportBuilderComponent.GenerateQueryBuilder(queryId, true);
        WfReports_UserControls_UCGenericReport_UcGenericReport GenericReport = new WfReports_UserControls_UCGenericReport_UcGenericReport();
        GenericReport.Query = Query;
        //divGenericos.Controls.Add(GenericReport);
    }

 
  /*  protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {/*
        switch (this.TreeView1.SelectedValue)
        {
            case "Balances":
            case "UCTaskBalances":
            case "UCAverageTimeInSteps":
                this.TabContainer1.ActiveTabIndex = 0;
                break;
            case "Vencimiento":
            case "UCTaskToExpire":
                this.TabContainer1.ActiveTabIndex = 1;
                break;
            case "Cantidad":
            case "UCAsignedTasksCount":
                this.TabContainer1.ActiveTabIndex = 2;
                break;
            default:
                break;
        }*/
    
    private void AddReport(string Report)
    {
        ArrayList Reports;
        Reports = (ArrayList)Session["Reports"];
        if (Reports == null)
        {
            Reports = new ArrayList();
        }
        if (Reports.Contains(Report) == false)
        {
            Reports.Add(Report);
            Session["Reports"] = Reports;

        }
    }
    private void RemoveReport(string Report)
    {
        ArrayList Reports;
        Reports = (ArrayList)Session["Reports"];
        if (Reports == null)
        {
            Reports = new ArrayList();
        }
        if (Reports.Contains(Report) == true)
        {
            Reports.Remove(Report);
            Session["Reports"] = Reports;
        }

    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        switch (e.Item.DataItem.ToString())
        {
            //case "UCAverageTimeInStepsByWorkflow":
            //    this.UCAverageTimeInStepsByWorkflow.Visible = true;
            //    break;
            //case "UCAverageTimeInStepsByUser":
            //    this.TabUCAverageTimeInStepsByUser.Visible = true;
            //    break;

            //Control UC = LoadControl("~/UserControls/UCUsersAsigned/UCUsersAsignedByWorkflow.ascx");

            //new UserControls_UCUsersAsigned_UCUsersAsignedByWorkflow();
            //             UCTaskToExpireByWorkflow UC = new UCTaskToExpireByWorkflow();

            //this.Panel1.Controls.Add(UC);
            //this.Panel1.DataBind();
            default:
                break;
        }

        //Microsoft.Reporting.WebForms.ReportViewer RV = (Microsoft.Reporting.WebForms.ReportViewer)e.Item.FindControl("ReportViewer1");
        //RV.LocalReport.ReportPath = ("Reports/rptUsersAsigned.rdlc");    

    }
   

    //protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    //{
    //    Boolean showall = false ;

    //    switch (e.Node.Text.ToUpper())
    //    {
    //        case "SELECCIONAR TODOS":
    //            showall = e.Node.Checked;
    //            break;
    //        default:
    //            break;
    //    }

    //    if (showall)
    //    {
    //        foreach (TreeNode C in TreeView1.Nodes)
    //        {
    //            foreach (TreeNode D in C.ChildNodes)
    //            {
    //                D.Checked = true;
    //                this.iframeTasksBalances.Visible = true;
    //                this.iframeTasksToExpire.Visible = true;
    //                this.iframeAsignedTasks.Visible = true;
    //                this.iframeAverageTime.Visible = true;
    //            }
    //        }
    //    }

    //    foreach (TreeNode B in this.TreeView1.Nodes)
    //    {
    //        switch (B.Value)
    //        {
    //            case "SeleccionarTodos":
    //                B.Checked = false;
    //                break;
    //        }
    //    }
    //}

    private void LoadUserState()
    {
        StringBuilder QueryBuilder = new StringBuilder();
        QueryBuilder.Append("Select Value from zopt where Item='");
        QueryBuilder.Append(ZOPT_PREFIX);
        QueryBuilder.Append(Session["UserId"]);
        QueryBuilder.Append("'");

        DataSet ds = Zamba.Servers.Server.get_Con(false, false, false).ExecuteDataset(CommandType.Text, QueryBuilder.ToString());
        QueryBuilder.Remove(0, QueryBuilder.Length);

        if (ds.Tables[0].Rows.Count > 0)
        {

            List<String> values = new List<String>();
            values.AddRange(ds.Tables[0].Rows[0][0].ToString().Split(";".ToCharArray()));

            if (values.Count > 0)
            {
                foreach (TreeNode node in TreeView1.Nodes[0].ChildNodes)
                {
                    node.Checked = values.Contains(node.Value);

                    foreach (TreeNode childNode in node.ChildNodes)
                    {
                        childNode.Checked = values.Contains(childNode.Value);
                    }
                }
            }
        }
    }

    private void SaveUserState()
    {
        foreach (TreeNode Node in TreeView1.Nodes[0].ChildNodes)
        {
            if (Node.Checked)
                _checkedNodes.Add(Node.Value);

            foreach (TreeNode ChildNode in Node.ChildNodes)
            {
                if (ChildNode.Checked)
                    _checkedNodes.Add(ChildNode.Value);
            }
        }

        StringBuilder QueryBuilder = new StringBuilder();
        QueryBuilder.Append("select count(item) from zopt where item = '");
        QueryBuilder.Append(ZOPT_PREFIX);
        QueryBuilder.Append(Session["UserId"]);
        QueryBuilder.Append("'");

        Int32 ReturnValue = (Int32)Zamba.Servers.Server.get_Con(false, false, false).ExecuteScalar(CommandType.Text, QueryBuilder.ToString());
        QueryBuilder.Remove(0, QueryBuilder.Length);

        if (ReturnValue == 0)
        {
            QueryBuilder.Append("insert into zopt values('");
            QueryBuilder.Append(ZOPT_PREFIX);
            QueryBuilder.Append(Session["UserId"]);
            QueryBuilder.Append("','");

            if (_checkedNodes.Count > 0)
            {
                foreach (String CheckedNode in _checkedNodes)
                {
                    QueryBuilder.Append(CheckedNode);
                    QueryBuilder.Append(";");
                }
                QueryBuilder.Remove(QueryBuilder.Length - 1, 1);
            }
            QueryBuilder.Append("')");
        }
        else
        {
            QueryBuilder.Append("update zopt set value = '");
            if (_checkedNodes.Count > 0)
            {
                foreach (String CheckedNode in _checkedNodes)
                {
                    QueryBuilder.Append(CheckedNode);
                    QueryBuilder.Append(";");
                }
                QueryBuilder.Remove(QueryBuilder.Length - 1, 1);
            }
            QueryBuilder.Append("' where item = '");
            QueryBuilder.Append(ZOPT_PREFIX);
            QueryBuilder.Append(Session["UserId"]);
            QueryBuilder.Append("'");
        }

        try
        {
            Zamba.Servers.Server.get_Con(false, false, false).ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString());
        }
        catch (Exception ex)
        { }

        QueryBuilder.Remove(0, QueryBuilder.Length);
        QueryBuilder = null;
    }

    protected void StartedQuery()
    {
    }

    #endregion

    // Clase privada utilizada para almacenar temporalmente un nombre de índice, operador y lo ingresado por el usuario
    /// <history>
    ///     [Gaston]  21/11/2008  Created
    /// </history>     
    [Serializable]
    private class Indexs
    {
        public Indexs()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        String indexName;

        public String IndexName
        {
            get { return indexName; }
            set { indexName = value; }
        }

        String indexOperator;

        public String IndexOperator
        {
            get { return indexOperator; }
            set { indexOperator = value; }
        }

        String indexData;

        public String IndexData
        {
            get { return indexData; }
            set { indexData = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <history>
    ///     [Ezequiel]  23/01/2009  Modified
    /// </history>  
    protected void TreeView1_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            if ((null != this.TreeView1.SelectedNode) && (string.Compare(this.TreeView1.SelectedNode.Value.ToString(), "SeleccionarTodos") == 0))
            {
                if (string.Compare(this.TreeView1.SelectedNode.Text.ToString(), "Deseleccionar Todos") == 0)
                {
                    foreach (TreeNode x in ((TreeView)sender).Nodes)
                        foreach (TreeNode i in x.ChildNodes)
                            foreach (TreeNode z in i.ChildNodes)
                                z.Checked = false;
                    this.TreeView1.SelectedNode.Text = "Seleccionar Todos";
                }
                else if (string.Compare(this.TreeView1.SelectedNode.Text.ToString(), "Seleccionar Todos") == 0)
                {
                    foreach (TreeNode x in ((TreeView)sender).Nodes)
                        foreach (TreeNode i in x.ChildNodes)
                            foreach (TreeNode z in i.ChildNodes)
                                z.Checked = true;
                    this.TreeView1.SelectedNode.Text = "Deseleccionar Todos";
                }
            }
        }

        //if (string.Compare(((TreeView)sender).SelectedNode.Value.ToString(), "SeleccionarTodos") == 0)
        //{
        //    TreeView1.SelectedNode.Text = (TreeView1.SelectedNode.Text == "Seleccionar Todos" ? "Deseleccionar Todos" : "Seleccionar Todos");
        //    foreach (TreeNode A in TreeView1.Nodes)
        //    {
        //        foreach (TreeNode C in A.ChildNodes)
        //        {
        //            foreach (TreeNode D in C.ChildNodes)
        //            {
        //                if (D.Checked == true && TreeView1.SelectedNode.Text == "Seleccionar Todos")
        //                {
        //                    D.Checked = false;
        //                }

        //                else if (TreeView1.SelectedNode.Text == "Deseleccionar Todos")
        //                {
        //                    D.Checked = true;
        //                }
        //            }
        //        }
        //    }
        //}
        }
}