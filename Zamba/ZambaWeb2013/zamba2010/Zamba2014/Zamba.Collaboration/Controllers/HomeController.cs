using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zamba.Collaboration.Controllers
{
    public class HomeController : Controller
    {
        //[Authorize]
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Zamba Collaboration®";

            return View();
        }
    }
}
