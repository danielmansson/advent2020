using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day1 : BaseDay
    {
        public override string Example1 => 
            @"1721
979
366
299
675
1456";

        List<int> Transform(string raw)
        {
            return raw
                .Split("\n")
                .Select(l => int.Parse(l))
                .ToList();
        }

        public override object Solve1(string raw)
        {
            var input = Transform(raw);

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = i; j < input.Count; j++)
                {
                    if (input[i] + input[j] == 2020)
                    {
                        return input[i] * input[j];
                    }
                }
            }

            return -1;
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = i; j < input.Count; j++)
                {
                    for (int k = i; k < input.Count; k++)
                    {
                        if (input[i] + input[j] + input[k] == 2020)
                        {
                            return input[i] * input[j] * input[k];
                        }
                    }
                }
            }

            return -1;
        }
    }
}