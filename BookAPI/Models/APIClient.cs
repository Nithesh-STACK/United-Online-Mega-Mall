using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Models
{
        public  class Book
        {
        [Key]

        public int Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public int Price { get; set; }
            public string Photo { get; set; }
            public string PlotDescription { get; set; }
    }
         
    public class Chaat
    {
        [Key]

        [Required]
        [Display(Name = "Id")]
        public int Cid { get; set; }
        [Display(Name = " ")]
        public string Cname { get; set; }
        [Display(Name = "Chaat")]
        public string Cimage { get; set; }
        [Display(Name = "Price")]
        public double? Cprice { get; set; }
    }

    public class Pro
    {
        [Key]


        public int Productid { get; set; }
        public string Productname { get; set; }
        public int? Productprice { get; set; }
        public string ProductImage { get; set; }

    }
    public class ShipmentDetail
        {
            [Required]
            public string BuyersName { get; set; }
            [Required]
            public int? Age { get; set; }
            [Required]
            public string AddressDetails { get; set; }
            [StringLength(maximumLength:10,ErrorMessage ="Mobile Number should be 10 numbers")]
            [Required]
            public double MobileNumber { get; set; }
        }
    public class PaymentDetail
    {
        [Required]
        public string CardHolderName { get; set; }
        [StringLength(maximumLength: 16, ErrorMessage = "Debit card Number should be 16 numbers")]
        [Required]
        public float? DebitCardNumber { get; set; }
        [Required]
        public DateTime? ExpiryDate { get; set; }
        [StringLength(maximumLength: 3, ErrorMessage = "Mobile Number should be 10 numbers")]
        [Required]
        public int CvvPin { get; set; }
    }


}
    

