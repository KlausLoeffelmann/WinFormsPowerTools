namespace WinForms.PowerTools.Controls;

public class SymbolSource<T> where T : Enum
{
    private bool _hasValue;
    private T? _symbol;

    public SymbolSource(string fontName)
    {
        FontName = fontName;
        _symbol = default!;
    }

    public T? Symbol 
    { 
        get => _symbol;

        internal set
        {
            if (value is null)
            {
                _hasValue = false;
            }

            _symbol = value;
            _hasValue = true;
        }
    }

    internal void SetSymbolNull()
    {
        Symbol = default!;
    }
    
    public bool HasSymbolValue
    {
        get => _hasValue;
    }

    public string FontName { get; }
}
