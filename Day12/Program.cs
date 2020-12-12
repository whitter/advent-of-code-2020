using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day12
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

            Regex regx = new Regex(@"(.)(\d*)");

            var input = content
                .Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => regx.Match(x))
                .Select(x => (x.Groups[1].Value.ToString(), Convert.ToInt32(x.Groups[2].Value)));

            Console.WriteLine($"Task 1: {Task1(input)}");
            Console.WriteLine($"Task 2: {Task2(input)}");
        }

        static readonly string[] compass = new string[4] { "N", "E", "S", "W" };

        static int Task1(IEnumerable<(string, int)> input)
        {
            int deg = 90;

            var boat = new Point(0, 0);

            foreach((string dir, int value)  in input)
            {
                string heading = dir;

                switch(dir)
                {
                    case "L":
                        deg -= value;
                        break;
                    case "R":
                        deg += value;
                        break;
                    case "F":
                        heading = compass[((deg % 360 + 360) % 360) / 90];
                        goto default;
                    default:
                        switch(heading)
                        {
                            case "N":
                                boat.Y -= value;
                                break;
                            case "E":
                                boat.X += value;
                                break;
                            case "S":
                                boat.Y += value;
                                break;
                            case "W":
                                boat.X -= value;
                                break;
                        }
                        break;
                }                
            }

            return Math.Abs(boat.X + boat.Y);
        }

        static int Task2(IEnumerable<(string, int)> input)
        {
            var boat = new Point(0, 0);
            var waypoint = new Point(10, -1);

            foreach ((string dir, int value) in input)
            {
                string heading = dir;

                switch (dir)
                {
                    case "L":
                        waypoint = RotatePoint(waypoint, new Point(0, 0), -1 * value);
                        break;
                    case "R":
                        waypoint = RotatePoint(waypoint, new Point(0, 0), value);
                        break;
                    case "F":
                        boat.X += waypoint.X * value;
                        boat.Y += waypoint.Y * value;
                        break;
                    case "N":
                        waypoint.Y -= value;
                        break;
                    case "E":
                        waypoint.X += value;
                        break;
                    case "S":
                        waypoint.Y += value;
                        break;
                    case "W":
                        waypoint.X -= value;
                        break;
                }
            }

            return Math.Abs(boat.X + boat.Y);
        }

        static Point RotatePoint(Point pt, Point origin, int degs)
        {
            double rads = degs * (Math.PI / 180);
            double cos = Math.Cos(rads);
            double sin = Math.Sin(rads);

            return new Point
            {
                X = (int)Math.Round(cos * (pt.X - origin.X) - sin * (pt.Y - origin.Y) + origin.X),
                Y = (int)Math.Round(sin * (pt.X - origin.X) + cos * (pt.Y - origin.Y) + origin.Y)
            };
        }
    }
}
