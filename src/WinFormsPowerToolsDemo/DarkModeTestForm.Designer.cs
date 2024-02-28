using System.Windows.Forms.DataEntryForms.Components;
using System.Windows.Forms.DataEntryForms.Controls;

namespace WinFormsPowerToolsDemo
{
    partial class DarkModeTestForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DarkModeTestForm));
            DecimalEntryFormatterComponent.DecimalDataEntryFormatter decimalDataEntryFormatter2 = new DecimalEntryFormatterComponent.DecimalDataEntryFormatter();
            DecimalEntryFormatterComponent.DecimalDataEntryFormatter decimalDataEntryFormatter1 = new DecimalEntryFormatterComponent.DecimalDataEntryFormatter();
            DateEntryFormatterComponent.DateDataEntryFormatter dateDataEntryFormatter1 = new DateEntryFormatterComponent.DateDataEntryFormatter();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            cancelToolStripButton = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            clearToolStripButton = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            settingsToolStripButton = new System.Windows.Forms.ToolStripButton();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            dataEntry1 = new DataEntry();
            decimalEntryFormatterComponent1 = new DecimalEntryFormatterComponent();
            dataEntry2 = new DataEntry();
            dataEntry3 = new DataEntry();
            dateEntryFormatterComponent1 = new DateEntryFormatterComponent();
            label3 = new System.Windows.Forms.Label();
            btnSetTypesValueDelayed = new System.Windows.Forms.Button();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            lblTypedValues = new System.Windows.Forms.Label();
            lblObjectValues = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            btnSetObjectValuesDelayed = new System.Windows.Forms.Button();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataEntry1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)decimalEntryFormatterComponent1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataEntry2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataEntry3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateEntryFormatterComponent1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
            toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { saveToolStripButton, cancelToolStripButton, toolStripSeparator1, clearToolStripButton, toolStripSeparator2, settingsToolStripButton });
            toolStrip1.Location = new System.Drawing.Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(914, 35);
            toolStrip1.TabIndex = 5;
            toolStrip1.Text = "toolStrip1";
            // 
            // saveToolStripButton
            // 
            saveToolStripButton.AutoSize = false;
            saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            saveToolStripButton.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F);
            saveToolStripButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            saveToolStripButton.Image = (System.Drawing.Image)resources.GetObject("saveToolStripButton.Image");
            saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            saveToolStripButton.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            saveToolStripButton.Name = "saveToolStripButton";
            saveToolStripButton.Size = new System.Drawing.Size(32, 32);
            saveToolStripButton.Text = "";
            saveToolStripButton.ToolTipText = "Save";
            // 
            // cancelToolStripButton
            // 
            cancelToolStripButton.AutoSize = false;
            cancelToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            cancelToolStripButton.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F);
            cancelToolStripButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            cancelToolStripButton.Image = (System.Drawing.Image)resources.GetObject("cancelToolStripButton.Image");
            cancelToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            cancelToolStripButton.Name = "cancelToolStripButton";
            cancelToolStripButton.Size = new System.Drawing.Size(32, 32);
            cancelToolStripButton.Text = "";
            cancelToolStripButton.ToolTipText = "Cancel/Undo";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Margin = new System.Windows.Forms.Padding(3, 0, 6, 0);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // clearToolStripButton
            // 
            clearToolStripButton.AutoSize = false;
            clearToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            clearToolStripButton.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F);
            clearToolStripButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            clearToolStripButton.Image = (System.Drawing.Image)resources.GetObject("clearToolStripButton.Image");
            clearToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            clearToolStripButton.Name = "clearToolStripButton";
            clearToolStripButton.Size = new System.Drawing.Size(38, 32);
            clearToolStripButton.Text = "\u0083";
            clearToolStripButton.ToolTipText = "Clear entries";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
            // 
            // settingsToolStripButton
            // 
            settingsToolStripButton.AutoSize = false;
            settingsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            settingsToolStripButton.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F);
            settingsToolStripButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            settingsToolStripButton.Image = (System.Drawing.Image)resources.GetObject("settingsToolStripButton.Image");
            settingsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            settingsToolStripButton.Name = "settingsToolStripButton";
            settingsToolStripButton.Size = new System.Drawing.Size(32, 32);
            settingsToolStripButton.Text = "";
            settingsToolStripButton.ToolTipText = "Settings";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            label1.Location = new System.Drawing.Point(14, 80);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(116, 20);
            label1.TabIndex = 0;
            label1.Text = "Numeric Field 1:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            label2.Location = new System.Drawing.Point(14, 119);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(116, 20);
            label2.TabIndex = 2;
            label2.Text = "Numeric Field 2:";
            // 
            // dataEntry1
            // 
            dataEntry1.ErrorColor = System.Drawing.Color.Red;
            dataEntry1.FocusColor = System.Drawing.Color.Yellow;
            dataEntry1.FocusEmphasize = true;
            dataEntry1.FocusSelectionBehavior = FocusSelectionBehaviors.PreSelectInput;
            dataEntry1.Formatter = decimalEntryFormatterComponent1;
            decimalDataEntryFormatter2.CurrencySymbol = null;
            decimalDataEntryFormatter2.DecimalPlaces = 0;
            decimalEntryFormatterComponent1.SetFormattingProperties(dataEntry1, decimalDataEntryFormatter2);
            dataEntry1.Location = new System.Drawing.Point(135, 73);
            dataEntry1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            dataEntry1.Name = "dataEntry1";
            dataEntry1.Size = new System.Drawing.Size(209, 27);
            dataEntry1.TabIndex = 1;
            decimalEntryFormatterComponent1.SetValue(dataEntry1, new decimal(new int[] { 0, 0, 0, 0 }));
            // 
            // decimalEntryFormatterComponent1
            // 
            decimalEntryFormatterComponent1.BlinkRate = 300;
            decimalEntryFormatterComponent1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            decimalEntryFormatterComponent1.ContainerControl = this;
            // 
            // dataEntry2
            // 
            dataEntry2.ErrorColor = System.Drawing.Color.Red;
            dataEntry2.FocusColor = System.Drawing.Color.Yellow;
            dataEntry2.FocusEmphasize = true;
            dataEntry2.FocusSelectionBehavior = FocusSelectionBehaviors.PreSelectInput;
            dataEntry2.Formatter = decimalEntryFormatterComponent1;
            decimalDataEntryFormatter1.CurrencySymbol = null;
            decimalDataEntryFormatter1.DecimalPlaces = 0;
            decimalEntryFormatterComponent1.SetFormattingProperties(dataEntry2, decimalDataEntryFormatter1);
            dataEntry2.Location = new System.Drawing.Point(135, 112);
            dataEntry2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            dataEntry2.Name = "dataEntry2";
            dataEntry2.Size = new System.Drawing.Size(209, 27);
            dataEntry2.TabIndex = 3;
            decimalEntryFormatterComponent1.SetValue(dataEntry2, new decimal(new int[] { 0, 0, 0, 0 }));
            // 
            // dataEntry3
            // 
            dataEntry3.ErrorColor = System.Drawing.Color.Red;
            dataEntry3.FocusColor = System.Drawing.Color.Yellow;
            dataEntry3.FocusEmphasize = true;
            dataEntry3.FocusSelectionBehavior = FocusSelectionBehaviors.PreSelectInput;
            dataEntry3.Formatter = dateEntryFormatterComponent1;
            dateEntryFormatterComponent1.SetFormattingProperties(dataEntry3, dateDataEntryFormatter1);
            dataEntry3.Location = new System.Drawing.Point(135, 151);
            dataEntry3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            dataEntry3.Name = "dataEntry3";
            dataEntry3.Size = new System.Drawing.Size(209, 27);
            dataEntry3.TabIndex = 5;
            dateEntryFormatterComponent1.SetValue(dataEntry3, new System.DateTime(2020, 8, 4, 0, 0, 0, 0));
            // 
            // dateEntryFormatterComponent1
            // 
            dateEntryFormatterComponent1.ContainerControl = this;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            label3.Location = new System.Drawing.Point(14, 157);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(92, 20);
            label3.TabIndex = 4;
            label3.Text = "Date Field 1:";
            // 
            // btnSetTypesValueDelayed
            // 
            btnSetTypesValueDelayed.Location = new System.Drawing.Point(687, 68);
            btnSetTypesValueDelayed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnSetTypesValueDelayed.Name = "btnSetTypesValueDelayed";
            btnSetTypesValueDelayed.Size = new System.Drawing.Size(211, 44);
            btnSetTypesValueDelayed.TabIndex = 6;
            btnSetTypesValueDelayed.Text = "Set types values delayed";
            btnSetTypesValueDelayed.UseVisualStyleBackColor = true;
            btnSetTypesValueDelayed.Click += BtnSetValueDelayed_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel1.Controls.Add(lblTypedValues, 2, 0);
            tableLayoutPanel1.Controls.Add(lblObjectValues, 1, 0);
            tableLayoutPanel1.Controls.Add(textBox1, 0, 0);
            tableLayoutPanel1.Location = new System.Drawing.Point(15, 391);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new System.Drawing.Size(887, 188);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // lblTypedValues
            // 
            lblTypedValues.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            lblTypedValues.Dock = System.Windows.Forms.DockStyle.Fill;
            lblTypedValues.ForeColor = System.Drawing.SystemColors.ButtonFace;
            lblTypedValues.Location = new System.Drawing.Point(593, 0);
            lblTypedValues.Name = "lblTypedValues";
            lblTypedValues.Size = new System.Drawing.Size(291, 188);
            lblTypedValues.TabIndex = 9;
            lblTypedValues.Text = "#TypedValues";
            lblTypedValues.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblObjectValues
            // 
            lblObjectValues.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            lblObjectValues.Dock = System.Windows.Forms.DockStyle.Fill;
            lblObjectValues.ForeColor = System.Drawing.SystemColors.ButtonFace;
            lblObjectValues.Location = new System.Drawing.Point(298, 0);
            lblObjectValues.Name = "lblObjectValues";
            lblObjectValues.Size = new System.Drawing.Size(289, 188);
            lblObjectValues.TabIndex = 8;
            lblObjectValues.Text = "#ObjectValues";
            lblObjectValues.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox1.Location = new System.Drawing.Point(3, 4);
            textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(289, 180);
            textBox1.TabIndex = 7;
            // 
            // btnSetObjectValuesDelayed
            // 
            btnSetObjectValuesDelayed.Location = new System.Drawing.Point(687, 120);
            btnSetObjectValuesDelayed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnSetObjectValuesDelayed.Name = "btnSetObjectValuesDelayed";
            btnSetObjectValuesDelayed.Size = new System.Drawing.Size(211, 44);
            btnSetObjectValuesDelayed.TabIndex = 11;
            btnSetObjectValuesDelayed.Text = "Set object values delayed";
            btnSetObjectValuesDelayed.UseVisualStyleBackColor = true;
            btnSetObjectValuesDelayed.Click += BtnSetObjectValuesDelayed_Click;
            // 
            // DarkModeTestForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
            ClientSize = new System.Drawing.Size(914, 600);
            Controls.Add(btnSetObjectValuesDelayed);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(btnSetTypesValueDelayed);
            Controls.Add(dataEntry3);
            Controls.Add(label3);
            Controls.Add(dataEntry2);
            Controls.Add(dataEntry1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(toolStrip1);
            Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            Name = "DarkModeTestForm";
            Text = "Numeric Field 1:";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataEntry1).EndInit();
            ((System.ComponentModel.ISupportInitialize)decimalEntryFormatterComponent1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataEntry2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataEntry3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateEntryFormatterComponent1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataEntry dataEntry1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton cancelToolStripButton;
        private System.Windows.Forms.ToolStripButton clearToolStripButton;
        private System.Windows.Forms.ToolStripButton settingsToolStripButton;
        private DecimalEntryFormatterComponent decimalEntryFormatterComponent1;
        private DataEntry dataEntry2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSetTypesValueDelayed;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblObjectValues;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblTypedValues;
        private System.Windows.Forms.Button btnSetObjectValuesDelayed;
        private DataEntry dataEntry3;
        private DateEntryFormatterComponent dateEntryFormatterComponent1;
    }
}

