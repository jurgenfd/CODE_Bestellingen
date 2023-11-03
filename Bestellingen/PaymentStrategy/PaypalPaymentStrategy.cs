using System.Text.RegularExpressions;
using System;

namespace Bestellingen.PaymentStrategy
{
    internal class PaypalPaymentStrategy : IPaymentStrategy
    {
        public void Pay(double amount)
        {
            Console.WriteLine("Please give your login name:");
            string login = Console.ReadLine();
            string passwd = "";
            var validPasswd = new Regex(@"^\d{8}$");
            while (!validPasswd.IsMatch(passwd))
            {
                Console.WriteLine("Please enter your eight digit password:");
                passwd = Console.ReadLine();
            }
            Console.WriteLine($"Thanks for your payment with credentials as login: {login}");
        }
    }
}