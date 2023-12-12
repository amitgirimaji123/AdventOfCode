using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {
        static Dictionary<int, char> IntCharDict = new Dictionary<int, char>()
        {
            { '0','.' },
            { '1', '#' }
        };

        static void Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Calcualte();
            stopwatch.Stop();
            Console.WriteLine("Elapsed time = " + stopwatch.Elapsed.TotalMilliseconds + "ms");
            Console.WriteLine("Elapsed time = " + stopwatch.Elapsed.TotalMinutes + "mins");
            Console.ReadKey();

        }

        private static void Calcualte()
        {
            long count = 0;
            List<string> codes = File.ReadLines("Input.txt").ToList();
            //List<string> codes = File.ReadLines("Input-Demo.txt").ToList();

            foreach (var code in codes)
            {
                long combination = _getCombination(code);
                count += combination;
            }

            Console.WriteLine("answer = " + count);
        }

        private static long _getCombination(string code)
        {
            long validCombination = 0;
            var split = code.Split(' ');
            char[] springs = split[0].ToCharArray();

            int[] parity = split[1].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x.ToString())).ToArray();

            int unknownCount = springs.Count(x => x == '?');

            long totalCombinations = (long)Math.Pow(2, unknownCount);
            for (int i = 0; i < totalCombinations; i++)
            {
                char[] combination = _getspringCombination(springs, i, unknownCount);
                List<int> combinationParity = _getParity(combination);
                if (Enumerable.SequenceEqual(combinationParity, parity))
                {
                    validCombination++;
                }
            }
            Console.WriteLine(code);
            Console.WriteLine(validCombination);
            return validCombination;
        }

        private static List<int> _getParity(char[] combination)
        {
            List<int> parity = new List<int>();
            for (int i = 0; i < combination.Length; i++)
            {
                char ch = combination[i];
                if (ch == '#')
                {
                    int cnt = 0;
                    do
                    {
                        cnt++;
                        i++;
                    } while (i < combination.Length && combination[i] == '#');
                    parity.Add(cnt);
                }
                else
                {
                    Debug.Assert(ch == '.');
                }
            }
            return parity;
        }

        private static char[] _getspringCombination(char[] springs, int num, int unknownCount)
        {
            List<char> binaryNum = Convert.ToString(num, 2).ToCharArray().ToList();
            while (binaryNum.Count < unknownCount)
            {
                binaryNum.Insert(0, '0');
            }
            char[] charCombination = binaryNum.Select(x => IntCharDict[x]).ToArray();
            char[] newSprings = new char[springs.Length];
            int j = 0;
            for (int i = 0; i < springs.Length; i++)
            {
                if (springs[i] == '?')
                {
                    newSprings[i] = charCombination[j++];
                }
                else
                {
                    newSprings[i] = springs[i];
                }
            }
            return newSprings;
        }
    }

}
