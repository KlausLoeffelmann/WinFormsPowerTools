using PrueddelLib;

namespace PrueddelLib
{
    public class ContactModel
    {
        public Guid IDContact { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime SomeDate { get; set; }
    }

    public interface IAutoLayoutComponent
    {
    }

    public interface IAutoLayoutDocument
    {
    }

    public interface IAutoLayoutGroup
    {
    }

    public interface IAutoLayoutEntryField
    {
    }

    public interface IAutoLayoutLabel
    {
    }
    
    public partial class AutoLayoutContactController<T> where T:class
    {
    }
}

public interface IUITemplate
{
    public static abstract PrueddelLib.IAutoLayoutDocument CreateDocument();
}

namespace Consumer
{
    public class ContactUserControlGenerator : IUITemplate
    {
        static IAutoLayoutDocument IUITemplate.CreateDocument()
        {
            throw new NotImplementedException();
        }
    }
}
