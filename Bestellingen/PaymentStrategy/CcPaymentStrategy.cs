using System.Text.RegularExpressions;
using System;

namespace Bestellingen.PaymentStrategy
{
    internal class CcPaymentStrategy : IPaymentStrategy
    {
        public void Pay(double amount)
        {
            Console.WriteLine("Please give your full name:");
            string name = Console.ReadLine();
            string number = "";
            var validCreditCardNumberRegEx = new Regex(@"^\d{16}$");
            while (!validCreditCardNumberRegEx.IsMatch(number))
            {
                Console.WriteLine("Please enter a sixteen digit credit card number:");
                number = Console.ReadLine();
            }
            Console.WriteLine("Finally your cvc:");
            string cvc = Console.ReadLine();
            Console.WriteLine($"Thanks {name} for {number} with cvc: {cvc} for value: {amount}");
            Console.WriteLine("Please don't forget your credit card.");
        }
    }
}