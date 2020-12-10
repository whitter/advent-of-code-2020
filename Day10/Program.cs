using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
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

            var input = content
                .Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => Convert.ToInt32(x))
                .OrderBy(x => x)
                .ToArray();

            var diff = Diffrences(input);

            Console.WriteLine($"Task 1: {Task1(diff)}");
            Console.WriteLine($"Task 2: {Task2(diff)}");
        }

        static int Task1(int[] input)
        {
            var groups = input
                .GroupBy(x => x, (x, y) => new KeyValuePair<int, int>(x,y.Count()))
                .ToDictionary(x => x.Key, x => x.Value);

            return groups[1] * groups[3];
        }

        static long Task2(int[] input)
        {
            var sequences = input
                .Segment((curr, prev, _) => curr != prev)
                .Where(x => x.First() == 1)
                .Select(x => x.Count() switch
                {
                    1 => 1,
                    2 => 2,
                    3 => 4,
                    4 => 7,
                    5 => 15,
                    _ => throw new Exception()
                })
                .Aggregate(1L, (x1, x2) => x1 * x2);

            return sequences;
        }

        static int[] Diffrences(int[] input)
        {
            return input
                .Prepend(0)
                .Append(input[^1] + 3)
                .Window(3)
                .Select(x => x[1] - x[0])
                .ToArray();
        }
    }
}
