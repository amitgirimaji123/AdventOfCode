using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        //Day 3-1 
        //63981 ans

        
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
            //Regex regex = new Regex(@"Card (\d+)");
            //Match match = regex.Match(pieces[0]);
            //if (!match.Success)
            //{
            //    throw new Exception();

            //}
            //string gameId = match.Groups[1].Value;

            string[] reveals = pieces[1].Split('|');
            long points = _getPoints(reveals);
            Console.WriteLine(code);
            Console.WriteLine(points);
           
            return points;

        }

        private static long _getPoints(string[] reveals)
        {
            List<int> winningNos = reveals[0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();
            List<int> myNos = reveals[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();

            List<int> myWinnings = winningNos.Where(x => myNos.Contains(x)).ToList();

            return Convert.ToInt64(Math.Pow(2, myWinnings.Count-1));

        }


    }
}
