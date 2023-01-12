using System;
using System.Web.UI;
using Zamba.Core;
using System.Data;
using System.Web.UI.WebControls;

public partial class WebPages_News : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Zamba.Services.SZOptBusiness ZOptBusines = new Zamba.Services.SZOptBusiness();
        Page.Title = (string)ZOptBusines.GetValue("WebViewTitle") + " - Novedades";

        if (!Page.IsPostBack)
        {            
             if (Session["UserId"] != null)
            {
                        LoadNews();
            }

        }
    }

    public void LoadNews()
    {
        DataTable dt;
        grdNews.Columns.Clear();
        dt = NewsBusiness.ShowNews(Int64.Parse(Session["UserId"].ToString())).Tables[0];

        //Si existen novedades las muestra, caso contrario redirecciona a tareas
        if (dt.Rows.Count > 0)
        {
            generateGridColumns(dt);
            BindGrid(dt);
        }
        else
        {
            if (Boolean.Parse(UserPreferences.getValue("ShowZeroNews", Sections.UserPreferences, "true")))
                lblZeroNews.Visible = true;
            else
                Response.Redirect("~/Views/WF/WF.aspx");
        }
    }

    private void generateGridColumns(DataTable dt)
    {
        try
        {
            if (dt != null && dt.Columns.Count > 0)
            {
                //Se agrega la columna al gridview la columna que representa los hipervinculos para abrir la tarea
                HyperLinkField colver = new HyperLinkField
                {
                    ShowHeader = true,
                    HeaderText = "Ver",
                    Target = "_blank",
                    Text = "Ver",
                    DataTextFormatString = "<img src=\"../Tools/icono.aspx?id={0}\" border=0/>",
                    DataTextField = "ICONID"
                };
                colver.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                String[] a = { "DOCTYPEID", "DOCID"};
                colver.DataNavigateUrlFields = a;
                colver.DataNavigateUrlFormatString = @"../../Views/WF/TaskSelector.ashx?doctypeid={0}&docid={1}&news=1";
                grdNews.Columns.Add(colver);

                //Se modifica el nombre de la columna con la descripcion y fecha
                if (Zamba.Servers.Server.isOracle)
                    dt.Columns["c_value"].ColumnName = "Novedades";
                else
                    dt.Columns["value"].ColumnName = "Novedades";

                dt.Columns["crdate"].ColumnName = "Fecha";

                //Se agregan las columnas del datatable al gridview
                foreach (DataColumn c in dt.Columns)
                {
                    BoundField f = new BoundField
                    {
                        DataField = c.Caption,
                        ShowHeader = true,
                        HeaderText = c.Caption,
                        SortExpression = c.Caption + " ASC"
                    };

                    grdNews.Columns.Add(f);
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
            dt.Columns.Add("ICONID");
            dt.Columns["ICONID"].SetOrdinal(0);
            grdNews.DataSource = dt;
            grdNews.DataBind();

            grdNews.Columns[1].Visible = false;
            grdNews.Columns[2].Visible = false;
            grdNews.Columns[3].Visible = false;
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }
}