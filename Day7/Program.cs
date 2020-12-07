using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day7
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

            Regex outerRx = new Regex(@"(.*) bags contain (.*).", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex innerRx = new Regex(@"(\d+) (.+) bags?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Dictionary<string, Dictionary<string, int>> input = outerRx.Matches(content)
                .ToDictionary(
                    x => x.Groups[1].Value,
                    x => x.Groups[2].Value.Split(", ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(y => innerRx.Match(y))
                        .Where(y => y.Success)
                        .ToDictionary(y => y.Groups[2].Value, y => Convert.ToInt32(y.Groups[1].Value))
                );

            Console.WriteLine($"Task 1: {input.Bags("shiny gold").Count()}");
            Console.WriteLine($"Task 2: {input.BagsCount("shiny gold", 1) - 1}");
        }
    }

    public static class Extenstions
    {
        public static int BagsCount(this Dictionary<string, Dictionary<string, int>> list, string type, int count)
        {
            var bag = list.FirstOrDefault(x => x.Key == type);

            if (!bag.Value.Any())
            {
                return count;
            }

            var multi = bag.Value.Sum(x => x.Value);
            var sub = bag.Value.Sum(x => list.BagsCount(x.Key, x.Value));

            var total = count + (count * sub);

            return total;
        }

        public static Dictionary<string, Dictionary<string, int>> Bags(this Dictionary<string, Dictionary<string, int>> list, string type)
        {            
            var bags = list.Where(x => x.Value.Any(y => y.Key == type))
                .ToDictionary(x => x.Key, x => x.Value);

            if(!bags.Any())
            {
                return bags;
            }

            var result = bags
                .SelectMany(x => list.Bags(x.Key))
                .Distinct();

            bags = bags.Concat(result.Where(x => !bags.ContainsKey(x.Key)))
                .ToDictionary(x => x.Key, x => x.Value);

            return bags;
        }
    }
}
