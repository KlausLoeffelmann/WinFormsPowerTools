using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WinFormsPowerTools.AutoLayout;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutFoo
    {
    }
}

namespace WinFormsPowerTools.CodeGen
{
    [Generator]
    public class AutoLayoutGen : ISourceGenerator
    {
        internal const string In0 = "";
        internal const string In1 = "    ";
        internal const string In2 = "        ";
        internal const string In3 = "            ";

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

            try
            {
                foreach (var (classDeclaration, identifier, syntaxTree, fieldDictionary) in identifiersAndClasses)
                {
                    var semanticModel = context.Compilation.GetSemanticModel(syntaxTree);

                    var viewControllerSymbol = (INamedTypeSymbol)semanticModel.GetDeclaredSymbol(classDeclaration)!;
                    var viewControllerNamespace = viewControllerSymbol?.ContainingNamespace;
                    var formsControllerProperties = viewControllerSymbol?.GetMembers().OfType<IPropertySymbol>();

                    var formsControllerFields = viewControllerSymbol?.GetMembers()
                        .OfType<IFieldSymbol>()
                        .SelectMany(field => field.GetAttributes(), (field, attributeData) => (field, attributeData))
                        .Where(fieldAttributeTuple => fieldAttributeTuple.attributeData.AttributeClass.Name == nameof(ViewControllerMappingAttribute));

                    var viewControllerAttribute = viewControllerSymbol?.GetAttributes()
                        .OfType<AttributeData>()
                        .Where(attribute => attribute.AttributeClass.Name == nameof(ViewControllerAttribute))
                        .FirstOrDefault();

                    INamedTypeSymbol modelType;
                    IEnumerable<IPropertySymbol> modelProperties;

                    string cacheVarName = $"__{ classDeclaration.Identifier.Text}";

                    if (viewControllerAttribute?.ConstructorArguments.FirstOrDefault() is { } argument)
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

                    string cacheTypeName = $"{classDeclaration.Identifier.Text}_Cache";
                    
                    StringBuilder viewModelCachingClass = new();
                    viewModelCachingClass.AppendLine($"{In1}file class {cacheTypeName}");
                    viewModelCachingClass.AppendLine($"{In1}{{");
                    viewModelCachingClass.AppendLine();
                    viewModelCachingClass.AppendLine($"{In2}private static {cacheTypeName} _instance;");
                    viewModelCachingClass.AppendLine();
                    viewModelCachingClass.AppendLine($"{In2}public static {cacheTypeName} GetInstance()");
                    viewModelCachingClass.AppendLine($"{In2}{{");
                    viewModelCachingClass.AppendLine($"{In2}    return _instance ??= new {cacheTypeName}();");
                    viewModelCachingClass.AppendLine($"{In2}}}");
                    
                    StringBuilder extensionClass = new();
                    extensionClass.AppendLine($"using WinFormsPowerTools.AutoLayout;");
                    extensionClass.AppendLine($"using System.Collections.Generic;");
                    extensionClass.AppendLine($"using System.Runtime.CompilerServices;");
                    extensionClass.AppendLine();
                    extensionClass.AppendLine($"namespace {viewControllerNamespace}");
                    extensionClass.AppendLine($"{{");
                    extensionClass.AppendLine($"    public static class {classDeclaration.Identifier.Text}Extensions");
                    extensionClass.AppendLine($"    {{");

                    StringBuilder viewModelClass = new();
                    viewModelClass.AppendLine($"using WinFormsPowerTools.StandardLib.ViewControllerBaseClasses;");
                    viewModelClass.AppendLine($"using WinFormsPowerTools.AutoLayout;");
                    viewModelClass.AppendLine($"using System.ComponentModel;");
                    viewModelClass.AppendLine($"using System.Runtime.CompilerServices;");
                    viewModelClass.AppendLine();
                    viewModelClass.AppendLine($"namespace {viewControllerNamespace}");
                    viewModelClass.AppendLine($"{{");
                    viewModelClass.AppendLine($"{In1}public partial class {classDeclaration.Identifier.Text} : INotifyPropertyChangedDocumentFactory<{classDeclaration.Identifier.Text}>");
                    viewModelClass.AppendLine($"{In1}{{");

                    foreach (var fieldAttributeTuple in formsControllerFields!)
                    {
                        var mappingAttribute = GetMappingAttribute(fieldAttributeTuple);

                        string eventPropertyBackingField = $"_{fieldAttributeTuple.field.Name}";
                        string eventPropertyName = $"{fieldAttributeTuple.field.Name}";

                        viewModelClass.AppendLine($"{In2}public {fieldAttributeTuple.field.Type.ToDisplayString(NullableFlowState.None)} {mappingAttribute.PropertyName}");
                        viewModelClass.AppendLine($"{In2}{{");
                        viewModelClass.AppendLine($"{In2}    get");
                        viewModelClass.AppendLine($"{In2}    {{");
                        viewModelClass.AppendLine($"{In2}        return {fieldAttributeTuple.field.Name};");
                        viewModelClass.AppendLine($"{In2}    }}");
                        viewModelClass.AppendLine($"{In2}    set");
                        viewModelClass.AppendLine($"{In2}    {{");
                        viewModelClass.AppendLine($"{In2}        if (!object.Equals({fieldAttributeTuple.field.Name}, value))");
                        viewModelClass.AppendLine($"{In2}        {{");
                        viewModelClass.AppendLine($"{In2}            {fieldAttributeTuple.field.Name} = value;");
                        viewModelClass.AppendLine($"{In2}            OnPropertyChanged({cacheTypeName}.GetInstance().{fieldAttributeTuple.field.Name}PropertyChangedEventArgs);");
                        viewModelClass.AppendLine($"{In2}        }}");
                        viewModelClass.AppendLine($"{In2}    }}");
                        viewModelClass.AppendLine($"{In2}}}");
                        viewModelClass.AppendLine();
                        viewModelCachingClass.AppendLine($"{In2}// Backing field for {eventPropertyName}PropertyChangedEventArgs property:");
                        viewModelCachingClass.AppendLine($"{In2}private PropertyChangedEventArgs {eventPropertyBackingField}PropertyChangedEventArgs;");
                        viewModelCachingClass.AppendLine();
                        viewModelCachingClass.AppendLine($"{In2}// Actual {eventPropertyName}PropertyChangedEventArgs property:");
                        viewModelCachingClass.AppendLine($"{In2}public PropertyChangedEventArgs {eventPropertyName}PropertyChangedEventArgs");
                        viewModelCachingClass.AppendLine($"{In2}{{");
                        viewModelCachingClass.AppendLine($"{In2}    get");
                        viewModelCachingClass.AppendLine($"{In2}    {{");
                        viewModelCachingClass.AppendLine($"{In2}        if ({eventPropertyBackingField}PropertyChangedEventArgs is null)");
                        viewModelCachingClass.AppendLine($"{In2}        {{");
                        viewModelCachingClass.AppendLine($"{In2}            {eventPropertyBackingField}PropertyChangedEventArgs = new PropertyChangedEventArgs(\"{mappingAttribute.PropertyName}\");");
                        viewModelCachingClass.AppendLine($"{In2}        }}");
                        viewModelCachingClass.AppendLine($"{In2}        return {eventPropertyBackingField}PropertyChangedEventArgs;");
                        viewModelCachingClass.AppendLine($"{In2}    }}");
                        viewModelCachingClass.AppendLine($"{In2}}}");
                        viewModelCachingClass.AppendLine();

                        // TODO: Adding extension methods for each generated property (see **)
                    }

                    if (formsControllerProperties is not null)
                    {
                        
                        // **Adding extension methods for each existing property:
                        foreach (IPropertySymbol symbol in formsControllerProperties)
                        {
                            extensionClass.AppendLine($"{In2}public static AutoLayoutComponents<{classDeclaration.Identifier.Text}> Add{symbol.Name}Component(this AutoLayoutComponents<{classDeclaration.Identifier.Text}> components)");
                            extensionClass.AppendLine($"{In2}{{");
                            extensionClass.AppendLine($"{In2}    if (components is null)");
                            extensionClass.AppendLine($"{In2}    {{");
                            extensionClass.AppendLine($"{In2}        components = new AutoLayoutComponents<{classDeclaration.Identifier.Text}>() {{ Components = new List<AutoLayoutComponent<{classDeclaration.Identifier.Text}>>() }};");
                            extensionClass.AppendLine($"{In2}    }}");
                            extensionClass.AppendLine();
                            extensionClass.AppendLine($"{In2}    var component = new AutoLayoutComponent<{classDeclaration.Identifier.Text}>(\"{symbol.Name}\");");
                            extensionClass.AppendLine($"{In2}    components.Components.Add(component);");
                            extensionClass.AppendLine($"{In2}    components.LastComponent = component;");
                            extensionClass.AppendLine();
                            extensionClass.AppendLine($"{In2}    return components;");
                            extensionClass.AppendLine($"{In2}}}");
                        }
                    }

                    extensionClass.AppendLine($"{In1}}}");
                    extensionClass.AppendLine($"}}");

                    viewModelClass.AppendLine($"{In1}}}");
                    viewModelClass.Append(viewModelCachingClass);
                    viewModelClass.AppendLine($"{In1}}}");
                    viewModelClass.AppendLine($"}}");

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
            catch (Exception codeGenEx)
            {
                StringBuilder extensionClass = new();
                extensionClass.AppendLine($"class CodeGenExceptionPrinter");
                extensionClass.AppendLine("{");
                extensionClass.AppendLine($"   private string errorMessage=@\"{codeGenEx.Message}");
                extensionClass.AppendLine($"");
                extensionClass.AppendLine($"{codeGenEx.StackTrace.Replace("\"", "\\\"")}");
                extensionClass.AppendLine($"\";");
                extensionClass.AppendLine("}");
                context.AddSource(hintName: $"CodeGenExceptionPrinter.g.cs", extensionClass.ToString());
            }
        }

        private ViewControllerMappingAttribute GetMappingAttribute((IFieldSymbol field, AttributeData attributeData) fieldAttributeTuple)
        {
            if (Debugger.IsAttached)
                Debugger.Break();

            if (fieldAttributeTuple.attributeData is null)
            {
                return new ViewControllerMappingAttribute(
                    AutoLayoutTarget.Implicit,
                    GetPropertyNameFromFieldName(fieldAttributeTuple.field.Name)!);
            }

            var attributeToReturn = new ViewControllerMappingAttribute();

            if (fieldAttributeTuple.attributeData.NamedArguments.Length>0)
            {
                foreach (var namedArgument in fieldAttributeTuple.attributeData.NamedArguments) 
                {
                    switch (namedArgument.Key)
                    {
                        case nameof(ViewControllerMappingAttribute.TargetHint):
                            attributeToReturn.TargetHint = (AutoLayoutTarget)namedArgument.Value.Value!;
                            break;
                        case nameof(ViewControllerMappingAttribute.PropertyName):
                            attributeToReturn.PropertyName = (string)namedArgument.Value.Value!;
                            break;
                        case nameof(ViewControllerMappingAttribute.DisplayName):
                            attributeToReturn.DisplayName = (string)namedArgument.Value.Value!;
                            break;
                        case nameof(ViewControllerMappingAttribute.MapsToModelProperty):
                            attributeToReturn.MapsToModelProperty = (string)namedArgument.Value.Value!;
                            break;
                        default:
                            break;
                    }
                }

                return attributeToReturn;
            }

            return new ViewControllerMappingAttribute(
                targetHint: AutoLayoutTarget.Implicit,
                propertyName: GetPropertyNameFromFieldName(fieldAttributeTuple.field.Name)!);
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
}
