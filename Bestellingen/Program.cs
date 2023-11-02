using Bestellingen.PaymentStrategy;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Bestellingen
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Dictionary<string, List<string>> restaurants = new();
            var reader = new StreamReader("./PaymentMethodsPerRestaurant.csv");
            var csvReader = new CsvReader(reader, new CsvConfiguration(
                CultureInfo.InvariantCulture)
            { Delimiter = ";" });
            var records = csvReader.GetRecords<RestaurantPaymentMethod>();

            foreach (var record in records)
            {
                Console.WriteLine($"{record.Restaurant} accepts: {record.PaymentMethod}");
                if (!restaurants.ContainsKey(record.Restaurant))
                {
                    restaurants[record.Restaurant] = new();
                }
                restaurants[record.Restaurant].Add(record.PaymentMethod);


            }

            GoAndEat(restaurants);

        }

        private static void GoAndEat(Dictionary<string, List<string>> restaurants)
        {
            Console.WriteLine("Please enter your favorite restaurant.");
            string restaurant = Console.ReadLine();

            double value = 29.95;
            Console.WriteLine($"Allright, how was your diner. The amount was {value}.");
            Console.WriteLine("How would you like to pay?");
            string paymentMethod = Console.ReadLine();

            if (restaurants.ContainsKey(restaurant) && restaurants[restaurant].Contains(paymentMethod))
            {
                Console.WriteLine("Thank you for your payment.");
                IPaymentStrategy paymentStrategy;
                switch (paymentMethod)
                {
                    case "creditcard":
                        paymentStrategy = new CcPaymentStrategy();
                        break;
                    case "cash":
                        paymentStrategy = new CashPaymentStrategy();
                        break;
                    case "bank":
                        paymentStrategy = new BankPaymentStrategy();
                        break;
                    default:
                        throw new NotImplementedException();
                }
                paymentStrategy.Pay(value);
            }
            else
            {
                Console.WriteLine($"Sorry, we don't accept payment method: {paymentMethod}.");
            }

        }


    }
}
