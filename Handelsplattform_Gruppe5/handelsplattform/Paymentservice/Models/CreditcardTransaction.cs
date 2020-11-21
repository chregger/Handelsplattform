using System;


namespace Paymentservice.Models
{
    [Serializable]
    
    public class CreditcardTransaction 
    {
        public CreditcardTransaction(string t, string cn, string ct, double a, string r)
        {
            Type = t;
            CreditcardNumber = cn;
            CreditcardType = ct;
            Amount = a;
            ReceiverName = r;
        }

        public string Type { get; set; }
        public string CreditcardNumber { get; set; }
        public string CreditcardType { get; set; }
        public double Amount { get; set; }
        public string ReceiverName { get; set; }
    }
}
