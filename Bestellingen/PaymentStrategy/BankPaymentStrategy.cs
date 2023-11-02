using System.Text.RegularExpressions;
using System;

namespace Bestellingen.PaymentStrategy
{
    internal class BankPaymentStrategy : IPaymentStrategy
    {
        public void Pay(double amount)
        {
            Console.WriteLine("Please write your bank address:");
            string address = Console.ReadLine();
            Console.WriteLine($"Thanks for address: {address}");
        }
    }
}