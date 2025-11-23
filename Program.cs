using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegularExpressionLab
{
    /// <summary>
    /// Результат пошуку для одного шаблону
    /// </summary>
    public class PatternResult
    {
        public string PatternName { get; set; }
        public List<string> Matches { get; set; } = new List<string>();
        public int Count => Matches.Count;
    }

    /// <summary>
    /// Клас для пошуку шаблонів у тексті.
    /// </summary>
    public class TextPatternFinder
    {
        // Статичні компільовані Regex для повторного використання
        private static readonly Regex _postalRegex = new Regex(@"(?<!\d)\d{5}(?!\d)", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        private static readonly Regex _dateRegex = new Regex(@"\b\d{2}/\d{2}/\d{4}\b", RegexOptions.Compiled);
        private static readonly Regex _ipRegex = new Regex(@"\b(?:\d{1,3}\.){3}\d{1,3}\b", RegexOptions.Compiled);

        /// <summary>
        /// Пошук кількох типів шаблонів у тексті.
        /// </summary>
        /// <param name="text">Вхідний текст</param>
        /// <returns>Список результатів пошуку для кожного шаблону</returns>
        public List<PatternResult> FindPatterns(string text)
        {
            var results = new List<PatternResult>();

            if (string.IsNullOrWhiteSpace(text))
                return results;

            // Словник шаблонів
            var patterns = new Dictionary<string, Regex>
            {
                { "PostalCode", _postalRegex },
                { "Date (dd/mm/yyyy)", _dateRegex },
                { "IP Address", _ipRegex }
            };

            foreach (var kvp in patterns)
            {
                var result = new PatternResult { PatternName = kvp.Key };
                MatchCollection matches = kvp.Value.Matches(text);

                foreach (Match match in matches)
                {
                    result.Matches.Add(match.Value);
                }

                results.Add(result);
            }

            return results;
        }
    }

    class Program
    {
        static void Main()
        {
            var finder = new TextPatternFinder();

            Console.WriteLine("Введіть текст для пошуку шаблонів:");
            string input = Console.ReadLine();

            var results = finder.FindPatterns(input);

            foreach (var res in results)
            {
                Console.WriteLine($"\nШаблон: {res.PatternName}");
                if (res.Count == 0)
                {
                    Console.WriteLine("Збігів не знайдено.");
                }
                else
                {
                    Console.WriteLine($"Кількість збігів: {res.Count}");
                    foreach (var match in res.Matches)
                    {
                        Console.WriteLine(match);
                    }
                }
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
