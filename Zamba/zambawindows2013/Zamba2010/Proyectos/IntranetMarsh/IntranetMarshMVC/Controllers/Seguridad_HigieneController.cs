using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Marsh.Bussines;

namespace IntranetMarshMVC.Controllers
{
    public class Seguridad_HigieneController : Controller
    {
        //
        // GET: /SeguridadHigiene/

        public ActionResult Index(string file)
        {
            FormularioBussines form = new FormularioBussines();

            form.File = file;

            return View("Index", form);
        }
    }
}
