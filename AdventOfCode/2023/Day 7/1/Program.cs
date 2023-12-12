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
        private static Dictionary<Card, int> CardsBidDict = new();

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

            foreach (var line in codes)
            {
                var splits = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Card card = new Card(splits[0]);
                //Console.WriteLine($"card - {card.CardCombination}, {card.CardType}");
                CardsBidDict.Add(card, Convert.ToInt32(splits[1]));
            }

            List<KeyValuePair<Card, int>> ordered = CardsBidDict.OrderBy(x => x.Key, new CustomComaprer()).ToList();

            long sum = 0;
            for (int i = 0; i < ordered.Count; i++)
            {
                Console.WriteLine($"card - { ordered[i].Key.CardCombination}, {ordered[i].Value}, {i + 1}");
                sum = sum + ordered[i].Value * (i + 1);
            }
            Console.WriteLine("Sum = " + sum);
        }



        class Card
        {
            public string CardCombination { get; }

            public CardType CardType { get; set; }

            public Card(string cardCombination)
            {
                CardCombination = cardCombination;
                CardType = _getCardType();
            }

            private CardType _getCardType()
            {
                List<IGrouping<char, char>> groups = CardCombination.GroupBy(x => x).ToList();

                if (groups.Count == 1)
                {
                    return CardType.FiveOfAKind;
                }
                else if (groups.Count == 2 && groups.Any(x => x.Count() == 4))
                {
                    return CardType.FourOfAKind;
                }
                else if (groups.Count == 2 && groups.Any(x => x.Count() == 3) && groups.Any(x => x.Count() == 2))
                {
                    return CardType.FullHouse;
                }
                else if (groups.Count == 3 && groups.Any(x => x.Count() == 3))
                {
                    return CardType.ThreeOfAKind;
                }
                else if (groups.Count == 3 && groups.Where(x => x.Count() == 2).Count() == 2)
                {
                    return CardType.TwoPair;
                }
                else if (groups.Count == 4 && groups.Any(x => x.Count() == 2))
                {
                    return CardType.OnePair;
                }
                else return CardType.HighCard;
            }



        }

        class CustomComaprer : IComparer<Card>
        {
            public int Compare(Card x, Card y)
            {
                if (IsGreaterCard(x, y))
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }

            public static List<char> Strengths = new List<char>() { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };


            public bool IsGreaterCard(Card card1, Card card2)
            {
                if ((int)card1.CardType != (int)card2.CardType)
                {
                    return (int)card1.CardType > (int)card2.CardType;
                }
                for (int i = 0; i < card1.CardCombination.Length; i++)
                {
                    int strength1 = Strengths.IndexOf(card1.CardCombination.ElementAt(i));
                    int strength2 = Strengths.IndexOf(card2.CardCombination.ElementAt(i));
                    if (strength1 != strength2)
                    {
                        return strength1 > strength2;
                    }
                }
                Debug.Assert(false);
                return false;
            }
        }
    }
}
