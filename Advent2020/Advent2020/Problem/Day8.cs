using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day8 : BaseDay
    {
        public override string Example1 => 
            @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6";

        class Instruction
        {
            public string name;
            public int add = 0;
            public int jmp = 1;
        }

        List<Instruction> Transform(string raw)
        {
            return raw
                .Split("\n")
                .Select(l =>
                {
                    var s = l.Split(" ");
                    var i = new Instruction() { name = s[0] };
                    if (s[0] == "jmp")
                        i.jmp = int.Parse(s[1]);
                    else if (s[0] == "acc")
                        i.add = int.Parse(s[1]);
                    return i;
                })
                .ToList();
        }

        public override object Solve1(string raw)
        {
            var input = Transform(raw);

            var visited = new HashSet<int>();
            int cur = 0;
            int acc = 0;

            while (!visited.Contains(cur))
            {
                visited.Add(cur);
                var i = input[cur];
                cur += i.jmp;
                acc += i.add;
            }

            return acc;
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);

            var jmps = input.Where(i => i.name == "jmp").ToList();

            foreach (var jmpInstr in jmps)
            {
                int pJmp = jmpInstr.jmp;
                jmpInstr.jmp = 1;
            
                var visited = new HashSet<int>();
                int cur = 0;
                int acc = 0;

                while (!visited.Contains(cur))
                {
                    visited.Add(cur);

                    if (cur >= input.Count)
                        return acc;
                
                    var i = input[cur];
                    cur += i.jmp;
                    acc += i.add;
                }

                jmpInstr.jmp = pJmp;
            }

            return -1;
        }
    }
}