using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            string content;

            using(var reader = new StreamReader("input.txt"))
            {
                content = reader.ReadToEnd();
            }

            var input = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => Convert.ToInt32(x));

            var results = Task1(input);
            Task2(input, results);
        }

        static IEnumerable<dynamic> Task1(IEnumerable<int> input)
        {
            var results = input
                .SelectMany(a => input.Where(x => x != a), (a, b) => new { a, b });

            var result = results
                .FirstOrDefault(set => set.a + set.b == 2020);

            Console.WriteLine($"Task 1: {result.a * result.b}");

            return results;
        }

        static void Task2(IEnumerable<int> input, IEnumerable<dynamic> results)
        {
            var result = results
                .SelectMany(set => input.Where(c => c != set.a || c != set.b), (set, c) => new { set.a, set.b, c })
                .FirstOrDefault(set => set.a + set.b + set.c == 2020);

            Console.WriteLine($"Task 2: {result.a * result.b * result.c}");
        }
    }
}
