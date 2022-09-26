using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace WinFormsPowerTools.CodeGen
{
    internal class ViewModelInfo
    {
        public ViewModelInfo(
            ClassDeclarationSyntax classDeclaration, 
            AttributeSyntax classAttribute, 
            SyntaxTree syntaxTree, 
            Dictionary<FieldDeclarationSyntax, AttributeSyntax> foundModelClasses)
        { 
            ClassDeclaration = classDeclaration;
            ClassAttribute = classAttribute;
            SyntaxTree = syntaxTree;
            FoundModelClasses = foundModelClasses;
        }
        
        public ClassDeclarationSyntax ClassDeclaration { get; set; }
        public AttributeSyntax ClassAttribute { get; set; }
        public SyntaxTree SyntaxTree { get; set; }
        public Dictionary<FieldDeclarationSyntax, AttributeSyntax> FoundModelClasses { get; set; }
    }
}
