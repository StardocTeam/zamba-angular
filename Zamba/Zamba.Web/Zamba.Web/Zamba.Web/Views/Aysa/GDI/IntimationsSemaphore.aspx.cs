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

public partial class Views_Aysa_IntimationsSemaphore : System.Web.UI.Page
{
    private long _IndustryID;
    private const string PAGELOCATION = "Aysa/GDI/IntimationsSemaphore.aspx{0}";
    private  string TASKSELECTORURL = "WF/TaskSelector.ashx?DocId={1}&DocTypeId={0}&userId=" + Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();
    UserPreferences UP = new UserPreferences();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                DataTable dt = GetIntimations();

                if(dt != null && dt.Rows.Count > 0)
                {
                    FormatGridview();
                    generateGridColumns(dt);
                    grdIntimaciones.DataSource = dt;
                    grdIntimaciones.DataBind();
                }
                else
                {
                    lblMsg.Text = "No se han encontrado intimaciones.";
                    lblMsg.Visible = true;
                }

                if (Zamba.Membership.MembershipHelper.CurrentUser != null)
                {
                    IUser user = Zamba.Membership.MembershipHelper.CurrentUser;
                    SRights rights = new SRights();
                    Int32 type = 0;
                    if (user.WFLic) type = 1;
                    if (user.ConnectionId > 0)
                    {
                        UserPreferences UserPreferences = new UserPreferences();
                        Ucm.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, "30")), type);
                        UserPreferences = null;
                    }
                    else
                        Response.Redirect("~/Views/Security/LogIn.aspx");
                    rights = null;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                lblMsg.Text = "Error al cargar las intimaciones.";
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

                        grdIntimaciones.Columns.Add(f);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    private DataTable GetIntimations()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ind.i1187 as [Codificación de la Industria],");
        sb.Append("ind.i1188 as [Razon Social],");
        sb.Append("ind.i1192 as [Calle],");
        sb.Append("ind.i1193 as [Num],");
        sb.Append(" Convert(char(10), ind.i11318, 103) as [Fecha ultima CD],");
        sb.Append(" Convert(char(10), ind.i11317, 103) as [Fecha CN],");
        sb.Append("ind.i11296 as [ID de Muestra],");
        sb.Append("ind.i11298 as [Nro Siseme],");
        sb.Append("slst_s11312.descripcion as [Tipo Intimación],");
        sb.Append("ind.i1229 as [Observaciones],");
        sb.Append("slst_s1181.descripcion as [Region],");
        sb.Append("slst_s11297.descripcion as [Motivo de la Intimación],");
        sb.Append("ind.i11335 as [Nro Siseme Recepción],");
        sb.Append("(CASE WHEN i11337 = '1' THEN 'SI' ELSE 'NO' END) as [Firma CN],");
        sb.Append("Convert(char(10), ind.i11338, 103) as [Plazo Establecido],");
        sb.Append("Convert(char(10), ind.i11339, 103) as [Fecha],");
        sb.Append("Convert(char(10),  ind.i11344, 103) as [Fecha Ingreso Siseme],");
        sb.Append(" ind.DOC_ID,wf.DOC_TYPE_ID ");
        sb.Append(" FROM WFDocument wf INNER JOIN ");
        sb.Append(" DOC_I11074 ind ON wf.Doc_ID = ind.DOC_ID ");
        sb.Append(" INNER JOIN SLST_S11312 on ind.i11312 = slst_s11312.codigo");
        sb.Append(" INNER JOIN SLST_S1181 on ind.i1181 = slst_s1181.codigo");
        sb.Append(" INNER JOIN SLST_S11297 on ind.i11297 = slst_s11297.codigo");
        sb.Append(" where ");

        if (long.TryParse(Request.QueryString["IndustryID"], out _IndustryID))
        {
            sb.Append("I1217 = ");
            sb.Append(_IndustryID);
            sb.Append(" and ");
        }

        sb.Append("ind.I11338 is not null and ");
        sb.Append(GetWhereForRegion(Zamba.Membership.MembershipHelper.CurrentUser.ID));
        //La etapa tiene que ser distinta de 0. Desestimadas .
        sb.Append("ind.I11338 < getdate() + 31 and wf.step_ID <> 11115 ");
        sb.Append("order by ind.I11338 asc");

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
            grdIntimaciones.AutoGenerateColumns = false;
            grdIntimaciones.ShowFooter = false;
            
            String[] a = { "DOC_TYPE_ID", "DOC_ID"};

            grdIntimaciones.Columns.Clear();

            HyperLinkField colver = new HyperLinkField
            {
                ShowHeader = true,
                HeaderText = "Ver",
                Target = "_blank",
                Text = "Ver",
                DataTextFormatString = "<img src=\"../SemaphoreImageHandler.aspx?Date={0}\" border=0/>",
                DataTextField = "Plazo Establecido"
            };

            colver.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            
            colver.DataNavigateUrlFields = a;

            Uri currUrl = HttpContext.Current.Request.Url;
            colver.DataNavigateUrlFormatString = currUrl.ToString().Replace(string.Format(PAGELOCATION, currUrl.Query), TASKSELECTORURL);

            grdIntimaciones.Columns.Add(colver);
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }
}
