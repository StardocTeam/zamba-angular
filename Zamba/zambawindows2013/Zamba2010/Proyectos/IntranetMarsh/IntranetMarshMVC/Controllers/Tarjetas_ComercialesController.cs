using System.Web.Mvc;
using Marsh.Bussines;
using Marsh.Services;

namespace IntranetMarshMVC.Controllers
{
    public class Tarjetas_ComercialesController : Controller
    {
        //
        // GET: /Tarjetas_Comerciales/

        public ActionResult Index()
        {
            ViewData["usuarios"] = new UsuariosServices().GetUsersArrayList();
     
            return View("Index", ViewData);
        }
        
        public JsonResult getUserData(string nombre_apellido)
        {
            return this.Json(new UsuarioBussines().getUserData(nombre_apellido));
        }

        public bool GuardarSolicitud(string usuario, string cargo, string sector, string telefono, string email)
        {
            TarjetasComercialesBussines tj = new TarjetasComercialesBussines();

            tj.Usuario = usuario;
            tj.Cargo = cargo;
            tj.Sector = sector;
            tj.Telefono = telefono;
            tj.Email = email;

            return tj.GuardarSolicitud();
        }
    }
}
