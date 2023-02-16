using Pharmacy_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy_Management.Controllers
{
    public class HomeController : Controller
    {
        private ModelDbContext DbContext = new ModelDbContext();
        public ActionResult Index()
        {
            var UserLogged = DbContext.Employees.Where(u => u.Username == User.Identity.Name).FirstOrDefault();
            var UserRole = DbContext.Roles.Where(u => u.IdRole == UserLogged.IdRole).FirstOrDefault();
            Users.Name = UserLogged.FirstName;
            Users.Role = UserRole.TypeRole;
            Users.ImgUser = UserLogged.UrlImg;
            Users.IdEmployee = UserLogged.IdEmployee;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}