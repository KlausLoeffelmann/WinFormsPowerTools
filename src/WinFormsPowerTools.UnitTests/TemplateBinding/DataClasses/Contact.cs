using System;

namespace WinFormsPowerTools.UnitTests.TemplateBinding
{
    public class Contact : BindableBase
    {
        private int _idContact;
        private string? _firstName;
        private string? _lastName;
        private Address? _address;

        public Contact(IServiceProvider? serviceProvider) : base(serviceProvider)
        { }

        public int IDContact
        {
            get => _idContact;
            set => SetProperty(ref _idContact, value);
        }

        public string? FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string? LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public Address? Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
    }
}
