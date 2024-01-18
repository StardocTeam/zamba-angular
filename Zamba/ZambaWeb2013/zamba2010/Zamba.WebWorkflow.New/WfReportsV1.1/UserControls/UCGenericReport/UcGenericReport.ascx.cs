﻿using System;
using System.Web.UI.WebControls;
using System.Data;

public partial class WfReports_UserControls_UCGenericReport_UcGenericReport : System.Web.UI.UserControl
{
    private String _query;
    public String Query
    {
        set
        {
            _query = value;
            hdQuery.Value = _query;
            Reload();
        }

        get
        {
            if (!string.IsNullOrEmpty(_query))
                return _query;

            return hdQuery.Value;
        }
    }

    public String Title
    {
        get { return lblTitle.Text; }
        set { lblTitle.Text = value; }
    }

    public String NoDataMessage
    {
        get { return lblNoData.Text; }
        set { lblNoData.Text = value; }
    }

    public Int32 PageSize
    {
        set
        {
            gvGeneric.PageSize = value;
            gvGeneric.DataBind();
        }
    }
    protected void gvGeneric_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvGeneric.PageIndex = e.NewPageIndex;
        Reload();
    }

    private void Reload()
    {
        try
        {

            Int32 MinValue = gvGeneric.PageSize * (gvGeneric.PageIndex);
            Int32 MaxValue = MinValue + gvGeneric.PageSize;

            DataSet ds = Zamba.Servers.Server.get_Con(false, false, false).ExecuteDataset(System.Data.CommandType.Text, Query);

            Int32 RowsCount = ds.Tables[0].Rows.Count;

            for (int i = 0; i < RowsCount; i++)
            {
                if (i < MinValue || i > MaxValue)
                    ds.Tables[0].Rows[i].Delete();
            }
            ds.AcceptChanges();

            gvGeneric.DataSource = ds;
            gvGeneric.DataBind();

            

        }
        catch (Exception ex)
        {
            lblTitle.Text = ex.ToString();
        }

        ValidateStatus();
    }

    public WfReports_UserControls_UCGenericReport_UcGenericReport()
    {
        this.hdQuery = new HiddenField();
        this.lblNoData = new Label();
        this.lblTitle = new Label();
        this.gvGeneric = new GridView();
    }

    private void ValidateStatus()
    {
        if (gvGeneric.Rows.Count == 0)
        {
            this.hdQuery.Visible = false;
            this.lblNoData.Visible = true;
            this.gvGeneric.Visible = false;
        }
        else
        {
            this.hdQuery.Visible = true ;
            this.lblNoData.Visible = false;
            this.gvGeneric.Visible = true;
        }
    }
}