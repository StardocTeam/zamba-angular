using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Zamba.Core;
using System.IO;
using System.Text;
using Zamba.Services;
using System.Globalization;
using System.Data.SqlClient;
using Telerik.Web.UI;
using Zamba;

public partial class ListadoEstablecimientos : System.Web.UI.Page
{
    RightsBusiness RiB = new RightsBusiness();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DataTable dt = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, "select doc_i601058.doc_id, rtrim(doc_file) as Imagen, rtrim(DISK_VOL_PATH + '\\' + CAST(DT.DOC_TYPE_ID AS VARCHAR) + '\\' + CAST(DT.OFFSET AS VARCHAR) + '\\' + DT.DOC_FILE) as fullpath, doc_i601058.I601275, I601272, I601276, I1229 from doc_i601058 inner join doc_i601068 on doc_i601058.I601275=doc_i601068.I601275 inner join doc_t601068 DT on doc_i601068.doc_id=DT.doc_id INNER JOIN DISK_VOLUME DV ON DT.VOL_ID = DV.DISK_VOL_ID WHERE i601306='SI'").Tables[0];

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (File.Exists(dr["fullpath"].ToString()))
                    {
                        ZOptBusiness zoptb = new ZOptBusiness();
                        String CurrentTheme = zoptb.GetValue("CurrentTheme");
                        zoptb = null;

                        File.Copy(dr["fullpath"].ToString(), Zamba.Membership.MembershipHelper.AppFormPath(CurrentTheme) + "\\imgMantenimiento\\" + dr["Imagen"].ToString(), true);
                    }
                }


                RadRotator1.DataSource = dt;
                RadRotator1.DataBind();
            }
        }
    }

    /// <summary>
    /// Muestra la tarea seleccionada
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShow_Click(object sender, RadRotatorEventArgs e)
    {
        Int64 doc_id = Int64.Parse(((HiddenField)e.Item.Controls[3]).Value);

        STasks stasks = new STasks();
        ITaskResult Task = stasks.GetTaskByDocId(doc_id);

        //Realiza la apertura del documento dependiendo de si tiene tareas o permisos.
        string script;
        string path = "http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + "/Views/";
        if (Task != null)
        {
            SRights sRights = new SRights();

            //Verifica si tiene permisos de abrir la tarea
            if (RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID,ObjectTypes.WFSteps, RightsType.Use, Task.StepId))
                script = "parent.HideHome();parent.OpenTask('" + path + "WF/TaskViewer.aspx?taskid=" + Task.TaskId + "'," + Task.TaskId + ",'" + Task.Name + "');";
            else
                script = "parent.HideHome();parent.OpenDocTask('" + path + "/Search/DocViewer.aspx?doctype=601058&docid=" + doc_id + "'," + doc_id + ",'" + Task.Name + "');";
        }
        else
        {
            script = "parent.HideHome();parent.OpenDocTask('" + path + "/Search/DocViewer.aspx?doctype=601058&docid=" + doc_id + "'," + doc_id + ",'" + Task.Name + "');";
        }
        
        Page.ClientScript.RegisterStartupScript(this.GetType(), "DoOpenTaskScript", script, true);
    }
}