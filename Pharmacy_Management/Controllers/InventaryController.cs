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
            var finishedMedicines = db.Medicines.Where(m => m.Stock <= 5).Include(m => m.Drawers).Include(m => m.SupplierCompanies).Include(m => m.TypeMedicine).Include(m => m.TypeProduct);
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
      
        public ActionResult FixSingleProducts(int id)
        {
            var medicine = db.Medicines.Find(id);
            return View(medicine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FixSingleProducts(Medicines medicine)
        {
            Medicines newMedicine = db.Medicines.Find(medicine.IdMedicine);
            newMedicine.Stock = medicine.Stock;
            db.Entry(newMedicine).State = EntityState.Modified;
            db.SaveChanges();

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
        public ActionResult EditMedicine([Bind(Include = "NameMedicine,price,IdMedicine,IdTypeProduct,IdTypeMedicine,DescriptionUse,FileImg, IdSupplierCompanies,IdDrawer,Stock")] Medicines medicines, HttpPostedFileBase FileUpload)
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

        // Categorie di Prodotti
        public ActionResult ListGroupsMedicine()
        {
            return View(db.TypeProduct.ToList());
        }

        public ActionResult DetailsGroupMedicine(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeProduct typeProduct = db.TypeProduct.Find(id);
            if (typeProduct == null)
            {
                return HttpNotFound();
            }
            return View(typeProduct);
        }

        public ActionResult CreateGroupMedicine()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroupMedicine([Bind(Include = "IdTypeProduct,DescTypeProduct")] TypeProduct typeProduct)
        {
            if (ModelState.IsValid)
            {
                db.TypeProduct.Add(typeProduct);
                db.SaveChanges();
                return RedirectToAction("ListGroupsMedicine");
            }

            return View(typeProduct);
        }

        public ActionResult EditGroupMedicine(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeProduct typeProduct = db.TypeProduct.Find(id);
            if (typeProduct == null)
            {
                return HttpNotFound();
            }
            return View(typeProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTypeProduct,DescTypeProduct")] TypeProduct typeProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListGroupsMedicine");
            }
            return View(typeProduct);
        }

        public ActionResult DeleteGroupMedicine(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeProduct typeProduct = db.TypeProduct.Find(id);
            if (typeProduct == null)
            {
                return HttpNotFound();
            }
            return View(typeProduct);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGroupMedicineConfirmed(int id)
        {
            TypeProduct typeProduct = db.TypeProduct.Find(id);
            db.TypeProduct.Remove(typeProduct);
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