using System;

namespace PaymentService.Models
{
    [Serializable]
    public class Transaction
    {
        public Transaction(string creditCardNumber, string creditCardTye, double amount, string receiver)
        {
            CreditCardNumber = creditCardNumber;
            CreditCardType = creditCardTye;
            Amount = amount;
            ReceiverName = receiver;
        }

        public string CreditCardNumber { get; set; }

        public string CreditCardType { get; set; }

        public double Amount { get; set; }

        public string ReceiverName { get; set; }
    }
}
