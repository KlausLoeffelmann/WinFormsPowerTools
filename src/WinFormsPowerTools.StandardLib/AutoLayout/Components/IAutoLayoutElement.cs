namespace WinFormsPowerTools.AutoLayout
{
    // We need this basically only for the mapping to real components/controls.
    public interface IAutoLayoutElement
    {
        string Name { get; set; }
        object Tag { get; set; }
    }
}
