using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using PaymentShippingModel;

namespace PaymentShippingDataService
{
    public class JsonDataService : IPaymentShippingDataService
    {
        private List<Payment> payments = new List<Payment>();
        private List<Shipping> shippings = new List<Shipping>();

        private string paymentFile;
        private string shippingFile;

        public JsonDataService()
        {
            paymentFile = $"{AppDomain.CurrentDomain.BaseDirectory}/Payments.json";
            shippingFile = $"{AppDomain.CurrentDomain.BaseDirectory}/Shippings.json";

            PopulateJsonFile();
        }

        private void PopulateJsonFile()
        {
            RetrievePayments();
            RetrieveShippings();

            if (payments.Count <= 0)
            {
                payments.Add(new Payment { Id = 1, Method = "GCash", AccountName = "Sample", AccountNumber = "09123456789" });
                SavePayments();
            }

            if (shippings.Count <= 0)
            {
                shippings.Add(new Shipping { Id = 1, Name = "Sample", Address = "Manila" });
                SaveShippings();
            }
        }


        private void SavePayments()
        {
            using (var stream = File.OpenWrite(paymentFile))
            {
                JsonSerializer.Serialize(
                    new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }),
                    payments);
            }
        }

        private void SaveShippings()
        {
            using (var stream = File.OpenWrite(shippingFile))
            {
                JsonSerializer.Serialize(
                    new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }),
                    shippings);
            }
        }


        private void RetrievePayments()
        {
            if (!File.Exists(paymentFile))
            {
                payments = new List<Payment>();
                return;
            }

            using (var reader = File.OpenText(paymentFile))
            {
                payments = JsonSerializer.Deserialize<List<Payment>>(
                    reader.ReadToEnd(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                ) ?? new List<Payment>();
            }
        }

        private void RetrieveShippings()
        {
            if (!File.Exists(shippingFile))
            {
                shippings = new List<Shipping>();
                return;
            }

            using (var reader = File.OpenText(shippingFile))
            {
                shippings = JsonSerializer.Deserialize<List<Shipping>>(
                    reader.ReadToEnd(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                ) ?? new List<Shipping>();
            }
        }

        public void AddPayment(Payment payment)
        {
            RetrievePayments();

            payment.Id = payments.Count > 0 ? payments.Max(x => x.Id) + 1 : 1;

            payments.Add(payment);
            SavePayments();
        }

        public void AddCreditCardPayment(string nameOnCard, string cardNumber, string expiry, string cvv)
        {
            RetrievePayments();

            var payment = new Payment(nameOnCard, cardNumber, expiry, cvv)
            {
                Id = payments.Count > 0 ? payments.Max(x => x.Id) + 1 : 1
            };

            payments.Add(payment);
            SavePayments();
        }

        public void AddBankAccountPayment(string bankName, string accountHolder, string accountNumber)
        {
            RetrievePayments();

            var payment = new Payment(bankName, accountHolder, accountNumber, true)
            {
                Id = payments.Count > 0 ? payments.Max(x => x.Id) + 1 : 1
            };

            payments.Add(payment);
            SavePayments();
        }

        public List<Payment> GetPayments()
        {
            RetrievePayments();
            return payments;
        }

        public void UpdatePayment(int id, Payment payment)
        {
            RetrievePayments();

            var existing = payments.FirstOrDefault(x => x.Id == id);

            if (existing != null)
            {
                existing.Method = payment.Method;
                existing.AccountName = payment.AccountName;
                existing.AccountNumber = payment.AccountNumber;
            }

            SavePayments();
        }

        public void UpdateCreditCardPayment(int id, string nameOnCard, string cardNumber, string expiry, string cvv)
        {
            RetrievePayments();

            var existing = payments.FirstOrDefault(x => x.Id == id);

            if (existing != null)
            {
                existing.Method = "Credit Card";
                existing.AccountName = nameOnCard;
                existing.AccountNumber = cardNumber;
                existing.CardExpiry = expiry;
                existing.CardCVV = cvv;
            }

            SavePayments();
        }

        public void UpdateBankAccountPayment(int id, string bankName, string accountHolder, string accountNumber)
        {
            RetrievePayments();

            var existing = payments.FirstOrDefault(x => x.Id == id);

            if (existing != null)
            {
                existing.Method = "Bank Account";
                existing.BankName = bankName;
                existing.AccountHolder = accountHolder;
                existing.AccountName = $"{bankName}|{accountHolder}";
                existing.AccountNumber = accountNumber;
            }

            SavePayments();
        }

        public void DeletePayment(int id)
        {
            RetrievePayments();

            payments.RemoveAll(x => x.Id == id);

            SavePayments();
        }

        public void AddShipping(Shipping shipping)
        {
            RetrieveShippings();

            shipping.Id = shippings.Count > 0 ? shippings.Max(x => x.Id) + 1 : 1;

            shippings.Add(shipping);
            SaveShippings();
        }

        public List<Shipping> GetShippings()
        {
            RetrieveShippings();
            return shippings;
        }

        public void UpdateShipping(int id, Shipping shipping)
        {
            RetrieveShippings();

            var existing = shippings.FirstOrDefault(x => x.Id == id);

            if (existing != null)
            {
                existing.Name = shipping.Name;
                existing.Address = shipping.Address;
            }

            SaveShippings();
        }

        public void DeleteShipping(int id)
        {
            RetrieveShippings();

            shippings.RemoveAll(x => x.Id == id);

            SaveShippings();
        }
    }
}