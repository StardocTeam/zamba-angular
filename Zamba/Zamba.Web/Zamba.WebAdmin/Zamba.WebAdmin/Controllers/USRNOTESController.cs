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
    public class USRNOTESController : Controller
    {
        public USRNOTESController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
            }
            db = new ContextDB(Zamba.Servers.Server.get_Con().CN.ConnectionString);
        }

        private ContextDB db;

        // GET: USRNOTES
        public async Task<ActionResult> Index()
        {
            return View(await db.USRNOTES.ToListAsync());
        }

        // GET: USRNOTES/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USRNOTES uSRNOTES = await db.USRNOTES.FindAsync(id);
            if (uSRNOTES == null)
            {
                return HttpNotFound();
            }
            return View(uSRNOTES);
        }

        // GET: USRNOTES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USRNOTES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,nombre,conf_mailserver,conf_basemail,conf_patharch,conf_vistaexportacion,Conf_Papelera,Conf_Nomarchtxt,Conf_seqatt,conf_lockeo,conf_acumimg,conf_limimg,conf_destext,conf_textosubject,Conf_Borrar,conf_archctrl,conf_schedulesel,conf_schedulevar,conf_ejecutable,conf_nomusernotes,conf_nomuserred,conf_charsreempsubj,conf_reintento,activo,conf_seqimg,conf_bodyandattachsinexportedmails")] USRNOTES uSRNOTES)
        {
            if (ModelState.IsValid)
            {
                db.USRNOTES.Add(uSRNOTES);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(uSRNOTES);
        }

        // GET: USRNOTES/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USRNOTES uSRNOTES = await db.USRNOTES.FindAsync(id);
            if (uSRNOTES == null)
            {
                return HttpNotFound();
            }
            return View(uSRNOTES);
        }

        // POST: USRNOTES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,nombre,conf_mailserver,conf_basemail,conf_patharch,conf_vistaexportacion,Conf_Papelera,Conf_Nomarchtxt,Conf_seqatt,conf_lockeo,conf_acumimg,conf_limimg,conf_destext,conf_textosubject,Conf_Borrar,conf_archctrl,conf_schedulesel,conf_schedulevar,conf_ejecutable,conf_nomusernotes,conf_nomuserred,conf_charsreempsubj,conf_reintento,activo,conf_seqimg,conf_bodyandattachsinexportedmails")] USRNOTES uSRNOTES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSRNOTES).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(uSRNOTES);
        }

        // GET: USRNOTES/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USRNOTES uSRNOTES = await db.USRNOTES.FindAsync(id);
            if (uSRNOTES == null)
            {
                return HttpNotFound();
            }
            return View(uSRNOTES);
        }

        // POST: USRNOTES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            USRNOTES uSRNOTES = await db.USRNOTES.FindAsync(id);
            db.USRNOTES.Remove(uSRNOTES);
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
