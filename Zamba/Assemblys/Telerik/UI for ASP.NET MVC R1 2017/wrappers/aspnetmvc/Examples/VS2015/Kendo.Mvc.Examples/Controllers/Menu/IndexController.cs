using System;
using System.Linq;
using System.Web.Mvc;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class MenuController : Controller
    {
        [Demo]
        public ActionResult Index()
        {
            return View();
        }
    }
}
