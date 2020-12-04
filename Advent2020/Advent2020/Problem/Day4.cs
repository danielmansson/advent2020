using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Advent2020.Util;

namespace Advent2020.Problem
{
    public class Day4 : BaseDay
    {
        public override string Example1 => 
            @"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in";

        public override string Example2 =>
            @"pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
hcl:#623a2f

eyr:2029 ecl:blu cid:129 byr:1989
iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm

hcl:#888785
hgt:164cm byr:2001 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022

iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719";

        List<string> schema = new List<string>()
        {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl",
            "pid",
        };

        private Dictionary<string, Func<string, bool>> validators = new Dictionary<string, Func<string, bool>>()
        {
            { 
                "byr", 
                i =>
                { 
                    var v = int.Parse(i);
                    return v >= 1920 && v <= 2002;
                }
            },
            { 
                "iyr", 
                i =>
                { 
                    var v = int.Parse(i);
                    return v >= 2010 && v <= 2020;
                }
            },
            { 
                "eyr", 
                i =>
                { 
                    var v = int.Parse(i);
                    return v >= 2020 && v <= 2030;
                }
            },
            { 
                "hgt", 
                i =>
                {
                    var end = i.Substring(i.Length - 2);
                    if (end == "in")
                    {
                        var v = int.Parse(i.Substring(0, i.Length - 2));
                        return v >= 59 && v <= 76;
                    }
                    else if (end == "cm")
                    {
                        var v = int.Parse(i.Substring(0, i.Length - 2));
                        return v >= 150 && v <= 193;
                    }

                    return false;
                }
            },
            { 
                "hcl", 
                i => i.Length == 7 && Regex.IsMatch(i, "#[a-f0-9]*")
            },
            { 
                "ecl", 
                i =>
                {
                    return new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}.Contains(i);
                }
            },
            { 
                "pid", 
                i =>
                {
                    if (i.Length != 9)
                        return false;
                    
                    return Regex.IsMatch(i, "[0-9]*");
                }
            },
            { 
                "cid", 
                i => true
            }
        };

        List<Dictionary<string, string>> Transform(string raw)
        {
            return raw
                .Split("\n\n")
                .Select(b => b
                    .Replace("\n", " ")
                    .Split(" ")
                    .Select(kvp => kvp.Split(":"))
                    .ToDictionary(kvp => kvp[0], kvp => kvp[1]))
                .ToList();
        }

        public override object Solve1(string raw)
        {
            var input = Transform(raw);

            return input.Count(CheckSchema);
        }

        bool CheckSchema(Dictionary<string, string> passport)
        {
            return schema.All(s => passport.Keys.Contains(s));
        }

        bool CheckValid(Dictionary<string, string> passport)
        {
            if (!CheckSchema(passport))
            {
                return false;
            }

            return passport.All(i => ValidateEntry(i.Key, i.Value));
        }

        bool ValidateEntry(string key, string value)
        {
            try
            {
                return validators[key].Invoke(value);
            }
            catch
            {
                return false;
            }
        }

        public override object Solve2(string raw)
        {
            var input = Transform(raw);

            return input.Count(CheckValid);
        }
    }
}