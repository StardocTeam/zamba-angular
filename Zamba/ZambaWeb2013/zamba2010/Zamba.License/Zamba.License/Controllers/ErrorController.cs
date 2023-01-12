using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zamba.License.Models;

namespace Zamba.License.Controllers
{
    public partial class ErrorController : Controller
    {
        //
        // GET: /Error/

        public virtual ActionResult Index()
        {
            return View();
        }
    }
}
