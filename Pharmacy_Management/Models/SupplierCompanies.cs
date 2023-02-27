namespace Pharmacy_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SupplierCompanies
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierCompanies()
        {
            Medicines = new HashSet<Medicines>();
        }

        [Key]
        public int IdSupplierCompanies { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Nominativo della Ditta Fornitrice")]
        public string NameCompany { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Indirizzo")]
        public string Address { get; set; }
        [Display(Name = "Numero di Telefono")]
        public int PhoneNumber { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "E-Mail")]
        public string Mail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medicines> Medicines { get; set; }
    }
}
