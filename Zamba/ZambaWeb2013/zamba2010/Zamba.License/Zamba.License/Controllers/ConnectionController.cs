using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Zamba.LicenseCore;
using Zamba.AppBlock;
using Zamba.License.Models;

namespace Zamba.License.Controllers
{
    public partial class ConnectionController : Controller
    {
        //
        // GET: /Connection/

        public virtual ActionResult Index()
        {
            //Se obtienen las conexiones
            LicenseBusiness.LicenseBusiness lb = new LicenseBusiness.LicenseBusiness();
            List<IConnection> connections = null;

            try
            {
                //Se obtienen las conexiones
                connections = lb.GetConnections();
            }
            catch (Exception ex)
            {
                ZException.Log(ex);
            }
            finally
            {
                lb = null;
            }

            //Verifica si existen conexiones
            if (connections != null)
            {
                if (connections.Count > 0)
                {
                    //Se completa el modelo de conexion
                    List<ConnectionModel> model = new List<ConnectionModel>();

                    foreach (var c in connections)
                    {
                        model.Add(new ConnectionModel(c));
                    }

                    return View(model);
                }
                else
                {
                    TempData["ErrorDescription"] = "No se encontraron conexiones a Zamba.";
                    return RedirectToAction(MVC.Error.Index());
                }
            }
            else
            {
                TempData["ErrorDescription"] = "Error al cargar el listado de conexiones.";
                return RedirectToAction(MVC.Error.Index());
            }
        }
    }
}
