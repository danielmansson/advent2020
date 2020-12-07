using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day7 : BaseDay
    {
        public override string Example1 => 
            @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";

        public override string Example2 =>
            @"shiny gold bags contain 2 dark red bags.
dark red bags contain 2 dark orange bags.
dark orange bags contain 2 dark yellow bags.
dark yellow bags contain 2 dark green bags.
dark green bags contain 2 dark blue bags.
dark blue bags contain 2 dark violet bags.
dark violet bags contain no other bags.";

        class Node
        {
            public string name;
            public List<Content> content;
            public List<string> parents = new List<string>();
        }

        class Content
        {
            public string name;
            public int count;
        }

        Dictionary<string, Node> Transform(string raw)
        {
            return raw
                .Replace("bags", "bag")
                .Replace(" bag", "")
                .Replace(".", "")
                .Split("\n")
                .Select(l =>
                {
                    var s = l.Split(" contain ");

                    return new Node()
                    {
                        name = s[0],
                        content = s[1]
                            .Split(", ")
                            .Where(b => b != "no other")
                            .Select(b =>
                            {
                                var i = b.IndexOf(' ');
                                return new Content()
                                {
                                    name = b.Substring(i + 1),
                                    count = int.Parse(b.Substring(0, i))
                                };
                            })
                            .ToList()
                    };
                })
                .ToDictionary(n => n.name, n => n);
        }

        public override object Solve1(string raw)
        {
            var input = Transform(raw);

            foreach (var node in input.Values)
            {
                foreach (var child in node.content)
                {
                    input[child.name].parents.Add(node.name);
                }
            }

            return GetParents(input, "shiny gold")
                .Distinct()
                .Count();;
        }

        private List<string> GetParents(Dictionary<string, Node> input, string id)
        {
            var node = input[id];
            var allParents = node.parents.SelectMany(p => GetParents(input, p)).ToList();
            allParents.AddRange(node.parents);
            return allParents;
        }

        int CountChildren(Dictionary<string, Node> input, string id)
        {
            var node = input[id];

            return 1 + node.content.Sum(c => c.count * CountChildren(input, c.name));
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);

            return CountChildren(input, "shiny gold") - 1;
        }
    }
}