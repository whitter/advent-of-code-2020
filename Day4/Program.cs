using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
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

            var input = content.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new string[] { "\r\n", " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(y => y.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries))
                    .ToDictionary(y => y[0], y => y[1])
                );

            var results = Task1(input);

            Console.WriteLine($"Task 1: {Task1(input).Count()}");
            Console.WriteLine($"Task 2: {Task2(results)}");
        }

        static IEnumerable<Dictionary<string, string>> Task1(IEnumerable<Dictionary<string, string>> input)
        {
            var results = input.Where(x => x.ContainsKey("byr") && x.ContainsKey("iyr") && x.ContainsKey("eyr") && x.ContainsKey("hgt") && x.ContainsKey("hcl") && x.ContainsKey("ecl") && x.ContainsKey("pid"));

            return results;
        }

        static int Task2(IEnumerable<Dictionary<string, string>> input)
        {
            var count = 0;

            Regex heightRx = new Regex(@"(\d*)(in|cm)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex colorRx = new Regex(@"^#(?:[0-9a-f]{3}){1,2}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string[] eyeColors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            foreach (var result in input)
            {
                if (result["byr"].Length != 4 || (Convert.ToInt32(result["byr"]) < 1920 || Convert.ToInt32(result["byr"]) > 2002))
                {
                    continue;
                }

                if (result["iyr"].Length != 4 || (Convert.ToInt32(result["iyr"]) < 2010 || Convert.ToInt32(result["iyr"]) > 2020))
                {
                    continue;
                }

                if (result["eyr"].Length != 4 || (Convert.ToInt32(result["eyr"]) < 2020 || Convert.ToInt32(result["eyr"]) > 2030))
                {
                    continue;
                }

                var heightResult = heightRx.Match(result["hgt"]);

                if(!heightResult.Success || 
                    (heightResult.Groups[2].Value == "cm" && (Convert.ToInt32(heightResult.Groups[1].Value) < 150 || Convert.ToInt32(heightResult.Groups[1].Value) > 193)) ||
                    (heightResult.Groups[2].Value == "in" && (Convert.ToInt32(heightResult.Groups[1].Value) < 59 || Convert.ToInt32(heightResult.Groups[1].Value) > 76)))
                {
                    continue;
                }

                if(!colorRx.Match(result["hcl"]).Success)
                {
                    continue;
                }

                if(!eyeColors.Contains(result["ecl"]))
                {
                    continue;
                }

                if(result["pid"].Length != 9)
                { 
                    continue;
                }

                count++;
            }

            return count;
        }
    }
}
