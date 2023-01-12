using System.Collections.Generic;
using System.Web.Mvc;
using Marsh.Bussines;
using Marsh.Services;

namespace IntranetMarshMVC.Controllers
{
    public class LibreriaController : Controller
    {
        // GET: /Libreria/
        public ActionResult Index()
        {
            ViewData["usuarios"] = new UsuariosServices().GetUsersArrayList();
            ViewData["productos"] = ArticuloLibreriaServices.Listar();

            return View("Index", ViewData);
        }

        // Guarda los datos y productos del pedido
        public bool EnviarPedido(string usuario, IList<ArticuloLibreriaBussines> articulos)
        {
            SolicitudLibreriaBussines solicitud = new SolicitudLibreriaBussines();

            solicitud.Usuario = usuario;
            solicitud.ListaArticulos = articulos;

            return solicitud.GuardarSolicitud();
        }
    }
}
