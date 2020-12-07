using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day5 : BaseDay
    {
        public override string Example1 =>
            @"BFFFBBFRRR
FFFBBBFRRR
BBFFBBFRLL";

        class Input
        {
            public string row;
            public string column;
        }

        List<Input> Transform(string raw)
        {
            return raw
                .Split("\n")
                .Select(l => new Input()
                {
                    row = l.Substring(0, l.Length - 3)
                        .Replace("F", "0")
                        .Replace("B", "1"),
                    column = l.Substring(l.Length - 3)
                        .Replace("L", "0")
                        .Replace("R", "1")
                })
                .ToList();
        }
        
        int FromBinary(string input)
        {
            return Convert.ToInt32(input, 2);
        }

        public override object Solve1(string raw)
        {
            var input = Transform(raw);

            return input.Max(Seat);
        }

        int Seat(Input i)
        {
            var row = FromBinary(i.row);
            var column = FromBinary(i.column);
            
            return row * 8 + column;
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);
            var takenSeats = input.Select(Seat).ToList();

            var s = input.Min(i => FromBinary(i.row));
            var e = input.Min(i => FromBinary(i.row));
            
            for (int r = 1; r <= 126; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    var seat = r * 8 + c;

                    if (!takenSeats.Contains(seat) &&
                        takenSeats.Contains(seat + 1) &&
                        takenSeats.Contains(seat - 1))
                        return seat;
                }
            }

            return -1;
        }
    }
}