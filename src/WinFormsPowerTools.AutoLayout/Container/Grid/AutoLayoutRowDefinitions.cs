using System.Collections.Generic;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutRowDefinitions : List<AutoLayoutRowDefinition>
    {
        public AutoLayoutRowDefinitions(params string[] rowDefinitions)
        {
            foreach (string item in rowDefinitions)
            {
                if (AutoLayoutRowDefinition.TryParse(item, out var rowDefinition))
                {
                    Add(rowDefinition);
                }
            }
        }

        public AutoLayoutRowDefinitions(params AutoLayoutRowDefinition[] rowDefinitions)
        {
            AddRange(rowDefinitions);
        }

        public AutoLayoutRowDefinitions(IEnumerable<AutoLayoutRowDefinition> rowDefinitions)
        {
            AddRange(rowDefinitions);
        }

        public static AutoLayoutRowDefinitions Default
            => new AutoLayoutRowDefinitions("*");
    }
}
