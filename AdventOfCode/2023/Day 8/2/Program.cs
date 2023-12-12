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
        private const string endChar = "Z";
        private static Dictionary<string, string[]> Maps = new();
        private static char[] Instructions;
        private static int[] InstructionsInt;
        static long count;

        private static readonly Dictionary<char, int> NextDirIndex = new()
        {
            { 'L', 1 },
            { 'R', 2 }
        };

        static void Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Calcualte();
            stopwatch.Stop();
            Console.WriteLine("Elapsed time = " + stopwatch.Elapsed.TotalMilliseconds + "ms");
            Console.WriteLine("Elapsed time = " + stopwatch.Elapsed.TotalMinutes + "mins");
            //Console.ReadKey();

        }

        private static void Calcualte()
        {
            List<string> codes = File.ReadLines("Input.txt").ToList();
            Instructions = codes[0].ToCharArray();
            InstructionsInt = Instructions.Select(x => NextDirIndex[x]).ToArray();
            for (int i = 2; i < codes.Count; i++)
            {
                var map = codes[i].Replace("(", "").Replace(")", "").Split(new char[] { '=', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                Maps.Add(map[0], map);
            }

            int totalInstructions = Instructions.Length;
            string[] endsWithA = Maps.Where(x => x.Key.EndsWith('A')).Select(x => x.Key).ToArray();

            string[] currentNodes = endsWithA;

            Thread updateThread = new Thread(updateCount)
            {
                IsBackground = true
            };
            updateThread.Start();
            int nextIndex;
            long[] firstZIndex = new long[currentNodes.Length];


            while (!currentNodes.All(x => x.EndsWith(endChar)) && firstZIndex.Any(x => x == 0))
            {
                nextIndex = InstructionsInt[count % totalInstructions];

                for (int i = 0; i < currentNodes.Length; i++)
                {
                    currentNodes[i] = Maps[currentNodes[i]][nextIndex];
                    if (currentNodes[i].EndsWith('Z') && firstZIndex[i]==0)  
                    {
                        firstZIndex[i] =  count + 1;
                        Console.WriteLine($"Node {i}-Z-{count+1}");
                    }
                }
                count++;
            }
            long lcm = LCM(firstZIndex[0], firstZIndex[1]);
            for (int i = 2; i < firstZIndex.Length; i++)
            {
                lcm = LCM(lcm, firstZIndex[i]);
            }

            Console.WriteLine("Finished!!!!!!!!!!!!!!!! cnt = " + lcm);
            File.AppendAllText("answer.txt", count.ToString() + Environment.NewLine);
        }

        private static void updateCount(object obj)
        {
            while (true)
            {
                Thread.Sleep(5000);
                Console.WriteLine("count: " + count);
            }
        }

        static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static long LCM(long a, long b)
        {
            return (a / GCD(a, b)) * b;
        }
    }

}
