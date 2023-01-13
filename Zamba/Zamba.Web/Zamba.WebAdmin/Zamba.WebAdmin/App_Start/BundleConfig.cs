using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace Zamba.WebAdmin
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                  "~/Scripts/angular.min.js",
                  "~/Scripts/angular-route.min.js",
                  "~/Scripts/angular-sanitize.min.js",
                  "~/Scripts/angular-ui.min.js",
                  "~/Scripts/angular-ui/ui-bootstrap.min.js",
                  "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                  "~/Scripts/angular-ui.min.js",
                  "~/Scripts/angular-block-ui.js",
                  "~/Scripts/angular-touch.min.js",
                    "~/Scripts/ui-grid.js"
                  ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
             "~/Scripts/jquery-ui-1.11.4.js",
             "~/Scripts/jquery.ui.widget.js"
           ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
          "~/scripts/toastr.js",
          "~/scripts/bootbox.min.js"
     ));
            bundles.Add(new ScriptBundle("~/bundles/Scripts").Include(
      
     ));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
               //"~/Scripts/tinymce/tinymce.js",
               "~/Scripts/tinymce/webadmin.tinymce.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/NGScripts").Include(
     "~/scripts/NG/tinymceNG.js",
    "~/scripts/NG/webadminNG.js"
    ));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/app/home.viewmodel.js",
                "~/Scripts/app/_run.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                   "~/Scripts/bootstrap-treeview.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(             
                         "~/Content/bootstrap.css",
                               "~/Content/bootstrap-treeview.css",
                               "~/Content/site.css",
                               "~/Content/toastr.css",
                               "~/Content/ui-grid.css"
                 ));
        }
    }
}
