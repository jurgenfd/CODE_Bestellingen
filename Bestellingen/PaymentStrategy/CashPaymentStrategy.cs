using System;

namespace Bestellingen.PaymentStrategy
{
    internal class CashPaymentStrategy : IPaymentStrategy
    {
        public void Pay(double value)
        {
            while (value > 0)
            {
                Console.WriteLine($"Please enter coins for {Math.Round(value, 2)} euro.");
                double amount2 = double.Parse(Console.ReadLine());
                value -= amount2;
            }
            Console.WriteLine($"Here's your change: {-Math.Round(value, 2)}");
            Console.WriteLine("Please don't forget your change.");
        }
    }
}