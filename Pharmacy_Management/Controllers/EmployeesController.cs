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
    [Authorize]
    public class EmployeesController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Roles);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.IdRole = new SelectList(db.Roles, "IdRole", "TypeRole");
            return View();
        }

        // POST: Employees/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEmployee,Username,FirstName,LastName,Pwd,FileImg,IdRole")] Employees employee)
        {

            if (ModelState.IsValid)
            {
                if (employee.FileImg != null)
                {
                    string Path = Server.MapPath("~/Content/Assets/Img/Employees/" + employee.FileImg.FileName);
                    employee.FileImg.SaveAs(Path);
                    employee.UrlImg = employee.FileImg.FileName;
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Alcuni campi non sono stati completati!";
                    return View();
                }
            }
            ViewBag.IdRole = new SelectList(db.Roles, "IdRole", "TypeRole", employee.IdRole);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRole = new SelectList(db.Roles, "IdRole", "TypeRole", employees.IdRole);
            return View(employees);
        }

        // POST: Employees/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEmployee,Username,FirstName,LastName,Pwd,FileImg, IdRole")] Employees employees)
        {
            if (ModelState.IsValid)
            {
                Employees employeeDb = db.Employees.Find(employees.IdEmployee);

                if(employees.FileImg != null)
                {
                    employeeDb.UrlImg = employees.FileImg.FileName;
                    employees.FileImg.SaveAs(Server.MapPath("~/Content/Assets/Img/Employees/" + employees.FileImg.FileName));
                }

                employeeDb.Username = employees.Username;
                employeeDb.FirstName = employees.FirstName;
                employeeDb.LastName = employees.LastName;
                employeeDb.Pwd = employees.Pwd;
                employeeDb.IdRole= employees.IdRole;

                db.Entry(employeeDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRole = new SelectList(db.Roles, "IdRole", "TypeRole", employees.IdRole);
            return View(employees);
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employees = db.Employees.Find(id);
            if (employees == null)
            {
                return HttpNotFound();
            }
            return View(employees);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employees employees = db.Employees.Find(id);
            db.Employees.Remove(employees);
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
