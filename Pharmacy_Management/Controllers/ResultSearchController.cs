using Pharmacy_Management.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pharmacy_Management.Controllers
{
    [Authorize]
    public class ResultSearchController : Controller
    {
        private ModelDbContext DbContext = new ModelDbContext();
        // GET: ResultSearch
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchByCodFisc()
        {
            return View();
        }
        public ActionResult SearchByDate()
        {
            return View();
        }
        // Funzioni ricerca in Jquery
        public JsonResult GetMedicineByCodFisc(string codFisc)
        {
            var customer = DbContext.Customers.Where(c => c.CodFisc == codFisc).Include(c => c.Orders).FirstOrDefault();

            List<MedicinesByCodFiscJson> ListBCFJ = new List<MedicinesByCodFiscJson>();

            if (customer != null)
            {
                var ListResults = DbContext.Orders.Where(o => o.IdCustomer == customer.IdCustomer).ToList();

                foreach (var result in ListResults)
                {
                    MedicinesByCodFiscJson SingleMedicine = new MedicinesByCodFiscJson();

                    // INFO CUSTOMER
                    SingleMedicine.CodFisc = customer.CodFisc;
                    SingleMedicine.FirstName = customer.FirstName;
                    SingleMedicine.LastName = customer.LastName;
                    // INFO ORDER
                    SingleMedicine.IdMedicine = result.IdMedicine;
                    SingleMedicine.Quantity = result.Quantity;
                    // INFO MEDICINE
                    Medicines medicineFind = DbContext.Medicines.Find(result.IdMedicine);
                    SingleMedicine.NameMedicine = medicineFind.NameMedicine;
                    SingleMedicine.UrlImg = medicineFind.UrlImg;
                    // INFO DRAWER
                    Drawers drawer = DbContext.Drawers.Find(medicineFind.IdDrawer);
                    SingleMedicine.IdDrawer = medicineFind.IdDrawer;
                    SingleMedicine.IdentifierDrawer = drawer.Identifier;

                    ListBCFJ.Add(SingleMedicine);
                }

                return Json(ListBCFJ, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("ERRORE", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetMedicineByDate(DateTime DataSelected)
        {
            var orders = DbContext.Orders.Where(o => o.DateOrder == DataSelected).Include(o => o.Medicines).ToList();
            List<MedicinesByCodFiscJson> ListMedicineSearched = new List<MedicinesByCodFiscJson>();

            if (orders != null)
            {
                foreach (var order in orders)
                {
                    MedicinesByCodFiscJson SingleMedicine = new MedicinesByCodFiscJson();
                    SingleMedicine.IdMedicine = order.IdMedicine;
                    SingleMedicine.NameMedicine = order.Medicines.NameMedicine;
                    SingleMedicine.Quantity = order.Quantity;
                    SingleMedicine.UrlImg = order.Medicines.UrlImg;
                    SingleMedicine.DateOrder = order.DateOrder;

                    ListMedicineSearched.Add(SingleMedicine);
                }
                return Json(ListMedicineSearched, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("ERRORE", JsonRequestBehavior.AllowGet);
            }

        }
    }
}