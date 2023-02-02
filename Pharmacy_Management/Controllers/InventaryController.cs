using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy_Management.Controllers
{
    public class InventaryController : Controller
    {
        // GET: Inventary
        public ActionResult Index()
        {
            return View();
        }
    }
}