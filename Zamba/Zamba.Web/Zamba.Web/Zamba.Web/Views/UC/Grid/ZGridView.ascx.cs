using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;
using System.Diagnostics;
using System.Data;


public partial class ZGridView : System.Web.UI.UserControl
{
    int _pageButtonCount = 4;
    const int imageColumnCount = 1;//Se agrego columna situacion al inicio de grilla como imagen y omitio dato de DT, al trabajar con grilla debe saltearse imageColumnCount respecto al dataTable
    #region Properties
    private Int16 itemsPerPage = Int16.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["PageSize"]);
    public int? PageCount { get; set; }
    public Int64 StepCount { get; set; }
    public int PagingButtonCount
    {
        get
        {
            return _pageButtonCount + 1;
        }
        set
        {
            _pageButtonCount = value - 1;
        }
    }
    public int PageIndex { get; set; } = 1;
    public object DataSource { get; set; }
    public GridView CurrentGrid
    {
        get
        {
            return grvGrid;
        }
    }
    #endregion

    public ZGridView()
    {

    }

    public delegate void NeedDataSource(object sender, EventArgs e);
    public event NeedDataSource OnNeedDataSource;

    public void BindGrid()
    {
        if (DataSource != null)
        {

            grvGrid.DataSource = DataSource;
            grvGrid.DataBind();
        }
    }

    private void BindPager()
    {
        PageIndex = hiddenCurrentPage.Value != string.Empty ? int.Parse(hiddenCurrentPage.Value) : 1;

        int pageCount = PageCount.Value;
        long totItems = StepCount;
        if (totItems != 0)
        {
            pagerZmb.Visible = true;

            FromTab.Text = PageIndex == 1 ? "1" : (1 + (PageIndex - 1) * itemsPerPage).ToString();

            if (PageIndex == 1 && totItems >= itemsPerPage)
                ToTab.Text = itemsPerPage.ToString();
            else if (PageIndex == 1 && totItems < itemsPerPage)
                ToTab.Text = totItems.ToString();
            else
                ToTab.Text = (PageIndex * itemsPerPage) < totItems ?
                ((PageIndex * itemsPerPage)).ToString() : totItems.ToString();

            TotalTab.Text = totItems.ToString();

            List<ListItem> pages = new List<ListItem>();
            if (PageIndex > 1)
            {
                pages.Insert(0, new ListItem("glyphicon glyphicon-menu-left", (PageIndex - 1).ToString(), true));

                if (PageIndex > 2)
                {
                    pages.Insert(0, new ListItem("glyphicon glyphicon-triangle-left", "1", true));
                }
            }
            if (PageIndex + 1 <= pageCount)
            {
                pages.Add(new ListItem("glyphicon glyphicon-menu-right", (PageIndex + 1).ToString(), true));

                if (PageIndex <= pageCount)
                {
                    pages.Add(new ListItem("glyphicon glyphicon-triangle-right", pageCount.ToString(), true));
                }
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }
        else
        {
            pagerZmb.Visible = false;
        }
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        PageIndex = Convert.ToInt32(((sender as LinkButton).CommandArgument));
        hiddenCurrentPage.Value = PageIndex.ToString();
        OnNeedDataSource(this, EventArgs.Empty);
        BindGrid();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Definido en Arbol.ascx.cs que no cargue grilla hasta que se haya seleccionado 'Listado' mejora velocidad carga
        if (Session["RefreshGrid"] == null)
            return;

        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    /// <summary>
    /// Se formatea la fecha
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DataItemGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        System.Data.DataRowView dtview;
        DateTime dt;
        int intCounter;
        // Get the contents of the current row
        // as a DataRowView 
        dtview = (DataRowView)e.Row.DataItem;
        if (dtview != null)
        {
            // Loop through the individual values in the
            // DataRowView's ItemArray 
            for (intCounter = 0; intCounter <= dtview.Row.ItemArray.Length - 1; intCounter++)
            {
                // Check if the current value is
                // a System.DateTime type 
                if (dtview.Row.ItemArray[intCounter] is System.DateTime)
                {
                    // If it is a DateTime, cast it as such
                    // into the variable 
                    dt = (DateTime)dtview.Row.ItemArray[intCounter];
                    // Set the text of the current cell
                    // as the short date representation
                    // of the datetime
                    e.Row.Cells[intCounter + 1 + imageColumnCount].Text = dt.ToShortDateString();
                    e.Row.Cells[intCounter + 1 + imageColumnCount].Attributes.Add("draggable", "true");
                }
            }
        }
    }
}