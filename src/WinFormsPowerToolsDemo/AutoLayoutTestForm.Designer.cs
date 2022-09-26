using System.Windows.Forms.DataEntryForms.Controls;

namespace AutoLayoutDemo
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
            this.generatedUserControl1 = new WinFormsPowerToolsDemo.GeneratedUserControl();
            this.SuspendLayout();
            // 
            // generatedUserControl1
            // 
            this.generatedUserControl1.DataContext = null;
            this.generatedUserControl1.Location = new System.Drawing.Point(38, 53);
            this.generatedUserControl1.Name = "generatedUserControl1";
            this.generatedUserControl1.Size = new System.Drawing.Size(499, 374);
            this.generatedUserControl1.TabIndex = 0;
            // 
            // AutoLayoutTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1355, 992);
            this.Controls.Add(this.generatedUserControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AutoLayoutTestForm";
            this.Text = "AutoLayoutTestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private WinFormsPowerToolsDemo.AutoLayoutTestUserControl autoLayoutTestUserControl1;
        private WinFormsPowerToolsDemo.GeneratedUserControl generatedUserControl1;
    }
}