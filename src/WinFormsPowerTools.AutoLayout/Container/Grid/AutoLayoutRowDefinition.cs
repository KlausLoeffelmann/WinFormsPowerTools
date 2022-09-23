namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutRowDefinition : IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>
    {
        public AutoLayoutGridLength Height
        {
            get => ((IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>)this).Value;
            set => ((IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>)this).Value = value;
        }

        public double? MinHeight
        {
            get => ((IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>)this).Min;
            set => ((IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>)this).Min = value;
        }

        public double? MaxHeight
        {
            get => ((IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>)this).Max;
            set => ((IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>)this).Max = value;
        }

        AutoLayoutGridLength IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>.Value { get; set; }
        double? IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>.Min { get; set; }
        double? IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>.Max { get; set; }

        public static bool TryParse(string value, out AutoLayoutRowDefinition rowDefinition)
            => IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>.TryParse(value, out rowDefinition);
    }
}
