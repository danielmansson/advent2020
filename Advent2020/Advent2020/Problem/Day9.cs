using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day9 : BaseDay
    {
        public override string Example1 => 
            @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576";

        List<long> Transform(string raw)
        {
            return raw
                .Split("\n")
                .Select(l => long.Parse(l))
                .ToList();
        }

        public override object Solve1(string raw)
        {
            var input = Transform(raw);

            int preamble = 5;
            if (input.Count > 40)
                preamble = 25;

            var prev = input.GetRange(0, preamble);
            input.RemoveRange(0, preamble);

            while (input.Count > 0)
            {
                if (!ContainsSum(prev, input[0]))
                    return input[0];
                
                prev.Add(input[0]);
                prev.RemoveAt(0);
                input.RemoveAt(0);
            }

            return -1;
        }

        bool ContainsSum(List<long> values, long expected)
        {
            for (int i = 0; i < values.Count; i++)
            {
                for (int j = i + 1; j < values.Count; j++)
                {
                    var sum = values[i] + values[j];
                    if (sum == expected)
                        return true;
                }
            }

            return false;
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);
            var expected = (long)Solve1(raw);

            for (int i = 0; i < input.Count; i++)
            {
                long sum = 0;
                for (int j = i; j < input.Count; j++)
                {
                    sum += input[j];

                    if (sum == expected)
                    {
                        var range = input.GetRange(i, j - i);
                        return range.Min() + range.Max();
                    }
                    
                    if (sum > expected)
                        break;
                }
            }
            
            return -1;
        }
    }
}