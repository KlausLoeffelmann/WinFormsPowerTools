using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms.DataEntryForms.Components
{
    public partial class DateEntryFormatterComponent
    {
        private static readonly DateTimeFormatInfo s_dateFormats;
        private static readonly string[] s_dateTimeFormatStrings;

        static DateEntryFormatterComponent()
        {
            s_dateFormats = CultureInfo.CurrentCulture.DateTimeFormat;
            s_dateTimeFormatStrings = new[]
            {
                s_dateFormats.ShortDatePattern,
                s_dateFormats.LongDatePattern,
                s_dateFormats.ShortDatePattern,
                s_dateFormats.LongDatePattern,
                s_dateFormats.SortableDateTimePattern,
                s_dateFormats.UniversalSortableDateTimePattern,
                s_dateFormats.RFC1123Pattern,
                s_dateFormats.ShortDatePattern + " " +
                s_dateFormats.ShortTimePattern
            };
        }

        public class DateDataEntryFormatter : DataEntryFormatter<DateTime>
        {
            [DefaultValue(DateTimeFormats.ShortDate)]
            public DateTimeFormats DateTimeFormat { get; set; } = DateTimeFormats.ShortDate;

            public override string ConvertToDisplay(DateTime value)
            {
                return value.ToString(GetFormatString());
            }

            public override DateTime ConvertToValue(string stringValue)
            {
                return DateTime.Parse(stringValue);
            }

            public override string InitializeEditedValue(DateTime value)
            {
                return value.ToString();
            }

            public string GetFormatString()
            {
                return s_dateTimeFormatStrings[(int)DateTimeFormat];
            }
        }
    }
}
