using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFormsPowerTools.CodeGen
{
    [Generator]
    public class AutoLayoutGen : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not AutoLayoutSyntaxReceiver syntaxReceiver)
            {
                throw new InvalidOperationException("Got the wrong syntax receiver type.");
            }


            if (syntaxReceiver.foundModelClasses.Count == 0)
            {
                return;
            }

            var identifiersAndClasses = syntaxReceiver.foundModelClasses;
            var sementicModel = context.Compilation.GetSemanticModel(
                identifiersAndClasses[0]
                .classDeclaration
                .SyntaxTree);
            string ind = "    ";


            foreach (var item in identifiersAndClasses)
            {
                var namedTypeSymbol = (INamedTypeSymbol)sementicModel.GetTypeInfo(item.identifier).Type;
                var @namespace = namedTypeSymbol.ContainingNamespace;
                var properties = namedTypeSymbol.GetMembers().OfType<IPropertySymbol>();

                StringBuilder extensionClass = new();
                extensionClass.AppendLine($"using DataEntryForms.AutoLayout;");
                extensionClass.AppendLine($"using System.Collections.Generic;");
                extensionClass.AppendLine($"");
                extensionClass.AppendLine($"public static class {item.classDeclaration.Identifier.Text}Extensions");
                extensionClass.AppendLine("{");

                StringBuilder viewModelClass = new();
                viewModelClass.AppendLine($"using DataEntryForms.AutoLayout;");
                viewModelClass.AppendLine($"using {@namespace};");
                viewModelClass.AppendLine($"");
                viewModelClass.AppendLine($"public partial class {item.classDeclaration.Identifier.Text} : AutoLayoutViewModelBase<{item.identifier.Identifier.Text}>");
                viewModelClass.AppendLine("{");

                if (properties is not null)
                {
                    foreach (IPropertySymbol symbol in properties)
                    {
                        extensionClass.AppendLine($"{ind}public static AutoLayoutComponents Add{symbol.Name}(this AutoLayoutComponents components)");
                        extensionClass.AppendLine($"{ind}{{");
                        extensionClass.AppendLine($"{ind + ind}if (components is null)");
                        extensionClass.AppendLine($"{ind + ind}{{");
                        extensionClass.AppendLine($"{ind + ind + ind}components = new AutoLayoutComponents() {{ Components = new List<AutoLayoutComponent>() }};");
                        extensionClass.AppendLine($"{ind + ind}}}");
                        extensionClass.AppendLine($"{ind + ind}var component = new AutoLayoutComponent(\"{symbol.Name}\");");
                        extensionClass.AppendLine($"{ind + ind}components.Components.Add(component);");
                        extensionClass.AppendLine($"{ind + ind}components.LastComponent = component;");
                        extensionClass.AppendLine($"{ind + ind}return components;");
                        extensionClass.AppendLine($"{ind}}}");
                    }
                }

                extensionClass.AppendLine("}");
                viewModelClass.AppendLine("}");

                if (extensionClass is not null)
                {
                    context.AddSource(hintName: $"{item.classDeclaration.Identifier.Text}Extensions.g.cs", extensionClass.ToString());
                }
                if (viewModelClass is not null)
                {
                    context.AddSource(hintName: $"{item.classDeclaration.Identifier.Text}.g.cs", viewModelClass.ToString());
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new AutoLayoutSyntaxReceiver());
        }
    }

    internal class AutoLayoutSyntaxReceiver : ISyntaxReceiver
    {
        private const string AutoLayoutViewModelBase = nameof(AutoLayoutViewModelBase);

        internal List<(ClassDeclarationSyntax classDeclaration, IdentifierNameSyntax identifier)> foundModelClasses
            = new List<(ClassDeclarationSyntax, IdentifierNameSyntax)>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classDeclaration)
            {
                if (classDeclaration.BaseList?.Types.Count == 1)
                {
                    if (classDeclaration.BaseList.Types[0] is SimpleBaseTypeSyntax baseType &&
                        baseType.Type is GenericNameSyntax genericType &&
                        genericType.Identifier.Text == AutoLayoutViewModelBase)
                    {
                        var templateType = genericType.TypeArgumentList.Arguments[0] as IdentifierNameSyntax;
                        foundModelClasses.Add((classDeclaration, templateType));
                    }
                }
            }
        }
    }
}
