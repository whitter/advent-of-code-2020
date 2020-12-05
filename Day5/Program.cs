using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
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
                .Replace('F' , '0')
                .Replace('L', '0')
                .Replace('B', '1')
                .Replace('R', '1')
                .Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => Convert.ToInt32(x, 2));

            Console.WriteLine($"Task 1: {input.Max()}");
            Console.WriteLine($"Task 2: {Enumerable.Range(input.Min(), input.Max()).Except(input).First()}");
        }
    }

}
