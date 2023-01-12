using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zamba.Core;
using Zamba.Help.Models;

namespace Zamba.Help.Controllers
{
    public class ListOptionsCRUDController : Controller
    {
        public ListOptionsCRUDController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Help");
            }
            db = new HelpContext(Zamba.Servers.Server.get_Con().CN.ConnectionString);          
        }

        private HelpContext db;
       
        // GET: ListOptionsCRUD
        public JsonResult Application(int id, string method, string value)
        {
            var result = new HelpApplication();

            switch (method)
            {
                case "insert":
                    result = new HelpApplication { Application = value };
                    db.HelpApplication.Add(result);
                    db.SaveChanges();                 
                    break;
                case "update":
                    result = db.HelpApplication.Where(x => x.Id == id).FirstOrDefault();
                    result.Application = value;
                    db.SaveChanges();
                    
                    break;
                case "remove":
                    result = db.HelpApplication.Where(x => x.Id == id).FirstOrDefault();
                    db.HelpApplication.Remove(result);
                    db.SaveChanges();                    
                    break;
            }
            return Json(result);
        }
        public JsonResult Module(int appid, int id, string method, string value)
        {
            var result = new HelpModule();
            try { 
            switch (method)
            {
                case "insert":
                    result = new HelpModule { Module = value, HelpApplicationId = appid };
                    db.HelpModule.Add(result);
                    db.SaveChanges();                    
                    break;
                case "update":
                    result= db.HelpModule.Where(x => x.Id == id).FirstOrDefault();
                    result.Module = value;
                    db.SaveChanges();                    
                    break;
                case "remove":
                    result = db.HelpModule.Where(x => x.Id == id).FirstOrDefault();
                    db.HelpModule.Remove(result);
                    db.SaveChanges();                    
                    break;
            }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return Json(result);
        }
        public JsonResult Function(int modid, int id, string method, string value)
        {
            var result = new HelpFunction();
            switch (method)
            {
                case "insert":
                    result = new HelpFunction { Function = value, HelpModuleId = modid };
                    db.HelpFunction.Add(result);
                    db.SaveChanges();                    
                    break;
                case "update":
                    result = db.HelpFunction.Where(x => x.Id == id).FirstOrDefault();
                    result.Function = value;
                    db.SaveChanges();
                    break;
                case "remove":
                    result = db.HelpFunction.Where(x => x.Id == id).FirstOrDefault();
                    db.HelpFunction.Remove(result);
                    db.SaveChanges();                    
                    break;
            }
            return Json(result);
        }

        public int AddUndefinedMod(int appid)
        {
            try
            {
                var mod = new HelpModule();
                mod.HelpApplicationId = appid;
                mod.Module = "Sin definir";
                db.HelpModule.Add(mod);
                db.SaveChanges();
                return mod.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int AddUndefinedFun(int modid)
        {
            try
            {
                var undFun = db.HelpFunction.Where(x => x.HelpModuleId == modid).FirstOrDefault();
                if (undFun == null)
                {
                    var fn = new HelpFunction();
                    fn.HelpModuleId = modid;
                    fn.Function = "Sin definir";
                    db.HelpFunction.Add(fn);
                    db.SaveChanges();
                    return fn.Id;
                }
                else
                    return undFun.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}