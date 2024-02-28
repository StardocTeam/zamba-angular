using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZambaWeb.RestApi.Controllers.Dashboard
{
    public static class DashboardRoutesHelper
    {
        public static string ApiServerName { get; set; } = System.Configuration.ConfigurationManager.AppSettings["RRHHApiServerName"];
        public static string ApiServerPort { get; set; } = System.Configuration.ConfigurationManager.AppSettings["RRHHApiServerPort"];

        public static string validate
        {
            get { return "http://" + ApiServerName + ":" + ApiServerPort + "/#/passport/validate"; }
        }

        public static string reset
        {
            //get { return "http://" + ApiServerName + ":" + ApiServerPort + "/#/passport/validate"; }
            get { return "http://" + ApiServerName + ":" + ApiServerPort + "/#/passport/changepassword"; }
        }
    }
}