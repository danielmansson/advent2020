using System;
using Advent2020.Problem;
using FluentAssertions;
using Xunit;

namespace Advent2020.Tests
{
    public class Day1Tests
    {
        [Fact]
        public void Test1()
        {
            var day = new Day1();

            var answer = day.Solve1("input");

            answer.Should().Be(-1);
        }
    }
}