using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day3 : BaseDay
    {
        public override string Example1 =>
            @"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#";

        List<string> Transform(string raw)
        {
            return raw
                .Split("\n")
                .ToList();
        }

        char Get(List<string> map, int x, int y)
        {
            return map[y][(x % map[y].Length)];
        }

        public override object Solve1(string raw)
        {
            var input = Transform(raw);

            int treeCount = 0;

            int x = 0;
            int y = 0;

            while (true)
            {
                x += 3;
                y += 1;

                if (y >= input.Count)
                {
                    break;
                }

                if (Get(input, x, y) == '#')
                {
                    treeCount++;
                }
            }

            return treeCount;
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);

            var deltaList = new List<(int, int)>
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2),
            };

            int result = 1;
            foreach (var delta in deltaList)
            {
                result *= CountTrees(input, delta.Item1, delta.Item2);
            }

            return result;
        }

        private int CountTrees(List<string> input, int dx, int dy)
        {
            int treeCount = 0;
            int x = 0;

            int y = 0;
            while (true)
            {
                x += dx;
                y += dy;

                if (y >= input.Count)
                {
                    break;
                }

                if (Get(input, x, y) == '#')
                {
                    treeCount++;
                }
            }

            return treeCount;
        }
    }
}