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
    public class PescritionsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Pescritions
        public ActionResult Index()
        {
            var pescritions = db.Pescritions.Include(p => p.Customers);
            return View(pescritions.ToList());
        }

        // GET: Pescritions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pescritions pescritions = db.Pescritions.Find(id);
            if (pescritions == null)
            {
                return HttpNotFound();
            }
            return View(pescritions);
        }

        // GET: Pescritions/Create
        public ActionResult Create()
        {
            ViewBag.IdCustomer = new SelectList(db.Customers, "IdCustomer", "Username");
            return View();
        }

        // POST: Pescritions/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPrescription,IdentifierPrescription,IdCustomer")] Pescritions pescritions)
        {
            if (ModelState.IsValid)
            {
                db.Pescritions.Add(pescritions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCustomer = new SelectList(db.Customers, "IdCustomer", "Username", pescritions.IdCustomer);
            return View(pescritions);
        }

        // GET: Pescritions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pescritions pescritions = db.Pescritions.Find(id);
            if (pescritions == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCustomer = new SelectList(db.Customers, "IdCustomer", "Username", pescritions.IdCustomer);
            return View(pescritions);
        }

        // POST: Pescritions/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPrescription,IdentifierPrescription,IdCustomer")] Pescritions pescritions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pescritions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCustomer = new SelectList(db.Customers, "IdCustomer", "Username", pescritions.IdCustomer);
            return View(pescritions);
        }

        // GET: Pescritions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pescritions pescritions = db.Pescritions.Find(id);
            if (pescritions == null)
            {
                return HttpNotFound();
            }
            return View(pescritions);
        }

        // POST: Pescritions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pescritions pescritions = db.Pescritions.Find(id);
            db.Pescritions.Remove(pescritions);
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
