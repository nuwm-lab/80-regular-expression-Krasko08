using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LabRegex
{
    /// <summary>
    /// Клас для пошуку поштових індексів у форматі 00000 у тексті.
    /// Використовує точний регулярний вираз: (?<!\d)\d{5}(?!\d)
    /// </summary>
    public static class PostalCodeFinder
    {
        // Компіляція і кешування регулярного виразу для більшої продуктивності
        private static readonly Regex PostalRegex = new Regex(
            pattern: @"(?<!\d)\d{5}(?!\d)",   // рівно п'ять цифр, не частина довшого числа
            options: RegexOptions.Compiled | RegexOptions.CultureInvariant,
            matchTimeout: TimeSpan.FromMilliseconds(200) // щоб уникнути зависань у великих текстах
        );

        /// <summary>
        /// Повертає список знайдених поштових індексів у тексті.
        /// </summary>
        /// <param name="text">Вхідний текст для аналізу</param>
        /// <param name="distinct">Чи повертати унікальні індекси</param>
        /// <returns>Список знайдених індексів</returns>
        public static List<string> FindPostalCodes(string text, bool distinct = true)
        {
            if (string.IsNullOrEmpty(text))
                return new List<string>();

            try
            {
                var matches = PostalRegex.Matches(text).Select(m => m.Value);
                return distinct ? matches.Distinct().ToList() : matches.ToList();
            }
            catch (RegexMatchTimeoutException)
            {
                Console.WriteLine("Помилка: перевищено час пошуку регулярного виразу.");
                return new List<string>();
            }
        }
    }

    /// <summary>
    /// Точка входу програми. Зчитує текст, знаходить індекси та виводить результат.
    /// </summary>
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Введіть текст для аналізу:");
            string input = Console.ReadLine();

            var postalCodes = PostalCodeFinder.FindPostalCodes(input);

            if (postalCodes.Count == 0)
            {
                Console.WriteLine("Поштових індексів не знайдено.");
            }
            else
            {
                Console.WriteLine("\nЗнайдені поштові індекси (формат 00000):");
                foreach (var code in postalCodes)
                    Console.WriteLine(code);
            }

            Console.WriteLine("\nРоботу завершено.");
        }
    }
}
