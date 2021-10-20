using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Models
{
    public class Login
    {
        [Required]

        public string LoginId { get; set; }
        [Required]

        public string password { get; set; }
        public Login()
        {
            LoginId = "Nithesh";
            password = "Nit";
        }
        }
    
}
