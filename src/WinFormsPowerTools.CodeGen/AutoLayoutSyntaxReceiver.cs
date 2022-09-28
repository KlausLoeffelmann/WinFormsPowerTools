using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WinFormsPowerTools.AutoLayout;

namespace WinFormsPowerTools.CodeGen
{
    internal class AutoLayoutSyntaxReceiver : ISyntaxContextReceiver
    {
        private readonly string ShortenedViewControllerAttributeName = nameof(ViewControllerAttribute).Replace("Attribute", string.Empty);
        private readonly string ShortenedViewControllerMappingAttributeName = nameof(PropertyMappingAttribute).Replace("Attribute", string.Empty);
        private readonly string ShortenedCommandMappingAttributeName = nameof(CommandMappingAttribute).Replace("Attribute", string.Empty);

        internal List<ViewModelClassInfo> viewModelClassesInfo = new();

        public void OnVisitSyntaxNode(GeneratorSyntaxContext syntaxContext)
        {
            var syntaxNode = syntaxContext.Node;

            if (syntaxNode is ClassDeclarationSyntax classDeclaration && classDeclaration.AttributeLists.Count > 0)
            {
                var viewControllerAttribute = classDeclaration
                    .AttributeLists
                    .SelectMany(lists => lists.Attributes)
                    .FirstOrDefault(attribute => TestAttributeName(attribute, ShortenedViewControllerAttributeName));

                if (viewControllerAttribute is not null)
                {
                    var fieldDictionary = new Dictionary<IFieldSymbol, AttributeData>(SymbolEqualityComparer.Default);
                    var methodDictionary = new Dictionary<IMethodSymbol, AttributeData>(SymbolEqualityComparer.Default);

                    viewModelClassesInfo.Add(new(
                        classDeclaration,
                        viewControllerAttribute,
                        syntaxNode.SyntaxTree,
                        fieldDictionary,
                        methodDictionary));

                    var viewControllerSymbol = (INamedTypeSymbol)syntaxContext.SemanticModel.GetDeclaredSymbol(classDeclaration)!;

                    foreach (var memberSymbol in viewControllerSymbol.GetMembers())
                    {
                        if (memberSymbol is IFieldSymbol fieldSymbol)
                        {
                            var fieldViewControllerMappingAttribute = fieldSymbol.GetAttributes()
                                .FirstOrDefault(attribute => attribute?.AttributeClass?.Name == nameof(PropertyMappingAttribute));

                            if (fieldViewControllerMappingAttribute is not null)
                            {
                                fieldDictionary.Add(fieldSymbol, fieldViewControllerMappingAttribute);
                            }
                        }

                        // Let's find all methods which are attributed with the CommandMappingAttribute.
                        if (memberSymbol is IMethodSymbol methodSymbol)
                        {
                            if (Debugger.IsAttached)
                            {
                                Debugger.Break();
                            }

                            if (methodSymbol.Parameters.Length == 1 &&
                                methodSymbol.Parameters[0].Type.SpecialType == SpecialType.System_Object)
                            {
                                var commandMappingAttribute = methodSymbol.GetAttributes()
                                    .FirstOrDefault(attribute => attribute?.AttributeClass?.Name == nameof(CommandMappingAttribute));

                                if (commandMappingAttribute is not null)
                                {
                                    methodDictionary.Add(methodSymbol, commandMappingAttribute);
                                }
                            }
                        }
                    }
                }

                bool TestAttributeName(AttributeSyntax attribute, string name)
                {
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
