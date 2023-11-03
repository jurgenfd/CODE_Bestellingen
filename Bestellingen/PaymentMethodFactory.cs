using Bestellingen.PaymentStrategy;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace Bestellingen
{
    internal class PaymentMethodFactory
    {
        Dictionary<string, List<string>> restaurants = new();
        Dictionary<string, Type> paymentTypes = new();

        public PaymentMethodFactory(TextReader reader)
        {
            ReadRestaurantPaymentMethods(reader);
            PreloadPaymentMethods();

        }

        private void PreloadPaymentMethods()
        {
            var interfaceType = typeof(IPaymentStrategy);
            var implementingTypes = AppDomain.CurrentDomain.GetAssemblies() // Get all assemblies
                                        .SelectMany(a => a.GetTypes()) // Get all types
                                        .Where(type => interfaceType.IsAssignableFrom(type) && // that implement the interface
                                            !type.IsAbstract && // and are not abstract
                                            !type.IsInterface); // and are not interfaces themselves
            foreach (var type in implementingTypes)
            {
                string name = type.Name.ToLower().Replace("paymentstrategy", "");
                paymentTypes[name] = type;
            }
        }


        private void ReadRestaurantPaymentMethods(TextReader reader)
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
                    Type paymentType = paymentTypes[paymentMethod];
                    return (IPaymentStrategy)Activator.CreateInstance(paymentType);
                } else
                {
                    throw new ArgumentException($"Restaurant {restaurant} does not have payment method: {paymentMethod}");
                }
            } else
            {
                throw new ArgumentException($"Restaurant does not have payment method: {paymentMethod}");
            }
        }
    }
}
