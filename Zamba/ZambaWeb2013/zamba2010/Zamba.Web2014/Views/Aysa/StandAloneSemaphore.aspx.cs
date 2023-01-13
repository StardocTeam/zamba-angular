using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Zamba.Services;
using Zamba.Core;
using Zamba.AppBlock;
using System.Text;

public partial class Views_Aysa_StandAloneSemaphore : System.Web.UI.Page
{
    private long _IndustryID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                DataTable dt = GetInspections();

                if(dt != null && dt.Rows.Count > 0)
                {
                    FormatGridview();
                    generateGridColumns(dt);
                    grdInspecciones.DataSource = dt;
                    grdInspecciones.DataBind();
                }
                else
                {
                    lblMsg.Text = "No se han encontrado inspecciones.";
                    lblMsg.Visible = true;
                }

                if (Session["User"] != null)
                {
                    IUser user = (IUser)Session["User"];
                    SRights rights = new SRights();
                    Int32 type = 0;
                    if (user.WFLic) type = 1;
                    if (user.ConnectionId > 0)
                    {
                        SUserPreferences SUserPreferences = new SUserPreferences();
                        rights.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type);
                        SUserPreferences = null;
                    }
                    else
                        Response.Redirect("~/Views/Security/LogIn.aspx");
                    rights = null;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                lblMsg.Text = "Error al cargar las inspecciones.";
                lblMsg.Visible = true;
            }
        }
    }

    private void generateGridColumns(DataTable _dt)
    {
        try
        {
            if (_dt != null && _dt.Columns.Count > 0)
            {
                foreach (DataColumn c in _dt.Columns)
                {
                    if (!(c.Caption.Contains("DOC_ID") || c.Caption.Contains("DOC_TYPE_ID")))
                    {
                        BoundField f = new BoundField
                        {
                            DataField = c.Caption,
                            ShowHeader = true,
                            HeaderText = c.Caption,
                            SortExpression = c.Caption + " ASC"
                        };

                        grdInspecciones.Columns.Add(f);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    private DataTable GetInspections()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ind.I1187 as 'Codificación de Industria',");
        sb.Append(" ind.I1188 as 'Razón social',");
        sb.Append(" ind.I1192 as 'Calle',");
        sb.Append(" ind.I1193 as 'Número',");
        sb.Append(" I1224 as 'Nro de ODT', sls.descripcion as 'Motivo',");
        sb.Append(" Convert(char(10), I11358, 103) as 'Fecha programada',");
        sb.Append(" doc.DOC_ID,wf.DOC_TYPE_ID ");
        sb.Append("FROM DOC_I1028 doc with(nolock) ");
        sb.Append("left outer join DOC_I1027 ind with(nolock) on ind.I1217 = doc.I1217 ");
        sb.Append("left outer join slst_s1228 sls with(nolock) on doc.I1228 = sls.codigo ");
        sb.Append("left outer join WFDocument wf with(nolock) on wf.DOC_ID = doc.DOC_ID ");
        sb.Append("where ");

        if (long.TryParse(Request.QueryString["IndustryID"], out _IndustryID))
        {
            sb.Append("I1217 = ");
            sb.Append(_IndustryID);
            sb.Append(" and ");
        }
        
        sb.Append("I11358 is not null and ");
        sb.Append(GetWhereForRegion(long.Parse((string)Session["UserId"])));
        sb.Append("I11358 < getdate() + 31 and wf.step_ID = 1070 ");
        sb.Append("order by I11358 asc");

        DataSet ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sb.ToString());

        if (ds.Tables.Count > 0)
            return ds.Tables[0];
        else
            return null;
    }

    private string GetWhereForRegion(long UsrId)
    {
        StringBuilder sbQuery = new StringBuilder();
        sbQuery.Append("select g.name as GroupName ");
        sbQuery.Append("from USR_R_GROUP usrG ");
        sbQuery.Append("left outer join USRGROUP G on G.id = usrG.GROUPID ");
        sbQuery.Append("where Usrid = ");
        sbQuery.Append(UsrId.ToString());

        DataSet ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sbQuery.ToString());

        if (ds.Tables.Count > 0)
        {
            DataTable dtRegiones = ds.Tables[0];

            List<string> regions = new List<string>();

            foreach (DataRow row in dtRegiones.Rows)
            {
                if (row["GroupName"] != null)
                {
                    string groupName = row["GroupName"].ToString();

                    switch (groupName)
                    {
                        case "DRCF":
                            regions.Add("ind.I1181 = 1");
                            break;
                        case "DRN":
                            regions.Add("ind.I1181 = 2");
                            break;
                        case "DRO":
                            regions.Add("ind.I1181 = 5");
                            break;
                        case "DRSE":
                            regions.Add("ind.I1181 = 3");
                            break;
                        case "DRSO":
                            regions.Add("ind.I1181 = 4");
                            break;
                    }
                }
            }
            sbQuery = new StringBuilder();
            if (regions.Count > 1)
            {
                sbQuery.Append("(");
                foreach (string item in regions)
                {
                    sbQuery.Append(item);
                    sbQuery.Append(" OR ");
                }
                if (sbQuery.ToString().Trim().EndsWith("OR"))
                {
                    sbQuery.Remove(sbQuery.ToString().LastIndexOf("OR "), 3);
                }

                sbQuery.Append(") and ");
            }
            else
            {
                if (regions.Count > 0)
                {
                    sbQuery.Append(regions[0] + " and ");
                }
            }
        }

        return sbQuery.ToString();
    }

    private void FormatGridview()
    {
        try
        {
            grdInspecciones.AutoGenerateColumns = false;
            grdInspecciones.ShowFooter = false;

            String[] a = { "DOC_TYPE_ID", "DOC_ID"};

            grdInspecciones.Columns.Clear();

            HyperLinkField colver = new HyperLinkField
            {
                ShowHeader = true,
                HeaderText = "Ver",
                Target = "_blank",
                Text = "Ver",
                DataTextFormatString = "<img src=\"SemaphoreImageHandler.aspx?Date={0}\" border=0/>",
                DataTextField = "Fecha programada"
            };

            colver.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

            colver.DataNavigateUrlFields = a;
            colver.DataNavigateUrlFormatString = @"../../Views/WF/TaskSelector.ashx?doctype={0}&docid={1}";

            grdInspecciones.Columns.Add(colver);
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }
}
