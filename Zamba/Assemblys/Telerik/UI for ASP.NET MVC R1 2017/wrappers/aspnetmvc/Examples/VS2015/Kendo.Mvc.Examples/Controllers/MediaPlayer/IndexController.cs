using System.Web.Mvc;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class MediaPlayerController : Controller
    {

        public MediaPlayerController()
        {
        }

        [Demo]
        public ActionResult Index()
        {
            return View();
        }
    }
}