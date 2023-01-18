using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zamba.WebAdmin.Models;
using Zamba.WebAdmin.Models.News;

namespace Zamba.WebAdmin.Controllers
{
    public class NewsController : Controller
    {
        public NewsController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
            }
            db = new ContextDB(Zamba.Servers.Server.get_Con().CN.ConnectionString);
        }

        private ContextDB db;
        public ActionResult Index()
        {
            ViewBag.Title = "Zamba News";
            return View();
        }
        public ActionResult Create()
        {
            ViewBag.Title = "Nuevo";

            return View();
        }
        public ActionResult Edit()
        {
            ViewBag.Title = "Editar";

            return View();
        }
        [HttpGet]
        public ActionResult GetNewByUser(int userId)
        {
            //var info = from zi in db.ZInformation
            //           join ziu in db.ZInformationUser
            //           on zi.Id equals ziu.ZInformationId into z
            //           from zunion in z.DefaultIfEmpty()
            //          where zunion.UserId!= userId
            //           select new
            //           {
            //               zi
            //           };
            var inf = new ZInformation();
            var info = db.ZInformation.OrderByDescending(x => x.Important).ToList();
            var uInfo = db.ZInformationUser.Where(x => x.UserId == userId).ToList().Select(x=>x.ZInformationId);
            foreach(var i in info)
            {
                if (!uInfo.Contains(i.Id))
                {
                    inf = i;
                    break;
                }
            }

            if (inf.Id==0) return Json("", JsonRequestBehavior.AllowGet);
           // var inf = (ZInformation)info.OrderByDescending(x=>x.zi.Important).FirstOrDefault().zi;
            var zIU = new ZInformationUser()
            {
                UserId = userId,
                ZInformationId = inf.Id,
                Readed = 1,
                LastRead = DateTime.Now,
            };
            db.ZInformationUser.Add(zIU);
            db.SaveChanges();

            var vm = new ZInformationVM()
            {
                Id = inf.Id,                
                Title = inf.Title,
                Content = string.Empty
            };
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
    }
}