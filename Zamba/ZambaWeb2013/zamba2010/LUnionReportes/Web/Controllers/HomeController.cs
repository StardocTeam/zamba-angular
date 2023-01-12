using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }


        //
        // GET: /Home/SalesData
        public ActionResult WFData(ZambaEntities model)
        {
            // Populate the DDL lists
            using (var context = new ZambaEntities())
            {
             var WFLIst = from c in context.WFWorkflow select c.Name;
            }

            return View(model);
        }

        //
        // GET: /Home/SalesDataFancy
        public ActionResult SalesDataFancy()
        {
            return View();
        }
    }
}
