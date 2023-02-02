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
    public class SupplierCompaniesController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: SupplierCompanies
        public ActionResult Index()
        {
            return View(db.SupplierCompanies.ToList());
        }

        // GET: SupplierCompanies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierCompanies supplierCompanies = db.SupplierCompanies.Find(id);
            if (supplierCompanies == null)
            {
                return HttpNotFound();
            }
            return View(supplierCompanies);
        }

        // GET: SupplierCompanies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierCompanies/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSupplierCompanies,NameCompany,Address,PhoneNumber,Mail")] SupplierCompanies supplierCompanies)
        {
            if (ModelState.IsValid)
            {
                db.SupplierCompanies.Add(supplierCompanies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplierCompanies);
        }

        // GET: SupplierCompanies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierCompanies supplierCompanies = db.SupplierCompanies.Find(id);
            if (supplierCompanies == null)
            {
                return HttpNotFound();
            }
            return View(supplierCompanies);
        }

        // POST: SupplierCompanies/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSupplierCompanies,NameCompany,Address,PhoneNumber,Mail")] SupplierCompanies supplierCompanies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierCompanies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplierCompanies);
        }

        // GET: SupplierCompanies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierCompanies supplierCompanies = db.SupplierCompanies.Find(id);
            if (supplierCompanies == null)
            {
                return HttpNotFound();
            }
            return View(supplierCompanies);
        }

        // POST: SupplierCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupplierCompanies supplierCompanies = db.SupplierCompanies.Find(id);
            db.SupplierCompanies.Remove(supplierCompanies);
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
