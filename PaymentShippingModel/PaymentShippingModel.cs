using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentShippingModel
{

    public class Payment
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public string AccountName { get; set; }
        public string? AccountNumber { get; set; }

        public string? CardExpiry { get; set; }
        public string? CardCVV { get; set; }
        public string? BankName { get; set; }
        public string? AccountHolder { get; set; }

        public Payment() { }

        public Payment(int id, string method, string name, string number)
        {
            Id = id;
            Method = method;
            AccountName = name;
            AccountNumber = number;
        }

        public Payment(string method, string name, string number)
        {
            Method = method;
            AccountName = name;
            AccountNumber = number;
        }

        public Payment(string nameOnCard, string cardNumber, string expiry, string cvv)
        {
            Method = "Credit Card";
            AccountName = nameOnCard;
            AccountNumber = cardNumber;
            CardExpiry = expiry;
            CardCVV = cvv;
        }

        public Payment(int id, string nameOnCard, string cardNumber, string expiry, string cvv)
        {
            Id = id;
            Method = "Credit Card";
            AccountName = nameOnCard;
            AccountNumber = cardNumber;
            CardExpiry = expiry;
            CardCVV = cvv;
        }

        public Payment(string bankName, string accountHolder, string accountNumber, bool isBankAccount)
        {
            Method = "Bank Account";
            BankName = bankName;
            AccountHolder = accountHolder;
            AccountName = $"{bankName}|{accountHolder}";
            AccountNumber = accountNumber;
        }

        public Payment(int id, string bankName, string accountHolder, string accountNumber, bool isBankAccount)
        {
            Id = id;
            Method = "Bank Account";
            BankName = bankName;
            AccountHolder = accountHolder;
            AccountName = $"{bankName}|{accountHolder}";
            AccountNumber = accountNumber;
        }

        public string MaskedCardNumber =>
            Method == "Credit Card" && AccountNumber?.Length >= 4
                ? $"**** **** **** {AccountNumber.Substring(AccountNumber.Length - 4)}"
                : AccountNumber;

        public string MaskedAccountNumber =>
            Method == "Bank Account" && AccountNumber?.Length >= 4
                ? $"********{AccountNumber.Substring(AccountNumber.Length - 4)}"
                : AccountNumber;
    }

    public class Shipping
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

      
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Shipping() { }

        public Shipping(int id, string name, string address, double latitude, double longitude)
        {
            Id = id;
            Name = name;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Shipping(string name, string address, double latitude, double longitude)
        {
            Name = name;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }

    
        public Shipping(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public Shipping(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}