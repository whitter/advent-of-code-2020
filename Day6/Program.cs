using System;
using System.IO;
using System.Linq;

namespace Day6
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
                .Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(y => y.ToCharArray()));

            Console.WriteLine($"Task 1: {input.Sum(x => x.SelectMany(y => y).Distinct().Count())}");
            Console.WriteLine($"Task 2: {input.Sum(x => x.Aggregate((x1, x2) => x1.Intersect(x2).ToArray()).Count())}");
        }
    }
}
