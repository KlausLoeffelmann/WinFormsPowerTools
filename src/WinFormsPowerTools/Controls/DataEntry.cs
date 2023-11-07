using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms.DataEntryForms.Components;

namespace System.Windows.Forms.DataEntryForms.Controls
{
    [
    DefaultBindingProperty(nameof(DataEntry.ObjectValue)),
    ToolboxBitmap(typeof(DataEntry),"DataEntry.bmp")
    ]
    public class DataEntry : TextBox, ISupportInitialize
    {
        public event EventHandler ObjectValueChanged;
        
        private const bool DoFocusEmphasizeDefaultSetting = true;

        private readonly Color ErrorColorDefaultSetting = Color.Red;
        private readonly Color FocusColorDefaultSetting = Color.Yellow;
        private readonly FocusSelectionBehaviours FocusSelectionBehaviorDefaultSetting = FocusSelectionBehaviours.PreSelectInput;

        private bool _initializing;
        private bool _hasFocus;
        private readonly bool _commitOnFocusedRead = true;
        private bool _changingValueInternally;

        private Color _myOriginalBackColor;
        private string _editedValue;
        private object _valueInternal;
        private bool _hasError;
        private Guid _valueProcessCycle;
        private IDataEntryFormatterComponent _formatter;

        public DataEntry()
        {
            FocusEmphasize = DoFocusEmphasizeDefaultSetting;
            ErrorColor = ErrorColorDefaultSetting;
            FocusColor = FocusColorDefaultSetting;
            FocusSelectionBehavior = FocusSelectionBehaviorDefaultSetting;
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            _valueProcessCycle = Guid.NewGuid();
            Debug.Print("");
            Debug.Print($"{_valueProcessCycle}: OnEnter ({this.Name})");
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Debug.Print($"{_valueProcessCycle}: OnGotFocus ({this.Name})");

            _hasFocus = true;
            Text = _editedValue;

            HandleFocusEmphasizing(!_hasError);

            switch (FocusSelectionBehavior)
            {
                case FocusSelectionBehaviours.PreSelectInput:
                    SelectAll();
                    break;
                case FocusSelectionBehaviours.PlaceCaretAtEnd:
                    SelectionStart = Text.Length;
                    SelectionLength = 0;
                    break;
                default:
                    SelectionStart = 0;
                    SelectionLength = 0;
                    break;
            }
        }

        private void HandleFocusEmphasizing(bool savePreviousColorState)
        {
            if (savePreviousColorState)
            {
                _myOriginalBackColor = BackColor;
            }

            if (FocusEmphasize)
            {
                BackColor = _hasError ? ErrorColor : FocusColor;
            }
            else
            {
                BackColor = _hasError ? ErrorColor : BackColor;
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Debug.Print($"{_valueProcessCycle}: OnLostFocus ({this.Name})");

            _hasFocus = false;

            if (FocusEmphasize)
            {
                BackColor = _myOriginalBackColor;
            }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);
            Debug.Print($"{_valueProcessCycle}: OnValidating ({this.Name})");

            _editedValue = Text;

            if (Formatter is IDataEntryFormatterComponent dataEntryFormatter)
            {
                if (TryCommitInput())
                {
                    try
                    {
                        Text = dataEntryFormatter.ConvertToDisplay(this);
                    }
                    finally
                    {
                    }

                    if (_hasError)
                    {
                        _hasError = false;
                        if (dataEntryFormatter is ErrorProvider errorProvider)
                        {
                            errorProvider.SetError(this, null);
                        }
                    }
                }

                else
                {
                    _hasError = true;
                    e.Cancel = true;

                    if (dataEntryFormatter is ErrorProvider errorProvider)
                    {
                        // TODO: Make Error message configurable via dedicated property.
                        errorProvider.SetError(
                            this,
                            "Wrong input format - please check your input.");
                    }

                    HandleFocusEmphasizing(!_hasFocus);
                }
            }
        }

        private bool TryCommitInput()
        {
            if (Formatter is IDataEntryFormatterComponent dataEntryFormatter)
            {
                if (dataEntryFormatter.TryConvertToValue(this, _editedValue))
                {
                    try
                    {
                        _changingValueInternally = true;
                        ObjectValue = dataEntryFormatter.GetValue(this);
                    }
                    finally
                    {
                        _changingValueInternally = false;
                    }

                    return true;
                }
            }

            return false;
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            Debug.Print($"{_valueProcessCycle}: OnLeave ({this.Name})");
        }

        public void BeginInit()
        {
            _initializing = true;
        }

        public void EndInit()
        {
            _initializing = false;
            if (Formatter != null)
            {
                _formatter.SetDefaultFormatterInstanceOnDemand(this);
                _editedValue = Formatter.InitializeEditedValue(this);
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            if (_hasFocus)
            {
                Text = _editedValue;
            }
            else
            {
                Text = Formatter.ConvertToDisplay(this);
            }
        }

        /// <summary>
        /// Sets or gets the Text. Shadowed, because we need to hide it from Designer and Editor.
        /// </summary>
        /// <value></value>
        [
        Bindable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never), Browsable(false)
        ]
        public override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        /// <summary>
        /// Sets or returns a value which determines if the control's background should be colored if it gets the focus. 
        /// </summary>
        /// <value></value>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Description("Sets or returns a value which determines if the control's background should be colored if it gets the focus. "),
        Category("Behavior"),
        EditorBrowsable(EditorBrowsableState.Always), Browsable(true)
        ]
        public bool FocusEmphasize { get; set; }

        /// <summary>
        /// Sets or returns a value which determines the background color which is applied when the controls gets the focus.
        /// </summary>
        /// <value></value>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Description("Sets or returns a value which determines the background color which is applied when the controls gets the focus."),
        Category("Behavior"),
        EditorBrowsable(EditorBrowsableState.Always),
        Browsable(true)
        ]
        public Color FocusColor { get; set; }

        /// <summary>
        /// Sets or returns a value which determines the error background color which is applied on a failed validation.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Description("Sets or returns a value which determines the error background color which is applied on a failed validation."),
        Category("Behavior"),
        EditorBrowsable(EditorBrowsableState.Always), Browsable(true)
        ]
        public Color ErrorColor { get; set; }

        /// <summary>
        /// Sets or returns a value which determines how the preselection of text in the control is handled when it gets the focus. 
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        Description("Sets or retrieves how the preselection of text in the control is handled when it gets the focus."),
        Category("Behavior"),
        EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        public FocusSelectionBehaviours FocusSelectionBehavior { get; set; }

        [RefreshProperties(RefreshProperties.All)]
        public IDataEntryFormatterComponent Formatter
        {
            get => _formatter;
            set
            {
                if (!object.Equals(value, _formatter))
                {
                    _formatter = value;
                    
                    if (_formatter != null)
                    {
                        if (!_initializing)
                        {
                            _formatter.SetDefaultFormatterInstanceOnDemand(this);
                            _valueInternal = _formatter.GetDefaultValue();
                            Formatter.SetValue(this, _valueInternal);
                            _editedValue = Formatter.InitializeEditedValue(this);
                            UpdateDisplay();
                        }
                    }
                }
            }
        }

        [
        Bindable(true),
        Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)
        ]
        public object ObjectValue
        {
            get
            {
                if (_hasFocus && _commitOnFocusedRead)
                {
                    _editedValue = Text;
                    TryCommitInput();
                }

                return _valueInternal;
            }

            set
            {
                if (!object.Equals(value, _valueInternal))
                {
                    _valueInternal = value;

                    if (Formatter is null)
                    {
                        OnObjectValueChanged();
                    }
                }

                if (!_changingValueInternally)
                {
                    if (Formatter != null && !_initializing)
                    {
                        Formatter.SetValue(this, _valueInternal);
                        _editedValue = Formatter.InitializeEditedValue(this);
                        UpdateDisplay();
                    }
                }
            }
        }

        // If we have a Formatter, then this came from it, otherwise it could have only
        // come for the ObjectValue property, because there wouldn't be another infrastructure
        // in place to have this changed. In the first case, we need to update the internal value,
        // in case the Formatter goes away so we still have the latest update.
        internal protected void OnObjectValueChanged()
        {
            if (Formatter != null)
            {
                _valueInternal = Formatter.GetValue(this);
                UpdateDisplay();
            }

            ObjectValueChanged?.Invoke(this,EventArgs.Empty);
        }
    }

    internal enum DataEntryValueOrigin
    {
        ObjectValue,
        TypedValue
    }
}
