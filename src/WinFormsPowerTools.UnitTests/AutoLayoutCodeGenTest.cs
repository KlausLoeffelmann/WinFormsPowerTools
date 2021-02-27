using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using WinFormsPowerTools.CodeGen;
using Xunit;

namespace PowerTools.UnitTests
{
    public class AutoLayoutCodeGenTest
    {
        [Fact]
        public void SimpleCodeGenTest()
        {
            string userSource = @"
namespace MyCode
{
   public class Foo
    {
        public Guid IDContact { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int NoOfChildren { get; set; }
    }

    public class AutoLayoutComponent
    {
        public AutoLayoutComponent(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Caption { get; set; }
        public string ComponentTypename { get; set; }
        public Padding Margin { get; set; }
        public PropertyDescriptor Binding { get; set; }
    }

    public class AutoLayoutViewModelBase<T> where T : class
    {
        public AutoLayoutDocument Document { get; set; } = new AutoLayoutDocument(""document1"");
        public T DataContext { get; set; }
    }

    public partial class vmOptions : AutoLayoutViewModelBase<Foo>
    {

        private AutoLayoutComponent _idContactProperty;
        private AutoLayoutComponent IdContextComponent
        {
            get
            {
                // Return new Component.
                return null;
            }
        }
    }
";
            Compilation comp = CreateCompilation(userSource);
            var newComp = RunGenerators(comp, out var generatorDiags, new AutoLayoutGen());

            Assert.Empty(generatorDiags);
            Assert.Empty(newComp.GetDiagnostics());
        }

        private static Compilation CreateCompilation(string source) => CSharpCompilation.Create(
           assemblyName: "compilation",
           syntaxTrees: new[] { CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(LanguageVersion.Preview)) },
           references: new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) },
           options: new CSharpCompilationOptions(OutputKind.ConsoleApplication)
       );

        private static GeneratorDriver CreateDriver(Compilation compilation, params ISourceGenerator[] generators) => CSharpGeneratorDriver.Create(
            generators: ImmutableArray.Create(generators),
            additionalTexts: ImmutableArray<AdditionalText>.Empty,
            parseOptions: (CSharpParseOptions)compilation.SyntaxTrees.First().Options,
            optionsProvider: null
        );

        private static Compilation RunGenerators(Compilation compilation, out ImmutableArray<Diagnostic> diagnostics, params ISourceGenerator[] generators)
        {
            CreateDriver(compilation, generators).RunGeneratorsAndUpdateCompilation(compilation, out var updatedCompilation, out diagnostics);
            return updatedCompilation;
        }
    }
}

