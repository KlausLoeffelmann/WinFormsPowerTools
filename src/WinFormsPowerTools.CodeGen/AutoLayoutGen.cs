using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

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

            var source = "public class FooFighter { }";
            if (!string.IsNullOrEmpty(source))
            {
                context.AddSource(hintName: "generated.cs", source);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new AutoLayoutSyntaxReceiver());
        }
    }

    internal class AutoLayoutSyntaxReceiver : ISyntaxReceiver
    {
        public string Classname { get; private set; }
        public SeparatedSyntaxList<TypeParameterSyntax> Parameters { get; private set; }
        public SeparatedSyntaxList<BaseTypeSyntax> BaseTypes { get; private set; }

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classDeclaration)
            {
                if (classDeclaration.BaseList?.Types.Count > 0)
                {
                    BaseTypes = classDeclaration.BaseList.Types;

                    if (classDeclaration.TypeParameterList?.Parameters.Count > 0)
                    {
                        Parameters = classDeclaration.TypeParameterList.Parameters;
                    }
                }
            }
        }
    }
}
