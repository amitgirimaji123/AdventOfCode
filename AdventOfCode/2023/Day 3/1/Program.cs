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

        private static long Sum = 0;
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
                    string numberStr = _getfullNumIfAdjSymbol(codesArray, i, ref j);
                    if (numberStr != null)
                    {
                        Console.WriteLine($"Line {i}: {numberStr}");
                        Sum += Convert.ToInt64(numberStr);
                    }
                }
            }
        }

        private static string _getfullNumIfAdjSymbol(char[][] codesArray, int i, ref int j)
        {
            string numStr = "";
            bool hasAdjacentSymbol = false;
            do
            {
                numStr = numStr + codesArray[i][j];
                hasAdjacentSymbol |= _checkAdjacentSymbol(codesArray, i, j);
                j++;
            } while (j < codesArray[i].Length &&char.IsDigit(codesArray[i][j]));

            if (hasAdjacentSymbol)
            {
                return numStr;
            }
            else
            {
                return null;
            }
        }

       
        private static bool _checkAdjacentSymbol(char[][] codesArray, int i, int j)
        {
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
                if (ch != '.' && !char.IsLetterOrDigit(ch))
                {
                    hasSymbol = true;
                    break;
                }
            }
            return hasSymbol;
        }


    }
}
