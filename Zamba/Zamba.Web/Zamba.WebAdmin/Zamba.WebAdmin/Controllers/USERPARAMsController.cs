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
    public class USERPARAMsController : Controller
    {
        public USERPARAMsController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
            }
            db = new ContextDB(Zamba.Servers.Server.get_Con().CN.ConnectionString);
        }

        private ContextDB db;

        // GET: USERPARAMs
        public async Task<ActionResult> Index()
        {
            return View(await db.USERPARAMs.ToListAsync());
        }

        // GET: USERPARAMs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USERPARAM uSERPARAM = await db.USERPARAMs.FindAsync(id);
            if (uSERPARAM == null)
            {
                return HttpNotFound();
            }
            return View(uSERPARAM);
        }

        // GET: USERPARAMs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USERPARAMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,idparam,nombre")] USERPARAM uSERPARAM)
        {
            if (ModelState.IsValid)
            {
                db.USERPARAMs.Add(uSERPARAM);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(uSERPARAM);
        }

        // GET: USERPARAMs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USERPARAM uSERPARAM = await db.USERPARAMs.FindAsync(id);
            if (uSERPARAM == null)
            {
                return HttpNotFound();
            }
            return View(uSERPARAM);
        }

        // POST: USERPARAMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,idparam,nombre")] USERPARAM uSERPARAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSERPARAM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(uSERPARAM);
        }

        // GET: USERPARAMs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USERPARAM uSERPARAM = await db.USERPARAMs.FindAsync(id);
            if (uSERPARAM == null)
            {
                return HttpNotFound();
            }
            return View(uSERPARAM);
        }

        // POST: USERPARAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            USERPARAM uSERPARAM = await db.USERPARAMs.FindAsync(id);
            db.USERPARAMs.Remove(uSERPARAM);
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
