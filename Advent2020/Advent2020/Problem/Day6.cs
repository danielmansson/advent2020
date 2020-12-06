using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day6 : BaseDay
    {
        public override string Example1 => 
            @"abc

a
b
c

ab
ac

a
a
a
a

b";

        class Group
        {
            public List<string> persons;
        }

        List<Group> Transform(string raw)
        {
            return raw
                .Split("\n\n")
                .Select(l =>  new Group()
                {
                    persons = l.Split("\n").ToList()
                })
                .ToList();
        }

        public override object Solve1(string raw)
        {
            var input = Transform(raw);

            return input.Sum(g => g.persons
                .SelectMany(p => p)
                .Distinct()
                .Count());
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);

            return input.Sum(g => g.persons
                .SelectMany(p => p)
                .GroupBy(c => c)
                .Count(c => c.Count() == g.persons.Count));
        }
    }
}