using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegularExpressionLab
{
    /// <summary>
    /// Клас для пошуку поштових індексів у тексті.
    /// Формат індексу: 5 цифр поспіль.
    /// </summary>
    public class PostalCodeFinder
    {
        // Статичний компільований регулярний вираз для продуктивності
        private static readonly Regex _postalRegex = new Regex(
            @"(?<!\d)\d{5}(?!\d)", 
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        /// <summary>
        /// Пошук поштових індексів у тексті.
        /// </summary>
        /// <param name="text">Вхідний текст</param>
        /// <returns>Список знайдених поштових індексів</returns>
        public List<string> FindPostalCodes(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>(); // Порожній список для null або пустого тексту

            var postalCodes = new List<string>();

            MatchCollection matches = _postalRegex.Matches(text);

            foreach (Match match in matches)
            {
                postalCodes.Add(match.Value);
            }

            return postalCodes;
        }
    }

    class Program
    {
        static void Main()
        {
            var finder = new PostalCodeFinder();

            Console.WriteLine("Введіть текст для пошуку поштових індексів:");
            string input = Console.ReadLine();

            List<string> results = finder.FindPostalCodes(input);

            if (results.Count == 0)
            {
                Console.WriteLine("Збігів не знайдено.");
            }
            else
            {
                Console.WriteLine("Знайдені поштові індекси:");
                foreach (var code in results)
                {
                    Console.WriteLine(code);
                }
            }

            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
