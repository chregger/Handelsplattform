using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackFriday.Models
{
    public class Basket
    {
        public Basket(string Product, string Vendor, string CustomCreditCardnumber, double AmountInEuro)
        {
            this.Product = Product;
            this.Vendor = Vendor;
            this.CustomerCreditCardnumber = CustomerCreditCardnumber;
            this.AmountInEuro = AmountInEuro;
        }

        public string Product { get; set; }
        public string Vendor { get; set; }
        public string CustomerCreditCardnumber { get; set; }
        public double AmountInEuro { get; set; }
    }
}
