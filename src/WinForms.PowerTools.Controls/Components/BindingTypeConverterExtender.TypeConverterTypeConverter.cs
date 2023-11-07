using System.ComponentModel;
using System.Diagnostics;

namespace WinForms.PowerTools.Components;

public partial class BindingTypeConverterExtender
{
    /// <summary>
    ///  This name is no joke: It's the TypeConverter for the TypeConverters which we are interested in.
    /// </summary>
    private class TypeConverterTypeConverter : TypeConverter
    {
        // Implementation to list available TypeConverters
        // This is just conceptual; actual implementation would require reflection and filtering

        private static readonly Type[]? s_typeConverters;

        public override bool GetStandardValuesSupported(ITypeDescriptorContext? context) 
            => true;

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext? context)
            => new(s_typeConverters);

        private static List<Type> GetExportedTypes<TBase>(
    params Type[] requiredAttributes)
        {
            var appDomainAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly =>
                {
                    var assemblyName = assembly.GetName().Name;
                    if (assemblyName is null)
                    {
                        return false;
                    }

                    return !SystemAssemblyNames.Contains(assemblyName);
                });

            var exportedTypes = new List<Type>();

            foreach (var assemblyItem in appDomainAssemblies)
            {
                exportedTypes.AddRange(
                    FilterTypesByAttributes(assemblyItem.GetExportedTypes(), requiredAttributes));
            }

            return exportedTypes.Where(
                t => typeof(TBase).IsAssignableFrom(t)
                    && t.IsClass
                    && !t.IsAbstract)
                .ToList();
        }

        static IEnumerable<Type> FilterTypesByAttributes(IEnumerable<Type> types, Type[] attributes)
            => types
                .Where(type => attributes.All(attribute => type.GetCustomAttributes(attribute, true)
                .Length != 0));

        static TypeConverterTypeConverter()
            => s_typeConverters =
                [
                    .. GetExportedTypes<TypeConverter>(
                                    typeof(BindingConverterAttribute)),
                ];

        public static readonly HashSet<string> SystemAssemblyNames =
        [
            "Accessibility",
            "DirectWriteForwarder",
            "Microsoft.Bcl.AsyncInterfaces",
            "Microsoft.CSharp",
            "Microsoft.CodeAnalysis",
            "Microsoft.CodeAnalysis.CSharp",
            "Microsoft.CodeAnalysis.CSharp.Workspaces",
            "Microsoft.CodeAnalysis.Workspaces",
            "Microsoft.Extensions.DependencyInjection",
            "Microsoft.Extensions.DependencyInjection.Abstractions",
            "Microsoft.Extensions.DependencyModel",
            "Microsoft.Extensions.Logging",
            "Microsoft.Extensions.Logging.Abstractions",
            "Microsoft.Extensions.Options",
            "Microsoft.Extensions.Primitives",
            "Microsoft.VisualBasic",
            "Microsoft.VisualBasic.Core",
            "Microsoft.VisualBasic.Forms",
            "Microsoft.VisualStudio.Debugger.Runtime.NetCoreApp",
            "Microsoft.VisualStudio.DesignTools.WpfTap",
            "Microsoft.VisualStudio.Threading",
            "Microsoft.VisualStudio.Validation",
            "Microsoft.Win32.Primitives",
            "Microsoft.Win32.Registry",
            "Microsoft.Win32.Registry.AccessControl",
            "Microsoft.Win32.SystemEvents",
            "Microsoft.WinForms.DesignTools.Protocol",
            "Microsoft.WinForms.DesignTools.Server",
            "Microsoft.WinForms.Utilities.Desktop",
            "Microsoft.WinForms.Utilities.Shared",
            "PresentationCore",
            "PresentationFramework",
            "PresentationFramework-SystemCore",
            "PresentationFramework-SystemData",
            "PresentationFramework-SystemDrawing",
            "PresentationFramework-SystemXml",
            "PresentationFramework-SystemXmlLinq",
            "PresentationFramework.Aero",
            "PresentationFramework.Aero2",
            "PresentationFramework.AeroLite",
            "PresentationFramework.Classic",
            "PresentationFramework.Luna",
            "PresentationFramework.Royale",
            "PresentationUI",
            "ReachFramework",
            "StreamJsonRpc",
            "System",
            "System.AppContext",
            "System.Buffers",
            "System.CodeDom",
            "System.CodeDom",
            "System.Collections",
            "System.Collections.Concurrent",
            "System.Collections.Immutable",
            "System.Collections.Immutable",
            "System.Collections.NonGeneric",
            "System.Collections.Specialized",
            "System.ComponentModel",
            "System.ComponentModel.Annotations",
            "System.ComponentModel.DataAnnotations",
            "System.ComponentModel.EventBasedAsync",
            "System.ComponentModel.Primitives",
            "System.ComponentModel.TypeConverter",
            "System.Composition.AttributedModel",
            "System.Composition.AttributedModel",
            "System.Composition.Convention",
            "System.Composition.Hosting",
            "System.Composition.Hosting",
            "System.Composition.Runtime",
            "System.Composition.Runtime",
            "System.Composition.TypedParts",
            "System.Composition.TypedParts",
            "System.Configuration",
            "System.Configuration.ConfigurationManager",
            "System.Console",
            "System.Core",
            "System.Data",
            "System.Data.Common",
            "System.Data.DataSetExtensions",
            "System.Design",
            "System.Diagnostics.Contracts",
            "System.Diagnostics.Debug",
            "System.Diagnostics.DiagnosticSource",
            "System.Diagnostics.EventLog",
            "System.Diagnostics.EventLog.Messages",
            "System.Diagnostics.FileVersionInfo",
            "System.Diagnostics.PerformanceCounter",
            "System.Diagnostics.Process",
            "System.Diagnostics.StackTrace",
            "System.Diagnostics.TextWriterTraceListener",
            "System.Diagnostics.Tools",
            "System.Diagnostics.TraceSource",
            "System.Diagnostics.Tracing",
            "System.DirectoryServices",
            "System.Drawing",
            "System.Drawing.Common",
            "System.Drawing.Design",
            "System.Drawing.Primitives",
            "System.Dynamic.Runtime",
            "System.Formats.Asn1",
            "System.Formats.Tar",
            "System.Globalization",
            "System.Globalization.Calendars",
            "System.Globalization.Extensions",
            "System.IO",
            "System.IO.Compression",
            "System.IO.Compression.Brotli",
            "System.IO.Compression.FileSystem",
            "System.IO.Compression.ZipFile",
            "System.IO.FileSystem",
            "System.IO.FileSystem.AccessControl",
            "System.IO.FileSystem.DriveInfo",
            "System.IO.FileSystem.Primitives",
            "System.IO.FileSystem.Watcher",
            "System.IO.IsolatedStorage",
            "System.IO.MemoryMappedFiles",
            "System.IO.Packaging",
            "System.IO.Pipelines",
            "System.IO.Pipelines",
            "System.IO.Pipes",
            "System.IO.Pipes.AccessControl",
            "System.IO.UnmanagedMemoryStream",
            "System.Linq",
            "System.Linq.Expressions",
            "System.Linq.Parallel",
            "System.Linq.Queryable",
            "System.Memory",
            "System.Net",
            "System.Net.Http",
            "System.Net.Http.Json",
            "System.Net.HttpListener",
            "System.Net.Mail",
            "System.Net.NameResolution",
            "System.Net.NetworkInformation",
            "System.Net.Ping",
            "System.Net.Primitives",
            "System.Net.Quic",
            "System.Net.Requests",
            "System.Net.Security",
            "System.Net.ServicePoint",
            "System.Net.Sockets",
            "System.Net.WebClient",
            "System.Net.WebHeaderCollection",
            "System.Net.WebProxy",
            "System.Net.WebSockets",
            "System.Net.WebSockets.Client",
            "System.Numerics",
            "System.Numerics.Vectors",
            "System.ObjectModel",
            "System.Printing",
            "System.Private.CoreLib",
            "System.Private.DataContractSerialization",
            "System.Private.Uri",
            "System.Private.Xml",
            "System.Private.Xml.Linq",
            "System.Reflection",
            "System.Reflection.DispatchProxy",
            "System.Reflection.Emit",
            "System.Reflection.Emit.ILGeneration",
            "System.Reflection.Emit.Lightweight",
            "System.Reflection.Extensions",
            "System.Reflection.Metadata",
            "System.Reflection.Primitives",
            "System.Reflection.TypeExtensions",
            "System.Resources.Extensions",
            "System.Resources.Reader",
            "System.Resources.ResourceManager",
            "System.Resources.Writer",
            "System.Runtime",
            "System.Runtime.CompilerServices.Unsafe",
            "System.Runtime.CompilerServices.Unsafe",
            "System.Runtime.CompilerServices.VisualC",
            "System.Runtime.Extensions",
            "System.Runtime.Handles",
            "System.Runtime.InteropServices",
            "System.Runtime.InteropServices.JavaScript",
            "System.Runtime.InteropServices.RuntimeInformation",
            "System.Runtime.Intrinsics",
            "System.Runtime.Loader",
            "System.Runtime.Numerics",
            "System.Runtime.Serialization",
            "System.Runtime.Serialization.Formatters",
            "System.Runtime.Serialization.Json",
            "System.Runtime.Serialization.Primitives",
            "System.Runtime.Serialization.Xml",
            "System.Security",
            "System.Security.AccessControl",
            "System.Security.Claims",
            "System.Security.Cryptography",
            "System.Security.Cryptography.Algorithms",
            "System.Security.Cryptography.Cng",
            "System.Security.Cryptography.Csp",
            "System.Security.Cryptography.Encoding",
            "System.Security.Cryptography.OpenSsl",
            "System.Security.Cryptography.Pkcs",
            "System.Security.Cryptography.Primitives",
            "System.Security.Cryptography.ProtectedData",
            "System.Security.Cryptography.X509Certificates",
            "System.Security.Cryptography.Xml",
            "System.Security.Permissions",
            "System.Security.Principal",
            "System.Security.Principal.Windows",
            "System.Security.SecureString",
            "System.ServiceModel.Web",
            "System.ServiceProcess",
            "System.Text.Encoding",
            "System.Text.Encoding.CodePages",
            "System.Text.Encoding.Extensions",
            "System.Text.Encodings.Web",
            "System.Text.Json",
            "System.Text.RegularExpressions",
            "System.Threading",
            "System.Threading.AccessControl",
            "System.Threading.Channels",
            "System.Threading.Overlapped",
            "System.Threading.Tasks",
            "System.Threading.Tasks.Dataflow",
            "System.Threading.Tasks.Extensions",
            "System.Threading.Tasks.Parallel",
            "System.Threading.Thread",
            "System.Threading.ThreadPool",
            "System.Threading.Timer",
            "System.Transactions",
            "System.Transactions.Local",
            "System.ValueTuple",
            "System.Web",
            "System.Web.HttpUtility",
            "System.Windows",
            "System.Windows.Controls.Ribbon",
            "System.Windows.Extensions",
            "System.Windows.Input.Manipulations",
            "System.Windows.Presentation",
            "System.Xaml",
            "System.Xml",
            "System.Xml.Linq",
            "System.Xml.ReaderWriter",
            "System.Xml.Serialization",
            "System.Xml.XDocument",
            "System.Xml.XPath",
            "System.Xml.XPath.XDocument",
            "System.Xml.XmlDocument",
            "System.Xml.XmlSerializer",
            "UIAutomationClient",
            "UIAutomationClientSideProviders",
            "UIAutomationProvider",
            "UIAutomationTypes",
            "WindowsBase",
            "WindowsFormsIntegration",
            "mscorlib",
            "netstandard"
        ];
    }
}
