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

        //const int ExtraAddCount = 999999;
        const int ExtraAddCount = 1;


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

            _addDummyRowsAndColumns();

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
            int vertical = Math.Abs(point1.Y - point2.Y);
            int horiz = Math.Abs(point1.X - point2.X);
            return vertical + horiz;
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

        private static void _addDummyRowsAndColumns()
        {
            Console.WriteLine("Adding rows...");
            for (int y = 0; y < MapArray.Length; y++)
            {
                if (MapArray[y].All(x => x == '.'))
                {
                    int colLength = MapArray[y].Length;
                    char[] row = Enumerable.Range(0, colLength).Select(x => '.').ToArray();
                    var mapArrayList = MapArray.ToList();
                    for (int i = 0; i < ExtraAddCount; i++)
                    {
                        mapArrayList.Insert(y, row);
                    }
                    MapArray = mapArrayList.ToArray();
                    y += ExtraAddCount;
                }
            }

            for (int x = 0; x < MapArray[0].Length; x++)
            {
                if (Enumerable.Range(0, MapArray.Length).Select(y => MapArray[y][x]).All(m => m == '.'))
                {
                    Console.WriteLine("Adding for column " + x);
                    char[] col = Enumerable.Range(0, MapArray[0].Length).Select(x => '.').ToArray();

                    for (int y = 0; y < MapArray.Length; y++)
                    {
                        var row = MapArray[y].ToList();
                        for (int i = 0; i < ExtraAddCount; i++)
                        {
                            row.Insert(x, '.');
                        }
                        MapArray[y] = row.ToArray();
                    }
                    x+=ExtraAddCount;
                }
            }
            Console.WriteLine("Finished adding");
        }


    }

}
