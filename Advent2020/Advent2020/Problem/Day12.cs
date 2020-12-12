using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day12 : BaseDay
    {
        public override string Example1 => 
            @"F10
N3
F7
R90
F11";

        class Instruction
        {
            public char Cmd;
            public int Amount;

            public int Turn;
            public int Forward;
            public int X;
            public int Y;
        }

        List<Instruction> Transform(string raw)
        {
            return raw
                .Split("\n")
                .Select(l =>
                {
                    var cmd = l[0];
                    var amount = int.Parse(l.Substring(1));
                    
                    return new Instruction()
                    {
                        Cmd = cmd,
                        Amount = amount,
                        X = cmd == 'E' ? amount : cmd == 'W' ? -amount : 0,
                        Y = cmd == 'N' ? amount : cmd == 'S' ? -amount : 0,
                        Forward = cmd == 'F' ? amount : 0,
                        Turn = cmd == 'R' ? amount / 90 : cmd == 'L' ? -amount / 90 : 0
                    };
                })
                .ToList();
        }

        private int[][] lookup = new[]
        {
            new[] {1, 0},
            new[] {0, -1},
            new[] {-1, 0},
            new[] {0, 1},
        };

        public override object Solve1(string raw)
        {
            var input = Transform(raw);

            int dir = 0;
            int x = 0;
            int y = 0;

            foreach (var instruction in input)
            {
                dir = (dir + instruction.Turn + 4) % 4;
                x += instruction.X;
                y += instruction.Y;
                x += instruction.Forward * lookup[dir][0];
                y += instruction.Forward * lookup[dir][1];
                
                //Console.WriteLine($"x:{x}  y:{y}");
            }

            var dist = Math.Abs(x) + Math.Abs(y);
            return dist;
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);

            int x = 0;
            int y = 0;
            int wx = 10;
            int wy = 1;

            foreach (var instr in input)
            {
                switch (instr.Cmd)
                {
                    case 'N':
                        wy += instr.Amount;
                        break;
                    case 'S':
                        wy -= instr.Amount;
                        break;
                    case 'E':
                        wx += instr.Amount;
                        break;
                    case 'W':
                        wx -= instr.Amount;
                        break;
                    case 'L':
                        for (int i = 0; i < instr.Amount/90; i++)
                        {
                            var tmp = wx;
                            wx = -wy;
                            wy = tmp;
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < instr.Amount/90; i++)
                        {
                            var tmp = wy;
                            wy = -wx;
                            wx = tmp;
                        }
                        break;
                    case 'F':
                        x += wx * instr.Amount;
                        y += wy * instr.Amount;
                        break;
                }
            }

            var dist = Math.Abs(x) + Math.Abs(y);
            return dist;
        }
    }
}