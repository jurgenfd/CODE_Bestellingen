using Bestellingen.PaymentStrategy;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Bestellingen
{
    public static class Program
    {

        private static PaymentMethodFactory _factory;

        public static void Main(string[] args)
        {
            var reader = new StreamReader("./PaymentMethodsPerRestaurant.csv");
            _factory = new PaymentMethodFactory(reader);
            GoAndEat();
        }

        private static void GoAndEat()
        {
            Console.WriteLine("Please enter your favorite restaurant.");
            string restaurant = Console.ReadLine();

            double value = 29.95;
            Console.WriteLine($"Allright, how was your diner. The amount was {value}.");
            Console.WriteLine("How would you like to pay?");
            string paymentMethod = Console.ReadLine();

            IPaymentStrategy paymentStrategy = _factory.GetPaymentStrategy(restaurant, paymentMethod);
            paymentStrategy.Pay(value);

        }
    }
}
