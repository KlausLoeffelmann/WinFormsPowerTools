using System.Collections.Generic;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutColumnDefinitions : List<AutoLayoutColumnDefinition>
    {
        public AutoLayoutColumnDefinitions(params string[] columnDefinitions)
        {
            foreach (string item in columnDefinitions)
            {
                if (AutoLayoutColumnDefinition.TryParse(item, out var columnDefinition))
                {
                    Add(columnDefinition);
                }
            }
        }

        public AutoLayoutColumnDefinitions(params AutoLayoutColumnDefinition[] columnDefinitions)
        {
            AddRange(columnDefinitions);
        }

        public AutoLayoutColumnDefinitions(IEnumerable<AutoLayoutColumnDefinition> columnDefinitions)
        {
            AddRange(columnDefinitions);
        }
    }
}
