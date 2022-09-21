using WinFormsPowerTools.UnitTests.Support;

namespace AutoLayoutTests
{
    public class UIControllerGeneratorTest
    {
        [Fact]
        public void EnsurePropertyGeneration()
        {
            var firstNameBackingField = nameof(OptionFormsController._firstName);
            var lastNameBackingField = nameof(OptionFormsController._lastName);
            var anotherBackingField = nameof(OptionFormsController.__anotherName);
            
        }
    }
}