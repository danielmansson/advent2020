using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day14 : BaseDay
    {
        public override string Example1 => 
            @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";

        public override string Example2 => @"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1";

        class Instr
        {
            public string Type;
            public long Addr;
            public string Raw;
            public long Value;
            public long Or;
            public long And;
        }

        List<Instr> Transform(string raw)
        {
            return raw
                .Split("\n")
                .Select(l =>
                {
                    var s = l.Split(" = ");
                    if (s[0] == "mask")
                    {
                        return new Instr()
                        {
                            Type = "mask",
                            Addr = 0,
                            Raw = s[1],
                            And = Convert.ToInt64(s[1].Replace("X", "1"), 2),
                            Or = Convert.ToInt64(s[1].Replace("X", "0"), 2),
                            Value = 0
                        };
                    }
                    else
                    {
                        return new Instr()
                        {
                            Type = "mem",
                            Addr = long.Parse(s[0].Replace("mem[", "").Replace("]", "")),
                            Raw = s[1],
                            Value = long.Parse(s[1])
                        };
                    }
                })
                .ToList();
        }

        public override object Solve1(string raw)
        {
            var input = Transform(raw);

            var mem = new Dictionary<long, long>();
            Instr mask = null;
            
            foreach (var instr in input)
            {
                if (instr.Type == "mask")
                {
                    mask = instr;
                }
                else
                {
                    var value = instr.Value;

                    value |= mask.Or;
                    value &= mask.And;

                    mem[instr.Addr] = value;
                }
            }
            
            var sum =  mem
                .Values
                .Aggregate((acc, x) => acc + x);

            return sum;
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);

            var mem = new Dictionary<long, long>();
            Instr mask = null;
            
            foreach (var instr in input)
            {
                if (instr.Type == "mask")
                {
                    mask = instr;
                }
                else
                {
                    var bits = mask.Raw
                        .Select((c, idx) => (c, idx: 35 - idx))
                        .Where(t => t.c == 'X')
                        .OrderBy(t => t.idx)
                        .ToList();

                    int count = 1 << bits.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var s = Convert.ToString(i, 2);
                        var addr = instr.Addr | mask.Or;

                        for (int j = 0; j < bits.Count; j++)
                        {
                            if (j < s.Length && s[s.Length - j - 1] == '1')
                            {
                                addr |= (1L << bits[j].idx);
                            }
                            else
                            {
                                addr &= ~(1L << bits[j].idx);
                            }
                        }
                        
                        mem[addr] = instr.Value;
                    }
                }
            }
            
            var sum =  mem
                .Values
                .Aggregate((acc, x) => acc + x);

            return sum;
        }
    }
}