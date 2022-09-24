namespace WinFormsPowerTools.AutoLayout
{
    public interface IAutoLayoutGridElementDefinition<TSelf> where TSelf : IAutoLayoutGridElementDefinition<TSelf>, new()
    {
        AutoLayoutGridLength Value { get; set; }
        double? Min { get; set; }
        double? Max { get; set; }

        protected static bool TryParse(string value, out TSelf elementDefinition)
        {
            value = value.Trim();
            elementDefinition = new();

            if (string.IsNullOrWhiteSpace(value))
                return false;

            var valueItems = value.Split(':');

            if (valueItems.Length == 0)
            {
                return GetGridLength(value.Trim(), elementDefinition);
            }

            // First element must be GridLength
            var result = GetGridLength(valueItems[0].Trim(), elementDefinition);

            if (valueItems.Length > 1)
            {
                result &= TryParseMinOrMaxHeight(valueItems[1].Trim(), ref elementDefinition);
            }

            if (valueItems.Length > 2)
            {
                result &= TryParseMinOrMaxHeight(valueItems[2].Trim(), ref elementDefinition);
            }

            return result;

            static bool TryParseMinOrMaxHeight(string value, ref TSelf rowDefinition)
            {
                if (value[0] == '>')
                {
                    double minHeight;
                    var result = double.TryParse(value[1..], out minHeight);
                    rowDefinition.Min = minHeight;
                    return result;
                }
                else if (value[0] == '<')
                {
                    double maxHeight;
                    var result = double.TryParse(value[1..], out maxHeight);
                    rowDefinition.Max = maxHeight;
                    return result;
                }
                else
                {
                    return false;
                }
            }

            static bool GetGridLength(string value, TSelf rowDefinition)
            {
                bool result = AutoLayoutGridLength.TryParse(value, out var rowDefinitionHeight);
                rowDefinition.Value = rowDefinitionHeight;
                return result;
            }
        }
    }
}
