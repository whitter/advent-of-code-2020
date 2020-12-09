using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day9
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
                .Select(x => Convert.ToInt64(x))
                .ToArray();

            var result = Task1(input);

            Console.WriteLine($"Task 1: {result}");
            Console.WriteLine($"Task 2: {Task2(input, result)}");
        }

        static long Task1(long[] input)
        {
            for (int i = 25; i < input.Count(); i++)
            {
                try
                {
                    Calculate(input.Skip(i - 25).Take(25), 2, input[i]);
                }
                catch
                {
                    return input[i];
                }
            }

            return 0;
        }

        static long Task2(long[] input, long target)
        {
            for (int i = 0; i < input.Count(); i++)
            {
                for (int x = i + 1; x < input.Count(); x++)
                {
                    var range = input.Skip(i).Take(x - i + 1);
                    var sum = range.Sum();

                    if(sum == target)
                    {
                        var order = range.OrderBy(x => x);

                        return order.First() + order.Last();
                    }
                    else if(sum > target)
                    {
                        break;
                    }
                }
            }

            return 0;
        }

        static long Calculate(IEnumerable<long> input, int length, long target)
        {
            return input.Permutations(length)
                .Where(x => x.Sum() == target)
                .FirstOrDefault()
                .Aggregate((x1, x2) => x1 * x2);
        }
    }

    public static class Extenstions
    {
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1)
            {
                return list.Select(x => new T[] { x });
            }

            return list.Permutations(length - 1)
                .SelectMany(x => list.Where(n => !x.Contains(n)), (x1, x2) => x1.Concat(new T[] { x2 }));
        }
    }
}
