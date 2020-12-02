using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day2 : BaseDay
    {
        public override string Example1 => 
            @"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc";

        class Input
        {
            public int Min;
            public int Max;
            public char Letter;
            public string Password;
        }

        List<Input> Transform(string raw)
        {
            return raw
                .Split("\n")
                .Select(line =>
                {
                    var split = line.Split(" ");
                    var range = split[0].Split("-");
                    
                    return new Input()
                    {
                        Min = int.Parse(range[0]),
                        Max = int.Parse(range[1]),
                        Letter = split[1].Replace(":", "")[0],
                        Password = split[2]
                    };
                })
                .ToList();
        }

        public override object Solve1(string raw)
        {
            var input = Transform(raw);

            return input.Count(i =>
            {
                var c = i.Password.Count(c => c == i.Letter);
                return c >= i.Min && c <= i.Max;
            });
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);

            return input.Count(i => 
                i.Password[i.Min - 1] == i.Letter ^ 
                i.Password[i.Max - 1] == i.Letter);
        }
    }
}