using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace WinFormsPowerTools.CodeGen
{
    internal class ViewModelClassInfo
    {
        public ViewModelClassInfo(
            ClassDeclarationSyntax classDeclaration, 
            AttributeSyntax classAttribute, 
            SyntaxTree syntaxTree, 
            Dictionary<IFieldSymbol, AttributeData> fieldDefinitions,
            Dictionary<string, CommandInfo> commandMethodDefinitions)
        { 
            ClassDeclaration = classDeclaration;
            ClassAttribute = classAttribute;
            SyntaxTree = syntaxTree;
            FieldDefinitions = fieldDefinitions;
            CommandMethodDefinitions = commandMethodDefinitions;
        }
        
        public ClassDeclarationSyntax ClassDeclaration { get; set; }
        public AttributeSyntax ClassAttribute { get; set; }
        public SyntaxTree SyntaxTree { get; set; }
        public Dictionary<IFieldSymbol, AttributeData> FieldDefinitions { get; set; }
        public Dictionary<string, CommandInfo> CommandMethodDefinitions { get; set; }
    }

    internal class CommandInfo
    {
        public CommandInfo(string baseLineName,
            AttributeData commandAttribute,
            IFieldSymbol? fieldSymbol,
            IMethodSymbol? executeMethodSymbol,
            IMethodSymbol? canExecuteMethodSymbol)
        {
            BaseLineName = baseLineName;
            CommandAttribute = commandAttribute;
            FieldSymbol = fieldSymbol;
            ExecuteMethodSymbol = executeMethodSymbol;
            CanExecuteMethodSymbol = canExecuteMethodSymbol;
        }

        public string BaseLineName { get; set; }
        public AttributeData CommandAttribute {get; set;}
        public IFieldSymbol? FieldSymbol { get; set; }
        public IMethodSymbol? ExecuteMethodSymbol { get; set; }
        public IMethodSymbol? CanExecuteMethodSymbol { get; set; }
    }
}
