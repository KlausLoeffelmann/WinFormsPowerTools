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
        internal const string In0 = "";
        internal const string In1 = "    ";
        internal const string In2 = "        ";
        internal const string In3 = "            ";

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxContextReceiver is not AutoLayoutSyntaxReceiver syntaxReceiver)
            {
                throw new InvalidOperationException("Got the wrong syntax receiver type.");
            }

            if (syntaxReceiver.viewModelClassesInfo.Count == 0)
            {
                return;
            }

            var identifiersAndClasses = syntaxReceiver.viewModelClassesInfo;

            try
            {
                foreach (var viewModelItem in identifiersAndClasses)
                {
                    var semanticModel = context.Compilation.GetSemanticModel(viewModelItem.SyntaxTree);

                    var viewControllerSymbol = (INamedTypeSymbol)semanticModel.GetDeclaredSymbol(viewModelItem.ClassDeclaration)!;
                    var viewControllerNamespace = viewControllerSymbol?.ContainingNamespace;
                    var formsControllerProperties = viewControllerSymbol?.GetMembers().OfType<IPropertySymbol>();

                    var formsControllerFields = viewModelItem.FieldDefinitions;

                    var viewControllerAttribute = viewControllerSymbol?.GetAttributes()
                        .OfType<AttributeData>()
                        .Where(attribute => attribute.AttributeClass.Name == nameof(ViewControllerAttribute))
                        .FirstOrDefault();

                    INamedTypeSymbol modelType;
                    IEnumerable<IPropertySymbol> modelProperties;

                    string cacheVarName = $"__{viewModelItem.ClassDeclaration.Identifier.Text}";

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

                    string cacheTypeName = $"{viewModelItem.ClassDeclaration.Identifier.Text}_Cache";
                    
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
                    extensionClass.AppendLine($"using System;");
                    extensionClass.AppendLine($"using System.Collections.Generic;");
                    extensionClass.AppendLine($"using System.Runtime.CompilerServices;");
                    extensionClass.AppendLine($"using WinFormsPowerTools.AutoLayout;");
                    extensionClass.AppendLine();
                    extensionClass.AppendLine($"using static {viewControllerNamespace}.{viewModelItem.ClassDeclaration.Identifier.Text}BindingExtensions;");
                    extensionClass.AppendLine();
                    extensionClass.AppendLine($"namespace {viewControllerNamespace}");
                    extensionClass.AppendLine($"{{");
                    extensionClass.AppendLine($"    public static class {viewModelItem.ClassDeclaration.Identifier.Text}Extensions");
                    extensionClass.AppendLine($"    {{");

                    StringBuilder viewModelClass = new();
                    viewModelClass.AppendLine($"using System;");
                    viewModelClass.AppendLine($"using System.ComponentModel;");
                    viewModelClass.AppendLine($"using System.Runtime.CompilerServices;");
                    viewModelClass.AppendLine($"using WinFormsPowerTools.AutoLayout;");
                    viewModelClass.AppendLine($"using WinFormsPowerTools.StandardLib.ViewControllerBaseClasses;");
                    viewModelClass.AppendLine();
                    viewModelClass.AppendLine($"namespace {viewControllerNamespace}");
                    viewModelClass.AppendLine($"{{");
                    viewModelClass.AppendLine($"{In1}public partial class {viewModelItem.ClassDeclaration.Identifier.Text}");
                    viewModelClass.AppendLine($"{In1}{{");

                    StringBuilder bindingExtensionClass = new();
                    bindingExtensionClass.AppendLine($"using System;");
                    bindingExtensionClass.AppendLine($"using WinFormsPowerTools.AutoLayout;");
                    bindingExtensionClass.AppendLine();
                    bindingExtensionClass.AppendLine($"namespace {viewControllerNamespace}");
                    bindingExtensionClass.AppendLine($"{{");
                    bindingExtensionClass.AppendLine($"    public static class {viewModelItem.ClassDeclaration.Identifier.Text}BindingExtensions");
                    bindingExtensionClass.AppendLine($"    {{");

                    StringBuilder viewModelRelayCommandInitializer = new();
                    viewModelRelayCommandInitializer.AppendLine($"{In2}private void InitializeRelayCommands()");
                    viewModelRelayCommandInitializer.AppendLine($"{In2}{{");

                    foreach (var commandMethodInfo in viewModelItem.CommandMethodDefinitions)
                    {
                        if (Debugger.IsAttached)
                            Debugger.Break();
                        
                        var mappingAttribute = GetCommandMappingAttribute(commandMethodInfo.Key, commandMethodInfo.Value.CommandAttribute);
                        var displayName = mappingAttribute.DisplayName;
                        
                        // We only allow MenuItems and Buttons to be bound to commands.
                        mappingAttribute.TargetHint = mappingAttribute.TargetHint switch
                        {
                            AutoLayoutTarget.MenuItem => AutoLayoutTarget.MenuItem,
                            _ => AutoLayoutTarget.Button
                        };

                        // No Command backing field was defined, we have to create it.
                        if (commandMethodInfo.Value.FieldSymbol is null)
                        {
                            GenerateNotifyChangedProperty(
                                cacheTypeName,
                                context.Compilation.GetTypeByMetadataName("System.Windows.Input.ICommand")!,
                                viewModelItem.ClassDeclaration,
                                viewModelClass,
                                viewModelCachingClass,
                                extensionClass,
                                bindingExtensionClass,
                                commandMethodInfo.Value.BaseLineName,
                                GetFieldNameFromPropertyName(commandMethodInfo.Value.BaseLineName),
                                commandMethods: (commandMethodInfo.Value.ExecuteMethodSymbol!, commandMethodInfo.Value.CanExecuteMethodSymbol),
                                viewModelRelayCommandInitializerSourceCode: viewModelRelayCommandInitializer,
                                createBackingField: true,
                                autoLayoutTarget: mappingAttribute.TargetHint,
                                forcedTextPropertyContentInExtensionMethod: displayName);

                            if (!string.IsNullOrEmpty(displayName))
                            {
                                GenerateNotifyChangedProperty(
                                    cacheTypeName,
                                    context.Compilation.GetTypeByMetadataName("System.String")!,
                                    viewModelItem.ClassDeclaration,
                                    viewModelClass,
                                    viewModelCachingClass,
                                    extensionClass,
                                    bindingExtensionClass,
                                    commandMethodInfo.Value.BaseLineName + "Caption",
                                    GetFieldNameFromPropertyName(commandMethodInfo.Value.BaseLineName) + "Caption",
                                    createBackingField: true,
                                    defaultValueAssignment: displayName,
                                    autoLayoutTarget: AutoLayoutTarget.None);
                            }
                        }
                    }

                    foreach (var fieldAttributeTuple in formsControllerFields!)
                    {
                        var mappingAttribute = GetPropertyMappingAttribute(fieldAttributeTuple.Key.Name, fieldAttributeTuple.Value);

                        // Generating the Property based of the attributed backing field the User provided.
                        GenerateNotifyChangedProperty(
                            cacheTypeName,
                            fieldAttributeTuple.Key.Type,
                            viewModelItem.ClassDeclaration,
                            viewModelClass,
                            viewModelCachingClass,
                            extensionClass,
                            bindingExtensionClass,
                            mappingAttribute.PropertyName!,
                            fieldAttributeTuple.Key.Name,
                            autoLayoutTarget: mappingAttribute.TargetHint);

                        // Generated the correlating Property for the Display value (which leads to a Label), 
                        // if the user provided a Display value through the MappingAttribute.
                        if (!string.IsNullOrEmpty(mappingAttribute.DisplayName))
                        {
                            GenerateNotifyChangedProperty(
                                cacheTypeName,
                                context.Compilation.GetTypeByMetadataName("System.String")!,
                                viewModelItem.ClassDeclaration,
                                viewModelClass,
                                viewModelCachingClass,
                                extensionClass,
                                bindingExtensionClass,
                                mappingAttribute.PropertyName! + "Caption",
                                fieldAttributeTuple.Key.Name + "Caption",
                                createBackingField: !string.IsNullOrEmpty(mappingAttribute.DisplayName),
                                defaultValueAssignment: mappingAttribute.DisplayName,
                                autoLayoutTarget: AutoLayoutTarget.Label);
                        }

                        // TODO: Adding extension methods for each generated property (see **)
                    }

                    extensionClass.AppendLine($"{In1}}}");
                    extensionClass.AppendLine($"}}");

                    bindingExtensionClass.AppendLine($"{In1}}}");
                    bindingExtensionClass.AppendLine($"}}");

                    viewModelRelayCommandInitializer.AppendLine($"{In2}}}");
                    viewModelRelayCommandInitializer.AppendLine();

                    viewModelClass.AppendLine();
                    viewModelClass.Append(viewModelRelayCommandInitializer);
                    viewModelClass.AppendLine($"{In1}}}");
                    viewModelClass.AppendLine();
                    viewModelClass.Append(viewModelCachingClass);
                    viewModelClass.AppendLine($"{In1}}}");
                    viewModelClass.AppendLine($"}}");

                    var viewModelClassString = viewModelClass.ToString();
                    var extensionClassString = extensionClass.ToString();

                    if (extensionClass is not null)
                    {
                        context.AddSource(hintName: $"{viewModelItem.ClassDeclaration.Identifier.Text}Extensions.g.cs", extensionClass.ToString());
                    }
                    if (viewModelClass is not null)
                    {
                        context.AddSource(hintName: $"{viewModelItem.ClassDeclaration.Identifier.Text}.g.cs", viewModelClass.ToString());
                    }
                    if (bindingExtensionClass is not null)
                    {
                        context.AddSource(hintName: $"{viewModelItem.ClassDeclaration.Identifier.Text}BindingExtensions.g.cs", bindingExtensionClass.ToString());
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
            ITypeSymbol propertyType,
            ClassDeclarationSyntax viewModelClass,
            StringBuilder sourceCode,
            StringBuilder cachingClassSourceCode,
            StringBuilder extensionClassSourceCode,
            StringBuilder bindingExtensionClassSourceCode,
            string propertyName,
            string backingFieldName,
            StringBuilder? viewModelRelayCommandInitializerSourceCode=default,
            (IMethodSymbol executeCommand, IMethodSymbol? canExecuteCommand)? commandMethods=default,
            string indentString = In2,
            bool createBackingField = false,
            string? defaultValueAssignment = null,
            AutoLayoutTarget autoLayoutTarget = AutoLayoutTarget.Implicit,
            string? forcedTextPropertyContentInExtensionMethod=default)
        {
            if (createBackingField)
            {
                sourceCode.Append($"{indentString}private {propertyType.ToDisplayString()} {backingFieldName}");
                if (!string.IsNullOrEmpty(defaultValueAssignment))
                {
                    sourceCode.AppendLine($" = \"{defaultValueAssignment}\";");
                }
                else
                {
                    sourceCode.AppendLine(";");
                }

                if (viewModelRelayCommandInitializerSourceCode != null)
                {
                    viewModelRelayCommandInitializerSourceCode.Append($"{indentString}    {backingFieldName} = new RelayCommand({commandMethods!.Value.executeCommand.Name}");
                    if (commandMethods!.Value.canExecuteCommand != null)
                    {
                        viewModelRelayCommandInitializerSourceCode.AppendLine($", {commandMethods!.Value.canExecuteCommand.Name});");
                    }
                    else
                    {
                        viewModelRelayCommandInitializerSourceCode.AppendLine($");");
                    }
                }
            }

            sourceCode.AppendLine($"{indentString}public {propertyType.ToDisplayString()} {propertyName}");
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

            string vmTypeName = viewModelClass.Identifier.Text;

            bindingExtensionClassSourceCode.AppendLine($"{In2}public static AutoLayoutBinding To{propertyName}(string componentPropertyName)");
            bindingExtensionClassSourceCode.AppendLine($"{In2}{{");
            bindingExtensionClassSourceCode.AppendLine($"{In2}    return new(componentPropertyName, nameof({vmTypeName}.{propertyName}));");
            bindingExtensionClassSourceCode.AppendLine($"{In2}}}");
            bindingExtensionClassSourceCode.AppendLine();

            if (autoLayoutTarget==AutoLayoutTarget.Implicit)
            {
                autoLayoutTarget = GetTargetFromType(propertyType);
            }

            if (autoLayoutTarget == AutoLayoutTarget.Label)
            {
                extensionClassSourceCode.AppendLine($"{In2}public static AutoLayoutGrid<{vmTypeName}> Add{propertyName}Label(");
                extensionClassSourceCode.AppendLine($"{In2}    this AutoLayoutGrid<{vmTypeName}> grid,");
                extensionClassSourceCode.AppendLine($"{In2}    int row, int column, int rowSpan = 1, int columnSpan = 1)");
                extensionClassSourceCode.AppendLine($"{In2}    {{");

                if (defaultValueAssignment is not null)
                {
                    extensionClassSourceCode.AppendLine($"{In2}        AutoLayoutLabel<{vmTypeName}> label = new(name: \"{propertyName}Label\", text: \"{defaultValueAssignment}\", bindings: To{propertyName}(nameof(AutoLayoutTextEntry<{vmTypeName}>.Text)));");
                }
                else
                {
                    extensionClassSourceCode.AppendLine($"{In2}        AutoLayoutLabel<{vmTypeName}> label = new(name: \"{propertyName}Label\", bindings: To{propertyName}(nameof(AutoLayoutTextEntry<{vmTypeName}>.Text)));");
                }

                extensionClassSourceCode.AppendLine($"{In2}        grid.AddComponent(label, row, column, rowSpan, columnSpan);");
                extensionClassSourceCode.AppendLine($"");
                extensionClassSourceCode.AppendLine($"{In2}        return grid;");
                extensionClassSourceCode.AppendLine($"{In2}    }}");
                extensionClassSourceCode.AppendLine($"");
            }

            if (autoLayoutTarget == AutoLayoutTarget.TextEntry)
            {
                extensionClassSourceCode.AppendLine($"{In2}public static AutoLayoutGrid<{vmTypeName}> Add{propertyName}TextEntry(");
                extensionClassSourceCode.AppendLine($"{In2}    this AutoLayoutGrid<{vmTypeName}> grid,");
                extensionClassSourceCode.AppendLine($"{In2}    int row, int column, int rowSpan = 1, int columnSpan = 1)");
                extensionClassSourceCode.AppendLine($"{In2}    {{");
                extensionClassSourceCode.AppendLine($"{In2}        AutoLayoutTextEntry<{vmTypeName}> textEntry = new(name: \"{propertyName}TextEntry\", bindings: To{propertyName}(nameof(AutoLayoutTextEntry<{vmTypeName}>.Text)));");
                extensionClassSourceCode.AppendLine($"{In2}        grid.AddComponent(textEntry, row, column, rowSpan, columnSpan);");
                extensionClassSourceCode.AppendLine($"");
                extensionClassSourceCode.AppendLine($"{In2}        return grid;");
                extensionClassSourceCode.AppendLine($"{In2}    }}");
                extensionClassSourceCode.AppendLine($"");
            }

            if (autoLayoutTarget == AutoLayoutTarget.Button)
            {
                if (string.IsNullOrWhiteSpace(forcedTextPropertyContentInExtensionMethod))
                {
                    forcedTextPropertyContentInExtensionMethod = "Not defined.";
                }

                extensionClassSourceCode.AppendLine($"{In2}public static AutoLayoutGrid<{vmTypeName}> Add{propertyName}Button(");
                extensionClassSourceCode.AppendLine($"{In2}    this AutoLayoutGrid<{vmTypeName}> grid,");
                extensionClassSourceCode.AppendLine($"{In2}    int row, int column, int rowSpan = 1, int columnSpan = 1)");
                extensionClassSourceCode.AppendLine($"{In2}    {{");
                extensionClassSourceCode.AppendLine($"{In2}        AutoLayoutButton<{vmTypeName}> button = new(name: \"{propertyName}Button\", text: \"{forcedTextPropertyContentInExtensionMethod}\", null,");
                extensionClassSourceCode.AppendLine($"{In2}            To{propertyName}(nameof(AutoLayoutButton<{vmTypeName}>.Command)),");
                extensionClassSourceCode.AppendLine($"{In2}            To{propertyName}Caption(nameof(AutoLayoutButton<{vmTypeName}>.Text)));");
                extensionClassSourceCode.AppendLine($"");
                extensionClassSourceCode.AppendLine($"{In2}        grid.AddComponent(button, row, column, rowSpan, columnSpan);");
                extensionClassSourceCode.AppendLine($"");
                extensionClassSourceCode.AppendLine($"{In2}        return grid;");
                extensionClassSourceCode.AppendLine($"{In2}    }}");
                extensionClassSourceCode.AppendLine($"");
            }

            if (autoLayoutTarget == AutoLayoutTarget.MenuItem)
            {
                if (string.IsNullOrWhiteSpace(forcedTextPropertyContentInExtensionMethod))
                {
                    forcedTextPropertyContentInExtensionMethod = "Not defined.";
                }

                extensionClassSourceCode.AppendLine($"{In2}public static AutoLayoutMenu<{vmTypeName}> Add{propertyName}MenuItem(");
                extensionClassSourceCode.AppendLine($"{In2}    this AutoLayoutMenu<{vmTypeName}> menu)");
                extensionClassSourceCode.AppendLine($"{In2}    {{");
                extensionClassSourceCode.AppendLine($"{In2}        AutoLayoutMenuItem<{vmTypeName}> menuItem = new(name: \"{propertyName}MenuItem\", text: \"{forcedTextPropertyContentInExtensionMethod}\", null, true, false, ");
                extensionClassSourceCode.AppendLine($"{In2}            To{propertyName}(nameof(AutoLayoutMenuItem<{vmTypeName}>.Command)),");
                extensionClassSourceCode.AppendLine($"{In2}            To{propertyName}Caption(nameof(AutoLayoutMenuItem<{vmTypeName}>.Text)));");
                extensionClassSourceCode.AppendLine($"");
                extensionClassSourceCode.AppendLine($"{In2}        menu.Add(menuItem);");
                extensionClassSourceCode.AppendLine($"");
                extensionClassSourceCode.AppendLine($"{In2}        return menu;");
                extensionClassSourceCode.AppendLine($"{In2}    }}");
                extensionClassSourceCode.AppendLine($"");

                extensionClassSourceCode.AppendLine($"{In2}public static AutoLayoutMenuItem<{vmTypeName}> Add{propertyName}MenuItem(");
                extensionClassSourceCode.AppendLine($"{In2}    this AutoLayoutMenuItem<{vmTypeName}> menuItem)");
                extensionClassSourceCode.AppendLine($"{In2}    {{");
                extensionClassSourceCode.AppendLine($"{In2}        AutoLayoutMenuItem<{vmTypeName}> subMenuItem = new(name: \"{propertyName}MenuItem\", text: \"{forcedTextPropertyContentInExtensionMethod}\", null, true, false,");
                extensionClassSourceCode.AppendLine($"{In2}            To{propertyName}(nameof(AutoLayoutMenuItem<{vmTypeName}>.Command)),");
                extensionClassSourceCode.AppendLine($"{In2}            To{propertyName}Caption(nameof(AutoLayoutMenuItem<{vmTypeName}>.Text)));");
                extensionClassSourceCode.AppendLine($"");
                extensionClassSourceCode.AppendLine($"{In2}        menuItem.Add(subMenuItem);");
                extensionClassSourceCode.AppendLine($"");
                extensionClassSourceCode.AppendLine($"{In2}        return menuItem;");
                extensionClassSourceCode.AppendLine($"{In2}    }}");
                extensionClassSourceCode.AppendLine($"");
            }
        }

        private PropertyMappingAttribute GetPropertyMappingAttribute(string fieldName, AttributeData attributeData)
        {
            if (Debugger.IsAttached)
                Debugger.Break();

            if (attributeData is null)
            {
                return new PropertyMappingAttribute(
                    AutoLayoutTarget.Implicit,
                    fieldName);
            }

            var attributeToReturn = new PropertyMappingAttribute();

            if (attributeData.NamedArguments.Length > 0)
            {
                foreach (var namedArgument in attributeData.NamedArguments)
                {
                    switch (namedArgument.Key)
                    {
                        case nameof(PropertyMappingAttribute.TargetHint):
                            attributeToReturn.TargetHint = (AutoLayoutTarget)namedArgument.Value.Value!;
                            break;
                        case nameof(PropertyMappingAttribute.PropertyName):
                            attributeToReturn.PropertyName = (string)namedArgument.Value.Value!;
                            break;
                        case nameof(PropertyMappingAttribute.DisplayName):
                            attributeToReturn.DisplayName = (string)namedArgument.Value.Value!;
                            break;
                        case nameof(PropertyMappingAttribute.MapsToModelProperty):
                            attributeToReturn.MapsToModelProperty = (string)namedArgument.Value.Value!;
                            break;
                        default:
                            break;
                    }
                }

                return attributeToReturn;
            }

            if (attributeData.ConstructorArguments.Length > 0)
            {
                int count = 0;

                foreach (var constructorArgument in attributeData.ConstructorArguments)
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
                                attributeToReturn.PropertyName = GetPropertyNameFromFieldName(fieldName);
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

            return new PropertyMappingAttribute(
                targetHint: AutoLayoutTarget.Implicit,
                propertyName: GetPropertyNameFromFieldName(fieldName));
        }

        private CommandMappingAttribute GetCommandMappingAttribute(string fieldName, AttributeData attributeData)
        {
            if (Debugger.IsAttached)
                Debugger.Break();

            if (attributeData is null)
            {
                return new CommandMappingAttribute(
                    AutoLayoutTarget.Implicit,
                    fieldName);
            }

            var attributeToReturn = new CommandMappingAttribute();

            if (attributeData.NamedArguments.Length > 0)
            {
                foreach (var namedArgument in attributeData.NamedArguments)
                {
                    switch (namedArgument.Key)
                    {
                        case nameof(PropertyMappingAttribute.TargetHint):
                            attributeToReturn.TargetHint = (AutoLayoutTarget)namedArgument.Value.Value!;
                            break;
                        case nameof(PropertyMappingAttribute.PropertyName):
                            attributeToReturn.PropertyName = (string)namedArgument.Value.Value!;
                            break;
                        case nameof(PropertyMappingAttribute.DisplayName):
                            attributeToReturn.DisplayName = (string)namedArgument.Value.Value!;
                            break;
                        default:
                            break;
                    }
                }

                if (string.IsNullOrEmpty(attributeToReturn.DisplayName))
                {
                    try
                    {
                        attributeToReturn.DisplayName = attributeData.ConstructorArguments[1].Value!.ToString();
                    }
                    catch (Exception)
                    {
                        attributeToReturn.DisplayName = fieldName;
                    }
                }

                return attributeToReturn;
            }

            if (attributeData.ConstructorArguments.Length > 0)
            {
                int count = 0;

                foreach (var constructorArgument in attributeData.ConstructorArguments)
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
                                attributeToReturn.PropertyName = GetPropertyNameFromFieldName(fieldName);
                            }
                            break;
                        case 3:
                            attributeToReturn.GetAccessorScope = (Scope)constructorArgument.Value!;
                            break;
                        case 4:
                            attributeToReturn.SetAccessorScope = (Scope)constructorArgument.Value!;
                            break;
                    }
                }

                return attributeToReturn;
            }

            return new CommandMappingAttribute(
                targetHint: AutoLayoutTarget.Implicit,
                propertyName: GetPropertyNameFromFieldName(fieldName));
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

        private static string GetFieldNameFromPropertyName(string propertyName)
            => "_" + propertyName[..1].ToLower() + propertyName[1..];

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
                    + fieldName[2..];
            }
            else
            {
                propertyName = (lowerFirstChar
                    ? fieldName[..1].ToLower()
                    : fieldName[..1].ToUpper())
                    + fieldName[1..];
            }

            return propertyName;
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new AutoLayoutSyntaxReceiver());
        }
    }
}
