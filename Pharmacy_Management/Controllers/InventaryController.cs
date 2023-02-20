using Pharmacy_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy_Management.Controllers
{
    public class InventaryController : Controller
    {
        private ModelDbContext db = new ModelDbContext();
        // GET: Inventary
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListMedicines()
        {
            var medicines = db.Medicines.Include(m => m.Drawers).Include(m => m.SupplierCompanies).Include(m => m.TypeMedicine).Include(m => m.TypeProduct);
            var order = new Orders();
            return View(medicines.ToList());
        }
    }
}