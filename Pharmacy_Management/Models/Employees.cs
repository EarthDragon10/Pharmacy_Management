namespace Pharmacy_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Employees
    {
        [Key]
        public int IdEmployee { get; set; }

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30)]
        public string Pwd { get; set; }
        public string UrlImg { get; set; }
        [NotMapped()]
        public HttpPostedFileBase FileImg { get; set; }

        public int IdRole { get; set; }

        public virtual Roles Roles { get; set; }
    }
}
