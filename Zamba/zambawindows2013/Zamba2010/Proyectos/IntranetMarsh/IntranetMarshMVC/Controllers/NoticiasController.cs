using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using Marsh.Services;
using Marsh.Bussines;
using System.IO;
using Zamba.Core;
using System;
using System.Security.Principal;

namespace IntranetMarshMVC.Controllers
{
    public class NoticiasController : Controller
    {
        public ActionResult Index()
        {
            return View(ObtenerPagina(1));
        }

        public ActionResult Pagina(int pagina)
        {
            return View(ObtenerPagina(pagina));
        }

        public ActionResult Leer(int id)
        {
            return View(new NoticiasServices().getNoticiaById(id));
        }

        private ViewDataDictionary ObtenerPagina(int pagina)
        {
            NoticiasServices servicios = new NoticiasServices();
            List<NoticiaBussines> noticias = new List<NoticiaBussines>();

            int cant_noticias = int.Parse(ConfigurationSettings.AppSettings["noticia_cant_por_pagina"].ToString());

            noticias = servicios.Listar(pagina, cant_noticias);                     

            ViewData["noticias"] = noticias;
            ViewData["total_paginas"] = servicios.TotalPaginas;
            ViewData["pagina_actual"] = pagina;

            return ViewData;
        }

        //// copia de prueba
        //private string CopiaLocal(NoticiaBussines noticia)
        //{
        //    string img_folder;
        //    string newfile = "";

        //    img_folder = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"imgs\noticias";

        //    if (!Directory.Exists(img_folder))
        //    {
        //        try
        //        {
        //            Directory.CreateDirectory(img_folder);
        //        }
        //        catch (Exception ex)
        //        {
        //            ZClass.raiseerror(ex);
        //        }
        //    }

        //    try
        //    {
        //        newfile = img_folder + @"\" + noticia.ImagenFileName;

        //        if (!System.IO.File.Exists(newfile))
        //            System.IO.File.Copy(noticia.Imagen, newfile, true);

        //        string aux;

        //        aux = AppDomain.CurrentDomain.BaseDirectory.ToString();
        //        aux += "copia_imagen.bat ";
        //        aux += "" + noticia.Imagen + " ";
        //        aux += "" + newfile + "";

        //        System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
        //        System.Security.SecureString pass = new System.Security.SecureString();
                
        //        pass.AppendChar('s');
        //        pass.AppendChar('t');
        //        pass.AppendChar('a');
        //        pass.AppendChar('r');
        //        pass.AppendChar('d');
        //        pass.AppendChar('o');
        //        pass.AppendChar('c');

        //        info.Arguments = noticia.Imagen + " " + newfile;
        //        info.FileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + "copia_imagen.bat ";
        //        info.LoadUserProfile = true;
        //        info.Password = pass;
        //        info.UserName = "stardoc";
        //        info.UseShellExecute = false;

        //        System.Diagnostics.Process.Start(info);

        //        newfile = @"/imgs/noticias/" + noticia.ImagenFileName;
        //    }
        //    catch (Exception ex)
        //    {
        //        ZClass.raiseerror(ex);

        //        newfile = "file:///" + noticia.ImagenPath + @"\" + noticia.ImagenFileName;
        //    }

        //    return newfile;
        //}    
    }
}