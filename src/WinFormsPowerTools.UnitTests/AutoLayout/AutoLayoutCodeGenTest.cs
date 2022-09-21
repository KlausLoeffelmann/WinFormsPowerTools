using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using WinFormsPowerTools.CodeGen;
using Xunit;

namespace WinFormsPowerTools.UnitTests.AutoLayout
{
    public class AutoLayoutCodeGenTest
    {
        [Fact]
        public void SimpleCodeGenTest()
        {
            string userSource = @"

using System;

namespace WinFormsPowerTools.AutoLayout
{


    public class ViewControllerAttribute : Attribute
    {
        public ViewControllerAttribute()
        {
        }

        public ViewControllerAttribute(Type modelType)
        {
            ModelType = modelType;
        }

        public ViewControllerAttribute(Type modelType, params string[] excludeProperties)
        {
            ModelType = modelType;
            ExcludeProperties = excludeProperties;
        }

        public Type ModelType { get; }
        public string[] ExcludeProperties { get; }
    }
}

namespace WinFormsPowerTools.AutoLayout
{
    public class ViewControllerPropertyAttribute : Attribute
    {
        public ViewControllerPropertyAttribute()
        {
        }

        public ViewControllerPropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; }

        // TODO: Implement scope handling.
        public Scope GetAccessorScope { get; }
        public Scope SetAccessorScope { get; }
    }

    public enum Scope
    {
        @public,
        @private,
        @internal,
        @protected
    }
}

namespace MyCode
{
    using WinFormsPowerTools.AutoLayout;

    public class Foo
    {
        public Guid IDContact { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int NoOfChildren { get; set; }
    }

    public interface INotifyPropertyChanged : INotifyPropertyChanged
    { 
    }

    public abstract class ViewControllerBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        virtual protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }

    [ViewController(typeof(Foo))]
    public partial class TestFormsController : ViewControllerBase
    {
        private double _foo;

        [ViewControllerProperty] private string _firstName;
        [ViewControllerProperty(""LastName"")] private string _lstName;

        public string FooProperty {get; set;}
    }
}
";
            Compilation comp = CreateCompilation(userSource);
            var newComp = RunGenerators(comp, out var generatorDiags, new AutoLayoutGen());

            Assert.Empty(generatorDiags);
            var diagnostic = newComp.GetDiagnostics();
            Assert.Empty(diagnostic);
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
