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
        static char[][] MapArray;

        const int EmptyLineCount = 999999;

        static List<int> EmptyRowIndexes = new List<int>();
        static List<int> EmptyColIndexes = new List<int>();

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
            MapArray = codes.Select(x => x.ToCharArray()).ToArray();

            _addRowsAndColumnsIndexes();

            List<Point> galaxyPoints = _getAllGalaxies();

            long count = _getDistancesSum(galaxyPoints);
            Console.WriteLine("answer = " + count);
        }

        private static long _getDistancesSum(List<Point> galaxyPoints)
        {
            long sum = 0;
            for (int i = 0; i < galaxyPoints.Count; i++)
            {
                for (int j = i + 1; j < galaxyPoints.Count; j++)
                {
                    long dist = _getShortestPath(galaxyPoints[i], galaxyPoints[j]);
                    //Console.WriteLine($"Dist bw {i+1} and {j+1} is {dist}");
                    sum += dist;
                }
            }
            return sum;
        }

        private static long _getShortestPath(Point point1, Point point2)
        {
            int emptyRows = EmptyRowIndexes.Where(x => x > Math.Min(point1.Y, point2.Y) && x < Math.Max(point1.Y, point2.Y)).Count();
            int emptyColumns = EmptyColIndexes.Where(x => x > Math.Min(point1.X, point2.X) && x < Math.Max(point1.X, point2.X)).Count();
            int vertical = Math.Abs(point1.Y - point2.Y);
            int horiz = Math.Abs(point1.X - point2.X);
            return vertical + emptyRows * EmptyLineCount + horiz + emptyColumns * EmptyLineCount;
        }

        private static List<Point> _getAllGalaxies()
        {
            List<Point> galaxies = new List<Point>();
            for (int y = 0; y < MapArray.Length; y++)
            {
                for (int x = 0; x < MapArray[y].Length; x++)
                {
                    if (MapArray[y][x] == '#')
                    {
                        galaxies.Add(new Point(x, y));
                    }
                }
            }
            return galaxies;
        }

        private static void _addRowsAndColumnsIndexes()
        {
            for (int y = 0; y < MapArray.Length; y++)
            {
                if (MapArray[y].All(x => x == '.'))
                {
                    EmptyRowIndexes.Add(y);
                }
            }

            for (int x = 0; x < MapArray[0].Length; x++)
            {
                if (Enumerable.Range(0, MapArray.Length).Select(y => MapArray[y][x]).All(m => m == '.'))
                {
                    EmptyColIndexes.Add(x);
                }
            }
        }


    }

}
