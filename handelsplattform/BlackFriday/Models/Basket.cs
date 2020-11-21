namespace BlackFriday.Models
{
    public class Basket
    {
        public Basket(string product, string vendor, string customCreditCardNumber, double amountInEuro)
        {
            Product = product;
            Vendor = vendor;
            CustomerCreditCardNumber = customCreditCardNumber;
            AmountInEuro = amountInEuro;
        }

        public string Product { get; set; }
        public string Vendor { get; set; }
        public string CustomerCreditCardNumber { get; set; }
        public double AmountInEuro { get; set; }
    }
}
