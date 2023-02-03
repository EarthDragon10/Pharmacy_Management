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
    public class OrdersController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customers).Include(o => o.Medicines).Include(o => o.Pescritions);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // CREAZIONE DELL'ORDINE TRAMITE CLIENTI E PRODOTTI

        public ActionResult PreviewCustomers() // ./Customers/Index
        {
            var customers = db.Customers.Include(c => c.Roles);
            return View(customers.ToList());
        }

        public ActionResult ChoiceProduct(int id) // ./Medicines/Index
        {
            var medicines = db.Medicines.Include(m => m.Drawers).Include(m => m.SupplierCompanies).Include(m => m.TypeMedicine).Include(m => m.TypeProduct);
            var order = new Orders();
            Orders.StaticIdCustomer = id;
            //Orders.ListIdCustomer.Add(id);
            return View(medicines.ToList());
        }

        // GET: Customers/Create
        public ActionResult CreateCustomers()
        {
            ViewBag.IdRole = new SelectList(db.Roles, "IdRole", "TypeRole");
            return View();
        }

        // POST: Customers/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomers([Bind(Include = "IdCustomer,Username,FirstName,LastName,Pwd,CodFisc, IdRole")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customers);
                db.SaveChanges();
                return RedirectToAction("PreviewCustomers");
            }

            ViewBag.IdRole = new SelectList(db.Roles, "IdRole", "TypeRole", customers.IdRole);
            return View(customers);
        }
        // GET: Orders/Create
        public ActionResult Create(int id)
        {
            Orders.StaticIdMedicine = id;
            Orders.ListIdMedicine.Add(id);

            foreach (var item in Orders.ListIdMedicine)
            {
                Orders.PreOrder NewPreOrder = new Orders.PreOrder();
                var medicine = db.Medicines.Find(item);
                var typeProduct = db.TypeProduct.Find(medicine.IdTypeProduct);

                NewPreOrder.NameProduct = medicine.DescriptionUse;
                NewPreOrder.DescriptionUse = medicine.DescriptionUse;
                NewPreOrder.UrlImg = medicine.UrlImg;
                NewPreOrder.TypeProduct = typeProduct.DescTypeProduct;
                NewPreOrder.TypeMedicine = medicine.TypeMedicine.DescTypeMedicine;
                Orders.PreOrder.PreOrderList.Add(NewPreOrder);
            }

            ViewBag.IdCustomer = new SelectList(db.Customers, "IdCustomer", "Username");
            ViewBag.IdMedicine = new SelectList(db.Medicines, "IdMedicine", "DescriptionUse");
            ViewBag.IdPrescription = new SelectList(db.Pescritions, "IdPrescription", "IdPrescription");
            return View();
        }

        // POST: Orders/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOrder, Quantity,IdPrescription")] Orders orders)
        {
            orders.IdMedicine = Orders.StaticIdMedicine;
            orders.IdCustomer = Orders.StaticIdCustomer;
            orders.DateOrder = DateTime.Now;

            if (ModelState.IsValid)
            {
                // Oltre ad effettuare l'ordine, aggiorno anche la quantitá del prodotto nell'inventario
                var medicine = db.Medicines.Find(orders.IdMedicine);
                medicine.Stock -= orders.Quantity;
                db.Entry(medicine).State = System.Data.Entity.EntityState.Modified;

                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index", "Dashboard");
            }

            //ViewBag.IdCustomer = new SelectList(db.Customers, "IdCustomer", "Username", orders.IdCustomer);
            //ViewBag.IdMedicine = new SelectList(db.Medicines, "IdMedicine", "DescriptionUse", orders.IdMedicine);
            ViewBag.IdPrescription = new SelectList(db.Pescritions, "IdPrescription", "IdPrescription", orders.IdPrescription);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCustomer = new SelectList(db.Customers, "IdCustomer", "Username", orders.IdCustomer);
            ViewBag.IdMedicine = new SelectList(db.Medicines, "IdMedicine", "DescriptionUse", orders.IdMedicine);
            ViewBag.IdPrescription = new SelectList(db.Pescritions, "IdPrescription", "IdPrescription", orders.IdPrescription);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOrder,IdCustomer,IdMedicine,Quantity,IdPrescription,DateOrder")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCustomer = new SelectList(db.Customers, "IdCustomer", "Username", orders.IdCustomer);
            ViewBag.IdMedicine = new SelectList(db.Medicines, "IdMedicine", "DescriptionUse", orders.IdMedicine);
            ViewBag.IdPrescription = new SelectList(db.Pescritions, "IdPrescription", "IdPrescription", orders.IdPrescription);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
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
