using Bestellingen.PaymentStrategy;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection.PortableExecutable;

namespace Bestellingen
{
    internal class PaymentMethodFactory
    {
        Dictionary<string, List<string>> restaurants = new();

        public PaymentMethodFactory(TextReader reader)
        {
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

        }

        public IPaymentStrategy GetPaymentStrategy(string restaurant, string paymentMethod)
        {
            if (restaurants.ContainsKey(restaurant))
            {
                if (restaurants[restaurant].Contains(paymentMethod))
                {

                    switch (paymentMethod)
                    {
                        case "creditcard":
                            return new CcPaymentStrategy();
                        case "cash":
                            return new CashPaymentStrategy();
                        case "bank":
                            return new BankPaymentStrategy();
                        default:
                            return null;
                    }
                }
                else
                {
                    throw new ArgumentException($"Restaurant {restaurant} does not have payment method: {paymentMethod}");
                }
            }
            else
            {
                throw new ArgumentException($"Restaurant does not have payment method: {paymentMethod}");
            }
        }
    }
}
