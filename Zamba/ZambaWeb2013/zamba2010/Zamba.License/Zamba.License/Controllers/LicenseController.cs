using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zamba.LicenseBusiness;
using Zamba.LicenseCore;
using Zamba.License.Models;
using Zamba.AppBlock;

namespace Zamba.License.Controllers
{
    public partial class LicenseController : Controller
    {
        //
        // GET: /License/

        public virtual ActionResult Index()
        {
            //Se obtienen las licencias configuradas, disponibles y consumidas
            LicenseBusiness.LicenseBusiness lb = new LicenseBusiness.LicenseBusiness();
            List<ILicense> licenses = null;

            try
            {
                //Se obtienen las licencias
                licenses = lb.GetLicenses();
            }
            catch (Exception ex)
            {
                ZException.Log(ex);
            }
            finally
            {
                lb = null;
            }

            //Verifica si existen licencias cargadas
            if (licenses != null && licenses.Count > 0)
            {
                //Se completa el modelo de licencias
                List<LicenseModel> model = new List<LicenseModel>();

                foreach (var l in licenses)
                {
                    model.Add(new LicenseModel(l));
                }

                return View(model);
            }
            else
            {
                TempData["ErrorDescription"] = "Error al cargar el listado de licencias del cliente.";
                return RedirectToAction(MVC.Error.Index());
            }
        }
    }
}
