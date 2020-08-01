using System.Windows.Forms.DataEntryForms;

namespace ExtenderPropertiesTest
{
    partial class Form2
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
            this.dataEntry1 = new System.Windows.Forms.DataEntryForms.DataEntry();
            ((System.ComponentModel.ISupportInitialize)(this.dataEntry1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataEntry1
            // 
            this.dataEntry1.ErrorColor = System.Drawing.Color.Red;
            this.dataEntry1.FocusColor = System.Drawing.Color.Yellow;
            this.dataEntry1.FocusEmphasize = true;
            this.dataEntry1.FocusSelectionBehaviour = System.Windows.Forms.DataEntryForms.FocusSelectionBehaviours.PreSelectInput;
            this.dataEntry1.Formatter = null;
            this.dataEntry1.Location = new System.Drawing.Point(12, 12);
            this.dataEntry1.Name = "dataEntry1";
            this.dataEntry1.Size = new System.Drawing.Size(191, 23);
            this.dataEntry1.TabIndex = 1;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataEntry1);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.dataEntry1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DataEntry dataEntry1;
    }
}