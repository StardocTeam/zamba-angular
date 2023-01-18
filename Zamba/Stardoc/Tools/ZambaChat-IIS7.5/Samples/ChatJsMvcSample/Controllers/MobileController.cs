using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatJsMvcSample.Controllers
{
    public class MobileController : Controller
    {
        // GET: Mobile
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult Join(string enc)
        {
            try
            {
                Session["userId"] = enc;
            }
            catch { }
            return View();
        }

    }
}