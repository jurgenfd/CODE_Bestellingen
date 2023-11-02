using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Bestellingen {
    public static class Program {
        public static void Main(string[] args) {
            Dictionary<string, List<string>> restaurants = new();
            var reader = new StreamReader("./PaymentMethodsPerRestaurant.csv");
            var csvReader = new CsvReader(reader, new CsvConfiguration(
                CultureInfo.InvariantCulture) { Delimiter = ";" });
            var records = csvReader.GetRecords<RestaurantPaymentMethod>();

            foreach (var record in records) {
                Console.WriteLine($"{record.Restaurant} accepts: {record.PaymentMethod}");
                if (!restaurants.ContainsKey(record.Restaurant)) {
                    restaurants[record.Restaurant] = new();
                }
                restaurants[record.Restaurant].Add(record.PaymentMethod);
            }
        }
    }
}
