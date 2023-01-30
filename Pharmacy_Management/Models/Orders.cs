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

        public int Quantity { get; set; }

        public int? IdPrescription { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOrder { get; set; }

        public virtual Customers Customers { get; set; }

        public virtual Medicines Medicines { get; set; }

        public virtual Pescritions Pescritions { get; set; }
    }
}
