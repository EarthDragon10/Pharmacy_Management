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
        public string NameCompany { get; set; }

        [Required]
        [StringLength(30)]
        public string Address { get; set; }

        public int PhoneNumber { get; set; }

        [Required]
        [StringLength(30)]
        public string Mail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medicines> Medicines { get; set; }
    }
}
