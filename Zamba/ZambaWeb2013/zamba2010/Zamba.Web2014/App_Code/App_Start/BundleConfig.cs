using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
using Zamba.Core;
/// <summary>
/// Summary description for BundleConfig
/// </summary>
public class BundleConfig
{
    public static void RegisterBundles(BundleCollection bundles)
    {
        #region FrameworkJS
        bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                                "~/Scripts/toastr.js*"));

        bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                                "~/Scripts/modernizr-*"));

        bundles.Add(new ScriptBundle("~/bundles/jqueryCore").Include(
            "~/Scripts/jquery-2.2.0.min.js"));

        bundles.Add(new ScriptBundle("~/bundles/jqueryOldBrowsers").Include(
            "~/Scripts/jquery-1.*",
             "~/Scripts/respond.min.js"));

        bundles.Add(new ScriptBundle("~/bundles/jqueryAddIns").Include(
                                "~/Scripts/jquery.*",
                                "~/Scripts/jquery-ui-*",                             
                                  "~/Scripts/jquery.caret.js"));

        bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                              "~/Scripts/jquery.unobtrusive*",
            "~/Scripts/jquery.validate*"));

        bundles.Add(new ScriptBundle("~/bundles/tabber").Include(
                                "~/Scripts/tabber*"));

        bundles.Add(new ScriptBundle("~/bundles/ZScripts").Include(
                                "~/Scripts/Zamba.*"));

        bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                                "~/Scripts/bootstrap.min.js"));

        #endregion

        #region FrameworkStyles
        bundles.Add(new StyleBundle("~/bundles/Styles/jquery").Include(
            "~/Content/Styles/jquery-ui-{version}.css"));

        bundles.Add(new StyleBundle("~/bundles/Styles/bootstrap").Include(
    "~/Content/bootstrap.*"));

        bundles.Add(new StyleBundle("~/bundles/Styles/ZStyles").Include(
            "~/Content/Styles/ZambaUIWebTables.css",
            "~/Content/Styles/ZambaUIWeb.css",
            "~/Content/Styles/header.css"));

        bundles.Add(new StyleBundle("~/bundles/Styles/normalize").Include(
               "~/Content/styles/normalize.css"
                    ));
        bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
               "~/Content/app.css"
                    ));

        bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                  "~/Content/themes/base/jquery.ui.core.css",
                  "~/Content/themes/base/jquery.ui.resizable.css",
                  "~/Content/themes/base/jquery.ui.selectable.css",
                  "~/Content/themes/base/jquery.ui.accordion.css",
                  "~/Content/themes/base/jquery.ui.autocomplete.css",
                  "~/Content/themes/base/jquery.ui.button.css",
                  "~/Content/themes/base/jquery.ui.dialog.css",
                  "~/Content/themes/base/jquery.ui.slider.css",
                  "~/Content/themes/base/jquery.ui.tabs.css",
                  "~/Content/themes/base/jquery.ui.progressbar.css",
                  "~/Content/themes/base/jquery.ui.theme.css"));

        bundles.Add(new StyleBundle("~/Content/themes/base/datepicker").Include(
                    "~/Content/themes/base/jquery.ui.datepicker.css"));

   bundles.Add(new StyleBundle("~/bundles/Styles/grids").Include(
            "~/Content/Styles/GridThemes/WhiteChromeGridView.css"));

        bundles.Add(new StyleBundle("~/bundles/Styles/toastr").Include(
    "~/Content/toastr.css"));
#endregion

        #region MasterScripts
        bundles.Add(new StyleBundle("~/bundles/Styles/popupmaster").Include(
                       "~/Content/Styles/thickbox.css",
              "~/Content/Styles/jquery-ui-1.8.6.css",
             //"~/Content/Styles/jq_datepicker.css",
             "~/Content/Styles/demo_table.css",
             "~/Content/Styles/demo_page.css"
       ));

        bundles.Add(new StyleBundle("~/bundles/Styles/master").Include(
          "~/Content/Styles/tabber.css",
            "~/Content/Styles/jquery.galleryview-3.0-dev.css"
          ));

        bundles.Add(new StyleBundle("~/bundles/Styles/masterblank").Include(
              "~/Content/Styles/tabber.css",
                "~/Content/Styles/jquery.galleryview-3.0-dev.css",
                "~/Content/Styles/demo_table.css",
        //"~/css/jq_datepicker.css",
        "~/images/UI_blue/DroplistFilter.css",
        "~/css/tabber.css"
         ));

        bundles.Add(new ScriptBundle("~/bundles/Scripts/masterblank").Include(
        //"~/Scripts/jq_datepicker.js",
"~/Scripts/DroplistFilter.js",
"~/Scripts/ChequearIndicePorValorErroneo.js",
"~/Scripts/json2.js",
"~/Scripts/jquery.dataTables.min.js",
"~/Scripts/jsontable.js",
        "~/Scripts/Zamba.js",
"~/Scripts/Zamba.Tables.js",
            "~/Scripts/tabber.js"
 ));
        #endregion     

        #region ChatScripts
        bundles.Add(new ScriptBundle("~/bundles/ChatJS").Include(
            "~/ChatJs/Scripts/Bootstrap/js/bootstrap.min.js",
            "~/ChatJs/Scripts/jquery.signalR-1.1.4.min.js",
             "~/ChatJs/Scripts/jquery.autosize.min.js",
            "~/ChatJs/Scripts/jquery.activity-indicator-1.0.0.min.js",
            //"~/ChatJs/Scripts/scripts.js",
            // URLBase + "signalr/hubs",
            //"~/ChatJs/Scripts/jquery.chatjs.signalradapter.js",   
            "~/ChatJs/Scripts/zambachat.js"

           ));

        bundles.Add(new StyleBundle("~/bundles/ChatStyles").Include(
             //"~/ChatJs/Styles/styles.css",
             // "~/ChatJs/Styles/jquery.chatjs.less",
             "~/ChatJs/Styles/jquery.chatjs.css",
            "~/ChatJs/Bootstrap/css/bootstrap.min.css",
            "~/ChatJs/Bootstrap/css/bootstrap-responsive.min.css"
         ));
        #endregion

        #region SearchZamba
        bundles.Add(new ScriptBundle("~/bundles/search").Include(
  "~/Scripts/app/search/zamba.search.js"));

        bundles.Add(new ScriptBundle("~/bundles/globalsearch").Include(
            "~/Scripts/angular.min.js",
            "~/Scripts/knockout-3.3.0.js",
            "~/Scripts/ko-custom-component-loaders.js",
            "~/Scripts/typeahead.bundle.js",
            "~/Scripts/handlebars-v2.0.0.js",
            "~/Scripts/bootstrap.min.js",
            "~/Scripts/ui-bootstrap-tpls-0.12.0.min.js",
            "~/Scripts/bootstrap-waitingfor.js",
            "~/Scripts/modernizr-custom.js",
            "~/Scripts/bootbox.js",
            "~/GlobalSearch/search/searchbox-loader.js",
            "~/GlobalSearch/search/searchbox.js",
            "~/GlobalSearch/grid/grid-loader.js",
            "~/GlobalSearch/grid/grid.js",          
            "~/GlobalSearch/scripts/zambasearch.js"
        //"~/GlobalSearch/scripts/jquery-dialogextend.js"
        ));
        #endregion

        #region KendoUI
        bundles.Add(new StyleBundle("~/bundles/Styles/kendo").Include(
    "~/Content/telerik.kendoui/styles/kendo.common.min.css",
    "~/Content/telerik.kendoui/styles/kendo.rtl.min.css",
    "~/Content/telerik.kendoui/styles/kendo.default.min.css",
    "~/Content/telerik.kendoui/styles/kendo.dataviz.min.css",
    "~/Content/telerik.kendoui/styles/kendo.dataviz.default.min.css"
        ));

        bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
    "~/Content/telerik.kendoui/js/kendo.all.min.js"
));
        #endregion

        #region  ZambaClients (Colocar bundles en HTML de clientes)
        bundles.Add(new ScriptBundle("~/bundles/Aysa").Include(
"~/Scripts/AysaUCScripts.js",
"~/Scripts/Aysa.js"
));

        bundles.Add(new StyleBundle("~/bundles/Styles/Aysa").Include(
"~/Content/Styles/Aysa_styles.css",
"~/Content/Styles/Aysa_styles_table.css"

 ));

        #endregion
    }    

    public static string AddThemesFolderBundles(BundleCollection bundles)
    {
        string themesFolderName = AppSettings.ThemesFolderName;
        StyleBundle currentBundle;
        string bundleName;

        string themesPath = StringExtensions.AppendBackSlash(HttpContext.Current.Server.MapPath("~\\App_Themes\\")) + themesFolderName;
        if (System.IO.Directory.Exists(themesPath))
        {
            bundleName = String.Format("~/{0}/CssBundle", themesFolderName);
            currentBundle = new StyleBundle(bundleName);

            IEnumerable<string> themeFolders = System.IO.Directory.GetDirectories(themesPath);
            foreach (string path in themeFolders)
            {
                string folderName = new System.IO.DirectoryInfo(path).Name;
                string bundleVirtualPath = String.Format("~/App_Themes/{0}/{1}/*.css", themesFolderName, folderName);

                currentBundle.Include(bundleVirtualPath);

            }
            bundles.Add(currentBundle);
            return bundleName;
        }
        return null;
    }

    public static String RegisterThemeBundles(BundleCollection bundles)
    {
        AppSettings.LoadTheme();

        return AddThemesFolderBundles(bundles);
    }   

    public static class AppSettings
    {
        public static void LoadTheme()
        {
            ZOptBusiness zoptb = new ZOptBusiness();
            String CurrentTheme = zoptb.GetValue("CurrentTheme");
            zoptb = null;
            ThemesFolderName = CurrentTheme;
        }

        public static string ThemesFolderName = "Stardoc";
    }

    public static class StringExtensions
    {
        public static string AppendBackSlash(String s)
        {
            if (s.EndsWith(@"\"))
                return s;
            return s + @"\";
        }

        public static string AppendForwardSlash(String s)
        {
            if (s.EndsWith(@"/"))
                return s;
            return s + @"/";
        }
    }
}