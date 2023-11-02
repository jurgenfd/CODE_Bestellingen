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
                if (paymentMethod == "creditcard")
                {
                    Console.WriteLine("Please give your full name:");
                    string name = Console.ReadLine();
                    string number = "";
                    var validCreditCardNumberRegEx = new Regex(@"^\d{16}$");
                    while (!validCreditCardNumberRegEx.IsMatch(number))
                    {
                        Console.WriteLine("Please enter a credit card number:");
                        number = Console.ReadLine();
                    }
                    Console.WriteLine("Finally your cvc:");
                    string cvc = Console.ReadLine();
                    Console.WriteLine($"Thanks {name} for {number} with cvc: {cvc}");
                    Console.WriteLine("Please don't forget your credit card.");
                }
                else if (paymentMethod == "cash")
                {
                    while (value > 0)
                    {
                        Console.WriteLine($"Please enter coins for {Math.Round(value,2)} euro.");
                        double amount = double.Parse(Console.ReadLine());
                        value -= amount;
                    }
                    Console.WriteLine($"Here's your change: {-Math.Round(value,2)}");
                    Console.WriteLine("Please don't forget your change.");
                }
            }
            else
            {
                Console.WriteLine($"Sorry, we don't accept payment method: {paymentMethod}.");
            }

        }
    }
}
