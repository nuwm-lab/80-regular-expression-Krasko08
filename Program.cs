using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LabRegex
{
    public static class PostalCodeFinder
    {
        // Регулярний вираз для пошуку п'ятизначних індексів, які не є частиною довшого числа
        private static readonly Regex PostalRegex = new Regex(@"(?<!\d)\d{5}(?!\d)",
                                                              RegexOptions.Compiled);

        /// <summary>
        /// Повертає список знайдених поштових індексів у вхідному тексті.
        /// </summary>
        public static List<string> FindPostalCodes(string text, bool distinct = true)
        {
            if (string.IsNullOrEmpty(text))
                return new List<string>();

            var matches = PostalRegex.Matches(text)
                                     .Select(m => m.Value);

            return distinct ? matches.Distinct().ToList() : matches.ToList();
        }

        // Простий демонстраційний Main (можна прибрати для бібліотеки)
        public static void Main()
        {
            string sample = @"Приклади: 01001, 02002, адреса: вул. Петра 12, кв. 45, індекс: 03150.
                              Неправильні: 123456 (6 цифр — не індекс), частина 9123456 також не підходить.
                              Повтор: 03150 і ще раз 03150. Також 10000-10005 як інтервал.";

            var found = FindPostalCodes(sample);
            Console.WriteLine(""Знайдені індекси:"");
            foreach (var idx in found)
                Console.WriteLine(idx);
        }
    }
}
