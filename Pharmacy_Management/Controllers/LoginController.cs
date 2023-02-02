using Pharmacy_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pharmacy_Management.Controllers
{
    public class LoginController : Controller
    {
        private ModelDbContext DbContext = new ModelDbContext();
        // GET: Login
        public ActionResult SignIn()
        {
            ViewBag.IdRole = new SelectList(DbContext.Roles, "IdRole", "TypeRole");
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Users user)
        {

             if(DbContext.Employees.Where(e => e.Username == user.Username && e.Pwd == user.Password).Count() > 0)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return Redirect(FormsAuthentication.DefaultUrl);
            } else if(DbContext.Customers.Where(e => e.Username == user.Username && e.Pwd == user.Password).Count() > 0)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return Redirect(FormsAuthentication.DefaultUrl);
            } else
            {
                ViewBag.Errore = "Username e/o password sbagliati";
            }

            return View();
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }

        public ActionResult RegistrationEmployee()
        {
            ViewBag.IdRole = new SelectList(DbContext.Roles, "IdRole", "TypeRole");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrationEmployee(Employees employee)
        {
            //employee.Roles = DbContext.Roles.Where( role => role.IdRole == employee.IdRole ).FirstOrDefault();

            if (ModelState.IsValid)
            {
                if (employee.FileImg != null)
                {
                    string Path = Server.MapPath("~/Content/Assets/Img/Employees/" + employee.FileImg.FileName);
                    employee.FileImg.SaveAs(Path);
                    employee.UrlImg = employee.FileImg.FileName;
                    DbContext.Employees.Add(employee);
                    DbContext.SaveChanges();
                    return RedirectToAction("SignIn");
                } else
                {
                    ViewBag.Error = "Alcuni campi non sono stati completati!";
                    return View();
                }
            }
            ViewBag.IdRole = new SelectList(DbContext.Roles, "IdRole", "TypeRole", employee.IdRole);
            return View(employee);
        }

        public ActionResult RegistrationCustomer()
        {
            ViewBag.IdRole = new SelectList(DbContext.Roles, "IdRole", "TypeRole");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrationCustomer(Customers customer)
        {
            if (!ModelState.IsValid)
            {
                if(customer.FileImg != null)
                {
                    string Path = Server.MapPath("~/Content/Assets/Img/Customers/" + customer.FileImg.FileName);
                    customer.FileImg.SaveAs(Path);
                    customer.UrlImg = customer.FileImg.FileName;
                    DbContext.Customers.Add(customer);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index", "Home");
                } else
                {
                    ViewBag.Errore = "Alcuni campi non sono stati compilati!";
                }
            }
            return View();
        }
    }
}