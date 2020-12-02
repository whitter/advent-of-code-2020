using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            string content;

            using (var reader = new StreamReader("input.txt"))
            {
                content = reader.ReadToEnd();
            }

            var input = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Replace(":", "").Split(new string[] { "-", " " }, StringSplitOptions.RemoveEmptyEntries))
                .Select(x => new PasswordPolicy { Min = Convert.ToInt32(x[0]), Max = Convert.ToInt32(x[1]), Letter = Convert.ToChar(x[2]), Password = x[3] });

            Console.WriteLine($"Task 1: {CorrectPasswords1(input)}");
            Console.WriteLine($"Task 2: {CorrectPasswords2(input)}");
        }

        static int CorrectPasswords1(IEnumerable<PasswordPolicy> input)
        {
            var result = input.Where(x => x.Password.Count(c => c == x.Letter) >= x.Min && x.Password.Count(c => c == x.Letter) <= x.Max)
                .Count();

            return result;
        }

        static int CorrectPasswords2(IEnumerable<PasswordPolicy> input)
        {
            var result = input.Where(x => x.Password[x.Min - 1] == x.Letter ^ x.Password[x.Max - 1] == x.Letter)
                .Count();

            return result;
        }
    }

    class PasswordPolicy
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public char Letter { get; set; }
        public string Password { get; set; }
    }
}
