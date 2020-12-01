using System;
using System.Linq;
using System.Reflection;
using Advent2020.Util;

namespace Advent2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var types = Assembly.GetAssembly(typeof(Program)).GetTypes();
            var days = types.Where(t => t.Name.StartsWith("Day")).ToList();

            int dayToRun = -1;

            if (dayToRun < 0)
            {
                dayToRun = days.Select(d => int.Parse(d.Name.Replace("Day", ""))).Max();
            }

            var type = days.FirstOrDefault(d => d.Name == "Day" + dayToRun);
            Console.Write("Running day ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(dayToRun);
            Console.ResetColor();

            if (type == null)
            {
                Console.WriteLine("Could not find day " + dayToRun);
            }
            else
            {
                var day = (BaseDay)Activator.CreateInstance(type);
                day.Run(2020, dayToRun);
            }
        }
    }
}