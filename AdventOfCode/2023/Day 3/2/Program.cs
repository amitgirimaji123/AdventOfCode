using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        //Day 3-1 

        private static long Sum = 0;
        private static Dictionary<Point, List<long>> StarPointNumbers = new Dictionary<Point, List<long>>();
        private static List<int[]> combinations = new List<int[]>()
        {
            new int[] { 1 , 0 },
            new int[] { 1 , 1 },
            new int[] { 0 , 1 },
            new int[] {-1 , 1 },
            new int[] {-1 , 0 },
            new int[] {-1 , -1},
            new int[] { 0 , -1},
            new int[] { 1 , -1},
        };

        static void Main(string[] args)
        {
            IEnumerable<string> codes = File.ReadLines("Input.txt");
            getNumbers(codes);

            foreach (var starPoints in StarPointNumbers)
            {
                if(starPoints.Value.Count == 2)
                {
                    long num = starPoints.Value[0] * starPoints.Value[1];
                    Sum += num;
                }
            }


            Console.WriteLine(Sum);
            Console.ReadKey();
        }


        static void getNumbers(IEnumerable<string> codes)
        {
            char[][] codesArray = codes.Select(x => x.ToCharArray()).ToArray();

            for (int i = 0; i < codesArray.Length; i++)
            {
                for (int j = 0; j < codesArray[i].Length; j++)
                {
                    if (!char.IsDigit(codesArray[i][j]))
                    {
                        continue;
                    }
                    string numberStr = _getfullNumIfAdjSymbol(codesArray, i, ref j, out Point? starPoint);
                    if (numberStr != null)
                    {
                        Console.WriteLine($"Line {i}: {numberStr}");
                        Console.WriteLine($"starPoint : {starPoint}");
                        if (!StarPointNumbers.ContainsKey(starPoint.Value))
                        {
                            StarPointNumbers.Add(starPoint.Value, new List<long>());
                        }

                        long number = Convert.ToInt64(numberStr);
                        StarPointNumbers[starPoint.Value].Add(number);
                    }
                }
            }
        }

        private static string _getfullNumIfAdjSymbol(char[][] codesArray, int i, ref int j, out Point? starPoint)
        {
            Point? localStarPoint = null;
            string numStr = "";
            bool hasAdjacentSymbol = false;
            do
            {
                numStr = numStr + codesArray[i][j];
                hasAdjacentSymbol |= _checkAdjacentStar(codesArray, i, j, out Point? starPoint2);
                j++;
                if (starPoint2 != null)
                {
                    if (localStarPoint != null && starPoint2 != null && localStarPoint != starPoint2)
                    {
                        Debug.Assert(false);
                    }
                    localStarPoint = starPoint2;
                }
            } while (j < codesArray[i].Length && char.IsDigit(codesArray[i][j]));


            starPoint = localStarPoint;
            if (hasAdjacentSymbol)
            {
                return numStr;
            }
            else
            {
                return null;
            }
        }


        private static bool _checkAdjacentStar(char[][] codesArray, int i, int j, out Point? starPoint)
        {
            starPoint = null;
            bool hasSymbol = false;
            foreach (int[] combination in combinations)
            {
                int i2 = i + combination[0];
                int j2 = j + combination[1];
                if (i2 < 0 || j2 < 0 || i2 >= codesArray.Length || j2 >= codesArray[i2].Length)
                {
                    continue;
                }
                char ch = codesArray[i2][j2];
                if (ch == '*')
                {
                    hasSymbol = true;
                    starPoint = new Point(i2, j2);
                    break;
                }
            }
            return hasSymbol;
        }


    }
}
