using System.ComponentModel;
using System.Text;
using System.Windows.Forms.DataEntryForms.Controls;

namespace System.Windows.Forms.DataEntryForms.Components
{
    public partial class DecimalEntryFormatterComponent
    {
        private const string DefaultCurrencySymbol = "";

        public class DecimalDataEntryFormatter : DataEntryFormatter<decimal>
        {
            // Backing fields. We implement INotifyPropertyChanged, so we can't use AutoProperties.
            private string? _currencySymbol;
            private int _decimalPlaces;
            private bool _hasThousandsSeparator;
            private bool _placeCurrencySymbolUpFront;
            private bool _allowFormular;
            private int _leadingZeros;

            /// <summary>
            /// Sets or gets the currency sign that should be placed before or after the amount.
            /// </summary>
            [
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            Description("Sets or gets the currency sign that should be placed before or after the amount."),
            Category("Behavior"),
            EditorBrowsable(EditorBrowsableState.Advanced),
            Browsable(true), DefaultValue(DefaultCurrencySymbol)
            ]
            public string? CurrencySymbol
            {
                get
                {
                    return _currencySymbol;
                }
                set
                {
                    SetProperty(ref _currencySymbol, value);
                }
            }

            /// <summary>
            /// Sets or gets the number of decimal places to be displayed for fractions.
            /// </summary>
            [
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            Description("Sets or gets the number of decimal places to be displayed for fractions."),
            Category("Behavior"),
            EditorBrowsable(EditorBrowsableState.Advanced),
            Browsable(true), DefaultValue(-1)
            ]
            public int DecimalPlaces
            {
                get
                {
                    return _decimalPlaces;
                }
                set
                {
                    SetProperty(ref _decimalPlaces, value);
                }
            }

            /// <summary>
            /// Sets or sets whether the thousands separator should be displayed for numbers above 999 (<-999).
            /// </summary>
            [
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            Description("Sets or sets whether the thousands separator should be displayed for numbers above 999 (<-999)."),
            Category("Behavior"),
            EditorBrowsable(EditorBrowsableState.Advanced),
            Browsable(true), DefaultValue(false)
            ]
            public bool HasThousandsSeparator
            {
                get
                {
                    return _hasThousandsSeparator;
                }
                set
                {
                    SetProperty(ref _hasThousandsSeparator, value);
                }
            }

            /// <summary>
            /// Sets or gets whether the currency sign should be placed in front (true) or behind.
            /// </summary>
            [
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            Description("Sets or gets whether the currency sign should be placed in front (true) or behind."),
            Category("Behavior"),
            EditorBrowsable(EditorBrowsableState.Advanced),
            Browsable(true), DefaultValue(false)
            ]
            public bool PlaceCurrencySymbolUpFront
            {
                get
                {
                    return _placeCurrencySymbolUpFront;
                }
                set
                {
                    SetProperty(ref _placeCurrencySymbolUpFront, value);
                }
            }

            /// <summary>
            /// Sets or gets whether mathematical, calculable expressions (formulars) can be entered instead of values.
            /// </summary>
            [
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            Description("Sets or gets whether mathematical, calculable expressions (formulars) can be entered instead of values."),
            Category("Behavior"),
            EditorBrowsable(EditorBrowsableState.Advanced),
            Browsable(true), DefaultValue(false)
            ]
            public bool AllowFormular
            {
                get
                {
                    return _allowFormular;
                }
                set
                {
                    SetProperty(ref _allowFormular, value);
                }
            }

            /// <summary>
            /// Sets or gets whether the formatted value to be displayed is padded with the appropriate number of leading zeros.
            /// </summary>
            [
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            Description("Sets or gets whether the formatted value to be displayed is padded with the appropriate number of leading zeros."),
            Category("Behavior"),
            EditorBrowsable(EditorBrowsableState.Advanced),
            Browsable(true), DefaultValue(0)
            ]
            public int LeadingZeros
            {
                get
                {
                    return _leadingZeros;
                }
                set
                {
                    SetProperty(ref _leadingZeros, value);
                }
            }

            public override string ConvertToDisplay(decimal value)
            {
                return value.ToString(GetFormatString());
            }

            public override decimal ConvertToValue(string? stringValue) => 
                stringValue is null ? 0 : decimal.Parse(stringValue);

            public override string? InitializeEditedValue(decimal value) => 
                value.ToString();

            public string? GetFormatString()
            {
                var formatString = new StringBuilder();

                if (LeadingZeros > 0)
                {
                    formatString.Append('0', LeadingZeros);
                }
                else
                {
                    if (HasThousandsSeparator)
                    {
                        formatString.Append("#,##0");
                    }
                    else
                    {
                        formatString.Append("###0");
                    }
                }

                if (DecimalPlaces == -1)
                {
                    formatString.Append(".########################");
                }
                else if (DecimalPlaces > 0)
                {
                    formatString.Append('.');
                    formatString.Append('0', DecimalPlaces);
                }

                return formatString.ToString();
            }
        }

        protected override bool CanExtendProperties(DataEntry dataEntry)
            => dataEntry.Formatter is DecimalEntryFormatterComponent;
    }
}
