using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegularExpressionLab
{
    // Клас для роботи з поштовими індексами
    public class PostalCodeFinder
    {
        // Інкапсульоване поле для збереження тексту
        private readonly string _inputText;

        // Конструктор
        public PostalCodeFinder(string inputText)
        {
            _inputText = inputText;
        }

        // Метод для пошуку індексів
        public List<string> FindPostalCodes()
        {
            var postalCodes = new List<string>();
            string pattern = @"\b\d{5}\b"; // регулярний вираз для формату 00000

            MatchCollection matches = Regex.Matches(_inputText, pattern);

            foreach (Match match in matches)
            {
                postalCodes.Add(match.Value);
            }

            return postalCodes;
        }
    }

    // Тестовий клас програми
    internal class Program
    {
        private static void Main()
        {
            string text = "Адреси: Київ 01001, Львів 79000, Рівне 33028, а також код 123456 не підходить.";
            
            PostalCodeFinder finder = new PostalCodeFinder(text);
            List<string> postalCodes = finder.FindPostalCodes();

            Console.WriteLine("Знайдені поштові індекси:");
            foreach (string code in postalCodes)
            {
                Console.WriteLine(code);
            }
        }
    }
}
