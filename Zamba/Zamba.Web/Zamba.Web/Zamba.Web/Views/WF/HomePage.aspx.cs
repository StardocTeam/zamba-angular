using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Zamba.Core;
using Zamba.Services;
using Zamba.Core.Enumerators;
using System.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using Zamba.Core.WF.WF;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using Zamba.Web.App_Code.Helpers;
using Zamba.Web;
using Zamba.Membership;
using Zamba;
using Zamba.Framework;


namespace Zamba.Web.Views.UC.Home
{
    public partial class HomePage : System.Web.UI.Page
    {





        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.HasKeys() && Request.QueryString["userid"] != null && Request.QueryString["userid"] != "undefined")
            {
                UserBusiness UB = new UserBusiness();
                UB.ValidateLogIn(long.Parse(Request.QueryString["userid"]), ClientType.Web);
            }


            if (Membership.MembershipHelper.CurrentUser != null)
            {
                //Instancio un controller 
                DynamicButtonController dynamicBtnController = new DynamicButtonController();
                //Pido la vista
                DynamicButtonPartialViewBase dynBtnView = dynamicBtnController.GetViewHomeButtons(MembershipHelper.CurrentUser);
                //La agrego
                if (dynBtnView != null)
                {
                    emptyMessage.Visible = false;
                    pnl.Controls.Add(dynBtnView);
                }
                else
                    emptyMessage.Visible = true;
            }
            else
            {
                //  ClientScript.RegisterClientScriptBlock(this.GetType(),"RedirectToLoginScript", "RedirectOpenerToLogin();",true);
            }



            try
            {

                //Ezequiel - 01/02/10 - Creo variable de ejecucion de workflow y se la paso al taskheader.
                Zamba.Framework.WFExecution WFExec = new Zamba.Framework.WFExecution(Zamba.Membership.MembershipHelper.CurrentUser);
                WFExec._haceralgoEvent -= UC_WFExecution.HandleWFExecutionPendingEvents;
                WFExec._haceralgoEvent += UC_WFExecution.HandleWFExecutionPendingEvents;
                // UC_WFExecution.RefreshTask += RefreshTask;
                UC_WFExecution.OpenFormInWindow -= openFormInWindow;
                UC_WFExecution.OpenFormInWindow += openFormInWindow;
                UC_WFExecution.RefreshWFTree -= GenerateWfRefreshJs;
                UC_WFExecution.RefreshWFTree += GenerateWfRefreshJs;
                this.UC_WFExecution.WFExec = WFExec;

                string TaskIDStr = this.UC_WFExecution.hdnCurrTaskID.Value;
                Int64 TaskID = 0;

                if (TaskIDStr != null && TaskIDStr.Length > 0 && Int64.TryParse(TaskIDStr, out TaskID))
                {
                    this.UC_WFExecution.TaskID = TaskID;
                }
                else
                {
                    this.UC_WFExecution.TaskID = 0;                   
                }


            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }






        }



        private void GenerateCloseTaskJs(Int64 taskID)
        {
            try
            {
                Response.Write("<script language='javascript'> { parent.CloseTask(" + taskID + "),false;}</script>");
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
        }

        /// <summary>
        /// Actualizo el arbol de tareas
        /// </summary>
        /// <remarks></remarks>
        private void GenerateWfRefreshJs()
        {
            try
            {
                Response.Write("<script language='javascript'> { parent.RefreshGrid('tareas');}</script>");
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
        }

        public Boolean IsStartupScriptRegistered { get; set; }

        private void openFormInWindow(ITaskResult task)
        {
            StringBuilder sbScript = new StringBuilder();
            //string url = ;
            Zamba.Core.WF.WF.WFTaskBusiness WFTB = new Zamba.Core.WF.WF.WFTaskBusiness();
            //string host = Request.Url.Host;
            string url = "'/bpm/Views/WF/TaskViewer.aspx?doctypeid=" + task.DocTypeId.ToString() + "&taskid=" + task.TaskId.ToString() + "&docid=" + task.ID.ToString() + "&mode=&UserID=" + Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString() + "#Zamba/'";

            sbScript.Append("$(document).ready(function(){");
            sbScript.Append("window.open(");
            sbScript.Append(url.ToString());
            sbScript.Append(");");
            sbScript.Append("});");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenwinScript", sbScript.ToString(), true);
            IsStartupScriptRegistered = true;

        }




        /// <summary>
        /// Ejecuta las reglas desde la master
        /// </summary>
        /// <param name="ruleId">ID de la regla que se quiere ejecutar</param>
        /// <param name="results">Tareas a ejecutar</param>
        /// <remarks></remarks>
        private void ExecuteRule(Int64 ruleId, List<ITaskResult> results)
        {
            try
            {
                Zamba.Core.RuleExecutionResult ExecutionResult = new Zamba.Core.RuleExecutionResult();
                Zamba.Core.RulePendingEvents PendigEvent = new Zamba.Core.RulePendingEvents();
                List<Int64> ExecutedIDs = new List<Int64>();
                List<Int64> PendingChildRules = new List<Int64>();
                Hashtable Params = new Hashtable();
                bool RefreshRule = false;

                if (UC_WFExecution.TaskIdsToRefresh == null)
                {
                    UC_WFExecution.TaskIdsToRefresh = new List<long>();
                }

                //  ZTrace.WriteLineIf(ZTrace.IsVerbose, "UC_WFExecution.WFExec.ExecuteRule" + ruleId.ToString());

                this.UC_WFExecution.WFExec.ExecuteRule(ruleId, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, UC_WFExecution.TaskIdsToRefresh, false);
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
        }

        protected void hdnsender_Click(object sender, EventArgs e)
        {
            try
            {
                long RuleId = long.Parse(this.Request["__EVENTARGUMENT"]);

                List<Zamba.Core.ITaskResult> results = new List<Zamba.Core.ITaskResult>();




                Session["ExecutingRule"] = RuleId;
                long stepid = 0;
                DocType dt = new DocType(0);
                ITaskResult task = new TaskResult(ref stepid, 0, 0, dt, "Home", 0, 0, TaskStates.Asignada, null, null, Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString());
                results.Add(task);

                if (RuleId > 0)
                    ExecuteRule(RuleId, results);



            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }
}