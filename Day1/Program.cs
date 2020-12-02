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

            Console.WriteLine($"Task 1: {Calculate(input, 2, 2020)}");
            Console.WriteLine($"Task 2: {Calculate(input, 3, 2020)}");
        }

        static int Calculate(IEnumerable<int> input, int length, int target)
        {
            return Permutations(input, length)
                .Where(x => x.Sum() == target)
                .FirstOrDefault()
                .Aggregate((x1, x2) => x1 * x2);
        }


        static IEnumerable<IEnumerable<T>> Permutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1)
            {
                return list.Select(x => new T[] { x });
            }

            return Permutations(list, length - 1)
                .SelectMany(x => list.Where(n => !x.Contains(n)), (x1, x2) => x1.Concat(new T[] { x2 }));
        }
    }
}
