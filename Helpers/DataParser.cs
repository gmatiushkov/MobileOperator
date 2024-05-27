using System;
using System.Globalization;

namespace MobileOperator.Helpers
{
    public static class DataParser
    {
        public static int ParseYearOfBirth(string input)
        {
            int year = -1;
            if (DataValidator.ValidateYearOfBirth(input))
            {
                year = int.Parse(input);
            }
            return year;
        }

        public static DateTime ParseDate(string input)
        {
            DateTime date = DateTime.MinValue;
            if (DataValidator.ValidateDate(input))
            {
                date = DateTime.ParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            return date;
        }
    }
}
