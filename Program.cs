using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LabWorkRegex
{
    // Клас для пошуку поштових індексів у тексті
    public class PostalCodeFinder
    {
        // Інкапсульоване поле для збереження тексту
        private readonly string _text;

        // Конструктор приймає текст для обробки
        public PostalCodeFinder(string text)
        {
            _text = text ?? throw new ArgumentNullException(nameof(text), "Текст не може бути порожнім");
        }

        // Метод для знаходження всіх поштових індексів формату 00000
        public List<string> FindPostalCodes()
        {
            var postalCodes = new List<string>();

            // Регулярний вираз для пошуку 5 цифр підряд
            string pattern = @"\b\d{5}\b";
            Regex regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(_text);

            foreach (Match match in matches)
            {
                postalCodes.Add(match.Value);
            }

            return postalCodes;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Приклад тексту для пошуку
            string sampleText = "Мої поштові індекси: 01001, 03022, 12345, а також 67890 і неправильний 1234.";

            // Створюємо об'єкт класу PostalCodeFinder
            PostalCodeFinder finder = new PostalCodeFinder(sampleText);

            // Виконуємо пошук
            List<string> postalCodes = finder.FindPostalCodes();

            // Виводимо результати
            Console.WriteLine("Знайдені поштові індекси:");
            foreach (string code in postalCodes)
            {
                Console.WriteLine(code);
            }
        }
    }
}
