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
    public class InventaryController : Controller
    {
        private ModelDbContext db = new ModelDbContext();
        // GET: Inventary
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListMedicines()
        {
            var medicines = db.Medicines.Include(m => m.Drawers).Include(m => m.SupplierCompanies).Include(m => m.TypeMedicine).Include(m => m.TypeProduct);
            var order = new Orders();
            ViewBag.MedicinesCount = db.Medicines.Count();
            return View(medicines.ToList());
        }

        public ActionResult DetailsMedicine(int id) {
            var medicine = db.Medicines.Find(id);
            ViewBag.NameProduct = medicine.NameMedicine;
            return View(medicine);
        }

        public ActionResult EditMedicine(int id)
        {
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

        public ActionResult DeleteMedicine(int? id)
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
        public ActionResult DeleteMedicineConfirmed(int id)
        {
            Medicines medicines = db.Medicines.Find(id);
            db.Medicines.Remove(medicines);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}