using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zamba.License.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            //si sesion es nula
            //  redirect accountcontroller
            //else
            //  redirect licensecontroller

            return RedirectToAction(MVC.License.Index());
        }
    }
}
