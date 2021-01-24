using Microsoft.Extensions.Configuration;
using System;
using System.Text.RegularExpressions;

namespace StockWebAPI.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Checks if the company symbol is valid format.
        /// </summary>
        /// <param name="symbol">Stock symbol of company.</param>
        public static bool IsValid(this string symbol)
        {
            Regex rgx = new Regex("[^A-Za-z0-9]");
            if (!rgx.IsMatch(symbol) && !string.IsNullOrEmpty(symbol))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a string representing the api's base url.
        /// </summary>
        public static string GetBaseUrl(this string apiKey)
        {
            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", true, true)
              .Build();
            return config[$"{apiKey}"] ?? throw new ArgumentException("Please provide a valid Api key.");
        }
    }
}
