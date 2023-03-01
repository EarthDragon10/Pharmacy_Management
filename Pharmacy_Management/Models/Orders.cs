namespace Pharmacy_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [Key]
        public int IdOrder { get; set; }

        public int IdCustomer { get; set; }

        public int IdMedicine { get; set; }
        [Display(Name = "Quantitá")]
        public int Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Display(Name = "Prezzo Totale")]
        public decimal TotalPrice { get; set; }

        public int? IdPrescription { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Ordine")]
        public DateTime DateOrder { get; set; }

        public virtual Customers Customers { get; set; }

        public virtual Medicines Medicines { get; set; }

        public virtual Pescritions Pescritions { get; set; }

        public static int? StaticIdCustomer { get; set; }
        public static int? StaticIdMedicine { get; set; }

        public static List<int> ListIdCustomer = new List<int>();
        public static List<int> ListIdMedicine = new List<int>();

        [NotMapped()]
        public class PreOrder
        {
            public string NameProduct { get; set; }
            public int IdMedicine { get; set; }
            public string TypeProduct { get; set; }
            public string TypeMedicine { set; get; }
            public string DescriptionUse { get; set; }
            public string UrlImg { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }

            public static List<PreOrder> PreOrderList = new List<PreOrder>();
        }

    }
}
