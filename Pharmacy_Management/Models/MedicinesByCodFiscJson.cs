using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Pharmacy_Management.Models
{
    public class MedicinesByCodFiscJson
    {
        public int IdCustomer { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CodFisc { get; set; }
        public int IdMedicine { get; set; }
        public int Quantity { get; set; }
        public string NameMedicine { get; set; }
        public string UrlImg { get; set; }
        public int IdDrawer { get; set; }
        public int IdentifierDrawer { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         
        public DateTime DateOrder { get; set; }
       
    }
}