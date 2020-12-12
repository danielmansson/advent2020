using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day11 : BaseDay
    {
        public override string Example1 =>
            @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";

        List<List<char>> Transform(string raw)
        {
            var split = raw
                .Split("\n")
                .Select(l => "." + l + ".")
                .ToList();

            var empty = new string('.', split[0].Length);

            split.Insert(0, empty);
            split.Add(empty);

            return split
                .Select(l => l.ToList())
                .ToList();
        }

        public override object Solve1(string raw)
        {
            var buf = new[]
            {
                Transform(raw),
                Transform(raw)
            };

            int read = 1;
            int write = 0;

            int w = buf[0][0].Count;
            int h = buf[0].Count;

            while (true)
            {
                int changes = 0;

                for (int i = 1; i < h - 1; i++)
                {
                    for (int j = 1; j < w - 1; j++)
                    {
                        var c = buf[read][i][j];

                        if (c == 'L')
                        {
                            var n = GetOccupiedNeighbors(buf[read], i, j);
                            buf[write][i][j] = n == 0 ? '#' : 'L';
                            changes += n == 0 ? 1 : 0;
                        }
                        else if (c == '#')
                        {
                            var n = GetOccupiedNeighbors(buf[read], i, j);
                            buf[write][i][j] = n >= 4 ? 'L' : '#';
                            changes += n >= 4 ? 1 : 0;
                        }
                    }
                }

                write = 1 - write;
                read = 1 - read;
                

                if (changes == 0)
                    break;
            }

            var count = buf[read]
                .Sum(l => l
                    .Count(c => c == '#'));

            return count;
        }

        void Print(List<List<char>> data)
        {
            foreach (var line in data)
            {
                Console.WriteLine(new string(line.ToArray()));
            }            
            
            Console.WriteLine();
            Console.WriteLine();
        }

        int GetOccupiedNeighbors(List<List<char>> data, int i, int j)
        {
            var sum = 0;

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    if (x != 0 || y != 0)
                        sum += data[i + y][j + x] == '#' ? 1 : 0;
                }
            }

            return sum;
        }

        public override object Solve2(string raw)
        {
            var buf = new[]
            {
                Transform(raw),
                Transform(raw)
            };

            int read = 1;
            int write = 0;

            int w = buf[0][0].Count;
            int h = buf[0].Count;

            while (true)
            {
                int changes = 0;

                for (int i = 1; i < h - 1; i++)
                {
                    for (int j = 1; j < w - 1; j++)
                    {
                        var c = buf[read][i][j];

                        if (c == 'L')
                        {
                            var n = GetVisibleNeighbors(buf[read], i, j);
                            buf[write][i][j] = n == 0 ? '#' : 'L';
                            changes += n == 0 ? 1 : 0;
                        }
                        else if (c == '#')
                        {
                            var n = GetVisibleNeighbors(buf[read], i, j);
                            buf[write][i][j] = n >= 5 ? 'L' : '#';
                            changes += n >= 4 ? 1 : 0;
                        }
                    }
                }

                write = 1 - write;
                read = 1 - read;
                

                if (changes == 0)
                    break;
            }

            var count = buf[read]
                .Sum(l => l
                    .Count(c => c == '#'));

            return count;
        }

        int GetVisibleNeighbors(List<List<char>> data, int i, int j)
        {
            var sum = 0;

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    if (x != 0 || y != 0)
                    {
                        var c = Ray(data, i, j, x, y);
                        sum += c == '#' ? 1 : 0;
                    }
                }
            }

            return sum;
        }

        char Ray(List<List<char>> data, int i, int j, int dx, int dy)
        {
            while (i > 0 && j > 0 && i < data.Count - 1 && j < data[0].Count - 1)
            {
                i += dy;
                j += dx;
                if (data[i][j] != '.')
                    return data[i][j];
            }

            return '.';
        }
    }
}