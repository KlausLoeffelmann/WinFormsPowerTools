using System;

namespace WinFormsPowerTools.UnitTests.TemplateBinding
{
    public class Employee : BindableBase
    {
        private int _idEmployee;
        private Contact? _contact;

        public Employee(IServiceProvider? serviceProvider) : base(serviceProvider)
        { }

        public int IDEmployee
        {
            get => _idEmployee;
            set => SetProperty(ref _idEmployee, value);
        }

        public Contact? Contact
        {
            get => _contact;
            set => SetProperty(ref _contact, value);
        }
    }
}
