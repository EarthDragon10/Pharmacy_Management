namespace Pharmacy_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Customers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customers()
        {
            Orders = new HashSet<Orders>();
            Pescritions = new HashSet<Pescritions>();
        }

        [Key]
        public int IdCustomer { get; set; }

        //[Required]
        //[StringLength(30)]
        //public string Username { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Cognome")]
        public string LastName { get; set; }

        //[Required]
        //[StringLength(30)]
        //public string Pwd { get; set; }

        [Required]
        [StringLength(16)]
        [Display(Name = "Codice Fiscale")]
        public string CodFisc { get; set; }

        //public string UrlImg { get; set; }
        //[NotMapped()]
        //public HttpPostedFileBase FileImg { get; set; }

        public int IdRole { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pescritions> Pescritions { get; set; }

        public virtual Roles Roles { get; set; }
        [NotMapped()]
        public List<Orders> ListOrders = new List<Orders>();
    }
}
