using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookAPI.Models
{
    public class AddCart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int? Price { get; set; }
        public string Photo { get; set; }
        public string PlotDescription { get; set; }

        public virtual Book IdNavigation { get; set; }

    }
    public class ChaatsCart
    {
        public int Cid { get; set; }
        public string Cname { get; set; }
        public string Cimage { get; set; }
        public double? Cprice { get; set; }
        public virtual Chaat CidNavigation { get; set; }

    }
    public class ProductsCart
    {
        public int Productid { get; set; }
        public string Productname { get; set; }
        public int? Productprice { get; set; }
        public string ProductImage { get; set; }
        public virtual Pro IdNavigation { get; set; }

    }
}
    
    