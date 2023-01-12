using System.Collections.Generic;
using System.Web.Mvc;
using Marsh.Services;
using Marsh.Bussines;
using System.Configuration;

namespace IntranetMarshMVC.Controllers
{
    public class InicioController : Controller
    {
        //
        // GET: /Inicio/

        public ActionResult Index()
        {
            NoticiasServices servicios = new NoticiasServices();
            List<NoticiaBussines> noticias = new List<NoticiaBussines>();

            int cant_noticias = int.Parse(ConfigurationSettings.AppSettings["noticia_home_cant_por_pagina"].ToString());

            noticias = servicios.Listar(1, cant_noticias);

            ViewData["noticias"] = noticias;
            ViewData["total_paginas"] = servicios.TotalPaginas;
            ViewData["pagina_actual"] = 1;

            return View(ViewData);
        }
    }
}
