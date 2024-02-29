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
            this.mainFormMenu1 = new WinFormsPowerToolsDemo.MainFormMenu();
            this.generatedUserControl1 = new WinFormsPowerToolsDemo.GeneratedUserControl();
            this.SuspendLayout();
            // 
            // mainFormMenu1
            // 
            this.mainFormMenu1.DataContext = null;
            this.mainFormMenu1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainFormMenu1.Location = new System.Drawing.Point(0, 0);
            this.mainFormMenu1.Name = "mainFormMenu1";
            this.mainFormMenu1.Size = new System.Drawing.Size(1076, 28);
            this.mainFormMenu1.TabIndex = 0;
            this.mainFormMenu1.Text = "mainFormMenu1";
            // 
            // generatedUserControl1
            // 
            this.generatedUserControl1.DataContext = null;
            this.generatedUserControl1.Location = new System.Drawing.Point(184, 110);
            this.generatedUserControl1.Name = "generatedUserControl1";
            this.generatedUserControl1.Padding = new System.Windows.Forms.Padding(10);
            this.generatedUserControl1.Size = new System.Drawing.Size(645, 410);
            this.generatedUserControl1.TabIndex = 1;
            // 
            // AutoLayoutTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 610);
            this.Controls.Add(this.generatedUserControl1);
            this.Controls.Add(this.mainFormMenu1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AutoLayoutTestForm";
            this.Text = "AutoLayoutTestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WinFormsPowerToolsDemo.MainFormMenu mainFormMenu1;
        private WinFormsPowerToolsDemo.GeneratedUserControl generatedUserControl1;
    }
}
