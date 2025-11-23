using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace RegularExpressionLab
{
    public class PostalCodeFinder
    {
        public List<string> FindPostalCodes(string text)
        {
            var postalCodes = new List<string>();

            // Регулярний вираз для індексу формату 00000
            string pattern = @"\b\d{5}\b";

            MatchCollection matches = Regex.Matches(text, pattern);

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
            string inputText = "Мої адреси: 33024, 01001, а це не індекс 123.",
                   // Для тесту.
            ;

            var finder = new PostalCodeFinder();
            var codes = finder.FindPostalCodes(inputText);

            Console.WriteLine("Знайдені індекси:");

            foreach (string code in codes)
            {
                Console.WriteLine(code);
            }
        }
    }
}
