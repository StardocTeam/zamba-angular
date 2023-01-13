using System.Web.Mvc;
using Marsh.Services;

namespace IntranetMarshMVC.Controllers
{
    public class DesperfectosController : Controller
    {
        public ActionResult Index()
        {
            return View("Index", new UsuariosServices().GetUsersArrayList());
        }

        public bool Enviar(string usuario, string lugar, string descripcion)
        {
            return new DesperfectoServices().Enviar(usuario, lugar, descripcion);
        }
    }
}
