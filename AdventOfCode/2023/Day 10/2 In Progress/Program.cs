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

        public static List<PointGroups> PointGroupsMainList = new List<PointGroups>();

        public static PointGroups MainLoopGroup;

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

            MainLoopGroup = new PointGroups()
            {
                IsMainLoop = true
            };

            _populateMainLoop(mapArray, StartIndex, MainLoopGroup);
            PointGroupsMainList.Add(MainLoopGroup);

            _populateOtherGroups(mapArray);

            long count = 0;
            foreach (var group in PointGroupsMainList.Where(x => (!x.IsEndArea) && x.IsAdjacentToMainLoop && !x.IsMainLoop))
            {
                _addIfDoubleWalled(mapArray, group);
                if (group.IsDoubleAdjacentToMainLoop)
                {
                    continue;
                }
                count = count + group.Points.Count();
            }

            Console.WriteLine("answer = " + count);
        }

        private static void _addIfDoubleWalled(char[][] mapArray, PointGroups pointGroup)
        {
            bool leftDouble = false;
            bool rightDouble = false;
            bool bottomDouble = false;
            bool topDouble = false;

            int leftMostIndex = pointGroup.Points.Min(x => x.X);
            var leftPoints = pointGroup.Points.Where(x => x.X == leftMostIndex);

            foreach (var point in leftPoints)
            {
                int pointsCount = _getAllPointsInDirection(mapArray, point, Direction.West).Where(x => MainLoopGroup.Points.Contains(x)).Count();
                leftDouble = leftDouble || ((pointsCount % 2) == 0);
            }

            int rightMostIndex = pointGroup.Points.Max(x => x.X);
            var rightPoints = pointGroup.Points.Where(x => x.X == rightMostIndex);

            foreach (var point in rightPoints)
            {
                int pointsCount = _getAllPointsInDirection(mapArray, point, Direction.East).Where(x => MainLoopGroup.Points.Contains(x)).Count();
                rightDouble |= ((pointsCount % 2) == 0);
            }

            int topMostIndex = pointGroup.Points.Min(x => x.Y);
            var topPoints = pointGroup.Points.Where(x => x.Y == topMostIndex);

            foreach (var point in topPoints)
            {
                int pointsCount = _getAllPointsInDirection(mapArray, point, Direction.North).Where(x => MainLoopGroup.Points.Contains(x)).Count();
                topDouble |=((pointsCount % 2) == 0);
            }

            int bottonMostIndex = pointGroup.Points.Max(x => x.Y);
            var bottonPoints = pointGroup.Points.Where(x => x.Y == bottonMostIndex);

            foreach (var point in bottonPoints)
            {
                int pointsCount = _getAllPointsInDirection(mapArray, point, Direction.South).Where(x => MainLoopGroup.Points.Contains(x)).Count();
                bottomDouble |= ((pointsCount % 2) == 0);
            }

            pointGroup.IsDoubleAdjacentToMainLoop = leftDouble && rightDouble && topDouble && bottomDouble;

        }

        private static void _populateOtherGroups(char[][] mapArray)
        {
            for (int y = 0; y < mapArray.Length; y++)
            {
                for (int x = 0; x < mapArray[y].Length; x++)
                {
                    Point point = new Point(x, y);
                    if (!_containsInPointsGroup(point))
                    {
                        PointGroups allconnectedPoints = new PointGroups();
                        allconnectedPoints.Points.Add(point);
                        _addAllConnectedPoints(mapArray, point, allconnectedPoints);

                        PointGroupsMainList.Add(allconnectedPoints);
                    }
                }
            }

        }

        private static bool _containsInPointsGroup(Point point)
        {
            return PointGroupsMainList.Any(p => p.Points.Contains(point));
        }

        private static char _getCharacter(char[][] mapArray, Point point)
        {
            return mapArray[point.Y][point.X];
        }

        private static void _addAllConnectedPoints(char[][] mapArray, Point point, PointGroups allconnectedPoints)
        {


            List<Point> AdjacentPoints = _getAdjacentPoints(mapArray, point);

            if (AdjacentPoints.Count != 8)
            {
                allconnectedPoints.IsEndArea = true;
            }
            allconnectedPoints.IsAdjacentToMainLoop |= AdjacentPoints.Any(x => MainLoopGroup.Points.Contains(x));
            foreach (var adjacentPoint in AdjacentPoints)
            {
                if (allconnectedPoints.Points.Contains(adjacentPoint))
                {
                    continue;
                }
                if (_containsInPointsGroup(adjacentPoint))
                {
                    continue;
                }

                allconnectedPoints.Points.Add(adjacentPoint);
                _addAllConnectedPoints(mapArray, adjacentPoint, allconnectedPoints);

            }

        }

        private static void _populateMainLoop(char[][] mapArray, Point startIndex, PointGroups loopGroup)
        {
            List<Point> connectedToSPoints = _getAdjacentConnectedPoints(mapArray, startIndex);
            Debug.Assert(connectedToSPoints.Count == 2);

            Point route1Point = connectedToSPoints[0];
            Point route1PreviousPoint = startIndex;

            loopGroup.Points.Add(startIndex);
            loopGroup.Points.Add(route1Point);

            while (route1Point != startIndex)
            {
                var previousPoint1 = route1Point;

                route1Point = _getNextPoint(mapArray, route1Point, route1PreviousPoint);

                route1PreviousPoint = previousPoint1;
                loopGroup.Points.Add(route1Point);
            }
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

        private static List<Point> _getAdjacentPoints(char[][] mapArray, Point index)
        {
            List<Point> adjacentPoints = new();
            foreach (var size in AdjacentSizes)
            {
                Point point = index + size;
                if (_isValidPoint(mapArray, point))
                {
                    adjacentPoints.Add(point);
                }
            }
            return adjacentPoints;

        }

        private static bool _isValidPoint(char[][] mapArray, Point point)
        {
            return point.X >= 0 && point.Y >= 0 && point.Y < mapArray.Length && point.X < mapArray[point.Y].Length;
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

        public static List<Point> _getAllPointsInDirection(char[][]mapArray, Point currentPoint, Direction direction)
        {
            List<Point> points = new List<Point>();
            Point point = _getPointInDirection(currentPoint, direction);
            while(_isValidPoint(mapArray,point))
            {
                points.Add(point);
                point = _getPointInDirection(point, direction);
            }
            return points;
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

    public class PointGroups
    {
        public HashSet<Point> Points { get; set; }

        public bool IsEndArea { get; set; }

        public bool IsAdjacentToMainLoop { get; set; }

        public bool IsDoubleAdjacentToMainLoop { get; set; }

        public bool IsMainLoop { get; set; }

        public PointGroups()
        {
            Points = new HashSet<Point>();
        }
    }
}
