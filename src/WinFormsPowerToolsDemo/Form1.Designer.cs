using System.Windows.Forms.DataEntryForms.Components;
using System.Windows.Forms.DataEntryForms.Controls;

namespace WinFormsPowerToolsDemo
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataEntryForms.Components.DecimalEntryFormatterComponent.DecimalDataEntryFormatter decimalDataEntryFormatter2 = new System.Windows.Forms.DataEntryForms.Components.DecimalEntryFormatterComponent.DecimalDataEntryFormatter();
            System.Windows.Forms.DataEntryForms.Components.DecimalEntryFormatterComponent.DecimalDataEntryFormatter decimalDataEntryFormatter1 = new System.Windows.Forms.DataEntryForms.Components.DecimalEntryFormatterComponent.DecimalDataEntryFormatter();
            System.Windows.Forms.DataEntryForms.Components.DateEntryFormatterComponent.DateDataEntryFormatter dateDataEntryFormatter1 = new System.Windows.Forms.DataEntryForms.Components.DateEntryFormatterComponent.DateDataEntryFormatter();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.cancelToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataEntry1 = new System.Windows.Forms.DataEntryForms.Controls.DataEntry();
            this.decimalEntryFormatterComponent1 = new System.Windows.Forms.DataEntryForms.Components.DecimalEntryFormatterComponent();
            this.dataEntry2 = new System.Windows.Forms.DataEntryForms.Controls.DataEntry();
            this.dataEntry3 = new System.Windows.Forms.DataEntryForms.Controls.DataEntry();
            this.dateEntryFormatterComponent1 = new System.Windows.Forms.DataEntryForms.Components.DateEntryFormatterComponent();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSetTypesValueDelayed = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTypedValues = new System.Windows.Forms.Label();
            this.lblObjectValues = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSetObjectValuesDelayed = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataEntry1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.decimalEntryFormatterComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataEntry2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataEntry3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEntryFormatterComponent1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripButton,
            this.cancelToolStripButton,
            this.toolStripSeparator1,
            this.clearToolStripButton,
            this.toolStripSeparator2,
            this.settingsToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 35);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.AutoSize = false;
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveToolStripButton.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.saveToolStripButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(32, 32);
            this.saveToolStripButton.Text = "";
            this.saveToolStripButton.ToolTipText = "Save";
            // 
            // cancelToolStripButton
            // 
            this.cancelToolStripButton.AutoSize = false;
            this.cancelToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cancelToolStripButton.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cancelToolStripButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cancelToolStripButton.Image")));
            this.cancelToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cancelToolStripButton.Name = "cancelToolStripButton";
            this.cancelToolStripButton.Size = new System.Drawing.Size(32, 32);
            this.cancelToolStripButton.Text = "";
            this.cancelToolStripButton.ToolTipText = "Cancel/Undo";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(3, 0, 6, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // clearToolStripButton
            // 
            this.clearToolStripButton.AutoSize = false;
            this.clearToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clearToolStripButton.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.clearToolStripButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.clearToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("clearToolStripButton.Image")));
            this.clearToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearToolStripButton.Name = "clearToolStripButton";
            this.clearToolStripButton.Size = new System.Drawing.Size(38, 32);
            this.clearToolStripButton.Text = "";
            this.clearToolStripButton.ToolTipText = "Clear entries";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
            // 
            // settingsToolStripButton
            // 
            this.settingsToolStripButton.AutoSize = false;
            this.settingsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsToolStripButton.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.settingsToolStripButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.settingsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("settingsToolStripButton.Image")));
            this.settingsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsToolStripButton.Name = "settingsToolStripButton";
            this.settingsToolStripButton.Size = new System.Drawing.Size(32, 32);
            this.settingsToolStripButton.Text = "";
            this.settingsToolStripButton.ToolTipText = "Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(12, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numeric Field 1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(12, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Numeric Field 2:";
            // 
            // dataEntry1
            // 
            this.dataEntry1.ErrorColor = System.Drawing.Color.Red;
            this.dataEntry1.FocusColor = System.Drawing.Color.Yellow;
            this.dataEntry1.FocusEmphasize = true;
            this.dataEntry1.FocusSelectionBehavior = System.Windows.Forms.DataEntryForms.Controls.FocusSelectionBehaviors.PreSelectInput;
            this.dataEntry1.Formatter = this.decimalEntryFormatterComponent1;
            decimalDataEntryFormatter2.CurrencySymbol = null;
            decimalDataEntryFormatter2.DecimalPlaces = 0;
            this.decimalEntryFormatterComponent1.SetFormattingProperties(this.dataEntry1, decimalDataEntryFormatter2);
            this.dataEntry1.Location = new System.Drawing.Point(118, 55);
            this.dataEntry1.Name = "dataEntry1";
            this.dataEntry1.Size = new System.Drawing.Size(183, 23);
            this.dataEntry1.TabIndex = 1;
            this.decimalEntryFormatterComponent1.SetValue(this.dataEntry1, new decimal(new int[] {
                0,
                0,
                0,
                0}));
            // 
            // decimalEntryFormatterComponent1
            // 
            this.decimalEntryFormatterComponent1.BlinkRate = 300;
            this.decimalEntryFormatterComponent1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.decimalEntryFormatterComponent1.ContainerControl = this;
            // 
            // dataEntry2
            // 
            this.dataEntry2.ErrorColor = System.Drawing.Color.Red;
            this.dataEntry2.FocusColor = System.Drawing.Color.Yellow;
            this.dataEntry2.FocusEmphasize = true;
            this.dataEntry2.FocusSelectionBehavior = System.Windows.Forms.DataEntryForms.Controls.FocusSelectionBehaviors.PreSelectInput;
            this.dataEntry2.Formatter = this.decimalEntryFormatterComponent1;
            decimalDataEntryFormatter1.CurrencySymbol = null;
            decimalDataEntryFormatter1.DecimalPlaces = 0;
            this.decimalEntryFormatterComponent1.SetFormattingProperties(this.dataEntry2, decimalDataEntryFormatter1);
            this.dataEntry2.Location = new System.Drawing.Point(118, 84);
            this.dataEntry2.Name = "dataEntry2";
            this.dataEntry2.Size = new System.Drawing.Size(183, 23);
            this.dataEntry2.TabIndex = 3;
            this.decimalEntryFormatterComponent1.SetValue(this.dataEntry2, new decimal(new int[] {
                0,
                0,
                0,
                0}));
            // 
            // dataEntry3
            // 
            this.dataEntry3.ErrorColor = System.Drawing.Color.Red;
            this.dataEntry3.FocusColor = System.Drawing.Color.Yellow;
            this.dataEntry3.FocusEmphasize = true;
            this.dataEntry3.FocusSelectionBehavior = System.Windows.Forms.DataEntryForms.Controls.FocusSelectionBehaviors.PreSelectInput;
            this.dataEntry3.Formatter = this.dateEntryFormatterComponent1;
            this.dateEntryFormatterComponent1.SetFormattingProperties(this.dataEntry3, dateDataEntryFormatter1);
            this.dataEntry3.Location = new System.Drawing.Point(118, 113);
            this.dataEntry3.Name = "dataEntry3";
            this.dataEntry3.Size = new System.Drawing.Size(183, 23);
            this.dataEntry3.TabIndex = 5;
            this.dateEntryFormatterComponent1.SetValue(this.dataEntry3, new System.DateTime(2020, 8, 4, 0, 0, 0, 0));
            // 
            // dateEntryFormatterComponent1
            // 
            this.dateEntryFormatterComponent1.ContainerControl = this;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(12, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Date Field 1:";
            // 
            // btnSetTypesValueDelayed
            // 
            this.btnSetTypesValueDelayed.Location = new System.Drawing.Point(601, 51);
            this.btnSetTypesValueDelayed.Name = "btnSetTypesValueDelayed";
            this.btnSetTypesValueDelayed.Size = new System.Drawing.Size(185, 33);
            this.btnSetTypesValueDelayed.TabIndex = 6;
            this.btnSetTypesValueDelayed.Text = "Set types values delayed";
            this.btnSetTypesValueDelayed.UseVisualStyleBackColor = true;
            this.btnSetTypesValueDelayed.Click += new System.EventHandler(this.btnSetValueDelayed_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.lblTypedValues, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblObjectValues, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 293);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(776, 141);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // lblTypedValues
            // 
            this.lblTypedValues.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTypedValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTypedValues.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTypedValues.Location = new System.Drawing.Point(519, 0);
            this.lblTypedValues.Name = "lblTypedValues";
            this.lblTypedValues.Size = new System.Drawing.Size(254, 141);
            this.lblTypedValues.TabIndex = 9;
            this.lblTypedValues.Text = "#TypedValues";
            this.lblTypedValues.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblObjectValues
            // 
            this.lblObjectValues.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblObjectValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblObjectValues.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblObjectValues.Location = new System.Drawing.Point(261, 0);
            this.lblObjectValues.Name = "lblObjectValues";
            this.lblObjectValues.Size = new System.Drawing.Size(252, 141);
            this.lblObjectValues.TabIndex = 8;
            this.lblObjectValues.Text = "#ObjectValues";
            this.lblObjectValues.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(252, 135);
            this.textBox1.TabIndex = 7;
            // 
            // btnSetObjectValuesDelayed
            // 
            this.btnSetObjectValuesDelayed.Location = new System.Drawing.Point(601, 90);
            this.btnSetObjectValuesDelayed.Name = "btnSetObjectValuesDelayed";
            this.btnSetObjectValuesDelayed.Size = new System.Drawing.Size(185, 33);
            this.btnSetObjectValuesDelayed.TabIndex = 11;
            this.btnSetObjectValuesDelayed.Text = "Set object values delayed";
            this.btnSetObjectValuesDelayed.UseVisualStyleBackColor = true;
            this.btnSetObjectValuesDelayed.Click += new System.EventHandler(this.btnSetObjectValuesDelayed_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSetObjectValuesDelayed);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnSetTypesValueDelayed);
            this.Controls.Add(this.dataEntry3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataEntry2);
            this.Controls.Add(this.dataEntry1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "Numeric Field 1:";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataEntry1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.decimalEntryFormatterComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataEntry2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataEntry3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEntryFormatterComponent1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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

