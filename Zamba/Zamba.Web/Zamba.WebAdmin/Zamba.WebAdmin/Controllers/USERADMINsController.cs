using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zamba.WebAdmin.Models;

namespace Zamba.WebAdmin.Controllers
{
    public class USERADMINsController : Controller
    {
        public USERADMINsController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
            }
            db = new ContextDB(Zamba.Servers.Server.get_Con().CN.ConnectionString);
        }

        private ContextDB db;

        // GET: USERADMINs
        public async Task<ActionResult> Index()
        {
            return View(await db.USERADMINs.ToListAsync());
        }

        // GET: USERADMINs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USERADMIN uSERADMIN = await db.USERADMINs.FindAsync(id);
            if (uSERADMIN == null)
            {
                return HttpNotFound();
            }
            return View(uSERADMIN);
        }

        // GET: USERADMINs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USERADMINs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,idadmin,nombre")] USERADMIN uSERADMIN)
        {
            if (ModelState.IsValid)
            {
                db.USERADMINs.Add(uSERADMIN);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(uSERADMIN);
        }

        // GET: USERADMINs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USERADMIN uSERADMIN = await db.USERADMINs.FindAsync(id);
            if (uSERADMIN == null)
            {
                return HttpNotFound();
            }
            return View(uSERADMIN);
        }

        // POST: USERADMINs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,idadmin,nombre")] USERADMIN uSERADMIN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSERADMIN).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(uSERADMIN);
        }

        // GET: USERADMINs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USERADMIN uSERADMIN = await db.USERADMINs.FindAsync(id);
            if (uSERADMIN == null)
            {
                return HttpNotFound();
            }
            return View(uSERADMIN);
        }

        // POST: USERADMINs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            USERADMIN uSERADMIN = await db.USERADMINs.FindAsync(id);
            db.USERADMINs.Remove(uSERADMIN);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
