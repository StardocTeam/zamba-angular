using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zamba.WebAdmin.Models;
using Zamba.WebAdmin.Models.News;

namespace Zamba.WebAdmin.Controllers
{
    public class NewViewsController : Controller
    {
        public NewViewsController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
            }
            db = new ContextDB(Zamba.Servers.Server.get_Con().CN.ConnectionString);
        }

        private ContextDB db;
        public ActionResult New(int? id)
        {
            var userId = 1; //REEMPLAZAR POR ID USER ZAMBA

            ViewBag.Title = "Noticias";
            GetContent(id ?? db.ZInformation.FirstOrDefault().Id, "full");
            ViewBag.NewsCount = db.ZInformation.ToList().Count;
            var news = db.ZInformation.OrderByDescending(x => x.Important)
                            .Where(x => x.Id != id).ToList();
            var lvm = new List<ZInformationVM>();
            foreach (var n in news)
            {
                lvm.Add(new ZInformationVM
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.ShortContent,
                    Readed = n.ZInformationUser.Select(x => x.UserId).Contains(userId),
                    Created = n.Created
                });
            }
            if (id != null)
            {
                db.ZInformationUser.Add(new ZInformationUser()
                {
                    LastRead = DateTime.Now,
                    Readed = 1,
                    ZInformationId = id??0,
                    UserId = userId
                });
            }
            db.SaveChanges();
            return View(lvm);
        }

        public ActionResult NewHTML(int id)
        {
            ViewBag.Title = "Noticias";
            GetContent(id, "full");
            return View();
        }
        public ActionResult NewTooltip(int id)
        {
            ViewBag.Title = "Noticias";
            GetContent(id, "short");
            return View();
        }
        private void GetContent(int? id, string type)
        {
            string content = string.Empty;
            string title = string.Empty;
            if (id != null)
            {
                var news = db.ZInformation.Where(x => x.Id == id).FirstOrDefault();
                if (news != null)
                {
                    content = type == "short" ? news.ShortContent : news.FullContent;
                    title = news.Title;
                }
            }
            ViewBag.NewTitle = title;
            ViewBag.Content = content;
        }
    }
}