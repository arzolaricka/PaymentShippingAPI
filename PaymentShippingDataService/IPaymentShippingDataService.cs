using System.Collections.Generic;
using PaymentShippingModel;

namespace PaymentShippingDataService
{
    public interface IPaymentShippingDataService
    {
        void AddPayment(Payment payment);
        void AddCreditCardPayment(string nameOnCard, string cardNumber, string expiry, string cvv);
        void AddBankAccountPayment(string bankName, string accountHolder, string accountNumber);
        List<Payment> GetPayments();
        void UpdatePayment(int id, Payment payment);
        void UpdateCreditCardPayment(int id, string nameOnCard, string cardNumber, string expiry, string cvv);
        void UpdateBankAccountPayment(int id, string bankName, string accountHolder, string accountNumber);
        void DeletePayment(int id);

        void AddShipping(Shipping shipping);
        List<Shipping> GetShippings();
        void UpdateShipping(int id, Shipping shipping);
        void DeleteShipping(int id);
    }
}
