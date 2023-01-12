using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MVC_Demo.Models;
using System.Web.UI;

namespace MVC_Demo.Controllers
{
    public class PeliculaController : Controller 
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Buscar()
        //{
        //    if (Request.HttpMethod == "GET")
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return View(new PeliculaDB().Search(Request.Form["titulo"]));
        //    }
        //}

        public ActionResult Buscar()
        {
            return View();            
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Buscar(string titulo)
        {
            return View(new PeliculaDB().Search(titulo));
        }
        
        public ActionResult BuscarAjax(string titulo)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("BuscarAjaxPV", new PeliculaDB().Search(titulo));
            }
            else
            {
                return View();
            }
        }

        public ActionResult Listar()
        {
            return View(new PeliculaDB().GetAll());
        }

        public ActionResult Editar(int id)
        {
            return View(new PeliculaDB().GetById(id));
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editar(Pelicula peli)
        {
            new PeliculaDB().Update(peli);

            return RedirectToAction("Listar");
        }
        
        public ActionResult Detalle(int id)
        {
            return Editar(id);
        }
        
        public ActionResult Nuevo()
        {
            return View();
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Nuevo(Pelicula peli)
        {
            new PeliculaDB().Add(peli);

            return RedirectToAction("Listar");
        }

        public bool Borrar(int id)
        {
            return new PeliculaDB().Delete(id);
        }
    }
}