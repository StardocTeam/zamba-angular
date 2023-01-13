using System.Web.Mvc;
using Marsh.Services;
using System.Collections.Generic;
using Marsh.Bussines;
using System.Configuration;

namespace IntranetMarshMVC.Controllers
{
    public class TelefonosController : Controller
    {
        private int _cant_contactos = int.Parse(ConfigurationSettings.AppSettings["contacto_cant_por_pagina"].ToString());

        //
        // GET: /Telefonos/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar(char letra, int pagina)
        {
            UsuariosServices servicios = new UsuariosServices();
            List<UsuarioBussines> users = new List<UsuarioBussines>();

            users = servicios.ListarPorInicial(letra, pagina, _cant_contactos);

            ViewData["resultados"] = users;
            ViewData["resultados_tipo"] = "busqueda";
            ViewData["tipo_busqueda"] = "letra";
            ViewData["resultados_cant"] = users.Count;
            ViewData["total_paginas"] = servicios.TotalPaginas;
            ViewData["pagina_actual"] = pagina;

            return PartialView("ResultadoBusqueda", ViewData);
        }

        public ActionResult Buscar(string abuscar, string buscaren, int pagina)
        {
            UsuariosServices servicios = new UsuariosServices();
            List<UsuarioBussines> users = new List<UsuarioBussines>();

            users = servicios.Buscar(abuscar, buscaren, pagina, _cant_contactos);

            ViewData["resultados_tipo"] = "busqueda";            
            ViewData["resultados"] = users;            
            ViewData["tipo_busqueda"] = "libre";
            ViewData["resultados_cant"] = users.Count;
            ViewData["total_paginas"] = servicios.TotalPaginas;
            ViewData["pagina_actual"] = pagina;

            return PartialView("ResultadoBusqueda", ViewData);
        }
    }
}
