using Pharmacy_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}