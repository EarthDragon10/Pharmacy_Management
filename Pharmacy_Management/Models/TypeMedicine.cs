namespace Pharmacy_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeMedicine")]
    public partial class TypeMedicine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeMedicine()
        {
            Medicines = new HashSet<Medicines>();
        }

        [Key]
        public int IdTypeMedicine { get; set; }

        [Required]
        [StringLength(30)]
        public string DescTypeMedicine { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medicines> Medicines { get; set; }
    }
}
