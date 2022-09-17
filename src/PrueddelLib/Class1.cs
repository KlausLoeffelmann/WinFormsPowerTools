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

    public interface IAutoLayoutGroup
    {
    }

    public interface IAutoLayoutEntryField
    {
    }

    public interface IAutoLayoutLabel
    {
    }



}