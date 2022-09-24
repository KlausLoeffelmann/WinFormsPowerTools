namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutColumnDefinition : IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>
    {
        public AutoLayoutGridLength Width
        {
            get => ((IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>)this).Value;
            set => ((IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>)this).Value = value;
        }

        public double? MinWidth
        {
            get => ((IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>)this).Min;
            set => ((IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>)this).Min = value;
        }

        public double? MaxWidth
        {
            get => ((IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>)this).Max;
            set => ((IAutoLayoutGridElementDefinition<AutoLayoutRowDefinition>)this).Max = value;
        }

        AutoLayoutGridLength IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>.Value { get; set; }
        double? IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>.Min { get; set; }
        double? IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>.Max { get; set; }

        public static bool TryParse(string value, out AutoLayoutColumnDefinition columnDefinition)
            => IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>.TryParse(value, out columnDefinition);
    }
}
