using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day15 : BaseDay
    {
        public override string Example1 => 
            @"0,3,6";

        List<int> Transform(string raw)
        {
            return raw
                .Split(",")
                .Select(int.Parse)
                .ToList();
        }

        public override object Solve1(string raw)
        {
            var input = Transform(raw);

            return Solve(input, 2020);
        }

        private static object Solve(List<int> input, int turnCount)
        {
            var count = new Dictionary<int, List<int>>();

            int turn = 0;
            var lastSpoken = 0;
            foreach (var num in input)
            {
                turn++;
                lastSpoken = num;
                if (num != input.Last())
                    count[lastSpoken] = new List<int>() {turn};
            }

            while (turn != turnCount)
            {
                if (!count.TryGetValue(lastSpoken, out var l))
                {
                    l = new List<int>();
                    count.Add(lastSpoken, l);
                }

                l.Add(turn);

                if (l.Count == 1)
                {
                    lastSpoken = 0;
                }
                else
                {
                    lastSpoken = l[^1] - l[^2];
                }

                turn++;
            }

            return lastSpoken;
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);

            return Solve(input, 30000000);
        }
    }
}