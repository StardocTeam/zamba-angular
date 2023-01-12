using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;

public partial class ResultsGrid : System.Web.UI.UserControl
{


    public void LoadTasks(Int32 WFId, Int32 StepId, Int32 DocTypeId)
    {
        try
        {
            if (WFId > 0)
            {
                if (!string.IsNullOrEmpty(ViewState["WFId"].ToString()))
                {
                    WFId = Convert.ToInt32(ViewState["WFId"]);
                }
            }

            if (StepId > 0)
            {
                if (!string.IsNullOrEmpty(ViewState["WFId"].ToString()))
                {
                    StepId = Convert.ToInt32(ViewState["StepId"]);
                }
            }

            if (DocTypeId > 0)
            {
                if (!string.IsNullOrEmpty(ViewState["DocTypeId"].ToString()))
                {
                    DocTypeId = Convert.ToInt32(ViewState["DocTypeId"]);
                }
            }

            //Variables de estado solo usadas en la grilla de tareas
            ViewState["WFId"] = WFId;
            ViewState["StepId"] = StepId;
            ViewState["DocTypeId"] = DocTypeId;

            //Variables de sesion usadas en el taskviewer (si se abre una tarea)
            Session["WFId"] = WFId;
            Session["StepId"] = StepId;
            Session["DocTypeId"] = DocTypeId;

            //WI 6042
            //Grid.ColumnsToHide.Add("Nombre del Documento")
            //Grid.ColumnsToHide.Add("Situacion")
            //Grid.ColumnsToHide.Add("Asignado")
            //Grid.ColumnsToHide.Add("Vencimiento")
            //Grid.ColumnsToHide.Add("DocId")
            //Grid.ColumnsToHide.Add("folder_Id")
            //Grid.ColumnsToHide.Add("Doctype_Id")
            //Grid.ColumnsToHide.Add("WfStepId")
            //Grid.ColumnsToHide.Add("Do_State_ID")
            //Grid.ColumnsToHide.Add("IconId")
            //Grid.ColumnsToHide.Add("User_Asigned")
            //Grid.ColumnsToHide.Add("Exclusive")
            //Grid.ColumnsToHide.Add("User_ Asigned_By")
            //Grid.ColumnsToHide.Add("Date_Asigned_By")
            //Grid.ColumnsToHide.Add("Task_Id")
            //Grid.ColumnsToHide.Add("Task_State_Id")
            //Grid.ColumnsToHide.Add("Remark")
            //Grid.ColumnsToHide.Add("Tag")
            //Grid.ColumnsToHide.Add("work_id")
            //Grid.ColumnsToHide.Add("Solicitud")
            //Grid.ColumnsToHide.Add("TaskColor")


            //Grid.Height = "600px"
            //Grid.Width = "700px"
            //Grid.WFId = WFId
            //Grid.StepId = StepId
            //Grid.DocTypeId = DocTypeId

            //Grid.LoadSearchResult()
            //Grid.LoadDocTypes()
            //Grid.LoadTasks()
        }
        catch (Exception ex)
        {

            ZCore.raiseerror(ex);
        }
    }


    public void LoadResults(DataTable dt)
    {
        Int32 pageId = Int16.Parse(Session["ResultsPagingId"].ToString());
        Int32 pageSize = Int16.Parse(Session["PageSize"].ToString());

        //lblPageNumber.Text = string.Empty;
        //lblTotal.Text = "No hay registros";
        //EnableSearchButtons(false, false);

        YuiGrid1.AutoExpandMax = true;
        YuiGrid1.AutoExpandMin = true;
        YuiGrid1.TotalRecords = dt.Rows.Count;
        YuiGrid1.PageSize = pageSize;
        YuiGrid1.EnablePaging = true;
        YuiGrid1.EnableRowSorting = true;
        YuiGrid1.PagingStyle = ExtExtenders.PagingStyle.NavBar;

        YuiGrid1.DataSource = dt;
        YuiGrid1.DataBind();

        Session["ResultsCount"] = dt.Rows.Count;

    }

    private void DocTypeSelected(long DocTypeId)
    {
        //ViewState("DocTypeId") = String.Empty
        //LoadTasks(Session["WFId"], Session["StepId"], DocTypeId)
        //cuando se llame a esta funcion (DocTypeSelected) se debe usar el request en lugar de la llamada a "session"
        //LoadTasks(Request("WFid")...
    }

    protected void YuiGrid1_SelectedIndexChanged(object sender, ExtExtenders.SelectedRowArgs e)
    {
        Session["docid"] = Int64.Parse(e.SelectedRow["DOC_ID"].ToString());
        Session["doctypeid"] = Int64.Parse(e.SelectedRow["DOC_TYPE_ID"].ToString());
    }

    protected void imgtareas_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (string.Compare(Session["docid"].ToString(), "0") != 0 && string.Compare(Session["doctypeid"].ToString(), "0") != 0)
        {
            Response.Redirect("http://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + "/Views/WF/TaskSelector.ashx?doctypeid=" + Session["doctypeid"] + "&docid=" + Session["docid"]);
        }
    }
}

