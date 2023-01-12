namespace Kendo.Mvc.Examples.Controllers
{
    using System.Web.Mvc;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.Examples.Models.Scheduler;

    public partial class SchedulerController
    {
        [Demo]
        public ActionResult Selection()
        {
            return View();
        }
    }
}
