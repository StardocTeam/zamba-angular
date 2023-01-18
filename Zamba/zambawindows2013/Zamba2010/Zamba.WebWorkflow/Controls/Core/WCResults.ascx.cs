using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Zamba.Core;
using System.Collections.Generic;

public partial class Controls_Core_WCResults : System.Web.UI.UserControl
{
    //Declaro el delegado que va a manejar el agregar de los steps en el arbol
    public delegate void SelectedResult(Int64? docId);
    public delegate void ReloadValues();
    public Zamba.AppBlock.ZIconsList IL;
    public event ReloadValues OnReloadValues;

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
    private static DataTable CurrentDataTable = null;
    //Declaro una variable del delegado
    private SelectedResult dSelectResult = null;
    #endregion

    protected void gvDocuments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
                        
            gvDocuments.PageIndex = e.NewPageIndex;
            OnReloadValues();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (gvDocuments.SelectedIndex == -1 && gvDocuments.Rows.Count > 0)
                gvDocuments.SelectedIndex = 0;

            gvDocuments_SelectedIndexChanged(null, null);
        }
    }

    public void ShowResults(DataTable DTResults)
    {
        Session["PageSize"] = GetPageSize();
        Session["PagingId"] = 1;
        Session["ResultsCount"] = 0;
        Session["ShowDocuments"] = ResultsLoadType.ALL;
        Session["SortSTR"] = string.Empty;
    // -------------------------------------------[sebastian 09/01/2009]----------------------------------
        //le paso el valor de orden jarcodeado para que siempre la primera vez ordene por nombre de doc.
        LoadGridview(DTResults,"Nombre Documento");
        CurrentDataTable = DTResults;//guardo el dt para luego usarlo para ordenar la grilla.
        //cargo el combo que tiene los valores por el cual se puede ordenar
        foreach (DataColumn  CurrentColumnName in DTResults.Columns )
        {
            //no cargo los nombres de las columnas que se ocultan en la grilla.
            if(string.Compare(CurrentColumnName.ColumnName.ToLower(),"fullpath")!=0 && string.Compare(CurrentColumnName.ColumnName.ToLower (),"docid")!=0)
                
                dplstColumName.Items.Add(CurrentColumnName.ColumnName);
        }
     // -----------------------------------------------------------------------------  
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="SortBy"></param>
    /// <history>[Ezequiel] 28/01/09 Modified - Se elimino la opcion de imprimir y enviar de la grilla, ya que se agregaron esas funcionalidades en la toolbar. </history>
    public void LoadGridview(DataTable dt, string SortBy)
    {
        try
        {
            Int32 resultcount = Int32.Parse(Session["ResultsCount"].ToString());
            Int32 UserId = Int32.Parse(Session["UserId"].ToString());
            Int16 PageId = Int16.Parse(Session["PagingId"].ToString());
            Int16 PageSize = Int16.Parse(Session["PageSize"].ToString());
            //DataTable dtByRightsOfSearch = null;
            gvDocuments.Columns.Clear();


            CommandField s = new CommandField();
            s.ShowSelectButton = true;
            s.SelectText = "Info";
            this.gvDocuments.Columns.Add(s);

            HyperLinkField H = new HyperLinkField();
            H.ShowHeader = true;
            H.HeaderText = "VER";
            H.Target = "blank";
            H.Text = "VER";
            H.DataTextFormatString = "{0} Details";


            //String[] d = { "fullpath" };

            String[] d = { "fullpath","docid" };      




            H.DataNavigateUrlFields = d;
            //Se cambio la pagina para poder visualizar tambien formularios electronicos. [sebastian 08/01/2009]
            //H.DataNavigateUrlFormatString = "~/WebClient/Results/DocViewer.aspx?fullpath={0}&docid={1}";
            
            
            H.DataNavigateUrlFormatString = "~/WebBrowser/WebBrowser.aspx?fullpath={0}&docid={1}";            

            //H.DataNavigateUrlFormatString = "~/WebBrowser/WebBrowser.aspx?fullpath={0}";

            this.gvDocuments.Columns.Add(H);

            //se agrego la columna para ver el historial del documento.[sebastian 08/01/2009]
            HyperLinkField I = new HyperLinkField();
            I.ShowHeader = true;
            I.HeaderText = "Historial";
            I.Target = "blank";
            I.Text = "Historial";
            I.DataTextFormatString = "{0} Details";
            String[] e = { "docid" };
            I.DataNavigateUrlFields = e;
            I.DataNavigateUrlFormatString = "~/WebClient/DocHistory.aspx?docid={0}";
            this.gvDocuments.Columns.Add(I);

            //---------Emiliano---- Se agrego esta columna para poder insertar los iconos de los documentos asociados
            ImageField Imagen_doc = new ImageField ();
            Imagen_doc.ShowHeader = true;
            Imagen_doc.HeaderText = "Icono";
            Imagen_doc.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            Imagen_doc.ItemStyle.VerticalAlign = VerticalAlign.Middle;
            Imagen_doc.DataImageUrlField = "Icono";
            Imagen_doc.DataImageUrlFormatString ="../../icono.aspx?id={0}";                  
            this.gvDocuments.Columns.Add(Imagen_doc) ;

          
            //-------------------------------------------------------------------
            try
            {
                foreach (DataColumn c in dt.Columns)
                {
                    BoundField f = new BoundField();
                    f.DataField = c.Caption;
                    f.ShowHeader = true;
                    f.HeaderText = c.Caption;
                    f.SortExpression = c.Caption + " ASC";
                    if (c.Caption == "Icono")
                    {
                        f.Visible = false;
                    }
                    this.gvDocuments.Columns.Add(f);
                    this.cmbFilter.Items.Add(c.Caption);

                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            //ExtGrid.DataSource = dt;
            //ExtGrid.DataBind();

            //foreach (DataColumn col in ((DataTable)ExtGrid.DataSource).Columns)
            //    col.DataType = typeof(String);
            //SE AGREGO ESTO PARA MOSTRAR LA GRILLA ORDENADA DE ACUERDO AL VALOR DE SORTBY[SEBASTIAN 07-01-2009]
            DataView dv = new DataView(dt, "", SortBy , DataViewRowState.CurrentRows);
            this.gvDocuments.DataSource = dv;
            this.gvDocuments.DataBind();
            
            if (this.gvDocuments.Rows.Count > 0)
            {
                lblTotal.Text = "Cantidad de documentos encontrados: " + dt.Rows.Count.ToString();
                lblPageNumber.Text = "Pagina Nº " + PageId + " de " +
                    ((resultcount / PageSize) + 1) + "  ";

                if (resultcount < 30)
                {
                    EnableSearchButtons(false, false);
                }
                else
                {
                    EnableSearchButtons(true, true);
                }
                setFilters();
            }
            else
            {
                lblTotal.Text = "No se han encontrado documentos";
                lblPageNumber.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
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
    /// Combo de filtros
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        cmbOperadoresDate.Visible = false;
        txtSecondFilter.Visible = false;
        txtFirstFilter.Text = string.Empty;
        txtSecondFilter.Text = string.Empty;
        //GetIndexs(false);
        SetIndexType();
        setFilters();
    }

    /// <summary>
    /// Trae los documentos en un dataset y setea las columnas de la grilla de documentos
    /// Lo uso para ordenar el dataset y mostrarlo
    /// </summary>
    /// <param name="refreshCombo">True si hay que llenar el combo de columnas</param>
    protected void cmbOperadores_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ChkAgroup_OnCheckedChanged(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Muestra un popup con una lista de sustitucion
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShowList_Click(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Activa el filtro
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Cambian los indices trae todos los valores que se encuentran en el mismo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmbIndex_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Evento Click del boton de navegacion Volver
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnBack_OnClick(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Evento Click del boton de navegacion siguiente
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnNext_OnClick(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Evento Click del boton de navegacion primero
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFirst_OnClick(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Evento Click del boton de navegacion ultimo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLast_OnClick(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Trae el documento
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <history>
    ///     [Gaston]  17/02/2009  Modified  Validación Session["NewSubject"]
    /// </history>
    protected void gvDocuments_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Si NewSubject es igual a null o es igual a cero entonces significa que el usuario no presiono el botón "Nuevo Tema" y por lo tanto se cambia
        // el id del documento. Esta condición se coloco porque si se presiona el botón "Nuevo Tema" el id del documento actual cambia (ya que se ejecuta
        // este evento) y por lo tanto, el nuevo tema sería no para el documento actual sino para el primero de la grilla (lo mismo para el adjunto)
        // Esta condición evita esto, ya que cuando se presiona el botón "Nuevo Tema" NewSubject se coloca en uno. Para volver a cero se debe presionar 
        // el botón "Cancelar" de la vista 2 que vuelve a la grilla de results
        if (Session["NewSubject"] == null || Session["NewSubject"].ToString() == "0")
        {
            try
            {
                Int64 SelectedDocId = 0;
                Int32 DocIdIndex = 0;

                for (int i = 0; i < gvDocuments.Columns.Count; i++)
                {
                    if (String.Compare(gvDocuments.Columns[i].HeaderText, "docId", true) == 0)
                    {
                        DocIdIndex = i;
                        break;
                    }
                }

                //todo obtener el docid seleccionado
                GridViewRow row = gvDocuments.SelectedRow;
                if (row == null || !Int64.TryParse(row.Cells[DocIdIndex].Text, out SelectedDocId))
                    dSelectResult(null);
                else
                {
                    dSelectResult((Int64?)SelectedDocId);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }

    protected void gvDocuments_RowCreated(object sender, GridViewRowEventArgs e)
    {
    }

    /// <summary>
    /// Oculta el fullpath
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDocuments_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.Cells.Count > 0)
            {

                // Andres - Esto hacia que tire errores por todos lados , se comento y no cambio nada en la grilla.
                //e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
                //e.Row.Cells[e.Row.Cells.Count - 2].Visible = false;

                if (!ShowDocumentName())
                    e.Row.Cells[1].Visible = false;

            }

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label titleLabel = (Label)e.Row.FindControl("lblTitle");
            //    string strval = ((Label)(titleLabel)).Text;
            //    string title = (string)ViewState["name"];
            //    if (title == strval)
            //    {
            //        titleLabel.Visible = false;
            //        titleLabel.Text = string.Empty;
            //    }
            //    else
            //    {
            //        title = strval;
            //        ViewState["name"] = title;
            //        titleLabel.Visible = true;
            //        titleLabel.Text = "<br><b>" + title + "</b><br>";
            //    }
            //}
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// Sortea las columnas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OnSorting(object sender, GridViewSortEventArgs e)
    {
        //e.SortExpression = "iva";
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
    /// Muestra los filtros dependiendo del valor del indice
    /// </summary>
    private void setFilters()
    {
        try
        {
            Int64 Indexid = Zamba.Services.Index.GetIndexidByName(cmbFilter.SelectedValue.ToString());
            this.cmbOperadores.Items.Clear();
            cmbOperadoresDate.Visible = false;
            txtSecondFilter.Visible = false;
            if (Session["List"] != null)
            {
                SortedList st = (SortedList)Session["List"];
                if (st[Indexid] != null)
                {
                    switch (st[Indexid].ToString())
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
            else
            {
                this.cmbOperadores.Items.Add("=");
                this.cmbOperadores.Items.Add(">");
                this.cmbOperadores.Items.Add(">=");
                this.cmbOperadores.Items.Add("<");
                this.cmbOperadores.Items.Add("<=");
                this.cmbOperadores.Items.Add("<>");
                this.cmbOperadores.Items.Add("Entre");
                this.cmbOperadores.Items.Add("Es Nulo");
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    private void SetIndexType()
    {
        Int64 indexId = Zamba.Services.Index.GetIndexidByName(cmbFilter.SelectedValue.ToString());
        if (indexId > 0)
        {
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
        else
        {
            btnShowList.Visible = false;
            txtFirstFilter.Visible = true;
        }
    }

    /// <summary>
    /// Obtiene del webconfig si se va a mostrar el nombre del result o no
    /// </summary>
    static Boolean ShowDocumentName()
    {
        return Boolean.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ShowDocumentName"]);
    }

    public event SelectedResult OnSelectResult
    {
        add
        {
            this.dSelectResult += value;
        }
        remove
        {
            this.dSelectResult -= value;

        }
    }

    /// <summary>
    /// Oculta la columna especificada en la grilla 
    /// </summary>
    /// <param name="columnName"></param>
    public void HideColumn(String columnName)
    {
        foreach (DataControlField CurrentColumn in gvDocuments.Columns)
        {
            if (String.Compare(CurrentColumn.HeaderText, columnName) == 0)
            {
                CurrentColumn.Visible = false;
                break;
            }
        }
    }

    /// <summary>
    /// Oculta el listado de columnas especificado en la grilla 
    /// </summary>
    /// <param name="columnNames"></param>
    public void HideColumns(List<String> columnNames)
    {
        foreach (DataControlField CurrentColumn in gvDocuments.Columns)
        {
            if (columnNames.Contains(CurrentColumn.HeaderText))
            {
                CurrentColumn.Visible = false;
                columnNames.Remove(CurrentColumn.HeaderText);
            }

            if (columnNames.Count == 0)
                break;
        }
    }
    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    LoadGridview(CurrentDataTable, DropDownList1.SelectedValue);
    //}
    protected void dplstSortDocs_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// Al hacer click sobre el boton se aplica el criterio de ordenamiento de la grilla.
    /// [Sebastián 09/01/2009]
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAplicarOrden_Click(object sender, EventArgs e)
    {
        LoadGridview(CurrentDataTable, dplstColumName.SelectedValue);
        //oculte las columnas que no se muestran en ningun momento.
        HideColumn("fullpath");
        HideColumn("docid");
    }
}