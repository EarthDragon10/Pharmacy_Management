namespace Pharmacy_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Medicines
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Medicines()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        public int IdMedicine { get; set; }
        [Display(Name = "Nome Della Medicina")]
        [StringLength(30)]
        public string NameMedicine { get; set; }

        public int IdTypeProduct { get; set; }

        public int? IdTypeMedicine { get; set; }
        [Display(Name = "Per cosa usare")]
        [Required]
        public string DescriptionUse { get; set; }
        [Display(Name = "Immagine")]
        public string UrlImg { get; set; }

        [NotMapped()]
        public HttpPostedFileBase FileImg { get; set; }

        public int IdSupplierCompanies { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]

        [Display(Name = "Prezzo")]
        public decimal price { get; set; }

        public int IdDrawer { get; set; }
        [Display(Name = "Giacenza")]
        public int Stock { get; set; }

        public virtual Drawers Drawers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }

        public virtual SupplierCompanies SupplierCompanies { get; set; }

        public virtual TypeMedicine TypeMedicine { get; set; }

        public virtual TypeProduct TypeProduct { get; set; }
    }
}
