﻿using System;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Web.Optimization;

/// <summary>
/// Summary description for Tools
/// </summary>
public static class Tools
{
    public static DataTable FilterDT(DataTable dt)
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

    public static IHtmlString GetJqueryCoreScript(HttpRequest request)
    {
        if (GetIsOldBrowser(request))
            return Scripts.Render("~/bundles/jqueryOldBrowsers");
        else
            return Scripts.Render("~/bundles/jqueryCore");
    }

    public static IHtmlString RegisterThemeBundles(HttpRequest request)
    {
     String BundleName =   BundleConfig.RegisterThemeBundles(System.Web.Optimization.BundleTable.Bundles);
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
                protocol="http://";
                break;
            case "https":
                protocol= "https://";
                break;
            default:
               break;
        }
        return protocol;
    }
}