using System.Web.Mvc;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class EditorController : Controller
    {
        [Demo]
        public ActionResult Index()
        {
            return View();
        }

        [Demo]
        public ActionResult ImageBrowser()
        {
            return View();
        }
    }
}