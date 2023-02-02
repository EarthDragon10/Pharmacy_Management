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
    public class TypeMedicinesController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: TypeMedicines
        public ActionResult Index()
        {
            return View(db.TypeMedicine.ToList());
        }

        // GET: TypeMedicines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeMedicine typeMedicine = db.TypeMedicine.Find(id);
            if (typeMedicine == null)
            {
                return HttpNotFound();
            }
            return View(typeMedicine);
        }

        // GET: TypeMedicines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeMedicines/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTypeMedicine,DescTypeMedicine")] TypeMedicine typeMedicine)
        {
            if (ModelState.IsValid)
            {
                db.TypeMedicine.Add(typeMedicine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeMedicine);
        }

        // GET: TypeMedicines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeMedicine typeMedicine = db.TypeMedicine.Find(id);
            if (typeMedicine == null)
            {
                return HttpNotFound();
            }
            return View(typeMedicine);
        }

        // POST: TypeMedicines/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTypeMedicine,DescTypeMedicine")] TypeMedicine typeMedicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeMedicine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeMedicine);
        }

        // GET: TypeMedicines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeMedicine typeMedicine = db.TypeMedicine.Find(id);
            if (typeMedicine == null)
            {
                return HttpNotFound();
            }
            return View(typeMedicine);
        }

        // POST: TypeMedicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeMedicine typeMedicine = db.TypeMedicine.Find(id);
            db.TypeMedicine.Remove(typeMedicine);
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
