namespace Kendo.Mvc.Examples.Controllers
{
    using System.Web.Mvc;
    using Kendo.Mvc.Examples.Models.Scheduler;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public partial class SchedulerController
    {
        [Demo]
        public ActionResult Move_Resize()
        {
            return View();
        }
    }
}
