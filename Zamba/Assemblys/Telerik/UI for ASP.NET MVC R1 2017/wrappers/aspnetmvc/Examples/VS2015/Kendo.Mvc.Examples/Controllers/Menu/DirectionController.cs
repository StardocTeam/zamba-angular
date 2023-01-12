using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class MenuController : Controller
    {
        [Demo]
        public ActionResult Direction(String directionString)
        {
            if (string.IsNullOrEmpty(directionString))
            {
                ViewBag.Direction = "bottom";
            }
            else
            {
                ViewBag.Direction = directionString;
            }
            return View();
        }
    }
}
