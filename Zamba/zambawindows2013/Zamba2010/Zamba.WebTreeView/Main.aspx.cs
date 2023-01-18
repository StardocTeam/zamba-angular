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

public partial class Main : System.Web.UI.Page
{
    Hashtable hshDataType = new Hashtable();
    private const Int32 GV_DOCTYPES_VALUEINDEX = 1;

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
            //btnShowSustitutionList.Visible = false;
            if (IsPostBack == false)
            {

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
                }
                Int32 UserId = 0;
                if (Session["UserId"] != null)
                {
                    if (Int32.TryParse(Session["UserId"].ToString(), out UserId) == true)
                    {
                        ArrayList _arraylist = Zamba.Services.DocType.GetDocTypesIdsAndNamesbyRightView(UserId);
                        if (_arraylist != null)
                        {
                            //if (ds.Tables.Count > 0)
                            //{
                            //DataView dv = new DataView(ds.Tables[0].Copy());
                            //dv.Sort = "Doc_Type_name Asc";
                            //this.cmbDocType.DataSource = dv.ToTable();
                            this.cmbDocType.DataSource = _arraylist;
                            this.cmbDocType.DataBind();
                            getIndexs(false);
                            getDocs(true, false, true);
                            //}
                        }
                    }
                }
                else
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
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
        txtFilter.Text = string.Empty;
        lblTotal.Text = string.Empty;
        txtFilterDate.Text = string.Empty;
        this.gvDocuments.DataSource = null;
        this.gvDocuments.DataBind();
        getIndexs(false);
        getDocs(true, true, true);
        if (LoadDocumentsOnDTSelection() == true)
        {
            Session["ShowDocuments"] = "ALL";
            getDocs(true, false, false);
        }
    }

    /// <summary>
    /// Cambian los indices trae todos los valores que se encuentran en el mismo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmbIndex_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFilter.Text = string.Empty;
        txtFilterDate.Text = string.Empty;
        lblTotal.Text = string.Empty;
        this.gvDocuments.DataSource = null;
        this.gvDocuments.DataBind();
        //getIndexValues();
        getIndexs(true);
    }

    /// <summary>
    /// Trae los documentos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDocTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["ShowDocuments"] = "CATALOGED";
        getIndexs(true);
        getDocs(true, true, false);
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
            if (ShowDocumentName() == false)
                e.Row.Cells[1].Visible = false;
        }

        System.Data.DataRowView drv;
        drv = (System.Data.DataRowView)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //if (drv != null)
            //{
            //    String catName = drv[1].ToString();
                
            //    int catNameLen = catName.Length;
            //    if (catNameLen > widestData)
            //    {
            //        widestData = catNameLen;
                    //gvDocuments.Columns[2].ItemStyle.Width =
                    //  widestData * 30;
            gvDocuments.HeaderStyle.Wrap = false;
            Int32 count = 0;
            Int32 colWrap = Int32.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["AjustarColumna"]);
            foreach (DataControlField df in gvDocuments.Columns)
            {
                if (count != colWrap)
                {
                    df.ItemStyle.Wrap = false;
                }
                count++;
            }
                    
                //}

            //}
        }
    }
    //protected int widestData;

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
        FilterDocs();
    }

    /// <summary>
    /// Muestra un popup con una lista de sustitucion
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShowSustitutionList_Click(object sender, EventArgs e)
    {
        StudentDialog.Show();
        DataTable dt = Zamba.Services.Index.GetIndexSubstitutionTable(Int64.Parse(cmbIndex.SelectedValue.ToString()));
        if (dt != null)
        {
            dgSustitutionList.DataSource = dt;
            dgSustitutionList.DataBind();
        }

    }


    private void FilterDocs()
    {
        try
        {
            Session["ShowDocuments"] = "FILTERED";
            //getIndexs();
            if (cmbFilter.SelectedIndex >= 0 && String.Compare(txtFilter.Text, String.Empty) != 0)
            {
                if (Session["Datos"] != null)
                {
                    Boolean data = Boolean.Parse(Session["Datos"].ToString());
                    getIndexs(false);
                    getDocs(false, data, false);

                    DataView dv = new DataView((DataTable)gvDocuments.DataSource);
                    //Si es fecha filtro con desde y hasta
                    String sr = cmbFilter.SelectedValue;

                    if (Session["List"] != null)
                    {
                        SortedList st = (SortedList)Session["List"];
                        if (Zamba.Core.DocTypesBusiness.GetIndexIdByName(sr) > 0)
                        {
                            if (hshDataType[(Int64)Zamba.Core.DocTypesBusiness.GetIndexIdByName(sr)].ToString() == "4" || hshDataType[(Int64)Zamba.Core.DocTypesBusiness.GetIndexIdByName(sr)].ToString() == "5")
                            {
                                DateTime dt1;
                                DateTime dt2;
                                if (DateTime.TryParse(txtFilter.Text, out dt1) == true)
                                {
                                    if (cmbOperadoresDate.Visible == false)
                                        dv.RowFilter = ("convert([" + cmbFilter.SelectedItem.Text + "],System.DateTime) " + cmbOperadores.SelectedValue + "'" + dt1.ToShortDateString() + "'");
                                    else
                                        if (DateTime.TryParse(txtFilterDate.Text, out dt2) == true)
                                        {
                                            dv.RowFilter = ("convert([" + cmbFilter.SelectedItem.Text + "],System.DateTime) >= '" + dt1.ToShortDateString() + "' And " + "convert([" + cmbFilter.SelectedItem.Text + "],System.DateTime) <= '" + dt2.ToShortDateString() + "'");
                                        }
                                }
                            }
                            else if (hshDataType[(Int64)Zamba.Core.DocTypesBusiness.GetIndexIdByName(sr)].ToString() == "8" || hshDataType[(Int64)Zamba.Core.DocTypesBusiness.GetIndexIdByName(sr)].ToString() == "7")
                            {
                                //Le coloco los caracteres comodin a la consulta
                                if (String.Compare(cmbOperadores.SelectedValue, "Termina") == 0)
                                {
                                    dv.RowFilter = ("[" + cmbFilter.SelectedItem.Text + "] Like '%" + txtFilter.Text + "'");
                                }
                                //Sino filtro de manera normal
                                else if (String.Compare(cmbOperadores.SelectedValue, "Empieza") == 0)
                                {
                                    dv.RowFilter = ("[" + cmbFilter.SelectedItem.Text + "] Like '" + txtFilter.Text + "%'");
                                }
                                else if (String.Compare(cmbOperadores.SelectedValue, "Contiene") == 0)
                                {
                                    dv.RowFilter = ("[" + cmbFilter.SelectedItem.Text + "] Like '%" + txtFilter.Text + "%'");
                                }
                                else if (String.Compare(cmbOperadores.SelectedValue, "Igual") == 0)
                                {
                                    dv.RowFilter = ("[" + cmbFilter.SelectedItem.Text + "] = '" + txtFilter.Text + "'");
                                }
                            }
                            else
                                dv.RowFilter = ("convert([" + cmbFilter.SelectedItem.Text + "],System.Double) " + cmbOperadores.SelectedValue + " " + txtFilter.Text);
                        }

                        this.gvDocuments.DataSource = dv.ToTable();
                        this.gvDocuments.DataBind();

                        if (this.gvDocuments.Rows.Count > 0)
                        {
                            lblTotal.Text = "Cantidad de documentos encontrados: " + this.gvDocuments.Rows.Count;
                        }
                        else
                            lblTotal.Text = "No se han encontrado documentos";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        }

    }

    /// <summary>
    /// Combo de filtros
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbOperadoresDate.Visible = false;
        txtFilterDate.Visible = false;
        txtFilter.Text = string.Empty;
        txtFilterDate.Text = string.Empty;
        String indexname = cmbFilter.SelectedValue;
        Int16 indextypeid = Zamba.Services.Index.GetIndexTypeByName(indexname);
        //0 = normal, 1= busqueda 2 = sustitucion
        switch (indextypeid)
        {
            case 0:
                //btnShowSustitutionList.Visible = false;
                break;

            case 1:
                //btnShowSustitutionList.Visible = false;
                break;
            case 2:
                //btnShowSustitutionList.Visible = true;
                break;
        }
        setFilters();
    }



    protected void btnSeeDocs_Click(object sender, EventArgs e)
    {
        Session["ShowDocuments"] = "ALL";
        txtFilter.Text = string.Empty;
        txtFilterDate.Text = string.Empty;
        this.gvDocuments.DataSource = null;
        this.gvDocuments.DataBind();
        getIndexs(false);
        getDocs(true, false, false);
    }


    /// <summary>
    /// Sortea las columnas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void sort(object sender, GridViewSortEventArgs e)
    {
        try
        {
            switch (Session["ShowDocuments"].ToString())
            {
                case "FILTERED":
                    getIndexs(true);
                    FilterDocs();
                    break;
                case "ALL":
                    getIndexs(true);
                    getDocs(false, false, false);
                    break;
                case "CATALOGED":
                    getIndexs(true);
                    getDocs(true, true, false);
                    break;
            }
            DataTable dt;
            if (gvDocuments.DataSource != null)
            {
                dt = (DataTable)gvDocuments.DataSource;
                if (Session["SortOrder"] == null)
                    Session["SortOrder"] = "DESC";
                else
                {
                    if (Session["SortOrder"].ToString() == "DESC")
                        Session["SortOrder"] = "ASC";
                    else
                        Session["SortOrder"] = "DESC";

                }
                DataView dv = new DataView(dt);

                Char[] mander = { Char.Parse(" ") };
                string _Sort = string.Empty;
                for (int i = 0; i < e.SortExpression.Split(mander).Length; i++)
                {
                    if (e.SortExpression.Split(mander)[i] != "ASC" && e.SortExpression.Split(mander)[i] != "DESC")
                        _Sort += e.SortExpression.Split(mander)[i] + " ";

                }
                if (string.Compare(_Sort, string.Empty) == 1)
                {
                    dv.Sort = _Sort + Session["SortOrder"];
                }
                gvDocuments.DataSource = dv;
                gvDocuments.DataBind();
            }
        }
        catch (Exception ex)
        {
            writeLog("Error:" + ex.Message + " Trace:" + ex.StackTrace);
        }
    }

    /// <summary>
    /// Metodo que hace sort de los valores de la grilla
    /// </summary>
    /// <param name="useSort">If useSort = true pone el sorteo inverso,
    /// sino hace el mismo que estaba antes</param>
    private void sort(Boolean useSort, String SortExpression)
    {
        try
        {
            if (this.gvDocuments.DataSource != null)
            {
                DataTable table = (DataTable)this.gvDocuments.DataSource;

                DataView view = new DataView(table);
                if (useSort == true)
                {
                    if (Session["Sorted"] != null)
                    {
                        if (Session["Sorted"].ToString() != SortExpression)
                        {
                            view.Sort = SortExpression + " " + "Asc";
                            Session.Add("Sorted", SortExpression);
                        }
                        else
                        {
                            view.Sort = SortExpression + " " + "Desc";
                            Session.Remove("Sorted");
                        }
                    }
                    else
                    {
                        view.Sort = SortExpression + " " + "Asc";
                        Session.Add("Sorted", SortExpression);
                    }
                }
                else
                    if (Session["Sorted"] != null)
                    {
                        view.Sort = Session["Sorted"].ToString() + " " + "Asc";
                    }
                this.gvDocuments.DataSource = view.ToTable();
                this.gvDocuments.DataBind();
            }
        }
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
    #endregion

    #region Metodos

    static Boolean ShowCatalog()
    {
        return Boolean.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowCatalog"]);
    }

    static Boolean ShowDocumentName()
    {
        return Boolean.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowDocumentName"]);
    }

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
    private static DataView SortDataSet(DataSet ds, String column, String sorttype)
    {
        DataView resultView = ds.Tables[0].DefaultView;
        StringBuilder sortText = new StringBuilder();
        sortText.Append(column);
        sortText.Append(" ");
        sortText.Append(sorttype.ToUpper());
        resultView.Sort = sortText.ToString();
        return resultView;
    }


    /// <summary>
    /// Trae los indices
    /// </summary>
    private void getIndexs(Boolean dontrefreshcombo)
    {
        try
        {
            if (this.cmbDocType.SelectedIndex >= 0)
            {
                Int32 docTypeId = 0;
                if (Int32.TryParse(this.cmbDocType.SelectedValue, out docTypeId) == true)
                {
                    DataSet ds = Zamba.Services.Index.getIndexByDocTypeId(docTypeId);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            DataView dv2 = SortDataSet(ds, "Orden", "ASC");
                            foreach (DataRow r in dv2.Table.Rows)
                            {
                                if (hshDataType.ContainsKey(Int64.Parse(r[0].ToString())) == false)
                                {
                                    hshDataType.Add(Int64.Parse(r[0].ToString()), Int64.Parse(r[2].ToString()));
                                }
                            }
                            /* DataView dv = new DataView(ds.Tables[0].Copy());
                             dv.Sort = "Index_name Asc";*/
                            if (dontrefreshcombo == false)
                            {
                                this.cmbIndex.DataSource = dv2.ToTable();
                                this.cmbIndex.DataBind();
                            }
                            if (ShowCatalog())
                                getIndexValues();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
        }
    }

    private void getIndexValues()
    {
        try
        {
            gvDocTypes.DataSource = null;
            if (this.cmbIndex.SelectedIndex >= 0 && this.cmbDocType.SelectedIndex >= 0)
            {
                Int32 docTypeId = 0;
                if (Int32.TryParse(this.cmbDocType.SelectedValue, out docTypeId) == true)
                {
                    Int32 indexId = 0;
                    if (Int32.TryParse(this.cmbIndex.SelectedValue, out indexId) == true)
                    {
                        Int32 UserId = 0;
                        if (Session["UserId"] != null)
                        {
                            if (Int32.TryParse(Session["UserId"].ToString(), out UserId) == true)
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
    private void getDocs(Boolean refreshCombo, Boolean useFilter, Boolean DontLoadGrid)
    {
        try
        {
            DataSet ds = null;
            List<ArrayList> genIndex = new List<ArrayList>();

            //Agrego los numeros y nombre de los indices
            foreach (ListItem item in this.cmbIndex.Items)
            {
                ArrayList arrLst = new ArrayList(3);
                arrLst.Add(item.Value.Trim());
                arrLst.Add(item.Text.Trim());
                if (hshDataType.Contains(Int64.Parse(item.Value.Trim())) == true)
                {
                    arrLst.Add(hshDataType[Int64.Parse(item.Value.Trim())].ToString());
                }
                genIndex.Add(arrLst);
            }

            Int32 docTypeId = 0;
            if (Int32.TryParse(this.cmbDocType.SelectedValue, out docTypeId) == true)
            {
                Int32 indexId = 0;
                if (Int32.TryParse(this.cmbIndex.SelectedValue, out indexId) == true)
                {
                    Int32 UserId = 0;
                    if (Session["UserId"] != null)
                    {
                        if (Int32.TryParse(Session["UserId"].ToString(), out UserId) == true)
                        {
                            if (useFilter == true)
                            {
                                if (this.gvDocTypes.SelectedValue != null)
                                {
                                    Session["Datos"] = true;
                                    ds = Zamba.Services.Result.getResultsData(docTypeId, indexId, this.gvDocTypes.SelectedValue.ToString(), genIndex, useFilter, UserId);
                                }
                                else
                                {
                                    Session["Datos"] = true;
                                    ds = Zamba.Services.Result.getResultsData(docTypeId, indexId, cmbDocType.SelectedValue.ToString(), genIndex, false, UserId);
                                }
                            }
                            else
                            {
                                Session["Datos"] = false;
                                ds = Zamba.Services.Result.getResultsData(docTypeId, indexId, String.Empty, genIndex, useFilter, UserId);
                            }
                        }
                    }
                }
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                if (refreshCombo == true)
                {
                    SortedList st = new SortedList();
                    Boolean showName = ShowDocumentName();
                    foreach (DataColumn dc in ds.Tables[0].Columns)
                    {
                        if (dc.ColumnName.ToLower() != "fullpath")
                        {
                            if (showName == false)
                            {
                                if (dc.ColumnName.ToLower() != "nombre del documento")
                                    st.Add(dc.ColumnName, dc.DataType.ToString());
                            }
                            else
                                st.Add(dc.ColumnName, dc.DataType.ToString());
                        }
                    }

                    this.cmbFilter.DataSource = st;
                    this.cmbFilter.DataTextField = "Key";
                    this.cmbFilter.DataBind();
                    Session["List"] = st;
                    setFilters();
                }
                if (DontLoadGrid == false)
                {
                    //for (int i = 1; 1 < this.gvDocuments.Columns.Count; i++)
                    //gvDocuments.Columns.RemoveAt(1);
                    gvDocuments.Columns.Clear();

                    //<asp:HyperLink ID="HyperLink1" runat="server" Text="Ver" Target="_blank" NavigateUrl='<%# Eval("Fullpath", "DocViewer.aspx?fullpath={0}") %>'>

                    HyperLinkField H = new HyperLinkField();
                    H.ShowHeader = true;
                    H.HeaderText = "VER";
                    H.Target = "blank";
                    H.Text = "VER";
                    //linkColumn.DataTextField = "ProductName";
                    H.DataTextFormatString = "{0} Details";
                    String[] a = { "fullpath" };
                    H.DataNavigateUrlFields = a;
                    H.DataNavigateUrlFormatString = "DocViewer.aspx?fullpath={0}";
                    this.gvDocuments.Columns.Add(H);

                    foreach (DataColumn c in ds.Tables[0].Columns)
                    {
                        BoundField f = new BoundField();
                        f.DataField = c.Caption;
                        f.ShowHeader = true;
                        f.HeaderText = c.Caption;
                        f.SortExpression = c.Caption + " ASC";

                        if (c.DataType.Name == "DateTime")
                        {
                            f.DataFormatString = "{0:dd-MM-yyyy}";
                            f.HtmlEncode = false;
                        }

                        this.gvDocuments.Columns.Add(f);

                    }

                    this.gvDocuments.DataSource = ds.Tables[0];
                    this.gvDocuments.DataBind();

                    if (this.gvDocuments.Rows.Count > 0)
                    {
                        lblTotal.Text = "Cantidad de documentos encontrados: " + this.gvDocuments.Rows.Count;
                    }
                    else
                        lblTotal.Text = "No se han encontrado documentos";
                    //for (int i = 0; i < this.gvDocuments.Rows.Count;i++)
                    //    ((HyperLink)((DataControlFieldCell)this.gvDocuments.Rows[i].Cells[0])).NavigateUrl = "DocViewer.aspx?fullpath=" + this.gvDocuments.Rows[i].Cells[this.gvDocuments.Rows[i].Cells.Count - 1].Text;
                    //object d = this.gvDocuments.Columns[8].ItemStyle;
                    //this.gvDocuments.DataBind();
                }
                //this.TabContainer1.ActiveTabIndex = 0;
            }
            this.UpdGrid.Update();
        }
        catch (Exception ex)
        {
            writeLog("Error: " + ex.Message + " Trace: " + ex.StackTrace);
            this.gvDocuments.DataSource = null;
            this.gvDocuments.DataBind();
        }
    }

    /// <summary>
    /// Trae los documentos en un dataset y setea las columnas de la grilla de documentos
    /// Lo uso para ordenar el dataset y mostrarlo
    /// </summary>
    /// <param name="refreshCombo">True si hay que llenar el combo de columnas</param>

    protected void cmbOperadores_SelectedIndexChanged(object sender, EventArgs e)
    {
        getIndexs(false);
        String dt = cmbFilter.SelectedValue;
        if (Session["List"] != null)
        {
            SortedList st = (SortedList)Session["List"];
            if (Zamba.Core.DocTypesBusiness.GetIndexIdByName(dt) > 0)
            {
                if ((cmbOperadores.SelectedIndex == 0 && hshDataType[(Int64)Zamba.Core.DocTypesBusiness.GetIndexIdByName(dt)].ToString() == "4") || (cmbOperadores.SelectedIndex == 0 && hshDataType[(Int64)Zamba.Core.DocTypesBusiness.GetIndexIdByName(dt)].ToString() == "5"))
                {
                    cmbOperadoresDate.Visible = false;
                    txtFilterDate.Visible = false;
                }
                else
                {
                    if ((hshDataType[(Int64)Zamba.Core.DocTypesBusiness.GetIndexIdByName(dt)].ToString() == "4" || hshDataType[(Int64)Zamba.Core.DocTypesBusiness.GetIndexIdByName(dt)].ToString() == "5") && cmbOperadores.SelectedValue == "Entre")
                    {
                        cmbOperadoresDate.Visible = true;
                        txtFilterDate.Visible = true;
                    }
                    else
                    {
                        cmbOperadoresDate.Visible = false;
                        txtFilterDate.Visible = false;
                    }
                }
            }
        }

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
        String dt = cmbFilter.SelectedValue;
        this.cmbOperadores.Items.Clear();
        cmbOperadoresDate.Visible = false;
        txtFilterDate.Visible = false;
        if (Session["List"] != null)
        {
            SortedList st = (SortedList)Session["List"];
            if (st[dt] != null)
            {
                if (st[dt].ToString() == "System.String")
                {
                    getIndexs(true);
                    if (Zamba.Core.DocTypesBusiness.GetIndexIdByName(dt) > 0)
                    {

                        if (hshDataType[(Int64)Zamba.Core.DocTypesBusiness.GetIndexIdByName(dt)].ToString() == "8" || hshDataType[(Int64)Zamba.Core.DocTypesBusiness.GetIndexIdByName(dt)].ToString() == "7")
                        {
                            this.cmbOperadores.Items.Add("Contiene");
                            this.cmbOperadores.Items.Add("Empieza");
                            this.cmbOperadores.Items.Add("Termina");
                            this.cmbOperadores.Items.Add("Igual");
                            return;
                        }
                    }
                }

                this.cmbOperadores.Items.Add("=");
                this.cmbOperadores.Items.Add(">");
                this.cmbOperadores.Items.Add(">=");
                this.cmbOperadores.Items.Add("<");
                this.cmbOperadores.Items.Add("<=");
                this.cmbOperadores.Items.Add("Entre");
            }
        }
    }
    #endregion
}
