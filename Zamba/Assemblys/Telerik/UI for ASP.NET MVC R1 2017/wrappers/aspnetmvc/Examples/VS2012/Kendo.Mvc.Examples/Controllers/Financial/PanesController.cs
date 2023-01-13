using System;
using System.Linq;
using System.Web.Mvc;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class FinancialController : Controller
    {
        [Demo]
        public ActionResult Panes()
        {
            return View();
        }
    }
}