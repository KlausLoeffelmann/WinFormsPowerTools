using System;
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
                    Contact = new Contact(null)
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
                    }
                };

        private void BuildPropertyTree(Employee dataContext, PcmTree<object> pcmTree)
        {
            // We are simulating binding City, so we need to have the whole property path
            // build up as a node. This is what later needs to be code-generated from the 
            // binding definition.
            var node = pcmTree!.RootNode.AddNode(dataContext?.Contact);
            node = node.AddNode(dataContext?.Contact?.Address);
            node = node.AddNode(dataContext?.Contact?.Address?.City);
        }

        private void NodeValueChangedEventProc(object? sender, NodeValueChangedEventArgs<object> e)
        {
            if (sender is Contact || sender is Address)
            {
            }
        }


        [Fact]
        public void SimplePcmTreeTest()
        {
            var dataSource = GetTestEmployee();
            PcmTree<object> pcmTree = new(dataSource, NodeValueChangedEventProc);
            BuildPropertyTree(dataSource, pcmTree);
            dataSource!.Contact!.Address!.City = "New City!";
        }
    }
}
