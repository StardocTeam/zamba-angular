using System.Collections.Generic;
using System.Web.Mvc;
using Marsh.Services;
using Marsh.Bussines;
using System.Configuration;

namespace IntranetMarshMVC.Controllers
{
    public class FormulariosController : Controller
    {
        private int _cant_forms = int.Parse(ConfigurationSettings.AppSettings["formularios_cant_por_pagina"].ToString());

        //
        // GET: /Formularios/

        public ActionResult Index()
        {
            FormulariosServices servicios = new FormulariosServices();

            ViewData["categorias"] = servicios.ListarCategorias();
            ViewData["formularios"] = servicios.Listar(1, _cant_forms);
            ViewData["total_paginas"] = servicios.TotalPaginas;
            ViewData["pagina_actual"] = 1;

            return View("Index", ViewData);
        }

        public ActionResult Pagina(int pagina)
        {
            FormulariosServices servicios = new FormulariosServices();

            ViewData["categorias"] = servicios.ListarCategorias();
            ViewData["formularios"] = servicios.Listar(pagina, _cant_forms);
            ViewData["total_paginas"] = servicios.TotalPaginas;
            ViewData["pagina_actual"] = pagina;

            return View("Index", ViewData);
        }
        
        public ActionResult Buscar(string buscar, string categ, int pagina)
        {
            FormulariosServices servicios = new FormulariosServices();

            ViewData["categorias"] = servicios.ListarCategorias();
            ViewData["formularios"] = servicios.Buscar(buscar, categ, pagina, _cant_forms);
            ViewData["total_paginas"] = servicios.TotalPaginas;
            ViewData["pagina_actual"] = pagina;

            return View("Index", ViewData);
        }

        public ActionResult Filtrar(string categ, int pagina)
        {
            FormulariosServices servicios = new FormulariosServices();

            ViewData["categorias"] = servicios.ListarCategorias();
            ViewData["formularios"] = servicios.Filtrar(categ, pagina, _cant_forms);
            ViewData["total_paginas"] = servicios.TotalPaginas;
            ViewData["pagina_actual"] = pagina;

            return View("Index", ViewData);
        }

        public ActionResult Ver(int id)
        {
            return View(new FormulariosServices().getFormularioById(id));
        }

        public void BajarFormulario(int id)
        {
            FormularioBussines form = new FormulariosServices().getFormularioById(id);
            Response.Redirect(form.File);
        }
    }
}