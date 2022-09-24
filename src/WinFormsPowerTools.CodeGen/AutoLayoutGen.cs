using Microsoft.CodeAnalysis;
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


                        // Generating the Property based of the attributed backing field the User provided.
                        GenerateNotifyChangedProperty(
                            cacheTypeName,
                            fieldAttributeTuple.field.Type.ToDisplayString(), 
                            viewModelClass,
                            viewModelCachingClass,
                            mappingAttribute.PropertyName!,
                            fieldAttributeTuple.field.Name);

                        // Generated the correlating Property for the Display value (which leads to a Label), 
                        // if the user provided a Display value through the MappingAttribute.
                        if (!string.IsNullOrEmpty(mappingAttribute.DisplayName))
                        {
                            GenerateNotifyChangedProperty(
                                cacheTypeName,
                                "string",
                                viewModelClass,
                                viewModelCachingClass,
                                mappingAttribute.PropertyName! + "Caption",
                                fieldAttributeTuple.field.Name + "Caption",
                                createBackingField: !string.IsNullOrEmpty(mappingAttribute.DisplayName),
                                defaultValueAssignment: mappingAttribute.DisplayName);
                        }

                        // TODO: Adding extension methods for each generated property (see **)
                    }

                    if (formsControllerProperties is not null)
                    {
                        // **Adding extension methods for each existing property:
                        foreach (IPropertySymbol symbol in formsControllerProperties)
                        {
                            extensionClass.AppendLine($"{In2}public static AutoLayoutGrid<{classDeclaration.Identifier.Text}> Add{symbol.Name}Component(this AutoLayoutGrid<{classDeclaration.Identifier.Text}> grid, AutoLayoutComponent<{classDeclaration.Identifier.Text}> child, int row, int column, int rowSpan = 1, int columnSpan = 1)");
                            extensionClass.AppendLine($"{In2}{{");
                            extensionClass.AppendLine($"{In2}    grid.AddComponent(row, column, child, rowSpan, columnSpan);");
                            extensionClass.AppendLine($"{In2}    return grid;");
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

        /// <summary>
        /// Generates the c# source code for INotifyPropertyChanged compatible properties from backing field definition.
        /// </summary>
        /// <param name="cacheTypeName">The type name of the caching support class. This is where we get the cached EventArgs for each properties from.</param>
        /// <param name="propertyTypeName">The type of the property as string.</param>
        /// <param name="sourceCode">A stringbilder containing the class which the property should be added to.</param>
        /// <param name="cachingClassSourceCode">A stringbuilder containing a file class, which the optional EventArgs property for caching purposes should be adding to.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="backingFieldName">The name of the backing field.</param>
        /// <param name="indentString">The indent string.</param>
        /// <param name="createBackingField">true, if a backing field does not exist and should be created.</param>
        /// <param name="defaultValueAssignment">A default value which should be assigned to the property (only if string).</param>
        private static void GenerateNotifyChangedProperty(
            string cacheTypeName,
            string propertyTypeName,
            StringBuilder sourceCode,
            StringBuilder cachingClassSourceCode,
            string propertyName,
            string backingFieldName,
            string indentString = In2,
            bool createBackingField = false,
            string? defaultValueAssignment = null)
        {
            if (createBackingField)
            {
                sourceCode.Append($"{indentString}private {propertyTypeName} {backingFieldName}");
                if (!string.IsNullOrEmpty(defaultValueAssignment))
                {
                    sourceCode.AppendLine($" = \"{defaultValueAssignment}\";");
                }
                else
                {
                    sourceCode.AppendLine();
                }
            }

            sourceCode.AppendLine($"{indentString}public {propertyTypeName} {propertyName}");
            sourceCode.AppendLine($"{indentString}{{");
            sourceCode.AppendLine($"{indentString}    get");
            sourceCode.AppendLine($"{indentString}    {{");
            sourceCode.AppendLine($"{indentString}        return {backingFieldName};");
            sourceCode.AppendLine($"{indentString}    }}");
            sourceCode.AppendLine($"{indentString}    set");
            sourceCode.AppendLine($"{indentString}    {{");
            sourceCode.AppendLine($"{indentString}        if (!object.Equals({backingFieldName}, value))");
            sourceCode.AppendLine($"{indentString}        {{");
            sourceCode.AppendLine($"{indentString}            {backingFieldName} = value;");
            sourceCode.AppendLine($"{indentString}            OnPropertyChanged({cacheTypeName}.GetInstance().{propertyName}PropertyChangedEventArgs);");
            sourceCode.AppendLine($"{indentString}        }}");
            sourceCode.AppendLine($"{indentString}    }}");
            sourceCode.AppendLine($"{indentString}}}");
            sourceCode.AppendLine();

            string eventPropertyBackingField = $"_{GetPropertyNameFromFieldName(backingFieldName, true)}PropertyChangedEventArgs";
            string eventPropertyName = $"{GetPropertyNameFromFieldName(backingFieldName)}PropertyChangedEventArgs";

            cachingClassSourceCode.AppendLine($"{In2}// Backing field for {eventPropertyName} property:");
            cachingClassSourceCode.AppendLine($"{In2}private PropertyChangedEventArgs {eventPropertyBackingField};");
            cachingClassSourceCode.AppendLine();
            cachingClassSourceCode.AppendLine($"{In2}// Actual {eventPropertyName} property:");
            cachingClassSourceCode.AppendLine($"{In2}public PropertyChangedEventArgs {eventPropertyName}");
            cachingClassSourceCode.AppendLine($"{In2}{{");
            cachingClassSourceCode.AppendLine($"{In2}    get");
            cachingClassSourceCode.AppendLine($"{In2}    {{");
            cachingClassSourceCode.AppendLine($"{In2}        if ({eventPropertyBackingField} is null)");
            cachingClassSourceCode.AppendLine($"{In2}        {{");
            cachingClassSourceCode.AppendLine($"{In2}            {eventPropertyBackingField} = new PropertyChangedEventArgs(\"{propertyName}\");");
            cachingClassSourceCode.AppendLine($"{In2}        }}");
            cachingClassSourceCode.AppendLine($"{In2}        return {eventPropertyBackingField};");
            cachingClassSourceCode.AppendLine($"{In2}    }}");
            cachingClassSourceCode.AppendLine($"{In2}}}");
            cachingClassSourceCode.AppendLine();
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

            if (fieldAttributeTuple.attributeData.ConstructorArguments.Length > 0)
            {
                int count = 0;
                
                foreach (var constructorArgument in fieldAttributeTuple.attributeData.ConstructorArguments)
                {
                    switch (count++)
                    {
                        case 0:
                            attributeToReturn.TargetHint = (AutoLayoutTarget)constructorArgument.Value!;
                            break;
                        case 1:
                            attributeToReturn.DisplayName = (string)constructorArgument.Value!;
                            break;
                        case 2:
                            attributeToReturn.PropertyName = (string)constructorArgument.Value!;
                            if (string.IsNullOrWhiteSpace(attributeToReturn.PropertyName))
                            {
                                // We need a property name unconditionally.
                                attributeToReturn.PropertyName=GetPropertyNameFromFieldName(fieldAttributeTuple.field.Name!);
                            }
                            break;
                        case 3:
                            attributeToReturn.MapsToModelProperty = (string)constructorArgument.Value!;
                            break;
                        case 4:
                            attributeToReturn.GetAccessorScope = (Scope)constructorArgument.Value!;
                            break;
                        case 5:
                            attributeToReturn.SetAccessorScope = (Scope)constructorArgument.Value!;
                            break;
                    }
                }
                
                return attributeToReturn;
            }

            return new ViewControllerMappingAttribute(
                targetHint: AutoLayoutTarget.Implicit,
                propertyName: GetPropertyNameFromFieldName(fieldAttributeTuple.field.Name)!);
        }

        private static AutoLayoutTarget GetTargetFromType(ITypeSymbol typeSymbol)
        {
            return typeSymbol switch
            {
                // TODO: Combo/ListBoxes.
                { SpecialType: SpecialType.System_Boolean } => AutoLayoutTarget.CheckBox,
                { SpecialType: SpecialType.System_Int32 } => AutoLayoutTarget.IntegerEntry,
                { SpecialType: SpecialType.System_Double } => AutoLayoutTarget.FloatEntry,
                { SpecialType: SpecialType.System_Single } => AutoLayoutTarget.FloatEntry,
                { SpecialType: SpecialType.System_String } => AutoLayoutTarget.TextEntry,
                { SpecialType: SpecialType.System_DateTime } => AutoLayoutTarget.DateEntry,
                { SpecialType: SpecialType.System_Decimal } => AutoLayoutTarget.DecimalEntry,
                { SpecialType: SpecialType.System_Byte } => AutoLayoutTarget.IntegerEntry,
                { SpecialType: SpecialType.System_SByte } => AutoLayoutTarget.IntegerEntry,
                { SpecialType: SpecialType.System_Int16 } => AutoLayoutTarget.IntegerEntry,
                { SpecialType: SpecialType.System_UInt16 } => AutoLayoutTarget.IntegerEntry,
                { SpecialType: SpecialType.System_UInt32 } => AutoLayoutTarget.IntegerEntry,
                { SpecialType: SpecialType.System_Int64 } => AutoLayoutTarget.IntegerEntry,
                { SpecialType: SpecialType.System_UInt64 } => AutoLayoutTarget.IntegerEntry,
                { SpecialType: SpecialType.System_Char } => AutoLayoutTarget.TextEntry,
                _ => AutoLayoutTarget.TextEntry
            };
        }

        private static string? GetPropertyNameFromFieldName(string fieldName, bool lowerFirstChar = false)
        {
            if (string.IsNullOrEmpty(fieldName) || fieldName == "_")
            {
                return $"_{new Guid():N}";
            }

            var propertyName = string.Empty;

            if (fieldName.StartsWith("_"))
            {
                propertyName = (lowerFirstChar
                    ? fieldName.Substring(1, 1).ToLower()
                    : fieldName.Substring(1, 1).ToUpper())
                    + fieldName.Substring(2);
            }
            else
            {
                propertyName = (lowerFirstChar
                    ? fieldName.Substring(0, 1).ToLower()
                    : fieldName.Substring(0, 1).ToUpper())
                    + fieldName.Substring(1);
            }

            return propertyName;
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new AutoLayoutSyntaxReceiver());
        }
    }
}
