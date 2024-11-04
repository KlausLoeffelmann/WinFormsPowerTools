namespace WinForms.PowerToolsDemo
{
    partial class ControlsTestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataEntryPanel1 = new PowerTools.Controls.DataEntryPanel();
            textBox1 = new System.Windows.Forms.TextBox();
            dateEntryFormatterComponent1 = new System.Windows.Forms.DataEntryForms.Components.DateEntryFormatterComponent();
            decimalEntryFormatterComponent1 = new System.Windows.Forms.DataEntryForms.Components.DecimalEntryFormatterComponent();
            dataEntryPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dateEntryFormatterComponent1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)decimalEntryFormatterComponent1).BeginInit();
            SuspendLayout();
            // 
            // dataEntryPanel1
            // 
            dataEntryPanel1.Controls.Add(textBox1);
            dataEntryPanel1.Location = new System.Drawing.Point(92, 62);
            dataEntryPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            dataEntryPanel1.Name = "dataEntryPanel1";
            dataEntryPanel1.Size = new System.Drawing.Size(642, 354);
            dataEntryPanel1.TabIndex = 0;
            // 
            // textBox1
            // 
            dataEntryPanel1.SetFormatterComponent(textBox1, decimalEntryFormatterComponent1);
            decimalEntryFormatterComponent1.SetFormattingProperties(textBox1, null);
            dateEntryFormatterComponent1.SetFormattingProperties(textBox1, null);
            textBox1.Location = new System.Drawing.Point(110, 63);
            textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(260, 27);
            textBox1.TabIndex = 0;
            // 
            // dateEntryFormatterComponent1
            // 
            dateEntryFormatterComponent1.ContainerControl = this;
            // 
            // decimalEntryFormatterComponent1
            // 
            decimalEntryFormatterComponent1.ContainerControl = this;
            // 
            // ControlsTestForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(946, 532);
            Controls.Add(dataEntryPanel1);
            Name = "ControlsTestForm";
            Text = "1st Controls Test Form";
            dataEntryPanel1.ResumeLayout(false);
            dataEntryPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dateEntryFormatterComponent1).EndInit();
            ((System.ComponentModel.ISupportInitialize)decimalEntryFormatterComponent1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PowerTools.Components.ThemingComponent themingComponent1;
        private PowerTools.Controls.ToolStripSymbolMenuItem toolStripFluentSymbolMenuItem1;
        private PowerTools.Controls.DataEntryPanel dataEntryPanel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataEntryForms.Components.DateEntryFormatterComponent dateEntryFormatterComponent1;
        private System.Windows.Forms.DataEntryForms.Components.DecimalEntryFormatterComponent decimalEntryFormatterComponent1;
    }
}