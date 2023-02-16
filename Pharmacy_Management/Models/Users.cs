using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pharmacy_Management.Models
{
    public partial class Users
    {
        [NotMapped()]
        public string Username { get; set; }
        [NotMapped()]
        public string Password { get; set; }
        [NotMapped()] 
        public static int IdEmployee { get; set; }
        [NotMapped()]
        public static string Name { get; set; }
        [NotMapped()]
        public static string Role { get; set; }
        [NotMapped()]
        public static string ImgUser { get; set; }
    }
}