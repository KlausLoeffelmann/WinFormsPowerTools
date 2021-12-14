using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms.TemplateBinding
{
    public class UiControllerManagerComponent : Component
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Dictionary<string, UiControllerBindingItem> BindingAssignmentsLookup { get; set; }
            = new Dictionary<string, UiControllerBindingItem>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<UiControllerBindingItem> BindingAssignments { get; set; }=new List<UiControllerBindingItem>();
    }
}
