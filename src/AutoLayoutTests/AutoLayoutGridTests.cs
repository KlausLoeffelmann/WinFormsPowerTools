using WinFormsPowerTools.AutoLayout;
using WinFormsPowerTools.UnitTests.Support;

namespace AutoLayoutTests
{
    public class AutoLayoutGridTests
    {
        [Fact]
        public void SimpleGridGenerationTest()
        {
            bool result = AutoLayoutRowDefinition.TryParse("2*", out var rowDefinition);
            Assert.True(result);
            Assert.False(rowDefinition.Height.IsAuto);
            Assert.False(rowDefinition.Height.IsAbsolut);
            Assert.True(rowDefinition.Height.IsStar);
        }
    }
}