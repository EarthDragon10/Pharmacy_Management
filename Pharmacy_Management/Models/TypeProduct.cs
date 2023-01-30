namespace Pharmacy_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeProduct")]
    public partial class TypeProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeProduct()
        {
            Medicines = new HashSet<Medicines>();
        }

        [Key]
        public int IdTypeProduct { get; set; }

        [Required]
        [StringLength(30)]
        public string DescTypeProduct { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medicines> Medicines { get; set; }
    }
}
