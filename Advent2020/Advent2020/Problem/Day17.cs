using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day17 : BaseDay
    {
        public override string Example1 =>
            @".#.
..#
###";

        HashSet<(int, int, int)> Transform(string raw)
        {
            return raw.Split("\n")
                .Select((l, y) => l.ToCharArray()
                    .Select((c, x) => (x, y, 0, c))
                    .Where(x => x.c == '#')
                    .Select(x => (x.x, x.y, 0)))
                .SelectMany(a => a)
                .ToHashSet();
        }

        public override object Solve1(string raw)
        {
            var current = Transform(raw);
            
            for (int i = 0; i < 6; i++)
            {
                var activeNeighbors = GetActiveNeighbors(current);
                var next = new HashSet<(int, int, int)>();

                foreach (var kvp in activeNeighbors)
                {
                    if (current.Contains(kvp.Key))
                    {
                        if (kvp.Value >= 2 && kvp.Value <= 3)
                        {
                            next.Add(kvp.Key);
                        }
                    }
                    else
                    {
                        if (kvp.Value == 3)
                        {
                            next.Add(kvp.Key);
                        }
                    }
                }

                current = next;
            }

            return current.Count;
        }

        void Print(HashSet<(int, int, int)> set, int z)
        {
            Console.WriteLine($"z={z}");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(set.Contains((j - 2, i - 2, z)) ? "#" : "." );
                }
                Console.WriteLine();
            }
        }

        private Dictionary<(int, int, int), int> GetActiveNeighbors(HashSet<(int, int, int)> current)
        {
            var dict = new Dictionary<(int, int, int), int>();
            
            foreach (var active in current)
            {
                Neighbors(active, n =>
                { 
                    dict[n] = dict.GetValueOrDefault(n, 0) + 1;
                });
            }

            return dict;
        }

        private Dictionary<(int, int, int, int), int> GetActiveNeighbors4d(HashSet<(int, int, int, int)> current)
        {
            var dict = new Dictionary<(int, int, int, int), int>();
            
            foreach (var active in current)
            {
                Neighbors4d(active, n =>
                { 
                    dict[n] = dict.GetValueOrDefault(n, 0) + 1;
                });
            }

            return dict;
        }

        void Neighbors((int, int, int) pos, Action<(int, int, int)> cb)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        if(i == 0 && j == 0 && k == 0)
                            continue;
                        
                        cb((pos.Item1 + i, pos.Item2 + j, pos.Item3 + k));
                    }
                }
            }
        }
        
        void Neighbors4d((int, int, int, int) pos, Action<(int, int, int, int)> cb)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int l = -1; l <= 1; l++)
                        {
                            if (i == 0 && j == 0 && k == 0 && l ==0)
                                continue;
                        
                            cb((pos.Item1 + i, pos.Item2 + j, pos.Item3 + k, pos.Item4 + l));
                        }
                    }
                }
            }
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);

            var current = input
                .Select(i => (i.Item1, i.Item2, i.Item3, 0))
                .ToHashSet();
            
            for (int i = 0; i < 6; i++)
            {
                var activeNeighbors = GetActiveNeighbors4d(current);
                var next = new HashSet<(int, int, int, int)>();

                foreach (var kvp in activeNeighbors)
                {
                    if (current.Contains(kvp.Key))
                    {
                        if (kvp.Value >= 2 && kvp.Value <= 3)
                        {
                            next.Add(kvp.Key);
                        }
                    }
                    else
                    {
                        if (kvp.Value == 3)
                        {
                            next.Add(kvp.Key);
                        }
                    }
                }

                current = next;
            }

            return current.Count;
        }
    }
}