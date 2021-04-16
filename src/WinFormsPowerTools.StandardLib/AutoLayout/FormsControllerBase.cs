using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public interface IFormsController : INotifyPropertyChanged
    { 
    }

    public abstract class FormsControllerBase : IFormsController 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        virtual protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }

    public class TestModelFoo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    [FormsController(displayPropertySuffix: "DisplayName")]
    public partial class TestFormsController : FormsControllerBase
    {
        private double _foo;

        [FormsControllerProperty] private string _firstName;
        [FormsControllerProperty(propertyName: "LastName", displayName: "Last name:")] private string _lstName;
        [FormsControllerDisplay("First name", nameof(TestModelFoo.FirstName))] private string _firstNameDisplayName;
    }

    public partial class TestFormsController 
    {
        private double _bar;

        public double Foo
        {
            get
            {
                return _foo;
            }

            set
            {
                if (!object.Equals(_foo,value))
                {
                    _foo = value;
                    OnPropertyChanged(FooPropertyChangedEventArgs);
                }
            }
        }

        private PropertyChangedEventArgs __fooPropertyChangedEventArgs;
        private PropertyChangedEventArgs FooPropertyChangedEventArgs
        {
            get
            {
                if (__fooPropertyChangedEventArgs is null)
                {
                    __fooPropertyChangedEventArgs = new PropertyChangedEventArgs("Foo");
                }
                return __fooPropertyChangedEventArgs;
            }
        }
    }
}
