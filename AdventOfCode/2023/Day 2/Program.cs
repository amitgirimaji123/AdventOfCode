using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        //Day 2- 2: Cubes 2
        //63981 ans

        private static Dictionary<string, int> dict = new()
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };
        static void Main(string[] args)
        {
            IEnumerable<string> codes = File.ReadLines("Sample.txt");
            List<long> list = getNumbers(codes);
            long sum = list.Sum();
            Console.WriteLine(sum);
        }
        static List<long> getNumbers(IEnumerable<string> codes)
        {
            List<long> list = new List<long>();
            foreach (var code in codes)
            {
                long gameNum = _getNum(code);
                list.Add(gameNum);
            }
            return list;
        }

        private static long _getNum(string code)
        {
            string[] pieces = code.Split(':');
            Regex regex = new Regex(@"Game (\d+)");
            Match match = regex.Match(pieces[0]);
            if (!match.Success)
            {
                throw new Exception();

            }
            string gameId = match.Groups[1].Value;

            string[] reveals = pieces[1].Split(';');
            Dictionary<string, int> minvaluesRGB = _getMinRGB(reveals);
            Console.WriteLine(code);
            Console.WriteLine($"{ minvaluesRGB["red"]} , { minvaluesRGB["green"]} , { minvaluesRGB["blue"]}");
            long multiply = minvaluesRGB["red"] * minvaluesRGB["green"] * minvaluesRGB["blue"];
            Console.WriteLine(multiply) ;
            return multiply;

        }

        private static Dictionary<string, int> _getMinRGB(string[] reveals)
        {
            Dictionary<string, int> maxDict = new()
            {
                { "red", 0 },
                { "green", 0 },
                { "blue", 0 }
            };
            foreach (var reveal in reveals)
            {
                string[] colorCounts = reveal.Split(',');
                foreach (var colorCount in colorCounts)
                {
                    string[] number_color = colorCount.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    int count = Convert.ToInt32(number_color[0]);
                    string color = number_color[1];
                    if (maxDict[color] < count)
                    {
                        maxDict[color] = count;
                    }
                }

            }
            return maxDict;
        }


    }
}
