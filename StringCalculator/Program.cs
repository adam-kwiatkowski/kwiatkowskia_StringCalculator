using System.Text.RegularExpressions;

namespace StringCalculator
{
    public static partial class Calculator
    {
        private static int ValidateNumber(int value)
        {
            if (value < 0) throw new ArgumentException("Number cannot be negative");
            return value > 1000 ? 0 : value;
        }

        public static int Add(string numbers)
        {
            var delimiters = new List<string>() { "\n", "," };
            var match = MyRegex().Match(numbers);
            if (match.Success)
            {
                delimiters.AddRange(match.Groups["delimiter"].Captures.Select(capture => capture.Value));
                numbers = numbers[match.Length..];
            }
            else if (numbers.StartsWith("//"))
            {
                delimiters.Add(numbers[2].ToString());
                numbers = numbers[4..];
            }
            
            var summands = numbers.Split(delimiters.ToArray(), StringSplitOptions.None);

            return summands.Where(summand => summand != "").Sum(summand => ValidateNumber(int.Parse(summand)));
        }

        [GeneratedRegex("^//(\\[(?<delimiter>.+)\\])+")]
        private static partial Regex MyRegex();
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