using System;
using RegularExpressionLab;

namespace RegularExpressionLabApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть текст:");
            string inputText = Console.ReadLine();

            var finder = new PostalCodeFinder();
            var foundCodes = finder.FindPostalCodes(inputText);

            Console.WriteLine("\nЗнайдені поштові індекси (формат 00000):");

            if (foundCodes.Count == 0)
            {
                Console.WriteLine("Нічого не знайдено.");
            }
            else
            {
                foreach (var code in foundCodes)
                {
                    Console.WriteLine(code);
                }
            }

            Console.WriteLine("\nГотово!");
        }
    }
}
