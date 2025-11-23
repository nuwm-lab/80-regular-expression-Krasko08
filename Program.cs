using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegularExpressionLab
{
    public class PostalCodeFinder
    {
        // Метод для пошуку поштових індексів формату 00000
        public List<string> FindPostalCodes(string text)
        {
            var postalCodes = new List<string>();

            // П'ять цифр поспіль
            string pattern = @"\b\d{5}\b";

            MatchCollection matches = Regex.Matches(text, pattern);

            foreach (Match match in matches)
            {
                postalCodes.Add(match.Value);
            }

            return postalCodes;
        }
    }
}
//цівсмпр
