using System;

namespace System.Windows.Forms.TemplateBinding
{
    public class ChainValueChangedEventArgs : EventArgs
    {
        public ChainValueChangedEventArgs(ChainLink? link, string propertyName, ChainValueChangedReason valueChangedReason)
        {
            Link = link;
            PropertyName = propertyName;
            ValueChangedReason = valueChangedReason;
        }

        public ChainLink? Link { get; set; }
        public string PropertyName { get; set; }
        public ChainValueChangedReason ValueChangedReason {get; set; }
    }
}
