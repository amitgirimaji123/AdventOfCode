using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1NetFx
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] codes = File.ReadAllLines(@"C:\Users\AGirimaj\source\repos\ConsoleApp1NetFx\ConsoleApp1NetFx\file.txt");
            List<long> list = getNumbers(codes);
            long sum = list.Sum();
            Console.WriteLine(sum);
            //Sum=55701
        }

        static List<long> getNumbers(string[] codes)
        {
            List<long> nos = new List<long>(codes.Length);
            foreach (var code in codes)
            {
                int num = _getNum(code);
                nos.Add(num);
            }
            return nos;
        }

        private static int _getNum(string code)
        {
            List<string> numbers = new List<string>() { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            List<KeyValuePair<string, int>> matches = new List<KeyValuePair<string, int>>();

            foreach (var str in numbers)
            {
                if (code.Contains(str))
                {
                    var index = code.IndexOf(str);
                    var indexLast = code.LastIndexOf(str);
                    matches.Add(new KeyValuePair<string, int>(str, index));
                    if (index != indexLast)
                    {
                        matches.Add(new KeyValuePair<string, int>(str, indexLast));

                    }
                }
            }

            int min = matches.Min(x => x.Value);
            int max = matches.Max(x => x.Value);

            int first = numbers.IndexOf(matches.First(x => x.Value == min).Key) % 10;
            int last = numbers.IndexOf(matches.First(x => x.Value == max).Key) % 10;

            return first * 10 + last;

        }
    }


}
