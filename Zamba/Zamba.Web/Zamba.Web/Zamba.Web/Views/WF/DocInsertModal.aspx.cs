using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using Zamba.Core;
using Zamba.Core.WF.WF;
using Zamba.Framework;
using Zamba.Membership;

public partial class Views_WF_DocInsertModal : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Int64 UrlUserId = 0;
        string userToken = string.Empty;
        try
        {
            // se optiene el user de la url
            if (!string.IsNullOrEmpty(Request.QueryString["user"]))
            {
                UrlUserId = Convert.ToInt64(Request.QueryString["user"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["userid"]))
            {
                UrlUserId = Convert.ToInt64(Request.QueryString["userid"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["userId"]))
            {
                UrlUserId = Convert.ToInt64(Request.QueryString["userId"]);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["t"]))
            {
                userToken = Request.QueryString["t"].ToString().Trim();
            }

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "UrlUserId : " + UrlUserId);

            //HttpContext.Current.Session["User"] = null;

            Results_Business RB = new Results_Business();

            if (MembershipHelper.CurrentUser != null && Response.IsClientConnected)
            {

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 1 - El currentUser no es null ");
                Int64 userid = MembershipHelper.CurrentUser.ID;

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "userid :  " + userid);
                //Se evalua si el usuario de la url es diferente al del MembershipHelper para hacer relogin

                // si el usuario es diferente el usuario se sacar del currentuser y se relogea, vuelve a pasar y valida ya q el usuario esta bien
                bool reloadModalLogin = userid != UrlUserId ? true : false;

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "reloadModalLogin :  " + reloadModalLogin);

                bool isActiveSession = true;

                if (!reloadModalLogin)
                {
                    isActiveSession = RB.getValidateActiveSession(userid, userToken);
                }


                ZTrace.WriteLineIf(ZTrace.IsVerbose, "isActiveSession :  " + isActiveSession);


                if (!reloadModalLogin && !isActiveSession)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 1.1 - isActiveSession es false se dispara modal ");
                    Modal_login(reloadModalLogin);
                }
            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 2 - El currentUser  es null ");
                bool isActiveSession = RB.getValidateActiveSession(UrlUserId, userToken);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "isActiveSession :  " + isActiveSession);

                if (isActiveSession)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 2.1 - isActiveSession  es true, reestablezo la session ");
                    UserBusiness ub = new UserBusiness();
                    ub.ValidateLogIn(UrlUserId, ClientType.Web);
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 2.1.1 -  valido el usuario creado" + MembershipHelper.CurrentUser.ID);
                }
                else
                {

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 2.2 - isActiveSession  es false, se dispara modal ");
                    Modal_login(true);
                }
            }
        }
        catch (global::System.Exception ex)
        {
            FormsAuthentication.RedirectToLoginPage();
            return;
        }

    }

    public void Modal_login(Boolean reloadModalLogin)
    {
        string loginUrl = FormsAuthentication.LoginUrl.ToString();
        string script = "$(document).ready(function() {  var linkSrc = location.origin.trim() + '" + loginUrl.Replace(".aspx", "").Trim() + "?showModal=true&reloadLogin=" + reloadModalLogin + "';  document.getElementById('iframeModalLogin').src = linkSrc; $('#modalLogin').modal('show');});";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "showModalLogin", script, true);
    }

    protected void Page_PreLoad(object sender, System.EventArgs e)
    {
        try
        {
            FormBrowser.IsShowing = false;

            FormBrowser.ExecuteRule -= ExecuteRule;
            FormBrowser.ExecuteRule += ExecuteRule;

            WFExecution WFExec = new WFExecution(Zamba.Membership.MembershipHelper.CurrentUser);
            WFExec._haceralgoEvent -= UC_WFExecution.HandleWFExecutionPendingEvents;
            WFExec._haceralgoEvent += UC_WFExecution.HandleWFExecutionPendingEvents;
            this.UC_WFExecution.WFExec = WFExec;
            //this.UC_WFExecution.TaskID = Task_ID;
            //WFTaskBusiness wFTaskBusiness = new WFTaskBusiness();
            //wFTaskBusiness.UpdateTaskState(Task_ID, (Int64)TaskStates.Ejecucion);           

        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }


    }

    /// <summary>
    /// Ejecuta las reglas desde la master
    /// </summary>
    /// <param name="ruleId">ID de la regla que se quiere ejecutar</param>
    /// <param name="results">Tareas a ejecutar</param>
    /// <remarks></remarks>
    private void ExecuteRule(Int64 ruleId, List<Zamba.Core.ITaskResult> results)
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


}
