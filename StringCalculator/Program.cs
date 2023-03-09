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

        public static int Add(string expression)
        {
            var delimiters = new List<string>() { "\n", "," };
            var matches = MyRegex().Matches(expression);
            if (expression.StartsWith("//"))
            {
                if (matches.Count > 0)
                {
                    delimiters.AddRange(matches.Select(match => match.Groups["delimiter"].Value));
                    expression = expression[(expression.IndexOf("\n", StringComparison.Ordinal) + 1)..];
                }
                else 
                {
                    delimiters.Add(expression[2].ToString());
                    expression = expression[4..];
                }
            }

            var numbers = expression.Split(delimiters.ToArray(), StringSplitOptions.None);

            return numbers.Where(summand => summand != "").Sum(summand => ValidateNumber(int.Parse(summand)));
        }

        [GeneratedRegex(@"\[(?<delimiter>.*?)\]", RegexOptions.Compiled)]
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