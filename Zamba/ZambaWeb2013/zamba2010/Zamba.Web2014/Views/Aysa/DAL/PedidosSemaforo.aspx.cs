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

public partial class Views_Aysa_PedidosSemaforo : System.Web.UI.Page
{
    //private long _IndustryID;
    private const string PAGELOCATION = "Aysa/DAL/PedidosSemaforo.aspx{0}";
    private const string TASKSELECTORURL = "WF/TaskSelector.ashx?DocId={1}&DocTypeId={0}";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                DataTable dt = GetPedidos();

                if(dt != null && dt.Rows.Count > 0)
                {
                    FormatGridview();
                    generateGridColumns(dt);
                    grdPedidos.DataSource = dt;
                    grdPedidos.DataBind();
                }
                else
                {
                    lblMsg.Text = "No se han encontrado Pedidos.";
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
                lblMsg.Text = "Error al cargar los Pedidos.";
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

                        grdPedidos.Columns.Add(f);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    private DataTable GetPedidos()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" (select DATEDIFF(day, wfdoc.CHECKIN , GETDATE()) AS [Antiguedad en DIAS],  CAST( wfdoc.Checkin AS smalldatetime) AS [Fecha],  Aysa.i12362  as [Numero de Pedido],  Aysa.i12475  as [Numero de Requerimiento],  usuario1.nombres + ' ' + usuario1.apellido as [Administrativo],  usuario2.nombres + ' ' + usuario2.apellido as [Autorizador],  Aysa.i12368  as [Usuario Solicitante],  Area.name  as [Direccion],  Aysa.i12387  as [Centro de Costo],  Sitio.i12404 as [Sitio], Deposito.i12352 as [Deposito],   Proceso.i12344 as [Proceso], etapa.name as [Etapa], estado.name as [Estado],  wfdoc.DOC_ID, wfdoc.DOC_TYPE_ID   from doc_i12084 Aysa  inner join wfdocument wfdoc on wfdoc.doc_id = Aysa.doc_id   inner join wfworkflow wfwk  on wfwk.work_id = wfdoc.work_id  inner join wfstep etapa on etapa.step_id = wfdoc.step_id  and ( etapa.step_id = 12116 or etapa.step_id = 12118  or etapa.step_id = 12121 or etapa.step_id = 12120 or etapa.step_id = 12122)   inner join wfstepstates estado on estado.doc_state_id = wfdoc.do_state_id  inner join usrtable usuario1 on usuario1.id = Aysa.i12461  inner join usrtable usuario2 on usuario2.id = Aysa.i12433  inner join usrgroup area on area.id = Aysa.i12451  inner join doc_i12116 sitio on sitio.i12403 = Aysa.i12427  inner join doc_i12098 deposito on deposito.i12351 = Aysa.i12428  inner join doc_i12125 proceso on proceso.i12436 =  Aysa.i12437  )  union  (  select DATEDIFF(day, wfdoc.CHECKIN , GETDATE()) AS [Antiguedad en DIAS],   CAST( wfdoc.Checkin AS smalldatetime) AS [Fecha],  DAL.i12362  as [Numero de Pedido],  DAL.i12475  as [Numero de Requerimiento],  usuario1.nombres + ' ' + usuario1.apellido as [Administrativo],  usuario2.nombres + ' ' + usuario2.apellido as [Autorizador],  DAL.i12368  as [Usuario Solicitante],  Area.name  as [Direccion],  '' as [Centro de Costo],  Sitio.i12404 as [Sitio], Deposito.i12352 as [Deposito],  Proceso.i12344 as [Proceso], etapa.name as [Etapa], estado.name as [Estado],  wfdoc.DOC_ID, wfdoc.DOC_TYPE_ID  from doc_i12123 DAL  inner join wfdocument wfdoc on wfdoc.doc_id = DAL.doc_id  inner join wfworkflow wfwk  on wfwk.work_id = wfdoc.work_id  inner join wfstep etapa on etapa.step_id = wfdoc.step_id  and ( etapa.step_id = 12129  or etapa.step_id = 12131  or etapa.step_id = 12153  or etapa.step_id = 12132  or etapa.step_id = 12136)  inner join wfstepstates estado on estado.doc_state_id = wfdoc.do_state_id  inner join usrtable usuario1 on usuario1.id = DAL.i12461  inner join usrtable usuario2 on usuario2.id = DAL.i12433  inner join usrgroup area on area.id = DAL.i12451  inner join doc_i12116 sitio on sitio.i12403 = DAL.i12427  inner join doc_i12098 deposito on deposito.i12351 = DAL.i12428  inner join doc_i12125 proceso on proceso.i12436 =  DAL.i12437) ");
       
                
        DataSet ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sb.ToString());

        if (ds.Tables.Count > 0)
            return ds.Tables[0];
        else
            return null;
    }

    private void FormatGridview()
    {
        try
        {
            grdPedidos.AutoGenerateColumns = false;
            grdPedidos.ShowFooter = false;
            
            String[] a = { "DOC_TYPE_ID", "DOC_ID"};

            grdPedidos.Columns.Clear();

            HyperLinkField colver = new HyperLinkField
            {
                ShowHeader = true,
                HeaderText = "Ver",
                Target = "_blank",
                Text = "Ver",
                DataTextFormatString = "<img src=\"PedidosSemaphoreImageHandler.aspx?Date={0}\" border=0/>",
                DataTextField = "Fecha"
            };

            colver.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            
            colver.DataNavigateUrlFields = a;

            Uri currUrl = HttpContext.Current.Request.Url;
            colver.DataNavigateUrlFormatString = currUrl.ToString().Replace(string.Format(PAGELOCATION, currUrl.Query), TASKSELECTORURL);

            grdPedidos.Columns.Add(colver);
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }
}
