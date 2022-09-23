using System.Windows.Forms.DataEntryForms.Controls;

namespace ExtenderPropertiesTest
{
    partial class AutoLayoutTestForm
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
            this.autoLayoutTestUserControl1 = new WinFormsPowerToolsDemo.AutoLayoutTestUserControl();
            this.SuspendLayout();
            // 
            // autoLayoutTestUserControl1
            // 
            this.autoLayoutTestUserControl1.Location = new System.Drawing.Point(12, 28);
            this.autoLayoutTestUserControl1.Name = "autoLayoutTestUserControl1";
            this.autoLayoutTestUserControl1.Size = new System.Drawing.Size(568, 320);
            this.autoLayoutTestUserControl1.TabIndex = 0;
            // 
            // AutoLayoutTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 377);
            this.Controls.Add(this.autoLayoutTestUserControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AutoLayoutTestForm";
            this.Text = "AutoLayoutTestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private WinFormsPowerToolsDemo.AutoLayoutTestUserControl autoLayoutTestUserControl1;
    }
}