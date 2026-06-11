using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using PaymentShippingModel;

namespace PaymentShippingDataService
{
    public class PaymentShippingDataService
    {
        private List<Payment> payments = new List<Payment>();
        private List<Shipping> shippings = new List<Shipping>();
        public void AddPayment(Payment payment)
        {
            payments.Add(payment);
        }

        public void AddCreditCardPayment(string nameOnCard, string cardNumber, string expiry, string cvv)
        {
            payments.Add(new Payment(nameOnCard, cardNumber, expiry, cvv));
        }

        public void AddBankAccountPayment(string bankName, string accountHolder, string accountNumber)
        {
            payments.Add(new Payment(bankName, accountHolder, accountNumber, true));
        }

        public List<Payment> GetPayments()
        {
            return payments;
        }

        public void UpdatePayment(int index, Payment payment)
        {
            if (index >= 0 && index < payments.Count)
            {
                payments[index] = payment;
            }
        }

        public void UpdateCreditCardPayment(int index, string nameOnCard, string cardNumber, string expiry, string cvv)
        {
            if (index >= 0 && index < payments.Count)
            {
                payments[index] = new Payment(nameOnCard, cardNumber, expiry, cvv);
            }
        }

        public void UpdateBankAccountPayment(int index, string bankName, string accountHolder, string accountNumber)
        {
            if (index >= 0 && index < payments.Count)
            {
                payments[index] = new Payment(bankName, accountHolder, accountNumber, true);
            }
        }

        public void DeletePayment(int index)
        {
            if (index >= 0 && index < payments.Count)
            {
                payments.RemoveAt(index);
            }
        }

        public void AddShipping(Shipping shipping)
        {
            shippings.Add(shipping);
        }

        public List<Shipping> GetShippings()
        {
            return shippings;
        }

        public void UpdateShipping(int index, Shipping shipping)
        {
            if (index >= 0 && index < shippings.Count)
            {
                shippings[index] = shipping;
            }
        }

        public void DeleteShipping(int index)
        {
            if (index >= 0 && index < shippings.Count)
            {
                shippings.RemoveAt(index);
            }
        }
    }
}