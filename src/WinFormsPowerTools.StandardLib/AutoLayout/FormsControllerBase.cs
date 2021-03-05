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

    [FormsController]
    public partial class TestFormsController : FormsControllerBase
    {
        private double _foo;

        [FormsControllerProperty] private string _firstName;
        [FormsControllerProperty("LastName")] private string _lstName;
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
