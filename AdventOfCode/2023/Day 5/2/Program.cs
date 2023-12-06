﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {

        //private static List<long> seeds = new List<long>() { 79, 14, 55, 13 };

        //private static List<Map> SeedSoilMap = new()
        //{
        //    new Map(50, 98, 2),
        //    new Map(52, 50, 48),
        //};

        //private static List<Map> SoilFertilizerMap = new()
        //{
        //    new Map(0, 15, 37),
        //    new Map(37, 52, 2),
        //    new Map(39, 0, 15),
        //};

        //private static List<Map> FertilizerWaterMap = new()
        //{
        //    new Map(49, 53, 8),
        //    new Map(0, 11, 42),
        //    new Map(42, 0, 7),
        //    new Map(57, 7, 4),
        //};
        //private static List<Map> WaterLightMap = new()
        //{
        //    new Map(88, 18, 7),
        //    new Map(18, 25, 70)
        //};
        //private static List<Map> LightTempMap = new()
        //{
        //    new Map(45, 77, 23),
        //    new Map(81, 45, 19),
        //    new Map(68, 64, 13),
        //};
        //private static List<Map> TempHumidityMap = new()
        //{
        //    new Map(0, 69, 1),
        //    new Map(1, 0, 69),
        //};
        //private static List<Map> HumidityLocationMap = new()
        //{
        //    new Map(60, 56, 37),
        //    new Map(56, 93, 4),
        //};

        /// <summary>
        /// Final Min = 59370572
        /// Elapsed time = 421769.4801ms
        /// Elapsed time = 7.029491335mins
        /// </summary>
        /// 


        private static List<long> seeds = new List<long>() { 3416930225, 56865175, 4245248379, 7142355, 1808166864, 294882110, 863761171, 233338109, 4114335326, 67911591, 1198254212, 504239157, 3491380151, 178996923, 3965970270, 15230597, 2461206486, 133606394, 2313929258, 84595688 };

        private static List<Map> SeedSoilMap = new()
        {
            new Map(3534435790, 4123267198, 50004089),
            new Map(3584439879, 3602712894, 238659237),
            new Map(2263758314, 0, 160870825),
            new Map(2971481857, 2850687195, 31776688),
            new Map(4173604159, 3353763588, 121363137),
            new Map(3823099116, 3003258545, 350505043),
            new Map(2850687195, 2882463883, 120794662),
            new Map(1503174517, 2076905328, 347723811),
            new Map(1850898328, 195477286, 412859986),
            new Map(1265521310, 1606247567, 17062682),
            new Map(3285153612, 4173271287, 121696009),
            new Map(488201540, 828927797, 777319770),
            new Map(453595079, 160870825, 34606461),
            new Map(3406849621, 3475126725, 127586169),
            new Map(1282583992, 608337272, 220590525),
            new Map(3003258545, 3841372131, 281895067),
            new Map(0, 1623310249, 453595079),
        };

        private static List<Map> SoilFertilizerMap = new()
        {
            new Map(131427930, 1185330183, 180485664),
            new Map(748806267, 2475960003, 160820884),
            new Map(311913594, 3858074623, 436892673),
            new Map(3738185633, 2255483282, 220476721),
            new Map(909627151, 2636780887, 1221293736),
            new Map(2848518198, 1365815847, 889667435),
            new Map(2130920887, 131427930, 666553095),
            new Map(2797473982, 1134285967, 51044216),
            new Map(3958662354, 797981025, 336304942),
        };

        private static List<Map> FertilizerWaterMap = new()
        {
            new Map(318410581, 1095359367, 168721315),
            new Map(1850530626, 4267113166, 11515024),
            new Map(1868157768, 2129267011, 114327162),
            new Map(3662276437, 4191001581, 22313216),
            new Map(2980811924, 3765310336, 180818294),
            new Map(3971289879, 3991326516, 15449292),
            new Map(4217905563, 2279561459, 35118050),
            new Map(2287003279, 4213314797, 47187938),
            new Map(1837473204, 2314679509, 13057422),
            new Map(1998824036, 3946128630, 45197886),
            new Map(222462505, 1264080682, 95948076),
            new Map(487131896, 791770420, 303588947),
            new Map(4253023613, 4068573955, 41943683),
            new Map(2334191217, 3252696905, 278223677),
            new Map(3986739171, 2786104784, 83568826),
            new Map(3495640623, 3600917310, 61383314),
            new Map(3557023937, 3662300624, 15042395),
            new Map(2222048360, 3535962391, 64954919),
            new Map(2919013777, 4006775808, 61798147),
            new Map(3660033649, 4182646675, 2242788),
            new Map(3266573705, 2327736931, 229066918),
            new Map(3684589653, 2869673610, 235425729),
            new Map(3261531896, 3530920582, 5041809),
            new Map(790720843, 417781090, 105163807),
            new Map(895884650, 0, 159434487),
            new Map(2044021922, 2608078346, 178026438),
            new Map(1862045650, 4184889463, 6112118),
            new Map(3572066332, 3677343019, 87967317),
            new Map(1830862773, 4260502735, 6610431),
            new Map(1338009275, 159434487, 258346603),
            new Map(3233759255, 1830862773, 27772641),
            new Map(1055319137, 1360028758, 282690138),
            new Map(1982484930, 4278628190, 16339106),
            new Map(2883046491, 2243594173, 35967286),
            new Map(2612414894, 1858635414, 270631597),
            new Map(4070307997, 3105099339, 147597566),
            new Map(3161630218, 4110517638, 72129037),
            new Map(1596355878, 522944897, 46363018),
            new Map(0, 569307915, 222462505),
            new Map(3920015382, 2556803849, 51274497),
        };
        private static List<Map> WaterLightMap = new()
        {
            new Map(3185219492, 1324735395, 185266775),
            new Map(3146586681, 1249776213, 38632811),
            new Map(28244350, 428471809, 312696716),
            new Map(340941066, 3650819202, 117391304),
            new Map(458332370, 963661621, 286114592),
            new Map(2785969088, 3483794777, 117695215),
            new Map(1279106820, 1583617352, 194624401),
            new Map(1473731221, 3601489992, 49329210),
            new Map(0, 2796264154, 28244350),
            new Map(744446962, 3356330358, 127464419),
            new Map(2562107674, 223281414, 205190395),
            new Map(3544929092, 0, 223281414),
            new Map(2767298069, 944990602, 18671019),
            new Map(2903664303, 775683406, 169307196),
            new Map(3072971499, 1510002170, 73615182),
            new Map(1140236238, 741168525, 34514881),
            new Map(1242780449, 1288409024, 36326371),
            new Map(1523060431, 1778241753, 949993071),
            new Map(871911381, 3088005501, 268324857),
            new Map(3370486267, 2913562676, 174442825),
            new Map(4096117687, 4180659844, 114307452),
            new Map(1174751119, 2728234824, 68029330),
            new Map(4210425139, 4096117687, 84542157),
            new Map(2473053502, 2824508504, 89054172),
        };
        private static List<Map> LightTempMap = new()
        {
            new Map(57304962, 1726059676, 351776583),
            new Map(1567802332, 965133212, 510033927),
            new Map(3296678005, 3095070487, 435408435),
            new Map(2476702913, 3609026358, 293401880),
            new Map(1363411758, 0, 204390574),
            new Map(1017340727, 204390574, 346071031),
            new Map(2770104793, 2148583721, 409359530),
            new Map(994650876, 1475167139, 22689851),
            new Map(0, 550461605, 20445762),
            new Map(20445762, 1692752937, 33306739),
            new Map(2144982382, 2092188566, 56395155),
            new Map(799754929, 1497856990, 194895947),
            new Map(53752501, 961580751, 3552461),
            new Map(409081545, 570907367, 390673384),
            new Map(2201377537, 3902428238, 275325376),
            new Map(3988154754, 2557943251, 306812542),
            new Map(3179464323, 4177753614, 117213682),
            new Map(3732086440, 3530478922, 78547436),
            new Map(3810633876, 2864755793, 177520878),
            new Map(2092188566, 3042276671, 52793816),
        };
        private static List<Map> TempHumidityMap = new()
        {
            new Map(18928354, 3414191527, 36074961),
            new Map(3774151818, 3588716061, 144651966),
            new Map(2046448856, 1384376044, 7569690),
            new Map(2737178317, 903028814, 27883660),
            new Map(2981004508, 930912474, 349046239),
            new Map(1609626976, 3084565214, 120015958),
            new Map(2765061977, 2248931514, 215942531),
            new Map(157942811, 3887783726, 359543023),
            new Map(3330050747, 2195105292, 25002873),
            new Map(2041971091, 78356652, 4477765),
            new Map(1938796124, 861459710, 41569104),
            new Map(1608860129, 2474627144, 766847),
            new Map(1812604664, 3795542966, 74143188),
            new Map(4196563819, 3386335999, 27024746),
            new Map(1980365228, 3204581172, 61605863),
            new Map(1136292153, 82834417, 94266783),
            new Map(4015908448, 1279958713, 104417331),
            new Map(2701246339, 2062523244, 35931978),
            new Map(4223588565, 3286123619, 23738184),
            new Map(91521115, 3309861803, 56668597),
            new Map(1886747852, 2552562071, 52048272),
            new Map(1729642934, 0, 61644451),
            new Map(1791287385, 1504576228, 21317279),
            new Map(517485834, 1726582276, 261020280),
            new Map(2192468119, 2604610343, 479954871),
            new Map(1413642666, 1391945734, 112630494),
            new Map(71715516, 3366530400, 19805599),
            new Map(3918803784, 2475393991, 77168080),
            new Map(1546685190, 3733368027, 62174939),
            new Map(2672422990, 2220108165, 28823349),
            new Map(148189712, 2464874045, 9753099),
            new Map(18097572, 3413360745, 830782),
            new Map(0, 3869686154, 18097572),
            new Map(3995971864, 3266187035, 19936584),
            new Map(853426802, 432206724, 27759827),
            new Map(2054018546, 3450266488, 138449573),
            new Map(881186629, 177101200, 255105524),
            new Map(778506114, 1987602556, 74920688),
            new Map(1526273160, 2098455222, 20412030),
            new Map(55003315, 61644451, 16712201),
            new Map(3355053620, 459966551, 401493159),
            new Map(4120325779, 2118867252, 76238040),
            new Map(1230558936, 1525893507, 183083730),
            new Map(3756546779, 1708977237, 17605039),
        };
        private static List<Map> HumidityLocationMap = new()
        {
            new Map(166973311, 827072705, 21017988),
            new Map(2420657564, 1797988486, 98294592),
            new Map(3711057743, 1724407580, 50543704),
            new Map(151140540, 331672683, 15832771),
            new Map(2611368935, 1098267426, 35837870),
            new Map(329110209, 0, 39413233),
            new Map(848395356, 3293184115, 2008343),
            new Map(850403699, 1508109908, 216297672),
            new Map(3203490893, 3278069000, 15115115),
            new Map(1322210923, 4179894531, 115072765),
            new Map(3761601447, 848395356, 249872070),
            new Map(230668724, 39413233, 27530262),
            new Map(1437283688, 2240143464, 113813014),
            new Map(1551096702, 2957294790, 77256502),
            new Map(716039283, 695021295, 132051410),
            new Map(258198986, 109620920, 70911223),
            new Map(1314022212, 2357817442, 8188711),
            new Map(2793895840, 2474472017, 281155944),
            new Map(385486933, 347505454, 330552350),
            new Map(3218606008, 3561131747, 206517169),
            new Map(1066701371, 3354107909, 156810008),
            new Map(4011473517, 1134105296, 283493779),
            new Map(0, 180532143, 151140540),
            new Map(368523442, 678057804, 16963491),
            new Map(1223511379, 1417599075, 90510833),
            new Map(3183517648, 1988699857, 19973245),
            new Map(2270463718, 4038012896, 141881635),
            new Map(1817187310, 1774951284, 23037202),
            new Map(1963899457, 2755627961, 201666829),
            new Map(2412345353, 3295192458, 8312211),
            new Map(1840224512, 3767648916, 123674945),
            new Map(187991299, 66943495, 42677425),
            new Map(2518952156, 1896283078, 92416779),
            new Map(1628353204, 3034551292, 138620276),
            new Map(3075051784, 2366006153, 108465864),
            new Map(3660454503, 3303504669, 50603240),
            new Map(3425123177, 2008673102, 231470362),
            new Map(3656593539, 2353956478, 3860964),
            new Map(1766973480, 3510917917, 50213830),
            new Map(2647206805, 3891323861, 146689035),
            new Map(2165566286, 3173171568, 104897432),
        };



        private static long[] ThreadMinValues;
        static async Task Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            await Calcualte();
            stopwatch.Stop();
            Console.WriteLine("Elapsed time = " + stopwatch.Elapsed.TotalMilliseconds + "ms");
            Console.WriteLine("Elapsed time = " + stopwatch.Elapsed.TotalMinutes + "mins");
            Console.ReadKey();

        }

        private async static Task Calcualte()
        {
            //IEnumerable<string> codes = File.ReadLines("Input.txt");
            if (seeds.Count % 2 != 0)
            {
                throw new Exception("Odd Seeds Count");
            }

            int totalThreads = seeds.Count / 2;

            ThreadMinValues = new long[totalThreads];

            List<Task> tasksList = new List<Task>();
            for (int i = 0, threadCount = 0; i < seeds.Count; i += 2, threadCount++)
            {
                long start = seeds[i];
                long count = seeds[i + 1];
                int threadId = threadCount;
                Task task = Task.Run(() => getMin(start, count, threadId));
                tasksList.Add(task);
            }

            foreach (var item in tasksList)
            {
                await item;
            }

            long min = ThreadMinValues.Min();

            Console.WriteLine("Final Min = " + min);
        }

        private static void getMin(long value, long length, int threadId)
        {
            Console.WriteLine($"Starting Task {value} , {length} , {threadId}");
            long minLoc = long.MaxValue;
            long count = 0;
            for (long i = value; i < value + length; i++)
            {
                long location = _getLocation(i);
                minLoc = Math.Min(location, minLoc);
                count++;
                if (count % 1000000 == 0)
                {
                    Console.WriteLine("ThreadId " + threadId + ": Completed = " + (count / (double)length * 100.0) + "%");
                }
            }
            lock (lock_obj)
            {
                ThreadMinValues[threadId] = minLoc;
            }
            Console.WriteLine("ThreadId " + threadId + ": finished. Min=" + minLoc);
        }

        private static object lock_obj = new();

        //private static List<long> _getallSeeds(List<long> seeds)
        //{
        //    List<long> allSeeds = new List<long>();
        //    for (int i = 0; i < seeds.Count; i = i + 2)
        //    {
        //        for (int j = 0; j < seeds[i + 1]; j++)
        //        {
        //            allSeeds.Add(seeds[i] + j);
        //        }
        //    }
        //    return allSeeds;
        //}

        //private static long calculate(List<long> allSeeds)
        //{
        //    long minLoc = long.MaxValue;
        //    long count = 0;
        //    foreach (long seed in allSeeds)
        //    {
        //        long location = _getLocation(seed);
        //        minLoc = Math.Min(location, minLoc);
        //        count++;
        //        if (count % 1000 == 0)
        //        {
        //            Console.WriteLine("Completed = " + (count / (double)allSeeds.Count * 100.0) + "%");
        //        }
        //    }
        //    return minLoc;
        //}

        private static long _getLocation(long seed)
        {
            long soil = _getDestination(SeedSoilMap, seed);
            long fert = _getDestination(SoilFertilizerMap, soil);
            long water = _getDestination(FertilizerWaterMap, fert);
            long light = _getDestination(WaterLightMap, water);
            long temp = _getDestination(LightTempMap, light);
            long humid = _getDestination(TempHumidityMap, temp);
            long loc = _getDestination(HumidityLocationMap, humid);
            return loc;
        }

        private static long _getDestination(List<Map> maps, long src)
        {
            foreach (var map in maps)
            {
                if (map.ContainsSource(src))
                {
                    return map.GetDestination(src);
                }
            }
            return src;
        }
    }

    public class Map
    {
        public Map(long destination, long src, long length)
        {
            Src = src;
            Destination = destination;
            Length = length;
            Difference = Src - Destination;
        }

        public long Src { get; }
        public long Destination { get; }
        public long Length { get; }

        private readonly long Difference;

        public bool ContainsSource(long srcNum)
        {
            return srcNum >= Src && srcNum < (Src + Length);
        }

        public long GetDestination(long srcNum)
        {
            return srcNum - Difference;
        }
    }
}
