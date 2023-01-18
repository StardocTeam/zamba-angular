using System.Web.Mvc;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class GridController : Controller
    {
        [Demo]
        public ActionResult Local_Data_Binding()
        {
            var model = productService.Read();

            return View(model);
        }        
    }
}