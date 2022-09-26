using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutDocuments<T>
        : Dictionary<string, AutoLayoutDocument<T>> where T : INotifyPropertyChanged
    {
        [AllowNull]
        private static AutoLayoutDocuments<T> _instance;

        private AutoLayoutDocuments()
        { }
        
        internal static AutoLayoutDocument<T> GetDocumentOrNew(string documentName, string? title = default, T? dataContext=default)
        {
            _instance ??= new AutoLayoutDocuments<T>();

            if (_instance.TryGetValue(documentName, out var document))
            {
                return document;
            }

            document = new AutoLayoutDocument<T>(name: documentName, dataContext: dataContext);
            _instance.Add(documentName, document);
            return document;
        }
    }
}
