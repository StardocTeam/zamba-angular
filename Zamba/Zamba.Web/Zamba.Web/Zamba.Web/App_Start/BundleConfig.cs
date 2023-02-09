using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
using Zamba.Core;

namespace Zamba.Web
{


    public class BundleConfig
    {
        // Para obtener más información sobre la unión, visite http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region GralConfig
            bundles.IgnoreList.Clear();
            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/respond.min.js",
                    DebugPath = "~/Scripts/respond.js",
                });
            #endregion

            #region FrameworkJS
            //bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
            //               "~/Scripts/WebForms/WebForms.js",
            //               "~/Scripts/WebForms/WebUIValidation.js",
            //               "~/Scripts/WebForms/MenuStandards.js",
            //               "~/Scripts/WebForms/Focus.js",
            //               "~/Scripts/WebForms/GridView.js",
            //               "~/Scripts/WebForms/DetailsView.js",
            //               "~/Scripts/WebForms/TreeView.js",
            //               "~/Scripts/WebForms/WebParts.js"));

            //bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
            //        "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
            //        "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
            //        "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
            //        "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use la versión de desarrollo de Modernizr para desarrollar y aprender. Luego, cuando esté listo
            // para la producción, use la herramienta de creación en http://modernizr.com para elegir solo las pruebas que necesite
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-3.4.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/particles").Include(
                           "~/Scripts/Particulas/particles.js",
                            "~/Scripts/Particulas/app.js",
                              //"~/Scripts/Particulas/particles.json",
                              "~/Scripts/Particulas/lib/stats.js"
                           ));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                                    "~/Scripts/toastr.js*",
                                    "~/Scripts/sweetalert.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                        "~/Scripts/moment.min.js"
                        ));

            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/Globalize").Include(
                               "~/Scripts/globalize.js",
                               "~/Scripts/globalize.culture.es-AR.js"
                               ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryCore").Include(
                "~/Scripts/jquery-3.1.1.js"
                ));



            bundles.Add(new ScriptBundle("~/bundles/jqueryAddIns")
                .Include("~/Scripts/jquery.*")
                .Include("~/Scripts/jquery-ui-*", new CssRewriteUrlTransformWrapper())
                .Include("~/Scripts/jquery.caret.js")
.Include("~/Scripts/jquery.ba-resize.min.js").Include("~/Scripts/jquery.blockUI.js")
.Include("~/Scripts/jquery.blockUI.js")
.Include("~/Scripts/jquery.blockUI.js")
.Include("~/Scripts/jquery.bootstrap-autohidingnavbar.js")
.Include("~/Scripts/jquery.bootstrap-pureClearButton.min.js")
.Include("~/Scripts/jquery.caret.1.02.min.js")
.Include("~/Scripts/jquery.dataTables.min.js")
.Include("~/Scripts/jquery.easing.1.3.js")
.Include("~/Scripts/jquery.galleryview-3.0-dev.min.js")
.Include("~/Scripts/jquery.quicksearch.min.js")
.Include("~/Scripts/jquery.scrollTo.js")
.Include("~/Scripts/jquery.switchButton.js")
.Include("~/Scripts/jquery.tablehover.pack.js")
.Include("~/Scripts/jquery.timers-1.2.js")
.Include("~/Scripts/jquery.touchSwipe.min.js")
);

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                              "~/Scripts/jquery.unobtrusive*"
            //,"~/Scripts/jquery.validate*"
            )
            );

            bundles.Add(new ScriptBundle("~/bundles/tabber").Include(
                                    "~/Scripts/tabber*"));

            bundles.Add(new ScriptBundle("~/bundles/ZScripts").Include(
                                    "~/Scripts/Zamba.Autocomplete.js",
                                    "~/Scripts/Zamba.Autocomplete.min.js",
                                    "~/Scripts/Zamba.feeds.js",
                                    "~/Scripts/Zamba.feeds.min.js",
                                    "~/Scripts/Zamba.fn.js",
                                    "~/Scripts/Zamba.fn.min.js",
                                    "~/Scripts/Zamba.optionfields.js",
                                    "~/Scripts/Zamba.optionfields.min.js",
                                    "~/Scripts/Zamba.tables.js",
                                    "~/Scripts/Zamba.tables.min.js",
                                    "~/Scripts/Zamba.tabs.js",
                                    "~/Scripts/Zamba.tabs.min.js",
                                    "~/Scripts/Zamba.validations.js",
                                    "~/Scripts/Zamba.validations.min.js"

                                    ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.min.js")

                );
            bundles.Add(new ScriptBundle("~/bundles/bootbox")
            .Include("~/Scripts/bootbox.js")
            );
            bundles.Add(new ScriptBundle("~/bundles/angular")
              .Include("~/Scripts/angular.js",
                "~/Scripts/angular-cookies.min.js",
                "~/Scripts/angular-resource.min.js",
                "~/Scripts/angular-route.min.js")
          );



            #endregion

            #region FrameworkStyles
            bundles.Add(new StyleBundle("~/bundles/Styles/jquery").Include(
                "~/Content/Styles/jquery-ui-{version}.css", new CssRewriteUrlTransformWrapper()));

            bundles.Add(new StyleBundle("~/bundles/Styles/bootstrap")
                .Include("~/Content/bootstrap.min.*", new CssRewriteUrlTransformWrapper())
                 .Include("~/Content/bootstrap.*", new CssRewriteUrlTransformWrapper())
                );

            bundles.Add(new StyleBundle("~/bundles/Styles/ZStyles")
                .Include("~/Content/Styles/ZambaUIWebTables.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/Styles/ZambaUIWeb.css", new CssRewriteUrlTransformWrapper())
                .Include("~/Content/Styles/header.css", new CssRewriteUrlTransformWrapper())
                );

            bundles.Add(new StyleBundle("~/bundles/Styles/normalize").Include(
                   "~/Content/styles/normalize.css", new CssRewriteUrlTransformWrapper()
                        ));


            bundles.Add(new StyleBundle("~/Content/themes/base/css")
                .Include("~/Content/themes/base/jquery.ui.core.css", new CssRewriteUrlTransformWrapper())
                      .Include("~/Content/themes/base/jquery.ui.resizable.css", new CssRewriteUrlTransformWrapper())
                      .Include("~/Content/themes/base/jquery.ui.selectable.css", new CssRewriteUrlTransformWrapper())
                      .Include("~/Content/themes/base/jquery.ui.accordion.css", new CssRewriteUrlTransformWrapper())
                      .Include("~/Content/themes/base/jquery.ui.autocomplete.css", new CssRewriteUrlTransformWrapper())
                      .Include("~/Content/themes/base/jquery.ui.button.css", new CssRewriteUrlTransformWrapper())
                      .Include("~/Content/themes/base/jquery.ui.dialog.css", new CssRewriteUrlTransformWrapper())
                      .Include("~/Content/themes/base/jquery.ui.slider.css", new CssRewriteUrlTransformWrapper())
                      .Include("~/Content/themes/base/jquery.ui.tabs.css", new CssRewriteUrlTransformWrapper())
                      .Include("~/Content/themes/base/jquery.ui.progressbar.css", new CssRewriteUrlTransformWrapper())
                      .Include("~/Content/themes/base/jquery.ui.theme.css", new CssRewriteUrlTransformWrapper())
                      );

            bundles.Add(new StyleBundle("~/Content/themes/base/datepicker")
                .Include("~/Content/themes/base/jquery.ui.datepicker.css", new CssRewriteUrlTransformWrapper()));

            bundles.Add(new StyleBundle("~/bundles/Styles/grids")
                .Include("~/Content/Styles/GridThemes/WhiteChromeGridView.css", new CssRewriteUrlTransformWrapper()));

            bundles.Add(new StyleBundle("~/bundles/Styles/toastr")
                .Include("~/Content/toastr.css"));

            bundles.Add(new StyleBundle("~/bundles/Styles/search")
                .Include("~/Scripts/ng-embed/ng-embed.min.css"));
            #endregion

            #region MasterScripts
            bundles.Add(new StyleBundle("~/bundles/Styles/popupmaster").Include(
                           "~/Content/Styles/thickbox.css",
                         "~/Content/Styles/demo_table.css",
                 "~/Content/Styles/demo_page.css"
           ));

            bundles.Add(new StyleBundle("~/bundles/Styles/master").Include(
                "~/Content/dropzone.css",
              "~/Content/Styles/tabber.css",
                "~/Content/Styles/jquery.galleryview-3.0-dev.css"
              ));

            bundles.Add(new StyleBundle("~/bundles/Styles/masterblankStyles").Include(
                  "~/Content/dropzone.css",
                "~/Content/Styles/jquery.galleryview-3.0-dev.css",
                "~/Content/Styles/demo_table.css",
                "~/images/UI_blue/DroplistFilter.css",
                "~/css/tabber.css",
                 "~/Content/Styles/jquery.dataTables.css"
             ));




            bundles.Add(new ScriptBundle("~/bundles/Scripts/masterblankScripts").Include(
                "~/Scripts/DroplistFilter.js",
                "~/Scripts/toastr.js",
                 "~/Scripts/sweetalert.min.js",
                "~/Scripts/ChequearIndicePorValorErroneo.js",
                "~/Scripts/jquery.dataTables.js",
                "~/Scripts/jsontable.js"
             ));
            bundles.Add(new ScriptBundle("~/bundles/Scripts/DropFiles").Include(
                  "~/DropFiles/dropfiles.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/HelpModule").Include(
               "~/Scripts/helperscript.js"
            ));
            bundles.Add(new ScriptBundle("~/scripts/debbug").Include(
                "~/Scripts/debbug.js"
            ));
            bundles.Add(new ScriptBundle("~/scripts/token").Include(
        "~/Scripts/token.js"
    ));
            #endregion

            #region ChatScripts
            bundles.Add(new ScriptBundle("~/bundles/ChatJS").Include(
               "~/ChatJs/Scripts/jquery.signalR-2.2.0.min.js",
               "~/ChatJs/Scripts/zambachat.js"
               ));

            bundles.Add(new StyleBundle("~/bundles/ChatStyles")
                .Include("~/ChatJs/Styles/jquery.chatjs.css", new CssRewriteUrlTransformWrapper())
                .Include("~/ChatJs/Bootstrap/css/bootstrap.min.css", new CssRewriteUrlTransformWrapper())
                );
            #endregion

            #region SearchZamba
            bundles.Add(new ScriptBundle("~/bundles/globalsearch").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-sanitize.min.js",
                "~/Scripts/angular-animate.min.js",
                "~/Scripts/ng-embed/ng-embed.min.js",
                "~/Scripts/typeahead.bundle.js",
                "~/Scripts/handlebars-v2.0.0.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/ui-bootstrap-tpls-0.12.0.min.js",
                "~/Scripts/bootstrap-waitingfor.js",
                "~/Scripts/modernizr-custom.js",
                "~/Scripts/bootbox.js",
                "~/Scripts/underscore.min.js",
                "~/Scripts/lodash.min.js",
                "~/GlobalSearch/services/angular-local-storage.min.js",

                "~/GlobalSearch/search/searchbox-loader.js?v=165",
                "~/GlobalSearch/services/authService.js?v=165",
                "~/GlobalSearch/services/authInterceptorService.js?v=165",
                "~/GlobalSearch/search/search-directives.js?v=165",
                "~/Scripts/app/_common/forms/directives/input/smartDatepicker.js",
                "~/Scripts/app/search/zamba.search.js?v=165",
                "~/GlobalSearch/scripts/zambasearch.js?v=165"
            ));
            #endregion

            #region KendoUI


            bundles.Add(new StyleBundle("~/bundles/Styles/kendo").Include(

                      "~/Scripts/KendoUI/styles/kendo.common.min.css",
                   "~/scripts/kendoui/styles/kendo.metro.min.css",
                   "~/Scripts/KendoUI/styles/kendo.metro.mobile.min.css",
                   "~/Scripts/KendoUI/styles/kendo.dataviz.default.min.css",
                   "~/Scripts/KendoUI/styles/kendo.dataviz.silver.min.css",
                   "~/Scripts/KendoUI/styles/kendo.mobile.all.min.css",
                   "~/Scripts/KendoUI/styles/kendo.rtl.min.css",
                   "~/Scripts/KendoUI/styles/kendo.silver.min.css"

                )
//.Include("~/scripts/kendoui/styles/kendo.common.min.css", new CssRewriteUrlTransformWrapper())
//.Include("~/scripts/kendoui/styles/kendo.rtl.min.css", new CssRewriteUrlTransformWrapper())
//.Include("~/scripts/kendoui/styles/kendo.flat.min.css", new CssRewriteUrlTransformWrapper())
//.Include("~/scripts/kendoui/styles/kendo.default.min.css", new CssRewriteUrlTransformWrapper())
//.Include("~/scripts/kendoui/styles/kendo.dataviz.min.css", new CssRewriteUrlTransformWrapper())
//.Include("~/scripts/kendoui/styles/kendo.dataviz.default.min.css", new CssRewriteUrlTransformWrapper())
//.Include("~/scripts/kendoui/styles/kendo.mobile.all.min.css", new CssRewriteUrlTransformWrapper())
//.Include("~/scripts/kendoui/styles/kendo.silver.min.css", new CssRewriteUrlTransformWrapper())

// .Include("~/scripts/kendoui/styles/kendo.common.min.css", new CssRewriteUrlTransformWrapper())
//.Include("~/scripts/kendoui/styles/kendo.rtl.min.css", new CssRewriteUrlTransformWrapper())
//.Include("~/scripts/kendoui/styles/kendo.flat.min.css", new CssRewriteUrlTransformWrapper())
//.Include("~/scripts/kendoui/styles/kendo.default.min.css", new CssRewriteUrlTransformWrapper())
//.Include("~/scripts/kendoui/styles/kendo.dataviz.min.css", new CssRewriteUrlTransformWrapper())
//.Include("~/scripts/kendoui/styles/kendo.dataviz.default.min.css", new CssRewriteUrlTransformWrapper())
//.Include("~/scripts/kendoui/styles/kendo.mobile.all.min.css", new CssRewriteUrlTransformWrapper())
//.Include("~/scripts/kendoui/styles/kendo.silver.min.css", new CssRewriteUrlTransformWrapper())
);

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                //.Include("~/scripts/kendoui/js/kendo.all.min.js")
                "~/Scripts/KendoUI/js/kendo.all.min.js",
                "~/Scripts/KendoUI/js/kendo.grid.min.js",
                                "~/Scripts/app/grids/kendogrid.js?v=165",
                                "~/Scripts/KendoUI/js/cultures/kendo.culture.es-AR.min.js",
                "~/scripts/jszip.min.js"
                ));
            #endregion

            #region  ZambaClients
            bundles.Add(new ScriptBundle("~/bundles/Aysa").Include(
            "~/Scripts/AysaUCScripts.js"
            ));
            #endregion
            //BundleTable.EnableOptimizations = false;
            #region Minificacion
#if (!DEBUG)
            BundleTable.EnableOptimizations = true;
            bundles.UseCdn = false;   //enable CDN support
            var cdnPath = "/fonts";
            bundles.Add(new StyleBundle("~/fonts", cdnPath));
#endif
            #endregion
        }

        #region BundleConfig
        public class CssRewriteUrlTransformWrapper : IItemTransform
        {
            public string Process(string includedVirtualPath, string input)
            {
                return new CssRewriteUrlTransform().Process("~" + VirtualPathUtility.ToAbsolute(includedVirtualPath), input);
            }
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
        #endregion
    }
}