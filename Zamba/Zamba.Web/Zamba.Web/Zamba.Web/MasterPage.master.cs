
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;
using Zamba.Membership;
using Zamba.Services;

namespace Zamba.Web
{
    partial class MasterPage : System.Web.UI.MasterPage
    {
        public bool loadChatZmb { get; set; }
        public bool loadGlobalSearch { get; set; }

        public string WebServiceResultsURL;
       

        protected void Page_Init(object sender, System.EventArgs e)
        {
            WebServiceResultsURL = ZOptBusiness.GetValueOrDefault("WSResultsUrl", "http://www.zamba.com.ar/zambastardocWS");
            //icono del titulo
            lnkWebIcon.Attributes.Add("href", "~/App_Themes/" + Page.Theme + "/Images/WebIcon.jpg");

            //if (Membership.MembershipHelper.CurrentUser != null)
            //{
            //    SWorkflow Sworkflow = new SWorkflow();

            //    var WorkFlows = Sworkflow.GetUserWFIdsAndNamesWithSteps(Membership.MembershipHelper.CurrentUser.ID);

            //    if (WorkFlows != null && WorkFlows.Count > 0)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this, GetType(), "TaskCount", "$(document).ready(function () { LoadStepsCounts(); });", true);
            //    }
            //}
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            UserPreferences UP = new UserPreferences();
            try
            {

                if (Membership.MembershipHelper.CurrentUser != null)
                {
                    hdnUserId.Value = Membership.MembershipHelper.CurrentUser.ID.ToString();
                    ConnectionId.Value = Zamba.Membership.MembershipHelper.CurrentUser.ConnectionId.ToString();
                    hdnComputer.Value = Session["ComputerNameOrIP"].ToString();
                    //Page.Header.DataBind();
                    if (!Page.IsPostBack)
                    {
                        ZOptBusiness zopt = new ZOptBusiness();
                        string link = zopt.GetValue("WebHomeLink");
                        string target = zopt.GetValue("WebHomeTarget");
                        zopt = null;
                        hdnRefreshWF.Value = UP.getValue("WebRefreshWFTab", UPSections.WorkFlow, false);

                        if (string.IsNullOrEmpty(link))
                        {
                            hdnLink.Value = HttpContext.Current.Request.Url.ToString();
                        }
                        else
                        {
                            hdnLink.Value = link;
                        }

                        if (string.IsNullOrEmpty(target))
                        {
                            hdnTarget.Value = "_self";
                        }
                        else
                        {
                            hdnTarget.Value = target;
                        }
                        //Me.UC_WFExecution.TaskID = Task_ID

                        Session["ListOfTask"] = null;

                    }


                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally { UP = null; }
        }


        protected void MasterLogout(object sender, System.EventArgs e)
        {
            Response.Redirect("~/Views/Security/Logout.aspx");
        }

        public static string GetAppRootUrl(bool endSlash)
        {
            string host = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            string appRootUrl = HttpContext.Current.Request.ApplicationPath;
            if (!appRootUrl.EndsWith("/"))
            {
                //a virtual
                appRootUrl += "/";
            }
            if (!endSlash)
            {
                appRootUrl = appRootUrl.Substring(0, appRootUrl.Length - 1);
            }
            return host + appRootUrl;
        }

        public static string GetShowSessionRefreshLog()
        {
            SZOptBusiness zopt = new SZOptBusiness();
            string val = zopt.GetValue("ShowSessionRefreshLog");
            if (string.IsNullOrEmpty(val))
            {
                return "false";
            }
            else
            {
                return val.ToLower();
            }
        }
 
        //public static IHtmlString GetJqueryCoreScript()
        //{
        //    return Zamba.Web.Helpers.Tools.GetJqueryCoreScript(HttpContext.Current.Request);
        //}

        //public static string GetIsOldBrowser()
        //{
        //    return Zamba.Web.Helpers.Tools.GetIsOldBrowser(HttpContext.Current.Request).ToString().ToLower();
        //}
        public MasterPage()
        {
            Load += Page_Load;
            Init += Page_Init;
        }


    }
}
