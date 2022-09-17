using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WinFormsPowerTools.AutoLayout;

namespace WinFormsPowerTools.CodeGen
{
    [Generator]
    public class AutoLayoutGen : ISourceGenerator
    {
        internal const string AutoLayoutViewControllerBase = nameof(AutoLayoutViewControllerBase);

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

                string ind = "    ";

            try
            {
                foreach (var (classDeclaration, identifier, syntaxTree,fieldDictionary) in identifiersAndClasses)
                {
                    var semanticModel = context.Compilation.GetSemanticModel(syntaxTree);

                    var formsControllerSymbol = (INamedTypeSymbol)semanticModel.GetDeclaredSymbol(classDeclaration)!;
                    var viewControllerNamespace = formsControllerSymbol?.ContainingNamespace;
                    var formsControllerProperties = formsControllerSymbol?.GetMembers().OfType<IPropertySymbol>();

                    var formsControllerFields = formsControllerSymbol?.GetMembers()
                        .OfType<IFieldSymbol>()
                        .SelectMany(field => field.GetAttributes(), (field, attributeData) => (field, attributeData))
                        .Where(fieldAttributeTuple => fieldAttributeTuple.attributeData.AttributeClass.Name == nameof(ViewControllerPropertyAttribute));

                    var formsControllerAttribute = formsControllerSymbol?.GetAttributes()
                        .OfType<AttributeData>()
                        .Where(attribute => attribute.AttributeClass.Name == nameof(ViewControllerAttribute))
                        .FirstOrDefault();

                    INamedTypeSymbol modelType;
                    IEnumerable<IPropertySymbol> modelProperties;

                    if (formsControllerAttribute?.ConstructorArguments.FirstOrDefault() is { } argument)
                    {
                        try
                        {
                            modelType = (INamedTypeSymbol)argument.Value!;
                            modelProperties = modelType?.GetMembers().OfType<IPropertySymbol>()!;
                        }
                        catch (Exception)
                        {
                        }
                    }

                    StringBuilder extensionClass = new();
                    extensionClass.AppendLine($"using WinFormsPowerTools.AutoLayout;");
                    extensionClass.AppendLine($"using System.Collections.Generic;");
                    extensionClass.AppendLine($"using System.Runtime.CompilerServices;");
                    extensionClass.AppendLine($"");
                    extensionClass.AppendLine($"namespace {viewControllerNamespace}");
                    extensionClass.AppendLine($"{{");
                    extensionClass.AppendLine($"public static class {classDeclaration.Identifier.Text}Extensions");
                    extensionClass.AppendLine($"{{");

                    StringBuilder viewModelClass = new();
                    viewModelClass.AppendLine($"using WinFormsPowerTools.AutoLayout;");
                    viewModelClass.AppendLine($"using System.ComponentModel;");
                    viewModelClass.AppendLine($"using System.Runtime.CompilerServices;");
                    viewModelClass.AppendLine($"namespace {viewControllerNamespace}");
                    viewModelClass.AppendLine($"{{");
                    viewModelClass.AppendLine($"public partial class {classDeclaration.Identifier.Text}");
                    viewModelClass.AppendLine($"{{");

                    foreach (var fieldAttributeTuple in formsControllerFields!)
                    {
                        string propertyName;

                        if (fieldAttributeTuple.attributeData.ConstructorArguments.FirstOrDefault() is { } constructorArgument && constructorArgument.Value is not null)
                        {
                            propertyName = constructorArgument.Value.ToString();
                        }
                        else
                        {
                            propertyName = GetPropertyNameFromFieldName(fieldAttributeTuple.field.Name)!;
                        }

                        if (string.IsNullOrEmpty(propertyName))
                            continue;
                        
                        // TODO: Convert to correct NullableFlowState depending on backing field.
                        viewModelClass.AppendLine();
                        viewModelClass.AppendLine($"{ind}{ind}public {fieldAttributeTuple.field.Type.ToDisplayString(NullableFlowState.None)} {propertyName}");
                        viewModelClass.AppendLine($"{ind}{ind}{{");
                        viewModelClass.AppendLine($"{ind}{ind}    get");
                        viewModelClass.AppendLine($"{ind}{ind}    {{");
                        viewModelClass.AppendLine($"{ind}{ind}        return {fieldAttributeTuple.field.Name};");
                        viewModelClass.AppendLine($"{ind}{ind}    }}");
                        viewModelClass.AppendLine($"{ind}{ind}    set");
                        viewModelClass.AppendLine($"{ind}{ind}    {{");
                        viewModelClass.AppendLine($"{ind}{ind}        if (!object.Equals({fieldAttributeTuple.field.Name} ,value))");
                        viewModelClass.AppendLine($"{ind}{ind}        {{");
                        viewModelClass.AppendLine($"{ind}{ind}        {fieldAttributeTuple.field.Name} = value;");
                        viewModelClass.AppendLine($"{ind}{ind}        OnPropertyChanged(__{fieldAttributeTuple.field.Name}PropertyChangedEventArgs);");
                        viewModelClass.AppendLine($"{ind}{ind}        }}");
                        viewModelClass.AppendLine($"{ind}{ind}    }}");
                        viewModelClass.AppendLine($"{ind}{ind}}}");
                        viewModelClass.AppendLine();
                        viewModelClass.AppendLine($"{ind}{ind}[CompilerGenerated] private PropertyChangedEventArgs ___{fieldAttributeTuple.field.Name}PropertyChangedEventArgs;");
                        viewModelClass.AppendLine($"{ind}{ind}[CompilerGenerated] private PropertyChangedEventArgs __{fieldAttributeTuple.field.Name}PropertyChangedEventArgs");
                        viewModelClass.AppendLine($"{ind}{ind}{{");
                        viewModelClass.AppendLine($"{ind}{ind}    get");
                        viewModelClass.AppendLine($"{ind}{ind}    {{");
                        viewModelClass.AppendLine($"{ind}{ind}        if (___{fieldAttributeTuple.field.Name}PropertyChangedEventArgs is null)");
                        viewModelClass.AppendLine($"{ind}{ind}        {{");
                        viewModelClass.AppendLine($"{ind}{ind}            ___{fieldAttributeTuple.field.Name}PropertyChangedEventArgs = new PropertyChangedEventArgs(nameof({propertyName}));");
                        viewModelClass.AppendLine($"{ind}{ind}        }}");
                        viewModelClass.AppendLine($"{ind}{ind}        return ___{fieldAttributeTuple.field.Name}PropertyChangedEventArgs;");
                        viewModelClass.AppendLine($"{ind}{ind}    }}");
                        viewModelClass.AppendLine($"{ind}{ind}}}");
                        viewModelClass.AppendLine();
                    }

                    if (formsControllerProperties is not null)
                    {
                        foreach (IPropertySymbol symbol in formsControllerProperties)
                        {
                            extensionClass.AppendLine($"{ind}public static AutoLayoutComponents<{classDeclaration.Identifier.Text}> Add{symbol.Name}(this AutoLayoutComponents<{classDeclaration.Identifier.Text}> components)");
                            extensionClass.AppendLine($"{ind}{{");
                            extensionClass.AppendLine($"{ind + ind}if (components is null)");
                            extensionClass.AppendLine($"{ind + ind}{{");
                            extensionClass.AppendLine($"{ind + ind + ind}components = new AutoLayoutComponents<{classDeclaration.Identifier.Text}>() {{ Components = new List<AutoLayoutComponent<{classDeclaration.Identifier.Text}>>() }};");
                            extensionClass.AppendLine($"{ind + ind}}}");
                            extensionClass.AppendLine($"{ind + ind}var component = new AutoLayoutComponent<{classDeclaration.Identifier.Text}>(\"{symbol.Name}\");");
                            extensionClass.AppendLine($"{ind + ind}components.Components.Add(component);");
                            extensionClass.AppendLine($"{ind + ind}components.LastComponent = component;");
                            extensionClass.AppendLine($"{ind + ind}return components;");
                            extensionClass.AppendLine($"{ind}}}");
                        }
                    }

                    extensionClass.AppendLine("}");
                    extensionClass.AppendLine("}");
                    viewModelClass.AppendLine("}");
                    viewModelClass.AppendLine("}");

                    var viewModelClassString = viewModelClass.ToString();
                    var extensionClassString = extensionClass.ToString();

                    if (Debugger.IsAttached)
                    {
                        Debugger.Break();
                    }

                    if (extensionClass is not null)
                    {
                        context.AddSource(hintName: $"{classDeclaration.Identifier.Text}Extensions.g.cs", extensionClass.ToString());
                    }
                    if (viewModelClass is not null)
                    {
                        context.AddSource(hintName: $"{classDeclaration.Identifier.Text}.g.cs", viewModelClass.ToString());
                    }
                }
            }
            catch(Exception codeGenEx)
            {
                StringBuilder extensionClass = new();
                extensionClass.AppendLine($"class CodeGenExceptionPrinter");
                extensionClass.AppendLine("{");
                extensionClass.AppendLine($"   private string errorMessage=@\"{codeGenEx.Message}");
                extensionClass.AppendLine($"");
                extensionClass.AppendLine($"{codeGenEx.StackTrace.Replace("\"","\\\"")}");
                extensionClass.AppendLine($"\";");
                extensionClass.AppendLine("}");
                context.AddSource(hintName: $"CodeGenExceptionPrinter.g.cs", extensionClass.ToString());
            }
        }

        private string? GetPropertyNameFromFieldName(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName) || fieldName == "_")
            {
                return null;
            }

            var propertyName = string.Empty;

            if (fieldName.StartsWith("_"))
            {
                propertyName = fieldName.Substring(1, 1).ToUpper() + fieldName.Substring(2);
            }
            else
            {
                propertyName = fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
            }

            return propertyName;
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new AutoLayoutSyntaxReceiver());
        }
    }

    internal class AutoLayoutSyntaxReceiver : ISyntaxReceiver
    {
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
                    .FirstOrDefault(attribute => ((IdentifierNameSyntax)attribute.Name).Identifier.ValueText == "ViewController");

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
                                .FirstOrDefault(attribute => ((IdentifierNameSyntax)attribute.Name).Identifier.ValueText == "ViewControllerProperty");

                            if (fieldAttribute is not null)
                            {
                                fieldDictionary.Add(fieldDeclaration, fieldAttribute);
                            }
                        }
                    }
                }
            }
        }
    }
}
