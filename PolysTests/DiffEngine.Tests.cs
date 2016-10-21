using FluentAssertions;
using Xunit;
using Polys;

namespace PolysTests
{
    public class DiffEngineTest
    {
        [Theory]
        [InlineData("x^2", "2x")]
        [InlineData("x^3", "3x^2")]
        [InlineData("4x^5", "20x^4")]
        public void Diff(string input, string expected) => DiffEngine.Diff(input).Should().Be(expected);
    }
}
