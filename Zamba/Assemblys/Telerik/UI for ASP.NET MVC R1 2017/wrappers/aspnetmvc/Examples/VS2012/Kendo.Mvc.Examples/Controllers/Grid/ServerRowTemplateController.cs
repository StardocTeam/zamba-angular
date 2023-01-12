using System.Web.Mvc;
using System.Linq;
using Kendo.Mvc.Examples.Models;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class GridController : Controller
    {
        [Demo]
        public ActionResult ServerRowTemplate()
        {
            return View(new SampleEntities().Customers);
        }
    }
}
