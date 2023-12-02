using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        //Day 2: Cubes
        //12 red cubes, 13 green cubes, and 14 blue cubes.
        //2449 ans
       
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
                long gameNum = _getValidGameValOr0(code);
                list.Add(gameNum);
            }
            return list;
        }

        private static long _getValidGameValOr0(string code)
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
            bool isValid = _checkValidity(reveals);
            Console.WriteLine(code);
            Console.WriteLine(isValid);
            if (isValid)
            {
                return Convert.ToInt64(gameId);
            }
            else
            {
                return 0;
            }

        }

        private static bool _checkValidity(string[] reveals)
        {
            foreach (var reveal in reveals)
            {
                string[] colorCounts = reveal.Split(',');
                foreach (var colorCount in colorCounts)
                {
                    string[] number_color = colorCount.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if(dict[number_color[1]] < Convert.ToInt32( number_color[0]))
                    {
                        return false;
                    }
                }

            }
            return true;
        }

        private static int _getNum(string code)
        {
            List<string> numbers = new List<string>() { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            List<KeyValuePair<string, int>> matches = new List<KeyValuePair<string, int>>();

            foreach (var str in numbers)
            {
                if (code.Contains(str))
                {
                    var index = code.IndexOf(str);
                    var indexLast = code.LastIndexOf(str);
                    matches.Add(new KeyValuePair<string, int>(str, index));
                    if (index != indexLast)
                    {
                        matches.Add(new KeyValuePair<string, int>(str, indexLast));

                    }
                }
            }

            int min = matches.Min(x => x.Value);
            int max = matches.Max(x => x.Value);

            int first = numbers.IndexOf(matches.First(x => x.Value == min).Key) % 10;
            int last = numbers.IndexOf(matches.First(x => x.Value == max).Key) % 10;

            return first * 10 + last;

        }
    }
}
