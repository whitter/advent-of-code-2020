using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
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
                .Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            var time = Convert.ToInt32(input.First());
            var buses = input
                .Last()
                .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);


            Console.WriteLine($"Task 1: {Task1(time, buses)}");
            Console.WriteLine($"Task 1: {Task2(buses.ToArray())}");
        }

        static int Task1(int time, IEnumerable<string> buses)
        {
            var nextbuses = buses
                .Where(x => x != "x")
                .Select(x => Convert.ToInt32(x))
                .Select(x => (x, time + (x - (time % x))));

            var nextbus = nextbuses
                .OrderBy(x => x.Item2)
                .First();

            return nextbus.x * (nextbus.Item2 - time);
        }

        static long Task2(string[] buses)
        {            
            long timestamp = 0;
            long step = 1;

            for (int i = 0; i < buses.Length; i++)
            {
                if(buses[i] == "x")
                {
                    continue;
                }

                var id = Convert.ToInt32(buses[i]);

                while((timestamp + i) % id != 0)
                {
                    timestamp += step;
                }

                step *= id;
            }

            return timestamp;
        }
    }
}
