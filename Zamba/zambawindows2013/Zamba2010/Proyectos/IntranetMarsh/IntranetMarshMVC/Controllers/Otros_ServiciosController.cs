using System.Web.Mvc;
using Marsh.Services;

namespace IntranetMarshMVC.Controllers
{
    public class Otros_ServiciosController : Controller
    {
        //
        // GET: /Otros_Servicios/

        public ActionResult Index()
        {
            ViewData["usuarios"] = new UsuariosServices().GetUsersArrayList();
            ViewData["servicios"] = new OtrosServiciosServices().getListaServicios();

            return View("Index", ViewData);
        }

        // Guarda los datos de la solicitud
        public bool EnviarSolicitud(long idservicio, string usuario, string mensaje)
        {
            return new OtrosServiciosServices().EnviarSolicitud(idservicio, usuario, mensaje);
        }
    }
}
