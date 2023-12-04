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

        private static int[] cardCount;
        static void Main(string[] args)
        {
            IEnumerable<string> codes = File.ReadLines("Sample.txt");
            getNumbers(codes);
            int sum = cardCount.Sum();
            Console.WriteLine(sum);
        }
        static List<long> getNumbers(IEnumerable<string> codes)
        {
            List<long> list = new List<long>();
            cardCount = new int[codes.Count() + 100];
            foreach (var code in codes)
            {
                _process(code);
            }
            return list;
        }

        private static void _process(string code)
        {
            Console.WriteLine(code);
            string[] pieces = code.Split(':');
            Regex regex = new Regex(@"Card\s*(\d+)");
            Match match = regex.Match(pieces[0]);
            if (!match.Success)
            {
                throw new Exception();

            }
            int cardNum = Convert.ToInt32(match.Groups[1].Value);
            cardCount[cardNum]++;
            string[] reveals = pieces[1].Split('|');

            int matchCount = (int)_getPoints(reveals);

            Console.WriteLine($"Match Count : {matchCount} ");
           
            Console.WriteLine("Card " + cardNum + ": Count " + cardCount[cardNum]);
            for (int j = 0; j < cardCount[cardNum]; j++)
            {
                for (int i = 1; i < matchCount + 1; i++)
                {
                    cardCount[cardNum + i]++;
                }
            }
           

        }

        private static long _getPoints(string[] reveals)
        {
            List<int> winningNos = reveals[0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();
            List<int> myNos = reveals[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();

            List<int> myWinnings = winningNos.Where(x => myNos.Contains(x)).ToList();

            return myWinnings.Count;

        }


    }
}
