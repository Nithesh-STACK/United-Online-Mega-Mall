using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Assign.Models
{
    public class Drink
    {
        [Required]
        [Display(Name = "Id")]
        public int Did { get; set; }
        [Display(Name = " ")]
        public string Dname { get; set; }
        [Display(Name = "Drink")]
        
        public string Dimage { get; set; }
        [Display(Name = "Price")]
        public double? Dprice { get; set; }
    }
}
