using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Collections.Generic;
using System.Text;
using Zamba.Core;


public partial class Main : System.Web.UI.Page

{
    



    #region Fields
    public enum ResultsLoadType
    {
        FILTERED,
        ALL,
        CATALOGED,
        CATALOGEDSORTED,
        ALLSORTED,
        FILTEREDSORTED,
        SESSIONS
    }
    Hashtable hshDataType = new Hashtable();
    private const Int32 GV_DOCTYPES_VALUEINDEX = 1;
    private const Int16 CONST_DOCUMENT_NAME_COLUMN = 2;
    #endregion

    #region Sessions


    /*
        Session["SortSTR"]: Contiene la expresion para ordenamiento de la grilla
        Session["SortOrder"]: ASC O DESC (Orden de la grilla)
        Session["ShowDocuments"]

	            "FILTERED"
	            "ALL"
	            "SORTED"
	            "CATALOGED"

        Session["List"]
        Session["ResultsCount"] Contiene la cantidad de results, lo utilizo para paginar
        Session["PagingId"] contiene el numero de pagina actual, Utilizado para paginado
     *  Session["UserId"] contiene el id de usuario
    */
    #endregion

    #region Eventos

  
   

    /// <summary>
    /// Cuando se carga la pagina carga todos los tipos de documento disponibles
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                Session["PageSize"] = GetPageSize();
                Session["PagingId"] = 1;
                Session["ResultsCount"] = 0;
                Session["ShowDocuments"] = ResultsLoadType.ALL;
                Session["SortSTR"] = string.Empty;

                if (null != Session["UserId"])
                {

                    //Diego: Flag utilizada porque despues de la seleccion del popup para indices de sustitucion y busqueda Postback == true
                    // Y se carga la sesion con indices de otro doc type (el primero del combo)
                    if (Session["DoPostBack"] == null)
                    {
                        if (null != cmbFilter.SelectedValue && string.IsNullOrEmpty(cmbFilter.SelectedValue.ToString()) == false)
                        {   
                            SetIndexType();
                        }
                        else
                        {  
                            btnShowList.Visible = false;
                        }

                        ArrayList _arraylist = Zamba.Services.DocType.GetDocTypesIdsAndNamesbyRightView(Int32.Parse(Session["UserId"].ToString()));
                        if (_arraylist != null)
                        {
                            //if (ds.Tables.Count > 0)
                            //{
                            //DataView dv = new DataView(ds.Tables[0].Copy());
                            //dv.Sort = "Doc_Type_name Asc";
                            //this.cmbDocType.DataSource = dv.ToTable();
                            this.cmbDocType.DataSource = _arraylist;
                            this.cmbDocType.DataBind();
                            GetIndexs(true);
                            getDocs(true, false, true, string.Empty);

                            if (ShowCatalog() == false)
                            {
                                ChkAgroup.Checked = false;
                                ChkAgroup_OnCheckedChanged(sender, e);
                                ChkAgroup.Visible = false;
                            }
                            else
                            {
                                ChkAgroup.Checked = true;
                                ChkAgroup_OnCheckedChanged(sender, e);
                                ChkAgroup.Visible = true;
                                LoadCatalogGrid();

                            }

                            //}
                        }

                    }
                }
                else
                    FormsAuthentication.RedirectToLoginPage();
            }


        }


        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        }
    }
    
    
    /// <summary>
    /// Cambia el tipo de documento trae los indices 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmbDocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtFirstFilter.Text = string.Empty;
            lblTotal.Text = string.Empty;
            Session["PagingId"] = 1;
            Session["SortSTR"] = string.Empty;
            txtSecondFilter.Text = string.Empty;
            this.gvDocuments.DataSource = null;
            this.gvDocuments.DataBind();
            if (LoadDocumentsOnDTSelection() == true)
            {
                Session["ShowDocuments"] = ResultsLoadType.ALL;
                GetIndexs(true);
                getDocs(true, false, true, string.Empty);
            }
            SetIndexType();
        }
        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        }

        
    }
    
    
    ///// <summary>
    ///// Paginado de la Grilla
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void gvDocuments_OnPageIndexChanging(object sender,GridViewPageEventArgs e)
    //{
    //    gvDocuments.PageIndex = e.NewPageIndex;
    //    gvDocuments.DataBind();
    //}

    /// <summary>
    /// Cambian los indices trae todos los valores que se encuentran en el mismo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 

    protected void cmbIndex_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFirstFilter.Text = string.Empty;
        txtSecondFilter.Text = string.Empty;
        lblTotal.Text = string.Empty;
        this.gvDocuments.DataSource = null;
        this.gvDocuments.DataBind();
        GetIndexs(false);
        LoadCatalogGrid();
    }

    /// <summary>
    /// Trae los documentos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDocTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["ShowDocuments"] = ResultsLoadType.CATALOGED;
        //getIndexs(false);
        getDocs(true, true, true, string.Empty);
    }

    /// <summary>
    /// Quita la Hora del Formato Date
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void gvDocTypes_OnRowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    foreach (TableCell cell in e.Row.Cells)
    //    {
    //        if (cell.Controls.Count >= 3)
    //        {
    //            if (null != cell.Controls[1])
    //            {

    //                DateTime dt;
    //                if (DateTime.TryParse(((HyperLink)cell.Controls[GV_DOCTYPES_VALUEINDEX]).Text, out dt) == true)
    //                {
    //                    ((HyperLink)cell.Controls[GV_DOCTYPES_VALUEINDEX]).Text = dt.ToShortDateString();
    //                }
    //            }
    //        }
    //    }
    //}

    /// <summary>
    /// Oculta el fullpath
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDocuments_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 0)
        {
            e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
            e.Row.Cells[e.Row.Cells.Count - 2].Visible = false;
            e.Row.Cells[e.Row.Cells.Count - 3].Visible = false;
            if (ShowDocumentName() == false)
                e.Row.Cells[1].Visible = false;
        }

        System.Data.DataRowView drv;
        drv = (System.Data.DataRowView)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            gvDocuments.HeaderStyle.Wrap = false;
            foreach (DataControlField df in gvDocuments.Columns)
            {
                df.ItemStyle.Wrap = false;
            }
        }
    }

    /// <summary>
    /// Trae el documento
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    protected void gvDocuments_SelectedIndexChanged(object sender, EventArgs e)
    {
        setURL();
    }

    protected void gvDocuments_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //Use the RowType property to determine whether the 
        //row being created is the header row. 
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Call the GetSortColumnIndex helper method to determine
            //the index of the column being sorted.
            Int32 sortColumnIndex = GetSortColumnIndex();

            if (sortColumnIndex != -1)
            {

                //Call the AddSortImage helper method to add
                //a sort direction image to the appropriate
                //column header. 
                AddSortImage(sortColumnIndex, e.Row);

            }
        }
    }

    protected void AddSortImage(Int32 columnIndex, GridViewRow row)
    {
        //Create the sorting image based on the sort direction.
        Image sortImage = new Image();
        if (gvDocuments.SortDirection == SortDirection.Ascending)
        {
            //sortImage.ImageUrl = "~/Images/Ascending.jpg"
            sortImage.AlternateText = "Ascending Order";
        }
        else
        {
            sortImage.ImageUrl = "~/Images/Descending.jpg";
            sortImage.AlternateText = "Descending Order";
        }
        // Add the image to the appropriate header cell.
        //row.Cells(columnIndex).Controls.Add(sortImage)

    }

    protected Int32 GetSortColumnIndex()
    {
        //Iterate through the Columns collection to determine the index
        //of the column being sorted.

        foreach (DataControlField field in gvDocuments.Columns)
        {
            if (field.SortExpression == gvDocuments.SortExpression)
            {
                return gvDocuments.Columns.IndexOf(field);
            }
        }
        return -1;

    }

    /// <summary>
    /// Activa el filtro
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        Session["PagingId"] = 1;
        Session["ShowDocuments"] = ResultsLoadType.FILTERED;
        //getIndexs(false);
        getDocs(false, true, true, string.Empty);
    }


    /// <summary>
    /// Muestra un popup con una lista de sustitucion
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShowList_Click(object sender, EventArgs e)
    {
        try
        {

            Int64 indexId = Int64.Parse(cmbFilter.SelectedValue.ToString());
            Int16 indextypeid = Zamba.Services.Index.GetIndexDropDownType(indexId);
            //0 = normal, 1= busqueda 2 = sustitucion
            DataTable dt = new DataTable();
            switch (indextypeid)
            {
                case 0:

                    break;
                case 1:
                    txtFirstFilter.Visible = true;
                    dt = Zamba.Services.Index.LoadIndexFindTypeValues(Int64.Parse(cmbFilter.Text));
                    if (dt != null)
                    {
                        gvSustitutionList.DataSource = dt;
                        //btnCancelarIndex.Visible = false;
                        gvSustitutionList.DataBind();
                        //gvSustitutionList.HeaderRow.Visible = false;
                        gvSustitutionList.Height = 50;
                        PopupDialog.Show();
                    }

                   
                    break;
                case 2:

                    dt = Zamba.Services.Index.GetIndexSubstitutionTable(Int64.Parse(cmbFilter.SelectedValue));
                    if (dt != null)
                    {
                        gvSustitutionList.DataSource = dt;
                        //btnCancelarIndex.Visible = true;
                        gvSustitutionList.DataBind();
                        //gvSustitutionList.HeaderRow.Visible = true;
                        gvSustitutionList.Height = 50;
                        PopupDialog.Show();
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        }
    }

    public void RefreshGrid()
    {
        btnSeeDocs_Click(null, null);
    }

    /// <summary>
    /// Combo de filtros
    /// 
    /// 
 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbOperadoresDate.Visible = false;
        txtSecondFilter.Visible = false;
        txtFirstFilter.Text = string.Empty;
        txtSecondFilter.Text = string.Empty;
        GetIndexs(false);
        SetIndexType();
        setFilters();
    }

    private void SetIndexType()
    {
        Int64 indexId = Int64.Parse(cmbFilter.SelectedValue.ToString());
        Int16 indextypeid = Zamba.Services.Index.GetIndexDropDownType(indexId);
        //0 = normal, 1= busqueda 2 = sustitucion
        switch (indextypeid)
        {
            case 0:
                btnShowList.Visible = false;
                txtFirstFilter.Visible = true;
                break;

            case 1:
                btnShowList.Visible = true;
                txtFirstFilter.Visible = true;
                btnShowList.Text = "V";
                btnShowList.Width = 18;
                break;
            case 2:
                btnShowList.Visible = true;
                btnShowList.Text = "...";
                btnShowList.Width = 25;
                break;
        }
    }


    /// <summary>
    /// Trae los documentos en un dataset y setea las columnas de la grilla de documentos
    /// Lo uso para ordenar el dataset y mostrarlo
    /// </summary>
    /// <param name="refreshCombo">True si hay que llenar el combo de columnas</param>
    protected void cmbOperadores_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            GetIndexs(false);

            Int32 IndexId = Int32.Parse(cmbFilter.SelectedValue);
            String Indexname =Zamba.Core.IndexsBussines.GetIndexName(IndexId);

            if (Session["List"] != null)
            {

                    switch (cmbOperadores.SelectedItem.ToString().ToUpper())
                    {
                        case "ES NULO":
                            cmbOperadoresDate.Visible = false;
                            txtFirstFilter.Text = string.Empty;
                            txtFirstFilter.Visible = false;
                            txtSecondFilter.Text = string.Empty;
                            txtSecondFilter.Visible = false;
                            break;
                        case "ENTRE":
                            cmbOperadoresDate.Visible = false;
                            txtFirstFilter.Visible = true;
                            txtSecondFilter.Visible = true;
                            txtFirstFilter.Text = string.Empty;
                            txtSecondFilter.Text = string.Empty;
                            break;
                        default:
                            cmbOperadoresDate.Visible = false;
                            txtFirstFilter.Visible = true;
                            txtSecondFilter.Visible = false;
                            txtFirstFilter.Text = string.Empty;
                            txtSecondFilter.Text = string.Empty;

                            break;
                    }

                    //SortedList st = (SortedList)Session["List"];

                    //if ((cmbOperadores.SelectedIndex == 0 && hshDataType[IndexId].ToString() == "4") || (cmbOperadores.SelectedIndex == 0 && hshDataType[IndexId].ToString() == "5"))
                    //{
                    //    cmbOperadoresDate.Visible = false;
                    //    txtSecondFilter.Visible = false;
                    //}
                    //else
                    //{
                    //    if ((hshDataType[IndexId].ToString() == "4" || hshDataType[IndexId].ToString() == "5") && cmbOperadores.SelectedValue == "Entre")
                    //    {
                    //        cmbOperadoresDate.Visible = true;
                    //        txtSecondFilter.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        cmbOperadoresDate.Visible = false;
                    //        txtSecondFilter.Visible = false;
                    //    }
                    //}
                }
        }
        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        }
    }

    protected void btnSeeDocs_Click(object sender, EventArgs e)
    {
        Session["ShowDocuments"] = ResultsLoadType.ALL;
        Session["SortSTR"] = string.Empty;
        Session["PagingId"] = 1;
        txtFirstFilter.Text = string.Empty;
        txtSecondFilter.Text = string.Empty;
        this.gvDocuments.DataSource = null;
        this.gvDocuments.DataBind();
        //getIndexs(false);
        getDocs(true, false, true, string.Empty);
    }

    
    /// <summary>
    /// Sortea las columnas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OnSorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            Session["PagingId"] = 1;

            //if (gvDocuments.DataSource != null)
            //{
            if (Session["SortOrder"] == null)
            {
                Session["SortOrder"] = "DESC";
            }
            else
            {
                if (Session["SortOrder"].ToString() == "DESC")
                    Session["SortOrder"] = "ASC";
                else
                    Session["SortOrder"] = "DESC";

            }

            Char[] mander = { Char.Parse(" ") };
            //string _Sort = string.Empty;
            Session["SortSTR"] = string.Empty;
            for (int i = 0; i < e.SortExpression.Split(mander).Length; i++)
            {
                if (e.SortExpression.Split(mander)[i] != "ASC" && e.SortExpression.Split(mander)[i] != "DESC")
                    //_Sort += e.SortExpression.Split(mander)[i] + " ";
                    Session["SortSTR"] += e.SortExpression.Split(mander)[i] + " ";


            }
            Session["SortSTR"] = Session["SortSTR"].ToString() + Session["SortOrder"].ToString();
            switch ((ResultsLoadType)Session["ShowDocuments"])
            {
                case ResultsLoadType.FILTERED:
                    //getIndexs(true);
                    getDocs(false, true, true, Session["SortSTR"].ToString());
                    break;
                case ResultsLoadType.ALL:
                    //getIndexs(true);
                    getDocs(false, false, true, Session["SortSTR"].ToString());
                    break;
                case ResultsLoadType.CATALOGED:
                    //getIndexs(true);
                    getDocs(false, true, true, Session["SortSTR"].ToString());
                    break;
                default:
                    break;
            }

        }
        //}
        catch (Exception ex)
        {
            writeLog("Error:" + ex.Message + " Trace:" + ex.StackTrace);
        }
    }

    protected void ChkAgroup_OnCheckedChanged(object sender, EventArgs e)
    {
        if (ChkAgroup.Checked == false)
        {
            lblIndex.Visible = false;
            cmbIndex.Visible = false;
            gvDocTypes.Visible = false;
            if (ShowCatalog() == false)
            {
                rowCategory.Visible = false;
            }
            updList.Update();
        }
        else
        {
            lblIndex.Visible = true;
            cmbIndex.Visible = true;
            gvDocTypes.Visible = true;
            if (ShowCatalog() == false)
            {
                panelAgroup.Visible = true;
            }
            updList.Update();
        }
    }

    protected void gvSustitutionList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gvSustitutionList.SelectedRow.Cells.Count > 2)
                txtFirstFilter.Text = gvSustitutionList.SelectedRow.Cells[2].Text.Trim();
        else
            txtFirstFilter.Text = gvSustitutionList.SelectedRow.Cells[1].Text.Trim();
      
        Session["DoPostBack"] = "true";
    }

    /// <summary>
    /// Evento Click del boton de navegacion Volver
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnBack_OnClick(object sender, EventArgs e)
    {
        SetNavigation("BtnBack");
    }

    /// <summary>
    /// Evento Click del boton de navegacion siguiente
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnNext_OnClick(object sender, EventArgs e)
    {
        SetNavigation("BtnNext");
    }

    /// <summary>
    /// Evento Click del boton de navegacion primero
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFirst_OnClick(object sender, EventArgs e)
    {
        SetNavigation("BtnFirst");
    }

    /// <summary>
    /// Evento Click del boton de navegacion ultimo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLast_OnClick(object sender, EventArgs e)
    {
        SetNavigation("BtnLast");
    }

    #endregion

    #region Metodos



    /// <summary>
    /// Setea la navegacion (paginado, Ordenamiento)
    /// </summary>
    private void SetNavigation(String SenderName)
    {
        Int16 PageId = Int16.Parse(Session["PagingId"].ToString());
        Int16 PageSize = Int16.Parse(Session["PageSize"].ToString());
        Int32 ResultsCount = Int32.Parse(Session["ResultsCount"].ToString());
        switch (SenderName)
        {
            case "BtnNext":
                if ((PageId * PageSize) >= ResultsCount)
                {
                    EnableSearchButtons(true, false);
                    return;
                }
                else
                {
                    Session["PagingId"] = PageId + 1;
                    EnableSearchButtons(false, true);
                }

                break;
            case "BtnFirst":
                Session["PagingId"] = 1;

                EnableSearchButtons(true, false);

                break;
            case "BtnBack":
                if (PageId <= 1)
                {
                    EnableSearchButtons(false, true);
                    return;
                }
                else
                {
                    EnableSearchButtons(false, true);
                    Session["PagingId"] = PageId - 1;
                }

                break;
            case "BtnLast":
                EnableSearchButtons(false, true);
                Session["PagingId"] = ((ResultsCount) / PageSize) + 1;
                break;
        }

        switch ((ResultsLoadType)Session["ShowDocuments"])
        {
            case ResultsLoadType.FILTERED:
                //getIndexs(true);
                getDocs(false, true, true, Session["SortSTR"].ToString());
                break;
            case ResultsLoadType.ALL:
                //getIndexs(true);
                getDocs(false, false, true, Session["SortSTR"].ToString());
                break;
            case ResultsLoadType.CATALOGED:
                //getIndexs(true);
                getDocs(false, true, true, Session["SortSTR"].ToString());
                break;
            case ResultsLoadType.CATALOGEDSORTED:
                //getIndexs(true);
                getDocs(false, true, true, Session["SortSTR"].ToString());
                break;
            case ResultsLoadType.ALLSORTED:
                //getIndexs(true);
                getDocs(false, true, true, Session["SortSTR"].ToString());
                break;
            case ResultsLoadType.FILTEREDSORTED:
                //getIndexs(true);
                getDocs(false, true, true, Session["SortSTR"].ToString());
                break;

        }

    }

    /// <summary>
    /// Obtiene la cantidad de results mostrados en el paginado de la grilla
    /// </summary>
    static Int16 GetPageSize()
    {
        return Int16.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["PageSize"]);
    }

    /// <summary>
    /// Obtiene del webconfig si se va a mostrar el catalogo de los results o no
    /// </summary>
    static Boolean ShowCatalog()
    {
        return Boolean.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowCatalog"]);
    }

    /// <summary>
    /// Obtiene del webconfig si se va a mostrar el nombre del result o no
    /// </summary>
    static Boolean ShowDocumentName()
    {
        return Boolean.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowDocumentName"]);
    }

    /// <summary>
    /// Obtiene del webconfig si se va a cargar los results al seleccionar doctype en combo
    /// </summary>
    static Boolean LoadDocumentsOnDTSelection()
    {
        return Boolean.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["LoadDocumentsOnDTSelection"]);
    }

    /// <summary>
    /// Ordena un DataSet, por defecto en la tabla[0], en la columna indicada y
    /// con el tipo de sorteo indicado. Devuelve una vista con el ordenamiento.
    /// [Alejandro]
    /// </summary>
    /// <param name="ds">DataSet a ordenar</param>
    /// <param name="column">Columna por la que se ordenará</param>
    /// <param name="sorttype">Tipo de ordenamiento: ASC(ascendente), DESC(descendente)</param>
    private static DataView SortDataTable(DataTable dt, String column, String sorttype)
    {
        DataView resultView = dt.DefaultView;
        StringBuilder sortText = new StringBuilder();
        sortText.Append(column);
        sortText.Append(" ");
        sortText.Append(sorttype.ToUpper());
        resultView.Sort = sortText.ToString();
        return resultView;
    }


    ///// <summary>
    ///// Trae los indices
    ///// </summary>
    //private void getIndexs()
    //{
    //    try
    //    {



    //    }
    //    catch (Exception ex)
    //    {
    //        writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
    //    }
    //}

    private void LoadCatalogGrid()
    {
        try
        {
            gvDocTypes.DataSource = null;
            if (this.cmbIndex.SelectedIndex >= 0 && this.cmbDocType.SelectedIndex >= 0)
            {
                Int32 docTypeId = Int32.Parse(cmbDocType.SelectedValue);
                Int32 indexId = Int32.Parse(cmbIndex.SelectedValue);
                Int32 UserId = Int32.Parse(Session["UserId"].ToString());

                if (UserId > 0)
                {
                    DataSet ds = Zamba.Services.Index.GetDistinctIndexValues(docTypeId, indexId, UserId, Int32.Parse(hshDataType[(Int64)indexId].ToString()));
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                gvDocTypes.DataSource = ds;
                            }
                        }
                    }
                }
            }
            gvDocTypes.DataBind();
            this.updList.Update();
            this.UpdGrid.Update();
        }
        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        }
    }

    /// <summary>
    /// Trae los documentos y llena la grilla y el combo de columnas
    /// </summary>
    /// <param name="refreshCombo">True si hay que llenar el combo de columnas</param>
    private void getDocs(Boolean refreshCombo, Boolean useFilter, Boolean LoadGrid, String SortExpression)
    {
        try
        {
            DataTable dt = null;
            List<ArrayList> genIndex;
            Int32 resultcount = 0;
            Int16 PageId = Int16.Parse(Session["PagingId"].ToString());
            Int16 PageSize = Int16.Parse(Session["PageSize"].ToString());
            Int32 docTypeId = Int32.Parse(this.cmbDocType.SelectedValue);
            Int32 indexId = Int32.Parse(cmbFilter.SelectedValue);
            Int32 UserId = Int32.Parse(Session["UserId"].ToString());

            string replaceFor = System.Web.Configuration.WebConfigurationManager.AppSettings["ReplaceFor"].ToString();
            string replaceValue = System.Web.Configuration.WebConfigurationManager.AppSettings["ReplaceValue"].ToString();

            genIndex = GetIndexs(false);

            if (docTypeId > 0 && UserId > 0)
            {
                if (useFilter == true)
                {
                    if (this.gvDocTypes.SelectedValue != null && ChkAgroup.Checked == true)
                    {
                        if ((ResultsLoadType)Session["ShowDocuments"] == ResultsLoadType.FILTERED)
                        {
                            //if (string.IsNullOrEmpty(txtFirstFilter.Text) == false)
                            //{
                                dt = Zamba.Services.Result.getResultsAndPageQueryResults(PageId, PageSize, docTypeId, Int64.Parse(cmbFilter.SelectedValue.ToString()), genIndex, UserId, txtFirstFilter.Text, txtSecondFilter.Text, cmbOperadores.SelectedValue.ToString(), true, SortExpression, replaceValue, replaceFor);
                                resultcount = Zamba.Services.Result.getResultsCount(docTypeId, Int64.Parse(cmbFilter.SelectedValue.ToString()), genIndex, UserId, txtFirstFilter.Text, txtSecondFilter.Text, cmbOperadores.SelectedValue.ToString(), true);
                            //}
                        }
                        else
                        {
                            dt = Zamba.Services.Result.getResultsAndPageQueryResults(PageId, PageSize, docTypeId, Int64.Parse(cmbIndex.SelectedValue.ToString()), genIndex, UserId, this.gvDocTypes.SelectedValue.ToString(), string.Empty, "=", useFilter, SortExpression, replaceValue, replaceFor);
                            resultcount = Zamba.Services.Result.getResultsCount(docTypeId, Int64.Parse(cmbIndex.SelectedValue.ToString()), genIndex, UserId, this.gvDocTypes.SelectedValue.ToString(), string.Empty, "=", useFilter);
                        }
                    }
                    else
                    {
                        //if (string.IsNullOrEmpty(txtFirstFilter.Text) == false)
                       //{
                            dt = Zamba.Services.Result.getResultsAndPageQueryResults(PageId, PageSize, docTypeId, Int64.Parse(cmbFilter.SelectedValue), genIndex, UserId, txtFirstFilter.Text, txtSecondFilter.Text, cmbOperadores.SelectedValue.ToString(), true, SortExpression, replaceValue, replaceFor);
                            resultcount = Zamba.Services.Result.getResultsCount(docTypeId, Int64.Parse(cmbFilter.SelectedValue), genIndex, UserId, txtFirstFilter.Text, txtSecondFilter.Text, cmbOperadores.SelectedValue.ToString(), true);
                        //}
                    }
                    Session["Datos"] = true;
                }
                else
                {
                    Session["Datos"] = false;
                    dt = Zamba.Services.Result.getResultsAndPageQueryResults(PageId, PageSize, docTypeId, 0, genIndex, UserId, String.Empty, String.Empty, cmbOperadores.SelectedValue.ToString(), false, SortExpression, replaceValue, replaceFor);
                    resultcount = Zamba.Services.Result.getResultsCount(docTypeId, indexId, genIndex, UserId, String.Empty, String.Empty, String.Empty, useFilter);
                }
            }

            Session["ResultsCount"] = resultcount;

            if (dt != null)
            {
                if (refreshCombo == true)
                    SetFilterControls(dt);

                if (LoadGrid == true && dt.Rows.Count > 0)
                    LoadGridview(dt);
                else
                {
                    gvDocuments.DataSource = null;
                    gvDocuments.DataBind();
                    lblPageNumber.Text = "Pagina Nº 1 de 1 ";
                    lblTotal.Text = "No hay registros";
                    EnableSearchButtons(false, false);
                }

                //this.TabContainer1.ActiveTabIndex = 0;
            }
            else
                EnableSearchButtons(false, false);

            this.UpdGrid.Update();
        }
        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
            this.gvDocuments.DataSource = null;
            this.gvDocuments.DataBind();
        }
    }

    private List<ArrayList> GetIndexs(Boolean RefreshCombo)
    {
        try
        {
            List<ArrayList> genIndex = new List<ArrayList>();
            DataTable dt;
            DataTable dtByRightsOfSearch = null;

            Int32 UserId = Int32.Parse(Session["UserId"].ToString());

            if (Zamba.Core.UserBusiness.Rights.GetUserRights(Zamba.Core.UserBusiness.GetUserById(UserId), Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, Int32.Parse(cmbDocType.SelectedValue)))
                dt = Zamba.Services.Index.getIndexByDocTypeId(Int32.Parse(cmbDocType.SelectedValue), UserId, Zamba.Core.RightsType.IndexView);
            else
                dt = Zamba.Services.Index.getIndexByDocTypeId(Int32.Parse(cmbDocType.SelectedValue)).Tables[0];


            ArrayList arrLst;

            foreach (DataRow r in dt.Rows)
            {

                if (hshDataType.ContainsKey(Int64.Parse(r[0].ToString())) == false)
                {
                    hshDataType.Add(Int64.Parse(r[0].ToString()), Int64.Parse(r[2].ToString()));
                }

                arrLst = new ArrayList(3);
                arrLst.Add(r[0]);
                arrLst.Add(r[1]);
                arrLst.Add(hshDataType[Int64.Parse(r[0].ToString())]);
                genIndex.Add(arrLst);
            }


            if (RefreshCombo)
            {

                if (Zamba.Core.UserBusiness.Rights.GetUserRights(Zamba.Core.UserBusiness.GetUserById(UserId), Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, Int32.Parse(cmbDocType.SelectedValue)))
                    /*[sebastian] hay que poner algo que indique si tiene o no permiso, porque cuando 
                    no lo tiene no llena el combo de indices y te deja insertar un documento igual.
                     el problema es que esto genera exceptions y realizar una tarea al usuario que no deberia
                     poder hacer según entiendo*/
                    dtByRightsOfSearch = Zamba.Services.Index.getIndexByDocTypeId(Int32.Parse(cmbDocType.SelectedValue), UserId, Zamba.Core.RightsType.IndexSearch);


                if (Zamba.Core.UserBusiness.Rights.GetUserRights(Zamba.Core.UserBusiness.GetUserById(UserId), Zamba.Core.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, Int32.Parse(cmbDocType.SelectedValue)))
                {
                    cmbIndex.DataSource = dtByRightsOfSearch;
                    cmbFilter.DataSource = dtByRightsOfSearch;
                }
                else
                {
                    cmbIndex.DataSource = dt;
                    cmbFilter.DataSource = dt;

                }

                cmbFilter.DataTextField = "Index_Name";
                cmbFilter.DataValueField = "Index_Id";
                cmbIndex.DataTextField = "Index_Name";
                cmbIndex.DataValueField = "Index_Id";

                //cmbIndex.Items.Remove(new ListItem("Nombre del Documento"));
                cmbIndex.DataBind();

                cmbFilter.DataBind();
            }

            return genIndex;

        }
        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
            return null;
        }
    }

    private void LoadGridview(DataTable dt)
    {
        try
        {
            
            Int32 resultcount = Int32.Parse(Session["ResultsCount"].ToString());
            Int32 UserId = Int32.Parse(Session["UserId"].ToString());
            Int16 PageId = Int16.Parse(Session["PagingId"].ToString());
            Int16 PageSize = Int16.Parse(Session["PageSize"].ToString());
            //for (int i = 1; 1 < this.gvDocuments.Columns.Count; i++)
            //gvDocuments.Columns.RemoveAt(1);
            gvDocuments.Columns.Clear();
            String[] a = { "fullpath", "doc_type_id","doc_id" };

            //String[] b = { "doc_id" };
            //<asp:HyperLink ID="HyperLink1" runat="server" Text="Ver" Target="_blank" NavigateUrl='<%# Eval("Fullpath", "DocViewer.aspx?fullpath={0}") %>'>

            HyperLinkField colprint = new HyperLinkField();
            colprint.ShowHeader = true;
            colprint.HeaderText = "Imprimir";
            colprint.Target = "blank";
            colprint.Text = "Imprimir";
            colprint.DataTextFormatString = "{0} Details";
            colprint.DataNavigateUrlFields = a;
            colprint.DataNavigateUrlFormatString = "~/PrintDocument.aspx?fullpath={0}";
            this.gvDocuments.Columns.Add(colprint);

            HyperLinkField colmail = new HyperLinkField();
            colmail.ShowHeader = true;
            colmail.HeaderText = "Email";
            colmail.Target = "blank";
            colmail.Text = "Enviar";
            colmail.DataTextFormatString = "{0} Details";
            colmail.DataNavigateUrlFields = a;
            colmail.DataNavigateUrlFormatString = "~/SendMail.aspx?fullpath={0}";
            this.gvDocuments.Columns.Add(colmail);

            //if (string.Compare(cmbDocType.SelectedValue ,"2204")== 0)
            //{
            //    HyperLinkField H = new HyperLinkField();
            //    H.ShowHeader = true;
            //    H.HeaderText = "VER";
            //    H.Target = "blank";
            //    H.Text = "VER";
            //    //linkColumn.DataTextField = "ProductName";      
            //    H.DataTextFormatString = "{0} Details";
            //    H.DataNavigateUrlFields = b;
            //    //H.DataNavigateUrlFormatString = "DocViewer.aspx?fullpath={0}";
            //    H.DataNavigateUrlFormatString = "http://legolas/danoneweb/PagFactura.aspx?DocId={0}";
            //    this.gvDocuments.Columns.Add(H);
            //}
            //else
            //{
                HyperLinkField H = new HyperLinkField();
                H.ShowHeader = true;
                H.HeaderText = "VER";
                H.Target = "blank";
                H.Text = "VER";
                //linkColumn.DataTextField = "ProductName";      
                H.DataTextFormatString = "{0} Details";
                H.DataNavigateUrlFields = a;
                H.DataNavigateUrlFormatString = "DocViewer.aspx?fullpath={0}&doctype={1}&docid={2}";
                this.gvDocuments.Columns.Add(H);
            //}

            foreach (DataColumn c in dt.Columns)
            {
                BoundField f = new BoundField();
                f.DataField = c.Caption;
                f.ShowHeader = true;
                f.HeaderText = c.Caption;
                f.SortExpression = c.Caption + " ASC";

                if (null != hshDataType[Zamba.Services.Index.GetIndexidByName(c.ColumnName)])
                {
                    switch (hshDataType[Zamba.Services.Index.GetIndexidByName(c.ColumnName)].ToString())
                    {
                        case "4":
                            // Date
                            f.DataFormatString = "{0:dd-MM-yyyy}";
                            f.HtmlEncode = false;
                            break;
                        case "5":
                            // Datetime
                            f.DataFormatString = "{0:dd-MM-yyyy HH:mm}";
                            f.HtmlEncode = false;
                            break;
                    }

                }

                this.gvDocuments.Columns.Add(f);
            }
            this.gvDocuments.DataSource = dt;

            this.gvDocuments.DataBind();

            if (this.gvDocuments.Rows.Count > 0)
            {
                lblTotal.Text = "Cantidad de documentos encontrados: " + resultcount;
                lblPageNumber.Text = "Pagina Nº " + PageId + " de " +
                    ((resultcount / PageSize) + 1) + "  ";
                if (resultcount <= PageSize)
                {
                    EnableSearchButtons(false, false);
                }
                else
                {
                    EnableSearchButtons(true, true);
                }
            }
            else
            {
                lblTotal.Text = "No se han encontrado documentos";
                lblPageNumber.Text = string.Empty;
            }

            //for (int i = 0; i < this.gvDocuments.Rows.Count;i++)
            //    ((HyperLink)((DataControlFieldCell)this.gvDocuments.Rows[i].Cells[0])).NavigateUrl = "DocViewer.aspx?fullpath=" + this.gvDocuments.Rows[i].Cells[this.gvDocuments.Rows[i].Cells.Count - 1].Text;
            //object d = this.gvDocuments.Columns[8].ItemStyle;
            //this.gvDocuments.DataBind();
        }
        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        }
    }

    private void SetFilterControls(DataTable dt)
    {
        try
        {

            SortedList st = new SortedList();
            Boolean showName = ShowDocumentName();
            cmbFilter.DataSource = null;

            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName.ToLower() != "fullpath")
                {
                    if (showName == false)
                    {
                        if (dc.ColumnName.ToLower() != "nombre del documento")
                        {
                            if (dc.ColumnName.ToLower() != "doc_id")
                                st.Add(dc.ColumnName, dc.DataType.ToString());

                        }
                    }
                    else
                        if (dc.ColumnName.ToLower() != "doc_id")
                            st.Add(dc.ColumnName, dc.DataType.ToString());

                }
            }
            Hashtable hsh = new Hashtable();
            foreach (System.Collections.DictionaryEntry Indexname in st)
            {
                hsh.Add(Indexname.Key.ToString(), Zamba.Services.Index.GetIndexidByName(Indexname.Key.ToString()));
            }
            this.cmbFilter.DataSource = hsh;
            this.cmbFilter.DataTextField = "Key";
            this.cmbFilter.DataValueField = "Value";
            this.cmbFilter.DataBind();

            hsh.Remove("Nombre del Documento");
            cmbIndex.DataSource = hsh;
            this.cmbIndex.DataTextField = "Key";
            this.cmbIndex.DataValueField = "Value";
            this.cmbIndex.DataBind();


            Session["List"] = st;
            setFilters();
        }
        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        }
    }

    /// <summary>
    /// Habilita o deshabilita los botones de navegacion de la grilla cuando se pagina
    /// </summary>
    private void EnableSearchButtons(Boolean Back, Boolean Next)
    {
        btnLast.Enabled = Next;
        BtnNext.Enabled = Next;

        btnFirst.Enabled = Back;
        BtnBack.Enabled = Back;
    }

    /// <summary>
    /// Asigna la url al iframe (muestra el documento
    /// </summary>
    private void setURL()
    {
        //try
        //{
        //    if (gvDocuments.SelectedRow != null)
        //    {
        //        string Url = gvDocuments.SelectedRow.Cells[gvDocuments.SelectedRow.Cells.Count - 1].Text;

        //        System.IO.FileInfo fi;
        //        fi = new System.IO.FileInfo(Url);
        //        System.IO.FileInfo fa;
        //        fa = new System.IO.FileInfo(System.Web.HttpRuntime.AppDomainAppPath + "\\temp\\" + fi.Name);
        //        try
        //        {
        //            if (fa.Exists == true)
        //            {
        //                fa.Delete();
        //            }
        //            if (fa.Directory.Exists == false)
        //                fa.Directory.Create();
        //            System.IO.File.Copy(fi.FullName, fa.FullName);
        //        }
        //        catch (Exception ex)
        //        {
        //            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        //        }

        //               //this.frame1.Attributes["src"] = ".\\temp\\" + fi.Name;
        //        //this.TabContainer1.ActiveTab = this.TabContainer1.Tabs[1];
        //        //this.TabContainer1.ActiveTabIndex = 1;
        //                //this.UpdImg.Update();


        //    }
        //}
        //catch (Exception ex)
        //{
        //    writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        //}
    }


    /// <summary>
    /// Escribe un log con el error ocurrido en el servidor
    /// </summary>
    /// <param name="message"></param>
    private void writeLog(String message)
    {
        try
        {
            string path = System.Web.HttpRuntime.AppDomainAppPath + "Exceptions\\";

            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }

            path += "Exception ";
            path += System.DateTime.Now.ToString().Replace(".", "").Replace(":", "");
            path = path.Replace("/", "-");
            path += ".txt";

            System.IO.StreamWriter writer = new System.IO.StreamWriter(path);
            writer.Write(message);
            writer.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Muestra los filtros dependiendo del valor del indice
    /// </summary>
    private void setFilters()
    {
        try
        {
            //todo: exception aca Al hacer click sobre el catalogo
            String dt = cmbFilter.SelectedItem.ToString();
            Int32 Indexid = Int32.Parse(cmbFilter.SelectedValue.ToString());
            this.cmbOperadores.Items.Clear();
            cmbOperadoresDate.Visible = false;
            txtSecondFilter.Visible = false;
            if (Session["List"] != null)
            {
                SortedList st = (SortedList)Session["List"];
                if (st[dt] != null)
                {
                    switch (st[dt].ToString())
                    {
                        case "System.String":
                            this.cmbOperadores.Items.Add("Contiene");
                            this.cmbOperadores.Items.Add("Empieza");
                            this.cmbOperadores.Items.Add("Termina");
                            this.cmbOperadores.Items.Add("Igual");
                            this.cmbOperadores.Items.Add("Distinto");
                            this.cmbOperadores.Items.Add("Es Nulo");
                            break;
                        case "System.DateTime":
                            this.cmbOperadores.Items.Add("=");
                            this.cmbOperadores.Items.Add(">");
                            this.cmbOperadores.Items.Add(">=");
                            this.cmbOperadores.Items.Add("<");
                            this.cmbOperadores.Items.Add("=<");
                            this.cmbOperadores.Items.Add("<>");
                            this.cmbOperadores.Items.Add("Entre");
                            this.cmbOperadores.Items.Add("Es Nulo");
                            break;
                        default:
                            this.cmbOperadores.Items.Add("=");
                            this.cmbOperadores.Items.Add(">");
                            this.cmbOperadores.Items.Add(">=");
                            this.cmbOperadores.Items.Add("<");
                            this.cmbOperadores.Items.Add("<=");
                            this.cmbOperadores.Items.Add("<>");
                            this.cmbOperadores.Items.Add("Entre");
                            this.cmbOperadores.Items.Add("Es Nulo");
                            break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        }
    }
    #endregion



    protected void imbtnInsertar_Click(object sender, ImageClickEventArgs e)
    {
        
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("InsertarDoc.aspx");
    }
}
