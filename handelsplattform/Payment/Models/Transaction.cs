using System;

namespace PaymentService.Models
{
    [Serializable]
    public class Transaction
    {
        public Transaction(string type, string creditcardnr, string creditcardtye, double amount, string receiver)
        {
            Type = type;
            CreditcardNumber = creditcardnr;
            CreditcardType = creditcardtye;
            Amount = amount;
            ReceiverName = receiver;
        }

        public string Type { get; set; }
        public string CreditcardNumber { get; set; }
        public string CreditcardType { get; set; }
        public double Amount { get; set; }
        public string ReceiverName { get; set; }
    }
}
