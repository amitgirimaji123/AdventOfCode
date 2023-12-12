using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{

    enum CardType
    {
        FiveOfAKind = 6,
        FourOfAKind = 5,
        FullHouse = 4,
        ThreeOfAKind = 3,
        TwoPair = 2,
        OnePair = 1,
        HighCard = 0
    }


    class Program
    {
        private static Dictionary<string,List<string>> Maps = new ();
        private static char[] Instructions;

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
            for (int i = 2; i < codes.Count; i++)
            {
                List<string> map = new List<string>();
                map = codes[i].Replace("(", "").Replace(")","").Split(new char[] { '=', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x=>x.Trim()).ToList();
                Maps.Add(map[0], map);
            }

            long count = 0;
            int totalInstructions = Instructions.Length;
            List<string> currentNode = Maps["AAA"];
            List<string> lastNode = Maps["ZZZ"];

            while(currentNode != lastNode)
            {
                long instructionIndex = count % totalInstructions;
                char NextDir = Instructions[instructionIndex];
                if (NextDir == 'L')
                {
                    currentNode = Maps[currentNode[1]];
                }
                else if (NextDir == 'R')
                {
                    currentNode = Maps[currentNode[2]];
                }
                else
                {
                    Debug.Assert(false);
                }
                count++;
            }


            Console.WriteLine("Sum = " + count);
        }



    }

}

//Not 250026099 less <
//Not 247835722 >
//249631077
// Answer = 249631254