using System;
using System.Web.UI;
using Zamba.Core;
using System.Data;
using System.Web.UI.WebControls;
using Zamba.Membership;

public partial class Views_UC_Grid_UcNewsGrid : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Zamba.Services.SZOptBusiness ZOptBusines = new Zamba.Services.SZOptBusiness();

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
        try
        {

        
        DataTable dt;
        grdNews.Columns.Clear();
        dt = NewsBusiness.ShowNews(Int64.Parse(Session["UserId"].ToString())).Tables[0];

        //Si existen novedades las muestra, caso contrario redirecciona a tareas
        if (dt.Rows.Count > 0)
        {
            generateGridColumns(dt);
            BindGrid(dt);
            lblZeroNews.Visible = false;
        }
        else
        {
            if (Boolean.Parse(UserPreferences.getValue("ShowZeroNews", Sections.UserPreferences, "true")))
            {
                lblZeroNews.Visible = true;
            }
            else
            {
                //TODO: AL CARGAR EL SCRIPT, TODAVIA NO ESTA RENDERIZADO EL TABBER Y TIRA ERROR
                //SelectTaskListTab();
            }
        }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }

    }

    private void SelectTaskListTab()
    {
        if (!Page.ClientScript.IsStartupScriptRegistered("SelectTaskListTab"))
        {
            string script = "$(function() { ZDispatcherRedirection_tabtasklist(); });";
            Page.ClientScript.RegisterStartupScript(typeof(Page), "SelectTaskListTab", script, true);
        }
    }

    private void generateGridColumns(DataTable dt)
    {
        try
        {
            if (dt != null && dt.Columns.Count > 0)
            {
                grdNews.Columns.Clear();

                //Se agrega la columna al gridview la columna que representa los hipervinculos para abrir la tarea
                HyperLinkField colver = new HyperLinkField
                {
                    ShowHeader = true,
                    HeaderText = "Ver",
                    Text = "Ver",
                    Target = "_blank",
                    DataTextFormatString = "<img src=\"../Tools/icono.aspx?id={0}\" border=0/>",
                    DataTextField = "ICONID"
                };

                colver.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                String[] a = { "DOCID", "DOCTYPEID" };
                colver.DataNavigateUrlFields = a;
                colver.DataNavigateUrlFormatString = MembershipHelper.Protocol + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + @"/Views/WF/TaskSelector.ashx?docid={0}&doctypeid={1}&news=1"; ;
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

 //  protected void btnRefresh_Click(object sender, EventArgs e)
  //  {
  //     LoadNews();
  //  }
}