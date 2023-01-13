using Kendo.Mvc.Examples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using IOFile = System.IO.File;

namespace Kendo.Mvc.Examples.Controllers
{
    public abstract class ActionFilterAttributeBase : ActionFilterAttribute
    {
        protected List<string> examplesUrl = new List<string>();

        private static readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

        public dynamic ViewBag
        {
            get
            {
                return Controller.ViewBag;
            }
        }

        public Controller Controller { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Controller = filterContext.Controller as Controller;

            ViewBag.ShowCodeStrip = true;
            ViewBag.Product = "aspnet-mvc";
            ViewBag.NavProduct = "mvc";

            SetTheme();
            LoadNavigation();
            LoadCategories();
        }

        protected void SetTheme()
        {
            var theme = "material";
            var themeParam = Controller.HttpContext.Request.QueryString["theme"];
            var themeCookie = Controller.HttpContext.Request.Cookies["theme"];

            if (themeParam != null && Regex.IsMatch(themeParam, "[a-z0-9\\-]+", RegexOptions.IgnoreCase))
            {
                theme = themeParam;

                // update cookie
                HttpCookie cookie = new HttpCookie("theme");
                cookie.Value = theme;
                Controller.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
            else if (themeCookie != null)
            {
                theme = themeCookie.Value;
            }

            var CommonFileCookie = Controller.HttpContext.Request.Cookies["commonFile"];

            ViewBag.Theme = theme;
            ViewBag.CommonFile = CommonFileCookie == null ? "common-material" : CommonFileCookie.Value;
        }

        protected void LoadNavigation()
        {
            ViewBag.Navigation = LoadWidgets();
        }

        protected void LoadCategories()
        {
            ViewBag.WidgetCategories = LoadWidgets().GroupBy(w => w.Category).ToList();
        }

        protected IEnumerable<NavigationWidget> LoadWidgets()
        {
            var navJson = IOFile.ReadAllText(Controller.Server.MapPath("~/content/nav.json"));

            return Serializer.Deserialize<NavigationWidget[]>(navJson.Replace("$FRAMEWORK", "ASP.NET MVC"));
        }
    }
}