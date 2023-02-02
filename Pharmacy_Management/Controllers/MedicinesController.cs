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
    public class MedicinesController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Medicines
        public ActionResult Index()
        {
            var medicines = db.Medicines.Include(m => m.Drawers).Include(m => m.SupplierCompanies).Include(m => m.TypeMedicine).Include(m => m.TypeProduct);
            return View(medicines.ToList());
        }

        // GET: Medicines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicines medicines = db.Medicines.Find(id);
            if (medicines == null)
            {
                return HttpNotFound();
            }
            return View(medicines);
        }

        // GET: Medicines/Create
        public ActionResult Create()
        {
            ViewBag.IdDrawer = new SelectList(db.Drawers, "IdDrawer", "IdDrawer");
            ViewBag.IdSupplierCompanies = new SelectList(db.SupplierCompanies, "IdSupplierCompanies", "NameCompany");
            ViewBag.IdTypeMedicine = new SelectList(db.TypeMedicine, "IdTypeMedicine", "DescTypeMedicine");
            ViewBag.IdTypeProduct = new SelectList(db.TypeProduct, "IdTypeProduct", "DescTypeProduct");
            return View();
        }

        // POST: Medicines/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMedicine,IdTypeProduct,IdTypeMedicine,DescriptionUse,UrlImg,IdSupplierCompanies,IdDrawer,Stock")] Medicines medicines)
        {
            if (ModelState.IsValid)
            {
                db.Medicines.Add(medicines);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDrawer = new SelectList(db.Drawers, "IdDrawer", "IdDrawer", medicines.IdDrawer);
            ViewBag.IdSupplierCompanies = new SelectList(db.SupplierCompanies, "IdSupplierCompanies", "NameCompany", medicines.IdSupplierCompanies);
            ViewBag.IdTypeMedicine = new SelectList(db.TypeMedicine, "IdTypeMedicine", "DescTypeMedicine", medicines.IdTypeMedicine);
            ViewBag.IdTypeProduct = new SelectList(db.TypeProduct, "IdTypeProduct", "DescTypeProduct", medicines.IdTypeProduct);
            return View(medicines);
        }

        // GET: Medicines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicines medicines = db.Medicines.Find(id);
            if (medicines == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDrawer = new SelectList(db.Drawers, "IdDrawer", "IdDrawer", medicines.IdDrawer);
            ViewBag.IdSupplierCompanies = new SelectList(db.SupplierCompanies, "IdSupplierCompanies", "NameCompany", medicines.IdSupplierCompanies);
            ViewBag.IdTypeMedicine = new SelectList(db.TypeMedicine, "IdTypeMedicine", "DescTypeMedicine", medicines.IdTypeMedicine);
            ViewBag.IdTypeProduct = new SelectList(db.TypeProduct, "IdTypeProduct", "DescTypeProduct", medicines.IdTypeProduct);
            return View(medicines);
        }

        // POST: Medicines/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMedicine,IdTypeProduct,IdTypeMedicine,DescriptionUse,UrlImg,IdSupplierCompanies,IdDrawer,Stock")] Medicines medicines)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicines).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDrawer = new SelectList(db.Drawers, "IdDrawer", "IdDrawer", medicines.IdDrawer);
            ViewBag.IdSupplierCompanies = new SelectList(db.SupplierCompanies, "IdSupplierCompanies", "NameCompany", medicines.IdSupplierCompanies);
            ViewBag.IdTypeMedicine = new SelectList(db.TypeMedicine, "IdTypeMedicine", "DescTypeMedicine", medicines.IdTypeMedicine);
            ViewBag.IdTypeProduct = new SelectList(db.TypeProduct, "IdTypeProduct", "DescTypeProduct", medicines.IdTypeProduct);
            return View(medicines);
        }

        // GET: Medicines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicines medicines = db.Medicines.Find(id);
            if (medicines == null)
            {
                return HttpNotFound();
            }
            return View(medicines);
        }

        // POST: Medicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medicines medicines = db.Medicines.Find(id);
            db.Medicines.Remove(medicines);
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
