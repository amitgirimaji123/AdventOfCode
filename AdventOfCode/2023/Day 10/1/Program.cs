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
        static List<Size> AdjacentSizes = new List<Size>()
            {
            new Size( 1 , 0 ),
            new Size( 1 , 1 ),
            new Size( 0 , 1 ),
            new Size(-1 , 1 ),
            new Size(-1 , 0 ),
            new Size(-1 , -1),
            new Size( 0 , -1),
            new Size( 1 , -1),
            };

        public enum Direction
        {
            North,
            East,
            South,
            West
        }

        public static Dictionary<char, Direction[]> NextDirection = new()
        {
            { '|', new Direction[] { Direction.North, Direction.South } },
            { '-', new Direction[] { Direction.East, Direction.West } },
            { 'L', new Direction[] { Direction.North, Direction.East } },
            { 'J', new Direction[] { Direction.North, Direction.West } },
            { '7', new Direction[] { Direction.South, Direction.West } },
            { 'F', new Direction[] { Direction.South, Direction.East } },
        };

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
            char[][] mapArray = codes.Select(x => x.ToCharArray()).ToArray();

            Point StartIndex = _getStartIndex(mapArray);

            long maxDistance = _findFarDist(mapArray, StartIndex);

            Console.WriteLine("answer = " + maxDistance);
        }

        private static long _findFarDist(char[][] mapArray, Point startIndex)
        {
            List<Point> connectedToSPoints = _getAdjacentConnectedPoints(mapArray, startIndex);
            Debug.Assert(connectedToSPoints.Count == 2);

            long furtherDistance = _getFurtherDistance(mapArray, connectedToSPoints, startIndex);

            return furtherDistance;
        }

        private static long _getFurtherDistance(char[][] mapArray, List<Point> connectedToSPoints, Point previousPoint)
        {
            Point route1Point = connectedToSPoints[0];
            Point route2Point = connectedToSPoints[1];

            Point route1PreviousPoint = previousPoint;
            Point route2PreviousPoint = previousPoint;

            long count = 1;
            while (route1Point != route2Point)
            {
                var previousPoint1 = route1Point;
                var previousPoint2 = route2Point;

                route1Point = _getNextPoint(mapArray, route1Point, route1PreviousPoint);
                route2Point = _getNextPoint(mapArray, route2Point, route2PreviousPoint);

                route1PreviousPoint = previousPoint1;
                route2PreviousPoint = previousPoint2;
                count++;
            }
            return count;
        }

        private static Point _getNextPoint(char[][] mapArray, Point currentPoint, Point previousPoint)
        {
            return _getConnectedPoints(mapArray, currentPoint).Where(x => x != previousPoint).First();
        }

        private static List<Point> _getAdjacentConnectedPoints(char[][] mapArray, Point index)
        {
            List<Point> adjacentPoints = new();
            foreach (var size in AdjacentSizes)
            {
                Point point = index + size;
                if (point.X >= 0 && point.Y >= 0)
                {
                    adjacentPoints.Add(point);
                }
            }
            var connected = adjacentPoints.Where(x => _getConnectedPoints(mapArray, x).Contains(index)).ToList();
            return connected;

        }

        private static List<Point> _getConnectedPoints(char[][] mapArray, Point point)
        {
            char ch = mapArray[point.Y][point.X];
            if (NextDirection.ContainsKey(ch))
            {
                Direction[] connectedDirections = NextDirection[ch];
                var returnVal = connectedDirections.Select(d => _getPointInDirection(point, d)).ToList();
                return returnVal;
            }
            return new List<Point>();
        }

        private static Point _getPointInDirection(Point currentPoint, Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return currentPoint + new Size(0, -1);
                case Direction.East:
                    return currentPoint + new Size(1, 0);
                case Direction.South:
                    return currentPoint + new Size(0, 1);
                case Direction.West:
                    return currentPoint + new Size(-1, 0);
                default:
                    throw new Exception();
            }
        }

        private static Point _getStartIndex(char[][] mapArray)
        {
            for (int i = 0; i < mapArray.Length; i++)
            {
                for (int j = 0; j < mapArray[i].Length; j++)
                {
                    if (mapArray[i][j] == 'S')
                    {
                        return new Point(j, i);

                    }
                }
            }

            throw new Exception("not found");
        }
    }

}
