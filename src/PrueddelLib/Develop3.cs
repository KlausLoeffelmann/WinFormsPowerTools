using System.ComponentModel;

file class Develop3
{
    private static Develop3? _instance;

    public static Develop3 GetInstance()
    {
        return _instance ??= new Develop3();
    }

    public string FirstName { get; set; } = "Klaus";
    public string LastName { get; set; } = "Loeffelmann";
}

public partial class Develop4
{
    public class FooBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }

    public interface IBar<TSelf> : INotifyPropertyChanged
    {
        public abstract static TSelf CreateDocument();
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            //PropertyChanged?.Invoke(this, e);
        }
    }

    public partial class Foo : FooBase
    {
        public static Foo CreateDocument()
        {
            throw new NotImplementedException();
        }
    }

    public partial class Foo : IBar<Foo>
    {
        public string FirstName
        {
            get => "Klaus Loeffelmann";
            set
            {

            }
        }
    }
}
