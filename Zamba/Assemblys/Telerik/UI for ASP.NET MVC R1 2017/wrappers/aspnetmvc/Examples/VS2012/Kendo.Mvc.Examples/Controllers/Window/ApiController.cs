using System.Web.Mvc;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class WindowController : Controller
    {
        [Demo]
        public ActionResult Api()
        {
            return View();
        }
    }
}