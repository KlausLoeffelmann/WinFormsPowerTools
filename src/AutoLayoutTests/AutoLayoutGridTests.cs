using WinFormsPowerTools.AutoLayout;
using WinFormsPowerTools.UnitTests.Support;

namespace AutoLayoutTests
{
    public class AutoLayoutGridTests
    {
        

        [Fact]
        public void SimpleRowDefinitionTest()
        {
            bool result = AutoLayoutRowDefinition.TryParse("*", out var rowDefinition);
            TestDefinitionConditions(
                result, rowDefinition, expectedValue: 1, 
                expectedStarCondition: true, expectedAutoCondition: false,
                expectedAbsoluteCondition: false);

            result = AutoLayoutRowDefinition.TryParse("2*", out rowDefinition);
            TestDefinitionConditions(
                result, rowDefinition, expectedValue: 2,
                expectedStarCondition: true, expectedAutoCondition: false,
                expectedAbsoluteCondition: false);

            result = AutoLayoutRowDefinition.TryParse("10*", out rowDefinition);
            TestDefinitionConditions(
                result, rowDefinition, expectedValue: 10,
                expectedStarCondition: true, expectedAutoCondition: false,
                expectedAbsoluteCondition: false);

            result = AutoLayoutRowDefinition.TryParse("73.42", out rowDefinition);
            TestDefinitionConditions(
                result, rowDefinition, expectedValue: 73.42,
                expectedStarCondition: false, expectedAutoCondition: false,
                expectedAbsoluteCondition: true);

            result = AutoLayoutRowDefinition.TryParse("Auto", out rowDefinition);
            TestDefinitionConditions(
                result, rowDefinition, expectedValue: null,
                expectedStarCondition: false, expectedAutoCondition: true,
                expectedAbsoluteCondition: false);

            result = AutoLayoutRowDefinition.TryParse("2*:>50.42", out rowDefinition);
            TestDefinitionConditions(
                result, rowDefinition, expectedValue: 2,
                expectedStarCondition: true, expectedAutoCondition: false,
                expectedAbsoluteCondition: false, expectedMin: 50.42, expectedMax: null);

            result = AutoLayoutRowDefinition.TryParse("2.5  * : < 420.73", out rowDefinition);
            TestDefinitionConditions(
                result, rowDefinition, expectedValue: 2.5,
                expectedStarCondition: true, expectedAutoCondition: false,
                expectedAbsoluteCondition: false, expectedMin: null, expectedMax: 420.73);

            result = AutoLayoutRowDefinition.TryParse("*  :  > 50  :  < 200", out rowDefinition);
            TestDefinitionConditions(
                result, rowDefinition, expectedValue: 1,
                expectedStarCondition: true, expectedAutoCondition: false,
                expectedAbsoluteCondition: false, expectedMin: 50, expectedMax: 200);

            static void TestDefinitionConditions(
                bool result,
                AutoLayoutRowDefinition rowDefinition,
                double? expectedValue,
                bool expectedStarCondition,
                bool expectedAutoCondition,
                bool expectedAbsoluteCondition,
                double? expectedMin = null,
                double? expectedMax = null)
            {
                Assert.True(result);
                Assert.Equal(expectedAutoCondition, rowDefinition.Height.IsAuto);
                Assert.Equal(expectedStarCondition, rowDefinition.Height.IsStar);
                Assert.Equal(expectedAbsoluteCondition, rowDefinition.Height.IsAbsolut);
                Assert.Equal(expectedValue, rowDefinition.Height.Value);
                Assert.Equal(expectedMin, rowDefinition.MinHeight);
                Assert.Equal(expectedMax, rowDefinition.MaxHeight);
            }
        }

        [Fact]
        public void SimpleColumnDefinitionTest()
        {
            bool result = AutoLayoutColumnDefinition.TryParse("*", out var columnDefinition);
            TestDefinitionConditions(
                result, columnDefinition, expectedValue: 1,
                expectedStarCondition: true, expectedAutoCondition: false,
                expectedAbsoluteCondition: false);

            result = AutoLayoutColumnDefinition.TryParse("2*", out columnDefinition);
            TestDefinitionConditions(
                result, columnDefinition, expectedValue: 2,
                expectedStarCondition: true, expectedAutoCondition: false,
                expectedAbsoluteCondition: false);

            result = AutoLayoutColumnDefinition.TryParse("10*", out columnDefinition);
            TestDefinitionConditions(
                result, columnDefinition, expectedValue: 10,
                expectedStarCondition: true, expectedAutoCondition: false,
                expectedAbsoluteCondition: false);

            result = AutoLayoutColumnDefinition.TryParse("73.42", out columnDefinition);
            TestDefinitionConditions(
                result, columnDefinition, expectedValue: 73.42,
                expectedStarCondition: false, expectedAutoCondition: false,
                expectedAbsoluteCondition: true);

            result = AutoLayoutColumnDefinition.TryParse("Auto", out columnDefinition);
            TestDefinitionConditions(
                result, columnDefinition, expectedValue: null,
                expectedStarCondition: false, expectedAutoCondition: true,
                expectedAbsoluteCondition: false);

            result = AutoLayoutColumnDefinition.TryParse("2*:>50.42", out columnDefinition);
            TestDefinitionConditions(
                result, columnDefinition, expectedValue: 2,
                expectedStarCondition: true, expectedAutoCondition: false,
                expectedAbsoluteCondition: false, expectedMin: 50.42, expectedMax: null);

            result = AutoLayoutColumnDefinition.TryParse("2.5  * : < 420.73", out columnDefinition);
            TestDefinitionConditions(
                result, columnDefinition, expectedValue: 2.5,
                expectedStarCondition: true, expectedAutoCondition: false,
                expectedAbsoluteCondition: false, expectedMin: null, expectedMax: 420.73);

            result = AutoLayoutColumnDefinition.TryParse("*  :  > 50  :  < 200", out columnDefinition);
            TestDefinitionConditions(
                result, columnDefinition, expectedValue: 1,
                expectedStarCondition: true, expectedAutoCondition: false,
                expectedAbsoluteCondition: false, expectedMin: 50, expectedMax: 200);

            static void TestDefinitionConditions(
                bool result,
                AutoLayoutColumnDefinition columnDefinition,
                double? expectedValue,
                bool expectedStarCondition,
                bool expectedAutoCondition,
                bool expectedAbsoluteCondition,
                double? expectedMin = null,
                double? expectedMax = null)
            {
                Assert.True(result);
                Assert.Equal(expectedAutoCondition, columnDefinition.Width.IsAuto);
                Assert.Equal(expectedStarCondition, columnDefinition.Width.IsStar);
                Assert.Equal(expectedAbsoluteCondition, columnDefinition.Width.IsAbsolut);
                Assert.Equal(expectedValue, columnDefinition.Width.Value);
                Assert.Equal(expectedMin, columnDefinition.MinWidth);
                Assert.Equal(expectedMax, columnDefinition.MaxWidth);
            }
        }

    }
}
