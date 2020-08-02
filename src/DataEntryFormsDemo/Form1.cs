using System;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataEntryForms;
using System.Windows.Forms.DataEntryForms.Controls;

namespace DataEntryFormsDemo
{
    public partial class Form1 : Form
    {
        private Timer _delayOperationTimer;
        private Timer _propertyReportTimer;

        public Form1()
        {
            InitializeComponent();
            saveToolStripButton.Click += saveToolStripButton_Clicked;

            _propertyReportTimer = new Timer();
            _propertyReportTimer.Interval = 500;
            //_propertyReportTimer.Enabled = true;
            _propertyReportTimer.Tick += PropertyReportTimer_Tick;
            //_propertyReportTimer.Start();
        }

        private void PropertyReportTimer_Tick(object sender, EventArgs e)
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

        private void saveToolStripButton_Clicked(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.WriteLine($"Value of {dataEntry1.Name}: {dataEntry1.ObjectValue?.ToString()}");
            textBox1.WriteLine($"Value of {dataEntry2.Name}: {dataEntry2.ObjectValue?.ToString()}");
        }

        private void btnSetValueDelayed_Click(object sender, EventArgs e)
        {
            _delayOperationTimer = new Timer() { Tag = new Action(WriteDelayedTypedValues) };
            _delayOperationTimer.Interval = 1000;
            btnSetTypesValueDelayed.Tag = (5, Text);
            _delayOperationTimer.Tick += _delayOperationTimer_Tick;
            _delayOperationTimer.Enabled = true;
        }

        private void btnSetObjectValuesDelayed_Click(object sender, EventArgs e)
        {
            _delayOperationTimer = new Timer() { Tag = new Action(WriteDelayedObjectValues) };
            _delayOperationTimer.Interval = 1000;
            btnSetTypesValueDelayed.Tag = (5, Text);
            _delayOperationTimer.Tick += _delayOperationTimer_Tick;
            _delayOperationTimer.Enabled = true;
        }

        private void _delayOperationTimer_Tick(object sender, EventArgs e)
        {
            btnSetTypesValueDelayed.Enabled = false;
            (int counter, string oldText) = ((int, string))btnSetTypesValueDelayed.Tag;
            if (counter--==0)
            {
                _delayOperationTimer.Dispose();
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
            decimalEntryFormatter1.SetValue(dataEntry1, random.Next(10000));
            decimalEntryFormatter1.SetValue(dataEntry2, random.Next(10000));
            decimalEntryFormatter1.SetValue(dataEntry3, random.Next(10000));
        }

        private void WriteDelayedObjectValues()
        {
            var random = new Random(DateTime.Now.Millisecond);
            dataEntry1.ObjectValue = (decimal) random.Next(10000);
            dataEntry2.ObjectValue = (decimal) random.Next(10000);
            dataEntry3.ObjectValue = (decimal) random.Next(10000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var binding = new Binding(nameof(TestProperty), textBox2, nameof(TextBox.Text), false, DataSourceUpdateMode.OnPropertyChanged);
        }

        public string TestProperty { get; set; }
    }
}
