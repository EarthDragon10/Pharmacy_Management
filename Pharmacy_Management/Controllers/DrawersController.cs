using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pharmacy_Management.Models;

namespace Pharmacy_Management.Controllers
{
    public class DrawersController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Drawers
        public ActionResult Index()
        {
            return View(db.Drawers.ToList());
        }

        // GET: Drawers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drawers drawers = db.Drawers.Find(id);
            if (drawers == null)
            {
                return HttpNotFound();
            }
            return View(drawers);
        }

        // GET: Drawers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drawers/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDrawer,Identifier")] Drawers drawers)
        {
            if (ModelState.IsValid)
            {
                db.Drawers.Add(drawers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drawers);
        }

        // GET: Drawers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drawers drawers = db.Drawers.Find(id);
            if (drawers == null)
            {
                return HttpNotFound();
            }
            return View(drawers);
        }

        // POST: Drawers/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDrawer,Identifier")] Drawers drawers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drawers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drawers);
        }

        // GET: Drawers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drawers drawers = db.Drawers.Find(id);
            if (drawers == null)
            {
                return HttpNotFound();
            }
            return View(drawers);
        }

        // POST: Drawers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Drawers drawers = db.Drawers.Find(id);
            db.Drawers.Remove(drawers);
            db.SaveChanges();
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
