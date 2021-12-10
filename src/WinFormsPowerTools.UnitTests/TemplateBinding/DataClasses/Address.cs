using System;

namespace WinFormsPowerTools.UnitTests.TemplateBinding
{
    public class Address : BindableBase
    {
        private int _idAddress;
        private string? _addressLine1;
        private string? _addressLine2;
        private string? _city;
        private string? _country;
        private string? _zip;
        private string? _state;
        private DateTime _dateCreated = DateTime.Now;

        public Address(IServiceProvider? serviceProvider) : base(serviceProvider)
        { }

        public int IdAddress
        {
            get => _idAddress;
            set => SetProperty(ref _idAddress, value);
        }

        public string? AddressLine1
        {
            get => _addressLine1;
            set => SetProperty(ref _addressLine1, value);
        }

        public string? AddressLine2
        {
            get => _addressLine2;
            set => SetProperty(ref _addressLine2, value);
        }

        public string? City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public string? Zip
        {
            get => _zip;
            set => SetProperty(ref _zip, value);
        }

        public string? State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        public string? Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        public DateTime DateCreated
        {
            get => _dateCreated;
            set => SetProperty(ref _dateCreated, value);
        }
    }
}
