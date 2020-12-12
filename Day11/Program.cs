using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day11
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

            Console.WriteLine($"Task 1: {Simulate(content, true)}");
            Console.WriteLine($"Task 2: {Simulate(content, false)}");
        }

        static int Simulate(string map, bool immediate)
        {
            char[][] draw = map
                .Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToCharArray())
                .ToArray();

            for (int y = 0; y < draw.Length; y++)
            {
                for (int x = 0; x < draw[y].Length; x++)
                {
                    Console.SetCursorPosition(x, y);
                    if (draw[y][x] == 'L')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (draw[y][x] == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.Write(draw[y][x]);
                }
            }

            while (true)
            {
                var iteration = Run(map, immediate);

                if(iteration == map)
                {
                    break;
                }

                map = iteration;
            }

            return map.Count(x => x == '#');
        }

        private static (int x, int y)[] directions = { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

        static string Run(string map, bool immediate)
        {
            char[][] oldMap = map
                .Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToCharArray())
                .ToArray();

            char[][] newMap = map
                .Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToCharArray())
                .ToArray();

            for (int y = 0; y < oldMap.Length; y ++)
            {
                for (int x = 0; x < oldMap[y].Length; x++)
                {
                    if(oldMap[y][x] == '.')
                    {
                        continue;
                    }                    

                    int count = 0;

                    foreach(var direction in directions)
                    {
                        var _y = y;
                        var _x = x;

                        do
                        {
                            _y += direction.y;
                            _x += direction.x;

                            if (_y < 0 || _y >= oldMap.Length)
                            {
                                break;
                            }

                            if (_x < 0 || _x >= oldMap[y].Length)
                            {
                                break;
                            }

                            if (oldMap[_y][_x] == '#')
                            {
                                count++;
                                break;
                            }

                            if (oldMap[_y][_x] == 'L')
                            {
                                break;
                            }
                        }
                        while (!immediate);
                    }

                    newMap[y][x] = (oldMap[y][x], immediate, count) switch
                    {
                        ('L', _, 0) => '#',
                        ('#', true, >= 4) => 'L',
                        ('#', false, >= 5) => 'L',
                        (var c, _, _) => c
                    };

                    //System.Threading.Thread.Sleep(1);

                    Console.SetCursorPosition(x, y);
                    if (newMap[y][x] == 'L')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (newMap[y][x] == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.Write(newMap[y][x]);
                }
            }

            return string.Join("\r\n", newMap.Select(x => new string(x.ToArray())));
        }
    }
}
