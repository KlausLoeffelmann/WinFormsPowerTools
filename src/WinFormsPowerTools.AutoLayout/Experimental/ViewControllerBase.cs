using System.ComponentModel;
using WinFormsPowerTools.StandardLib.ViewControllerBaseClasses;

namespace WinFormsPowerTools.AutoLayout
{
    public class TestModelFoo
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    [ViewController(displayPropertySuffix: "DisplayName")]
    [ViewControllerPropertySpecifier(nameof(ExistingProperty))]
    public partial class TestFormsController : ObservableObject
    {
        private double _foo;

        [PropertyMapping(AutoLayoutTarget.TextEntry,"First Name")] private string? _firstName;
        [PropertyMapping(propertyName: "LastName", displayName: "Last name:")] private string? _lstName;
        [PropertyMapping(displayName: "First name", mapsToModelProperty: nameof(TestModelFoo.FirstName))] private string? _firstNameDisplayName;

        public string? ExistingProperty { get; set; }
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
                    OnPropertyChanged(nameof(Foo));
                }
            }
        }

        private PropertyChangedEventArgs? __fooPropertyChangedEventArgs;
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
