using System;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataEntryForms;
using System.Windows.Forms.DataEntryForms.Controls;

namespace WinFormsPowerToolsDemo
{
    public partial class DarkModeTestForm : Form
    {
        private Timer? _delayOperationTimer;
        private readonly Timer? _propertyReportTimer;

        public DarkModeTestForm()
        {
            InitializeComponent();
            saveToolStripButton.Click += SaveToolStripButton_Clicked;

            _propertyReportTimer = new Timer
            {
                Interval = 500
            };

            //_propertyReportTimer.Enabled = true;
            _propertyReportTimer.Tick += PropertyReportTimer_Tick;
            //_propertyReportTimer.Start();
        }

        private void PropertyReportTimer_Tick(object? sender, EventArgs e)
        {
            var objectValuePropertiesReport = new StringBuilder();
            var typedValuePropertiesReport = new StringBuilder();

            foreach(Control control in Controls)
            {
                if (control is DataEntry dataEntry)
                {
                    objectValuePropertiesReport.Append($"DataEntry {dataEntry.Name}, ObjectValue: {dataEntry.ObjectValue?.ToString()}\n");
                    typedValuePropertiesReport.Append($"DataEntry {dataEntry.Name}, typed value: {dataEntry.Formatter.GetValue(dataEntry)?.ToString()}\n");
                }
            }

            lblObjectValues.Text = objectValuePropertiesReport.ToString();
            lblTypedValues.Text = typedValuePropertiesReport.ToString();
        }

        private void SaveToolStripButton_Clicked(object? sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.WriteLine($"Value of {dataEntry1.Name}: {dataEntry1.ObjectValue?.ToString()}");
            textBox1.WriteLine($"Value of {dataEntry2.Name}: {dataEntry2.ObjectValue?.ToString()}");
        }

        private void BtnSetValueDelayed_Click(object? sender, EventArgs e)
        {
            _delayOperationTimer = new Timer
            {
                Tag = new Action(WriteDelayedTypedValues),
                Interval = 1000
            };

            btnSetTypesValueDelayed.Tag = (5, Text);
            _delayOperationTimer.Tick += DelayOperationTimer_Tick;
            _delayOperationTimer.Enabled = true;
        }

        private void BtnSetObjectValuesDelayed_Click(object? sender, EventArgs e)
        {
            _delayOperationTimer = new Timer
            {
                Tag = new Action(WriteDelayedObjectValues),
                Interval = 1000
            };

            btnSetTypesValueDelayed.Tag = (5, Text);
            _delayOperationTimer.Tick += DelayOperationTimer_Tick;
            _delayOperationTimer.Enabled = true;
        }

        private void DelayOperationTimer_Tick(object? sender, EventArgs e)
        {
            btnSetTypesValueDelayed.Enabled = false;
            (int counter, string oldText) = ((int, string))btnSetTypesValueDelayed.Tag;
            if (counter--==0)
            {
                _delayOperationTimer?.Dispose();
                btnSetTypesValueDelayed.Text = oldText;
                btnSetTypesValueDelayed.Enabled = true;
                ((Action)(_delayOperationTimer).Tag).Invoke();
            }
            else
            {
                btnSetTypesValueDelayed.Tag = (counter, oldText);
                btnSetTypesValueDelayed.Text = counter.ToString();
            }
        }

        private void WriteDelayedTypedValues()
        {
            var random = new Random(DateTime.Now.Millisecond);
            decimalEntryFormatterComponent1.SetValue(dataEntry1, random.Next(10000));
            decimalEntryFormatterComponent1.SetValue(dataEntry2, random.Next(10000));
            decimalEntryFormatterComponent1.SetValue(dataEntry3, random.Next(10000));
        }

        private void WriteDelayedObjectValues()
        {
            var random = new Random(DateTime.Now.Millisecond);
            dataEntry1.ObjectValue = (decimal) random.Next(10000);
            dataEntry2.ObjectValue = (decimal) random.Next(10000);
            dataEntry3.ObjectValue = (decimal) random.Next(10000);
        }
    }
}
