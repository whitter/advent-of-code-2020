using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
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
                .Select(x => x.ToCharArray())
                .ToArray();

            Console.WriteLine($"Task 1: {FindPath(input, 3, 1)}");
            Console.WriteLine($"Task 2: {Task2(input)}");
        }

        static long Task2(char[][] input)
        {
            var slope1 = FindPath(input, 1, 1);
            var slope2 = FindPath(input, 3, 1);
            var slope3 = FindPath(input, 5, 1);
            var slope4 = FindPath(input, 7, 1);
            var slope5 = FindPath(input, 1, 2);

            return slope1 * slope2 * slope3 * slope4 * slope5;
        }

        static long FindPath(char[][] input, int right, int down, int x = 0, int y = 0)
        {
            long trees = 0;

            x += right;
            y += down;

            if(y >= input.Length)
            {
                return trees;
            }

            if(x >= input[y].Length)
            {
                x -= input[y].Length;
            }

            if(input[y][x] == '#')
            {
                trees++;
            }

            trees += FindPath(input, right, down, x, y);

            return trees;
        }
    }
}
