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
    }
}
