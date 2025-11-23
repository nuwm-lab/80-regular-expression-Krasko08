using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegularExpressionLab
{
    /// <summary>
    /// Клас для пошуку поштових індексів у тексті.
    /// Використовує регулярні вирази для знаходження індексів у форматі 00000.
    /// </summary>
    public class PostalCodeFinder
    {
        // Статичний Regex для продуктивності та повторного використання
        private static readonly Regex PostalCodeRegex = new Regex(
            @"(?<!\d)\d{5}(?!\d)", 
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private readonly string _inputText;

        /// <summary>
        /// Ініціалізує новий екземпляр класу PostalCodeFinder.
        /// </summary>
        /// <param name="inputText">Вхідний текст для пошуку індексів.</param>
        /// <exception cref="ArgumentNullException">Кидається, якщо inputText дорівнює null.</exception>
        public PostalCodeFinder(string inputText)
        {
            _inputText = inputText ?? throw new ArgumentNullException(nameof(inputText));
        }

        /// <summary>
        /// Повертає список поштових індексів у форматі 00000.
        /// </summary>
        /// <returns>Список знайдених індексів. Якщо збігів немає — порожній список.</returns>
        public List<string> FindPostalCodes()
        {
            var postalCodes = new List<string>();
            MatchCollection matches = PostalCodeRegex.Matches(_inputText);

            foreach (Match match in matches)
            {
                postalCodes.Add(match.Value);
            }

            return postalCodes;
        }
    }

    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Введіть текст для пошуку поштових індексів:");
            string? text = Console.ReadLine();

            try
            {
                var finder = new PostalCodeFinder(text ?? string.Empty);
                List<string> postalCodes = finder.FindPostalCodes();

                if (postalCodes.Count > 0)
                {
                    Console.WriteLine("Знайдені поштові індекси:");
                    foreach (string code in postalCodes)
                    {
                        Console.WriteLine(code);
                    }
                }
                else
                {
                    Console.WriteLine("Поштових індексів у форматі 00000 не знайдено.");
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Непередбачена помилка: {ex.Message}");
            }
        }
    }
}
