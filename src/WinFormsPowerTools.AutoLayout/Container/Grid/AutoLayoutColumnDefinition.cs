namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutColumnDefinition : IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>
    {
        public AutoLayoutGridLength Width
        {
            get => ((IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>)this).Value;
            set => ((IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>)this).Value = value;
        }

        public double? MinWidth
        {
            get => ((IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>)this).Min;
            set => ((IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>)this).Min = value;
        }

        public double? MaxWidth
        {
            get => ((IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>)this).Max;
            set => ((IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>)this).Max = value;
        }

        AutoLayoutGridLength IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>.Value { get; set; }
        double? IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>.Min { get; set; }
        double? IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>.Max { get; set; }

        public static bool TryParse(string value, out AutoLayoutColumnDefinition columnDefinition)
            => IAutoLayoutGridElementDefinition<AutoLayoutColumnDefinition>.TryParse(value, out columnDefinition);
    }
}
