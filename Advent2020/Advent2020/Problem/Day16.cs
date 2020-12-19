using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day16 : BaseDay
    {
        public override string Example1 => 
            @"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12";
        public override string Example2 => 
            @"class: 0-1 or 4-19
departure row: 0-5 or 8-19
departure seat: 0-13 or 16-19

your ticket:
11,12,13

nearby tickets:
3,9,18
15,1,5
5,14,9
55,2,202";//143

        class Rule
        {
            public string name;
            public List<(int, int)> ranges = new List<(int, int)>();
        }

        class Ticket
        {
            public List<int> numbers;
        }

        class Input
        {
            public List<Rule> rules;
            public Ticket myTicket;
            public List<Ticket> otherTickets;
        }

        Input Transform(string raw)
        {
            var s = raw
                .Replace("your ticket:\n", "")
                .Replace("nearby tickets:\n", "")
                .Split("\n\n");
            
            return new Input()
            {
                rules = s[0]
                    .Split("\n")
                    .Select(l =>
                    {
                        var r = l.Split(": ");
                            return new Rule()
                            {
                                name = r[0],
                                ranges = r[1]
                                    .Split(" or ")
                                    .Select(e =>
                                    {
                                        var es = e.Split("-");
                                        return (int.Parse(es[0]), int.Parse(es[1]));
                                    })
                                    .ToList()
                            };
                        })
                    .ToList(),
                myTicket = new Ticket() { numbers = s[1].Split(",").Select(int.Parse).ToList() },
                otherTickets = s[2].Split("\n")
                    .Select(t => new Ticket() { numbers = t.Split(",").Select(int.Parse).ToList() })
                    .ToList()
            };
        }

        bool AnyMatch(List<Rule> rules, int num)
        {
            return rules.Any(r => r.ranges.Any(ra => ra.Item1 <= num && ra.Item2 >= num));
        }

        public override object Solve1(string raw)
        {
            var input = Transform(raw);

            var sum = 0;

            foreach (var ticket in input.otherTickets)
            {
                foreach (var num in ticket.numbers)
                {
                    if (!AnyMatch(input.rules, num))
                    {
                        sum += num;
                    }
                }
            }
            
            return sum;
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);

            input.otherTickets
                .RemoveAll(t => t.numbers.Any(n => !AnyMatch(input.rules, n)));

            var count = input.myTicket.numbers.Count;

            var orderedRules = new Dictionary<int, Rule>();
            while (input.rules.Count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if(orderedRules.ContainsKey(i))
                        continue;
                    
                    var rules = input.rules.Where(r =>
                            input.otherTickets
                                .All(t => r.ranges.Any(ra => InRange(ra, t.numbers[i]))))
                        .ToList();

                    if (rules.Count == 0 && input.rules.Count == 1)
                    {
                        rules = input.rules.ToList();
                    }

                    if (rules.Count == 1)
                    {
                        orderedRules.Add(i, rules[0]);
                        input.rules.Remove(rules[0]);
                    }
                }
            }

            var values = orderedRules
                .Where(r => r.Value.name.StartsWith("departure"))
                .Select(r => (long)input.myTicket.numbers[r.Key])
                .ToList();
            
            return values.Aggregate((acc, r) => acc * r);
        }

        private bool InRange((int, int) ra, int n)
        {
            return n >= ra.Item1 && n <= ra.Item2;
        }
    }
}