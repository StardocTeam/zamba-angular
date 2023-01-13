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

    #region Properties
    public int ? PageCount { get; set; }
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
    public int ? PageIndex { get; set; }
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
        PageIndex = 1;
    }

    public delegate void NeedDataSource(object sender, EventArgs e);
    public event NeedDataSource OnNeedDataSource;

    public void BindGrid()
    {
        if (DataSource != null)
        {
            grvGrid.DataSource = DataSource;
            grvGrid.DataBind();
            PageIndex = (PageIndex.HasValue) ? PageIndex.Value : 1;

            BindPager();
        }
    }

    private void BindPager()
    {        
        if(!PageCount.HasValue)
            throw new Exception("ZGridView: Page count not set");

        int pageCount = PageCount.Value;

        List<ListItem> pages = new List<ListItem>();
        ListItem lItem;

        if (pageCount > PagingButtonCount)
        {
            int pageStartToRender = 0;
            int pageEndToRender = 0;

            pageStartToRender = (PageIndex.Value + _pageButtonCount < pageCount) ? PageIndex.Value : pageCount - _pageButtonCount;
            pageEndToRender = pageStartToRender + _pageButtonCount;

            for (int i = pageStartToRender; i <= pageEndToRender; i++)
            {
                lItem = new ListItem((i).ToString(), (i).ToString(), true);
                lItem.Selected = (i == PageIndex);
                lItem.Enabled = (i != PageIndex);

                pages.Add(lItem);
            }

            if (PageIndex > 1)
            {
                pages.Insert(0, new ListItem("...", (pageStartToRender - 1).ToString(), true));

                if (PageIndex > 2)
                {
                    pages.Insert(0, new ListItem("Primera p&aacute;gina", "1", true));
                }
            }

            if (pageEndToRender + 1 <= pageCount)
            {
                pages.Add(new ListItem("...", (pageEndToRender + 1).ToString(), true));

                if (pageEndToRender <= pageCount)
                {
                    pages.Add(new ListItem("&Uacute;ltima p&aacute;gina", pageCount.ToString(), true));
                }
            }
        }
        else
            if (pageCount > 1)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    lItem = new ListItem((i).ToString(), (i).ToString(), true);
                    lItem.Selected = (i == PageIndex);
                    lItem.Enabled = (i != PageIndex);
                    pages.Add(lItem);
                }
            }

        rptPager.DataSource = pages;
        rptPager.DataBind();
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        PageIndex = Convert.ToInt32(((sender as LinkButton).CommandArgument));
        OnNeedDataSource(this,EventArgs.Empty);
        BindGrid();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
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
                    e.Row.Cells[intCounter+1].Text = dt.ToShortDateString();
                }
            }
        }
    }
}