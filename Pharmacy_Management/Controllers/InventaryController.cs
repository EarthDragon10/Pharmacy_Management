using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Pharmacy_Management.Models;

namespace Pharmacy_Management.Controllers
{
    public class InventaryController : Controller
    {
        private ModelDbContext db = new ModelDbContext();
        // GET: Inventary
        public ActionResult Index()
        {
            var finishedMedicines = db.Medicines.Where(m => m.Stock > 5).Include(m => m.Drawers).Include(m => m.SupplierCompanies).Include(m => m.TypeMedicine).Include(m => m.TypeProduct);
            ViewBag.FinishedMedicinesCount = finishedMedicines.Count();
            var medicines = db.Medicines.Where(m => m.Stock > 0).ToList();
            int TotalStock = 0;
            foreach (var item in medicines)
            {
                TotalStock += item.Stock;
            }

            ViewBag.MedicinesCount = TotalStock;
            ViewBag.MedicineGroups = db.TypeProduct.Where(p => p.DescTypeProduct != null).Count();
            return View();
        }

        public ActionResult ListMedicines()
        {
            var medicines = db.Medicines.Include(m => m.Drawers).Include(m => m.SupplierCompanies).Include(m => m.TypeMedicine).Include(m => m.TypeProduct);
            var order = new Orders();
            
            ViewBag.MedicinesCount = db.Medicines.Count();
            
            return View(medicines.ToList());
        }

        public ActionResult FixLastProducts()
        {
            var finishedMedicines = db.Medicines.Where(m => m.Stock <= 5).Include(m => m.Drawers).Include(m => m.SupplierCompanies).Include(m => m.TypeMedicine).Include(m => m.TypeProduct).ToList();
            return View(finishedMedicines);
        }

        [HttpPost]
        public ActionResult FixLastProducts(Medicines medicine)
        {
            return RedirectToAction("Index");
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
        public ActionResult EditMedicine([Bind(Include = "NameMedicine,price,IdMedicine,IdTypeProduct,IdTypeMedicine,DescriptionUse,UrlImg,IdSupplierCompanies,IdDrawer,Stock")] Medicines medicines)
        {
            if (ModelState.IsValid == true && medicines.FileImg != null)
            {
                string path = Server.MapPath("~/Content/Assets/Img/Medicines/" + medicines.FileImg.FileName);
                medicines.FileImg.SaveAs(path);
                medicines.UrlImg = medicines.FileImg.FileName;
                db.Entry(medicines).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListMedicines");
            } else if (ModelState.IsValid)
            {
                Medicines medicine = db.Medicines.Find(medicines.IdMedicine);
                medicine.NameMedicine = medicines.NameMedicine;
                medicine.IdTypeProduct = medicines.IdTypeProduct;
                medicine.IdTypeMedicine = medicines.IdTypeMedicine;
                medicine.DescriptionUse= medicines.DescriptionUse;
                medicine.UrlImg = medicines.UrlImg;
                medicine.IdSupplierCompanies = medicines.IdSupplierCompanies;
                medicine.price = medicines.price;
                medicine.IdDrawer = medicines.IdDrawer;
                medicine.Stock = medicines.Stock;
                db.Entry(medicine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListMedicines");
            }
            ViewBag.IdDrawer = new SelectList(db.Drawers, "IdDrawer", "IdDrawer", medicines.IdDrawer);
            ViewBag.IdSupplierCompanies = new SelectList(db.SupplierCompanies, "IdSupplierCompanies", "NameCompany", medicines.IdSupplierCompanies);
            ViewBag.IdTypeMedicine = new SelectList(db.TypeMedicine, "IdTypeMedicine", "DescTypeMedicine", medicines.IdTypeMedicine);
            ViewBag.IdTypeProduct = new SelectList(db.TypeProduct, "IdTypeProduct", "DescTypeProduct", medicines.IdTypeProduct);
            return View(medicines);
        }

        public ActionResult DeleteProduct(int? id)
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
        public ActionResult DeleteProductConfirmed(int id)
        {
            Medicines medicines = db.Medicines.Find(id);
            db.Medicines.Remove(medicines);
            db.SaveChanges();
            return RedirectToAction("ListMedicines");
        }

        public ActionResult CreateProduct() {
            ViewBag.IdDrawer = new SelectList(db.Drawers, "IdDrawer", "IdDrawer");
            ViewBag.IdSupplierCompanies = new SelectList(db.SupplierCompanies, "IdSupplierCompanies", "NameCompany");
            ViewBag.IdTypeMedicine = new SelectList(db.TypeMedicine, "IdTypeMedicine", "DescTypeMedicine");
            ViewBag.IdTypeProduct = new SelectList(db.TypeProduct, "IdTypeProduct", "DescTypeProduct");
            return View();
        }

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
    }
}