using System.Text.RegularExpressions;

namespace StringCalculator
{
    public static class Calculator
    {
        private static int ValidateNumber(int value)
        {
            if (value < 0) throw new ArgumentException("Number cannot be negative");
            return value > 1000 ? 0 : value;
        }

        public static int Add(string numbers)
        {
            var delimiters = new List<string>() { "\n", "," };
            var sum = 0;
            var number = string.Empty;
            var parsingDelimiters = true;

            if (numbers.Length <= 0) return sum;
            foreach (var character in numbers)
            {
                if (parsingDelimiters)
                {
                    if (character == '\n') parsingDelimiters = false;
                }
                else
                {
                    if (char.IsDigit(character) || character == '-') number += character;
                    else if (delimiters.Contains(character.ToString()))
                    {
                        sum += ValidateNumber(int.Parse(number));
                        number = string.Empty;
                    }
                }
            }

            sum += ValidateNumber(int.Parse(number));

            return sum;
        }
    }

    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine(Calculator.Add("1,2"));
            Console.WriteLine(Calculator.Add("1\n2"));
            Console.WriteLine(Calculator.Add("1,2,3"));
            Console.WriteLine(Calculator.Add("1\n2\n3"));
            try
            {
                Console.WriteLine(Calculator.Add("-1"));
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}