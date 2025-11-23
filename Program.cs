using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegularExpressionLab
{
    /// <summary>
    /// Клас для пошуку шаблонів у тексті.
    /// Може знаходити поштові індекси, дати, IP-адреси та інші патерни.
    /// </summary>
    public class PatternFinder
    {
        // Словник патернів: назва -> регулярний вираз
        private readonly Dictionary<string, Regex> _patterns;

        private readonly string _inputText;

        /// <summary>
        /// Ініціалізує новий екземпляр класу PatternFinder.
        /// </summary>
        /// <param name="inputText">Вхідний текст для пошуку.</param>
        /// <exception cref="ArgumentException">Кидається, якщо рядок порожній або null.</exception>
        public PatternFinder(string inputText)
        {
            if (string.IsNullOrWhiteSpace(inputText))
            {
                throw new ArgumentException("Вхідний текст не може бути порожнім.", nameof(inputText));
            }

            _inputText = inputText;

            // Ініціалізація патернів
            _patterns = new Dictionary<string, Regex>
            {
                { "Поштовий індекс", new Regex(@"(?<!\d)\d{5}(?!\d)", RegexOptions.Compiled | RegexOptions.CultureInvariant) },
                { "Дата (dd/mm/yyyy)", new Regex(@"\b\d{2}/\d{2}/\d{4}\b", RegexOptions.Compiled | RegexOptions.CultureInvariant) },
                { "IP-адреса", new Regex(@"\b(?:\d{1,3}\.){3}\d{1,3}\b", RegexOptions.Compiled | RegexOptions.CultureInvariant) }
            };
        }

        /// <summary>
        /// Пошук усіх збігів для заданих патернів.
        /// </summary>
        /// <returns>Словник: назва патерну -> список знайдених збігів.</returns>
        public Dictionary<string, List<string>> FindPatterns()
        {
            var results = new Dictionary<string, List<string>>();

            foreach (var kvp in _patterns)
            {
                var matches = kvp.Value.Matches(_inputText);
                var found = new List<string>();

                foreach (Match match in matches)
                {
                    found.Add(match.Value);
                }

                results[kvp.Key] = found;
            }

            return results;
        }
    }

    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Введіть текст для пошуку шаблонів:");
            string? text = Console.ReadLine();

            try
            {
                var finder = new PatternFinder(text ?? string.Empty);
                var results = finder.FindPatterns();

                Console.WriteLine("\nРезультати пошуку:");
                foreach (var kvp in results)
                {
                    if (kvp.Value.Count > 0)
                    {
                        Console.WriteLine($"{kvp.Key}: {string.Join(", ", kvp.Value)}");
                    }
                    else
                    {
                        Console.WriteLine($"{kvp.Key}: збігів не знайдено.");
                    }
                }
            }
            catch (ArgumentException ex)
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
