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
    public class TypeProductsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: TypeProducts
        public ActionResult Index()
        {
            return View(db.TypeProduct.ToList());
        }

        // GET: TypeProducts/Details/5
        public ActionResult Details(int? id)
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

        // GET: TypeProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeProducts/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTypeProduct,DescTypeProduct")] TypeProduct typeProduct)
        {
            if (ModelState.IsValid)
            {
                db.TypeProduct.Add(typeProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeProduct);
        }

        // GET: TypeProducts/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: TypeProducts/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTypeProduct,DescTypeProduct")] TypeProduct typeProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeProduct);
        }

        // GET: TypeProducts/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: TypeProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
