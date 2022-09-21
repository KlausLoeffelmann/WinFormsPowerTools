using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WinFormsPowerTools.AutoLayout;

namespace WinFormsPowerTools.CodeGen
{
    internal class AutoLayoutSyntaxReceiver : ISyntaxReceiver
    {
        private readonly string ShortenedViewControllerAttributeName = nameof(ViewControllerAttribute).Replace("Attribute", string.Empty);
        private readonly string ShortenedViewControllerMappingAttributeName = nameof(ViewControllerMappingAttribute).Replace("Attribute", string.Empty);

        internal List<(
            ClassDeclarationSyntax classDeclaration,
            AttributeSyntax classAttribute,
            SyntaxTree syntaxTree,
            Dictionary<FieldDeclarationSyntax, AttributeSyntax> fieldLookup)> foundModelClasses = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classDeclaration && classDeclaration.AttributeLists.Count > 0)
            {
                //if (classDeclaration.BaseList?.Types.Count == 1)
                //{
                //    if (classDeclaration.BaseList.Types[0] is SimpleBaseTypeSyntax baseType &&
                //        baseType.Type is GenericNameSyntax genericType &&
                //        genericType.Identifier.Text == AutoLayoutGen.AutoLayoutFormsControllerBase)
                //    {
                //        var templateType = genericType.TypeArgumentList.Arguments[0] as IdentifierNameSyntax;
                //        foundModelClasses.Add((classDeclaration, templateType));
                //    }
                //}

                var attribute = classDeclaration
                    .AttributeLists
                    .SelectMany(lists => lists.Attributes)
                    .FirstOrDefault(attribute => TestAttributeName(attribute, ShortenedViewControllerAttributeName));

                if (attribute is not null)
                {
                    var fieldDictionary = new Dictionary<FieldDeclarationSyntax, AttributeSyntax>();
                    foundModelClasses.Add((classDeclaration, attribute, syntaxNode.SyntaxTree, fieldDictionary));

                    foreach (var nodeItem in classDeclaration.ChildNodes())
                    {
                        if (nodeItem is FieldDeclarationSyntax fieldDeclaration)
                        {
                            var fieldAttribute = fieldDeclaration
                                .AttributeLists
                                .SelectMany(lists => lists.Attributes)

                                // This throws in some cases: We can't reliably cast this to IdentifierNameSyntax.
                                .FirstOrDefault(attribute => TestAttributeName(attribute, ShortenedViewControllerMappingAttributeName));

                            if (fieldAttribute is not null)
                            {
                                fieldDictionary.Add(fieldDeclaration, fieldAttribute);
                            }
                        }
                    }
                }

                bool TestAttributeName(AttributeSyntax attribute, string name)
                {
                    if (Debugger.IsAttached)
                        Debugger.Break();
                        
                    if (attribute is null || attribute.Name is not IdentifierNameSyntax identifierName)
                    {
                        return false;
                    }

                    return identifierName.Identifier.ValueText == name;
                }
            }
        }
    }
}
