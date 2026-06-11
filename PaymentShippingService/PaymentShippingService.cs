using System.Collections.Generic;
using PaymentShippingModel;
using PaymentShippingDataService;

namespace PaymentShippingService
{
    public class PaymentShippingService
    {

        private IPaymentShippingDataService data;

        public PaymentShippingService(IPaymentShippingDataService dataService)
        {
            data = dataService;
        }
        // ─── Payment ────────────────────────────────────────────────────────
        public void AddPayment(string method, string name, string number)
        {
            data.AddPayment(new Payment(method, name, number));
        }
        public void AddCreditCardPayment(string nameOnCard, string cardNumber, string expiry, string cvv)
        {
            data.AddCreditCardPayment(nameOnCard, cardNumber, expiry, cvv);
        }
        public void AddBankAccountPayment(string bankName, string accountHolder, string accountNumber)
        {
            data.AddBankAccountPayment(bankName, accountHolder, accountNumber);
        }
        public List<Payment> ViewPayments()
        {
            return data.GetPayments();
        }

        public void UpdatePayment(int id, string method, string name, string number)
        {
            data.UpdatePayment(id, new Payment(method, name, number));
        }
        public void UpdateCreditCardPayment(int id, string nameOnCard, string cardNumber, string expiry, string cvv)
        {
            data.UpdateCreditCardPayment(id, nameOnCard, cardNumber, expiry, cvv);
        }
        public void UpdateBankAccountPayment(int id, string bankName, string accountHolder, string accountNumber)
        {
            data.UpdateBankAccountPayment(id, bankName, accountHolder, accountNumber);
        }
        public void DeletePayment(int id)
        {
            data.DeletePayment(id);
        }
        // ─── Helper: Parse Credit Card fields from stored Payment ────────────
        public (string CardNumber, string Expiry, string CVV) ParseCreditCard(Payment p)
        {
            var parts = p.AccountNumber.Split('|');
            return parts.Length == 3
                ? (parts[0], parts[1], parts[2])
                : (p.AccountNumber, "", "");
        }
        // ─── Helper: Parse Bank Account fields from stored Payment ───────────
        public (string BankName, string AccountHolder) ParseBankAccount(Payment p)
        {
            var parts = p.AccountName.Split('|');
            return parts.Length == 2
                ? (parts[0], parts[1])
                : (p.AccountName, "");
        }
        // ─── Shipping ───────────────────────────────────────────────────────
        public void AddShipping(string name, string address, double latitude, double longitude)
        {
            data.AddShipping(new Shipping(name, address, latitude, longitude));
        }

        public List<Shipping> ViewShipping()
        {
            return data.GetShippings();
        }
        public void UpdateShipping(int id, string name, string address, double latitude, double longitude)
        {
            data.UpdateShipping(id, new Shipping(name, address, latitude, longitude));
        }

        public void DeleteShipping(int id)
        {
            data.DeleteShipping(id);
        }
    }
}