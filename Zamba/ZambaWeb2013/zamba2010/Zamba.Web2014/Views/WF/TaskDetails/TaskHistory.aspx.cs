using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class Views_WF_TaskDetails_TaskHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Zamba.Services.SZOptBusiness ZOptBusines = new Zamba.Services.SZOptBusiness();
        Page.Title = (string)ZOptBusines.GetValue("WebViewTitle") + " - Historial";
        ZOptBusines = null;

        if (!IsPostBack)
        {
            try
            {
                //Obtencion del docid
                Int64 docId = Int64.Parse(Request["ResultId"].ToString());
                ViewState["DocId"] = docId;

                //Obtencion de tareas asociadas al docid
                Zamba.Services.STasks st = new Zamba.Services.STasks();
                List<Int64> taskIds = st.GetTaskIDsByDocId(docId);

                if(taskIds.Count > 0)
                {
                    //Se hardcodea a la primer tarea recibida.
                    //Para solucionarlo se debe asignar el taskId sobre DocToolbar.ascx 
                    //desde DocViewer.ascx y FormBrowser.ascx.
                    Int64 taskId = taskIds[0];
                    ViewState["TaskId"] = taskId;

                    //Si tiene tarea, muestra por defecto el historial de esta
                    MostrarTodos();
                }
                else
                {
                    //Si solo tiene documento, muestra el historial de atributos
                    MostrarHistorialIndices();                    
                }
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
        }

        //Si no tiene tarea, oculta el boton que muestra el historial de esta
        if(ViewState["TaskId"] == null)
        {
            btnMostrarTodos.Visible = false;
        }
    }

    private void MostrarTodos()
    {
        Int64 TaskId = Int64.Parse(ViewState["TaskId"].ToString());
        ucTaskHistoryGrid.LoadTaskHistory(TaskId);
    }

    protected void btnMostrarTodos_Click(object sender, EventArgs e)
    {
        MostrarTodos();
    }

    protected void btnMostrarHistorialIndice_Click(object sender, EventArgs e)
    {
        MostrarHistorialIndices();
    }

    private void MostrarHistorialIndices()
    {
        Int64 docID = Int64.Parse(ViewState["DocId"].ToString());
        ucTaskHistoryGrid.LoadOnlyIndexsHistory(docID);
    }

    public static IHtmlString GetJqueryCoreScript()
    {
        return Tools.GetJqueryCoreScript(HttpContext.Current.Request);
    }
}
