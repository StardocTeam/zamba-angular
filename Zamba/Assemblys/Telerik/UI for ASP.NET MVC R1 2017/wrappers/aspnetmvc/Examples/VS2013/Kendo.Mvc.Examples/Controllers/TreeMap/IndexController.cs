using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Examples.Models;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class TreeMapController : Controller
    {
        [Demo]
        public ActionResult Index()
        {
            return View();
        }
    }
}