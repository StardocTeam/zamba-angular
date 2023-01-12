using System.Web;
using System.Web.Optimization;

namespace Zamba.Help
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            System.Web.Optimization.BundleTable.EnableOptimizations = false;

            #region Style
            bundles.Add(new StyleBundle("~/Content/css").Include(
                               "~/Content/bootstrap.css",
                               "~/Content/bootstrap-treeview.css",
                               "~/Content/site.css",
                               "~/Content/toastr.css",
                               "~/Content/jquery.splitter.css"
                               ));
            #endregion

            #region Scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                        //"~/Scripts/jquery-3.1.1.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                      "~/Scripts/jquery-ui-1.11.4.js",
                      "~/Scripts/jquery.ui.widget.js",
                      "~/Scripts/jquery.splitter-0.14.0.js"
                    ));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-treeview.js",
                      "~/Scripts/respond.js"));
                        
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            ));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                    "~/Scripts/angular.min.js",
                    "~/Scripts/angular-route.min.js",
                    "~/Scripts/angular-sanitize.min.js",
                    "~/Scripts/angular-ui.min.js",
                    "~/Scripts/angular-ui/ui-bootstrap.min.js",
                    "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                    "~/Scripts/angular-ui.min.js",
                    "~/Scripts/angular-block-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
                  //"~/Scripts/tinymce/tinymce.js",
                  "~/Scripts/tinymce/helper.tinymce.js"
                  ));
            bundles.Add(new ScriptBundle("~/bundles/NGScripts").Include(              
                "~/scripts/NG/tinymceNG.js",
               "~/scripts/NG/helperNG.js"               
               ));
            bundles.Add(new ScriptBundle("~/bundles/Scripts").Include(
           "~/scripts/helperfn.js",
                "~/scripts/helperscript.js"
          ));
            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(               
               "~/scripts/toastr.min.js",
               "~/scripts/bootbox.min.js"
          ));
            #endregion

//#if DEBUG
//            foreach (var bundle in BundleTable.Bundles)
//            {
//                bundle.Transforms.Clear();
//            }
//#endif

        }
    }
}
