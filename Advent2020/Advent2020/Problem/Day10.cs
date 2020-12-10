using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day10 : BaseDay
    {
        public override string Example1 => 
            @"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3";

        List<long> Transform(string raw)
        {
            return raw
                .Split("\n")
                .Select(long.Parse)
                .OrderBy(i => i)
                .ToList();
        }

        public override object Solve1(string raw)
        {
            var input = Transform(raw);
            
            var diffs = new List<long>();
            var current = 0L;

            foreach (var i in input)
            {
                var diff = i - current;

                if(diff == 0)
                    continue;
                
                if (diff > 3)
                    break;

                current += diff;
                diffs.Add(diff);
            }

            var numOneDiffs = diffs.Count(i => i == 1);
            var numThreeDiffs = diffs.Count(i => i == 3) + 1;

            return numOneDiffs * numThreeDiffs;
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);

            var prev = 0L;
            var ways = new Dictionary<long, long>();
            ways.Add(0, 1);

            foreach (var i in input)
            {
                var diff = i - prev;

                if (diff == 0)
                    continue;

                if (diff > 3)
                    break;

                var w = ways.GetValueOrDefault(i - 1, 0);
                w += ways.GetValueOrDefault(i - 2, 0);
                w += ways.GetValueOrDefault(i - 3, 0);
                
                ways.Add(i, w);
                prev = i;
            }

            return ways[prev];
        }
    }
}