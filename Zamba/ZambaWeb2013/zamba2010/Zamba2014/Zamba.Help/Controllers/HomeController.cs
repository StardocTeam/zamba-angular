using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zamba.Membership;

namespace Zamba.Help.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (MembershipHelper.CurrentUser != null)
            {
                ViewBag.Title = "Zamba Helper";

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Request.RawUrl.ToString() });
            }

        }
        


        public ActionResult Create()
        {

            if (MembershipHelper.CurrentUser != null)
            {
                ViewBag.Title = "Nuevo helper";

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Request.RawUrl.ToString() });
            }

        } 
        public ActionResult Edit()
        {
            if (MembershipHelper.CurrentUser != null)
            {
                ViewBag.Title = "Editar Helper";

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Request.RawUrl.ToString() });
            }

        }
    }
}
