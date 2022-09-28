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
            Dictionary<IMethodSymbol, AttributeData> commandMethodDefinitions)
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
        public Dictionary<IMethodSymbol, AttributeData> CommandMethodDefinitions { get; set; }
    }
}
