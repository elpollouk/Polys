﻿using FluentAssertions;
using Xunit;
using Polys;
using System;

namespace PolysTests
{
    public class DiffEngineTest
    {
        [Theory]
        [InlineData("2", "")]
        [InlineData("2^3", "")]
        [InlineData("x", "1")]
        [InlineData("3x", "3")]
        [InlineData("x^-1", "-x^-2")]
        [InlineData("2x^-2", "-4x^-3")]
        [InlineData("x^2", "2x")]
        [InlineData("x^3", "3x^2")]
        [InlineData("4x^5", "20x^4")]
        [InlineData("-x", "-1")]
        [InlineData("-x^2", "-2x")]
        [InlineData("-x^-3", "3x^-4")]
        [InlineData("-2x^3", "-6x^2")]
        [InlineData("-4x^-3", "12x^-4")]
        [InlineData("2bob^3", "6bob^2")]
        [InlineData("3x^2+x+1", "6x+1")]
        [InlineData("4x^2+5x+3", "8x+5")]
        [InlineData("x+x^-1", "1-x^-2")]
        public void Diff(string input, string expected) => DiffEngine.Diff(input).Should().Be(expected);

        [Theory]
        [InlineData("x2")]
        [InlineData("3x^")]
        [InlineData("x5x")]
        [InlineData("x^y")]
        [InlineData("^2")]
        [InlineData("--x")]
        [InlineData("x^--2")]
        [InlineData("x^-")]
        [InlineData("^")]
        [InlineData("")]
        [InlineData("$%^")]
        [InlineData("3x^2+")]
        [InlineData("2x+4^")]
        [InlineData("foo 3x^2 bar")]
        public void Diff_InvalidInput(string input) => Assert.Throws<FormatException>(() => DiffEngine.Diff(input));
    }
}
