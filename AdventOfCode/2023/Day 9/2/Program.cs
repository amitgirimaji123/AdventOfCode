using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace AdventOfCode
{
    class Program
    {
        static long count;


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
            List<List<long>> numbers = codes.Select(x => (x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(y => Convert.ToInt64(y))).ToList()).ToList();

            foreach (var sequence in numbers)
            {
                long number = _getFirstSeq(sequence);
                count += number;
            }
            
            Console.WriteLine("Finished! Count = " + count);
        }

        private static long _getFirstSeq(List<long> sequence)
        {
            Debug.Assert(sequence.Count > 0);
            List<long> differenceSeq = _getDiffSequence(sequence);
            if (differenceSeq.All(x=>x==0))
            {
                return sequence.First();
            }
          
            long number = _getFirstSeq(differenceSeq);
            return sequence.First() - number;

        }

        private static List<long> _getDiffSequence(List<long> sequence)
        {
            List<long> diff = new List<long>();
            for (int i = 0; i < sequence.Count-1; i++)
            {
                diff.Add(sequence[i + 1] - sequence[i]);
            }
            return diff;
        }
    }



}
