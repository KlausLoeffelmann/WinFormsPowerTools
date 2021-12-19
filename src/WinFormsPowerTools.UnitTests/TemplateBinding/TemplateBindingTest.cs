using System;
using System.Diagnostics;
using System.Windows.Forms.TemplateBinding;
using Xunit;

namespace WinFormsPowerTools.UnitTests.TemplateBinding
{
    public class TemplateBindingTest
    {
        private Employee GetTestEmployee()
            => new(null)
                {
                    IDEmployee = 42,
                    Contact = GetTestContact1()
                };

        private Contact GetTestContact1()
            => new(null)
            {
                FirstName = "John",
                LastName = "Doe",
                IDContact = 42,
                Address = new Address(null)
                {
                    IdAddress = 19,
                    AddressLine1 = "4711 NE 42 Str.",
                    AddressLine2 = "Apartment 42",
                    Country = "USA",
                    Zip = "98052",
                    State = "WA",
                    City = "Redmond",
                    DateCreated = DateTime.Now
                }
            };

        private Contact GetTestContact2()
            => new(null)
            {
                FirstName = "Jane",
                LastName = "Doe-Ardelean",
                IDContact = 19,
                Address = new Address(null)
                {
                    IdAddress = 42,
                    AddressLine1 = "4711 NE 19 Str.",
                    AddressLine2 = "Apartment 72",
                    Country = "USA",
                    Zip = "98019",
                    State = "WA",
                    City = "Duvall",
                    DateCreated = DateTime.Now
                }
            };

        private void BuildPropertyTree(Chain chain)
        {
            // We are simulating binding City, so we need to have the whole property path
            // build up as a node. This is what later needs to be code-generated from the 
            // binding definition.
            var chainLink = chain!.RootLink.AddLink(dataContext => ((Employee?)dataContext)?.Contact, nameof(Employee.Contact));
            chainLink = chainLink.AddLink(dataContext => ((Employee?)dataContext)?.Contact?.Address, nameof(Employee.Contact.Address));
            chainLink = chainLink.AddLink(dataContext => ((Employee?)dataContext)?.Contact?.Address?.City, nameof(Employee.Contact.Address.City), true);
        }

        private void ChainLinkValueChanged(object? sender, ChainValueChangedEventArgs e)
        {
            Debug.Print($"{e.PropertyName}:{e.ValueChangedReason}");
        }

        [Fact]
        public void SimplePcmTreeTest()
        {
            var dataSource = GetTestEmployee();
            Chain chain = new(dataSource);
            chain.ChainValueChanged += ChainLinkValueChanged;
            BuildPropertyTree(chain);
            dataSource!.Contact!.Address!.City = "New City!";
            dataSource!.Contact = GetTestContact2();
            dataSource!.Contact!.Address!.City = "New second City!";
            dataSource!.Contact = null;
            dataSource!.Contact = GetTestContact2();
            chain.DataContext = null;
        }
    }
}
