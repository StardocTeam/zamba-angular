using System.Web.Mvc;
using Marsh.Services;

namespace IntranetMarshMVC.Controllers
{
    public class ComunicandonosController : Controller
    {
        //
        // GET: /Comunicandonos/

        public ActionResult Index()
        {
            return View("Index", new UsuariosServices().GetUsersArrayList());
        }

        public bool Enviar(string usuario, string mensaje)
        {
            return new SugerenciaServices().Enviar(usuario, mensaje);
        }
    }
}
