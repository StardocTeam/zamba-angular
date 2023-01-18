using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ApiController : Controller
    {
        //
        // GET: /Api/Categories
        public JsonResult Categories()
        {
            var context = new ZambaEntities();
       //     return Json(context.WFWorkflow.Name.Select(c => c.Name), JsonRequestBehavior.AllowGet);
            return null;
        }

        //
        // GET: /Api/Years
        public JsonResult Years()
        {
            return Json(new[] { "1995", "1996", "1997", "1998" }, JsonRequestBehavior.AllowGet);
        }
    }
}
