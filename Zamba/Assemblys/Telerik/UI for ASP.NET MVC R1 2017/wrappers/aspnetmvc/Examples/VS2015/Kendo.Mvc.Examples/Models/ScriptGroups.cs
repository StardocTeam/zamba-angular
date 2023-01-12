using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Kendo.Mvc.Examples.Models
{
    public static class ScriptGroups
    {
        private static string jQueryPath
        {
            get {
                var CDN_ROOT = ConfigurationManager.AppSettings["CDN_ROOT"];
                if (CDN_ROOT == "$CDN_ROOT")
                {
                    return "jquery.min.js";
                }

                return string.Format(
                    "//code.jquery.com/jquery-{0}.min.js",
                    ConfigurationManager.AppSettings["JQUERY_VERSION"]
                );
            }
        }

        private static readonly string JSZipPath = "jszip.min.js";

        public static IList<string> All = new string[]
        {
            jQueryPath,
            JSZipPath,
            "kendo.all.min.js",
            "kendo.aspnetmvc.min.js",
            "kendo.timezones.min.js"
        };
    }
}
