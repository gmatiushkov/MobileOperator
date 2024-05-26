using System;
using System.Text.RegularExpressions;

namespace MobileOperator.Processing
{
    public static class DataValidator
    {
        public static bool ValidatePassportNumber(string input)
        {
            // Паттерн для номера паспорта в формате "NNNN-NNNNNN", где N – цифры
            string pattern = @"^\d{4}-\d{6}$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool ValidateFullName(string input)
        {
            // Паттерн для полного имени кириллицей в формате "Иванов Иван Иванович"
            string pattern = @"^(?=.{8,64}$)[А-ЯЁа-яё\s-]+$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool ValidateYearOfBirth(string input)
        {
            // Паттерн для года рождения в формате "2004" или "1991"
            string pattern = @"^(19\d{2}|20\d{2})$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool ValidateAddress(string input)
        {
            // Паттерн для адреса на кириллице с допустимыми символами ".", ",", "-"
            string pattern = @"^[А-ЯЁа-яё\d\s"".,-]{1,256}$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool ValidateSimNumber(string input)
        {
            // Паттерн для номера SIM-карты в формате "NNN-NNNNNNN", где N – цифры
            string pattern = @"^\d{3}-\d{7}$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool ValidateTariff(string input)
        {
            // Паттерн для названия тарифа (любые буквы и цифры)
            string pattern = @"^[A-Za-zА-ЯЁа-яё\d\s-]{1,64}$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool ValidateDate(string input)
        {
            // Паттерн для проверки даты в формате "дд.мм.гггг"
            string pattern = @"^(\d{2})\.(\d{2})\.(\d{4})$";
            return Regex.IsMatch(input, pattern) && DateTime.TryParse(input, out _);
        }
    }
}
