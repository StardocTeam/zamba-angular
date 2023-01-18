using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Zamba.Help.Models;
using System.Web.Http.Cors;
using System.IO;
using System.Reflection;
using Zamba.Core;
using Zamba.Membership;
using Zamba.AppBlock;
using System.Web;
using Zamba.Data;

namespace Zamba.Help.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]


    public class HelperController : ApiController
    {
        public HelperController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Help");
            }
            db = new HelpContext(Zamba.Servers.Server.get_Con().CN.ConnectionString);

            var user = GetUser();

            UserPermition = new UserPermitions();

            UserPermition.Edit = RightsBusiness.GetUserRights(user.ID, Zamba.ObjectTypes.Helper, Zamba.Core.RightsType.Edit);
            UserPermition.Delete = RightsBusiness.GetUserRights(user.ID, Zamba.ObjectTypes.Helper, Zamba.Core.RightsType.Delete);
            UserPermition.Create = RightsBusiness.GetUserRights(user.ID, Zamba.ObjectTypes.Helper, Zamba.Core.RightsType.Create);
        }

        private HelpContext db ;
        public UserPermitions UserPermition { get; private set; }
       

        [Route("api/helper/GetListOptions")]
        [ResponseType(typeof(ListOptions))]
        public IHttpActionResult GetListOptions()
        {
            var lo = new ListOptions()
            {
                Applications = db.HelpApplication.AsNoTracking().OrderBy(x => x.Application).ToList(),
                Types = db.HelpType.AsNoTracking().OrderBy(x => x.Type).ToList()
            };
            return Ok(lo);
        }

        [Route("api/helper/GetHelpModules")]
        [ResponseType(typeof(List<HelpModule>))]
        public IHttpActionResult GetHelpModules(int appId)
        {
            var hm = db.HelpModule.AsNoTracking().Where(x => x.HelpApplicationId == appId).OrderBy(x => x.Module).ToList();
            return Ok(hm);
        }


        [Route("api/helper/GetHelpFunctions")]
        [ResponseType(typeof(List<HelpFunction>))]
        public IHttpActionResult GetHelpFunctions(int modId)
        {
            var hm = db.HelpFunction.AsNoTracking().Where(x => x.HelpModuleId == modId).OrderBy(x => x.Function).ToList();
            return Ok(hm);
        }

        [Route("api/helper/GetTemplates")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult GetTemplates()
        {
            var lst = new List<string>();
            string assemblyFile = Path.GetDirectoryName((
                             new System.Uri(Assembly.GetExecutingAssembly().CodeBase)
                         ).AbsolutePath).ToString();
            var str = File.ReadAllText(assemblyFile + "\\Content\\htmltemplate\\help.html");
            lst.Add(str);
            return Ok(lst);
        }

        private static IUser GetUser()
        {
            try
            {
                IUser user = HttpContext.Current.Session["UserHelp"] != null ?
                    (IUser)HttpContext.Current.Session["UserHelp"] : new User();
                return user;
            }
            catch (Exception ex)
            {
                return new User();
            }
        }


        // GET: api/Helper
        public HelpItemWithPermitions GetHelpModel()//(int userid)
        {
            try
            {          
                var hml = db.Database.SqlQuery<HelpItemVM>(
                      "SELECT i.Id, Code, Title, Name, i.ForAllUsers, t.[Type], a.[Application], m.[Module], f.[Function], i.OrderId FROM HelpItems i" +
                      " inner join HelpFunctions f on i.HelpFunctionId= f.Id " +
                      " inner join HelpModules m on f.HelpModuleId= m.Id " +
                      " inner join HelpApplications a on m.HelpApplicationId= a.Id " +
                      " inner join HelpTypes t on i.HelpTypeId=t.Id").ToList();

                var l = new List<HelpItem>();
                hml.ForEach(i => l.Add(new HelpItem(i.Id, i.Code, i.Title, i.Name, i.Type, i.Application, i.Module, i.Function, i.ForAllUsers, i.OrderId)));
                                
                var hIWP = new HelpItemWithPermitions
                {
                    HelpItems = l,
                    Edit =UserPermition.Edit,
                    Delete =UserPermition.Delete,
                    Create= UserPermition.Create
                };

                return hIWP;
            }
            catch (Exception ex)
            {
                return new HelpItemWithPermitions();
            }
        }

        // GET: api/Helper/5
        [ResponseType(typeof(HelpItem))]
        public IHttpActionResult GetHelpModel(int id)
        {
            HelpItem helpItem = db.HelpItem.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            if (helpItem == null)
            {
                return NotFound();
            }
            return Ok(helpItem);
        }

        // PUT: api/Helper/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHelpModel(int id, HelpItem helpItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != helpItem.Id)
            {
                return BadRequest();
            }

            db.Entry(helpItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HelpModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Helper
        [ResponseType(typeof(HelpItem))]
        public IHttpActionResult PostHelpModel(HelpItem HelpItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (HelpItem.Id > 0)
            {
                db.Entry(HelpItem).State = EntityState.Modified;
            }
            else
            {
                db.HelpItem.Add(HelpItem);
            }
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = HelpItem.Id }, HelpItem);
        }

        //// DELETE: api/Helper/5
        [ResponseType(typeof(HelpItem))]
        public IHttpActionResult DeleteHelpModel(int id)
        {
            HelpItem helpModel = db.HelpItem.Find(id);
            if (helpModel == null)
            {
                return NotFound();
            }
            db.HelpItem.Remove(helpModel);
            db.SaveChanges();
            return Ok(helpModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HelpModelExists(int id)
        {
            return db.HelpItem.AsNoTracking().Count(e => e.Id == id) > 0;
        }
    }
}