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
    /*
    Inputs


Input 1:

Time:        41     96     88     94
Distance:   214   1789   1127   1055

Input 2:

Time:        41968894
Distance:   214178911271055
    

     */
    class Program
    {
        private static List<long> TimeList = new();
        private static List<long> DistanceList = new();

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
            List<string> codes = File.ReadLines("Input.txt").ToList();

            TimeList = codes[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt64(x)).ToList();
            DistanceList = codes[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt64(x)).ToList();

            Debug.Assert(TimeList.Count == DistanceList.Count);

            long result = 1;
            for (int i = 0; i < TimeList.Count; i++)
            {
                long num = _getPossibilities(TimeList[i], DistanceList[i]);
                result = result * num;
            }

            Console.WriteLine("Result = " + result);
        }

        private static long _getPossibilities(long time, long distance)
        {
            long totalPossibilities = 0;
            for (long i = 0; i < time + 1; i++)
            {
                long distanceTravelled = _getDistance(i, time);
                if (distanceTravelled > distance)
                {
                    totalPossibilities++;
                }
            }
            Console.WriteLine($"time {time}, dist {distance}, totalPoss {totalPossibilities}");
            return totalPossibilities;
        }

        private static long _getDistance(long holdTime, long totalTime)
        {
            long speed = holdTime;
            long distance = (totalTime - holdTime) * speed;
            return distance;
        }
    }
}
