using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day18 : BaseDay
    {
        public override string Example1 =>
            @"2 * 3 + (4 * 5)
5 + (8 * 3 + 9 + 3 * 4 * 3)
5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))
((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";
        //26335
        public override string Example2 =>
            @"((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";

        List<string> Transform(string raw)
        {
            return raw
                .Replace(" ", "")
                .Split("\n")
                .ToList();
        }

        class Op
        {
            public char type;
            public long value;
            public List<Op> children;
        }

        public override object Solve1(string raw)
        {
            var input = Transform(raw);

            var sum = 0L;
            foreach (var str in input)
            {
                int idx = 0;
                var v = Calc(str, ref idx);
                //     Console.WriteLine(v);
                sum += v;
            }

            return sum;
        }

        long Calc(string input, ref int i)
        {
            var value = 0L;
            var op = '+';

            while (i < input.Length)
            {
                var c = input[i++];

                if (c == '+' || c == '*')
                {
                    op = c;
                }
                else if (c == ')')
                {
                    return value;
                }
                else
                {
                    long next;
                    if (c == '(')
                    {
                        next = Calc(input, ref i);
                    }
                    else
                    {
                        next = long.Parse(c.ToString());
                    }

                    if (op == '+')
                        value += next;
                    else
                        value *= next;
                }
            }

            return value;
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);

            var sum = 0L;
            foreach (var str in input)
            {
                int idx = 0;

                var t = str
                    .Select((c, idx) => new Token()
                    {
                        c = long.TryParse(c.ToString(), out var n) ? 'N' : c,
                        value = n,
                        depth = str.Substring(0, idx).Sum(s => s == '(' ? 1 : (s == ')' ? -1 : 0))
                    })
                    .ToList();

                var v = Calc2(t);
               // Console.WriteLine(v);
                sum += v;
            }

            return sum;
        }


        class Token
        {
            public char c;
            public long value;
            public int depth;
        }


        long Calc2(List<Token> tokens)
        {
            while (tokens.Count != 1)
            {
                int idx;

                // for (int i = 0; i < tokens.Count; i++)
                // {
                //     if(tokens[i].c == 'N')
                //         Console.Write(tokens[i].value);
                //     else
                //         Console.Write(tokens[i].c);
                // }
                // Console.WriteLine();

                var maxDepth = tokens.Max(t => t.depth);

                if ((idx = Match(tokens, new List<char>() {'N', '+', 'N'}, maxDepth)) != -1)
                {
                    var token = new Token()
                    {
                        c = 'N',
                        value = tokens[idx].value + tokens[idx + 2].value,
                        depth = tokens[idx].depth
                    };
                    tokens.RemoveRange(idx, 3);
                    tokens.Insert(idx, token);
                }
                else if ((idx = Match(tokens, new List<char>() {'(', 'N', ')'}, maxDepth)) != -1)
                {
                    tokens.RemoveAt(idx);
                    tokens.RemoveAt(idx + 1);
                    tokens[idx].depth--;
                }
                else if ((idx = Match(tokens, new List<char>() {'N', '*', 'N'}, maxDepth)) != -1)
                {
                    var token = new Token()
                    {
                        c = 'N',
                        value = tokens[idx].value * tokens[idx + 2].value,
                        depth = tokens[idx].depth
                    };
                    tokens.RemoveRange(idx, 3);
                    tokens.Insert(idx, token);
                }
                else
                {
                    throw new Exception();
                }
            }

            return tokens.First().value;
        }

        int Match(List<Token> tokens, List<char> c, int depth)
        {
            for (int i = 0; i <= tokens.Count - c.Count; i++)
            {
                if(tokens[i + 1].depth != depth)
                    continue;
                
                bool match = true;
                for (int j = 0; j < c.Count; j++)
                {
                    if (tokens[i + j].c != c[j])
                    {
                        match = false;
                        break;
                    }
                }

                if (match)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}