using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI.WebControls;
using Zamba;
using Zamba.Core;
using Zamba.Services;

public partial class TaskGrid : System.Web.UI.UserControl
{
    RightsBusiness RiB = new RightsBusiness();
    public void LoadTasks(Int32 WFId, Int32 StepId, Int32 DocTypeId, TreeNode Node)
    {
        SRights sRights = new SRights();

        if ((Node != null) && RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.Use, StepId))
        {
            if (WFId == 0)
            {
                if (ViewState["WFId"] != null && !string.IsNullOrEmpty(ViewState["WFId"].ToString()))
                {
                    WFId = Convert.ToInt32(ViewState["WFId"]);
                }
            }

            if (StepId == 0)
            {
                if (ViewState["StepId"] != null && !string.IsNullOrEmpty(ViewState["StepId"].ToString()))
                {
                    StepId = Convert.ToInt32(ViewState["StepId"]);
                }
            }

            if (DocTypeId == 0)
            {
                if (ViewState["DocTypeId"] != null && !String.IsNullOrEmpty(ViewState["DocTypeId"].ToString()))
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


            UserPreferences.setValue("UltimoWFStepUtilizado", StepId.ToString(), Zamba.UPSections.WorkFlow);
            ucTaskGrid.Visible = true;
            lblMsg.Visible = false;

            //ucTaskGrid.loadTasks(StepId, Node, ucTaskGridFilter.FC)
            ZOptBusiness zopt = new ZOptBusiness();
            string showAysaFilter = zopt.GetValue("ShowAysaGridFilter");
            string showMarshFilter = zopt.GetValue("ShowMarshGridFilter");
            zopt = null;

            if ((ucTaskGrid.ucTaskGridFilter != null))
            {
                ucTaskGrid.ucTaskGridFilter.Visible = true;
                //ucTaskGrid.ucTaskGridFilter.OnFilterCall += UcTaskGridFilter_OnFilterCall;
            }
        }
        else
        {
            ucTaskGrid.Visible = false;
            if ((ucTaskGrid.ucTaskGridFilter != null))
                ucTaskGrid.ucTaskGridFilter.Visible = false;
            lblMsg.Visible = true;
        }
    }




    //public void GetCMB(long StepId)
    //{
    //    if ((ucTaskGrid.ucTaskGridFilter != null))
    //        ucTaskGrid.ucTaskGridFilter.GetCMB(StepId);
    //    //if ((ucTaskGridFilterMarsh != null)) ;
    //        //ucTaskGridFilterMarsh.GetCMB(StepId);

    //}

    /// <summary>
    /// Realiza el rebindeo de la grilla de tareas
    /// </summary>
    /// <remarks></remarks>
    public void RebindGrid()
    {
        ucTaskGrid.BindZGrid(true);
    }
}
