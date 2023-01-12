using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zamba.Core;
using Zamba.Help.Models;
using Zamba.Membership;

namespace Zamba.Help.Controllers
{
    public class ViewerController : Controller
    {
        public ViewerController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Help");
            }
            db = new HelpContext(Zamba.Servers.Server.get_Con().CN.ConnectionString);
        }

        private HelpContext db;
        public ActionResult Index(int? id)
        {

            ViewBag.Title = "Contenido completo";
            //  GetContent(id, "full");
            return RedirectToAction("FullContent", "Viewer", new { id = id });
        }
        public ActionResult FullContent(string id)
        {
            ViewBag.Title = "Contenido completo";
            GetContent(id, "full");
            return View();
        }
        public ActionResult FullContentHTML(int id)
        {
            ViewBag.Title = "Contenido completo";
            GetContent(id.ToString(), "full");
            return View();
        }

        public ActionResult ShortContent(int id)
        {
            ViewBag.Title = "Contenido breve";
            GetContent(id.ToString(), "short");
            return View();
        }
        private void GetContent(string id, string type)
        {

            string content = string.Empty;
            if (id != null)
            {

                bool AdvancedHelps = false;
                if (MembershipHelper.CurrentUser != null)
                {
                    AdvancedHelps = RightsBusiness.GetUserRights(MembershipHelper.CurrentUser.ID, ObjectTypes.Helper, Zamba.Core.RightsType.Admin);
                }


                HelpItem helper;

                int v;
                if (Int32.TryParse(id.Trim(), out v))
                {
                    if (AdvancedHelps)
                    {
                        helper = db.HelpItem.AsNoTracking().Where(x => x.Id == v).FirstOrDefault();
                    }
                    else
                    {
                        helper = db.HelpItem.AsNoTracking().Where(x => x.Id == v && x.ForAllUsers).FirstOrDefault();
                    }
                }
                else
                {
                    if (AdvancedHelps)
                    {
                        helper = db.HelpItem.AsNoTracking().Where(x => x.Code.CompareTo(id) >= 0).FirstOrDefault();
                    }
                    else
                    {
                        helper = db.HelpItem.AsNoTracking().Where(x => x.Code.CompareTo(id) >= 0 && x.ForAllUsers).FirstOrDefault();
                    }
                }
            
            if (helper != null)
                content = type == "short" ? helper.ShortContent : helper.FullContent;
            else
                content = "<br/><br/>El manual de ayuda que esta intentando acceder no esta disponible en estos momentos." +
                    "<br/><br/>Utilice el buscador del panel izquierdo para acceder a las ayudas disponibles.";
        }
        ViewBag.Content = content;
            
        }
        public JsonResult GetTreeData()
        {
            var appVMl = new List<HelpTreeItem>();
            bool AdvancedHelps = false;
            if (MembershipHelper.CurrentUser != null)
            {
               AdvancedHelps = RightsBusiness.GetUserRights(MembershipHelper.CurrentUser.ID, ObjectTypes.Helper, Zamba.Core.RightsType.Admin);
            }
         
            foreach (HelpApplication app in db.HelpApplication.AsNoTracking().OrderBy(c => c.OrderId).ToList())
            {
                var appVM = new HelpTreeItem();
                app.Modules = db.HelpModule.AsNoTracking().Where(x => x.HelpApplicationId == app.Id).OrderBy(c => c.OrderId).ToList();
                List<HelpItemVM> items;
                foreach (HelpModule mod in app.Modules)
                {
                    var modVM = new HelpTreeItem();
                    mod.Functions = db.HelpFunction.AsNoTracking().Where(x => x.HelpModule.Id == mod.Id).OrderBy(c => c.OrderId).ToList();
                    foreach (HelpFunction fun in mod.Functions)
                    {
                        if (AdvancedHelps)
                        {
                            items = db.Database.SqlQuery<HelpItemVM>("select Id, Code, Title, Name, OrderId from HelpItems where HelpFunctionId =" + fun.Id).OrderBy(c => c.OrderId).ToList();
                        }
                        else
                        {
                            items = db.Database.SqlQuery<HelpItemVM>("select Id, Code, Title, Name, OrderId from HelpItems where HelpFunctionId = " + fun.Id + " and ForAllUsers = 1").OrderBy(c => c.OrderId).ToList();
                        }

                        if (items.Count == 0) continue;
                        var l = new List<HelpItem>();
                        items.ForEach(i => l.Add(new HelpItem(i.Id, i.Code, i.Title, i.Name, i.OrderId)));

                        if (fun.Function == "Sin definir")
                        {
                            modVM.HelpItems.AddRange(l);
                        }
                        else
                        {
                            var funVM = new HelpTreeItem();
                            funVM.Name = fun.Function;
                            funVM.HelpItems.AddRange(l);
                            modVM.ChildItems.Add(funVM);
                        }
                    }
                    if (mod.Module == "Sin definir")
                    {
                        appVM.ChildItems.AddRange(modVM.ChildItems);
                        appVM.HelpItems.AddRange(modVM.HelpItems);
                    }
                    else
                    {
                        modVM.Name = mod.Module;
                        if (modVM.ChildItems.Count > 0)
                        {
                            appVM.ChildItems.Add(modVM);
                        }
                    }
                }
                if (appVM.ChildItems.Count > 0 || appVM.HelpItems.Count > 0)
                {
                    appVM.Name = app.Application;
                    appVMl.Add(appVM);
                }
            }

            var json = this.Json(new
            {
                Nodes = appVMl,
            }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
        }
        public JsonResult GetRigths()
        {
            bool crear = RightsBusiness.GetUserRights(MembershipHelper.CurrentUser.ID, Zamba.ObjectTypes.Helper, Zamba.Core.RightsType.Create);

            var json = this.Json(new
            {
                userid = MembershipHelper.CurrentUser.ID,
                create = crear,
            }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;

        }
    }
}
