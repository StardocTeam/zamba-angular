using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Examples.Models;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class Bar_ChartsController : Controller
    {
        [Demo]
        public ActionResult Pan_And_Zoom()
        {
            return View(ChartDataRepository.PanAndZoomData());
        }
    }
}
