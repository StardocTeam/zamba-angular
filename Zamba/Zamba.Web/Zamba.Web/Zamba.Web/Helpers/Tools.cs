using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Optimization;

namespace Zamba.Web.Helpers
{
    public class Tools
    {
        public DataTable FilterDT(DataTable dt)
        {
            try
            {
                string filter = WebConfigurationManager.AppSettings["FilterDT"];

                if (!string.IsNullOrEmpty(filter))
                {
                    DataView dv = new DataView(dt);
                    dv.RowFilter = filter;
                    return dv.ToTable();
                }
            }
            catch
            {
                //Zamba.AppBlock.ZException.Log(ex);
            }

            return dt;
        }

        //public static IHtmlString GetJqueryCoreScript(HttpRequest request)
        //{
        //        return Scripts.Render("~/bundles/jqueryCore");
        //}

        public static IHtmlString RegisterThemeBundles(HttpRequest request)
        {
            String BundleName = BundleConfig.RegisterThemeBundles(System.Web.Optimization.BundleTable.Bundles);
            if (String.IsNullOrEmpty(BundleName) == false)
                return Styles.Render(BundleName);
            else
                return new HtmlString(string.Empty);
        }

        public static bool GetIsOldBrowser(HttpRequest request)
        {
            string userAgent = request.UserAgent; //entire UA string
            string browser = request.Browser.Type; //Browser name and Major Version #

            //Compatibilidad para IE11
            if (request.UserAgent.IndexOf("Trident/7.0") > -1)
                return false;

            if (userAgent.Contains("Trident/") && request.Browser.MajorVersion <= 8 && request.Browser.Browser == "IE")
            {
                return true;
            }

            return false;
        }

        public static string GetProtocol(HttpRequest request)
        {
            string protocol = String.Empty;
            switch (request.Url.Scheme)
            {
                case "http":
                    protocol = "http://";
                    break;
                case "https":
                    protocol = "https://";
                    break;
                default:
                    break;
            }
            return protocol;
        }

        public static string GetProtocol(HttpRequestMessage request)
        {
            string protocol = String.Empty;
            switch (request.Properties.ToString())//.Url.Scheme)
            {
                case "http":
                    protocol = "http://";
                    break;
                case "https":
                    protocol = "https://";
                    break;
                default:
                    break;
            }
            return protocol;
        }

    }
}